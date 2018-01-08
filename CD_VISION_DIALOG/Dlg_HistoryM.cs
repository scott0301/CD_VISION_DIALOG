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
using System.Diagnostics; // for process run
using System.Collections; // for list view sorting

using CD_View;
using CD_Figure;
using CD_Measure;
using DEF_PARAMS;

using CodeKing.Native;
 

namespace CD_VISION_DIALOG
{
    public partial class Dlg_HistoryM : Form
    {
        CFileIO fileIO = new CFileIO();

        public CFigureManager m_fm = new CFigureManager();

        public delegate void dele_ChangeRecp(string strPathRecp, string strPathImage);
        public event dele_ChangeRecp eventDele_ChangeRecp;

        public Dlg_HistoryM()
        {
            InitializeComponent();

            uc_view_history.eventDele_HereComesNewImage += new dele_HereComesNewImage(fuck);
            fileIO.EventThreadFinishedFileLoad += EventThreadFinished_LoadFile;
            fileIO.EventThreadFinishedFileLoad += EventThreadFinished_SaveFile;

            uc_view_history.BOOL_DRAW_FOCUS_ROI = false;
            uc_view_history.BOOL_DRAW_PTRN_ROI = false;
            
        }

        public bool SetParam(CFigureManager fm)
        {
            this.m_fm = fm;

 
            BTN_UPDATE_HISTORY_Click(null, EventArgs.Empty);

            return true;
        }

        #region MAIN BUTTON RELATED EVENTS
        private void BTN_PTRN_APPLY_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void BTN_HISTORY_EXPERIMENT_SET_Click(object sender, EventArgs e)
        {
            string strTestImage = TXT_HISTORY_PREV_IMAGE.Text;
            string strTestRecp = TXT_HISTORY_PREV_RECP.Text;

            if (File.Exists(strTestImage) == true &&
                File.Exists(strTestRecp) == true)
            {
                uc_view_history.ThreadCall_LoadImage(strTestImage);
                eventDele_ChangeRecp(strTestRecp, strTestImage);
            }
        }

        private void BTN_UPDATE_HISTORY_Click(object sender, EventArgs e)
        {
            LV_HISTORY.Items.Clear();
            RICH_HISTORY_MSG.Clear();
            TXT_HISTORY_PREV_RECP.Text = string.Empty;
            TXT_HISTORY_PREV_IMAGE.Text = string.Empty;


            string strPathHistory = m_fm.param_path.i15_PATH_HIST_MEASURE;
            String[] allfiles = System.IO.Directory.GetFiles(strPathHistory, "*.*", System.IO.SearchOption.AllDirectories);

            LV_HISTORY.BeginUpdate();

            Array.Reverse(allfiles);

            for (int i = 0; i < allfiles.Length; i++)
            {
                string single = allfiles.ElementAt(i);

                string strFileName = Path.GetFileName(single);

                if (Path.GetExtension(strFileName).ToUpper() == ".XML") continue;
                if (Path.GetExtension(strFileName).ToUpper() == ".TXT") continue;

                string strDate = single.Replace(m_fm.param_path.i15_PATH_HIST_MEASURE + "\\", "");
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
            Process.Start(m_fm.param_path.i15_PATH_HIST_MEASURE);
        }
        #endregion

        #region VIEWER RELATED EVENTS

        public void fuck()
        {
        }

        private void EventThreadFinished_LoadFile(object sender, CFileIO.ThreadFinishedEventArgs e)
        {
            this.UIThread(delegate
            {
                RICH_HISTORY_MSG.Clear();

                List<string> dispTextIn = new List<string>();

                for (int i = 0; i < e.list.Count; i++)
                {
                    string strData = e.list.ElementAt(i);

                    if (strData.Contains("@") == true)
                    {
                        dispTextIn.Add(strData);
                    }
                    else
                    {
                        RICH_HISTORY_MSG.AppendText(strData + System.Environment.NewLine);
                    }
                }
                RICH_HISTORY_MSG.ScrollToCaret();

                uc_view_history.SetDispTextObjects(dispTextIn);
                uc_view_history.Refresh();
            });


        }
        private void EventThreadFinished_SaveFile(object sender, CFileIO.ThreadFinishedEventArgs e)
        {
            // Nothing To do. Just Finish
        }

        private void Dlg_History_Load(object sender, EventArgs e)
        {
             uc_view_history.SetInit();
        }

        #endregion

        #region LIST_VIEW RELATED 

        private void LV_HISTORY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV_HISTORY.FocusedItem == null) return;

            LV_HISTORY.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_HISTORY_SelectedIndexChanged);

            // Message Window Clear
            RICH_HISTORY_MSG.Clear();

            // Get Selected Item
            int nIndex = LV_HISTORY.FocusedItem.Index;

            string strDate = LV_HISTORY.Items[nIndex].SubItems[1].Text;
            string strImageFile = LV_HISTORY.Items[nIndex].SubItems[2].Text;

            // Parsing
            string PATH_DATE = Path.Combine(m_fm.param_path.i15_PATH_HIST_MEASURE, strDate);
            string PATH_IMAGE = Path.Combine(m_fm.param_path.i15_PATH_HIST_MEASURE, strDate, strImageFile);

            string strTimeCode = strImageFile.Substring(0, 12);
            string strInspFile = strTimeCode + "_INSP.txt";
            string PATH_INSP = Path.Combine(PATH_DATE, strInspFile);

            // Load Image 
            uc_view_history.ThreadCall_LoadImage(PATH_IMAGE);
            System.Threading.Thread.Sleep(100);

            // Load Display Data and refresh
            if (File.Exists(PATH_INSP) == true) { uc_view_history.ThreadCall_LoadFile(fileIO, PATH_INSP); }
            uc_view_history.Refresh();

            // Get Recp File Names
            String[] allfiles = System.IO.Directory.GetFiles(m_fm.param_path.i15_PATH_HIST_MEASURE, "*.*", System.IO.SearchOption.AllDirectories);

            string PATH_PREV_RECP = string.Empty;

            for (int i = 0; i < allfiles.Length; i++)
            {
                if (allfiles.ElementAt(i).Contains(strTimeCode) && allfiles.ElementAt(i).Contains(".xml"))
                {
                    PATH_PREV_RECP = allfiles.ElementAt(i);
                }
            }
            // update set information 
            TXT_HISTORY_PREV_IMAGE.Text = PATH_IMAGE;
            TXT_HISTORY_PREV_RECP.Text = PATH_PREV_RECP;

            LV_HISTORY.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_HISTORY_SelectedIndexChanged);

        }

        private void LV_HISTORY_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            WrapperLV.SortData(LV_HISTORY, e.Column);
        }

        #endregion 

        public void AppedHistory(Bitmap bmp, List<string> listInspRes, List<string> listDispObjText)
        {
            System.Threading.Thread thr = new System.Threading.Thread(delegate()
            {
                string strTimeCode = Computer.GetTimeCode4Save_HH_MM_SS_MMM();

                string strPathImageView = strTimeCode + "_DCIM.BMP";
                string strPathImageTemplate = strTimeCode + "_" + m_fm.param_ptrn.PTRN_FILE;
                string strPathRecp = strTimeCode + "_" + m_fm.RECP_FILE;
                string strPathInspRes = strTimeCode + "_INSP.txt";

                // Setup Daily Directory
                string strPathDaily = Path.Combine(m_fm.param_path.i15_PATH_HIST_MEASURE, Computer.GetTimeCode4Save_YYYY_MM_DD());
                Computer.EnsureFolderExsistance(strPathDaily);

                // Backup Recp File
                string strRecipeSource = Path.Combine(m_fm.param_path.i04_PATH_RECP_REAL, m_fm.RECP_FILE);
                string strRecipeTarget = Path.Combine(strPathDaily, strPathRecp);

                if (File.Exists(strRecipeSource) == true && File.Exists(strRecipeTarget) == false)
                {
                    File.Copy(strRecipeSource, strRecipeTarget);
                }

                // Backup Image View File                
                string strFullPath = Path.Combine(strPathDaily, strPathImageView);
                uc_view_history.ThreadCall_SaveImage(strFullPath, bmp.Clone() as Bitmap);
                System.Threading.Thread.Sleep(200);
                // Get TextObjects Append
                listInspRes.AddRange(listDispObjText);

                // Backup Insp Result
                strPathInspRes = Path.Combine(strPathDaily, strPathInspRes);
                uc_view_history.ThreadCall_SaveFile(fileIO, strPathInspRes, listInspRes);
                System.Threading.Thread.Sleep(200);

            });
            thr.IsBackground = true;
            thr.Start();
        }

       #region RECP DUMPING 
        private void BTN_RECP_DUMP_OUT_Click(object sender, EventArgs e)
        {
            // get the recp file and ptrn file path 
            string strCurrentRecp = Path.Combine(m_fm.param_path.i04_PATH_RECP_REAL, m_fm.RECP_FILE);
            string strCurrentPtrn = Path.Combine(m_fm.param_path.i11_PATH_IMG_PTRN, m_fm.param_ptrn.PTRN_FILE);

            bool bSuccess = true;

            if (File.Exists(strCurrentRecp) == true)
            {
                if (File.Exists(strCurrentPtrn) == true)
                {
                    // if all pass for checking path validity
                    // getnerate zip list
                    List<string> list = new List<string>();
                    list.Add( strCurrentPtrn);
                    list.Add(strCurrentRecp);

                    // do it!!!
                    string strTimeCode = Computer.GetTImeCode4Save_YYYY_MM_DD_HH_MM_SS_MMM();
                    string strFileName = string.Format("{0}_RecpDump.zip", strTimeCode);
                    string strDest = Path.Combine(Computer.getPath_Desktop(), strFileName);

                    bSuccess = WrapperZip.CreateZipFiles(list, strDest);
                }
            }
            MessageBox.Show(string.Format("Dump Recp Process Finished. {0}", bSuccess == true ? "SUCESS" : "FAILED"));
        }

        private void BTN_RECP_DUMP_IN_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Zip Files|*.zip";
            openFileDialog1.Title = "Select a Zip File";

            string strFileNmae = string.Empty;


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                strFileNmae = openFileDialog1.FileName;
            }

            // cancel  & empty path exception
            if (strFileNmae == "") return;

            string strFolder = Computer.GetFolderPath(strFileNmae);

            List<string> list = WrapperZip.GetFileList_Without_unzip(strFileNmae);

            string strPathTemp = "c:\\Temp";

            // dump file must have two types of  files : ptrn + recp
            if (list.Count == 2)
            {
                string[] items = new string[2];

                // i don't know which one is recp or ptrn 
                items[0] = list.ElementAt(0).ToUpper();
                items[1] = list.ElementAt(1).ToUpper();

                string strPtrn = string.Empty;
                string strRecp = string.Empty;

                // check ? who is it.  A or B 
                if (items[0].Contains("BMP") == true) { strPtrn = items[0]; } else if (items[1].Contains("BMP") == true) { strPtrn = items[1]; }
                if (items[0].Contains("XML") == true) { strRecp = items[0]; } else if (items[1].Contains("XML") == true) { strRecp = items[1]; }

                if (strPtrn != string.Empty && strRecp != string.Empty)
                {
                    string destPtrn = strPtrn;
                    string destRecp = strRecp;

                    // unzip ptrn file and recp file 
                    WrapperZip.ExtractDirect(strFileNmae, destPtrn, strPathTemp);
                    WrapperZip.ExtractDirect(strFileNmae, destRecp, strPathTemp);

                    // path process 
                    destPtrn = Path.Combine(strPathTemp, destPtrn).Replace("/","\\");
                    destRecp = Path.Combine(strPathTemp, destRecp).Replace("/", "\\");

                    // generate appropriate full path 
                    string strAbsolutePtrn = Computer.GetFileName(destPtrn);
                    string strAbsoluteRecp = Computer.GetFileName(destRecp);
                    string strDestPtrn = Path.Combine(m_fm.param_path.i11_PATH_IMG_PTRN, strAbsolutePtrn);
                    string strDestRecp = Path.Combine(m_fm.param_path.i04_PATH_RECP_REAL, strAbsoluteRecp);

                    // is there any duplicate?
                    if (File.Exists(strDestPtrn) == true || File.Exists(strDestRecp) == true)
                    {
                        // do you want to overwrite??
                        if (MessageBox.Show("File Existance Detected.\nDo You Want To Overwrite?", "File Duplication Warrning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // delete first
                            Computer.DeleteFile(strAbsolutePtrn);
                            Computer.DeleteFile(strAbsoluteRecp);

                            // and copy
                            Computer.FileCopy(destPtrn, strDestPtrn);
                            Computer.FileCopy(destRecp, strDestRecp);

                            // recp change
                            eventDele_ChangeRecp(strDestRecp, "");
                        }
                    }
                    else // just do it.
                    {
                        // just copy 
                        Computer.FileCopy(destPtrn, strDestPtrn);
                        Computer.FileCopy(destRecp, strDestRecp);
                        // and change
                        eventDele_ChangeRecp(strDestRecp, "");
                    }
                    MessageBox.Show("Recp Changed", "Dump Automation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }

            }
        }

        #endregion

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
