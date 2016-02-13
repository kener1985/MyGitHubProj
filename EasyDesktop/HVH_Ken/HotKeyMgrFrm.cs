using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HVH_Ken_Modules;
using System.IO;
using UtilHelper.Database;

namespace HVH_Ken
{
    public partial class HotKeyMgrFrm : HVH_Ken.CustomIconForm
    {
        DataTable _tbl;
        public static IntPtr HotKeyHandle;
        private static Dictionary<int, string> _HotKeys = new Dictionary<int, string>();
        private static int _StartId = 104;
        public HotKeyMgrFrm()
        {
            InitializeComponent();
        }
        public static Dictionary<int,string> HotKeys
        {
            get { return _HotKeys; }
        }
        public static void InitHotKeys()
        {
            _HotKeys.Clear();
            DataTable tbl = new DataTable("hotkeys");
            GlobalVar.Helper.AddSelect("hotkeys", "key_code,cmd,modifier_code");
            GlobalVar.Helper.Fill(ref tbl);
            int idStart = _StartId;
            foreach (DataRow row in tbl.Rows)
            {       
                uint mc = Convert.ToUInt32(row["modifier_code"]);
                uint kc = Convert.ToUInt32(row["key_code"]);

                if (RunFrm.RegisterHotKey(HotKeyHandle, idStart, mc, kc))
                {
                    _HotKeys[idStart] = row.Field<string>("cmd");
                    idStart++;
                }
                
            }
        }

        private void HotKeyMgrFrm_Load(object sender, EventArgs e)
        {
            
            _tbl = new DataTable("hotkeys");
            GlobalVar.Helper.AddSelect("hotkeys", "id,key_code,cmd,modifier_code,display");
            GlobalVar.Helper.AddDelete("hotkeys", "id");
            GlobalVar.Helper.AddInsert("hotkeys", "id,key_code,cmd,modifier_code,display");
            GlobalVar.Helper.AddUpdate("hotkeys", "key_code,cmd,modifier_code,display", "id");
            GlobalVar.Helper.Fill(ref _tbl);

            foreach (DataRow row in _tbl.Rows)
            {
                ListViewItem item = new ListViewItem(row.Field<string>("display"));
                item.SubItems.Add(row.Field<string>("cmd"));
                item.Tag = row;
                lvHotkeys.Items.Add(item);
            }
            //_tbl.GetChanges(DataRowState.Added);
            string basePath = System.Windows.Forms.Application.StartupPath;
            string[] imageNames = { "AddProg.png", "AddDir.png"};
            Button[] buttons = { btnAddProg, btnDir};
            Size size = new Size(16, 16);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(tbCmd.Text.Trim() == String.Empty ||
                tbKey.Text == string.Empty)
            {
                return;
            }

            int modicierCode = tbKey.ModifierCode;
            string dis = tbKey.Text;
            ListViewItem item;
            DataRow row;

            if (cbWin.Checked)
            {
                dis = "Win + " + dis;
                modicierCode |= 8;
            }

            if (lvHotkeys.SelectedItems.Count == 0)//新增
            {
                item = new ListViewItem(dis);
                item.SubItems.Add(tbCmd.Text);
                row = _tbl.NewRow();
                _tbl.Rows.Add(row);
                item.Tag = row;
                lvHotkeys.Items.Add(item);
            }else
            {
                item = lvHotkeys.SelectedItems[0];
                row = item.Tag as DataRow;
                item.SubItems[0].Text = dis;
                item.SubItems[1].Text = tbCmd.Text;
            }

            row["key_code"] = tbKey.KeyCode;
            row["cmd"] = tbCmd.Text;
            row["modifier_code"] = modicierCode;
            row["display"] = dis;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AbstractIdGengerator gen = new IncreatableIdGengerator("hotkeys");
            GlobalVar.Helper.Update(_tbl,gen);
            
            foreach (KeyValuePair<int, string> p in HotKeys)
            {
                RunFrm.UnregisterHotKey(HotKeyHandle, p.Key);
            }
            int iStart = _StartId;
            HotKeys.Clear();
            foreach (DataRow row in _tbl.Rows)
            {
                uint mc = Convert.ToUInt32(row["modifier_code"]);
                uint kc = Convert.ToUInt32(row["key_code"]);
                if (RunFrm.RegisterHotKey(HotKeyHandle, iStart, mc, kc))
                {
                    HotKeys.Add(iStart, row.Field<string>("cmd"));
                    iStart++;
                }
               
            }
        }

        private void lvHotkeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHotkeys.SelectedItems.Count != 1)
            {
                cbWin.Checked = false;
                tbKey.Clear();
                tbCmd.Clear();
                return;
            }

            DataRow row = lvHotkeys.SelectedItems[0].Tag as DataRow;
            string dis = row.Field<string>("display");
            cbWin.Checked = dis.Contains("Win + ");
            tbKey.Text = dis.Replace("Win + ", String.Empty);
            tbCmd.Text = row.Field<string>("cmd");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            int iStart = _StartId;
            StringBuilder sz = new StringBuilder();
            //先卸掉程序已经注册的热键
            foreach (KeyValuePair<int,string> p in HotKeys)
            {
                RunFrm.UnregisterHotKey(HotKeyHandle, p.Key);
            }
            sz.AppendLine("以下热键存在冲突:");
            foreach(DataRow row in _tbl.Rows)
            {
                if(row.RowState == DataRowState.Deleted)
                    continue;
                uint mc = Convert.ToUInt32(row["modifier_code"]);
                uint kc = Convert.ToUInt32(row["key_code"]);

                if (!RunFrm.RegisterHotKey(HotKeyHandle, iStart, mc, kc))
                {
                    sz.AppendLine(row.Field<string>("display"));
                }
                else
                {
                    RunFrm.UnregisterHotKey(HotKeyHandle, iStart);
                }
                
                iStart++;
            }
            //重新注册
            InitHotKeys();
            GlobalVar.Tip.Info(sz.ToString());
        }

        private void lvHotkeys_KeyDown(object sender, KeyEventArgs e)
        {
            if(lvHotkeys.SelectedItems.Count == 1 && e.KeyData == Keys.Delete && 
                GlobalVar.Tip.Question("真的要删除吗?") == DialogResult.Yes)
            {
                ListViewItem item = lvHotkeys.SelectedItems[0];
                DataRow row = item.Tag as DataRow;
                lvHotkeys.Items.Remove(item);
                row.Delete();
                
            }
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
                            tbCmd.Text = stream.Name;

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

        private void btnDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.Description = "请在下面列表中选定目标目录";
                string sPath = tbCmd.Text.Trim();
                if (!sPath.Equals(string.Empty))
                    folderBrowserDialog.SelectedPath = Path.GetDirectoryName(sPath);

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    tbCmd.Text = folderBrowserDialog.SelectedPath;
                    folderBrowserDialog.Reset();
                }
            }
        }
    }
}
