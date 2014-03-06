﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GOLD.Engine.Enums;

namespace GOLD.Engine
{
    public class Parser
    {
        public event Action<Token> OnTokenRead;
        public event Action<Tuple<int, int>, Tuple<int, int>, Reduction> OnReduction;
        public event Action<object> OnCompletion;
        public event Action OnGroupError;
        public event Action OnInternalError;
        public event Action OnNotLoadedError;
        public event Action<Tuple<int, int>, string> OnLexicalError;
        public event Action<Tuple<int, int>, string, string> OnSyntaxError;


        public bool TrimReductions { get; set; }
        public bool TablesLoaded { get; private set; }
        public GrammarProperties GrammarProperties { get; private set; }

        private GrammarTables grammarTables;

        public Parser()
        {
            TrimReductions = false;
            TablesLoaded = false;
        }

        private void Reset()
        {
            TablesLoaded = false;
            GrammarProperties = null;
            grammarTables.Clear();
        }

        public bool LoadEGT(BinaryReader reader)
        {
            if(TablesLoaded)
            {
                Reset();
            }

            using (EGTReader egtReader = new EGTReader(reader))
            {
                while (egtReader.GetNextRecord())
                {
                    switch (egtReader.ReadRecordType())
                    {
                        case EGTRecord.Property:
                            GrammarProperties = new GrammarProperties();
                            GrammarProperties.SetProperty(egtReader.ReadGrammarProperty());
                            break;

                        case EGTRecord.TableCounts:
                            grammarTables = egtReader.ReadGrammarTables();
                            break;

                        case EGTRecord.InitialStates:
                            //DFA, LALR
                            ushort dfaState = egtReader.ReadUInt16();
                            ushort lalrState = egtReader.ReadUInt16();

                            Debug.Assert(dfaState == 0, "The initial DFA State is not 0!");
                            Debug.Assert(lalrState == 0, "The initial LALR State is not 0!");
                            break;

                        case EGTRecord.Symbol:
                            Symbol sym = egtReader.ReadSymbol();
                            grammarTables.Symbols[sym.TableIndex] = sym;
                            break;

                        case EGTRecord.Group:
                            Group group = egtReader.ReadGroup();
                            grammarTables.Groups[group.TableIndex] = group;
                            break;

                        case EGTRecord.CharRanges:
                            CharacterSet charSet = egtReader.ReadCharacterSet();
                            grammarTables.CharacterSets[charSet.Index] = charSet;
                            break;

                        case EGTRecord.Production:
                            Production prod = egtReader.ReadProduction();
                            grammarTables.Productions[prod.TableIndex] = prod;
                            break;

                        case EGTRecord.DFAState:
                            FAState faState = egtReader.ReadFAState();
                            grammarTables.FAStates[faState.TableIndex] = faState;
                            break;

                        case EGTRecord.LRState:
                            LRActionList actionList = egtReader.ReadLRActionList();
                            grammarTables.LRActionLists[actionList.Index] = actionList;
                            break;
                    }
                }
            }

            TablesLoaded = true;
            return true;
        }

        //This function implements the DFA for th parser's lexer.
        //It generates a token which is used by the LALR state
        //machine.
        private Token LookAheadDFA(LookAheadBuffer lookAheadBuffer)
        {
            string ch = lookAheadBuffer.LookAhead(1);

            if (String.IsNullOrEmpty(ch) || ch[0] == Char.MaxValue)
            {
                return new Token(grammarTables.Symbols.GetFirstOfType(SymbolType.End), String.Empty)
                {
                    StartPosition = lookAheadBuffer.Position,
                    EndPosition = lookAheadBuffer.Position
                };
            }

            int currentDFA = 0;
            int lookAheadPosition = 1;
            //Next byte in the input Stream
            int lastAcceptState = -1;
            //We have not yet accepted a character string
            int lastAcceptPosition = -1;

            while (true)
            {
                bool found = false;
                ch = lookAheadBuffer.LookAhead(lookAheadPosition);

                if (!String.IsNullOrEmpty(ch))
                {
                    for (int i = 0; i < grammarTables.FAStates[currentDFA].Edges.Count; i++)
                    {
                        FAEdge Edge = grammarTables.FAStates[currentDFA].Edges[i];

                        //Look for character in the Character Set Table
                        if (Edge.Characters.Contains(ch[0]))
                        {
                            int target = Edge.Target;

                            if (grammarTables.FAStates[target].Accept != null)
                            {
                                lastAcceptState = target;
                                lastAcceptPosition = lookAheadPosition;
                            }

                            currentDFA = target;
                            lookAheadPosition++;

                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    Symbol symbol = null;
                    string data = String.Empty;

                    // Lexer cannot recognize symbol
                    if (lastAcceptState == -1)
                    {
                        symbol = grammarTables.Symbols.GetFirstOfType(SymbolType.Error);
                        data = lookAheadBuffer.GetTextFromBuffer(1);
                    }
                    else
                    {
                        symbol = grammarTables.FAStates[lastAcceptState].Accept;
                        data = lookAheadBuffer.GetTextFromBuffer(lastAcceptPosition);
                    }

                    return new Token(symbol, data)
                    {
                        StartPosition = lookAheadBuffer.Position
                    };
                }
            }
        }

        private Token ProduceToken(Stack<Token> groupStack, LookAheadBuffer lookAheadBuffer)
        {
            while (true)
            {
                Token read = LookAheadDFA(lookAheadBuffer);

                if (read.Type == SymbolType.GroupStart && (groupStack.Count == 0 || groupStack.Peek().Group.Nesting.Contains(read.Group.TableIndex)))
                {
                    lookAheadBuffer.Consume(read.Data.Length);
                    read.EndPosition = lookAheadBuffer.Position;
                    groupStack.Push(read);
                }
                else if (groupStack.Count == 0)
                {
                    //The token is ready to be analyzed.             
                    lookAheadBuffer.Consume(read.Data.Length);
                    read.EndPosition = lookAheadBuffer.Position;
                    return read;
                }
                else if (Object.ReferenceEquals(groupStack.Peek().Group.End, read.Symbol))
                {
                    //End the current group
                    Token pop = groupStack.Pop();

                    if (pop.Group.Ending == GroupEndingMode.Closed)
                    {
                        pop.Data += read.Data;
                        lookAheadBuffer.Consume(read.Data.Length);
                        read.EndPosition = lookAheadBuffer.Position;
                    }

                    //We are out of the group. Return pop'd token (which contains all the group text)
                    if (groupStack.Count == 0)
                    {
                        //Change symbol to parent
                        pop.Symbol = pop.Group.Container;
                        return pop;
                    }
                    else
                    {
                        groupStack.Peek().Data += pop.Data;
                    }
                }
                else if (read.Type == SymbolType.End)
                {
                    return read;
                }
                else
                {
                    //We are in a group, Append to the Token on the top of the stack.
                    //Take into account the Token group mode  
                    Token top = groupStack.Peek();

                    if (top.Group.Advance == GroupAdvanceMode.Token)
                    {
                        // Append all text
                        top.Data += read.Data;
                        lookAheadBuffer.Consume(read.Data.Length);
                        read.EndPosition = lookAheadBuffer.Position;
                    }
                    else
                    {
                        // Append one character
                        top.Data += read.Data[0].ToString();
                        lookAheadBuffer.Consume(1);
                        read.EndPosition = lookAheadBuffer.Position;
                    }
                }
            }
        }

        public bool Parse(ref string input)
        {
            return Parse(new StringReader(input));
        }

        public bool Parse(TextReader reader)
        {
            if(!TablesLoaded)
            {
                if(OnNotLoadedError != null)
                {
                    OnNotLoadedError();
                }
                return false;
            }

            TokenQueueStack inputTokens = new TokenQueueStack();
            Stack<Token> tokenStack = new Stack<Token>();
            tokenStack.Push(new Token());
            Stack<Token> groupStack = new Stack<Token>();
            LookAheadBuffer lookAheadBuffer = new LookAheadBuffer(reader);
            ushort lalrState = 0;

            while (true)
            {
                if (inputTokens.Count == 0)
                {
                    inputTokens.Push(ProduceToken(groupStack, lookAheadBuffer));
                    if(OnTokenRead != null)
                    {
                        OnTokenRead(inputTokens.Peek());
                    }
                }
                //Runaway group
                else if (groupStack.Count != 0)
                {
                    if(OnGroupError != null)
                    {
                        OnGroupError();
                    }
                    return false; //Error; abort
                }
                else
                {
                    Token read = inputTokens.Peek();

                    switch (read.Type)
                    {
                        case SymbolType.Noise:
                            inputTokens.Pop();
                            break;

                        case SymbolType.Error:
                            if(OnLexicalError != null)
                            {
                                OnLexicalError(read.EndPosition, read.Data);
                            }
                            return false; //Error; abort

                        default:
                            LRAction parseAction = grammarTables.LRActionLists[lalrState][read.Symbol];
                            if (parseAction == null)
                            {
                                SymbolList expectedSymbols = new SymbolList();
                                for (int i = 0; i < grammarTables.LRActionLists[lalrState].Count; i++)
                                {
                                    Symbol actionSymbol = grammarTables.LRActionLists[lalrState][i].Symbol;
                                    switch (actionSymbol.Type)
                                    {
                                        case SymbolType.Content:
                                        case SymbolType.End:
                                        case SymbolType.GroupStart:
                                        case SymbolType.GroupEnd:
                                            expectedSymbols.Add(actionSymbol);
                                            break;
                                    }
                                }

                                if (OnSyntaxError != null)
                                {
                                    OnSyntaxError(read.EndPosition, read.Data, expectedSymbols.Text());
                                }
                                return false;
                            }

                            switch(parseAction.Type)
                            {
                                case LRActionType.Accept:
                                    if (OnCompletion != null)
                                    {
                                        OnCompletion(tokenStack.Peek().Tag);
                                    }
                                    return true;

                                case LRActionType.Shift:
                                    lalrState = parseAction.Value;
                                    read.State = lalrState;
                                    tokenStack.Push(read);
                                    //It now exists on the Token-Stack and must be eliminated from the queue.
                                    inputTokens.Dequeue();
                                    break;

                                case LRActionType.Reduce:
                                    Token head = null;
                                    //Produce a reduction - remove as many tokens as members in the rule & push a nonterminal token
                                    Production prod = grammarTables.Productions[parseAction.Value];
                                    bool reductionSkipped = false;

                                    //Skip reduction?
                                    if (TrimReductions && prod.ContainsOneNonTerminal())
                                    {
                                        head = tokenStack.Pop();
                                        head.Symbol = prod.Head;
                                        reductionSkipped = true;
                                    }
                                    //Build a Reduction
                                    else
                                    {
                                        Reduction reduction = new Reduction(prod.Handle.Count, prod);

                                        for (int i = prod.Handle.Count - 1; i >= 0; i--)
                                        {
                                            reduction[i] = tokenStack.Pop();
                                        }

                                        //If a production has no handles, it has no location.
                                        //Set a would-be location instead.
                                        if (prod.Handle.Count == 0)
                                        {
                                            reduction.StartPosition = reduction.EndPosition = lookAheadBuffer.Position;
                                        }

                                        head = reduction;
                                    }

                                    ushort nextActionIndex = tokenStack.Peek().State;

                                    //If n is -1 here, then we have an Internal Table Error!!!
                                    int n = grammarTables.LRActionLists[nextActionIndex].IndexOf(prod.Head);
                                    if (n == -1)
                                    {
                                        if (OnInternalError != null)
                                        {
                                            OnInternalError();
                                        }
                                        return false;
                                    }

                                    lalrState = grammarTables.LRActionLists[nextActionIndex][n].Value;

                                    head.State = lalrState;
                                    tokenStack.Push(head);

                                    if (!reductionSkipped)
                                    {
                                        if (OnReduction != null)
                                        {
                                            Reduction r = (Reduction)tokenStack.Peek();
                                            OnReduction(r.StartPosition, r.EndPosition, r);
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }
        }
    }
}
