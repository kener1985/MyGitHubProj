namespace HVH_Ken
{
    partial class NoticeMgrFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbStop = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lvNotices = new HVH_Ken.MyListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnDateTime = new System.Windows.Forms.Panel();
            this.udDur = new System.Windows.Forms.NumericUpDown();
            this.udTimes = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udHour = new System.Windows.Forms.NumericUpDown();
            this.udMnt = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udSec = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbOneTime = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnDateTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udDur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSec)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbStop);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 24);
            this.panel1.TabIndex = 0;
            // 
            // cbStop
            // 
            this.cbStop.AutoSize = true;
            this.cbStop.Location = new System.Drawing.Point(12, 5);
            this.cbStop.Name = "cbStop";
            this.cbStop.Size = new System.Drawing.Size(72, 16);
            this.cbStop.TabIndex = 22;
            this.cbStop.Text = "暂停通知";
            this.cbStop.UseVisualStyleBackColor = true;
            this.cbStop.CheckedChanged += new System.EventHandler(this.cbStop_CheckedChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Location = new System.Drawing.Point(236, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 23);
            this.btnRefresh.TabIndex = 21;
            this.btnRefresh.Text = "更新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(183, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(44, 23);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "保存";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 320);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(286, 73);
            this.panel2.TabIndex = 1;
            // 
            // tbInfo
            // 
            this.tbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInfo.Location = new System.Drawing.Point(5, 5);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(276, 63);
            this.tbInfo.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.lvNotices);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel3.Size = new System.Drawing.Size(286, 142);
            this.panel3.TabIndex = 2;
            // 
            // lvNotices
            // 
            this.lvNotices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvNotices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNotices.FullRowSelect = true;
            this.lvNotices.GridLines = true;
            this.lvNotices.Location = new System.Drawing.Point(5, 0);
            this.lvNotices.Name = "lvNotices";
            this.lvNotices.OwnerDraw = true;
            this.lvNotices.Size = new System.Drawing.Size(276, 137);
            this.lvNotices.TabIndex = 0;
            this.lvNotices.UseCompatibleStateImageBehavior = false;
            this.lvNotices.View = System.Windows.Forms.View.Details;
            this.lvNotices.SelectedIndexChanged += new System.EventHandler(this.lvNotices_SelectedIndexChanged);
            this.lvNotices.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvNotices_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "通知";
            this.columnHeader1.Width = 252;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 225);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(286, 95);
            this.panel4.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnDateTime);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(276, 85);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时间";
            // 
            // pnDateTime
            // 
            this.pnDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnDateTime.Controls.Add(this.udDur);
            this.pnDateTime.Controls.Add(this.udTimes);
            this.pnDateTime.Controls.Add(this.label7);
            this.pnDateTime.Controls.Add(this.label6);
            this.pnDateTime.Controls.Add(this.label5);
            this.pnDateTime.Controls.Add(this.label2);
            this.pnDateTime.Controls.Add(this.udHour);
            this.pnDateTime.Controls.Add(this.udMnt);
            this.pnDateTime.Controls.Add(this.label1);
            this.pnDateTime.Controls.Add(this.label3);
            this.pnDateTime.Controls.Add(this.label4);
            this.pnDateTime.Controls.Add(this.udSec);
            this.pnDateTime.Location = new System.Drawing.Point(4, 17);
            this.pnDateTime.Name = "pnDateTime";
            this.pnDateTime.Size = new System.Drawing.Size(269, 65);
            this.pnDateTime.TabIndex = 23;
            // 
            // udDur
            // 
            this.udDur.DecimalPlaces = 1;
            this.udDur.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udDur.Location = new System.Drawing.Point(167, 36);
            this.udDur.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.udDur.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udDur.Name = "udDur";
            this.udDur.Size = new System.Drawing.Size(49, 21);
            this.udDur.TabIndex = 34;
            this.udDur.Tag = "2";
            this.udDur.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // udTimes
            // 
            this.udTimes.Location = new System.Drawing.Point(64, 36);
            this.udTimes.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udTimes.Name = "udTimes";
            this.udTimes.Size = new System.Drawing.Size(54, 21);
            this.udTimes.TabIndex = 33;
            this.udTimes.Tag = "2";
            this.udTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(222, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "分钟";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(126, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "间隔:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "次数:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "时间：";
            // 
            // udHour
            // 
            this.udHour.Location = new System.Drawing.Point(64, 9);
            this.udHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.udHour.Name = "udHour";
            this.udHour.Size = new System.Drawing.Size(33, 21);
            this.udHour.TabIndex = 12;
            this.udHour.Tag = "2";
            // 
            // udMnt
            // 
            this.udMnt.Location = new System.Drawing.Point(126, 9);
            this.udMnt.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.udMnt.Name = "udMnt";
            this.udMnt.Size = new System.Drawing.Size(33, 21);
            this.udMnt.TabIndex = 13;
            this.udMnt.Tag = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "时";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "分";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "秒";
            // 
            // udSec
            // 
            this.udSec.Location = new System.Drawing.Point(185, 9);
            this.udSec.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.udSec.Name = "udSec";
            this.udSec.Size = new System.Drawing.Size(33, 21);
            this.udSec.TabIndex = 14;
            this.udSec.Tag = "2";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 148);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(5);
            this.panel5.Size = new System.Drawing.Size(286, 77);
            this.panel5.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(276, 67);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "高级";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.cbOneTime);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.dtpDate);
            this.panel6.Location = new System.Drawing.Point(4, 17);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(269, 47);
            this.panel6.TabIndex = 23;
            // 
            // cbOneTime
            // 
            this.cbOneTime.AutoSize = true;
            this.cbOneTime.Location = new System.Drawing.Point(185, 13);
            this.cbOneTime.Name = "cbOneTime";
            this.cbOneTime.Size = new System.Drawing.Size(60, 16);
            this.cbOneTime.TabIndex = 13;
            this.cbOneTime.Text = "一次性";
            this.cbOneTime.UseVisualStyleBackColor = true;
            this.cbOneTime.CheckedChanged += new System.EventHandler(this.cbOneTime_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "日期：";
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Location = new System.Drawing.Point(64, 12);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(117, 21);
            this.dtpDate.TabIndex = 0;
            // 
            // NoticeMgrFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(286, 417);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "NoticeMgrFrm";
            this.Text = "通知";
            this.Load += new System.EventHandler(this.NoticeMgrFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.pnDateTime.ResumeLayout(false);
            this.pnDateTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udDur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSec)).EndInit();
            this.panel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private HVH_Ken.MyListView lvNotices;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnDateTime;
        private System.Windows.Forms.NumericUpDown udHour;
        private System.Windows.Forms.NumericUpDown udMnt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udSec;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udDur;
        private System.Windows.Forms.NumericUpDown udTimes;
        private System.Windows.Forms.CheckBox cbStop;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbOneTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}
