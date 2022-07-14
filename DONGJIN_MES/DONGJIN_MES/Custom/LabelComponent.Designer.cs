namespace DONGJIN_MES.Custom
{
    partial class LabelComponent
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
            this.lbText = new System.Windows.Forms.Label();
            this.iPanelS1 = new DONGJIN_MES.Custom.IPanelS();
            this.lbValue = new DONGJIN_MES.Custom.ILabelS();
            this.tableLayoutPanel1.SuspendLayout();
            this.iPanelS1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(109)))), ((int)(((byte)(199)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.iPanelS1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(352, 118);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbText.Location = new System.Drawing.Point(0, 0);
            this.lbText.Margin = new System.Windows.Forms.Padding(0);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(93, 118);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "Text";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbText.TextChanged += new System.EventHandler(this.txtValue__TextChanged);
            // 
            // iPanelS1
            // 
            this.iPanelS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(109)))), ((int)(((byte)(199)))));
            this.iPanelS1.BorderColor = System.Drawing.Color.DarkBlue;
            this.iPanelS1.BorderRadius = 10;
            this.iPanelS1.BorderSize = 2;
            this.iPanelS1.Controls.Add(this.lbValue);
            this.iPanelS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iPanelS1.ForeColor = System.Drawing.Color.White;
            this.iPanelS1.Location = new System.Drawing.Point(100, 6);
            this.iPanelS1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.iPanelS1.Name = "iPanelS1";
            this.iPanelS1.Size = new System.Drawing.Size(245, 106);
            this.iPanelS1.TabIndex = 1;
            // 
            // lbValue
            // 
            this.lbValue.BackColor = System.Drawing.Color.GhostWhite;
            this.lbValue.BorderColor = System.Drawing.Color.DarkBlue;
            this.lbValue.BorderRadius = 10;
            this.lbValue.BorderSize = 2;
            this.lbValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbValue.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValue.ForeColor = System.Drawing.Color.Black;
            this.lbValue.Location = new System.Drawing.Point(0, 0);
            this.lbValue.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(245, 106);
            this.lbValue.TabIndex = 0;
            this.lbValue.Text = "0";
            this.lbValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbValue.TextChanged += new System.EventHandler(this.txtValue__TextChanged);
            // 
            // LabelComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LabelComponent";
            this.Size = new System.Drawing.Size(352, 118);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.iPanelS1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbText;
        private IPanelS iPanelS1;
        private ILabelS lbValue;
    }
}
