using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HVH_Ken
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

                //string sStartType = "NORMAL";
                //if(args.Length != 0)
                //    sStartType = args[0];
                Application.Run(new RunFrm());
            }
            catch (Exception)
            { }
        }

       static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //捕获全局异常
            System.Windows.Forms.MessageBox.Show("Error : " + e.Exception.Message, 
                                                 "Program Error!", 
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
        }
    }
}
