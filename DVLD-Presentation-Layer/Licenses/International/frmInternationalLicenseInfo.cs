using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International
{
    public partial class frmInternationalLicenseInfo : Form
    {
        int _InternationalLicenseID = -1;

        public frmInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            if(_InternationalLicenseID != -1)
            {
                ctrlInternationalDriverLicenseInfo1.LoadInternationalLicenseInfo(_InternationalLicenseID);
            }
        }
    }
}
