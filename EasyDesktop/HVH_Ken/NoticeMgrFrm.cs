using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HVH_Ken_Modules;
using UtilHelper.Tip;
using System.Globalization;
using UtilHelper.Database;

namespace HVH_Ken
{
    public partial class NoticeMgrFrm : HVH_Ken.CustomIconForm
    {
        private DataTable _table;
        public NoticeMgrFrm()
        {
            _table = new DataTable("notices");
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string info = tbInfo.Text.Trim();
            if (String.IsNullOrEmpty(info))
                return;

            if (lvNotices.SelectedItems.Count == 0)//新增
            {

                ListViewItem item = new ListViewItem(info);
                string cdt = GetCondition();
                double dur = Convert.ToDouble(udDur.Value);
                int times = Convert.ToInt32(udTimes.Value);
                Notice n = new Notice(info, cdt, dur, times,cdt.Contains(" "));
                item.Tag = GlobalVar.Helper.FillDataFromObj(_table, n);
                lvNotices.Items.Add(item);
            }
            else
            {
                ListViewItem item = lvNotices.SelectedItems[0];
                item.Text = tbInfo.Text;
                DataRow row = item.Tag as DataRow;
                Notice n = new Notice();
                //GlobalVar.Helper.Row2DbObj(row, n);
                //n.Init();
                string sCdt = GetCondition();
                row.SetField<string>("condition",sCdt);
                row.SetField<bool>("is_temp", sCdt.Contains(" "));
                row.SetField<string>("info", tbInfo.Text);
                row.SetField<double>("dur_times", Convert.ToDouble(udDur.Value));
                row.SetField<int>("times", Convert.ToInt32(udTimes.Value));
            }

            btnOk.Enabled = true;
        }
        private string GetCondition()
        {
            StringBuilder sz = new StringBuilder();
            string sHour = udHour.Value.ToString();
            string sMnt = udMnt.Value.ToString();
            string sSec = udSec.Value.ToString();
            string sTimeSep = CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator;

            if(cbOneTime.Checked)
            {
                string sDateSep = CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator;
                DateTime dt = dtpDate.Value;
                sz.Append(dt.Year).Append(sDateSep).
                    Append(dt.Month).Append(sDateSep).
                    Append(dt.Day).Append(" ");
            }
            sz.Append(sHour).Append(sTimeSep).Append(sMnt).Append(sTimeSep).Append(sSec);

            return sz.ToString();
        }
        private void lvNotices_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;

            if (lv == null || lv.SelectedItems.Count == 0)
            {
                Clear();
                return;
            }
            ListViewItem item = lv.SelectedItems[0];
            tbInfo.Text = item.Text;
            DataRow row = item.Tag as DataRow;
            
            if (row == null)
                return;
            Notice n = new Notice();
            GlobalVar.Helper.Row2DbObj(row, n);

            DateTime dt;
            if (DateTime.TryParse(n.Condition, out dt))
            {
                udHour.Value = dt.Hour;
                udMnt.Value = dt.Minute;
                udSec.Value = dt.Second;
                dtpDate.Value = dt;
                cbOneTime.Checked = n.IsTemp;
                udDur.Value = Convert.ToDecimal(n.DurMin);
                udTimes.Value = n.Times;
            }
            cbOneTime_CheckedChanged(null, null);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            AbstractIdGengerator gen = new IncreatableIdGengerator("notices");
            GlobalVar.Helper.Update(_table,gen);
            lock (GlobalVar.Instanse.Trigers)
            {
                List<ITrigerable> list = GlobalVar.Instanse.Trigers;
                //去掉原来的通知
                for (int i = 0; i < list.Count; i++)
                {
                    ITrigerable t = list[i];
                    if (t is Notice)
                    {
                        list.Remove(t);
                        i--;
                    }
                }
                //加上新的通知
                foreach (ListViewItem item in lvNotices.Items)
                {
                    DataRow row = item.Tag as DataRow;
                    Notice n = new Notice();
                    GlobalVar.Helper.Row2DbObj(row, n);
                    GlobalVar.Instanse.Trigers.Add(n);
                }
            }
            //GlobalVar.Instanse.NeedToSaveNotice = true;
            btn.Enabled = false;

        }

        private void NoticeMgrFrm_Load(object sender, EventArgs e)
        {
            Init();

            foreach (DataRow row in _table.Rows)
            {
                Notice n = new Notice();
                GlobalVar.Helper.Row2DbObj(row, n);

                ListViewItem i = new ListViewItem(n.Info);
               // i.Text = ;
                i.SubItems.Add(n.Condition);
                i.Tag = row;
                lvNotices.Items.Add(i);
            }
            cbStop.Checked = GlobalVar.Instanse.StopNotice;
        }

        private void Init()
        {
            udTimes.Maximum = Int32.MaxValue;
            string sFld = "id,condition,info,dur_times,times,is_temp";
            GlobalVar.Helper.AddSelect("notices", sFld);
            GlobalVar.Helper.AddInsert("notices", sFld);
            GlobalVar.Helper.AddUpdate("notices", sFld, "id");
            GlobalVar.Helper.AddDelete("notices", "id");
            GlobalVar.Helper.Fill(ref _table);
            //删除掉过期临时通知
            bool bNeedUpdate = false;
            foreach (DataRow row in _table.Rows)
            {
                DateTime dt;
                if (row.Field<bool>("is_temp") &&
                    DateTime.TryParse(row.Field<string>("condition"), out dt) &&
                    dt < DateTime.Now)
                {
                    row.Delete();
                    bNeedUpdate = true;
                }
            }
            if (bNeedUpdate)
                GlobalVar.Helper.Update(_table);
            Clear();
        }
        private void Clear()
        {
            DateTime dtNow = DateTime.Now;
            udHour.Value = dtNow.Hour;
            udMnt.Value = dtNow.Minute;
            udSec.Value = dtNow.Second;
            udTimes.Value = 1;
            udDur.Value = (decimal)0.1;
            dtpDate.Value = DateTime.Now;
            cbOneTime.Checked = false;
            tbInfo.Clear();
        }
        private void lvNotices_KeyDown(object sender, KeyEventArgs e)
        {
            MB mb = new MB("通知");
            if (e.KeyData == Keys.Delete)
            {
                if (lvNotices.SelectedItems.Count == 0)
                    return;

                if (mb.Question("是否要删除?") == DialogResult.No)
                    return;

                ListViewItem item = lvNotices.SelectedItems[0];
                DataRow row = item.Tag as DataRow;
                lvNotices.Items.Remove(item);
                row.Delete();
                btnOk.Enabled = true;
            }
        }

        private void cbStop_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.Instanse.StopNotice = cbStop.Checked;
        }

        private void cbOneTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpDate.Enabled = cbOneTime.Checked;
        }

    }
}
