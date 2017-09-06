using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

using CD_Figure;

namespace CD_View
{
    public partial class FormPopup : Form
    {
        private Point m_WindowPosition = new Point(0, 0);

        iPopupROI m_pHandle = null;
        
        Point m_ptStart = new Point(0,0);

        int m_nCurrRoi = 0;

        public FormPopup(iPopupROI pHandle, Point ptClicked, Point ptDlg)
        {
            InitializeComponent();

            // popup windows position setting
            this.StartPosition =  FormStartPosition.Manual;
            this.Location =  ptDlg;
            m_ptStart = ptClicked;

            // get interface handle
            m_pHandle = pHandle;

            //*************************************************************************************
            //Information update 

            TXT_MOUSE_POS_X.Text = ptClicked.X.ToString("N0");
            TXT_MOUSE_POS_Y.Text = ptClicked.Y.ToString("N0");

            bool bCurrentOverlayStatus = m_pHandle.iGetOverlayStatus();
            CHK_DRAW_OVERLAY.Checked = bCurrentOverlayStatus;

            //int nROI = m_pHandle.iReadRoiCount();
            //m_nCurrRoi = nRoiIndex;
            
        }
        //*************************************************************************************
        #region FORM DEFAULT BASIC FUCKING FUNCTIONS
        //*************************************************************************************
        private void FormPopup_Load(object sender, EventArgs e)
        {

           
         
        }

        #region EVENTS FOR POPUP HEADER WINDOW
        private void FORM_HEADER_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                m_WindowPosition = new Point(-e.X, -e.Y);
        }
        private void FORM_HEADER_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point
                    (
                    this.Location.X + (m_WindowPosition.X + e.X),
                    this.Location.Y + (m_WindowPosition.Y + e.Y)
                    );
                // 마우스의 이동치를 Form Location에 반영한다.
            }
        }
        private void FORM_HEADER_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            using (Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0), 65f))
            {
                graphics.FillRectangle(b, gradient_rectangle);
            }
        }

        #endregion

        private void BTN_TEACHING_FORM_CLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //*************************************************************************************
        #endregion
       
        //*****************************************************************************************
        #region SINGLE RECTANGLE EVENTS
        //*****************************************************************************************
        private void BTN_ROI_DEL_Click(object sender, EventArgs e)
        {
            m_pHandle.iRemove_Roi_At(m_nCurrRoi);
        }

        private void CHK_DRAW_OVERLAY_CheckedChanged(object sender, EventArgs e)
        {
            bool bCurrentOverlayStatus = CHK_DRAW_OVERLAY.Checked;

            m_pHandle.iDrawROI(bCurrentOverlayStatus);
        }
       
        //*****************************************************************************************
        #endregion
        
        
      

       

        

        

       

        

        
       



       

      

       
       

     
        

       
       

       

       

       

       

        
     
        

        

       
       

      
      
        

        
         
    }
}

