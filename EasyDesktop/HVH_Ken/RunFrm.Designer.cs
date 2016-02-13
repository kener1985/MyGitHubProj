namespace HVH_Ken
{
    partial class RunFrm
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
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tbCmd = new HVH_Ken.TextBoxWithoutSound();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CfgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.itemNotice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHotKey = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsOptimize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsItemSrcEdt = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.plMain = new System.Windows.Forms.Panel();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.plMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 0;
            this.toolTip.InitialDelay = 100;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            // 
            // tbCmd
            // 
            this.tbCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCmd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbCmd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbCmd.BackColor = System.Drawing.Color.AliceBlue;
            this.tbCmd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCmd.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbCmd.Location = new System.Drawing.Point(24, 4);
            this.tbCmd.MaxLength = 0;
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(138, 14);
            this.tbCmd.TabIndex = 0;
            this.toolTip.SetToolTip(this.tbCmd, "在此输入运行命令");
            this.tbCmd.TextChanged += new System.EventHandler(this.tbCmd_TextChanged);
            this.tbCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.tbCmd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCmd_KeyPress);
            this.tbCmd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbCmd_MouseDoubleClick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.notifyIcon.BalloonTipTitle = "嘿嘿";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Text = "Commander";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CfgToolStripMenuItem,
            this.EditEToolStripMenuItem,
            this.toolStripSeparator9,
            this.itemNotice,
            this.tsmHotKey,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.tsOptimize,
            this.tsItemSrcEdt,
            this.ExitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(161, 176);
            // 
            // CfgToolStripMenuItem
            // 
            this.CfgToolStripMenuItem.Name = "CfgToolStripMenuItem";
            this.CfgToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.CfgToolStripMenuItem.Text = "系统配置[&A]";
            this.CfgToolStripMenuItem.Click += new System.EventHandler(this.CfgToolStripMenuItem_Click);
            // 
            // EditEToolStripMenuItem
            // 
            this.EditEToolStripMenuItem.Name = "EditEToolStripMenuItem";
            this.EditEToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.EditEToolStripMenuItem.Text = "随机窗口[&E]";
            this.EditEToolStripMenuItem.Click += new System.EventHandler(this.EditEToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(157, 6);
            // 
            // itemNotice
            // 
            this.itemNotice.Name = "itemNotice";
            this.itemNotice.Size = new System.Drawing.Size(160, 22);
            this.itemNotice.Text = "通知设置[&N]";
            this.itemNotice.Click += new System.EventHandler(this.itemNotice_Click);
            // 
            // tsmHotKey
            // 
            this.tsmHotKey.Name = "tsmHotKey";
            this.tsmHotKey.Size = new System.Drawing.Size(160, 22);
            this.tsmHotKey.Text = "热盘设置[&H]";
            this.tsmHotKey.Click += new System.EventHandler(this.tsmHotKey_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem1.Text = "编辑搜索引擎[&E]";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // tsOptimize
            // 
            this.tsOptimize.Name = "tsOptimize";
            this.tsOptimize.Size = new System.Drawing.Size(160, 22);
            this.tsOptimize.Text = "数据优化[&O]";
            this.tsOptimize.Click += new System.EventHandler(this.tsOptimize_Click);
            // 
            // tsItemSrcEdt
            // 
            this.tsItemSrcEdt.Name = "tsItemSrcEdt";
            this.tsItemSrcEdt.Size = new System.Drawing.Size(157, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ExitToolStripMenuItem.Text = "退出[&X]";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLogo.BackColor = System.Drawing.SystemColors.Window;
            this.pbLogo.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pbLogo.InitialImage = null;
            this.pbLogo.Location = new System.Drawing.Point(1, 1);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(20, 20);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 1;
            this.pbLogo.TabStop = false;
            this.pbLogo.Click += new System.EventHandler(this.pbLogo_Click);
            this.pbLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbLogo_MouseDown);
            this.pbLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveImpl);
            // 
            // plMain
            // 
            this.plMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plMain.BackColor = System.Drawing.SystemColors.Window;
            this.plMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plMain.Controls.Add(this.tbCmd);
            this.plMain.Controls.Add(this.pbLogo);
            this.plMain.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.plMain.Location = new System.Drawing.Point(0, 0);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(168, 24);
            this.plMain.TabIndex = 2;
            // 
            // RunFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(168, 24);
            this.Controls.Add(this.plMain);
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "RunFrm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.ControlLight;
            this.Deactivate += new System.EventHandler(this.RunFrm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RunFrm_FormClosing);
            this.Load += new System.EventHandler(this.RunFrm_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveImpl);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.plMain.ResumeLayout(false);
            this.plMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBoxWithoutSound tbCmd;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem CfgToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator tsItemSrcEdt;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem EditEToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel plMain;
        private System.Windows.Forms.ToolStripMenuItem itemNotice;
        private System.Windows.Forms.ToolStripMenuItem tsOptimize;
        private System.Windows.Forms.ToolStripMenuItem tsmHotKey;
    }
}