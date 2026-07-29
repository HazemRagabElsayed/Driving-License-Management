using DVLDBusinessLayer;
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

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        int _AppID;

        clsApplication _App;

        void _ResetAppInfo()
        {
            lblAppID.Text = "[???]";
            lblAppStatus.Text = "[???]";
            lblAppFees.Text = "[$$$]";
            lblAppType.Text = "[???]";
            lblApplicantName.Text = "[???]";
            lblAppDate.Text = "[??/??/????]";
            lblAppStatusDate.Text = "[??/??/????]";
            lblCreatedBy.Text = "[???]";
        }

        public void LoadAppBasicInfo(int AppID)
        {
            _AppID = AppID;
            _App = clsApplication.Find(AppID);
            if (_App == null)
            {
                
                MessageBox.Show($"Application with ID {_AppID} Is Not Found", "Warning"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _ResetAppInfo();
                return;
            }

            lblAppID.Text = AppID.ToString();
            lblAppStatus.Text = _App.ApplicationStatus.ToString();
            lblAppFees.Text = _App.PaidFees.ToString();
            lblAppType.Text = _App.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicantName.Text = _App.ApplicantPersonInfo.FullName;
            lblAppDate.Text = _App.ApplicationDate.ToShortDateString();
            lblAppStatusDate.Text = _App.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = _App.CreatedByUserInfo.UserName;


        }


        private void llViewPersonInfo_LinkClicked(object sender, EventArgs e)
        {
            frmShowPersonInfo frmShowPersonInfo  = new frmShowPersonInfo(_App.ApplicantPersonID);
            frmShowPersonInfo.ShowDialog();
            LoadAppBasicInfo(_AppID);
        }
    }
}
