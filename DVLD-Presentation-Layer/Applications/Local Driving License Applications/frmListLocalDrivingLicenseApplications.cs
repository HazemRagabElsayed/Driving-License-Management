using DVLDBusinessLayer;
using DVLD.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static DVLDBusinessLayer.clsTestType;

namespace DVLD.Applications
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private DataTable _dtLDLApplications;

        enum enSelectedItem
        {
            None = 0,
            LDLAppID = 1,
            NationalNo = 2,
            FullName = 3,
            Status = 4
        };

        private void _LoadLDLApplicationsData()
        {
            _dtLDLApplications = clsLocalDrivingLicenseApplication.GetAll();
            dgvLDLApplicationsList.DataSource = _dtLDLApplications;
            lblRecordsNumber.Text = dgvLDLApplicationsList.Rows.Count.ToString();

            if (dgvLDLApplicationsList.Rows.Count > 0)
            {
                dgvLDLApplicationsList.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLDLApplicationsList.Columns[0].Width = 100;

                dgvLDLApplicationsList.Columns[1].HeaderText = "Driving Class";
                dgvLDLApplicationsList.Columns[1].Width = 200;

                dgvLDLApplicationsList.Columns[2].HeaderText = "National No.";
                dgvLDLApplicationsList.Columns[2].Width = 100;

                dgvLDLApplicationsList.Columns[3].HeaderText = "Full Name";
                dgvLDLApplicationsList.Columns[3].Width = 260;

                dgvLDLApplicationsList.Columns[4].HeaderText = "Application Date";
                dgvLDLApplicationsList.Columns[4].Width = 160;

                dgvLDLApplicationsList.Columns[5].HeaderText = "Passed Tests";
                dgvLDLApplicationsList.Columns[5].Width = 100;

                dgvLDLApplicationsList.Columns[6].HeaderText = "Status";
                dgvLDLApplicationsList.Columns[6].Width = 80;

            }

        }



        private void frmManageLDLApplications_Load(object sender, EventArgs e)
        {
            _LoadLDLApplicationsData();
            cbFilter.SelectedIndex = (int)enSelectedItem.None;
        }



        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtFilter.Visible = (cbFilter.SelectedIndex != Convert.ToInt32(enSelectedItem.None)))
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
                _dtLDLApplications.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvLDLApplicationsList.Rows.Count.ToString();
                return;
            }

            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;

            }


            if (cbFilter.SelectedIndex == (int)enSelectedItem.LDLAppID)
            {
                _dtLDLApplications.DefaultView.RowFilter =
                    string.Format($"{FilterColumn} = {txtFilter.Text.Trim()}");
            }
            else
            {

                _dtLDLApplications.DefaultView.RowFilter =
                    string.Format($"{FilterColumn} Like '{txtFilter.Text.Trim()}%'");
            }
            lblRecordsNumber.Text = dgvLDLApplicationsList.Rows.Count.ToString();

        }

        private void dgvLDLApplicationsList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmManageLDLApplications_Load(null, null);
        }

        private void pbAddNewLDLApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication
                = new frmAddUpdateLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            frmManageLDLApplications_Load(null, null);
        }




        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmShowLocalDrivingApplicationInfo frmShowLocalDrivingApplicationInfo =
                new frmShowLocalDrivingApplicationInfo((int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value);
            frmShowLocalDrivingApplicationInfo.ShowDialog();
            frmManageLDLApplications_Load(null, null);
        }


        private void EditApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmAddUpdateLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication
                = new frmAddUpdateLocalDrivingLicenseApplication((int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value);
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            frmManageLDLApplications_Load(null, null);
        }

        private void DeleteApplicationtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            int LDLAppID = Convert.ToInt32(dgvLDLApplicationsList.CurrentRow.Cells[0].Value);

            if (MessageBox.Show($"Are you sure you want to delete L.D Application with ID =" +
                $"  [{LDLAppID}]"
                , "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.Find(LDLAppID);

            if(LDLApp == null)
            {
                MessageBox.Show($"L.D Application with ID {LDLAppID} Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LDLApp.Delete())
            {
                frmManageLDLApplications_Load(null, null);
                MessageBox.Show("L.D Application Deleted Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                MessageBox.Show("L.D Application was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirm"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

                clsLocalDrivingLicenseApplication CurrentApplicationInfo =
                    clsLocalDrivingLicenseApplication
                    .Find((int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value);


                if (CurrentApplicationInfo.ApplicationStatus == clsApplication.enStatus.New && CurrentApplicationInfo.Cancel())
                {
                    MessageBox.Show("Application cancelled successfully.", "Cancelled"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmManageLDLApplications_Load(null, null);

                }
                else
                {
                    MessageBox.Show("Error : Cannot Cancell This Application", "Error"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
 
        void _ScheduleTest(clsTestType.enTestType TestType)
        {
            frmListTestAppointments frmListTestAppointments =
                new frmListTestAppointments((int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value, TestType);
            frmListTestAppointments.ShowDialog();
            frmManageLDLApplications_Load(null, null);
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.Vision);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.Written);

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.Street);
        }

        private void IssueDrivingLicensetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            int LDLAppID = (int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value;

            if(!clsLocalDrivingLicenseApplication.PassedAllTests(LDLAppID))
            {
                MessageBox.Show("Person didn't pass all tests!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsLocalDrivingLicenseApplication.DoesLDAppApplicantHaveLicense(LDLAppID))
            {
                MessageBox.Show("Person already has an active license of this class type",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            frmIssueDriverLicenseForFirstTime frmIssueDriverLicenseForFirstTime =
    new frmIssueDriverLicenseForFirstTime(LDLAppID);
            frmIssueDriverLicenseForFirstTime.ShowDialog();
            frmManageLDLApplications_Load(null, null);

        }

        private void ShowLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication
                .Find((int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value);

            if(LDLApp != null)
            {
                int LicenseID = LDLApp.GetLicenseID();
                if(LicenseID == -1)
                {
                    MessageBox.Show("No License Found!","No License",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(LicenseID);
                frmLicenseInfo.ShowDialog();
            }
            else
            {
                //MessageBox
            }

            

            
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.
                Find((int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value);

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(LDLApp.ApplicantPersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == (int)enSelectedItem.LDLAppID)
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }
        private void pbAddNewLDLApplication_MouseEnter(object sender, EventArgs e)
        {
            pbAddNewLDLApplication.BackColor = SystemColors.ControlDark;
        }
        private void pbAddNewLDLApplication_MouseLeave(object sender, EventArgs e)
        {
            pbAddNewLDLApplication.BackColor = PictureBox.DefaultBackColor;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmsLDLAppList_Opening(object sender, CancelEventArgs e)
        {
            int LDLAppID = (int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value;
            int PassedTests = (int)dgvLDLApplicationsList.CurrentRow.Cells[5].Value;
            string Status = (string)dgvLDLApplicationsList.CurrentRow.Cells[6].Value;
            bool LicenseExists = clsLocalDrivingLicenseApplication.DoesLDAppApplicantHaveLicense(LDLAppID);

            ShowLicenseToolStripMenuItem.Enabled = LicenseExists;
            IssueDrivingLicensetoolStripMenuItem.Enabled = 
                !LicenseExists && PassedTests == 3;

            EditApplicationToolStripMenuItem.Enabled = (Status == clsApplication.enStatus.New.ToString());
            DeleteApplicationtoolStripMenuItem.Enabled = (Status == clsApplication.enStatus.New.ToString());
            CancelApplicationToolStripMenuItem.Enabled = (Status == clsApplication.enStatus.New.ToString());

            ScheduleTeststoolStripMenuItem.Enabled = PassedTests != 3 && Status == clsApplication.enStatus.New.ToString();

            if (ScheduleTeststoolStripMenuItem.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled  = (PassedTests == 0);
                scheduleWrittenTestToolStripMenuItem.Enabled = (PassedTests == 1);
                scheduleStreetTestToolStripMenuItem.Enabled  = (PassedTests == 2);
            }



        }

        //enum enPassedTests { Zero = 0, One = 1, Two = 2, Three = 3 };
        //void _RefreshLDLApplicationsList()
        //{
        //    _dtLDLApplications = clsLocalDrivingLicenseApplication.GetAll();
        //    dgvLDLApplicationsList.DataSource = _dtLDLApplications;
        //    lblRecordsNumber.Text = dgvLDLApplicationsList.Rows.Count.ToString();
        //}


        //private void dgvLDLApplicationsList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        //{

        //    ShowLicenseToolStripMenuItem.Enabled = false;
        //    IssueDrivingLicensetoolStripMenuItem.Enabled = false;



        //    if (dgvLDLApplicationsList.CurrentRow.Cells[6].Value.ToString() != "Cancelled")
        //    {
        //        switch ((enPassedTests)dgvLDLApplicationsList.CurrentRow.Cells[5].Value)
        //        {
        //            case enPassedTests.Zero:

        //                scheduleVisionTestToolStripMenuItem.Enabled = true;
        //                scheduleWrittenTestToolStripMenuItem.Enabled = false;
        //                scheduleStreetTestToolStripMenuItem.Enabled = false;
        //                ShowLicenseToolStripMenuItem.Enabled = false;

        //                break;
        //            case enPassedTests.One:

        //                scheduleVisionTestToolStripMenuItem.Enabled = false;
        //                scheduleWrittenTestToolStripMenuItem.Enabled = true;
        //                scheduleStreetTestToolStripMenuItem.Enabled = false;

        //                break;
        //            case enPassedTests.Two:

        //                scheduleVisionTestToolStripMenuItem.Enabled = false;
        //                scheduleWrittenTestToolStripMenuItem.Enabled = false;
        //                scheduleStreetTestToolStripMenuItem.Enabled = true;

        //                break;
        //            case enPassedTests.Three:

        //                ScheduleTeststoolStripMenuItem.Enabled = false;

        //                if (clsLocalDrivingLicenseApplication.
        //                    DoesLDAppApplicantHaveLicense
        //                    ((int)dgvLDLApplicationsList.CurrentRow.Cells[0].Value))
        //                {
        //                    ShowLicenseToolStripMenuItem.Enabled = true;
        //                    EditApplicationToolStripMenuItem.Enabled = false;
        //                    DeleteApplicationtoolStripMenuItem.Enabled = false;
        //                    CancelApplicationToolStripMenuItem.Enabled = false;

        //                    return;
        //                }
        //                IssueDrivingLicensetoolStripMenuItem.Enabled = true;

        //                break;
        //        }
        //    }
        //    else
        //    {
        //        ScheduleTeststoolStripMenuItem.Enabled = false;
        //        EditApplicationToolStripMenuItem.Enabled = false;
        //        DeleteApplicationtoolStripMenuItem.Enabled = false;
        //        CancelApplicationToolStripMenuItem.Enabled = false;

        //        return;

        //    }

        //    ScheduleTeststoolStripMenuItem.Enabled = true;
        //    EditApplicationToolStripMenuItem.Enabled = true;
        //    DeleteApplicationtoolStripMenuItem.Enabled = true;
        //    CancelApplicationToolStripMenuItem.Enabled = true;


        //}
    }
}
