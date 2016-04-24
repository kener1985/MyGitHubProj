using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseLib;
using BaseLib.Extends;
namespace DefaultAction
{
    public partial class PaybackFrm : Form
    {
        private readonly string _mainseqnbr;
        public PaybackFrm(string seqnbr)
        {
            _mainseqnbr = seqnbr;
            InitializeComponent();
        }

        private void btnPayback_Click(object sender, EventArgs e)
        {
            if(nudAmt.Value == 0)
            {
                MessageBox.Show("金额不能0");
                return;
            }
            if(tbMark.Text.Trim() == "")
            {
                MessageBox.Show("备注不能为空");
                return;
            }
            //MessageBox.Show(_id);
            GlobalVar.DBHelper.AddInsert("debtdetail", "main_seqnbr,billseq,opr,type,amount,mark,seqnbr");
            DataTable tbl = new DataTable();
            tbl.Init("debtdetail", "main_seqnbr,billseq,opr,type,amount,mark,seqnbr");
            DataRow r = tbl.NewRow();
            r.SetField<long>("seqnbr", DateTime.Now.Ticks);
            r.SetField<string>("opr", GlobalVar.LogInfo.WorkCode);
            r.SetField<string>("type", "B");//还款
            r.SetField<string>("billseq", "");
            r.SetField<string>("main_seqnbr", _mainseqnbr);
            r.SetField<string>("mark", tbMark.Text.Trim());
            r.SetField<string>("amount", nudAmt.Value.ToString());
            tbl.Rows.Add(r);
            GlobalVar.DBHelper.Update(tbl);
            DialogResult = DialogResult.OK;
        }
    }
}
