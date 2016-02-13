using System;
using System.Data;
using System.Windows.Forms;
using BaseLib;

namespace HtmlForm
{
    public partial class LogonFrm : Form
    {
        //public delegate void CloseDelegate();
        //public CloseDelegate CloseDelegateHandler;
        public LogonFrm()
        {
            InitializeComponent();
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            string name = tbName.Text.Trim();
            string psw = tbPsw.Text;
            GlobalVar.DBHelper.AddSelect("members", "name,password,role,sex,code");
            //GlobalVar.DBHelper.AddCustomParam("@name", name);
            DataTable tbl = new DataTable("members");
            GlobalVar.DBHelper.Fill(ref tbl);
            if (GlobalVar.Users.Count == 0)
            {
                //初始化用户信息
                foreach (DataRow row in tbl.Rows)
                {
                    LogInfoClass user = new LogInfoClass();
                    user.Name = row.Field<string>("name");
                    user.WorkCode = row.Field<string>("code");
                    user.Password = row.Field<string>("password");
                    user.Role = row.Field<string>("role");
                    GlobalVar.Users.Add(user.WorkCode, user);
                }
            }
            if (checkAuth(name, psw) == false)
            {
                MessageBox.Show("登录信息错误，请检查!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (GlobalVar.MainFrame.Visible == false)
                GlobalVar.MainFrame.Show();
            tbName.Clear();
            tbPsw.Clear();
            tbName.Select();
            MainForm mf = GlobalVar.MainFrame as MainForm;
            mf.FreshPage();
        }
        private bool checkAuth(string name, string psw)
        {
            System.Collections.Generic.Dictionary<string, LogInfoClass> users = GlobalVar.Users;
            foreach (string key in users.Keys)
            {
                LogInfoClass info = users[key];
                if (name.Equals(info.Name) && psw.Equals(info.Password))
                {
                    GlobalVar.LogInfo = info;

                    return true;
                }
            }

            return false;
        }
        private void LogonFrm_Load(object sender, EventArgs e)
        {
            GlobalVar.MainFrame = new MainForm();
            GlobalVar.MainFrame.Hide();
            MainForm mf = GlobalVar.MainFrame as MainForm;
            mf.LogFrm = this;

        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            btnLogon_Click(null, null);
        }

    }
}
