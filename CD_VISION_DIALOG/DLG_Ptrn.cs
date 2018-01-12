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

using CD_View;
using DEF_PARAMS;

using CD_Measure; // measure-related
using CD_Figure; // recp-related

using CodeKing.Native;

namespace CD_VISION_DIALOG
{
    public partial class DLG_Ptrn : Form
    {
        public delegate void dele_ApplyParamPtrn(PARAM_PTRN param_ptrn);
        public event dele_ApplyParamPtrn eventDele_ApplyParamPtrn;

        public Bitmap MAIN_IMAGE = new Bitmap(100, 100);
        public Bitmap MAIN_TEACH = new Bitmap(100, 100);

        iPtrn m_pHandle = null;

        Boolean m_bFolderStatus = true;

        private CFigureManager m_fm = new CFigureManager();

        public DLG_Ptrn(iPtrn handle)
        {
            InitializeComponent();
            uc_view_ptrn.SetInit();

            m_pHandle = handle;

            uc_view_ptrn.eventDele_HereComesNewImage += new dele_HereComesNewImage(fuck);

            // backup input image
            MAIN_IMAGE = m_pHandle.iPtrn_LoadImage();

            m_bFolderStatus = false;
            BTN_SHOW_AND_HIDE_Click(null, EventArgs.Empty);
            
            uc_view_ptrn.SetDisplay(MAIN_IMAGE.Clone() as Bitmap);
            uc_view_ptrn.VIEW_SET_Mag_Origin();
            uc_view_ptrn.Refresh();

        }
        private void fuck()
        {
        }

        private void DLG_Ptrn_Load(object sender, EventArgs e)
        {
            //CHK_PTRN_SEARCH_GLOBAL.Checked = true;

            // set main view image
            BTN_SET_INTERNAL_IMAGE_Click(null, EventArgs.Empty);
        }

        public bool SetParam(CFigureManager fm)
        {
            m_fm = fm;

            PARAM_PTRN param_ptrn = fm.param_ptrn;

            string strPtrnPath = param_ptrn.PTRN_FILE;
            strPtrnPath = Path.Combine(m_fm.param_path.i11_PATH_IMG_PTRN, strPtrnPath);

            bool bPtrnMissing = false;

            if (File.Exists(strPtrnPath) == false)
            {
                bPtrnMissing = true;

                if (MessageBox.Show("Pattern Matching File Not Found. Do You Want To Proceed?", "Parameter Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return false;
                }
            }
            
            // set main view image
            BTN_SET_INTERNAL_IMAGE_Click(null, EventArgs.Empty);
            BTN_UPDATE_PTRN_LIST_Click(null, EventArgs.Empty);

            TXT_PTRN_FILE_NAME.Text = param_ptrn.PTRN_FILE;
            TXT_PTRN_ACC_RATIO.Text = param_ptrn.ACC_RATIO.ToString("F4");

            CHK_PTRN_APPLY_EDGE_BASED.Checked = param_ptrn.BOOL_EDGE_BASED;
            CHK_PTRN_SEARCH_GLOBAL.Checked = param_ptrn.BOOL_GLOBAL_SEARCHING;

            TXT_LOCAL_SEARCH_RGN_X.Text = param_ptrn.RC_SEARCH_RGN.X.ToString("F4");
            TXT_LOCAL_SEARCH_RGN_Y.Text = param_ptrn.RC_SEARCH_RGN.Y.ToString("F4");
            TXT_LOCAL_SEARCH_RGN_W.Text = param_ptrn.RC_SEARCH_RGN.Width.ToString("F4");
            TXT_LOCAL_SEARCH_RGN_H.Text = param_ptrn.RC_SEARCH_RGN.Height.ToString("F4");

            TXT_PTRN_TEACH_ROI_X.Text = param_ptrn.RC_TEMPLATE.X.ToString("F4");
            TXT_PTRN_TEACH_ROI_Y.Text = param_ptrn.RC_TEMPLATE.Y.ToString("F4");
            TXT_PTRN_TEACH_ROI_W.Text = param_ptrn.RC_TEMPLATE.Width.ToString("F4");
            TXT_PTRN_TEACH_ROI_H.Text = param_ptrn.RC_TEMPLATE.Height.ToString("F4");

            if (param_ptrn.BOOL_GLOBAL_SEARCHING == false &&
                param_ptrn.RC_SEARCH_RGN.Width != 0 &&
                param_ptrn.RC_SEARCH_RGN.Height != 0)
            {
                uc_view_ptrn.DrawRect(param_ptrn.RC_SEARCH_RGN, Color.Orange);
            }

            // only for Ptrn File Existance.
            if (bPtrnMissing == false)
            {
                Bitmap bmpPtrn = Bitmap.FromFile(strPtrnPath) as Bitmap;

                PIC_PTRN_NORMAL.Image = new Bitmap(bmpPtrn);
                PIC_PTRN_EDGE.Image = _Ptrn_Preprocess_Edge(bmpPtrn);

                bmpPtrn.Dispose();
            }

             return true;   
        }

        private void _ToUI_SetPTRNView(string strFileName)
        {
            string strFullFilePath = Path.Combine(m_fm.param_path.i11_PATH_IMG_PTRN, strFileName);

            if (File.Exists(strFullFilePath) == false)
            {
                MessageBox.Show("PTRN FILE NOT FOUND.", "File Existance Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                // just loading
                using (Bitmap bmp = Bitmap.FromFile(strFullFilePath) as Bitmap)
                {
                    Bitmap buff = new Bitmap(bmp);
                    // set display & backup 
                    PIC_PTRN_NORMAL.Image = buff.Clone() as Bitmap;
                    MAIN_TEACH = buff.Clone() as Bitmap;
                    PIC_PTRN_EDGE.Image = _Ptrn_Preprocess_Edge(buff.Clone() as Bitmap);

                    // update file name 
                    TXT_PTRN_FILE_NAME.Text = strFileName;

                    TXT_PTRN_TEACH_ROI_W.Text = buff.Width.ToString("F4");
                    TXT_PTRN_TEACH_ROI_H.Text = buff.Height.ToString("F4");

                    CHK_PTRN_APPLY_EDGE_BASED.Checked = false;
                }

                // make coloring
                //_LV_PTRN_Coloring();
            }
            //BTN_UPDATE_PTRN_LIST_Click(null, EventArgs.Empty);
        }
        private void LV_PTRN_SelectedIndexChanged(object sender, EventArgs e) 
        {
            //_Update_PTRN_Files();
            if (LV_PTRN.FocusedItem == null) return;

            int nIndex = LV_PTRN.FocusedItem.Index;

            string strFileName = LV_PTRN.Items[nIndex].SubItems[1].Text;

            _ToUI_SetPTRNView(strFileName);

            
        }

        private void BTN_UPDATE_PTRN_LIST_Click(object sender, EventArgs e)
        {
            //************************************************************************************
            // Get PTRN Files

            string[] arrRecpFiles = Directory.GetFiles(m_fm.param_path.i11_PATH_IMG_PTRN, "*.BMP");

            LV_PTRN.Items.Clear();

            LV_PTRN.BeginUpdate();

            for (int i = 0; i < arrRecpFiles.Length; i++)
            {
                string strFile = Path.GetFileName(arrRecpFiles.ElementAt(i));

                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString("N0");
                lvi.SubItems.Add(strFile);

                LV_PTRN.Items.Add(lvi);
            }

            LV_PTRN.EndUpdate();
            _LV_PTRN_Coloring();
        }

        private void _LV_PTRN_Coloring()
        {
            if (LV_PTRN.Items.Count == 0) return;

            string strCurrentRecpFile = TXT_PTRN_FILE_NAME.Text;

            int nSubCount = 0;

            for (int nData = 0; nData < LV_PTRN.Items.Count; nData++)
            {
                string strRecpFile = LV_PTRN.Items[nData].SubItems[1].Text;

                if (strRecpFile == strCurrentRecpFile)
                {
                    nSubCount = LV_PTRN.Items[nData].SubItems.Count;

                    LV_PTRN.Items[nData].UseItemStyleForSubItems = false;
                    LV_PTRN.Items[nData].ForeColor = Color.Red;

                    for (int i = 0; i < nSubCount; i++)
                    {
                        LV_PTRN.Items[nData].SubItems[i].ForeColor = Color.Red;
                    }
                }
                else
                {
                    LV_PTRN.Items[nData].UseItemStyleForSubItems = true;
                    LV_PTRN.Items[nData].ForeColor = Color.White;
                    for (int i = 0; i < nSubCount; i++)
                    {
                        LV_PTRN.Items[nData].SubItems[i].ForeColor = Color.White;
                    }
                }

            }
        }

        private void BTN_PTRN_APPLY_Click(object sender, EventArgs e)
        {
            string strCurrentPtrnFile = TXT_PTRN_FILE_NAME.Text;
            if (strCurrentPtrnFile == string.Empty || File.Exists(Path.Combine(m_fm.param_path.i11_PATH_IMG_PTRN, strCurrentPtrnFile)) == false)
            {
                MessageBox.Show("Invalid Teaching File. \n Please Check Teaching File Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float fTeachOrgX = 0; float fTeachOrgY = 0;
            float fTeachOrgW = 0; float fTeachOrgH = 0;

            float.TryParse(TXT_PTRN_TEACH_ROI_X.Text, out fTeachOrgX);
            float.TryParse(TXT_PTRN_TEACH_ROI_Y.Text, out fTeachOrgY);
            float.TryParse(TXT_PTRN_TEACH_ROI_W.Text, out fTeachOrgW);
            float.TryParse(TXT_PTRN_TEACH_ROI_H.Text, out fTeachOrgH);

            RectangleF rcTeaching = new RectangleF(fTeachOrgX, fTeachOrgY, fTeachOrgW, fTeachOrgH);

            if (fTeachOrgX < 0 || fTeachOrgY < 0 || fTeachOrgW <= 0 || fTeachOrgH <= 0)
            {
                MessageBox.Show("Invalid Teaching Data.\n Please Check Teaching Data Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            double fACCR = 0;
            double.TryParse(TXT_PTRN_ACC_RATIO.Text, out fACCR);

            if( fACCR <= 0 )
            {
                MessageBox.Show("Invalid Accept Ratio. \n Please Check Value Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Rectangle rcPtrnRgn = uc_view_ptrn.iGet_Roi_Ptrn();

            PARAM_PTRN ptrn = new PARAM_PTRN();

            ptrn.BOOL_EDGE_BASED = CHK_PTRN_APPLY_EDGE_BASED.Checked;
            ptrn.BOOL_GLOBAL_SEARCHING  = CHK_PTRN_SEARCH_GLOBAL.Checked;
            ptrn.PTRN_FILE = strCurrentPtrnFile;
            ptrn.RC_SEARCH_RGN = rcPtrnRgn;

            if( CHK_PTRN_SEARCH_GLOBAL.Checked == false)
            {
                float rx = float.Parse(TXT_LOCAL_SEARCH_RGN_X.Text);
                float ry = float.Parse(TXT_LOCAL_SEARCH_RGN_Y.Text);
                float rw = float.Parse(TXT_LOCAL_SEARCH_RGN_W.Text);
                float rh = float.Parse(TXT_LOCAL_SEARCH_RGN_H.Text);

                ptrn.RC_SEARCH_RGN = new RectangleF(rx, ry, rw, rh);
            }
            ptrn.RC_TEMPLATE = rcTeaching;
            ptrn.ACC_RATIO = fACCR;

            eventDele_ApplyParamPtrn(ptrn);
            this.Hide();
        }

        private void LV_PTRN_MouseDoubleClick(object sender, MouseEventArgs e) {}

        private void CHK_PTRN_APPLY_EDGE_BASED_CheckedChanged(object sender, EventArgs e)
        {
            uc_view_ptrn.VIEW_Set_Clear_DispObject();

            if (CHK_PTRN_APPLY_EDGE_BASED.Checked == true)
            {
                Bitmap bmpSrc = _Ptrn_Preprocess_Edge(MAIN_IMAGE);
                uc_view_ptrn.SetDisplay(bmpSrc);
            }
            else if (CHK_PTRN_APPLY_EDGE_BASED.Checked == false)
            {
                uc_view_ptrn.SetDisplay(MAIN_IMAGE);

            }
            uc_view_ptrn.VIEW_SET_Mag_Origin();
        }
        private Bitmap _Ptrn_Preprocess_Edge(Bitmap bmp)
        {
            int imageW = 0; int imageH = 0;
            byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp, ref imageW, ref imageH);

            rawImage = Computer.HC_TRANS_GradientImage(rawImage, imageW, imageH);

            return Computer.HC_CONV_Byte2Bmp(rawImage, imageW, imageH);
        }
        
        private void BTN_MATCHING_Click(object sender, EventArgs e)
        {
            uc_view_ptrn.VIEW_Set_Clear_DispObject();

            Bitmap bmpSource = null;
            Bitmap bmpTemplate = null;

            // get main image
            bmpSource = MAIN_IMAGE.Clone() as Bitmap;

            // get template image
            string strPtrnFile = Path.Combine(m_fm.param_path.i11_PATH_IMG_PTRN, TXT_PTRN_FILE_NAME.Text);

            if (File.Exists(strPtrnFile) == true) { bmpTemplate = Bitmap.FromFile(strPtrnFile).Clone() as Bitmap; }
            else
            {
                MessageBox.Show("Invalid Teaching File.\nPlease Check Teaching File Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // if edge based approach
            if (CHK_PTRN_APPLY_EDGE_BASED.Checked == true)
            {
                bmpSource = _Ptrn_Preprocess_Edge(bmpSource);
                bmpTemplate = _Ptrn_Preprocess_Edge(bmpTemplate);
            }

            // set essential variables
            RectangleF rcTemplate = new RectangleF();
            PointF ptTemplateCenter = new PointF();
            RectangleF rcSearching = new RectangleF();

            // searching region setting
            if (CHK_PTRN_SEARCH_GLOBAL.Checked == true)
            {
                rcSearching = new RectangleF(0, 0, bmpSource.Width, bmpSource.Height);
            }
            else if (CHK_PTRN_SEARCH_GLOBAL.Checked == false)
            {
                float px = 0; float py = 0; float pw = 0; float ph = 0;
                float.TryParse(TXT_LOCAL_SEARCH_RGN_X.Text, out px);
                float.TryParse(TXT_LOCAL_SEARCH_RGN_Y.Text, out py);
                float.TryParse(TXT_LOCAL_SEARCH_RGN_W.Text, out pw);
                float.TryParse(TXT_LOCAL_SEARCH_RGN_H.Text, out ph);

                rcSearching = new RectangleF(px, py, pw, ph);
            }


            // get accept ratio 
            double fAccRatio = double.Parse(TXT_PTRN_ACC_RATIO.Text);
            double fMatchingRatio = m_pHandle.iPtrn_DoPtrn(bmpSource, bmpTemplate, fAccRatio, rcSearching, out rcTemplate, out ptTemplateCenter);
            uc_view_ptrn.DrawPatternMathcing(ptTemplateCenter, rcTemplate);
            uc_view_ptrn.DrawRect(rcSearching, Color.Orange);
            uc_view_ptrn.Refresh();

            // display result
            _PRINT_MESSAGE(string.Format("MR  = {0} %", fMatchingRatio.ToString("F4")));
            _PRINT_MESSAGE(string.Format("POS[X,Y] = [{0},{1}]", ptTemplateCenter.X.ToString("F4"), ptTemplateCenter.Y.ToString("F4")));

            TXT_PTRN_TEACH_ROI_X.Text = rcTemplate.X.ToString("F4");
            TXT_PTRN_TEACH_ROI_Y.Text = rcTemplate.Y.ToString("F4");
            TXT_PTRN_TEACH_ROI_W.Text = rcTemplate.Width.ToString("F4");
            TXT_PTRN_TEACH_ROI_H.Text = rcTemplate.Height.ToString("F4");
        }

        private void _PRINT_MESSAGE(string strMessage)
        {
            RICH_MESSAGE.AppendText(strMessage+System.Environment.NewLine);
            RICH_MESSAGE.ScrollToCaret();
        }

        private void BTN_DRAW_PTRN_Click(object sender, EventArgs e)
        {
            uc_view_ptrn.iRemove_Roi_Ptrn();
            uc_view_ptrn.iDrawPtrn(true);
            uc_view_ptrn.Refresh();
        }
        private void BTN_PTRN_TEACH_OVERWRITE_Click(object sender, EventArgs e)
        {
            string fileName = TXT_PTRN_FILE_NAME.Text;

            Rectangle rc = uc_view_ptrn.iGet_Roi_Ptrn();

            if (rc.Width != 0 && rc.Height != 0 && rc.X > 0 && rc.Y > 0)
            {
                //DEF_PARAMS.PARAM_PTRN.set_PTRN_FILE
                string strFileName = uc_view_ptrn.iSave_Roi_Ptrn(Path.Combine(m_fm.param_path.i11_PATH_IMG_PTRN, fileName));

                _PRINT_MESSAGE("PTRN Teaching Finished.");

                _ToUI_SetPTRNView(Path.GetFileName(strFileName));
                BTN_UPDATE_PTRN_LIST_Click(null, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Invalid PTRN ROI.\nPlease Check ROI FOR PTRN Teaching.", "INVALID ROI DRAWING", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            uc_view_ptrn.iRemove_Roi_Ptrn();
            uc_view_ptrn.Refresh();
        }
        private void BTN_PTRN_TEACH_NEW_Click(object sender, EventArgs e)
        {
            Rectangle rc = uc_view_ptrn.iGet_Roi_Ptrn();

            if (rc.Width != 0 && rc.Height != 0 && rc.X > 0 && rc.Y > 0)
            {
                string strFileName = uc_view_ptrn.iSave_Roi_Ptrn("");

                _PRINT_MESSAGE("PTRN Teaching Finished.");

                _ToUI_SetPTRNView(Path.GetFileName(strFileName));
                BTN_UPDATE_PTRN_LIST_Click(null, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Invalid PTRN ROI.\nPlease Check ROI FOR PTRN Teaching.", "INVALID ROI DRAWING", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            uc_view_ptrn.iRemove_Roi_Ptrn();
            uc_view_ptrn.Refresh();
        }

        private void CHK_PTRN_SEARCH_GLOBAL_CheckedChanged(object sender, EventArgs e)
        {
            /***/if (CHK_PTRN_SEARCH_GLOBAL.Checked == false){GB_PTRN_SEARCH_RGN.Visible = true;}
            else if (CHK_PTRN_SEARCH_GLOBAL.Checked == true) 
            {
                uc_view_ptrn.iSet_Roi_Ptrn(new Rectangle(0, 0, 0, 0));
                GB_PTRN_SEARCH_RGN.Visible = false; 
            }

        }

        private void BTN_SET_SEARCHING_RGN_Click(object sender, EventArgs e)
        {
            uc_view_ptrn.iRemove_Roi_Ptrn();
            uc_view_ptrn.iDrawPtrn(true);
            uc_view_ptrn.Refresh();
        }

        private void BTN_PTRN_GET_LOCAL_SEARCHING_RGN_Click(object sender, EventArgs e)
        {
            Rectangle rc = uc_view_ptrn.iGet_Roi_Ptrn();

            if (m_fm.param_ptrn.BOOL_GLOBAL_SEARCHING == false) 
            {
                // not the global searching but there is no new drawn rectangle.
                if ( rc.Width <= 0 && rc.Height <=0)
                {
                    double x = double.Parse(TXT_LOCAL_SEARCH_RGN_X.Text);
                    double y = double.Parse(TXT_LOCAL_SEARCH_RGN_Y.Text);
                    double w = double.Parse(TXT_LOCAL_SEARCH_RGN_W.Text);
                    double h = double.Parse(TXT_LOCAL_SEARCH_RGN_H.Text);

                    // get the previous records
                    rc = new Rectangle((int)x, (int)y, (int)w, (int)h);
                }
            }

            // validation check
            if (rc.X < 0 || rc.Y < 0 || rc.Width < 0 || rc.Height < 0)
            {
                MessageBox.Show("Invalid Croodinates.", "Croodinate Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                TXT_LOCAL_SEARCH_RGN_X.Text = rc.X.ToString("N0").Replace(",", "");
                TXT_LOCAL_SEARCH_RGN_Y.Text = rc.Y.ToString("N0").Replace(",", "");
                TXT_LOCAL_SEARCH_RGN_W.Text = rc.Width.ToString("N0").Replace(",", "");
                TXT_LOCAL_SEARCH_RGN_H.Text = rc.Height.ToString("N0").Replace(",", "");
            }

            uc_view_ptrn.iSet_Roi_Ptrn(rc);
            uc_view_ptrn.Refresh();
        }

        private void BTN_SET_EXTERNAL_IMAGE_Click(object sender, EventArgs e)
        {
            MAIN_IMAGE = uc_view_ptrn.GetDisplay_Bmp();
            uc_view_ptrn.VIEW_SET_Mag_Origin();
            uc_view_ptrn.Refresh();
        }

        private void BTN_SET_INTERNAL_IMAGE_Click(object sender, EventArgs e)
        {
            MAIN_IMAGE = m_pHandle.iPtrn_LoadImage();
            uc_view_ptrn.SetDisplay(MAIN_IMAGE);
            uc_view_ptrn.VIEW_SET_Mag_Origin();
            uc_view_ptrn.Refresh();
        }

        private void BTN_PTRN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void uc_view_history_Load(object sender, EventArgs e)
        {
            this.Padding = new Padding(10);
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

        

        private void LV_PTRN_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            WrapperLV.SortData(LV_PTRN, e.Column);
        }

         

        private void BTN_SYNC_WITH_RECP_NAME_Click(object sender, EventArgs e)
        {
            string strText = m_fm.RECP_FILE;

            if (MessageBox.Show(string.Format("Do You Want To Make Sync. Recp Name and PTRN Teaching Name?\nTo {0}", strText), "Naming Syncronization", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string strName = Path.GetFileName(strText).ToUpper();
                strName = strName.Replace("XML", "BMP");
                TXT_PTRN_FILE_NAME.Text = strName;
            }
        }

      

        private void BTN_UPDATE_HISTORY_Click(object sender, EventArgs e)
        {
            LV_HISTORY_PTRN_ERROR.Items.Clear();


            string strPathHistory = m_fm.param_path.i16_PATH_HIST_PTRN;
            String[] allfiles = System.IO.Directory.GetFiles(strPathHistory, "*.*", System.IO.SearchOption.AllDirectories);

            LV_HISTORY_PTRN_ERROR.BeginUpdate();

            Array.Reverse(allfiles);

            for (int i = 0; i < allfiles.Length; i++)
            {
                string single = allfiles.ElementAt(i);

                string strFileName = Path.GetFileName(single);

                string strDate = single.Replace(m_fm.param_path.i16_PATH_HIST_PTRN + "\\", "");
                strDate = strDate.Replace(strFileName, "").Replace("\\", "");

                ListViewItem lvi = new ListViewItem();

                int nCount = LV_HISTORY_PTRN_ERROR.Items.Count;
                lvi.Text = (nCount + 1).ToString();
                lvi.SubItems.Add(strDate);
                lvi.SubItems.Add(strFileName);

                LV_HISTORY_PTRN_ERROR.Items.Add(lvi);
            }

            LV_HISTORY_PTRN_ERROR.EndUpdate();
        }

        #region WINDOW FOLDER - DATAVIEW
        private void BTN_SHOW_AND_HIDE_Click(object sender, EventArgs e)
        {
            _SetChange_WindowFolderStatus();
        }

        private void _SetChange_WindowFolderStatus()
        {
            if (m_bFolderStatus == true)
            {
                BTN_SHOW_AND_HIDE.BackgroundImage = Properties.Resources.recipe_left;
                this.Size = new Size(1800, 800);
             }
            else if (m_bFolderStatus == false)
            {
                BTN_SHOW_AND_HIDE.BackgroundImage = Properties.Resources.recipe_right;
                this.Size = new Size(1310, 800);
             }

            // reverse status 
            m_bFolderStatus = !m_bFolderStatus;
        }
        #endregion

        private void LV_HISTORY_PTRN_ERROR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV_HISTORY_PTRN_ERROR.FocusedItem == null) return;

            LV_HISTORY_PTRN_ERROR.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_HISTORY_PTRN_ERROR_SelectedIndexChanged);

            // Get Selected Item
            int nIndex = LV_HISTORY_PTRN_ERROR.FocusedItem.Index;

            string strDate = LV_HISTORY_PTRN_ERROR.Items[nIndex].SubItems[1].Text;
            string strImageFile = LV_HISTORY_PTRN_ERROR.Items[nIndex].SubItems[2].Text;

            // Parsing
            string PATH_DATE = Path.Combine(m_fm.param_path.i16_PATH_HIST_PTRN, strDate);
            string PATH_IMAGE = Path.Combine(m_fm.param_path.i16_PATH_HIST_PTRN, strDate, strImageFile);

            string strTimeCode = strImageFile.Substring(0, 12);
            string strInspFile = strTimeCode + "_PTRN_ERR.BMP";
            string PATH_INSP = Path.Combine(PATH_DATE, strInspFile);

            // Load Image 
            Bitmap bmpTemp = new Bitmap(PATH_IMAGE);
            Bitmap bmp = new Bitmap(bmpTemp);
            bmpTemp.Dispose();
            uc_view_ptrn.SetDisplay(bmp);
            System.Threading.Thread.Sleep(100);

            BTN_SET_EXTERNAL_IMAGE_Click(null, EventArgs.Empty);


            LV_HISTORY_PTRN_ERROR.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_HISTORY_PTRN_ERROR_SelectedIndexChanged);

            
        }

        private void BTN_RGN_INFO_PTRN_Click(object sender, EventArgs e)
        {
            RectangleF rcMatching = new RectangleF();

            float px = 0; float py = 0; float pw = 0; float ph = 0;
            float.TryParse(TXT_PTRN_TEACH_ROI_X.Text, out px);
            float.TryParse(TXT_PTRN_TEACH_ROI_Y.Text, out py);
            float.TryParse(TXT_PTRN_TEACH_ROI_W.Text, out pw);
            float.TryParse(TXT_PTRN_TEACH_ROI_H.Text, out ph);

            rcMatching = new RectangleF(px, py, pw, ph);

            uc_view_ptrn.iSet_Roi_Ptrn(Rectangle.Round(rcMatching));
            uc_view_ptrn.Refresh();
            
            
        }

        private void BTN_RGN_INFO_SEARCH_Click(object sender, EventArgs e)
        {
            RectangleF rcSearching = new RectangleF();

            float px = 0; float py = 0; float pw = 0; float ph = 0;
            float.TryParse(TXT_LOCAL_SEARCH_RGN_X.Text, out px);
            float.TryParse(TXT_LOCAL_SEARCH_RGN_Y.Text, out py);
            float.TryParse(TXT_LOCAL_SEARCH_RGN_W.Text, out pw);
            float.TryParse(TXT_LOCAL_SEARCH_RGN_H.Text, out ph);

            rcSearching = new RectangleF(px, py, pw, ph);
            uc_view_ptrn.iSet_Roi_Ptrn(Rectangle.Round(rcSearching));
            uc_view_ptrn.Refresh();
        }

         

       
    }
}
