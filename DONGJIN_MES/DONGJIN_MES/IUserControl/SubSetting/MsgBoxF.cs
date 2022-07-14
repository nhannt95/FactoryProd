using System;
using System.Drawing;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class MsgBoxF : Form
    {
        Settings _set;
        public MsgBoxF(Settings set)
        {
            InitializeComponent();
        }

        private void MsgBox_Load(object sender, EventArgs e)
        {

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
