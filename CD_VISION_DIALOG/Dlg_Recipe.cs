using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using CD_Figure;
using DEF_PARAMS;

using CodeKing.Native;

namespace CD_VISION_DIALOG
{
    public partial class Dlg_Recp : Form
    {
        public CFigureManager fm = new CFigureManager();

        public Dlg_Recp()
        {
            InitializeComponent();
        }

        public bool SetParam(CFigureManager figure)
        {
            fm = figure;

            PARAM_OPTICS param_optic = fm.param_optics;
            BASE_RECP baseRecp = fm.baseRecp;

            string strPathRecp = fm.RECP_FILE;
            strPathRecp = Path.Combine(fm.param_path.i04_PATH_RECP_REAL, strPathRecp);

            if (File.Exists(strPathRecp) == false)
            {
                MessageBox.Show("Recipe File Not Found. Please Set Proper Recipe.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // only for  [LENS + PIXEL VALUE] & [LIGHT INDEX + VALUE]
            int nCurCamm = param_optic.i01_CAM_INDEX;

            /***/if (nCurCamm == 1) RDO_CAM_ALIGN.Checked = true;
            else if (nCurCamm == 2) RDO_CAM_25X.Checked = true;
            else if (nCurCamm == 3) RDO_CAM_50X.Checked = true;

            TXT_PIXEL_RES.Text = param_optic.i02_PIXEL_RES.ToString("F6");

            int nLightIndex = param_optic.i03_LIGHT_INDEX;

            /***/if (nLightIndex == 1) RDO_LIGHT_ALIGN.Checked = true;
            else if (nLightIndex == 2) RDO_LIGHT_BF.Checked = true;
            else if (nLightIndex == 3) RDO_LIGHT_DF.Checked = true;

            TXT_LIGHT_VALUE.Text = param_optic.i04_LIGHT_VALUE.ToString("N0");

            TXT_CAM_EXP.Text = param_optic.i05_EXPOSURE.ToString("N0");

            CHK_FOCUS_NONE.Checked = true;
            CHK_FOCUS_NONE.Checked = false;

            TXT_MULTI_FRAME_SHOT_COUNT.Text = param_optic.i06_MULTI_SHOT_COUNT.ToString("N0");
            TXT_MULTI_FRAME_SHOT_DELAY.Text = param_optic.i07_MULTI_SHOT_DELAY.ToString("N0");

            /***/if (baseRecp.PARAM_04_FOCUS_TYPE == 0) { CHK_FOCUS_NONE.Checked = true; }
            else if (baseRecp.PARAM_04_FOCUS_TYPE == 1) { CHK_FOCUS_ZAF.Checked = true; }
            else if (baseRecp.PARAM_04_FOCUS_TYPE == 2) { CHK_FOCUS_LAF.Checked = true; }
            else if (baseRecp.PARAM_04_FOCUS_TYPE == 3) { CHK_FOCUS_ZAF.Checked = true; CHK_FOCUS_LAF.Checked = true; }
            else if (baseRecp.PARAM_04_FOCUS_TYPE == 4) { CHK_FOCUS_IAF.Checked = true; } 
            else if (baseRecp.PARAM_04_FOCUS_TYPE == 5) { CHK_FOCUS_ZAF.Checked = true; CHK_FOCUS_IAF.Checked = true; }
            else if (baseRecp.PARAM_04_FOCUS_TYPE == 6) { CHK_FOCUS_LAF.Checked = true; CHK_FOCUS_IAF.Checked = true; }

            /***/if (baseRecp.PARAM_05_USE_CENTERING == 0) CHK_USE_CENTERING.Checked = false;
            else if (baseRecp.PARAM_05_USE_CENTERING == 1) CHK_USE_CENTERING.Checked = true;


            return true;
        }

        private void Dlg_Recp_Load(object sender, EventArgs e)
        {
        }

        private void BTN_RECP_PARAM_CANCEL_Click(object sender, EventArgs e){this.Hide();}

        private void BTN_RECP_PARAM_APPLY_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Update Parameter Values?", "Parameter Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int nCurCam = -1;

                /***/if (RDO_CAM_ALIGN.Checked == true) nCurCam = PARAM_OPTICS.CAM_ALIGN;
                else if (RDO_CAM_25X.Checked == true) nCurCam = PARAM_OPTICS.CAM_25X;
                else if (RDO_CAM_50X.Checked == true) nCurCam = PARAM_OPTICS.CAM_50X;

                fm.param_optics.i01_CAM_INDEX = nCurCam;

                double fPixelRes = 0;
                double.TryParse(TXT_PIXEL_RES.Text, out fPixelRes);
                fm.param_optics.i02_PIXEL_RES = fPixelRes;

                int nLightIndex = -1;
                /***/if (RDO_LIGHT_ALIGN.Checked == true) nLightIndex = PARAM_OPTICS.LIGHT_ALIGN;
                else if (RDO_LIGHT_BF.Checked == true) nLightIndex = PARAM_OPTICS.LIGHT_BF;
                else if (RDO_LIGHT_DF.Checked == true) nLightIndex = PARAM_OPTICS.LIGHT_DF;

                fm.param_optics.i03_LIGHT_INDEX = nLightIndex;

                int nLightValue = 0;
                int.TryParse(TXT_LIGHT_VALUE.Text, out nLightValue);
                fm.param_optics.i04_LIGHT_VALUE = nLightValue;


                int nCamExp = 0;
                int.TryParse(TXT_CAM_EXP.Text.Replace(",",""), out nCamExp);


                fm.param_optics.i05_EXPOSURE = nCamExp;

                fm.baseRecp.PARAM_04_FOCUS_TYPE = _GetFocusStatus();

                int nMultiShotCount = 0;
                int.TryParse(TXT_MULTI_FRAME_SHOT_COUNT.Text.Replace(",",""), out nMultiShotCount);
                fm.param_optics.i06_MULTI_SHOT_COUNT = nMultiShotCount;

                int nMultiShotDelay = 0;
                int.TryParse(TXT_MULTI_FRAME_SHOT_DELAY.Text.Replace(",",""), out nMultiShotDelay);
                fm.param_optics.i07_MULTI_SHOT_DELAY = nMultiShotDelay;

                if (RDO_MULTI_FRAME_VALUE_AVG.Checked == true)
                {
                    fm.param_optics.i08_MULTI_SHOT_VALUE_AVG = true;
                }
                else if ( RDO_MULTI_FRAME_MEDIAN.Checked == true)
                {
                    fm.param_optics.i08_MULTI_SHOT_VALUE_AVG = false;
                }

                /***/if (CHK_USE_CENTERING.Checked == false) fm.baseRecp.PARAM_05_USE_CENTERING = 0;
                else if (CHK_USE_CENTERING.Checked == true) fm.baseRecp.PARAM_05_USE_CENTERING = 1;


            }
            this.Hide();
        }

         

        private void CHK_FOCUS_NONE_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_FOCUS_NONE.Checked == true)
            {
                CHK_FOCUS_ZAF.Checked = false;
                CHK_FOCUS_LAF.Checked = false;
                CHK_FOCUS_IAF.Checked = false;
            }
           
        }

        private void CHK_FOCUS_ZAF_CheckedChanged(object sender, EventArgs e) {  }
        private void CHK_FOCUS_LAF_CheckedChanged(object sender, EventArgs e) {  }
        private void CHK_FOCUS_IAF_CheckedChanged(object sender, EventArgs e) {  }

        private int _GetFocusStatus()
        {
            int nFocusValue = 0;

            if (CHK_FOCUS_NONE.Checked == true)
            {
                nFocusValue = 0;
            }
            else
            {
                if (CHK_FOCUS_ZAF.Checked == true) nFocusValue += 1;
                if (CHK_FOCUS_LAF.Checked == true) nFocusValue += 2;
                if (CHK_FOCUS_IAF.Checked == true) nFocusValue += 4;
            }
            return nFocusValue;
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
}
