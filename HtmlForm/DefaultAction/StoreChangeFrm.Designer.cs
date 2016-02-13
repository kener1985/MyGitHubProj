namespace DefaultAction
{
    partial class StoreChangeFrm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.rdIn = new System.Windows.Forms.RadioButton();
            this.rdModify = new System.Windows.Forms.RadioButton();
            this.lbStore = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rdOut = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMark = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCustomer = new System.Windows.Forms.TextBox();
            this.tbNum = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(209, 76);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 26);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rdIn
            // 
            this.rdIn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.rdIn.AutoSize = true;
            this.rdIn.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdIn.Location = new System.Drawing.Point(80, 79);
            this.rdIn.Name = "rdIn";
            this.rdIn.Size = new System.Drawing.Size(58, 20);
            this.rdIn.TabIndex = 5;
            this.rdIn.Text = "入货";
            this.rdIn.UseVisualStyleBackColor = true;
            this.rdIn.Click += new System.EventHandler(this.radioBtn_Click);
            // 
            // rdModify
            // 
            this.rdModify.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.rdModify.AutoSize = true;
            this.rdModify.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdModify.Location = new System.Drawing.Point(146, 79);
            this.rdModify.Name = "rdModify";
            this.rdModify.Size = new System.Drawing.Size(58, 20);
            this.rdModify.TabIndex = 6;
            this.rdModify.Text = "修改";
            this.rdModify.UseVisualStyleBackColor = true;
            this.rdModify.Click += new System.EventHandler(this.radioBtn_Click);
            // 
            // lbStore
            // 
            this.lbStore.AutoSize = true;
            this.lbStore.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStore.Location = new System.Drawing.Point(5, 14);
            this.lbStore.Name = "lbStore";
            this.lbStore.Size = new System.Drawing.Size(48, 16);
            this.lbStore.TabIndex = 5;
            this.lbStore.Text = "数量:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(275, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 26);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // rbOut
            // 
            this.rdOut.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.rdOut.AutoSize = true;
            this.rdOut.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdOut.Location = new System.Drawing.Point(16, 79);
            this.rdOut.Name = "rbOut";
            this.rdOut.Size = new System.Drawing.Size(58, 20);
            this.rdOut.TabIndex = 4;
            this.rdOut.Text = "出货";
            this.rdOut.UseVisualStyleBackColor = true;
            this.rdOut.Click += new System.EventHandler(this.radioBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "备注:";
            // 
            // tbMark
            // 
            this.tbMark.Location = new System.Drawing.Point(55, 43);
            this.tbMark.MaxLength = 255;
            this.tbMark.Name = "tbMark";
            this.tbMark.Size = new System.Drawing.Size(284, 21);
            this.tbMark.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(152, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "客户:";
            // 
            // tbCustomer
            // 
            this.tbCustomer.Location = new System.Drawing.Point(196, 14);
            this.tbCustomer.MaxLength = 16;
            this.tbCustomer.Name = "tbCustomer";
            this.tbCustomer.Size = new System.Drawing.Size(143, 21);
            this.tbCustomer.TabIndex = 2;
            // 
            // tbNum
            // 
            this.tbNum.Location = new System.Drawing.Point(55, 14);
            this.tbNum.Name = "tbNum";
            this.tbNum.Size = new System.Drawing.Size(90, 21);
            this.tbNum.TabIndex = 1;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 1000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip.ToolTipTitle = "温馨提示";
            // 
            // StoreChangeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 115);
            this.Controls.Add(this.tbNum);
            this.Controls.Add(this.tbCustomer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMark);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdOut);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbStore);
            this.Controls.Add(this.rdModify);
            this.Controls.Add(this.rdIn);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StoreChangeFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "库存变更";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rdIn;
        private System.Windows.Forms.RadioButton rdModify;
        private System.Windows.Forms.Label lbStore;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rdOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCustomer;
        private System.Windows.Forms.TextBox tbNum;
        private System.Windows.Forms.ToolTip toolTip;
    }
}