namespace MySolution.Licenses.Controls
{
    partial class ctrlDriverLicenses
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLocal = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLocalRecordsNumber = new System.Windows.Forms.Label();
            this.dgvLocalLicensesList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tpInternational = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInternationalRecordsNumber = new System.Windows.Forms.Label();
            this.dgvInternationalLicensesList = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.cmsLocalLicensesHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowLocalLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInternationalLicensesHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowInternationalLicenseInfoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesList)).BeginInit();
            this.tpInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesList)).BeginInit();
            this.cmsLocalLicensesHistory.SuspendLayout();
            this.cmsInternationalLicensesHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1004, 230);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpLocal);
            this.tabControl1.Controls.Add(this.tpInternational);
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(992, 205);
            this.tabControl1.TabIndex = 0;
            // 
            // tpLocal
            // 
            this.tpLocal.Controls.Add(this.label1);
            this.tpLocal.Controls.Add(this.lblLocalRecordsNumber);
            this.tpLocal.Controls.Add(this.dgvLocalLicensesList);
            this.tpLocal.Controls.Add(this.label2);
            this.tpLocal.Location = new System.Drawing.Point(4, 22);
            this.tpLocal.Name = "tpLocal";
            this.tpLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocal.Size = new System.Drawing.Size(984, 179);
            this.tpLocal.TabIndex = 0;
            this.tpLocal.Text = "Local";
            this.tpLocal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Local Licenses History :";
            // 
            // lblLocalRecordsNumber
            // 
            this.lblLocalRecordsNumber.AutoSize = true;
            this.lblLocalRecordsNumber.Location = new System.Drawing.Point(73, 153);
            this.lblLocalRecordsNumber.Name = "lblLocalRecordsNumber";
            this.lblLocalRecordsNumber.Size = new System.Drawing.Size(13, 13);
            this.lblLocalRecordsNumber.TabIndex = 54;
            this.lblLocalRecordsNumber.Text = "0";
            // 
            // dgvLocalLicensesList
            // 
            this.dgvLocalLicensesList.AllowUserToAddRows = false;
            this.dgvLocalLicensesList.AllowUserToDeleteRows = false;
            this.dgvLocalLicensesList.AllowUserToOrderColumns = true;
            this.dgvLocalLicensesList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLocalLicensesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicensesList.ContextMenuStrip = this.cmsLocalLicensesHistory;
            this.dgvLocalLicensesList.Location = new System.Drawing.Point(6, 34);
            this.dgvLocalLicensesList.Name = "dgvLocalLicensesList";
            this.dgvLocalLicensesList.ReadOnly = true;
            this.dgvLocalLicensesList.RowHeadersWidth = 20;
            this.dgvLocalLicensesList.Size = new System.Drawing.Size(972, 116);
            this.dgvLocalLicensesList.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "# Records:";
            // 
            // tpInternational
            // 
            this.tpInternational.Controls.Add(this.label3);
            this.tpInternational.Controls.Add(this.lblInternationalRecordsNumber);
            this.tpInternational.Controls.Add(this.dgvInternationalLicensesList);
            this.tpInternational.Controls.Add(this.label5);
            this.tpInternational.Location = new System.Drawing.Point(4, 22);
            this.tpInternational.Name = "tpInternational";
            this.tpInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternational.Size = new System.Drawing.Size(984, 179);
            this.tpInternational.TabIndex = 1;
            this.tpInternational.Text = "International";
            this.tpInternational.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "International Licenses History :";
            // 
            // lblInternationalRecordsNumber
            // 
            this.lblInternationalRecordsNumber.AutoSize = true;
            this.lblInternationalRecordsNumber.Location = new System.Drawing.Point(73, 153);
            this.lblInternationalRecordsNumber.Name = "lblInternationalRecordsNumber";
            this.lblInternationalRecordsNumber.Size = new System.Drawing.Size(13, 13);
            this.lblInternationalRecordsNumber.TabIndex = 58;
            this.lblInternationalRecordsNumber.Text = "0";
            // 
            // dgvInternationalLicensesList
            // 
            this.dgvInternationalLicensesList.AllowUserToAddRows = false;
            this.dgvInternationalLicensesList.AllowUserToDeleteRows = false;
            this.dgvInternationalLicensesList.AllowUserToOrderColumns = true;
            this.dgvInternationalLicensesList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInternationalLicensesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicensesList.ContextMenuStrip = this.cmsInternationalLicensesHistory;
            this.dgvInternationalLicensesList.Location = new System.Drawing.Point(6, 34);
            this.dgvInternationalLicensesList.Name = "dgvInternationalLicensesList";
            this.dgvInternationalLicensesList.ReadOnly = true;
            this.dgvInternationalLicensesList.RowHeadersWidth = 20;
            this.dgvInternationalLicensesList.Size = new System.Drawing.Size(972, 116);
            this.dgvInternationalLicensesList.TabIndex = 56;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "# Records:";
            // 
            // cmsLocalLicensesHistory
            // 
            this.cmsLocalLicensesHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowLocalLicenseInfoToolStripMenuItem});
            this.cmsLocalLicensesHistory.Name = "cmsLocalLicensesHistory";
            this.cmsLocalLicensesHistory.Size = new System.Drawing.Size(186, 42);
            // 
            // ShowLocalLicenseInfoToolStripMenuItem
            // 
            this.ShowLocalLicenseInfoToolStripMenuItem.Image = global::MySolution.Properties.Resources.License_View_32;
            this.ShowLocalLicenseInfoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowLocalLicenseInfoToolStripMenuItem.Name = "ShowLocalLicenseInfoToolStripMenuItem";
            this.ShowLocalLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(185, 38);
            this.ShowLocalLicenseInfoToolStripMenuItem.Text = "Show License Info";
            this.ShowLocalLicenseInfoToolStripMenuItem.Click += new System.EventHandler(this.ShowLocalLicenseInfoToolStripMenuItem_Click);
            // 
            // cmsInternationalLicensesHistory
            // 
            this.cmsInternationalLicensesHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowInternationalLicenseInfoToolStripMenuItem1});
            this.cmsInternationalLicensesHistory.Name = "cmsInternationalLicensesHistory";
            this.cmsInternationalLicensesHistory.Size = new System.Drawing.Size(186, 42);
            // 
            // ShowInternationalLicenseInfoToolStripMenuItem1
            // 
            this.ShowInternationalLicenseInfoToolStripMenuItem1.Image = global::MySolution.Properties.Resources.License_View_32;
            this.ShowInternationalLicenseInfoToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowInternationalLicenseInfoToolStripMenuItem1.Name = "ShowInternationalLicenseInfoToolStripMenuItem1";
            this.ShowInternationalLicenseInfoToolStripMenuItem1.Size = new System.Drawing.Size(185, 38);
            this.ShowInternationalLicenseInfoToolStripMenuItem1.Text = "Show License Info";
            this.ShowInternationalLicenseInfoToolStripMenuItem1.Click += new System.EventHandler(this.ShowInternationalLicenseInfoToolStripMenuItem1_Click);
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlDriverLicenses";
            this.Size = new System.Drawing.Size(1010, 239);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpLocal.ResumeLayout(false);
            this.tpLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesList)).EndInit();
            this.tpInternational.ResumeLayout(false);
            this.tpInternational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesList)).EndInit();
            this.cmsLocalLicensesHistory.ResumeLayout(false);
            this.cmsInternationalLicensesHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLocal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLocalRecordsNumber;
        private System.Windows.Forms.DataGridView dgvLocalLicensesList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tpInternational;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInternationalRecordsNumber;
        private System.Windows.Forms.DataGridView dgvInternationalLicensesList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip cmsLocalLicensesHistory;
        private System.Windows.Forms.ToolStripMenuItem ShowLocalLicenseInfoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsInternationalLicensesHistory;
        private System.Windows.Forms.ToolStripMenuItem ShowInternationalLicenseInfoToolStripMenuItem1;
    }
}
