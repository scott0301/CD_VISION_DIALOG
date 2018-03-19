using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CD_View;
using CD_Figure;
using CD_Measure;

using CodeKing.Native;

namespace CD_VISION_DIALOG
{
    public partial class DLg_Processing : Form
    {
        private CFigureManager fm = new CFigureManager();

        public int m_imageW { get { return m_bmp_Back.Width; } }
        public int m_imageH { get { return m_bmp_Back.Height; } }
        public Bitmap m_bmp_Back = new Bitmap(600,600);

        public bool m_bStatus_Record = false;

        public List<String> listCommand = new List<String>();

        public subclass classUI = null;
 
        public DLg_Processing()
        {
            InitializeComponent();

            uc_view_processing.eventDele_HereComesNewImage += new dele_HereComesNewImage(updateView);
            uc_view_processing.SetInit(500, 400, true);

            classUI = new subclass(this as DLg_Processing);

            uc_view_processing.BOOL_DRAW_FOCUS_ROI = false;
            uc_view_processing.BOOL_DRAW_PTRN_ROI = false; 

        }

        public void updateView() { }

        private void DLg_Processing_Load(object sender, EventArgs e)
        {
            m_bStatus_Record = false;
            _SetRecordStatus(m_bStatus_Record);
        }

        public bool SetParam(CFigureManager fm, Bitmap bmp)
        {
            // connect main figuremanager
            this.fm = fm;

            // set the base image 
            PIC_INPUT.Image = bmp.Clone() as Bitmap;

            // show~ up to the view 
            uc_view_processing.SetDisplay(bmp.Clone() as Bitmap);
            uc_view_processing.VIEW_SET_Mag_Origin();

            // get the view image and generate temperature image
            int imageW = uc_view_processing.VIEW_GetImageW();
            int imageH = uc_view_processing.VIEW_GetImageH();
            byte[] rawImage = classUI.GetMainRawImage();

            // set temperature image
            PIC_TEMPERATURE.Image = classUI._GetColorTemperature(rawImage, imageW, imageH);

            // pre defined pre-processing elements updates 170809
            listCommand = fm.listCommand.ToList();
            classUI.LV_DisplayProcedure();

            return true;
        }

        private void BTN_PTRN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BTN_RECOVER_Click(object sender, EventArgs e)
        {
            Bitmap bmp = PIC_INPUT.Image.Clone() as Bitmap;
            uc_view_processing.SetDisplay(bmp);
            uc_view_processing.Refresh();
        }

        private void BTN_TO_SHOW_COLOR_Click(object sender, EventArgs e)
        {
            Bitmap bmp = PIC_TEMPERATURE.Image.Clone() as Bitmap;
            uc_view_processing.SetDisplay(bmp);
            uc_view_processing.Refresh();
        }

        private void BTN_CALCULATE_IMAGE_QUALITY_Click(object sender, EventArgs e)
        {
            byte[] rawImage = classUI.GetMainRawImage();
            int imageW = uc_view_processing.VIEW_GetImageW();
            int imageH = uc_view_processing.VIEW_GetImageH();

            double fMin = 0; double fMax = 0; double fDif = 0; double fAvg = 0;

            fAvg = Computer.GetStatistics(rawImage, imageW, imageH, ref fMin, ref fMax, ref fDif);

            double fNoise = Computer.GetSelfNoiseRatio(rawImage, imageW, imageH);

            TXT_IMAGE_QUALITY_MIN_GV.Text = fMin.ToString("N0");
            TXT_IMAGE_QUALITY_MAX_GV.Text = fMax.ToString("N0");
            TXT_IMAGE_QUALITY_DIFF_GV.Text = fDif.ToString("N0");
            TXT_IMAGE_QUALITY_AVG_GV.Text = fAvg.ToString("N0");
            TXT_IMAGE_QUALITY_NOISE_LEVEL.Text = fNoise.ToString("F4");

            PIC_TEMPERATURE.Image = classUI._GetColorTemperature(rawImage, imageW, imageH);

            classUI.DrawHistogram();
        }

      
        private void TB_BRIGHTNESS_CHANGE_Scroll(object sender, EventArgs e)
        {
            TXT_CTRL_BRIGHTNESS.Text = TB_CHANGING_BRIGHTNESS.Value.ToString("N0");

            int nValue = classUI.GetParam_Brightness();

            byte[] rawImage = Computer.HC_TRANS_Brightness(classUI.GetBackBuffer(), m_imageW, m_imageH, nValue);

            uc_view_processing.SetDisplay(rawImage, m_imageW, m_imageH);
            uc_view_processing.Refresh();
        }

        private void TB_CONSTRAST_CHANGE_Scroll(object sender, EventArgs e)
        {
            TXT_CTRL_CONTRAST.Text = TB_CHANGING_CONTRAST.Value.ToString("N0");

            int nValue = classUI.GetParam_Contrast();

            byte [] rawImage = Computer.HC_TRANS_Contrast(classUI.GetBackBuffer(), m_imageW, m_imageH, nValue);

            uc_view_processing.SetDisplay(rawImage, m_imageW, m_imageH);
            uc_view_processing.Refresh();

        }
        private void TB_CHANGING_SMOOTHING_Scroll(object sender, EventArgs e)
        {
            TXT_CTRL_SMOOTHING.Text = TB_CHANGING_SMOOTHING.Value.ToString("N0");

            int nValue = classUI.GetParam_Smoothing();

            byte[] rawImage = Computer.HC_FILTER_StepSmoothing(classUI.GetBackBuffer(), m_imageW, m_imageH, nValue);

            uc_view_processing.SetDisplay(rawImage, m_imageW, m_imageH);
            uc_view_processing.Refresh();
        }
        private void BTN_CHANGING_SHARPENING_Click(object sender, EventArgs e)
        {
            byte [] rawImage = Computer.HC_FILTER_Sharpening(classUI.GetBackBuffer(), m_imageW, m_imageH);

            uc_view_processing.SetDisplay(rawImage, m_imageW, m_imageH);
            uc_view_processing.Refresh();
        }
        private void BTN_CHANGING_MAGNITUDE_Click(object sender, EventArgs e)
        {
            byte[] rawImage = Computer.HC_TRANS_GradientImage(classUI.GetBackBuffer(), m_imageW, m_imageH);
            uc_view_processing.SetDisplay(rawImage, m_imageW, m_imageH);
            uc_view_processing.Refresh();

        }
        private void BTN_CHANGING_NOISE_REMOVAL_Click(object sender, EventArgs e)
        {
            byte[] rawImage = Computer.HC_FILTER_Median(classUI.GetBackBuffer(), m_imageW, m_imageH, 3);

            uc_view_processing.SetDisplay(rawImage, m_imageW, m_imageH);
            uc_view_processing.Refresh();
        }
        private void CHK_CONTROL_BRIGHTNESS_CheckedChanged(object sender, EventArgs e)
        {
            TXT_CTRL_BRIGHTNESS.Enabled = CHK_CONTROL_BRIGHTNESS.Checked;
            TB_CHANGING_BRIGHTNESS.Enabled = CHK_CONTROL_BRIGHTNESS.Checked;
            BTN_ADD_PROC_BRIGHTNESS.Enabled = CHK_CONTROL_BRIGHTNESS.Checked;

            if( CHK_CONTROL_BRIGHTNESS.Checked == false)
            {
                TXT_CTRL_BRIGHTNESS.Text = "0";
                TB_CHANGING_BRIGHTNESS.Value = 0;
            }
        }

        private void CHK_CONTROL_CONTRAST_CheckedChanged(object sender, EventArgs e)
        {
            BTN_ADD_PROC_CONTRAST.Enabled = CHK_CONTROL_CONTRAST.Checked;
            TXT_CTRL_CONTRAST.Enabled = CHK_CONTROL_CONTRAST.Checked;
            TB_CHANGING_CONTRAST.Enabled = CHK_CONTROL_CONTRAST.Checked;
            
            if( CHK_CONTROL_CONTRAST.Checked == false )
            {
                TXT_CTRL_CONTRAST.Text = "0";
                TB_CHANGING_CONTRAST.Value = 0;
            }
        }
        private void CHK_CONTROL_SMOOTHING_CheckedChanged(object sender, EventArgs e)
        {
            TXT_CTRL_SMOOTHING.Enabled = CHK_CONTROL_SMOOTHING.Checked;
            TB_CHANGING_SMOOTHING.Enabled = CHK_CONTROL_SMOOTHING.Checked;
            BTN_ADD_PROC_SMOOTHING.Enabled = CHK_CONTROL_SMOOTHING.Checked;

            if( CHK_CONTROL_SMOOTHING.Checked == false )
            {
                TB_CHANGING_SMOOTHING.Value = 0;
                TXT_CTRL_SMOOTHING.Text = "0";
            }
        }
        private void CHK_CONTROL_NOISE_REMOVAL_CheckedChanged(object sender, EventArgs e)
        {
            BTN_ADD_PROC_NOISE_REMOVAL.Enabled = CHK_CONTROL_NOISE_REMOVAL.Checked;
            BTN_CHANGING_NOISE_REMOVAL.Enabled = CHK_CONTROL_NOISE_REMOVAL.Checked;
        }

        private void CHK_CONTROL_SHARPENING_CheckedChanged(object sender, EventArgs e)
        {
            BTN_ADD_PROC_SHARPENING.Enabled = CHK_CONTROL_SHARPENING.Checked;
            BTN_CHANGING_SHARPENING.Enabled = CHK_CONTROL_SHARPENING.Checked;
        }
        private void CHK_CONTROL_MAGNITUDE_CheckedChanged(object sender, EventArgs e)
        {
            BTN_ADD_PROC_MAGNITUDE.Enabled = CHK_CONTROL_MAGNITUDE.Checked;
            BTN_CHANGING_MAGNITUDE.Enabled = CHK_CONTROL_MAGNITUDE.Checked;
        }

        private void BTN_SHOW_RESULT_IMAGE_Click(object sender, EventArgs e)
        {
            int imageW =  0; int imageH =  0;
            byte[] rawImage = classUI.GetInputImage(out imageW, out imageH);

            rawImage = CInspUnit.TriggerProcess(rawImage, imageW, imageH, listCommand);
            BTN_CALCULATE_IMAGE_QUALITY_Click(null, EventArgs.Empty);

            uc_view_processing.SetDisplay(rawImage, imageW, imageH);
            uc_view_processing.Refresh();

            m_bmp_Back = Computer.HC_CONV_Byte2Bmp(rawImage, imageW, imageH);

            MessageBox.Show("Result Displayed.", "Pre-Processing Simulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void BTN_ADD_PROC_BR_Click(object sender, EventArgs e)
        {
            m_bmp_Back = uc_view_processing.GetDisplay_Bmp();

            listCommand.Add(string.Format("BR,{0}", classUI.GetParam_Brightness()));
            classUI.LV_DisplayProcedure();

            TXT_CTRL_BRIGHTNESS.Text = "0";
            TB_CHANGING_BRIGHTNESS.Value = 0;
            CHK_CONTROL_BRIGHTNESS.Checked = false;
            BTN_SHOW_RESULT_IMAGE_Click(null, EventArgs.Empty);
        }

        private void BTN_ADD_PROC_CONTRAST_Click(object sender, EventArgs e)
        {
            m_bmp_Back = uc_view_processing.GetDisplay_Bmp();

            listCommand.Add(string.Format("CONT,{0}", classUI.GetParam_Contrast()));
            classUI.LV_DisplayProcedure();

            TXT_CTRL_CONTRAST.Text = "0";
            TB_CHANGING_CONTRAST.Value = 0;
            CHK_CONTROL_CONTRAST.Checked = false;
            BTN_SHOW_RESULT_IMAGE_Click(null, EventArgs.Empty);
        }
        private void BTN_ADD_PROC_SMOOTHING_Click(object sender, EventArgs e)
        {
            m_bmp_Back = uc_view_processing.GetDisplay_Bmp();

            listCommand.Add(string.Format("SMOOTH,{0}", classUI.GetParam_Smoothing()));
            classUI.LV_DisplayProcedure();

            TXT_CTRL_SMOOTHING.Text = "0";
            TB_CHANGING_SMOOTHING.Value = 0;
            CHK_CONTROL_SMOOTHING.Checked = false;
            BTN_SHOW_RESULT_IMAGE_Click(null, EventArgs.Empty);
        }
        private void BTN_ADD_PROC_SHARPENING_Click(object sender, EventArgs e)
        {
            m_bmp_Back = uc_view_processing.GetDisplay_Bmp();

            listCommand.Add(string.Format("SHARPEN,{0}", 1));
            classUI.LV_DisplayProcedure();
            CHK_CONTROL_SHARPENING.Checked = false;
            BTN_SHOW_RESULT_IMAGE_Click(null, EventArgs.Empty);
        }
        private void BTN_ADD_PROC_NOISE_REMOVAL_Click(object sender, EventArgs e)
        {
            m_bmp_Back = uc_view_processing.GetDisplay_Bmp();

            listCommand.Add(string.Format("NR,{0}", 1));
            classUI.LV_DisplayProcedure();
            CHK_CONTROL_NOISE_REMOVAL.Checked = false;
            BTN_SHOW_RESULT_IMAGE_Click(null, EventArgs.Empty);
        }
        private void BTN_ADD_PROC_MAGNITUDE_Click(object sender, EventArgs e)
        {
            m_bmp_Back = uc_view_processing.GetDisplay_Bmp();

            listCommand.Add(string.Format("MG,{0}", 1));
            classUI.LV_DisplayProcedure();
            CHK_CONTROL_MAGNITUDE.Checked = false;
            BTN_SHOW_RESULT_IMAGE_Click(null, EventArgs.Empty);
        }
        private void BTN_PROC_CLEAR_Click(object sender, EventArgs e)
        {
            listCommand.Clear();
            classUI.LV_DisplayProcedure();
            BTN_SHOW_RESULT_IMAGE_Click(null, EventArgs.Empty);
        }
        private void BTN_PROC_DELETE_IT_Click(object sender, EventArgs e)
        {
            if( LV_PROC_PROCESS.FocusedItem == null ) return;

            int nIndex = LV_PROC_PROCESS.FocusedItem.Index;

            string iIndex = LV_PROC_PROCESS.Items[nIndex].SubItems[0].Text;
            string iOperation = LV_PROC_PROCESS.Items[nIndex].SubItems[1].Text;
            string iValue = LV_PROC_PROCESS.Items[nIndex].SubItems[2].Text;

            string strTarget = string.Format("Do You Want To Remove Selected Target?\n IDX : {0}\nOPERATION : {1}\nValue : {2}", iIndex, iOperation, iValue);

            if (MessageBox.Show(strTarget, "CONFIRM", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                listCommand.RemoveAt(nIndex);
                classUI.LV_DisplayProcedure();
                BTN_SHOW_RESULT_IMAGE_Click(null, EventArgs.Empty);
            }
         }

        
        private void BTN_RECORD_Click(object sender, EventArgs e)
        {
            _SetRecordStatus(!m_bStatus_Record);
        }

        // Set Record Status (on / off ) 170809
        private void _SetRecordStatus(bool bStatus)
        {
            m_bStatus_Record = bStatus;

            /***/if (m_bStatus_Record == false) 
            {
                BTN_RECORD.BackgroundImage = Properties.Resources.record_off;
                GB_OPERATIONS1.Enabled = false;
                GB_OPERATIONS2.Enabled = false;
                LB_RECORD_STATUS.Text = ": RECOARDING OFF";
                _PRINT_MSG("Recording Finished.");
            }
            else if (m_bStatus_Record == true) 
            {
                BTN_RECORD.BackgroundImage = Properties.Resources.record_on;
                GB_OPERATIONS1.Enabled = true;
                GB_OPERATIONS2.Enabled = true;
                LB_RECORD_STATUS.Text = ": RECORDING ON";
                m_bmp_Back = PIC_INPUT.Image.Clone() as Bitmap;
                _PRINT_MSG("Recording Started.");
            }
        }

        private void _PRINT_MSG(string msg)
        {
            string s = Computer.GetTimeCode4Save_HH_MM_SS_MMM() + " : " + msg + System.Environment.NewLine;
            RICH_MESSAGE_WINDOW.AppendText(s);
            RICH_MESSAGE_WINDOW.ScrollToCaret();
            
        }

        private void BTN_PTRN_APPLY_Click(object sender, EventArgs e)
        {
            _SetRecordStatus(false);

            this.fm.listCommand = this.listCommand.ToList();

            this.Hide();
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

       

     
         
    }
    public class subclass  
    {
        public DLg_Processing handle;
 
        public subclass(DLg_Processing form)
        {
            this.handle = form;
        }
 
        public Bitmap _GetColorTemperature(byte[] rawImage, int imageW, int imageH)
        {
            return (Bitmap)Computer.HC_CONV_Raw2Color(rawImage, imageW, imageH);
        }

        public void LV_DisplayProcedure()
        {
            handle.LV_PROC_PROCESS.Items.Clear();

            if (handle.listCommand.Count != 0)
            {
                for (int i = 0; i < handle.listCommand.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = (i + 1).ToString("N0");

                    string strItem = handle.listCommand.ElementAt(i);
                    string[] strParse = strItem.Split(',');

                    lvi.SubItems.Add(strParse[0]);
                    lvi.SubItems.Add(strParse[1]);

                    handle.LV_PROC_PROCESS.Items.Add(lvi);

                }
            }
        }

        public byte[] GetBackBuffer()
        {
            int imageW = 0;
            int imageH = 0;
            byte[] rawImage = Computer.HC_CONV_Bmp2Raw(handle.m_bmp_Back, ref imageW, ref imageH);
            return rawImage;
        }
       
        public int GetParam_Brightness()
        {
            int nBR = 0;
            int.TryParse(handle.TXT_CTRL_BRIGHTNESS.Text, out nBR);
            return nBR;
        }
        public int GetParam_Contrast()
        {
            int nCont = 0;
            int.TryParse(handle.TXT_CTRL_CONTRAST.Text, out nCont);
            return nCont;
        }
        public int GetParam_Smoothing()
        {
            int nSmoothing = 0;
            int.TryParse(handle.TXT_CTRL_SMOOTHING.Text, out nSmoothing);
            return nSmoothing;
        }

        public byte[] GetInputImage(out int imageW, out int imageH)
        {
            Bitmap bmp = handle.PIC_INPUT.Image.Clone() as Bitmap;

            // set init
            imageW = imageH = 0;

            byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp, ref imageW, ref imageH);
            return rawImage;

        }
        public byte[] GetMainRawImage()
        {
            return handle.uc_view_processing.GetDisplay_Raw();
        }

        public void DrawHistogram()
        {
            byte[] rawImage = GetMainRawImage();
            int imageW = handle.uc_view_processing.VIEW_GetImageW();
            int imageH = handle.uc_view_processing.VIEW_GetImageH();

            handle.uc_histogram.SetImage(1, rawImage, imageW, imageH);
        }

    }
}
