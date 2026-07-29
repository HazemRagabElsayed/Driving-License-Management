using DVLDBusinessLayer;
using MySolution.Global;
using MySolution.Properties;
using MySolution.Tests.Controls;
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
    public partial class frmTakeTest : Form
    {


        clsTestType.enTestType _TestTypeID;

        int _TestAppointmentID;

        clsTest _Test;

        public frmTakeTest(int TestAppointmentID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;

        }

        void _FillTestData()
        {
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {

            btnSave.Enabled = (_TestAppointmentID != -1);

            ctrlScheduledTest1.TestTypeID = _TestTypeID;
            ctrlScheduledTest1.LoadInfo(_TestAppointmentID);

            int TestID = ctrlScheduledTest1.TestID;

            if (TestID != -1)
            {
                ctrlScheduledTest1.TestID = TestID;

                _Test = clsTest.Find(TestID);

                rbFail.Checked = !(rbPass.Checked = _Test.TestResult);
                txtNotes.Text = _Test.Notes;

                lblUserMessage.Visible = true;

                rbPass.Enabled = false;
                rbFail.Enabled = false;

                return;
            }


            _Test = new clsTest();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save?" +
                "After that you cannot change the Pass/Fail results!",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
                _FillTestData();

                if (_Test.Save())
                {
                MessageBox.Show(@"Date Saved Successfully.",
                           "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlScheduledTest1.TestID = _Test.TestID;
                }
                else
                {
                    MessageBox.Show(@"Could not Save Test",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


        }
    }
}
