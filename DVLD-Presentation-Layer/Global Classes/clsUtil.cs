using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;

namespace DVLD.Global_Classes
{
    public class clsUtil
    {

        static public string GenerateGuid()
        {
            Guid NewGuid = Guid.NewGuid();

            return NewGuid.ToString();
        }

        static public string GenerateGuidWithExtensionOfFile(string FileName)
        {
            return GenerateGuid() + (new FileInfo(FileName).Extension);
        }

        static public bool CreateFolderIfDoesntExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    {
                        MessageBox.Show("Error Creating Folder :" + ex.Message);
                        return false;
                    }

                }
            }
            return true;
        }

        static public bool CopyImageToProjectFolder(ref string SourceFile)
        {
            if (!CreateFolderIfDoesntExist(@"D:\Repos\Driving-License-Management\DVLD-People-Images"))
            {
                return false;
            }

            string DestFile = @"D:\Repos\Driving-License-Management\DVLD-People-Images\" + GenerateGuidWithExtensionOfFile(SourceFile);

            try
            {
                File.Copy(SourceFile, DestFile, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving image :" + ex.Message);
                return false;
            }

            SourceFile = DestFile;

            return true;
        }

        static public void CenterLabelTitle(Size Size, Label lbl)
        {


                int x = (Size.Width - lbl.Width) / 2;
                int y = (Size.Height - lbl.Height) / 2;

                lbl.Location = new Point(x, lbl.Location.Y);
        }
    }
}
