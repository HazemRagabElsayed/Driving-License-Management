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
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MySolution.Licenses.Local.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;

        protected virtual void SelectLicense(int License)
        {
            Action<int> handler = OnLicenseSelected;

            if (handler != null)
            {
                handler(License);
            }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public clsLicense SelectedLicenseInfo
        { get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; } }

        int _LicenseID;

        public int LicenseID
        { get { return ctrlDriverLicenseInfo1.LicenseID; } }


        public bool FilterEnabled
        {
            set
            {
                gbLicenseFilter.Enabled = value;
            }
            get
            {
                return gbLicenseFilter.Enabled;
            }
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            ctrlDriverLicenseInfo1.LoadLicenseInfo(LicenseID);
            txtLicenseID.Text = LicenseID.ToString();
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;

            if (OnLicenseSelected != null && FilterEnabled)
                SelectLicense(_LicenseID);  
        }


        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
                
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if(txtLicenseID.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This Field is Required!");
                return;
            }
            if(!clsValidation.IsNumber(txtLicenseID.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "License ID must be a number !");
                return;
            }

            errorProvider1.SetError(txtLicenseID, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);



        }
        }
    }
