using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsUser
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public int PersonID { get; set; }

        public clsPerson Person { get; set; }

        public clsUser()
        {
            UserID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            PersonID = -1;

            _Mode = enMode.AddNew;
        }

        private clsUser
            (int UserID, string UserName, string Password, bool IsActive, int PersonID)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.Person = clsPerson.Find(PersonID);
            this.PersonID = PersonID;

            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {

            this.UserID = clsUserData.AddNew( UserName,  Password,  IsActive,  PersonID);

            return (this.UserID != -1);
        }
        private bool _Update()
        {
            return clsUserData.Update(this.UserID, UserName, Password, IsActive, PersonID);
        }

        public static clsUser Find(int UserID)
        {

            string UserName = "";
            string Password = "";
            bool IsActive = false;
            int PersonID = -1;


            if (clsUserData.FindByID(UserID,ref UserName, ref Password, ref IsActive,ref PersonID))
            {
                return new clsUser(UserID, UserName, Password, IsActive, PersonID);
            }
            else
            {
                return null;
            }
        }

        public static clsUser Find(string UserName , string Password)
        {

            int UserID = -1;
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.FindByUserNameAndPassword(UserName, Password,ref UserID, ref IsActive, ref PersonID))
            {
                return new clsUser(UserID, UserName, Password, IsActive, PersonID);
            }
            else
            {
                return null;
            }
        }

        public static clsUser Find(string UserName)
        {

            int UserID = -1;
            string Password = "";
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.FindByUserName(UserName, ref Password, ref UserID, ref IsActive, ref PersonID))
            {
                return new clsUser(UserID, UserName, Password, IsActive, PersonID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAll()
        {
            return clsUserData.GetAll();
        }

        public static bool IsExist(int UserID)
        {
            return clsUserData.IsExist(UserID);
        }

        public static bool IsExist(string UserName)
        {
            return clsUserData.IsExist(UserName);
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



        public static bool Delete(int UserID)
        {

            return clsUserData.Delete(UserID);
        }

        public static bool IsPersonAUser(int PersonID)
        {
            return clsUserData.IsPersonAUser(PersonID);
        }
    }
}
