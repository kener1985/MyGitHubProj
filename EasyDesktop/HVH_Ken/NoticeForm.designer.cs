namespace HVH_Ken
{
    partial class NoticeForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbStop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbClose = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.cbStop);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbClose);
            this.panel1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 19);
            this.panel1.TabIndex = 1;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseEnter += new System.EventHandler(this.rtbInfo_MouseEnter);
            // 
            // cbStop
            // 
            this.cbStop.AutoSize = true;
            this.cbStop.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbStop.ForeColor = System.Drawing.Color.Yellow;
            this.cbStop.Location = new System.Drawing.Point(150, 2);
            this.cbStop.Name = "cbStop";
            this.cbStop.Size = new System.Drawing.Size(72, 16);
            this.cbStop.TabIndex = 2;
            this.cbStop.Text = "暂停通知";
            this.cbStop.UseVisualStyleBackColor = true;
            this.cbStop.CheckedChanged += new System.EventHandler(this.cbStop_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "您有新的通知^_^";
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // lbClose
            // 
            this.lbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbClose.AutoSize = true;
            this.lbClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbClose.ForeColor = System.Drawing.Color.Yellow;
            this.lbClose.Location = new System.Drawing.Point(228, 3);
            this.lbClose.Name = "lbClose";
            this.lbClose.Size = new System.Drawing.Size(17, 12);
            this.lbClose.TabIndex = 0;
            this.lbClose.Text = "※";
            this.lbClose.Click += new System.EventHandler(this.lbClose_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rtbInfo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(249, 96);
            this.panel3.TabIndex = 5;
            // 
            // rtbInfo
            // 
            this.rtbInfo.BackColor = System.Drawing.Color.Honeydew;
            this.rtbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbInfo.ForeColor = System.Drawing.Color.Blue;
            this.rtbInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ReadOnly = true;
            this.rtbInfo.Size = new System.Drawing.Size(249, 96);
            this.rtbInfo.TabIndex = 4;
            this.rtbInfo.Text = "";
            // 
            // NoticeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(253, 119);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NoticeForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NoticeForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NoticeForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NoticeForm_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.CheckBox cbStop;
    }
}