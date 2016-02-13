using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HVH_Ken_Modules
{
    public partial class SelectFrm : Form
    {
        private List<ConfigData> _List = new List<ConfigData>();
        public SelectFrm()
        {
            InitializeComponent();
        }
        public SelectFrm(List<ConfigData> list)
        {
            InitializeComponent();
            int top = 0;
            _List = list;
            foreach (ConfigData data in list)
            {
                CheckBox cb = new CheckBox();
                cb.Text = data.Title;
                cb.AutoSize = true;
                cb.Left = 10;
                cb.Top = top;
                cb.Tag = data;
                pnMain.Controls.Add(cb);
                top += cb.Height;

            }
        }

        public void UpdateSelectedHint()
        {
            DataTable table = new DataTable();
            foreach (Control ctrl in pnMain.Controls)
            {
                CheckBox cb = ctrl as CheckBox;
                if(cb == null)
                    continue;

                ConfigData data = cb.Tag as ConfigData;
                if(cb.Checked == false)
                {
                    _List.Remove(data);
                    continue;
                }
                int hint = data.Row.Field<int>("hints");
                data.Row.SetField<int>("hints", ++hint);
            }
            
        }

        private void SelectFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                DialogResult = DialogResult.Cancel;
            else if (e.KeyData == Keys.Enter)
                DialogResult = DialogResult.OK;
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private static extern int ReleaseCapture();
        private const int WM_SysCommand = 0x0112;
        private const int SC_MOVE = 0xF012;
        private void SelectFrm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;
            ReleaseCapture();
            SendMessage(this.Handle, WM_SysCommand, SC_MOVE, 0);
        }
    }
}
