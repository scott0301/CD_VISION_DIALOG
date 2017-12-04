using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Remote
{
    public class IPC_ID
    {
        public const int AC_IMAGE_GRAB_REQ = 101;
        public const int CA_IMAGE_GRAB_REP = 102;
        public const int CA_MACRO_LIST_REQ = 201;
        public const int AC_MACRO_LIST_REP = 202;
        public const int CA_MACRO_LOAD_REQ = 301;
        public const int AC_MACRO_LOAD_REP = 302;
        public const int CA_IMAGE_MATCHING_REQ = 401;
        public const int AC_IMAGE_MATCHING_REP = 402;


        public const int MC_IMAGE_GRAB_REQ = 501;
        public const int CM_IMAGE_GRAB_REP = 502;
        
        //MEASURE START - MESSAGE => "CYCLE,POINTS"
        public const int CM_MEASURE_START = 503;
        //MEASURE END - MESSAGE => ""
        public const int CM_MEASURE_END = 504;

        public const int CM_MACRO_LIST_REQ = 601;
        public const int MC_MACRO_LIST_REP = 602;
        public const int CM_MACRO_LOAD_REQ = 701;
        public const int MC_MACRO_LOAD_REP = 702;
        public const int CM_IMAGE_MATCHING_REQ = 801;
        public const int MC_IMAGE_MATCHING_REP = 802;
        public const int CM_IMAGE_MEASURE_REQ = 901;
        public const int MC_IMAGE_MEASURE_REP = 902;
    }
    
    public class IpcBuffer : MarshalByRefObject
    {
        private static byte[] buffer = null;
        public byte[] Buffer { get { return buffer; } set { buffer = value; } }
    }

    [Serializable]
    public class FocusInfo
    {
        //FocusType : 0-> Focus Non, 1 -> ZAF, 2 -> LAF, 4 -> IAF 
        public int FocusType = 0;
        // rect, unit pixel -> matching 기준 x,y(left,top), width, height 
        public double X = 0;
        public double Y = 0;
        public double W = 0;
        public double H = 0;
    }


    [Serializable]
    public class PosInfo
    {
        //No -> 1,2,3,4,5....
        public int No;
        public double X;
        public double Y;
        public PosInfo(int no, double x, double y)
        {
            No = no;
            X = x;
            Y = y;
        }
    }


    [Serializable]
    public class IllInfo
    {
        public int IllNo;
        public int IllValue;
        public IllInfo(int no, int value)
        {
            IllNo = no;
            IllValue = value;
        }
    }
    [Serializable]
    public class CamInfo
    {
        public int CamNo;
        public double PixelRes;
        public long Exposure;
        public long Contrast;
        public long Brightness;


        public CamInfo(int camNo, double res, long exp, long cont, long bright)
        {
            CamNo = camNo;
            PixelRes = res;
            Exposure = exp;
            Contrast = cont;
            Brightness = bright;

        }
    }

    [Serializable]
    public class ImageInfo
    {
        public int Width; /* The width of the image. */
        public int Height; /* The height of the image. */
        public Byte[] Buffer; /* The raw image data. */
        
        public ImageInfo(byte[] img, int w, int h)
        {
            Width = w;
            Height = h;
            Buffer = new Byte[w*h];
            
            img.CopyTo(Buffer, 0);
        }
    }

    [Serializable]
    public class GrabInfo
    {
        public PosInfo POS = null;
        public IllInfo ILL = null;
        public CamInfo CAM = null;
        public ImageInfo IMG = null;
    }


    [Serializable]
    public class RecipeList
    {
        public List<string> Recipes = null;
    }

    /// <summary>
    /// CD OL 
    /// </summary>
    [Serializable]
    public class RecipeInfo
    {
        //macro, ret, camNo, camRes, illNo, illValue
        public int Result = 0;
        public string Macro = "";
        public int CamNo = 0;
        public double CamRes = 0.0;
        public int IllNo = 0;
        public int IllValue = 0;
        public long Exposure = 100; 
        public int IsCentering = 0; // on-> 1,off -> 0
        public FocusInfo FI = null;
    }
    
    [Serializable]
    public class MatcingInfo
    {
        //macro,errno,matching xy,register matching xy, hit ratio(score)
        public int Result = 0;
        public string Macro = "";
        public double X = 0.0;
        public double Y = 0.0;
        public double RegX = 0.0;
        public double RegY = 0.0;
        public double Score = 0.0;
    }

    [Serializable]
    public class RES_DATA
    {
        public int type = 0;

        public double dist = 0; // distance From P1 to P2  ( Width | Height | Diameter )

        public PointF P1 = new PointF(); // measure point a ( image croodinate)
        public PointF P2 = new PointF(); // measure point b ( image croodinate)

        public PointF OVL = new PointF();
    }

    public class StaticResult
    {
        public int m_nCycleTarget { get; set; }
        public int m_nCycleCurrent { get; set; }
        public int m_nPointTarget { get; set; }
        public int m_nSequentialIndex { get; set; }

        public List<RES_DATA[,]> mapData = new List<RES_DATA[,]>();

        public void StatisticResult()
        {
            ClearMap();
        }

        public void SetInit( int nDataCount)
        {
            for (int nData = 0; nData < nDataCount; nData++)
            {
                RES_DATA[,] singlemap = new RES_DATA[m_nPointTarget,m_nCycleTarget];
                mapData.Add(singlemap);
            }
         }
        public bool InsertData(int nPointCurrent, int dataindex, RES_DATA res, out string strResult)
        {
            strResult = string.Empty;

          

            try
            {
                RES_DATA[,] single = mapData[dataindex];

                int nPoint = nPointCurrent - 1;
                int nCycle = m_nCycleCurrent;

                if( nPoint > this.m_nPointTarget || 
                    nCycle > this.m_nCycleTarget)
                {
                    strResult = string.Format("SEQ-TWISTED. CYCLE{0:00}-POINT{1:00}", nPoint, nCycle);
                    return false;
                }
                else
                {
                    single[nPoint, nCycle] = res;
                }

               
               
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public void ClearMap()
        {
            this.m_nCycleTarget = 0;
            this.m_nPointTarget = 0;
            this.m_nSequentialIndex = 0;
            this.m_nCycleCurrent = 0;

            if (mapData != null)
            {
                mapData.Clear();
            }
            
        }

    }
    [Serializable]
    public class MeasureInfo
    {
        //No -> 1,2,3,4,5...., PosInfo에서 받은 그대로 .. 
        public int Result = 0;
        public int No = 0;
        public string Macro = "";
        public double Focus = 0;
        public List<RES_DATA> list = new List<RES_DATA>();
    }
}
