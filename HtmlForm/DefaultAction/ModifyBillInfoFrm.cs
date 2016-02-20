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
    public partial class ModifyBillInfoFrm : Form
    {
        private string billid;
        public ModifyBillInfoFrm(string billid)
        {
            this.billid = billid;
            InitializeComponent();
        }

        private void ModifyBillInfoFrm_Load(object sender, EventArgs e)
        {
            EasyUITable etb = new EasyUITable();
            DataTable tbl = new DataTable("bills");
            string fields = "id,payer,seqnbr,purunit,operator,mobile,pid,pagecode,type";

            //MessageBox.Show(qs);
            GlobalVar.DBHelper.AddSelectWithLimit("bills", fields, "id=@id");
            GlobalVar.DBHelper.AddCustomParam("@id", billid);
            GlobalVar.DBHelper.Fill(ref tbl);
            if (tbl.Rows.Count == 0)
            {
                MessageBox.Show("查无此订单");
                return;
            }
            DataRow row = tbl.Rows[0];
            tbPurunit.Text = row.Field<string>("purunit");
            tbPayer.Text = row.Field<string>("payer");
            tbMobile.Text = row.Field<string>("mobile");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            string fields = "id,purunit,payer,mobile";
            tbl.Init("bills", fields);
            DataRow r = tbl.NewRow();
            r.SetField<string>("id", this.billid);
            r.SetField<string>("purunit", tbPurunit.Text.Trim());
            r.SetField<string>("payer", tbPayer.Text.Trim());
            r.SetField<string>("mobile", tbMobile.Text.Trim());
            tbl.Rows.Add(r);
            r.AcceptChanges();
            r.SetModified();
            GlobalVar.DBHelper.AddUpdate("bills", fields, "id");
            if (GlobalVar.DBHelper.Update(tbl) != 1)
            {
                MessageBox.Show("更新失败");
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
