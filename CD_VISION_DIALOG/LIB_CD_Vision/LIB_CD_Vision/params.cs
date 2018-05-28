using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Drawing;
using System.Drawing.Imaging;

namespace DEF_PARAMS
{
    //*****************************************************************************************
    // Actual Parameters 

    public class CAdvancedMode
    {
        public bool/*****/BOOL_SHOW_IMAGE_PROCESS/****************/= false;
        public bool/*****/BOOL_USE_SAVE_MANUAL_GRAB/**************/= false;
        public bool/*****/BOOL_USE_SAVE_FOCUS_REGION/*************/= false;
        public bool/*****/BOOL_USE_SAVE_SEQUENTIAL_IMAGE_SET/*****/= false;
        public bool/*****/BOOL_USE_SAVE_INPUT_IMAGE /*************/= false;
        public bool/*****/BOOL_USE_LEAVE_HISTORY_ERROR_PTRN /*****/= false;
        public bool/*****/BOOL_USE_LEAVE_HISTORY_MEASUREMENT/*****/= false;
        public string/***/PATH_EXPERIMENTAL_IMAGE_SET/************/= string.Empty;
        public int/******/INT_LOOP_COUNTER/***********************/= 1;

        public CAdvancedMode() { }

    }

    public class DEF_LAYERS
    {
        private int _type = 0; // 0 == ADI | 1 == ACI 
        private int _layer = 0;

        public int TYPE { get { return _type; } set { _type = value; } }
        public int LAYER { get { return _layer; } set { _layer = value; } }

        public DEF_LAYERS() { this.TYPE = TYPE_ACI; this.LAYER = LAYER_ACTIVE; }

        public void SetADI() { TYPE = TYPE_ADI; }
        public void SetACI() { TYPE = TYPE_ACI; }

        public void SetType(string strType) { TYPE = GetTypeToInt(strType); }
        public void SetLayer(string strLayer) { LAYER = GetTypeToInt(strLayer); }

        #region DEFINE TYPE & LAYER

        // For types
        public static int TYPE_ADI/*******/= 0;
        public static int TYPE_ACI/*******/= 1;
        // For Layers
        public static int LAYER_ACTIVE/***/= 0;
        public static int LAYER_GATE1/****/= 1;
        public static int LAYER_CNT1/*****/= 2;
        public static int LAYER_SDR/******/= 3;
        public static int LAYER_VIA/******/= 4;
        public static int LAYER_HPDL/*****/= 5;

        #endregion

        #region STRING FUCNTIONS

        public static string GetTypeString(int nType)
        {
            List<string> list = GetTypesToString();
            string strType = string.Empty;

            if/***/ (nType == TYPE_ADI) { strType = list.ElementAt(0); }
            else if (nType == TYPE_ACI) { strType = list.ElementAt(1); }
            return strType;
        }
        public static string GetLayerString(int nLayer)
        {
            List<string> list = GetLayersToString();
            string strLayer = string.Empty;

            if (nLayer <= list.Count) { strLayer = list.ElementAt(nLayer); }
            return strLayer;
        }

        public static List<string> GetTypesToString()
        {
            List<string> list = new List<string>();
            list.Add("ADI");
            list.Add("ACI");
            return list;
        }
        public static int GetTypeToInt(string strType)
        {
            List<string> list = GetTypesToString();
            return list.IndexOf(strType);
        }
        public static List<string> GetLayersToString()
        {
            List<string> list = new List<string>();
            list.Add("ACTIVE");
            list.Add("GATE1");
            list.Add("CNT1");
            list.Add("SDR");
            list.Add("VIA");
            list.Add("HPDL");
            return list;
        }
        public static int GetLayerToInt(string strLayer)
        {
            List<string> list = GetLayersToString();
            return list.IndexOf(strLayer);
        }
        #endregion
    }


    public class BASE_RECP : DEF_LAYERS
    {
        public string/***/PARAM_00_BASE_RECP_NAME { get; set; }

        public int/******/PARAM_01_LENS_INDEX { get; set; }
        public string/***/_param_01_lens_index
        {
            get
            {
                string lens = string.Empty;
                lens = PARAM_01_LENS_INDEX == 1 ? "ALIGN" : PARAM_01_LENS_INDEX == 2 ? "25X" : "50X";
                return lens;
            }
            set
            {
                /***/if (value == "ALIGN") PARAM_01_LENS_INDEX = 1;
                else if (value == "25X") PARAM_01_LENS_INDEX = 2;
                else if (value == "50X") PARAM_01_LENS_INDEX = 3;
            }
        }

        public int/******/PARAM_02_LIGHT_INDEX { get; set; }
        public string/***/_param_02_light_index
        {
            get
            {
                string light = string.Empty;
                light = PARAM_02_LIGHT_INDEX == 1 ? "ALIGN" : PARAM_02_LIGHT_INDEX == 2 ? "BF" : "DF";
                return light;
            }
            set
            {
                if/***/ (value == "ALIGN") PARAM_02_LIGHT_INDEX = 1;
                else if (value == "BF") PARAM_02_LIGHT_INDEX = 2;
                else if (value == "DF") PARAM_02_LIGHT_INDEX = 3;
            }
        }

        public int/******/PARAM_03_LIGHT_VALUE { get; set; }
        public int/******/PARAM_04_FOCUS_TYPE { get; set; }
        public int/******/PARAM_05_USE_CENTERING { get; set; }

        public BASE_RECP(){RemoveAll(); }

        public void RemoveAll()
        {
            PARAM_00_BASE_RECP_NAME/*****/= string.Empty;
            PARAM_01_LENS_INDEX/*********/= 0;
            PARAM_02_LIGHT_INDEX/********/= 0;
            PARAM_03_LIGHT_VALUE/********/= 0;
            PARAM_04_FOCUS_TYPE/*********/= 0;
            PARAM_05_USE_CENTERING/******/= 0;
        }

        public BASE_RECP CopyTo()
        {
            BASE_RECP single = new BASE_RECP();

            single.PARAM_00_BASE_RECP_NAME/****/= this.PARAM_00_BASE_RECP_NAME;
            single.PARAM_01_LENS_INDEX/********/= this.PARAM_01_LENS_INDEX;
            single.PARAM_02_LIGHT_INDEX/*******/= this.PARAM_02_LIGHT_INDEX;
            single.PARAM_03_LIGHT_VALUE/*******/= this.PARAM_03_LIGHT_VALUE;
            single.PARAM_04_FOCUS_TYPE/********/= this.PARAM_04_FOCUS_TYPE;
            single.PARAM_05_USE_CENTERING/*****/= this.PARAM_05_USE_CENTERING;

            return single;
        }

        public static string DEF_PARAM_00_BASE_RECP/*********/= "PARAM_00_BASE_RECP_NAME";
        public static string DEF_PARAM_01_LENS_INDEX/********/= "PARAM_01_LENS_INDEX";
        public static string DEF_PARAM_02_LIGHT_INDEX/*******/= "PARAM_02_LIGHT_INDEX";
        public static string DEF_PARAM_03_LIGHT_VALUE/*******/= "PARAM_03_LIGHT_VALUE";
        public static string DEF_PARAM_04_FOCUS_TYPE/********/= "PARAM_04_FOCUS_TYPE";
        public static string DEF_PARAM_05_USE_CENTERING/*****/= "PARAM_05_USE_CENTERING";

        public string [] GetComparedData(BASE_RECP target)
        {
            List<string> list_Diff = new List<string>();

            if (this.PARAM_00_BASE_RECP_NAME /***/!= target.PARAM_00_BASE_RECP_NAME)/***/ list_Diff.Add(string.Format("RECP NAME : {0} → {1}",/******/ this.PARAM_00_BASE_RECP_NAME,/***/ target.PARAM_00_BASE_RECP_NAME));
            if (this.PARAM_01_LENS_INDEX /*******/!= target.PARAM_01_LENS_INDEX)/*******/ list_Diff.Add(string.Format("LENS INDEX : {0} → {1}",/*****/ this.PARAM_01_LENS_INDEX,/*******/ target.PARAM_01_LENS_INDEX));
            if (this.PARAM_02_LIGHT_INDEX /******/!= target.PARAM_02_LIGHT_INDEX)/******/ list_Diff.Add(string.Format("LIGHT INDEX : {0} → {1}",/****/ this.PARAM_02_LIGHT_INDEX,/******/ target.PARAM_02_LIGHT_INDEX));
            if (this.PARAM_03_LIGHT_VALUE /******/!= target.PARAM_03_LIGHT_VALUE)/******/ list_Diff.Add(string.Format("LIGHT VALUE : {0} → {1}",/****/ this.PARAM_03_LIGHT_VALUE,/******/ target.PARAM_03_LIGHT_VALUE));
            if (this.PARAM_04_FOCUS_TYPE /*******/!= target.PARAM_04_FOCUS_TYPE)/*******/ list_Diff.Add(string.Format("FOCUS TYPE : {0} → {1}",/*****/ this.PARAM_04_FOCUS_TYPE,/*******/ target.PARAM_04_FOCUS_TYPE));
            if (this.PARAM_05_USE_CENTERING/*****/!= target.PARAM_05_USE_CENTERING)/****/ list_Diff.Add(string.Format("USE CENTERING : {0} → {1}",/**/ this.PARAM_05_USE_CENTERING,/****/ target.PARAM_05_USE_CENTERING));

            return list_Diff.ToArray();
        }
    }


    public class PARAM_PTRN
    {
        public string/*******/PTRN_FILE/**************/ = string.Empty;
        public bool/*********/BOOL_EDGE_BASED/********/= false;
        public bool/*********/BOOL_GLOBAL_SEARCHING/**/= true;
        public double/*******/ACC_RATIO/**************/= 55; // acceptance ratio for pattern matching 
        public RectangleF/***/RC_SEARCH_RGN/**********/= new RectangleF();
        public RectangleF/***/RC_TEMPLATE/************/= new RectangleF();

        public PARAM_PTRN(){}

        public PARAM_PTRN CopyTo()
        {
            PARAM_PTRN single = new PARAM_PTRN();

            single.PTRN_FILE/***************/= this.PTRN_FILE;
            single.BOOL_EDGE_BASED/*********/= this.BOOL_EDGE_BASED;
            single.BOOL_GLOBAL_SEARCHING/***/= this.BOOL_GLOBAL_SEARCHING;
            single.ACC_RATIO/***************/= this.ACC_RATIO;
            single.RC_SEARCH_RGN/***********/= this.RC_SEARCH_RGN;
            single.RC_TEMPLATE/*************/= this.RC_TEMPLATE;
            return single;
        }

    }

    public class PARAM_OPTICS
    {
        public static int CAM_ALIGN/*****/= 1;
        public static int CAM_25X/*******/= 2;
        public static int CAM_50X/*******/= 3;

        public static int LIGHT_ALIGN/***/= 1;
        public static int LIGHT_BF/******/= 2;
        public static int LIGHT_DF/******/= 3;

        public int /******/i01_CAM_INDEX { get; set; }
        public double /***/i02_PIXEL_RES { get; set; }
        public int /******/i03_LIGHT_INDEX { get; set; }
        public int /******/i04_LIGHT_VALUE { get; set; }
        public int /******/i05_EXPOSURE { get; set; }
        public int/*******/i06_MULTI_SHOT_COUNT { get; set; }
        public int/*******/i07_MULTI_SHOT_DELAY { get; set; }
        public bool/******/i08_MULTI_SHOT_VALUE_AVG { get; set; }
        public string/****/i09_MAKE_ZERO_FILE { get; set; }

        public double REAL_SCALE_PIXEL_RES { get { return i02_PIXEL_RES * 1000; } }

        public PARAM_OPTICS()
        {
            i01_CAM_INDEX/*************/= 0;
            i02_PIXEL_RES/*************/= 0.000069;
            i03_LIGHT_INDEX/***********/= 0;
            i04_LIGHT_VALUE/***********/= 0;
            i05_EXPOSURE/**************/= 100;
            i06_MULTI_SHOT_COUNT/******/= 1;
            i07_MULTI_SHOT_DELAY/******/= 100;
            i08_MULTI_SHOT_VALUE_AVG/**/= true;
            i09_MAKE_ZERO_FILE/********/= string.Empty;

        }

        public PARAM_OPTICS CopyTo()
        {
            PARAM_OPTICS single = new PARAM_OPTICS();

            single.i01_CAM_INDEX/**************/= this.i01_CAM_INDEX;
            single.i02_PIXEL_RES/**************/= this.i02_PIXEL_RES;
            single.i03_LIGHT_INDEX/************/= this.i03_LIGHT_INDEX;
            single.i04_LIGHT_VALUE/************/= this.i04_LIGHT_VALUE;
            single.i05_EXPOSURE/***************/= this.i05_EXPOSURE;
            single.i06_MULTI_SHOT_COUNT/*******/= this.i06_MULTI_SHOT_COUNT;
            single.i07_MULTI_SHOT_DELAY/*******/= this.i07_MULTI_SHOT_DELAY;
            single.i08_MULTI_SHOT_VALUE_AVG/***/= this.i08_MULTI_SHOT_VALUE_AVG;
            single.i09_MAKE_ZERO_FILE/*********/= this.i09_MAKE_ZERO_FILE;
            return single;
        }
    }

    public class PARAM_PROGRAM
    {
        public string i00_previous_recp = string.Empty;
    }
  
    public class PARAM_PATH
    {
        public string i01_PATH_MAIN/***********/= string.Empty;
        public string i02_PATH_DATA_DUMP/******/= string.Empty;
        public string i03_PATH_RECP_BASE/******/= string.Empty;
        public string i04_PATH_RECP_REAL/******/= string.Empty;
        public string i05_PATH_IMG_ORG/********/= string.Empty;
        public string i06_PATH_IMG_PTRN/*******/= string.Empty;
        public string i07_PATH_HIST_MEASURE/***/= string.Empty;
        public string i08_PATH_HIST_PTRN/******/= string.Empty;
        public string i09_PATH_INI/************/= string.Empty;
        public string i10_PATH_LOG/************/= string.Empty;

        public PARAM_PATH()
        {
            i01_PATH_MAIN/***********/= "C:\\CD_METER";
            i02_PATH_DATA_DUMP/******/= "C:\\CD_METER\\DUMP";
            i03_PATH_RECP_BASE/******/= "C:\\CD_METER\\RECP_BASE";
            i04_PATH_RECP_REAL/******/= "C:\\CD_METER\\RECP";
            i05_PATH_IMG_ORG/********/= "C:\\CD_METER\\IMAGE_INPUT";
            i06_PATH_IMG_PTRN/*******/= "C:\\CD_METER\\PTRN_TEACHING";
            i07_PATH_HIST_MEASURE/***/= "C:\\CD_METER\\HISTORY";
            i08_PATH_HIST_PTRN/******/= "C:\\CD_METER\\PTRN_ERR";
            i09_PATH_INI/************/= "C:\\CD_METER\\INI";
            i10_PATH_LOG/************/= "C:\\CD_METER\\LOG";
        }
        public PARAM_PATH CopyTo()
        {
            PARAM_PATH single = new PARAM_PATH();

            single.i01_PATH_MAIN/***********/= this.i01_PATH_MAIN;
            single.i02_PATH_DATA_DUMP/******/= this.i02_PATH_DATA_DUMP;
            single.i03_PATH_RECP_BASE/******/= this.i03_PATH_RECP_BASE;
            single.i04_PATH_RECP_REAL/******/= this.i04_PATH_RECP_REAL;
            single.i05_PATH_IMG_ORG/********/= this.i05_PATH_IMG_ORG;
            single.i06_PATH_IMG_PTRN/*******/= this.i06_PATH_IMG_PTRN;
            single.i07_PATH_HIST_MEASURE/***/= this.i07_PATH_HIST_MEASURE;
            single.i08_PATH_HIST_PTRN/******/= this.i08_PATH_HIST_PTRN;
            single.i09_PATH_INI/************/= this.i09_PATH_INI;
            single.i10_PATH_LOG/************/= this.i10_PATH_LOG;

            return single;
        }
    }

    public static class IFX_DIR
    {
        public static int DIR_LFT = 0;
        public static int DIR_TOP = 1;
        public static int DIR_RHT = 2;
        public static int DIR_BTM = 3;

        public static string ToStringType(int nType)
        {
            string strType = string.Empty;
            /***/if (nType == DIR_LFT) /***/ strType = "DIR_LFT";
            else if (nType == DIR_TOP) /***/ strType = "DIR_TOP";
            else if (nType == DIR_RHT) /***/ strType = "DIR_RHT";
            else if (nType == DIR_BTM) /***/ strType = "DIR_BTM";
            return strType;   
        }
    }
    
    public static class IFX_RECT_TYPE
    {
        public static int DIR_HOR = 0;
        public static int DIR_VER = 1;
        public static int DIR_DIA = 2;

        public static int ToNumericType(string strType)
        {
            int nReturn = 0;
            /***/if (strType == "DIR_HOR") { nReturn = DIR_HOR; }
            else if (strType == "DIR_VER") { nReturn = DIR_VER; }
            else if (strType == "DIR_DIA") { nReturn = DIR_DIA; }
            return nReturn;
        }
        public static string ToStringType(int nType)
        {
            string strType = string.Empty;

            /***/if (nType == DIR_HOR) strType = "DIR_HOR";
            else if (nType == DIR_VER) strType = "DIR_VER";
            else if (nType == DIR_DIA) strType = "DIR_DIA";
            return strType;
        }
    }
   
    public static class IFX_FIGURE
    {
        public static int PAIR_RCT/****/= 0;
        public static int PAIR_CIR/****/= 1;
        public static int PAIR_OVL/****/= 2;
        public static int RC_FOCUS/****/= 3;
        public static int MIXED_RC/****/= 4;
        public static int MIXED_CC/****/= 5;
        public static int MIXED_RCC/***/= 6;
        public static int TOTAL/*******/= 7;

        public static int ToNumericType(string strType)
        {
            int nReturn = 0;

            /***/if (strType == "PAIR_RCT") { nReturn = PAIR_RCT; }
            else if (strType == "PAIR_CIR") { nReturn = PAIR_CIR; }
            else if (strType == "PAIR_OVL") { nReturn = PAIR_OVL; }
            else if (strType == "RC_FOCUS") { nReturn = RC_FOCUS; }
            else if (strType == "MIXED_RC") { nReturn = MIXED_RC; }
            else if (strType == "MIXED_CC") { nReturn = MIXED_CC; }
            else if (strType == "MIXED_RCC") {nReturn = MIXED_RCC; }

            return nReturn;
        }
        public static string ToStringType(int nType)
        {
            string strType = string.Empty;

            /***/if (nType == PAIR_RCT) strType = "PAIR_RCT";
            else if (nType == PAIR_CIR) strType = "PAIR_CIR";
            else if (nType == PAIR_OVL) strType = "PAIR_OVL";
            else if (nType == RC_FOCUS) strType = "RC_FOCUS";
            else if (nType == MIXED_RC) strType = "MIXED_RC";
            else if (nType == MIXED_CC) strType = "MIXED_CC";
            else if (nType == MIXED_RCC) strType = "MIXED_RCC";

            return strType;
        }
        public static string[] ToStringArray()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < TOTAL; i++) { list.Add(ToStringType(i)); }
            return list.ToArray();
        }
        public static List<string> ToList()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < TOTAL; i++) { list.Add(ToStringType(i)); }
            return list;
        }
    }

    public static class IFX_ADJ_ACTION
    {
        public static int POS = 0;
        public static int GAP = 1;
        public static int SIZE = 2;
        public static int WHEEL = 3;
    }

    public static class IFX_METRIC
    {
        public static int P2P = 0;
        public static int HOR = 1;
        public static int VER = 2;

        public static string ToStringType(int nType)
        {
            string strType = string.Empty;
            if/***/ (nType == P2P) /***/ strType = "P2P";
            else if (nType == HOR) /***/ strType = "HOR";
            else if (nType == VER) /***/ strType = "VER";
            return strType;
        }
    }
       
    public static class IFX_ALGORITHM
    {
        public static int CARDIN/***/= 0;       // Cardinal direction
        public static int DIR_IN/***/= 1;      // Direction = To Inside Falling
        public static int DIR_EX/***/= 2;      // Direction = to outside Falling
        public static int TOTAL/****/= 3;

        public static int ToNumericType(string strType)
        {
            int nReturn = 0;
            if/***/ (strType == "DIR_IN") /***/nReturn = DIR_IN;
            else if (strType == "DIR_EX") /***/nReturn = DIR_EX;
            else if (strType == "CARDIN")/****/nReturn = CARDIN;
            return nReturn;
        }
        public static string ToStringType(int nType)
        {
            string strType = string.Empty;
            if/***/ (nType == DIR_IN) /***/ strType = "DIR_IN";
            else if (nType == DIR_EX) /***/ strType = "DIR_EX";
            else if (nType == CARDIN)/****/ strType = "CARDIN";
            return strType;
        }

        public static string[] ToStringArray()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < TOTAL; i++){list.Add(ToStringType(i));}
            return list.ToArray();
        }
        public static List<string> ToList()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < TOTAL; i++){list.Add(ToStringType(i));}
            return list;
        }
    }


}
