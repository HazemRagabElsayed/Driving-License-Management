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

namespace DVLD.Licenses
{
    public partial class frmLicenseHistory : Form
    {
        int _PersonID = -1;

        public frmLicenseHistory( )
        {
            InitializeComponent();
        }

        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }



        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {

            if( _PersonID != -1 )
            {
                ctrlPersonCardWithFilter1.LoadPersonData(_PersonID);
                ctrlPersonCardWithFilter1.EnableFilter = false;
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.EnableFilter = true;
                ctrlPersonCardWithFilter1.FilterFocus();

            }



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
