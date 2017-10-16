using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using DispObject;

namespace CD_Measure
{
    // 170828 PeakPairManager
    public class CPeakPair
    {
        public int COUNT { get { return listPeaks.Count; } }
        public class PeakPair
        {
            public PointF ptJoint = new PointF();
            public CLine l1 = new CLine();
            public CLine l2 = new CLine();

            // internal real sorter
            private class SortByHorizontal : IComparer
            {
                int IComparer.Compare(object a, object b)
                {

                    PeakPair pp1 = (PeakPair)a;
                    PeakPair pp2 = (PeakPair)b;

                    if (pp1.ptJoint.X < pp2.ptJoint.X) return -1;
                    else if (pp1.ptJoint.X > pp2.ptJoint.X) return 1;
                    return 0;
                }
            }
            private class SortByVertical : IComparer
            {
                int IComparer.Compare(object a, object b)
                {

                    PeakPair pp1 = (PeakPair)a;
                    PeakPair pp2 = (PeakPair)b;

                    if (pp1.ptJoint.Y < pp2.ptJoint.Y) return -1;
                    else if (pp1.ptJoint.Y > pp2.ptJoint.Y) return 1;
                    return 0;
                }
            }

            // external converter!!! real called function 
            public static IComparer Sort_By_Horizontal()
            {
                return (IComparer)new SortByHorizontal();
            }
            public static IComparer Sort_By_Vertical()
            {
                return (IComparer)new SortByVertical();
            }


        }

        public List<PeakPair> listPeaks = new List<PeakPair>();

        public PeakPair GetElement(int nIndex)
        {
            PeakPair single = new PeakPair();

            if (nIndex >= 0 && nIndex < this.COUNT)
            {
                single = listPeaks.ElementAt(nIndex);
            }
            return single;
        }
        public void Clear()
        {
            listPeaks.Clear();
        }
        public void Add(CLine l1, CLine l2, PointF ptJoint)
        {
            l1.EnsureDirectionalPosition();
            l2.EnsureDirectionalPosition();

            PeakPair single = new PeakPair();
            single.l1 = l1;
            single.l2 = l2;
            single.ptJoint = ptJoint;
            listPeaks.Add(single);
        }
        public void Sort_Horizontal()
        {
            PeakPair[] arrPeakPair = listPeaks.ToArray();
            Array.Sort(arrPeakPair, PeakPair.Sort_By_Horizontal());
            listPeaks = arrPeakPair.ToList();
        }
        public void Sort_Vertical()
        {
            PeakPair[] arrPeakPair = listPeaks.ToArray();
            Array.Sort(arrPeakPair, PeakPair.Sort_By_Vertical());
            listPeaks = arrPeakPair.ToList();
        }

        
    }

    public class CPeakMaster
    {
        public byte[] m_rawImage { get; set; }
        public int m_imageW { get; set; }
        public int m_imageH { get; set; }

        public CPeakMaster()
        {
            m_rawImage = null;
            m_imageW = m_imageH = 0;
        }

        public void SetImage(byte[] rawImage, int imageW, int imageH)
        {
            byte[] rawCopy = new byte[imageW * imageH];
            Array.Copy(rawImage, rawCopy, rawImage.Length);
            m_rawImage = rawCopy;
            m_imageW = imageW; m_imageH = imageH;
        }

        public void SetImage(Bitmap bmp)
        {
            m_rawImage = HC_CONV_Bmp2Byte(bmp);
            m_imageW = bmp.Width;
            m_imageH = bmp.Height;

        }
        public double[] GetNormalizedProjection_HOR()
        {
            if (m_rawImage != null)
            {
                double[] proj = CPeakMaster.HC_HISTO_GetProjection_HOR(this.m_rawImage, this.m_imageW, this.m_imageH);
                proj = CPeakMaster.HC_HISTO_Normalization(proj, this.m_imageW);
                return proj;
            }
            return new double[1];
        }
        public double[] GetNormalizedProjection_VER()
        {
            if (m_rawImage != null)
            {
                double[] proj = CPeakMaster.HC_HISTO_GetProjection_VER(this.m_rawImage, this.m_imageW, this.m_imageH);
                proj = CPeakMaster.HC_HISTO_Normalization(proj, this.m_imageH);
                return proj;
            }
            return new double[1];
        }

        public double[] GetProjection_HOR()
        {
            return CPeakMaster.HC_HISTO_GetProjection_HOR(this.m_rawImage, this.m_imageW, this.m_imageH);
        }
        public double[] GetProjection_VER()
        {
            return CPeakMaster.HC_HISTO_GetProjection_VER(this.m_rawImage, this.m_imageW, this.m_imageH);
        }
        public void ApplyGaussianFilter(double fSigma, int nKernel)
        {
            double[] fKernel = HC_FILTER_GenerateGaussianFilter(fSigma, nKernel);
            this.m_rawImage = HC_FILTER_Convolution(fKernel, this.m_rawImage, this.m_imageW, this.m_imageH);
        }

        public CPeakPair GeneratePeakData_HOR(double[] proj, int nPeaks)
        {
            List<PointF> listPoints = GetDispProjectionPoints_HOR(proj);
            List<CLine> listLine = GetLineSegments(listPoints, true);

            float[] arrValueList_X1 = listLine.Select(element => element.P1.X).ToArray();
            float[] arrValueList_X2 = listLine.Select(element => element.P2.X).ToArray();

            CPeakPair peakData = new CPeakPair();

            int nItemCount = 0;
            while (nItemCount != nPeaks)
            {
                if (listLine.Count == 0) break;
                if (nItemCount == nPeaks) break;

                float X2_fMax = arrValueList_X2.Max();
                int X2_MaxIndex = Array.IndexOf(arrValueList_X2, X2_fMax);

                CLine lineHead = listLine.ElementAt(X2_MaxIndex);

                // kill find position
                arrValueList_X2[X2_MaxIndex] = 0;

                CLine lineTail = new CLine();

                if ( X2_MaxIndex != 0 && arrValueList_X1[X2_MaxIndex - 1] == X2_fMax)
                {
                    lineTail = listLine[X2_MaxIndex - 1];
                    // kill tail
                    arrValueList_X2[X2_MaxIndex - 1] = 0;
                }
                else if (X2_MaxIndex !=0 && X2_MaxIndex < arrValueList_X1.Length - 1 && arrValueList_X1[X2_MaxIndex + 1] == X2_fMax)
                {
                    lineTail = listLine[X2_MaxIndex + 1];
                    // kill tail
                    arrValueList_X2[X2_MaxIndex - 1] = 0;
                }

                peakData.Add(lineHead, lineTail, lineHead.P2);
                nItemCount++;

            }

            peakData.Sort_Vertical();

            return peakData;
        }
        public CPeakPair GeneratePeakData_VER(double[] proj, int nPeaks, int nPeakH)
        {
            List<PointF> listPoints = GetDispProjectionPoints_VER(proj, nPeakH);
            List<CLine> listLine = GetLineSegments(listPoints, false);


            float[] arrValueList_Y1 = listLine.Select(element => element.P1.Y).ToArray();
            float[] arrValueList_Y2 = listLine.Select(element => element.P2.Y).ToArray();


            CPeakPair peakData = new CPeakPair();

            int nItemCount = 0;
            while (nItemCount != nPeaks)
            {
                if (listLine.Count == 0) break;
                if (nItemCount == nPeaks) break;

                float Y2_fMin = arrValueList_Y2.Min();
                int Y2_MinIndex = Array.IndexOf(arrValueList_Y2, Y2_fMin);

                CLine lineHead = listLine.ElementAt(Y2_MinIndex);

                // kill find position
                arrValueList_Y2[Y2_MinIndex] = int.MaxValue;

                CLine lineTail = new CLine();

                if (Y2_MinIndex != 0 && arrValueList_Y1[Y2_MinIndex - 1] == Y2_fMin)
                {
                    lineTail = listLine[Y2_MinIndex - 1];
                    // kill tail
                    arrValueList_Y2[Y2_MinIndex - 1] = int.MaxValue;
                }
                else if (Y2_MinIndex != 0 && Y2_MinIndex < arrValueList_Y1.Length-1 && arrValueList_Y1[Y2_MinIndex + 1] == Y2_fMin)
                {
                    lineTail = listLine[Y2_MinIndex + 1];
                    // kill tail
                    arrValueList_Y2[Y2_MinIndex - 1] = int.MaxValue;
                }

                peakData.Add(lineHead, lineTail, lineHead.P2);
                nItemCount++;

            }

            peakData.Sort_Horizontal();

            return peakData;
        }

        public CPeakPair GenAutoPeakData(bool bUseFiltering, bool bDirHor, int nMV_AGVF, int nCandidate)
        {
            if (bUseFiltering == true)
            {
                ApplyGaussianFilter(1.0, 5);
            }

            double[] proj = null;
            int PRJ_LENGTH = 0;

            /***/
            if (bDirHor == true) { proj = GetProjection_HOR(); PRJ_LENGTH = m_imageW; }
            else if (bDirHor == false) { proj = GetProjection_VER(); PRJ_LENGTH = m_imageH; }

            proj = CPeakMaster.HC_HISTO_MovingAverageFilter(proj, nMV_AGVF);
            proj = CPeakMaster.HC_EDGE_Get2ndDerivativeArrayFromLineBuff_FIXEDLENGTH(proj);
            proj = CPeakMaster.HC_EDGE_EnsurePossitiveScale(proj);
            proj = CPeakMaster.HC_HISTO_Normalization(proj, PRJ_LENGTH);

            List<CLine> listLine = new List<CLine>();

            CPeakPair peakData = new CPeakPair();

            if (bDirHor == true)
            {
                listLine = CPeakMaster.GetDispProjectionLine_HOR(proj);
                peakData = GeneratePeakData_HOR(proj, nCandidate);
            }
            else if (bDirHor == false)
            {
                listLine = CPeakMaster.GetDispProjectionLine_VER(proj, m_imageH);
                peakData = GeneratePeakData_VER(proj, nCandidate, m_imageH);
            }
            return peakData;

        }

        public RectangleF GetRegionByPeakAnalysis(CPeakPair.PeakPair single,  int param_03_detect_type, bool bool_horizontal_dir)
        {
            RectangleF rcRegion = new RectangleF();

            if (param_03_detect_type == 0)
            {
                // nothing to do
            }
            else if (param_03_detect_type == 1) // peak-orient negative
            {
                /***/
                if (bool_horizontal_dir == true) { rcRegion = single.l1.ToRectangleF_StrechHor(0, this.m_imageW); }
                else if (bool_horizontal_dir == false) { rcRegion = single.l1.ToRectangleF_StrechVer(0, this.m_imageH); }
            }
            else if (param_03_detect_type == 2) // peak-orient positive
            {
                /***/
                if (bool_horizontal_dir == true) { rcRegion = single.l2.ToRectangleF_StrechHor(0, this.m_imageW); }
                else if (bool_horizontal_dir == false) { rcRegion = single.l2.ToRectangleF_StrechVer(0, this.m_imageH); }
            }
            else if (param_03_detect_type == 3) // line-orient negative-negative
            {
                if (bool_horizontal_dir == true)
                {
                    rcRegion = single.l1.ToRectangleF_StrechHor(0, this.m_imageW);
                    rcRegion = CRect.Shift_Half_TOP(rcRegion);
                }
                else if (bool_horizontal_dir == false)
                {
                    rcRegion = single.l1.ToRectangleF_StrechVer(0, this.m_imageH);
                    rcRegion = CRect.Shift_Half_LFT(rcRegion);
                }
            }
            else if (param_03_detect_type == 4) // line-orient negative-positive
            {
                if (bool_horizontal_dir == true)
                {
                    rcRegion = single.l1.ToRectangleF_StrechHor(0, this.m_imageW);
                    rcRegion = CRect.Shift_Half_BTN(rcRegion);
                }
                else if (bool_horizontal_dir == false)
                {
                    rcRegion = single.l1.ToRectangleF_StrechVer(0, this.m_imageH);
                    rcRegion = CRect.Shift_Half_RHT(rcRegion);
                }

            }
            else if (param_03_detect_type == 5) // line-orient positive-negative
            {
                if (bool_horizontal_dir == true)
                {
                    rcRegion = single.l2.ToRectangleF_StrechHor(0, this.m_imageW);
                    rcRegion = CRect.Shift_Half_TOP(rcRegion);

                }
                else if (bool_horizontal_dir == false)
                {
                    rcRegion = single.l2.ToRectangleF_StrechVer(0, this.m_imageH);
                    rcRegion = CRect.Shift_Half_LFT(rcRegion);
                }

            }
            else if (param_03_detect_type == 6) // line-orient positivie-positive
            {
                if (bool_horizontal_dir == true)
                {
                    rcRegion = single.l2.ToRectangleF_StrechHor(0, this.m_imageW);
                    rcRegion = CRect.Shift_Half_BTN(rcRegion);
                }
                else if (bool_horizontal_dir == false)
                {
                    rcRegion = single.l2.ToRectangleF_StrechVer(0, this.m_imageH);
                    rcRegion = CRect.Shift_Half_RHT(rcRegion);
                }
            }
            return rcRegion;
        }
        //*****************************************************************************************
        // Static Functions
        //*****************************************************************************************

        // extract Line segments according to their own direction. 
        // line direction is determined by substraction  (i - (i+1))
        // if { negative : i < i+1 }  && { positive : i > i+1} && { equal : i == i+1 }
        public static List<CLine> GetLineSegments(List<PointF> list, bool bCroodX)
        {
            List<CLine> listLine = new List<CLine>();

            PointF[] listCopy = list.ToArray();
            int[] arrSign = new int[listCopy.Length];


            for (int i = 0; i < listCopy.Length - 1; i++)
            {
                arrSign[i] = _GetSign(listCopy[i], listCopy[i + 1], bCroodX);
            }

            int nStart = 0;
            int signPrev = arrSign[0];
            int nEnd = 0;


            for (int i = 1; i < listCopy.Length - 1; i++)
            {
                int signCurr = arrSign[i];

                if (signCurr == signPrev || signCurr == 0)
                {
                    continue;
                }
                else if (signCurr != signPrev)
                {
                    nEnd = i - 1; // minus 1 is very important!!! to finish correctly.

                }

                CLine line = new CLine();
                line.P1 = listCopy.ElementAt(nStart);
                line.P2 = listCopy.ElementAt(nEnd + 1);
                listLine.Add(line);
                signPrev = signCurr;
                nStart = i;
            }

            //*************************************************************************************

            return listLine;
        }

        /// <summary>
        /// Get The Sign ( -1, 0 , +1 ) from " P1-P2" of [X || Y]
        /// 170828
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="bCroodX"> SELECTION OF THE TARGET CROODINATE X OR Y</param>
        /// <returns></returns>
        private static int _GetSign(PointF P1, PointF P2, bool bCroodX)
        {
            int nSign = 0;

            if (bCroodX == true)
            {
                /***/
                if (P1.X == P2.X) nSign = 0;
                else if (P1.X < P2.X) nSign = 1;
                else if (P1.X > P2.X) nSign = -1;
            }
            else
            {
                /***/
                if (P1.Y == P2.Y) nSign = 0;
                else if (P1.Y < P2.Y) nSign = 1;
                else if (P1.Y > P2.Y) nSign = -1;
            }

            return nSign;
        }
        public static int[] HC_ARRAY_CONV_Double2Int(double[] fArray) // 170825
        {
            int[] nArray = fArray.Select(element => (int)element).ToArray();
            return nArray;
        }
        public static double[] HC_ARRAY_CONV_Int2Double(int[] nArray) // 170825
        {
            double[] fArray = nArray.Select(element => (double)element).ToArray();
            return fArray;
        }


        public static double[] /****/HC_HISTO_GetProjection_VER(byte[] rawImage, int imageW, int imageH)
        {
            double[] arrProjectionH = new double[imageW];

            int nSum = 0;
            for (int x = 0; x < imageW; x++)
            {
                nSum = 0;
                for (int y = 0; y < imageH; y++) { nSum += rawImage[y * imageW + x]; }
                arrProjectionH[x] = nSum;
            }
            return arrProjectionH;
        }
        public static double[] /****/HC_HISTO_GetProjection_HOR(byte[] rawImage, int imageW, int imageH)
        {
            double[] arrProjectionV = new double[imageH];

            int nSum = 0;
            for (int y = 0; y < imageH; y++)
            {
                nSum = 0;
                for (int x = 0; x < imageW; x++) { nSum += rawImage[y * imageW + x]; }
                arrProjectionV[y] = nSum;
            }
            return arrProjectionV;
        }

        public static List<PointF> GetDispProjectionPoints_HOR(object proj)
        {
            object ARRAY_DOUBLE = new double[1];
            object ARRAY_INT = new byte[1];

            List<PointF> list = new List<PointF>();

            if (proj.GetType() == ARRAY_INT.GetType())
            {
                int[] arr = (int[])proj;

                for (int y = 0; y < arr.Length; y++)
                {
                    PointF p1 = new PointF(arr[y], y);
                    list.Add(p1);
                }
            }
            else if (proj.GetType() == ARRAY_DOUBLE.GetType())
            {
                double[] arr = (double[])proj;

                for (int y = 0; y < arr.Length; y++)
                {
                    PointF p1 = new PointF((float)arr[y], y);
                    list.Add(p1);
                }

            }

            return list;
        }
        public static List<PointF> GetDispProjectionPoints_VER(object proj, int dispH)
        {
            object ARRAY_DOUBLE = new double[1];
            object ARRAY_INT = new byte[1];

            List<PointF> list = new List<PointF>();

            if (proj.GetType() == ARRAY_INT.GetType())
            {
                int[] arr = (int[])proj;

                for (int x = 0; x < arr.Length - 1; x++)
                {
                    PointF p1 = new PointF(x, dispH - arr[x]);
                    list.Add(p1);
                }
            }
            else if (proj.GetType() == ARRAY_DOUBLE.GetType())
            {
                double[] arr = (double[])proj;
                for (int x = 0; x < arr.Length - 1; x++)
                {
                    PointF p1 = new PointF(x, dispH - (float)arr[x]);
                    list.Add(p1);
                }

            }
            return list;
        }
        public static List<CLine> GetDispProjectionLine_HOR(object proj)
        {
            List<PointF> list = GetDispProjectionPoints_HOR(proj);

            List<CLine> listLine = new List<CLine>();

            for (int i = 0; i < list.Count - 1; i++)
            {
                PointF p1 = list.ElementAt(i);
                PointF p2 = list.ElementAt(i + 1);

                CLine line = new CLine(p1, p2);
                listLine.Add(line);
            }
            return listLine;
        }
        public static List<CLine> GetDispProjectionLine_VER(object proj, int dispH)
        {
            List<PointF> list = GetDispProjectionPoints_VER(proj, dispH);

            List<CLine> listLine = new List<CLine>();

            for (int i = 0; i < list.Count - 1; i++)
            {
                PointF p1 = list.ElementAt(i);
                PointF p2 = list.ElementAt(i + 1);

                CLine line = new CLine(p1, p2);
                listLine.Add(line);
            }
            return listLine;
        }

        // if array includes minus scale -> move to the possitive plus scale 170825
        // this function is combination function for the below : HC_EDGE_Get2ndDerivativeArrayFromLineBuff_FixedLength
        public static double[] HC_EDGE_EnsurePossitiveScale(double[] fLineBuff)
        {
            double[] fBuffCopy = fLineBuff.ToArray();

            // get min value 
            double fMin = fBuffCopy.Min();

            // if min value is minus scale  -> shift!! up 
            if (fMin < 0)
            {
                for (int i = 0; i < fLineBuff.Length; i++)
                {
                    fBuffCopy[i] += Math.Abs(fMin);
                }
            }
            return fBuffCopy;
        }

        // Just Get 2nd Derivative but, Make Length Equal to the input by filling method called mirroring 170825
        public static double[] HC_EDGE_Get2ndDerivativeArrayFromLineBuff_FIXEDLENGTH(double[] fLineBuff)
        {
            double[] arr1st = new double[fLineBuff.Length - 1];
            double[] arr2nd = new double[fLineBuff.Length - 2];

            for (int i = 0; i < fLineBuff.Length - 1; i++)
            {
                arr1st[i] = fLineBuff[i + 1] - fLineBuff[i];
            }
            for (int i = 0; i < arr1st.Length - 1 + 0; i++)
            {
                arr2nd[i] = arr1st[i + 1] - arr1st[i];
            }
            // above procedure is same with basic 2nd derivative.


            double[] FixedArr = new double[fLineBuff.Length];

            // Fill the first empty value
            FixedArr[0] = arr2nd[0];

            // copy body
            for (int i = 0; i < arr2nd.Length; i++)
            {
                FixedArr[i + 1] = arr2nd[i];
            }
            // fill the last tail 
            FixedArr[FixedArr.Length - 1] = arr2nd[arr2nd.Length - 1];

            return arr2nd;
        }

        // sliding window averaging approach 170825 
        public static double[] HC_HISTO_MovingAverageFilter(double[] arr, int nWindow)
        {
            // empty window size is now allowed
            if (nWindow == 0) nWindow = 3;

            double[] arrCopy = new double[arr.Length];
            double[] arrWindow = new double[nWindow];

            // get the window gravity 
            int nHalf = nWindow / 2;

            for (int index = nHalf; index < arr.Length - nHalf; index++)
            {
                Array.Clear(arrWindow, 0, nWindow);

                // get the window list
                for (int i = index - nHalf, winPos = 0; i < index + nHalf; i++)
                {
                    arrWindow[winPos++] = arr[i];
                }
                // fill the average value 
                arrCopy[index] = (int)arrWindow.Average();
            }

            // fill head empty space by copy
            for (int i = 0; i < nHalf; i++)
            {
                arrCopy[i] = arrCopy[nHalf];
            }
            // fill tail empty space by copy 
            for (int i = arrCopy.Length - nHalf - 1; i < arrCopy.Length; i++)
            {
                arrCopy[i] = arrCopy[arrCopy.Length - nHalf - 1];
            }


            return arrCopy;
        }

        public static int[]/**/HC_HISTO_Normalization(int[]/**/nArray, int nNormalizationValue)
        {
            int[] nArrCopy = nArray.ToArray();

            try
            {
                // Fucking Normalization
                int Max = 1;

                for (int nIndex = 0; nIndex < nArrCopy.Length; nIndex++)
                {
                    double fValue = nArrCopy[nIndex];
                    if (fValue > Max) Max = (int)fValue;
                }

                for (int nIndex = 0; nIndex < nArrCopy.Length; nIndex++)
                {
                    double fValue = nArrCopy[nIndex] * nNormalizationValue;
                    nArrCopy[nIndex] = (int)fValue;

                    // i want to avoid divide by zero
                    if (nArrCopy[nIndex] != 0) nArrCopy[nIndex] /= Max;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return nArrCopy;
        }
        public static double[] HC_HISTO_Normalization(double[] fArray, int nNormalizationValue)
        {
            double[] fArrCopy = fArray.ToArray();

            try
            {
                // Fucking Normalization
                int Max = 1;

                for (int nIndex = 0; nIndex < fArrCopy.Length; nIndex++)
                {
                    double fValue = fArrCopy[nIndex];
                    if (fValue > Max) Max = (int)fValue;
                }

                for (int nIndex = 0; nIndex < fArrCopy.Length; nIndex++)
                {
                    double fValue = fArrCopy[nIndex] * nNormalizationValue;
                    fArrCopy[nIndex] = (int)fValue;

                    // i want to avoid divide by zero
                    if (fArrCopy[nIndex] != 0) fArrCopy[nIndex] /= Max;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return fArrCopy;

        }

        public static double[] HC_FILTER_GenerateGaussianFilter(double fSigma, int nKSize)
        {
            double[] fKernel = new double[nKSize * nKSize];

            int GAP = nKSize / 2;

            Parallel.Invoke(() =>
            {
                for (int y = -GAP; y <= GAP; y++)
                {
                    for (int x = -GAP; x <= GAP; x++)
                    {
                        fKernel[(y + GAP) * nKSize + x + GAP] = x;
                    }
                }

                double s = 2.0 * fSigma * fSigma;

                double fSum = 0;

                for (int x = -GAP; x <= GAP; x++)
                {
                    for (int y = -GAP; y <= GAP; y++)
                    {
                        double r = Math.Sqrt(x * x + y * y);

                        fKernel[(y + GAP) * nKSize + x + GAP] = Math.Exp((-((r * r) / s))) / (s * Math.PI);
                        fSum += fKernel[(y + GAP) * nKSize + x + GAP];
                    }
                }
                for (int y = 0; y < nKSize; y++)
                {
                    for (int x = 0; x < nKSize; x++)
                    {
                        fKernel[y * nKSize + x] /= fSum;
                    }
                }
            });


            return fKernel;
        }

        #region CONVERSION

        public static Bitmap/******/HC_CONV_Byte2Bmp(byte[] rawImage, int imageW, int imageH)
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
        public static byte[] /*****/HC_CONV_Bmp2Byte(System.Drawing.Bitmap bmpImage)
        {
            int imageW = bmpImage.Width;
            int imageH = bmpImage.Height;

            int nRealW = 0, nStride = 0, bmpLength = 0;
            byte[] rawBmp = null;

            BitmapData bitmapData = bmpImage.LockBits(new Rectangle(0, 0, imageW, imageH), System.Drawing.Imaging.ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            {
                imageW = bitmapData.Width;
                imageH = bitmapData.Height;
                nRealW = imageW;

                nStride = Math.Abs(bitmapData.Stride);
                bmpLength = nStride * imageH;


                rawBmp = new byte[bmpLength];
                System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, rawBmp, 0, bmpLength);
            }
            bmpImage.UnlockBits(bitmapData);

            int nImageW = imageW;
            int nImageH = imageH;

            byte[] rawImage = new byte[imageW * imageH];

            Parallel.For(0, imageH, y =>
            {
                for (int x = 0; x < nImageW; x++)
                {
                    rawImage[y * nImageW + x] = (byte)((rawBmp[(y * nStride) + (x * 3) + 0] + rawBmp[(y * nStride) + (x * 3) + 1] + rawBmp[(y * nStride) + (x * 3) + 2]) / 3);
                }
            });
            return rawImage;
        }

        #endregion

        #region convolution
        public static object ARRAY_Padding_LT(object rawImage, int imageW, int imageH, int nGap)
        {
            object ARRAY_DOUBLE = new double[1];
            object ARRAY_BYTE = new byte[1];
            object returnData = null;

            int newW = imageW + nGap;
            int newH = imageH + nGap;


            if (rawImage.GetType() == ARRAY_DOUBLE.GetType())
            {
                #region for double
                int size = sizeof(double);
                double[] fArray = (double[])rawImage;
                double[] newArray = new double[newW * newH];

                int orgY = 0;
                int CopyLength = imageW * size;
                for (int y = nGap; y < newH; y++)
                    Buffer.BlockCopy(fArray, (orgY++ * imageW) * size, newArray, ((y * newW) + nGap) * size, CopyLength);

                if (nGap > imageW) return newArray;

                Parallel.For(nGap, newH, y => { for (int x = 0; x <= nGap; x++) { newArray[y * newW + nGap - x] = newArray[y * newW + nGap + x]; } });

                CopyLength = newW * size;
                Parallel.For(0, nGap + 1, y => { Buffer.BlockCopy(newArray, ((nGap + y) * (newW)) * size, newArray, ((nGap - y) * (newW)) * size, CopyLength); });
                returnData = newArray;
                #endregion
            }
            else if (rawImage.GetType() == ARRAY_BYTE.GetType())
            {
                #region for byte

                byte[] byteArray = (byte[])rawImage;
                byte[] newArray = new byte[newW * newH];

                int orgY = 0;

                for (int y = nGap; y < newH; y++)
                    Buffer.BlockCopy(byteArray, orgY++ * imageW, newArray, y * newW + nGap, imageW);

                if (nGap > imageW) return newArray;

                Parallel.For(nGap, newH, y => { for (int x = 0; x <= nGap; x++) { newArray[y * newW + nGap - x] = newArray[y * newW + nGap + x]; } });
                Parallel.For(0, nGap + 1, y => { Buffer.BlockCopy(newArray, (nGap + y) * newW, newArray, (nGap - y) * newW, newW); });

                returnData = newArray;
                #endregion
            }

            return returnData;
        }
        public static object ARRAY_Padding_RB(object rawImage, int imageW, int imageH, int nGap)
        {
            object ARRAY_DOUBLE = new double[1];
            object ARRAY_BYTE = new byte[1];
            object returnData = null;

            int newW = imageW + nGap;
            int newH = imageH + nGap;

            if (rawImage.GetType() == ARRAY_DOUBLE.GetType())
            {
                int size = sizeof(double);
                double[] fArray = (double[])rawImage;
                double[] newArray = new double[newW * newH];

                int copyLength = imageW * sizeof(double);
                Parallel.For(0, imageH, y =>
                {
                    Buffer.BlockCopy(fArray, (y * imageW) * size, newArray, (y * newW) * size, copyLength);
                });

                // right direction copy
                Parallel.For(0, nGap, x =>
                {
                    for (int y = 0; y < imageH; y++)
                    {
                        newArray[y * newW + imageW + x] = newArray[y * newW + imageW - 1 - x];
                    }
                });

                double[] rawPadVert = new double[newW];

                // bottom direction copy - reverse
                copyLength = newW * sizeof(double);
                Parallel.For(0, nGap, y =>
                {
                    Buffer.BlockCopy(newArray, ((imageH - 1 - y) * newW) * size, rawPadVert, 0, copyLength);
                    Buffer.BlockCopy(rawPadVert, 0, newArray, ((imageH + y) * newW) * size, copyLength);
                });
                returnData = newArray;
            }
            else if (rawImage.GetType() == ARRAY_BYTE.GetType())
            {
                byte[] byteArray = (byte[])rawImage;
                byte[] newArray = new byte[newW * newH];

                Parallel.For(0, imageH, y =>
                {
                    Buffer.BlockCopy(byteArray, y * imageW, newArray, y * newW, imageW);
                });

                // right direction copy
                Parallel.For(0, nGap, x =>
                {
                    for (int y = 0; y < imageH; y++)
                    {
                        newArray[y * newW + imageW + x] = newArray[y * newW + imageW - 1 - x];
                    }
                });

                byte[] rawPadVert = new byte[newW];

                // bottom direction copy - reverse
                Parallel.For(0, nGap, y =>
                {
                    Buffer.BlockCopy(newArray, (imageH - 1 - y) * newW, rawPadVert, 0, newW);
                    Buffer.BlockCopy(rawPadVert, 0, newArray, (imageH + y) * newW, newW);
                });
                returnData = newArray;
            }
            return returnData;
        }
        public static Object ARRAY_Padding_ALL(Object obArray, int arrW, int arrH, int nGap)
        {
            object firstPadding = null;
            object seconPadding = null;
            Parallel.Invoke(() => { firstPadding = ARRAY_Padding_LT(obArray, arrW, arrH, nGap); });
            Parallel.Invoke(() => { seconPadding = ARRAY_Padding_RB(firstPadding, arrW + nGap, arrH + nGap, nGap); });
            return seconPadding;
        }

        public static byte[] /*****/HC_FILTER_Convolution(double[] fKernel, byte[] rawImage, int imageW, int imageH)
        {
            double[] fImage = new double[imageW * imageH];

            int KSIZE = (int)Math.Sqrt(fKernel.Length);
            int GAP = KSIZE / 2;

            byte[] rawExpanded = (byte[])ARRAY_Padding_ALL(rawImage, imageW, imageH, GAP);

            int imageNewW = imageW + GAP * 2;
            int imageNewH = imageH + GAP * 2;

            //for (int y = GAP; y < imageNewH - GAP; y++)
            Parallel.For(GAP, imageNewH - GAP, y =>
            {
                for (int x = GAP; x < imageNewW - GAP; x++)
                {
                    double kernelSum = 0;
                    for (int j = -GAP; j <= GAP; j++)
                    {
                        for (int k = -GAP; k <= GAP; k++)
                        {
                            kernelSum += (fKernel[(j + GAP) * KSIZE + k + GAP] * rawExpanded[(y - j) * imageNewW + (x - k)]);
                        }
                    }
                    kernelSum = kernelSum > 255 ? 255 : kernelSum < 0 ? 0 : kernelSum;
                    fImage[(y - GAP) * imageW + (x - GAP)] = kernelSum;
                }
            });

            byte[] res = new byte[imageW * imageH];


            Parallel.For(0, imageH, y =>
            {
                for (int x = 0; x < imageW; x++)
                {
                    //res[y * imageW + x] = EnsureByte(fImage[y * imageW + x]);
                    res[y * imageW + x] = (byte)fImage[y * imageW + x];

                }
            });

            return res;
        }
        public static byte EnsureByte(object value)
        {
            if (double.IsNaN((double)value) == true) return 0;

            double result /*****/= (double)value; ;
            return (byte)result > 255 ? (byte)255 : (byte)result < 0 ? (byte)0 : (byte)result;
        }
        public static double[] /***/HC_FILTER_Convolution(double[] fKernel, double[] fRawImage, int imageW, int imageH)
        {
            int KSIZE = (int)Math.Sqrt(fKernel.Length);
            int GAP = KSIZE / 2;

            double[] rawExpanded = (double[])ARRAY_Padding_ALL(fRawImage, imageW, imageH, GAP);

            double[] fRawRes = new double[imageW * imageH];

            int imageNewW = imageW + GAP * 2;
            int imageNewH = imageH + GAP * 2;

            Parallel.For(GAP, imageNewH - GAP, y =>
            {
                for (int x = GAP; x < imageNewW - GAP; x++)
                {
                    double kernelSum = 0;
                    for (int j = -GAP; j <= GAP; j++)
                    {
                        for (int k = -GAP; k <= GAP; k++)
                        {
                            kernelSum += (fKernel[(j + GAP) * KSIZE + k + GAP] * rawExpanded[(y - j) * imageNewW + (x - k)]);
                        }
                    }
                    fRawRes[(y - GAP) * imageW + (x - GAP)] = kernelSum;
                }
            });
            return fRawRes;
        }
        public static byte[] /*****/HC_FILTER_ConvolutionWindow(double[] fKernel, byte[] rawImage, int imageW, int imageH, Rectangle rc)
        {
            double[] fImage = new double[rawImage.Length];

            int KSIZE = (int)Math.Sqrt(fKernel.Length);
            int GAP = KSIZE / 2;

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == false) // 170523 overlapeed rectangle exception
            {
                //for( int y = rc.Y; y < rc.Y + rc.Height; y++)
                Parallel.For(rc.Y, rc.Y + rc.Height, y =>
                {
                    for (int x = rc.X; x < rc.X + rc.Width; x++)
                    {
                        double kernelSum = 0;
                        for (int j = -GAP; j <= GAP; j++)
                        {
                            for (int k = -GAP; k <= GAP; k++)
                            {
                                kernelSum += (fKernel[(j + GAP) * KSIZE + k + GAP] * rawImage[(y - j) * imageW + (x - k)]);
                            }
                        }
                        //fImage[(y - GAP) * imageW + (x - GAP)] = kernelSum;
                        fImage[y * imageW + x] = kernelSum;
                    }
                });

                Parallel.For(rc.Y, rc.Y + rc.Height, y =>
                {
                    for (int x = rc.X; x < rc.X + rc.Width; x++)
                    {
                        rawImage[y * imageW + x] = (byte)fImage[y * imageW + x];
                    }
                });
            }

            return rawImage;
        }
        #endregion

        public static void HC_ARRAY_Dump(string strPath, int[] nArray)
        {

            string strBody = string.Empty;

            for (int i = 0; i < nArray.Length; i++)
            {
                strBody += nArray[i].ToString() + ",";
            }

            try
            {
                System.IO.File.WriteAllText(strPath, strBody);
            }
            catch { }
        }
    }
}
