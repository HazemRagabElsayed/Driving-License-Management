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
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();

                string FilePath = CurrentDirectory + @"\data.txt";

                if (!File.Exists(FilePath))
                {
                    return false;
                }

                
                using (StreamReader Read = new StreamReader(FilePath))
                {
                    string Line;
                    while ((Line = Read.ReadLine()) != null)
                    {
                        string[] Result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                        UserName = Result[0];
                        Password = Result[1];
                    }
                    return true;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
                return false;
            }
        }
    }
}
