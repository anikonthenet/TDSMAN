namespace TDSMAN.FormTrn
{
    partial class TrnInputMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnInputMessageBox));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOkPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPromptLine = new System.Windows.Forms.Label();
            this.chkBlankDate = new System.Windows.Forms.CheckBox();
            this.label52 = new System.Windows.Forms.Label();
            this.mskPrintDate = new System.Windows.Forms.MaskedTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 36);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnOkPrint
            // 
            this.btnOkPrint.Location = new System.Drawing.Point(152, 75);
            this.btnOkPrint.Name = "btnOkPrint";
            this.btnOkPrint.Size = new System.Drawing.Size(75, 23);
            this.btnOkPrint.TabIndex = 1;
            this.btnOkPrint.Text = "&Ok";
            this.btnOkPrint.UseVisualStyleBackColor = true;
            this.btnOkPrint.Click += new System.EventHandler(this.btnOkPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(228, 75);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPromptLine
            // 
            this.lblPromptLine.AutoSize = true;
            this.lblPromptLine.Location = new System.Drawing.Point(47, 15);
            this.lblPromptLine.Name = "lblPromptLine";
            this.lblPromptLine.Size = new System.Drawing.Size(91, 13);
            this.lblPromptLine.TabIndex = 3;
            this.lblPromptLine.Text = "Enter Print Date : ";
            // 
            // chkBlankDate
            // 
            this.chkBlankDate.AutoSize = true;
            this.chkBlankDate.Location = new System.Drawing.Point(4, 79);
            this.chkBlankDate.Name = "chkBlankDate";
            this.chkBlankDate.Size = new System.Drawing.Size(79, 17);
            this.chkBlankDate.TabIndex = 4;
            this.chkBlankDate.Text = "Blank Date";
            this.chkBlankDate.UseVisualStyleBackColor = true;
            this.chkBlankDate.CheckedChanged += new System.EventHandler(this.chkBlankDate_CheckedChanged);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Blue;
            this.label52.Location = new System.Drawing.Point(225, 15);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(77, 15);
            this.label52.TabIndex = 233;
            this.label52.Text = "DD/MM/YYYY";
            // 
            // mskPrintDate
            // 
            this.mskPrintDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskPrintDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskPrintDate.Location = new System.Drawing.Point(141, 12);
            this.mskPrintDate.Mask = "00/00/0000";
            this.mskPrintDate.Name = "mskPrintDate";
            this.mskPrintDate.Size = new System.Drawing.Size(80, 20);
            this.mskPrintDate.TabIndex = 232;
            this.mskPrintDate.ValidatingType = typeof(System.DateTime);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(47, 48);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(134, 13);
            this.lblMessage.TabIndex = 234;
            this.lblMessage.Text = "Do You Want to Print Form";
            // 
            // TrnInputMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 100);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label52);
            this.Controls.Add(this.mskPrintDate);
            this.Controls.Add(this.chkBlankDate);
            this.Controls.Add(this.lblPromptLine);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOkPrint);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnInputMessageBox";
            this.ShowIcon = false;
            this.Text = "TDSMAN";
            this.Load += new System.EventHandler(this.TrnInputMessageBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOkPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPromptLine;
        private System.Windows.Forms.CheckBox chkBlankDate;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.MaskedTextBox mskPrintDate;
        private System.Windows.Forms.Label lblMessage;
    }
}