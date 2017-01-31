using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput.Native;
using Microsoft.Win32;
using Timer = System.Timers.Timer;

namespace D3Hot
{
    public partial class D3Hotkeys //: Form
    {
        private const uint WmKeydown = 0x100;
        //const uint WmKeyup = 0x101;
        //const uint WM_CHAR = 0x102;
        //const uint WM_SYSKEYDOWN = 0x104;
        private const uint WmLbuttondown = 0x201;
        //const uint WM_LBUTTONUP = 0x202;
        private const uint WmRbuttondown = 0x204;
        //const uint WM_RBUTTONUP = 0x205;

        //public System.Windows.Forms.Timer startTimer;
        //public System.Windows.Forms.Timer repeatTimer;

        private readonly Timer[] //09.07.2015
            _startTimer = new Timer[6],
            _repeatTimer = new Timer[6];

        private readonly int[] _stTimerR = {0, 0, 0, 0, 0, 0},
            //09.07.2015
            _rTimerR = {0, 0, 0, 0, 0, 0}; //09.07.2015

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
        private IntPtr _handle = IntPtr.Zero; // = GetForegroundWindow();
        private HandleRef _handleRef; //new HandleRef(this, handle);


        //public int key_for_hold = 0, key_for_keyup = 0;
        //public uint key_down = 0, key_up=0;
        //public int st_timer1_r = 0, st_timer2_r = 0, st_timer3_r = 0, st_timer4_r = 0, st_timer5_r = 0, st_timer6_r = 0,
        //    r_timer1_r = 0, r_timer2_r = 0, r_timer3_r = 0, r_timer4_r = 0, r_timer5_r = 0, r_timer6_r = 0;
        private int _keyboardDelay, _keyboardSpeed, _procId;
        private Dictionary<string, IntPtr> _procHandle;

        private void Keyup(int i)
        {
            var keyForKeyup = KeyH[i]; //0;
            //var key = KeyV[i];

            //uint ret = 0;

            //switch (i)
            //{
            //    case 1: key_for_keyup = key_h[0]; break; //key_hold_code(key1)
            //    case 2: key_for_keyup = key_h[1]; break; //key_hold_code(key2)
            //    case 3: key_for_keyup = key_h[2]; break; //key_hold_code(key3)
            //    case 4: key_for_keyup = key_h[3]; break; //key_hold_code(key4)
            //    case 5: key_for_keyup = key_h[4]; break; //key_hold_code(key5)
            //    case 6: key_for_keyup = key_h[5]; break; //key_hold_code(key6)
            //}

            //if ((key_for_keyup == (int)Keys.LButton) || (key_for_keyup == (int)Keys.RButton))
            //{
            //Point defPnt = new Point();
            //GetCursorPos(ref defPnt);

            //PostMessage(handle,
            //           updown_keys(key_for_keyup)+1,
            //           0, (int)MakeLong(defPnt.X, defPnt.Y));

            //}
            switch (keyForKeyup)
            {
                case (int) Keys.LButton:
                    _inp.Mouse.LeftButtonUp(); //Mouse.ButtonUp(Mouse.MouseKeys.Left);
                    Thread.Sleep(50);
                    if (_inp.InputDeviceState.IsKeyDown(VirtualKeyCode.LBUTTON))
                        _inp.Mouse.LeftButtonUp();
                    _lmousehold = false;
                    break;
                case (int) Keys.RButton:
                    _inp.Mouse.RightButtonUp(); //Mouse.ButtonUp(Mouse.MouseKeys.Right);
                    Thread.Sleep(50);
                    if (_inp.InputDeviceState.IsKeyDown(VirtualKeyCode.RBUTTON))
                        _inp.Mouse.RightButtonUp();
                    _rmousehold = false;
                    break;
                default:
                    try
                    {
                        if (usage_area()) //09.12.2015
                            keyb_down(keyForKeyup, 2); //inp.Keyboard.KeyUp(key);
                        else
                            NativeMethods.PostMessage(_handleRef, //hWindow,
                                    updown_keys(keyForKeyup) + 1, //(int)WM_KEYUP,
                                    (IntPtr) keyForKeyup, UIntPtr.Zero);
                                //(int) //key_for_keyup //(IntPtr)(MakeLong(1, ret) + 0xC0000000)
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(@"Data: " + e.Data + @" Exeption: " + e.Message
                            //"Handle: " + handle_ref.ToString() + " updown_keys: " 
                            //+ (updown_keys(key_for_keyup) + 1).ToString()
                            //+ " key_for_keyup "+ ((IntPtr)key_for_keyup).ToString() 
                            //+ " lparam: " + ((IntPtr)(MakeLong(1, ret) + 0xC0000000)).ToString()
                        );
                    }
                    break;
            }
        }

        private uint timer_key(Timer timer, out VirtualKeyCode key)
        {
            uint keyForHold = 0; //09.09.2015
            key = VirtualKeyCode.ESCAPE;

            //for (int i = 0; i < 6; i++)
            //{
            //    if (timer == RepeatTimer[i] || timer == StartTimer[i] || timer == tmr[i])
            //        key_for_hold = key_h[i];
            //}

            var i = -1;

            if (timer == _repeatTimer[0] || timer == _startTimer[0] || timer == _tmr[0]) i = 0;
            else //key_for_hold = key_h[0] //_tmrCdrCurr == 1 || 
            if (timer == _repeatTimer[1] || timer == _startTimer[1] || timer == _tmr[1]) i = 1;
            else //_tmrCdrCurr == 2 || 
            if (timer == _repeatTimer[2] || timer == _startTimer[2] || timer == _tmr[2]) i = 2;
            else //_tmrCdrCurr == 3 || 
            if (timer == _repeatTimer[3] || timer == _startTimer[3] || timer == _tmr[3]) i = 3;
            else //_tmrCdrCurr == 4 || 
            if (timer == _repeatTimer[4] || timer == _startTimer[4] || timer == _tmr[4]) i = 4;
            else //_tmrCdrCurr == 5 || 
            if (timer == _repeatTimer[5] || timer == _startTimer[5] || timer == _tmr[5]) i = 5; //_tmrCdrCurr == 6 || 

            if (i <= -1) return keyForHold;
            keyForHold = KeyH[i];
            key = KeyV[i];

            return keyForHold;
        }

        private static uint updown_keys(uint keyForHold)
        {
            uint keyDown;
            switch (keyForHold)
            {
                case 1:
                    keyDown = WmLbuttondown;
                    //key_up = WM_LBUTTONUP;
                    break;
                case 2:
                    keyDown = WmRbuttondown;
                    //key_up = WM_RBUTTONUP;
                    break;
                default:
                    keyDown = WmKeydown;
                    //key_up = WM_KEYUP;
                    break;
            }
            return keyDown;
        }

        private void repeatTimer_Tick(object sender, EventArgs e)
        {
            if (_holdNum > -1 && (!_canPress || _trigPressed[_holdNum] == 0)) //Устраняемся, если надо остановиться
            {
                var i1 = _holdNum;
                new Thread(() => hold_clear(i1)).Start();
                return;
            }

            if (!_holded) return;
            var timer = sender as Timer;

            VirtualKeyCode key; //VirtualKeyCode.VK_0;
            var keyForHold = timer_key(timer, out key);

            if (keyForHold > 0) //09.09.2015
            {
                var ret = NativeMethods._MapVirtualKey(keyForHold, 0);

                if (keyForHold == (int) Keys.LButton || keyForHold == (int) Keys.RButton)
                {
                    //IntPtr handle1;
                    //handle1 = FindWindow(null, "akelpad");
                    //handle1 = FindWindowEx(handle1, IntPtr.Zero, "AkelEditW", null); //For debugging
                    //if (usage_area() || handle == handle1)

                    if (!usage_area() &&
                        (!lb_debug.Visible ||
                         _handle !=
                         NativeMethods.FindWindowEx(NativeMethods.FindWindow(null, "akelpad"), IntPtr.Zero, "AkelEditW",
                             null))) return;
                    if (keyForHold == (int) Keys.LButton && !_lmousehold)
                        // && inp.InputDeviceState.IsKeyUp(VirtualKeyCode.LBUTTON)
                    {
                        //System.Threading.Thread.Sleep(100); 
                        _inp.Mouse.LeftButtonDown();
                        _lmousehold = true;
                    }
                    if (keyForHold != (int) Keys.RButton || _rmousehold) return;
                    //System.Threading.Thread.Sleep(100);
                    _inp.Mouse.RightButtonDown();
                    _rmousehold = true;
                }
                else
                {
                    if (usage_area()) //09.12.2015
                        keyb_down(keyForHold, 0); //inp.Keyboard.KeyDown(key);
                    else
                        //MessageBox.Show("Make long: " + ((IntPtr)(MakeLong(1, ret))).ToString() + "Another way: " + MakeLParam(1, ret).ToString());
                        NativeMethods.PostMessage(_handleRef, //hWindow,
                            updown_keys(keyForHold),
                            (IntPtr) keyForHold, (UIntPtr) MakeLong(1, ret));

                    //MessageBox.Show("Process ID: "+proc_curr + ", Handle: " + handle.ToString() + ", Клавиша: " + key_for_hold.ToString());
                }
            }
            else if (timer != null)
            {
                timer.Stop();
            }
        }

        private void startTimer_Tick(object sender, EventArgs eventArgs)
        {
            if (_holdNum > -1 && (!_canPress || _trigPressed[_holdNum] == 0)) //Устраняемся, если надо остановиться
            {
                var i1 = _holdNum;
                new Thread(() => hold_clear(i1)).Start();
                return;
            }

            var timer = sender as Timer;
            if (timer != null) timer.Stop();

            VirtualKeyCode key; // = VirtualKeyCode.ESCAPE;//VirtualKeyCode.VK_0;
            var keyForHold = timer_key(timer, out key);

            if (keyForHold <= 0) return;
            var i = -1;

            for (var j = 0; j < 6; j++)
            {
                if (timer != _startTimer[j] || _stTimerR[j] <= 0) continue;
                i = j;
                _stTimerR[j] = 0;
                break;
            }

            //uint ret = 0;
            //ret = NativeMethods._MapVirtualKey(keyForHold, 0);


            switch (_pressType)
            {
                case 0:
                    if (usage_area()) //09.12.2015
                        switch (keyForHold)
                        {
                            case (int) Keys.LButton:
                                _inp.Mouse.LeftButtonDown();
                                break;
                            case (int) Keys.RButton:
                                _inp.Mouse.RightButtonDown();
                                break;
                            default:
                                sendinput_push(key, keyForHold);
                                break;
                        }

                    else if ((int) _handle > 0)
                        post_push(keyForHold);
                    break;
                case 1:
                    post_push(keyForHold);
                    break;
                default:
                    if (_pressType > 1)
                    {
                        switch (keyForHold)
                        {
                            case (int) Keys.LButton:
                                _inp.Mouse.LeftButtonDown();
                                break;
                            case (int) Keys.RButton:
                                _inp.Mouse.RightButtonDown();
                                break;
                            default:
                                sendinput_push(key, keyForHold);
                                break;
                        }
                    }
                    break;
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

            if (i <= -1) return;
//11.01.2017 Анализ PVS Studio - возможны ситуации, где i == -1, добавил остальные строки в if
            Thread.Sleep(50);

            _repeatTimer[i] = new Timer ();
            try
            {
                _repeatTimer[i].Interval = _keyboardSpeed;
                _repeatTimer[i].Elapsed += repeatTimer_Tick;
                _rTimerR[i] = 1;
                _repeatTimer[i].Start();
            }
            catch 
            {
                _repeatTimer[i].Dispose();
            }
        }

        private void hold_load(int i)
        {
            _holded = true;
            //int keyboardDelay, keyboardSpeed;
            using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Keyboard"))
            {
                //Debug.Assert(key != null);
                _keyboardDelay = 1;
                if (key != null)
                {
                    int.TryParse((string) key.GetValue("KeyboardDelay", "1"), out _keyboardDelay);
                    _keyboardSpeed = 31;
                    int.TryParse((string) key.GetValue("KeyboardSpeed", "31"), out _keyboardSpeed);
                }
            }

            _startTimer[i] = new Timer();

            try
            {
                _startTimer[i].Interval = _keyboardDelay;
                _startTimer[i].Elapsed += startTimer_Tick;
                _stTimerR[i] = 1;
                _startTimer[i].Start();
            }
            catch 
            {
                _startTimer[i].Dispose();
            }

        }

        /// <summary>
        ///     Метод определения не мышь ли выбрана для нажатия
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
        private void hold_unload(int i)
        {
            if (_startTimer[i] != null) //&& _startTimer[i].Enabled 23.01.2017
            {
                _startTimer[i].Dispose(); //.Stop()
                _startTimer[i] = null;
            }
            if (_repeatTimer[i] == null) return; //|| !_repeatTimer[i].Enabled 23.01.2017
            _repeatTimer[i].Dispose(); //.Stop()
            _repeatTimer[i] = null;
        }

        //    //отключаемся от процесса
        //    hFocus = GetFocus();
        //    //получаем хэндл фокуса
        //    AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), true);
        //    //подключаемся к процессу
        //    hFore = GetForegroundWindow();
        //    //узнаем в каком окне находится фокус ввода
        //    int id = 0;
        //    IntPtr hFore;
        //    IntPtr hFocus = IntPtr.Zero;
        //{
        //public IntPtr GetFocusedControl()
        // <returns></returns>
        // </summary>
        // Метод для нахождения окна с фокусом

        // <summary>
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