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
using MySolution.Global;
using System.Runtime.CompilerServices;
using System.IO;


namespace MySolution
{
    public partial class frmLogin : Form
    {

        


        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = clsUser.Find(txtUserName.Text, txtPassword.Text);

            if(clsGlobal.CurrentUser != null)
            {
                if (clsGlobal.CurrentUser.IsActive == true)
                {

                    if (chkRememberLogin.Checked)
                    {
                        //Properties.Settings.Default.UserName = clsGlobal.CurrentUser.UserName;
                        //Properties.Settings.Default.Password = clsGlobal.CurrentUser.Password;
                        //Properties.Settings.Default.Save();

                        if (!File.Exists("Rememberedlogin.txt"))
                        {
                            File.WriteAllText("Rememberedlogin.txt", clsGlobal.CurrentUser.UserName
                                + Environment.NewLine + clsGlobal.CurrentUser.Password);
                        }

                    }
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

            if (File.Exists("Rememberedlogin.txt"))
            {
                string[] lines = File.ReadAllLines("Rememberedlogin.txt");

                UserName = lines[0];
                Password = lines[1];
            }

            //UserName = Properties.Settings.Default.UserName;
            //Password = Properties.Settings.Default.Password;

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            pbClose.BackColor = SystemColors.ControlDark;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            pbClose.BackColor = PictureBox.DefaultBackColor;

        }


    }
}
