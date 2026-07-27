using DVLDBusinessLayer;
using MySolution.Licenses;
using MySolution.Licenses.International;
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

namespace MySolution.Applications.International_Driving_License_Applications
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }


        private DataTable _dtIntLicenseApplications = new DataTable();

        enum enSelectedItem
        {
            None = 0,
            InternationalLicenseID = 1,
            ApplicationID = 2,
            DriverID = 3,
            LocalLicenseID = 4,
            IsActive = 5

        };

        enum enSelectedIsActiveIndex
        {
            All = 0, Yes = 1, No = 2
        };

        private void _LoadIntLicenseApplicationsData()
        {


            _dtIntLicenseApplications =
                clsInternationalLicense.GetAll().DefaultView.
                ToTable(false, "InternationalLicenseID", "ApplicationID",
                "DriverID", "IssuedUsingLocalLicenseID",
                "IssueDate", "ExpirationDate", "IsActive"); ;

            dgvIntLicenseApplicationsList.DataSource = _dtIntLicenseApplications;
            lblRecordsNumber.Text = dgvIntLicenseApplicationsList.Rows.Count.ToString();

            if (dgvIntLicenseApplicationsList.Rows.Count > 0)
            {
                dgvIntLicenseApplicationsList.Columns[0].HeaderText = "Int.License ID";
                dgvIntLicenseApplicationsList.Columns[0].Width = 100;

                dgvIntLicenseApplicationsList.Columns[1].HeaderText = "Application ID";
                dgvIntLicenseApplicationsList.Columns[1].Width = 100;

                dgvIntLicenseApplicationsList.Columns[2].HeaderText = "Driver ID";
                dgvIntLicenseApplicationsList.Columns[2].Width = 100;

                dgvIntLicenseApplicationsList.Columns[3].HeaderText = "L.License ID";
                dgvIntLicenseApplicationsList.Columns[3].Width = 100;

                dgvIntLicenseApplicationsList.Columns[4].HeaderText = "Issue Date";
                dgvIntLicenseApplicationsList.Columns[4].Width = 200;

                dgvIntLicenseApplicationsList.Columns[5].HeaderText = "Expiration Date";
                dgvIntLicenseApplicationsList.Columns[5].Width = 200;

                dgvIntLicenseApplicationsList.Columns[6].HeaderText = "Is Active";
                dgvIntLicenseApplicationsList.Columns[6].Width = 80;

            }

        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = (int)enSelectedItem.None;
            cbIsActive.SelectedIndex = (int)enSelectedIsActiveIndex.All;
            _LoadIntLicenseApplicationsData();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(cbIsActive.Visible = cbFilter.SelectedIndex == (int)enSelectedItem.IsActive)
            {
                cbIsActive.SelectedIndex = (int)enSelectedIsActiveIndex.All;
                cbIsActive.Focus();
            }

            if(
                txtFilter.Visible = (cbFilter.SelectedIndex != (int)enSelectedItem.None 
                && !cbIsActive.Visible))
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
                _dtIntLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvIntLicenseApplicationsList.Rows.Count.ToString();
                return;
            }

            string FilterColumn = "";

            switch (cbFilter.SelectedIndex)
            {
                case (int)enSelectedItem.InternationalLicenseID:
                    FilterColumn = "InternationalLicenseID";
                    break;
                case (int)enSelectedItem.ApplicationID:
                    FilterColumn = "ApplicationID";
                    break;
                case (int)enSelectedItem.DriverID:
                    FilterColumn = "DriverID";
                    break;
                case (int)enSelectedItem.LocalLicenseID:
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                default:
                    FilterColumn = "";
                        break;

            }

                _dtIntLicenseApplications.DefaultView.RowFilter =
                    string.Format($"{FilterColumn} = {txtFilter.Text.Trim()}");
            lblRecordsNumber.Text = dgvIntLicenseApplicationsList.Rows.Count.ToString();
        }

        private void pbAddNewIntLicenseApplication_Click(object sender, EventArgs e)
        {
            frmAddInternationalLicenseApplication frm =
                new frmAddInternationalLicenseApplication();
            frm.ShowDialog();
            frmListInternationalLicenseApplications_Load(null, null);
        }

        private void ShowPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            clsDriver Driver = clsDriver.
                Find((int)dgvIntLicenseApplicationsList.CurrentRow.Cells[2].Value);

            frmShowPersonInfo frm = new frmShowPersonInfo(Driver.PersonID);
            frm.ShowDialog();
        }

        private void ShowLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInfo frm =
                new frmInternationalLicenseInfo
                ((int)dgvIntLicenseApplicationsList.CurrentRow.Cells[0].Value);
            frm.Show();
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            clsDriver Driver = clsDriver.
                Find((int)dgvIntLicenseApplicationsList.CurrentRow.Cells[2].Value);

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(Driver.PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActive.SelectedIndex)
            {
                case (int)enSelectedIsActiveIndex.All:
                    _dtIntLicenseApplications.DefaultView.RowFilter = "";
                    break;
                case (int)enSelectedIsActiveIndex.Yes:
                    _dtIntLicenseApplications.DefaultView.RowFilter = string.Format
                                                    ("IsActive = 1");
                    break;
                case (int)enSelectedIsActiveIndex.No:
                    _dtIntLicenseApplications.DefaultView.RowFilter = "IsActive = 0";
                    break;
            }
            lblRecordsNumber.Text = dgvIntLicenseApplicationsList.Rows.Count.ToString();
        }
    
        private void pbAddNewIntLicenseApplication_MouseLeave(object sender, EventArgs e)
        {
            pbAddNewIntLicenseApplication.BackColor = PictureBox.DefaultBackColor;
        }
        private void pbAddNewIntLicenseApplication_MouseEnter(object sender, EventArgs e)
        {
            pbAddNewIntLicenseApplication.BackColor = SystemColors.ControlDark;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

  
    }
}
