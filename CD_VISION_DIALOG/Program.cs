using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

 namespace CD_VISION_DIALOG
{
    static class Program
    {
 
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary> 
        [STAThread]
        static void Main()
        {
            bool bExecution;
            Mutex mutex = new Mutex(true, "scotty", out bExecution);

            if (bExecution == false)
            {
                MessageBox.Show("Vision Simulator is Running.", "Execution Warnning.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Process[] processes = Process.GetProcessesByName("CD_VISION_DIALOG");
                Process currentProcess = Process.GetCurrentProcess();
                foreach (Process proc in processes)
                {
                    if (proc.Id != currentProcess.Id)
                    {
                        proc.Kill();
                    }
                }
             }

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CDMainForm());
            mutex.ReleaseMutex();
        }

        // in form's constructor

 
        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            MessageBox.Show(e.ToString());
        }
    }
}
