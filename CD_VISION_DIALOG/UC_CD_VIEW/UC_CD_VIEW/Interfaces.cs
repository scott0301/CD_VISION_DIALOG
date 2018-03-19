using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

using DispObject;
using CD_Figure;

namespace CD_View
{
    //*********************************************************************************************
    // interface for popup window
    //*********************************************************************************************

    public interface iPopupROI
    {
        void iSetOverlayStatus(bool bDraw);
        bool iGetOverlayStatus();

        void iDrawROI(bool bDraw);
        void iDrawFocus(bool bDraw);
        void iDrawPtrn(bool bDraw);

        int/*********/iGet_Roi_Count();
        int/*********/iGet_Roi_Index(Point pt);
        Rectangle/***/iGet_Roi_Focus();
        Rectangle/***/iGet_Roi_Ptrn();

        void/********/iSet_Roi_Ptrn(Rectangle rc);
        void/********/iSet_Roi_Focus(Rectangle rc);

        void/********/iRemove_Roi_Focus();
        void/********/iRemove_Roi_Ptrn();
        void/********/iRemove_Roi_At(int nIndex);
        void/********/iRemove_Roi_All();

        void/********/iSave_Roi_At(int nIndex);
        string/******/iSave_Roi_Ptrn(string filename ="");
        
        //******************************************************************************************
        // for vertical rect pair

        void /*********/iMod_RectPair_DigonalAngle(int nIndex, int nAngle); // for angle adjustment

        //*****************************************************************************************
        // shared functions for paired figure 
        void iGet_CropsRectPairNormal(RectangleF rc1, RectangleF rc2, out byte[] crop1, out byte[] crop2);
        void iGet_CropsRectPairDigonal(CMeasurePairRct single, out byte[] crop1, out byte[] crop2);
        void iGet_CropsCircle(CMeasurePairCir single, out byte[] crop1, out byte[] crop2);

        //*****************************************************************************************
        // common functions for all figures

        void /********/iMod_Figure(object single, int nIndex);
        void /********/iAdj_Figure(int nFigureType, int nIndex, int nAction, int x, int y);
        void /********/iAdj_Overlay(int nAction, int nTarget, int nIndex,  int nDir, int nScale, bool bHorizontal, bool bVertical);
        void /********/iAdd_Figure(object figure);
        void /********/iDel_Figure(int nFigureType, int nIndex);
        object /******/iGet_Figure(int nFigureType, int nIndex);
        CFigureManager iGet_AllData();
        
    }

}
