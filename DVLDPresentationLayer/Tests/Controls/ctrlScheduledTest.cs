using DVLDBusinessLayer;
using MySolution.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        int _TestAppointmentID = -1;

        int _TestID;

        public int TestID
        {
            get { return _TestID; }
            set { lblTestID.Text = value.ToString(); }
        }

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
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int TestAppointmentID)
        {

            _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if(_TestAppointment == null)
            {
                MessageBox.Show("Error Test Appointment With ID" +
            $"{(_TestAppointmentID).ToString()} Not Found!",
            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestID = _TestAppointment.TestID;

            int DLAppID = _TestAppointment.LocalDrivingLicenseApplicationID;
            
            _LDLApp =
                _TestAppointment.LocalDrivingLicenseApplicationInfo;

            if (_LDLApp == null)
            {
                MessageBox.Show("Error Local Driving Application With ID" +
            $"{(DLAppID).ToString()} Not Found!",
            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDLAppID.Text = DLAppID.ToString();
            lblLicenseClass.Text = _LDLApp.LicenseClassInfo.ClassName;
            lblName.Text = _LDLApp.ApplicantPersonInfo.FullName;
            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblTrial.Text = clsLocalDrivingLicenseApplication.
                TotalTestTrialsPerTestType(DLAppID, TestTypeID).ToString();

            lblTestID.Text = _TestID == -1 ? "Not Taken Yet" : _TestID.ToString();

            lblTakeTestFees.Text =
            _TestAppointment.PaidFees.ToString();
        }
    }
}
