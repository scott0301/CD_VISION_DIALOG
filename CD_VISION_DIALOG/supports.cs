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

        public static string TIME_GetTimeCode_MD_HMS_MS()
        {
            string strTime = string.Format("[{0:00}{1:00}_{2:00}:{3:00}:{4:00}_{5:00}]", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            return strTime;
        }

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
        public static string TIME_DateTimeTo_YMD_HMS(DateTime dt)
        {
            string strTime = string.Format("{0:00}{1:00}{2:00}_{3:00}{4:00}{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            strTime = strTime.Remove(0, 2);
            return strTime;
        }
        public static void MakeSureDirectoryExistance(string strPath)
        {
            if (Directory.Exists(strPath) == false)
            {
                Directory.CreateDirectory(strPath);
            }
        }
        public static DateTime Conv_YMD_HMSToDateTime(string strTimeCode)
        {
            DateTime dt = DateTime.Now;

            try
            {
                string[] parse = strTimeCode.Split('_');

                string year = "20" + parse[0].Substring(0, 2);
                string month = parse[0].Substring(2, 2);
                string day = parse[0].Substring(4, 2);
                string hour = parse[1].Substring(0, 2);
                string min = parse[1].Substring(2, 2);
                string sec = parse[1].Substring(4, 2);

                dt = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day), Convert.ToInt32(hour), Convert.ToInt32(min), Convert.ToInt32(sec));
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }

            return dt;
        }

        


       
    }
}
