using DVLDBusinessLayer;
using DVLD.Global;
using DVLD.Global_Classes;
using DVLD.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLDBusinessLayer.clsApplication;

namespace DVLD.Applications.International_Driving_License_Applications
{
    public partial class frmAddInternationalLicenseApplication : Form
    {
        public frmAddInternationalLicenseApplication()
        {
            InitializeComponent();
        }


        int _LocalLicenseID;

        int _InternationalLicenseID;


        private void frmAddInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblApplicationFees.Text = clsApplicationType.Find(clsApplicationType.enAppType.NewInternationalL).ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = true;

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LocalLicenseID = obj;

            llShowLicensesHistory.Enabled = (_LocalLicenseID != -1);

            //if(_OldLicenseID == -1)
            //{
            //    return;
            //}

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                return;
            }


            //lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblLocalLicenseID.Text = _LocalLicenseID.ToString();


            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass
    != (int)clsLicense.enClassName.OrdinaryDrivingLicense)
            {
                MessageBox.Show(
                    "Selected License should be Class 3, Select another one.",
                   "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssue.Enabled = false;
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active , Choose another one",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssue.Enabled = false;
                return;
            }


            _InternationalLicenseID = clsLicense.GetActiveInternationalLicenseID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if (_InternationalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " +
                    $"{_InternationalLicenseID}",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }


        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the International license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;



           /* clsInternationalLicense InternationalLicense =
                ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.
               IssueInternationalLicense(clsGlobal.CurrentUser.UserID);
            if (InternationalLicense == null)
            {
                MessageBox.Show($"License Issue Failed", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            MessageBox.Show($"International License Issued Successfully with " +
                $"ID = {_InternationalLicenseID}", "License Issued",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            */

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = 
                ctrlDriverLicenseInfoWithFilter1.
                SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationStatus = clsApplication.enStatus.Completed;
            InternationalLicense.PaidFees = clsApplicationType.
                Find(InternationalLicense.ApplicationTypeID).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            InternationalLicense.DriverID = 
                ctrlDriverLicenseInfoWithFilter1
                .SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = _LocalLicenseID; 

            if(!InternationalLicense.Save())
            {
                MessageBox.Show($"international License Issue Failed", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;

            MessageBox.Show($"International License Issued Successfully with " +
                $"ID = {_InternationalLicenseID}", "License Issued",
               MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblInternationalLicenseApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = _InternationalLicenseID.ToString();

            btnIssue.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowNewLicensesInfo.Enabled = true;


        }



        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LocalLicenseID);
        }

        private void llShowNewLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_InternationalLicenseID);
            frmLicenseInfo.ShowDialog();
        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmRenewLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }



    }
}
