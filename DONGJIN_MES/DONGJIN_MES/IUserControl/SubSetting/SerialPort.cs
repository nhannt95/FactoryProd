using DONGJIN_MES.Class;
using MES_IO.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class SerialPort : UserControl
    {
        private List<string> mListDevices = new List<string> { "Scanner" };
        private List<string> mListComPort = new List<string>();
        private ProductionResult mProduction;
        public SerialPort(ref ProductionResult prd)
        {
            InitializeComponent();
            mProduction = prd;
            AddCheckBoxHeader();
            mListComPort = System.IO.Ports.SerialPort.GetPortNames().ToList();
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
            dtgw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            if (dtgw.RowCount > 0)
            {
                for (int j = 0; j < dtgw.RowCount; j++)
                {
                    ((DataGridViewCheckBoxCell)dtgw[0, j]).Value = mChk.Checked;
                }
            }
            //dtgw.EndEdit();
            dtgw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void SerialPort_Load(object sender, EventArgs e)
        {
            try
            {
                dtgw.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dtDevices.DataSource = mListDevices;
                for (int i = 1; i < 10; i++)
                {
                    dtgw.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgw.EditMode = DataGridViewEditMode.EditOnEnter;
                dtCom.DataSource = mListComPort;
                if (mProduction.mSettingInfo.SerialPorts != null)
                {
                    int row = 0;
                    foreach (var item in mProduction.mSettingInfo.SerialPorts)
                    {
                        row++;
                        dtgw.Rows.Add(new DataGridViewRow());
                        if (dtDevices.Items.Contains(item.Device) && !string.IsNullOrEmpty(item.Device))
                            dtgw.Rows[row - 1].Cells[1].Value = item.Device;
                        if (dtCom.Items.Contains(item.Comport) && !string.IsNullOrEmpty(item.Comport))
                            dtgw.Rows[row - 1].Cells[2].Value = item.Comport;
                        if (dtDataBits.Items.Contains(item.DataBis.ToString()) && !string.IsNullOrEmpty(item.DataBis.ToString()))
                            dtgw.Rows[row - 1].Cells[3].Value = item.DataBis.ToString();
                        if (dtBaud.Items.Contains(item.BaudRate.ToString()) && !string.IsNullOrEmpty(item.BaudRate.ToString()))
                            dtgw.Rows[row - 1].Cells[4].Value = item.BaudRate.ToString();
                        if (dtParity.Items.Contains(item.Parity) && !string.IsNullOrEmpty(item.Parity))
                            dtgw.Rows[row - 1].Cells[5].Value = item.Parity;
                        if (dtStopBit.Items.Contains(item.StopBit.ToString()) && !string.IsNullOrEmpty(item.StopBit.ToString()))
                            dtgw.Rows[row - 1].Cells[6].Value = item.StopBit.ToString();
                        if (dtPrefix.Items.Contains(item.Prefix) && !string.IsNullOrEmpty(item.Prefix))
                            dtgw.Rows[row - 1].Cells[7].Value = item.Prefix;
                        if (dtSuffix.Items.Contains(item.Suffix) && !string.IsNullOrEmpty(item.Suffix))
                            dtgw.Rows[row - 1].Cells[8].Value = item.Suffix;
                        dtgw.Rows[row - 1].Cells[9].Value = item.Remark;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void AddNewRow()
        {
            dtgw.Rows.Add(new DataGridViewRow());
            return;
            int row = dtgw.Rows.Count;

            this.Invoke((MethodInvoker)delegate
            {
                dtgw.CurrentCell = dtgw.Rows[row - 1].Cells[1];
                dtgw.BeginEdit(true);
                ComboBox cbb = (ComboBox)dtgw.EditingControl;
                cbb.SelectedIndex = 0;
            });

            //
            this.Invoke((MethodInvoker)delegate
            {
                dtgw.CurrentCell = dtgw.Rows[row - 1].Cells[2];
                dtgw.BeginEdit(true);
                ComboBox cbb1 = (ComboBox)dtgw.EditingControl;
                cbb1.SelectedIndex = 0;
            });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        private bool ComExist(string com)
        {
            foreach (var item in mProduction.mSettingInfo.SerialPorts)
            {
                if (com == item.Comport) return true;
            }
            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            for(int i = 1; i < dtgw.Rows.Count; i++)
            {
                for(int j=1; j < dtgw.Rows[i].Cells.Count-1; j++)
                {
                    if(dtgw.Rows[i].Cells[j].Value is null)
                    {
                        Utinity.Mgs(MgsLog.Warning, "Please fill all data !");
                        return;
                    }
                    if (string.IsNullOrEmpty(dtgw.Rows[i].Cells[j].Value.ToString())){
                        Utinity.Mgs(MgsLog.Warning, "Please fill all data !");
                        return;
                    }
                }
            }
            List<SerialPortx> serialPort = new List<SerialPortx>();
            if (dtgw.Rows.Count <= mProduction.mSettingInfo.SerialPorts.Count)
            {
                Utinity.Mgs(MgsLog.Error, "Saved Failed !");
                return;
            }
            foreach (DataGridViewRow row in dtgw.Rows)
            {
                try
                {
                    SerialPortx sp = new SerialPortx();
                    sp.Device = (row.Cells[1].Value is null) ? string.Empty : row.Cells[1].Value.ToString();
                    sp.Comport = (row.Cells[2].Value is null) ? string.Empty : row.Cells[2].Value.ToString();
                    if (string.IsNullOrEmpty(sp.Comport) || string.IsNullOrEmpty(sp.Device))
                    {
                        MessageBox.Show($"Please select comport,device!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!ComExist(sp.Comport))
                    {
                        int databis = 8;
                        if (row.Cells[3].Value != null)
                            int.TryParse(row.Cells[3].Value.ToString(), out databis);
                        sp.DataBis = databis;

                        int baud = 9600;
                        if (row.Cells[4].Value != null)
                            int.TryParse(row.Cells[4].Value.ToString(), out baud);
                        sp.BaudRate = baud;

                        int stopbis = 1;
                        if (row.Cells[6].Value != null)
                            int.TryParse(row.Cells[6].Value.ToString(), out stopbis);
                        sp.StopBit = stopbis;

                        sp.Parity = (row.Cells[5].Value is null) ? "None" : row.Cells[5].Value.ToString();
                        sp.Prefix = (row.Cells[7].Value is null) ? string.Empty : row.Cells[7].Value.ToString();
                        sp.Suffix = (row.Cells[8].Value is null) ? string.Empty : row.Cells[8].Value.ToString();
                        sp.Remark = (row.Cells[9].Value is null) ? string.Empty : row.Cells[9].Value.ToString();
                        SerialPortConnection.SpPort.Add(new System.IO.Ports.SerialPort { PortName = sp.Comport, BaudRate = sp.BaudRate });
                        SerialPortConnection.Timers.Add(new System.Windows.Forms.Timer { Interval = 100 });
                        serialPort.Add(sp);
                    }
                    //else
                    //{
                    //    MessageBox.Show($"{sp.Comport} existed!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                }
                catch (Exception ex)
                {

                }
            }
            mProduction.mSettingInfo.SerialPorts = serialPort;
            mProduction.ConnectSerialPort();
            Utinity.Mgs(MgsLog.Success, "Saved");
        }
        private void dtgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dtgw.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dtgw_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex == -1) return;
            //if(dtgw.Rows[e.RowIndex].Cells[1].Value.ToString() == "Scanner")
            //{
            //    dtCom.DataSource = System.IO.Ports.SerialPort.GetPortNames();
            //    this.Invoke((MethodInvoker)delegate { 
            //        dtgw.CurrentCell=dtgw.Rows[e.RowIndex].Cells[2];
            //        dtgw.BeginEdit(true);
            //        ComboBox cbb = (ComboBox)dtgw.EditingControl;
            //        cbb.SelectedIndex = 1;
            //    });
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<SerialPortx> serialPort = new List<SerialPortx>(mProduction.mSettingInfo.SerialPorts);
                foreach (DataGridViewRow row in dtgw.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if ((bool)row.Cells[0].Value)
                        {
                            if (row.Cells[2].Value != null)
                            {
                                int idx = 0;
                                foreach (var item in SerialPortConnection.SpPort)
                                {
                                    if (row.Cells[2].Value.ToString() == item.PortName)
                                    {
                                        if (SerialPortConnection.SpPort[idx].IsOpen)
                                            SerialPortConnection.SpPort[idx].Close();
                                        serialPort.RemoveAt(idx);
                                    }
                                    idx++;
                                }
                            }
                            dtgw.Rows.RemoveAt(row.Index);
                        }
                    }
                }
                mProduction.mSettingInfo.SerialPorts = serialPort;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
