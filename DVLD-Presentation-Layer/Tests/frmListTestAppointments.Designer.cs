namespace DVLD.Applications
{
    partial class frmListTestAppointments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.pbTestType = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbAddNewTestAppointment = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvTestAppointmentsList = new System.Windows.Forms.DataGridView();
            this.cmsTestAppointmentsList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editTestAppointmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TakeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlApplicationInfo1 = new DVLD.Applications.ctrlLocalDrivingApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewTestAppointment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestAppointmentsList)).BeginInit();
            this.cmsTestAppointmentsList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Location = new System.Drawing.Point(83, 655);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(13, 13);
            this.lblRecordsNumber.TabIndex = 68;
            this.lblRecordsNumber.Text = "0";
            // 
            // pbTestType
            // 
            this.pbTestType.Image = global::DVLD.Properties.Resources.Vision_512;
            this.pbTestType.Location = new System.Drawing.Point(263, 12);
            this.pbTestType.Name = "pbTestType";
            this.pbTestType.Size = new System.Drawing.Size(161, 94);
            this.pbTestType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestType.TabIndex = 67;
            this.pbTestType.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(169, 109);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(349, 35);
            this.lblTitle.TabIndex = 66;
            this.lblTitle.Text = "Vision  Test Appointments";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 655);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "# Records:";
            // 
            // pbAddNewTestAppointment
            // 
            this.pbAddNewTestAppointment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAddNewTestAppointment.Image = global::DVLD.Properties.Resources.AddAppointment_32;
            this.pbAddNewTestAppointment.Location = new System.Drawing.Point(642, 424);
            this.pbAddNewTestAppointment.Name = "pbAddNewTestAppointment";
            this.pbAddNewTestAppointment.Size = new System.Drawing.Size(33, 30);
            this.pbAddNewTestAppointment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAddNewTestAppointment.TabIndex = 63;
            this.pbAddNewTestAppointment.TabStop = false;
            this.pbAddNewTestAppointment.Click += new System.EventHandler(this.pbAddNewTestAppointment_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(565, 655);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 34);
            this.btnClose.TabIndex = 64;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvTestAppointmentsList
            // 
            this.dgvTestAppointmentsList.AllowUserToAddRows = false;
            this.dgvTestAppointmentsList.AllowUserToDeleteRows = false;
            this.dgvTestAppointmentsList.AllowUserToOrderColumns = true;
            this.dgvTestAppointmentsList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTestAppointmentsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestAppointmentsList.ContextMenuStrip = this.cmsTestAppointmentsList;
            this.dgvTestAppointmentsList.Location = new System.Drawing.Point(12, 460);
            this.dgvTestAppointmentsList.Name = "dgvTestAppointmentsList";
            this.dgvTestAppointmentsList.ReadOnly = true;
            this.dgvTestAppointmentsList.RowHeadersWidth = 20;
            this.dgvTestAppointmentsList.Size = new System.Drawing.Size(663, 189);
            this.dgvTestAppointmentsList.TabIndex = 62;
            // 
            // cmsTestAppointmentsList
            // 
            this.cmsTestAppointmentsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTestAppointmentToolStripMenuItem,
            this.TakeTestToolStripMenuItem});
            this.cmsTestAppointmentsList.Name = "cmsPeopleList";
            this.cmsTestAppointmentsList.Size = new System.Drawing.Size(197, 102);
            // 
            // editTestAppointmentToolStripMenuItem
            // 
            this.editTestAppointmentToolStripMenuItem.Image = global::DVLD.Properties.Resources.edit_32;
            this.editTestAppointmentToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editTestAppointmentToolStripMenuItem.Name = "editTestAppointmentToolStripMenuItem";
            this.editTestAppointmentToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.editTestAppointmentToolStripMenuItem.Text = "Edit";
            this.editTestAppointmentToolStripMenuItem.Click += new System.EventHandler(this.editTestAppointmentToolStripMenuItem_Click);
            // 
            // TakeTestToolStripMenuItem
            // 
            this.TakeTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Test_32;
            this.TakeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TakeTestToolStripMenuItem.Name = "TakeTestToolStripMenuItem";
            this.TakeTestToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.TakeTestToolStripMenuItem.Text = "Take Test";
            this.TakeTestToolStripMenuItem.Click += new System.EventHandler(this.TakeTestToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 433);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Appointments :";
            // 
            // ctrlApplicationInfo1
            // 
            this.ctrlApplicationInfo1.Location = new System.Drawing.Point(12, 147);
            this.ctrlApplicationInfo1.Name = "ctrlApplicationInfo1";
            this.ctrlApplicationInfo1.Size = new System.Drawing.Size(663, 271);
            this.ctrlApplicationInfo1.TabIndex = 0;
            // 
            // frmListTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 698);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRecordsNumber);
            this.Controls.Add(this.pbTestType);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbAddNewTestAppointment);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvTestAppointmentsList);
            this.Controls.Add(this.ctrlApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListTestAppointments";
            this.Load += new System.EventHandler(this.frmListTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTestType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewTestAppointment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestAppointmentsList)).EndInit();
            this.cmsTestAppointmentsList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlLocalDrivingApplicationInfo ctrlApplicationInfo1;
        private System.Windows.Forms.Label lblRecordsNumber;
        private System.Windows.Forms.PictureBox pbTestType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbAddNewTestAppointment;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvTestAppointmentsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmsTestAppointmentsList;
        private System.Windows.Forms.ToolStripMenuItem editTestAppointmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TakeTestToolStripMenuItem;
    }
}