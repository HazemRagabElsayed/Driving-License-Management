using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }


        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            ApplicationID = -1;
            LicenseClassID = -1;
            _Mode = enMode.AddNew;
        }
        private clsLocalDrivingLicenseApplication
           (int LocalDrivingLicenseApplicationID, int ApplicationID,
             int ApplicantPersonID,
             DateTime ApplicationDate,
             clsApplicationType.enAppType ApplicationTypeID,
             enStatus ApplicationStatus,
             DateTime LastStatusDate,
             float PaidFees,
             int CreatedByUserID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            _Mode = enMode.Update;
        }

        new public static DataTable GetAll()
        {
            return clsLocalDrivingLicenseApplicationData.GetAll();
        }

        new public static clsLocalDrivingLicenseApplication Find(int LocalDrivingLicenseApplicationID)
        {

            int ApplicationID = -1;
            int LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationData.FindByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                clsApplication Application = clsApplication.Find(ApplicationID);

                if (Application != null)
                {
                    return new clsLocalDrivingLicenseApplication( LocalDrivingLicenseApplicationID,  ApplicationID,
              Application.ApplicantPersonID,
              Application.ApplicationDate,
             Application.ApplicationTypeID,
              Application.ApplicationStatus,
              Application.LastStatusDate,
              Application.PaidFees,
              Application.CreatedByUserID,  LicenseClassID);
                }
                else
                {
                    return null;
                }

                
            }
            else
            {
                return null;
            }
        }

        private bool _AddNew()
        {

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNew(ApplicationID, LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }
        private bool _Update()
        {
            return clsLocalDrivingLicenseApplicationData.Update(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }

        new public bool Save()
        {

            base._Mode = (clsApplication.enMode)this._Mode;

            if (!base.Save())
            {
                return false;
            }

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

        public  bool Delete()
        {

            if (!clsLocalDrivingLicenseApplicationData.Delete(this.LocalDrivingLicenseApplicationID))
            {
                return false;
            }
            return clsApplication.Delete(this.ApplicationID);
        }

        new public static bool IsExist(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.IsExist(LocalDrivingLicenseApplicationID);
        }

        public static bool PersonHasNewLicenseClassApplication(ref int ApplicationID ,int ApplicantPersonID, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.
                PersonHasNewLicenseClassApplication
                (ref ApplicationID, ApplicantPersonID, LicenseClassID);
        }

        public static byte GetPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.CountPassedTests(LocalDrivingLicenseApplicationID);
        }

        public static bool DoesPersonHaveActiveTestAppointmentPerTestType
            (int LocalDrivingLicenseApplicationID
            , clsTestType.enTestType TestTypeID)
        {

            return clsTestAppointment.
                DoesPersonHaveActiveTestAppointmentPerTestType
                (LocalDrivingLicenseApplicationID,
                (int)TestTypeID);

        }
        public  bool DoesPersonHaveActiveTestAppointmentPerTestType
            (clsTestType.enTestType TestTypeID)
        {

            return clsTestAppointment.
                DoesPersonHaveActiveTestAppointmentPerTestType
                (this.LocalDrivingLicenseApplicationID,
                (int)TestTypeID);

        }

        public static bool DoesPersonPassedPerTestType(int LocalDrivingLicenseApplicationID
            , clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.DoesPersonPassedPerTestType
                (LocalDrivingLicenseApplicationID,
                (int)TestTypeID);
        }

        public  bool DoesPersonPassedPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.DoesPersonPassedPerTestType
                (this.LocalDrivingLicenseApplicationID,
                (int)TestTypeID);
        }
        public static bool DoesAttendedTestType
            (int LocalDrivingLicenseApplicationID,
            clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.DoesPersonAttendedTestType
                (LocalDrivingLicenseApplicationID,
                (int)TestTypeID);
        }

        public static DateTime GetLastTestAppointment(int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointment.
                GetLastTestAppointment
                (LocalDrivingLicenseApplicationID);
        }

        public  clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.GetLastTestPerTestType(this.LocalDrivingLicenseApplicationID,TestTypeID);
        }

        public  static short TotalTestTrialsPerTestType(int LocalDrivingLicenseApplicationID,
            clsTestType.enTestType TestTypeID)
        {
            return clsTestData.
                TotalTestTrialsPerTestType(LocalDrivingLicenseApplicationID,(int)TestTypeID);
        }

        public short GetNumberOfTries(clsTestType.enTestType TestTypeID)
        {
            return clsTestData.
                TotalTestTrialsPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool DoesLDAppApplicantHaveLicense(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.
                DoesLDAppApplicantHaveLicense(LocalDrivingLicenseApplicationID);
        }

        public static bool DoesLDAppApplicantActiveHaveLicense(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.
                DoesLDAppApplicantActiveHaveLicense(LocalDrivingLicenseApplicationID);
        }

        public static bool DoesPersonHaveLicense
                (int PersonID, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.
                DoesPersonHaveLicense(PersonID, LicenseClassID);
        }


        public static byte CountPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.CountPassedTests(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }
        public int IssueLicenseForFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver  Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if( Driver == null)
            {
                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (!Driver.Save())
                {
                    return -1;
                }
            }
            
            DriverID = Driver.DriverID;

            clsLicense License = new clsLicense();

            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.ExpirationDate = License.IssueDate
                .AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                this.SetComplelte();

                return License.LicenseID;
            }

            return -1;

        }

        public  int GetLicenseID()
        {
            return clsLicense.GetLicenseID(this.ApplicationID);
        }


    }
}
