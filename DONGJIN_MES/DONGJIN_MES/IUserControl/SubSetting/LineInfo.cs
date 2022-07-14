using DONGJIN_MES.Class;
using MES_IO.Class;
using PLCMonitoring.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class LineInfo : UserControl
    {
        ProductionResult mProductionResult;
        private DataLayer mDataLayer;
        private bool mIsLoad = false, mEnableScan;
        private List<string> mLstLines = new List<string>();
        private List<string> mLstProcess = new List<string>();
        private List<string> mLstWS = new List<string>();

        public LineInfo(ref ProductionResult pro)
        {
            InitializeComponent();
            mProductionResult = pro;
        }
        public class ProcessPlan
        {
            public string ProcessCode { get; set; }
            public string ProcessName { get; set; }
            public string PlanYN { get; set; }
            public string BarCodeYN { get; set; }
            public string FinalYN { get; set; }
        }
        public class LinePlan
        {
            public string LineCode { get; set; }
            public string LineName { get; set; }
            public Dictionary<string, ProcessPlan> ProcessPlanList = new Dictionary<string, ProcessPlan>();
        }
        private Dictionary<string, string> lines;
        private Dictionary<string, string> Factory;
        private void LineInfo_Load(object sender, EventArgs e)
        {
            try
            {
                mDataLayer = new DataLayer(StaticSetting.Connection);
                var getFactory = mDataLayer.GetFactory();
                Factory = new Dictionary<string, string>();
                List<string> factory = new List<string>();
                factory.Add(string.Empty);
                if (getFactory.Rows.Count > 0)
                {
                    foreach (DataRow row in getFactory.Rows)
                    {
                        if (!Factory.ContainsKey(row["i_name"].ToString()))
                        {
                            factory.Add(row["i_name"].ToString());
                            Factory.Add(row["i_name"].ToString(), row["i_factory_code"].ToString());
                        }
                    }
                }
                cbbFactory.DataSource = factory;
                ///
                if (!string.IsNullOrEmpty(mProductionResult.mSettingInfo.LineInfos.LineName) && !string.IsNullOrEmpty(mProductionResult.mSettingInfo.LineInfos.ProcessName) && !string.IsNullOrEmpty(mProductionResult.mSettingInfo.LineInfos.WSName) && !string.IsNullOrEmpty(mProductionResult.mSettingInfo.LineInfos.Factory))
                {
                    cbbFactory.Text = mProductionResult.mSettingInfo.LineInfos.Factory;
                    LoadLine();
                    lbLineCode.Text = mProductionResult.mSettingInfo.LineInfos.LineCode;
                    cbbLineN.Text = mProductionResult.mSettingInfo.LineInfos.LineName;
                    LoadProcess(mProductionResult.mSettingInfo.LineInfos.LineCode);
                    lbProcessCode.Text = mProductionResult.mSettingInfo.LineInfos.ProcessCode;
                    cbbProcessN.Text = mProductionResult.mSettingInfo.LineInfos.ProcessName;
                    LoadWS(mProductionResult.mSettingInfo.LineInfos.ProcessCode);
                    lbWSCode.Text = mProductionResult.mSettingInfo.LineInfos.WSCode;
                    cbbWSN.Text = mProductionResult.mSettingInfo.LineInfos.WSName;
                    cbbAutoPO.Text = mProductionResult.mSettingInfo.LineInfos.AutoPO;
                }
                mIsLoad = true;
                mEnableScan = mProductionResult.mSettingInfo.LineInfos.ScanSerial;
                //btnEdit.Text = !mEnableScan ? "Input Box" : "Scan Serial";
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //mProductionResult.EnableScanBox();
            //mEnableScan = !mEnableScan;
            //if (!mEnableScan)
            //{
            //    btnEdit.Text = "Input Box";
            //}
            //else
            //{
            //    btnEdit.Text = "Scan Serial";
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbbLineN.Text) || string.IsNullOrEmpty(lbLineCode.Text) || string.IsNullOrEmpty(cbbProcessN.Text) || string.IsNullOrEmpty(lbProcessCode.Text) || string.IsNullOrEmpty(cbbWSN.Text) || string.IsNullOrEmpty(lbWSCode.Text) || string.IsNullOrEmpty(cbbAutoPO.Text))
                {
                    MessageBox.Show("Please fill all data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    mProductionResult.mSettingInfo.LineInfos.Factory = cbbFactory.Text;
                    if (Factory.ContainsKey(cbbFactory.Text))
                        mProductionResult.mSettingInfo.LineInfos.FactoryCode = Factory[cbbFactory.Text];
                    mProductionResult.mSettingInfo.LineInfos.LineName = cbbLineN.Text;
                    mProductionResult.mSettingInfo.LineInfos.LineCode = lbLineCode.Text;
                    mProductionResult.mSettingInfo.LineInfos.ProcessName = cbbProcessN.Text;
                    mProductionResult.mSettingInfo.LineInfos.ProcessCode = lbProcessCode.Text;
                    mProductionResult.mSettingInfo.LineInfos.WSName = cbbWSN.Text;
                    mProductionResult.mSettingInfo.LineInfos.WSCode = lbWSCode.Text;
                    mProductionResult.mSettingInfo.LineInfos.Input = mInputYS;
                    mProductionResult.mSettingInfo.LineInfos.Final = mProcess[mProductionResult.mSettingInfo.LineInfos.ProcessName].FinalYN;
                    if (mWS.ContainsKey(mProductionResult.mSettingInfo.LineInfos.WSName))
                        mProductionResult.mSettingInfo.LineInfos.Reflect = mWS[mProductionResult.mSettingInfo.LineInfos.WSName].Reflect;
                    mProductionResult.mSettingInfo.LineInfos.BarcodeYN = mProcess[mProductionResult.mSettingInfo.LineInfos.ProcessName].BarCodeYN;
                    if (mProductionResult.mSettingInfo.LineInfos.AutoPO != cbbAutoPO.Text)
                    {
                        mProductionResult.ResetSet();
                    }
                    mProductionResult.mSettingInfo.LineInfos.AutoPO = cbbAutoPO.Text;
                    StaticSetting.AutoPO = mProductionResult.mSettingInfo.LineInfos.AutoPO;
                }
                Utinity.Mgs(MgsLog.Information, "Settings saved");
                mProductionResult.ResetUI();
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        public class WSS
        {
            public string Code { get; set; }
            public string Reflect { get; set; }
        }
        private Dictionary<string, ProcessPlan> mProcess;
        private Dictionary<string, WSS> mWS;
        private string mInputYS;
        private void LoadProcess(string lineCode)
        {
            try
            {
                mLstProcess = new List<string>();
                mLstProcess.Add(string.Empty);
                mProcess = new Dictionary<string, ProcessPlan>();
                var process = mDataLayer.GetProcess(lineCode);
                foreach (DataRow row in process.Rows)
                {
                    string process_name = row["process_name"].ToString();
                    if (!mProcess.ContainsKey(process_name))
                    {
                        mLstProcess.Add(process_name);
                        mProcess.Add(process_name, new ProcessPlan { ProcessName = process_name, ProcessCode = row["process_code"].ToString(), PlanYN = row["prod_plan"].ToString(), BarCodeYN = row["barcode_yn"].ToString(), FinalYN = row["final_yn"].ToString() });
                    }
                }
                cbbProcessN.DataSource = mLstProcess;
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private void LoadWS(string processCode)
        {
            try
            {
                mLstWS = new List<string>();
                mLstWS.Add(string.Empty);
                mWS = new Dictionary<string, WSS>();
                var ws = mDataLayer.GetWS(processCode);
                foreach (DataRow row in ws.Rows)
                {
                    string wsName = row["i_name"].ToString();
                    if (!mProcess.ContainsKey(wsName))
                    {
                        mLstWS.Add(wsName);
                        mWS.Add(wsName, new WSS { Code = row["i_code"].ToString(), Reflect = row["reflect"].ToString() });
                    }
                }
                cbbWSN.DataSource = mLstWS;
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }

        private void cbbLineN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!mIsLoad) return;
                string lineCode = lines[cbbLineN.Text];
                lbLineCode.Text = lineCode;
                LoadProcess(lineCode);
                //mProcess = new Dictionary<string, ProcessPlan>();
                //var process = mDataLayer.GetProcess(lineCode);
                //foreach (DataRow row in process.Rows)
                //{
                //    string process_name = row["process_name"].ToString();
                //    if (!mProcess.ContainsKey(process_name))
                //    {
                //        mProcess.Add(process_name, new ProcessPlan { ProcessName = process_name, ProcessCode = row["process_code"].ToString(), PlanYN = row["prod_plan"].ToString(), BarCodeYN = row["barcode_yn"].ToString() });
                //        mInputYS = row["input_yn"].ToString();
                //        mFinalYN = row["final_yn"].ToString();
                //    }
                //}
                //cbbProcessN.DataSource = mProcess.Keys.ToList(); ;
                lbProcessCode.Text = string.Empty;
                lbWSCode.Text = string.Empty;
                cbbWSN.DataSource = new List<string>();
            }
            catch (Exception ex)
            {

            }
        }
        private void LoadLine()
        {
            try
            {
                mLstLines=new List<string>();
                mLstLines.Add(string.Empty);
                lines = new Dictionary<string, string>();
                var getProcess = mDataLayer.LoadLineInfo();
                foreach (DataRow row in getProcess.Rows)
                {
                    string lineNames = row["line_name"].ToString();
                    if (!string.IsNullOrEmpty(lineNames))
                    {
                        if (!lines.ContainsKey(lineNames))
                        {
                            mLstLines.Add(lineNames);
                            lines.Add(lineNames, row["line_code"].ToString());
                        }
                    }
                }
                cbbLineN.DataSource = mLstLines;
            }
            catch (Exception ex)
            {

            }
        }
        private void cbbFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!mIsLoad) return;
                lbLineCode.Text = string.Empty;
                lbProcessCode.Text = string.Empty;
                lbWSCode.Text = string.Empty;
                cbbWSN.Text = string.Empty;
                cbbProcessN.Text = string.Empty;
                cbbLineN.Text = string.Empty;
                StaticSetting.FactoryCode = Factory[cbbFactory.Text];
                LoadLine();
            }
            catch (Exception ex)
            {

            }
        }

        private void cbbProcessN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsLoad) return;
            try
            {
                if (mProcess[cbbProcessN.Text].PlanYN == "N")
                {
                    MessageBox.Show("This process doesn't calculate plan");
                    return;
                }
                string processCode = mProcess[cbbProcessN.Text].ProcessCode;
                lbProcessCode.Text = processCode;
                LoadWS(processCode);
                lbWSCode.Text = string.Empty;
            }
            catch (Exception ex)
            {

            }
        }

        private void cbbWSN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsLoad) return;
            try
            {
                lbWSCode.Text = mWS[cbbWSN.Text].Code;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
