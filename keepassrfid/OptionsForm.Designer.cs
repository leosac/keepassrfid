namespace KeePassRFID
{
    partial class OptionsForm
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
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.gpKeyType = new System.Windows.Forms.GroupBox();
            this.lblSecureID = new System.Windows.Forms.Label();
            this.linkWriteNFC = new System.Windows.Forms.LinkLabel();
            this.rbtnSecureID = new System.Windows.Forms.RadioButton();
            this.rbtnNFC = new System.Windows.Forms.RadioButton();
            this.rbtnCSN = new System.Windows.Forms.RadioButton();
            this.gpReaderConfig = new System.Windows.Forms.GroupBox();
            this.linkRefreshRU = new System.Windows.Forms.LinkLabel();
            this.cbReaderUnit = new System.Windows.Forms.ComboBox();
            this.lblReaderUnit = new System.Windows.Forms.Label();
            this.cbReaderProvider = new System.Windows.Forms.ComboBox();
            this.lblReaderProvider = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelBottom.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.gpKeyType.SuspendLayout();
            this.gpReaderConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnSave);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(142, 304);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(319, 30);
            this.panelBottom.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(161, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(80, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.imgLogo);
            this.panelLeft.Controls.Add(this.lblProductName);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(142, 334);
            this.panelLeft.TabIndex = 1;
            // 
            // imgLogo
            // 
            this.imgLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.imgLogo.Image = global::KeePassRFID.Properties.Resources.logo;
            this.imgLogo.Location = new System.Drawing.Point(0, 241);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(140, 91);
            this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgLogo.TabIndex = 2;
            this.imgLogo.TabStop = false;
            this.toolTip.SetToolTip(this.imgLogo, "Open www.islog.com");
            this.imgLogo.Click += new System.EventHandler(this.imgLogo_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.White;
            this.lblProductName.Location = new System.Drawing.Point(0, 0);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(140, 22);
            this.lblProductName.TabIndex = 2;
            this.lblProductName.Text = "KeePass RFID";
            // 
            // gpKeyType
            // 
            this.gpKeyType.Controls.Add(this.lblSecureID);
            this.gpKeyType.Controls.Add(this.linkWriteNFC);
            this.gpKeyType.Controls.Add(this.rbtnSecureID);
            this.gpKeyType.Controls.Add(this.rbtnNFC);
            this.gpKeyType.Controls.Add(this.rbtnCSN);
            this.gpKeyType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpKeyType.Location = new System.Drawing.Point(142, 75);
            this.gpKeyType.Name = "gpKeyType";
            this.gpKeyType.Size = new System.Drawing.Size(319, 229);
            this.gpKeyType.TabIndex = 2;
            this.gpKeyType.TabStop = false;
            this.gpKeyType.Text = "Key Type";
            // 
            // lblSecureID
            // 
            this.lblSecureID.AutoSize = true;
            this.lblSecureID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSecureID.Location = new System.Drawing.Point(78, 149);
            this.lblSecureID.Name = "lblSecureID";
            this.lblSecureID.Size = new System.Drawing.Size(175, 13);
            this.lblSecureID.TabIndex = 4;
            this.lblSecureID.Text = "Secure ID (3-way auth, SAM, HSM)";
            this.toolTip.SetToolTip(this.lblSecureID, "Contact for more information");
            this.lblSecureID.Click += new System.EventHandler(this.lblSecureID_Click);
            // 
            // linkWriteNFC
            // 
            this.linkWriteNFC.AutoSize = true;
            this.linkWriteNFC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkWriteNFC.Enabled = false;
            this.linkWriteNFC.Location = new System.Drawing.Point(137, 108);
            this.linkWriteNFC.Name = "linkWriteNFC";
            this.linkWriteNFC.Size = new System.Drawing.Size(91, 13);
            this.linkWriteNFC.TabIndex = 3;
            this.linkWriteNFC.TabStop = true;
            this.linkWriteNFC.Text = "Write (text record)";
            this.toolTip.SetToolTip(this.linkWriteNFC, "Write password to a NFC Tag");
            this.linkWriteNFC.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkWriteNFC_LinkClicked);
            // 
            // rbtnSecureID
            // 
            this.rbtnSecureID.AutoSize = true;
            this.rbtnSecureID.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rbtnSecureID.Enabled = false;
            this.rbtnSecureID.Location = new System.Drawing.Point(63, 149);
            this.rbtnSecureID.Name = "rbtnSecureID";
            this.rbtnSecureID.Size = new System.Drawing.Size(14, 13);
            this.rbtnSecureID.TabIndex = 2;
            this.rbtnSecureID.UseVisualStyleBackColor = true;
            // 
            // rbtnNFC
            // 
            this.rbtnNFC.AutoSize = true;
            this.rbtnNFC.Location = new System.Drawing.Point(63, 106);
            this.rbtnNFC.Name = "rbtnNFC";
            this.rbtnNFC.Size = new System.Drawing.Size(68, 17);
            this.rbtnNFC.TabIndex = 1;
            this.rbtnNFC.Text = "NFC Tag";
            this.rbtnNFC.UseVisualStyleBackColor = true;
            this.rbtnNFC.CheckedChanged += new System.EventHandler(this.rbtnNFC_CheckedChanged);
            // 
            // rbtnCSN
            // 
            this.rbtnCSN.AutoSize = true;
            this.rbtnCSN.Checked = true;
            this.rbtnCSN.Location = new System.Drawing.Point(63, 63);
            this.rbtnCSN.Name = "rbtnCSN";
            this.rbtnCSN.Size = new System.Drawing.Size(115, 17);
            this.rbtnCSN.TabIndex = 0;
            this.rbtnCSN.TabStop = true;
            this.rbtnCSN.Text = "Chip Serial Number";
            this.rbtnCSN.UseVisualStyleBackColor = true;
            // 
            // gpReaderConfig
            // 
            this.gpReaderConfig.Controls.Add(this.linkRefreshRU);
            this.gpReaderConfig.Controls.Add(this.cbReaderUnit);
            this.gpReaderConfig.Controls.Add(this.lblReaderUnit);
            this.gpReaderConfig.Controls.Add(this.cbReaderProvider);
            this.gpReaderConfig.Controls.Add(this.lblReaderProvider);
            this.gpReaderConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpReaderConfig.Location = new System.Drawing.Point(142, 0);
            this.gpReaderConfig.Name = "gpReaderConfig";
            this.gpReaderConfig.Size = new System.Drawing.Size(319, 75);
            this.gpReaderConfig.TabIndex = 3;
            this.gpReaderConfig.TabStop = false;
            this.gpReaderConfig.Text = "Reader Configuration";
            // 
            // linkRefreshRU
            // 
            this.linkRefreshRU.AutoSize = true;
            this.linkRefreshRU.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkRefreshRU.Location = new System.Drawing.Point(259, 49);
            this.linkRefreshRU.Name = "linkRefreshRU";
            this.linkRefreshRU.Size = new System.Drawing.Size(39, 13);
            this.linkRefreshRU.TabIndex = 4;
            this.linkRefreshRU.TabStop = true;
            this.linkRefreshRU.Text = "refresh";
            this.toolTip.SetToolTip(this.linkRefreshRU, "Refresh reader unit list");
            this.linkRefreshRU.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRefreshRU_LinkClicked);
            // 
            // cbReaderUnit
            // 
            this.cbReaderUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReaderUnit.DropDownWidth = 220;
            this.cbReaderUnit.FormattingEnabled = true;
            this.cbReaderUnit.Location = new System.Drawing.Point(99, 46);
            this.cbReaderUnit.Name = "cbReaderUnit";
            this.cbReaderUnit.Size = new System.Drawing.Size(154, 21);
            this.cbReaderUnit.TabIndex = 3;
            // 
            // lblReaderUnit
            // 
            this.lblReaderUnit.AutoSize = true;
            this.lblReaderUnit.Location = new System.Drawing.Point(26, 49);
            this.lblReaderUnit.Name = "lblReaderUnit";
            this.lblReaderUnit.Size = new System.Drawing.Size(67, 13);
            this.lblReaderUnit.TabIndex = 2;
            this.lblReaderUnit.Text = "Reader Unit:";
            // 
            // cbReaderProvider
            // 
            this.cbReaderProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReaderProvider.FormattingEnabled = true;
            this.cbReaderProvider.Location = new System.Drawing.Point(99, 19);
            this.cbReaderProvider.Name = "cbReaderProvider";
            this.cbReaderProvider.Size = new System.Drawing.Size(154, 21);
            this.cbReaderProvider.TabIndex = 1;
            this.cbReaderProvider.SelectedIndexChanged += new System.EventHandler(this.cbReaderProvider_SelectedIndexChanged);
            // 
            // lblReaderProvider
            // 
            this.lblReaderProvider.AutoSize = true;
            this.lblReaderProvider.Location = new System.Drawing.Point(6, 22);
            this.lblReaderProvider.Name = "lblReaderProvider";
            this.lblReaderProvider.Size = new System.Drawing.Size(87, 13);
            this.lblReaderProvider.TabIndex = 0;
            this.lblReaderProvider.Text = "Reader Provider:";
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(461, 334);
            this.Controls.Add(this.gpKeyType);
            this.Controls.Add(this.gpReaderConfig);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.Text = "KeePass RFID Options";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.panelBottom.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.gpKeyType.ResumeLayout(false);
            this.gpKeyType.PerformLayout();
            this.gpReaderConfig.ResumeLayout(false);
            this.gpReaderConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.GroupBox gpKeyType;
        private System.Windows.Forms.GroupBox gpReaderConfig;
        private System.Windows.Forms.Label lblReaderProvider;
        private System.Windows.Forms.ComboBox cbReaderProvider;
        private System.Windows.Forms.ComboBox cbReaderUnit;
        private System.Windows.Forms.Label lblReaderUnit;
        private System.Windows.Forms.LinkLabel linkRefreshRU;
        private System.Windows.Forms.RadioButton rbtnCSN;
        private System.Windows.Forms.RadioButton rbtnNFC;
        private System.Windows.Forms.RadioButton rbtnSecureID;
        private System.Windows.Forms.LinkLabel linkWriteNFC;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblSecureID;
    }
}