namespace TDSMAN.FormSys
{
    partial class SysServerInfoLocal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysServerInfoLocal));
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.txtSQLDatabaseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabaseUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDatabasePassword = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUpdtDate = new System.Windows.Forms.Label();
            this.lblHidePassword = new System.Windows.Forms.Label();
            this.lblShowPassword = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtServerName
            // 
            this.txtServerName.BackColor = System.Drawing.Color.White;
            this.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerName.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtServerName.ForeColor = System.Drawing.Color.Blue;
            this.txtServerName.Location = new System.Drawing.Point(131, 30);
            this.txtServerName.MaxLength = 0;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(283, 21);
            this.txtServerName.TabIndex = 0;
            this.txtServerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.Label1.ForeColor = System.Drawing.Color.GhostWhite;
            this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label1.Location = new System.Drawing.Point(129, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(186, 19);
            this.Label1.TabIndex = 32;
            this.Label1.Text = "Server Machine Name";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSubmit.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.BtnSubmit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnSubmit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnSubmit.Location = new System.Drawing.Point(197, 180);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(108, 23);
            this.BtnSubmit.TabIndex = 4;
            this.BtnSubmit.Text = "&Submit";
            this.BtnSubmit.UseVisualStyleBackColor = false;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnCancel.Location = new System.Drawing.Point(306, 180);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(108, 23);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabaseName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDatabaseName.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.lblDatabaseName.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblDatabaseName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDatabaseName.Location = new System.Drawing.Point(129, 127);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(186, 19);
            this.lblDatabaseName.TabIndex = 32;
            this.lblDatabaseName.Text = "Database Name";
            this.lblDatabaseName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSQLDatabaseName
            // 
            this.txtSQLDatabaseName.BackColor = System.Drawing.SystemColors.Control;
            this.txtSQLDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLDatabaseName.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtSQLDatabaseName.ForeColor = System.Drawing.Color.Blue;
            this.txtSQLDatabaseName.Location = new System.Drawing.Point(131, 145);
            this.txtSQLDatabaseName.MaxLength = 0;
            this.txtSQLDatabaseName.Name = "txtSQLDatabaseName";
            this.txtSQLDatabaseName.Size = new System.Drawing.Size(283, 21);
            this.txtSQLDatabaseName.TabIndex = 3;
            this.txtSQLDatabaseName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.GhostWhite;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(129, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 19);
            this.label3.TabIndex = 32;
            this.label3.Text = "User Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDatabaseUserName
            // 
            this.txtDatabaseUserName.BackColor = System.Drawing.Color.White;
            this.txtDatabaseUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabaseUserName.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtDatabaseUserName.ForeColor = System.Drawing.Color.Blue;
            this.txtDatabaseUserName.Location = new System.Drawing.Point(131, 68);
            this.txtDatabaseUserName.MaxLength = 0;
            this.txtDatabaseUserName.Name = "txtDatabaseUserName";
            this.txtDatabaseUserName.Size = new System.Drawing.Size(283, 21);
            this.txtDatabaseUserName.TabIndex = 1;
            this.txtDatabaseUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.GhostWhite;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(129, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 19);
            this.label4.TabIndex = 32;
            this.label4.Text = "Password";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDatabasePassword
            // 
            this.txtDatabasePassword.BackColor = System.Drawing.Color.White;
            this.txtDatabasePassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabasePassword.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtDatabasePassword.ForeColor = System.Drawing.Color.Blue;
            this.txtDatabasePassword.Location = new System.Drawing.Point(131, 106);
            this.txtDatabasePassword.MaxLength = 0;
            this.txtDatabasePassword.Name = "txtDatabasePassword";
            this.txtDatabasePassword.PasswordChar = '*';
            this.txtDatabasePassword.Size = new System.Drawing.Size(261, 21);
            this.txtDatabasePassword.TabIndex = 2;
            this.txtDatabasePassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(2, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 154);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // lblUpdtDate
            // 
            this.lblUpdtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdtDate.ForeColor = System.Drawing.Color.DarkGray;
            this.lblUpdtDate.Location = new System.Drawing.Point(2, 196);
            this.lblUpdtDate.Name = "lblUpdtDate";
            this.lblUpdtDate.Size = new System.Drawing.Size(192, 23);
            this.lblUpdtDate.TabIndex = 33;
            this.lblUpdtDate.Text = "Version date :";
            this.lblUpdtDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHidePassword
            // 
            this.lblHidePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblHidePassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHidePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHidePassword.Image = ((System.Drawing.Image)(resources.GetObject("lblHidePassword.Image")));
            this.lblHidePassword.Location = new System.Drawing.Point(391, 106);
            this.lblHidePassword.Name = "lblHidePassword";
            this.lblHidePassword.Size = new System.Drawing.Size(23, 21);
            this.lblHidePassword.TabIndex = 34;
            this.lblHidePassword.Tag = "";
            this.lblHidePassword.Click += new System.EventHandler(this.lblHidePassword_Click);
            // 
            // lblShowPassword
            // 
            this.lblShowPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblShowPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowPassword.Image = ((System.Drawing.Image)(resources.GetObject("lblShowPassword.Image")));
            this.lblShowPassword.Location = new System.Drawing.Point(391, 106);
            this.lblShowPassword.Name = "lblShowPassword";
            this.lblShowPassword.Size = new System.Drawing.Size(23, 21);
            this.lblShowPassword.TabIndex = 35;
            this.lblShowPassword.Tag = "HIDE_PASSWORD";
            this.lblShowPassword.Click += new System.EventHandler(this.lblShowPassword_Click);
            // 
            // SysServerInfoLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(428, 221);
            this.Controls.Add(this.lblHidePassword);
            this.Controls.Add(this.lblShowPassword);
            this.Controls.Add(this.lblUpdtDate);
            this.Controls.Add(this.txtDatabasePassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDatabaseUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSQLDatabaseName);
            this.Controls.Add(this.lblDatabaseName);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SysServerInfoLocal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config Info";
            this.Load += new System.EventHandler(this.SysServerInfoLocal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.TextBox txtServerName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button BtnSubmit;
        internal System.Windows.Forms.Button BtnCancel;
        internal System.Windows.Forms.Label lblDatabaseName;
        internal System.Windows.Forms.TextBox txtSQLDatabaseName;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtDatabaseUserName;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtDatabasePassword;
        private System.Windows.Forms.Label lblUpdtDate;
        private System.Windows.Forms.Label lblHidePassword;
        private System.Windows.Forms.Label lblShowPassword;
    }
}