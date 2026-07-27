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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Tests
{



    public partial class frmAddEditTestAppointment : Form
    {

        int _LDLAppID;

        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.Vision;

        int _TestAppointmentID = -1;


        public frmAddEditTestAppointment(int DLAppID, clsTestType.enTestType TestTypeID,int TestAppointmentID = -1)
        {
            InitializeComponent();
            _LDLAppID = DLAppID;
            _TestTypeID = TestTypeID;
            _TestAppointmentID = TestAppointmentID;
        }


        private void frmAddTestAppointment_Load(object sender, EventArgs e)
        {

            //if (_TestAppointmentID == -1)
            //{
            //    if (clsLocalDrivingLicenseApplication.DoesPersonHaveActiveTestAppointmentPerTestType(_LDLAppID, _TestTypeID))
            //    {
            //        MessageBox.Show(@"Person Already have an active
            //                       appointment for this test, you cannot add new appointment"
            //                , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //        Close();
            //    }
            //}

            ctrlScheduleTest1.TestTypeID = _TestTypeID;
            ctrlScheduleTest1.LoadInfo(_LDLAppID, _TestAppointmentID);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
