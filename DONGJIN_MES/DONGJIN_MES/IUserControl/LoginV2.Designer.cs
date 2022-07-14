namespace DONGJIN_MES.IUserControl
{
    partial class LoginV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginV2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelShadow1 = new DONGJIN_MES.Custom.PanelShadow();
            this.btnUpdate = new Sunny.UI.UISymbolButton();
            this.lbDatabase = new Sunny.UI.UILinkLabel();
            this.btnLogin = new Sunny.UI.UIButton();
            this.btnExit = new Sunny.UI.UIButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.txtUser = new Sunny.UI.UITextBox();
            this.cbbLanguage = new Sunny.UI.UIComboBox();
            this.txtPass = new Sunny.UI.UITextBox();
            this.cbRemember = new Sunny.UI.UICheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel3.SuspendLayout();
            this.panelShadow1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.uiLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 49);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DONGJIN_MES.Properties.Resources.logo_8f77d0ce;
            this.pictureBox2.Location = new System.Drawing.Point(3, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel1.ForeColor = System.Drawing.Color.Navy;
            this.uiLabel1.Location = new System.Drawing.Point(50, 9);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(237, 34);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "DONGJIN MES";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(179)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 763);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1200, 37);
            this.panel2.TabIndex = 3;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox3.Image = global::DONGJIN_MES.Properties.Resources.DTHAUS_LOGO_white;
            this.pictureBox3.Location = new System.Drawing.Point(1112, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(88, 37);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::DONGJIN_MES.Properties.Resources.logo3;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.panelShadow1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 49);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1200, 714);
            this.panel3.TabIndex = 5;
            // 
            // panelShadow1
            // 
            this.panelShadow1.BackColor = System.Drawing.SystemColors.Control;
            this.panelShadow1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelShadow1.Controls.Add(this.btnUpdate);
            this.panelShadow1.Controls.Add(this.lbDatabase);
            this.panelShadow1.Controls.Add(this.btnLogin);
            this.panelShadow1.Controls.Add(this.btnExit);
            this.panelShadow1.Controls.Add(this.uiLabel2);
            this.panelShadow1.Controls.Add(this.txtUser);
            this.panelShadow1.Controls.Add(this.cbbLanguage);
            this.panelShadow1.Controls.Add(this.txtPass);
            this.panelShadow1.Controls.Add(this.cbRemember);
            this.panelShadow1.Location = new System.Drawing.Point(471, 211);
            this.panelShadow1.Name = "panelShadow1";
            this.panelShadow1.Size = new System.Drawing.Size(334, 371);
            this.panelShadow1.TabIndex = 4;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FillColor = System.Drawing.Color.Transparent;
            this.btnUpdate.FillDisableColor = System.Drawing.Color.Gray;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnUpdate.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.ForeSelectedColor = System.Drawing.Color.Gray;
            this.btnUpdate.Location = new System.Drawing.Point(148, 339);
            this.btnUpdate.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.RectColor = System.Drawing.Color.Transparent;
            this.btnUpdate.ShowFocusLine = true;
            this.btnUpdate.Size = new System.Drawing.Size(34, 25);
            this.btnUpdate.Style = Sunny.UI.UIStyle.Custom;
            this.btnUpdate.StyleCustomMode = true;
            this.btnUpdate.Symbol = 361465;
            this.btnUpdate.SymbolColor = System.Drawing.Color.RoyalBlue;
            this.btnUpdate.SymbolHoverColor = System.Drawing.Color.DeepSkyBlue;
            this.btnUpdate.SymbolPressColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.SymbolSelectedColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.TipsText = "1";
            this.btnUpdate.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lbDatabase
            // 
            this.lbDatabase.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(155)))), ((int)(((byte)(40)))));
            this.lbDatabase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDatabase.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lbDatabase.Location = new System.Drawing.Point(208, 244);
            this.lbDatabase.Name = "lbDatabase";
            this.lbDatabase.Size = new System.Drawing.Size(77, 23);
            this.lbDatabase.TabIndex = 7;
            this.lbDatabase.TabStop = true;
            this.lbDatabase.Text = "Database";
            this.lbDatabase.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lbDatabase.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.lbDatabase.Click += new System.EventHandler(this.lbDatabase_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(43, 294);
            this.btnLogin.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.btnLogin.Size = new System.Drawing.Size(102, 35);
            this.btnLogin.Style = Sunny.UI.UIStyle.Custom;
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.TipsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(111)))));
            this.btnLogin.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FillColor = System.Drawing.Color.White;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.btnExit.Location = new System.Drawing.Point(179, 294);
            this.btnExit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 35);
            this.btnExit.Style = Sunny.UI.UIStyle.Custom;
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Cancel";
            this.btnExit.TipsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(111)))));
            this.btnExit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel2.Location = new System.Drawing.Point(97, 12);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(133, 49);
            this.uiLabel2.TabIndex = 6;
            this.uiLabel2.Text = "LOGIN";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtUser
            // 
            this.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(43, 76);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUser.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtUser.Name = "txtUser";
            this.txtUser.Radius = 8;
            this.txtUser.ShowText = false;
            this.txtUser.Size = new System.Drawing.Size(238, 47);
            this.txtUser.TabIndex = 0;
            this.txtUser.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUser.Watermark = "Email Address";
            this.txtUser.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbbLanguage
            // 
            this.cbbLanguage.DataSource = null;
            this.cbbLanguage.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbbLanguage.FillColor = System.Drawing.Color.White;
            this.cbbLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbLanguage.Items.AddRange(new object[] {
            "English",
            "Viet Nam",
            "Korea"});
            this.cbbLanguage.Location = new System.Drawing.Point(43, 190);
            this.cbbLanguage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbLanguage.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbbLanguage.Name = "cbbLanguage";
            this.cbbLanguage.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbbLanguage.Size = new System.Drawing.Size(238, 41);
            this.cbbLanguage.TabIndex = 2;
            this.cbbLanguage.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbbLanguage.Watermark = "";
            this.cbbLanguage.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.cbbLanguage.SelectedIndexChanged += new System.EventHandler(this.cbbLanguage_SelectedIndexChanged);
            this.cbbLanguage.SelectedValueChanged += new System.EventHandler(this.cbbLanguage_SelectedValueChanged);
            // 
            // txtPass
            // 
            this.txtPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPass.DoubleValue = 123D;
            this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.IntValue = 123;
            this.txtPass.Location = new System.Drawing.Point(43, 133);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPass.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Radius = 8;
            this.txtPass.ShowText = false;
            this.txtPass.Size = new System.Drawing.Size(238, 47);
            this.txtPass.TabIndex = 1;
            this.txtPass.Text = "123";
            this.txtPass.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPass.Watermark = "Password";
            this.txtPass.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbRemember
            // 
            this.cbRemember.BackColor = System.Drawing.SystemColors.Control;
            this.cbRemember.CheckBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.cbRemember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRemember.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRemember.Location = new System.Drawing.Point(43, 239);
            this.cbRemember.MinimumSize = new System.Drawing.Size(1, 1);
            this.cbRemember.Name = "cbRemember";
            this.cbRemember.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.cbRemember.Size = new System.Drawing.Size(159, 29);
            this.cbRemember.Style = Sunny.UI.UIStyle.Custom;
            this.cbRemember.TabIndex = 3;
            this.cbRemember.Text = "Remember Me";
            this.cbRemember.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // LoginV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginV2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginV2";
            this.Load += new System.EventHandler(this.LoginV2_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginV2_KeyPress);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panelShadow1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UITextBox txtPass;
        private Sunny.UI.UITextBox txtUser;
        private Sunny.UI.UIComboBox cbbLanguage;
        private Sunny.UI.UICheckBox cbRemember;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton btnExit;
        private Custom.PanelShadow panelShadow1;
        private System.Windows.Forms.Panel panel3;
        private Sunny.UI.UIButton btnLogin;
        private Sunny.UI.UILinkLabel lbDatabase;
        private Sunny.UI.UISymbolButton btnUpdate;
    }
}