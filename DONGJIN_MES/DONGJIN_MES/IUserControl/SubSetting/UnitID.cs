using DONGJIN_MES.Class;
using MES_IO.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DONGJIN_MES.IUserControl.SubSetting
{
    public partial class UnitID : UserControl
    {
        ProductionResult mProduction;
        public UnitID(ref ProductionResult prod)
        {
            InitializeComponent();
            AddCheckBoxHeader();
            mProduction=prod;
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
                    TextAlign = ContentAlignment.MiddleRight,
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
            dtgw.BeginEdit(true);
            if (dtgw.RowCount > 0)
            {
                for (int j = 0; j < dtgw.RowCount; j++)
                {
                    ((DataGridViewCheckBoxCell)dtgw[0, j]).Value = mChk.Checked;
                }
            }
            dtgw.EndEdit();
            dtgw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void UnitID_Load(object sender, EventArgs e)
        {
            dtgw.Columns[0].DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleLeft;
            for (int i = 1; i < 3; i++)
            {
                dtgw.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            try
            {
                if (mProduction.mSettingInfo.UnitIDs is null) return;
                int idx = 0;
                foreach (var item in mProduction.mSettingInfo.UnitIDs)
                {
                    idx++;
                    dtgw.Rows.Add(new DataGridViewRow());
                    dtgw.Rows[idx - 1].Cells[0].Value = false;
                    dtgw.Rows[idx - 1].Cells[1].Value = (item.UnitIDs is null) ? "" : item.UnitIDs;
                    dtgw.Rows[idx - 1].Cells[2].Value = (item.UnitName is null) ? "" : item.UnitName;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dtgw.Rows.Add(new DataGridViewRow());
        }
        private bool SelectRow()
        {
            foreach(DataGridViewRow row in dtgw.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    return true;
                }
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgw.Rows.Count <= mProduction.mSettingInfo.UnitIDs.Count)
                {
                    Utinity.Mgs(MgsLog.Error, "Saved Failed !");
                    return;
                }
                List<UnitIDx> unitID = new List<UnitIDx>();
                foreach (DataGridViewRow row in dtgw.Rows)
                {
                    UnitIDx unit = new UnitIDx();
                    unit.UnitIDs = (row.Cells[1].Value is null) ? string.Empty : row.Cells[1].Value.ToString();
                    unit.UnitName = (row.Cells[2].Value is null) ? string.Empty : row.Cells[2].Value.ToString();
                    unitID.Add(unit);
                }
                mProduction.mSettingInfo.UnitIDs = unitID;
                Utinity.Mgs(MgsLog.Success, "Saved");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
