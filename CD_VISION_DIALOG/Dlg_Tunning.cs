using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CodeKing.Native;

using DEF_PARAMS;
using CD_View;
using CD_Figure;
using CD_Measure;
using DispObject;

namespace CD_VISION_DIALOG
{
    public partial class Dlg_Tunning : Form
    {
        const int TARGET_FIGURE_TYPE_CIR = 0;
        const int TARGEt_FIGURE_TYPE_REC = 1;

        public delegate void dele_FinishTunning();
        public event dele_FinishTunning eventDele_FinishTunning;

         public Bitmap MAIN_IMAGE = new Bitmap(100, 100);
         public CFigureManager fm = new CFigureManager();

         public CPeakMaster pm = new CPeakMaster();

        public Dlg_Tunning()
        {
            InitializeComponent();

            uc_tunning_view.SetInit();
            uc_tunning_view.eventDele_HereComesNewImage += new dele_HereComesNewImage(fuck);

            uc_thumb_nail_circle.ThumbNailSize = 128;
            uc_thumb_nail_circle.SetInit();

            uc_thumb_nail_rect.ThumbNailSize = 128;
            uc_thumb_nail_rect.SetInit();

            uc_tunning_view.BOOL_DRAW_FOCUS_ROI = false;
            uc_tunning_view.BOOL_DRAW_PTRN_ROI = false;

            
        }

        public bool SetParam(CFigureManager fm, Bitmap bmp)
        {
            MAIN_IMAGE = bmp.Clone() as Bitmap;

            uc_tunning_view.SetDisplay(MAIN_IMAGE);
            uc_tunning_view.VIEW_SET_Mag_Origin();

            int imageW = 0;
            int imageH = 0;
            byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp.Clone() as Bitmap, ref imageW, ref imageH);

            List<Bitmap> listImageCircle = new List<Bitmap>();
            List<string> listFilesCircle = new List<string>();

            List<Bitmap> listImageRect = new List<Bitmap>();
            List<string> listFilesRect = new List<string>();

            this.fm = fm;

            //********************************************************************************************
            // For Circle
            //********************************************************************************************

            for (int i = 0; i < this.fm.COUNT_PAIR_CIR; i++)
            {
                CMeasurePairCir single = this.fm.list_pair_Cir.ElementAt(i);

                Rectangle rc = Rectangle.Round(single.rc_EX);

                uc_tunning_view.DrawRect(rc, Color.Red);
                uc_tunning_view.DrawString(single.NICKNAME, rc.X, rc.Y-10, 10, Color.Yellow);

                byte[] rawCrop = Computer.HC_CropImage(rawImage, imageW, imageH, rc);
                int rcW = rc.Width;
                int rcH = rc.Height;

                Bitmap bmpCrop = Computer.HC_CONV_Byte2Bmp(rawCrop, rcW, rcH);

                listFilesCircle.Add(single.NICKNAME);
                listImageCircle.Add(bmpCrop);
            }
            uc_thumb_nail_circle.LoadImages(listImageCircle.ToArray(), listFilesCircle);

            //********************************************************************************************
            // For Rect
            //********************************************************************************************

            CHK_RECT_USE_AUTO_PEAK_DETECTION.Checked = true;
            CHK_RECT_USE_AUTO_PEAK_DETECTION_CheckedChanged(null, EventArgs.Empty);

            for (int i = 0; i < this.fm.COUNT_PAIR_RCT; i++)
            {
                CMeasurePairRct single = this.fm.list_pair_Rct.ElementAt(i);

                RectangleF rcFST = single.rc_FST.ToRectangleF();
                RectangleF rcSCD = single.rc_SCD.ToRectangleF();

                RectangleF rcMerged = CRect.GetMergedRect(rcFST, rcSCD);

                uc_tunning_view.DrawRect(rcMerged, Color.Red);
                uc_tunning_view.DrawString(single.NICKNAME, (int)(rcMerged.X), (int)(rcMerged.Y - 10), 10, Color.Yellow);

                byte[] rawCrop = Computer.HC_CropImage(rawImage, imageW, imageH, rcMerged);
                int rcW = (int)rcMerged.Width;
                int rcH = (int)rcMerged.Height;

                Bitmap bmpCrop = Computer.HC_CONV_Byte2Bmp(rawCrop, rcW, rcH);

                listFilesRect.Add(single.NICKNAME);
                listImageRect.Add(bmpCrop);
            }
            uc_thumb_nail_rect.LoadImages(listImageRect.ToArray(), listFilesRect);

            uc_tunning_view.fm.RC_FOCUS = new RectangleF(-1, -1, -1, -1);
            uc_tunning_view.fm.param_ptrn.RC_SEARCH_RGN = new RectangleF(-1, -1, -1, -1);
             return true;
        }

        private void Dlg_Tunning_Load(object sender, EventArgs e)
        {
 
        }

        private void fuck()
        {

        }
        #region glass effect
        // defines how far we are extending the Glass margins
        private Win32.MARGINS margins;
        private int padding_TOP = 5;
        private int padding_LFT = 5;
        private int padding_RHT = 5;
        private int padding_BTM = 5;
        /// <summary>
        /// Override the onload, and define our Glass margins
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Win32.DwmIsCompositionEnabled())
            {
                MessageBox.Show("This demo requires Vista, with Aero enabled.");
                Application.Exit();
            }
            SetGlassRegion();
        }
        /// <summary>
        /// Use the form padding values to define a Glass margin
        /// </summary>
        private void SetGlassRegion()
        {
            // Set up the glass effect using padding as the defining glass region
            if (Win32.DwmIsCompositionEnabled())
            {
                margins = new Win32.MARGINS();
                margins.Top = padding_TOP;
                margins.Left = padding_LFT;
                margins.Bottom = padding_BTM;
                margins.Right = padding_RHT;
                Win32.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
        }
        /// <summary>
        /// Override the OnPaintBackground method, to draw the desired
        /// Glass regions black and display as Glass
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Win32.DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.Black);
                // put back the original form background for non-glass area
                Rectangle clientArea = new Rectangle(
                margins.Left,
                margins.Top,
                this.ClientRectangle.Width - margins.Left - margins.Right,
                this.ClientRectangle.Height - margins.Top - margins.Bottom);
                Brush b = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(b, clientArea);
            }
        }

        #endregion

        private void BTN_PTRN_APPLY_Click(object sender, EventArgs e)
        {
            eventDele_FinishTunning();

            this.Hide();
        }

        private void BTN_RECOVER_Click(object sender, EventArgs e)
        {
            uc_tunning_view.SetDisplay(MAIN_IMAGE.Clone() as Bitmap);
            uc_tunning_view.Refresh();
        }

        private void uc_thumb_nail_DoubleClick(object sender, EventArgs e)
        {
            int nIndex = uc_thumb_nail_circle.FocusedItem.Index;
            Bitmap bmp = uc_thumb_nail_circle.GetImageOriginal(nIndex);

            CMeasurePairCir single = fm.ElementAt_PairCir(nIndex);

            // 01 Target Info
            TXT_CIRCLE_SELECTED_FIGURE.Text = single.NICKNAME;
            TXT_CIRCLE_SELECTED_INDEX.Text = nIndex.ToString("N0");

            // 00 Algorithm 
            if (single.param_00_algorithm_CIR == IFX_ALGORITHM.MEXHAT) { RDO_CIR_ALGO_MEXHAT.Checked = true; }
            if (single.param_00_algorithm_CIR == IFX_ALGORITHM.CARDIN) { RDO_CIR_ALGO_CARDIN.Checked = true; }
            if (single.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_EX) { RDO_CIR_ALGO_DIR_EX.Checked = true; }
            if (single.param_00_algorithm_CIR == IFX_ALGORITHM.DIR_IN) { RDO_CIR_ALGO_DIR_IN.Checked = true; }

            // 01 Damage Tolerance 
            TXT_CIR_DMG_TOLERANCE.Text = single.param_01_DMG_Tol.ToString("F2");

            // 02 Circle Shape
            CHK_CIR_TREAT_AS_ELLIPSE.Checked = single.param_02_BOOL_TREAT_AS_ELLIPSE;

            // 03 Auto Circle Detection

            int nValue = _FromUI_GetCircleDetectionType();
            _CHANGE_AUTO_CIRCLE_DETECTION_TYPE(nValue);

            // 04 Shrinkage 
            TXT_CIR_SHRINKAGE.Text = single.param_04_Shrinkage.ToString("F2");

            // 05 outlier filter
            TXT_CIR_OUTLIER_FILTER.Text = single.param_05_Outlier_Filter.ToString("N0");


            // 00 Edge Position
            TXT_CIR_EDGE_POSITION.Text = single.param_06_EdgePos.ToString("N0");

            // 0102 Compensation
            TXT_CIR_COMPEN_A.Text = single.param_comm_01_compen_A.ToString("F2");
            TXT_CIR_COMPEN_B.Text = single.param_comm_02_compen_B.ToString("F2");

            TXT_CIR_COVERAGE.Text = single.param_07_Coverage;

            // 03 Show Raw Data
            CHK_CIR_SHOW_RAW_DATA.Checked = single.param_comm_04_show_raw_data;

            uc_tunning_view.VIEW_Set_Clear_DispObject();
            uc_tunning_view.SetDisplay(bmp);
            uc_tunning_view.VIEW_SET_Mag_Origin();
        }

        private CMeasurePairCir _FromUI_GetParameters_Circle()
        {
            double fValue = 0; int nValue = 0;

            CMeasurePairCir single = new CMeasurePairCir();

            // algorithm 
            single.param_00_algorithm_CIR = _FromUI_GetMeasureAlgorithm(TARGET_FIGURE_TYPE_CIR);

            // damge tolerance
            double.TryParse(TXT_CIR_DMG_TOLERANCE.Text, out fValue); single.param_01_DMG_Tol = fValue;

            // ellipse or circle
            single.param_02_BOOL_TREAT_AS_ELLIPSE = CHK_CIR_TREAT_AS_ELLIPSE.Checked;

            // auto circle detection
            single.param_03_CircleDetecType = _FromUI_GetCircleDetectionType();

            // shrinkage
            single.param_04_Shrinkage = Convert.ToDouble(TXT_CIR_SHRINKAGE.Value);

            // outlier filter
            int.TryParse(TXT_CIR_OUTLIER_FILTER.Text, out nValue); single.param_05_Outlier_Filter = nValue;

            // edge position
            double.TryParse(TXT_CIR_EDGE_POSITION.Text, out fValue); single.param_06_EdgePos = fValue;

            // compensation
            double.TryParse(TXT_CIR_COMPEN_A.Text, out fValue); single.param_comm_01_compen_A = fValue;
            double.TryParse(TXT_CIR_COMPEN_B.Text, out fValue); single.param_comm_02_compen_B = fValue;

            int.TryParse(CB_CIR_SPC_ENHANCEMENT.Text, out nValue); single.param_comm_03_spc_enhance = nValue;

            // show raw data
            single.param_comm_04_show_raw_data = CHK_CIR_SHOW_RAW_DATA.Checked;

            return single;
        }
        private CMeasurePairRct _FromUI_GetParameters_Rect()
        {
            double fValue = 0; int nValue = 0;

            CMeasurePairRct single = new CMeasurePairRct();

            // algorithm 
            single.param_00_algorithm = _FromUI_GetMeasureAlgorithm(TARGEt_FIGURE_TYPE_REC);

            single.param_03_bool_Use_AutoDetection = CHK_RECT_USE_AUTO_PEAK_DETECTION.Checked;

            // edge detection target for each rectangles
            int.TryParse(TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Text, out nValue); single.param_04_peakTargetIndex_fst = nValue;
            int.TryParse(TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Text, out nValue); single.param_05_peakTargetIndex_scd = nValue;

            // candidate count 
            int.TryParse(TXT_RECT_CANDIDATE_COUNT.Text, out nValue); single.param_06_peakCandidate = nValue;

            // profile filtering window size 
            int.TryParse(TXT_RECT_WINDOW_SIZE.Text, out nValue); single.param_07_windowSize = nValue;

            // edge positions for each rectangles
            int.TryParse(TXT_RECT_EDGE_POSITION_FST.Text, out nValue); single.param_08_edge_position_fst = nValue;
            int.TryParse(TXT_RECT_EDGE_POSITION_SCD.Text, out nValue); single.param_09_edge_position_scd = nValue;

            // outlier filtering refinement value 
            int.TryParse(TXT_RECT_EDGE_REFINEMENT.Text, out nValue); single.param_10_refinement = nValue;

            // spc enhancement value 
            int.TryParse(CB_RECT_SPC_ENHANCEMENT.Text, out nValue); single.param_comm_03_spc_enhance = nValue;

            // compensation 
            double.TryParse(TXT_RECT_COMPEN_A.Text, out fValue); single.param_comm_01_compen_A = fValue;
            double.TryParse(TXT_RECT_COMPEN_B.Text, out fValue); single.param_comm_02_compen_B = fValue;
            
            single.param_comm_04_show_raw_data = CHK_RECT_SHOW_RAW_DATA.Checked;
 


            return single;
        }
         
        private int _FromUI_GetMeasureAlgorithm(int nTargetFigureType)
        {
            int nAlgorithm = 0;

            if (nTargetFigureType == TARGET_FIGURE_TYPE_CIR)
            {
                if (RDO_CIR_ALGO_MEXHAT.Checked == true) nAlgorithm = IFX_ALGORITHM.MEXHAT;
                if (RDO_CIR_ALGO_CARDIN.Checked == true) nAlgorithm = IFX_ALGORITHM.CARDIN;
                if (RDO_CIR_ALGO_DIR_EX.Checked == true) nAlgorithm = IFX_ALGORITHM.DIR_EX;
                if (RDO_CIR_ALGO_DIR_IN.Checked == true) nAlgorithm = IFX_ALGORITHM.DIR_IN;
            }
            else if (nTargetFigureType == TARGEt_FIGURE_TYPE_REC)
            {
                if (RDO_RECT_ALGO_MEXHAT.Checked == true) nAlgorithm = IFX_ALGORITHM.MEXHAT;
                if (RDO_RECT_ALGO_CARDIN.Checked == true) nAlgorithm = IFX_ALGORITHM.CARDIN;
                if (RDO_RECT_ALGO_DIR_EX.Checked == true) nAlgorithm = IFX_ALGORITHM.DIR_EX;
                if (RDO_RECT_ALGO_DIR_IN.Checked == true) nAlgorithm = IFX_ALGORITHM.DIR_IN;
            }
            return nAlgorithm;
        }

        private int _FromUI_GetCircleDetectionType()
        {
            int nValue = 0;
            int.TryParse(CB_CIRCLE_AUTO_DETECTION_TYPE.Text, out nValue);
            return nValue; ;
        }

        private void _CHANGE_AUTO_CIRCLE_DETECTION_TYPE(int nType)
        {
            uc_tunning_view.VIEW_Set_Clear_DispObject();

            RectangleF rcRegion = new RectangleF();

            byte[] rawImage = uc_tunning_view.GetDisplay_Raw();
            int imageW = uc_tunning_view.VIEW_GetImageW();
            int imageH = uc_tunning_view.VIEW_GetImageH();

            double fShrinkage = 0;
            double.TryParse(TXT_CIR_SHRINKAGE.Text, out fShrinkage);

            rcRegion = Computer.HC_CIRCLE_CENTERING(rawImage, imageW, imageH, new RectangleF(0, 0, imageW, imageH), fShrinkage, nType);
            uc_tunning_view.DrawCircle(rcRegion, Color.Cyan, 1);
            uc_tunning_view.Refresh();
        }
         

        private void RDO_CIRCLE_DETEC_NONE_Click(object sender, EventArgs e){uc_tunning_view.VIEW_Set_Clear_DispObject();uc_tunning_view.Refresh();}
        private void RDO_CIRCLE_DETECT_POSSITIVE_Click(object sender, EventArgs e){_CHANGE_AUTO_CIRCLE_DETECTION_TYPE(1);}
        private void RDO_CIRCLE_DETEC_NEGATIVE_Click(object sender, EventArgs e){_CHANGE_AUTO_CIRCLE_DETECTION_TYPE(2);}

        private void BTN_PARAM_WRITE_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Selected Figure Parameters Will Be Overwritten.\nDo You Want To Update?", "UPDATE CHECK", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            const int TARGET_CIR = 0;
            const int TARGET_RECT = 1;

            //*************************************************************************************
            // CIRCLE 
            //*************************************************************************************

            if (TAB_TUNNING_TARGET.SelectedIndex == TARGET_CIR)
            {
                // empty operation exception 171017
                if (fm.COUNT_PAIR_CIR == 0) return;

                CMeasurePairCir[] arrCircle = fm.ToArray_PairCir();

                string strTarget = TXT_CIRCLE_SELECTED_FIGURE.Text;

                int element = 0;
                Int32.TryParse(TXT_CIRCLE_SELECTED_INDEX.Text, out element);

                CMeasurePairCir single = _FromUI_GetParameters_Circle();

                if (arrCircle[element].NICKNAME == strTarget)
                {
                    arrCircle[element] = single.CopyTo();
                }

                this.fm.list_pair_Cir = arrCircle.ToList();
            }
            //*************************************************************************************
            // RECTANGLE
            //*************************************************************************************
            else if (TAB_TUNNING_TARGET.SelectedIndex == TARGET_RECT)
            {
                // empty operation exception 171017
                if (fm.COUNT_PAIR_RCT == 0) return;

                CMeasurePairRct[] arrRect = fm.ToArray_PairRct();

                string strTarget = TXT_RECT_SELECTED_FIGURE.Text;

                int element = 0;
                Int32.TryParse(TXT_RECT_SELECTED_INDEX.Text, out element);

                CMeasurePairRct single = _FromUI_GetParameters_Rect();

                if (arrRect[element].NICKNAME == strTarget)
                {
                    arrRect[element] = single.CopyTo();
                }
                this.fm.list_pair_Rct = arrRect.ToList();
            }

            MessageBox.Show("Update Finished", "JOB FINISHED", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        private void BTN_PARAM_WRITE_ALL_Click(object sender, EventArgs e)
        {
            const int TYPE_CIR = 0;
            const int TYPE_REC = 1;

            if (MessageBox.Show("Every Data Will Be Syncronized According To User Selection.\n Do You Want To Proceed?", "OPERATION CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (TAB_TUNNING_TARGET.SelectedIndex == TYPE_CIR)
            {
                CMeasurePairCir single = _FromUI_GetParameters_Circle();

                CMeasurePairCir[] arrCircle = fm.ToArray_PairCir();

                for (int i = 0; i < arrCircle.Length; i++)
                {
                    if (CHK_SAVE_CIR_ALGORITHM.Checked/**********/) { arrCircle[i].param_00_algorithm_CIR /*************/= single.param_00_algorithm_CIR; }
                    if (CHK_SAVE_CIR_DAMAGE_TOLERANCE.Checked/***/) { arrCircle[i].param_01_DMG_Tol /*******************/= single.param_01_DMG_Tol; }
                    if (CHK_SAVE_CIR_TREAT_AS_ELLIPSE.Checked/***/) { arrCircle[i].param_02_BOOL_TREAT_AS_ELLIPSE /*****/= single.param_02_BOOL_TREAT_AS_ELLIPSE; }
                    if (CHK_SAVE_CIR_AUTO_CIRCLE_DETECTION.Checked) { arrCircle[i].param_03_CircleDetecType /***********/= single.param_03_CircleDetecType; }
                    if (CHK_SAVE_CIR_SHRINKAGE.Checked/**********/) { arrCircle[i].param_04_Shrinkage /*****************/= single.param_04_Shrinkage; }
                    if (CHK_SAVE_CIR_OUTLIER_FILTER.Checked/*****/) { arrCircle[i].param_05_Outlier_Filter /************/= single.param_05_Outlier_Filter; }
                    if (CHK_SAVE_CIR_EDGE_POSITION.Checked/******/) { arrCircle[i].param_06_EdgePos /*******************/= single.param_06_EdgePos; }
                    if (CHK_SAVE_CIR_COMPENSATION.Checked/*******/) { arrCircle[i].param_comm_01_compen_A /*************/= single.param_comm_01_compen_A; }
                    if (CHK_SAVE_CIR_COMPENSATION.Checked/*******/) { arrCircle[i].param_comm_02_compen_B /*************/= single.param_comm_02_compen_B; }
                    if (CHK_SAVE_CIR_SPC_ENHANCEMENT.Checked/****/) { arrCircle[i].param_comm_03_spc_enhance/***********/= single.param_comm_03_spc_enhance; }
                    //arrCircle[i].param_comm_04_refinement = single.param_comm_04_refinement;
                    if (CHK_SAVE_CIR_SHOW_RAW_DATA.Checked/******/) { arrCircle[i].param_comm_04_show_raw_data /********/= single.param_comm_04_show_raw_data; }
                    
                }

                this.fm.list_pair_Cir = arrCircle.ToList();

                CHK_SAVE_CIR_ALGORITHM.Checked = CHK_SAVE_CIR_AUTO_CIRCLE_DETECTION.Checked = CHK_SAVE_CIR_COMPENSATION.Checked = CHK_SAVE_CIR_DAMAGE_TOLERANCE.Checked = CHK_SAVE_CIR_EDGE_POSITION.Checked =
                CHK_SAVE_CIR_OUTLIER_FILTER.Checked = CHK_SAVE_CIR_SHOW_RAW_DATA.Checked = CHK_SAVE_CIR_SHRINKAGE.Checked = CHK_SAVE_CIR_TREAT_AS_ELLIPSE.Checked = false;

            }
            else if (TAB_TUNNING_TARGET.SelectedIndex == TYPE_REC)
            {
                CMeasurePairRct single = _FromUI_GetParameters_Rect();

                CMeasurePairRct[] arrRect = fm.ToArray_PairRct();

                for (int i = 0; i < arrRect.Length; i++)
                {
                    if (CHK_SAVE_RECT_ALGORITHM.Checked/**********/) { arrRect[i].param_00_algorithm /*****************/= single.param_00_algorithm; }
                    if (CHK_SAVE_RECT0__EDGE_DETECTION.Checked/***/) { arrRect[i].param_03_bool_Use_AutoDetection /****/= single.param_03_bool_Use_AutoDetection; }
                    if (CHK_SAVE_RECT0__EDGE_DETECTION.Checked/***/) { arrRect[i].param_04_peakTargetIndex_fst /*******/= single.param_04_peakTargetIndex_fst; }
                    if (CHK_SAVE_RECT0__EDGE_DETECTION.Checked/***/) { arrRect[i].param_05_peakTargetIndex_scd /*******/= single.param_05_peakTargetIndex_scd; }
                    if (CHK_SAVE_RECT0__EDGE_DETECTION.Checked/***/) { arrRect[i].param_06_peakCandidate /*************/= single.param_06_peakCandidate; }
                    if (CHK_SAVE_RECT0__EDGE_DETECTION.Checked/***/) { arrRect[i].param_07_windowSize /****************/= single.param_07_windowSize; }
                    if (CHK_SAVE_RECT0__EDGE_DETECTION.Checked/***/) { arrRect[i].param_08_edge_position_fst /*********/= single.param_08_edge_position_fst; }
                    if (CHK_SAVE_RECT_EDGE_POSITION.Checked/******/) { arrRect[i].param_09_edge_position_scd /*********/= single.param_09_edge_position_scd; }
                    if (CHK_SAVE_RECT_COMPENSATION.Checked/*******/) { arrRect[i].param_comm_01_compen_A /*************/= single.param_comm_01_compen_A; }
                    if (CHK_SAVE_RECT_COMPENSATION.Checked/*******/) { arrRect[i].param_comm_02_compen_B /*************/= single.param_comm_02_compen_B; }
                    if (CHK_SAVE_RECT_SPC_ENHANCEMENT.Checked/****/) { arrRect[i].param_comm_03_spc_enhance/***********/= single.param_comm_03_spc_enhance; }
                    if (CHK_SAVE_RECT_REFINEMENT.Checked/*********/) { arrRect[i].param_10_refinement/*****************/= single.param_10_refinement; }
                    if (CHK_SAVE_RECT_SHOW_RAW_DATA.Checked/******/) { arrRect[i].param_comm_04_show_raw_data /********/= single.param_comm_04_show_raw_data; }
                }

                this.fm.list_pair_Rct = arrRect.ToList();

                CHK_SAVE_RECT_ALGORITHM.Checked = CHK_SAVE_RECT_COMPENSATION.Checked = CHK_SAVE_RECT_EDGE_POSITION.Checked = CHK_SAVE_RECT_SHOW_RAW_DATA.Checked = CHK_SAVE_RECT0__EDGE_DETECTION.Checked = false;
            }


        }

        private void TXT_CIR_SHRINKAGE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int nType = _FromUI_GetCircleDetectionType();
                _CHANGE_AUTO_CIRCLE_DETECTION_TYPE(nType);
            }
        }

        private void uc_thumb_nail_rect_DoubleClick(object sender, EventArgs e)
        {
            int nIndex = uc_thumb_nail_rect.FocusedItem.Index;
            Bitmap bmp = uc_thumb_nail_rect.GetImageOriginal(nIndex);

            CMeasurePairRct single = fm.ElementAt_PairRct(nIndex);

            
            // 01 Target Info
            TXT_RECT_SELECTED_INDEX.Text = nIndex.ToString("N0");
            TXT_RECT_SELECTED_FIGURE.Text = single.NICKNAME;

            //00 Algorithm 
            if (single.param_00_algorithm == IFX_ALGORITHM.MEXHAT) { RDO_RECT_ALGO_MEXHAT.Checked = true; }
            if (single.param_00_algorithm == IFX_ALGORITHM.CARDIN) { RDO_RECT_ALGO_CARDIN.Checked = true; }
            if (single.param_00_algorithm == IFX_ALGORITHM.DIR_EX) { RDO_RECT_ALGO_DIR_EX.Checked = true; }
            if (single.param_00_algorithm == IFX_ALGORITHM.DIR_IN) { RDO_RECT_ALGO_DIR_IN.Checked = true; }

            // 01 Rectangle Type { HOR | VER | DIA }
            /***/if (single.param_01_rc_type == IFX_RECT_TYPE.DIR_HOR) RDO_RECT_TYPE_HOR.Checked = true;
            else if (single.param_01_rc_type == IFX_RECT_TYPE.DIR_VER) RDO_RECT_TYPE_VER.Checked = true;
            else if (single.param_01_rc_type == IFX_RECT_TYPE.DIR_DIA) RDO_RECT_TYPE_DIA.Checked = true;

            CHK_RECT_USE_AUTO_PEAK_DETECTION.Checked = single.param_03_bool_Use_AutoDetection;
            CHK_RECT_USE_AUTO_PEAK_DETECTION_CheckedChanged(null, EventArgs.Empty);

            // 02 edge detection type { 0 ~ 6 }
            TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Text = single.param_04_peakTargetIndex_fst.ToString("N0");
            TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Text = single.param_05_peakTargetIndex_scd.ToString("N0");

            CB_RECT_TARGET_INDEX_FST.SelectedIndex = single.param_04_peakTargetIndex_fst;
            CB_RECT_TARGET_INDEX_SCD.SelectedIndex = single.param_05_peakTargetIndex_scd;
            // update UI from the first line data 

            TXT_RECT_WINDOW_SIZE.Text = single.param_07_windowSize.ToString("NO");

            // 03 compensation value            
           TXT_RECT_COMPEN_A.Text= single.param_comm_01_compen_A.ToString("F2");
           TXT_RECT_COMPEN_B.Text = single.param_comm_02_compen_B.ToString("F2");

            // 04 edge position for the line 
           TXT_RECT_EDGE_POSITION_FST.Text = single.param_08_edge_position_fst.ToString("F2");
           TXT_RECT_EDGE_POSITION_SCD.Text = single.param_09_edge_position_scd.ToString("F2");

           TXT_RECT_EDGE_REFINEMENT.Text = single.param_10_refinement.ToString("N0");

            // 05 show raw data 
           CHK_RECT_SHOW_RAW_DATA.Checked = single.param_comm_04_show_raw_data;
            
            uc_tunning_view.VIEW_Set_Clear_DispObject();
            uc_tunning_view.SetDisplay(bmp);
            uc_tunning_view.VIEW_SET_Mag_Origin();
        }

        private void BTN_CHECK_EDGE_DETECTION_Click(object sender, EventArgs e)
        {
            if (RDO_RECT_TYPE_DIA.Checked == true)
            {
                //MessageBox.Show("Under Construction.", "Comming Soon.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                bool bool_horizontal_dir = RDO_RECT_TYPE_HOR.Checked == true ? true : false;

                uc_tunning_view.VIEW_Set_Clear_DispObject();

                CMeasurePairRct rcProc = new CMeasurePairRct();

                
                int param_00_FilterSize = Convert.ToInt32(TXT_RECT_WINDOW_SIZE.Value);
                int param_01_PeakCandidate = Convert.ToInt32(TXT_RECT_CANDIDATE_COUNT.Value);
  
                Bitmap bmp = uc_tunning_view.GetDisplay_Bmp();
                pm.SetImage(bmp);

                List<PointF> listPeak = pm.GetPeakListSorted(param_01_PeakCandidate, bool_horizontal_dir);

                _ToUI_DrawPeakAnalysis(listPeak, bool_horizontal_dir);

                uc_tunning_view.Refresh();
            }
        }
        private void BTN_RECT_VERIFY_EDGE_REGION_FST_Click(object sender, EventArgs e)
        {
            uc_tunning_view.VIEW_Set_Clear_DispObject();

            int param_00_WindowSize = Convert.ToInt32(TXT_RECT_WINDOW_SIZE.Value);
            int param_01_PeakCandidate = Convert.ToInt32(TXT_RECT_CANDIDATE_COUNT.Value);
            int param_02_target_peak_index = Convert.ToInt32(CB_RECT_TARGET_INDEX_FST.Text);            

            RDO_RECT_APD_FST.Checked = true;

            Bitmap bmp = uc_tunning_view.GetDisplay_Bmp();
            pm.SetImage(bmp);

            bool bool_horizontal_dir = RDO_RECT_TYPE_HOR.Checked == true ? true : false;

            // draw entire set
            List<PointF> listPeak = pm.GetPeakListSorted(param_01_PeakCandidate, bool_horizontal_dir);
            _ToUI_DrawPeakAnalysis(listPeak, bool_horizontal_dir);

            // get the selected peak and region
            PointF ptCurrent = pm.GetPeakDesinated(param_01_PeakCandidate, bool_horizontal_dir, param_02_target_peak_index);

            int nParse = Convert.ToInt32(param_00_WindowSize / 2.0);

            if( bool_horizontal_dir == true)
            {
                CLine line = new CLine(new PointF(0, ptCurrent.Y), new PointF(bmp.Width, ptCurrent.Y));

                CLine lineM = line.ShiftLine(0, -nParse);
                CLine lineP = line.ShiftLine(0, +nParse);

                uc_tunning_view.DrawLine(line, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineM, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineP, 1, Color.LimeGreen);
            }
            else if (bool_horizontal_dir == false)
            {
                CLine line = new CLine(new PointF(ptCurrent.X, 0), new PointF(ptCurrent.X, bmp.Height));

                CLine lineM = line.ShiftLine(-nParse, 0);
                CLine lineP = line.ShiftLine(+nParse, 0);

                uc_tunning_view.DrawLine(line, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineM, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineP, 1, Color.LimeGreen);
            }

             uc_tunning_view.Refresh();
        }
        private void BTN_RECT_VERIFY_EDGE_REGION_SCD_Click(object sender, EventArgs e)
        {
            int param_00_WindowSize = Convert.ToInt32(TXT_RECT_WINDOW_SIZE.Value);
            int param_01_PeakCandidate = Convert.ToInt32(TXT_RECT_CANDIDATE_COUNT.Value);
            int param_02_target_peak_index = Convert.ToInt32(CB_RECT_TARGET_INDEX_SCD.Text);

            RDO_RECT_APD_SCD.Checked = true;
 
            uc_tunning_view.VIEW_Set_Clear_DispObject();

            Bitmap bmp = uc_tunning_view.GetDisplay_Bmp();
            pm.SetImage(bmp);

            bool bool_horizontal_dir = RDO_RECT_TYPE_HOR.Checked == true ? true : false;

            // draw entire set
            List<PointF> listPeak = pm.GetPeakListSorted(param_01_PeakCandidate, bool_horizontal_dir);
            _ToUI_DrawPeakAnalysis(listPeak, bool_horizontal_dir);

            // get the selected peak and region
            PointF ptCurrent = pm.GetPeakDesinated(param_01_PeakCandidate, bool_horizontal_dir, param_02_target_peak_index);

            int nParse = Convert.ToInt32(param_00_WindowSize / 2.0);

            if (bool_horizontal_dir == true)
            {
                CLine line = new CLine(new PointF(0, ptCurrent.Y), new PointF(bmp.Width, ptCurrent.Y));

                CLine lineM = line.ShiftLine(0, -nParse);
                CLine lineP = line.ShiftLine(0, +nParse);

                uc_tunning_view.DrawLine(line, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineM, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineP, 1, Color.LimeGreen);
            }
            else if (bool_horizontal_dir == false)
            {
                CLine line = new CLine(new PointF(ptCurrent.X, 0), new PointF(ptCurrent.X, bmp.Height));

                CLine lineM = line.ShiftLine(-nParse, 0);
                CLine lineP = line.ShiftLine(+nParse, 0);

                uc_tunning_view.DrawLine(line, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineM, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineP, 1, Color.LimeGreen);
            }
            uc_tunning_view.Refresh();

        }
        
     

        private void _ToUI_DrawPeakAnalysis(CPeakPair peakData)
        {
            for (int i = 0; i < peakData.COUNT; i++)
            {
                CPeakPair.PeakPair single = peakData.listPeaks.ElementAt(i);

                uc_tunning_view.DrawLine(single.l1, (float)0.5, Color.Magenta);
                uc_tunning_view.DrawLine(single.l2, (float)0.5, Color.Cyan);
                uc_tunning_view.DrawCircle(single.ptJoint, (float)1.5, (float)1.5, Color.LimeGreen, (float)0.5);
                uc_tunning_view.DrawString(i.ToString("N0"), (int)single.ptJoint.X - 2, (int)single.ptJoint.Y - 4, 5, Color.Red);
            }
        }

        private void _ToUI_DrawPeakAnalysis(List<PointF> list, bool bHorizontal)
        {
            if (bHorizontal == true)
            {
                int nAxisPos = 10;
                for (int i = 0; i < list.Count(); i++)
                {
                    PointF ptCurrent = list.ElementAt(i);

                    uc_tunning_view.DrawPoint(nAxisPos, ptCurrent.Y, 1, 1, Color.Red);
                    uc_tunning_view.DrawString(i.ToString("N0"), nAxisPos, (int)ptCurrent.Y - 4, 5, Color.Red);
                }
            }
            else
            {
                int nAxisPos = 10;
                for (int i = 0; i < list.Count(); i++)
                {
                    PointF ptCurrent = list.ElementAt(i);

                    uc_tunning_view.DrawPoint(ptCurrent.X, nAxisPos, 1, 1, Color.Red);
                    uc_tunning_view.DrawString(i.ToString("N0"), (int)ptCurrent.X-4, nAxisPos, 5, Color.Red);
                }
            }
            
        }

        

        

        private void CHK_RECT_DETECT_FST_CheckedChanged(object sender, EventArgs e)
        {

            if (RDO_RECT_APD_FST.Checked == true)
            {
                if (uc_thumb_nail_rect.FocusedItem == null) return;

                int nIndex = uc_thumb_nail_rect.FocusedItem.Index;
                Bitmap bmp = uc_thumb_nail_rect.GetImageOriginal(nIndex);

                CMeasurePairRct single = fm.ElementAt_PairRct(nIndex);
                TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Text = single.param_04_peakTargetIndex_fst.ToString("N0");
                CB_RECT_TARGET_INDEX_FST.SelectedIndex = single.param_04_peakTargetIndex_fst;
            }
        }

        private void CHK_RECT_DETECT_SCD_CheckedChanged(object sender, EventArgs e)
        {
            if (RDO_RECT_APD_SCD.Checked == true)
            {
                if (uc_thumb_nail_rect.FocusedItem == null) return;

                int nIndex = uc_thumb_nail_rect.FocusedItem.Index;
                Bitmap bmp = uc_thumb_nail_rect.GetImageOriginal(nIndex);

                CMeasurePairRct single = fm.ElementAt_PairRct(nIndex);

                TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Text = single.param_05_peakTargetIndex_scd.ToString("N0");
                CB_RECT_TARGET_INDEX_SCD.SelectedIndex = single.param_05_peakTargetIndex_scd;
            }
        }


        private void CHK_RECT_USE_AUTO_PEAK_DETECTION_CheckedChanged(object sender, EventArgs e)
        {
            RDO_RECT_APD_FST.Enabled = RDO_RECT_APD_SCD.Enabled = CHK_RECT_USE_AUTO_PEAK_DETECTION.Checked;
        }

        private void BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST_Click(object sender, EventArgs e)
        {
            TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Text = CB_RECT_TARGET_INDEX_FST.Text;
        }

        private void BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD_Click(object sender, EventArgs e)
        {
            TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Text = CB_RECT_TARGET_INDEX_SCD.Text;
        }

        

        private void BTN_CIRCLE_CHECK_AUTO_DETECTION_Click(object sender, EventArgs e)
        {
            int nType = _FromUI_GetCircleDetectionType();
            _CHANGE_AUTO_CIRCLE_DETECTION_TYPE(nType);
        }

        private void CHK_SHOW_SECTOR_DRAWING_CheckedChanged(object sender, EventArgs e)
        {
            uc_tunning_view.VIEW_Set_Clear_DispObject();

            if (CHK_SHOW_SECTOR.Checked == true)
            {
                int imageW = uc_tunning_view.VIEW_GetImageW();
                int imageH = uc_tunning_view.VIEW_GetImageH();

                PointF ptCenter = new PointF(imageW / 2, imageH / 2);

                uc_tunning_view.DrawCircle(ptCenter, (float)3, (float)3, Color.LimeGreen, 1);

                double[] arrayCos = Computer.GetArray_COS();
                double[] arraySin = Computer.GetArray_SIN();

                double radius = Math.Sqrt((imageW * imageW) + (imageH * imageH)) / 2.0;

                for (int nAngle = 0; nAngle < 360; nAngle += 30)
                {
                    PointF ptEdge = new PointF();

                    for (int nPos = 0; nPos < (int)radius; nPos++)
                    {
                        double x = ptCenter.X + (nPos * arrayCos[nAngle]);
                        double y = ptCenter.Y + (nPos * arraySin[nAngle]);

                        if (x < 0 || y < 0 || x >= imageW || y >= imageH) { continue; }

                        ptEdge = new PointF((float)x, (float)y);
                    }

                    uc_tunning_view.DrawLine(ptCenter, ptEdge, 1, Color.LimeGreen);
                    uc_tunning_view.DrawCircle(ptEdge, (float)3, (float)3, Color.LimeGreen, 1);
                }//);

                int nSector = 1;
                for (int nAngle = 15; nAngle < 360; nAngle += 30)
                {
                    double x = ptCenter.X + ((radius*0.5) * arrayCos[nAngle]);
                    double y = ptCenter.Y + ((radius*0.5) * arraySin[nAngle]);

                    PointF ptMid = new PointF((float)x, (float)y);

                    uc_tunning_view.DrawString(string.Format("{0}", nSector++), (int)ptMid.X, (int)ptMid.Y, 5, Color.Yellow);

                }

                uc_tunning_view.Refresh();
            }

        }

         

      

       
        
       

       
    }
}
