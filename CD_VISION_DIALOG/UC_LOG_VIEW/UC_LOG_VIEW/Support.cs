using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using System.IO;

namespace UC_LogView
{
     public static class DEF_OPERATION
    {
        public static string OPER_01_POWER/****/ = "[POWER]";  // on/off related
        public static string OPER_02_RECP/*****/ = "[RECIPE]";  // recp related
        public static string OPER_03_PARAM/****/ = "[PARAMETER]"; // parameter related
        public static string OPER_04_IMAGE/****/ = "[IMAGE]"; // image related
        public static string OPER_05_MEAS/*****/ = "[MEASURE]"; // measure related
        public static string OPER_06_COMM/*****/ = "[COMM]"; // COMM related
    }

     
    public static class CSupport
    {
        public static string GetBaseLogFileName()
        {
           return GetTimeCode4SaveDaily() + ".LOG";
        }
        public static string GetFullLogFileName(string basePath, string baseFile)
        {
            return Path.Combine(basePath, baseFile);
        }

        //*****************************************************************************************
        // Time Code
        //*****************************************************************************************

        public static string GetTimeCode4SaveDaily()
        {
            string strTime = string.Format("{0:00}_{1:00}_{2:00}", DateTime.Now.Year % 2000, DateTime.Now.Month, DateTime.Now.Day);
            return strTime;
        }
        public static string TIME_GetTimeCode_MD_HMS_MS()
        {
            string strTime = string.Format("[{0:00}{1:00}_{2:00}:{3:00}:{4:00}_{5:000}]", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            return strTime;
        }
 
    }

    //****************************************************************************************************
    // ThreadPool Structure
    //****************************************************************************************************
    public class CThreadPoolData
    {
        // for data racing
        object locker = new object();

        // share data :
        public static bool m_bActive = false;

        // personal data
        public string basePath = string.Empty;
        public string msg = string.Empty;
        public int/**/nIndex = 0;


        //******************************************************************
        // Creator
        //******************************************************************

        public CThreadPoolData(string basepath, string msg, int nIndex)
        {
            this.nIndex = nIndex;
            this.basePath = basepath;
            this.msg = msg;
        }

        //******************************************************************
        // functions for status
        //******************************************************************

        public void AbortAllWorks() { m_bActive = false; }

        public bool GetActivation() { return m_bActive; }
        public void SetActivation(bool bActive) { m_bActive = bActive; }


    }


    //****************************************************************************************************
    // UI Invoke
    //****************************************************************************************************
    static class ControlExtensions
    {
        static public void UIThread(this Control control, Action code)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(code);

            }
            else
            {
                code();
            }
        }

        static public void UIThreadInvoke(this Control control, Action code)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(code);
                return;
            }
            else
            {
                code.Invoke();
            }
        }
    }
}
