using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DONGJIN_MES.Custom
{
    public partial class LabelComponents : UserControl
    {
        private string lbTitle = "";
        private string lbValue = "";
        private Padding margin;
        private Font fontTitle;
        private Font fontValue;
        private Color color;
        public LabelComponents()
        {
            InitializeComponent();
            margin = txtValue.Padding;
            lbTitle = lbText.Text;
            lbValue = txtValue.Text;
            fontTitle = lbText.Font;
            fontValue = txtValue.Font;
            color = txtValue.BackColor;
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
            using (GraphicsPath GraphPath = GetRoundPath(Rect, 7))
            {
                this.Region = new Region(GraphPath);
                using (Pen pen = new Pen(Color.CadetBlue, 2f))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }
        public string TextTitle
        {
            get { return lbText.Text; }
            set
            {
                lbText.Text = value;
            }
        }
        public string TextValue
        {
            get { return lbValue; }
            set
            {
                txtValue.Text = value;
            }
        }
        public Padding Margins
        {
            get { return txtValue.Margin; }
            set
            {
                txtValue.Margin = value;
            }
        }
        public Font FontTitle
        {
            get { return lbText.Font; }
            set
            {
                lbText.Font = value;
            }
        }
        public Font FontValue
        {
            get { return fontValue; }
            set
            {
                txtValue.Font = value;
            }
        }
        public Color BackColors
        {
            get { return color; }
            set
            {
                txtValue.BackColor = value;
            }
        }

        private void txtValue__TextChanged(object sender, EventArgs e)
        {
            lbValue = txtValue.Text;
        }
    }
}
