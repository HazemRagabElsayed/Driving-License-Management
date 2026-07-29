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

namespace DVLD.Applications.Release_Detained_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {

        int _LicenseID = -1;

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = true;

        }
        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationFees.Text =
                clsApplicationType.
                Find(clsApplicationType.enAppType.ReleaseDetainedDL)
                .ApplicationFees.ToString();

           
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

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is not detained, Choose another one ",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRelease.Enabled = false;
                return;
            }

            clsDetainedLicense DetainInfo = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo;

            lblDetainID.Text = DetainInfo.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(DetainInfo.DetainDate);
            lblDetainedBy.Text = DetainInfo.CreatedByUserInfo.UserName;
            lblFineFees.Text = DetainInfo.FineFees.ToString();
            lblTotalFees.Text =(
                                   Convert.ToSingle(lblApplicationFees.Text)
                                 + Convert.ToSingle(lblFineFees.Text)

                                ).ToString();


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
            btnRelease.Enabled = true;
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {


            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validating Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to release this detained license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int ApplicationID = -1;

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.
                 ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID))
            {
                MessageBox.Show($"Detained License Released Successfully", "License Released",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblApplicationID.Text = ApplicationID.ToString();
                ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);

                btnRelease.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                llShowNewLicensesInfo.Enabled = true;

                return;
            }

            MessageBox.Show($"Detained License Release Failed", "Release Failed",
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
            frmLicenseInfo frm = new frmLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }
        private void frmReleaseDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
