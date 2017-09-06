using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP_Troops
{
    public static class HELPER_TIME_CODE
    {
        public static string TIME_GetTImeCode_YY_MM_DD()
        {
            DateTime curr = DateTime.Now;
            string strTime = string.Format("[{0:0000}-{1:00}-{2:00}]", curr.Year, curr.Month, curr.Day);
            return strTime;
        }
        public static string GetTimeCode4Save_MM_DD_HH_MM_SS()
        {
            DateTime curr = DateTime.Now;
            string strTime = string.Format("{0:00}_{1:00}_{2:00}{3:00}{4:00}", curr.Month, curr.Day, curr.Hour, curr.Minute, curr.Second);
            return strTime;
        }
        public static string GetTimeCode4Save_HH_MM_SS_MMM()
        {
            DateTime curr = DateTime.Now;
            string strTime = string.Format("{0:00}_{1:00}_{2:00}_{3:000}", curr.Hour, curr.Minute, curr.Second, curr.Millisecond);
            return strTime;
        }
        public static string TIME_GetTimeCode_MD_HMS_MS()
        {
            string strTime = string.Format("[{0:00}{1:00}_{2:00}:{3:00}:{4:00}_{5:00}]", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            return strTime;
        }
        public static string TIME_DateTimeTo_YMD_HMS(DateTime dt)
        {
            string strTime = string.Format("{0:00}{1:00}{2:00}_{3:00}{4:00}{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            strTime = strTime.Remove(0, 2);
            return strTime;
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
