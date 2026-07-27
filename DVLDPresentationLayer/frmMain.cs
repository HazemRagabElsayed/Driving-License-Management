using MySolution.Applications;
using MySolution.Applications.International_Driving_License_Applications;
using MySolution.Applications.Release_Detained_License;
using MySolution.Applications.Renew_Local_License;
using MySolution.Applications.Replace_Local_License;
using MySolution.Drivers;
using MySolution.Global;
using MySolution.Licenses.DetainedLicenses;
using MySolution.People;
using MySolution.Tests.TestTypes;
using MySolution.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution
{
    public partial class frmMain : Form
    {

        frmLogin _LoginForm;

        public frmMain(frmLogin LoginForm)
        {
            InitializeComponent();
            _LoginForm = LoginForm;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frmListPeople = new frmListPeople();
            frmListPeople.ShowDialog();
        }



        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmListDrivers frmListDrivers = new frmListDrivers();
            frmListDrivers.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmListUsers frmListUsers = new frmListUsers();
            frmListUsers.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmUserInfo frmUserInfo = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frmUserInfo.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmChangePassword frmChangePassword = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frmChangePassword.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            clsGlobal.CurrentUser = null;
            _LoginForm.Show();
            
            ;

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmAddUpdateLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmAddUpdateLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.Show();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmAddInternationalLicenseApplication frm = new frmAddInternationalLicenseApplication();
            frm.Show();
        }

        private void replacementForLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmReplaceLocalLicenseApplication frm = new frmReplaceLocalLicenseApplication();
            frm.Show();
        }

        private void releaseDetainedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmReleaseDetainedLicenseApplication frm = new
                frmReleaseDetainedLicenseApplication();
            frm.Show();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmListLocalDrivingLicenseApplications frmListLocalDrivingLicenseApplications =
    new frmListLocalDrivingLicenseApplications();
            frmListLocalDrivingLicenseApplications.Show();
        }

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmListLocalDrivingLicenseApplications frmListLocalDrivingLicenseApplications =
                new frmListLocalDrivingLicenseApplications();
            frmListLocalDrivingLicenseApplications.Show();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmListInternationalLicenseApplications frm = new frmListInternationalLicenseApplications();
            frm.Show();
        }

        private void managedDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.Show();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmDetainLicense frm = new frmDetainLicense();
            frm.Show();
        }

        private void releaseDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.Show();
        }

        private void tsmManageApplicationTypes_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmListApplicationTypes frmManageApplicationTypes = new frmListApplicationTypes();
            frmManageApplicationTypes.Show();
        }

        private void tsmManageTestTypes_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmListTestTypes frmListTestTypes = new frmListTestTypes();
            frmListTestTypes.Show();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frmRenewLocalDrivingLicense = new frmRenewLocalDrivingLicenseApplication();
            frmRenewLocalDrivingLicense.Show();
        }
    }
}
