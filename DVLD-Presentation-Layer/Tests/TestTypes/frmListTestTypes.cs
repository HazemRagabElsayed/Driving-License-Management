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

namespace DVLD.Tests.TestTypes
{
    public partial class frmListTestTypes : Form
    {

        static DataTable _dtAllTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        void _RefreshTestTypesList()
        {
            _dtAllTestTypes = clsTestType.GetAll();
            dgvTestTypesList.DataSource = _dtAllTestTypes;
            lblRecordsNumber.Text = dgvTestTypesList.Rows.Count.ToString();

        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsTestType.GetAll();

            dgvTestTypesList.DataSource = _dtAllTestTypes;
            lblRecordsNumber.Text = _dtAllTestTypes.Rows.Count.ToString();

            if (dgvTestTypesList.Rows.Count > 0)
            {
                dgvTestTypesList.Columns[0].HeaderText = "ID";
                dgvTestTypesList.Columns[0].Width = 50;

                dgvTestTypesList.Columns[1].HeaderText = "Title";
                dgvTestTypesList.Columns[1].Width = 150;

                dgvTestTypesList.Columns[2].HeaderText = "Description";
                dgvTestTypesList.Columns[2].Width = 320;

                dgvTestTypesList.Columns[3].HeaderText = "Fees";
                dgvTestTypesList.Columns[3].Width = 80;
            }
        }

        private void EditTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frmEditTestType = new frmEditTestType((clsTestType.enTestType)dgvTestTypesList.CurrentRow.Cells[0].Value);
            frmEditTestType.Owner = this;
            frmEditTestType.Show();
        }
    }
}
