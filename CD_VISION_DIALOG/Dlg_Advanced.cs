using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DEF_PARAMS;
using CD_Measure;
using CodeKing.Native;


namespace CD_VISION_DIALOG
{
    public partial class Dlg_Advanced : Form
    {
        public CAdvancedMode advanced = new CAdvancedMode();

        public Dlg_Advanced(){InitializeComponent();}

        public void SetParam(CAdvancedMode handle){advanced = handle;}

        private void Dlg_Hacker_Load(object sender, EventArgs e){ }

        private void BTN_APPLY_Click(object sender, EventArgs e){this.Hide();}

        #region IMAGE SAVE OPTIONS

        private void CHK_USE_SAVE_MANUAL_GRAB_CheckedChanged(object sender, EventArgs e) { advanced.BOOL_USE_SAVE_MANUAL_GRAB = CHK_USE_SAVE_MANUAL_GRAB.Checked; }
        private void CHK_USE_SAVE_FOCUS_REGION_CheckedChanged(object sender, EventArgs e) { advanced.BOOL_USE_SAVE_FOCUS_REGION = CHK_USE_SAVE_FOCUS_REGION.Checked; }
        private void CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET_CheckedChanged(object sender, EventArgs e) { advanced.BOOL_USE_SAVE_SEQUENTIAL_IMAGE_SET = CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.Checked; }
        private void CHK_USE_SAVE_INPUT_IMAGE_CheckedChanged(object sender, EventArgs e){advanced.BOOL_USE_SAVE_INPUT_IMAGE = CHK_USE_SAVE_INPUT_IMAGE.Checked;}

        private void BTN_SET_SAVE_IMAGE_PATH_Click(object sender, EventArgs e)
        {
            string strPath = Computer.SelectFolderAndGetName();
            TXT_SAVE_IMAGE_SET_PATH.Text = strPath;
            advanced.PATH_EXPERIMENTAL_IMAGE_SET = strPath;
        }

        #endregion

        #region HISTORY RELATED OPTIONS

        private void CHK_USE_LEAVE_HISTORY_ERROR_PTRN_CheckedChanged(object sender, EventArgs e) { advanced.BOOL_USE_LEAVE_HISTORY_ERROR_PTRN = CHK_USE_LEAVE_HISTORY_ERROR_PTRN.Checked; }
        private void CHK_USE_LEAVE_HISTORY_MEASUREMENT_CheckedChanged(object sender, EventArgs e) { advanced.BOOL_USE_LEAVE_HISTORY_MEASUREMENT = CHK_USE_LEAVE_HISTORY_MEASUREMENT.Checked; }

        #endregion 

        private void CHK_SHOW_IMAGE_PROCESSING_CheckedChanged(object sender, EventArgs e)
        {
            advanced.BOOL_SHOW_IMAGE_PROCESS = CHK_SHOW_IMAGE_PROCESSING.Checked;
        }


        #region glass effect
        // defines how far we are extending the Glass margins
        private Win32.MARGINS margins;
        private int padding_TOP = 1000;
        private int padding_LFT = 1000;
        private int padding_RHT = 1000;
        private int padding_BTM = 1000;
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
