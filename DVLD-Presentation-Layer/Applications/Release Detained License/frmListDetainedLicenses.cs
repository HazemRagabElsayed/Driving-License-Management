using DVLDBusinessLayer;
using MySolution.Applications;
using MySolution.Applications.Release_Detained_License;
using MySolution.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Licenses.DetainedLicenses
{
    public partial class frmListDetainedLicenses : Form
    {
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }


        private DataTable _dtDetainedLicenses;

        enum enSelectedItem
        {
            None = 0,
            DetainID = 1,
            IsReleased = 2,
            NationalNo = 3,
            FullName = 4,
            ReleaseApplicationID = 5
        };

        enum enSelectedIsReleased
        {
            All = 0, Yes = 1, No = 2
        };

        private void _LoadDetainedLicensesData()
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAll();
            dgvDetainedLicensesList.DataSource = _dtDetainedLicenses;
            lblRecordsNumber.Text = dgvDetainedLicensesList.Rows.Count.ToString();

            if (dgvDetainedLicensesList.Rows.Count > 0)
            {
                dgvDetainedLicensesList.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicensesList.Columns[0].Width = 80;

                dgvDetainedLicensesList.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicensesList.Columns[1].Width = 80;

                dgvDetainedLicensesList.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicensesList.Columns[2].Width = 150;

                dgvDetainedLicensesList.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicensesList.Columns[3].Width = 80;

                dgvDetainedLicensesList.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicensesList.Columns[4].Width = 100;

                dgvDetainedLicensesList.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicensesList.Columns[5].Width = 150;

                dgvDetainedLicensesList.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicensesList.Columns[6].Width = 80;

                dgvDetainedLicensesList.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicensesList.Columns[7].Width = 180;

                dgvDetainedLicensesList.Columns[8].HeaderText = "Release App.ID";
                dgvDetainedLicensesList.Columns[8].Width = 80;

            }

        }
        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _LoadDetainedLicensesData();
            cbFilter.SelectedIndex = (int)enSelectedItem.None;
            cbIsReleased.SelectedIndex = (int)enSelectedIsReleased.All;
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            _dtDetainedLicenses.DefaultView.RowFilter = "";
            lblRecordsNumber.Text = dgvDetainedLicensesList.Rows.Count.ToString();

            if (txtFilter.Visible = (cbFilter.SelectedIndex != (short)enSelectedItem.None
                &&  !(cbIsReleased.Visible = cbFilter.SelectedIndex == (short)enSelectedItem.IsReleased)))
            {
                txtFilter.Text = "";
                txtFilter.Focus();
                return;
            }

        }
        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsReleased.SelectedIndex)
            {
                case (int)enSelectedIsReleased.All:
                    _dtDetainedLicenses.DefaultView.RowFilter = "";
                    break;
                case (int)enSelectedIsReleased.Yes:
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format
                                                    ("IsReleased = 1");
                    break;
                case (int)enSelectedIsReleased.No:
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format
                                ("IsReleased = 0");
                    break;
            }
            lblRecordsNumber.Text = dgvDetainedLicensesList.Rows.Count.ToString();
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            if (cbFilter.SelectedIndex == (short)enSelectedItem.None
                || txtFilter.Text.Trim() == "" 
                || cbFilter.SelectedIndex == (short)enSelectedItem.IsReleased)
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvDetainedLicensesList.Rows.Count.ToString();

                return;
            }

            string FilterColumn = "";

            switch (cbFilter.SelectedIndex)
            {
                case (int)enSelectedItem.DetainID:
                    FilterColumn = "DetainID";
                    break;
                case (int)enSelectedItem.NationalNo:
                    FilterColumn = "NationalNo";
                    break;
                case (int)enSelectedItem.FullName:
                    FilterColumn = "FullName";
                    break;
                case (int)enSelectedItem.ReleaseApplicationID:
                    FilterColumn = "ReleaseApplicationID";
                    break;
                default:
                    FilterColumn = "";
                    break;

            }


            if (cbFilter.SelectedIndex == (int)enSelectedItem.DetainID ||
                cbFilter.SelectedIndex == (int)enSelectedItem.ReleaseApplicationID)
            {
                _dtDetainedLicenses.DefaultView.RowFilter =
                    string.Format($"{FilterColumn} = {txtFilter.Text.Trim()}");
            }
            else
            {

                _dtDetainedLicenses.DefaultView.RowFilter =
                    string.Format($"{FilterColumn} Like '{txtFilter.Text.Trim()}%'");
            }
            lblRecordsNumber.Text = dgvDetainedLicensesList.Rows.Count.ToString();


        }
        private void pbDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            _LoadDetainedLicensesData();
        }
        private void pbRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = 
                new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            _LoadDetainedLicensesData();
        }
        private void ShowPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense License = clsLicense.Find(
                (int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value);

            frmShowPersonInfo frm = new frmShowPersonInfo(License.DriverInfo.PersonID);
            frm.ShowDialog();

            _LoadDetainedLicensesData();
        }
        private void ShowLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
       
                frmLicenseInfo frm = new frmLicenseInfo
                ((int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value);
                frm.ShowDialog();

        }
        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            clsLicense License = clsLicense.Find(
                (int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value
                );

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(License.DriverInfo.PersonID);
            frmLicenseHistory.ShowDialog();
            _LoadDetainedLicensesData();
        }
        private void ReleaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new
                frmReleaseDetainedLicenseApplication
                ((int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _LoadDetainedLicensesData();
        }
        private void cmsDetainLicensesList_Opening(object sender, CancelEventArgs e)
        {
            ReleaseDetainedLicenseToolStripMenuItem.Enabled =
                !(bool)dgvDetainedLicensesList.CurrentRow.Cells[3].Value;
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == (int)enSelectedItem.DetainID
                || cbFilter.SelectedIndex == (int) enSelectedItem.ReleaseApplicationID)
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }

            if (e.KeyChar == (char)13)
                txtFilter_TextChanged(null, null);
                
        }
        private void pb_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = SystemColors.ControlDark;
        }
        private void pb_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = PictureBox.DefaultBackColor;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

  
    }
}
