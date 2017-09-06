using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace NS_UC_THUMB_NAIL
{
    public partial class UC_THUMB_NAIL : ListView
    {
        private BackgroundWorker myWorker = new BackgroundWorker();

        public event EventHandler OnLoadComplete;
 
        //public delegate void MyEventHandler(object sender, MyEventArgs e);

        private Bitmap[] m_listImages = null;

        public Bitmap GetImageOriginal(int nIndex)
        {
            Bitmap bmp = null;

            if (m_listImages.Length-1 >= nIndex)
            {
                bmp = m_listImages[nIndex].Clone() as Bitmap;
            }

            return bmp;
        }
        private int thumbNailSize = 64;
        public int ThumbNailSize
        {
            get { return thumbNailSize; }
            set { thumbNailSize = value; }
        }

        private Color thumbBorderColor = Color.Wheat;

        public Color ThumbBorderColor
        {
            get { return thumbBorderColor; }
            set { thumbBorderColor = value; }
        }

        public bool IsLoading
        {
            get { return myWorker.IsBusy; }
        }

        public int GetItemCount()
        {
            return Items.Count;
        }

        private delegate void SetThumbnailDelegate(Image image);

        private void SetThumbnail(Image image)
        {
            if (Disposing) return;

            if (this.InvokeRequired)
            {
                SetThumbnailDelegate d = new SetThumbnailDelegate(SetThumbnail);
                this.Invoke(d, new object[] { image });
            }
            else
            {
                LargeImageList.Images.Add(image); //Images[i].repl  
                int index = LargeImageList.Images.Count - 1;
                Items[index - 1].ImageIndex = index;
            }
        }

        private bool bLoadAvailable = false;
        public bool m_bLoadAvailable
        {
            get { return bLoadAvailable; }
            set { bLoadAvailable = value; }
        }


        public string PATH_FOLDER {get;set;}

        public void SetInit()
        {
            bLoadAvailable = true;
            Items.Clear();
            LargeImageList = InitImageList();
        }
        public ImageList InitImageList()
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(thumbNailSize, thumbNailSize);
            imageList.ColorDepth = ColorDepth.Depth24Bit;
            imageList.TransparentColor = Color.White;

            return imageList;
        }
        public UC_THUMB_NAIL()
        {
            InitializeComponent();

            LargeImageList = InitImageList();

            components.Add(myWorker);

            myWorker.WorkerSupportsCancellation = true;
            //myWorker.DoWork += new DoWorkEventHandler(Worker_LoadImage_FileList);
            myWorker.DoWork += new DoWorkEventHandler(Worker_LoadImage_ImageList);
         
            myWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myWorker_RunWorkerCompleted);
        }

        void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnLoadComplete != null)
            {
                OnLoadComplete(this, new EventArgs());
            }
        }

        public void ReLoadItems()
        {
            string strFilter = "*.jpg;*.png;*.gif;*.bmp";

            List<string> fileList = new List<string>();
            string[] arExtensions = strFilter.Split(';');

            if (Directory.Exists(PATH_FOLDER) == false)
            {
                MessageBox.Show("Path : " + PATH_FOLDER + " Not Found", "Folder Existance Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (string filter in arExtensions)
            {

                string[] strFiles = Directory.GetFiles(PATH_FOLDER, filter);
                fileList.AddRange(strFiles);
            }

            fileList.Sort();
            LoadImages(fileList.ToArray());

        }
        public Image GetThumbNail(string fileName)
        {
            return GetThumbNail(fileName, thumbNailSize, thumbNailSize, thumbBorderColor);
        }

        public static Image GetThumbNail(string fileName, int imgWidth, int imgHeight, Color penColor)
        {
            Bitmap bmp;

            try
            {
                bmp = new Bitmap(fileName);
            }
            catch
            {
                bmp = new Bitmap(imgWidth, imgHeight); //If we cant load the image, create a blank one with ThumbSize
            }


            imgWidth = bmp.Width > imgWidth ? imgWidth : bmp.Width;
            imgHeight = bmp.Height > imgHeight ? imgHeight : bmp.Height;

            Bitmap retBmp = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Graphics grp = Graphics.FromImage(retBmp);


            int tnWidth = imgWidth, tnHeight = imgHeight;

            if (bmp.Width > bmp.Height)
            {
                tnHeight = (int)(((float)bmp.Height / (float)bmp.Width) * tnWidth);
            }
            else if (bmp.Width < bmp.Height)
            {
                tnWidth = (int)(((float)bmp.Width / (float)bmp.Height) * tnHeight);
            }

            int iLeft = (imgWidth / 2) - (tnWidth / 2);
            int iTop = (imgHeight / 2) - (tnHeight / 2);

            grp.PixelOffsetMode = PixelOffsetMode.None;
            grp.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grp.DrawImage(bmp, iLeft, iTop, tnWidth, tnHeight);

            Pen pn = new Pen(penColor, 1); //Color.Wheat
            grp.DrawRectangle(pn, 0, 0, retBmp.Width - 1, retBmp.Height - 1);

            return retBmp;
        }

        private void AddDefaultThumb()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(LargeImageList.ImageSize.Width, LargeImageList.ImageSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            Graphics grp = Graphics.FromImage(bmp);
            Brush brs = new SolidBrush(Color.White);
            Rectangle rect = new Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1);
            grp.FillRectangle(brs, rect);

            Pen pn = new Pen(Color.Wheat, 1);

            grp.DrawRectangle(pn, 0, 0, bmp.Width - 1, bmp.Height - 1);

            LargeImageList.Images.Add(bmp);
        }

        private void Worker_LoadImage_FileList(object sender, DoWorkEventArgs e)
        {
            if (myWorker.CancellationPending) return;

            string[] fileList = (string[])e.Argument;

            foreach (string fileName in fileList)
            {
                SetThumbnail(GetThumbNail(fileName));
            }
        }
        private void Worker_LoadImage_ImageList(object sender, DoWorkEventArgs e)
        {
            if (myWorker.CancellationPending) return;

            Bitmap[] imageList = (Bitmap[])e.Argument;

            foreach (Bitmap bmp in imageList)
            {
                SetThumbnail(bmp);
            }
        }

        public void AddImage(string strPath)
        {
            if ((myWorker != null) && (myWorker.IsBusy))
            {
                myWorker.CancelAsync();
            }

            List<string> arrList = new List<string>();
            arrList.Add(strPath);

            if (Items.Count == 0)
            {
                AddDefaultThumb();
            }

            BeginUpdate();

            string strFileName = System.IO.Path.GetFileName(strPath);
            ListViewItem item = Items.Add(strFileName);
            item.ImageIndex = 0;
            item.Tag = strFileName;

            EndUpdate();

            if (myWorker != null)
            {
                if (!myWorker.CancellationPending)
                {
                    myWorker.RunWorkerAsync(arrList.ToArray());
                }
            }
        }

        public void LoadImages(string[] arrFileList)
        {
            if ((myWorker != null) && (myWorker.IsBusy))
            {
                myWorker.CancelAsync();
            }

            BeginUpdate();
            Items.Clear();

            LargeImageList.Images.Clear();

            AddDefaultThumb();

            foreach (string fileName in arrFileList)
            {
                ListViewItem item = Items.Add(System.IO.Path.GetFileName(fileName));
                item.ImageIndex = 0;
                item.Tag = fileName;
            }

            EndUpdate();

            if (myWorker != null)
            {
                if (!myWorker.CancellationPending)
                {
                    myWorker.RunWorkerAsync(arrFileList);
                }
            }
        }
        public void LoadImages(Bitmap [] arrBitmap, List<string> listNames )
        {
            if ((myWorker != null) && (myWorker.IsBusy))
            {
                myWorker.CancelAsync();
            }

            BeginUpdate();
            Items.Clear();

            LargeImageList.Images.Clear();

            AddDefaultThumb();

            foreach (string fileName in listNames)
            {
                ListViewItem item = Items.Add( fileName);
                item.ImageIndex = 0;
                item.Tag = fileName;
            }

            EndUpdate();

            if (myWorker != null)
            {
                if (!myWorker.CancellationPending)
                {
                    if (m_listImages != null)
                    {
                        Array.Clear(m_listImages, 0, m_listImages.Length);
                        GC.Collect();
                    }

                    m_listImages = arrBitmap;
                    myWorker.RunWorkerAsync(arrBitmap);
                }
            }
        }


      
       
    }
}
