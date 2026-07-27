using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicense
    {
        protected enum enMode { AddNew = 0, Update = 1 };
        protected enMode _Mode;

        public enum enIssueReason 
        { FirstTime = 1, Renew = 2,
            ReplacementForDamaged = 3, ReplacementForLost = 4 };

        public enum enClassName { 
            SmallMotorcycle = 1,
            HeavyMotorcycleLicense = 2,
            OrdinaryDrivingLicense = 3,
            Commercial = 4,
            Agricultural = 5,
            SmallAndMediumBus = 6,
            TruckAndHeavyVehicle = 7
        }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int LicenseClass {  get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                return _GetIssueReasonText(this.IssueReason);
            }
        }

        public bool IsDetained
        {
            get { return clsDetainedLicense.IsLicenseDetained(LicenseID); }
        }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public clsDetainedLicense DetainedInfo {  get; set; }

        public clsLicense()
        {

            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.PaidFees = 0;
            this.IsActive = true;
            IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;

        }

        private clsLicense
            (int LicenseID,
              int ApplicationID,
              int DriverID,
              int LicenseClass,
              DateTime IssueDate,
              DateTime ExpirationDate,
              string Notes,
              float PaidFees,
              bool IsActive,
              enIssueReason IssueReason,
              int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplication.Find(ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.Find(DriverID);
            this.LicenseClass = LicenseClass;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClass);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(LicenseID);

            _Mode = enMode.Update;
        }

        private string _GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.ReplacementForDamaged:
                    return "Replacement For Damaged";
                case enIssueReason.ReplacementForLost:
                    return "Replacement For Lost";
                default:
                    return "";
            }
        }
        private bool _AddNew()
        {

            this.LicenseID = clsLicenseData.AddNew( ApplicationID,
              DriverID,
              LicenseClass,
              IssueDate,
              ExpirationDate,
              Notes,  PaidFees,  IsActive,
              (short) IssueReason,
              CreatedByUserID);

            return (this.LicenseID != -1);
        }
        private bool _Update()
        {
            return clsLicenseData.Update(LicenseID,
             ApplicationID,
              DriverID,
              LicenseClass,
              IssueDate,
              ExpirationDate,
              Notes, PaidFees, IsActive,
              (short)IssueReason,
              CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {

            int ApplicationID = -1;

            int DriverID = -1;
             int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
             DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            short IssueReason = (short)enIssueReason.FirstTime;
            int CreatedByUserID = -1;


            if (clsLicenseData.FindByID( LicenseID,
             ref  ApplicationID,
             ref  DriverID,
             ref  LicenseClass,
             ref  IssueDate,
             ref  ExpirationDate,
             ref  Notes,
             ref  PaidFees,
             ref  IsActive,
             ref  IssueReason,
             ref  CreatedByUserID))
            {
                return new clsLicense(LicenseID,
             ApplicationID,
              DriverID,
              LicenseClass,
              IssueDate,
              ExpirationDate,
              Notes, PaidFees, IsActive,
              (enIssueReason)IssueReason,
              CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsLicense FindByDriverID(int DriverID)
        {

            int ApplicationID = -1;
            int LicenseID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            short IssueReason = (short)enIssueReason.FirstTime;
            int CreatedByUserID = -1;


            if (clsLicenseData.FindByDriverID(DriverID, ref LicenseID,
             ref ApplicationID,
             ref LicenseClass,
             ref IssueDate,
             ref ExpirationDate,
             ref Notes,
             ref PaidFees,
             ref IsActive,
             ref IssueReason,
             ref CreatedByUserID))
            {
                return new clsLicense(LicenseID,
             ApplicationID,
              DriverID,
              LicenseClass,
              IssueDate,
              ExpirationDate,
              Notes, PaidFees, IsActive,
              (enIssueReason)IssueReason,
              CreatedByUserID);
            }
            else
            {
                return null;
            }
        }



        public static DataTable GetAll()
        {
            return clsLicenseData.GetAll();
        }

        public static DataTable GetAllByPersonID(int PersonID)
        {
            return clsLicenseData.GetAllByPersonID(PersonID);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public static bool IsExist(int LicenseID)
        {
            return clsLicenseData.IsExist(LicenseID);
        }

        public static bool ApplicationHasLicense(int ApplicationID)
        {
            return clsLicenseData.DoesApplicationhaveLicense(ApplicationID);
        }

        public bool Save()
        {
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
                        return false;
                    }

                case enMode.Update:
                    return _Update();



            }
            return false;
        }



        public static bool Delete(int LicenseID)
        {

            return clsLicenseData.Delete(LicenseID);
        }

        public bool IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool DeactivateCurrentLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }

        public clsLicense Renew(string Notes, int CreatedByUserID)
        {
            if (!this.IsLicenseExpired())
                return null;

                clsApplication App = new clsApplication();
                App.ApplicantPersonID = this.DriverInfo.PersonID;
                App.ApplicationDate = DateTime.Now;
                App.ApplicationTypeID = 
                clsApplicationType.enAppType.RenewDLService;
                App.ApplicationStatus = clsApplication.enStatus.Completed;
                App.LastStatusDate = DateTime.Now;
                App.PaidFees = clsApplicationType.
                Find(App.ApplicationTypeID).ApplicationFees;
                App.CreatedByUserID = CreatedByUserID;

            if (!App.Save())
                return null;

            clsLicenseClass LicenseClass = clsLicenseClass.Find(this.LicenseClass);

            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = App.ApplicationID;
            NewLicense.DriverID = this.DriverInfo.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.
                AddYears(LicenseClass.DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = LicenseClass.ClassFees;
            NewLicense.IssueReason = enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
                return null;

            this.DeactivateCurrentLicense();

            return NewLicense;


        }
        public clsLicense Replace(int CreatedByUserID, enIssueReason IssueReason)
        {
            

                clsApplication App = new clsApplication();
                App.ApplicantPersonID = this.DriverInfo.PersonID;
                App.ApplicationDate = DateTime.Now;

                App.ApplicationTypeID =  
                IssueReason == enIssueReason.ReplacementForLost?
                clsApplicationType.enAppType.ReplacementLostDL
                : clsApplicationType.enAppType.ReplacementDamagedDL;

                App.ApplicationStatus = clsApplication.enStatus.Completed;
                App.LastStatusDate = DateTime.Now;
                App.PaidFees = clsApplicationType.
                Find(App.ApplicationTypeID).ApplicationFees;
                App.CreatedByUserID = CreatedByUserID;

            if (!App.Save())
                return null;


            clsLicense ReplacedLicense = new clsLicense();
            ReplacedLicense.ApplicationID = App.ApplicationID;
            ReplacedLicense.DriverID = this.DriverInfo.DriverID;
            ReplacedLicense.LicenseClass = this.LicenseClass;
            ReplacedLicense.IssueDate = DateTime.Now;
            ReplacedLicense.ExpirationDate = this.ExpirationDate;
            ReplacedLicense.Notes = this.Notes;
            ReplacedLicense.PaidFees = 0;
            ReplacedLicense.IssueReason = IssueReason;
            ReplacedLicense.CreatedByUserID = CreatedByUserID;

            if (!ReplacedLicense.Save())
                return null;

            this.DeactivateCurrentLicense();

            return ReplacedLicense;


        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            if (!this.IsDetained)
                return false;

            clsApplication App = new clsApplication();
            App.ApplicantPersonID = this.DriverInfo.PersonID;
            App.ApplicationDate = DateTime.Now;
            App.ApplicationTypeID =clsApplicationType.enAppType.ReleaseDetainedDL;
            App.ApplicationStatus = clsApplication.enStatus.Completed;
            App.LastStatusDate = DateTime.Now;
            App.PaidFees = clsApplicationType.Find(App.ApplicationTypeID).ApplicationFees;
            App.CreatedByUserID = ReleasedByUserID;

            if (!App.Save())
            {
                ReleaseApplicationID = -1;
                return false;
            }

            /*clsDetainedLicense DetainedLicense = clsDetainedLicense.FindByLicenseID(LicenseID);
            DetainedLicense.ReleaseApplicationID = App.ApplicationID;
            DetainedLicense.IsReleased = true;
            DetainedLicense.ReleaseDate = DateTime.Now;
            DetainedLicense.ReleasedByUserID = ReleasedByUserID;
            if (!DetainedLicense.Save())
                return false;*/

            /* if (!clsDetainedLicense.ReleaseByLicenseID(LicenseID, ReleasedByUserID, App.ApplicationID))
                 return false;*/

            ReleaseApplicationID = App.ApplicationID;

            return this.DetainedInfo.Release(ReleasedByUserID, ReleaseApplicationID);
        }

        public int Detain(int CreatedByUserID, float FineFees)
        {

            clsDetainedLicense DetainedLicense = new clsDetainedLicense();
            DetainedLicense.LicenseID = this.LicenseID;
            DetainedLicense.CreatedByUserID = CreatedByUserID;
            DetainedLicense.FineFees = FineFees;

            if (!DetainedLicense.Save())
                return -1;

            return DetainedLicense.DetainID;

        }
        public static int GetLicenseID(int ApplicationID)
        {
            return clsLicenseData.GetLicenseID(ApplicationID);
        }

        public static bool HasInternationalLicense(int DriverID)
        {
            return clsInternationalLicense.HasInternationalLicense(DriverID);
        }

        public static int GetActiveInternationalLicenseID(int DriverID)
        {
            return clsInternationalLicense.GetActiveInternationalLicenseID(DriverID);

        }

       /* public clsInternationalLicense IssueInternationalLicense(int CreatedByUserID)
        {
            if (this.LicenseClass != (int)enClassName.OrdinaryDrivingLicense || this.IsActive == false 
                || HasInternationalLicense(this.LicenseID))
            {
                return null;
            }

            clsApplication App = new clsApplication();
            App.ApplicantPersonID = this.DriverInfo.PersonID;
            App.ApplicationDate = DateTime.Now;
            App.ApplicationTypeID = clsApplicationType.enAppType.NewInternationalL;
            App.ApplicationStatus = clsApplication.enStatus.Completed;
            App.LastStatusDate = DateTime.Now;
            App.PaidFees = clsApplicationType.
            Find(App.ApplicationTypeID).ApplicationFees;
            App.CreatedByUserID = CreatedByUserID;

            if (!App.Save())
                return null;

            clsInternationalLicense NewInternationalLicense = new clsInternationalLicense();
            NewInternationalLicense.ApplicationID = App.ApplicationID;
            NewInternationalLicense.DriverID = this.DriverID;
            NewInternationalLicense.IssuedUsingLocalLicenseID = this.LicenseID;
            NewInternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            NewInternationalLicense.CreatedByUserID = CreatedByUserID;

            if (!NewInternationalLicense.Save())
            {
                clsApplication.Delete(App.ApplicationID);
                return null;
            }

            return NewInternationalLicense;

        }
       */

    }
}
