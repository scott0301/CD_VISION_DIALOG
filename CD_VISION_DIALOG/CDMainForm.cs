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
 
using IPCUtility;
using Remote;

using CodeKing.Native;
using System.Drawing.Drawing2D;

namespace CD_VISION_DIALOG
{
    public partial class CDMainForm : Form, iPtrn
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
       
        #region 제어부와 연관 부분

        public UdpIPC MsgIPC = null;
        public IpcBuffer1 sharedIPC1 = null;
        public IpcBuffer2 sharedIPC2 = null;

        StaticResult staticManager = new StaticResult();

        private void IPC_Init()
        {
            IpcClient ipcC = new IpcClient();

            sharedIPC1 = ipcC.IpcConnect<IpcBuffer1>("Remote", "Shared1");
            sharedIPC2 = ipcC.IpcConnect<IpcBuffer2>("Remote", "Shared2");

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
                #region 
                string [] LoopInfo = e.Msg.Split(',');

                int nCycle = Convert.ToInt32(LoopInfo[0]);
                int nPoint = Convert.ToInt32(LoopInfo[1]);

                nCycle += 1; // sequence index started from 0 !!!

                report.SetInit(nCycle, nPoint);
                _ctrlProgressBarSetInit(nCycle * nPoint);

                staticManager.ClearMap();
                staticManager.m_nCycleTarget = nCycle;
                staticManager.m_nPointTarget = nPoint;

                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-MEASUREMENT_START", DEF_OPERATION.OPER_06_COMM);
                UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]CYCLE={0:00}, POINTS={1:00}",nCycle, nPoint), DEF_OPERATION.OPER_06_COMM);
                #endregion
            }

            //*************************************************************************************
            // measuremet END
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_MEASURE_END)
            {
              #region
                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-MEASUREMENT_END", DEF_OPERATION.OPER_06_COMM);
 
                int nSecFin = (staticManager.m_nCycleTarget) * staticManager.m_nPointTarget;
                if (staticManager.m_nSequentialIndex == nSecFin)
                {
                    UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]CYCLE=[{0:00}/{1:00}], FIN-PNT=[{2:00}/{3:00}]",
                        staticManager.m_nCycleTarget, staticManager.m_nCycleTarget,
                        staticManager.m_nPointTarget, staticManager.m_nPointTarget), DEF_OPERATION.OPER_06_COMM);

                    _ctrlProgressBarUpdate(nSecFin);
                }
                else
                {
                    int nRestPoints = staticManager.m_nSequentialIndex - nSecFin;
                    int nTargetTotal = staticManager.m_nCycleTarget * staticManager.m_nPointTarget;
                    UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]CYCLE=[{0:00}/{1:00}], FIN-PNT=[{2:00}/{3:00}], Rest-PNT={4:00}", 
                        staticManager.m_nCycleCurrent,staticManager.m_nCycleTarget,
                        staticManager.m_nSequentialIndex, nTargetTotal,
                         nRestPoints), DEF_OPERATION.OPER_06_COMM);

                    report.INTERRUPT = false;
                }

                report.INFO_PTRN = imageView1.fm.param_ptrn.PTRN_FILE;
                report.INFO_RECIPE = imageView1.fm.RECP_FILE;
                report.INFO_PIXEL_X = imageView1.fm.param_optics.REAL_SCALE_PIXEL_RES;

                //*************************************************************************
                // make report file

                CMeasureReport._WriteMeasurementData(report, imageView1.fm.param_path.i02_PATH_DATA_DUMP, CHK_MEASURE_DUMP.Checked);
                #endregion
            }

            //*************************************************************************************
            // measuremet
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_IMAGE_MEASURE_REQ) //fin
            {
                #region 
                byte[] buffer = new byte[sharedIPC1.Buffer.Length];
                
                sharedIPC1.Buffer.CopyTo(buffer,0);
                GrabInfo obj = ByteArraySerializer<GrabInfo>.Deserialize(buffer);

                CInspUnit iu = new CInspUnit();


                int nCamNo = obj.CAM.CamNo;
                CFigureManager fm = imageView1.fm.Clone() as CFigureManager;

                for (int i = 0; i < obj.IMGs.Count; i++ )
                {
                    ImageInfo img = obj.IMGs.ElementAt(i);
                    iu.AppendItem_Single(img.Buffer, img.Width, img.Height, fm, nCamNo);
                }

                ///////////////////////////////////////////////////
                MsgIPC.Send(IPC_ID.MC_IMAGE_MEASURE_REP, "", 5510);

                // update the most recently used recp file name
                this.param_program.i00_previous_recp = TXT_PATH_RECP_FILE.Text;
                _INI_SAVE_Program();

                Remote.MeasureInfo mi = CDOL_OnReqMeasure(0, 0, iu);
                mi.list = iu.listTransResult.ToList();
 
                int nPoint = obj.POS.No;
                /***/mi.No = obj.POS.No;

                int nDataCount = mi.list.Count;

                // first initialization 
                if (staticManager.m_nSequentialIndex == 0) { staticManager.SetInit(nDataCount); }

                // show progress bar
                staticManager.m_nSequentialIndex++;
                _ctrlProgressBarUpdate(staticManager.m_nSequentialIndex);


                for (int nItem = 0; nItem < nDataCount; nItem++)
                {
                    string strResult = string.Empty;

                    if (staticManager.InsertData(nPoint, nItem, mi.list.ElementAt(nItem), out strResult) == false)
                    {
                        _PRINT_MSG("[ERROR]-" + strResult);
                    }
                }

                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-MEASURE", DEF_OPERATION.OPER_06_COMM);
                UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK] CYCLE[{0:00}/{1:00}] POS[{2:00}/{3:00}] SUCESS-{4} {5}", 
                    staticManager.m_nCycleCurrent, staticManager.m_nCycleTarget, 
                    nPoint, staticManager.m_nPointTarget, mi.Result, 
                    staticManager.m_nSequentialIndex), DEF_OPERATION.OPER_06_COMM);

                sharedIPC2.Buffer = IPCUtility.ByteArraySerializer<MeasureInfo>.Serialize(mi);
                MsgIPC.Send(IPC_ID.MC_IMAGE_MEASURE_RESULT, "", 5510);

                if (m_hacker.BOOL_USE_SAVE_SEQUENTIAL_IMAGE_SET == true)
                {
                    string timecode = Computer.GetTimeCode4Save_HH_MM_SS_MMM();
                    string savePath = Path.Combine(m_hacker.PATH_EXPERIMENTAL_IMAGE_SET, string.Format("POINT{0:00}_CYCLE{1:00}_{2}.bmp", nPoint,staticManager.m_nCycleCurrent + 1, timecode));

                    if (iu.listImage_input.Count != 0)
                    {
                        byte[] rawCopy = iu.listImage_input.ElementAt(0);
                        imageView2.ThreadCall_SaveImage(savePath, rawCopy, iu.imageW, iu.imageH);
                    }
                }

                if (staticManager.m_nSequentialIndex != 0 &&
                    staticManager.m_nPointTarget == nPoint)
                {
                    staticManager.m_nCycleCurrent++;
                }

                //MsgIPC.Send(IPC_ID.MC_IMAGE_MEASURE_REP, "", 5510);

                #endregion
            }
            //*************************************************************************************
            // matching
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_IMAGE_MATCHING_REQ) //fin
            {
                #region 
                byte[] buffer = new byte[sharedIPC1.Buffer.Length];
                //
                sharedIPC1.Buffer.CopyTo(buffer,0);
                GrabInfo obj = ByteArraySerializer<GrabInfo>.Deserialize(buffer);

                CFigureManager fm = imageView1.fm.Clone() as CFigureManager;

                //---------------------------171210
                byte[] rawImage = null;
                int imageW = 0;
                int imageH = 0;

                if( obj.IMGs.Count != 0)
                {
                    rawImage = obj.IMGs.ElementAt(0).Buffer;
                    imageW = obj.IMGs.ElementAt(0).Width;
                    imageH = obj.IMGs.ElementAt(0).Height;
                }

                Remote.MatcingInfo mi = CDOL_OnReqMatching(fm, obj.POS.X, obj.POS.Y, rawImage, imageW, imageH);

                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-MATCHING", DEF_OPERATION.OPER_06_COMM);

                if (mi.Result == -1)
                {
                    UC_LOG_VIEWER.WRITE_LOG("[☆ACK]ERR - PTRN", DEF_OPERATION.OPER_06_COMM);
                }
                else
                {
                    UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]POS[{0:F3},{1:F3}] SCORE[{2:F3}%]", mi.X, mi.Y, mi.Score ), DEF_OPERATION.OPER_06_COMM);
                }

                sharedIPC2.Buffer = IPCUtility.ByteArraySerializer<MatcingInfo>.Serialize(mi);
                MsgIPC.Send(IPC_ID.MC_IMAGE_MATCHING_REP, "", 5510);
                #endregion
            }
            //*************************************************************************************
            // recipe change
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_MACRO_LOAD_REQ) //fin
            {
                #region
                UC_LOG_VIEWER.WRITE_LOG(string.Format("[★REQ]-RECP CHANGE → {0}", e.Msg), DEF_OPERATION.OPER_06_COMM);

                Remote.RecipeInfo ri = CDOL_OnReqLoadMacro(e.Msg);

                ri.LAF = new LAFInfo(imageView1.fm.param_optics.i09_MAKE_ZERO_FILE);

                UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]CAM[{0:0}:{1:f6}] LIGHT[{2:00}:{3:000}] MZ {4}", ri.CamNo, ri.CamRes, ri.IllNo, ri.IllValue, ri.LAF.Make0Name), DEF_OPERATION.OPER_06_COMM);
                UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]FOCUS-TYPE {0} CENTERING : {1} MULTISHOT : {2}-{3}", ri.FI.FocusType, ri.IsCentering, ri.GrabCount, ri.GrabTime), DEF_OPERATION.OPER_06_COMM);

                sharedIPC2.Buffer = IPCUtility.ByteArraySerializer<RecipeInfo>.Serialize(ri);
                MsgIPC.Send(IPC_ID.MC_MACRO_LOAD_REP, "", 5510);
                #endregion
            }
            //*************************************************************************************
            // image grab
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_IMAGE_GRAB_REP) //fin
            {
                if (proc_IterativeRun.IS_STATIC_RUN == false)
                {
                #region NORMAL RUN
                    try
                    {
                        GrabInfo obj = ByteArraySerializer<GrabInfo>.Deserialize(sharedIPC1.Buffer);

                        LB_MAIN_FOCUS_PARAM.Text = obj.LAF.Make0Name;
                        imageView1.fm.param_optics.i09_MAKE_ZERO_FILE = obj.LAF.Make0Name;

                        //---------------------------171210
                        byte[] rawImage = null; int imageW = 0; int imageH = 0;

                        if (obj.IMGs.Count != 0)
                        {
                            rawImage = obj.IMGs.ElementAt(0).Buffer;
                            imageW = obj.IMGs.ElementAt(0).Width;
                            imageH = obj.IMGs.ElementAt(0).Height;
                        }

                        CDOL_OnRepGrab(obj.CAM.CamNo,
                           obj.CAM.PixelRes,
                           obj.ILL.IllNo,
                           obj.ILL.IllValue,
                           (int)obj.CAM.Exposure,
                           rawImage,
                           imageW, 
                           imageH);

                        // update croodinates according to ptrn matching
                        BTN_PTRN_MATCH_Click(null, EventArgs.Empty);

                        UC_LOG_VIEWER.WRITE_LOG("★[REQ]-GRAB", DEF_OPERATION.OPER_06_COMM);
                        UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK]CAM[{0:00}:{1:F6}] LCH[{2:00}:{3:000} IMG[{4}X{5}]",
                            obj.CAM.CamNo, obj.CAM.PixelRes, obj.ILL.IllNo, obj.ILL.IllValue, imageW, imageH), DEF_OPERATION.OPER_04_IMAGE);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                #endregion
                }
                else if (proc_IterativeRun.IS_STATIC_RUN == true)
                {
                 #region iterative static run
                    GrabInfo obj = ByteArraySerializer<GrabInfo>.Deserialize(sharedIPC1.Buffer);

                    byte[] rawImage = null;
                    int imageW = 0;
                    int imageH = 0;

                    if (obj.IMGs.Count != 0)
                    {
                        rawImage = obj.IMGs.ElementAt(0).Buffer;
                        imageW = obj.IMGs.ElementAt(0).Width;
                        imageH = obj.IMGs.ElementAt(0).Height;

                        proc_IterativeRun.AppendItem_Single(rawImage, imageW, imageH, obj.CAM.CamNo, imageView1.fm.Clone() as CFigureManager);

                    }

                    if( proc_IterativeRun.STACK_SIZE == 10)
                    {
                        _DoIterativeExperiments();
                    }
                 #endregion
                }
            }
            //*************************************************************************************
            // recipe list
            //*************************************************************************************
            if (e.Id == IPC_ID.CM_MACRO_LIST_REQ) //fin
            {
                Remote.RecipeList rl = new Remote.RecipeList();

                rl.Recipes = CDOL_OnReqMacroList();
                sharedIPC2.Buffer = IPCUtility.ByteArraySerializer<RecipeList>.Serialize(rl);

                UC_LOG_VIEWER.WRITE_LOG("[★REQ]-GET RECP LIST", DEF_OPERATION.OPER_06_COMM);
                UC_LOG_VIEWER.WRITE_LOG(string.Format("[☆ACK] Returned Recps {0}", rl.Recipes.Count), DEF_OPERATION.OPER_02_RECP);

                MsgIPC.Send(IPC_ID.MC_MACRO_LIST_REP, "", 5510);
            }
        }



        public Remote.MeasureInfo CDOL_OnReqMeasure(int nCycle, int nPoint, CInspUnit iu)
        {
            //*************************************************************************************
            // figure copy
            CFigureManager fm = imageView1.fm.Clone() as CFigureManager;

            //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
            // Do measurement
            _Do_Measurement(iu, false, 0, nCycle, nPoint);
            //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●

            MeasureInfo mi = new MeasureInfo();
            mi.Focus = iu.mi.Focus;
            mi.list = iu.listTransResult.ToList();
            mi.Result = mi.list.Count == 0 ? -1 : 1;

            //*************************************************************************************
            // Image Save
            //*************************************************************************************

            int imageW = 0;int imageH = 0;
            byte[] rawImage = iu.GetImage_Raw_Last(out imageW, out imageH);


            //*************************************************************************************
            if (m_hacker.BOOL_USE_LEAVE_HISTORY_MEASUREMENT == true)
            {
                Bitmap bmp = Computer.HC_CONV_Byte2Bmp(rawImage, iu.imageW, iu.imageH);
                dlgHistM.AppedHistory(bmp, iu.listInspResult, imageView1.GetDispTextObjects());
            }

            //*************************************************************************************
            if (m_hacker.BOOL_USE_SAVE_INPUT_IMAGE == true)
            {
                string strPathDir = Path.Combine(fm.param_path.i05_PATH_IMG_ORG, Computer.GetTimeCode4Save_YYYY_MM_DD());
                Computer.EnsureFolderExsistance(strPathDir);

                string strTimecode = Computer.GetTimeCode4Save_HH_MM_SS_MMM();
                byte[] rawInput4Save = iu.GetImage_Raw_First(out imageW, out imageH);
                imageView2.ThreadCall_SaveImage(Path.Combine(strPathDir, strTimecode + "_MEASURE.BMP"), rawInput4Save, imageW, imageH);
            }
            //*************************************************************************************
            // Display
            //*************************************************************************************
            imageView1.VIEW_Set_Clear_DispObject();

            if (m_hacker.BOOL_SHOW_IMAGE_PROCESS == true && iu.IsPreprocessed() == true)
            {
                rawImage = iu.GetImage_prep_Last(out imageW, out imageH);
            }
            
            imageView1.SetDisplay(rawImage, imageW, imageH);
            imageView1.DrawPoints(iu.listDispEdgePoints);
            imageView1.Refresh();

            // display data to the message window 
            for (int i = 0; i < iu.listInspResult.Count; i++) { _PRINT_MSG(iu.listInspResult.ElementAt(i)); }

            return mi;
        }


        public Remote.MatcingInfo CDOL_OnReqMatching(CFigureManager fm, double x, double y, byte[] rawImage, int imageW, int imageH)
        {
            //imageView1.VIEW_Set_Clear_DispObject();
            //imageView1.SetDisplay(rawImage, imageW, imageH);

            RectangleF rcTemplate/*****/ = new RectangleF();
            double fMatchingRatio = 0;

            Bitmap bmpView = Computer.HC_CONV_Byte2Bmp(rawImage, imageW, imageH);
            PointF ptTemplateCenter = _DO_PTRN_And_Get_TemplatePos(fm, bmpView, out rcTemplate, out fMatchingRatio);

            //imageView1.DrawPatternMathcing(ptTemplateCenter, rcTemplate);
            //imageView1.Refresh();

            // Modify
            Remote.MatcingInfo mi = new Remote.MatcingInfo();
            if (fMatchingRatio == 0)
            {
                mi.Result = -1;
                mi.Macro = TXT_PATH_RECP_FILE.Text.Replace(".xml", "");

                if (m_hacker.BOOL_USE_LEAVE_HISTORY_ERROR_PTRN == true)
                {
                    string strPathDir = Path.Combine(imageView1.fm.param_path.i08_PATH_HIST_PTRN, Computer.GetTimeCode4Save_YYYY_MM_DD());
                    Computer.EnsureFolderExsistance(strPathDir);

                    string strTimecode = Computer.GetTimeCode4Save_HH_MM_SS_MMM();
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

        public Remote.RecipeInfo CDOL_OnReqLoadMacro(string strMacroName)
        {
            string strMacroPath = Path.Combine(imageView1.fm.param_path.i04_PATH_RECP_REAL, strMacroName);
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

                CFigureManager fm = imageView1.fm.Clone() as CFigureManager;

                PARAM_OPTICS param_optic = fm.param_optics;

                ri.CamNo/*********/= param_optic.i01_CAM_INDEX;
                ri.CamRes/********/= param_optic.i02_PIXEL_RES;
                ri.IllNo/*********/= param_optic.i03_LIGHT_INDEX;
                ri.IllValue/******/= param_optic.i04_LIGHT_VALUE;
                ri.Exposure/******/= param_optic.i05_EXPOSURE;
                ri.IsCentering/***/= fm.baseRecp.PARAM_05_USE_CENTERING;
                ri.GrabCount/*****/= param_optic.i06_MULTI_SHOT_COUNT;
                ri.GrabTime/******/= param_optic.i07_MULTI_SHOT_DELAY;

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

        public void CDOL_OnRepGrab(int camNo, double camRes, int illNo, int illValue, int iExposure, byte[] rawImage, int imageW, int imageH)
        {
            PARAM_OPTICS param_optic = imageView1.fm.param_optics;

            param_optic.i01_CAM_INDEX = camNo;
            param_optic.i02_PIXEL_RES = camRes;
            param_optic.i03_LIGHT_INDEX = illNo;
            param_optic.i04_LIGHT_VALUE = illValue;
            //param_optic.i05_EXPOSURE = iExposure;

            imageView1.VIEW_Set_Clear_DispObject();
            imageView1.SetDisplay(rawImage, imageW, imageH);
            imageView1.VIEW_SET_Mag_Origin();
            imageView1.Refresh();

            if (m_hacker.BOOL_USE_SAVE_MANUAL_GRAB == true)
            {
                string strPathDir = Path.Combine("c:\\", Computer.GetTimeCode4Save_YYYY_MM_DD());
                Computer.EnsureFolderExsistance(strPathDir);

                Bitmap bmp = imageView1.GetDisplay_Bmp();

                string strTimecode = Computer.GetTimeCode4Save_HH_MM_SS_MMM();
                imageView1.ThreadCall_SaveImage(Path.Combine(strPathDir, strTimecode + "_GRAB.BMP"), bmp);
            }
        }
        #endregion

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

       #region SEQ <--> VS thirdparty
        private void COMMON_BTN_GRAB_FROM_SEQUENCE_Click(object sender, EventArgs e) 
        {
            MsgIPC.Send(IPC_ID.MC_IMAGE_GRAB_REQ, "", 5510); 
        }
        public List<string> COMMON_GetRecpList()
        {
            string[] recpFiles = System.IO.Directory.GetFiles(imageView1.fm.param_path.i04_PATH_RECP_REAL, "*.xml", System.IO.SearchOption.AllDirectories);

            List<string> list = new List<string>();

            for (int i = 0; i < recpFiles.Count(); i++)
            {
                list.Add(Path.GetFileName(recpFiles.ElementAt(i).Replace(".xml", "")));
            }
            return list.ToList();
        }
        public void COMMON_ChangeRecp(string strRecpName)
        {
            _Recp_Change(strRecpName);
        }


       
        #endregion

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //*******************************************************************************************
        // FUCK THAT SHIT
        //*******************************************************************************************

 
        BASE_RECP param_baseRecp = new BASE_RECP();

        public cogWrapper wrapperCog = new cogWrapper();
        public CMeasureReport report = new CMeasureReport();

        int m_DrawType_Master = UC_Graph.IFX_GRAPH_TYPE.PROJ_V;
        int m_DrawType_Slave = UC_Graph.IFX_GRAPH_TYPE.PROJ_V;

        public PARAM_PATH param_path = new PARAM_PATH();
        public PARAM_PROGRAM param_program = new PARAM_PROGRAM();

        public WrapperINI INI_LAYERS = new WrapperINI();
        public WrapperINI INI_CONFIG = new WrapperINI();
        public WrapperINI INI_PROGRAM = new WrapperINI();

        // for running interruption!! 
        public int/********/m_nInterruptCount = 0;
        public bool/*******/m_bActivateCheatMode = false;
        public int/********/m_nSecretKeyPress = 0;
        public List<string> m_listCheatKey = new List<string>();

        FormBaseRecp formBaseRecp = new FormBaseRecp();

        DLG_Ptrn dlgPtrn = null;
        Dlg_Recp dlgRecp = null;

        DLg_Processing dlgProcessing = null;

        Dlg_HistoryP/****/dlgHistP = null;
        Dlg_HistoryM/****/dlgHistM = null;
        Dlg_Config/******/dlgConfig = null;
        Dlg_Tunning/*****/dlgTunning = null;
        Dlg_Advanced/****/dlgAdvanced = null;
        Dlg_Spc/*********/dlgSPC = null;

        CAdvancedMode m_hacker = new CAdvancedMode();

        CThrProc_FocusTool proc_FocusTool = new CThrProc_FocusTool();
        CIterativeStaticRun proc_IterativeRun = new CIterativeStaticRun();
        CThreadProc_Insp proc_inspection = new CThreadProc_Insp();

        DateTime dt_Measurement_start = new DateTime();

        CLinguisticHelper m_speaker = new CLinguisticHelper();
        public byte[] m_raw_blend = null;

        //****************************************************
        // Constant Variable
        //****************************************************

        public CDMainForm()
        {
            InitializeComponent();
            
            // initialization for COMMNUNICATION <---> SEQ 
            IPC_Init();

            // Initialization for Viwer 
            imageView1.eventDele_HereComesNewImage += new dele_HereComesNewImage(ViewUpdateEvent_FIRST);
            imageView2.eventDele_HereComesNewImage += new dele_HereComesNewImage(ViewUpdateEvent_SECON);

            proc_inspection.EventThreadFinished_Inspection += EventThreadFinished_Inspection;

           #region DIALOG Update Event Related 

            formBaseRecp.eventDele_SendDelebaseResult += new FormBaseRecp.deleSendBaseRecp(deleFunc_GetBaseRecp);

            dlgPtrn = new DLG_Ptrn(this as iPtrn);
            dlgPtrn.eventDele_ApplyParamPtrn += new DLG_Ptrn.dele_ApplyParamPtrn(deleFunc_GetParamPtrn);

            dlgRecp = new Dlg_Recp();

            dlgHistM = new Dlg_HistoryM();
            dlgHistM.eventDele_ChangeRecp += new Dlg_HistoryM.dele_ChangeRecp(deleFunc_ChangeRecp);

            dlgHistP = new Dlg_HistoryP();
            dlgProcessing = new DLg_Processing();


            dlgConfig = new Dlg_Config();
            dlgConfig.eventDele_ChangeParamPath += new Dlg_Config.dele_ChangeParamPath(deleFunc_ChangeConfig);

            dlgTunning = new Dlg_Tunning();
            dlgTunning.eventDele_FinishTunning += new Dlg_Tunning.dele_FinishTunning(deleFunc_ChangeTunning);

            dlgAdvanced = new Dlg_Advanced();

            dlgSPC = new Dlg_Spc();

           #endregion 

            proc_IterativeRun.SetInit();

            // for glass effect
            this.Padding = new Padding(10);
        }

    

       #region INTERFACE FOR PATTERN MATCHING

        // just for cognex initialization delay removal. 
        public void/*****/iPtrn_Init()
        {
            Bitmap bmpSource = new Bitmap(100, 100);
            Bitmap bmpTemplate = new Bitmap(10, 10);

            double fRatio = 100;

            RectangleF rcTemplate = new RectangleF();
            PointF ptTemplateCenter = new Point();

            iPtrn_DoPtrn(bmpSource, bmpTemplate, fRatio, new RectangleF(0,0, 100, 100), out rcTemplate, out ptTemplateCenter);
        }
        public Bitmap/***/iPtrn_LoadImage()
        {
            return imageView1.GetDisplay_Bmp();
        }
        public double/***/iPtrn_DoPtrn(Bitmap bmpSource, Bitmap bmpTemplate, double fRatio, RectangleF rcSearching, out RectangleF rcTemplate, out PointF ptTemplateCenter)
        {
            rcTemplate/*********/= new RectangleF(0, 0, 0, 0);
            ptTemplateCenter/***/= new PointF(0, 0);

            // Searching region verificaiton 
            if( m_speaker.Check_Is_Invalid_Figure(rcSearching, bmpSource.Width, bmpSource.Height) == true){return 0;}

            ResultPTRN ptrn = new ResultPTRN();

            // set parameters
            wrapperCog.PARAM_NUM_TO_FIND = 1;
            wrapperCog.PARAM_ACCEPT_RATIO = fRatio;
            wrapperCog.PARAM_RC_SEARCH = rcSearching;
            wrapperCog.PARAM_PT_RELATIVE_ORIGIN = new PointF(0, 0);

            // do pattern matching
            wrapperCog.Do_Ptrn(bmpSource, bmpTemplate);

            // get the result 
            ptrn = wrapperCog.ptrnResult;

            double fMatchingRatio = 0;

            // if sucess, then set the results 
            if (ptrn.Count >= 1)
            {
                ptTemplateCenter/***/= ptrn.resultList.ElementAt(0).ptCenter;
                rcTemplate/*********/= ptrn.resultList.ElementAt(0).rc;
                fMatchingRatio/*****/= ptrn.resultList.ElementAt(0).fScore * 100.0;
            }
            return fMatchingRatio;
        }

       #endregion

       #region INI RELATED 
       private void _INI_LOAD_Program()
        {
            // get the ini file path
            string strPath = Path.Combine(Application.StartupPath, "INI", "PROGRAM.INI");

            // check ini file existance
            if (m_speaker.Check_INI_Does_Exist_File(strPath) == false) { return; }

            // clear structure and load from ini file
            INI_PROGRAM.Clear();
            INI_PROGRAM.Load(strPath);

            // set values 
            string strSection = "RECP";
            this.param_program.i00_previous_recp = INI_PROGRAM.GetStringField(strSection, "i00_PREVIOUS_RECP");
        }
        private void _INI_SAVE_Program()
       {
           // make path
           string strPath = Path.Combine(Application.StartupPath, "INI", "PROGRAM.INI");

           // clear structure

           INI_PROGRAM.Clear();

           // set data and save 
           string strSection = "RECP";
           INI_PROGRAM.Add(strSection);
           INI_PROGRAM.Add(strSection, "i00_PREVIOUS_RECP"/***********/, this.param_program.i00_previous_recp, "previous recp");
           INI_PROGRAM.Save(strPath);
       }

        private void _INI_Load_Config()
        {
            // get the ini file path
            string strPath = Path.Combine(Application.StartupPath, "INI", "CONFIG.INI");

            // check ini file existance
            if (m_speaker.Check_INI_Does_Exist_File(strPath) == true){return;}

            // clear structure and load from ini file
            INI_CONFIG.Clear();
            INI_CONFIG.Load( strPath);

            // set values 
            string strSection = "PATH";
            this.param_path.i01_PATH_MAIN/************/= INI_CONFIG.GetStringField(strSection,"i01_PATH_MAIN");
            this.param_path.i02_PATH_DATA_DUMP/*******/= INI_CONFIG.GetStringField(strSection,"i02_PATH_DATA_DUMP");
            this.param_path.i03_PATH_RECP_BASE/*******/= INI_CONFIG.GetStringField(strSection,"i03_PATH_RECP_BASE");
            this.param_path.i04_PATH_RECP_REAL/*******/= INI_CONFIG.GetStringField(strSection,"i04_PATH_RECP_REAL");
            this.param_path.i05_PATH_IMG_ORG/*********/= INI_CONFIG.GetStringField(strSection,"i10_PATH_IMG_ORG");
            this.param_path.i06_PATH_IMG_PTRN/********/= INI_CONFIG.GetStringField(strSection,"i11_PATH_IMG_PTRN");
            this.param_path.i07_PATH_HIST_MEASURE/****/= INI_CONFIG.GetStringField(strSection,"i15_PATH_HIST_MEASURE");
            this.param_path.i08_PATH_HIST_PTRN /******/= INI_CONFIG.GetStringField(strSection,"i16_PATH_HIST_PTRN");
            this.param_path.i09_PATH_INI/*************/= INI_CONFIG.GetStringField(strSection,"i20_PATH_INI");
            this.param_path.i10_PATH_LOG/*************/= INI_CONFIG.GetStringField(strSection,"i21_PATH_LOG");

            // ensure eccential folder existance
            _EnsureEccentialFolders();
        }
        private void _INI_Save_Config()
        {
            // make path
            string strPath = Path.Combine(Application.StartupPath, "INI", "CONFIG.INI");

            // clear structure
            INI_CONFIG.Clear();

            // set data and save 
            string strSection = "PATH";
            INI_CONFIG.Add(strSection);
            INI_CONFIG.Add(strSection, "i01_PATH_MAIN"/***********/,this.param_path.i01_PATH_MAIN/***********/,"path for main");
            INI_CONFIG.Add(strSection, "i02_PATH_DATA_DUMP"/******/,this.param_path.i02_PATH_DATA_DUMP/******/,"path for data dump");
            INI_CONFIG.Add(strSection, "i03_PATH_RECP_BASE"/******/,this.param_path.i03_PATH_RECP_BASE/******/,"path for base recp files");
            INI_CONFIG.Add(strSection, "i04_PATH_RECP_REAL"/******/,this.param_path.i04_PATH_RECP_REAL/******/,"path for real recp files");
            INI_CONFIG.Add(strSection, "i10_PATH_IMG_ORG"/********/,this.param_path.i05_PATH_IMG_ORG/********/,"path for original input images");
            INI_CONFIG.Add(strSection, "i11_PATH_IMG_PTRN"/*******/,this.param_path.i06_PATH_IMG_PTRN/*******/,"path for teaching images");
            INI_CONFIG.Add(strSection, "i15_PATH_HIST_MEASURE"/***/,this.param_path.i07_PATH_HIST_MEASURE/***/,"path for history of measurement");
            INI_CONFIG.Add(strSection, "i16_PATH_HIST_PTRN"/******/,this.param_path.i08_PATH_HIST_PTRN/******/,"path for history of pattern matching failure");
            INI_CONFIG.Add(strSection, "i20_PATH_INI"/************/,this.param_path.i09_PATH_INI/************/,"path for ini files");
            INI_CONFIG.Add(strSection, "i21_PATH_LOG"/************/,this.param_path.i10_PATH_LOG/************/,"path for log files");
            INI_CONFIG.Save(strPath);

            // ensure folder existance
            _EnsureEccentialFolders();
        }
        private void _INI_Load_Default_Recp(string strPath)
        {
            INI_LAYERS.Clear();
            INI_LAYERS.Load(strPath);

            BASE_RECP single = new BASE_RECP();

            string strSection = "PARAM";

            single.PARAM_00_BASE_RECP_NAME/*****/= strPath;
            //single.PARAM_01_LENS_INDEX/*******/= ??
            single._param_01_lens_index/********/= INI_LAYERS.GetStringField(strSection, "PARAM_01_LENS_INDEX");
            //single.PARAM_02_LIGHT_INDEX/******/= ??
            single._param_02_light_index/*******/= INI_LAYERS.GetStringField(strSection, "PARAM_02_LIGHT_INDEX");
            single.PARAM_03_LIGHT_VALUE/********/= INI_LAYERS.GetIntegerField(strSection, "PARAM_03_LIGHT_VALUE");
            //single.PARAM_04_FOCUS_TYPE/*******/= ??
            single.PARAM_04_FOCUS_TYPE/*********/= INI_LAYERS.GetIntegerField(strSection, "PARAM_04_FOCUS_TYPE");
            single.PARAM_05_USE_CENTERING/******/= INI_LAYERS.GetIntegerField(strSection, "PARAM_05_USE_CENTERING");
        }
        #endregion

       #region MAIN DIALOG PROGRESS BAR AND TIMER RELATED

        private void _ctrlProgressBarSetInit(int nMax)
        {
            // just intit : set to zero
            this.UIThread(delegate 
            {
                PROG_RUN.Value = 0;
                PROG_RUN.Maximum = nMax;
                LB_CYCLE_INFO.Text = string.Format("CYCLE : [0/{0}]", nMax);
            });

            dt_Measurement_start = DateTime.Now;
            
        }
        private void _ctrlProgressBarUpdate(int nValue)
        {
            // update running sequence
            this.UIThread(delegate
            {
                int nTotal = PROG_RUN.Maximum;
                int nRest = nTotal - nValue;
                if (nTotal < nValue) return;

                System.Threading.Thread.Sleep(10);
                PROG_RUN.Value = nValue;
                LB_CYCLE_INFO.Text = string.Format("CYCLE : [{0}/{1}]", nValue, PROG_RUN.Maximum);

                DateTime dt_current = DateTime.Now;

                string strExecution = Computer.TIME_Get_Substracted_Time(dt_Measurement_start, dt_current);

                if (nValue == 0) nValue = 1;

                double fSecRemained/***/= Computer.GetDiffTotalSec(dt_Measurement_start, dt_current);
                double fOneUnit/*******/= fSecRemained  / nValue;
                double fPredict/*******/= fOneUnit * nRest;

                LB_TIME_EXECUTION.Text = "STARTED : " + strExecution;
                LB_TIME_PREDICTION.Text = "REMAINED : " + Computer.GetTactCode_DD_HH_MM_SS(fPredict);
            }); 
        }
        private void _Activate_LifeTime()
        {
            DateTime dtExecute = DateTime.Now;

            Timer tm = new Timer();
            tm.Interval = 120;

            tm.Tick += delegate(object se, EventArgs ea)
            {
                DateTime dtCurrent = DateTime.Now;

                int nSecSum = Computer.GetDiffTotalSec(dtExecute, dtCurrent);

                string s = Computer.GetTactCode_DD_HH_MM_SS(nSecSum);

                this.UIThread(delegate
                {
                    LB_LIFE_TIME.Text = "LIFE_TIME : " + s;
                });

            };
            tm.Start();
        }

        #endregion

       #region SUB MENU DIALOG Delegates
        private void deleFunc_GetBaseRecp(BASE_RECP single)
        {
            TXT_BASE_RECP.Text = single.PARAM_00_BASE_RECP_NAME;
            imageView1.fm.baseRecp = single.CopyTo();
        }
        private void deleFunc_GetParamPtrn(PARAM_PTRN paramPtrn)
        {
            imageView1.fm.param_ptrn = paramPtrn;
            imageView1.fm.SetPtrnDelta(paramPtrn.RC_TEMPLATE.X, paramPtrn.RC_TEMPLATE.Y);

            /***/
            if (paramPtrn.BOOL_GLOBAL_SEARCHING == true)
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
            TXT_PTRN_ACC_RATIO.Text = paramPtrn.ACC_RATIO.ToString("F2");
            TXT_PTRN_POS_ORG_X.Text = paramPtrn.RC_TEMPLATE.X.ToString("F2");
            TXT_PTRN_POS_ORG_Y.Text = paramPtrn.RC_TEMPLATE.Y.ToString("F2");

            string strPathPtrn = Path.Combine(imageView1.fm.param_path.i06_PATH_IMG_PTRN, paramPtrn.PTRN_FILE);
            _SetView_PTRN(strPathPtrn);

        }
        private void deleFunc_ChangeRecp(string strPathRecp, string strPathImage)
        {
            _Recp_Change(strPathRecp);

            if (m_speaker.Check_Is_Error_File_Path_Validity(strPathImage) == false)
            {
                imageView1.ThreadCall_LoadImage(strPathImage);
            }
        }
        private void deleFunc_ChangeConfig(PARAM_PATH param_Path)
        {
            // if configuration has changed??
            // then, update data in the current recipe and !!!
            // Must!!!!  Save!!! Default Configuration 
            // Also, Every Recp change has occured??  directly replace configuration .!!!! 

            imageView1.fm.param_path = param_Path.CopyTo();
            this.param_path = param_Path.CopyTo();
            _INI_Save_Config();
        }
        private void deleFunc_ChangeTunning()
        {
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            // set window position 
            this.Location = new System.Drawing.Point(0, 0);

            //init main dialog timer display
            _Activate_LifeTime();

            // init image viewers
            imageView1.SetInit();
            imageView2.SetInit();

            // load ini 
            _INI_Load_Config();
            // copy path related parameters from the ini-linked structure
            imageView1.fm.param_path = this.param_path.CopyTo();
            // make sure eccential folders
            _EnsureEccentialFolders();

            // init pattern matching module
            iPtrn_Init();

            // set default image for ptrn matching result view 
            PIC_FOCUS.Image = new Bitmap(300, 300);

            //initialze log control
            UC_LOG_VIEWER.SetBasePath(param_path.i10_PATH_LOG);
            UC_LOG_VIEWER.WRITE_LOG("Library Initialization", DEF_OPERATION.OPER_01_POWER);
            UC_LOG_VIEWER.SetSize(655, 235);
            UC_LOG_VIEWER.WRITE_LOG("Program Initialized.", DEF_OPERATION.OPER_01_POWER);
            UC_Parameter.SetSize(380, 350);

            // update recp list 
            _Recp_Update_List();

            // initialize figure tab
            TAB_FIGURE_SelectedIndexChanged(null, EventArgs.Empty);

            // update program build information 
            this.Text = this.Text + " :: " + _GetBuildInfo();
         }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_speaker.Check_And_Ask_Further_Do_Program_Exit() ==true )
            {
                exitToolStripMenuItem_Click(null, EventArgs.Empty);
                notifyIcon1.Dispose();
            }
            else
            {
                // cancel closing message
                e.Cancel = true;

                // recover notifyicon
                this.Visible = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(100);

            }
        }

        public static string _GetBuildInfo()
        {
            // get assembly name 
            string strinfo = System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName;

            // parse purename 
            var parse = strinfo.Split(',');
            string filename = parse[0] + ".exe";

            // make path and get the last compiled time
            string strCurPath = Application.StartupPath;
            FileInfo fi = new FileInfo(Path.Combine( strCurPath , filename));
            DateTime dt = fi.LastWriteTime;

            // make the timecode
            string[] t = { dt.Year.ToString(), string.Format("{0:00}", dt.Month),  string.Format("{0:00}", dt.Day  ), 
                                      string.Format("{0:00}", dt.Hour  ), string.Format("{0:00}", dt.Minute), string.Format("{0:00}", dt.Second) };

            string strDateTime = t[0] + t[1] + t[2] + "_" + t[3] + t[4] + t[5];

            // return generated build information
            return "BUILD INFORMATION : " + strDateTime;
        }
        private void _PRINT_MSG(string message)
        {
            this.UIThread(delegate
            {
                string time = Computer.GetTimeCode4Save_HH_MM_SS_MMM();
                string line = time + " : " + message + System.Environment.NewLine;
                msg.AppendText(line);
                msg.ScrollToCaret();
            });
        }
        private void _EnsureEccentialFolders()
        {
            // working directory exsistance Check and Ensure existance.
            Computer.EnsureFolderExsistance(param_path.i01_PATH_MAIN);
            Computer.EnsureFolderExsistance(param_path.i02_PATH_DATA_DUMP);
            Computer.EnsureFolderExsistance(param_path.i03_PATH_RECP_BASE);
            Computer.EnsureFolderExsistance(param_path.i04_PATH_RECP_REAL);
            Computer.EnsureFolderExsistance(param_path.i05_PATH_IMG_ORG);
            Computer.EnsureFolderExsistance(param_path.i06_PATH_IMG_PTRN);
            Computer.EnsureFolderExsistance(param_path.i07_PATH_HIST_MEASURE);
            Computer.EnsureFolderExsistance(param_path.i08_PATH_HIST_PTRN);
            Computer.EnsureFolderExsistance(param_path.i09_PATH_INI);
            Computer.EnsureFolderExsistance(param_path.i10_PATH_LOG);
        }

       #region IMAGE Viewer Related
        //*****************************************************************************************
        // for image blending  ( viewer 1 + viewer 2 )
        private void CHK_BLEND_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_BLEND.Checked == true)
            {
                imageView1.SetLayerDisp_Status(true, imageView2.GetDisplay_Bmp());
                TB_BLENDING_RATIO.Enabled = true;
                LB_BLEND_VALUE.ForeColor = Color.Red;
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
            int nValue = 0;

            if (imageW1 == imageW2 && imageH1 == imageH2)
            {
                nValue = TB_BLENDING_RATIO.Value;
                LB_BLEND_VALUE.Text = nValue.ToString() + "%";
                imageView1.SetLayerDisp(nValue);
                imageView1.Refresh();
            }
            else
            {
                return;
            }

        }
        //*****************************************************************************************
        // image viewer update event 
        private void ViewUpdateEvent_FIRST() { }
        private void ViewUpdateEvent_SECON() { }
       #endregion

       #region MAIN_RECP SAVE BUTTON EVENTS
        private void BTN_RECP_SAVE_AS_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML files (*.XML)|*.xml|All files (*.*)|*.*";
            dlg.InitialDirectory = imageView1.fm.param_path.i04_PATH_RECP_REAL;

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            _Recp_Save(dlg.FileName);
            _Recp_Update_List();
        }
        private void BTN_RECP_SAVE_Click(object sender, EventArgs e)
        {
            // verification for overwritting
            if (m_speaker.Check_And_Ask_Further_Do_Overwrite_Recp() == false) {return;}

            // check recp file verification
            string strRecpFile = Path.Combine(imageView1.fm.param_path.i04_PATH_RECP_REAL, TXT_PATH_RECP_FILE.Text);
            if (m_speaker.Check_Is_Error_File_Path_Validity(strRecpFile) == true) return;

            _Recp_Save(strRecpFile);
            _Recp_Update_List();
        }
        private void _Recp_Save(string strPathRecp)
        {
            // Pattern Matching File Verification
            string strPtrnFile = Path.Combine(imageView1.fm.param_path.i06_PATH_IMG_PTRN, TXT_PATH_PTRN_FILE.Text);
            if (m_speaker.Check_Ptrn_Is_Error_Teaching_File_Validity(strPtrnFile) == true){return;}

            // item 01 : get recipe file name 
            imageView1.fm.RECP_FILE = strPathRecp;

            // item 02 : get pattern mathcing file
            imageView1.fm.param_ptrn.PTRN_FILE = Path.GetFileName(strPtrnFile);

            // item 03 : get accept ratio
            if (TXT_PTRN_ACC_RATIO.Text == string.Empty) TXT_PTRN_ACC_RATIO.Text = "95.00";

            double fAcceptRatio = double.Parse(TXT_PTRN_ACC_RATIO.Text);
            TXT_PTRN_ACC_RATIO.Text = fAcceptRatio.ToString("F2");
            imageView1.fm.param_ptrn.ACC_RATIO = fAcceptRatio;

            imageView1.fm.param_optics.i09_MAKE_ZERO_FILE = LB_MAIN_FOCUS_PARAM.Text;
                        
            //*************************************************************************************
            // save recp
            CFigureManager.SerializedXml_Save(strPathRecp, imageView1.fm);

            // write log 
            strPathRecp = Path.GetFileName(strPathRecp);
            UC_LOG_VIEWER.WRITE_LOG("RECP Saved.", DEF_OPERATION.OPER_02_RECP);
            UC_LOG_VIEWER.WRITE_LOG("RECP : " + strPathRecp, DEF_OPERATION.OPER_02_RECP);

            // inform working finished.
            m_speaker.Inform_Finished_Save_Recp();

            // change to saved recp new recp
            _Recp_Change(Path.Combine(imageView1.fm.param_path.i04_PATH_RECP_REAL, strPathRecp));

        }
        private void _Recp_Change(string strFileName)
        {
            // do operation
            imageView1.fm = CFigureManager.SerializedXml_Load(strFileName);

            // replace imediately latest update for path configuration 170810
            imageView1.fm.param_path = this.param_path;

            PARAM_OPTICS param_optic/**/= imageView1.fm.param_optics.CopyTo();
            param_baseRecp = imageView1.fm.baseRecp.CopyTo();

            // only for Optics information From the SEQUENCE !!! 
            _Recp_Update_List();
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
                string strPtrnFullPath = Path.Combine(imageView1.fm.param_path.i06_PATH_IMG_PTRN, imageView1.fm.param_ptrn.PTRN_FILE);
                string strPtrnFileName = Path.GetFileName(strPtrnFullPath);
                TXT_PATH_PTRN_FILE.Text = strPtrnFileName;

                // item 04 : get pattern matching accept ratio
                TXT_PTRN_ACC_RATIO.Text = imageView1.fm.param_ptrn.ACC_RATIO.ToString("F2");

                // item 05 : get pattern matching origian teaching position 
                TXT_PTRN_POS_ORG_X.Text = imageView1.fm.param_ptrn.RC_TEMPLATE.X.ToString("F2");
                TXT_PTRN_POS_ORG_Y.Text = imageView1.fm.param_ptrn.RC_TEMPLATE.Y.ToString("F2");

                // item 06 : get image focus region 
                RectangleF rcFocus = imageView1.fm.RC_FOCUS;
                CControl.SetTextBoxFrom_RectangleF(rcFocus, TXT_FOCUS_POS_X, TXT_FOCUS_POS_Y, TXT_FOCUS_SZ_W, TXT_FOCUS_SZ_H);

                LB_MAIN_FOCUS_PARAM.Text = imageView1.fm.param_optics.i09_MAKE_ZERO_FILE;
                // update ptrn view
                _SetView_PTRN(strPtrnFullPath);

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString() + System.Environment.NewLine); }

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            imageView1.Refresh();

            //*************************************************************************************
            // WRITE LOG 
            string strRecpName = strFileName.Replace(imageView1.fm.param_path.i04_PATH_RECP_REAL + "\\", "");
            UC_LOG_VIEWER.WRITE_LOG(string.Format("RECP Loaded → {0}", strRecpName), DEF_OPERATION.OPER_02_RECP);

        }
        private void _Recp_Update_List()
        {
            //************************************************************************************
            // Get PTRN Files

            string[] arrRecpFiles = Directory.GetFiles(imageView1.fm.param_path.i04_PATH_RECP_REAL, "*.xml");

            LV_RECP.Items.Clear();

            LV_RECP.BeginUpdate();

            for (int i = 0; i < arrRecpFiles.Length; i++)
            {
                string strFile = Path.GetFileName(arrRecpFiles.ElementAt(i));


                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString("N0");
                lvi.SubItems.Add(strFile);

                string strFullPath = Path.Combine(imageView1.fm.param_path.i04_PATH_RECP_REAL, strFile);
                //System.IO.FileInfo file1 = new System.IO.FileInfo(strFullPath);
                //DateTime dtAccess = file1.LastAccessTime;

                DateTime dtAccess = File.GetLastWriteTime(strFullPath);
                lvi.SubItems.Add(Computer.GetTimeCodeString_YY_MM_DD_HH_MM_SS(dtAccess));

                LV_RECP.Items.Add(lvi);
            }

            LV_RECP.EndUpdate();
            _Set_Color_RECP_List();
        }
        private void _Set_Color_RECP_List()
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
        private void LV_RECP_SelectedIndexChanged(object sender, EventArgs e) { }
        private void LV_RECP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LV_RECP.FocusedItem == null) return;

            int nIndex = LV_RECP.FocusedItem.Index;

            string strFileName = LV_RECP.Items[nIndex].SubItems[1].Text;
            string strFullFilePath = Path.Combine(imageView1.fm.param_path.i04_PATH_RECP_REAL, strFileName);

            if (m_speaker.Check_RECP_Ask_Change_Recp() == true)
            {
                _Recp_Change(strFullFilePath);
                _Set_Color_RECP_List();
            }
            _Recp_Update_List();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
        }
        private void LV_RECP_ColumnClick(object sender, ColumnClickEventArgs e) { WrapperLV.SortData(LV_RECP, e.Column); }
        #endregion 

       #region MAIN MATCHING & PTRN CONTROL Related
        private void BTN_PTRN_MATCH_Click(object sender, EventArgs e)
        {
            if (TXT_PATH_PTRN_FILE.Text != null)
            {
                imageView1.VIEW_Set_Clear_DispObject();

                RectangleF rcTemplate = new RectangleF();
                double fMatchingRatio = 0;

                CFigureManager fm = imageView1.fm.Clone() as CFigureManager;
                Bitmap bmpView = imageView1.GetDisplay_Bmp();

                PointF ptTemplateCenter = _DO_PTRN_And_Get_TemplatePos(fm, bmpView, out rcTemplate, out fMatchingRatio);

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

                TXT_PTRN_POS_ORG_X.Text = ptTemplateCenter.X.ToString("F2");
                TXT_PTRN_POS_ORG_Y.Text = ptTemplateCenter.Y.ToString("F2");
            }
            else { MessageBox.Show("PTRN Data Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        private void PIC_FOCUS_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
        }
        private PointF _DO_PTRN_And_Get_TemplatePos(CFigureManager fm, Bitmap bmpSrc, out RectangleF rcTemplate, out double fMatchingRatio)
        {
            // initialize matching center position info.
            PointF ptTemplateCenter = new PointF(0, 0);

            rcTemplate = new RectangleF();
            fMatchingRatio = 0;

            // get the ptrn file name 
            string strPtrnFile = fm.param_ptrn.PTRN_FILE;

            // get all ptrn files
            string[] arrPtrnFiles_All = Computer.GetFileList_Itself(fm.param_path.i06_PATH_IMG_PTRN, "*.bmp");
            List<string> listFriends = new List<string>();

            // get base ptrn file name 
            string strPtrnOnlyName = Path.GetFileName(fm.param_ptrn.PTRN_FILE).ToUpper().Replace(".BMP", "");

            if (strPtrnOnlyName != "") // if this is not an empty recp for ptrn
            {
                // get the related ptrn files 
                for (int i = 0; i < arrPtrnFiles_All.Length; i++)
                {
                    string strFileCurrent = Path.GetFileName(arrPtrnFiles_All[i].ToUpper());
                    string strNameOnly = strFileCurrent.Replace(".BMP", "");

                    if (strNameOnly.IndexOf(strPtrnOnlyName) >= 0)
                    {
                        listFriends.Add(strFileCurrent);
                    }
                }
            }


            bool bMatchingSuccess = false;

            // do multiple matching process for each related ptrn files.
            for (int nPtrnLoop = 0; nPtrnLoop < listFriends.Count(); nPtrnLoop++)
            {
                // get the ptrn file one by one 
                string strPTRN_FullPath = Path.Combine(fm.param_path.i06_PATH_IMG_PTRN, listFriends.ElementAt(nPtrnLoop));

                // if currently successfull matching is not yet ??
                if (File.Exists(strPTRN_FullPath) == true && bMatchingSuccess == false)
                {
                    // get the bitmap for ptrn 
                    Bitmap bmpTemp = Bitmap.FromFile(strPTRN_FullPath) as Bitmap;
                    Bitmap bmpPTRN = new Bitmap(bmpTemp);
                    Bitmap bmpView = new Bitmap(bmpSrc);
                    bmpTemp.Dispose();

                    // in case of edge-based ptrn matching
                    if (fm.param_ptrn.BOOL_EDGE_BASED == true)
                    {
                        bmpPTRN = Computer._Ptrn_Preprocess_Edge(bmpPTRN);
                        bmpView = Computer._Ptrn_Preprocess_Edge(bmpView);
                    }

                    // initialize ptrn result structure
                    ResultPTRN ptrn = new ResultPTRN();

                    // find only one! and Set matching acceptance ratio 
                    wrapperCog.PARAM_NUM_TO_FIND = 1;
                    wrapperCog.PARAM_ACCEPT_RATIO = fm.param_ptrn.ACC_RATIO;

                    // is global searching??
                    if (fm.param_ptrn.BOOL_GLOBAL_SEARCHING == true)
                    {
                        wrapperCog.PARAM_RC_SEARCH = new RectangleF(0, 0, bmpView.Width, bmpView.Height);
                    }
                    //  is partial searching??
                    else if (fm.param_ptrn.BOOL_GLOBAL_SEARCHING == false)
                    {
                        wrapperCog.PARAM_RC_SEARCH = imageView1.fm.param_ptrn.RC_SEARCH_RGN;
                    }

                    wrapperCog.PARAM_PT_RELATIVE_ORIGIN = new PointF(0, 0);

                    // Do! pattern matching 
                    wrapperCog.Do_Ptrn(bmpView, bmpPTRN);

                    ptrn = wrapperCog.ptrnResult;

                    bmpPTRN.Dispose();
                    bmpView.Dispose();

                    // if matching succeed
                    if (ptrn.Count >= 1)
                    {
                        bMatchingSuccess = true;

                        // set data 
                        ptTemplateCenter = ptrn.resultList.ElementAt(0).ptCenter;
                        rcTemplate = ptrn.resultList.ElementAt(0).rc;
                        fMatchingRatio = ptrn.resultList.ElementAt(0).fScore * 100.0;
                        _PRINT_MSG("MR = " + fMatchingRatio.ToString("F2") + "%");
                        // and finish
                        break;
                    }
                }
                else { _PRINT_MSG("PTRN File Not Found."); }
            }

            if (bMatchingSuccess == false)
            {
                _PRINT_MSG("PTRN MATCHING Failed.");
            }

            return ptTemplateCenter;
        }
       #endregion


       #region DRAG_AND_DROP FILE LIST for Local Simulation
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
        private void LV_FILE_LIST_DragDrop(object sender, DragEventArgs e){PNL_SIMULATION_DragDrop(sender, e);}
        private void LV_FILE_LIST_DragEnter(object sender, DragEventArgs e){PNL_SIMULATION_DragEnter(sender, e);}

        bool bLV_File_Triggered = false;
        private void LV_FILE_LIST_SelectedIndexChanged(object sender, EventArgs e)
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
                    //Bitmap bmp = await staticImageLoadAsync(strSelected);

                    Bitmap bmpView = null;
                    if (File.Exists(strSelected) == true)
                    {
                        using (var bmpTemp = new Bitmap(strSelected))
                        {
                            bmpView = new Bitmap(bmpTemp);
                        }
                    }

                    imageView1.SetDisplay(bmpView);
                    imageView1.VIEW_Set_Clear_DispObject();
                    //imageView1.VIEW_SET_Mag_Origin();


                    RectangleF rcTemplate = new RectangleF();
                    double fMatchingRatio = 0;

                    CFigureManager fm = imageView1.fm.Clone() as CFigureManager;
                    PointF ptTemplateCenter = _DO_PTRN_And_Get_TemplatePos(fm, bmpView.Clone() as Bitmap, out rcTemplate, out fMatchingRatio);

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
        private void LV_FILE_LIST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled) return;

            e.Handled = true;
        }
        #endregion

        #region SIMULATION RELATED
        private void CHK_SIMUL_FOR_MULTI_POINTS_CheckedChanged(object sender, EventArgs e)
        {
            LB_SIMUL_FOR_MULTI_POINT.Visible = CHK_SIMUL_FOR_MULTI_POINT.Checked;
            TXT_SIMUL_FOR_MULTI_POINT_CYCLE.Visible = CHK_SIMUL_FOR_MULTI_POINT.Checked;
            TXT_SIMUL_FOR_MULTI_POINT_TARGET.Visible = CHK_SIMUL_FOR_MULTI_POINT.Checked;
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
                System.Threading.Thread.Sleep(500);

                if (m_nInterruptCount > 3)
                {
                    m_speaker.Inform_Interruption_occred();
                    m_nInterruptCount = 0;
                    _PRINT_MSG("USER CALLED URGENT INTERUPT.");
                    break;
                }

                if (nIndex == nCount) nIndex = 0;
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
                using (var bmpTemp = new Bitmap(strPath))
                {
                    bmp = new Bitmap(bmpTemp);
                }
            }
            return bmp;
        }
        #endregion

        #region MEASUREMENT RELATED
        private void BTN_MEASURE_Click(object sender, EventArgs e)
        {

            imageView1.VIEW_Set_Clear_DispObject();

            CFigureManager fm = imageView1.fm.Clone() as CFigureManager;

            if (fm.RECP_FILE == null)
            {
                MessageBox.Show("Invalid RECP File.\n Please Try Again With Correct RECP.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int nLoopTarget = LV_FILE_LIST.Items.Count;
            List<string> listItems = WrapperLV.GetAllItems_ByIndex(LV_FILE_LIST, 1);

            int imageW = 0;
            int imageH = 0;

            if (RDO_SIMUL_VIEW_ONLY.Checked == true) // image view measurement
            {
               #region VIEW ONLY
                Bitmap bmp = imageView1.GetDisplay_Bmp();
                byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp, ref imageW, ref imageH);

                CInspUnit iu = new CInspUnit();
                iu.AppendItem_Single(rawImage, imageW, imageH, imageView1.fm.Clone() as CFigureManager, 0);

                //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
                _Do_Measurement(iu, true,  0, 0, 0);
                //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
                
                rawImage = iu.GetImage_Raw_Last(out imageW, out imageH);
                imageView1.VIEW_Set_Clear_DispObject();
                imageView1.SetDisplay(rawImage, imageW, imageH);
                imageView1.DrawPoints(iu.listDispEdgePoints);
                imageView1.Refresh();

                // display data to the message window 
                for (int i = 0; i < iu.listInspResult.Count; i++) { _PRINT_MSG(iu.listInspResult.ElementAt(i)); }


                m_nInterruptCount = 0;
                #endregion
            }
            else if (RDO_SIMUL_LOCAL_FILES.Checked) // Sequential measurement
            {
                #region FILE LIST SIMULATION
                if (CHK_SIMUL_FOR_MULTI_POINT.Checked == true)
                {
                    int nCycle = 0; int.TryParse(TXT_SIMUL_FOR_MULTI_POINT_CYCLE.Text, out nCycle);
                    int nTarget = 0;  int.TryParse(TXT_SIMUL_FOR_MULTI_POINT_TARGET.Text, out nTarget);

                    string msg = string.Format("Simulation For Multiple Points are Activated. \n Please Ensure Parameters.\n[Cycle {0},Target {1}].\n Do You Want To Proceed?", nCycle, nTarget);

                    if (MessageBox.Show(msg, "Parameter Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        report.SetInit(nCycle, nTarget);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    report.SetInit(0, 0);
                }

                _ctrlProgressBarSetInit(nLoopTarget);

                System.Threading.Thread thr = new System.Threading.Thread(delegate()
                {

                    for (int nLoopIndex = 0; nLoopIndex < nLoopTarget; nLoopIndex++)
                    {
                        System.Threading.Thread.Sleep(20);

                        if (report.INTERRUPT == true)
                        {
                            MessageBox.Show("Abnormal Interruption Occured.");
                            m_nInterruptCount = 0;
                            break;
                        }

                        //string strFileName = LV_FILE_LIST.Items[nLoopIndex].SubItems[1].Text;
                        string strFileName = listItems.ElementAt(nLoopIndex);

                        if (Computer.isValidImageFile(strFileName) == false)
                        {
                            MessageBox.Show("Unsupported File.", "INVALID FILE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }

                        if (File.Exists(strFileName) == true)
                        {
                            Bitmap bmpTemp = Bitmap.FromFile(strFileName) as Bitmap;
                            Bitmap bmp = new Bitmap(bmpTemp);
                            bmpTemp.Dispose();

                            byte[] rawImage = Computer.HC_CONV_Bmp2Raw(bmp, ref imageW, ref imageH);

                            UC_LOG_VIEWER.WRITE_LOG(string.Format("SEQ {0}/{1} - {2} ",nLoopIndex, nLoopTarget, Path.GetFileName(strFileName)), DEF_OPERATION.OPER_05_MEAS);

                            CInspUnit iu = new CInspUnit();
                            iu.AppendItem_Single(rawImage, imageW, imageH, imageView1.fm.Clone() as CFigureManager, 0);

                            // update the most recently used recp file name
                            this.param_program.i00_previous_recp = TXT_PATH_RECP_FILE.Text;
                            _INI_SAVE_Program();

                            //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
                            _Do_Measurement(iu, true,  0, 0, 0);
                            //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●

                            this.UIThread(delegate 
                            {
                                //rawImage = iu.GetImage_Raw_Last(out imageW, out imageH);

                                if (iu.IsPreprocessed() == true && m_hacker.BOOL_SHOW_IMAGE_PROCESS == true)
                                {
                                    rawImage = iu.GetImage_prep_Last(out imageW, out imageH);
                                }

                                imageView1.VIEW_Set_Clear_DispObject();
                                imageView1.SetDisplay(rawImage, imageW, imageH);
                                imageView1.DrawPoints(iu.listDispEdgePoints);
                                imageView1.Refresh();

                                // display data to the message window 
                                for (int i = 0; i < iu.listInspResult.Count; i++) { _PRINT_MSG(iu.listInspResult.ElementAt(i)); }

                            });
                            
                            System.Threading.Thread.Sleep(20);

                        }
                        else
                        {
                            MessageBox.Show("File Not Found.\n" + strFileName, "File Existance Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        _ctrlProgressBarUpdate(nLoopIndex);

                    }

                    _ctrlProgressBarUpdate(nLoopTarget);
                    UC_LOG_VIEWER.WRITE_LOG(string.Format("MEASUREMENT FINISHED -: Totally {0}", nLoopTarget), DEF_OPERATION.OPER_05_MEAS);

                    report.INFO_PTRN = fm.param_ptrn.PTRN_FILE;
                    report.INFO_RECIPE = fm.RECP_FILE;
                    report.INFO_PIXEL_X = fm.param_optics.REAL_SCALE_PIXEL_RES;

                    //*************************************************************************
                    // make report file

                    CMeasureReport._WriteMeasurementData(report, fm.param_path.i02_PATH_DATA_DUMP, CHK_MEASURE_DUMP.Checked);
                });
                thr.IsBackground = true;
                thr.Start();

            #endregion
            }
            else if( RDO_SIMUL_STATIC.Checked ) // 171127
            {
                int nTOTAL_REPEAT = 10;

                proc_IterativeRun.SetInit();
                proc_IterativeRun.IS_STATIC_RUN = true;

                // fist grab trying.
                for (int i = 0; i < nTOTAL_REPEAT; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    _PRINT_MSG("GRAB IMAGE " + string.Format(" [{0}/{1}]", 1+i, nTOTAL_REPEAT));
                    MsgIPC.Send(IPC_ID.MC_IMAGE_GRAB_REQ, "", 5510);
                }
            }
            
        }
        private void _DoIterativeExperiments()
        {
             int nTOTAL_REPEAT = proc_IterativeRun.STACK_SIZE;
             
             report.SetInit(0, 0);
             _ctrlProgressBarSetInit(nTOTAL_REPEAT);

            for (int i = 0; i < nTOTAL_REPEAT; i++)
            {
                CInspUnit iu = proc_IterativeRun.listSequence.Pop();

                iu.nStackCount = proc_IterativeRun.listSequence.Count;
                if (i < nTOTAL_REPEAT)
                {
                    this.UIThread(delegate  { _PRINT_MSG("SEQUENTIAL IMAGE " + string.Format(" [{0}/{1}]", 1 + i, nTOTAL_REPEAT)); });

                    //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
                    //_Do_Measurement(iu, true, 0, 0, 0);
                    //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
                    CThreadProc_Insp.ThreadFinishedEventArgs temp = new CThreadProc_Insp.ThreadFinishedEventArgs();

                    System.Threading.Thread thr = new System.Threading.Thread(delegate() { proc_inspection.ThreadCall_Inspection(wrapperCog, iu, report, true, 0, 0, i, temp); });
                    thr.IsBackground = true;
                    thr.Start();

                    //int imageW = 0; int imageH = 0;
                    //byte[] rawImage = iu.GetImage_Raw_Last(out imageW, out imageH);
                    //
                    //if (m_hacker.BOOL_SHOW_IMAGE_PROCESS == true && iu.IsPreprocessed() == true)
                    //{
                    //    rawImage = iu.GetImage_prep_Last(out imageW, out imageH);
                    //}
                    //
                    //imageView1.VIEW_Set_Clear_DispObject();
                    //imageView1.SetDisplay(rawImage, imageW, imageH);
                    //imageView1.DrawPoints(iu.listDispEdgePoints);
                    //imageView1.Refresh();
                    //
                    //// display data to the message window 
                    //for (int nItem = 0; nItem < iu.listInspResult.Count; nItem++) 
                    //{
                    //    _PRINT_MSG(iu.listInspResult.ElementAt(nItem)); 
                    //}
                    //
                    //System.Threading.Thread.Sleep(20);


                    _ctrlProgressBarUpdate(i);
                }

                if (proc_IterativeRun.STACK_SIZE == 0)
                {
                    _ctrlProgressBarUpdate(nTOTAL_REPEAT);
                    UC_LOG_VIEWER.WRITE_LOG(string.Format("MEASUREMENT FINISHED -: Totally {0}", nTOTAL_REPEAT), DEF_OPERATION.OPER_05_MEAS);

                    report.INFO_PTRN = iu.fm.param_ptrn.PTRN_FILE;
                    report.INFO_RECIPE = iu.fm.RECP_FILE;
                    report.INFO_PIXEL_X = iu.fm.param_optics.REAL_SCALE_PIXEL_RES;

                    //*************************************************************************
                    // make report file

                    CMeasureReport._WriteMeasurementData(report, iu.fm.param_path.i02_PATH_DATA_DUMP, CHK_MEASURE_DUMP.Checked);
                }

            }
            

         }

        int?[] nARR_RESULT = new int?[10];

        private void EventThreadFinished_Inspection(object sender, CThreadProc_Insp.ThreadFinishedEventArgs e)
        {
            System.Threading.Thread.Sleep(50);

            this.UIThread(delegate
            {
                System.Threading.Thread.Sleep(10);
                imageView1.VIEW_Set_Clear_DispObject();
                System.Threading.Thread.Sleep(20);

                PointF ptDelta = e.fm.PT_PTRN_DELTA;

                if (e.PTRN_fMAtchingRatio == 0) { _SetColor_PtrnResul(0); } else { _SetColor_PtrnResul(1); }

                System.Threading.Thread.Sleep(20);
                PIC_FOCUS.Image = e.bmpFocus;
                System.Threading.Thread.Sleep(20);

                string stamp = string.Format("{0}-{1}-{2} ", e.IDX_Cam, e.IDX_Cycle, e.IDX_Point);
                _PRINT_MSG(stamp + e.PTRN_MSG);


                int imageW = e.bmpView.Width;
                int imageH = e.bmpView.Height;
                byte[] rawImage = null;
                if (e.iu.IsPreprocessed() == true && m_hacker.BOOL_SHOW_IMAGE_PROCESS == true)
                {
                    rawImage = e.iu.GetImage_prep_Last(out imageW, out imageH);
                    imageView1.SetDisplay(rawImage, imageW, imageH);
                }
                else
                {
                    imageView1.SetDisplay(e.bmpView);
                }


                imageView1.DrawPatternMathcing(e.PTRN_ptTemplateCenter, e.PTRN_rcTemplate);
                imageView1.DrawPoints(e.iu.listDispEdgePoints);
                imageView1.Refresh();
                // display data to the message window 
                for (int i = 0; i < e.iu.listInspResult.Count; i++) { _PRINT_MSG(e.iu.listInspResult.ElementAt(i)); }

                System.Threading.Thread.Sleep(300);

                int nCurr = PROG_RUN.Value + 1;
                _ctrlProgressBarUpdate(nCurr);

            });

            nARR_RESULT[e.IDX_Point] = 1;

            int nResCount = nARR_RESULT.Count(element => element > 1);
            if (nResCount == 10) 
            {
                int nCurr = PROG_RUN.Maximum;
                UC_LOG_VIEWER.WRITE_LOG(string.Format("MEASUREMENT FINISHED -: Totally {0}", nCurr), DEF_OPERATION.OPER_05_MEAS);
                report.INFO_PTRN = e.iu.fm.param_ptrn.PTRN_FILE;
                report.INFO_RECIPE = e.iu.fm.RECP_FILE;
                report.INFO_PIXEL_X = e.iu.fm.param_optics.REAL_SCALE_PIXEL_RES;

                //*************************************************************************
                // make report file

                CMeasureReport._WriteMeasurementData(report, e.iu.fm.param_path.i02_PATH_DATA_DUMP, CHK_MEASURE_DUMP.Checked);
            }
        }
        private double  ThrProc_01_FocusRegionProcess(PointF ptTemplateCenter, byte[] rawImage, int imageW, int imageH, out int [] nHisto)
        {
            //*************************************************************************************
            // Set histogram
            nHisto = new int[256];
            Array.Clear(nHisto, 0, nHisto.Length);

            //*************************************************************************************

            if (ptTemplateCenter.X > 0 && ptTemplateCenter.Y > 0)
            {
                imageView1.fm.SetRelativeMovemntFocusRect();
            }
            else // matching failed == position(x,y) is  that the result placed on minus croodinates
            {
                if (m_hacker.BOOL_USE_LEAVE_HISTORY_ERROR_PTRN == true)
                {
                    // Setup Daily Directory
                    string strPathDir = Path.Combine(imageView1.fm.param_path.i08_PATH_HIST_PTRN, Computer.GetTimeCode4Save_YYYY_MM_DD());
                    Computer.EnsureFolderExsistance(strPathDir);

                    string strTimeCode = Computer.GetTimeCode4Save_HH_MM_SS_MMM();
                    imageView2.ThreadCall_SaveImage(Path.Combine(strPathDir, strTimeCode + "_ERROR.BMP"), rawImage, imageW, imageH);
                }
            }

            double fFocusValue = 0;

            Rectangle rcFocus = Rectangle.Round(imageView1.fm.RC_FOCUS);
            Bitmap bmp = null;

            if( rcFocus.Width == 0 && rcFocus.Height == 0)
            {
                // in order to avoid empty show~
                rcFocus = new Rectangle((int)(imageW *0.5 - 50), (int)(imageH *0.5 - 50), 100, 100);
                imageView1.fm.RC_FOCUS = rcFocus;
            }

            if (rcFocus.X > 0 && rcFocus.Y > 0 && rcFocus.Width > 0 && rcFocus.Height > 0)
            {
                byte[] rawFocus = Computer.HC_CropImage(rawImage, imageW, imageH, rcFocus);

                byte[] gradient = Computer.HC_TRANS_GradientImage(rawFocus, rcFocus.Width, rcFocus.Height);
                double[] fGradient = gradient.Select(element => (double)element).ToArray();
                fFocusValue = fGradient.Average();

                nHisto = Computer.HC_HISTO_GetHistogram(rawImage, 256);
                bmp = Computer.HC_CONV_Byte2Bmp(rawFocus, rcFocus.Width, rcFocus.Height);
            }
            else
            {
                bmp = new Bitmap(200, 200);
            }

            this.UIThread(delegate { PIC_FOCUS.Image = bmp.Clone() as Bitmap; });

            if (m_hacker.BOOL_USE_SAVE_FOCUS_REGION == true)
            {
                string strPathDir = "c:\\" + Computer.GetTimeCode4Save_YYYY_MM_DD();
                Computer.EnsureFolderExsistance(strPathDir);

                string strTimeCode = Computer.GetTimeCode4Save_HH_MM_SS_MMM();

                Bitmap bmpFocus = bmp.Clone() as Bitmap;
                imageView2.ThreadCall_SaveImage(Path.Combine(strPathDir, strTimeCode + "_FOCUS.BMP"), bmpFocus);

                //bmpFocus.Dispose();

                System.Threading.Thread.Sleep(50);

            }
            
            return fFocusValue;
        }
        private void _Do_Measurement(CInspUnit iu, bool bSimulation, int nCamNo, int nIdxCycle, int nIdxPoint)
        {
            CFigureManager fm = iu.fm.Clone() as CFigureManager;

            int imageW = 0;int imageH = 0;
            byte[] rawView = iu.GetImage_Raw_First(out imageW, out imageH);
            Bitmap bmpView = iu.GetImage_Bitmap_First();

            report.IncreseSequenceIndex();

            //*************************************************************************************
            // Pattern matching
            //*************************************************************************************

            RectangleF rcTemplate/*****/ = new RectangleF();
            double fMatchingRatio = 0;

            PointF ptTemplateCenter = _DO_PTRN_And_Get_TemplatePos(fm, bmpView, out rcTemplate, out fMatchingRatio);
            fm.SetPtrnDelta(rcTemplate.X, rcTemplate.Y);

            PointF ptDelta = fm.PT_PTRN_DELTA;
            imageView1.DrawPatternMathcing(ptTemplateCenter, rcTemplate);

            report.m_listMatchPoint.Add(ptTemplateCenter);
            if (fMatchingRatio == 0) { _SetColor_PtrnResul(0); } else { _SetColor_PtrnResul(1); }

            //*************************************************************************************
            // Focus Region Processing
            //*************************************************************************************

            int[] nHisto = new int[256];
            double fFocusValue = ThrProc_01_FocusRegionProcess(ptTemplateCenter, rawView, imageW, imageH, out nHisto);
            report.AddFocusMag(fFocusValue);
            report.AddHistogram(nHisto);
            iu.mi.Focus = fFocusValue;

            //*************************************************************************************
            // user defined pre-processing 170809
            //*************************************************************************************

            iu.Proc_00_DoPreProcess();

            PointF P1 = new PointF(0, 0);
            PointF P2 = new PointF(0, 0);

            // just scale up to real unit. because optic parameter for pixel resolution is based-on stage unit ( mm )
            // remark description date : 170809
            // SEQ RES * 1000 == real scale pixel resolution
            double PIXEL_RES = fm.param_optics.REAL_SCALE_PIXEL_RES; 

            //*************************************************************************************
            // CIRCLE 
            //*************************************************************************************

            if( fm.COUNT_PAIR_RCT != 0) _INSP_RC(fm, ptDelta, iu);
            if( fm.COUNT_PAIR_CIR != 0) _INSP_CIR(fm, ptDelta, iu);
            if (fm.COUNT_PAIR_OVL != 0) _INSP_OL(fm, ptDelta, iu);
            if (fm.COUNT_MIXED_RC != 0) _INSP_MRC(fm, ptDelta, iu);
            if (fm.COUNT_MIXED_CC != 0) _INSP_MCC(fm, ptDelta, iu);
            if (fm.COUNT_MIXED_RCC != 0) _INSP_MRCC(fm, ptDelta, iu);

            return ;
        }
       
        private void _INSP_MCC(CFigureManager fm, PointF ptDelta, CInspUnit iu)
        {
            List<PointF> listEdges_FEX = new List<PointF>(); List<PointF> listEdges_FMD = new List<PointF>(); List<PointF> listEdges_FIN = new List<PointF>();
            List<PointF> listEdges_SEX = new List<PointF>(); List<PointF> listEdges_SMD = new List<PointF>(); List<PointF> listEdges_SIN = new List<PointF>();

            PointF P1 = new PointF(0, 0);
            PointF P2 = new PointF(0, 0);

            int imageW = iu.imageW;
            int imageH = iu.imageH;

           #region CIRCLE - NOrmal case
            for (int i = 0; i < fm.COUNT_MIXED_CC; i++)
            {
                // get element ( to modify name w/h , make duplica)
                CMeasureMixedCC single = fm.ElementAt_MCC(i).CopyTo();
                string strOwnName = single.NICKNAME;

                // set relative croodinate
                single.SetRelativeMovement(ptDelta);
                single.PIXEL_RES = fm.param_optics.REAL_SCALE_PIXEL_RES;

                List<double> listDistance = new List<double>();
                RectangleF rcVoid = new RectangleF();

                double fDistance = 0;

                for (int nImage = 0; nImage < iu.listImage_prep.Count; nImage++)
                {
                    byte[] rawTargetImage = iu.GetImage_Prep_by_index(nImage);

                    // get measure data
                    fDistance = single.MeasureData(rawTargetImage, imageW, imageH, 
                        ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, 
                        ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN,
                        out P1, out P2, out rcVoid, out rcVoid);
                    
                    listDistance.Add(fDistance);
                }

                if (fm.param_optics.i08_MULTI_SHOT_VALUE_AVG == true || listDistance.Count < 3)
                {
                    fDistance = listDistance.Average();
                }
                else
                {
                    fDistance = listDistance.ElementAt((int)Math.Floor(listDistance.Count / 2.0));
                }

                // take inspection result 

                string strInspRes = string.Empty;

                report.AddResult_FIG(IFX_FIGURE.MIXED_CC, single, fDistance);

                iu.InsertTransResultData(fDistance);
                iu.InsertDispResult_Circle(single.NICKNAME, fDistance);
                iu.Insert_DispEdgePoints(listEdges_FEX, listEdges_FMD, listEdges_FIN);
                iu.Insert_DispEdgePoints(listEdges_SEX, listEdges_SMD, listEdges_SIN);

                iu.Insert_DispMeasurePoint(P1, P2);



            }
            #endregion
        }
        private void _INSP_MRCC(CFigureManager fm, PointF ptDelta, CInspUnit iu)
        {
            List<PointF> listEdges_FEX = new List<PointF>(); List<PointF> listEdges_FMD = new List<PointF>(); List<PointF> listEdges_FIN = new List<PointF>();
            List<PointF> listEdges_SEX = new List<PointF>(); List<PointF> listEdges_SMD = new List<PointF>(); List<PointF> listEdges_SIN = new List<PointF>();

            PointF P1 = new PointF(0, 0);
            PointF P2 = new PointF(0, 0);

            int imageW = iu.imageW;
            int imageH = iu.imageH;

            #region CIRCLE - NOrmal case
            for (int i = 0; i < fm.COUNT_MIXED_RCC; i++)
            {
                // get element ( to modify name w/h , make duplica)
                CMeasureMixedRCC single = fm.ElementAt_MRCC(i).CopyTo();
                string strOwnName = single.NICKNAME;

                // set relative croodinate
                single.SetRelativeMovement(ptDelta);
                single.PIXEL_RES = fm.param_optics.REAL_SCALE_PIXEL_RES;

                List<double> listDistance = new List<double>();
                RectangleF rcVoid = new RectangleF();

                double fDistance = 0;

                for (int nImage = 0; nImage < iu.listImage_prep.Count; nImage++)
                {
                    byte[] rawTargetImage = iu.GetImage_Prep_by_index(nImage);

                    // get measure data
                    fDistance = single.MeasureData(rawTargetImage, imageW, imageH,
                        ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN,
                        ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN,
                        out P1, out P2, out rcVoid, out rcVoid);

                    listDistance.Add(fDistance);
                }

                if (fm.param_optics.i08_MULTI_SHOT_VALUE_AVG == true || listDistance.Count < 3)
                {
                    fDistance = listDistance.Average();
                }
                else
                {
                    fDistance = listDistance.ElementAt((int)Math.Floor(listDistance.Count / 2.0));
                }

                // take inspection result 

                string strInspRes = string.Empty;

                report.AddResult_FIG(IFX_FIGURE.MIXED_RCC, single, fDistance);

                iu.InsertTransResultData(fDistance);
                iu.InsertDispResult_Circle(single.NICKNAME, fDistance);
                iu.Insert_DispEdgePoints(listEdges_FEX, listEdges_FMD, listEdges_FIN);
                iu.Insert_DispEdgePoints(listEdges_SEX, listEdges_SMD, listEdges_SIN);

                iu.Insert_DispMeasurePoint(P1, P2);
            }
            #endregion
        }
        private void _INSP_CIR(CFigureManager fm, PointF ptDelta, CInspUnit iu)
        {
            List<PointF> listEdges_FEX = new List<PointF>(); List<PointF> listEdges_FMD = new List<PointF>(); List<PointF> listEdges_FIN = new List<PointF>();
            List<PointF> listEdges_SEX = new List<PointF>(); List<PointF> listEdges_SMD = new List<PointF>(); List<PointF> listEdges_SIN = new List<PointF>();

            PointF P1 = new PointF(0, 0);
            PointF P2 = new PointF(0, 0);

            int imageW = iu.imageW;
            int imageH = iu.imageH;

            #region CIRCLE - NOrmal case
            for (int i = 0; i < fm.COUNT_PAIR_CIR; i++)
            {

                // get element ( to modify name w/h , make duplica)
                CMeasurePairCir single = fm.ElementAt_PairCir(i).CopyTo();
                string strOwnName = single.NICKNAME;

                // set relative croodinate
                single.SetRelativeMovement(ptDelta);
                single.PIXEL_RES = fm.param_optics.REAL_SCALE_PIXEL_RES; 

                RectangleF rcEstimated = new RectangleF();
                RectangleF rcMeasured = new RectangleF();

                List<double> listW = new List<double>();
                List<double> listH = new List<double>();

                double fDistanceW = 0;
                double fDistanceH = 0;

                for (int nImage = 0; nImage < iu.listImage_prep.Count; nImage++)
                {
                    byte[] rawTargetImage = iu.GetImage_Prep_by_index(nImage);

                    // get measure data
                    single.MeasureData(rawTargetImage, imageW, imageH, ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN, ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN,
                        out P1, out P2, out rcEstimated, out rcMeasured);

                    if (single.param_02_BOOL_TREAT_AS_ELLIPSE == false)
                    {
                        listW.Add(rcMeasured.Width * single.PIXEL_RES);
                    }
                    else if (single.param_02_BOOL_TREAT_AS_ELLIPSE == true)
                    {
                        listW.Add(rcMeasured.Width * single.PIXEL_RES);
                        listH.Add(rcMeasured.Height * single.PIXEL_RES);
                    }
                }

                if (fm.param_optics.i08_MULTI_SHOT_VALUE_AVG == true || listW.Count < 3)
                {
                    fDistanceW = listW.Average();
                    if (single.param_02_BOOL_TREAT_AS_ELLIPSE == true) { fDistanceH = listH.Average(); }
                }
                else
                {
                    fDistanceW = listW.ElementAt((int)Math.Floor(listW.Count / 2.0));
                    if (single.param_02_BOOL_TREAT_AS_ELLIPSE == true) { fDistanceH = listH.ElementAt((int)Math.Floor(listH.Count / 2.0)); }
                }

                // take inspection result 

                string strInspRes = string.Empty;

                if (single.param_02_BOOL_TREAT_AS_ELLIPSE == false)
                {
                    iu.InsertDispResult_Circle(single.NICKNAME, fDistanceW);
                    // leave inspection result 
                    report.AddResult_FIG(IFX_FIGURE.PAIR_CIR, single, fDistanceW);
                }
                else if (single.param_02_BOOL_TREAT_AS_ELLIPSE == true)
                {
                    iu.InsertDispResult_Ellipse(single.NICKNAME, fDistanceW, fDistanceH);
                    // leave inspection result 
                    single.NICKNAME = strOwnName + "_W";
                    report.AddResult_FIG(IFX_FIGURE.PAIR_CIR, single, fDistanceW);
                    single.NICKNAME = strOwnName + "_H";
                    report.AddResult_FIG(IFX_FIGURE.PAIR_CIR, single, fDistanceH);
                }

                iu.InsertTransResultData(fDistanceW);
                iu.Insert_DispEdgePoints(listEdges_FEX, listEdges_FMD, listEdges_FIN);

                iu.Insert_DispMeasurePoint(P1, P2);
                iu.Insert_DispMeasurePoint(CPoint.GetMidPoint(P1, P2));

                imageView1.DrawCircle(rcEstimated, Color.Cyan, 4);

                Rectangle rcMeasuredCircle = Rectangle.Round(rcMeasured);
                iu.Insert_DispMeasurePoint(CRect.GetCenter(rcMeasured));

                iu.Insert_DispDirectionalEdgePointsForCircle(single.param_06_EdgePos, listEdges_FEX, listEdges_FMD, listEdges_FIN);

            }
            #endregion
        }
        private void _INSP_OL(CFigureManager fm, PointF ptDelta, CInspUnit iu)
        {

            //*************************************************************************************
            // Rectangle
            //*************************************************************************************

            List<PointF> listEdgesHOR_EX = new List<PointF>(); List<PointF> listEdgesHOR_MD = new List<PointF>(); List<PointF> listEdgesHOR_IN = new List<PointF>();
            List<PointF> listEdgesVER_EX = new List<PointF>(); List<PointF> listEdgesVER_MD = new List<PointF>(); List<PointF> listEdgesVER_IN = new List<PointF>();

            int imageW = iu.imageW;
            int imageH = iu.imageH;

            List<double> list_Distance = new List<double>();

            PointF P1 = new PointF(0, 0);
            PointF P2 = new PointF(0, 0);

            #region OVL - Normal Case
            for (int i = 0; i < fm.COUNT_PAIR_OVL; i++)
            {
                List<PointF> listPoints = new List<PointF>();

                // get element 
                CMeasurePairOvl single = fm.ElementAt_PairOvl(i);

                single.PIXEL_RES = fm.param_optics.REAL_SCALE_PIXEL_RES;
                single.SetRelativeMovement(ptDelta);

                // take measurement result 
                double fOL_X = 0; double fOL_Y = 0;

                List<double> listX = new List<double>();
                List<double> listY = new List<double>();

                for (int nImage = 0; nImage < iu.listImage_prep.Count; nImage++)
                {
                    byte[] rawTargetImage = iu.GetImage_Prep_by_index(nImage);

                    single.rape_MotherFucker(rawTargetImage, imageW, imageH,
                        ref listEdgesHOR_EX, ref listEdgesHOR_MD, ref listEdgesHOR_IN,
                        ref listEdgesVER_EX, ref listEdgesVER_MD, ref listEdgesVER_IN,
                        ref listPoints,
                        out fOL_X, out fOL_Y);

                    listX.Add(fOL_X);
                    listY.Add(fOL_Y);
                }

                if (fm.param_optics.i08_MULTI_SHOT_VALUE_AVG == true || listX.Count < 3)
                {
                    fOL_X = listX.Average();
                    fOL_Y = listY.Average();
                }
                else
                {
                    fOL_X = listX.ElementAt((int)Math.Floor(listX.Count / 2.0));
                    fOL_Y = listY.ElementAt((int)Math.Floor(listY.Count / 2.0));
                }

                iu.InsertDispResult_Overlay(single.NICKNAME, fOL_X, fOL_Y);
                iu.InsertTransResultData(fOL_X, fOL_Y);
                report.AddResult_OVL(IFX_FIGURE.PAIR_OVL, fOL_X, fOL_Y, single);

                iu.Insert_DispEdgePoints(listEdgesHOR_EX, listEdgesHOR_MD, listEdgesHOR_IN);
                iu.Insert_DispEdgePoints(listEdgesVER_EX, listEdgesVER_MD, listEdgesVER_IN);
                iu.Insert_DispMeasurePoint(listPoints);
            }
            #endregion
        }
        private void _INSP_RC(CFigureManager fm, PointF ptDelta, CInspUnit iu)
        {
            //*************************************************************************************
            // Rectangle
            //*************************************************************************************

            List<PointF> listEdges_FEX = new List<PointF>(); List<PointF> listEdges_FMD = new List<PointF>(); List<PointF> listEdges_FIN = new List<PointF>();
            List<PointF> listEdges_SEX = new List<PointF>(); List<PointF> listEdges_SMD = new List<PointF>(); List<PointF> listEdges_SIN = new List<PointF>();

            int imageW = iu.imageW;
            int imageH = iu.imageH;

            List<double> list_Distance = new List<double>();

            PointF P1 = new PointF(0, 0);
            PointF P2 = new PointF(0, 0);

            //*************************************************************************************
            // Rectangle
            //*************************************************************************************

            #region Rectangle - Normal Case

            for (int i = 0; i < fm.COUNT_PAIR_RCT; i++)
            {
                // get element 
                CMeasurePairRct single = fm.ElementAt_PairRct(i);

                single.PIXEL_RES = fm.param_optics.REAL_SCALE_PIXEL_RES; 
                single.SetRelativeMovement(ptDelta);

                RectangleF rcEstimaged = new RectangleF();
                RectangleF rcMeasured = new RectangleF();

                List<double> listD = new List<double>();
                double fDistance = 0;

                for (int nImage = 0; nImage < iu.listImage_prep.Count; nImage++)
                {
                    byte[] rawTargetImage = iu.GetImage_Prep_by_index(nImage);

                    // get measure data 
                    fDistance = single.MeasureData(rawTargetImage, imageW, imageH,
                        ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN,
                        ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, out P1, out P2, out rcEstimaged, out rcMeasured);

                    listD.Add(fDistance);

                    if (fDistance > 30)
                    {
                        string strPath = "c:\\" + Computer.GetTimeCode4Save_HH_MM_SS_MMM() + ".Bmp";
                        imageView2.ThreadCall_SaveImage(strPath, rawTargetImage, imageW, imageH);
                    }
                }

                if (fm.param_optics.i08_MULTI_SHOT_VALUE_AVG == true || listD.Count < 3)
                {
                    fDistance = listD.Average();
                }
                else
                {
                    fDistance = listD.ElementAt((int)Math.Floor(listD.Count / 2.0));
                }

               
                iu.InsertDispResult_Rectangle(single.NICKNAME, fDistance);
                iu.InsertTransResultData(fDistance);
                report.AddResult_FIG(IFX_FIGURE.PAIR_RCT, single, fDistance);


                iu.Insert_DispEdgePoints(listEdges_FEX, listEdges_FMD, listEdges_FIN);
                iu.Insert_DispEdgePoints(listEdges_SEX, listEdges_SMD, listEdges_SIN);
                iu.Insert_DispMeasurePoint(P1);
                iu.Insert_DispMeasurePoint(P2);

            }
            #endregion
        }
        private void _INSP_MRC(CFigureManager fm, PointF ptDelta, CInspUnit iu)
        {
            //*************************************************************************************
            // Rectangle
            //*************************************************************************************

            List<PointF> listEdges_FEX = new List<PointF>(); List<PointF> listEdges_FMD = new List<PointF>(); List<PointF> listEdges_FIN = new List<PointF>();
            List<PointF> listEdges_SEX = new List<PointF>(); List<PointF> listEdges_SMD = new List<PointF>(); List<PointF> listEdges_SIN = new List<PointF>();

            int imageW = iu.imageW;
            int imageH = iu.imageH;

            List<double> list_Distance = new List<double>();

           #region Rectangle - Normal Case

            for (int i = 0; i < fm.COUNT_MIXED_RC; i++)
            {
                CMeasureMixedRC single = fm.ElementAt_MRC(i);
                single.PIXEL_RES = fm.param_optics.REAL_SCALE_PIXEL_RES;
                single.SetRelativeMovement(ptDelta);

                list_Distance.Clear();

                RectangleF rcEstimaged = new RectangleF();
                RectangleF rcMeasured = new RectangleF();

                PointF P1 = new PointF();
                PointF P2 = new PointF();

                double fDistance = 0;

                for (int nImage = 0; nImage < iu.listImage_prep.Count; nImage++)
                {
                    byte[] rawTargetImage = iu.GetImage_Prep_by_index(nImage);

                    fDistance = single.MeasureData(rawTargetImage, imageW, imageH,
                        ref listEdges_FEX, ref listEdges_FMD, ref listEdges_FIN,
                        ref listEdges_SEX, ref listEdges_SMD, ref listEdges_SIN, out P1, out P2, out rcEstimaged, out rcMeasured);

                    list_Distance.Add(fDistance);
                }

                if (fm.param_optics.i08_MULTI_SHOT_VALUE_AVG == true || list_Distance.Count < 3)
                {
                    fDistance = list_Distance.Average();
                }
                else
                {
                    fDistance = list_Distance.ElementAt((int)Math.Floor(list_Distance.Count / 2.0));
                }

                iu.InsertDispResult_Rectangle(single.NICKNAME, fDistance);
                iu.InsertTransResultData(fDistance);
                report.AddResult_FIG(IFX_FIGURE.MIXED_RC, single, fDistance);

                iu.Insert_DispEdgePoints(listEdges_FEX, listEdges_FMD, listEdges_FIN);
                iu.Insert_DispEdgePoints(listEdges_SEX, listEdges_SMD, listEdges_SIN);
                iu.Insert_DispMeasurePoint(P1, P2);

            }
           #endregion
        }
        #endregion


       #region WORKING TRAY - TOOL STRIP RELATED
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
        #endregion
        
       #region MAIN MENU BUTTON EVENTS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
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
            if (dlgHistP.SetParam(param_path) == true){dlgHistP.ShowDialog();}
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
            if (dlgConfig.SetParam(imageView1.fm.param_path) == true) { dlgConfig.ShowDialog(); }
        }
        private void BTN_MENU_STATISTICS_Click(object sender, EventArgs e)
        {
            if (dlgSPC.SetParam(imageView1.fm.param_path) == true) { dlgSPC.ShowDialog(); }
        }
        private void BTN_MENU_CREATE_RECP_Click(object sender, EventArgs e)
        {
            // check user selection for the create recp
            if (m_speaker.Check_And_Ask_Further_Do_Create_Recp() == false) return;

            // reset windows control 
            TXT_BASE_RECP.Text = string.Empty;
            TXT_PATH_PTRN_FILE.Text = string.Empty;
            TXT_PATH_RECP_FILE.Text = string.Empty;
            TXT_PTRN_POS_ORG_X.Text = TXT_PTRN_POS_ORG_Y.Text = string.Empty;
            TXT_PTRN_ACC_RATIO.Text = "0";

            // reset data 
            imageView1.fm.baseRecp.RemoveAll();
            imageView1.fm.RemoveAll();
            imageView1.VIEW_Set_Clear_DispObject();
            imageView1.VIEW_SET_Mag_Origin();
            imageView1.Refresh();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            formBaseRecp.ShowDialog();
        }
        private void BTN_MENU_ADVENCED_Click(object sender, EventArgs e)
        {
            dlgAdvanced.SetParam(m_hacker);
            dlgAdvanced.ShowDialog();
        }
        #endregion
       
       #region MAIN LOGO
        private void BTN_MAIN_LOGO_Click(object sender, EventArgs e)
        {
            // increase interruption counter 
            m_nInterruptCount++;

            // reverse mode
            m_bActivateCheatMode = !m_bActivateCheatMode;

            // meet!! interruption
            if (m_nInterruptCount >= 3)
            {
                report.INTERRUPT = true;
            }
            // checking cheat mode
            if (m_bActivateCheatMode == false)
            {
                string temp = string.Empty;

                foreach (string s in m_listCheatKey)
                {
                    temp += s;
                }
                if (temp == "QLALFDLDI")
                {
                    MessageBox.Show("Advanced Mode Activated!!");

                    LB_HACKER.Visible = true;
                    BTN_MENU_ADVANCED.Visible = true;
                }

                m_listCheatKey.Clear();
            }
        }
        private void BTN_MAIN_LOGO_KeyDown(object sender, KeyEventArgs e) { m_listCheatKey.Add(e.KeyCode.ToString());  }
        #endregion

       #region PARAMETER LISTING RELATIVED
        private void LV_PARAMETER_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled) return;

            int nFigureIndex = TAB_FIGURE.SelectedIndex;
            int tx = TB_FIGURE_CTRL_SCALE.Value;
            int ty = TB_FIGURE_CTRL_SCALE.Value; ;

            int nDataIndex = -1;

            //********************************************************************

            #region Figure Type and Data Index Selection

            if (nFigureIndex == IFX_FIGURE.PAIR_RCT)
            {
                nDataIndex = Convert.ToInt32(TXT_INDEX_OF_RECT.Text);
                nFigureIndex = IFX_FIGURE.PAIR_RCT;
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_CIR)
            {
                nDataIndex = Convert.ToInt32(TXT_INDEX_OF_CIRCLE.Text);
                nFigureIndex = IFX_FIGURE.PAIR_CIR;
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_OVL)
            {
                nDataIndex = Convert.ToInt32(TXT_INDEX_OF_OVL.Text);
                nFigureIndex = IFX_FIGURE.PAIR_OVL;
            }
            else if (nFigureIndex == IFX_FIGURE.RC_FOCUS)
            {
                nFigureIndex = IFX_FIGURE.RC_FOCUS;
            }
            else if (nFigureIndex == IFX_FIGURE.MIXED_RC)
            {
                nDataIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RC.Text);
                nFigureIndex = IFX_FIGURE.MIXED_RC;
            }
            else if (nFigureIndex == IFX_FIGURE.MIXED_CC)
            {
                nDataIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_CC.Text);
                nFigureIndex = IFX_FIGURE.MIXED_CC;
            }
            else if (nFigureIndex == IFX_FIGURE.MIXED_RCC)
            {
                nDataIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RCC.Text);
                nFigureIndex = IFX_FIGURE.MIXED_RCC;
            }
            #endregion

            //********************************************************************               
            //apply directional operation                                                        
            int m_nAction = 0;

            /***/
            if (RDO_ROI_POSITION.Checked == true) m_nAction = IFX_ADJ_ACTION.POS;
            else if (RDO_ROI_GAP.Checked /***/== true) m_nAction = IFX_ADJ_ACTION.GAP;
            else if (RDO_ROI_SIZE.Checked /**/== true) m_nAction = IFX_ADJ_ACTION.SIZE;
            else if (RDO_ROI_ASYM.Checked /**/== true) m_nAction = IFX_ADJ_ACTION.ASYM;


            //************************************************************************************************
            // 01 Mode control 
            //************************************************************************************************
            if (e.KeyCode == Keys.P || e.KeyCode == Keys.G || e.KeyCode == Keys.S || e.KeyCode == Keys.Z)
            {
                string mode = string.Empty;

                /***/
                if (e.KeyCode == Keys.P) { RDO_ROI_POSITION.Checked = true; mode = "POSITION ADJUST"; }
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
                    /***/if (e.KeyCode == Keys.PageUp) BTN_DIAGONAL_CHANGE_Click(BTN_DIA_ANGLE_UP, EventArgs.Empty);
                    else if (e.KeyCode == Keys.PageDown) BTN_DIAGONAL_CHANGE_Click(BTN_DIA_ANGLE_DW, EventArgs.Empty);
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
                /***/
                if (e.KeyCode == Keys.Add/***/) { _ChangeFigure_AdjustLevel(+1); }
                else if (e.KeyCode == Keys.Subtract) { _ChangeFigure_AdjustLevel(-1); }
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.OemPeriod)
            {
                /***/
                if (e.KeyCode == Keys.OemPeriod/***/) { _ChangeFigure_AdjustLevel(+1); }
                else if (e.KeyCode == Keys.Oemcomma/****/) { _ChangeFigure_AdjustLevel(-1); }
            }
            else
            {
                if (nFigureIndex == IFX_FIGURE.PAIR_OVL) // only for overlay figure
                {
                    int nTarget = RDO_ROI_OVL_IN.Checked == true ? 0 : RDO_ROI_OVL_EX.Checked == true ? 1 : -1;

                    int nDir = 0;

                    /***/
                    if (e.KeyCode == Keys.Left)/****/nDir = IFX_DIR.DIR_LFT;
                    else if (e.KeyCode == Keys.Up)/******/nDir = IFX_DIR.DIR_TOP;
                    else if (e.KeyCode == Keys.Right)/***/nDir = IFX_DIR.DIR_RHT;
                    else if (e.KeyCode == Keys.Down)/****/nDir = IFX_DIR.DIR_BTM;

                    if (nTarget != -1) // 0 or 1 Special target 
                    {
                        imageView1.iAdj_Overlay(m_nAction, nTarget, nDataIndex, nDir, TB_FIGURE_CTRL_SCALE.Value, CHK_ROI_OVL_MODE_H.Checked, CHK_ROI_OVL_MODE_V.Checked);
                    }
                    else if (nTarget == -1) // NON SELECTION : ENTIARE TARGET
                    {
                        imageView1.iAdj_Overlay(m_nAction, 0, nDataIndex, nDir, TB_FIGURE_CTRL_SCALE.Value, CHK_ROI_OVL_MODE_H.Checked, CHK_ROI_OVL_MODE_V.Checked);
                        imageView1.iAdj_Overlay(m_nAction, 1, nDataIndex, nDir, TB_FIGURE_CTRL_SCALE.Value, CHK_ROI_OVL_MODE_H.Checked, CHK_ROI_OVL_MODE_V.Checked);
                    }
                }
                //************************************************************************************************
                // Position Control 
                //************************************************************************************************
                else // for common figure 
                {
                    if (e.KeyCode == Keys.Left)
                    {
                        /***/
                        if (m_nAction == IFX_ADJ_ACTION.POS/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.POS, -tx, 0);
                        else if (m_nAction == IFX_ADJ_ACTION.GAP/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.GAP, -tx, 0);
                        else if (m_nAction == IFX_ADJ_ACTION.SIZE/*****/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.SIZE, -tx, 0);
                    }
                    else if (e.KeyCode == Keys.Right)
                    {
                        /***/
                        if (m_nAction == IFX_ADJ_ACTION.POS/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.POS, tx, 0);
                        else if (m_nAction == IFX_ADJ_ACTION.GAP/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.GAP, tx, 0);
                        else if (m_nAction == IFX_ADJ_ACTION.SIZE/*****/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.SIZE, tx, 0);
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        /***/
                        if (m_nAction == IFX_ADJ_ACTION.POS/******/) { imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.POS, 0, -ty); }
                        else if (m_nAction == IFX_ADJ_ACTION.GAP/******/) { imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.GAP, 0, -ty); }
                        else if (m_nAction == IFX_ADJ_ACTION.SIZE/*****/) { imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.SIZE, 0, -ty); }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        /***/
                        if (m_nAction == IFX_ADJ_ACTION.POS/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.POS, 0, ty);
                        else if (m_nAction == IFX_ADJ_ACTION.GAP/******/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.GAP, 0, ty);
                        else if (m_nAction == IFX_ADJ_ACTION.SIZE/*****/) imageView1.iAdj_Figure(nFigureIndex, nDataIndex, IFX_ADJ_ACTION.SIZE, 0, ty);
                    }
                }
                _UpdateUI_FigureSelection(nDataIndex);
            }

            //BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            // to prevent envet duplication
            e.Handled = true;
        }
        private void LV_PARAMETER_MouseDoubleClick(object sender, MouseEventArgs e) { }
        private void LV_PARAMETER_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region
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

            if (strHeader == "RC")
            {
                TAB_FIGURE.SelectedIndex = IFX_FIGURE.PAIR_RCT;
                CMeasurePairRct single = fmCopy.ElementAt_PairRct(nItemIndex);

                PROPERTY_PairRct propertySingle = new PROPERTY_PairRct();
                propertySingle.FromFigure(single);
                UC_Parameter.SetParam(propertySingle);
            }
            else if (strHeader == "CC")
            {
                TAB_FIGURE.SelectedIndex = IFX_FIGURE.PAIR_CIR;
                CMeasurePairCir single = fmCopy.ElementAt_PairCir(nItemIndex);

                PROPERTY_PairCir propertySingle = new PROPERTY_PairCir();
                propertySingle.FromFigure(single);
                UC_Parameter.SetParam(propertySingle);
            }
            else if (strHeader == "OVL")
            {
                TAB_FIGURE.SelectedIndex = IFX_FIGURE.PAIR_OVL;
                CMeasurePairOvl single = fmCopy.ElementAt_PairOvl(nItemIndex);

                PROPERTY_PairOvl propertySingle = new PROPERTY_PairOvl();
                propertySingle.FromFigure(single);
                UC_Parameter.SetParam(propertySingle);
            }
            else if (strHeader == "FOCUS")
            {
                TAB_FIGURE.SelectedIndex = IFX_FIGURE.RC_FOCUS;
                UC_Parameter.ClearData();
            }
            else if (strHeader == "MRC")
            {
                TAB_FIGURE.SelectedIndex = IFX_FIGURE.MIXED_RC;
                CMeasureMixedRC single = fmCopy.ElementAt_MRC(nItemIndex);

                PROPERTY_MixedRC propertySingle = new PROPERTY_MixedRC();
                propertySingle.FromFigure(single);
                UC_Parameter.SetParam(propertySingle);
            }
            else if (strHeader == "MCC")
            {
                TAB_FIGURE.SelectedIndex = IFX_FIGURE.MIXED_CC; 
                CMeasureMixedCC single = fmCopy.ElementAt_MCC(nItemIndex);

                PROPERTY_MixedCC propertySingle = new PROPERTY_MixedCC();
                propertySingle.FromFigure(single);
                UC_Parameter.SetParam(propertySingle);
            }
            else if (strHeader == "MRCC")
            {
                TAB_FIGURE.SelectedIndex = IFX_FIGURE.MIXED_RCC; 
                CMeasureMixedRCC single = fmCopy.ElementAt_MRCC(nItemIndex);

                PROPERTY_MixedRCC propertySingle = new PROPERTY_MixedRCC();
                propertySingle.FromFigure(single);
                UC_Parameter.SetParam(propertySingle);

            }

            _UpdateUI_FigureSelection(nItemIndex);
            #endregion
        }
       #endregion

        private void BTN_TEMP_TRANS_DESIGN_Click(object sender, EventArgs e)
        {
            byte[] rawImage = imageView1.GetDisplay_Raw();
            int imageW = imageView1.VIEW_GetImageW();
            int imageH = imageView1.VIEW_GetImageH();

            byte[] gradient = Computer.HC_TRANS_GradientImage(rawImage, imageW, imageH);
            double [] kernel = Computer.HC_FILTER_GenerateGaussianFilter(1.5, 7);
            gradient = Computer.HC_FILTER_Convolution(kernel, gradient, imageW, imageH);
            imageView1.SetDisplay(gradient, imageW, imageH);
            imageView1.Refresh();
        }

        #region FOR FIGURES IN THE MAIN DIALOG
        // for figure drawing action
        private void BTN_FIGURE_DRAW_Click(object sender, EventArgs e)
        {
            imageView1.iRemove_Roi_Focus();
            imageView1.iDrawFocus(true);
            imageView1.Refresh();
        }
        // remove region temporary drawn
        private void BTN_FIGURE_REMOVE_Click(object sender, EventArgs e)
        {
            imageView1.iRemove_Roi_Focus();
            imageView1.Refresh();
        }
        // set region from the free-drawn - only for base figures 
        private void BTN_FIGURE_SET_REGION_Click(object sender, EventArgs e)
        {
            Rectangle rc = imageView1.iGet_Roi_Focus();

            int nFigureIndex = TAB_FIGURE.SelectedIndex;

            // region verification
            if (m_speaker.Check_Is_Invalid_Figure(rc, imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }

            if (nFigureIndex == IFX_FIGURE.RC_FOCUS)
            {
                #region
                imageView1.fm.RC_FOCUS = rc;
                imageView1.fm.CroodinateBackckupFocusRect();
                CControl.SetTextBoxFrom_RectangleF(rc, TXT_FOCUS_POS_X, TXT_FOCUS_POS_Y, TXT_FOCUS_SZ_W, TXT_FOCUS_SZ_H);
                #endregion
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_RCT)
            {
                #region
                CMeasurePairRct single = new CMeasurePairRct();

                if ( RDO_TYPE_HOR.Checked == true)
                {
                    single.param_01_rc_type = IFX_RECT_TYPE.DIR_HOR;
                    single.rc_FST = new parseRect(rc.X, rc.Y - 5/**********/, rc.Width, 10);
                    single.rc_SCD = new parseRect(rc.X, rc.Y + rc.Height - 5, rc.Width, 10);
                }

                if (RDO_TYPE_VER.Checked == true)
                {
                    single.param_01_rc_type = IFX_RECT_TYPE.DIR_VER;
                    single.rc_FST = new parseRect(rc.X - 5/*********/, rc.Y, 10, rc.Height);
                    single.rc_SCD = new parseRect(rc.X + rc.Width - 5, rc.Y, 10, rc.Height);
                }

                single.CroodinateBackup();

                BASE_RECP baseRecp = imageView1.fm.baseRecp;

                int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_RCT);
                single.NICKNAME = "RECT_" + nLastIndex.ToString("N0");

                imageView1.fm.Figure_Add(single);
                #endregion
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_CIR)
            {
                #region
                CMeasurePairCir single = new CMeasurePairCir();

                int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_CIR);
                single.NICKNAME = "CIRCLE_" + nLastIndex.ToString("N0");

                int rcW = rc.Width;
                int rcH = rc.Height;

                single.rc_EX = new RectangleF(rc.X, rc.Y, rcW, rcH);
                single.rc_IN = new RectangleF(rc.X, rc.Y, 6, 6);
                single.rc_IN = CRect.SetCenter(single.rc_IN, single.rc_EX);

                single.CroodinateBackup();

                BASE_RECP baseRecp = imageView1.fm.baseRecp;

                imageView1.fm.Figure_Add(single);
                #endregion
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_OVL)
            {
                #region
                CMeasurePairOvl single = new CMeasurePairOvl();

                int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_OVL);
                single.NICKNAME = "OVERLAY_" + nLastIndex.ToString("N0");

                int rcW = rc.Width; int rcH = rc.Height;

                single.RC_HOR_EX.rc_FST = new parseRect(rc.X, rc.Y /**********/- 5, rcW, 10);
                single.RC_HOR_EX.rc_SCD = new parseRect(rc.X, rc.Y + rc.Height - 5, rcW, 10);
                single.RC_VER_EX.rc_FST = new parseRect(rc.X /*********/- 5, rc.Y, 10, rcH);
                single.RC_VER_EX.rc_SCD = new parseRect(rc.X + rc.Width - 5, rc.Y, 10, rcH);

                single.RC_HOR_IN.rc_FST = new parseRect((float)(rc.X + rcW * 0.2), (float)(rc.Y /**********/+ rcH * 0.2 - 5), (float)(rcW * 0.6), 10);
                single.RC_HOR_IN.rc_SCD = new parseRect((float)(rc.X + rcW * 0.2), (float)(rc.Y + rc.Height - rcH * 0.2 - 5), (float)(rcW * 0.6), 10);

                single.RC_VER_IN.rc_FST = new parseRect((float)(rc.X + rcW * 0.2) - 5, /******/(float)(rc.Y + rcH * 0.2), 10, (float)(rcH * 0.6));
                single.RC_VER_IN.rc_SCD = new parseRect((float)(rc.X - rcW * 0.2) + rc.Width - 5, (float)(rc.Y + rcH * 0.2), 10, (float)(rcH * 0.6));

                single.CroodinateBackup();

                imageView1.fm.Figure_Add(single);
                #endregion
            }

            BTN_FIGURE_REMOVE_Click(null, EventArgs.Empty);
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            imageView1.iRemove_Roi_All();
            imageView1.iRemove_Roi_Focus();
            imageView1.Refresh();
        }
        // set region from the free-drawn - only for mixed figures
        private void BTN_SET_MIXED_FIGURES_Click(object sender, EventArgs e)
        {
            Rectangle rc = imageView1.iGet_Roi_Focus();

            int imageW = imageView1.VIEW_GetImageW();
            int imageH = imageView2.VIEW_GetImageH();

            if (TAB_FIGURE.SelectedIndex == IFX_FIGURE.MIXED_RC)
            {
                if (RDO_MIXED_RC_FST.Checked == true)
                {
                    CControl.SetTextBoxFrom_RectangleF(rc, TXT_MIXED_RC_FST_POS_X, TXT_MIXED_RC_FST_POS_Y, TXT_MIXED_RC_FST_SZ_W, TXT_MIXED_RC_FST_SZ_H);
                }
                else if (RDO_MIXED_RC_SCD.Checked == true)
                {
                    CControl.SetTextBoxFrom_RectangleF(rc, TXT_MIXED_RC_SCD_POS_X, TXT_MIXED_RC_SCD_POS_Y, TXT_MIXED_RC_SCD_SZ_W, TXT_MIXED_RC_SCD_SZ_H);
                }
            }
            else if (TAB_FIGURE.SelectedIndex == IFX_FIGURE.MIXED_CC)
            {
                if (RDO_MIXED_CC_FST.Checked == true)
                {
                    CControl.SetTextBoxFrom_RectangleF(rc, TXT_MIXED_CC_FST_POS_X, TXT_MIXED_CC_FST_POS_Y, TXT_MIXED_CC_FST_SZ_W, TXT_MIXED_CC_FST_SZ_H);
                }
                else if (RDO_MIXED_CC_SCD.Checked == true)
                {
                    CControl.SetTextBoxFrom_RectangleF(rc, TXT_MIXED_CC_SCD_POS_X, TXT_MIXED_CC_SCD_POS_Y, TXT_MIXED_CC_SCD_SZ_W, TXT_MIXED_CC_SCD_SZ_H);
                }
            }
            else if (TAB_FIGURE.SelectedIndex == IFX_FIGURE.MIXED_RCC)
            {
                if (RDO_MIXED_RCC_FST.Checked == true)
                {
                    CControl.SetTextBoxFrom_RectangleF(rc, TXT_MIXED_RCC_FST_POS_X, TXT_MIXED_RCC_FST_POS_Y, TXT_MIXED_RCC_FST_SZ_W, TXT_MIXED_RCC_FST_SZ_H);
                }
                else if (RDO_MIXED_RCC_SCD.Checked == true)
                {
                    CControl.SetTextBoxFrom_RectangleF(rc, TXT_MIXED_RCC_SCD_POS_X, TXT_MIXED_RCC_SCD_POS_Y, TXT_MIXED_RCC_SCD_SZ_W, TXT_MIXED_RCC_SCD_SZ_H);
                }
            }


            // BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            imageView1.Refresh();
        }

        private void BTN_FIGURE_DELETE_Click(object sender, EventArgs e)
        {
            // exception
            if (LV_PARAMETER.FocusedItem == null) return;
            int nIndexLV = LV_PARAMETER.FocusedItem.Index;

            // get selection index
            string strSelected = LV_PARAMETER.Items[nIndexLV].SubItems[1].Text;
            string[] parse = strSelected.Split('-');

            string strHeader = parse[0];
            int nSelectedIndex = Convert.ToInt32(parse[1]) - 1;

            // index verification
            if (m_speaker.Check_is_Error_Index_Validity(nSelectedIndex) == true) { return; }

            // delete selected figure
            if (m_speaker.Figure_Remove() == DialogResult.Yes)
            {
                object objectSelected = UC_Parameter.GetCurrentData();

                /***/if (strHeader == "RC"/******/) { imageView1.iDel_Figure(IFX_FIGURE.PAIR_RCT, nSelectedIndex); }
                else if (strHeader == "CC"/******/) { imageView1.iDel_Figure(IFX_FIGURE.PAIR_CIR, nSelectedIndex); }
                else if (strHeader == "OVL"/*****/) { imageView1.iDel_Figure(IFX_FIGURE.PAIR_OVL, nSelectedIndex); }
                else if (strHeader == "FOCUS"/***/) { imageView1.fm.RC_FOCUS = new RectangleF(0, 0, 0, 0); }
                else if (strHeader == "MRC"/*****/) { imageView1.iDel_Figure(IFX_FIGURE.MIXED_RC, nSelectedIndex); }
                else if (strHeader == "MCC"/*****/) { imageView1.iDel_Figure(IFX_FIGURE.MIXED_CC, nSelectedIndex); }
                else if (strHeader == "MRCC"/****/) { imageView1.iDel_Figure(IFX_FIGURE.MIXED_RCC, nSelectedIndex); }

                imageView1.Refresh();
                BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            }
        }
        private void BTN_FIGURE_REMOVE_ALL_Click(object sender, EventArgs e)
        {
            if( m_speaker.Figure_Remove_All() == DialogResult.Yes)
            {
                imageView1.fm.RemoveAll();
                imageView1.Refresh();
                BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            }
        }
        private void BTN_FIGURE_REFRESH_Click(object sender, EventArgs e)
        {
            UC_Parameter.ClearData();
            LV_PARAMETER.Items.Clear();
            m_bChangedDiagonalAngle = false;

            CFigureManager fmCopy = imageView1.fm;

            LV_PARAMETER.BeginUpdate();

            {
                //@@@ IMPORTANT 
                //@@@ PARSER EQUALS =====> -  underbar it is "hipen" == "-";

                // for digonal rect pair
                for (int i = 0; i < fmCopy.COUNT_PAIR_RCT; i++)
                {
                    string strType = "RC";

                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strType + "-" + (i + 1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }

                // for circle 
                for (int i = 0; i < fmCopy.COUNT_PAIR_CIR; i++)
                {
                    string strType = "CC";

                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strType + "-" + (i + 1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }
                // for overlay
                for (int i = 0; i < fmCopy.COUNT_PAIR_OVL; i++)
                {
                    string strTye = "OVL";

                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strTye + "-" + (i + 1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }
                // for mixed rect
                for (int i = 0; i < fmCopy.COUNT_MIXED_RC; i++)
                {
                    string strTye = "MRC";
                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strTye + "-" + (i + 1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }
                // for mixed circle
                for (int i = 0; i < fmCopy.COUNT_MIXED_CC; i++)
                {
                    string strTye = "MCC";
                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strTye + "-" + (i + 1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }
                // for mixed rect
                for (int i = 0; i < fmCopy.COUNT_MIXED_RCC; i++)
                {
                    string strTye = "MRCC";
                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(strTye + "-" + (i + 1).ToString("N0"));
                    LV_PARAMETER.Items.Add(lvi);
                }

                // for focus region
                if (imageView1.fm.RC_FOCUS.X != 0 && imageView1.fm.RC_FOCUS.Y != 0)
                {
                    ListViewItem lvi = new ListViewItem("0");
                    string strType = "FOCUS";
                    lvi.SubItems.Add(strType + "-" + "1");
                    LV_PARAMETER.Items.Add(lvi);
                }

            }

            LV_PARAMETER.EndUpdate();

            _UpdateUI_Figure(new CMeasurePairRct(), 0);
            _UpdateUI_Figure(new CMeasurePairCir(), 0);
            _UpdateUI_Figure(new CMeasurePairOvl(), 0);
            _UpdateUI_Figure(new CMeasureMixedRC(), 0);
            _UpdateUI_Figure(new CMeasureMixedCC(), 0);
            _UpdateUI_Figure(new CMeasureMixedRCC(), 0);
        }
        private void BTN_FIGURE_Modify_Click(object sender, EventArgs e)
        {
            // exception
            if (LV_PARAMETER.FocusedItem == null) return;

            // in order to avoid data twist
            if( m_bChangedDiagonalAngle == true)
            {
                BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
                MessageBox.Show("Data Sync is not Matched. Please Try Again.", "DATA SYNC ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int nIndexLV = LV_PARAMETER.FocusedItem.Index;

            // get selection index
            string strSelected = LV_PARAMETER.Items[nIndexLV].SubItems[1].Text;
            string[] parse = strSelected.Split('-');

            string strHeader = parse[0];
            int nSelectedIndex = Convert.ToInt32(parse[1]) - 1;

            // index verification
            if (m_speaker.Check_is_Error_Index_Validity(nSelectedIndex) == true){return;}

            // get the selected object
            object objectSelected = UC_Parameter.GetCurrentData();

            if (strHeader == "RC")
            {
                CMeasurePairRct org = (CMeasurePairRct)imageView1.iGet_Figure(IFX_FIGURE.PAIR_RCT, nSelectedIndex);
                CMeasurePairRct obj = ((PROPERTY_PairRct)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    // backup and Recovery
                    obj.param_02_rect_angle = org.param_02_rect_angle;
                    obj.param_03_bool_Use_AutoDetection = org.param_03_bool_Use_AutoDetection;
                    obj.param_04_peakTargetIndex_fst = org.param_04_peakTargetIndex_fst;
                    obj.param_05_peakTargetIndex_scd = org.param_05_peakTargetIndex_scd;
                    obj.param_06_peakCandidate = org.param_06_peakCandidate;
                    obj.param_07_windowSize = org.param_07_windowSize;

                    org = obj.CopyTo();

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-RCT", DEF_OPERATION.OPER_03_PARAM);
                    m_speaker.Inform_Finished_Parameter_Change();
                }
            }
            else if (strHeader == "CC")
            {
                CMeasurePairCir org = (CMeasurePairCir)imageView1.iGet_Figure(IFX_FIGURE.PAIR_CIR, nSelectedIndex);
                CMeasurePairCir obj = ((PROPERTY_PairCir)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    org = obj.CopyTo();

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-CIR", DEF_OPERATION.OPER_03_PARAM);
                    m_speaker.Inform_Finished_Parameter_Change();
                }
            }
            else if (strHeader == "MRC")
            {
                CMeasureMixedRC org = (CMeasureMixedRC)imageView1.iGet_Figure(IFX_FIGURE.MIXED_RC, nSelectedIndex);
                CMeasureMixedRC obj = ((PROPERTY_MixedRC)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    org = obj.CopyTo();

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-MRC", DEF_OPERATION.OPER_03_PARAM);
                    m_speaker.Inform_Finished_Parameter_Change();
                }
            }
            else if (strHeader == "MCC")
            {
                CMeasureMixedCC org = (CMeasureMixedCC)imageView1.iGet_Figure(IFX_FIGURE.MIXED_CC, nSelectedIndex);
                CMeasureMixedCC obj = ((PROPERTY_MixedCC)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    org = obj.CopyTo();

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-MCC", DEF_OPERATION.OPER_03_PARAM);
                    m_speaker.Inform_Finished_Parameter_Change();
                }
            }
            else if (strHeader == "MRCC")
            {
                CMeasureMixedRCC org = (CMeasureMixedRCC)imageView1.iGet_Figure(IFX_FIGURE.MIXED_RCC, nSelectedIndex);
                CMeasureMixedRCC obj = ((PROPERTY_MixedRCC)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    org = obj.CopyTo();

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-MRCC", DEF_OPERATION.OPER_03_PARAM);
                    m_speaker.Inform_Finished_Parameter_Change();
                }
            }
            else if (strHeader == "OVL")
            {
                CMeasurePairOvl org = (CMeasurePairOvl)imageView1.iGet_Figure(IFX_FIGURE.PAIR_OVL, nSelectedIndex);
                CMeasurePairOvl obj = ((PROPERTY_PairOvl)objectSelected).ToFigure();

                if (org.NICKNAME == obj.NICKNAME)
                {
                    org = obj.CopyTo();

                    imageView1.iMod_Figure(org, nSelectedIndex);
                    UC_LOG_VIEWER.WRITE_LOG("PARAM Changed. FIG-OVL", DEF_OPERATION.OPER_03_PARAM);
                    m_speaker.Inform_Finished_Parameter_Change();
                }
            }
            imageView1.Refresh();
        }

        private void _UpdateUI_Figure(object obj, int nIndex)
        {
            if (obj.GetType() == new CMeasurePairRct().GetType())
            {
                #region
                CMeasurePairRct single = ((CMeasurePairRct)obj).CopyTo();
                TXT_INDEX_OF_RECT.Text = nIndex.ToString("N0");
                TXT_PARAM_DIG_NICK.Text = single.NICKNAME;
                TXT_PARAM_DIA_ANGLE.Text = single.param_02_rect_angle.ToString("N0");

                /***/if (single.param_01_rc_type == IFX_RECT_TYPE.DIR_HOR) RDO_TYPE_HOR.Checked = true;
                else if (single.param_01_rc_type == IFX_RECT_TYPE.DIR_VER) RDO_TYPE_VER.Checked = true;
                else if (single.param_01_rc_type == IFX_RECT_TYPE.DIR_DIA) RDO_TYPE_DIA.Checked = true;

                CControl.SetTextBoxFrom_RectangleF(single.rc_FST.ToRectangleF(), TXT_RECT_FST_POS_X, TXT_RECT_FST_POS_Y, TXT_RECT_FST_SZ_W, TXT_RECT_FST_SZ_H);
                CControl.SetTextBoxFrom_RectangleF(single.rc_SCD.ToRectangleF(), TXT_RECT_SCD_POS_X, TXT_RECT_SCD_POS_Y, TXT_RECT_SCD_SZ_W, TXT_RECT_SCD_SZ_H);
                #endregion
            }
            else if (obj.GetType() == new CMeasurePairCir().GetType())
            {
                #region
                CMeasurePairCir single = ((CMeasurePairCir)obj).CopyTo();

                TXT_INDEX_OF_CIRCLE.Text = nIndex.ToString("N0");
                TXT_PARAM_CIR_NICK.Text = single.NICKNAME;

                CControl.SetTextBoxFrom_RectangleF(single.rc_EX, TXT_CIRCLE_POS_X, TXT_CIRCLE_POS_Y, TXT_CIRCLE_SZ_W, TXT_CIRCLE_SZ_H);
                #endregion
            }

            else if (obj.GetType() == new CMeasurePairOvl().GetType())
            {
                #region
                CMeasurePairOvl single = ((CMeasurePairOvl)obj).CopyTo();

                TXT_INDEX_OF_OVL.Text = nIndex.ToString("N0");
                TXT_PARAM_OVL_NICK.Text = single.NICKNAME;

                CControl.SetTextBoxFrom_RectangleF(single.RC_HOR_IN.rc_FST.Width, single.RC_HOR_IN.rc_FST.Height, TXT_OVL_IN_W, TXT_OVL_IN_H);
                CControl.SetTextBoxFrom_RectangleF(single.RC_HOR_EX.rc_FST.Width, single.RC_HOR_EX.rc_FST.Height, TXT_OVL_EX_W, TXT_OVL_EX_H);
                #endregion  
            }
            else if (obj.GetType() == new CMeasureMixedRC().GetType())
            {
                #region
                CMeasureMixedRC single = ((CMeasureMixedRC)obj).CopyTo();

                TXT_INDEX_OF_MIXED_RC.Text = nIndex.ToString("N0");
                TXT_MIXED_RC_NICK.Text = single.NICKNAME;

                /***/if (single.param_04_rc_type_fst == IFX_RECT_TYPE.DIR_HOR) RDO_MIXED_RC_FST_DIR_HOR.Checked = true;
                else if (single.param_04_rc_type_fst == IFX_RECT_TYPE.DIR_VER) RDO_MIXED_RC_FST_DIR_VER.Checked = true;

                /***/if (single.param_05_rc_type_scd == IFX_RECT_TYPE.DIR_HOR) RDO_MIXED_RC_SCD_DIR_HOR.Checked = true;
                else if (single.param_05_rc_type_scd == IFX_RECT_TYPE.DIR_VER) RDO_MIXED_RC_SCD_DIR_VER.Checked = true;

                /***/if (single.param_07_metric_type == IFX_METRIC.P2P) RDO_MIXED_RC_METRIC_P2P.Checked = true;
                else if (single.param_07_metric_type == IFX_METRIC.HOR) RDO_MIXED_RC_METRIC_HOR.Checked = true;
                else if (single.param_07_metric_type == IFX_METRIC.VER) RDO_MIXED_RC_METRIC_VER.Checked = true;

                CControl.SetTextBoxFrom_RectangleF(single.rc_FST.ToRectangleF(), TXT_MIXED_RC_FST_POS_X, TXT_MIXED_RC_FST_POS_Y, TXT_MIXED_RC_FST_SZ_W, TXT_MIXED_RC_FST_SZ_H);
                CControl.SetTextBoxFrom_RectangleF(single.rc_SCD.ToRectangleF(), TXT_MIXED_RC_SCD_POS_X, TXT_MIXED_RC_SCD_POS_Y, TXT_MIXED_RC_SCD_SZ_W, TXT_MIXED_RC_SCD_SZ_H);

                CHK_MIXED_RC_USE_CENTROID.Checked = single.param_08_use_centroid;
                #endregion
            }
            else if (obj.GetType() == new CMeasureMixedCC().GetType())
            {
                #region
                CMeasureMixedCC single = ((CMeasureMixedCC)obj).CopyTo();

                TXT_INDEX_OF_MIXED_CC.Text = nIndex.ToString("N0");
                TXT_MIXED_CC_NICK.Text = single.NICKNAME;

                /***/if (single.param_06_ms_pos_fst == IFX_DIR.DIR_LFT) RDO_MIXED_CC_FST_DIR_LFT.Checked = true;
                else if (single.param_06_ms_pos_fst == IFX_DIR.DIR_TOP) RDO_MIXED_CC_FST_DIR_TOP.Checked = true;
                else if (single.param_06_ms_pos_fst == IFX_DIR.DIR_RHT) RDO_MIXED_CC_FST_DIR_RHT.Checked = true;
                else if (single.param_06_ms_pos_fst == IFX_DIR.DIR_BTM) RDO_MIXED_CC_FST_DIR_BTM.Checked = true;

                /***/if (single.param_07_ms_pos_scd == IFX_DIR.DIR_LFT) RDO_MIXED_CC_SCD_DIR_LFT.Checked = true;
                else if (single.param_07_ms_pos_scd == IFX_DIR.DIR_TOP) RDO_MIXED_CC_SCD_DIR_TOP.Checked = true;
                else if (single.param_07_ms_pos_scd == IFX_DIR.DIR_RHT) RDO_MIXED_CC_SCD_DIR_RHT.Checked = true;
                else if (single.param_07_ms_pos_scd == IFX_DIR.DIR_BTM) RDO_MIXED_CC_SCD_DIR_BTM.Checked = true;

                /***/if (single.param_08_metric_type == IFX_METRIC.P2P) RDO_MIXED_CC_METRIC_P2P.Checked = true;
                else if (single.param_08_metric_type == IFX_METRIC.HOR) RDO_MIXED_CC_METRIC_HOR.Checked = true;
                else if (single.param_08_metric_type == IFX_METRIC.VER) RDO_MIXED_CC_METRIC_VER.Checked = true;

                CControl.SetTextBoxFrom_RectangleF(single.rc_FST_EX, TXT_MIXED_CC_FST_POS_X, TXT_MIXED_CC_FST_POS_Y, TXT_MIXED_CC_FST_SZ_W, TXT_MIXED_CC_FST_SZ_H);
                CControl.SetTextBoxFrom_RectangleF(single.rc_SCD_EX, TXT_MIXED_CC_SCD_POS_X, TXT_MIXED_CC_SCD_POS_Y, TXT_MIXED_CC_SCD_SZ_W, TXT_MIXED_CC_SCD_SZ_H);
                #endregion
            }
            else if (obj.GetType() == new CMeasureMixedRCC().GetType())
            {
                #region
                CMeasureMixedRCC single = ((CMeasureMixedRCC)obj).CopyTo();

                TXT_INDEX_OF_MIXED_RCC.Text = nIndex.ToString("N0");
                TXT_MIXED_RCC_NICK.Text = single.NICKNAME;

                CControl.SetTextBoxFrom_RectangleF(single.rc_FST.ToRectangleF(), TXT_MIXED_RCC_FST_POS_X, TXT_MIXED_RCC_FST_POS_Y, TXT_MIXED_RCC_FST_SZ_W, TXT_MIXED_RCC_FST_SZ_H);
                CControl.SetTextBoxFrom_RectangleF(single.rc_FST_EX, TXT_MIXED_RCC_SCD_POS_X, TXT_MIXED_RCC_SCD_POS_Y, TXT_MIXED_RCC_SCD_SZ_W, TXT_MIXED_RCC_SCD_SZ_H);

                /***/if (single.param_02_rc_type_fst == IFX_RECT_TYPE.DIR_HOR) RDO_MIXED_RCC_FST_DIR_HOR.Checked = true;
                else if (single.param_02_rc_type_fst == IFX_RECT_TYPE.DIR_VER) RDO_MIXED_RCC_FST_DIR_VER.Checked = true;

                /***/if (single.param_14_ms_pos_scd == IFX_DIR.DIR_LFT) RDO_MIXED_RCC_SCD_DIR_LFT.Checked = true;
                else if (single.param_14_ms_pos_scd == IFX_DIR.DIR_TOP) RDO_MIXED_RCC_SCD_DIR_TOP.Checked = true;
                else if (single.param_14_ms_pos_scd == IFX_DIR.DIR_RHT) RDO_MIXED_RCC_SCD_DIR_RHT.Checked = true;
                else if (single.param_14_ms_pos_scd == IFX_DIR.DIR_BTM) RDO_MIXED_RCC_SCD_DIR_BTM.Checked = true;

                /***/if (single.param_20_metric_type == IFX_METRIC.P2P) RDO_MIXED_RCC_METRIC_P2P.Checked = true;
                else if (single.param_20_metric_type == IFX_METRIC.HOR) RDO_MIXED_RCC_METRIC_HOR.Checked = true;
                else if (single.param_20_metric_type == IFX_METRIC.VER) RDO_MIXED_RCC_METRIC_VER.Checked = true;
                #endregion
            }
        }
        private void _UpdateUI_FigureSelection(int nDataIndex)
        {
            CFigureManager fm = imageView1.iGet_AllData();

            //********************************************************************
            // select figure type

            int nFigureIndex = TAB_FIGURE.SelectedIndex;

            if (nFigureIndex == IFX_FIGURE.PAIR_RCT)
            {
                CMeasurePairRct single = fm.ElementAt_PairRct(nDataIndex);
                _UpdateUI_Figure(single, nDataIndex);
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_CIR)
            {
                CMeasurePairCir single = fm.ElementAt_PairCir(nDataIndex);
                _UpdateUI_Figure(single, nDataIndex);
            }
            else if (nFigureIndex == IFX_FIGURE.PAIR_OVL)
            {
                CMeasurePairOvl single = fm.ElementAt_PairOvl(nDataIndex);
                _UpdateUI_Figure(single, nDataIndex);
            }
            else if (nFigureIndex == IFX_FIGURE.RC_FOCUS)
            {
                RectangleF rc = CRect.CopyTo(imageView1.fm.RC_FOCUS);
                CControl.SetTextBoxFrom_RectangleF(rc, TXT_FOCUS_POS_X, TXT_FOCUS_POS_Y, TXT_FOCUS_SZ_W, TXT_FOCUS_SZ_H);
            }
            else if (nFigureIndex == IFX_FIGURE.MIXED_RC)
            {
                CMeasureMixedRC single = fm.ElementAt_MRC(nDataIndex);
                _UpdateUI_Figure(single, nDataIndex);
            }
            else if (nFigureIndex == IFX_FIGURE.MIXED_CC)
            {
                CMeasureMixedCC single = fm.ElementAt_MCC(nDataIndex);
                _UpdateUI_Figure(single, nDataIndex);
            }
            else if (nFigureIndex == IFX_FIGURE.MIXED_RCC)
            {
                CMeasureMixedRCC single = fm.ElementAt_MRCC(nDataIndex);
                _UpdateUI_Figure(single, nDataIndex);
            }
        }

        //*****************************************************************************************
        private void TAB_FIGURE_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            int nFigureIndex = TAB_FIGURE.SelectedIndex;

            // set visibility : according to tab selection only for OVL
            RDO_ROI_ASYM.Visible = nFigureIndex == IFX_FIGURE.PAIR_OVL ? true : false;
        }
        //*****************************************************************************************
        #region FIGURE MANIPULATION Scale
        private void _ChangeFigure_AdjustLevel(int sign)
        {
            int nValue = TB_FIGURE_CTRL_SCALE.Value;

            /***/
            if (sign > 0x0) { nValue++; }
            else if (sign < 0x0) { nValue--; }

            nValue = nValue > 0 ? nValue : 1;
            TB_FIGURE_CTRL_SCALE.Value = nValue;
            TB_FIGURE_CTRL_SCALE_Scroll(null, EventArgs.Empty);
        }
        private void TB_FIGURE_CTRL_SCALE_Scroll(object sender, EventArgs e)
        {
            TXT_FIGURE_CONTROL_SCALE.Text = TB_FIGURE_CTRL_SCALE.Value.ToString("N0");
        }
        private void BTN_FIGURE_MANIPULATION_SCALE_Click(object sender, EventArgs e)
        {
            int nValue = TB_FIGURE_CTRL_SCALE.Value;

            /***/
            if (((Button)sender).Name == BTN_FIGURE_MODIF_SCALE_PLUS.Name) { nValue++; }
            else if (((Button)sender).Name == BTN_FIGURE_MODIF_SCALE_MINUS.Name) { nValue--; }

            TB_FIGURE_CTRL_SCALE.Value = nValue;
            TXT_FIGURE_CONTROL_SCALE.Text = nValue.ToString("N0");
        }
        #endregion
        //*****************************************************************************************
        #region RECTANGLES - DIGONAL ANGLE CHANGER
        
        bool m_bChangedDiagonalAngle = false;
        // in order to avoid data twist for validate.

        private void BTN_DIAGONAL_CHANGE_Click(object sender, EventArgs e)
        {
            m_bChangedDiagonalAngle = true;

            int nIndex = Convert.ToInt32(TXT_INDEX_OF_RECT.Text);

            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_DIG_NICK.Text) == true) return;

            Button btn_angle_up = BTN_DIA_ANGLE_UP;
            Button btn_angle_rv = BTN_DIA_ANGLE_RV;
            Button btn_angle_dw = BTN_DIA_ANGLE_DW;

            int nAngleAccu = 0;

            CMeasurePairRct single = (CMeasurePairRct)imageView1.fm.ElementAt_PairRct(nIndex);

            int nTempAngle = single.param_02_rect_angle;

            /***/if (((Button)sender).Name == btn_angle_up.Name){nAngleAccu = +1;}
            else if (((Button)sender).Name == btn_angle_rv.Name) { nAngleAccu = -(nTempAngle * 2); }
            else if (((Button)sender).Name == btn_angle_dw.Name){nAngleAccu = -1;}


            TXT_PARAM_DIA_ANGLE.Text = (nTempAngle + nAngleAccu).ToString("N0");


            imageView1.iMod_RectPair_DigonalAngle(nIndex, nAngleAccu);
            imageView1.Refresh();
            

            //BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
        }
        #endregion
        //*****************************************************************************************
        #region FOR RECTANGLES - RADIO BUTTON EVENT ( TYPE CHANGE TO HOR/VER/DIA)
        private void RDO_TYPE_HOR_CheckedChanged(object sender, EventArgs e) { _SetDiagonalControl(false); }
        private void RDO_TYPE_VER_CheckedChanged(object sender, EventArgs e) { _SetDiagonalControl(false); }
        private void RDO_TYPE_DIA_CheckedChanged(object sender, EventArgs e) { _SetDiagonalControl(true); }
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
                LB_PARAM_DIA_ANGLE.Visible = false;
                TXT_PARAM_DIA_ANGLE.Visible = false;
                BTN_DIA_ANGLE_UP.Visible = false;
                BTN_DIA_ANGLE_DW.Visible = false;
                BTN_DIA_ANGLE_RV.Visible = false;
            }

        }
        #endregion
        //*****************************************************************************************
        #region FIGURE MINIPULATION - ADD/COPY/MODIFY/REMOVE

        #region RECTANGLE
        private void BTN_DIG_ADD_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Add() == DialogResult.No) { return; }

            imageView1.VIEW_Set_Overlay(true);
            CMeasurePairRct single = new CMeasurePairRct();

            PointF ptDraw = imageView1.PT_FIGURE_TO_DRAW;

            if (RDO_TYPE_HOR.Checked == true)
            {
                single.param_01_rc_type = IFX_RECT_TYPE.DIR_HOR;
                single.rc_FST = new parseRect(ptDraw.X, ptDraw.Y, 100, 30);
                single.rc_SCD = new parseRect(ptDraw.X, ptDraw.Y + 50, 100, 30);
            }
            else if (RDO_TYPE_VER.Checked == true)
            {
                single.param_01_rc_type = IFX_RECT_TYPE.DIR_VER;
                single.rc_FST = new parseRect(ptDraw.X + 00, ptDraw.Y, 30, 100);
                single.rc_SCD = new parseRect(ptDraw.X + 50, ptDraw.Y, 30, 100);
            }
            else if (RDO_TYPE_DIA.Checked == true)
            {
                single.param_01_rc_type = IFX_RECT_TYPE.DIR_DIA;
                // set rectangle as vertical 
                single.rc_FST = new parseRect(ptDraw.X + 00, ptDraw.Y, 30, 100);
                single.rc_SCD = new parseRect(ptDraw.X + 50, ptDraw.Y, 30, 100);
                // change to digonal
                single.ConvertRectangleType(IFX_RECT_TYPE.DIR_VER, IFX_RECT_TYPE.DIR_DIA);
            }

            single.CroodinateBackup();

            BASE_RECP baseRecp = imageView1.fm.baseRecp;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_RCT);
            single.NICKNAME = "RECT_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            imageView1.Refresh();


            BTN_FIGURE_REMOVE_Click(null, EventArgs.Empty);
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            //-------------------------------------------------------------------------------------
            // Paramter Update on the UI

            CControl.SetTextBoxFrom_RectangleF(single.rc_FST.ToRectangleF(), TXT_RECT_FST_POS_X, TXT_RECT_FST_POS_Y, TXT_RECT_FST_SZ_W, TXT_RECT_FST_SZ_H);
            CControl.SetTextBoxFrom_RectangleF(single.rc_SCD.ToRectangleF(), TXT_RECT_SCD_POS_X, TXT_RECT_SCD_POS_Y, TXT_RECT_SCD_SZ_W, TXT_RECT_SCD_SZ_H);

            _LV_File_Coloring();
            RDO_TYPE_HOR.Checked = true;

            UC_LOG_VIEWER.WRITE_LOG(string.Format("{0} Added.", IFX_FIGURE.ToStringType(single.param_01_rc_type)), DEF_OPERATION.OPER_03_PARAM);
        }
        private void BTN_DIG_COPY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Copy() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_RECT.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_DIG_NICK.Text) == true) { return; }

            CMeasurePairRct temp = (CMeasurePairRct)imageView1.fm.ElementAt_PairRct(nIndex);
            CMeasurePairRct single = temp.CopyTo();

            // update last index
            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_RCT);
            single.NICKNAME = "RECT_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            imageView1.Refresh();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            RDO_TYPE_HOR.Checked = true;
            UC_LOG_VIEWER.WRITE_LOG("RECT Copied.", DEF_OPERATION.OPER_03_PARAM);

        }
        private void BTN_DIG_MODIFY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Modify() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_RECT.Text);

            //// read  for latest roi croodinates
            CMeasurePairRct single = (CMeasurePairRct)imageView1.fm.ElementAt_PairRct(nIndex);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_DIG_NICK.Text) == true) { return; }

            // Change Nick Name
            single.NICKNAME = TXT_PARAM_DIG_NICK.Text;

           

            //-------------------------------------------
            // Change Rectangle type
            int nRC_TypePrev = single.param_01_rc_type; //for default 
            int nRC_TypeCurr = single.param_01_rc_type; //for new 

            /***/if (RDO_TYPE_HOR.Checked == true) nRC_TypeCurr = IFX_RECT_TYPE.DIR_HOR;
            else if (RDO_TYPE_VER.Checked == true) nRC_TypeCurr = IFX_RECT_TYPE.DIR_VER;
            else if (RDO_TYPE_DIA.Checked == true) nRC_TypeCurr = IFX_RECT_TYPE.DIR_DIA;

            #region  Rectangle verification

            if (single.param_01_rc_type == IFX_RECT_TYPE.DIR_DIA)
            {
                // do not touch croodinates
            }
            else
            {
                single.rc_FST = new parseRect(CControl.GetRectangleFrom_TextBox_Set(TXT_RECT_FST_POS_X, TXT_RECT_FST_POS_Y, TXT_RECT_FST_SZ_W, TXT_RECT_FST_SZ_H));
                single.rc_SCD = new parseRect(CControl.GetRectangleFrom_TextBox_Set(TXT_RECT_SCD_POS_X, TXT_RECT_SCD_POS_Y, TXT_RECT_SCD_SZ_W, TXT_RECT_SCD_SZ_H));
            }

            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST.ToRectangleF(), imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_SCD.ToRectangleF(), imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }

            single.CroodinateBackup();
            #endregion

            single.param_01_rc_type = nRC_TypeCurr;
            single.ConvertRectangleType(nRC_TypePrev, nRC_TypeCurr);

            //-------------------------------------------
            // replace !! updated data
            CMeasurePairRct[] array = imageView1.fm.ToArray_PairRct();
            array[nIndex] = (CMeasurePairRct)single;
            imageView1.fm.Figure_Replace(array);
            imageView1.Refresh();

            TXT_PARAM_DIA_ANGLE.Text = single.param_02_rect_angle.ToString();
            //-------------------------------------------
            // Write log 
            m_speaker.Inform_Finished_Parameter_Change();
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            UC_LOG_VIEWER.WRITE_LOG("RECT Modified.", DEF_OPERATION.OPER_03_PARAM);

        }
        private void BTN_DIG_REMOVE_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Remove() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_RECT.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_DIG_NICK.Text) == true) { return; }

            imageView1.fm.Figure_Remove(IFX_FIGURE.PAIR_RCT, nIndex);
            imageView1.Refresh();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            RDO_TYPE_HOR.Checked = true;
            UC_LOG_VIEWER.WRITE_LOG("FIG-RECT Removed.", DEF_OPERATION.OPER_03_PARAM);
        }
        #endregion

        #region CIRCLE
        private void BTN_CIR_ADD_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Add() == DialogResult.No) { return; }

            imageView1.VIEW_Set_Overlay(true);
            CMeasurePairCir single = new CMeasurePairCir();

            PointF ptDraw = imageView1.PT_FIGURE_TO_DRAW;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_CIR);
            single.NICKNAME = "CIRCLE_" + nLastIndex.ToString("N0");

            #region region verification

            single.rc_EX = new RectangleF(ptDraw.X - 50, ptDraw.Y - 50, 100, 100);
            single.rc_IN = new RectangleF(ptDraw.X - 3, ptDraw.Y - 3, 6, 6);
            single.rc_IN = CRect.SetCenter(single.rc_IN, single.rc_EX);

            if (m_speaker.Check_Is_Invalid_Figure(single.rc_EX, imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }
            CControl.SetTextBoxFrom_RectangleF(single.rc_EX, TXT_CIRCLE_POS_X, TXT_CIRCLE_POS_Y, TXT_CIRCLE_SZ_W, TXT_CIRCLE_SZ_H);

            #endregion

            single.CroodinateBackup();

            BASE_RECP baseRecp = imageView1.fm.baseRecp;

            imageView1.fm.Figure_Add(single);
            imageView1.Refresh();

            BTN_FIGURE_REMOVE_Click(null, EventArgs.Empty);
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            UC_LOG_VIEWER.WRITE_LOG("FIG-CIR Added.", DEF_OPERATION.OPER_03_PARAM);
        }
        private void BTN_CIR_COPY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Copy() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_CIRCLE.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_CIR_NICK.Text) == true) { return; }

            CMeasurePairCir temp = (CMeasurePairCir)imageView1.fm.ElementAt_PairCir(nIndex);
            CMeasurePairCir single = temp.CopyTo();

            // update last index
            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_CIR);
            single.NICKNAME = "CIRCLE_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            imageView1.Refresh();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            UC_LOG_VIEWER.WRITE_LOG("FIG-CIR Copied.", DEF_OPERATION.OPER_03_PARAM);
        }
        private void BTN_CIR_MODIFY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Modify() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_CIRCLE.Text);

            //// read  for latest roi croodinates
            CMeasurePairCir single = (CMeasurePairCir)imageView1.fm.ElementAt_PairCir(nIndex);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_CIR_NICK.Text) == true) { return; }
            single.NICKNAME = TXT_PARAM_CIR_NICK.Text;

            #region region verification

            RectangleF rcCircle = CControl.GetRectangleFrom_TextBox_Set(TXT_CIRCLE_POS_X, TXT_CIRCLE_POS_Y, TXT_CIRCLE_SZ_W, TXT_CIRCLE_SZ_H);
            if (m_speaker.Check_Is_Invalid_Figure(rcCircle, imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }

            single.rc_EX = rcCircle;
            single.rc_IN = new RectangleF(3, 3, 6, 6);
            single.rc_IN = CRect.SetCenter(single.rc_IN, single.rc_EX);
            single.CroodinateBackup();

            CControl.SetTextBoxFrom_RectangleF(single.rc_EX, TXT_CIRCLE_POS_X, TXT_CIRCLE_POS_Y, TXT_CIRCLE_SZ_W, TXT_CIRCLE_SZ_H);

            #endregion

            CMeasurePairCir[] array = imageView1.fm.ToArray_PairCir();
            array[nIndex] = (CMeasurePairCir)single;
            imageView1.fm.Figure_Replace(array);
            imageView1.Refresh();

            m_speaker.Inform_Finished_Parameter_Change();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            UC_LOG_VIEWER.WRITE_LOG("FIG-CIR Modified.", DEF_OPERATION.OPER_03_PARAM);

        }
        private void BTN_CIR_REMOVE_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Remove() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_CIRCLE.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_CIR_NICK.Text) == true) { return; }

            imageView1.fm.Figure_Remove(IFX_FIGURE.PAIR_CIR, nIndex);
            imageView1.Refresh();

            UC_LOG_VIEWER.WRITE_LOG("FIG-CIR Removed.", DEF_OPERATION.OPER_03_PARAM);
        }
        #endregion

        #region MIXED_RECTANGLE
        private void BTN_MIX_RC_ADD_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Add() == DialogResult.No) { return; }

            CMeasureMixedRC single = new CMeasureMixedRC();

            #region GET RECTANGLE FIRST AND SECOND

            single.rc_FST = new parseRect(CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_RC_FST_POS_X, TXT_MIXED_RC_FST_POS_Y, TXT_MIXED_RC_FST_SZ_W, TXT_MIXED_RC_FST_SZ_H));
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST.ToRectangleF(), imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) return;

            single.rc_SCD = new parseRect(CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_RC_SCD_POS_X, TXT_MIXED_RC_SCD_POS_Y, TXT_MIXED_RC_SCD_SZ_W, TXT_MIXED_RC_SCD_SZ_H));
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_SCD.ToRectangleF(), imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) return;

            #endregion

            #region SET RECTANGLE DIRECTION
            /***/
            if (RDO_MIXED_RC_FST_DIR_HOR.Checked == true) { single.param_04_rc_type_fst = IFX_RECT_TYPE.DIR_HOR; }
            else if (RDO_MIXED_RC_FST_DIR_VER.Checked == true) { single.param_04_rc_type_fst = IFX_RECT_TYPE.DIR_VER; }

            /***/
            if (RDO_MIXED_RC_SCD_DIR_HOR.Checked == true) { single.param_05_rc_type_scd = IFX_RECT_TYPE.DIR_HOR; }
            else if (RDO_MIXED_RC_SCD_DIR_VER.Checked == true) { single.param_05_rc_type_scd = IFX_RECT_TYPE.DIR_VER; }
            #endregion

            #region SET METRIC TYPE
            /***/
            if (RDO_MIXED_RC_METRIC_P2P.Checked == true) { single.param_07_metric_type = IFX_METRIC.P2P; }
            else if (RDO_MIXED_RC_METRIC_HOR.Checked == true) { single.param_07_metric_type = IFX_METRIC.HOR; }
            else if (RDO_MIXED_RC_METRIC_VER.Checked == true) { single.param_07_metric_type = IFX_METRIC.VER; }
            #endregion

            if (CHK_MIXED_RC_USE_CENTROID.Checked) single.param_08_use_centroid = true;

            single.CroodinateBackup();

            BASE_RECP baseRecp = imageView1.fm.baseRecp;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.MIXED_RC);
            single.NICKNAME = "MRC_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            imageView1.Refresh();

            BTN_FIGURE_REMOVE_Click(null, EventArgs.Empty);
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            UC_LOG_VIEWER.WRITE_LOG(string.Format("{0} Added.", IFX_FIGURE.ToStringType(IFX_FIGURE.MIXED_RC)), DEF_OPERATION.OPER_03_PARAM);

        }
        private void BTN_MIXED_RC_COPY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Copy() == DialogResult.No) { return; }

            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RC.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_RC_NICK.Text) == true) { return; }

            CMeasureMixedRC temp = (CMeasureMixedRC)imageView1.fm.ElementAt_MRC(nIndex);
            CMeasureMixedRC single = temp.CopyTo();

            // update last index
            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.MIXED_RC);
            single.NICKNAME = "MRC_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            imageView1.Refresh();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            UC_LOG_VIEWER.WRITE_LOG("FIG-MRC Copied.", DEF_OPERATION.OPER_03_PARAM);
        }
        private void BTN_MIXED_RC_MODIFY_Click(object sender, EventArgs e)
        {
             if (m_speaker.Figure_Modify() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RC.Text);

            //// read  for latest roi croodinates
            CMeasureMixedRC single = (CMeasureMixedRC)imageView1.fm.ElementAt_MRC(nIndex);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_RC_NICK.Text) == true) { return; }

            // Change Nick Name *******************************************************************
            single.NICKNAME = TXT_MIXED_RC_NICK.Text;

            #region set rectangle type / direction
            int nRC_TYPE_FST = IFX_RECT_TYPE.DIR_HOR;
            int nRC_TYPE_SCD = IFX_RECT_TYPE.DIR_HOR;

            if (RDO_MIXED_RC_FST_DIR_HOR.Checked == true) nRC_TYPE_FST = IFX_RECT_TYPE.DIR_HOR;
            if (RDO_MIXED_RC_FST_DIR_VER.Checked == true) nRC_TYPE_FST = IFX_RECT_TYPE.DIR_VER;

            if (RDO_MIXED_RC_SCD_DIR_HOR.Checked == true) nRC_TYPE_SCD = IFX_RECT_TYPE.DIR_HOR;
            if (RDO_MIXED_RC_SCD_DIR_VER.Checked == true) nRC_TYPE_SCD = IFX_RECT_TYPE.DIR_VER;

            single.param_04_rc_type_fst = nRC_TYPE_FST;
            single.param_05_rc_type_scd = nRC_TYPE_SCD;
            #endregion

            #region set metric type
            int nMetric = IFX_METRIC.P2P;

            if (RDO_MIXED_RC_METRIC_P2P.Checked == true) nMetric = IFX_METRIC.P2P;
            if (RDO_MIXED_RC_METRIC_HOR.Checked == true) nMetric = IFX_METRIC.HOR;
            if (RDO_MIXED_RC_METRIC_VER.Checked == true) nMetric = IFX_METRIC.VER;

            single.param_07_metric_type = nMetric;
            #endregion

            #region region verification
            single.rc_FST = new parseRect(CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_RC_FST_POS_X, TXT_MIXED_RC_FST_POS_Y, TXT_MIXED_RC_FST_SZ_W, TXT_MIXED_RC_FST_SZ_H));
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST.ToRectangleF(), imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }
            single.rc_SCD = new parseRect(CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_RC_SCD_POS_X, TXT_MIXED_RC_SCD_POS_Y, TXT_MIXED_RC_SCD_SZ_W, TXT_MIXED_RC_SCD_SZ_H));
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_SCD.ToRectangleF(), imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }

            single.CroodinateBackup();
            #endregion

            single.param_08_use_centroid = CHK_MIXED_RC_USE_CENTROID.Checked;

            //-------------------------------------------
            // replace !! updated data
            CMeasureMixedRC[] array = imageView1.fm.ToArray_Mixed_RC();
            array[nIndex] = (CMeasureMixedRC)single;
            imageView1.fm.Figure_Replace(array);
            imageView1.Refresh();

            //-------------------------------------------
            // Write log 
            UC_LOG_VIEWER.WRITE_LOG("MIXED RECT Modified.", DEF_OPERATION.OPER_03_PARAM);
            m_speaker.Inform_Finished_Parameter_Change();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
        }
        private void BTN_MIXED_RC_REMOVE_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Remove() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RC.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_RC_NICK.Text) == true) { return; }

            imageView1.fm.Figure_Remove(IFX_FIGURE.MIXED_RC, nIndex);
            imageView1.Refresh();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            UC_LOG_VIEWER.WRITE_LOG("FIG-MRC Removed.", DEF_OPERATION.OPER_03_PARAM);
        }
        #endregion

        #region MIXED CIRCLE
        private void BTN_MIXED_CC_ADD_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Add() == DialogResult.No) { return; }

            CMeasureMixedCC single = new CMeasureMixedCC();

            #region GET RECTANGLE FIRST AND SECOND
            single.rc_FST_EX = CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_CC_FST_POS_X, TXT_MIXED_CC_FST_POS_Y, TXT_MIXED_CC_FST_SZ_W, TXT_MIXED_CC_FST_SZ_H);
            single.rc_SCD_EX = CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_CC_SCD_POS_X, TXT_MIXED_CC_SCD_POS_Y, TXT_MIXED_CC_SCD_SZ_W, TXT_MIXED_CC_SCD_SZ_H);

            RectangleF rcFST_IN = new RectangleF(0, 0, 5, 5);
            RectangleF rcSCD_IN = new RectangleF(0, 0, 5, 5);
            rcFST_IN = CRect.SetCenter(rcFST_IN, single.rc_FST_EX);
            rcSCD_IN = CRect.SetCenter(rcSCD_IN, single.rc_SCD_EX);

            single.rc_FST_IN = rcFST_IN;
            single.rc_SCD_IN = rcSCD_IN;

            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST_EX, imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST_EX, imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }


            #endregion

            #region SET MEASUREMENT DIRECTION
            /***/
            if (RDO_MIXED_CC_FST_DIR_LFT.Checked == true) { single.param_06_ms_pos_fst = IFX_DIR.DIR_LFT; }
            else if (RDO_MIXED_CC_FST_DIR_TOP.Checked == true) { single.param_06_ms_pos_fst = IFX_DIR.DIR_TOP; }
            else if (RDO_MIXED_CC_FST_DIR_RHT.Checked == true) { single.param_06_ms_pos_fst = IFX_DIR.DIR_RHT; }
            else if (RDO_MIXED_CC_FST_DIR_BTM.Checked == true) { single.param_06_ms_pos_fst = IFX_DIR.DIR_BTM; }

            /***/
            if (RDO_MIXED_CC_SCD_DIR_LFT.Checked == true) { single.param_07_ms_pos_scd = IFX_DIR.DIR_LFT; }
            else if (RDO_MIXED_CC_SCD_DIR_TOP.Checked == true) { single.param_07_ms_pos_scd = IFX_DIR.DIR_TOP; }
            else if (RDO_MIXED_CC_SCD_DIR_RHT.Checked == true) { single.param_07_ms_pos_scd = IFX_DIR.DIR_RHT; }
            else if (RDO_MIXED_CC_SCD_DIR_BTM.Checked == true) { single.param_07_ms_pos_scd = IFX_DIR.DIR_BTM; }

            #endregion

            #region SET METRIC TYPE
            /***/
            if (RDO_MIXED_CC_METRIC_P2P.Checked == true) { single.param_08_metric_type = IFX_METRIC.P2P; }
            else if (RDO_MIXED_CC_METRIC_HOR.Checked == true) { single.param_08_metric_type = IFX_METRIC.HOR; }
            else if (RDO_MIXED_CC_METRIC_VER.Checked == true) { single.param_08_metric_type = IFX_METRIC.VER; }
            #endregion

            single.CroodinateBackup();

            BASE_RECP baseRecp = imageView1.fm.baseRecp;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.MIXED_CC);
            single.NICKNAME = "MCC_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            imageView1.Refresh();

            BTN_FIGURE_REMOVE_Click(null, EventArgs.Empty);
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            UC_LOG_VIEWER.WRITE_LOG(string.Format("{0} Added.", IFX_FIGURE.ToStringType(IFX_FIGURE.MIXED_CC)), DEF_OPERATION.OPER_03_PARAM);
        }
        private void BTN_MIXED_CC_COPY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Copy() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_CC.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_CC_NICK.Text) == true) { return; }

            CMeasureMixedCC temp = (CMeasureMixedCC)imageView1.fm.ElementAt_MCC(nIndex);
            CMeasureMixedCC single = temp.CopyTo();

            // update last index
            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.MIXED_CC);
            single.NICKNAME = "MCC_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG("FIG-MCC Copied.", DEF_OPERATION.OPER_03_PARAM);

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

        }
        private void BTN_MIXED_CC_MODIFY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Modify() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_CC.Text);

            //// read  for latest roi croodinates
            CMeasureMixedCC single = (CMeasureMixedCC)imageView1.fm.ElementAt_MCC(nIndex);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_CC_NICK.Text) == true) { return; }

            // Change Nick Name *******************************************************************
            single.NICKNAME = TXT_MIXED_CC_NICK.Text;

            #region SET MEASUREMENT POS
            int nMS_POS_FST = IFX_DIR.DIR_LFT;
            int nMS_POS_SCD = IFX_DIR.DIR_LFT;

            if (RDO_MIXED_CC_FST_DIR_LFT.Checked) nMS_POS_FST = IFX_DIR.DIR_LFT;
            if (RDO_MIXED_CC_FST_DIR_TOP.Checked) nMS_POS_FST = IFX_DIR.DIR_TOP;
            if (RDO_MIXED_CC_FST_DIR_RHT.Checked) nMS_POS_FST = IFX_DIR.DIR_RHT;
            if (RDO_MIXED_CC_FST_DIR_BTM.Checked) nMS_POS_FST = IFX_DIR.DIR_BTM;

            if (RDO_MIXED_CC_SCD_DIR_LFT.Checked) nMS_POS_SCD = IFX_DIR.DIR_LFT;
            if (RDO_MIXED_CC_SCD_DIR_TOP.Checked) nMS_POS_SCD = IFX_DIR.DIR_TOP;
            if (RDO_MIXED_CC_SCD_DIR_RHT.Checked) nMS_POS_SCD = IFX_DIR.DIR_RHT;
            if (RDO_MIXED_CC_SCD_DIR_BTM.Checked) nMS_POS_SCD = IFX_DIR.DIR_BTM;

            single.param_06_ms_pos_fst = nMS_POS_FST;
            single.param_07_ms_pos_scd = nMS_POS_SCD;
            #endregion

            #region SET METRIC TYPE
            int nMetric = IFX_METRIC.P2P;
            if (RDO_MIXED_CC_METRIC_P2P.Checked) nMetric = IFX_METRIC.P2P;
            if (RDO_MIXED_CC_METRIC_HOR.Checked) nMetric = IFX_METRIC.HOR;
            if (RDO_MIXED_CC_METRIC_VER.Checked) nMetric = IFX_METRIC.VER;
            single.param_08_metric_type = nMetric;

            #endregion

            #region rectangle verification

            single.rc_FST_EX = CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_CC_FST_POS_X, TXT_MIXED_CC_FST_POS_Y, TXT_MIXED_CC_FST_SZ_W, TXT_MIXED_CC_FST_SZ_H);
            single.rc_SCD_EX = CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_CC_SCD_POS_X, TXT_MIXED_CC_SCD_POS_Y, TXT_MIXED_CC_SCD_SZ_W, TXT_MIXED_CC_SCD_SZ_H);

            RectangleF rcFST_IN = new RectangleF(0, 0, 5, 5);
            RectangleF rcSCD_IN = new RectangleF(0, 0, 5, 5);
            rcFST_IN = CRect.SetCenter(rcFST_IN, single.rc_FST_EX);
            rcSCD_IN = CRect.SetCenter(rcSCD_IN, single.rc_SCD_EX);

            single.rc_FST_IN = rcFST_IN;
            single.rc_SCD_IN = rcSCD_IN;

            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST_EX, imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST_EX, imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) { return; }

            #endregion

            //-------------------------------------------
            // replace !! updated data
            CMeasureMixedCC[] array = imageView1.fm.ToArray_Mixed_CC();
            array[nIndex] = (CMeasureMixedCC)single;
            imageView1.fm.Figure_Replace(array);

            //-------------------------------------------
            // Write log 
            UC_LOG_VIEWER.WRITE_LOG("FIG-MCC Modified.", DEF_OPERATION.OPER_03_PARAM);
            m_speaker.Inform_Finished_Parameter_Change();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
        }
        private void BTN_MIXED_CC_REMOVE_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Remove() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RC.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_CC_NICK.Text) == true) { return; }

            imageView1.fm.Figure_Remove(IFX_FIGURE.MIXED_CC, nIndex);
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            UC_LOG_VIEWER.WRITE_LOG("FIG-MCC Removed.", DEF_OPERATION.OPER_03_PARAM);
        }
        #endregion

        #region MIXED RECTANGLE AND CIRCLE
        private void BTN_MIXED_RCC_ADD_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Add() == DialogResult.No) { return; }

            CMeasureMixedRCC single = new CMeasureMixedRCC();

            #region GET RECTANGLE FIRST AND SECOND
            single.rc_FST = new parseRect(CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_RCC_FST_POS_X, TXT_MIXED_RCC_FST_POS_Y, TXT_MIXED_RCC_FST_SZ_W, TXT_MIXED_RCC_FST_SZ_H));
            single.rc_FST_EX = CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_RCC_SCD_POS_X, TXT_MIXED_RCC_SCD_POS_Y, TXT_MIXED_RCC_SCD_SZ_W, TXT_MIXED_RCC_SCD_SZ_H);
            single.rc_FST_IN = CRect.SetCenter(new RectangleF(single.rc_FST_EX.X, single.rc_FST_EX.Y, 5, 5), single.rc_FST_EX);

            //*****************************************************************************
            // Size Exception 
            //*****************************************************************************

            int imageW = imageView1.VIEW_GetImageW();
            int imageH = imageView1.VIEW_GetImageH();

            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST.ToRectangleF(), imageW, imageH) == true) return;
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST_EX/**********/, imageW, imageH) == true) return;

            //*****************************************************************************

            #endregion

            #region SET RECTANGLE DIRECTION & MEASUREMENT POSITION
            /***/
            if (RDO_MIXED_RCC_FST_DIR_HOR.Checked) { single.param_02_rc_type_fst = IFX_RECT_TYPE.DIR_HOR; }
            else if (RDO_MIXED_RCC_FST_DIR_VER.Checked) { single.param_02_rc_type_fst = IFX_RECT_TYPE.DIR_VER; }

            /***/
            if (RDO_MIXED_RCC_SCD_DIR_LFT.Checked == true) { single.param_14_ms_pos_scd = IFX_DIR.DIR_LFT; }
            else if (RDO_MIXED_RCC_SCD_DIR_TOP.Checked == true) { single.param_14_ms_pos_scd = IFX_DIR.DIR_TOP; }
            else if (RDO_MIXED_RCC_SCD_DIR_RHT.Checked == true) { single.param_14_ms_pos_scd = IFX_DIR.DIR_RHT; }
            else if (RDO_MIXED_RCC_SCD_DIR_BTM.Checked == true) { single.param_14_ms_pos_scd = IFX_DIR.DIR_BTM; }
            #endregion

            #region SET METRIC TYPE
            /***/
            if (RDO_MIXED_RCC_METRIC_P2P.Checked == true) { single.param_20_metric_type = IFX_METRIC.P2P; }
            else if (RDO_MIXED_RCC_METRIC_HOR.Checked == true) { single.param_20_metric_type = IFX_METRIC.HOR; }
            else if (RDO_MIXED_RCC_METRIC_VER.Checked == true) { single.param_20_metric_type = IFX_METRIC.VER; }
            #endregion

            if (CHK_MIXED_RCC_USE_CENTROID.Checked == true) single.param_04_use_centroid = true;

            single.CroodinateBackup();

            BASE_RECP baseRecp = imageView1.fm.baseRecp;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.MIXED_RCC);
            single.NICKNAME = "MRCC_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            imageView1.Refresh();

            BTN_FIGURE_REMOVE_Click(null, EventArgs.Empty);
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);


            UC_LOG_VIEWER.WRITE_LOG(string.Format("{0} Added.", IFX_FIGURE.ToStringType(IFX_FIGURE.MIXED_RCC)), DEF_OPERATION.OPER_03_PARAM);
        }
        private void BTN_MIXED_RCC_COPY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Copy() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RCC.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_RCC_NICK.Text) == true) { return; }

            CMeasureMixedRCC temp = (CMeasureMixedRCC)imageView1.fm.ElementAt_MRCC(nIndex);
            CMeasureMixedRCC single = temp.CopyTo();

            // update last index
            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.MIXED_RCC);
            single.NICKNAME = "MRCC_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG("FIG-MRCC Copied.", DEF_OPERATION.OPER_03_PARAM);

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
        }
        private void BTN_MIXED_RCC_MODIFY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Modify() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RCC.Text);

            //// read  for latest roi croodinates
            CMeasureMixedRCC single = (CMeasureMixedRCC)imageView1.fm.ElementAt_MRCC(nIndex);


            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_RCC_NICK.Text) == true) { return; }

            //-------------------------------------------
            // Change Nick Name
            single.NICKNAME = TXT_MIXED_RCC_NICK.Text;

            #region region verification

            single.rc_FST/******/= new parseRect(CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_RCC_FST_POS_X, TXT_MIXED_RCC_FST_POS_Y, TXT_MIXED_RCC_FST_SZ_W, TXT_MIXED_RCC_FST_SZ_H));
            single.rc_FST_EX/***/= CControl.GetRectangleFrom_TextBox_Set(TXT_MIXED_RCC_SCD_POS_X, TXT_MIXED_RCC_SCD_POS_Y, TXT_MIXED_RCC_SCD_SZ_W, TXT_MIXED_RCC_SCD_SZ_H);
            single.rc_FST_IN/***/= CRect.SetCenter(new RectangleF(single.rc_FST_EX.X, single.rc_FST_EX.Y, 5, 5), single.rc_FST_EX);

            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST.ToRectangleF(), imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) return;
            if (m_speaker.Check_Is_Invalid_Figure(single.rc_FST_EX/**********/, imageView1.VIEW_GetImageW(), imageView1.VIEW_GetImageH()) == true) return;

            #endregion

            #region SET RECTANGLE DIRECTION & MEASUREMENT POSITION
            /***/
            if (RDO_MIXED_RCC_FST_DIR_HOR.Checked) { single.param_02_rc_type_fst = IFX_RECT_TYPE.DIR_HOR; }
            else if (RDO_MIXED_RCC_FST_DIR_VER.Checked) { single.param_02_rc_type_fst = IFX_RECT_TYPE.DIR_VER; }

            /***/
            if (RDO_MIXED_RCC_SCD_DIR_LFT.Checked == true) { single.param_14_ms_pos_scd = IFX_DIR.DIR_LFT; }
            else if (RDO_MIXED_RCC_SCD_DIR_TOP.Checked == true) { single.param_14_ms_pos_scd = IFX_DIR.DIR_TOP; }
            else if (RDO_MIXED_RCC_SCD_DIR_RHT.Checked == true) { single.param_14_ms_pos_scd = IFX_DIR.DIR_RHT; }
            else if (RDO_MIXED_RCC_SCD_DIR_BTM.Checked == true) { single.param_14_ms_pos_scd = IFX_DIR.DIR_BTM; }
            #endregion

            #region SET METRIC TYPE
            /***/
            if (RDO_MIXED_RCC_METRIC_P2P.Checked == true) { single.param_20_metric_type = IFX_METRIC.P2P; }
            else if (RDO_MIXED_RCC_METRIC_HOR.Checked == true) { single.param_20_metric_type = IFX_METRIC.HOR; }
            else if (RDO_MIXED_RCC_METRIC_VER.Checked == true) { single.param_20_metric_type = IFX_METRIC.VER; }
            #endregion

            if (CHK_MIXED_RCC_USE_CENTROID.Checked == true) single.param_04_use_centroid = true;

            //-------------------------------------------
            // replace !! updated data
            CMeasureMixedRCC[] array = imageView1.fm.ToArray_Mixed_RCC();
            array[nIndex] = (CMeasureMixedRCC)single;
            imageView1.fm.Figure_Replace(array);

            //-------------------------------------------
            // Write log  
            UC_LOG_VIEWER.WRITE_LOG("FIG-MRCC Modified.", DEF_OPERATION.OPER_03_PARAM);
            m_speaker.Inform_Finished_Parameter_Change();

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            imageView1.Refresh();
        }
        private void BTN_MIXED_RCC_REMOVE_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Remove() == DialogResult.No) { return; }

            int nIndex = Convert.ToInt32(TXT_INDEX_OF_MIXED_RCC.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_MIXED_RCC_NICK.Text) == true) { return; }

            imageView1.fm.Figure_Remove(IFX_FIGURE.MIXED_RCC, nIndex);
            UC_LOG_VIEWER.WRITE_LOG("FIG-MRCC Removed.", DEF_OPERATION.OPER_03_PARAM);

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            imageView1.Refresh();
        }
        #endregion

        #region OVERALY
        private void BTN_OL_ADD_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Add() == DialogResult.No) { return; }

            imageView1.VIEW_Set_Overlay(true);
            CMeasurePairOvl single = new CMeasurePairOvl();

            PointF ptDraw = imageView1.PT_FIGURE_TO_DRAW;

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_OVL);
            single.NICKNAME = "OVERLAY_" + nLastIndex.ToString("N0");

            single.RC_VER_EX.rc_FST = new parseRect(ptDraw.X - 300, ptDraw.Y - 50, 50, 100);
            single.RC_VER_EX.rc_SCD = new parseRect(ptDraw.X + 250, ptDraw.Y - 50, 50, 100);

            single.RC_VER_IN.rc_FST = new parseRect(ptDraw.X - 150, ptDraw.Y - 50, 50, 100);
            single.RC_VER_IN.rc_SCD = new parseRect(ptDraw.X + 100, ptDraw.Y - 50, 50, 100);

            single.RC_HOR_EX.rc_FST = new parseRect(ptDraw.X - 050, ptDraw.Y - 300, 100, 50);
            single.RC_HOR_EX.rc_SCD = new parseRect(ptDraw.X - 050, ptDraw.Y + 250, 100, 50);

            single.RC_HOR_IN.rc_FST = new parseRect(ptDraw.X - 050, ptDraw.Y - 150, 100, 50);
            single.RC_HOR_IN.rc_SCD = new parseRect(ptDraw.X - 050, ptDraw.Y + 100, 100, 50);

            single.CroodinateBackup();

            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG("FIG-OVL Added.", DEF_OPERATION.OPER_03_PARAM);

            BTN_FIGURE_REMOVE_Click(null, EventArgs.Empty);
            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);

            imageView1.Refresh();
        }
        private void BTN_OL_COPY_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Copy() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_OVL.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_OVL_NICK.Text) == true) { return; }

            CMeasurePairOvl temp = (CMeasurePairOvl)imageView1.fm.ElementAt_PairOvl(nIndex);
            CMeasurePairOvl single = temp.CopyTo();

            int nLastIndex = imageView1.fm.GetFigureEmptyIndex(IFX_FIGURE.PAIR_OVL);
            single.NICKNAME = "OVERLAY_" + nLastIndex.ToString("N0");

            imageView1.fm.Figure_Add(single);
            UC_LOG_VIEWER.WRITE_LOG("FIG-OVL Copied.", DEF_OPERATION.OPER_03_PARAM);

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            imageView1.Refresh();

        }
        private void BTN_OL_REMOVE_Click(object sender, EventArgs e)
        {
            if (m_speaker.Figure_Remove() == DialogResult.No) { return; }
            int nIndex = Convert.ToInt32(TXT_INDEX_OF_OVL.Text);

            // Void Check for figure name *********************************************************
            if (m_speaker.Check_Is_Error_Figure_Selection_Validity(TXT_PARAM_OVL_NICK.Text) == true) { return; }

            imageView1.fm.Figure_Remove(IFX_FIGURE.PAIR_OVL, nIndex);
            UC_LOG_VIEWER.WRITE_LOG("FIG-OVL Removed.", DEF_OPERATION.OPER_03_PARAM);

            BTN_FIGURE_REFRESH_Click(null, EventArgs.Empty);
            imageView1.Refresh();
        }
        #endregion

       

        #endregion

       
        #endregion

        private void LB_RECP_NAME_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Load Final Recp Used Most Recently?","PREVIOUS RECP LOAD", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _INI_LOAD_Program();
                string strRecpFile = this.param_program.i00_previous_recp;
                string strFullFilePath = Path.Combine(imageView1.fm.param_path.i04_PATH_RECP_REAL, strRecpFile);
                _Recp_Change(strFullFilePath);
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
