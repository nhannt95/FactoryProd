using DONGJIN_MES.Class;
using MES_IO.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class PrinterInfo : UserControl
    {
        ProductionResult mProduction;
        public PrinterInfo(ref ProductionResult prod)
        {
            InitializeComponent();
            AddCheckBoxHeader();
            mProduction = prod;
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

        private void PrinterInfo_Load(object sender, EventArgs e)
        {
            dtgw.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            for (int i = 1; i < 5; i++)
            {
                dtgw.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            try
            {
                if (mProduction.mSettingInfo.Printers is null) return;
                int idx = 0;
                foreach (var item in mProduction.mSettingInfo.Printers)
                {
                    idx++;
                    dtgw.Rows.Add(new DataGridViewRow());
                    dtgw.Rows[idx - 1].Cells[1].Value = (item.LabelBox is null) ? "" : item.LabelBox;
                    dtgw.Rows[idx - 1].Cells[2].Value = (item.LabelDetail is null) ? "" : item.LabelDetail;
                    dtgw.Rows[idx - 1].Cells[3].Value = (item.PrinterName is null) ? "" : item.PrinterName;
                    dtgw.Rows[idx - 1].Cells[4].Value = (item.Remark is null) ? "" : item.Remark;
                }
                FindPrinters();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dtgw.Rows.Add(new DataGridViewRow());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgw.Rows.Count <= mProduction.mSettingInfo.Printers.Count)
                {
                    Utinity.Mgs(MgsLog.Error, "Saved Failed !");
                    return;
                }
                List<Printerx> printInfo = new List<Printerx>();
                foreach (DataGridViewRow row in dtgw.Rows)
                {
                    Printerx print = new Printerx();
                    print.Remark = (row.Cells[4].Value is null) ? string.Empty : row.Cells[4].Value.ToString();
                    print.LabelBox = (row.Cells[1].Value is null) ? string.Empty : row.Cells[1].Value.ToString();
                    print.LabelDetail = (row.Cells[2].Value is null) ? string.Empty : row.Cells[2].Value.ToString();
                    print.PrinterName = (row.Cells[3].Value is null) ? string.Empty : row.Cells[3].Value.ToString();
                    printInfo.Add(print);
                }
                mProduction.mSettingInfo.Printers = printInfo;
                if (printInfo.Count > 0)
                    mProduction.SavePrinter(printInfo[0].PrinterName);
                Utinity.Mgs(MgsLog.Success, "Saved");
            }
            catch (Exception ex)
            {

            }
        }
        private bool SelectRow()
        {
            foreach (DataGridViewRow row in dtgw.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    return true;
                }
            }
            return false;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgw.Rows.Count == 0)
                {
                    Utinity.Mgs(MgsLog.Warning, "Data empty!");
                    return;
                }
                if (!SelectRow())
                {
                    Utinity.Mgs(MgsLog.Warning, "Please select 1 row!");
                    return;
                }
                List<int> rowIdx = new List<int>();
                List<int> unitIdx = new List<int>();
                List<Printerx> print = new List<Printerx>(mProduction.mSettingInfo.Printers);
                foreach (DataGridViewRow row in dtgw.Rows)
                {
                    if (row.Cells[0].Value is null) return;
                    if ((bool)row.Cells[0].Value)
                    {
                        if (row.Cells[3].Value != null)
                        {
                            int idx = 0;
                            foreach (var item in mProduction.mSettingInfo.Printers)
                            {
                                idx++;
                                if (row.Cells[3].Value.ToString() == item.PrinterName)
                                {
                                    unitIdx.Add(idx - 1);
                                }
                            }
                        }
                        rowIdx.Add(row.Index);
                    }
                }
                dtgw.BeginEdit(true);
                for (int i = 0; i < rowIdx.Count; i++)
                {
                    dtgw.Rows.RemoveAt(rowIdx[i] - i);
                }
                for (int i = 0; i < unitIdx.Count; i++)
                {
                    print.RemoveAt(unitIdx[i] - i);
                }
                mProduction.mSettingInfo.Printers = print;
            }
            catch (Exception ex)
            {

            }
        }
        private void LoadPrinter()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
        private List<string> lstPrinter = new List<string>();
        private void FindPrinters()
        {
            lstPrinter = DarkSoul.PrintingFunctions.GetOnlinePrinters();
            cbbPrinter.DataSource = lstPrinter;
        }

        private void btnPrintConfig_Click(object sender, EventArgs e)
        {
            PrintConfig frm = new PrintConfig(mProduction.mPrintSetting);
            frm.Show();
        }
    }
}
