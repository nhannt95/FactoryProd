using DONGJIN_MES.Class;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class MsgBox : Form
    {
        public MsgBox(MgsLog notice, string msg)
        {
            InitializeComponent();
            lbMsg.Text = msg;
            lbNotice.Text=notice.ToString().ToUpper();
            switch (notice)
            {
                case MgsLog.Confirm:
                    lbNotice.BackColor = panel1.BackColor = Color.FromArgb(46, 59, 111);
                    btnokk.Visible=false;
                    break;
                case MgsLog.Success:
                    lbNotice.BackColor = panel1.BackColor = btnokk.BackColor= Color.FromArgb(38, 126, 87);
                    btnOK.Visible=btnCancel.Visible= false;
                    btnokk.Text = "OK";
                    break ;
                case MgsLog.Information:
                    lbNotice.BackColor = panel1.BackColor = btnokk.BackColor = Color.FromArgb(38, 126, 87);
                    btnOK.Visible = btnCancel.Visible = false;
                    btnokk.Text = "OK";
                    break;
                case MgsLog.Error:
                    lbNotice.BackColor = panel1.BackColor = btnokk.BackColor = Color.FromArgb(151, 14, 0);
                    btnOK.Visible = btnCancel.Visible = false;
                    btnokk.Text = "Close";
                    break;
                case MgsLog.LoginFailed:
                    lbNotice.BackColor = panel1.BackColor = btnokk.BackColor = Color.FromArgb(151, 14, 0);
                    btnOK.Visible = btnCancel.Visible = false;
                    btnokk.Text = "Close";
                    break;
                case MgsLog.Warning:
                    lbNotice.BackColor = panel1.BackColor = btnokk.BackColor = Color.FromArgb(255, 82, 0);
                    btnOK.Visible = btnCancel.Visible = false;
                    btnokk.Text = "Close";
                    break;
                case MgsLog.Notice:
                    lbNotice.BackColor = panel1.BackColor = btnokk.BackColor = Color.FromArgb(46, 59, 111);
                    btnOK.Visible = btnCancel.Visible = false;
                    btnokk.Text = "OK";
                    break;
            }
        }

        private void MsgBox_Load(object sender, EventArgs e)
        {

        }

        private void btnokk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
