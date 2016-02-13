namespace HVH_Ken
{
    partial class TaskSetting
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcSet = new System.Windows.Forms.TabControl();
            this.tpDay = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.tpWeek = new System.Windows.Forms.TabPage();
            this.btnCvs = new System.Windows.Forms.Button();
            this.cbSat = new System.Windows.Forms.CheckBox();
            this.cbSun = new System.Windows.Forms.CheckBox();
            this.cbFri = new System.Windows.Forms.CheckBox();
            this.cbThu = new System.Windows.Forms.CheckBox();
            this.cbWed = new System.Windows.Forms.CheckBox();
            this.cbTus = new System.Windows.Forms.CheckBox();
            this.cbMon = new System.Windows.Forms.CheckBox();
            this.tpMonth = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.tpYear = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.cbClose = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnDateTime = new System.Windows.Forms.Panel();
            this.udHour = new System.Windows.Forms.NumericUpDown();
            this.udMnt = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udSec = new System.Windows.Forms.NumericUpDown();
            this.tcSet.SuspendLayout();
            this.tpDay.SuspendLayout();
            this.tpWeek.SuspendLayout();
            this.tpMonth.SuspendLayout();
            this.tpYear.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnDateTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSec)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(171, 195);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 23);
            this.btnOk.TabIndex = 19;
            this.btnOk.Text = "确定[&O]";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(237, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消[&C]";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tcSet
            // 
            this.tcSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSet.Controls.Add(this.tpDay);
            this.tcSet.Controls.Add(this.tpWeek);
            this.tcSet.Controls.Add(this.tpMonth);
            this.tcSet.Controls.Add(this.tpYear);
            this.tcSet.Location = new System.Drawing.Point(12, 33);
            this.tcSet.Name = "tcSet";
            this.tcSet.SelectedIndex = 0;
            this.tcSet.Size = new System.Drawing.Size(285, 86);
            this.tcSet.TabIndex = 2;
            // 
            // tpDay
            // 
            this.tpDay.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tpDay.Controls.Add(this.label5);
            this.tpDay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tpDay.Location = new System.Drawing.Point(4, 21);
            this.tpDay.Name = "tpDay";
            this.tpDay.Padding = new System.Windows.Forms.Padding(3);
            this.tpDay.Size = new System.Drawing.Size(277, 61);
            this.tpDay.TabIndex = 0;
            this.tpDay.Text = "每天";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "运行计划设置需重启才能生效！";
            // 
            // tpWeek
            // 
            this.tpWeek.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tpWeek.Controls.Add(this.btnCvs);
            this.tpWeek.Controls.Add(this.cbSat);
            this.tpWeek.Controls.Add(this.cbSun);
            this.tpWeek.Controls.Add(this.cbFri);
            this.tpWeek.Controls.Add(this.cbThu);
            this.tpWeek.Controls.Add(this.cbWed);
            this.tpWeek.Controls.Add(this.cbTus);
            this.tpWeek.Controls.Add(this.cbMon);
            this.tpWeek.Location = new System.Drawing.Point(4, 21);
            this.tpWeek.Name = "tpWeek";
            this.tpWeek.Padding = new System.Windows.Forms.Padding(3);
            this.tpWeek.Size = new System.Drawing.Size(277, 61);
            this.tpWeek.TabIndex = 2;
            this.tpWeek.Text = "每周";
            // 
            // btnCvs
            // 
            this.btnCvs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCvs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCvs.Location = new System.Drawing.Point(211, 30);
            this.btnCvs.Name = "btnCvs";
            this.btnCvs.Size = new System.Drawing.Size(60, 23);
            this.btnCvs.TabIndex = 20;
            this.btnCvs.Text = "反选[&R]";
            this.btnCvs.UseVisualStyleBackColor = true;
            this.btnCvs.Click += new System.EventHandler(this.btnCvs_Click);
            // 
            // cbSat
            // 
            this.cbSat.AutoSize = true;
            this.cbSat.Location = new System.Drawing.Point(80, 34);
            this.cbSat.Name = "cbSat";
            this.cbSat.Size = new System.Drawing.Size(60, 16);
            this.cbSat.TabIndex = 6;
            this.cbSat.Tag = "6";
            this.cbSat.Text = "星期六";
            this.cbSat.UseVisualStyleBackColor = true;
            // 
            // cbSun
            // 
            this.cbSun.AutoSize = true;
            this.cbSun.Location = new System.Drawing.Point(146, 34);
            this.cbSun.Name = "cbSun";
            this.cbSun.Size = new System.Drawing.Size(60, 16);
            this.cbSun.TabIndex = 7;
            this.cbSun.Tag = "0";
            this.cbSun.Text = "星期日";
            this.cbSun.UseVisualStyleBackColor = true;
            // 
            // cbFri
            // 
            this.cbFri.AutoSize = true;
            this.cbFri.Location = new System.Drawing.Point(14, 34);
            this.cbFri.Name = "cbFri";
            this.cbFri.Size = new System.Drawing.Size(60, 16);
            this.cbFri.TabIndex = 5;
            this.cbFri.Tag = "5";
            this.cbFri.Text = "星期五";
            this.cbFri.UseVisualStyleBackColor = true;
            // 
            // cbThu
            // 
            this.cbThu.AutoSize = true;
            this.cbThu.Location = new System.Drawing.Point(212, 12);
            this.cbThu.Name = "cbThu";
            this.cbThu.Size = new System.Drawing.Size(60, 16);
            this.cbThu.TabIndex = 4;
            this.cbThu.Tag = "4";
            this.cbThu.Text = "星期四";
            this.cbThu.UseVisualStyleBackColor = true;
            // 
            // cbWed
            // 
            this.cbWed.AutoSize = true;
            this.cbWed.Location = new System.Drawing.Point(146, 12);
            this.cbWed.Name = "cbWed";
            this.cbWed.Size = new System.Drawing.Size(60, 16);
            this.cbWed.TabIndex = 3;
            this.cbWed.Tag = "3";
            this.cbWed.Text = "星期三";
            this.cbWed.UseVisualStyleBackColor = true;
            // 
            // cbTus
            // 
            this.cbTus.AutoSize = true;
            this.cbTus.Location = new System.Drawing.Point(80, 12);
            this.cbTus.Name = "cbTus";
            this.cbTus.Size = new System.Drawing.Size(60, 16);
            this.cbTus.TabIndex = 2;
            this.cbTus.Tag = "2";
            this.cbTus.Text = "星期二";
            this.cbTus.UseVisualStyleBackColor = true;
            // 
            // cbMon
            // 
            this.cbMon.AutoSize = true;
            this.cbMon.Location = new System.Drawing.Point(14, 12);
            this.cbMon.Name = "cbMon";
            this.cbMon.Size = new System.Drawing.Size(60, 16);
            this.cbMon.TabIndex = 1;
            this.cbMon.Tag = "1";
            this.cbMon.Text = "星期一";
            this.cbMon.UseVisualStyleBackColor = true;
            // 
            // tpMonth
            // 
            this.tpMonth.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tpMonth.Controls.Add(this.label6);
            this.tpMonth.Location = new System.Drawing.Point(4, 21);
            this.tpMonth.Name = "tpMonth";
            this.tpMonth.Padding = new System.Windows.Forms.Padding(3);
            this.tpMonth.Size = new System.Drawing.Size(277, 61);
            this.tpMonth.TabIndex = 3;
            this.tpMonth.Text = "每月";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(115, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "谢谢关注";
            // 
            // tpYear
            // 
            this.tpYear.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tpYear.Controls.Add(this.label7);
            this.tpYear.Location = new System.Drawing.Point(4, 21);
            this.tpYear.Name = "tpYear";
            this.tpYear.Padding = new System.Windows.Forms.Padding(3);
            this.tpYear.Size = new System.Drawing.Size(277, 61);
            this.tpYear.TabIndex = 4;
            this.tpYear.Text = "每年";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(115, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "谢谢关注";
            // 
            // cbClose
            // 
            this.cbClose.AutoSize = true;
            this.cbClose.Location = new System.Drawing.Point(13, 11);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(72, 16);
            this.cbClose.TabIndex = 21;
            this.cbClose.Text = "关闭任务";
            this.cbClose.UseVisualStyleBackColor = true;
            this.cbClose.CheckedChanged += new System.EventHandler(this.cbClose_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnDateTime);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox1.Location = new System.Drawing.Point(16, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 65);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时间";
            // 
            // pnDateTime
            // 
            this.pnDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnDateTime.Controls.Add(this.udHour);
            this.pnDateTime.Controls.Add(this.udMnt);
            this.pnDateTime.Controls.Add(this.label1);
            this.pnDateTime.Controls.Add(this.label3);
            this.pnDateTime.Controls.Add(this.label4);
            this.pnDateTime.Controls.Add(this.udSec);
            this.pnDateTime.Location = new System.Drawing.Point(7, 20);
            this.pnDateTime.Name = "pnDateTime";
            this.pnDateTime.Size = new System.Drawing.Size(264, 39);
            this.pnDateTime.TabIndex = 23;
            // 
            // udHour
            // 
            this.udHour.Location = new System.Drawing.Point(36, 9);
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
            this.udMnt.Location = new System.Drawing.Point(98, 9);
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
            this.label1.Location = new System.Drawing.Point(75, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "时";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "分";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "秒";
            // 
            // udSec
            // 
            this.udSec.Location = new System.Drawing.Point(160, 9);
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
            // TaskSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 230);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbClose);
            this.Controls.Add(this.tcSet);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "TaskSetting";
            this.Text = "运行计划设置";
            this.Load += new System.EventHandler(this.TaskSetting_Load);
            this.tcSet.ResumeLayout(false);
            this.tpDay.ResumeLayout(false);
            this.tpDay.PerformLayout();
            this.tpWeek.ResumeLayout(false);
            this.tpWeek.PerformLayout();
            this.tpMonth.ResumeLayout(false);
            this.tpMonth.PerformLayout();
            this.tpYear.ResumeLayout(false);
            this.tpYear.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.pnDateTime.ResumeLayout(false);
            this.pnDateTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tcSet;
        private System.Windows.Forms.TabPage tpDay;
        private System.Windows.Forms.TabPage tpWeek;
        private System.Windows.Forms.TabPage tpMonth;
        private System.Windows.Forms.TabPage tpYear;
        private System.Windows.Forms.CheckBox cbClose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbSat;
        private System.Windows.Forms.CheckBox cbSun;
        private System.Windows.Forms.CheckBox cbFri;
        private System.Windows.Forms.CheckBox cbThu;
        private System.Windows.Forms.CheckBox cbWed;
        private System.Windows.Forms.CheckBox cbTus;
        private System.Windows.Forms.CheckBox cbMon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnDateTime;
        private System.Windows.Forms.NumericUpDown udHour;
        private System.Windows.Forms.NumericUpDown udMnt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udSec;
        private System.Windows.Forms.Button btnCvs;
    }
}