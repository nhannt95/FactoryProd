namespace DONGJIN_MES.IUserControl
{
    partial class Login
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbRemember = new Sunny.UI.UICheckBox();
            this.txtPass = new Sunny.UI.UITextBox();
            this.txtUser = new Sunny.UI.UITextBox();
            this.cbbLanguage = new Sunny.UI.UIComboBox();
            this.lbForgot = new System.Windows.Forms.LinkLabel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnLogin = new Sunny.UI.UIButton();
            this.btnSign = new Sunny.UI.UIButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1316, 645);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.pictureBox1.Image = global::DONGJIN_MES.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(921, 645);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSign);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.cbRemember);
            this.panel1.Controls.Add(this.txtPass);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Controls.Add(this.cbbLanguage);
            this.panel1.Controls.Add(this.lbForgot);
            this.panel1.Controls.Add(this.lbTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(921, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 645);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cbRemember
            // 
            this.cbRemember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRemember.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRemember.Location = new System.Drawing.Point(24, 412);
            this.cbRemember.MinimumSize = new System.Drawing.Size(1, 1);
            this.cbRemember.Name = "cbRemember";
            this.cbRemember.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.cbRemember.Size = new System.Drawing.Size(176, 29);
            this.cbRemember.TabIndex = 12;
            this.cbRemember.Text = "Remember me";
            this.cbRemember.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtPass
            // 
            this.txtPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPass.DoubleValue = 123D;
            this.txtPass.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.IntValue = 123;
            this.txtPass.Location = new System.Drawing.Point(25, 314);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPass.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Radius = 8;
            this.txtPass.ShowText = false;
            this.txtPass.Size = new System.Drawing.Size(232, 38);
            this.txtPass.TabIndex = 11;
            this.txtPass.Text = "123";
            this.txtPass.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPass.Watermark = "Password";
            this.txtPass.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyDown);
            // 
            // txtUser
            // 
            this.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(25, 266);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUser.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtUser.Name = "txtUser";
            this.txtUser.Radius = 8;
            this.txtUser.ShowText = false;
            this.txtUser.Size = new System.Drawing.Size(232, 38);
            this.txtUser.TabIndex = 11;
            this.txtUser.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUser.Watermark = "Username";
            this.txtUser.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbbLanguage
            // 
            this.cbbLanguage.DataSource = null;
            this.cbbLanguage.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbbLanguage.FillColor = System.Drawing.Color.White;
            this.cbbLanguage.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbLanguage.Items.AddRange(new object[] {
            "English",
            "Viet Nam",
            "Korea"});
            this.cbbLanguage.Location = new System.Drawing.Point(25, 363);
            this.cbbLanguage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbLanguage.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbbLanguage.Name = "cbbLanguage";
            this.cbbLanguage.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbbLanguage.Radius = 9;
            this.cbbLanguage.Size = new System.Drawing.Size(168, 34);
            this.cbbLanguage.TabIndex = 10;
            this.cbbLanguage.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbbLanguage.Watermark = "";
            this.cbbLanguage.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.cbbLanguage.SelectedIndexChanged += new System.EventHandler(this.cbbLanguage_SelectedIndexChanged);
            // 
            // lbForgot
            // 
            this.lbForgot.AutoSize = true;
            this.lbForgot.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lbForgot.Location = new System.Drawing.Point(242, 412);
            this.lbForgot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbForgot.Name = "lbForgot";
            this.lbForgot.Size = new System.Drawing.Size(116, 16);
            this.lbForgot.TabIndex = 5;
            this.lbForgot.TabStop = true;
            this.lbForgot.Text = "Forgot Password?";
            this.lbForgot.Visible = false;
            this.lbForgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbForgot_LinkClicked);
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(20, 196);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(237, 23);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Sign in to Dongjin Viet Nam";
            // 
            // btnLogin
            // 
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnLogin.Location = new System.Drawing.Point(54, 488);
            this.btnLogin.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 35);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "Login";
            this.btnLogin.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSign
            // 
            this.btnSign.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSign.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnSign.Location = new System.Drawing.Point(225, 488);
            this.btnSign.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(100, 35);
            this.btnSign.TabIndex = 14;
            this.btnSign.Text = "Sign Up";
            this.btnSign.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login";
            this.Size = new System.Drawing.Size(1316, 645);
            this.Load += new System.EventHandler(this.Login_Load);
            this.Resize += new System.EventHandler(this.Login_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.LinkLabel lbForgot;
        private Sunny.UI.UIComboBox cbbLanguage;
        private Sunny.UI.UITextBox txtUser;
        private Sunny.UI.UITextBox txtPass;
        private Sunny.UI.UICheckBox cbRemember;
        private Sunny.UI.UIButton btnSign;
        private Sunny.UI.UIButton btnLogin;
    }
}
