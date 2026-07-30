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
    public partial class frmListUsers : Form
    {

        static DataTable _dtAllUsers = clsUser.GetAll();

        static DataTable _dtUsers = _dtAllUsers.DefaultView.ToTable
            (false, "UserID", "PersonID", "FullName", "UserName", "IsActive");

        enum enSelectedFilterIndex { None = 0, UserID = 1, PersonID = 2,
        FullName = 3, UserName = 4, IsActive = 5 };

        enum enSelectedIsActiveIndex
        {
            All = 0, Yes = 1, No = 2
        };

        enum enTableColumnIndex
        {
            UserID = 0,
            PersonID = 1,
            FullName = 2,
            UserName = 3,
            IsActive = 4
        };

        public frmListUsers()
        {
            InitializeComponent();
        }

        void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAll();
            _dtUsers = _dtAllUsers.DefaultView.ToTable
                (false, "UserID", "PersonID", "FullName", "UserName", "IsActive");
            dgvUsersList.DataSource = _dtUsers;
            lblRecordsNumber.Text = dgvUsersList.Rows.Count.ToString();

        }

        void _LoadUsersData()
        {
            dgvUsersList.DataSource = _dtUsers;
            lblRecordsNumber.Text = _dtUsers.Rows.Count.ToString();

            if (dgvUsersList.Rows.Count > 0)
            {
                dgvUsersList.Columns[(short)enTableColumnIndex.UserID].HeaderText = "User ID";
                dgvUsersList.Columns[(short)enTableColumnIndex.UserID].Width = 80;

                dgvUsersList.Columns[(short)enTableColumnIndex.PersonID].HeaderText = "Person ID";
                dgvUsersList.Columns[(short)enTableColumnIndex.PersonID].Width = 80;

                dgvUsersList.Columns[(short)enTableColumnIndex.FullName].HeaderText = "Full Name";
                dgvUsersList.Columns[(short)enTableColumnIndex.FullName].Width = 300;

                dgvUsersList.Columns[(short)enTableColumnIndex.UserName].HeaderText = "User Name";
                dgvUsersList.Columns[(short)enTableColumnIndex.UserName].Width = 100;

                dgvUsersList.Columns[(short)enTableColumnIndex.IsActive].HeaderText = "Is Active";
                dgvUsersList.Columns[(short)enTableColumnIndex.IsActive].Width = 100;
            }
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = (int)enSelectedFilterIndex.None;
            cbIsActive.SelectedIndex = (int)enSelectedIsActiveIndex.All;
            _LoadUsersData();
        }

        private void dgvUsersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo((int)dgvUsersList.CurrentRow.Cells[0].Value);
            frmUserInfo.Show();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbIsActive.Visible = (cbFilter.SelectedIndex == (int)enSelectedFilterIndex.IsActive);
            txtFilter.Visible = (
                !cbIsActive.Visible &&
                cbFilter.SelectedIndex != (int)enSelectedFilterIndex.None
                );
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActive.SelectedIndex)
            {
                case (int)enSelectedIsActiveIndex.All:
                    _dtUsers.DefaultView.RowFilter = "";
                    break;
                case (int)enSelectedIsActiveIndex.Yes:
                    _dtUsers.DefaultView.RowFilter = 
                                                    string.Format("IsActive = 1");
                    break;
                case (int)enSelectedIsActiveIndex.No:
                    _dtUsers.DefaultView.RowFilter = 
                                                    string.Format("IsActive = 0");
                    break;
            }
            lblRecordsNumber.Text = _dtUsers.Rows.Count.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            if (cbFilter.SelectedIndex == (int)enSelectedFilterIndex.IsActive)
            {
                return;
            }

            if (txtFilter.Text.Trim() == "" || 
                cbFilter.SelectedIndex == (int)enSelectedFilterIndex.None)
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = _dtUsers.Rows.Count.ToString();
                return;
            }

            string ColumnFilter;

            switch (cbFilter.SelectedIndex)
            {
                case (int)enSelectedFilterIndex.UserID:
                    ColumnFilter = "UserID";
                    break;
                case (int)enSelectedFilterIndex.PersonID:
                    ColumnFilter = "PersonID";
                    break;
                case (int)enSelectedFilterIndex.FullName:
                    ColumnFilter = "FullName";
                    break;
                case (int)enSelectedFilterIndex.UserName:
                    ColumnFilter = "UserName";
                    break;
                case (int)enSelectedFilterIndex.IsActive:
                    ColumnFilter = "IsActive";
                    break;
                default:
                    return;
            }

            string FilteringProcess = "";

            if(cbFilter.SelectedIndex == (int)enSelectedFilterIndex.UserID
                || cbFilter.SelectedIndex == (int)enSelectedFilterIndex.PersonID)
            {
                FilteringProcess = "{0} = {1}";
            }
            else
            {
                FilteringProcess = "{0} Like '{1}%'";
            }

            _dtUsers.DefaultView.RowFilter =
                    string.Format(FilteringProcess
                    , ColumnFilter, txtFilter.Text.Trim());

            lblRecordsNumber.Text = _dtUsers.Rows.Count.ToString();


        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmUserInfo frmUserInfo = new frmUserInfo((int)dgvUsersList.CurrentRow.Cells[(short)enTableColumnIndex.UserID].Value);
            frmUserInfo.Show();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmAddEditUserInfo frmAddEditUserInfo = new frmAddEditUserInfo();
            frmAddEditUserInfo.Show();
            _RefreshUsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmAddEditUserInfo frmAddEditUserInfo = new frmAddEditUserInfo(
                (int)dgvUsersList.CurrentRow.Cells[(short)enTableColumnIndex.UserID].Value);
            frmAddEditUserInfo.Show();
            _RefreshUsersList();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmChangePassword frmChangePassword = new frmChangePassword(
                (int)dgvUsersList.CurrentRow.Cells[(short)enTableColumnIndex.UserID].Value);
            frmChangePassword.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(dgvUsersList.CurrentRow.Cells[(short)enTableColumnIndex.UserID].Value);

            if (MessageBox.Show($"Are you sure you want to delete User" +
                $"  [{UserID}]"
                , "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsUser.Delete(UserID))
                {
                    //DeleteImage(clsUser.Find(UserID).ImagePath);
                    //_RefreshPeopleList();
                    MessageBox.Show("User Deleted Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUsersList();
                }
                else
                {

                    MessageBox.Show("User was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void pbAddNewUser_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmAddEditUserInfo frmAddEditUserInfo = new frmAddEditUserInfo();
            frmAddEditUserInfo.Show();

            _RefreshUsersList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {      if (cbFilter.SelectedIndex == (int)enSelectedFilterIndex.UserID
                || cbFilter.SelectedIndex == (int)enSelectedFilterIndex.PersonID)
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }

        }

        private void pbAddNewUser_MouseEnter(object sender, EventArgs e)
        {
            pbAddNewUser.BackColor = SystemColors.ControlDark;

        }

        private void pbAddNewUser_MouseLeave(object sender, EventArgs e)
        {
            pbAddNewUser.BackColor = PictureBox.DefaultBackColor;

        }
    }
}
