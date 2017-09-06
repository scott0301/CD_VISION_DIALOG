using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace UC_Graph
{
  

    public partial class UC_GRAPH: UserControl
    {
        Bitmap bmp = null;
        bool m_bDrawReal = false;

        byte[] rawImage = null;
        int imageW = 0;
        int imageH = 0;
        


        int[] arrGraphData = new int[256];

        int static_histoMax = 0;
        int static_histoMin = 0;
        int static_histoDiff = 0;
        int static_histoAvg = 0;

        int BOUND_MARGIN_T = 10;
        int BOUND_MARGIN_L = 10;
        int BOUND_MARGIN_B = 10;
        int BOUND_MARGIN_R = 10;

        int MARK_BAR_LENGTH = 5;

        Point ptBound_LT = new Point(0,0);
        Point ptBound_RT = new Point(0,0);
        Point ptBound_RB = new Point(0,0);
        Point ptBound_LB = new Point(0, 0);

        bool bDrawBarGraph = true;

        Color colorBound = Color.White;
        Color colorGraph = Color.DeepSkyBlue;

        
        public UC_GRAPH()
        {
            InitializeComponent();

            // this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);      
        }

        public void ResizeWindow(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Refresh();
        }

        public void SetDrawTypeAsBar(bool bDraw)
        {
            bDrawBarGraph = bDraw;
            this.Refresh();
        }
        

        public int ANALYSIS_MAX{get { return static_histoMax; }}
        public int ANALYSIS_MIN { get { return static_histoMin; } }
        public int ANALYSIS_DIFF { get { return static_histoDiff; } }
        public int ANNALYSIS_AVG { get { return static_histoAvg; } }

        public Color COLOR_BOUND { get { return colorBound; } set { colorBound = value; } }
        public Color COLOR_GRAPH { get { return colorGraph; } set { colorGraph = value; } }

        public void SetImage(int nAnalysisType, byte [] rawImage, int imageW, int imageH )
        {
            // new allocation
            this.rawImage = new byte[imageW * imageH];
            
            Array.Copy(rawImage, this.rawImage, imageW * imageH);
            
            // update size info
            this.imageW = imageW;
            this.imageH = imageH;

            if (nAnalysisType == IFX_GRAPH_TYPE.REAL)
            {
                m_bDrawReal = true;
                this.rawImage = EnsureHorizontalImage(rawImage, ref this.imageW, ref this.imageH);
            }
            else
            {
                m_bDrawReal = false;
            }

            Analysis(nAnalysisType);

            this.Refresh(); 
        }

        private void DrawBoundary()
        {            
            Graphics g = Graphics.FromHwnd(this.Handle);

            ptBound_LT = new Point(BOUND_MARGIN_L, BOUND_MARGIN_T);
            ptBound_RT = new Point(Width - BOUND_MARGIN_R, BOUND_MARGIN_T);
            ptBound_RB = new Point(Width - BOUND_MARGIN_R, Height - BOUND_MARGIN_B);
            ptBound_LB = new Point(BOUND_MARGIN_L, Height - BOUND_MARGIN_B);

            Pen penBound = new Pen(COLOR_BOUND);

            g.DrawLine(penBound, ptBound_LT, ptBound_RT);
            g.DrawLine(penBound, ptBound_RT, ptBound_RB);
            g.DrawLine(penBound, ptBound_RB, ptBound_LB);
            g.DrawLine(penBound, ptBound_LT, ptBound_LB);
            
            Point pv1 = new Point();
            Point pv2 = new Point();
            Point ph1 = new Point();
            Point ph2 = new Point();

            int iDivision_V = 10;
            int iDivision_H = 10;

            int iVertSpace = Convert.ToInt32((Height - (BOUND_MARGIN_T + BOUND_MARGIN_B)) / (double)iDivision_V);
            int iHorizSpace = Convert.ToInt32((Width - (BOUND_MARGIN_L + BOUND_MARGIN_R)) / (double)iDivision_H);

            // Tick marks for the vertical axis
            for (int i = 0; i < iDivision_V; i++)
            {
                pv1.X = MARK_BAR_LENGTH;
                pv1.Y = BOUND_MARGIN_T + i * iVertSpace;
                pv2.X = BOUND_MARGIN_T;
                pv2.Y = pv1.Y;
                g.DrawLine(penBound, pv1, pv2);
            }

            // Draw the last tick mark for vertical axis
            pv1.Y = Height - BOUND_MARGIN_T;
            pv2.Y = pv1.Y;
            g.DrawLine(penBound, pv1, pv2);

            // Tick marks for the horizontal axis
            for (int i = 0; i < iDivision_H; i++)
            {
                ph1.X = BOUND_MARGIN_T + i * iHorizSpace;
                ph1.Y = Height - BOUND_MARGIN_T;
                ph2.X = ph1.X;
                ph2.Y = Height - MARK_BAR_LENGTH;
                g.DrawLine(penBound, ph1, ph2);
            }
            // Draw the last tick mark for horizontal axis
            ph1.X = Width - BOUND_MARGIN_T;
            ph2.X = ph1.X;
            g.DrawLine(penBound, ph1, ph2);

            penBound.Dispose();
        }

        private void DrawGraph()
        {
            // Find out the horizontal scale
            int xLength = this.Width - 2 * BOUND_MARGIN_T;

            // Find out what the vertical scale is
            int yLength = this.Height - 2 * BOUND_MARGIN_T;

            List<Point> listPoint = new List<Point>();

            // Draw the different lines comprising the histogram
            int i;

            Pen penGraph = new Pen(COLOR_GRAPH);
            Graphics g = Graphics.FromHwnd(this.Handle);

            static_histoMax = static_histoMax == 0 ? 1 : static_histoMax;
            
            // Demonstrates two different ways of drawing the histogram
           for (i = 0; i < arrGraphData.Length; ++i)
           {
               int x1 = Convert.ToInt32(xLength * i / (float)arrGraphData.Length) + BOUND_MARGIN_T;
               int y1 = Height - BOUND_MARGIN_T;

               int x2 = x1;
               int y2 = y1 - Convert.ToInt32(arrGraphData[i] * yLength / static_histoMax);

               if (bDrawBarGraph == true)
               {
                   g.DrawLine(penGraph, new Point(x1, y1),new Point(x2,y2));
               }
               listPoint.Add(new Point(x2,y2));               
           }

           if (bDrawBarGraph == false)
           {
               for (int j = 0; j < listPoint.Count; j++)
               {
                   if ((j == 0) || (j == listPoint.Count - 1))
                       g.DrawLine(penGraph, listPoint[j], listPoint[j]);
                   else
                       g.DrawLine(penGraph, listPoint[j], listPoint[j + 1]);
               }
           }
            penGraph.Dispose();
        }

        private void Analysis(int nAnalysisType)
        {
            if (rawImage != null)
            {
                // initialization
                static_histoMin = static_histoMax = static_histoDiff = static_histoAvg = 0;
                Array.Clear(arrGraphData, 0, arrGraphData.Length);

                if (nAnalysisType == IFX_GRAPH_TYPE.HISTO)
                {
                    Array.Resize(ref arrGraphData, 256);
                    
                    //for (int i = 0; i < rawImage.Length; ++i)
                    Parallel.For(0, rawImage.Length, i =>
                    {
                        int iVal = (byte)(rawImage[i]);
                        arrGraphData[iVal]++;
                    });

                    static_histoMax = arrGraphData.Max();
                    static_histoMin = arrGraphData.Min();
                    static_histoDiff = static_histoMax - static_histoMin;
                    int sum = arrGraphData.Sum();
                    static_histoAvg = sum / arrGraphData.Length;
                }
                else if (nAnalysisType ==IFX_GRAPH_TYPE.PROJ_H)
                {
                    Array.Resize(ref arrGraphData, imageH);
                    
                    //for (int y = 0; y < imageH; y++)
                    Parallel.For(0, imageH, y =>
                    {
                        int nSum = 0;

                        for (int x = 0; x < imageW; x++)
                        {
                            nSum += rawImage[y * imageW + x];
                        }

                        arrGraphData[y] = nSum;
                        static_histoMax = arrGraphData.Max();
                    });
                }
                else if (nAnalysisType == IFX_GRAPH_TYPE.PROJ_V)
                {
                    Array.Resize(ref arrGraphData, imageW);

                    //for (int x = 0; x < imageW; x++)
                    Parallel.For(0, imageW, x =>
                    {
                        int nSum = 0;

                        for (int y = 0; y < imageH; y++)
                        {
                            nSum += rawImage[y * imageW + x];
                        }

                        arrGraphData[x] = nSum;
                        static_histoMax = arrGraphData.Max();
                    });
                }
                else if (nAnalysisType == IFX_GRAPH_TYPE.REAL)
                {
                    bmp = HC_CONV_Byte2Bmp(rawImage, imageW, imageH);
                }
            }
        }

        public static byte[]  EnsureHorizontalImage(byte[] rawImage, ref int imageW, ref int imageH)
        {
            byte[] convImage = new byte[imageW * imageH];

            if (imageW < imageH)
            {
                for (int x = imageW - 1; x >= 0; x--)
                {
                    for (int y = 0; y < imageH; y++)
                    {
                        convImage[((imageW - 1) - x) * imageH + y] = rawImage[y * imageW + x];
                    }
                }
                int tempW = imageW;
                int tempH = imageH;

                imageW = tempH;
                imageH = tempW;
            }
            else
            {
                Buffer.BlockCopy(rawImage, 0, convImage, 0, rawImage.Length);
            }
            return convImage;
        }
        public static Bitmap/*******/HC_CONV_Byte2Bmp(byte[] rawImage, int imageW, int imageH)
        {
            if (imageW == 0 || imageH == 0)
            {
                return new Bitmap(444, 444, PixelFormat.Format24bppRgb);
            }

            Bitmap bmpImage = new Bitmap(imageW, imageH, PixelFormat.Format24bppRgb);

            int nStride = 0, bmpLength = 0;
            byte[] rawBmp = null;

            BitmapData bitmapData = bmpImage.LockBits(new Rectangle(0, 0, imageW, imageH), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            {
                nStride = Math.Abs(bitmapData.Stride);
                bmpLength = nStride * imageH;

            }
            bmpImage.UnlockBits(bitmapData);


            rawBmp = new byte[bmpLength];

            Parallel.For(0, imageH, y =>
            {
                for (int x = 0; x < imageW; x++)
                {
                    //rawBmp[(y * nStride) + x ] = rawImage[y * imageW + x];
                    rawBmp[(y * nStride) + (x * 3) + 0] =
                    rawBmp[(y * nStride) + (x * 3) + 1] =
                    rawBmp[(y * nStride) + (x * 3) + 2] = rawImage[y * imageW + x];
                }
            });


            bitmapData = bmpImage.LockBits(new Rectangle(0, 0, imageW, imageH), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            {
                System.Runtime.InteropServices.Marshal.Copy(rawBmp, 0, bitmapData.Scan0, bmpLength);
            }
            bmpImage.UnlockBits(bitmapData);

            return bmpImage;
        }

        private void penelGraph_Paint(object sender, PaintEventArgs e)
        {
            if (rawImage != null)
            {

                if (m_bDrawReal == false)
                {
                    Color c = Color.FromArgb(44, 44, 44);
                    e.Graphics.Clear(c);
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    DrawBoundary();
                    DrawGraph();
                }
                else if (m_bDrawReal == true)
                {
                    float rx = Convert.ToSingle(this.Width / (double)bmp.Width);
                    float ry = Convert.ToSingle(this.Height / (double)bmp.Height);

                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

                    e.Graphics.ScaleTransform((float)rx, (float)ry);

                    e.Graphics.DrawImage(bmp, 0, 0);
                    //bmp.Save("c:\\nony.bmp");
                }
            }
        }
    }
    public static class IFX_GRAPH_TYPE
    {
        public static int PROJ_H = 0;
        public static int PROJ_V = 1;
        public static int HISTO = 2;
        public static int REAL = 3;
    }
}
