﻿using QL_Grammar.AST.Expr;
using QL_Grammar.AST.Value;

namespace QL_Grammar.Eval.Expr
{
    public class NotEqualExpr : DoubleExprNode<IEvalExpr>, IEvalExpr
	{
		public NotEqualExpr(IEvalExpr l, IEvalExpr r)
            : base(l, r)
		{

		}

        public IValue Eval()
		{
			return new BoolValue(!Expr1.Eval().Value.Equals(Expr2.Eval().Value));
		}
	}
}
