using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDetainedLicense
    {


        protected enum enMode { AddNew = 0, Update = 1 };
        protected enMode _Mode;


        public int DetainID { get; set;}
        public int LicenseID { get; set;}
        public DateTime DetainDate { get; set;}
        public float FineFees { get; set;}
        public int CreatedByUserID { get; set;}

        public clsUser CreatedByUserInfo { get; set;}
        public bool IsReleased { get; set;}
        public DateTime? ReleaseDate{ get; set;}
        public int ReleasedByUserID { get; set;}
        public int ReleaseApplicationID { get; set;}


        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = null;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
          
            _Mode = enMode.AddNew;

        }

        private clsDetainedLicense
            (int DetainID,
             int LicenseID,
             DateTime DetainDate,
             float FineFees,
             int CreatedByUserID,
             bool IsReleased,
             DateTime? ReleaseDate,
             int ReleasedByUserID,
             int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;


            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {

            this.DetainID = clsDetainedLicenseData.AddNew(
              LicenseID,
              FineFees,
              CreatedByUserID);

            return (this.DetainID != -1);
        }
        private bool _Update()
        {
            return clsDetainedLicenseData.Update(DetainID,
              LicenseID,
              DetainDate,
              FineFees,
              CreatedByUserID,
              IsReleased,
              ReleaseDate,
              ReleasedByUserID,
              ReleaseApplicationID);
        }

        public static clsDetainedLicense FindByID(int DetainID)
        {

             int LicenseID= -1;
            DateTime DetainDate=DateTime.Now;
             float FineFees= 0;
             int CreatedByUserID= -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1 ;


            if (clsDetainedLicenseData.FindByID(
                 DetainID,
            ref  LicenseID,
            ref  DetainDate,
            ref  FineFees,
            ref  CreatedByUserID,
            ref  IsReleased,
            ref  ReleaseDate,
            ref  ReleasedByUserID,
            ref  ReleaseApplicationID))
            {
                return new clsDetainedLicense( DetainID,
              LicenseID,
              DetainDate,
              FineFees,
              CreatedByUserID,
              IsReleased,
              ReleaseDate,
              ReleasedByUserID,
              ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {

            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;


            if (clsDetainedLicenseData.FindByLicenseID(
                 LicenseID,
            ref DetainID,
            ref DetainDate,
            ref FineFees,
            ref CreatedByUserID,
            ref IsReleased,
            ref ReleaseDate,
            ref ReleasedByUserID,
            ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID,
              LicenseID,
              DetainDate,
              FineFees,
              CreatedByUserID,
              IsReleased,
              ReleaseDate,
              ReleasedByUserID,
              ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAll()
        {
            return clsDetainedLicenseData.GetAll();
        }

        public static bool IsExist(int DetainID)
        {
            return clsDetainedLicenseData.IsExist(DetainID);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
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



        public static bool Delete(int DetainID)
        {

            return clsDetainedLicenseData.Delete(DetainID);
        }

        public  bool ReleaseByLicenseID(
            int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseByLicenseID(this.LicenseID, ReleasedByUserID, ReleaseApplicationID);
        }

        public  bool Release(
    int ReleasedByUserID,
    int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.Release(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }
    }
}
