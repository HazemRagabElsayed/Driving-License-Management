using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Licenses
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {


        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }


        int _LicenseID;

        clsLicense _License;

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }

        void _LoadPersonImage(string PersonImage)
        {

            clsPerson Person = _License.ApplicationInfo.ApplicantPersonInfo;

            //if (string.IsNullOrEmpty(Person.ImagePath))
            //{
            //                pbImage.Image = (Person.Gender == 0) ?
            //Properties.Resources.Male_512 : Properties.Resources.Female_512;
            //    return;
            //}

            //pbImage.ImageLocation = Person.ImagePath;



            pbImage.Image = (_License.DriverInfo.PersonInfo.Gender == 0) ?
                    Properties.Resources.Male_512 : Properties.Resources.Female_512;

            if (!string.IsNullOrEmpty(PersonImage))
                if (File.Exists(PersonImage))
                    //pbImage.Load(PersonImage);
                    pbImage.ImageLocation = PersonImage;
                else
                    MessageBox.Show($"Couldn't found Image path = {PersonImage}"
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }


        public void LoadLicenseInfo(string NationalNo)
        {
            clsPerson Person = clsPerson.Find(NationalNo);
            if (Person == null)
            {
                return;
            }

            clsDriver Driver = clsDriver.FindByPersonID(Person.PersonID);

            if (Driver == null)
            {
                return;
            }

             _License = clsLicense.FindByDriverID(Driver.DriverID);

            if (_License == null)
            {
                MessageBox.Show($"License with ID {LicenseID} not found!", "Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblClass.Text = clsLicenseClass.Find(_License.LicenseClass).ClassName;
            lblName.Text = Person.FullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = NationalNo;
            lblGender.Text = (Person.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text =  string.IsNullOrEmpty(_License.Notes) ? "No Notes" : _License.Notes;
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = Driver.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();


            //lblIsDetained.Text = 
            //to be implemented Detained License class

             _LoadPersonImage(Person.ImagePath);

        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
             _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                MessageBox.Show($"License with ID {LicenseID} not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsPerson Person = _License.ApplicationInfo.ApplicantPersonInfo;

            lblClass.Text = clsLicenseClass.Find(_License.LicenseClass).ClassName;
            lblName.Text = Person.FullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = Person.NationalNo;
            lblGender.Text = (Person.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = string.IsNullOrEmpty(_License.Notes) ? "No Notes" : _License.Notes;
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";

            _LoadPersonImage(Person.ImagePath);
        }
    }
}
