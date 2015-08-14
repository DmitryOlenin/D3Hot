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
using System.Drawing;
using System.Net;
using System.Threading;
//using InputManager; //26.03.2015
//InputManager - http://www.codeproject.com/Articles/117657/InputManager-library-Track-user-input-and-simulate

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public double ver = 2.2;
        public string sep = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).ToString(),
            version = "Diablo 3 Hotkeys ver. 2.2";
        public System.Timers.Timer tmr_all, tmr_save, tmr_cdr, tmr_pic, tmr_pic_press; 
        public Stopwatch delay_watch, return_watch, key_watch, proc_watch, map_watch, tele_watch, cdr_watch;
        public Boolean shift = false, d3prog = false, d3proc = false, pic_get = false, resolution = true, lang_load = false;
        public InputSimulator inp = new InputSimulator();
        public int 
            pause = 0, prof_prev, tp_delay = 0, tmr_all_counter = 0, tmr_all_count = 0, cdr_count = 0, map_delay = 0, return_delay = 0
            ,multikeys = 0, opt_change = 0, opt_click = 0,WidthScreen, HeightScreen
            ;

        public static bool holded = false, hotkey_pressed = false, prof_name_changed = false, form_shown = false
            , start_main = true, start_opt = true, proc_selected = false, lmousehold = false, rmousehold = false
            , hold_use = false
            , debug = false, tmr_holding = false
            ;
        public static int t_press = 0, map_press = 0, return_press = 0, r_press = 0, return_press_count = 0,
             delay_press = 0, delay_press_interval = 0, shift_press = 0, delay_wait = 0
             ,tmr_cdr_curr = 0
             ;
        
        public static string tp_key = "", map_key = "", proc_curr = "", key_delay = "", ver_click = "",
                                                                                            diablo_name = "diablo";
        public long proc_size = 400000000;

        public static SettingsTable overview;
        public int[] hold = new int[] { 0, 0, 0, 0, 0, 0 }; //17.03.2015
        public int[] tmr_cdr_n = new int[] { 0, 0, 0, 0, 0, 0 }; //30.06.2015
        public int[] cdr_key = new int[] { 0, 0, 0, 0, 0, 0 }; //01.07.2015
        public int[] trig = new int[] { 0, 0, 0, 0, 0, 0 }; //01.07.2015
        public int[] tmr_f = new int[] { 0, 0, 0, 0, 0, 0 }; //01.07.2015
        public int[] hold_key = new int[] { 0, 0, 0, 0, 0, 0 }; //01.07.2015
        public int[] tmr_r = new int[] { 0, 0, 0, 0, 0, 0 }; //01.07.2015
        public int[] cdr_run = new int[] { 0, 0, 0, 0, 0, 0 }; //01.07.2015
        public System.Timers.Timer[] tmr; //09.07.2015
        public static Stopwatch[] tmr_watch;//09.07.2015
        public static int[] tmr_left = new int[] { 0, 0, 0, 0, 0, 0 }; //09.07.2015
        public static int[] key_h = new int[] { 0, 0, 0, 0, 0, 0 }; //09.07.2015
        public double[] tmr_i = new double[] { 0, 0, 0, 0, 0, 0 }; //09.07.2015
        public static VirtualKeyCode[] key_v = new VirtualKeyCode[] { 0, 0, 0, 0, 0, 0 }; //09.07.2015

        public static object valueLocker = new object(), valueLocker1 = new object(); //16.07.2015
        //private readonly object valueLocker = new object();

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

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y); //09.06.2015

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
                rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
                if (rnd + tmr_i[i] < 1) rnd = 31 - (int)tmr_i[i];
            }
            tmr[i].Interval = tmr_i[i] + rnd;
        }

        /// <summary>
        /// Метод запуска таймеров, установка задержки по ним.
        /// </summary>
        /// <param name="i"></param>
        public void timer_load(int i)
        {

            if (i == -1)
            {
                tmr_all = new System.Timers.Timer();
                tmr_all.Elapsed += tmr_all_Elapsed;
                tmr_all.Interval = 1; //01.07.2015
            }
            else
            {
                tmr[i] = new System.Timers.Timer();
                tmr[i].Elapsed += tmr_Elapsed;
                if (cdr_key[i] < 1) rand_interval(i);
                tmr_watch[i] = new Stopwatch();
            }
        }


        /// <summary>
        /// Метод удаления таймеров при остановке. 99 - всё. 88 - все, кроме главного потока.
        /// </summary>
        /// <param name="i"></param>
        public void timer_unload(int i)
        {
            if (tmr_all != null && (i == -1 || i == 99)) tmr_all.Dispose();

            int timer_elapsed = 0;

            if (i > -1 && i < 88 && tmr[i] != null)
            {
                bool result = Int32.TryParse(tmr_watch[i].ElapsedMilliseconds.ToString(), out timer_elapsed);
                tmr_left[i] = (int)tmr[i].Interval > 0 && result ? (int)tmr[i].Interval - timer_elapsed : 0;
                tmr[i].Dispose();
                //tmr_watch[i].Stop();
                StopWatch(tmr_watch[i]);
            }

            else if (i > 87)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (tmr[j] != null)
                    {
                        bool result = Int32.TryParse(tmr_watch[j].ElapsedMilliseconds.ToString(), out timer_elapsed);
                        tmr_left[j] = (int)tmr[j].Interval > 0 && result ? (int)tmr[j].Interval - timer_elapsed : 0;
                        //tmr1.Enabled = false;
                        tmr[j].Dispose();
                        //tmr_watch[j].Stop();
                        StopWatch(tmr_watch[j]);
                    }
                }
            }
            
        }

        /// <summary>
        /// Метод для отслеживания нажатий T/Enter и подсчёта количества нажатий.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseKeyEventProvider1_KeyDown(object sender, KeyEventArgs e)
        {
            if (delay_press_interval > 0 && e.KeyCode.ToString() == key_delay) delay_press = 1;
            if (e.KeyCode.ToString() == tp_key) t_press = 1;
            if (e.KeyCode.ToString() == map_key) map_press = 1;
            if (e.KeyCode.ToString() == "Return") return_press = 1;
            //lb_press.Text = e.KeyCode.ToString();
        }

        private void mouseKeyEventProvider1_MouseDown(object sender, MouseEventArgs e)
        {
            if (delay_press_interval > 0 &&
                (key_press(trig[0]) || key_press(trig[1]) || key_press(trig[2]) || key_press(trig[3]) || key_press(trig[4]) || key_press(trig[5]))
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
                    if (this.Location.X > 0) Settings.Default.pos_x = this.Location.X; //14.05.2015
                    if (this.Location.Y > 0) Settings.Default.pos_y = this.Location.Y;

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
            debug = lb_debug.Visible;
            //this.Icon = D3Hot.Properties.Resources.diablo_hot;
            //notify_d3h.Icon = D3Hot.Properties.Resources.diablo_hot;

            //MessageBox.Show((rect.Width / 16 * 10).ToString() + " / " + (rect.Width / 16 * 9).ToString() + " /  " + rect.Height.ToString());
            
            //resolution = false;
            if (rect.Width / 16 * 10 != rect.Height && rect.Width / 16 * 9 != rect.Height) //15.07.2015
                resolution = false;

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
            if (nud_tmr1.Value > 0 && cb_tmr1.SelectedIndex < 1) lb_tmr1_sec.Text = Math.Round((nud_tmr1.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr2.Value > 0 && cb_tmr2.SelectedIndex < 1) lb_tmr2_sec.Text = Math.Round((nud_tmr2.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr3.Value > 0 && cb_tmr3.SelectedIndex < 1) lb_tmr3_sec.Text = Math.Round((nud_tmr3.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr4.Value > 0 && cb_tmr4.SelectedIndex < 1) lb_tmr4_sec.Text = Math.Round((nud_tmr4.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr5.Value > 0 && cb_tmr5.SelectedIndex < 1) lb_tmr5_sec.Text = Math.Round((nud_tmr5.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr6.Value > 0 && cb_tmr6.SelectedIndex < 1) lb_tmr6_sec.Text = Math.Round((nud_tmr6.Value / 1000), 2).ToString() + " " + lng.lang_sec;
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

            if (chb_ver_check.Checked) lb_ver_check_Click(null, null); //Проверка новой версии 27.04.2015

        }

        private void key_menu()
        {
            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("key"))
                {
                    cb.Items[0] = lng.cb_keys_choose;
                }
            }
            cb_tp.Items[0] = lng.cb_keys_choose;
            cb_map.Items[0] = lng.cb_keys_choose;
            cb_key_delay.Items[0] = lng.cb_keys_choose;

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

                version = "Diablo 3 Hotkeys ver. " + string.Format("{0:F1}", ver).ToString().Replace(sep, ".");
                this.Text = version;
            }
            else
            {
                string progname = "", iconame = "", jpgname = "", txtname = "";
                //progname = Path.GetFullPath(Application.ExecutablePath); //GetFileNameWithoutExtension
                try { progname = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 3); }
                catch { }

                if (progname.Length > 0 && File.Exists(progname + "ico"))
                {
                    iconame = progname + "ico";
                }
                else
                {
                    string[] icofiles = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ico", System.IO.SearchOption.TopDirectoryOnly);
                    if (icofiles.Length > 0 && File.Exists(icofiles[0])) iconame = icofiles[0];
                }

                if (progname.Length > 0 && File.Exists(progname + "jpg"))
                {
                    jpgname = progname + "jpg";
                }
                else
                {
                    string[] jpgfiles = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg", System.IO.SearchOption.TopDirectoryOnly);
                    if (jpgfiles.Length > 0 && File.Exists(jpgfiles[0])) jpgname = jpgfiles[0];
                }

                if (progname.Length > 0 && File.Exists(progname + "txt"))
                {
                    txtname = progname + "txt";
                }
                else
                {
                    string[] txtfiles = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt", System.IO.SearchOption.TopDirectoryOnly);
                    if (txtfiles.Length > 0 && File.Exists(txtfiles[0])) txtname = txtfiles[0];
                }


                if (iconame.Length > 0)
                {
                    this.Icon = new System.Drawing.Icon(iconame);
                    notify_d3h.Icon = new System.Drawing.Icon(iconame);
                }

                if (jpgname.Length > 0)
                    this.BackgroundImage = new Bitmap(jpgname);


                if (txtname.Length > 0)
                {
                    string[] readText = System.IO.File.ReadAllLines(txtname, Encoding.Default);
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
        /// <param name="i">1: Shift, 2: Scroll, 3: Caps, 4: Num</param>
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

        private void timer_cdr_create(int i)
        {
            //if (tmr_cdr_n[i] == 0) //- 1
            //{
            //MessageBox.Show("123"+i.ToString());
            //tmr_cdr = new System.Timers.Timer();
            tmr_cdr_curr = i + 1;
            tmr_Elapsed(tmr_cdr, null);
            
            //tmr_cdr.Dispose();
            //}
            //tmr_cdr_n[i]++;// 07.07.2015
            //if (tmr_cdr_n[i] > 1) tmr_cdr_n[i] = 0; // 07.07.2015
        }

        /// <summary>
        /// Основной метод после запуска процесса - срабатывание главного таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tmr_all_Elapsed(object sender, EventArgs e)
        {

            if (return_delay > 0 && return_press == 1) //(pause == 2 || pause == 3)
            {
                return_press_count++;
                return_press = 0;
                if (return_press_count == 1)
                {
                    return_watch = new Stopwatch();
                    return_watch.Start();
                }
            }

            if (return_press_count > 1 || (return_delay > 0 && return_watch != null && (int)return_watch.ElapsedMilliseconds > return_delay * 1000)) //Обработка количества Enter для работы с Shift
            {
                return_press_count = 0;
                return_press = 0;
                delay_wait = (int)return_watch.ElapsedMilliseconds;
                return_watch.Stop();
            }

            //if (seconds_count > 0) seconds_count++;


            //Работаем только если хоть что-то из триггеров зажато/переключено.
            if (key_press(trig[0]) || key_press(trig[1]) || key_press(trig[2]) || key_press(trig[3]) || key_press(trig[4]) || key_press(trig[5]))
            {

                if (return_watch != null && return_watch.IsRunning && r_press == 0)
                {
                    timer_unload(88);
                    Array.Clear(tmr_r, 0, 6);
                    //tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    r_press = 1; //return_press = 0;
                    hold_clear(88);
                    Array.Clear(hold, 0, 6);// hold[0] = 0; hold[1] = 0; hold[2] = 0; hold[3] = 0; hold[4] = 0; hold[5] = 0;
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
                if (tele_watch != null && tele_watch.ElapsedMilliseconds >= tp_delay * 1000) //tp_delay > 0 && t_press > 0 && tmr_all.Interval == tp_delay * 1000
                {
                    delay_wait = (int)tele_watch.ElapsedMilliseconds;
                    t_press = 0;
                    //tmr_all.Interval = 1; //01.06.2015
                    StopWatch(tele_watch);//delay_timers(33);
                    return_press = 0;
                }

                if (!(tele_watch != null && tele_watch.IsRunning) && tp_delay > 0 && t_press > 0 && r_press == 0) //(pause == 1 || pause == 3)
                {
                    tmr_cdr_destroy(); //21.07.2015
                    timer_unload(88); 
                    Array.Clear(tmr_r, 0, 6); //tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    hold_clear(88);
                    Array.Clear(hold, 0, 6);// hold[0] = 0; hold[1] = 0; hold[2] = 0; hold[3] = 0; hold[4] = 0; hold[5] = 0;
                    //tmr_all.Interval = tp_delay * 1000; //01.06.2015
                    RestartWatch(ref tele_watch); //delay_timers(23);
                }

                if (tp_delay > 0 && r_press > 0 && t_press > 0) t_press = 0; //(pause == 1 || pause == 3)

                //Проверка на нажатие M.
                if (map_watch != null && map_watch.ElapsedMilliseconds >= map_delay * 1000) //map_delay > 0 && map_press > 0 && tmr_all.Interval == map_delay * 1000
                {
                    delay_wait = (int)map_watch.ElapsedMilliseconds;
                    map_press = 0;
                    //tmr_all.Interval = 1; //01.06.2015
                    StopWatch(map_watch);//delay_timers(32);
                    return_press = 0;
                }

                if (!(map_watch != null && map_watch.IsRunning) && map_delay > 0 && map_press > 0 && r_press == 0)
                {
                    tmr_cdr_destroy(); //21.07.2015
                    timer_unload(88);
                    Array.Clear(tmr_r, 0, 6); //tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    hold_clear(88);
                    Array.Clear(hold, 0, 6);//hold[0] = 0; hold[1] = 0; hold[2] = 0; hold[3] = 0; hold[4] = 0; hold[5] = 0;
                    //tmr_all.Interval = map_delay * 1000; //01.06.2015
                    RestartWatch(ref map_watch); //delay_timers(22);
                }

                if (map_delay > 0 && r_press > 0 && map_press > 0) map_press = 0;

                //16.03.2015 Проверка на клавишу задержки
                if (key_watch != null && key_watch.ElapsedMilliseconds >= delay_press_interval) //delay_press > 0 && tmr_all.Interval == delay_press_interval
                {
                    if (delay_wait == 0) delay_wait = (int)key_watch.ElapsedMilliseconds;
                    delay_press = 0;
                    StopWatch(key_watch);//delay_timers(31);
                    //tmr_all.Interval = 1;
                }

                if (delay_press > 0 && delay_press_interval > 0 && r_press == 0 && map_press == 0 && t_press == 0) //&& !key_press(1)
                {
                    if (key_watch == null || (key_watch != null && !key_watch.IsRunning)) delay_wait = 0;
                    if (key_watch != null && key_watch.IsRunning) delay_wait += (int)key_watch.ElapsedMilliseconds;

                    tmr_cdr_destroy(); //21.07.2015
                    timer_unload(88);
                    Array.Clear(tmr_r, 0, 6); //tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    hold_clear(88);
                    Array.Clear(hold, 0, 6);//hold[0] = 0; hold[1] = 0; hold[2] = 0; hold[3] = 0; hold[4] = 0; hold[5] = 0;
                    delay_press = 0;
                    //tmr_all.Interval = delay_press_interval;
                    RestartWatch(ref key_watch); //delay_timers(21);
                }

                if (return_delay > 0 && r_press > 0 && delay_press > 0) delay_press = 0; //(pause == 2 || pause == 3)

                //Если T|Enter не нажаты, запускаем таймеры триггеров 1-2-3-4-5-6 при активном состоянии и останавливаем при отключенном.
                if (map_press == 0 && t_press == 0 && r_press == 0 && (delay_press == 0 && (key_watch == null || (key_watch != null && !key_watch.IsRunning))))
                {
                    tmr_all_counter = 3;

                    ////Stopwatch st = null;
                    ////if (tmr_all_count == 1)
                    ////{st = new Stopwatch(); st.Reset(); st.Start(); }

                    ////if (tmr_pic != null && !tmr_pic.Enabled) tmr_all_count += 20;
                    ////if ((tmr_pic == null || !tmr_pic.Enabled)) 

                    ////if (tmr_all_count > 15) tmr_all_count = 0; //|| (tmr_pic != null && !tmr_pic.Enabled) //07.07.2015
                    ////tmr_all_count++;

                    ////if (tmr_watch[2] != null) MessageBox.Show(tmr_watch[2].IsRunning.ToString() + " " + tmr_watch[2].ElapsedMilliseconds.ToString() + " "
                    ////    + tmr_f[2].ToString() + " " + key_press(trig[2]).ToString() + " " + cdr_key[2].ToString());

                    ////MessageBox.Show("Долгое " + cdr_isrun.ToString() + cdr_isready.ToString() + cdr_press.Any(item => item == 1).ToString());
                    ////MessageBox.Show("cdr_isrun: " + cdr_isrun.ToString() +
                    ////" Прожимаем?" + cdr_press.Any(item => item == 1).ToString() +
                    ////" Захват картинки? " + (tmr_pic == null || !tmr_pic.Enabled).ToString());

                    //if (!cdr_isready && //!cdr_isrun && 
                    //    //tmr_all_count == 1 &&
                    //    (tmr_pic == null || !tmr_pic.Enabled)
                    //    //(tmr_cdr == null || !tmr_cdr.Enabled)&&
                    //    && !cdr_press.Any(item => item == 1)
                    //    //&& (cdr_watch == null || !cdr_watch.IsRunning || cdr_watch.ElapsedMilliseconds > 100)
                    //    )// 
                    //{

                    //    //RestartWatch(ref cdr_watch); //15.07.2015
                    //    //StopWatch(cdr_watch);
                    //    cdr_isready = true;
                    //    bool coold = false;//13.07.2015 

                    //    //if (cdr_run == null)
                    //    cdr_run = new int[] { 0, 0, 0, 0, 0, 0 };


                    //    //if (Monitor.TryEnter(valueLocker, 10))
                    //    //{
                    //    //    try { coold = true; }
                    //    //    finally { Monitor.Exit(valueLocker); }
                    //    //}

                    //    //MessageBox.Show(tmr_f[2].ToString() + cdr_key[2].ToString() + key_press(trig[2]).ToString());

                    //    for (int i = 0; i < 6; i++)
                    //    {
                    //        if (tmr_f[i] == 1 && key_press(trig[i]) && cdr_key[i] == 1 && pic_analyze[i] == 0) //&& cdr_press[i] == 0
                    //        {
                    //            //cdr_run[i] = 1;
                    //            //if (tmr_watch[i] != null) MessageBox.Show(tmr_watch[i].IsRunning.ToString() + " " + tmr_watch[i].ElapsedMilliseconds.ToString());

                    //            //if (tmr_watch[i] != null) MessageBox.Show(tmr_watch[i].ElapsedMilliseconds.ToString());
                    //            //else MessageBox.Show("Наш триггер: "+i.ToString());


                    //            //if (tmr_watch[i] != null && tmr_watch[i].IsRunning && tmr_watch[i].ElapsedMilliseconds < 500)
                    //            //    cdr_run[i] = 0;
                    //            //else
                    //            //{
                    //            //    //coold = true;//13.07.2015 
                    //            //    //timer_unload(i);
                    //            //    if (tmr_watch[i] != null) tmr_watch[i].Reset();
                    //            //    cdr_run[i] = 1;
                    //            //}

                    //            cdr_run[i] = 1;
                    //            pic_analyze[i]++;
                    //            coold = true;

                    //        }

                    //        //if (tmr_all_count > 1 && cdr_press[i] > 0)
                    //        //{
                    //        //    cdr_run[i] = 0;
                    //        //    cdr_press[i] = 2;
                    //        //}
                    //        //else
                    //        //{
                    //        //    //MessageBox.Show("Что обнулили? "+i.ToString());
                    //        //    cdr_press[i] = 0;
                    //        //}

                    //        //if (cdr_run[i] == 1)
                    //        //{
                    //        //    coold = true;
                    //        //    //if (tmr_all_count > 100) MessageBox.Show("!@#");
                    //        //}
                    //        if (pic_analyze[i] > 0) pic_analyze[i]++;
                    //        if (pic_analyze[i] > 20) pic_analyze[i] = 0; //40
                    //    }

                    //    if (coold) //13.07.2015 
                    //    {
                    //        //MessageBox.Show("123");
                    //        //screen_capt_prereq(cdr_run);
                    //        //screen_capt_pre();

                    //        //if (tmr_pic == null)
                    //        //{
                    //        //    tmr_pic = new System.Timers.Timer();
                    //        //    tmr_pic.Elapsed += tmr_pic_Elapsed;
                    //        //    tmr_pic.AutoReset = false;
                    //        //}
                    //        //tmr_pic.Start();

                    //        screen_capt_pre();
                    //        cdr_press = ScreenCapture(cdr_run);
                    //        cdr_isready = false;
                    //    }
                    //    else cdr_isready = false;

                    //}

                    ////if (Monitor.TryEnter(valueLocker, 0))
                    ////{
                    //    //try
                    //    //{

                    //        if (!cdr_isrun && //!cdr_isready &&
                    //            //(tmr_cdr == null || !tmr_cdr.Enabled)&&
                    //            (cdr_press.Any(item => item == 1))
                    //            //&&(cdr_watch != null && cdr_watch.ElapsedMilliseconds > 5)
                    //            )
                    //        {
                    //            //Interlocked.Increment(ref tmr_all_count);
                    //            //tmr_all_count++;
                    //            cdr_isrun = true;

                    //            if (tmr_cdr == null)
                    //            {
                    //                tmr_cdr = new System.Timers.Timer();
                    //                tmr_cdr.Elapsed += tmr_cdr_Elapsed;
                    //                tmr_cdr.AutoReset = false;
                    //            }
                    //            tmr_cdr.Start();

                    //            //MessageBox.Show(cdr_watch.ElapsedMilliseconds.ToString()); //15.07.2015
                    //            //StopWatch(cdr_watch); //15.07.2015

                    //            //for (int i = 0; i < 6; i++) //прожимаем после предыдущего таймера
                    //            //{
                    //            //    if (cdr_press[i] == 1 && key_press(trig[i])) timer_cdr_create(i);
                    //            //    cdr_press[i] = 0;
                    //            //}
                    //            //cdr_isrun = false;

                    //            //for (int i = 0; i < 6; i++)
                    //            //{
                    //            //    if (cdr_press[i] == 1)
                    //            //    {
                    //            //        tmr_r[i] = 1;
                    //            //        timer_load(i);
                    //            //        tmr[i].AutoReset = false;
                    //            //        tmr[i].Enabled = true;
                    //            //    }
                    //            //}

                    //            //for (int i = 0; i < 6; i++)
                    //            //{
                    //            //    if (cdr_press[i] == 1)
                    //            //    {
                    //            //        tmr[i].AutoReset = false;
                    //            //        //tmr_watch[i].Start();
                    //            //        tmr[i].Enabled = true;
                    //            //        //MessageBox.Show("1: " + tmr_watch[2].IsRunning.ToString());


                    //            //        //if (tmr[i] != null)
                    //            //        //    try
                    //            //        //    {
                    //            //        //        tmr[i].Enabled = true;
                    //            //        //        tmr_watch[i].Start();
                    //            //        //    }
                    //            //        //    catch { }

                    //            //    }
                    //            //}

                    //            //for (int i = 0; i < 6; i++)
                    //            //{
                    //            //    if (cdr_press[i] == 1)
                    //            //    {
                    //            //        MessageBox.Show(i.ToString() + " Включён:" + cdr_press[i].ToString());
                    //            //    }
                    //            //}

                    //            //Array.Clear(cdr_press, 0, 6); //14.07.2015
                    //            //cdr_isrun = false; //14.07.2015

                    //            //RestartWatch(ref cdr_watch);
                    //            //do { }
                    //            //while (cdr_watch.ElapsedMilliseconds < 1000); //Сразу пытается нажать второй раз, как секунда проходит
                    //            //StopWatch(cdr_watch);
                    //        }
                    //    //}
                    //    //finally { Monitor.Exit(valueLocker); }
                    ////}


                            if (!cdr_run.Any(item => item == 1))
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    if (tmr_f[i] == 1 && key_press(trig[i]) && cdr_key[i] == 1)
                                    {
                                        cdr_run[i] = 1;
                                    }
                                }
                                if (cdr_run.Any(item => item == 1))
                                {
                                    if (tmr_cdr == null)
                                    {
                                        tmr_cdr = new System.Timers.Timer();
                                        tmr_cdr.Interval = 20;
                                        tmr_cdr.Elapsed += Cooldown_Tick;
                                    }
                                    if (!tmr_cdr.Enabled)
                                    {
                                        screen_capt_pre();
                                        tmr_cdr.Start();
                                    }
                                }
                            }
                    



                    if (tmr_f[0] == 1 && cdr_key[0] != 1) //09.07.2015
                    {
                        if (key_press(trig[0]))
                        {
                            //if (cdr_key[0] == 1 && pic_get) //&& cdr_key_check(0)) //25.06.2015
                            //{
                                //timer_cdr_create(1);
                            //}
                            if (cdr_key[0] == 0 && hold_key[0] == 1 && hold[0] == 0)  //18.03.2015
                            {
                                hold[0] = 1;
                                hold_load(0);
                            }
                            else if (cdr_key[0] == 0 && tmr_r[0] == 0 && hold_key[0] == 0)
                            {
                                tmr_r[0] = 1;
                                timer_load(0);

                                if (tmr_left[0] != 0 && (tmr_left[0] - delay_wait > 0))
                                    tmr[0].Interval = tmr_left[0] - delay_wait;
                                    //MessageBox.Show("Таймера осталось: " + tmr_left[0].ToString() + " А ждали мы: " + delay_wait.ToString());
                                else
                                    tmr_Elapsed(tmr[0], null);
                                if (tmr[0] != null)
                                    try { 
                                        tmr[0].Enabled = true;
                                        tmr_watch[0].Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key[0] == 1) hold_clear (0); else
                                if (tmr_r[0] != 0) timer_unload(0);
                            tmr_r[0] = 0; tmr_left[0] = 0; tmr_cdr_n[0] = 0;
                        }
                    }

                    if (tmr_f[1] == 1 && cdr_key[1] != 1) //09.07.2015
                    {
                        if (key_press(trig[1]))
                        {
                            //if (cdr_key[1] == 1 && pic_get) // && cdr_key_check(1)) //25.06.2015
                            //{
                                //timer_cdr_create(2);
                            //}
                            if (cdr_key[1] == 0 && hold_key[1] == 1 && hold[1] == 0)  //18.03.2015
                            {
                                hold[1] = 1;
                                hold_load(1);
                            }
                            else if (cdr_key[1] == 0 && tmr_r[1] == 0 && hold_key[1] == 0)
                            {
                                tmr_r[1] = 1;
                                timer_load(1);
                                if (tmr_left[1] != 0 && (tmr_left[1] - delay_wait > 0))
                                    tmr[1].Interval = tmr_left[1] - delay_wait;
                                else
                                    tmr_Elapsed(tmr[1], null);
                                if (tmr[1] != null)
                                    try { 
                                        tmr[1].Enabled = true;
                                        tmr_watch[1].Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key[1] == 1) hold_clear(1);
                            else if (tmr_r[1] != 0) timer_unload(1);
                            tmr_r[1] = 0; tmr_left[1] = 0; tmr_cdr_n[1] = 0;
                        }
                    }

                    if (tmr_f[2] == 1 && cdr_key[2] != 1) //09.07.2015
                    {
                        if (key_press(trig[2]))
                        {
                            //if (cdr_key[2] == 1 && pic_get && cdr_key_check(2)) //25.06.2015
                            //{
                            //   timer_cdr_create(3);
                            //}
                            if (cdr_key[2] == 0 && hold_key[2] == 1 && hold[2] == 0)  //18.03.2015
                            {
                                hold[2] = 1;
                                hold_load(2);
                            }
                            else if (cdr_key[2] == 0 && tmr_r[2] == 0 && hold_key[2] == 0)
                            {
                                tmr_r[2] = 1;
                                timer_load(2);
                                if (tmr_left[2] != 0 && (tmr_left[2] - delay_wait > 0))
                                    tmr[2].Interval = tmr_left[2] - delay_wait;
                                else
                                    tmr_Elapsed(tmr[2], null);
                                if (tmr[2] != null)
                                    try { 
                                        tmr[2].Enabled = true;
                                        tmr_watch[2].Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key[2] == 1) hold_clear(2);
                            else if (tmr_r[2] != 0) timer_unload(2);
                            tmr_r[2] = 0; tmr_left[2] = 0; tmr_cdr_n[2] = 0;
                        }
                    }

                    if (tmr_f[3] == 1 && cdr_key[3] != 1) //09.07.2015
                    {
                        if (key_press(trig[3]))
                        {
                            //if (cdr_key[3] == 1 && pic_get && cdr_key_check(3)) //25.06.2015
                            //{
                            //    timer_cdr_create(4);
                            //}
                            if (cdr_key[3] == 0 && hold_key[3] == 1 && hold[3] == 0)  //18.03.2015
                            {
                                hold[3] = 1;
                                hold_load(3);
                            }
                            else if (cdr_key[3] == 0 && tmr_r[3] == 0 && hold_key[3] == 0)
                            {
                                tmr_r[3] = 1;
                                timer_load(3);
                                if (tmr_left[3] != 0 && (tmr_left[3] - delay_wait > 0))
                                    tmr[3].Interval = tmr_left[3] - delay_wait;
                                else
                                    tmr_Elapsed(tmr[3], null);
                                if (tmr[3] != null)
                                    try { 
                                        tmr[3].Enabled = true;
                                        tmr_watch[3].Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key[3] == 1) hold_clear(3);
                            else if (tmr_r[3] != 0) timer_unload(3);
                            tmr_r[3] = 0; tmr_left[3] = 0; tmr_cdr_n[3] = 0;
                        }
                    }

                    if (tmr_f[4] == 1 && cdr_key[4] != 1) //09.07.2015
                    {
                        if (key_press(trig[4]))
                        {
                            //if (cdr_key[4] == 1 && pic_get && cdr_key_check(4)) //25.06.2015
                            //{
                            //    timer_cdr_create(5);
                            //}
                            if (cdr_key[4] == 0 && hold_key[4] == 1 && hold[4] == 0)  //18.03.2015
                            {
                                hold[4] = 1;
                                hold_load(4);
                            }
                            else if (cdr_key[4] == 0 && tmr_r[4] == 0 && hold_key[4] == 0)
                            {
                                tmr_r[4] = 1;
                                timer_load(4);
                                if (tmr_left[4] != 0 && (tmr_left[4] - delay_wait > 0))
                                    tmr[4].Interval = tmr_left[4] - delay_wait;
                                else
                                    tmr_Elapsed(tmr[4], null);
                                if (tmr[4] != null)
                                    try { 
                                        tmr[4].Enabled = true;
                                        tmr_watch[4].Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key[4] == 1) hold_clear(4);
                            else if (tmr_r[4] != 0) timer_unload(4);
                            tmr_r[4] = 0; tmr_left[4] = 0; tmr_cdr_n[4] = 0;
                        }
                    }

                    if (tmr_f[5] == 1 && cdr_key[5] != 1) //09.07.2015
                    {
                        if (key_press(trig[5]))
                        {
                            //if (cdr_key[5] == 1 && pic_get && cdr_key_check(5)) //25.06.2015
                            //{
                            //    timer_cdr_create(6);
                            //}
                            if (cdr_key[5] == 0 && hold_key[5] == 1 && hold[5] == 0)  //18.03.2015
                            {
                                hold[5] = 1;
                                hold_load(5);
                            }
                            else if (cdr_key[5] == 0 && tmr_r[5] == 0 && hold_key[5] == 0)
                            {
                                tmr_r[5] = 1;
                                timer_load(5);
                                if (tmr_left[5] != 0 && (tmr_left[5] - delay_wait > 0))
                                    tmr[5].Interval = tmr_left[5] - delay_wait;
                                else
                                    tmr_Elapsed(tmr[5], null);
                                if (tmr[5] != null)
                                    try { 
                                        tmr[5].Enabled = true;
                                        tmr_watch[5].Start();
                                        }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key[5] == 1) hold_clear(5);
                            else if (tmr_r[5] != 0) timer_unload(5);
                            tmr_r[5] = 0; tmr_left[5] = 0; tmr_cdr_n[5] = 0;
                        }
                    }
                    delay_wait = 0;
                }
            }
            else //Ни один триггер не нажат
            {
                if (tmr_r[0] != 0) timer_unload(0);
                if (tmr_r[1] != 0) timer_unload(1);
                if (tmr_r[2] != 0) timer_unload(2);
                if (tmr_r[3] != 0) timer_unload(3);
                if (tmr_r[4] != 0) timer_unload(4);
                if (tmr_r[5] != 0) timer_unload(5);
                tmr_cdr_destroy();
                tmr_all_count = 0;
                Array.Clear(tmr_cdr_n, 0, 6);//tmr_cdr_n = new int[] { 0, 0, 0, 0, 0, 0 };
                delay_wait = 0;
                tmr_all.Interval = 1; //29.06.2015
                Array.Clear(tmr_left, 0, 6);//tmr_left[0] = 0; tmr_left[1] = 0; tmr_left[2] = 0; tmr_left[3] = 0; tmr_left[4] = 0; tmr_left[5] = 0;
                return_press = 0; r_press = 0; t_press = 0; map_press = 0; delay_press = 0;
                Array.Clear(tmr_r, 0, 6);//tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                StopWatch(key_watch); StopWatch(tele_watch); StopWatch(map_watch); StopWatch(return_watch);//delay_timers(1);
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
                    keyup(i); //20.03.2015
                    hold_unload(i);
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
                        hold_unload(j );
                        keyup(j);
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
            var tmr_local = (System.Timers.Timer)sender;
            int mult = 1, ret = 0;
            int key_for_hold = timer_key(tmr_local);
            VirtualKeyCode key = VirtualKeyCode.VK_0;
            if (nud_rand.Value > 0) rand = new Random();
            ret = _MapVirtualKey(key_for_hold, 0);

            if (multikeys != 0) mult = 3;

            for (int i = 0; i < 6; i++)
            {
                if (tmr_local == tmr[i] && key_press(trig[i]))
                {
                    key = key_v[i];// virt_code(key1);
                    if (cdr_key[i] < 1) rand_interval(i);
                    //tmr_watch[i].Reset();
                    //tmr_watch[i].Start();
                    RestartWatch(ref tmr_watch[i]); //13.07.2015
                    //if (i==2) MessageBox.Show("2: " + tmr_watch[2].IsRunning.ToString());
                    break;
                }
            }

            for (int i = 0; i < mult; i++)
            {
                //MessageBox.Show("Время задержки: "+tmr_i[0].ToString());
                if ((int)handle > 0)
                {
                    if ((key_for_hold == (int)Keys.LButton) || (key_for_hold == (int)Keys.RButton))
                    {

                        Point defPnt = new Point();
                        GetCursorPos(ref defPnt);

                        PostMessage(handle,
                                   updown_keys(key_for_hold),
                                   key_for_hold, (int)MakeLong(defPnt.X, defPnt.Y)); //(int)
                        //MessageBox.Show(System.Threading.Thread.CurrentThread.Name);
                        //System.Threading.Thread.Sleep(1000);
                        PostMessage(handle,
                                   updown_keys(key_for_hold) + 1,
                                   0, (int)MakeLong(defPnt.X, defPnt.Y)); //(int)
                    }

                    else
                    {
                        PostMessage(handle,//hWindow,
                                   updown_keys(key_for_hold),//(int)WM_KEYDOWN,
                                   key_for_hold, (int)(MakeLong(1, ret)));
                        //System.Threading.Thread.Sleep(40); //30.06.2015
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
                        key_v[0] = vkc;
                        key_h[0] = key_hold;
                        break;
                    case 2:
                        key_v[1] = vkc;
                        key_h[1] = key_hold;
                        break;
                    case 3:
                        key_v[2] = vkc;
                        key_h[2] = key_hold;
                        break;
                    case 4:
                        key_v[3] = vkc;
                        key_h[3] = key_hold;
                        break;
                    case 5:
                        key_v[4] = vkc;
                        key_h[4] = key_hold;
                        break;
                    case 6:
                        key_v[5] = vkc;
                        key_h[5] = key_hold;
                        break;
                }
            }

        }

        /// <summary>
        /// Метод при запуске программы или её остановке (Start/Stop/F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_start_CheckedChanged(object sender, EventArgs e)
        {
            //test = 1;
            lb_lang_name.Focus();
            //timer_unload(99);
            //if (key_watch != null) key_watch.Stop();
            StopWatch(key_watch); StopWatch(tele_watch); StopWatch(map_watch); //delay_timers(1);
            Array.Clear(tmr_left, 0, 6); // tmr_left[0] = 0; tmr_left[1] = 0; tmr_left[2] = 0; tmr_left[3] = 0; tmr_left[4] = 0; tmr_left[5] = 0;
            Array.Clear(tmr_f, 0, 6); // tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0; tmr5_f = 0; tmr6_f = 0;
            Array.Clear(tmr_r, 0, 6); //tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
            Array.Clear(tmr_i, 0, 6); //tmr_i[0] = 0; tmr_i[1] = 0; tmr_i[2] = 0; tmr_i[3] = 0; tmr_i[4] = 0; tmr_i[5] = 0;
            Array.Clear(trig, 0, 6); delay_wait = 0; shift_press = 0;
            //Array.Clear(hold_key, 0, 6);//hold_key0 = 0; hold_key1 = 0; hold_key2 = 0; hold_key3 = 0; hold_key4 = 0; hold_key5 = 0; //17.03.2015
            t_press = 0; map_press = 0; return_press = 0; r_press = 0; delay_press = 0; return_press_count = 0;

            if (cb_start.Text != "Stop")
            {
                if (pan_opt.Visible || opt_click == 1) b_opt_Click(null, null);

                bool proc_exist = false;

                if (debug && handle == IntPtr.Zero && d3proc) //For Debugging //27.04.2015
                {
                    proc_exist = true; 
                    diablo_name = "opera";
                    handle = FindWindow(null, "akelpad");
                    handle = FindWindowEx(handle, IntPtr.Zero, "AkelEditW", null);
                }

                if (!debug && chb_hold.Checked && !proc_selected && handle != IntPtr.Zero)
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

                if (chb_hold.Checked == false &&
                    (cb_key1.SelectedItem.ToString().Contains("Shift") || cb_key2.SelectedItem.ToString().Contains("Shift") || cb_key3.SelectedItem.ToString().Contains("Shift") ||
                    cb_key4.SelectedItem.ToString().Contains("Shift") || cb_key5.SelectedItem.ToString().Contains("Shift") || cb_key6.SelectedItem.ToString().Contains("Shift")))
                    foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                        if (cb.Name.Contains("trig") && cb.SelectedIndex == 1) cb.SelectedIndex = 0;
                
                error_not(2); //check_only(); 17.04.2015
                error_select();

                if (!start_main || !start_opt || !cb_start.Enabled)
                {
                    cb_start.Checked = false;
                    if (this.WindowState == FormWindowState.Minimized) ShowMe();
                }
            }

            if (cb_start.Checked)
            {
                tmr_all_count = 0;
                tmr = new System.Timers.Timer[6]; //09.07.2015
                tmr_watch = new Stopwatch[6]; //09.07.2015
                //test_sw.Start(); //07.07.2015
                Array.Clear(key_h, 0, 6); //key_h[0] = 0; key_h[1] = 0; key_h[2] = 0; key_h[3] = 0; key_h[4] = 0; key_h[5] = 0; //24.04.2015
                key_v[0] = 0; key_v[1] = 0; key_v[2] = 0; key_v[3] = 0; key_v[4] = 0; key_v[5] = 0; //24.04.2015
                d3hot_Activated(null, null); //27.04.2015

                //if (d3proc) proc_watch = new Stopwatch(); //06.04.2015
                if (GetForegroundWindow() != this.handle) d3hot_Deactivate(null, null);

                mouseKeyEventProvider1.Enabled = true; 
                
                multikeys = Settings.Default.chb_mpress;
                hold = new int[] { 0, 0, 0, 0, 0, 0 }; //17.03.2015

                if (nud_key_delay_ms.Value>0) delay_press_interval = Convert.ToInt32(nud_key_delay_ms.Value);

                //pause = cb_pause.SelectedIndex; //Tp, Enter, All

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
                //b_opt.Enabled = false;
                foreach (Button b in this.Controls.OfType<Button>()) b.Enabled = false; //13.08.2015

                if ((nud_tmr1.Value > 0 || cdr_key[0] == 1 || chb_key1.Checked) && cb_trig_tmr1.SelectedIndex > 0 && cb_key1.SelectedIndex > 0)
                {
                    if (lb_tmr1_sec.Text != lng.cb_tmr2 &&  lb_tmr1_sec.Text != lng.cb_tmr3)
                        lb_tmr1_sec.Text = Math.Round((nud_tmr1.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr_i[0] = Convert.ToDouble(nud_tmr1.Value);
                    trig[0] = cb_trig_tmr1.SelectedIndex;
                    //key1 = cb_key1.SelectedIndex;
                    key_codes(1);
                    tmr_f[0] = 1;
                }
                if ((nud_tmr2.Value > 0 || cdr_key[1] == 1 || chb_key2.Checked) && cb_trig_tmr2.SelectedIndex > 0 && cb_key2.SelectedIndex > 0)
                {
                    if (lb_tmr2_sec.Text != lng.cb_tmr2 && lb_tmr2_sec.Text != lng.cb_tmr3)
                        lb_tmr2_sec.Text = Math.Round((nud_tmr2.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr_i[1] = Convert.ToDouble(nud_tmr2.Value);
                    trig[1] = cb_trig_tmr2.SelectedIndex;
                    //key2 = cb_key2.SelectedIndex;
                    key_codes(2);
                    tmr_f[1] = 1;
                }

                if ((nud_tmr3.Value > 0 || cdr_key[2] == 1 || chb_key3.Checked) && cb_trig_tmr3.SelectedIndex > 0 && cb_key3.SelectedIndex > 0)
                {
                    if (lb_tmr3_sec.Text != lng.cb_tmr2 && lb_tmr3_sec.Text != lng.cb_tmr3)
                        lb_tmr3_sec.Text = Math.Round((nud_tmr3.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr_i[2] = Convert.ToDouble(nud_tmr3.Value);
                    trig[2] = cb_trig_tmr3.SelectedIndex;
                    //key3 = cb_key3.SelectedIndex;
                    key_codes(3);
                    tmr_f[2] = 1;
                }

                if ((nud_tmr4.Value > 0 || cdr_key[3] == 1 || chb_key4.Checked) && cb_trig_tmr4.SelectedIndex > 0 && cb_key4.SelectedIndex > 0)
                {
                    if (lb_tmr4_sec.Text != lng.cb_tmr2 && lb_tmr4_sec.Text != lng.cb_tmr3)
                    lb_tmr4_sec.Text = Math.Round((nud_tmr4.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr_i[3] = Convert.ToDouble(nud_tmr4.Value);
                    trig[3] = cb_trig_tmr4.SelectedIndex;
                    //key4 = cb_key4.SelectedIndex;
                    key_codes(4);
                    tmr_f[3] = 1;
                }
                if ((nud_tmr5.Value > 0 || cdr_key[4] == 1 || chb_key5.Checked) && cb_trig_tmr5.SelectedIndex > 0 && cb_key5.SelectedIndex > 0)
                {
                    if (lb_tmr5_sec.Text != lng.cb_tmr2 && lb_tmr5_sec.Text != lng.cb_tmr3)
                        lb_tmr5_sec.Text = Math.Round((nud_tmr5.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr_i[4] = Convert.ToDouble(nud_tmr5.Value);
                    trig[4] = cb_trig_tmr5.SelectedIndex;
                    //key5 = cb_key5.SelectedIndex;
                    key_codes(5);
                    tmr_f[4] = 1;
                }
                if ((nud_tmr6.Value > 0 || cdr_key[5] == 1 || chb_key6.Checked) && cb_trig_tmr6.SelectedIndex > 0 && cb_key6.SelectedIndex > 0)
                {
                    if (lb_tmr6_sec.Text != lng.cb_tmr2 && lb_tmr6_sec.Text != lng.cb_tmr3)
                        lb_tmr6_sec.Text = Math.Round((nud_tmr6.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr_i[5] = Convert.ToDouble(nud_tmr6.Value);
                    trig[5] = cb_trig_tmr6.SelectedIndex;
                    //key6 = cb_key6.SelectedIndex;
                    key_codes(6);
                    tmr_f[5] = 1;
                }

                //bool coold = false;
                //for (int i = 0; i < 6; i++)
                //    if (trig[i] > 0 && cdr_key[i] > 0)
                //    {
                //        coold = true;
                //        break;
                //    }
                //if (coold)
                //    screen_capt_pre();//screen_capt_prereq(cdr_key);

                
                if (tmr_f[0] + tmr_f[1] + tmr_f[2] + tmr_f[3] + tmr_f[4] + tmr_f[5] > 0)
                {
                    timer_load(-1);
                    tmr_all_counter = 0;
                    if (!tmr_all.Enabled) tmr_all.Enabled = true;
                }
            }
            else //Остановили работу программы
            {
                tmr_cdr_destroy();

                //MessageBox.Show(tmr_all_count.ToString());
                //test_sw.Reset(); //07.07.2015
                if (return_watch != null && return_watch.IsRunning)
                    return_watch.Stop();

                //if (Form.ActiveForm != this) d3hot_Deactivate(null, null);

                mouseKeyEventProvider1.Enabled = false;
                if ((!chb_key1.Checked || !chb_hold.Checked) && cb_tmr1.SelectedIndex < 1) nud_tmr1.Enabled = true;
                if ((!chb_key2.Checked || !chb_hold.Checked) && cb_tmr2.SelectedIndex < 1) nud_tmr2.Enabled = true;
                if ((!chb_key3.Checked || !chb_hold.Checked) && cb_tmr3.SelectedIndex < 1) nud_tmr3.Enabled = true;
                if ((!chb_key4.Checked || !chb_hold.Checked) && cb_tmr4.SelectedIndex < 1) nud_tmr4.Enabled = true;
                if ((!chb_key5.Checked || !chb_hold.Checked) && cb_tmr5.SelectedIndex < 1) nud_tmr5.Enabled = true;
                if ((!chb_key6.Checked || !chb_hold.Checked) && cb_tmr6.SelectedIndex < 1) nud_tmr6.Enabled = true;

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
                //b_opt.Enabled = true;
                foreach (Button b in this.Controls.OfType<Button>()) b.Enabled = true; //13.08.2015

                proc_selected = false; //Отменяем выбор процесса после остановки

                cb_start.Text = "Start";
                tt_start.SetToolTip(cb_start, lng.tt_start);

                //timer_unload(99);
                if (tmr_all != null) timer_unload(-1);
                if (tmr[0] != null) timer_unload(0);
                if (tmr[1] != null) timer_unload(1);
                if (tmr[2] != null) timer_unload(2);
                if (tmr[3] != null) timer_unload(3);
                if (tmr[4] != null) timer_unload(4);
                if (tmr[5] != null) timer_unload(5);

                hold_clear(88);

                //tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0; tmr5_f = 0; tmr6_f = 0;
                //tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                //tmr_i[0] = 0; tmr_i[1] = 0; tmr_i[2] = 0; tmr_i[3] = 0; tmr_i[4] = 0; tmr_i[5] = 0;

                //Lang();
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
            lang_load = true;

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

            if (lb_tmr1_sec.Text.Contains("..")) lb_tmr1_sec.Text = lng.lb_tmr_sec;
            if (lb_tmr2_sec.Text.Contains("..")) lb_tmr2_sec.Text = lng.lb_tmr_sec;
            if (lb_tmr3_sec.Text.Contains("..")) lb_tmr3_sec.Text = lng.lb_tmr_sec;
            if (lb_tmr4_sec.Text.Contains("..")) lb_tmr4_sec.Text = lng.lb_tmr_sec;
            if (lb_tmr5_sec.Text.Contains("..")) lb_tmr5_sec.Text = lng.lb_tmr_sec;
            if (lb_tmr6_sec.Text.Contains("..")) lb_tmr6_sec.Text = lng.lb_tmr_sec;

            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("cb_tmr"))
                {
                    int i = cb.SelectedIndex;
                    cb.Items.Clear();
                    cb.Items.Add(lng.cb_tmr1);
                    if (resolution) cb.Items.Add(lng.cb_tmr2);
                    //MessageBox.Show(resolution.ToString());
                    if (!hold_key.Any(item => item == 1)) cb.Items.Add(lng.cb_tmr3);
                    cb.SelectedIndex = i;
                }
            }

            lb_key_delay_desc.Text = lng.lb_tmr_sec;
            lb_nud_rand.Text = lng.lb_tmr_sec;
            lb_rand.Text = lng.lb_rand;
            lb_hot_prof.Text = lng.lb_hot_prof;

            //lb_about.Text = lng.lb_about;
            lb_area.Text = lng.lb_area;
            lb_proc.Text = lng.lb_proc;
            lb_returndelay.Text = lng.lb_returndelay;
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
            
            //chb_ver_check.Text = lng.chb_ver_check;
            lb_ver_check.Text = lng.chb_ver_check;
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
                case 4: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof4.xml"); break;
                case 5: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof5.xml"); break;
                case 6: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof6.xml"); break;
            }

            if (path != "" && File.Exists(path)) ReadXML(path);
            else if (path != "") Save_settings(1); //08.04.2015

            //cb_key_delay_SelectedIndexChanged(null, null);
            //cb_map_SelectedIndexChanged(null, null);
            //cb_tp_SelectedIndexChanged(null, null);

            //cb_hot_prof.SelectedIndex = k;

            //if (pan_proc.Visible == true) cb_proc.SelectedIndex = -1;  //14.05.2015
            
            //if (!chb_proconly.Checked && cb_proc.SelectedIndex < 1) cb_prog.Enabled = true; //17.04.2015
            //cb_prog_SelectionChangeCommitted(null, null); //17.04.2015
            chb_hold_CheckedChanged(null, null);


            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("cb_tmr"))
                {
                    cb_tmr_SelectedIndexChanged(cb, null);
                }
            }


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
            tp_key = "";
            if (cb_tp.SelectedIndex > 0 && (string)cb_tp.Items[cb_tp.SelectedIndex] != "") //24.04.2015
                tp_key = (string)cb_tp.Items[cb_tp.SelectedIndex].ToString();
            if (tp_key.Length>0 && tp_key.Substring(0, 1) == "*") tp_key = tp_key.Remove(0, 1);
        }

        /// <summary>
        /// Метод установки задержки приостановки (при телепорте)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_tpdelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            tp_delay = 0;
            if (cb_tpdelay.SelectedIndex > 0) tp_delay = Convert.ToInt32(cb_tpdelay.Items[cb_tpdelay.SelectedIndex]);
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
                    if (lb_debug.Visible && p.ProcessName.ToLower().Contains("dllhost")) //For Debugging //25.06.2015 //akelpad
                    {
                        cb_proc.Items.Add("картинка" + " " + p.Id.ToString());
                        proc_handle.Add("картинка" + " " + p.Id.ToString(), p.MainWindowHandle);
                    }
                    if (lb_debug.Visible && (p.ProcessName.ToLower().Contains("akelpad") || p.ProcessName.ToLower().Contains("notepad"))) //For Debugging //25.06.2015 //akelpad
                    {
                        cb_proc.Items.Add("блокнот" + " " + p.Id.ToString());
                        proc_handle.Add("блокнот" + " " + p.Id.ToString(), p.MainWindowHandle);
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

                if (lb_debug.Visible && cb_proc.Text.Contains("блокнот"))
                {
                    handle = FindWindow(null, "akelpad");
                    handle = FindWindowEx(handle, IntPtr.Zero, "AkelEditW", null);  //For debugging
                    if (handle == IntPtr.Zero) handle = FindWindow(null, "notepad");
                }

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
            if (frm_key_input != null) frm_key_input.Hide();
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (form_shown && chb_tray.Checked)
                {
                    if (start_main && start_opt)
                    {
                        this.Hide();
                        notify_d3h.Visible = true;
                    }
                }
                else if (!form_shown && Settings.Default.chb_tray == 1)
                {
                    this.Hide();
                    notify_d3h.Visible = true;
                }
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
            var cb = sender as ComboBox;

            if (cb != null)
                switch (cb.Name)
                {
                    case "cb_map":
                        cb_map_SelectedIndexChanged(null, null);
                        break;
                    case "cb_tp":
                        cb_tp_SelectedIndexChanged(null, null);
                        break;
                    case "cb_key_delay":
                        cb_key_delay_SelectedIndexChanged(null, null);
                        break;
                }
                
            if (opt_click == 1) opt_change = 1;

            if (cb != null && (cb.Name == "cb_key_delay" || cb.Name == "cb_tp" || cb.Name == "cb_map"))
                key_choose_SelectionChangeCommitted(cb, null);

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

            if (cb_trig_tmr1.SelectedIndex > 0 && (nud_tmr1.Value != 0 || chb_key1.Checked || cb_tmr1.SelectedIndex > 0)) nulls[0] = 1;
            if (cb_trig_tmr2.SelectedIndex > 0 && (nud_tmr2.Value != 0 || chb_key2.Checked || cb_tmr2.SelectedIndex > 0)) nulls[1] = 1;
            if (cb_trig_tmr3.SelectedIndex > 0 && (nud_tmr3.Value != 0 || chb_key3.Checked || cb_tmr3.SelectedIndex > 0)) nulls[2] = 1;
            if (cb_trig_tmr4.SelectedIndex > 0 && (nud_tmr4.Value != 0 || chb_key4.Checked || cb_tmr4.SelectedIndex > 0)) nulls[3] = 1;
            if (cb_trig_tmr5.SelectedIndex > 0 && (nud_tmr5.Value != 0 || chb_key5.Checked || cb_tmr5.SelectedIndex > 0)) nulls[4] = 1;
            if (cb_trig_tmr6.SelectedIndex > 0 && (nud_tmr6.Value != 0 || chb_key6.Checked || cb_tmr6.SelectedIndex > 0)) nulls[5] = 1;

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

            
            if (i == 1 &&
                ((tp_key.Length > 0 && (tp_key == key_delay || tp_key == map_key))
                || (map_key.Length > 0 && map_key == key_delay))
                )
                hot_tpmap = 1;

            //if (
            //    (cb_tp.SelectedIndex == cb_map.SelectedIndex && cb_tp.SelectedIndex > 0 && !((string)cb_tp.SelectedItem).Contains("*"))
            //    || (tp_key.Length > 0 && map_key.Length > 0 && tp_key == map_key)
            //    ) hot_tpmap = 1;

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
                        lb_key_delay.Font = new Font(lb_key_delay.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else//Хоткеи карты/телепорта не пересекаются
                    {
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Regular);
                        lb_tp.Font = new Font(lb_tp.Font, FontStyle.Regular);
                        lb_key_delay.Font = new Font(lb_key_delay.Font, FontStyle.Regular);
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
                        lb_key_delay.Font = new Font(lb_key_delay.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else//Хоткеи карты/телепорта не пересекаются
                    {
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Regular);
                        lb_tp.Font = new Font(lb_tp.Font, FontStyle.Regular);
                        lb_key_delay.Font = new Font(lb_key_delay.Font, FontStyle.Regular);
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
            key_delay = "";
            if (cb_key_delay.SelectedIndex > 0 && (string)cb_key_delay.Items[cb_key_delay.SelectedIndex] != "")
            {
                key_delay = (string)cb_key_delay.Items[cb_key_delay.SelectedIndex].ToString();
                if (key_delay == "1" || key_delay == "2" || key_delay == "3" || key_delay == "4") key_delay = "D" + key_delay;
                nud_key_delay_ms.Enabled = true;
                if (key_delay.Length > 0 && key_delay.Substring(0, 1) == "*") key_delay = key_delay.Remove(0, 1);
            }
            else
            {
                nud_key_delay_ms.Value = 0;
                nud_key_delay_ms.Enabled = false;
                //key_delay = "";
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
                hold_use = true;
                //for (int k = 1; k < 7; k++)
                //{
                //    cb_key_del(k);
                //}
            }
            else
            {
                hold_use = false;
                if (chb_hold.Checked)
                    foreach (CheckBox cheb in this.pan_main.Controls.OfType<CheckBox>())
                        cheb.Visible = true;


                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                {
                    if (cb.Name.Contains("tmr") && !cb.Name.Contains("trig")) cb.Visible = true;
                    cb_tmr_SelectedIndexChanged(cb, null);
                }

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
                    {
                        nud_tmr1.Enabled = false;
                        cb_tmr1.Enabled = false;
                    }
                    else if (cb_tmr1.SelectedIndex < 1)
                    {
                        nud_tmr1.Enabled = true;
                        cb_tmr1.Enabled = true;
                    }
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
                    {
                        nud_tmr2.Enabled = false;
                        cb_tmr2.Enabled = false;
                    }
                    else if (cb_tmr2.SelectedIndex < 1)
                    {
                        nud_tmr2.Enabled = true;
                        cb_tmr2.Enabled = true;
                    }
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
                    {
                        nud_tmr3.Enabled = false;
                        cb_tmr3.Enabled = false;
                    }
                    else if (cb_tmr3.SelectedIndex < 1)
                    {
                        nud_tmr3.Enabled = true;
                        cb_tmr3.Enabled = true;
                    }
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
                    {
                        nud_tmr4.Enabled = false;
                        cb_tmr4.Enabled = false;
                    }
                    else if (cb_tmr4.SelectedIndex < 1)
                    {
                        nud_tmr4.Enabled = true;
                        cb_tmr4.Enabled = true;
                    }
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
                    {
                        nud_tmr5.Enabled = false;
                        cb_tmr5.Enabled = false;
                    }
                    else if (cb_tmr5.SelectedIndex < 1)
                    {
                        nud_tmr5.Enabled = true;
                        cb_tmr5.Enabled = true;
                    }
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
                    {
                        nud_tmr6.Enabled = false;
                        cb_tmr6.Enabled = false;
                    }
                    else if (cb_tmr6.SelectedIndex < 1)
                    {
                        nud_tmr6.Enabled = true;
                        cb_tmr6.Enabled = true;
                    }
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
            string[] keys = new string[] { "cb_key1", "cb_key2", "cb_key3", "cb_key4", "cb_key5", "cb_key6", "cb_tp", "cb_map", "cb_key_delay" };
            string[] vals = new string[] { Settings.Default.cb_key1_desc, Settings.Default.cb_key2_desc, Settings.Default.cb_key3_desc, 
                Settings.Default.cb_key4_desc, Settings.Default.cb_key5_desc, Settings.Default.cb_key6_desc, 
                Settings.Default.cb_tp_desc, Settings.Default.cb_map_desc, Settings.Default.cb_key_delay_desc};

            for (int j = 0; j < 9; j++)
            {
                if (cb.Name.Contains(keys[j]) && vals[j] != null && vals[j].Length > 1) 
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
                
                //14.07.2015
                //foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>())
                //{
                //    if (chb.Name != "cb_start")
                //    {
                //        chb.Visible = true;
                //        chb_key_CheckedChanged(chb, null);
                //        //if (chb.Checked && cb_proc.SelectedIndex < 1) pan_hold.Visible = true;
                //    }
                //}
                //check_only();//14.07.2015
                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                {
                    if (cb.Name.Contains("tmr") && !cb.Name.Contains("trig"))
                    {
                        cb.Visible = true;
                    }
                }

                //foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                //{
                //    if (cb.Name.Contains("tmr") && !cb.Name.Contains("trig")) cb.Visible = true;
                //    cb_tmr_SelectedIndexChanged(cb, null);
                //}

                //if (cb_key1.FindString("Shift+LM") > 1) 
                    cb_key_del(0);

            }
            else 
            {
                handle = IntPtr.Zero;
                pan_proc.Visible = false;
                pan_prog.Visible = true;
                d3proc = false;

                //14.07.2015
                //foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>())
                //{
                //    if (chb.Name != "cb_start")
                //    {
                //        chb.Checked = false;
                //        chb.Visible = false;
                //        chb_key_CheckedChanged(chb, null);
                //    }
                //}

                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                {
                    if (cb.Name.Contains("tmr") && !cb.Name.Contains("trig"))
                    {
                        cb.SelectedIndex = -1;
                        cb.Visible = false;
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
            String[] profiles = new String[] { Settings.Default.profile1, Settings.Default.profile2, Settings.Default.profile3, 
                                                Settings.Default.profile4, Settings.Default.profile5, Settings.Default.profile6 };

            //lb_lang_name.Focus();
            if (tb_prof_name.Text == "")
                tb_prof_name.Text = Settings.Default.tb_prof_name;
            else
            //if (tb_prof_name.Text != Settings.Default.profile1 && 
            //    tb_prof_name.Text != Settings.Default.profile2 && 
            //    tb_prof_name.Text != Settings.Default.profile3)
            if (!profiles.Contains (tb_prof_name.Text))
            {
                //Save_settings(0);

                int j = 0;
                string prof_name = tb_prof_name.Text;
                lng.Lang_eng();
                if (lng.tb_prof_name == tb_prof_name.Text) j++;
                lng.Lang_rus();
                if (lng.tb_prof_name == tb_prof_name.Text) j++;
                if (prof_name.Length > 15) prof_name = prof_name.Substring(0, 15);
                
                if (j == 0)
                    switch (cb_prof.SelectedIndex)
                    {
                        case 1:
                            Settings.Default.profile1 = prof_name;
                            break;
                        case 2:
                            Settings.Default.profile2 = prof_name;
                            break;
                        case 3:
                            Settings.Default.profile3 = prof_name;
                            break;
                        case 4:
                            Settings.Default.profile4 = prof_name;
                            break;
                        case 5:
                            Settings.Default.profile5 = prof_name;
                            break;
                        case 6:
                            Settings.Default.profile6 = prof_name;
                            break;
                    }
                
                //if (j == 0 && cb_prof.SelectedIndex == 1) Settings.Default.profile1 = prof_name;
                //else
                //    if (j == 0 && cb_prof.SelectedIndex == 2) Settings.Default.profile2 = prof_name;
                //    else
                //        if (j == 0 && cb_prof.SelectedIndex == 3) Settings.Default.profile3 = prof_name;

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
            map_delay = 0;
            if (cb_mapdelay.SelectedIndex > 0) map_delay = Convert.ToInt32(cb_mapdelay.Items[cb_mapdelay.SelectedIndex]);
        }

        private void cb_mapdelay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (opt_click == 1) opt_change = 1;
        }

        private void cb_returndelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            return_delay = 0;
            if (cb_returndelay.SelectedIndex > 0) return_delay = Convert.ToInt32(cb_returndelay.Items[cb_returndelay.SelectedIndex]);
        }

        private void cb_map_SelectedIndexChanged(object sender, EventArgs e)
        {
            map_key = "";
            if (cb_map.SelectedIndex > 0 && (string)cb_map.Items[cb_map.SelectedIndex] != "") //24.04.2015
                map_key = (string)cb_map.Items[cb_map.SelectedIndex].ToString();
            if (map_key.Length > 0 && map_key.Substring(0, 1) == "*") map_key = map_key.Remove(0, 1);
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
            if (cb != null && cb.Name != "cb_key_delay" && cb.Name != "cb_tp" && cb.Name != "cb_map")
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
            //if (this.WindowState != FormWindowState.Minimized) mouseKeyEventProvider2.Enabled = true; //deactivated = true; //30.06.2015
            if (!cb_start.Checked)
            {
                UnregisterHotKey(this.Handle, 1);
                UnregisterHotKey(this.Handle, 2);
                UnregisterHotKey(this.Handle, 3);
            }
        }

        private void d3hot_Activated(object sender, EventArgs e)
        {
            //mouseKeyEventProvider2.Enabled = false; //deactivated = false; //30.06.2015
            d3hot_KeyUp(null, null);
            if (cb_hot_prof.SelectedIndex > 0) 
                cb_hot_prof_SelectedIndexChanged(null, null);
        }

        private void lb_ver_check_Click(object sender, EventArgs e)
        {
            var send = (Label)sender;
            ver_click = "";
            if (send != null) ver_click = send.Name;

            System.Timers.Timer ver_timer = new System.Timers.Timer();
            ver_timer.AutoReset = false;
            ver_timer.Interval = 10;
            ver_timer.Elapsed += ver_timer_Tick;
            ver_timer.Start();
        }

        public void ver_timer_Tick(object sender, EventArgs eventArgs)
        {


            //WebRequest request = WebRequest.Create("http://csharpindepth.com/asd");
            ////HttpWebRequest.DefaultMaximumErrorResponseLength = 100;
            //try
            //{
            //    using (WebResponse response = request.GetResponse())
            //    {
            //        MessageBox.Show("Won't get here");
            //    }
            //}
            //catch (WebException e)
            //{
            //    using (WebResponse response = e.Response)
            //    {
            //        HttpWebResponse httpResponse = (HttpWebResponse)response;
            //        MessageBox.Show("Error code: " + httpResponse.StatusCode.ToString());
            //        using (Stream data = response.GetResponseStream())
            //        using (var reader = new StreamReader(data))
            //        {
            //            string text = reader.ReadToEnd();
            //            MessageBox.Show(text);
            //        }
            //    }
            //}


            //return;



            //HttpWebRequest req_ver = (HttpWebRequest)WebRequest.Create(@"https://github.com/DmitryOlenin/D3Hot");
            //HttpWebResponse resp1 = null;

            System.Net.WebRequest req_ver = System.Net.WebRequest.Create(@"https://github.com/DmitryOlenin/D3Hot");
            System.Net.WebResponse resp1 = null;

            //var proxy = System.Net.HttpWebRequest.GetSystemWebProxy();
            //Uri proxyUri = proxy.GetProxy(new Uri("http://www.google.com"));


            //if (proxyUri.Authority.ToString() != "www.google.com")
            //{
            //    WebProxy myproxy = new WebProxy(proxyUri.Host, proxyUri.Port);
            //    //myproxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            //    myproxy.BypassProxyOnLocal = true;
            //    myproxy.UseDefaultCredentials = true;
            //    req_ver.Proxy = myproxy;
            //}

            //IWebProxy myProxy = WebRequest.DefaultWebProxy;
            //myProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            //req_ver.Proxy = myProxy;

            bool ver_ok = true;

            try
            {
                resp1 = (HttpWebResponse)req_ver.GetResponse(); //req_ver.GetResponse();
            }
            catch 
            {
                ver_ok = false;
            }

            if (ver_ok)
            {
                System.IO.Stream stream1 = resp1.GetResponseStream();
                System.IO.StreamReader sr1 = new System.IO.StreamReader(stream1);
                string version = sr1.ReadToEnd();
                string[] pars_ver = version.Split('\n');  //парсим строку и получаем стринговый массив

                for (int i = 0; i < pars_ver.Length; i++)
                {
                    if (pars_ver[i].Contains("Diablo3 Hotkeys"))
                    {
                        string vers = pars_ver[i].Substring(pars_ver[i].IndexOf("title=\"Diablo3 Hotkeys") + 23, 3).Trim();
                        double new_ver = 0;
                        try { new_ver = Convert.ToDouble(vers.Replace(".", sep)); }
                        catch 
                        {
                            MessageBox.Show(lng.ver_err_nover, lng.ver_cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        if (vers != string.Format("{0:F1}", ver).ToString().Trim().Replace(sep, ".") && new_ver > ver)
                        {
                            if (ver_click == "lb_ver_check")
                            {

                                if (MessageBox.Show(lng.download, lng.new_ver + vers, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000 //0x40000 is the "MB_TOPMOST"-Flag.
                                ) == DialogResult.Yes)
                                    System.Diagnostics.Process.Start("http://bit.ly/d3hotkeys");
                            }
                            else
                                if (MessageBox.Show(new Form(), //Пустая форма, чтобы не было ничего в панели задач
                                lng.download, lng.new_ver + vers, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                                ) == DialogResult.Yes)
                                    System.Diagnostics.Process.Start("http://bit.ly/d3hotkeys");
                        }
                        else
                            if (ver_click == "lb_ver_check") MessageBox.Show(lng.no_new, lng.ver_cap, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        //MessageBox.Show(vers);
                        break;
                    }
                }
                stream1.Dispose();
                sr1.Dispose();
            }
            else
                MessageBox.Show(lng.ver_err_open + "https://github.com/DmitryOlenin/D3Hot", lng.ver_cap, MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (resp1 != null) resp1.Close();
        }

        private void d3hot_KeyUp(object sender, KeyEventArgs e)
        {
            //1: Shift, 2: Scroll, 3: Caps, 4: Num
            if (e != null)
            {
                if (e.KeyData == Keys.Scroll)
                    if ((key_press(2) && !mouseKeyEventProvider2.Enabled) || (mouseKeyEventProvider2.Enabled && !key_press(2))) lb_scroll.Image = Properties.Resources.ind_lock;
                    else lb_scroll.Image = null;
                else if (e.KeyData == Keys.CapsLock)
                    if ((key_press(3) && !mouseKeyEventProvider2.Enabled) || (mouseKeyEventProvider2.Enabled && !key_press(3))) lb_caps.Image = Properties.Resources.ind_lock;
                    else lb_caps.Image = null;
                else if (e.KeyData == Keys.NumLock)
                    if ((key_press(4) && !mouseKeyEventProvider2.Enabled) || (mouseKeyEventProvider2.Enabled && !key_press(4))) lb_num.Image = Properties.Resources.ind_lock;
                    else lb_num.Image = null;
            }
            else
            {
                if (key_press(2)) lb_scroll.Image = Properties.Resources.ind_lock;
                else lb_scroll.Image = null;
                if (key_press(3)) lb_caps.Image = Properties.Resources.ind_lock;
                else lb_caps.Image = null;
                if (key_press(4)) lb_num.Image = Properties.Resources.ind_lock;
                else lb_num.Image = null;
            }
        }

        private void lb_locks_Click(object sender, EventArgs e)
        {
            var lb = (Label)sender;
            switch (lb.Name)
            {
                case "lb_scroll":
                    inp.Keyboard.KeyPress((VirtualKeyCode)Keys.Scroll);
                    break;
                case "lb_caps":
                    inp.Keyboard.KeyPress((VirtualKeyCode)Keys.CapsLock);
                    break;
                case "lb_num":
                    inp.Keyboard.KeyPress((VirtualKeyCode)Keys.NumLock);
                    break;
            }
        }

        private void mouseKeyEventProvider2_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyData == Keys.Scroll || e.KeyData == Keys.CapsLock || e.KeyData == Keys.NumLock)
                    d3hot_KeyUp(sender, e);
        }

        /// <summary>
        /// Остановка и сброс интервала Stopwatch
        /// </summary>
        /// <param name="watch"></param>
        static void StopWatch(Stopwatch watch)
        {
            if (watch != null) watch.Reset();
        }
        /// <summary>
        /// Запуск нового Stopwatch
        /// </summary>
        /// <param name="watch"></param>
        static void RestartWatch(ref Stopwatch watch)
        {
            watch = Stopwatch.StartNew();
        }


        private void d3hot_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                cb_trig_tmr1.SelectedIndex = 0;
                cb_trig_tmr2.SelectedIndex = 0;
                cb_trig_tmr3.SelectedIndex = 0;
                cb_trig_tmr4.SelectedIndex = 0;
                cb_trig_tmr5.SelectedIndex = 0;
                cb_trig_tmr6.SelectedIndex = 0;
                error_select();
            }
        }

        private void cb_tmr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lang_load)
            {
                var cb = (ComboBox)sender;
                string name = cb.Name;

                ComboBox[] cb_tmr = new ComboBox[] { cb_tmr1, cb_tmr2, cb_tmr3, cb_tmr4, cb_tmr5, cb_tmr6 }; //15.07.2015
                NumericUpDown[] nud_tmr = new NumericUpDown[] { nud_tmr1, nud_tmr2, nud_tmr3, nud_tmr4, nud_tmr5, nud_tmr6 }; //15.07.2015
                Label[] lb_tmr_sec = new Label[] { lb_tmr1_sec, lb_tmr2_sec, lb_tmr3_sec, lb_tmr4_sec, lb_tmr5_sec, lb_tmr6_sec }; //15.07.2015

                int num = -1;
                for (int i = 0; i < 6; i++)
                {
                    if (cb_tmr[i].Name == cb.Name) num = i;
                }

                //check_only();
                if (resolution)  //Разрешение 16:10 или 16:9
                {

                    nud_tmr[num].Enabled = false;
                    cdr_key[num] = 0;
                    switch (cb.SelectedIndex)
                    {
                        case 1:
                            cdr_key[num] = 1;
                            lb_tmr_sec[num].Text = lng.cb_tmr2;
                            break;
                        case 2:
                            lb_tmr_sec[num].Text = lng.cb_tmr3;
                            break;
                        default:
                            nud_Leave(nud_tmr[num], null);
                            nud_tmr[num].Enabled = true;
                            break;
                    }



                    if (cb.SelectedIndex == 2)
                    {
                        cdr_del(cb);
                        Array.Clear(hold_key, 0, 6);
                        hold_key[num] = 1;
                    }
                    else
                    {
                        hold_key[num] = 0;
                        if (!hold_key.Any(item => item == 1)) cdr_add();
                    }
                }
                else //Разрешение не 16:10 или 16:9
                {
                    //MessageBox.Show(cb_tmr[1].Name + " / " + cb_tmr[2].Name + " Num:" + num.ToString());

                    nud_tmr[num].Enabled = false;
                    switch (cb.SelectedIndex)
                    {
                        case 1:
                            lb_tmr_sec[num].Text = lng.cb_tmr3;
                            break;
                        default:
                            nud_Leave(nud_tmr[num], null);
                            nud_tmr[num].Enabled = true;
                            break;
                    }

                    if (cb.SelectedIndex == 1)
                    {
                        cdr_del(cb);
                        Array.Clear(hold_key, 0, 6);
                        hold_key[num] = 1;
                    }
                    else 
                    {
                        hold_key[num] = 0;
                        if (!hold_key.Any(item => item == 1)) cdr_add();
                    }

                }
                
                error_select();
            }
        }

        private void cb_tmr_DrawItem(object sender, DrawItemEventArgs e)
        {
            var cb = (ComboBox)sender;

            int pos = -1;
            if (cb.SelectedIndex > -1) pos = cb.SelectedIndex;


            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            Font ft = cb.Font;
            e.Graphics.DrawString(cb.Items[e.Index].ToString(), ft, myBrush, e.Bounds, StringFormat.GenericDefault);
            
            // Draw the focus rectangle if the mouse hovers over an item.
            if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
                e.DrawFocusRectangle();

            cb.SelectedIndex = pos;
        }

        public void refreshList(List<string> list, ComboBox cb)
        {
            cb.DataSource = null;
            cb.DataSource = list;
        }

        private void cdr_only()
        {
            tmr_holding = false;
            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("cb_tmr") && ((resolution && cb.SelectedIndex == 2) || (!resolution && cb.SelectedIndex == 1)))
                {
                    tmr_holding = true;
                    break;
                }
            }

        }

        private void cdr_del(ComboBox input)
        {
            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("cb_tmr") && input.Name.Contains("cb_tmr") && cb.Name != input.Name)
                {
                    int sel = !resolution && cb.SelectedIndex > 1 ? cb.SelectedIndex - 1 : cb.SelectedIndex;
   
                    if (resolution && cb.Items.Count > 2)
                        cb.Items.Remove(cb.Items[2]);
                    else if (!resolution && cb.Items.Count > 1)
                    {
                        cb.Items.Remove(cb.Items[1]);
                        sel = 0;
                    }

                    if (cb.SelectedIndex != sel) cb.SelectedIndex = sel;
                }
            }
        }

        private void cdr_add()
        {
            bool res = true;

            foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
            {
                if (cb.Name.Contains("cb_tmr") && (cb.SelectedIndex > 1 || (!resolution && cb.SelectedIndex > 0)))
                    res = false;
            }
            if (res)
            {
                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                {
                    if (cb.Name.Contains("cb_tmr") && ((resolution && cb.Items.Count < 3) || (!resolution && cb.Items.Count < 2)))
                    {
                        cb.Items.Add(lng.cb_tmr3);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cdr_run[0] = 1;
            //screen_capt_pre();
            //screen_capt_prereq(cdr_run);
            //get_picture(cdr_key);
            //cdr_key_check((int)nud_tmr6.Value);
            //get_pic();
            //int[] check_res = check_pic(cdr_run);

            //if (check_res[0] == 0) bmp.Save("test_12.jpg");

            cdr_run[0] = 1;
            cdr_run[1] = 1;
            cdr_run[2] = 1;
            test = 0; test1 = 0;

            System.Timers.Timer test_timer = new System.Timers.Timer();
            test_timer.Interval = 1;
            test_timer.Elapsed += test_timer_Tick;
            //test_timer.AutoReset = false;

            sw = new Stopwatch();
            sw.Start();
            test_timer.Start();

        }

        public void test_timer_Tick(object sender, EventArgs eventArgs)
        {
            var timer = (System.Timers.Timer)sender;

            //lock (valueLocker)
            //    if (cdr_isrun)
            //        return;
            //    else
            //        cdr_isrun = true;



            //if (Monitor.TryEnter(valueLocker, 20))
            //{
            //    try
            //    {
            //lock (valueLocker)
            //{
                    //var timer = (System.Timers.Timer)sender;
                    //test++;
                    //ScreenCapture(cdr_run);
                    //if (test == 1000)
                    //{
                    //    //timer.Stop();
                    //    MessageBox.Show(sw.ElapsedMilliseconds.ToString() + " / " + test1.ToString());
                    //    sw.Reset();
                    //}
            //}
            //    }
            //    finally { Monitor.Exit(valueLocker); }
            //}

                //lock (valueLocker)
                //    cdr_isrun = false;

            if (Monitor.TryEnter(valueLocker, TimeSpan.FromMilliseconds(2000)))
            {
                try
                {
                    test++;

                    if (test > 10)
                    {
                        timer.Stop();
                        MessageBox.Show("Прошло: " + sw.ElapsedMilliseconds.ToString() + " Посчитали: " + test1.ToString());
                        sw.Reset();
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        test1 += 10;
                        //Thread.Sleep(2);
                        
                        //ProcessStartInfo psi = new ProcessStartInfo(); //Имя запускаемого приложения
                        //psi.FileName = "cmd"; //команда, которую надо выполнить       
                        //psi.Arguments = @"/c ping -n 1 127.0.0.1"; //c - после выполнения команды консоль закроется, k - нет
                        //psi.WindowStyle = ProcessWindowStyle.Hidden;
                        //Process.Start(psi);

                    }
                }
                finally { Monitor.Exit(valueLocker); }
            }
            else
            {
                test++;
            }
                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sw = new Stopwatch();
            //Bitmap bmp = null;
            sw.Start();
            //MemoryStream ms = new MemoryStream();

            //ms = PrintWindow();
            //if (ms != null && ms.Length > 0)

            //cdr_key[0] = 1;
            //screen_capt_prereq(cdr_run);
            cdr_run[0] = 0; cdr_run[1] = 0; cdr_run[2] = 1;
            trig[0] = cb_trig_tmr1.SelectedIndex;
            trig[1] = cb_trig_tmr1.SelectedIndex;
            trig[2] = cb_trig_tmr1.SelectedIndex;
            test = 0; test1 = 0;
            tmr = new System.Timers.Timer[6];
            tmr_cdr = new System.Timers.Timer();
            tmr_cdr.Elapsed += tmr_cdr_Elapsed;
            tmr_cdr.AutoReset = false;
            key_codes(1); key_codes(2); key_codes(3);
            screen_capt_pre();


            System.Timers.Timer test_timer = new System.Timers.Timer();
            test_timer.Interval = 15;
            test_timer.Elapsed += Cooldown_Tick;
            test_timer.Start();

            //screen_capt_pre();

            //for (int i = 0; i < 10000; i++)
            //{

            //    using (Bitmap bmp = ScreenShot())//.Save("test444.jpg"); 
            //    {
            //        ScreenFind(cdr_run, bmp);
            //    }

            //    //ScreenCapture (cdr_run);
                

            //}
            //sw.Stop();
            //MessageBox.Show(sw.ElapsedMilliseconds.ToString());
            ////MessageBox.Show(test_times);
            //sw.Reset();
        }

        public void Cooldown_Tick(object sender, EventArgs eventArgs)
        {
            var timer = (System.Timers.Timer)sender;
            
            //test++;
            //if (test > 999)
            //{
            //    timer.Stop();
            //    sw.Stop();
            //    MessageBox.Show("Времени прошло: " + sw.ElapsedMilliseconds.ToString() + ". Выполнилось кода (раз): " + (test1+1).ToString());
            //    sw.Reset();
            //}

            if (Monitor.TryEnter(valueLocker, TimeSpan.FromMilliseconds(10)))
            {
                try
                {
                    if (cdr_run.Any(item => item == 1)) cdr_press = ScreenCapture(cdr_run);

                    for (int i = 0; i < 6; i++) //прожимаем после предыдущего таймера
                    {
                        if (cdr_run[i] > 1) cdr_run[i]--;
                        if (cdr_press[i] == 1 && key_press(trig[i]))
                        {
                            timer_cdr_create(i);
                            cdr_press[i] = 0;
                            cdr_run[i] = 10;
                            Thread.Sleep(10); //Ждём после каждого нажатия 10мс.
                            //test1++;
                        }
                    }

                    //if (cdr_press.Any(item => item == 1))
                    //{
                    //    if (tmr_cdr == null)
                    //    {
                    //        tmr_cdr = new System.Timers.Timer();
                    //        tmr_cdr.Elapsed += tmr_cdr_Elapsed;
                    //        tmr_cdr.AutoReset = false;
                    //    }
                    //    tmr_cdr.Start();
                    //}

                    for (int i = 0; i < 6; i++)
                    {
                        if (tmr_f[i] == 1 && cdr_key[i] == 1)
                            if (key_press(trig[i]) && cdr_run[i] == 0)
                                cdr_run[i] = 1;
                            else if (!key_press(trig[i]))
                                cdr_run[i] = 0;
                    }

                    if (cdr_run.Sum() == 0) timer.Stop();

                }
                finally { Monitor.Exit(valueLocker); }
            }

        }

        private void tmr_cdr_destroy() //21.07.2015
        {
            if (tmr_cdr != null && tmr_cdr.Enabled)
            {
                Array.Clear(cdr_run, 0, 6);
                tmr_cdr.Stop();
            }
        }

    }
}
