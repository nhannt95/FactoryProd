namespace DONGJIN_MES.IUserControl.SubSetting
{
    partial class Resize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resize));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nbSize = new Sunny.UI.UIDoubleUpDown();
            this.nbLogSize = new Sunny.UI.UIDoubleUpDown();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "GirdView Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Log Size";
            // 
            // nbSize
            // 
            this.nbSize.Decimal = 0;
            this.nbSize.DecimalPlaces = 0;
            this.nbSize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbSize.Location = new System.Drawing.Point(165, 19);
            this.nbSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nbSize.Maximum = 30D;
            this.nbSize.Minimum = 4D;
            this.nbSize.MinimumSize = new System.Drawing.Size(100, 0);
            this.nbSize.Name = "nbSize";
            this.nbSize.ShowText = false;
            this.nbSize.Size = new System.Drawing.Size(128, 29);
            this.nbSize.Step = 1D;
            this.nbSize.TabIndex = 2;
            this.nbSize.Text = "uiDoubleUpDown1";
            this.nbSize.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nbSize.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.nbSize.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.nbSize_ValueChanged);
            // 
            // nbLogSize
            // 
            this.nbLogSize.Decimal = 0;
            this.nbLogSize.DecimalPlaces = 0;
            this.nbLogSize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbLogSize.Location = new System.Drawing.Point(165, 74);
            this.nbLogSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nbLogSize.Maximum = 30D;
            this.nbLogSize.Minimum = 4D;
            this.nbLogSize.MinimumSize = new System.Drawing.Size(100, 0);
            this.nbLogSize.Name = "nbLogSize";
            this.nbLogSize.ShowText = false;
            this.nbLogSize.Size = new System.Drawing.Size(128, 29);
            this.nbLogSize.Step = 1D;
            this.nbLogSize.TabIndex = 2;
            this.nbLogSize.Text = "uiDoubleUpDown1";
            this.nbLogSize.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nbLogSize.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.nbLogSize.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.nbLogSize_ValueChanged);
            // 
            // Resize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(306, 122);
            this.Controls.Add(this.nbLogSize);
            this.Controls.Add(this.nbSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Resize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Font Size modify";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UIDoubleUpDown nbSize;
        private Sunny.UI.UIDoubleUpDown nbLogSize;
    }
}