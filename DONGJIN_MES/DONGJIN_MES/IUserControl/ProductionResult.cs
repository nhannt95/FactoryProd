using DONGJIN_MES.Class;
using MES_IO.Class;
using PLCMonitoring.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;
using DONGJIN_MES.IUserControl.SubSetting;
using System.IO;
using Spire.Barcode;
using RestSharp;

namespace DONGJIN_MES.IUserControl
{
    public partial class ProductionResult : UserControl
    {
        frmMain frmMain;
        public DataLayer mDataLayer;
        public ProgramSettings mSettingInfo;
        public SetInfo mSetInfo;
        private Dictionary<string, LineQty> mLineInfo = new Dictionary<string, LineQty>();
        private Dictionary<string, string> mPOStatus = new Dictionary<string, string>();
        private Dictionary<string, string> mPOPlan = new Dictionary<string, string>();
        private Dictionary<string, string> mTypeStatus = new Dictionary<string, string>();
        public PrintSetting mPrintSetting;
        private int mSeq = 0;
        private BarcodeSettings mBarcodeSettings;
        DateTime mDtFrom;
        private bool mIsStart = false;
        public ProdTime mProdTime;
        public ProductionResult(frmMain frm)
        {
            InitializeComponent();
            frmMain = frm;
            try
            {
                new Thread(new ThreadStart(() => { ChangeLanguage(); })).Start();
                if (!File.Exists(StaticSetting.PathConfigPrinter))
                {
                    return;
                }
                cbbStatusPO.Nodes.Add("All");
                cbbTypePO.Nodes.Add("All");
                //////////////////////////
                mSettingInfo = new ProgramSettings();
                mSettingInfo.ScreenID = "P001010101";
                mSettingInfo.ShortScreen = nameof(ProductionResult);
                mSettingInfo.ScreenName = "Production Result";
                mDataLayer = new DataLayer(StaticSetting.Connection);
                var getStatus = mDataLayer.GetStatus();
                foreach (DataRow row in getStatus.Rows)
                {
                    string status = row["i_code_name"].ToString();
                    if (!mPOStatus.ContainsKey(status))
                    {
                        mPOStatus.Add(status, row["i_code"].ToString());
                        cbbStatusPO.Nodes.Add(status);
                    }
                }
                var getPOType = mDataLayer.GetPOType();
                foreach (DataRow row in getPOType.Rows)
                {
                    string type = row["i_code_name"].ToString();
                    if (!mTypeStatus.ContainsKey(type))
                    {
                        mTypeStatus.Add(type, row["i_code"].ToString());
                        cbbTypePO.Nodes.Add(type);
                    }
                }
                var loadData = mDataLayer.LoadScreen(mSettingInfo.ScreenID, StaticSetting.MacAddress);
                foreach (DataRow row in loadData.Rows)
                {
                    StaticSetting.IP = row["i_ipaddss"].ToString();
                    StaticSetting.ScreenID = row["i_screen_id"].ToString();
                    StaticSetting.ScreenShortName = row["i_short_screen"].ToString();
                    mSettingInfo.LineInfos.Factory = row["i_factory"].ToString();
                    mSettingInfo.LineInfos.LineName = row["i_line_name"].ToString();
                    mSettingInfo.LineInfos.LineCode = row["i_line_code"].ToString();
                    mSettingInfo.LineInfos.ProcessName = row["i_process_name"].ToString();
                    mSettingInfo.LineInfos.ProcessCode = row["i_process_code"].ToString();
                    mSettingInfo.LineInfos.WSName = row["i_ws_name"].ToString();
                    mSettingInfo.LineInfos.WSCode = row["i_ws_code"].ToString();
                    if (!string.IsNullOrEmpty(row["i_config"].ToString()))
                    {
                        mSettingInfo = JsonConvert.DeserializeObject<ProgramSettings>(row["i_config"].ToString());
                    }
                    mSettingInfo.ScreenID = StaticSetting.ScreenID;
                    mSettingInfo.IpAddress = StaticSetting.IP;
                    mSettingInfo.ShortScreen = nameof(ProductionResult);
                    mSettingInfo.ScreenName = "Production Result";
                    mSettingInfo.LineInfos.AutoPO = (mSettingInfo.LineInfos.AutoPO is null) ? "N" : mSettingInfo.LineInfos.AutoPO;
                    StaticSetting.Line = mSettingInfo.LineInfos.LineName;
                    StaticSetting.AutoPO = mSettingInfo.LineInfos.AutoPO;
                    StaticSetting.FactoryCode = mSettingInfo.LineInfos.FactoryCode;
                    break;
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
            //cbbStatusPO.Nodes[0].Checked = true;
            //cbbTypePO.Nodes[0].Checked = true;
            cbbStatusPO.Text = "All";
            cbbTypePO.Text = "All";
        }

        private void ChangeLanguage()
        {
            if (Language.Languages.Count == 0)
            {
                Utinity.Mgs(MgsLog.Error, "Language does not found!");
                return;
            }
            if (Language.Languages.ContainsKey("004"))
                label3.Text = Language.Languages["004"];
            if (Language.Languages.ContainsKey("005"))
            {
                label4.Text = Language.Languages["005"];
                label18.Text = Language.Languages["005"];
            }
            if (Language.Languages.ContainsKey("006"))
            {
                label5.Text = Language.Languages["006"];

            }
            if (Language.Languages.ContainsKey("007"))
            {
                label6.Text = Language.Languages["007"];
                label19.Text = Language.Languages["007"];
            }
            if (Language.Languages.ContainsKey("008"))
                label7.Text = Language.Languages["008"];
            if (Language.Languages.ContainsKey("006"))
                label9.Text = Language.Languages["006"];
            if (Language.Languages.ContainsKey("037"))
            {
                label13.Text = Language.Languages["037"];
                lbPO.TextTitle = Language.Languages["037"];
            }
            if (Language.Languages.ContainsKey("012"))
                lbModel.TextTitle = Language.Languages["012"];
            if (Language.Languages.ContainsKey("013"))
                iLabelS1.Text = Language.Languages["013"];
            if (Language.Languages.ContainsKey("010"))
                lbPlanQTT.TextLabel = Language.Languages["010"];
            if (Language.Languages.ContainsKey("015"))
                lbActualTT.TextLabel = Language.Languages["015"];
            if (Language.Languages.ContainsKey("016"))
                label2.Text = Language.Languages["016"];
            if (Language.Languages.ContainsKey("025"))
                label20.Text = Language.Languages["025"];
            if (Language.Languages.ContainsKey("018"))
                iLabelS7.Text = Language.Languages["018"];
            if (Language.Languages.ContainsKey("026"))
                iLabelS6.Text = Language.Languages["026"];
            if (Language.Languages.ContainsKey("027"))
                iLabelS5.Text = Language.Languages["027"];
            if (Language.Languages.ContainsKey("028"))
                btnRePrint.Text = Language.Languages["028"];
            if (Language.Languages.ContainsKey("014"))
            {
                btnSearchBox.Text = Language.Languages["014"];
                btnSearchPlan.Text = Language.Languages["014"];
            }
            if (Language.Languages.ContainsKey("017"))
                btnSaveRemark.Text = Language.Languages["017"];
            if (Language.Languages.ContainsKey("019"))
                iLabelS10.Text = Language.Languages["019"];
            if (Language.Languages.ContainsKey("029"))
                label1.Text = Language.Languages["029"];
            if (Language.Languages.ContainsKey("030"))
                iLabelS9.Text = Language.Languages["030"];
            if (Language.Languages.ContainsKey("031"))
                iLabelS8.Text = Language.Languages["031"];
            if (Language.Languages.ContainsKey("032"))
                iLabelS4.Text = Language.Languages["032"];
            if (Language.Languages.ContainsKey("033"))
                dtgwMain.Columns[0].HeaderText = Language.Languages["033"];
            if (Language.Languages.ContainsKey("034"))
                dtgwMain.Columns[1].HeaderText = Language.Languages["034"];
            if (Language.Languages.ContainsKey("035"))
                dtgwMain.Columns[2].HeaderText = Language.Languages["035"];
            if (Language.Languages.ContainsKey("036"))
                dtgwMain.Columns[3].HeaderText = Language.Languages["036"];
            if (Language.Languages.ContainsKey("037"))
                dtgwMain.Columns[4].HeaderText = Language.Languages["037"];
            if (Language.Languages.ContainsKey("038"))
                dtgwMain.Columns[5].HeaderText = Language.Languages["038"];
            if (Language.Languages.ContainsKey("039"))
                dtgwMain.Columns[6].HeaderText = Language.Languages["039"];
            if (Language.Languages.ContainsKey("005"))
                dtgwMain.Columns[7].HeaderText = Language.Languages["005"];
            if (Language.Languages.ContainsKey("040"))
                dtgwMain.Columns[8].HeaderText = Language.Languages["040"];
            if (Language.Languages.ContainsKey("041"))
                dtgwMain.Columns[9].HeaderText = Language.Languages["041"];

            if (Language.Languages.ContainsKey("042"))
            {
                dtgw2.Columns[0].HeaderText = Language.Languages["042"];
                dtgw3.Columns[0].HeaderText = Language.Languages["042"];
            }
            if (Language.Languages.ContainsKey("029"))
            {
                dtgw2.Columns[1].HeaderText = Language.Languages["029"];
                dtgw3.Columns[1].HeaderText = Language.Languages["029"];
            }
            if (Language.Languages.ContainsKey("043"))
            {
                dtgw2.Columns[2].HeaderText = Language.Languages["043"];
            }
        }
        private DateTime mStartTime;
        private void ProductionResult_Load(object sender, EventArgs e)
        {
            try
            {
                txtItemSize.TextAlign = HorizontalAlignment.Center;
                SetWidth();
                lbIPLocal.Text = Utinity.GetIPLocal();
                //int x = (int)tableLayoutPanel2.ColumnStyles[0].Width;
                dtFrom.Value = DateTime.Now.AddDays(-1);
                dtTo.Value = DateTime.Now;
                mDtFrom = dtFrom.Value;
                ////get target
                var getTarget = mDataLayer.GetTarget();
                if (getTarget.Rows.Count > 0)
                {
                    DateTime.TryParse(getTarget.Rows[0]["i_value"].ToString(), out mStartTime);
                }
                new Thread(new ThreadStart(() => { LoadDailyPlan(0, string.Empty); })).Start();
                ResetUI();
                SerialPortConnection.SpPort.Clear();
                SerialPortConnection.Timers.Clear();
                foreach (var sp in mSettingInfo.SerialPorts)
                {
                    SerialPortConnection.SpPort.Add(new System.IO.Ports.SerialPort { PortName = sp.Comport, BaudRate = sp.BaudRate });
                    SerialPortConnection.Timers.Add(new System.Windows.Forms.Timer { Interval = 100 });
                }
                ConnectSerialPort();
                ////load config printer
                mPrintSetting = new PrintSetting();
                using (StreamReader rd = new StreamReader(StaticSetting.PathConfigPrinter))
                {
                    string conf = rd.ReadToEnd();
                    mPrintSetting = JsonConvert.DeserializeObject<PrintSetting>(conf);
                    rd.Close();
                }
                if (mPrintSetting != null)
                {
                    if (mPrintSetting.DtgwSize != 0) dtgwMain.RowsDefaultCellStyle.Font = new Font("Segoe UI", (float)mPrintSetting.DtgwSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    if (mPrintSetting.LogSize != 0) rbt_msg.Font = new Font("Segoe UI", (float)mPrintSetting.LogSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
            Title();
            tmInterval.Enabled = true;
            lbInputScan.BackColor = Color.Yellow;
            lbProQty.ReadOnly = mSettingInfo.LineInfos.ScanSerial ? true : false;
            txtItemSize.ReadOnly = true;
            tmRefresh.Enabled = true;
            lbProQty.ReadOnly = (mSettingInfo.LineInfos.BarcodeYN == "N") ? false : true;
            mIsStart = true;
        }
        public void Title()
        {
            if (!string.IsNullOrEmpty(mSettingInfo.LineInfos.LineName))
            {
                lbLinkScreen.Text = $"PRODUCTION RESULT > {mSettingInfo.LineInfos.LineName} ({mSettingInfo.LineInfos.LineCode}) > {mSettingInfo.LineInfos.ProcessName} ({mSettingInfo.LineInfos.ProcessCode}) > {mSettingInfo.LineInfos.WSName} ({mSettingInfo.LineInfos.WSCode})";
            }
            else
            {
                lbLinkScreen.Text = $"PRODUCTION RESULT";
            }
            try
            {
                mProdTime = new ProdTime();
                var getProdTime = mDataLayer.ProdTime();
                if (getProdTime.Rows.Count > 0)
                {
                    mProdTime.Allow = true;
                    string[] value = getProdTime.Rows[0]["i_value"].ToString().Split(' ');
                    if (value.Length == 5)
                    {
                        mProdTime.DayFrom = value[0].Substring(1);
                        mProdTime.DayTo = value[3].Substring(1);
                        TimeSpan dtFrom, dtTo;
                        TimeSpan.TryParse(value[1], out dtFrom);
                        TimeSpan.TryParse(value[4], out dtTo);
                        mProdTime.TsFrom = dtFrom;
                        mProdTime.TsTo = dtTo;
                    }
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        public class ProdTime
        {
            public bool Allow { get; set; } = false;
            public string DayFrom { get; set; }
            public string DayTo { get; set; }
            public TimeSpan TsFrom { get; set; }
            public TimeSpan TsTo { get; set; }
        }
        private bool AllowProd(string planDate)
        {
            if (mProdTime == null) return true;
            if (!mProdTime.Allow) return true;
            DateTime dtPlan;
            DateTime.TryParse($"{planDate} 00:00:00", out dtPlan);
            DateTime now = DateTime.Now;
            if (now <= (dtPlan.AddDays(int.Parse(mProdTime.DayTo)) + mProdTime.TsTo) && now >= (dtPlan.AddDays(int.Parse(mProdTime.DayFrom)) + mProdTime.TsFrom))
            {
                return true;
            }
            else return false;
        }
        private void SetWidth()
        {
            dtgw2.Columns[0].Width = 50;
            dtgw2.Columns[2].Width = 70;
            dtgw2.Columns[3].Width = 150;

            dtgw3.Columns[0].Width = 50;
            dtgw3.Columns[2].Width = 150;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Settings frm = new Settings(this);
            frm.Show();
        }
        public void LoadDailyPlan(byte mode, string _po)  // 0 is load default, 1 is search
        {
            try
            {
                if (!string.IsNullOrEmpty(mSettingInfo.LineInfos.LineCode))
                {
                    BeginInvoke(new Action(() =>
                    {
                        int planOfDay = 0;
                        int actualOfDay = 0;
                        string con = string.Empty;
                        if (mode == 0)
                        {
                            con += $"and a.i_plan_date = '{DateTime.Now.ToString("yyyy-MM-dd")}'";
                        }
                        else if (mode == 1)
                        {
                            con += (string.IsNullOrEmpty(lbModel.TextValue)) ? "" : $" and c.i_name like'%{lbModel.TextValue.ToUpper()}%'";
                            con += (string.IsNullOrEmpty(lbPO.TextValue)) ? "" : $" and a.i_prod_order_no like '%{lbPO.TextValue.ToUpper()}%' ";
                            con += $"and a.i_plan_date >= '{dtFrom.Value.ToString("yyyy-MM-dd")}' and a.i_plan_date <= '{dtTo.Value.ToString("yyyy-MM-dd")}'";
                            string poStatus = string.Empty;
                            if (!cbbStatusPO.Text.StartsWith("All"))
                            {
                                string[] status = cbbStatusPO.Text.Split(';');
                                foreach (string s in status)
                                {
                                    if (!string.IsNullOrEmpty(s.Trim()))
                                        poStatus += $"'{mPOStatus[s.Trim()]}',";
                                }
                                poStatus = $"({poStatus.Substring(0, poStatus.Length - 1)})";
                                con += $" and a.i_prod_status in {poStatus}";
                            }
                            poStatus = string.Empty;
                            if (!cbbTypePO.Text.StartsWith("All"))
                            {
                                string[] status = cbbTypePO.Text.Split(';');
                                foreach (string s in status)
                                {
                                    if (!string.IsNullOrEmpty(s.Trim()))
                                        poStatus += $"'{mTypeStatus[s.Trim()]}',";
                                }
                                poStatus = $"({poStatus.Substring(0, poStatus.Length - 1)})";
                                con += $" and a.i_po_type in {poStatus}";
                            }
                        }
                        else if (mode == 2)
                        {
                            con += $" and a.i_prod_order_no like '%{_po}%' ";
                        }
                        Dictionary<string, int> actualQty = new Dictionary<string, int>();
                        var loadActual = new DataLayer(StaticSetting.Connection).LoadActual(mSettingInfo.LineInfos.LineCode, con, mSettingInfo.LineInfos.WSCode);
                        foreach (DataRow row in loadActual.Rows)
                        {
                            string po = row["i_po"].ToString();
                            if (!string.IsNullOrEmpty(po) && !actualQty.ContainsKey(po))
                            {
                                int qty = 0;
                                int.TryParse(row["qty"].ToString(), out qty);
                                actualQty.Add(po, qty);
                            }
                        }
                        var loadDaily = new DataLayer(StaticSetting.Connection).LoadDaily(mSettingInfo.LineInfos.LineCode, con, mSettingInfo.LineInfos.WSCode);
                        dtgwMain.Rows.Clear();
                        mLineInfo = new Dictionary<string, LineQty>();
                        foreach (DataRow row in loadDaily.Rows)
                        {
                            string po = row["i_prod_order_no"].ToString();
                            if (!string.IsNullOrEmpty(po))
                            {
                                if (!mLineInfo.ContainsKey(po))
                                {
                                    LineQty lineQty = new LineQty();
                                    lineQty.LineCode = mSettingInfo.LineInfos.LineCode;
                                    lineQty.PlanDate = DateTime.Now.ToString("yyyy-MM-dd");
                                    lineQty.PO = row["i_prod_order_no"].ToString();
                                    lineQty.ModelCode = row["model_code"].ToString();
                                    lineQty.ModelName = row["model_name"].ToString();
                                    lineQty.LotNo = row["lot_no"].ToString();
                                    int modelID = 0;
                                    int.TryParse(row["model_id"].ToString(), out modelID);
                                    lineQty.ModelID = modelID;
                                    lineQty.FactoryCode = row["i_factory_code"].ToString();
                                    int version = 0;
                                    int.TryParse(row["i_version"].ToString(), out version);
                                    lineQty.Version = version;
                                    lineQty.State = row["i_state"].ToString();
                                    lineQty.ProcessCode = mSettingInfo.LineInfos.ProcessCode;
                                    lineQty.FactoryCode = row["i_factory_code"].ToString();
                                    lineQty.POType = row["i_po_type"].ToString();
                                    lineQty.ProdGRId = row["prod_gr_id"].ToString();
                                    int plan = 0;
                                    int actual = 0;
                                    if (!string.IsNullOrEmpty(row["i_plan_qty"].ToString()))
                                    {
                                        int.TryParse(row["i_plan_qty"].ToString(), out plan);
                                    }
                                    if (actualQty.ContainsKey(po)) actual = actualQty[po];
                                    int tacttime = 0;
                                    int.TryParse(row["i_tact_time"].ToString(), out tacttime);
                                    lineQty.TactTime = tacttime;
                                    lineQty.Plan = plan;
                                    lineQty.Actual = actual;
                                    lineQty.Bal = plan - actual;
                                    if (row["plan_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
                                    {
                                        planOfDay += plan;
                                        actualOfDay += actual;
                                    }
                                    mLineInfo.Add(po, lineQty);
                                    dtgwMain.Rows.Add(row["plan_date"].ToString(), mSettingInfo.LineInfos.LineCode, mSettingInfo.LineInfos.LineName, lineQty.LotNo, lineQty.PO, lineQty.ModelCode, lineQty.ModelName, plan.ToString(), actual.ToString(), lineQty.Bal.ToString());
                                }
                            }
                        }

                        lbPlanOfDay.Text = planOfDay.ToString();
                        lbActualOfDay.Text = actualOfDay.ToString();
                        lbRate.Text = (planOfDay == 0 && actualOfDay == 0) ? "0" : Math.Round((double)(actualOfDay * 100) / planOfDay, 1).ToString();
                        PlanQtyOfDay = planOfDay;
                        var target = (int)((DateTime.Now - mStartTime).TotalMilliseconds * planOfDay / 86400000);
                        lbTargerQty.Text = target > 0 ? target.ToString() : "0";
                    }));
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
            GC.Collect();
        }
        public void LoadDailyPlanDaily()  // 0 is load default, 1 is search
        {
            try
            {
                if (!string.IsNullOrEmpty(mSettingInfo.LineInfos.LineCode))
                {
                    int planOfDay = 0;
                    int actualOfDay = 0;
                    string con = string.Empty;
                    con += $"and a.i_plan_date = '{DateTime.Now.ToString("yyyy-MM-dd")}'";
                    Dictionary<string, int> actualQty = new Dictionary<string, int>();
                    var loadActual = mDataLayer.LoadActual(mSettingInfo.LineInfos.LineCode, con, mSettingInfo.LineInfos.WSCode);
                    foreach (DataRow row in loadActual.Rows)
                    {
                        string po = row["i_po"].ToString();
                        if (!string.IsNullOrEmpty(po) && !actualQty.ContainsKey(po))
                        {
                            int qty = 0;
                            int.TryParse(row["qty"].ToString(), out qty);
                            actualQty.Add(po, qty);
                        }
                    }
                    var loadDaily = mDataLayer.LoadDaily(mSettingInfo.LineInfos.LineCode, con, mSettingInfo.LineInfos.WSCode);
                    Dictionary<string, LineQty> lineInfo = new Dictionary<string, LineQty>();
                    foreach (DataRow row in loadDaily.Rows)
                    {
                        string po = row["i_prod_order_no"].ToString();
                        if (!string.IsNullOrEmpty(po))
                        {
                            if (!lineInfo.ContainsKey(po))
                            {
                                LineQty lineQty = new LineQty();
                                lineQty.LineCode = mSettingInfo.LineInfos.LineCode;
                                lineQty.PlanDate = DateTime.Now.ToString("yyyy-MM-dd");
                                lineQty.PO = row["i_prod_order_no"].ToString();
                                lineQty.ModelCode = row["model_code"].ToString();
                                lineQty.ModelName = row["model_name"].ToString();
                                lineQty.LotNo = row["lot_no"].ToString();
                                int modelID = 0;
                                int.TryParse(row["model_id"].ToString(), out modelID);
                                lineQty.ModelID = modelID;
                                lineQty.FactoryCode = row["i_factory_code"].ToString();
                                int version = 0;
                                int.TryParse(row["i_version"].ToString(), out version);
                                lineQty.Version = version;
                                lineQty.State = row["i_state"].ToString();
                                lineQty.ProcessCode = mSettingInfo.LineInfos.ProcessCode;
                                lineQty.FactoryCode = row["i_factory_code"].ToString();
                                lineQty.POType = row["i_po_type"].ToString();
                                lineQty.ProdGRId = row["prod_gr_id"].ToString();
                                int plan = 0;
                                int actual = 0;
                                if (!string.IsNullOrEmpty(row["i_plan_qty"].ToString()))
                                {
                                    int.TryParse(row["i_plan_qty"].ToString(), out plan);
                                }
                                if (actualQty.ContainsKey(po)) actual = actualQty[po];
                                int tacttime = 0;
                                int.TryParse(row["i_tact_time"].ToString(), out tacttime);
                                lineQty.TactTime = tacttime;
                                lineQty.Plan = plan;
                                lineQty.Actual = actual;
                                lineQty.Bal = plan - actual;
                                planOfDay += plan;
                                actualOfDay += actual;
                            }
                        }
                    }
                    lbPlanOfDay.Text = planOfDay.ToString();
                    lbActualOfDay.Text = actualOfDay.ToString();
                    lbRate.Text = planOfDay == 0 ? "0" : Math.Round((double)(actualOfDay * 100) / planOfDay, 1).ToString();
                    PlanQtyOfDay = planOfDay;
                    var target = (int)((DateTime.Now - mStartTime).TotalMilliseconds * planOfDay / 86400000);
                    lbTargerQty.Text = target > 0 ? target.ToString() : "0";
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private int PlanQtyOfDay = 0;
        public void ResetUI()
        {
            mSetInfo = null;
            lbCurrentPO.Text = lbPlanQtyy.Text = lbActualQtyy.Text = lbBalQtyy.Text = string.Empty;
            lbStatus.Text = "OK"; lbStatus.BackColor = Color.SeaGreen;
        }
        private void LoadSerialScan(string po, string ws)
        {
            try
            {
                dtgw3.Invoke(new Action(() => { dtgw3.Rows.Clear(); }));
                var getSerialScan = new DataLayer(StaticSetting.Connection).GetSerial(po, ws);
                int idx = 1;
                foreach (DataRow item in getSerialScan.Rows)
                {
                    dtgw3.Invoke(new Action(() => { dtgw3.Rows.Add(idx, item["i_label_serial"].ToString(), item["i_dte_created"].ToString()); }));
                    idx++;
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }

        public void ConnectSerialPort()
        {
            int idx = 0;
            foreach (var item in SerialPortConnection.SpPort)
            {
                idx++;
                if (!item.IsOpen)
                {
                    new Thread(new ThreadStart(() => { ConnectMutiSerialPort(idx - 1); })).Start();
                    Thread.Sleep(50);
                }
            }
        }
        public void ConnectMutiSerialPort(int index)
        {
            try
            {
                SerialPortConnection.SpPort[index].Open();
                SerialPortConnection.SpPort[index].DataReceived += (sender, e) => Sp_DataReceive(sender, e, index);
                SerialPortConnection.Timers[index].Tick += (sender, e) => DataReceive(sender, e, index);
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }

        private void Sp_DataReceive(object obj, SerialDataReceivedEventArgs e, int index)
        {
            if (this.IsHandleCreated)
            {
                BeginInvoke(new MethodInvoker(() => { SerialPortConnection.Timers[index].Enabled = true; }));
            }
        }
        private void DataReceive(object sender, EventArgs e, int index)
        {
            if (index < mSettingInfo.SerialPorts.Count)
            {
                SerialPortConnection.Timers[index].Enabled = false;
                ProcessScan(SerialPortConnection.SpPort[index].ReadExisting().Replace(((char)02).ToString(), string.Empty).Replace(((char)03).ToString(), string.Empty));
            }
        }
        private bool CheckSerialExist(string serial)
        {
            try
            {
                return mDataLayer.CheckSerialExsit(serial).Rows.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void ProcessScan(string serial)
        {
            try
            {
                if (serial.Length == 0) return;
                if (cbCancelRS.Checked) // cancel result mode
                {
                    tmTactTime.Enabled = false;
                    mTactTime = 0;
                    lbActualTT.TextBox = mTactTime.ToString();
                    tmTactTime.Enabled = true;
                    ///allow cancel prod time
                    var getProdTime = mDataLayer.GetPlanDate(serial, mSettingInfo.LineInfos.WSCode);
                    if (getProdTime.Rows.Count > 0)
                    {
                        if (!AllowProd(getProdTime.Rows[0]["plan_date"].ToString()))
                        {
                            Log(string.Empty, Color.Red, Language.Languages.ContainsKey("139") ? Language.Languages["139"] : "PLANTIME IS OVER !");
                            return;
                        }
                    }
                    if (serial.Length == 26)  // cancel epass
                    {
                        ///check time cancel
                        var getTime = mDataLayer.GetTimeSerial(serial);
                        if (getTime.Rows.Count == 0)
                        {
                            Log(serial, Color.Red, Language.Languages.ContainsKey("104") ? Language.Languages["104"] : "SERIAL NOT FOUND !");
                            lbInputScan.Text = string.Empty;
                            MarkStatus(false);
                            return;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(getTime.Rows[0]["i_dte_created"].ToString()))
                            {
                                lbInputScan.Text = string.Empty;
                                return;
                            }
                            else
                            {
                                var setDate = Convert.ToDateTime(getTime.Rows[0]["i_dte_created"].ToString());
                                if ((DateTime.Now - setDate).Hours > 24)
                                {
                                    Log(string.Empty, Color.Red, Language.Languages.ContainsKey("105") ? Language.Languages["105"] : "CANCELLATION TIME IS OVER !");
                                    lbInputScan.Text = string.Empty;
                                    MarkStatus(false);
                                    return;
                                }
                            }
                        }
                        // check serial 
                        var getExistBox = mDataLayer.CheckSerialBox(serial, mSettingInfo.LineInfos.WSCode);
                        if (getExistBox.Rows.Count > 0)// box created
                        {
                            if (!string.IsNullOrEmpty(getExistBox.Rows[0]["i_box_no"].ToString()))
                            {
                                Log(string.Empty, Color.Red, Language.Languages.ContainsKey("106") ? Language.Languages["106"] : "AREADY MADE BOX LABEL, PLEASE CANCEL BOX LABEL !");
                                lbInputScan.Text = string.Empty;
                                MarkStatus(false);
                                return;
                            }
                        }
                        var getHistoryScan = mDataLayer.CheckSerialScan(serial, mSettingInfo.LineInfos.WSCode);
                        if (getHistoryScan.Rows.Count > 0)
                        {
                            string po = getHistoryScan.Rows[0]["i_po"].ToString();
                            if (mDataLayer.DeleteSerial(serial, mSettingInfo.LineInfos.WSCode))
                            {
                                Log(serial, Color.Green, Language.Languages.ContainsKey("107") ? Language.Languages["107"] : "CANCEL LABEL SUCCESS");
                                new Thread(new ThreadStart(() => { LoadSerialScan(po, mSettingInfo.LineInfos.WSCode); })).Start();
                                lbInputScan.Text = string.Empty;
                                MarkStatus(true);
                                cbCancelRS.Checked = false;
                                LoadDailyPlan(1, string.Empty);
                                if (mSetInfo != null)  // selected po
                                {
                                    int qty = 0;
                                    int.TryParse(mDataLayer.GetPS(po, mSettingInfo.LineInfos.WSCode).Rows[0]["qty"].ToString(), out qty);
                                    mSetInfo.ActualPS = qty;
                                    mSetInfo.Actual = mSetInfo.Actual == 0 ? 0 : mSetInfo.Actual - 1;
                                    mSetInfo.Bal = mSetInfo.Plan - mSetInfo.Actual;
                                    lbProQty.Text = mSetInfo.ActualPS.ToString();
                                    lbBalQtyy.Text = mSetInfo.Bal.ToString();
                                    lbActualQtyy.Text = mSetInfo.Actual.ToString();
                                    if (mDataLayer.GetReflect(mSettingInfo.LineInfos.ProcessCode, mSettingInfo.LineInfos.WSCode).Rows.Count > 0) // check reflect
                                    {
                                        new Thread(new ThreadStart(() => { new DataLayer(StaticSetting.Connection).UpdateProdPlan(mSetInfo, mSetInfo.Actual == 1 ? true : false, mPOStatus["IN-PROCESSING"], mPOStatus["COMPLETED"]); })).Start();
                                    }
                                }
                                else // don't select po
                                {
                                    SetInfo setInfo = new SetInfo();
                                    setInfo.PO = po;
                                    int qty = 0;
                                    int.TryParse(mDataLayer.GetPS(po, mSettingInfo.LineInfos.WSCode).Rows[0]["qty"].ToString(), out qty);
                                    setInfo.ActualPS = qty;
                                    var getPOQty = mDataLayer.GetPOQty(po, mSettingInfo.LineInfos.WSCode);
                                    int Actualqty = 0;
                                    if (getPOQty.Rows.Count > 0)
                                    {
                                        int.TryParse(getPOQty.Rows[0]["qty"].ToString(), out Actualqty);
                                    }
                                    setInfo.Actual = Actualqty;

                                    int plan = 0;
                                    var getPlan = mDataLayer.GetPlanQty(po);
                                    if (getPlan.Rows.Count > 0) int.TryParse(getPlan.Rows[0]["i_plan_qty"].ToString(), out plan);
                                    setInfo.Plan = plan;
                                    lbProQty.Text = setInfo.ActualPS.ToString();
                                    lbBalQtyy.Text = (setInfo.Plan - setInfo.Actual).ToString();
                                    lbActualQtyy.Text = setInfo.Actual.ToString();
                                    if (mDataLayer.GetReflect(mSettingInfo.LineInfos.ProcessCode, mSettingInfo.LineInfos.WSCode).Rows.Count > 0)
                                    {
                                        new Thread(new ThreadStart(() => { new DataLayer(StaticSetting.Connection).UpdateProdPlan(setInfo, setInfo.Actual == 1 ? true : false, mPOStatus["IN-PROCESSING"], mPOStatus["COMPLETED"]); })).Start();
                                    }
                                }
                                return;
                            }
                        }
                        else
                        {
                            Log(serial, Color.Red, Language.Languages.ContainsKey("104") ? Language.Languages["104"] : "LABEL NOT FOUND!");
                            lbInputScan.Text = string.Empty;
                            MarkStatus(false);
                            return;
                        }
                    }
                    else if (serial.Length == 24)
                    {
                        var getTime = mDataLayer.GetTimeBox(serial);
                        if (getTime.Rows.Count == 0)
                        {
                            Log(serial, Color.Red, Language.Languages.ContainsKey("118") ? Language.Languages["118"] : "BOX NOT FOUND !");
                            lbInputScan.Text = string.Empty;
                            MarkStatus(false);
                            return;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(getTime.Rows[0]["i_dte_log_i"].ToString()))
                            {
                                lbInputScan.Text = string.Empty;
                                return;
                            }
                            else
                            {
                                var setDate = Convert.ToDateTime(getTime.Rows[0]["i_dte_log_i"].ToString());
                                if ((DateTime.Now - setDate).Hours > 24)
                                {
                                    Log(string.Empty, Color.Red, Language.Languages.ContainsKey("105") ? Language.Languages["105"] : "CANCELLATION TIME IS OVER !");
                                    lbInputScan.Text = string.Empty;
                                    MarkStatus(false);
                                    return;
                                }
                            }
                        }
                        var getStatusBox = mDataLayer.GetBoxStatus(serial);
                        if (getStatusBox.Rows.Count > 0)
                        {
                            if (getStatusBox.Rows[0]["i_state"].ToString() != "RUNNING")
                            {
                                Log(string.Empty, Color.Red, "BOX NO IS ALREADY CANCELLED !");
                                lbInputScan.Text = string.Empty;
                                MarkStatus(false);
                                return;
                            }
                        }
                        else
                        {
                            Log(string.Empty, Color.Red, "BOX NO DOESN'T EXIST !");
                            lbInputScan.Text = string.Empty;
                            MarkStatus(false);
                            return;
                        }
                        if (mDataLayer.CancelBox(serial))
                        {
                            var getPo = mDataLayer.GetPO(serial);
                            if (getPo.Rows.Count > 0)
                            {
                                string po = getPo.Rows[0]["i_po"].ToString();
                                mDataLayer.CancelSerialBox(serial, mSettingInfo.LineInfos.WSCode, mSettingInfo.LineInfos.FactoryCode);
                                Log(serial, Color.Green, Language.Languages.ContainsKey("125") ? Language.Languages["125"] : "CANCEL BOX SUCCESS");
                                SetInfo setInfo = new SetInfo();
                                setInfo.Serial = serial;
                                setInfo.PO = po;
                                int plan = 0;
                                var getPlan = mDataLayer.GetPlanQty(po);
                                if (getPlan.Rows.Count > 0) int.TryParse(getPlan.Rows[0]["i_plan_qty"].ToString(), out plan);
                                setInfo.Plan = plan;
                                cbCancelRS.Checked = false;
                                lbInputScan.Text = string.Empty;
                                lbProQty.Text = string.Empty;
                                var getPOQty = mDataLayer.GetPOQty(po, mSettingInfo.LineInfos.WSCode);
                                int Actualqty = 0;
                                if (getPOQty.Rows.Count > 0)
                                {
                                    int.TryParse(getPOQty.Rows[0]["qty"].ToString(), out Actualqty);
                                }
                                setInfo.Actual = Actualqty;
                                if (mSetInfo != null) mSetInfo.Actual = Actualqty;
                                if (mDataLayer.GetReflect(mSettingInfo.LineInfos.ProcessCode, mSettingInfo.LineInfos.WSCode).Rows.Count > 0)
                                {
                                    mDataLayer.UpdateProdPlan(setInfo, setInfo.Actual == 1 ? true : false, mPOStatus["IN-PROCESSING"], mPOStatus["COMPLETED"]);
                                }
                                lbActualQtyy.Text = setInfo.Actual.ToString();
                                lbBalQtyy.Text = (setInfo.Plan - setInfo.Actual).ToString();
                                new Thread(new ThreadStart(() =>
                                {
                                    SearchBoxPO(po, mSettingInfo.LineInfos.WSCode);
                                    LoadSerialScan(po, mSettingInfo.LineInfos.WSCode);
                                })).Start();
                                MarkStatus(true);
                                new Thread(new ThreadStart(() => { LoadDailyPlan(1, string.Empty); })).Start();
                                return;
                            }
                        }
                        else
                        {
                            Log(serial, Color.Red, Language.Languages.ContainsKey("108") ? Language.Languages["108"] : "CANCEL FAIL");
                            lbInputScan.Text = string.Empty;
                            MarkStatus(false);
                            return;
                        }
                    }
                }
                else
                {
                    if (cbPackingSize.Checked || (!cbPackingSize.Checked && !cbPOSize.Checked))
                    {
                        int itemSize = 0;
                        int.TryParse(txtItemSize.Text, out itemSize);
                        if (itemSize == 0)
                        {
                            Log(Language.Languages.ContainsKey("113") ? Language.Languages["113"] : "PLEASE INPUT ITEM SIZE!", Color.Red, string.Empty);
                            lbInputScan.Text = string.Empty;
                            return;
                        }
                    }
                    if (serial.Length == 26)
                    {
                        lbInputScan.Text = serial;

                        if (mSettingInfo.LineInfos.BarcodeYN == "N")
                        {
                            Log("MODE SCAN IS DISABLE!", Color.Red, string.Empty);
                            return;
                        }
                        if (!CheckSerialExist(serial))
                        {
                            Log(serial, Color.Red, Language.Languages.ContainsKey("126") ? Language.Languages["126"] : "LABEL DOESN'T EXIST");
                            lbInputScan.Text = string.Empty;
                            MarkStatus(false);
                            return;
                        }
                        if (mDataLayer.CheckSerialScan(serial, mSettingInfo.LineInfos.WSCode).Rows.Count > 0)
                        {
                            Log(serial, Color.Red, Language.Languages.ContainsKey("109") ? Language.Languages["109"] : "SERIAL SCANNED");
                            lbInputScan.Text = string.Empty;
                            MarkStatus(false);
                            return;
                        }

                        if (StaticSetting.AutoPO == "N") /// Manual
                        {
                            lbInputScan.Text = string.Empty;
                            lbInputScan.Focus();
                            if (!AllowProd(mSetInfo.PlanDate))
                            {
                                Log(string.Empty, Color.Red, Language.Languages.ContainsKey("139") ? Language.Languages["139"] : "PLANTIME IS OVER !");
                                return;
                            }
                            /// Prod input
                            if (mDataLayer.ProdStatus(mPOStatus["IN-PROCESSING"], mPOStatus["PLANNED"], mPOStatus["PENDING"], mSetInfo.PO).Rows.Count == 0)
                            {
                                Log(string.Empty, Color.Red, "CANN'T INPUT PRODUCTION RESULT!");
                                return;
                            }
                            if (!mSettingInfo.LineInfos.ScanSerial) // input box
                            {
                                Log(Language.Languages.ContainsKey("103") ? Language.Languages["103"] : "SYSTEM MODE IS INPUT BOX!", Color.Red, string.Empty);
                                return;
                            }
                            if (mSetInfo != null)
                            {
                                if (mSetInfo.Actual >= mSetInfo.Plan)
                                {
                                    ResetUI();
                                    lbInputScan.Text = string.Empty;
                                    return;
                                }
                                if (serial.Substring(4, 7) == mSetInfo.Model)
                                {
                                    mSetInfo.LineSerial = serial.Substring(11, 4);
                                    if (string.IsNullOrEmpty(mSetInfo.LotNo))
                                    {
                                        mSetInfo.LotNo = serial.Substring(15, 7);
                                    }
                                    if (mSetInfo.ItemModes == ItemMode.PACKING_SIZE)
                                    {
                                        int qty = 0;
                                        int.TryParse(mDataLayer.GetPS(mSetInfo.PO, mSettingInfo.LineInfos.WSCode).Rows[0]["qty"].ToString(), out qty);
                                        mSetInfo.ActualPS = qty;
                                        if (mSetInfo.ActualPS > mSetInfo.PackingSize)
                                        {
                                            Log(serial, Color.Red, "PLEASE SELECT ITEM SIZE!");
                                            lbInputScan.Text = string.Empty;
                                            return;
                                        }
                                        lbProQty.Text = mSetInfo.ActualPS.ToString();
                                    }
                                    mSetInfo.Serial = serial;
                                    mSetInfo.Actual++;
                                    mSetInfo.ActualPS++;
                                    lbActualQtyy.Text = mSetInfo.Actual.ToString();
                                    mSetInfo.Bal = mSetInfo.Plan - mSetInfo.Actual;
                                    lbBalQtyy.Text = mSetInfo.Bal.ToString();
                                    mSetInfo.Reflect = "N";
                                    if (mDataLayer.GetReflect(mSettingInfo.LineInfos.ProcessCode, mSettingInfo.LineInfos.WSCode).Rows.Count > 0)
                                    {
                                        mSetInfo.Reflect = "Y";
                                        mSetInfo.Final = "Y";
                                        new Thread(new ThreadStart(() => { new DataLayer(StaticSetting.Connection).UpdateProdPlan(mSetInfo, mSetInfo.Actual == 1 ? true : false, mPOStatus["IN-PROCESSING"], mPOStatus["COMPLETED"]); })).Start();
                                    }
                                    if (mDataLayer.SaveSerial(mSetInfo))
                                    {
                                        new Thread(new ThreadStart(() =>
                                        {
                                            LoadSerialScan(mSetInfo.PO, mSettingInfo.LineInfos.WSCode);
                                            LoadDailyPlan(1, string.Empty);
                                        })).Start();
                                        Log(serial, Color.Green, "OK");
                                        MarkStatus(true);
                                        tmTactTime.Enabled = false;
                                        mTactTime = 0;
                                        lbActualTT.TextBox = mTactTime.ToString();
                                        tmTactTime.Enabled = true;
                                    }

                                    if (mSetInfo.ItemModes == ItemMode.PACKING_SIZE)
                                    {
                                        lbProQty.Text = mSetInfo.ActualPS.ToString();
                                        if (mSetInfo.PackingSize == 0)
                                        {
                                            lbInputScan.Text = string.Empty;
                                            return;
                                        }
                                        if (mSetInfo.ActualPS == mSetInfo.PackingSize)
                                        {
                                            mSetInfo.LabelBox = CreateLabelBox(mSetInfo, true);
                                            if (PrintQR(ref mSetInfo) && APISaveBox(mSetInfo))
                                            {
                                                new Thread(new ThreadStart(() => { UpdateBoxLabel(mSetInfo); })).Start();
                                                new Thread(new ThreadStart(() => { SearchBoxPO(mSetInfo.PO, mSettingInfo.LineInfos.WSCode); })).Start();
                                                BeginInvoke(new Action(() => { dtgw3.Rows.Clear(); }));
                                                MarkStatus(true);
                                                mSetInfo.ActualPS = 0;
                                                tmTactTime.Enabled = false;
                                                mTactTime = 0;
                                                lbActualTT.Invoke(new MethodInvoker(() => { lbActualTT.TextBox = mTactTime.ToString(); }));
                                                lbProQty.Text = mSetInfo.ActualPS.ToString();
                                                lbPlanQTT.TextBox = "0";
                                                lbInputScan.Text = string.Empty;
                                                Log(mSetInfo.LabelBox, Color.Green, Language.Languages.ContainsKey("127") ? Language.Languages["127"] : "CREATED BOX!");
                                                return;
                                            }
                                        }
                                    }
                                    else if (mSetInfo.ItemModes == ItemMode.PO_SIZE)
                                    {
                                        lbProQty.Text = mSetInfo.Actual.ToString();
                                        txtItemSize.Text = mSetInfo.Plan.ToString();
                                        if (mSetInfo.Actual == mSetInfo.Plan)
                                        {
                                            mSetInfo.LabelBox = CreateLabelBox(mSetInfo, true);
                                            if (PrintQR(ref mSetInfo) && APISaveBox(mSetInfo))
                                            {
                                                new Thread(new ThreadStart(() =>
                                                {
                                                    UpdateBoxLabel(mSetInfo);
                                                })).Start();
                                                new Thread(new ThreadStart(() =>
                                                {
                                                    SearchBoxPO(mSetInfo.PO, mSettingInfo.LineInfos.WSCode);
                                                })).Start();
                                                BeginInvoke(new Action(() => { dtgw3.Rows.Clear(); }));
                                                new Thread(new ThreadStart(() =>
                                                {
                                                    new DataLayer(StaticSetting.Connection).UpdateProdFinal(mSetInfo);
                                                })).Start();
                                                MarkStatus(true);
                                                ResetUI();
                                                tmTactTime.Enabled = false;
                                                mTactTime = 0;
                                                lbActualTT.Invoke(new MethodInvoker(() => { lbActualTT.TextBox = mTactTime.ToString(); }));
                                                lbProQty.Text = lbPlanQTT.TextBox = "0";
                                                lbCurrentPO.Text = lbPlanQtyy.Text = lbActualQtyy.Text = lbBalQtyy.Text = string.Empty;
                                                lbInputScan.Text = string.Empty;
                                                Log(mSetInfo.LabelBox, Color.Green, Language.Languages.ContainsKey("127") ? Language.Languages["127"] : "CREATED BOX!");
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Log(serial, Color.Red, Language.Languages.ContainsKey("110") ? Language.Languages["110"] : "DIFF MODEL");
                                        lbInputScan.Text = string.Empty;
                                        MarkStatus(false);
                                    }
                                }
                            }
                            else
                            {
                                Log("", Color.Red, Language.Languages.ContainsKey("111") ? Language.Languages["111"] : "PLEASE SELECT PO!");
                                lbInputScan.Text = string.Empty;
                                MarkStatus(false);
                            }
                        }
                        else  ///Auto select PO
                        {
                            lbInputScan.Text = string.Empty;
                            lbInputScan.Focus();
                            var getPo = new DataLayer(StaticSetting.Connection).GetPOPLan(serial, mSettingInfo.LineInfos.ProcessCode);
                            if (getPo.Rows.Count == 0)
                            {
                                Log(serial, Color.Red, Language.Languages.ContainsKey("128") ? Language.Languages["128"] : "PO NOT FOUND");
                                MarkStatus(false);
                                lbInputScan.Text = string.Empty;
                                return;
                            }
                            else
                            {
                                if (!AllowProd(getPo.Rows[0]["i_plan_date"].ToString()))
                                {
                                    Log(string.Empty, Color.Red, Language.Languages.ContainsKey("139") ? Language.Languages["139"] : "PLANTIME IS OVER !");
                                    return;
                                }
                                string prodStatus = getPo.Rows[0]["i_prod_status"].ToString();
                                if (prodStatus != mPOStatus["IN-PROCESSING"] || prodStatus != mPOStatus["PLANNED"] || prodStatus != mPOStatus["PENDING"])
                                {
                                    Log(string.Empty, Color.Red, "CANN'T INPUT PRODUCTION RESULT!");
                                    return;
                                }
                            }
                            if (getPo.Rows[0]["line_code"].ToString() != mSettingInfo.LineInfos.LineCode)
                            {
                                Log(serial, Color.Red, Language.Languages.ContainsKey("112") ? Language.Languages["112"] : "LINE DOESN'T MATCH!");
                                MarkStatus(false);
                                lbInputScan.Text = string.Empty;
                                return;
                            }
                            else
                            {
                                mSetInfo = new SetInfo();
                                mSetInfo.Model = serial.Substring(4, 7);
                                if (mSettingInfo.Printers.Count > 0)
                                    mSetInfo.PrinterName = string.IsNullOrEmpty(mSettingInfo.Printers[0].PrinterName) ? string.Empty : mSettingInfo.Printers[0].PrinterName;
                                mSetInfo.PO = getPo.Rows[0]["i_prod_order_no"].ToString();
                                mSetInfo.ModelName = getPo.Rows[0]["i_name"].ToString();
                                mSetInfo.POType = getPo.Rows[0]["i_po_type"].ToString();
                                mSetInfo.Input = mSettingInfo.LineInfos.Input;
                                mSetInfo.Final = mSettingInfo.LineInfos.Final;
                                mSetInfo.LotNo = serial.Substring(15, 7);
                                mSetInfo.LineSerial = serial.Substring(11, 4);
                                mSetInfo.Line = mSettingInfo.LineInfos.LineCode;
                                mSetInfo.LineName = mSettingInfo.LineInfos.LineName;
                                mSetInfo.ProdGRId = (mLineInfo.ContainsKey(mSetInfo.PO)) ? mLineInfo[mSetInfo.PO].ProdGRId : string.Empty;
                                mSetInfo.Reflect = mSettingInfo.LineInfos.Reflect;
                                mSetInfo.WSCode = mSettingInfo.LineInfos.WSCode;
                                mSetInfo.Factory = mSettingInfo.LineInfos.FactoryCode;
                                mSetInfo.Plan = int.Parse(getPo.Rows[0]["i_plan_qty"].ToString());
                                mSetInfo.ProcessCode = getPo.Rows[0]["i_code"].ToString();
                                mSetInfo.ModelID = int.Parse(getPo.Rows[0]["model_id"].ToString());
                                mSetInfo.Remark = string.Empty;
                                mSetInfo.Verison = int.Parse(getPo.Rows[0]["i_version"].ToString());
                                mSetInfo.State = getPo.Rows[0]["i_state"].ToString();
                                var getPOQty = mDataLayer.GetPOQty(mSetInfo.PO, mSettingInfo.LineInfos.WSCode);
                                if (getPOQty.Rows.Count > 0)
                                {
                                    int qty = 0;
                                    int.TryParse(getPOQty.Rows[0]["qty"].ToString(), out qty);
                                    mSetInfo.Actual = qty;
                                }
                                //// update UI
                                ///

                                if (cbPOSize.Checked)
                                {
                                    mSetInfo.ItemModes = ItemMode.PO_SIZE;
                                    txtItemSize.Text = mSetInfo.Plan.ToString();
                                }
                                else
                                {
                                    mSetInfo.ItemModes = ItemMode.PACKING_SIZE;
                                }
                                if (mSetInfo.ItemModes == ItemMode.PACKING_SIZE)
                                {
                                    int size = 0;
                                    int.TryParse(txtItemSize.Text, out size);
                                    mSetInfo.PackingSize = size;
                                    if (mSetInfo.PackingSize == 0)
                                    {
                                        Log(serial, Color.Red, "PLEASE INPUT ITEM SIZE!");
                                        lbInputScan.Text = string.Empty;
                                        return;
                                    }
                                    int qty = 0;
                                    int.TryParse(mDataLayer.GetPS(mSetInfo.PO, mSettingInfo.LineInfos.WSCode).Rows[0]["qty"].ToString(), out qty);
                                    mSetInfo.ActualPS = qty;
                                    if (mSetInfo.ActualPS > mSetInfo.PackingSize)
                                    {
                                        return;
                                    }
                                }

                                mSetInfo.UnitID = serial.Substring(2, 2);
                                if (mSetInfo.Actual >= mSetInfo.Plan)
                                {
                                    Log("SET SCANNED FULL", Color.Red, "");
                                    lbInputScan.Text = string.Empty;
                                    mSetInfo = null;
                                    return;
                                }
                                lbCurrentPO.Text = mSetInfo.PO;
                                lbPlanQTT.TextBox = getPo.Rows[0]["i_tact_time"].ToString();
                            }
                            mSetInfo.Actual++;
                            mSetInfo.ActualPS++;
                            mSetInfo.Bal = mSetInfo.Plan - mSetInfo.Actual;
                            lbPlanQtyy.Text = mSetInfo.Plan.ToString();
                            lbActualQtyy.Text = mSetInfo.Actual.ToString();
                            lbBalQtyy.Text = mSetInfo.Bal.ToString();

                            mSetInfo.Serial = serial;
                            mSetInfo.Reflect = "N";
                            if (mDataLayer.GetReflect(mSettingInfo.LineInfos.ProcessCode, mSettingInfo.LineInfos.WSCode).Rows.Count > 0)
                            {
                                mSetInfo.Reflect = "Y";
                                mSetInfo.Final = "Y";
                                new Thread(new ThreadStart(() => { new DataLayer(StaticSetting.Connection).UpdateProdPlan(mSetInfo, mSetInfo.Actual == 1 ? true : false, mPOStatus["IN-PROCESSING"], mPOStatus["COMPLETED"]); })).Start();
                            }
                            if (mDataLayer.SaveSerial(mSetInfo))
                            {

                                new Thread(new ThreadStart(() =>
                                {
                                    LoadSerialScan(mSetInfo.PO, mSettingInfo.LineInfos.WSCode);
                                    LoadDailyPlan(2, mSetInfo.PO);
                                })).Start();

                                Log(serial, Color.Green, "OK");
                                MarkStatus(true);
                                tmTactTime.Enabled = false;
                                mTactTime = 0;
                                lbActualTT.TextBox = mTactTime.ToString();
                                tmTactTime.Enabled = true;
                            }

                            if (mSetInfo.ItemModes == ItemMode.PACKING_SIZE)
                            {
                                lbProQty.Text = mSetInfo.ActualPS.ToString();

                                if (mSetInfo.ActualPS == mSetInfo.PackingSize)
                                {
                                    mSetInfo.LabelBox = CreateLabelBox(mSetInfo, true);
                                    if (PrintQR(ref mSetInfo) && APISaveBox(mSetInfo))
                                    {
                                        new Thread(new ThreadStart(() => { UpdateBoxLabel(mSetInfo); })).Start();
                                        new Thread(new ThreadStart(() => { SearchBoxPO(mSetInfo.PO, mSettingInfo.LineInfos.WSCode); })).Start();
                                        BeginInvoke(new Action(() => { dtgw3.Rows.Clear(); }));
                                        //mDataLayer.UpdateProd(mSetInfo);
                                        MarkStatus(true);
                                        mSetInfo.ActualPS = 0;
                                        tmTactTime.Enabled = false;
                                        mTactTime = 0;
                                        lbActualTT.Invoke(new MethodInvoker(() => { lbActualTT.TextBox = mTactTime.ToString(); }));
                                        lbProQty.Text = lbPlanQTT.TextBox = "0";
                                        lbInputScan.Text = string.Empty;
                                        Log(mSetInfo.LabelBox, Color.Green, Language.Languages.ContainsKey("127") ? Language.Languages["127"] : "CREATED BOX!");
                                        return;
                                    }
                                }
                            }
                            else if (mSetInfo.ItemModes == ItemMode.PO_SIZE)
                            {
                                lbProQty.Text = mSetInfo.Actual.ToString();
                                txtItemSize.Text = mSetInfo.Plan.ToString();
                                if (mSetInfo.Actual == mSetInfo.Plan)
                                {
                                    mSetInfo.LabelBox = CreateLabelBox(mSetInfo, true);
                                    //// in tem
                                    if (PrintQR(ref mSetInfo) && APISaveBox(mSetInfo))
                                    {
                                        new Thread(new ThreadStart(() => { UpdateBoxLabel(mSetInfo); })).Start();
                                        new Thread(new ThreadStart(() => { SearchBoxPO(mSetInfo.PO, mSettingInfo.LineInfos.WSCode); })).Start();
                                        BeginInvoke(new Action(() => { dtgw3.Rows.Clear(); }));
                                        new Thread(new ThreadStart(() =>
                                        {
                                            new DataLayer(StaticSetting.Connection).UpdateProdFinal(mSetInfo);
                                        })).Start();
                                        MarkStatus(true);
                                        mSetInfo = null;
                                        ResetUI();
                                        tmTactTime.Enabled = false;
                                        mTactTime = 0;
                                        lbActualTT.Invoke(new MethodInvoker(() => { lbActualTT.TextBox = mTactTime.ToString(); }));
                                        lbProQty.Text = lbPlanQTT.TextBox = "0";
                                        lbCurrentPO.Text = lbPlanQtyy.Text = lbActualQtyy.Text = lbBalQtyy.Text = string.Empty;
                                        lbInputScan.Text = string.Empty;
                                        Log(mSetInfo.LabelBox, Color.Green, Language.Languages.ContainsKey("127") ? Language.Languages["127"] : "CREATED BOX!");
                                        return;
                                    }
                                }
                            }
                            //LoadDailyPlanDaily();
                        }
                    }
                    else
                    {
                        Log(serial, Color.Red, Language.Languages.ContainsKey("114") ? Language.Languages["114"] : "FORMAT WRONG");
                        lbInputScan.Text = string.Empty;
                        MarkStatus(false);
                    }
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
            lbInputScan.Text = string.Empty;
            GC.Collect();
        }

        private bool UpdateBoxLabel(SetInfo setInfo)
        {
            try
            {
                return new DataLayer(StaticSetting.Connection).UpdateBoxScan(setInfo);
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
                return false;
            }
        }
        private bool PrintQR(ref SetInfo setInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(setInfo.PrinterName))
                {
                    Log(string.Empty, Color.Red, Language.Languages.ContainsKey("129") ? Language.Languages["129"] : "PRINT FAIL,SELECT PRINTER AND RE-PRINT!");
                    return true;
                }
                setInfo.LabelHeight = mPrintSetting.PaperHeight;
                setInfo.LabelWidth = mPrintSetting.PaperWidth;
                mBarcodeSettings = new BarcodeSettings
                {
                    Type = BarCodeType.QRCode,
                    ShowTopText = false,
                    ShowText = false,
                    ShowTextOnBottom = false,
                    Data = setInfo.LabelBox,
                    Data2D = setInfo.LabelBox,
                    TextFont = new Font("Arial", 8.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                    QRCodeDataMode = QRCodeDataMode.AlphaNumber
                };
                float size = (float)(mPrintSetting.QRSize * 0.1);
                mBarcodeSettings.X = size;
                BarCodeGenerator code = new BarCodeGenerator(mBarcodeSettings);
                setInfo.QR = code.GenerateImage();
                var is_Print = new Samsung_System.ProductionPrinting(setInfo, setInfo.PrinterName, mPrintSetting);
                is_Print.Print();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void SavePrinter(string printerName)
        {
            if (mSetInfo != null)
                mSetInfo.PrinterName = printerName;
        }
        private bool SaveLabelBox(SetInfo setInfo, int id)
        {
            try
            {
                return mDataLayer.SaveBox(setInfo, id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void LoadSeq()
        {
            if (mSetInfo == null) return;
            try
            {
                var loadSeq = mDataLayer.LoadSeqBox(mSetInfo.Line, mSetInfo.PO);
                if (loadSeq.Rows.Count > 0)
                {
                    if (loadSeq.Rows[0]["i_label_box"] != null)
                    {
                        if (!string.IsNullOrEmpty(loadSeq.Rows[0]["i_label_box"].ToString()))
                        {
                            string lbBox = loadSeq.Rows[0]["i_label_box"].ToString();
                            int.TryParse(lbBox.Substring(lbBox.Length - 4), out mSeq);
                        }
                        else mSeq = 0;
                    }
                    else mSeq = 0;
                    mSeq++;
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private string CreateLabelBox(SetInfo setInfo, bool isBox)
        {
            try
            {
                DataTable getmax_lb;
                if (isBox)
                {
                    getmax_lb = mDataLayer.GetLabelBox(setInfo);
                }
                else
                {
                    getmax_lb = mDataLayer.GetLabelBoxSet(setInfo);
                }
                if (getmax_lb.Rows.Count > 0)
                {
                    mSeq = (string.IsNullOrEmpty(getmax_lb.Rows[0]["box_no"].ToString())) ? 0 : int.Parse(getmax_lb.Rows[0]["box_no"].ToString().Substring(getmax_lb.Rows[0]["box_no"].ToString().Length - 4));
                }
                else mSeq = 0;
                mSeq++;
                return "VN" + setInfo.Model + setInfo.LineSerial + setInfo.LotNo + CreateSeq(mSeq.ToString());
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        private string CreateSeq(string seq)
        {
            return seq.PadLeft(4, '0');
        }
        private bool GetPo(ref SetInfo setInfo)
        {
            foreach (var item in mLineInfo)
            {
                if (setInfo.Model == mLineInfo[item.Key].ModelCode)
                {
                    setInfo.PO = mLineInfo[item.Key].PO;
                    setInfo.ModelName = mLineInfo[item.Key].ModelName;
                    return true;
                }
            }
            return false;
        }
        private void MarkStatus(bool status)
        {
            if (!status)
            {
                lbStatus.Text = "NG";
                lbStatus.BackColor = Color.OrangeRed;
            }
            else
            {
                lbStatus.Text = "OK";
                lbStatus.BackColor = Color.SeaGreen;
            }
        }
        private void dtgwMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearchPlan_Click(object sender, EventArgs e)
        {
            btnSearchPlan.Enabled = false;
            LoadDailyPlan(1, string.Empty);
            dtgw2.Rows.Clear();
            ResetUI();
            btnSearchPlan.Enabled = true;
        }
        private void SelectPO(string po, string planDate)
        {
            try
            {
                SearchBoxPO(po, mSettingInfo.LineInfos.WSCode);
                LoadSerialScan(po, mSettingInfo.LineInfos.WSCode);
                if (mSettingInfo.LineInfos.AutoPO == "Y")
                {
                    //Log(string.Empty, Color.Red, Language.Languages.ContainsKey("130") ? Language.Languages["130"] : "Please select Auto PO is 'N' !");
                    return;
                }
                if (string.IsNullOrEmpty(po)) return;
                mSetInfo = new SetInfo();
                if (mLineInfo.ContainsKey(po))
                {
                    mSetInfo.Input = mSettingInfo.LineInfos.Input;
                    mSetInfo.Final = mSettingInfo.LineInfos.Final;
                    mSetInfo.Factory = mLineInfo[po].FactoryCode;
                    mSetInfo.PO = po;
                    mSetInfo.Model = mLineInfo[po].ModelCode;
                    mSetInfo.ModelName = mLineInfo[po].ModelName;
                    mSetInfo.ModelID = mLineInfo[po].ModelID;
                    mSetInfo.ProcessCode = mLineInfo[po].ProcessCode;
                    mSetInfo.TactTime = mLineInfo[po].TactTime;
                    mSetInfo.POType = mLineInfo[po].POType;
                    mSetInfo.LotNo = mLineInfo[po].LotNo;
                    mSetInfo.Plan = mLineInfo[po].Plan;
                    mSetInfo.Actual = mLineInfo[po].Actual;
                    mSetInfo.Bal = mSetInfo.Plan - mSetInfo.Actual;
                    mSetInfo.Line = mLineInfo[po].LineCode;
                    mSetInfo.LineSerial = mSetInfo.Line;
                    mSetInfo.ProdGRId = (mLineInfo.ContainsKey(po)) ? mLineInfo[po].ProdGRId : string.Empty;
                    mSetInfo.LineName = mSettingInfo.LineInfos.LineName;
                    mSetInfo.WSCode = mSettingInfo.LineInfos.WSCode;
                    mSetInfo.Reflect = mSettingInfo.LineInfos.Reflect;
                    mSetInfo.Verison = mLineInfo[po].Version;
                    mSetInfo.State = mLineInfo[po].State;
                    mSetInfo.PlanDate = planDate;
                    mSetInfo.Remark = string.Empty;
                    if (mSettingInfo.Printers.Count > 0)
                        mSetInfo.PrinterName = string.IsNullOrEmpty(mSettingInfo.Printers[0].PrinterName) ? string.Empty : mSettingInfo.Printers[0].PrinterName;
                    int size = 0;
                    int.TryParse(txtItemSize.Text, out size);
                    mSetInfo.PackingSize = size;
                    lbPlanQTT.TextBox = mSetInfo.TactTime.ToString();
                    lbCurrentPO.Text = mSetInfo.PO.ToString();
                    lbActualQtyy.Text = mSetInfo.Actual.ToString();
                    lbPlanQtyy.Text = mSetInfo.Plan.ToString();
                    lbBalQtyy.Text = mSetInfo.Bal.ToString();
                    if (cbPOSize.Checked)
                    {
                        mSetInfo.ItemModes = ItemMode.PO_SIZE;
                        txtItemSize.Text = mSetInfo.Plan.ToString();
                        lbProQty.Text = mSetInfo.Actual.ToString();
                    }
                    else
                    {
                        mSetInfo.ItemModes = ItemMode.PACKING_SIZE;
                        int qty = 0;
                        int.TryParse(mDataLayer.GetPS(mSetInfo.PO, mSettingInfo.LineInfos.WSCode).Rows[0]["qty"].ToString(), out qty);
                        mSetInfo.ActualPS = qty;
                        lbProQty.Text = mSetInfo.ActualPS.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private void dtgwMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;
                string po = dtgwMain.Rows[e.RowIndex].Cells[4].Value.ToString();
                string planDate = dtgwMain.Rows[e.RowIndex].Cells[0].Value.ToString();
                SelectPO(po, planDate);
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private void cbPackingSize_CheckedStateChanged(object sender, EventArgs e)
        {
            if (cbPackingSize.Checked)
            {
                MsgBox msg = new MsgBox(MgsLog.Confirm, Language.Languages.ContainsKey("116") ? Language.Languages["116"] : "If you change item size, production result label will be printed based on current result?");
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    txtItemSize.ReadOnly = false;
                    lbInputScan.Focus();
                    cbPOSize.Checked = false;
                    txtItemSize.Text = "0";
                    if (mSetInfo == null)
                    {
                        return;
                    }
                    else
                    {
                        int size = 0;
                        int.TryParse(txtItemSize.Text, out size);
                        if (size > mSetInfo.Plan)
                        {
                            Utinity.Mgs(MgsLog.Error, Language.Languages.ContainsKey("124") ? Language.Languages["124"] : "Packing size is less than PO Size!");
                            cbPackingSize.Checked = false;
                            return;
                        }
                        mSetInfo.PackingSize = int.Parse(txtItemSize.Text);
                        mSetInfo.ItemModes = ItemMode.PACKING_SIZE;
                    }
                }
                else cbPackingSize.Checked = false;
            }
        }

        private void cbPOSize_CheckedStateChanged(object sender, EventArgs e)
        {
            if (cbPOSize.Checked)
            {
                MsgBox msg = new MsgBox(MgsLog.Confirm, Language.Languages.ContainsKey("116") ? Language.Languages["116"] : "If you change item size, production result label will be printed based on current result?");
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    cbPackingSize.Checked = false;
                    txtItemSize.ReadOnly = true;
                    lbInputScan.Focus();
                    if (mSetInfo == null)
                    {
                        return;
                    }
                    else
                    {
                        txtItemSize.Text = mSetInfo.Plan.ToString();
                        mSetInfo.ItemModes = ItemMode.PO_SIZE;
                    }
                }
                else cbPOSize.Checked = false;
            }
        }

        private void cbCancelRS_CheckedStateChanged(object sender, EventArgs e)
        {
            if (cbCancelRS.Checked)
            {
                MsgBox msg = new MsgBox(MgsLog.Confirm, Language.Languages.ContainsKey("131") ? Language.Languages["131"] : "If you select it, production result label will be deleted on current result?");
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    cbPackingSize.Enabled = cbPOSize.Enabled = false;
                }
                else
                {
                    cbCancelRS.Checked = false;
                }
            }
            else
            {
                cbPackingSize.Enabled = cbPOSize.Enabled = true;
            }
        }
        private int mTactTime = 0;
        private void tmTactTime_Tick(object sender, EventArgs e)
        {
            mTactTime++;
            lbActualTT.TextBox = mTactTime.ToString();
        }

        private void btnSearchBox_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchBox.Text))
            {
                Utinity.Mgs(MgsLog.Notice, Language.Languages.ContainsKey("132") ? Language.Languages["132"] : "Please input label box");
                return;
            }
            SearchBox(txtSearchBox.Text.ToUpper());
            txtSearchBox.Text = string.Empty;
        }
        private void SearchBox(string box)
        {
            try
            {
                dtgw2.Rows.Clear();
                string con = $"i_box_no='{box.ToUpper()}'";
                var search = new DataLayer(StaticSetting.Connection).SearchBox(con);
                var searchDate = new DataLayer(StaticSetting.Connection).SearchBoxDateTime(con);
                Dictionary<string, string> data = new Dictionary<string, string>();
                foreach (DataRow row in searchDate.Rows)
                {
                    if (!data.ContainsKey(row["i_box_no"].ToString())) data.Add(row["i_box_no"].ToString(), row["i_dte_created"].ToString());
                }
                int idx = 0;
                foreach (DataRow row in search.Rows)
                {
                    idx++;
                    string boxlabel = row["i_box_no"].ToString();
                    dtgw2.Rows.Add(idx, boxlabel, row["qty"].ToString(), data[boxlabel]);
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private void SearchBoxPO(string po, string ws)
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    dtgw2.Rows.Clear();
                    string con = $"i_po='{po.ToUpper()}' and i_box_no is not null and i_box_no <>'' and i_state='RUNNING' and i_ws_code='{ws}'";
                    var search = new DataLayer(StaticSetting.Connection).SearchBoxFPO(con);
                    var searchDate = new DataLayer(StaticSetting.Connection).SearchBoxDateTimeOFPO(con);
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    foreach (DataRow row in searchDate.Rows)
                    {
                        if (!data.ContainsKey(row["i_box_no"].ToString())) data.Add(row["i_box_no"].ToString(), row["i_dte_created"].ToString());
                    }
                    int idx = 0;
                    foreach (DataRow row in search.Rows)
                    {
                        idx++;
                        string boxlabel = row["i_box_no"].ToString();
                        dtgw2.Rows.Add(idx, boxlabel, row["qty"].ToString(), data[boxlabel]);
                    }
                    GC.Collect();
                }));
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }

        private void dtgw2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            string labelbox = dtgw2.Rows[e.RowIndex].Cells[1].Value.ToString();
            SearchSerial(labelbox);
        }

        private void SearchSerial(string labelBox)
        {
            try
            {
                if (!string.IsNullOrEmpty(labelBox))
                {
                    var pro_no = new DataLayer(StaticSetting.Connection).SearchBoxDetail(labelBox, mSettingInfo.LineInfos.WSCode);
                    if (pro_no.Rows.Count > 0)
                    {
                        dtgw3.Rows.Clear();
                        int idx = 0;
                        foreach (DataRow row in pro_no.Rows)
                        {
                            idx++;
                            dtgw3.Rows.Add(idx, row["i_label_serial"].ToString(), row["dte"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        private void btnSaveRemark_Click(object sender, EventArgs e)
        {
            try
            {
                btnSaveRemark.Enabled = false;
                if (string.IsNullOrEmpty(lbRemark.Text))
                {
                    Utinity.Mgs(MgsLog.Error, Language.Languages.ContainsKey("120") ? Language.Languages["120"] : "Please input remark!");
                    btnSaveRemark.Enabled = true;
                    return;
                }
                if (mSetInfo == null)
                {
                    Utinity.Mgs(MgsLog.Error, Language.Languages.ContainsKey("118") ? Language.Languages["118"] : "Set not found!");
                    btnSaveRemark.Enabled = true;
                    return;
                }
                tmTactTime.Enabled = false;
                mTactTime = 0;
                lbActualTT.Invoke(new MethodInvoker(() => { lbActualTT.TextBox = mTactTime.ToString(); }));
                mSetInfo.Remark = lbRemark.Text;
                lbPlanQTT.TextBox = lbActualTT.TextBox = "0";
                lbCurrentPO.Text = lbPlanQtyy.Text = lbActualQtyy.Text = lbBalQtyy.Text = string.Empty;
                lbInputScan.Text = string.Empty;
                //// in tem
                mSetInfo.LabelBox = CreateLabelBox(mSetInfo, true);
                var getMaxIdBox = mDataLayer.GetBoxLabel();
                if (PrintQR(ref mSetInfo) && APISaveBox(mSetInfo))
                {
                    BeginInvoke(new Action(() => { dtgw3.Rows.Clear(); }));
                    new Thread(new ThreadStart(() => { UpdateBoxLabel(mSetInfo); })).Start();
                    new Thread(new ThreadStart(() => { LoadDailyPlan(1, string.Empty); })).Start();
                    new Thread(new ThreadStart(() => { SearchBoxPO(mSetInfo.PO, mSettingInfo.LineInfos.WSCode); })).Start();
                    new Thread(new ThreadStart(() => { LoadSerialScan(mSetInfo.PO, mSettingInfo.LineInfos.WSCode); })).Start();
                    //mDataLayer.SaveRemark(mSetInfo);
                    lbRemark.Text = string.Empty;
                    lbCurrentPO.Text = lbPlanQtyy.Text = lbActualQtyy.Text = lbBalQtyy.Text = string.Empty;
                    lbStatus.Text = "OK"; lbStatus.BackColor = Color.SeaGreen;
                    mSetInfo.ActualPS = 0;
                    lbProQty.Text = mSetInfo.ActualPS.ToString();
                    mSetInfo = null;
                    Log(mSetInfo.LabelBox, Color.Green, Language.Languages.ContainsKey("127") ? Language.Languages["127"] : "CREATED BOX!");
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
            btnSaveRemark.Enabled = true;
        }

        private void Log(string msg, Color color, string status)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new MethodInvoker(() => Log(msg, color, status)));
                    return;
                }
                if (rbt_msg.IsDisposed) return;
                if (rbt_msg.Lines.Length == 1000)
                {
                    var lstline = rbt_msg.Lines[rbt_msg.Lines.Length - 1];
                    rbt_msg.Clear();
                    rbt_msg.Text = lstline;
                    return;
                }
                rbt_msg.SelectionColor = Color.Black;
                rbt_msg.SelectionFont = new Font("Microsoft Sans Serif", rbt_msg.Font.Size, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                rbt_msg.AppendText($"\n[{DateTime.Now.ToString("HH:mm:ss")}]: {msg}");
                rbt_msg.SelectionFont = new Font("Microsoft Sans Serif", rbt_msg.Font.Size + 2, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                rbt_msg.SelectionColor = color;
                rbt_msg.AppendText($" {status}");
                rbt_msg.SelectionStart = rbt_msg.Text.Length;
                rbt_msg.SelectionColor = Color.Black;
                rbt_msg.ScrollToCaret();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchBox.Text))
                {
                    Utinity.Mgs(MgsLog.Warning, Language.Languages.ContainsKey("132") ? Language.Languages["132"] : "Please input label box!");
                    return;
                }
                SetInfo setInfo = new SetInfo();
                Reason frm = new Reason(ref setInfo);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string labelBox = txtSearchBox.Text.ToUpper();
                    var re_print = mDataLayer.RePrint(labelBox);
                    if (re_print.Rows.Count > 0)
                    {
                        setInfo.ModelName = re_print.Rows[0]["model_name"].ToString();
                        setInfo.LabelBox = labelBox;
                        setInfo.LineName = re_print.Rows[0]["line_name"].ToString();
                        setInfo.PO = re_print.Rows[0]["i_po"].ToString();
                        setInfo.Actual = string.IsNullOrEmpty(re_print.Rows[0]["qty"].ToString()) ? 1 : int.Parse(re_print.Rows[0]["qty"].ToString());
                        setInfo.LotNo = labelBox.Substring(13, 7);
                        setInfo.PrinterName = mSettingInfo.Printers[0].PrinterName;
                        if (PrintQR(ref setInfo))
                        {
                            if (mDataLayer.UpdateRePrint(setInfo))
                            {
                                Log($"{setInfo.LabelBox} ", Color.Green, Language.Languages.ContainsKey("121") ? Language.Languages["121"] : "RE-PRINTED!");
                                txtSearchBox.Text = string.Empty;
                            }
                            else
                            {
                                Log($"{setInfo.LabelBox} ", Color.Red, Language.Languages.ContainsKey("122") ? Language.Languages["122"] : "UPDATE FAIL!");
                            }
                        }
                        else
                        {
                            Log($"{setInfo.LabelBox} ", Color.Red, Language.Languages.ContainsKey("123") ? Language.Languages["123"] : "PRINT FAIL!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }
        public void SaveLineConfig(string line)
        {
            mSettingInfo.LineInfos = JsonConvert.DeserializeObject<LineInfox>(line);
            LoadDailyPlan(1, string.Empty);
            dtgw2.Rows.Clear();
            lbProQty.ReadOnly = (mSettingInfo.LineInfos.BarcodeYN == "N") ? false : true;
        }
        public void ResetSet()
        {
            mSetInfo = null;
        }

        private void tmInterval_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmMain.ToMain();
        }

        private void lbProQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void lbProQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (mSettingInfo.LineInfos.AutoPO == "Y")
                    {
                        Utinity.Mgs(MgsLog.Notice, Language.Languages.ContainsKey("134") ? Language.Languages["134"] : "Please select scan manual !");
                        return;
                    }
                    if (mSetInfo is null)
                    {
                        Utinity.Mgs(MgsLog.Notice, Language.Languages.ContainsKey("111") ? Language.Languages["111"] : "Please select PO !");
                        return;
                    }

                    int itemQty, prodQty;
                    int.TryParse(txtItemSize.Text, out itemQty);
                    int.TryParse(lbProQty.Text, out prodQty);
                    if (itemQty != prodQty || prodQty == 0)
                    {
                        Utinity.Mgs(MgsLog.Warning, Language.Languages.ContainsKey("124") ? Language.Languages["124"] : "Item size is less than production qty !");
                        return;
                    }
                    else
                    {
                        mSetInfo.LabelBox = CreateLabelBox(mSetInfo, false);
                        mSetInfo.PackingSize = int.Parse(txtItemSize.Text);
                        mSetInfo.ActualPS = mSetInfo.PackingSize;
                        mSetInfo.Actual = mSetInfo.Actual + mSetInfo.PackingSize;
                        if (mDataLayer.GetReflect(mSettingInfo.LineInfos.ProcessCode, mSettingInfo.LineInfos.WSCode).Rows.Count > 0)
                        {
                            mSetInfo.Reflect = "Y";
                            mSetInfo.Final = "Y";
                            new Thread(new ThreadStart(() => { new DataLayer(StaticSetting.Connection).UpdateProdPlan(mSetInfo, mSetInfo.Actual == 1 ? true : false, mPOStatus["IN-PROCESSING"], mPOStatus["COMPLETED"]); })).Start();
                        }
                        mDataLayer.SaveSerialInput(mSetInfo);
                        if (PrintQR(ref mSetInfo) && APISaveBox(mSetInfo))
                        {
                            new Thread(new ThreadStart(() => { LoadDailyPlan(1, string.Empty); })).Start();
                            mDataLayer.UpdateProdPlan(mSetInfo, mSetInfo.Actual == 1 ? true : false, mPOStatus["IN-PROCESSING"], mPOStatus["COMPLETED"]);
                            lbProQty.Text = "0";
                            Log($"{mSetInfo.LabelBox} ", Color.Green, Language.Languages.ContainsKey("133") ? Language.Languages["133"] : "PRINTED!");
                            ResetUI();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
        }

        private void txtItemSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void lbInputScan_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void lbInputScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessScan(lbInputScan.Text.ToUpper());
            }
        }

        private void dtgwMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                this.ContextMenu = cm;
                cm.MenuItems.Add(new MenuItem("&Resize", new System.EventHandler(this.Resize)));
                cm.MenuItems.Add(new MenuItem(Language.Languages.ContainsKey("136") ? $"&{Language.Languages["136"]}" : "&Export Excel Data Plan", new System.EventHandler(this.ExportExcelPlan)));
                cm.MenuItems.Add(new MenuItem(Language.Languages.ContainsKey("137") ? $"&{Language.Languages["137"]}" : "&Export Excel Data Box", new System.EventHandler(this.ExportExcelBox)));
            }
        }
        private void Resize(Object sender, EventArgs e)
        {
            Resize frm = new Resize(this);
            frm.ShowDialog();
        }
        private void ExportExcelPlan(Object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sf = new SaveFileDialog() { RestoreDirectory = true, Title = "Save As", Filter = "Excel files (*.xls)|*.xlsx|All files (*.*)|*.*" })
                {
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application oXL;
                        Microsoft.Office.Interop.Excel._Workbook oWB;
                        Microsoft.Office.Interop.Excel._Worksheet oSheet;
                        Microsoft.Office.Interop.Excel.Range oRng;
                        object misvalue = System.Reflection.Missing.Value;
                        oXL = new Microsoft.Office.Interop.Excel.Application();
                        if (oXL == null)
                        {
                            MessageBox.Show(Language.Languages.ContainsKey("134") ? Language.Languages["134"] : "Excel is not properly installed!!");
                            return;
                        }
                        oXL.Visible = false;
                        oXL.DisplayAlerts = false;
                        //Get a new workbook.
                        oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                        oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                        oSheet.Name = "ProductionPlan";
                        int idx = 1;
                        //Add table headers going cell by cell.
                        oSheet.Cells[1, idx++] = "Plan date";
                        oSheet.Cells[1, idx++] = "Line Code";
                        oSheet.Cells[1, idx++] = "Line Name";
                        oSheet.Cells[1, idx++] = "Lot No";
                        oSheet.Cells[1, idx++] = "Production Order";
                        oSheet.Cells[1, idx++] = "Model id";
                        oSheet.Cells[1, idx++] = "Model Code";
                        oSheet.Cells[1, idx++] = "Plan Qty";
                        oSheet.Cells[1, idx++] = "Actual Qty";
                        oSheet.Cells[1, idx++] = "Bal Qty";
                        oSheet.get_Range("A1", "J1").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.RoyalBlue);
                        Microsoft.Office.Interop.Excel.Range chartRange;
                        chartRange = oSheet.get_Range("A1", "J1");
                        foreach (Microsoft.Office.Interop.Excel.Range cell in chartRange.Cells)
                        {
                            cell.BorderAround2();
                        }
                        int gRow = 1;
                        for (int i = 0; i < dtgwMain.Rows.Count; i++)
                        {
                            int col = 1;
                            gRow++;
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[0].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[1].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[2].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[3].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[4].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[5].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[6].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[7].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[8].Value.ToString();
                            oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[9].Value.ToString();
                        }
                        oXL.Visible = false;
                        oXL.UserControl = false;
                        oWB.SaveAs(sf.FileName);
                        oWB.Close();
                        oXL.Quit();
                    }
                }
            }
            catch (Exception ex)
            {
                Utinity.Mgs(MgsLog.Error, ex.Message);
            }
        }
        public void ChangeFontGW()
        {
            dtgwMain.RowsDefaultCellStyle.Font = new Font("Segoe UI", (float)mPrintSetting.DtgwSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        }
        public void ChangeFontLog()
        {
            rbt_msg.Font = new Font("Segoe UI", (float)mPrintSetting.LogSize, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            rbt_msg.Clear();
        }

        private void dtgw2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgw2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                this.ContextMenu = cm;
                cm.MenuItems.Add(new MenuItem("&Resize", new System.EventHandler(this.Resize)));
                cm.MenuItems.Add(new MenuItem(Language.Languages.ContainsKey("136") ? $"&{Language.Languages["136"]}" : "&Export Excel Data Plan", new System.EventHandler(this.ExportExcelPlan)));
                cm.MenuItems.Add(new MenuItem(Language.Languages.ContainsKey("137") ? $"&{Language.Languages["137"]}" : "&Export Excel Data Box", new System.EventHandler(this.ExportExcelBox)));
            }
        }
        private void ExportExcelBox(Object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sf = new SaveFileDialog() { RestoreDirectory = true, Title = "Save As", Filter = "Excel files (*.xls)|*.xlsx|All files (*.*)|*.*" })
                {
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string box = string.Empty;
                        if (dtgw2.Rows.Count == 0)
                        {
                            Log("", Color.Red, Language.Languages.ContainsKey("138") ? Language.Languages["138"] : "Data empty!");
                            return;
                        }
                        for (int i = 0; i < dtgw2.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(dtgw2.Rows[i].Cells[1].Value.ToString()))
                            {
                                box += $"'{dtgw2.Rows[i].Cells[1].Value.ToString()}',";
                            }
                        }
                        if (string.IsNullOrEmpty(box))
                        {
                            return;
                        }
                        else
                        {
                            box = box.Substring(0, box.Length - 1);
                        }
                        var lbBox = mDataLayer.GetLabelBoxSerial(box);
                        if (lbBox.Rows.Count > 0)
                        {
                            Microsoft.Office.Interop.Excel.Application oXL;
                            Microsoft.Office.Interop.Excel._Workbook oWB;
                            Microsoft.Office.Interop.Excel._Worksheet oSheet;
                            Microsoft.Office.Interop.Excel.Range oRng;
                            object misvalue = System.Reflection.Missing.Value;
                            oXL = new Microsoft.Office.Interop.Excel.Application();
                            if (oXL == null)
                            {
                                MessageBox.Show(Language.Languages.ContainsKey("134") ? Language.Languages["134"] : "Excel is not properly installed!!");
                                return;
                            }
                            oXL.Visible = false;
                            oXL.DisplayAlerts = false;
                            //Get a new workbook.
                            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                            oSheet.Name = "Label Box";
                            int idx = 1;
                            //Add table headers going cell by cell.
                            oSheet.Cells[1, idx++] = "Factory Code";
                            oSheet.Cells[1, idx++] = "Line Code";
                            oSheet.Cells[1, idx++] = "WS Code";
                            oSheet.Cells[1, idx++] = "PO";
                            oSheet.Cells[1, idx++] = "Model ID";
                            oSheet.Cells[1, idx++] = "Label Box";
                            oSheet.Cells[1, idx++] = "Label Epass";
                            oSheet.Cells[1, idx++] = "State";
                            oSheet.Cells[1, idx++] = "Version";
                            oSheet.Cells[1, idx++] = "UserLogin";
                            oSheet.Cells[1, idx++] = "Remark";
                            oSheet.Cells[1, idx++] = "Created";
                            oSheet.get_Range("A1", "N1").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.RoyalBlue);
                            Microsoft.Office.Interop.Excel.Range chartRange;
                            chartRange = oSheet.get_Range("A1", "N1");
                            foreach (Microsoft.Office.Interop.Excel.Range cell in chartRange.Cells)
                            {
                                cell.BorderAround2();
                            }
                            int gRow = 1;
                            foreach (DataRow row in lbBox.Rows)
                            {
                                int col = 1;
                                gRow++;
                                oSheet.Cells[gRow, col++] = row["i_factory"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_line_code"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_ws_code"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_po"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_model"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_box_no"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_label_serial"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_state"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_version"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_usr_log_i"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_remark"].ToString();
                                oSheet.Cells[gRow, col++] = row["i_dte_created"].ToString();
                            }
                            oXL.Visible = false;
                            oXL.UserControl = false;
                            oWB.SaveAs(sf.FileName);
                            oWB.Close();
                            oXL.Quit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utinity.Mgs(MgsLog.Error, ex.Message);
            }
        }

        private void dtgw3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                this.ContextMenu = cm;
                cm.MenuItems.Add(new MenuItem("&Resize", new System.EventHandler(this.Resize)));
                cm.MenuItems.Add(new MenuItem(Language.Languages.ContainsKey("136") ? $"&{Language.Languages["136"]}" : "&Export Excel Data Plan", new System.EventHandler(this.ExportExcelPlan)));
                cm.MenuItems.Add(new MenuItem(Language.Languages.ContainsKey("137") ? $"&{Language.Languages["137"]}" : "&Export Excel Data Box", new System.EventHandler(this.ExportExcelBox)));
            }
        }
        private void ExportExcelSerial(Object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;
                Microsoft.Office.Interop.Excel.Range oRng;
                object misvalue = System.Reflection.Missing.Value;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                if (oXL == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }
                oXL.Visible = false;
                oXL.DisplayAlerts = false;
                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                oSheet.Name = "Label Serial";
                int idx = 1;
                //Add table headers going cell by cell.
                oSheet.Cells[1, idx++] = "Label Box";
                oSheet.Cells[1, idx++] = "Qty";
                oSheet.Cells[1, idx++] = "Prod.Date";
                int gRow = 1;
                for (int i = 0; i < dtgwMain.Rows.Count; i++)
                {
                    int col = 1;
                    gRow++;
                    oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[1].Value.ToString();
                    oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[2].Value.ToString();
                    oSheet.Cells[gRow, col++] = dtgwMain.Rows[i].Cells[3].Value.ToString();
                }
                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.Close();
                oXL.Quit();
            }
            catch (Exception ex)
            {
                Utinity.Mgs(MgsLog.Error, ex.Message);
            }
        }

        private void ProductionResult_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private string lastInput = string.Empty;
        private void txtItemSize_TextChanged(object sender, EventArgs e)
        {
            if (cbPackingSize.Checked && mSetInfo != null)
            {
                int itemSize = 0;
                int.TryParse(txtItemSize.Text, out itemSize);
                if (itemSize > mSetInfo.Plan)
                {
                    txtItemSize.Text = lastInput;
                    return;
                }
                lastInput = txtItemSize.Text;
            }
        }

        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            var target = (int)((DateTime.Now - mStartTime).TotalMilliseconds * PlanQtyOfDay / 86400000);
            lbTargerQty.Text = target > 0 ? target.ToString() : "0";
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtFrom.Value > dtTo.Value) dtFrom.Value = mDtFrom;
        }

        private void cbbStatusPO_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtItemSize_Validated(object sender, EventArgs e)
        {
            if (cbPackingSize.Checked && mSetInfo != null)
            {
                int itemSize = 0;
                int.TryParse(txtItemSize.Text, out itemSize);
                if (itemSize <= mSetInfo.ActualPS)
                {
                    txtItemSize.Text = "0";
                    Utinity.Mgs(MgsLog.Error, Language.Languages.ContainsKey("124") ? Language.Languages["124"] : "Item size is greater than production qty !");
                    return;
                }
                mSetInfo.PackingSize = itemSize;
            }
        }

        private void iLabelS6_Click(object sender, EventArgs e)
        {

        }

        private void cbbStatusPO_SelectedIndexChanged(object sender, TreeNode node)
        {
            if (cbbStatusPO.Nodes[0].Checked)
            {
                for (int i = 1; i < cbbStatusPO.Nodes.Count; i++)
                {
                    cbbStatusPO.Nodes[i].Checked = false;
                }
            }
        }

        private void cbbTypePO_NodeSelected(object sender, TreeNode node)
        {
            if (cbbTypePO.Nodes[0].Checked)
            {
                for (int i = 1; i < cbbTypePO.Nodes.Count; i++)
                {
                    cbbTypePO.Nodes[i].Checked = false;
                }
            }
        }

        private void cbbStatusPO_NodesSelected(object sender, TreeNodeCollection nodes)
        {
            if (cbbStatusPO.Nodes[0].Checked)
            {
                for (int i = 1; i < cbbStatusPO.Nodes.Count; i++)
                {
                    cbbStatusPO.Nodes[i].Checked = false;
                }
                cbbStatusPO.Text = "All";
            }
        }

        private void cbbStatusPO_Click(object sender, EventArgs e)
        {

        }

        private void dtFrom_ValueChanged(object sender, DateTime value)
        {
            if (dtFrom.Value > dtTo.Value && mIsStart) dtFrom.Value = mDtFrom;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Settings frm = new Settings(this);
            frm.Show();
        }

        private void lbInputScan_KeyDown(object sender, KeyPressEventArgs e)
        {

        }
        public bool APISaveBox(SetInfo setInfo)
        {
            try
            {
                /// get prodction_gr_id
                int prd_gr_id = 1;
                var prd_gr = new DataLayer(StaticSetting.Connection).GetProdGRId(setInfo.PO);
                if (prd_gr.Rows.Count > 0) int.TryParse(prd_gr.Rows[0]["prod_gr_id"].ToString(), out prd_gr_id);
                // body
                APIData api = new APIData();
                factoryPK fbk = new factoryPK();
                fbk.factoryPk = mSettingInfo.LineInfos.FactoryCode + "-" + prd_gr_id;
                PK pk = new PK();
                pk.factoryCode = mSettingInfo.LineInfos.FactoryCode;
                productionGRPlanT productionGRPlan = new productionGRPlanT();
                productionGRPlan.productionGRPlan = fbk;
                productionGRPlan.generateQty = setInfo.ActualPS;
                productionGRPlan.lotNo = setInfo.LotNo;
                productionGRPlan.packageQty = 1;
                productionGRPlan.pk = pk;
                productionGRPlan.state = "RUNNING";
                productionGRPlan.reflect = setInfo.Reflect;
                productionGRPlan.finalValue = setInfo.Final;
                api.boxLabel = productionGRPlan;

                LbType lbt = new LbType();
                lbt.code = setInfo.POType;
                LabelList lbl = new LabelList();
                lbl.boxNo = setInfo.LabelBox;
                lbl.printNo = 0;
                lbl.rePrintReason = string.IsNullOrEmpty(setInfo.Reason) ? "" : setInfo.Reason;
                lbl.qty = setInfo.ActualPS;
                lbl.state = "RUNNING";
                lbl.labelType = lbt;
                lbl.pk = pk;
                api.printBoxLabelList.Add(lbl);

                var client = new RestClient("http://dthaus-api-317058744.ap-southeast-1.elb.amazonaws.com/api/rest/v1/box-label/create-production-box");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json, text/plain, */*");
                request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                request.AddHeader("Authorization", StaticSetting.Token);
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Content-Type", "application/json;charset=UTF-8");
                request.AddHeader("FeatureCode", "user.create");
                request.AddHeader("Origin", "http://dthaus.net");
                request.AddHeader("Referer", "http://dthaus.net/");
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36";
                var body = JsonConvert.SerializeObject(api);

                request.AddParameter("application/json;charset=UTF-8", body, ParameterType.RequestBody);
                var result = client.Execute(request);
                return result.IsSuccessful;
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
                return false;
            }
        }

        public class APIData
        {
            public productionGRPlanT boxLabel { get; set; }
            public List<LabelList> printBoxLabelList { get; set; } = new List<LabelList>();
        }
        public class factoryPK
        {
            public string factoryPk { get; set; }
        }
        public class PK
        {
            public string factoryCode { get; set; }
        }
        public class LbType
        {
            public string code { get; set; }
        }
        public class productionGRPlanT
        {
            public factoryPK productionGRPlan { get; set; }
            public int generateQty { get; set; }
            public int packageQty { get; set; }
            public string lotNo { get; set; }
            public string state { get; set; }
            public string reflect { get; set; }
            public string finalValue { get; set; }
            public PK pk { get; set; }
        }
        public class LabelList
        {
            public string boxNo { get; set; }
            public int printNo { get; set; }
            public string rePrintReason { get; set; }
            public int qty { get; set; }
            public string state { get; set; }
            public PK pk { get; set; }
            public LbType labelType { get; set; }
        }

        private void cbbTypePO_NodesSelected(object sender, TreeNodeCollection nodes)
        {
            if (cbbTypePO.Nodes[0].Checked)
            {
                for (int i = 1; i < cbbTypePO.Nodes.Count; i++)
                {
                    cbbTypePO.Nodes[i].Checked = false;
                }
                cbbTypePO.Text = "All";
            }
        }

        private void btnSorting_Click(object sender, EventArgs e)
        {
            Sorting frm = new Sorting(this);
            frm.ShowDialog();
        }
        public void LoadSortingPlan()
        {

        }
    }
}
