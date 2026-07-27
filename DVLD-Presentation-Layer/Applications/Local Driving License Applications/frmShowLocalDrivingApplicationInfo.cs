using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Applications
{
    public partial class frmShowLocalDrivingApplicationInfo : Form
    {

        int _LocalDrvingLicenseApplicationID;

        public frmShowLocalDrivingApplicationInfo(int _LocalDrvingLicenseApplicationID)
        {
            InitializeComponent();
            this._LocalDrvingLicenseApplicationID = _LocalDrvingLicenseApplicationID;
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmShowLocalDrivingApplicationInfo_Load(object sender, EventArgs e)
        {
            if(clsLocalDrivingLicenseApplication.IsExist(_LocalDrvingLicenseApplicationID))
            {
                ctrlLocalDrivingApplicationInfo1.LoadLDLAppInfo(_LocalDrvingLicenseApplicationID);
            }
            else
            {
                MessageBox.Show($"LDApp with ID {_LocalDrvingLicenseApplicationID} doesn't exist",
                    "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Close();
            }
            
        }
    }
}
