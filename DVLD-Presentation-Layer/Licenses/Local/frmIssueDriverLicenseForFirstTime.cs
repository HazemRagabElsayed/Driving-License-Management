using DVLDBusinessLayer;
using MySolution.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace MySolution.Licenses
{
    public partial class frmIssueDriverLicenseForFirstTime : Form
    {

        int _LDLAppID;

        clsLocalDrivingLicenseApplication _LDLApp;

        //clsLicense _License;

        //clsDriver _Driver;

        public frmIssueDriverLicenseForFirstTime(int LDLAppID)
        {
            InitializeComponent();
            _LDLAppID = LDLAppID;
            
        }
        /*
                bool _AddApplicantToDriversList()
                {
                    if (clsDriver.IsPersonADriver(_LDLApp.ApplicantPersonID))
                        return true;


                    _Driver = new clsDriver();
                    _Driver.PersonID = _LDLApp.ApplicantPersonID;
                    _Driver.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                    if (!_Driver.Save())
                    {
                        MessageBox.Show(" Driver Date Saving Failed", "Error"
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    return true;
                }
                 void _FillLicenseData()
                 {
                     clsLicenseClass LicenseClass = clsLicenseClass.Find(_LDLApp.LicenseClassID);

                      _License = new clsLicense();
                     _License.ApplicationID = _LDLAppID;
                     _License.DriverID = _Driver.DriverID;
                     _License.LicenseClass = _LDLApp.LicenseClassID;
                     _License.ExpirationDate = _License.IssueDate
                         .AddYears(LicenseClass.DefaultValidityLength);
                     _License.Notes = txtNotes.Text;
                     _License.PaidFees = LicenseClass.ClassFees;
                     _License.IssueReason = clsLicense.enIssueReason.FirstTime;
                     _License.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                 }

                bool _IssueLicense()
                {
                    if (!_License.Save())
                    {
                        return false;
                    }

                    return true;
                }
        */

        private void frmIssueDriverLicenseForFirstTime_Load(object sender, EventArgs e)
        {
            if (clsLocalDrivingLicenseApplication.IsExist(_LDLAppID))
            {
                if (!clsLocalDrivingLicenseApplication.PassedAllTests(_LDLAppID))
                {
                    MessageBox.Show("Person didn't pass all tests!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                if (clsLocalDrivingLicenseApplication.DoesLDAppApplicantActiveHaveLicense(_LDLAppID))
                {
                    MessageBox.Show("Person already has an active license of this class type",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;

                }

                ctrlApplicationInfo1.LoadLDLAppInfo(_LDLAppID);
            }
            else
            {
                MessageBox.Show($"LDApp with ID {_LDLAppID} doesn't exist",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

    //        MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    //        if (!clsLocalDrivingLicenseApplication.PassedAllTests(_LDLAppID) ||
    //            clsLocalDrivingLicenseApplication.DoesLDAppApplicantHaveLicense(_LDLAppID))
    //        {
    //            MessageBox.Show("Error : Process of Issue of License Failed", "Issue Failed",
    //MessageBoxButtons.OK, MessageBoxIcon.Error);
    //            return;
    //        }



            _LDLApp = clsLocalDrivingLicenseApplication.Find(_LDLAppID);

            if (_LDLApp == null)
            {
                MessageBox.Show($"Local Driving Application with ID {_LDLAppID} is not found!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseID;

            if((LicenseID = _LDLApp.IssueLicenseForFirstTime(txtNotes.Text, clsGlobal.CurrentUser.UserID)) != -1)
            {


                MessageBox.Show($"License Issued Successfully with License ID = " +
                                        $"{LicenseID}", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
                return;

            }

            MessageBox.Show("License was not issued", "Issue Failed",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);



            //            if (_AddApplicantToDriversList())
            //            {

            //                _FillLicenseData();

            //                if (_IssueLicense())
            //                {
            //                    MessageBox.Show($"License Issued Successfully with License ID = " +
            //                        $"{_License.LicenseID}", "Saved",
            //MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    return;
            //                }
            //                MessageBox.Show("License Data Saving failed", "Saved",
            //MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                clsDriver.Delete(_Driver.DriverID);
            //            }





        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
