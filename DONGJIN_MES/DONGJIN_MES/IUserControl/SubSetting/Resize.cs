using System;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class Resize : Form
    {
        ProductionResult mPrint;
        public Resize(ProductionResult print)
        {
            InitializeComponent();
            mPrint = print;
            nbSize.Value = (double)mPrint.mPrintSetting.DtgwSize;
            nbLogSize.Value = (double)mPrint.mPrintSetting.LogSize;
        }

        private void nbSize_ValueChanged(object sender, double value)
        {
            mPrint.mPrintSetting.DtgwSize = (decimal)nbSize.Value;
            mPrint.ChangeFontGW();
        }

        private void nbLogSize_ValueChanged(object sender, double value)
        {
            mPrint.mPrintSetting.LogSize = (decimal)nbLogSize.Value;
            mPrint.ChangeFontLog();
        }
    }
}
