namespace MySolution.People.Controls
{
    partial class ctrlPersonCardWithFilter
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
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.pbAddNewPerson = new System.Windows.Forms.PictureBox();
            this.pbSearchPerson = new System.Windows.Forms.PictureBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.ctrlPersonCard1 = new MySolution.ctrlPersonCard();
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchPerson)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.pbAddNewPerson);
            this.gbFilter.Controls.Add(this.pbSearchPerson);
            this.gbFilter.Controls.Add(this.txtFilter);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Controls.Add(this.cbFilter);
            this.gbFilter.Location = new System.Drawing.Point(19, 13);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(775, 75);
            this.gbFilter.TabIndex = 7;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // pbAddNewPerson
            // 
            this.pbAddNewPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAddNewPerson.Image = global::MySolution.Properties.Resources.AddPerson_32;
            this.pbAddNewPerson.Location = new System.Drawing.Point(510, 25);
            this.pbAddNewPerson.Name = "pbAddNewPerson";
            this.pbAddNewPerson.Size = new System.Drawing.Size(48, 34);
            this.pbAddNewPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAddNewPerson.TabIndex = 11;
            this.pbAddNewPerson.TabStop = false;
            this.pbAddNewPerson.Click += new System.EventHandler(this.pbAddNewPerson_Click);
            this.pbAddNewPerson.MouseEnter += new System.EventHandler(this.pbAddNewPerson_MouseEnter);
            this.pbAddNewPerson.MouseLeave += new System.EventHandler(this.pbAddNewPerson_MouseLeave);
            // 
            // pbSearchPerson
            // 
            this.pbSearchPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSearchPerson.Image = global::MySolution.Properties.Resources.SearchPerson;
            this.pbSearchPerson.Location = new System.Drawing.Point(456, 25);
            this.pbSearchPerson.Name = "pbSearchPerson";
            this.pbSearchPerson.Size = new System.Drawing.Size(48, 34);
            this.pbSearchPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchPerson.TabIndex = 10;
            this.pbSearchPerson.TabStop = false;
            this.pbSearchPerson.Click += new System.EventHandler(this.pbSearchPerson_Click);
            this.pbSearchPerson.MouseEnter += new System.EventHandler(this.pbSearchPerson_MouseEnter);
            this.pbSearchPerson.MouseLeave += new System.EventHandler(this.pbSearchPerson_MouseLeave);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(231, 32);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(219, 20);
            this.txtFilter.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Find By:";
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "Person ID",
            "National No."});
            this.cbFilter.Location = new System.Drawing.Point(74, 31);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(151, 21);
            this.cbFilter.TabIndex = 7;
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.Location = new System.Drawing.Point(19, 94);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.PersonID = -1;
            this.ctrlPersonCard1.Size = new System.Drawing.Size(775, 291);
            this.ctrlPersonCard1.TabIndex = 1;
            // 
            // ctrlPersonCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.ctrlPersonCard1);
            this.Name = "ctrlPersonCardWithFilter";
            this.Size = new System.Drawing.Size(814, 405);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchPerson)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonCard ctrlPersonCard1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.PictureBox pbAddNewPerson;
        private System.Windows.Forms.PictureBox pbSearchPerson;
    }
}
