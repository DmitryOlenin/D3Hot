using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;

namespace D3Hot
{
    public partial class d3hot : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus();

        [DllImport("user32")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);


        //public int counter = 0, maxRepeatedCharacters = 30; // repeat char 30 times
        public IntPtr handle = IntPtr.Zero;// = GetForegroundWindow();
        Dictionary<string, IntPtr> proc_handle;
        const uint WM_KEYDOWN = 0x100;
        const uint WM_KEYUP = 0x101;
        const uint WM_CHAR = 0x102;
        const uint WM_LBUTTONDOWN = 0x201;
        const uint WM_LBUTTONUP = 0x202;
        const uint WM_RBUTTONDOWN = 0x204;
        const uint WM_RBUTTONUP = 0x205;

        //public System.Windows.Forms.Timer startTimer;
        //public System.Windows.Forms.Timer repeatTimer;
        public System.Timers.Timer startTimer, repeatTimer;
        public int key_for_hold = 0, key_for_keyup = 0;
        public uint key_down = 0, key_up=0;

        
        public void key_hold(int i)
        {
            //counter = 0;
            int keyboardDelay, keyboardSpeed;
            using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Keyboard"))
            {
                Debug.Assert(key != null);
                keyboardDelay = 1;
                int.TryParse((String)key.GetValue("KeyboardDelay", "1"), out keyboardDelay);
                keyboardSpeed = 31;
                int.TryParse((String)key.GetValue("KeyboardSpeed", "31"), out keyboardSpeed);
            }

            switch (i)
            {
                case 1: key_for_hold = key_hold_code(key1); break;
                case 2: key_for_hold = key_hold_code(key2); break;
                case 3: key_for_hold = key_hold_code(key3); break;
                case 4: key_for_hold = key_hold_code(key4); break;
                case 5: key_for_hold = key_hold_code(key5); break;
                case 6: key_for_hold = key_hold_code(key6); break;
            }

            switch (key_for_hold)
            {
                case 1: 
                    key_down = WM_LBUTTONDOWN;
                    key_up = WM_LBUTTONUP;
                    break;
                case 2:
                    key_down = WM_RBUTTONDOWN;
                    key_up = WM_RBUTTONUP;
                    break;
                default:
                    key_down = WM_KEYDOWN;
                    key_up = WM_KEYUP;
                    break;                        
            }
            
            //startTimer = new System.Timers.Timer { Interval = keyboardDelay };
            //startTimer.Elapsed += startTimer_Tick; //Tick
            //startTimer.Start();
            //repeatTimer = new System.Timers.Timer() { Interval = keyboardSpeed };
            //repeatTimer.Elapsed += repeatTimer_Tick;
            //repeatTimer.Start();

            hold_load(i);

            //repeatTimer.Interval += keyboardSpeed;
            //...
        }

        public void keyup(int i)
        {
            switch (i)
            {
                case 1: key_for_keyup = key_hold_code(key1); break;
                case 2: key_for_keyup = key_hold_code(key2); break;
                case 3: key_for_keyup = key_hold_code(key3); break;
                case 4: key_for_keyup = key_hold_code(key4); break;
                case 5: key_for_keyup = key_hold_code(key5); break;
                case 6: key_for_keyup = key_hold_code(key6); break;
            }

            IntPtr hWindow = IntPtr.Zero;
            //hWindow = FindWindow(null, "akelpad");
            //hWindow = FindWindowEx(hWindow, IntPtr.Zero, "AkelEditW", null);

            if ((!d3proc && !d3prog) || (d3prog && usage_area())) hWindow = GetFocusedControl();
            else if (d3proc) hWindow = handle;

            //if (!d3proc) hWindow = GetFocusedControl();//GetForegroundWindow(); //19.03.2015
            //else hWindow = handle;

            if (hWindow != IntPtr.Zero)
            PostMessage(hWindow,
                       key_up,//(int)WM_KEYUP,
                       key_for_keyup, 0);
        }

        public void repeatTimer_Tick(object sender, EventArgs e)
        {
            IntPtr hWindow = IntPtr.Zero;
            //if (usage_area())
            //{
                //hWindow = FindWindow(null, "akelpad");
                //hWindow = FindWindowEx(hWindow, IntPtr.Zero, "AkelEditW", null);

                if ((!d3proc && !d3prog) || (d3prog && usage_area())) hWindow = GetFocusedControl();
                else if (d3proc) hWindow = handle;

                //if (!d3proc) hWindow = GetFocusedControl();//GetForegroundWindow(); //19.03.2015
                //else hWindow = handle;

                if (hWindow != IntPtr.Zero)
                    PostMessage(hWindow,
                           key_down,//(int)WM_KEYDOWN,
                           key_for_hold, 0);
            
                //System.Timers.Timer timer = sender as System.Timers.Timer;

            //}
        }

        public void startTimer_Tick(object sender, EventArgs eventArgs)
        {
            IntPtr hWindow = IntPtr.Zero;

            //if (usage_area())
            //{
                //hWindow = FindWindow(null, "akelpad");
                //hWindow = FindWindowEx(hWindow, IntPtr.Zero, "AkelEditW", null);
                
                if ((!d3proc && !d3prog) || (d3prog && usage_area())) hWindow = GetFocusedControl();
                else if (d3proc) hWindow = handle;

                //if (!d3proc) hWindow = GetFocusedControl();//GetForegroundWindow(); //19.03.2015
                //else hWindow = handle;

                //int t1 = (int)Keys.D3;

                System.Timers.Timer timer = sender as System.Timers.Timer;
                if (timer != null) timer.Stop();

                if (hWindow != IntPtr.Zero)
                    PostMessage(hWindow,
                               key_down,//(int)WM_KEYDOWN,
                               key_for_hold, 0);
            //}
        }



        public void hold_load(int i)
        {

            int keyboardDelay, keyboardSpeed;
            using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Keyboard"))
            {
                Debug.Assert(key != null);
                keyboardDelay = 1;
                int.TryParse((String)key.GetValue("KeyboardDelay", "1"), out keyboardDelay);
                keyboardSpeed = 31;
                int.TryParse((String)key.GetValue("KeyboardSpeed", "31"), out keyboardSpeed);
            }

            switch (i)
            {
                case 1:
                    StartTimer1 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer1.Elapsed += startTimer_Tick;
                    RepeatTimer1 = new System.Timers.Timer { Interval = keyboardSpeed };
                    RepeatTimer1.Elapsed += repeatTimer_Tick;
                    StartTimer1.Start();
                    RepeatTimer1.Start();
                    break;
                case 2:
                    StartTimer2 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer2.Elapsed += startTimer_Tick;
                    RepeatTimer2 = new System.Timers.Timer { Interval = keyboardSpeed };
                    RepeatTimer2.Elapsed += repeatTimer_Tick;
                    StartTimer2.Start();
                    RepeatTimer2.Start();
                    break;
                case 3:
                    StartTimer3 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer3.Elapsed += startTimer_Tick;
                    RepeatTimer3 = new System.Timers.Timer { Interval = keyboardSpeed };
                    RepeatTimer3.Elapsed += repeatTimer_Tick;
                    StartTimer3.Start();
                    RepeatTimer3.Start();
                    break;
                case 4:
                    StartTimer4 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer4.Elapsed += startTimer_Tick;
                    RepeatTimer4 = new System.Timers.Timer { Interval = keyboardSpeed };
                    RepeatTimer4.Elapsed += repeatTimer_Tick;
                    StartTimer4.Start();
                    RepeatTimer4.Start();
                    break;
                case 5:
                    StartTimer5 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer5.Elapsed += startTimer_Tick;
                    RepeatTimer5 = new System.Timers.Timer { Interval = keyboardSpeed };
                    RepeatTimer5.Elapsed += repeatTimer_Tick;
                    StartTimer5.Start();
                    RepeatTimer5.Start();
                    break;
                case 6:
                    StartTimer6 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer6.Elapsed += startTimer_Tick;
                    RepeatTimer6 = new System.Timers.Timer { Interval = keyboardSpeed };
                    RepeatTimer6.Elapsed += repeatTimer_Tick;
                    StartTimer6.Start();
                    RepeatTimer6.Start();
                    break;
            }
        }


        public void hold_unload(int i)
        {
            switch (i)
            {
                case 1:
                    if (StartTimer1.Enabled) StartTimer1.Stop();//.Dispose()
                    if (RepeatTimer1.Enabled) RepeatTimer1.Stop();//.Dispose()
                    break;
                case 2:
                    if (StartTimer2.Enabled) StartTimer2.Stop();
                    if (RepeatTimer2.Enabled) RepeatTimer2.Stop();
                    break;
                case 3:
                    if (StartTimer3.Enabled) StartTimer3.Stop();//.Dispose()
                    if (RepeatTimer3.Enabled) RepeatTimer3.Stop();//.Dispose()
                    keyup(i);
                    break;
                case 4:
                    if (StartTimer4.Enabled) StartTimer4.Stop();//.Dispose()
                    if (RepeatTimer4.Enabled) RepeatTimer4.Stop();//.Dispose()
                    break;
                case 5:
                    if (StartTimer5.Enabled) StartTimer5.Stop();//.Dispose()
                    if (RepeatTimer5.Enabled) RepeatTimer5.Stop();//.Dispose()
                    break;
                case 6:
                    if (StartTimer6.Enabled) StartTimer6.Stop();//.Dispose()
                    if (RepeatTimer6.Enabled) RepeatTimer6.Stop();//.Dispose()
                    break;
            }
            keyup(i);
        }

        /// <summary>
        /// Метод для нахождения окна с фокусом
        /// </summary>
        /// <returns></returns>
        public IntPtr GetFocusedControl()
        {
            IntPtr hFocus = IntPtr.Zero;
            IntPtr hFore;
            int id = 0;
            //узнаем в каком окне находится фокус ввода
            hFore = GetForegroundWindow();
            //подключаемся к процессу
            AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), true);
            //получаем хэндл фокуса
            hFocus = GetFocus();
            //отключаемся от процесса
            AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), false);
            return hFocus;
        }

        //public IntPtr GetProcessByName()
        //{
        //    IntPtr hWnd = IntPtr.Zero;
        //    if (d3proc && proc_curr !="")
        //    {
        //        string wName = proc_curr.Substring(0, proc_curr.IndexOf(" "));
        //        foreach (Process pList in Process.GetProcesses())
        //        {
        //            if (pList.MainWindowTitle.ToLower().Contains(wName.ToLower()))
        //            {
        //                hWnd = pList.MainWindowHandle;
        //            }
        //        }
        //    }
        //    return hWnd; //Should contain the handle but may be zero if the title doesn't match
        //}


    }
}
