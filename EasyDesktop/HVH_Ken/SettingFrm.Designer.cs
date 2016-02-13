namespace HVH_Ken
{
    partial class SettingFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plMain = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEngEdt = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbKWFlag = new HVH_Ken.TextBoxWithoutSound();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUrl = new HVH_Ken.TextBoxWithoutSound();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbSinfMod = new System.Windows.Forms.Label();
            this.tbHkInf = new HVH_Ken.LetterKeyTextBox();
            this.tbHkAct = new HVH_Ken.HotKeyTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearHints = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.plMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.AllowDrop = true;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(260, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // HToolStripMenuItem
            // 
            this.HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpMenuItem,
            this.toolStripSeparator6,
            this.AToolStripMenuItem});
            this.HToolStripMenuItem.Name = "HToolStripMenuItem";
            this.HToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.HToolStripMenuItem.Text = "帮助";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(112, 22);
            this.helpMenuItem.Text = "帮助[&H]";
            this.helpMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(109, 6);
            // 
            // AToolStripMenuItem
            // 
            this.AToolStripMenuItem.Name = "AToolStripMenuItem";
            this.AToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.AToolStripMenuItem.Text = "关于[&A]";
            this.AToolStripMenuItem.Click += new System.EventHandler(this.AToolStripMenuItem_Click);
            // 
            // plMain
            // 
            this.plMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plMain.AutoScroll = true;
            this.plMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plMain.Controls.Add(this.groupBox3);
            this.plMain.Controls.Add(this.btnSave);
            this.plMain.Controls.Add(this.groupBox2);
            this.plMain.Controls.Add(this.groupBox1);
            this.plMain.Location = new System.Drawing.Point(0, 28);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(260, 286);
            this.plMain.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(183, 256);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(45, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEngEdt);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbKWFlag);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbUrl);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox2.Location = new System.Drawing.Point(11, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(227, 117);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "默认搜索引擎";
            // 
            // btnEngEdt
            // 
            this.btnEngEdt.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEngEdt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEngEdt.Location = new System.Drawing.Point(89, 88);
            this.btnEngEdt.Name = "btnEngEdt";
            this.btnEngEdt.Size = new System.Drawing.Size(88, 23);
            this.btnEngEdt.TabIndex = 2;
            this.btnEngEdt.Text = "编辑搜索引擎";
            this.btnEngEdt.UseVisualStyleBackColor = true;
            this.btnEngEdt.Click += new System.EventHandler(this.btnEngEdt_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "搜索引擎URL：";
            // 
            // tbKWFlag
            // 
            this.tbKWFlag.Location = new System.Drawing.Point(89, 52);
            this.tbKWFlag.MaxLength = 16;
            this.tbKWFlag.Name = "tbKWFlag";
            this.tbKWFlag.Size = new System.Drawing.Size(128, 21);
            this.tbKWFlag.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "关键字标识：";
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(89, 20);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(128, 21);
            this.tbUrl.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbSinfMod);
            this.groupBox1.Controls.Add(this.tbHkInf);
            this.groupBox1.Controls.Add(this.tbHkAct);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox1.Location = new System.Drawing.Point(11, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "热键";
            // 
            // lbSinfMod
            // 
            this.lbSinfMod.AutoSize = true;
            this.lbSinfMod.Location = new System.Drawing.Point(110, 46);
            this.lbSinfMod.Name = "lbSinfMod";
            this.lbSinfMod.Size = new System.Drawing.Size(0, 12);
            this.lbSinfMod.TabIndex = 18;
            // 
            // tbHkInf
            // 
            this.tbHkInf.Location = new System.Drawing.Point(152, 41);
            this.tbHkInf.Name = "tbHkInf";
            this.tbHkInf.Size = new System.Drawing.Size(24, 21);
            this.tbHkInf.TabIndex = 17;
            // 
            // tbHkAct
            // 
            this.tbHkAct.Location = new System.Drawing.Point(108, 14);
            this.tbHkAct.Name = "tbHkAct";
            this.tbHkAct.ReadOnly = true;
            this.tbHkAct.Size = new System.Drawing.Size(68, 21);
            this.tbHkAct.TabIndex = 15;
            this.tbHkAct.TextChanged += new System.EventHandler(this.tbHkChg_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "显示信息：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "激活窗口：";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClearHints);
            this.groupBox3.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox3.Location = new System.Drawing.Point(11, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 45);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "其它";
            // 
            // btnClearHints
            // 
            this.btnClearHints.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClearHints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearHints.Location = new System.Drawing.Point(20, 16);
            this.btnClearHints.Name = "btnClearHints";
            this.btnClearHints.Size = new System.Drawing.Size(197, 23);
            this.btnClearHints.TabIndex = 4;
            this.btnClearHints.Text = "将程序运行排名归零";
            this.btnClearHints.UseVisualStyleBackColor = true;
            this.btnClearHints.Click += new System.EventHandler(this.btnClearHints_Click);
            // 
            // SettingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(260, 312);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.plMain);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "SettingFrm";
            this.Text = "Commander 设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingFrm_FormClosed);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.plMain.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem HToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem AToolStripMenuItem;
        private System.Windows.Forms.Panel plMain;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label6;
        private TextBoxWithoutSound tbKWFlag;
        private System.Windows.Forms.Label label2;
        private TextBoxWithoutSound tbUrl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnEngEdt;
        private System.Windows.Forms.Label label5;
        private HotKeyTextBox tbHkAct;
        private LetterKeyTextBox tbHkInf;
        private System.Windows.Forms.Label lbSinfMod;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClearHints;

    }
}

