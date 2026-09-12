namespace TDSMAN.FormTrn
{
    partial class TrnCertificateExtraction
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
            this.txtZIPFilePath = new System.Windows.Forms.TextBox();
            this.btnGetZipFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetOutputFolderPath = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.lblWait = new System.Windows.Forms.Label();
            this.grpSelectCertificates = new System.Windows.Forms.GroupBox();
            this.rbnFrom16PartB = new System.Windows.Forms.RadioButton();
            this.rbnFrom1616A = new System.Windows.Forms.RadioButton();
            this.lblMessage = new System.Windows.Forms.Label();
            this.bgCertificateExtraction = new System.ComponentModel.BackgroundWorker();
            this.bgReadingTextFile = new System.ComponentModel.BackgroundWorker();
            this.chkSignedPDF = new System.Windows.Forms.CheckBox();
            this.grpSelectCertificates.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtZIPFilePath
            // 
            this.txtZIPFilePath.BackColor = System.Drawing.Color.White;
            this.txtZIPFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZIPFilePath.Location = new System.Drawing.Point(131, 32);
            this.txtZIPFilePath.Name = "txtZIPFilePath";
            this.txtZIPFilePath.ReadOnly = true;
            this.txtZIPFilePath.Size = new System.Drawing.Size(243, 20);
            this.txtZIPFilePath.TabIndex = 0;
            // 
            // btnGetZipFile
            // 
            this.btnGetZipFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetZipFile.Location = new System.Drawing.Point(375, 31);
            this.btnGetZipFile.Name = "btnGetZipFile";
            this.btnGetZipFile.Size = new System.Drawing.Size(29, 21);
            this.btnGetZipFile.TabIndex = 0;
            this.btnGetZipFile.Text = "...";
            this.btnGetZipFile.UseVisualStyleBackColor = true;
            this.btnGetZipFile.Click += new System.EventHandler(this.BtnGetZipFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select ZIP File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Output Folder";
            // 
            // btnGetOutputFolderPath
            // 
            this.btnGetOutputFolderPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetOutputFolderPath.Location = new System.Drawing.Point(375, 77);
            this.btnGetOutputFolderPath.Name = "btnGetOutputFolderPath";
            this.btnGetOutputFolderPath.Size = new System.Drawing.Size(29, 21);
            this.btnGetOutputFolderPath.TabIndex = 2;
            this.btnGetOutputFolderPath.Text = "...";
            this.btnGetOutputFolderPath.UseVisualStyleBackColor = true;
            this.btnGetOutputFolderPath.Click += new System.EventHandler(this.BtnGetOutputFolderPath_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.BackColor = System.Drawing.Color.White;
            this.txtOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFolder.Location = new System.Drawing.Point(131, 78);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(243, 20);
            this.txtOutputFolder.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Enter Password";
            // 
            // txtPassword
            // 
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(131, 55);
            this.txtPassword.MaxLength = 10;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // btnExtract
            // 
            this.btnExtract.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExtract.Location = new System.Drawing.Point(290, 100);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(84, 21);
            this.btnExtract.TabIndex = 3;
            this.btnExtract.Text = "Extract";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.BtnExtract_Click);
            // 
            // lblWait
            // 
            this.lblWait.AutoSize = true;
            this.lblWait.ForeColor = System.Drawing.Color.Blue;
            this.lblWait.Location = new System.Drawing.Point(358, 104);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(38, 13);
            this.lblWait.TabIndex = 9;
            this.lblWait.Text = "Wait...";
            this.lblWait.Visible = false;
            // 
            // grpSelectCertificates
            // 
            this.grpSelectCertificates.Controls.Add(this.rbnFrom16PartB);
            this.grpSelectCertificates.Controls.Add(this.rbnFrom1616A);
            this.grpSelectCertificates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSelectCertificates.Location = new System.Drawing.Point(86, -3);
            this.grpSelectCertificates.Name = "grpSelectCertificates";
            this.grpSelectCertificates.Size = new System.Drawing.Size(227, 32);
            this.grpSelectCertificates.TabIndex = 10;
            this.grpSelectCertificates.TabStop = false;
            this.grpSelectCertificates.Visible = false;
            // 
            // rbnFrom16PartB
            // 
            this.rbnFrom16PartB.AutoSize = true;
            this.rbnFrom16PartB.Location = new System.Drawing.Point(113, 10);
            this.rbnFrom16PartB.Name = "rbnFrom16PartB";
            this.rbnFrom16PartB.Size = new System.Drawing.Size(109, 17);
            this.rbnFrom16PartB.TabIndex = 1;
            this.rbnFrom16PartB.TabStop = true;
            this.rbnFrom16PartB.Text = "Form 16 Part B";
            this.rbnFrom16PartB.UseVisualStyleBackColor = true;
            // 
            // rbnFrom1616A
            // 
            this.rbnFrom1616A.AutoSize = true;
            this.rbnFrom1616A.Location = new System.Drawing.Point(7, 10);
            this.rbnFrom1616A.Name = "rbnFrom1616A";
            this.rbnFrom1616A.Size = new System.Drawing.Size(98, 17);
            this.rbnFrom1616A.TabIndex = 0;
            this.rbnFrom1616A.TabStop = true;
            this.rbnFrom1616A.Text = "Form 16/16A";
            this.rbnFrom1616A.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblMessage.Location = new System.Drawing.Point(5, 123);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(255, 13);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "For signing the pdfs keep the DSC inserted.";
            this.lblMessage.Visible = false;
            // 
            // bgReadingTextFile
            // 
            this.bgReadingTextFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgReadingTextFile_DoWork);
            // 
            // chkSignedPDF
            // 
            this.chkSignedPDF.AutoSize = true;
            this.chkSignedPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSignedPDF.Location = new System.Drawing.Point(131, 103);
            this.chkSignedPDF.Name = "chkSignedPDF";
            this.chkSignedPDF.Size = new System.Drawing.Size(93, 17);
            this.chkSignedPDF.TabIndex = 12;
            this.chkSignedPDF.Text = "Signed PDF";
            this.chkSignedPDF.UseVisualStyleBackColor = true;
            // 
            // TrnCertificateExtraction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 125);
            this.Controls.Add(this.chkSignedPDF);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.grpSelectCertificates);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGetOutputFolderPath);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetZipFile);
            this.Controls.Add(this.txtZIPFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TrnCertificateExtraction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Certificate Extraction";
            this.grpSelectCertificates.ResumeLayout(false);
            this.grpSelectCertificates.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtZIPFilePath;
        private System.Windows.Forms.Button btnGetZipFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetOutputFolderPath;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.GroupBox grpSelectCertificates;
        private System.Windows.Forms.RadioButton rbnFrom1616A;
        private System.Windows.Forms.RadioButton rbnFrom16PartB;
        private System.Windows.Forms.Label lblMessage;
        private System.ComponentModel.BackgroundWorker bgCertificateExtraction;
        private System.ComponentModel.BackgroundWorker bgReadingTextFile;
        private System.Windows.Forms.CheckBox chkSignedPDF;
    }
}