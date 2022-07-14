using DONGJIN_MES.Class;
using MES_IO.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class SoundInfo : UserControl
    {
        ProductionResult mProduction;
        public SoundInfo(ref ProductionResult prod)
        {
            InitializeComponent();
            AddCheckBoxHeader();
            mProduction=prod;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dtgw.Rows.Add(new DataGridViewRow());
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

        private void SoundInfo_Load(object sender, EventArgs e)
        {
            dtgw.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            for (int i = 1; i < 4; i++)
            {
                dtgw.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            try
            {
                if (mProduction.mSettingInfo.SoundInfos is null) return;
                int idx = 0;
                foreach (var item in mProduction.mSettingInfo.SoundInfos)
                {
                    idx++;
                    dtgw.Rows.Add(new DataGridViewRow());
                    dtgw.Rows[idx - 1].Cells[1].Value = (item.Item is null) ? "" : item.Item;
                    dtgw.Rows[idx - 1].Cells[2].Value = (item.Path is null) ? "" : item.Path;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgw.Rows.Count <= mProduction.mSettingInfo.SoundInfos.Count)
                {
                    Utinity.Mgs(MgsLog.Error, "Saved Failed !");
                    return;
                }
                List<SoundInfox> soundInfo = new List<SoundInfox>();
                foreach (DataGridViewRow row in dtgw.Rows)
                {
                    SoundInfox sound = new SoundInfox();
                    sound.Path = (row.Cells[2].Value is null) ? string.Empty : row.Cells[2].Value.ToString();
                    sound.Item = (row.Cells[1].Value is null) ? string.Empty : row.Cells[1].Value.ToString();
                    soundInfo.Add(sound);
                }
                mProduction.mSettingInfo.SoundInfos = soundInfo;
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
                List<SoundInfox> sound = new List<SoundInfox>(mProduction.mSettingInfo.SoundInfos);
                foreach (DataGridViewRow row in dtgw.Rows)
                {
                    if (row.Cells[0].Value is null) return;
                    if ((bool)row.Cells[0].Value)
                    {
                        if (row.Cells[1].Value != null)
                        {
                            int idx = 0;
                            foreach (var item in mProduction.mSettingInfo.SoundInfos)
                            {
                                idx++;
                                if (row.Cells[1].Value.ToString() == item.Item)
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
                    sound.RemoveAt(unitIdx[i] - i);
                }
                mProduction.mSettingInfo.SoundInfos=sound;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
