using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.People
{
    public partial class frmListPeople : Form
    {



        static private DataTable _dtAllPeople = clsPerson.GetAll();
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable
            (false,"PersonID","NationalNo","FirstName","SecondName"
            ,"ThirdName","LastName", "GenderCaption", "DateOfBirth","CountryName","Phone","Email");

        public frmListPeople()
        {
            InitializeComponent();
        }
        
        enum enSelectedItem {None = 0 , PersonID = 1, FirstName = 1,
            SecondName = 2, ThirdName = 4, LastName = 5, Nationality = 6,
            Gender = 7, Phone = 8, Email = 9
        };

        private void _LoadPeopleData()
        {

            dgvPeopleList.DataSource = _dtPeople;
            lblRecordsNumber.Text = dgvPeopleList.Rows.Count.ToString();

            if (dgvPeopleList.Rows.Count > 0)
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                //dgvPeopleList.Columns[0].Width = 100;

                dgvPeopleList.Columns[1].HeaderText = "National No.";
                //dgvPeopleList.Columns[1].Width = 100;

                dgvPeopleList.Columns[2].HeaderText = "First Name";
                //dgvPeopleList.Columns[2].Width = 100;

                dgvPeopleList.Columns[3].HeaderText = "Second Name";
                //dgvPeopleList.Columns[3].Width = 100;

                dgvPeopleList.Columns[4].HeaderText = "Third Name";
                //dgvPeopleList.Columns[4].Width = 100;

                dgvPeopleList.Columns[5].HeaderText = "Last Name";
                //dgvPeopleList.Columns[4].Width = 100;

                dgvPeopleList.Columns[6].HeaderText = "Gender";
                //dgvPeopleList.Columns[6].Width = 100;

                dgvPeopleList.Columns[7].HeaderText = "Date Of Birth";
                //dgvPeopleList.Columns[7].Width = 100;

                dgvPeopleList.Columns[8].HeaderText = "Nationality";
                //dgvPeopleList.Columns[8].Width = 100;

                dgvPeopleList.Columns[9].HeaderText = "Phone";
                //dgvPeopleList.Columns[9].Width = 100;

                dgvPeopleList.Columns[10].HeaderText = "Email";
                //dgvPeopleList.Columns[10].Width = 100;

            }

        }

        void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAll();
            _dtPeople = _dtAllPeople.DefaultView.ToTable
            (false, "PersonID", "NationalNo", "FirstName", "SecondName"
            , "ThirdName", "LastName", "GenderCaption", "DateOfBirth", "CountryName", "Phone", "Email");

            dgvPeopleList.DataSource = _dtPeople;
            lblRecordsNumber.Text = dgvPeopleList.Rows.Count.ToString();
        }
        
        private void frmManagePeople_Load(object sender, EventArgs e)
        {

            _LoadPeopleData();
            cbFilter.SelectedIndex = (int)enSelectedItem.None;
        }

        private void dgvPeopleList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmShowPersonInfo frmPersonDetails = new frmShowPersonInfo(Convert.ToInt16(dgvPeopleList.CurrentRow.Cells[0].Value));
            frmPersonDetails.ShowDialog();
            _RefreshPeopleList();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(txtFilter.Visible = (cbFilter.SelectedIndex != Convert.ToInt16(enSelectedItem.None)))
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            if(cbFilter.SelectedIndex == (short)enSelectedItem.None
                || txtFilter.Text.Trim() == "")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvPeopleList.Rows.Count.ToString();
                return;
            }

            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name" :
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;
                case "Date Of Birth":
                    FilterColumn = "DateOfBirth";
                    break;
                case "Nationality":
                    FilterColumn = "CountryName";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                    
            }


            if (cbFilter.SelectedIndex == (int)enSelectedItem.PersonID)
            {
                _dtPeople.DefaultView.RowFilter = 
                    string.Format($"{FilterColumn} = {txtFilter.Text.Trim()}");
            }
            else
            {
               
                _dtPeople.DefaultView.RowFilter = 
                    string.Format($"{FilterColumn} Like '{txtFilter.Text.Trim()}%'");
            }
            lblRecordsNumber.Text = dgvPeopleList.Rows.Count.ToString();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frmPersonDetails = new frmShowPersonInfo(Convert.ToInt16(dgvPeopleList.CurrentRow.Cells[0].Value));
            frmPersonDetails.ShowDialog();
            _RefreshPeopleList();
        }

        private void pbAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddNewPerson = new frmAddEditPersonInfo();
            frmAddNewPerson.ShowDialog();
            _RefreshPeopleList();
        }
        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddNewPerson = new frmAddEditPersonInfo();
            frmAddNewPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmEditPerson = new frmAddEditPersonInfo
                (Convert.ToInt16(dgvPeopleList.CurrentRow.Cells[0].Value));
            frmEditPerson.ShowDialog();
            _RefreshPeopleList();
        }


        private void DeleteImage(string Image)
        {
            if (!string.IsNullOrEmpty(Image))
            {

                File.Delete(Image);
            }
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt16(dgvPeopleList.CurrentRow.Cells[0].Value);

            if(MessageBox.Show($"Are you sure you want to delete Person" +
                $"  [{PersonID}]"
                ,"Confirm Delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.Delete(Convert.ToInt16(PersonID)))
                {
                    DeleteImage(clsPerson.Find(PersonID).ImagePath);
                    _RefreshPeopleList();
                    MessageBox.Show("Person Deleted Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
                else
                {

                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.SelectedIndex == (int)enSelectedItem.PersonID)
            {
                    e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbAddNewPerson_MouseEnter(object sender, EventArgs e)
        {
            pbAddNewPerson.BackColor = SystemColors.ControlDark;
        }

        private void pbAddNewPerson_MouseLeave(object sender, EventArgs e)
        {
            pbAddNewPerson.BackColor = PictureBox.DefaultBackColor;
        }


    }


}
