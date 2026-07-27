using DVLDDataAccessLayer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicense : clsApplication
    {
        protected enMode Mode;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public clsLicense IssuedUsingLocalLicenseInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public clsInternationalLicense()
        {

            base.ApplicationID = -1;
            base.ApplicantPersonID = -1;
            base.ApplicationDate = DateTime.Now;
            base.ApplicationTypeID = clsApplicationType.enAppType.NewInternationalL;
            base.ApplicationStatus = enStatus.New;
            base.LastStatusDate = DateTime.Now;
            base.PaidFees = 0;
            base.CreatedByUserID = -1;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(1);
            this.IsActive = true;

            _Mode = enMode.AddNew;

        }

        private clsInternationalLicense
            (int ApplicationID,
             int ApplicantPersonID,
             DateTime ApplicationDate,
             enStatus ApplicationStatus,
             DateTime LastStatusDate,
             float PaidFees,
             int CreatedByUserID,
             int InternationalLicenseID,
              int DriverID,
              int IssuedUsingLocalLicenseID,
              DateTime IssueDate,
              DateTime ExpirationDate,
              bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = clsApplicationType.enAppType.NewInternationalL;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.Find(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssuedUsingLocalLicenseInfo = clsLicense.Find(IssuedUsingLocalLicenseID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;

            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {

            this.InternationalLicenseID = clsInternationalLicenseData.AddNew(ApplicationID,
              DriverID,
              IssuedUsingLocalLicenseID,
              CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }
        private bool _Update()
        {
            return clsInternationalLicenseData.Update(InternationalLicenseID,
             ApplicationID,
              DriverID,
              IssuedUsingLocalLicenseID,
              IssueDate,
              ExpirationDate,
              IsActive,
              CreatedByUserID);
        }

        new public static clsInternationalLicense Find(int InternationalLicenseID)
        {

            int ApplicationID = -1;

            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;


            if (clsInternationalLicenseData.FindByID(InternationalLicenseID,
             ref ApplicationID,
             ref DriverID,
             ref IssuedUsingLocalLicenseID,
             ref IssueDate,
             ref ExpirationDate,
             ref IsActive,
             ref CreatedByUserID))
            {

                clsApplication App = clsApplication.Find(ApplicationID);

                return new clsInternationalLicense( 
                    App.ApplicationID,
                    App.ApplicantPersonID,
                    App.ApplicationDate,
                    App.ApplicationStatus,
                    App.LastStatusDate,
                    App.PaidFees,
                    App.CreatedByUserID,
                    InternationalLicenseID,
                    DriverID,
                    IssuedUsingLocalLicenseID,
                    IssueDate,
                    ExpirationDate,
                    IsActive
              );
            }
            else
            {
                return null;
            }
        }



        new public static DataTable GetAll()
        {
            return clsInternationalLicenseData.GetAll();
        }
        public static DataTable GetAllByPersonID(int PersonID)
        {
            return clsInternationalLicenseData.GetAllByPersonID(PersonID);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverLicenses(DriverID);
        }

        new public static bool IsExist(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.IsExist(InternationalLicenseID);
        }

        public static bool ApplicationHasLicense(int ApplicationID)
        {
            return clsInternationalLicenseData.DoesApplicationhaveInternationalLicense(ApplicationID);
        }

        public bool Save()
        {

            base._Mode = Mode;

            if(!base.Save()) return false;

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        Delete(this.ApplicationID);
                        return false;
                    }

                case enMode.Update:
                    return _Update();



            }
            return false;
        }



        new public static bool Delete(int InternationalLicenseID)
        {

            return clsInternationalLicenseData.Delete(InternationalLicenseID);
        }

       public static bool HasInternationalLicense(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseID(DriverID) != -1;
        }

        public static int GetActiveInternationalLicenseID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseID(DriverID);
        }

    }
}
