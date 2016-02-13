using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HVH_Ken
{
    public partial class LetterKeyTextBox : TextBox
    {
        public LetterKeyTextBox()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            char l = (char)e.KeyCode;
            if (!char.IsLetterOrDigit(l))
                return;
            Text = l.ToString();
            
            e.Handled = true;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = true;
        }
       
    }
}
