using DVLDBusinessLayer;
using DVLD.Applications;
using DVLD.Licenses.International;
using DVLD.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        int _DriverID;
        clsDriver _Driver;
        private DataTable _dtLocalLicenses;
        private DataTable _dtInternationalLicenses;

        private void _LoadLocalLicensesData()
        {
            _dtLocalLicenses = clsDriver.GetDriverLicenses(_DriverID);
            dgvLocalLicensesList.DataSource = _dtLocalLicenses;
            lblLocalRecordsNumber.Text = dgvLocalLicensesList.Rows.Count.ToString();

            if (dgvLocalLicensesList.Rows.Count > 0)
            {
                dgvLocalLicensesList.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesList.Columns[0].Width = 100;

                dgvLocalLicensesList.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesList.Columns[1].Width = 100;

                dgvLocalLicensesList.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesList.Columns[2].Width = 250;

                dgvLocalLicensesList.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesList.Columns[3].Width = 200;

                dgvLocalLicensesList.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesList.Columns[4].Width = 200;

                dgvLocalLicensesList.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesList.Columns[5].Width = 100;

            }

        }
        private void _LoadInternationalLicensesData()
        {
            _dtInternationalLicenses = clsInternationalLicense.GetDriverLicenses(_DriverID);
            dgvInternationalLicensesList.DataSource = _dtInternationalLicenses;
            lblInternationalRecordsNumber.Text = dgvInternationalLicensesList.Rows.Count.ToString();

            if (dgvInternationalLicensesList.Rows.Count > 0)
            {
                dgvInternationalLicensesList.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesList.Columns[0].Width = 100;

                dgvInternationalLicensesList.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesList.Columns[1].Width = 100;

                dgvInternationalLicensesList.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesList.Columns[2].Width = 250;

                dgvInternationalLicensesList.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesList.Columns[3].Width = 200;

                dgvInternationalLicensesList.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesList.Columns[4].Width = 200;

                dgvInternationalLicensesList.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesList.Columns[5].Width = 100;

            }

        }

        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);

            if( _Driver == null )
            {
                MessageBox.Show($"There is no Driver linked" +
                    $" to this Person ID {PersonID}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            _DriverID = _Driver.DriverID;


            _LoadLocalLicensesData();
            _LoadInternationalLicensesData();
        }

        private void ShowLocalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo((int)dgvLocalLicensesList.CurrentRow.Cells[0].Value);
            frmLicenseInfo.ShowDialog();
        }

        private void ShowInternationalLicenseInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmInternationalLicenseInfo frm =
                new frmInternationalLicenseInfo
                ((int)dgvInternationalLicensesList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
