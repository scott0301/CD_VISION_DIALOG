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

using DispObject;
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

    public static class CMessagebox
    {
        public static DialogResult Figure_Add()
        {
            return MessageBox.Show("Do You Want To Add New Figure?", "CREATE FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult Figure_Copy()
        {
            return MessageBox.Show("Do You Want To Copy Selected Figure?", "COPY FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
        }
        public static DialogResult Figure_Modify()
        {
            return MessageBox.Show("Do You Want To Modify Selected Figure?", "MODIFY FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult Figure_Remove()
        {
            return MessageBox.Show("Do You Want To Remove Selected Figure?", "REMOVE FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult Finished_Parameter_Change()
        {
            return MessageBox.Show("Parameters Has Changed.", "CHANGE PARAMETER", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool Figure_is_invalid(RectangleF rc, int imageW, int imageH)
        {
            bool isInvalid = false;

            // size error
            if (rc.Width == 0 || rc.Height == 0) isInvalid |= true;

            // position error
            if (CRect.isValid(rc, imageW, imageH) == false ) isInvalid |= true;

            if (isInvalid == true)
            {
                MessageBox.Show("Measurement Region is not Valid", "INVALID FIGURE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isInvalid;
        }
    }
}
