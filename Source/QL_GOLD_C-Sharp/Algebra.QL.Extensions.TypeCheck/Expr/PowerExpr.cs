using System;
using Algebra.QL.TypeCheck.Environment;
using Algebra.QL.TypeCheck.Expr;
using Algebra.QL.TypeCheck.Type;

namespace Algebra.QL.Extensions.TypeCheck.Expr
{
    public class PowerExpr : BinaryExpr, ITypeCheckExpr
    {
        private static readonly ITypeCheckType ExpressionUpperBound = new RealType();

        public PowerExpr(ITypeCheckExpr l, ITypeCheckExpr r)
            : base(l, r)
        {

        }

        public override ITypeCheckType TypeCheck(ITypeEnvironment env)
        {
            ITypeCheckType a = Expr1.TypeCheck(env);
            ITypeCheckType b = Expr2.TypeCheck(env);

            if (!a.CompatibleWith(ExpressionUpperBound) || !a.CompatibleWith(b))
            {
                env.ReportError(String.Format("Power not possible. Incompatible types: '{0}', '{1}'. Only numeric types are supported.",
                    a, b), SourceStartPosition, SourceEndPosition);

                return ExpressionUpperBound;
            }

            return a.GetLeastUpperBound(b);
        }
    }
}
