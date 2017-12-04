using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using DEF_PARAMS;
using CD_Figure;

using WrapperUnion;
using CodeKing.Native;

namespace CD_VISION_DIALOG
{
    public partial class FormBaseRecp : Form
    {
        public WrapperINI INI_BASE_RECP = new WrapperINI();
        FormBaseRecpCopy dlgGenerateRecp = new FormBaseRecpCopy();

        public delegate void deleSendBaseRecp(BASE_RECP base_recp);
        public event deleSendBaseRecp eventDele_SendDelebaseResult;

        CHelper_BaseRecp helperBaseRecp;

        CFigureManager m_fm = new CFigureManager();

        public FormBaseRecp()
        {
            InitializeComponent();

            dlgGenerateRecp.event_GeneratedBaseRecp += new FormBaseRecpCopy.dele_GenerateBaseRecp(_GeneratedBaseRecp);

        }

        private void RecpCreateForm_Load(object sender, EventArgs e)
        {

            // Create Window for GenerateBaseRecp & set Hide
            dlgGenerateRecp.Show();
            dlgGenerateRecp.Hide();

            // Update Base Recp Files
            _UpdateBaseRecpFiles();


           
            helperBaseRecp = new CHelper_BaseRecp(LV_BASE_RECP, RDO_TYPE_ADI, RDO_TYPE_ACI);
            helperBaseRecp.SetBasePath(m_fm.param_path.i03_PATH_RECP_BASE);
            this.Padding = new Padding(10);
        }
        private void FormBaseRecp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            return;
        }

        private void _GeneratedBaseRecp(string msg)
        {
            _UpdateBaseRecpFiles();

            if (msg.Contains("[PASS]") == true) { MessageBox.Show(msg, "Recipe Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else { MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void _UpdateBaseRecpFiles()
        {
            string[] files = Directory.GetFiles(m_fm.param_path.i03_PATH_RECP_BASE, "*.INI");

            LV_BASE_RECP.Items.Clear();

            string SelectedType = RDO_TYPE_ADI.Checked == true ? "ADI" : "ACI";

            LV_BASE_RECP.BeginUpdate();

            for (int i = 0; i < files.Count(); i++)
            {
                try
                {
                    string strFile = Path.GetFileName(files[i]);

                    // Strong Exception 1 : Seperator
                    if (strFile.Contains("_") == false) continue;
                    // Strong Exception 2 : Extension
                    if (strFile.Contains(".INI") == false) continue;

                    string[] parseLayer = strFile.Split('_');
                    string strLayer = parseLayer[0];

                    // Strong Exception 3 : Length for Extension Parsing
                    if (parseLayer.Length < 2) continue;

                    string[] parseType = parseLayer[1].Split('.');
                    string strType = parseType[0];

                    if (strType == SelectedType)
                    {
                        int nCount = LV_BASE_RECP.Items.Count;

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = (nCount + 1).ToString("N0");

                        lvi.SubItems.Add(strLayer);

                        LV_BASE_RECP.Items.Add(lvi);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            LV_BASE_RECP.EndUpdate();
        }

        private void RDO_TYPE_ADI_CheckedChanged(object sender, EventArgs e) { BTN_REFRESH_BASE_RECP_Click(null, EventArgs.Empty); }
        private void RDO_TYPE_ACI_CheckedChanged(object sender, EventArgs e) { BTN_REFRESH_BASE_RECP_Click(null, EventArgs.Empty); }

        private void LV_BASE_INI_SelectedIndexChanged(object sender, EventArgs e){}
        private void LV_BASE_INI_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string strCurrentRecp = helperBaseRecp.GetSelectedBaseRecpFileName();

            TXT_TARGET_BASE_RECP.Text = helperBaseRecp.ExtensionRemove(strCurrentRecp);

            BASE_RECP recpCopy = _LoadRecpFromFS(strCurrentRecp);

            _UpdateRecp2UI(recpCopy);

        }
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
        private void _UpdateRecp2UI(BASE_RECP single)
        {
            helperBaseRecp._LV_SetColorIndex();

            /***/if (single.PARAM_01_LENS_INDEX == 1) { RDO_LENS_ALIGN.Checked = true; }
            else if (single.PARAM_01_LENS_INDEX == 2) { RDO_LENS_25X.Checked = true; }
            else if (single.PARAM_01_LENS_INDEX == 3) { RDO_LENS_50X.Checked = true; }
            else { helperBaseRecp.MSG_INVALID_PARAM_LENS_INDEX(); return; }

            /***/if (single.PARAM_02_LIGHT_INDEX == 1) { RDO_LIGHT_ALIGN.Checked = true; }
            else if (single.PARAM_02_LIGHT_INDEX == 2) { RDO_LIGHT_BF.Checked = true; }
            else if (single.PARAM_02_LIGHT_INDEX == 3) { RDO_LIGHT_DF.Checked = true; }
            else { helperBaseRecp.MSG_INVALID_PARAM_LIGHT_INDEX(); return; }

            TXT_LIGHT_VALUE.Text = single.PARAM_03_LIGHT_VALUE.ToString("N0");

            CHK_FOCUS_NONE.Checked = true;
            CHK_FOCUS_NONE.Checked = false;

            /***/if (single.PARAM_04_FOCUS_TYPE == 0) { CHK_FOCUS_NONE.Checked = true; }
            else if (single.PARAM_04_FOCUS_TYPE == 1) { CHK_FOCUS_ZAF.Checked = true; }
            else if (single.PARAM_04_FOCUS_TYPE == 2) { CHK_FOCUS_LAF.Checked = true; }
            else if (single.PARAM_04_FOCUS_TYPE == 3) { CHK_FOCUS_ZAF.Checked = true; CHK_FOCUS_LAF.Checked = true; }
            else if (single.PARAM_04_FOCUS_TYPE == 4) { CHK_FOCUS_IAF.Checked = true; } 
            else if (single.PARAM_04_FOCUS_TYPE == 5) { CHK_FOCUS_ZAF.Checked = true; CHK_FOCUS_IAF.Checked = true; }
            else if (single.PARAM_04_FOCUS_TYPE == 6) { CHK_FOCUS_LAF.Checked = true; CHK_FOCUS_IAF.Checked = true; }
            else { helperBaseRecp.MSG_INVALID_PARAM_FOCUS_TYPE(); return; }

            /***/if (single.PARAM_05_USE_CENTERING == 0) CHK_USE_CENTERING.Checked = false;
            else if (single.PARAM_05_USE_CENTERING == 1) CHK_USE_CENTERING.Checked = true;
            
            TXT_COMPEN_A.Text = single.PARAM_06_COMPEN_A.ToString("F4");
            TXT_COMPEN_B.Text = single.PARAM_06_COMPEN_B.ToString("F4");

            /***/if (single.PARAM_07_ALGORITHM_INDEX == 0) { RDO_ALGORITHM_MAXHAT.Checked = true; }
            else if (single.PARAM_07_ALGORITHM_INDEX == 1) { RDO_ALGORITHM_DIR_IN.Checked = true; }
            else if (single.PARAM_07_ALGORITHM_INDEX == 2) { RDO_ALGORITHM_DIR_EX.Checked = true; }
            else if (single.PARAM_07_ALGORITHM_INDEX == 3) { RDO_ALGORITHM_CARDIN.Checked = true; }
            else { helperBaseRecp.MSG_INVALID_PARAM_ALGORITHM(); return; }

            TXT_DMG_TOLERANCE.Text = single.PARAM_08_DMG_TOLERANCE.ToString("F4");
            TXT_EDGE_POSITION.Text = single.PARAM_09_EDGE_POSITION.ToString("F4");
        }

        private BASE_RECP _LoadRecpFromUI()
        {
            BASE_RECP single = new BASE_RECP();

            /***/if (RDO_LENS_ALIGN.Checked == true) single.PARAM_01_LENS_INDEX = 1;
            else if (RDO_LENS_25X.Checked == true) single.PARAM_01_LENS_INDEX = 2;
            else if (RDO_LENS_50X.Checked == true) single.PARAM_01_LENS_INDEX = 3;

            /***/if (RDO_LIGHT_ALIGN.Checked == true) single.PARAM_02_LIGHT_INDEX = 1;
            else if (RDO_LIGHT_BF.Checked == true) single.PARAM_02_LIGHT_INDEX = 2;
            else if (RDO_LIGHT_DF.Checked == true) single.PARAM_02_LIGHT_INDEX = 3;

            single.PARAM_03_LIGHT_VALUE = int.Parse(TXT_LIGHT_VALUE.Text);

            single.PARAM_04_FOCUS_TYPE = _GetFocusStatus();

            single.PARAM_05_USE_CENTERING = CHK_USE_CENTERING.Checked == false ? 0 : 1;

            single.PARAM_06_COMPEN_A = double.Parse(TXT_COMPEN_A.Text);
            single.PARAM_06_COMPEN_B = double.Parse(TXT_COMPEN_B.Text);

            /***/if (RDO_ALGORITHM_MAXHAT.Checked == true) single.PARAM_07_ALGORITHM_INDEX = 0;
            else if (RDO_ALGORITHM_DIR_IN.Checked == true) single.PARAM_07_ALGORITHM_INDEX = 1;
            else if (RDO_ALGORITHM_DIR_EX.Checked == true) single.PARAM_07_ALGORITHM_INDEX = 2;
            else if (RDO_ALGORITHM_CARDIN.Checked == true) single.PARAM_07_ALGORITHM_INDEX = 3;

            single.PARAM_08_DMG_TOLERANCE = double.Parse(TXT_DMG_TOLERANCE.Text);
            single.PARAM_09_EDGE_POSITION = double.Parse(TXT_EDGE_POSITION.Text);

            return single;
        }
        private void _SaveRecp(BASE_RECP recp, string strFilePath)
        {
            INI_BASE_RECP.Clear();

            string SECTION = "PARAM";

            INI_BASE_RECP.Add(SECTION);
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_01_LENS_INDEX, recp._param_lens_index, "ALIGN = 1 | 25X = 2 | 50X = 3");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_02_LIGHT_INDEX, recp._param_light_index, "ALIGN = 1 | BF = 2 | DF = 3");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_03_LIGHT_VALUE, recp.PARAM_03_LIGHT_VALUE, "VALUE : 0 ~ 255");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_04_FOCUS_TYPE, recp.PARAM_04_FOCUS_TYPE, " // NONE = 0 | ZAF = 1 | LAF = 2 | IAF = 4");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_05_USE_CENTERING, recp.PARAM_05_USE_CENTERING, " 0 = FALSE | 1 = TRUE");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_06_COMPEN_A, recp.PARAM_06_COMPEN_A, "Ax + B of A");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_06_COMPEN_B, recp.PARAM_06_COMPEN_B, "Ax + B of B");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_07_ALGORITHM_INDEX, recp._param_algoritm_index, "MAXHAT = 0 | DIR_IN = 1 | DIR_EX = 2 | CARDIN = 3");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_08_DMG_TOLERANCE, recp.PARAM_08_DMG_TOLERANCE, " Damage Tolerance(%) : 0.0 ~ 0.99");
            INI_BASE_RECP.Add(SECTION, BASE_RECP.DEF_PARAM_09_EDGE_POSITION, recp.PARAM_09_EDGE_POSITION, "Edge Position(%) : 0.0 ~ 1");

            INI_BASE_RECP.Save(strFilePath);
        }
        private BASE_RECP _LoadRecpFromFS(string strFilePath)
        {
            if (strFilePath == ".INI")
            {
                return new BASE_RECP();
            }

            INI_BASE_RECP.Clear();
            INI_BASE_RECP.Load(Path.Combine(m_fm.param_path.i03_PATH_RECP_BASE, strFilePath));


            BASE_RECP recp = new BASE_RECP();

            string strSection = "PARAM";

            recp.PARAM_00_BASE_RECP_NAME = strFilePath;
            //single.PARAM_01_LENS_INDEX = ??
            recp._param_lens_index = INI_BASE_RECP.GetStringField(strSection, BASE_RECP.DEF_PARAM_01_LENS_INDEX);
            //single.PARAM_02_LIGHT_INDEX = ??
            recp._param_light_index = INI_BASE_RECP.GetStringField(strSection, BASE_RECP.DEF_PARAM_02_LIGHT_INDEX);

            recp.PARAM_03_LIGHT_VALUE = INI_BASE_RECP.GetIntegerField(strSection, BASE_RECP.DEF_PARAM_03_LIGHT_VALUE);
            //single.PARAM_04_FOCUS_TYPE = ??
            recp.PARAM_04_FOCUS_TYPE = INI_BASE_RECP.GetIntegerField(strSection, BASE_RECP.DEF_PARAM_04_FOCUS_TYPE);

            recp.PARAM_05_USE_CENTERING = INI_BASE_RECP.GetIntegerField(strSection, BASE_RECP.DEF_PARAM_05_USE_CENTERING);

            recp.PARAM_06_COMPEN_A = INI_BASE_RECP.GetDoubleField(strSection, BASE_RECP.DEF_PARAM_06_COMPEN_A);
            recp.PARAM_06_COMPEN_B = INI_BASE_RECP.GetDoubleField(strSection, BASE_RECP.DEF_PARAM_06_COMPEN_B);
            //single.PARAM_09_ALGORITHM_INDEX  = ??
            recp._param_algoritm_index = INI_BASE_RECP.GetStringField(strSection, BASE_RECP.DEF_PARAM_07_ALGORITHM_INDEX);
            recp.PARAM_08_DMG_TOLERANCE = INI_BASE_RECP.GetDoubleField(strSection, BASE_RECP.DEF_PARAM_08_DMG_TOLERANCE);
            recp.PARAM_09_EDGE_POSITION = INI_BASE_RECP.GetDoubleField(strSection, BASE_RECP.DEF_PARAM_09_EDGE_POSITION);

            return recp;
        }

        //*****************************************************************************************
        // Button Events 
        //*****************************************************************************************

        private void BTN_REFRESH_BASE_RECP_Click(object sender, EventArgs e)
        {
            TXT_TARGET_BASE_RECP.Text = string.Empty;
            _UpdateBaseRecpFiles();
        }

        private void BTN_BASE_RECP_COPY_Click(object sender, EventArgs e)
        {
            if (helperBaseRecp.BaseRecpNullSelection() == true) return;

            string strSelectedBaseRecp = helperBaseRecp.GetSelectedBaseRecpFileName();

            dlgGenerateRecp.SetBaseFilePath(m_fm.param_path.i03_PATH_RECP_BASE);
            dlgGenerateRecp._TriggerSetBaseRecp(strSelectedBaseRecp);
            dlgGenerateRecp.ShowDialog();
        }

        private void BTN_BASE_RECP_REMOVE_Click(object sender, EventArgs e)
        {
            if (helperBaseRecp.BaseRecpNullSelection() == true) return;

            string strFileNameBaseRecp = helperBaseRecp.GetSelectedBaseRecpFileName();

            if (MessageBox.Show("Do You Want To Remove Recipe?\n" + strFileNameBaseRecp, "Base Recipe Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string strReal = helperBaseRecp.ExtensionRemove(strFileNameBaseRecp);

                // Deny delete operation if this is important recp.
                if (helperBaseRecp.IsImportantRecp( strReal) == true)
                {
                    helperBaseRecp.MSG_DENY_IMPOARTANT_RECP_REMOVE();
                }
                // if this is fucking shit, proceed ~
                else
                {
                    File.Delete(Path.Combine(m_fm.param_path.i03_PATH_RECP_BASE, strFileNameBaseRecp));
                    _UpdateBaseRecpFiles();
                }

            }
        }

        private void BTN_BASE_RECP_CANCEL_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void BTN_BASE_RECP_APPLY_Click(object sender, EventArgs e)
        {
            if (helperBaseRecp.BaseRecpNullSelection() == true) return;

            // step 01 : making full path
            string strCurrentRecp = helperBaseRecp.ExtensionAdd(TXT_TARGET_BASE_RECP.Text);
            strCurrentRecp = Path.Combine(m_fm.param_path.i03_PATH_RECP_BASE, strCurrentRecp);

            // step 02 : Load base recp & ui update
            BASE_RECP single = _LoadRecpFromFS(strCurrentRecp);
            single.PARAM_00_BASE_RECP_NAME = TXT_TARGET_BASE_RECP.Text;

            // step 03 : set Default Recp name.
            BASE_RECP baseRecpUI = _LoadRecpFromUI();
            baseRecpUI.PARAM_00_BASE_RECP_NAME = single.PARAM_00_BASE_RECP_NAME;

            // step 04 : Send Selecte Result To the main form.
            eventDele_SendDelebaseResult(baseRecpUI);

            // move to the MainForm 
            // private void deleFunc_GetBaseRecp(BASE_RECP single)

            this.Hide();
        }

        private void BTN_BASE_RECP_MODIFY_Click(object sender, EventArgs e)
        {
            // exception for : non selection
            if (helperBaseRecp.BaseRecpNullSelection() == true) return;

            // Get Selected Base Recp File Name
            string strSelectedBaseRecp = helperBaseRecp.GetSelectedBaseRecpFileName();

            // recp Preparation : UI vs File 
            BASE_RECP baseRecpUI = _LoadRecpFromUI();
            BASE_RECP baseRecpFS = _LoadRecpFromFS(strSelectedBaseRecp);

            string[] difference = baseRecpFS.GetComparedData(baseRecpUI);

            if (difference.Length == 0)
            {
                MessageBox.Show("There Is No Difference.", "Parameters Are Same", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (difference.Length != 0)
            {
                string strDiff = string.Empty;

                foreach (string d in difference)
                {
                    strDiff += d + System.Environment.NewLine;
                }

                strDiff += System.Environment.NewLine + "Do You Want To Apply?";

                if (MessageBox.Show(strDiff, "Changed Parameters", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _SaveRecp(baseRecpUI, Path.Combine(m_fm.param_path.i03_PATH_RECP_BASE, strSelectedBaseRecp));
                }
            }
            
        }

        // defines how far we are extending the Glass margins
        private Win32.MARGINS margins;

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
                margins.Top = Padding.Top;
                margins.Left = Padding.Left;
                margins.Bottom = Padding.Bottom;
                margins.Right = Padding.Right;
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
       
    }

    public class CHelper_BaseRecp
    {
        private ListView lv_base_recp;
        private RadioButton rdo_type_adi;
        private RadioButton rdo_type_aci;
        private string basePath {get;set;}

        public List<string> listVIP = new List<string>();

        public void SetBasePath(string strTemp)
        {
            basePath = strTemp;
        }

        public CHelper_BaseRecp(ListView lv, RadioButton rdoTypeADI, RadioButton rdoTypeACI)
        {
            this.lv_base_recp = lv;
            this.rdo_type_aci = rdoTypeACI;
            this.rdo_type_adi = rdoTypeADI;

            string[] arrLayer = { "ACTIVE", "CNT1", "GATE1", "HPDL", "SDR", "VIA" };
            string[] arrType = { "ADI", "ACI" };

            foreach (string sLayer in arrLayer)
            {
                foreach (string sType in arrType)
                {
                    listVIP.Add(sLayer + "_" + sType);
                }
            }
        }
        /// <summary>
        /// Return T/F whether the input is important or not 
        /// </summary>
        /// <param name="strRecpName"></param>
        /// <returns></returns>
        public bool IsImportantRecp(string strRecpName)
        {
            bool bRes = false;
            int nIndex = listVIP.IndexOf(strRecpName);
            if (nIndex >= 0) bRes = true;
            return bRes;
        }

        /// <summary>
        /// Exception For : Non Selection for the base recp
        /// </summary>
        public bool BaseRecpNullSelection()
        {
            bool bRes = false;

            if (lv_base_recp.FocusedItem == null)
            {
                MessageBox.Show("Select Base Recipe To Copy", "Null Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bRes = true;
            }
            return bRes;
        }
        /// <summary>
        /// Return Selected base Recp File Name ( pure file name with extension )
        /// </summary>
        public string GetSelectedBaseRecpFileName()
        {
            int nIndex = lv_base_recp.FocusedItem.Index;

            string strSelectedBaseRecpFileName = lv_base_recp.Items[nIndex].SubItems[1].Text.ToUpper();
            if (rdo_type_adi.Checked == true) { strSelectedBaseRecpFileName = strSelectedBaseRecpFileName + "_" + "ADI.INI"; }
            if (rdo_type_aci.Checked == true) { strSelectedBaseRecpFileName = strSelectedBaseRecpFileName + "_" + "ACI.INI"; }

            return strSelectedBaseRecpFileName;
        }
        /// <summary>
        /// remove ini extension
        /// </summary>
        public string ExtensionRemove(string strFileName)
        {
            return strFileName.Replace(".INI", "");
        }
        /// <summary>
        /// add ini extension
        /// </summary>
        public string ExtensionAdd(string strFileName)
        {
            return strFileName + ".INI";
        }
        /// <summary>
        /// coloring for selected recp
        /// </summary>
        public  void _LV_SetColorIndex()
        {
            int nIndex = lv_base_recp.FocusedItem.Index;

            int nItemCount = lv_base_recp.Items.Count;
            int nSubItemCount = 0;

            if (nItemCount != 0)
            {
                nSubItemCount = lv_base_recp.Columns.Count;

                lv_base_recp.BeginUpdate();

                for (int item = 0; item < nItemCount; item++)
                {
                    for (int col = 0; col < nSubItemCount; col++)
                    {
                        if (item == nIndex)
                        {
                            lv_base_recp.Items[item].SubItems[col].ForeColor = Color.Red;
                        }
                        else if (item != nIndex)
                        {
                            lv_base_recp.Items[item].SubItems[col].ForeColor = Color.White;
                        }
                    }
                }

                lv_base_recp.EndUpdate();
            }
        }

        public void MSG_DENY_IMPOARTANT_RECP_REMOVE()
        {
            MessageBox.Show("Delete Operation Denied.\n Target Recipe Is Preserved.","OPERATION FAILED", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public void MSG_INVALID_PARAM_LENS_INDEX()
        {
            MessageBox.Show("Invalid Parameter {LENS_INDEX}\n Please Check Value.", "Invalid Parameter", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MSG_INVALID_PARAM_LIGHT_INDEX()
        {
            MessageBox.Show("Invalid Parameter {LIGHT_INDEX}\n Please Check Value.", "Invalid Parameter", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MSG_INVALID_PARAM_FOCUS_TYPE()
        {
            MessageBox.Show("Invalid Parameter {FOCUS_TYPE}\n Please Check Value.", "Invalid Parameter", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MSG_INVALID_PARAM_ALGORITHM()
        {
            MessageBox.Show("Invalid Parameter {ALGORITHM}\n Please Check Value.", "Invalid Parameter", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }




    }
}
