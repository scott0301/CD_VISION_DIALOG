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
using CodeKing.Native;


namespace CD_VISION_DIALOG
{
    public partial class Dlg_Hacker : Form
    {
        public CHacker hacker = new CHacker();

        public Dlg_Hacker()
        {
            InitializeComponent();
        }
        public void SetParam(CHacker handle)
        {
            hacker = handle;
        }

        public void _ToUI_DisplayData()
        {

        }

        private void Dlg_Hacker_Load(object sender, EventArgs e)
        {

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

        

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CHK_SHOW_IMAGE_PROCESSING_CheckedChanged(object sender, EventArgs e)
        {
            hacker.BOOL_SHOW_IMAGE_PROCESS = CHK_SHOW_IMAGE_PROCESSING.Checked;
        }

        private void CHK_OFF_DETECT_CROSS_CheckedChanged(object sender, EventArgs e)
        {
            hacker.BOOL_SHOW_DETECT_CROSS = CHK_OFF_DETECT_CROSS.Checked;
        }

        

        

        private void CHK_USE_GRAB_SAVE_CheckedChanged(object sender, EventArgs e)
        {
            hacker.BOOL_USE_GRAB_SAVE = CHK_USE_GRAB_SAVE.Checked;
        }

        private void CHK_USE_FOCUS_IMAGE_CheckedChanged(object sender, EventArgs e)
        {
            hacker.BOOL_USE_FOCUS_SAVE = CHK_USE_FOCUS_IMAGE_SAVE.Checked;
        }

        private void CHK_USE_SAVE_EXPERIMENTAL_IMAGE_SET_CheckedChanged(object sender, EventArgs e)
        {
            hacker.BOOL_USE_SAVE_EXPERIMENTAL_IMAGE_SET = CHK_USE_SAVE_EXPERIMENTAL_IMAGE_SET.Checked;
        }

        private void BTN_SET_SAVE_IMAGE_SET_Click(object sender, EventArgs e)
        {
            string strPath = WrapperUnion.WrapperFile.SelectFolderAndGetName();
            TXT_SAVE_IMAGE_SET_PATH.Text = strPath;

            hacker.PATH_EXPERIMENTAL_IMAGE_SET = strPath;
        }
    }
}
