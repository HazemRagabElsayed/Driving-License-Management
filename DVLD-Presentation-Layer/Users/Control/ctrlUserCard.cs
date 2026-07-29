using DVLDBusinessLayer;
using MySolution.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Users.Control
{
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        clsUser _User;

        int _UserID = -1;

        public void _LoadUserInfo(int UserID)
        {

            _User = clsUser.Find(UserID);
            if (_User != null)
            {

                lblUserID.Text = _User.UserID.ToString();
                lblUserName.Text = _User.UserName.ToString();
                if (_User.IsActive)
                {
                    lblIsActive.Text = "Yes";
                }
                else
                {
                    lblIsActive.Text = "No";
                }

                ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            }
        }

        private void ctrlUserCard_Load(object sender, EventArgs e)
        {
            _LoadUserInfo(_UserID);

        }
    }
}
