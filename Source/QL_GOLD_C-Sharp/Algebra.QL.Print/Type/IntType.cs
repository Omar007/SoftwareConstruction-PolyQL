using Algebra.QL.Print.Expr;
using Algebra.QL.Print.Expr.Literals;

namespace Algebra.QL.Print.Type
{
    public class IntType : BaseType
    {
        public override IPrintExpr DefaultValue { get { return new IntLiteral(0); } }

        public IntType()
        {

        }

        public override string ToString()
        {
            return "int";
        }
    }
}
