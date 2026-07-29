using DVLDBusinessLayer;
using DVLD.Licenses;
using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        public frmListDrivers()
        {
            InitializeComponent();
        }

        static private DataTable _dtAllDrivers = clsDriver.GetAll();

        enum enSelectedItem
        {
            None = 0, DriverID = 1, PersonID = 2,
            NationalNo = 3, FullName = 4
        };

        private void _LoadDriversData()
        {

            dgvDriversList.DataSource = _dtAllDrivers;
            lblRecordsNumber.Text = dgvDriversList.Rows.Count.ToString();

            if (dgvDriversList.Rows.Count > 0)
            {

                dgvDriversList.Columns[1].HeaderText = "Driver ID";
                dgvDriversList.Columns[0].Width = 100;

                dgvDriversList.Columns[1].HeaderText = "Person ID";
                dgvDriversList.Columns[1].Width = 100;

                dgvDriversList.Columns[2].HeaderText = "National No.";
                dgvDriversList.Columns[2].Width = 100;

                dgvDriversList.Columns[3].HeaderText = "Full Name";
                dgvDriversList.Columns[3].Width = 300;

                dgvDriversList.Columns[4].HeaderText = "Date";
                dgvDriversList.Columns[4].Width = 200;

                dgvDriversList.Columns[5].HeaderText = "Active Licenses";
                dgvDriversList.Columns[5].Width = 100;

            }

        }
        void _RefreshDriversList()
        {
            _dtAllDrivers = clsDriver.GetAll();
            dgvDriversList.DataSource = _dtAllDrivers;
            lblRecordsNumber.Text = dgvDriversList.Rows.Count.ToString();
        }
        private void frmManageDrivers_Load(object sender, EventArgs e)
        {

            _LoadDriversData();
            cbFilter.SelectedIndex = (int)enSelectedItem.None;
        }
        private void dgvDriversList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            _RefreshDriversList();
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtFilter.Visible = (cbFilter.SelectedIndex != Convert.ToInt16(enSelectedItem.None)))
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }

        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            if (cbFilter.SelectedIndex == (short)enSelectedItem.None
                || txtFilter.Text.Trim() == "")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvDriversList.Rows.Count.ToString();
                return;
            }

            string FilterColumn = "";

            switch (cbFilter.SelectedIndex)
            {
                case (int)enSelectedItem.DriverID:
                    FilterColumn = "DriverID";
                    break;
                case (int)enSelectedItem.PersonID:
                    FilterColumn = "PersonID";
                    break;
                case (int)enSelectedItem.NationalNo:
                    FilterColumn = "NationalNo";
                    break;
                case (int)enSelectedItem.FullName:
                    FilterColumn = "FullName";
                    break;

            }


            if (cbFilter.SelectedIndex == (int)enSelectedItem.PersonID ||
                cbFilter.SelectedIndex == (int)enSelectedItem.DriverID)
            {
                _dtAllDrivers.DefaultView.RowFilter =
                    string.Format($"{FilterColumn} = {txtFilter.Text.Trim()}");
            }
            else
            {

                _dtAllDrivers.DefaultView.RowFilter =
                    string.Format($"{FilterColumn} Like '{txtFilter.Text.Trim()}%'");
            }
            lblRecordsNumber.Text = dgvDriversList.Rows.Count.ToString();

        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == (int)enSelectedItem.PersonID)
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }
        private void ShowPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frmPersonInfo = new frmShowPersonInfo(Convert.ToInt16(dgvDriversList.CurrentRow.Cells[1].Value));
            frmPersonInfo.ShowDialog();
            _RefreshDriversList();
        }
        private void IssueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory
                ((int)dgvDriversList.CurrentRow.Cells[1].Value);
            frmLicenseHistory.ShowDialog();
            _RefreshDriversList();

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
