using System;
using System.Drawing;
using System.Windows.Forms;

namespace D3Hot
{
    public partial class D3Hotkeys //: Form
    {
        private static int _keyNumber;

        private NonFocusButton _bKeyOk;
        private Form _frmKeyInput;

        private Label _lbKeyPrev,
            _lbKeyDesc;

        private void form_create()
        {
            _frmKeyInput = new Form();

            try
            {
                _frmKeyInput.AutoSize = true;
                _frmKeyInput.Size = new Size(180, 110);
                _frmKeyInput.MaximumSize = new Size(180, 110);
                _frmKeyInput.MinimumSize = new Size(180, 110);
                _frmKeyInput.Visible = false;
                _frmKeyInput.Deactivate += form_Deactivate;
                _frmKeyInput.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                _frmKeyInput.StartPosition = FormStartPosition.Manual;
                _frmKeyInput.ShowInTaskbar = false;
                _frmKeyInput.Move += form_move;
                _frmKeyInput.ControlBox = false;
                _frmKeyInput.KeyPreview = true;
                _frmKeyInput.KeyDown += form_KeyDown;
            }
            catch 
            {
                _frmKeyInput.Dispose();
                return;
            }

            _lbKeyDesc = new Label();

            try
            {
                _lbKeyDesc.Text = _lng.LbKeyDesc;
                _lbKeyDesc.Location = new Point(22, 5);
                _lbKeyDesc.AutoSize = true;
                _frmKeyInput.Controls.Add(_lbKeyDesc);
            }
            catch 
            {
                _lbKeyDesc.Dispose();
            }

            _lbKeyPrev = new Label();

            try
            {
                _lbKeyPrev.Location = new Point(20, 30);
                _lbKeyPrev.BorderStyle = BorderStyle.FixedSingle;
                _lbKeyPrev.AutoSize = false;
                _lbKeyPrev.Size = new Size(127, 17);
                _lbKeyPrev.TextAlign = ContentAlignment.MiddleCenter;
                _frmKeyInput.Controls.Add(_lbKeyPrev);
            }
            catch 
            {
                _lbKeyPrev.Dispose();
            }

            _bKeyOk = new NonFocusButton();
            try
            {
                _bKeyOk.Location = new Point(45, 60);
                _bKeyOk.Text = @"OK";
                _bKeyOk.AutoSize = true;
                _bKeyOk.Click += b_key_ok_Click;
                _bKeyOk.TabStop = false;
                _frmKeyInput.Controls.Add(_bKeyOk);
            }
            catch 
            {
                _bKeyOk.Dispose();
            }
        }

        private void b_key_ok_Click(object sender, EventArgs e)
        {
            cb_startstop_SelectedIndexChanged(null, null);
            cb_hot_prof_SelectedIndexChanged(null, null);
            lb_lang_name.Focus();
        }

        private void form_open(object sender) //22.04.2015
        {

            //d3hot_FormClosing(null, null);
            for (var i = 0; i < 4; i++)
                NativeMethods.UnregisterHotKey(Handle, i);
            _lbKeyPrev.Text = "";

            _keyNumber = key_find((ComboBox) sender);

            //frm_key_input.Visible = true;
            //test.Size = new System.Drawing.Size(100, 100);
            _frmKeyInput.Location = Location;
            _frmKeyInput.Show();
            _frmKeyInput.BringToFront();
        }

        private static int key_find(Control cb) //15.09.2015
        {
            var result = -1;
            switch (cb.Name)
            {
                case "cb_key1":
                    result = 0;
                    break;
                case "cb_key2":
                    result = 1;
                    break;
                case "cb_key3":
                    result = 2;
                    break;
                case "cb_key4":
                    result = 3;
                    break;
                case "cb_key5":
                    result = 4;
                    break;
                case "cb_key6":
                    result = 5;
                    break;
                case "cb_tp":
                    result = 6;
                    break;
                case "cb_map":
                    result = 7;
                    break;
                case "cb_key_delay":
                    result = 8;
                    break;
                case "cb_trig_tmr1":
                    result = 9;
                    break;
                case "cb_trig_tmr2":
                    result = 10;
                    break;
                case "cb_trig_tmr3":
                    result = 11;
                    break;
                case "cb_trig_tmr4":
                    result = 12;
                    break;
                case "cb_trig_tmr5":
                    result = 13;
                    break;
                case "cb_trig_tmr6":
                    result = 14;
                    break;
            }
            return result;
        }


        /// <summary>
        ///     Процедура проверки триггеров/кнопок на пересечение.
        /// </summary>
        /// <param name="keyNum"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool double_key_check(int keyNum, string key) //14.09.2015
        {
            var keyExist = false;

            if (key == _lng.CbKeysChoose) return false;
            var cbKey = new[] {cb_key1, cb_key2, cb_key3, cb_key4, cb_key5, cb_key6};
            var cbTrig = new[] {cb_trig_tmr1, cb_trig_tmr2, cb_trig_tmr3, cb_trig_tmr4, cb_trig_tmr5, cb_trig_tmr6};
            //ComboBox[] cb_other = new ComboBox[] { cb_tp, cb_map, cb_key_delay };

            string tpSel = _lng.CbKeysChoose, mapSel = _lng.CbKeysChoose, keyDelaySel = _lng.CbKeysChoose;

            if (cb_tp.SelectedItem != null) tpSel = ((string) cb_tp.SelectedItem).Replace("*", "");
            if (cb_map.SelectedItem != null) mapSel = ((string) cb_map.SelectedItem).Replace("*", "");
            if (cb_key_delay.SelectedItem != null) keyDelaySel = ((string) cb_key_delay.SelectedItem).Replace("*", "");

            if (
                tpSel == mapSel && tpSel != _lng.CbKeysChoose && mapSel != _lng.CbKeysChoose ||
                tpSel == keyDelaySel && tpSel != _lng.CbKeysChoose && keyDelaySel != _lng.CbKeysChoose ||
                mapSel == keyDelaySel && mapSel != _lng.CbKeysChoose && keyDelaySel != _lng.CbKeysChoose
            )
            {
                keyExist = true;
            }


            else
            {
                //11.01.2017 Анализ PVS Studio - переписал блок if
                if (keyNum < 6
                    && (key == ((string) cbTrig[keyNum].SelectedItem).Replace("*", "")
                        || key == tpSel || key == mapSel || key == keyDelaySel))
                    keyExist = true;
                else if (keyNum > 5 && keyNum < 9)
                    for (var i = 0; i < 6; i++)
                    {
                        if (key == ((string) cbTrig[i].SelectedItem).Replace("*", "")
                            || keyNum != 8 && key == ((string) cbKey[i].SelectedItem).Replace("*", ""))
                            keyExist = true;
                    }
                else if (keyNum > 9 && key == ((string)cbKey[keyNum - 9].SelectedItem).Replace("*", "")
                         || key.Replace("*", "") == tpSel || key.Replace("*", "") == mapSel)
                    keyExist = true;
            }

            return keyExist;
        }

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e != null && e.Modifiers == Keys.None
                && e.KeyData != Keys.F1 && e.KeyData != Keys.F2 && e.KeyData != Keys.F3 && e.KeyData != Keys.F4 &&
                e.KeyData != Keys.F5 && e.KeyData != Keys.F6
                && e.KeyData != Keys.F7 && e.KeyData != Keys.F8 && e.KeyData != Keys.F9 && e.KeyData != Keys.F10 &&
                e.KeyData != Keys.F11 && e.KeyData != Keys.F12
                && e.KeyData != Keys.Scroll && e.KeyData != Keys.NumLock && e.KeyData != Keys.Capital &&
                e.KeyData != Keys.CapsLock && e.KeyData != Keys.Escape
            )
            {
                //lb_key_prev.Text = e.KeyData.ToString(); //11.01.2017 Анализ PVS Studio - две переменные присвоение одной функции

                var key = e.KeyData.ToString();
                _lbKeyPrev.Text = key;

                var cbNew = new[]
                {
                    cb_key1, cb_key2, cb_key3, cb_key4, cb_key5, cb_key6, cb_tp, cb_map, cb_key_delay,
                    cb_trig_tmr1, cb_trig_tmr2, cb_trig_tmr3, cb_trig_tmr4, cb_trig_tmr5, cb_trig_tmr6
                };

                if (double_key_check(_keyNumber, key))
                {
                    _lbKeyPrev.Text = _lng.LbKeyPrevUsed; //"Клавиша занята!"
                }
                else
                {
                    if (cbNew[_keyNumber].FindString("*") > 0)
                        cbNew[_keyNumber].Items.Remove(cbNew[_keyNumber].Items[cbNew[_keyNumber].FindString("*")]);
                    cbNew[_keyNumber].Items.Add("*" + key);
                    cbNew[_keyNumber].SelectedIndex = cbNew[_keyNumber].FindString("*");
                    key_choose_SelectionChangeCommitted(cbNew[_keyNumber], null);
                    Save_settings(0);
                }
            }
            else
            {
                _lbKeyPrev.Text = _lng.LbKeyPrevErr; //"Недопустимая клавиша!"
            }
        }


        private void form_move(object sender, EventArgs e) //22.04.2015
        {
            _frmKeyInput.DesktopLocation = new Point(Location.X + 5, Location.Y + 28); //+5, +25
        }

        //protected void form_Closing(object sender, FormClosingEventArgs e)
        //{
        //    //((Form)sender).Visible = false;
        //    //this.Show();
        //}

        private void form_Deactivate(object sender, EventArgs e)
        {
            //frm_key_input.Visible = false;
            if (Disposing || IsDisposed) return;
            //((Form)sender).Hide();
            //((Form)sender).Visible = false;
            //((Form)sender).SendToBack();
            cb_startstop_SelectedIndexChanged(null, null);
            cb_hot_prof_SelectedIndexChanged(null, null);
        }

        //public static String[] key_list = new String[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" }; //08.09.2015
        //public int key_code1, key_code2, key_code3, key_code4, key_code5, key_code6;
        //public VirtualKeyCode vkc1, vkc2, vkc3, vkc4, vkc5, vkc6;

        private class NonFocusButton : Button
        {
            public NonFocusButton()
            {
                SetStyle(ControlStyles.Selectable, false);
            }
        }
    }
}