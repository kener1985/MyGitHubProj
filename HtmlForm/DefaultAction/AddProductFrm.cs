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
    public partial class AddProductFrm : Form
    {
        public AddProductFrm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            string fields = "productid,name,innerid,price,cost,colornum,position,storenum,packagenum,size,opr";
            tbl.Init("products", fields);
            DataRow row = tbl.NewRow();
            if (IntiRow(row) == false)
                return;

            tbl.Rows.Add(row);
            GlobalVar.DBHelper.AddInsert(tbl.TableName, fields);
            if(GlobalVar.DBHelper.Update(tbl) == 1)
            {
                MessageBox.Show("添加成功");
                DialogResult = DialogResult.OK;
            }else
            {
                MessageBox.Show("添加失败,请检查日志!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IntiRow(DataRow row)
        {
            string pid = tbId.Text.Trim();
            string innerid = tbInnerId.Text.Trim();
            string size = tbSize.Text.Trim();
            string colornum = tbColorNum.Text.Trim();
            string name = tbName.Text.Trim();
            string pos = tbPosition.Text.Trim();

            decimal price = nudPrice.Value;
            decimal cost = nudCost.Value;
            decimal store = nudStore.Value;
            decimal pkgnum = nudPkgNum.Value;

            if (String.IsNullOrEmpty(innerid))
            {
                MessageBox.Show("编号不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            row.SetField<string>("productid", pid);
            row.SetField<string>("innerid", innerid);
            row.SetField<string>("size", size);
            row.SetField<string>("colornum", colornum);
            row.SetField<string>("name", name);
            row.SetField<string>("position", pos);
            row.SetField<decimal>("price", price);
            row.SetField<decimal>("cost", cost);
            row.SetField<decimal>("storenum", store);
            row.SetField<decimal>("packagenum", pkgnum);
            row.SetField<string>("opr", GlobalVar.LogInfo.Name);
            return true;
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            btnAdd_Click(null, null);
        }


    }
}
