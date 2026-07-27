using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsPerson
    {

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public int PersonID { get; set; }
        public string NationalNo { get; set;}
        public string FirstName { get; set;}
        public string SecondName { get; set;}
        public string ThirdName { get; set;}
        public string LastName { get; set;}

        public string FullName { get
            {
                return FirstName + " " + SecondName + " " + (string.IsNullOrEmpty(ThirdName) ? "" : (ThirdName + " ")) + LastName;
            }
        }
        public DateTime DateOfBirth { get; set;}
        public short Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public int NationalityCountryID { get; set; }
        public clsCountry Country { get; set; }

        public clsPerson()
        {
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gender = 0;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";
            _Mode = enMode.AddNew;
        }

        private clsPerson
            (int PersonID, string NationalNo, string FirstName, string SecondName,string ThirdName,
            string LastName, DateTime DateOfBirth,short Gender,string Address,
            string Phone, string Email, int NationalCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalCountryID;
            this.Country = clsCountry.Find(NationalityCountryID);
            this.ImagePath = ImagePath;
            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {

            this.PersonID = clsPersonData.AddNew(this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
                this.Address, this.Phone, this.Email, this.NationalityCountryID,
                this.ImagePath);

            return (this.PersonID != -1);
        }
        private bool _Update()
        {
            return clsPersonData.Update(this.PersonID, this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
                this.Address, this.Phone, this.Email, NationalityCountryID,
                this.ImagePath);
        }

        public static clsPerson Find(int PersonID)
        {

           string NationalNo = "";
           string FirstName = "";
           string SecondName = "";
           string ThirdName = "";
           string LastName = "";
           DateTime DateOfBirth = DateTime.Now;
           short Gender = 0;
           string Address = "";
           string Phone = "";
           string Email = "";
           int NationalCountryID = -1;
           string ImagePath = "";

            if (clsPersonData.FindByID(PersonID,ref NationalNo,ref FirstName,ref SecondName,
                ref ThirdName,ref LastName,ref DateOfBirth,ref Gender,ref Address,ref Phone,ref Email,
                ref NationalCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID,NationalNo, FirstName, SecondName,
                 ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email,
                 NationalCountryID, ImagePath);
            }
            else
            {
                return null;
            }    
        }

        public static clsPerson Find(string NationalNo)
        {

            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalCountryID = -1; ;
            string ImagePath = "";

            if (clsPersonData.FindByNationalNo(NationalNo,ref PersonID , ref FirstName, ref SecondName,
                ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email,
                ref NationalCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName,
                 ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email,
                 NationalCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAll()
        {
            return clsPersonData.GetAll();
        }

        //public static DataTable GetAllWhere(int PersonID = -1, string NationalNo = "")
        //{
        //    return clsPersonData.GetAll(PersonID, NationalNo);
        //}

        //public static DataTable GetAll(string Filter = "", string Value = "")
        //{
        //    return clsPersonData.GetAll(Filter, Value);
        //}

        public static bool IsExist(int PersonID)
        {
            return clsPersonData.IsExist(PersonID);
        }

        public static bool IsExist(string NationalNo)
        {
            return clsPersonData.IsExist(NationalNo);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if(_AddNew())
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



        public static bool Delete(int PersonID)
        {
            //if (IsLinkedToOtherData(PersonID))
            //    return false;

            return clsPersonData.Delete(PersonID);
        }

        public static short GetAge(int PersonID)
        {
            return clsPersonData.GetAge(PersonID);
        }

        //public static bool IsLinkedToOtherData(int PersonID)
        //{
        //    return clsPersonData.IsLinkedToOtherData(PersonID);
        //}
    }
}
