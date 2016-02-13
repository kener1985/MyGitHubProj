using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HVH_Ken_Modules;
namespace HVH_Ken
{
    public partial class TaskSetting : CustomIconForm
    {
        private TaskItem m_taskitem;
        private long m_id;
        private string m_cmd;
        //public TaskItem TaskObj
        //{
        //    get { return this.m_taskitem; }
        //}
        public TaskSetting(long  itemid,string cmd)
        {
            InitializeComponent();
            m_taskitem = null;
            m_id = itemid;
            m_cmd = cmd;
        }
       
        private void TaskSetting_Load(object sender, EventArgs e)
        {
            tpDay.BackColor = GlobalVar.Instanse.StyleColor;
            tpWeek.BackColor = GlobalVar.Instanse.StyleColor;
            tpMonth.BackColor = GlobalVar.Instanse.StyleColor;
            tpYear.BackColor = GlobalVar.Instanse.StyleColor;
            GlobalVar.Helper.AddCustomParam("fid", m_id);
            GlobalVar.Helper.AddSelect("taskitem", "fid,condition,type,shortcut","fid");
            DataTable table = new DataTable("taskitem");
            GlobalVar.Helper.Fill(ref table);
            if(table.Rows.Count != 0)
            {
                m_taskitem = new TaskItem();
                GlobalVar.Helper.Row2DbObj(table.Rows[0], m_taskitem);
            }
            //this.Update();
            if (m_taskitem == null)
            {
                SetDefaultValue();
                //cbClose.Checked = true;
            }
            else
            {
                ParseCustomValue();
            }
            
        }

        private void ParseCustomValue()
        {
            string sDateTime = "000000";
            if (m_taskitem.CircleType == TaskType.DAY)
            {
                sDateTime = m_taskitem.Condition;
            }
            else if (m_taskitem.CircleType == TaskType.WEEK)
            { 
                int iPos = m_taskitem.Condition.IndexOf('|');
                int iWeek = 0;
                CheckBox[] cb = { cbSun, cbMon, cbTus, cbWed, cbThu, cbFri, cbSat };
                for (; iWeek < cb.Length; iWeek++)
                {
                    int iTmp = m_taskitem.Condition.IndexOf("W" + iWeek.ToString());
                    if (iTmp != -1)
                        cb[iWeek].Checked = true;
                }
                    sDateTime = m_taskitem.Condition.Substring(iPos + 1, 6);
                tcSet.SelectedTab = tpWeek;
            }
            udHour.Value = Convert.ToDecimal(sDateTime.Substring(0, 2));
            udMnt.Value = Convert.ToDecimal(sDateTime.Substring(2, 2));
            udSec.Value = Convert.ToDecimal(sDateTime.Substring(4, 2));
        }
        private void SetDefaultValue()
        {
             DateTime dtNow = DateTime.Now;
             
            if (tcSet.SelectedTab == tpDay)
            {
                udHour.Value = dtNow.Hour;
                udMnt.Value = dtNow.Minute;
                udSec.Value = dtNow.Second;
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            string sFld = "shortcut,fid,type,condition";
            GlobalVar.Helper.AddCustomParam("id", m_id);
            DataRowState state = DataRowState.Modified;
            if (cbClose.Checked == true)
            {
                m_taskitem = null;
                this.DialogResult = DialogResult.OK;
                GlobalVar.Helper.ExcuteSQL("delete from taskitem where fid=@id",false);
                GlobalVar.Helper.ExcuteSQL("update programs set has_taskitem=0 where id=@id");
                return;
            }
            if (m_taskitem == null)
            {
                GlobalVar.Helper.AddInsert("taskitem",sFld);
                state = DataRowState.Added;
                m_taskitem = new TaskItem();
            }else
            {
                GlobalVar.Helper.AddUpdate("taskitem", sFld,"fid");
            }
            m_taskitem.Id = m_id;
            m_taskitem.Name = m_cmd;
            if (tcSet.SelectedTab == tpDay)
                AddDayTask();
            else if (tcSet.SelectedTab == tpWeek)
                AddWeekTask();

            
            DataTable table = new DataTable("taskitem");
            GlobalVar.Helper.FillDataFromObj(table, m_taskitem, state);
            GlobalVar.Helper.Update(table);
            GlobalVar.Helper.ExcuteSQL("update programs set has_taskitem=1 where id=@id");
            //MessageBox.Show(m_sCondition);
        }
        private void AddDayTask()
        {
            StringBuilder sb = new StringBuilder(6);
            sb.Append(UDToString(udHour, udMnt, udSec));
            m_taskitem.Condition = sb.ToString();
            m_taskitem.CircleType = TaskType.DAY;
            this.DialogResult = DialogResult.OK;
        }
        private void AddWeekTask()
        {
            CheckBox[] cbs = { cbSun,cbMon, cbTus, cbWed, cbThu, cbFri, cbSat };
            StringBuilder sb = new StringBuilder();
            //MessageBox.Show(cbSun.Tag.ToString());
            bool bIsWeekDaySel = false;//是否选择了星期
            //增加星期
            foreach (CheckBox cb in cbs)
            {
                if (cb.Checked == true)
                {
                    bIsWeekDaySel = true;
                    sb.Append("W");//W表示周期为星期
                    sb.Append(cb.Tag.ToString());//星期数，从星期日到星期六到分别为：0 - 6
                }
            }
            if (bIsWeekDaySel == false)
            {
                MessageBox.Show("至少选择一个星期");
                return;
            }
            //增加时间
            sb.Append("|");//星期和时间的分割字符
            sb.Append(UDToString(udHour, udMnt, udSec));

            m_taskitem.Condition = sb.ToString();
            m_taskitem.CircleType = TaskType.WEEK;
            this.DialogResult = DialogResult.OK;
        }
       
        private string FillZeroAtFront(string sSrc, int iTotalLen)
        {
            int iToAdd = iTotalLen - sSrc.Length;
            if (iToAdd <= 0)
                return sSrc;
            StringBuilder sb = new StringBuilder(iTotalLen);
            for (int i = 0; i < iToAdd; i++)
            {
                sb.Append('0');
            }
            sb.Append(sSrc);
            return sb.ToString();
        }

        private void cbClose_CheckedChanged(object sender, EventArgs e)
        {
            if (cbClose.Checked == true)
            {
                tcSet.Enabled = false;
                m_taskitem = null;
            }
            else
            {
                tcSet.Enabled = true;
            }
        }
        private string UDToString(NumericUpDown udHour,NumericUpDown udMin,NumericUpDown udSec)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(FillZeroAtFront(udHour.Value.ToString(), 2));
            sb.Append(FillZeroAtFront(udMin.Value.ToString(), 2));
            sb.Append(FillZeroAtFront(udSec.Value.ToString(), 2));

            return sb.ToString();
        }

        private void btnCvs_Click(object sender, EventArgs e)
        {
            CheckBox[] cbs = { cbSun, cbMon, cbTus, cbWed, cbThu, cbFri, cbSat };
            foreach (CheckBox cb in cbs)
                cb.Checked = !cb.Checked;
        }
       
    }
}
