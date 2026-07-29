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

namespace MySolution.Applications.Renew_Local_License
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        int _OldLicenseID;

        int _RenewedLicenseID;


        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = true;
            lblApplicationFees.Text = clsApplicationType.Find(clsApplicationType.enAppType.RenewDLService).ApplicationFees.ToString();
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


            lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.PaidFees.ToString();
            //lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblOldLicenseID.Text = _OldLicenseID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.
                AddYears(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength));


            lblTotalFees.Text =
                (Convert.ToSingle(lblApplicationFees.Text) +
                Convert.ToSingle(lblLicenseFees.Text))
                .ToString();

            txtNotes.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Notes;

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet Expired, it will Expire on:" +
                    $" {clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate)} ",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenew.Enabled = false;
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active , Choose another one",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenew.Enabled = false;
                return;
            }

            btnRenew.Enabled = true;
        }


        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLicense RenewedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.
                Renew(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (RenewedLicense != null)
            {
                MessageBox.Show($"License Renewed successfully with ID = {RenewedLicense.LicenseID}", "License Issued",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                _RenewedLicenseID = RenewedLicense.LicenseID;
                lblRLApplicationID.Text = RenewedLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = _RenewedLicenseID.ToString();
                ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_OldLicenseID);

                btnRenew.Enabled = false;
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
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_RenewedLicenseID);
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
