using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.IO;
using HVH_Ken_Modules;
using System.Data;
//using System.Data.DataSetExtensions;
namespace HVH_Ken
{
    public partial class RecOprFrm : CustomIconForm
    {
        private GlobalVar.ActionType m_action;
        private string m_fields;
        private ItemFinder m_finder;
        private Timer timer;
        private DataTable _table;
        private bool m_bChange;
        public RecOprFrm()
        {
            Init();
            m_action = GlobalVar.ActionType.Add;
        }
        public RecOprFrm(GlobalVar.ActionType act)
        {
            Init();
            m_action = act;
        }
        private void Init()
        {
            timer = new Timer();
            InitializeComponent();
            btnSave.Enabled = false;
            btnDel.Enabled = false;
            btnMod.Enabled = false;
            m_fields = "_title,_path,_shortcut,_hints";
            _table = new DataTable("programs");
            m_bChange = false;
        }
        public bool HasChange
        {
            get { return m_bChange; }
        }
        private void OprFrm_Load(object sender, EventArgs e)
        {
            GlobalVar.Helper.AddSelect("programs", "id,shortcut,path,title,is_auto_run,has_taskitem,hints");
            GlobalVar.Helper.AddInsert("programs", "shortcut,path,title,is_auto_run,has_taskitem,hints");
            GlobalVar.Helper.AddDelete("programs", "id");
            GlobalVar.Helper.AddUpdate("programs", "shortcut,path,title,is_auto_run,has_taskitem,hints", "id");
            GlobalVar.Helper.Fill(ref _table);
            panel3.BackColor = BackColor;
            
            //按钮图标
            string basePath = System.Windows.Forms.Application.StartupPath;
            Size size = new Size(16, 16);

            if (sender != null)//为null说明为人为重新加载
            {
                string[] imageNames = { "AddProg.png", "AddDir.png", "Task.png",
                                          "Add.png","Modify.png","Change.png",
                                          "Save.png","Delete.png" };
                Button[] buttons = {btnAddProg,btnDir,btnTask,btnAdd,btnMod,btnChgMod,btnSave,btnDel };
                for (int i = 0; i < buttons.Length; i++)
                {
                    try
                    {
                        Image image = Image.FromFile(basePath + @"\images\" + imageNames[i]);
                        buttons[i].Image = new Bitmap(image, size);
                        image.Dispose();
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            m_lvRecords.Items.Clear();
            lbTaskSum.Text = "0";
            lbSum.Text = "0";
            lbAutoRun.Text = "0";
            ClearData();
            if (m_action == GlobalVar.ActionType.Add)
            {
                this.Text = "新增";
                btnAdd.Enabled = true;
            }
            else
            {
                this.Text = "编辑";
                //如果是编辑,进行数据加载
                //System.Collections.Generic.Dictionary<string, ConfigData> buf = GlobalVar.Instanse.DataBuffer;
                RefreshList();
                btnAdd.Enabled = false;
            }
            //增加列表项时会发出itemcheck事件,所以这里应该重新初始化
            btnSave.Enabled = false;
            //lbSum.Text = m_lvRecords.Items.Count.ToString();
            btnChgMod.Select();

        }

        /// <summary>
        /// 根据缓存数据更新列表.
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void RefreshList()
        {
            int iAutoRun = 0;
            int iTask = 0;
            m_lvRecords.SuspendLayout();
            
            foreach (DataRow row in _table.Rows)
            {
                ConfigData data = new ConfigData();;
                GlobalVar.Helper.Row2DbObj(row, data);
                data.Row = row;
                ListViewItem item = new ListViewItem(data.Title);
                item.SubItems.Add(data.Path);
                item.SubItems.Add(data.Shortcut);
                item.SubItems.Add(data.Hints.ToString());
                //item.SubItems.Add(data.IsAutRun);
                item.ToolTipText = data.Path;
                

                if (data.HasTaskitem)
                {
                    iTask++;
                }else if (data.IsAutRun == true)
                {
                    iAutoRun++;
                }
                
                item.Tag = data;
                m_lvRecords.Items.Add(item);
            }
            lbAutoRun.Text = iAutoRun.ToString();
            lbTaskSum.Text = iTask.ToString();
            lbSum.Text = m_lvRecords.Items.Count.ToString();
            
            m_lvRecords.ResumeLayout();
        }
        private ConfigData GetValueFromItem(ListViewItem item)
        {
            ConfigData data = new ConfigData();
            Type type = typeof(ConfigData);
            string[] fs = m_fields.Split(',');
            int i = 0;
            foreach (string f in fs)
            {
                FieldInfo fi;
                fi = type.GetField(f, BindingFlags.NonPublic | BindingFlags.Instance);
                if (fi == null)
                    continue;
                fi.SetValue(data, item.SubItems[i++].Text);
            }
            ConfigData newinfo = item.Tag as ConfigData;
            if (newinfo != null)
            {
                //data.TaskItem = newinfo.TaskItem;
                data.IsAutRun = newinfo.IsAutRun;
            }
            return data;
        }
        private void SetValueToItem(ConfigData data, ListViewItem item)
        {
            Type type = typeof(ConfigData);
            string[] fs = m_fields.Split(',');
            int i = 0;
            string value;
            item.SubItems.Clear();

            foreach (string f in fs)
            {
                FieldInfo fi;
                fi = type.GetField(f, BindingFlags.NonPublic | BindingFlags.Instance);
                if (fi == null)
                    continue;
                value = fi.GetValue(data).ToString();
                if (i != 0)
                    item.SubItems.Add(value);
                else
                    item.Text = value;
                i++;
            }

            ((ConfigData)item.Tag).IsAutRun = data.IsAutRun;
            if (data.HasTaskitem)
            {
                  lbTaskSum.Text = (Convert.ToInt32(lbTaskSum.Text) + 1).ToString();
                //((ConfigData)item.Tag).TaskItem = data.TaskItem;
            }
            else if (data.IsAutRun == true)
            {
                lbAutoRun.Text = (Convert.ToInt32(lbAutoRun.Text) + 1).ToString();
            }
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            if (m_lvRecords.SelectedItems.Count > 0 &&
                GlobalVar.Tip.Question("真的要删除吗?") == DialogResult.Yes)
            {
                btnSave.Enabled = true;
                foreach (ListViewItem item in m_lvRecords.SelectedItems)
                {
                    m_lvRecords.Items.Remove(item);
                    ((ConfigData)item.Tag).Row.Delete();
                }
                if (m_action == GlobalVar.ActionType.Add && m_lvRecords.Items.Count == 0)
                {
                    btnSave.Enabled = false;
                }
                lbSum.Text = m_lvRecords.Items.Count.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //ListViewItem item1 = m_lvRecords.Items[0];
            //item1.BackColor = Color.AliceBlue;

            ////System.Collections.Generic.Dictionary<string, ConfigData> buf = GlobalVar.Instanse.DataBuffer;
            //ConfigData data = null;
            //if (m_action == GlobalVar.ActionType.Modify)
            //{
            //    //buf.Clear();
            //}
            //DataTable dt = new DataTable("programs");
            
            //foreach (ListViewItem item in m_lvRecords.Items)
            //{
            //    data = GetValueFromItem(item);
            //    GlobalVar.Helper.FillDataFromObj(dt, data,DataRowState.Modified);
            //}

            //btnSave.Enabled = false;
            ////ClearData();
            //if(m_action == GlobalVar.ActionType.Add)
            //    m_lvRecords.Items.Clear();
            GlobalVar.Helper.Update(_table);
            
            GlobalVar.Tip.Info("操作成功");
            btnSave.Enabled = false;
            m_bChange = true;
            //GlobalVar.Instanse.NeedToSaveData = true;
        }

        private void lvRecords_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnDel.Enabled = m_lvRecords.SelectedItems.Count != 0;
            
            if (m_lvRecords.SelectedItems.Count == 1)
            {
                btnMod.Enabled = true;
                btnTask.Enabled = true;
                FillData(m_lvRecords.SelectedItems[0]);
            }
            else
            {
                btnMod.Enabled = false;
                btnTask.Enabled = false;
                ClearData();
                cbAutRun.Checked = false;
            }


        }
        private void FillData(ListViewItem item)
        {

            tbTitle.Text = item.SubItems[0].Text;
            tbPath.Text = item.SubItems[1].Text;
            tbSct.Text = item.SubItems[2].Text;
            cbAutRun.Checked = ((ConfigData)item.Tag).IsAutRun;
            //bool tmp;
            //if (Boolean.TryParse(item.SubItems[3].Text, out tmp))
            //    cbAutRun.Checked = tmp;
        }

        /// <summary>
        /// 修改记录.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnMod_Click(object sender, EventArgs e)
        {
            //列表有记录被选中时，修改键才有效，但在修改模式下按下，需要判断
            if (m_lvRecords.SelectedItems.Count == 0)
                return;
            ConfigData data = new ConfigData();
            
            ColNewDat(data);
            //检查快捷名
            if (CheckShortcut(data.Shortcut, GlobalVar.ActionType.Modify)
                )
            {
                ListViewItem item = m_lvRecords.SelectedItems[0];
                ((ConfigData)item.Tag).Merge(data);
                SetValueToItem(data, item);

                btnSave.Enabled = true;
            }
        }
        private void AddBatFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSave.Enabled == true)
            {
                if (GlobalVar.Tip.Question("你有未保存数据,是否退出?") == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (m_finder != null)
                m_finder.Close();
            
        }

        /// <summary>
        /// 添加记录.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbPath.Text.Trim().Equals(""))
            {
                toolTip.Show("路径不能为空", tbPath);
                return;
            }
            ConfigData data = new ConfigData();
            ColNewDat(data);

            //检查快捷名
            if (CheckShortcut(data.Shortcut, GlobalVar.ActionType.Add))
            {
                AddRecord(data);
                ClearData();
                btnSave.Enabled = true;
                lbSum.Text = m_lvRecords.Items.Count.ToString();
            }
        }

        private void AddRecord(ConfigData data)
        {
            ListViewItem item = new ListViewItem();

            item.Tag = data;
            m_lvRecords.Items.Add(item);
            data.Row = GlobalVar.Helper.FillDataFromObj(_table, data);

            SetValueToItem(data, item);
        }

        /// </summary>
        /// <param name="data">The data.</param>
        private void ColNewDat(ConfigData data)
        {
            data.Title = tbTitle.Text;
            data.Path = tbPath.Text;
            data.Row = _table.NewRow();
            //去掉左右和中间的空格,并且所有的命令都为小写格式
            data.Shortcut = tbSct.Text.Replace(" ", null).ToLower();
            data.IsAutRun = cbAutRun.Checked;
            //einfo.IsAutoRun = data.IsAutRun;
            if (m_lvRecords.SelectedItems.Count == 1)
            {
                ConfigData einfo = m_lvRecords.SelectedItems[0].Tag as ConfigData;
                //data.TaskItem = einfo.TaskItem;
            }
        }

        /// <summary>
        /// 清空输入栏.
        /// </summary>
        private void ClearData()
        {
            tbPath.Clear();
            tbTitle.Clear();
            tbSct.Clear();
            cbAutRun.Checked = false;
            
        }
        private void btnAddProg_Click(object sender, EventArgs e)
        {
            FileStream stream;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.FileName = "";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        stream = openFileDialog.OpenFile() as FileStream;
                        using (stream)
                        {
                            tbPath.Text = stream.Name;

                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalVar.Tip.Error(ex.Message);
                    }
                    //tbPath.ReadOnly = true;
                }
            }
        }
        private void ShowToolTip(string sMsg, Control ctrl, int iItv)
        {
            if (iItv == 0)
                return;
            toolTip.ShowAlways = true;
            toolTip.AutoPopDelay = iItv;
            toolTip.AutomaticDelay = 0;
            toolTip.Show(sMsg, ctrl);
            timer.Enabled = true;
            timer.Interval = iItv;
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            toolTip.Hide(tbPath);
            toolTip.RemoveAll();
            timer.Enabled = false;
            timer.Stop();
        }


        #region 数据校验
        /// <summary>
        /// 查看快捷名称是否存在,如果为修改，检查列表，如果为增加，多检查缓冲区.
        /// </summary>
        /// <param name="sct">快捷名.</param>
        /// <param name="type">当前操作类型.</param>
        /// <returns></returns>
        private bool CheckShortcut(string sct, GlobalVar.ActionType type)
        {
            bool bRst = true;

            if (sct.Trim().Equals(String.Empty))
            {
                toolTip.SetToolTip(tbSct, "命令不能为空");
                tbSct.Focus();
                tbSct.SelectAll();
                return false;
            }
            if (CheckFirstForbiden(sct) == false)
            {
                toolTip.SetToolTip(tbSct, "命令: [ " + sct + " ]不能以 " + sct[0] + " 开头");
                tbSct.Focus();
                tbSct.SelectAll();
                return false;
            }
            //判断快捷名的有效性
            //if (type == GlobalVar.ActionType.Modify)
            //{
            //    foreach (ListViewItem item in m_lvRecords.Items)
            //    {
            //        if (sct.Equals(item.SubItems[2].Text))
            //        {
            //            if (type == GlobalVar.ActionType.Modify)
            //                bRst = item == m_lvRecords.SelectedItems[0];
            //            else
            //                bRst = false;
            //            break;
            //        }
            //    }
            //}else if (bRst == true && m_action == GlobalVar.ActionType.Add)
            //{
            //    //if (sct != null &&
            //    //    !sct.Equals(String.Empty) &&
            //    //    GlobalVar.Instanse.DataBuffer.ContainsKey(sct))
            //    //    bRst = false;
            //    GlobalVar.Helper.AddCustomParam("shortcut",sct);
            //    long count = (long)GlobalVar.Helper.ExcuteForUnique<object>("select count(*) from programs where shortcut=@shortcut");

            //    bRst = count == 0;
            //}


           
            
            //if (bRst == false)
            //{
            //    toolTip.SetToolTip(tbSct, "命令: [ " + sct + " ] 已存在!");
            //    tbSct.Focus();
            //    tbSct.SelectAll();
            //}
            //return bRst;
            return true;
        }

        private bool CheckFirstForbiden(string sct)
        {
            if (sct[0] == '$' || sct[0] == '{')
                return false;
            return true;
        }
        #endregion

        private void lvRecords_KeyDown(object sender, KeyEventArgs e)
        {
            //响应Ctrl + F 事件
            if (e.KeyData == (Keys.F | Keys.Control))
            {
                //打开查找窗口
                if (m_finder == null)
                {
                    m_finder = new ItemFinder(this.m_lvRecords);
                }
                m_lvRecords.MultiSelect = false;
                m_finder.Show();
                m_finder.Activate();
            }
            else if (e.KeyData == Keys.Delete)
            {
                btnDel_Click(sender, e);
            }
            else if (e.KeyData == Keys.Enter && m_finder != null)
            {
                if(m_finder.Visible)
                {
                    //m_finder.Focus();
                    m_finder.btnNext_Click(null, e);
                }
            }
        }

        private void tbPath_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(tbPath.Text, tbPath, tbPath.Text.Length * 300);
        }

        private void tbPath_MouseLeave(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Stop();
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.Description = "请在下面列表中选定目标目录";
                string sPath = tbPath.Text.Trim();
                if (!sPath.Equals(string.Empty))
                    folderBrowserDialog.SelectedPath = Path.GetDirectoryName(sPath);
                
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    tbPath.Text = folderBrowserDialog.SelectedPath;
                    folderBrowserDialog.Reset();
                }
            }
        }

        private void tbCommon_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (this.m_action == GlobalVar.ActionType.Add)
                            btnAdd_Click(sender, e);
                        else
                            btnMod_Click(sender, e);
                        break;
                    }
            }
        }

        private void btnChgMod_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled == true)
                if (GlobalVar.Tip.Question("有数据被修改，是否继续？") == DialogResult.No)
                    return;
            if (m_action == GlobalVar.ActionType.Add)
                m_action = GlobalVar.ActionType.Modify;
            else
                m_action = GlobalVar.ActionType.Add;
            OprFrm_Load(null, e);
        }
        private class CollumnSorter : IComparer
        {
            private int _Column;
            SortOrder _Order;
            public CollumnSorter()
            {
                _Order = SortOrder.Ascending;
                _Column = 0;
            }
            public CollumnSorter(int col, SortOrder order)
            {
                this._Column = col;
                this._Order = order;
            }
            public int Compare(object x, object y)
            {
                ListViewItem itemX = x as ListViewItem;
                ListViewItem itemY = y as ListViewItem;
                ListViewItem item1 = _Order == SortOrder.Ascending ? itemX : itemY;
                ListViewItem item2 = _Order == SortOrder.Ascending ? itemY : itemX;
                ListView lv = itemX.ListView;
                if (lv.Columns[_Column].Tag != null)
                {
                    //整数型比较
                    decimal i1 = Convert.ToDecimal(item1.SubItems[_Column].Text);
                    decimal i2 = Convert.ToDecimal(item2.SubItems[_Column].Text);

                    return Decimal.Compare(i1, i2);

                }
                return String.Compare(item1.SubItems[_Column].Text, item2.SubItems[_Column].Text);
            }
        }

        private void m_lvRecords_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (m_lvRecords.Sorting == SortOrder.Ascending)
            {
                this.m_lvRecords.ListViewItemSorter = new CollumnSorter(e.Column, SortOrder.Descending);
                this.m_lvRecords.Sorting = SortOrder.Descending;
            }
            else
            {
                this.m_lvRecords.ListViewItemSorter = new CollumnSorter(e.Column, SortOrder.Ascending);
                this.m_lvRecords.Sorting = SortOrder.Ascending;
            }
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            if (m_lvRecords.SelectedItems.Count != 1)
                return;
            
            ListViewItem item = m_lvRecords.SelectedItems[0];
            //string sCmd = item.SubItems[2].Text;
            DataRow row = ((ConfigData)item.Tag).Row;

            TaskSetting ts = new TaskSetting(row.Field<long>("id"),row.Field<string>("shortcut"));

            if (ts.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}
