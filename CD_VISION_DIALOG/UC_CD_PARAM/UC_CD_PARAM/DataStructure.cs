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
        public parseRect ToParseRect()
        {
            RectangleF rc = ToRectangleF();
            parseRect prc = new parseRect(rc.X, rc.Y, rc.Width, rc.Height);
            return prc;
        }
        
    }

 
    public class PROPERTY_PairRct
    {
        private string/***/param_00_algorithm = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        private string/***/param_01_rect_type = IFX_RECT_TYPE.ToStringType(IFX_RECT_TYPE.DIR_HOR);
        private int/******/param_02_rect_angle = 0;
        private bool/*/***/param_03_bool_use_auto_peak_detection= false;
        private double/***/param_04_peak_target_index_fst= 0;
        private double/***/param_05_peak_target_index_scd= 1;
        private double/***/param_06_peak_candidate= 2;
        private double/***/param_07_window_size = 3;
        private double/***/param_08_edge_pos_fst = 0;
        private double/***/param_09_edge_pos_scd = 0;
        private int/******/param_10_refinement= 3;

        private double/***/param_comm_01_compen_A = 1;
        private double/***/param_comm_02_compen_B = 0;
        private int/******/param_comm_03_spc_enhance = 0;
        private bool/*****/param_comm_04_show_raw_data = false;

        public PROPERTY_PairRct()
        {
        }
       

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
        public string RECT_TYPE { get { return param_01_rect_type; } set { param_01_rect_type = value; } }

        [CategoryAttribute("02 Angle"), DescriptionAttribute("Angle of figures"), ReadOnly(true)]
        public int ANGLE { get { return param_02_rect_angle; } set { param_02_rect_angle = value; } }

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

        [CategoryAttribute("04 Edge Position for FST"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ], In case of DIGONAL = [0.0, 0.5, 1.0]")]
        public double EDGE_POS_FST { get{return param_08_edge_pos_fst;} set{param_08_edge_pos_fst=value;} }
        [CategoryAttribute("05 Edge Position SCD"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ], In case of DIGONAL = [0.0, 0.5, 1.0]")]
        public double EDGE_POS_SCD { get { return param_09_edge_pos_scd; } set { param_09_edge_pos_scd = value; } }

        [CategoryAttribute("06 Compensation A"), DescriptionAttribute("A of Ax + B")]
        public double COMPEN_A { get { return param_comm_01_compen_A; } set { param_comm_01_compen_A = value; } }
        [CategoryAttribute("07 Compensation B"), DescriptionAttribute("B of Ax + B")]
        public double COMPEN_B { get{ return param_comm_02_compen_B;} set{param_comm_02_compen_B=value;} }

        [CategoryAttribute("08 SPC-EF"), DescriptionAttribute("DEFAULT(0) : ADF(1), RVS(2), STD(3), MN(4), PWRVSUB(5), GRD(6), GRDRVSUB(7), POWRVSUBAD(8)")]
        public int SPC_ENHANCE { get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }

        [CategoryAttribute("14 Refinement"), DescriptionAttribute("DEFAULT(3) : Not Use 0, Refinement Distance(N), Only For V/H")]
        public int REFINEMENT { get { return param_10_refinement; } set { param_10_refinement= value; } }

        [CategoryAttribute("15 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get { return param_comm_04_show_raw_data; } set { param_comm_04_show_raw_data = value; } }

        public CMeasurePairRct ToFigure()
        {
            CMeasurePairRct single = new CMeasurePairRct();

            single.NICKNAME = NICKNAME;

            single.rc_FST = CustomPointD.ToParseRect(pt_FST_LT, pt_FST_RT, pt_FST_LB, pt_FST_RB);
            single.rc_SCD = CustomPointD.ToParseRect(pt_SCD_LT, pt_SCD_RT, PT_SCD_LB, pt_SCD_RB);

            single._rc_FST = CustomPointD.ToParseRect(_pt_fst_lt, _pt_fst_rt, _pt_fst_lb, _pt_fst_rb);
            single._rc_SCD = CustomPointD.ToParseRect(_pt_scd_lt, _pt_scd_rt, _pt_scd_lb, _pt_scd_rb);

            single.param_00_algorithm/****************/= IFX_ALGORITHM.ToNumericType(this.param_00_algorithm);
            single.param_01_rc_type /*****************/= IFX_RECT_TYPE.ToNumericType(this.param_01_rect_type);
            single.param_02_rect_angle /**************/= this.param_02_rect_angle;
            single.param_03_bool_Use_AutoDetection/***/= this.param_03_bool_use_auto_peak_detection;
            single.param_04_peakTargetIndex_fst /*****/= Convert.ToInt32(this.param_04_peak_target_index_fst);
            single.param_05_peakTargetIndex_scd/******/= Convert.ToInt32(this.param_05_peak_target_index_scd);
            single.param_06_peakCandidate/************/= Convert.ToInt32(this.param_06_peak_candidate);
            
            if (param_07_window_size <= 10) param_07_window_size = 10;
            single.param_07_windowSize/***************/= Convert.ToInt32(this.param_07_window_size);
            single.param_08_edge_position_fst/********/= this.param_08_edge_pos_fst;
            single.param_09_edge_position_scd/********/= this.param_09_edge_pos_scd;
            single.param_10_refinement /**************/= this.param_10_refinement;

            single.param_comm_01_compen_A/************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B/************/= this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance /********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_show_raw_data/**/= this.param_comm_04_show_raw_data;

            return single;
        }
        public void FromFigure(CMeasurePairRct single)
        {
            this.NICKNAME = single.NICKNAME;
  
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
            this.param_01_rect_type /*********************/= IFX_RECT_TYPE.ToStringType(single.param_01_rc_type);
            this.param_02_rect_angle /********************/= single.param_02_rect_angle;
            this.param_03_bool_use_auto_peak_detection/***/= single.param_03_bool_Use_AutoDetection;
            this.param_04_peak_target_index_fst/**********/= single.param_04_peakTargetIndex_fst;
            this.param_05_peak_target_index_scd/**********/= single.param_05_peakTargetIndex_scd;
            this.param_06_peak_candidate/*****************/= single.param_06_peakCandidate;
            this.param_07_window_size/********************/= single.param_07_windowSize;

            if (this.param_07_window_size <= 10) this.param_07_window_size = 10;
            this.param_08_edge_pos_fst/*******************/= single.param_08_edge_position_fst;
            this.param_09_edge_pos_scd/*******************/= single.param_09_edge_position_scd;
            this.param_10_refinement/*********************/= single.param_10_refinement;

            this.param_comm_01_compen_A/*************/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B/*************/= single.param_comm_02_compen_B;
            this.param_comm_03_spc_enhance/**********/= single.param_comm_03_spc_enhance;
            this.param_comm_04_show_raw_data /*******/= single.param_comm_04_show_raw_data;
        }
    }
    public class PROPERTY_PairOvl
    {
        public string/****/param_01_algorithm_hor_in = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        public string/****/param_03_algorithm_ver_in = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        public string/****/param_02_algorithm_hor_ex = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        public string/****/param_04_algorithm_ver_ex = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        private double/***/param_05_edge_pos_hor_in = 0;
        private double/***/param_06_edge_pos_hor_ex = 0;
        private double/***/param_07_edge_pos_ver_in = 0;
        private double/***/param_08_edge_pos_ver_ex = 0;
        private int/******/param_09_refinement = 3;
        private int/******/param_10_shape_of_measure = 0;
        private double/***/param_comm_01_compen_A = 1;
        private double/***/param_comm_02_compen_B = 0;
        private int/******/param_comm_03_spc_enhance = 0;
        private bool/*****/param_comm_04_show_raw_data = false;

        public PROPERTY_PairOvl()
        {
        }

        [CategoryAttribute("00 Nick Name"), DescriptionAttribute("Figure Nick Name"), ReadOnly(true)]
        public string NICKNAME { get; set; }

        // for absolution coordinate for display 
        private CustomRectangleD rect_HOR_in_lft = new CustomRectangleD();
        private CustomRectangleD rect_HOR_in_rht = new CustomRectangleD();
        private CustomRectangleD rect_HOR_ex_lft = new CustomRectangleD();
        private CustomRectangleD rect_HOR_ex_rht = new CustomRectangleD();

        private CustomRectangleD rect_VER_in_top = new CustomRectangleD();
        private CustomRectangleD rect_VER_in_btm = new CustomRectangleD();
        private CustomRectangleD rect_VER_ex_top = new CustomRectangleD();
        private CustomRectangleD rect_VER_ex_btm = new CustomRectangleD();


        // for relative coordinate backup 
        private RectangleF rc_HOR_IN_FST = new RectangleF();
        private RectangleF rc_HOR_IN_SCD = new RectangleF();
        private RectangleF rc_HOR_EX_FST = new RectangleF();
        private RectangleF rc_HOR_EX_SCD = new RectangleF();
        private RectangleF rc_VER_IN_FST = new RectangleF();
        private RectangleF rc_VER_IN_SCD = new RectangleF();
        private RectangleF rc_VER_EX_FST = new RectangleF();
        private RectangleF rc_VER_EX_SCD = new RectangleF();

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

        //*****************************************************************************************
        [CategoryAttribute("11 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_HOR_IN
        {
            get
            {
                string str = "";

                if (param_01_algorithm_hor_in != null)
                {
                    str = param_01_algorithm_hor_in;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_01_algorithm_hor_in = value; }
        }

        [CategoryAttribute("11 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_HOR_EX
        {
            get
            {
                string str = "";

                if (param_02_algorithm_hor_ex != null)
                {
                    str = param_02_algorithm_hor_ex;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_02_algorithm_hor_ex = value; }
        }
        

        //*****************************************************************************************

        [CategoryAttribute("11 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_VER_IN
        {
            get
            {
                string str = "";

                if (param_03_algorithm_ver_in != null)
                {
                    str = param_03_algorithm_ver_in;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_03_algorithm_ver_in = value; }
        }

        [CategoryAttribute("11 Measure Method"), DescriptionAttribute("Measure Method")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string MEASURE_VER_EX
        {
            get
            {
                string str = "";

                if (param_04_algorithm_ver_ex != null)
                {
                    str = param_04_algorithm_ver_ex;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_04_algorithm_ver_ex = value; }
        }


        //*****************************************************************************************

        [CategoryAttribute("15 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_HOR_IN { get { return param_05_edge_pos_hor_in;} set { param_05_edge_pos_hor_in = value;} }
        [CategoryAttribute("16 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_VER_IN { get { return param_07_edge_pos_ver_in; } set { param_07_edge_pos_ver_in = value; } }

        //*****************************************************************************************

        [CategoryAttribute("17 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_HOR_EX { get { return param_06_edge_pos_hor_ex; } set { param_06_edge_pos_hor_ex = value; } }
        [CategoryAttribute("18 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_VER_EX { get{return param_08_edge_pos_ver_ex;} set{param_08_edge_pos_ver_ex = value;} }

        //*****************************************************************************************

        [CategoryAttribute("20 COMPENSATION A "), DescriptionAttribute("True or False")]
        public double COMPEN_A {get{return param_comm_01_compen_A;} set{param_comm_01_compen_A =value;}}
        [CategoryAttribute("21 COMPENSATION B"), DescriptionAttribute("True or False")]
        public double COMPEN_B {get { return param_comm_02_compen_B;} set{ param_comm_02_compen_B = value;}}
        [CategoryAttribute("22 SPC-EF"), DescriptionAttribute("DEFAULT(0) : ADF(1), RVS(2), STD(3), MN(4), PWRVSUB(5), GRD(6), GRDRVSUB(7), POWRVSUBAD(8)")]
        public int SPC_ENHANCE { get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }
        [CategoryAttribute("23 Refinement"), DescriptionAttribute("DEFAULT(3) : Not Use 0, Refinement Distance(N)")]
        public int REFINEMENT { get { return param_09_refinement; } set { param_09_refinement = value; } }
        [CategoryAttribute("24 Shape Of Measure"), DescriptionAttribute("Default- OVL(0), Rectangle(1)")]
        public int SHAPE_OF_MEASURE { get {return param_10_shape_of_measure ;} set{param_10_shape_of_measure =value;} }
        [CategoryAttribute("33 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get{return param_comm_04_show_raw_data;} set{param_comm_04_show_raw_data = value;}}


        public CMeasurePairOvl ToFigure()
        {
            CMeasurePairOvl single = new CMeasurePairOvl();

            single.NICKNAME = NICKNAME;

             // for main croodinates 
            single.RC_HOR_IN.rc_FST = new parseRect(RC_HOR_IN_LFT.ToRectangleF());
            single.RC_HOR_IN.rc_SCD = new parseRect(RC_HOR_IN_RHT.ToRectangleF());
            single.RC_HOR_EX.rc_FST = new parseRect(RC_HOR_EX_LFT.ToRectangleF());
            single.RC_HOR_EX.rc_SCD = new parseRect(RC_HOR_EX_RHT.ToRectangleF());

            single.RC_VER_IN.rc_FST = new parseRect(RC_VER_IN_TOP.ToRectangleF());
            single.RC_VER_IN.rc_SCD = new parseRect(RC_VER_IN_BTM.ToRectangleF());
            single.RC_VER_EX.rc_FST = new parseRect(RC_VER_EX_TOP.ToRectangleF());
            single.RC_VER_EX.rc_SCD = new parseRect(RC_VER_EX_BTM.ToRectangleF());

            // for sub croodinates
            single.RC_HOR_IN._rc_FST = new parseRect(rc_HOR_IN_FST);
            single.RC_HOR_IN._rc_SCD = new parseRect(rc_HOR_IN_SCD);
            single.RC_HOR_EX._rc_FST = new parseRect(rc_HOR_EX_FST);
            single.RC_HOR_EX._rc_SCD = new parseRect(rc_HOR_EX_SCD);

            single.RC_VER_IN._rc_FST = new parseRect(rc_VER_IN_FST);
            single.RC_VER_IN._rc_SCD = new parseRect(rc_VER_IN_SCD);
            single.RC_VER_EX._rc_FST = new parseRect(rc_VER_EX_FST);
            single.RC_VER_EX._rc_SCD = new parseRect(rc_VER_EX_SCD);
            
            
            single.param_01_algorithm_HOR_IN = IFX_ALGORITHM.ToNumericType(param_01_algorithm_hor_in);
            single.param_02_algorithm_HOR_EX = IFX_ALGORITHM.ToNumericType(param_02_algorithm_hor_ex);
            single.param_03_algorithm_VER_IN = IFX_ALGORITHM.ToNumericType(param_03_algorithm_ver_in);
            single.param_04_algorithm_VER_EX = IFX_ALGORITHM.ToNumericType(param_04_algorithm_ver_ex);

            single.param_05_edge_position_hor_in = Convert.ToDouble(param_05_edge_pos_hor_in);
            single.param_06_edge_position_hor_ex = Convert.ToDouble(this.param_06_edge_pos_hor_ex);
            single.param_07_edge_position_ver_in = Convert.ToDouble(this.param_07_edge_pos_ver_in);
            single.param_08_edge_position_ver_ex = Convert.ToDouble(this.param_08_edge_pos_ver_ex);
            single.param_09_refinement /********/= this.param_09_refinement;
            single.param_10_shape_of_measure/***/= Convert.ToInt32(this.param_10_shape_of_measure);

            single.param_comm_01_compen_A/*************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B/*************/= this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance/**********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_show_raw_data/***/= this.param_comm_04_show_raw_data;

            return single;
        }
        public void FromFigure(CMeasurePairOvl single)
        {
            this.NICKNAME = single.NICKNAME;

            this.RC_HOR_IN_LFT = new CustomRectangleD(single.RC_HOR_IN.rc_FST.ToRectangleF());
            this.RC_HOR_IN_RHT = new CustomRectangleD(single.RC_HOR_IN.rc_SCD.ToRectangleF());
            this.RC_HOR_EX_LFT = new CustomRectangleD(single.RC_HOR_EX.rc_FST.ToRectangleF());
            this.RC_HOR_EX_RHT = new CustomRectangleD(single.RC_HOR_EX.rc_SCD.ToRectangleF());

            this.RC_VER_IN_TOP = new CustomRectangleD(single.RC_VER_IN.rc_FST.ToRectangleF());
            this.RC_VER_IN_BTM = new CustomRectangleD(single.RC_VER_IN.rc_SCD.ToRectangleF());
            this.RC_VER_EX_TOP = new CustomRectangleD(single.RC_VER_EX.rc_FST.ToRectangleF());
            this.RC_VER_EX_BTM = new CustomRectangleD(single.RC_VER_EX.rc_SCD.ToRectangleF());

            this.rc_HOR_IN_FST = single.RC_HOR_IN._rc_FST.ToRectangleF();
            this.rc_HOR_IN_SCD = single.RC_HOR_IN._rc_SCD.ToRectangleF();
            this.rc_HOR_EX_FST = single.RC_HOR_EX._rc_FST.ToRectangleF();
            this.rc_HOR_EX_SCD = single.RC_HOR_EX._rc_SCD.ToRectangleF();

            this.rc_VER_IN_FST = single.RC_VER_IN._rc_FST.ToRectangleF();
            this.rc_VER_IN_SCD = single.RC_VER_IN._rc_SCD.ToRectangleF();
            this.rc_VER_EX_FST = single.RC_VER_EX._rc_FST.ToRectangleF();
            this.rc_VER_EX_SCD = single.RC_VER_EX._rc_SCD.ToRectangleF();

            this.param_01_algorithm_hor_in/*****/= IFX_ALGORITHM.ToStringType(single.param_01_algorithm_HOR_IN);
            this.param_02_algorithm_hor_ex/*****/= IFX_ALGORITHM.ToStringType(single.param_02_algorithm_HOR_EX);
            this.param_03_algorithm_ver_in/*****/= IFX_ALGORITHM.ToStringType(single.param_03_algorithm_VER_IN);
            this.param_04_algorithm_ver_ex/*****/= IFX_ALGORITHM.ToStringType(single.param_04_algorithm_VER_EX);

            this.param_05_edge_pos_hor_in/******/= single.param_05_edge_position_hor_in;
            this.param_06_edge_pos_hor_ex/******/= single.param_06_edge_position_hor_ex;
            this.param_07_edge_pos_ver_in/******/= single.param_07_edge_position_ver_in;
            this.param_08_edge_pos_ver_ex/******/= single.param_08_edge_position_ver_ex;
            this.param_09_refinement/***********/= single.param_09_refinement;
            this.param_10_shape_of_measure/*****/= single.param_10_shape_of_measure;

            this.param_comm_01_compen_A/********/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B/********/= single.param_comm_02_compen_B;
            this.param_comm_03_spc_enhance/*****/= single.param_comm_03_spc_enhance;
            this.param_comm_04_show_raw_data/***/= single.param_comm_04_show_raw_data;
        }

    }
    public class PROPERTY_PairCir
    {
        private string/***/param_00_algorithm = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.DIR_EX);
        private double/***/param_01_damage_tolerance = 0;
        private bool/*****/param_02_bool_treat_as_ellipse = false;
        private int/******/param_03_circle_detect_type = 0;
        private double/***/param_04_Shrinkage =0.1;
        private int/******/param_05_Outlier_Filter = 0;
        private double/***/param_06_edge_pos =0;
        private string/***/param_07_coverage ="0";

        private double/***/param_comm_01_compen_A = 1;
        private double/***/param_comm_02_compen_B = 0;
        private int/******/param_comm_03_spc_enhance = 0;
        private bool/*****/param_comm_04_show_raw_data = false;


        public PROPERTY_PairCir()
        {
        }


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

        [CategoryAttribute("05 Pre Circle Detection"), DescriptionAttribute("DEFAULT(0) : DRVS(1), RVS(2), MNDIF(3), BRSA(4), GGRAD(5)")]
        public int CIRCLE_DETECT_TYPE { get { return param_03_circle_detect_type; } set { param_03_circle_detect_type = value; } }

        [CategoryAttribute("06 Estimated Circle Shrinkage"), DescriptionAttribute(" ±F(%)")]
        public double CIRCLE_SHRINKAGE { get { return param_04_Shrinkage; } set { param_04_Shrinkage = value; } }

        [CategoryAttribute("07 Data Filtering"), DescriptionAttribute("Available [ 0 = none, 1 = inside 2 = outside, 3 = dual ]")]
        public int OUTLIER_FILTER { get { return param_05_Outlier_Filter; } set { param_05_Outlier_Filter = value; } }

        [CategoryAttribute("08 Circle DAM Mag"), DescriptionAttribute("Available [ 0 ~ 0.99 (%)]")]
        public double DMG_IGNORANCE { get{return param_01_damage_tolerance;} set{param_01_damage_tolerance = value;} }

        [CategoryAttribute("09 Ellipse Process"), DescriptionAttribute("Treat As Ellipse\nTrue or False")]
        public bool TREAT_AS_ELLIPSE { get{return param_02_bool_treat_as_ellipse;} set{param_02_bool_treat_as_ellipse = value;} }

        [CategoryAttribute("10 SPC-EF"), DescriptionAttribute("DEFAULT(0) : ADF(1), RVS(2), STD(3), MN(4), PWRVSUB(5), GRD(6), GRDRVSUB(7), POWRVSUBAD(8)")]
        public int SPC_ENHANCE{ get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }

        [CategoryAttribute("11 Coverage"), DescriptionAttribute("DEFAULT=FULL(0) : DIG↘(1), DIG↙(2), PARTIAL(3, 1,...,12)")]
        public string COVERAGE { get { return param_07_coverage; } set { param_07_coverage= value; } }

        [CategoryAttribute("12 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get{return param_comm_04_show_raw_data;} set{param_comm_04_show_raw_data = value;} }

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
            single.param_07_Coverage /****************/= this.param_07_coverage;

            single.param_comm_01_compen_A /*************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B /*************/= this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance /**********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_show_raw_data /********/= this.param_comm_04_show_raw_data;

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
            this.param_07_coverage /****************/= single.param_07_Coverage;

            this.param_comm_01_compen_A /*************/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B /*************/= single.param_comm_02_compen_B;
            this.param_comm_03_spc_enhance /**********/= single.param_comm_03_spc_enhance;
            this.param_comm_04_show_raw_data /********/= single.param_comm_04_show_raw_data;
        }

    }
    public class PROPERTY_MixedRC
    {
        private string/***/param_00_algorithm_fst = IFX_ALGORITHM.ToStringType( IFX_ALGORITHM.CARDIN);
        private string/***/param_01_algorithm_scd = IFX_ALGORITHM.ToStringType( IFX_ALGORITHM.CARDIN);
        private double/***/param_02_edge_pos_fst = 0;
        private double/***/param_03_edge_pos_scd = 0;
        private string/***/param_04_rect_type_fst = IFX_RECT_TYPE.ToStringType(IFX_RECT_TYPE.DIR_HOR);
        private string/***/param_05_rect_type_scd = IFX_RECT_TYPE.ToStringType(IFX_RECT_TYPE.DIR_HOR);
        private int/******/param_06_refinement = 3;
        private int/******/param_07_dist_metric = 0;
        private bool/*****/param_08_use_centroid = false;

        private double/***/param_comm_01_compen_A = 1;
        private double/***/param_comm_02_compen_B = 0;
        private int/******/param_comm_03_spc_enhance = 0;
        private bool/*****/param_comm_04_show_raw_data = false;

        public PROPERTY_MixedRC()
        {
        }
        
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

        [CategoryAttribute("01 Rectangle Type FST"), DescriptionAttribute("Rectangle Type : HOR | VER")]
        public string RC_TYPE_FST { get { return param_04_rect_type_fst; } set { param_04_rect_type_fst = value; } }

        [CategoryAttribute("01 Rectangle Type FST"), DescriptionAttribute("Rectangle Type : HOR | VER")]
        public string RC_TYPE_SCD { get { return param_05_rect_type_scd; } set { param_05_rect_type_scd = value; } }

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

        [CategoryAttribute("03 Measure Algorithm FST"), DescriptionAttribute("Measure Algorithm { MEXHAT | DIR_IN | DIR_EX | CARDIN")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string ALGORITHM_FST
        {
            get
            {
                string str = "";

                if (param_00_algorithm_fst != null)
                {
                    str = param_00_algorithm_fst;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_00_algorithm_fst = value; }

        }

        [CategoryAttribute("03 Measure Algorithm SCD"), DescriptionAttribute("Measure Algorithm { MEXHAT | DIR_IN | DIR_EX | CARDIN")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string ALGORITHM_SCD
        {
            get
            {
                string str = "";

                if (param_01_algorithm_scd != null)
                {
                    str = param_01_algorithm_scd;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_01_algorithm_scd = value; }

        }

        [CategoryAttribute("04 DISTANCE METRIC"), DescriptionAttribute("P2P(0) HOR(1), VER(2)")]
        public int DIST_METRIC { get { return param_07_dist_metric; } set { param_07_dist_metric = value; } }

        [CategoryAttribute("09 Edge Position for FST"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ], In case of DIGONAL = [0.0, 0.5, 1.0]")]
        public double EDGE_POS_FST { get { return param_02_edge_pos_fst; } set { param_02_edge_pos_fst = value; } }
        [CategoryAttribute("10 Edge Position SCD"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ], In case of DIGONAL = [0.0, 0.5, 1.0]")]
        public double EDGE_POS_SCD { get { return param_03_edge_pos_scd; } set { param_03_edge_pos_scd = value; } }

        [CategoryAttribute("01 Rectangle Type"), DescriptionAttribute("Rectangle Type : HOR | VER"), ReadOnly(true)]
        public string RECT_TYPE_FST { get { return this.param_04_rect_type_fst; } set { this.param_04_rect_type_fst = value; } }

        [CategoryAttribute("01 Rectangle Type"), DescriptionAttribute("Rectangle Type : HOR | VER"), ReadOnly(true)]
        public string RECT_TYPE_SCD { get { return param_05_rect_type_scd; } set { param_05_rect_type_scd = value; } }

        [CategoryAttribute("11 Compensation A"), DescriptionAttribute("A of Ax + B")]
        public double COMPEN_A { get { return param_comm_01_compen_A; } set { param_comm_01_compen_A = value; } }
        [CategoryAttribute("12 Compensation B"), DescriptionAttribute("B of Ax + B")]
        public double COMPEN_B { get { return param_comm_02_compen_B; } set { param_comm_02_compen_B = value; } }

        [CategoryAttribute("13 SPC-EF"), DescriptionAttribute("DEFAULT(0) : ADF(1), RVS(2), STD(3), MN(4), PWRVSUB(5), GRD(6), GRDRVSUB(7), POWRVSUBAD(8)")]
        public int SPC_ENHANCE { get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }

        [CategoryAttribute("14 Refinement"), DescriptionAttribute("DEFAULT(3) : Not Use 0, Refinement Distance(N), Only For V/H")]
        public int REFINEMENT { get { return param_06_refinement; } set { param_06_refinement = value; } }

        [CategoryAttribute("15 USE CENTROID"), DescriptionAttribute("True or False")]
        public bool USE_CENTROID { get { return param_08_use_centroid; } set { param_08_use_centroid = value; } }

        [CategoryAttribute("16 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get { return param_comm_04_show_raw_data; } set { param_comm_04_show_raw_data = value; } }

        public CMeasureMixedRC ToFigure()
        {
            CMeasureMixedRC single = new CMeasureMixedRC();

            single.NICKNAME = NICKNAME;

            single.rc_FST = CustomPointD.ToParseRect(pt_FST_LT, pt_FST_RT, pt_FST_LB, pt_FST_RB);
            single.rc_SCD = CustomPointD.ToParseRect(pt_SCD_LT, pt_SCD_RT, PT_SCD_LB, pt_SCD_RB);

            single._rc_FST = CustomPointD.ToParseRect(_pt_fst_lt, _pt_fst_rt, _pt_fst_lb, _pt_fst_rb);
            single._rc_SCD = CustomPointD.ToParseRect(_pt_scd_lt, _pt_scd_rt, _pt_scd_lb, _pt_scd_rb);

            single.param_00_algorithm_fst/********/= IFX_ALGORITHM.ToNumericType(this.param_00_algorithm_fst);
            single.param_01_algorithm_scd/********/= IFX_ALGORITHM.ToNumericType(this.param_01_algorithm_scd);
            single.param_02_edge_position_fst/****/= this.param_02_edge_pos_fst;
            single.param_03_edge_position_scd/****/= this.param_03_edge_pos_scd;
            single.param_04_rc_type_fst /*********/= IFX_RECT_TYPE.ToNumericType(this.param_04_rect_type_fst);
            single.param_05_rc_type_scd /*********/= IFX_RECT_TYPE.ToNumericType(this.param_05_rect_type_scd);
            single.param_06_refinement /**********/= this.param_06_refinement;
            single.param_07_metric_type /*********/= this.param_07_dist_metric;
            single.param_08_use_centroid /********/= this.param_08_use_centroid;
            
            single.param_comm_01_compen_A/************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B/************/= this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance /********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_show_raw_data/*******/= this.param_comm_04_show_raw_data;

            return single;
        }
        public void FromFigure(CMeasureMixedRC single)
        {
            this.NICKNAME = single.NICKNAME;

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


            this.param_00_algorithm_fst/*******/= IFX_ALGORITHM.ToStringType(single.param_00_algorithm_fst);
            this.param_01_algorithm_scd/*******/= IFX_ALGORITHM.ToStringType(single.param_01_algorithm_scd);
            this.param_02_edge_pos_fst/********/= single.param_02_edge_position_fst;
            this.param_03_edge_pos_scd/********/= single.param_03_edge_position_scd; 
            this.param_04_rect_type_fst /******/= IFX_RECT_TYPE.ToStringType(single.param_04_rc_type_fst);
            this.param_05_rect_type_scd /******/= IFX_RECT_TYPE.ToStringType(single.param_05_rc_type_scd); 
            this.param_06_refinement/**********/= single.param_06_refinement;
            this.param_07_dist_metric /********/= single.param_07_metric_type;
            this.param_08_use_centroid /*******/= single.param_08_use_centroid;
            

            this.param_comm_01_compen_A/*************/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B/*************/= single.param_comm_02_compen_B;
            this.param_comm_03_spc_enhance/**********/= single.param_comm_03_spc_enhance;
            this.param_comm_04_show_raw_data /*******/= single.param_comm_04_show_raw_data;
        }
    }
    public class PROPERTY_MixedCC
    {
        private string param_00_algorithm_fst = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        private string param_01_algorithm_scd = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        private double param_02_edge_pos_fst = 0;
        private double param_03_edge_pos_scd = 0;
        private string param_04_coverage_fst = "0";
        private string param_05_coverage_scd = "0";
        private int/**/param_06_ms_dir_fst = 0;
        private int/**/param_07_ms_pos_scd = 0;
        private int/**/param_08_dist_metric = 0;

        private double/***/param_comm_01_compen_A = 1;
        private double/***/param_comm_02_compen_B = 0;
        private int/******/param_comm_03_spc_enhance = 0;
        private bool/*****/param_comm_04_show_raw_data = false;

        public PROPERTY_MixedCC()
        {
        }

        private CustomRectangleD rc_FST_EX = new CustomRectangleD();
        private CustomRectangleD rc_FST_IN = new CustomRectangleD();
        private CustomRectangleD _rc_FST_EX = new CustomRectangleD();
        private CustomRectangleD _rc_FST_IN = new CustomRectangleD();

        private CustomRectangleD rc_SCD_EX = new CustomRectangleD();
        private CustomRectangleD rc_SCD_IN = new CustomRectangleD();
        private CustomRectangleD _rc_SCD_EX = new CustomRectangleD();
        private CustomRectangleD _rc_SCD_IN = new CustomRectangleD();

        [CategoryAttribute("00 Nick Name"), DescriptionAttribute("Figure Nick Name"), ReadOnly(true)]
        public string NICKNAME { get; set; }

        [CategoryAttribute("01 Measure Method"), DescriptionAttribute("Measure Method for FIRST CIRCLE")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string ALGORITHM_FST
        {
            get
            {
                string str = "";

                if (param_00_algorithm_fst != null)
                {
                    str = param_00_algorithm_fst;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_00_algorithm_fst = value; }

        }

        [CategoryAttribute("02 Measure Method"), DescriptionAttribute("Measure Method for SECOND CIRCLE")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string ALGORITHM_SCD
        {
            get
            {
                string str = "";

                if (param_01_algorithm_scd != null)
                {
                    str = param_01_algorithm_scd;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_01_algorithm_scd = value; }

        }

        [CategoryAttribute("02 Searching Direction"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS_FST { get { return param_02_edge_pos_fst; } set { param_02_edge_pos_fst = value; } }

        [CategoryAttribute("02 Searching Direction"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS_SCD { get { return param_03_edge_pos_scd; } set { param_03_edge_pos_scd = value; } }

        [CategoryAttribute("03 DISTANCE METRIC"), DescriptionAttribute("P2P(0) HOR(1), VER(2)")]
        public int DIST_METRIC { get { return param_08_dist_metric; } set { param_08_dist_metric = value; } }

        [CategoryAttribute("03 Compensation A"), DescriptionAttribute("A of Ax + B")]
        public double COMPEN_A { get { return param_comm_01_compen_A; } set { param_comm_01_compen_A = value; } }
        [CategoryAttribute("04 Compensation B"), DescriptionAttribute("B of Ax + B")]
        public double COMPEN_B { get { return param_comm_02_compen_B; } set { param_comm_02_compen_B = value; } }

        [CategoryAttribute("05 SPC-EF"), DescriptionAttribute("DEFAULT(0) : ADF(1), RVS(2), STD(3), MN(4), PWRVSUB(5), GRD(6), GRDRVSUB(7), POWRVSUBAD(8)")]
        public int SPC_ENHANCE { get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }

        [CategoryAttribute("06 Coverage"), DescriptionAttribute("DEFAULT=FULL(0) : DIG↘(1), DIG↙(2), PARTIAL(3, 1,...,12)")]
        public string COVERAGE_FST { get { return param_04_coverage_fst; } set { param_04_coverage_fst = value; } }

        [CategoryAttribute("07 Coverage"), DescriptionAttribute("DEFAULT=FULL(0) : DIG↘(1), DIG↙(2), PARTIAL(3, 1,...,12)")]
        public string COVERAGE_SCD { get { return param_05_coverage_scd; } set { param_05_coverage_scd = value; } }

        [CategoryAttribute("08 Measurement Direction for First Circle"), DescriptionAttribute("LFT(0), TOP(1) RHT(2) BTM(3)")]
        public int MS_DIR_FST { get { return param_06_ms_dir_fst; } set { param_06_ms_dir_fst= value; } }

        [CategoryAttribute("09 Measurement Direction for Second Circle"), DescriptionAttribute("LFT(0), TOP(1) RHT(2) BTM(3)")]
        public int MS_DIR_SCD { get { return param_07_ms_pos_scd; } set { param_07_ms_pos_scd = value; } }

        [CategoryAttribute("10 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get { return param_comm_04_show_raw_data; } set { param_comm_04_show_raw_data = value; } }


        public CMeasureMixedCC ToFigure()
        {
            CMeasureMixedCC single = new CMeasureMixedCC();

            single.NICKNAME = NICKNAME;

            single.rc_FST_EX = rc_FST_EX.ToRectangleF();
            single.rc_FST_IN = rc_FST_IN.ToRectangleF();
            single._rc_FST_EX = _rc_FST_EX.ToRectangleF();
            single._rc_FST_IN = _rc_FST_IN.ToRectangleF();

            single. rc_SCD_EX =  rc_SCD_EX.ToRectangleF();
            single. rc_SCD_IN =  rc_SCD_IN.ToRectangleF();
            single._rc_SCD_EX = _rc_SCD_EX.ToRectangleF();
            single._rc_SCD_IN = _rc_SCD_IN.ToRectangleF();

            single.param_00_algorithm_fst /***********/= IFX_ALGORITHM.ToNumericType(this.param_00_algorithm_fst);
            single.param_01_algorithm_scd /***********/= IFX_ALGORITHM.ToNumericType(this.param_01_algorithm_scd);

            single.param_02_edge_position_fst /*******/= this.param_02_edge_pos_fst;
            single.param_03_edge_position_scd /*******/= this.param_03_edge_pos_scd;
            single.param_04_Coverage_fst /************/= this.param_04_coverage_fst;
            single.param_05_Coverage_scd /************/= this.param_05_coverage_scd;
            single.param_06_ms_pos_fst /**************/= this.param_06_ms_dir_fst;
            single.param_07_ms_pos_scd /**************/= this.param_07_ms_pos_scd;
            single.param_08_metric_type /*************/= this.param_08_dist_metric;
            
            single.param_comm_01_compen_A /*************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B /*************/= this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance /**********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_show_raw_data /********/= this.param_comm_04_show_raw_data;

            return single;
        }

        public void FromFigure(CMeasureMixedCC single)
        {
            this.NICKNAME = single.NICKNAME;

            this.rc_FST_EX.SetValue(single.rc_FST_EX);
            this.rc_FST_IN.SetValue(single.rc_FST_IN);
            this._rc_FST_EX.SetValue(single._rc_FST_EX);
            this._rc_FST_IN.SetValue(single._rc_FST_IN);

            this. rc_SCD_EX.SetValue(single. rc_SCD_EX);
            this. rc_SCD_IN.SetValue(single. rc_SCD_IN);
            this._rc_SCD_EX.SetValue(single._rc_SCD_EX);
            this._rc_SCD_IN.SetValue(single._rc_SCD_IN);

            this.param_00_algorithm_fst /***************/= IFX_ALGORITHM.ToStringType(single.param_00_algorithm_fst);
            this.param_01_algorithm_scd /***************/= IFX_ALGORITHM.ToStringType(single.param_01_algorithm_scd);
            this.param_02_edge_pos_fst /****************/= single.param_02_edge_position_fst;
            this.param_03_edge_pos_scd/*****************/= single.param_03_edge_position_scd;
            this.param_04_coverage_fst /****************/= single.param_04_Coverage_fst;
            this.param_05_coverage_scd /****************/= single.param_05_Coverage_scd;
            this.param_06_ms_dir_fst /******************/= single.param_06_ms_pos_fst;
            this.param_07_ms_pos_scd /******************/= single.param_07_ms_pos_scd;
            this.param_08_dist_metric /*****************/= single.param_08_metric_type;

            this.param_comm_01_compen_A /***************/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B /***************/= single.param_comm_02_compen_B;
            this.param_comm_03_spc_enhance /************/= single.param_comm_03_spc_enhance;
            this.param_comm_04_show_raw_data /**********/= single.param_comm_04_show_raw_data;
        }

    }
    public class PROPERTY_MixedRCC
    {
        private double/***/param_comm_01_compen_A = 1;
        private double/***/param_comm_02_compen_B= 0;
        private int/******/param_comm_03_spc_enhance = 0;
        private bool/*****/param_comm_04_show_raw_data = false;

        private string/***/param_00_algorithm_fst = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        private double/***/param_01_edge_position_fst =0;
        private string/***/param_02_rc_type_fst = IFX_RECT_TYPE.ToStringType(IFX_RECT_TYPE.DIR_HOR);
        private int/******/param_03_refinement = 3;
        private bool/*****/param_04_use_centroid = false;

        private string/***/param_11_algorithm_scd = IFX_ALGORITHM.ToStringType(IFX_ALGORITHM.CARDIN);
        private double/***/param_12_edge_position_scd= 0;
        public string/****/param_13_Coverage_scd= "0";
        public int/*******/param_14_ms_pos_scd = 0;

        private int/******/param_20_metric_type = 0;

        public PROPERTY_MixedRCC()
        {
        }

        // surface position croodinate
        private CustomPointD pt_RC_LT = new CustomPointD();
        private CustomPointD pt_RC_RT = new CustomPointD();
        private CustomPointD pt_RC_LB = new CustomPointD();
        private CustomPointD pt_RC_RB = new CustomPointD();

        // hidden position croodinate
        private CustomPointD _pt_rc_lt = new CustomPointD();
        private CustomPointD _pt_rc_rt = new CustomPointD();
        private CustomPointD _pt_rc_lb = new CustomPointD();
        private CustomPointD _pt_rc_rb = new CustomPointD();

        private CustomRectangleD rc_CIRCLE_EX = new CustomRectangleD();
        private CustomRectangleD rc_CIRCLE_IN = new CustomRectangleD();
        private CustomRectangleD _rc_CIRCLE_EX = new CustomRectangleD();
        private CustomRectangleD _rc_CIRCLE_IN = new CustomRectangleD();

        [CategoryAttribute("00 Nick Name"), DescriptionAttribute("Figure Nick Name"), ReadOnly(true)]
        public string NICKNAME { get; set; }

        [CategoryAttribute("01 Measure Method"), DescriptionAttribute("Measure Method for FIRST RECTANGLE")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string ALGORITHM_FST
        {
            get
            {
                string str = "";

                if (param_00_algorithm_fst != null)
                {
                    str = param_00_algorithm_fst;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_00_algorithm_fst = value; }

        }

       
        [CategoryAttribute("01 Rectangle Type"), DescriptionAttribute("Rectangle Type : HOR | VER"), ReadOnly(true)]
        public string RECT_TYPE_FST { get { return this.param_02_rc_type_fst; } set { this.param_02_rc_type_fst = value; } }

        [CategoryAttribute("02 Edge Position"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS_RC { get { return param_01_edge_position_fst; } set { param_01_edge_position_fst = value; } }


        [CategoryAttribute("14 Refinement"), DescriptionAttribute("DEFAULT(3) : Not Use 0, Refinement Distance(N), Only For V/H")]
        public int REFINEMENT { get { return param_03_refinement; } set { param_03_refinement = value; } }

        [CategoryAttribute("15 USE CENTROID"), DescriptionAttribute("True or False")]
        public bool USE_CENTROID { get { return param_04_use_centroid; } set { param_04_use_centroid = value; } }

        [CategoryAttribute("02 Measure Method"), DescriptionAttribute("Measure Method for SECOND CIRCLE")]
        [Browsable(true)]
        [TypeConverter(typeof(ConverterMeasureTyes))]
        public string ALGORITHM_SCD
        {
            get
            {
                string str = "";

                if (param_11_algorithm_scd != null)
                {
                    str = param_11_algorithm_scd;
                }
                else
                {
                    str = IFX_ALGORITHM.ToStringType(0);
                }
                return str;
            }

            set { param_11_algorithm_scd = value; }

        }
        

        [CategoryAttribute("02 Searching Direction"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS_CC { get { return param_12_edge_position_scd; } set { param_12_edge_position_scd= value; } }

        [CategoryAttribute("06 Coverage"), DescriptionAttribute("DEFAULT=FULL(0) : DIG↘(1), DIG↙(2), PARTIAL(3, 1,...,12)")]
        public string COVERAGE_CIRCLE { get { return param_13_Coverage_scd; } set { param_13_Coverage_scd = value; } }

        [CategoryAttribute("08 Measurement Direction for First Circle"), DescriptionAttribute("East=(0), West=(1), South=(2), North=(3)")]
        public int MS_DIR_CIRCLE { get { return param_14_ms_pos_scd; } set { param_14_ms_pos_scd = value; } }

        [CategoryAttribute("03 DISTANCE METRIC"), DescriptionAttribute("P2P(0) HOR(1), VER(2)")]
        public int DIST_METRIC { get { return param_20_metric_type; } set { param_20_metric_type = value; } }


        [CategoryAttribute("03 Compensation A"), DescriptionAttribute("A of Ax + B")]
        public double COMPEN_A { get { return param_comm_01_compen_A; } set { param_comm_01_compen_A = value; } }
        [CategoryAttribute("04 Compensation B"), DescriptionAttribute("B of Ax + B")]
        public double COMPEN_B { get { return param_comm_02_compen_B; } set { param_comm_02_compen_B = value; } }

        [CategoryAttribute("05 SPC-EF"), DescriptionAttribute("DEFAULT(0) : ADF(1), RVS(2), STD(3), MN(4), PWRVSUB(5), GRD(6), GRDRVSUB(7), POWRVSUBAD(8)")]
        public int SPC_ENHANCE { get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }


        [CategoryAttribute("10 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get { return param_comm_04_show_raw_data; } set { param_comm_04_show_raw_data = value; } }


        public CMeasureMixedRCC ToFigure()
        {
            CMeasureMixedRCC single = new CMeasureMixedRCC();

            single.NICKNAME = NICKNAME;

            single.rc_FST = CustomPointD.ToParseRect(pt_RC_LT, pt_RC_RT, pt_RC_LB, pt_RC_RB);
            single._rc_FST = CustomPointD.ToParseRect(_pt_rc_lt, _pt_rc_rt, _pt_rc_lb, _pt_rc_rb);

            single.rc_FST_EX =  rc_CIRCLE_EX.ToRectangleF();
            single.rc_FST_IN =  rc_CIRCLE_IN.ToRectangleF();
            single._rc_FST_EX= _rc_CIRCLE_EX.ToRectangleF();
            single._rc_FST_IN = _rc_CIRCLE_IN.ToRectangleF();

            single.param_00_algorithm_fst /***********/= IFX_ALGORITHM.ToNumericType(this.param_00_algorithm_fst);
            single.param_01_edge_position_fst /*******/= this.param_01_edge_position_fst;
            single.param_02_rc_type_fst /*************/= IFX_RECT_TYPE.ToNumericType(this.param_02_rc_type_fst);
            single.param_03_refinement /**************/= this.param_03_refinement;
            single.param_04_use_centroid /************/= this.param_04_use_centroid;

            single.param_11_algorithm_scd /***********/= IFX_ALGORITHM.ToNumericType(this.param_11_algorithm_scd);
            single.param_12_edge_position_scd /*******/= this.param_12_edge_position_scd;
            single.param_13_Coverage_scd /************/= this.param_13_Coverage_scd;
            single.param_14_ms_pos_scd /**************/= this.param_14_ms_pos_scd;

            single.param_20_metric_type /*************/= this.param_20_metric_type;

            single.param_comm_01_compen_A /*************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B /*************/= this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance /**********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_show_raw_data /********/= this.SHOW_RAW_DATA;

            return single;
        }

        public void FromFigure(CMeasureMixedRCC single)
        {
            this.NICKNAME = single.NICKNAME;

            this.pt_RC_LT.SetValue(single.rc_FST.LT);
            this.pt_RC_RT.SetValue(single.rc_FST.RT);
            this.pt_RC_LB.SetValue(single.rc_FST.LB);
            this.pt_RC_RB.SetValue(single.rc_FST.RB);

            this._pt_rc_lt.SetValue(single._rc_FST.LT);
            this._pt_rc_rt.SetValue(single._rc_FST.RT);
            this._pt_rc_lb.SetValue(single._rc_FST.LB);
            this._pt_rc_rb.SetValue(single._rc_FST.RB);

            this. rc_CIRCLE_EX.SetValue(single.rc_FST_EX);
            this. rc_CIRCLE_IN.SetValue(single.rc_FST_IN);
            this._rc_CIRCLE_EX.SetValue(single._rc_FST_EX);
            this._rc_CIRCLE_IN.SetValue(single._rc_FST_IN);

            this.param_00_algorithm_fst = IFX_ALGORITHM.ToStringType(single.param_00_algorithm_fst);
            this.param_01_edge_position_fst = single.param_01_edge_position_fst;
            this.param_02_rc_type_fst = IFX_RECT_TYPE.ToStringType(single.param_02_rc_type_fst);
            this.param_03_refinement = single.param_03_refinement;
            this.param_04_use_centroid = single.param_04_use_centroid;

            this.param_11_algorithm_scd = IFX_ALGORITHM.ToStringType(single.param_11_algorithm_scd);
            this.param_12_edge_position_scd = single.param_12_edge_position_scd;
            this.param_13_Coverage_scd = single.param_13_Coverage_scd;
            this.param_14_ms_pos_scd = single.param_14_ms_pos_scd;
            this.param_20_metric_type = single.param_20_metric_type;

            this.param_comm_01_compen_A /*************/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B /*************/= single.param_comm_02_compen_B;
            this.param_comm_03_spc_enhance /**********/= single.param_comm_03_spc_enhance;
            this.SHOW_RAW_DATA /**********************/= single.param_comm_04_show_raw_data;
        }

    }
}
