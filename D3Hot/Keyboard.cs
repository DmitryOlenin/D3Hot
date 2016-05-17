﻿using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using WindowsInput.Native;
using System.Runtime.InteropServices;
//using InputManager; //26.03.2015

namespace D3Hot
{
    public partial class d3hot : Form
    {

        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        //public const int MOUSEEVENTF_LEFTUP = 0x04;
        //public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        //public const int MOUSEEVENTF_RIGHTUP = 0x10;

        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern IntPtr GetFocus();

        //[DllImport("user32")]
        //public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern uint GetCurrentThreadId();

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);


        //public int counter = 0, maxRepeatedCharacters = 30; // repeat char 30 times
        private IntPtr handle = IntPtr.Zero;// = GetForegroundWindow();
        private HandleRef handle_ref; //new HandleRef(this, handle);
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
        public System.Timers.Timer[] StartTimer = new System.Timers.Timer[6];//09.07.2015
        public System.Timers.Timer[] RepeatTimer = new System.Timers.Timer[6];//09.07.2015
        
        //public int key_for_hold = 0, key_for_keyup = 0;
        public uint key_down = 0, key_up=0;
        //public int st_timer1_r = 0, st_timer2_r = 0, st_timer3_r = 0, st_timer4_r = 0, st_timer5_r = 0, st_timer6_r = 0,
        //    r_timer1_r = 0, r_timer2_r = 0, r_timer3_r = 0, r_timer4_r = 0, r_timer5_r = 0, r_timer6_r = 0;
        public int keyboardDelay, keyboardSpeed;

        public int[] st_timer_r = new int[] { 0, 0, 0, 0, 0, 0 }; //09.07.2015
        public int[] r_timer_r = new int[] { 0, 0, 0, 0, 0, 0 }; //09.07.2015


        public void keyup(int i)
        {
            uint key_for_keyup = key_h[i];//0;
            VirtualKeyCode key = key_v[i];
            
            uint ret = 0;

            //switch (i)
            //{
            //    case 1: key_for_keyup = key_h[0]; break; //key_hold_code(key1)
            //    case 2: key_for_keyup = key_h[1]; break; //key_hold_code(key2)
            //    case 3: key_for_keyup = key_h[2]; break; //key_hold_code(key3)
            //    case 4: key_for_keyup = key_h[3]; break; //key_hold_code(key4)
            //    case 5: key_for_keyup = key_h[4]; break; //key_hold_code(key5)
            //    case 6: key_for_keyup = key_h[5]; break; //key_hold_code(key6)
            //}

            ret = NativeMethods._MapVirtualKey(key_for_keyup, 0);

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
                try
                {
                    if (usage_area()) //09.12.2015
                        keyb_down(key_for_keyup, 2);//inp.Keyboard.KeyUp(key);
                    else
                        NativeMethods.PostMessage(handle_ref,//hWindow,
                               updown_keys(key_for_keyup) + 1,//(int)WM_KEYUP,
                               (IntPtr)key_for_keyup, UIntPtr.Zero); //(int) //key_for_keyup //(IntPtr)(MakeLong(1, ret) + 0xC0000000)
                }
                catch (Exception e)
                {
                    MessageBox.Show("Data: " + e.Data + " Exeption: " + e.Message
                        //"Handle: " + handle_ref.ToString() + " updown_keys: " 
                        //+ (updown_keys(key_for_keyup) + 1).ToString()
                        //+ " key_for_keyup "+ ((IntPtr)key_for_keyup).ToString() 
                        //+ " lparam: " + ((IntPtr)(MakeLong(1, ret) + 0xC0000000)).ToString()
                        ); 
                }
            }
        }

        public uint timer_key(System.Timers.Timer timer, out VirtualKeyCode key)
        {
            uint key_for_hold = 0; //09.09.2015
            key = VirtualKeyCode.ESCAPE;

            //for (int i = 0; i < 6; i++)
            //{
            //    if (timer == RepeatTimer[i] || timer == StartTimer[i] || timer == tmr[i])
            //        key_for_hold = key_h[i];
            //}

            int i = -1;

                if (tmr_cdr_curr == 1 || timer == RepeatTimer[0] || timer == StartTimer[0] || timer == tmr[0]) i=0; else //key_for_hold = key_h[0]
                if (tmr_cdr_curr == 2 || timer == RepeatTimer[1] || timer == StartTimer[1] || timer == tmr[1]) i=1; else
                if (tmr_cdr_curr == 3 || timer == RepeatTimer[2] || timer == StartTimer[2] || timer == tmr[2]) i=2; else
                if (tmr_cdr_curr == 4 || timer == RepeatTimer[3] || timer == StartTimer[3] || timer == tmr[3]) i=3; else
                if (tmr_cdr_curr == 5 || timer == RepeatTimer[4] || timer == StartTimer[4] || timer == tmr[4]) i=4; else
                if (tmr_cdr_curr == 6 || timer == RepeatTimer[5] || timer == StartTimer[5] || timer == tmr[5]) i=5;

                if (i > -1)
                {
                    key_for_hold = key_h[i];
                    key = key_v[i];
                }
                 
            return key_for_hold;
        }

        public uint updown_keys(uint key_for_hold)
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

                VirtualKeyCode key = VirtualKeyCode.ESCAPE;//VirtualKeyCode.VK_0;
                uint key_for_hold = timer_key(timer, out key);

                if (key_for_hold > 0) //09.09.2015
                {
                    uint ret = 0;
                    ret = NativeMethods._MapVirtualKey(key_for_hold, 0);

                    if ((key_for_hold == (int)Keys.LButton) || (key_for_hold == (int)Keys.RButton))
                    {
                        //IntPtr handle1;
                        //handle1 = FindWindow(null, "akelpad");
                        //handle1 = FindWindowEx(handle1, IntPtr.Zero, "AkelEditW", null); //For debugging
                        //if (usage_area() || handle == handle1)

                        if (usage_area()
                            || (lb_debug.Visible && handle == NativeMethods.FindWindowEx(NativeMethods.FindWindow(null, "akelpad"), IntPtr.Zero, "AkelEditW", null))
                            //|| (lb_debug.Visible && handle == FindWindowEx(proc_handle[proc_curr], IntPtr.Zero, "Chrome_RenderWidgetHostHWND", null))
                            )
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

                        if (usage_area()) //09.12.2015
                            keyb_down(key_for_hold, 0);//inp.Keyboard.KeyDown(key);
                        else
                            //MessageBox.Show("Make long: " + ((IntPtr)(MakeLong(1, ret))).ToString() + "Another way: " + MakeLParam(1, ret).ToString());
                            NativeMethods.PostMessage(handle_ref,//hWindow,
                                        updown_keys(key_for_hold),
                                        (IntPtr)key_for_hold, (UIntPtr)(MakeLong(1, ret)));

                        //MessageBox.Show("Process ID: "+proc_curr + ", Handle: " + handle.ToString() + ", Клавиша: " + key_for_hold.ToString());
                    }

                }
                else timer.Stop();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Ликвидировать объекты перед потерей области")]
        public void startTimer_Tick(object sender, EventArgs eventArgs)
        {
            System.Timers.Timer timer = sender as System.Timers.Timer;
            if (timer != null) timer.Stop();

            VirtualKeyCode key = VirtualKeyCode.ESCAPE;//VirtualKeyCode.VK_0;
            uint key_for_hold = timer_key(timer, out key);

            if (key_for_hold > 0) //09.09.2015
            {
                int i = -1;

                for (int j = 0; j < 6; j++)
                {
                    if (timer == StartTimer[j] && st_timer_r[j] > 0)
                    {
                        i = j;
                        st_timer_r[j] = 0;
                        break;
                    }
                }

                uint ret = 0;
                ret = NativeMethods._MapVirtualKey(key_for_hold, 0);


                if (press_type == 0)
                {
                    if (usage_area()) //09.12.2015
                    {
                        if (key_for_hold == (int)Keys.LButton) inp.Mouse.LeftButtonDown();
                        else if (key_for_hold == (int)Keys.RButton) inp.Mouse.RightButtonDown();
                        else sendinput_push(key, key_for_hold);
                    }

                    else if ((int)handle > 0)
                    {
                        post_push(key_for_hold);
                    }
                }

                else if (press_type == 1)
                    post_push(key_for_hold);

                else if (press_type > 1)
                    {
                        if (key_for_hold == (int)Keys.LButton) inp.Mouse.LeftButtonDown();
                        else if (key_for_hold == (int)Keys.RButton) inp.Mouse.RightButtonDown();
                        else sendinput_push(key, key_for_hold);
                    }

                //if ((key_for_hold == (int)Keys.LButton) || (key_for_hold == (int)Keys.RButton))
                //{
                //    if (i != -1)
                //    {
                //        //IntPtr handle1;
                //        //handle1 = FindWindow(null, "akelpad");
                //        //handle1 = FindWindowEx(handle1, IntPtr.Zero, "AkelEditW", null);  //For debugging
                //        //if (usage_area() || handle == handle1)

                //        if (usage_area()
                //            || (lb_debug.Visible && handle == FindWindowEx(FindWindow(null, "akelpad"), IntPtr.Zero, "AkelEditW", null))
                //            //|| (lb_debug.Visible && handle == FindWindowEx(proc_handle[proc_curr], IntPtr.Zero, "Chrome_RenderWidgetHostHWND", null))
                //            )
                //        {
                //            if (key_for_hold == (int)Keys.LButton)
                //                inp.Mouse.LeftButtonDown();
                //            if (key_for_hold == (int)Keys.RButton)
                //                inp.Mouse.RightButtonDown();
                //        }

                //        //if (key_for_hold == (int)Keys.LButton) Mouse.ButtonDown(Mouse.MouseKeys.Left); //26.03.2015
                //        //if (key_for_hold == (int)Keys.RButton) Mouse.ButtonDown(Mouse.MouseKeys.Right); //26.03.2015

                //        //Point defPnt = new Point();
                //        //GetCursorPos(ref defPnt);

                //        //PostMessage(handle,
                //        //           updown_keys(key_for_hold),
                //        //           key_for_hold, (int)MakeLong(defPnt.X, defPnt.Y));
                //    }
                //}
                //else
                //{
                //    PostMessage(handle_ref,//hWindow,
                //               updown_keys(key_for_hold),//(int)WM_KEYDOWN,
                //               (IntPtr)key_for_hold, (UIntPtr)(MakeLong(1, ret)));
                //}

                if (i > -1)
                    System.Threading.Thread.Sleep(50);

                RepeatTimer[i] = new System.Timers.Timer { Interval = keyboardSpeed };
                RepeatTimer[i].Elapsed += repeatTimer_Tick;
                r_timer_r[i] = 1;
                RepeatTimer[i].Start();
            }
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Ликвидировать объекты перед потерей области")]
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

            StartTimer[i] = new System.Timers.Timer { Interval = keyboardDelay };
            StartTimer[i].Elapsed += startTimer_Tick;
            st_timer_r[i] = 1;
            StartTimer[i].Start();

            //switch (i)
            //{
            //    case 1:
            //        StartTimer[0] = new System.Timers.Timer { Interval = keyboardDelay };
            //        StartTimer[0].Elapsed += startTimer_Tick;
            //        st_timer_r[0] = 1; 
            //        StartTimer[0].Start();
            //        //if (!mouse(1))
            //        //{
            //            //RepeatTimer[0] = new System.Timers.Timer { Interval = keyboardSpeed };
            //            //RepeatTimer[0].Elapsed += repeatTimer_Tick;
            //            //r_timer_r[0] = 1;
            //            //RepeatTimer[0].Start();
            //        //}
            //        break;
            //    case 2:
            //        StartTimer[1] = new System.Timers.Timer { Interval = keyboardDelay };
            //        StartTimer[1].Elapsed += startTimer_Tick;
            //        st_timer_r[1] = 1; 
            //        StartTimer[1].Start();
            //        //if (!mouse(2))
            //        //{
            //            //RepeatTimer[1] = new System.Timers.Timer { Interval = keyboardSpeed };
            //            //RepeatTimer[1].Elapsed += repeatTimer_Tick;
            //            //r_timer_r[1] = 1;
            //            //RepeatTimer[1].Start();
            //        //}
            //        break;
            //    case 3:
            //        StartTimer[2] = new System.Timers.Timer { Interval = keyboardDelay };
            //        StartTimer[2].Elapsed += startTimer_Tick;
            //        st_timer_r[2] = 1; 
            //        StartTimer[2].Start();
            //        //if (!mouse(3))
            //        //{
            //            //RepeatTimer[2] = new System.Timers.Timer { Interval = keyboardSpeed };
            //            //RepeatTimer[2].Elapsed += repeatTimer_Tick;
            //            //r_timer_r[2] = 1;
            //            //RepeatTimer[2].Start();
            //        //}
            //        break;
            //    case 4:
            //        StartTimer[3] = new System.Timers.Timer { Interval = keyboardDelay };
            //        StartTimer[3].Elapsed += startTimer_Tick;
            //        StartTimer[3].AutoReset = false;
            //        st_timer_r[3] = 1; 
            //        StartTimer[3].Start();
            //        //if (!mouse(4))
            //        //{
            //            //RepeatTimer[3] = new System.Timers.Timer { Interval = keyboardSpeed };
            //            //RepeatTimer[3].Elapsed += repeatTimer_Tick;
            //            //r_timer_r[3] = 1;
            //            //RepeatTimer[3].Start();
            //        //}
            //        break;
            //    case 5:
            //        StartTimer[4] = new System.Timers.Timer { Interval = keyboardDelay };
            //        StartTimer[4].Elapsed += startTimer_Tick;
            //        st_timer_r[4] = 1; 
            //        StartTimer[4].Start();
            //        //if (!mouse(5))
            //        //{
            //            //RepeatTimer[4] = new System.Timers.Timer { Interval = keyboardSpeed };
            //            //RepeatTimer[4].Elapsed += repeatTimer_Tick;
            //            //r_timer_r[4] = 1;
            //            //RepeatTimer[4].Start();
            //        //}
            //        break;
            //    case 6:
            //        StartTimer[5] = new System.Timers.Timer { Interval = keyboardDelay };
            //        StartTimer[5].Elapsed += startTimer_Tick;
            //        st_timer_r[5] = 1; 
            //        StartTimer[5].Start();
            //        //if (!mouse(6))
            //        //{
            //            //RepeatTimer[5] = new System.Timers.Timer { Interval = keyboardSpeed };
            //            //RepeatTimer[5].Elapsed += repeatTimer_Tick;
            //            //r_timer_r[5] = 1;
            //            //RepeatTimer[5].Start();
            //        //}
            //        break;
            //}
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
            if (StartTimer[i] != null && StartTimer[i].Enabled) StartTimer[i].Dispose();//.Stop()
            if (RepeatTimer[i] != null && RepeatTimer[i].Enabled) RepeatTimer[i].Dispose();//.Stop()
            
            //switch (i)
            //{
            //    case 1:
            //        if (StartTimer[0] != null && StartTimer[0].Enabled) StartTimer[0].Dispose();//.Stop()
            //        if (RepeatTimer[0] != null && RepeatTimer[0].Enabled) RepeatTimer[0].Dispose();//.Stop()
            //        break;
            //    case 2:
            //        if (StartTimer[1] != null && StartTimer[1].Enabled) StartTimer[1].Dispose();//.Stop()
            //        if (RepeatTimer[1] != null && RepeatTimer[1].Enabled) RepeatTimer[1].Dispose();//.Stop()
            //        break;
            //    case 3:
            //        if (StartTimer[2] != null && StartTimer[2].Enabled) StartTimer[2].Dispose();//.Stop()
            //        if (RepeatTimer[2] != null && RepeatTimer[2].Enabled) RepeatTimer[2].Dispose();//.Stop()
            //        break;
            //    case 4:
            //        if (StartTimer[3] != null && StartTimer[3].Enabled) StartTimer[3].Dispose();//.Stop()
            //        if (RepeatTimer[3] != null && RepeatTimer[3].Enabled) RepeatTimer[3].Dispose();//.Stop()
            //        break;
            //    case 5:
            //        if (StartTimer[4] != null && StartTimer[4].Enabled) StartTimer[4].Dispose();//.Stop()
            //        if (RepeatTimer[4] != null && RepeatTimer[4].Enabled) RepeatTimer[4].Dispose();//.Stop()
            //        break;
            //    case 6:
            //        if (StartTimer[5] != null && StartTimer[5].Enabled) StartTimer[5].Dispose();//.Stop()
            //        if (RepeatTimer[5] != null && RepeatTimer[5].Enabled) RepeatTimer[5].Dispose();//.Stop()
            //        break;
            //}
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
