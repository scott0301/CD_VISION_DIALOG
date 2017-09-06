using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Windows.Forms;

using System.IO;

namespace UC_LogView
{
    public partial class UC_LOG_VIEWER: UserControl
    {
        public StringBuilder m_msg = new StringBuilder();
        
        public string PATH_BASE_FOLDER = string.Empty;
        public string PATH_BASE_LOG_FILE = string.Empty;
        public string PATH_LIFE_CICLE_FILE = string.Empty;

        static AutoResetEvent AutoResetCom = new AutoResetEvent(false);

        CThreadPoolData poolManager = new CThreadPoolData("master","null", 0);

        List<CheckBox> m_listCheckBox = new List<CheckBox>();

        public int m_workIndexer { get; set; } // job counter 

        private System.Windows.Forms.Timer tLifeCyle = new System.Windows.Forms.Timer();


        public UC_LOG_VIEWER()
        {
            InitializeComponent();

            m_listCheckBox.Add(CHK_00_ALL);
            m_listCheckBox.Add(CHK_01_PWR);
            m_listCheckBox.Add(CHK_02_RECP);
            m_listCheckBox.Add(CHK_03_PARAM);
            m_listCheckBox.Add(CHK_04_IMAGE);
            m_listCheckBox.Add(CHK_05_MEAS);
            m_listCheckBox.Add(CHK_06_COMM);
        }

        public delegate void deleRichAppendText(String myString);
        public deleRichAppendText AppendRichText;
 
        private void UC_LOG_VIEWER_Load(object sender, EventArgs e)
        {
            PATH_BASE_LOG_FILE = CSupport.GetBaseLogFileName();

            ThreadPool.RegisterWaitForSingleObject(AutoResetCom, new WaitOrTimerCallback(Thread_Terminator), poolManager, -1, true);
            m_workIndexer = 0;

           

            AppendRichText = new deleRichAppendText(AppendText);

            this.tLifeCyle.Interval = 1000;
            this.tLifeCyle.Tick += new System.EventHandler(this.tLifeCyle_Tick);
            tLifeCyle.Enabled = true;
        }

        private void tLifeCyle_Tick(object sender, EventArgs e)
        {
            try
            {
                PATH_BASE_LOG_FILE = CSupport.GetBaseLogFileName().Replace(".LOG","_LIFE.LOG");
                string PathLogFile = CSupport.GetFullLogFileName(PATH_BASE_FOLDER, PATH_BASE_LOG_FILE);

                CThreadPoolData thrFather = new CThreadPoolData(PathLogFile, CSupport.TIME_GetTimeCode_MD_HMS_MS() + " : I'am Alive.\r\n", m_workIndexer++);
                ThreadPool.QueueUserWorkItem(new WaitCallback(Thread_Worker), thrFather);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //*****************************************************************************************

        static void Thread_Worker(Object param)
        {
            CThreadPoolData args = (CThreadPoolData)param;

            if (args.GetActivation() == true)
            {
                try
                {
                    if (File.Exists(args.basePath) == false)
                    {
                        StreamWriter sw = File.CreateText(args.basePath);
                        {
                            string strTimeCode = CSupport.TIME_GetTimeCode_MD_HMS_MS();
                            string Tag = "[FILE CREATION]";
                            string msg = "Newly File has generated.";
                            sw.WriteLine(strTimeCode + " : " + Tag + " " + msg + System.Environment.NewLine);
                        }
                        sw.Close();
                    }
                    else
                    {
                        //StreamWriter sw = File.AppendText(args.basePath);
                        using( StreamWriter sw = new StreamWriter( args.basePath, true))
                        {
                            sw.WriteLine(args.msg);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(args.msg);
                }
            }

            if (args.GetActivation() == false )
            {
                AutoResetCom.Set();
            }
        }

        static void Thread_Terminator(Object param, bool timedOut)
        {
            CThreadPoolData args = (CThreadPoolData)param;

            if (args.GetActivation() == true)
            {
                Console.WriteLine("Finished " + args.nIndex.ToString("N0"));
            }
            else if (args.GetActivation() == false)
            {
                //MessageBox.Show("Jobs are Canceled.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //*****************************************************************************************
        // COMMON FUNCTIONS
        //*****************************************************************************************

        public void SetBasePath(string strBasePath){PATH_BASE_FOLDER = strBasePath;}
        public void SetSize(int x, int y)
        {
            this.Size = new Size(x, y);

            int nWidth = this.Size.Width;
            int nHeight = this.Size.Height;

            RICH_MESSAGE.Location = new Point(5, 5);
            RICH_MESSAGE.Size = new Size(this.Size.Width-10, this.Size.Height - 60);

            // Set Position for the clear button
            BTN_CLEAR.Location = new Point(BTN_CLEAR.Location.X, nHeight - 50);

            // set position for the checkboxes [ header ]
            m_listCheckBox[0].Location = new Point(m_listCheckBox[0].Location.X, nHeight - 50);
            m_listCheckBox[1].Location = new Point(m_listCheckBox[1].Location.X, nHeight - 50);

            // set position for the checkboxes [ elements ]
            for (int nChk = 2; nChk < m_listCheckBox.Count; nChk++)
            {
                m_listCheckBox[nChk].Location = new Point(m_listCheckBox[nChk].Location.X, nHeight - 30);
            }
        }

        public void SetClear() { m_msg.Clear(); RICH_MESSAGE.Clear(); }
        public void SetBackColor(Color c) { this.BackColor = c; }

      
       
        //*****************************************************************************************

        public void WRITE_LOG(string msg, string Tag)
        {
            string strTimeCode = CSupport.TIME_GetTimeCode_MD_HMS_MS();
            string temp = strTimeCode + " : " + Tag + " " + msg + System.Environment.NewLine;
            string HeaderCut = temp;

            m_msg.Append(temp);

            this.UIThread(delegate
            {
                if (CHK_00_ALL.Checked == true)
                {
                    HeaderCut = _RemoveHeaders(temp);
                    RICH_MESSAGE.AppendText(HeaderCut);
                    RICH_MESSAGE.ScrollToCaret();
                }
                else if (CHK_00_ALL.Checked == false)
                {
                    System.Threading.Thread thr = new Thread(delegate() {UpdatePrintContents(m_msg, RICH_MESSAGE, m_listCheckBox);});
                    thr.IsBackground = true;
                    thr.Start();
                }
            });

            if (poolManager.GetActivation() == false)
            {
                poolManager.SetActivation(true);
            }

            PATH_BASE_LOG_FILE = CSupport.GetBaseLogFileName();
            string PathLogFile = CSupport.GetFullLogFileName(PATH_BASE_FOLDER, PATH_BASE_LOG_FILE);

            CThreadPoolData thrFather = new CThreadPoolData(PathLogFile, temp, m_workIndexer++);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Thread_Worker), thrFather);
        }

        private string _RemoveHeaders(string strTemp)
        {
            string strFiltered = strTemp;
            strFiltered = strFiltered.Replace(DEF_OPERATION.OPER_01_POWER, "");
            strFiltered = strFiltered.Replace(DEF_OPERATION.OPER_02_RECP, "");
            strFiltered = strFiltered.Replace(DEF_OPERATION.OPER_03_PARAM, "");
            strFiltered = strFiltered.Replace(DEF_OPERATION.OPER_04_IMAGE, "");
            strFiltered = strFiltered.Replace(DEF_OPERATION.OPER_05_MEAS, "");
            strFiltered = strFiltered.Replace(DEF_OPERATION.OPER_06_COMM, "");

            return strFiltered;
        }
        public void UpdatePrintContents(StringBuilder sb, RichTextBox rich, List<CheckBox> list)
        {
            var parse = sb.ToString().Split('\n');

            StringBuilder temp = new StringBuilder();

            if (list[0].Checked == true)
            {
                foreach (string s in parse)
                {
                    string strTemp = _RemoveHeaders(s);
                    temp.Append(strTemp);
                }

                // for thread-safe : invoke delegate
                this.Invoke(AppendRichText, temp.ToString());
            }
            else
            {
                foreach (string s in parse)
                {
                    string strTemp  = _RemoveHeaders(s);

                    /***/if (list[1].Checked/***/ && s.Contains(DEF_OPERATION.OPER_01_POWER))/**/ { temp.Append(strTemp); }
                    else if (list[2].Checked/***/ && s.Contains(DEF_OPERATION.OPER_02_RECP))/***/ { temp.Append(strTemp); }
                    else if (list[3].Checked/***/ && s.Contains(DEF_OPERATION.OPER_03_PARAM))/**/ { temp.Append(strTemp); }
                    else if (list[4].Checked/***/ && s.Contains(DEF_OPERATION.OPER_04_IMAGE))/**/ { temp.Append(strTemp); }
                    else if (list[5].Checked/***/ && s.Contains(DEF_OPERATION.OPER_05_MEAS))/***/ { temp.Append(strTemp); }
                    else if (list[6].Checked/***/ && s.Contains(DEF_OPERATION.OPER_06_COMM))/***/ { temp.Append(strTemp); }
                }

                // for thread-safe : invoke delegate
                this.Invoke(AppendRichText, temp.ToString());
            }
        }
        private void AppendText( string text)
        {
            RICH_MESSAGE.Clear();
            RICH_MESSAGE.AppendText(text.ToString());
            RICH_MESSAGE.ScrollToCaret();
        }

        //*****************************************************************************************
        // Command Message
        //*****************************************************************************************

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            SetClear();
        }

        private void CHK_ALL_CheckedChanged(object sender, EventArgs e)
        {
            bool bCurrent = CHK_00_ALL.Checked;

            CHK_06_COMM.Checked = CHK_04_IMAGE.Checked = CHK_05_MEAS.Checked = CHK_03_PARAM.Checked = CHK_01_PWR.Checked = CHK_02_RECP.Checked = !bCurrent;

            System.Threading.Thread thr = new Thread(delegate() { UpdatePrintContents(m_msg, RICH_MESSAGE, m_listCheckBox);  });
            thr.IsBackground = true;
            thr.Start();
            

        }

        private void CHK_LOG_Contents_Changed(object sender, EventArgs e)
        {
            System.Threading.Thread thr = new Thread(delegate() { UpdatePrintContents(m_msg, RICH_MESSAGE, m_listCheckBox);  });
            thr.IsBackground = true;
            thr.Start();

        }

        private void UC_LOG_VIEWER_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

        }

        private void UC_LOG_VIEWER_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files[0].Contains(".LOG") == true)
            {
                string[] arrLines = File.ReadAllLines(files[0]);

                m_msg.Clear();

                foreach (string s in arrLines)
                {
                    m_msg.Append(s+System.Environment.NewLine);
                }
            }

            System.Threading.Thread thr = new Thread(delegate() { UpdatePrintContents(m_msg, RICH_MESSAGE, m_listCheckBox); });
            thr.IsBackground = true;
            thr.Start();

        }

      
    }

   
   

 
}
