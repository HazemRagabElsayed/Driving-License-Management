using DVLDBusinessLayer;
using DVLD.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;


namespace DVLD
{
    public partial class frmLogin : Form
    {

        


        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser CurrentUser = clsUser.Find(txtUserName.Text, txtPassword.Text);

            if(CurrentUser != null)
            {
                if (CurrentUser.IsActive == true)
                {

                    if (chkRememberLogin.Checked)
                    {
                        //Properties.Settings.Default.UserName = clsGlobal.CurrentUser.UserName;
                        //Properties.Settings.Default.Password = clsGlobal.CurrentUser.Password;
                        //Properties.Settings.Default.Save();

                        //File.WriteAllText("Rememberedlogin.txt", CurrentUser.UserName
                        //        + Environment.NewLine + clsGlobal.CurrentUser.Password);

                        clsGlobal.RememberUserNameAndPassword(CurrentUser.UserName, CurrentUser.Password);

                    }
                    else
                    {
                        clsGlobal.RememberUserNameAndPassword("", "");

                    }

                    clsGlobal.CurrentUser = CurrentUser;

                    frmMain frmMain = new frmMain(this);
                    frmMain.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("your account is deactivated" +
                        ", please contact your admin", "Account need Activation"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
            else
            {
                MessageBox.Show("Invalid UserName/Password", "Wrong Credintials"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "";
            string Password = "";

            //if (File.Exists("Rememberedlogin.txt"))
            //{
            //    string[] lines = File.ReadAllLines("Rememberedlogin.txt");

            //    UserName = lines[0];
            //    Password = lines[1];
            //}

            //UserName = Properties.Settings.Default.UserName;
            //Password = Properties.Settings.Default.Password;

            if(clsGlobal.GetStoredCredentials(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberLogin.Checked = true;
            }
            else
            {
                chkRememberLogin.Checked = false;
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = PictureBox.DefaultBackColor;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = SystemColors.ControlDark;
        }
    }
}
