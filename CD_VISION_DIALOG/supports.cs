using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

using System.Timers;
using System.Collections.Generic;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using CD_Figure;
using CD_Paramter;
using CD_View;

using WrapperUnion;

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

        public static void DumpPoints(List<PointF> list)
        {
            WrapperExcel ex = new WrapperExcel();

            for (int i = 0; i < list.Count; i++)
            {
                string[] data = new string[2];

                data[0] = list.ElementAt(i).X.ToString();
                data[1] = list.ElementAt(i).Y.ToString();

                ex.data.Add(data);
            }

            ex.Dump_Data("c:\\" + WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM() + "_COORD.xlsx");

        }
    }
    public class CIterativeStaticRun
    {
        private bool bStaticRun = false; // static repeatability test
        public bool IS_STATIC_RUN { get { return bStaticRun; } set { bStaticRun = value; } }
        public int STACK_SIZE { get { return listSequence.Count(); } } 

        public Stack<CInspectionUnit> listSequence = new Stack<CInspectionUnit>();

        public void SetInit()
        {
            listSequence.Clear();
            IS_STATIC_RUN = false;
        }

        public void AppendItem(byte[] rawImage, int imageW, int imageH, int nCamNo, CFigureManager fm)
        {
            CInspectionUnit iu = new CInspectionUnit(rawImage, imageW, imageH, nCamNo, fm);
            listSequence.Push(iu);
        }
       
    }
}
