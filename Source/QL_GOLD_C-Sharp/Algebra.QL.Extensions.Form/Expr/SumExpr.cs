using System.Linq;
using Algebra.QL.Form.Environment;
using Algebra.QL.Form.Expr;
using Algebra.QL.Form.Type;

namespace Algebra.QL.Extensions.Form.Expr
{
    public class SumExpr : Eval.Expr.SumExpr, IFormExpr
    {
        public SumExpr(string name)
            : base(name)
        {
            
        }

        public IFormType BuildForm(ITypeEnvironment env)
        {
            return ((Environment.TypeEnvironment)env).GetRange(Name).First();
        }
    }
}
