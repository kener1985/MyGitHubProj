using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseLib;
using UtilHelper.Http;
using Microsoft.SqlServer;
using BaseLib.Extends;
using UtilHelper.Tip;
namespace DefaultAction
{
    public partial class StoreChangeFrm : Form
    {
        private readonly int _OriNum;
        public StoreChangeFrm(int num)
        {
            InitializeComponent();
            //nudStore.Value = num;
            _OriNum = num;
        }

        public int Value
        {
            get
            {
                int val = 0;
                Int32.TryParse(tbNum.Text,out val);
                return val;
            }
        }
        public char OprType
        {
            get
            {
                
                if(rdModify.Checked)
                    return 'M';//修改
                else if (rdOut.Checked)
                    return 'O';//出货
                else if(rdIn.Checked)
                    return 'I';//入货
                
                return '*';//*表示没有选择类型
            }
        }
        public string Mark
        {
            get { return tbMark.Text.Trim(); }
        }
        public string Customer
        {
            get { return this.tbCustomer.Text.Trim(); }
        }
        private void radioBtn_Click(object sender, EventArgs e)
        {
            toolTip.Hide(rdOut);
            if (rdModify.Checked == true)
                tbNum.Text = _OriNum.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            MB mb = new MB();
            if (this.OprType == '*')
            {
                toolTip.Show("请选择操作类型", rdOut);
                //mb.Stop("温馨提示", "请选择操作类型\t");
                return;
            }

            if (this.Value <= 0)
            {
                toolTip.Show("数量无效\t",tbNum);
                tbNum.Focus();
                tbNum.SelectAll();
                return;
            }
            if (this.OprType != 'M' && String.IsNullOrEmpty(this.tbCustomer.Text.Trim()))
            {
                toolTip.Show("请输入客户名称\t",tbCustomer);
                tbCustomer.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.tbMark.Text.Trim()))
            {
                toolTip.Show("请输入备注\t",tbMark);
                tbMark.Focus();
                return;
            }
          
            if (OprType == 'O' && _OriNum < this.Value)
            {
                toolTip.Show("库存不足,请重新输入!\t",tbNum);
                tbNum.Focus();
                tbNum.SelectAll();
                return;
            }

            if (OprType == 'M' && _OriNum == Value)
            {
                mb.Stop("温馨提示", "库存无变化");
                return;
            }
            
            DialogResult = DialogResult.OK;
        }
    }
}
