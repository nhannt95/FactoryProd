namespace DONGJIN_MES.Custom
{
    partial class CustomCheckBox
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
            this.iPanelS1 = new DONGJIN_MES.Custom.IPanelS();
            this.cbValue = new XanderUI.XUICheckBox();
            this.lbCaption = new DONGJIN_MES.Custom.ILabelS();
            this.tableLayoutPanel1.SuspendLayout();
            this.iPanelS1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.iPanelS1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbCaption, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(155, 39);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // iPanelS1
            // 
            this.iPanelS1.BackColor = System.Drawing.Color.Lavender;
            this.iPanelS1.BorderColor = System.Drawing.Color.DarkBlue;
            this.iPanelS1.BorderRadius = 5;
            this.iPanelS1.BorderSize = 1;
            this.iPanelS1.Controls.Add(this.cbValue);
            this.iPanelS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iPanelS1.ForeColor = System.Drawing.Color.White;
            this.iPanelS1.Location = new System.Drawing.Point(4, 4);
            this.iPanelS1.Margin = new System.Windows.Forms.Padding(4);
            this.iPanelS1.Name = "iPanelS1";
            this.iPanelS1.Size = new System.Drawing.Size(32, 31);
            this.iPanelS1.TabIndex = 0;
            // 
            // cbValue
            // 
            this.cbValue.BackColor = System.Drawing.Color.AliceBlue;
            this.cbValue.CheckboxCheckColor = System.Drawing.Color.Black;
            this.cbValue.CheckboxColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbValue.CheckboxHoverColor = System.Drawing.Color.CornflowerBlue;
            this.cbValue.CheckboxStyle = XanderUI.XUICheckBox.Style.iOS;
            this.cbValue.Checked = true;
            this.cbValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbValue.ForeColor = System.Drawing.Color.White;
            this.cbValue.Location = new System.Drawing.Point(0, 0);
            this.cbValue.Margin = new System.Windows.Forms.Padding(0);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(32, 31);
            this.cbValue.TabIndex = 0;
            this.cbValue.TickThickness = 4;
            // 
            // lbCaption
            // 
            this.lbCaption.AutoSize = true;
            this.lbCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(109)))), ((int)(((byte)(199)))));
            this.lbCaption.BorderColor = System.Drawing.Color.Navy;
            this.lbCaption.BorderRadius = 1;
            this.lbCaption.BorderSize = 0;
            this.lbCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCaption.ForeColor = System.Drawing.Color.White;
            this.lbCaption.Location = new System.Drawing.Point(43, 0);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(109, 39);
            this.lbCaption.TabIndex = 1;
            this.lbCaption.Text = "Text";
            this.lbCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(109)))), ((int)(((byte)(199)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CustomCheckBox";
            this.Size = new System.Drawing.Size(155, 39);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.iPanelS1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private IPanelS iPanelS1;
        private XanderUI.XUICheckBox cbValue;
        private ILabelS lbCaption;
    }
}
