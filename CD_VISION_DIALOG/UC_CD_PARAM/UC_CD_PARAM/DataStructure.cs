using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using CD_Figure;
using DEF_PARAMS;

namespace CD_Paramter
{
    [TypeConverter(typeof(CustomPoinDConverter))]
    public class CustomPointD
    {
        public float X { get; set; }
        public float Y { get; set; }

        public CustomPointD(float x, float y)
        {
            this.X = x; this.Y = y;
        }
        public static parseRect ToParseRect(CustomPointD lt, CustomPointD rt, CustomPointD lb, CustomPointD rb)
        {
            parseRect rc = new parseRect();

            rc.LT = new PointF(lt.X, lt.Y);
            rc.RT = new PointF(rt.X, rt.Y);
            rc.LB = new PointF(lb.X, lb.Y);
            rc.RB = new PointF(rb.X, rb.Y);

            return rc;
        }
        
        public CustomPointD()
        {
            this.X = this.Y = 0;
        }
        public override string ToString()
        {
            return X.ToString() + " , " + Y.ToString();
        }
        public static CustomPointD parsePoint(string strPoint)
        {
            string[] parse = strPoint.Split(',');

            try
            {
                float x = Convert.ToSingle(parse[0]);
                float y = Convert.ToSingle(parse[1]);
                CustomPointD point = new CustomPointD(x, y);
                return point;
            }
            catch (FormatException)
            {
                return null;
            }
        }
        public void SetValue(PointF point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }
        public PointF ToPointF()
        {
            PointF pt = new PointF(this.X, this.Y);
            return pt;
        }
        
    }

    [TypeConverter(typeof(CustomRectangleDConverter))]
    public class CustomRectangleD
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public CustomRectangleD()
        {
            this.X = this.Y = this.Width = this.Height;
        }
        public CustomRectangleD(float x, float y, float width, float height)
        {
            this.X = x; this.Y = y; this.Width = width; this.Height = height;
        }
        public CustomRectangleD(RectangleF rc)
        {
            this.X = rc.X; this.Y = rc.Y; this.Width = rc.Width; this.Height = rc.Height;
        }
        public override string ToString()
        {
            return this.X.ToString() + " , " + this.Y.ToString() + " , " + this.Width.ToString() + " , " + this.Height.ToString();
        }
        public static CustomRectangleD parseRect(string strPoint)
        {
            string[] parse = strPoint.Split(',');

            try
            {
                float x = Convert.ToSingle(parse[0]);
                float y = Convert.ToSingle(parse[1]);
                float w = Convert.ToSingle(parse[2]);
                float h = Convert.ToSingle(parse[3]);
                CustomRectangleD rect = new CustomRectangleD(x, y, w, h);
                return rect;
            }
            catch (FormatException)
            {
                return null;
            }
        }
        public void SetValue(RectangleF rc)
        {
            this.X = rc.X; this.Y = rc.Y; this.Width = rc.Width; this.Height = rc.Height;
        }
        public RectangleF ToRectangleF()
        {
            RectangleF rect = new RectangleF(this.X, this.Y, this.Width, this.Height);
            return rect;
        }
        
    }

    public class PROPERTY_PairHor
    {
        private CustomRectangleD rcTop = new CustomRectangleD();
        private CustomRectangleD rcBtm = new CustomRectangleD();

        private string measure_TOP;
        private string measure_BTM;

        [CategoryAttribute("00 Nick Name"), DescriptionAttribute("Figure Nick Name"), ReadOnly(true)]
        public string NICKNAME { get; set; }

        //[CategoryAttribute("03 TOP Region"), DescriptionAttribute("Region Croodinates"), ReadOnly(true)]
        private CustomRectangleD RC_TOP { get { return rcTop; } }
        //[CategoryAttribute("04 BTN region"), DescriptionAttribute("Region Croodinates"), ReadOnly(true)]
        private CustomRectangleD RC_BTM { get { return rcBtm; } }

        [CategoryAttribute("05 Measure Method - TOP"), DescriptionAttribute("Measure Method For TOP Region")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_TOP
        {
            get
            {
                string str = "";

                if (measure_TOP != null)
                {
                    str = measure_TOP;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measure_TOP = value; }

        }

        [CategoryAttribute("06 Measure Method - BTM"), DescriptionAttribute("Measure Method For BTM Region")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_BTM
        {
            get
            {
                string str = "";

                if (measure_BTM != null)
                {
                    str = measure_BTM;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measure_BTM = value; }

        }

        [CategoryAttribute("07 Searching Direction "), DescriptionAttribute("Only For LoG Based Approach\nSets of Parameters : -1 or 1")]
        public double DIR_TOP { get; set; }

        [CategoryAttribute("08 Searching Direction"), DescriptionAttribute("Only For LoG Based Approach\nSets of Parameters : -1 or 1")]
        public double DIR_BTM { get; set; }

        [CategoryAttribute("09 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get; set; }

        public CMeasurePairHor ToFigure()
        {
            CMeasurePairHor single = new CMeasurePairHor();

            single.NICKNAME = NICKNAME;

            single.measure_TOP = IFX_ALGORITHM.ToNumericType(measure_TOP);
            single.measure_BTM = IFX_ALGORITHM.ToNumericType(measure_BTM);

            single.rc_TOP = RC_TOP.ToRectangleF();
            single.rc_BTM = RC_BTM.ToRectangleF();

            single.DIR_TOP = Convert.ToInt32(DIR_TOP);
            single.DIR_BTM = Convert.ToInt32(DIR_BTM);
            single.param_comm_03_BOOL_SHOW_RAW_DATA = SHOW_RAW_DATA;

            return single;
        }
        public void FromFigure(CMeasurePairHor single)
        {
            this.NICKNAME = single.NICKNAME;
            this.RC_TOP.SetValue(single.rc_TOP);
            this.RC_BTM.SetValue(single.rc_BTM);
            this.measure_TOP = IFX_ALGORITHM.ToStringType(single.measure_TOP);
            this.measure_BTM = IFX_ALGORITHM.ToStringType(single.measure_BTM);
            this.DIR_TOP = single.DIR_TOP;
            this.DIR_BTM = single.DIR_BTM;
            this.SHOW_RAW_DATA = single.param_comm_03_BOOL_SHOW_RAW_DATA;
        }
    }
    public class PROPERTY_PairVer
    {
        private CustomRectangleD rc_LFT = new CustomRectangleD();
        private CustomRectangleD rc_RHT = new CustomRectangleD();

        private string measure_LFT;
        private string measure_RHT;

        [CategoryAttribute("00 Nick Name"), DescriptionAttribute("Figure Nick Name"), ReadOnly(true)]
        public string NICKNAME { get; set; }

        //[CategoryAttribute("03 LFT Region"), DescriptionAttribute("Region Croodinates"), ReadOnly(true)]
        private CustomRectangleD RC_LFT{get { return rc_LFT; }}

        //[CategoryAttribute("04 RHT Region"), DescriptionAttribute("Region Croodinates"), ReadOnly(true)]
        private CustomRectangleD RC_RHT{get { return rc_RHT; }}

        [CategoryAttribute("05 Measure Method - LFT"), DescriptionAttribute("Measure Method For LFT Region")]

        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]

        public string MEASURE_LFT
        {
            get
            {
                string str = "";

                if (measure_LFT != null)
                {
                    str = measure_LFT;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measure_LFT = value; }

        }

        [CategoryAttribute("06 Measure Method - RHT"), DescriptionAttribute("Measure Method For RHT Region")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]

        public string MEASURE_RHT
        {
            get
            {
                string str = "";

                if (measure_RHT != null)
                {
                    str = measure_RHT;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measure_RHT = value; }

        }
        [CategoryAttribute("07 Searching Direction"), DescriptionAttribute("Only For LoG Based Approach\nSets of Parameters : -1 or 1")]
        public double DIR_LFT { get; set; }

        [CategoryAttribute("08 Searching Direction"), DescriptionAttribute("Only For LoG Based Approach\nSets of Parameters : -1 or 1")]
        public double DIR_RHT { get; set; }

        [CategoryAttribute("10 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get; set; }

        public CMeasurePairVer ToFigure()
        {
            CMeasurePairVer single = new CMeasurePairVer();

            single.NICKNAME = NICKNAME;

            single.measure_LFT = IFX_ALGORITHM.ToNumericType(measure_LFT);
            single.measure_RHT = IFX_ALGORITHM.ToNumericType(measure_RHT);

            single.rc_LFT = RC_LFT.ToRectangleF();
            single.rc_RHT = RC_RHT.ToRectangleF();

            single.DIR_LFT = Convert.ToInt32(DIR_LFT);
            single.DIR_RHT = Convert.ToInt32(DIR_RHT);
            single.param_comm_03_BOOL_SHOW_RAW_DATA = this.SHOW_RAW_DATA;

            return single;
        }
        public void FromFigure(CMeasurePairVer single)
        {
            this.NICKNAME = single.NICKNAME;
            this.rc_LFT.SetValue(single.rc_LFT);
            this.rc_RHT.SetValue(single.rc_RHT);
            this.measure_LFT = IFX_ALGORITHM.ToStringType(single.measure_LFT);
            this.measure_RHT = IFX_ALGORITHM.ToStringType(single.measure_RHT);
            this.DIR_LFT = single.DIR_LFT;
            this.DIR_RHT = single.DIR_RHT;
            this.SHOW_RAW_DATA = single.param_comm_03_BOOL_SHOW_RAW_DATA;
        }
    }

    public class PROPERTY_PairDia
    {
        public PROPERTY_PairDia()
        {
            param_00_algorithm = string.Empty;
            param_01_bool_use_auto_peak_detection = false;
            param_02_peak_target_index_fst = 0;
            param_03_peak_target_index_scd = 1;
            param_04_peak_candidate = 2;
            param_05_window_size = 2;
            param_06_edge_pos_fst = 0;
            param_07_edge_pos_scd = 0;

            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_BOOL_SHOW_RAW_DATA = false;

        }
        private double param_comm_01_compen_A;
        private double param_comm_02_compen_B;
        private bool param_comm_03_BOOL_SHOW_RAW_DATA;

        private string param_00_algorithm;
        private bool   param_01_bool_use_auto_peak_detection;
        private double param_02_peak_target_index_fst;
        private double param_03_peak_target_index_scd;
        private double param_04_peak_candidate;
        private double param_05_window_size;
        private double param_06_edge_pos_fst;
        private double param_07_edge_pos_scd;

        private string rect_type;

        // surface position croodinate
        private CustomPointD pt_FST_LT = new CustomPointD();
        private CustomPointD pt_FST_RT = new CustomPointD();
        private CustomPointD pt_FST_LB = new CustomPointD();
        private CustomPointD pt_FST_RB = new CustomPointD();

        // hidden position croodinate
        private CustomPointD _pt_fst_lt = new CustomPointD();
        private CustomPointD _pt_fst_rt = new CustomPointD();
        private CustomPointD _pt_fst_lb = new CustomPointD();
        private CustomPointD _pt_fst_rb = new CustomPointD();

        // surface position croodinate
        private CustomPointD pt_SCD_LT = new CustomPointD();
        private CustomPointD pt_SCD_RT = new CustomPointD();
        private CustomPointD pt_SCD_LB = new CustomPointD();
        private CustomPointD pt_SCD_RB = new CustomPointD();

        // hidden position croodinate
        private CustomPointD _pt_scd_lt = new CustomPointD();
        private CustomPointD _pt_scd_rt = new CustomPointD();
        private CustomPointD _pt_scd_lb = new CustomPointD();
        private CustomPointD _pt_scd_rb = new CustomPointD();

        [CategoryAttribute("00 Nick Name"), DescriptionAttribute("Figure Nick Name"), ReadOnly(true)]
        public string NICKNAME { get; set; }

        [CategoryAttribute("01 Rectangle Type"), DescriptionAttribute("Rectangle Type : HOR | VER | DIA")]
        public string RECT_TYPE { get { return rect_type; } set { rect_type = value; } }


        [CategoryAttribute("02 Angle"), DescriptionAttribute("Angle of figures"), ReadOnly(true)]
        public float ANGLE { get; set; }

        //[CategoryAttribute("04 First Region - Point LT"), DescriptionAttribute("Region Croodinates For Each Rectagle Points"), ReadOnly(true)]
        private CustomPointD PT_FST_LT { get { return pt_FST_LT; } set { pt_FST_LT = value; } }
        //[CategoryAttribute("04 First Region - point RT"), DescriptionAttribute("Region Croodinates For Each Rectangle Points"), ReadOnly(true)]
        private CustomPointD PT_FST_RT { get { return pt_FST_RT; } set { pt_FST_RT = value; } }
        //[CategoryAttribute("04 First Region - Point LB"), DescriptionAttribute("Region Croodinates For Each Rectangle Points"), ReadOnly(true)]
        private CustomPointD PT_FST_LB { get { return pt_FST_LB; } set { pt_FST_LB = value; } }
        //[CategoryAttribute("04 First Region - Point RB"), DescriptionAttribute("Region Croodinates For Each Rectangle Points"), ReadOnly(true)]
        private CustomPointD PT_FST_RB { get { return pt_FST_RB; } set { pt_FST_RB = value; } }

        //[CategoryAttribute("05 Second Region - Point LT"), DescriptionAttribute("Region Croodinates For Each Rectangle Points"), ReadOnly(true)]
        private CustomPointD PT_SCD_LT { get { return pt_SCD_LT; } set { pt_SCD_LT = value; } }
        //[CategoryAttribute("05 Second Region - Point RT"), DescriptionAttribute("Region Croodinates For Each Rectangle Points"), ReadOnly(true)]
        private CustomPointD PT_SCD_RT { get { return pt_SCD_RT; } set { pt_SCD_RT = value; } }
        //[CategoryAttribute("05 Second Region - Point LB"), DescriptionAttribute("Region Croodinates For Each Rectangle Points"), ReadOnly(true)]
        private CustomPointD PT_SCD_LB { get { return pt_SCD_LB; } set { pt_SCD_LB = value; } }
        //[CategoryAttribute("05 Second Region - Point RB"), DescriptionAttribute("Region Croodinates For Each Rectangle Points"), ReadOnly(true)]
        private CustomPointD PT_SCD_RB { get { return pt_SCD_RB; } set { pt_SCD_RB = value; } }

        [CategoryAttribute("03 Measure Algorithm"), DescriptionAttribute("Measure Algorithm { MEXHAT | DIR_IN | DIR_EX | CARDIN")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string ALGORITHM
        {
            get
            {
                string str = "";

                if (param_00_algorithm != null)
                {
                    str = param_00_algorithm;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_00_algorithm = value; }

        }

        [CategoryAttribute("04 Edge Detection Type"), DescriptionAttribute("true | false")]
        public bool USE_AUTO_PEAK_DETECTION{ get{return param_01_bool_use_auto_peak_detection;} set{param_01_bool_use_auto_peak_detection = value;} }

        [CategoryAttribute("05 Peak Target Index For FST "), DescriptionAttribute("In case of EDGE_DETECTION_TYPE > 0 & has to be  <= Peak Candidate ")]
        public double PEAK_TARGET_IDX_FST{ get { return param_02_peak_target_index_fst; } set { param_02_peak_target_index_fst= value; } }

        [CategoryAttribute("06 Peak Target Index For SCD"), DescriptionAttribute("In case of EDGE_DETECTION_TYPE > 0 & has to be  <= Peak Candidate ")]
        public double PEAK_TARGET_IDX_SCD { get { return param_03_peak_target_index_scd; } set { param_03_peak_target_index_scd = value; } }

        [CategoryAttribute("07 Peak Candidate Count"), DescriptionAttribute("In case of EDGE_DETECTION_TYPE > 0 & 2 ~ ∞")]
        public double PEAK_CANDIDATE { get { return param_04_peak_candidate; } set { param_04_peak_candidate = value; } }

        [CategoryAttribute("08 Window Size"), DescriptionAttribute("In case of EDGE_DETECTION_TYPE > 0 & 2 ~ ∞")]
        public double WINDOW_SIZE { get { return param_05_window_size; } set { param_05_window_size= value; } }


        [CategoryAttribute("09 Edge Position for FST"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS_FST { get{return param_06_edge_pos_fst;} set{param_06_edge_pos_fst=value;} }
        [CategoryAttribute("10 Edge Position SCD"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS_SCD { get { return param_07_edge_pos_scd; } set { param_07_edge_pos_scd = value; } }

        [CategoryAttribute("11 Compensation A"), DescriptionAttribute("A of Ax + B")]
        public double COMPEN_A { get { return param_comm_01_compen_A; } set { param_comm_01_compen_A = value; } }
        [CategoryAttribute("12 Compensation B"), DescriptionAttribute("B of Ax + B")]
        public double COMPEN_B { get{ return param_comm_02_compen_B;} set{param_comm_02_compen_B=value;} }

        [CategoryAttribute("13 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get { return param_comm_03_BOOL_SHOW_RAW_DATA; } set { param_comm_03_BOOL_SHOW_RAW_DATA = value; } }

        public CMeasurePairDia ToFigure()
        {
            CMeasurePairDia single = new CMeasurePairDia();

            single.NICKNAME = NICKNAME;

            single.RC_TYPE = IFX_FIGURE.ToNumericType(RECT_TYPE);

            single.rc_FST = CustomPointD.ToParseRect(pt_FST_LT, pt_FST_RT, pt_FST_LB, pt_FST_RB);
            single.rc_SCD = CustomPointD.ToParseRect(pt_SCD_LT, pt_SCD_RT, PT_SCD_LB, pt_SCD_RB);

            single._rc_FST = CustomPointD.ToParseRect(_pt_fst_lt, _pt_fst_rt, _pt_fst_lb, _pt_fst_rb);
            single._rc_SCD = CustomPointD.ToParseRect(_pt_scd_lt, _pt_scd_rt, _pt_scd_lb, _pt_scd_rb);

            single.param_00_algorithm/****************/= IFX_ALGORITHM.ToNumericType(this.param_00_algorithm);
            single.param_01_bool_Use_AutoDetection/***/= this.param_01_bool_use_auto_peak_detection;
            single.param_02_peakTargetIndex_fst /*****/= Convert.ToInt32(this.param_02_peak_target_index_fst);
            single.param_03_peakTargetIndex_scd/******/= Convert.ToInt32(this.param_03_peak_target_index_scd);
            single.param_04_peakCandidate/************/= Convert.ToInt32(this.param_04_peak_candidate);
            single.param_05_windowSize/***************/= Convert.ToInt32(this.param_05_window_size);
            single.param_06_edge_position_fst/********/= this.param_06_edge_pos_fst;
            single.param_07_edge_position_scd/********/= this.param_07_edge_pos_scd;

            single.param_comm_01_compen_A/************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B/************/= this.param_comm_02_compen_B;
            single.param_comm_03_BOOL_SHOW_RAW_DATA/**/= this.SHOW_RAW_DATA;
            return single;
        }
        public void FromFigure(CMeasurePairDia single)
        {
            this.NICKNAME = single.NICKNAME;
            
            this.ANGLE = single.ANGLE;
            
            this.pt_FST_LT.SetValue(single.rc_FST.LT);
            this.pt_FST_RT.SetValue(single.rc_FST.RT);
            this.pt_FST_LB.SetValue(single.rc_FST.LB);
            this.pt_FST_RB.SetValue(single.rc_FST.RB);

            this.pt_SCD_LT.SetValue(single.rc_SCD.LT);
            this.pt_SCD_RT.SetValue(single.rc_SCD.RT);
            this.pt_SCD_LB.SetValue(single.rc_SCD.LB);
            this.pt_SCD_RB.SetValue(single.rc_SCD.RB);

            this._pt_fst_lt.SetValue(single._rc_FST.LT);
            this._pt_fst_rt.SetValue(single._rc_FST.RT);
            this._pt_fst_lb.SetValue(single._rc_FST.LB);
            this._pt_fst_rb.SetValue(single._rc_FST.RB);

            this._pt_scd_lt.SetValue(single._rc_SCD.LT);
            this._pt_scd_rt.SetValue(single._rc_SCD.RT);
            this._pt_scd_lb.SetValue(single._rc_SCD.LB);
            this._pt_scd_rb.SetValue(single._rc_SCD.RB);

            this.param_00_algorithm/**********************/= IFX_ALGORITHM.ToStringType(single.param_00_algorithm);
            this.param_01_bool_use_auto_peak_detection/***/= single.param_01_bool_Use_AutoDetection;
            this.param_02_peak_target_index_fst/**********/= single.param_02_peakTargetIndex_fst;
            this.param_03_peak_target_index_scd/**********/= single.param_03_peakTargetIndex_scd;
            this.param_04_peak_candidate/*****************/= single.param_04_peakCandidate;
            this.param_05_window_size/********************/= single.param_05_windowSize;
            this.param_06_edge_pos_fst/*******************/= single.param_06_edge_position_fst;
            this.param_07_edge_pos_scd/*******************/= single.param_07_edge_position_scd;

            this.RECT_TYPE = IFX_FIGURE.ToStringType(single.RC_TYPE);

            this.param_comm_01_compen_A/*************/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B/*************/= single.param_comm_02_compen_B;
            this.param_comm_03_BOOL_SHOW_RAW_DATA/***/= single.param_comm_03_BOOL_SHOW_RAW_DATA;
        }
    }

    public class PROPERTY_PairOvl
    {
        [CategoryAttribute("00 Nick Name"), DescriptionAttribute("Figure Nick Name"), ReadOnly(true)]
        public string NICKNAME { get; set; }

        private CustomRectangleD rect_HOR_in_lft = new CustomRectangleD();
        private CustomRectangleD rect_HOR_in_rht = new CustomRectangleD();
        private CustomRectangleD rect_HOR_ex_lft = new CustomRectangleD();
        private CustomRectangleD rect_HOR_ex_rht = new CustomRectangleD();

        private CustomRectangleD rect_VER_in_top = new CustomRectangleD();
        private CustomRectangleD rect_VER_in_btm = new CustomRectangleD();
        private CustomRectangleD rect_VER_ex_top = new CustomRectangleD();
        private CustomRectangleD rect_VER_ex_btm = new CustomRectangleD();

        //*****************************************************************************************

        //[CategoryAttribute("03 HOR_IN_LFT"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD RC_HOR_IN_LFT { get { return rect_HOR_in_lft; } set { rect_HOR_in_lft = value; } }

        //[CategoryAttribute("04 HOR_IN_RHT"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD RC_HOR_IN_RHT { get { return rect_HOR_in_rht; } set { rect_HOR_in_rht = value; } }

        //[CategoryAttribute("07 VER_IN_TOP"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD RC_VER_IN_TOP { get { return rect_VER_in_top; } set { rect_VER_in_top = value; } }

        //[CategoryAttribute("08 VER_IN_BTM"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD RC_VER_IN_BTM { get { return rect_VER_in_btm; } set { rect_VER_in_btm = value; } }

        //*****************************************************************************************

        //[CategoryAttribute("05 HOR_EX_LFT"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD RC_HOR_EX_LFT { get { return rect_HOR_ex_lft; } set { rect_HOR_ex_lft = value; } }

        //[CategoryAttribute("06 HOR_EX_RHT"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD RC_HOR_EX_RHT { get { return rect_HOR_ex_rht; } set { rect_HOR_ex_rht = value; } }


        //[CategoryAttribute("09 VER_EX_TOP"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD RC_VER_EX_TOP { get { return rect_VER_ex_top; } set { rect_VER_ex_top = value; } }

        //[CategoryAttribute("10 VER_EX_BTM"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD RC_VER_EX_BTM { get { return rect_VER_ex_btm; } set { rect_VER_ex_btm = value; } }

        //*****************************************************************************************

        public string measureHOR_IN_L = string.Empty;
        public string measureHOR_IN_R = string.Empty;
        public string measureHOR_EX_L = string.Empty;
        public string measureHOR_EX_R = string.Empty;

        public string measureVER_IN_T = string.Empty;
        public string measureVER_IN_B = string.Empty;
        public string measureVER_EX_T = string.Empty;
        public string measureVER_EX_B = string.Empty;

        //*****************************************************************************************
        [CategoryAttribute("11 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_HOR_IN_L
        {
            get
            {
                string str = "";

                if (measureHOR_IN_L != null)
                {
                    str = measureHOR_IN_L;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measureHOR_IN_L = value; }
        }

        [CategoryAttribute("12 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_HOR_IN_R
        {
            get
            {
                string str = "";

                if (measureHOR_IN_R != null)
                {
                    str = measureHOR_IN_R;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measureHOR_IN_R = value; }
        }

        [CategoryAttribute("13 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_VER_IN_T
        {
            get
            {
                string str = "";

                if (measureVER_IN_T != null)
                {
                    str = measureVER_IN_T;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measureVER_IN_T = value; }
        }

        [CategoryAttribute("14 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_VER_IN_B
        {
            get
            {
                string str = "";

                if (measureVER_IN_B != null)
                {
                    str = measureVER_IN_B;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measureVER_IN_B = value; }
        }

        //*****************************************************************************************

        [CategoryAttribute("11 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_HOR_EX_L
        {
            get
            {
                string str = "";

                if (measureHOR_EX_L != null)
                {
                    str = measureHOR_EX_L;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measureHOR_EX_L = value; }
        }

        [CategoryAttribute("12 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_HOR_EX_R
        {
            get
            {
                string str = "";

                if (measureHOR_EX_R != null)
                {
                    str = measureHOR_EX_R;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measureHOR_EX_R = value; }
        }

        [CategoryAttribute("13 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_VER_EX_T
        {
            get
            {
                string str = "";

                if (measureVER_EX_T != null)
                {
                    str = measureVER_EX_T;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measureVER_EX_T = value; }
        }

        [CategoryAttribute("14 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_VER_EX_B
        {
            get
            {
                string str = "";

                if (measureVER_EX_B != null)
                {
                    str = measureVER_EX_B;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { measureVER_EX_B = value; }
        }

        //*****************************************************************************************

        [CategoryAttribute("15 Searching Direction"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double DIR_HOR_IN_L { get; set; }
        [CategoryAttribute("16 Searching Direction"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double DIR_HOR_IN_R { get; set; }
        [CategoryAttribute("17 Searching Direction"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double DIR_VER_IN_T { get; set; }
        [CategoryAttribute("18 Searching Direction"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double DIR_VER_IN_B { get; set; }

        //*****************************************************************************************

        [CategoryAttribute("19 Searching Direction"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double DIR_HOR_EX_L { get; set; }
        [CategoryAttribute("20 Searching Direction"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double DIR_HOR_EX_R { get; set; }
        [CategoryAttribute("21 Searching Direction"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double DIR_VER_EX_T { get; set; }
        [CategoryAttribute("22 Searching Direction"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double DIR_VER_EX_B { get; set; }


        //*****************************************************************************************

        [CategoryAttribute("31 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get; set; }

        public CMeasurePairOvl ToFigure()
        {
            CMeasurePairOvl single = new CMeasurePairOvl();

            single.NICKNAME = NICKNAME;

            single.measureHOR_IN_L = IFX_ALGORITHM.ToNumericType(measureHOR_IN_L);
            single.measureHOR_IN_R = IFX_ALGORITHM.ToNumericType(measureHOR_IN_R);
            single.measureHOR_EX_L = IFX_ALGORITHM.ToNumericType(measureHOR_EX_L);
            single.measureHOR_EX_R = IFX_ALGORITHM.ToNumericType(measureHOR_EX_R);

            single.measureVER_IN_T = IFX_ALGORITHM.ToNumericType(measureVER_IN_T);
            single.measureVER_IN_B = IFX_ALGORITHM.ToNumericType(measureVER_IN_B);
            single.measureVER_EX_T = IFX_ALGORITHM.ToNumericType(measureVER_EX_T);
            single.measureVER_EX_B = IFX_ALGORITHM.ToNumericType(measureVER_EX_B);

            single.RC_HOR_IN.rc_LFT = RC_HOR_IN_LFT.ToRectangleF();
            single.RC_HOR_IN.rc_RHT = RC_HOR_IN_RHT.ToRectangleF();
            single.RC_HOR_EX.rc_LFT = RC_HOR_EX_LFT.ToRectangleF();
            single.RC_HOR_EX.rc_RHT = RC_HOR_EX_RHT.ToRectangleF();

            single.RC_VER_IN.rc_TOP = RC_VER_IN_TOP.ToRectangleF();
            single.RC_VER_IN.rc_BTM = RC_VER_IN_BTM.ToRectangleF();
            single.RC_VER_EX.rc_TOP = RC_VER_EX_TOP.ToRectangleF();
            single.RC_VER_EX.rc_BTM = RC_VER_EX_BTM.ToRectangleF();

            
            single.DIR_HOR_IN_L = Convert.ToInt32(DIR_HOR_IN_L);
            single.DIR_HOR_IN_R = Convert.ToInt32(DIR_HOR_IN_R);
            single.DIR_HOR_EX_L = Convert.ToInt32(DIR_HOR_EX_L);
            single.DIR_HOR_EX_R = Convert.ToInt32(DIR_HOR_EX_R);

            single.DIR_VER_IN_T = Convert.ToInt32(DIR_VER_IN_T);
            single.DIR_VER_IN_B = Convert.ToInt32(DIR_VER_IN_B);
            single.DIR_VER_EX_T = Convert.ToInt32(DIR_VER_EX_T);
            single.DIR_VER_EX_B = Convert.ToInt32(DIR_VER_EX_B);

            single.param_comm_03_BOOL_SHOW_RAW_DATA = this.SHOW_RAW_DATA;
            return single;
        }
        public void FromFigure(CMeasurePairOvl single)
        {
            this.NICKNAME = single.NICKNAME;

            this.MEASURE_HOR_IN_L = IFX_ALGORITHM.ToStringType(single.measureHOR_IN_L);
            this.MEASURE_HOR_IN_R = IFX_ALGORITHM.ToStringType(single.measureHOR_IN_R);
            this.MEASURE_HOR_EX_L = IFX_ALGORITHM.ToStringType(single.measureHOR_EX_L);
            this.MEASURE_HOR_EX_R = IFX_ALGORITHM.ToStringType(single.measureHOR_EX_R);

            this.MEASURE_VER_IN_T = IFX_ALGORITHM.ToStringType(single.measureVER_IN_T);
            this.MEASURE_VER_IN_B = IFX_ALGORITHM.ToStringType(single.measureVER_IN_B);
            this.MEASURE_VER_EX_T = IFX_ALGORITHM.ToStringType(single.measureVER_EX_T);
            this.MEASURE_VER_EX_B = IFX_ALGORITHM.ToStringType(single.measureVER_EX_B);

            this.RC_HOR_IN_LFT = new CustomRectangleD(single.RC_HOR_IN.rc_LFT);
            this.RC_HOR_IN_RHT = new CustomRectangleD(single.RC_HOR_IN.rc_RHT);
            this.RC_HOR_EX_LFT = new CustomRectangleD(single.RC_HOR_EX.rc_LFT);
            this.RC_HOR_EX_RHT = new CustomRectangleD(single.RC_HOR_EX.rc_RHT);

            this.RC_VER_IN_TOP = new CustomRectangleD(single.RC_VER_IN.rc_TOP);
            this.RC_VER_IN_BTM = new CustomRectangleD(single.RC_VER_IN.rc_BTM);
            this.RC_VER_EX_TOP = new CustomRectangleD(single.RC_VER_EX.rc_TOP);
            this.RC_VER_EX_BTM = new CustomRectangleD(single.RC_VER_EX.rc_BTM);

            this.DIR_HOR_IN_L = single.DIR_HOR_IN_L;
            this.DIR_HOR_IN_R = single.DIR_HOR_IN_R;
            this.DIR_HOR_EX_L = single.DIR_HOR_EX_L;
            this.DIR_HOR_EX_R = single.DIR_HOR_EX_R;

            this.DIR_VER_IN_T = single.DIR_VER_IN_T;
            this.DIR_VER_IN_B = single.DIR_VER_IN_B;
            this.DIR_VER_EX_T = single.DIR_VER_EX_T;
            this.DIR_VER_EX_B = single.DIR_VER_EX_B;

            this.SHOW_RAW_DATA = single.param_comm_03_BOOL_SHOW_RAW_DATA;
        }

    }
    public class PROPERTY_PairCir
    {

        public PROPERTY_PairCir()
        {
            param_00_algorithm = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.MEXHAT);
            param_01_damage_tolerance = 0;
            param_02_bool_treat_as_ellipse = false;
            param_03_circle_detect_type = 0;
            param_04_Shrinkage = 0.1;
            param_05_Outlier_Filter = 1;
            param_06_edge_pos = 0;

            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_bool_show_raw_data = false;
        }

        public string param_00_algorithm;
        public double param_01_damage_tolerance;
        public bool param_02_bool_treat_as_ellipse;
        public int param_03_circle_detect_type;
        public double param_04_Shrinkage;
        public int param_05_Outlier_Filter;
        public double param_06_edge_pos;

        public double param_comm_01_compen_A;
        public double param_comm_02_compen_B;
        public bool param_comm_03_bool_show_raw_data;

        private CustomRectangleD rcEX = new CustomRectangleD();
        private CustomRectangleD rcIN = new CustomRectangleD();
        private CustomRectangleD _rcEX = new CustomRectangleD();
        private CustomRectangleD _rcIN = new CustomRectangleD();

        [CategoryAttribute("00 Nick Name"), DescriptionAttribute("Figure Nick Name"), ReadOnly(true)]
        public string NICKNAME { get; set; }

        //[CategoryAttribute("03 Outter Circle"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD CIRCLE_EX { get { return rcEX; } set { rcEX = value; } }

        //[CategoryAttribute("04 Inner Circle"), DescriptionAttribute("Region Information"), ReadOnly(true)]
        private CustomRectangleD CIRCLE_IN { get { return rcIN; } set { rcIN = value; } }

        [CategoryAttribute("01 Measure Method"), DescriptionAttribute("Measure Method")]

        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]

        public string ALGORITHM
        {
            get
            {
                string str = "";

                if (param_00_algorithm != null)
                {
                    str = param_00_algorithm;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_00_algorithm = value; }

        }

        [CategoryAttribute("02 Searching Direction"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS { get{return param_06_edge_pos;} set{param_06_edge_pos = value;} }

        [CategoryAttribute("03 Compensation A"), DescriptionAttribute("A of Ax + B")]
        public double COMPEN_A { get { return param_comm_01_compen_A; } set { param_comm_01_compen_A = value; } }
        [CategoryAttribute("04 Compensation B"), DescriptionAttribute("B of Ax + B")]
        public double COMPEN_B { get { return param_comm_02_compen_B; } set { param_comm_02_compen_B = value; } }

        [CategoryAttribute("05 Pre Circle Detection"), DescriptionAttribute("Available [ 0 = DEFAULT, 1 = POSSITIVE_CON 2 = NEGATIVE_DIFF]")]
        public int CIRCLE_DETECT_TYPE { get { return param_03_circle_detect_type; } set { param_03_circle_detect_type = value; } }

        [CategoryAttribute("06 Estimated Circle Shrinkage"), DescriptionAttribute(" ±F(%)")]
        public double CIRCLE_SHRINKAGE { get { return param_04_Shrinkage; } set { param_04_Shrinkage = value; } }

        [CategoryAttribute("07 Data Filtering"), DescriptionAttribute("Available [ 0 = none, 1 = inside 2 = outside, 3 = dual ]")]
        public int OUTLIER_FILTER { get { return param_05_Outlier_Filter; } set { param_05_Outlier_Filter = value; } }

        [CategoryAttribute("08 Circle DAM Mag"), DescriptionAttribute("Available [ 0 ~ 0.99 (%)]")]
        public double DMG_IGNORANCE { get{return param_01_damage_tolerance;} set{param_01_damage_tolerance = value;} }


        [CategoryAttribute("09 Ellipse Process"), DescriptionAttribute("Treat As Ellipse\nTrue or False")]
        public bool TREAT_AS_ELLIPSE { get{return param_02_bool_treat_as_ellipse;} set{param_02_bool_treat_as_ellipse = value;} }

        [CategoryAttribute("10 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get{return param_comm_03_bool_show_raw_data;} set{param_comm_03_bool_show_raw_data = value;} }

        public CMeasurePairCir ToFigure()
        {
            CMeasurePairCir single = new CMeasurePairCir();

            single.NICKNAME = NICKNAME;

            single.rc_EX = rcEX.ToRectangleF();
            single.rc_IN = rcIN.ToRectangleF();

            single._rc_EX = _rcEX.ToRectangleF();
            single._rc_IN = _rcIN.ToRectangleF();

            single.param_00_algorithm_CIR /***********/= IFX_ALGORITHM.ToNumericType(this.param_00_algorithm);
            single.param_01_DMG_Tol /*****************/= this.param_01_damage_tolerance;
            single.param_02_BOOL_TREAT_AS_ELLIPSE /***/= this.param_02_bool_treat_as_ellipse;
            single.param_03_CircleDetecType /*********/= this.param_03_circle_detect_type;
            single.param_04_Shrinkage /***************/= this.param_04_Shrinkage;
            single.param_05_Outlier_Filter/***********/= this.param_05_Outlier_Filter;
            single.param_06_EdgePos/******************/= this.param_06_edge_pos;

            single.param_comm_01_compen_A /*************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B /*************/= this.param_comm_02_compen_B;
            single.param_comm_03_BOOL_SHOW_RAW_DATA /***/= this.param_comm_03_bool_show_raw_data;


            return single;
        }

        public void FromFigure(CMeasurePairCir single)
        {
            this.NICKNAME = single.NICKNAME;

            this.rcEX.SetValue(single.rc_EX);
            this.rcIN.SetValue(single.rc_IN);
            this._rcEX.SetValue(single._rc_EX);
            this._rcIN.SetValue(single._rc_IN);

            this.param_00_algorithm /***************/= IFX_ALGORITHM.ToStringType(single.param_00_algorithm_CIR);
            this.param_01_damage_tolerance /********/= single.param_01_DMG_Tol;
            this.param_02_bool_treat_as_ellipse /***/= single.param_02_BOOL_TREAT_AS_ELLIPSE;
            this.param_03_circle_detect_type /******/= single.param_03_CircleDetecType;
            this.param_04_Shrinkage /***************/= single.param_04_Shrinkage;
            this.param_05_Outlier_Filter /**********/= single.param_05_Outlier_Filter;
            this.param_06_edge_pos /****************/= single.param_06_EdgePos;

            this.param_comm_01_compen_A /*************/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B /*************/= single.param_comm_02_compen_B;
            this.param_comm_03_bool_show_raw_data /***/= single.param_comm_03_BOOL_SHOW_RAW_DATA;
        }

    }

}
