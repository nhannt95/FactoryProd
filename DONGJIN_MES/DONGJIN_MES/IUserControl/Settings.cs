using DONGJIN_MES.IUserControl.SubSetting;
using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using DONGJIN_MES.Class;
using MES_IO.Class;
using System.IO;

namespace DONGJIN_MES.IUserControl
{
    public partial class Settings : Form
    {
        ProductionResult productionResult;
        private string localSetting;
        public Settings(ProductionResult prd)
        {
            InitializeComponent();
            productionResult = prd;
            localSetting = JsonConvert.SerializeObject(productionResult.mSettingInfo);
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnCount = 1;

            ////
            Panel pb1 = new Panel();
            pb1.Dock = DockStyle.Top;
            pb1.Height = 150;
            pb1.Controls.Add(new LineInfo(ref productionResult) { Dock = DockStyle.Fill });
            tableLayoutPanel1.Controls.Add(pb1);
            //
            Panel pb2 = new Panel();
            pb2.Dock = DockStyle.Top;
            pb2.Height = 190;
            pb2.Controls.Add(new SerialPort(ref productionResult) { Dock = DockStyle.Fill });
            tableLayoutPanel1.Controls.Add(pb2);
            //
            Panel pb3 = new Panel();
            pb3.Dock = DockStyle.Top;
            pb3.Height = 150;
            pb3.Controls.Add(new PLCInfo(ref productionResult) { Dock = DockStyle.Fill });
            tableLayoutPanel1.Controls.Add(pb3);
            //
            Panel pb4 = new Panel();
            pb4.Dock = DockStyle.Top;
            pb4.Height = 150;
            pb4.Controls.Add(new IOInfo(ref productionResult) { Dock = DockStyle.Fill });
            tableLayoutPanel1.Controls.Add(pb4);
            //
            Panel pb5 = new Panel();
            pb5.Dock = DockStyle.Top;
            pb5.Height = 150;
            pb5.Controls.Add(new SoundInfo(ref productionResult) { Dock = DockStyle.Fill });
            tableLayoutPanel1.Controls.Add(pb5);
            //
            Panel pb6 = new Panel();
            pb6.Dock = DockStyle.Top;
            pb6.Height = 150;
            pb6.Controls.Add(new PrinterInfo(ref productionResult) { Dock = DockStyle.Fill });
            tableLayoutPanel1.Controls.Add(pb6);
            //
            Panel pb7 = new Panel();
            pb7.Dock = DockStyle.Top;
            pb7.Height = 150;
            pb7.Controls.Add(new UnitID(ref productionResult) { Dock = DockStyle.Fill });
            tableLayoutPanel1.Controls.Add(pb7);

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ResumeLayout();
            GC.Collect();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (localSetting == JsonConvert.SerializeObject(productionResult.mSettingInfo)) return;
                string linename = string.Empty;
                if (productionResult.mSettingInfo.LineInfos.LineName.Contains("'"))
                {
                    for (int i = 0; i < productionResult.mSettingInfo.LineInfos.LineName.Length; i++)
                    {
                        if (productionResult.mSettingInfo.LineInfos.LineName[i].ToString() == "'")
                        {
                            linename += "'";
                        }
                        linename += productionResult.mSettingInfo.LineInfos.LineName[i].ToString();
                    }
                    productionResult.mSettingInfo.LineInfos.LineName = linename;
                }
                string processname = string.Empty;
                if (productionResult.mSettingInfo.LineInfos.ProcessName.Contains("'"))
                {
                    for (int i = 0; i < productionResult.mSettingInfo.LineInfos.ProcessName.Length; i++)
                    {
                        if (productionResult.mSettingInfo.LineInfos.ProcessName[i].ToString() == "'")
                        {
                            processname += "'";
                        }
                        processname += productionResult.mSettingInfo.LineInfos.ProcessName[i].ToString();
                    }
                    productionResult.mSettingInfo.LineInfos.ProcessName = processname;
                }
                string wsname = string.Empty;
                if (productionResult.mSettingInfo.LineInfos.WSName.Contains("'"))
                {
                    for (int i = 0; i < productionResult.mSettingInfo.LineInfos.WSName.Length; i++)
                    {
                        if (productionResult.mSettingInfo.LineInfos.WSName[i].ToString() == "'")
                        {
                            wsname += "'";
                        }
                        wsname += productionResult.mSettingInfo.LineInfos.WSName[i].ToString();
                    }
                    productionResult.mSettingInfo.LineInfos.WSName = wsname;
                }
                var save = productionResult.mDataLayer.SaveConfig(productionResult.mSettingInfo);
                using (StreamWriter sw = new StreamWriter(StaticSetting.PathConfig))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(productionResult.mSettingInfo));
                    sw.Close();
                }
                if (productionResult.mSettingInfo.LineInfos.LineName.Contains("''"))
                {
                    productionResult.mSettingInfo.LineInfos.LineName = productionResult.mSettingInfo.LineInfos.LineName.Replace("''","'");
                }
                if (productionResult.mSettingInfo.LineInfos.ProcessName.Contains("''"))
                {
                    productionResult.mSettingInfo.LineInfos.ProcessName = productionResult.mSettingInfo.LineInfos.ProcessName.Replace("''", "'");
                }
                if (productionResult.mSettingInfo.LineInfos.WSName.Contains("''"))
                {
                    productionResult.mSettingInfo.LineInfos.WSName = productionResult.mSettingInfo.LineInfos.WSName.Replace("''", "'");
                }

                productionResult.Title();
                productionResult.SaveLineConfig(JsonConvert.SerializeObject(productionResult.mSettingInfo.LineInfos));
            }
            catch (Exception ex)
            {
                Utinity.Mgs(MgsLog.Error, ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
