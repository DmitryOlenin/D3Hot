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
        [STAThread]
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
                    NativeMethods.PostMessage(
                        (IntPtr)NativeMethods.HWND_BROADCAST,
                        NativeMethods.WM_SHOWME,
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

        internal class NativeMethods
        {
            public const int HWND_BROADCAST = 0xffff;
            public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
            [DllImport("user32")]
            public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
            [DllImport("user32")]
            public static extern int RegisterWindowMessage(string message);
        }

    }
}
