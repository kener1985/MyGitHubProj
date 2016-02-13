namespace HVH_Ken
{
    partial class HotKeyMgrFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnAddProg = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCmd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbWin = new System.Windows.Forms.CheckBox();
            this.tbKey = new HVH_Ken.HotKeyTextBox(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvHotkeys = new HVH_Ken.MyListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDir);
            this.panel1.Controls.Add(this.btnAddProg);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbCmd);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbWin);
            this.panel1.Controls.Add(this.tbKey);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 292);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 75);
            this.panel1.TabIndex = 5;
            // 
            // btnDir
            // 
            this.btnDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDir.Location = new System.Drawing.Point(260, 45);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(21, 21);
            this.btnDir.TabIndex = 14;
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnAddProg
            // 
            this.btnAddProg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddProg.Location = new System.Drawing.Point(238, 45);
            this.btnAddProg.Name = "btnAddProg";
            this.btnAddProg.Size = new System.Drawing.Size(21, 21);
            this.btnAddProg.TabIndex = 13;
            this.btnAddProg.UseVisualStyleBackColor = true;
            this.btnAddProg.Click += new System.EventHandler(this.btnAddProg_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Location = new System.Drawing.Point(399, 8);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(45, 23);
            this.btnTest.TabIndex = 12;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(399, 43);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(45, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(348, 43);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 23);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "更新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "命令：";
            // 
            // tbCmd
            // 
            this.tbCmd.Location = new System.Drawing.Point(59, 45);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(172, 21);
            this.tbCmd.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "热键：";
            // 
            // cbWin
            // 
            this.cbWin.AutoSize = true;
            this.cbWin.Location = new System.Drawing.Point(238, 12);
            this.cbWin.Name = "cbWin";
            this.cbWin.Size = new System.Drawing.Size(54, 16);
            this.cbWin.TabIndex = 6;
            this.cbWin.Text = "Win键";
            this.cbWin.UseVisualStyleBackColor = true;
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(59, 10);
            this.tbKey.Name = "tbKey";
            this.tbKey.ReadOnly = true;
            this.tbKey.Size = new System.Drawing.Size(172, 21);
            this.tbKey.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvHotkeys);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(456, 292);
            this.panel2.TabIndex = 6;
            // 
            // lvHotkeys
            // 
            this.lvHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvHotkeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvHotkeys.FullRowSelect = true;
            this.lvHotkeys.GridLines = true;
            this.lvHotkeys.Location = new System.Drawing.Point(0, 0);
            this.lvHotkeys.Name = "lvHotkeys";
            this.lvHotkeys.OwnerDraw = true;
            this.lvHotkeys.Size = new System.Drawing.Size(456, 292);
            this.lvHotkeys.TabIndex = 0;
            this.lvHotkeys.UseCompatibleStateImageBehavior = false;
            this.lvHotkeys.View = System.Windows.Forms.View.Details;
            this.lvHotkeys.SelectedIndexChanged += new System.EventHandler(this.lvHotkeys_SelectedIndexChanged);
            this.lvHotkeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvHotkeys_KeyDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "热键";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 157;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "命令";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 288;
            // 
            // HotKeyMgrFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(456, 367);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "HotKeyMgrFrm";
            this.Text = "热键管理";
            this.Load += new System.EventHandler(this.HotKeyMgrFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCmd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbWin;
        private HotKeyTextBox tbKey;
        private System.Windows.Forms.Panel panel2;
        private HVH_Ken.MyListView lvHotkeys;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Button btnAddProg;

    }
}
