using System;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using HVH_Ken_Modules;

namespace HVH_Ken
{
    public partial class SrcEngEdt : CustomIconForm
    {
        private readonly char CHAR_COLLOMN_SEP;//列分隔符 
        private readonly string STR_LINE_SEP;//行分隔符 
        
        public SrcEngEdt()
        {
            InitializeComponent();
            CHAR_COLLOMN_SEP = (char)0;
            STR_LINE_SEP = "\r\n";
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (tbNam.Text.Trim().Equals(String.Empty) || tbUrl.Text.Trim().Equals(String.Empty))
                    return;
                string sKey = tbNam.Text.Trim();
                tbNam.Focus();
                foreach (ListViewItem item in lvRecords.Items)
                {
                    if (item.Text.Equals(sKey))
                    {
                        if (GlobalVar.Tip.Question("记录已存在，是否更新?") == DialogResult.Yes)
                        {       
                            item.SubItems[1].Text = tbUrl.Text;
                            btnSave.Enabled = true;
                        }
                        return;
                    }
                }
                //添加记录
                ListViewItem newItem = new ListViewItem();
                newItem.Text = tbNam.Text;
                newItem.SubItems.Add(tbUrl.Text);
                lvRecords.Items.Add(newItem);
                tbNam.Clear();
                tbUrl.Clear();
                btnSave.Enabled = true;
            }
        }

        private void lvRecords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
               // int iSelCot = lvRecords.SelectedItems.Count;
                foreach (ListViewItem item in lvRecords.SelectedItems)
                {
                    lvRecords.Items.Remove(item);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Instanse.SrcEngInfo == null)
                GlobalVar.Instanse.SrcEngInfo = new System.Collections.Specialized.OrderedDictionary();
            GlobalVar.Instanse.SrcEngInfo.Clear();

            foreach (ListViewItem item in lvRecords.Items)
            {
                //MessageBox.Show(item.Text);
                GlobalVar.Instanse.SrcEngInfo[item.Text] = item.SubItems[1].Text;
            }
            GlobalVar.Instanse.NeedToSaveCfg = true;
            btnSave.Enabled = false;
            GlobalVar.Tip.Info("保存成功");
            //this.Close();
        }

        private void SrcEngEdt_Load(object sender, EventArgs e)
        {
            toolTip.AutoPopDelay = 6000;
            tbFlag.Text = GlobalVar.Instanse.KEY_WORD_FLAG;
            tbFlag.BackColor = GlobalVar.Instanse.StyleColor;
            if (GlobalVar.Instanse.SrcEngInfo != null)
            {
                foreach (DictionaryEntry entry in GlobalVar.Instanse.SrcEngInfo)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = entry.Key.ToString();
                    item.SubItems.Add(entry.Value.ToString());
                    lvRecords.Items.Add(item);
                }
            }
        }

        private void lvRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRecords.SelectedItems.Count == 1)
            {
                tbNam.Text = lvRecords.SelectedItems[0].Text;
                tbUrl.Text = lvRecords.SelectedItems[0].SubItems[1].Text;
                btnDown.Enabled = true;
                btnUp.Enabled = true;
            }
            else
            {
                tbNam.Clear();
                tbUrl.Clear();
                btnDown.Enabled = false;
                btnUp.Enabled = false;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvRecords.SelectedItems[0];
            //不是最前一条
            if (item.Index != 0)
            {
                MoveSelectedItemTo(item, item.Index - 1);
                btnSave.Enabled = true;
            }
            lvRecords.Focus();
        }
        private void MoveSelectedItemTo(ListViewItem item,int iPos)
        {
            if (iPos >= 0 && iPos != item.Index)
            {
                ListViewItem itemNew = item.Clone() as ListViewItem;
                lvRecords.Items.Remove(item);
                lvRecords.Items.Insert(iPos, itemNew);
                itemNew.Selected = true;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvRecords.SelectedItems[0];
            //不是最后一条
            if (item.Index != (lvRecords.Items.Count - 1))
            {
                MoveSelectedItemTo(item, item.Index + 1);
                btnSave.Enabled = true;
            }
            lvRecords.Focus();
        }

        private void SrcEngEdt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSave.Enabled == true)
                if (GlobalVar.Tip.Question("是否关闭当前窗口？") == DialogResult.No)
                    e.Cancel = true;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Tip.Question("是否继续?") == DialogResult.No)
                return;
            lvRecords.Items.Clear();
            System.IO.FileStream stream;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.FileName = "";
                openFileDialog.Multiselect = false;
                string path = null;

                openFileDialog.FileName = "";
                //选择文件
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    stream = openFileDialog.OpenFile() as System.IO.FileStream;
                    using (stream)
                    {
                        try
                        {
                            path = stream.Name;
                            string sText = System.IO.File.ReadAllText(path, Encoding.UTF8);
                            string[] saSep = new string[] { STR_LINE_SEP };
                            string[] sLines = sText.Split(saSep, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string sLine in sLines)
                            {
                                string[] sCollomns = sLine.Split(CHAR_COLLOMN_SEP);
                                if (sCollomns.Length < 2)
                                    continue;
                                ListViewItem item = new ListViewItem();
                                item.Text = sCollomns[0];
                                item.SubItems.Add(sCollomns[1]);
                                lvRecords.Items.Add(item);
                            }
                            btnSave.Enabled = true;
                        }
                        catch (Exception)
                        {
                            GlobalVar.Tip.Error("导入失败,我已经很努力了");
                        }
                    }
                }
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            if (lvRecords.Items.Count == 0)
                return;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FileName = "";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sbBuf = new StringBuilder();
                    try
                    {
                        System.IO.FileStream stream = saveFileDialog.OpenFile() as System.IO.FileStream;
                        foreach (ListViewItem item in lvRecords.Items)
                        {
                            sbBuf.Append(item.Text);
                            sbBuf.Append(CHAR_COLLOMN_SEP);
                            sbBuf.Append(item.SubItems[1].Text);
                            sbBuf.Append(STR_LINE_SEP);
                        }
                        stream.Close();
                        System.IO.File.WriteAllText(stream.Name, sbBuf.ToString());
                        GlobalVar.Tip.Info("导出完成");
                    }
                    catch (Exception)
                    {
                        GlobalVar.Tip.Error("导出失败,我已经很努力了");
                    }
                }
            }
        }

    }
}
