using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

using System.Timers;
using System.Collections.Generic;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using DispObject;
using CD_Figure;
using CD_Paramter;
using CD_View;

 
namespace CD_VISION_DIALOG
{
    public interface iPtrn
    {
        void/*****/ iPtrn_Init();
        Bitmap/***/ iPtrn_LoadImage(); 
        double/***/ iPtrn_DoPtrn(Bitmap bmpSource, Bitmap bmpTemplate, double fRatio, RectangleF rcSearching, out RectangleF rcTemplate, out PointF ptTemplateCenter);
    }

    

    public class CIterativeStaticRun
    {
        private bool bStaticRun = false; // static repeatability test
        public bool IS_STATIC_RUN { get { return bStaticRun; } set { bStaticRun = value; } }
        public int STACK_SIZE { get { return listSequence.Count(); } }

        public Stack<CInspUnit> listSequence = new Stack<CInspUnit>();

        public void SetInit()
        {
            listSequence.Clear();
            IS_STATIC_RUN = false;
        }

        public void AppendItem_Single(byte[] rawImage, int imageW, int imageH, int nCamNo, CFigureManager fm)
        {
            CInspUnit iu = new CInspUnit();
            iu.AppendItem_Single(rawImage, imageW, imageH, fm, nCamNo);
            listSequence.Push(iu);
        }
       
    }
    
    public static class CControl
    {
        public static RectangleF GetRectangleFrom_TextBox_Set(TextBox txt_pos_x, TextBox txt_pos_y, TextBox txt_size_w, TextBox txt_size_h)
        {
            float fx = 0; float fy = 0;
            float fw = 0; float fh = 0;

            float.TryParse(txt_pos_x.Text, out fx);
            float.TryParse(txt_pos_y.Text, out fy);
            float.TryParse(txt_size_w.Text, out fw);
            float.TryParse(txt_size_h.Text, out fh);

            return new RectangleF(fx, fy, fw, fh);;
        }
        public static void SetTextBoxFrom_RectangleF(RectangleF rc, TextBox txt_pos_x, TextBox txt_pos_y, TextBox txt_size_w, TextBox txt_size_h)
        {
            txt_pos_x.Text/****/= rc/*********/.X.ToString("N0").Replace(",", "");
            txt_pos_y.Text/****/= rc/*********/.Y.ToString("N0").Replace(",", "");
            txt_size_w.Text/***/= rc/*****/.Width.ToString("N0").Replace(",", "");
            txt_size_h.Text/***/= rc/****/.Height.ToString("N0").Replace(",", "");
        }
        public static void SetTextBoxFrom_RectangleF(double fx, double fy, TextBox txt_fa, TextBox txt_fb)
        {
            txt_fa.Text = fx.ToString("N0").Replace(",", "");
            txt_fb.Text = fy.ToString("N0").Replace(",", "");
        }
        public static double GetDoubleFrom_TextBox(TextBox txt)
        {
            double f = 0;
            double.TryParse(txt.Text, out f);
            return f;   
        }
    }

    public class CLinguisticHelper
    {
        public const int ENG = 0;
        public const int KOR = 1;

        public int LANGUAGE { get; set; }

        public CLinguisticHelper(int nLanguage = 0)
        {
            LANGUAGE = nLanguage;
        }

        //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
        #region COMMON AND GENERAL 
        public string LANG_PATH_ERROR_EMPTY_STRING/*****/{ get { return LANGUAGE == ENG ? "File Path Is Not Valid." : "파일 패스가 올바르지 않습니다."; } }
        public string LANG_PATH_ERROR_FILE_NOT_EXIST/***/{ get { return LANGUAGE == ENG ? "File Not Found." : "파일이 존재하지 않습니다."; } }
        public string LANG_FINISH_PARAMETER_CHANGE/*****/{ get { return LANGUAGE == ENG ? "Parameter Has Changed." : "파라미터가 변경되었습니다."; } }
        public string LANG_INDEX_ERROR/*****************/{ get { return LANGUAGE == ENG ? "Invalid Index" : "Index가 올바르지 않습니다."; } }
        //******************************************************************************************
        public DialogResult Inform_Finished_Parameter_Change()
        {
            return MessageBox.Show(LANG_FINISH_PARAMETER_CHANGE, "CHANGE PARAMETER", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //******************************************************************************************
        public bool/*******/Check_Is_Error_File_Path_Validity(string strPath)
        {
            bool isInvalid = false;

            if (strPath == string.Empty)
            {
                MessageBox.Show(string.Format("{0}\nPATH :{1}", LANG_PATH_ERROR_EMPTY_STRING, strPath), "INVALID FILE PATH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isInvalid = true;
            }
            else if (File.Exists(strPath) == false)
            {
                MessageBox.Show(string.Format("{0}\nPATH :{1}", LANG_PATH_ERROR_FILE_NOT_EXIST, strPath), "INVALID FILE PATH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isInvalid = true;
            }
            return isInvalid;
        }
        public bool/*******/Check_is_Error_Index_Validity(int nIndex)
        {
            if (nIndex < 0)
            {
                MessageBox.Show(LANG_INDEX_ERROR, "INDEX ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
        //******************************************************************************************
        
        #endregion

        //○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○
        //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
        #region FUNCTIONS FIGURE RELATED
        public string LANG_FIGURE_ASK_ADD/********************/{ get { return LANGUAGE == ENG ? "Do You Want To Add New Figure?" : "도형을 새로 그리시겠습니까?"; } }
        public string LANG_FIGURE_ASK_COPY/*******************/{ get { return LANGUAGE == ENG ? "Do You Want To Copy New Figure?" : "도형을 복사하시겠습니까?"; } }
        public string LANG_FIGURE_ASK_MODIFY/*****************/{ get { return LANGUAGE == ENG ? "Do You Want To Modify Selected Figure?" : "선택된 도형의 설정을 변경하시겠습니까?"; } }
        public string LANG_FIGURE_ASK_REMOVE/*****************/{ get { return LANGUAGE == ENG ? "Do You Want To Remove Selected Figure?" : "선택된 도형을 삭제하시겠습니까?"; } }
        public string LANG_FIGURE_ASK_REMOVE_ALL/*************/{ get { return LANGUAGE == ENG ? "Do You Want To Remove All Figures?" : "모든 도형을 삭제하시겠습니까?"; } }
        public string LANG_FIGURE_ERROR_SELECTION_VALIDITY/***/{ get { return LANGUAGE == ENG ? "Invalid Target Selection." : "대상 선택이 올바르지 않습니다."; } }
        //******************************************************************************************
        public DialogResult Figure_Add()/**********/{ return MessageBox.Show(LANG_FIGURE_ASK_ADD, "CREATE FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
        public DialogResult Figure_Copy()/*********/{ return MessageBox.Show(LANG_FIGURE_ASK_COPY, "COPY FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
        public DialogResult Figure_Modify()/*******/{ return MessageBox.Show(LANG_FIGURE_ASK_MODIFY, "MODIFY FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
        public DialogResult Figure_Remove()/*******/{ return MessageBox.Show(LANG_FIGURE_ASK_REMOVE, "REMOVE FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
        public DialogResult Figure_Remove_All()/***/{ return MessageBox.Show(LANG_FIGURE_ASK_REMOVE_ALL, "REMOVE FIGURE", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
        //******************************************************************************************
        public bool Check_Is_Error_Figure_Selection_Validity(string strName)
        {
            bool isInvalid = false;
            if (strName == string.Empty)
            {
                MessageBox.Show(LANG_FIGURE_ERROR_SELECTION_VALIDITY, "INVALID TARGET SELECTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isInvalid = true;
            }
            return isInvalid;
        }
        //******************************************************************************************
        public bool Check_Is_Invalid_Figure(RectangleF rc, int imageW, int imageH)
        {
            bool isInvalid = false;

            // size error
            if (rc.Width == 0 || rc.Height == 0) isInvalid |= true;

            // position error
            if (CRect.isValid(rc, imageW, imageH) == false) isInvalid |= true;

            if (isInvalid == true)
            {
                MessageBox.Show("A Given Region is not Valid\n Please Check Data Again.", "INVALID FIGURE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isInvalid;
        }
        #endregion

        //○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○
        //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
        #region INI_RELATED
        public string LANG_INI_ERROR_FILE_NOT_FOUND/******/{ get { return LANGUAGE == ENG ? "INI FILE NOT FOUND." : "INI 파일이 존재하지 않습니다."; } }
        public string LANG_RECP_FINISH_SAVE_RECP/*********/{ get { return LANGUAGE == ENG ? "Recipe Has Saved." : "레시피가 저장되었습니다."; } }
        public string LANG_RECP_ASK_CHANGE_RECP/**********/{ get { return LANGUAGE == ENG ? "Do You Want To Change Recp?" : "레시피를 교체하시겠습니까?"; } }
        public string LANG_RECP_ASK_CREATE/***************/{ get { return LANGUAGE == ENG ? "Current Recp Will Be Initialized.\nDo You Want To Proceed?" : "레시피가 초기화 됩니다.진행하시겠습니까?"; } }
        public string LANG_RECP_ASK_OVERWRITE/************/{ get { return LANGUAGE == ENG ? "Do You Want To Overwrite?" : "레시가 중복되어도 덮어쓰시겠습니까?"; } }

        public bool Check_INI_Does_Exist_File(string strPath)
        {
            if (File.Exists(strPath) == false)
            {
                MessageBox.Show(LANG_INI_ERROR_FILE_NOT_FOUND, "INI FILE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool Check_RECP_Ask_Change_Recp()
        {
            if (MessageBox.Show(LANG_RECP_ASK_CHANGE_RECP, "CHANGE RECP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;   
            }
            return true;
        }
        public bool Check_And_Ask_Further_Do_Create_Recp()
        {
            if (MessageBox.Show(LANG_RECP_ASK_CREATE, "CREATE NEW RECP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
        public bool Check_And_Ask_Further_Do_Overwrite_Recp()
        {
            if (MessageBox.Show(LANG_RECP_ASK_OVERWRITE, "OVERWRITE RECP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
        public void Inform_Finished_Save_Recp()
        {
            MessageBox.Show(LANG_RECP_FINISH_SAVE_RECP, "SAVE RECIEPE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
       #endregion

        //○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○
        //●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●●
       #region MAIN MENU START OR WORKING PROCEED CHECK
        public string LANG_ASK_FINISH_MAIN_PROGRAM/********/{ get { return LANGUAGE == ENG ? "Do You Want To Exit This Program?" : "프로그램을 종료하시겠습니까?"; } }
        public string LANG_INFORM_INTERRUPTION_OCCURED/****/{ get { return LANGUAGE == ENG ? "User Called Abrupt Interruption." : "사용자에 의한 작업중단이 요청되었습니다."; } }

        public string LANG_PTRN_ASK_TEACH_NAME_SYNC/*******/{ get { return LANGUAGE == ENG ? "Do You Want To Make Sync. Recp Name and PTRN Teaching Name?" : "레시피 명과 동기화를 수행하시겠습니까?"; } }
        public string LANG_PTRN_ERROR_TEACHING_FILE/*******/{ get { return LANGUAGE == ENG ? "Teaching Data Not Found, Do You Want To Proceed?" : "티칭 데이터가 존재하지 않습니까. 티칭을 진행하시겠습니까?"; } }
        public string LANG_PTRN_ERROR_ACCEPTANCE_RATIO/****/{ get { return LANGUAGE == ENG ? "Invalid Acceptance Ratio." : "매칭 허용 수치가 올바르지 않습니다."; } }
        //******************************************************************************************
        public bool/*******/Check_Ptrn_Is_Error_Teaching_File_Validity(string strFilePath)
        {
            if (strFilePath == string.Empty || File.Exists(strFilePath) == false)
            {
                if (MessageBox.Show(LANG_PTRN_ERROR_TEACHING_FILE, "INVALID TEACHING DATA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return true;
                }
            }
            return false;
        }
        public bool/*******/Check_Ptrn_Ask_teaching_name_syncronization()
        {
            if (MessageBox.Show(LANG_PTRN_ASK_TEACH_NAME_SYNC, "TEACHING FILE NAME SYNC.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }
            return true;
        }
        public bool/*******/Check_Ptrn_Is_Error_Acceptance_ratio(double fAccR)
        {
            if (fAccR < 0 || fAccR < 1.0)
            {
                MessageBox.Show(LANG_PTRN_ERROR_ACCEPTANCE_RATIO, "PARAMETER ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
        public bool/*******/Check_And_Ask_Further_Do_Program_Exit()
        {
            if (MessageBox.Show(LANG_ASK_FINISH_MAIN_PROGRAM, "PROGRAM EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }
            return true;
        }
        public void/*******/Inform_Interruption_occred()
        {
            MessageBox.Show(LANG_INFORM_INTERRUPTION_OCCURED, "INTERRUPTION ACTIVATED", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion
        //○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○○


    }
}

