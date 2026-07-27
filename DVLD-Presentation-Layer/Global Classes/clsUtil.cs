using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MySolution.Global_Classes
{
    public class clsUtil
    {
        static public void CenterLabelTitle(Size Size, Label lbl)
        {


            int x = (Size.Width - lbl.Width) / 2;
            int y = (Size.Height - lbl.Height) / 2;

            lbl.Location = new Point(x, lbl.Location.Y);
        }
    }
}
