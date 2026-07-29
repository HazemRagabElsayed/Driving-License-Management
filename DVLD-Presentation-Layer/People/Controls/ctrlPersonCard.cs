using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution
{
    public partial class ctrlPersonCard : UserControl
    {
        private int _PersonID = -1;

        clsPerson _Person;

        public int PersonID
        {
            get { return _PersonID; }
            set
            {
                _PersonID = value;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();

        }

        void _ResetDefaultValues()
        {
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblPhone.Text = "[????]";
            lblCountry.Text = "[????]";
            
            pbImage.Image = Properties.Resources.Male_512;

            llEditPersonInfo.Visible = false;
        }

        void _FillPersonForm()
        {
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _PersonID.ToString();

            lblName.Text = _Person.FullName;

            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.Country.CountryName;
            llEditPersonInfo.Visible = true;
        }

        private void _LoadPersonImage()
        {
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pbImage.ImageLocation = _Person.ImagePath;
            }
            else
            {
                if (lblGender.Text == "Male")
                {
                    pbImage.Image = Properties.Resources.Male_512;
                }
                else
                {
                    pbImage.Image = Properties.Resources.Female_512;
                }
            }
        }

        public void LoadPersonInfo(int PersonID)
        {
            clsPerson Person;

            if((Person = clsPerson.Find(PersonID)) != null)
            {
                _Person = Person;
                _FillPersonForm();
                _LoadPersonImage();
          
            }
            else
            {
                _ResetDefaultValues();
            }
        }

        public void LoadPersonInfo(string NationalNo)
        {
            clsPerson Person;

            if ((Person = clsPerson.Find(NationalNo)) != null)
            {
                _Person = Person;
                _FillPersonForm();
                _LoadPersonImage();

            }
            else
            {
                _ResetDefaultValues();
            }
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frmEditPersonInfo = new frmAddEditPersonInfo(_PersonID);
            frmEditPersonInfo.ShowDialog();
            LoadPersonInfo(_PersonID);
        }


    }
}
