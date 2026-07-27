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

namespace MySolution.Applications
{
    public partial class frmListApplicationTypes : Form
    {

        static DataTable _dtAllApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        void _RefreshApplicationTypesList()
        {
            _dtAllApplicationTypes = clsApplicationType.GetAll();
            dgvApplicationTypesList.DataSource = _dtAllApplicationTypes;
            lblRecordsNumber.Text = dgvApplicationTypesList.Rows.Count.ToString();

        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAll();

            dgvApplicationTypesList.DataSource = _dtAllApplicationTypes;
            lblRecordsNumber.Text = _dtAllApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypesList.Rows.Count > 0)
            {
                dgvApplicationTypesList.Columns[0].HeaderText = "ID";
                dgvApplicationTypesList.Columns[0].Width = 50;

                dgvApplicationTypesList.Columns[1].HeaderText = "Title";
                dgvApplicationTypesList.Columns[1].Width = 280;

                dgvApplicationTypesList.Columns[2].HeaderText = "Fees";
                dgvApplicationTypesList.Columns[2].Width = 80;
            }
        }

        private void EditApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            frmEditApplicationType frmUpdateApplicationType = new frmEditApplicationType
                ((clsApplicationType.enAppType)dgvApplicationTypesList.CurrentRow.Cells[0].Value);

            /* To solve problem of form window 
             * go behind MainForm when UpdateForm Is Closed*/

            //Use This Method
            //frmUpdateApplicationType.Owner = this;
            //frmUpdateApplicationType.Show();
            //Or use This Method
            frmUpdateApplicationType.ShowDialog();
            //_RefreshApplicationTypesList();
            frmManageApplicationTypes_Load(null, null);
 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
