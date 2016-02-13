namespace DefaultAction
{
    partial class PopupFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbMain = new BaseLib.FormBrowser();
            this.SuspendLayout();
            // 
            // cbMain
            // 
            this.cbMain.AllowWebBrowserDrop = false;
            this.cbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMain.Location = new System.Drawing.Point(0, 0);
            this.cbMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.cbMain.Name = "cbMain";
            this.cbMain.PageId = "635589333944582148";
            this.cbMain.Size = new System.Drawing.Size(698, 451);
            this.cbMain.TabIndex = 0;
            // 
            // PopupFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(698, 451);
            this.Controls.Add(this.cbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PopupFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PopupFrm_FormClosing);
            this.Load += new System.EventHandler(this.PopupFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BaseLib.FormBrowser cbMain;
    }
}