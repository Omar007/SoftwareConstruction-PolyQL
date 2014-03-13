﻿/*
 * Grammar Information:
 *
 * Name            : Questionaire Language
 * Version         : v2
 * Author          : Omar Pakker
 * About           : A grammar for Questionaires.
 * Case Sensitive  : 
 * Start Symbol    : 
 *
 *
 * Grammar Build Information:
 *
 * Output File Path : 
 * Output Path      : 
 * Output File      : 
 * Output File Base : 
 *
 * Symbol Count : 70
 * Rule Count   : 57
 */
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Grammar.Generated.v2
{
	public static class GrammarData
    {
        public const int SymbolCount = 70;
        public const int RuleCount = 57;

        public static readonly ReadOnlyDictionary<string, short> Rules = new ReadOnlyDictionary<string, short>(
	        new Dictionary<string, short>()
            {
                { "Type_String", 0 },                                       // <Type> ::= string
                { "Type_Int", 1 },                                          // <Type> ::= int
                { "Type_Real", 2 },                                         // <Type> ::= real
                { "Type_Bool", 3 },                                         // <Type> ::= bool
                { "Type_Date", 4 },                                         // <Type> ::= date
                { "Forms", 5 },                                             // <Forms> ::= <Form> <Forms>
                { "Forms2", 6 },                                            // <Forms> ::= <Form>
                { "Form_Form_Identifier", 7 },                              // <Form> ::= form Identifier <Block>
                { "Block_Lbrace_Rbrace", 8 },                               // <Block> ::= '{' <Statements> '}'
                { "Statements", 9 },                                        // <Statements> ::= <Statement> <Statements>
                { "Statements2", 10 },                                      // <Statements> ::= <Statement>
                { "Statement_If_Lparen_Rparen", 11 },                       // <Statement> ::= if '(' <Expression> ')' <Statement> <OptElse>
                { "Statement", 12 },                                        // <Statement> ::= <Block>
                { "Statement2", 13 },                                       // <Statement> ::= <Question>
                { "Statement_Gotonextform_Semi", 14 },                      // <Statement> ::= gotoNextForm ';'
                { "Statement_Loop_Lparen_Rparen", 15 },                     // <Statement> ::= loop '(' <Expression> ')' <Statement>
                { "Optelse_Else", 16 },                                     // <OptElse> ::= else <Statement>
                { "Optelse", 17 },                                          // <OptElse> ::= 
                { "Vardecl_Identifier_Colon", 18 },                         // <VarDecl> ::= Identifier ':' <Type>
                { "Varassign_Identifier_Colon_Eq", 19 },                    // <VarAssign> ::= Identifier ':' <Type> '=' <Expression>
                { "Question_Stringlit_Gtgt_Semi", 20 },                     // <Question> ::= StringLit '>>' <VarDecl> ';'
                { "Question_Stringlit_Ltlt_Semi", 21 },                     // <Question> ::= StringLit '<<' <VarAssign> ';'
                { "Question_Stringlit_Ltlt_Semi2", 22 },                    // <Question> ::= StringLit '<<' <Expression> ';'
                { "Expression_Question_Colon", 23 },                        // <Expression> ::= <OrExpr> '?' <OrExpr> ':' <Expression>
                { "Expression", 24 },                                       // <Expression> ::= <OrExpr>
                { "Orexpr_Pipepipe", 25 },                                  // <OrExpr> ::= <OrExpr> '||' <AndExpr>
                { "Orexpr", 26 },                                           // <OrExpr> ::= <AndExpr>
                { "Andexpr_Ampamp", 27 },                                   // <AndExpr> ::= <AndExpr> '&&' <EqExpr>
                { "Andexpr", 28 },                                          // <AndExpr> ::= <EqExpr>
                { "Eqexpr_Eqeq", 29 },                                      // <EqExpr> ::= <EqExpr> '==' <CompExpr>
                { "Eqexpr_Exclameq", 30 },                                  // <EqExpr> ::= <EqExpr> '!=' <CompExpr>
                { "Eqexpr", 31 },                                           // <EqExpr> ::= <CompExpr>
                { "Compexpr_Lt", 32 },                                      // <CompExpr> ::= <CompExpr> '<' <AddExpr>
                { "Compexpr_Gt", 33 },                                      // <CompExpr> ::= <CompExpr> '>' <AddExpr>
                { "Compexpr_Lteq", 34 },                                    // <CompExpr> ::= <CompExpr> '<=' <AddExpr>
                { "Compexpr_Gteq", 35 },                                    // <CompExpr> ::= <CompExpr> '>=' <AddExpr>
                { "Compexpr", 36 },                                         // <CompExpr> ::= <AddExpr>
                { "Addexpr_Plus", 37 },                                     // <AddExpr> ::= <AddExpr> '+' <MultExpr>
                { "Addexpr_Minus", 38 },                                    // <AddExpr> ::= <AddExpr> '-' <MultExpr>
                { "Addexpr", 39 },                                          // <AddExpr> ::= <MultExpr>
                { "Multexpr_Times", 40 },                                   // <MultExpr> ::= <MultExpr> '*' <PowerExpr>
                { "Multexpr_Div", 41 },                                     // <MultExpr> ::= <MultExpr> '/' <PowerExpr>
                { "Multexpr_Percent", 42 },                                 // <MultExpr> ::= <MultExpr> '%' <PowerExpr>
                { "Multexpr", 43 },                                         // <MultExpr> ::= <PowerExpr>
                { "Powerexpr_Caret", 44 },                                  // <PowerExpr> ::= <PowerExpr> '^' <NegateExpr>
                { "Powerexpr", 45 },                                        // <PowerExpr> ::= <NegateExpr>
                { "Negateexpr_Minus", 46 },                                 // <NegateExpr> ::= '-' <Value>
                { "Negateexpr_Exclam", 47 },                                // <NegateExpr> ::= '!' <Value>
                { "Negateexpr", 48 },                                       // <NegateExpr> ::= <Value>
                { "Value_Identifier", 49 },                                 // <Value> ::= Identifier
                { "Value", 50 },                                            // <Value> ::= <Literal>
                { "Value_Lparen_Rparen", 51 },                              // <Value> ::= '(' <Expression> ')'
                { "Literal_Stringlit", 52 },                                // <Literal> ::= StringLit
                { "Literal_Intlit", 53 },                                   // <Literal> ::= IntLit
                { "Literal_Reallit", 54 },                                  // <Literal> ::= RealLit
                { "Literal_Boollit", 55 },                                  // <Literal> ::= BoolLit
                { "Literal_Datelit", 56 }                                   // <Literal> ::= DateLit
            }
		);
    };
}



/*
 * Pre-build if's covering all the rules
 */
#region Rules If
////<Type> ::= string
//if (r.Parent.TableIndex() == Rules["Type_String"])
//{
//    return;
//}
////<Type> ::= int
//if (r.Parent.TableIndex() == Rules["Type_Int"])
//{
//    return;
//}
////<Type> ::= real
//if (r.Parent.TableIndex() == Rules["Type_Real"])
//{
//    return;
//}
////<Type> ::= bool
//if (r.Parent.TableIndex() == Rules["Type_Bool"])
//{
//    return;
//}
////<Type> ::= date
//if (r.Parent.TableIndex() == Rules["Type_Date"])
//{
//    return;
//}
////<Forms> ::= <Form> <Forms>
//if (r.Parent.TableIndex() == Rules["Forms"])
//{
//    return;
//}
////<Forms> ::= <Form>
//if (r.Parent.TableIndex() == Rules["Forms2"])
//{
//    return;
//}
////<Form> ::= form Identifier <Block>
//if (r.Parent.TableIndex() == Rules["Form_Form_Identifier"])
//{
//    return;
//}
////<Block> ::= '{' <Statements> '}'
//if (r.Parent.TableIndex() == Rules["Block_Lbrace_Rbrace"])
//{
//    return;
//}
////<Statements> ::= <Statement> <Statements>
//if (r.Parent.TableIndex() == Rules["Statements"])
//{
//    return;
//}
////<Statements> ::= <Statement>
//if (r.Parent.TableIndex() == Rules["Statements2"])
//{
//    return;
//}
////<Statement> ::= if '(' <Expression> ')' <Statement> <OptElse>
//if (r.Parent.TableIndex() == Rules["Statement_If_Lparen_Rparen"])
//{
//    return;
//}
////<Statement> ::= <Block>
//if (r.Parent.TableIndex() == Rules["Statement"])
//{
//    return;
//}
////<Statement> ::= <Question>
//if (r.Parent.TableIndex() == Rules["Statement2"])
//{
//    return;
//}
////<Statement> ::= gotoNextForm ';'
//if (r.Parent.TableIndex() == Rules["Statement_Gotonextform_Semi"])
//{
//    return;
//}
////<Statement> ::= loop '(' <Expression> ')' <Statement>
//if (r.Parent.TableIndex() == Rules["Statement_Loop_Lparen_Rparen"])
//{
//    return;
//}
////<OptElse> ::= else <Statement>
//if (r.Parent.TableIndex() == Rules["Optelse_Else"])
//{
//    return;
//}
////<OptElse> ::= 
//if (r.Parent.TableIndex() == Rules["Optelse"])
//{
//    return;
//}
////<VarDecl> ::= Identifier ':' <Type>
//if (r.Parent.TableIndex() == Rules["Vardecl_Identifier_Colon"])
//{
//    return;
//}
////<VarAssign> ::= Identifier ':' <Type> '=' <Expression>
//if (r.Parent.TableIndex() == Rules["Varassign_Identifier_Colon_Eq"])
//{
//    return;
//}
////<Question> ::= StringLit '>>' <VarDecl> ';'
//if (r.Parent.TableIndex() == Rules["Question_Stringlit_Gtgt_Semi"])
//{
//    return;
//}
////<Question> ::= StringLit '<<' <VarAssign> ';'
//if (r.Parent.TableIndex() == Rules["Question_Stringlit_Ltlt_Semi"])
//{
//    return;
//}
////<Question> ::= StringLit '<<' <Expression> ';'
//if (r.Parent.TableIndex() == Rules["Question_Stringlit_Ltlt_Semi2"])
//{
//    return;
//}
////<Expression> ::= <OrExpr> '?' <OrExpr> ':' <Expression>
//if (r.Parent.TableIndex() == Rules["Expression_Question_Colon"])
//{
//    return;
//}
////<Expression> ::= <OrExpr>
//if (r.Parent.TableIndex() == Rules["Expression"])
//{
//    return;
//}
////<OrExpr> ::= <OrExpr> '||' <AndExpr>
//if (r.Parent.TableIndex() == Rules["Orexpr_Pipepipe"])
//{
//    return;
//}
////<OrExpr> ::= <AndExpr>
//if (r.Parent.TableIndex() == Rules["Orexpr"])
//{
//    return;
//}
////<AndExpr> ::= <AndExpr> '&&' <EqExpr>
//if (r.Parent.TableIndex() == Rules["Andexpr_Ampamp"])
//{
//    return;
//}
////<AndExpr> ::= <EqExpr>
//if (r.Parent.TableIndex() == Rules["Andexpr"])
//{
//    return;
//}
////<EqExpr> ::= <EqExpr> '==' <CompExpr>
//if (r.Parent.TableIndex() == Rules["Eqexpr_Eqeq"])
//{
//    return;
//}
////<EqExpr> ::= <EqExpr> '!=' <CompExpr>
//if (r.Parent.TableIndex() == Rules["Eqexpr_Exclameq"])
//{
//    return;
//}
////<EqExpr> ::= <CompExpr>
//if (r.Parent.TableIndex() == Rules["Eqexpr"])
//{
//    return;
//}
////<CompExpr> ::= <CompExpr> '<' <AddExpr>
//if (r.Parent.TableIndex() == Rules["Compexpr_Lt"])
//{
//    return;
//}
////<CompExpr> ::= <CompExpr> '>' <AddExpr>
//if (r.Parent.TableIndex() == Rules["Compexpr_Gt"])
//{
//    return;
//}
////<CompExpr> ::= <CompExpr> '<=' <AddExpr>
//if (r.Parent.TableIndex() == Rules["Compexpr_Lteq"])
//{
//    return;
//}
////<CompExpr> ::= <CompExpr> '>=' <AddExpr>
//if (r.Parent.TableIndex() == Rules["Compexpr_Gteq"])
//{
//    return;
//}
////<CompExpr> ::= <AddExpr>
//if (r.Parent.TableIndex() == Rules["Compexpr"])
//{
//    return;
//}
////<AddExpr> ::= <AddExpr> '+' <MultExpr>
//if (r.Parent.TableIndex() == Rules["Addexpr_Plus"])
//{
//    return;
//}
////<AddExpr> ::= <AddExpr> '-' <MultExpr>
//if (r.Parent.TableIndex() == Rules["Addexpr_Minus"])
//{
//    return;
//}
////<AddExpr> ::= <MultExpr>
//if (r.Parent.TableIndex() == Rules["Addexpr"])
//{
//    return;
//}
////<MultExpr> ::= <MultExpr> '*' <PowerExpr>
//if (r.Parent.TableIndex() == Rules["Multexpr_Times"])
//{
//    return;
//}
////<MultExpr> ::= <MultExpr> '/' <PowerExpr>
//if (r.Parent.TableIndex() == Rules["Multexpr_Div"])
//{
//    return;
//}
////<MultExpr> ::= <MultExpr> '%' <PowerExpr>
//if (r.Parent.TableIndex() == Rules["Multexpr_Percent"])
//{
//    return;
//}
////<MultExpr> ::= <PowerExpr>
//if (r.Parent.TableIndex() == Rules["Multexpr"])
//{
//    return;
//}
////<PowerExpr> ::= <PowerExpr> '^' <NegateExpr>
//if (r.Parent.TableIndex() == Rules["Powerexpr_Caret"])
//{
//    return;
//}
////<PowerExpr> ::= <NegateExpr>
//if (r.Parent.TableIndex() == Rules["Powerexpr"])
//{
//    return;
//}
////<NegateExpr> ::= '-' <Value>
//if (r.Parent.TableIndex() == Rules["Negateexpr_Minus"])
//{
//    return;
//}
////<NegateExpr> ::= '!' <Value>
//if (r.Parent.TableIndex() == Rules["Negateexpr_Exclam"])
//{
//    return;
//}
////<NegateExpr> ::= <Value>
//if (r.Parent.TableIndex() == Rules["Negateexpr"])
//{
//    return;
//}
////<Value> ::= Identifier
//if (r.Parent.TableIndex() == Rules["Value_Identifier"])
//{
//    return;
//}
////<Value> ::= <Literal>
//if (r.Parent.TableIndex() == Rules["Value"])
//{
//    return;
//}
////<Value> ::= '(' <Expression> ')'
//if (r.Parent.TableIndex() == Rules["Value_Lparen_Rparen"])
//{
//    return;
//}
////<Literal> ::= StringLit
//if (r.Parent.TableIndex() == Rules["Literal_Stringlit"])
//{
//    return;
//}
////<Literal> ::= IntLit
//if (r.Parent.TableIndex() == Rules["Literal_Intlit"])
//{
//    return;
//}
////<Literal> ::= RealLit
//if (r.Parent.TableIndex() == Rules["Literal_Reallit"])
//{
//    return;
//}
////<Literal> ::= BoolLit
//if (r.Parent.TableIndex() == Rules["Literal_Boollit"])
//{
//    return;
//}
////<Literal> ::= DateLit
//if (r.Parent.TableIndex() == Rules["Literal_Datelit"])
//{
//    return;
//}
#endregion


/*
 * Pre-build switch covering all the rules
 */
#region Rules Switch
//switch(r.Parent.TableIndex())
//{
//    case RuleIndices.Type_String:
//        // <Type> ::= string
//        break;
//
//    case RuleIndices.Type_Int:
//        // <Type> ::= int
//        break;
//
//    case RuleIndices.Type_Real:
//        // <Type> ::= real
//        break;
//
//    case RuleIndices.Type_Bool:
//        // <Type> ::= bool
//        break;
//
//    case RuleIndices.Type_Date:
//        // <Type> ::= date
//        break;
//
//    case RuleIndices.Forms:
//        // <Forms> ::= <Form> <Forms>
//        break;
//
//    case RuleIndices.Forms2:
//        // <Forms> ::= <Form>
//        break;
//
//    case RuleIndices.Form_Form_Identifier:
//        // <Form> ::= form Identifier <Block>
//        break;
//
//    case RuleIndices.Block_Lbrace_Rbrace:
//        // <Block> ::= '{' <Statements> '}'
//        break;
//
//    case RuleIndices.Statements:
//        // <Statements> ::= <Statement> <Statements>
//        break;
//
//    case RuleIndices.Statements2:
//        // <Statements> ::= <Statement>
//        break;
//
//    case RuleIndices.Statement_If_Lparen_Rparen:
//        // <Statement> ::= if '(' <Expression> ')' <Statement> <OptElse>
//        break;
//
//    case RuleIndices.Statement:
//        // <Statement> ::= <Block>
//        break;
//
//    case RuleIndices.Statement2:
//        // <Statement> ::= <Question>
//        break;
//
//    case RuleIndices.Statement_Gotonextform_Semi:
//        // <Statement> ::= gotoNextForm ';'
//        break;
//
//    case RuleIndices.Statement_Loop_Lparen_Rparen:
//        // <Statement> ::= loop '(' <Expression> ')' <Statement>
//        break;
//
//    case RuleIndices.Optelse_Else:
//        // <OptElse> ::= else <Statement>
//        break;
//
//    case RuleIndices.Optelse:
//        // <OptElse> ::= 
//        break;
//
//    case RuleIndices.Vardecl_Identifier_Colon:
//        // <VarDecl> ::= Identifier ':' <Type>
//        break;
//
//    case RuleIndices.Varassign_Identifier_Colon_Eq:
//        // <VarAssign> ::= Identifier ':' <Type> '=' <Expression>
//        break;
//
//    case RuleIndices.Question_Stringlit_Gtgt_Semi:
//        // <Question> ::= StringLit '>>' <VarDecl> ';'
//        break;
//
//    case RuleIndices.Question_Stringlit_Ltlt_Semi:
//        // <Question> ::= StringLit '<<' <VarAssign> ';'
//        break;
//
//    case RuleIndices.Question_Stringlit_Ltlt_Semi2:
//        // <Question> ::= StringLit '<<' <Expression> ';'
//        break;
//
//    case RuleIndices.Expression_Question_Colon:
//        // <Expression> ::= <OrExpr> '?' <OrExpr> ':' <Expression>
//        break;
//
//    case RuleIndices.Expression:
//        // <Expression> ::= <OrExpr>
//        break;
//
//    case RuleIndices.Orexpr_Pipepipe:
//        // <OrExpr> ::= <OrExpr> '||' <AndExpr>
//        break;
//
//    case RuleIndices.Orexpr:
//        // <OrExpr> ::= <AndExpr>
//        break;
//
//    case RuleIndices.Andexpr_Ampamp:
//        // <AndExpr> ::= <AndExpr> '&&' <EqExpr>
//        break;
//
//    case RuleIndices.Andexpr:
//        // <AndExpr> ::= <EqExpr>
//        break;
//
//    case RuleIndices.Eqexpr_Eqeq:
//        // <EqExpr> ::= <EqExpr> '==' <CompExpr>
//        break;
//
//    case RuleIndices.Eqexpr_Exclameq:
//        // <EqExpr> ::= <EqExpr> '!=' <CompExpr>
//        break;
//
//    case RuleIndices.Eqexpr:
//        // <EqExpr> ::= <CompExpr>
//        break;
//
//    case RuleIndices.Compexpr_Lt:
//        // <CompExpr> ::= <CompExpr> '<' <AddExpr>
//        break;
//
//    case RuleIndices.Compexpr_Gt:
//        // <CompExpr> ::= <CompExpr> '>' <AddExpr>
//        break;
//
//    case RuleIndices.Compexpr_Lteq:
//        // <CompExpr> ::= <CompExpr> '<=' <AddExpr>
//        break;
//
//    case RuleIndices.Compexpr_Gteq:
//        // <CompExpr> ::= <CompExpr> '>=' <AddExpr>
//        break;
//
//    case RuleIndices.Compexpr:
//        // <CompExpr> ::= <AddExpr>
//        break;
//
//    case RuleIndices.Addexpr_Plus:
//        // <AddExpr> ::= <AddExpr> '+' <MultExpr>
//        break;
//
//    case RuleIndices.Addexpr_Minus:
//        // <AddExpr> ::= <AddExpr> '-' <MultExpr>
//        break;
//
//    case RuleIndices.Addexpr:
//        // <AddExpr> ::= <MultExpr>
//        break;
//
//    case RuleIndices.Multexpr_Times:
//        // <MultExpr> ::= <MultExpr> '*' <PowerExpr>
//        break;
//
//    case RuleIndices.Multexpr_Div:
//        // <MultExpr> ::= <MultExpr> '/' <PowerExpr>
//        break;
//
//    case RuleIndices.Multexpr_Percent:
//        // <MultExpr> ::= <MultExpr> '%' <PowerExpr>
//        break;
//
//    case RuleIndices.Multexpr:
//        // <MultExpr> ::= <PowerExpr>
//        break;
//
//    case RuleIndices.Powerexpr_Caret:
//        // <PowerExpr> ::= <PowerExpr> '^' <NegateExpr>
//        break;
//
//    case RuleIndices.Powerexpr:
//        // <PowerExpr> ::= <NegateExpr>
//        break;
//
//    case RuleIndices.Negateexpr_Minus:
//        // <NegateExpr> ::= '-' <Value>
//        break;
//
//    case RuleIndices.Negateexpr_Exclam:
//        // <NegateExpr> ::= '!' <Value>
//        break;
//
//    case RuleIndices.Negateexpr:
//        // <NegateExpr> ::= <Value>
//        break;
//
//    case RuleIndices.Value_Identifier:
//        // <Value> ::= Identifier
//        break;
//
//    case RuleIndices.Value:
//        // <Value> ::= <Literal>
//        break;
//
//    case RuleIndices.Value_Lparen_Rparen:
//        // <Value> ::= '(' <Expression> ')'
//        break;
//
//    case RuleIndices.Literal_Stringlit:
//        // <Literal> ::= StringLit
//        break;
//
//    case RuleIndices.Literal_Intlit:
//        // <Literal> ::= IntLit
//        break;
//
//    case RuleIndices.Literal_Reallit:
//        // <Literal> ::= RealLit
//        break;
//
//    case RuleIndices.Literal_Boollit:
//        // <Literal> ::= BoolLit
//        break;
//
//    case RuleIndices.Literal_Datelit:
//        // <Literal> ::= DateLit
//        break;
//
//}
#endregion
