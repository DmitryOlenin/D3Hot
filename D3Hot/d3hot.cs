using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Reflection;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using D3Hot.Properties;
//using Utilities;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System.Globalization; 

namespace D3Hot
{
    public partial class d3hot : Form
    {

        public System.Timers.Timer tmr1;// = new System.Timers.Timer(); 
        public System.Timers.Timer tmr2;// = new System.Timers.Timer(); 
        public System.Timers.Timer tmr3;// = new System.Timers.Timer(); 
        public System.Timers.Timer tmr4;// = new System.Timers.Timer(); 
        //public System.Timers.Timer tmr5 = new System.Timers.Timer();
        public System.Timers.Timer tmr_all;// = new System.Timers.Timer(); 
        public Boolean shift = false, d3prog = false;
        public InputSimulator inp = new InputSimulator();
        public int trig1 = 0, trig2 = 0, trig3 = 0, trig4 = 0, trig5 = 0,
            key1 = 0, key2 = 0, key3 = 0, key4 = 0, key5 = 0, 
            tmr1_f = 0, tmr2_f = 0, tmr3_f = 0, tmr4_f = 0, tmr5_f = 0,
            tmr1_r = 0, tmr2_r = 0, tmr3_r = 0, tmr4_r = 0, tmr5_r = 0,
            pause = 0;

        public double tmr1_i = 0, tmr2_i = 0, tmr3_i = 0, tmr4_i = 0;
        public static int t_press = 0, return_press = 0, t_press_count = 0, return_press_count = 0;

        public Class_lang lng = new Class_lang();

        //globalKeyboardHook gkh = new globalKeyboardHook();
        //private KeyboardHookListener khl;
        //KeyboardHookListener khl = new KeyboardHookListener(new GlobalHooker());

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

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

        // Subroutine for activating the hook
        //public void khl_Activate()
        //{
        //    // Note: for an application hook, use the AppHooker class instead
        //    khl = new KeyboardHookListener(new GlobalHooker());

        //    // The listener is not enabled by default
        //    khl.Enabled = true;

        //    // Set the event handler
        //    // recommended to use the Extended handlers, which allow input suppression among other additional information
        //    khl.KeyDown += khl_KeyDown;
        //}


        //public void khl_Deactivate()
        //{
        //    khl.Dispose();
        //}

        //private void khl_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.T) MessageBox.Show("!@#124");
        //}

        public void timer_load(int i)
        {
            if (i == 0)
            {
                tmr_all = new System.Timers.Timer();
                tmr_all.Elapsed += tmr_all_Elapsed;
                tmr_all.Interval = 1;
            }

            if (i == 1)
            {
                tmr1 = new System.Timers.Timer();
                tmr1.Elapsed += tmr_Elapsed;
                tmr1.Interval = tmr1_i;
            }

            if (i == 2)
            {
                tmr2 = new System.Timers.Timer();
                tmr2.Elapsed += tmr_Elapsed;
                tmr2.Interval = tmr2_i;
            }
        
            if (i == 3)
            {
                tmr3 = new System.Timers.Timer();
                tmr3.Elapsed += tmr_Elapsed;
                tmr3.Interval = tmr3_i;
            }

            if (i == 4)
            {
                tmr4 = new System.Timers.Timer();
                tmr4.Elapsed += tmr_Elapsed;
                tmr4.Interval = tmr4_i;
            }
        }


        public void timer_unload(int i)
        {
            if (tmr_all != null && (i == 0 || i == 99)) tmr_all.Dispose();
            if (tmr1 != null && (i == 1 || i == 99 || i == 88)) tmr1.Dispose();
            if (tmr2 != null && (i == 2 || i == 99 || i == 88)) tmr2.Dispose();
            if (tmr3 != null && (i == 3 || i == 99 || i == 88)) tmr3.Dispose();
            if (tmr4 != null && (i == 4 || i == 99 || i == 88)) tmr4.Dispose();
        }

        private void mouseKeyEventProvider1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Y) MessageBox.Show("!Это же Y!");

            if (e.KeyCode.ToString() == "T") t_press += 1;
            if (e.KeyCode.ToString() == "Return") return_press += 1;
        }

        public d3hot()
        {
            InitializeComponent();

            //khl_Activate();

            int id = 0;     // The id of the hotkey. 
            RegisterHotKey(this.Handle, id, (int)KeyModifier.None, Keys.F11.GetHashCode());

            //cb_prog.SelectedIndex = 0;
            //cb_key1.SelectedIndex = 0;
            //cb_key2.SelectedIndex = 1;
            //cb_key3.SelectedIndex = 2;
            //cb_key4.SelectedIndex = 3;

            //timer_load(); //01.03.2015

            //gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            //gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);

            //if (Control.IsKeyLocked(Keys.CapsLock)) MessageBox.Show("CapsLock is locked"); //Keys.NumLock, Keys.Scroll
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
            
            cb_key1.SelectedIndex = Settings.Default.cb_key1;
            cb_key2.SelectedIndex = Settings.Default.cb_key2;
            cb_key3.SelectedIndex = Settings.Default.cb_key3;
            cb_key4.SelectedIndex = Settings.Default.cb_key4;
            cb_trig_tmr1.SelectedIndex = Settings.Default.cb_trig_tmr1;
            cb_trig_tmr2.SelectedIndex = Settings.Default.cb_trig_tmr2;
            cb_trig_tmr3.SelectedIndex = Settings.Default.cb_trig_tmr3;
            cb_trig_tmr4.SelectedIndex = Settings.Default.cb_trig_tmr4;
            cb_prog.SelectedIndex = Settings.Default.cb_prog;
            cb_pause.SelectedIndex = Settings.Default.cb_pause;
            nud_tmr1.Value = Settings.Default.nud_tmr1;
            nud_tmr2.Value = Settings.Default.nud_tmr2;
            nud_tmr3.Value = Settings.Default.nud_tmr3;
            nud_tmr4.Value = Settings.Default.nud_tmr4;

            if (Settings.Default.lb_lang == "")
            {
                if (CultureInfo.CurrentCulture.EnglishName.Contains("Russia")) Settings.Default.lb_lang = "Eng";
                else Settings.Default.lb_lang = "Rus";
                Settings.Default.Save();
            }
            lb_lang.Text = Settings.Default.lb_lang;
            if (lb_lang.Text == "Eng") lng.Lang_rus(); else lng.Lang_eng();
            Lang();

            if (nud_tmr1.Value > 0) lb_tmr1_sec.Text = Math.Round((nud_tmr1.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr2.Value > 0) lb_tmr2_sec.Text = Math.Round((nud_tmr2.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr3.Value > 0) lb_tmr3_sec.Text = Math.Round((nud_tmr3.Value / 1000), 2).ToString() + " " + lng.lang_sec;
            if (nud_tmr4.Value > 0) lb_tmr4_sec.Text = Math.Round((nud_tmr4.Value / 1000), 2).ToString() + " " + lng.lang_sec;

            tt_start.SetToolTip(cb_start, "F11 to start");

            cb_pause_SelectedIndexChanged (null,null);


        }

        //void gkh_KeyUp(object sender, KeyEventArgs e)
        //{
        //    //lstLog.Items.Add("Up\t" + e.KeyCode.ToString());

        //    //e.Handled = true;
        //}

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            //lstLog.Items.Add("Down\t" + e.KeyCode.ToString());

            if ((cb_pause.SelectedIndex == 1 || cb_pause.SelectedIndex == 3) && e.KeyCode.ToString() == "T") t_press += 1;
            if ((cb_pause.SelectedIndex == 2 || cb_pause.SelectedIndex == 3) && e.KeyCode.ToString() == "Return") return_press += 1;
            
            //e.Handled = true;
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

        private bool key_press(int i)
        {
            bool result=false;
            if (i == 1) result = Control.ModifierKeys == Keys.Shift;
            if (i == 2) result = Control.IsKeyLocked(Keys.Scroll);
            if (i == 3) result = Control.IsKeyLocked(Keys.CapsLock);
            if (i == 4) result = Control.IsKeyLocked(Keys.NumLock);
            return result;
        }

        private VirtualKeyCode virt_code(int i)
        {
            VirtualKeyCode vkc = VirtualKeyCode.VK_0;
            if (i == 0) vkc = VirtualKeyCode.VK_1;
            if (i == 1) vkc = VirtualKeyCode.VK_2;
            if (i == 2) vkc = VirtualKeyCode.VK_3;
            if (i == 3) vkc = VirtualKeyCode.VK_4;
            if (i == 4) vkc = VirtualKeyCode.VK_Q;
            if (i == 5) vkc = VirtualKeyCode.VK_W;
            if (i == 6) vkc = VirtualKeyCode.VK_E;
            if (i == 7) vkc = VirtualKeyCode.VK_R;
            if (i == 8) vkc = VirtualKeyCode.VK_A;
            if (i == 9) vkc = VirtualKeyCode.VK_S;
            if (i == 10) vkc = VirtualKeyCode.VK_D;
            if (i == 11) vkc = VirtualKeyCode.VK_F;
            if (i == 12) vkc = VirtualKeyCode.VK_Z;
            if (i == 13) vkc = VirtualKeyCode.VK_X;
            if (i == 14) vkc = VirtualKeyCode.VK_C;
            if (i == 15) vkc = VirtualKeyCode.VK_V;
            return vkc;
        }

        public bool time_stop(int i)
        {
            bool chk = true;




            return chk;
        }

        public void tmr_all_Elapsed(object sender, EventArgs e)
        {
            if (key_press(trig1) || key_press(trig2) || key_press(trig3) || key_press(trig4))
            {
                if ((pause == 2 || pause == 3) && return_press == 1)
                {
                    //if (tmr1.Enabled) tmr1.Enabled = false;
                    //if (tmr2.Enabled) tmr2.Enabled = false;
                    //if (tmr3.Enabled) tmr3.Enabled = false;
                    //if (tmr4.Enabled) tmr4.Enabled = false;
                    timer_unload(88); 
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0;
                }

                if ((pause == 2 || pause == 3) && return_press > 1)
                {
                    return_press = 0;
                }

                if ((pause == 1 || pause == 3) && t_press > 0)
                {
                    t_press_count += 1;
                    //if (tmr1.Enabled) tmr1.Enabled = false;
                    //if (tmr2.Enabled) tmr2.Enabled = false;
                    //if (tmr3.Enabled) tmr3.Enabled = false;
                    //if (tmr4.Enabled) tmr4.Enabled = false;
                    timer_unload(88); 
                    tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0;
                }

                if ((pause == 1 || pause == 3) && t_press_count > 500)
                {
                    //MessageBox.Show(t_press_count.ToString());
                    t_press_count = 0;
                    t_press = 0;
                }

                if (t_press == 0 && return_press == 0)
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
                            //tmr1.Enabled = false;
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
                            //tmr2.Enabled = false;
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
                            //tmr3.Enabled = false;
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
                            //tmr4.Enabled = false;
                            timer_unload(4); 
                            tmr4_r = 0;
                        }
                    }

                }
            }
            else
            {
                timer_unload(88);
                return_press = 0; t_press = 0; t_press_count = 0;
                tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0;
            }
        }

        public void tmr_Elapsed(object sender, EventArgs e)
        {
            int f = 1;
            var tmr = (System.Timers.Timer)sender;

            string title = "";
            if (GetActiveWindowTitle() != null) title = GetActiveWindowTitle();

            if (d3prog && !title.ToLower().Contains("diablo")) f = 0;
            if (f == 1)
            {
                //bool isShiftOn = Control.ModifierKeys == Keys.Shift;
                //bool isScrollLockOn = Control.IsKeyLocked(Keys.Scroll);
                //bool isCapsLockOn = Control.IsKeyLocked(Keys.CapsLock);
                //bool isNumLockOn = Control.IsKeyLocked(Keys.NumLock);

                VirtualKeyCode key = VirtualKeyCode.VK_0; 
                                                
                //if (isShiftOn || isCapsLockOn)

                if (tmr == tmr1 && key_press(trig1)) key = virt_code(key1);
                if (tmr == tmr2 && key_press(trig2)) key = virt_code(key2);
                if (tmr == tmr3 && key_press(trig3)) key = virt_code(key3);
                if (tmr == tmr4 && key_press(trig4)) key = virt_code(key4);
                //if (tmr == tmr5 && key_press(trig5)) key = virt_code(key5); 

                    //SendKeys.SendWait("1");
                if (key != VirtualKeyCode.VK_0) inp.Keyboard.KeyPress(key);

                    //string fullName = GetActiveWindowTitle();
                    //MessageBox.Show(fullName);
            }

        }

        private void cb_start_CheckedChanged(object sender, EventArgs e)
        {
            timer_unload(99);
            tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0;
            tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0;
            trig1 = 0; trig2 = 0; trig3 = 0; trig4 = 0;
            tmr1_i = 0; tmr2_i = 0; tmr3_i = 0; tmr4_i = 0;

            foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;

            int i = 0;

            if  ((cb_trig_tmr1.SelectedIndex != 0 && nud_tmr1.Value != 0) ||
                (cb_trig_tmr2.SelectedIndex != 0 && nud_tmr2.Value != 0) ||
                (cb_trig_tmr3.SelectedIndex != 0 && nud_tmr3.Value != 0) ||
                (cb_trig_tmr4.SelectedIndex != 0 && nud_tmr4.Value != 0) )
                i++;

            if (i == 0) cb_start.Checked = false;
            //if (nud_tmr1.Value == 0 && nud_tmr2.Value == 0 && nud_tmr3.Value == 0 && nud_tmr4.Value == 0) cb_start.Checked = false;

            t_press=0; t_press_count=0;return_press=0;

            if (cb_start.Checked)
            {
                pause = cb_pause.SelectedIndex;
                cb_start.Text = "Stop";

                foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) numud.Enabled = false;
                foreach (ComboBox cb in this.Controls.OfType<ComboBox>()) cb.Enabled = false;


                if (nud_tmr1.Value > 0 && cb_trig_tmr1.SelectedIndex > 0)// && !tmr1.Enabled && key_press(cb_trig_tmr1.SelectedIndex))
                {
                    lb_tmr1_sec.Text = Math.Round((nud_tmr1.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    //tmr1.Interval = Convert.ToDouble(nud_tmr1.Value);
                    tmr1_i = Convert.ToDouble(nud_tmr1.Value);
                    trig1 = cb_trig_tmr1.SelectedIndex;
                    key1 = cb_key1.SelectedIndex;
                    //tmr_Elapsed(tmr1, null);
                    //tmr1.Enabled = true;
                    tmr1_f = 1;
                }
                if (nud_tmr2.Value > 0 && cb_trig_tmr2.SelectedIndex > 0)// && !tmr2.Enabled)// && key_press(cb_trig_tmr2.SelectedIndex))
                {
                    lb_tmr2_sec.Text = Math.Round((nud_tmr2.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    //tmr2.Interval = Convert.ToDouble(nud_tmr2.Value);
                    tmr2_i = Convert.ToDouble(nud_tmr2.Value);
                    trig2 = cb_trig_tmr2.SelectedIndex;
                    key2 = cb_key2.SelectedIndex;
                    //tmr_Elapsed(tmr2, null);
                    //tmr2.Enabled = true;
                    tmr2_f = 1;
                }

                if (nud_tmr3.Value > 0 && cb_trig_tmr3.SelectedIndex > 0)// && !tmr3.Enabled)// && key_press(cb_trig_tmr3.SelectedIndex))
                {
                    lb_tmr3_sec.Text = Math.Round((nud_tmr3.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    //tmr3.Interval = Convert.ToDouble(nud_tmr3.Value);
                    tmr3_i = Convert.ToDouble(nud_tmr3.Value);
                    trig3 = cb_trig_tmr3.SelectedIndex;
                    key3 = cb_key3.SelectedIndex;
                    //tmr_Elapsed(tmr3, null);
                    //tmr3.Enabled = true;
                    tmr3_f = 1;
                }

                if (nud_tmr4.Value > 0 && cb_trig_tmr4.SelectedIndex > 0)// && !tmr4.Enabled)// && key_press(cb_trig_tmr4.SelectedIndex))
                {
                    lb_tmr4_sec.Text = Math.Round((nud_tmr4.Value / 1000), 2).ToString() + " " + lng.lang_sec;
                    //tmr4.Interval = Convert.ToDouble(nud_tmr4.Value);
                    tmr4_i = Convert.ToDouble(nud_tmr4.Value);
                    trig4 = cb_trig_tmr4.SelectedIndex;
                    key4 = cb_key4.SelectedIndex;
                    //tmr_Elapsed(tmr4, null);
                    //tmr4.Enabled = true;
                    tmr4_f = 1;
                }

                //if (nud_tmr5.Value > 0 && cb_trig_tmr5.SelectedIndex > 0)
                //{
                //    lb_tmr5_sec.Text = Math.Round((nud_tmr5.Value / 1000), 2).ToString() + " сек.";
                //    //lb_tmr5_sec.Visible = true;
                //    tmr5.Interval = Convert.ToDouble(nud_tmr5.Value);
                //    trig5 = cb_trig_tmr5.SelectedIndex;
                //    key5 = cb_key5.SelectedIndex;
                //    tmr5.Enabled = true;
                //}


                //if (tmr1.Enabled && !key_press(cb_trig_tmr1.SelectedIndex)) tmr1.Enabled = false;
                //if (tmr2.Enabled && !key_press(cb_trig_tmr2.SelectedIndex)) tmr2.Enabled = false;
                //if (tmr3.Enabled && !key_press(cb_trig_tmr3.SelectedIndex)) tmr3.Enabled = false;
                //if (tmr4.Enabled && !key_press(cb_trig_tmr4.SelectedIndex)) tmr4.Enabled = false;

                if (tmr1_f == 1 || tmr2_f == 1 || tmr3_f == 1 || tmr4_f == 1)
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

                //tmr_all.Enabled = false;
                //tmr1.Enabled = false;
                //tmr2.Enabled = false;
                //tmr3.Enabled = false;
                //tmr4.Enabled = false;

                timer_unload(99);

                tmr1_f = 0; tmr2_f = 0; tmr3_f = 0; tmr4_f = 0;
                tmr1_r = 0; tmr2_r = 0; tmr3_r = 0; tmr4_r = 0;
                tmr1_i = 0; tmr2_i = 0; tmr3_i = 0; tmr4_i = 0;

                lb_tmr1_sec.Text = "Пауза..мс"; //lb_tmr1_sec.Visible = false;
                lb_tmr2_sec.Text = "Пауза..мс"; //lb_tmr2_sec.Visible = false;
                lb_tmr3_sec.Text = "Пауза..мс"; //lb_tmr3_sec.Visible = false;
                lb_tmr4_sec.Text = "Пауза..мс"; //lb_tmr4_sec.Visible = false;
                //lb_tmr5_sec.Text = "Пауза5..мс"; //lb_tmr5_sec.Visible = false;
            }

            //bw.RunWorkerAsync();
        }

        private void cb_prog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_prog.SelectedIndex == 1) d3prog = true;
            if (cb_prog.SelectedIndex == 0) d3prog = false;
        }

        private void d3hot_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.cb_key1 = cb_key1.SelectedIndex;
            Settings.Default.cb_key2 = cb_key2.SelectedIndex;
            Settings.Default.cb_key3 = cb_key3.SelectedIndex;
            Settings.Default.cb_key4 = cb_key4.SelectedIndex;
            Settings.Default.cb_trig_tmr1 = cb_trig_tmr1.SelectedIndex;
            Settings.Default.cb_trig_tmr2 = cb_trig_tmr2.SelectedIndex;
            Settings.Default.cb_trig_tmr3 = cb_trig_tmr3.SelectedIndex;
            Settings.Default.cb_trig_tmr4 = cb_trig_tmr4.SelectedIndex;
            Settings.Default.cb_prog = cb_prog.SelectedIndex;
            Settings.Default.cb_pause = cb_pause.SelectedIndex;
            Settings.Default.nud_tmr1 = nud_tmr1.Value;
            Settings.Default.nud_tmr2 = nud_tmr2.Value;
            Settings.Default.nud_tmr3 = nud_tmr3.Value;
            Settings.Default.nud_tmr4 = nud_tmr4.Value;
            Settings.Default.lb_lang = lb_lang.Text;
            Settings.Default.Save();
            UnregisterHotKey(this.Handle, 0);       // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.
        }

        private void cb_pause_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((cb_pause.SelectedIndex == 1 || cb_pause.SelectedIndex == 3) && !gkh.HookedKeys.Contains(Keys.T)) gkh.HookedKeys.Add(Keys.T);
            //if ((cb_pause.SelectedIndex == 2 || cb_pause.SelectedIndex == 3) && !gkh.HookedKeys.Contains(Keys.Enter)) gkh.HookedKeys.Add(Keys.Enter);
            //if (cb_pause.SelectedIndex == 1 && gkh.HookedKeys.Contains(Keys.Enter)) gkh.HookedKeys.Remove(Keys.Enter);
            //if (cb_pause.SelectedIndex == 2 && gkh.HookedKeys.Contains(Keys.T)) gkh.HookedKeys.Remove(Keys.T);
        }

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
            System.Diagnostics.Process.Start("http://horadric.ru/forum/viewtopic.php?f=16&t=26771");
            //if (bt_lang.Text == "ENG") System.Diagnostics.Process.Start("http://horadric.ru/forum/viewtopic.php?f=16&t=26771");
            //else System.Diagnostics.Process.Start("http://www.diablofans.com/forums/diablo-iii-general-forums/diablo-iii-general-discussion/89743-offline-dps-calculator-diablo-3");
        }

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

            lb_key1.Text = lng.lb_key1;
            lb_key2.Text = lng.lb_key2;
            lb_key3.Text = lng.lb_key3;
            lb_key4.Text = lng.lb_key4;

            lb_tmr1_sec.Text = lng.lb_tmr_sec;
            lb_tmr2_sec.Text = lng.lb_tmr_sec;
            lb_tmr3_sec.Text = lng.lb_tmr_sec;
            lb_tmr4_sec.Text = lng.lb_tmr_sec;

            lb_about.Text = lng.lb_about;
            lb_area.Text = lng.lb_area;
            lb_stop.Text = lng.lb_stop;
            lb_auth.Text = lng.lb_auth;
            //lang_sec
        }


    }
}
