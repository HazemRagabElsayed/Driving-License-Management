using DVLDBusinessLayer;
using MySolution.Global;
using MySolution.Global_Classes;
using MySolution.Licenses.Local.Controls;
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
    public partial class frmDetainLicense : Form
    {



        int _LicenseID;

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        public frmDetainLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(LicenseID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = true;
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            llShowLicensesHistory.Enabled = (_LicenseID != -1);

            //if(_OldLicenseID == -1)
            //{
            //    return;
            //}

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                return;
            }


            //lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblLicenseID.Text = _LicenseID.ToString();

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is already detained, Choose another one ",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnDetain.Enabled = false;
                return;
            }

            //if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            //{
            //    MessageBox.Show("Selected License is not yet Expired, it will Expire on:" +
            //        $" {clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate)} ",
            //        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    btnDetain.Enabled = false;
            //    return;
            //}

            //if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            //{
            //    MessageBox.Show("Selected License is not Active , Choose another one",
            //        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    btnDetain.Enabled = false;
            //    return;
            //}
            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {


            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validating Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to Detain this license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int DetainID = 
                ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain
                (clsGlobal.CurrentUser.UserID, Convert.ToSingle(txtFineFees.Text));


            if (DetainID != -1)
            {
                MessageBox.Show($"License Detained Successfully with ID = {DetainID}", "License Detained",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblDetainID.Text = DetainID.ToString();
                ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);

                btnDetain.Enabled = false;
                txtFineFees.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                llShowNewLicensesInfo.Enabled = true;

                return;
            }

            MessageBox.Show($"License Detain Failed", "Detain Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frmLicenseHistory.ShowDialog();
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);
        }
        private void llShowNewLicensesInfo_LinkClicked(object sender, EventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_LicenseID);
            frmLicenseInfo.ShowDialog();
        }
        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }
        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }

            if (!clsValidation.IsNumber(txtFineFees.Text) || Convert.ToSingle(txtFineFees.Text) <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "InValid Number.");
                return;
            }

            errorProvider1.SetError(txtFineFees, null);



        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
