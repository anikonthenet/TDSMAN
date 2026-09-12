namespace TDSMAN.FormUtl
{
    partial class UtlImportData
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
            this.BtnImportData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnImportData
            // 
            this.BtnImportData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImportData.Location = new System.Drawing.Point(238, 292);
            this.BtnImportData.Name = "BtnImportData";
            this.BtnImportData.Size = new System.Drawing.Size(183, 34);
            this.BtnImportData.TabIndex = 0;
            this.BtnImportData.Text = "Import Data";
            this.BtnImportData.UseVisualStyleBackColor = true;
            this.BtnImportData.Click += new System.EventHandler(this.BtnImportData_Click);
            // 
            // MstImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 434);
            this.Controls.Add(this.BtnImportData);
            this.Name = "MstImportData";
            this.Text = "MstImportData";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnImportData;
    }
}