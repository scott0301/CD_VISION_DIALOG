using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using System.IO;
using System.Diagnostics;

using DEF_PARAMS;
using DispObject;

using CD_Figure;
using CD_Measure;
using CD_Paramter;
using CD_View;
using UC_LogView;
using WrapperCognex;
using WrapperUnion;

using IPCUtility;
using Remote;

using CodeKing.Native;
using System.Drawing.Drawing2D;

namespace CD_VISION_DIALOG
{
    public partial class CDMainForm : Form, iPtrn
    {

       #region 제어부와 연관 부분
        public UdpIPC MsgIPC = null;
        public IpcBuffer sharedIPC1 = null;

        
        private void IPC_Init()
        {
            IpcClient ipcC = new IpcClient();

            sharedIPC1 = ipcC.IpcConnect<IpcBuffer>("Remote", "Shared1");

            MsgIPC = new UdpIPC(1024, 5512);
            MsgIPC.UdpRecvEvent += new UdpIPC.UdpRecvHandler(udpIPC_UdpRecvEvent);
        }

        void udpIPC_UdpRecvEvent(UdpIPC.UdpRecvEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new IPCUtility.UdpIPC.UdpRecvHandler(udpIPC_UdpRecvEvent), e);
                return;
            }

            //*************************************************************************************
            // measuremet START
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_MEASURE_START)
            {
                string strData = e.Msg;
                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-MEASUREMENT_START", DEF_OPERATION.OPER_06_COMM);

            }

            //*************************************************************************************
            // measuremet END
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_MEASURE_END)
            {
                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-MEASUREMENT_END", DEF_OPERATION.OPER_06_COMM);

            }

            //*************************************************************************************
            // measuremet
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_IMAGE_MEASURE_REQ) //fin
            {
                byte[] buffer = null;

                buffer = sharedIPC1.Buffer;
                GrabInfo obj = ByteArraySerializer<GrabInfo>.Deserialize(buffer);


                Remote.MeasureInfo mi = CDOL_OnReqMeasure(obj.POS.X, obj.POS.Y, obj.IMG.Buffer, obj.IMG.Width, obj.IMG.Height);
                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-MEASURE", DEF_OPERATION.OPER_06_COMM);
                UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK] POS = [{0:F3},{1:F3}] SUCESS-{2}", obj.POS.X, obj.POS.Y, mi.Result), DEF_OPERATION.OPER_06_COMM);

                sharedIPC1.Buffer = IPCUtility.ByteArraySerializer<MeasureInfo>.Serialize(mi);
                MsgIPC.Send(IPC_ID.MC_IMAGE_MEASURE_REP, "", 5510);

            }
            //*************************************************************************************
            // matching
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_IMAGE_MATCHING_REQ) //fin
            {
                byte[] buffer = null;

                buffer = sharedIPC1.Buffer;
                GrabInfo obj = ByteArraySerializer<GrabInfo>.Deserialize(buffer);

                Remote.MatcingInfo mi = CDOL_OnReqMatching(obj.POS.X, obj.POS.Y, obj.IMG.Buffer, obj.IMG.Width, obj.IMG.Height);

                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-MATCHING", DEF_OPERATION.OPER_06_COMM);

                if (mi.Result == -1)
                {
                    UC_LOG_VIEWER.WRITE_LOG("ERR - PTRN", DEF_OPERATION.OPER_05_MEAS);
                }
                else
                {
                    UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]POS[{0:F3},{1:F3}] SCORE[{2:F3}%]", mi.X, mi.Y, mi.Score ), DEF_OPERATION.OPER_06_COMM);
                }

                sharedIPC1.Buffer = IPCUtility.ByteArraySerializer<MatcingInfo>.Serialize(mi);
                MsgIPC.Send(IPC_ID.MC_IMAGE_MATCHING_REP, "", 5510);

            }
            //*************************************************************************************
            // recipe change
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_MACRO_LOAD_REQ) //fin
            {
                UC_LOG_VIEWER.WRITE_LOG(string.Format("[★REQ]-RECP CHANGE → {0}", e.Msg), DEF_OPERATION.OPER_06_COMM);

                Remote.RecipeInfo ri = CDOL_OnReqLoadMacro(e.Msg);

                UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]CAM[{0:0}:{1:f6}] LIGHT[{2:00}:{3:000}] FOCUS[{4}] CNT-T[{5}]", ri.CamNo, ri.CamRes, ri.IllNo, ri.IllValue, ri.FI.FocusType, ri.IsCentering), DEF_OPERATION.OPER_06_COMM);

                sharedIPC1.Buffer = IPCUtility.ByteArraySerializer<RecipeInfo>.Serialize(ri);
                MsgIPC.Send(IPC_ID.MC_MACRO_LOAD_REP, "", 5510);
            }
            //*************************************************************************************
            // image grab
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_IMAGE_GRAB_REP) //fin
            {
                try
                {
                    byte[] buffer = null;

                    buffer = sharedIPC1.Buffer;
                    GrabInfo obj = ByteArraySerializer<GrabInfo>.Deserialize(buffer);

                     CDOL_OnRepGrab(obj.CAM.CamNo,
                        obj.CAM.PixelRes,
                        obj.ILL.IllNo,
                        obj.ILL.IllValue,
                        obj.IMG.Buffer,
                        obj.IMG.Width,
                        obj.IMG.Height);

                    // update croodinates according to ptrn matching
                     BTN_PTRN_MATCH_Click(null, EventArgs.Empty);

                     UC_LOG_VIEWER.WRITE_LOG("★[REQ]-GRAB", DEF_OPERATION.OPER_06_COMM);
                     UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]CAM[{0:00}:{1:F6}] LCH[{2:00}:{3:000} IMG[{4}X{5}]", 
                         obj.CAM.CamNo, obj.CAM.PixelRes, obj.ILL.IllNo, obj.ILL.IllValue, obj.IMG.Width, obj.IMG.Height), DEF_OPERATION.OPER_04_IMAGE);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //*************************************************************************************
            // recipe list
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_MACRO_LIST_REQ) //fin
            {
                Remote.RecipeList rl = new Remote.RecipeList();

                rl.Recipes = CDOL_OnReqMacroList();
                sharedIPC1.Buffer = IPCUtility.ByteArraySerializer<RecipeList>.Serialize(rl);

                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-GET RECP LIST", DEF_OPERATION.OPER_06_COMM);
                UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK] Returned Recps {0}", rl.Recipes.Count), DEF_OPERATION.OPER_02_RECP);

                MsgIPC.Send(IPC_ID.MC_MACRO_LIST_REP, "", 5510);
            }
        }



        public Remote.MeasureInfo CDOL_OnReqMeasure(double x, double y, byte[] rawImage, int imageW, int imageH)
        {

            // do Measure , 
            Remote.MeasureInfo mi = _Do_Measurement(rawImage, imageW, imageH, false, string.Empty, x, y);

            return mi;
        }


        public Remote.MatcingInfo CDOL_OnReqMatching(double x, double y, byte[] rawImage, int imageW, int imageH)
        {
            imageView1.VIEW_Set_Clear_DispObject();
            imageView1.SetDisplay(rawImage, imageW, imageH);

            RectangleF rcTemplate/*****/ = new RectangleF();
            double fMatchingRatio = 0;

            PointF ptTemplateCenter = _DO_PTRN_And_Get_TemplatePos(out rcTemplate, out fMatchingRatio);

             imageView1.DrawPatternMathcing(ptTemplateCenter, rcTemplate);
            imageView1.Refresh();

            // Modify
            Remote.MatcingInfo mi = new Remote.MatcingInfo();
            if (fMatchingRatio == 0)
            {
                mi.Result = -1;
                mi.Macro = TXT_PATH_RECP_FILE.Text.Replace(".xml", "");

                if (CHK_USE_SAVE_PTRN_ERR.Checked == true)
                {
                    string strPathDir = Path.Combine(imageView1.fm.config.i16_PATH_HIST_PTRN, WrapperDateTime.GetTimeCode4Save_YYYY_MM_DD());
                    WrapperFile.EnsureFolderExsistance(strPathDir);

                    string strTimecode = WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM();
                    imageView2.ThreadCall_SaveImage(Path.Combine(strPathDir, strTimecode + "_PTRN_ERR.BMP"), rawImage, imageW, imageH);
                }

            }
            else
            {
                mi.Result = 0; // pass 

                RectangleF rc = imageView1.fm.param_ptrn.RC_TEMPLATE;
                PointF ptPTRN_Org = new PointF(rc.X, rc.Y);

                mi.Macro = TXT_PATH_RECP_FILE.Text.Replace(".xml", "");
                mi.RegX = CRect.GetCenter(rc).X;
                mi.RegY = CRect.GetCenter(rc).Y;
                mi.X = ptTemplateCenter.X;
                mi.Y = ptTemplateCenter.Y;
                mi.Score = fMatchingRatio;


            }
            return mi;
        }

        public Remote.RecipeInfo CDOL_OnReqLoadMacro(string macro)
        {
            string strMacroPath = Path.Combine(imageView1.fm.config.i04_PATH_RECP_REAL, macro);
            strMacroPath +=  ".xml";

            // Modify
            bool ret = false;
            ret = File.Exists(strMacroPath);

            Remote.RecipeInfo ri = new Remote.RecipeInfo();
            if (!ret)
            {
                ri.Result = -1;
            }
            else
            {
                COMMON_ChangeRecp(strMacroPath);

                PARAM_OPTICS param_optic = imageView1.fm.param_optics;

                ri.CamNo = param_optic.CAM_INDEX;
                ri.CamRes = param_optic.PIXEL_RES;
                ri.IllNo = param_optic.LIGHT_INDEX;
                ri.IllValue = param_optic.LIGHT_VALUE;
                ri.IsCentering = imageView1.fm.baseRecp.PARAM_05_USE_CENTERING;
                FocusInfo fi = new FocusInfo();

                fi.FocusType = imageView1.fm.baseRecp.PARAM_04_FOCUS_TYPE; ;


                PointF ptTemplateCenter = CRect.GetCenter(imageView1.fm.param_ptrn.RC_TEMPLATE);

                // template 좌표 - focus roi 좌표
                fi.X = imageView1.fm._rc_focus.X - ptTemplateCenter.X;
                fi.Y = imageView1.fm._rc_focus.Y - ptTemplateCenter.Y;
                fi.W = imageView1.fm._rc_focus.Width;
                fi.H = imageView1.fm._rc_focus.Height;

                ri.FI = fi;
            }
            return ri;
        }


        public List<string> CDOL_OnReqMacroList()
        {
            return COMMON_GetRecpList();
        }

        public void CDOL_OnRepGrab(int camNo, double camRes, int illNo, int illValue, byte[] buf, int w, int h)
        {
            PARAM_OPTICS param_optic = imageView1.fm.param_optics;

            param_optic.CAM_INDEX = camNo;
            param_optic.PIXEL_RES = camRes;
            param_optic.LIGHT_INDEX = illNo;
            param_optic.LIGHT_VALUE = illValue;

            imageView1.VIEW_Set_Clear_DispObject();
            imageView1.SetDisplay(buf, w, h);
            imageView1.VIEW_SET_Mag_Origin();
            imageView1.Refresh();

            if (m_hacker.BOOL_USE_GRAB_SAVE == true)
            {
                string strPathDir = Path.Combine("c:\\", WrapperDateTime.GetTimeCode4Save_YYYY_MM_DD());
                WrapperFile.EnsureFolderExsistance(strPathDir);

                Bitmap bmp = imageView1.GetDisplay_Bmp();

                string strTimecode = WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM();
                imageView1.ThreadCall_SaveImage(Path.Combine(strPathDir, strTimecode + "_GRAB.BMP"), bmp);
            }
        }
        #endregion


        //*******************************************************************************************
        // FUCK THAT SHIT
        //*******************************************************************************************

 
        BASE_RECP param_baseRecp = new BASE_RECP();

        public cogWrapper wrapperCog = new cogWrapper();

        public CMeasureReport report = new CMeasureReport();

        int m_DrawType_Master = UC_Graph.IFX_GRAPH_TYPE.PROJ_V;
        int m_DrawType_Slave = UC_Graph.IFX_GRAPH_TYPE.PROJ_V;

        public PARAM_CONFIG config = new PARAM_CONFIG();
        public WrapperINI INI_LAYERS = new WrapperINI();
        public WrapperINI INI_CONFIG = new WrapperINI();

        public Tact time = new Tact();

        public int nHarras = 0;
        public bool bActivateCheatMode = false;
        public int nSecretKeyPress = 0;
        public List<string> listCheatKey = new List<string>();

        FormBaseRecp formBaseRecp = new FormBaseRecp();

        DLG_Ptrn dlgPtrn = null;
        Dlg_Recp dlgRecp = null;

        DLg_Processing dlgProcessing = null;

        Dlg_HistoryP dlgHistP = null;
        Dlg_HistoryM dlgHistM = null;
        Dlg_Config dlgConfig = null;
        Dlg_Tunning dlgTunning = null;
        Dlg_Hacker dlgHacker = null;
        Dlg_Spc dlgSPC = null;


        CHacker m_hacker = new CHacker();




        //****************************************************
        // Constant Variable
        //****************************************************

        const int FIGURE_RCT = 0;
        const int FIGURE_CIR = 1;
        const int FIGURE_OVL = 2;

        public CDMainForm()
        {
            InitializeComponent();
            
            IPC_Init();

            imageView1.eventDele_HereComesNewImage += new dele_HereComesNewImage(fuck1);
            imageView2.eventDele_HereComesNewImage += new dele_HereComesNewImage(fuck2);
         
            formBaseRecp.eventDele_SendDelebaseResult += new FormBaseRecp.deleSendBaseRecp(deleFunc_GetBaseRecp);

            dlgPtrn = new DLG_Ptrn(this as iPtrn);
            dlgPtrn.eventDele_ApplyParamPtrn += new DLG_Ptrn.dele_ApplyParamPtrn(deleFunc_GetParamPtrn);

            dlgRecp = new Dlg_Recp();

            dlgHistM = new Dlg_HistoryM();
            dlgHistM.eventDele_ChangeRecp += new Dlg_HistoryM.dele_ChangeRecp(deleFunc_ChangeRecp);

            dlgHistP = new Dlg_HistoryP();
            dlgProcessing = new DLg_Processing();


            dlgConfig = new Dlg_Config();
            dlgConfig.eventDele_ChangeConfig += new Dlg_Config.dele_ChangeConfig(deleFunc_ChangeConfig);

            dlgTunning = new Dlg_Tunning();
            dlgTunning.eventDele_FinishTunning += new Dlg_Tunning.dele_FinishTunning(deleFunc_ChangeTunning);

            dlgHacker = new Dlg_Hacker();

            dlgSPC = new Dlg_Spc();

            this.Padding = new Padding(10);
        }

       #region ITERFACE FOR PATTERN MATCHING

        // just for cognex initialization delay removal. 
        public void iPtrn_Init()
        {
            Bitmap bmpSource = new Bitmap(100, 100);
            Bitmap bmpTemplate = new Bitmap(10, 10);

            double fRatio = 100;

            RectangleF rcTemplate = new RectangleF();
            PointF ptTemplateCenter = new Point();

            iPtrn_DoPtrn(bmpSource, bmpTemplate, fRatio, new RectangleF(0,0, 100, 100), out rcTemplate, out ptTemplateCenter);
        }
        public Bitmap iPtrn_LoadImage()
        {
            return imageView1.GetDisplay_Bmp();
        }
        public double iPtrn_DoPtrn(Bitmap bmpSource, Bitmap bmpTemplate, double fRatio, RectangleF rcSearching, out RectangleF rcTemplate, out PointF ptTemplateCenter)
        {
            rcTemplate = new RectangleF();
            ptTemplateCenter = new PointF();

            if( rcSearching.Width == 0 || rcSearching.Height == 0) 
            {
                MessageBox.Show("Invalid Searching Region", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            ResultPTRN ptrn = new ResultPTRN();

            wrapperCog.PARAM_NUM_TO_FIND = 1;
            wrapperCog.PARAM_ACCEPT_RATIO = fRatio;
            wrapperCog.PARAM_RC_SEARCH = rcSearching;
            wrapperCog.PARAM_PT_RELATIVE_ORIGIN = new PointF(0, 0);

            wrapperCog.Do_Ptrn(bmpSource, bmpTemplate);

            ptrn = wrapperCog.ptrnResult;

            double fMatchingRatio = 0;
            rcTemplate = new RectangleF(0, 0, 0, 0);
            ptTemplateCenter = new PointF(0, 0);

            if (ptrn.Count >= 1)
            {
                ptTemplateCenter = ptrn.resultList.ElementAt(0).ptCenter;
                rcTemplate = ptrn.resultList.ElementAt(0).rc;
                fMatchingRatio = ptrn.resultList.ElementAt(0).fScore * 100.0;
            }
            return fMatchingRatio;
        }

       #endregion

        private void Load_INI_Config()
        {
            string strPath = Path.Combine(Application.StartupPath, "INI", "CONFIG.INI");

            if (File.Exists(strPath) == false)
            {
                MessageBox.Show("Configuration INI File Not Found.", "INI LOADING ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            INI_CONFIG.Clear();
            INI_CONFIG.Load( strPath);

            string strSection = "PATH";

            this.config.i01_PATH_MAIN /****/= INI_CONFIG.GetStringField(strSection, "i01_PATH_MAIN");
            this.config.i02_PATH_DATA_DUMP /****/= INI_CONFIG.GetStringField(strSection, "i02_PATH_DATA_DUMP");
            this.config.i03_PATH_RECP_BASE /****/= INI_CONFIG.GetStringField(strSection, "i03_PATH_RECP_BASE");
            this.config.i04_PATH_RECP_REAL /****/= INI_CONFIG.GetStringField(strSection, "i04_PATH_RECP_REAL");
            
            this.config.i10_PATH_IMG_ORG  /****/= INI_CONFIG.GetStringField(strSection, "i10_PATH_IMG_ORG");
            this.config.i11_PATH_IMG_PTRN /****/= INI_CONFIG.GetStringField(strSection, "i11_PATH_IMG_PTRN");
            
            this.config.i15_PATH_HIST_MEASURE  /****/= INI_CONFIG.GetStringField(strSection, "i15_PATH_HIST_MEASURE");
            this.config.i16_PATH_HIST_PTRN  /****/= INI_CONFIG.GetStringField(strSection, "i16_PATH_HIST_PTRN");
            
            this.config.i20_PATH_INI /****/= INI_CONFIG.GetStringField(strSection, "i20_PATH_INI");
            this.config.i21_PATH_LOG = INI_CONFIG.GetStringField(strSection, "i21_PATH_LOG");

            // 170810 ensure folder existance
            _EnsureEccentialFolders();
        }
        private void Save_INI_Config()
        {
            string strPath = Path.Combine(Application.StartupPath, "INI", "CONFIG.INI");

            INI_CONFIG.Clear();

            string strSection = "PATH";

            INI_CONFIG.Add(strSection);
            INI_CONFIG.Add(strSection, "i01_PATH_MAIN", this.config.i01_PATH_MAIN,"path for main");
            INI_CONFIG.Add(strSection, "i02_PATH_DATA_DUMP", this.config.i02_PATH_DATA_DUMP,"path for data dump");
            INI_CONFIG.Add(strSection, "i03_PATH_RECP_BASE", this.config.i03_PATH_RECP_BASE,"path for base recp files");
            INI_CONFIG.Add(strSection, "i04_PATH_RECP_REAL", this.config.i04_PATH_RECP_REAL,"path for real recp files");

            INI_CONFIG.Add(strSection, "i10_PATH_IMG_ORG", this.config.i10_PATH_IMG_ORG, "path for original input images");
            INI_CONFIG.Add(strSection, "i11_PATH_IMG_PTRN", this.config.i11_PATH_IMG_PTRN, "path for teaching images");

            INI_CONFIG.Add(strSection, "i15_PATH_HIST_MEASURE", this.config.i15_PATH_HIST_MEASURE, "path for history of measurement");
            INI_CONFIG.Add(strSection, "i16_PATH_HIST_PTRN", this.config.i16_PATH_HIST_PTRN, "path for history of pattern matching failure");

            INI_CONFIG.Add(strSection, "i20_PATH_INI", this.config.i20_PATH_INI," path for ini files");
            INI_CONFIG.Add(strSection, "i21_PATH_LOG", this.config.i21_PATH_LOG, "path for log files");

            INI_CONFIG.Save(strPath);

            // 170810 ensure folder existance
            _EnsureEccentialFolders();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            PIC_FOCUS.Image = new Bitmap(300, 300);

            imageView1.SetInit();
            imageView2.SetInit();

            // load configuration and update 170816
            Load_INI_Config();
            imageView1.fm.config = this.config.CopyTo();


            this.Location = new System.Drawing.Point(0, 0);

            iPtrn_Init();

            //Point posPNL_DrawFigure = new Point(820, 10);
            //PNL_DRAW_FIGURE.Location = posPNL_DrawFigure;
            //PNL_DRAW_FIGURE.Visible = false;

            //************************************************************************************* 
            // Set Build Information

            UC_LOG_VIEWER.SetBasePath(config.i21_PATH_LOG);
            UC_LOG_VIEWER.WRITE_LOG("Library Initialization", DEF_OPERATION.OPER_01_POWER);
            UC_LOG_VIEWER.SetSize(655, 235);
            UC_LOG_VIEWER.WRITE_LOG("Program Initialized.", DEF_OPERATION.OPER_01_POWER);

            UC_Parameter.SetSize(380, 350);

            //*************************************************************************************
            // Make sure Eccential directory existance
            _EnsureEccentialFolders();

            //*************************************************************************************
            // Set Caminfo & Simulation Panel & other UI setting
 
            _Update_RECP_Files();
            TAB_FIGURE_SelectedIndexChanged(null, EventArgs.Empty);

            this.Text = this.Text + " :: " + GetBuildInfo();
         }
        public static string GetBuildInfo()
        {
            string strinfo = System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName;

            var parse = strinfo.Split(',');
            string filename = parse[0] + ".exe";

            string strCurPath = Application.StartupPath;
            FileInfo fi = new FileInfo(Path.Combine( strCurPath , filename));
            DateTime dt = fi.LastWriteTime;

            string[] t = { dt.Year.ToString(),                 string.Format("{0:00}", dt.Month),  string.Format("{0:00}", dt.Day  ), 
                                      string.Format("{0:00}", dt.Hour  ), string.Format("{0:00}", dt.Minute), string.Format("{0:00}", dt.Second) };

            string strDateTime = t[0] + t[1] + t[2] + "_" + t[3] + t[4] + t[5];

            return "BUILD INFORMATION : " + strDateTime;
        }
        
        private void _EnsureEccentialFolders()
        {
            WrapperFile.EnsureFolderExsistance(config.i01_PATH_MAIN);
            WrapperFile.EnsureFolderExsistance(config.i02_PATH_DATA_DUMP);
            WrapperFile.EnsureFolderExsistance(config.i03_PATH_RECP_BASE);
            WrapperFile.EnsureFolderExsistance(config.i04_PATH_RECP_REAL);

            WrapperFile.EnsureFolderExsistance(config.i10_PATH_IMG_ORG);
            WrapperFile.EnsureFolderExsistance(config.i11_PATH_IMG_PTRN);

            WrapperFile.EnsureFolderExsistance(config.i15_PATH_HIST_MEASURE);
            WrapperFile.EnsureFolderExsistance(config.i16_PATH_HIST_PTRN);

            WrapperFile.EnsureFolderExsistance(config.i20_PATH_INI);
            WrapperFile.EnsureFolderExsistance(config.i21_PATH_LOG);

        }
        private void _SetView_PTRN(string strPath_Full)
        {
            System.Threading.Thread thr = new System.Threading.Thread(delegate()
            {
                string FILE_PATH = Path.GetFileName(strPath_Full);
                string EXTENSION = Path.GetExtension(FILE_PATH).ToUpper();

                if (EXTENSION == ".BMP" && File.Exists(strPath_Full) == true)
                {
                    this.UIThread(delegate 
                    {
                        Image bmpPtrn = Bitmap.FromFile(strPath_Full);
                        PIC_PTRN.Image = new Bitmap(bmpPtrn);
                        bmpPtrn.Dispose();
                    });
                }
                else
                {
                    this.UIThread(delegate { PIC_PTRN.Image = new Bitmap(100, 100); }); 
                }
            });
            thr.IsBackground = true;
            thr.Start();
        }
        private void fuck1()
        {

        }
        private void fuck2()
        {
        }

        private void _Load_INI_DEFAULT_RECP(string strPath)
        {
            INI_LAYERS.Clear();

            INI_LAYERS.Load(strPath);

            BASE_RECP single = new BASE_RECP();

            string strSection = "PARAM";

            single.PARAM_00_BASE_RECP_NAME = strPath;
            //single.PARAM_01_LENS_INDEX = ??
            single._param_lens_index = INI_LAYERS.GetStringField(strSection, "PARAM_01_LENS_INDEX");
            //single.PARAM_02_LIGHT_INDEX = ??
            single._param_light_index = INI_LAYERS.GetStringField(strSection, "PARAM_02_LIGHT_INDEX");

            single.PARAM_03_LIGHT_VALUE = INI_LAYERS.GetIntegerField(strSection, "PARAM_03_LIGHT_VALUE");
            //single.PARAM_04_FOCUS_TYPE = ??
            single.PARAM_04_FOCUS_TYPE = INI_LAYERS.GetIntegerField(strSection, "PARAM_04_FOCUS_TYPE");

            single.PARAM_05_USE_CENTERING = INI_LAYERS.GetIntegerField(strSection, "PARAM_05_USE_CENTERING");

            single.PARAM_06_COMPEN_A = INI_LAYERS.GetDoubleField(strSection, "PARAM_06_COMPEN_A");
            single.PARAM_06_COMPEN_B = INI_LAYERS.GetDoubleField(strSection, "PARAM_06_COMPEN_B");

            //single.PARAM_09_ALGORITHM_INDEX  = ??
            single._param_algoritm_index = INI_LAYERS.GetStringField(strSection, "PARAM_07_ALGORITHM_INDEX");
            single.PARAM_08_DMG_TOLERANCE = INI_LAYERS.GetDoubleField(strSection, "PARAM_08_DMG_TOLERANCE");
            single.PARAM_09_EDGE_POSITION = INI_LAYERS.GetDoubleField(strSection, "PARAM_09_EDGE_POSITION");

        }

        private void CHK_BLEND_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_BLEND.Checked == true)
            {
                imageView1.SetLayerDisp_Status(true, imageView2.GetDisplay_Bmp());
                TB_BLENDING_RATIO.Enabled = true;
                LB_BLEND_VALUE.ForeColor = Color.Yellow;
            }
            else
            {
                imageView1.SetLayerDisp_Status(false, null);
                TB_BLENDING_RATIO.Enabled = false;
                LB_BLEND_VALUE.ForeColor = Color.Black;
            }
        }

        private void TB_BLENDING_RATIO_Scroll(object sender, EventArgs e)
        {
            int imageW1 = imageView1.VIEW_GetImageW();
            int imageH1 = imageView1.VIEW_GetImageH();
            int imageW2 = imageView2.VIEW_GetImageW();
            int imageH2 = imageView2.VIEW_GetImageH();

            if (imageW1 == imageW2 && imageH1 == imageH2)
            {
                int nValue = TB_BLENDING_RATIO.Value;
                LB_BLEND_VALUE.Text = nValue.ToString() + "%";
                imageView1.SetLayerDisp(nValue);
            }
            else
            {
                return;
            }
        }

        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do You Want To Close?", "Progrm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                wrapperCog.Dispose();
            }
            else
            {
                e.Cancel = true;

                this.Visible = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(100);

            }
        }

        bool bLV_File_Triggered = false;
        private async void LV_FILE_LIST_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (LV_FILE_LIST.FocusedItem == null) return;
            if (LV_FILE_LIST.SelectedItems.Count == 0) return;

            LV_FILE_LIST.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_FILE_LIST_SelectedIndexChanged);

            if (bLV_File_Triggered == false)
            {
                int nIndex = LV_FILE_LIST.FocusedItem.Index;
                
                string strSelected = LV_FILE_LIST.Items[nIndex].SubItems[1].Text;

                if (File.Exists(strSelected) == true)
                {
                    //imageView1.ThreadCall_LoadImage(strSelected);
                    Bitmap bmp = await staticImageLoadAsync(strSelected);
                    imageView1.SetDisplay(bmp);
                    imageView1.VIEW_Set_Clear_DispObject();
                    //imageView1.VIEW_SET_Mag_Origin();


                    RectangleF rcTemplate = new RectangleF();
                    double fMatchingRatio = 0;

                    PointF ptTemplateCenter = _DO_PTRN_And_Get_TemplatePos(out rcTemplate, out fMatchingRatio);

                    imageView1.DrawPatternMathcing(ptTemplateCenter, rcTemplate);
                    imageView1.Refresh();

                }


                bLV_File_Triggered = true;
            }
            else if (bLV_File_Triggered == true)
            {
                bLV_File_Triggered = false;
            }

            _LV_File_Coloring();
            LV_FILE_LIST.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(LV_FILE_LIST_SelectedIndexChanged);
        }
        private void _LV_File_Coloring()
        {
            if (LV_FILE_LIST.Items.Count == 0) return;

            if (LV_FILE_LIST.SelectedIndices.Count == 0) return;

            int nIndex = LV_FILE_LIST.SelectedIndices[0];
            int nSubCount = 0;

            for (int nData = 0; nData < LV_FILE_LIST.Items.Count; nData++)
            {
                if (nData == nIndex)
                {
                    nSubCount = LV_FILE_LIST.Items[nData].SubItems.Count;

                    LV_FILE_LIST.Items[nData].UseItemStyleForSubItems = false;
                    LV_FILE_LIST.Items[nData].ForeColor = Color.Red;

                    for (int i = 0; i < nSubCount; i++)
                    {
                        LV_FILE_LIST.Items[nData].SubItems[i].ForeColor = Color.Red;
                    }
                }
                else
                {
                    nSubCount = LV_FILE_LIST.Items[nData].SubItems.Count;

                    LV_FILE_LIST.Items[nData].UseItemStyleForSubItems = false;
                    LV_FILE_LIST.Items[nData].ForeColor = Color.LimeGreen;

                    for (int i = 0; i < nSubCount; i++)
                    {
                        LV_FILE_LIST.Items[nData].SubItems[i].ForeColor = Color.Black;
                    }
                }
            }
        }

        private void LV_FILE_LIST_DragDrop(object sender, DragEventArgs e)
        {
            PNL_SIMULATION_DragDrop(sender, e);

        }

        private void LV_FILE_LIST_DragEnter(object sender, DragEventArgs e)
        {
            PNL_SIMULATION_DragEnter(sender, e);

        }

        private void LV_FILE_LIST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled) return;

            e.Handled = true;
        }

        private void _ChangeRecipe(string strFileName)
        {
            // do operation
            imageView1.fm = CFigureManager.SerializedXml_Load(strFileName);


            // replace imediately latest update for path configuration 170810
            imageView1.fm.config = this.config;

            PARAM_OPTICS param_optic/**/= imageView1.fm.param_optics.CopyTo();
            param_baseRecp = imageView1.fm.baseRecp.CopyTo();

            // only for Optics information From the SEQUENCE !!! 
             _Update_RECP_Files();
             _SetColor_PtrnResul(-1);
            
            try
            {
                // item 01 : get recipe file name 
                string strPathFile_Recp = Path.GetFileName(strFileName);
                imageView1.fm.RECP_FILE = strPathFile_Recp;
                TXT_PATH_RECP_FILE.Text = strPathFile_Recp;

                // item 02 : get base recipe file name 
                TXT_BASE_RECP.Text = param_baseRecp.PARAM_00_BASE_RECP_NAME;

                // itme 03 : get pattern matching file name 
                string strPtrnFullPath = Path.Combine(imageView1.fm.config.i11_PATH_IMG_PTRN, imageView1.fm.param_ptrn.PTRN_FILE);
                string strPtrnFileName = Path.GetFileName(strPtrnFullPath);
                TXT_PATH_PTRN_FILE.Text = strPtrnFileName;

                // item 04 : get pattern matching accept ratio
                TXT_PTRN_ACC_RATIO.Text = imageView1.fm.param_ptrn.ACC_RATIO.ToString("F4");

                // item 05 : get pattern matching origian teaching position 
                TXT_PTRN_POS_ORG_X.Text = imageView1.fm.param_ptrn.RC_TEMPLATE.X.ToString("F4");
                TXT_PTRN_POS_ORG_Y.Text = imageView1.fm.param_ptrn.RC_TEMPLATE.Y.ToString("F4");

                // item 06 : get image focus region 
                RectangleF rcFocus = imageView1.fm.RC_FOCUS;

                TXT_FOCUS_POS_X.Text = rcFocus.X.ToString("N0").Replace(",", "");
                TXT_FOCUS_POS_Y.Text = rcFocus.Y.ToString("N0").Replace(",", "");

                // update ptrn view
                _SetView_PTRN(strPtrnFullPath);

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString() + System.Environment.NewLine); }

            imageView1.Refresh();

            // update information 


            //*************************************************************************************
            // WRITE LOG 
            string strRecpName = strFileName.Replace(imageView1.fm.config.i04_PATH_RECP_REAL + "\\", "");
            UC_LOG_VIEWER.WRITE_LOG(string.Format("RECP Loaded → {0}", strRecpName), DEF_OPERATION.OPER_02_RECP);

             
            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
        }


        private void BTN_RECP_SAVE_AS_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML files (*.XML)|*.xml|All files (*.*)|*.*";
            dlg.InitialDirectory = imageView1.fm.config.i04_PATH_RECP_REAL;

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            _SaveRecp(dlg.FileName);
            _Update_RECP_Files();
        }

        private void deleFunc_GetBaseRecp(BASE_RECP single)
        {
            TXT_BASE_RECP.Text = single.PARAM_00_BASE_RECP_NAME;
            imageView1.fm.baseRecp = single.CopyTo();
        }
        private void deleFunc_GetParamPtrn(PARAM_PTRN paramPtrn)
        {
            imageView1.fm.param_ptrn = paramPtrn;
            imageView1.fm.SetPtrnDelta(paramPtrn.RC_TEMPLATE.X, paramPtrn.RC_TEMPLATE.Y);

            /***/if (paramPtrn.BOOL_GLOBAL_SEARCHING == true)
            {
                // in case of global search --> set default rect
                imageView1.iSet_Roi_Ptrn(new Rectangle(0, 0, 0, 0));
            }
            else if (paramPtrn.BOOL_GLOBAL_SEARCHING == false)
            {
                // if not --> set proper rectangle
                imageView1.iSet_Roi_Ptrn(Rectangle.Round(paramPtrn.RC_SEARCH_RGN));
            }

            TXT_PATH_PTRN_FILE.Text = paramPtrn.PTRN_FILE;
            TXT_PTRN_ACC_RATIO.Text = paramPtrn.ACC_RATIO.ToString("F4");
            TXT_PTRN_POS_ORG_X.Text = paramPtrn.RC_TEMPLATE.X.ToString("F4");
            TXT_PTRN_POS_ORG_Y.Text = paramPtrn.RC_TEMPLATE.Y.ToString("F4");

            string strPathPtrn = Path.Combine(imageView1.fm.config.i11_PATH_IMG_PTRN, paramPtrn.PTRN_FILE);
            _SetView_PTRN(strPathPtrn);

        }
        private void deleFunc_ChangeRecp(string strPathRecp)
        {
            _ChangeRecipe(strPathRecp);
        }

        private void deleFunc_ChangeConfig(PARAM_CONFIG config)
        {
            // if configuration has changed??
            // then, update data in the current recipe and !!!
            // Must!!!!  Save!!! Default Configuration 
            // Also, Every Recp change has occured??  directly replace configuration .!!!! 

            imageView1.fm.config = config.CopyTo();
            this.config = config.CopyTo();
            Save_INI_Config();
        }
        private void deleFunc_ChangeTunning()
        {
            BTN_RELOAD_PARAM_FIGURES_Click(null, EventArgs.Empty);
        }
        private void BTN_RECIPE_SAVE_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Overwrite Current RECP?", "RECP Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if( TXT_PATH_RECP_FILE.Text == string.Empty )
            {
                MessageBox.Show("Invalid Recipe File Name. \n Please Select Proper Recipe Name", "Invalid Recipe File Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string strRecpFile = Path.Combine(imageView1.fm.config.i04_PATH_RECP_REAL, TXT_PATH_RECP_FILE.Text);

            _SaveRecp(strRecpFile);
            _Update_RECP_Files();
        }

        private void _SaveRecp(string strPathRecp)
        {
            string strPtrnFile = Path.Combine(imageView1.fm.config.i11_PATH_IMG_PTRN, TXT_PATH_PTRN_FILE.Text);

            if (File.Exists(strPtrnFile) == false)
            {
                MessageBox.Show("Ptrn Teaching Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            // Update Path Info ( just overwrite )

            // item 01 : get recipe file name 
            imageView1.fm.RECP_FILE = strPathRecp;

            // item 02 : get pattern mathcing file
            imageView1.fm.param_ptrn.PTRN_FILE = Path.GetFileName(strPtrnFile);

            // item 03 : get accept ratio
            if (TXT_PTRN_ACC_RATIO.Text == string.Empty) TXT_PTRN_ACC_RATIO.Text = "95.000";
            double fAcceptRatio = double.Parse(TXT_PTRN_ACC_RATIO.Text);
            TXT_PTRN_ACC_RATIO.Text = fAcceptRatio.ToString("F4");
            imageView1.fm.param_ptrn.ACC_RATIO = fAcceptRatio;


            //**********************
            CFigureManager.SerializedXml_Save(strPathRecp, imageView1.fm);

            strPathRecp = Path.GetFileName(strPathRecp);

            UC_LOG_VIEWER.WRITE_LOG("RECP Saved.", DEF_OPERATION.OPER_02_RECP);
            UC_LOG_VIEWER.WRITE_LOG("RECP : " + strPathRecp, DEF_OPERATION.OPER_02_RECP);

 
            MessageBox.Show("RECP Saved.", "Save Recp", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _ChangeRecipe(Path.Combine(imageView1.fm.config.i04_PATH_RECP_REAL, strPathRecp));

        }


        private void _SetColor_PtrnResul(int nResult)
        {
            if (nResult == -1)
            {
                BTN_PTRN_RESULT.BackColor = Color.DimGray;
            }
            else if (nResult == 0)
            {
                BTN_PTRN_RESULT.BackColor = Color.Red;
            }
            else if (nResult == 1)
            {
                BTN_PTRN_RESULT.BackColor = Color.LimeGreen;
            }
        }

        private void BTN_PTRN_MATCH_Click(object sender, EventArgs e)
        {
            if (TXT_PATH_PTRN_FILE.Text != null)
            {
                imageView1.VIEW_Set_Clear_DispObject();

                RectangleF rcTemplate = new RectangleF();
                double fMatchingRatio = 0;

                PointF ptTemplateCenter = _DO_PTRN_And_Get_TemplatePos(out rcTemplate, out fMatchingRatio);
                
                imageView1.fm.SetPtrnDelta(rcTemplate.X, rcTemplate.Y);
                imageView1.DrawPatternMathcing(ptTemplateCenter, rcTemplate);
                imageView1.Refresh();

                if (fMatchingRatio == 0)
                {
                    _SetColor_PtrnResul(0);
                }
                else
                {
                    _SetColor_PtrnResul(1);
                }
                 
                PointF ptDelta = imageView1.fm.PT_PTRN_DELTA;

                for (int i = 0; i < imageView1.fm.COUNT_PAIR_CIR; i++)
                {
                    CMeasurePairCir single = imageView1.fm.ElementAt_PairCir(i);
                    single.SetRelativeMovement(ptDelta);
                }
              
                for (int i = 0; i < imageView1.fm.COUNT_PAIR_RCT; i++)
                {
                    CMeasurePairRct single = imageView1.fm.ElementAt_PairRct(i);
                    single.SetRelativeMovement(ptDelta);
                }
                for (int i = 0; i < imageView1.fm.COUNT_PAIR_OVL; i++)
                {
                    CMeasurePairOvl single = imageView1.fm.ElementAt_PairOvl(i);
                    single.SetRelativeMovement(ptDelta);
                }

                imageView1.Refresh();

                TXT_PTRN_POS_ORG_X.Text = ptTemplateCenter.X.ToString("F4");
                TXT_PTRN_POS_ORG_Y.Text = ptTemplateCenter.Y.ToString("F4");
            }
            else { MessageBox.Show("PTRN Data Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private PointF _DO_PTRN_And_Get_TemplatePos(out RectangleF rcTemplate, out double fMatchingRatio)
        {
            PointF ptTemplateCenter = new PointF(0, 0);

            rcTemplate = new RectangleF();
            fMatchingRatio = 0;

 
            string strFile = TXT_PATH_PTRN_FILE.Text;
            string strPTRNFIle = Path.Combine(imageView1.fm.config.i11_PATH_IMG_PTRN, strFile);

            
            if (File.Exists(strPTRNFIle) == true)
            {
                Bitmap bmpPTRN = (Bitmap)Bitmap.FromFile(strPTRNFIle);
                Bitmap bmpView = (Bitmap)imageView1.GetDisplay_Bmp();
                
                

                ResultPTRN ptrn = new ResultPTRN();
 
                wrapperCog.PARAM_NUM_TO_FIND = 1;
                wrapperCog.PARAM_ACCEPT_RATIO = imageView1.fm.param_ptrn.ACC_RATIO;

                if (imageView1.fm.param_ptrn.BOOL_GLOBAL_SEARCHING == true)
                {
                    wrapperCog.PARAM_RC_SEARCH = new RectangleF(0, 0, bmpView.Width, bmpView.Height);
                }
                else if (imageView1.fm.param_ptrn.BOOL_GLOBAL_SEARCHING == false)
                {
                    wrapperCog.PARAM_RC_SEARCH = imageView1.fm.param_ptrn.RC_SEARCH_RGN;
                }

                wrapperCog.PARAM_PT_RELATIVE_ORIGIN = new PointF(0, 0);

                wrapperCog.Do_Ptrn(bmpView, bmpPTRN);

                ptrn = wrapperCog.ptrnResult;
                
                bmpPTRN.Dispose();
                bmpView.Dispose();

                if (ptrn.Count >= 1)
                {
                    ptTemplateCenter = ptrn.resultList.ElementAt(0).ptCenter;
                    rcTemplate = ptrn.resultList.ElementAt(0).rc;
                    fMatchingRatio = ptrn.resultList.ElementAt(0).fScore * 100.0;
                    _PRINT_MSG("MR = " + fMatchingRatio.ToString("F4") + "%");
                }
                else
                {
                    _PRINT_MSG("PTRN MATCHING Failed.");

                }
            }
            else { _PRINT_MSG("PTRN File Not Found."); }

            return ptTemplateCenter;
        }
        private void _PRINT_MSG(string message)
        {
            this.UIThread(delegate
            {
                string time = WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM();

                string line = time + " : " + message + System.Environment.NewLine;
                msg.AppendText(line);
                msg.ScrollToCaret();
            });
        }

        private void PNL_SIMULATION_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

        }

        private void PNL_SIMULATION_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            int nFileCount = files.Length;


            FileAttributes attr = File.GetAttributes(files[0]);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                files = Directory.GetFiles(files[0]);
                nFileCount = files.Length;
            }


            LV_FILE_LIST.Items.Clear();

            for (int i = 0; i < nFileCount; i++)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = string.Format("{0}", i + 1);
                lvi.SubItems.Add(files[i]);

                LV_FILE_LIST.Items.Add(lvi);
            }

            UC_LOG_VIEWER.WRITE_LOG("Image Drop", DEF_OPERATION.OPER_04_IMAGE);
            UC_LOG_VIEWER.WRITE_LOG(string.Format("Total {0} Files Found.", nFileCount), DEF_OPERATION.OPER_04_IMAGE);
        }

        private void BTN_UPDATE_FIGURE_LIST_Click(object sender, EventArgs e)
        {
            CFigureManager fm = imageView1.fm.Clone() as CFigureManager;

            LV_PAIR_DIG.Items.Clear();
            LV_PAIR_CIR.Items.Clear();
            LV_PAIR_OVL.Items.Clear();

            //*************************************************************************************
            // Digonal rect pair
            //*************************************************************************************
            for (int nIndex = 0; nIndex < fm.COUNT_PAIR_RCT; nIndex++)
            {
                CMeasurePairRct single = fm.ElementAt_PairRct(nIndex);

                ListViewItem lvi = new ListViewItem(); lvi.Text = nIndex.ToString("N0");
                lvi.SubItems.Add(string.Format("{0}", single.NICKNAME));

                LV_PAIR_DIG.Items.Add(lvi);
            }
            
            //*************************************************************************************
            // circle
            //*************************************************************************************
            for (int nIndex = 0; nIndex < fm.COUNT_PAIR_CIR; nIndex++)
            {
                CMeasurePairCir single = fm.ElementAt_PairCir(nIndex);

                ListViewItem lvi = new ListViewItem(); lvi.Text = nIndex.ToString("N0");
                lvi.SubItems.Add(string.Format("{0}", single.NICKNAME));

                LV_PAIR_CIR.Items.Add(lvi);
            }
            //*************************************************************************************
            // OverLay
            //*************************************************************************************
            for (int nIndex = 0; nIndex < fm.COUNT_PAIR_OVL; nIndex++)
            {
                CMeasurePairOvl single = fm.ElementAt_PairOvl(nIndex);

                ListViewItem lvi = new ListViewItem(); lvi.Text = nIndex.ToString("N0");
                lvi.SubItems.Add(string.Format("{0}", single.NICKNAME));

                LV_PAIR_OVL.Items.Add(lvi);
            }

            imageView1.Refresh();
            BTN_RELOAD_PARAM_FIGURES_Click(null, EventArgs.Empty);
        }

        private void BTN_RELOAD_PARAM_FIGURES_Click(object sender, EventArgs e)
        {
            UC_Parameter.ClearData();
            LV_PARAMETER.Items.Clear();

            CFigureManager fmCopy = imageView1.fm;

            LV_PARAMETER.BeginUpdate();

            {
                //@@@ IMPORTANT 
                //@@@ PARSER EQUALS =====> -  not underbar it is hipen;

                // for digonal rect pair
                for (int i = 0; i < fmCopy.COUNT_PAIR_RCT; i++)
                {
                    string strType = "RECT"; 

                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strType + "-" + (i+1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }

                // for circle 
                for (int i = 0; i < fmCopy.COUNT_PAIR_CIR; i++)
                {
                    string strType = "CIRCLE";

                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strType + "-" + (i+1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }
                // for overlay
                for (int i = 0; i < fmCopy.COUNT_PAIR_OVL; i++)
                {
                    string strTye = "OVERLAY";

                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strTye + "-" + (i+1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }
            }

            LV_PARAMETER.EndUpdate();
        }

        private void BTN_DIG_ANGLE_UP_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_DIG.FocusedItem == null) return;

            int nAngleTemp = Convert.ToInt32(TXT_PARAM_DIA_ANGLE.Text);
            int nAngleAccu = +1;

            TXT_PARAM_DIA_ANGLE.Text = (nAngleTemp + nAngleAccu).ToString("N0");

            int nDataIndex = LV_PAIR_DIG.FocusedItem.Index;

            imageView1.iMod_RectPair_DigonalAngle(nDataIndex, nAngleAccu);
        }

        private void BTN_DIG_ANGLE_RV_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_DIG.FocusedItem == null) return;

            int nAngleTemp = Convert.ToInt32(TXT_PARAM_DIA_ANGLE.Text);
            int nAngleAccu = -(nAngleTemp * 2);

            TXT_PARAM_DIA_ANGLE.Text = (nAngleTemp + nAngleAccu).ToString("N0");

            int nDataIndex = LV_PAIR_DIG.FocusedItem.Index;

            imageView1.iMod_RectPair_DigonalAngle(nDataIndex, nAngleAccu);
        }

        private void BTN_DIG_ANGLE_DW_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_DIG.FocusedItem == null) return;

            int nAngleTemp = Convert.ToInt32(TXT_PARAM_DIA_ANGLE.Text);
            int nAngleAccu = -1;

            TXT_PARAM_DIA_ANGLE.Text = (nAngleTemp + nAngleAccu).ToString("N0");

            int nDataIndex = LV_PAIR_DIG.FocusedItem.Index;

            imageView1.iMod_RectPair_DigonalAngle(nDataIndex, nAngleAccu);
        }

        private void BTN_DIG_ADD_Click(object sender, EventArgs e)
        {
            if (imageView1.BOOL_TEACHING_ACTIVATION == false) return;

            imageView1.VIEW_Set_Overlay(true);
            CMeasurePairRct single = new CMeasurePairRct();

            PointF ptDraw = imageView1.PT_FIGURE_TO_DRAW;

            if (RDO_TYPE_HOR.Checked == true)
            {
                single.RC_TYPE = IFX_RECT_TYPE.DIR_HOR;
                single.rc_FST = new parseRect(ptDraw.X, ptDraw.Y, 100, 30);
                single.rc_SCD = new parseRect(ptDraw.X, ptDraw.Y + 50, 100, 30);
            }
            else if (RDO_TYPE_VER.Checked == true)
            {
                single.RC_TYPE = IFX_RECT_TYPE.DIR_VER;
                single.rc_FST = new parseRect(ptDraw.X + 00, ptDraw.Y, 30, 100);
                single.rc_SCD = new parseRect(ptDraw.X + 50, ptDraw.Y, 30, 100);
            }
            else if (RDO_TYPE_DIA.Checked == true)
            {
                single.RC_TYPE = IFX_RECT_TYPE.DIR_DIA;
                single.rc_FST = new parseRect(ptDraw.X + 00, ptDraw.Y, 30, 100);
                single.rc_SCD = new parseRect(ptDraw.X + 50, ptDraw.Y, 30, 100);

            }

            single.CroodinateBackup();

            BASE_RECP baseRecp = imageView1.fm.baseRecp;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_RCT);
            single.NICKNAME = "RC_" + nLastIndex.ToString("N0");

            single.param_06_edge_position_fst = baseRecp.PARAM_09_EDGE_POSITION;
            single.param_07_edge_position_scd = baseRecp.PARAM_09_EDGE_POSITION;

            single.param_comm_01_compen_A = baseRecp.PARAM_06_COMPEN_A;
            single.param_comm_02_compen_B = baseRecp.PARAM_06_COMPEN_B;
 
            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG(string.Format("{0} Added.", IFX_FIGURE.ToStringType(single.RC_TYPE)), DEF_OPERATION.OPER_03_PARAM);

            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
 
            //-------------------------------------------------------------------------------------
            // Paramter Update on the UI

            TXT_RCD_W.Text = single.rc_FST.Width.ToString();
            TXT_RCD_H.Text = single.rc_FST.Height.ToString();

            _LV_File_Coloring();
            RDO_TYPE_HOR.Checked = true;
        }

        private void BTN_DIG_COPY_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_DIG.FocusedItem == null) return;
            int nIndex = LV_PAIR_DIG.FocusedItem.Index;

            CMeasurePairRct temp = (CMeasurePairRct)imageView1.fm.ElementAt_PairRct(nIndex);
            CMeasurePairRct single = temp.CopyTo();

            // update last index
            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_RCT);
            single.NICKNAME = "RC_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);

            UC_LOG_VIEWER.WRITE_LOG("RECT Copied.", DEF_OPERATION.OPER_03_PARAM);

            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
            RDO_TYPE_HOR.Checked = true;
        }

        private void BTN_DIG_MODIFY_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_DIG.FocusedItem == null) return;
            int nIndex = LV_PAIR_DIG.FocusedItem.Index;

            //// read  for latest roi croodinates
            CMeasurePairRct single = (CMeasurePairRct)imageView1.fm.ElementAt_PairRct(nIndex);

            //-------------------------------------------
            // Change Nick Name
            single.NICKNAME = TXT_PARAM_DIG_NICK.Text;

            //-------------------------------------------
            // Change Rectangle type
            int nRC_TypePrev = single.RC_TYPE; //for default 
            int nRC_TypeCurr = single.RC_TYPE; //for new 

            /***/if (RDO_TYPE_HOR.Checked == true) nRC_TypeCurr = IFX_RECT_TYPE.DIR_HOR;
            else if (RDO_TYPE_VER.Checked == true) nRC_TypeCurr = IFX_RECT_TYPE.DIR_VER;
            else if (RDO_TYPE_DIA.Checked == true) nRC_TypeCurr = IFX_RECT_TYPE.DIR_DIA;

            single.RC_TYPE = nRC_TypeCurr;
            single.ConvertRectangleType(nRC_TypePrev, nRC_TypeCurr);

            //-------------------------------------------
            // replace !! updated data
            CMeasurePairRct[] array = imageView1.fm.ToArray_PairRct();
            array[nIndex] = (CMeasurePairRct)single;
            imageView1.fm.Figure_Replace(array);

            TXT_PARAM_DIA_ANGLE.Text = single.ANGLE.ToString();
            //-------------------------------------------
            // Write log 
            UC_LOG_VIEWER.WRITE_LOG("RECT Modified.", DEF_OPERATION.OPER_03_PARAM);
            MessageBox.Show("Data Modification has finished.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
         }

        private void BTN_DIG_REMOVE_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_DIG.FocusedItem != null)
            {
                int nIndex = LV_PAIR_DIG.FocusedItem.Index;
                imageView1.fm.Figure_Remove(IFX_FIGURE.PAIR_RCT, nIndex);
                UC_LOG_VIEWER.WRITE_LOG("FIG-RECT Removed.", DEF_OPERATION.OPER_03_PARAM);

                BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
                RDO_TYPE_HOR.Checked = true;

            }
        }
        private void _LV_PAIR_DIG_Coloring()
        {
            if (LV_PAIR_DIG.Items.Count == 0) return;

            if (LV_PAIR_DIG.SelectedIndices.Count == 0) return;

            int nIndex = LV_PAIR_DIG.SelectedIndices[0];
            int nSubCount = 0;

            for (int nData = 0; nData < LV_PAIR_DIG.Items.Count; nData++)
            {
                if (nData == nIndex)
                {
                    nSubCount = LV_PAIR_DIG.Items[nData].SubItems.Count;

                    LV_PAIR_DIG.Items[nData].UseItemStyleForSubItems = false;
                    LV_PAIR_DIG.Items[nData].ForeColor = Color.Red;

                    for (int i = 0; i < nSubCount; i++)
                    {
                        LV_PAIR_DIG.Items[nData].SubItems[i].ForeColor = Color.Red;
                    }
                }
                else
                {
                    nSubCount = LV_PAIR_DIG.Items[nData].SubItems.Count;

                    LV_PAIR_DIG.Items[nData].UseItemStyleForSubItems = false;
                    LV_PAIR_DIG.Items[nData].ForeColor = Color.LimeGreen;

                    for (int i = 0; i < nSubCount; i++)
                    {
                        LV_PAIR_DIG.Items[nData].SubItems[i].ForeColor = Color.LimeGreen;
                    }
                }
            }
        }
        private void _LV_RECP_Coloring()
        {
            if (LV_RECP.Items.Count == 0) return;

            string strCurrentRecpFile = TXT_PATH_RECP_FILE.Text;
            
            int nSubCount = 0;

             for (int nData = 0; nData < LV_RECP.Items.Count; nData++)
            {
                string strRecpFile = LV_RECP.Items[nData].SubItems[1].Text;

                if (strRecpFile == strCurrentRecpFile)
                {
                    nSubCount = LV_RECP.Items[nData].SubItems.Count;

                    LV_RECP.Items[nData].UseItemStyleForSubItems = false;
                    LV_RECP.Items[nData].ForeColor = Color.Red;

                    for (int i = 0; i < nSubCount; i++)
                    {
                        LV_RECP.Items[nData].SubItems[i].ForeColor = Color.Red;
                    }
                }
                else
                {
                    LV_RECP.Items[nData].UseItemStyleForSubItems = true;
                    LV_RECP.Items[nData].ForeColor = Color.Black;
                    for (int i = 0; i < nSubCount; i++)
                    {
                        LV_RECP.Items[nData].SubItems[i].ForeColor = Color.Black;
                    }
                }
                
            }
        }
        private void _Update_RECP_Files()
        {
            //************************************************************************************
            // Get PTRN Files

            string[] arrRecpFiles = Directory.GetFiles(imageView1.fm.config.i04_PATH_RECP_REAL, "*.xml");

            LV_RECP.Items.Clear();

            LV_RECP.BeginUpdate();

            for (int i = 0; i < arrRecpFiles.Length; i++)
            {
                string strFile = Path.GetFileName(arrRecpFiles.ElementAt(i));

                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString("N0");
                lvi.SubItems.Add(strFile);

                LV_RECP.Items.Add(lvi);
            }

            LV_RECP.EndUpdate();
            _LV_RECP_Coloring();
        }
        private void BTN_CIR_ADD_Click(object sender, EventArgs e)
        {
            if (imageView1.BOOL_TEACHING_ACTIVATION == false) return;

            imageView1.VIEW_Set_Overlay(true);
            CMeasurePairCir single = new CMeasurePairCir();

            PointF ptDraw = imageView1.PT_FIGURE_TO_DRAW;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_CIR);
            single.NICKNAME = "CC_" + nLastIndex.ToString("N0");

            single.rc_EX = new RectangleF(ptDraw.X - 50, ptDraw.Y - 50, 100, 100);
            single.rc_IN = new RectangleF(ptDraw.X-3, ptDraw.Y-3, 6, 6);
            single.rc_IN = CRect.SetCenter(single.rc_IN, single.rc_EX);

            single.CroodinateBackup();

            BASE_RECP baseRecp = imageView1.fm.baseRecp;

            single.param_comm_01_compen_A = baseRecp.PARAM_06_COMPEN_A;
            single.param_comm_02_compen_B = baseRecp.PARAM_06_COMPEN_B;
            single.param_06_EdgePos = baseRecp.PARAM_09_EDGE_POSITION;


            single.param_01_DMG_Tol= baseRecp.PARAM_08_DMG_TOLERANCE;

            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG("FIG-CIR Added.", DEF_OPERATION.OPER_03_PARAM);

            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
        }

        private void BTN_CIR_COPY_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_CIR.FocusedItem == null) return;
            int nIndex = LV_PAIR_CIR.FocusedItem.Index;

            CMeasurePairCir temp = (CMeasurePairCir)imageView1.fm.ElementAt_PairCir(nIndex);
            CMeasurePairCir single = temp.CopyTo();

            // update last index
            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_CIR);
            single.NICKNAME = "CC_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG("FIG-CIR Copied.", DEF_OPERATION.OPER_03_PARAM);

            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
        }

        private void BTN_CIR_MODIFY_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_CIR.FocusedItem == null) return;
            int nIndex = LV_PAIR_CIR.FocusedItem.Index;

            //// read  for latest roi croodinates
            CMeasurePairCir single = (CMeasurePairCir)imageView1.fm.ElementAt_PairCir(nIndex);
            single.NICKNAME = TXT_PARAM_CIR_NICK.Text;

            CMeasurePairCir[] array = imageView1.fm.ToArray_PairCir();
            array[nIndex] = (CMeasurePairCir)single;
            imageView1.fm.Figure_Replace(array);

            UC_LOG_VIEWER.WRITE_LOG("FIG-CIR Modified.", DEF_OPERATION.OPER_03_PARAM);
            MessageBox.Show("Data Modification has finished.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
        }

        private void BTN_CIR_REMOVE_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_CIR.FocusedItem != null)
            {
                int nIndex = LV_PAIR_CIR.FocusedItem.Index;
                imageView1.fm.Figure_Remove(IFX_FIGURE.PAIR_CIR, nIndex);
                UC_LOG_VIEWER.WRITE_LOG("FIG-CIR Removed.", DEF_OPERATION.OPER_03_PARAM);

                BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
            }
        }

        private void BTN_OL_ADD_Click(object sender, EventArgs e)
        {
            if (imageView1.BOOL_TEACHING_ACTIVATION == false) return;

            imageView1.VIEW_Set_Overlay(true);
            CMeasurePairOvl single = new CMeasurePairOvl();

            PointF ptDraw = imageView1.PT_FIGURE_TO_DRAW;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_OVL);
            single.NICKNAME = "OL_" + nLastIndex.ToString("N0");

            single.RC_HOR_IN.rc_FST = new parseRect(ptDraw.X - 150, ptDraw.Y - 50, 50, 100);
            single.RC_HOR_IN.rc_SCD = new parseRect(ptDraw.X + 100, ptDraw.Y - 50, 50, 100);

            single.RC_HOR_EX.rc_FST = new parseRect(ptDraw.X - 300, ptDraw.Y - 50, 50, 100);
            single.RC_HOR_EX.rc_SCD = new parseRect(ptDraw.X + 250, ptDraw.Y - 50, 50, 100);

            single.RC_VER_IN.rc_FST = new parseRect(ptDraw.X - 050, ptDraw.Y - 150, 100, 50);
            single.RC_VER_IN.rc_SCD = new parseRect(ptDraw.X - 050, ptDraw.Y + 100, 100, 50);

            single.RC_VER_EX.rc_FST = new parseRect(ptDraw.X - 050, ptDraw.Y - 300, 100, 50);
            single.RC_VER_EX.rc_SCD = new parseRect(ptDraw.X - 050, ptDraw.Y + 250, 100, 50);


            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG("FIG-OVL Added.", DEF_OPERATION.OPER_03_PARAM);

            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
        }

        private void BTN_OL_COPY_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_OVL.FocusedItem == null) return;
            int nIndex = LV_PAIR_OVL.FocusedItem.Index;

            CMeasurePairOvl temp = (CMeasurePairOvl)imageView1.fm.ElementAt_PairOvl(nIndex);
            CMeasurePairOvl single = temp.CopyTo();

            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG("FIG-OVL Copied.", DEF_OPERATION.OPER_03_PARAM);

            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
       
        }

        private void BTN_OL_REMOVE_Click(object sender, EventArgs e)
        {
            if (LV_PAIR_OVL.FocusedItem != null)
            {
                int nIndex = LV_PAIR_OVL.FocusedItem.Index;
                imageView1.fm.Figure_Remove(IFX_FIGURE.PAIR_OVL, nIndex);
                UC_LOG_VIEWER.WRITE_LOG("FIG-OVL Removed.", DEF_OPERATION.OPER_03_PARAM);

                BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
            }
        }

        private void BTN_DRAW_FOCUS_ROI_Click(object sender, EventArgs e)
        {
            imageView1.iRemove_Roi_Focus();
            imageView1.iDrawFocus(true);
            imageView1.Refresh();

        }
        
        private void BTN_SET_FOCUS_ROI_Click(object sender, EventArgs e)
        {
            Rectangle rc = imageView1.iGet_Roi_Focus();

            imageView1.fm.RC_FOCUS = rc;
            imageView1.fm.CroodinateBackckupFocusRect();
 
            if (rc.X > 0 && rc.Y > 0 && rc.Width > 0 && rc.Height > 0)
            {
                TXT_FOCUS_POS_X.Text = rc.X.ToString("N0").Replace(",", "");
                TXT_FOCUS_POS_Y.Text = rc.Y.ToString("N0").Replace(",", "");
            }

            imageView1.iRemove_Roi_Focus();
            imageView1.Refresh();
        }


        private void BTN_REMOVE_FOCUS_ROI_Click(object sender, EventArgs e)
        {
            imageView1.iRemove_Roi_Focus();
            imageView1.Refresh();
        }



        bool bLV_PAIR_DIG_Triggered = false;
        private void LV_PAIR_DIG_SelectedIndexChanged(object sender, EventArgs e)
        {
            LV_PAIR_DIG.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_PAIR_DIG_SelectedIndexChanged);

            // initialization
            if (bLV_PAIR_DIG_Triggered == false)
            {
                if (LV_PAIR_DIG.FocusedItem != null)
                {
                    int nIndex = LV_PAIR_DIG.FocusedItem.Index;

                    _UpdateFigureVariation(nIndex);

                    // value assignment
                    CFigureManager fm = imageView1.iGet_AllData();
                    CMeasurePairRct single = fm.ElementAt_PairRct(nIndex);

                    TXT_PARAM_DIG_NICK.Text = single.NICKNAME;
                    TXT_PARAM_DIA_ANGLE.Text = single.ANGLE.ToString("N0");

                    /***/if (single.RC_TYPE == IFX_RECT_TYPE.DIR_HOR) RDO_TYPE_HOR.Checked = true;
                    else if (single.RC_TYPE == IFX_RECT_TYPE.DIR_VER) RDO_TYPE_VER.Checked = true;
                    else if (single.RC_TYPE == IFX_RECT_TYPE.DIR_DIA) RDO_TYPE_DIA.Checked = true;

                    bLV_PAIR_DIG_Triggered = true;
                }
            }
            else if (bLV_PAIR_DIG_Triggered == true)
            {
                bLV_PAIR_DIG_Triggered = false;
            }

            LV_PAIR_DIG.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(LV_PAIR_DIG_SelectedIndexChanged);
        }

        private void LV_PAIR_CIR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LV_PAIR_CIR.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_PAIR_CIR_SelectedIndexChanged);

            if (LV_PAIR_CIR.FocusedItem != null)
            {
                int nIndex = LV_PAIR_CIR.FocusedItem.Index;
                _UpdateFigureVariation(nIndex);

                CFigureManager fm = imageView1.iGet_AllData();
                CMeasurePairCir single = fm.ElementAt_PairCir(nIndex);

                TXT_PARAM_CIR_NICK.Text = single.NICKNAME;
            }
            LV_PAIR_CIR.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(LV_PAIR_CIR_SelectedIndexChanged);
        }
        private void LV_PAIR_OVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LV_PAIR_OVL.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_PAIR_OVL_SelectedIndexChanged);

            if (LV_PAIR_OVL.FocusedItem != null)
            {

                int nIndex = LV_PAIR_OVL.FocusedItem.Index;
                _UpdateFigureVariation(nIndex);
            }
            LV_PAIR_OVL.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(LV_PAIR_OVL_SelectedIndexChanged);
        }
        private void _UpdateFigureVariation(int nDataIndex)
        {
            CFigureManager fm = imageView1.iGet_AllData();

            //********************************************************************
            // select figure type

            int nFigureIndex = TAB_FIGURE.SelectedIndex;

            const int FIGURE_RCT = 0;
            const int FIGURE_CIR = 1;
            const int FIGURE_OVL = 2;

            // Diagonal 에서 모든 렉트를 처리하는 방식으로 바껴서... 강제변환 170524

            /***/if (nFigureIndex == FIGURE_RCT) nFigureIndex = IFX_FIGURE.PAIR_RCT;
            else if (nFigureIndex == FIGURE_CIR) nFigureIndex = IFX_FIGURE.PAIR_CIR;
            else if (nFigureIndex == FIGURE_OVL) nFigureIndex = IFX_FIGURE.PAIR_OVL;

            if (nFigureIndex == IFX_FIGURE.PAIR_RCT)
            {
                CMeasurePairRct single = fm.ElementAt_PairRct(nDataIndex);
                TXT_RCD_W.Text = single.rc_FST.Width.ToString("N0");
                TXT_RCD_H.Text = single.rc_FST.Height.ToString("N0");
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_CIR)
            {
                CMeasurePairCir single = fm.ElementAt_PairCir(nDataIndex);
                TXT_CIRCLE_W.Text = single.rc_EX.Width.ToString("N0");
                TXT_CIRCLE_H.Text = single.rc_EX.Height.ToString("N0");
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_OVL)
            {
                CMeasurePairOvl single = fm.ElementAt_PairOvl(nDataIndex);
                TXT_OVL_IN_W.Text = single.RC_VER_IN.rc_FST.Width.ToString("N0");
                TXT_OVL_IN_H.Text = single.RC_VER_IN.rc_FST.Height.ToString("N0");

                TXT_OVL_EX_W.Text = single.RC_VER_EX.rc_FST.Width.ToString("N0");
                TXT_OVL_EX_H.Text = single.RC_VER_EX.rc_FST.Height.ToString("N0");
            }
        }
        

        private void LV_PARAMETER_SelectedIndexChanged(object sender, EventArgs e)
        {
            LV_PARAMETER.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_PARAMETER_SelectedIndexChanged);

            if (LV_PARAMETER.FocusedItem == null) return;

            int nIndex = LV_PARAMETER.FocusedItem.Index;


            string strSelected = LV_PARAMETER.Items[nIndex].SubItems[1].Text;

            string[] parse = strSelected.Split('-');

            string strHeader = parse[0];
            int nItemIndex = Convert.ToInt32(parse[1]) - 1;

            if (nItemIndex == -1)
            {
                MessageBox.Show("Invalid Index Underflow.");
                return;
            }
            //*****************************************************************************************
            // value assignment

            CFigureManager fmCopy = imageView1.fm;

            if (strHeader == "RECT")
            {
                CMeasurePairRct single = fmCopy.ElementAt_PairRct(nItemIndex);

                PROPERTY_PairRct propertySingle = new PROPERTY_PairRct();

                propertySingle.FromFigure(single);

                UC_Parameter.SetParam_RC_PAIR_DIG(propertySingle);
            }
            else if (strHeader == "CIRCLE")
            {
                CMeasurePairCir single = fmCopy.ElementAt_PairCir(nItemIndex);

                PROPERTY_PairCir propertySingle = new PROPERTY_PairCir();

                propertySingle.FromFigure(single);
                UC_Parameter.SetParam_Circle(propertySingle);
            }
            else if (strHeader == "OVERLAY")
            {
                CMeasurePairOvl single = fmCopy.ElementAt_PairOvl(nItemIndex);

                PROPERTY_PairOvl propertySingle = new PROPERTY_PairOvl();

                propertySingle.FromFigure(single);
                UC_Parameter.SetParam_Overlay(propertySingle);
            }

            LV_PAIR_DIG.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(LV_PARAMETER_SelectedIndexChanged);
        }

        private void TAB_FIGURE_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTN_RELOAD_PARAM_FIGURES_Click(null, EventArgs.Empty);

            int nFigureIndex = TAB_FIGURE.SelectedIndex;

            // set visibility : according to tab selection only for OVL
            RDO_ROI_ASYM.Visible = nFigureIndex == FIGURE_OVL ? true : false;
        }

        private void LV_Figure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled) return;

            int nFigureIndex = TAB_FIGURE.SelectedIndex;
            int nDataIndex = IFX_RECT_TYPE.DIR_HOR; // just set defalut value 

            //********************************************************************
            // Invalid Focus exception


            /***/if (nFigureIndex == FIGURE_RCT && LV_PAIR_DIG.Focused == false) { return; }
            else if (nFigureIndex == FIGURE_CIR && LV_PAIR_CIR.Focused == false) { return; }
            else if (nFigureIndex == FIGURE_OVL && LV_PAIR_OVL.Focused == false) { return; }

            int tx = TB_FIGURE_CTRL_SCALE.Value;
            int ty = TB_FIGURE_CTRL_SCALE.Value; ;

            /***/if (nFigureIndex == FIGURE_RCT) { if (LV_PAIR_DIG.FocusedItem == null) return; nDataIndex = LV_PAIR_DIG.FocusedItem.Index; }
            else if (nFigureIndex == FIGURE_CIR) { if (LV_PAIR_CIR.FocusedItem == null) return; nDataIndex = LV_PAIR_CIR.FocusedItem.Index; }
            else if (nFigureIndex == FIGURE_OVL) { if (LV_PAIR_OVL.FocusedItem == null) return; nDataIndex = LV_PAIR_OVL.FocusedItem.Index; }


            // Diagonal 에서 모든 렉트를 처리하는 방식으로 바껴서... 강제변환 170524

            /***/if (nFigureIndex == FIGURE_RCT) nFigureIndex = IFX_FIGURE.PAIR_RCT;
            else if (nFigureIndex == FIGURE_CIR) nFigureIndex = IFX_FIGURE.PAIR_CIR;
            else if (nFigureIndex == FIGURE_OVL) nFigureIndex = IFX_FIGURE.PAIR_OVL;

            //********************************************************************               
            //apply directional operation                                                        
            int m_nAction = 0;

            /***/if (RDO_ROI_POSITION.Checked == true) m_nAction = IFX_ADJ_ACTION.POS;
            else if (RDO_ROI_GAP.Checked /***/== true) m_nAction = IFX_ADJ_ACTION.GAP;
            else if (RDO_ROI_SIZE.Checked /**/== true) m_nAction = IFX_ADJ_ACTION.SIZE;
            else if (RDO_ROI_ASYM.Checked /**/== true) m_nAction = IFX_ADJ_ACTION.ASYM;


            //************************************************************************************************
            // 01 Mode control 
            //************************************************************************************************
            if (e.KeyCode == Keys.P || e.KeyCode == Keys.G || e.KeyCode == Keys.S || e.KeyCode == Keys.Z)
            {
                string mode = string.Empty;

                /***/if (e.KeyCode == Keys.P) { RDO_ROI_POSITION.Checked = true; mode = "POSITION ADJUST"; }
                else if (e.KeyCode == Keys.G) { RDO_ROI_GAP.Checked /***/= true; mode = "GAP ADJUST"; }
                else if (e.KeyCode == Keys.S) { RDO_ROI_SIZE.Checked /**/= true; mode = "SIZE ADJUST"; }
                else if (e.KeyCode == Keys.Z) { RDO_ROI_ASYM.Checked/**/ = true; mode = "ZIG ZAG ADJUST"; }
                MessageBox.Show("MODE CHANGE : " + mode, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //************************************************************************************************
            // 02 Angle Control for Diagonal 
            //************************************************************************************************
            else if (e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
            {
                if (RDO_TYPE_DIA.Checked == true)
                {
                    /***/if (e.KeyCode == Keys.PageUp) BTN_DIG_ANGLE_UP_Click(null, EventArgs.Empty);
                    else if (e.KeyCode == Keys.PageDown) BTN_DIG_ANGLE_DW_Click(null, EventArgs.Empty);
                }
                else
                {
                    // Nothing To Do : Other figures do not need any rotation.
                }
            }
            //************************************************************************************************
            // Scale Control 
            //************************************************************************************************
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Subtract)
            {
                /***/if (e.KeyCode == Keys.Add/***/) { _ChangeFigure_AdjustLevel(+1); }
                else if (e.KeyCode == Keys.Subtract) { _ChangeFigure_AdjustLevel(-1); }
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.OemPeriod)
            {
                /***/if (e.KeyCode == Keys.OemPeriod/***/) { _ChangeFigure_AdjustLevel(+1); }
                else if (e.KeyCode == Keys.Oemcomma/****/) { _ChangeFigure_AdjustLevel(-1); }
            }
            else
            {
                if (nFigureIndex == IFX_FIGURE.PAIR_OVL) // only for overlay figure
                {
                    int nTarget = RDO_ROI_OVL_IN.Checked == true ? 0 : RDO_ROI_OVL_EX.Checked == true ? 1 : -1;

                    int nDir = 0;

                    /***/if (e.KeyCode == Keys.Left)/****/nDir = IFX_DIR.DIR_LFT;
                    else if (e.KeyCode == Keys.Up)/******/nDir = IFX_DIR.DIR_TOP;
                    else if (e.KeyCode == Keys.Right)/***/nDir = IFX_DIR.DIR_RHT;
                    else if (e.KeyCode == Keys.Down)/****/nDir = IFX_DIR.DIR_BTM;

                    if (nTarget != -1) // 0 or 1 Special target 
                    {
                        imageView1.iAdj_Overlay(m_nAction, nTarget, nDataIndex, nDir, TB_FIGURE_CTRL_SCALE.Value);
                    }
                    else if (nTarget == -1) // NON SELECTION : ENTIARE TARGET
                    {
                        imageView1.iAdj_Overlay(m_nAction, 0, nDataIndex, nDir, TB_FIGURE_CTRL_SCALE.Value);
                        imageView1.iAdj_Overlay(m_nAction, 1, nDataIndex, nDir, TB_FIGURE_CTRL_SCALE.Value);
                    }
                }
                //************************************************************************************************
                // Position Control 
                //************************************************************************************************
                else // for common figure 
                {
                    if (e.KeyCode == Keys.Left)
                    {
                        // 가독성을 위해서.. "if 문" <--- 
                        /***/if (m_nAction == IFX_ADJ_ACTION.POS/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.POS, -tx, 0);
                        else if (m_nAction == IFX_ADJ_ACTION.GAP/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.GAP, -tx, 0);
                        else if (m_nAction == IFX_ADJ_ACTION.SIZE/*****/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.SIZE, -tx, 0);
                    }
                    else if (e.KeyCode == Keys.Right)
                    {
                        /***/if (m_nAction == IFX_ADJ_ACTION.POS/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.POS, tx, 0);
                        else if (m_nAction == IFX_ADJ_ACTION.GAP/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.GAP, tx, 0);
                        else if (m_nAction == IFX_ADJ_ACTION.SIZE/*****/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.SIZE, tx, 0);
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        /***/if (m_nAction == IFX_ADJ_ACTION.POS/******/) { imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.POS, 0, -ty); }
                        else if (m_nAction == IFX_ADJ_ACTION.GAP/******/) { imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.GAP, 0, -ty); }
                        else if (m_nAction == IFX_ADJ_ACTION.SIZE/*****/) { imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.SIZE, 0, -ty); }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        /***/if (m_nAction == IFX_ADJ_ACTION.POS/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.POS, 0, ty);
                        else if (m_nAction == IFX_ADJ_ACTION.GAP/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.GAP, 0, ty);
                        else if (m_nAction == IFX_ADJ_ACTION.SIZE/*****/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.SIZE, 0, ty);
                    }
                }
                _UpdateFigureVariation(nDataIndex);
            }

            BTN_RELOAD_PARAM_FIGURES_Click(null, EventArgs.Empty);
            // to prevent envet duplication
            e.Handled = true;
        }

        private void _ChangeFigure_AdjustLevel(int sign)
        {
            int nValue = TB_FIGURE_CTRL_SCALE.Value;

            /***/if (sign > 0x0) { nValue++; }
            else if (sign < 0x0) { nValue--; }

            nValue = nValue > 0 ? nValue : 1;
            TB_FIGURE_CTRL_SCALE.Value = nValue;
            TB_FIGURE_CTRL_SCALE_Scroll(null, EventArgs.Empty);
        }

        private void TB_FIGURE_CTRL_SCALE_Scroll(object sender, EventArgs e)
        {
            // reaction for control navigation scale 
            TXT_FIGURE_CONTROL_SCALE.Text = TB_FIGURE_CTRL_SCALE.Value.ToString("N0");

        }

        private async void BTN_MEASURE_Click(object sender, EventArgs e)
        {
            imageView1.VIEW_Set_Clear_DispObject();
            report.SetInit();

            CFigureManager fm = imageView1.fm.Clone() as CFigureManager;

            if (fm.RECP_FILE == null)
            {
                MessageBox.Show("Invalid RECP File.\n Please Try Again With Correct RECP.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int nCount = LV_FILE_LIST.Items.Count;

            int imageW = 0;
            int imageH = 0;

            if (CHK_MEASURE_VIEW_ONLY.Checked == true) // image view measurement
            {
                string strFile = imageView1.m_strLoadedFile;
                Bitmap bmp = imageView1.GetDisplay_Bmp();
                byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp, ref imageW, ref imageH);

                _Do_Measurement(rawImage, imageW, imageH, true, strFile, 0, 0);

                nHarras = 0;
            }
            else if (CHK_MEASURE_VIEW_ONLY.Checked == false) // Sequential measurement
            {
                for (int nIndex = 0; nIndex < nCount; nIndex++)
                {
                    string strFile = LV_FILE_LIST.Items[nIndex].SubItems[1].Text;
                    Bitmap bmp = await staticImageLoadAsync(strFile);

                    byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp, ref imageW, ref imageH);

                    _PRINT_MSG(Path.GetFileName(strFile));
                    _Do_Measurement(rawImage, imageW, imageH, true, strFile, 0, 0);

                    if (nHarras > 3)
                    {
                        MessageBox.Show("Sudden Attack!!!", "Priority 1 Interrupt Occured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        nHarras = 0;
                        _PRINT_MSG("USER CALLED URGENT INTERUPT.");
                        break;
                    }
                }
                UC_LOG_VIEWER.WRITE_LOG( string.Format("MEASUREMENT FINISHED -: Totally {0}", nCount), DEF_OPERATION.OPER_05_MEAS);
            }

 
            if (CHK_MEASURE_DUMP.Checked == true)
            {

                report.INFO_PTRN = fm.param_ptrn.PTRN_FILE;
                report.INFO_RECIPE = fm.RECP_FILE;
                report.INFO_TIME = WrapperDateTime.GetTImeCode4Save_YYYY_MM_DD_HH_MM_SS_MMM();
                report.INFO_PIXEL_X = fm.param_optics.PIXEL_RES*1000.0;
                report.INFO_PIXEL_Y = fm.param_optics.PIXEL_RES*1000.0;

                //*************************************************************************
                // make report file

                CMeasureReport._WriteMeasurementData(report, fm.config.i02_PATH_DATA_DUMP);
            }
        }

        
        public static Task<Bitmap> staticImageLoadAsync(string strPath)
        {
            return Task.Run<Bitmap>(() =>
                {
                    return staticImageLoad(strPath);
                });
        }
        public static Bitmap staticImageLoad(string strPath)
        {
            Bitmap bmp = null;
            if (File.Exists(strPath) == true)
            {
                bmp = Bitmap.FromFile(strPath) as Bitmap;
            }
            return bmp;
        }
        
      

        private void BTN_UPDATE_PARAMETER_Click(object sender, EventArgs e)
        {
            // exception
            if (LV_PARAMETER.FocusedItem == null) return;
            int nIndexLV = LV_PARAMETER.FocusedItem.Index;

            // get selection index
            string strSelected = LV_PARAMETER.Items[nIndexLV].SubItems[1].Text;
            string[] parse = strSelected.Split('-');

            string strHeader = parse[0];
            int nSelectedIndex = Convert.ToInt32(parse[1])-1;

            if (nSelectedIndex == -1)
            {
                MessageBox.Show("Invalid Index");
                return;
            }

            object objectSelected = UC_Parameter.GetCurrentData();

            if (strHeader == "RECT")
            {
                CMeasurePairRct org = (CMeasurePairRct)imageView1.iGet_Figure(IFX_FIGURE.PAIR_RCT, nSelectedIndex);

                string strOrgName = org.GetType().ToString();
                string strObjName = objectSelected.GetType().ToString();
                if (strOrgName.Contains("PairRct") == false || strObjName.Contains("PairRct") == false)
                {
                    MessageBox.Show("Wrong Target Selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;  // exception for the cross selection
                }

                CMeasurePairRct obj = ((PROPERTY_PairRct)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    // backup and Recovery
                    obj.ANGLE = org.ANGLE;

                    org = obj.CopyTo();

                    if (org.param_comm_01_compen_A == 0) // compensation value A exception 170809
                    {
                        MessageBox.Show("Invalid Compensation Value. \nParameter Update has denied.", "Invalid Parameter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (IsInvalidParameters(org) == true) return;

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-DIG", DEF_OPERATION.OPER_03_PARAM);
                    MessageBox.Show("Parameters has modified.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (strHeader == "CIRCLE")
            {
                CMeasurePairCir org = (CMeasurePairCir)imageView1.iGet_Figure(IFX_FIGURE.PAIR_CIR, nSelectedIndex);

                string strOrgName = org.GetType().ToString();
                string strObjName = objectSelected.GetType().ToString();
                if (strOrgName.Contains("PairCir") == false || strObjName.Contains("PairCir") == false)
                {
                    MessageBox.Show("Wrong Target Selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;  // exception for the cross selection
                }

                CMeasurePairCir obj = ((PROPERTY_PairCir)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    org = obj.CopyTo();

                    if (org.param_comm_01_compen_A == 0) // compensation value A exception 170809
                    {
                        MessageBox.Show("Invalid Compensation Value. \nParameter Update has denied.", "Invalid Parameter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (IsInvalidParameters(obj) == true) return;

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-CIR", DEF_OPERATION.OPER_03_PARAM);
                    MessageBox.Show("Parameters has modified.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (strHeader == "OVERLAY")
            {
                CMeasurePairOvl org = (CMeasurePairOvl)imageView1.iGet_Figure(IFX_FIGURE.PAIR_OVL, nSelectedIndex);

                string strOrgName = org.GetType().ToString();
                string strobjName = objectSelected.GetType().ToString();

                if (strobjName.Contains("PairOvl") == false || strobjName.Contains("PairOvl") == false)
                {
                    MessageBox.Show("Wrong Target Selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CMeasurePairOvl obj = ((PROPERTY_PairOvl)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    org = obj.CopyTo();

                    if (IsInvalidParameters(obj) == true) return;

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-OVL", DEF_OPERATION.OPER_03_PARAM);
                    MessageBox.Show("Parameters has modified.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            imageView1.Refresh();
        }
        private bool IsInvalidParameters(Object obj)
        {
            CMeasurePairRct singleDig = new CMeasurePairRct();
            CMeasurePairCir singleCIr = new CMeasurePairCir();

            bool bError = false;


            if (obj.GetType() == singleDig.GetType())
            {
                CMeasurePairRct org = (CMeasurePairRct)obj;

                if (bError == true)
                {
                    MessageBox.Show("Invalid Parameter.\n Incorrect Mag Value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return bError;
                }

                if (bError == true)
                {
                    MessageBox.Show("Invalid Parameter.\n Incorrect Searching Direction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return bError;
                }
            }

            return bError;
        }
        private void LV_RECP_SelectedIndexChanged(object sender, EventArgs e) {  }
        private void LV_RECP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //_Update_PTRN_Files();
            if (LV_RECP.FocusedItem == null) return;
            
            int nIndex = LV_RECP.FocusedItem.Index;
            
            string strFileName = LV_RECP.Items[nIndex].SubItems[1].Text;

            string strFullFilePath = Path.Combine(imageView1.fm.config.i04_PATH_RECP_REAL, strFileName);

            if (MessageBox.Show("Do You Want To Change Current Recp\nTo :" + strFileName, "RECP Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _ChangeRecipe(strFullFilePath);
                _LV_RECP_Coloring();
            }
            _Update_RECP_Files();
            
        }

        

        
 

        

        private void TAB_MENU_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
        }
 

       

        private void RDO_TYPE_HOR_CheckedChanged(object sender, EventArgs e){_SetDiagonalControl(false);}
        private void RDO_TYPE_VER_CheckedChanged(object sender, EventArgs e){_SetDiagonalControl(false);}
        private void RDO_TYPE_DIA_CheckedChanged(object sender, EventArgs e){_SetDiagonalControl(true);}
        private void _SetDiagonalControl(bool bActive)
        {
            if (bActive == true)
            {
                LB_PARAM_DIA_ANGLE.Visible = true;
                TXT_PARAM_DIA_ANGLE.Visible = true;
                BTN_DIA_ANGLE_UP.Visible = true;
                BTN_DIA_ANGLE_DW.Visible = true;
                BTN_DIA_ANGLE_RV.Visible = true;
            }
            else
            {
                LB_PARAM_DIA_ANGLE.Visible  = false;
                TXT_PARAM_DIA_ANGLE.Visible = false;
                BTN_DIA_ANGLE_UP.Visible = false;
                BTN_DIA_ANGLE_DW.Visible = false;
                BTN_DIA_ANGLE_RV.Visible = false;
            }

        }
       
       

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //
        // SEQ <--> VS
        //
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        private void COMMON_BTN_GET_OPTICS_INFO_Click(object sender, EventArgs e)
        {
            MsgIPC.Send(IPC_ID.MC_IMAGE_GRAB_REQ, "", 5510);

           
        }

        public List<string> COMMON_GetRecpList()
        {
            string[] recpFiles = System.IO.Directory.GetFiles(imageView1.fm.config.i04_PATH_RECP_REAL, "*.xml", System.IO.SearchOption.AllDirectories);

            List<string> list = new List<string>();

            for (int i = 0; i < recpFiles.Count(); i++)
            {
                list.Add(Path.GetFileName( recpFiles.ElementAt(i).Replace(".xml","")));
            }
            return list.ToList();
        }

        
        public void COMMON_ChangeRecp(string strRecpName)
        {
            _ChangeRecipe(strRecpName);
        }

        //public static int DISTANCE = 0; // with | height | diameter
        //public static int DISPLACE = 1; // overlay

        public RES_DATA InsertMeasureData(double fDist1, PointF p1, PointF p2)
        {
            RES_DATA data = new RES_DATA();

            data.type = 0;

            data.dist = fDist1;
            data.OVL = new PointF(0, 0);

            data.P1 = p1;
            data.P2 = p2;

            return data;
        }
        // this is for the OVL Measurement
        public RES_DATA InsertMeasureData(double fDist1, double fDist2, PointF p1, PointF p2)
        {
            RES_DATA data = new RES_DATA();

            data.type = 1;

            data.dist = 0;
            data.OVL = new PointF((float)fDist1, (float)fDist1);

            data.P1 = p1;
            data.P2 = p2;

            return data;
        }

        private MeasureInfo _Do_Measurement(byte[] rawImage, int imageW, int imageH, bool bSimulation, string strFileName, double posX, double posY)
        {
            // set display original image 170814 

            imageView1.VIEW_Set_Clear_DispObject();
            imageView1.SetDisplay(rawImage, imageW, imageH);

            //*************************************************************************************
            // user defined pre-processing 170809
            //*************************************************************************************

            if (imageView1.fm.listCommand.Count != 0)
            {
                if (CHK_MEASURE_VIEW_ONLY.Checked == false)
                {
                    rawImage = Computer.TriggerProcess(rawImage, imageW, imageH, imageView1.fm.listCommand);

                    if (CHK_USE_SAVE_INPUT.Checked == true)
                    {
                        string strPathDir = Path.Combine( imageView1.fm.config.i10_PATH_IMG_ORG, WrapperDateTime.GetTimeCode4Save_YYYY_MM_DD());
                        WrapperFile.EnsureFolderExsistance( strPathDir);

                        string strTimecode = WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM();
                        imageView2.ThreadCall_SaveImage( Path.Combine(strPathDir, strTimecode + "_MEASURE.BMP"), rawImage, imageW, imageH);
                    }

                    if (m_hacker.BOOL_SHOW_IMAGE_PROCESS == true)
                    {
                        imageView1.VIEW_Set_Clear_DispObject();
                        imageView1.SetDisplay(rawImage, imageW, imageH);
                    }
                }
            }

            // removed : to check focused region during inspection 170811
            //imageView1.VIEW_SET_Mag_Origin();

            // make the copy for saving.
            byte[] rawBuff = new byte[imageW * imageH];
            Array.Copy(rawImage, rawBuff, rawImage.Length);

            RectangleF rcTemplate/*****/ = new RectangleF();
            double fMatchingRatio = 0;

            PointF ptTemplateCenter = _DO_PTRN_And_Get_TemplatePos(out rcTemplate, out fMatchingRatio);
            imageView1.fm.SetPtrnDelta(rcTemplate.X, rcTemplate.Y);

            PointF ptDelta = imageView1.fm.PT_PTRN_DELTA;
            imageView1.DrawPatternMathcing(ptTemplateCenter, rcTemplate);

            if (ptTemplateCenter.X != 0 && ptTemplateCenter.Y != 0)
            {
                imageView1.fm.SetRelativeMovemntFocusRect();
                Rectangle rcFocus = Rectangle.Round(imageView1.fm.RC_FOCUS);

                if (rcFocus.X > 0 && rcFocus.Y > 0 && rcFocus.Width > 0 && rcFocus.Height > 0)
                {
                    byte[] rawFocus = Computer.HC_CropImage(rawImage, imageW, imageH, rcFocus);
                    Bitmap bmpFocus = Computer.HC_CONV_Byte2Bmp(rawFocus, rcFocus.Width, rcFocus.Height);
                    PIC_FOCUS.Image = bmpFocus.Clone() as Bitmap;

                    if (m_hacker.BOOL_USE_FOCUS_SAVE == true)
                    {
                        string strPathDir = "c:\\"+ WrapperDateTime.GetTimeCode4Save_YYYY_MM_DD();
                        WrapperFile.EnsureFolderExsistance(strPathDir);

                        string strTimeCode = WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM();
                        Computer.SaveImage(rawFocus, bmpFocus.Width, bmpFocus.Height, Path.Combine(strPathDir, strTimeCode + "_FOCUS.BMP"));
                        System.Threading.Thread.Sleep(500);
                    }
                }


            }
            else
            {
                PIC_FOCUS.Image = new Bitmap(PIC_FOCUS.Image.Width, PIC_FOCUS.Image.Height);

                if (CHK_USE_SAVE_PTRN_ERR.Checked == true)
                {
                    // Setup Daily Directory
                    string strPathDir = Path.Combine(imageView1.fm.config.i16_PATH_HIST_PTRN, WrapperDateTime.GetTimeCode4Save_YYYY_MM_DD());
                    WrapperFile.EnsureFolderExsistance(strPathDir);

                    string strTimeCode = WrapperDateTime.GetTimeCode4Save_HH_MM_SS_MMM();
                    imageView2.ThreadCall_SaveImage(Path.Combine(strPathDir, strTimeCode + "_ERROR.BMP"), rawBuff, imageW, imageH);
                }
            }
           
            

            if (fMatchingRatio == 0) { _SetColor_PtrnResul(0); } else { _SetColor_PtrnResul(1); } 

            CFigureManager fm = imageView1.fm.Clone() as CFigureManager;

            List<string> listMeasureResult = new List<string>();
            MeasureInfo mi = new MeasureInfo();

            PointF P1 = new PointF(0, 0);
            PointF P2 = new PointF(0, 0);


            // just scale up to real unit. because optic parameter for pixel resolution is based-on stage unit ( mm )
            // remark description date : 170809

            double PIXEL_RES = fm.param_optics.PIXEL_RES*1000;

           #region CIRCLE - NOrmal case
            for (int i = 0; i < fm.COUNT_PAIR_CIR; i++)
            {
                List<PointF> listEdges_FEX = new List<PointF>();
                List<PointF> listEdges_FMD = new List<PointF>();
                List<PointF> listEdges_FIN = new List<PointF>();
                List<PointF> listEdges_SEX = new List<PointF>();
                List<PointF> listEdges_SMD = new List<PointF>();
                List<PointF> listEdges_SIN = new List<PointF>();

                // get element
                CMeasurePairCir single = fm.ElementAt_PairCir(i);

                // set relative croodinate
                single.SetRelativeMovement(ptDelta);
                single.PIXEL_RES = PIXEL_RES;

                RectangleF rcEstimated = new RectangleF();

                // get measure data
                double fDistance = single.MeasureData(rawImage, imageW, imageH, 
                    ref listEdges_FEX, 
                    ref listEdges_FMD, 
                    ref listEdges_FIN,
                    ref listEdges_SEX,
                    ref listEdges_SMD,
                    ref listEdges_SIN,
                    out P1, out P2, out rcEstimated);

                // take inspection result 
                string strInspRes = string.Format("{0} : Dia  = {1:F4} um", single.NICKNAME, fDistance );

                if (m_hacker.BOOL_USE_LOOP_COUNTER == true){strInspRes = string.Format("{0}_", m_hacker.INT_LOOP_COUNTER) + strInspRes;}

                listMeasureResult.Add(strInspRes);

                // leave inspection result 
                if (bSimulation == true)
                {
                    report.AddResult_FIG(IFX_FIGURE.PAIR_CIR, strFileName, single, fDistance);
                }
                else if (bSimulation == false)
                {
                    mi.list.Add(InsertMeasureData(fDistance, P1, P2));
                }

                // write log 
                UC_LOG_VIEWER.WRITE_LOG(strInspRes, DEF_OPERATION.OPER_05_MEAS);

                // draw fucking edges 
                imageView1.DrawPoints(listEdges_FEX, /***/1, (float)0.1, Color.Magenta);
                imageView1.DrawPoints(listEdges_FMD, /***/1, (float)0.1, Color.LimeGreen);
                imageView1.DrawPoints(listEdges_FIN, /***/1, (float)0.1, Color.Magenta);

                // draw fucking major point 

                if (m_hacker.BOOL_SHOW_DETECT_CROSS == true)
                {
                    PointF ptCenter = CPoint.GetMidPoint(P1, P2);
                    imageView1.DrawPoint(ptCenter, 10, 1, Color.Yellow);
                    imageView1.DrawPoint(P1, 10, 1, Color.Yellow);
                    imageView1.DrawPoint(P2, 10, 1, Color.Yellow);
                }

                imageView1.DrawCircle(rcEstimated, Color.DarkCyan, 3);

            }
           #endregion
            
           #region Digonal - Normal Case
            for (int i = 0; i < fm.COUNT_PAIR_RCT; i++)
            {
                List<PointF> listEdges_FEX = new List<PointF>();
                List<PointF> listEdges_FMD = new List<PointF>();
                List<PointF> listEdges_FIN = new List<PointF>();
                List<PointF> listEdges_SEX = new List<PointF>();
                List<PointF> listEdges_SMD = new List<PointF>();
                List<PointF> listEdges_SIN = new List<PointF>();

                // get element 
                CMeasurePairRct single = fm.ElementAt_PairRct(i);

                single.PIXEL_RES = PIXEL_RES;
                // set relative croodinate 
                single.SetRelativeMovement(ptDelta);

                RectangleF rcEstimaged = new RectangleF();
                // get measure data 
                double fDistance = single.MeasureData(rawImage, imageW, imageH, 
                    ref listEdges_FEX, 
                    ref listEdges_FMD, 
                    ref listEdges_FIN,
                    ref listEdges_SEX,
                    ref listEdges_SMD,
                    ref listEdges_SIN, 
                    out P1, out P2, out rcEstimaged);

                // take mearsure result 
                string strInspRes = string.Format("{0} : Width  = {1:F4} um", single.NICKNAME, fDistance );

                if (m_hacker.BOOL_USE_LOOP_COUNTER == true) { strInspRes = string.Format("{0}_", m_hacker.INT_LOOP_COUNTER) + strInspRes; }

                listMeasureResult.Add(strInspRes);

                // leave inspection result 
                if (bSimulation == true)
                {
                    report.AddResult_FIG(IFX_FIGURE.PAIR_RCT, strFileName, single, fDistance);
                }
                else if (bSimulation == false)
                {
                    mi.list.Add(InsertMeasureData(fDistance, P1, P2));
                }

                // write log 
                UC_LOG_VIEWER.WRITE_LOG(strInspRes, DEF_OPERATION.OPER_05_MEAS);

                // draw fucking edges  
                imageView1.DrawPoints(listEdges_FEX, 1, (float)0.1, Color.Red);
                imageView1.DrawPoints(listEdges_FMD, 1, (float)0.1, Color.LimeGreen);
                imageView1.DrawPoints(listEdges_FIN, 1, (float)0.1, Color.Blue);

                imageView1.DrawPoints(listEdges_SEX, 1, (float)0.1, Color.Red);
                imageView1.DrawPoints(listEdges_SMD, 1, (float)0.1, Color.LimeGreen);
                imageView1.DrawPoints(listEdges_SIN, 1, (float)0.1, Color.Blue);

                if (m_hacker.BOOL_SHOW_DETECT_CROSS == true)
                {
                    // draw fucking major point 
                    imageView1.DrawPoint(P1, 10, 1, Color.Yellow);
                    imageView1.DrawPoint(P2, 10, 1, Color.Yellow);
                }

            }
            #endregion

           #region OVL - Normal Case
            for (int i = 0; i < fm.COUNT_PAIR_OVL; i++)
            {
                List<PointF> listEdgesHOR = new List<PointF>();
                List<PointF> listEdgesVER = new List<PointF>();

                // get element 
                CMeasurePairOvl single = fm.ElementAt_PairOvl(i);

                single.PIXEL_RES = PIXEL_RES;

                // set relative croodinate 
                single.SetRelativeMovement(ptDelta);

                // take measurement result 
                double fOL_X = 0;double fOL_Y = 0;
                single.rape_MotherFucker(rawImage, imageW, imageH, ref listEdgesHOR, ref listEdgesVER, out fOL_X, out fOL_Y);

                // get measure data 
                string strInspRes = string.Format("{0} : OVL [X,Y]  = [{1:F4}, {2:F4}] um", single.NICKNAME, fOL_X , fOL_Y );

                if (m_hacker.BOOL_USE_LOOP_COUNTER == true) { strInspRes = string.Format("{0}_", m_hacker.INT_LOOP_COUNTER) + strInspRes; }

                listMeasureResult.Add(strInspRes);

                // leave measurement result 
                if (bSimulation == true)
                {
                    report.AddResult_OVL(IFX_FIGURE.PAIR_OVL, strFileName, fOL_X, fOL_Y, single);
                }
                else if (bSimulation == false)
                {
                    mi.list.Add(InsertMeasureData(fOL_X, fOL_Y, P1, P2));
                }
                     
                // write log 
                UC_LOG_VIEWER.WRITE_LOG(strInspRes, DEF_OPERATION.OPER_05_MEAS);

                // draw fucking edges 
                imageView1.DrawPoints(listEdgesHOR, /***/1, (float)0.1, Color.Magenta);
                imageView1.DrawPoints(listEdgesVER, /***/1, (float)0.1, Color.Magenta);
            }
            #endregion

            // display data to the message window 
            for (int i = 0; i < listMeasureResult.Count; i++)  { _PRINT_MSG(listMeasureResult.ElementAt(i)); }

            imageView1.Refresh();
            System.Threading.Thread.Sleep(50);

            //*************************************************************************************
            if (CHK_USE_HISTORY.Checked == true)
            //*************************************************************************************
            {
                Bitmap bmp = Computer.HC_CONV_Byte2Bmp(rawBuff, imageW, imageH);
                dlgHistM.AppedHistory(bmp, listMeasureResult, imageView1.GetDispTextObjects());
            }

            if (bSimulation == false){mi.Result = mi.list.Count == 0 ? -1 : 1;}
            if (m_hacker.BOOL_USE_LOOP_COUNTER == true) { m_hacker.INT_LOOP_COUNTER++; }

            return mi;
        }

        

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;

            if (this.WindowState == FormWindowState.Minimized){this.WindowState = FormWindowState.Normal;}

            this.Activate();
            notifyIcon1.Visible = false;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;

            if (this.WindowState == FormWindowState.Minimized){this.WindowState = FormWindowState.Normal;}

            this.Activate();
            notifyIcon1.Visible = false;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            notifyIcon1.Visible = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wrapperCog.Dispose();
            Application.Exit();
        }


        private void RDO_ROI_ASYM_CheckedChanged(object sender, EventArgs e)
        {
            // Set Focust --> after status changed  
            LV_PAIR_OVL.Focus();
        }

        private void RDO_ROI_FIGURE_CheckedChanged(object sender, EventArgs e)
        {
            int nFigureIndex = TAB_FIGURE.SelectedIndex;

            const int FIGURE_DIG = 0;
            const int FIGURE_CIR = 1;
            const int FIGURE_OVL = 2;

            /***/if (nFigureIndex == FIGURE_DIG) LV_PAIR_DIG.Focus();
            else if (nFigureIndex == FIGURE_CIR) LV_PAIR_CIR.Focus();
            else if (nFigureIndex == FIGURE_OVL) LV_PAIR_OVL.Focus();
        }

        private void BTN_MENU_PTRN_Click(object sender, EventArgs e)
        {
            if (dlgPtrn.SetParam(imageView1.fm) == true){dlgPtrn.ShowDialog();}
        }
        private void BTN_MENU_RECP_Click(object sender, EventArgs e)
        {
            if (dlgRecp.SetParam(imageView1.fm) == true){dlgRecp.ShowDialog();}
        }
        private void BTN_MENU_TUNNING_Click(object sender, EventArgs e)
        {
            if (dlgTunning.SetParam(imageView1.fm, imageView1.GetDisplay_Bmp()) == true) { dlgTunning.ShowDialog(); }
            
        }
        private void BTN_MENU_HISTORY_MATCHING_Click(object sender, EventArgs e)
        {
            if (dlgHistP.SetParam(config) == true)
            {
                dlgHistP.ShowDialog();
            }
        }
        private void BTN_MENU_HISTORY_Click(object sender, EventArgs e)
        {
            if (dlgHistM.SetParam(imageView1.fm) == true) { dlgHistM.ShowDialog(); }
        }
        private void BTN_MENU_IMAGE_PROCESSING_Click(object sender, EventArgs e)
        {
            if (dlgProcessing.SetParam(imageView1.fm, imageView1.GetDisplay_Bmp()) == true) { dlgProcessing.ShowDialog(); }
        }
        private void BTN_MENU_CONFIG_Click(object sender, EventArgs e)
        {
            if (dlgConfig.SetParam(imageView1.fm.config) == true) { dlgConfig.ShowDialog(); }
        }
        private void BTN_MENU_STATISTICS_Click(object sender, EventArgs e)
        {
            if (dlgSPC.SetParam(imageView1.fm.config) == true) { dlgSPC.ShowDialog(); }
        }

        

        private void BTN_FIGURE_REMOVE_ALL_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Remove All Figures?", "Reset All", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                imageView1.fm.RemoveAll();
                BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);
            }
        }

        private void BTN_MENU_CREATE_RECP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Current Recp Will Be Initialized.\nDo You Want To Proceed?", "Create New Recp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                // reset windows control 
                TXT_BASE_RECP.Text = string.Empty;
                TXT_PATH_PTRN_FILE.Text = string.Empty;
                TXT_PATH_RECP_FILE.Text = string.Empty;
                TXT_PTRN_POS_ORG_X.Text = TXT_PTRN_POS_ORG_Y.Text = string.Empty;
                TXT_PTRN_ACC_RATIO.Text = "0";

                // rest data 
                imageView1.VIEW_Set_Clear_DispObject();
                imageView1.fm.baseRecp.RemoveAll();
                imageView1.fm.RemoveAll();
                imageView1.Refresh();
                imageView1.VIEW_SET_Mag_Origin();

                BTN_UPDATE_FIGURE_LIST_Click(null, EventArgs.Empty);

                formBaseRecp.ShowDialog();
            }
        }
 

        private void BTN_FIGURE_MODIFY_SCALE_Click(object sender, EventArgs e)
        {
            int nValue = TB_FIGURE_CTRL_SCALE.Value;

            /***/if (((Button)sender).Name.Contains("PLUS") == true){nValue++;} 
            else if (((Button)sender).Name.Contains("MINUS") == true){nValue--;}

            TB_FIGURE_CTRL_SCALE.Value = nValue;
            TXT_FIGURE_CONTROL_SCALE.Text = nValue.ToString("N0");
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PNL_DRAW_FIGURE_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LV_RECP_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            WrapperLV.SortData(LV_RECP, e.Column);
        }

        private void BTN_MAIN_LOGO_Click(object sender, EventArgs e)
        {
            nHarras++;
            bActivateCheatMode = !bActivateCheatMode;

            if (bActivateCheatMode == false)
            {
                string temp = string.Empty;

                foreach (string s in listCheatKey)
                {
                    temp += s;
                }
                if (temp == "QLALFDLDI")
                {
                    MessageBox.Show("Hacker Mode Activated!!");

                    LB_HACKER.Visible = true;
                    BTN_HACKER.Visible = true;
                }

                listCheatKey.Clear();
            }
        }

        private void BTN_MAIN_LOGO_KeyDown(object sender, KeyEventArgs e)
        {
            listCheatKey.Add(e.KeyCode.ToString());
        }

        private void BTN_HACKER_Click(object sender, EventArgs e)
        {
            dlgHacker.SetParam(m_hacker);
            dlgHacker.ShowDialog();
        }

        private async void BTN_PLAY_Click(object sender, EventArgs e)
        {
            int nCount = LV_FILE_LIST.Items.Count;

            
            for (int nIndex = 0; nIndex < nCount; nIndex++)
            {
                string strFile = LV_FILE_LIST.Items[nIndex].SubItems[1].Text;
                Bitmap bmp = await staticImageLoadAsync(strFile);

                imageView1.SetDisplay(bmp);
                imageView1.Refresh();
                System.Threading.Thread.Sleep(700);
                //byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp, ref imageW, ref imageH);
                //
                //_PRINT_MSG(Path.GetFileName(strFile));
                //_Do_Measurement(rawImage, imageW, imageH, true, strFile, 0, 0);
                //
                if (nHarras > 3)
                {
                    MessageBox.Show("Sudden Attack!!!", "Priority 1 Interrupt Occured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    nHarras = 0;
                    _PRINT_MSG("USER CALLED URGENT INTERUPT.");
                    break;
                }

                if (nIndex == nCount) nIndex = 0;
            }
        }

        

       
    }

     static class ControlExtensions
    {
        static public void UIThread(this Control control, Action code)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(code);
                return;
            }
            code.Invoke();
        }
    }
}
