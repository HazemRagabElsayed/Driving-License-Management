using DVLDBusinessLayer;
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
    public partial class frmChangePassword : Form
    {

        int _UserID;

        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        void ValidateCurrentPassword()
        {
            if (txtCurrentPassword.Text != _User.Password)
            {
                epCurrentPasswordValidation.SetError(txtCurrentPassword, "Current password is wrong!");
            }
            else
            {
                epCurrentPasswordValidation.Clear();
            }
        }
        void ValidateNewPassword()
        {
            if (txtNewPassword.Text == "")
            {
                epNewPasswordValidation.SetError(txtNewPassword, "New Password cannot be blank");
            }
            else
            {
                epNewPasswordValidation.Clear();
            }
        }
        void ValidateConfirmPassword()
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                epConfirmPasswordValidation.SetError(txtConfirmPassword, "Password Confirmation doesn't match New Password!");
            }
            else
            {
                epConfirmPasswordValidation.Clear();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (_User != null)
            {
                if (txtCurrentPassword.Text == _User.Password
                    && txtNewPassword.Text != ""
                    && txtNewPassword.Text == txtConfirmPassword.Text)
                {
                    _User.Password = txtNewPassword.Text;
                    if (_User.Save())
                    {
                        MessageBox.Show("Password Saved Successfully",
                    "Saved"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Password is not saved , an error occured",
                    "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                        
                }
                else
                {
                    ValidateCurrentPassword();
                    MessageBox.Show("Some fields are not valid!" +
                    ", put the mouse over the red icon(s) to see the error", "Validation Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            ValidateCurrentPassword();
        }
        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            ValidateNewPassword();
        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            ValidateConfirmPassword();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.Find(_UserID);
            if (_User != null)
            {
                ctrlUserCard1._LoadUserInfo(_UserID);
            }
            else
            {
                //MessageBox
                //Close();
            }
        }
    }
}
