using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using DispObject;
using DotNetMatrix;

namespace CD_Measure
{
    public static class Computer
    {
        static int DIR_INFALL = 1;
        static int DIR_INRISE = 2;
        static int DIR_EXFALL = 3;
        static int DIR_EXRISE = 4;

        public static double[] GetArray_Degree()
        {
            double [] array = new double[360];

            Parallel.For(0, 360, nAngle =>
            {
                array[nAngle] = ((nAngle - 90.0) * Math.PI / 180.0);

            });
            return array;
        }
        public static double[] GetArray_COS()
        {
            double[] arrDegree = GetArray_Degree();

            double[] array = new double[360];

            Parallel.For(0, 360, nAngle =>
            {
                array[nAngle] = Math.Cos(arrDegree[nAngle]);
            });
            return array;
        }

        public static double[] GetArray_SIN()
        {
            double[] arrDegree = GetArray_Degree();

            double[] array = new double[360];

            Parallel.For(0, 360, nAngle =>
            {
                array[nAngle] = Math.Sin(arrDegree[nAngle]);
            });
            return array;
        }

     

        #region IMAGE IO
        public static void /*****/SaveImage(byte[] rawImage, int imageW, int imageH, string strPath)
        {
            Bitmap bmp = (Bitmap)HC_CONV_Byte2Bmp(rawImage, imageW, imageH);
            bmp.Save(strPath);
        }
        public static byte[] HC_CropImage(byte[] rawInput, int imageW, int imageH, int ptX, int ptY, int cropW, int cropH)
        {
            byte[] rawCrop = new byte[cropW * cropH];

            if (CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), new RectangleF(ptX, ptY, cropW, cropH)) == true)
            {
                for (int y = ptY, copyLine = 0; y < ptY + cropH; y++)
                {
                    Buffer.BlockCopy(rawInput, y * imageW + ptX, rawCrop, cropW * copyLine++, cropW);
                }
            }

            return rawCrop;
        }
        public static byte[] HC_CropImage(byte[] rawInput, int imageW, int imageH, RectangleF rc)
        {
            int nLength = (int)rc.Width * (int)rc.Height;
            int toHeight = (int)rc.Y + (int)rc.Height;
            int toWidth = (int)rc.Width;
            int px = (int)rc.X;

            byte[] rawCrop = new byte[nLength];

            if (CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), rc) == true)
            {
                for (int y = (int)rc.Y, copyLine = 0; y < toHeight; y++)
                {
                    Buffer.BlockCopy(rawInput, y * imageW + px, rawCrop, toWidth * copyLine++, toWidth);
                }

            }
            return rawCrop;
        }
        public static byte[] HC_CropImage_Center(byte[] rawInput, int imageW, int imageH, RectangleF rc, PointF ptCenter)
        {
            PointF ptDist = CPoint.GetDistancePoint(CRect.GetCenter(rc), ptCenter);
            rc.Offset(ptDist);

            int nLength = (int)rc.Width * (int)rc.Height;
            int toHeight = (int)rc.Y + (int)rc.Height;
            int toWidth = (int)rc.Width;
            int px = (int)rc.X;

            byte[] rawCrop = new byte[nLength];

            if (CRect.IsIntersectRect(new RectangleF(0, 0, imageW, imageH), rc) == true)
            {
                for (int y = (int)rc.Y, copyLine = 0; y < toHeight; y++)
                {
                    Buffer.BlockCopy(rawInput, y * imageW + px, rawCrop, toWidth * copyLine++, toWidth);
                }

            }
            return rawCrop;
        }
        public static byte[] HC_CropImage_Polar(byte[] rawInput, int imageW, int imageH, RectangleF rc )
        {
            int nCX = Convert.ToInt32(rc.Width / 2.0);
            int nCY = Convert.ToInt32(rc.Height / 2.0);
            int nRadius = Math.Max(nCX, nCY);

            nCX += (int)rc.X;
            nCY += (int)rc.Y;

            byte[] rawPolar = new byte[360 * nRadius];

            for (int na = 0; na < 360; na++)
            {
                int nPolarY = nRadius;
                for (int nr = 0; nr < nRadius; nr++)
                {
                    double fDegree = ((na - 90.0) * Math.PI / 180.0);

                    double x = nCX + (((double)nr) * Math.Cos(fDegree));
                    double y = nCY + (((double)nr) * Math.Sin(fDegree));

                    if (x < 0 || y < 0 || x >= imageW || y >= imageH)
                    {
                        continue;
                    }
                    rawPolar[--nPolarY * 360 + na] = rawInput[(int)y * imageW + (int)x];
                }
            }
            return rawPolar;
        }
        public static byte[] HC_CropImage_Polar(byte[] rawInput, int imageW, int imageH, RectangleF rc, out int nRadius)
        {
            int nCX = Convert.ToInt32(rc.Width / 2.0);
            int nCY = Convert.ToInt32(rc.Height/ 2.0);
            nRadius = Math.Max(nCX, nCY);

            nCX += (int)rc.X;
            nCY += (int)rc.Y;

            byte[] rawPolar = new byte[360 * nRadius];

            for (int na = 0; na < 360; na++)
            {
                int nPolarY = nRadius;
                for (int nr = 0; nr < nRadius; nr++)
                {
                    double fDegree = ((na-90.0 ) * Math.PI / 180.0);

                    double x = nCX + (((double)nr) * Math.Cos(fDegree));
                    double y = nCY + (((double)nr) * Math.Sin(fDegree));

                    if (x < 0 || y < 0 || x >= imageW || y >= imageH)
                    {
                        continue;
                    }
                    rawPolar[--nPolarY * 360 + na] = rawInput[(int)y*imageW+(int)x];
                }
            }
            return rawPolar;
        }
        // 임으의 센터를 기준으로 Interpolated Polar를 구해
        public static byte[] HC_CropImage_Interpolated_Polar(byte[] rawInput, int imageW, int imageH, RectangleF rc, PointF ptCenter)
        {
            int hW = Convert.ToInt32(rc.Width / 2.0);
            int hh = Convert.ToInt32(rc.Height / 2.0);
            int nRadius = Math.Max(hW, hh);
            //
            //nCX += (int)rc.X;
            //nCY += (int)rc.Y;

            byte[] rawPolar = new byte[360 * nRadius];

            double[] arrDegree = Computer.GetArray_Degree();

            //for (int nAngle = 0; nAngle < 360; nAngle++)
            Parallel.For(0, 360, nAngle =>
            {
                int nPolarY = nRadius;
                for (int nr = 0; nr < nRadius; nr++)
                {
                    double fDegree = arrDegree[nAngle];

                    double x = ptCenter.X + ((double)nr * Math.Cos(fDegree));
                    double y = ptCenter.Y + ((double)nr * Math.Sin(fDegree));

                    // final size = -1 170620 bugfix
                    if (x < 0 || y < 0 || x >= imageW - 1 || y >= imageH - 1) { continue; }

                    int x1 = (int)Math.Floor(x);
                    int x2 = (int)Math.Ceiling(x);
                    int y1 = (int)Math.Floor(y);
                    int y2 = (int)Math.Ceiling(y);

                    int q11 = rawInput[y1 * imageW + x1];
                    int q12 = rawInput[y2 * imageW + x1];
                    int q21 = rawInput[y1 * imageW + x2];
                    int q22 = rawInput[y2 * imageW + x2];

                    double fInterplated = GetInterPolatedValue(x, y, x1, x2, y1, y2, q11, q12, q21, q22);

                    byte valueOrg = rawInput[(int)y * imageW + (int)x];

                    // if interpolation is not successful, zero value produced. 170623
                    // thus, in this case, if non interpolated value is not zero?
                    // replace error interpolated value from non interpolated original value.
                    byte value = fInterplated < 0 ? (byte)0 : fInterplated > 255 ? (byte)255 : (byte)fInterplated;

                    if (double.IsNaN(fInterplated) == true || (fInterplated == 0 && valueOrg != 0)) value = valueOrg;
                    rawPolar[--nPolarY * 360 + nAngle] = value;

                }
            });
            return rawPolar;
        }
        // 그냥 닥치는 대로 Interpolated Polar를 구해
        public static byte[] HC_CropImage_Interpolated_Polar(byte[] rawInput, int imageW, int imageH, RectangleF rc)
        {
            int nCX = Convert.ToInt32(rc.Width / 2.0);
            int nCY = Convert.ToInt32(rc.Height / 2.0);
            int nRadius = Math.Max(nCX, nCY);

            nCX += (int)rc.X;
            nCY += (int)rc.Y;

            byte[] rawPolar = new byte[360 * nRadius];

            for (int na = 0; na < 360; na++)
            {
                int nPolarY = nRadius;
                for (int nr = 0; nr < nRadius; nr++)
                {
                    double fDegree = ((na - 90.0) * Math.PI / 180.0);

                    double x = nCX + ((double)nr * Math.Cos(fDegree));
                    double y = nCY + ((double)nr * Math.Sin(fDegree));

                    // final size = -1 170620 bugfix
                    if (x < 0 || y < 0 || x >= imageW-1 || y >= imageH-1) { continue; }

                    int x1 = (int)Math.Floor(x);
                    int x2 = (int)Math.Ceiling(x);
                    int y1 = (int)Math.Floor(y);
                    int y2 = (int)Math.Ceiling(y);

                    int q11 = rawInput[y1 * imageW + x1];
                    int q12 = rawInput[y2 * imageW + x1];
                    int q21 = rawInput[y1 * imageW + x2];
                    int q22 = rawInput[y2 * imageW + x2];

                    double fInterplated = GetInterPolatedValue(x, y, x1, x2, y1, y2, q11, q12, q21, q22);

                    byte valueOrg = rawInput[(int)y * imageW + (int)x];

                    // if interpolation is not successful, zero value produced. 170623
                    // thus, in this case, if non interpolated value is not zero?
                    // replace error interpolated value from non interpolated original value.
                    byte value = fInterplated < 0 ? (byte)0 : fInterplated > 255 ? (byte)255 : (byte)fInterplated;

                    if (double.IsNaN(fInterplated) == true || (fInterplated == 0 && valueOrg != 0)) value = valueOrg;
                    rawPolar[--nPolarY * 360 + na] = value;

                }
            }
            return rawPolar;
        }
        // 특정 영역을 Polar로 저장하고싶을때. radius를 알아야 이미지 싸이즈를 알지..
        public static byte[] HC_CropImage_Interpolated_Polar(byte[] rawInput, int imageW, int imageH, RectangleF rc, out int nRadius)
        {
            int nCX = Convert.ToInt32(rc.Width / 2.0);
            int nCY = Convert.ToInt32(rc.Height / 2.0);
            nRadius = Math.Max(nCX, nCY);

            nCX += (int)rc.X;
            nCY += (int)rc.Y;

            byte[] rawPolar = new byte[360 * nRadius];

            for (int na = 0; na < 360; na++)
            {
                int nPolarY = nRadius;
                for (int nr = 0; nr < nRadius; nr++)
                {
                    double fDegree = ((na - 90.0) * Math.PI / 180.0);

                    double x = nCX + ((double)nr * Math.Cos(fDegree));
                    double y = nCY + ((double)nr * Math.Sin(fDegree));

                    // final size = -1 170620 bugfix
                    if (x < 0 || y < 0 || x >= imageW - 1 || y >= imageH - 1) { continue; }

                    int x1 = (int)Math.Floor(x);
                    int x2 = (int)Math.Ceiling(x);
                    int y1 = (int)Math.Floor(y);
                    int y2 = (int)Math.Ceiling(y);

                    int q11 = rawInput[y1 * imageW + x1];
                    int q12 = rawInput[y2 * imageW + x1];
                    int q21 = rawInput[y1 * imageW + x2];
                    int q22 = rawInput[y2 * imageW + x2];

                    double fInterplated = GetInterPolatedValue(x, y, x1, x2, y1, y2, q11, q12, q21, q22);

                    byte valueOrg = rawInput[(int)y * imageW + (int)x];

                    // if interpolation is not successful, zero value produced. 170623
                    // thus, in this case, if non interpolated value is not zero?
                    // replace error interpolated value from non interpolated original value.
                    byte value = fInterplated < 0 ? (byte)0 : fInterplated > 255 ? (byte)255 : (byte)fInterplated;

                    if (double.IsNaN(fInterplated) == true || (fInterplated == 0 && valueOrg != 0) ) value = valueOrg;

                    rawPolar[--nPolarY * 360 + na] = value;
                }
            }
            return rawPolar;
        }
        public static byte[] HC_CropImage_Rotate(byte[] rawInput, int imageW, int imageH, RectangleF rc, PointF ptGravity, float fAngle)
        {
            List<byte> listRot = new List<Byte>();

            int fromX = (int)(rc.X);
            int fromY = (int)(rc.Y);

            for (int y = fromY; y < (int) fromY+(int)rc.Height; y++)
            {
                for (int x = fromX; x < (int)fromX+(int)rc.Width; x++)
                {
                    PointF ptRot = _RotatePointByGravity(new PointF(x, y), ptGravity, fAngle);

                    if (x < 0 || y < 0 || x >= imageW - 1 || y >= imageH - 1) { continue; }

                    double cx = ptRot.X;
                    double cy = ptRot.Y;
                    int x1 = (int)Math.Floor(cx);
                    int x2 = (int)Math.Ceiling(cx);
                    int y1 = (int)Math.Floor(cy);
                    int y2 = (int)Math.Ceiling(cy);

                    int q11 = rawInput[y1*imageW+x1];
                    int q12 = rawInput[y2*imageW+x1];
                    int q21 = rawInput[y1*imageW+x2];
                    int q22 = rawInput[y2*imageW+x2];

                    double fInterplated= GetInterPolatedValue(cx, cy, x1, x2, y1, y2, q11, q12, q21, q22);

                    byte value = fInterplated < 0 ? (byte)0 : fInterplated > 255 ? (byte)255 : (byte)fInterplated;
                    listRot.Add(value);
                }
            }
            
            byte[] rawRes = new byte[(int)rc.Width*(int)rc.Height];
            byte [] rawOut = listRot.ToArray();
            return rawOut;
        }
       #endregion


        public static double[] GetAnglurarSliceArray(byte[] rawImage, int imageW, int imageH, int nRadiLength, int nRadiStart, PointF ptCenter, bool bReverse)
        {
            int nRealRadi = nRadiLength - nRadiStart;

            double[] rawAngularSlice = new double[360 * nRealRadi];

            Array.Clear(rawAngularSlice, 0, rawAngularSlice.Length);

            double fToDegree = Math.PI / 180.0;

            for (int nAngle = 0; nAngle < 360; nAngle++)
            {
                double fDegree = (((double)nAngle - 90.0) * fToDegree);

                double[] buffLine = new double[nRealRadi];
                Array.Clear(buffLine, 0, buffLine.Length);

                for (int nRadiPos = nRadiStart, nIndex = 0; nRadiPos < nRadiLength; nRadiPos++)
                {
                    double fRadi = nRadiPos;
                    double x = (double)ptCenter.X + (fRadi * Math.Cos(fDegree));
                    double y = (double)ptCenter.Y + (fRadi * Math.Sin(fDegree));

                    if (x < 0 || y < 0 || x >= imageW || y >= imageH) { continue; }

                    int x1 = (int)Math.Floor(x);
                    int x2 = (int)Math.Ceiling(x);
                    int y1 = (int)Math.Floor(y);
                    int y2 = (int)Math.Ceiling(y);

                    int q11 = rawImage[y1 * imageW + x1];
                    int q12 = rawImage[y2 * imageW + x1];
                    int q21 = rawImage[y1 * imageW + x2];
                    int q22 = rawImage[y2 * imageW + x2];

                    double fInterplated = Computer.GetInterPolatedValue(x, y, x1, x2, y1, y2, q11, q12, q21, q22);

                    if (fInterplated == 0)
                    {
                       fInterplated = rawImage[(int)y * imageW + (int)x];
                    }
                    buffLine[nIndex++] = fInterplated;
                }

                if (bReverse == true)
                {
                    Array.Reverse(buffLine);
                }

                Array.Copy(buffLine, 0, rawAngularSlice, nAngle * nRealRadi, nRealRadi);
            }

            return rawAngularSlice;
        }
        public static int[] GetAngularProjection_Horizontal(byte[] rawImage, int imageW, int imageH) // 170417 for circle distortion checking.
        {
            int[] proj = new int[imageW];

            Parallel.For(0, 360, x =>
            {
                for (int y = 0; y < imageH; y++)
                {
                    proj[x] += rawImage[y * imageW + x];
                }
            });
            return proj;
        }
        public static double GetInterPolatedValue(double cx, double cy, double x1, double x2, double y1, double y2, double q11, double q12, double q21, double q22)
        {
            double r1 = (((x2 - cx) / (x2 - x1)) * q11) + (((cx - x1) / (x2 - x1)) * q21);
            double r2 = (((x2 - cx) / (x2 - x1)) * q12) + (((cx - x1) / (x2 - x1)) * q22);
            double pvalue = (((y2 - cy) / (y2 - y1)) * r1) + (((cy - y1) / (y2 - y1)) * r2);

            if (double.IsNaN(pvalue) == true) pvalue = 0;

            return pvalue;
        }

        // Get the Filtered Points according to the damage tolerance 170921
        public static List<PointF> GetFilterdedCircleEdgesByDamageTolderance(byte[] raImage, int imageW, int imageH, RectangleF rc, List<PointF> listEdges, double fDMG_Tol)
        {
            PointF ptCenter = CRect.GetCenter(rc);
            byte[] rawPoloar = Computer.HC_CropImage_Interpolated_Polar(raImage, imageW, (int)imageH, rc, ptCenter);

            int[] arrProj = Computer.GetAngularProjection_Horizontal(rawPoloar, 360, rawPoloar.Length / 360);

            int nMinValue = arrProj.Min();
            int nMaxValue = arrProj.Max();

            int nThreshold = nMinValue + Convert.ToInt32((nMaxValue - nMinValue) * fDMG_Tol);

            List<PointF> tempFiltered = new List<PointF>();

            for (int nAngle = 0; nAngle < 360; nAngle++)
            {
                PointF ptAngle = listEdges.ElementAt(nAngle);

                if (arrProj[nAngle] > nThreshold && CPoint.IsValid(ptAngle))
                {
                    tempFiltered.Add(ptAngle);
                }
            }
            return tempFiltered;
        }

        // get the filtered points according to the damage tolerance iteratively. Find the minimum variation Tolerance Value 170921
        public static List<PointF> GetIterativeCircleDiaByDmgTolerance(byte[] rawImage, int imageW, int imageH, RectangleF rc, List<PointF> listEdges)
        {
            double fRadius = 0;
            PointF ptCenter = new PointF();

            double[] arrDmgTol = { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8 };
            double[] arrRadi = new double[9];
            double[] arrDist = new double[9];

            for (int nLoop_DmgTol = 0; nLoop_DmgTol < arrDmgTol.Length; nLoop_DmgTol++)
            {
                List<PointF> list = GetFilterdedCircleEdgesByDamageTolderance(rawImage, imageW, imageH, rc, listEdges, arrDmgTol[nLoop_DmgTol]);

                Computer.HC_FIT_Circle(list, ref ptCenter, ref fRadius);

                arrRadi[nLoop_DmgTol] = fRadius * 2;
            }

            arrDist[0] = double.MaxValue;
            for (int i = 1; i < arrRadi.Length; i++)
            {
                arrDist[i] = Math.Abs(arrRadi[i] - arrRadi[i-1]);
            }
            
            double fminVal = arrDist.Min();
            int nMinIndex = Array.IndexOf(arrDist, fminVal);

            double fTol = nMinIndex / 10.0;

            List<PointF> listFinal = GetFilterdedCircleEdgesByDamageTolderance(rawImage, imageW, imageH, rc, listEdges, fTol);

            return listFinal;
        }

        // 블럭 average 170511 테스트용
        public static double GetMeanValue(byte[] rawImage, int imageW, int imageH, int x, int y, int nKernelSize)
        {
            List<double> listArr = new List<double>();

 
            int nKs = nKernelSize / 2;

            for (int yy = y-nKs; yy < y+nKs; yy++)
            {
                for (int xx = x-nKs; xx < x + nKs; xx++)
                {
                    int nValue = rawImage[yy * imageW + xx];
                    listArr.Add(nValue);
                }
            }

           
            
            return listArr.Average();
        }
        public static List<PointF> GenCircleContourPoints(double fRadius, PointF ptCenter)
        {
            List<PointF> listContour = new List<PointF>();

            for (int nAngle = 0; nAngle < 360; nAngle++)
            {
                double fDegree = ((nAngle - 90.0) * Math.PI / 180.0);

                double x = ptCenter.X + (fRadius * Math.Cos(fDegree));
                double y = ptCenter.Y + (fRadius * Math.Sin(fDegree));

                listContour.Add(new PointF((float)x, (float)y));
            }
            return listContour;
        }
        private static PointF _RotatePointByGravity(PointF ptTarget, PointF ptGravity, double fAngle)
        {
            //x' = (x-a) * cosR - (y-b)sinR + a
            //y' = (x-a) * sinR + (y-b)cosR + b
            fAngle = fAngle * Math.PI / 180.0;

            PointF ptRotated = new PointF(0, 0);

            ptRotated.X = (float)(((ptTarget.X - ptGravity.X) * Math.Cos(fAngle) - (ptTarget.Y - ptGravity.Y) * Math.Sin(fAngle)) + ptGravity.X);
            ptRotated.Y = (float)(((ptTarget.X - ptGravity.X) * Math.Sin(fAngle) + (ptTarget.Y - ptGravity.Y) * Math.Cos(fAngle)) + ptGravity.Y);

            return ptRotated;
        }

        #region TRANSFORM
        public static byte[] HC_TRANS_GradientImage(byte[] rawImage, int imageW, int imageH)
        {
            double[] fImage = new double[imageW * imageH];

            //for (int y = 1; y < imageH - 1; y++)
            Parallel.For(1, imageH - 1, y =>
            {
                for (int x = 1; x < imageW - 1; x++)
                {
                    double dx = (rawImage[y * imageW + x + 1] - rawImage[y * imageW + x - 1]) * (rawImage[y * imageW + x + 1] - rawImage[y * imageW + x - 1]);
                    double dy = (rawImage[(y + 1) * imageW + x] - rawImage[(y - 1) * imageW + x]) * (rawImage[(y + 1) * imageW + x] - rawImage[(y - 1) * imageW + x]);
                    double fValue = Math.Sqrt(dx + dy);//Math.Sqrt(dx + dy);

                    if (double.IsNaN(fValue)) fValue = 0;

                    fImage[y * imageW + x] = fValue;
                }
            });
            return HC_CONV_GetNormalizedImage(fImage);
        }
        public static byte[] HC_TRANS_Brightness(byte[] rawImage, int imageW, int imageH, int nIncrement)
        {
            byte[] newRaw = new byte[imageW * imageH];

            Parallel.For(0, rawImage.Length, i =>
            {
                int nValue = rawImage[i] + nIncrement;
                nValue = nValue > 255 ? 255 : nValue < 0 ? 0 : nValue;
                newRaw[i] = (byte)nValue;
            });

            return newRaw;
        }
        public static byte[] HC_TRANS_Contrast(byte[] rawImage, int imageW, int imageH, int nValue)
        {
            double fContrastLevel = Math.Pow((100 + nValue) / 100.0, 2.0);

            byte[] newRes = new byte[imageW * imageH];

            //for (int i = 0; i < newRes.Length; i++)
            Parallel.For(0, newRes.Length, i =>
            {
                double newValue = (((((rawImage[i] / 255.0) - 0.5) * fContrastLevel) + 0.5) * 255.0);
                newRes[i] = newValue > 255 ? (byte)255 : newValue > 0 ? (byte)newValue : (byte)0;
            });

            return newRes;
        }
        public static byte[] HC_TRANS_Reverse(byte[] rawImage, int imageW, int imageH)
        {
            byte[] newRaw = new byte[imageW * imageH];
            Parallel.For(0, imageW, x => { for (int y = 0; y < imageH; y++) { newRaw[y * imageW + x] = (byte)(255 - rawImage[y * imageW + x]); } });
            return newRaw;
        }

        #endregion

        #region ARITHMATIC

        public static byte[] HC_ARITH_SUB(byte[] rawImage1, byte[] rawImage2, int imageW, int imageH)
        {
            byte[] newImage = new byte[imageW * imageH];

            for (int i = 0; i < imageW * imageH; i++)
            {
                int nValue = rawImage1[i] - rawImage2[i];
                newImage[i] = nValue < 0 ? (byte)0 : (byte)nValue;
            }
            return newImage;
        }

        #endregion

        #region IMAGE CONVERSION
        public static Bitmap/***/HC_CONV_Byte2Bmp(byte[] rawImage, int imageW, int imageH)
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
        public static double[]/**/HC_CONV_Byte2Double(byte[] byteArray)
        {
            double[] fArray = new double[byteArray.Length];

            if (fArray.Length == byteArray.Length)
            {
                Parallel.For(0, fArray.Length, i =>
                {
                    fArray[i] = byteArray[i];
                });
            }

            return fArray;
        }
        public static byte[] /**/HC_CONV_Double2Byte(double[] fArray)
        {
            byte[] rawImage = new byte[fArray.Length];

            Parallel.For(0, rawImage.Length, i =>
            {
                double fValue = fArray[i];
                fValue = fValue < 0x0 ? 0x0 : fValue > 0xff ? 0xff : fValue;
                rawImage[i] = (byte)fValue;

            });

            return rawImage;
        }
        public static byte[] /**/HC_CONV_Bmp2Raw(System.Drawing.Bitmap bmpImage, ref int imageW, ref int imageH)
        {
            imageW = bmpImage.Width;
            imageH = bmpImage.Height;

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

            System.Threading.Tasks.Parallel.For(0, imageH, y =>
            {
                for (int x = 0; x < nImageW; x++)
                {
                    rawImage[y * nImageW + x] = (byte)((rawBmp[(y * nStride) + (x * 3) + 0] + rawBmp[(y * nStride) + (x * 3) + 1] + rawBmp[(y * nStride) + (x * 3) + 2]) / 3);
                }
            });
            return rawImage;
        }
        public static byte[] /**/HC_CONV_BlendedImage(byte[] i1, byte[] i2, int imageW, int imageH, int nBlend)
        {
            byte[] returnRaw = new byte[imageW * imageH];

            //for (int i = 0; i < returnRaw.Length; i++)
            Parallel.For(0, returnRaw.Length, i =>
            {
                double fValue = (i1[i] * ((100 - nBlend) / 100.0)) + (i2[i] * (nBlend / 100.0));
                returnRaw[i] = fValue >= 255 ? (byte)255 : fValue < 0 ? (byte)0 : Convert.ToByte(fValue);

            });

            return returnRaw;
        }
        

        public static byte[] /*****/HC_CONV_GetNormalizedImage(double[] fImage)
        {
            double MIN = fImage.Min();
            double MAX = fImage.Max();

            if (double.IsNaN(MIN))
                MIN = 0;

            double RANGE = MAX - MIN;

            byte[] rawImage = new byte[fImage.Length];

            Parallel.For(0, fImage.Length, i =>
            {
                double fValue = ((fImage[i] - MIN) / (RANGE)) * 255.0;

                fValue = double.IsNaN(fValue) == true ? 0 : Math.Floor(fValue);

                rawImage[i] = (byte)fValue;
            });
            return rawImage;
        }
        public static void /***/HC_HISTO_Normalization(int[] nArray, int nLength, int nNormalizationValue)
        {
            try
            {
                // Fucking Normalization
                int Max = 1;

                for (int nIndex = 0; nIndex < nArray.Length; nIndex++)
                {
                    double fValue = nArray[nIndex];
                    if (fValue > Max) Max = (int)fValue;
                }

                for (int nIndex = 0; nIndex < nArray.Length; nIndex++)
                {
                    double fValue = nArray[nIndex] * nNormalizationValue;
                    nArray[nIndex] = (int)fValue;

                    // i want to avoid divide by zero
                    if (nArray[nIndex] != 0) nArray[nIndex] /= Max;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        public static int[] /******/HC_HISTO_GetHistogram(byte[] array, int nNormalization)
        {
            int[] nHistogram = new int[256];
            for (int i = 0; i < array.Length; i++)
            {
                nHistogram[array[i]]++;
            }
            if (nNormalization > 0)
            {
                HC_HISTO_Normalization(nHistogram, array.Length, nNormalization);
            }
            return nHistogram;
        }

        public static int /******/HC_THR_Huang(int[] nHistogram)
        {
            // Implements Huang's fuzzy thresholding method 
            // Uses Shannon's entropy function (one can also use Yager's entropy function) 
            // Huang L.-K. and Wang M.-J.J. (1995) "Image Thresholding by Minimizing  
            // the Measures of Fuzziness" Pattern Recognition, 28(1): 41-51
            // M. Emre Celebi  06.15.2007

            int threshold = -1;
            int ih, it;
            int sum_pix;
            int num_pix;
            double term;
            double ent;  // entropy 
            double min_ent; // min entropy 
            double mu_x;

            /* Determine the first non-zero bin */
            int first_bin = 0;
            for (ih = 0; ih < 256; ih++)
            {
                if (nHistogram[ih] != 0)
                {
                    first_bin = ih;
                    break;
                }
            }

            /* Determine the last non-zero bin */
            int last_bin = 255;
            for (ih = 255; ih >= first_bin; ih--)
            {
                if (nHistogram[ih] != 0)
                {
                    last_bin = ih;
                    break;
                }
            }
            term = 1.0 / (double)(last_bin - first_bin);
            double[] mu_0 = new double[256];
            sum_pix = num_pix = 0;
            for (ih = first_bin; ih < 256; ih++)
            {
                sum_pix += ih * nHistogram[ih];
                num_pix += nHistogram[ih];
                /* NUM_PIX cannot be zero ! */
                mu_0[ih] = sum_pix / (double)num_pix;
            }

            double[] mu_1 = new double[256];
            sum_pix = num_pix = 0;
            for (ih = last_bin; ih > 0; ih--)
            {
                sum_pix += ih * nHistogram[ih];
                num_pix += nHistogram[ih];
                /* NUM_PIX cannot be zero ! */
                mu_1[ih - 1] = sum_pix / (double)num_pix;
            }

            /* Determine the threshold that minimizes the fuzzy entropy */
            threshold = -1;
            min_ent = Double.MaxValue;
            for (it = 0; it < 256; it++)
            {
                ent = 0.0;
                for (ih = 0; ih <= it; ih++)
                {
                    /* Equation (4) in Ref. 1 */
                    mu_x = 1.0 / (1.0 + term * Math.Abs(ih - mu_0[it]));
                    if (!((mu_x < 1e-06) || (mu_x > 0.999999)))
                    {
                        /* Equation (6) & (8) in Ref. 1 */
                        ent += nHistogram[ih] * (-mu_x * Math.Log(mu_x) - (1.0 - mu_x) * Math.Log(1.0 - mu_x));
                    }
                }

                for (ih = it + 1; ih < 256; ih++)
                {
                    /* Equation (4) in Ref. 1 */
                    mu_x = 1.0 / (1.0 + term * Math.Abs(ih - mu_1[it]));
                    if (!((mu_x < 1e-06) || (mu_x > 0.999999)))
                    {
                        /* Equation (6) & (8) in Ref. 1 */
                        ent += nHistogram[ih] * (-mu_x * Math.Log(mu_x) - (1.0 - mu_x) * Math.Log(1.0 - mu_x));
                    }
                }
                /* No need to divide by NUM_ROWS * NUM_COLS * LOG(2) ! */
                if (ent < min_ent)
                {
                    min_ent = ent;
                    threshold = it;
                }
            }
            return threshold;

        }
        public static byte[] /***/HC_THR_Huang(byte[] rawImage, int imageW, int imageH)
        {
            
            int[] histo = HC_HISTO_GetHistogram(rawImage, 255);
            int nThreshold = HC_THR_Huang(histo);
            byte[] thrImage = HC_THR_Binarization_Single(rawImage, imageW, imageH, nThreshold);
            return thrImage;
        }
        public static byte[] /***/HC_THR_Binarization_Single(byte[] rawImage, int imageW, int imageH, int nThreshold)
        {
            byte[] rawRes = new byte[imageW * imageH];
            for (int i = 0; i < imageW * imageH; i++)
            {
                rawRes[i] = rawImage[i] > nThreshold ? (byte)255 : (byte)0;
            }
            return rawRes;
        }
        public static Image/*****/HC_CONV_Raw2Color(byte[] rawImage, int imageW, int imageH)
        {
            byte[] rawImageC = new byte[imageW * imageH * 3];

            int nMin = rawImage.Min();
            int nMax = rawImage.Max();

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

            System.Threading.Tasks.Parallel.For(0, imageH, y =>
            {
                for (int x = 0; x < imageW; x++)
                {
                    byte value = rawImage[y * imageW + x];
                    byte[] c = getPseudoColorRaw(value, nMin, nMax);

                    rawBmp[(y * nStride) + (x * 3) + 0] = c[0];
                    rawBmp[(y * nStride) + (x * 3) + 1] = c[1];
                    rawBmp[(y * nStride) + (x * 3) + 2] = c[2];
                }
            });

            bitmapData = bmpImage.LockBits(new Rectangle(0, 0, imageW, imageH), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            {
                System.Runtime.InteropServices.Marshal.Copy(rawBmp, 0, bitmapData.Scan0, bmpLength);
            }
            bmpImage.UnlockBits(bitmapData);

            return bmpImage;
        }
        public static byte[] /*****/ getPseudoColorRaw(double value, double min, double max)
        {
            int r = 0;
            int g = 0;
            int b = 0;

            if (value < min + 0.25 * (max - min))
            {
                r = 0;
                g = (int)(4.0 * 255.0 * (value - min) / (max - min));
                b = 255;
            }
            else if (value < min + 0.5 * (max - min))
            {
                r = 0;
                g = 255;
                b = (int)(255.0 + 4.0 * 255.0 * (min + 0.25 * (max - min) - value) / (max - min));
            }
            else if (value < min + 0.75 * (max - min))
            {
                r = (int)(4.0 * 255.0 * (value - min - 0.5 * (max - min)) / (max - min));
                g = 255;
                b = 0;
            }
            else
            {
                r = 255;
                g = (int)(255.0 + 4.0 * 255.0 * (min + 0.75 * (max - min) - value) / (max - min));
                b = 0;
            }

            r = r < 0 ? 0 : r;
            r = r > 255 ? 255 : r;
            g = g < 0 ? 0 : g;
            g = g > 255 ? 255 : g;
            b = b < 0 ? 0 : b;
            b = b > 255 ? 255 : b;
            byte br = Convert.ToByte(r);
            byte bg = Convert.ToByte(g);
            byte bb = Convert.ToByte(b);

            //byte[] c = { br, bg, bb };
            byte[] c = { bb, bg, br };
            return c;
        }

        public static byte[] HC_FILTER_Median(byte[] rawImage, int imageW, int imageH, int nKernelSize)
        {
            int nMedian = nKernelSize * nKernelSize / 2;
            int GAP = nKernelSize / 2;

            byte[] rawRes = new byte[imageW * imageH];

            byte[] rawExpanded = (byte[])ARRAY_Padding_ALL(rawImage, imageW, imageH, GAP);
            int imageNewW = imageW + GAP * 2;
            int imageNewH = imageH + GAP * 2;


            Parallel.For(GAP, imageNewH - GAP, y =>
            {
                for (int x = GAP; x < imageNewW - GAP; x++)
                {
                    double[] arrWindow = new double[nKernelSize * nKernelSize];

                    int index = 0;
                    for (int h = -GAP; h <= GAP; h++)
                    {
                        for (int w = -GAP; w <= GAP; w++, index++)
                        {
                            arrWindow[index] = rawExpanded[(y + h) * imageNewW + (x + w)];
                        }
                    }
                    Array.Sort(arrWindow);
                    rawRes[(y - GAP) * imageW + (x - GAP)] = Convert.ToByte(arrWindow[nMedian]);
                }
            });
            return rawRes;
        }

        public static byte[] TriggerProcess(byte[] rawImage, int imageW, int imageH, List<string> listCommand)
        {
            if (listCommand.Count != 0)
            {
                for (int i = 0; i < listCommand.Count; i++)
                {
                    string strCommand = listCommand.ElementAt(i);

                    string[] parse = strCommand.Split(',');

                    string command = parse[0];
                    int/**/value = Convert.ToInt32(parse[1]);

                    /***/if (command == "BR"/*******/) { rawImage = Computer.HC_TRANS_Brightness(rawImage, imageW, imageH, value); }
                    else if (command == "CONT"/*****/) { rawImage = Computer.HC_TRANS_Contrast(rawImage, imageW, imageH, value); }
                    else if (command == "SMOOTH"/***/) { rawImage = Computer.HC_FILTER_StepSmoothing(rawImage, imageW, imageH, value); }
                    else if (command == "SHARPEN"/**/) { rawImage = Computer.HC_FILTER_Sharpening(rawImage, imageW, imageH); }
                    else if (command == "NR"/*******/) { rawImage = Computer.HC_FILTER_Median(rawImage, imageW, imageH, 3); }
                    else if (command == "MG"/*******/) { rawImage = Computer.HC_TRANS_GradientImage(rawImage, imageW, imageH); }
                }
            }

            return rawImage;
        }
        #endregion

        
        //*****************************************************************************************
        // Image Quality Related 
        //*****************************************************************************************

        public static double GetStatistics(byte[] rawImage, int imageW, int imageH, ref double min, ref double max, ref double var)
        {
            int HCount = imageH / 10;

            double[] fImage = rawImage.Select(x => (double)x).ToArray();

            min = fImage.Min();
            max = fImage.Max();
            var = max - min;
            return fImage.Average();
        }
        public static  double GetSelfNoiseRatio(byte[] rawImage, int imageW, int imageH)
        {
            double fSigma = 0;

            double fBaseValue = 0;

            for (int y = 1; y < imageH - 1; y++)
            {
                for (int x = 1; x < imageW - 1; x++)
                {
                    fBaseValue += Math.Abs((rawImage[y * imageW + x - 1] * (+1)) + (rawImage[(y - 1) * imageW + x] * (-2)) + (rawImage[y * imageW + x + 1] * (+1)) +
                                           (rawImage[y * imageW + x - 1] * (-2)) + (rawImage[(y + 0) * imageW + x] * (+4)) + (rawImage[y * imageW + x + 1] * (-2)) +
                                           (rawImage[y * imageW + x - 1] * (+1)) + (rawImage[(y + 1) * imageW + x] * (-2)) + (rawImage[y * imageW + x + 1] * (+1)));
                }
            }

            fSigma = fBaseValue * Math.Sqrt(0.5 * Math.PI) / (6 * (imageW - 2) * (imageH - 2));

            return fSigma;
        }

        //*****************************************************************************************
        // LoG-based Approaches
        //*****************************************************************************************
        #region FOR LAPLACIAN OF GAUSSIAN 2ND DERIVATIVE FUCKER
        public static double[] HC_FILTER_GetLogKernel(int nKernelSize, double fSigma, int nSign = 1)
        {
            //double[] fKernel = new double[5 * 5] 
            //{ 0,0,1,0,0,
            //  0,1,2,1,0,
            //  1,2,-16,2,1,
            //  0,1,2,1,0,
            //  0,0,1,0,0};

            double[] fKernel = null;

            if (nKernelSize == 5)
            {
                if (nSign == 1)
                {
                    fKernel = new double[] 
                    { +0,+0,+01,+0,+0,
                      +0,+1,+02,+1,+0,
                      +1,+2,-16,+2,+1,
                      +0,+1,+02,+1,+0,
                      +0,+0,+01,+0,+0
                    };
                }
                else if (nSign == -1)
                {
                    fKernel = new double[] 
                    { -0,-0,-01,-0,-0,
                      -0,-1,-02,-1,-0,
                      -1,-2,+16,-2,-1,
                      -0,-1,-02,-1,-0,
                      -0,-0,-01,-0,-0
                    };

                }
            }
            else if (nKernelSize == 9)
            {

                if (nSign == -1)
                {

                    fKernel = new double[]{
                    -0, -0, -3,  -2,  -2,  -2, -3, -0, -0,
                    -0, -2, -3,  -5,  -5,  -5, -3, -2, -0,
                    -3, -3, -5,  -3,  -0,  -3, -5, -3, -3,
                    -2, -5, -3, +12, +23, +12, -3, -5, -2,
                    -2, -5, -0, +23, +40, +23, -0, -5, -2,
                    -2, -5, -3, +12, +23, +12, -3, -5, -2,
                    -3, -3, -5,  -3,  -0,  -3, -5, -3, -3,
                    -0, -2, -3,  -5,  -5,  -5, -3, -2, -0,
                    -0, -0, -3,  -2,  -2,  -2, -3, -0, -0};
                }
                else
                {
                    fKernel = new double[]{
                    0, 0, 3,  2,  2,  2, 3, 0, 0,
                    0, 2, 3,  5,  5,  5, 3, 2, 0,
                    3, 3, 5,  3,  0,  3, 5, 3, 3,
                    2, 5, 3,-12,-23,-12, 3, 5, 2,
                    2, 5, 0,-23,-40,-23, 0, 5, 2,
                    2, 5, 3,-12,-23,-12, 3, 5, 2,
                    3, 3, 5,  3,  0,  3, 5, 3, 3,
                    0, 2, 3,  5,  5,  5, 3, 2, 0,
                    0, 0, 3,  2,  2,  2, 3, 0, 0};
                }
            }

            return fKernel;
        }

     
        
        public static List<PointF> HC_EDGE_GetRawPoints_HOR_LOG_Sign(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, int nSign)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            PointF[] buffPoints = new PointF[(int)rc.Height];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list; // 170523 rectangle out of range exception

          
            for (int x = sx; x < ex; x++)
            {
                Array.Clear(buffPoints, 0, buffPoints.Length);

                for (int y = sy, nIndex = 0; y < ey; y++)
                {
                    buffPoints[nIndex++] = new PointF(x, y);
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0,0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(x, sy + (float)fSubPosIN);
                    ptEX = new PointF(x, sy + (float)fSubPosEX);

                }
                else if (bReverse == true)
                {
                    Array.Reverse(buffPoints);
                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, +1);
                    ptIN = new PointF(x, ey - (float)fSubPosIN);
                    ptEX = new PointF(x, ey - (float)fSubPosEX);

                }

                /***/if (nSign == -1) { list.Add(ptIN); }
                else if (nSign == +1) { list.Add(ptEX); }
                else if (nSign == +0)
                {
                    double miny = Math.Min(ptIN.Y, ptEX.Y);
                    double maxy = Math.Max(ptIN.Y, ptEX.Y);
                    double midY = miny + ((maxy - miny) / 2.0);
                    PointF ptMid = new PointF(ptIN.X, (float)midY);
                    list.Add(ptMid);
                }
            }

            return list;
        }
        public static List<PointF> HC_EDGE_GetRawPoints_VER_LOG_Sign(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, int nSign)
        {
            //bool bIMRC = false;
            //
            //int nInitStatus = 0;
            //double nDiff = 0;
            //
            //
            //BEGIN:

            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

           //List<double> buffListIN = new List<double>();
           //List<double> buffListEX = new List<double>();
           //
           //if (nInitStatus == 1)
           //{
           //    // if in and ex position is confusing..
           //    // rearrange measurement position automatically,  
           //    // according to for each position. 170811
           //
           //    int nGap = (int)(nDiff / 2.0);
           //    if (nPos == 0)
           //    {
           //        sx += nGap;
           //        ex += nGap;
           //    }
           //    if (nPos == 1)
           //    {
           //        sx -= nGap;
           //        ex -= nGap;
           //    }
           //}

            PointF[] buffPoints = new PointF[(int)rc.Width];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list;   // 170523 rectangle out of range exception

            

            for (int y = sy; y < ey; y++)
            {
                Array.Clear(buffPoints, 0, buffPoints.Length);

                for (int x = sx, nIndex = 0; x < ex; x++) 
                {
                    buffPoints[nIndex++] = new PointF(x, y);
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0, 0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(sx + (float)fSubPosIN, y);
                    ptEX = new PointF(sx + (float)fSubPosEX, y);
                }
                else if (bReverse == true)
                {
                    Array.Reverse(buffPoints);

                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(ex - (float)fSubPosIN, y);
                    ptEX = new PointF(ex - (float)fSubPosEX, y);
                }
                

                /***/if (nSign == -1) { list.Add(ptIN); }
                else if (nSign == 1) { list.Add(ptEX); }
                else if (nSign == 0)
                {
                    double minx = Math.Min(ptIN.X, ptEX.X);
                    double maxx = Math.Max(ptIN.X, ptEX.X);
                    double midx = minx + ((maxx - minx) / 2.0);
                    PointF ptMid = new PointF((float)midx, ptIN.Y);
                    list.Add(ptMid);
                }
            }

            //double avgIN = buffListIN.Average();
            //double avgEX = buffListEX.Average();
            //nDiff = Math.Abs(avgIN - avgEX);
            //
            //if (nInitStatus == 0 && nDiff > 10 && bIMRC == true)
            //{
            //    nInitStatus++;
            //
            //    goto BEGIN;
            //}

            return list;
        }

        public static void HC_EDGE_GetRawPoints_HOR_LOG_MULTI_Sign(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, List<PointF> list_EX, List<PointF> list_MD, List<PointF> list_IN )
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            PointF[] buffPoints = new PointF[(int)rc.Height];

            list_EX.Clear();
            list_IN.Clear();
            list_MD.Clear();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return; // 170523 rectangle out of range exception


            for (int x = sx; x < ex; x++)
            {
                Array.Clear(buffPoints, 0, buffPoints.Length);

                for (int y = sy, nIndex = 0; y < ey; y++)
                {
                    buffPoints[nIndex++] = new PointF(x, y);
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0, 0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(x, sy + (float)fSubPosIN);
                    ptEX = new PointF(x, sy + (float)fSubPosEX);

                }
                else if (bReverse == true)
                {
                    Array.Reverse(buffPoints);
                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, +1);
                    ptIN = new PointF(x, ey - ( (float)fSubPosIN));
                    ptEX = new PointF(x, ey - ( (float)fSubPosEX));
                    //ptIN = new PointF(x, ey - (buffPoints.Length - (float)fSubPosIN));
                    //ptEX = new PointF(x, ey - (buffPoints.Length - (float)fSubPosEX));

                }

                list_IN.Add(ptIN);
                list_MD.Add(CPoint.GetMidPoint_Only_Y(ptIN, ptEX));
                list_EX.Add(ptEX);
            }
        }
        public static void HC_EDGE_GetRawPoints_VER_LOG_MULTI_Sign(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, List<PointF> list_EX, List<PointF> list_MD, List<PointF> list_IN )
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            PointF[] buffPoints = new PointF[(int)rc.Width];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return;   // 170523 rectangle out of range exception


            for (int y = sy; y < ey; y++)
            {
                Array.Clear(buffPoints, 0, buffPoints.Length);

                for (int x = sx, nIndex = 0; x < ex; x++)
                {
                    buffPoints[nIndex++] = new PointF(x, y);
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0, 0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(sx + (float)fSubPosIN, y);
                    ptEX = new PointF(sx + (float)fSubPosEX, y);
                }
                else if (bReverse == true)
                {
                    Array.Reverse(buffPoints);

                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(ex - (float)fSubPosIN, y);
                    ptEX = new PointF(ex - (float)fSubPosEX, y);
                }

                list_IN.Add(ptIN);
                list_MD.Add(CPoint.GetMidPoint_Only_X(ptIN, ptEX));
                list_EX.Add(ptEX);

            }
        }

        public static List<PointF> HC_EDGE_GetRawPoints_DIA_LOG_Sign(byte[] rawImage, int imageW, int imageH, int nbuffLength, List<PointF> listEdges, int nDir)
        {
            List<PointF> list = new List<PointF>();

            PointF[] arrPoints = new PointF[nbuffLength];

            RectangleF rcBound = CPoint.GetBoundingRect(listEdges);
            if (CRect.IsBoarderPosition(rcBound, imageW, imageH) == true) return list; // 170615 rectangle out of range exception
            
            for (int i = 0; i < listEdges.Count; i++)
            {
                int imgX = (int)listEdges.ElementAt(i).X;
                int imgY = (int)listEdges.ElementAt(i).Y;

                Array.Clear(arrPoints, 0, arrPoints.Length);
                for (int x = (int)imgX, nIndex = 0; x < (int)imgX + nbuffLength; x++)
                {
                    arrPoints[nIndex++] = new PointF(x, imgY);
                }

                double fSubPosEX = 0;
                double fSubPosIN = 0;

                if (nDir == +1)
                {
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, arrPoints, +1);
                    list.Add(new PointF(listEdges.ElementAt(i).X + (float)fSubPosEX, imgY));
                }
                else if (nDir == -1)
                {
                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, arrPoints, -1);
                    list.Add(new PointF(listEdges.ElementAt(i).X + (float)fSubPosIN, imgY));

                }
                else if (nDir == 0)
                {
                    fSubPosEX = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, arrPoints, +1);
                    fSubPosIN = Computer.HC_EDGE_GetLogPos_Sign(rawImage, imageW, imageH, arrPoints, -1);

                    PointF ptEX = new PointF(listEdges.ElementAt(i).X + (float)fSubPosEX, imgY);
                    PointF ptIN = new PointF(listEdges.ElementAt(i).X + (float)fSubPosIN, imgY);
                    PointF ptCT = CPoint.GetMidPoint(ptEX, ptIN);

                    list.Add(ptCT);
                }
            }
            return list;
        }
         
        public static double HC_EDGE_GetCARDINPos(byte[] rawImage, int imageW, int imageH, PointF[] arrPoints, int nSign)
        {
            double[] fKernel= new double[] 
            { 
                    + 1,+ 1,+ 1,+15,+ 1,+ 1,+ 1,
                    + 1,+ 1,+ 1,+15,+ 1,+ 1,+ 1,
                    + 1,+ 1,+15,+15,+15,+ 1,+ 1,
                    +15,+15,+15,+15,+15,+15,+15, 
                    + 1,+ 1,+15,+15,+15,+ 1,+ 1,
                    + 1,+ 1,+ 1,+15,+ 1,+ 1,+ 1,
                    + 1,+ 1,+ 1,+15,+ 1,+ 1,+ 1
            };

            double[] fImage = new double[arrPoints.Length];

            int KSIZE = (int)Math.Sqrt(fKernel.Length);
            int GAP = KSIZE / 2;

            //for (int i = 0; i < arrPoints.Length; i++)
            Parallel.For(0, arrPoints.Length, i =>
            {
                float x = arrPoints.ElementAt(i).X;
                float y = arrPoints.ElementAt(i).Y;

                double kernelSum = 0;

                for (int j = -GAP; j <= GAP; j++)
                {

                    for (int k = -GAP; k <= GAP; k++)
                    {
                        //   kernelSum += (fKernel[(j + GAP) * KSIZE + k + GAP] * rawImage[(y - j) * imageW + (x - k)]);

                        float dy = y - j;
                        float dx = x - k;

                        int x1 = (int)Math.Floor(dx);
                        int x2 = (int)Math.Ceiling(dx);
                        int y1 = (int)Math.Floor(dy);
                        int y2 = (int)Math.Ceiling(dy);

                        int q11 = rawImage[y1 * imageW + x1];
                        int q12 = rawImage[y2 * imageW + x1];
                        int q21 = rawImage[y1 * imageW + x2];
                        int q22 = rawImage[y2 * imageW + x2];

                        double fInterplated = GetInterPolatedValue(dx, dy, x1, x2, y1, y2, q11, q12, q21, q22);

                        byte valueOrg = rawImage[(int)y * imageW + (int)x];

                        // if interpolation is not successful, zero value produced. 170623
                        // thus, in this case, if non interpolated value is not zero?
                        // replace error interpolated value from non interpolated original value.
                        byte value = fInterplated < 0 ? (byte)0 : fInterplated > 255 ? (byte)255 : (byte)fInterplated;

                        if (double.IsNaN(fInterplated) == true || (fInterplated == 0 && valueOrg != 0)) value = valueOrg;

                        kernelSum += (fKernel[(j + GAP) * KSIZE + k + GAP] * value);
                    }
                }
                fImage[i] = kernelSum;
                //fTemp[i] = rawImage[y * imageW + x];

            });

            double [] fDerivative = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(fImage);
            double fSubPos = 0;

            try
            {
                if (nSign == -1)
                {
                    double fValue = fDerivative.Min();
                    int nPos = Array.IndexOf(fDerivative, fValue);
                    fSubPos = nPos + GetSubPixelFromLineBuff(fDerivative, nPos);
                }
                else if (nSign == +1)
                {
                    double fValue = fDerivative.Max();
                    int nPos = Array.IndexOf(fDerivative, fValue);
                    fSubPos = nPos + GetSubPixelFromLineBuff(fDerivative, nPos);
                }
            }
            catch { }

            return fSubPos;
        }

        public static List<PointF> HC_EDGE_GetRawPoints_CARDIN_HOR(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool isTop, int nSign)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            PointF[] buffPoints = new PointF[(int)rc.Height];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list; // 170523 rectangle out of range exception

            for (int x = sx; x < ex; x++)
            {
                Array.Clear(buffPoints, 0, buffPoints.Length);

                for (int y = sy, nIndex = 0; y < ey; y++)
                {
                    buffPoints[nIndex++] = new PointF(x, y);
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;
                PointF ptIN = new PointF();
                PointF ptEX = new PointF();
                if (isTop == true)
                {
                    fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(x, sy + (float)fSubPosIN);
                    ptEX = new PointF(x, sy + (float)fSubPosEX);
                }
                else if (isTop == false)
                {
                    Array.Reverse(buffPoints);
                    fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(x, ey - (float)fSubPosIN);
                    ptEX = new PointF(x, ey - (float)fSubPosEX);
                }

                /***/
                if (nSign == -1) { list.Add(ptIN); }
                else if (nSign == 1) { list.Add(ptEX); }
                else if (nSign == 0)
                {
                    double miny = Math.Min(ptIN.Y, ptEX.Y);
                    double maxy = Math.Max(ptIN.Y, ptEX.Y);
                    double midY = miny + ((maxy - miny) / 2.0);
                    PointF ptMid = new PointF(ptIN.X, (float)midY);
                    list.Add(ptMid);
                }
            }
            return list;
        }
        public static List<PointF> HC_EDGE_GetRawPoints_CARDIN_VER(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool isLFT, int nSign)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            PointF[] buffPoints = new PointF[(int)rc.Width];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list;   // 170523 rectangle out of range exception

            for (int y = sy; y < ey; y++)
            {
                Array.Clear(buffPoints, 0, buffPoints.Length);

                for (int x = sx, nIndex = 0; x < ex; x++) { buffPoints[nIndex++] = new PointF(x, y); }

                double fSubPosIN = 0;
                double fSubPosEX = 0;
                PointF ptIN = new PointF();
                PointF ptEX = new PointF();
                if (isLFT == true)
                {
                    fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(sx + (float)fSubPosIN, y);
                    ptEX = new PointF(sx + (float)fSubPosEX, y);
                }
                else if (isLFT == false)
                {
                    Array.Reverse(buffPoints);
                    fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(ex - (float)fSubPosIN, y);
                    ptEX = new PointF(ex - (float)fSubPosEX, y);
                }

                if (nSign == -1) { list.Add(ptIN); }
                else if (nSign == 1) { list.Add(ptEX); }
                else if (nSign == 0)
                {
                    double minx = Math.Min(ptIN.X, ptEX.X);
                    double maxx = Math.Max(ptIN.X, ptEX.X);
                    double midx = minx + ((maxx - minx) / 2.0);
                    PointF ptMid = new PointF((float)midx, ptIN.Y);
                    list.Add(ptMid);
                }
            }
            return list;
        }
        public static void HC_EDGE_GetRawPoints_CARDIN_MULTI_HOR(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool isTop, List<PointF> listEdges_EX, List<PointF> listEdges_MD, List<PointF> listEdges_IN)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            PointF[] buffPoints = new PointF[(int)rc.Height];

            listEdges_EX.Clear();
            listEdges_IN.Clear();
            listEdges_MD.Clear();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return; // 170523 rectangle out of range exception

            for (int x = sx; x < ex; x++)
            {
                Array.Clear(buffPoints, 0, buffPoints.Length);

                for (int y = sy, nIndex = 0; y < ey; y++)
                {
                    buffPoints[nIndex++] = new PointF(x, y);
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;
                PointF ptIN = new PointF();
                PointF ptEX = new PointF();
                if (isTop == true)
                {
                    fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(x, sy + (float)fSubPosIN);
                    ptEX = new PointF(x, sy + (float)fSubPosEX);
                }
                else if (isTop == false)
                {
                    Array.Reverse(buffPoints);
                    fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(x, ey - (float)fSubPosIN);
                    ptEX = new PointF(x, ey - (float)fSubPosEX);
                }

                listEdges_IN.Add(ptIN);
                listEdges_MD.Add(CPoint.GetMidPoint_Only_Y(ptIN, ptEX));
                listEdges_EX.Add(ptEX);
            }
        }

        // in order to display and visualize  ex, mid, in ... split version 
        public static void HC_EDGE_GetRawPoints_CARDIN_MULTI_VER(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool isLFT, List<PointF> list_EX, List<PointF> list_MD, List<PointF> list_IN)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            PointF[] buffPoints = new PointF[(int)rc.Width];

            list_EX.Clear();
            list_MD.Clear();
            list_IN.Clear();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return ;   // 170523 rectangle out of range exception

            for (int y = sy; y < ey; y++)
            {
                Array.Clear(buffPoints, 0, buffPoints.Length);

                for (int x = sx, nIndex = 0; x < ex; x++) { buffPoints[nIndex++] = new PointF(x, y); }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF();
                PointF ptEX = new PointF();

                if (isLFT == true)
                {
                    fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(sx + (float)fSubPosIN, y);
                    ptEX = new PointF(sx + (float)fSubPosEX, y);
                }
                else if (isLFT == false)
                {
                    Array.Reverse(buffPoints);
                    fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, -1);
                    fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, buffPoints, +1);

                    ptIN = new PointF(ex - (float)fSubPosIN, y);
                    ptEX = new PointF(ex - (float)fSubPosEX, y);
                }

                list_IN.Add(ptIN);
                list_MD.Add(CPoint.GetMidPoint_Only_X(ptIN, ptEX));
                list_EX.Add(ptEX);
            }
        }
        public static List<PointF> HC_EDGE_GetRawPoints_CARDIN_DIA(byte[] rawImage, int imageW, int imageH, int nbuffLength, List<PointF> listEdges, int nDir, bool bReverse)
        {
            List<PointF> list = new List<PointF>();

            PointF[] arrPoints = new PointF[nbuffLength];

            for (int i = 0; i < listEdges.Count; i++)
            {
                int imgX = (int)listEdges.ElementAt(i).X;
                int imgY = (int)listEdges.ElementAt(i).Y;

                Array.Clear(arrPoints, 0, arrPoints.Length);
                for (int x = (int)imgX, nIndex = 0; x < (int)imgX + nbuffLength; x++)
                {
                    arrPoints[nIndex++] = new PointF(x, imgY);
                }

                double fSubPosEX = 0;
                double fSubPosIN = 0;

                fSubPosEX = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, arrPoints, +1);
                fSubPosIN = Computer.HC_EDGE_GetCARDINPos(rawImage, imageW, imageH, arrPoints, -1);

                if (nDir == +1)
                {
                    list.Add(new PointF(listEdges.ElementAt(i).X + (float)fSubPosEX, imgY));
                }
                else if (nDir == -1)
                {
                    list.Add(new PointF(listEdges.ElementAt(i).X + (float)fSubPosIN, imgY));
                }
                else if (nDir == 0)
                {
                    PointF ptEX = new PointF(listEdges.ElementAt(i).X + (float)fSubPosEX, imgY);
                    PointF ptIN = new PointF(listEdges.ElementAt(i).X + (float)fSubPosIN, imgY);
                    PointF ptCT = CPoint.GetMidPoint(ptEX, ptIN);

                    list.Add(ptCT);
                }
            }
            return list;
        }

        public static double HC_EDGE_GetLogPos_Sign(byte[] rawImage, int imageW, int imageH, PointF[] arrPoints, int nSign)
        {
            bool bUSE_OLD = true;
            double fSubPos = 0;

            if (arrPoints.Length == 0) return fSubPos;

            if( bUSE_OLD == true)
            {
              #region oldversion
                double[] fKernel = HC_FILTER_GetLogKernel(5, 1.6, nSign);

                double[] fImage = new double[arrPoints.Length];
                double[] fTemp = new double[arrPoints.Length];

                int KSIZE = (int)Math.Sqrt(fKernel.Length);
                int GAP = KSIZE / 2;

                for (int i = 0; i < arrPoints.Length; i++)
                {
                    int x = (int)arrPoints.ElementAt(i).X;
                    int y = (int)arrPoints.ElementAt(i).Y;

                    double kernelSum = 0;
                    for (int j = -GAP; j <= GAP; j++)
                    {
                        for (int k = -GAP; k <= GAP; k++)
                        {
                            kernelSum += (fKernel[(j + GAP) * KSIZE + k + GAP] * rawImage[(y - j) * imageW + (x - k)]);
                        }
                    }
                    fImage[i] = kernelSum;
                    fTemp[i] = rawImage[y * imageW + x];
                }

                double fValue = fImage.Max();
                int nPos = Array.IndexOf(fImage, fValue);
                fSubPos = nPos + GetSubPixelFromLineBuff(fImage, nPos);
                #endregion
            }
            else if (bUSE_OLD == false)
            {
                #region new version
                double[] fKernel = HC_FILTER_GetLogKernel(5, 1.6, nSign);
                
                double[] fImage = new double[arrPoints.Length];
                
                int KSIZE = (int)Math.Sqrt(fKernel.Length);
                int GAP = KSIZE / 2;
                
                for (int i = 0; i < arrPoints.Length; i++)
                {
                    float x = arrPoints.ElementAt(i).X;
                    float y = arrPoints.ElementAt(i).Y;
                
                    double kernelSum = 0;
                
                    for (int j = -GAP; j <= GAP; j++)
                    {
                        for (int k = -GAP; k <= GAP; k++)
                        {
                            float dy = y - j;
                            float dx = x - k;
                
                            int x1 = (int)Math.Floor(dx);
                            int x2 = (int)Math.Ceiling(dx);
                            int y1 = (int)Math.Floor(dy);
                            int y2 = (int)Math.Ceiling(dy);
                            
                            int q11 = rawImage[y1 * imageW + x1];
                            int q12 = rawImage[y2 * imageW + x1];
                            int q21 = rawImage[y1 * imageW + x2];
                            int q22 = rawImage[y2 * imageW + x2];
                            
                            double fInterplated = GetInterPolatedValue(dx, dy, x1, x2, y1, y2, q11, q12, q21, q22);
                
                            byte valueOrg = rawImage[(int)y * imageW + (int)x];
                
                            // if interpolation is not successful, zero value produced. 170623
                            // thus, in this case, if non interpolated value is not zero?
                            // replace error interpolated value from non interpolated original value.
                            byte value = fInterplated < 0 ? (byte)0 : fInterplated > 255 ? (byte)255 : (byte)fInterplated;
                            
                            if (double.IsNaN(fInterplated) == true || (fInterplated == 0 && valueOrg != 0)) value = valueOrg;
                
                            kernelSum += (fKernel[(j + GAP) * KSIZE + k + GAP] * value);
                        }
                    }
                    fImage[i] = kernelSum;
                }
                
                double fValue = fImage.Max();
                int nPos = Array.IndexOf(fImage, fValue);
                fSubPos = nPos + GetSubPixelFromLineBuff(fImage, nPos);
                #endregion
            }
            return fSubPos;
         }
        public static double HC_EDGE_GetLoG_PosMax(byte[] rawImage, int imageW, int imageH, PointF[] list)
        {
            double[] arrDerivative = new double[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                int xx = (int)list.ElementAt(i).X;
                int yy = (int)list.ElementAt(i).Y;

                arrDerivative[i] = rawImage[yy * imageW + xx + 1] + rawImage[yy * imageW + xx - 1] - (2 * rawImage[yy * imageW + xx]);
            }

            double fMaxVal = arrDerivative.Max();
            int nMaxPos = Array.IndexOf(arrDerivative, fMaxVal);

            double fSubPixel = GetSubPixelFromLineBuff(arrDerivative, nMaxPos);

            return nMaxPos + fSubPixel;
        }
        public static double HC_EDGE_GetLoG_PosMin(byte[] rawImage, int imageW, int imageH, PointF[] list)
        {
            double[] arrDerivative = new double[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                int xx = (int)list.ElementAt(i).X;
                int yy = (int)list.ElementAt(i).Y;

                arrDerivative[i] = rawImage[(yy + 0) * imageW + xx + 1] +
                                   rawImage[(yy + 0) * imageW + xx - 1] +
                                   rawImage[(yy + 1) * imageW + xx + 0] +
                                   rawImage[(yy - 1) * imageW + xx + 0] -
                                   (4 * rawImage[yy * imageW + xx]);
            }

            double fMin = arrDerivative.Min();
            int nMinPos = Array.IndexOf(arrDerivative, fMin);

            double fSubpixel = GetSubPixelFromLineBuff(arrDerivative, nMinPos);

            return nMinPos + fSubpixel;
        }
        #endregion

        //*******************************************************************************************
        // METHOD Serise

        public static byte[] HC_TRANS_MAGMEDI(byte[] rawImage, int imageW, int imageH)
        {
            byte[] rawDump = new byte[imageW*imageH];
            Array.Copy( rawImage, rawDump, rawImage.Length);

            rawDump = HC_TRANS_GradientImage(rawImage, imageW, imageH);
            rawDump = HC_TRANS_Contrast(rawImage, imageW, imageH, 30);
            rawDump = HC_FILTER_Median(rawImage, imageW, imageH, 5);
            return rawDump;
        }
        public static RectangleF HC_CIRCLE_CENTERING(byte[] rawImage, int imageW, int imageH, RectangleF rc, double fShrinkage, int nOption)
        {
            // get the rectangle info as integer
            int rcx = (int)rc.X;
            int rcy = (int)rc.Y;
            int rcw = (int)rc.Width;
            int rch = (int)rc.Height;

            bool bSave = false;

            // get the crop image 
            byte[] processedImage = HC_CropImage(rawImage, imageW, imageH, rcx, rcy, rcw, rch);

            if (nOption == 1)
            {
                processedImage = HC_TRANS_SIGMOID_Contrast(processedImage, rcw, rch, 0.4, 20);
            }
            else if (nOption == 2)
            {
                byte[] reverse = HC_TRANS_Reverse(processedImage, rcw, rch);
                byte[] sub = HC_ARITH_SUB(reverse, processedImage, rcw, rch);
                sub = HC_ARITH_SUB(sub, processedImage, rcw, rch);
                Array.Copy(sub, processedImage, sub.Length);
            }

            // generate threshold image
            processedImage = HC_THR_Huang(processedImage, rcw, rch);

            if( bSave == true) SaveImage(processedImage, rcw, rch, "c:\\nicetomeetyou2.bmp");

            CLabelWorker label = new CLabelWorker();
            // set labeling map
            label.Set(processedImage, rcw, rch);

            int cx = rcw / 2;
            int cy = rch / 2;

            // get the center value from the threshold image
            int value = processedImage[cy * rcw+ cx];

            // extract center oriented segment 
            List<Point> list = label.ExtractSingleSegment(processedImage, rcw, rch, cx, cy, value);

            CSegment seg = new CSegment(list, 1);

            // convert croped-segment croodinate to the real image croodinate 
            RectangleF rcCenter = seg.RC;
            rcCenter.X = seg.ptCenter.X;
            rcCenter.Y = seg.ptCenter.Y;

            rcCenter.Offset(rc.X, rc.Y);

            double rcFinalW = rcCenter.Width;
            double rcFinalH = rcCenter.Height;

            rcCenter.Width  = Convert.ToSingle(rcCenter.Width * (1.0 - fShrinkage));
            rcCenter.Height = Convert.ToSingle(rcCenter.Height * (1.0 - fShrinkage));
            

            return rcCenter;
        }
     
        //*****************************************************************************************
        // 2nd Derivative approaches
        //*****************************************************************************************

        #region FOR 2nd DERIVATIVE APPROACH

        public static double[] HC_EDGE_Get2ndDerivativeArrayFromLineBuff(double[] fLineBuff)
        {
            // fucking error exception 170901 
            if (fLineBuff == null || fLineBuff.Length < 2) return new double[2];

            double[] arr1st = new double[fLineBuff.Length - 1];
            double[] arr2nd = new double[fLineBuff.Length - 2];

            try
            {
                for (int i = 0; i < fLineBuff.Length - 1; i++)
                {
                    arr1st[i] = fLineBuff[i + 1] - fLineBuff[i];
                }
                for (int i = 0; i < arr1st.Length - 1 + 0; i++)
                {
                    arr2nd[i] = arr1st[i + 1] - arr1st[i];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            

            
            return arr2nd;
        }

        // get the every raw points based on directional 2nd derivative 170419 
        // this functions prepared for the point filtering or fitting which has outliers.
        public static List<PointF> GetPointList_Derivative_HOR(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bTarget_TOP, int nDir)
        {

            List<PointF> list = new List<PointF>();

            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            double fSubPixel = 0.0;

            double[] buff_Org = new double[(int)rc.Height + 2];

            if (bTarget_TOP == true)
            {
                #region FOR TOP REGION : IN-RISE & IN-FALL
                if (nDir == DIR_INFALL || nDir == DIR_INRISE)
                {
                    for (int x = sx; x < ex; x++)
                    {
                        Array.Clear(buff_Org, 0, buff_Org.Length);
                        for (int y = sy, nIndex = 0; y < ey + 2; y++)
                        {
                            buff_Org[nIndex++] = rawImage[y * imageW + x];
                        }
                        double[] buff_2nd = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(buff_Org);


                        if (nDir == DIR_INFALL)
                        {
                            int maxPos = Computer.GetMaxElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, maxPos);

                            list.Add(new PointF(x, (float)(sy + maxPos + fSubPixel)));
                        }
                        else if (nDir == DIR_INRISE)
                        {
                            int minPos = Computer.GetMinElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, minPos);

                            list.Add(new PointF(x, (float)(sy + minPos + fSubPixel)));
                        }
                    }
                }
                #endregion

                #region FOR TOP REGION : IN-RISE AND IN-FALL

                else if (nDir == DIR_EXFALL || nDir == DIR_EXRISE)
                {
                    for (int x = sx; x < ex; x++)
                    {
                        Array.Clear(buff_Org, 0, buff_Org.Length);
                        for (int y = ey - 1, nIndex = 0; y >= sy - 2; y--)
                        {
                            buff_Org[nIndex++] = rawImage[y * imageW + x];
                        }
                        double[] buff_Top_2nd = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(buff_Org);

                        if (nDir == DIR_EXFALL)
                        {
                            int maxPos = Computer.GetMaxElementPosition(buff_Top_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_Top_2nd, maxPos);

                            list.Add(new PointF(x, (float)(ey - maxPos - fSubPixel)));
                        }
                        else if (nDir == DIR_EXRISE)
                        {
                            int minPos = Computer.GetMinElementPosition(buff_Top_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_Top_2nd, minPos);
                            list.Add(new PointF(x, (float)(ey - minPos - fSubPixel)));
                        }
                    }
                }
                #endregion

            }
            else if (bTarget_TOP == false)
            {
                #region FOR BTM REGION : IN-RISE AND IN-FALL
                if (nDir == DIR_INFALL || nDir == DIR_INRISE)
                {
                    for (int x = sx; x < ex; x++)
                    {
                        Array.Clear(buff_Org, 0, buff_Org.Length);
                        for (int y = ey - 1, nIndex = 0; y >= sy - 2; y--)
                        {
                            buff_Org[nIndex++] = rawImage[y * imageW + x];
                        }
                        double[] buff_2nd = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(buff_Org);

                        if (nDir == DIR_INFALL)
                        {
                            int minPos = Computer.GetMaxElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, minPos);

                            list.Add(new PointF(x, (float)(ey - minPos - fSubPixel)));

                        }
                        else if (nDir == DIR_INRISE)
                        {
                            int maxPos = Computer.GetMinElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, maxPos);

                            list.Add(new PointF(x, (float)(ey - maxPos - fSubPixel)));
                        }
                    }
                }
                #endregion

                #region FOR BTM REGION : EX-RISE AND EX-FALL
                else if (nDir == DIR_EXFALL || nDir == DIR_EXRISE)
                {
                    for (int x = sx; x < ex; x++)
                    {
                        Array.Clear(buff_Org, 0, buff_Org.Length);
                        for (int y = sy, nIndex = 0; y < ey + 2; y++)
                        {
                            buff_Org[nIndex++] = rawImage[y * imageW + x];
                        }
                        double[] buff_2nd = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(buff_Org);

                        if (nDir == DIR_EXFALL)
                        {
                            int minPos = Computer.GetMaxElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, minPos);
                            list.Add(new PointF(x, (float)(sy + minPos + fSubPixel)));

                        }
                        else if (nDir == DIR_EXRISE)
                        {
                            int maxPos = Computer.GetMinElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, maxPos);
                            list.Add(new PointF(x, (float)(sy + maxPos + fSubPixel)));
                        }
                    }
                }
                #endregion
            }

            return list;
        }
        public static List<PointF> GetPointList_Derivative_VER(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bTarget_LFT, int nDir)
        {
            List<PointF> list = new List<PointF>();

            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            double fSubPixel = 0.0;

            double[] buff_Org = new double[(int)rc.Width + 2];

            if (bTarget_LFT == true)
            {
                #region FOR TOP REGION : IN-RISE & IN-FALL
                if (nDir == DIR_INFALL || nDir == DIR_INRISE)
                {
                    for (int y = sy; y < ey; y++)
                    {
                        Array.Clear(buff_Org, 0, buff_Org.Length);
                        for (int x = sx, nIndex = 0; x < ex + 2; x++)
                        {
                            buff_Org[nIndex++] = rawImage[y * imageW + x];
                        }
                        double[] buff_2nd = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(buff_Org);

                        if (nDir == DIR_INFALL)
                        {
                            int maxPos = Computer.GetMaxElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, maxPos);
                            list.Add(new PointF((float)(sx + maxPos + fSubPixel), y));
                        }
                        else if (nDir == DIR_INRISE)
                        {
                            int minPos = Computer.GetMinElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, minPos);
                            list.Add(new PointF((float)(sx + minPos + fSubPixel), y));
                        }
                    }
                }
                #endregion

                #region FOR TOP REGION : IN-RISE AND IN-FALL
                else if (nDir == DIR_EXFALL || nDir == DIR_EXRISE)
                {
                    for (int y = sy; y < ey; y++)
                    {
                        Array.Clear(buff_Org, 0, buff_Org.Length);
                        for (int x = ex - 1, nIndex = 0; x >= sx - 2; x--)
                        {
                            buff_Org[nIndex++] = rawImage[y * imageW + x];
                        }
                        double[] buff_2nd = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(buff_Org);

                        if (nDir == DIR_EXFALL)
                        {
                            int maxPos = Computer.GetMaxElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, maxPos);
                            list.Add(new PointF((float)(ex - maxPos - fSubPixel), y));
                        }
                        else if (nDir == DIR_EXRISE)
                        {
                            int minPos = Computer.GetMinElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, minPos);
                            list.Add(new PointF((float)(ex - minPos - fSubPixel), y));
                        }
                    }
                }
                #endregion
            }
            else if (bTarget_LFT == false)
            {
                #region FOR BTM REGION : IN-RISE AND IN-FALL

                if (nDir == DIR_INFALL || nDir == DIR_INRISE)
                {
                    for (int y = sy; y < ey; y++)
                    {
                        Array.Clear(buff_Org, 0, buff_Org.Length);
                        for (int x = ex - 1, nIndex = 0; x >= sx - 2; x--)
                        {
                            buff_Org[nIndex++] = rawImage[y * imageW + x];
                        }
                        double[] buff_RHT_2nd = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(buff_Org);

                        if (nDir == DIR_INFALL)
                        {
                            int maxPos = Computer.GetMaxElementPosition(buff_RHT_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_RHT_2nd, maxPos);
                            list.Add(new PointF((float)(ex - maxPos - fSubPixel), y));
                        }
                        else if (nDir == DIR_INRISE)
                        {
                            int minPos = Computer.GetMinElementPosition(buff_RHT_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_RHT_2nd, minPos);
                            list.Add(new PointF((float)(ex - minPos - fSubPixel), y));
                        }
                    }
                }
                #endregion

                #region FOR BTM REGION : EX-RISE AND EX-FALL
                else if (nDir == DIR_EXFALL || nDir == DIR_EXRISE)
                {
                    for (int y = sy; y < ey; y++)
                    {
                        Array.Clear(buff_Org, 0, buff_Org.Length);
                        for (int x = sx, nIndex = 0; x < ex + 2; x++)
                        {
                            buff_Org[nIndex++] = rawImage[y * imageW + x];
                        }
                        double[] buff_2nd = Computer.HC_EDGE_Get2ndDerivativeArrayFromLineBuff(buff_Org);
                        if (nDir == DIR_EXFALL)
                        {
                            int maxPos = Computer.GetMaxElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, maxPos);
                            list.Add(new PointF((float)(sx + maxPos + fSubPixel), y));
                        }
                        else if (nDir == DIR_EXRISE)
                        {
                            int minPos = Computer.GetMinElementPosition(buff_2nd);
                            fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, minPos);

                            list.Add(new PointF((float)(sx + minPos + fSubPixel), y));
                        }
                    }
                }
                #endregion
            }
            return list;
        }

        public static double HC_EDGE_Get2ndDerivLine_PosMax(byte[] line)
        {
            double[] buff_1st = new double[line.Length - 1];
            double[] buff_2nd = new double[line.Length - 2];

            for (int nIndex = 0; nIndex < line.Length - 1; nIndex++) { buff_1st[nIndex] = line[nIndex + 1] - line[nIndex]; }
            for (int nIndex = 0; nIndex < line.Length - 2; nIndex++) { buff_2nd[nIndex] = buff_1st[nIndex + 1] - buff_1st[nIndex]; }

            double fMax = buff_2nd.Max();
            int/**/nPos = Array.IndexOf(buff_2nd, fMax);

            double fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, nPos);

            return fSubPixel + nPos;
        }
        public static double HC_EDGE_Get2ndDerivLine_PosMin(byte[] line)
        {
            double[] buff_1st = new double[line.Length - 1];
            double[] buff_2nd = new double[line.Length - 2];

            for (int nIndex = 0; nIndex < line.Length - 1; nIndex++) { buff_1st[nIndex] = line[nIndex + 1] - line[nIndex]; }
            for (int nIndex = 0; nIndex < line.Length - 2; nIndex++) { buff_2nd[nIndex] = buff_1st[nIndex + 1] - buff_1st[nIndex]; }

            double fMin = buff_2nd.Min();
            int/**/nPos = Array.IndexOf(buff_2nd, fMin);

            double fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, nPos);

            return fSubPixel + nPos;
        }

        public static double HC_EDGE_Get2ndDerivLine_PosMax(double[] line)
        {
            if (line.Length <= 2) return 0;

            double[] buff_1st = new double[line.Length - 1];
            double[] buff_2nd = new double[line.Length - 2];

            for (int nIndex = 0; nIndex < line.Length - 1; nIndex++) { buff_1st[nIndex] = line[nIndex + 1] - line[nIndex]; }
            for (int nIndex = 0; nIndex < line.Length - 2; nIndex++) { buff_2nd[nIndex] = buff_1st[nIndex + 1] - buff_1st[nIndex]; }

            double fMax = buff_2nd.Max();
            int/**/nPos = Array.IndexOf(buff_2nd, fMax);

            double fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, nPos);

            return fSubPixel + nPos;
        }
        public static double HC_EDGE_Get2ndDerivLine_PosMin(double[] line)
        {
            if (line.Length <= 2) return 0;

            double[] buff_1st = new double[line.Length - 1];
            double[] buff_2nd = new double[line.Length - 2];

            for (int nIndex = 0; nIndex < line.Length - 1; nIndex++) { buff_1st[nIndex] = line[nIndex + 1] - line[nIndex]; }
            for (int nIndex = 0; nIndex < line.Length - 2; nIndex++) { buff_2nd[nIndex] = buff_1st[nIndex + 1] - buff_1st[nIndex]; }

            double fMin = buff_2nd.Min();
            int/**/nPos = Array.IndexOf(buff_2nd, fMin);

            double fSubPixel = Computer.GetSubPixelFromLineBuff(buff_2nd, nPos);

            return fSubPixel + nPos;
        }
        public static void HC_EDGE_PREWITT_SUBPIXEL(double[] buffLine, ref double fMaxPos, ref double fMinPos )
        {
            double [] profile = new double[buffLine.Length];

            for (int nIndex = 1; nIndex < buffLine.Length-1; nIndex++)
            {
                profile[nIndex] += buffLine[(nIndex + 1)] - buffLine[(nIndex - 1)];
            }
            profile[0] = profile[1];
            profile[profile.Length - 1] = profile[profile.Length - 2];

            double fMaxValue = profile.Max();
            double fMinValue = profile.Min();
            int nMaxValuePos = 0;
            int nMinValuePos = 0;

            nMaxValuePos = Array.IndexOf(profile, fMaxValue);
            nMinValuePos = Array.IndexOf(profile, fMinValue);

            double fSubPixelMax = 0;
            double fSubPixelMin = 0;
            
            fSubPixelMax = Computer.GetSubPixelFromLineBuff(profile, nMaxValuePos);
            fSubPixelMin = Computer.GetSubPixelFromLineBuff(profile, nMinValuePos);

            fMaxPos = nMaxValuePos + fSubPixelMax;
            fMinPos = nMinValuePos + fSubPixelMin;
        }

        public static List<PointF> HC_EDGE_GetRawPoints_2ndDeriv_HOR( byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, int nDir)
        {
             // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            double[] buffLine = new double[(int)rc.Width];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list; // 170523 rectangle out of range exception

            for (int y = sy; y < ey; y++)
            {
                Array.Clear(buffLine, 0, buffLine.Length);

                for (int x = sx, nIndex = 0; x < ex; x++)
                {
                    buffLine[nIndex++] = rawImage[y*imageW+x];
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0,0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);

                    ptIN = new PointF(sx + (float)fSubPosIN, y);
                    ptEX = new PointF(sx + (float)fSubPosEX, y);
                }
                else if( bReverse == true)
                {
                    Array.Reverse(buffLine);
                    fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);

                    ptIN = new PointF(ex - (float)fSubPosIN, y);
                    ptEX = new PointF(ex - (float)fSubPosEX, y);
                }

                /***/if (nDir == -1) { list.Add(ptIN); }
                else if (nDir == +1) { list.Add(ptEX); }
                else if (nDir == +0)
                {
                    list.Add( CPoint.GetMidPoint_Only_X(ptIN, ptEX));
                }
            }
                 
            return list;

        }
        public static List<PointF> HC_EDGE_GetRawPoints_2ndDeriv_VER(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, int nDir)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            double[] buffLine = new double[(int)rc.Height];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list; // 170523 rectangle out of range exception
            if (buffLine.Length == 0) return list; // exception for the region detection failed 170907

            for (int x = sx; x < ex; x++)
            {
                Array.Clear(buffLine, 0, buffLine.Length);

                for (int y = sy, nIndex = 0; y < ey; y++)
                {
                    buffLine[nIndex++] = rawImage[y * imageW + x];
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0, 0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);

                    ptIN = new PointF(x, sy + (float)fSubPosIN);
                    ptEX = new PointF(x, sy + (float)fSubPosEX);
                }
                else if (bReverse == true)
                {
                    Array.Reverse(buffLine);
                    fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);

                    ptIN = new PointF(x, ey - (float)fSubPosIN);
                    ptEX = new PointF(x, ey - (float)fSubPosEX);
                }

                /***/if (nDir == -1) { list.Add(ptIN); }
                else if (nDir == +1) { list.Add(ptEX); }
                else if (nDir == +0)
                {
                    list.Add( CPoint.GetMidPoint_Only_Y(ptIN, ptEX));
                }
            }

            return list;

        }

        public static void HC_EDGE_GetRawPoints_2ndDeriv_MULTI_HOR(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, List<PointF> list_EX, List<PointF> list_MD, List<PointF> list_IN)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            double[] buffLine = new double[(int)rc.Width];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return ; // 170523 rectangle out of range exception

            list_EX.Clear();
            list_MD.Clear();
            list_IN.Clear();

            for (int y = sy; y < ey; y++)
            {
                Array.Clear(buffLine, 0, buffLine.Length);

                for (int x = sx, nIndex = 0; x < ex; x++)
                {
                    buffLine[nIndex++] = rawImage[y * imageW + x];
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0, 0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    //fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    //fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    ptIN = new PointF(sx + (float)fSubPosIN, y);
                    ptEX = new PointF(sx + (float)fSubPosEX, y);
                }
                else if (bReverse == true)
                {
                    Array.Reverse(buffLine);
                    //fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    //fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    ptIN = new PointF(ex - (float)fSubPosIN, y);
                    ptEX = new PointF(ex - (float)fSubPosEX, y);
                }

                list_IN.Add(ptIN);
                list_MD.Add(CPoint.GetMidPoint_Only_X(ptIN, ptEX));
                list_EX.Add(ptEX);
            }

            return ;

        }
        public static void HC_EDGE_GetRawPoints_2ndDeriv_MULTI_VER(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, List<PointF> list_EX, List<PointF> list_MD, List<PointF> list_IN)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            double[] buffLine = new double[(int)rc.Height];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return ; // 170523 rectangle out of range exception

            list_EX.Clear();
            list_MD.Clear();
            list_IN.Clear();

            for (int x = sx; x < ex; x++)
            {
                Array.Clear(buffLine, 0, buffLine.Length);

                for (int y = sy, nIndex = 0; y < ey; y++)
                {
                    buffLine[nIndex++] = rawImage[y * imageW + x];
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0, 0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    Computer.HC_EDGE_PREWITT_SUBPIXEL(buffLine, ref fSubPosIN, ref fSubPosEX);
                    //fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    //fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);

                    ptIN = new PointF(x, sy + (float)fSubPosIN);
                    ptEX = new PointF(x, sy + (float)fSubPosEX);
                }
                else if (bReverse == true)
                {
                    Array.Reverse(buffLine);
                    //fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    //fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    Computer.HC_EDGE_PREWITT_SUBPIXEL(buffLine, ref fSubPosIN, ref fSubPosEX);

                    ptIN = new PointF(x, ey - (float)fSubPosIN);
                    ptEX = new PointF(x, ey - (float)fSubPosEX);
                }
                
                
                list_IN.Add(ptIN);
                list_MD.Add(CPoint.GetMidPoint_Only_Y(ptIN, ptEX));
                list_EX.Add(ptEX);

                
            }

            return ;

        }
        public static List<PointF> HC_EDGE_GetRawPoints_2ndDeriv_DIA(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bReverse, int nDir)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            double[] buffLine = new double[(int)rc.Width];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list; // 170523 rectangle out of range exception

            for (int y = sy; y < ey; y++)
            {
                Array.Clear(buffLine, 0, buffLine.Length);

                for (int x = sx, nIndex = 0; x < ex; x++)
                {
                    buffLine[nIndex++] = rawImage[y * imageW + x];
                }

                double fSubPosIN = 0;
                double fSubPosEX = 0;

                PointF ptIN = new PointF(0, 0);
                PointF ptEX = new PointF(0, 0);

                if (bReverse == false)
                {
                    fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);

                    ptIN = new PointF(sx + (float)fSubPosIN, y);
                    ptEX = new PointF(sx + (float)fSubPosEX, y);
                }
                else if (bReverse == true)
                {
                    Array.Reverse(buffLine);
                    fSubPosIN = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    fSubPosEX = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);

                    ptIN = new PointF(ex - (float)fSubPosIN, y);
                    ptEX = new PointF(ex - (float)fSubPosEX, y);
                }

                /***/if (nDir == -1) { list.Add(ptIN); }
                else if (nDir == +1) { list.Add(ptEX); }
                else if (nDir == +0)
                {
                    double miny = Math.Min(ptIN.Y, ptEX.Y);
                    double maxy = Math.Max(ptIN.Y, ptEX.Y);
                    double midY = miny + ((maxy - miny) / 2.0);
                    PointF ptMid = new PointF(ptIN.X, (float)midY);
                    list.Add(ptMid);
                }
            }

            return list;

        }


        public static List<double> HC_EDGE_GetRawPoints_2ndDeriv_HOR(List<PointF> listEdges, int nBuffLength, byte[] rawImage, int imageW, int imageH, bool bPosLeft, int nSign, bool bReverse)
        {
            List<double> list = new List<double>();

            double[] buffLine = new double[nBuffLength];

            for (int y = 0; y < listEdges.Count; y++)
            {
                int px = (int)listEdges.ElementAt(y).X;
                int py = (int)listEdges.ElementAt(y).Y;

                Array.Clear(buffLine, 0, nBuffLength);

                if (bPosLeft == true)
                {
                    Buffer.BlockCopy(rawImage, py * imageW + px - nBuffLength, buffLine, 0, nBuffLength);
                }
                else if (bPosLeft == false)
                {
                    Buffer.BlockCopy(rawImage, py * imageW + px, buffLine, 0, buffLine.Length);
                }

                if (bReverse == true)
                {
                    Array.Reverse(buffLine);
                }

                if (nSign == -1)
                {
                    double posMin = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    list.Add(posMin);
                }
                else if (nSign == 1)
                {
                    double posMax = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    list.Add(posMax);
                }
                else if (nSign == 0)
                {
                    double posMax = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    double posMin = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    double posMid = Math.Min(posMax, posMin) + ((Math.Max(posMax, posMin) - Math.Min(posMax, posMin)) / 2.0);

                    list.Add(posMid);
                }
            }

            return list;
        }
        public static List<double> HC_EDGE_GetRawPoints_2ndDeriv_VER(List<PointF> listEdges, int nBuffLength, byte[] rawImage, int imageW, int imageH, bool bPosTop, int nSign, bool bReverse)
        {
            List<double> list = new List<double>();

            double[] buffLine = new double[nBuffLength];

            // 
            for (int i = 0; i < listEdges.Count; i++)
            {
                int px = (int)listEdges.ElementAt(i).X;
                int py = (int)listEdges.ElementAt(i).Y;

                Array.Clear(buffLine, 0, nBuffLength);

                if (bPosTop == true)
                {
                    for (int y = py - nBuffLength, nIndex = 0; y < py; y++)
                    {
                        buffLine[nIndex++] = rawImage[y * imageW + px];
                    }
                }
                else if (bPosTop == false)
                {
                    for (int y = py, nIndex = 0; y < py + nBuffLength; y++)
                    {
                        buffLine[nIndex++] = rawImage[y * imageW + px];
                    }
                }

                if (bReverse == true)
                {
                    Array.Reverse(buffLine);
                }

                if (nSign == -1)
                {
                    double posMin = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    list.Add(posMin);
                }
                else if (nSign == 1)
                {
                    double posMax = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    list.Add(posMax);
                }
                else if (nSign == 0)
                {
                    double posMax = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    double posMin = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    double posMid = Math.Min(posMax, posMin) + ((Math.Max(posMax, posMin) - Math.Min(posMax, posMin)) / 2.0);

                    list.Add(posMid);
                }
            }

            return list;
        }
        public static List<double> HC_EDGE_GetRawPoints_2ndDeriv_DIA(List<PointF> listEdges, int nBuffLength, byte[] rawImage, int imageW, int imageH, bool bPosLeft, int nSign, bool bReverse)
        {
            List<double> list = new List<double>();

            double[] buffLine = new double[nBuffLength];

            for (int y = 0; y < listEdges.Count; y++)
            {
                int px = (int)listEdges.ElementAt(y).X;
                int py = (int)listEdges.ElementAt(y).Y;

                int nPos = py*imageW+px;

                Array.Clear(buffLine, 0, nBuffLength);

                if (bPosLeft == true)
                {
                    for (int posX = nPos - nBuffLength, nIndex = 0; posX < nPos + nBuffLength; posX++)
                    {
                        buffLine[nIndex++] = rawImage[y * imageW + posX];
                    }
                }
                else if (bPosLeft == false)
                {
                    for (int posX = nPos, nIndex = 0; posX < nPos + nBuffLength; posX++)
                    {
                        buffLine[nIndex++] = rawImage[y * imageW + posX];
                    }
                }

                if (bReverse == true)
                {
                    Array.Reverse(buffLine);
                }

                if (nSign == -1)
                {
                    double posMin = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    list.Add(posMin);
                }
                else if (nSign == 1)
                {
                    double posMax = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    list.Add(posMax);
                }
                else if (nSign == 0)
                {
                    double posMax = Computer.HC_EDGE_Get2ndDerivLine_PosMax(buffLine);
                    double posMin = Computer.HC_EDGE_Get2ndDerivLine_PosMin(buffLine);
                    double posMid = Math.Min(posMax, posMin) + ((Math.Max(posMax, posMin) - Math.Min(posMax, posMin)) / 2.0);

                    list.Add(posMid);
                }
            }

            return list;
        }


        #endregion

        //*****************************************************************************************
        // Overlay method
        //*****************************************************************************************

        #region OVERLAY METHOD
        public static float GetNewtonRapRes(double[] arrAccProfile)
        {
            // set kernel size by default 
            const int KSIZE = 5;
            double[][] pMatrix = new double[5][];

            // allocation
            for (int y = 0; y < KSIZE; y++) { pMatrix[y] = new double[6]; }

            // get max position
            double fMin = arrAccProfile.Min();
            int nMinPos = Array.IndexOf(arrAccProfile, fMin);

            // assignment
            for (int y = 0; y < KSIZE; y++)
            {
                for (int x = 0; x < KSIZE; x++)
                {
                    pMatrix[y][x] = Math.Pow((double)(y + 1), (double)(KSIZE - 1 - x));
                }
                int nPos = (int)(nMinPos - ((KSIZE - 1) / 2.0) + y);

                // additive fuck exception : incase of Position want to place on the asshole 
                if (nPos > 0 && arrAccProfile.Length > nPos)
                {
                    pMatrix[y][KSIZE] = arrAccProfile[nPos];
                }
                else
                {
                    pMatrix[y][KSIZE] = 0;
                }
            }

            // fucking gauss
            _GaussElimination(pMatrix, KSIZE + 1, KSIZE);

            double[] pNewton = new double[KSIZE];

            for (int x = 0; x < KSIZE - 1; x++)
            {
                pNewton[x] = (double)(KSIZE - 1 - x) * pMatrix[x][KSIZE];
            }

            double NRValue = _NewtonRaphson(pNewton, KSIZE - 1, (double)(KSIZE + 1) / 2.0);

            // set fucking value
            return Convert.ToSingle(nMinPos + NRValue);
        }

        //  capsulated Edge detection function for only horizontal and vertical  170412 
        public static List<PointF> GetRawPoints_HOR_Prewitt(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bTarget_TOP, int nDir)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            byte[] buffLine = new byte[ey - sy];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list; // 170523 rectangle out of range exception

            // top side reverse 
            if (bTarget_TOP == true)
            {
                for (int x = sx; x < ex; x++)
                {
                    Array.Clear(buffLine, 0, buffLine.Length);
                    for (int y = sy, nIndex = 0; y < ey; y++)
                    {
                        buffLine[nIndex++] = rawImage[y * imageW + x];
                    }
                    double[] projH = Computer.GetPrewitBuffLine(buffLine, nDir);
                    float yy = (float)Computer.GetNewtonRapRes(projH);

                    list.Add(new PointF(x, (float)(sy + yy)));
                }
            }
            // btm side 
            else if (bTarget_TOP == false)
            {
                for (int x = sx; x < ex; x++)
                {
                    Array.Clear(buffLine, 0, buffLine.Length);
                    for (int y = ey - 1, nIndex = 0; y >= sy; y--)
                    {
                        buffLine[nIndex++] = rawImage[y * imageW + x];
                    }
                    double[] projH = Computer.GetPrewitBuffLine(buffLine, nDir);
                    float yy = (float)Computer.GetNewtonRapRes(projH);

                    list.Add(new PointF(x, (float)(ey + yy)));
                }
            }
            return list;
        }
        public static List<PointF> GetRawPoints_VER_Prewitt(byte[] rawImage, int imageW, int imageH, RectangleF rc, bool bTarget_LFT, int nDir)
        {
            // get joint points positions
            int sx = (int)rc.X;
            int ex = (int)rc.Width + sx;
            int sy = (int)rc.Y;
            int ey = (int)rc.Height + sy;

            byte[] buffLine = new byte[ex - sx];

            List<PointF> list = new List<PointF>();

            if (CRect.IsBoarderPosition(rc, imageW, imageH) == true) return list; // 170523 rectangle out of range exception

            // top side reverse 
            if (bTarget_LFT == true)
            {

                for (int y = sy; y < ey; y++)
                {
                    Array.Clear(buffLine, 0, buffLine.Length);
                    for (int x = ex - 1, nIndex = 0; x >= sx; x--)
                    {
                        buffLine[nIndex++] = rawImage[y * imageW + x];
                    }
                    double[] projV = Computer.GetPrewitBuffLine(buffLine, nDir);
                    float xx = (float)Computer.GetNewtonRapRes(projV);

                    list.Add(new PointF(ex - xx, y));
                }
            }
            // btm side 
            else if (bTarget_LFT == false)
            {
                for (int y = sy; y < ey; y++)
                {
                    for (int x = sx, nIndex = 0; x < ex; x++)
                    {
                        buffLine[nIndex++] = rawImage[y * imageW + x];
                    }
                    double[] projV = Computer.GetPrewitBuffLine(buffLine, nDir);
                    float xx = (float)Computer.GetNewtonRapRes(projV);

                    list.Add(new PointF(sx + xx, y));
                }
            }
            return list;
        }
        public static double[] GetAccPrewitHor(byte[] rawImage, int imageW, int imageH, RectangleF rc, int nMeasureType)
        {
            const int RISING = 0;
            const int FALLING = 1;

            if (CRect.isValid(ref rc, imageW, imageH) == true)
            {
                int sy = (int)rc.Y;
                int ey = (int)rc.Y + (int)rc.Height;
                int sx = (int)rc.X;
                int ex = (int)rc.X + (int)rc.Width;

                double[] profile = new double[(int)rc.Height];

                // accumulation for each position
                for (int x = sx; x < ex; x++)
                {
                    for (int y = sy; y < ey; y++)
                    {
                        if (nMeasureType == RISING)
                        {
                            profile[y - sy] += rawImage[(y + 1) * imageW + x] - rawImage[(y - 1) * imageW + x];
                        }
                        else if (nMeasureType == FALLING)
                        {
                            profile[y - sy] += rawImage[(y - 1) * imageW + x] - rawImage[(y + 1) * imageW + x];
                        }
                    }
                }
                return profile;
            }
            return new double[0];
        }
        public static double[] GetAccPrewitVer(byte[] rawImage, int imageW, int imageH, RectangleF rc, int nMeasureType)
        {
            List<PointF> list = new List<PointF>();

            const int RISING = 0;
            const int FALLING = 1;

            int sy = (int)rc.Y;
            int ey = (int)rc.Y + (int)rc.Height;
            int sx = (int)rc.X;
            int ex = (int)rc.X + (int)rc.Width;

            double[] profile = new double[(int)rc.Width];

            // accumulation for each position
            for (int y = sy; y < ey; y++)
            {
                for (int x = sx; x < ex; x++)
                {
                    if (nMeasureType == RISING)
                    {
                        profile[x - sx] += rawImage[y * imageW + (x + 1)] - rawImage[y * imageW + (x - 1)];
                    }
                    else if (nMeasureType == FALLING)
                    {
                        profile[x - sx] += rawImage[y  * imageW + (x - 1)] - rawImage[y * imageW + (x + 1)];
                    }
                }
            }
            return profile;
        }
        public static double[] GetPrewitBuffLine(byte[] buffLine, int nMeasureType)
        {
            const int RISING = 0;
            const int FALLING = 1;

            double[] profile = new double[buffLine.Length];

            // accumulation for each position
            for (int nIndex = 1; nIndex < buffLine.Length-1; nIndex++)
            {
                if (nMeasureType == RISING)
                {
                    profile[nIndex] += buffLine[(nIndex + 1)] - buffLine[(nIndex - 1)];
                }
                else if (nMeasureType == FALLING)
                {
                    profile[nIndex] += buffLine[(nIndex - 1)] - buffLine[(nIndex + 1)];
                }
            }

            return profile;
        }
        public static double _NewtonRaphson(double []pNewton, int szPoly, double tStart)
        {
            double tSlope = 0;
	        double tPoint = tStart;

	        for(int itr = 0; itr < 10; itr++)
	        {
		        double tValue = tSlope = 0.0;

		        for(int i = 0; i < szPoly; i++)
                {
			        tValue += pNewton[i] * Math.Pow(tPoint, (double)(szPoly - 1 - i));
                }
		        for(int i = 0; i < szPoly - 1; i++)
                {
			        tSlope += (double)(szPoly - 1 - i) * pNewton[i] * Math.Pow(tPoint, (double)(szPoly - 2 - i));
                }
		        double bPoint = tPoint;

		        if(Math.Abs(tSlope) < 1e-10) break;
		        if(Math.Abs(tValue) < 1e-16) break;

		        tPoint = (tSlope * tPoint - tValue) / tSlope;
		        if( Math.Abs(bPoint - tPoint) < 1e-5) break;
	        }
	        return tPoint;
        }
        public static void _GaussElimination(double [][] pMatrix, int szX, int szY)
        {
	        // X must be bigger than Y by 1

	        // Left Diagonal
	        for(int i = 0; i < szX - 2; i++)
	        {
		        for(int j = i + 1; j < szY; j++)
		        {
			        if(pMatrix[j][i] * pMatrix[i][i] != double.NaN)
			        {
				        double eCoeff = pMatrix[j][i] / pMatrix[i][i];
                        for (int k = i; k < szX; k++)
                        {
                            pMatrix[j][k] = pMatrix[j][k] - pMatrix[i][k] * eCoeff;
                        }
			        }
		        }
	        }

	        // Right Diagonal
	        for(int j = 0; j < szY - 1; j++)
	        {
		        for(int i = j + 1; i < szX - 1; i++)
		        {
			        if(pMatrix[j][i] * pMatrix[i][i] != double.NaN)
			        {
				        double eCoeff = pMatrix[j][i] / pMatrix[i][i];
                        for (int k = i; k < szX; k++)
                        {
                            pMatrix[j][k] = pMatrix[j][k] - pMatrix[i][k] * eCoeff;
                        }
			        }
		        }
	        }

	        for(int j = 0; j < szY; j++)
	        {
		        if(pMatrix[j][j] != double.NaN)
		        {
			        double eCoeff = pMatrix[j][j];
			        pMatrix[j][j] = 1.0;
			        pMatrix[j][szX - 1] = pMatrix[j][szX - 1] / eCoeff;
		        }
	        }
        }
        #endregion

        public static double GetSubPixelFromLineBuff<T>(T[] lineBuff, int nPos)
        {
            double pa = 0; double pb = 0; double pc = 0;

            double fSubPixel = 0;

            try
            {
                if (nPos != 0 && nPos < lineBuff.Length - 1)
                {
                    pa = Convert.ToDouble(lineBuff[nPos - 1]);
                    pb = Convert.ToDouble(lineBuff[nPos + 0]);
                    pc = Convert.ToDouble(lineBuff[nPos + 1]);

                    // simple quadratic interpolation
                    fSubPixel = 0.5 * (pa - pc) / (pa - (2 * pb) + pc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return fSubPixel;
        }

        //*****************************************************************************************
        // Circle Fitting
        //*****************************************************************************************

        #region CIRCLE FITTING
        public static void HC_FIT_Circle(List<PointF> list, ref PointF ptCenter, ref double radius)
        {
            double sx = 0.0, sy = 0.0;
            double sx2 = 0.0, sy2 = 0.0, sxy = 0.0;
            double sx3 = 0.0, sy3 = 0.0, sx2y = 0.0, sxy2 = 0.0;

            /* compute summations */
            for (int k = 0; k < list.Count; k++)
            {
                double x = list.ElementAt(k).X;
                double y = list.ElementAt(k).Y;

                double xx = x * x;
                double yy = y * y;

                sx += x;
                sy += y;
                sx2 += xx;
                sy2 += yy;
                sxy +=  x * y;
                sx3 +=  x * xx;
                sy3 +=  y * yy;
                sx2y += xx * y;
                sxy2 += yy * x;
            }
            /* compute a's,b's,c's */
            double a1 = 2.0 * (sx * sx - sx2 * list.Count);
            double a2 = 2.0 * (sx * sy - sxy * list.Count);
            double b1 = a2;
            double b2 = 2.0 * (sy * sy - sy2 * list.Count);
            double c1 = sx2 * sx - sx3 * list.Count + sx * sy2 - sxy2 * list.Count;
            double c2 = sx2 * sy - sy3 * list.Count + sy * sy2 - sx2y * list.Count;

            double det = a1 * b2 - a2 * b1;
            if (Math.Abs(det) < 0.0001)
            {                /*collinear한 경우임;*/
                return;
            }

            /* floating value  center */
            double cx = (c1 * b2 - c2 * b1) / det;
            double cy = (a1 * c2 - a2 * c1) / det;

            /* compute radius squared */
            double radsq = (sx2 - 2 * sx * cx + cx * cx * list.Count + sy2 - 2 * sy * cy + cy * cy * list.Count) / list.Count;
            radius = Math.Sqrt(radsq);
            /* integer value center */
            ptCenter.X = Convert.ToSingle(cx + 0.5);
            ptCenter.Y = Convert.ToSingle(cy + 0.5);

            return;
        }
        public static List<PointF> HC_FIT_Ellipse(List<PointF> ptListTarget, int density, ref PointF ptCenter)
        {
            double A = 0, B = 0;
            double cos_phi = 0, sin_phi = 0;

            double ptCX = 0;
            double ptCY = 0;
            HC_FIT_EllipseParamSet(ptListTarget, out ptCX, out ptCY, out A, out B, out cos_phi, out sin_phi);

            List<PointF> ptContourList = HC_FIT_EllipseGenContour(density, ptCX, ptCY, A, B, cos_phi, sin_phi);

            double fAVG_X = 0;
            double fAVG_Y = 0;

            foreach (PointF pt in ptContourList)
            {
                fAVG_X += pt.X; fAVG_Y += pt.Y;
            }
            fAVG_X /= ptContourList.Count;
            fAVG_Y /= ptContourList.Count;

            ptCenter = new PointF((float)fAVG_X, (float)fAVG_Y);

            return ptContourList;
        }
        public static List<PointF> HC_FIT_EllipseGenContour(int nSamplingDensity, double CX, double CY, double A, double B, double cos_phi, double sin_phi)
        {
            double fPitch = (2 * Math.PI) / nSamplingDensity;

            double[] contArrX = new double[nSamplingDensity];
            double[] contArrY = new double[nSamplingDensity];

            Parallel.For(0, nSamplingDensity, i =>
            {
                contArrX[i] = CX + (A * Math.Cos(fPitch * i));
                contArrY[i] = CY + (B * Math.Sin(fPitch * i));
            });

            double[][] arrContour = { contArrX, contArrY };
            DotNetMatrix.Matrix mtrpreContour = new DotNetMatrix.Matrix(arrContour);

            double[][] arrSC = { new double[] { cos_phi, sin_phi }, new double[] { -sin_phi, cos_phi } };
            DotNetMatrix.Matrix mtrSC = new DotNetMatrix.Matrix(arrSC);

            DotNetMatrix.Matrix mtrContour = mtrSC.Multiply(mtrpreContour);

            float x = 0;
            float y = 0;

            List<PointF> ptList = new List<PointF>();

            for (int i = 0; i < nSamplingDensity; i++)
            {
                x = (float)mtrContour.GetElement(0, i);
                y = (float)mtrContour.GetElement(1, i);

                ptList.Add(new PointF(x, y));
            }

            return ptList;
        }
        public static void HC_FIT_EllipseParamSet(List<PointF> ptListTarget, out double ptCX, out double ptCY, out double A, out double B, out double cos_phi, out double sin_phi)
        {
            if (ptListTarget.Count == 0)
            {
                A = B = 0;
                cos_phi = sin_phi = 0;
                ptCX = ptCY = 0;
                return;
            }
            int nDataCount = ptListTarget.Count;

            float[] CroodList_X = ptListTarget.Select(element => element.X).ToArray();
            float[] CroodList_Y = ptListTarget.Select(element => element.Y).ToArray();

            // required output : a, b, sign, cos, center x, center y
            A = B = 0; cos_phi = sin_phi = 0; ptCX = ptCY = 0;

            double meanX = CroodList_X.Average();
            double meanY = CroodList_Y.Average();

            double[] x1 = new double[nDataCount];
            double[] y1 = new double[nDataCount];
            double[] xx = new double[nDataCount];
            double[] yy = new double[nDataCount];
            double[] xy = new double[nDataCount];

            for (int i = 0; i < nDataCount; i++)
            {
                x1[i] = CroodList_X[i] - meanX;
                y1[i] = CroodList_Y[i] - meanY;
            }

            for (int i = 0; i < nDataCount; i++)
            {
                xx[i] = x1[i] * x1[i];
                yy[i] = y1[i] * y1[i];
                xy[i] = x1[i] * y1[i];
            }

            double[][] arrFittingData = { xx, xy, yy, x1, y1 };
            double[] arrFittingDataSum = new double[5];

            arrFittingDataSum[0] = xx.Sum();
            arrFittingDataSum[1] = xy.Sum();
            arrFittingDataSum[2] = yy.Sum();
            arrFittingDataSum[3] = x1.Sum();
            arrFittingDataSum[4] = y1.Sum();

            // make nemerator *********************************************************************
            // sum ( fitting data 1X5 matrix )

            DotNetMatrix.Matrix mtrNumerator = new DotNetMatrix.Matrix(arrFittingDataSum, 1);

            // make denominator  ******************************************************************
            // fitting data' * fitting data   by transpose & multiply

            DotNetMatrix.Matrix mtrFittingData = new DotNetMatrix.Matrix(arrFittingData);
            DotNetMatrix.Matrix mtrTrans = mtrFittingData.Transpose();
            DotNetMatrix.Matrix mtrDenominator = mtrFittingData.Multiply(mtrTrans);
            DotNetMatrix.Matrix mtrInvertDenom = mtrDenominator.Inverse();

            // calcuate parameters ****************************************************************
            DotNetMatrix.Matrix res = mtrNumerator.Multiply(mtrInvertDenom);

            A = res.GetElement(0, 0);
            B = res.GetElement(0, 1);

            double C = res.GetElement(0, 2);
            double D = res.GetElement(0, 3);
            double E = res.GetElement(0, 4);

            double oriental_Rad = 0;

            // fitting check value 
            double checkValue = Math.Min(Math.Abs(B / A), Math.Abs(B / C));

            if (checkValue > 0.0001) // pass case 
            {
                oriental_Rad = 1.0 / 2.0 * Math.Atan(B / (C - A));

                cos_phi = Math.Cos(oriental_Rad);
                sin_phi = Math.Sin(oriental_Rad);

                A = (A * cos_phi * cos_phi) - (B * cos_phi * sin_phi) + (C * sin_phi * sin_phi);
                B = 0;
                C = (A * sin_phi * sin_phi) + (B * cos_phi * sin_phi) + (C * cos_phi * cos_phi);
                D = (D * cos_phi - E * sin_phi);
                E = (D * sin_phi + E * cos_phi);

                meanX = cos_phi * meanX - sin_phi * meanY;
                meanY = sin_phi * meanX + cos_phi * meanY;
            }
            else // false case 
            {
                oriental_Rad = 0;
                cos_phi = Math.Cos(oriental_Rad);
                sin_phi = Math.Sin(oriental_Rad);
            }

            double Status = A * C;
            double F = 0;

            /***/
            if (Status == 0) Console.Write("Parabola Found\n");
            else if (Status < 0) Console.Write("Hyperbola Found\n");
            else if (Status > 0)
            {
                if (A < 0)
                {
                    A *= -1; C *= -1; D *= -1; E *= -1;
                }
                else
                {
                    ptCX = (float)(meanX - D / 2 / A);
                    ptCY = (float)(meanY - E / 2 / C);

                    F = 1 + (D * D) / (4 * A) + (E * E) / (4 * C);

                    A = Math.Sqrt(F / A);
                    B = Math.Sqrt(F / C);

                    #region MyRegion
                    // no meaning full variables for axis
                    //double long_Axis = 2 * Math.Max(a, b);
                    //double shor_Axis = 2 * Math.Min(a, b);

                    // Circle center calculation
                    //double[] arrXY = new double[] { CX, CY };

                    //Matrix mtrPosIN = new Matrix(arrXY, 1);
                    //Matrix mtrPosRes = mtrPosIN.Multiply(mtrSC);

                    //X0_in = (X0 * cos_phi )+  (Y0 * sin_phi);
                    //Y0_in = (X0 * -sin_phi) + (Y0 * cos_phi);

                    //ptCenter.X = Convert.ToInt32(mtrPosRes.GetElement(0, 0));
                    //ptCenter.Y = Convert.ToInt32(mtrPosRes.GetElement(0, 1));
                    #endregion

                    #region cross line calculation - no need

                    //double[][] verLine = { new double[] { X0, X0 }, new double[] { Y0 + (-B), Y0 + B } };
                    //double[][] horLine = { new double[] { X0 + (-A), X0 + A }, new double[] { Y0, Y0 } };

                    //Matrix mtrVerLine = new Matrix(verLine);
                    //Matrix mtrHorLine = new Matrix(horLine);

                    //Matrix mtrFinalLineVer = mtrSC.Multiply(mtrVerLine);
                    //Matrix mtrFinalLineHor = mtrSC.Multiply(mtrHorLine);

                    //Point pt_v1 = new Point(); Point pt_v2 = new Point();
                    //Point pt_h1 = new Point(); Point pt_h2 = new Point();

                    //pt_v1.X = (int)(mtrFinalLineVer.GetElement(0, 0));
                    //pt_v1.Y = (int)(mtrFinalLineVer.GetElement(1, 0));
                    //pt_v2.X = (int)(mtrFinalLineVer.GetElement(0, 1));
                    //pt_v2.Y = (int)(mtrFinalLineVer.GetElement(1, 1));

                    //pt_h1.X = (int)(mtrFinalLineHor.GetElement(0, 0));
                    //pt_h1.Y = (int)(mtrFinalLineHor.GetElement(1, 0));
                    //pt_h2.X = (int)(mtrFinalLineHor.GetElement(0, 1));
                    //pt_h2.Y = (int)(mtrFinalLineHor.GetElement(1, 1));

                    #endregion

                }
            }
        }
        public static  List<PointF> GetFilteredEllipsePoints(RectangleF rc, List<PointF> list)
        {
            System.Drawing.Drawing2D.GraphicsPath myPath = new System.Drawing.Drawing2D.GraphicsPath();
            myPath.AddEllipse(rc);

            List<PointF> listTemp = new List<PointF>();

            for( int i = 0; i < list.Count; i++)
            {
                PointF pt = list.ElementAt(i);
                if (myPath.IsVisible(pt) == true) { listTemp.Add(pt); }
            }
            return listTemp;
        }
        #endregion


        //*****************************************************************************************
        // Convolution
        //*****************************************************************************************

        #region CONVOLUTION
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
      
        public static double[] ARRAY_GetMeanImage(double[] rawImage, int imageW, int imageH, int nKernelSize)
        {
            int nGap = nKernelSize / 2;

            double[] fPadding = (double[])ARRAY_Padding_ALL(rawImage, imageW, imageH, nGap);

            double[] fmeanImage = new double[imageW * imageH];

            int nCount = nKernelSize * nKernelSize;
            
            for (int y = nGap; y < imageH + nGap; y++)
            //Parallel.For(nGap, imageH + nGap, y =>
            {
                for (int x = nGap; x < imageW + nGap; x++)
                {
                    double fBlockSum = 0x00;
                    for (int yy = y - nGap; yy <= y + nGap; yy++)
                    {
                        for (int xx = x - nGap; xx <= x + nGap; xx++)
                        {
                            fBlockSum += fPadding[yy * (imageW + nGap * 2) + xx];
                        }
                    }
                    fmeanImage[(y - nGap) * imageW + (x - nGap)] = fBlockSum / nCount;
                }
            }//);

            return fmeanImage;
        }
        public static double[] ARRAY_GetPowImage(double[] fImage)
        {
            double[] fPowImage = new double[fImage.Length];

            // in double case : just do it
            Parallel.For(0, fImage.Length, i =>
            {
                fPowImage[i] = fImage[i] * fImage[i];
                    
            });
            return fPowImage;
        }

        public static byte[] HC_CropImage_Overlap(byte[] rawInput, int imageW, int imageH, byte[] cropImage, int cropW, int cropH, RectangleF rc)
        {
            int posX = Convert.ToInt32(rc.X);
            int posY = Convert.ToInt32(rc.Y);
            Parallel.For(0, cropH, y => { Buffer.BlockCopy(cropImage, y * cropW, rawInput, (posY + y) * imageW + posX, cropW); });
            return rawInput;
        }
        public static byte[] ARRAY_GetNormalizedImage(double[] fImage)
        {
            double MIN = fImage.Min();
            double MAX = fImage.Max();

            if (double.IsNaN(MIN)){MIN = 0;}

            double RANGE = MAX - MIN;

            byte[] rawImage = new byte[fImage.Length];

            //for( int i = 0; i < rawImage.Length; i++ )
            Parallel.For(0, fImage.Length, i =>
            {
                double fValue = 0;

                /*****/if (RANGE != 0){fValue = ((fImage[i] - MIN) / (RANGE)) * 255.0;}
                //else if (RANGE == 0){ fValue = 0; }

                /***/if( double.IsNaN(fValue) == false){fValue = (int)(fValue);}
                else if (double.IsNaN(fValue) == true ){fValue = 0;}

                rawImage[i] = (byte)fValue;
            });
            return rawImage;
        }
        #endregion

        ///*****************************************************************************************
        // Filters
        //*****************************************************************************************
        #region FILTERES
        public static byte[] HC_FILTER_STD_Window(byte[] rawImage, int imageW, int imageH, RectangleF rc, int nKernelSize, double powValue = 0.5)
        {
            int cropW = (int)rc.Width;
            int cropH = (int)rc.Height;
            byte[] cropImage = HC_CropImage(rawImage, imageW, imageH, rc);

            double[] fImageCrop = null;
            Parallel.Invoke(() => { fImageCrop = cropImage.Select(element => (double)element).ToArray(); });

            //SaveImage(cropImage, cropW, cropH, "c:\\micle.bmp");

            double[] meanPow = ARRAY_GetMeanImage(fImageCrop, cropW, cropH, nKernelSize);
            meanPow = ARRAY_GetPowImage(meanPow);

            double[] powMean = ARRAY_GetPowImage(fImageCrop);
            powMean = ARRAY_GetMeanImage(powMean, cropW, cropH, 5);


            for (int i = 0; i < meanPow.Length; i++)
            //Parallel.For(0, meanPow.Length, i =>
            {
                double fValue1 = powMean[i];
                double fValue2 = meanPow[i];
                double fValue3 = fValue1 - fValue2;
                double fvalue4 = Math.Pow(fValue3, powValue);

                if (double.IsNaN(fvalue4) == true)
                {
                    fvalue4 = 0;
                }

                fImageCrop[i] = fvalue4;
            }
            byte[] cropRaw = ARRAY_GetNormalizedImage(fImageCrop); ;

            return HC_CropImage_Overlap(rawImage, imageW, imageH, cropRaw, cropW, cropH, rc);
        }
        public static byte[] HC_FILTER_STD(byte[] rawImage, int imageW, int imageH, int nKernelSize, double powValue=0.5)
        {
            double[] fImage = rawImage.Select(element => (double)element).ToArray();

            double[] meanPow = ARRAY_GetMeanImage(fImage, imageW, imageH, nKernelSize);
            meanPow = ARRAY_GetPowImage(meanPow);

            double[] powMean = ARRAY_GetPowImage(fImage);
            powMean = ARRAY_GetMeanImage(powMean, imageW, imageH, nKernelSize);

            for (int i = 0; i < meanPow.Length; i++)
            //Parallel.For(0, meanPow.Length, i =>
            {
                double fValue1 = powMean[i];
                double fValue2 = meanPow[i];
                double fValue3 = fValue1 - fValue2;
                double fvalue4 = Math.Pow(fValue3, powValue);

                if (double.IsNaN(fvalue4) == true)
                {
                    fvalue4 = 0;
                }

                fImage[i] = fvalue4;
            }
            return ARRAY_GetNormalizedImage(fImage); ;
        }

        public static byte[] HC_TRANS_SIGMOID_Contrast(byte[] rawImage, int imageW, int imageH, double fCutoff, double fGain)
        {
            double[] fImage = HC_CONV_Byte2Double(rawImage);

            double min = fImage.Min();
            Parallel.For(0, fImage.Length, i => { fImage[i] -= min; });

            // devision max
            double max = fImage.Max();

            Parallel.For(0, fImage.Length, i => { fImage[i] /= max; });

            // case 2 : gamma 
            Parallel.For(0, fImage.Length, i =>
            {
                double fValue = (fCutoff - fImage[i]) * fGain;
                fImage[i] = 1.0 / (1.0 + Math.Exp(fValue));
            });

            return ARRAY_GetNormalizedImage(fImage);

        }

        public static byte[] HC_FILTER_ADF(byte[] rawImage, int imageW, int imageH, double fKappa, double iter, double fDelta)
        {
            double[] KernelN = new double[9] { 0, 1, 0, 0, -1, 0, 0, 0, 0 };
            double[] KernelS = new double[9] { 0, 0, 0, 0, -1, 0, 0, 1, 0 };
            double[] KernelE = new double[9] { 0, 0, 0, 0, -1, 1, 0, 0, 0 };
            double[] KernelW = new double[9] { 0, 0, 0, 1, -1, 0, 0, 0, 0 };
            double[] KernelNE = new double[9] { 0, 0, 1, 0, -1, 0, 0, 0, 0 };
            double[] KernelSE = new double[9] { 0, 0, 0, 0, -1, 0, 0, 0, 1 };
            double[] KernelSW = new double[9] { 0, 0, 0, 0, -1, 0, 1, 0, 0 };
            double[] KernelNW = new double[9] { 1, 0, 0, 0, -1, 0, 0, 0, 0 };


            double[] rawImageN = new double[imageW * imageH];
            double[] rawImageS = new double[imageW * imageH];
            double[] rawImageE = new double[imageW * imageH];
            double[] rawImageW = new double[imageW * imageH];
            double[] rawImageNE = new double[imageW * imageH];
            double[] rawImageSE = new double[imageW * imageH];
            double[] rawImageSW = new double[imageW * imageH];
            double[] rawImageNW = new double[imageW * imageH];

            double[] diffusN = new double[imageW * imageH];
            double[] diffusS = new double[imageW * imageH];
            double[] diffusE = new double[imageW * imageH];
            double[] diffusW = new double[imageW * imageH];

            double[] diffusNE = new double[imageW * imageH];
            double[] diffusSE = new double[imageW * imageH];
            double[] diffusNW = new double[imageW * imageH];
            double[] diffusSW = new double[imageW * imageH];


            double[] fImage = HC_CONV_Byte2Double(rawImage);

            double dx = 1.0 / Math.Pow(1.0, 2);
            double dy = 1.0 / Math.Pow(1.0, 2);
            double dxy = 1.0 / Math.Pow(Math.Sqrt(2), 2);

            for (int loop = 0; loop < iter; loop++)
            {
                rawImageN = HC_FILTER_Convolution(KernelN, fImage, imageW, imageH);
                rawImageS = HC_FILTER_Convolution(KernelS, fImage, imageW, imageH);
                rawImageE = HC_FILTER_Convolution(KernelE, fImage, imageW, imageH);
                rawImageW = HC_FILTER_Convolution(KernelW, fImage, imageW, imageH);

                rawImageNE = HC_FILTER_Convolution(KernelNE, fImage, imageW, imageH);
                rawImageSE = HC_FILTER_Convolution(KernelSE, fImage, imageW, imageH);
                rawImageNW = HC_FILTER_Convolution(KernelNW, fImage, imageW, imageH);
                rawImageSW = HC_FILTER_Convolution(KernelSW, fImage, imageW, imageH);

                CalcDifussion(rawImageN, diffusN, fKappa);
                CalcDifussion(rawImageS, diffusS, fKappa);
                CalcDifussion(rawImageE, diffusE, fKappa);
                CalcDifussion(rawImageW, diffusW, fKappa);

                CalcDifussion(rawImageNE, diffusNE, fKappa);
                CalcDifussion(rawImageSE, diffusSE, fKappa);
                CalcDifussion(rawImageNW, diffusNW, fKappa);
                CalcDifussion(rawImageSW, diffusSW, fKappa);

                Parallel.For(0, imageW * imageH, i =>
                {
                    fImage[i] = fImage[i] + fDelta *
                        (
                              diffusN[i] * rawImageN[i] * dx
                            + diffusS[i] * rawImageS[i] * dx
                            + diffusE[i] * rawImageE[i] * dy
                            + diffusW[i] * rawImageW[i] * dy
                            + diffusNE[i] * rawImageNE[i] * dxy
                            + diffusSE[i] * rawImageSE[i] * dxy
                            + diffusNW[i] * rawImageNW[i] * dxy
                            + diffusSW[i] * rawImageSW[i] * dxy
                        ); ;
                });

            }

            CalcDiffuseNormal(fImage);

            rawImage = HC_CONV_Double2Byte(fImage);

            return rawImage;
        }
        public static byte[] HC_FILTER_ADF_Window(byte[] rawImage, int imageW, int imageH, RectangleF rc, double fKappa, double iter, double fDelta)
        {
            int cropW = (int)rc.Width;
            int cropH = (int)rc.Height;
            byte[] cropImage = HC_CropImage(rawImage, imageW, imageH, rc);

            byte[] adfResult = HC_FILTER_ADF(cropImage, cropW, cropH, fKappa, iter, fDelta);

            return HC_CropImage_Overlap(rawImage, imageW, imageH, adfResult, cropW, cropH, rc);
        }
        private static double[] CalcDifussion(double[] rawImage, double[] fImage, double fkappa)
        {
            int nLength = rawImage.Length;

            Parallel.For(0, nLength, i =>
            {
                fImage[i] = Math.Exp(-Math.Pow((rawImage[i] / fkappa), 2));
            });

            return fImage;
        }
        private static void CalcDiffuseNormal(double[] fImage)
        {
            double fMax = fImage.Max();
            double fMin = fImage.Min();

            double fRange = fMax - fMin;

            Parallel.For(0, fImage.Length, i =>
            {
                fImage[i] = (fImage[i] - fMin) / fRange;
                fImage[i] *= 255;
            });
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

        public static double[] HC_FILTER_GenerateGaussianFilter_MODFIED(double fSigma, int nKSize)
        {
            double[] fKernel = new double[nKSize * nKSize];

            int GAP = nKSize / 2;

            double fSum = 0;

            for (int x = -GAP; x <= GAP; x++)
            {
                for (int y = -GAP; y <= GAP; y++)
                {

                    fKernel[(y + GAP) * nKSize + x + GAP] = (Math.Exp((-((x * x + y * y) / 2 * fSigma * fSigma))) * (1.0 - ((x * x + y * y) / 2.0 * fSigma * fSigma)) * -(Math.PI * fSigma * fSigma * fSigma * fSigma));

                    //fKernel[(y + GAP) * nKSize + x + GAP] = Math.Exp((-((x * x + y * y) / 2 * fSigma * fSigma))) / (2 * Math.PI * fSigma * fSigma);
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

            return fKernel;
        }
        public static byte[] HC_FILTER_Sharpening(byte[] rawImage, int imageW, int imageH)
        {
            double[] fKernel = HC_FILTER_GenerateGaussianFilter_MODFIED(1.0, 5);

             return HC_FILTER_Convolution(fKernel, rawImage, imageW, imageH);
        }
        public static byte[] HC_FILTER_StepSmoothing(byte[] rawImage, int imageW, int imageH, int nStep = 1)
        {
            nStep = nStep * 2 + 1;
            double[] fKernel = HC_FILTER_GenerateGaussianFilter_MODFIED(0.1, nStep);
            return HC_FILTER_Convolution(fKernel, rawImage, imageW, imageH);
        }
        #endregion

        //*****************************************************************************************
        // Data filtering
        //***************************************************************************************** * 
        #region DATA Filtering
        // fitering functions for the horizontal and vertical rectangle boundary 170412 
        public static List<PointF> GetList_FilteredBy_TY_BY(List<PointF> list, double fTY, double fBY, double fThreshold)
        {
            List<PointF> listBuff = new List<PointF>();

            for (int i = 0; i < list.Count; i++)
            {
                PointF pt = list.ElementAt(i);
                if (Math.Abs(pt.Y - fTY) < fThreshold || Math.Abs(pt.Y - fBY) < fThreshold) continue;
                listBuff.Add(pt);
            }
            return listBuff;
        }
        public static List<PointF> GetList_FilteredBy_LX_RX(List<PointF> list, double fLX, double fRX, double fThreshold)
        {
            List<PointF> listBuff = new List<PointF>();

            for (int i = 0; i < list.Count; i++)
            {
                PointF pt = list.ElementAt(i);
                if (Math.Abs(pt.X - fLX) < fThreshold || Math.Abs(pt.X - fRX) < fThreshold) continue;
                listBuff.Add(pt);
            }
            return listBuff;
        }

        public static List<PointF> GetList_FilterBy_MajorDistance(List<PointF> list, bool bAxisX, double fDistance)
        {
            List<PointF> listBuff = new List<PointF>();

            PointF[] arrPoints = list.ToArray();

            // empty input exception 170523
            if (list.Count == 0) return list;

            int nMax = 0;
            int nMaxPos = 0;

            if (bAxisX == true)
            {

                int[] arrX = arrPoints.Select(element => (int)element.X).ToArray();
                nMax = arrX.Max() + 1;
                int[] nHisto = new int[nMax];

                for (int i = 0; i < arrX.Length; i++)
                {
                    nHisto[arrX[i]]++;
                }
                nMax = nHisto.Max();
                nMaxPos = Array.IndexOf(nHisto, nMax);

                for (int i = 0; i < list.Count; i++)
                {
                    PointF pt = list.ElementAt(i);
                    if (Math.Abs(pt.X - nMaxPos) < fDistance)
                    {
                        listBuff.Add(pt);
                    }
                    else
                    {
                        listBuff.Add(new PointF(nMaxPos, pt.Y));
                    }
                }
            }
            else if (bAxisX == false)
            {
                int[] arrY = arrPoints.Select(element => (int)element.Y).ToArray();
                nMax = arrY.Max() + 1;
                int[] nHisto = new int[nMax];

                for (int i = 0; i < arrY.Length; i++)
                {
                    nHisto[arrY[i]]++;
                }
                nMax = nHisto.Max();
                nMaxPos = Array.IndexOf(nHisto, nMax);

                for (int i = 0; i < list.Count; i++)
                {
                    PointF pt = list.ElementAt(i);
                    if (Math.Abs(pt.Y - nMaxPos) < fDistance)
                    {
                        listBuff.Add(pt);
                    }
                    else
                    {
                        listBuff.Add(new PointF(pt.X, nMaxPos));
                    }
                }
            }
            return listBuff;
        }

        // croodinate points fixation function for the axis  170412 
        public static List<PointF> ReplacePointList_Absolute_X(RectangleF rc, float x)
        {
            List<PointF> list = new List<PointF>();

            int nHead = (int)rc.Y;
            int nTail = (int)rc.Y + (int)rc.Height;

            for (int y = nHead; y < nTail; y++)
            {
                list.Add(new PointF(x, y));
            }
            return list;
        }
        public static List<PointF> ReplacePointList_Absolute_Y(RectangleF rc, float y)
        {
            List<PointF> list = new List<PointF>();

            int nHead = (int)rc.X;
            int nTail = (int)rc.X + (int)rc.Width;

            for (int x = nHead; x < nTail; x++)
            {
                list.Add(new PointF(x, y));
            }
            return list;
        }

        public static int GetMaxElementPosition(double[] array)
        {
            double fMax = array.Max();
            int nIndex = Array.IndexOf(array, fMax);

            return nIndex;
        }
        public static int GetMinElementPosition(double[] array)
        {
            double fMin = array.Min();
            int nIndex = Array.IndexOf(array, fMin);
            return nIndex;
        }
        public static int GetMajorValue(int[] array)
        {
            int max = array.Max();

            int[] histo = new int[max+1];

            for (int i = 0; i < array.Count(); i++)
            {
                histo[array[i]]++;
            }

            max = histo.Max();

            int nMaxPos = Array.IndexOf(histo, max);
            return nMaxPos;
        }
        #endregion
    }

    //*****************************************************************************************
    // Ransac Models
    //***************************************************************************************** * 

    #region RANSAC MODELS
    public class CModelEllipse
    {
        public double a, b, c, d, e, f;	// 타원방정식: ax^2 + bxy + cy^2 + dx + ey + f = 0
        public double cx, cy, w, h;		// 표준 형태: (x - cx)^2/w^2 + (y - cy)^2/h^2 = 1
        public double theta;				// 표준 형태 타원의 기울어진 각도

        public bool convert_std_form()
        {
            // 타원 방정식에서 표준 형태의 타원의 매개변수로 변경
            // 참조: http://blog.naver.com/helloktk/80035366367

            // orientation of ellipse;    
            theta = Math.Atan2(b, a - c) / 2.0;
            // scaled major/minor axes of ellipse;
            double ct = Math.Cos(theta);
            double st = Math.Sin(theta);
            double ap = a * ct * ct + b * ct * st + c * st * st;
            double cp = a * st * st - b * ct * st + c * ct * ct;

            // translations 
            cx = (2 * c * d - b * e) / (b * b - 4 * a * c);
            cy = (2 * a * e - b * d) / (b * b - 4 * a * c);

            // scale factor
            double val = a * cx * cx + b * cx * cy + c * cy * cy;
            double scale_inv = val - f;

            if (scale_inv / ap <= 0 || scale_inv / cp <= 0)
            {
                //not ellipse;
                return false;
            }

            w = Math.Sqrt(scale_inv / ap);
            h = Math.Sqrt(scale_inv / cp);
            return true;
        }
    }
    public class CModelLine
    {
        public double mx = 0;
        public double my = 0;
        public double sx = 0;
        public double sy = 0;

        public CModelLine()
        {
            mx = my = sx = sy = 0;
        }
    }
    public class CModelCircle
    {
        public double cx = 0;
        public double cy = 0;
        public double r = 0;

        public CModelCircle()
        {
            cx = cy = r = 0;
        }
    }
    #endregion

    //*****************************************************************************************
    // Ransac
    //*****************************************************************************************

    public static class CRansac
    {

        public static PointF GetMidPointY_by_Ratio(CModelLine lex, CModelLine lin, double fRatio)
        {
            PointF P1 = new PointF((float)lex.sx, (float)lex.sy);
            PointF P2 = new PointF((float)lin.sx, (float)lin.sy);

            double fPosX = (float)((P1.X + P2.X) / 2.0);
            double fPosY = CPoint.GetRatioDistance(P1.Y, P2.Y, fRatio);

            return new PointF( (float)fPosX, (float)fPosY);
        }
        public static PointF GetMidPointX_by_Ratio(CModelLine lex, CModelLine lin, double fRatio)
        {
            PointF P1 = new PointF((float)lex.sx, (float)lex.sy);
            PointF P2 = new PointF((float)lin.sx, (float)lin.sy);

            double fPosX = CPoint.GetRatioDistance(P1.X, P2.X, fRatio);
            double fPosY = (float)((P1.Y + P2.Y) / 2.0);

            return new PointF((float)fPosX, (float)fPosY);
        }

        public static double GetDistanceY(CModelLine fex, CModelLine fin,  CModelLine sex, CModelLine sin, double posF, double posS)
        {
            if (posF > 1) posF = 1; if (posF < 0) posF = 0;
            if (posS > 1) posS = 1; if (posS < 0) posS = 0;

            double fMax = Math.Max(fex.sy, fin.sy);
            double fMin = Math.Min(fex.sy, fin.sy);
            double fLen = fMax - fMin;

            double sMax = Math.Max(sex.sy, sin.sy);
            double sMin = Math.Min(sex.sy, sin.sy);
            double sLen = sMax - sMin;

            double fLineY = fMin + (fLen * posF);
            double SLineY = sMin + (sLen * posS);

            return Math.Abs(fLineY - SLineY);
        }

        public static double GetDistanceX(CModelLine fex, CModelLine fin, CModelLine sex, CModelLine sin, double posF, double posS)
        {
            if (posF > 1) posF = 1; if (posF < 0) posF = 0;
            if (posS > 1) posS = 1; if (posS < 0) posS = 0;

            double fMax = Math.Max(fex.sx, fin.sx);
            double fMin = Math.Min(fex.sx, fin.sx);
            double fLen = fMax - fMin;

            double sMax = Math.Max(sex.sx, sin.sx);
            double sMin = Math.Min(sex.sx, sin.sx);
            double sLen = sMax - sMin;

            double fLineY = fMin + (fLen * posF);
            double SLineY = sMin + (sLen * posS);

            return Math.Abs(fLineY - SLineY);
        }
        //*****************************************************************************************
        // Random samples
        //*****************************************************************************************

        public static void get_samples(ref List<PointF> samples, int no_samples, PointF [] data)
        {
            Random rand = new Random();

            // 데이터에서 중복되지 않게 N개의 무작위 셈플을 채취한다.
            for (int i = 0; i < data.Length; i++ )
            {
                int nRand = rand.Next(0, data.Length-1);
            
                if (!find_in_samples(samples, data.ElementAt(nRand)))
                {
                    samples.Add(data[nRand]);
                    if (samples.Count == no_samples)
                    {
                        break;
                    }
                }
            }
            //for( int i = 0; i < no_samples; i++)
            //{
            //    samples.Add(data.ElementAt(i));
            //}
        }
        private static bool find_in_samples(List<PointF>  samples, PointF data)
            {
                for (int i = 0; i < samples.Count; ++i)
                {
                    if (samples[i].X == data.X && samples[i].Y == data.Y)
                    {
                        return true;
                    }
                }
                return false;
            }

        //*****************************************************************************************
        // Ellipse Fitting
        //*****************************************************************************************

        public static double ransac_ellipse_fitting(PointF[] data, ref CModelEllipse model, double distance_threshold)
        {
            const int no_samples = 30;
            int no_data = data.Length;

            if (no_data < no_samples)
            {
                return 0.0;
            }

            List<PointF> samples = new List<PointF>(); // count 5
            List<PointF> inliers = new List<PointF>(); // count 5;

            CModelEllipse estimated_model;
            double max_cost = 0.0;

            int max_iteration = (int)(1 + Math.Log(1.0 - 0.99) / Math.Log(1.0 - Math.Pow(0.5, no_samples)));


            for (int i = 0; i < 50; i++)
            {
                samples.Clear();

                // 1. hypothesis
                // 원본 데이터에서 임의로 N개의 셈플 데이터를 고른다.

                get_samples(ref samples, no_samples, data);

                // 이 데이터를 정상적인 데이터로 보고 모델 파라메터를 예측한다.
                estimated_model = compute_ellipse_model(ref samples);
                if (!estimated_model.convert_std_form()) { --i; continue; }

                // 2. Verification

                // 원본 데이터가 예측된 모델에 잘 맞는지 검사한다.
                double cost = verify_ellipse(inliers, ref estimated_model, data, distance_threshold);

                // 만일 예측된 모델이 잘 맞는다면, 이 모델에 대한 유효한 데이터로 새로운 모델을 구한다.
                if (max_cost < cost)
                {
                    max_cost = cost;

                    model = compute_ellipse_model(ref inliers);
                    model.convert_std_form();
                }
            }
            return max_cost;
        }
        
        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★

        public static CModelEllipse compute_ellipse_model(ref List<PointF> samples)
        {
            // 타원 방정식: x^2 + axy + by^2 + cx + dy + e = 0

            DotNetMatrix.Matrix A = new DotNetMatrix.Matrix(samples.Count, 5);
            DotNetMatrix.Matrix B = new DotNetMatrix.Matrix(samples.Count, 1);

            for (int i = 0; i < samples.Count; i++)
            {
                double x = samples[i].X;
                double y = samples[i].Y;

                A.SetElement(i, 0, x * y);
                A.SetElement(i, 1, y * y);
                A.SetElement(i, 2, x);
                A.SetElement(i, 3, y);
                A.SetElement(i, 4, 1.0);

                B.SetElement(i, 0, -x * x);
            }

            // AX=B 형태의 해를 least squares solution으로 구하기 위해
            // Moore-Penrose pseudo-inverse를 이용한다.
            //DotNetMatrix.Matrix invA = 

            DotNetMatrix.Matrix transA = A.Transpose();
            DotNetMatrix.Matrix multiA = transA.Multiply(A);
            DotNetMatrix.Matrix InverA = multiA.Inverse();
            DotNetMatrix.Matrix invA = InverA.Multiply(transA);
            //dMatrix invA = !(~A*A)*~A;

            DotNetMatrix.Matrix X = invA.Multiply(B);

            // c가 1보다 클 때는 c를 1이 되도록 ratio 값을 곱해준다.
            double ratio = (X.GetElement(1, 0) > 1.0) ? 1.0 / X.GetElement(1, 0) : 1.0;

            CModelEllipse model = new CModelEllipse();

            model.a = ratio * 1.0;
            model.b = ratio * X.GetElement(0, 0);
            model.c = ratio * X.GetElement(1, 0);
            model.d = ratio * X.GetElement(2, 0);
            model.e = ratio * X.GetElement(3, 0);
            model.f = ratio * X.GetElement(4, 0);

            return model;
        }
        public static double /*****/compute_ellipse_distance(ref CModelEllipse ellipse, PointF p)
        {
            // 한 점 p에서 타원에 내린 수선의 길이를 계산하기 힘들다.
            // 부정확하지만, 간단히 하기위하여 대수적 거리를 계산한다.
            double x = p.X;
            double y = p.Y;

            double e = Math.Abs(ellipse.a * x * x + ellipse.b * x * y + ellipse.c * y * y + ellipse.d * x + ellipse.e * y + ellipse.f);
            return Math.Sqrt(e);
        }
        public static double /*****/verify_ellipse (List<PointF> inliers, ref CModelEllipse estimated_model, PointF [] data, double distance_threshold)
        {
            inliers.Clear();


            double cost = 0.0;
            
            for(int i=0; i< data.Length; i++)
            {
		        // 직선에 내린 수선의 길이를 계산한다.
		        double distance = compute_ellipse_distance(ref estimated_model, data[i]);
	
		        // 예측된 모델에서 유효한 데이터인 경우, 유효한 데이터 집합에 더한다.
		        if (distance < distance_threshold) 
                {
			        cost += 1.0;

                    inliers.Add(data[i]);
		        }
            }
    
            return cost;
        }
        public static List<PointF> GetEllipseContours (CModelEllipse ellipse)
        {
            double ct = Math.Cos(ellipse.theta);
	        double st = Math.Sin(ellipse.theta);

            List<PointF> list = new List<PointF>();

            for (int i=0; i<=360; i+=1) 
            {
		        double a = i* Math.PI/180.0;
		        double x = ellipse.w*Math.Cos(a);
		        double y = ellipse.h*Math.Sin(a);

		        double rx = x*ct - y*st;
		        double ry = x*st + y*ct;
                    
                double newX = ellipse.cx + rx;
                double newY = ellipse.cy + ry;

                list.Add(new PointF( (float) newX, (float)newY));
            }

            return list;
        }

        //*****************************************************************************************
        // Line Fitting
        //*****************************************************************************************

        public static double ransac_Line_fitting(PointF[] data, ref CModelLine model, double distance_threshold, int no_samples, int iter)
        {
             int no_data = data.Length;

            if (no_data < no_samples)
            {
                return 0.0;
            }

            List<PointF> samples = new List<PointF>(); // count 5
            List<PointF> inliers = new List<PointF>(); // count 5;

            CModelLine estimated_model;
            double max_cost = 0.0;

            int max_iteration = (int)(1 + Math.Log(1.0 - 0.99) / Math.Log(1.0 - Math.Pow(0.5, no_samples)));

            for (int i = 0; i < iter; i++)
            {
                samples.Clear();

                // 1. hypothesis
                // 원본 데이터에서 임의로 N개의 셈플 데이터를 고른다.

                get_samples(ref samples, no_samples, data);

                // 이 데이터를 정상적인 데이터로 보고 모델 파라메터를 예측한다.
                estimated_model = compute_model_line(ref samples);

                // 2. Verification
                // 원본 데이터가 예측된 모델에 잘 맞는지 검사한다.
                double cost = verify_line(inliers, ref estimated_model, data, distance_threshold);

                // 만일 예측된 모델이 잘 맞는다면, 이 모델에 대한 유효한 데이터로 새로운 모델을 구한다.
                if (max_cost < cost)
                {
                    max_cost = cost;

                    model = compute_model_line(ref inliers);
                }
            }
            return max_cost;
        }

        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★

        public static CModelLine compute_model_line(ref List<PointF> samples)
        {
            // PCA 방식으로 직선 모델의 파라메터를 예측한다.

            double sx = 0, sy = 0;
            double sxx = 0, syy = 0;
            double sxy = 0, sw = 0;

            for (int i = 0; i < samples.Count; ++i)
            {
                double x = samples[i].X;
                double y = samples[i].Y;

                sx += x;
                sy += y;
                sxx += x * x;
                sxy += x * y;
                syy += y * y;
                sw += 1;
            }

            //variance;
            double vxx = (sxx - sx * sx / sw) / sw;
            double vxy = (sxy - sx * sy / sw) / sw;
            double vyy = (syy - sy * sy / sw) / sw;

            //principal axis
            double theta = Math.Atan2(2 * vxy, vxx - vyy) / 2.0;

            CModelLine model = new CModelLine();

            model.mx = Math.Cos(theta);
            model.my = Math.Sin(theta);

            //center of mass(xc, yc)
            model.sx = sx / sw;
            model.sy = sy / sw;

            //직선의 방정식: sin(theta)*(x - sx) = cos(theta)*(y - sy);
            return model;
        }
        public static double /**/compute_line_distance(ref CModelLine model, PointF p)
        {
            // 한 점(x)로부터 직선(line)에 내린 수선의 길이(distance)를 계산한다.
            return Math.Abs((p.X - model.sx) * model.my - (p.Y - model.sy) * model.mx) / Math.Sqrt(model.mx * model.mx + model.my * model.my);
        }
        public static double /**/verify_line(List<PointF> inliers, ref CModelLine estimated_model, PointF[] data, double distance_threshold)
            {
                inliers.Clear();

                double[] arrDist = new double[data.Length];

                double cost = 0.0;

                for (int i = 0; i < data.Length; i++)
                {
                    // 직선에 내린 수선의 길이를 계산한다.
                    double distance = compute_line_distance(ref estimated_model, data[i]);

                    arrDist[i] = distance;
                    // 예측된 모델에서 유효한 데이터인 경우, 유효한 데이터 집합에 더한다.
                    if (distance < distance_threshold)
                    {
                        cost += 1.0;

                        inliers.Add(data[i]);
                    }
                }

                return cost;
            }

        //*****************************************************************************************
        // Circle Fitting
        //*****************************************************************************************

        public static double ransac_Circle_fitting(PointF[] data, ref CModelCircle model, double distance_threshold, int no_samples, int iter)
        {
            int no_data = data.Length;

            if (no_data < no_samples)
            {
                return 0.0;
            }

            List<PointF> samples = new List<PointF>(); // count 5
            List<PointF> inliers = new List<PointF>(); // count 5;

            CModelCircle model_buffer = new CModelCircle();
            double max_cost = 0.0;

            int max_iteration = (int)(1 + Math.Log(1.0 - 0.99) / Math.Log(1.0 - Math.Pow(0.5, no_samples)));


            for (int i = 0; i < iter; i++)
            {
                samples.Clear();

                // 1. hypothesis
                // 원본 데이터에서 임의로 N개의 셈플 데이터를 고른다.

                get_samples(ref samples, no_samples, data);

                // 이 데이터를 정상적인 데이터로 보고 모델 파라메터를 예측한다.
                model = compute_circle_model(ref samples);

                // 2. Verification
                // 원본 데이터가 예측된 모델에 잘 맞는지 검사한다.
                double cost = verify_circle(inliers, ref model, data, distance_threshold);

                // 만일 예측된 모델이 잘 맞는다면, 이 모델에 대한 유효한 데이터로 새로운 모델을 구한다.
                if (max_cost < cost)
                {
                    max_cost = cost;

                    model = compute_circle_model(ref inliers);
                }
            }
            if (max_cost == 0)
            {
                model = model_buffer;
            }
            return max_cost;
        }

        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★

        public static CModelCircle compute_circle_model(ref List<PointF> samples)
        {
            // 중심 (a,b), 반지름 c인 원의 방정식: (x - a)^2 + (y - b)^2 = c^2
	        // 식을 전개하면: x^2 + y^2 - 2ax - 2by + a^2 + b^2 - c^2 = 0
            DotNetMatrix.Matrix A = new DotNetMatrix.Matrix(samples.Count, 3);
            DotNetMatrix.Matrix B = new DotNetMatrix.Matrix(samples.Count, 1);


            for (int i = 0; i < samples.Count; i++)
            {
                double x = samples[i].X;
                double y = samples[i].Y;

                A.SetElement(i, 0, x);
                A.SetElement(i, 1, y);
                A.SetElement(i, 2, 1.0);

                B.SetElement(i, 0, (-x * x) - (y*y));
            }

            // AX=B 형태의 해를 least squares solution으로 구하기 위해
            // Moore-Penrose pseudo-inverse를 이용한다.
            //DotNetMatrix.Matrix invA = 

            DotNetMatrix.Matrix transA = A.Transpose();
            DotNetMatrix.Matrix multiA = transA.Multiply(A);
            DotNetMatrix.Matrix InverA = multiA.Inverse();
            DotNetMatrix.Matrix invA = InverA.Multiply(transA);
            //dMatrix invA = !(~A*A)*~A;

            DotNetMatrix.Matrix X = invA.Multiply(B);

            CModelCircle model = new CModelCircle();
            
            double cx = -X.GetElement(0, 0) / 2.0;
            double cy = -X.GetElement(1, 0) / 2.0;

            model.r = Math.Sqrt(cx * cx + cy * cy - X.GetElement(2, 0));
            model.cx = cx;
            model.cy = cy;

            return model;
        }
        public static double /****/compute_circle_distance(ref CModelCircle model, PointF p)
        {
            // 원의 둘레로부터 떨어진 거리를 계산한다.
            // 즉, 점 x와 원의 중심 까지의 거리를 구해서 원의 반지름을 뺀다.

            double dx = model.cx - p.X;
            double dy = model.cy- p.Y;

            return Math.Abs(Math.Sqrt(dx * dx + dy * dy) - model.r);
        }
        public static double /****/verify_circle(List<PointF> inliers, ref CModelCircle estimated_model, PointF[] data, double distance_threshold)
        {
            inliers.Clear();


            double cost = 0.0;

            for (int i = 0; i < data.Length; i++)
            {
                // 직선에 내린 수선의 길이를 계산한다.
                double distance = compute_circle_distance(ref estimated_model, data[i]);

                // 예측된 모델에서 유효한 데이터인 경우, 유효한 데이터 집합에 더한다.
                if (distance < distance_threshold)
                {
                    cost += 1.0;

                    inliers.Add(data[i]);
                }
            }

            return cost;
        }

        public static CLine GetFittedLine_VER(CModelLine model, RectangleF rc)
        {
            float fHalf_H = Convert.ToSingle(rc.Height / 2.0);

            PointF p1 = new PointF(Convert.ToSingle(model.sx - fHalf_H * model.mx), Convert.ToSingle(model.sy - fHalf_H * model.my));
            PointF p2 = new PointF(Convert.ToSingle(model.sx + fHalf_H * model.mx), Convert.ToSingle(model.sy + fHalf_H * model.my));

            return new CLine(p1, p2);
        }
    }
}
