﻿using System.Windows;
using System.Windows.Controls;
using Algebra.QL.Core.Stmnt;
using Algebra.QL.Form.Helpers;

namespace Algebra.QL.Form.Stmnt
{
    public class StatementsStmnt : CompStmnt<IFormStmnt>, IFormStmnt
    {
        public StatementsStmnt(IFormStmnt a, IFormStmnt b)
            : base(a, b)
        {

        }

        public FrameworkElement BuildForm(VarEnvironment env)
        {
            StackPanel sp = new StackPanel();
            sp.Children.Add(Statement1.BuildForm(env));
            sp.Children.Add(Statement2.BuildForm(env));

            return sp;
        }
    }
}
