using DVLDBusinessLayer;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using MySolution.Global_Classes;

namespace MySolution
{
    public partial class frmAddEditPersonInfo : Form
    {
        enum EnMode { AddNew = 0 , Update = 1 };
        enum EnGender { Male = 0, Female = 1 };

        string _ImageSource = "";
        EnMode _Mode;

        clsPerson _Person;
        int _PersonID;

        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            _Mode = EnMode.AddNew;

        }
        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
            _Mode = EnMode.Update;

        }

        private void _ResetDefaultValues()
        {

            _SetAllowedMinAge(18);
            _SetAllowedMaxAge(100);
            _LoadCountriesData();

            switch (_Mode)
            {
                case EnMode.AddNew:

                    this.Text = "Add New Person";
                    lblTitle.Text = "Add New Person";
                    _Person = new clsPerson();

                    lblPersonID.Text = "N/A";

                    txtFirstName.Text = "";
                    txtSecondName.Text = "";
                    txtThirdName.Text = "";
                    txtLastName.Text = "";

                    txtNationalNo.Text = "";
                    dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
                    rbMale.Checked = true;

                    txtPhone.Text = "";
                    txtEmail.Text = "";

                    //Use This
                    int CountryIndex = cbCountry.FindStringExact("Egypt");

                    cbCountry.SelectedIndex = CountryIndex;

                    //Or Use This instead 

                    //cbCountry.Text = "Egypt";

                    txtAddress.Text = "";

                    pbImage.ImageLocation = null;

                    llRemoveImage.Visible = false;

                    break;
                case EnMode.Update:

                    this.Text = "Update Person";
                    lblTitle.Text = "Update Person";
                    break;
            }

        }

        private void _SetAllowedMinAge(int Age)
        {
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-Age);
        }
        private void _SetAllowedMaxAge(int Age)
        {
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-Age);
        }
        private void _LoadPersonInfo()
        {

            if ((_Person = clsPerson.Find(_PersonID)) == null)
            {
                MessageBox.Show($"Person with ID {_Person.PersonID} doesn't exist");
                this.Close();
                return;
            }

                lblPersonID.Text = _Person.PersonID.ToString();
                
                txtFirstName.Text = _Person.FirstName;
                txtSecondName.Text = _Person.SecondName;
                txtThirdName.Text = _Person.ThirdName;
                txtLastName.Text = _Person.LastName;
                txtNationalNo.Text = _Person.NationalNo;
                dtpDateOfBirth.Text = _Person.DateOfBirth.ToString();

                if (_Person.Gender == Convert.ToInt16(EnGender.Male))
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }

                txtPhone.Text = _Person.Phone;
                txtEmail.Text = _Person.Email;
                cbCountry.Text = _Person.Country.CountryName;
                txtAddress.Text = _Person.Address;

                if (!string.IsNullOrEmpty(_Person.ImagePath))
                {
                    pbImage.ImageLocation = _Person.ImagePath;
                }

                llRemoveImage.Visible = (!string.IsNullOrEmpty(pbImage.ImageLocation));
         
        }
        private void _LoadCountriesData()
        {
            foreach (DataRow Row in clsCountry.GetAll().Rows)
            {
                cbCountry.Items.Add(Row["CountryName"].ToString());
            }

        }
        private void _FillPersonObjectWithData()
        {
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            
            _Person.NationalNo = txtNationalNo.Text ;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            
            _Person.Gender = Convert.ToInt16(rbFemale.Checked);
            
            _Person.Phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;
            
            _Person.NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;
            
            _Person.Address = txtAddress.Text;

            _Person.ImagePath = pbImage.ImageLocation;

        }

        private bool _HandlePersonImage()
        {

            if(pbImage.ImageLocation == _Person.ImagePath)
            {
                return true;
            }

            if(!string.IsNullOrEmpty(_Person.ImagePath))
            {
                try
                {
                    File.Delete(_Person.ImagePath);
                   
                }
                catch
                {
                    return false;
                }
                
            }

            if (string.IsNullOrEmpty(pbImage.ImageLocation))
            {
                return true;
            }

            _ImageSource = pbImage.ImageLocation;

            if (clsUtil.CopyImageToProjectFolder(ref _ImageSource))
            {
                _Person.ImagePath = _ImageSource;
                return true;
            }

            MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private void _ValidateNationalNo()
        {
            if(txtNationalNo.Text.Trim() == "")
            {
                epValidation.SetError(txtNationalNo, "This field is required!");

                return;
            }
            if (clsPerson.IsExist(txtNationalNo.Text))
            {
                epValidation.SetError(txtNationalNo, "National Number is used by another person!");
                return;
            }
            
                epValidation.SetError(txtNationalNo, null);
            
        }
        private void _ValidateEmail()
        {

            if (txtEmail.Text.EndsWith("@gmail.com"))
            {
                epValidation.SetError(txtEmail,null);
            }
            else
            {
                epValidation.SetError(txtEmail, "Invalid Email Address Foramt");          
            }
        }

       private void _ChangeImageBasedOnGenderSelection()
        {
            if (string.IsNullOrEmpty(pbImage.ImageLocation))
            {
                if (rbFemale.Checked == true)
                {
                    pbImage.Image = Properties.Resources.Female_512;
                }
                else
                {
                    pbImage.Image = Properties.Resources.Male_512;
                }
            }
        }
        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == EnMode.Update)
                _LoadPersonInfo();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!" +
                    ", put the mouse over the red icon(s) to see the error", "Validation Error"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage())
            {
                MessageBox.Show("Erorr : Handling person image!", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonObjectWithData();

            if (_Person.Save())
            {
                _PersonID = _Person.PersonID;
                lblPersonID.Text = _PersonID.ToString();
                lblTitle.Text = "Update Person";



                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _PersonID);
            }
            else
            {
                MessageBox.Show("Error Data saving failed" ,"Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rbGender_Checked_Changed(object sender, EventArgs e)
        {
            _ChangeImageBasedOnGenderSelection();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ofdSetImage.ShowDialog() == DialogResult.OK)
            {
                pbImage.ImageLocation = ofdSetImage.FileName;
               
                llRemoveImage.Visible = true;
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;
            _ChangeImageBasedOnGenderSelection();
            llRemoveImage.Visible = false;

        }


        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (_Mode == EnMode.AddNew)
            {
                _ValidateNationalNo();
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if(txtEmail.Text != "")
            {
                _ValidateEmail();
            }
        }

        private void txtEmpty_Validating(object sender, CancelEventArgs e)
        {
            if(((TextBox)sender).Text.Trim() == "")
            {
                epValidation.SetError((TextBox)sender, "This field is required!");
            }
            else
            {
                epValidation.SetError((TextBox)sender, null);
            }
        }
    }
}
