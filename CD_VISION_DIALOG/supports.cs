using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using CD_Figure;
using CD_Paramter;
using CD_View;

using System.Drawing;

namespace CD_VISION_DIALOG
{
    public interface iPtrn
    {
        void/*****/ iPtrn_Init();
        Bitmap/***/ iPtrn_LoadImage(); 
        double/***/ iPtrn_DoPtrn(Bitmap bmpSource, Bitmap bmpTemplate, double fRatio, RectangleF rcSearching, out RectangleF rcTemplate, out PointF ptTemplateCenter);
    }

    public static class Support
    {
        //*****************************************************************************************
        // TIME RELATED 
        //*****************************************************************************************

        

        public static string GetBuildInfo()
        {
            string strinfo = System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName;

            var parse = strinfo.Split(',');
            string filename = parse[0] + ".exe";

            string strCurPath = Application.StartupPath;
            FileInfo fi = new FileInfo(strCurPath);
            DateTime dt = fi.LastWriteTime;

            string[] t = { dt.Year.ToString(),                 string.Format("{0:00}", dt.Month),  string.Format("{0:00}", dt.Day  ), 
                                      string.Format("{0:00}", dt.Hour  ), string.Format("{0:00}", dt.Minute), string.Format("{0:00}", dt.Second) };

            string strDateTime = t[0] + t[1] + t[2] + "_" + t[3] + t[4] + t[5];

            return "BUILD INFORMATION : " + strDateTime;
        }
        
         

        


       
    }
}
