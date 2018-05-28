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
            uc_tunning_view.eventDele_HereComesNewImage += new dele_HereComesNewImage(updateView);


            uc_thumb_nail_rect.ThumbNailSize = 128;
            uc_thumb_nail_rect.SetInit();

            uc_tunning_view.BOOL_DRAW_FOCUS_ROI = false;
            uc_tunning_view.BOOL_DRAW_PTRN_ROI = false;
        }
        public void updateView() { }

        public bool SetParam(CFigureManager fm, Bitmap bmp)
        {
            MAIN_IMAGE = bmp.Clone() as Bitmap;

            uc_tunning_view.BOOL_DRAW_CROSS = false;    
            uc_tunning_view.SetDisplay(MAIN_IMAGE);
            uc_tunning_view.VIEW_SET_Mag_Origin();

            int imageW = 0;
            int imageH = 0;
            byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp.Clone() as Bitmap, ref imageW, ref imageH);

            List<Bitmap> listImageRect = new List<Bitmap>();
            List<string> listFilesRect = new List<string>();

            this.fm = fm;

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
        
        private CMeasurePairRct _FromUI_GetParameters_Rect()
        {
            int nValue = 0;

            CMeasurePairRct single = new CMeasurePairRct();

            single.param_03_bool_Use_AutoDetection = CHK_RECT_USE_AUTO_PEAK_DETECTION.Checked;

            // edge detection target for each rectangles
            int.TryParse(TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Text, out nValue); single.param_04_peakTargetIndex_fst = nValue;
            int.TryParse(TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Text, out nValue); single.param_05_peakTargetIndex_scd = nValue;

            // candidate count 
            int.TryParse(TXT_RECT_CANDIDATE_COUNT.Text, out nValue); single.param_06_peakCandidate = nValue;

            return single;
        }
        

        private void BTN_PARAM_WRITE_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Selected Figure Parameters Will Be Overwritten.\nDo You Want To Update?", "UPDATE CHECK", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
             
            // empty operation exception 171017
            if (fm.COUNT_PAIR_RCT == 0) return;

            CMeasurePairRct[] arrRect = fm.ToArray_PairRct();

            string strTarget = TXT_RECT_SELECTED_FIGURE.Text;

            int element = 0;
            Int32.TryParse(TXT_RECT_SELECTED_INDEX.Text, out element);

            CMeasurePairRct single = _FromUI_GetParameters_Rect();

            if (arrRect[element].NICKNAME == strTarget)
            {
                arrRect[element].param_03_bool_Use_AutoDetection/***/= single.param_03_bool_Use_AutoDetection;
                arrRect[element].param_04_peakTargetIndex_fst/******/= single.param_04_peakTargetIndex_fst;
                arrRect[element].param_05_peakTargetIndex_scd/******/= single.param_05_peakTargetIndex_scd;
                arrRect[element].param_06_peakCandidate/************/= single.param_06_peakCandidate;
                arrRect[element].param_07_windowSize/***************/= single.param_07_windowSize;
            }
            this.fm.list_pair_Rct = arrRect.ToList();
             
            MessageBox.Show("Update Finished", "JOB FINISHED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            eventDele_FinishTunning();
        }


        private void uc_thumb_nail_rect_DoubleClick(object sender, EventArgs e)
        {
            int nIndex = uc_thumb_nail_rect.FocusedItem.Index;
            Bitmap bmp = uc_thumb_nail_rect.GetImageOriginal(nIndex);

            CMeasurePairRct single = fm.ElementAt_PairRct(nIndex);

            /***/if( single.param_01_rc_type == IFX_RECT_TYPE.DIR_HOR){RDO_RECT_TYPE_HOR.Checked = true;}
            else if( single.param_01_rc_type == IFX_RECT_TYPE.DIR_VER){RDO_RECT_TYPE_VER.Checked = true;}
            else if( single.param_01_rc_type == IFX_RECT_TYPE.DIR_DIA){RDO_RECT_TYPE_DIA.Checked = true;}

            // 01 Target Info
            TXT_RECT_SELECTED_INDEX.Text = nIndex.ToString("N0");
            TXT_RECT_SELECTED_FIGURE.Text = single.NICKNAME;

            CHK_RECT_USE_AUTO_PEAK_DETECTION.Checked = single.param_03_bool_Use_AutoDetection;
            CHK_RECT_USE_AUTO_PEAK_DETECTION_CheckedChanged(null, EventArgs.Empty);

            // 02 edge detection type { 0 ~ 6 }
            TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Text = single.param_04_peakTargetIndex_fst.ToString("N0");
            TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Text = single.param_05_peakTargetIndex_scd.ToString("N0");

            CB_RECT_TARGET_INDEX_FST.SelectedIndex = single.param_04_peakTargetIndex_fst;
            CB_RECT_TARGET_INDEX_SCD.SelectedIndex = single.param_05_peakTargetIndex_scd;
            // update UI from the first line data 

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

            int nParse = 5;

            if( bool_horizontal_dir == true)
            {
                CLine line = new CLine(new PointF(0, ptCurrent.Y), new PointF(bmp.Width, ptCurrent.Y));

                CLine lineM = line.ShiftLine(0, -nParse);
                CLine lineP = line.ShiftLine(0, +nParse);

                uc_tunning_view.DrawLine(lineM, 1, Color.DeepSkyBlue);
                uc_tunning_view.DrawLine(line, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineP, 1, Color.DeepSkyBlue);
            }
            else if (bool_horizontal_dir == false)
            {
                CLine line = new CLine(new PointF(ptCurrent.X, 0), new PointF(ptCurrent.X, bmp.Height));

                CLine lineM = line.ShiftLine(-nParse, 0);
                CLine lineP = line.ShiftLine(+nParse, 0);

                uc_tunning_view.DrawLine(lineM, 1, Color.DeepSkyBlue);
                uc_tunning_view.DrawLine(line, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineP, 1, Color.DeepSkyBlue);
            }

             uc_tunning_view.Refresh();
        }
        private void BTN_RECT_VERIFY_EDGE_REGION_SCD_Click(object sender, EventArgs e)
        {
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

            int nParse = 5;

            if (bool_horizontal_dir == true)
            {
                CLine line = new CLine(new PointF(0, ptCurrent.Y), new PointF(bmp.Width, ptCurrent.Y));

                CLine lineM = line.ShiftLine(0, -nParse);
                CLine lineP = line.ShiftLine(0, +nParse);

                uc_tunning_view.DrawLine(lineM, 1, Color.DeepSkyBlue);
                uc_tunning_view.DrawLine(line, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineP, 1, Color.DeepSkyBlue);
            }
            else if (bool_horizontal_dir == false)
            {
                CLine line = new CLine(new PointF(ptCurrent.X, 0), new PointF(ptCurrent.X, bmp.Height));

                CLine lineM = line.ShiftLine(-nParse, 0);
                CLine lineP = line.ShiftLine(+nParse, 0);

                uc_tunning_view.DrawLine(line, 1, Color.DeepSkyBlue);
                uc_tunning_view.DrawLine(lineM, 1, Color.LimeGreen);
                uc_tunning_view.DrawLine(lineP, 1, Color.DeepSkyBlue);
            }
            uc_tunning_view.Refresh();
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
         
    }
}
