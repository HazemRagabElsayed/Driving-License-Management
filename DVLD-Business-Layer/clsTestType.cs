using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestType
    {

        public enum enTestType { Vision = 1, Written = 2, Street  = 3 };

        public clsTestType.enTestType TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        private clsTestType
           (enTestType TestTypeID, string TestTypeTitle,
            string TestTypeDescription, float TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        public static DataTable GetAll()
        {
            return clsTestTypeData.GetAll();
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {

            string TestTypeTitle = "";
            string TestTypeDescription = "";
            float TestTypeFees = -1;

            if (clsTestTypeData.FindByID((int)TestTypeID ,ref TestTypeTitle ,
                ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestType(TestTypeID , TestTypeTitle,
                    TestTypeDescription,TestTypeFees);
            }
            else
            {
                return null;
            }
        }


        private bool _Update()
        {
            return clsTestTypeData.Update((int)TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }

        public bool Save()
        {
            return _Update();
        }
    }
}
