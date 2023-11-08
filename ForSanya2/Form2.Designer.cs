namespace ForSanya2
{
    partial class Form2
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
            this.tbPass = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.pReg = new System.Windows.Forms.Panel();
            this.btnReg = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pLog = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pReg.SuspendLayout();
            this.pLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(208, 30);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(190, 20);
            this.tbPass.TabIndex = 3;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(12, 30);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(190, 20);
            this.tbUser.TabIndex = 2;
            // 
            // pReg
            // 
            this.pReg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pReg.Controls.Add(this.btnReg);
            this.pReg.Controls.Add(this.label4);
            this.pReg.Location = new System.Drawing.Point(155, 258);
            this.pReg.Name = "pReg";
            this.pReg.Size = new System.Drawing.Size(10, 10);
            this.pReg.TabIndex = 14;
            // 
            // btnReg
            // 
            this.btnReg.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnReg.Location = new System.Drawing.Point(14, 3);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(183, 53);
            this.btnReg.TabIndex = 0;
            this.btnReg.Text = "Register";
            this.btnReg.UseVisualStyleBackColor = false;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(56, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Уже есть аккаунт?";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // pLog
            // 
            this.pLog.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pLog.Controls.Add(this.label3);
            this.pLog.Location = new System.Drawing.Point(155, 258);
            this.pLog.Name = "pLog";
            this.pLog.Size = new System.Drawing.Size(10, 10);
            this.pLog.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(66, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Нет аккаунта?";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnLog
            // 
            this.btnLog.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLog.Location = new System.Drawing.Point(119, 65);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(183, 53);
            this.btnLog.TabIndex = 1;
            this.btnLog.Text = "Войти";
            this.btnLog.UseVisualStyleBackColor = false;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Пароль";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(419, 130);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.pLog);
            this.Controls.Add(this.pReg);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.pReg.ResumeLayout(false);
            this.pReg.PerformLayout();
            this.pLog.ResumeLayout(false);
            this.pLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Panel pReg;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}