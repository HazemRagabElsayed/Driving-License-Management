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

namespace MySolution.Applications
{
    public partial class frmEditApplicationType : Form
    {
        clsApplicationType.enAppType _ApplicationTypeID;
        clsApplicationType _ApplicationType;
        public frmEditApplicationType(clsApplicationType.enAppType ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        void _FillApplicationTypeObject()
        {
            _ApplicationType.ApplicationTypeTitle = txtTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToSingle(txtFees.Text);
        }

        void _FillApplicationTypeForm()
        {
            lblID.Text = _ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            if ((_ApplicationType = clsApplicationType.Find(_ApplicationTypeID)) == null)
            {
                MessageBox.Show($"ApplicationType with ID {_ApplicationTypeID} doesn't exist");
                return;
            }

            _FillApplicationTypeForm();
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
  
            return; 
        
            _FillApplicationTypeObject();

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error Data saving failed","Error" ,MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void frmEditApplicationType_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner?.Activate();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if(txtTitle.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title Cannot be empty");
                return;
            }
            errorProvider1.SetError(txtTitle, null);
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (txtFees.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees Cannot be empty");
                return;
            }
            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number");
                return;
            }

            errorProvider1.SetError(txtFees, null);
        }
    }
}
