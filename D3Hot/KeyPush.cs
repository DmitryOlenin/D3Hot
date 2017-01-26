using System;
using System.Windows.Forms;
using System.Drawing;
using WindowsInput.Native;

namespace D3Hot
{
    public partial class D3Hotkeys //: Form
    {
        private void post_push(uint keyForHold)
        {
            var ret = NativeMethods._MapVirtualKey(keyForHold, 0);

            if (keyForHold == (int)Keys.LButton || keyForHold == (int)Keys.RButton)
            {
                var defPnt = new Point();
                NativeMethods.GetCursorPos(ref defPnt);

                if (_debug)
                {
                    //Cursor.Position = new Point(1556, 705);
                    //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 1);
                    //Thread.Sleep(100);
                    //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 1);

                    //inp.Mouse.LeftButtonDown();
                    //Thread.Sleep(100);
                    //inp.Mouse.LeftButtonUp();

                    //mouse_event(MOUSEEVENTF_LEFTDOWN, 1556, 705, 0, 0);
                    //mouse_event(MOUSEEVENTF_LEFTUP, 1556, 705, 0, 0);

                    if (keyForHold == (int)Keys.LButton) //08.12.2015
                        _inp.Mouse.LeftButtonClick();
                    else
                        _inp.Mouse.RightButtonClick();
                }
                else
                {
                    NativeMethods.PostMessage(_handleRef,
                               updown_keys(keyForHold),
                               IntPtr.Zero, MakeLParam(defPnt.X, defPnt.Y)); //(IntPtr)key_for_hold //11.09.2015
                    System.Threading.Thread.Sleep(20); //08.07.2016 Ждём 20мс до отжатия кнопки.
                    NativeMethods.PostMessage(_handleRef,
                               updown_keys(keyForHold) + 1,
                               IntPtr.Zero, MakeLParam(defPnt.X, defPnt.Y)); //(IntPtr)0 //11.09.2015
                }

            }

            else
            {

                const uint repeatCount = 1;
                var scanCode = ret; //0x03;//0x2D;
                const uint extended = 0;
                const uint context = 0;
                uint previousState = 0;
                uint transition = 0;

                // combine the parameters above according to the bit
                // fields described in the MSDN page for WM_KEYDOWN

                var lParam = repeatCount
                    | (scanCode << 16)
                    | (extended << 24)
                    | (context << 29)
                    | (previousState << 30)
                    | (transition << 31);

                //11.11.2015
                NativeMethods.PostMessage(_handleRef,//hWindow,
                           updown_keys(keyForHold),//(int)WM_KEYDOWN,
                           (IntPtr)keyForHold, (UIntPtr)lParam); //(IntPtr)(MakeLong(1, ret)) //11.09.2015 //UIntPtr.Zero //07.12.2015

                //System.Threading.Thread.Sleep(5); //11.11.2015

                previousState = 1;
                transition = 1;

                lParam = repeatCount
                    | (scanCode << 16)
                    | (extended << 24)
                    | (context << 29)
                    | (previousState << 30)
                    | (transition << 31);

                NativeMethods.PostMessage(_handleRef,//hWindow,
                            updown_keys(keyForHold) + 1,//(int)WM_KEYUP,
                            (IntPtr)keyForHold, (UIntPtr)lParam); //(IntPtr)(MakeLong(1, ret) + 0xC0000000) //11.09.2015  //(IntPtr)(0 | 0xc0000000) //(0 | 0xc0000000)
                //MessageBox.Show("1: " + tmr_local.Enabled.ToString() + " " + ret.ToString());
            }
        }

        private void sendinput_push(VirtualKeyCode key, uint keyForHold)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases - Используем  не все клавиши
            switch (key)
            {
                case VirtualKeyCode.LBUTTON: _inp.Mouse.LeftButtonClick(); break;
                case VirtualKeyCode.RBUTTON: _inp.Mouse.RightButtonClick(); break;
                case VirtualKeyCode.XBUTTON1: shift_click(1); break;
                case VirtualKeyCode.XBUTTON2: shift_click(2); break;
                case VirtualKeyCode.ESCAPE: break; //VK_0
                default:

                    //inp.Keyboard.KeyDown(key); //08.12.2015
                    //inp.Keyboard.Sleep((int)coold_delay/3); //50 //10.12.2015
                    //inp.Keyboard.KeyUp(key);

                    //inp.Keyboard.KeyPress(VirtualKeyCode.DOWN); 

                    //short key1 = 0x14;
                    //PressKey((short)DirectXScanCode.DIK_DOWN);

                    //GenerateKey((short)DirectXScanCode.DIK_DOWN, false);

                    //uint KEYEVENTF_KEYUP = 0x02;
                    //byte keyp = 0x41; //A
                    //uint scanCode = MapVirtualKey((uint)keyp, 0);

                    switch (_pressType)
                    {
                        case 2:
                            keyb_down(keyForHold, 0); //06.05.2016
                            System.Threading.Thread.Sleep(20); //06.05.2016
                            keyb_down(keyForHold, 2); //06.05.2016
                            break;
                        case 0:
                        case 3:
                            _inp.Keyboard.KeyPress(key);
                            break;
                    }

                    //keybd_event((byte)key_for_hold, (byte)ret, 1 | 0, 0);
                    //keybd_event((byte)key_for_hold, (byte)ret, 1 | 2, 0);

                    //key codes: http://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
                    //key_codes2: http://www.kbdedit.com/manual/low_level_vk_list.html
                    //vParam, lParam: http://pastebin.com/f6c1db818
                    //DirectInput keycodes: http://www.flint.jp/misc/?q=dik&lang=en
                    //mapvirtualkey: http://www.pinvoke.net/default.aspx/user32.mapvirtualkey

                    
                    //MessageBox.Show("vk :" + key.ToString() + " ret: " + ret.ToString() + " vcode: " + key_for_hold.ToString());

                    break;
            }
        }


        private void keyb_down(uint keyForHold, int action)
        {
            if (action != 0) action = 2; //0  - Down, 2 - Up.
            var ret = NativeMethods._MapVirtualKey(keyForHold, 0);
            NativeMethods.keybd_event((byte)keyForHold, (byte)ret, (uint)action, IntPtr.Zero); //0
        }

    }
}
