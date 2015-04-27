using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using WindowsInput.Native;
using D3Hot.Properties;

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public Form frm_key_input;
        public Label lb_key_prev, lb_key_desc;
        public static int key_number=0;
        public static String[] key_list = new String[] { "", "", "", "", "", "", "", "", "" };
        public int key_code1, key_code2, key_code3, key_code4, key_code5, key_code6;
        public VirtualKeyCode vkc1, vkc2, vkc3, vkc4, vkc5, vkc6;

        public class NonFocusButton : Button
        {
            public NonFocusButton()
            {
                SetStyle(ControlStyles.Selectable, false);
            }
        }

        public NonFocusButton b_key_ok;

        private void form_create()
        {
            frm_key_input = new Form();

            //frm_key_input.Owner = this;

            frm_key_input.AutoSize = true;
            frm_key_input.Size = new System.Drawing.Size(140, 100);
            frm_key_input.MaximumSize = new System.Drawing.Size(140, 100);
            frm_key_input.MinimumSize = new System.Drawing.Size(140, 100);
            frm_key_input.Visible = false;
            this.Move += form_move;
            frm_key_input.Deactivate += new EventHandler(form_Deactivate);
            frm_key_input.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            frm_key_input.StartPosition = FormStartPosition.Manual;
            frm_key_input.ShowInTaskbar = false;
            frm_key_input.Move += form_move;
            frm_key_input.ControlBox = false;
            frm_key_input.KeyPreview = true;
            frm_key_input.KeyDown += new KeyEventHandler(form_KeyDown);

            lb_key_desc = new Label();
            lb_key_desc.Text = lng.lb_key_desc;//"Клавиша нажата:"
            lb_key_desc.Location = new Point(15, 5);
            lb_key_desc.AutoSize = true;
            frm_key_input.Controls.Add(lb_key_desc);

            lb_key_prev = new Label();
            lb_key_prev.Location = new Point(13, 30);
            lb_key_prev.BorderStyle = BorderStyle.FixedSingle;
            lb_key_prev.AutoSize = false;
            lb_key_prev.Size = new System.Drawing.Size (97, 17);
            lb_key_prev.TextAlign = ContentAlignment.MiddleCenter;
            frm_key_input.Controls.Add(lb_key_prev);

            b_key_ok = new NonFocusButton();
            b_key_ok.Location = new Point(26, 60);
            b_key_ok.Text = "OK";
            b_key_ok.AutoSize = true;
            b_key_ok.Click += b_key_ok_Click;
            frm_key_input.Controls.Add(b_key_ok);
            b_key_ok.TabStop = false;
        }

        private void b_key_ok_Click(object sender, EventArgs e)
        {
            cb_startstop_SelectedIndexChanged(null, null);
            cb_hot_prof_SelectedIndexChanged(null, null);
            //frm_key_input.Close();
            lb_lang_name.Focus();
        }

        private void form_open(object sender)//22.04.2015
        {
            //UnregisterHotKey(cb_startstop.Handle, 0);
            //UnregisterHotKey(cb_hot_prof.Handle, 1);
            //UnregisterHotKey(cb_hot_prof.Handle, 2);
            //UnregisterHotKey(cb_hot_prof.Handle, 3);
            //d3hot_FormClosing(null, null);
            for (int i = 0; i < 4; i++)
            {
                UnregisterHotKey(this.Handle, i);    
            }
            lb_key_prev.Text = "";

            if (sender == cb_key1) key_number = 0;
            if (sender == cb_key2) key_number = 1;
            if (sender == cb_key3) key_number = 2;
            if (sender == cb_key4) key_number = 3;
            if (sender == cb_key5) key_number = 4;
            if (sender == cb_key6) key_number = 5;
            if (sender == cb_tp) key_number = 6;
            if (sender == cb_map) key_number = 7;
            if (sender == cb_key_delay) key_number = 8;

            //frm_key_input.Visible = true;
            //test.Size = new System.Drawing.Size(100, 100);
            frm_key_input.Location = this.Location;
            frm_key_input.Show();
            frm_key_input.BringToFront();
        }

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None
                && e.KeyData != Keys.F1 && e.KeyData != Keys.F2 && e.KeyData != Keys.F3 && e.KeyData != Keys.F4 && e.KeyData != Keys.F5 && e.KeyData != Keys.F6
                && e.KeyData != Keys.F7 && e.KeyData != Keys.F8 && e.KeyData != Keys.F9 && e.KeyData != Keys.F10 && e.KeyData != Keys.F11 && e.KeyData != Keys.F12
                && e.KeyData != Keys.Scroll && e.KeyData != Keys.NumLock && e.KeyData != Keys.Capital && e.KeyData != Keys.CapsLock && e.KeyData != Keys.Escape
                )
            {
                lb_key_prev.Text = e.KeyData.ToString();

                string key = e.KeyData.ToString();

                bool key_exist = false;
                //for (int i = 0; i < 9; i++) //Проверка на существование клавиши
                //{
                //    if (key_list[i] == key) key_exist = true;
                //}

                if (key_exist) lb_key_prev.Text = lng.lb_key_prev_used;//"Клавиша занята!"
                else
                {
                    key_list[key_number] = key;
                    switch (key_number)
                    {
                        case 0:
                            //if (cb_1.Items.Count > 1) cb_1.Items.Remove(cb_1.Items[1]);
                            if (cb_key1.FindString("*") > 0) cb_key1.Items.Remove(cb_key1.Items[cb_key1.FindString("*")]);
                            if (cb_key1.FindString("*") > 0) cb_key1.Items.RemoveAt(cb_key1.FindString("*"));
                            cb_key1.Items.Add("*" + key);
                            cb_key1.SelectedIndex = cb_key1.FindString("*");
                            //key1_v = (VirtualKeyCode)e.KeyValue;
                            //key1_h = e.KeyValue;
                            //    Keys test = (Keys)(new KeysConverter()).ConvertFromString(key);
                            //    MessageBox.Show(((int)e.KeyData).ToString() + " " + (VirtualKeyCode)e.KeyData);
                            //    MessageBox.Show(key1_h + " " + key1_v);
                            //    MessageBox.Show(((int)Keys.Tab).ToString() + " " + VirtualKeyCode.TAB.ToString());
                            //    MessageBox.Show((int)test + " " + (VirtualKeyCode)test);

                            key_choose_SelectionChangeCommitted(cb_key1, null);
                            Settings.Default.cb_key1_desc = (string)cb_key1.Items[cb_key1.SelectedIndex];
                            break;
                        case 1:
                            if (cb_key2.FindString("*") > 0) cb_key2.Items.Remove(cb_key2.Items[cb_key2.FindString("*")]);
                            cb_key2.Items.Add("*" + key);
                            cb_key2.SelectedIndex = cb_key2.FindString("*");
                            //key2_v = (VirtualKeyCode)e.KeyValue;
                            //key2_h = e.KeyValue;
                            key_choose_SelectionChangeCommitted(cb_key2, null);
                            Settings.Default.cb_key2_desc = (string)cb_key2.Items[cb_key2.SelectedIndex];
                            break;
                        case 2:
                            if (cb_key3.FindString("*") > 0) cb_key3.Items.Remove(cb_key3.Items[cb_key3.FindString("*")]);
                            cb_key3.Items.Add("*" + key);
                            cb_key3.SelectedIndex = cb_key3.FindString("*");
                            //key3_v = (VirtualKeyCode)e.KeyValue;
                            //key3_h = e.KeyValue;
                            key_choose_SelectionChangeCommitted(cb_key3, null);
                            Settings.Default.cb_key3_desc = (string)cb_key3.Items[cb_key3.SelectedIndex];
                            break;
                        case 3:
                            if (cb_key4.FindString("*") > 0) cb_key4.Items.Remove(cb_key4.Items[cb_key4.FindString("*")]);
                            cb_key4.Items.Add("*" + key);
                            cb_key4.SelectedIndex = cb_key4.FindString("*");
                            //key4_v = (VirtualKeyCode)e.KeyValue;
                            //key4_h = e.KeyValue;
                            key_choose_SelectionChangeCommitted(cb_key4, null);
                            Settings.Default.cb_key4_desc = (string)cb_key4.Items[cb_key4.SelectedIndex];
                            break;
                        case 4:
                            if (cb_key5.FindString("*") > 0) cb_key5.Items.Remove(cb_key5.Items[cb_key5.FindString("*")]);
                            cb_key5.Items.Add("*" + key);
                            cb_key5.SelectedIndex = cb_key5.FindString("*");
                            //key5_v = (VirtualKeyCode)e.KeyValue;
                            //key5_h = e.KeyValue;
                            key_choose_SelectionChangeCommitted(cb_key5, null);
                            Settings.Default.cb_key5_desc = (string)cb_key5.Items[cb_key5.SelectedIndex];
                            break;
                        case 5:
                            if (cb_key6.FindString("*") > 0) cb_key6.Items.Remove(cb_key6.Items[cb_key6.FindString("*")]);
                            cb_key6.Items.Add("*" + key);
                            cb_key6.SelectedIndex = cb_key6.FindString("*");
                            //key6_v = (VirtualKeyCode)e.KeyValue;
                            //key6_h = e.KeyValue;
                            key_choose_SelectionChangeCommitted(cb_key6, null);
                            Settings.Default.cb_key6_desc = (string)cb_key6.Items[cb_key6.SelectedIndex];
                            break;
                        case 6:
                            if (cb_tp.FindString("*") > 0) cb_tp.Items.Remove(cb_tp.Items[cb_tp.FindString("*")]);
                            cb_tp.Items.Add("*" + key);
                            cb_tp.SelectedIndex = cb_tp.FindString("*");
                            key_choose_SelectionChangeCommitted(cb_tp, null);
                            Settings.Default.cb_tp_desc = (string)cb_tp.Items[cb_tp.SelectedIndex];
                            break;
                        case 7:
                            if (cb_map.FindString("*") > 0) cb_map.Items.Remove(cb_map.Items[cb_map.FindString("*")]);
                            cb_map.Items.Add("*" + key);
                            cb_map.SelectedIndex = cb_map.FindString("*");
                            key_choose_SelectionChangeCommitted(cb_map, null);
                            Settings.Default.cb_map_desc = (string)cb_map.Items[cb_map.SelectedIndex];
                            break;
                        case 8:
                            if (cb_key_delay.FindString("*") > 0) cb_key_delay.Items.Remove(cb_key_delay.Items[cb_key_delay.FindString("*")]);
                            cb_key_delay.Items.Add("*" + key);
                            cb_key_delay.SelectedIndex = cb_key_delay.FindString("*");
                            key_choose_SelectionChangeCommitted(cb_key_delay, null);
                            Settings.Default.cb_key_delay_desc = (string)cb_key_delay.Items[cb_key_delay.SelectedIndex];
                            break;
                    }
                    Settings.Default.Save();
                }
            }
            else
                lb_key_prev.Text = lng.lb_key_prev_err; //"Недопустимая клавиша!"
        }

        private void form_move(object sender, EventArgs e)//22.04.2015
        {
            frm_key_input.DesktopLocation = new Point(this.Location.X + 5, this.Location.Y + 25);
        }

        protected void form_Closing(object sender, FormClosingEventArgs e)
        {
            //((Form)sender).Visible = false;
            //this.Show();
        }

        private void form_Deactivate(object sender, EventArgs e)
        {
            //frm_key_input.Visible = false;
            if (!this.Disposing && !this.IsDisposed)
            {
                //((Form)sender).Hide();
                //((Form)sender).Visible = false;
                //((Form)sender).SendToBack();
                cb_startstop_SelectedIndexChanged(null, null);
                cb_hot_prof_SelectedIndexChanged(null, null);
            }
        }
    }
}
