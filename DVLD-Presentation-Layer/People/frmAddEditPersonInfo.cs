using DVLDBusinessLayer;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.IO;

namespace MySolution
{
    public partial class frmAddEditPersonInfo : Form
    {
        enum EnMode { AddNew = 0 , Update = 1 };
        enum EnGender { Male = 0, Female = 1 };

        string _ImageExtension = "";
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
            _LoadCountryData();

            llRemoveImage.Visible = (!string.IsNullOrEmpty(pbImage.ImageLocation));
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

            if ((_Person = clsPerson.Find(_PersonID)) != null)
            {

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
                    llRemoveImage.Visible = true;
                }


            }
            else
            {
                MessageBox.Show($"Person with ID {_Person.PersonID} doesn't exist");
            }
        }
        private void _LoadCountryData()
        {
            foreach (DataRow Row in clsCountry.GetAll().Rows)
            {
                cbCountry.Items.Add(Row["CountryName"].ToString());
            }

            //Use This
            int CountryIndex = cbCountry.FindStringExact("Jordan");

            cbCountry.SelectedIndex = CountryIndex;

            //Or Use This instead 

            //cbCountry.Text = "Jordan";
        }
        private bool _AssureFinalChanges()
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

                return _SaveImageToPeopleFolder(_Person.ImagePath);


        }

       private void _ValidateNationalNo()
        {
            epValidation.Clear();
            if (clsPerson.IsExist(txtNationalNo.Text))
            {
                epValidation.SetError(txtNationalNo, "National Number is used by another person!");
            }
            else
            {
                epValidation.Clear();
            }
        }
       private void _ValidateEmail()
        {
            epValidation.Clear();

            if (txtEmail.Text.EndsWith("@gmail.com"))
            {
                epValidation.Clear();
            }
            else
            {
                epValidation.SetError(txtEmail, "Invalid Email Address Foramt");          
            }
        }
       private bool _IsThereInValidFields()
        {

            bool InValid = false;

            foreach (Control control in pInputtedData.Controls)
            {
               
           
            // If the control is a TextBox, collect it
            if ((control is System.Windows.Forms.TextBox textBox ||
                    control is System.Windows.Forms.RichTextBox richtextbox)
                    &&
                    control.Name != "txtThirdName"
                    && control.Name != "txtThirdName"
                    && control.Name != "txtEmail")
                {
                    if(control.Text == "")
                    {
                        epValidation.SetError(control, "This field is required!");
                        InValid = true;
                    }
                   
                }
            }
            return InValid;
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
       private bool _DeleteImage(string Image)
        {
            if (!string.IsNullOrEmpty(Image))
            {
                try
                {
                    File.Delete(Image);
                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
                
            }
            else
            {
                return true;
            }
               
        }
       private bool _CopyImage(string Image)
        {
            if (!Directory.Exists("D:\\Repos\\Driving-License-Management\\DVLD-People-Images"))
            {
                Directory.CreateDirectory(_Person.ImagePath);
            }
            if (!string.IsNullOrEmpty(Image))
            {
                try
                {
                    File.Copy(Image, _Person.ImagePath);

                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
                

            }
            else
            {
                return true;
            }
        }

        bool _SaveImageToPeopleFolder(string SourceImage)
        {

            bool NoError = true;

            if (pbImage.ImageLocation == null && SourceImage == "")
            {
                return NoError;
            }

            

            if (!string.IsNullOrEmpty(SourceImage))
            {
                if (!_DeleteImage(SourceImage))
                {

                    MessageBox.Show("Error : Old image is not deleted");
                    NoError = false;
                }
   
            }

            if (pbImage.ImageLocation == null)
            {
                _Person.ImagePath = "";
            }
            else
            {
                _ImageExtension = Path.GetExtension(pbImage.ImageLocation);
                _Person.ImagePath = "D:\\Repos\\Driving-License-Management\\DVLD-People-Images\\" + Guid.NewGuid() + _ImageExtension;

                if (!_CopyImage(pbImage.ImageLocation))
                {

                    MessageBox.Show("Error : New image is not copied");
                    NoError = false;
                }
            }

            return NoError;
            

        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {

            _ResetDefaultValues();

            if (_Mode == EnMode.Update)
            {
                _LoadPersonInfo();
                lblTitle.Text = "Update Person";
            }
            else
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (_IsThereInValidFields())
            {
                MessageBox.Show("Some fields are not valid!" +
                    ", put the mouse over the red icon(s) to see the error", "Validation Error"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            if (!_AssureFinalChanges())
            {
                return;
            }


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
                MessageBox.Show("Error Data saving failed");
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

    }
}
