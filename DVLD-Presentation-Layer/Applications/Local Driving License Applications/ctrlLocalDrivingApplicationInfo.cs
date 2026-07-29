using DVLDBusinessLayer;
using DVLD.Applications.Controls;
using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class ctrlLocalDrivingApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LDLApp;

        private int _LDLAppID;

        public void LoadLDLAppInfo(int LocalDrivingLicenseApplicationID)
        {
            _LDLAppID = LocalDrivingLicenseApplicationID;

            _LDLApp = clsLocalDrivingLicenseApplication.Find(_LDLAppID);

            if (_LDLApp != null)
            {

                lblDLAppID.Text = _LDLAppID.ToString();
                lblPassedTests.Text = clsLocalDrivingLicenseApplication.
                GetPassedTests(_LDLAppID).ToString() + "/3";
                lblLicenseClass.Text = _LDLApp.LicenseClassInfo.ClassName;
                ctrlApplicationInfo1.LoadAppBasicInfo(_LDLApp.ApplicationID);
            }
        }


        public ctrlLocalDrivingApplicationInfo()
        {
            InitializeComponent();
        }


    }
}
