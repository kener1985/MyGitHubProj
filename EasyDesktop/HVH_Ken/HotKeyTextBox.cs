using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
namespace HVH_Ken
{
    public partial class HotKeyTextBox : TextBox
    {
        private int _ModifierCode = 0;
        private int _KeyCode = 0;
        public HotKeyTextBox()
        {
            InitializeComponent();
        }

        public HotKeyTextBox(IContainer container)
        {
            InitializeComponent();
        }
        public int ModifierCode
        {
            get { return _ModifierCode; }
        }
        public int KeyCode
        {
            get { return _KeyCode; }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            StringBuilder sHotKey = new StringBuilder(16);
            _ModifierCode = 0;
            if (e.Control)
            {
                _ModifierCode |= 2;
                sHotKey.Append("Ctrl + ");
            }
            if (e.Alt)
            {
                _ModifierCode |= 1;
                sHotKey.Append("Alt + ");
            }
            if (e.Shift)
            {
                _ModifierCode |= 4;
                sHotKey.Append("Shift + ");
            }
            //win键和其它键组合会失效
            //if ((e.KeyData & Keys.LWin) == Keys.LWin || (e.KeyData & Keys.RWin) == Keys.RWin)
            //    sHotKey.Append("Win + ");

            if (IsValidKey(e.KeyValue))
            {
                string s = e.KeyCode.ToString();
                if(s.Length == 2 && s.StartsWith("D"))
                    sHotKey.Append(s[1]);
                else
                    sHotKey.Append(s);

                _KeyCode = e.KeyValue;
            }
            
            Text = sHotKey.ToString();
            e.Handled = true;
            base.OnKeyDown(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (Text.EndsWith("+ "))
            {
                _ModifierCode = 0;
                _KeyCode = 0;
                Text = String.Empty;
            }
                
            base.OnKeyUp(e);
        }
        private bool IsValidKey(int code)
        {
            bool bRtn = false;
            bRtn |= (0 <= code && code <= 9) || (48 <= code && code <= 57);//数字键
            bRtn |= (96 <= code && code <= 111);//小键盘
            bRtn |= (112 <= code && code <= 123);//F1 - F12
            bRtn |= (65 <= code && code <= 90);//字母键
            bRtn |= (186 <= code && code <= 222);//标点符号键
            bRtn |= (37 <= code && code <= 46);//方向键及删除键等
            bRtn |= (code == 27 || code == 32);

            return bRtn;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = true;
            base.OnKeyPress(e);
        }
       
    }
}
