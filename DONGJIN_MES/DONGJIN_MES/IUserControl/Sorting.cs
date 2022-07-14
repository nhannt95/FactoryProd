using DONGJIN_MES.Class;
using PLCMonitoring.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl
{
    public partial class Sorting : Form
    {
        ProductionResult mSetting;
        private SetInfo mSetInfo;
        public Sorting(ProductionResult pro)
        {
            InitializeComponent();
            mSetting = pro;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private CheckBox mChk;
        private void AddCheckBoxHeader()
        {
            try
            {
                Rectangle rect = dtgw.GetCellDisplayRectangle(0, -1, true);
                rect.Location = new Point(rect.Location.X + 4, rect.Location.Y + 5);
                mChk = new CheckBox
                {
                    Size = new Size(18, 18),
                    BackColor = Color.Transparent,
                    Text = "All",
                    TextAlign = ContentAlignment.MiddleCenter,
                    TextImageRelation = TextImageRelation.Overlay,
                    Location = rect.Location
                };
                mChk.CheckedChanged += new EventHandler(chk_CheckChanged);
                dtgw.Controls.Add(mChk);
            }
            catch (Exception ex)
            {

            }
        }
        private void chk_CheckChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //dtgw.BeginEdit(false);
            dtgw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            if (dtgw.RowCount > 0)
            {
                for (int j = 0; j < dtgw.RowCount; j++)
                {
                    ((DataGridViewCheckBoxCell)dtgw[0, j]).Value = mChk.Checked;
                }
            }
            //dtgw.EndEdit();
            dtgw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Cursor.Current = Cursors.Default;
        }
        private void Sorting_Load(object sender, EventArgs e)
        {
            AddCheckBoxHeader();
        }

        private void dtgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dtgw.Rows[e.RowIndex].Cells[0].Value = !(bool)dtgw.Rows[e.RowIndex].Cells[0].Value;
            }
        }

        private void txtScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Process(txtScan.Text);
            }
        }
        BoxNo mBoxNo;
        private void Process(string serial)
        {
            try
            {
                txtScan.Text = string.Empty;
                if (serial.Length == 24)////scan box
                {
                    //check box in stock
                    if (new DataLayer(StaticSetting.Connection).CheckBoxStock(serial).Rows.Count > 0)
                    {
                        Log(string.Empty, Color.Red, " BOX CANN'T MODIFY !");
                        return;
                    }
                    mBoxNo = new BoxNo();
                    mBoxNo.Box = serial;
                    txtScan.Watermark = "Please scan epass";
                    var getBox = new DataLayer(StaticSetting.Connection).GetLabelBoxSorting(serial, mSetting.mSettingInfo.LineInfos.WSCode);
                    int idx = 0;
                    foreach (DataRow row in getBox.Rows)
                    {
                        idx++;
                        mBoxNo.Factory = row["factory_name"].ToString();
                        mBoxNo.LineCode = row["i_code"].ToString();
                        mBoxNo.LineName = row["model_name"].ToString();
                        mBoxNo.PO = row["i_po"].ToString();
                        mBoxNo.LotNo = row["lot_no"].ToString();
                        mBoxNo.PoType = row["i_po_type"].ToString();
                        mBoxNo.LineCode = row["i_line_code"].ToString();
                        mBoxNo.Model = row["model"].ToString();
                        mBoxNo.LineName = row["i_name"].ToString();
                        mBoxNo.Version = row["i_version"].ToString();
                        mBoxNo.Serial.Add(row["i_label_serial"].ToString());
                        mBoxNo.ProDate.Add(row["i_dte_created"].ToString());
                        dtgw.Rows.Add(false, idx, row["i_label_serial"].ToString(), row["i_code"].ToString(), row["model_name"].ToString(), row["i_po"].ToString(), row["lot_no"].ToString(), row["i_line_code"].ToString(), row["i_name"].ToString(), row["i_dte_created"].ToString());
                    }
                    lbFactory.Text = mBoxNo.Factory;
                    lbProdNo.Text = mBoxNo.PO;
                    lbLineName.Text = mBoxNo.LineName;
                    lbBox.Text = mBoxNo.Box;
                    lbQty.Text = mBoxNo.Serial.Count.ToString();
                    lbLotNo.Text = mBoxNo.LotNo;
                    var getReflect = new DataLayer(StaticSetting.Connection).GetReflectSorting(mSetting.mSettingInfo.LineInfos.ProcessCode, mSetting.mSettingInfo.LineInfos.WSCode);
                    if (getReflect.Rows.Count > 0)
                    {
                        mBoxNo.Reflect = getReflect.Rows[0]["reflect"].ToString();
                        mBoxNo.Final = getReflect.Rows[0]["final_yn"].ToString();
                    }
                }
                else if (serial.Length == 26)//scan label
                {
                    if (mBoxNo == null || string.IsNullOrEmpty(mBoxNo.Box))
                    {
                        Log(string.Empty, Color.Red, " PLEASE SCAN BOX FRIST !");
                        return;
                    }
                    if (!CheckSerialExist(serial))
                    {
                        Log(serial, Color.Red, "LABEL DOESN'T EXIST");
                        MarkStatus(false);
                        return;
                    }
                    if (new DataLayer(StaticSetting.Connection).CheckSerialScan(serial, mSetting.mSettingInfo.LineInfos.WSCode).Rows.Count > 0)
                    {
                        Log(serial, Color.Red, "SERIAL SCANNED");
                        MarkStatus(false);
                        return;
                    }
                    if (serial.Substring(4, 7) == mBoxNo.Model)
                    {
                        Log(string.Empty, Color.Red, "MODEL ID WRONG!");
                        MarkStatus(false);
                        return;
                    }
                    mSetInfo = new SetInfo();
                    mSetInfo.PO = mBoxNo.PO;
                    mSetInfo.Serial = serial;
                    mSetInfo.LotNo = mBoxNo.LotNo;
                    mSetInfo.POType = mBoxNo.PoType;
                    mSetInfo.LabelBox = mBoxNo.Box;
                    mSetInfo.ActualPS = mBoxNo.Serial.Count + 1;
                    mSetInfo.Reflect = mBoxNo.Reflect;
                    mSetInfo.Final = mBoxNo.Final;
                    mSetInfo.Model = mBoxNo.Model;
                    mSetInfo.WSCode = mSetting.mSettingInfo.LineInfos.WSCode;
                    mSetInfo.ProcessCode = mSetting.mSettingInfo.LineInfos.ProcessCode;
                    mSetInfo.Line = mBoxNo.LineCode;
                    mSetInfo.Verison = int.Parse(mBoxNo.Version);
                    mSetInfo.Factory = mSetting.mSettingInfo.LineInfos.FactoryCode;
                    mSetInfo.Input = mSetting.mSettingInfo.LineInfos.Input;
                    if (new DataLayer(StaticSetting.Connection).SaveSerialSort(mSetInfo))
                    {
                        dtgw.Rows.Clear();
                        mBoxNo.Serial.Clear();
                        mBoxNo.ProDate.Clear();
                        var getBox = new DataLayer(StaticSetting.Connection).GetLabelBoxSorting(mBoxNo.Box, mSetting.mSettingInfo.LineInfos.WSCode);
                        int idx = 0;
                        foreach (DataRow row in getBox.Rows)
                        {
                            idx++;
                            mBoxNo.Factory = row["factory_name"].ToString();
                            mBoxNo.LineCode = row["i_code"].ToString();
                            mBoxNo.LineName = row["model_name"].ToString();
                            mBoxNo.PO = row["i_po"].ToString();
                            mBoxNo.LotNo = row["lot_no"].ToString();
                            mBoxNo.PoType = row["i_po_type"].ToString();
                            mBoxNo.LineCode = row["i_line_code"].ToString();
                            mBoxNo.Model = row["model"].ToString();
                            mBoxNo.LineName = row["i_name"].ToString();
                            mBoxNo.Version = row["i_version"].ToString();
                            mBoxNo.Serial.Add(row["i_label_serial"].ToString());
                            mBoxNo.ProDate.Add(row["i_dte_created"].ToString());
                            dtgw.Rows.Add(false, idx, row["i_label_serial"].ToString(), row["i_code"].ToString(), row["model_name"].ToString(), row["i_po"].ToString(), row["lot_no"].ToString(), row["i_line_code"].ToString(), row["i_name"].ToString(), row["i_dte_created"].ToString());
                        }
                        lbQty.Text = mBoxNo.Serial.Count.ToString();
                        mSetting.APISaveBox(mSetInfo);
                        Log(serial, Color.Green, "OK");
                        MarkStatus(true);
                    }
                }
            }
            catch (Exception ex)
            {
                DarkSoul.MadLog.Logging(this.Name, ex.ToString(), StaticSetting.PathLog);
            }
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
        private bool CheckSerialExist(string serial)
        {
            try
            {
                return new DataLayer(StaticSetting.Connection).CheckSerialExsit(serial).Rows.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void Clear()
        {
            txtScan.Watermark = "Please input box";
            lbFactory.Text = lbProdNo.Text = lbLineName.Text = lbLotNo.Text = lbQty.Text = lbBox.Text = string.Empty;
            dtgw.Rows.Clear();
            mBoxNo = null;
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
        public class BoxNo
        {
            public string Factory { get; set; }
            public string Box { get; set; }
            public List<string> Serial { get; set; } = new List<string>();
            public string Model { get; set; }
            public string ModelCode { get; set; }
            public string ModelName { get; set; }
            public string PO { get; set; }
            public string LotNo { get; set; }
            public string LineCode { get; set; }
            public string LineName { get; set; }
            public string PoType { get; set; }
            public string Reflect { get; set; }
            public string Final { get; set; }
            public string Version { get; set; }
            public List<string> ProDate { get; set; } = new List<string>();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string serial="(";
                foreach(DataGridViewRow row in dtgw.Rows)
                {
                    if (row.Cells[0].Value.ToString() == "True") serial += $"'{row.Cells[2].Value.ToString()}',";
                }
                if(serial.Length > 1) serial = serial.Substring(0, serial.Length-1);
                serial = serial + ")";
                ///update prod scan
                new DataLayer(StaticSetting.Connection).UpdateBoxScanSort(mSetting.mSettingInfo.LineInfos.WSCode,serial);
                mBoxNo = new BoxNo();
                mBoxNo.Box =lbBox.Text ;
                dtgw.Rows.Clear();
                var getBox = new DataLayer(StaticSetting.Connection).GetLabelBoxSorting(lbBox.Text, mSetting.mSettingInfo.LineInfos.WSCode);
                int idx = 0;
                foreach (DataRow row in getBox.Rows)
                {
                    idx++;
                    mBoxNo.Factory = row["factory_name"].ToString();
                    mBoxNo.LineCode = row["i_code"].ToString();
                    mBoxNo.LineName = row["model_name"].ToString();
                    mBoxNo.PO = row["i_po"].ToString();
                    mBoxNo.LotNo = row["lot_no"].ToString();
                    mBoxNo.PoType = row["i_po_type"].ToString();
                    mBoxNo.LineCode = row["i_line_code"].ToString();
                    mBoxNo.Model = row["model"].ToString();
                    mBoxNo.LineName = row["i_name"].ToString();
                    mBoxNo.Version = row["i_version"].ToString();
                    mBoxNo.Serial.Add(row["i_label_serial"].ToString());
                    mBoxNo.ProDate.Add(row["i_dte_created"].ToString());
                    dtgw.Rows.Add(false, idx, row["i_label_serial"].ToString(), row["i_code"].ToString(), row["model_name"].ToString(), row["i_po"].ToString(), row["lot_no"].ToString(), row["i_line_code"].ToString(), row["i_name"].ToString(), row["i_dte_created"].ToString());
                }
                lbFactory.Text = mBoxNo.Factory;
                lbProdNo.Text = mBoxNo.PO;
                lbLineName.Text = mBoxNo.LineName;
                lbBox.Text = mBoxNo.Box;
                lbQty.Text = mBoxNo.Serial.Count.ToString();
                lbLotNo.Text = mBoxNo.LotNo;
                Log(string.Empty, Color.Green, "DELETED SUCCESS!");
                MarkStatus(true);
            }
            catch (Exception ex)
            {
                MarkStatus(false);
                Log(string.Empty, Color.Red, "DELETED FAILED!");
            }
        }
    }
}
