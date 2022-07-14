using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DONGJIN_MES.Custom
{
    public class PanelShadow : Panel
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public PanelShadow()
        {
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Paint += ParentPaint;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCPAINT = 133;
            if (m.Msg == WM_NCPAINT)
            {
                IntPtr hdc = GetWindowDC(m.HWnd);
                Graphics g = Graphics.FromHdc(hdc);
                Rectangle rDraw = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                Pen pBottom = new Pen(Color.Black, 10);
                Pen pTop = new Pen(Color.White, 10);
                g.DrawRectangle(pBottom, rDraw);
                Point[] pts = new Point[3];

                pts[0] = new Point(0, this.Height - 1);
                pts[1] = new Point(0, 0);
                pts[2] = new Point(this.Width - 1, 0);


                g.DrawLines(pTop, pts);
                ReleaseDC(this.Handle, hdc);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void ParentPaint(object sender, PaintEventArgs e)
        {
            Graphics g = this.Parent.CreateGraphics();
            Matrix mx = new Matrix(1F, 0, 0, 1F, 4, 4);
            Rectangle rdraw = new Rectangle(this.Left, this.Top, this.Width, this.Height);
            g.Transform = mx;
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Black)), rdraw);
            g.Dispose();
        }
    }
}
