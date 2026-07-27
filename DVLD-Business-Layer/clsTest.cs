using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }

        public clsTestAppointment TestAppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUser CreatedByUserInfo { get; set; }

        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }
        private clsTest
           (int TestID, int TestAppointmentID, bool TestResult,
            string Notes, int CreatedByUserID
            )
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            _Mode = enMode.Update;
        }

        public static DataTable GetAll()
        {
            return clsTestData.GetAll();
        }

        public static clsTest Find(int TestID)
        {

            int TestAppointmentID = -1;
            int CreatedByUserID = -1;
            bool TestResult = false;
            string Notes = "";

            if (clsTestData.FindByID(TestID, ref TestAppointmentID
                , ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID,
                    TestResult,Notes, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNew()
        {

            this.TestID = clsTestData.AddNew(TestAppointmentID,
                    TestResult, Notes, CreatedByUserID);
            return (this.TestID != -1);
        }
        private bool _Update()
        {
            return clsTestData.Update(TestID, TestAppointmentID,
                    TestResult, Notes, CreatedByUserID);
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

        public static bool Delete(int TestID)
        {

            return clsTestData.Delete(TestID);
        }

       public static clsTest GetLastTestPerTestType
            (int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            int CreatedByUserID = -1;
            bool TestResult = false;
            string Notes = "";

            if (clsTestData.GetLastTestPerTestType
                (LocalDrivingLicenseApplicationID,
                (int)TestTypeID,
                ref TestID,
                ref TestAppointmentID
                , ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID,
                    TestResult, Notes, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }


        public static byte CountPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.CountPassedTests(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.CountPassedTests(LocalDrivingLicenseApplicationID) == 3;
        }
    }
}
