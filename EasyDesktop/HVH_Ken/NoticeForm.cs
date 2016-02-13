using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using HVH_Ken_Modules;
namespace HVH_Ken
{
    public partial class NoticeForm : Form
    {
        public bool CanClose;
        public NoticeForm()
        {
            InitializeComponent();
            CanClose = true;
            rtbInfo.SelectionAlignment = HorizontalAlignment.Center;
            Rectangle rect = SystemInformation.WorkingArea;
            this.Left = rect.Width;
            this.Top = rect.Height - this.Height;
        }
        public string NoticeInfo
        {
            get { return rtbInfo.Text; }
            set { rtbInfo.Text = value; }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private static extern int ReleaseCapture();

        private delegate bool InvokeHandler(int flag);
        private bool DoInvoke(int flag)
        {
            if (flag == 0)
            {
                Rectangle rect = SystemInformation.WorkingArea;
                this.Left -= 2;

                if ((rect.Width - this.Left) >= this.Width)
                {
                    return false;
                }
            }
            else
            {
                Close();
            }
            return true;

        }
        private void ShowNoticeThread(object state)
        {
            NoticeForm frm = state as NoticeForm;
            if (frm == null)
                return;
            //GlobalVar.Instanse.LogInfo("通知窗口上浮线程启动");
            bool bIvkRst = true;
            InvokeHandler ih = new InvokeHandler(frm.DoInvoke);
            try
            {
                while (bIvkRst)
                {
                    bIvkRst = (bool)frm.Invoke(ih,0);                  
                    Thread.Sleep(1);
                }

                //如果用户没有操作过窗口,则 5 秒后自动关闭;如果用户操作过,则用户自行关闭
                Thread.Sleep(5000);
            }
            catch (System.Exception ex)
            {
                System.IO.File.WriteAllText(@"C:\threaderror.txt", "ShowNoticeThread" + ex.Message + "\r\n");
            }
            finally
            {
                if (frm.CanClose == true)
                {
                    frm.Invoke(ih, 1);
                }
            }
            //GlobalVar.Instanse.LogInfo("通知窗口下浮线程结束");
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            //GlobalVar.Instanse.LogInfo("关闭通知窗口");
            this.Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;

            ReleaseCapture();
            // WM_SysCommand  0x0112
            // SC_MOVE = 0xF012
            SendMessage(this.Handle.ToInt32(), 0x0112, 0xF012, 0);
            CanClose = false;
        }

        private void NoticeForm_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(ShowNoticeThread, this);
        }
        public void Notice()
        {
            
            //this.Activate();
            //GlobalVar.Instanse.LogInfo("开启通知窗口上浮线程");
            
        }
        private void rtbInfo_MouseEnter(object sender, EventArgs e)
        {
            CanClose = false;
        }

        private void cbStop_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.Instanse.StopNotice = cbStop.Checked;
        }

        private void NoticeForm_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
