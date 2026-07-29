using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global_Classes
{
    internal class clsValidation
    {
        public static bool IsNumber(string Value)
        {
            try
            {
                int IntValue = Convert.ToInt32(Value);
                return true;
            }
            catch 
            {
                    return false;
            }
            finally
            {

            }
             
        }
    }
}
