using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

using DispObject;

namespace HP_Troops
{
    public static class HELPER_IMAGE_IO
    {
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
        public static byte[] HC_CropImage_Polar(byte[] rawInput, int imageW, int imageH, RectangleF rc)
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

            double[] arrDegree = HELPER_POLAR.GetArray_Degree();

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

                    double fInterplated = _GetInterPolatedValue(x, y, x1, x2, y1, y2, q11, q12, q21, q22);

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
                    if (x < 0 || y < 0 || x >= imageW - 1 || y >= imageH - 1) { continue; }

                    int x1 = (int)Math.Floor(x);
                    int x2 = (int)Math.Ceiling(x);
                    int y1 = (int)Math.Floor(y);
                    int y2 = (int)Math.Ceiling(y);

                    int q11 = rawInput[y1 * imageW + x1];
                    int q12 = rawInput[y2 * imageW + x1];
                    int q21 = rawInput[y1 * imageW + x2];
                    int q22 = rawInput[y2 * imageW + x2];

                    double fInterplated = _GetInterPolatedValue(x, y, x1, x2, y1, y2, q11, q12, q21, q22);

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

                    double fInterplated = _GetInterPolatedValue(x, y, x1, x2, y1, y2, q11, q12, q21, q22);

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
        public static byte[] HC_CropImage_Rotate(byte[] rawInput, int imageW, int imageH, RectangleF rc, PointF ptGravity, float fAngle)
        {
            List<byte> listRot = new List<Byte>();

            int fromX = (int)(rc.X);
            int fromY = (int)(rc.Y);

            for (int y = fromY; y < (int)fromY + (int)rc.Height; y++)
            {
                for (int x = fromX; x < (int)fromX + (int)rc.Width; x++)
                {
                    PointF ptRot = _RotatePointByGravity(new PointF(x, y), ptGravity, fAngle);

                    if (x < 0 || y < 0 || x >= imageW - 1 || y >= imageH - 1) { continue; }

                    double cx = ptRot.X;
                    double cy = ptRot.Y;
                    int x1 = (int)Math.Floor(cx);
                    int x2 = (int)Math.Ceiling(cx);
                    int y1 = (int)Math.Floor(cy);
                    int y2 = (int)Math.Ceiling(cy);

                    int q11 = rawInput[y1 * imageW + x1];
                    int q12 = rawInput[y2 * imageW + x1];
                    int q21 = rawInput[y1 * imageW + x2];
                    int q22 = rawInput[y2 * imageW + x2];

                    double fInterplated = _GetInterPolatedValue(cx, cy, x1, x2, y1, y2, q11, q12, q21, q22);

                    byte value = fInterplated < 0 ? (byte)0 : fInterplated > 255 ? (byte)255 : (byte)fInterplated;
                    listRot.Add(value);
                }
            }

            byte[] rawRes = new byte[(int)rc.Width * (int)rc.Height];
            byte[] rawOut = listRot.ToArray();
            return rawOut;
        }
        public static double _GetInterPolatedValue(double cx, double cy, double x1, double x2, double y1, double y2, double q11, double q12, double q21, double q22)
        {
            double r1 = (((x2 - cx) / (x2 - x1)) * q11) + (((cx - x1) / (x2 - x1)) * q21);
            double r2 = (((x2 - cx) / (x2 - x1)) * q12) + (((cx - x1) / (x2 - x1)) * q22);
            double pvalue = (((y2 - cy) / (y2 - y1)) * r1) + (((cy - y1) / (y2 - y1)) * r2);

            if (double.IsNaN(pvalue) == true) pvalue = 0;

            return pvalue;
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

        public static string _SelectAndSaveFileAsBitmap(string strDefaultPath)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.Filter = "Bmp files (*.bmp)|*.*";

            dlg.InitialDirectory = strDefaultPath;

            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return string.Empty;

            string strPath = dlg.FileName.ToUpper();

            if (strPath.Contains(".BMP") == false)
            {
                strPath += ".BMP";
            }
            return strPath;
        }
    }
}
