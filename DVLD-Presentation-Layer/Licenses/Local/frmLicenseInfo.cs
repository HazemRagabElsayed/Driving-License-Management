using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Licenses
{
    public partial class frmLicenseInfo : Form
    {

        //string _NationalNo = "";
        int _LicenseID = -1;

        //public frmLicenseInfo(string NationalNo)
        //{
        //    InitializeComponent();
        //    _NationalNo = NationalNo;

        //}

        public frmLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;

        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            if(_LicenseID != -1)
            {
                ctrlLicenseInfo1.LoadLicenseInfo(_LicenseID);
                return;
            }
            
            //if(_NationalNo.Trim() != "")
            //{
            //    ctrlLicenseInfo1.LoadLicenseInfo(_NationalNo);
            //}

        }
    }
}
