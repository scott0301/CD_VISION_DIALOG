using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using System.IO;
using CD_View;
using DEF_PARAMS;
using CodeKing.Native;

namespace CD_VISION_DIALOG
{
    public partial class Dlg_HistoryP : Form
    {
        PARAM_CONFIG config = new PARAM_CONFIG();

        public Dlg_HistoryP()
        {
            InitializeComponent();
            uc_view_history.eventDele_HereComesNewImage += new dele_HereComesNewImage(fuck);

            uc_view_history.BOOL_DRAW_FOCUS_ROI = false;
            uc_view_history.BOOL_DRAW_PTRN_ROI = false;
        }
        private void fuck()
        {
        }
        public bool SetParam(PARAM_CONFIG config)
        {
            this.config = config;
            TXT_PATH_HISTORY_MATCHING.Text = this.config.i11_PATH_IMG_PTRN;

            return true;
        }

        private void BTN_UPDATE_HISTORY_Click(object sender, EventArgs e)
        {
            LV_HISTORY.Items.Clear();

            string strPathHistory = config.i16_PATH_HIST_PTRN;
            String[] allfiles = System.IO.Directory.GetFiles(strPathHistory, "*.*", System.IO.SearchOption.AllDirectories);

            LV_HISTORY.BeginUpdate();

            Array.Reverse(allfiles);

            for (int i = 0; i < allfiles.Length; i++)
            {
                string single = allfiles.ElementAt(i);

                string strFileName = Path.GetFileName(single);

                string strDate = single.Replace(config.i16_PATH_HIST_PTRN + "\\", "");
                strDate = strDate.Replace(strFileName, "").Replace("\\", "");

                ListViewItem lvi = new ListViewItem();

                int nCount = LV_HISTORY.Items.Count;
                lvi.Text = (nCount + 1).ToString();
                lvi.SubItems.Add(strDate);
                lvi.SubItems.Add(strFileName);

                LV_HISTORY.Items.Add(lvi);
            }

            LV_HISTORY.EndUpdate();
        }

        private void BTN_OPEN_HISTORY_FOLDER_Click(object sender, EventArgs e)
        {
            Process.Start(@config.i16_PATH_HIST_PTRN);

        }

        private void Dlg_HistoryP_Load(object sender, EventArgs e)
        {
            uc_view_history.SetInit();
            BTN_UPDATE_HISTORY_Click(null, EventArgs.Empty);
        }

        private void LV_HISTORY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV_HISTORY.FocusedItem == null) return;

            LV_HISTORY.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_HISTORY_SelectedIndexChanged);

       
            // Get Selected Item
            int nIndex = LV_HISTORY.FocusedItem.Index;

            string strDate = LV_HISTORY.Items[nIndex].SubItems[1].Text;
            string strImageFile = LV_HISTORY.Items[nIndex].SubItems[2].Text;

            // Parsing
            string PATH_DATE = Path.Combine(config.i16_PATH_HIST_PTRN, strDate);
            string PATH_IMAGE = Path.Combine(config.i16_PATH_HIST_PTRN, strDate, strImageFile);

            string strTimeCode = strImageFile.Substring(0, 12);
            string strInspFile = strTimeCode + "_PTRN_ERR.BMP";
            string PATH_INSP = Path.Combine(PATH_DATE, strInspFile);

            // Load Image 
            uc_view_history.ThreadCall_LoadImage(PATH_IMAGE);
            System.Threading.Thread.Sleep(100);

     

            LV_HISTORY.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_HISTORY_SelectedIndexChanged);
        }

        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
