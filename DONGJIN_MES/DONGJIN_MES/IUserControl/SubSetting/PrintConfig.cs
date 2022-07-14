using DONGJIN_MES.Class;
using MES_IO.Class;
using System.IO;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class PrintConfig : Form
    {
        PrintSetting printSetting;
        public PrintConfig(PrintSetting set)
        {
            InitializeComponent();
            printSetting = set;
            nbPPW.Value = printSetting.PaperWidth>0?(double)printSetting.PaperWidth:1;
            nbPPH.Value = printSetting.PaperHeight>0?(double)printSetting.PaperHeight : 1;
            nbML.Value = printSetting.MarginLeft>0?(double)printSetting.MarginLeft : 1;
            nbMT.Value = printSetting.MarginTop>0?(double)printSetting.MarginTop : 1;
            nbTs.Value = printSetting.Font>0?(double)printSetting.Font:1;
            nbQs.Value = printSetting.QRSize>0?(double)printSetting.QRSize:1;
            nbLine.Value = printSetting.DistanceLine>0?(double)printSetting.DistanceLine:1;
            nbTQ.Value = printSetting.DistanceQRText>0?(double)printSetting.DistanceQRText:1;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            printSetting.PaperWidth = (int)nbPPW.Value;
            printSetting.PaperHeight = (int)nbPPH.Value;
            printSetting.MarginLeft = (int)nbML.Value;
            printSetting.MarginTop = (int)nbMT.Value;
            printSetting.Font = (int)nbTs.Value;
            printSetting.QRSize = (int)nbQs.Value;
            printSetting.DistanceQRText = (int)nbTQ.Value;
            printSetting.DistanceLine = (int)nbLine.Value;
            using(StreamWriter sw = new StreamWriter(StaticSetting.PathConfigPrinter))
            {
                sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(printSetting));
                sw.Close();
            }
            Utinity.Mgs(MgsLog.Success, "Saved");
            this.Close();
        }

        private void uiMarkLabel1_Click(object sender, System.EventArgs e)
        {

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
