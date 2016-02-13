namespace DefaultAction
{
    partial class PaybackFrm
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
            this.btnPayback = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMark = new System.Windows.Forms.TextBox();
            this.nudAmt = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmt)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPayback
            // 
            this.btnPayback.Location = new System.Drawing.Point(158, 123);
            this.btnPayback.Name = "btnPayback";
            this.btnPayback.Size = new System.Drawing.Size(75, 23);
            this.btnPayback.TabIndex = 0;
            this.btnPayback.Text = "还款";
            this.btnPayback.UseVisualStyleBackColor = true;
            this.btnPayback.Click += new System.EventHandler(this.btnPayback_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "金额：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "备注：";
            // 
            // tbMark
            // 
            this.tbMark.Location = new System.Drawing.Point(59, 41);
            this.tbMark.Multiline = true;
            this.tbMark.Name = "tbMark";
            this.tbMark.Size = new System.Drawing.Size(174, 76);
            this.tbMark.TabIndex = 4;
            // 
            // nudAmt
            // 
            this.nudAmt.DecimalPlaces = 2;
            this.nudAmt.Location = new System.Drawing.Point(59, 7);
            this.nudAmt.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.nudAmt.Name = "nudAmt";
            this.nudAmt.Size = new System.Drawing.Size(174, 21);
            this.nudAmt.TabIndex = 5;
            // 
            // PaybackFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 158);
            this.Controls.Add(this.nudAmt);
            this.Controls.Add(this.tbMark);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPayback);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PaybackFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户还款";
            ((System.ComponentModel.ISupportInitialize)(this.nudAmt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPayback;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMark;
        private System.Windows.Forms.NumericUpDown nudAmt;
    }
}