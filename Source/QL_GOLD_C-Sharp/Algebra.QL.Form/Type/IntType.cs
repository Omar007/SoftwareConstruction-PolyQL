using System;
using System.Windows;
using Algebra.QL.Eval.Value;
using Algebra.QL.Form.Expr;
using Algebra.QL.Form.Expr.Literals;
using Xceed.Wpf.Toolkit;

namespace Algebra.QL.Form.Type
{
    public class IntType : BaseType
    {
        public override IFormExpr DefaultValue { get { return new IntLiteral(0); } }
        public override IFormType SuperType { get { return new RealType(); } }

        public IntType()
        {

        }

        protected override FrameworkElement BuildElement(ValueContainer value)
        {
            IntegerUpDown iud = new IntegerUpDown() { Width = 200 };
            iud.ValueChanged += (s, e) =>
            {
                value.Value = iud.Value;
            };

            Action onValueChanged = () =>
            {
                iud.Value = Convert.ToInt32(value.Value);
            };
            value.ValueChanged += onValueChanged;
            onValueChanged();

            return iud;
        }
    }
}
