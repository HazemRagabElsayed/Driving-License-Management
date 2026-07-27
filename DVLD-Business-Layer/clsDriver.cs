using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;

            _Mode = enMode.AddNew;
        }

        private clsDriver
            (int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            this.CreatedDate = CreatedDate;

            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {

            this.DriverID = clsDriverData.AddNew(PersonID, CreatedByUserID);

            return (this.DriverID != -1);
        }
        private bool _Update()
        {
            return clsDriverData.Update(this.DriverID, PersonID, CreatedByUserID);
        }

        public static clsDriver Find(int DriverID)
        {

            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;



            if (clsDriverData.FindByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;



            if (clsDriverData.FindByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static clsDriver Find(int PersonID, int CreatedByUserID)
        {

            int DriverID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.FindByPersonIDAndCreatedByUserID(PersonID, CreatedByUserID, ref DriverID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }


        public static DataTable GetAll()
        {
            return clsDriverData.GetAll();
        }

        public static bool IsExist(int DriverID)
        {
            return clsDriverData.IsExist(DriverID);
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



        public static bool Delete(int DriverID)
        {

            return clsDriverData.Delete(DriverID);
        }

        public static bool IsPersonADriver(int PersonID)
        {
            return clsDriverData.IsPersonADriver(PersonID);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetDriverLicenses(DriverID);
        }
    }
}
