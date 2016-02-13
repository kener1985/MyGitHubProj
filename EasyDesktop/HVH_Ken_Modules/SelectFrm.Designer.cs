namespace HVH_Ken_Modules
{
    partial class SelectFrm
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
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.pnMain = new System.Windows.Forms.Panel();
            this.gbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMain
            // 
            this.gbMain.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbMain.Controls.Add(this.pnMain);
            this.gbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMain.Location = new System.Drawing.Point(3, 3);
            this.gbMain.Margin = new System.Windows.Forms.Padding(0);
            this.gbMain.Name = "gbMain";
            this.gbMain.Padding = new System.Windows.Forms.Padding(0);
            this.gbMain.Size = new System.Drawing.Size(168, 98);
            this.gbMain.TabIndex = 0;
            this.gbMain.TabStop = false;
            this.gbMain.Text = "运行哪个";
            // 
            // pnMain
            // 
            this.pnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMain.AutoScroll = true;
            this.pnMain.Location = new System.Drawing.Point(3, 17);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(162, 78);
            this.pnMain.TabIndex = 0;
            this.pnMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SelectFrm_MouseMove);
            // 
            // SelectFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(174, 104);
            this.Controls.Add(this.gbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "SelectFrm";
            this.Opacity = 0.9;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SelectFrm_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectFrm_KeyDown);
            this.gbMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.Panel pnMain;
    }
}