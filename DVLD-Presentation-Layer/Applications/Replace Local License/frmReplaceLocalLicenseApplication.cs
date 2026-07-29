using DVLDBusinessLayer;
using MySolution.Global;
using MySolution.Global_Classes;
using MySolution.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Applications.Replace_Local_License
{
    public partial class frmReplaceLocalLicenseApplication : Form
    {
        public frmReplaceLocalLicenseApplication()
        {
            InitializeComponent();
        }


        int _OldLicenseID;

        int _ReplacedLicenseID;

        //clsLicense.enIssueReason _IssueReason;

        private clsLicense.enIssueReason _GetIssueReason()
        {
            return rbDamagedLicense.Checked ?
                clsLicense.enIssueReason.ReplacementForDamaged
                :
                clsLicense.enIssueReason.ReplacementForLost;
        }

        private clsApplicationType.enAppType _GetApplicationType()
        {
            return rbDamagedLicense.Checked ?
                clsApplicationType.enAppType.ReplacementDamagedDL
                :
                clsApplicationType.enAppType.ReplacementLostDL;
                
        }


        private void frmReplaceLocalLicenseApplication_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = true;
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _OldLicenseID = obj;

            llShowLicensesHistory.Enabled = (_OldLicenseID != -1);

            //if(_OldLicenseID == -1)
            //{
            //    return;
            //}

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                return;
            }

            //lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblOldLicenseID.Text = _OldLicenseID.ToString();

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active , Choose another one",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Repalcement for the license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLicense ReplacedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.
                Replace(clsGlobal.CurrentUser.UserID,_GetIssueReason());

            if (ReplacedLicense != null)
            {
                _ReplacedLicenseID = ReplacedLicense.LicenseID;
                MessageBox.Show($"License Replaced successfully with ID = {_ReplacedLicenseID}", "License Issued",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblLRApplicationID.Text = ReplacedLicense.ApplicationID.ToString();
                lblReplacedLicenseID.Text = _ReplacedLicenseID.ToString();

                ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_OldLicenseID);

                btnIssueReplacement.Enabled = false;
                gbReplacementFor.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                llShowNewLicensesInfo.Enabled = true;

                return;
            }

            MessageBox.Show($"License Issue Failed", "License Issued",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frmLicenseHistory.ShowDialog();
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_OldLicenseID);
        }

        private void llShowNewLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_ReplacedLicenseID);
            frmLicenseInfo.ShowDialog();
        }

        private void rbDamagedLicense_Click(object sender, EventArgs e)
        {
            this.Text = "Replacement for Damaged License";
            lblTitle.Text = this.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationType()).ApplicationFees.ToString();

        }

        private void rbLostLicense_Click(object sender, EventArgs e)
        {
            this.Text = "Replacement for Lost License";
            lblTitle.Text = this.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationType()).ApplicationFees.ToString();

        }

        private void frmReplaceLocalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
