using DVLDBusinessLayer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Global
{
    static public class clsGlobal
    {
        static public clsUser CurrentUser { get; set; }

        static public bool RememberUserNameAndPassword(string UserName, string Password)
        {
            string KeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\DVLD";
            string UserValueName = "UserName";
            string PasswordValueName = "Password";

            try
            {
                Registry.SetValue(KeyPath, UserValueName, UserName, RegistryValueKind.String);
                Registry.SetValue(KeyPath, PasswordValueName, Password, RegistryValueKind.String);

                Console.WriteLine($"UserName Data : {UserName}" +
                    $" is stored successfully in Value Name : {UserValueName}" +
                    $" in path : {KeyPath}");
                Console.WriteLine($"Password Data : {Password}" +
                    $" is stored successfully in Value Name : {PasswordValueName}" +
                    $" in path : {KeyPath}");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
                return false;
                
            }
            
        }

        static public bool GetStoredCredentials(ref string UserName, ref string Password)
        {
            string KeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\DVLD";
            string UserValueName = "UserName";
            string PasswordValueName = "Password";

            try
            {
                UserName = (string)Registry.GetValue(KeyPath, UserValueName, null);
                Password = (string)Registry.GetValue(KeyPath, PasswordValueName, null);

                if (UserName != null && Password != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
                return false;
            }
        }
    }
}
