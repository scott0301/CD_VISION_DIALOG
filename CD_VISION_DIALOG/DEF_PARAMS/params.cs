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

    public class CHacker
    {
        public bool BOOL_SHOW_IMAGE_PROCESS { get; set; }
        public bool BOOL_SHOW_DETECT_CROSS { get; set; }
        public bool BOOL_USE_LOOP_COUNTER { get; set; }
        public bool BOOL_USE_GRAB_SAVE { get; set; }
        public bool BOOL_USE_FOCUS_SAVE { get; set; }

        public int INT_LOOP_COUNTER { get; set; }

        public CHacker()
        {
            BOOL_SHOW_IMAGE_PROCESS = false;
            BOOL_SHOW_DETECT_CROSS = true;
            BOOL_USE_LOOP_COUNTER = false;
            BOOL_USE_GRAB_SAVE = false;
            BOOL_USE_FOCUS_SAVE = false;
            INT_LOOP_COUNTER = 1;
        }

    }
    public class BASE_RECP : DEF_LAYERS
    {
        public string PARAM_00_BASE_RECP_NAME { get; set; }

        public int PARAM_01_LENS_INDEX { get; set; }
        public string _param_lens_index
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

        public int PARAM_02_LIGHT_INDEX { get; set; }
        public string _param_light_index
        {
            get
            {
                string light = string.Empty;
                light = PARAM_02_LIGHT_INDEX == 1 ? "ALIGN" : PARAM_02_LIGHT_INDEX == 2 ? "BF" : "DF";
                return light;
            }
            set
            {
                /***/if (value == "ALIGN") PARAM_02_LIGHT_INDEX = 1;
                else if (value == "BF") PARAM_02_LIGHT_INDEX = 2;
                else if (value == "DF") PARAM_02_LIGHT_INDEX = 3;
            }
        }

        public int PARAM_03_LIGHT_VALUE { get; set; }

        public int PARAM_04_FOCUS_TYPE { get; set; }
        public int PARAM_05_USE_CENTERING { get; set; }

        public double PARAM_06_COMPEN_A { get; set; }
        public double PARAM_06_COMPEN_B { get; set; }

        public double PARAM_07_ALGORITHM_INDEX { get; set; }
        public string _param_algoritm_index
        {
            get
            {
                string algorithm = string.Empty;
                /***/if (PARAM_07_ALGORITHM_INDEX == IFX_ALGORITHM.MEXHAT) { algorithm = "MAXHAT"; }
                else if (PARAM_07_ALGORITHM_INDEX == IFX_ALGORITHM.DIR_IN) { algorithm = "DIR_IN"; }
                else if (PARAM_07_ALGORITHM_INDEX == IFX_ALGORITHM.DIR_EX) { algorithm = "DIR_EX"; }
                else if (PARAM_07_ALGORITHM_INDEX == IFX_ALGORITHM.CARDIN) { algorithm = "CARDIN"; }
                return algorithm;
            }
            set
            {
                /***/if (value == "MAXHAT") PARAM_07_ALGORITHM_INDEX = IFX_ALGORITHM.MEXHAT;
                else if (value == "DIR_IN") PARAM_07_ALGORITHM_INDEX = IFX_ALGORITHM.DIR_IN;
                else if (value == "DIR_EX") PARAM_07_ALGORITHM_INDEX = IFX_ALGORITHM.DIR_EX;
                else if (value == "CARDIN") PARAM_07_ALGORITHM_INDEX = IFX_ALGORITHM.CARDIN;
            }
        }

        public double PARAM_08_DMG_TOLERANCE { get; set; }
        public double PARAM_09_EDGE_POSITION { get; set; }

        public BASE_RECP()
        {
            RemoveAll(); 
        }

        public void RemoveAll()
        {
            PARAM_00_BASE_RECP_NAME = string.Empty;
            PARAM_01_LENS_INDEX = 0;
            PARAM_02_LIGHT_INDEX = 0;
            PARAM_03_LIGHT_VALUE = 0;
            PARAM_04_FOCUS_TYPE = 0;
            PARAM_05_USE_CENTERING = 0;
            PARAM_06_COMPEN_A = 0;
            PARAM_06_COMPEN_B = 0;
            PARAM_07_ALGORITHM_INDEX = 0;
            PARAM_08_DMG_TOLERANCE = 0;
            PARAM_09_EDGE_POSITION = 0;
        }

        public BASE_RECP CopyTo()
        {
            BASE_RECP single = new BASE_RECP();

            single.PARAM_00_BASE_RECP_NAME = this.PARAM_00_BASE_RECP_NAME;
            single.PARAM_01_LENS_INDEX = this.PARAM_01_LENS_INDEX;
            single.PARAM_02_LIGHT_INDEX = this.PARAM_02_LIGHT_INDEX;
            single.PARAM_03_LIGHT_VALUE = this.PARAM_03_LIGHT_VALUE;
            single.PARAM_04_FOCUS_TYPE = this.PARAM_04_FOCUS_TYPE;
            single.PARAM_05_USE_CENTERING = this.PARAM_05_USE_CENTERING;
            single.PARAM_06_COMPEN_A = this.PARAM_06_COMPEN_A;
            single.PARAM_06_COMPEN_B = this.PARAM_06_COMPEN_B;
            single.PARAM_07_ALGORITHM_INDEX = this.PARAM_07_ALGORITHM_INDEX;
            single.PARAM_08_DMG_TOLERANCE = this.PARAM_08_DMG_TOLERANCE;
            single.PARAM_09_EDGE_POSITION = this.PARAM_09_EDGE_POSITION;

            return single;
        }

        public static string DEF_PARAM_00_BASE_RECP = "PARAM_00_BASE_RECP_NAME";
        public static string DEF_PARAM_01_LENS_INDEX = "PARAM_01_LENS_INDEX";
        public static string DEF_PARAM_02_LIGHT_INDEX = "PARAM_02_LIGHT_INDEX";
        public static string DEF_PARAM_03_LIGHT_VALUE = "PARAM_03_LIGHT_VALUE";
        public static string DEF_PARAM_04_FOCUS_TYPE = "PARAM_04_FOCUS_TYPE";
        public static string DEF_PARAM_05_USE_CENTERING = "PARAM_05_USE_CENTERING";
        public static string DEF_PARAM_06_COMPEN_A = "PARAM_06_COMPEN_A";
        public static string DEF_PARAM_06_COMPEN_B = "PARAM_06_COMPEN_B";
        public static string DEF_PARAM_07_ALGORITHM_INDEX = "PARAM_07_ALGORITHM_INDEX";
        public static string DEF_PARAM_08_DMG_TOLERANCE = "PARAM_08_DMG_TOLERANCE";
        public static string DEF_PARAM_09_EDGE_POSITION = "PARAM_09_EDGE_POSITION";

        public string [] GetComparedData(BASE_RECP target)
        {
            List<string> list_Diff = new List<string>();

            if (this.PARAM_00_BASE_RECP_NAME /***/!= target.PARAM_00_BASE_RECP_NAME)/***/ list_Diff.Add(string.Format("RECP NAME : {0} → {1}",/******/ this.PARAM_00_BASE_RECP_NAME,/***/ target.PARAM_00_BASE_RECP_NAME));
            if (this.PARAM_01_LENS_INDEX /*******/!= target.PARAM_01_LENS_INDEX)/*******/ list_Diff.Add(string.Format("LENS INDEX : {0} → {1}",/*****/ this.PARAM_01_LENS_INDEX,/*******/ target.PARAM_01_LENS_INDEX));
            if (this.PARAM_02_LIGHT_INDEX /******/!= target.PARAM_02_LIGHT_INDEX)/******/ list_Diff.Add(string.Format("LIGHT INDEX : {0} → {1}",/****/ this.PARAM_02_LIGHT_INDEX,/******/ target.PARAM_02_LIGHT_INDEX));
            if (this.PARAM_03_LIGHT_VALUE /******/!= target.PARAM_03_LIGHT_VALUE)/******/ list_Diff.Add(string.Format("LIGHT VALUE : {0} → {1}",/****/ this.PARAM_03_LIGHT_VALUE,/******/ target.PARAM_03_LIGHT_VALUE));
            if (this.PARAM_04_FOCUS_TYPE /*******/!= target.PARAM_04_FOCUS_TYPE)/*******/ list_Diff.Add(string.Format("FOCUS TYPE : {0} → {1}",/*****/ this.PARAM_04_FOCUS_TYPE,/*******/ target.PARAM_04_FOCUS_TYPE));
            if (this.PARAM_05_USE_CENTERING/*****/!= target.PARAM_05_USE_CENTERING)/****/ list_Diff.Add(string.Format("USE CENTERING : {0} → {1}",/**/ this.PARAM_05_USE_CENTERING,/****/ target.PARAM_05_USE_CENTERING));
            if (this.PARAM_06_COMPEN_A /*********/!= target.PARAM_06_COMPEN_A)/*********/ list_Diff.Add(string.Format("COMPEN A : {0} → {1}",/*******/ this.PARAM_06_COMPEN_A,/*********/ target.PARAM_06_COMPEN_A));
            if (this.PARAM_06_COMPEN_B /*********/!= target.PARAM_06_COMPEN_B)/*********/ list_Diff.Add(string.Format("COMPEN B : {0} → {1}",/*******/ this.PARAM_06_COMPEN_B,/*********/ target.PARAM_06_COMPEN_B));
            if (this.PARAM_07_ALGORITHM_INDEX /**/!= target.PARAM_07_ALGORITHM_INDEX)/**/ list_Diff.Add(string.Format("ALGORITHM : {0} → {1}",/******/ this.PARAM_07_ALGORITHM_INDEX,/**/ target.PARAM_07_ALGORITHM_INDEX));
            if (this.PARAM_08_DMG_TOLERANCE /****/!= target.PARAM_08_DMG_TOLERANCE)/****/ list_Diff.Add(string.Format("DMG TOL(%) : {0} → {1}",/*****/ this.PARAM_08_DMG_TOLERANCE,/****/ target.PARAM_08_DMG_TOLERANCE));
            if (this.PARAM_09_EDGE_POSITION /****/!= target.PARAM_09_EDGE_POSITION)/****/ list_Diff.Add(string.Format("Edge Pos(%) : {0} → {1}",/****/ this.PARAM_09_EDGE_POSITION,/****/ target.PARAM_09_EDGE_POSITION)); 

            return list_Diff.ToArray();
        }
    }

    public class DEF_LAYERS
    {
        private int _type = 0; // 0 == ADI | 1 == ACI 
        private int _layer = 0; 

        public int TYPE { get { return _type; } set { _type = value; } }
        public int LAYER { get { return _layer; } set { _layer = value; } }

        public DEF_LAYERS() { this.TYPE = TYPE_ACI;  this.LAYER = LAYER_ACTIVE; }

        public void SetADI(){TYPE = TYPE_ADI;}
        public void SetACI(){TYPE = TYPE_ACI;}

        public void SetType(string strType) { TYPE = GetTypeToInt(strType); }
        public void SetLayer(string strLayer) { LAYER = GetTypeToInt(strLayer); }
        
        #region DEFINE TYPE & LAYER 

        public static int TYPE_ADI = 0; 
        public static int TYPE_ACI = 1;

        public static int LAYER_ACTIVE = 0;
        public static int LAYER_GATE1  = 1;
        public static int LAYER_CNT1 = 2;
        public static int LAYER_SDR = 3;
        public static int LAYER_VIA = 4;
        public static int LAYER_HPDL = 5;

        #endregion  

        #region STRING FUCNTIONS 

        public static string GetTypeString(int nType)
        {
            List<string> list = GetTypesToString();
            string strType = string.Empty;

            /***/if (nType == TYPE_ADI) { strType = list.ElementAt(0); }
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

    public class PARAM_PTRN
    {
        public string PTRN_FILE { get; set; }
        public bool BOOL_EDGE_BASED { get; set; }
        public bool BOOL_GLOBAL_SEARCHING { get; set; }
        public double ACC_RATIO { get; set; }
        public RectangleF RC_SEARCH_RGN { get; set; }
        public RectangleF RC_TEMPLATE { get; set; }

        public PARAM_PTRN()
        {
            PTRN_FILE = string.Empty;
            BOOL_EDGE_BASED = false;
            BOOL_GLOBAL_SEARCHING = true;
            ACC_RATIO = 55;
            RC_SEARCH_RGN = new RectangleF();
            RC_TEMPLATE = new RectangleF();
        }

    }
    public class PARAM_OPTICS
    {
        public static int CAM_ALIGN = 1;
        public static int CAM_25X = 2;
        public static int CAM_50X = 3;

        public static int LIGHT_ALIGN = 1;
        public static int LIGHT_BF = 2;
        public static int LIGHT_DF = 3;


        public int /******/CAM_INDEX { get; set; }
        public double /***/PIXEL_RES { get; set; }
        public int /******/LIGHT_INDEX { get; set; }
        public int /******/LIGHT_VALUE { get; set; }
    
        public PARAM_OPTICS()
        {
            CAM_INDEX = 0;
            PIXEL_RES = 0.000069;
            LIGHT_INDEX = LIGHT_VALUE = 0;
        }

        public PARAM_OPTICS CopyTo()
        {
            PARAM_OPTICS single = new PARAM_OPTICS();

            single.CAM_INDEX = this.CAM_INDEX;
            single.LIGHT_INDEX = this.LIGHT_INDEX;
            single.LIGHT_VALUE = this.LIGHT_VALUE;
            single.PIXEL_RES = this.PIXEL_RES;

            return single;
        }
    }

    public class PARAM_PRE_PROCESS
    {
        public int nBrightness { get; set; }
        public int nContrast { get; set; }
        public bool USE_CTRL_BR { get; set; }
        public bool USE_CTRL_CONT { get; set; }

        public PARAM_PRE_PROCESS()
        {
            nBrightness = 0;
            nContrast = 0;
            USE_CTRL_BR = false;
            USE_CTRL_CONT = false;
        }
    }

    public class PARAM_CONFIG
    {
        public string i01_PATH_MAIN { get; set; }
        public string i02_PATH_DATA_DUMP { get; set; }
        public string i03_PATH_RECP_BASE { get; set; }
        public string i04_PATH_RECP_REAL { get; set; }

        public string i10_PATH_IMG_ORG { get; set; }
        public string i11_PATH_IMG_PTRN { get; set; }

        public string i15_PATH_HIST_MEASURE { get; set; }
        public string i16_PATH_HIST_PTRN { get; set; }

        public string i20_PATH_INI { get; set; }
        public string i21_PATH_LOG { get; set; }

        public PARAM_CONFIG()
        {
            i01_PATH_MAIN = "C:\\CD_METER";
            i02_PATH_DATA_DUMP = "C:\\CD_METER\\DUMP";
            i03_PATH_RECP_BASE = "C:\\CD_METER\\RECP_BASE";
            i04_PATH_RECP_REAL = "C:\\CD_METER\\RECP";

            i10_PATH_IMG_ORG = "C:\\CD_METER\\IMAGE_INPUT";
            i11_PATH_IMG_PTRN = "C:\\CD_METER\\PTRN_TEACHING";

            i15_PATH_HIST_MEASURE = "C:\\CD_METER\\HISTORY";
            i16_PATH_HIST_PTRN = "C:\\CD_METER\\PTRN_ERR";

            i20_PATH_INI = "C:\\CD_METER\\INI";
            i21_PATH_LOG = "C:\\CD_METER\\LOG";
        }
        public PARAM_CONFIG CopyTo()
        {
            PARAM_CONFIG single = new PARAM_CONFIG();

            single.i01_PATH_MAIN = this.i01_PATH_MAIN;
            single.i02_PATH_DATA_DUMP = this.i02_PATH_DATA_DUMP;
            single.i03_PATH_RECP_BASE = this.i03_PATH_RECP_BASE;
            single.i04_PATH_RECP_REAL = this.i04_PATH_RECP_REAL;

            single.i10_PATH_IMG_ORG = this.i10_PATH_IMG_ORG;
            single.i11_PATH_IMG_PTRN = this.i11_PATH_IMG_PTRN;

            single.i15_PATH_HIST_MEASURE = this.i15_PATH_HIST_MEASURE;
            single.i16_PATH_HIST_PTRN = this.i16_PATH_HIST_PTRN;

            single.i20_PATH_INI = this.i20_PATH_INI;
            single.i21_PATH_LOG = this.i21_PATH_LOG;

            return single;
        }


        
    }


    public static class IFX_DIR
    {
        public static int DIR_LFT = 0;
        public static int DIR_TOP = 1;
        public static int DIR_RHT = 2;
        public static int DIR_BTM = 3;
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
        public static int PAIR_RCT = 0;
        public static int PAIR_CIR = 1;
        public static int PAIR_OVL = 2;
        public static int TOTAL = 3;

        public static int ToNumericType(string strType)
        {
            int nReturn = 0;

            /***/if (strType == "PAIR_RCT") { nReturn = PAIR_RCT; }
            else if (strType == "PAIR_CIR") { nReturn = PAIR_CIR; }
            else if (strType == "PAIR_OVL") { nReturn = PAIR_OVL; }
            return nReturn;
        }
        public static string ToStringType(int nType)
        {
            string strType = string.Empty;

            /***/if (nType == PAIR_RCT) strType = "PAIR_RCT";
            else if (nType == PAIR_CIR) strType = "PAIR_CIR";
            else if (nType == PAIR_OVL) strType = "PAIR_OVL";
            return strType;
        }
        public static string[] ToStringArray()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < 2; i++)
            {
                list.Add(ToStringType(i));
            }
            return list.ToArray();
        }
        public static List<string> ToList()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                list.Add(ToStringType(i));
            }
            return list;
        }
    }

    public static class IFX_ADJ_ACTION
    {
        public static int POS = 0;
        public static int GAP = 1;
        public static int SIZE = 2;
        public static int ASYM = 3;
    }

   
    
    public static class IFX_ALGORITHM
    {
        public static int MEXHAT = 0;
        public static int DIR_IN = 1;      // Direction = To Inside Falling
        public static int DIR_EX = 2;      // Direction = to outside Falling
        public static int CARDIN = 3;   // overlay method
        public static int TOTAL/***/ = 4;

        public static int ToNumericType(string strType)
        {
            int nReturn = 0;
            /***/if (strType == "MEXHAT") /***/nReturn = MEXHAT;
            else if (strType == "DIR_IN") /***/nReturn = DIR_IN;
            else if (strType == "DIR_EX") /***/nReturn = DIR_EX;
            else if (strType == "CARDIN")/****/nReturn = CARDIN;
            return nReturn;
        }
        public static string ToStringType(int nType)
        {
            string strType = string.Empty;
            /***/if (nType == MEXHAT) /***/ strType = "MEXHAT";
            else if (nType == DIR_IN) /***/ strType = "DIR_IN";
            else if (nType == DIR_EX) /***/ strType = "DIR_EX";
            else if (nType == CARDIN)/****/ strType = "CARDIN";
            return strType;
        }

        public static string[] ToStringArray()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < TOTAL; i++)
            {
                list.Add(ToStringType(i));
            }
            return list.ToArray();
        }
        public static List<string> ToList()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < TOTAL; i++)
            {
                list.Add(ToStringType(i));
            }
            return list;
        }
    }


}
