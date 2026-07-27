using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicenseClass
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public short MinimumAllowedAge { get; set; }
        public short DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        private clsLicenseClass
           (int LicenseClassID,  string ClassName,  string ClassDescription,
             short MinimumAllowedAge,  short DefaultValidityLength,  float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        public static DataTable GetAll()
        {
            return clsLicenseClassData.GetAll();
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {

            string ClassName = "";
            string ClassDescription = "";
            short MinimumAllowedAge = -1;
            short DefaultValidityLength = -1;
            float ClassFees = -1;


            if (clsLicenseClassData.FindByID( LicenseClassID, ref  ClassName, ref  ClassDescription,
            ref  MinimumAllowedAge, ref  DefaultValidityLength, ref  ClassFees))
            {
                return new clsLicenseClass(LicenseClassID,  ClassName,  ClassDescription,
             MinimumAllowedAge,  DefaultValidityLength,  ClassFees);
            }
            else
            {
                return null;
            }
        }

        public static clsLicenseClass Find(string ClassName)
        {

            int LicenseClassID = -1;
            string ClassDescription = "";
            short MinimumAllowedAge = -1;
            short DefaultValidityLength = -1;
            float ClassFees = -1;


            if (clsLicenseClassData.FindByClassName(ClassName, ref LicenseClassID, ref ClassDescription,
            ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
             MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }


        private bool _Update()
        {
            return clsLicenseClassData.Update(LicenseClassID, ClassName, ClassDescription,
             MinimumAllowedAge, DefaultValidityLength, ClassFees);
        }

        public bool Save()
        {
            return _Update();
        }
    }
}
