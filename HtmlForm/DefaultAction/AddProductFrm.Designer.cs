namespace DefaultAction
{
    partial class AddProductFrm
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbId = new System.Windows.Forms.TextBox();
            this.tbColorNum = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbInnerId = new System.Windows.Forms.TextBox();
            this.tbSize = new System.Windows.Forms.TextBox();
            this.tbPosition = new System.Windows.Forms.TextBox();
            this.nudCost = new System.Windows.Forms.NumericUpDown();
            this.nudStore = new System.Windows.Forms.NumericUpDown();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.nudPkgNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPkgNum)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(314, 160);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "色号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "实际编号：";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 39;
            this.label3.Text = "名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 49;
            this.label4.Text = "成本价：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 59;
            this.label5.Text = "位置：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 69;
            this.label6.Text = "包装数：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(223, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 79;
            this.label7.Text = "价格：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 89;
            this.label8.Text = "库存：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(223, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 99;
            this.label9.Text = "规格：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 109;
            this.label10.Text = "编号：";
            // 
            // tbId
            // 
            this.tbId.Location = new System.Drawing.Point(270, 16);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(100, 21);
            this.tbId.TabIndex = 1;
            this.tbId.Visible = false;
            this.tbId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // tbColorNum
            // 
            this.tbColorNum.Location = new System.Drawing.Point(86, 45);
            this.tbColorNum.Name = "tbColorNum";
            this.tbColorNum.Size = new System.Drawing.Size(100, 21);
            this.tbColorNum.TabIndex = 2;
            this.tbColorNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(86, 74);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 21);
            this.tbName.TabIndex = 4;
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // tbInnerId
            // 
            this.tbInnerId.Location = new System.Drawing.Point(86, 16);
            this.tbInnerId.Name = "tbInnerId";
            this.tbInnerId.Size = new System.Drawing.Size(100, 21);
            this.tbInnerId.TabIndex = 0;
            this.tbInnerId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // tbSize
            // 
            this.tbSize.Location = new System.Drawing.Point(270, 45);
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(100, 21);
            this.tbSize.TabIndex = 3;
            this.tbSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // tbPosition
            // 
            this.tbPosition.Location = new System.Drawing.Point(270, 133);
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.Size = new System.Drawing.Size(100, 21);
            this.tbPosition.TabIndex = 9;
            this.tbPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // nudCost
            // 
            this.nudCost.DecimalPlaces = 2;
            this.nudCost.Location = new System.Drawing.Point(86, 104);
            this.nudCost.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudCost.Name = "nudCost";
            this.nudCost.Size = new System.Drawing.Size(100, 21);
            this.nudCost.TabIndex = 6;
            this.nudCost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // nudStore
            // 
            this.nudStore.Location = new System.Drawing.Point(270, 74);
            this.nudStore.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudStore.Name = "nudStore";
            this.nudStore.Size = new System.Drawing.Size(100, 21);
            this.nudStore.TabIndex = 5;
            this.nudStore.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // nudPrice
            // 
            this.nudPrice.DecimalPlaces = 2;
            this.nudPrice.Location = new System.Drawing.Point(270, 106);
            this.nudPrice.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(100, 21);
            this.nudPrice.TabIndex = 7;
            this.nudPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // nudPkgNum
            // 
            this.nudPkgNum.Location = new System.Drawing.Point(86, 133);
            this.nudPkgNum.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudPkgNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPkgNum.Name = "nudPkgNum";
            this.nudPkgNum.Size = new System.Drawing.Size(100, 21);
            this.nudPkgNum.TabIndex = 8;
            this.nudPkgNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPkgNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            // 
            // AddProductFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 190);
            this.Controls.Add(this.nudPkgNum);
            this.Controls.Add(this.nudPrice);
            this.Controls.Add(this.nudStore);
            this.Controls.Add(this.nudCost);
            this.Controls.Add(this.tbSize);
            this.Controls.Add(this.tbInnerId);
            this.Controls.Add(this.tbPosition);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbColorNum);
            this.Controls.Add(this.tbId);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddProductFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加新产品";
            ((System.ComponentModel.ISupportInitialize)(this.nudCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPkgNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.TextBox tbColorNum;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbInnerId;
        private System.Windows.Forms.TextBox tbSize;
        private System.Windows.Forms.TextBox tbPosition;
        private System.Windows.Forms.NumericUpDown nudCost;
        private System.Windows.Forms.NumericUpDown nudStore;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.NumericUpDown nudPkgNum;

    }
}