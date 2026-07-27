using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestAppointment
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public clsTestType TestTypeInfo { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplicationInfo { get; set; }

        public int TestID { get
            {
                return _GetTestID();
            }
        }

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = clsTestType.enTestType.Vision;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
            _Mode = enMode.AddNew;

        }

        private clsTestAppointment
            (int TestAppointmentID,
             clsTestType.enTestType TestTypeID,
             int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate,
             float PaidFees,
             int CreatedByUserID,
             bool IsLocked,
             int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;

            this.TestTypeID = TestTypeID;
            TestTypeInfo = clsTestType.Find(TestTypeID);

            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            LocalDrivingLicenseApplicationInfo = 
                clsLocalDrivingLicenseApplication
                .Find(LocalDrivingLicenseApplicationID);


            this.AppointmentDate = DateTime.Now;
            this.PaidFees = PaidFees;

            this.CreatedByUserID = CreatedByUserID;
            CreatedByUserInfo = clsUser.Find(CreatedByUserID);

            this.IsLocked = IsLocked;

            this.RetakeTestApplicationID = RetakeTestApplicationID;
            RetakeTestApplicationInfo = clsApplication.Find(RetakeTestApplicationID);


            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {

            this.TestAppointmentID = clsTestAppointmentData.AddNew((int)TestTypeID,
              LocalDrivingLicenseApplicationID,
              AppointmentDate,
              PaidFees,
              CreatedByUserID,
              RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }
        private bool _Update()
        {
            return clsTestAppointmentData.Update(TestAppointmentID,
             (int)TestTypeID,
              LocalDrivingLicenseApplicationID,
              AppointmentDate,
              PaidFees,
              CreatedByUserID,
              IsLocked,
              RetakeTestApplicationID);
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {


            int TestTypeID = -1;
             int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
             float PaidFees = 0;
             int CreatedByUserID = -1;
            bool IsLocked = false;
             int RetakeTestApplicationID = -1;




            if (clsTestAppointmentData.FindByID( TestAppointmentID,
            ref TestTypeID,
            ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate,
            ref PaidFees,
            ref CreatedByUserID,
            ref IsLocked,
            ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID,
             (clsTestType.enTestType)TestTypeID,
             LocalDrivingLicenseApplicationID,
             AppointmentDate,
             PaidFees,
             CreatedByUserID,
             IsLocked,
             RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
        }



        public static DataTable GetAll()
        {
            return clsTestAppointmentData.GetAll();
        }

        public static DataTable GetAllTestAppointmentsPerLDLAppAndTestType(int LDLApp,clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.
                GetAllTestAppointmentsPerTestType
                ( LDLApp, (int) TestTypeID);

        }


        public static bool IsExist(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsExist(TestAppointmentID);
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



        public static bool Delete(int TestAppointmentID)
        {

            return clsTestAppointmentData.Delete(TestAppointmentID);
        }


        public static DateTime GetLastTestAppointment(int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentData.
                GetLastTestAppointment(LocalDrivingLicenseApplicationID);
        }

        public static bool DoesPersonHaveActiveTestAppointmentPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.DoesPersonHaveActiveTestAppointmentPerTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool DoesPersonPassedPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return clsTestAppointmentData.DoesPersonPassedPerTestType(LocalDrivingLicenseApplicationID,TestTypeID);
        }
        public static bool DoesPersonAttendedTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.DoesPersonAttendedTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(this.TestAppointmentID);
        }

    }
}
