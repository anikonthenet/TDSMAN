
namespace TDSMAN.FormSys
{
    partial class SysPopulateRates
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
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPopulateRates = new System.Windows.Forms.Button();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.Black;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(692, 3);
            this.lblSearchMode.Name = "lblSearchMode";
            this.lblSearchMode.Size = new System.Drawing.Size(105, 24);
            this.lblSearchMode.TabIndex = 48;
            this.lblSearchMode.Text = "General Mode";
            this.lblSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(109, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(582, 24);
            this.pnlTitle.TabIndex = 46;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(-1, -2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(582, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Populate Rates";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.Black;
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMode.Location = new System.Drawing.Point(3, 3);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(105, 24);
            this.lblMode.TabIndex = 47;
            this.lblMode.Text = "Rates";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Gray;
            this.pnlHeader.Location = new System.Drawing.Point(3, 33);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(794, 3);
            this.pnlHeader.TabIndex = 49;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnClose);
            this.pnlControls.Controls.Add(this.groupBox1);
            this.pnlControls.Location = new System.Drawing.Point(3, 42);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(794, 396);
            this.pnlControls.TabIndex = 50;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(618, 353);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnPopulateRates);
            this.groupBox1.Location = new System.Drawing.Point(106, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 328);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(6, 252);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 2);
            this.panel1.TabIndex = 15;
            // 
            // btnPopulateRates
            // 
            this.btnPopulateRates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnPopulateRates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPopulateRates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPopulateRates.ForeColor = System.Drawing.Color.Black;
            this.btnPopulateRates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPopulateRates.Location = new System.Drawing.Point(364, 264);
            this.btnPopulateRates.Name = "btnPopulateRates";
            this.btnPopulateRates.Size = new System.Drawing.Size(146, 31);
            this.btnPopulateRates.TabIndex = 14;
            this.btnPopulateRates.Text = "&Populate Rates";
            this.btnPopulateRates.UseVisualStyleBackColor = false;
            this.btnPopulateRates.Click += new System.EventHandler(this.btnPopulateRates_Click);
            // 
            // SysPopulateRates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SysPopulateRates";
            this.ShowIcon = false;
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblSearchMode;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Button btnPopulateRates;
        private System.Windows.Forms.Panel panel1;
    }
}