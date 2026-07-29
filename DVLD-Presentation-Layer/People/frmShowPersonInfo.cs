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
using static System.Net.Mime.MediaTypeNames;

namespace MySolution.People
{
    public partial class frmShowPersonInfo : Form
    {
        int _PersonID;
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();


            _PersonID = PersonID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {
            ctrlPersonCard1.LoadPersonInfo(_PersonID);
        }
    }
}
