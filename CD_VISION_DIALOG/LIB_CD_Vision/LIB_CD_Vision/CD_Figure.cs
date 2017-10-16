using System;
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
        public PARAM_CONFIG config = new PARAM_CONFIG();

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
        public bool param_comm_04_BOOL_SHOW_RAW_DATA { get; set; }


        public CMeasureMotherFucker() // common creator 
        {
            NICKNAME = string.Empty;
            PIXEL_RES = 0.069;

            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_spc_enhance = 0;
            param_comm_04_BOOL_SHOW_RAW_DATA = false;

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
            ref List<PointF> listEdges_FEX, 
            ref List<PointF> listEdges_FMD,
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated);

        public abstract float rape_Bitch_Direction(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD,
            ref List<PointF>listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated);

        public abstract float rape_asshole_Log(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD,
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated);

        public abstract float MeasureData(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX, 
            ref List<PointF> listEdges_FMD,
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated);

        // Set Delta movement according to pre-calculated delta 170727
        public abstract void SetRelativeMovement(PointF ptDelta);
        // Backup Original Position
        public abstract void CroodinateBackup();
        public abstract void CroodinateRecover();
 
        public abstract byte[] DoPreProcess(byte[] rawImage, int imageW, int imageH, double fSigma, int KSize, Rectangle rc);

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
            param_comm_04_BOOL_SHOW_RAW_DATA = false;

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
            single.param_01_bool_Use_AutoDetection = this.param_01_bool_Use_AutoDetection;
            single.param_02_peakTargetIndex_fst = this.param_02_peakTargetIndex_fst;
            single.param_03_peakTargetIndex_scd = this.param_03_peakTargetIndex_scd;
            single.param_04_peakCandidate = this.param_04_peakCandidate;
            single.param_05_windowSize = this.param_05_windowSize;
            single.param_06_edge_position_fst = this.param_06_edge_position_fst;
            single.param_07_edge_position_scd = this.param_07_edge_position_scd;
             
            single.param_comm_01_compen_A = this.param_comm_01_compen_A;
            single.param_comm_02_compen_B = this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance = this.param_comm_03_spc_enhance;
            single.param_comm_04_BOOL_SHOW_RAW_DATA = this.param_comm_04_BOOL_SHOW_RAW_DATA;


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

         public override byte[] DoPreProcess(byte[] rawImage, int imageW, int imageH, double fSigma, int KSize, Rectangle rc)
        {
            rc.Inflate((int)(rc.Width / 2), (int)(rc.Width / 2));

            double[] fKernel = Computer.HC_FILTER_GenerateGaussianFilter(fSigma, KSize);
            rawImage = Computer.HC_FILTER_ConvolutionWindow(fKernel, rawImage, imageW, imageH, rc);

            bool bSave = false;

            if (bSave == true)
            {
                
                Computer.SaveImage(rawImage, imageW, imageH, "c:\\" +  WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM()+ ".bmp"); 
            }
            return rawImage;
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
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            parseRect rcFST = this.rc_FST;
            parseRect rcSCD = this.rc_SCD;

            //********************************************************************************************
            
            #region PRE-PROCESS
            RectangleF rcfFST = rcFST.ToRectangleF();
            RectangleF rcfSCD = rcSCD.ToRectangleF();
            RectangleF rcMerged = CRect.GetMergedRect(rcfFST, rcfSCD);

            byte[] rawBackup = new byte[imageW * imageH];
            Array.Copy(rawImage, rawBackup, rawImage.Length);

            rawImage = DoPreProcess(rawImage, imageW, imageH, SIGMA, KERNEL, Rectangle.Round(rcMerged));

            if (param_comm_03_spc_enhance == 1)
            {
                RectangleF rcBuff = rcMerged;
                rcBuff.Inflate( rcBuff.Width / 2, rcBuff.Height/2);
                rawImage = Computer.HC_FILTER_STD_Window(rawImage, imageW, imageH, rcBuff, 7);
            }

            rcEstimated = new RectangleF();
            #endregion

            //********************************************************************************************
            CModelLine model_FST = new CModelLine();
            CModelLine model_SCD = new CModelLine();

            CModelLine model_fex = new CModelLine();
            CModelLine model_fmd = new CModelLine();
            CModelLine model_fin = new CModelLine();

            CModelLine model_sex = new CModelLine();
            CModelLine model_smd = new CModelLine();
            CModelLine model_sin = new CModelLine();

            double fDistance = 0;
            p1 = p2 = new PointF(0, 0);

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


                    //listEdges_FEX = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectFS/* Changed */, false, +1); // rectengle change for option difference 
                    //listEdges_FIN = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectFF/* Changed */, false, -1); // rectengle change for option difference 
                    //listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, true);
                    //
                    //listEdges_SEX = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectSF, true, +1);
                    //listEdges_SIN = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectSS, true, -1);
                    //listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, true);
                 }
                else if( param_01_bool_Use_AutoDetection == false)
                {
                    Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_HOR(rawImage, imageW, imageH, rectFirst, true, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                    Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_HOR(rawImage, imageW, imageH, rectSecon, false, listEdges_SEX, listEdges_SMD, listEdges_SIN);
                }
                //listEdges_FEX = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rectFirst, true, (int)this.param_07_edge_position_fst);
                //listEdges_FIN = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rectSecon, false, (int)this.param_08_edge_position_scd);
                
                listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, false, 3);
                listEdges_FMD = Computer.GetList_FilterBy_MajorDistance(listEdges_FMD, false, 3);
                listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, false, 3);
                
                listEdges_SEX = Computer.GetList_FilterBy_MajorDistance(listEdges_SEX, false, 3);
                listEdges_SMD = Computer.GetList_FilterBy_MajorDistance(listEdges_SMD, false, 3);
                listEdges_SIN = Computer.GetList_FilterBy_MajorDistance(listEdges_SIN, false, 3);


                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_fex, param_comm_fitting_thr, listEdges_FEX.Count / 3, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FMD.ToArray(), ref model_fmd, param_comm_fitting_thr, listEdges_FMD.Count / 3, listEdges_FMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_fin, param_comm_fitting_thr, listEdges_FIN.Count / 3, listEdges_FIN.Count * 2);

                CRansac.ransac_Line_fitting(listEdges_SEX.ToArray(), ref model_sex, param_comm_fitting_thr, listEdges_SEX.Count / 3, listEdges_SEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SMD.ToArray(), ref model_smd, param_comm_fitting_thr, listEdges_SMD.Count / 3, listEdges_SMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SIN.ToArray(), ref model_sin, param_comm_fitting_thr, listEdges_SIN.Count / 3, listEdges_SIN.Count * 2);


                // 1이 0 좌표계로간다. --> inner vs outter가 뒤집힌다. 위가 작으니까..
                p1 = CRansac.GetMidPointY_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst); 
                p2 = CRansac.GetMidPointY_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_Y(p1, p2);

                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    listEdges_FEX = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangleF(), (float)model_fex.sy);
                    listEdges_FMD = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangleF(), (float)model_fmd.sy);
                    listEdges_FIN = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangleF(), (float)model_fin.sy);

                    listEdges_SEX = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangleF(), (float)model_sex.sy);
                    listEdges_SMD = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangleF(), (float)model_smd.sy);
                    listEdges_SIN = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangleF(), (float)model_sin.sy);
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
                }
                else if (param_01_bool_Use_AutoDetection == false)
                {
 
                    //listEdges_FEX = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rectFirst, true, (int)this.param_07_edge_position_fst);
                    //listEdges_FIN = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rectSecon, false, (int)this.param_08_edge_position_scd);
                    //
                    //listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, true, 3);
                    //listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, true, 3);


                    //CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_FST, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                    //CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_SCD, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                    //fDistance = _GetDistance(model_FST.sx, model_SCD.sx);
                    //
                    //p1 = new PointF((float)model_FST.sx, (float)model_FST.sy);
                    //p2 = new PointF((float)model_SCD.sx, (float)model_SCD.sy);
                    //
                    //if (this.param_comm_03_BOOL_SHOW_RAW_DATA == false)
                    //{
                    //    listEdges_FEX = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangleF(), (float)model_FST.sx);
                    //    listEdges_FIN = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangleF(), (float)model_SCD.sx);
                    //}


                    Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_VER(rawImage, imageW, imageH, rectFirst, true, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                    Computer.HC_EDGE_GetRawPoints_CARDIN_MULTI_VER(rawImage, imageW, imageH, rectSecon, false, listEdges_SEX, listEdges_SMD, listEdges_SIN);
                }
                listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, true, 3);
                listEdges_FMD = Computer.GetList_FilterBy_MajorDistance(listEdges_FMD, true, 3);
                listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, true, 3);

                listEdges_SEX = Computer.GetList_FilterBy_MajorDistance(listEdges_SEX, true, 3);
                listEdges_SMD = Computer.GetList_FilterBy_MajorDistance(listEdges_SMD, true, 3);
                listEdges_SIN = Computer.GetList_FilterBy_MajorDistance(listEdges_SIN, true, 3);

                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_fex, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FMD.ToArray(), ref model_fmd, param_comm_fitting_thr, listEdges_FMD.Count / 2, listEdges_FMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_fin, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                CRansac.ransac_Line_fitting(listEdges_SEX.ToArray(), ref model_sex, param_comm_fitting_thr, listEdges_SEX.Count / 2, listEdges_SEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SMD.ToArray(), ref model_smd, param_comm_fitting_thr, listEdges_SMD.Count / 2, listEdges_SMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SIN.ToArray(), ref model_sin, param_comm_fitting_thr, listEdges_SIN.Count / 2, listEdges_SIN.Count * 2);


                // 1이 0 좌표계로간다. --> inner vs outter가 뒤집힌다. 위가 작으니까..
                p1 = CRansac.GetMidPointX_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointX_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_X(p1, p2);

                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    listEdges_FEX = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangleF(), (float)model_fex.sx);
                    listEdges_FMD = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangleF(), (float)model_fmd.sx);
                    listEdges_FIN = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangleF(), (float)model_fin.sx);

                    listEdges_SEX = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangleF(), (float)model_sex.sx);
                    listEdges_SMD = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangleF(), (float)model_smd.sx);
                    listEdges_SIN = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangleF(), (float)model_sin.sx);
                }
#endregion
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
                #region PAIR_DIA
                //****************************************************************
                // Get fucking target regions
                int regionW = (int)(rcFST.Width * 1.5);

                //********************************************************
                // Get base axis for each digonal rectangle points 

                listEdges_FEX = CLine.GetLyingPointsFromVariationY(rcFST.LT, rcFST.LB);
                listEdges_FIN = CLine.GetLyingPointsFromVariationY(rcSCD.LT, rcSCD.LB);

                listEdges_FEX = Computer.HC_EDGE_GetRawPoints_CARDIN_DIA(rawImage, imageW, imageH, regionW, listEdges_FEX, (int)this.param_06_edge_position_fst, true);
                listEdges_FIN = Computer.HC_EDGE_GetRawPoints_CARDIN_DIA(rawImage, imageW, imageH, regionW, listEdges_FIN, (int)this.param_07_edge_position_scd, false);

                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_FST, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_SCD, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                p1 = new PointF((float)model_FST.sx, (float)model_FST.sy);
                p2 = new PointF((float)model_SCD.sx, (float)model_SCD.sy);

                fDistance = CPoint.GetDistance(p1, p2);
                 
                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    CLine line1 = CRansac.GetFittedLine_VER(model_FST, rc_FST.ToRectangleF());
                    CLine line2 = CRansac.GetFittedLine_VER(model_SCD, rc_SCD.ToRectangleF());

                    line1 = line1.GetExpandedLine_CrossLineBased(line1, rc_FST.LT, rc_FST.RT, rc_FST.LB, rc_FST.RB);
                    line2 = line2.GetExpandedLine_CrossLineBased(line2, rc_SCD.LT, rc_SCD.RT, rc_SCD.LB, rc_SCD.RB);

                    listEdges_FEX = CLine.GetLyingPointsFromVariationY(line1);
                    listEdges_FIN = CLine.GetLyingPointsFromVariationY(line2);
                }
                
                
                #endregion
            }
           
            return Convert.ToSingle(fDistance);
        }

        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        public override float rape_Bitch_Direction(byte[] rawImage, int imageW, int imageH,
        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            parseRect rcFST = this.rc_FST;
            parseRect rcSCD = this.rc_SCD;

            //********************************************************************************************

            #region PRE-PROCESS

            RectangleF rcfFST = rcFST.ToRectangleF();
            RectangleF rcfSCD = rcSCD.ToRectangleF();
            RectangleF rcMerged = CRect.GetMergedRect(rcfFST, rcfSCD);

            byte[] rawBackup = new byte[imageW * imageH];
            Array.Copy(rawImage, rawBackup, rawImage.Length);

            rawImage = DoPreProcess(rawImage, imageW, imageH, SIGMA, KERNEL, Rectangle.Round(rcMerged));

            rcEstimated = new RectangleF();

            #endregion 

            if (param_comm_03_spc_enhance == 1)
            {
                RectangleF rcBuff = rcMerged;
                rcBuff.Inflate(rcBuff.Width / 2, rcBuff.Height / 2);
                rawImage = Computer.HC_FILTER_STD_Window(rawImage, imageW, imageH, rcBuff, 7);
            }
            //********************************************************************************************

            double fDistance = 0;

            // set default value 170720
            p1 = p2 = new PointF(0, 0);

            //********************************************************
            // Get base axis for each digonal rectangle points 

            CModelLine model_FST = new CModelLine();
            CModelLine model_SCD = new CModelLine();

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
            else if (RC_TYPE == IFX_RECT_TYPE.DIR_HOR)
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
                }
                else if (param_01_bool_Use_AutoDetection == false)
                {
                    /***/if (this.param_00_algorithm == IFX_ALGORITHM.DIR_IN)
                    {
                        //listEdges_FEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectFirst, false, this.param_07_edge_position_fst);
                        //listEdges_FIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectSecon, true, this.param_08_edge_position_scd); 

                        Computer.HC_EDGE_GetRawPoints_2ndDeriv_MULTI_VER(rawImage, imageW, imageH, rectFirst, false, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                        Computer.HC_EDGE_GetRawPoints_2ndDeriv_MULTI_VER(rawImage, imageW, imageH, rectSecon, true, listEdges_SEX, listEdges_SMD, listEdges_SIN);
                    }
                    else if (this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                    {
                        //listEdges_FEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectFirst, true, this.param_07_edge_position_fst);
                        //listEdges_FIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_VER(rawImage, imageW, imageH, rectSecon, false, this.param_08_edge_position_scd); 
                        Computer.HC_EDGE_GetRawPoints_2ndDeriv_MULTI_VER(rawImage, imageW, imageH, rectFirst, true, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                        Computer.HC_EDGE_GetRawPoints_2ndDeriv_MULTI_VER(rawImage, imageW, imageH, rectSecon, false, listEdges_SEX, listEdges_SMD, listEdges_SIN);
                    }
                }

                //listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, false, 3);
                //listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, false, 3);
                //
                //
                //CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_FST, param_comm_fitting_thr, listEdges_FEX.Count / 3, listEdges_FEX.Count * 2);
                //CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_SCD, param_comm_fitting_thr, listEdges_FIN.Count / 3, listEdges_FIN.Count * 2);
                //
                //fDistance = _GetDistance(model_SCD.sy, model_FST.sy);
                //
                //p1 = new PointF((float)model_FST.sx, (float)model_FST.sy);
                //p2 = new PointF((float)model_SCD.sx, (float)model_SCD.sy);

                listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, false, 3);
                listEdges_FMD = Computer.GetList_FilterBy_MajorDistance(listEdges_FMD, false, 3);
                listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, false, 3);

                listEdges_SEX = Computer.GetList_FilterBy_MajorDistance(listEdges_SEX, false, 3);
                listEdges_SMD = Computer.GetList_FilterBy_MajorDistance(listEdges_SMD, false, 3);
                listEdges_SIN = Computer.GetList_FilterBy_MajorDistance(listEdges_SIN, false, 3);

                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_fex, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FMD.ToArray(), ref model_fmd, param_comm_fitting_thr, listEdges_FMD.Count / 2, listEdges_FMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_fin, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                CRansac.ransac_Line_fitting(listEdges_SEX.ToArray(), ref model_sex, param_comm_fitting_thr, listEdges_SEX.Count / 2, listEdges_SEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SMD.ToArray(), ref model_smd, param_comm_fitting_thr, listEdges_SMD.Count / 2, listEdges_SMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SIN.ToArray(), ref model_sin, param_comm_fitting_thr, listEdges_SIN.Count / 2, listEdges_SIN.Count * 2);


                // 1이 0 좌표계로간다. --> inner vs outter가 뒤집힌다. 위가 작으니까..
                p1 = CRansac.GetMidPointY_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointY_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_Y(p1, p2);

                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    listEdges_FEX = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangle(), (float)model_fex.sy);
                    listEdges_FMD = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangle(), (float)model_fmd.sy);
                    listEdges_FIN = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangle(), (float)model_fin.sy);

                    listEdges_SEX = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangle(), (float)model_sex.sy);
                    listEdges_SMD = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangle(), (float)model_smd.sy);
                    listEdges_SIN = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangle(), (float)model_sin.sy);
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
                }
                else if (param_01_bool_Use_AutoDetection == false)
                {
                    /***/if (this.param_00_algorithm == IFX_ALGORITHM.DIR_IN)
                    {
                        //listEdges_FEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectFirst, false, this.param_07_edge_position_fst);
                        //listEdges_FIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectSecon, true, this.param_08_edge_position_scd); 

                        Computer.HC_EDGE_GetRawPoints_2ndDeriv_MULTI_HOR(rawImage, imageW, imageH, rectFirst, false, listEdges_FIN, listEdges_FMD, listEdges_FEX);
                        Computer.HC_EDGE_GetRawPoints_2ndDeriv_MULTI_HOR(rawImage, imageW, imageH, rectSecon, true, listEdges_SIN, listEdges_SMD, listEdges_SEX);

                    }
                    else if (this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                    {
                        Computer.HC_EDGE_GetRawPoints_2ndDeriv_MULTI_HOR(rawImage, imageW, imageH, rectFirst, true, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                        Computer.HC_EDGE_GetRawPoints_2ndDeriv_MULTI_HOR(rawImage, imageW, imageH, rectSecon, false, listEdges_SEX, listEdges_SMD, listEdges_SIN);

                        //listEdges_FEX = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectFirst, true, this.param_07_edge_position_fst);
                        //listEdges_FIN = Computer.HC_EDGE_GetRawPoints_2ndDeriv_HOR(rawImage, imageW, imageH, rectSecon, false, this.param_08_edge_position_scd); 
                    }
                }

                listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, true, 3);
                listEdges_FMD = Computer.GetList_FilterBy_MajorDistance(listEdges_FMD, true, 3);
                listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, true, 3);

                listEdges_SEX = Computer.GetList_FilterBy_MajorDistance(listEdges_SEX, true, 3);
                listEdges_SMD = Computer.GetList_FilterBy_MajorDistance(listEdges_SMD, true, 3);
                listEdges_SIN = Computer.GetList_FilterBy_MajorDistance(listEdges_SIN, true, 3);

                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_fex, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FMD.ToArray(), ref model_fmd, param_comm_fitting_thr, listEdges_FMD.Count / 2, listEdges_FMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_fin, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                CRansac.ransac_Line_fitting(listEdges_SEX.ToArray(), ref model_sex, param_comm_fitting_thr, listEdges_SEX.Count / 2, listEdges_SEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SMD.ToArray(), ref model_smd, param_comm_fitting_thr, listEdges_SMD.Count / 2, listEdges_SMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SIN.ToArray(), ref model_sin, param_comm_fitting_thr, listEdges_SIN.Count / 2, listEdges_SIN.Count * 2);

                //CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_FST, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                //CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_SCD, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);
                //
                //fDistance = _GetDistance(model_SCD.sx, model_FST.sx);

                //p1 = new PointF((float)model_FST.sx, (float)model_FST.sy);
                //p2 = new PointF((float)model_SCD.sx, (float)model_SCD.sy);

                p1 = CRansac.GetMidPointX_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointX_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_X(p1, p2);
                
                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    listEdges_FEX = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangle(), (float)model_fex.sx);
                    listEdges_FMD = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangle(), (float)model_fmd.sx);
                    listEdges_FIN = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangle(), (float)model_fin.sx);

                    listEdges_SEX = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangle(), (float)model_sex.sx);
                    listEdges_SMD = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangle(), (float)model_smd.sx);
                    listEdges_SIN = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangle(), (float)model_sin.sx);
                }
                #endregion
            }
            else if (RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
                #region DIAGONAL
                int regionW = (int)(rcFST.Width * 1.5);

                listEdges_FEX = CLine.GetLyingPointsFromVariationY(rcFST.LT, rcFST.LB);
                listEdges_FIN = CLine.GetLyingPointsFromVariationY(rcSCD.LT, rcSCD.LB);

                List<double> fListFST = new List<double>();
                List<double> fListSCD = new List<double>();

                List<PointF> TempFST = new List<PointF>();
                List<PointF> TempSCD = new List<PointF>();

                if( this.param_00_algorithm == IFX_ALGORITHM.DIR_IN ) 
                {
                    fListFST = Computer.HC_EDGE_GetRawPoints_2ndDeriv_DIA(listEdges_FEX, regionW, rawImage, imageW, imageH, false, (int)this.param_06_edge_position_fst, false);

                    for (int i = 0; i < listEdges_FEX.Count; i++)
                    {
                        PointF ptFST = listEdges_FEX.ElementAt(i);

                        TempFST.Add(CPoint.OffsetPoint(ptFST, fListFST.ElementAt(i), 0));
                    }
                }
                else if( this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                {
                    listEdges_FEX = CLine.GetLyingPointsFromVariationY(rcFST.RT, rcFST.RB);

                    fListFST = Computer.HC_EDGE_GetRawPoints_2ndDeriv_DIA(listEdges_FEX, regionW, rawImage, imageW, imageH, false, (int)this.param_06_edge_position_fst, true);

                    for (int i = 0; i < listEdges_FEX.Count; i++)
                    {
                        PointF ptFST = listEdges_FEX.ElementAt(i);

                        TempFST.Add(CPoint.OffsetPoint(ptFST, -fListFST.ElementAt(i), 0));
                    }
                }
                // 이거 왜 씨발 두개지??? 왜 놔눠 놨지?? 대각선 ... 아놔.. 
                if (this.param_00_algorithm == IFX_ALGORITHM.DIR_IN)
                {
                    fListSCD = Computer.HC_EDGE_GetRawPoints_2ndDeriv_DIA(listEdges_FIN, regionW, rawImage, imageW, imageH, false, (int)this.param_07_edge_position_scd, true);
                }
                else if (this.param_00_algorithm == IFX_ALGORITHM.DIR_EX)
                {
                    fListSCD = Computer.HC_EDGE_GetRawPoints_2ndDeriv_DIA(listEdges_FIN, regionW, rawImage, imageW, imageH, false, (int)this.param_07_edge_position_scd, false);
                }
                
                //for (int i = 0; i < listEdgesSCD.Count; i++)
                //{
                //    PointF ptSCD = listEdgesSCD.ElementAt(i);
                //
                //    TempSCD.Add(CPoint.OffsetPoint(ptSCD, fListSCD.ElementAt(i), 0));
                //}

                listEdges_FEX = TempFST.ToList();
                listEdges_FIN = TempSCD.ToList();

                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_FST, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_SCD, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                p1 = new PointF((float)model_FST.sx, (float)model_FST.sy);
                p2 = new PointF((float)model_SCD.sx, (float)model_SCD.sy);

                fDistance = CPoint.GetDistance(p1, p2);
      
                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    listEdges_FEX = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangle(), (float)model_FST.sx);
                    listEdges_FIN = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangle(), (float)model_SCD.sx);
                }
                #endregion
            }
 
            return Convert.ToSingle(fDistance);
        }

        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        public override float rape_asshole_Log(byte[] rawImage, int imageW, int imageH,
        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            // pre-processing 
            //********************************************************************************************

            #region PRE-PROCESS
            parseRect rcFST = this.rc_FST;
            parseRect rcSCD = this.rc_SCD;

            RectangleF rcMerged = CRect.GetMergedRect(rc_FST.ToRectangleF(), rc_SCD.ToRectangleF());

            byte[] rawBackup = new byte[imageW * imageH];
            Array.Copy(rawImage, rawBackup, rawImage.Length);

            rawImage = DoPreProcess(rawImage, imageW, imageH, SIGMA,KERNEL, Rectangle.Round(rcMerged));
            rcEstimated = new RectangleF();

            #endregion 

            if (param_comm_03_spc_enhance == 1)
            {
                RectangleF rcBuff = rcMerged;
                rcBuff.Inflate(rcBuff.Width / 2, rcBuff.Height / 2);
                rawImage = Computer.HC_FILTER_STD_Window(rawImage, imageW, imageH, rcBuff, 7);
            }
            //********************************************************************************************

            CModelLine model_FST = new CModelLine();
            CModelLine model_SCD = new CModelLine();

            CModelLine model_fex = new CModelLine();
            CModelLine model_fmd = new CModelLine();
            CModelLine model_fin = new CModelLine();

            CModelLine model_sex = new CModelLine();
            CModelLine model_smd = new CModelLine();
            CModelLine model_sin = new CModelLine();


            double fDistance = 0;
            // set default value 170720
            p1 = p2 = new PointF(0, 0);

            if (CRect.isValid(ref rcMerged, imageW, imageH) == false) return 0;
            
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

                    listEdges_FEX = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectFS/* Changed */, false, +1); // rectengle change for option difference 
                    listEdges_FIN = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectFF/* Changed */, false, -1); // rectengle change for option difference 
                    listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, true);


                    listEdges_SEX = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectSF, true, +1);
                    listEdges_SIN = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectSS, true, -1);
                    listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, true);
                }
                else if( param_01_bool_Use_AutoDetection == false )
                {
                    Computer.HC_EDGE_GetRawPoints_HOR_LOG_MULTI_Sign(rawImage, imageW, imageH, rectFirst, false, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                    ; Computer.HC_EDGE_GetRawPoints_HOR_LOG_MULTI_Sign(rawImage, imageW, imageH, rectSecon, true, listEdges_SEX, listEdges_SMD, listEdges_SIN);
                }

                // without major cutting, fucking error will be meet for the fucking focused image. 170906

                listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, false, 3);
                listEdges_FMD = Computer.GetList_FilterBy_MajorDistance(listEdges_FMD, false, 3);
                listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, false, 3);
                
                listEdges_SEX = Computer.GetList_FilterBy_MajorDistance(listEdges_SEX, false, 3);
                listEdges_SMD = Computer.GetList_FilterBy_MajorDistance(listEdges_SMD, false, 3);
                listEdges_SIN = Computer.GetList_FilterBy_MajorDistance(listEdges_SIN, false, 3);

                //stEdges_FEX = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectFirst, false, (int)this.param_07_edge_position_fst,0);
                //stEdges_FIN = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rectSecon, true, (int)this.param_08_edge_position_scd, 1);
                //listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, false, 3);
                //listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, false, 3);

                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_fex, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FMD.ToArray(), ref model_fmd, param_comm_fitting_thr, listEdges_FMD.Count / 2, listEdges_FMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_fin, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                CRansac.ransac_Line_fitting(listEdges_SEX.ToArray(), ref model_sex, param_comm_fitting_thr, listEdges_SEX.Count / 2, listEdges_SEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SMD.ToArray(), ref model_smd, param_comm_fitting_thr, listEdges_SMD.Count / 2, listEdges_SMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SIN.ToArray(), ref model_sin, param_comm_fitting_thr, listEdges_SIN.Count / 2, listEdges_SIN.Count * 2);


                // 1이 0 좌표계로간다. --> inner vs outter가 뒤집힌다. 위가 작으니까..
                p1 = CRansac.GetMidPointY_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointY_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_Y(p1, p2);

                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    listEdges_FEX = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangle(), (float)model_fex.sy);
                    listEdges_FMD = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangle(), (float)model_fmd.sy);
                    listEdges_FIN = Computer.ReplacePointList_Absolute_Y(rcFST.ToRectangle(), (float)model_fin.sy);

                    listEdges_SEX = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangle(), (float)model_sex.sy);
                    listEdges_SMD = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangle(), (float)model_smd.sy);
                    listEdges_SIN = Computer.ReplacePointList_Absolute_Y(rcSCD.ToRectangle(), (float)model_sin.sy);
                }
                #endregion
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_VER) // verified 170608
            {
                #region 

                int rccW = (int)rcMerged.Width;
                int rccH = (int)rcMerged.Height;
                byte[] rawCropFucker = Computer.HC_CropImage(rawBackup, imageW, imageH, (int)rcMerged.X, (int)rcMerged.Y, rccW, rccH);

                RectangleF rcBuff_FST = rcFST.ToRectangleF();
                RectangleF rcBuff_SCD = rcSCD.ToRectangleF();

                RectangleF rectFirst = rcFST.ToRectangleF();
                RectangleF rectSecon = rcSCD.ToRectangleF();

                if ( param_01_bool_Use_AutoDetection == true)
                {
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

                    listEdges_FEX = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rectFS/* Changed */, false, 1); // rectengle change for option difference 
                    listEdges_FIN = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rectFF/* Changed */, false, -1); // rectengle change for option difference 
                    listEdges_FMD = CPoint.GetInterMediateList(listEdges_FEX, listEdges_FIN, false);

                    listEdges_SEX = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rectSF, true, 1);
                    listEdges_SIN = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rectSS, true, -1);
                    listEdges_SMD = CPoint.GetInterMediateList(listEdges_SEX, listEdges_SIN, false);
                }
                else if (param_01_bool_Use_AutoDetection == false)
                {
                    Computer.HC_EDGE_GetRawPoints_VER_LOG_MULTI_Sign(rawImage, imageW, imageH, rectFirst, false, listEdges_FEX, listEdges_FMD, listEdges_FIN);
                    Computer.HC_EDGE_GetRawPoints_VER_LOG_MULTI_Sign(rawImage, imageW, imageH, rectSecon, false, listEdges_SEX, listEdges_SMD, listEdges_SIN);
                }


                listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, true, 3);
                listEdges_FMD = Computer.GetList_FilterBy_MajorDistance(listEdges_FMD, true, 3);
                listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, true, 3);

                listEdges_SEX = Computer.GetList_FilterBy_MajorDistance(listEdges_SEX, true, 3);
                listEdges_SMD = Computer.GetList_FilterBy_MajorDistance(listEdges_SMD, true, 3);
                listEdges_SIN = Computer.GetList_FilterBy_MajorDistance(listEdges_SIN, true, 3);


                //listEdges_FEX = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rectFirst, false, (int)this.param_07_edge_position_fst,0);
                //listEdges_FIN = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rectSecon, true,(int)this.param_08_edge_position_scd,1);
                //
                //listEdges_FEX = Computer.GetList_FilterBy_MajorDistance(listEdges_FEX, true, 3);
                //listEdges_FIN = Computer.GetList_FilterBy_MajorDistance(listEdges_FIN, true, 3);
                //
                //CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_FST, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                //CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_SCD, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_fex, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FMD.ToArray(), ref model_fmd, param_comm_fitting_thr, listEdges_FMD.Count / 2, listEdges_FMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_fin, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                CRansac.ransac_Line_fitting(listEdges_SEX.ToArray(), ref model_sex, param_comm_fitting_thr, listEdges_SEX.Count / 2, listEdges_SEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SMD.ToArray(), ref model_smd, param_comm_fitting_thr, listEdges_SMD.Count / 2, listEdges_SMD.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_SIN.ToArray(), ref model_sin, param_comm_fitting_thr, listEdges_SIN.Count / 2, listEdges_SIN.Count * 2);


                p1 = CRansac.GetMidPointX_by_Ratio(model_fex, model_fin, (double)1.0 - param_06_edge_position_fst);
                p2 = CRansac.GetMidPointX_by_Ratio(model_sex, model_sin, (double)param_07_edge_position_scd);

                fDistance = CPoint.GetDistance_Only_X(p1, p2);

                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    listEdges_FEX = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangle(), (float)model_fex.sx);
                    listEdges_FMD = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangle(), (float)model_fmd.sx);
                    listEdges_FIN = Computer.ReplacePointList_Absolute_X(rcFST.ToRectangle(), (float)model_fin.sx);

                    listEdges_SEX = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangle(), (float)model_sex.sx);
                    listEdges_SMD = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangle(), (float)model_smd.sx);
                    listEdges_SIN = Computer.ReplacePointList_Absolute_X(rcSCD.ToRectangle(), (float)model_sin.sx);
                }
                #endregion
            }
            else if (this.RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
            {
                #region
                //****************************************************************
                // Get fucking target regions
                int regionW = (int)(rcFST.Width * 1.5);

                //********************************************************
                // Get base axis for each digonal rectangle points 

                listEdges_FEX = CLine.GetLyingPointsFromVariationY(rcFST.LT, rcFST.LB);
                listEdges_FIN = CLine.GetLyingPointsFromVariationY(rcSCD.LT, rcSCD.LB);

                listEdges_FEX = Computer.HC_EDGE_GetRawPoints_DIA_LOG_Sign(rawImage, imageW, imageH, regionW, listEdges_FEX, (int)this.param_06_edge_position_fst);
                listEdges_FIN = Computer.HC_EDGE_GetRawPoints_DIA_LOG_Sign(rawImage, imageW, imageH, regionW, listEdges_FIN, (int)this.param_07_edge_position_scd);
                
                CRansac.ransac_Line_fitting(listEdges_FEX.ToArray(), ref model_FST, param_comm_fitting_thr, listEdges_FEX.Count / 2, listEdges_FEX.Count * 2);
                CRansac.ransac_Line_fitting(listEdges_FIN.ToArray(), ref model_SCD, param_comm_fitting_thr, listEdges_FIN.Count / 2, listEdges_FIN.Count * 2);

                p1 = new PointF((float)model_FST.sx, (float)model_FST.sy);
                p2 = new PointF((float)model_SCD.sx, (float)model_SCD.sy);

                fDistance = CPoint.GetDistance(p1, p2);

                if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
                    {
                        CLine line1 = CRansac.GetFittedLine_VER(model_FST, rcFST.ToRectangleF());
                        CLine line2 = CRansac.GetFittedLine_VER(model_SCD, rcSCD.ToRectangleF());

                        line1 = line1.GetExpandedLine_CrossLineBased(line1, rcFST.LT, rcFST.RT, rcFST.LB, rcFST.RB);
                        line2 = line2.GetExpandedLine_CrossLineBased(line2, rcSCD.LT, rcSCD.RT, rcSCD.LB, rcSCD.RB);

                        listEdges_FEX = CLine.GetLyingPointsFromVariationY(line1);
                        listEdges_FIN = CLine.GetLyingPointsFromVariationY(line2);
                    }
                }
                #endregion
            }
            return Convert.ToSingle(fDistance);
        }

        public override float MeasureData(byte[] rawImage, int imageW, int imageH, ref List<PointF> listEdges_FEX,ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,ref List<PointF> listEdges_SMD,ref List<PointF> listEdges_SIN, out PointF P1, out PointF P2, out RectangleF rcEstimated)
        {
            #region EMPTY_IMPEMEMENTATION
            double fDistance = 0;
            // Set default value 170720
            P1 = P2 = new PointF(0, 0);
            rcEstimated = new RectangleF();

            /***/if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN)) 
            {
                fDistance = this.rape_Pussy_Cardin(rawImage, imageW, imageH, 
                    ref listEdges_FEX,
                    ref listEdges_FMD,
                    ref listEdges_FIN,
                    ref listEdges_SEX,
                    ref listEdges_SMD,
                    ref listEdges_SIN,
                    out P1, out P2, out rcEstimated);
            }
            else if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_IN)||
                     this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_EX)) 
            {
                fDistance = this.rape_Bitch_Direction(rawImage, imageW, imageH,
                    ref listEdges_FEX,
                    ref listEdges_FMD,
                    ref listEdges_FIN,
                    ref listEdges_SEX,
                    ref listEdges_SMD,
                    ref listEdges_SIN,
                    out P1, out P2, out rcEstimated); 
            }
            else if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.MEXHAT)) 
            {
                fDistance = this.rape_asshole_Log(rawImage, imageW, imageH,
                    ref listEdges_FEX,
                    ref listEdges_FMD,
                    ref listEdges_FIN,
                    ref listEdges_SEX,
                    ref listEdges_SMD,
                    ref listEdges_SIN,
                    out P1, out P2, out rcEstimated); 
            }

            fDistance *= PIXEL_RES;
            fDistance *= this.param_comm_01_compen_A;
            fDistance += this.param_comm_02_compen_B;

            return Convert.ToSingle(fDistance);
            #endregion
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

        public int algorithm_HOR_IN_L { get; set; }
        public int algorithm_HOR_IN_R { get; set; }
        public int algorithm_HOR_EX_L { get; set; }
        public int algorithm_HOR_EX_R { get; set; }

        public int algorithm_VER_IN_T { get; set; }
        public int algorithm_VER_IN_B { get; set; }
        public int algorithm_VER_EX_T { get; set; }
        public int algorithm_VER_EX_B { get; set; }

        public int ZigZagIN { get; set; }
        public int ZigZagEX { get; set; }

        public double param_01_edge_position_hor_in_L { get; set; }
        public double param_02_edge_position_hor_in_R { get; set; }
        public double param_03_edge_position_hor_ex_L { get; set; }
        public double param_04_edge_position_hor_ex_R { get; set; }

        public double param_05_edge_position_ver_in_T { get; set; }
        public double param_06_edge_position_ver_in_B { get; set; }
        public double param_07_edge_position_ver_ex_T { get; set; }
        public double param_08_edge_position_ver_ex_B { get; set; }
        
        public CMeasurePairOvl()
        {
            ZigZagEX = ZigZagIN = 0;
            NICKNAME = "OVL";
            algorithm_HOR_IN_L = algorithm_HOR_IN_R = algorithm_VER_IN_T = algorithm_VER_IN_B = 0;
 
            RC_VER_IN.RC_TYPE = IFX_RECT_TYPE.DIR_HOR;
            RC_VER_EX.RC_TYPE = IFX_RECT_TYPE.DIR_HOR;
                                              
            RC_HOR_IN.RC_TYPE = IFX_RECT_TYPE.DIR_VER;
            RC_HOR_EX.RC_TYPE = IFX_RECT_TYPE.DIR_VER;

            param_01_edge_position_hor_in_L = 0;
            param_02_edge_position_hor_in_R = 0;
            param_03_edge_position_hor_ex_L = 0;
            param_04_edge_position_hor_ex_R = 0;
            
            param_05_edge_position_ver_in_T = 0;
            param_06_edge_position_ver_in_B = 0;
            param_07_edge_position_ver_ex_T = 0;
            param_08_edge_position_ver_ex_B = 0;

            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_spc_enhance = 0;
            param_comm_04_BOOL_SHOW_RAW_DATA = false;

        }

        public CMeasurePairOvl CopyTo()
        {
            CMeasurePairOvl single = new CMeasurePairOvl();

            single.RC_HOR_IN = this.RC_HOR_IN.CopyTo();
            single.RC_HOR_EX = this.RC_HOR_EX.CopyTo();
            single.RC_VER_IN = this.RC_VER_IN.CopyTo();
            single.RC_VER_EX = this.RC_VER_EX.CopyTo();

            single.algorithm_HOR_IN_L = this.algorithm_HOR_IN_L;
            single.algorithm_HOR_IN_R = this.algorithm_HOR_IN_R;
            single.algorithm_HOR_EX_L = this.algorithm_HOR_EX_L;
            single.algorithm_HOR_EX_R = this.algorithm_HOR_EX_R;

            single.algorithm_VER_IN_T = this.algorithm_VER_IN_T;
            single.algorithm_VER_IN_B = this.algorithm_VER_IN_B;
            single.algorithm_VER_EX_T = this.algorithm_VER_EX_T;
            single.algorithm_VER_EX_B = this.algorithm_VER_EX_B;

            single.ZigZagIN = this.ZigZagIN;
            single.ZigZagEX = this.ZigZagEX;

            single.NICKNAME = this.NICKNAME;
            
            single.param_01_edge_position_hor_in_L  = this.param_01_edge_position_hor_in_L;
            single.param_02_edge_position_hor_in_R  = this.param_02_edge_position_hor_in_R;
            single.param_03_edge_position_hor_ex_L  = this.param_03_edge_position_hor_ex_L;
            single.param_04_edge_position_hor_ex_R  = this.param_04_edge_position_hor_ex_R;
                                                    
            single.param_05_edge_position_ver_in_T  = this.param_05_edge_position_ver_in_T;
            single.param_06_edge_position_ver_in_B  = this.param_06_edge_position_ver_in_B;
            single.param_07_edge_position_ver_ex_T  = this.param_07_edge_position_ver_ex_T;
            single.param_08_edge_position_ver_ex_B  = this.param_08_edge_position_ver_ex_B;

            
            single.param_comm_04_BOOL_SHOW_RAW_DATA = this.param_comm_04_BOOL_SHOW_RAW_DATA;

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
            RC_HOR_IN.SetRelativeMovement(ptDelta);
            RC_HOR_EX.SetRelativeMovement(ptDelta);
            RC_VER_IN.SetRelativeMovement(ptDelta);
            RC_VER_EX.SetRelativeMovement(ptDelta);
        }


        public override string GetMeasurementCategory()
        {
            // Overlay Position 별 Setting Value가 다를 수 있으니,,, 
            // Multi case로 대응해야 되겠다.  170412 

            //return IFX_ALGORITHM.ToStringType(measure_LFT).Split('_')[0];
            return string.Empty;
        }

        public override byte[] DoPreProcess(byte[] rawImage, int imageW, int imageH, double fSigma, int KSize, Rectangle rc)
        {
            rc.Inflate((int)(rc.Width / 2), (int)(rc.Width / 2));

            double[] fKernel = Computer.HC_FILTER_GenerateGaussianFilter(fSigma, KSize);
            rawImage = Computer.HC_FILTER_ConvolutionWindow(fKernel, rawImage, imageW, imageH, rc);
            return rawImage;
        }
        public override bool VeryfyMeasurementMatching()
        {
            // 이새끼도 Position 별로 세팅 값이 다를 수 있으므로 멀티 로 해야되겠네 . 젠장. 

            // rape techniques have to be same 
            //string measL = IFX_ALGORITHM.ToStringType(measure_LFT).Split('_')[0];
            //string measR = IFX_ALGORITHM.ToStringType(measure_LFT).Split('_')[0];
            //
            //if (measL != measR) return false;

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
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            throw new NotImplementedException();
        }
        public override float rape_Bitch_Direction(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            throw new NotImplementedException();
        }
        public override float rape_Pussy_Cardin(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            throw new NotImplementedException();
        }
        public override float MeasureData(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
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

            rawImage = DoPreProcess(rawImage, imageW, imageH, SIGMA, KERNEL, Rectangle.Round(rcMergedA));

            //*************************************************************************************
            // 01 HORIZONTAL - EXTERNAL TOP 
            //*************************************************************************************

            if (this.algorithm_VER_EX_T == IFX_ALGORITHM.DIR_IN || this.algorithm_VER_EX_T == IFX_ALGORITHM.DIR_EX)
            {
                listEdges_VER_EX_TOP = Computer.GetPointList_Derivative_HOR(rawImage, imageW, imageH, rcVER_EX_TOP, true, 0);
            }
            else if (this.algorithm_VER_EX_T == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_VER_EX_TOP = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rcVER_EX_TOP, false, 0);
            }
            else if (this.algorithm_VER_EX_T == IFX_ALGORITHM.CARDIN)
            {
                listEdges_VER_EX_TOP = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rcVER_EX_TOP, true, 0);
            }
            
            //*************************************************************************************
            // 02 HORIZONTAL - INTERNAL TOP
            //*************************************************************************************

            if (this.algorithm_VER_IN_T == IFX_ALGORITHM.DIR_IN || this.algorithm_VER_IN_T == IFX_ALGORITHM.DIR_EX)
            {
                listEdges_VER_IN_TOP = Computer.GetPointList_Derivative_HOR(rawImage, imageW, imageH, rcVER_IN_TOP, true, 0);
            }
            else if (this.algorithm_VER_IN_T == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_VER_IN_TOP = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rcVER_IN_TOP, false, 0);
            }
            else if (this.algorithm_VER_IN_T == IFX_ALGORITHM.CARDIN)
            {
                listEdges_VER_IN_TOP = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rcVER_IN_TOP, true, 0);
            }

            //*************************************************************************************
            // 03 HORIZONTAL - INTERNAL BTM
            //*************************************************************************************

            if (this.algorithm_VER_IN_B == IFX_ALGORITHM.DIR_IN || this.algorithm_VER_IN_B == IFX_ALGORITHM.DIR_EX)
            {
                listEdges_VER_IN_BTM = Computer.GetPointList_Derivative_HOR(rawImage, imageW, imageH, rcVER_IN_BTM, false, 0);
            }
            else if (this.algorithm_VER_IN_B == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_VER_IN_BTM = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rcVER_IN_BTM, true, 0);
            }
            else if (this.algorithm_VER_IN_B == IFX_ALGORITHM.CARDIN)
            {
                listEdges_VER_IN_BTM = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rcVER_IN_BTM, false, 0);
            }
            //*************************************************************************************
            // 04 HORIZONTAL - EXTERNAL BTM
            //*************************************************************************************

            if (this.algorithm_VER_EX_B == IFX_ALGORITHM.DIR_IN || this.algorithm_VER_EX_B == IFX_ALGORITHM.DIR_EX  )
            {
                listEdges_VER_EX_BTM = Computer.GetPointList_Derivative_HOR(rawImage, imageW, imageH, rcVER_EX_BTM, true, 0);
            }
            else if (this.algorithm_VER_EX_B == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_VER_EX_BTM = Computer.HC_EDGE_GetRawPoints_HOR_LOG_Sign(rawImage, imageW, imageH, rcVER_EX_BTM, true, 0);
            }
            else if (this.algorithm_VER_EX_B == IFX_ALGORITHM.CARDIN)
            {
                listEdges_VER_EX_BTM = Computer.HC_EDGE_GetRawPoints_CARDIN_HOR(rawImage, imageW, imageH, rcVER_EX_BTM, false,0);
            }
            //*************************************************************************************
            // 05 VERTICAL - EXTERNAL LFT
            //*************************************************************************************

            if (this.algorithm_HOR_EX_L == IFX_ALGORITHM.DIR_IN || this.algorithm_HOR_EX_L == IFX_ALGORITHM.DIR_EX )
            {
                listEdges_HOR_EX_LFT = Computer.GetPointList_Derivative_VER(rawImage, imageW, imageH, rcHOR_EX_LFT, true,0);
            }
            else if (this.algorithm_HOR_EX_L == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_HOR_EX_LFT = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rcHOR_EX_LFT, false,0);
            }
            else if (this.algorithm_HOR_EX_L == IFX_ALGORITHM.CARDIN)
            {
                listEdges_HOR_EX_LFT = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rcHOR_EX_LFT, true, 0);
            }

            //*************************************************************************************
            // 06 VERTICAL - INTERNAL LFT
            //*************************************************************************************

            if (this.algorithm_HOR_IN_L == IFX_ALGORITHM.DIR_IN|| this.algorithm_HOR_IN_L == IFX_ALGORITHM.DIR_EX )
            {
                listEdges_HOR_IN_LFT = Computer.GetPointList_Derivative_VER(rawImage, imageW, imageH, rcHOR_IN_LFT, true, 0);
            }
            else if (this.algorithm_HOR_IN_L == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_HOR_IN_LFT = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rcHOR_IN_LFT, false, 0);
            }
            else if (this.algorithm_HOR_IN_L == IFX_ALGORITHM.CARDIN)
            {
                listEdges_HOR_IN_LFT = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rcHOR_IN_LFT, true, 0);
            }
            
            //*************************************************************************************
            // 07 VERTICAL - INTERNAL RHT
            //*************************************************************************************

            if (this.algorithm_HOR_IN_R == IFX_ALGORITHM.DIR_IN ||this.algorithm_HOR_IN_R == IFX_ALGORITHM.DIR_EX )
            {
                listEdges_HOR_IN_RHT = Computer.GetPointList_Derivative_VER(rawImage, imageW, imageH, rcHOR_IN_RHT, false, 0);
            }
            else if (this.algorithm_HOR_IN_R == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_HOR_IN_RHT = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rcHOR_IN_RHT, true, 0);
            }
            else if (this.algorithm_HOR_IN_R == IFX_ALGORITHM.CARDIN)
            {
                listEdges_HOR_IN_RHT = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rcHOR_IN_RHT, false,0);
            }
            //*************************************************************************************
            // 08 VERTICAL - EXTERNAL RHT
            //*************************************************************************************
            if (this.algorithm_HOR_EX_R == IFX_ALGORITHM.DIR_IN|| this.algorithm_HOR_EX_R == IFX_ALGORITHM.DIR_EX )
            {
                listEdges_HOR_EX_RHT = Computer.GetPointList_Derivative_VER(rawImage, imageW, imageH, rcHOR_EX_RHT, false,0);
            }
            else if (this.algorithm_HOR_EX_R == IFX_ALGORITHM.MEXHAT)
            {
                listEdges_HOR_EX_RHT = Computer.HC_EDGE_GetRawPoints_VER_LOG_Sign(rawImage, imageW, imageH, rcHOR_EX_RHT,true, 0);
            }
            else if (this.algorithm_HOR_EX_R == IFX_ALGORITHM.CARDIN)
            {
                listEdges_HOR_EX_RHT = Computer.HC_EDGE_GetRawPoints_CARDIN_VER(rawImage, imageW, imageH, rcHOR_EX_RHT, false, 0);
            }

            listEdges_HOR_EX_LFT = Computer.GetList_FilterBy_MajorDistance(listEdges_HOR_EX_LFT, true, 5);
            listEdges_HOR_IN_LFT = Computer.GetList_FilterBy_MajorDistance(listEdges_HOR_IN_LFT, true, 5);
            listEdges_HOR_IN_RHT = Computer.GetList_FilterBy_MajorDistance(listEdges_HOR_IN_RHT, true, 5);
            listEdges_HOR_EX_RHT = Computer.GetList_FilterBy_MajorDistance(listEdges_HOR_EX_RHT, true, 5);

            listEdges_VER_EX_TOP = Computer.GetList_FilterBy_MajorDistance(listEdges_VER_EX_TOP, false, 5);
            listEdges_VER_IN_TOP = Computer.GetList_FilterBy_MajorDistance(listEdges_VER_IN_TOP, false, 5);
            listEdges_VER_IN_BTM = Computer.GetList_FilterBy_MajorDistance(listEdges_VER_IN_BTM, false, 5);
            listEdges_VER_EX_BTM = Computer.GetList_FilterBy_MajorDistance(listEdges_VER_EX_BTM, false, 5);

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

            if (this.param_comm_04_BOOL_SHOW_RAW_DATA == false)
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
        private double _SIGMA = 1.0;
        private int _KERNEL = 9;
        private int _CENTERING_INFLATE = 10;

        public bool UI_SELECTED { get; set; }

        public RectangleF rc_EX = new RectangleF();
        public RectangleF rc_IN = new RectangleF();
        public RectangleF _rc_EX = new RectangleF(); // 170726 in order to remove relative croodinates
        public RectangleF _rc_IN = new RectangleF();

        public int param_00_algorithm_CIR { get; set; }
        public double param_01_DMG_Tol { get; set; }
        public bool param_02_BOOL_TREAT_AS_ELLIPSE { get; set; }
        public int param_03_CircleDetecType { get; set; }
        public double param_04_Shrinkage{ get; set; }
        public int param_05_Outlier_Filter { get; set; }
        public double param_06_EdgePos { get; set; }

        public CMeasurePairCir() 
        {
            param_00_algorithm_CIR = IFX_ALGORITHM.MEXHAT;
            param_01_DMG_Tol = 0;
            param_02_BOOL_TREAT_AS_ELLIPSE = false;
            param_03_CircleDetecType = 0;
            param_04_Shrinkage = 0;
            param_05_Outlier_Filter = 0;
            param_06_EdgePos = 0;

            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_spc_enhance = 0;
            param_comm_04_BOOL_SHOW_RAW_DATA = false; 
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

            single.param_comm_01_compen_A = this.param_comm_01_compen_A;
            single.param_comm_02_compen_B = this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance = this.param_comm_03_spc_enhance;
            single.param_comm_04_BOOL_SHOW_RAW_DATA = this.param_comm_04_BOOL_SHOW_RAW_DATA;

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
        public override byte[] DoPreProcess(byte[] rawImage, int imageW, int imageH, double fSigma, int KSize, Rectangle rc)
        {
            double[] fKernel = Computer.HC_FILTER_GenerateGaussianFilter(fSigma, KSize);
            rawImage = Computer.HC_FILTER_ConvolutionWindow(fKernel, rawImage, imageW, imageH, rc);
            return rawImage;
        }

        private int GetRadiLength() { return Convert.ToInt32(Math.Max(this.rc_EX.Width / 2.0, this.rc_EX.Height / 2.0)); }
        private int GetRadiLength_INFULL() { return Convert.ToInt32(Math.Max(this.rc_IN.Width / 2.0, this.rc_IN.Height / 2.0)); }
        private int GetRadiStart() { return Convert.ToInt32(Math.Max(this.rc_IN.Width / 2.0, this.rc_IN.Height / 2.0)); }

        //************************************************************************************************
        
        public override float rape_Pussy_Cardin(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            double fRadius = 0;

            // set default value 170720
            p1 = p2 = new PointF(0, 0);
            rcEstimated = new RectangleF();


            try
            {
                RectangleF rcCirEx = this.rc_EX;
                RectangleF rcCirIn = this.rc_IN;
                RectangleF rcInflate = CRect.InflateRect(rcCirEx, _CENTERING_INFLATE);

                //***********************************************************************************

                #region PRE-PROCESS

                if (param_03_CircleDetecType != 0)
                {
                    //rawImage = Computer.HC_TRANS_MAGMEDI(rawImage, imageW, imageH);
                    rcEstimated = Computer.HC_CIRCLE_CENTERING(rawImage, imageW, imageH, Rectangle.Round(rcInflate), param_04_Shrinkage, param_03_CircleDetecType);

                    rcInflate = CRect.SetCenter(rcInflate, rcEstimated.X, rcEstimated.Y);
                    rcCirEx = CRect.SetCenter(rcCirEx, rcEstimated.X, rcEstimated.Y);
                }

                rawImage = DoPreProcess(rawImage, imageW, imageH, _SIGMA, _KERNEL, Rectangle.Round(rcInflate));

                byte[] rawBackup = new byte[rawImage.Length];
                Array.Copy(rawImage, rawBackup, rawImage.Length);

                if (param_comm_03_spc_enhance == 1)
                {
                    double fKappa = 1;
                    int nIter = 5;
                    double fDelta = 0.5;
                    rawImage = Computer.HC_FILTER_ADF_Window(rawImage, imageW, imageH, rcInflate, fKappa, nIter, fDelta);
                }
                else if (param_comm_03_spc_enhance == 2)
                {
                    byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                    rawImage  = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
                }
                else if (param_comm_03_spc_enhance == 3)
                {
                    rawImage = Computer.HC_FILTER_STD_Window(rawImage, imageW, imageH, rcInflate, 5, 1);
                }

                PointF ptCenter = CRect.GetCenter(rcCirEx);
                #endregion 

                //********************************************************************************************

                listEdges_FEX.Clear();
                listEdges_FIN.Clear();

                int nRadiLength = this.GetRadiLength();
                int nRadiStart = this.GetRadiStart();
                int nRealRadi = nRadiLength - nRadiStart;

                //***********************************************************************************
                // Get Rough Edges

                double[] arrayCos = Computer.GetArray_COS();
                double[] arraySin = Computer.GetArray_SIN();

                int[] arrSubPosEX = new int[360];
                int[] arrSubPosIN = new int[360];

                PointF[] arrContour = new PointF[360];

              #region EDGE EXTRACTION
                //for (int nAngle = 0; nAngle < 360; nAngle++)
                Parallel.For(0, 360, nAngle =>
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

                    // get the copy
                    Array.Copy(ptTarget_IN, ptTarget_EX, ptTarget_IN.Length);

                    // set the reverse
                    Array.Reverse(ptTarget_EX);

                    double fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, ptTarget_IN, -1);
                    double fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, ptTarget_EX, +1);

                    arrSubPosEX[nAngle] = (int)Math.Floor(fSubPosEX);
                    arrSubPosIN[nAngle] = (int)Math.Floor(fSubPosIN);

                    double EX_X = 0; double EX_Y = 0;
                    double IN_X = 0; double IN_Y = 0;

                    if (fSubPosEX != 0)
                    {
                        EX_X = ptCenter.X + ((nRadiLength - fSubPosEX) * arrayCos[nAngle]);
                        EX_Y = ptCenter.Y + ((nRadiLength - fSubPosEX) * arraySin[nAngle]);

                    }
                    if (fSubPosIN != 0)
                    {
                        IN_X = ptCenter.X + ((nRadiStart + fSubPosIN) * arrayCos[nAngle]);
                        IN_Y = ptCenter.Y + ((nRadiStart + fSubPosIN) * arraySin[nAngle]);
                    }

                    PointF pt_EX = new PointF((float)EX_X, (float)EX_Y);
                    PointF pt_IN = new PointF((float)IN_X, (float)IN_Y);

                    if (this.param_06_EdgePos == 0)
                    {
                        if (fSubPosEX != 0 && fSubPosIN != 0)
                        {
                            CLine line = new CLine(pt_IN, pt_EX);
                            PointF ptEstimatedCenter = line.CENTER;
                            arrContour[nAngle] = ptEstimatedCenter;
                        }
                        else{arrContour[nAngle] = ptCenter;}
                    }

                    if (this.param_06_EdgePos == +1) { if (fSubPosEX != 0) { arrContour[nAngle] = pt_EX; } else { arrContour[nAngle] = ptCenter; } }
                    if (this.param_06_EdgePos == -1) { if (fSubPosIN != 0) { arrContour[nAngle] = pt_IN; } else { arrContour[nAngle] = ptCenter; } }
                });

                #endregion

                double fMajorEX = (double)Computer.GetMajorValue(arrSubPosEX);
                double fMajorIN = (double)Computer.GetMajorValue(arrSubPosIN);

            
                listEdges_FEX = arrContour.ToList();

                // Currently listcount = raw contour points !@!!@!@!@!@
                //********************************************************************

                if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                {
                  #region THIS IS NOT AN ELLIPSE
                    if (0 < param_01_DMG_Tol && param_01_DMG_Tol < 1)
                    {
                        listEdges_FEX = Computer.GetFilterdedCircleEdgesByDamageTolderance(rawBackup, imageW, imageH, rcCirEx, listEdges_FEX, this.param_01_DMG_Tol);
                    }
                    else if (this.param_01_DMG_Tol == 1)
                    {
                        listEdges_FEX = Computer.GetIterativeCircleDiaByDmgTolerance(rawBackup, imageW, imageH, rcCirEx, listEdges_FEX);
                    }

                    if (param_05_Outlier_Filter == 1)
                    {
                        Rectangle rcEstCompen = CRect.OffsetRect(Rectangle.Round(rcEstimated), -(rcEstimated.Width / 2.0), -(rcEstimated.Height / 2.0));
                        listEdges_FEX = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listEdges_FEX);
                    }

                    // backup rawdata 
                    listEdges_FIN = listEdges_FEX.ToList();

                    Computer.HC_FIT_Circle(listEdges_FEX, ref ptCenter, ref fRadius);

                    p1 = new PointF((float)(ptCenter.X - fRadius), ptCenter.Y);
                    p2 = new PointF((float)(ptCenter.X + fRadius), ptCenter.Y);

                    #endregion
                }
                else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                {
                    #region THIS IS AN ELLIPSE
                    if (param_05_Outlier_Filter == 1)
                    {
                        Rectangle rcEstCompen = CRect.OffsetRect(Rectangle.Round(rcEstimated), -(rcEstimated.Width / 2.0), -(rcEstimated.Height / 2.0));
                        listEdges_FEX = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listEdges_FEX);
                    }

                    double distanceThreshold = 50;
                    CModelEllipse model = new CModelEllipse();
                    CRansac.ransac_ellipse_fitting(listEdges_FEX.ToArray(), ref model, distanceThreshold);

                    listEdges_FIN = listEdges_FEX.ToList();
                    listEdges_FEX= CRansac.GetEllipseContours(model);


                    for (int i = 0; i < listEdges_FEX.Count; i++)
                    {
                        PointF pt = listEdges_FEX.ElementAt(i);
                        fRadius += CPoint.GetDistance(ptCenter, pt);
                    }
                    fRadius /= listEdges_FEX.Count;
                    #endregion
                }

                if (param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                  #region SHOW RAW DATA
                    if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                    {
                        listEdges_FEX = Computer.GenCircleContourPoints(fRadius, ptCenter);
                        listEdges_FIN.Clear();
                    }
                    else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                    {
                        listEdges_FIN = listEdges_FEX.ToList();
                    }
                    #endregion
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
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            double fRadius = 0;

            // set default value 170720
            p1 = p2 = new PointF(0, 0);
            rcEstimated = new RectangleF();

            try
            {
                RectangleF rcCirEx = this.rc_EX;
                RectangleF rcCirIn = this.rc_IN;
                RectangleF rcInflate = CRect.InflateRect(rcCirEx, _CENTERING_INFLATE);

                //********************************************************************************************

                #region PRE-PREOCESS
                if (param_03_CircleDetecType != 0)
                {
                    //rawImage = Computer.HC_TRANS_MAGMEDI(rawImage, imageW, imageH);

                    rcEstimated = Computer.HC_CIRCLE_CENTERING(rawImage, imageW, imageH, Rectangle.Round(rcInflate), param_04_Shrinkage, param_03_CircleDetecType);
                    rcInflate = CRect.SetCenter(rcInflate, rcEstimated.X, rcEstimated.Y);
                    rcCirEx = CRect.SetCenter(rcCirEx, rcEstimated.X, rcEstimated.Y);
                }
                
                rawImage = DoPreProcess(rawImage, imageW, imageH, _SIGMA, _KERNEL, Rectangle.Round(rcInflate));

                byte[] rawBackup = new byte[rawImage.Length];
                Array.Copy(rawImage, rawBackup, rawImage.Length);

                if (param_comm_03_spc_enhance == 1)
                {
                    double fKappa = 1;
                    int nIter = 5;
                    double fDelta = 0.5;
                    rawImage = Computer.HC_FILTER_ADF_Window(rawImage, imageW, imageH, rcInflate, fKappa, nIter, fDelta);
                 }
                else if (param_comm_03_spc_enhance == 2)
                {
                    byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                    rawImage = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
                }
                else if (param_comm_03_spc_enhance == 3)
                {
                    rawImage = Computer.HC_FILTER_STD_Window(rawImage, imageW, imageH, rcInflate, 5, 0.5);
                }
                //Computer.SaveImage(rawImage, imageW, imageH, "c:\\wackfucker.bmp");

                PointF ptCenter = CRect.GetCenter(rcCirEx);

                #endregion

                //********************************************************************************************

                listEdges_FEX.Clear();
                listEdges_FIN.Clear();
 
                int nRadiLength = this.GetRadiLength();
                int nRadiStart = this.GetRadiStart();
                int nRealRadi = nRadiLength - nRadiStart;

                double[] arrayCos = Computer.GetArray_COS();
                double[] arraySin = Computer.GetArray_SIN();

                PointF[] arrContour = new PointF[360];

                if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_EX)
                {
                    #region DIRECTION_EXTERNAL
                    double [] rawAnglurarSlice = Computer.GetAnglurarSliceArray(rawImage, imageW, imageH, nRadiLength, nRadiStart, ptCenter, false);
                    byte[] rawAS = Computer.HC_CONV_Double2Byte(rawAnglurarSlice);
                    Computer.SaveImage(rawAS, nRadiLength-nRadiStart, 360, "c:\\licker.bmp");
                    //for (int nAngle = 0; nAngle < 360; nAngle++)
                    Parallel.For(0, 360, nAngle =>
                    {
                        double fSubPos = 0;
                        double ex = 0;
                        double ey = 0;

                        double[] buffLine = new double[nRealRadi];
                        Array.Copy(rawAnglurarSlice, (nAngle * nRealRadi), buffLine, 0, nRealRadi);

                        if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_EX && this.param_06_EdgePos == +1)
                        {
                            fSubPos = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                            ex = ptCenter.X + ((nRadiStart + fSubPos) * arrayCos[nAngle]);
                            ey = ptCenter.Y + ((nRadiStart + fSubPos) * arraySin[nAngle]);
                        }
                        else if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_EX && this.param_06_EdgePos == -1)
                        {
                            fSubPos = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                            ex = ptCenter.X + ((nRadiStart + fSubPos) * arrayCos[nAngle]);
                            ey = ptCenter.Y + ((nRadiStart + fSubPos) * arraySin[nAngle]);
                        }
                        else if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_EX && this.param_06_EdgePos == 0)
                        {
                            double fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                            double EX_X = ptCenter.X + ((nRadiStart + fSubPosEX) * arrayCos[nAngle]);
                            double EX_Y = ptCenter.Y + ((nRadiStart + fSubPosEX) * arraySin[nAngle]);

                            double fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                            double IN_X = ptCenter.X + ((nRadiStart + fSubPosIN) * arrayCos[nAngle]);
                            double IN_Y = ptCenter.Y + ((nRadiStart + fSubPosIN) * arraySin[nAngle]);

                            ex = Math.Min(EX_X, IN_X) + ((Math.Max(EX_X, IN_X) - Math.Min(EX_X, IN_X)) / 2.0);
                            ey = Math.Min(EX_Y, IN_Y) + ((Math.Max(EX_Y, IN_Y) - Math.Min(EX_Y, IN_Y)) / 2.0);
                        }

                        arrContour[nAngle] = new PointF((float)ex, (float)ey); ;
                    });
                    #endregion
                }
                else if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_IN)
                {
                    #region DIRECTION_INTERANAL
                    double [] rawAnglurarSlice = Computer.GetAnglurarSliceArray(rawImage, imageW, imageH, nRadiLength, nRadiStart, ptCenter, true);

                    //for (int nAngle = 0; nAngle < 360; nAngle++)
                    Parallel.For(0, 360, nAngle =>
                    {
                        double fSubPos = 0;
                        double ex = 0;
                        double ey = 0;

                        double[] buffLine = new double[nRealRadi];
                        Array.Copy(rawAnglurarSlice, (nAngle * nRealRadi), buffLine, 0, nRealRadi);

                        if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_IN && this.param_06_EdgePos == +1)
                        {
                            fSubPos = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                            ex = ptCenter.X + ((nRadiLength - 1 - fSubPos) * arrayCos[nAngle]);
                            ey = ptCenter.Y + ((nRadiLength - 1 - fSubPos) * arraySin[nAngle]);
                        }
                        else if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_IN && this.param_06_EdgePos == -1)
                        {
                            fSubPos = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                            ex = ptCenter.X + ((nRadiLength - 1 - fSubPos) * arrayCos[nAngle]);
                            ey = ptCenter.Y + ((nRadiLength - 1 - fSubPos) * arraySin[nAngle]);
                        }
                        else if (this.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_IN && this.param_06_EdgePos == 0)
                        {
                            double fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                            double EX_X = ptCenter.X + ((nRadiLength - 1 - fSubPosEX) * arrayCos[nAngle]);
                            double EX_Y = ptCenter.Y + ((nRadiLength - 1 - fSubPosEX) * arraySin[nAngle]);

                            double fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                            double IN_X = ptCenter.X + ((nRadiLength - 1 - fSubPosIN) * arrayCos[nAngle]);
                            double IN_Y = ptCenter.Y + ((nRadiLength - 1 - fSubPosIN) * arraySin[nAngle]);
                            ex = Math.Min(EX_X, IN_X) + ((Math.Max(EX_X, IN_X) - Math.Min(EX_X, IN_X)) / 2.0);
                            ey = Math.Min(EX_Y, IN_Y) + ((Math.Max(EX_Y, IN_Y) - Math.Min(EX_Y, IN_Y)) / 2.0);
                        }
                        arrContour[nAngle] = new PointF((float)ex, (float)ey); ;
                    });
                    #endregion
                }

                listEdges_FEX = arrContour.ToList();

                if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                {
                  #region THIS IS NOT AN ELLIPSE
                    if (0 < param_01_DMG_Tol && param_01_DMG_Tol < 1)
                    {
                        listEdges_FEX = Computer.GetFilterdedCircleEdgesByDamageTolderance(rawBackup, imageW, imageH, rcCirEx, listEdges_FEX, this.param_01_DMG_Tol);
                    }
                    else if (this.param_01_DMG_Tol == 1)
                    {
                        listEdges_FEX = Computer.GetIterativeCircleDiaByDmgTolerance(rawBackup, imageW, imageH, rcCirEx, listEdges_FEX);
                    }

                    // Additive Work 1 : Get the tempolary Radius 
                    //CModelCircle model = new CModelCircle();
                    //CRansac.ransac_Circle_fitting(listContour.ToArray(), ref model, 1.0, tempFiltered.Count / 3, tempFiltered.Count * 2);
                    //double fTempRadi = model.r;

                    if (param_05_Outlier_Filter == 1)
                    {
                        Rectangle rcEstCompen = CRect.OffsetRect(Rectangle.Round(rcEstimated), -(rcEstimated.Width / 2.0), -(rcEstimated.Height / 2.0));
                        listEdges_FEX = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listEdges_FEX);
                    }

                    listEdges_FIN = listEdges_FEX.ToList();

                    Computer.HC_FIT_Circle(listEdges_FEX, ref ptCenter, ref fRadius);

                    p1 = new PointF((float)(ptCenter.X - fRadius), ptCenter.Y);
                    p2 = new PointF((float)(ptCenter.X + fRadius), ptCenter.Y);
                    #endregion
                }
                else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                {
                    #region ELLIPSE
                    if (param_05_Outlier_Filter == 1)
                    {
                        Rectangle rcEstCompen = CRect.OffsetRect(Rectangle.Round(rcEstimated), -(rcEstimated.Width / 2.0), -(rcEstimated.Height / 2.0));
                        listEdges_FEX = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listEdges_FEX);
                    }

                    double distanceThreshold = 50;
                    CModelEllipse model = new CModelEllipse();
                    CRansac.ransac_ellipse_fitting(listEdges_FEX.ToArray(), ref model, distanceThreshold);

                    listEdges_FIN = listEdges_FEX.ToList();
                    listEdges_FEX = CRansac.GetEllipseContours(model);

                    for (int i = 0; i < listEdges_FIN.Count; i++)
                    {
                        PointF pt = listEdges_FIN.ElementAt(i);
                        fRadius += CPoint.GetDistance(ptCenter, pt);
                    }
                    fRadius /= listEdges_FIN.Count;
#endregion
                }

                if (param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    #region Show data
                    if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                        {
                            listEdges_FEX = Computer.GenCircleContourPoints(fRadius, ptCenter);
                            listEdges_FIN.Clear();
                        }
                        else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                        {
                            listEdges_FEX = listEdges_FIN.ToList();
                        }
                    #endregion
                } // bShowData
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Convert.ToSingle(fRadius * 2.0);
                    
        }
        public override float rape_asshole_Log(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,
            ref List<PointF> listEdges_FMD, 
            ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,
            ref List<PointF> listEdges_SMD,
            ref List<PointF> listEdges_SIN,
            out PointF p1, out PointF p2, out RectangleF rcEstimated)
        {
            double fRadius = 0;

            // set default value 170720
            p1 = p2 = new PointF(0, 0);
            rcEstimated = new RectangleF();
            
            try
            {
                RectangleF rcCirEx = this.rc_EX;
                RectangleF rcCirIn = this.rc_IN;

                RectangleF rcInflate = CRect.InflateRect(rcCirEx, _CENTERING_INFLATE);

                //***********************************************************************************

                #region PRE-PROCESS
                if (param_03_CircleDetecType != 0)
                {
                    //rawImage = Computer.HC_TRANS_MAGMEDI(rawImage, imageW, imageH);

                    rcEstimated = Computer.HC_CIRCLE_CENTERING(rawImage, imageW, imageH, Rectangle.Round(rcInflate), param_04_Shrinkage, param_03_CircleDetecType);

                    rcInflate = CRect.SetCenter(rcInflate, rcEstimated.X, rcEstimated.Y);
                    rcCirEx = CRect.SetCenter( rcCirEx, rcEstimated.X, rcEstimated.Y);
                }

                rawImage = DoPreProcess(rawImage, imageW, imageH, _SIGMA, _KERNEL, Rectangle.Round(rcInflate));

                byte[] rawBackup = new byte[rawImage.Length];
                Array.Copy(rawImage, rawBackup, rawImage.Length);

                if (param_comm_03_spc_enhance == 1)
                {
                    double fKappa = 1;
                    int nIter = 5;
                    double fDelta = 0.5;
                    rawImage = Computer.HC_FILTER_ADF_Window(rawImage, imageW, imageH, rcInflate, fKappa, nIter, fDelta);
                }
                else if (param_comm_03_spc_enhance == 2)
                {
                    byte[] reverse = Computer.HC_TRANS_Reverse(rawImage, imageW, imageH);
                    rawImage = Computer.HC_ARITH_SUB(reverse, rawImage, imageW, imageH);
                 }
                else if (param_comm_03_spc_enhance == 3)
                {
                    rawImage = Computer.HC_FILTER_STD_Window(rawImage, imageW, imageH, rcInflate, 5, 0.5);
                }
                PointF ptCenter = CRect.GetCenter(rcCirEx);

                #endregion

                //***********************************************************************************

                listEdges_FEX.Clear();
                listEdges_FIN.Clear();

                int nRadiLength = this.GetRadiLength();
                int nRadiStart = this.GetRadiStart();

                //***********************************************************************************
                // Get Rough Edges

                double[] arrayCos = Computer.GetArray_COS();
                double[] arraySin = Computer.GetArray_SIN();

                int[] arrSubPosEX = new int[360];
                int[] arrSubPosIN = new int[360];

                PointF[] arrContour = new PointF[360];

                #region EDGE-EXTRACTION
                for (int nAngle = 0; nAngle < 360; nAngle++)
                //Parallel.For(0, 360, nAngle =>
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

                    // get the copy
                    Array.Copy(ptTarget_IN, ptTarget_EX, ptTarget_IN.Length);

                    // set the reverse
                    Array.Reverse(ptTarget_EX);

                    // 170511 Fix direction for circle only 1
                    //fSubPos = Computer.GetLogPos(rawImage, imageW, imageH, ptTarget, (int)this.DIR);

                    double fSubPos_EX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, ptTarget_EX, 1);
                    double fSubPos_IN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, ptTarget_IN, 1);

                    arrSubPosEX[nAngle] = (int)Math.Floor(fSubPos_EX);
                    arrSubPosIN[nAngle] = (int)Math.Floor(fSubPos_IN);

                    double EX_X = 0; double EX_Y = 0;
                    double IN_X = 0; double IN_Y = 0;

                    if (fSubPos_EX != 0)
                    {
                        EX_X = ptCenter.X + ((nRadiLength - fSubPos_EX) * arrayCos[nAngle]);
                        EX_Y = ptCenter.Y + ((nRadiLength - fSubPos_EX) * arraySin[nAngle]);

                    }
                    if (fSubPos_IN != 0)
                    {
                        IN_X = ptCenter.X + ((nRadiStart + fSubPos_IN) * arrayCos[nAngle]);
                        IN_Y = ptCenter.Y + ((nRadiStart + fSubPos_IN) * arraySin[nAngle]);
                    }

                    PointF pt_EX = new PointF((float)EX_X, (float)EX_Y);
                    PointF pt_IN = new PointF((float)IN_X, (float)IN_Y);

                    if (this.param_06_EdgePos == 0)
                    {
                        if (fSubPos_EX != 0 && fSubPos_IN != 0)
                        {
                            CLine line = new CLine(pt_IN, pt_EX);
                            PointF ptEstimatedCenter = line.CENTER;
                            arrContour[nAngle] = ptEstimatedCenter;
                        }
                        else
                        {
                            arrContour[nAngle]  = ptCenter;
                        }
                    }

                    if (this.param_06_EdgePos == +1) { if (fSubPos_EX != 0) { arrContour[nAngle] = pt_EX; } else { arrContour[nAngle] = ptCenter; } }
                    if (this.param_06_EdgePos == -1) { if (fSubPos_IN != 0) { arrContour[nAngle] = pt_IN; } else { arrContour[nAngle] = ptCenter; } }
                }//);

                #endregion

                listEdges_FEX = arrContour.ToList();

                // Currently listcount = raw contour points !@!!@!@!@!@
                //********************************************************************

                if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                {
                  #region THIS IS NOT AN ELLIPSE
                    if (0 < param_01_DMG_Tol && param_01_DMG_Tol < 1)
                    {
                        listEdges_FEX = Computer.GetFilterdedCircleEdgesByDamageTolderance(rawBackup, imageW, imageH, rcCirEx, listEdges_FEX, this.param_01_DMG_Tol);
                    }
                    else if (this.param_01_DMG_Tol == 1)
                    {
                        listEdges_FEX = Computer.GetIterativeCircleDiaByDmgTolerance(rawBackup, imageW, imageH, rcCirEx, listEdges_FEX);
                    }

                    if (param_05_Outlier_Filter == 1)
                    {
                        Rectangle rcEstCompen = CRect.OffsetRect(Rectangle.Round(rcEstimated), -(rcEstimated.Width/2.0), -(rcEstimated.Height/2.0));
                        listEdges_FEX = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listEdges_FEX);
                    }

                    Computer.HC_FIT_Circle(listEdges_FEX, ref ptCenter, ref fRadius);

                    p1 = new PointF((float)(ptCenter.X - fRadius), (float)ptCenter.Y);
                    p2 = new PointF((float)(ptCenter.X + fRadius), (float)ptCenter.Y);
                  #endregion
                }
                else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                {
                   #region THIS IS AN ELLIPSE
                    if (param_05_Outlier_Filter == 1)
                    {
                        Rectangle rcEstCompen = CRect.OffsetRect(Rectangle.Round(rcEstimated), -rcEstimated.Width, -rcEstimated.Height);
                        listEdges_FEX = CLine.GetFilteredEllipsePoints_OUTSIDE(rcEstCompen, listEdges_FEX);
                    }
 
                    double distanceThreshold = 50;
                    CModelEllipse model = new CModelEllipse();
                    CRansac.ransac_ellipse_fitting(listEdges_FEX.ToArray(), ref model, distanceThreshold);

                    listEdges_FIN = listEdges_FEX.ToList();
                    listEdges_FEX = CRansac.GetEllipseContours(model);

                    for (int i = 0; i < listEdges_FIN.Count; i++)
                    {
                        PointF pt = listEdges_FIN.ElementAt(i);
                        fRadius += CPoint.GetDistance(ptCenter, pt);
                    }
                    fRadius /= listEdges_FIN.Count;
                    #endregion
                }

                if (param_comm_04_BOOL_SHOW_RAW_DATA == false)
                {
                    #region Show raw data
                    if (param_02_BOOL_TREAT_AS_ELLIPSE == false)
                    {
                        listEdges_FEX = Computer.GenCircleContourPoints(fRadius, ptCenter);
                        listEdges_FEX.Add(ptCenter);
                    }
                    else if (param_02_BOOL_TREAT_AS_ELLIPSE == true)
                    {
                        listEdges_FEX = listEdges_FIN.ToList();
                    }
                    #endregion
                } // bShowData
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Convert.ToSingle(fRadius * 2.0);
        }

        public override float MeasureData(byte[] rawImage, int imageW, int imageH, 
            ref List<PointF> listEdges_FEX,ref List<PointF> listEdges_FMD, ref List<PointF> listEdges_FIN,
            ref List<PointF> listEdges_SEX,ref List<PointF> listEdges_SMD,ref List<PointF> listEdges_SIN,
            out PointF P1, out PointF P2, out RectangleF rcEstimated)
        {
            #region NOT IMPLEMENTED
            double fDistance = 0;
            // set default value 170720
            P1 = P2 = new PointF(0, 0);
            rcEstimated = new RectangleF();

            /***/if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN)) 
            {
                fDistance = this.rape_Pussy_Cardin(rawImage, imageW, imageH,
                    ref listEdges_FEX,
                    ref listEdges_FMD,
                    ref listEdges_FIN,
                    ref listEdges_SEX,
                    ref listEdges_SMD,
                    ref listEdges_SIN,
                    out P1, out P2, out rcEstimated); 
            }
            else if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_IN) ||
                     this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_EX)) 
            {
                fDistance = this.rape_Bitch_Direction(rawImage, imageW, imageH,
                    ref listEdges_FEX,
                    ref listEdges_FMD,
                    ref listEdges_FIN,
                    ref listEdges_SEX,
                    ref listEdges_SMD,
                    ref listEdges_SIN,
                    out P1, out P2, out rcEstimated); 
            }
            else if (this.GetMeasurementCategory() == IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.MEXHAT)) 
            {
                fDistance = this.rape_asshole_Log(rawImage, imageW, imageH,
                    ref listEdges_FEX,
                    ref listEdges_FMD,
                    ref listEdges_FIN,
                    ref listEdges_SEX,
                    ref listEdges_SMD,
                    ref listEdges_SIN,
                    out P1, out P2, out rcEstimated); 
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

    public abstract class CMeasureFather
    {
        public CMeasureFather(){}

        public string INSP_FIGURE_TYPE { get; set; }
        public string INSP_TARGET_NAME { get; set; }

        public string INSP_TIME { get; set; }
        public string INSP_FILE { get; set; }
        
        public List<double> listRaw_Figure = new List<double>();
        public List<COverlay> listRaw_Overlay = new List<COverlay>();

        public int DataCount_Figure { get { return listRaw_Figure.Count; } }
        public int DataCount_Overlay { get { return listRaw_Overlay.Count; } }

        public abstract void CalcAccResult();

    }

    public class CMeasureReport
    {
        private List<DataFigure> m_listFigure = new List<DataFigure>();
        private List<DataOverlay> m_listOverlay = new List<DataOverlay>();

        public string INFO_RECIPE { get; set; }
        public string INFO_PTRN { get; set; }
        public double INFO_PIXEL_X { get; set; }
        public double INFO_PIXEL_Y { get; set; }
        public string INFO_TIME { get; set; }

        private int/**/COUNT_FIGURE { get { return m_listFigure.Count; } }
        private int/**/COUNT_OVERLAY { get { return m_listOverlay.Count; } }

        public class DataOverlay : CMeasureFather
        {
            public string INSP_METHOD_HOR_EX_LFT { get; set; }
            public string INSP_METHOD_HOR_IN_LFT { get; set; }
            public string INSP_METHOD_HOR_IN_RHT { get; set; }
            public string INSP_METHOD_HOR_EX_RHT { get; set; }

            public string INSP_METHOD_VER_EX_TOP { get; set; }
            public string INSP_METHOD_VER_IN_TOP { get; set; }
            public string INSP_METHOD_VER_IN_BTM { get; set; }
            public string INSP_METHOD_VER_EX_BTM { get; set; }

            public double INSP_DX { get; set; }
            public double INSP_DY { get; set; }

            public int TOTAL_COUNT { get { return listRaw_Overlay.Count; } }

            public DataOverlay CopyTo()
            {
                DataOverlay single = new DataOverlay();

                single.INSP_FILE = this.INSP_FILE;
                single.INSP_FIGURE_TYPE = this.INSP_FIGURE_TYPE;
                single.INSP_TARGET_NAME = this.INSP_TARGET_NAME;
                single.INSP_TIME = this.INSP_TIME;

                single.INSP_METHOD_HOR_EX_LFT = this.INSP_METHOD_HOR_EX_LFT;
                single.INSP_METHOD_HOR_IN_LFT = this.INSP_METHOD_HOR_IN_LFT;
                single.INSP_METHOD_HOR_IN_RHT = this.INSP_METHOD_HOR_IN_RHT;
                single.INSP_METHOD_HOR_EX_RHT = this.INSP_METHOD_HOR_EX_RHT;

                single.INSP_METHOD_VER_EX_TOP = this.INSP_METHOD_VER_EX_TOP;
                single.INSP_METHOD_VER_IN_TOP = this.INSP_METHOD_VER_IN_TOP;
                single.INSP_METHOD_VER_IN_BTM = this.INSP_METHOD_VER_IN_BTM;
                single.INSP_METHOD_VER_EX_BTM = this.INSP_METHOD_VER_EX_BTM;

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
            public string INSP_METHOD1 { get; set; }
            public string INSP_METHOD2 { get; set; }
            public double INSP_RES { get; set; }
            public double DMG_IGNORANCE { get; set; }
            public int TOTAL_COUNT { get { return listRaw_Figure.Count; } }

            // to make data frame for accumulation
            public DataFigure CopyTo()
            {
                DataFigure single = new DataFigure();
                single.INSP_FILE = this.INSP_FILE;
                single.INSP_FIGURE_TYPE = this.INSP_FIGURE_TYPE;
                single.INSP_TARGET_NAME = this.INSP_TARGET_NAME;
                single.INSP_METHOD1 = this.INSP_METHOD1;
                single.INSP_METHOD2 = this.INSP_METHOD2;
                single.DMG_IGNORANCE = this.DMG_IGNORANCE;
                single.INSP_TIME = this.INSP_TIME;
                single.INSP_RES = this.INSP_RES;
                

                return single;
            }

             // data accumulation interface 
            public void Insert_Data(double fValue)
            {
                listRaw_Figure.Add(fValue);
                INSP_RES += fValue;
             }

            // sumarry function
            public override void CalcAccResult() { INSP_RES /= listRaw_Figure.Count; }
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
                    temp.INSP_RES = 0;

                    listUnique.Add(temp);
                }
                else
                {
                    bool bExistance = Check_UniqueExistance_Figure(listUnique, single);

                    // if unique
                    if (bExistance == false)
                    {
                        DataFigure temp = single.CopyTo();
                        temp.INSP_RES = 0;

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

                arrUnitData[nIndex].Insert_Data(single.INSP_RES);
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
                double dy = single.INSP_DY * INFO_PIXEL_Y;

                arrUnitData[nIndex].Insert_Data(dx, dy);
            }

            // Summary 
            for (int i = 0; i < listUnique.Count; i++)
            {
                arrUnitData[i].CalcAccResult();
            }
            return arrUnitData.ToList();
        }

        // get index from a given array
        private int Find_UniqueIndex_Figure(DataFigure[] arrUnitData, DataFigure single)
        {
            int nIndex = -1;

            for (int i = 0; i < arrUnitData.Length; i++)
            {
                DataFigure baseSingle = arrUnitData[i];

                if (baseSingle.INSP_FIGURE_TYPE == single.INSP_FIGURE_TYPE &&
                    baseSingle.INSP_TARGET_NAME == single.INSP_TARGET_NAME)
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

                if (baseSingle.INSP_TARGET_NAME == single.INSP_TARGET_NAME)
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

                if (baseSignle.INSP_FIGURE_TYPE == single.INSP_FIGURE_TYPE &&
                    baseSignle.INSP_TARGET_NAME == single.INSP_TARGET_NAME)
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

                if (baseSignle.INSP_TARGET_NAME == single.INSP_TARGET_NAME)
                {
                    bExistance = true;
                    break;
                }
            }
            return bExistance;
        }

        public void SetInit()
        {
            m_listFigure.Clear();
            m_listOverlay.Clear();
        }
     

        public void AddResult_FIG(int nFigureType, string strFile, object Ini, double fResult)
        {
            string strTargetName = string.Empty;
            string strMethod1 = string.Empty;
            string strMethod2 = string.Empty;
            string strFigureType = string.Empty;
            double fDmgIgnorance = 0.0;

            //if (nFigureType == IFX_FIGURE.PAIR_HOR)
            //{
            //    CMeasurePairHor buff = ((CMeasurePairHor)Ini);
            //
            //    strTargetName = buff.NICKNAME;
            //    strMethod1 = IFX_ALGORITHM.ToStringType(buff.measure_TOP);
            //    strMethod2 = IFX_ALGORITHM.ToStringType(buff.measure_BTM);
            //}
            //else if( nFigureType == IFX_FIGURE.PAIR_VER)
            //{
            //    CMeasurePairVer buff = ((CMeasurePairVer)Ini);
            //
            //    strTargetName = buff.NICKNAME;
            //    strMethod1 = IFX_ALGORITHM.ToStringType(buff.measure_LFT);
            //    strMethod2 = IFX_ALGORITHM.ToStringType(buff.measure_RHT);
            //}
            if( nFigureType == IFX_FIGURE.PAIR_RCT)
            {
                CMeasurePairRct buff = ((CMeasurePairRct)Ini);

                strTargetName = buff.NICKNAME;
                strMethod1 = IFX_ALGORITHM.ToStringType(buff.param_00_algorithm);
                strMethod2 = IFX_ALGORITHM.ToStringType(buff.param_00_algorithm);

                strFigureType = IFX_RECT_TYPE.ToStringType(buff.RC_TYPE);
            }
            else if (nFigureType == IFX_FIGURE.PAIR_CIR)
            {
                CMeasurePairCir buff = ((CMeasurePairCir)Ini);

                strTargetName = buff.NICKNAME;
                strMethod1 = IFX_ALGORITHM.ToStringType(buff.param_00_algorithm_CIR);
                strMethod2 = IFX_ALGORITHM.ToStringType(buff.param_00_algorithm_CIR);
                fDmgIgnorance = buff.param_01_DMG_Tol;
                strFigureType = IFX_FIGURE.ToStringType(IFX_FIGURE.PAIR_CIR); ;
            }

            DataFigure single = new DataFigure();

            single.INSP_FIGURE_TYPE = strFigureType;
            single.INSP_FILE = strFile;
            single.INSP_TARGET_NAME = strTargetName;
            single.INSP_METHOD1 = strMethod1;
            single.INSP_METHOD2 = strMethod2;
            single.DMG_IGNORANCE = fDmgIgnorance;
            single.INSP_RES = fResult;
            single.INSP_TIME = WrapperDateTime.GetTImeCode4Save_YYYY_MM_DD_HH_MM_SS_MMM();

            m_listFigure.Add(single);
        }
        public void AddResult_OVL(int nFigureType, string strFile, double fOLX, double fOLY, CMeasurePairOvl ini)
        {
            DataOverlay single = new DataOverlay();

            single.INSP_FIGURE_TYPE = IFX_FIGURE.ToStringType(nFigureType);
            single.INSP_FILE = strFile;
            single.INSP_TARGET_NAME = ini.NICKNAME;

            single.INSP_METHOD_HOR_EX_LFT = IFX_ALGORITHM.ToStringType(ini.algorithm_HOR_EX_L);
            single.INSP_METHOD_HOR_IN_LFT = IFX_ALGORITHM.ToStringType(ini.algorithm_HOR_IN_L);
            single.INSP_METHOD_HOR_IN_RHT = IFX_ALGORITHM.ToStringType(ini.algorithm_HOR_IN_R);
            single.INSP_METHOD_HOR_EX_RHT = IFX_ALGORITHM.ToStringType(ini.algorithm_HOR_EX_R);

            single.INSP_METHOD_VER_EX_TOP = IFX_ALGORITHM.ToStringType(ini.algorithm_VER_EX_T);
            single.INSP_METHOD_VER_IN_TOP = IFX_ALGORITHM.ToStringType(ini.algorithm_VER_IN_T);
            single.INSP_METHOD_VER_IN_BTM = IFX_ALGORITHM.ToStringType(ini.algorithm_VER_IN_B);
            single.INSP_METHOD_VER_EX_BTM = IFX_ALGORITHM.ToStringType(ini.algorithm_VER_EX_B);

            single.INSP_DX = fOLX;
            single.INSP_DY = fOLY;

            m_listOverlay.Add(single);
        }

        public static void _WriteMeasurementData(CMeasureReport report, string strSavePath)
        {
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
                single[0] = "TYPE"; /************/single[1] = summary.INSP_FIGURE_TYPE; ex.data.Add(single.ToArray());
                single[0] = "NAME"; /************/single[1] = summary.INSP_TARGET_NAME; ex.data.Add(single.ToArray());
                single[0] = "MEASURE TYPE 1";/***/single[1] = summary.INSP_METHOD1; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE TYPE 2";/***/single[1] = summary.INSP_METHOD2; /**/ex.data.Add(single.ToArray());

                if (summary.INSP_FIGURE_TYPE != IFX_FIGURE.ToStringType(IFX_FIGURE.PAIR_CIR))
                {
                    single[0] = "WIDTH"; single[1] = summary.INSP_RES.ToString("F4"); ex.data.Add(single.ToArray());
                }
                else
                {
                    single[0] = "DIAMETER"; single[1] = summary.INSP_RES.ToString("F4"); ex.data.Add(single.ToArray());
                    single[0] = "DMG IGNORANCE"; single[1] = summary.DMG_IGNORANCE.ToString("F2"); ex.data.Add(single.ToArray());

                }
                single[0] = "3-SIGMA";/***/single[1] = fSigam.ToString("F4"); /**/ex.data.Add(single.ToArray());
                single[0] = single[1] = ""; ex.data.Add(single.ToArray());
            }

            header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());
            header[0] = /****************/ header[1] = ""; /*************************************/ ex.data.Add(header.ToArray());


            if (listFigure.Count != 0)
            {
                //*******************************************************************************
                // Entire Data Chart In detail : Column Generation
                //*******************************************************************************

                // column header [Empty] + [Index 0]...[Index N-1]
                int nIterationCount = listFigure.ElementAt(0).TOTAL_COUNT;  // 첫번째걸로 하는데 갯수 안맞을 수 있어 170811
                int nHeaderLength = nIterationCount + 1;
                string[] arrHeader = new string[nHeaderLength];
                arrHeader[0] = "ITEM";

                for (int nColumn = 1; nColumn < nHeaderLength; nColumn++) { arrHeader[nColumn] = "ITER_" + nColumn.ToString("N0"); }
                ex.data.Add(arrHeader.ToArray());

                //*******************************************************************************
                // Raw Data Listing
                //*******************************************************************************

                for (int nItem = 0; nItem < listFigure.Count; nItem++)
                {
                    CMeasureReport.DataFigure summary = listFigure.ElementAt(nItem);

                    string[] rawData = new string[nHeaderLength];

                    rawData[0] = summary.INSP_TARGET_NAME;
                    for (int i = 0; i < summary.TOTAL_COUNT; i++)  // 갯수가 엇박일수 있으니까 돌리는건 지 숫자대로
                    {
                        rawData[i + 1] = summary.listRaw_Figure.ElementAt(i).ToString("F4");
                    }
                    ex.data.Add(rawData.ToArray());
                }
            }

            for (int nItem = 0; nItem < listOverlay.Count; nItem++)
            {
                string[] single = new string[10];

                CMeasureReport.DataOverlay summary = listOverlay.ElementAt(nItem);
                COverlay sigmaOVL = summary.calcSigma(report.INFO_PIXEL_X);

                int nACC = summary.TOTAL_COUNT;

                single[0] = "TOTAL"; /***********/single[1] = nACC.ToString("N0");/****/ex.data.Add(single.ToArray());
                single[0] = "TYPE"; /************/single[1] = summary.INSP_FIGURE_TYPE; ex.data.Add(single.ToArray());
                single[0] = "NAME"; /************/single[1] = summary.INSP_TARGET_NAME; ex.data.Add(single.ToArray());

                single[0] = "MEASURE HOR_EX_LFT";/***/single[1] = summary.INSP_METHOD_HOR_EX_LFT; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE HOR_IN_LFT";/***/single[1] = summary.INSP_METHOD_HOR_IN_LFT; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE HOR_IN_RHT";/***/single[1] = summary.INSP_METHOD_HOR_IN_RHT; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE HOR_EX_RHT";/***/single[1] = summary.INSP_METHOD_HOR_EX_RHT; /**/ex.data.Add(single.ToArray());

                single[0] = "MEASURE VER_EX_TOP";/***/single[1] = summary.INSP_METHOD_VER_EX_TOP; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE VER_IN_TOP";/***/single[1] = summary.INSP_METHOD_VER_IN_TOP; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE VER_IN_BTM";/***/single[1] = summary.INSP_METHOD_VER_IN_BTM; /**/ex.data.Add(single.ToArray());
                single[0] = "MEASURE VER_EX_BTM";/***/single[1] = summary.INSP_METHOD_VER_EX_BTM; /**/ex.data.Add(single.ToArray());


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
                    rawData_X[0] = summary.INSP_TARGET_NAME + "OLX";
                    for (int i = 0; i < nIterationCount; i++)
                    {
                        rawData_X[i + 1] = summary.listRaw_Overlay.ElementAt(i).DX.ToString("F4");
                    }
                    ex.data.Add(rawData_X.ToArray());

                    //*******************************************************************
                    // for Overlay Y 
                    string[] rawData_Y = new string[nHeaderLength];
                    rawData_Y[0] = summary.INSP_TARGET_NAME + "OLY";
                    for (int i = 0; i < nIterationCount; i++)
                    {
                        rawData_Y[i + 1] = summary.listRaw_Overlay.ElementAt(i).DY.ToString("F4");
                    }
                    ex.data.Add(rawData_Y.ToArray());
                }
            }
            
            string strDumpFileName = WrapperDateTime.GetTImeCode4Save_YYYY_MM_DD_HH_MM_SS_MMM() + ".CSV";
            string strDumpFilePath = Path.Combine(strSavePath, strDumpFileName);

            //ex.Dump_Data(strDumpFilePath);

            WrapperCVS cvs = new WrapperCVS();
            cvs.SaveCSVFile(strDumpFilePath, ex.data);

            try
            {
                WrapperExcel.ExcuteExcel(strDumpFilePath);
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
        
    }
    
     
    
    
}



