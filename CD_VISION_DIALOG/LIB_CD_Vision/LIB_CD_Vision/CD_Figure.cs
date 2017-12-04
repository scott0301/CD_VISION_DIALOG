﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.IO;
using System.Xml.Serialization;

using DEF_PARAMS;
using CD_Measure;
using DispObject;
using WrapperUnion;

namespace CD_Figure
{
    
    [Serializable]
    public class CFigureManager : ICloneable
    {
        public BASE_RECP baseRecp = new BASE_RECP();
        public PARAM_OPTICS param_optics = new PARAM_OPTICS();
        public PARAM_PTRN param_ptrn = new PARAM_PTRN();
        public PARAM_PATH param_path = new PARAM_PATH();

        public List<string> listCommand = new List<string>();

        public List<CMeasurePairRct>/*******/ list_pair_Rct /***/= new List<CMeasurePairRct>();
        public List<CMeasurePairCir> /******/ list_pair_Cir /***/= new List<CMeasurePairCir>();
        public List<CMeasurePairOvl>/********/ list_pair_Ovl /****/= new List<CMeasurePairOvl>();
        public List<RectangleF> /***********/ list_rect /******/= new List<RectangleF>();

        public int COUNT_PAIR_RCT { get { return list_pair_Rct.Count; } }
        public int COUNT_PAIR_CIR { get { return list_pair_Cir.Count; } }
        public int COUNT_PAIR_OVL { get { return list_pair_Ovl.Count; } }
        public int COUNT_RECT { get { return list_rect.Count; } }
           
        public virtual object Clone() { return new CFigureManager(this); }

        public bool BOOL_USE_IMAGE_FOCUS { get; set; }

        public string RECP_FILE { get; set; }

        public RectangleF RC_FOCUS = new RectangleF();
        public RectangleF _rc_focus = new RectangleF();

        public PointF PT_PTRN_DELTA { get; set; }
        public void SetPtrnDelta(double fx, double fy)
        {
            PointF ptPtrnOrg = new PointF(param_ptrn.RC_TEMPLATE.X, param_ptrn.RC_TEMPLATE.Y);
            PointF ptPosNew = new PointF((float)fx, (float)fy);

            // just distance 
            PointF ptDelta = CPoint.GetDistancePoint(ptPtrnOrg, ptPosNew);
            // so apply delta to move ( minus vector ) 170727 
            this.PT_PTRN_DELTA = new PointF(-ptDelta.X, -ptDelta.Y);
        }

        public PointF SetPtrnDelta(PointF ptDelta)
        {
            SetPtrnDelta(ptDelta.X, ptDelta.Y);
            return this.PT_PTRN_DELTA;
        }

        public void CroodinateBackckupFocusRect(){_rc_focus = RC_FOCUS;}
        public void CroodinateRecoverFocusRect(){RC_FOCUS = _rc_focus;}
        public void SetRelativeMovemntFocusRect(){CroodinateRecoverFocusRect();RC_FOCUS.Offset(this.PT_PTRN_DELTA);}

        #region CREATOR
        public CFigureManager()
        {
            RECP_FILE = string.Empty;
            BOOL_USE_IMAGE_FOCUS = false;
            RC_FOCUS = new Rectangle(0,0,0,0);
         }
        protected CFigureManager(CFigureManager myself)
        {
            this.list_pair_Rct = myself.list_pair_Rct.ToList();
            this.list_pair_Cir = myself.list_pair_Cir.ToList();
            this.list_pair_Ovl = myself.list_pair_Ovl.ToList();
            this.list_rect = myself.list_rect.ToList();

            this.RECP_FILE = myself.RECP_FILE;

            this.RC_FOCUS = myself.RC_FOCUS;
            this.BOOL_USE_IMAGE_FOCUS = myself.BOOL_USE_IMAGE_FOCUS;

            this.param_optics = myself.param_optics;
            this.param_ptrn = myself.param_ptrn;

         }
        #endregion 

        #region GET SINGLE ELEMENT FOR EACH FIGURES
        public CMeasurePairRct ElementAt_PairRct(int nIndex)
        {
            CMeasurePairRct single = new CMeasurePairRct();
            if (nIndex <= COUNT_PAIR_RCT && COUNT_PAIR_RCT != 0) { single = list_pair_Rct.ElementAt(nIndex); }
            return single;
        }
        public CMeasurePairCir ElementAt_PairCir(int nIndex)
        {
            CMeasurePairCir single = new CMeasurePairCir();
            if (nIndex <= COUNT_PAIR_CIR && COUNT_PAIR_CIR != 0) { single = list_pair_Cir.ElementAt(nIndex); }
            return single;
        }
        public CMeasurePairOvl ElementAt_PairOvl(int nIndex)
        {
            CMeasurePairOvl single = new CMeasurePairOvl();
            if (nIndex <= COUNT_PAIR_OVL && COUNT_PAIR_OVL != 0) { single = list_pair_Ovl.ElementAt(nIndex); }
            return single;
        }
        public RectangleF ElementAt_Rectangle(int nIndex)
        {
            RectangleF rc = new RectangleF();

            if (nIndex <= COUNT_RECT && COUNT_RECT != 0)
            {
                rc = list_rect.ElementAt(nIndex);
            }
            return rc;
        }
        #endregion

        #region FIGURE MANAGEMENT

        public void RemoveAll()
        {
            list_rect.Clear();
            list_pair_Rct.Clear();
            list_pair_Cir.Clear();
            list_pair_Ovl.Clear();
        }
        public void Figure_Remove(int nFigureType, int nIndex)
        {
            /***/if (nFigureType == IFX_FIGURE.PAIR_RCT) { if (COUNT_PAIR_RCT >= nIndex && COUNT_PAIR_RCT != 0)list_pair_Rct.RemoveAt(nIndex); }
            else if (nFigureType == IFX_FIGURE.PAIR_CIR) { if (COUNT_PAIR_CIR >= nIndex && COUNT_PAIR_CIR != 0)list_pair_Cir.RemoveAt(nIndex); }
            else if (nFigureType == IFX_FIGURE.PAIR_OVL) { if (COUNT_PAIR_OVL >= nIndex && COUNT_PAIR_OVL != 0)list_pair_Ovl.RemoveAt(nIndex); }
        }
        public void Figure_Add(object single)
        {
            /***/if ("CMeasurePairRct" == single.GetType().Name){list_pair_Rct.Add((CMeasurePairRct)single);}
            else if ("CMeasurePairCir" == single.GetType().Name){list_pair_Cir.Add((CMeasurePairCir)single);}
            else if ("CMeasurePairOvl" == single.GetType().Name){list_pair_Ovl.Add((CMeasurePairOvl)single);}
        }

        public void Figure_Replace(object single)
        {
            string name = single.GetType().Name;
            /***/if (name == "CMeasurePairRct[]"){list_pair_Rct = ((CMeasurePairRct[])single).ToList();}
            else if (name == "CMeasurePairCir[]"){list_pair_Cir = ((CMeasurePairCir[])single).ToList();}
            else if (name == "CMeasurePairOvl[]"){list_pair_Ovl = ((CMeasurePairOvl[])single).ToList();}
        }

        public int GetFigureEmptyIndex(int nFigureIndex)
        {
            int nLastIndex = 1;

            int nItemCount = 0;

            /***/if (nFigureIndex == IFX_FIGURE.PAIR_RCT) nItemCount = COUNT_PAIR_RCT;
            else if (nFigureIndex == IFX_FIGURE.PAIR_CIR) nItemCount = COUNT_PAIR_CIR;
            else if (nFigureIndex == IFX_FIGURE.PAIR_OVL) nItemCount = COUNT_PAIR_OVL;

            if (nItemCount == 0) nLastIndex = 1;
            else
            {
                List<int> list = new List<int>();

                // make index list from every single data
                for (int i = 0; i < nItemCount; i++)
                {
                    string strName = string.Empty;

                    /***/if (nFigureIndex == IFX_FIGURE.PAIR_RCT) { strName = list_pair_Rct.ElementAt(i).NICKNAME; }
                    else if (nFigureIndex == IFX_FIGURE.PAIR_CIR) { strName = list_pair_Cir.ElementAt(i).NICKNAME; }
                    else if (nFigureIndex == IFX_FIGURE.PAIR_OVL) { strName = list_pair_Ovl.ElementAt(i).NICKNAME; }

                    string[] parsed = strName.Split('_');
                    try
                    {
                        int nIdx = Convert.ToInt32(parsed[1]);
                        list.Add(nIdx);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                int nMax = 0;

                if (list.Count > 0)  // zero exception 170811
                {
                    nMax = list.Max();;
                }
                bool bFound = false; 

                // if there is empty index , set !!
                for (int i = 1; i < list.Count; i++)
                {
                    if (list.IndexOf(i) == -1)
                    {
                        nLastIndex = i;
                        bFound = true;
                        break; 
                    }
                }

                if (bFound == false)
                {
                    nLastIndex = nMax + 1;
                }
            }

            return nLastIndex;
        }
        #endregion  

        #region ToArray()
        public CMeasurePairRct[] ToArray_PairRct() { return list_pair_Rct.ToArray(); }
        public CMeasurePairCir[] ToArray_PairCir() { return list_pair_Cir.ToArray(); }
        public CMeasurePairOvl[] ToArray_PairOvl() { return list_pair_Ovl.ToArray(); }
        #endregion  

        #region SERIALIZATION
        public static void SerializedXml_Save(string strPath, CFigureManager data)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CFigureManager));
            using (StreamWriter sw = new StreamWriter(strPath))
            {
                xmlSerializer.Serialize(sw, data);
            }      
        }

        public static CFigureManager SerializedXml_Load(string strPath)
        {
            CFigureManager fmTemp = new CFigureManager();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CFigureManager));
            using (StreamReader sr = new StreamReader(strPath))
            {
                fmTemp = (CFigureManager)xmlSerializer.Deserialize(sr);
            }

            return fmTemp;
        }
        #endregion
    }

    public sealed class parseRect
     {
        // public PointF ptOrigin = new PointF();

         private PointF pt_lt = new PointF();
         private PointF pt_rt = new PointF();
         private PointF pt_rb = new PointF();
         private PointF pt_lb = new PointF();

         public PointF LT { get { return pt_lt; } set { pt_lt = value; } }
         public PointF RT { get { return pt_rt; } set { pt_rt = value; } }
         public PointF LB { get { return pt_lb; } set { pt_lb = value; } }
         public PointF RB { get { return pt_rb; } set { pt_rb = value; } }

         public float MIN_X { get { return Math.Min(LT.X, LB.X); } }
         public float MAX_X { get { return Math.Max(RT.X, RB.X); } }
         public float MIN_Y { get { return Math.Min(LT.Y, RT.Y); } }
         public float MAX_Y { get { return Math.Max(LB.Y, RB.Y); } }

         public double Width
         {
             get
             {
                 double lt = _length(LT, RT);
                 double lb = _length(LB, RB);
                 return (lt + lb) / 2.0;
             }
         }
         public double Height
         {
             get
             {
                 double ll = _length(LT, LB);
                 double lr = _length(RT, RB);
                 return (ll + lr) / 2.0;
             }
         }

         public void ShiftX(object x)
         {

         }
         public void ScaleX(object tx)
         {
             float varX = Convert.ToSingle(tx);
             this.RT = new PointF(this.RT.X + varX, this.RT.Y);
             this.RB = new PointF(this.RB.X + varX, this.RB.Y);
         }
         public void ScaleY(object ty)
         {
             float varY = Convert.ToSingle(ty);

             this.LB = new PointF(this.LB.X, this.LB.Y + varY);
             this.RB = new PointF(this.RB.X, this.RB.Y + varY);
         }

         public void RotatePoint(PointF ptGravity, double fAngle)
         {
             LT = _RotatePointByGravity(LT, ptGravity, fAngle);
             RT = _RotatePointByGravity(RT, ptGravity, fAngle);
             LB = _RotatePointByGravity(LB, ptGravity, fAngle);
             RB = _RotatePointByGravity(RB, ptGravity, fAngle);
         }

         private PointF _RotatePointByGravity(PointF ptTarget, PointF ptGravity, double fAngle)
         {
             //x' = (x-a) * cosR - (y-b)sinR + a
             //y' = (x-a) * sinR + (y-b)cosR + b
             fAngle = fAngle * Math.PI / 180.0;

             PointF ptRotated = new PointF(0, 0);

             ptRotated.X = (float)(((ptTarget.X - ptGravity.X) * Math.Cos(fAngle) - (ptTarget.Y - ptGravity.Y) * Math.Sin(fAngle)) + ptGravity.X);
             ptRotated.Y = (float)(((ptTarget.X - ptGravity.X) * Math.Sin(fAngle) + (ptTarget.Y - ptGravity.Y) * Math.Cos(fAngle)) + ptGravity.Y);

             return ptRotated;
         }

         private double _length(PointF p1, PointF p2)
         {
             return Math.Sqrt(((p2.X - p1.X) * (p2.X - p1.X)) + ((p2.Y - p1.Y) * (p2.Y - p1.Y)));
         }

         public parseRect() { }
         public parseRect(parseRect rc)
         {
             this.LT = rc.LT;
             this.RT = rc.RT;
             this.LB = rc.LB;
             this.RB = rc.RB;
         }
         public parseRect(RectangleF rc)
         {
             this.pt_lt = new PointF(rc.X, rc.Y);
             this.pt_rt = new PointF(rc.X + rc.Width, rc.Y);
             this.pt_lb = new PointF(rc.X, rc.Y + rc.Height);
             this.pt_rb = new PointF(rc.X + rc.Width, rc.Y + rc.Height);
         }
         public parseRect(float x, float y, float w, float h)
         {
             this.pt_lt = new PointF(x, y);
             this.pt_rt = new PointF(x + w, y);
             this.pt_lb = new PointF(x, y + h);
             this.pt_rb = new PointF(x + w, y + h);
         }
         public List<PointF> ToArray()
         {
             PointF[] arrList = new PointF[4];
             arrList[0] = this.pt_lt;
             arrList[1] = this.pt_rt;
             arrList[2] = this.pt_rb;
             arrList[3] = this.pt_lb;
             return arrList.ToList();
         }
         public void OffsetRect(PointF pt)
         {
             OffsetRect(pt.X, pt.Y);
         }
         public void OffsetX(float tx)
         {
             this.LT = new PointF(this.LT.X + tx, this.LT.Y + 00);
             this.LB = new PointF(this.LB.X + tx, this.LB.Y + 00);
             this.RT = new PointF(this.RT.X + tx, this.RT.Y + 00);
             this.RB = new PointF(this.RB.X + tx, this.RB.Y + 00);

         }
         public void OffsetY(float ty)
         {
             this.LT = new PointF(this.LT.X + 00, this.LT.Y + ty);
             this.LB = new PointF(this.LB.X + 00, this.LB.Y + ty);
             this.RT = new PointF(this.RT.X + 00, this.RT.Y + ty);
             this.RB = new PointF(this.RB.X + 00, this.RB.Y + ty);
         }
         public void OffsetRect(float tx, float ty)
         {
             this.LT = new PointF(this.LT.X + tx, this.LT.Y + ty);
             this.RT = new PointF(this.RT.X + tx, this.RT.Y + ty);
             this.LB = new PointF(this.LB.X + tx, this.LB.Y + ty);
             this.RB = new PointF(this.RB.X + tx, this.RB.Y + ty);
         }
         public RectangleF ToRectangleF()
         {
             float minx = this.MIN_X;
             float miny = this.MIN_Y;
             float maxx = this.MAX_X;
             float maxy = this.MAX_Y;

             return new RectangleF(minx, miny, maxx - minx, maxy - miny);
         }
         public Rectangle ToRectangle()
         {
             return Rectangle.Round(ToRectangleF());
         }
         public parseRect CopyTo()
         {
             parseRect rc = new parseRect();
             rc.LT = this.LT;
             rc.RT = this.RT;
             rc.LB= this.LB;
             rc.RB = this.RB;
             return rc;
         }
         public static parseRect ConvHor2Ver(parseRect rcCur)
         {
             parseRect rcNew = rcCur.CopyTo();
             rcNew.LT = new PointF(rcCur.MIN_X, rcCur.MIN_Y);
             rcNew.LB = new PointF(rcCur.MIN_X, rcCur.MAX_Y);
             rcNew.RT = new PointF(rcCur.MAX_X, rcCur.MIN_Y);
             rcNew.RB = new PointF(rcCur.MAX_X, rcCur.MAX_Y);
             return rcNew;
         }
         public static parseRect ConvVer2Hor(parseRect rcCur)
         {
             parseRect rcNew = rcCur.CopyTo();
             rcNew.LT = new PointF(rcCur.MIN_X, rcCur.MIN_Y);
             rcNew.LB = new PointF(rcCur.MIN_X, rcCur.MAX_Y);
             rcNew.RT = new PointF(rcCur.MAX_X, rcCur.MIN_Y);
             rcNew.RB = new PointF(rcCur.MAX_X, rcCur.MAX_Y);
             return rcNew;
         }

     }

    public abstract class CMeasureMotherFucker
    {
        //***************************************************************************
        // Member variables
        //***************************************************************************

        public double PIXEL_RES { get; set; }
        public string NICKNAME { get; set; }

        public double/***/ param_comm_fitting_thr { get; set; }

        public double param_comm_01_compen_A { get; set; }
        public double param_comm_02_compen_B { get; set; }
        public int/**/param_comm_03_spc_enhance { get; set; }
        public int param_comm_04_refinement { get; set; }
        public bool param_comm_05_BOOL_SHOW_RAW_DATA { get; set; }


        public CMeasureMotherFucker() // common creator 
        {
            NICKNAME = string.Empty;
            PIXEL_RES = 0.069;

            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_spc_enhance = 0;
            param_comm_04_refinement = 3;
            param_comm_05_BOOL_SHOW_RAW_DATA = false;

            param_comm_fitting_thr = 3;
        }


        //***************************************************************************
        // Abstract Functions 
        //***************************************************************************

        // figure manpulation fuckers
        public abstract void AdjustGap(int tx, int ty);
        public abstract void AdjustPos(int tx, int ty);
        public abstract void AdjustSize(int tx, int ty);
 
        // mmanager for method branches
        public abstract bool VeryfyMeasurementMatching();
        public abstract string GetMeasurementCategory();

        // measurement method in detail for each fuckers.
        public abstract float rape_Pussy_Cardin(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,  ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX, ref List<PointF> listEdges_SMD, ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured);

        public abstract float rape_Bitch_Direction(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX, ref List<PointF> listEdges_FMD, ref List<PointF>listEdges_FIN,
            ref List<PointF> listEdges_SEX, ref List<PointF> listEdges_SMD, ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured);

        public abstract float rape_asshole_Log(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX, ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX, ref List<PointF> listEdges_SMD, ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured);

        public abstract float MeasureData(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX, ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX, ref List<PointF> listEdges_SMD, ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured);

        // Set Delta movement according to pre-calculated delta 170727
        public abstract void SetRelativeMovement(PointF ptDelta);
        // Backup Original Position
        public abstract void CroodinateBackup();
        public abstract void CroodinateRecover();

        public byte[] DoPreProcess(byte[] rawImage, int imageW, int imageH, double fSigma, int KSize, Rectangle rc)
        {
            double[] fKernel = Computer.HC_FILTER_GenerateGaussianFilter(fSigma, KSize);
            rawImage = Computer.HC_FILTER_ConvolutionWindow(fKernel, rawImage, imageW, imageH, rc);
            return rawImage;
        }
        public byte[] DoSPCProcess(byte[] rawImage, int imageW, int imageH, RectangleF rc, int nProcessType)
        {
            //rawImage = Computer.HC_TRANS_GradientImage(rawImage, imageW, imageH);

            if (CRect.isValid(ref rc, imageW, imageH) == false)
            {
                return rawImage;
            }

            double fKappa = 1;
            int nIter = 5;
            double fDelta = 0.5;


            bool bSave = false;

            if (bSave == true)
            {
                byte[] rawTemp = new byte[rawImage.Length];
                Array.Copy(rawImage, rawTemp, rawImage.Length);

                for (int nStep = 0; nStep < 7; nStep++)
                {
                    Array.Copy(rawTemp, rawImage, rawTemp.Length);   

                    if (nStep == 1)
                    {
                        rawImage = Computer.HC_FILTER_ADF_Window(rawImage, imageW, imageH, rc, fKappa, nIter, fDelta);
                    }
                    else if (nStep == 2)
                    {
                        byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                        rawImage = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
                    }
                    else if (nStep == 3)
                    {
                        rawImage = Computer.HC_FILTER_STD_Window(rawImage, imageW, imageH, rc, 5, 1);
                    }
                    else if (nStep == 4)
                    {
                        rawImage = Computer.HC_FILTER_MEAN_Window(rawImage, imageW, imageH, rc, 5);
                    }
                    else if (nStep == 5)
                    {
                        rawImage = Computer.ARRAY_GetPowImage(rawImage);
                        byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                        rawImage = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
                    }
                    else if (nStep == 6)
                    {
                        rawImage = Computer.HC_TRANS_GradientImage(rawImage, imageW, imageH);
                    }
                    else if (nStep == 7)
                    {
                        rawImage = Computer.HC_TRANS_GradientImage(rawImage, imageW, imageH);
                        byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                        rawImage = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
                    }
                    else if (nStep == 8)
                    {
                        rawImage = Computer.ARRAY_GetPowImage(rawImage);
                        byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                        rawImage = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
                        rawImage = Computer.HC_ARITH_ADD(rawTemp, rawImage, imageW, imageH);
                    }

                    Computer.SaveImage(rawImage, imageW, imageH, string.Format("c:\\PRP_STEP{0}.BMP", nStep));
                }

            }


            if (nProcessType == 1)
            {
                rawImage = Computer.HC_FILTER_ADF_Window(rawImage, imageW, imageH, rc, fKappa, nIter, fDelta);
            }
            else if (nProcessType == 2)
            {
                byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                rawImage = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
            }
            else if (nProcessType == 3)
            {
                rawImage = Computer.HC_FILTER_STD_Window(rawImage, imageW, imageH, rc, 5, 1);
            }
            else if (nProcessType == 4)
            {
                rawImage = Computer.HC_FILTER_MEAN_Window(rawImage, imageW, imageH, rc, 5);
            }
            else if (nProcessType == 5)
            {
                rawImage = Computer.ARRAY_GetPowImage(rawImage);
                byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                rawImage = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
            }
            else if (nProcessType == 6)
            {
                rawImage = Computer.HC_TRANS_GradientImage(rawImage, imageW, imageH);
            }


            
          
            return rawImage;
        }

        //***************************************************************************
        // Realistic Member Functions
        //***************************************************************************

        // for drawing : convert minus relative croodinate to absolute croodinate
        //public RectangleF GetCompensatedRect(RectangleF rc)
        //{
        //    RectangleF rcResult = rc;
        //
        //    // 상대좌표가 들어있으면 상대 출력을 해야지
        //    if (this.ptOrientBase.X != 0 && this.ptOrientBase.Y != 0)
        //    {
        //        rcResult.Offset(this.ptOrientBase);
        //
        //        // 보상좌표가 있으면 보상을 해야지
        //        if (this.ptOrientCurr.X != 0 && this.ptOrientCurr.Y != 0)
        //        {
        //            PointF ptRelativeDist = new PointF(ptOrientCurr.X - ptOrientBase.X, ptOrientCurr.Y - ptOrientBase.Y);
        //            rcResult.Offset(ptRelativeDist);
        //        }
        //        // 보상 좌표가 없으면 할게 없어.
        //        else if (this.ptOrientCurr.X == 0 && this.ptOrientCurr.Y == 0) { }
        //    }
        //    return rcResult;
        //}
    }
  

    public class CMeasurePairRct : CMeasureMotherFucker
    {
        private double SIGMA = 1.5;
        private int KERNEL = 7;

        public bool SELECTED { get; set; }

        public int ANGLE { get; set; }

        public int param_00_algorithm { get; set; }
        public bool param_01_bool_Use_AutoDetection { get; set; }
        public int param_02_peakTargetIndex_fst { get; set; }
        public int param_03_peakTargetIndex_scd { get; set; }
        public int param_04_peakCandidate { get; set; }
        public int param_05_windowSize { get; set; }
        public double param_06_edge_position_fst { get; set; }
        public double param_07_edge_position_scd { get; set; }

        public parseRect rc_FST = new parseRect();
        public parseRect rc_SCD = new parseRect();

        public parseRect _rc_FST = new parseRect();
        public parseRect _rc_SCD = new parseRect();

        public int RC_TYPE { get; set; }

        private CPeakMaster pm = new CPeakMaster();

        public CMeasurePairRct()
        {
            SELECTED = false;
            RC_TYPE = 0;
            ANGLE = 0;

            param_00_algorithm = 0;
            param_01_bool_Use_AutoDetection = false;
            param_02_peakTargetIndex_fst = 0;
            param_03_peakTargetIndex_scd = 1;
            param_04_peakCandidate = 2;
            param_05_windowSize = 2;
            param_06_edge_position_fst = 0;
            param_07_edge_position_scd = 0;

 
            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_spc_enhance = 0;
            param_comm_04_refinement = 3;
            param_comm_05_BOOL_SHOW_RAW_DATA = false;

        }
      
        public CMeasurePairRct CopyTo() // in order to avoid icloneable
        {
            CMeasurePairRct single = new CMeasurePairRct();

            single.NICKNAME = this.NICKNAME;

 
            single.ANGLE = this.ANGLE;
            single.RC_TYPE = this.RC_TYPE;

            single.rc_FST = this.rc_FST.CopyTo();
            single.rc_SCD = this.rc_SCD.CopyTo();
            single._rc_FST = this._rc_FST.CopyTo();
            single._rc_SCD = this._rc_SCD.CopyTo();

            single.param_00_algorithm = this.param_00_algorithm;
            single.param_01_bool_Use_AutoDetection/***/= this.param_01_bool_Use_AutoDetection;
            single.param_02_peakTargetIndex_fst/******/= this.param_02_peakTargetIndex_fst;
            single.param_03_peakTargetIndex_scd/******/= this.param_03_peakTargetIndex_scd;
            single.param_04_peakCandidate/************/= this.param_04_peakCandidate;
            single.param_05_windowSize/***************/= this.param_05_windowSize;
            single.param_06_edge_position_fst/********/= this.param_06_edge_position_fst;
            single.param_07_edge_position_scd/********/= this.param_07_edge_position_scd;
             
            single.param_comm_01_compen_A/*************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B/*************/= this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance/**********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_refinement/***********/= this.param_comm_04_refinement;
            single.param_comm_05_BOOL_SHOW_RAW_DATA/***/= this.param_comm_05_BOOL_SHOW_RAW_DATA;


            return single;
        }

        private double _GetDistance(double f1, double f2)
        {
            return Math.Max(f1, f2) - Math.Min(f1,f2);
        }
      
        
       # region COMMON OVERRIDINGS - NAVICATOR FUNCTIONS

        public override void AdjustGap(int tx, int ty)
        {
            if (this.RC_TYPE ==  IFX_RECT_TYPE.DIR_HOR)
            {
                this.rc_SCD.OffsetRect(0, ty);
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_VER)
            {
                this.rc_SCD.OffsetRect(tx, 0);
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
                if (tx == 0) { return; }// gap adjustment function for the fucking digonal rectangle response only for the fucking x-axis

                PointF ptCenter = GetCenter();
                rc_FST.RotatePoint(ptCenter, -ANGLE);
                rc_SCD.RotatePoint(ptCenter, -ANGLE);

                rc_SCD.OffsetRect(tx, ty);

                rc_FST.RotatePoint(ptCenter, ANGLE);
                rc_SCD.RotatePoint(ptCenter, ANGLE);
            }
            CroodinateBackup();
        }
        public override void AdjustPos(int tx, int ty)
        {
            rc_FST.OffsetRect(tx, ty);
            rc_SCD.OffsetRect(tx, ty);
            CroodinateBackup();
        }
        public override void AdjustSize(int tx, int ty)
        {
            PointF ptCenter = GetCenter();
            rc_FST.RotatePoint(ptCenter, -ANGLE);
            rc_SCD.RotatePoint(ptCenter, -ANGLE);

            if (tx != 0) { rc_FST.ScaleX(tx); rc_SCD.ScaleX(tx); }
            if (ty != 0) { rc_FST.ScaleY(ty); rc_SCD.ScaleY(ty); }

            rc_FST.RotatePoint(ptCenter, ANGLE);
            rc_SCD.RotatePoint(ptCenter, ANGLE);
            CroodinateBackup();
        }
         #endregion

       #region MEASUREMENT METHOD VERIFIER
        public override bool VeryfyMeasurementMatching(){return true; }
        public override string GetMeasurementCategory(){return IFX_ALGORITHM.ToStringType(param_00_algorithm);}
        #endregion
        
       #region croodinate related functions
        public PointF GetCenter()
        {
            float minX = Math.Min(rc_FST.MIN_X, rc_SCD.MIN_X);
            float maxX = Math.Max(rc_FST.MAX_X, rc_SCD.MAX_X);
            float minY = Math.Min(rc_FST.MIN_Y, rc_SCD.MIN_Y);
            float maxY = Math.Max(rc_FST.MAX_Y, rc_SCD.MAX_Y);

            float cx = Convert.ToSingle(minX + ((maxX - minX) / 2.0));
            float cy = Convert.ToSingle(minY + ((maxY - minY) / 2.0));

            return new PointF(cx, cy);
        }
        public PointF GetCenter(parseRect rc1, parseRect rc2)
        {
            float minX = Math.Min(rc1.MIN_X, rc2.MIN_X);
            float maxX = Math.Max(rc1.MAX_X, rc2.MAX_X);
            float minY = Math.Min(rc1.MIN_Y, rc2.MIN_Y);
            float maxY = Math.Max(rc1.MAX_Y, rc2.MAX_Y);

            float cx = Convert.ToSingle(minX + ((maxX - minX) / 2.0));
            float cy = Convert.ToSingle(minY + ((maxY - minY) / 2.0));

            return new PointF(cx, cy);
        }

        public void ApplyAbsoluteRotation(int nAngle) // only for increase 1 angle [Popup window]
        {
            ANGLE += nAngle;
            PointF ptCenter = GetCenter();
            rc_FST.RotatePoint(ptCenter, nAngle);
            rc_SCD.RotatePoint(ptCenter, nAngle);
        }

        public RectangleF GetRectOrigin_FST() // for drawing 170621 
        {
            parseRect rc = new parseRect();

            //if ( ptOrientCurr.X == 0 && ptOrientCurr.Y == 0)  // absolute croodinate case 
            //{
            //    PointF ptCenter = GetCenter();
            //
            //    rc = new parseRect(rc_FST);         // get normal croodinate ( + cartesian )
            //    rc.RotatePoint(ptCenter, -angle);
            //}
            //else if ( ptOrientCurr.X != 0 && ptOrientCurr.Y != 0) // relative croodinate case 
            //{
            //    parseRect rcFST = GetCompensatedRect_FST(); // get compensated croodinate ( -+ cartesian )
            //    parseRect rcSCD = GetCompensatedRect_SCD();
            //
            //    PointF ptCenter = GetCenter(rcFST, rcSCD); // get rotation gravity
            //
            //    rcFST.RotatePoint(ptCenter, -angle); // compensate angle 
            //
            //    rc = rcFST.CopyTo(); // make duplica
            //}
            
            return new RectangleF(rc.LT.X, rc.LT.Y, (float)rc.Width, (float)rc.Height);
        }
        public RectangleF GetRectOrigin_SCD() // modified basedon GetRectOrign_FST() 170621 
        {
            parseRect rc = new parseRect();

            //if (ptOrientCurr.X == 0 && ptOrientCurr.Y == 0)  // absolute croodinate case 
            //{
            //    PointF ptCenter = GetCenter();
            //
            //    rc = new parseRect(rc_SCD);         // get normal croodinate ( + cartesian )
            //    rc.RotatePoint(ptCenter, -angle);
            //}
            //else if (ptOrientCurr.X != 0 && ptOrientCurr.Y != 0) // relative croodinate case 
            //{
            //    parseRect rcFST = GetCompensatedRect_FST(); // get compensated croodinate ( -+ cartesian )
            //    parseRect rcSCD = GetCompensatedRect_SCD();
            //
            //    PointF ptCenter = GetCenter(rcFST, rcSCD); // get rotation gravity
            //
            //    rcSCD.RotatePoint(ptCenter, -angle); // compensate angle 
            //
            //    rc = rcSCD.CopyTo(); // make duplica
            //}

            return new RectangleF(rc.LT.X, rc.LT.Y, (float)rc.Width, (float)rc.Height);
        }

        public override void CroodinateBackup(){_rc_FST = rc_FST.CopyTo();_rc_SCD = rc_SCD.CopyTo();}
        public override void CroodinateRecover(){rc_FST = _rc_FST.CopyTo();rc_SCD = _rc_SCD.CopyTo();}
        public override void SetRelativeMovement(PointF ptDelta)
        {
            CroodinateRecover();
            rc_FST.OffsetRect(ptDelta);
            rc_SCD.OffsetRect(ptDelta);
        }

         
        #endregion

        public void ConvertRectangleType(int nPrevious, int nCurrent)
        {
            double rcW = this.rc_FST.Width;
            double rcH = this.rc_FST.Height;
            double rcA = this.ANGLE;

            if (nPrevious == IFX_RECT_TYPE.DIR_HOR && nCurrent == IFX_RECT_TYPE.DIR_VER)
            {
                ApplyAbsoluteRotation(-90);

                this.rc_FST = parseRect.ConvHor2Ver(this.rc_FST);
                this.rc_SCD = parseRect.ConvHor2Ver(this.rc_SCD);

                //// after rotation element orders are changed.  So. swapping.
                //parseRect temp = this.rc_FST.CopyTo();
                //this.rc_FST = this.rc_SCD.CopyTo();
                //this.rc_SCD = temp.CopyTo();

                this.ANGLE = 0;
            }
            else if (nPrevious == IFX_RECT_TYPE.DIR_VER && nCurrent == IFX_RECT_TYPE.DIR_HOR)
            {
                ApplyAbsoluteRotation(90);

                this.rc_FST = parseRect.ConvHor2Ver(this.rc_FST);
                this.rc_SCD = parseRect.ConvHor2Ver(this.rc_SCD);
                this.ANGLE = 0;
            }
            else if (nPrevious == IFX_RECT_TYPE.DIR_HOR && nCurrent == IFX_RECT_TYPE.DIR_DIA)
            {
                ApplyAbsoluteRotation(-90);
                ANGLE = 0;
                this.rc_FST = parseRect.ConvHor2Ver(this.rc_FST);
                this.rc_SCD = parseRect.ConvHor2Ver(this.rc_SCD);

                ApplyAbsoluteRotation(45);
            }
            else if( nPrevious == IFX_RECT_TYPE.DIR_VER && nCurrent == IFX_RECT_TYPE.DIR_DIA)
            {
                ApplyAbsoluteRotation(45);
                // Nothing to do
            }
            else if (nPrevious == IFX_RECT_TYPE.DIR_DIA && nCurrent == IFX_RECT_TYPE.DIR_HOR)
            {
                int nAngle = ANGLE;
                ApplyAbsoluteRotation(-nAngle);
                ANGLE = 0;
                ApplyAbsoluteRotation(90);

                this.rc_FST = parseRect.ConvHor2Ver(this.rc_FST);
                this.rc_SCD = parseRect.ConvHor2Ver(this.rc_SCD);
                this.ANGLE = 0;

            }
            else if (nPrevious == IFX_RECT_TYPE.DIR_DIA && nCurrent == IFX_RECT_TYPE.DIR_VER)
            {
                int nAngle = ANGLE;
                ApplyAbsoluteRotation(-nAngle);
                // Nothing To do
            }
        }


        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        public override float rape_Pussy_Cardin(byte[] rawImage, int imageW, int imageH, 
        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
            ref List<PointF> listEdges_FEX,ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,ref List<PointF> listEdges_SMD,ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
         
            //********************************************************************************************

            bool bVerify = false;

            if (bVerify == true)
            {
                Computer.CVerifier(rawImage, imageW, imageH);
            }

          #region PRE-PROCESS

            parseRect rcFST = this.rc_FST;
            parseRect rcSCD = this.rc_SCD;

            RectangleF rcfFST = rcFST.ToRectangleF();
            RectangleF rcfSCD = rcSCD.ToRectangleF();
            RectangleF rcMerged = CRect.GetBoundingRect(rcfFST, rcfSCD);

            byte[] rawBackup = new byte[imageW * imageH];
            Array.Copy(rawImage, rawBackup, rawImage.Length);

            RectangleF rcInflate = rcMerged;
            rcInflate.Inflate(rcInflate.Width / 2, rcInflate.Height / 2);
            rawImage = DoPreProcess(rawImage, imageW, imageH, SIGMA, KERNEL, Rectangle.Round(rcInflate));
            rawImage = DoSPCProcess(rawImage, imageW, imageH, rcInflate, param_comm_03_spc_enhance);

            rcEstimated = new RectangleF();
            rcMeasured = new RectangleF();

          #endregion

            //********************************************************************************************
            CModelLine model_fex = new CModelLine();
            CModelLine model_fmd = new CModelLine();
            CModelLine model_fin = new CModelLine();

            CModelLine model_sex = new CModelLine();
            CModelLine model_smd = new CModelLine();
            CModelLine model_sin = new CModelLine();

            double fDistance = 0;
            p1 = p2 = new PointF(0, 0);

            if (CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), rcFST.ToRectangleF()) == false ||
              CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), rcSCD.ToRectangleF()) == false) {return -444; }

            if (this.RC_TYPE == IFX_RECT_TYPE.DIR_HOR)
            {
                #region HORIZONTAL
                int rccW = (int)rcMerged.Width;
                int rccH = (int)rcMerged.Height;
                byte[] rawCropFucker = Computer.HC_CropImage(rawBackup, imageW, imageH, (int)rcMerged.X, (int)rcMerged.Y, rccW, rccH);

                RectangleF rcBuff_FST = rcFST.ToRectangleF();
                RectangleF rcBuff_SCD = rcSCD.ToRectangleF();

                RectangleF rectFirst = rcFST.ToRectangleF();
                RectangleF rectSecon = rcSCD.ToRectangleF();

                if ( param_01_bool_Use_AutoDetection == true )
                {
                    CPeakPair peakData = new CPeakPair();
                    CPeakPair.PeakPair single = new CPeakPair.PeakPair();

                    //*****************************************************************************
                    // Part 1  

                    pm.SetImage(rawCropFucker, rccW, rccH);
                    peakData = pm.GenAutoPeakData(true, true, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_02_peakTargetIndex_fst);

                    RectangleF rectFF = pm.GetRegionByPeakAnalysis(single, 1, true);
                    RectangleF rectFS = pm.GetRegionByPeakAnalysis(single, 2, true);
                    rectFF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectFS.Offset(rcBuff_FST.X, rcBuff_FST.Y);

                    //*****************************************************************************
                    // Part 2 

                    pm.SetImage(rawCropFucker, rccW, rccH);

                    peakData = pm.GenAutoPeakData(true, true, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_03_peakTargetIndex_scd);

                    RectangleF rectSF = pm.GetRegionByPeakAnalysis(single, 1, true);
                    RectangleF rectSS = pm.GetRegionByPeakAnalysis(single, 2, true);
                    rectSF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectSS.Offset(rcBuff_FST.X, rcBuff_FST.Y);

                    listEdges_FEX = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rectFS, true, +1);
                    listEdges_FIN = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rectFF, true, -1);
                    listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, true);

                    listEdges_SEX = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rectSF, false, +1);
                    listEdges_SIN = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rectSS, false, -1);
                    listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, true);
                 }
                else if( param_01_bool_Use_AutoDetection == false)
                {
                    Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_HOR(rawImage, imageW, imageH, rectFirst, true, listEdges_FIN, listEdges_FMD, listEdges_FEX);
                    Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_HOR(rawImage, imageW, imageH, rectSecon, false, listEdges_SIN, listEdges_SMD, listEdges_SEX);
                }

                Refinement_Process(null, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(null, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);

                Fit_Lines_ransac(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);


                // 1이 0 좌표계로간다. --> inner vs outter가 뒤집힌다. 위가 작으니까..
                p1 = CRansac.GetMidPointY_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst); 
                p2 = CRansac.GetMidPointY_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                p1 = new PointF(CRect.GetCenter(rcMerged).X, p1.Y);
                p2 = new PointF(CRect.GetCenter(rcMerged).X, p2.Y);

                fDistance = CPoint.GetDistance_Only_Y(p1, p2);

                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rcFST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rcSCD);
                }
#endregion
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_VER )
            {
                #region VERTICAL

                int rccW = (int)rcMerged.Width;
                int rccH = (int)rcMerged.Height;
                byte[] rawCropFucker = Computer.HC_CropImage(rawBackup, imageW, imageH, (int)rcMerged.X, (int)rcMerged.Y, rccW, rccH);

                RectangleF rcBuff_FST = rcFST.ToRectangleF();
                RectangleF rcBuff_SCD = rcSCD.ToRectangleF();

                RectangleF rectFirst = rcFST.ToRectangleF();
                RectangleF rectSecon = rcSCD.ToRectangleF();

                if ( param_01_bool_Use_AutoDetection == true)
                {
                    #region AUTOPEAK
                    CPeakPair peakData = new CPeakPair();
                     CPeakPair.PeakPair single = new CPeakPair.PeakPair();

                    //*****************************************************************************
                    // Part 1 

                    pm.SetImage(rawCropFucker, rccW, rccH);
                    peakData = pm.GenAutoPeakData(true, false, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_02_peakTargetIndex_fst);

                    RectangleF rectFF = pm.GetRegionByPeakAnalysis(single, 1, false);
                    RectangleF rectFS = pm.GetRegionByPeakAnalysis(single, 2, false);
                    rectFF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectFS.Offset(rcBuff_FST.X, rcBuff_FST.Y);

                    //*****************************************************************************
                    // Part 2 
                
                    pm.SetImage(rawCropFucker, rccW, rccH);

                    peakData = pm.GenAutoPeakData(true, false, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_03_peakTargetIndex_scd);

                    RectangleF rectSF = pm.GetRegionByPeakAnalysis(single, 1, false);
                    RectangleF rectSS = pm.GetRegionByPeakAnalysis(single, 2, false);
                    rectSF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectSS.Offset(rcBuff_FST.X, rcBuff_FST.Y);


                    listEdges_FEX = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rectFS/* Changed */, false, 1); // rectengle change for option difference 
                    listEdges_FIN = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rectFF/* Changed */, false, -1); // rectengle change for option difference 
                    listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, false);

                    listEdges_SEX = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rectSF, true, 1);
                    listEdges_SIN = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rectSS, true, -1);
                    listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, false);
                    #endregion
                }
                else if (param_01_bool_Use_AutoDetection == false)
                {
                    Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_VER(rawImage, imageW, imageH, rectFirst, listEdges_FIN, listEdges_FMD, listEdges_FEX);
                    Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_VER(rawImage, imageW, imageH, rectSecon, listEdges_SIN, listEdges_SMD, listEdges_SEX);
                }

                Refinement_Process(null, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(null, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);

                Fit_Lines_ransac(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);


                // 1이 0 좌표계로간다. --> inner vs outter가 뒤집힌다. 위가 작으니까..
                p1 = CRansac.GetMidPointX_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointX_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_X(p1, p2);

                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rc_FST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rc_SCD);
                }
#endregion
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
              #region PAIR_DIA

                //********************************************************
                // Get base axis for each digonal rectangle points 

                listEdges_FEX = CLine.GetLyingPointsFromVariationY(rcFST.LT, rcFST.LB);
                listEdges_SEX = CLine.GetLyingPointsFromVariationY(rcSCD.LT, rcSCD.LB);

                List<PointF> listFirst2D = Computer.GetRotateRegionaldCroodinates(listEdges_FEX, (int)rcFST.Width, ANGLE);
                List<PointF> listSecon2D = Computer.GetRotateRegionaldCroodinates(listEdges_SEX, (int)rcSCD.Width, ANGLE);

                bool bFixedPos = param_comm_04_refinement == -1 ? true : false;

                List<PointF> listGravityFST = Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_DIA(rawImage, imageW, imageH, listFirst2D, listFirst2D.Count / listEdges_FEX.Count, listEdges_FEX.Count, false, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, bFixedPos, 0 );
                List<PointF> listGravitySCD = Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_DIA(rawImage, imageW, imageH, listSecon2D, listSecon2D.Count / listEdges_SEX.Count, listEdges_SEX.Count, true, ref listEdges_SIN, ref listEdges_SMD, ref listEdges_SEX, bFixedPos, 1);

                Refinement_Process(listGravityFST, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(listGravitySCD, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);

                Fit_Lines_ransac(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);

                GetDigonalCrossPoints(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, rc_FST, model_fex, model_fmd, model_fin, out p1);
                GetDigonalCrossPoints(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, rc_SCD, model_sex, model_smd, model_sin, out p2);
                 
                fDistance = CPoint.GetAbsoluteDistance(p1, p2);

                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rc_FST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rc_SCD);
                }
                #endregion
            }
           
            return Convert.ToSingle(fDistance);
        }

        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        public override float rape_Bitch_Direction(byte[] rawImage, int imageW, int imageH,
        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
            ref List<PointF> listEdges_FEX,ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,ref List<PointF> listEdges_SMD,ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
           
            //********************************************************************************************
            
          #region PRE-PROCESS

            parseRect rcFST = this.rc_FST;
            parseRect rcSCD = this.rc_SCD;

            RectangleF rcfFST = rcFST.ToRectangleF();
            RectangleF rcfSCD = rcSCD.ToRectangleF();
            RectangleF rcMerged = CRect.GetBoundingRect(rcfFST, rcfSCD);

            byte[] rawBackup = new byte[imageW * imageH];
            Array.Copy(rawImage, rawBackup, rawImage.Length);

            RectangleF rcInflate = rcMerged;
            rcInflate.Inflate(rcInflate.Width / 2, rcInflate.Height / 2);
            rawImage = DoPreProcess(rawImage, imageW, imageH, SIGMA, KERNEL, Rectangle.Round(rcInflate));
            rawImage = DoSPCProcess(rawImage, imageW, imageH, rcInflate, param_comm_03_spc_enhance);

            rcEstimated = new RectangleF();
            rcMeasured = new RectangleF();
            #endregion 

            //********************************************************************************************

            double fDistance = 0;

            // set default value 170720
            p1 = p2 = new PointF(0, 0);

            //********************************************************
            // Get base axis for each digonal rectangle points 

            CModelLine model_fex = new CModelLine();
            CModelLine model_fmd = new CModelLine();
            CModelLine model_fin = new CModelLine();

            CModelLine model_sex = new CModelLine();
            CModelLine model_smd = new CModelLine();
            CModelLine model_sin = new CModelLine();

            if (CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), rcFST.ToRectangleF()) == false ||
                CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), rcSCD.ToRectangleF()) == false)
            {
                return -444;
            }
            
            if (RC_TYPE == IFX_RECT_TYPE.DIR_HOR)
            {
                #region HORIZONTAL
                int rccW = (int)rcMerged.Width;
                int rccH = (int)rcMerged.Height;
                byte[] rawCropFucker = Computer.HC_CropImage(rawBackup, imageW, imageH, (int)rcMerged.X, (int)rcMerged.Y, rccW, rccH);

                RectangleF rcBuff_FST = rcFST.ToRectangleF();
                RectangleF rcBuff_SCD = rcSCD.ToRectangleF();

                RectangleF rectFirst = rcFST.ToRectangleF();
                RectangleF rectSecon = rcSCD.ToRectangleF();

                if ( param_01_bool_Use_AutoDetection == true)
                {
                    #region use_auto_detection
                    CPeakPair peakData = new CPeakPair();
                    CPeakPair.PeakPair single = new CPeakPair.PeakPair();

                    //*****************************************************************************
                    // Part 1

                    pm.SetImage(rawCropFucker, rccW, rccH);
                    peakData = pm.GenAutoPeakData(true, true, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_02_peakTargetIndex_fst);

                    RectangleF rectFF = pm.GetRegionByPeakAnalysis(single, 1, true);
                    RectangleF rectFS = pm.GetRegionByPeakAnalysis(single, 2, true);
                    rectFF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectFS.Offset(rcBuff_FST.X, rcBuff_FST.Y);


                    //*****************************************************************************
                    // Part 2

                    pm.SetImage(rawCropFucker, rccW, rccH);

                    peakData = pm.GenAutoPeakData(true, true, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_03_peakTargetIndex_scd);

                    RectangleF rectSF = pm.GetRegionByPeakAnalysis(single, 1, true);
                    RectangleF rectSS = pm.GetRegionByPeakAnalysis(single, 2, true);
                    rectSF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectSS.Offset(rcBuff_FST.X, rcBuff_FST.Y);


                    /***/
                    if (this.param_00_algorithm == IFX_ALGORITHM.DIR_IN)
                    {
                        listEdges_FIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectFS, false, +1);
                        listEdges_FEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectFF, false, -1);
                        listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, true);

                        listEdges_SIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectSF, true, +1);
                        listEdges_SEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectSS, true, -1);
                        listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, true);
                    }
                    else if (this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                    {
                        listEdges_FEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectFS, true, +1);
                        listEdges_FIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectFF, true, -1);
                        listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, true);

                        listEdges_SEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectSF, false, +1);
                        listEdges_SIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectSS, false, -1);
                        listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, true);

                    }
                    #endregion
                }
                else if (param_01_bool_Use_AutoDetection == false)
                {
                    /***/if (this.param_00_algorithm == IFX_ALGORITHM.DIR_IN)
                    {
                        Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_VER(rawImage, imageW, imageH, rectFirst, false, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                        Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_VER(rawImage, imageW, imageH, rectSecon, true, listEdges_SEX, listEdges_SMD, listEdges_SIN);
                    }
                    else if (this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                    {
                        Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_VER(rawImage, imageW, imageH, rectFirst, true, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                        Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_VER(rawImage, imageW, imageH, rectSecon, false, listEdges_SEX, listEdges_SMD, listEdges_SIN);
                    }
                }

                Refinement_Process(null, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(null, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);

                Fit_Lines_ransac(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);


                // 1이 0 좌표계로간다. --> inner vs outter가 뒤집힌다. 위가 작으니까..
                p1 = CRansac.GetMidPointY_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointY_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                p1 = new PointF(CRect.GetCenter(rcMerged).X, p1.Y);
                p2 = new PointF(CRect.GetCenter(rcMerged).X, p2.Y);

                fDistance = CPoint.GetDistance_Only_Y(p1, p2);

                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rcFST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rcSCD);
                }
                #endregion
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_VER )
            {
                #region VERTICAL
                int rccW = (int)rcMerged.Width;
                int rccH = (int)rcMerged.Height;
                byte[] rawCropFucker = Computer.HC_CropImage(rawBackup, imageW, imageH, (int)rcMerged.X, (int)rcMerged.Y, rccW, rccH);

                RectangleF rcBuff_FST = rcFST.ToRectangleF();
                RectangleF rcBuff_SCD = rcSCD.ToRectangleF();

                RectangleF rectFirst = rcFST.ToRectangleF();
                RectangleF rectSecon = rcSCD.ToRectangleF();

                if (param_01_bool_Use_AutoDetection == true)
                {
                    #region use_auto_detection
                    CPeakPair peakData = new CPeakPair();
                    CPeakPair.PeakPair single = new CPeakPair.PeakPair();

                    //*****************************************************************************
                    // Part 1 : Negative

                    pm.SetImage(rawCropFucker, rccW, rccH);
                    peakData = pm.GenAutoPeakData(true, false, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_02_peakTargetIndex_fst);

                    RectangleF rectFF = pm.GetRegionByPeakAnalysis(single, 1, false);
                    RectangleF rectFS = pm.GetRegionByPeakAnalysis(single, 2, false);
                    rectFF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectFS.Offset(rcBuff_FST.X, rcBuff_FST.Y);

                    //*****************************************************************************
                    // Part 2 : Possitive 

                    pm.SetImage(rawCropFucker, rccW, rccH);
                    peakData = pm.GenAutoPeakData(true, false, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_03_peakTargetIndex_scd);

                    RectangleF rectSF = pm.GetRegionByPeakAnalysis(single, 1, false);
                    RectangleF rectSS = pm.GetRegionByPeakAnalysis(single, 2, false);
                    rectSF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectSS.Offset(rcBuff_FST.X, rcBuff_FST.Y);

                    /***/
                    if (this.param_00_algorithm == IFX_ALGORITHM.DIR_IN)
                    {
                        listEdges_FIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectFS/* Changed */, false, 1); // rectengle change for option difference 
                        listEdges_FEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectFF/* Changed */, false, -1); // rectengle change for option difference 
                        listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, false);

                        listEdges_SIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectSF, true, 1);
                        listEdges_SEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectSS, true, -1);
                        listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, false);
                    }
                    else if (this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                    {
                        listEdges_FEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectFS/* Changed */, true, 1); // rectengle change for option difference 
                        listEdges_FIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectFF/* Changed */, true, -1); // rectengle change for option difference 
                        listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, false);

                        listEdges_SEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectSF, false, 1);
                        listEdges_SIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectSS, false, -1);
                        listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, false);
                    }
                    #endregion
                }
                else if (param_01_bool_Use_AutoDetection == false)
                {
                    /***/if (this.param_00_algorithm == IFX_ALGORITHM.DIR_IN)
                    {
                        Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_HOR(rawImage, imageW, imageH, rectFirst, false, listEdges_FIN, listEdges_FMD, listEdges_FEX);
                        Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_HOR(rawImage, imageW, imageH, rectSecon, true, listEdges_SIN, listEdges_SMD, listEdges_SEX);

                    }
                    else if (this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                    {
                        Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_HOR(rawImage, imageW, imageH, rectFirst, true, listEdges_FIN, listEdges_FMD, listEdges_FEX);
                        Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_HOR(rawImage, imageW, imageH, rectSecon, false, listEdges_SIN, listEdges_SMD, listEdges_SEX);
                    }
                }

                Refinement_Process(null, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(null, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);
                
                Fit_Lines_ransac(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);

               

                p1 = CRansac.GetMidPointX_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointX_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_X(p1, p2);
                
                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rc_FST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rc_SCD);
                }
                #endregion
            }
            else if (RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
              #region DIAGONAL
                listEdges_FEX = CLine.GetLyingPointsFromVariationY(rcFST.LT, rcFST.LB);
                listEdges_SEX = CLine.GetLyingPointsFromVariationY(rcSCD.LT, rcSCD.LB);

                List<PointF> listFirst2D = Computer.GetRotateRegionaldCroodinates(listEdges_FEX, (int)rcFST.Width, ANGLE);
                List<PointF> listSecon2D = Computer.GetRotateRegionaldCroodinates(listEdges_SEX, (int)rcSCD.Width, ANGLE);

                List<PointF> listGravityFST = new List<PointF>();
                List<PointF> listGravitySCD = new List<PointF>();

                bool bFixedPos = param_comm_04_refinement == -1 ? true : false;

                if( this.param_00_algorithm == IFX_ALGORITHM.DIR_IN ) 
                {
                    listGravityFST = Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_DIA(rawImage, imageW, imageH, listFirst2D, listFirst2D.Count / listEdges_FEX.Count, listEdges_FEX.Count, false, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, bFixedPos);
                    listGravitySCD = Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_DIA(rawImage, imageW, imageH, listSecon2D, listSecon2D.Count / listEdges_SEX.Count, listEdges_SEX.Count, true, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, bFixedPos);
                }
                else if( this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                {

                    listGravityFST = Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_DIA(rawImage, imageW, imageH, listFirst2D, listFirst2D.Count / listEdges_FEX.Count, listEdges_FEX.Count, true, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, bFixedPos);
                    listGravitySCD = Computer.HC_EDGE_GetRawPoints_1stDeriv_MULTI_DIA(rawImage, imageW, imageH, listSecon2D, listSecon2D.Count / listEdges_SEX.Count, listEdges_SEX.Count, false, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, bFixedPos);
                }

                Refinement_Process(listGravityFST, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(listGravitySCD, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);
                
                Fit_Lines_ransac( ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac( ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);

                GetDigonalCrossPoints(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, rc_FST, model_fex, model_fmd, model_fin, out p1);
                GetDigonalCrossPoints(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, rc_SCD, model_sex, model_smd, model_sin, out p2);


                fDistance = CPoint.GetAbsoluteDistance(p1, p2);

                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rc_FST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rc_SCD);
                }
                #endregion
            }
 
            return Convert.ToSingle(fDistance);
        }

      
        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        public override float rape_asshole_Log(byte[] rawImage, int imageW, int imageH,
        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
            ref List<PointF> listEdges_FEX,ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,ref List<PointF> listEdges_SMD,ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            // pre-processing 
            //********************************************************************************************

            #region PRE-PROCESS

            parseRect rcFST = this.rc_FST;
            parseRect rcSCD = this.rc_SCD;

            RectangleF rcfFST = rcFST.ToRectangleF();
            RectangleF rcfSCD = rcSCD.ToRectangleF();
            RectangleF rcMerged = CRect.GetBoundingRect(rcfFST, rcfSCD);

            byte[] rawBackup = new byte[imageW * imageH];
            Array.Copy(rawImage, rawBackup, rawImage.Length);

            RectangleF rcInflate = rcMerged;
            rcInflate.Inflate(rcInflate.Width / 2, rcInflate.Height / 2);
            rawImage = DoPreProcess(rawImage, imageW, imageH, SIGMA, KERNEL, Rectangle.Round(rcInflate));
            rawImage = DoSPCProcess(rawImage, imageW, imageH, rcInflate, param_comm_03_spc_enhance);

            rcEstimated = new RectangleF();
            rcMeasured = new RectangleF();
            #endregion 

            //********************************************************************************************
 
            CModelLine model_fex = new CModelLine();
            CModelLine model_fmd = new CModelLine();
            CModelLine model_fin = new CModelLine();

            CModelLine model_sex = new CModelLine();
            CModelLine model_smd = new CModelLine();
            CModelLine model_sin = new CModelLine();


            double fDistance = 0;
            // set default value 170720
            p1 = p2 = new PointF(0, 0);

            if (CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), rcFST.ToRectangleF()) == false ||
                CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), rcSCD.ToRectangleF()) == false)
            {
                return -444;
            }   
         
            if (this.RC_TYPE == IFX_RECT_TYPE.DIR_HOR) // verified 170608
            {
                #region HORIZONTAL
                int rccW = (int)rcMerged.Width;
                int rccH = (int)rcMerged.Height;
                byte[] rawCropFucker = Computer.HC_CropImage(rawBackup, imageW, imageH, (int)rcMerged.X, (int)rcMerged.Y, rccW, rccH);

                RectangleF rcBuff_FST = rcFST.ToRectangleF();
                RectangleF rcBuff_SCD = rcSCD.ToRectangleF();

                RectangleF rectFirst = rcFST.ToRectangleF();
                RectangleF rectSecon = rcSCD.ToRectangleF();

                if (param_01_bool_Use_AutoDetection == true)
                {
                    CPeakPair peakData = new CPeakPair();
                    CPeakPair.PeakPair single = new CPeakPair.PeakPair();

                    //*****************************************************************************
                    // Part 1

                    pm.SetImage(rawCropFucker, rccW, rccH);
                    peakData = pm.GenAutoPeakData(true, true, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_02_peakTargetIndex_fst);

                    RectangleF rectFF = pm.GetRegionByPeakAnalysis(single, 1, true);
                    RectangleF rectFS = pm.GetRegionByPeakAnalysis(single, 2, true);
                    rectFF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectFS.Offset(rcBuff_FST.X, rcBuff_FST.Y);


                    //*****************************************************************************
                    // Part 2

                    pm.SetImage(rawCropFucker, rccW, rccH);

                    peakData = pm.GenAutoPeakData(true, true, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_03_peakTargetIndex_scd);

                    RectangleF rectSF = pm.GetRegionByPeakAnalysis(single, 1, true);
                    RectangleF rectSS = pm.GetRegionByPeakAnalysis(single, 2, true);
                    rectSF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectSS.Offset(rcBuff_FST.X, rcBuff_FST.Y);

                    listEdges_FEX = Computer.HC_EDGE_GetRawPoints_LOG_HOR(rawImage, imageW, imageH, rectFS/* Changed */, false, +1); // rectengle change for option difference 
                    listEdges_FIN = Computer.HC_EDGE_GetRawPoints_LOG_HOR(rawImage, imageW, imageH, rectFF/* Changed */, false, -1); // rectengle change for option difference 
                    listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, true);

                    listEdges_SEX = Computer.HC_EDGE_GetRawPoints_LOG_HOR(rawImage, imageW, imageH, rectSF, false, +1);
                    listEdges_SIN = Computer.HC_EDGE_GetRawPoints_LOG_HOR(rawImage, imageW, imageH, rectSS, false, -1);
                    listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, true);
                }
                else if( param_01_bool_Use_AutoDetection == false )
                {
                    Computer.HC_EDGE_GetRawPoints_LOG_MULTI_HOR(rawImage, imageW, imageH, rectFirst, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                    Computer.HC_EDGE_GetRawPoints_LOG_MULTI_HOR(rawImage, imageW, imageH, rectSecon, listEdges_SIN, listEdges_SMD, listEdges_SEX);
                }

                Refinement_Process(null, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(null, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);

                Fit_Lines_ransac(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);

                // 1이 0 좌표계로간다. --> inner vs outter가 뒤집힌다. 위가 작으니까..
                p1 = CRansac.GetMidPointY_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointY_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                p1 = new PointF(CRect.GetCenter(rcMerged).X, p1.Y);
                p2 = new PointF(CRect.GetCenter(rcMerged).X, p2.Y);

                fDistance = CPoint.GetDistance_Only_Y(p1, p2);

                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rcFST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rcSCD);
                }
                #endregion
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_VER) // verified 170608
            {
                #region  VERTICAL

                int rccW = (int)rcMerged.Width;
                int rccH = (int)rcMerged.Height;
                byte[] rawCropFucker = Computer.HC_CropImage(rawBackup, imageW, imageH, (int)rcMerged.X, (int)rcMerged.Y, rccW, rccH);

                RectangleF rcBuff_FST = rcFST.ToRectangleF();
                RectangleF rcBuff_SCD = rcSCD.ToRectangleF();

                RectangleF rectFirst = rcFST.ToRectangleF();
                RectangleF rectSecon = rcSCD.ToRectangleF();

                if ( param_01_bool_Use_AutoDetection == true)
                {
                    #region AUTO PEAK
                    CPeakPair peakData = new CPeakPair();
                    CPeakPair.PeakPair single = new CPeakPair.PeakPair();

                    //*****************************************************************************
                    // Part 1

                    pm.SetImage(rawCropFucker, rccW, rccH);
                    peakData = pm.GenAutoPeakData(true, false, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_02_peakTargetIndex_fst);

                    RectangleF rectFF = pm.GetRegionByPeakAnalysis(single, 1, false);
                    RectangleF rectFS = pm.GetRegionByPeakAnalysis(single, 2, false);
                    rectFF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectFS.Offset(rcBuff_FST.X, rcBuff_FST.Y);

                    //*****************************************************************************
                    // Part 2

                    pm.SetImage(rawCropFucker, rccW, rccH);
                    peakData = pm.GenAutoPeakData(true, false, param_05_windowSize, param_04_peakCandidate);
                    single = peakData.GetElement(param_03_peakTargetIndex_scd);

                    RectangleF rectSF = pm.GetRegionByPeakAnalysis(single, 1, false);
                    RectangleF rectSS = pm.GetRegionByPeakAnalysis(single, 2, false);
                    rectSF.Offset(rcBuff_FST.X, rcBuff_FST.Y);
                    rectSS.Offset(rcBuff_FST.X, rcBuff_FST.Y);

                    listEdges_FEX = Computer.HC_EDGE_GetRawPoints_LOG_VER(rawImage, imageW, imageH, rectFS/* Changed */, false, 1); // rectengle change for option difference 
                    listEdges_FIN = Computer.HC_EDGE_GetRawPoints_LOG_VER(rawImage, imageW, imageH, rectFF/* Changed */, false, -1); // rectengle change for option difference 
                    listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, false);

                    listEdges_SEX = Computer.HC_EDGE_GetRawPoints_LOG_VER(rawImage, imageW, imageH, rectSF, true, 1);
                    listEdges_SIN = Computer.HC_EDGE_GetRawPoints_LOG_VER(rawImage, imageW, imageH, rectSS, true, -1);
                    listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, false);
                    #endregion
                }
                else if (param_01_bool_Use_AutoDetection == false)
                {
                    Computer.HC_EDGE_GetRawPoints_LOG_MULTI_VER(rawImage, imageW, imageH, rectFirst, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                    Computer.HC_EDGE_GetRawPoints_LOG_MULTI_VER(rawImage, imageW, imageH, rectSecon, listEdges_SIN, listEdges_SMD, listEdges_SEX);
                }

                Refinement_Process(null, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(null, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);

                Fit_Lines_ransac(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);


                p1 = CRansac.GetMidPointX_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointX_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_X(p1, p2);

                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rc_FST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rc_SCD);
                }
                #endregion
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
              #region Diagonal 
                listEdges_FEX = CLine.GetLyingPointsFromVariationY(rcFST.LT, rcFST.LB);
                listEdges_SEX = CLine.GetLyingPointsFromVariationY(rcSCD.LT, rcSCD.LB);

                List<PointF> listFirst2D = Computer.GetRotateRegionaldCroodinates(listEdges_FEX, (int)rcFST.Width, ANGLE);
                List<PointF> listSecon2D = Computer.GetRotateRegionaldCroodinates(listEdges_SEX, (int)rcSCD.Width, ANGLE);

                bool bFixedPos = param_comm_04_refinement == -1 ? true : false;

                List<PointF> listGravityFST = Computer.HC_EDGE_GetRawPoints_LOG_MULTI_DIA(rawImage, imageW, imageH, listFirst2D, listFirst2D.Count / listEdges_FEX.Count, listEdges_FEX.Count, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, bFixedPos);
                List<PointF> listGravitySCD = Computer.HC_EDGE_GetRawPoints_LOG_MULTI_DIA(rawImage, imageW, imageH, listSecon2D, listSecon2D.Count / listEdges_SEX.Count, listEdges_SEX.Count, ref listEdges_SIN, ref listEdges_SMD, ref listEdges_SEX, bFixedPos);

                Refinement_Process(listGravityFST, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN);
                Refinement_Process(listGravitySCD, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN);

                Fit_Lines_ransac(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref model_fex, ref model_fmd, ref model_fin);
                Fit_Lines_ransac(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, ref model_sex, ref model_smd, ref model_sin);

                GetDigonalCrossPoints(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, rc_FST, model_fex, model_fmd, model_fin, out p1);
                GetDigonalCrossPoints(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, rc_SCD, model_sex, model_smd, model_sin, out p2);
              
                fDistance = CPoint.GetAbsoluteDistance(p1, p2);

                if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
                {
                    MakeResultForRect(ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, model_fex, model_fmd, model_fin, rc_FST);
                    MakeResultForRect(ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, model_sex, model_smd, model_sin, rc_SCD);
                }
              #endregion

            }
            return Convert.ToSingle(fDistance);
        }

        public override float MeasureData(byte[] rawImage, int imageW, int imageH, ref List<PointF> listEdges_FEX,ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,ref List<PointF> listEdges_SMD,ref List<PointF> listEdges_SIN, out PointF P1, out PointF P2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            #region EMPTY_IMPEMEMENTATION
            double fDistance = 0;
            // Set default value 170720
            P1 = P2 = new PointF(0, 0);
            rcEstimated = new RectangleF();
            rcMeasured = new RectangleF();

            /***/if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN)) 
            {
                fDistance = this.rape_Pussy_Cardin(rawImage, imageW, imageH, 
                    ref listEdges_FEX,ref listEdges_FMD,ref listEdges_FIN,
                    ref listEdges_SEX,ref listEdges_SMD,ref listEdges_SIN,
                    out P1, out P2, out rcEstimated, out rcMeasured);
            }
            else if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_IN)||
                     this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_EX)) 
            {
                fDistance = this.rape_Bitch_Direction(rawImage, imageW, imageH,
                    ref listEdges_FEX,ref listEdges_FMD,ref listEdges_FIN,
                    ref listEdges_SEX,ref listEdges_SMD,ref listEdges_SIN,
                    out P1, out P2, out rcEstimated, out rcMeasured); 
            }
            else if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.MEXHAT)) 
            {
                fDistance = this.rape_asshole_Log(rawImage, imageW, imageH,
                    ref listEdges_FEX,ref listEdges_FMD,ref listEdges_FIN,
                    ref listEdges_SEX,ref listEdges_SMD,ref listEdges_SIN,
                    out P1, out P2, out rcEstimated, out rcMeasured); 
            }

            fDistance *= PIXEL_RES;
            fDistance *= this.param_comm_01_compen_A;
            fDistance += this.param_comm_02_compen_B;

            return Convert.ToSingle(fDistance);
            #endregion
        }

        private void Fit_Lines_ransac(ref List<PointF> listEdges_EX, ref List<PointF> listEdges_MD, ref List<PointF> listEdges_IN,
          ref CModelLine model_ex, ref CModelLine model_md, ref CModelLine model_in)
        {
            CRansac.ransac_Line_fitting(listEdges_EX.ToArray(), ref model_ex, param_comm_fitting_thr, listEdges_EX.Count / 3, listEdges_EX.Count * 10);
            CRansac.ransac_Line_fitting(listEdges_MD.ToArray(), ref model_md, param_comm_fitting_thr, listEdges_MD.Count / 3, listEdges_MD.Count * 10);
            CRansac.ransac_Line_fitting(listEdges_IN.ToArray(), ref model_in, param_comm_fitting_thr, listEdges_IN.Count / 3, listEdges_IN.Count * 10);
        }
        private void GetDigonalCrossPoints(ref List<PointF> listEdges_EX, ref List<PointF> listEdges_MD, ref List<PointF> listEdges_IN,
            parseRect parseRC_Original, CModelLine model_ex, CModelLine model_md, CModelLine model_in,
            out PointF ptCross)
        {
            #region
            // get the original digonal line 
            CLine lineEX = CRansac.GetFittedLine_VER(model_ex, parseRC_Original.ToRectangleF());
            CLine lineMD = CRansac.GetFittedLine_VER(model_md, parseRC_Original.ToRectangleF());
            CLine lineIN = CRansac.GetFittedLine_VER(model_in, parseRC_Original.ToRectangleF());

            // get the cross line 
            CLine line_Cut = new CLine(new CLine(parseRC_Original.LT, parseRC_Original.LB).CENTER, new CLine(parseRC_Original.RT, parseRC_Original.RB).CENTER);

            // get the cross point

            PointF ptCrossEX = CLine.GetCrossPointOfTwoLines(lineEX, line_Cut);
            PointF ptCrossMD = CLine.GetCrossPointOfTwoLines(lineMD, line_Cut);
            PointF ptCrossIN = CLine.GetCrossPointOfTwoLines(lineIN, line_Cut);

            PointF p1 = new PointF(); // temporary buffer
            /***/
            if (param_06_edge_position_fst == 0.0) { p1 = ptCrossIN; }
            else if (param_06_edge_position_fst == 0.5) { p1 = ptCrossMD; }
            else if (param_06_edge_position_fst == 1.0) { p1 = ptCrossEX; }
            else/*************************************/ { p1 = ptCrossIN; }

            ptCross = p1;
            #endregion
        }
        private void Refinement_Process(List<PointF> listGravity, ref List<PointF> listEdges_EX, ref List<PointF> listEdges_MD, ref List<PointF> listEdges_IN)
        {
            if (RC_TYPE == IFX_RECT_TYPE.DIR_HOR)
            {
                if (param_comm_04_refinement != 0)
                {
                    listEdges_EX = Computer.GetList_FilterBy_MajorDistance(listEdges_EX, false, param_comm_04_refinement);
                    listEdges_MD = Computer.GetList_FilterBy_MajorDistance(listEdges_MD, false, param_comm_04_refinement);
                    listEdges_IN = Computer.GetList_FilterBy_MajorDistance(listEdges_IN, false, param_comm_04_refinement);
                }
            }
            else if (RC_TYPE == IFX_RECT_TYPE.DIR_VER)
            {
                if (param_comm_04_refinement != 0)
                {
                    listEdges_EX = Computer.GetList_FilterBy_MajorDistance(listEdges_EX, true, param_comm_04_refinement);
                    listEdges_MD = Computer.GetList_FilterBy_MajorDistance(listEdges_MD, true, param_comm_04_refinement);
                    listEdges_IN = Computer.GetList_FilterBy_MajorDistance(listEdges_IN, true, param_comm_04_refinement);
                }
            }
            else if (RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
                if (param_comm_04_refinement > 0)
                {
                    Computer.HC_EDGE_FILTERING_FOR_DIGONAL(listGravity, ref listEdges_EX, ref listEdges_MD, ref listEdges_IN, 3);

                }
            }
        }

        private void MakeResultForRect(ref List<PointF> listEdges_EX, ref List<PointF> listEdges_MD, ref List<PointF> listEdges_IN,
            CModelLine model_ex, CModelLine model_md, CModelLine model_in, parseRect rc)
        {

            if (RC_TYPE == IFX_RECT_TYPE.DIR_HOR)
            {
                listEdges_EX = Computer.ReplacePointList_Absolute_Y(rc.ToRectangleF(), (float)model_ex.sy);
                listEdges_MD = Computer.ReplacePointList_Absolute_Y(rc.ToRectangleF(), (float)model_md.sy);
                listEdges_IN = Computer.ReplacePointList_Absolute_Y(rc.ToRectangleF(), (float)model_in.sy);
            }
            else if (RC_TYPE == IFX_RECT_TYPE.DIR_VER)
            {
                listEdges_EX = Computer.ReplacePointList_Absolute_X(rc.ToRectangleF(), (float)model_ex.sx);
                listEdges_MD = Computer.ReplacePointList_Absolute_X(rc.ToRectangleF(), (float)model_md.sx);
                listEdges_IN = Computer.ReplacePointList_Absolute_X(rc.ToRectangleF(), (float)model_in.sx);
            }
            else if (RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
                CLine lineEX = CRansac.GetFittedLine_VER(model_ex, rc.ToRectangleF());
                CLine lineMD = CRansac.GetFittedLine_VER(model_md, rc.ToRectangleF());
                CLine lineIN = CRansac.GetFittedLine_VER(model_in, rc.ToRectangleF());

                lineEX = lineEX.GetExpandedLine_CrossLineBased(lineEX, rc.LT, rc.RT, rc.LB, rc.RB);
                lineMD = lineMD.GetExpandedLine_CrossLineBased(lineMD, rc.LT, rc.RT, rc.LB, rc.RB);
                lineIN = lineIN.GetExpandedLine_CrossLineBased(lineIN, rc.LT, rc.RT, rc.LB, rc.RB);

                listEdges_EX = CLine.GetLyingPointsFromVariationY(lineEX);
                listEdges_MD = CLine.GetLyingPointsFromVariationY(lineMD);
                listEdges_IN = CLine.GetLyingPointsFromVariationY(lineIN);
            }

        }

    }

    public class CMeasurePairOvl : CMeasureMotherFucker
    {
        private double SIGMA = 1.5;
        private int KERNEL = 7;

        // 이새끼들은 셀프 자가복사본이 있으니까 구지. 뭐
        public CMeasurePairRct RC_VER_IN = new CMeasurePairRct();
        public CMeasurePairRct RC_VER_EX = new CMeasurePairRct();
        public CMeasurePairRct RC_HOR_IN = new CMeasurePairRct();
        public CMeasurePairRct RC_HOR_EX = new CMeasurePairRct();

        public int algorithm_HOR_IN { get; set; }
        public int algorithm_HOR_EX { get; set; }

        public int algorithm_VER_IN { get; set; }
        public int algorithm_VER_EX { get; set; }

        public int ZigZagIN { get; set; }
        public int ZigZagEX { get; set; }

        public double param_01_edge_position_hor_in { get; set; }
        public double param_02_edge_position_hor_ex { get; set; }

        public double param_03_edge_position_ver_in { get; set; }
        public double param_04_edge_position_ver_ex { get; set; }
        
        public CMeasurePairOvl()
        {
            ZigZagEX = ZigZagIN = 0;
            NICKNAME = "OVL";
            algorithm_HOR_IN =  algorithm_HOR_EX =  algorithm_VER_IN = algorithm_VER_EX = 0;
 
            RC_VER_IN.RC_TYPE = IFX_RECT_TYPE.DIR_HOR;
            RC_VER_EX.RC_TYPE = IFX_RECT_TYPE.DIR_HOR;
                                              
            RC_HOR_IN.RC_TYPE = IFX_RECT_TYPE.DIR_VER;
            RC_HOR_EX.RC_TYPE = IFX_RECT_TYPE.DIR_VER;

            param_01_edge_position_hor_in = 0;
            param_02_edge_position_hor_ex = 0;
            
            param_03_edge_position_ver_in = 0;
            param_04_edge_position_ver_ex = 0;

            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_spc_enhance = 0;
            param_comm_05_BOOL_SHOW_RAW_DATA = false;

        }

        public CMeasurePairOvl CopyTo()
        {
            CMeasurePairOvl single = new CMeasurePairOvl();

            single.RC_HOR_IN = this.RC_HOR_IN.CopyTo();
            single.RC_HOR_EX = this.RC_HOR_EX.CopyTo();
            single.RC_VER_IN = this.RC_VER_IN.CopyTo();
            single.RC_VER_EX = this.RC_VER_EX.CopyTo();

            single.algorithm_HOR_IN = this.algorithm_HOR_IN;
            single.algorithm_HOR_EX = this.algorithm_HOR_EX;

            single.algorithm_VER_IN = this.algorithm_VER_IN;
            single.algorithm_VER_EX = this.algorithm_VER_EX;

            single.ZigZagIN = this.ZigZagIN;
            single.ZigZagEX = this.ZigZagEX;

            single.NICKNAME = this.NICKNAME;
            
            single.param_01_edge_position_hor_in  = this.param_01_edge_position_hor_in;
            single.param_02_edge_position_hor_ex  = this.param_02_edge_position_hor_ex;
                                                    
            single.param_03_edge_position_ver_in  = this.param_03_edge_position_ver_in;
            single.param_04_edge_position_ver_ex  = this.param_04_edge_position_ver_ex;

            single.param_comm_01_compen_A = this.param_comm_01_compen_A;
            single.param_comm_02_compen_B = this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance = this.param_comm_03_spc_enhance;
            single.param_comm_04_refinement = this.param_comm_04_refinement;
            single.param_comm_05_BOOL_SHOW_RAW_DATA = this.param_comm_05_BOOL_SHOW_RAW_DATA;

            return single;
        }

        
        public PointF GetCenter()
        {
            RectangleF rcIN_LFT = this.RC_HOR_IN.rc_FST.ToRectangleF();
            RectangleF rcIN_RHT = this.RC_HOR_IN.rc_SCD.ToRectangleF();
            RectangleF rcIN_TOP = this.RC_VER_IN.rc_FST.ToRectangleF();
            RectangleF rcIN_BTM = this.RC_VER_IN.rc_SCD.ToRectangleF();

            float lcx = CRect.GetCenter(rcIN_LFT).X;
            float rcx = CRect.GetCenter(rcIN_RHT).X;
            float tcy = CRect.GetCenter(rcIN_TOP).Y;
            float bcy = CRect.GetCenter(rcIN_BTM).Y;

            float cx = Convert.ToSingle(lcx + ((rcx - lcx) / 2.0));
            float cy = Convert.ToSingle(tcy + ((bcy - tcy) / 2.0));

            return new PointF(cx, cy);
        }

        public override void CroodinateBackup()
        {
            RC_HOR_IN.CroodinateBackup();
            RC_HOR_EX.CroodinateBackup();
            RC_VER_IN.CroodinateBackup();
            RC_VER_EX.CroodinateBackup();
        }
        public override void CroodinateRecover()
        {
            RC_HOR_IN.CroodinateRecover();
            RC_HOR_EX.CroodinateRecover();
            RC_VER_IN.CroodinateRecover();
            RC_VER_EX.CroodinateRecover();
        }
        public override void SetRelativeMovement(PointF ptDelta)
        {
            CroodinateRecover();

            RC_HOR_IN.rc_FST.OffsetRect(ptDelta);
            RC_HOR_IN.rc_SCD.OffsetRect(ptDelta);
            
            RC_HOR_EX.rc_FST.OffsetRect(ptDelta); ;
            RC_HOR_EX.rc_SCD.OffsetRect(ptDelta); ;

            RC_VER_IN.rc_FST.OffsetRect(ptDelta); ;
            RC_VER_IN.rc_SCD.OffsetRect(ptDelta); ;

            RC_VER_EX.rc_FST.OffsetRect(ptDelta); ;
            RC_VER_EX.rc_SCD.OffsetRect(ptDelta); ;
        }


        public override string GetMeasurementCategory()
        {
            return string.Empty;
        }

   
        public override bool VeryfyMeasurementMatching()
        {
            return true;
        }
        public override void AdjustGap(int tx, int ty) { throw new NotImplementedException(); }
        public override void AdjustPos(int tx, int ty) { throw new NotImplementedException(); }
        public override void AdjustSize(int tx, int ty) {throw new NotImplementedException(); }

        public void AdjustPos_IN(int nDir, int nScale)
        {
            float fScale = Convert.ToSingle(nScale);
            #region
            if (nDir == IFX_DIR.DIR_LFT)
            {
                this.RC_HOR_IN.rc_FST.OffsetX(-fScale);
                this.RC_HOR_IN.rc_SCD.OffsetX(-fScale);
                this.RC_VER_IN.rc_FST.OffsetX(-fScale);
                this.RC_VER_IN.rc_SCD.OffsetX(-fScale);
            }
            else if (nDir == IFX_DIR.DIR_TOP)
            {
                this.RC_HOR_IN.rc_FST.OffsetY(-fScale);
                this.RC_HOR_IN.rc_SCD.OffsetY(-fScale);
                this.RC_VER_IN.rc_FST.OffsetY(-fScale);
                this.RC_VER_IN.rc_SCD.OffsetY(-fScale);
            }
            else if (nDir == IFX_DIR.DIR_RHT)
            {
                this.RC_HOR_IN.rc_FST.OffsetX(fScale);
                this.RC_HOR_IN.rc_SCD.OffsetX(fScale);
                this.RC_VER_IN.rc_FST.OffsetX(fScale);
                this.RC_VER_IN.rc_SCD.OffsetX(fScale);
            }
            else if (nDir == IFX_DIR.DIR_BTM)
            {
                this.RC_HOR_IN.rc_FST.OffsetY(fScale);
                this.RC_HOR_IN.rc_SCD.OffsetY(fScale);
                this.RC_VER_IN.rc_FST.OffsetY(fScale);
                this.RC_VER_IN.rc_SCD.OffsetY(fScale);
            }
            #endregion
            CroodinateBackup();
        }
        public void AdjustPos_EX(int nDir, int nScale)
        {
            float fScale = Convert.ToSingle(nScale);
            #region
            if (nDir == IFX_DIR.DIR_LFT)
            {
                this.RC_HOR_EX.rc_FST.OffsetX(-fScale);
                this.RC_HOR_EX.rc_SCD.OffsetX(-fScale);
                this.RC_VER_EX.rc_FST.OffsetX(-fScale);
                this.RC_VER_EX.rc_SCD.OffsetX(-fScale);
            }
            else if (nDir == IFX_DIR.DIR_TOP)
            {
                this.RC_HOR_EX.rc_FST.OffsetY(-fScale);
                this.RC_HOR_EX.rc_SCD.OffsetY(-fScale);
                this.RC_VER_EX.rc_FST.OffsetY(-fScale);
                this.RC_VER_EX.rc_SCD.OffsetY(-fScale);
            }
            else if (nDir == IFX_DIR.DIR_RHT)
            {
                this.RC_HOR_EX.rc_FST.OffsetX(+fScale);
                this.RC_HOR_EX.rc_SCD.OffsetX(+fScale);
                this.RC_VER_EX.rc_FST.OffsetX(+fScale);
                this.RC_VER_EX.rc_SCD.OffsetX(+fScale);
            }
            else if (nDir == IFX_DIR.DIR_BTM)
            {
                this.RC_HOR_EX.rc_FST.OffsetY(+fScale);
                this.RC_HOR_EX.rc_SCD.OffsetY(+fScale);
                this.RC_VER_EX.rc_FST.OffsetY(+fScale);
                this.RC_VER_EX.rc_SCD.OffsetY(+fScale);
            }
            #endregion
            CroodinateBackup();
        }

        public void AdjustGap_IN(int nDir, int nScale)
        {
            float fScale = Convert.ToSingle(nScale / 2.0);
            #region
            if (nDir == IFX_DIR.DIR_LFT)
            {
                this.RC_HOR_IN.rc_FST.OffsetX( +fScale);
                this.RC_HOR_IN.rc_SCD.OffsetX( -fScale);
            }
            else if (nDir == IFX_DIR.DIR_TOP)
            {
                this.RC_VER_IN.rc_FST.OffsetY( +fScale);
                this.RC_VER_IN.rc_SCD.OffsetY( -fScale);
            }
            else if (nDir == IFX_DIR.DIR_RHT)
            {
                this.RC_HOR_IN.rc_FST.OffsetX( -fScale);
                this.RC_HOR_IN.rc_SCD.OffsetX( +fScale);
            }
            else if (nDir == IFX_DIR.DIR_BTM)
            {
                this.RC_VER_IN.rc_FST.OffsetY( -fScale);
                this.RC_VER_IN.rc_SCD.OffsetY( +nScale);
            }
            #endregion
            CroodinateBackup();
        }
        public void AdjustGap_EX(int nDir, int nScale)
        {
            float fScale = Convert.ToSingle(nScale / 2.0);
            #region
            if (nDir == IFX_DIR.DIR_LFT)
            {
                this.RC_HOR_EX.rc_FST.OffsetX(+fScale);
                this.RC_HOR_EX.rc_SCD.OffsetX(-fScale);
            }
            else if (nDir == IFX_DIR.DIR_TOP)
            {
                this.RC_VER_EX.rc_FST.OffsetY(+fScale);
                this.RC_VER_EX.rc_SCD.OffsetY(-fScale);
            }
            else if (nDir == IFX_DIR.DIR_RHT)
            {
                this.RC_HOR_EX.rc_FST.OffsetX(-fScale);
                this.RC_HOR_EX.rc_SCD.OffsetX(+fScale);
            }
            else if (nDir == IFX_DIR.DIR_BTM)
            {
                this.RC_VER_EX.rc_FST.OffsetY(-fScale);
                this.RC_VER_EX.rc_SCD.OffsetY(+nScale);
            }
            #endregion
            CroodinateBackup();
        }

        public void AdjustSize_IN(int nDir, int nScale)
        {
            float fScale = Convert.ToSingle(nScale);
            float fPos = Convert.ToSingle(fScale / 2.0);
            #region
            if (nDir == IFX_DIR.DIR_LFT)
            {
                this.RC_HOR_IN.rc_FST.ScaleY(-fScale);
                this.RC_HOR_IN.rc_SCD.ScaleY(-fScale);
                this.RC_VER_IN.rc_FST.ScaleX(-fScale);
                this.RC_VER_IN.rc_SCD.ScaleX(-fScale);

                this.RC_HOR_IN.rc_FST.OffsetY(+fPos);
                this.RC_HOR_IN.rc_SCD.OffsetY(+fPos);
                this.RC_VER_IN.rc_FST.OffsetX(+fPos);
                this.RC_VER_IN.rc_SCD.OffsetX(+fPos);
            }
            else if (nDir == IFX_DIR.DIR_TOP)
            {
                this.RC_HOR_IN.rc_FST.ScaleX(-fScale);
                this.RC_HOR_IN.rc_SCD.ScaleX(-fScale);
                this.RC_VER_IN.rc_FST.ScaleY(-fScale);
                this.RC_VER_IN.rc_SCD.ScaleY(-fScale);

                this.RC_HOR_IN.rc_FST.OffsetX(+fPos);
                this.RC_HOR_IN.rc_SCD.OffsetX(+fPos);
                this.RC_VER_IN.rc_FST.OffsetY(+fPos);
                this.RC_VER_IN.rc_SCD.OffsetY(+fPos);
            }
            else if (nDir == IFX_DIR.DIR_RHT)
            {
                this.RC_HOR_IN.rc_FST.ScaleY(+fScale);
                this.RC_HOR_IN.rc_SCD.ScaleY(+fScale);
                this.RC_VER_IN.rc_FST.ScaleX(+fScale);
                this.RC_VER_IN.rc_SCD.ScaleX(+fScale);

                this.RC_HOR_IN.rc_FST.OffsetY(-fPos);
                this.RC_HOR_IN.rc_SCD.OffsetY(-fPos);
                this.RC_VER_IN.rc_FST.OffsetX(-fPos);
                this.RC_VER_IN.rc_SCD.OffsetX(-fPos);
            }
            else if (nDir == IFX_DIR.DIR_BTM)
            {
                this.RC_HOR_IN.rc_FST.ScaleX(+fScale);
                this.RC_HOR_IN.rc_SCD.ScaleX(+fScale);
                this.RC_VER_IN.rc_FST.ScaleY(+fScale);
                this.RC_VER_IN.rc_SCD.ScaleY(+fScale);

                this.RC_HOR_IN.rc_FST.OffsetX(-fPos);
                this.RC_HOR_IN.rc_SCD.OffsetX(-fPos);
                this.RC_VER_IN.rc_FST.OffsetY(-fPos);
                this.RC_VER_IN.rc_SCD.OffsetY(-fPos);
            }
            #endregion
            CroodinateBackup();
        }
        public void AdjustSize_EX(int nDir, int nScale)
        {
            float fScale = Convert.ToSingle(nScale);
            float fPos = Convert.ToSingle(fScale / 2.0);

            #region
            if (nDir == IFX_DIR.DIR_LFT)
            {
                this.RC_HOR_EX.rc_FST.ScaleY(-fScale);
                this.RC_HOR_EX.rc_SCD.ScaleY(-fScale);
                this.RC_VER_EX.rc_FST.ScaleX(-fScale);
                this.RC_VER_EX.rc_SCD.ScaleX(-fScale);

                this.RC_HOR_EX.rc_FST.OffsetY(fPos);
                this.RC_HOR_EX.rc_SCD.OffsetY(fPos);
                this.RC_VER_EX.rc_FST.OffsetX(fPos);
                this.RC_VER_EX.rc_SCD.OffsetX(fPos);
            }
            else if (nDir == IFX_DIR.DIR_TOP)
            {
                this.RC_HOR_EX.rc_FST.ScaleX( -fScale);
                this.RC_HOR_EX.rc_SCD.ScaleX( -fScale);
                this.RC_VER_EX.rc_FST.ScaleY( -fScale);
                this.RC_VER_EX.rc_SCD.ScaleY( -fScale);

                this.RC_HOR_EX.rc_FST.OffsetX( fPos );
                this.RC_HOR_EX.rc_SCD.OffsetX( fPos );
                this.RC_VER_EX.rc_FST.OffsetY( fPos );
                this.RC_VER_EX.rc_SCD.OffsetY( fPos );
            }
            else if (nDir == IFX_DIR.DIR_RHT)
            {
                this.RC_HOR_EX.rc_FST.ScaleY( +fScale);
                this.RC_HOR_EX.rc_SCD.ScaleY( +fScale);
                this.RC_VER_EX.rc_FST.ScaleX( +fScale);
                this.RC_VER_EX.rc_SCD.ScaleX( +fScale);

                this.RC_HOR_EX.rc_FST.OffsetY(-fPos);
                this.RC_HOR_EX.rc_SCD.OffsetY(-fPos);
                this.RC_VER_EX.rc_FST.OffsetX( -fPos);
                this.RC_VER_EX.rc_SCD.OffsetX( -fPos);
            }
            else if (nDir == IFX_DIR.DIR_BTM)
            {
                this.RC_HOR_EX.rc_FST.ScaleX( +fScale);
                this.RC_HOR_EX.rc_SCD.ScaleX( +fScale);
                this.RC_VER_EX.rc_FST.ScaleY( +fScale);
                this.RC_VER_EX.rc_SCD.ScaleY( +fScale);

                this.RC_HOR_EX.rc_FST.OffsetX( -fPos);
                this.RC_HOR_EX.rc_SCD.OffsetX( -fPos);
                this.RC_VER_EX.rc_FST.OffsetY( -fPos);
                this.RC_VER_EX.rc_SCD.OffsetY( -fPos);
            }
            #endregion
            CroodinateBackup();
        }

        public void AdjustAsym_IN(int nDir, int nScale)
        {
            float fScale = Convert.ToSingle(nScale);

            if (nDir == IFX_DIR.DIR_LFT)
            {
                this.RC_HOR_IN.rc_FST.OffsetY( +fScale);
                this.RC_HOR_IN.rc_SCD.OffsetY( -fScale);
                this.RC_VER_IN.rc_FST.OffsetX( -fScale);
                this.RC_VER_IN.rc_SCD.OffsetX( +fScale);
            }
            else if (nDir == IFX_DIR.DIR_RHT)
            {
                this.RC_HOR_IN.rc_FST.OffsetY( -fScale);
                this.RC_HOR_IN.rc_SCD.OffsetY( +fScale);
                this.RC_VER_IN.rc_FST.OffsetX( +fScale);
                this.RC_VER_IN.rc_SCD.OffsetX( -fScale);
            }
            else if (nDir == IFX_DIR.DIR_TOP){}
            else if (nDir == IFX_DIR.DIR_BTM){}
            CroodinateBackup();
        }
        public void AdjustAsym_EX(int nDir, int nScale)
        {
            float fScale = Convert.ToSingle(nScale);

            if (nDir == IFX_DIR.DIR_LFT)
            {
                this.RC_HOR_EX.rc_FST.OffsetY( +fScale);
                this.RC_HOR_EX.rc_SCD.OffsetY( -fScale);
                this.RC_VER_EX.rc_FST.OffsetX( -fScale);
                this.RC_VER_EX.rc_SCD.OffsetX( +fScale);
            }
            else if (nDir == IFX_DIR.DIR_RHT)
            {
                this.RC_HOR_EX.rc_FST.OffsetY( -fScale);
                this.RC_HOR_EX.rc_SCD.OffsetY( +fScale);
                this.RC_VER_EX.rc_FST.OffsetX( +fScale);
                this.RC_VER_EX.rc_SCD.OffsetX( -fScale);
            }
            else if (nDir == IFX_DIR.DIR_TOP){}
            else if (nDir == IFX_DIR.DIR_BTM){}
            CroodinateBackup();
        }

        public override float rape_asshole_Log(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX, ref List<PointF> listEdges_FMD,  ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX, ref List<PointF> listEdges_SMD, ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            throw new NotImplementedException();
        }
        public override float rape_Bitch_Direction(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX, ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX, ref List<PointF> listEdges_SMD, ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            throw new NotImplementedException();
        }
        public override float rape_Pussy_Cardin(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX, ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX, ref List<PointF> listEdges_SMD, ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            throw new NotImplementedException();
        }
        public override float MeasureData(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX, ref List<PointF> listEdges_FMD,  ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX, ref List<PointF> listEdges_SMD, ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            throw new NotImplementedException();
        }

        public void rape_MotherFucker(byte[] rawImage, int imageW, int imageH, ref List<PointF> listEdgesHor, ref List<PointF> listEdgesVer, out double fOL_X, out double fOL_Y)
        {
            //*****************************************************************
            // Getting Horizontal Rectangle
            RectangleF rcHOR_EX_LFT = this.RC_HOR_EX.rc_FST.ToRectangleF();
            RectangleF rcHOR_IN_LFT = this.RC_HOR_IN.rc_FST.ToRectangleF();
            RectangleF rcHOR_IN_RHT = this.RC_HOR_IN.rc_SCD.ToRectangleF();
            RectangleF rcHOR_EX_RHT = this.RC_HOR_EX.rc_SCD.ToRectangleF();

            //*****************************************************************
            // Getting Vertical Rectangle 
            RectangleF rcVER_EX_TOP = this.RC_VER_EX.rc_FST.ToRectangleF();
            RectangleF rcVER_IN_TOP = this.RC_VER_IN.rc_FST.ToRectangleF();
            RectangleF rcVER_IN_BTM = this.RC_VER_IN.rc_SCD.ToRectangleF();
            RectangleF rcVER_EX_BTM = this.RC_VER_EX.rc_SCD.ToRectangleF();

            List<PointF> listEdges_VER_EX_TOP = new List<PointF>();
            List<PointF> listEdges_VER_IN_TOP = new List<PointF>();
            List<PointF> listEdges_VER_IN_BTM = new List<PointF>();
            List<PointF> listEdges_VER_EX_BTM = new List<PointF>();

            List<PointF> listEdges_HOR_EX_LFT = new List<PointF>();
            List<PointF> listEdges_HOR_IN_LFT = new List<PointF>();
            List<PointF> listEdges_HOR_IN_RHT = new List<PointF>();
            List<PointF> listEdges_HOR_EX_RHT = new List<PointF>();

            RectangleF rcMergedV = CRect.GetMergedRect(rcVER_EX_TOP, rcVER_EX_BTM);
            RectangleF rcMergedH = CRect.GetMergedRect(rcHOR_EX_LFT, rcHOR_EX_RHT);
            RectangleF rcMergedA = CRect.GetMergedRect(rcMergedH, rcMergedV);

            RectangleF rcInflate = rcMergedA;
            rcInflate.Inflate(rcInflate.Width / 2, rcInflate.Height / 2);
            rawImage = DoPreProcess(rawImage, imageW, imageH, SIGMA, KERNEL, Rectangle.Round(rcInflate));
            rawImage = DoSPCProcess(rawImage, imageW, imageH, rcInflate, param_comm_03_spc_enhance);           


            //*************************************************************************************
            // 01 HORIZONTAL - EXTERNAL TOP 
            //*************************************************************************************

            if (this.algorithm_VER_EX == IFX_ALGORITHM.DIR_IN || this.algorithm_VER_EX == IFX_ALGORITHM.DIR_EX)
            {
                listEdges_VER_EX_TOP = Computer.GetPointList_Derivative_HOR(rawImage, imageW, imageH, rcVER_EX_TOP, true, 0);
                listEdges_VER_EX_BTM = Computer.GetPointList_Derivative_HOR(rawImage, imageW, imageH, rcVER_EX_BTM, true, 0);
            }
            else if (this.algorithm_VER_EX == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_VER_EX_TOP = Computer.HC_EDGE_GetRawPoints_LOG_HOR(rawImage, imageW, imageH, rcVER_EX_TOP, false, 0);
                listEdges_VER_EX_BTM = Computer.HC_EDGE_GetRawPoints_LOG_HOR(rawImage, imageW, imageH, rcVER_EX_BTM, true, 0);

            }
            else if (this.algorithm_VER_EX == IFX_ALGORITHM.CARDIN)
            {
                listEdges_VER_EX_TOP = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rcVER_EX_TOP, true, 0);
                listEdges_VER_EX_BTM = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rcVER_EX_BTM, false, 0);

            }
            
            //*************************************************************************************
            // 02 HORIZONTAL - INTERNAL TOP
            //*************************************************************************************

            if (this.algorithm_VER_IN == IFX_ALGORITHM.DIR_IN || this.algorithm_VER_IN == IFX_ALGORITHM.DIR_EX)
            {
                listEdges_VER_IN_TOP = Computer.GetPointList_Derivative_HOR(rawImage, imageW, imageH, rcVER_IN_TOP, true, 0);
                listEdges_VER_IN_BTM = Computer.GetPointList_Derivative_HOR(rawImage, imageW, imageH, rcVER_IN_BTM, false, 0);

            }
            else if (this.algorithm_VER_IN == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_VER_IN_TOP = Computer.HC_EDGE_GetRawPoints_LOG_HOR(rawImage, imageW, imageH, rcVER_IN_TOP, false, 0);
                listEdges_VER_IN_BTM = Computer.HC_EDGE_GetRawPoints_LOG_HOR(rawImage, imageW, imageH, rcVER_IN_BTM, true, 0);

            }
            else if (this.algorithm_VER_IN == IFX_ALGORITHM.CARDIN)
            {
                listEdges_VER_IN_TOP = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rcVER_IN_TOP, true, 0);
                listEdges_VER_IN_BTM = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rcVER_IN_BTM, false, 0);

            }

         
            //*************************************************************************************
            // 05 VERTICAL - EXTERNAL LFT
            //*************************************************************************************

            if (this.algorithm_HOR_EX == IFX_ALGORITHM.DIR_IN || this.algorithm_HOR_EX == IFX_ALGORITHM.DIR_EX )
            {
                listEdges_HOR_EX_LFT = Computer.GetPointList_Derivative_VER(rawImage, imageW, imageH, rcHOR_EX_LFT, true,0);
                listEdges_HOR_IN_RHT = Computer.GetPointList_Derivative_VER(rawImage, imageW, imageH, rcHOR_IN_RHT, false, 0);

            }
            else if (this.algorithm_HOR_EX == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_HOR_EX_LFT = Computer.HC_EDGE_GetRawPoints_LOG_VER(rawImage, imageW, imageH, rcHOR_EX_LFT, false,0);
                listEdges_HOR_IN_RHT = Computer.HC_EDGE_GetRawPoints_LOG_VER(rawImage, imageW, imageH, rcHOR_IN_RHT, true, 0);

            }
            else if (this.algorithm_HOR_EX == IFX_ALGORITHM.CARDIN)
            {
                listEdges_HOR_EX_LFT = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rcHOR_EX_LFT, true, 0);
                listEdges_HOR_IN_RHT = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rcHOR_IN_RHT, false, 0);

            }

            //*************************************************************************************
            // 06 VERTICAL - INTERNAL LFT
            //*************************************************************************************

            if (this.algorithm_HOR_IN == IFX_ALGORITHM.DIR_IN|| this.algorithm_HOR_IN == IFX_ALGORITHM.DIR_EX )
            {
                listEdges_HOR_IN_LFT = Computer.GetPointList_Derivative_VER(rawImage, imageW, imageH, rcHOR_IN_LFT, true, 0);
                listEdges_HOR_EX_RHT = Computer.GetPointList_Derivative_VER(rawImage, imageW, imageH, rcHOR_EX_RHT, false, 0);

            }
            else if (this.algorithm_HOR_IN == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_HOR_IN_LFT = Computer.HC_EDGE_GetRawPoints_LOG_VER(rawImage, imageW, imageH, rcHOR_IN_LFT, false, 0);
                listEdges_HOR_EX_RHT = Computer.HC_EDGE_GetRawPoints_LOG_VER(rawImage, imageW, imageH, rcHOR_EX_RHT, true, 0);

            }
            else if (this.algorithm_HOR_IN == IFX_ALGORITHM.CARDIN)
            {
                listEdges_HOR_IN_LFT = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rcHOR_IN_LFT, true, 0);
                listEdges_HOR_EX_RHT = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rcHOR_EX_RHT, false, 0);

            }
            
            if (param_comm_04_refinement != 0)
            {
                listEdges_HOR_EX_LFT = Computer.GetList_FilterBy_MajorDistance(listEdges_HOR_EX_LFT, true, param_comm_04_refinement);
                listEdges_HOR_IN_LFT = Computer.GetList_FilterBy_MajorDistance(listEdges_HOR_IN_LFT, true, param_comm_04_refinement);
                listEdges_HOR_IN_RHT = Computer.GetList_FilterBy_MajorDistance(listEdges_HOR_IN_RHT, true, param_comm_04_refinement);
                listEdges_HOR_EX_RHT = Computer.GetList_FilterBy_MajorDistance(listEdges_HOR_EX_RHT, true, param_comm_04_refinement);

                listEdges_VER_EX_TOP = Computer.GetList_FilterBy_MajorDistance(listEdges_VER_EX_TOP, false, param_comm_04_refinement);
                listEdges_VER_IN_TOP = Computer.GetList_FilterBy_MajorDistance(listEdges_VER_IN_TOP, false, param_comm_04_refinement);
                listEdges_VER_IN_BTM = Computer.GetList_FilterBy_MajorDistance(listEdges_VER_IN_BTM, false, param_comm_04_refinement);
                listEdges_VER_EX_BTM = Computer.GetList_FilterBy_MajorDistance(listEdges_VER_EX_BTM, false, param_comm_04_refinement);
            }

            //*************************************************************************************
            // 10 Summary Every Edges
            //*************************************************************************************

            CModelLine model_HOR_EX_LFT = new CModelLine();
            CModelLine model_HOR_IN_LFT = new CModelLine();
            CModelLine model_HOR_IN_RHT = new CModelLine();
            CModelLine model_HOR_EX_RHT = new CModelLine();

            CModelLine model_VER_EX_TOP = new CModelLine();
            CModelLine model_VER_IN_TOP = new CModelLine();
            CModelLine model_VER_IN_BTM = new CModelLine();
            CModelLine model_VER_EX_BTM = new CModelLine();

            CRansac.ransac_Line_fitting(listEdges_HOR_EX_LFT.ToArray(), ref model_HOR_EX_LFT, param_comm_fitting_thr, listEdges_HOR_EX_LFT.Count / 2, listEdges_HOR_EX_LFT.Count);
            CRansac.ransac_Line_fitting(listEdges_HOR_IN_LFT.ToArray(), ref model_HOR_IN_LFT, param_comm_fitting_thr, listEdges_HOR_IN_LFT.Count / 2, listEdges_HOR_IN_LFT.Count);
            CRansac.ransac_Line_fitting(listEdges_HOR_IN_RHT.ToArray(), ref model_HOR_IN_RHT, param_comm_fitting_thr, listEdges_HOR_IN_RHT.Count / 2, listEdges_HOR_IN_RHT.Count);
            CRansac.ransac_Line_fitting(listEdges_HOR_EX_RHT.ToArray(), ref model_HOR_EX_RHT, param_comm_fitting_thr, listEdges_HOR_EX_RHT.Count / 2, listEdges_HOR_EX_RHT.Count);

            CRansac.ransac_Line_fitting(listEdges_VER_EX_TOP.ToArray(), ref model_VER_EX_TOP, param_comm_fitting_thr, listEdges_VER_EX_TOP.Count / 2, listEdges_VER_EX_TOP.Count);
            CRansac.ransac_Line_fitting(listEdges_VER_IN_TOP.ToArray(), ref model_VER_IN_TOP, param_comm_fitting_thr, listEdges_VER_IN_TOP.Count / 2, listEdges_VER_IN_TOP.Count);
            CRansac.ransac_Line_fitting(listEdges_VER_IN_BTM.ToArray(), ref model_VER_IN_BTM, param_comm_fitting_thr, listEdges_VER_IN_BTM.Count / 2, listEdges_VER_IN_BTM.Count);
            CRansac.ransac_Line_fitting(listEdges_VER_EX_BTM.ToArray(), ref model_VER_EX_BTM, param_comm_fitting_thr, listEdges_VER_EX_BTM.Count / 2, listEdges_VER_EX_BTM.Count);

            if (this.param_comm_05_BOOL_SHOW_RAW_DATA == false)
            {
                listEdges_HOR_EX_LFT = Computer.ReplacePointList_Absolute_X(rcHOR_EX_LFT, (float)model_HOR_EX_LFT.sx);
                listEdges_HOR_IN_LFT = Computer.ReplacePointList_Absolute_X(rcHOR_IN_LFT, (float)model_HOR_IN_LFT.sx);
                listEdges_HOR_IN_RHT = Computer.ReplacePointList_Absolute_X(rcHOR_IN_RHT, (float)model_HOR_IN_RHT.sx);
                listEdges_HOR_EX_RHT = Computer.ReplacePointList_Absolute_X(rcHOR_EX_RHT, (float)model_HOR_EX_RHT.sx);

                listEdges_VER_EX_TOP = Computer.ReplacePointList_Absolute_Y(rcVER_EX_TOP, (float)model_VER_EX_TOP.sy);
                listEdges_VER_IN_TOP = Computer.ReplacePointList_Absolute_Y(rcVER_IN_TOP, (float)model_VER_IN_TOP.sy);
                listEdges_VER_IN_BTM = Computer.ReplacePointList_Absolute_Y(rcVER_IN_BTM, (float)model_VER_IN_BTM.sy);
                listEdges_VER_EX_BTM = Computer.ReplacePointList_Absolute_Y(rcVER_EX_BTM, (float)model_VER_EX_BTM.sy);
            }

            listEdgesHor.AddRange(listEdges_HOR_EX_LFT);
            listEdgesHor.AddRange(listEdges_HOR_IN_LFT);
            listEdgesHor.AddRange(listEdges_HOR_IN_RHT);
            listEdgesHor.AddRange(listEdges_HOR_EX_RHT);

            listEdgesVer.AddRange(listEdges_VER_EX_TOP);
            listEdgesVer.AddRange(listEdges_VER_IN_TOP);
            listEdgesVer.AddRange(listEdges_VER_IN_BTM);
            listEdgesVer.AddRange(listEdges_VER_EX_BTM);

            
            //****************************************************************************************************
            // Overlay Calculation 
            //****************************************************************************************************

            double fol_IN_X = (model_HOR_IN_LFT.sx + model_HOR_IN_RHT.sx) / 2.0;
            double fol_EX_X = (model_HOR_EX_LFT.sx + model_HOR_EX_RHT.sx) / 2.0;

            fOL_X = fol_EX_X - fol_IN_X;

            double fol_IN_Y = (model_VER_IN_TOP.sy + model_VER_IN_BTM.sy) / 2.0;
            double fol_EX_Y = (model_VER_EX_TOP.sy + model_VER_EX_BTM.sy) / 2.0;

            fOL_Y = fol_EX_Y - fol_IN_Y;


            fOL_X *= PIXEL_RES;
            fOL_X *= this.param_comm_01_compen_A;
            fOL_X += this.param_comm_02_compen_B;

            fOL_Y *= PIXEL_RES;
            fOL_Y *= this.param_comm_01_compen_A;
            fOL_Y += this.param_comm_02_compen_B;

            return;
        }

    }

    public class CMeasurePairCir : CMeasureMotherFucker
    {
        private int MEASURE_CIRCLE = 0;

        private double SIGMA = 1.0;
        private int KERNEL = 9;
        private int _CENTERING_INFLATE = 10;

        public bool UI_SELECTED { get; set; }

        public RectangleF rc_EX = new RectangleF();
        public RectangleF rc_IN = new RectangleF();
        public RectangleF _rc_EX = new RectangleF(); // 170726 in order to remove relative croodinates
        public RectangleF _rc_IN = new RectangleF();

        public int/******/param_00_algorithm_CIR { get; set; }
        public double/***/param_01_DMG_Tol { get; set; }
        public bool/*****/param_02_BOOL_TREAT_AS_ELLIPSE { get; set; }
        public int/******/param_03_CircleDetecType { get; set; }
        public double/***/param_04_Shrinkage{ get; set; }
        public int/******/param_05_Outlier_Filter { get; set; }
        public double/***/param_06_EdgePos { get; set; }
        public string /*****/param_07_Coverage { get; set; }

        public double[] arrayCos = Computer.GetArray_COS();
        public double[] arraySin = Computer.GetArray_SIN();

        public CMeasurePairCir() 
        {
            param_00_algorithm_CIR = IFX_ALGORITHM.MEXHAT;
            param_01_DMG_Tol = 0;
            param_02_BOOL_TREAT_AS_ELLIPSE = false;
            param_03_CircleDetecType = 0;
            param_04_Shrinkage = 0;
            param_05_Outlier_Filter = 0;
            param_06_EdgePos = 0;
            param_07_Coverage = "0";

            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_spc_enhance = 0;
            param_comm_04_refinement = 3;
            param_comm_05_BOOL_SHOW_RAW_DATA = false; 
        }

        public CMeasurePairCir CopyTo() // In order to avoid icloneable
        {
            CMeasurePairCir single = new CMeasurePairCir();

            single.NICKNAME = this.NICKNAME;
            
            single.rc_EX = this.rc_EX;
            single.rc_IN = this.rc_IN;
            single._rc_EX = this._rc_EX;
            single._rc_IN = this._rc_IN;

            single.param_00_algorithm_CIR = this.param_00_algorithm_CIR;
            single.param_01_DMG_Tol = this.param_01_DMG_Tol;
            single.param_02_BOOL_TREAT_AS_ELLIPSE = this.param_02_BOOL_TREAT_AS_ELLIPSE;
            single.param_03_CircleDetecType = this.param_03_CircleDetecType;
            single.param_04_Shrinkage = this.param_04_Shrinkage;
            single.param_05_Outlier_Filter = this.param_05_Outlier_Filter;
            single.param_06_EdgePos = this.param_06_EdgePos;
            single.param_07_Coverage = this.param_07_Coverage;

            single.param_comm_01_compen_A = this.param_comm_01_compen_A;
            single.param_comm_02_compen_B = this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance = this.param_comm_03_spc_enhance;
            single.param_comm_05_BOOL_SHOW_RAW_DATA = this.param_comm_05_BOOL_SHOW_RAW_DATA;

            return single;
        }
        //************************************************************************************************

        public PointF GetCenter()
        {
            float cx = Convert.ToSingle(rc_EX.X + (rc_EX.Width / 2.0));
            float cy = Convert.ToSingle(rc_EX.Y + (rc_EX.Height / 2.0));

            return new PointF(cx, cy);
        }
        private PointF GetCenter(RectangleF rc)
        {
            float rchw = Convert.ToSingle(rc.Width / 2.0); // half width
            float rchh = Convert.ToSingle(rc.Height / 2.0); // half height 

            float cx = rc.X + rchw;
            float cy = rc.Y + rchh;

            return new PointF(cx, cy);
        }
        

        # region COMMON OVERRIDINGS - NAVIGATOR FUNCTIONS
      
        public override void AdjustGap(int tx, int ty)
        {
            rc_IN.Width += tx;
            rc_IN.Height += ty;

            // size ensure not to be zero. 170518 
            if (rc_IN.Width <= 2) rc_IN.Width = 2;
            if (rc_IN.Height <= 2) rc_IN.Height = 2;

            rc_IN = CRect.SetCenter( rc_IN, rc_EX);

            CroodinateBackup();
        }
        public override void AdjustPos(int tx, int ty)
        {
            rc_EX.Offset(tx, ty);
            rc_IN.Offset(tx, ty);

            CroodinateBackup();
        }
        public override void AdjustSize(int tx, int ty)
        {
            // central resize mode 170515 
            float hx = Convert.ToSingle(tx / 2.0);
            float hy = Convert.ToSingle(ty / 2.0);

            rc_EX.Offset(-hx, -hy);

            rc_EX.Width += tx;
            rc_EX.Height += ty;

            if (rc_EX.Width <= 6) rc_EX.Width = 6;
            if (rc_EX.Height <= 6) rc_EX.Height = 6;

            // size ensure not to be zero. 170518 
            if (rc_IN.Width <= 2) rc_IN.Width = 2;
            if (rc_IN.Height <= 2) rc_IN.Height = 2;

            rc_IN = CRect.SetCenter(rc_IN, rc_EX);

            CroodinateBackup();
        }
         
        #endregion  

        #region MEASUREMENT METHOD VERIFIER
        public override bool VeryfyMeasurementMatching()  {return true;} // this asshole has always true.
        public override string GetMeasurementCategory() {return IFX_ALGORITHM.ToStringType(param_00_algorithm_CIR);}
        #endregion

        public override void CroodinateBackup() { _rc_EX = rc_EX; _rc_IN = rc_IN; }
        public override void CroodinateRecover() { rc_EX = _rc_EX; rc_IN = _rc_IN; }
        public override void SetRelativeMovement(PointF ptDelta)
        {
            CroodinateRecover();
            rc_EX.Offset(ptDelta);
            rc_IN.Offset(ptDelta);
        }

        public void _MarkAngle(int nSectorIndex, int nSectorDivision, ref int[] arrAngle)
        {
            int nAngles = 360;
            int nSectorUnit = nAngles / nSectorDivision;

            int nBegin = nSectorUnit * (nSectorIndex-1);
            int nFinal = nSectorUnit * (nSectorIndex);

            if (nSectorIndex == 1)
            {
                nBegin = 0;
            }

             
            for (int angle = nBegin; angle < nFinal; angle++)
            {
                arrAngle[angle] = 1;
            }

        }
        public int[] GetSelectedAngleByCoverage(string strCoverage)
        {
            int[] arrSelectedAngle = new int[360];
            arrSelectedAngle = Enumerable.Repeat(0, 360).ToArray();

            if( strCoverage.Length == 1)
            {
                if( strCoverage == "0" )
                {
                    arrSelectedAngle = Enumerable.Repeat(1, 360).ToArray();
                }
                else if( strCoverage == "1")
                {
                    _MarkAngle(2, 12, ref arrSelectedAngle);
                    _MarkAngle(8, 12, ref arrSelectedAngle);
                }
                else if( strCoverage =="2")
                {
                    _MarkAngle(5, 12, ref arrSelectedAngle);
                    _MarkAngle(11, 12, ref arrSelectedAngle);
                }
            }
            else
            {
                string[] parser = strCoverage.Split(',');
                List<int> angles = new List<int>();

                for (int i = 1; i < parser.Length; i++)
                {
                    angles.Add(Convert.ToInt32(parser[i]));
                }

                for( int i = 0; i < angles.Count; i++)
                {
                    int nSector =angles.ElementAt(i);
                    _MarkAngle(nSector, 12, ref arrSelectedAngle);
                }
            }
            return arrSelectedAngle;
            
        }

        private int GetRadiLength() { return Convert.ToInt32(Math.Max(this.rc_EX.Width / 2.0, this.rc_EX.Height / 2.0)); }
        private int GetRadiLength_INFULL() { return Convert.ToInt32(Math.Max(this.rc_IN.Width / 2.0, this.rc_IN.Height / 2.0)); }
        private int GetRadiStart() { return Convert.ToInt32(Math.Max(this.rc_IN.Width / 2.0, this.rc_IN.Height / 2.0)); }

        //************************************************************************************************
        
        public override float rape_Pussy_Cardin(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listContours_FEX,ref List<PointF> listContours_FMD, ref List<PointF> listContours_FIN,
            ref List<PointF> listContours_SEX,ref List<PointF> listContours_SMD,ref List<PointF> listContours_SIN,
            out PointF P1, out PointF P2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            double fRadius = 0;

            // set default value 170720
            P1 = P2 = new PointF(0, 0);
            rcEstimated = new RectangleF();
            rcMeasured = new RectangleF();

            PointF ptCenter = new PointF();
            RectangleF rcProcessedRegion = new RectangleF();

            listContours_FEX.Clear();
            listContours_FMD.Clear();
            listContours_FIN.Clear();

            PointF[] arrContour_EX = new PointF[360];
            PointF[] arrContour_MD = new PointF[360];
            PointF[] arrContour_IN = new PointF[360];

            try
            {
                //********************************************************************************************
                // Pre-Process 

                byte[] rawProcessed = IntergratedPreProcessingForCircle(rawImage, imageW, imageH, out ptCenter, out rcProcessedRegion, out rcEstimated);

                //********************************************************************************************

                int nRadiLength = this.GetRadiLength();
                int nRadiStart = this.GetRadiStart();

                //***********************************************************************************
                // Get Rough Edges

              #region EDGE EXTRACTION

                int[] arrSelectedAngle = GetSelectedAngleByCoverage(param_07_Coverage);

                //for (int nAngle = 0; nAngle < 360; nAngle++)
                Parallel.For(0, 360, nAngle =>
                {
                    if (arrSelectedAngle[nAngle] != 0)
                    {
                        PointF[] ptTarget_IN = new PointF[nRadiLength - nRadiStart];
                        PointF[] ptTarget_EX = new PointF[nRadiLength - nRadiStart];

                        for (int nRadiPos = nRadiStart, nIndex = 0; nRadiPos < nRadiLength; nRadiPos++)
                        {
                            double x = ptCenter.X + (nRadiPos * arrayCos[nAngle]);
                            double y = ptCenter.Y + (nRadiPos * arraySin[nAngle]);

                            if (x < 0 || y < 0 || x >= imageW || y >= imageH) { continue; }

                            ptTarget_IN[nIndex++] = new PointF((float)x, (float)y);
                        }

                        double fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawProcessed, imageW, imageH, ptTarget_IN, -1, MEASURE_CIRCLE);
                        double fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawProcessed, imageW, imageH, ptTarget_IN, +1, MEASURE_CIRCLE);

                        double EX_X = ptCenter.X; double EX_Y = ptCenter.Y;
                        double IN_X = ptCenter.X; double IN_Y = ptCenter.Y;

                        if (fSubPosEX != 0)
                        {
                            EX_X = ptCenter.X + ((double)(nRadiStart + fSubPosEX) * arrayCos[nAngle]);
                            EX_Y = ptCenter.Y + ((double)(nRadiStart + fSubPosEX) * arraySin[nAngle]);

                        }
                        if (fSubPosIN != 0)
                        {
                            IN_X = ptCenter.X + ((double)(nRadiStart + fSubPosIN) * arrayCos[nAngle]);
                            IN_Y = ptCenter.Y + ((double)(nRadiStart + fSubPosIN) * arraySin[nAngle]);
                        }

                        PointF pt_EX = new PointF((float)EX_X, (float)EX_Y);
                        PointF pt_IN = new PointF((float)IN_X, (float)IN_Y);
                        PointF pt_MD = CPoint.GetMidPoint(pt_EX, pt_IN);

                        arrContour_EX[nAngle] = pt_EX;
                        arrContour_MD[nAngle] = pt_MD;
                        arrContour_IN[nAngle] = pt_IN;
                    }

                });

                #endregion

                listContours_FEX = arrContour_EX.ToList();
                listContours_FMD = arrContour_MD.ToList();
                listContours_FIN = arrContour_IN.ToList();

                // Currently listcount = raw contour points !@!!@!@!@!@
                //********************************************************************

                if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                {
                    MakeResultForCircle(rawImage, imageW, imageH, rcEstimated, rcProcessedRegion, ref listContours_FEX, ref listContours_FMD, ref listContours_FIN, out rcMeasured);
                }
                else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                {
                    MakeResultForEllipse(rcEstimated, ref listContours_FEX, ref listContours_FMD, ref listContours_FIN, out rcMeasured);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Convert.ToSingle(fRadius * 2.0);
        }

        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        public override float rape_Bitch_Direction(byte[] rawImage, int imageW, int imageH,
        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
            ref List<PointF> listContours_FEX,ref List<PointF> listContours_FMD, ref List<PointF> listContours_FIN,
            ref List<PointF> listContours_SEX, ref List<PointF> listContours_SMD, ref List<PointF> listContours_SIN,
            out PointF P1, out PointF P2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            
            double fRadius = 0;

            // set default value 170720
            P1 = P2 = new PointF(0, 0);
            rcEstimated = new RectangleF();
            rcMeasured = new RectangleF();

            PointF ptCenter = new PointF();
            RectangleF rcProcessedRegion = new RectangleF();

            listContours_FEX.Clear();
            listContours_FMD.Clear();
            listContours_FIN.Clear();

            PointF[] arrContour_EX = new PointF[360];
            PointF[] arrContour_MD = new PointF[360];
            PointF[] arrContour_IN = new PointF[360];


            try
            {
                //********************************************************************************************
                // Pre-Process 

                byte[] rawProcessed = IntergratedPreProcessingForCircle(rawImage, imageW, imageH, out ptCenter, out rcProcessedRegion, out rcEstimated);

                //********************************************************************************************
 
                int nRadiLength = this.GetRadiLength();
                int nRadiStart = this.GetRadiStart();
                int nRealRadi = nRadiLength - nRadiStart;

                if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_EX)
                {
                  #region DIRECTION_EXTERNAL

                    double[] rawAnglurarSlice = Computer.GetAnglurarSliceArray(rawProcessed, imageW, imageH, nRadiLength, nRadiStart, ptCenter, false);

                    int[] arrSelectedAngle = GetSelectedAngleByCoverage(param_07_Coverage);

                    //for (int nAngle = 0; nAngle < 360; nAngle++)
                    Parallel.For(0, 360, nAngle =>
                    {
                        if (arrSelectedAngle[nAngle] != 0)
                        {
                            double EX_X = ptCenter.X; double EX_Y = ptCenter.Y;
                            double IN_X = ptCenter.X; double IN_Y = ptCenter.Y;

                            double[] buffLine = new double[nRealRadi];
                            Array.Copy(rawAnglurarSlice, (nAngle * nRealRadi), buffLine, 0, nRealRadi);

                            double fSubPosEX = Computer.HC_EDGE_Get1stDerivativeLine_PosMax(buffLine);
                            double fSubPosIN = Computer.HC_EDGE_Get1stDerivativeLine_PosMin(buffLine);

                            if (fSubPosEX != 0)
                            {
                                EX_X = ptCenter.X + ((nRadiStart + fSubPosEX) * arrayCos[nAngle]);
                                EX_Y = ptCenter.Y + ((nRadiStart + fSubPosEX) * arraySin[nAngle]);
                            }

                            if (fSubPosIN != 0)
                            {
                                IN_X = ptCenter.X + ((nRadiStart + fSubPosIN) * arrayCos[nAngle]);
                                IN_Y = ptCenter.Y + ((nRadiStart + fSubPosIN) * arraySin[nAngle]);
                            }

                            PointF pt_EX = new PointF((float)EX_X, (float)EX_Y);
                            PointF pt_IN = new PointF((float)IN_X, (float)IN_Y);
                            PointF pt_MD = CPoint.GetMidPoint(pt_EX, pt_IN);

                            arrContour_EX[nAngle] = pt_EX;
                            arrContour_MD[nAngle] = pt_MD;
                            arrContour_IN[nAngle] = pt_IN;
                        }
                    });
                    #endregion
                }
                else if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_IN)
                {
                  #region DIRECTION_INTERANAL
                    double[] rawAnglurarSlice = Computer.GetAnglurarSliceArray(rawProcessed, imageW, imageH, nRadiLength, nRadiStart, ptCenter, true);

                    int[] arrSelectedAngle = GetSelectedAngleByCoverage(param_07_Coverage);


                    //for (int nAngle = 0; nAngle < 360; nAngle++)
                    Parallel.For(0, 360, nAngle =>
                    {
                        if (arrSelectedAngle[nAngle] != 0)
                        {
                            double[] buffLine = new double[nRealRadi];
                            Array.Copy(rawAnglurarSlice, (nAngle * nRealRadi), buffLine, 0, nRealRadi);

                            double EX_X = ptCenter.X; double EX_Y = ptCenter.Y;
                            double IN_X = ptCenter.X; double IN_Y = ptCenter.Y;

                            double fSubPosEX = Computer.HC_EDGE_Get1stDerivativeLine_PosMax(buffLine);
                            double fSubPosIN = Computer.HC_EDGE_Get1stDerivativeLine_PosMin(buffLine);

                            if (fSubPosEX != 0)
                            {
                                EX_X = ptCenter.X + ((nRadiLength - 1 - fSubPosEX) * arrayCos[nAngle]);
                                EX_Y = ptCenter.Y + ((nRadiLength - 1 - fSubPosEX) * arraySin[nAngle]);
                            }
                            if (fSubPosIN != 0)
                            {
                                IN_X = ptCenter.X + ((nRadiLength - 1 - fSubPosIN) * arrayCos[nAngle]);
                                IN_Y = ptCenter.Y + ((nRadiLength - 1 - fSubPosIN) * arraySin[nAngle]);
                            }

                            PointF pt_EX = new PointF((float)EX_X, (float)EX_Y);
                            PointF pt_IN = new PointF((float)IN_X, (float)IN_Y);
                            PointF pt_MD = CPoint.GetMidPoint(pt_EX, pt_IN);

                            arrContour_EX[nAngle] = pt_EX;
                            arrContour_MD[nAngle] = pt_MD;
                            arrContour_IN[nAngle] = pt_IN;
                        }
                    });
                    #endregion
                }

                listContours_FEX = arrContour_EX.ToList();
                listContours_FMD = arrContour_MD.ToList();
                listContours_FIN = arrContour_IN.ToList();

                if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                {
                    MakeResultForCircle(rawImage, imageW, imageH, rcEstimated, rcProcessedRegion, ref listContours_FEX, ref listContours_FMD, ref listContours_FIN, out rcMeasured);
                }
                else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                {
                    MakeResultForEllipse(rcEstimated, ref listContours_FEX, ref listContours_FMD, ref listContours_FIN, out rcMeasured);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Convert.ToSingle(fRadius * 2.0);
                    
        }
        public override float rape_asshole_Log(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listContours_FEX,ref List<PointF> listContours_FMD, ref List<PointF> listContours_FIN,
            ref List<PointF> listContours_SEX,ref List<PointF> listContours_SMD,ref List<PointF> listContours_SIN,
            out PointF P1, out PointF P2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
            double fRadius = 0;

            // set default value 170720
            P1 = P2 = new PointF(0, 0);
            rcEstimated = new RectangleF();
            rcMeasured = new RectangleF();

            PointF ptCenter = new PointF();
            RectangleF rcProcessedRegion = new RectangleF();

            listContours_FEX.Clear();
            listContours_FMD.Clear();
            listContours_FIN.Clear();

            PointF[] arrContour_EX = new PointF[360];
            PointF[] arrContour_MD = new PointF[360];
            PointF[] arrContour_IN = new PointF[360];
            
            try
            {
                //********************************************************************************************
                // Pre-Process 

                byte[] rawProcessed = IntergratedPreProcessingForCircle(rawImage, imageW, imageH, out ptCenter, out rcProcessedRegion, out rcEstimated);

                //********************************************************************************************

                int nRadiLength = this.GetRadiLength();
                int nRadiStart = this.GetRadiStart();

                //***********************************************************************************
                // Get Rough Edges
                
              #region EDGE-EXTRACTION

                int[] arrSelectedAngle = GetSelectedAngleByCoverage(param_07_Coverage);


                //for (int nAngle = 0; nAngle < 360; nAngle++)
                Parallel.For(0, 360, nAngle =>
                {
                    if (arrSelectedAngle[nAngle] != 0)
                    {
                        PointF[] ptTarget_IN = new PointF[nRadiLength - nRadiStart];
                        PointF[] ptTarget_EX = new PointF[nRadiLength - nRadiStart];

                        for (int nRadiPos = nRadiStart, nIndex = 0; nRadiPos < nRadiLength; nRadiPos++)
                        {
                            double x = ptCenter.X + (nRadiPos * arrayCos[nAngle]);
                            double y = ptCenter.Y + (nRadiPos * arraySin[nAngle]);

                            if (x < 0 || y < 0 || x >= imageW || y >= imageH) { continue; }

                            ptTarget_IN[nIndex++] = new PointF((float)x, (float)y);
                        }

                        double fSubPos_EX = Computer.HC_EDGE_GetLogPos(rawProcessed, imageW, imageH, ptTarget_IN, +1);
                        double fSubPos_IN = Computer.HC_EDGE_GetLogPos(rawProcessed, imageW, imageH, ptTarget_IN, -1);

                        double EX_X = ptCenter.X; double EX_Y = ptCenter.Y;
                        double IN_X = ptCenter.X; double IN_Y = ptCenter.Y;

                        if (fSubPos_EX != 0)
                        {
                            EX_X = ptCenter.X + ((nRadiStart + fSubPos_EX) * arrayCos[nAngle]);
                            EX_Y = ptCenter.Y + ((nRadiStart + fSubPos_EX) * arraySin[nAngle]);

                        }
                        if (fSubPos_IN != 0)
                        {
                            IN_X = ptCenter.X + ((nRadiStart + fSubPos_IN) * arrayCos[nAngle]);
                            IN_Y = ptCenter.Y + ((nRadiStart + fSubPos_IN) * arraySin[nAngle]);
                        }

                        PointF pt_EX = new PointF((float)EX_X, (float)EX_Y);
                        PointF pt_IN = new PointF((float)IN_X, (float)IN_Y);
                        PointF pt_MD = CPoint.GetMidPoint(pt_EX, pt_IN);

                        arrContour_EX[nAngle] = pt_EX;
                        arrContour_MD[nAngle] = pt_MD;
                        arrContour_IN[nAngle] = pt_IN;
                    }
                    
                });

                #endregion

                listContours_FEX = arrContour_EX.ToList();
                listContours_FMD = arrContour_MD.ToList();
                listContours_FIN = arrContour_IN.ToList();

                // Currently listcount = raw contour points !@!!@!@!@!@
                //********************************************************************

                if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                {
                    MakeResultForCircle(rawImage, imageW, imageH, rcEstimated, rcProcessedRegion, ref listContours_FEX, ref listContours_FMD, ref listContours_FIN, out rcMeasured);
                }
                else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                {
                    MakeResultForEllipse(rcEstimated, ref listContours_FEX, ref listContours_FMD, ref listContours_FIN, out rcMeasured);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Convert.ToSingle(fRadius * 2.0);
        }

        // 171025 intergrated All together for work convenience and code run stability.
        private byte[] IntergratedPreProcessingForCircle(byte[] rawImage, int imageW, int imageH, out PointF ptCenter, out RectangleF rcProcessedRegion, out RectangleF rcEstimated)
        {
            RectangleF rcCirEx = this.rc_EX;
            RectangleF rcInflate = CRect.InflateRect(rcCirEx, _CENTERING_INFLATE);
            rcEstimated = new RectangleF();

            // in case of auto circle auto detection applied
            if (param_03_CircleDetecType != 0)
            {
                // auto circle estimation
                rcEstimated = Computer.HC_CIRCLE_CENTERING(rawImage, imageW, imageH, Rectangle.Round(rcInflate), param_04_Shrinkage, param_03_CircleDetecType);

                // centering preprocessing region by estimated region 
                rcInflate = CRect.SetCenter(rcInflate, rcEstimated.X, rcEstimated.Y);

                // centering measurement region by estimated region 
                rcCirEx = CRect.SetCenter(rcCirEx, rcEstimated.X, rcEstimated.Y);
            }

            byte[] imgProcessed = DoPreProcess(rawImage, imageW, imageH, SIGMA, KERNEL, Rectangle.Round(rcInflate));
            imgProcessed = DoSPCProcess(rawImage, imageW, imageH, rcInflate, param_comm_03_spc_enhance);

            // get the corrected center
            ptCenter = CRect.GetCenter(rcCirEx);
            // get the corredted measure region
            rcProcessedRegion = rcCirEx;

            return imgProcessed; 
        }

        // 171025 intergrated All together for work convenience and code run stability.
        private void MakeResultForCircle(byte [] rawImage, int imageW, int imageH, RectangleF rcEstimated, RectangleF rcProcessedRegion, 
            ref List<PointF> listContours_EX, ref List<PointF> listContours_MD, ref List<PointF> listContours_IN,
            out RectangleF rcMeasured)
        {
            rcMeasured = new RectangleF();

            if (0 < param_01_DMG_Tol && param_01_DMG_Tol < 1)
            {
                listContours_EX = Computer.GetFilterdedCircleEdgesByDamageTolderance(rawImage, imageW, imageH, rcProcessedRegion, listContours_EX, this.param_01_DMG_Tol);
                listContours_MD = Computer.GetFilterdedCircleEdgesByDamageTolderance(rawImage, imageW, imageH, rcProcessedRegion, listContours_MD, this.param_01_DMG_Tol);
                listContours_IN = Computer.GetFilterdedCircleEdgesByDamageTolderance(rawImage, imageW, imageH, rcProcessedRegion, listContours_IN, this.param_01_DMG_Tol);
            }
            else if (this.param_01_DMG_Tol == 1)
            {
                listContours_EX = Computer.GetIterativeCircleDiaByDmgTolerance(rawImage, imageW, imageH, rcProcessedRegion, listContours_EX);
                listContours_MD = Computer.GetIterativeCircleDiaByDmgTolerance(rawImage, imageW, imageH, rcProcessedRegion, listContours_MD);
                listContours_IN = Computer.GetIterativeCircleDiaByDmgTolerance(rawImage, imageW, imageH, rcProcessedRegion, listContours_IN);
            }

            if (param_05_Outlier_Filter == 1)
            {
                Rectangle rcEstCompen = CRect.OffsetRect(Rectangle.Round(rcEstimated), -(rcEstimated.Width / 2.0), -(rcEstimated.Height / 2.0));
                listContours_EX = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listContours_EX);
                listContours_MD = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listContours_MD);
                listContours_IN = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listContours_IN);
            }

            PointF ptCenter_EX = new PointF();
            PointF ptCenter_MD = new PointF();
            PointF ptCenter_IN = new PointF();

            double fRadius_EX = 0;
            double fRadius_MD = 0;
            double fRadius_IN = 0;

            // Remove ZeroPoisition!!! to make accurate circle fitting result 171124
            listContours_EX = CPoint.getList_Without_ZeroPosition(listContours_EX);
            listContours_MD = CPoint.getList_Without_ZeroPosition(listContours_MD);
            listContours_IN = CPoint.getList_Without_ZeroPosition(listContours_IN);

            {
                Computer.HC_FIT_Circle(listContours_EX, ref ptCenter_EX, ref fRadius_EX);
                Computer.HC_FIT_Circle(listContours_MD, ref ptCenter_MD, ref fRadius_MD);
                Computer.HC_FIT_Circle(listContours_IN, ref ptCenter_IN, ref fRadius_IN);
            }

            /***/if (param_06_EdgePos == 0.0)
            {
                rcMeasured = CRect.GenRectangle(ptCenter_IN, fRadius_IN, fRadius_IN);
            }
            else if (param_06_EdgePos == 0.5)
            {
                rcMeasured = CRect.GenRectangle(ptCenter_MD, fRadius_MD, fRadius_MD);
            }
            else if (param_06_EdgePos == 1.0)
            {
                rcMeasured = CRect.GenRectangle(ptCenter_EX, fRadius_EX, fRadius_EX);
            }

            if (param_comm_05_BOOL_SHOW_RAW_DATA == false)
            {
                if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                {
                    /***/
                    if (param_06_EdgePos == 0.0)
                    {
                        listContours_IN = Computer.GenCircleContourPoints(fRadius_IN, ptCenter_IN);
                        listContours_MD.Clear();
                        listContours_EX.Clear();
                    }
                    else if (param_06_EdgePos == 0.5)
                    {
                        listContours_MD = Computer.GenCircleContourPoints(fRadius_MD, ptCenter_MD);
                        listContours_IN.Clear();
                        listContours_EX.Clear();
                    }
                    else if (param_06_EdgePos == 1.0)
                    {
                        listContours_EX = Computer.GenCircleContourPoints(fRadius_EX, ptCenter_EX);
                        listContours_IN.Clear();
                        listContours_MD.Clear();
                    }
                }
            } 


            
         }
        private void MakeResultForEllipse(RectangleF rcEstimated, ref List<PointF> listContours_EX, ref List<PointF> listContours_MD, ref List<PointF> listContours_IN,out RectangleF rcMeasured)
        {
            rcMeasured = new RectangleF();

            if (param_05_Outlier_Filter == 1)
            {
                Rectangle rcEstCompen = CRect.OffsetRect(Rectangle.Round(rcEstimated), -rcEstimated.Width, -rcEstimated.Height);

                listContours_EX = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listContours_EX);
                listContours_MD = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listContours_MD);
                listContours_IN = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listContours_IN);
            }
 
            double distanceThreshold = 50;

            CModelEllipse model_EX = new CModelEllipse();
            CModelEllipse model_MD = new CModelEllipse();
            CModelEllipse model_IN = new CModelEllipse();

            // Remove ZeroPoisition!!! to make accurate circle fitting result 171124
            listContours_EX = CPoint.getList_Without_ZeroPosition(listContours_EX);
            listContours_MD = CPoint.getList_Without_ZeroPosition(listContours_MD);
            listContours_IN = CPoint.getList_Without_ZeroPosition(listContours_IN);

            CRansac.ransac_ellipse_fitting(listContours_EX.ToArray(), ref model_EX, distanceThreshold);
            CRansac.ransac_ellipse_fitting(listContours_MD.ToArray(), ref model_MD, distanceThreshold);
            CRansac.ransac_ellipse_fitting(listContours_IN.ToArray(), ref model_IN, distanceThreshold);

            PointF[] arrPt_EX = listContours_EX.ToArray();
            PointF[] arrPt_MD = listContours_MD.ToArray();
            PointF[] arrPt_IN = listContours_IN.ToArray();


            /***/if (param_06_EdgePos == 0.0) 
            {
                rcMeasured = CRect.GenRectangle(model_IN.cx, model_IN.cy, model_IN.w, model_IN.h); 
            }
            else if (param_06_EdgePos == 0.5) 
            {
                rcMeasured = CRect.GenRectangle(model_MD.cx, model_MD.cy, model_IN.w, model_IN.h); 
            }
            else if (param_06_EdgePos == 1.0) 
            {
                rcMeasured = CRect.GenRectangle(model_EX.cx, model_EX.cy, model_IN.w, model_IN.h); 
            }
            
            if (param_comm_05_BOOL_SHOW_RAW_DATA == false)
            { 
                if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                {
                    List<PointF> listContours_SEX = CRansac.GetEllipseContours(model_EX);
                    List<PointF> listContours_SMD = CRansac.GetEllipseContours(model_MD);
                    List<PointF> listContours_SIN = CRansac.GetEllipseContours(model_IN);

                    /***/if (param_06_EdgePos == 0.0)
                    {
                        listContours_IN = listContours_SIN.ToList();
                        listContours_MD.Clear();
                        listContours_EX.Clear();
                    }
                    else if (param_06_EdgePos == 0.5)
                    {
                        listContours_MD = listContours_SMD.ToList();
                        listContours_IN.Clear();
                        listContours_EX.Clear();
                    }
                    else if (param_06_EdgePos == 1.0)
                    {
                        listContours_EX = listContours_SEX.ToList();
                        listContours_IN.Clear();
                        listContours_MD.Clear();
                    }
                }
            } // bShowData
        }
            
        public override float MeasureData(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listContours_FEX,ref List<PointF> listContours_FMD, ref List<PointF> listContours_FIN,
            ref List<PointF> listContours_SEX,ref List<PointF> listContours_SMD,ref List<PointF> listContours_SIN,
            out PointF P1, out PointF P2, out RectangleF rcEstimated, out RectangleF rcMeasured)
        {
           #region Call Algorithm Functions For each 

            double fDistance = 0;
            // set default value 170720
            P1 = P2 = new PointF(0, 0);
            rcEstimated = new RectangleF();
            rcMeasured = new RectangleF();


            /***/if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN)) 
            {
                fDistance = this.rape_Pussy_Cardin(rawImage, imageW, imageH,
                    ref listContours_FEX,ref listContours_FMD,ref listContours_FIN,
                    ref listContours_SEX,ref listContours_SMD,ref listContours_SIN,
                    out P1, out P2, out rcEstimated, out rcMeasured); 
            }
            else if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_IN) ||
                     this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_EX)) 
            {
                fDistance = this.rape_Bitch_Direction(rawImage, imageW, imageH,
                    ref listContours_FEX,ref listContours_FMD,ref listContours_FIN,
                    ref listContours_SEX,ref listContours_SMD,ref listContours_SIN,
                    out P1, out P2, out rcEstimated, out rcMeasured); 
            }
            else if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.MEXHAT)) 
            {
                fDistance = this.rape_asshole_Log(rawImage, imageW, imageH,
                    ref listContours_FEX,ref listContours_FMD,ref listContours_FIN,
                    ref listContours_SEX,ref listContours_SMD,ref listContours_SIN,
                    out P1, out P2, out rcEstimated, out rcMeasured); 
            }

            fDistance *= PIXEL_RES;
            fDistance *= this.param_comm_01_compen_A;
            fDistance += this.param_comm_02_compen_B;

            return Convert.ToSingle(fDistance);
            #endregion
        }
    }

     
    public class COverlay
    {
        public double DX { get; set; }
        public double DY { get; set; }

        public COverlay()
        {
            DX = DY = 0;
        }
        public COverlay(double dx, double dy)
        {
            DX = dx; DY = dy;
        }
    }

    public class CInspectionUnit
    {
        public CFigureManager fm = new CFigureManager();
        public int nCamNo = 0;
        public byte[] image = null;
        public int imageW = 0;
        public int imageH = 0;

        public CInspectionUnit(byte[] rawImage, int imageW, int imageH, int CamNo, CFigureManager fm)
        {
            this.fm = fm.Clone() as CFigureManager;

            image = new byte[rawImage.Length];

            if( rawImage.Length != 0 )
            {
                Array.Copy(rawImage, this.image, rawImage.Length);
            }

            this.imageW = imageW;
            this.imageH = imageH;
        }
    }

    public abstract class CMeasureFather
    {
        public CMeasureFather(){}

        public string global_01_input_file_name { get; set; }
        public string global_02_insp_time { get; set; }

        
        public string param_01_figure_type { get; set; }
        public string param_00_target_name { get; set; }

        public string str_comm_01_compenA { get; set; }
        public string str_comm_02_compenB { get; set; }
        public string str_comm_03_spc_enhance { get; set; }
        public string str_comm_04_refinement { get; set; }

        
        public List<double> listRaw_Figure = new List<double>();
        public List<double> listRaw_Mag = new List<double>();
        public List<COverlay> listRaw_Overlay = new List<COverlay>();

        public int DataCount_Figure { get { return listRaw_Figure.Count; } }
        public int DataCount_Overlay { get { return listRaw_Overlay.Count; } }
        public int DataCount_Magnitude { get { return listRaw_Mag.Count; } }

        public abstract void CalcAccResult();

    }

    public class CMeasureReport
    {
        public int SequenceIndex { get; set; } // 171031 added

        public bool INTERRUPT { get; set; }
        private List<DataFigure> m_listFigure = new List<DataFigure>();
        private List<DataOverlay> m_listOverlay = new List<DataOverlay>();
        public List<double> m_listFocusMag = new List<double>();
        public List<int[]> m_listHIstogram = new List<int[]>();
        public List<double> m_list_Similarity = new List<double>();
        public List<PointF> m_listMatchPoint = new List<PointF>();

        public string INFO_RECIPE { get; set; }
        public string INFO_PTRN { get; set; }
        public double INFO_PIXEL_X { get; set; }
        public string INFO_TIME { get; set; }

        private int/**/COUNT_FIGURE { get { return m_listFigure.Count; } }
        private int/**/COUNT_OVERLAY { get { return m_listOverlay.Count; } }

        public int MEAS_CYCLE { get; set; }
        public int MEAS_POINT { get; set; }

        public void IncreseSequenceIndex() { SequenceIndex++; }
        public class DataOverlay : CMeasureFather
        {
            public string INSP_METHOD_HOR_EX { get; set; }
            public string INSP_METHOD_HOR_IN { get; set; }

            public string INSP_METHOD_VER_IN { get; set; }
            public string INSP_METHOD_VER_EX { get; set; }

            public double INSP_DX { get; set; }
            public double INSP_DY { get; set; }

            public int TOTAL_COUNT { get { return listRaw_Overlay.Count; } }

            public DataOverlay CopyTo()
            {
                DataOverlay single = new DataOverlay();

                single.global_01_input_file_name = this.global_01_input_file_name;
                single.param_01_figure_type = this.param_01_figure_type;
                single.param_00_target_name = this.param_00_target_name;
                single.global_02_insp_time = this.global_02_insp_time;

                single.INSP_METHOD_HOR_EX = this.INSP_METHOD_HOR_EX;
                single.INSP_METHOD_HOR_IN = this.INSP_METHOD_HOR_IN;

                single.INSP_METHOD_VER_IN = this.INSP_METHOD_VER_IN;
                single.INSP_METHOD_VER_EX = this.INSP_METHOD_VER_EX;

                single.INSP_DX = this.INSP_DY;
                single.INSP_DY = this.INSP_DY;
                return single;
            }
            public void Insert_Data(double dx, double dy)
            {
                COverlay single = new COverlay(dx, dy);

                listRaw_Overlay.Add(single);
                INSP_DX += dx;
                INSP_DY += dy;
              }

            public override void CalcAccResult()
            {
                INSP_DX /= listRaw_Overlay.Count();
                INSP_DY /= listRaw_Overlay.Count();
             }
            public COverlay calcSigma(double fPixelRes )
            {
                COverlay[] arrRaw = listRaw_Overlay.ToArray();

                double [] arrRaw_X = arrRaw.Select(element => element.DX).ToArray();
                double [] arrRaw_Y = arrRaw.Select(element => element.DY).ToArray();

                double fMeanX = arrRaw_X.Average();
                double fMeanY = arrRaw_Y.Average();

                double varX = 0;
                double varY = 0;

                for (int i = 0; i < TOTAL_COUNT; i++)
                {
                    varX += (arrRaw_X[i] - fMeanX) * (arrRaw_X[i] - fMeanX);
                    varY += (arrRaw_Y[i] - fMeanY) * (arrRaw_Y[i] - fMeanY);
                }

                varX /= TOTAL_COUNT - 1;
                varY /= TOTAL_COUNT - 1;

                COverlay overlay = new COverlay();

                overlay.DX = Math.Sqrt(varX);
                overlay.DY = Math.Sqrt(varY);

                return overlay;
            }
        }
        public class DataFigure : CMeasureFather
        {
            public string param_02_algorithm { get; set; }
            public double temporar_01_measureResult { get; set; }

            public string param_rect_01_bool_use_auto_peak_Detection { get; set; }
            public string param_rect_02_peak_target_index_fst { get; set; }
            public string param_rect_03_peak_target_index_scd { get; set; }
            public string param_rect_04_peak_candidate { get; set; }
            public string param_rect_05_peak_window_size { get; set; }
            public string param_rect_06_edge_position_fst { get; set; }
            public string param_rect_07_edge_position_scd { get; set; }

            public string param_cir_01_damage_tolerance { get; set; }
            public string param_cir_02_treat_as_ellipse { get; set; }
            public string param_cir_03_circle_detection_type { get; set; }
            public string param_cir_04_shrinkage { get; set; }
            public string param_cir_05_outlier_filter { get; set; }
            public string param_cir_06_edge_posistion { get; set; }
            public string param_cir_07_coverage { get; set; }


            public string param_comm_01_compenA { get; set; }
            public string param_comm_02_compenB { get; set; }
            public string param_comm_03_spc_enhance { get; set; }
            public string param_comm_04_refinement { get; set; }

            public int TOTAL_COUNT { get { return listRaw_Figure.Count; } }

            // to make data frame for accumulation
            public DataFigure CopyTo()
            {
                DataFigure single = new DataFigure();

                
                single.temporar_01_measureResult = this.temporar_01_measureResult;

                single.global_01_input_file_name = this.global_01_input_file_name;
                single.global_02_insp_time = this.global_02_insp_time;

                single.param_00_target_name = this.param_00_target_name;
                single.param_01_figure_type = this.param_01_figure_type;
                single.param_02_algorithm = this.param_02_algorithm;

                single.param_cir_01_damage_tolerance = this.param_cir_01_damage_tolerance;
                single.param_cir_02_treat_as_ellipse = this.param_cir_02_treat_as_ellipse;
                single.param_cir_03_circle_detection_type = this.param_cir_03_circle_detection_type;
                single.param_cir_04_shrinkage = this.param_cir_04_shrinkage;
                single.param_cir_05_outlier_filter = this.param_cir_05_outlier_filter;
                single.param_cir_06_edge_posistion = this.param_cir_06_edge_posistion;
                single.param_cir_07_coverage = this.param_cir_07_coverage;

                single.param_rect_01_bool_use_auto_peak_Detection = this.param_rect_01_bool_use_auto_peak_Detection;
                single.param_rect_02_peak_target_index_fst = this.param_rect_02_peak_target_index_fst;
                single.param_rect_03_peak_target_index_scd = this.param_rect_03_peak_target_index_scd;
                single.param_rect_04_peak_candidate = this.param_rect_04_peak_candidate;
                single.param_rect_05_peak_window_size = this.param_rect_05_peak_window_size;
                single.param_rect_06_edge_position_fst = this.param_rect_06_edge_position_fst;
                single.param_rect_07_edge_position_scd = this.param_rect_07_edge_position_scd;

                single.param_comm_01_compenA = this.param_comm_01_compenA;
                single.param_comm_02_compenB = this.param_comm_02_compenB;
                single.param_comm_03_spc_enhance = this.param_comm_03_spc_enhance;
                single.param_comm_04_refinement = this.param_comm_04_refinement;
                
                return single;
            }

             // data accumulation interface 
            public void Insert_Data(double fValue)
            {
                listRaw_Figure.Add(fValue);
                temporar_01_measureResult += fValue;
             }

            // sumarry function
            public override void CalcAccResult() 
            {
                temporar_01_measureResult /= listRaw_Figure.Count;
            }
            public double calcSigma(double fPixelRes)
            {
                double[] arrRaw = listRaw_Figure.ToArray();
                
                double fMean = arrRaw.Average();

                double variance = 0;

                for (int i = 0; i < arrRaw.Length; i++)
                {
                    variance += (arrRaw[i] - fMean)*(arrRaw[i] - fMean);
                }
                variance /= arrRaw.Length-1;

                double sigma = Math.Sqrt(variance); 
                return sigma;
            }
        }


        private DataFigure GetIndex_Figure(int nIndex)
        {
            DataFigure single = new DataFigure();
            if (COUNT_FIGURE >= nIndex){single = m_listFigure.ElementAt(nIndex);}
            return single;
        }
        private DataOverlay GetIndex_Overlay(int nIndex)
        {
            DataOverlay single = new DataOverlay();
            if (COUNT_OVERLAY >= nIndex){single = m_listOverlay.ElementAt(nIndex);}
            return single;
        }

        public List<DataFigure> GetAverage_Figure()
        {
            List<DataFigure> listUnique = new List<DataFigure>();

            // Build Unique List 
            for (int i = 0; i < COUNT_FIGURE; i++)
            {
                DataFigure single = GetIndex_Figure(i);

                // first chance exception
                if (listUnique.Count == 0)
                {
                    DataFigure temp = single.CopyTo();
                    temp.temporar_01_measureResult = 0;

                    listUnique.Add(temp);
                }
                else
                {
                    bool bExistance = Check_UniqueExistance_Figure(listUnique, single);

                    // if unique
                    if (bExistance == false)
                    {
                        DataFigure temp = single.CopyTo();
                        temp.temporar_01_measureResult = 0;

                        listUnique.Add(temp);
                    }
                }
            }

            //**********************************************************
            // generate array : accumulation

            DataFigure[] arrUnitData = listUnique.ToArray();

            for (int i = 0; i < COUNT_FIGURE; i++)
            {
                DataFigure single = m_listFigure.ElementAt(i);

                int nIndex = Find_UniqueIndex_Figure(arrUnitData, single);

                arrUnitData[nIndex].Insert_Data(single.temporar_01_measureResult);
            }

            // Summary 
            for (int i = 0; i < listUnique.Count; i++)
            {
                arrUnitData[i].CalcAccResult();
            }
            return arrUnitData.ToList();
        }
        public List<DataOverlay> GetAverage_Overlay()
        {
            List<DataOverlay> listUnique = new List<DataOverlay>();

            // Build Unique List 
            for (int i = 0; i < COUNT_OVERLAY; i++)
            {
                DataOverlay single = GetIndex_Overlay(i);

                // first chance exception
                if (listUnique.Count == 0)
                {
                    DataOverlay temp = single.CopyTo();
                    temp.INSP_DX = 0;
                    temp.INSP_DY = 0;
                    listUnique.Add(temp);
                }
                else
                {
                    bool bExistance = Check_UniqueExistance_Overlay(listUnique, single);

                    // if unique
                    if (bExistance == false)
                    {
                        DataOverlay temp = single.CopyTo();
                        temp.INSP_DX = 0;
                        temp.INSP_DY = 0;

                        listUnique.Add(temp);
                    }
                }
            }

            //**********************************************************
            // generate array : accumulation

            DataOverlay[] arrUnitData = listUnique.ToArray();

            for (int i = 0; i < COUNT_OVERLAY; i++)
            {
                DataOverlay single = m_listOverlay.ElementAt(i);

                int nIndex = Find_UniqueIndex_Overlay(arrUnitData, single);

                double dx = single.INSP_DX * INFO_PIXEL_X;
                double dy = single.INSP_DY * INFO_PIXEL_X;

                arrUnitData[nIndex].Insert_Data(dx, dy);
            }

            // Summary 
            for (int i = 0; i < listUnique.Count; i++)
            {
                arrUnitData[i].CalcAccResult();
            }
            return arrUnitData.ToList();
        }
        public List<double> GetAverage_Similarity()
        {
            m_list_Similarity.Clear();

            if (m_listHIstogram.Count != 0)
            {
                int[] nHisto = m_listHIstogram.ElementAt(0);

                for (int i = 0; i < m_listHIstogram.Count; i++)
                {
                    int[] nCurrent = m_listHIstogram.ElementAt(i);

                    double fDist = HC_HISTO_GetDistance(nHisto, nCurrent);

                    m_list_Similarity.Add(fDist);
                }
            }
            return m_list_Similarity;
        }
        // get index from a given array
        private int Find_UniqueIndex_Figure(DataFigure[] arrUnitData, DataFigure single)
        {
            int nIndex = -1;

            for (int i = 0; i < arrUnitData.Length; i++)
            {
                DataFigure baseSingle = arrUnitData[i];

                if (baseSingle.param_01_figure_type == single.param_01_figure_type &&
                    baseSingle.param_00_target_name == single.param_00_target_name)
                {
                    nIndex = i;
                    break;
                }
            }
            return nIndex;
        }
        private int Find_UniqueIndex_Overlay(DataOverlay[] arrUnitData, DataOverlay single)
        {
            int nIndex = -1;

            for (int i = 0; i < arrUnitData.Length; i++)
            {
                DataOverlay baseSingle = arrUnitData[i];

                if (baseSingle.param_00_target_name == single.param_00_target_name)
                {
                    nIndex = i;
                    break;
                }
            }
            return nIndex;
        }
        // get existance from a given list
        private bool Check_UniqueExistance_Figure(List<DataFigure> list, DataFigure single)
        {
            bool bExistance = false;

            for (int i = 0; i < list.Count; i++)
            {
                DataFigure baseSignle = list.ElementAt(i);

                if (baseSignle.param_01_figure_type == single.param_01_figure_type &&
                    baseSignle.param_00_target_name == single.param_00_target_name)
                {
                    bExistance = true;
                    break;
                }
            }
            return bExistance;
        }
        private bool Check_UniqueExistance_Overlay(List<DataOverlay> list, DataOverlay single)
        {
            bool bExistance = false;

            for (int i = 0; i < list.Count; i++)
            {
                DataOverlay baseSignle = list.ElementAt(i);

                if (baseSignle.param_00_target_name == single.param_00_target_name)
                {
                    bExistance = true;
                    break;
                }
            }
            return bExistance;
        }

        public void SetInit(int nCycle, int nPoint)
        {
            m_listFigure.Clear();
            m_listOverlay.Clear();
            m_listFocusMag.Clear();
            m_listMatchPoint.Clear();
            MEAS_CYCLE = nCycle;
            MEAS_POINT = nPoint;
            INTERRUPT = false;
            SequenceIndex = 0;
            m_list_Similarity.Clear();
            m_listHIstogram.Clear();
        }

        // 171018 focus magnitude 
        public void AddFocusMag(double fFocusMag) { m_listFocusMag.Add(fFocusMag); }
        public void AddHistogram(int[] nHisto) { m_listHIstogram.Add(nHisto); }
        public double GetFocusMagAvg()
        {
            double fAVG = 0;
            if (m_listFocusMag.Count() != 0) // non data exception 171030
            {
                fAVG = m_listFocusMag.Average();
            }
            return fAVG;
        }

        public void AddResult_FIG(int nFigureType, string strPathInputImage, object Ini, double fResult)
        {
            string str_00_TargetName = string.Empty;
            string str_01_figure_type = string.Empty;
            string str_02_algo_method = string.Empty;

            string str_comm_01_compenA = string.Empty;
            string str_comm_02_compenB = string.Empty;
            string str_comm_03_spc_enhance = string.Empty;
            string str_comm_04_refinement = string.Empty;

            string str_rect_01_bool_use_auto_peak_Detection = string.Empty;
            string str_rect_02_peak_target_index_fst = string.Empty;
            string str_rect_03_peak_target_index_scd = string.Empty;
            string str_rect_04_peak_candidate= string.Empty;
            string str_rect_05_peak_window_size = string.Empty;
            string str_rect_06_edge_position_fst = string.Empty;
            string str_rect_07_edge_position_scd = string.Empty;

            string str_cir_01_damage_tolerance = string.Empty;
            string str_cir_02_treat_as_ellipse = string.Empty;
            string str_cir_03_circle_detection_type = string.Empty;
            string str_cir_04_shrinkage = string.Empty;
            string str_cir_05_outlier_filter = string.Empty;
            string str_cir_06_edge_posistion = string.Empty;
            string str_cir_07_coverage = string.Empty;

          
            if( nFigureType == IFX_FIGURE.PAIR_RCT)
            {
                #region rectangle 
                CMeasurePairRct buff = ((CMeasurePairRct)Ini);

                str_00_TargetName = buff.NICKNAME;
                str_01_figure_type = IFX_RECT_TYPE.ToStringType(buff.RC_TYPE);
                str_02_algo_method = IFX_ALGORITHM.ToStringType(buff.param_00_algorithm);

                str_comm_01_compenA = buff.param_comm_01_compen_A.ToString("F2");
                str_comm_02_compenB = buff.param_comm_02_compen_B.ToString("F2");
                str_comm_03_spc_enhance = buff.param_comm_03_spc_enhance.ToString("N0");
                str_comm_04_refinement = buff.param_comm_04_refinement.ToString("N0");

                str_rect_01_bool_use_auto_peak_Detection = buff.param_01_bool_Use_AutoDetection == true ? "TRUE" : "FALSE";
                str_rect_02_peak_target_index_fst = buff.param_02_peakTargetIndex_fst.ToString("N0");
                str_rect_03_peak_target_index_scd = buff.param_03_peakTargetIndex_scd.ToString("N0");
                str_rect_04_peak_candidate = buff.param_04_peakCandidate.ToString("N0");
                str_rect_05_peak_window_size = buff.param_05_windowSize.ToString("N0");
                str_rect_06_edge_position_fst = buff.param_06_edge_position_fst.ToString("F2");
                str_rect_07_edge_position_scd = buff.param_07_edge_position_scd.ToString("F2");
                
                #endregion
            }
            else if (nFigureType == IFX_FIGURE.PAIR_CIR)
            {
              #region circle
                CMeasurePairCir buff = ((CMeasurePairCir)Ini);

                str_00_TargetName = buff.NICKNAME;
                str_01_figure_type = IFX_FIGURE.ToStringType(IFX_FIGURE.PAIR_CIR); ;
                str_02_algo_method = IFX_ALGORITHM.ToStringType(buff.param_00_algorithm_CIR);

                str_comm_01_compenA = buff.param_comm_01_compen_A.ToString("F2");
                str_comm_02_compenB = buff.param_comm_02_compen_B.ToString("F2");
                str_comm_03_spc_enhance = buff.param_comm_03_spc_enhance.ToString("N0");
                str_comm_04_refinement = buff.param_comm_04_refinement.ToString("N0");

                str_cir_01_damage_tolerance = buff.param_01_DMG_Tol.ToString("F2");
                str_cir_02_treat_as_ellipse = buff.param_02_BOOL_TREAT_AS_ELLIPSE == true ? "TRUE" : "FALSE";
                str_cir_03_circle_detection_type = buff.param_03_CircleDetecType.ToString("N0");
                str_cir_04_shrinkage = buff.param_04_Shrinkage.ToString("F2");
                str_cir_05_outlier_filter = buff.param_05_Outlier_Filter.ToString("F2");
                str_cir_06_edge_posistion = buff.param_06_EdgePos.ToString("F2");
                str_cir_07_coverage = buff.param_07_Coverage;
               #endregion

            }

            DataFigure single = new DataFigure();

            single.temporar_01_measureResult = fResult;
            
            single.global_01_input_file_name = strPathInputImage;
            single.global_02_insp_time = WrapperDateTime.GetTImeCode4Save_YYYY_MM_DD_HH_MM_SS_MMM();

            single.param_00_target_name = str_00_TargetName;
            single.param_01_figure_type = str_01_figure_type;
            single.param_02_algorithm = str_02_algo_method;

            single.param_cir_01_damage_tolerance = str_cir_01_damage_tolerance;
            single.param_cir_02_treat_as_ellipse = str_cir_02_treat_as_ellipse;
            single.param_cir_03_circle_detection_type = str_cir_03_circle_detection_type;
            single.param_cir_04_shrinkage = str_cir_04_shrinkage;
            single.param_cir_05_outlier_filter = str_cir_05_outlier_filter;
            single.param_cir_06_edge_posistion = str_cir_06_edge_posistion;

            single.param_rect_01_bool_use_auto_peak_Detection = str_rect_01_bool_use_auto_peak_Detection;
            single.param_rect_02_peak_target_index_fst = str_rect_02_peak_target_index_fst;
            single.param_rect_03_peak_target_index_scd = str_rect_03_peak_target_index_scd;
            single.param_rect_04_peak_candidate = str_rect_04_peak_candidate;
            single.param_rect_05_peak_window_size = str_rect_05_peak_window_size;
            single.param_rect_06_edge_position_fst = str_rect_06_edge_position_fst;
            single.param_rect_07_edge_position_scd = str_rect_07_edge_position_scd;

            single.param_comm_01_compenA = str_comm_01_compenA;
            single.param_comm_02_compenB = str_comm_02_compenB;
            single.param_comm_03_spc_enhance = str_comm_03_spc_enhance;
            single.param_comm_04_refinement = str_comm_04_refinement;
             
            m_listFigure.Add(single);
        }
        public void AddResult_OVL(int nFigureType, string strFile, double fOLX, double fOLY, CMeasurePairOvl ini)
        {
            DataOverlay single = new DataOverlay();

            single.param_01_figure_type = IFX_FIGURE.ToStringType(nFigureType);
            single.global_01_input_file_name = strFile;
            single.param_00_target_name = ini.NICKNAME;

            single.INSP_METHOD_HOR_EX = IFX_ALGORITHM.ToStringType(ini.algorithm_HOR_EX);
            single.INSP_METHOD_HOR_IN = IFX_ALGORITHM.ToStringType(ini.algorithm_HOR_IN);

            single.INSP_METHOD_VER_IN = IFX_ALGORITHM.ToStringType(ini.algorithm_VER_IN);
            single.INSP_METHOD_VER_EX = IFX_ALGORITHM.ToStringType(ini.algorithm_VER_EX);

            single.INSP_DX = fOLX;
            single.INSP_DY = fOLY;
            m_listOverlay.Add(single);
        }

        public List<double[]> GetListByCycle_FocusMag()
        {
            List<double[]> list = new List<double[]>();

            double[] arr = m_listFocusMag.ToArray();

            
            for( int y = 0; y < MEAS_CYCLE; y++)
            {
                double[] arrFocusMag = new double[MEAS_POINT];

                for( int x = 0; x < MEAS_POINT; x++)
                {
                    int nIndex = y * MEAS_POINT + x;

                    if (nIndex >= arr.Length) continue;
                    arrFocusMag[x] = arr[y * MEAS_POINT + x];
                }
                list.Add(arrFocusMag);
            }
            return list;
        }
        public List<double[]> GetListByCycle_ImageSimilarity()
        {
            List<double[]> list = new List<double[]>();

            

            for (int y = 0; y < MEAS_CYCLE; y++)
            {
                double[] arrSimilarity = new double[MEAS_POINT];

                for (int x = 0; x < MEAS_POINT; x++)
                {
                    int nIndex = y * MEAS_POINT + x;

                    int[] nHistoSrc = m_listHIstogram.ElementAt(0 * MEAS_POINT + x);


                    int[] nCurrentHisto = m_listHIstogram.ElementAt(nIndex);

                    if (nIndex >= m_listHIstogram.Count) continue;

                    arrSimilarity[x] = HC_HISTO_GetDistance(nHistoSrc, nCurrentHisto);
                }
                list.Add(arrSimilarity);
            }
            return list;
        }
        public List<double> GetListBySequence_ImageSimilarity()
        {
            m_list_Similarity.Clear();
            int[] nHistoSrc = m_listHIstogram.ElementAt(0);

            for (int i = 0; i < m_listHIstogram.Count; i++)
            {
                int [] nHistoCurrent = m_listHIstogram.ElementAt(i);
                m_list_Similarity.Add(HC_HISTO_GetDistance(nHistoSrc, nHistoCurrent));
            }

            return m_list_Similarity;
        }
        public List<float[]> GetListByCycle_PTRN_X()
        {
            List<float[]> list = new List<float[]>();

            PointF[] arr = m_listMatchPoint.ToArray();
            float[] arrX = arr.Select(element => element.X).ToArray();
            float[] arrY = arr.Select(element => element.Y).ToArray();

            for (int y = 0; y < MEAS_CYCLE; y++)
            {
                float[] arrPtrn_X = new float[MEAS_POINT];

                for (int x = 0; x < MEAS_POINT; x++)
                {
                    int nIndex = y * MEAS_POINT + x;

                    if (nIndex >= arr.Length) continue;
                    arrPtrn_X[x] = arrX[y * MEAS_POINT + x];
                }
                list.Add(arrPtrn_X);
            }
            return list;
        }
        public List<float[]> GetListByCycle_PTRN_Y()
        {
            List<float[]> list = new List<float[]>();

            PointF[] arr = m_listMatchPoint.ToArray();
            float[] arrX = arr.Select(element => element.X).ToArray();
            float[] arrY = arr.Select(element => element.Y).ToArray();

            for (int y = 0; y < MEAS_CYCLE; y++)
            {
                float[] arrPtrn_Y = new float[MEAS_POINT];

                for (int x = 0; x < MEAS_POINT; x++)
                {
                    int nIndex = y * MEAS_POINT + x;

                    if (nIndex >= arr.Length) continue;
                    arrPtrn_Y[x] = arrY[y * MEAS_POINT + x];
                }
                list.Add(arrPtrn_Y);
            }
            return list;
        }

        public static double /***/HC_HISTO_GetIntersection(int[] QueryGrayHisto, int[] TargetGrayHisto)
        {
            int x = 0;
            double Buffer = 0;
            float SumOfModel = 0;

            //*********************************************************************************************
            //		The Histogram of the intersection
            //*********************************************************************************************
            for (x = 0; x < 256; x++)
            {
                if (QueryGrayHisto[x] < TargetGrayHisto[x])
                {
                    Buffer += QueryGrayHisto[x];
                }
                else if (QueryGrayHisto[x] > TargetGrayHisto[x])
                {
                    Buffer += TargetGrayHisto[x];
                }
                else if (QueryGrayHisto[x] == TargetGrayHisto[x])
                {
                    Buffer += QueryGrayHisto[x];
                }
                SumOfModel += TargetGrayHisto[x];
            }
            //*********************************************************************************************
            // To obtain a fractional match value between 0 to 1
            // the intersection is normalized by the number of pixels in the model histogram;
            //*********************************************************************************************

            Buffer = Buffer / SumOfModel;

            return Buffer;
        }

        public static double /***/HC_HISTO_GetDistance(int[] QueryGrayHisto, int[] TargetGrayHisto)
        {
            int x = 0;
            double Buffer = 0;
 
            //*********************************************************************************************
            //		The Histogram of the Distance
            //*********************************************************************************************
            for (x = 0; x < 256; x++)
            {
                Buffer += Math.Abs(QueryGrayHisto[x] - TargetGrayHisto[x]);
            }

            return Buffer;
        }

        public static void _WriteMeasurementData(CMeasureReport report, string strSavePath, bool bExecution)
        {
            if (report.SequenceIndex == 1) return;
            if (report.INTERRUPT == true) return;
            
            if( report.MEAS_CYCLE != 0 && report.MEAS_CYCLE*report.MEAS_POINT > report.SequenceIndex) 
            {
                return;
            }

            if (report.SequenceIndex == 0) return;

            WrapperExcel ex = new WrapperExcel();

            string[] header = new string[10];

            header[0] = "RECP"/*****/; header[1] = report.INFO_RECIPE; /*********************/ ex.data.Add(header.ToArray());
            header[0] = "PTRN"/*****/; header[1] = report.INFO_PTRN; /***********************/ ex.data.Add(header.ToArray());
            header[0] = "TIME"/*****/; header[1] = report.INFO_TIME; /***********************/ ex.data.Add(header.ToArray());
            header[0] = "PXL_RES"/**/; header[1] = report.INFO_PIXEL_X.ToString("N5"); /*****/ ex.data.Add(header.ToArray());
            header[0] = /************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
            header[0] = /************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
            header[0] = "SUMARRY"/**/; header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());


            List<CMeasureReport.DataFigure> listFigure = report.GetAverage_Figure();
            List<CMeasureReport.DataOverlay> listOverlay = report.GetAverage_Overlay();

            for (int nItem = 0; nItem < listFigure.Count; nItem++)
            {
                string[] single = new string[10];

                CMeasureReport.DataFigure summary = listFigure.ElementAt(nItem);
                
                double fSigam = summary.calcSigma(report.INFO_PIXEL_X) * 3;

                int nACC = summary.TOTAL_COUNT;

                single[0] = "TOTAL"; /***********/single[1] = nACC.ToString("N0");/****/ex.data.Add(single.ToArray());
                single[0] = "TYPE"; /************/single[1] = summary.param_01_figure_type; ex.data.Add(single.ToArray());
                single[0] = "NAME"; /************/single[1] = summary.param_00_target_name; ex.data.Add(single.ToArray());
                single[0] = "MEASURE TYPE";/*****/single[1] = summary.param_02_algorithm; /**/ex.data.Add(single.ToArray());
 
                //*********************************************************************************
                // in case of Rectangle
                //*********************************************************************************
                if (summary.param_01_figure_type != IFX_FIGURE.ToStringType(IFX_FIGURE.PAIR_CIR))
                {
                    single[0] = "WIDTH"; /****************/single[1] = summary.temporar_01_measureResult.ToString("F4"); ex.data.Add(single.ToArray());

                    single[0] = "AUTO_PEAK_DETECTION";/***/single[1] = summary.param_rect_01_bool_use_auto_peak_Detection; ex.data.Add(single.ToArray());
                    single[0] = "PEAK_INDEX_FST"; /*******/single[1] = summary.param_rect_02_peak_target_index_fst; ex.data.Add(single.ToArray());
                    single[0] = "PEAK_INDEX_SCD"; /*******/single[1] = summary.param_rect_03_peak_target_index_scd; ex.data.Add(single.ToArray());
                    single[0] = "PEAK_CANDIDATE"; /*******/single[1] = summary.param_rect_04_peak_candidate; ex.data.Add(single.ToArray());
                    single[0] = "PEAK_WINDOW"; /**********/single[1] = summary.param_rect_05_peak_window_size; ex.data.Add(single.ToArray());
                    single[0] = "EDGE_POS_FST"; /*********/single[1] = summary.param_rect_06_edge_position_fst; ex.data.Add(single.ToArray());
                    single[0] = "EDGE_POS_SCD"; /*********/single[1] = summary.param_rect_07_edge_position_scd; ex.data.Add(single.ToArray());
                    single[0] = "COMPEN_A";/**************/single[1] = summary.param_comm_01_compenA; ex.data.Add(single.ToArray());
                    single[0] = "COMPEN_B";/**************/single[1] = summary.param_comm_02_compenB; ex.data.Add(single.ToArray());
                    single[0] = "SPC_ENHANCE";/***********/single[1] = summary.param_comm_03_spc_enhance; ex.data.Add(single.ToArray());
                    single[0] = "REFINEMENT"; /***********/single[1] = summary.param_comm_04_refinement; ex.data.Add(single.ToArray());
                }
                //*********************************************************************************
                // in case of circle  
                //*********************************************************************************
                else
                {
                    single[0] = "DIAMETER"; single[1] = summary.temporar_01_measureResult.ToString("F4"); ex.data.Add(single.ToArray());

                    single[0] = "DMG IGNORANCE";/***************/ single[1] = summary.param_cir_01_damage_tolerance; ex.data.Add(single.ToArray());
                    single[0] = "TREAT_AS_ELLIPSE";/************/ single[1] = summary.param_cir_02_treat_as_ellipse; ex.data.Add(single.ToArray());
                    single[0] = "AUTO_CIRCLE_DETECTION_TYPE";/**/ single[1] = summary.param_cir_03_circle_detection_type; ex.data.Add(single.ToArray());
                    single[0] = "CIRCLE_SHRINKAGE";/************/ single[1] = summary.param_cir_04_shrinkage; ex.data.Add(single.ToArray());
                    single[0] = "OUTLIER_FILTER"; /*************/ single[1] = summary.param_cir_05_outlier_filter; ex.data.Add(single.ToArray());
                    single[0] = "EDGE_POS";/********************/ single[1] = summary.param_cir_06_edge_posistion; ex.data.Add(single.ToArray());
                    single[0] = "COVERAGE";/********************/ single[1] = summary.param_cir_07_coverage; ex.data.Add(single.ToArray());
                    single[0] = "COMPEN_A";/**************/single[1] = summary.param_comm_01_compenA; ex.data.Add(single.ToArray());
                    single[0] = "COMPEN_B";/**************/single[1] = summary.param_comm_02_compenB; ex.data.Add(single.ToArray());
                    single[0] = "SPC_ENHANCE";/***********/single[1] = summary.param_comm_03_spc_enhance; ex.data.Add(single.ToArray());
                    single[0] = "REFINEMENT"; /***********/single[1] = summary.param_comm_04_refinement; ex.data.Add(single.ToArray());

                }
                single[0] = "FOCUS-MAG";/**/single[1] = report.GetFocusMagAvg().ToString("f2"); ex.data.Add(single.ToArray()); 

                single[0] = "3-SIGMA";/****/single[1] = fSigam.ToString("F4"); /**/ex.data.Add(single.ToArray());
                single[0] = /**************/single[1] = ""; ex.data.Add(single.ToArray());
            }

            header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
            header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());

            //************************************************************************************
            // FOCUS-MAGNITUDE 
            //************************************************************************************

            if (report.MEAS_CYCLE == 0)
            {
                int nFocusIter = report.SequenceIndex;
                string[] arrHeader_FocusMag = new string[nFocusIter + 1];
                arrHeader_FocusMag[0] = "Focus-Mag";

                // header generation
                for (int nColumn = 1; nColumn < arrHeader_FocusMag.Length; nColumn++)
                {
                    arrHeader_FocusMag[nColumn] = "POINT_" + nColumn.ToString("N0");
                }
                ex.data.Add(arrHeader_FocusMag.ToArray());

                //**************************************************************************
 
                // fill data
                string[] arrFocusMag = new string[nFocusIter + 1];
                for (int nColumn = 1; nColumn < arrFocusMag.Length; nColumn++) 
                {
                    string strFocus = "0.00";

                    if (nColumn <= report.m_listFocusMag.Count())
                    {
                        strFocus = report.m_listFocusMag.ElementAt(nColumn - 1).ToString("F2");
                    }
                    arrFocusMag[nColumn] = strFocus;
                }
                ex.data.Add(arrFocusMag);
            }
            else if( report.MEAS_CYCLE > 0)
            {
                int nFocusIter = report.MEAS_POINT;
                string[] arrHeader_FocusMag = new string[nFocusIter + 1];
                arrHeader_FocusMag[0] = "Focus-Mag";

                for (int nColumn = 1; nColumn < arrHeader_FocusMag.Length; nColumn++)
                {
                    arrHeader_FocusMag[nColumn] = "POINT_" + nColumn.ToString("N0");
                }
                ex.data.Add(arrHeader_FocusMag);

                //**************************************************************************
                // iterative data


                List<double[]> listFocusMag = report.GetListByCycle_FocusMag();
                 
                for (int nCycle = 0; nCycle < report.MEAS_CYCLE; nCycle++)
                {
                    string[] strArrFocusMag = new string[nFocusIter + 1];
                    double[] fArrFocusMag = listFocusMag.ElementAt(nCycle);

                    for (int nColumn = 1; nColumn < strArrFocusMag.Length; nColumn++)
                    {
                        strArrFocusMag[0] = "ITER_" + (nCycle + 1).ToString("N0");
                        strArrFocusMag[nColumn] = fArrFocusMag[nColumn - 1].ToString("F4");
                    }
                    ex.data.Add(strArrFocusMag);
                }
            }

            header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
            header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());

            //************************************************************************************
            // Image Similarity
            //************************************************************************************

            if (report.MEAS_CYCLE == 0)
            {
                report.GetAverage_Similarity();

                int nFocusIter = report.SequenceIndex;
                string[] arrHeader_FocusMag = new string[nFocusIter + 1];
                arrHeader_FocusMag[0] = "img-Similarity";

                // header generation
                for (int nColumn = 1; nColumn < arrHeader_FocusMag.Length; nColumn++)
                {
                    arrHeader_FocusMag[nColumn] = "POINT_" + nColumn.ToString("N0");
                }
                ex.data.Add(arrHeader_FocusMag.ToArray());

                //**************************************************************************

                // fill data
                string[] arrSimilarity = new string[nFocusIter + 1];
                for (int nColumn = 1; nColumn < arrSimilarity.Length; nColumn++)
                {
                    string strFocus = "0.00";

                    if (nColumn <= report.m_list_Similarity.Count())
                    {
                        strFocus = report.m_list_Similarity.ElementAt(nColumn - 1).ToString("F2");
                    }
                    arrSimilarity[nColumn] = strFocus;
                }
                ex.data.Add(arrSimilarity);
            }
            else if (report.MEAS_CYCLE > 0)
            {
                int nFocusIter = report.MEAS_POINT;
                string[] arrHeader_FocusMag = new string[nFocusIter + 1];
                arrHeader_FocusMag[0] = "img-Similarity";

                for (int nColumn = 1; nColumn < arrHeader_FocusMag.Length; nColumn++)
                {
                    arrHeader_FocusMag[nColumn] = "POINT_" + nColumn.ToString("N0");
                }
                ex.data.Add(arrHeader_FocusMag);

                //**************************************************************************
                // iterative data


                List<double[]> listSimilarity = report.GetListByCycle_ImageSimilarity();


                for (int nCycle = 0; nCycle < report.MEAS_CYCLE; nCycle++)
                {
                    string[] strArrSimilarity = new string[nFocusIter + 1];
                    double[] fArrSimilarity = listSimilarity.ElementAt(nCycle);

                    for (int nColumn = 1; nColumn < strArrSimilarity.Length; nColumn++)
                    {
                        strArrSimilarity[0] = "ITER_" + (nCycle + 1).ToString("N0");
                        strArrSimilarity[nColumn] = fArrSimilarity[nColumn - 1].ToString("F4");
                    }
                    ex.data.Add(strArrSimilarity);
                }
            }
             
            header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
            header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());

            //*************************************************************************************
            // PTRN_POSITION TRACKING
            //*************************************************************************************
            #region PTRN_POSITION
            for (int nPoint = 0; nPoint < 2; nPoint++ )
            {
                // if nPoint == x or nPoint == Y

                if (report.MEAS_CYCLE == 0)
                {
                    int nPtrnIter = report.m_listMatchPoint.Count;
                    string[] arrHeader_PTRN = new string[nPtrnIter + 1];

                    /***/if (nPoint == 0){arrHeader_PTRN[0] = "PTRN_POS_X";}
                    else if (nPoint == 1){arrHeader_PTRN[0] = "PTRN_POS_Y";}

                    for (int nColumn = 1; nColumn < arrHeader_PTRN.Length; nColumn++)
                    {
                        arrHeader_PTRN[nColumn] = "POINT_" + nColumn.ToString("N0");
                    }
                    ex.data.Add(arrHeader_PTRN.ToArray());

                    //**************************************************************************

                    string[] arrPTRN = new string[nPtrnIter + 1];
                    for (int nColumn = 1; nColumn < arrPTRN.Length; nColumn++)
                    {
                        /***/if (nPoint == 0) { arrPTRN[nColumn] = report.m_listMatchPoint.ElementAt(nColumn - 1).X.ToString("F3"); }
                        else if (nPoint == 1) { arrPTRN[nColumn] = report.m_listMatchPoint.ElementAt(nColumn - 1).Y.ToString("F3"); }
                        
                    }
                    ex.data.Add(arrPTRN);
                }
                else if (report.MEAS_CYCLE > 0)
                {
                    int nPtrnIter = report.MEAS_POINT;
                    string[] arrHeader_PTRN = new string[nPtrnIter + 1];

                    /***/if (nPoint == 0) { arrHeader_PTRN[0] = "PTRN_POS_X"; }
                    else if (nPoint == 1) { arrHeader_PTRN[0] = "PTRN_POS_Y"; }

                    for (int nColumn = 1; nColumn < arrHeader_PTRN.Length; nColumn++)
                    {
                        arrHeader_PTRN[nColumn] = "POINT_" + nColumn.ToString("N0");
                    }
                    ex.data.Add(arrHeader_PTRN);

                    //**************************************************************************
                    // iterative data


                    List<float[]> listPtrn = null;

                    /***/if (nPoint == 0) listPtrn = report.GetListByCycle_PTRN_X();
                    else if (nPoint == 1) listPtrn = report.GetListByCycle_PTRN_Y();

                    for (int nCycle = 0; nCycle < report.MEAS_CYCLE; nCycle++)
                    {
                        string[] strArrPTRN = new string[nPtrnIter + 1];
                        float[] fArrPtrn = listPtrn.ElementAt(nCycle);

                        for (int nColumn = 1; nColumn < strArrPTRN.Length; nColumn++)
                        {
                            strArrPTRN[0] = "ITER_" + (nCycle + 1).ToString("N0");
                            strArrPTRN[nColumn] = fArrPtrn[nColumn - 1].ToString("F4");
                        }
                        ex.data.Add(strArrPTRN);
                    }
                }


                header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
                header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
            }

            #endregion

            //************************************************************************************
            // RAW-DATA-FIGURE
            //************************************************************************************
            if (listFigure.Count != 0)
            {
                //*******************************************************************************
                // Entire Data Chart In detail : Column Generation
                //*******************************************************************************
                int nShotCount = report.SequenceIndex;
                int nItemTotal = report.m_listFigure.Count();
                int nItemCount = nItemTotal / nShotCount;
                int nCycle = report.MEAS_CYCLE;
                int nPoint = report.MEAS_POINT;

                if (report.MEAS_CYCLE == 0)
                {
                    // column header [Empty] + [Index 0]...[Index N-1]
                    string[] arrHeader = new string[nShotCount + 1];
                    arrHeader[0] = "ITEM";

                    for (int nColumn = 1; nColumn < arrHeader.Length; nColumn++)
                    {
                        arrHeader[nColumn] = "ITER_" + nColumn.ToString("N0");
                    }
                    ex.data.Add(arrHeader.ToArray());

                    //*******************************************************************************
                    // Raw Data Listing
                    //*******************************************************************************

                    for (int nItem = 0; nItem < listFigure.Count; nItem++)
                    {
                        CMeasureReport.DataFigure summary = listFigure.ElementAt(nItem);

                        string[] rawData = new string[arrHeader.Length];

                        rawData[0] = summary.param_00_target_name;
                        for (int i = 0; i < summary.TOTAL_COUNT; i++)  // 갯수가 엇박일수 있으니까 돌리는건 지 숫자대로
                        {
                            rawData[i + 1] = summary.listRaw_Figure.ElementAt(i).ToString("F4");
                        }
                        ex.data.Add(rawData.ToArray());
                    }
                }
                else if( report.MEAS_CYCLE > 0)
                {
                    for (int nItem = 0; nItem < listFigure.Count; nItem++)
                    {
                        CMeasureReport.DataFigure summary = listFigure.ElementAt(nItem);

                        string[] arrHeader = new string[ nPoint +1 ];
                        arrHeader[0] = summary.param_00_target_name; ;

                        for (int nColumn = 1; nColumn < arrHeader.Length; nColumn++)
                        {
                            arrHeader[nColumn] = "POINT_" + nColumn.ToString("N0");
                        }
                        ex.data.Add(arrHeader.ToArray());
                        
                        double[] fArrFigure = summary.listRaw_Figure.ToArray();

                        for (int nIter = 0; nIter < nCycle; nIter++ )
                        {
                            arrHeader[0] = "ITER_" + (1+nIter).ToString("N0");
                            for( int x = 0; x < nPoint; x++)
                            {
                                arrHeader[1 + x] = fArrFigure[nIter * nPoint + x].ToString("F4");
                            }
                            ex.data.Add(arrHeader.ToArray());
                        }
                        header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
                    }
                }

              
            }
            //************************************************************************************
            // RAW-DATA-OVERLAY
            //************************************************************************************
            for (int nItem = 0; nItem < listOverlay.Count; nItem++)
            {
                string[] single = new string[10];

                CMeasureReport.DataOverlay summary = listOverlay.ElementAt(nItem);
                COverlay sigmaOVL = summary.calcSigma(report.INFO_PIXEL_X);

                int nACC = summary.TOTAL_COUNT;

                single[0] = "TOTAL"; /***********/single[1] = nACC.ToString("N0");/****/ex.data.Add(single.ToArray());
                single[0] = "TYPE"; /************/single[1] = summary.param_01_figure_type; ex.data.Add(single.ToArray());
                single[0] = "NAME"; /************/single[1] = summary.param_00_target_name; ex.data.Add(single.ToArray());

                single[0] = "MEASURE HOR_IN_";/***/single[1] = summary.INSP_METHOD_HOR_IN; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE HOR_EX_";/***/single[1] = summary.INSP_METHOD_HOR_EX; /**/ex.data.Add(single.ToArray());

                single[0] = "MEASURE VER_IN";/***/single[1] = summary.INSP_METHOD_VER_IN; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE VER_EX";/***/single[1] = summary.INSP_METHOD_VER_EX; /**/ex.data.Add(single.ToArray());

                single[0] = "SIGMA_X";/***/single[1] = sigmaOVL.DX.ToString("F4"); /**/ex.data.Add(single.ToArray());
                single[0] = "SIGMA_Y";/***/single[1] = sigmaOVL.DY.ToString("F4"); /**/ex.data.Add(single.ToArray());
                single[0] = single[1] = ""; ex.data.Add(single.ToArray());
            }
            if (listOverlay.Count != 0)
            {
                //*******************************************************************************
                // Entire Data Chart In detail : Column Generation
                //*******************************************************************************

                // column header [Empty] + [Index 0]...[Index N-1]
                int nIterationCount = listOverlay.ElementAt(0).TOTAL_COUNT;
                int nHeaderLength = nIterationCount + 1;
                string[] arrHeader = new string[nHeaderLength];
                arrHeader[0] = "ITEM";

                for (int nColumn = 1; nColumn < nHeaderLength; nColumn++) { arrHeader[nColumn] = "ITER_" + nColumn.ToString("N0"); }
                ex.data.Add(arrHeader.ToArray());

                //*******************************************************************************
                // Raw Data Listing
                //*******************************************************************************

                for (int nItem = 0; nItem < listOverlay.Count; nItem++)
                {
                    CMeasureReport.DataOverlay summary = listOverlay.ElementAt(nItem);

                    //*******************************************************************
                    // For overlay X
                    string[] rawData_X = new string[nHeaderLength];
                    rawData_X[0] = summary.param_00_target_name + "OLX";
                    for (int i = 0; i < nIterationCount; i++)
                    {
                        rawData_X[i + 1] = summary.listRaw_Overlay.ElementAt(i).DX.ToString("F4");
                    }
                    ex.data.Add(rawData_X.ToArray());

                    //*******************************************************************
                    // for Overlay Y 
                    string[] rawData_Y = new string[nHeaderLength];
                    rawData_Y[0] = summary.param_00_target_name + "OLY";
                    for (int i = 0; i < nIterationCount; i++)
                    {
                        rawData_Y[i + 1] = summary.listRaw_Overlay.ElementAt(i).DY.ToString("F4");
                    }
                    ex.data.Add(rawData_Y.ToArray());
                }
            }

            
            string strPathDir = Path.Combine( strSavePath, WrapperDateTime.GetTimeCode4Save_YYYY_MM_DD());
            WrapperFile.EnsureFolderExsistance(strPathDir);

            string strDumpFileName = WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM() + ".CSV";
            string strDumpFilePath = Path.Combine(strPathDir, strDumpFileName);

            WrapperCVS cvs = new WrapperCVS();
            cvs.SaveCSVFile(strDumpFilePath, ex.data);

            if (bExecution == true)
            {
                try
                {
                    WrapperExcel.ExcuteExcel(strDumpFilePath);
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
            }
        }
        
    }
    
     
    
    
}





