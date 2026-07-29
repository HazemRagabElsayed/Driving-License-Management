using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Global
{
    static public class clsGlobal
    {
        static public clsUser CurrentUser { get; set; }

        static public bool RememberUserNameAndPassword(string UserName, string Password)
        {
            try
            {

                string CurrentDirectory = Directory.GetCurrentDirectory();

                string FilePath = CurrentDirectory + @"\data.txt";

                if (File.Exists(FilePath) 
                    && (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)))
                {
                        File.Delete(FilePath);
                        return true;
                }
                string DataToSave = UserName + "#//#" + Password;
                
                using (StreamWriter Writer = new StreamWriter(FilePath, false))
                {
                        Writer.Write(DataToSave);

                        return true;
                }

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
