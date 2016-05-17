using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace D3Hot
{
    public partial class d3hot : Form
    {
        internal static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("Kernel32")]
            public extern static Boolean CloseHandle(IntPtr handle);

            public const int HWND_BROADCAST = 0xffff;
            public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
            [DllImport("user32")]

            public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
            [DllImport("user32")]
            public static extern int RegisterWindowMessage(string message);

            [DllImport("user32.dll")]
            public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

            [DllImport("user32.dll")]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll", SetLastError = true)]
            public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId); //11.03.2015 

            [DllImport("user32.dll")]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

            [DllImportAttribute("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport("user32.dll")]
            public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo); //17.03.2015

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName); //17.03.2015

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle); //17.03.2015

            [DllImport("user32.dll", SetLastError = true)]
            //public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam); //17.03.2015
            public static extern IntPtr PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, UIntPtr lParam); //09.09.2015

            //[DllImport("user32.dll", SetLastError = true)] //11.11.2015
            //public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, UIntPtr lParam); 

            [DllImport("user32.dll", EntryPoint = "PostMessage")]
            public static extern int _PostMessage(IntPtr hWnd, int msg, int wParam, uint lParam);

            [DllImport("user32.dll", EntryPoint = "MapVirtualKey")]
            public static extern uint _MapVirtualKey(uint uCode, int uMapType); //11.09.2015

            [DllImport("user32.dll")]
            public static extern uint MapVirtualKey(uint uCode, uint uMapType);

            [DllImport("user32.dll")]
            public static extern bool GetCursorPos(ref Point lpPoint); //25.03.2015

            [DllImport("user32.dll")]
            public static extern bool SetCursorPos(int X, int Y); //09.06.2015

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, UIntPtr lParam); //25.09.2015

            [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, Int32 dwRop);

            [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
            public static extern IntPtr GetDC(IntPtr hwnd);

            [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
            public static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

            [DllImport("user32.dll")]
            public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        }
    }
}
