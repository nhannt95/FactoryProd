﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DONGJIN_MES.Custom
{
    public partial class LabelComponent : UserControl
    {
        private string textlb = "";
        private string textBox = "";
        private Padding margin;
        public LabelComponent()
        {
            InitializeComponent();
            margin = lbValue.Margin;
            textBox = lbValue.Text;
            textlb = lbText.Text;
        }
        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            using (GraphicsPath GraphPath = GetRoundPath(Rect, 15))
            {
                this.Region = new Region(GraphPath);
                using (Pen pen = new Pen(Color.CadetBlue, 0.75f))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }
        public string TextLabel
        {
            get { return lbText.Text; }
            set
            {
                lbText.Text = value;
            }
        }
        public string TextBox
        {
            get { return lbValue.Text; }
            set
            {
                lbValue.Text = value;
            }
        }
        public Padding Margins
        {
            get { return margin; }
            set
            {
                lbValue.Margin = value;
            }
        }

        private void txtValue__TextChanged(object sender, EventArgs e)
        {
            textBox = lbValue.Text;
        }

        private void lbText_TextChanged(object sender, EventArgs e)
        {
            textlb = lbText.Text;
        }

        private void lbValue_Click(object sender, EventArgs e)
        {

        }
    }
}
