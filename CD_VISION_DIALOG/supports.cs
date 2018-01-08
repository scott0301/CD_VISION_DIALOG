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

 
namespace CD_VISION_DIALOG
{
    public interface iPtrn
    {
        void/*****/ iPtrn_Init();
        Bitmap/***/ iPtrn_LoadImage(); 
        double/***/ iPtrn_DoPtrn(Bitmap bmpSource, Bitmap bmpTemplate, double fRatio, RectangleF rcSearching, out RectangleF rcTemplate, out PointF ptTemplateCenter);
    }

    public class CIterativeStaticRun
    {
        private bool bStaticRun = false; // static repeatability test
        public bool IS_STATIC_RUN { get { return bStaticRun; } set { bStaticRun = value; } }
        public int STACK_SIZE { get { return listSequence.Count(); } }

        public Stack<CInspUnit> listSequence = new Stack<CInspUnit>();

        public void SetInit()
        {
            listSequence.Clear();
            IS_STATIC_RUN = false;
        }

        public void AppendItem_Single(byte[] rawImage, int imageW, int imageH, int nCamNo, CFigureManager fm)
        {
            CInspUnit iu = new CInspUnit();
            iu.AppendItem_Single(rawImage, imageW, imageH, fm, nCamNo);
            listSequence.Push(iu);
        }
       
    }
}
