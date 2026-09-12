namespace TDSMAN.FormSys
{
    partial class SysUserRights
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysUserRights));
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.grpButton = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnGrant = new System.Windows.Forms.Button();
            this.cmbUserId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flxgrdDetails = new AxMSHierarchicalFlexGridLib.AxMSHFlexGrid();
            this.chkGrant = new System.Windows.Forms.CheckBox();
            this.chkGrantDeselect = new System.Windows.Forms.CheckBox();
            this.pnlTitle.SuspendLayout();
            this.grpButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.BackColor = System.Drawing.Color.DarkGray;
            this.lblSearchMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchMode.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearchMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblSearchMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchMode.Location = new System.Drawing.Point(811, 17);
            this.lblSearchMode.Name = "lblSearchMode";
            this.lblSearchMode.Size = new System.Drawing.Size(132, 24);
            this.lblSearchMode.TabIndex = 51;
            this.lblSearchMode.Text = "General Mode";
            this.lblSearchMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(217, 17);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(595, 24);
            this.pnlTitle.TabIndex = 49;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(-1, -2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(595, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.DarkGray;
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.lblMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMode.Location = new System.Drawing.Point(86, 17);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(132, 24);
            this.lblMode.TabIndex = 50;
            this.lblMode.Text = "Add Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.Gray;
            this.pnlFooter.Location = new System.Drawing.Point(3, 608);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1011, 3);
            this.pnlFooter.TabIndex = 53;
            // 
            // grpButton
            // 
            this.grpButton.BackColor = System.Drawing.Color.Gainsboro;
            this.grpButton.Controls.Add(this.BtnCancel);
            this.grpButton.Controls.Add(this.BtnGrant);
            this.grpButton.Location = new System.Drawing.Point(377, 617);
            this.grpButton.Name = "grpButton";
            this.grpButton.Size = new System.Drawing.Size(274, 48);
            this.grpButton.TabIndex = 52;
            this.grpButton.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnCancel.Location = new System.Drawing.Point(136, 16);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(92, 23);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnGrant
            // 
            this.BtnGrant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnGrant.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGrant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGrant.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.BtnGrant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnGrant.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnGrant.Location = new System.Drawing.Point(42, 16);
            this.BtnGrant.Name = "BtnGrant";
            this.BtnGrant.Size = new System.Drawing.Size(92, 23);
            this.BtnGrant.TabIndex = 0;
            this.BtnGrant.Text = "&Grant";
            this.BtnGrant.UseVisualStyleBackColor = false;
            this.BtnGrant.Click += new System.EventHandler(this.BtnGrant_Click);
            // 
            // cmbUserId
            // 
            this.cmbUserId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cmbUserId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserId.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUserId.FormattingEnabled = true;
            this.cmbUserId.Location = new System.Drawing.Point(302, 64);
            this.cmbUserId.Name = "cmbUserId";
            this.cmbUserId.Size = new System.Drawing.Size(404, 23);
            this.cmbUserId.TabIndex = 189;
            this.cmbUserId.SelectedIndexChanged += new System.EventHandler(this.cmbUserId_SelectedIndexChanged);
            this.cmbUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUserId_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(218, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 190;
            this.label6.Text = "Login Id";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(4, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1011, 3);
            this.panel1.TabIndex = 193;
            // 
            // flxgrdDetails
            // 
            this.flxgrdDetails.DataSource = null;
            this.flxgrdDetails.Location = new System.Drawing.Point(13, 102);
            this.flxgrdDetails.Name = "flxgrdDetails";
            this.flxgrdDetails.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flxgrdDetails.OcxState")));
            this.flxgrdDetails.Size = new System.Drawing.Size(1002, 499);
            this.flxgrdDetails.TabIndex = 194;
            this.flxgrdDetails.TabStop = false;
            this.flxgrdDetails.MouseMoveEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEventHandler(this.flxgrdDetails_MouseMoveEvent);
            this.flxgrdDetails.KeyDownEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyDownEventHandler(this.flxgrdDetails_KeyDownEvent);
            this.flxgrdDetails.ClickEvent += new System.EventHandler(this.flxgrdDetails_ClickEvent);
            // 
            // chkGrant
            // 
            this.chkGrant.AutoSize = true;
            this.chkGrant.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkGrant.Location = new System.Drawing.Point(377, 179);
            this.chkGrant.Name = "chkGrant";
            this.chkGrant.Size = new System.Drawing.Size(15, 14);
            this.chkGrant.TabIndex = 195;
            this.chkGrant.UseVisualStyleBackColor = true;
            this.chkGrant.Visible = false;
            this.chkGrant.Leave += new System.EventHandler(this.chkGrant_Leave);
            this.chkGrant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkGrant_KeyPress);
            this.chkGrant.CheckedChanged += new System.EventHandler(this.chkGrant_CheckedChanged);
            // 
            // chkGrantDeselect
            // 
            this.chkGrantDeselect.AutoSize = true;
            this.chkGrantDeselect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkGrantDeselect.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGrantDeselect.Location = new System.Drawing.Point(38, 105);
            this.chkGrantDeselect.Name = "chkGrantDeselect";
            this.chkGrantDeselect.Size = new System.Drawing.Size(15, 14);
            this.chkGrantDeselect.TabIndex = 196;
            this.chkGrantDeselect.UseVisualStyleBackColor = true;
            this.chkGrantDeselect.Leave += new System.EventHandler(this.chkGrantDeselect_Leave);
            this.chkGrantDeselect.CheckedChanged += new System.EventHandler(this.chkGrantDeselect_CheckedChanged);
            // 
            // SysUserRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1028, 683);
            this.Controls.Add(this.chkGrantDeselect);
            this.Controls.Add(this.chkGrant);
            this.Controls.Add(this.flxgrdDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbUserId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.grpButton);
            this.Controls.Add(this.lblSearchMode);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lblMode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SysUserRights";
            this.Text = "SysUserRights";
            this.Activated += new System.EventHandler(this.SysUserRights_Activated);
            this.pnlTitle.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblSearchMode;
        public System.Windows.Forms.Panel pnlTitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMode;
        public System.Windows.Forms.Panel pnlFooter;
        public System.Windows.Forms.GroupBox grpButton;
        public System.Windows.Forms.Button BtnGrant;
        public System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.ComboBox cmbUserId;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Panel panel1;
        private AxMSHierarchicalFlexGridLib.AxMSHFlexGrid flxgrdDetails;
        private System.Windows.Forms.CheckBox chkGrant;
        private System.Windows.Forms.CheckBox chkGrantDeselect;
    }
}