using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using WindowsInput.Native;
//using InputManager; //26.03.2015

namespace D3Hot
{
    public partial class d3hot : Form
    {

        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern IntPtr GetFocus();

        //[DllImport("user32")]
        //public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern uint GetCurrentThreadId();

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);


        //public int counter = 0, maxRepeatedCharacters = 30; // repeat char 30 times
        public IntPtr handle = IntPtr.Zero;// = GetForegroundWindow();
        Dictionary<string, IntPtr> proc_handle;
        const uint WM_KEYDOWN = 0x100;
        const uint WM_KEYUP = 0x101;
        const uint WM_CHAR = 0x102;
        const uint WM_SYSKEYDOWN = 0x104;
        const uint WM_LBUTTONDOWN = 0x201;
        const uint WM_LBUTTONUP = 0x202;
        const uint WM_RBUTTONDOWN = 0x204;
        const uint WM_RBUTTONUP = 0x205;

        //public System.Windows.Forms.Timer startTimer;
        //public System.Windows.Forms.Timer repeatTimer;
        public System.Timers.Timer startTimer, repeatTimer;
        //public int key_for_hold = 0, key_for_keyup = 0;
        public uint key_down = 0, key_up=0;
        public int st_timer1_r = 0, st_timer2_r = 0, st_timer3_r = 0, st_timer4_r = 0, st_timer5_r = 0, st_timer6_r = 0,
            r_timer1_r = 0, r_timer2_r = 0, r_timer3_r = 0, r_timer4_r = 0, r_timer5_r = 0, r_timer6_r = 0;
        public int keyboardDelay, keyboardSpeed;


        public void keyup(int i)
        {
            int key_for_keyup = 0;
            int ret = 0;

            switch (i)
            {
                case 1: key_for_keyup = key_hold_code(key1); break;
                case 2: key_for_keyup = key_hold_code(key2); break;
                case 3: key_for_keyup = key_hold_code(key3); break;
                case 4: key_for_keyup = key_hold_code(key4); break;
                case 5: key_for_keyup = key_hold_code(key5); break;
                case 6: key_for_keyup = key_hold_code(key6); break;
            }

            ret = _MapVirtualKey(key_for_keyup, 0);

            //if ((key_for_keyup == (int)Keys.LButton) || (key_for_keyup == (int)Keys.RButton))
            //{
                //Point defPnt = new Point();
                //GetCursorPos(ref defPnt);

                //PostMessage(handle,
                //           updown_keys(key_for_keyup)+1,
                //           0, (int)MakeLong(defPnt.X, defPnt.Y));

            //}
            if (key_for_keyup == (int)Keys.LButton)
            {
                inp.Mouse.LeftButtonUp(); //Mouse.ButtonUp(Mouse.MouseKeys.Left);
                System.Threading.Thread.Sleep(50);
                if (inp.InputDeviceState.IsKeyDown(VirtualKeyCode.LBUTTON))
                    inp.Mouse.LeftButtonUp();
                lmousehold = false;
            }
            else if (key_for_keyup == (int)Keys.RButton)
            {
                inp.Mouse.RightButtonUp(); //Mouse.ButtonUp(Mouse.MouseKeys.Right);
                System.Threading.Thread.Sleep(50);
                if (inp.InputDeviceState.IsKeyDown(VirtualKeyCode.RBUTTON))
                    inp.Mouse.RightButtonUp();
                rmousehold = false;
            }
            else
            {
                PostMessage(handle,//hWindow,
                       updown_keys(key_for_keyup) + 1,//(int)WM_KEYUP,
                       key_for_keyup, (int)(MakeLong(1, ret) + 0xC0000000)); //(int)
            }
        }

        public int timer_key(System.Timers.Timer timer)
        {
            int key_for_hold = 0;

                if (timer == RepeatTimer1 || timer == StartTimer1 || timer == tmr1) key_for_hold = key_hold_code(key1); else
                if (timer == RepeatTimer2 || timer == StartTimer2 || timer == tmr2) key_for_hold = key_hold_code(key2); else
                if (timer == RepeatTimer3 || timer == StartTimer3 || timer == tmr3) key_for_hold = key_hold_code(key3); else
                if (timer == RepeatTimer4 || timer == StartTimer4 || timer == tmr4) key_for_hold = key_hold_code(key4); else
                if (timer == RepeatTimer5 || timer == StartTimer5 || timer == tmr5) key_for_hold = key_hold_code(key5); else
                if (timer == RepeatTimer6 || timer == StartTimer6 || timer == tmr6) key_for_hold = key_hold_code(key6);
                
            return key_for_hold;
        }

        public uint updown_keys(int key_for_hold)
        {
            uint key_down = 0;
            switch (key_for_hold)
            {
                case 1:
                    key_down = WM_LBUTTONDOWN;
                    //key_up = WM_LBUTTONUP;
                    break;
                case 2:
                    key_down = WM_RBUTTONDOWN;
                    //key_up = WM_RBUTTONUP;
                    break;
                default:
                    key_down = WM_KEYDOWN;
                    //key_up = WM_KEYUP;
                    break;
            }
            return key_down;
        }

        public void repeatTimer_Tick(object sender, EventArgs e)
        {
            if (holded)
            {
                System.Timers.Timer timer = sender as System.Timers.Timer;
                int key_for_hold = timer_key(timer);

                int ret = 0;
                ret = _MapVirtualKey(key_for_hold, 0);

                if ((key_for_hold == (int)Keys.LButton) || (key_for_hold == (int)Keys.RButton))
                {
                    //IntPtr handle1;
                    //handle1 = FindWindow(null, "akelpad");
                    //handle1 = FindWindowEx(handle1, IntPtr.Zero, "AkelEditW", null); //For debugging
                    //if (usage_area() || handle == handle1)

                        if (usage_area())
                    {
                        if (key_for_hold == (int)Keys.LButton && !lmousehold) // && inp.InputDeviceState.IsKeyUp(VirtualKeyCode.LBUTTON)
                        {
                            //System.Threading.Thread.Sleep(100); 
                            inp.Mouse.LeftButtonDown();
                            lmousehold = true;
                        }
                        if (key_for_hold == (int)Keys.RButton && !rmousehold) // && inp.InputDeviceState.IsKeyUp(VirtualKeyCode.RBUTTON)
                        {
                            //System.Threading.Thread.Sleep(100);
                            inp.Mouse.RightButtonDown();
                            rmousehold = true;
                        }
                    }
                }
                else
                {

                    PostMessage(handle,//hWindow,
                                updown_keys(key_for_hold),
                                key_for_hold, (int)(MakeLong(1, ret)));
                }

            }
        }

        public void startTimer_Tick(object sender, EventArgs eventArgs)
        {
            System.Timers.Timer timer = sender as System.Timers.Timer;
            if (timer != null) timer.Stop();
            int key_for_hold = timer_key(timer);

            int i = 0;
            if (timer == StartTimer1 && st_timer1_r > 0)
            {
                i = 1;
                st_timer1_r = 0;
            } 
            else if (timer == StartTimer2 && st_timer2_r > 0)
            {
                i = 2;
                st_timer2_r = 0;
            } 
            else if (timer == StartTimer3 && st_timer3_r > 0)
            {
                i = 3;
                st_timer3_r = 0;
            }
            else if (timer == StartTimer4 && st_timer4_r > 0)
            {
                i = 4;
                st_timer4_r = 0;
            }
            else if (timer == StartTimer5 && st_timer5_r > 0)
            {
                i = 5;
                st_timer5_r = 0;
            }
            else if (timer == StartTimer6 && st_timer6_r > 0)
            {
                i = 6;
                st_timer6_r = 0;
            }

            int ret = 0;
            ret = _MapVirtualKey(key_for_hold, 0);

            if ((key_for_hold == (int)Keys.LButton) || (key_for_hold == (int)Keys.RButton))
            {
                if (i != 0)
                {
                    //IntPtr handle1;
                    //handle1 = FindWindow(null, "akelpad");
                    //handle1 = FindWindowEx(handle1, IntPtr.Zero, "AkelEditW", null);  //For debugging
                    //if (usage_area() || handle == handle1)

                    if (usage_area())
                    {
                        if (key_for_hold == (int)Keys.LButton)
                            inp.Mouse.LeftButtonDown();
                        if (key_for_hold == (int)Keys.RButton)
                            inp.Mouse.RightButtonDown();
                    }

                    //if (key_for_hold == (int)Keys.LButton) Mouse.ButtonDown(Mouse.MouseKeys.Left); //26.03.2015
                    //if (key_for_hold == (int)Keys.RButton) Mouse.ButtonDown(Mouse.MouseKeys.Right); //26.03.2015

                    //Point defPnt = new Point();
                    //GetCursorPos(ref defPnt);

                    //PostMessage(handle,
                    //           updown_keys(key_for_hold),
                    //           key_for_hold, (int)MakeLong(defPnt.X, defPnt.Y));
                }
            }
            else
            {
                PostMessage(handle,//hWindow,
                           updown_keys(key_for_hold),//(int)WM_KEYDOWN,
                           key_for_hold, (int)(MakeLong(1, ret)));
            }

                    //PostMessage(handle,//hWindow,
                    //           updown_keys(key_for_hold),//(int)WM_KEYDOWN,
                    //           key_for_hold, 0);

            if (i > 0)
                System.Threading.Thread.Sleep(50);
                switch (i)
                {
                    case (1):
                        RepeatTimer1 = new System.Timers.Timer { Interval = keyboardSpeed };
                        RepeatTimer1.Elapsed += repeatTimer_Tick;
                        r_timer1_r = 1;
                        RepeatTimer1.Start();
                        break;
                    case (2):
                        RepeatTimer2 = new System.Timers.Timer { Interval = keyboardSpeed };
                        RepeatTimer2.Elapsed += repeatTimer_Tick;
                        r_timer2_r = 1;
                        RepeatTimer2.Start();
                        break;
                    case (3):
                        RepeatTimer3 = new System.Timers.Timer { Interval = keyboardSpeed };
                        RepeatTimer3.Elapsed += repeatTimer_Tick;
                        r_timer3_r = 1;
                        RepeatTimer3.Start();
                        break;
                    case (4):
                        RepeatTimer4 = new System.Timers.Timer { Interval = keyboardSpeed };
                        RepeatTimer4.Elapsed += repeatTimer_Tick;
                        r_timer4_r = 1;
                        RepeatTimer4.Start();
                        break;
                    case (5):
                        RepeatTimer5 = new System.Timers.Timer { Interval = keyboardSpeed };
                        RepeatTimer5.Elapsed += repeatTimer_Tick;
                        r_timer5_r = 1;
                        RepeatTimer5.Start();
                        break;
                    case (6):
                        RepeatTimer6 = new System.Timers.Timer { Interval = keyboardSpeed };
                        RepeatTimer6.Elapsed += repeatTimer_Tick;
                        r_timer6_r = 1;
                        RepeatTimer6.Start();
                        break;
                }
        }



        public void hold_load(int i)
        {
            holded = true;
            //int keyboardDelay, keyboardSpeed;
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
                    st_timer1_r = 1; 
                    StartTimer1.Start();
                    //if (!mouse(1))
                    //{
                        //RepeatTimer1 = new System.Timers.Timer { Interval = keyboardSpeed };
                        //RepeatTimer1.Elapsed += repeatTimer_Tick;
                        //r_timer1_r = 1;
                        //RepeatTimer1.Start();
                    //}
                    break;
                case 2:
                    StartTimer2 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer2.Elapsed += startTimer_Tick;
                    st_timer2_r = 1; 
                    StartTimer2.Start();
                    //if (!mouse(2))
                    //{
                        //RepeatTimer2 = new System.Timers.Timer { Interval = keyboardSpeed };
                        //RepeatTimer2.Elapsed += repeatTimer_Tick;
                        //r_timer2_r = 1;
                        //RepeatTimer2.Start();
                    //}
                    break;
                case 3:
                    StartTimer3 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer3.Elapsed += startTimer_Tick;
                    st_timer3_r = 1; 
                    StartTimer3.Start();
                    //if (!mouse(3))
                    //{
                        //RepeatTimer3 = new System.Timers.Timer { Interval = keyboardSpeed };
                        //RepeatTimer3.Elapsed += repeatTimer_Tick;
                        //r_timer3_r = 1;
                        //RepeatTimer3.Start();
                    //}
                    break;
                case 4:
                    StartTimer4 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer4.Elapsed += startTimer_Tick;
                    StartTimer4.AutoReset = false;
                    st_timer4_r = 1; 
                    StartTimer4.Start();
                    //if (!mouse(4))
                    //{
                        //RepeatTimer4 = new System.Timers.Timer { Interval = keyboardSpeed };
                        //RepeatTimer4.Elapsed += repeatTimer_Tick;
                        //r_timer4_r = 1;
                        //RepeatTimer4.Start();
                    //}
                    break;
                case 5:
                    StartTimer5 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer5.Elapsed += startTimer_Tick;
                    st_timer5_r = 1; 
                    StartTimer5.Start();
                    //if (!mouse(5))
                    //{
                        //RepeatTimer5 = new System.Timers.Timer { Interval = keyboardSpeed };
                        //RepeatTimer5.Elapsed += repeatTimer_Tick;
                        //r_timer5_r = 1;
                        //RepeatTimer5.Start();
                    //}
                    break;
                case 6:
                    StartTimer6 = new System.Timers.Timer { Interval = keyboardDelay };
                    StartTimer6.Elapsed += startTimer_Tick;
                    st_timer6_r = 1; 
                    StartTimer6.Start();
                    //if (!mouse(6))
                    //{
                        //RepeatTimer6 = new System.Timers.Timer { Interval = keyboardSpeed };
                        //RepeatTimer6.Elapsed += repeatTimer_Tick;
                        //r_timer6_r = 1;
                        //RepeatTimer6.Start();
                    //}
                    break;
            }
        }

        /// <summary>
        /// Метод определения не мышь ли выбрана для нажатия
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        //public bool mouse(int i)
        //{
        //    bool m = false;
        //    switch (i)
        //    {
        //        case 1: if ((key_hold_code(key1) == (int)Keys.LButton) || (key_hold_code(key1) == (int)Keys.RButton)) m = true; break;
        //        case 2: if ((key_hold_code(key2) == (int)Keys.LButton) || (key_hold_code(key2) == (int)Keys.RButton)) m = true; break;
        //        case 3: if ((key_hold_code(key3) == (int)Keys.LButton) || (key_hold_code(key3) == (int)Keys.RButton)) m = true; break;
        //        case 4: if ((key_hold_code(key4) == (int)Keys.LButton) || (key_hold_code(key4) == (int)Keys.RButton)) m = true; break;
        //        case 5: if ((key_hold_code(key5) == (int)Keys.LButton) || (key_hold_code(key5) == (int)Keys.RButton)) m = true; break;
        //        case 6: if ((key_hold_code(key6) == (int)Keys.LButton) || (key_hold_code(key6) == (int)Keys.RButton)) m = true; break;
        //    }
        //    return m;
        //}

        public void hold_unload(int i)
        {
            switch (i)
            {
                case 1:
                    if (StartTimer1 != null && StartTimer1.Enabled) StartTimer1.Dispose();//.Stop()
                    if (RepeatTimer1 != null && RepeatTimer1.Enabled) RepeatTimer1.Dispose();//.Stop()
                    break;
                case 2:
                    if (StartTimer2 != null && StartTimer2.Enabled) StartTimer2.Dispose();//.Stop()
                    if (RepeatTimer2 != null && RepeatTimer2.Enabled) RepeatTimer2.Dispose();//.Stop()
                    break;
                case 3:
                    if (StartTimer3 != null && StartTimer3.Enabled) StartTimer3.Dispose();//.Stop()
                    if (RepeatTimer3 != null && RepeatTimer3.Enabled) RepeatTimer3.Dispose();//.Stop()
                    break;
                case 4:
                    if (StartTimer4 != null && StartTimer4.Enabled) StartTimer4.Dispose();//.Stop()
                    if (RepeatTimer4 != null && RepeatTimer4.Enabled) RepeatTimer4.Dispose();//.Stop()
                    break;
                case 5:
                    if (StartTimer5 != null && StartTimer5.Enabled) StartTimer5.Dispose();//.Stop()
                    if (RepeatTimer5 != null && RepeatTimer5.Enabled) RepeatTimer5.Dispose();//.Stop()
                    break;
                case 6:
                    if (StartTimer6 != null && StartTimer6.Enabled) StartTimer6.Dispose();//.Stop()
                    if (RepeatTimer6 != null && RepeatTimer6.Enabled) RepeatTimer6.Dispose();//.Stop()
                    break;
            }
                //hold[i - 1] = 0;
                //keyup(i);
        }

        /// <summary>
        /// Метод для нахождения окна с фокусом
        /// </summary>
        /// <returns></returns>
        //public IntPtr GetFocusedControl()
        //{
        //    IntPtr hFocus = IntPtr.Zero;
        //    IntPtr hFore;
        //    int id = 0;
        //    //узнаем в каком окне находится фокус ввода
        //    hFore = GetForegroundWindow();
        //    //подключаемся к процессу
        //    AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), true);
        //    //получаем хэндл фокуса
        //    hFocus = GetFocus();
        //    //отключаемся от процесса
        //    AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), false);
        //    return hFocus;
        //}

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
