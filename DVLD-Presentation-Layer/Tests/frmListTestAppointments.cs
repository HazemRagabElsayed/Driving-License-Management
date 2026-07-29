using DVLDBusinessLayer;
using MySolution.Properties;
using MySolution.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Applications
{
    public partial class frmListTestAppointments : Form
    {
        public frmListTestAppointments()
        {
            InitializeComponent();
        }

        int _LDLAppID;


        clsTestType.enTestType _TestTypeID;

        DataTable _dtAllTestAppointments;
        DataTable _dtTestAppointments;

        public frmListTestAppointments(int LDLAppID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _LDLAppID = LDLAppID;
            _TestTypeID = TestTypeID;

        }

        void _RefreshTestAppointmentsData()
        {
            _LoadTestAppointmentsData();
        }

        void _LoadTestTypeImageAndTitle()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.Vision:

                    pbTestType.Image = Properties.Resources.Vision_512;
                    this.Text = "Vision Test Appointments";
                    lblTitle.Text = "Vision Test Appointments";

                    break;
                case clsTestType.enTestType.Written:

                    pbTestType.Image = Resources.Written_Test_512;
                    this.Text = "Written Test Appointments";
                    lblTitle.Text = "Written Test Appointments";


                    break;
                case clsTestType.enTestType.Street:

                    pbTestType.Image = Resources.Street_Test_32;
                    this.Text = "Street Test Appointments";
                    lblTitle.Text = "Street Test Appointments";


                    break;

            }
        }

        void _LoadTestAppointmentsData()
        {
            //_dtAllTestAppointments = clsTestAppointment.GetAll();

            //_dtAllTestAppointments.DefaultView.RowFilter =
            //    string.Format("LocalDrivingLicenseApplicationID = {0} and TestTypeID = {1}", _LDLAppID, (int)_TestTypeID);

            //_dtTestAppointments = _dtAllTestAppointments.DefaultView
            //    .ToTable(false, "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");


            _LoadTestTypeImageAndTitle();
            ctrlApplicationInfo1.LoadLDLAppInfo(_LDLAppID);

            _dtTestAppointments = clsTestAppointment.GetAllTestAppointmentsPerLDLAppAndTestType(_LDLAppID, _TestTypeID);
            dgvTestAppointmentsList.DataSource = _dtTestAppointments;
            lblRecordsNumber.Text = dgvTestAppointmentsList.Rows.Count.ToString();

            if (dgvTestAppointmentsList.Rows.Count > 0)
            {
                dgvTestAppointmentsList.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointmentsList.Columns[0].Width = 120;

                dgvTestAppointmentsList.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointmentsList.Columns[1].Width = 200;

                dgvTestAppointmentsList.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointmentsList.Columns[2].Width = 100;

                dgvTestAppointmentsList.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointmentsList.Columns[3].Width = 100;

            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestAppointmentsData();

        }

        private void pbAddNewTestAppointment_Click(object sender, EventArgs e)
        {


            clsLocalDrivingLicenseApplication LDLApp =
                clsLocalDrivingLicenseApplication.Find(_LDLAppID);

            if (LDLApp.DoesPersonHaveActiveTestAppointmentPerTestType(_TestTypeID))
            {
                MessageBox.Show(@"Person Already have an active
                    appointment for this test, you cannot add new appointment"
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = LDLApp.GetLastTestPerTestType(_TestTypeID);

            if(LastTest == null)
            {
                frmAddEditTestAppointment frmAddTestAppointment1 = new frmAddEditTestAppointment(_LDLAppID, _TestTypeID);
                frmAddTestAppointment1.ShowDialog();
                _RefreshTestAppointmentsData();
                return;
            }

            if(LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake failed test"
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            //short NumberOfTestsPassedIfHeDidnotPassedThisTestType = (short)((short)_TestTypeID - 1);

            //if (clsLocalDrivingLicenseApplication.GetPassedTests(_LDLAppID) != NumberOfTestsPassedIfHeDidnotPassedThisTestType)
            //{
            //    MessageBox.Show("This person already passed this test before, you can only retake failed test"
            //        , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //foreach (DataRow dr in _dtTestAppointments.Rows)
            //{
            //    if (Convert.ToBoolean(dr["IsLocked"]) == false)
            //    {
            //        MessageBox.Show(@"Person Already have an active
            //        appointment for this test, you cannot add new appointment"
            //        , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //}

            frmAddEditTestAppointment frmAddTestAppointment2 = new frmAddEditTestAppointment(_LDLAppID, _TestTypeID);
            frmAddTestAppointment2.ShowDialog();
            frmListTestAppointments_Load(null, null);

        }

        private void editTestAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditTestAppointment frmEditTestAppointment =
                new frmAddEditTestAppointment
                (_LDLAppID,_TestTypeID,(int)dgvTestAppointmentsList.CurrentRow.Cells[0].Value);
            frmEditTestAppointment.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //short NumberOfTestsPassedIfHeDidnotPassedThisTestType = (short)((short)_TestTypeID - 1);

            //if ((bool)dgvTestAppointmentsList.CurrentRow.Cells[3].Value == true)
            //{
            //    MessageBox.Show("This Test Appointment Is Locked"
            //        , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            frmTakeTest frmTakeTest = new frmTakeTest
                ((int)dgvTestAppointmentsList.CurrentRow.Cells[0].Value,_TestTypeID);
            frmTakeTest.ShowDialog();
            frmListTestAppointments_Load(null, null);

        }
    }
}
