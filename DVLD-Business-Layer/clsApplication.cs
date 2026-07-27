using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {
        protected enum enMode { AddNew = 0, Update = 1 };
        protected enMode _Mode;

        public enum enStatus { New = 1, Cancelled = 2, Completed = 3 };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson ApplicantPersonInfo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public clsApplicationType.enAppType ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo { get; set; }
        public enStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }



        public clsPerson Person { get; set; }

        public clsApplication()
        {

            this.ApplicationID = -1;
            this.ApplicantPersonID    = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = clsApplicationType.enAppType.NewLDLService;
            this.ApplicationStatus = enStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;

        }

        private clsApplication
            (int ApplicationID,
             int ApplicantPersonID,
             DateTime ApplicationDate,
             clsApplicationType.enAppType ApplicationTypeID,
             enStatus ApplicationStatus,
             DateTime LastStatusDate,
             float PaidFees,
             int CreatedByUserID)
        {
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

            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {

            this.ApplicationID = clsApplicationData.AddNew(ApplicantPersonID,
             ApplicationDate,
             (int)ApplicationTypeID,
             (byte)ApplicationStatus,
             LastStatusDate,
             PaidFees,
             CreatedByUserID);

            return (this.ApplicationID != -1);
        }
        private bool _Update()
        {
            return clsApplicationData.Update(ApplicationID,
             ApplicantPersonID,
             ApplicationDate,
             (int)ApplicationTypeID,
             (byte)ApplicationStatus,
             LastStatusDate,
             PaidFees,
             CreatedByUserID);
        }

        public static clsApplication Find(int ApplicationID)
        {

           int ApplicantPersonID = -1;
           DateTime ApplicationDate = DateTime.Now;
           int ApplicationTypeID = -1;
            byte ApplicationStatus = (byte)enStatus.New;
           DateTime LastStatusDate = DateTime.Now;
           float PaidFees = 0;
           int CreatedByUserID = -1;


            if (clsApplicationData.FindByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
             ref ApplicationTypeID,
             ref ApplicationStatus,
             ref LastStatusDate,
             ref PaidFees,
             ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID,  ApplicantPersonID,  ApplicationDate,
              (clsApplicationType.enAppType) ApplicationTypeID,
              (enStatus) ApplicationStatus,
              LastStatusDate,
              PaidFees,
              CreatedByUserID);
            }
            else
            {
                return null;
            }
        }



        public static DataTable GetAll()
        {
            return clsApplicationData.GetAll();
        }

        public static bool IsExist(int ApplicationID)
        {
            return clsApplicationData.IsExist(ApplicationID);
        }

        public static bool PersonHasApplication(int ApplicantPersonID)
        {
            return clsApplicationData.DoesPersonhaveApplication(ApplicantPersonID);
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



        public static bool Delete(int ApplicationID)
        {

            return clsApplicationData.Delete(ApplicationID);
        }

        //public static bool UpdateStatus(int ApplicationID, string ApplicationStatus)
        //{
        //    return clsApplicationData.UpdateStatus(ApplicationID, ApplicationStatus);
        //}

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)enStatus.Cancelled );
        }

        public bool SetComplelte()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)enStatus.Completed);
        }


    }
}
