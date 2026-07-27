using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLDBusinessLayer
{
    public class clsApplicationType
    {

        public enum enAppType
        {
            NewLDLService = 1, RenewDLService = 2,
            ReplacementLostDL = 3, ReplacementDamagedDL = 4,
            ReleaseDetainedDL = 5, NewInternationalL = 6,
            RetakeTest = 7

        }

        public enAppType ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

        private clsApplicationType
           (enAppType ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }

        public static DataTable GetAll()
        {
            return clsApplicationTypeData.GetAll();
        }

        public static clsApplicationType Find(enAppType ApplicationTypeID)
        {

            string ApplicationTypeTitle = "";
            float ApplicationFees = -1;
  
            if (clsApplicationTypeData.FindByID((int)ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }

        public static clsApplicationType Find(string ApplicationTypeTitle)
        {

            int ApplicationTypeID = -1;
            float ApplicationFees = -1;

            if (clsApplicationTypeData.FindByApplicationTypeTitle(ApplicationTypeTitle, ref ApplicationTypeID, ref ApplicationFees))
            {
                return new clsApplicationType((enAppType) ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }


        private bool _Update()
        {
            return clsApplicationTypeData.Update((int)ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
        }

        public bool Save()
        {
            return _Update();
        }


    }
}
