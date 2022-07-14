using DONGJIN_MES.Class;
using MES_IO.Class;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl
{
    public partial class Main : UserControl
    {
        frmMain frmMain;
        private Label prdmngt = new Label { Text = "Production Management", Font = new Font("Segoe UI", 8.75F, FontStyle.Bold, GraphicsUnit.Point), Location = new Point(10, 10), AutoSize = false };

        private Rectangle lb0;
        private Rectangle lb1;
        private Rectangle lb11;
        private Rectangle lb111;
        private Rectangle lb112;
        private Rectangle lb2;
        private Rectangle lb21;
        private Rectangle lb211;
        private Rectangle formRect;
        //private Rectangle lbUser;

        private float ilb0;
        private float ilb1;
        private float ilb11;
        private float ilb111;
        private float ilb112;
        private float ilb2;
        private float ilb21;
        private float ilb211;
        //private float ilbUser;
        private float fontScale = 1f;

        public Main(frmMain frm)
        {
            InitializeComponent();
            frmMain = frm;
            lbUserLogin.Text = UserSettings.FullName;
            //frmMain.HideSearch(true);
        }

        //private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        //{
        //    ControlPaint.DrawBorder(e.Graphics, this.tableLayoutPanel1.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
        //}

        private void Main_Load(object sender, EventArgs e)
        {
            //Ini();
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, this.pbMain.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
            //using (Graphics g = e.Graphics)
            //{
            //    g.DrawLine(new Pen(Color.Black, 3), new Point(12, 60), new Point(293, 60));
            //}
        }
        private void lbProdResult_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain.ToProductionResult();
        }
        private void Ini()
        {
            formRect = new Rectangle(this.Location, this.Size);
            lb0 = new Rectangle(label2.Location, label2.Size);
            lb1 = new Rectangle(label3.Location, label3.Size);
            lb11 = new Rectangle(label4.Location, label4.Size);
            lb111 = new Rectangle(lbProdResult.Location, lbProdResult.Size);
            lb112 = new Rectangle(lbRework.Location, lbRework.Size);
            lb2 = new Rectangle(label7.Location, label7.Size);
            lb21 = new Rectangle(label8.Location, label8.Size);
            lb211 = new Rectangle(lbFoolSub.Location, lbFoolSub.Size);

            ilb0 = label2.Font.Size;
            ilb1 = label3.Font.Size;
            ilb11 = label4.Font.Size;
            ilb111 = lbProdResult.Font.Size;
            ilb112 = lbRework.Font.Size;
            ilb2 = label7.Font.Size;
            ilb21 = label8.Font.Size;
            ilb211 = lbFoolSub.Font.Size;
        }
        private void ResizeControl(Control control, Rectangle rec, float fontSize, FontStyle fontStyle)
        {
            float xRatio = (float)this.ClientRectangle.Width / (float)formRect.Width;
            float yRatio = (float)this.ClientRectangle.Height / (float)formRect.Height;
            if (xRatio == 0 && yRatio == 0) return;
            float newX = rec.Location.X * xRatio;
            float newY = rec.Location.Y * yRatio;
            control.Location = new Point((int)newX, (int)newY);
            control.Width = (int)(rec.Width * xRatio);
            control.Height = (int)(rec.Height * yRatio);
            float ratio = xRatio;
            if (xRatio >= yRatio) ratio = yRatio;
            float newFontSize = fontSize * ratio * fontScale;
            Font newFont = new Font(control.Font.FontFamily, newFontSize, fontStyle);
            control.Font = newFont;
        }
        private void ResizeChildrenContrtol()
        {
            ResizeControl(label2, lb0, ilb0, FontStyle.Bold);
            ResizeControl(label3, lb1, ilb1, FontStyle.Bold);
            ResizeControl(label4, lb11, ilb11, FontStyle.Bold);
            ResizeControl(lbProdResult, lb111, ilb111, FontStyle.Regular);
            ResizeControl(lbRework, lb112, ilb112, FontStyle.Regular);
            ResizeControl(label7, lb2, ilb2, FontStyle.Bold);
            ResizeControl(label8, lb21, ilb21, FontStyle.Bold);
            ResizeControl(lbFoolSub, lb211, ilb211, FontStyle.Regular);
            //ResizeControl(lbUserLogin, lbUser, ilbUser, FontStyle.Regular);
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            //ResizeChildrenContrtol();
            //lbUserLogin.Size = new Size((int)Utinity.MeasureString(lbUserLogin.Text,lbUserLogin.Font).Width, lbUserLogin.Height);
        }

        private void lbUserLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain.Logout();
        }

        private void lbRework_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This function is updating...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lbFoolSub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This function is updating...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
