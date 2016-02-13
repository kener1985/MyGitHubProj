namespace HVH_Ken
{
    partial class RecOprFrm
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
            this.btnChgMod = new System.Windows.Forms.Button();
            this.btnTask = new System.Windows.Forms.Button();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAddProg = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnMod = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbAutRun = new System.Windows.Forms.CheckBox();
            this.tbSct = new HVH_Ken.TextBoxWithoutSound();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTitle = new HVH_Ken.TextBoxWithoutSound();
            this.tbPath = new HVH_Ken.TextBoxWithoutSound();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lvRecords = new HVH_Ken.MyListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lbAutoRun = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTaskSum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSum = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 1000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            // 
            // btnChgMod
            // 
            this.btnChgMod.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChgMod.Location = new System.Drawing.Point(346, 39);
            this.btnChgMod.Name = "btnChgMod";
            this.btnChgMod.Size = new System.Drawing.Size(21, 21);
            this.btnChgMod.TabIndex = 7;
            this.toolTip.SetToolTip(this.btnChgMod, "切换模式");
            this.btnChgMod.UseVisualStyleBackColor = true;
            this.btnChgMod.Click += new System.EventHandler(this.btnChgMod_Click);
            // 
            // btnTask
            // 
            this.btnTask.Enabled = false;
            this.btnTask.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTask.Location = new System.Drawing.Point(373, 39);
            this.btnTask.Name = "btnTask";
            this.btnTask.Size = new System.Drawing.Size(21, 21);
            this.btnTask.TabIndex = 8;
            this.toolTip.SetToolTip(this.btnTask, "计划任务设置");
            this.btnTask.UseVisualStyleBackColor = true;
            this.btnTask.Click += new System.EventHandler(this.btnTask_Click);
            // 
            // btnDir
            // 
            this.btnDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDir.Location = new System.Drawing.Point(275, 39);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(21, 21);
            this.btnDir.TabIndex = 7;
            this.toolTip.SetToolTip(this.btnDir, "添加文件夹路径");
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(400, 39);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(21, 21);
            this.btnSave.TabIndex = 9;
            this.toolTip.SetToolTip(this.btnSave, "保存");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(346, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(21, 21);
            this.btnAdd.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnAdd, "添加");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddProg
            // 
            this.btnAddProg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddProg.Location = new System.Drawing.Point(248, 39);
            this.btnAddProg.Name = "btnAddProg";
            this.btnAddProg.Size = new System.Drawing.Size(21, 21);
            this.btnAddProg.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnAddProg, "添加程序路径");
            this.btnAddProg.UseVisualStyleBackColor = true;
            this.btnAddProg.Click += new System.EventHandler(this.btnAddProg_Click);
            // 
            // btnDel
            // 
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDel.Location = new System.Drawing.Point(400, 12);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(21, 21);
            this.btnDel.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnDel, "删除");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnMod
            // 
            this.btnMod.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMod.Location = new System.Drawing.Point(373, 12);
            this.btnMod.Name = "btnMod";
            this.btnMod.Size = new System.Drawing.Size(21, 21);
            this.btnMod.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnMod, "修改");
            this.btnMod.UseVisualStyleBackColor = true;
            this.btnMod.Click += new System.EventHandler(this.btnMod_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.Controls.Add(this.btnChgMod);
            this.panel3.Controls.Add(this.btnTask);
            this.panel3.Controls.Add(this.btnDir);
            this.panel3.Controls.Add(this.cbAutRun);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.tbSct);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.btnAddProg);
            this.panel3.Controls.Add(this.btnDel);
            this.panel3.Controls.Add(this.btnMod);
            this.panel3.Controls.Add(this.tbTitle);
            this.panel3.Controls.Add(this.tbPath);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(451, 79);
            this.panel3.TabIndex = 3;
            // 
            // cbAutRun
            // 
            this.cbAutRun.AutoSize = true;
            this.cbAutRun.Location = new System.Drawing.Point(248, 16);
            this.cbAutRun.Name = "cbAutRun";
            this.cbAutRun.Size = new System.Drawing.Size(72, 16);
            this.cbAutRun.TabIndex = 3;
            this.cbAutRun.Text = "自动运行";
            this.cbAutRun.UseVisualStyleBackColor = true;
            // 
            // tbSct
            // 
            this.tbSct.Location = new System.Drawing.Point(175, 12);
            this.tbSct.MaxLength = 8;
            this.tbSct.Name = "tbSct";
            this.tbSct.Size = new System.Drawing.Size(67, 21);
            this.tbSct.TabIndex = 1;
            this.tbSct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCommon_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(136, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "命令:";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(37, 12);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(93, 21);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCommon_KeyDown);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(37, 39);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(205, 21);
            this.tbPath.TabIndex = 2;
            this.tbPath.MouseLeave += new System.EventHandler(this.tbPath_MouseLeave);
            this.tbPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCommon_KeyDown);
            this.tbPath.MouseHover += new System.EventHandler(this.tbPath_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "位置:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 209);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lvRecords);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(451, 180);
            this.panel2.TabIndex = 2;
            // 
            // m_lvRecords
            // 
            this.m_lvRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lvRecords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader1});
            this.m_lvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvRecords.FullRowSelect = true;
            this.m_lvRecords.GridLines = true;
            this.m_lvRecords.Location = new System.Drawing.Point(0, 0);
            this.m_lvRecords.Name = "m_lvRecords";
            this.m_lvRecords.OwnerDraw = true;
            this.m_lvRecords.Size = new System.Drawing.Size(451, 180);
            this.m_lvRecords.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_lvRecords.TabIndex = 2;
            this.m_lvRecords.UseCompatibleStateImageBehavior = false;
            this.m_lvRecords.View = System.Windows.Forms.View.Details;
            this.m_lvRecords.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lvRecords_ColumnClick);
            this.m_lvRecords.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvRecords_ItemSelectionChanged);
            this.m_lvRecords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvRecords_KeyDown);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "标题";
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "位置";
            this.columnHeader4.Width = 220;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "命令";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "1";
            this.columnHeader1.Text = "运行排名";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 76;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.lbAutoRun);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.lbTaskSum);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.lbSum);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 180);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(451, 29);
            this.panel4.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(221, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "自动运行数：";
            // 
            // lbAutoRun
            // 
            this.lbAutoRun.AutoSize = true;
            this.lbAutoRun.Location = new System.Drawing.Point(301, 8);
            this.lbAutoRun.Name = "lbAutoRun";
            this.lbAutoRun.Size = new System.Drawing.Size(11, 12);
            this.lbAutoRun.TabIndex = 20;
            this.lbAutoRun.Text = " ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(109, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "计划任务数：";
            // 
            // lbTaskSum
            // 
            this.lbTaskSum.AutoSize = true;
            this.lbTaskSum.Location = new System.Drawing.Point(189, 8);
            this.lbTaskSum.Name = "lbTaskSum";
            this.lbTaskSum.Size = new System.Drawing.Size(11, 12);
            this.lbTaskSum.TabIndex = 17;
            this.lbTaskSum.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "共";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "项";
            // 
            // lbSum
            // 
            this.lbSum.AutoSize = true;
            this.lbSum.Location = new System.Drawing.Point(27, 8);
            this.lbSum.Name = "lbSum";
            this.lbSum.Size = new System.Drawing.Size(11, 12);
            this.lbSum.TabIndex = 13;
            this.lbSum.Text = " ";
            // 
            // RecOprFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 288);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "RecOprFrm";
            this.Text = "添加";
            this.Load += new System.EventHandler(this.OprFrm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddBatFrm_FormClosing);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnChgMod;
        private System.Windows.Forms.Button btnTask;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.CheckBox cbAutRun;
        private System.Windows.Forms.Button btnSave;
        private TextBoxWithoutSound tbSct;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddProg;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnMod;
        private TextBoxWithoutSound tbTitle;
        private TextBoxWithoutSound tbPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private MyListView m_lvRecords;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbAutoRun;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbTaskSum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbSum;
    }
}