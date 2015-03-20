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
using System.Collections.Generic; //11.03.2015 

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public System.Timers.Timer tmr1, tmr2, tmr3, tmr4, tmr5, tmr6, tmr_all;
        public System.Timers.Timer StartTimer1, RepeatTimer1, StartTimer2, RepeatTimer2, StartTimer3, RepeatTimer3,
                                    StartTimer4, RepeatTimer4, StartTimer5, RepeatTimer5, StartTimer6, RepeatTimer6;
        public Boolean shift = false, d3prog = false, d3proc = false;
        public InputSimulator inp = new InputSimulator();
        public int trig1 = 0, trig2 = 0, trig3 = 0, trig4 = 0, trig5 = 0, trig6 = 0,
            key1 = 0, key2 = 0, key3 = 0, key4 = 0, key5 = 0, key6 = 0,
            tmr1_f = 0, tmr2_f = 0, tmr3_f = 0, tmr4_f = 0, tmr5_f = 0, tmr6_f = 0,
            tmr1_r = 0, tmr2_r = 0, tmr3_r = 0, tmr4_r = 0, tmr5_r = 0, tmr6_r = 0,
            pause = 0, prof_prev, tp_delay=0, tmr_all_counter=0
            ,hold_key0 = 0, hold_key1 = 0, hold_key2 = 0, hold_key3 = 0, hold_key4 = 0, hold_key5 = 0
            ,multikeys = 0
            ;

        public double tmr1_i = 0, tmr2_i = 0, tmr3_i = 0, tmr4_i = 0, tmr5_i = 0, tmr6_i = 0;
        public static int t_press = 0, return_press = 0, r_press = 0, return_press_count = 0, delay_press = 0, delay_press_interval = 0; 
        public static string tp_key = "", proc_curr = "", key_delay = "";
        public static SettingsTable overview;
        public int[] hold = new int[] { 0, 0, 0, 0, 0, 0 }; //17.03.2015

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

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
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
                    tmr1.Interval = tmr1_i;
                    break;
                case 2:
                    tmr2 = new System.Timers.Timer();
                    tmr2.Elapsed += tmr_Elapsed;
                    tmr2.Interval = tmr2_i;
                    break;
                case 3:
                    tmr3 = new System.Timers.Timer();
                    tmr3.Elapsed += tmr_Elapsed;
                    tmr3.Interval = tmr3_i;
                    break;
                case 4:
                    tmr4 = new System.Timers.Timer();
                    tmr4.Elapsed += tmr_Elapsed;
                    tmr4.Interval = tmr4_i;
                    break;
                case 5:
                    tmr5 = new System.Timers.Timer();
                    tmr5.Elapsed += tmr_Elapsed;
                    tmr5.Interval = tmr5_i;
                    break;
                case 6:
                    tmr6 = new System.Timers.Timer();
                    tmr6.Elapsed += tmr_Elapsed;
                    tmr6.Interval = tmr6_i;
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
            if (tmr1 != null && (i == 1 || i == 99 || i == 88)) tmr1.Dispose();
            if (tmr2 != null && (i == 2 || i == 99 || i == 88)) tmr2.Dispose();
            if (tmr3 != null && (i == 3 || i == 99 || i == 88)) tmr3.Dispose();
            if (tmr4 != null && (i == 4 || i == 99 || i == 88)) tmr4.Dispose();
            if (tmr5 != null && (i == 5 || i == 99 || i == 88)) tmr5.Dispose();
            if (tmr6 != null && (i == 6 || i == 99 || i == 88)) tmr6.Dispose();
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
            if (e.KeyCode.ToString() == "Return") return_press = 1;
        }

        private void mouseKeyEventProvider1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((key_delay == "LMouse" && e.Button.ToString() == "Left") 
                || (key_delay == "RMouse" && e.Button.ToString() == "Right"))
                    delay_press = 1;
        }

        public d3hot()
        {
            InitializeComponent();
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

                //MessageBox.Show("Hotkey has been pressed!");
                if (id == 0) cb_start.Checked = !cb_start.Checked;
                // do something
            }
        }

        private void d3hot_Load(object sender, EventArgs e)
        {
            this.Icon = D3Hot.Properties.Resources.diablo_hot;
            notify_d3h.Icon = D3Hot.Properties.Resources.diablo_hot;

            Load_settings();

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

            //Установка глобального хоткея запуска/остановки
            reghotkey();            
        }


        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

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
        private VirtualKeyCode virt_code(int i)
        {
            VirtualKeyCode vkc = VirtualKeyCode.VK_0;
            switch (i)
            {
                case 0: vkc = VirtualKeyCode.VK_1; break;
                case 1: vkc = VirtualKeyCode.VK_2; break;
                case 2: vkc = VirtualKeyCode.VK_3; break;
                case 3: vkc = VirtualKeyCode.VK_4; break;
                case 4: vkc = VirtualKeyCode.VK_Q; break;
                case 5: vkc = VirtualKeyCode.VK_W; break;
                case 6: vkc = VirtualKeyCode.VK_E; break;
                case 7: vkc = VirtualKeyCode.VK_R; break;
                case 8: vkc = VirtualKeyCode.VK_A; break;
                case 9: vkc = VirtualKeyCode.VK_S; break;
                case 10: vkc = VirtualKeyCode.VK_D; break;
                case 11: vkc = VirtualKeyCode.VK_F; break;
                case 12: vkc = VirtualKeyCode.VK_Z; break;
                case 13: vkc = VirtualKeyCode.VK_X; break;
                case 14: vkc = VirtualKeyCode.VK_C; break;
                case 15: vkc = VirtualKeyCode.VK_V; break;
                case 16: vkc = VirtualKeyCode.SPACE; break;
                case 17: vkc = VirtualKeyCode.LBUTTON; break;
                case 18: vkc = VirtualKeyCode.RBUTTON; break;
                case 19: vkc = VirtualKeyCode.XBUTTON1; break;
                case 20: vkc = VirtualKeyCode.XBUTTON2; break;
            }
            return vkc;
        }

        /// <summary>
        /// Метод для установки клавиш для залипания.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int key_hold_code(int i)
        {
            int key_code = 0;
            switch (i)
            {
                case 0: key_code = (int)Keys.D1; break;
                case 1: key_code = (int)Keys.D2; break;
                case 2: key_code = (int)Keys.D3; break;
                case 3: key_code = (int)Keys.D4; break;
                case 4: key_code = (int)Keys.Q; break;
                case 5: key_code = (int)Keys.W; break;
                case 6: key_code = (int)Keys.E; break;
                case 7: key_code = (int)Keys.R; break;
                case 8: key_code = (int)Keys.A; break;
                case 9: key_code = (int)Keys.S; break;
                case 10: key_code = (int)Keys.D; break;
                case 11: key_code = (int)Keys.F; break;
                case 12: key_code = (int)Keys.Z; break;
                case 13: key_code = (int)Keys.X; break;
                case 14: key_code = (int)Keys.C; break;
                case 15: key_code = (int)Keys.V; break;
                case 16: key_code = (int)Keys.Space; break;
                case 17: key_code = (int)Keys.LButton; break;
                case 18: key_code = (int)Keys.RButton; break;
            }
            return key_code;
        }

        /// <summary>
        /// Основной метод после запуска процесса - срабатывание главного таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tmr_all_Elapsed(object sender, EventArgs e)
        {
            if (return_press == 1) return_press_count++;
            if (return_press_count > 1) return_press_count = 0; //Обработка количества Enter для работы с Shift

            //Работаем только если хоть что-то из триггеров зажато/переключено.
            if (key_press(trig1) || key_press(trig2) || key_press(trig3) || key_press(trig4) || key_press(trig5) || key_press(trig6))
            {
                //Проверка на одинарное/двойное нажатие Enter.
                if (
                        (pause == 2 || pause == 3) && 
                        (
                            (return_press == 1 && r_press == 0 && t_press == 0) ||
                                (
                                    return_press_count==1 && 
                                    ((trig1==1 && key_press(trig1)) ||
                                    (trig2==1 && key_press(trig2)) ||
                                    (trig3==1 && key_press(trig3)) ||
                                    (trig4==1 && key_press(trig4)) ||
                                    (trig5==1 && key_press(trig5)) ||
                                    (trig6==1 && key_press(trig6)))
                                )
                        )
                    )
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    r_press = 1; return_press = 0;
                    hold_clear(88);
                }

                if (r_press == 1 && return_press == 1)
                {
                    r_press = 0; return_press = 0; t_press = 0;
                }

                //Проверка на нажатие T.
                if ((pause == 1 || pause == 3) && t_press > 0 && tmr_all.Interval == tp_delay * 1000)
                {
                    t_press = 0;
                    tmr_all.Interval = 1;
                    return_press = 0;
                }

                if ((pause == 1 || pause == 3) && t_press > 0 && r_press == 0)
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    hold_clear(88);
                    tmr_all.Interval = tp_delay * 1000;
                }

                if ((pause == 1 || pause == 3) && r_press > 0 && t_press > 0) t_press = 0;

                //16.03.2015 Проверка на клавишу задержки
                if (delay_press > 0 && tmr_all.Interval == delay_press_interval)
                {
                    delay_press = 0;
                    tmr_all.Interval = 1;
                }

                if (delay_press > 0 && delay_press_interval > 0 && r_press == 0)
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    hold_clear(88);
                    tmr_all.Interval = delay_press_interval;
                }

                if ((pause == 2 || pause == 3) && r_press > 0 && delay_press > 0) delay_press = 0;

                //Если T|Enter не нажаты, запускаем таймеры триггеров 1-2-3-4-5-6 при активном состоянии и останавливаем при отключенном.
                if (t_press == 0 && r_press == 0 && delay_press == 0)
                {
                    tmr_all_counter = 3;
                    if (tmr1_f == 1)
                    {
                        if (key_press(trig1))
                        {
                            if (hold_key0 == 1 && hold[0] == 0)  //18.03.2015
                            {
                                hold[0] = 1;
                                key_hold(1);
                            } 
                            else if (tmr1_r == 0 && hold_key0 == 0)
                            {
                                tmr1_r = 1;
                                timer_load(1);
                                tmr_Elapsed(tmr1, null);
                                if (tmr1 != null)
                                    try { tmr1.Enabled = true; }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key0 == 1) hold_clear (0);
                            timer_unload(1);
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
                                key_hold(2);
                            }
                            else if (tmr2_r == 0 && hold_key1 == 0)
                            {
                                tmr2_r = 1;
                                timer_load(2);
                                tmr_Elapsed(tmr2, null);
                                if (tmr2 != null)
                                    try { tmr2.Enabled = true; }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key1 == 1) hold_clear(1);
                            timer_unload(2);
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
                                key_hold(3);
                            }
                            else if (tmr3_r == 0 && hold_key2 == 0)
                            {
                                tmr3_r = 1;
                                timer_load(3);
                                tmr_Elapsed(tmr3, null);
                                if (tmr3 != null)
                                    try { tmr3.Enabled = true; }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key2 == 1) hold_clear(2);
                            timer_unload(3);
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
                                key_hold(4);
                            }
                            else if (tmr4_r == 0 && hold_key3 == 0)
                            {
                                tmr4_r = 1;
                                timer_load(4);
                                tmr_Elapsed(tmr4, null);
                                if (tmr4 != null)
                                    try { tmr4.Enabled = true; }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key3 == 1) hold_clear(3);
                            timer_unload(4);
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
                                key_hold(5);
                            }
                            else if (tmr5_r == 0 && hold_key4 == 0)
                            {
                                tmr5_r = 1;
                                timer_load(5);
                                tmr_Elapsed(tmr5, null);
                                if (tmr5 != null)
                                    try { tmr5.Enabled = true; }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key4 == 1) hold_clear(4);
                            timer_unload(5);
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
                                key_hold(6);
                            }
                            else if (tmr6_r == 0 && hold_key5 == 0)
                            {
                                tmr6_r = 1;
                                timer_load(6);
                                tmr_Elapsed(tmr6, null);
                                if (tmr6 != null)
                                    try { tmr6.Enabled = true; }
                                    catch { }
                            }
                        }
                        else
                        {
                            if (hold_key5 == 1) hold_clear(5);
                            timer_unload(6);
                            tmr6_r = 0;
                        }
                    }

                }
            }
            else
            {
                timer_unload(88);
                return_press = 0; r_press = 0; t_press = 0; tmr_all.Interval = 1; delay_press = 0;
                tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                if (tmr_all_counter>0) 
                {
                    tmr_all_counter--;
                    hold_clear(88);                
                }                
            }
        }

        public void hold_clear(int i)
        {
            if (i < 6)
            {
                if (hold[i] > 0)
                {
                    keyup(i+1); //20.03.2015
                    hold_unload(i + 1);
                    hold[i] = 0;
                }
            }
            else
            {
                for (int j = 0; j < 6; j++)
                {
                    if (hold[j] > 0) 
                        keyup(j + 1);
                }
                for (int j = 0; j < 6; j++)
                {
                    if (hold[j] > 0)
                    {
                        hold_unload(j + 1);
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
            if (GetActiveWindowTitle() != null) title = GetActiveWindowTitle();

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
                (!d3prog || (d3prog && title.ToLower().Contains("diablo")))
                &&
                (!d3proc || (d3proc && proc_right))
                &&
                !title.ToLower().Contains("hotkeys")
               ) result = true;


            return result;

        }
        

        public void tmr_Elapsed(object sender, EventArgs e)
        {
            var tmr = (System.Timers.Timer)sender;
            int mult = 1;
            //Если всё в порядке, нажимаем соответствующую клавишу.
            if (usage_area())
            {
                VirtualKeyCode key = VirtualKeyCode.VK_0;

                if (tmr == tmr1 && key_press(trig1)) key = virt_code(key1); else 
                if (tmr == tmr2 && key_press(trig2)) key = virt_code(key2); else
                if (tmr == tmr3 && key_press(trig3)) key = virt_code(key3); else
                if (tmr == tmr4 && key_press(trig4)) key = virt_code(key4); else
                if (tmr == tmr5 && key_press(trig5)) key = virt_code(key5); else
                if (tmr == tmr6 && key_press(trig6)) key = virt_code(key6);

                if (multikeys != 0) mult = 3;

                for (int i = 0; i < mult; i++)
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
            inp.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
            inp.Keyboard.Sleep(50);
            if (i==1) inp.Mouse.LeftButtonClick(); 
            else inp.Mouse.RightButtonClick();
            inp.Keyboard.Sleep(50);
            inp.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
        }

        /// <summary>
        /// Метод при запуске программы или её остановке (Start/Stop/F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_start_CheckedChanged(object sender, EventArgs e)
        {
            timer_unload(99);
            tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0; tmr5_f = 0; tmr6_f = 0;
            tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
            tmr1_i = 0; tmr2_i = 0; tmr3_i = 0; tmr4_i = 0; tmr5_i = 0; tmr6_i = 0;
            trig1 = 0; trig2 = 0; trig3 = 0; trig4 = 0; trig5 = 0; trig6 = 0;
            hold_key0 = 0; hold_key1 = 0; hold_key2 = 0; hold_key3 = 0; hold_key4 = 0; hold_key5 = 0; //17.03.2015
            t_press = 0; return_press = 0; r_press = 0; delay_press = 0; return_press_count = 0;

            foreach (NumericUpDown numud in this.pan_main.Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;

            int i = 0;

            if (cb_key1.SelectedIndex > 18 || cb_key2.SelectedIndex > 18 || cb_key3.SelectedIndex > 18
                || cb_key4.SelectedIndex > 18 || cb_key5.SelectedIndex > 18 || cb_key6.SelectedIndex > 18)
                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                    if (cb.Name.Contains("trig") && cb.SelectedIndex == 1) cb.SelectedIndex = 0;

            if  ((cb_trig_tmr1.SelectedIndex != 0 && nud_tmr1.Value != 0) ||
                (cb_trig_tmr2.SelectedIndex != 0 && nud_tmr2.Value != 0) ||
                (cb_trig_tmr3.SelectedIndex != 0 && nud_tmr3.Value != 0) ||
                (cb_trig_tmr4.SelectedIndex != 0 && nud_tmr4.Value != 0)  ||
                (cb_trig_tmr5.SelectedIndex != 0 && nud_tmr5.Value != 0)  ||
                (cb_trig_tmr6.SelectedIndex != 0 && nud_tmr6.Value != 0) ) 
                i++;

            if (i == 0 || lb_hold.Visible) cb_start.Checked = false;

            if (cb_start.Checked)
            {
                multikeys = Settings.Default.chb_mpress;
                hold = new int[] { 0, 0, 0, 0, 0, 0 }; //17.03.2015
                if (pan_opt.Visible) b_opt_Click(null, null);
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
                foreach (NumericUpDown numud in this.pan_main.Controls.OfType<NumericUpDown>()) numud.Enabled = false;
                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>()) cb.Enabled = false;
                foreach (ComboBox cbm in this.Controls.OfType<ComboBox>()) cbm.Enabled = false;
                foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>()) chb.Enabled = false;
                b_opt.Enabled = false;

                if (nud_tmr1.Value > 0 && cb_trig_tmr1.SelectedIndex > 0)
                {
                    lb_tmr1_sec.Text = Math.Round((nud_tmr1.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr1_i = Convert.ToDouble(nud_tmr1.Value);
                    trig1 = cb_trig_tmr1.SelectedIndex;
                    key1 = cb_key1.SelectedIndex;
                    tmr1_f = 1;
                }
                if (nud_tmr2.Value > 0 && cb_trig_tmr2.SelectedIndex > 0)
                {
                    lb_tmr2_sec.Text = Math.Round((nud_tmr2.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr2_i = Convert.ToDouble(nud_tmr2.Value);
                    trig2 = cb_trig_tmr2.SelectedIndex;
                    key2 = cb_key2.SelectedIndex;
                    tmr2_f = 1;
                }

                if (nud_tmr3.Value > 0 && cb_trig_tmr3.SelectedIndex > 0)
                {
                    lb_tmr3_sec.Text = Math.Round((nud_tmr3.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr3_i = Convert.ToDouble(nud_tmr3.Value);
                    trig3 = cb_trig_tmr3.SelectedIndex;
                    key3 = cb_key3.SelectedIndex;
                    tmr3_f = 1;
                }

                if (nud_tmr4.Value > 0 && cb_trig_tmr4.SelectedIndex > 0)
                {
                    lb_tmr4_sec.Text = Math.Round((nud_tmr4.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr4_i = Convert.ToDouble(nud_tmr4.Value);
                    trig4 = cb_trig_tmr4.SelectedIndex;
                    key4 = cb_key4.SelectedIndex;
                    tmr4_f = 1;
                }
                if (nud_tmr5.Value > 0 && cb_trig_tmr5.SelectedIndex > 0)
                {
                    lb_tmr5_sec.Text = Math.Round((nud_tmr5.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr5_i = Convert.ToDouble(nud_tmr5.Value);
                    trig5 = cb_trig_tmr5.SelectedIndex;
                    key5 = cb_key5.SelectedIndex;
                    tmr5_f = 1;
                }
                if (nud_tmr6.Value > 0 && cb_trig_tmr6.SelectedIndex > 0)
                {
                    lb_tmr6_sec.Text = Math.Round((nud_tmr6.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    tmr6_i = Convert.ToDouble(nud_tmr6.Value);
                    trig6 = cb_trig_tmr6.SelectedIndex;
                    key6 = cb_key6.SelectedIndex;
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
                if (!chb_key1.Checked) nud_tmr1.Enabled = true;
                if (!chb_key2.Checked) nud_tmr2.Enabled = true;
                if (!chb_key3.Checked) nud_tmr3.Enabled = true;
                if (!chb_key4.Checked) nud_tmr4.Enabled = true;
                if (!chb_key5.Checked) nud_tmr5.Enabled = true;
                if (!chb_key6.Checked) nud_tmr6.Enabled = true;

                //foreach (NumericUpDown numud in this.pan_main.Controls.OfType<NumericUpDown>()) numud.Enabled = true;
                foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>()) cb.Enabled = true;
                foreach (ComboBox cbm in this.Controls.OfType<ComboBox>()) cbm.Enabled = true;
                foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>())
                {
                    chb.Enabled = true;
                    if (chb.Checked) cb_prog.Enabled = false;
                }

   
                b_opt.Enabled = true;

                cb_start.Text = "Start";
                tt_start.SetToolTip(cb_start, lng.tt_start);

                timer_unload(99);

                hold_clear(88);

                //tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0; tmr5_f = 0; tmr6_f = 0;
                //tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                //tmr1_i = 0; tmr2_i = 0; tmr3_i = 0; tmr4_i = 0; tmr5_i = 0; tmr6_i = 0;

                Lang();
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
            UnregisterHotKey(this.Handle, 0);       // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.
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

            //lb_about.Text = lng.lb_about;
            lb_area.Text = lng.lb_area;
            lb_proc.Text = lng.lb_proc;
            lb_stop.Text = lng.lb_stop;
            lb_auth.Text = lng.lb_auth;
            lb_prof.Text = lng.lb_prof;
            lb_tp.Text = lng.lb_tp;
            lb_tpdelay.Text = lng.lb_tpdelay;
            lb_startstop.Text = lng.lb_startstop;

            chb_tray.Text = lng.chb_tray;
            chb_mult.Text = lng.chb_mult;
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
        }

        /// <summary>
        /// Метод для выбора профилей, сохранения и чтения настроек из них.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_prof_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Save_settings(0);
            string path = "";
            switch (cb_prof.SelectedIndex)
            {
                case 1: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof1.xml"); break;
                case 2: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof2.xml"); break;
                case 3: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof3.xml"); break;
            }

            if (path != "" && File.Exists(path)) ReadXML(path);

            cb_prog_SelectionChangeCommitted(null, null);
            if (cb_proc != null) cb_proc.SelectedIndex = -1;
            cb_proc_SelectionChangeCommitted(null, null);
        }

        public void reghotkey()
        {
            //Установка глобального хоткея запуска/остановки
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
            if ((string)cb_tp.Items[cb_tp.SelectedIndex] != "") tp_key = (string)cb_tp.Items[cb_tp.SelectedIndex].ToString();
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
            if (cb_pause.SelectedIndex == 1 || cb_pause.SelectedIndex == 3)
            {
                cb_tp.Enabled=true;
                cb_tpdelay.Enabled = true;
            }
            else
            {
                cb_tp.Enabled = false;
                cb_tpdelay.Enabled = false;
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

            cb_proc.Items.Add("");
            try
            {
                foreach (Process p in processlist)
                {
                    if (p.PagedMemorySize64 > 400000000) //cb_proc.Items.Add(p.ProcessName + " " + p.Id.ToString());
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
            if (proc_curr != "")
            {
                d3proc = true;
                d3prog = false;
                cb_prog.SelectedIndex = 0;
                cb_prog.Enabled = false;
                handle = proc_handle[proc_curr];

                handle = FindWindow(null, "akelpad");
                handle = FindWindowEx(handle, IntPtr.Zero, "AkelEditW", null);
                lb_hold.Visible = false;
            }
            else
            {
                d3proc = false;
                
                int i = 0;
                foreach (CheckBox chb_check in this.pan_main.Controls.OfType<CheckBox>())
                    if (chb_hold.Checked && chb_check.Checked) i++;
                if (i>0)
                    lb_hold.Visible = true;
                else
                    cb_prog.Enabled = true;
            }
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
                cb_proc.Enabled = false;
                cb_proc.SelectedIndex = -1;
                d3proc = false;
            }
            else
            {
                cb_proc.Enabled = true;
            }
        }

        private void d3hot_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && chb_tray.Checked)
            {
                this.Hide();
                notify_d3h.Visible = true;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            ShowMe();
        }

        private void b_opt_Click(object sender, EventArgs e)
        {
            if (!pan_opt.Visible)
            {
                pan_opt.BringToFront();
                pan_opt.Visible=true;
            }
            else   
            {   
                pan_opt.SendToBack();
                pan_opt.Visible = false;
                Save_settings(0);
            }
        }

        private void cb_key_delay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)cb_key_delay.Items[cb_key_delay.SelectedIndex] != "")
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

        private void chb_key_CheckedChanged(object sender, EventArgs e)
        {
            var cb = (CheckBox)sender;
            var chb = ((CheckBox)sender).Name;

            if (cb.Checked && cb_prog.Enabled)
            {
                cb_prog.Enabled = false;
                cb_prog.SelectedIndex = 0;
                cb_prog_SelectionChangeCommitted(null, null);
                if (chb_hold.Checked)
                {
                    lb_hold.Visible = true;
                    cb_proc.SelectedIndex = -1;
                }
            }
            else
            {
                int i = 0;
                foreach (CheckBox chb_check in this.pan_main.Controls.OfType<CheckBox>())
                    if (chb_check.Checked) i++;
                if (i == 0 && !cb_prog.Enabled && cb_proc.SelectedIndex<1)
                {
                    cb_prog.Enabled = true;
                    lb_hold.Visible = false;
                }

            }

            switch (chb)
            {
                case "chb_key1":
                    if (cb.Checked && chb_hold.Checked)
                    {
                        nud_tmr1.Enabled = false;
                        if (cb_key1.Items.Count > 20)
                        {
                            if (cb_key1.SelectedIndex == 19) cb_key1.SelectedIndex = 17;
                            if (cb_key1.SelectedIndex == 20) cb_key1.SelectedIndex = 18;
                            cb_key1.Items.Remove(cb_key1.Items[20]);
                            cb_key1.Items.Remove(cb_key1.Items[19]);
                        }
                    }
                    else
                    {
                        nud_tmr1.Enabled = true;
                        if (cb_key1.Items.Count < 20)
                        {
                            cb_key1.Items.Add("Shift+LM");
                            cb_key1.Items.Add("Shift+RM");
                        }
                    }
                    break;

                case "chb_key2":
                    if (cb.Checked && chb_hold.Checked)
                    {
                        nud_tmr2.Enabled = false;
                        if (cb_key2.Items.Count > 20)
                        {
                            if (cb_key2.SelectedIndex == 19) cb_key2.SelectedIndex = 17;
                            if (cb_key2.SelectedIndex == 20) cb_key2.SelectedIndex = 18;
                            cb_key2.Items.Remove(cb_key2.Items[20]);
                            cb_key2.Items.Remove(cb_key2.Items[19]);
                        }
                    }
                    else
                    {
                        nud_tmr2.Enabled = true;
                        if (cb_key2.Items.Count < 20)
                        {
                            cb_key2.Items.Add("Shift+LM");
                            cb_key2.Items.Add("Shift+RM");
                        }
                    }
                    break;

                case "chb_key3":
                    if (cb.Checked && chb_hold.Checked)
                    {
                        nud_tmr3.Enabled = false;
                        if (cb_key3.Items.Count > 20)
                        {
                            if (cb_key3.SelectedIndex == 19) cb_key3.SelectedIndex = 17;
                            if (cb_key3.SelectedIndex == 20) cb_key3.SelectedIndex = 18;
                            cb_key3.Items.Remove(cb_key3.Items[20]);
                            cb_key3.Items.Remove(cb_key3.Items[19]);
                        }
                    }
                    else
                    {
                        nud_tmr3.Enabled = true;
                        if (cb_key3.Items.Count < 20)
                        {
                            cb_key3.Items.Add("Shift+LM");
                            cb_key3.Items.Add("Shift+RM");
                        }
                    }
                    break;

                case "chb_key4":
                    if (cb.Checked && chb_hold.Checked)
                    {
                        nud_tmr4.Enabled = false;
                        if (cb_key4.Items.Count > 20)
                        {
                            if (cb_key4.SelectedIndex == 19) cb_key4.SelectedIndex = 17;
                            if (cb_key4.SelectedIndex == 20) cb_key4.SelectedIndex = 18;
                            cb_key4.Items.Remove(cb_key4.Items[20]);
                            cb_key4.Items.Remove(cb_key4.Items[19]);
                        }
                    }
                    else
                    {
                        nud_tmr4.Enabled = true;
                        if (cb_key4.Items.Count < 20)
                        {
                            cb_key4.Items.Add("Shift+LM");
                            cb_key4.Items.Add("Shift+RM");
                            
                        }
                    }
                    break;

                case "chb_key5":
                    if (cb.Checked && chb_hold.Checked)
                    {
                        nud_tmr5.Enabled = false;
                        if (cb_key5.Items.Count > 20)
                        {
                            if (cb_key5.SelectedIndex == 19) cb_key5.SelectedIndex = 17;
                            if (cb_key5.SelectedIndex == 20) cb_key5.SelectedIndex = 18;
                            cb_key5.Items.Remove(cb_key5.Items[20]);
                            cb_key5.Items.Remove(cb_key5.Items[19]);
                        }
                    }
                    else
                    {
                        nud_tmr5.Enabled = true;
                        if (cb_key5.Items.Count < 20)
                        {
                            cb_key5.Items.Add("Shift+LM");
                            cb_key5.Items.Add("Shift+RM");
                        }
                    }
                    break;

                case "chb_key6":
                    if (cb.Checked && chb_hold.Checked)
                    {
                        nud_tmr6.Enabled = false;
                        if (cb_key6.Items.Count > 20)
                        {
                            if (cb_key6.SelectedIndex == 19) cb_key6.SelectedIndex = 17;
                            if (cb_key6.SelectedIndex == 20) cb_key6.SelectedIndex = 18;
                            cb_key6.Items.Remove(cb_key6.Items[20]);
                            cb_key6.Items.Remove(cb_key6.Items[19]);
                        }
                    }
                    else
                    {
                        nud_tmr6.Enabled = true;
                        if (cb_key6.Items.Count < 20)
                        {
                            cb_key6.Items.Add("Shift+LM");
                            cb_key6.Items.Add("Shift+RM");

                        }
                    }
                    break;
            }
        }

        private void chb_hold_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_hold.Checked)
            {
                foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>())
                {
                    if (chb.Name != "cb_start")
                    {
                        chb.Visible = true;
                        chb_key_CheckedChanged(chb, null);
                        if (chb.Checked && cb_proc.SelectedIndex<1) lb_hold.Visible = true;
                    }
                }
            }
            else 
            {
                foreach (CheckBox chb in this.pan_main.Controls.OfType<CheckBox>())
                {
                    if (chb.Name != "cb_start")
                    {
                        chb.Visible = false;
                        chb_key_CheckedChanged(chb, null);
                    }
                }
                lb_hold.Visible = false;
                if (!cb_prog.Enabled && cb_proc.SelectedIndex < 1)
                    cb_prog.Enabled = true;
            }
        }

    }
}
