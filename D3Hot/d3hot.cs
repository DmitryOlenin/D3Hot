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

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public System.Timers.Timer tmr1, tmr2, tmr3, tmr4, tmr5, tmr6, tmr_all;
        public Boolean shift = false, d3prog = false;
        public InputSimulator inp = new InputSimulator();
        public int trig1 = 0, trig2 = 0, trig3 = 0, trig4 = 0, trig5 = 0, trig6 = 0,
            key1 = 0, key2 = 0, key3 = 0, key4 = 0, key5 = 0, key6 = 0,
            tmr1_f = 0, tmr2_f = 0, tmr3_f = 0, tmr4_f = 0, tmr5_f = 0, tmr6_f = 0,
            tmr1_r = 0, tmr2_r = 0, tmr3_r = 0, tmr4_r = 0, tmr5_r = 0, tmr6_r = 0,
            pause = 0, prof_prev, tp_delay=0;

        public double tmr1_i = 0, tmr2_i = 0, tmr3_i = 0, tmr4_i = 0, tmr5_i = 0, tmr6_i = 0;
        public static int t_press = 0, return_press = 0, r_press = 0, return_press_count = 0;
        public static string tp_key = "";
        public static SettingsTable overview;

        public Class_lang lng = new Class_lang();

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll", SetLastError = true)]
        //static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

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
            if (e.KeyCode.ToString() == tp_key) t_press += 1;
            if (e.KeyCode.ToString() == "Return") return_press = 1;
        }

        public d3hot()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        protected override void WndProc(ref Message m)
        {
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
            if (i == 1) result = Control.ModifierKeys == Keys.Shift; else
            if (i == 2) result = Control.IsKeyLocked(Keys.Scroll); else
            if (i == 3) result = Control.IsKeyLocked(Keys.CapsLock); else
            if (i == 4) result = Control.IsKeyLocked(Keys.NumLock); 
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
            if (i == 0) vkc = VirtualKeyCode.VK_1; else
            if (i == 1) vkc = VirtualKeyCode.VK_2; else
            if (i == 2) vkc = VirtualKeyCode.VK_3; else
            if (i == 3) vkc = VirtualKeyCode.VK_4; else
            if (i == 4) vkc = VirtualKeyCode.VK_Q; else
            if (i == 5) vkc = VirtualKeyCode.VK_W; else
            if (i == 6) vkc = VirtualKeyCode.VK_E; else
            if (i == 7) vkc = VirtualKeyCode.VK_R; else
            if (i == 8) vkc = VirtualKeyCode.VK_A; else
            if (i == 9) vkc = VirtualKeyCode.VK_S; else
            if (i == 10) vkc = VirtualKeyCode.VK_D; else
            if (i == 11) vkc = VirtualKeyCode.VK_F; else
            if (i == 12) vkc = VirtualKeyCode.VK_Z; else
            if (i == 13) vkc = VirtualKeyCode.VK_X; else
            if (i == 14) vkc = VirtualKeyCode.VK_C; else
            if (i == 15) vkc = VirtualKeyCode.VK_V; else
            if (i == 16) vkc = VirtualKeyCode.SPACE; else
            if (i == 17) vkc = VirtualKeyCode.LBUTTON; else
            if (i == 18) vkc = VirtualKeyCode.RBUTTON;
            return vkc;
        }

        /// <summary>
        /// Основной метод после запуска процесса - срабатывание главного таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tmr_all_Elapsed(object sender, EventArgs e)
        {
            //Работаем только если хоть что-то из триггеров зажато/переключено.
            if (key_press(trig1) || key_press(trig2) || key_press(trig3) || key_press(trig4) || key_press(trig5) || key_press(trig6))
            {
                //Проверка на одинарное/двойное нажатие Enter.
                if ((pause == 2 || pause == 3) && return_press == 1 && r_press == 0 && t_press == 0)
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    r_press = 1; return_press = 0;
                    //tmr_all.Interval = 100;
                }

                if (r_press == 1 && return_press == 1)
                {
                    r_press = 0; return_press = 0; t_press = 0;
                }

                //Проверка на нажатие T.
                if ((pause == 1 || pause == 3) && t_press > 0 && tmr_all.Interval == tp_delay*1000)
                {
                    t_press = 0;
                    tmr_all.Interval = 1;
                    return_press = 0;
                }

                if ((pause == 1 || pause == 3) && t_press > 0 && r_press == 0)
                {
                    timer_unload(88);
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                    tmr_all.Interval = tp_delay * 1000;
                }

                if ((pause == 1 || pause == 3) && r_press > 0 && t_press > 0) t_press = 0;


                //Если T|Enter не нажаты, запускаем таймеры триггеров 1-2-3-4-5-6 при активном состоянии и останавливаем при отключенном.
                if (t_press == 0 && r_press == 0)
                {
                    if (tmr1_f == 1)
                    {
                        if (key_press(trig1))
                        {
                            if (tmr1_r == 0)
                            {
                                tmr1_r = 1;
                                timer_load(1);
                                tmr_Elapsed(tmr1, null);
                                tmr1.Enabled = true;
                            }
                        }
                        else
                        {
                            timer_unload(1); 
                            tmr1_r = 0;
                        }
                    }

                    if (tmr2_f == 1)
                    {
                        if (key_press(trig2))
                        {
                            if (tmr2_r == 0)
                            {
                                tmr2_r = 1;
                                timer_load(2);
                                tmr_Elapsed(tmr2, null);
                                tmr2.Enabled = true;
                            }
                        }
                        else
                        {
                            timer_unload(2); 
                            tmr2_r = 0;
                        }
                    }

                    if (tmr3_f == 1)
                    {
                        if (key_press(trig3))
                        {
                            if (tmr3_r == 0)
                            {
                                tmr3_r = 1;
                                timer_load(3);
                                tmr_Elapsed(tmr3, null);
                                tmr3.Enabled = true;
                            }
                        }
                        else
                        {
                            timer_unload(3); 
                            tmr3_r = 0;
                        }
                    }

                    if (tmr4_f == 1)
                    {
                        if (key_press(trig4))
                        {
                            if (tmr4_r == 0)
                            {
                                tmr4_r = 1;
                                timer_load(4);
                                tmr_Elapsed(tmr4, null);
                                tmr4.Enabled = true;
                            }
                        }
                        else
                        {
                            timer_unload(4); 
                            tmr4_r = 0;
                        }
                    }

                    if (tmr5_f == 1)
                    {
                        if (key_press(trig5))
                        {
                            if (tmr5_r == 0)
                            {
                                tmr5_r = 1;
                                timer_load(5);
                                tmr_Elapsed(tmr5, null);
                                tmr5.Enabled = true;
                            }
                        }
                        else
                        {
                            timer_unload(5);
                            tmr5_r = 0;
                        }
                    }

                    if (tmr6_f == 1)
                    {
                        if (key_press(trig6))
                        {
                            if (tmr6_r == 0)
                            {
                                tmr6_r = 1;
                                timer_load(6);
                                tmr_Elapsed(tmr6, null);
                                tmr6.Enabled = true;
                            }
                        }
                        else
                        {
                            timer_unload(6);
                            tmr6_r = 0;
                        }
                    }

                }
            }
            else
            {
                timer_unload(88);
                return_press = 0; r_press = 0; t_press = 0; 
                tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
            }
        }

        public void tmr_Elapsed(object sender, EventArgs e)
        {
            int f = 1;
            var tmr = (System.Timers.Timer)sender;

            string title = "";
            if (GetActiveWindowTitle() != null) title = GetActiveWindowTitle();

                    //int PID;
                    //GetWindowThreadProcessId(GetForegroundWindow(), out PID);
                    //Process proc = Process.GetProcessById(PID);
                    //MessageBox.Show(proc.ProcessName);

            //Проверка окна на ниличе слова "Diablo", если область действия соответствующая.
            if (d3prog && !title.ToLower().Contains("diablo")) f = 0; 
            //Если всё в порядке, нажимаем соответствующую клавишу.
            if (f == 1)
            {
                VirtualKeyCode key = VirtualKeyCode.VK_0; 

                if (tmr == tmr1 && key_press(trig1)) key = virt_code(key1);
                if (tmr == tmr2 && key_press(trig2)) key = virt_code(key2);
                if (tmr == tmr3 && key_press(trig3)) key = virt_code(key3);
                if (tmr == tmr4 && key_press(trig4)) key = virt_code(key4);
                if (tmr == tmr5 && key_press(trig5)) key = virt_code(key5);
                if (tmr == tmr6 && key_press(trig6)) key = virt_code(key6);
                if (key != VirtualKeyCode.VK_0) inp.Keyboard.KeyPress(key);
                if (key == VirtualKeyCode.LBUTTON) inp.Mouse.LeftButtonClick();
                if (key == VirtualKeyCode.RBUTTON) inp.Mouse.RightButtonClick();
            }

        }

        /// <summary>
        /// Метод при запуске программы или её остановке (Start/Stop/F11).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_start_CheckedChanged(object sender, EventArgs e)
        {
            timer_unload(99);
            tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0; tmr5_f = 0; tmr6_f = 0;
            tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
            trig1 = 0; trig2 = 0; trig3 = 0; trig4 = 0; trig5 = 0; trig6 = 0;
            tmr1_i = 0; tmr2_i = 0; tmr3_i = 0; tmr4_i = 0; tmr5_i = 0; tmr6_i = 0;

            foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;

            int i = 0;

            if  ((cb_trig_tmr1.SelectedIndex != 0 && nud_tmr1.Value != 0) ||
                (cb_trig_tmr2.SelectedIndex != 0 && nud_tmr2.Value != 0) ||
                (cb_trig_tmr3.SelectedIndex != 0 && nud_tmr3.Value != 0) ||
                (cb_trig_tmr4.SelectedIndex != 0 && nud_tmr4.Value != 0)  ||
                (cb_trig_tmr5.SelectedIndex != 0 && nud_tmr5.Value != 0)  ||
                (cb_trig_tmr6.SelectedIndex != 0 && nud_tmr6.Value != 0) ) 
                i++;

            if (i == 0) cb_start.Checked = false;

            t_press = 0; return_press = 0; r_press = 0;

            if (cb_start.Checked)
            {
                pause = cb_pause.SelectedIndex;
                cb_start.Text = "Stop";
                tt_start.SetToolTip(cb_start, lng.tt_stop);

                //Блокирование элементов настройки, пока программа работает
                foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) numud.Enabled = false;
                foreach (ComboBox cb in this.Controls.OfType<ComboBox>()) cb.Enabled = false;

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
                    if (!tmr_all.Enabled) tmr_all.Enabled = true;
                }
            }
            else
            {
                foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) numud.Enabled = true;
                foreach (ComboBox cb in this.Controls.OfType<ComboBox>()) cb.Enabled = true;

                cb_start.Text = "Start";
                tt_start.SetToolTip(cb_start, lng.tt_start);

                timer_unload(99);

                tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0; tmr5_f = 0; tmr6_f = 0;
                tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0; tmr5_r = 0; tmr6_r = 0;
                tmr1_i = 0; tmr2_i = 0; tmr3_i = 0; tmr4_i = 0; tmr5_i = 0; tmr6_i = 0;

                Lang();
            }

        }

        /// <summary>
        /// Выбор области действия программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_prog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_prog.SelectedIndex == 1) d3prog = true;
            if (cb_prog.SelectedIndex == 0) d3prog = false;
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
        /// Языковые методы.
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

            lb_about.Text = lng.lb_about;
            lb_area.Text = lng.lb_area;
            lb_stop.Text = lng.lb_stop;
            lb_auth.Text = lng.lb_auth;
            lb_prof.Text = lng.lb_prof;
            lb_tp.Text = lng.lb_tp;
            lb_tpdelay.Text = lng.lb_tpdelay;
            lb_startstop.Text = lng.lb_startstop;

            tt_start.SetToolTip(cb_start, lng.tt_start);
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
                    case 12: RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F12.GetHashCode()); break;//Глобальный хоткей запуска/остановки F12 
                }
            }
        }

        private void cb_startstop_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnregisterHotKey(this.Handle, 0);
            reghotkey();
        }

        private void cb_tp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)cb_tp.Items[cb_tp.SelectedIndex] != "") tp_key = (string)cb_tp.Items[cb_tp.SelectedIndex].ToString();
        }

        private void cb_tpdelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_tpdelay.SelectedIndex > -1) tp_delay = Convert.ToInt32(cb_tpdelay.Items[cb_tpdelay.SelectedIndex]);
        }

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

    }
}
