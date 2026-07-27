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

namespace MySolution.Users
{
    public partial class frmAddEditUserInfo : Form
    {

        clsUser _User;
        int _UserID;
        enum enMode { AddNew, Update };
        enMode _Mode;

        public frmAddEditUserInfo()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;
        }

        enum enTabControlIndex { PersonalInfo = 0, LoginInfo = 1 };

        void _DisableFilter()
        {
            ctrlPersonCardWithFilter1.EnableFilter = false;
        }
        void _EnableFilter()
        {
            ctrlPersonCardWithFilter1.EnableFilter = true;
        }

        void _DisableLoginInfo()
        {
            lblUserID.Enabled = false;
            txtUserName.Enabled = false;
            txtPassword.Enabled = false;
            txtConfirmPassword.Enabled = false;
            chkIsActive.Enabled = false;
            btnSave.Enabled = false;

        }

        void _EnableLoginInfo()
        {
            lblUserID.Enabled = true;
            txtUserName.Enabled = true;
            txtPassword.Enabled = true;
            txtConfirmPassword.Enabled = true;
            chkIsActive.Enabled = true;
            btnSave.Enabled = true;
        }

        void _FillUserWithData()
        {
            _User.PersonID = ctrlPersonCardWithFilter1.Person.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;
        }

        void _FillFormWithUserData()
        {
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
        }

        private void frmAddEditUserInfo_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                lblActionTitle.Text = "Add New User";
                _EnableFilter();
                _DisableLoginInfo();
                _User = new clsUser();
                

            }
            else
            {
                lblActionTitle.Text = "Update User";
                _EnableLoginInfo();
                _DisableFilter();
                _User = clsUser.Find(_UserID);

                if (_User != null)
                {
                    ctrlPersonCardWithFilter1.LoadPersonData(_User.PersonID);
                }

                if (ctrlPersonCardWithFilter1.Person != null)
                {

                    _FillFormWithUserData();

                }


            }

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                epUserNameValidation.SetError(txtUserName,
                    "UserName cannot be blank");
            }
            else if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsExist(txtUserName.Text))
                {
                    epUserNameValidation.SetError(txtUserName,
                        "UserName is used by another user");
                }
            }
            else
            {
                epUserNameValidation.Clear();
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
            {
                epPasswordValidation.SetError(txtPassword,
                    "Password cannot be blank");
            }
            else
            {
                epPasswordValidation.Clear();
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtPassword.Text != txtConfirmPassword.Text)
            {
                epConfirmPasswordValidation.SetError(txtConfirmPassword,
                    "Password Confirmation doesn't match Password");
            }
            else
            {
                epConfirmPasswordValidation.Clear();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNew)
            {
                if (ctrlPersonCardWithFilter1.Person != null)
                {
                    if (clsUser.IsPersonAUser(ctrlPersonCardWithFilter1.Person.PersonID))
                    {
                        MessageBox.Show("Selected Person already has a user," +
                            " choose another one.", "Selected another person"
                            , buttons: default, icon: MessageBoxIcon.Error);

                        _DisableLoginInfo();

                    }
                    else
                    {
                        _EnableLoginInfo();
                        tbAddEditUser.SelectedIndex = (int)enTabControlIndex.LoginInfo;

                    }

                }
                else
                {
                    MessageBox.Show("Please select a person", "Select a Person",
                         buttons: default, icon: MessageBoxIcon.Error);
                    _DisableLoginInfo();
                }
            }
            else
            {
                tbAddEditUser.SelectedIndex = (int)enTabControlIndex.LoginInfo;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(epUserNameValidation.GetError(txtUserName) != "" ||
               epPasswordValidation.GetError(txtPassword) != "" ||
               epConfirmPasswordValidation.GetError(txtConfirmPassword) != "")
            {

                MessageBox.Show("Some fields are not valid!" +
                    ", put the mouse over the red icon(s) to see the error", "Validation Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserWithData();

            if (_User.Save())
            {
                if (_Mode == enMode.AddNew)
                {
                    lblActionTitle.Text = "Update User";
                    lblUserID.Text = _User.UserID.ToString();
                    _DisableFilter();
                }

                MessageBox.Show("Data saved successfully", "Saved", buttons: default,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Some fields are not valid!" +
                    ", put the mouse over the red icon(s) to see the error", "Validation Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
