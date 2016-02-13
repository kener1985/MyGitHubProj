using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseLib;

namespace DefaultAction
{
    public partial class PopupFrm : Form
    {
        StrDictionary _QueryParams = new StrDictionary();

        public PopupFrm(StrDictionary sd)
        {
            _QueryParams = sd;
            InitializeComponent();
        }

        private void PopupFrm_Load(object sender, EventArgs e)
        {
            string file = GlobalVar.AppPath + @"views\busintf\" + _QueryParams["file"] + "?"+ _QueryParams["data"];
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
            
            if (_QueryParams.ContainsKey("width"))
                this.Width = Convert.ToInt32(_QueryParams["width"]);

            if (_QueryParams.ContainsKey("height"))
                this.Height = Convert.ToInt32(_QueryParams["height"]);
            this.Top = (ScreenArea.Height - this.Height) / 2;
            this.Left = (ScreenArea.Width - this.Width) / 2;
            cbMain.Navigate(file);
        }

        private void PopupFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cbMain.Dispose();
            
        }



    }
}
