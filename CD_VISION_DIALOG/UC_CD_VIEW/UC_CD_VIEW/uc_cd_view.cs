using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using DispObject;
using CD_Figure;
using CD_Measure;
using WrapperUnion;
using DEF_PARAMS;

using HP_Troops;

namespace CD_View
{
    
    //*********************************************************************************************
    // delegate
    //*********************************************************************************************

    public delegate void deleDisplayImage(byte[] rawImage, int imageW, int imageH);
    public delegate void deleDisplayImageBitmap(Bitmap bmp);
    public delegate void dele_HereComesNewImage();
    
    //*********************************************************************************************
    // body 
    //*********************************************************************************************

    public partial class UC_CD_VIEWER: UserControl, iPopupROI
    {
        CImageIO imageIO = new CImageIO();
        CFileIO fileIO = new CFileIO();

        DispObj m_dispObj = new DispObj();

        bool m_bViewerInitState = false;

        public double m_dPixelPitch = 0;

        // Viwer's current magnification
        double m_INFO_fMagnification = 1;
        double m_INFO_fMagScale = 0.08;
        
        public Point m_ptMouseClicked;
        bool m_bMousePressed = false;

        // panning activation variable
        bool m_bPanning = false;
        public bool m_bDrawOverlay = true;

        public bool BOOL_DRAW_FOCUS_ROI { get; set; }
        public bool BOOL_DRAW_PTRN_ROI { get; set; }

        // image transformation croodinates
        int m_VirtualImgPosX = 0;
        int m_VirtualImgPosY = 0;

        // for panning
        int m_nPanStartX = 0;
        int m_nPanStartY = 0;

        public string m_strLoadedFile = string.Empty;

        Image m_bmp = new Bitmap(600,600); // Main에서 바로 긁자. 
        byte [] rawLayer1 = null;
        byte [] rawLayer2 = null;
        
        object lockerDisp = new object();
        object lockerView = new object();

        public event dele_HereComesNewImage eventDele_HereComesNewImage;

        public PointF PT_FIGURE_TO_DRAW { get; set; }
        public bool BOOL_TEACHING_ACTIVATION { get; set; }
        public int ROI_INDEX { get; set; }

        public bool BOOL_MODIFY_FIGURE_POSITION = false;

        //************************************************************************************
        public UC_CD_VIEWER()
        {
            InitializeComponent();

            // Teaching Activation Status
            BOOL_TEACHING_ACTIVATION = false;

            m_dPixelPitch = 0;
            ROI_INDEX = -1;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            UpdateStyles();

            PIC_VIEW.PreviewKeyDown += new PreviewKeyDownEventHandler(KeyDown_Event);

            imageIO.EventThreadFinishedImageLoad += EventThreadFinished_LoadImage;
            imageIO.EventThreadFinishedImageSave += EventThreadFinished_SaveImage;

            BOOL_DRAW_FOCUS_ROI = true;
            BOOL_DRAW_PTRN_ROI = true;
            
        }
        public List<string> GetDispTextObjects()
        {
            return m_dispObj.ToTextout();
        }
        public void SetDispTextObjects(List<string> list)
        {
            m_dispObj = m_dispObj.ToTextIn(list);
        }
        //************************************************************************************
        // ROI Related 
        //************************************************************************************
        
        #region ROI related
        
        // mouse action for realtime overllay 
        public bool m_bDrawPTRN = false;
        public bool m_bDrawFocus = false;

        public bool m_bDrawROI = false;

        public bool m_bEditRoi = false;
        public bool m_bROIRegistered = false;

        public Point m_ptRoiStart = new Point(-1, -1);
        public Point m_ptRealTimeMousePos = new Point(-1, -1);


        FormPopup popup = null;

        public CFigureManager fm = new CFigureManager();

        List<Rectangle> list_ROI = new List<Rectangle>();
        List<Rectangle> list_ROI_Temp = new List<Rectangle>();

        Rectangle ROI_FOCUS = new Rectangle();

        Rectangle ROI_PTRN = new Rectangle();

        public void ROI_Clear() { list_ROI.Clear(); }
        public void ROI_ADD(Rectangle rc) { list_ROI.Add(rc); }
        public void ROI_Del(int nIndex)
        {
            list_ROI.RemoveAt(nIndex);
        }

        public Rectangle ROI_GetData(int nIndex)
        {
            Rectangle rc = new Rectangle();

            if (nIndex >= list_ROI.Count)
            {
                rc = list_ROI.ElementAt(nIndex);
            }
            return rc;
        }
        public Rectangle ROI_GetDataTemp()
        {
            Rectangle rc = new Rectangle();
            if (list_ROI_Temp.Count > 0){rc = list_ROI_Temp[0];}
            return rc;
        }

        public void ROI_Del_Temp()
        {
            list_ROI_Temp.Clear();
        }
        public void ROI_Update_Temp(Rectangle rc)
        {
            list_ROI_Temp.Clear();
            list_ROI_Temp.Add(rc);
        }
        public byte[] ROI_GetRegion_Image(int nIndex)
        {
            Rectangle rc = ROI_GetData(nIndex);

            byte[] rawImage = GetDisplay_Raw();
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            byte[] cropRC = HELPER_IMAGE_IO.HC_CropImage(rawImage, imageW, imageH, rc.X, rc.Y, rc.Width, rc.Height);

            return cropRC;
        }

        
        #endregion

        //************************************************************************************
        // Initialization Fields
        //************************************************************************************

        public bool USE_BASIC_MODE = false;
        

        //************************************************************************************
        // Interface for popup
        //************************************************************************************

       #region Interface 
        public void iSetOverlayStatus(bool bDraw) { m_bDrawOverlay = bDraw; PIC_VIEW.Refresh();}
        public bool iGetOverlayStatus() { return m_bDrawOverlay; }

        public void iDrawROI(bool bDraw) { m_bDrawROI = m_bDrawOverlay = true; }
        public void iDrawFocus(bool bDraw) { m_bDrawFocus = m_bDrawROI = m_bDrawOverlay = true; }
        public void iDrawPtrn(bool bDraw) { m_bDrawPTRN = m_bDrawROI = m_bDrawOverlay = true; }

        public Rectangle iGet_Roi_Focus() { return ROI_FOCUS; }
        public Rectangle iGet_Roi_Ptrn() { return ROI_PTRN; }
        public void iSet_Roi_Focus(Rectangle rc) { ROI_FOCUS = rc;}
        public void iSet_Roi_Ptrn(Rectangle rc) { ROI_PTRN = rc;}

        public void iRemove_Roi_Focus() { ROI_FOCUS = new Rectangle(0, 0, 0, 0); }
        public void iRemove_Roi_Ptrn() { ROI_PTRN = new Rectangle(0, 0, 0, 0); }

        public int iGet_Roi_Count() { return list_ROI.Count; }
        public int iGet_Roi_Index(Point pt)
        {
            Rectangle[] rcArr = list_ROI.ToArray();

            int nIndex = -1;
            for (int i = 0; i < rcArr.Length; i++)
            {
                bool bIntersect = CRect.IsIntersectPoint(rcArr.ElementAt(i), pt);

                if (bIntersect == true)
                {
                    nIndex = i;
                    break;
                }
            }
            return nIndex;
        }

        public void iRemove_Roi_At(int nIndex)
        {
            if (list_ROI.Count >= nIndex)
            {
                list_ROI.RemoveAt(nIndex);
            }
        }
        public void iRemove_Roi_All()
        {
            list_ROI.Clear();
        }

        public void iSave_Roi_At(int nIndex)
        {
            Rectangle rc = list_ROI.ElementAt(nIndex);

            byte[] rawImage = GetDisplay_Raw();
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            byte[] cropImage = HELPER_IMAGE_IO.HC_CropImage(rawImage, imageW, imageH, rc.X, rc.Y, rc.Width, rc.Height);

            
            string strPath = _SelectSaveFile(fm.config.i11_PATH_IMG_PTRN);

            HELPER_IMAGE_IO.SaveImage(cropImage, rc.Width, rc.Height, strPath);
        }
        public string iSave_Roi_Ptrn()
        {
            Rectangle rc = ROI_PTRN;
            string strFileName = string.Empty;

            if (ROI_PTRN.X > 0 && ROI_PTRN.Y > 0 && ROI_PTRN.Width > 0 && ROI_PTRN.Height > 0)
            {
                byte[] rawImage = GetDisplay_Raw();
                int imageW = VIEW_GetImageW();
                int imageH = VIEW_GetImageH();

                byte[] cropRC = HELPER_IMAGE_IO.HC_CropImage(rawImage, imageW, imageH, rc.X, rc.Y, rc.Width, rc.Height);

                strFileName = HELPER_IMAGE_IO._SelectAndSaveFileAsBitmap(fm.config.i11_PATH_IMG_PTRN);

                if (strFileName != "")
                {

                    if (File.Exists(strFileName) == true)
                    {
                        if (MessageBox.Show("Target File is Already Exist.\nDo You Want To Overwrite??", "FILE EXISTANCE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            HELPER_IMAGE_IO.SaveImage(cropRC, rc.Width, rc.Height, strFileName);
                            if (File.Exists(strFileName) == true)
                            {
                                MessageBox.Show("PTRN Teaching has Finished.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                fm.param_ptrn.PTRN_FILE = strFileName;
                                fm.param_ptrn.ACC_RATIO = 90.0;
                            }
                            else
                            {
                                MessageBox.Show("PTRN Teaching has Failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        HELPER_IMAGE_IO.SaveImage(cropRC, rc.Width, rc.Height, strFileName);
                        if (File.Exists(strFileName) == true)
                        {
                            MessageBox.Show("PTRN Teaching has Finished.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fm.param_ptrn.PTRN_FILE = strFileName;
                            fm.param_ptrn.ACC_RATIO = 90.0;
                        }
                        else
                        {
                            MessageBox.Show("PTRN Teaching has Failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            return strFileName;
        }
        
         
        public void iAdd_Figure(Object single)
        {
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            fm.Figure_Add(single);

            Refresh();
        }
 
        public void iMod_RectPair_DigonalAngle(int nIndex, int nAngle)
        {
            CMeasurePairRct[] array = fm.ToArray_PairRct();
            CMeasurePairRct single = array[nIndex];

            single.ApplyAbsoluteRotation(nAngle);

            array[nIndex] = single;
            Refresh();
        }
    

        public void iMod_Figure(object single, int nIndex)
        {
 
            if (single.GetType().Name == "CMeasurePairRct")
            {
                CMeasurePairRct[] array = fm.ToArray_PairRct();
                array[nIndex] = (CMeasurePairRct)single;
                fm.Figure_Replace(array);
            }
            else if(single.GetType().Name == "CMeasurePairCir")
            {
                CMeasurePairCir[] array = fm.ToArray_PairCir();
                array[nIndex] = (CMeasurePairCir)single;
                fm.Figure_Replace(array);
            }
            else if (single.GetType().Name == "CMeasurePairOvl")
            {
                CMeasurePairOvl[] array = fm.ToArray_PairOvl();
                array[nIndex] = (CMeasurePairOvl)single;
                fm.Figure_Replace(array);
            }
            Refresh();
        }

        public object iGet_Figure(int nFigureType, int nIndex)
        {
            object objReturn = new object();

            /***/if (nFigureType == IFX_FIGURE.PAIR_RCT) { if (fm.COUNT_PAIR_RCT >= nIndex) { objReturn = fm.ElementAt_PairRct(nIndex); } }
            else if (nFigureType == IFX_FIGURE.PAIR_CIR) { if (fm.COUNT_PAIR_CIR >= nIndex) { objReturn = fm.ElementAt_PairCir(nIndex); } }
            else if (nFigureType == IFX_FIGURE.PAIR_OVL) { if (fm.COUNT_PAIR_OVL >= nIndex) { objReturn = fm.ElementAt_PairOvl(nIndex); } }
            return objReturn;
        }

        public void iDel_Figure(int nFigureType, int nIndex)
        {
            fm.Figure_Remove(nFigureType, nIndex);
            Refresh();
        }

        // Set Figure Drawing Status 
        public void iSetFigureDrawingActivation(bool bStatus)
        {
            BOOL_TEACHING_ACTIVATION = bStatus;
            Refresh();
        }
        // Get figure drawing status
        public bool iGetFigureDrawingActivation()
        {
            return BOOL_TEACHING_ACTIVATION;
        }

        public void iGet_CropsRectPairNormal(RectangleF rc1, RectangleF rc2,  out byte[] crop1, out byte[] crop2)
        {
            byte[] rawImgae = GetDisplay_Raw();
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            crop1 = HELPER_IMAGE_IO.HC_CropImage(rawImgae, imageW, imageH, rc1);
            crop2 = HELPER_IMAGE_IO.HC_CropImage(rawImgae, imageW, imageH, rc2);
        }
        public void iGet_CropsCircle(CMeasurePairCir single, out byte[] crop1, out byte[] crop2)
        {
            byte[] rawImgae = GetDisplay_Raw();
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            RectangleF rcEX = single.rc_EX;
            RectangleF rcIN = single.rc_IN;

            int nRadi = 0;
            crop1 = HELPER_IMAGE_IO.HC_CropImage_Interpolated_Polar(rawImgae, imageW, imageH, rcEX, out nRadi);
            crop2 = HELPER_IMAGE_IO.HC_CropImage_Interpolated_Polar(rawImgae, imageW, imageH, rcEX, out nRadi);
            
            // in case of circle, second crop region is not required.
            //crop2 = Computer.HC_Cr0opImage_Polar(rawImgae, imageW, imageH, rcIN);

            
            int nCX = Convert.ToInt32(single.rc_EX.Width / 2.0);
            int nCY = Convert.ToInt32(single.rc_EX.Height / 2.0);

            int nRadius = Math.Max(nCX, nCY);
        }

        public void iGet_CropsRectPairDigonal(CMeasurePairRct single, out byte[] crop1, out byte[] crop2)
        {
            byte[] rawImgae = GetDisplay_Raw();
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            RectangleF rc1 = single.GetRectOrigin_FST();
            RectangleF rc2 = single.GetRectOrigin_SCD();


            crop1 = HELPER_IMAGE_IO.HC_CropImage_Rotate(rawImgae, imageW, imageH, rc1, single.GetCenter(), single.ANGLE);
            crop2 = HELPER_IMAGE_IO.HC_CropImage_Rotate(rawImgae, imageW, imageH, rc2, single.GetCenter(), single.ANGLE);

        }
        public void iAdj_Figure(int nFigureType, int nIndex, int nAction, int x, int y)
        {
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            if (nFigureType == IFX_FIGURE.PAIR_RCT)
            {
                CMeasurePairRct[] array = fm.ToArray_PairRct();
                CMeasurePairRct single = fm.ElementAt_PairRct(nIndex);

                /***/if (nAction == IFX_ADJ_ACTION.POS) { single.AdjustPos(x, y); }
                else if (nAction == IFX_ADJ_ACTION.GAP) { single.AdjustGap(x, y); }
                else if (nAction == IFX_ADJ_ACTION.SIZE) { single.AdjustSize(x, y); }

                array[nIndex] = single;
                fm.Figure_Replace(array);
            }
            else if (nFigureType == IFX_FIGURE.PAIR_CIR)
            {
                CMeasurePairCir[] array = fm.ToArray_PairCir();
                CMeasurePairCir single = fm.ElementAt_PairCir(nIndex);

                /***/if (nAction == IFX_ADJ_ACTION.POS) { single.AdjustPos(x, y); }
                else if (nAction == IFX_ADJ_ACTION.GAP) { single.AdjustGap(x, y); }
                else if (nAction == IFX_ADJ_ACTION.SIZE) { single.AdjustSize(x, y); }
            }
         
            Refresh();
        }
        public void iAdj_Overlay(int nAction, int nTarget, int nIndex, int nDir, int nScale)
        {
            CMeasurePairOvl[] array = fm.ToArray_PairOvl();
            CMeasurePairOvl single = fm.ElementAt_PairOvl(nIndex);

            const int TARGET_IN = 0;
            const int TARGET_EX = 1;
             
            if (nAction == IFX_ADJ_ACTION.GAP)
            {
                /***/if (nTarget == TARGET_IN) { single.AdjustGap_IN(nDir, nScale); }
                else if (nTarget == TARGET_EX) { single.AdjustGap_EX(nDir, nScale); }
            }
            else if (nAction == IFX_ADJ_ACTION.POS)
            {
                /***/if (nTarget == TARGET_IN) { single.AdjustPos_IN(nDir, nScale); }
                else if (nTarget == TARGET_EX) { single.AdjustPos_EX(nDir, nScale); }
            }
            else if (nAction == IFX_ADJ_ACTION.SIZE)
            {
                /***/if (nTarget == TARGET_IN) { single.AdjustSize_IN(nDir, nScale); }
                else if (nTarget == TARGET_EX) { single.AdjustSize_EX(nDir, nScale); }
            }
            else if (nAction == IFX_ADJ_ACTION.ASYM)
            {
                /***/if (nTarget == TARGET_IN) { single.AdjustAsym_IN(nDir, nScale); }
                else if (nTarget == TARGET_EX) { single.AdjustAsym_EX(nDir, nScale); }
            }
            Refresh();
        }
        public CFigureManager iGet_AllData() { return fm; }


        public string _SelectSaveFile(string strDefaultPath)
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
        #endregion

        //************************************************************************************
        // Image IO
        //************************************************************************************

        #region EventFinished IO Works

        

        private void EventThreadFinished_LoadImage(object sender, CImageIO.ThreadFinishedEventArgs e)
        {
            this.UIThread(delegate
            {
                // 170614 null exception
                if (e.bmp == null) return;

                SetDisplay(e.bmp);
                m_strLoadedFile = e.strFilePath;
                VIEW_SET_Mag_Origin();

                // delegate called in the setdisplay function
                //eventDele_HereComesNewImage();
            });
        }
        private void EventThreadFinished_SaveImage(object sender, CImageIO.ThreadFinishedEventArgs e)
        {
            //none Nothing To do
        }
        public void ThreadCall_SaveImage(string strPath, Bitmap bmp)
        {
            CImageIO.ThreadFinishedEventArgs temp = new CImageIO.ThreadFinishedEventArgs();

            System.Threading.Thread thr = new System.Threading.Thread(delegate() { imageIO.SaveImage(strPath, bmp, temp); });

            thr.IsBackground = true;
            thr.Start();
        }
        public void ThreadCall_SaveImage(string strPath, byte[] rawImage, int imageW, int imageH)
        {
            CImageIO.ThreadFinishedEventArgs temp = new CImageIO.ThreadFinishedEventArgs();

            System.Threading.Thread thr = new System.Threading.Thread(delegate() { imageIO.SaveImage(strPath, rawImage, imageW, imageH, temp); });

            thr.IsBackground = true;
            thr.Start();
        } 
        
        public void ThreadCall_LoadImage(string strPath)
        {
            if (File.Exists(strPath) == false) return;  // Exception 170614

            // set parameters init
            m_bPanning = false;

            // empty file or invalid file exception
            if (strPath.Length <= 5) return;

            CImageIO.ThreadFinishedEventArgs temp = new CImageIO.ThreadFinishedEventArgs();

            System.Threading.Thread thr = new System.Threading.Thread(delegate() { imageIO.LoadImage(strPath, temp); });

            thr.IsBackground = true;
            thr.Start();

            m_strLoadedFile = strPath;

            VIEW_Set_Clear_DispObject();
            VIEW_SET_Mag_Origin();
        }
        public void ThreadCall_SaveFile(CFileIO file,  string strPath, List<string> list)
        {
            CFileIO.ThreadFinishedEventArgs temp = new CFileIO.ThreadFinishedEventArgs();

            System.Threading.Thread thr = new System.Threading.Thread(delegate() { file.SaveFile(strPath, list, temp); });
            thr.IsBackground = true;
            thr.Start();
        }
        public void ThreadCall_LoadFile(CFileIO file, string strPath)
        {
            CFileIO.ThreadFinishedEventArgs temp = new CFileIO.ThreadFinishedEventArgs();
            System.Threading.Thread thr = new System.Threading.Thread(delegate() { file.LoadFile(strPath, temp); });
            thr.IsBackground = true;
            thr.Start();
        }
        #endregion

        //*****************************************************************************************
        // Image buffer related 
        //*****************************************************************************************

        // 반드시 새로 생성된 영상이 들어온다고 가정하다.... 
        public void SetDisplay(Bitmap bmp)
        {
            lock (lockerDisp)
            {
                m_bmp = bmp;
            }
            eventDele_HereComesNewImage();
        }
        public void SetDisplay(byte[] rawImage, int imageW, int imageH)
        {
            Bitmap bmp = HELPER_IMAGE_IO.HC_CONV_Byte2Bmp(rawImage, imageW, imageH);

            lock (lockerDisp)
            {
                m_bmp = bmp;
            }
            eventDele_HereComesNewImage();
        }
        public void SetLayerDisp_Status(bool bStart, Bitmap Target)
        {
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            if (bStart == true)
            {

                rawLayer1 = HELPER_IMAGE_IO.HC_CONV_Bmp2Raw((Bitmap)m_bmp, ref imageW, ref imageH);
                rawLayer2 = HELPER_IMAGE_IO.HC_CONV_Bmp2Raw((Bitmap)Target, ref imageW, ref imageH);
            }
            else if (bStart == false)
            {
                SetDisplay(rawLayer1, imageW, imageH);
                Refresh();
            }
        }
        public void SetLayerDisp(int nBlendValue)
        {
            if (rawLayer1.Length != rawLayer2.Length) return;

            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            byte[] rawBlend = HELPER_IMAGE_IO.HC_CONV_BlendedImage(rawLayer1, rawLayer2, imageW, imageH, nBlendValue);

            SetDisplay(rawBlend, imageW, imageH);
            Refresh();
        }

       
        public string SaveDisplayOvr(int nQuality)
        {
            Bitmap bmpOvr = null;

            lock (lockerDisp)
            {
                bmpOvr = m_bmp.Clone() as Bitmap;
            }

            Graphics g = Graphics.FromImage(bmpOvr);

            PaintEventArgs e = new PaintEventArgs(g, new Rectangle(0,0, bmpOvr.Width, bmpOvr.Height));

            float fTransX = 0;
            float fTransY = 0;

            DrawOverlay(m_dispObj, e, fTransX, fTransY);
            DrawMeasures(e, fTransX, fTransY);
            DrawViewCross(e, bmpOvr.Width, bmpOvr.Height, fTransX, fTransY);

            g.Dispose();

            var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)nQuality) } };

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "BMP files (*.BMP)|*.bmp|JPG files (*.JPG)|*.JPG|All files (*.*)|*.*";

            string strFileName = string.Empty;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                strFileName = dlg.FileName.ToUpper();

                if (dlg.FileName.Contains("JPG") == true)
                {
                    strFileName = strFileName.Replace("BMP", "JPG");
                    bmpOvr.Save(strFileName, encoder, encParams);
                }
                else
                {
                    bmpOvr.Save(strFileName);
                }
            }
            return strFileName;
        }
        public byte[] GetDisplay_Raw()
        {
            Bitmap bmp = null;

            lock (lockerDisp)
            {
                bmp = m_bmp.Clone() as Bitmap;
            }

            int imageW = 0;
            int imageH = 0;
            byte[] rawImage = HELPER_IMAGE_IO.HC_CONV_Bmp2Raw(bmp, ref imageW, ref imageH);
            return rawImage;
        }
        public Bitmap GetDisplay_Bmp()
        {
            Bitmap bmp = null;

            lock (lockerDisp)
            {
                bmp = m_bmp.Clone() as Bitmap;
            }
            return bmp;
        }
        public void SetInit()
        {
            Image bmp = new Bitmap(PIC_VIEW.Width, PIC_VIEW.Height);
            m_bmp = bmp.Clone() as Bitmap;
            PIC_VIEW.Image = bmp;

            int imageW = PIC_VIEW.Image.Width;
            int imageH = PIC_VIEW.Image.Height;

            _SetElementPosition(imageW, imageH);

            m_bViewerInitState = true;

            VIEW_SET_Mag_Origin();
        }
        public void SetInit(int imageW, int imageH, bool bUseBasicMode)
        {
            Image bmp = new Bitmap(imageW, imageH);
            m_bmp = bmp.Clone() as Bitmap;
            PIC_VIEW.Image = bmp;

            USE_BASIC_MODE = bUseBasicMode;
            _SetElementPosition(imageW, imageH);
            
        }

        private void _SetElementPosition(int imageW, int imageH)
        {
            LB_VIEW_MOUSE_POS_X.Location = new Point(10, imageH - 10);
            LB_VIEW_MOUSE_POS_Y.Location = new Point(100, imageH - 10);
            LB_VIEW_PIXEL_VALUE.Location = new Point(200, imageH - 10);

            if (USE_BASIC_MODE == false)
            {
                PNL_TOOL_COMMON.Location = new Point(0, imageH + 5);
            }
            else if (USE_BASIC_MODE == true)
            {
                PNL_TOOL_BASIC.Location = new Point(0, imageH + 5);
            }

            if (USE_BASIC_MODE == true)
            {
                PNL_TOOL_BASIC.Visible = true;
                PNL_TOOL_COMMON.Visible = false;
            }
            else if (USE_BASIC_MODE == false)
            {
                PNL_TOOL_BASIC.Visible = false;
                PNL_TOOL_COMMON.Visible = true;
            }



        }
        
        private void KeyDown_Event(object s, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            if (e.Control == true )
            {
                m_bDrawOverlay = !m_bDrawOverlay;
            }
        }
        //*****************************************************************************************
        // drag and drop
        //*****************************************************************************************
        
        #region drag and drop

        private void Viewer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Viewer_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                int dropX = this.PointToClient(new Point(e.X, e.Y)).X;
                int dropY = this.PointToClient(new Point(e.X, e.Y)).Y;

                Point ptPicView = new Point();
                Size szPicView = new Size();

                lock (lockerView)
                {
                    ptPicView = PIC_VIEW.Location;
                    szPicView = PIC_VIEW.Size;
                }

                Rectangle rcPicView = new Rectangle(ptPicView.X, ptPicView.Y, szPicView.Width, szPicView.Height);
                
                if (CRect.IsIntersectPoint(rcPicView, dropX, dropY))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    ThreadCall_LoadImage(files[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion  

        //*****************************************************************************************
        // Outer Call Functions
        //*****************************************************************************************

        public void VIEW_ImageSave()
        {
            if (!m_bViewerInitState)
                return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "BMP files (*.BMP)|*.bmp|JPG files (*.JPG)|*.JPG | PNG files (*.PNG) | *.PNG | All files (*.*)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            Bitmap bmp = GetDisplay_Bmp();

            ThreadCall_SaveImage(dlg.FileName, bmp);
        }
        public void VIEW_ImageLoad()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "BMP files (*.BMP)|*.bmp|JPG files (*.JPG)|*.JPG|PNG files (*.PNG)|*.PNG|All files (*.*)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            ThreadCall_LoadImage(dlg.FileName);
        }
      
        public void VIEW_SET_Mag_Origin()
        {
            Graphics g = this.CreateGraphics();

            int imageW = 0; int imageH = 0;
            int nViewH = 0; int nViewW = 0;

            lock (lockerDisp)
            {
                imageW = m_bmp.Width; imageH = m_bmp.Height;
            }

            bool bHorizontalImage = imageW >= imageH ? true : false;

            double fResolution = 0;

            if (bHorizontalImage == true) //  horizontal & vertical  divergence 161228
            {
                lock (lockerDisp) fResolution = m_bmp.HorizontalResolution;
                lock (lockerView) nViewW = PIC_VIEW.Width;

                m_INFO_fMagnification = ((float)nViewW / (float)imageW) * (fResolution / g.DpiX);

            }
            else if (bHorizontalImage == false)
            {
                lock (lockerDisp) fResolution = m_bmp.VerticalResolution;
                lock (lockerView) nViewH = PIC_VIEW.Height;

                m_INFO_fMagnification = ((float)nViewH / (float)imageH) * (fResolution / g.DpiX);
            }

            g.Dispose();

            m_VirtualImgPosX = 0;
            m_VirtualImgPosY = 0;

            m_bPanning = true; // panning status 원복 161215 
            BTN_VIEW_PANNING_Click(null, EventArgs.Empty); // 170803 fuck exception 

            // 원배율 복구니까 화면 갱신해야지
            this.UIThread(delegate { PIC_VIEW.Refresh(); });
        }
        public void VIEW_Set_Mag_Plus()
        {
            m_INFO_fMagnification += m_INFO_fMagScale;
            this.UIThread(delegate { PIC_VIEW.Refresh(); });
        }
        public void VIEW_Set_Mag_Minus()
        {
            m_INFO_fMagnification -= m_INFO_fMagScale;

            // exception for the zero assignment
            if (m_INFO_fMagnification < 0)
                m_INFO_fMagnification = Math.Max(m_INFO_fMagnification - m_INFO_fMagScale, m_INFO_fMagScale);

            this.UIThread(delegate { PIC_VIEW.Refresh(); });
        }

        public void VIEW_Set_Clear_DispObject()
        {
            m_dispObj.Clear();
        }
        public bool VIEW_Set_Panning()
        {
            m_bPanning = !m_bPanning;
            return m_bPanning;
        }
        public bool bDrawOveralay { get { return m_bDrawOverlay; } }
        public void VIEW_Set_Overlay(bool bDraw)
        {
            m_bDrawOverlay = bDraw;
        }

        //*****************************************************************************************
        // mouse related 
        //*****************************************************************************************

        private void PIC_VIEW_MouseEnter(object sender, EventArgs e)
        {
            PIC_VIEW.Focus();

        }
        private void PIC_VIEW_MouseUp(object sender, MouseEventArgs e)
        {
            m_bMousePressed = false;
        }
        private void PIC_VIEW_MouseDown(object sender, MouseEventArgs e)
        {
            PIC_VIEW.Focus();

            // 170803 mouse middle button down
            if (e.Button == MouseButtons.Middle)
            {
                this.VIEW_SET_Mag_Origin();
            }

            try
            {
                MouseEventArgs mouse = e as MouseEventArgs;

                int nCompensatedImagePosX = GetCompensated_ImageCroodX(mouse.Location.X);
                int nCompensatedImagePosY = GetCompensated_ImageCroodY(mouse.Location.Y);
                int nCompensatedViewPosX = GetCompensated_ViewCroodX(mouse.Location.X);
                int nCompensatedViewPosY = GetCompensated_ViewCroodY(mouse.Location.Y);
                //------------------------------------------------------------------------------------
                //                                  Left button
                //------------------------------------------------------------------------------------
                if (mouse.Button == MouseButtons.Left)
                {

                    if (!m_bMousePressed) // mouse를 방금 눌렀다고??
                    {
                        m_bMousePressed = true;
                        m_ptMouseClicked = mouse.Location; // mouse click

                        m_nPanStartX = m_VirtualImgPosX;
                        m_nPanStartY = m_VirtualImgPosY;

                        #region DRAW ROI
                        // ROI를 그리기 위해 클릭을 했어 - 시작점을 기록하자
                        if (m_bDrawROI && m_bROIRegistered == false)
                        {
                            m_bROIRegistered = true;
                            m_ptRoiStart = new Point( nCompensatedImagePosX, nCompensatedImagePosY);
                        }
                        // ROI를 그리고 있다가 클릭을 했어 - 사각형이 완성됬네 저장하자
                        else if (m_bDrawROI && m_bROIRegistered == true)
                        {
                            m_bROIRegistered = false;
                            m_bDrawROI = false;

                            m_ptRealTimeMousePos = new Point(nCompensatedImagePosX, nCompensatedImagePosY);

                            Rectangle rcDrawn = CRect.GenRect(m_ptRoiStart, m_ptRealTimeMousePos);
                            
                            if( CRect.IsIntersectPoint( rcDrawn, m_ptRealTimeMousePos) == true)
                            {
                                // add roi
                                ROI_Update_Temp(rcDrawn);
                            }
                            this.UIThread(delegate { PIC_VIEW.Refresh(); });

                        } // else if (m_bDrawROI && m_bROIRegistered == true)
                        #endregion

                    } // if (!m_bMousePressed) 

                    // 팝업창 떠있으면 집으로 보내.
                    if (popup != null) popup.Close();

                } // if (mouse.Button == MouseButtons.Left)
                else if (mouse.Button == MouseButtons.Right)
                {
                    bool ROI등록한거냐 = false;

                    int imageW = 0;
                    int imageH = 0;

                    lock (lockerDisp)
                    {
                        imageW = m_bmp.Width;
                        imageH = m_bmp.Height;
                    }

                    Point ptClicked = new Point(nCompensatedImagePosX, nCompensatedImagePosY);// 니가 클릭한거
                    Rectangle rcSelected = ROI_GetDataTemp();

                    if (CRect.IsIntersectPoint(rcSelected, ptClicked) == true)
                    {
                        if (m_bDrawPTRN == true)
                        {
                            ROI_PTRN = rcSelected;
                            m_bDrawPTRN = false;
                        }
                        else if (m_bDrawFocus == true) 
                        {
                            ROI_FOCUS = rcSelected;
                            m_bDrawFocus = false;
                        }

                        // Limitation : only one roi is allowed.
                        //ROI_Clear();
                        //ROI_ADD(rcSelected);
                        
                        // reset roi point data and status variables
                        m_ptRoiStart = new Point(-1, -1);
                        m_bROIRegistered = m_bDrawROI = false;

                        // 필요 없으니 지워 tempROI
                        ROI_Del_Temp();

                        ROI등록한거냐 = true;
                    }


                    if (ROI등록한거냐 == false) // 니가 짚은 ROI가 뭔지 보여주고 팝업을 띄워봐
                    {
                        if (popup != null) popup.Dispose();

                        /***/if(BOOL_TEACHING_ACTIVATION == true) { PT_FIGURE_TO_DRAW = ptClicked; }
                        else if(BOOL_TEACHING_ACTIVATION == false){ PT_FIGURE_TO_DRAW = new PointF(0, 0); }

                        Point actualPopupPos = PIC_VIEW.PointToScreen(new Point(nCompensatedViewPosX, nCompensatedViewPosY));

                        Point ptDlg = new Point(this.Location.X+this.Width-20, this.Location.Y+15);
                        ptDlg = PIC_VIEW.PointToScreen(ptDlg);
                        ROI_INDEX = iGet_Roi_Index( ptClicked);

                        if (ROI_INDEX == -1)
                        {
                            list_ROI_Temp.Clear();
                        }

                        // temporary fixed . D o N o T S h o W. 170717 
                        //popup = new FormPopup(this as iPopupROI, ptClicked , ptDlg);
                        //popup.Show();

                        m_ptRoiStart = new Point(-1, -1);
                        m_bROIRegistered = m_bDrawROI = false;

                    } // if (ROI등록한거냐 == false)
                }

            }
            catch { }
        }
        
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (m_bPanning == false) return; 

            float oldzoom = (float)m_INFO_fMagnification;

          

            m_INFO_fMagnification += (e.Delta*0.4) > 0 ? m_INFO_fMagScale : -m_INFO_fMagScale;

            if (m_INFO_fMagnification > 5.0)
            {
                m_INFO_fMagnification = oldzoom;
                return;
            }
            else if (m_INFO_fMagnification < 0.01)
            {
                m_INFO_fMagnification = oldzoom;
                return;
            }

            int nViewW = 0; int imageW = 0;

            lock (lockerView) imageW = PIC_VIEW.Width;
            lock (lockerDisp) nViewW = m_bmp.Width;

            double fViewRatio = nViewW / imageW;

            MouseEventArgs mouse = e as MouseEventArgs;
            Point mousePosNow = mouse.Location;

            Point locationView = new Point(m_ptMouseClicked.X, m_ptMouseClicked.Y);

            this.UIThread(delegate
            {
                locationView.X = PIC_VIEW.Location.X;
                locationView.Y = PIC_VIEW.Location.Y;
            });


            int x = mousePosNow.X - locationView.X;
            int y = mousePosNow.Y - locationView.Y;

            int oldimagex = (int)(x / oldzoom);
            int oldimagey = (int)(y / oldzoom);
            int newimagex = (int)(x / m_INFO_fMagnification);
            int newimagey = (int)(y / m_INFO_fMagnification);

            m_VirtualImgPosX = newimagex - oldimagex + m_VirtualImgPosX;
            m_VirtualImgPosY = newimagey - oldimagey + m_VirtualImgPosY;


            // 휠에 대한 Zoom in Zoom out 후 화면 갱신해야되지요
            this.UIThread(delegate { PIC_VIEW.Refresh(); });
        }
        private void PIC_VIEW_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //MouseEventArgs mouse = e as MouseEventArgs;

                if (e.Location.X < 0 || e.Location.Y < 0 || e.Location.X > PIC_VIEW.Size.Width || e.Location.Y > PIC_VIEW.Size.Height)
                    return;

                Point mousePosNow = e.Location;// mouse.Location;
                try
                {
                    // 여기는 패닝 부분입니다.
                    if (m_bPanning)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            int deltaX = mousePosNow.X - m_ptMouseClicked.X;// - m_ptMouseClickedUp.X;
                            int deltaY = mousePosNow.Y - m_ptMouseClicked.Y;// -m_ptMouseClickedUp.Y;

                            m_VirtualImgPosX = (int)(m_nPanStartX + (deltaX / m_INFO_fMagnification));
                            m_VirtualImgPosY = (int)(m_nPanStartY + (deltaY / m_INFO_fMagnification));

                        }
                    }

                    // 마우스 위치를 받고, 영상 좌표에 대한 보상값을 바꾸고
                    m_ptRealTimeMousePos.X = GetCompensated_ImageCroodX(mousePosNow.X);
                    m_ptRealTimeMousePos.Y = GetCompensated_ImageCroodY(mousePosNow.Y);

                    int nPixelValue = 0;

                    lock (lockerDisp)
                    {
                        byte[] bPixel = {0};

                        BitmapData bitmapData = ((Bitmap)m_bmp).LockBits(new Rectangle(0, 0, m_bmp.Width, m_bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                        {
                            int imageW = bitmapData.Width;
                            int imageH = bitmapData.Height;
                            int nStride = Math.Abs(bitmapData.Stride);

                            if (m_ptRealTimeMousePos.X >= 0 && m_ptRealTimeMousePos.X < imageW && m_ptRealTimeMousePos.Y >= 0 && m_ptRealTimeMousePos.Y < imageH)
                            {
                                IntPtr ptr = bitmapData.Scan0;
                                ptr += m_ptRealTimeMousePos.Y * nStride + m_ptRealTimeMousePos.X * 3;
                                System.Runtime.InteropServices.Marshal.Copy(ptr, bPixel, 0, 1);
                            }
                        }
                        ((Bitmap)m_bmp).UnlockBits(bitmapData);

                        nPixelValue = bPixel[0];
                    }

                    this.UIThread(delegate 
                    {
                        LB_VIEW_PIXEL_VALUE.Text = nPixelValue.ToString("N:000");
                        LB_VIEW_MOUSE_POS_X.Text = string.Format("X : {0:0}", m_ptRealTimeMousePos.X);
                        LB_VIEW_MOUSE_POS_Y.Text = string.Format("Y : {0:0}", m_ptRealTimeMousePos.Y);
                    });
                    
                   
                    // ROI를 그려보겠어요.; 그리고 화면을 갱신해 줍니다.
                    if ( m_bEditRoi == true || m_bROIRegistered == true)
                    {
                        m_ptRealTimeMousePos.X = GetCompensated_ImageCroodX(mousePosNow.X);
                        m_ptRealTimeMousePos.Y = GetCompensated_ImageCroodY(mousePosNow.Y);

                        int minX = Math.Min(m_ptRealTimeMousePos.X, m_ptRoiStart.X);
                        int maxX = Math.Max(m_ptRealTimeMousePos.X, m_ptRoiStart.X);
                        int minY = Math.Min(m_ptRealTimeMousePos.Y, m_ptRoiStart.Y);
                        int maxY = Math.Max(m_ptRealTimeMousePos.Y, m_ptRoiStart.Y);

                        Rectangle UserROI = new Rectangle(minX, minY, maxX- minX, maxY- minY);

                        ROI_Update_Temp(UserROI);
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex);
                }

                this.UIThread(delegate { PIC_VIEW.Refresh(); });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #region Croodinate Related
        // 뷰어 좌표에 대한 영상 좌표 획득 X
        private int GetCompensated_ImageCroodX(int x)
        {
            return Convert.ToInt32((m_VirtualImgPosX * -1) + (x * (1.0 / m_INFO_fMagnification)));
        }
        // 뷰어 좌표에 대한 영상 좌표 획득 Y
        private int GetCompensated_ImageCroodY(int y)
        {
            return Convert.ToInt32((m_VirtualImgPosY * -1) + (y * (1.0 / m_INFO_fMagnification)));
        }
        // 입력된 영상좌표가 배율이 변해도 뷰어에서 같은 위치이고 싶다.
        private int GetCompensated_ViewCroodX(int x)
        {
            double fPositionRatio = (double)x / (double)VIEW_GetImageW();
            double fMagnifiedLength = (double)VIEW_GetViewW() / m_INFO_fMagnification;
            return Convert.ToInt32(fMagnifiedLength * fPositionRatio);
        }
        // 입력된 영상좌표가 배율이 변해도 뷰어에서 같은 위치이고 싶다.
        private int GetCompensated_ViewCroodY(int y)
        {
            double fPositionRatio = (double)y / (double)VIEW_GetImageW();
            double fMagnifiedLength = (double)VIEW_GetViewH() / m_INFO_fMagnification;
            return Convert.ToInt32(fMagnifiedLength * fPositionRatio);
        }

        private int GetCompensated_ViewCroodY(int y, int nLimitY)
        {
            double fPositionRatio = (double)y / (double)nLimitY;
            double fMagnifiedLength = (double)VIEW_GetViewH() / m_INFO_fMagnification;
            return Convert.ToInt32(fMagnifiedLength * fPositionRatio);
        }

        // 영상 좌표에 대한 뷰어 좌표 획득 X
        private int GetCompensated_DefaultViewCroodX(int imageW, int x)
        {
            double fPositionRatio = (double)x / (double)imageW;
            double fMagnifiedLength = (double)VIEW_GetViewW() / GetDefaultMag();
            return Convert.ToInt32(fMagnifiedLength * fPositionRatio);
        }

        // 영상 좌표에 대한 뷰어 좌표 획득 Y
        private int GetCompensated_DefaultViewCroodY(int imageH, int y)
        {
            double fPositionRatio = (double)y / (double)imageH;
            double defaultMag = GetDefaultMag();
            double fMagnifiedLength = (double)VIEW_GetViewH() / defaultMag;
            return Convert.ToInt32(fMagnifiedLength * fPositionRatio);
        }
        #endregion
       

        private double GetDefaultMag()
        {
            Graphics g = this.CreateGraphics();
            int imageW = VIEW_GetImageW();
            int imageH = VIEW_GetImageH();

            double fHorizontalRes = 0;
            lock (lockerDisp) {fHorizontalRes = m_bmp.HorizontalResolution;}

            double dpiX = g.DpiX;
            g.Dispose();

            int nViewWidth = VIEW_GetViewW();
            return ((float)nViewWidth / (float)imageW) * (fHorizontalRes / dpiX);
        }


        //******************************************************************************************************
        // Outter Functions
        //******************************************************************************************************
        
        public IntPtr VIEW_GetHandle()
        {
            return PIC_VIEW.Handle;
        }
        public int VIEW_GetImageW()
        {
            int nImageW = 0;
            lock (lockerDisp){nImageW = m_bmp.Width;}
            return nImageW;
        }
        public int VIEW_GetImageH()
        {
            int nImageH = 0;
            lock (lockerDisp){nImageH = m_bmp.Height;}
            return nImageH;
        }
        public int VIEW_GetViewW()
        {
            int nViewW = 0;
            lock (lockerView) { nViewW = PIC_VIEW.Width; }
            return nViewW;
        }
        public int VIEW_GetViewH()
        {
            int nViewH = 0;
            lock (lockerView) { nViewH = PIC_VIEW.Height; }
            return nViewH;
        }

        //*****************************************************************************************
        // overlay functions
        //******************************************************************************************

        #region OVERLAY RELATED...
        public void DrawRect(double x, double y, double dx, double dy, Color c, float Thickness)
        {
            m_dispObj.InsertRect(x, y, dx, dy, c, Thickness);
        }
        public void DrawRect(RectangleF rect, Color c) { m_dispObj.InsertRect(rect.X, rect.Y, rect.Width, rect.Height, c, 2); }
        public void DrawRects(List<RectangleF> list, Color c)
        {
            foreach (RectangleF rc in list)
            {
                m_dispObj.InsertRect(rc.X, rc.Y, rc.Width, rc.Height, c, 2);
            }
        }
        public void DrawPoint(PointF pt, float size, float thick, Color c)
        {
            if (size == 1) size++; // size 2 is unvisible 170105
            m_dispObj.InsertPoint(pt.X, pt.Y, size, thick, c);
        }
        public void DrawPoint(double x, double y, float size, float thick, Color c)
        {
            if (size == 1) size++;// size 2 is unvisible 170105
            m_dispObj.InsertPoint((float)x, (float)y, size, thick, c);
        }
        public void DrawPoints(List<PointF> listPt, float size, float thick, Color c)
        {
            if (listPt != null)
            {
                if (size == 1) size++;// size 2 is unvisible 170105
                for (int i = 0; i < listPt.Count; i++)
                {
                    m_dispObj.InsertPoint(listPt.ElementAt(i).X, listPt.ElementAt(i).Y, size, thick, c);
                }
            }
        }
        public void DrawString(string msg, int x, int y, int size, Color c)
        {
            m_dispObj.InsertString(msg, x, y, size, c);
        }
        public void DrawString(List<RectangleF> listRects, Color c, int nSzie)
        {
            for (int i = 0; i < listRects.Count; i++)
            {
                m_dispObj.InsertString(i.ToString("N0"), (int)listRects.ElementAt(i).X, (int)listRects.ElementAt(i).Y, nSzie, c);
            }
        }
        public void DrawLine(PointF p1, PointF p2, double thick, Color c)
        {
            m_dispObj.InsertLine(p1, p2, c, (float)thick);
        }
        public void DrawLine(CLine line, double Thickness, Color c)
        {
            m_dispObj.InsertLine(line.P1, line.P2, c, (float)Thickness);
        }
        public void DrawCircle(RectangleF rc, Color color, double thick)
        {
            m_dispObj.InsertCircle(new PointF(rc.X, rc.Y), (float)(rc.Width / 2), (float)(rc.Height / 2), color, (float)thick);
        }
        public void DrawCircle(PointF pt, float rx, float ry, Color c, double thick)
        {
            m_dispObj.InsertCircle(pt, rx, ry, c, thick);
        }
        public void DrawBlob(int nID, int nType, float cx, float cy, RectangleF rc, List<PointF> ptArr)
        {
            m_dispObj.InsertBlob(nID, nType, new PointF(cx, cy), rc, ptArr);
        }
        public void DrawPatternMathcing(PointF PtCenter, RectangleF rcTemplate)
        {
            this.DrawPoint(PtCenter, 100, (float)5, Color.Magenta);
            this.DrawRect(rcTemplate.X, rcTemplate.Y, rcTemplate.Width, rcTemplate.Height, Color.Magenta, (float)5);
        }
        #endregion 

        private void PIC_VIEW_Paint(object sender, PaintEventArgs e)
        {
            #region draw
            
            try
            {
                int imageW = 0;
                int imageH = 0;

                lock (lockerDisp)
                {
                    imageW = m_bmp.Width;
                    imageH = m_bmp.Height;
                }

                if (imageW == 0 || imageH == 0)
                {
                    return;
                }

                //***************************************************************************************
                // Step 1 :: Image Transformation and Interpolation
                //***************************************************************************************

               // e.Graphics.Clear(Color.Black);
                // e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                //e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                //e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;

                e.Graphics.ScaleTransform((float)m_INFO_fMagnification, (float)m_INFO_fMagnification);

                //***************************************************************************************
                // 2 :: Image Setting 
                //***************************************************************************************

                float fTransX = m_VirtualImgPosX;
                float fTransY = m_VirtualImgPosY;

                lock (lockerDisp)
                {
                    e.Graphics.DrawImage(m_bmp, fTransX, fTransY);

                    if (BOOL_DRAW_PTRN_ROI == true)
                    {
                        Pen penCyan = new Pen(Brushes.Cyan, 5);
                        if (ROI_PTRN.Width == ROI_PTRN.Height && ROI_PTRN.Width == 0)
                        {
                            e.Graphics.DrawRectangle(penCyan, new Rectangle((int)fTransX, (int)fTransY, imageW, imageH));
                        }
                        else
                        {
                            RectangleF rcPtrn = ROI_PTRN;
                            rcPtrn.Offset(fTransX, fTransY);
                            e.Graphics.DrawRectangle(penCyan, Rectangle.Round(rcPtrn));
                        }
                        penCyan.Dispose();
                    }
                }

                //***************************************************************************************
                // Step 4 :: Draw Display Object with replica ondraw
                //***************************************************************************************

                if (m_bDrawOverlay == true)
                {
                    DrawOverlay(m_dispObj, e, fTransX, fTransY);
                    DrawMeasures(e, fTransX, fTransY);
                    DrawViewCross(e, imageW, imageH, fTransX, fTransY);

                    Pen penYellow = new Pen(Color.Yellow);
                    Pen penOrange = new Pen(Color.Orange);
                    Pen penBlue = new Pen(Color.Blue);
                    Pen penCyan = new Pen(Color.Cyan, 4);
                    
                    // temporary rois
                    if (list_ROI_Temp.Count > 0)
                    {
                        Rectangle rc = ROI_GetDataTemp();
                        e.Graphics.DrawRectangle(penYellow, rc.X+(int)fTransX, rc.Y+(int)fTransY, rc.Width, rc.Height);

                        Point pt = new Point(m_ptRealTimeMousePos.X, m_ptRealTimeMousePos.Y);
                        pt = CPoint.OffsetPoint(pt, fTransX, fTransY);

                        using (Pen penGuideDotLine = new Pen(Color.AliceBlue))
                        {
                            penGuideDotLine.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                            e.Graphics.DrawLine(penGuideDotLine, new Point(0, pt.Y), new Point(imageW, pt.Y));
                            e.Graphics.DrawLine(penGuideDotLine, new Point(pt.X, 0), new Point(pt.X, imageH));
                        }
                    }

                    // fixed real rois --> modified to Fined single rectangle 170714
                    //if (list_ROI.Count > 0)
                    {
                        //foreach (Rectangle rc in list_ROI)
                        {
                            Rectangle rc = ROI_FOCUS;
                            e.Graphics.DrawRectangle(penOrange, rc.X + (int)fTransX, rc.Y + (int)fTransY, rc.Width, rc.Height);
                        }
                    }
                    penYellow.Dispose();
                    penBlue.Dispose();
                    penOrange.Dispose();
                    penCyan.Dispose();
                }
             
                //***************************************************************************************
                // Step 5 :: Recover Graphics Transform !! 
                //***************************************************************************************

                e.Graphics.ResetTransform();
                e.Graphics.TranslateTransform(0, 0, MatrixOrder.Append);
                e.Graphics.ScaleTransform((float)1.0, (float)1.0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            #endregion
             
        }
        private void DrawViewCross(PaintEventArgs e, int imageW, int imageH, float fTransX, float fTransY)
        {
            PointF V1 = new PointF(imageW / 2, 0);
            PointF V2 = new PointF(imageW / 2, imageH);
            PointF H1 = new PointF(0, imageH / 2);
            PointF H2 = new PointF(imageW, imageH / 2);

            V1 = CPoint.OffsetPoint(V1, fTransX, fTransY);
            V2 = CPoint.OffsetPoint(V2, fTransX, fTransY);
            H1 = CPoint.OffsetPoint(H1, fTransX, fTransY);
            H2 = CPoint.OffsetPoint(H2, fTransX, fTransY);

            Pen penRed = new Pen(Color.Red);
            e.Graphics.DrawLine(penRed, V1, V2);
            e.Graphics.DrawLine(penRed, H1, H2);

            penRed.Dispose();


        }

        private void DrawMeasures(PaintEventArgs e, float fTransX, float fTransY)
        {
            Pen penBlue = new Pen(Color.Blue, (float) 0.3);
            Pen penLime = new Pen(Color.LimeGreen, (float) 0.3);
            Pen penOrange = new Pen(Color.Orange, (float)3);
            Pen penCyan = new Pen(Color.Cyan, 3);
            Pen penRed=new Pen(Color.Red, (float) 3);

            //penYellow.StartCap = LineCap.RoundAnchor;
            penRed.EndCap = LineCap.ArrowAnchor;

            Brush brush = new SolidBrush(Color.LimeGreen);
            
            Font font = new Font("verdana", 10);
            //*************************************************************************************
            // Horizontal rectangle 
            //*************************************************************************************

            if( BOOL_TEACHING_ACTIVATION == true)
            {
                PointF[] arrFigureDrawingPoints = CPoint.GetCrossPointsOfLine(PT_FIGURE_TO_DRAW, 10);

                List<PointF> list = CPoint.OffsetPoints(arrFigureDrawingPoints.ToList(), fTransX, fTransY);
                using ( Pen penFigureDrawingPos = new Pen(Brushes.Cyan, 3))
                {
                    e.Graphics.DrawLine(penFigureDrawingPos, list[0], list[1]);
                    e.Graphics.DrawLine(penFigureDrawingPos, list[2], list[3]);

                    using(Pen penSubLine = new Pen(Brushes.Cyan, (float)0.5))
                    {
                        PointF p1 = CPoint.OffsetPoint(list[0],-100,    0);
                        PointF p2 = CPoint.OffsetPoint(list[1],+100,    0);
                        PointF p3 = CPoint.OffsetPoint(list[2],   0, -100);
                        PointF p4 = CPoint.OffsetPoint(list[3],   0, +100);

                        e.Graphics.DrawLine(penSubLine, p1, p2);
                        e.Graphics.DrawLine(penSubLine, p3, p4);
                    }
                }

            }
            
            //*************************************************************************************
            // Digonal rectangle 
            //*************************************************************************************

            if (BOOL_DRAW_FOCUS_ROI == true)
            {
                fm.SetRelativeMovemntFocusRect();
                RectangleF rcFocus = fm.RC_FOCUS;
                e.Graphics.DrawRectangle(penCyan, rcFocus.X + (int)fTransX, rcFocus.Y + (int)fTransY, rcFocus.Width, rcFocus.Height);

                if (rcFocus.Width == 0 || rcFocus.Height == 0)
                {
                    e.Graphics.DrawRectangle(penCyan, fTransX - 10, fTransY - 10, 20, 20);
                }
            }

            for (int i = 0; i < fm.COUNT_PAIR_RCT; i++)
            {
                CMeasurePairRct single = fm.ElementAt_PairRct(i);

                parseRect rcFirst = single.rc_FST;
                parseRect rcSecon = single.rc_SCD;

                List<PointF> listFirst = rcFirst.ToArray();
                List<PointF> listSecon = rcSecon.ToArray();

                listFirst = CPoint.OffsetPoints(listFirst, fTransX, fTransY);
                listSecon = CPoint.OffsetPoints(listSecon, fTransX, fTransY);

                //e.Graphics.DrawLine(new Pen(Color.Yellow, 5), rcFirst.LT.off, rcFirst.RT);

                if (single.RC_TYPE == IFX_RECT_TYPE.DIR_HOR)
                {
                    e.Graphics.DrawLine(penRed, CPoint.OffsetPoint(rcFirst.LT, fTransX, fTransY), CPoint.OffsetPoint(rcFirst.LB, fTransX, fTransY));
                    e.Graphics.DrawLine(penRed, CPoint.OffsetPoint(rcSecon.LB, fTransX, fTransY), CPoint.OffsetPoint(rcSecon.LT, fTransX, fTransY));
                }
                else if (single.RC_TYPE == IFX_RECT_TYPE.DIR_VER)
                {
                    e.Graphics.DrawLine(penRed, CPoint.OffsetPoint(rcFirst.LT, fTransX, fTransY), CPoint.OffsetPoint(rcFirst.RT, fTransX, fTransY));
                    e.Graphics.DrawLine(penRed, CPoint.OffsetPoint(rcSecon.RT, fTransX, fTransY), CPoint.OffsetPoint(rcSecon.LT, fTransX, fTransY));
                }
                else if (single.RC_TYPE == IFX_RECT_TYPE.DIR_DIA)
                {
                    e.Graphics.DrawLine(penRed, CPoint.OffsetPoint(rcFirst.LT, fTransX, fTransY), CPoint.OffsetPoint(rcFirst.RT, fTransX, fTransY));
                    e.Graphics.DrawLine(penRed, CPoint.OffsetPoint(rcSecon.RT, fTransX, fTransY), CPoint.OffsetPoint(rcSecon.LT, fTransX, fTransY));
                }
                e.Graphics.DrawPolygon(penLime, listFirst.ToArray());
                e.Graphics.DrawPolygon(penLime, listSecon.ToArray());
                e.Graphics.DrawString(string.Format("{0}_{1}", i, single.NICKNAME), font, brush, listFirst.ElementAt(0).X, listFirst.ElementAt(0).Y - 10);
                e.Graphics.DrawString(string.Format("{0}_{1}", i, single.NICKNAME), font, brush, listSecon.ElementAt(0).X, listSecon.ElementAt(0).Y - 10);
            }

            //*************************************************************************************
            // Circle
            //*************************************************************************************
            for( int i = 0; i <fm.COUNT_PAIR_CIR; i++ )
            {
                CMeasurePairCir single = fm.ElementAt_PairCir(i);

                //RectangleF rcEX = single.GetCompensatedRect(single.rc_EX);
                //RectangleF rcIN = single.GetCompensatedRect(single.rc_IN);
                RectangleF rcEX = single.rc_EX;
                RectangleF rcIN = single.rc_IN;

                rcEX.Offset(fTransX, fTransY);
                rcIN.Offset(fTransX, fTransY);

                if (single.UI_SELECTED == true)
                {
                    e.Graphics.DrawEllipse(penOrange, rcEX);
                    e.Graphics.DrawEllipse(penOrange, rcIN);
                }
                else
                {
                    e.Graphics.DrawEllipse(penLime, rcEX);
                    e.Graphics.DrawEllipse(penLime, rcIN);
                }
                e.Graphics.DrawString(string.Format("{0}_{1}", i, single.NICKNAME), font, brush, rcEX.X, rcEX.Y - 10);
            }

            //*************************************************************************************
            // Over Lay
            //*************************************************************************************

            for (int i = 0; i < fm.COUNT_PAIR_OVL; i++)
            {
                CMeasurePairOvl single = fm.ElementAt_PairOvl(i);

                RectangleF rcHOR_IN_L = single.RC_HOR_IN.rc_FST.ToRectangleF();
                RectangleF rcHOR_IN_R = single.RC_HOR_IN.rc_SCD.ToRectangleF();
                RectangleF rcHOR_EX_L = single.RC_HOR_EX.rc_FST.ToRectangleF();
                RectangleF rcHOR_EX_R = single.RC_HOR_EX.rc_SCD.ToRectangleF();

                RectangleF rcVER_IN_T = single.RC_VER_IN.rc_FST.ToRectangleF();
                RectangleF rcVER_IN_B = single.RC_VER_IN.rc_SCD.ToRectangleF();
                RectangleF rcVER_EX_T = single.RC_VER_EX.rc_FST.ToRectangleF();
                RectangleF rcVER_EX_B = single.RC_VER_EX.rc_SCD.ToRectangleF();

                rcHOR_IN_L.Offset(fTransX, fTransY);rcHOR_IN_R.Offset(fTransX, fTransY);
                rcHOR_EX_L.Offset(fTransX, fTransY);rcHOR_EX_R.Offset(fTransX, fTransY);

                rcVER_IN_T.Offset(fTransX, fTransY);rcVER_IN_B.Offset(fTransX, fTransY);
                rcVER_EX_T.Offset(fTransX, fTransY);rcVER_EX_B.Offset(fTransX, fTransY);


                e.Graphics.DrawRectangle(penLime, rcHOR_IN_L.X, rcHOR_IN_L.Y, rcHOR_IN_L.Width, rcHOR_IN_L.Height);
                e.Graphics.DrawRectangle(penLime, rcHOR_IN_R.X, rcHOR_IN_R.Y, rcHOR_IN_R.Width, rcHOR_IN_R.Height);
                e.Graphics.DrawRectangle(penLime, rcHOR_EX_L.X, rcHOR_EX_L.Y, rcHOR_EX_L.Width, rcHOR_EX_L.Height);
                e.Graphics.DrawRectangle(penLime, rcHOR_EX_R.X, rcHOR_EX_R.Y, rcHOR_EX_R.Width, rcHOR_EX_R.Height);

                e.Graphics.DrawRectangle(penLime, rcVER_IN_T.X, rcVER_IN_T.Y, rcVER_IN_T.Width, rcVER_IN_T.Height);
                e.Graphics.DrawRectangle(penLime, rcVER_IN_B.X, rcVER_IN_B.Y, rcVER_IN_B.Width, rcVER_IN_B.Height);
                e.Graphics.DrawRectangle(penLime, rcVER_EX_T.X, rcVER_EX_T.Y, rcVER_EX_T.Width, rcVER_EX_T.Height);
                e.Graphics.DrawRectangle(penLime, rcVER_EX_B.X, rcVER_EX_B.Y, rcVER_EX_B.Width, rcVER_EX_B.Height);

                e.Graphics.DrawString(string.Format("{0}_{1}", i, "HOR_IN_L"), font, brush, rcHOR_IN_L.X, rcHOR_IN_L.Y - 10);
                e.Graphics.DrawString(string.Format("{0}_{1}", i, "HOR_IN_R"), font, brush, rcHOR_IN_R.X, rcHOR_IN_R.Y - 10);
                e.Graphics.DrawString(string.Format("{0}_{1}", i, "HOR_EX_L"), font, brush, rcHOR_EX_L.X, rcHOR_EX_L.Y - 10);
                e.Graphics.DrawString(string.Format("{0}_{1}", i, "HOR_EX_R"), font, brush, rcHOR_EX_R.X, rcHOR_EX_R.Y - 10);

                e.Graphics.DrawString(string.Format("{0}_{1}", i, "VER_IN_T"), font, brush, rcVER_IN_T.X, rcVER_IN_T.Y - 10);
                e.Graphics.DrawString(string.Format("{0}_{1}", i, "VER_IN_B"), font, brush, rcVER_IN_B.X, rcVER_IN_B.Y - 10);
                e.Graphics.DrawString(string.Format("{0}_{1}", i, "VER_EX_T"), font, brush, rcVER_EX_T.X, rcVER_EX_T.Y - 10);
                e.Graphics.DrawString(string.Format("{0}_{1}", i, "VER_EX_B"), font, brush, rcVER_EX_B.X, rcVER_EX_B.Y - 10);
            }

            brush.Dispose();
            font.Dispose();
            penBlue.Dispose();
            penLime.Dispose();
            penOrange.Dispose();
            penCyan.Dispose();
            penRed.Dispose();
        }

        private void DrawOverlay(DispObj obj, PaintEventArgs e, float fTransX, float fTransY)
        {
            try
            {
                //****************************************************************
                DPoint[] arrPoint = obj.dispPoint.ToArray();
                //****************************************************************
                #region Point Drawing
                object pointLocker = new object();
                
                for( int i = 0; i < arrPoint.Length; i++)
                {
                    DPoint ptCur = arrPoint[i];

                    int nSize = (int)(ptCur.Size / 2);
                    lock (pointLocker)
                    {
                        Pen pen = new Pen(arrPoint[i].Color, arrPoint[i].Thick);
                        {
                            PointF[] arrCross = CPoint.GetCrossPointsOfLine(ptCur.ToPoint(), nSize);

                            e.Graphics.DrawLine(pen, CPoint.OffsetPoint(arrCross[0], fTransX, fTransY), CPoint.OffsetPoint(arrCross[1], fTransX, fTransY));
                            e.Graphics.DrawLine(pen, CPoint.OffsetPoint(arrCross[2], fTransX, fTransY), CPoint.OffsetPoint(arrCross[3], fTransX, fTransY));
                        }
                        pen.Dispose();
                    }
                    
                }
                #endregion  
                //****************************************************************
                DRect[] arrRect = obj.dispRect.ToArray();
                //****************************************************************
                #region Rect Drawing
                object rectLocker = new object();

                for( int i = 0; i < arrRect.Length; i++)
                {
                    DRect rect = arrRect[i].CopyTo(); 
                    rect.OffsetRect(fTransX, fTransY);

                    Rectangle rc = rect.ToRectangle();
                    lock (rectLocker)
                    {
                        Pen pen = new Pen(rect.Color);
                        e.Graphics.DrawRectangle(pen, rc);
                        pen.Dispose();
                    }
                } 
                #endregion  
                //****************************************************************
                DCircle[] arrCircle = obj.dispCircle.ToArray();
                //****************************************************************
                #region Circle Drawing
                for( int i = 0; i < arrCircle.Length; i++)
                {
                    DCircle circle = arrCircle[i].Copy();
                    circle.OffsetCircle(fTransX, fTransY);
                    using (Pen pen = new Pen(arrCircle[i].Color))
                    {
                        e.Graphics.DrawEllipse(pen, circle.ToRectangle());
                    }
                }
                #endregion
                //****************************************************************
                DLine[] arrLine = obj.dispLine.ToArray();
                //****************************************************************
                #region Line Drawing
                for( int i = 0; i < arrLine.Length; i++)
                {
                    DLine line = arrLine[i].CopyTo();
                    line.OffsetLine(fTransX, fTransY);
                    using (Pen pen = new Pen(line.Color, line.Thick))
                    {
                        e.Graphics.DrawLine(pen, line.P1, line.P2);
                    }
                }
                #endregion
                //****************************************************************
                DString[] arrString = obj.dispString.ToArray();
                //****************************************************************
                #region String Drawing
                for( int i = 0; i < arrString.Length; i++)
                {
                    DString str = arrString[i].Copy();
                    str.OffsetString(fTransX, fTransY);

                    Brush brush = new SolidBrush(str.Color);
                    
                    using( Font font = new Font("verdana", str.SIZE))
                    {
                        e.Graphics.DrawString(str.MSG, font, brush, (int)str.X, (int)str.Y);
                    }
                    brush.Dispose();
                }
                #endregion  
                //****************************************************************
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void BTN_VIEW_PANNING_Click(object sender, EventArgs e)
        {
            bool bPanning = VIEW_Set_Panning();

            this.UIThread(delegate
            {
                if/***/ (bPanning == true) { BTN_VIEW_PANNING.BackgroundImage = UC_CD_VIEW.Properties.Resources.joystick_on; }
                else if (bPanning == false) { BTN_VIEW_PANNING.BackgroundImage = UC_CD_VIEW.Properties.Resources.joystick_off; }
                Refresh();
            });
        }

        private void BTN_VIEW_DRAW_FIGURE_Click(object sender, EventArgs e)
        {
            bool bDraw = !BOOL_TEACHING_ACTIVATION;

            BOOL_TEACHING_ACTIVATION = bDraw;

            this.UIThread(delegate
            {
                if/***/ (bDraw == true) { BTN_VIEW_DRAW_FIGURE.BackgroundImage = UC_CD_VIEW.Properties.Resources.drawfigure_On; }
                else if (bDraw == false) { BTN_VIEW_DRAW_FIGURE.BackgroundImage = UC_CD_VIEW.Properties.Resources.DrawFigure_Off; }
                Refresh();
            });
        }

        private void BTN_VIEW_SET_OVERLAY_Click(object sender, EventArgs e)
        {
            bool bDraw = !this.bDrawOveralay;

            this.VIEW_Set_Overlay(bDraw);

            this.UIThread(delegate
            {
                if/***/ (bDraw == true) { BTN_VIEW_SET_OVERLAY.BackgroundImage  = UC_CD_VIEW.Properties.Resources.PaintBrush_On; }
                else if (bDraw == false) { BTN_VIEW_SET_OVERLAY.BackgroundImage = UC_CD_VIEW.Properties.Resources.PaintBrush_Off; }
                Refresh();
            });
        }

        private void BTN_VIEW_OVERLAY_CLEAR_Click(object sender, EventArgs e)
        {
            this.UIThread(delegate
            {
                this.VIEW_Set_Clear_DispObject();
                this.Refresh();
            });
        }

        private void BTN_VIEW_OVERLAY_MAG_ORG_Click(object sender, EventArgs e)
        {
            this.VIEW_SET_Mag_Origin();
            this.VIEW_Set_Panning();
            BTN_VIEW_PANNING_Click(null, EventArgs.Empty);
        }

        private void BTN_VIEW_MAG_PLUS_Click(object sender, EventArgs e)
        {
            this.VIEW_Set_Mag_Plus();
        }

        private void BTN_VIEW_MAG_MINUS_Click(object sender, EventArgs e)
        {
            this.VIEW_Set_Mag_Minus();
        }

        private void BTN_VIEW_IMAGE_LOAD_Click(object sender, EventArgs e)
        {
            this.VIEW_ImageLoad();
        }

        private void BTN_VIEW_IMAGE_SAVE_Click(object sender, EventArgs e)
        {
            this.VIEW_ImageSave();
        }

        private void BTN_VIEW_SAVE_OVERLAY_Click(object sender, EventArgs e)
        {
            this.SaveDisplayOvr(100);
        }

        private void UC_CD_VIEWER_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void PIC_VIEW_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(string.Format("[X,Y] = {0},{1}", e.Location.X, e.Location.Y));

            CMeasurePairCir[] arrCir = fm.ToArray_PairCir();

            int x = e.Location.X;
            int y = e.Location.Y;

            PointF ptClicked = new PointF(GetCompensated_ImageCroodX(x), GetCompensated_ImageCroodY(y));

            for (int i = 0; i < arrCir.Length; i++)
            {
                RectangleF rcCircle = arrCir[i].rc_EX;

                if (CRect.IsIntersectPoint(rcCircle, ptClicked) == true)
                {
                    Console.WriteLine(String.Format("{0} CircleFucker {1}",i, arrCir[i].NICKNAME) );

                    arrCir[i].UI_SELECTED = !arrCir[i].UI_SELECTED;
                }
            }
            fm.list_pair_Cir = arrCir.ToList();
        }
    }

    public class CFileIO
    {
        public event EventHandler<ThreadFinishedEventArgs> EventThreadFinishedFileLoad;
        public event EventHandler<ThreadFinishedEventArgs> EventThreadFinishedFileSave;

        public class ThreadFinishedEventArgs : EventArgs
        {
            public List<string> list = new List<string>();
            public string strFilePath = string.Empty;
        }

        public void LoadFile(string strPath, ThreadFinishedEventArgs e)
        {
            // to do 
            WrapperTextFile txt = new WrapperTextFile();

            txt.ReadTextFile(strPath);
            
            e.strFilePath = strPath;
            e.list = txt.GetAll();
             
            OnThreadFinishedImageLoad(e);
        }
        public void SaveFile(string strPath, List<string> list, ThreadFinishedEventArgs e)
        {
            WrapperTextFile txt = new WrapperTextFile();

            txt.SaveTextFile(strPath, list);

            // copy To e.
            OnThreadFinishedImageSave(e);
        }

        protected virtual void OnThreadFinishedImageLoad(ThreadFinishedEventArgs e)
        {
            EventHandler<ThreadFinishedEventArgs> handler = EventThreadFinishedFileLoad;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnThreadFinishedImageSave(ThreadFinishedEventArgs e)
        {
            EventHandler<ThreadFinishedEventArgs> handler = EventThreadFinishedFileSave;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class CImageIO
    {
        public event EventHandler<ThreadFinishedEventArgs> EventThreadFinishedImageLoad;
        public event EventHandler<ThreadFinishedEventArgs> EventThreadFinishedImageSave;

        public class ThreadFinishedEventArgs : EventArgs
        {
            public Bitmap bmp = null;
            public string strFilePath = string.Empty;
        }

        public void LoadImage(string strPath, ThreadFinishedEventArgs e)
        {
            strPath = strPath.ToUpper();

            string strExt = Path.GetExtension(strPath);

            if (strExt == ".PNG" || strExt == ".JPG" || strExt == ".JPEG" || strExt == ".BMP" ||  strExt == ".GIF" || strExt  == ".TIFF" || strExt == ".TIF")
            {
                if (File.Exists(strPath) == true)
                {
                    e.strFilePath = strPath;
                    Bitmap bmp = (Bitmap)Bitmap.FromFile(strPath);
                    e.bmp = new Bitmap(bmp);
                    bmp.Dispose();
                 }
                OnThreadFinishedImageLoad(e);
            }
        }
        public void SaveImage(string strPath, byte[] rawImage, int imageW, int imageH, ThreadFinishedEventArgs e)
        {
            Bitmap bmp = Computer.HC_CONV_Byte2Bmp(rawImage, imageW, imageH);

            this.SaveImage(strPath, bmp, e);
        }
        public void SaveImage(string strPath, Bitmap bmp, ThreadFinishedEventArgs e)
        {
            string strExt = Path.GetExtension(strPath.ToUpper());

            if( strExt.Contains("BMP")  == true)
            {
                bmp.Save(strPath, ImageFormat.Bmp);
            }
            else if( strExt.Contains("JPG") || strExt.Contains("JPEG") == true)
            {
                bmp.Save(strPath, ImageFormat.Jpeg);
            }
            else if( strExt.Contains("PNG") == true)
            {
                bmp.Save(strPath, ImageFormat.Png);
            }
            else if( strExt.Contains("TIFF")  == true|| strExt.Contains("TIF") == true)
            {
                bmp.Save(strPath, ImageFormat.Tiff);
            }
            else if( strExt.Contains("GIF") == true )
            {
                bmp.Save(strPath, ImageFormat.Gif);
            }
               
            e.strFilePath = strPath;
            OnThreadFinishedImageSave(e);
        }
        


        protected virtual void OnThreadFinishedImageLoad(ThreadFinishedEventArgs e)
        {
            EventHandler<ThreadFinishedEventArgs> handler = EventThreadFinishedImageLoad;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnThreadFinishedImageSave(ThreadFinishedEventArgs e)
        {
            EventHandler<ThreadFinishedEventArgs> handler = EventThreadFinishedImageSave;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}


