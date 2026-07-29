using DVLD.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmUserInfo : Form
    {
        int _UserID;

        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser.UserID == _UserID)
            {
                lblTitle.Text = "Current User Info";
            }
            else
            {
                lblTitle.Text = "Selected User Info";
            }
                ctrlUserCard1._LoadUserInfo(_UserID);
        }
    }
}
