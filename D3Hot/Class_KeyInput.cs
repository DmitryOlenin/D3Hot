using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public Form frm_key_input;
        public Label lb_key_prev, lb_key_desc;
        public static int key_number=0;

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
            lb_key_desc.Text = "Клавиша нажата:";
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

            if (sender == cb_1) key_number = 1;
            if (sender == cb_2) key_number = 2;

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

                string key = e.KeyData.ToString();;
                switch (key_number)
                {
                    case 1:
                        if (cb_1.Items.Count > 1) cb_1.Items.Remove(cb_1.Items[1]);
                        cb_1.Items.Add(key);
                        cb_1.SelectedIndex = 1;
                        break;
                    case 2:
                        if (cb_2.Items.Count > 1) cb_2.Items.Remove(cb_2.Items[1]);
                        cb_2.Items.Add(key);
                        cb_2.SelectedIndex = 1;
                        break;
                }
            }
        }

        private void form_move(object sender, EventArgs e)//22.04.2015
        {
            frm_key_input.DesktopLocation = new Point(this.Location.X + 5, this.Location.Y + 25);
        }

        private void form_Deactivate(object sender, EventArgs e)
        {
            //frm_key_input.Visible = false;
            if (!this.Disposing && !this.IsDisposed)
            {
                cb_startstop_SelectedIndexChanged(null, null);
                cb_hot_prof_SelectedIndexChanged(null, null);
            }
        }
    }
}
