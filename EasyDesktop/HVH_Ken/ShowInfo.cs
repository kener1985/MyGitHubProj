using System;
using System.Drawing;
using System.Windows.Forms;
using HVH_Ken_Modules;
using System.Data;
namespace HVH_Ken
{
    public partial class ShowInfoFrm : CustomIconForm
    {
        private int m_Width;
        private ItemPosite itemPos;
        public ShowInfoFrm()
        {
            InitializeComponent();
            //lvRec.Items[0].canvas.Brush.Color = Color.Yellow;
            m_Width = this.Width;
            //设置显示位置
            Rectangle rect = System.Windows.Forms.SystemInformation.WorkingArea;
            this.Left = rect.Width - this.Width;
            this.Top = rect.Height - this.Height;
            itemPos = new ItemPosite(this.lvRec);
            itemPos.FieldToSearch = new string[] { "shortcut", "_title" };
        }

        private void ShowInfoFrm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = GlobalVar.Instanse.StyleColor;
            LoadDatas();
        }

        private void lvRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                LoadDatas();
            else if(e.KeyCode == Keys.Enter)
            {
                if (lvRec.SelectedItems.Count == 1)
                { 
                    ListViewItem item = lvRec.SelectedItems[0];
                    try
                    {
                        ProgFinder.FindToRun(item.SubItems[1].Text,false);
                    }
                    catch (Exception)
                    { }
                }
            }
        }
        private void LoadDatas()
        {
            int iscrWid = 15;//滚动栏宽度
            lvRec.Items.Clear();

            long count = (long)GlobalVar.Helper.ExcuteForUnique<object>("select count(*) from programs");
            if (count >= 10)
            {
                //还未曾改变过
                if (m_Width == this.Width)
                {
                    this.Width += iscrWid;//空出垂直滚动条空间
                    //窗体向左移动垂直滚动条宽度距离
                    this.Left -= iscrWid;
                }
            }
            else
            {
                if (m_Width != this.Width)
                {
                    //恢复原状
                    this.Width -= iscrWid;
                    this.Left += iscrWid;
                }
            }
            bool bChgCol = true;//是否另外着色
            DataTable table = new DataTable("programs");
            GlobalVar.Helper.AddSelect("programs", "id,shortcut,path,title,is_auto_run,has_taskitem");
            GlobalVar.Helper.Fill(ref table);
            foreach (DataRow row in table.Rows)
            {
                ConfigData data = new ConfigData();
                GlobalVar.Helper.Row2DbObj(row, data);
                ListViewItem item = new ListViewItem(data.Title);
                item.SubItems.Add(data.Shortcut);
                item.ToolTipText = data.Path;
                item.Tag = data;
                lvRec.Items.Add(item);
                if (data.HasTaskitem || data.IsAutRun)
                {
                    item.BackColor = Color.Pink;
                }
                else if (bChgCol)
                    item.BackColor = HVH_Ken_Modules.GlobalVar.Instanse.StyleColor;
                bChgCol = !bChgCol;
            }
            lbSum.Text = lvRec.Items.Count.ToString();
        }

        private void lvRec_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;
            
            if(lvRec.SelectedItems.Count == 0)
                return;
            ListViewItem selItem = lvRec.SelectedItems[0];
            try
            {
                ProgFinder.FindToRun(selItem.SubItems[1].Text,false);
            }
            catch (Exception ex)
            {
                GlobalVar.Tip.Error(ex.Message);
            }
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            itemPos.FindNext(tbCdt.Text.Trim());
        }

        private void tbCdt_TextChanged(object sender, EventArgs e)
        {
            itemPos.Reset();
        }

        private void ShowInfoFrm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.tbCdt.Focus();
                this.tbCdt.SelectAll();
            }
        }

        private void ShowInfoFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void tbCdt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        btnNext_Click(this,e);
                        break;
                    }
            }
        }
    }
}
