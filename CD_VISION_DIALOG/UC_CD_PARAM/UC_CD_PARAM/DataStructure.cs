﻿using System;
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
        public PROPERTY_PairRct()
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
            param_comm_03_spc_enhance = 0;
            param_comm_04_BOOL_SHOW_RAW_DATA = false;
            

        }
        private double param_comm_01_compen_A;
        private double param_comm_02_compen_B;
        private int    param_comm_03_spc_enhance;
        private bool param_comm_04_BOOL_SHOW_RAW_DATA;

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

        [CategoryAttribute("04 Auto Peak detection"), DescriptionAttribute("true | false")]
        public bool USE_AUTO_PEAK_DETECTION{ get{return param_01_bool_use_auto_peak_detection;} set{param_01_bool_use_auto_peak_detection = value;} }

        [CategoryAttribute("05 Peak Target Index For FST "), DescriptionAttribute("If Use Auto Peak Detection,   0 ~  Peak Candidate count")]
        public double PEAK_TARGET_IDX_FST{ get { return param_02_peak_target_index_fst; } set { param_02_peak_target_index_fst= value; } }

        [CategoryAttribute("06 Peak Target Index For SCD"), DescriptionAttribute("If Use Auto Peak Detection 0 ~ Peak Candidate ")]
        public double PEAK_TARGET_IDX_SCD { get { return param_03_peak_target_index_scd; } set { param_03_peak_target_index_scd = value; } }

        [CategoryAttribute("07 Peak Candidate Count"), DescriptionAttribute("2 ~ ∞")]
        public double PEAK_CANDIDATE { get { return param_04_peak_candidate; } set { param_04_peak_candidate = value; } }

        [CategoryAttribute("08 Window Size"), DescriptionAttribute("0 & 2 ~ ∞")]
        public double WINDOW_SIZE { get { return param_05_window_size; } set { param_05_window_size= value; } }


        [CategoryAttribute("09 Edge Position for FST"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS_FST { get{return param_06_edge_pos_fst;} set{param_06_edge_pos_fst=value;} }
        [CategoryAttribute("10 Edge Position SCD"), DescriptionAttribute("Available [ 0.0 ~ 1.0 ]")]
        public double EDGE_POS_SCD { get { return param_07_edge_pos_scd; } set { param_07_edge_pos_scd = value; } }

        [CategoryAttribute("11 Compensation A"), DescriptionAttribute("A of Ax + B")]
        public double COMPEN_A { get { return param_comm_01_compen_A; } set { param_comm_01_compen_A = value; } }
        [CategoryAttribute("12 Compensation B"), DescriptionAttribute("B of Ax + B")]
        public double COMPEN_B { get{ return param_comm_02_compen_B;} set{param_comm_02_compen_B=value;} }

        [CategoryAttribute("13 SPC Enhance"), DescriptionAttribute("DEFAULT(0) : 1, 2 ..")]
        public int SPC_ENHANCE { get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }

        [CategoryAttribute("14 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get { return param_comm_04_BOOL_SHOW_RAW_DATA; } set { param_comm_04_BOOL_SHOW_RAW_DATA = value; } }

        public CMeasurePairRct ToFigure()
        {
            CMeasurePairRct single = new CMeasurePairRct();

            single.NICKNAME = NICKNAME;

            single.RC_TYPE = IFX_RECT_TYPE.ToNumericType(RECT_TYPE);

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
            single.param_comm_03_spc_enhance /***********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_BOOL_SHOW_RAW_DATA/**/= this.param_comm_04_BOOL_SHOW_RAW_DATA;
            return single;
        }
        public void FromFigure(CMeasurePairRct single)
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

            this.RECT_TYPE = IFX_RECT_TYPE.ToStringType(single.RC_TYPE);

            this.param_comm_01_compen_A/*************/= single.param_comm_01_compen_A;
            this.param_comm_02_compen_B/*************/= single.param_comm_02_compen_B;
            this.param_comm_03_spc_enhance/**********/= single.param_comm_03_spc_enhance;
            this.param_comm_04_BOOL_SHOW_RAW_DATA /**/= single.param_comm_04_BOOL_SHOW_RAW_DATA;
        }
    }

    public class PROPERTY_PairOvl
    {
        private double param_comm_01_compen_A { get; set; }
        private double param_comm_02_compen_B { get; set; }
        private int/**/param_comm_03_spc_enhance { get; set; }
        private bool param_comm_04_show_raw_data { get; set; }


        public PROPERTY_PairOvl()
        {
            param_comm_01_compen_A = 1;
            param_comm_02_compen_B = 0;
            param_comm_03_spc_enhance = 0;
            param_comm_04_show_raw_data = false;
        }
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

        [CategoryAttribute("15 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_HOR_IN_L { get; set; }
        [CategoryAttribute("16 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_HOR_IN_R { get; set; }
        [CategoryAttribute("17 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_VER_IN_T { get; set; }
        [CategoryAttribute("18 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_VER_IN_B { get; set; }

        //*****************************************************************************************

        [CategoryAttribute("19 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_HOR_EX_L { get; set; }
        [CategoryAttribute("20 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_HOR_EX_R { get; set; }
        [CategoryAttribute("21 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_VER_EX_T { get; set; }
        [CategoryAttribute("22 Edge Pos"), DescriptionAttribute("Available [ -1, 0, +1 ]")]
        public double EDGE_POS_VER_EX_B { get; set; }


        //*****************************************************************************************

        [CategoryAttribute("31 COMPENSATION A "), DescriptionAttribute("True or False")]
        public double COMPEN_A {get{return param_comm_01_compen_A;} set{param_comm_01_compen_A =value;}}
        [CategoryAttribute("31 COMPENSATION B"), DescriptionAttribute("True or False")]
        public double COMPEN_B {get { return param_comm_02_compen_B;} set{ param_comm_02_compen_B = value;}}
        [CategoryAttribute("31 Show Raw Data"), DescriptionAttribute("True or False")]
        public int SPC_ENHANCE { get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }
        [CategoryAttribute("31 Show Raw Data"), DescriptionAttribute("True or False")]
        public bool SHOW_RAW_DATA { get{return param_comm_04_show_raw_data;} set{param_comm_04_show_raw_data = value;}}


        public CMeasurePairOvl ToFigure()
        {
            CMeasurePairOvl single = new CMeasurePairOvl();

            single.NICKNAME = NICKNAME;

            single.algorithm_HOR_IN_L = IFX_ALGORITHM.ToNumericType(measureHOR_IN_L);
            single.algorithm_HOR_IN_R = IFX_ALGORITHM.ToNumericType(measureHOR_IN_R);
            single.algorithm_HOR_EX_L = IFX_ALGORITHM.ToNumericType(measureHOR_EX_L);
            single.algorithm_HOR_EX_R = IFX_ALGORITHM.ToNumericType(measureHOR_EX_R);

            single.algorithm_VER_IN_T = IFX_ALGORITHM.ToNumericType(measureVER_IN_T);
            single.algorithm_VER_IN_B = IFX_ALGORITHM.ToNumericType(measureVER_IN_B);
            single.algorithm_VER_EX_T = IFX_ALGORITHM.ToNumericType(measureVER_EX_T);
            single.algorithm_VER_EX_B = IFX_ALGORITHM.ToNumericType(measureVER_EX_B);

            single.RC_HOR_IN.rc_FST = new parseRect(RC_HOR_IN_LFT.ToRectangleF());
            single.RC_HOR_IN.rc_SCD = new parseRect(RC_HOR_IN_RHT.ToRectangleF());
            single.RC_HOR_EX.rc_FST = new parseRect(RC_HOR_EX_LFT.ToRectangleF());
            single.RC_HOR_EX.rc_SCD = new parseRect(RC_HOR_EX_RHT.ToRectangleF());

            single.RC_VER_IN.rc_FST = new parseRect(RC_VER_IN_TOP.ToRectangleF());
            single.RC_VER_IN.rc_SCD = new parseRect(RC_VER_IN_BTM.ToRectangleF());
            single.RC_VER_EX.rc_FST = new parseRect(RC_VER_EX_TOP.ToRectangleF());
            single.RC_VER_EX.rc_SCD = new parseRect(RC_VER_EX_BTM.ToRectangleF());

            
            single.param_01_edge_position_hor_in_L = Convert.ToInt32(EDGE_POS_HOR_IN_L);
            single.param_02_edge_position_hor_in_R = Convert.ToInt32(EDGE_POS_HOR_IN_R);
            single.param_03_edge_position_hor_ex_L = Convert.ToInt32(EDGE_POS_HOR_EX_L);
            single.param_04_edge_position_hor_ex_R = Convert.ToInt32(EDGE_POS_HOR_EX_R);

            single.param_05_edge_position_ver_in_T = Convert.ToInt32(EDGE_POS_VER_IN_T);
            single.param_06_edge_position_ver_in_B = Convert.ToInt32(EDGE_POS_VER_IN_B);
            single.param_07_edge_position_ver_ex_T = Convert.ToInt32(EDGE_POS_VER_EX_T);
            single.param_08_edge_position_ver_ex_B = Convert.ToInt32(EDGE_POS_VER_EX_B);

            single.param_comm_01_compen_A = this.param_comm_01_compen_A;
            single.param_comm_02_compen_B = this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance = this.param_comm_03_spc_enhance;
            single.param_comm_04_BOOL_SHOW_RAW_DATA = this.SHOW_RAW_DATA;

            return single;
        }
        public void FromFigure(CMeasurePairOvl single)
        {
            this.NICKNAME = single.NICKNAME;

            this.MEASURE_HOR_IN_L = IFX_ALGORITHM.ToStringType(single.algorithm_HOR_IN_L);
            this.MEASURE_HOR_IN_R = IFX_ALGORITHM.ToStringType(single.algorithm_HOR_IN_R);
            this.MEASURE_HOR_EX_L = IFX_ALGORITHM.ToStringType(single.algorithm_HOR_EX_L);
            this.MEASURE_HOR_EX_R = IFX_ALGORITHM.ToStringType(single.algorithm_HOR_EX_R);

            this.MEASURE_VER_IN_T = IFX_ALGORITHM.ToStringType(single.algorithm_VER_IN_T);
            this.MEASURE_VER_IN_B = IFX_ALGORITHM.ToStringType(single.algorithm_VER_IN_B);
            this.MEASURE_VER_EX_T = IFX_ALGORITHM.ToStringType(single.algorithm_VER_EX_T);
            this.MEASURE_VER_EX_B = IFX_ALGORITHM.ToStringType(single.algorithm_VER_EX_B);

            this.RC_HOR_IN_LFT = new CustomRectangleD(single.RC_HOR_IN.rc_FST.ToRectangleF());
            this.RC_HOR_IN_RHT = new CustomRectangleD(single.RC_HOR_IN.rc_SCD.ToRectangleF());
            this.RC_HOR_EX_LFT = new CustomRectangleD(single.RC_HOR_EX.rc_FST.ToRectangleF());
            this.RC_HOR_EX_RHT = new CustomRectangleD(single.RC_HOR_EX.rc_SCD.ToRectangleF());

            this.RC_VER_IN_TOP = new CustomRectangleD(single.RC_VER_IN.rc_FST.ToRectangleF());
            this.RC_VER_IN_BTM = new CustomRectangleD(single.RC_VER_IN.rc_SCD.ToRectangleF());
            this.RC_VER_EX_TOP = new CustomRectangleD(single.RC_VER_EX.rc_FST.ToRectangleF());
            this.RC_VER_EX_BTM = new CustomRectangleD(single.RC_VER_EX.rc_SCD.ToRectangleF());

            this.EDGE_POS_HOR_IN_L = single.param_01_edge_position_hor_in_L;
            this.EDGE_POS_HOR_IN_R = single.param_02_edge_position_hor_in_R;
            this.EDGE_POS_HOR_EX_L = single.param_03_edge_position_hor_ex_L;
            this.EDGE_POS_HOR_EX_R = single.param_04_edge_position_hor_ex_R;

            this.EDGE_POS_VER_IN_T= single.param_05_edge_position_ver_in_T;
            this.EDGE_POS_VER_IN_B = single.param_06_edge_position_ver_in_B;
            this.EDGE_POS_VER_EX_T = single.param_07_edge_position_ver_ex_T;
            this.EDGE_POS_VER_EX_B = single.param_08_edge_position_ver_ex_B;

            this.param_comm_01_compen_A = single.param_comm_01_compen_A;
            this.param_comm_02_compen_B = single.param_comm_02_compen_B;
            this.param_comm_03_spc_enhance = single.param_comm_03_spc_enhance;
            this.param_comm_04_show_raw_data = single.param_comm_04_BOOL_SHOW_RAW_DATA;
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
            param_comm_03_spc_enhance = 0;
            param_comm_04_show_raw_data = false;
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
        public int param_comm_03_spc_enhance;
        public bool param_comm_04_show_raw_data;

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

        [CategoryAttribute("10 Special Enhancement"), DescriptionAttribute("Default(0), 1, 2...")]
        public int SPC_ENHANCE{ get { return param_comm_03_spc_enhance; } set { param_comm_03_spc_enhance = value; } }

        [CategoryAttribute("10 Show Raw Data"), DescriptionAttribute("True or False")]
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

            single.param_comm_01_compen_A /*************/= this.param_comm_01_compen_A;
            single.param_comm_02_compen_B /*************/= this.param_comm_02_compen_B;
            single.param_comm_03_spc_enhance /**********/= this.param_comm_03_spc_enhance;
            single.param_comm_04_BOOL_SHOW_RAW_DATA /***/= this.param_comm_04_show_raw_data;


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
            this.param_comm_03_spc_enhance /**********/= single.param_comm_03_spc_enhance;
            this.param_comm_04_show_raw_data /***/= single.param_comm_04_BOOL_SHOW_RAW_DATA;
        }

    }

}
