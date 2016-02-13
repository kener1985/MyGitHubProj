namespace HVH_Ken
{
    partial class ShowInfoFrm
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
            this.lvRec = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSum = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.tbCdt = new HVH_Ken.TextBoxWithoutSound();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvRec
            // 
            this.lvRec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRec.AutoArrange = false;
            this.lvRec.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lvRec.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvRec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvRec.FullRowSelect = true;
            this.lvRec.GridLines = true;
            this.lvRec.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvRec.Location = new System.Drawing.Point(1, 0);
            this.lvRec.MultiSelect = false;
            this.lvRec.Name = "lvRec";
            this.lvRec.ShowItemToolTips = true;
            this.lvRec.Size = new System.Drawing.Size(153, 192);
            this.lvRec.TabIndex = 0;
            this.toolTip.SetToolTip(this.lvRec, "按F5可以进行刷新哦!\r\n双击运行程序");
            this.lvRec.UseCompatibleStateImageBehavior = false;
            this.lvRec.View = System.Windows.Forms.View.Details;
            this.lvRec.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRec_MouseDoubleClick);
            this.lvRec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvRec_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "命令";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbSum);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.tbCdt);
            this.panel1.Location = new System.Drawing.Point(1, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 25);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "共";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "项";
            // 
            // lbSum
            // 
            this.lbSum.AutoSize = true;
            this.lbSum.Location = new System.Drawing.Point(118, 6);
            this.lbSum.Name = "lbSum";
            this.lbSum.Size = new System.Drawing.Size(11, 12);
            this.lbSum.TabIndex = 10;
            this.lbSum.Text = " ";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(51, -1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 23);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "GOo。";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tbCdt
            // 
            this.tbCdt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbCdt.Location = new System.Drawing.Point(3, 1);
            this.tbCdt.Name = "tbCdt";
            this.tbCdt.Size = new System.Drawing.Size(42, 21);
            this.tbCdt.TabIndex = 0;
            this.tbCdt.TextChanged += new System.EventHandler(this.tbCdt_TextChanged);
            this.tbCdt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCdt_KeyDown);
            // 
            // ShowInfoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(154, 219);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lvRec);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowInfoFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ShowInfoFrm_Load);
            this.VisibleChanged += new System.EventHandler(this.ShowInfoFrm_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowInfoFrm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvRec;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel1;
        private HVH_Ken.TextBoxWithoutSound tbCdt;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lbSum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}