using DONGJIN_MES.Class;
using MES_IO.Class;
using System;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl
{
    public partial class Reason : Form
    {
        SetInfo setInfo;
        public Reason(ref SetInfo set)
        {
            InitializeComponent();
            setInfo = set;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReason.Text))
            {
                Utinity.Mgs(MgsLog.Warning, "Please input reason!");
                return;
            }
            setInfo.Reason = txtReason.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
