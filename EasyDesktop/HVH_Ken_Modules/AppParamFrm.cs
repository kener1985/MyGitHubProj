using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HVH_Ken_Modules
{
    public partial class AppParamFrm : Form
    {
        public AppParamFrm(CommanderData cmdObj)
        {
            InitializeComponent();
            GlobalVar.Instanse.Init();
            
            SetInfo(cmdObj);
            
        }
        private void SetInfo(CommanderData obj)
        {
            groupBox.Text = "命令:" + obj.Cmd;
        }
        private void tbParam_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车、Esc、Ctrl + 回车
            if (e.KeyChar == (char)13 || e.KeyChar == (char)27 || e.KeyChar == (char)10)
                e.Handled = true;
            base.OnKeyPress(e);
        }
        public string Param
        {
            get { return tbParam.Text.Trim(); }
        }

        private void tbParam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
