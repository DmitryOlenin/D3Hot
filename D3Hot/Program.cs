using System;
using System.Windows.Forms;
using System.Threading;
using D3Hot.Properties;
using System.Diagnostics;

namespace D3Hot
{

    public class NumericTimer : System.Timers.Timer
    {
        public readonly int Number;

        public NumericTimer(int number)
        {
            Number = number;
        }
    }

    internal static class Program
    {

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool onlyInstance;
            using (var mtx = new Mutex(true, "D3Hot", out onlyInstance)) // используйте имя вашего приложения
            //11.01.2017 Анализ PVS Studio - Idisposable объект не был закрыт
            {
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

                        //var current = Process.GetCurrentProcess();
                        //foreach (var process in Process.GetProcessesByName(current.ProcessName))
                        //{
                        //    if (process.Id == current.Id) continue;
                        //    D3Hotkeys.NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                        //    break;
                        //}

                        // send our Win32 message to make the currently running instance
                        // jump on top of all the other windows
                        D3Hotkeys.NativeMethods.PostMessage(
                            (IntPtr)D3Hotkeys.NativeMethods.HwndBroadcast, //(IntPtr)
                            D3Hotkeys.NativeMethods.WmShowme,
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
        }

        private static void LoadProg()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new D3Hotkeys());
            }
            catch (Exception ex) //08.06.2016
            {
                try
                {
                    const string sourceName = "D3Hot Starting Error";
                    if (!EventLog.SourceExists(sourceName))
                    {
                        EventLog.CreateEventSource(sourceName, "Application");
                    }

                    using (var eventLog = new EventLog())
                    {
                        eventLog.Source = sourceName;
                        var message = string.Format("Exception: {0} \n\nStack: {1}", ex.Message, ex.StackTrace);
                        eventLog.WriteEntry(message, EventLogEntryType.Error);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

    }
}
