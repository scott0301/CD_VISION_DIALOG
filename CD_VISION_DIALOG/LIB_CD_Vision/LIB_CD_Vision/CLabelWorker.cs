using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using DispObject;

namespace CD_Measure
{
    public class CLabelWorker
    {
        public List<CSegment> listFull = new List<CSegment>();

        public int[] map = null;

        public int mapW = 0;
        public int mapH = 0;
        public int INDEXER = 0;

        public int COUNT_LABEL { get { return listFull.Count; } }

        public void Set(byte[] rawImage, int imageW, int imageH)
        {
            map = Enumerable.Repeat(-1, imageW * imageH).ToArray();
            mapW = imageW;
            mapH = imageH;
        }
        // Remove Rest Rectangle which occurs on the border. 
        public void Remove_BoundingRect()
        {
            List<CSegment> listBuff = new List<CSegment>();


            for (int i = 0; i < listFull.Count(); i++)
            {
                CSegment seg = listFull.ElementAt(i);

                Rectangle rc = seg.RC;

                if (CRect.IsBoarderPosition(rc, mapW - 1, mapH - 1) == false)
                {
                    listBuff.Add(seg);
                }

            }
            listFull = listBuff.ToList();

        }
        // Extract all the segements
        public void ExtractSegments(byte[] rawImage, int imageW, int imageH)
        {
            int nCurrPos = 0;
            int nCurrVal = 0;

            for (int y = 0; y < mapH; y++)
            {
                for (int x = 0; x < mapW; x++)
                {
                    if (map[y * imageW + x] == -1)
                    {
                        nCurrPos = y * imageW + x;
                        nCurrVal = rawImage[y * imageW + x];

                        List<Point> segment = ExtractSingleSegment(rawImage, imageW, imageH, x, y, nCurrVal);
                        CSegment seg = new CSegment(segment, this.INDEXER);
                        listFull.Add(seg);
                    }
                }
            }
        }
        // get segment by indext. have to run after extraction 
        public CSegment GetSegmentByIndex(int nIndex)
        {
            if (listFull.Count >= nIndex)
            {
                CSegment seg = listFull.ElementAt(nIndex);
                return seg;
            }
            return new CSegment();

        }
        // get segment points by index. have to run after extraction
        public List<Point> GetAllPointsByIndex(int nIndex)
        {
            CSegment seg = listFull.ElementAt(nIndex);

            return seg.list.ToList();
        }

        // get the color by index. 
        public Color GetColorByIndex(int nIndex)
        {
            CSegment seg = listFull.ElementAt(nIndex);

            return seg.c;

        }


        // internal object extraction function
        public List<Point> ExtractSingleSegment(byte[] rawImage, int imageW, int imageH, int px, int py, int nCurrValue)
        {
            List<Point> list = new List<Point>();
            Stack<Point> stack = new Stack<Point>();

            SolveStack(rawImage, imageW, imageH, px, py, ref stack, nCurrValue);

            while (stack.Count != 0)
            {
                Point ptCurr = stack.Pop();
                list.Add(ptCurr);
                SolveStack(rawImage, imageW, imageH, ptCurr.X, ptCurr.Y, ref stack, nCurrValue);
            }

            INDEXER++;

            return list;
        }


        public void SolveStack(byte[] rawImage, int imageW, int imageH, int px, int py, ref Stack<Point> stack, int nCurrValue)
        {
            int P10 = (py - 1) * imageW + (px - 1);
            int P12 = (py - 1) * imageW + (px + 0);
            int P02 = (py - 1) * imageW + (px + 1);

            int P09 = (py + 0) * imageW + (px - 1);
            int P00 = (py + 0) * imageW + (px + 0);
            int P03 = (py + 0) * imageW + (px + 1);

            int P08 = (py + 1) * imageW + (px - 1);
            int P06 = (py + 1) * imageW + (px + 0);
            int P04 = (py + 1) * imageW + (px + 1);

            //*************************************************************************************
            // upper
            //*************************************************************************************
            if (isvalidPos(P10) && rawImage[P10] == nCurrValue && map[P10] == -1)  // time 10 [-1.-1]
            {
                if (py != 0 && px != 0)
                {
                    stack.Push(GetValidPoint(P10)); map[P10] = INDEXER;
                }
            }
            if (isvalidPos(P12) && rawImage[P12] == nCurrValue && map[P12] == -1)  // time 12 [0,-1]
            {
                if (py != 0)
                {
                    stack.Push(GetValidPoint(P12)); map[P12] = INDEXER;
                }
            }
            if (isvalidPos(P02) && rawImage[P02] == nCurrValue && map[P02] == -1)  // time 02 [+1, -1]
            {
                if (py != 0 && px < imageW - 1)
                {
                    stack.Push(GetValidPoint(P02)); map[P02] = INDEXER;
                }
            }

            //*************************************************************************************
            // middle 
            //*************************************************************************************
            if (isvalidPos(P09) && rawImage[P09] == nCurrValue && map[P09] == -1)  // time 09 [-1, 0]
            {
                if (px > 0)
                {
                    stack.Push(GetValidPoint(P09)); map[P09] = INDEXER;
                }
            }
            if (isvalidPos(P00) && rawImage[P00] == nCurrValue && map[P00] == -1)  // time 00 [0,0]
            {
                stack.Push(GetValidPoint(P00)); map[P00] = INDEXER;
            }
            if (isvalidPos(P03) && rawImage[P03] == nCurrValue && map[P03] == -1)  // time 03 [+1, 0]
            {
                if (px < imageW - 1)
                {
                    stack.Push(GetValidPoint(P03)); map[P03] = INDEXER;
                }
            }

            //*************************************************************************************
            // lower
            //*************************************************************************************
            if (isvalidPos(P08) && rawImage[P08] == nCurrValue && map[P08] == -1)  // time 08 [ -1, +1]
            {
                if (px != 0 && py != imageH)
                {
                    stack.Push(GetValidPoint(P08)); map[P08] = INDEXER;
                }
            }
            if (isvalidPos(P06) && rawImage[P06] == nCurrValue && map[P06] == -1)  // time 06 [ 0, +1 ]
            {
                if (py != imageH)
                {
                    stack.Push(GetValidPoint(P06)); map[P06] = INDEXER;
                }
            }
            if (isvalidPos(P04) && rawImage[P04] == nCurrValue && map[P04] == -1) // time 04 [ +1, +1]
            {
                if (px < imageW - 1 && py < imageH - 1)
                {
                    stack.Push(GetValidPoint(P04)); map[P04] = INDEXER;
                }
            }

        }

        private Point GetValidPoint(int nIndexPoint)
        {
            int y = nIndexPoint / mapW;
            int x = nIndexPoint % mapW;
            return new Point(x, y);

        }
        public bool isvalidPos(int nValue)
        {
            if (nValue < 0 || nValue >= map.Length) return false;
            return true;
        }

    }

    public class CSegment
    {
        public List<Point> list = new List<Point>();
        public Rectangle RC { get; set; }
        public PointF ptCenter{get{return CRect.GetCenter(RC);}}

        public Color c = Color.Red;
        public int COUNT_POINT { get { return list.Count(); } }
        public int INDEX { get; set; }

        // creator an empty object
        public CSegment()
        {
            this.list = new List<Point>();
            this.RC = new Rectangle();
            this.INDEX = 0;

        }
        // create an object from data
        public CSegment(List<Point> list, int colorIndex)
        {
            this.list = list;
            this.INDEX = colorIndex;
            _CalcRectangle();
        }
        // get the fucking boundary rectangle for each object, this called automatically as an internal function
        // when generate new object from the data.
        private void _CalcRectangle()
        {
            if (list.Count != 0)
            {
                RC = CPoint.GetBoundingRect(this.list);
                return;
            }
            RC = new Rectangle();
        }
        // user-defined pseudo color code.
        public Color GetIndexColor()
        {
            Color c = Color.Empty;

            if/***/ (INDEX == 00) { c = Color.Red; }
            else if (INDEX == 01) { c = Color.Green; }
            else if (INDEX == 02) { c = Color.Blue; }
            else if (INDEX == 03) { c = Color.Beige; }
            else if (INDEX == 04) { c = Color.Purple; }
            else if (INDEX == 05) { c = Color.Brown; }
            else if (INDEX == 06) { c = Color.Coral; }
            else if (INDEX == 07) { c = Color.Cyan; }
            else if (INDEX == 08) { c = Color.DeepSkyBlue; }
            else if (INDEX == 09) { c = Color.Gold; }
            else if (INDEX == 10) { c = Color.Green; }
            else if (INDEX == 11) { c = Color.HotPink; }
            else if (INDEX == 12) { c = Color.Khaki; }
            else if (INDEX == 13) { c = Color.Lavender; }
            else if (INDEX == 14) { c = Color.LightBlue; }
            else if (INDEX == 15) { c = Color.Lime; }
            else if (INDEX == 16) { c = Color.Magenta; }
            else if (INDEX == 17) { c = Color.MintCream; }
            else if (INDEX == 18) { c = Color.Navy; }
            else if (INDEX == 19) { c = Color.Orange; }
            else if (INDEX == 20) { c = Color.Orchid; }
            else if (INDEX == 21) { c = Color.Pink; }
            else if (INDEX == 22) { c = Color.Indigo; }
            else if (INDEX == 23) { c = Color.Salmon; }
            else if (INDEX == 24) { c = Color.Silver; }
            else if (INDEX == 25) { c = Color.Violet; }
            else if (INDEX == 26) { c = Color.Yellow; }
            else if (INDEX == 27) { c = Color.Tomato; }
            else if (INDEX == 28) { c = Color.Tan; }
            else if (INDEX == 29) { c = Color.SeaGreen; }
            else if (INDEX == 30) { c = Color.RosyBrown; }
            else if (INDEX == 31) { c = Color.Plum; }
            else
            {
                c = Color.Maroon;
            }
            return c;
        }


    }

}
