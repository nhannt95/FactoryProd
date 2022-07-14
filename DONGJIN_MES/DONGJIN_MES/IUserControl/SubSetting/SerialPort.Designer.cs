namespace DONGJIN_MES.IUserControl.SubSetting
{
    partial class SerialPort
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgw = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtDevices = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dtCom = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dtDataBits = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dtBaud = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dtParity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dtStopBit = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dtPrefix = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dtSuffix = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new Sunny.UI.UISymbolButton();
            this.btnSave = new Sunny.UI.UISymbolButton();
            this.btnDelete = new Sunny.UI.UISymbolButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtgw)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial Port Info";
            // 
            // dtgw
            // 
            this.dtgw.AllowUserToAddRows = false;
            this.dtgw.AllowUserToDeleteRows = false;
            this.dtgw.AllowUserToResizeColumns = false;
            this.dtgw.AllowUserToResizeRows = false;
            this.dtgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgw.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgw.ColumnHeadersHeight = 29;
            this.dtgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.dtDevices,
            this.dtCom,
            this.dtDataBits,
            this.dtBaud,
            this.dtParity,
            this.dtStopBit,
            this.dtPrefix,
            this.dtSuffix,
            this.Column10});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgw.EnableHeadersVisualStyles = false;
            this.dtgw.Location = new System.Drawing.Point(11, 47);
            this.dtgw.Margin = new System.Windows.Forms.Padding(4);
            this.dtgw.Name = "dtgw";
            this.dtgw.RowHeadersVisible = false;
            this.dtgw.RowHeadersWidth = 51;
            this.dtgw.Size = new System.Drawing.Size(1039, 138);
            this.dtgw.TabIndex = 1;
            this.dtgw.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgw_CellContentClick);
            this.dtgw.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgw_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 25F;
            this.Column1.HeaderText = "";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // dtDevices
            // 
            this.dtDevices.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.dtDevices.FillWeight = 94.19064F;
            this.dtDevices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dtDevices.HeaderText = "Device";
            this.dtDevices.MinimumWidth = 6;
            this.dtDevices.Name = "dtDevices";
            // 
            // dtCom
            // 
            this.dtCom.FillWeight = 94.19064F;
            this.dtCom.HeaderText = "COM Port";
            this.dtCom.MinimumWidth = 6;
            this.dtCom.Name = "dtCom";
            // 
            // dtDataBits
            // 
            this.dtDataBits.FillWeight = 94.19064F;
            this.dtDataBits.HeaderText = "Data Bits";
            this.dtDataBits.Items.AddRange(new object[] {
            "8"});
            this.dtDataBits.MinimumWidth = 6;
            this.dtDataBits.Name = "dtDataBits";
            // 
            // dtBaud
            // 
            this.dtBaud.FillWeight = 94.19064F;
            this.dtBaud.HeaderText = "Baud Rate";
            this.dtBaud.Items.AddRange(new object[] {
            "9600",
            "19200",
            "115200"});
            this.dtBaud.MinimumWidth = 6;
            this.dtBaud.Name = "dtBaud";
            // 
            // dtParity
            // 
            this.dtParity.FillWeight = 94.19064F;
            this.dtParity.HeaderText = "Parity Bit";
            this.dtParity.Items.AddRange(new object[] {
            "None"});
            this.dtParity.MinimumWidth = 6;
            this.dtParity.Name = "dtParity";
            // 
            // dtStopBit
            // 
            this.dtStopBit.FillWeight = 94.19064F;
            this.dtStopBit.HeaderText = "Stop Bit";
            this.dtStopBit.Items.AddRange(new object[] {
            "1"});
            this.dtStopBit.MinimumWidth = 6;
            this.dtStopBit.Name = "dtStopBit";
            // 
            // dtPrefix
            // 
            this.dtPrefix.FillWeight = 94.19064F;
            this.dtPrefix.HeaderText = "Prefix";
            this.dtPrefix.Items.AddRange(new object[] {
            "STX"});
            this.dtPrefix.MinimumWidth = 6;
            this.dtPrefix.Name = "dtPrefix";
            // 
            // dtSuffix
            // 
            this.dtSuffix.FillWeight = 94.19064F;
            this.dtSuffix.HeaderText = "Suffix";
            this.dtSuffix.Items.AddRange(new object[] {
            "ETX"});
            this.dtSuffix.MinimumWidth = 6;
            this.dtSuffix.Name = "dtSuffix";
            // 
            // Column10
            // 
            this.Column10.FillWeight = 94.19064F;
            this.Column10.HeaderText = "Remark";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(307, 187);
            this.btnAdd.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.btnAdd.Size = new System.Drawing.Size(116, 35);
            this.btnAdd.Style = Sunny.UI.UIStyle.Custom;
            this.btnAdd.Symbol = 61543;
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "Add";
            this.btnAdd.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(497, 187);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.btnSave.Size = new System.Drawing.Size(116, 35);
            this.btnSave.Style = Sunny.UI.UIStyle.Custom;
            this.btnSave.Symbol = 61639;
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDelete.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDelete.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnDelete.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDelete.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(667, 187);
            this.btnDelete.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDelete.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnDelete.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDelete.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDelete.Size = new System.Drawing.Size(116, 35);
            this.btnDelete.Style = Sunny.UI.UIStyle.Red;
            this.btnDelete.Symbol = 61453;
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            this.btnDelete.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // SerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtgw);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SerialPort";
            this.Size = new System.Drawing.Size(1053, 224);
            this.Load += new System.EventHandler(this.SerialPort_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgw;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dtDevices;
        private System.Windows.Forms.DataGridViewComboBoxColumn dtCom;
        private System.Windows.Forms.DataGridViewComboBoxColumn dtDataBits;
        private System.Windows.Forms.DataGridViewComboBoxColumn dtBaud;
        private System.Windows.Forms.DataGridViewComboBoxColumn dtParity;
        private System.Windows.Forms.DataGridViewComboBoxColumn dtStopBit;
        private System.Windows.Forms.DataGridViewComboBoxColumn dtPrefix;
        private System.Windows.Forms.DataGridViewComboBoxColumn dtSuffix;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private Sunny.UI.UISymbolButton btnAdd;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UISymbolButton btnDelete;
    }
}
