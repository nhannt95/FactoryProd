namespace Office2013StyleSplashScreen
{
    partial class Splash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.appMini = new System.Windows.Forms.Label();
            this.bigApp = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Label();
            this.minimize = new System.Windows.Forms.Label();
            this.progressbar1 = new MetroProgressBar.Progressbar();
            this.splashtime = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // appMini
            // 
            this.appMini.AutoSize = true;
            this.appMini.BackColor = System.Drawing.Color.Transparent;
            this.appMini.Font = new System.Drawing.Font("Segoe UI", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.appMini.ForeColor = System.Drawing.Color.White;
            this.appMini.Location = new System.Drawing.Point(30, 9);
            this.appMini.Name = "appMini";
            this.appMini.Size = new System.Drawing.Size(150, 20);
            this.appMini.TabIndex = 0;
            this.appMini.Text = "DONGJIN VIETNAM";
            // 
            // bigApp
            // 
            this.bigApp.AutoSize = true;
            this.bigApp.BackColor = System.Drawing.Color.Transparent;
            this.bigApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bigApp.ForeColor = System.Drawing.Color.White;
            this.bigApp.Location = new System.Drawing.Point(23, 86);
            this.bigApp.Name = "bigApp";
            this.bigApp.Size = new System.Drawing.Size(382, 55);
            this.bigApp.TabIndex = 1;
            this.bigApp.Text = "DONG JIN MES";
            // 
            // close
            // 
            this.close.AutoSize = true;
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.Font = new System.Drawing.Font("Webdings", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.close.ForeColor = System.Drawing.Color.White;
            this.close.Location = new System.Drawing.Point(397, 1);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(36, 26);
            this.close.TabIndex = 3;
            this.close.Text = "r";
            this.close.Click += new System.EventHandler(this.close_Click);
            this.close.MouseLeave += new System.EventHandler(this.close_MouseLeave);
            this.close.MouseHover += new System.EventHandler(this.close_MouseHover);
            // 
            // minimize
            // 
            this.minimize.AutoSize = true;
            this.minimize.BackColor = System.Drawing.Color.Transparent;
            this.minimize.Font = new System.Drawing.Font("Webdings", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.minimize.ForeColor = System.Drawing.Color.White;
            this.minimize.Location = new System.Drawing.Point(362, 1);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(36, 26);
            this.minimize.TabIndex = 4;
            this.minimize.Text = "0";
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            this.minimize.MouseLeave += new System.EventHandler(this.minimize_MouseLeave);
            this.minimize.MouseHover += new System.EventHandler(this.minimize_MouseHover);
            // 
            // progressbar1
            // 
            this.progressbar1.BackColor = System.Drawing.Color.Transparent;
            this.progressbar1.ForeColor = System.Drawing.Color.Red;
            this.progressbar1.Location = new System.Drawing.Point(64, 162);
            this.progressbar1.Name = "progressbar1";
            this.progressbar1.Size = new System.Drawing.Size(308, 14);
            this.progressbar1.TabIndex = 5;
            // 
            // splashtime
            // 
            this.splashtime.Interval = 1600;
            this.splashtime.Tick += new System.EventHandler(this.splashtime_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(86)))), ((int)(((byte)(154)))));
            this.pictureBox1.Image = global::DONGJIN_MES.Properties.Resources.pixlr_bg_result;
            this.pictureBox1.Location = new System.Drawing.Point(7, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DONGJIN_MES.Properties.Resources.Splash;
            this.ClientSize = new System.Drawing.Size(439, 248);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressbar1);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.close);
            this.Controls.Add(this.bigApp);
            this.Controls.Add(this.appMini);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(439, 248);
            this.MinimumSize = new System.Drawing.Size(439, 248);
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label appMini;
        private System.Windows.Forms.Label bigApp;
        private System.Windows.Forms.Label close;
        private System.Windows.Forms.Label minimize;
        private MetroProgressBar.Progressbar progressbar1;
        private System.Windows.Forms.Timer splashtime;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}