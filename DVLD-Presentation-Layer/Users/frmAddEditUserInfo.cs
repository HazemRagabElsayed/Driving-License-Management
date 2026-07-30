using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
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

        private bool _EnableFilter
        {
            set { ctrlPersonCardWithFilter1.EnableFilter = value; }

        }
        private bool _EnableLoginInfo
        {
            set {
                lblUserID.Enabled = value;
                txtUserName.Enabled = value;
                txtPassword.Enabled = value;
                txtConfirmPassword.Enabled = value;
                chkIsActive.Enabled = value;
                btnSave.Enabled = value;
            }

        }
        void _FillUserObject()
        {
            _User.PersonID = ctrlPersonCardWithFilter1.Person.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;
        }

        void _FillUserForm()
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
                _EnableFilter = true;
                _EnableLoginInfo = false;

                _User = new clsUser();
                

            }
            else
            {
                lblActionTitle.Text = "Update User";
                _EnableLoginInfo = true;
                _EnableFilter = false;
                _User = clsUser.Find(_UserID);

                if (_User != null)
                {
                    ctrlPersonCardWithFilter1.LoadPersonData(_User.PersonID);
                }

                if (ctrlPersonCardWithFilter1.Person != null)
                {

                    _FillUserForm();

                }


            }

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                e.Cancel = true;
                epUserNameValidation.SetError(txtUserName,
                    "UserName cannot be blank");
                return;
            }
             if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsExist(txtUserName.Text))
                {
                    e.Cancel = true;
                    epUserNameValidation.SetError(txtUserName,
                        "UserName is used by another user");
                    return;
                }
            }
            epUserNameValidation.SetError(txtUserName,null);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
            {
                e.Cancel = true;
                epPasswordValidation.SetError(txtPassword,
                    "Password cannot be blank");
                return;
            }
            epPasswordValidation.SetError(txtPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                epConfirmPasswordValidation.SetError(txtConfirmPassword,
                    "Password Confirmation doesn't match Password");
                return;
            }
            epConfirmPasswordValidation.SetError(txtConfirmPassword, null);
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

                        _EnableLoginInfo = false;
                        return;

                    }

                    _EnableLoginInfo = true;
                    tbAddEditUser.SelectedIndex = (int)enTabControlIndex.LoginInfo;
                    return;
                }
                MessageBox.Show("Please select a person", "Select a Person",
                         buttons: default, icon: MessageBoxIcon.Error);
                _EnableLoginInfo = false;
                return;
            }
            
            tbAddEditUser.SelectedIndex = (int)enTabControlIndex.LoginInfo;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {

                MessageBox.Show("Some fields are not valid!" +
                    ", put the mouse over the red icon(s) to see the error", "Validation Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserObject();

            if (_User.Save())
            {
                if (_Mode == enMode.AddNew)
                {
                    lblActionTitle.Text = "Update User";
                    lblUserID.Text = _User.UserID.ToString();
                    _EnableFilter = false;
                }

                MessageBox.Show("Data saved successfully", "Saved", buttons: default,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data is not saved successfully", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
