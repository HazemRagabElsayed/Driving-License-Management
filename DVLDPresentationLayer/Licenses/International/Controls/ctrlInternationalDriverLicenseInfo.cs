using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Licenses.International.Controls
{
    public partial class ctrlInternationalDriverLicenseInfo : UserControl
    {
        public ctrlInternationalDriverLicenseInfo()
        {
            InitializeComponent();
        }



        int _InternationlaLicenseID;

        clsInternationalLicense _InternationalLicense;

        public int InternationalLicenseID
        {
            get { return _InternationlaLicenseID; }
        }

        public clsInternationalLicense SelectedInternationalLicenseInfo
        {
            get { return _InternationalLicense; }
        }

        void _LoadPersonImage(string PersonImage)
        {

            clsPerson Person = _InternationalLicense.DriverInfo.PersonInfo;

            //if (string.IsNullOrEmpty(Person.ImagePath))
            //{
            //                pbImage.Image = (Person.Gender == 0) ?
            //Properties.Resources.Male_512 : Properties.Resources.Female_512;
            //    return;
            //}

            //pbImage.ImageLocation = Person.ImagePath;



            pbImage.Image = (_InternationalLicense.DriverInfo.PersonInfo.Gender == 0) ?
                    Properties.Resources.Male_512 : Properties.Resources.Female_512;

            if (!string.IsNullOrEmpty(PersonImage))
                if (File.Exists(PersonImage))
                    //pbImage.Load(PersonImage);
                    pbImage.ImageLocation = PersonImage;
                else
                    MessageBox.Show($"Couldn't found Image path = {PersonImage}"
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }


    //    public void LoadLicenseInfo(string NationalNo)
    //    {
    //        clsPerson Person = clsPerson.Find(NationalNo);
    //        if (Person == null)
    //        {
    //            return;
    //        }

    //        clsDriver Driver = clsDriver.FindByPersonID(Person.PersonID);

    //        if (Driver == null)
    //        {
    //            return;
    //        }

    //       // _InternationalLicense = clsInternationalLicense.Find(Driver.DriverID);

    //        if (_InternationalLicense == null)
    //        {
    //            MessageBox.Show($"License with ID {_InternationlaLicenseID} not found!", "Error",
    //MessageBoxButtons.OK, MessageBoxIcon.Error);
    //            return;
    //        }


    //        lblName.Text = Person.FullName;
    //        lblIntLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
    //        lblNationalNo.Text = Person.NationalNo;
    //        lblGender.Text = (Person.Gender == 0) ? "Male" : "Female";
    //        lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
    //        lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
    //        lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
    //        lblDriverID.Text = _InternationalLicense.DriverID.ToString();
    //        lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();


    //        //lblIsDetained.Text = 
    //        //to be implemented Detained License class

    //        _LoadPersonImage(Person.ImagePath);

    //    }

        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {
            _InternationlaLicenseID = InternationalLicenseID;
            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                MessageBox.Show($"International License with ID = " +
                    $"{_InternationlaLicenseID} not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsPerson Person = _InternationalLicense.DriverInfo.PersonInfo;

            lblName.Text = Person.FullName;
            lblIntLicenseID.Text = _InternationlaLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = Person.NationalNo;
            lblGender.Text = (Person.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();


            //lblIsDetained.Text = 
            //to be implemented Detained License class

            _LoadPersonImage(Person.ImagePath);
        }
    }
}
