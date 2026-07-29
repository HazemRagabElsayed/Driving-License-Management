using DVLDBusinessLayer;
using MySolution.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {



        clsPerson _Person;

        int _PersonID = -1;

        enum enPersonDoes { NotExist = -1};

        public bool EnableFilter
        {
            set { DisableOrEnableFilter(value); }
        }

        void DisableOrEnableFilter(bool IsEnabled)
        {
            gbFilter.Enabled = IsEnabled;
            _ResetDefaultValues();


        }

        public void FilterFocus()
        {
            txtFilter.Focus();
        }

        public void SelectedFilterIndex(int SelectedIndex)
        {
            cbFilter.SelectedIndex = (int)SelectedIndex;

        }

        public clsPerson Person { 
            get { return _Person; } }
        


        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        enum enSelectedIndex { PersonID = 0, NationalNo = 1 };
        void _ResetDefaultValues()
        {
            cbFilter.SelectedIndex = (int)enSelectedIndex.PersonID;

        }

        public void LoadPersonData(int PersonID)
        {
            _PersonID = PersonID;

            if (_PersonID != -1)
            {
                cbFilter.SelectedIndex = (int)enSelectedIndex.PersonID;
                ctrlPersonCard1.LoadPersonInfo(_PersonID);
                txtFilter.Text = _PersonID.ToString();
                _Person = clsPerson.Find(_PersonID);

            }
        }

        void _LoadPersonData()
        {
            if (_PersonID != -1)
            {
                ctrlPersonCard1.LoadPersonInfo( _PersonID);
                txtFilter.Text = _PersonID.ToString();
                _Person = clsPerson.Find(_PersonID);

            }
        }

        private void pbAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddEditPersonInfo = new frmAddEditPersonInfo();
            frmAddEditPersonInfo.DataBack += GetPersonIDFromAddNewPersonForm; // Subscribe to the event
            frmAddEditPersonInfo.Show();
        }

        private void GetPersonIDFromAddNewPersonForm(object sender, int PersonID)
        {
            // Handle the data received from Form2
            _PersonID = PersonID;
            _LoadPersonData();


        }

        private void pbSearchPerson_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (txtFilter.Text.Trim() == "")
            {
                MessageBox.Show("you cannot search Person with empty field of " +
                    $"{cbFilter.Text}","Error",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            switch (cbFilter.SelectedIndex)
            {
                case (int)enSelectedIndex.NationalNo:
                    _Person = clsPerson.Find(txtFilter.Text);
                    break;
                case (int)enSelectedIndex.PersonID:
                    _Person = clsPerson.Find(Convert.ToInt32(txtFilter.Text));
                    break;
            }

            if (_Person == null)
            {
                MessageBox.Show(
                    $"No Person With {cbFilter.Text} = {txtFilter.Text}"
                    , "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);

                ctrlPersonCard1.PersonID = (int)enPersonDoes.NotExist;
            }
            else
            {
                ctrlPersonCard1.LoadPersonInfo(_Person.PersonID);
            }

        }

        private void pbSearchPerson_MouseEnter(object sender, EventArgs e)
        {
            pbSearchPerson.BackColor = SystemColors.ControlDark;
        }

        private void pbSearchPerson_MouseLeave(object sender, EventArgs e)
        {
            pbSearchPerson.BackColor = PictureBox.DefaultBackColor;
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
