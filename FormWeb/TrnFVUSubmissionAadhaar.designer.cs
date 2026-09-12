namespace TDSMAN.FormTrn
{
    partial class TrnFVUSubmissionAadhaar
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
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pgTimer = new System.Windows.Forms.Timer(this.components);
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkIncomeTax = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 666);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(39, 13);
            this.BtnCancel.Size = new System.Drawing.Size(10, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.Location = new System.Drawing.Point(102, 12);
            this.BtnSave.Size = new System.Drawing.Size(19, 23);
            this.BtnSave.Text = "&Submit";
            this.BtnSave.Visible = false;
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 658);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(23, 13);
            this.BtnEdit.Size = new System.Drawing.Size(10, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(6, 13);
            this.BtnAdd.Size = new System.Drawing.Size(11, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.Location = new System.Drawing.Point(460, 13);
            this.BtnExit.TabIndex = 0;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(86, 13);
            this.BtnRefresh.Size = new System.Drawing.Size(10, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(70, 13);
            this.BtnDelete.Size = new System.Drawing.Size(10, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(54, 13);
            this.BtnSearch.Size = new System.Drawing.Size(10, 23);
            this.BtnSearch.Visible = false;
            // 
            // grpButton
            // 
            this.grpButton.Location = new System.Drawing.Point(9, 599);
            // 
            // lblMode
            // 
            this.lblMode.Text = "View Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.pBar);
            this.pnlControls.Controls.Add(this.grpDetails);
            this.pnlControls.Size = new System.Drawing.Size(1036, 609);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // pgTimer
            // 
            this.pgTimer.Tick += new System.EventHandler(this.pgTimer_Tick);
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.panel2);
            this.grpDetails.Controls.Add(this.panel1);
            this.grpDetails.Controls.Add(this.label8);
            this.grpDetails.Controls.Add(this.label7);
            this.grpDetails.Controls.Add(this.label6);
            this.grpDetails.Controls.Add(this.label5);
            this.grpDetails.Controls.Add(this.label4);
            this.grpDetails.Controls.Add(this.label3);
            this.grpDetails.Controls.Add(this.label2);
            this.grpDetails.Controls.Add(this.lnkIncomeTax);
            this.grpDetails.Controls.Add(this.label1);
            this.grpDetails.Location = new System.Drawing.Point(93, 17);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(851, 480);
            this.grpDetails.TabIndex = 226;
            this.grpDetails.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(39, 134);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(773, 1);
            this.panel2.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(98, 394);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(654, 3);
            this.panel1.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(52, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(747, 21);
            this.label8.TabIndex = 8;
            this.label8.Text = "6. Now proceed with filling up the form and attaching the FVU file and follow ins" +
    "tructions.";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(52, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(747, 21);
            this.label7.TabIndex = 7;
            this.label7.Text = "5. Click in ‘Let’s Get Started’,";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(51, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(747, 21);
            this.label6.TabIndex = 6;
            this.label6.Text = "4. From the multiple options, click ‘File Now’ under ‘Deduction of Tax at Source " +
    "(Form TDS)’,";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(51, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(747, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "3. From the Menu, select ‘e-File > Income Tax Forms > File Income Tax Forms’,";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(51, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(747, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "2. Check on the Box to confirm Secure Access and enter the Password and click Con" +
    "tinue,";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(51, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(747, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "1. Enter TAN as the User ID and click Continue,";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(35, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Guidelines Steps:";
            // 
            // lnkIncomeTax
            // 
            this.lnkIncomeTax.AutoSize = true;
            this.lnkIncomeTax.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkIncomeTax.Location = new System.Drawing.Point(35, 97);
            this.lnkIncomeTax.Name = "lnkIncomeTax";
            this.lnkIncomeTax.Size = new System.Drawing.Size(200, 17);
            this.lnkIncomeTax.TabIndex = 1;
            this.lnkIncomeTax.TabStop = true;
            this.lnkIncomeTax.Text = "https://www.incometax.gov.in/";
            this.lnkIncomeTax.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkIncomeTax_LinkClicked);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(35, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(798, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "The Income Tax Department has recently modified the online filing of TDS Returns." +
    " Click on the link below to file the TDS Returns either through Adahaar authenti" +
    "cation or DSC.";
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(131, 516);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(640, 9);
            this.pBar.TabIndex = 227;
            this.pBar.Visible = false;
            // 
            // TrnFVUSubmissionAadhaar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1045, 575);
            this.Name = "TrnFVUSubmissionAadhaar";
            this.Load += new System.EventHandler(this.TrnPanVarification_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer pgTimer;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkIncomeTax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
