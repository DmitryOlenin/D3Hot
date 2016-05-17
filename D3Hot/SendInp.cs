using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;   //Needed to import your .dll

namespace D3Hot
{
    public partial class d3hot : Form
    {


        //0-15	
        //Определяет счет повторений текущего сообщения. Значение равно числу автоповтора нажатий клавиши в результате того, что пользователь удерживает клавишу нажатой. Если нажатие клавиши удерживается достаточно долго, отправляются многочисленные сообщения . Однако, счет повторений не накапливается.

        //16-23	
        //Определяет скэн-код. Значение зависит от фирмы - изготовителя комплектного оборудования (OEM).

        //24	
        //Определяет, является ли клавиша дополнительной клавишей, типа правосторонних клавиш ALT и CTRL, которые появляются на усовершенствованной 101- или 102-клавишной клавиатуре. Значение равно 1, если это дополнительная клавиша; иначе, оно равно 0.

        //25-28	
        //Зарезервировано; не используется.

        //29	
        //Определяет контекстный код. Это значение - всегда 0 для сообщения WM_KEYDOWN.

        //30	
        //Определяет предыдущее состояние клавиши. Это значение равно 1 в том случае, если клавиша была нажата перед отправкой сообщения или оно равно 0, если клавиша была не нажата.

        //31	
        //Определяет переходное состояние. Это значение - всегда 0 для сообщения WM_KEYDOWN.


        //[DllImport("user32.dll")]
        //static extern UInt32 SendInput(UInt32 nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] pInputs, Int32 cbSize);

        //[StructLayout(LayoutKind.Sequential)]
        //struct MOUSEINPUT
        //{
        //    public int dx;
        //    public int dy;
        //    public int mouseData;
        //    public int dwFlags;
        //    public int time;
        //    public IntPtr dwExtraInfo;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //struct KEYBDINPUT
        //{
        //    public short wVk;      //Virtual KeyCode (not needed here)
        //    public short wScan;    //Directx Keycode 
        //    public int dwFlags;    //This tells you what is use (Keyup, Keydown..)
        //    public int time;
        //    public IntPtr dwExtraInfo;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //struct HARDWAREINPUT
        //{
        //    public int uMsg;
        //    public short wParamL;
        //    public short wParamH;
        //}

        //[StructLayout(LayoutKind.Explicit)]
        //struct INPUT
        //{
        //    [FieldOffset(0)]
        //    public int type;
        //    [FieldOffset(4)]
        //    public KEYBDINPUT ki;
        //}

        //const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        //const int KEYEVENTF_KEYUP = 0x0002;
        //const int KEYEVENTF_UNICODE = 0x0004;
        //const int KEYEVENTF_SCANCODE = 0x0008;

        //[Flags]
        //public enum KeyFlag
        //{
        //    KeyDown = 0x0000,
        //    KeyUp = 0x0002,
        //    Scancode = 0x0008
        //}

        //public static void SendKey(short keyCode, KeyFlag keyFlag)
        //{
        //    INPUT[] InputData = new INPUT[1];

        //    InputData[0].type = 1;
        //    //InputData[0].ki.wVk = 50;
        //    InputData[0].ki.wScan = keyCode; // 0x14 = T for example
        //    InputData[0].ki.dwFlags = (int)keyFlag;
        //    InputData[0].ki.time = 0;
        //    InputData[0].ki.dwExtraInfo = IntPtr.Zero;

        //    SendInput(1, InputData, Marshal.SizeOf(typeof(INPUT)));
        //}

        //public static void PressKey(short key)
        //{
        //    SendKey(key, KeyFlag.KeyDown | KeyFlag.Scancode);
        //    Thread.Sleep(50);
        //    SendKey(key, KeyFlag.KeyUp | KeyFlag.Scancode);
        //}

        //public static void GenerateKey(short vk, bool bExtended)
        //{
        //    INPUT[] inputs = new INPUT[1];
        //    inputs[0].type = 1;

        //    KEYBDINPUT kb = new KEYBDINPUT(); //{0};
        //    // generate down 
        //    if (bExtended)
        //        kb.dwFlags = KEYEVENTF_EXTENDEDKEY;

        //    kb.wVk = vk;
        //    inputs[0].ki = kb;
        //    SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));

        //    // generate up 
        //    //ZeroMemory(&kb, sizeof(KEYBDINPUT));
        //    //ZeroMemory(&inputs,sizeof(inputs));
        //    kb.dwFlags = KEYEVENTF_KEYUP;
        //    if (bExtended)
        //        kb.dwFlags |= KEYEVENTF_EXTENDEDKEY;

        //    kb.wVk = vk;
        //    inputs[0].type = 1;
        //    inputs[0].ki = kb;
        //    SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
        //}


        //public enum DirectXScanCode : ushort
        //{
        //    DIK_ESCAPE = 0x01,	/* Esc */
        //    DIK_1 = 0x02,	/* 1 */
        //    DIK_2 = 0x03,	/* 2 */
        //    DIK_3 = 0x04,	/* 3 */
        //    DIK_4 = 0x05,	/* 4 */
        //    DIK_5 = 0x06,	/* 5 */
        //    DIK_6 = 0x07,	/* 6 */
        //    DIK_7 = 0x08,	/* 7 */
        //    DIK_8 = 0x09,	/* 8 */
        //    DIK_9 = 0x0A,	/* 9 */
        //    DIK_0 = 0x0B,	/* 0 */
        //    DIK_MINUS = 0x0C,	/* - */
        //    DIK_EQUALS = 0x0D,	/* = */
        //    DIK_BACK = 0x0E,	/* Back Space */
        //    DIK_TAB = 0x0F,	/* Tab */
        //    DIK_Q = 0x10,	/* Q */
        //    DIK_W = 0x11,	/* W */
        //    DIK_E = 0x12,	/* E */
        //    DIK_R = 0x13,	/* R */
        //    DIK_T = 0x14,	/* T */
        //    DIK_Y = 0x15,	/* Y */
        //    DIK_U = 0x16,	/* U */
        //    DIK_I = 0x17,	/* I */
        //    DIK_O = 0x18,	/* O */
        //    DIK_P = 0x19,	/* P */
        //    DIK_LBRACKET = 0x1A,	/* [ */
        //    DIK_RBRACKET = 0x1B,	/* ] */
        //    DIK_RETURN = 0x1C,	/* Enter */
        //    DIK_LContol = 0x1D,	/* Ctrl (Left) */
        //    DIK_A = 0x1E,	/* A */
        //    DIK_S = 0x1F,	/* S */
        //    DIK_D = 0x20,	/* D */
        //    DIK_F = 0x21,	/* F */
        //    DIK_G = 0x22,	/* G */
        //    DIK_H = 0x23,	/* H */
        //    DIK_J = 0x24,	/* J */
        //    DIK_K = 0x25,	/* K */
        //    DIK_L = 0x26,	/* L */
        //    DIK_SEMICOLON = 0x27,	/* ; */
        //    DIK_APOSTROPHE = 0x28,	/* ' */
        //    DIK_GRAVE = 0x29,	/* ` */
        //    DIK_LSHIFT = 0x2A,	/* Shift (Left) */
        //    DIK_BACKSLASH = 0x2B,	/* \ */
        //    DIK_Z = 0x2C,	/* Z */
        //    DIK_X = 0x2D,	/* X */
        //    DIK_C = 0x2E,	/* C */
        //    DIK_V = 0x2F,	/* V */
        //    DIK_B = 0x30,	/* B */
        //    DIK_N = 0x31,	/* N */
        //    DIK_M = 0x32,	/* M */
        //    DIK_COMMA = 0x33,	/* , */
        //    DIK_PERIOD = 0x34,	/* . */
        //    DIK_SLASH = 0x35,	/* / */
        //    DIK_RSHIFT = 0x36,	/* Shift (Right) */
        //    DIK_MULTIPLY = 0x37,	/* * (Numpad) */
        //    DIK_LMENU = 0x38,	/* Alt (Left) */
        //    DIK_SPACE = 0x39,	/* Space */
        //    DIK_CAPITAL = 0x3A,	/* Caps Lock */
        //    DIK_F1 = 0x3B,	/* F1 */
        //    DIK_F2 = 0x3C,	/* F2 */
        //    DIK_F3 = 0x3D,	/* F3 */
        //    DIK_F4 = 0x3E,	/* F4 */
        //    DIK_F5 = 0x3F,	/* F5 */
        //    DIK_F6 = 0x40,	/* F6 */
        //    DIK_F7 = 0x41,	/* F7 */
        //    DIK_F8 = 0x42,	/* F8 */
        //    DIK_F9 = 0x43,	/* F9 */
        //    DIK_F10 = 0x44,	/* F10 */
        //    DIK_NUMLOCK = 0x45,	/* Num Lock */
        //    DIK_SCROLL = 0x46,	/* Scroll Lock */
        //    DIK_NUMPAD7 = 0x47,	/* 7 (Numpad) */
        //    DIK_NUMPAD8 = 0x48,	/* 8 (Numpad) */
        //    DIK_NUMPAD9 = 0x49,	/* 9 (Numpad) */
        //    DIK_SUBTRACT = 0x4A,	/* - (Numpad) */
        //    DIK_NUMPAD4 = 0x4B,	/* 4 (Numpad) */
        //    DIK_NUMPAD5 = 0x4C,	/* 5 (Numpad) */
        //    DIK_NUMPAD6 = 0x4D,	/* 6 (Numpad) */
        //    DIK_ADD = 0x4E,	/* + (Numpad) */
        //    DIK_NUMPAD1 = 0x4F,	/* 1 (Numpad) */
        //    DIK_NUMPAD2 = 0x50,	/* 2 (Numpad) */
        //    DIK_NUMPAD3 = 0x51,	/* 3 (Numpad) */
        //    DIK_NUMPAD0 = 0x52,	/* 0 (Numpad) */
        //    DIK_DECIMAL = 0x53,	/* . (Numpad) */
        //    DIK_F11 = 0x57,	/* F11 */
        //    DIK_F12 = 0x58,	/* F12 */
        //    DIK_F13 = 0x64,	/* F13 */
        //    DIK_F14 = 0x65,	/* F14 */
        //    DIK_F15 = 0x66,	/* F15 */
        //    DIK_KANA = 0x70,	/* Kana */
        //    DIK_CONVERT = 0x79,	/* Convert */
        //    DIK_NOCONVERT = 0x7B,	/* No Convert */
        //    DIK_YEN = 0x7D,	/* ¥ */
        //    DIK_NUMPADEQUALS = 0x8D,	/* = */
        //    DIK_CIRCUMFLEX = 0x90,	/* ^ */
        //    DIK_AT = 0x91,	/* @ */
        //    DIK_COLON = 0x92,	/* : */
        //    DIK_UNDERLINE = 0x93,	/* _ */
        //    DIK_KANJI = 0x94,	/* Kanji */
        //    DIK_STOP = 0x95,	/* Stop */
        //    DIK_AX = 0x96,	/* (Japan AX) */
        //    DIK_UNLABELED = 0x97,	/* (J3100) */
        //    DIK_NUMPADENTER = 0x9C,	/* Enter (Numpad) */
        //    DIK_RCONTROL = 0x9D,	/* Ctrl (Right) */
        //    DIK_NUMPADCOMMA = 0xB3,	/* , (Numpad) */
        //    DIK_DIVIDE = 0xB5,	/* / (Numpad) */
        //    DIK_SYSRQ = 0xB7,	/* Sys Rq */
        //    DIK_RMENU = 0xB8,	/* Alt (Right) */
        //    DIK_PAUSE = 0xC5,	/* Pause */
        //    DIK_HOME = 0xC7,	/* Home */
        //    DIK_UP = 0xC8,	/* ↑ */
        //    DIK_PRIOR = 0xC9,	/* Page Up */
        //    DIK_LEFT = 0xCB,	/* ← */
        //    DIK_RIGHT = 0xCD,	/* → */
        //    DIK_END = 0xCF,	/* End */
        //    DIK_DOWN = 0xD0,	/* ↓ */
        //    DIK_NEXT = 0xD1,	/* Page Down */
        //    DIK_INSERT = 0xD2,	/* Insert */
        //    DIK_DELETE = 0xD3,	/* Delete */
        //    DIK_LWIN = 0xDB,	/* Windows */
        //    DIK_RWIN = 0xDC,	/* Windows */
        //    DIK_APPS = 0xDD,	/* Menu */
        //    DIK_POWER = 0xDE,	/* Power */
        //    DIK_SLEEP = 0xDF		/* Windows */
        //}

    }
}
