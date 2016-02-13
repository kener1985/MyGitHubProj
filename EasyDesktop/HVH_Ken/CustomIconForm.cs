using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace HVH_Ken
{
    public partial class CustomIconForm : Form
    {
        //private Sunisoft.IrisSkin.SkinEngine _SkinEngine = new Sunisoft.IrisSkin.SkinEngine();
        public CustomIconForm()
        {
            InitializeComponent();
            HVH_Ken_Modules.GlobalVar.Instanse.StyleColor = this.BackColor;
        }
                
        private void CustomIconForm_Load(object sender, EventArgs e)
        {
            try
            {
                string sIconPath = HVH_Ken_Modules.GlobalVar.PROGRAM_ROOT_PATH + @"\default.ico";
                this.Icon = new System.Drawing.Icon(sIconPath);
                MaximizeBox = false;
                //SetBgColor();
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 设置窗体背景色，不能放到Load函数中，否则设计窗口会报错，可能是BUG.
        /// </summary>
        //private void SetBgColor()
        //{
        //    try
        //    {
        //        this.BackColor = HVH_Ken_Modules.GlobalVar.Instanse.StyleColor;
        //    }
        //    catch (System.Exception)
        //    { }
        //}
    }
}
