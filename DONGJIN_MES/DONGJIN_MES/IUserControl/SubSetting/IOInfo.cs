using DONGJIN_MES.Class;
using MES_IO.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class IOInfo : UserControl
    {
        ProductionResult mProduction;
        public IOInfo(ref ProductionResult prod)
        {
            InitializeComponent();
            AddCheckBoxHeader();
            mProduction= prod;
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

        private void IOInfo_Load(object sender, EventArgs e)
        {
            dtgw.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgw.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgw.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgw.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgw.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgw.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgw.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            try
            {
                if (mProduction.mSettingInfo.IOInfos is null) return;
                int idx = 0;
                foreach(var item in mProduction.mSettingInfo.IOInfos)
                {
                    idx++;
                    dtgw.Rows.Add(new DataGridViewRow());
                    dtgw.Rows[idx - 1].Cells[1].Value = (item.Chanel is null)?"":item.Chanel;
                    dtgw.Rows[idx - 1].Cells[2].Value = (item.Event is null)?"":item.Event;
                    dtgw.Rows[idx - 1].Cells[3].Value = (item.ONOFF is null)?"":item.ONOFF;
                    dtgw.Rows[idx - 1].Cells[4].Value = item.Delay;
                    dtgw.Rows[idx - 1].Cells[5].Value = (item.Maker is null)?"":item.Maker;
                    dtgw.Rows[idx - 1].Cells[6].Value = (item.Remark is null)?"":item.Remark;
                }
            }catch (Exception ex)
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
                if (dtgw.Rows.Count <= mProduction.mSettingInfo.IOInfos.Count)
                {
                    Utinity.Mgs(MgsLog.Error, "Saved Failed !");
                    return;
                }
                List<IOInfox> ioInfo = new List<IOInfox>();
                foreach (DataGridViewRow row in dtgw.Rows)
                {
                    IOInfox io = new IOInfox();
                    io.Chanel = (row.Cells[1].Value is null) ? string.Empty : row.Cells[1].Value.ToString();
                    io.Event = (row.Cells[2].Value is null) ? string.Empty : row.Cells[2].Value.ToString();
                    io.ONOFF = (row.Cells[3].Value is null) ? string.Empty : row.Cells[3].Value.ToString();
                    io.Delay = (row.Cells[4].Value is null) ? 0 : int.Parse(row.Cells[4].Value.ToString());
                    io.Maker = (row.Cells[5].Value is null) ? string.Empty : row.Cells[5].Value.ToString();
                    io.Remark = (row.Cells[6].Value is null) ? string.Empty : row.Cells[6].Value.ToString();
                    ioInfo.Add(io);
                }
                mProduction.mSettingInfo.IOInfos = ioInfo;
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
                List<IOInfox> io = new List<IOInfox>(mProduction.mSettingInfo.IOInfos);
                foreach (DataGridViewRow row in dtgw.Rows)
                {
                    if (row.Cells[0].Value is null) return;
                    if ((bool)row.Cells[0].Value)
                    {
                        if (row.Cells[1].Value != null)
                        {
                            int idx = 0;
                            foreach (var item in mProduction.mSettingInfo.IOInfos)
                            {
                                idx++;
                                if (row.Cells[1].Value.ToString() == item.Chanel)
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
                    io.RemoveAt(unitIdx[i] - i);
                }
                mProduction.mSettingInfo.IOInfos=io;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
