using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using D3Hot.Properties;

namespace D3Hot
{
    public partial class D3Hotkeys //: Form
    {
        private string _path = "";
        //private bool loading = false;

        /// <summary>
        ///     Метод для сохранения настроек. Параметр i=1 для сохранения настроек при выходе.
        /// </summary>
        /// <param name="i"></param>
        private void Save_settings(int i)
        {
            foreach (var numud in Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;
            Settings.Default.cb_key1 = cb_key1.SelectedIndex;
            Settings.Default.cb_key2 = cb_key2.SelectedIndex;
            Settings.Default.cb_key3 = cb_key3.SelectedIndex;
            Settings.Default.cb_key4 = cb_key4.SelectedIndex;
            Settings.Default.cb_key5 = cb_key5.SelectedIndex;
            Settings.Default.cb_key6 = cb_key6.SelectedIndex;

            if (cb_key1.SelectedIndex > -1 && cb_key1.Items[cb_key1.SelectedIndex].ToString().Contains("*"))
                //09.09.2015
                Settings.Default.cb_key1_desc = cb_key1.Items[cb_key1.SelectedIndex].ToString();
            if (cb_key2.SelectedIndex > -1 && cb_key2.Items[cb_key2.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_key2_desc = cb_key2.Items[cb_key2.SelectedIndex].ToString();
            if (cb_key3.SelectedIndex > -1 && cb_key3.Items[cb_key3.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_key3_desc = cb_key3.Items[cb_key3.SelectedIndex].ToString();
            if (cb_key4.SelectedIndex > -1 && cb_key4.Items[cb_key4.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_key4_desc = cb_key4.Items[cb_key4.SelectedIndex].ToString();
            if (cb_key5.SelectedIndex > -1 && cb_key5.Items[cb_key5.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_key5_desc = cb_key5.Items[cb_key5.SelectedIndex].ToString();
            if (cb_key6.SelectedIndex > -1 && cb_key6.Items[cb_key6.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_key6_desc = cb_key6.Items[cb_key6.SelectedIndex].ToString();

            Settings.Default.cb_trig_tmr1 = cb_trig_tmr1.SelectedIndex;
            Settings.Default.cb_trig_tmr2 = cb_trig_tmr2.SelectedIndex;
            Settings.Default.cb_trig_tmr3 = cb_trig_tmr3.SelectedIndex;
            Settings.Default.cb_trig_tmr4 = cb_trig_tmr4.SelectedIndex;
            Settings.Default.cb_trig_tmr5 = cb_trig_tmr5.SelectedIndex;
            Settings.Default.cb_trig_tmr6 = cb_trig_tmr6.SelectedIndex;

            if (cb_trig_tmr1.SelectedIndex > -1 &&
                cb_trig_tmr1.Items[cb_trig_tmr1.SelectedIndex].ToString().Contains("*")) //09.09.2015
                Settings.Default.cb_trig_tmr1_desc = cb_trig_tmr1.Items[cb_trig_tmr1.SelectedIndex].ToString();
            if (cb_trig_tmr2.SelectedIndex > -1 &&
                cb_trig_tmr2.Items[cb_trig_tmr2.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr2_desc = cb_trig_tmr2.Items[cb_trig_tmr2.SelectedIndex].ToString();
            if (cb_trig_tmr3.SelectedIndex > -1 &&
                cb_trig_tmr3.Items[cb_trig_tmr3.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr3_desc = cb_trig_tmr3.Items[cb_trig_tmr3.SelectedIndex].ToString();
            if (cb_trig_tmr4.SelectedIndex > -1 &&
                cb_trig_tmr4.Items[cb_trig_tmr4.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr4_desc = cb_trig_tmr4.Items[cb_trig_tmr4.SelectedIndex].ToString();
            if (cb_trig_tmr5.SelectedIndex > -1 &&
                cb_trig_tmr5.Items[cb_trig_tmr5.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr5_desc = cb_trig_tmr5.Items[cb_trig_tmr5.SelectedIndex].ToString();
            if (cb_trig_tmr6.SelectedIndex > -1 &&
                cb_trig_tmr6.Items[cb_trig_tmr6.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr6_desc = cb_trig_tmr6.Items[cb_trig_tmr6.SelectedIndex].ToString();

            Settings.Default.cb_tmr1 = cb_tmr1.SelectedIndex;
            Settings.Default.cb_tmr2 = cb_tmr2.SelectedIndex;
            Settings.Default.cb_tmr3 = cb_tmr3.SelectedIndex;
            Settings.Default.cb_tmr4 = cb_tmr4.SelectedIndex;
            Settings.Default.cb_tmr5 = cb_tmr5.SelectedIndex;
            Settings.Default.cb_tmr6 = cb_tmr6.SelectedIndex;

            Settings.Default.cb_startstop = cb_startstop.SelectedIndex;
            Settings.Default.cb_prog = cb_prog.SelectedIndex;
            Settings.Default.nud_tmr1 = nud_tmr1.Value;
            Settings.Default.nud_tmr2 = nud_tmr2.Value;
            Settings.Default.nud_tmr3 = nud_tmr3.Value;
            Settings.Default.nud_tmr4 = nud_tmr4.Value;
            Settings.Default.nud_tmr5 = nud_tmr5.Value;
            Settings.Default.nud_tmr6 = nud_tmr6.Value;
            Settings.Default.lb_lang = lb_lang.Text;
            if (i == 1) Settings.Default.prof_curr = cb_prof.SelectedIndex;
            Settings.Default.cb_tp = cb_tp.Text;
            Settings.Default.cb_tpdelay = cb_tpdelay.SelectedIndex;
            Settings.Default.cb_map = cb_map.Text;
            Settings.Default.cb_mapdelay = cb_mapdelay.SelectedIndex;
            Settings.Default.cb_press_type = cb_press_type.SelectedIndex; //06.05.2016
            Settings.Default.chb_tray = chb_tray.Checked ? 1 : 0;
            Settings.Default.chb_mult = chb_mult.Checked ? 1 : 0;
            Settings.Default.nud_key_delay_ms = nud_key_delay_ms.Value;
            Settings.Default.cb_key_delay = cb_key_delay.Text; //cb_key_delay.SelectedIndex; //09.09.2015
            Settings.Default.cb_returndelay = cb_returndelay.SelectedIndex;

            if (cb_tp.SelectedIndex > -1 && cb_tp.Items[cb_tp.SelectedIndex].ToString().Contains("*")) //18.11.2015
                Settings.Default.cb_tp_desc = cb_tp.Items[cb_tp.SelectedIndex].ToString();
            if (cb_map.SelectedIndex > -1 && cb_map.Items[cb_map.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_map_desc = cb_map.Items[cb_map.SelectedIndex].ToString();
            if (cb_key_delay.SelectedIndex > -1 &&
                cb_key_delay.Items[cb_key_delay.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_key_delay_desc = cb_key_delay.Items[cb_key_delay.SelectedIndex].ToString();

            Settings.Default.chb_trig1 = chb_trig1.Checked ? 1 : 0; //02.09.2015 //09.09.2015
            Settings.Default.chb_trig2 = chb_trig2.Checked ? 1 : 0; //02.09.2015 //09.09.2015
            Settings.Default.chb_trig3 = chb_trig3.Checked ? 1 : 0; //02.09.2015 //09.09.2015
            Settings.Default.chb_trig4 = chb_trig4.Checked ? 1 : 0; //02.09.2015 //09.09.2015
            Settings.Default.chb_trig5 = chb_trig5.Checked ? 1 : 0; //02.09.2015 //09.09.2015
            Settings.Default.chb_trig6 = chb_trig6.Checked ? 1 : 0; //02.09.2015 //09.09.2015

            Settings.Default.chb_hold = chb_hold.Checked ? 1 : 0;
            Settings.Default.chb_mpress = chb_mpress.Checked ? 1 : 0;
            Settings.Default.chb_log = chb_log.Checked ? 1 : 0;
            Settings.Default.chb_saveload = chb_saveload.Checked ? 1 : 0;
            Settings.Default.chb_users = chb_users.Checked ? 1 : 0;
            Settings.Default.chb_ver_check = chb_ver_check.Checked ? 1 : 0;

            Settings.Default.nud_rand = nud_rand.Value;
            Settings.Default.nud_coold = nud_coold.Value;

            Settings.Default.cb_hot_prof = cb_hot_prof.SelectedIndex;

            Settings.Default.tb_prof_name = tb_prof_name.Text;

            if (Location.X > 0) Settings.Default.pos_x = Location.X;
            if (Location.Y > 0) Settings.Default.pos_y = Location.Y;

            if (pan_hold.Visible &&
                (lb_hold.Text == _lng.LbHoldHot || lb_hold.Text == _lng.LbHoldTrig || lb_hold.Text == _lng.LbHoldDelay ||
                 lb_hold.Text == _lng.LbHoldKey)) return;
            Settings.Default.Save();
            curr_save();
        }

        /// <summary>
        ///     Метод для сохранения настроек в XML-файл.
        /// </summary>
        private void curr_save()
        {
            if (Settings.Default.prof_curr <= 0) return;
            _overview = new SettingsTable();
            var strings = _overview.Dataset.Tables.Add("Strings");
            strings.Columns.Add("Key");
            strings.Columns.Add("Value", typeof(string));

            var decimals = _overview.Dataset.Tables.Add("Decimals");
            decimals.Columns.Add("Key");
            decimals.Columns.Add("Value", typeof(decimal));

            var ints = _overview.Dataset.Tables.Add("Ints");
            ints.Columns.Add("Key");
            ints.Columns.Add("Value", typeof(int));

            foreach (SettingsProperty currentProperty in Settings.Default.Properties)
            {
                if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.String")
                    strings.Rows.Add(currentProperty.Name, Settings.Default[currentProperty.Name].ToString());
                if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.Decimal")
                    decimals.Rows.Add(currentProperty.Name,
                        Convert.ToDecimal(Settings.Default[currentProperty.Name]));
                if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.Int32")
                    ints.Rows.Add(currentProperty.Name, Convert.ToInt32(Settings.Default[currentProperty.Name]));
            }
            if (_path == "")
                switch (Settings.Default.prof_curr)
                {
                    case 1:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof1.xml");
                        break;
                    case 2:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof2.xml");
                        break;
                    case 3:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof3.xml");
                        break;
                    case 4:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof4.xml");
                        break; //14.08.2015
                    case 5:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof5.xml");
                        break; //14.08.2015
                    case 6:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof6.xml");
                        break; //14.08.2015
                    case 7:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof7.xml");
                        break; //17.11.2015
                    case 8:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof8.xml");
                        break; //17.11.2015
                    case 9:
                        _path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof9.xml");
                        break; //17.11.2015
                }
            if (_path != "") WriteXml(_path);
            _path = "";
        }

        private void profiles_names()
        {
            var k = cb_prof.SelectedIndex;
            cb_prof.Items.Clear();
            cb_prof.Items.Add("");
            cb_prof.Items.Add(Settings.Default.profile1);
            cb_prof.Items.Add(Settings.Default.profile2);
            cb_prof.Items.Add(Settings.Default.profile3);
            cb_prof.Items.Add(Settings.Default.profile4); //14.08.2015
            cb_prof.Items.Add(Settings.Default.profile5); //14.08.2015
            cb_prof.Items.Add(Settings.Default.profile6); //14.08.2015
            cb_prof.Items.Add(Settings.Default.profile7); //16.11.2015
            cb_prof.Items.Add(Settings.Default.profile8); //16.11.2015
            cb_prof.Items.Add(Settings.Default.profile9); //16.11.2015
            cb_prof.SelectedIndex = k;
        }

        /// <summary>
        ///     Метод для загрузки настроек
        /// </summary>
        private void Load_settings()
        {
            //loading = true;
            //if (Settings.Default.cb_key1 > 5) Settings.Default.cb_key1 = 5;
            //if (Settings.Default.cb_key2 > 5) Settings.Default.cb_key2 = 5;
            //if (Settings.Default.cb_key3 > 5) Settings.Default.cb_key3 = 5;
            //if (Settings.Default.cb_key4 > 5) Settings.Default.cb_key4 = 5;
            //if (Settings.Default.cb_key5 > 5) Settings.Default.cb_key5 = 5;
            //if (Settings.Default.cb_key6 > 5) Settings.Default.cb_key6 = 5;

            chb_hold.Checked = Settings.Default.chb_hold == 1; //27.04.2015 (перенёс повыше)
            chb_hold_CheckedChanged(null, null);

            foreach (var cb in pan_opt.Controls.OfType<ComboBox>())
                switch (cb.Name)
                {
                    case "cb_tp":
                        if (cb.FindString("*") > 0) cb.Items.RemoveAt(cb.FindString("*"));
                        if (Settings.Default.cb_tp_desc.Length > 0) star_add(cb);
                        break;
                    case "cb_map":
                        if (cb.FindString("*") > 0) cb.Items.RemoveAt(cb.FindString("*"));
                        if (Settings.Default.cb_map_desc.Length > 0) star_add(cb);
                        break;
                    case "cb_key_delay":
                        if (cb.FindString("*") > 0) cb.Items.RemoveAt(cb.FindString("*"));
                        if (Settings.Default.cb_key_delay_desc.Length > 0) star_add(cb);
                        break;
                }

            //string[] cb_name = new string[] { "cb_trig_tmr1", "cb_trig_tmr2", "cb_trig_tmr3", "cb_trig_tmr4", "cb_trig_tmr5", "cb_trig_tmr6" };
            var cbTr = new[] {cb_trig_tmr1, cb_trig_tmr2, cb_trig_tmr3, cb_trig_tmr4, cb_trig_tmr5, cb_trig_tmr6};
                //08.09.2015
            var cbDesc = new[]
            {
                Settings.Default.cb_trig_tmr1_desc, Settings.Default.cb_trig_tmr2_desc,
                Settings.Default.cb_trig_tmr3_desc,
                Settings.Default.cb_trig_tmr4_desc, Settings.Default.cb_trig_tmr5_desc,
                Settings.Default.cb_trig_tmr6_desc
            };

            for (var i = 0; i < 6; i++)
            {
                if (cbTr[i].FindString("*") > 0) cbTr[i].Items.RemoveAt(cbTr[i].FindString("*"));
                if (cbDesc[i].Length > 0) star_add(cbTr[i]);
            }

            //if (Settings.Default.cb_key1_desc != null && cb_key1.FindString(Settings.Default.cb_key1_desc) < 1) 
            //    cb_key1.Items.Add(Settings.Default.cb_key1_desc);

            //if (cb_key1.FindString(Settings.Default.cb_key1_desc) < 1) //Settings.Default.cb_key1 > 21 && 
            //    cb_key1.Items.Add(Settings.Default.cb_key1_desc);
            //if (cb_key2.FindString(Settings.Default.cb_key2_desc) < 1) //Settings.Default.cb_key2 > 21 && 
            //    cb_key2.Items.Add(Settings.Default.cb_key2_desc);
            //if (cb_key3.FindString(Settings.Default.cb_key3_desc) < 1) //Settings.Default.cb_key3 > 21 && 
            //    cb_key3.Items.Add(Settings.Default.cb_key3_desc);
            //if (cb_key4.FindString(Settings.Default.cb_key4_desc) < 1) //Settings.Default.cb_key4 > 21 && 
            //    cb_key4.Items.Add(Settings.Default.cb_key4_desc);
            //if (cb_key5.FindString(Settings.Default.cb_key5_desc) < 1) //Settings.Default.cb_key5 > 21 && 
            //    cb_key5.Items.Add(Settings.Default.cb_key5_desc);
            //if (cb_key6.FindString(Settings.Default.cb_key6_desc) < 1) //Settings.Default.cb_key6 > 21 && 
            //    cb_key6.Items.Add(Settings.Default.cb_key6_desc);

            if (Settings.Default.pos_x > 0 && Settings.Default.pos_y > 0)
            {
                var tmpLocation = Location;
                tmpLocation.X = Settings.Default.pos_x;
                tmpLocation.Y = Settings.Default.pos_y;
                Location = tmpLocation;
            }

            cb_key1.SelectedIndex = Settings.Default.cb_key1;
            cb_key2.SelectedIndex = Settings.Default.cb_key2;
            cb_key3.SelectedIndex = Settings.Default.cb_key3;
            cb_key4.SelectedIndex = Settings.Default.cb_key4;
            cb_key5.SelectedIndex = Settings.Default.cb_key5;
            cb_key6.SelectedIndex = Settings.Default.cb_key6;
            cb_trig_tmr1.SelectedIndex = Settings.Default.cb_trig_tmr1;
            cb_trig_tmr2.SelectedIndex = Settings.Default.cb_trig_tmr2;
            cb_trig_tmr3.SelectedIndex = Settings.Default.cb_trig_tmr3;
            cb_trig_tmr4.SelectedIndex = Settings.Default.cb_trig_tmr4;
            cb_trig_tmr5.SelectedIndex = Settings.Default.cb_trig_tmr5;
            cb_trig_tmr6.SelectedIndex = Settings.Default.cb_trig_tmr6;

            cb_tmr1.SelectedIndex = -1;
            cb_tmr2.SelectedIndex = -1;
            cb_tmr3.SelectedIndex = -1;
            cb_tmr4.SelectedIndex = -1;
            cb_tmr5.SelectedIndex = -1;
            cb_tmr6.SelectedIndex = -1;

            cb_tmr1.SelectedIndex = !_resolution && Settings.Default.cb_tmr1 > 1
                ? Settings.Default.cb_tmr1 - 1
                : Settings.Default.cb_tmr1;
            cb_tmr2.SelectedIndex = !_resolution && Settings.Default.cb_tmr2 > 1
                ? Settings.Default.cb_tmr2 - 1
                : Settings.Default.cb_tmr2;
            cb_tmr3.SelectedIndex = !_resolution && Settings.Default.cb_tmr3 > 1
                ? Settings.Default.cb_tmr3 - 1
                : Settings.Default.cb_tmr3;
            cb_tmr4.SelectedIndex = !_resolution && Settings.Default.cb_tmr4 > 1
                ? Settings.Default.cb_tmr4 - 1
                : Settings.Default.cb_tmr4;
            cb_tmr5.SelectedIndex = !_resolution && Settings.Default.cb_tmr5 > 1
                ? Settings.Default.cb_tmr5 - 1
                : Settings.Default.cb_tmr5;
            cb_tmr6.SelectedIndex = !_resolution && Settings.Default.cb_tmr6 > 1
                ? Settings.Default.cb_tmr6 - 1
                : Settings.Default.cb_tmr6;
            cb_startstop.SelectedIndex = Settings.Default.cb_startstop;
            cb_prog.SelectedIndex = Settings.Default.cb_prog;
            nud_tmr1.Value = Settings.Default.nud_tmr1;
            nud_tmr2.Value = Settings.Default.nud_tmr2;
            nud_tmr3.Value = Settings.Default.nud_tmr3;
            nud_tmr4.Value = Settings.Default.nud_tmr4;
            nud_tmr5.Value = Settings.Default.nud_tmr5;
            nud_tmr6.Value = Settings.Default.nud_tmr6;

            cb_prof.SelectedIndex = Settings.Default.prof_curr;
            lb_lang.Text = Settings.Default.lb_lang;
            cb_tp.SelectedIndex = cb_tp.FindStringExact(Settings.Default.cb_tp);
            cb_tpdelay.SelectedIndex = Settings.Default.cb_tpdelay;
            cb_map.SelectedIndex = cb_map.FindStringExact(Settings.Default.cb_map);
            cb_mapdelay.SelectedIndex = Settings.Default.cb_mapdelay;
            chb_tray.Checked = Settings.Default.chb_tray == 1; // ? true : false
            chb_mult.Checked = Settings.Default.chb_mult == 1;
            nud_key_delay_ms.Value = Settings.Default.nud_key_delay_ms;
            cb_key_delay.SelectedIndex = cb_key_delay.FindStringExact(Settings.Default.cb_key_delay);
                //Settings.Default.cb_key_delay; //09.09.2015
            cb_returndelay.SelectedIndex = Settings.Default.cb_returndelay;

            chb_trig1.Checked = Settings.Default.chb_trig1 == 1; //02.09.2015 //09.09.2015
            chb_trig2.Checked = Settings.Default.chb_trig2 == 1; //02.09.2015 //09.09.2015
            chb_trig3.Checked = Settings.Default.chb_trig3 == 1; //02.09.2015 //09.09.2015
            chb_trig4.Checked = Settings.Default.chb_trig4 == 1; //02.09.2015 //09.09.2015
            chb_trig5.Checked = Settings.Default.chb_trig5 == 1; //02.09.2015 //09.09.2015
            chb_trig6.Checked = Settings.Default.chb_trig6 == 1; //02.09.2015 //09.09.2015

            chb_mpress.Checked = Settings.Default.chb_mpress == 1;
            chb_saveload.Checked = Settings.Default.chb_saveload == 1;
            chb_users.Checked = Settings.Default.chb_users == 1;
            chb_log.Checked = Settings.Default.chb_log == 1;

            nud_rand.Value = Settings.Default.nud_rand;
            nud_coold.Value = Settings.Default.nud_coold >= nud_coold.Minimum ? Settings.Default.nud_coold : nud_coold.Minimum;
            cb_hot_prof.SelectedIndex = Settings.Default.cb_hot_prof;
            tb_prof_name.Text = Settings.Default.tb_prof_name;

            chb_ver_check.Checked = Settings.Default.chb_ver_check == 1;

            cb_press_type.SelectedIndex = Settings.Default.cb_press_type; //06.05.2016

            //loading = false;
        }

        /// <summary>
        ///     Метод для записи XML-файла.
        /// </summary>
        /// <param name="target"></param>
        private static void WriteXml(string target)
        {
            var writer = new XmlSerializer(typeof(SettingsTable));
            //var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            using (var file = File.Create(target))
            {
                writer.Serialize(file, _overview);
            }
            //file.Close();
            //overview.dataset.Dispose();
            _overview.Dispose(); //06.05.2016
        }



        /// <summary>
        ///     Метод для чтения из XML-файла.
        /// </summary>
        private void ReadXml(string target)
        {
            var reader = new XmlSerializer(typeof(SettingsTable));
                    //System.IO.StreamReader file = new System.IO.StreamReader(target);
                    //SettingsTable overview = new SettingsTable();
                    //overview = (SettingsTable)reader.Deserialize(file);
            using (var file = new StreamReader(target))
            {
                using (_overview = (SettingsTable) reader.Deserialize(file))
                {
                    if (_overview.Dataset == null || _overview.Dataset.Tables.Count == 0) //Старый файл с настройками, переписываем. 19.01.2017
                    {
                        try
                        {
                            file.Dispose();
                            File.Delete(target);
                            Save_settings(1);
                        }
                        catch 
                        {
                            //ignored
                        }
                        return;
                    }

                    //_overview = (SettingsTable) reader.Deserialize(file);
                    for (var i = 0; i < _overview.Dataset.Tables[0].Rows.Count; i++)
                    {
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "lb_lang")
                            Settings.Default.lb_lang = _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_tp")
                            Settings.Default.cb_tp = _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_map")
                            Settings.Default.cb_map = _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key_delay")
                            Settings.Default.cb_key_delay =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_prof_name")
                            Settings.Default.tb_prof_name =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key1_desc")
                            Settings.Default.cb_key1_desc =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key2_desc")
                            Settings.Default.cb_key2_desc =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key3_desc")
                            Settings.Default.cb_key3_desc =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key4_desc")
                            Settings.Default.cb_key4_desc =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key5_desc")
                            Settings.Default.cb_key5_desc =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key6_desc")
                            Settings.Default.cb_key6_desc =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_tp_desc")
                            Settings.Default.cb_tp_desc = _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_map_desc")
                            Settings.Default.cb_map_desc = _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (_overview.Dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key_delay_desc")
                            Settings.Default.cb_key_delay_desc =
                                _overview.Dataset.Tables["Strings"].Rows[i][1].ToString();
                    }
                    for (var j = 0; j < _overview.Dataset.Tables[1].Rows.Count; j++)
                        switch (_overview.Dataset.Tables[1].Rows[j][0].ToString())
                        {
                            case "nud_tmr1":
                                Settings.Default.nud_tmr1 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_tmr2":
                                Settings.Default.nud_tmr2 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_tmr3":
                                Settings.Default.nud_tmr3 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_tmr4":
                                Settings.Default.nud_tmr4 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_tmr5":
                                Settings.Default.nud_tmr5 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_tmr6":
                                Settings.Default.nud_tmr6 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_key_delay_ms":
                                Settings.Default.nud_key_delay_ms =
                                    Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_rand":
                                Settings.Default.nud_rand = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_coold": 
                                Settings.Default.nud_coold = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_time_0":
                                Settings.Default.nud_trig_time_0 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_time_1":
                                Settings.Default.nud_trig_time_1 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_time_2":
                                Settings.Default.nud_trig_time_2 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_time_3":
                                Settings.Default.nud_trig_time_3 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_time_4":
                                Settings.Default.nud_trig_time_4 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_time_5":
                                Settings.Default.nud_trig_time_5 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_delay_0":
                                Settings.Default.nud_trig_delay_0 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_delay_1":
                                Settings.Default.nud_trig_delay_1 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_delay_2":
                                Settings.Default.nud_trig_delay_2 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_delay_3":
                                Settings.Default.nud_trig_delay_3 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_delay_4":
                                Settings.Default.nud_trig_delay_4 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;
                            case "nud_trig_delay_5":
                                Settings.Default.nud_trig_delay_5 = Convert.ToDecimal(_overview.Dataset.Tables[1].Rows[j][1]);
                                break;

                        }
                    for (var k = 0; k < _overview.Dataset.Tables[2].Rows.Count; k++)
                        switch (_overview.Dataset.Tables[2].Rows[k][0].ToString())
                        {
                            case "cb_trig_tmr1":
                                Settings.Default.cb_trig_tmr1 =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_trig_tmr2":
                                Settings.Default.cb_trig_tmr2 =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_trig_tmr3":
                                Settings.Default.cb_trig_tmr3 =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_trig_tmr4":
                                Settings.Default.cb_trig_tmr4 =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_trig_tmr5":
                                Settings.Default.cb_trig_tmr5 =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_trig_tmr6":
                                Settings.Default.cb_trig_tmr6 =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            //case "cb_startstop": Settings.Default.cb_startstop = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;

                            case "cb_tmr1":
                                Settings.Default.cb_tmr1 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_tmr2":
                                Settings.Default.cb_tmr2 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_tmr3":
                                Settings.Default.cb_tmr3 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_tmr4":
                                Settings.Default.cb_tmr4 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_tmr5":
                                Settings.Default.cb_tmr5 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_tmr6":
                                Settings.Default.cb_tmr6 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;

                            case "cb_key1":
                                Settings.Default.cb_key1 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_key2":
                                Settings.Default.cb_key2 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_key3":
                                Settings.Default.cb_key3 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_key4":
                                Settings.Default.cb_key4 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_key5":
                                Settings.Default.cb_key5 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_key6":
                                Settings.Default.cb_key6 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            //case "cb_key_delay": Settings.Default.cb_key_delay = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //09.09.2015

                            case "cb_prog":
                                Settings.Default.cb_prog = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_pause":
                                Settings.Default.cb_pause = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "prof_curr":
                                Settings.Default.prof_curr = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_tpdelay":
                                Settings.Default.cb_tpdelay = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_mapdelay":
                                Settings.Default.cb_mapdelay =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_tray":
                                Settings.Default.chb_tray = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_mult":
                                Settings.Default.chb_mult = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_returndelay":
                                Settings.Default.cb_returndelay =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "cb_press_type":
                                Settings.Default.cb_press_type =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break; //06.05.2016

                            case "chb_trig1":
                                Settings.Default.chb_trig1 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break; //02.09.2015
                            case "chb_trig2":
                                Settings.Default.chb_trig2 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break; //02.09.2015
                            case "chb_trig3":
                                Settings.Default.chb_trig3 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break; //02.09.2015
                            case "chb_trig4":
                                Settings.Default.chb_trig4 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break; //02.09.2015
                            case "chb_trig5":
                                Settings.Default.chb_trig5 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break; //02.09.2015
                            case "chb_trig6":
                                Settings.Default.chb_trig6 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break; //02.09.2015

                            case "chb_hold":
                                Settings.Default.chb_hold = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_mpress":
                                Settings.Default.chb_mpress = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_log":
                                Settings.Default.chb_log = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_ver_check":
                                Settings.Default.chb_ver_check =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_saveload":
                                Settings.Default.chb_saveload =
                                    Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_users":
                                Settings.Default.chb_users = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_proconly":
                                Settings.Default.chb_proconly = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_trig_once_0":
                                Settings.Default.chb_trig_once_0 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_trig_once_1":
                                Settings.Default.chb_trig_once_1 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_trig_once_2":
                                Settings.Default.chb_trig_once_2 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_trig_once_3":
                                Settings.Default.chb_trig_once_3 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_trig_once_4":
                                Settings.Default.chb_trig_once_4 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            case "chb_trig_once_5":
                                Settings.Default.chb_trig_once_5 = Convert.ToInt32(_overview.Dataset.Tables[2].Rows[k][1]);
                                break;
                            //case "cb_hot_prof": Settings.Default.cb_hot_prof = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;

                            //case "pos_x": Settings.Default.pos_x = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            //case "pos_y": Settings.Default.pos_y = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;   
                        }
                    Settings.Default.Save();
                }
            }
            Load_settings();
        }

        //public class SettingsTable
        //{
        //    public DataSet Dataset = new DataSet();
        //}

        // ReSharper disable once MemberCanBePrivate.Global - Нужно для сохранения XML в файл, private не работает
        public class SettingsTable : IDisposable
        {
            // тут могут быть ещё ресурсы

            private bool _isDisposed;
            public DataSet Dataset;

            public SettingsTable()
            {
                //try
                //{
                Dataset = new DataSet();
                //}
                //catch
                //{
                //    Dispose();
                //    throw;
                //}
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this); //говорим сборщику мусора, что наш объект уже освободил ресурсы

                // и запомним, что мы уже умерли
                _isDisposed = true;
            }

            // Protected implementation of Dispose pattern.
            private void Dispose(bool disposing)
            {
                if (_isDisposed) // мы уже умерли? валим отсюда
                    return; // Dispose имеет право быть вызван много раз

                if (disposing)
                    if (Dataset != null)
                    {
                        Dataset.Dispose();
                        Dataset = null;
                    }

                // Free any unmanaged objects here.
                //
                _isDisposed = true;
            }

            ~SettingsTable() //Финализатор/Деструктор
            {
                Dispose(false);
            }
        }
    }
}