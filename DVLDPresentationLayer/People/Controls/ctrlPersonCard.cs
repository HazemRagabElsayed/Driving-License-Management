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

        public int PersonID
        {
            get { return _PersonID; }
            set
            {
                _PersonID = value;
                LoadPersonInfo();
            }
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

        void LoadPersonInfo()
        {
            if(_PersonID  != -1)
            {

                clsPerson Person = clsPerson.Find(_PersonID);
                lblPersonID.Text = _PersonID.ToString();

                if (Person != null)
                {
                    lblName.Text = Person.FullName;

                    lblNationalNo.Text = Person.NationalNo;
                    lblGender.Text = Person.Gender == 0 ? "Male" : "Female";
                    lblEmail.Text = Person.Email;
                    lblAddress.Text = Person.Address;
                    lblDateOfBirth.Text = Person.DateOfBirth.ToString();
                    lblPhone.Text = Person.Phone;
                    lblCountry.Text = Person.Country.CountryName;

                    if (!string.IsNullOrEmpty(Person.ImagePath))
                    {
                        pbImage.ImageLocation = Person.ImagePath;
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
                    llEditPersonInfo.Visible = true;

                }
          
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
            LoadPersonInfo();
        }


    }
}
