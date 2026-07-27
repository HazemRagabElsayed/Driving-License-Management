using DVLDBusinessLayer;
using MySolution.Global;
using MySolution.Global_Classes;
using MySolution.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;

namespace MySolution.Applications
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        int _LocalDrivingLicenseApplicationID;

        enum enTabControlIndex { PersonalInfo = 0, ApplicationInfo = 1 };
        enum EnSelectedFilterIndex { PersonID = 0, NationalNo = 1 };

        enum enMode { AddNew = 1, Update = 2}

        enMode _Mode;
        
        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enStatus.New;
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _Mode = enMode.Update;
        }



        void _ResetValuesForAddNew()
        {

            btnSave.Enabled = false;

            tbApplicationInfo.Enabled = false;

            lblActionTitle.Text = "New Local Driving License Application";

            ctrlPersonCardWithFilter1.SelectedFilterIndex((int)EnSelectedFilterIndex.NationalNo);

            lblDLApplicationID.Text = "[???]";

            lblApplicationDate.Text = DateTime.Now.ToString();

            lblApplicationFees.Text = clsApplicationType.
                Find(clsApplicationType.enAppType.NewLDLService)
                .ApplicationFees.ToString();

            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString("Class 3 - Ordinary driving license");

        }

        void _FillCbLicenseClass()
        {
            DataTable dt_LicenseClass = clsLicenseClass.GetAll();

            foreach (DataRow dr in dt_LicenseClass.Rows)
            {
                cbLicenseClass.Items.Add(dr["ClassName"].ToString());
            }

        }

        void _CenterTitleInMiddleOfWindow()
        {
            this.Text = lblActionTitle.Text;
            clsUtil.CenterLabelTitle(this.Size, lblActionTitle);
        }

        private void FrmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillCbLicenseClass();



            switch (_Mode)
            {
                case enMode.AddNew:

                    _ResetValuesForAddNew();

                    break;
                case enMode.Update:

                    ctrlPersonCardWithFilter1.
SelectedFilterIndex((int)EnSelectedFilterIndex.PersonID);

                    _ResetValuesForUpdate();
                    _LoadApplicationData();

                    break;
            }
            _CenterTitleInMiddleOfWindow();


        }

        void _FillApplicationWithData()
        {
            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.Person.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = Convert.ToDateTime(lblApplicationDate.Text);
            _LocalDrivingLicenseApplication.ApplicationTypeID = 
                clsApplicationType.Find
                (clsApplicationType.enAppType.NewLDLService).ApplicationTypeID;
            _LocalDrivingLicenseApplication.LastStatusDate = Convert.ToDateTime(lblApplicationDate.Text);
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

        }

        void _ResetValuesForUpdate()
        {
            ctrlPersonCardWithFilter1.EnableFilter = false;
            tbApplicationInfo.Enabled = true;
            btnSave.Enabled = true;
            lblActionTitle.Text = "Update Local Driving License Application";

            
        }

        void _LoadApplicationData()
        {

            _LocalDrivingLicenseApplication = 
                clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            ctrlPersonCardWithFilter1.LoadPersonData(_LocalDrivingLicenseApplication.ApplicantPersonID);

            lblDLApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = 
                _LocalDrivingLicenseApplication.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = 
                _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedBy.Text =
                _LocalDrivingLicenseApplication.CreatedByUserInfo.UserName;
            cbLicenseClass.SelectedIndex = cbLicenseClass.
                FindString(_LocalDrivingLicenseApplication.LicenseClassInfo.ClassName);

        }


       

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.Person != null)
            {
                tbApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcLocalDrivingLicenseApplicationInfo.SelectedIndex = (int)enTabControlIndex.ApplicationInfo;
            }
            else
            {
                MessageBox.Show("Please select a person", "Select a Person",
                         buttons: default, icon: MessageBoxIcon.Error);
                _ResetValuesForAddNew();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.Person == null)
            {

                MessageBox.Show("Please select a person", "Select a Person",
                         buttons: default, icon: MessageBoxIcon.Error);
                tcLocalDrivingLicenseApplicationInfo.SelectedIndex = (int)enTabControlIndex.PersonalInfo;
                return;
            }

            if (clsLocalDrivingLicenseApplication.DoesPersonHaveLicense
                (ctrlPersonCardWithFilter1.Person.PersonID, clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID))
            {
                MessageBox.Show("Person already have a license with same applied driving class" +
                    "Choose different driving class", "Not Allowed",
                        buttons: default, icon: MessageBoxIcon.Error);
                return;
            }

            int ApplicationID = -1;

            if (clsLocalDrivingLicenseApplication.PersonHasNewLicenseClassApplication
                (ref ApplicationID,ctrlPersonCardWithFilter1.Person.PersonID, clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID))
            {
                MessageBox.Show($"Choose another License Class," +
                    " the selected Person Already have an active application" +
                    $" for the selected class with Id = {ApplicationID}", "Error",
                         buttons: default, icon: MessageBoxIcon.Error);
                return;
            }

            _FillApplicationWithData();

            /*//if (_LocalDrivingLicenseApplication.ApplicationInfo.Save())
            //{
            //    _LocalDrivingLicenseApplication.ApplicationID = _LocalDrivingLicenseApplication.ApplicationInfo.ApplicationID;
            //}
            //else
            //{
            //    MessageBox.Show("Save Faild", "Error Saving Application"
            //        , MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}*/

            short MinAge = clsLicenseClass.
                Find(_LocalDrivingLicenseApplication.LicenseClassID).MinimumAllowedAge;

            if (clsPerson.GetAge(_LocalDrivingLicenseApplication.ApplicantPersonID)
                < MinAge)
            {
                MessageBox.Show($"Minimum Age for this License Class is : {MinAge}", "Not Allowed",
                         buttons: default, icon: MessageBoxIcon.Warning);
                return;
            }

            if (_LocalDrivingLicenseApplication.Save())
            {
                lblDLApplicationID.Text = _LocalDrivingLicenseApplication.
                    LocalDrivingLicenseApplicationID.ToString();

                MessageBox.Show("Data saved successfully", "Saved", buttons: default,
                    MessageBoxIcon.Information);

                _Mode = enMode.Update;
                _ResetValuesForUpdate();
                clsUtil.CenterLabelTitle(this.Size, lblActionTitle);
  
            }
            else
            {
                MessageBox.Show("Save Failed", "Error Saving Local Driving Application"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
