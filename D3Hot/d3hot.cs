using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using D3Hot.Properties;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using System.Windows.Input; //11.03.2015 
//using InputManager; //26.03.2015
//InputManager - http://www.codeproject.com/Articles/117657/InputManager-library-Track-user-input-and-simulate

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public string title = "Diablo 3 Hotkeys ver. 2.0";
        public System.Timers.Timer tmr1, tmr2, tmr3, tmr4, tmr5, tmr6, tmr_all, tmr_save;
        public System.Timers.Timer StartTimer1, RepeatTimer1, StartTimer2, RepeatTimer2, StartTimer3, RepeatTimer3,
                                    StartTimer4, RepeatTimer4, StartTimer5, RepeatTimer5, StartTimer6, RepeatTimer6;
        public Stopwatch tmr1_watch, tmr2_watch, tmr3_watch, tmr4_watch, tmr5_watch, tmr6_watch, delay_watch, return_watch, key_watch, proc_watch;
        public Boolean shift = false, d3prog = false, d3proc = false;
        public InputSimulator inp = new InputSimulator();
        public int trig1 = 0, trig2 = 0, trig3 = 0, trig4 = 0, trig5 = 0, trig6 = 0,
            key1 = 0, key2 = 0, key3 = 0, key4 = 0, key5 = 0, key6 = 0,
            tmr1_f = 0, tmr2_f = 0, tmr3_f = 0, tmr4_f = 0, tmr5_f = 0, tmr6_f = 0,
            tmr1_r = 0, tmr2_r = 0, tmr3_r = 0, tmr4_r = 0, tmr5_r = 0, tmr6_r = 0,
            pause = 0, prof_prev, tp_delay = 0, tmr_all_counter = 0, map_delay
            ,hold_key0 = 0, hold_key1 = 0, hold_key2 = 0, hold_key3 = 0, hold_key4 = 0, hold_key5 = 0
            ,multikeys = 0, opt_change = 0, opt_click = 0
            ,tmr1_left = 0, tmr2_left = 0,  tmr3_left = 0,  tmr4_left = 0,  tmr5_left = 0,  tmr6_left = 0
            ,delay_wait = 0
            ;

        public static int key1_h = 0, key2_h = 0, key3_h = 0, key4_h = 0, key5_h = 0, key6_h = 0;
        public static VirtualKeyCode key1_v = 0, key2_v = 0, key3_v = 0, key4_v = 0, key5_v = 0, key6_v = 0;

        public double tmr1_i = 0, tmr2_i = 0, tmr3_i = 0, tmr4_i = 0, tmr5_i = 0, tmr6_i = 0;
        public static bool holded = false, hotkey_pressed = false, prof_name_changed = false, form_shown = false
            , start_main=true, start_opt=true, proc_selected=false, lmousehold = false, rmousehold = false;
        public static int t_press = 0, map_press = 0, return_press = 0, r_press = 0, return_press_count = 0,
             delay_press = 0, delay_press_interval = 0, shift_press = 0;
        public static string tp_key = "", map_key = "", proc_curr = "", key_delay = "", diablo_name = "diablo";//diablo
        public long proc_size = 400000000;

        public static SettingsTable overview;
        public int[] hold = new int[] { 0, 0, 0, 0, 0, 0 }; //17.03.2015
        public Random rand = null; //23.03.2015

        public Class_lang lng = new Class_lang();

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId); //11.03.2015 

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImportAttribute("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo); //17.03.2015

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName); //17.03.2015

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle); //17.03.2015

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam); //17.03.2015

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        private static extern int _PostMessage(IntPtr hWnd, int msg, int wParam, uint lParam);

        [DllImport("user32.dll", EntryPoint = "MapVirtualKey")]
        private static extern int _MapVirtualKey(int uCode, int uMapType);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint); //25.03.2015

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        public void rand_interval(int i)
        {
            int rnd = 0;
            if (nud_rand.Value > 0)
            {
                rand = new Random();
                switch (i)
                {
                    case 1:
                        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
                        if (rnd + tmr1_i < 1) rnd = 31 - (int)tmr1_i;
                        break;
                    case 2:
                        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
                        if (rnd + tmr2_i < 1) rnd = 31 - (int)tmr2_i;
                        break;
                    case 3:
                        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
                        if (rnd + tmr3_i < 1) rnd = 31 - (int)tmr3_i;
                        break;
                    case 4:
                        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
                        if (rnd + tmr4_i < 1) rnd = 31 - (int)tmr4_i;
                        break;
                    case 5:
                        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
                        if (rnd + tmr5_i < 1) rnd = 31 - (int)tmr5_i;
                        break;
                    case 6:
                        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
                        if (rnd + tmr6_i < 1) rnd = 31 - (int)tmr6_i;
                        break;
                }
            }
            switch (i)
            {
                case 1:
                    tmr1.Interval = tmr1_i + rnd;
                    break;
                case 2:
                    tmr2.Interval = tmr2_i + rnd;
                    break;
                case 3:
                    tmr3.Interval = tmr3_i + rnd;
                    break;
                case 4:
                    tmr4.Interval = tmr4_i + rnd;
                    break;
                case 5:
                    tmr5.Interval = tmr5_i + rnd;
                    break;
                case 6:
                    tmr6.Interval = tmr6_i + rnd;
                    break;
            }

        }

        /// <summary>
        /// Метод запуска таймеров, установка задержки по ним.
        /// </summary>
        /// <param name="i"></param>
        public void timer_load(int i)
        {
            switch (i)
            {
                case 0: 
                    tmr_all = new System.Timers.Timer();
                    tmr_all.Elapsed += tmr_all_Elapsed;
                    tmr_all.Interval = 1;
                    break;
                case 1:
                    tmr1 = new System.Timers.Timer();
                    tmr1.Elapsed += tmr_Elapsed;
                    rand_interval(1);
                    //MessageBox.Show("Задержка с рандомом 1: " + tmr1_i.ToString());
                    //tmr1.Interval = tmr1_i;
                    tmr1_watch = new Stopwatch();
                    break;
                case 2:
                    tmr2 = new System.Timers.Timer();
                    tmr2.Elapsed += tmr_Elapsed;
                    rand_interval(2);
                    //MessageBox.Show("Задержка с рандомом 2: " + tmr2_i.ToString());
                    //tmr2.Interval = tmr2_i;
                    tmr2_watch = new Stopwatch();
                    break;
                case 3:
                    tmr3 = new System.Timers.Timer();
                    tmr3.Elapsed += tmr_Elapsed;
                    rand_interval(3);
                    //MessageBox.Show("Задержка с рандомом 3: " + tmr3_i.ToString());
                    //tmr3.Interval = tmr3_i;
                    tmr3_watch = new Stopwatch();
                    break;
                case 4:
                    tmr4 = new System.Timers.Timer();
                    tmr4.Elapsed += tmr_Elapsed;
                    rand_interval(4);
                    //MessageBox.Show("Задержка с рандомом 4: " + tmr4_i.ToString());
                    //tmr4.Interval = tmr4_i;
                    tmr4_watch = new Stopwatch();
                    break;
                case 5:
                    tmr5 = new System.Timers.Timer();
                    tmr5.Elapsed += tmr_Elapsed;
                    rand_interval(5);
                    //MessageBox.Show("Задержка с рандомом 5: " + tmr5_i.ToString());
                    //tmr5.Interval = tmr5_i;
                    tmr5_watch = new Stopwatch();
                    break;
                case 6:
                    tmr6 = new System.Timers.Timer();
                    tmr6.Elapsed += tmr_Elapsed;
                    rand_interval(6);
                    //MessageBox.Show("Задержка с рандомом 6: " + tmr6_i.ToString());
                    //tmr6.Interval = tmr6_i;
                    tmr6_watch = new Stopwatch();
                    break;
            }
        }


        /// <summary>
        /// Метод удаления таймеров при остановке. 99 - всё. 88 - все, кроме главного потока.
        /// </summary>
        /// <param name="i"></param>
        public void timer_unload(int i)
        {
            if (tmr_all != null && (i == 0 || i == 99)) tmr_all.Dispose();
            if (tmr1 != null && (i == 1 || i == 99 || i == 88))
            {
                tmr1_left = (int)tmr1.Interval - (int)tmr1_watch.ElapsedMilliseconds;
                //tmr1.Enabled = false;
                tmr1.Dispose();
                tmr1_watch.Stop();
            }
            if (tmr2 != null && (i == 2 || i == 99 || i == 88))
            {
                tmr2_left = (int)tmr2.Interval - (int)tmr2_watch.ElapsedMilliseconds;
                tmr2.Dispose();
                tmr2_watch.Stop();
            }
            if (tmr3 != null && (i == 3 || i == 99 || i == 88)) 
            {
                tmr3_left = (int)tmr3.Interval - (int)tmr3_watch.ElapsedMilliseconds;
                tmr3.Dispose();
                tmr3_watch.Stop();
            }
            if (tmr4 != null && (i == 4 || i == 99 || i == 88)) 
            {
                tmr4_left = (int)tmr4.Interval - (int)tmr4_watch.ElapsedMilliseconds;
                tmr4.Dispose();
                tmr4_watch.Stop();
            }
            if (tmr5 != null && (i == 5 || i == 99 || i == 88)) 
            {
                tmr5_left = (int)tmr5.Interval - (int)tmr5_watch.ElapsedMilliseconds;
                tmr5.Dispose();
                tmr5_watch.Stop();
            }
            if (tmr6 != null && (i == 6 || i == 99 || i == 88)) 
            {
                tmr6_left = (int)tmr6.Interval - (int)tmr6_watch.ElapsedMilliseconds;
                tmr6.Dispose();
                tmr6_watch.Stop();
            }
        }

        /// <summary>
        /// Метод для отслеживания нажатий T/Enter и подсчёта количества нажатий.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseKeyEventProvider1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == key_delay) delay_press = 1;
            if (e.KeyCode.ToString() == tp_key) t_press = 1;
            if (e.KeyCode.ToString() == map_key) map_press = 1;
            if (e.KeyCode.ToString() == "Return") return_press = 1;
            //lb_press.Text = e.KeyCode.ToString();
        }

        private void mouseKeyEventProvider1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((key_press(trig1) || key_press(trig2) || key_press(trig3) || key_press(trig4) || key_press(trig5) || key_press(trig6))
                &&
            (((key_delay == "LMouse" && e.Button.ToString() == "Left") 
                || (key_delay == "RMouse" && e.Button.ToString() == "Right"))
                && shift_press == 0))
                    delay_press = 1;
        }

        public d3hot()
        {
            InitializeComponent();
            //mouseKeyEventProvider1.Enabled = false;
            this.MaximizeBox = false;
        }

        private void ShowMe()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notify_d3h.Visible = false;
            this.Activate();
        }

        protected override void WndProc(ref Message m)
        {

            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;

            if (m.Msg == WM_SYSCOMMAND)
            {
                //int command = m.WParam.ToInt32() & 0xfff0;
                if (m.WParam.ToInt32() == SC_MINIMIZE)
                {
                    if (pan_opt.Visible || opt_click == 1) b_opt_Click(null, null);
                    error_not(2); //17.04.2015
                    error_select();
                    if (!start_main || !start_opt) // this.WindowState = FormWindowState.Minimized;
                    {
                        m.Result = IntPtr.Zero;
                        error_show(2);
                        return; 
                    }
                }
            }

            if (m.Msg == Program.NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                /* Note that the three lines below are not needed if you only want to register one hotkey.
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, or if you want to know which key/modifier was pressed for some particular reason. */

                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                int j = 0;

                if (id == 0) cb_start.Checked = !cb_start.Checked; //cb_start_CheckedChanged(null, null);
                else
                    if ((id == 1 || id == 2 || id == 3) && !hotkey_pressed && cb_hot_prof.Enabled)
                {
                    hotkey_pressed = true;
                    switch (id)
                    {
                        case 1:
                            if (cb_prof.SelectedIndex != 1)
                            {
                                if (cb_start.Checked)
                                {
                                    j = 1;
                                    cb_start.Checked = !cb_start.Checked;
                                    //cb_start_CheckedChanged(null, null);
                                }
                                //System.Threading.Thread.Sleep(40);
                                cb_prof.SelectedIndex = 1;
                                cb_prof_SelectionChangeCommitted(null, null);
                                //System.Threading.Thread.Sleep(40);
                                if (j == 1)
                                {
                                    //cb_start_CheckedChanged(null, null);
                                    cb_start.Checked = !cb_start.Checked;
                                    j = 0;
                                }
                            }
                            break;
                        case 2:
                            if (cb_prof.SelectedIndex != 2)
                            {
                                if (cb_start.Checked)
                                {
                                    j = 1;
                                    cb_start.Checked = !cb_start.Checked;
                                    //cb_start_CheckedChanged(null, null);
                                }
                                //System.Threading.Thread.Sleep(40);
                                cb_prof.SelectedIndex = 2;
                                cb_prof_SelectionChangeCommitted(null, null);
                                //System.Threading.Thread.Sleep(40);
                                if (j == 1)
                                {
                                    //cb_start_CheckedChanged(null, null);
                                    cb_start.Checked = !cb_start.Checked;
                                    j = 0;
                                }
                            }
                            break;
                        case 3:
                            if (cb_prof.SelectedIndex != 3)
                            {
                                if (cb_start.Checked)
                                {
                                    j = 1;
                                    cb_start.Checked = !cb_start.Checked;
                                    //cb_start_CheckedChanged(null, null);
                                }
                                //System.Threading.Thread.Sleep(40);
                                cb_prof.SelectedIndex = 3;
                                cb_prof_SelectionChangeCommitted(null, null);
                                //System.Threading.Thread.Sleep(40);
                                if (j == 1)
                                {
                                    //cb_start_CheckedChanged(null, null);
                                    cb_start.Checked = !cb_start.Checked;
                                    j = 0;
                                }
                            }
                            break;
                    }
                }
                // do something
                hotkey_pressed = false;
            }
        }

        private void d3hot_Load(object sender, EventArgs e)
        {
           
            //this.Icon = D3Hot.Properties.Resources.diablo_hot;
            //notify_d3h.Icon = D3Hot.Properties.Resources.diablo_hot;

            Load_settings();
            //if (cb_key2.SelectedIndex > 19) MessageBox.Show("456");
            profiles_names();

            pan_prof_name.Width = tb_prof_name.Width;
            pan_prof_name.Height = tb_prof_name.Height;

            //pan_main.BackColor = System.Drawing.Color.Transparent; //01.04.2015 - ПЕРВОЕ АПРЕЛЯ ЖЕ!
            //tb_prof_name.Text = "ONE PIECE - FOREVER!"; //01.04.2015 - ПЕРВОЕ АПРЕЛЯ ЖЕ!

            //Выбор стандартного языка системы при первом запуске
            if (Settings.Default.lb_lang == "")
            {
                if (CultureInfo.CurrentCulture.EnglishName.Contains("Russia")) Settings.Default.lb_lang = "Eng";
                else Settings.Default.lb_lang = "Rus";
                Settings.Default.Save();
            }
            //Загрузка языка программы из настроек
            lb_lang.Text = Settings.Default.lb_lang;
            if (lb_lang.Text == "Eng") lng.Lang_rus(); else lng.Lang_eng();
            Lang();

            //Установка подписи в секундах, если при загрузке настроечные поля заполнены.
            if (nud_tmr1.Value > 0) lb_tmr1_sec.Text = Math.Round((nud_tmr1.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr2.Value > 0) lb_tmr2_sec.Text = Math.Round((nud_tmr2.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr3.Value > 0) lb_tmr3_sec.Text = Math.Round((nud_tmr3.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr4.Value > 0) lb_tmr4_sec.Text = Math.Round((nud_tmr4.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr5.Value > 0) lb_tmr5_sec.Text = Math.Round((nud_tmr5.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr6.Value > 0) lb_tmr6_sec.Text = Math.Round((nud_tmr6.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_key_delay_ms.Value > 0) lb_key_delay_desc.Text = Math.Round((nud_key_delay_ms.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_rand.Value > 0) lb_nud_rand.Text = Math.Round((nud_rand.Value / 1000), 2).ToString() + " " + lng.lang_sec;

            //Установка глобального хоткея запуска/остановки
            reghotkey();

            if (chb_users.Checked) person(1);
            else person(0);

            //22.04.2015
            form_create();

            key_menu();
            chb_hold_CheckedChanged(null, null);
        }

        private void key_menu()
        {
            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("key"))
                {
                    cb.Items[0] = lng.cb_keys_choose;
                    //if (cb.SelectedIndex > 19) MessageBox.Show("123");
                }

            }
        //            int i = cb.SelectedIndex;
        //            cb.Items.Clear();
        //            cb.Items.Add(lng.cb_keys_choose);
        //            cb.Items.Add("1");
        //            cb.Items.Add("2");
        //            cb.Items.Add("3");
        //            cb.Items.Add("4");
        //            cb.Items.Add("LMouse");
        //            cb.Items.Add("RMouse");
        //            cb.Items.Add("Shift+LM");
        //            cb.Items.Add("Shift+RM");
        //            cb.SelectedIndex = i;
        }

        /// <summary>
        /// Метод установки заголовка окна, иконки, фона программы
        /// </summary>
        private void person(int i)
        {
            if (i == 0)
            {
                this.Icon = D3Hot.Properties.Resources.diablo_hot;
                notify_d3h.Icon = D3Hot.Properties.Resources.diablo_hot;
                this.BackgroundImage = null;
                this.Text = title;
            }
            else
            {
                string[] files = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ico", System.IO.SearchOption.TopDirectoryOnly);
                if (files.Length > 0 && File.Exists(files[0]))
                {
                    this.Icon = new System.Drawing.Icon(files[0]);
                    notify_d3h.Icon = new System.Drawing.Icon(files[0]);
                }
                files = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg", System.IO.SearchOption.TopDirectoryOnly);
                if (files.Length > 0 && File.Exists(files[0])) this.BackgroundImage = new Bitmap(files[0]);
                files = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt", System.IO.SearchOption.TopDirectoryOnly);
                if (files.Length > 0 && File.Exists(files[0]))
                {
                    string[] readText = System.IO.File.ReadAllLines(files[0], Encoding.Default);
                    if (readText.Length > 0 && readText[0].Length > 0) this.Text = readText[0];
                    if (this.Text.Length > 30) this.Text = readText[0].Substring(0, 30);
                }
            }
        }

        //private void tmr_save_Elapsed(object sender, EventArgs e)
        //{
        //    Settings.Default.Save();
        //    tmr_save.Stop();
        //}

        private string GetActiveWindowTitle(IntPtr handle_input)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            IntPtr handle = handle_input;
            if (handle_input == IntPtr.Zero) handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        /// <summary>
        /// Метод для проверки зажатой/переключённой клавиши.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool key_press(int i)
        {
            bool result=false;
            switch (i)
            {
                case 1: result = Control.ModifierKeys == Keys.Shift; break;
                case 2: result = Control.IsKeyLocked(Keys.Scroll); break;
                case 3: result = Control.IsKeyLocked(Keys.CapsLock); break;
                case 4: result = Control.IsKeyLocked(Keys.NumLock); break;
            }
            return result;
        }

        /// <summary>
        /// Метод для установки клавиш для нажимания.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        //private VirtualKeyCode virt_code(int i)
        //{
        //    VirtualKeyCode vkc = VirtualKeyCode.VK_0;
        //    switch (i)
        //    {
        //        case 1: vkc = VirtualKeyCode.VK_1; break;
        //        case 2: vkc = VirtualKeyCode.VK_2; break;
        //        case 3: vkc = VirtualKeyCode.VK_3; break;
        //        case 4: vkc = VirtualKeyCode.VK_4; break;
        //        case 5: vkc = VirtualKeyCode.VK_Q; break;
        //        case 6: vkc = VirtualKeyCode.VK_W; break;
        //        case 7: vkc = VirtualKeyCode.VK_E; break;
        //        case 8: vkc = VirtualKeyCode.VK_R; break;
        //        case 9: vkc = VirtualKeyCode.VK_A; break;
        //        case 10: vkc = VirtualKeyCode.VK_S; break;
        //        case 11: vkc = VirtualKeyCode.VK_D; break;
        //        case 12: vkc = VirtualKeyCode.VK_F; break;
        //        case 13: vkc = VirtualKeyCode.VK_Z; break;
        //        case 14: vkc = VirtualKeyCode.VK_X; break;
        //        case 15: vkc = VirtualKeyCode.VK_C; break;
        //        case 16: vkc = VirtualKeyCode.VK_V; break;
        //        case 17: vkc = VirtualKeyCode.SPACE; break;
        //        case 18: vkc = VirtualKeyCode.LBUTTON; break;
        //        case 19: vkc = VirtualKeyCode.RBUTTON; break;
        //        case 20: vkc = VirtualKeyCode.XBUTTON1; break;
        //        case 21: vkc = VirtualKeyCode.XBUTTON2; break;
        //    }
        //    return vkc;
        //}

        /// <summary>
        /// Метод для установки клавиш для залипания.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        //private int key_hold_code(int i)
        //{
        //    int key_code = 0;
        //    switch (i)
        //    {
        //        case 1: key_code = (int)Keys.D1; break;
        //        case 2: key_code = (int)Keys.D2; break;
        //        case 3: key_code = (int)Keys.D3; break;
        //        case 4: key_code = (int)Keys.D4; break;
        //        case 5: key_code = (int)Keys.Q; break;
        //        case 6: key_code = (int)Keys.W; break;
        //        case 7: key_code = (int)Keys.E; break;
        //        case 8: key_code = (int)Keys.R; break;
        //        case 9: key_code = (int)Keys.A; break;
        //        case 10: key_code = (int)Keys.S; break;
        //        case 11: key_code = (int)Keys.D; break;
        //        case 12: key_code = (int)Keys.F; break;
        //        case 13: key_code = (int)Keys.Z; break;
        //        case 14: key_code = (int)Keys.X; break;
        //        case 15: key_code = (int)Keys.C; break;
        //        case 16: key_code = (int)Keys.V; break;
        //        case 17: key_code = (int)Keys.Space; break;
        //        case 18: key_code = (int)Keys.LButton; break;
        //        case 19: key_code = (int)Keys.RButton; break;
        //        //case 19: key_code = (int)Keys.XButton1; break; //27.03.2015
        //        //case 20: key_code = (int)Keys.XButton2; break; //27.03.2015
        //        //case 21: key_code = (int)Keys.Shift; break; //27.03.2015
        //    }
        //    return key_code;
        //}

        /// <summary>
        /// Основной метод после запуска процесса - срабатывание главного таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tmr_all_Elapsed(object sender, EventArgs e)
        {
            //Проверка существования окна //06.04.2015
            //if ((int)handle > 0 && proc_watch != null && !proc_watch.IsRunning)
            //{
            //    proc_watch.Start();
            //}

            //if (proc_watch != null && (int)proc_watch.ElapsedMilliseconds > 10000 && (int)handle > 0)
            //{
            //    proc_watch.Reset();
            //    proc_watch.Stop();
            //    if (GetActiveWindowTitle(handle) == null)
            //        cb_start.Invoke(new Action(() =>
            //            {
            //                cb_start.Checked = false;
            //            }));
            //}

            if ((pause == 2 || pause == 3) && return_press == 1)
            {
                return_press_count++;
                return_press = 0;
                if (return_press_count == 1)
                {
                    return_watch = new Stopwatch();
                    return_watch.Start();
                }
            }

            if (return_press_count > 1 || (return_watch != null && (int)return_watch.ElapsedMilliseconds > 30000)) //Обработка количества Enter для работы с Shift
            {
                return_press_count = 0;
                return_press = 0;
                delay_wait = (int)return_watch.ElapsedMilliseconds;
                return_watch.Stop();
            }

            //if (seconds_count > 0) seconds_count++;


            //Работаем только если хоть что-то из триггеров зажато/переключено.
            if (key_press(trig1) || key_press(trig2) || key_press(trig3) || key_press(trig4) || key_press(trig5) || key_press(trig6))
            {
                //Проверка на одинарное/двойное нажатие Enter.
                //if (
                //        (pause == 2 || pause == 3) && 
                //        (
                //            (return_press == 1 && r_press == 0 && t_press == 0 && map_press == 0) ||
                //                (
                //                    return_press_count==1 && 
                //                    ((trig1==1 && key_press(trig1)) ||
                //                    (trig2==1 && key_press(trig2)) ||
                //                    (trig3==1 && key_press(trig3)) ||
                //                    (trig4==1 && key_press(trig4)) ||
                //                    (trig5==1 && key_press(trig5)) ||
                //                    (trig6==1 && key_press(trig6)))
                //                )
                //        )
                //    )
                if (return_watch != null && return_watch.IsRunning && r_press == 0)
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    r_press = 1; //return_press = 0;
                    hold_clear(88);
                    hold[0] = 0; hold[1] = 0; hold[2] = 0; hold[3] = 0; hold[4] = 0; hold[5] = 0;
                    //delay_watch = new Stopwatch();
                    //delay_watch.Start();
                }

                //if ((r_press == 1 && return_press == 1) || (delay_watch != null && (int)delay_watch.ElapsedMilliseconds > 30000)) 
                else if (r_press == 1 && !return_watch.IsRunning)
                {
                    r_press = 0; t_press = 0; map_press = 0;
                    //r_press = 0; return_press = 0; t_press = 0; map_press = 0; return_press_count = 0;
                    //delay_wait = (int)delay_watch.ElapsedMilliseconds;
                    //delay_watch.Stop();
                }

                //Проверка на нажатие T.
                if ((pause == 1 || pause == 3) && t_press > 0 && tmr_all.Interval == tp_delay * 1000)
                {
                    delay_wait = (int)tmr_all.Interval;
                    t_press = 0;
                    tmr_all.Interval = 1;
                    return_press = 0;
                }

                if ((pause == 1 || pause == 3) && t_press > 0 && r_press == 0)
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    hold_clear(88);
                    hold[0] = 0; hold[1] = 0; hold[2] = 0; hold[3] = 0; hold[4] = 0; hold[5] = 0;
                    tmr_all.Interval = tp_delay * 1000;
                }

                if ((pause == 1 || pause == 3) && r_press > 0 && t_press > 0) t_press = 0;

                //Проверка на нажатие M.
                if (pause == 3 && map_press > 0 && tmr_all.Interval == map_delay * 1000)
                {
                    delay_wait = (int)tmr_all.Interval;
                    map_press = 0;
                    tmr_all.Interval = 1;
                    return_press = 0;
                }

                if (pause == 3 && map_press > 0 && r_press == 0)
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    hold_clear(88);
                    hold[0] = 0; hold[1] = 0; hold[2] = 0; hold[3] = 0; hold[4] = 0; hold[5] = 0;
                    tmr_all.Interval = map_delay * 1000;
                }

                if (pause == 3 && r_press > 0 && map_press > 0) map_press = 0;

                //16.03.2015 Проверка на клавишу задержки
                if (key_watch != null && key_watch.ElapsedMilliseconds > delay_press_interval) //delay_press > 0 && tmr_all.Interval == delay_press_interval
                {
                    delay_wait = (int)tmr_all.Interval;
                    delay_press = 0;
                    if (key_watch != null && key_watch.IsRunning)
                    {
                        key_watch.Reset();
                        key_watch.Stop();
                    }
                    //tmr_all.Interval = 1;
                }

                if (delay_press > 0 && delay_press_interval > 0 && r_press == 0 ) //&& !key_press(1)
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    hold_clear(88);
                    hold[0] = 0; hold[1] = 0; hold[2] = 0; hold[3] = 0; hold[4] = 0; hold[5] = 0;
                    delay_press = 0;
                    //tmr_all.Interval = delay_press_interval;
                    if (key_watch != null) key_watch.Stop();
                    key_watch = new Stopwatch();
                    key_watch.Reset();
                    key_watch.Start();
                }

                if ((pause == 2 || pause == 3) && r_press > 0 && delay_press > 0 ) delay_press = 0;

                //Если T|Enter не нажаты, запускаем таймеры триггеров 1-2-3-4-5-6 при активном состоянии и останавливаем при отключенном.
                if (map_press == 0 && t_press == 0 && r_press == 0 && (delay_press == 0 && (key_watch == null || (key_watch != null && !key_watch.IsRunning))))
                {
                    tmr_all_counter = 3;
                    if (tmr1_f == 1)
                    {
                        if (key_press(trig1))
                        {
                            if (hold_key0 == 1 && hold[0] == 0)  //18.03.2015
                            {
                                hold[0] = 1;
                                hold_load(1);
                            } 
                            else if (tmr1_r == 0 && hold_key0 == 0)
                            {
                                tmr1_r = 1;
                                timer_load(1);

                                if (tmr1_left != 0 && (tmr1_left - delay_wait > 0))
                                    tmr1.Interval = tmr1_left - delay_wait;
                                else
                                        tmr_Elapsed(tmr1, null);
                                if (tmr1 != null)
                                    try { 
                                        tmr1.Enabled = true;
                                        tmr1_watch.Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key0 == 1) hold_clear (0); else
                            if (tmr1_r != 0) timer_unload(1);
                            tmr1_r = 0; 
                        }
                    }

                    if (tmr2_f == 1)
                    {
                        if (key_press(trig2))
                        {
                            if (hold_key1 == 1 && hold[1] == 0)  //18.03.2015
                            {
                                hold[1] = 1;
                                hold_load(2);
                            }
                            else if (tmr2_r == 0 && hold_key1 == 0)
                            {
                                tmr2_r = 1;
                                timer_load(2);
                                if (tmr2_left != 0 && (tmr2_left - delay_wait > 0))
                                    tmr2.Interval = tmr2_left - delay_wait;
                                else
                                    tmr_Elapsed(tmr2, null);
                                if (tmr2 != null)
                                    try { 
                                        tmr2.Enabled = true;
                                        tmr2_watch.Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key1 == 1) hold_clear(1);
                            else if (tmr2_r != 0) timer_unload(2);
                            tmr2_r = 0;
                        }
                    }

                    if (tmr3_f == 1)
                    {
                        if (key_press(trig3))
                        {
                            if (hold_key2 == 1 && hold[2] == 0)  //18.03.2015
                            {
                                hold[2] = 1;
                                hold_load(3);
                            }
                            else if (tmr3_r == 0 && hold_key2 == 0)
                            {
                                tmr3_r = 1;
                                timer_load(3);
                                if (tmr3_left != 0 && (tmr3_left - delay_wait > 0))
                                    tmr3.Interval = tmr3_left - delay_wait;
                                else
                                    tmr_Elapsed(tmr3, null);
                                if (tmr3 != null)
                                    try { 
                                        tmr3.Enabled = true;
                                        tmr3_watch.Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key2 == 1) hold_clear(2);
                            else if (tmr3_r != 0) timer_unload(3);
                            tmr3_r = 0;
                        }
                    }

                    if (tmr4_f == 1)
                    {
                        if (key_press(trig4))
                        {
                            if (hold_key3 == 1 && hold[3] == 0)  //18.03.2015
                            {
                                hold[3] = 1;
                                hold_load(4);
                            }
                            else if (tmr4_r == 0 && hold_key3 == 0)
                            {
                                tmr4_r = 1;
                                timer_load(4);
                                if (tmr4_left != 0 && (tmr4_left - delay_wait > 0))
                                    tmr4.Interval = tmr4_left - delay_wait;
                                else
                                    tmr_Elapsed(tmr4, null);
                                if (tmr4 != null)
                                    try { 
                                        tmr4.Enabled = true;
                                        tmr4_watch.Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key3 == 1) hold_clear(3);
                            else if (tmr4_r != 0) timer_unload(4);
                            tmr4_r = 0;
                        }
                    }

                    if (tmr5_f == 1)
                    {
                        if (key_press(trig5))
                        {
                            if (hold_key4 == 1 && hold[4] == 0)  //18.03.2015
                            {
                                hold[4] = 1;
                                hold_load(5);
                            }
                            else if (tmr5_r == 0 && hold_key4 == 0)
                            {
                                tmr5_r = 1;
                                timer_load(5);
                                if (tmr5_left != 0 && (tmr5_left - delay_wait > 0))
                                    tmr5.Interval = tmr5_left - delay_wait;
                                else
                                    tmr_Elapsed(tmr5, null);
                                if (tmr5 != null)
                                    try { 
                                        tmr5.Enabled = true;
                                        tmr5_watch.Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key4 == 1) hold_clear(4);
                            else if (tmr5_r != 0) timer_unload(5);
                            tmr5_r = 0;
                        }
                    }

                    if (tmr6_f == 1)
                    {
                        if (key_press(trig6))
                        {
                            if (hold_key5 == 1 && hold[5] == 0)  //18.03.2015
                            {
                                hold[5] = 1;
                                hold_load(6);
                            }
                            else if (tmr6_r == 0 && hold_key5 == 0)
                            {
                                tmr6_r = 1;
                                timer_load(6);
                                if (tmr6_left != 0 && (tmr6_left - delay_wait > 0))
                                    tmr6.Interval = tmr6_left - delay_wait;
                                else
                                    tmr_Elapsed(tmr6, null);
                                if (tmr6 != null)
                                    try { 
                                        tmr6.Enabled = true;
                                        tmr6_watch.Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key5 == 1) hold_clear(5);
                            else if (tmr6_r != 0) timer_unload(6);
                            tmr6_r = 0;
                        }
                    }
                    delay_wait = 0;
                }
            }
            else
            {
                if (tmr1_r != 0) timer_unload(1);
                if (tmr2_r != 0) timer_unload(2);
                if (tmr3_r != 0) timer_unload(3);
                if (tmr4_r != 0) timer_unload(4);
                if (tmr5_r != 0) timer_unload(5);
                if (tmr6_r != 0) timer_unload(6);
                delay_wait = 0;
                tmr1_left = 0; tmr2_left = 0; tmr3_left = 0; tmr4_left = 0; tmr5_left = 0; tmr6_left = 0;
                return_press = 0; r_press = 0; t_press = 0; map_press = 0; tmr_all.Interval = 1; delay_press = 0;
                tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                if (key_watch != null) key_watch.Stop();
                if (tmr_all_counter>0) 
                {
                    tmr_all_counter--;
                    hold_clear(88);                
                }                
            }
        }

        public void hold_clear(int i)
        {
            holded = false;
            if (i < 6)
            {
                if (hold[i] > 0)
                {
                    keyup(i + 1); //20.03.2015
                    hold_unload(i + 1);
                    hold[i] = 0;
                }
            }
            else
            {
                //for (int j = 0; j < 6; j++)
                //{
                //    if (hold[j] > 0) 
                //        keyup(j + 1);
                //}
                for (int j = 0; j < 6; j++)
                {
                    if (hold[j] > 0)
                    {
                        hold_unload(j + 1);
                        keyup(j + 1);
                        hold[j] = 0;
                    }
                }
            }
        }

        //public enum WMessages : int //Убрал 19.03.2015
        //{
        //    WM_LBUTTONDOWN = 0x201,
        //    WM_LBUTTONUP = 0x202
        //}

        /// <summary>
        /// Метод для проверки области использования.
        /// </summary>
        /// <returns></returns>
        public bool usage_area()
        {
            bool proc_right = false;
            bool result = false;

            //Проверка окна на наличе слова "Diablo", если область действия соответствующая.
            string title = "";
            if (GetActiveWindowTitle(IntPtr.Zero) != null) title = GetActiveWindowTitle(IntPtr.Zero); //06.04.2015

            //11.03.2015
            int PID;
            if (d3proc)
            {
                GetWindowThreadProcessId(GetForegroundWindow(), out PID);
                Process proc = Process.GetProcessById(PID);
                //MessageBox.Show(proc.ProcessName);
                if (proc_curr.Contains(proc.Id.ToString())) proc_right = true;
            }

            if (
                (!d3prog || (d3prog && title.ToLower().Contains(diablo_name)))
                &&
                (!d3proc || (d3proc && proc_right))
                &&
                (title != null && !title.ToLower().Contains("hotkeys"))
               ) result = true;


            return result;

        }

        /// <summary>
        /// Create the lParam for PostMessage
        /// </summary>
        /// <param name="a">HiWord</param>
        /// <param name="b">LoWord</param>
        /// <returns>Returns the long value</returns>
        private static uint MakeLong(int a, int b)
        {
            return (uint)((uint)((ushort)(a)) | ((uint)((ushort)(b) << 16)));
        }
        

        public void tmr_Elapsed(object sender, EventArgs e)
        {
            var tmr = (System.Timers.Timer)sender;
            int mult = 1, ret = 0; 
            int key_for_hold = timer_key(tmr);
            VirtualKeyCode key = VirtualKeyCode.VK_0;
            if (nud_rand.Value > 0) rand = new Random();
            ret = _MapVirtualKey(key_for_hold, 0);

            if (multikeys != 0) mult = 3;

            if (tmr == tmr1 && key_press(trig1))
            {
                key = key1_v;// virt_code(key1);
                rand_interval(1);
                tmr1_watch.Reset();
                tmr1_watch.Start();
            }
            else if (tmr == tmr2 && key_press(trig2))
            {
                key = key2_v; //virt_code(key2);
                rand_interval(2);
                tmr2_watch.Reset();
                tmr2_watch.Start();
            }
            else if (tmr == tmr3 && key_press(trig3))
            {
                key = key3_v; //virt_code(key3);
                rand_interval(3);
                tmr3_watch.Reset();
                tmr3_watch.Start();
            }
            else if (tmr == tmr4 && key_press(trig4))
            {
                key = key4_v; //virt_code(key4);
                rand_interval(4);
                tmr4_watch.Reset();
                tmr4_watch.Start();
            }
            else if (tmr == tmr5 && key_press(trig5))
            {
                key = key5_v; //virt_code(key5);
                rand_interval(5);
                tmr5_watch.Reset();
                tmr5_watch.Start();
            }
            else if (tmr == tmr6 && key_press(trig6))
            {
                key = key6_v; //virt_code(key6);
                rand_interval(6);
                tmr6_watch.Reset();
                tmr6_watch.Start();
            }

            for (int i = 0; i < mult; i++)
            {
                //MessageBox.Show("Время задержки: "+tmr1_i.ToString());
                if ((int)handle > 0)
                {
                    if ((key_for_hold == (int)Keys.LButton) || (key_for_hold == (int)Keys.RButton))
                    {

                        //if (usage_area())
                        //{
                        //    if (key_for_hold == (int)Keys.LButton)
                        //        inp.Mouse.LeftButtonDown(); //Mouse.PressButton(Mouse.MouseKeys.Left);

                        //    if (key_for_hold == (int)Keys.RButton)
                        //        Mouse.PressButton(Mouse.MouseKeys.Right);
                        //}

                        Point defPnt = new Point();
                        GetCursorPos(ref defPnt);

                        PostMessage(handle,
                                   updown_keys(key_for_hold),
                                   key_for_hold, (int)MakeLong(defPnt.X, defPnt.Y)); //(int)
                        PostMessage(handle,
                                   updown_keys(key_for_hold) + 1,
                                   0, (int)MakeLong(defPnt.X, defPnt.Y)); //(int)
                    }
                    //else if (key_for_hold == (int)Keys.XButton1)
                    //{
                    //    ret = _MapVirtualKey((int)Keys.Shift, 0);
                    //    Point defPnt = new Point();
                    //    GetCursorPos(ref defPnt);

                    //    //_PostMessage(handle, 0x104, (int)Keys.ShiftKey, 0x002A0001);
                    //    //_PostMessage(handle, 0x104, (int)Keys.ShiftKey, 0x402A0001);
                    //    PostMessage(handle, updown_keys(10), (int)Keys.Shift, ret | 0x00000001);
                    //    System.Threading.Thread.Sleep(1);
                    //    PostMessage(handle,
                    //               updown_keys(1),
                    //               1, (int)MakeLong(defPnt.X, defPnt.Y)); //(int)
                    //    PostMessage(handle,
                    //               updown_keys(1) + 1,
                    //               0, (int)MakeLong(defPnt.X, defPnt.Y)); //(int)
                    //    PostMessage(handle, updown_keys(10) + 1, (int)Keys.Shift, ret | (int)(MakeLong(1, ret) + 0xC0000000));
                    //    //_PostMessage(handle, 0x105, (int)Keys.ShiftKey, 0xC02A0001);
                    //    //PostMessage(handle, updown_keys(key_for_hold) + 1, (int)Keys.Shift, (int)(MakeLong(1, ret) + 0xC0000000));
                    //}
                    //else if (key_for_hold == (int)Keys.XButton2)
                    //{
                    //    ret = _MapVirtualKey((int)Keys.Shift, 0);
                    //    Point defPnt = new Point();
                    //    GetCursorPos(ref defPnt);

                    //    PostMessage(handle, 0x100, 0x10, 0x002A0001);
                    //    PostMessage(handle, 0x100, 0x10, 0x402A0001);
                    //    System.Threading.Thread.Sleep(1);
                    //    PostMessage(handle,
                    //               updown_keys(2),
                    //               2, (int)MakeLong(defPnt.X, defPnt.Y));
                    //    System.Threading.Thread.Sleep(1);
                    //    PostMessage(handle, 0x101, 0x10, (int)(MakeLong(1, ret) + 0xC0000000));
                    //    PostMessage(handle,
                    //               updown_keys(2) + 1,
                    //               0, (int)MakeLong(defPnt.X, defPnt.Y));

                    //}
                    else
                    {
                        PostMessage(handle,//hWindow,
                                   updown_keys(key_for_hold),//(int)WM_KEYDOWN,
                                   key_for_hold, (int)(MakeLong(1, ret)));
                        System.Threading.Thread.Sleep(1);
                        PostMessage(handle,//hWindow,
                                    updown_keys(key_for_hold) + 1,//(int)WM_KEYUP,
                                    key_for_hold, (int)(MakeLong(1, ret) + 0xC0000000));
                    }
                }

                else

                    //Если всё в порядке, нажимаем соответствующую клавишу.
                    if (usage_area())
                    {
                        switch (key)
                        {
                            case VirtualKeyCode.LBUTTON: inp.Mouse.LeftButtonClick(); break;
                            case VirtualKeyCode.RBUTTON: inp.Mouse.RightButtonClick(); break;
                            case VirtualKeyCode.XBUTTON1: shift_click(1); break;
                            case VirtualKeyCode.XBUTTON2: shift_click(2); break;
                            case VirtualKeyCode.VK_0: break;
                            default: inp.Keyboard.KeyPress(key); break;
                        }
                        
                    }
            }
            
        }

        /// <summary>
        /// Метод нажатия Shift+Click
        /// </summary>
        /// <param name="i"></param>
        private void shift_click(int i)
        {
            shift_press = 1;
            inp.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
            inp.Keyboard.Sleep(1);
            if (i==1) inp.Mouse.LeftButtonClick(); 
            else inp.Mouse.RightButtonClick();
            inp.Keyboard.Sleep(1);
            inp.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
            shift_press = 0;
        }

        /// <summary>
        /// Метод нахождения кодов клавиш из списка
        /// </summary>
        /// <param name="i"></param>
        private void key_codes(int i)
        {
            Keys key_from_string = Keys.F12;
            VirtualKeyCode vkc = 0;
            int key_hold = 0;
            //char item = new char();
            string st = "";
            switch (i)
            {
                case 1:
                    st = (string)cb_key1.Items[cb_key1.SelectedIndex];
                    break;
                case 2:
                    st = (string)cb_key2.Items[cb_key2.SelectedIndex];
                    break;
                case 3:
                    st = (string)cb_key3.Items[cb_key3.SelectedIndex];
                    break;
                case 4:
                    st = (string)cb_key4.Items[cb_key4.SelectedIndex];
                    break;
                case 5:
                    st = (string)cb_key5.Items[cb_key5.SelectedIndex];
                    break;
                case 6:
                    st = (string)cb_key6.Items[cb_key6.SelectedIndex];
                    break;
            }

            bool done = false, error = false;

            switch (st)
            {
                case "Space":
                    done = true;
                    vkc = VirtualKeyCode.SPACE;
                    key_hold = (int)Keys.Space;
                    break;
                case "LMouse":
                    vkc = VirtualKeyCode.LBUTTON;
                    key_hold = (int)Keys.LButton;
                    done = true;
                    break;
                case "RMouse":
                    vkc = VirtualKeyCode.RBUTTON;
                    key_hold = (int)Keys.RButton;
                    done = true;
                    break;
                case "Shift+LM":
                    vkc = VirtualKeyCode.XBUTTON1;
                    key_hold = (int)Keys.XButton1; //не используется
                    done = true;
                    break;
                case "Shift+RM":
                    vkc = VirtualKeyCode.XBUTTON2;
                    key_hold = (int)Keys.XButton2; //не используется
                    done = true;
                    break;
                default:
                    try
                    {
                        if (st.Length > 0 && st.Substring(0, 1) == "*") st = st.Remove(0, 1);
                        key_from_string = (Keys)(new KeysConverter()).ConvertFromString(st);
                    }
                    catch
                    {
                        error = true;
                    }
                    break;
            }

            if (error)
            {
                switch (i)
                {
                    case 1: cb_key1.SelectedIndex = 0; break;
                    case 2: cb_key2.SelectedIndex = 0; break;
                    case 3: cb_key3.SelectedIndex = 0; break;
                    case 4: cb_key4.SelectedIndex = 0; break;
                    case 5: cb_key5.SelectedIndex = 0; break;
                    case 6: cb_key6.SelectedIndex = 0; break;
                }
                cb_start.Checked = false;
                if (this.WindowState == FormWindowState.Minimized) ShowMe();
            }
            else
            {
                //bool res = false;
                //if (st.Length > 0) res = Char.TryParse(st, out item);
                //if (res)
                //{
                if (!done)
                {
                    vkc = (VirtualKeyCode)key_from_string;
                    key_hold = (int)key_from_string;
                }

                switch (i)
                {
                    case 1:
                        key1_v = vkc;
                        key1_h = key_hold;
                        break;
                    case 2:
                        key2_v = vkc;
                        key2_h = key_hold;
                        break;
                    case 3:
                        key3_v = vkc;
                        key3_h = key_hold;
                        break;
                    case 4:
                        key4_v = vkc;
                        key4_h = key_hold;
                        break;
                    case 5:
                        key5_v = vkc;
                        key5_h = key_hold;
                        break;
                    case 6:
                        key6_v = vkc;
                        key6_h = key_hold;
                        break;
                }
            }

            //}

            //            char item = new char();
            //string st = "1";
            //bool res = false ;
            //if (st.Length > 0) res = Char.TryParse(st, out item);
            //if (res)
            //{
            //    Key wikey = KeyInterop.KeyFromVirtualKey((int)item);
            //    VirtualKeyCode vkc = (VirtualKeyCode)KeyInterop.VirtualKeyFromKey(wikey);
            //    VirtualKeyCode vkc1 = (VirtualKeyCode)item

            //    MessageBox.Show(item.ToString() + " " + ((int)item).ToString());
            //    MessageBox.Show("Keys D1: " + key_hold_code(1).ToString() + " Virt: " + (VirtualKeyCode.VK_1).ToString() + " Vks: " + vkc1.ToString());
                 
            //    //Key.
            //    //KeyInterop.VirtualKeyFromKey
            //    //KeysConverter kc = new KeysConverter();
            //    //kc.ConvertTo(Keys.D1, Type.);

            //}

        }

        /// <summary>
        /// Метод при запуске программы или её остановке (Start/Stop/F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_start_CheckedChanged(object sender, EventArgs e)
        {
            lb_lang_name.Focus();
            //timer_unload(99);
            if (key_watch != null) key_watch.Stop();
            tmr1_left = 0; tmr2_left = 0; tmr3_left = 0; tmr4_left = 0; tmr5_left = 0; tmr6_left = 0;
            tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0; tmr5_f = 0; tmr6_f = 0;
            tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
            tmr1_i = 0; tmr2_i = 0; tmr3_i = 0; tmr4_i = 0; tmr5_i = 0; tmr6_i = 0;
            trig1 = 0; trig2 = 0; trig3 = 0; trig4 = 0; trig5 = 0; trig6 = 0; delay_wait = 0; shift_press = 0;
            hold_key0 = 0; hold_key1 = 0; hold_key2 = 0; hold_key3 = 0; hold_key4 = 0; hold_key5 = 0; //17.03.2015
            t_press = 0; map_press = 0; return_press = 0; r_press = 0; delay_press = 0; return_press_count = 0;

            if (cb_start.Text != "Stop")
            {
                if (pan_opt.Visible || opt_click == 1) b_opt_Click(null, null);

                bool proc_exist = false;

                if (lb_debug.Visible) //For Debugging //27.04.2015
                {
                    proc_exist = true; 
                    diablo_name = "opera";
                    handle = FindWindow(null, "akelpad");
                    handle = FindWindowEx(handle, IntPtr.Zero, "AkelEditW", null);
                }

                if (chb_hold.Checked && !proc_selected && handle != IntPtr.Zero)
                {
                    Process[] processlist = Process.GetProcesses();
                    try
                                {
                                    foreach (Process p in processlist)
                                    {
                                        if (p.PagedMemorySize64 > proc_size) 
                                        {
                                            if (handle == p.MainWindowHandle) proc_exist = true;
                                        }
                                    }
                                }
                    catch { }
                    if (!proc_exist)
                    {
                        cb_proc.SelectedIndex = -1;
                    }
                }

                error_not(2); //check_only(); 17.04.2015
                error_select();

                if (cb_key1.SelectedIndex > 18 || cb_key2.SelectedIndex > 18 || cb_key3.SelectedIndex > 18
                    || cb_key4.SelectedIndex > 18 || cb_key5.SelectedIndex > 18 || cb_key6.SelectedIndex > 18)
                    foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                        if (cb.Name.Contains("trig") && cb.SelectedIndex == 1) cb.SelectedIndex = 0;

                if (!start_main || !start_opt || !cb_start.Enabled)
                {
                    cb_start.Checked = false;
                    if (this.WindowState == FormWindowState.Minimized) ShowMe();
                }
            }

            if (cb_start.Checked)
            {

                key1_h = 0; key2_h = 0; key3_h = 0; key4_h = 0; key5_h = 0; key6_h = 0; //24.04.2015
                key1_v = 0; key2_v = 0; key3_v = 0; key4_v = 0; key5_v = 0; key6_v = 0; //24.04.2015
                d3hot_Activated(null, null); //27.04.2015

                //if (d3proc) proc_watch = new Stopwatch(); //06.04.2015

                mouseKeyEventProvider1.Enabled = true;
                multikeys = Settings.Default.chb_mpress;
                hold = new int[] { 0, 0, 0, 0, 0, 0 }; //17.03.2015

                if (nud_key_delay_ms.Value>0) delay_press_interval = Convert.ToInt32(nud_key_delay_ms.Value);

                if (chb_key1.Checked) hold_key0 = 1; //17.03.2015
                if (chb_key2.Checked) hold_key1 = 1;
                if (chb_key3.Checked) hold_key2 = 1;
                if (chb_key4.Checked) hold_key3 = 1;
                if (chb_key5.Checked) hold_key4 = 1;
                if (chb_key6.Checked) hold_key5 = 1;

                pause = cb_pause.SelectedIndex;
                cb_start.Text = "Stop";
                tt_start.SetToolTip(cb_start, lng.tt_stop);

                //Блокирование элементов настройки, пока программа работает
                tb_prof_name.Enabled = false;
                foreach (NumericUpDown numud in this.pan_main.Controls.OfType<NumericUpDown>()) numud.Enabled = false;
                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>()) cb.Enabled = false;
                foreach (ComboBox cbm in this.Controls.OfType<ComboBox>()) cbm.Enabled = false;
                cb_prog.Enabled = false; cb_proc.Enabled = false;
                if (chb_hold.Checked) 
                    foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>()) chb.Enabled = false;
                
                b_opt.Enabled = false;

                if (nud_tmr1.Value > 0 && cb_trig_tmr1.SelectedIndex > 0 && cb_key1.SelectedIndex > 0)
                {
                    lb_tmr1_sec.Text = Math.Round((nud_tmr1.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr1_i = Convert.ToDouble(nud_tmr1.Value);
                    trig1 = cb_trig_tmr1.SelectedIndex;
                    //key1 = cb_key1.SelectedIndex;
                    key_codes(1);
                    tmr1_f = 1;
                }
                if (nud_tmr2.Value > 0 && cb_trig_tmr2.SelectedIndex > 0 && cb_key2.SelectedIndex > 0)
                {
                    lb_tmr2_sec.Text = Math.Round((nud_tmr2.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr2_i = Convert.ToDouble(nud_tmr2.Value);
                    trig2 = cb_trig_tmr2.SelectedIndex;
                    //key2 = cb_key2.SelectedIndex;
                    key_codes(2);
                    tmr2_f = 1;
                }

                if (nud_tmr3.Value > 0 && cb_trig_tmr3.SelectedIndex > 0 && cb_key3.SelectedIndex > 0)
                {
                    lb_tmr3_sec.Text = Math.Round((nud_tmr3.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr3_i = Convert.ToDouble(nud_tmr3.Value);
                    trig3 = cb_trig_tmr3.SelectedIndex;
                    //key3 = cb_key3.SelectedIndex;
                    key_codes(3);
                    tmr3_f = 1;
                }

                if (nud_tmr4.Value > 0 && cb_trig_tmr4.SelectedIndex > 0 && cb_key4.SelectedIndex > 0)
                {
                    lb_tmr4_sec.Text = Math.Round((nud_tmr4.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr4_i = Convert.ToDouble(nud_tmr4.Value);
                    trig4 = cb_trig_tmr4.SelectedIndex;
                    //key4 = cb_key4.SelectedIndex;
                    key_codes(4);
                    tmr4_f = 1;
                }
                if (nud_tmr5.Value > 0 && cb_trig_tmr5.SelectedIndex > 0 && cb_key5.SelectedIndex > 0)
                {
                    lb_tmr5_sec.Text = Math.Round((nud_tmr5.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr5_i = Convert.ToDouble(nud_tmr5.Value);
                    trig5 = cb_trig_tmr5.SelectedIndex;
                    //key5 = cb_key5.SelectedIndex;
                    key_codes(5);
                    tmr5_f = 1;
                }
                if (nud_tmr6.Value > 0 && cb_trig_tmr6.SelectedIndex > 0 && cb_key6.SelectedIndex > 0)
                {
                    lb_tmr6_sec.Text = Math.Round((nud_tmr6.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr6_i = Convert.ToDouble(nud_tmr6.Value);
                    trig6 = cb_trig_tmr6.SelectedIndex;
                    //key6 = cb_key6.SelectedIndex;
                    key_codes(6);
                    tmr6_f = 1;
                }

                if (tmr1_f == 1 || tmr2_f == 1 || tmr3_f == 1 || tmr4_f == 1 || tmr5_f == 1 || tmr6_f == 1)
                {
                    timer_load(0);
                    tmr_all_counter = 0;
                    if (!tmr_all.Enabled) tmr_all.Enabled = true;
                }
            }
            else
            {
                //if (d3proc && proc_watch != null) //06.04.2015
                //{
                //    proc_watch.Reset();
                //    proc_watch.Stop();
                //}

                mouseKeyEventProvider1.Enabled = false;
                if (!chb_key1.Checked || !chb_hold.Checked) nud_tmr1.Enabled = true;
                if (!chb_key2.Checked || !chb_hold.Checked) nud_tmr2.Enabled = true;
                if (!chb_key3.Checked || !chb_hold.Checked) nud_tmr3.Enabled = true;
                if (!chb_key4.Checked || !chb_hold.Checked) nud_tmr4.Enabled = true;
                if (!chb_key5.Checked || !chb_hold.Checked) nud_tmr5.Enabled = true;
                if (!chb_key6.Checked || !chb_hold.Checked) nud_tmr6.Enabled = true;

                //foreach (NumericUpDown numud in this.pan_main.Controls.OfType<NumericUpDown>()) numud.Enabled = true;
                tb_prof_name.Enabled = true;
                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>()) cb.Enabled = true;
                foreach (ComboBox cbm in this.Controls.OfType<ComboBox>()) cbm.Enabled = true;
                cb_prog.Enabled = true; cb_proc.Enabled = true;
                if (chb_hold.Checked)
                    foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>())
                    {
                        chb.Enabled = true;
                        //if (chb.Checked) cb_prog.Enabled = false; //17.04.2015
                    }
                //if (cb_proc.SelectedIndex > 0) cb_prog.Enabled = false; //17.04.2015
   
                b_opt.Enabled = true;

                proc_selected = false; //Отменяем выбор процесса после остановки

                cb_start.Text = "Start";
                tt_start.SetToolTip(cb_start, lng.tt_start);

                //timer_unload(99);
                if (tmr_all != null) timer_unload(0);
                if (tmr1 != null) timer_unload(1);
                if (tmr2 != null) timer_unload(2);
                if (tmr3 != null) timer_unload(3);
                if (tmr4 != null) timer_unload(4);
                if (tmr5 != null) timer_unload(5);
                if (tmr6 != null) timer_unload(6);

                hold_clear(88);

                //tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0; tmr5_f = 0; tmr6_f = 0;
                //tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                //tmr1_i = 0; tmr2_i = 0; tmr3_i = 0; tmr4_i = 0; tmr5_i = 0; tmr6_i = 0;

                //Lang();
            }

        }

        private void key_assign(int i, ComboBox curr)
        {
            char item = Char.Parse(((string)curr.Items[curr.SelectedIndex]).Substring(0,1));
            VirtualKeyCode vk = VirtualKeyCode.F12;
            string test="";
            int key_hold = 0;

            

            switch (test)
            {
                case "1":
                    vk = VirtualKeyCode.VK_1;
                    key_hold = (int)Keys.D1;
                    break;
                case "2":
                    vk = VirtualKeyCode.VK_2;
                    key_hold = (int)Keys.D2;
                    break;
                case "3":
                    vk = VirtualKeyCode.VK_3;
                    key_hold = (int)Keys.D3;
                    break;
                case "4":
                    vk = VirtualKeyCode.VK_4;
                    key_hold = (int)Keys.D4;
                    break;

                case "Q":
                    vk = VirtualKeyCode.VK_1;
                    key_hold = (int)Keys.D1;
                    break;
                case "W":
                    vk = VirtualKeyCode.VK_2;
                    key_hold = (int)Keys.D2;
                    break;
                case "E":
                    vk = VirtualKeyCode.VK_3;
                    key_hold = (int)Keys.D3;
                    break;
                case "R":
                    vk = VirtualKeyCode.VK_4;
                    key_hold = (int)Keys.D4;
                    break;
            }

            switch (i)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
        }

        /// <summary>
        /// Закрытие формы, удаление горячей клавиши, сохранение текущих настроек в профиль.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void d3hot_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save_settings(1);
            for (int i = 0; i < 4; i++)
            {
                UnregisterHotKey(this.Handle, i);       // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.   
            }
        }

        /// <summary>
        /// Три метода для обработки пункта "Автор".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_auth_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void lb_auth_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void lb_auth_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://glasscannon.ru/forum/viewtopic.php?f=5&t=1988");
            //if (bt_lang.Text == "ENG") System.Diagnostics.Process.Start("http://horadric.ru/forum/viewtopic.php?f=16&t=26771");
            //else System.Diagnostics.Process.Start("http://www.diablofans.com/forums/diablo-iii-general-forums/diablo-iii-general-discussion/89743-offline-dps-calculator-diablo-3");
        }

        /// <summary>
        /// Метод выбора языка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_lang_Click(object sender, EventArgs e)
        {
            if (lb_lang.Text == "Eng")
            {
                lb_lang.Text = "Rus";
                lng.Lang_eng();
            }
            else
            {
                lb_lang.Text = "Eng";
                lng.Lang_rus();
            }
            Lang();
            key_menu();
            //chb_hold_CheckedChanged(null, null);
        }

        /// <summary>
        /// Метод обработки языка
        /// </summary>
        public void Lang()
        {
            lb_trig1.Text = lng.lb_trig1;
            lb_trig2.Text = lng.lb_trig2;
            lb_trig3.Text = lng.lb_trig3;
            lb_trig4.Text = lng.lb_trig4;
            lb_trig5.Text = lng.lb_trig5;
            lb_trig6.Text = lng.lb_trig6;

            lb_key1.Text = lng.lb_key1;
            lb_key2.Text = lng.lb_key2;
            lb_key3.Text = lng.lb_key3;
            lb_key4.Text = lng.lb_key4;
            lb_key5.Text = lng.lb_key5;
            lb_key6.Text = lng.lb_key6;

            lb_tmr1_sec.Text = lng.lb_tmr_sec;
            lb_tmr2_sec.Text = lng.lb_tmr_sec;
            lb_tmr3_sec.Text = lng.lb_tmr_sec;
            lb_tmr4_sec.Text = lng.lb_tmr_sec;
            lb_tmr5_sec.Text = lng.lb_tmr_sec;
            lb_tmr6_sec.Text = lng.lb_tmr_sec;
            lb_key_delay_desc.Text = lng.lb_tmr_sec;
            lb_nud_rand.Text = lng.lb_tmr_sec;
            lb_rand.Text = lng.lb_rand;
            lb_hot_prof.Text = lng.lb_hot_prof;

            //lb_about.Text = lng.lb_about;
            lb_area.Text = lng.lb_area;
            lb_proc.Text = lng.lb_proc;
            lb_stop.Text = lng.lb_stop;
            lb_auth.Text = lng.lb_auth;
            if (!chb_saveload.Checked) lb_prof.Text = lng.lb_prof;
            else lb_prof.Text = lng.lb_prof_save;
            lb_tp.Text = lng.lb_tp;
            lb_tpdelay.Text = lng.lb_tpdelay;
            lb_startstop.Text = lng.lb_startstop;
            lb_map.Text = lng.lb_map;
            lb_mapdelay.Text = lng.lb_mapdelay;

            chb_tray.Text = lng.chb_tray;
            chb_mult.Text = lng.chb_mult;
            chb_users.Text = lng.chb_users;
            //chb_proconly.Text = lng.chb_proconly; 17.04.2015
            b_opt.Text = lng.b_opt;
            lb_key_delay_ms.Text = lng.lb_key_delay_ms;
            lb_key_delay.Text = lng.lb_key_delay;

            tt_start.SetToolTip(cb_start, lng.tt_start);
            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains ("key")) tt_key.SetToolTip(cb, lng.tt_key);
                else tt_key.SetToolTip(cb, lng.tt_trig);
            }
            foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>()) tt_key.SetToolTip(chb, lng.tt_key);
            foreach (NumericUpDown nud in this.pan_main.Controls.OfType<NumericUpDown>()) tt_key.SetToolTip(nud, lng.tt_delay);

            lb_hold.Text = lng.lb_hold;
            chb_hold.Text = lng.chb_hold;
            chb_mpress.Text = lng.chb_mpress;

            if (pan_opt.Visible) 
                error_not(1);
            else
                error_not(2);

            if (tb_prof_name.Text == "" || tb_prof_name.Text == "Наименование профиля" || tb_prof_name.Text == "Profile name") 
                tb_prof_name.Text = lng.tb_prof_name;

            if (lb_key_desc != null) lb_key_desc.Text = lng.lb_key_desc;
        }

        /// <summary>
        /// Метод для выбора профилей, сохранения и чтения настроек из них.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_prof_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Save_settings(0);
            int k = cb_hot_prof.SelectedIndex;

            string path = "";
            switch (cb_prof.SelectedIndex)
            {
                case 1: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof1.xml"); break;
                case 2: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof2.xml"); break;
                case 3: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof3.xml"); break;
            }

            if (path != "" && File.Exists(path)) ReadXML(path);
            else if (path != "") Save_settings(1); //08.04.2015

            //cb_hot_prof.SelectedIndex = k;

            cb_proc.SelectedIndex = -1; 
            
            //if (!chb_proconly.Checked && cb_proc.SelectedIndex < 1) cb_prog.Enabled = true; //17.04.2015
            //cb_prog_SelectionChangeCommitted(null, null); //17.04.2015
            chb_hold_CheckedChanged(null, null);

            if (chb_hold.Checked && cb_proc.SelectedIndex < 1
                //&& (chb_key1.Checked || chb_key2.Checked || chb_key3.Checked || chb_key4.Checked || chb_key5.Checked || chb_key6.Checked) //17.04.2015
                )
                cb_proc_Click(null, null);
            //    && !proc_auto()) 
            //{
            //    cb_proc.SelectedIndex = -1;
            //    cb_proc_SelectionChangeCommitted(null, null);
            //}
        }

        private void tsmi_save_Click(object sender, EventArgs e)
        {
            lb_lang_name.Focus();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Xml Files|*.xml";
            saveFileDialog1.Title = "Select a xml File to save";
            saveFileDialog1.FileName = tb_prof_name.Text;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(openFileDialog1.FileName.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
                path = saveFileDialog1.FileName.ToString();
                Save_settings(0);
                //WriteXML(saveFileDialog1.FileName.ToString());
            }
        }

        private void tsmi_load_Click(object sender, EventArgs e)
        {
            lb_lang_name.Focus();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Xml Files|*.xml";
            openFileDialog1.Title = "Select a xml File to load";
            //openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReadXML(openFileDialog1.FileName.ToString());
                cb_prog_SelectionChangeCommitted(null, null);
                if (cb_proc != null) cb_proc.SelectedIndex = -1;
                cb_proc_SelectionChangeCommitted(null, null);
            }
        }

        /// <summary>
        /// Метод установки глобального хоткея запуска/остановки
        /// </summary>
        public void reghotkey()
        {
            int id = 0;     // The id of the hotkey. 
            if (cb_startstop.SelectedIndex > 0)
            {
                switch (cb_startstop.SelectedIndex)
                {
                    case 1: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F1.GetHashCode()); break;//Глобальный хоткей запуска/остановки F1 
                    case 2: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F2.GetHashCode()); break;//Глобальный хоткей запуска/остановки F2 
                    case 3: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F3.GetHashCode()); break;//Глобальный хоткей запуска/остановки F3 
                    case 4: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F4.GetHashCode()); break;//Глобальный хоткей запуска/остановки F4 
                    case 5: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F5.GetHashCode()); break;//Глобальный хоткей запуска/остановки F5 
                    case 6: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F6.GetHashCode()); break;//Глобальный хоткей запуска/остановки F6 
                    case 7: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F7.GetHashCode()); break;//Глобальный хоткей запуска/остановки F7 
                    case 8: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F8.GetHashCode()); break;//Глобальный хоткей запуска/остановки F8 
                    case 9: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F9.GetHashCode()); break;//Глобальный хоткей запуска/остановки F9 
                    case 10: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F10.GetHashCode()); break;//Глобальный хоткей запуска/остановки F10
                    case 11: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F11.GetHashCode()); break;//Глобальный хоткей запуска/остановки F11 
                }
            }
        }

        /// <summary>
        /// Метод регистрации хоткея запуска/остановки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_startstop_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnregisterHotKey(this.Handle, 0);
            reghotkey();
        }

        /// <summary>
        /// Метод выбора кнопки для хоткея приостановки (телепорта)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_tp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_tp.SelectedIndex > 0 && (string)cb_tp.Items[cb_tp.SelectedIndex] != "") //24.04.2015
                tp_key = (string)cb_tp.Items[cb_tp.SelectedIndex].ToString();
        }

        /// <summary>
        /// Метод установки задержки приостановки (при телепорте)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_tpdelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_tpdelay.SelectedIndex > -1) tp_delay = Convert.ToInt32(cb_tpdelay.Items[cb_tpdelay.SelectedIndex]);
        }

        /// <summary>
        /// Метод установки режима доступа к настройкам задержки, если она возможна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_pause_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_pause.SelectedIndex)
            {
                case 1:
                    cb_tp.Enabled=true;
                    cb_tpdelay.Enabled = true;
                    cb_map.Enabled = false;
                    cb_mapdelay.Enabled = false;
                    break;
                case 3:
                    cb_tp.Enabled=true;
                    cb_tpdelay.Enabled = true;
                    cb_map.Enabled=true;
                    cb_mapdelay.Enabled = true;
                    break;
                default:
                    cb_tp.Enabled=false;
                    cb_tpdelay.Enabled = false;
                    cb_map.Enabled = false;
                    cb_mapdelay.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Метод выбора процессов в памяти >400Мб
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_proc_Click(object sender, EventArgs e)
        {
            proc_handle = new Dictionary<string, IntPtr>();
            cb_proc.Items.Clear();
            Process[] processlist = Process.GetProcesses();
            handle = IntPtr.Zero;

            //cb_proc.SelectedIndex = -1;
            //cb_proc_SelectionChangeCommitted(null, null);

            //if (!chb_proconly.Checked && cb_proc.SelectedIndex < 1) cb_prog.Enabled = true; //17.04.2015

            cb_proc.Items.Add("");
            try
            {
                foreach (Process p in processlist)
                {
                    if (p.PagedMemorySize64 > proc_size) //cb_proc.Items.Add(p.ProcessName + " " + p.Id.ToString());
                    //if (p.ProcessName.ToLower().Contains("notepad"))
                    {
                        cb_proc.Items.Add(p.ProcessName + " " + p.Id.ToString());
                        proc_handle.Add(p.ProcessName + " " + p.Id.ToString(), p.MainWindowHandle);
                        //cb_proc.Items.Add("блокнот" + " " + p.Id.ToString());
                        //proc_handle.Add("блокнот" + " " + p.Id.ToString(), p.MainWindowHandle);
                    }
                }
            }
            catch { }

            //bool result = false;
            for (int i = 0; i < cb_proc.Items.Count; i++)
            {
                if (cb_proc.Items[i].ToString().ToLower().Contains(diablo_name))
                {
                    cb_proc.SelectedIndex = i;
                    proc_selected = true;
                }
            }
            if (!proc_selected) cb_proc.SelectedIndex = -1;
            cb_proc_SelectionChangeCommitted(null, null);
        }

        /// <summary>
        /// Метод установки режима доступа к области действия в зависимости от установки меню процесса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_proc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            proc_curr = "";
            if (cb_proc.SelectedIndex > -1) proc_curr = (string)cb_proc.Items[cb_proc.SelectedIndex].ToString();
            if (proc_curr != "") //|| chb_proconly.Checked //17.04.2015
            {
                d3proc = true;
                d3prog = false;
                proc_selected = true;
                cb_prog.SelectedIndex = 0;
                //cb_prog.Enabled = false; //17.04.2015
                if (proc_curr != "") handle = proc_handle[proc_curr];

                //if (lb_debug.Visible)
                //{
                //    //handle = FindWindow(null, "akelpad");
                //    //handle = FindWindowEx(handle, IntPtr.Zero, "AkelEditW", null);  //For debugging
                //}

                //for (int i = 1; i < 7; i++)
                //{
                //    cb_key_del(i);
                //}

                //pan_hold.Visible = false;
            }
            else
            {
                d3proc = false;
                handle = IntPtr.Zero;

                //for (int j = 1; j < 7; j++)
                //{
                //    cb_key_add(j);
                //}

                //int i = 0;
                //if (cb_proc.SelectedIndex < 1) cb_prog.Enabled = true; //17.04.2015
                //foreach (CheckBox chb_check in this.pan_main.Controls.OfType<CheckBox>()) //17.04.2015
                //    if (chb_hold.Checked && chb_check.Checked) cb_prog.Enabled = false; //17.04.2015

                //if (i > 0)
                //    error_not(3);//pan_hold.Visible = true;
                //else
                //    cb_prog.Enabled = true;
            }
            error_not(2);
        }

        /// <summary>
        /// Метод выбора области действия программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_prog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_prog.SelectedIndex == 1)
            {
                d3prog = true;
            }
            if (cb_prog.SelectedIndex == 0)
            {
                d3prog = false;
            }
        }

        /// <summary>
        /// Метод отключения/выключения списка процессов из-за активной/неактивной области действия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_prog_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((cb_prog != null) && (cb_proc != null) && cb_prog.SelectedIndex == 1)
            {
                //cb_proc.Enabled = false;
                //cb_proc.SelectedIndex = -1;
                d3proc = false;
            }
            //else
            //{
            //    cb_proc.Enabled = true;
            //}
        }

        private void d3hot_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && chb_tray.Checked)
            {
                //if (pan_opt.Visible || opt_click == 1) b_opt_Click(null, null);
                //check_only();
                //error_select();

                if (start_main && start_opt)
                {
                    this.Hide();
                    notify_d3h.Visible = true;
                }
                //else
                //    ShowMe();

                //notify_d3h.ContextMenu = new ContextMenu();
                //notify_d3h.ContextMenu.MenuItems.Add("Exit", new EventHandler(Exit));
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) ShowMe();
        }

        //private void notify_d3h_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
        //        mi.Invoke(notify_d3h, null);
        //    }
        //}

        //private bool IsControlAtFront(Control control)
        //{
        //    while (control.Parent != null)
        //    {
        //        if (control.Parent.Controls.GetChildIndex(control) == 0)
        //        {
        //            control = control.Parent;
        //            if (control.Parent == null)
        //            {
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        private void cb_op_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (opt_click == 1) opt_change = 1;
            error_not(1);
        }

        /// <summary>
        /// Метод обработки ошибок отсутствия выбора
        /// </summary>
        private void error_select()
        {
            int[] nulls = new int[] { 0, 0, 0, 0, 0, 0 };
            int[] key_nulls = new int[] { 0, 0, 0, 0, 0, 0 };
            bool notrig = true;

            foreach (NumericUpDown numud in this.pan_main.Controls.OfType<NumericUpDown>())
            {
                if (numud.Text == "") numud.Value = 0;
            }

            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("trig") && cb.SelectedIndex > 0) notrig = false;
            }

            if (cb_trig_tmr1.SelectedIndex > 0 && (nud_tmr1.Value != 0 || chb_key1.Checked)) nulls[0] = 1;
            if (cb_trig_tmr2.SelectedIndex > 0 && (nud_tmr2.Value != 0 || chb_key2.Checked)) nulls[1] = 1;
            if (cb_trig_tmr3.SelectedIndex > 0 && (nud_tmr3.Value != 0 || chb_key3.Checked)) nulls[2] = 1;
            if (cb_trig_tmr4.SelectedIndex > 0 && (nud_tmr4.Value != 0 || chb_key4.Checked)) nulls[3] = 1;
            if (cb_trig_tmr5.SelectedIndex > 0 && (nud_tmr5.Value != 0 || chb_key5.Checked)) nulls[4] = 1;
            if (cb_trig_tmr6.SelectedIndex > 0 && (nud_tmr6.Value != 0 || chb_key6.Checked)) nulls[5] = 1;

            if (nulls[0] == 1 && cb_key1.SelectedIndex > 0) key_nulls[0] = 1;
            if (nulls[1] == 1 && cb_key2.SelectedIndex > 0) key_nulls[1] = 1;
            if (nulls[2] == 1 && cb_key3.SelectedIndex > 0) key_nulls[2] = 1;
            if (nulls[3] == 1 && cb_key4.SelectedIndex > 0) key_nulls[3] = 1;
            if (nulls[4] == 1 && cb_key5.SelectedIndex > 0) key_nulls[4] = 1;
            if (nulls[5] == 1 && cb_key6.SelectedIndex > 0) key_nulls[5] = 1;

            if (!notrig && nulls.Contains(1) && key_nulls.Contains(1) && start_opt)
            {
                error_show(1);
                start_main = true;
            }
            else
            {
                start_main = false;
                if (notrig) lb_hold.Text = lng.lb_hold_trig; //"Не выбрано ни одного триггера для активации."
                else if (!nulls.Contains(1)) lb_hold.Text = lng.lb_hold_delay; //"Не выставленаы паузы для триггеров."
                else if (!key_nulls.Contains(1)) lb_hold.Text = lng.lb_hold_key; //"Не выбраны клавиши для триггеров."              
                else if (!start_opt)
                {
                    error_not(2);
                    start_main = true;
                }
                error_show(2);
            }
        }

        /// <summary>
        /// Метод обработки ошибок при пользовательском выборе.
        /// </summary>
        /// <param name="i">1: хоткеи, 2: процессы</param>
        private void error_not(int i)
        {
            int hot_prof = 0;
            int no_proc = 0;
            int hot_tpmap = 0;

            if (cb_startstop.SelectedIndex > 0 && cb_hot_prof.SelectedIndex > 0 &&
                cb_hot_prof.Items[cb_hot_prof.SelectedIndex].ToString().Contains(cb_startstop.Items[cb_startstop.SelectedIndex].ToString())
                &&
                (
                !(cb_startstop.Items[cb_startstop.SelectedIndex].ToString() == "F1" && (cb_hot_prof.Items[cb_hot_prof.SelectedIndex].ToString().Contains("F11") || cb_hot_prof.Items[cb_hot_prof.SelectedIndex].ToString().Contains("F10")))
                )
                )
                hot_prof = 1;

            if  (cb_tp.SelectedIndex == cb_map.SelectedIndex) hot_tpmap = 1;

            //if (
            //    (cb_proc.SelectedIndex < 1) &&
            //        (chb_proconly.Checked ||
            //            (chb_hold.Checked &&
            //            (chb_key1.Checked || chb_key2.Checked || chb_key3.Checked || chb_key4.Checked || chb_key5.Checked || chb_key6.Checked))
            //        )
            //    ) //17.04.2015
            if (chb_hold.Checked && cb_proc.SelectedIndex < 1)
                no_proc = 1;

            switch (i)
            {
                case 1: //Ошибка пересечения хоткеев
                    if (no_proc == 1) //Не выбран процесс
                    {
                        lb_hold.Text = lng.lb_hold;
                        lb_proc.Font = new Font(lb_proc.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else //Процесс выбран
                        lb_proc.Font = new Font(lb_proc.Font, FontStyle.Regular);

                    if (hot_prof == 1) //Хоткеи старта программы/профилей пересекаются
                    {
                        lb_hold.Text = lng.lb_hold_hot;
                        lb_startstop.Font = new Font(lb_startstop.Font, FontStyle.Bold);
                        lb_hot_prof.Font = new Font(lb_hot_prof.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else//Хоткеи старта программы/профилей не пересекаются
                    {
                        lb_startstop.Font = new Font(lb_startstop.Font, FontStyle.Regular);
                        lb_hot_prof.Font = new Font(lb_hot_prof.Font, FontStyle.Regular);
                    }

                    if (hot_tpmap == 1) //Хоткеи карты/телепорта пересекаются
                    {
                        lb_hold.Text = lng.lb_hold_hot;
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Bold);
                        lb_tp.Font = new Font(lb_tp.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else//Хоткеи карты/телепорта не пересекаются
                    {
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Regular);
                        lb_tp.Font = new Font(lb_hot_prof.Font, FontStyle.Regular);
                    }

                    break;
                
                case 2: //Ошибка без выбора процесса
                    if (hot_prof == 1) //Хоткеи старта программы/профилей пересекаются
                    {
                        lb_hold.Text = lng.lb_hold_hot;
                        lb_startstop.Font = new Font(lb_startstop.Font, FontStyle.Bold);
                        lb_hot_prof.Font = new Font(lb_hot_prof.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else//Хоткеи старта программы/профилей не пересекаются
                    {
                        lb_startstop.Font = new Font(lb_startstop.Font, FontStyle.Regular);
                        lb_hot_prof.Font = new Font(lb_hot_prof.Font, FontStyle.Regular);
                    }

                    if (hot_tpmap == 1) //Хоткеи карты/телепорта пересекаются
                    {
                        lb_hold.Text = lng.lb_hold_hot;
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Bold);
                        lb_tp.Font = new Font(lb_tp.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else//Хоткеи карты/телепорта не пересекаются
                    {
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Regular);
                        lb_tp.Font = new Font(lb_hot_prof.Font, FontStyle.Regular);
                    }

                    if (no_proc == 1) //Не выбран процесс
                    {
                        lb_hold.Text = lng.lb_hold;
                        lb_proc.Font = new Font(lb_proc.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else //Процесс выбран
                        lb_proc.Font = new Font(lb_proc.Font, FontStyle.Regular);
                    break;
            }

            if (hot_prof != 1 && no_proc != 1 && hot_tpmap != 1)
            {
                error_show(1);
                start_opt = true;
                if (!start_main) error_select();
            }
            else
            {
                error_show(2);
                start_opt = false;
            }
        }

        private void error_show(int i)
        {
            switch (i)
            {
                case 1:
                    pan_hold.Visible = false;
                    pan_prof_name.Visible = true;
                    cb_start.Enabled = true;
                    break;

                case 2:
                    pan_prof_name.Visible = false;
                    pan_hold.Visible = true;
                    pan_hold.Width = lb_hold.Width + 1;
                    pan_hold.Height = lb_hold.Height + 5;
                    cb_start.Enabled = false;
                    break;
            }
        }

        private void b_opt_Click(object sender, EventArgs e)
        {
            //lb_lang_name.Focus();

            //if (!IsControlAtFront(pan_opt))
            //    pan_opt.BringToFront();
            //else
            //{
            //    pan_opt.SendToBack();
            //    lb_lang_name.Focus();
            //    Save_settings(0);
            //}

            if (!pan_opt.Visible || opt_click==0)
            {
                pan_opt.BringToFront();
                pan_opt.Visible = true;
                opt_click = 1;
            }
            else
            {
                error_not(1);
                if (lb_hold.Text != lng.lb_hold_hot || pan_hold.Visible==false)
                {
                    opt_click = 0;
                    lb_lang_name.Focus();
                    pan_opt.SendToBack();
                    pan_opt.Visible = false;
                    if (opt_change == 1)
                    {
                        Save_settings(0);
                        opt_change = 0;
                    }
                }
            }

        }

        private void cb_key_delay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_key_delay.SelectedIndex > 0 && (string)cb_key_delay.Items[cb_key_delay.SelectedIndex] != "")
            {
                key_delay = (string)cb_key_delay.Items[cb_key_delay.SelectedIndex].ToString();
                if (key_delay == "1" || key_delay == "2" || key_delay == "3" || key_delay == "4") key_delay = "D" + key_delay;
                nud_key_delay_ms.Enabled = true;
            }
            else
            {
                nud_key_delay_ms.Value = 0;
                nud_key_delay_ms.Enabled = false;
                key_delay = "";
            }
        }

        private void check_only()
        {
           int i = 0, j=0;

            foreach (CheckBox cheb in this.pan_main.Controls.OfType<CheckBox>())
            {
                i++;
                if (cheb.Checked) j = i;
            }

            if (j > 0)
            {
                i = 0;
                foreach (CheckBox cheb in this.pan_main.Controls.OfType<CheckBox>())
                {
                    i++;
                    if (j != i)
                    {
                        cheb.Checked = false;
                        cheb.Visible = false;
                    }
                }
                //for (int k = 1; k < 7; k++)
                //{
                //    cb_key_del(k);
                //}
            }
            else
            {
                if (chb_hold.Checked)
                    foreach (CheckBox cheb in this.pan_main.Controls.OfType<CheckBox>())
                        cheb.Visible = true;
               
                //if (cb_proc.SelectedIndex>0)
                //    for (int k = 1; k < 7; k++)
                //    {
                //        cb_key_del(k);
                //    }
            }
            error_not(2);
        }

        /// <summary>
        /// Метод для автоматического выбора процесса Diablo 3
        /// </summary>
        /// <returns></returns>
        //private bool proc_auto()
        //{
        //    bool result = false;
        //    cb_proc_Click(null, null);
        //    for (int i = 0; i < cb_proc.Items.Count; i++)
        //    {
        //        if (cb_proc.Items[i].ToString().ToLower().Contains(diablo_name))
        //        {
        //            cb_proc.SelectedIndex = i;
        //            result = true;
        //        }
        //    }
        //    if (result)
        //        cb_proc_SelectionChangeCommitted(null, null);

        //    return result;
        //}

        private void chb_key_CheckedChanged(object sender, EventArgs e)
        {
            var cb = (CheckBox)sender;
            var chb = ((CheckBox)sender).Name;

            //if (cb.Checked && cb_prog.Enabled && chb_hold.Checked) //17.04.2015
            //{
            //    cb_prog.Enabled = false;
            //    cb_prog.SelectedIndex = 0;
            //    cb_prog_SelectionChangeCommitted(null, null);
            //    cb_proc_Click(null, null);
            //    //if (!proc_auto()) cb_proc.SelectedIndex = -1;
            //}
            //else
            //{
            //    int i = 0;
            //    foreach (CheckBox chb_check in this.pan_main.Controls.OfType<CheckBox>())
            //        if (chb_check.Checked) i++;
            //    if (i == 0 && !cb_prog.Enabled && cb_proc.SelectedIndex < 1 && !chb_proconly.Checked)
            //    {
            //        cb_prog.Enabled = true;
            //    }
            //}
            check_only();

            switch (chb)
            {
                case "chb_key1": 
                    if (cb.Checked && chb_hold.Checked)
                        nud_tmr1.Enabled = false;
                    else
                        nud_tmr1.Enabled = true;
                    //{
                    //    nud_tmr1.Enabled = false;
                    //    cb_key_del(1);
                    //}
                    //else
                    //{
                    //    nud_tmr1.Enabled = true;
                    //    if (cb_proc.SelectedIndex<1) cb_key_add(1);
                    //}
                    break;

                case "chb_key2": 
                    if (cb.Checked && chb_hold.Checked)
                        nud_tmr2.Enabled = false;
                    else
                        nud_tmr2.Enabled = true;
                    //{
                    //    nud_tmr2.Enabled = false;
                    //    cb_key_del(2);
                    //}
                    //else
                    //{
                    //    nud_tmr2.Enabled = true;
                    //    if (cb_proc.SelectedIndex < 1) cb_key_add(2);
                    //}
                    break;

                case "chb_key3": 
                    if (cb.Checked && chb_hold.Checked)
                        nud_tmr3.Enabled = false;
                    else
                        nud_tmr3.Enabled = true;
                    //{
                    //    nud_tmr3.Enabled = false;
                    //    cb_key_del(3);
                    //}
                    //else
                    //{
                    //    nud_tmr3.Enabled = true;
                    //    if (cb_proc.SelectedIndex < 1) cb_key_add(3);
                    //}
                    break;

                case "chb_key4": 
                    if (cb.Checked && chb_hold.Checked)
                        nud_tmr4.Enabled = false;
                    else
                        nud_tmr4.Enabled = true;
                    //{
                    //    nud_tmr4.Enabled = false;
                    //    cb_key_del(4);
                    //}
                    //else
                    //{
                    //    nud_tmr4.Enabled = true;
                    //    if (cb_proc.SelectedIndex < 1) cb_key_add(4);
                    //}
                    break;

                case "chb_key5": 
                    if (cb.Checked && chb_hold.Checked)
                        nud_tmr5.Enabled = false;
                    else
                        nud_tmr5.Enabled = true;
                    //{
                    //    nud_tmr5.Enabled = false;
                    //    cb_key_del(5);
                    //}
                    //else
                    //{
                    //    nud_tmr5.Enabled = true;
                    //    if (cb_proc.SelectedIndex < 1) cb_key_add(5);
                    //}
                    break;

                case "chb_key6": 
                    if (cb.Checked && chb_hold.Checked)
                        nud_tmr6.Enabled = false;
                    else
                        nud_tmr6.Enabled = true;
                    //{
                    //    nud_tmr6.Enabled = false;
                    //    cb_key_del(6);
                    //}
                    //else
                    //{
                    //    nud_tmr6.Enabled = true;
                    //    if (cb_proc.SelectedIndex < 1) cb_key_add(6);
                    //}
                    break;
            }
        }



        /// <summary>
        /// Метод добавления пунктов меню с Shift
        /// </summary>
        /// <param name="i"></param>
        private void cb_key_add(int i)
        {
            int cb_select, shift_pos, star_pos,
                cb_select_new;

            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("key"))
                {
                    cb_select = cb.SelectedIndex;
                    shift_pos = cb.FindString("Shift");
                    star_pos = cb.FindString("*");

                    cb_select_new = cb_select;

                    if (shift_pos < 2)
                    {
                        cb.Items.Add("Shift+LM");
                        cb.Items.Add("Shift+RM");
                        if (cb_select == star_pos) cb_select_new = cb_select_new + 2;
                    }

                    if (star_pos > 0)
                    {
                        cb.Items.RemoveAt(cb.FindString("*"));
                        if (cb_select == star_pos) cb_select_new--;
                    }

                    if (star_add(cb) && cb_select == star_pos)
                        cb_select_new++;

                    cb.SelectedIndex = cb_select_new;
                }

            }            
        }

        /// <summary>
        /// Метод удаления пунктов меню с Shift
        /// </summary>
        /// <param name="i"></param>
        private void cb_key_del(int i)
        {
            int cb_select, shift_pos, star_pos,
                cb_select_new, star_pos_new;
            
            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("key"))
                {
                    cb_select = cb.SelectedIndex;
                    shift_pos = cb.FindString("Shift");
                    star_pos = cb.FindString("*");

                    cb_select_new = cb_select;
                    star_pos_new = star_pos;

                    if (shift_pos > 0)
                    {
                        cb.Items.RemoveAt(shift_pos);
                        cb.Items.RemoveAt(shift_pos);
                        if (cb_select >= shift_pos) cb_select_new = cb_select - 2;
                        star_pos_new = star_pos - 2;
                    }
                    if (star_pos > 0)
                    {
                        cb.Items.RemoveAt(star_pos_new);
                        if (cb_select == star_pos) cb_select_new--;
                    }

                    if (star_add(cb) && star_pos > 0 && cb_select == star_pos) cb_select_new++;

                    cb.SelectedIndex = cb_select_new;
                }
            }
        }

        private bool star_add(ComboBox cb)
        {
            bool result = false;
            string[] keys = new string[] { "cb_key1", "cb_key2", "cb_key3", "cb_key4", "cb_key5", "cb_key6" };
            string[] vals = new string[] { Settings.Default.cb_key1_desc, Settings.Default.cb_key2_desc, Settings.Default.cb_key3_desc, 
                Settings.Default.cb_key4_desc, Settings.Default.cb_key5_desc, Settings.Default.cb_key6_desc };

            for (int j = 0; j < 5; j++)
            {
                if (cb.Name.Contains(keys[j]) && vals[j] != null)
                {
                    cb.Items.Add(vals[j]);
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Метод обработки галки выбора программы или области
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chb_hold_CheckedChanged(object sender, EventArgs e)
        {
            if (opt_click == 1) opt_change = 1;
            if (chb_hold.Checked)
            {
                pan_proc.Visible = true;
                pan_prog.Visible = false;
                if (cb_proc.SelectedIndex < 1) cb_proc_Click(null, null);
                foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>())
                {
                    if (chb.Name != "cb_start")
                    {
                        chb.Visible = true;
                        chb_key_CheckedChanged(chb, null);
                        //if (chb.Checked && cb_proc.SelectedIndex < 1) pan_hold.Visible = true;
                    }
                }
                check_only();

                //if (cb_key1.FindString("Shift+LM") > 1) 
                    cb_key_del(0);
            }
            else 
            {
                handle = IntPtr.Zero;
                pan_proc.Visible = false;
                pan_prog.Visible = true;
                d3proc = false;
                foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>())
                {
                    if (chb.Name != "cb_start")
                    {
                        chb.Checked = false;
                        chb.Visible = false;
                        chb_key_CheckedChanged(chb, null);
                    }
                }
                //if (cb_key1.FindString("Shift+LM") < 1) 
                    cb_key_add(0);
                
                //if (lb_hold.Text != lng.lb_hold_hot) error_not(4);
                //pan_hold.Visible = false;
                //if (!cb_prog.Enabled && cb_proc.SelectedIndex < 1 && !chb_proconly.Checked) //17.04.2015
                //    cb_prog.Enabled = true;
            }
            //cb_op_SelectionChangeCommitted(null, null);
            error_not(2);
        }

        private void chb_saveload_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_saveload.Checked)
            {
                cb_prof.Visible = false;
                lb_prof.Text = lng.lb_prof_save;
                b_save.Visible = true;
                b_load.Visible = true;
                cb_hot_prof.SelectedIndex = -1;
                cb_hot_prof.Enabled = false;
            }
            else
            {
                cb_prof.Visible = true;
                lb_prof.Text = lng.lb_prof;
                b_save.Visible = false;
                b_load.Visible = false;
                cb_hot_prof.Enabled = true;
            }
            cb_op_SelectionChangeCommitted(null, null);
        }

        /// <summary>
        /// Метод перевод мс в секунды и отображения этой информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_Leave(object sender, EventArgs e)
        {
            Label lb = null; 
            var nud = (NumericUpDown) sender;
            var nud_name = nud.Name;
            if (nud.Text == "") nud.Value = 0;
            switch (nud_name)
            {
                case "nud_key_delay_ms": 
                    lb = lb_key_delay_desc;
                    cb_op_SelectionChangeCommitted(null, null);
                    break;
                case "nud_rand": 
                    lb = lb_nud_rand;
                    cb_op_SelectionChangeCommitted(null, null);
                    break;
                case "nud_tmr1": lb = lb_tmr1_sec; break;
                case "nud_tmr2": lb = lb_tmr2_sec; break;
                case "nud_tmr3": lb = lb_tmr3_sec; break;
                case "nud_tmr4": lb = lb_tmr4_sec; break;
                case "nud_tmr5": lb = lb_tmr5_sec; break;
                case "nud_tmr6": lb = lb_tmr6_sec; break;
            }

            if (lb != null)
                if (nud.Value > 0)
                    lb.Text = Math.Round((nud.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                else 
                    lb.Text = lng.lb_tmr_sec;
            error_select();
        }

        private void cb_hot_prof_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id_prof1 = 1, id_prof2 = 2, id_prof3 = 3;     // The id of the hotkey. 
            
            UnregisterHotKey(this.Handle, id_prof1);
            UnregisterHotKey(this.Handle, id_prof2);
            UnregisterHotKey(this.Handle, id_prof3);

            if (cb_hot_prof.SelectedIndex > 0)
            {

                switch (cb_hot_prof.SelectedIndex)
                {
                    case 1:
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F1.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F2.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F3.GetHashCode());
                        break;
                    case 2: 
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F2.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F3.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F4.GetHashCode());
                        break;
                    case 3: 
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F3.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F4.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F5.GetHashCode());
                        break;
                    case 4: 
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F4.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F5.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F6.GetHashCode());
                        break;
                    case 5: 
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F5.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F6.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F7.GetHashCode());
                        break;
                    case 6: 
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F6.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F7.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F8.GetHashCode());
                        break;
                    case 7: 
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F7.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F8.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F9.GetHashCode());
                        break;
                    case 8: 
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F8.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F9.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F10.GetHashCode());
                        break;
                    case 9: 
                        RegisterHotKey(this.Handle, id_prof1, (int)KeyModifier.None, Keys.F9.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof2, (int)KeyModifier.None, Keys.F10.GetHashCode());
                        RegisterHotKey(this.Handle, id_prof3, (int)KeyModifier.None, Keys.F11.GetHashCode());
                        break;
                }
            }
        }

        private void cb_hot_prof_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (opt_click == 1) opt_change = 1;
            error_not(1);
        }

        private void tb_prof_name_Click(object sender, EventArgs e)
        {
            //tb_prof_name.ReadOnly = !tb_prof_name.ReadOnly;
            tb_prof_name.Focus();
            tb_prof_name.Font = new Font(tb_prof_name.Font, FontStyle.Regular);
            if (tb_prof_name.Text == "Наименование профиля" || tb_prof_name.Text == "Profile name")
            tb_prof_name.Text = "";
        }

        /// <summary>
        /// Метод сохранения наименования профиля.
        /// </summary>
        private void tb_prof_name_save()
        {
            //lb_lang_name.Focus();
            if (tb_prof_name.Text == "")
                tb_prof_name.Text = Settings.Default.tb_prof_name;
            else
            if (tb_prof_name.Text != Settings.Default.profile1 && tb_prof_name.Text != Settings.Default.profile3 && tb_prof_name.Text != Settings.Default.profile3)
            {
                //Save_settings(0);

                int j = 0;
                string prof_name = tb_prof_name.Text;
                lng.Lang_eng();
                if (lng.tb_prof_name == tb_prof_name.Text) j++;
                lng.Lang_rus();
                if (lng.tb_prof_name == tb_prof_name.Text) j++;
                if (prof_name.Length > 15) prof_name = prof_name.Substring(0, 15);
                if (j == 0 && cb_prof.SelectedIndex == 1) Settings.Default.profile1 = prof_name;
                else
                    if (j == 0 && cb_prof.SelectedIndex == 2) Settings.Default.profile2 = prof_name;
                    else
                        if (j == 0 && cb_prof.SelectedIndex == 3) Settings.Default.profile3 = prof_name;

                Settings.Default.Save();

                profiles_names();

                //tb_prof_name.Font = new Font(tb_prof_name.Font, FontStyle.Bold);
            }
        }


        private void tb_prof_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && prof_name_changed)
            {
                lb_lang_name.Focus();
                tb_prof_name_save();
                prof_name_changed = false;
            }
        }

        private void tb_prof_name_Leave(object sender, EventArgs e)
        {
            if (prof_name_changed)
            {
                tb_prof_name_save();
                prof_name_changed = false;
            }
        }

        private void tb_prof_name_MouseHover(object sender, EventArgs e)
        {

            tb_prof_name_Click(null, null);

        }

        private void tb_prof_name_TextChanged(object sender, EventArgs e)
        {
           if (form_shown) prof_name_changed = true;
        }

        private void d3hot_Shown(object sender, EventArgs e)
        {
            form_shown = true;
            //b_opt.Text = "Загрузка: " + delay_watch.ElapsedMilliseconds.ToString() + " мсек.";
        }

        private void cb_mapdelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_mapdelay.SelectedIndex > -1) map_delay = Convert.ToInt32(cb_mapdelay.Items[cb_mapdelay.SelectedIndex]);
        }

        private void cb_mapdelay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (opt_click == 1) opt_change = 1;
        }

        private void cb_map_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_map.SelectedIndex > 0 && (string)cb_map.Items[cb_map.SelectedIndex] != "") //24.04.2015
                map_key = (string)cb_map.Items[cb_map.SelectedIndex].ToString();
        }

        private void cb_users_CheckedChanged(object sender, EventArgs e)
        {
            if (opt_click == 1) opt_change = 1;
            if (chb_users.Checked) person(1);
            else person(0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cb_start.Checked = !cb_start.Checked;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cb_start.Checked)
                contextMenuStrip1.Items[0].Text = "Stop";
            else
                contextMenuStrip1.Items[0].Text = "Start";
        }

        //private void chb_proconly_CheckedChanged(object sender, EventArgs e) //17.04.2015
        //{
        //    if (opt_click == 1) opt_change = 1;
        //    if (chb_proconly.Checked)
        //    {
        //        //lb_area.Visible = false;
        //        cb_prog.Enabled = false;
        //        if (cb_proc.SelectedIndex < 1) cb_proc_Click(null, null);
        //    }
        //    else
        //    {
        //        //lb_area.Visible = true;
        //        if (cb_proc.SelectedIndex < 1) cb_prog.Enabled = true;
        //        if (chb_hold.Checked)
        //        {
        //            int i = 0;
        //            foreach (CheckBox chb_check in this.pan_main.Controls.OfType<CheckBox>())
        //                if (chb_check.Checked) i++;
        //            if (i > 0) cb_prog.Enabled = false;
        //        }


        //    }
        //    error_not(2);
        //}

        private void cb_trig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            error_select();
        }

        private void key_choose_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var cb = (ComboBox)sender;
            if (cb.SelectedIndex == cb.FindString(lng.cb_keys_choose))
                form_open(cb);
            error_select();
        }

        private void key_choose_MouseClick(object sender, MouseEventArgs e)
        {
            var cb = (ComboBox)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                form_open(cb);
                error_select();
            }
            
        }

        private void d3hot_Deactivate(object sender, EventArgs e)
        {
            if (!cb_start.Checked)
            {
                UnregisterHotKey(this.Handle, 1);
                UnregisterHotKey(this.Handle, 2);
                UnregisterHotKey(this.Handle, 3);
            }
        }

        private void d3hot_Activated(object sender, EventArgs e)
        {
            if (cb_hot_prof.SelectedIndex > 0) 
                cb_hot_prof_SelectedIndexChanged(null, null);
        }


    }
}
