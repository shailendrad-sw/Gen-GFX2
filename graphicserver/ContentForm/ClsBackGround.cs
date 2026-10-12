using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphicserver.ContentForm
{
    
   public class ClsBackGround
    {
       public static List<string> LstColor = new List<string>() { "#2F4F4F", "#800000", "#191970", "#00CED1", "#008080", "#4B0082", "#ff6347" };
       private static int tempIndex=0;
        public static Color SelectTheamColor()
        {
            Random ran = new Random();
            int index=ran.Next(LstColor.Count);
            while(index==tempIndex)
            {
                index = ran.Next(LstColor.Count);
            }
            tempIndex = index;
            string strcolor = LstColor[index];
            return  ColorTranslator.FromHtml(strcolor);
              
        }
        public static Color ChangeWritness(Color _anyColor ,double correctionFactor)
        {
            double red= _anyColor.R;
            double green = _anyColor.G;
            double blue = _anyColor.B;
            if(correctionFactor<0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(_anyColor.A,(byte)red,(byte)green,(byte)blue);
        }
    }
    
}
