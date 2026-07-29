using DVLDBusinessLayer;
using MySolution.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Tests.TestTypes
{
    public partial class frmEditTestType : Form
    {

        clsTestType.enTestType _TestTypeID;
        clsTestType _TestType;

        public frmEditTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }


        void _AssureFinalChanges()
        {
            _TestType.TestTypeTitle = txtTitle.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees = Convert.ToSingle(txtFees.Text);
        }

        void _LoadTestTypesData()
        {
            lblID.Text =((int) _TestTypeID).ToString();
            txtTitle.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestTypeFees.ToString();
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            if ((_TestType = clsTestType.Find(_TestTypeID)) == null)
            {
                MessageBox.Show($"TestType with ID {((int)_TestTypeID).ToString()} doesn't exist");
                return;
            }

            _LoadTestTypesData();
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSave.PerformClick();
                return;
            }

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!" +
                    ", put the mouse over the red icon(s) to see the error", "Validation Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _AssureFinalChanges();


            if (_TestType.Save())
            {
                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error Data saving failed");
            }
        }

        private void frmEditTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner?.Activate();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title Cannot be empty");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (txtFees.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees Cannot be empty");
                return;
            }
            else
            {

                errorProvider1.SetError(txtFees, null);
            }
            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (txtDescription.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description Cannot be empty");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            }
        }
    }
}
