﻿using System;
using Algebra.QL.Core.Stmnt;
using Algebra.QL.TypeCheck.Expr;
using Algebra.QL.TypeCheck.Helpers;
using Algebra.QL.TypeCheck.Type;

namespace Algebra.QL.TypeCheck.Stmnt
{
	public class IfElseStmnt : IfElseStmnt<ITypeCheckExpr, ITypeCheckStmnt>, ITypeCheckStmnt
	{
		private static readonly ITypeCheckType ExpressionType = new BoolType();
        public Tuple<int, int> SourceStartPosition { get; set; }
        public Tuple<int, int> SourceEndPosition { get; set; }

		public IfElseStmnt(ITypeCheckExpr check, ITypeCheckStmnt ifTrue, ITypeCheckStmnt ifFalse)
            : base(check, ifTrue, ifFalse)
		{

		}

        public void TypeCheck(TypeEnvironment env, ErrorReporter errRep)
        {
            if (!CheckExpression.TypeCheck(env, errRep).CompatibleWith(ExpressionType))
            {
                errRep.ReportError(this, "Unable to evaluate 'if/else'. Expression must be of type bool!");
            }

            IfTrueBody.TypeCheck(env, errRep);
			IfFalseBody.TypeCheck(env, errRep);
        }
    }
}
