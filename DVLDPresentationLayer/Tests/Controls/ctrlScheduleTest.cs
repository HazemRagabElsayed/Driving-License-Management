using DVLDBusinessLayer;
using MySolution.Global;
using MySolution.Global_Classes;
using MySolution.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Tests
{
    public partial class ctrlScheduleTest : UserControl
    {
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        enum enMode { AddNew, Update }

        enum enCreationMode { FirstScheduleTime, ScheduleRetakeTest }

        enCreationMode _CreationMode;

        enMode _Mode = enMode.AddNew;

        int _LDLAppID = -1;

        int _TestAppointmentID = -1;

        clsTestAppointment _TestAppointment;

        clsLocalDrivingLicenseApplication _LDLApp;

        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.Vision;
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.Vision:

                        pbTestType.Image = Properties.Resources.Vision_512;
                        gbTestType.Text = "Vision Test";


                        break;
                    case clsTestType.enTestType.Written:

                        pbTestType.Image = Resources.Written_Test_512;
                        gbTestType.Text = "Written Test";

                        break;
                    case clsTestType.enTestType.Street:

                        pbTestType.Image = Resources.Street_Test_32;
                        gbTestType.Text = "Street Test";

                        break;
                }

            }
        }


        private bool _LoadTestAppointmentInfo()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error Test Appointment With ID" +
                            $"{_TestAppointmentID} Not Found!",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_TestAppointment.RetakeTestApplicationID != -1)
            {
                lblRTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblRAppFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRAppFees.Text = "0";
                lblRTestAppID.Text = "N/A";
            }

                lblTakeTestFees.Text =
                    _TestAppointment.TestTypeInfo.TestTypeFees.ToString();

            dtbDate.MinDate =
                DateTime.Compare(_TestAppointment.AppointmentDate, DateTime.Now) < 0 ?
                _TestAppointment.AppointmentDate : DateTime.Now;

            dtbDate.Value = _TestAppointment.AppointmentDate;

            return true;
        }

        bool _HandleActiveTestAppointmentConstraint()
        {

            if(enMode.AddNew == _Mode  && _LDLApp.DoesPersonHaveActiveTestAppointmentPerTestType(TestTypeID))
            {
                btnSave.Enabled = false;
                dtbDate.Enabled = false;
                lblAppointmentConstraintMessage.Text = "Person Already have an active"
                                  + "appointment for this test, you cannot add new appointment";
                lblAppointmentConstraintMessage.Visible = true;
                return false;
            }

            lblAppointmentConstraintMessage.Visible = false;
            return true;
        }
   
        bool _HandleLockedTestAppointmentContstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                dtbDate.Enabled = false;
                lblAppointmentConstraintMessage.Text = "Person already sat for the test, Appointment Locked";
                lblAppointmentConstraintMessage.Visible = true;
                btnSave.Enabled = false;
                return false;
            }

            lblAppointmentConstraintMessage.Visible = false;
            return true;
        }

        bool _HandlePreviousPassedTestsConstraints()
        {

            switch (_TestTypeID)
            {
                case clsTestType.enTestType.Vision:

                    lblAppointmentConstraintMessage.Visible = false;
                    return true;

                case clsTestType.enTestType.Written:

                    if (!_LDLApp.DoesPersonPassedPerTestType(clsTestType.enTestType.Vision))
                    {
                        btnSave.Enabled = false;
                        dtbDate.Enabled = false;
                        lblAppointmentConstraintMessage.Text = "you cannot take This Test Type" +
                            "You should take Vision Test First";
                        lblAppointmentConstraintMessage.Visible = true;
                        return false;
                    }
                    break;
                case clsTestType.enTestType.Street:
                    if (!_LDLApp.DoesPersonPassedPerTestType(clsTestType.enTestType.Written))
                    {
                        btnSave.Enabled = false;
                        dtbDate.Enabled = false;
                        lblAppointmentConstraintMessage.Text = "you cannot take This Test Type" +
                            "You should take Written Test First";
                        lblAppointmentConstraintMessage.Visible = true;
                        return false;
                    }
                    break;
            }
            lblAppointmentConstraintMessage.Visible = false;
            return true;
        }

        bool _HandleRetakeTestApp()
        {
            

            if (enCreationMode.ScheduleRetakeTest == _CreationMode && enMode.AddNew == _Mode)
            {

                clsApplication App = new clsApplication();
                App.ApplicantPersonID = _LDLApp.ApplicantPersonID;
                App.ApplicationDate = DateTime.Now;
                App.ApplicationTypeID = clsApplicationType.enAppType.RetakeTest;
                App.ApplicationStatus = clsApplication.enStatus.Completed;
                App.LastStatusDate = DateTime.Now;
                App.PaidFees = Convert.ToSingle(lblRAppFees.Text);
                App.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!App.Save())
                {
                    MessageBox.Show(@"Error : Cannot Create Retake Test Application"
        , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _TestAppointment.RetakeTestApplicationID = -1;
                    return false;
                }
                else
                {
                    _TestAppointment.RetakeTestApplicationID = App.ApplicationID;
                    lblRTestAppID.Text = App.ApplicationID.ToString();
                }

            }
            
            
            return true;
        }
        public void LoadInfo(int LDLAppID, int TestAppointmentID = -1)
        {
            _LDLAppID = LDLAppID;
            _TestAppointmentID = TestAppointmentID;

            _Mode = _TestAppointmentID == -1 ? enMode.AddNew : enMode.Update;
            _CreationMode =
                clsLocalDrivingLicenseApplication.DoesAttendedTestType(_LDLAppID, _TestTypeID)
                ?
                enCreationMode.ScheduleRetakeTest : enCreationMode.FirstScheduleTime;

            _LDLApp = clsLocalDrivingLicenseApplication.Find(_LDLAppID);

            if (_LDLApp == null)
            {
                MessageBox.Show("Error : Local Driving Application With ID" +
                                $"{_LDLAppID} Not Found!",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDLAppID.Text = _LDLAppID.ToString();
            lblLicenseClass.Text = _LDLApp.LicenseClassInfo.ClassName;
            lblName.Text = _LDLApp.ApplicantPersonInfo.FullName;
            lblTrial.Text = _LDLApp.GetNumberOfTries(_TestTypeID).ToString();

            if (enCreationMode.ScheduleRetakeTest == _CreationMode)
            {
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRAppFees.Text = clsApplicationType.Find(clsApplicationType.enAppType.RetakeTest).ApplicationFees.ToString();
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRAppFees.Text = "0";
                lblRTestAppID.Text = "N/A";
            }

            clsUtil.CenterLabelTitle(this.Size, lblTitle);

            if(enMode.AddNew == _Mode)
            {
                dtbDate.MinDate = clsLocalDrivingLicenseApplication.
                    GetLastTestAppointment(LDLAppID).AddDays(1);
                dtbDate.Value = dtbDate.MinDate;
                lblTakeTestFees.Text = clsTestType.Find(_TestTypeID).TestTypeFees.ToString();
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentInfo())
                    return;
            }


            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleLockedTestAppointmentContstraint())
                return;

            if (!_HandlePreviousPassedTestsConstraints())
                return;

            lblTotalFees.Text = (Convert.ToSingle(lblTakeTestFees.Text) +
                Convert.ToSingle(lblRAppFees.Text)).ToString();
        }

        void _FillTestAppointmentData()
        {
             _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LDLAppID;
            _TestAppointment.AppointmentDate = dtbDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblTakeTestFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!_HandleRetakeTestApp())
                return;
            
            _FillTestAppointmentData();


            if (_TestAppointment.Save())
            {
                MessageBox.Show(@"Date Saved Successfully.",
                        "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;

                return;
            }
            else
            {
                MessageBox.Show(@"Could not Save Test Appointment",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.ScheduleRetakeTest)
                {
                    clsApplication.Delete(_TestAppointment.RetakeTestApplicationID);
                    _TestAppointment.RetakeTestApplicationInfo = null;
                    
                }
            }

        }
    }
}
