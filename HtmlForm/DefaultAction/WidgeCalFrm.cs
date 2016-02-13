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
    public partial class WidgeCalFrm : Form
    {
        private bool HasUpdate;
        public WidgeCalFrm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = tbName.Text.Trim();
            decimal widge = nudWidge.Value;

            if (CheckName(list, name) == false)
                return;

            GlobalVar.DBHelper.AddInsert("productwidge", "name,widge");
            DataTable tbl = new DataTable();
            tbl.Init("productwidge", "name,widge");
            DataRow row = tbl.NewRow();
            row.SetField<decimal>("widge",widge);
            row.SetField<string>("name", name);
            tbl.Rows.Add(row);

            int ar = GlobalVar.DBHelper.Update(tbl);
            if(ar != 1)
            {
                MessageBox.Show("添加失败");
                return;
            }

            ListViewItem item = new ListViewItem(name);
            item.SubItems.Add(widge.ToString());
            list.Items.Add(item);

            HasUpdate = true;
        }
        private bool CheckName(ListView lv, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入品名");
                return false;
            }
            foreach (ListViewItem item in lv.Items)
            {
                if (name.EndsWith(item.Text))
                {
                    MessageBox.Show("品名 " + name + " 已存在!");
                    return false;
                }
            }
            return true;
        }
        private void WidgeCalFrm_Load(object sender, EventArgs e)
        {
            list.Items.Clear();
            tbItem.AutoCompleteCustomSource.Clear();

            GlobalVar.DBHelper.AddSelect("productwidge", "name,widge");
            DataTable tbl = new DataTable("productwidge");
            GlobalVar.DBHelper.Fill(ref tbl);
            ListView.ListViewItemCollection c = new ListView.ListViewItemCollection(list);
            List<string> ac = new List<string>();
            foreach (DataRow r in tbl.Rows)
            {
                ListViewItem item = new ListViewItem(r.Field<string>("name"));
                item.SubItems.Add(r.Field<Int32>("widge").ToString());
                c.Add(item);

                ac.Add(r.Field<string>("name"));
            }
            tbItem.AutoCompleteCustomSource.AddRange(ac.ToArray());
            HasUpdate = false;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string name = tbItem.Text.Trim();
            decimal num = nudNum.Value;

            if (CheckName(lvCal, name) == false)
                return;

            GlobalVar.DBHelper.AddSelect("productwidge", "widge","name");
            GlobalVar.DBHelper.AddCustomParam("@name", name);
            DataTable tbl = new DataTable("productwidge");
            GlobalVar.DBHelper.Fill(ref tbl);

            if(tbl.Rows.Count < 1)
            {
                MessageBox.Show("品名不存在，请检查");
                return;
            }
            ListViewItem item = new ListViewItem(name);
            int widge = tbl.Rows[0].Field<Int32>("widge");
            item.SubItems.Add(widge.ToString());
            item.SubItems.Add(num.ToString());
            item.SubItems.Add((num * widge).ToString());
            lvCal.Items.Add(item);
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            double ret = 0;
            foreach (ListViewItem item in lvCal.Items)
            {
                int w = Convert.ToInt32(item.SubItems[3].Text);
                ret += w;
            }

            lbResult.Text = ret.ToString() + "KG 合" + (ret/1000).ToString() + "吨";
        }

        private void menu_Click(object sender, EventArgs e)
        {
            if(tbMain.SelectedTab == tp1)
            {
                if (lvCal.SelectedItems.Count == 0)
                    return;

                lvCal.Items.Remove(lvCal.SelectedItems[0]);
            }else
            {
                if (list.SelectedItems.Count == 0)
                    return;

                GlobalVar.DBHelper.AddDelete("productwidge", "name");
                string name = list.SelectedItems[0].Text;
                GlobalVar.DBHelper.AddCustomParam("@name", name);
                GlobalVar.DBHelper.ExcuteSQL("delete from productwidge where name=@name",true);

                list.Items.Remove(list.SelectedItems[0]);
                HasUpdate = true;
            }
           
        }

        private void tbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HasUpdate)
            {
                WidgeCalFrm_Load(null, null);
            }
        }
    }
}
