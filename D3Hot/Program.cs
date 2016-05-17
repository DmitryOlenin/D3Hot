using System;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using D3Hot.Properties;

namespace D3Hot
{
    static class Program
    {

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Ликвидировать объекты перед потерей области"), STAThread]
        static void Main()
        {
            bool onlyInstance;
            Mutex mtx = new Mutex(true, "D3Hot", out onlyInstance); // используйте имя вашего приложения

            if (Settings.Default.chb_mult == 1)
            {
                LoadProg();
            }
            else 
                if (onlyInstance)
                {
                    LoadProg();
                    mtx.ReleaseMutex();
                }
                else
                {
                    // send our Win32 message to make the currently running instance
                    // jump on top of all the other windows
                    d3hot.NativeMethods.PostMessage(
                        (IntPtr)d3hot.NativeMethods.HWND_BROADCAST,
                        d3hot.NativeMethods.WM_SHOWME,
                        IntPtr.Zero,
                        IntPtr.Zero);

                    //Process[] procs = Process.GetProcessesByName("d3h");
                    //foreach (Process proc in procs)
                    //    if (proc.Id != Process.GetCurrentProcess().Id)
                    //    {
                    //        //ShowWindow((int)proc.MainWindowHandle, 9);//нормально развернутое
                    //        //EnableWindow(proc.MainWindowHandle, true);
                    //        SetForegroundWindow(proc.MainWindowHandle);                        
                        
                    //        if (proc.MainWindowHandle == IntPtr.Zero)
                    //        {
                    //            proc.Kill();
                    //            Application.EnableVisualStyles();
                    //            Application.SetCompatibleTextRenderingDefault(false);
                    //            Application.Run(new d3hot());
                    //        }
                    //        break;
                    //    }
                }

        }

        static void LoadProg()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new d3hot());
        }

    }
}
