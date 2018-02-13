using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices; // for safe buffer


using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;

using Cognex.VisionPro.PMAlign; // for pattern max tool
using Cognex.VisionPro.Exceptions;  // for exception

using DispObject;


using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;


namespace WrapperCognex
{
    public class cogWrapper
    {
        public CogPMAlignTool PatMaxTool = new CogPMAlignTool();
        public ResultPTRN ptrnResult = new ResultPTRN();

        public PointF PARAM_PT_RELATIVE_ORIGIN = new PointF();

        public int PARAM_NUM_TO_FIND { get; set; }
        public double PARAM_ACCEPT_RATIO { get; set; }
        public RectangleF PARAM_RC_SEARCH = new RectangleF();

        public  double PARAM_SEARCH_CX{get {return PARAM_RC_SEARCH.X + (PARAM_RC_SEARCH.Width/2.0);}}
        public double PARAM_SEARCH_CY{get{return PARAM_RC_SEARCH.Y + (PARAM_RC_SEARCH.Height /2.0);}}

        public object locker = new object();
        public cogWrapper()
        {
            PARAM_PT_RELATIVE_ORIGIN = new PointF(0,0);
            PARAM_RC_SEARCH = new RectangleF(0,0,0,0);
            PARAM_NUM_TO_FIND = 1;
            PARAM_ACCEPT_RATIO = 0.95;

            PatMaxTool.Changed += PatMaxTool_Changed;
        }
        ~cogWrapper()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            PatMaxTool.Changed -= PatMaxTool_Changed;
            if ((PatMaxTool != null))
            {
                PatMaxTool.Dispose();
            }
        }
        //*****************************************************************************************
        // Image I/O
        //*****************************************************************************************
        public ICogImage ImageLoad_From_CogImageFile(string strPath)
        {
            // Extention Header Error Included. 
            // it shows some errors from the different types of bitmaps. 

            CogImageFile cogImageFile = new CogImageFile();
            cogImageFile.Open(strPath, CogImageFileModeConstants.Read);
            ICogImage cogImage = cogImageFile[0];
            cogImageFile.Close();
            return cogImage;
        }
        public Bitmap ImageLoad_From_DC(string strPath)
        {
            // Pure CogimageFile has Coded Problems... 
            // So, stop by bitmap and get to the result.

            Bitmap bmp = Bitmap.FromFile(strPath) as Bitmap;
            return bmp;
        }

        //*****************************************************************************************
        // Conversion
        //*****************************************************************************************
        public ICogImage Conv_BitmapToCogImage(Bitmap bitmap)
        {
            CogImage24PlanarColor image = new CogImage24PlanarColor(bitmap);
            ICogImage cog = image;
            return cog;
        }
        public CogImage24PlanarColor Conv_BitmapTo24Planar(Bitmap bmp)
        {
            CogImage24PlanarColor image = new CogImage24PlanarColor(bmp);
            return image;
        }
        public CogImage8Grey Conv_BitmapToCog8Grey(Bitmap bmp)
        {
            CogImage8Grey cogGrey8 = new CogImage8Grey(bmp);
            return cogGrey8;
        }
        public Bitmap Conv_Byte2Bmp(byte[] rawImage, int imageW, int imageH)
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
        public ICogImage Conv_ByteToCog8Grey(byte[] imageData, int width, int height)
        {
            // no padding etc. so size calculation
            // is simple.
            var rawSize = width * height;

            var buf = new SafeMalloc(rawSize);

            // Copy from the byte array into the
            // previously allocated. memory
            Marshal.Copy(imageData, 0, buf, rawSize);

            // Create Cognex Root thing.
            var cogRoot = new CogImage8Root();

            // Initialise the image root, the stride is the
            // same as the widthas the input image is byte alligned and
            // has no padding etc.
            cogRoot.Initialize(width, height, buf, width, buf);

            // Create cognex 8 bit image.
            var cogImage = new CogImage8Grey();

            // And set the image roor
            cogImage.SetRoot(cogRoot);

            return cogImage;
        }

        //*****************************************************************************************
        // Conversion
        //*****************************************************************************************


        public void Do_Ptrn(Bitmap bmpSource, Bitmap bmpTemplate)
        {
            CogRectangleAffine PatMaxTrainRegion = default(CogRectangleAffine);
            PatMaxTrainRegion = PatMaxTool.Pattern.TrainRegion as CogRectangleAffine;

            ICogImage cogInput = this.Conv_BitmapToCog8Grey(bmpSource);
            ICogImage cogTeach = this.Conv_BitmapToCog8Grey(bmpTemplate);

            int imageW = bmpSource.Width;
            int imageH = bmpSource.Height;

            int teachW = bmpTemplate.Width;
            int teachH = bmpTemplate.Height;

           
            lock(locker)
            {
                CogRectangle PatMaxSearchRegion = new CogRectangle();
                PatMaxTool.SearchRegion = PatMaxSearchRegion;
                PatMaxSearchRegion.SetCenterWidthHeight(PARAM_SEARCH_CX, PARAM_SEARCH_CY, PARAM_RC_SEARCH.Width, PARAM_RC_SEARCH.Height);
                PatMaxSearchRegion.GraphicDOFEnable = CogRectangleDOFConstants.Position | CogRectangleDOFConstants.Size;
                PatMaxSearchRegion.Interactive = true;

                if ((PatMaxTrainRegion != null))
                {
                    PatMaxTrainRegion.SetCenterLengthsRotationSkew(teachW / 2, teachH / 2, teachW, teachH, 0, 0);
                    PatMaxTrainRegion.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position |
                                                         CogRectangleAffineDOFConstants.Rotation |
                                                         CogRectangleAffineDOFConstants.Size;
                }

                try
                {
                    PatMaxTool.Pattern.TrainImage = cogTeach as CogImage8Grey;
                    PatMaxTool.Pattern.TrainAlgorithm = CogPMAlignTrainAlgorithmConstants.PatMax;
                    PatMaxTool.Pattern.TrainMode = CogPMAlignTrainModeConstants.Image;
                    PatMaxTool.Pattern.Train();
                }
                catch (CogSecurityViolationException ex)
                {
                    MessageBox.Show("Please Check The License Dongle Validity.", "LISENCE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                PatMaxTool.InputImage = cogInput;
                PatMaxTool.RunParams.ApproximateNumberToFind = 100;
                PatMaxTool.RunParams.AcceptThreshold = 0.5;

                //PatMaxTool.Changed += PatMaxTool_Changed;

                ptrnResult.removeAll();
                PatMaxTool.Run();
            }
         }

       
        private void PatMaxTool_Changed(object sender, Cognex.VisionPro.CogChangedEventArgs e)
        {
            //If FunctionalArea And cogFA_Tool_Results Then
            if ((Cognex.VisionPro.Implementation.CogToolBase.SfCreateLastRunRecord |
                 Cognex.VisionPro.Implementation.CogToolBase.SfRunStatus) > 0)
            {

                //Note, Results will be nothing if Run failed.
                if (PatMaxTool.Results == null)
                {
                    //txtPatMaxScoreValue.Text = "N/A";
                }
                else if (PatMaxTool.Results.Count > 0)
                {
                    int nRetrieved = PatMaxTool.Results.Count;

                    for (int nItem = 0; nItem < nRetrieved; nItem++)
                    {
                        if (nItem >= PARAM_NUM_TO_FIND) break;

                        int nIndexer = nItem;//listSorted.ElementAt(nItem);

                        try
                        {
                            CogPMAlignResult result = PatMaxTool.Results[nIndexer];
                            
                            if (result.Score <= PARAM_ACCEPT_RATIO / 100.0) continue;
                            
                            //Passing result does not imply Pattern is found, must check count.
                            CogCompositeShape resultGraphics = default(CogCompositeShape);
                            resultGraphics = PatMaxTool.Results[nIndexer].CreateResultGraphics(CogPMAlignResultGraphicConstants.MatchRegion);
                            
                            CogRectangle rect = resultGraphics.EnclosingRectangle(CogCopyShapeConstants.All);
                            RectangleF rcResult = new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
                            
                            // Get matced Template Center 
                            PointF ptTemplateCenter = CRect.GetCenter(rcResult);
                            
                            // Set Relative Distance
                            PointF ptDistance = CPoint.GetDistancePoint(PARAM_PT_RELATIVE_ORIGIN, ptTemplateCenter);
                            
                            CogTransform2DLinear ll = result.GetPose();
                            
                            ptrnResult.AddData(rcResult, ptTemplateCenter, ptDistance, result.Score, 0);

                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    //txtPatMaxScoreValue.Text = "N/A";
                }
                
            }
        }
    }

    public class SafeMalloc : SafeBuffer
    {
        /// <summary>
        /// Allocates memory and initialises the SaveBuffer
        /// </summary>
        /// <param name="size">The number of bytes to allocate</param>
        public SafeMalloc(int size)
            : base(true)
        {
            this.SetHandle(Marshal.AllocHGlobal(size));
            this.Initialize((ulong)size);
        }

        /// <summary>
        /// Called when the object is disposed, ferr the
        /// memory via FreeHGlobal().
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(this.handle);
            return true;
        }

        /// <summary>
        /// Cast to IntPtr
        /// </summary>
        public static implicit operator IntPtr(SafeMalloc h)
        {
            return h.handle;
        }
    }
}
