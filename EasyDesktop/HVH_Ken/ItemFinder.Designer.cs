namespace HVH_Ken
{
    partial class ItemFinder
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
            this.tbCdt = new HVH_Ken.TextBoxWithoutSound();
            this.btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbAdv = new System.Windows.Forms.GroupBox();
            this.cbTitle = new System.Windows.Forms.CheckBox();
            this.cbIsAR = new System.Windows.Forms.CheckBox();
            this.cbIsTask = new System.Windows.Forms.CheckBox();
            this.cbCmd = new System.Windows.Forms.CheckBox();
            this.cbPath = new System.Windows.Forms.CheckBox();
            this.lbAdv = new System.Windows.Forms.Label();
            this.gbAdv.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCdt
            // 
            this.tbCdt.Location = new System.Drawing.Point(5, 10);
            this.tbCdt.Name = "tbCdt";
            this.tbCdt.Size = new System.Drawing.Size(82, 21);
            this.tbCdt.TabIndex = 7;
            this.tbCdt.TextChanged += new System.EventHandler(this.tbCdt_TextChanged);
            this.tbCdt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCdt_KeyDown);
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(93, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 23);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "GOo。";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(212, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "?";
            this.toolTip.SetToolTip(this.label1, "模糊查找标题以及命令");
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // gbAdv
            // 
            this.gbAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAdv.Controls.Add(this.cbTitle);
            this.gbAdv.Controls.Add(this.cbIsAR);
            this.gbAdv.Controls.Add(this.cbIsTask);
            this.gbAdv.Controls.Add(this.cbCmd);
            this.gbAdv.Controls.Add(this.cbPath);
            this.gbAdv.Location = new System.Drawing.Point(5, 35);
            this.gbAdv.Name = "gbAdv";
            this.gbAdv.Size = new System.Drawing.Size(222, 93);
            this.gbAdv.TabIndex = 15;
            this.gbAdv.TabStop = false;
            this.gbAdv.Text = "高级查询";
            // 
            // cbTitle
            // 
            this.cbTitle.AutoSize = true;
            this.cbTitle.Checked = true;
            this.cbTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTitle.Location = new System.Drawing.Point(15, 25);
            this.cbTitle.Name = "cbTitle";
            this.cbTitle.Size = new System.Drawing.Size(48, 16);
            this.cbTitle.TabIndex = 19;
            this.cbTitle.Tag = "_title";
            this.cbTitle.Text = "标题";
            this.cbTitle.UseVisualStyleBackColor = true;
            // 
            // cbIsAR
            // 
            this.cbIsAR.AutoSize = true;
            this.cbIsAR.Location = new System.Drawing.Point(15, 58);
            this.cbIsAR.Name = "cbIsAR";
            this.cbIsAR.Size = new System.Drawing.Size(60, 16);
            this.cbIsAR.TabIndex = 18;
            this.cbIsAR.Tag = "_isAutRun";
            this.cbIsAR.Text = "启动项";
            this.cbIsAR.UseVisualStyleBackColor = true;
            // 
            // cbIsTask
            // 
            this.cbIsTask.AutoSize = true;
            this.cbIsTask.Location = new System.Drawing.Point(81, 58);
            this.cbIsTask.Name = "cbIsTask";
            this.cbIsTask.Size = new System.Drawing.Size(72, 16);
            this.cbIsTask.TabIndex = 17;
            this.cbIsTask.Tag = "_hasTaskItem";
            this.cbIsTask.Text = "计划任务";
            this.cbIsTask.UseVisualStyleBackColor = true;
            // 
            // cbCmd
            // 
            this.cbCmd.AutoSize = true;
            this.cbCmd.Checked = true;
            this.cbCmd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCmd.Location = new System.Drawing.Point(156, 25);
            this.cbCmd.Name = "cbCmd";
            this.cbCmd.Size = new System.Drawing.Size(48, 16);
            this.cbCmd.TabIndex = 16;
            this.cbCmd.Tag = "_shortcut";
            this.cbCmd.Text = "命令";
            this.cbCmd.UseVisualStyleBackColor = true;
            // 
            // cbPath
            // 
            this.cbPath.AutoSize = true;
            this.cbPath.Checked = true;
            this.cbPath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPath.Location = new System.Drawing.Point(81, 25);
            this.cbPath.Name = "cbPath";
            this.cbPath.Size = new System.Drawing.Size(48, 16);
            this.cbPath.TabIndex = 15;
            this.cbPath.Tag = "_path";
            this.cbPath.Text = "路径";
            this.cbPath.UseVisualStyleBackColor = true;
            // 
            // lbAdv
            // 
            this.lbAdv.AutoSize = true;
            this.lbAdv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbAdv.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAdv.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbAdv.Location = new System.Drawing.Point(142, 13);
            this.lbAdv.Name = "lbAdv";
            this.lbAdv.Size = new System.Drawing.Size(67, 14);
            this.lbAdv.TabIndex = 16;
            this.lbAdv.Text = "高级查询";
            this.lbAdv.Click += new System.EventHandler(this.lbAdv_Click);
            // 
            // ItemFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 135);
            this.Controls.Add(this.lbAdv);
            this.Controls.Add(this.gbAdv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.tbCdt);
            this.MaximizeBox = false;
            this.Name = "ItemFinder";
            this.ShowInTaskbar = false;
            this.Text = "查找";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ItemFinder_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemFinder_FormClosing);
            this.gbAdv.ResumeLayout(false);
            this.gbAdv.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBoxWithoutSound tbCdt;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox gbAdv;
        private System.Windows.Forms.CheckBox cbTitle;
        private System.Windows.Forms.CheckBox cbIsAR;
        private System.Windows.Forms.CheckBox cbIsTask;
        private System.Windows.Forms.CheckBox cbCmd;
        private System.Windows.Forms.CheckBox cbPath;
        private System.Windows.Forms.Label lbAdv;
    }
}