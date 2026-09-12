namespace TDSMAN.FormUtl
{
    partial class UtlExportData
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
            this.BtnExportData = new System.Windows.Forms.Button();
            this.BtnUnzip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnExportData
            // 
            this.BtnExportData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExportData.Location = new System.Drawing.Point(172, 291);
            this.BtnExportData.Name = "BtnExportData";
            this.BtnExportData.Size = new System.Drawing.Size(122, 34);
            this.BtnExportData.TabIndex = 0;
            this.BtnExportData.Text = "&Export";
            this.BtnExportData.UseVisualStyleBackColor = true;
            this.BtnExportData.Click += new System.EventHandler(this.BtnExportData_Click);
            // 
            // BtnUnzip
            // 
            this.BtnUnzip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUnzip.Location = new System.Drawing.Point(300, 291);
            this.BtnUnzip.Name = "BtnUnzip";
            this.BtnUnzip.Size = new System.Drawing.Size(122, 34);
            this.BtnUnzip.TabIndex = 0;
            this.BtnUnzip.Text = "&Unzip";
            this.BtnUnzip.UseVisualStyleBackColor = true;
            this.BtnUnzip.Click += new System.EventHandler(this.BtnUnzip_Click);
            // 
            // UtlExportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 434);
            this.Controls.Add(this.BtnUnzip);
            this.Controls.Add(this.BtnExportData);
            this.Name = "UtlExportData";
            this.Text = "Export Data";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnExportData;
        private System.Windows.Forms.Button BtnUnzip;
    }
}