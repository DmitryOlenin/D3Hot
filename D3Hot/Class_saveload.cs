using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using D3Hot.Properties;
using System.Xml.Serialization;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public string path = "";
        public bool loading = false;

        /// <summary>
        /// Метод для сохранения настроек. Параметр i=1 для сохранения настроек при выходе.
        /// </summary>
        /// <param name="i"></param>
        public void Save_settings(int i)
        {
            foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;
            Settings.Default.cb_key1 = cb_key1.SelectedIndex;
            Settings.Default.cb_key2 = cb_key2.SelectedIndex;
            Settings.Default.cb_key3 = cb_key3.SelectedIndex;
            Settings.Default.cb_key4 = cb_key4.SelectedIndex;
            Settings.Default.cb_key5 = cb_key5.SelectedIndex;
            Settings.Default.cb_key6 = cb_key6.SelectedIndex;

            if (cb_key1.SelectedIndex > -1 && cb_key1.Items[cb_key1.SelectedIndex].ToString().Contains("*")) //09.09.2015
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

            if (cb_trig_tmr1.SelectedIndex > -1 && cb_trig_tmr1.Items[cb_trig_tmr1.SelectedIndex].ToString().Contains("*")) //09.09.2015
                Settings.Default.cb_trig_tmr1_desc = cb_trig_tmr1.Items[cb_trig_tmr1.SelectedIndex].ToString();
            if (cb_trig_tmr2.SelectedIndex > -1 && cb_trig_tmr2.Items[cb_trig_tmr2.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr2_desc = cb_trig_tmr2.Items[cb_trig_tmr2.SelectedIndex].ToString();
            if (cb_trig_tmr3.SelectedIndex > -1 && cb_trig_tmr3.Items[cb_trig_tmr3.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr3_desc = cb_trig_tmr3.Items[cb_trig_tmr3.SelectedIndex].ToString();
            if (cb_trig_tmr4.SelectedIndex > -1 && cb_trig_tmr4.Items[cb_trig_tmr4.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr4_desc = cb_trig_tmr4.Items[cb_trig_tmr4.SelectedIndex].ToString();
            if (cb_trig_tmr5.SelectedIndex > -1 && cb_trig_tmr5.Items[cb_trig_tmr5.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_trig_tmr5_desc = cb_trig_tmr5.Items[cb_trig_tmr5.SelectedIndex].ToString();
            if (cb_trig_tmr6.SelectedIndex > -1 && cb_trig_tmr6.Items[cb_trig_tmr6.SelectedIndex].ToString().Contains("*"))
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
            Settings.Default.cb_tp = (string)cb_tp.Text;
            Settings.Default.cb_tpdelay = cb_tpdelay.SelectedIndex;
            Settings.Default.cb_map = (string)cb_map.Text;
            Settings.Default.cb_mapdelay = cb_mapdelay.SelectedIndex;
            Settings.Default.cb_press_type = cb_press_type.SelectedIndex; //06.05.2016
            Settings.Default.chb_tray = chb_tray.Checked ? 1 : 0;
            Settings.Default.chb_mult = chb_mult.Checked ? 1 : 0;
            Settings.Default.nud_key_delay_ms = nud_key_delay_ms.Value;
            Settings.Default.cb_key_delay = (string)cb_key_delay.Text;//cb_key_delay.SelectedIndex; //09.09.2015
            Settings.Default.cb_returndelay = cb_returndelay.SelectedIndex;

            if (cb_tp.SelectedIndex > -1 && cb_tp.Items[cb_tp.SelectedIndex].ToString().Contains("*")) //18.11.2015
                Settings.Default.cb_tp_desc = cb_tp.Items[cb_tp.SelectedIndex].ToString();
            if (cb_map.SelectedIndex > -1 && cb_map.Items[cb_map.SelectedIndex].ToString().Contains("*"))
                Settings.Default.cb_map_desc = cb_map.Items[cb_map.SelectedIndex].ToString();
            if (cb_key_delay.SelectedIndex > -1 && cb_key_delay.Items[cb_key_delay.SelectedIndex].ToString().Contains("*"))
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

            if (this.Location.X > 0) Settings.Default.pos_x = this.Location.X;
            if (this.Location.Y > 0) Settings.Default.pos_y = this.Location.Y;

            if (!(pan_hold.Visible &&
                (lb_hold.Text == lng.lb_hold_hot || lb_hold.Text == lng.lb_hold_trig || lb_hold.Text == lng.lb_hold_delay || lb_hold.Text == lng.lb_hold_key)
                ))
            {
                Settings.Default.Save();
                curr_save();
            }
        }

        /// <summary>
        /// Метод для сохранения настроек в XML-файл.
        /// </summary>
        public void curr_save()
        {
            if (Settings.Default.prof_curr > 0)
            {
                using (overview = new SettingsTable())
                {

                    DataTable Strings = overview.dataset.Tables.Add("Strings");
                    Strings.Columns.Add("Key");
                    Strings.Columns.Add("Value", typeof(String));

                    DataTable Decimals = overview.dataset.Tables.Add("Decimals");
                    Decimals.Columns.Add("Key");
                    Decimals.Columns.Add("Value", typeof(Decimal));

                    DataTable Ints = overview.dataset.Tables.Add("Ints");
                    Ints.Columns.Add("Key");
                    Ints.Columns.Add("Value", typeof(int));

                    foreach (SettingsProperty currentProperty in Settings.Default.Properties)
                    {
                        if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.String")
                        {
                            Strings.Rows.Add(currentProperty.Name.ToString(), Settings.Default[currentProperty.Name].ToString());
                        }
                        if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.Decimal")
                        {
                            Decimals.Rows.Add(currentProperty.Name.ToString(), Convert.ToDecimal(Settings.Default[currentProperty.Name]));
                        }
                        if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.Int32")
                        {
                            Ints.Rows.Add(currentProperty.Name.ToString(), Convert.ToInt32(Settings.Default[currentProperty.Name]));
                        }
                    }
                    if (path == "")
                        switch (Settings.Default.prof_curr)
                        {
                            case 1: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof1.xml"); break;
                            case 2: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof2.xml"); break;
                            case 3: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof3.xml"); break;
                            case 4: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof4.xml"); break; //14.08.2015
                            case 5: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof5.xml"); break; //14.08.2015
                            case 6: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof6.xml"); break; //14.08.2015
                            case 7: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof7.xml"); break; //17.11.2015
                            case 8: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof8.xml"); break; //17.11.2015
                            case 9: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof9.xml"); break; //17.11.2015
                        }
                    if (path != "") WriteXML(path);
                    path = "";
                }
            }
        }

        private void profiles_names()
        {
            int k = cb_prof.SelectedIndex;
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
        /// Метод для загрузки настроек
        /// </summary>
        public void Load_settings()
        {

            loading = true;
            //if (Settings.Default.cb_key1 > 5) Settings.Default.cb_key1 = 5;
            //if (Settings.Default.cb_key2 > 5) Settings.Default.cb_key2 = 5;
            //if (Settings.Default.cb_key3 > 5) Settings.Default.cb_key3 = 5;
            //if (Settings.Default.cb_key4 > 5) Settings.Default.cb_key4 = 5;
            //if (Settings.Default.cb_key5 > 5) Settings.Default.cb_key5 = 5;
            //if (Settings.Default.cb_key6 > 5) Settings.Default.cb_key6 = 5;

            chb_hold.Checked = Settings.Default.chb_hold == 1 ? true : false; //27.04.2015 (перенёс повыше)
            chb_hold_CheckedChanged(null, null);

            foreach (ComboBox cb in this.pan_opt.Controls.OfType<ComboBox>())
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
            ComboBox[] cb_tr = new ComboBox[] { cb_trig_tmr1, cb_trig_tmr2, cb_trig_tmr3, cb_trig_tmr4, cb_trig_tmr5, cb_trig_tmr6 }; //08.09.2015
            string[] cb_desc = new string[] { Settings.Default.cb_trig_tmr1_desc, Settings.Default.cb_trig_tmr2_desc, Settings.Default.cb_trig_tmr3_desc,
                                    Settings.Default.cb_trig_tmr4_desc,Settings.Default.cb_trig_tmr5_desc,Settings.Default.cb_trig_tmr6_desc };

            for (int i = 0; i < 6; i++)
            {
                if (cb_tr[i].FindString("*") > 0) cb_tr[i].Items.RemoveAt(cb_tr[i].FindString("*"));
                if (cb_desc[i].Length > 0) star_add(cb_tr[i]);
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
                Point tmpLocation = this.Location;
                tmpLocation.X = Settings.Default.pos_x;
                tmpLocation.Y = Settings.Default.pos_y;
                this.Location = tmpLocation;
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

            cb_tmr1.SelectedIndex = -1; cb_tmr2.SelectedIndex = -1; cb_tmr3.SelectedIndex = -1; 
            cb_tmr4.SelectedIndex = -1; cb_tmr5.SelectedIndex = -1; cb_tmr6.SelectedIndex = -1;
            
            cb_tmr1.SelectedIndex = (!resolution && Settings.Default.cb_tmr1 > 1) ? Settings.Default.cb_tmr1 - 1 : Settings.Default.cb_tmr1;
            cb_tmr2.SelectedIndex = (!resolution && Settings.Default.cb_tmr2 > 1) ? Settings.Default.cb_tmr2 - 1 : Settings.Default.cb_tmr2;
            cb_tmr3.SelectedIndex = (!resolution && Settings.Default.cb_tmr3 > 1) ? Settings.Default.cb_tmr3 - 1 : Settings.Default.cb_tmr3;
            cb_tmr4.SelectedIndex = (!resolution && Settings.Default.cb_tmr4 > 1) ? Settings.Default.cb_tmr4 - 1 : Settings.Default.cb_tmr4;
            cb_tmr5.SelectedIndex = (!resolution && Settings.Default.cb_tmr5 > 1) ? Settings.Default.cb_tmr5 - 1 : Settings.Default.cb_tmr5;
            cb_tmr6.SelectedIndex = (!resolution && Settings.Default.cb_tmr6 > 1) ? Settings.Default.cb_tmr6 - 1 : Settings.Default.cb_tmr6;
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
            chb_tray.Checked = Settings.Default.chb_tray == 1 ? true : false;
            chb_mult.Checked = Settings.Default.chb_mult == 1 ? true : false;
            nud_key_delay_ms.Value = Settings.Default.nud_key_delay_ms;
            cb_key_delay.SelectedIndex = cb_key_delay.FindStringExact(Settings.Default.cb_key_delay);//Settings.Default.cb_key_delay; //09.09.2015
            cb_returndelay.SelectedIndex = Settings.Default.cb_returndelay;

            chb_trig1.Checked = Settings.Default.chb_trig1 == 1 ? true : false; //02.09.2015 //09.09.2015
            chb_trig2.Checked = Settings.Default.chb_trig2 == 1 ? true : false; //02.09.2015 //09.09.2015
            chb_trig3.Checked = Settings.Default.chb_trig3 == 1 ? true : false; //02.09.2015 //09.09.2015
            chb_trig4.Checked = Settings.Default.chb_trig4 == 1 ? true : false; //02.09.2015 //09.09.2015
            chb_trig5.Checked = Settings.Default.chb_trig5 == 1 ? true : false; //02.09.2015 //09.09.2015
            chb_trig6.Checked = Settings.Default.chb_trig6 == 1 ? true : false; //02.09.2015 //09.09.2015

            chb_mpress.Checked = Settings.Default.chb_mpress == 1 ? true : false;
            chb_saveload.Checked = Settings.Default.chb_saveload == 1 ? true : false;
            chb_users.Checked = Settings.Default.chb_users == 1 ? true : false;
            chb_log.Checked = Settings.Default.chb_log == 1 ? true : false;

            nud_rand.Value = Settings.Default.nud_rand;
            nud_coold.Value = Settings.Default.nud_coold;
            cb_hot_prof.SelectedIndex = Settings.Default.cb_hot_prof;
            tb_prof_name.Text = Settings.Default.tb_prof_name;

            chb_ver_check.Checked = Settings.Default.chb_ver_check == 1 ? true : false;

            cb_press_type.SelectedIndex = Settings.Default.cb_press_type;  //06.05.2016

            loading = false;
        }

        public class SettingsTable : IDisposable
        {
            public DataSet dataset;
            // тут могут быть ещё ресурсы

            bool isDisposed = false;

            public SettingsTable()
            {
                //try
                //{
                    dataset = new DataSet();
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
                isDisposed = true;
            }

            // Protected implementation of Dispose pattern.
            protected virtual void Dispose(bool disposing)
            {
                if (isDisposed) // мы уже умерли? валим отсюда
                    return; // Dispose имеет право быть вызван много раз

                if (disposing)
                {

                    // освободим ресурсы
                    if (dataset != null)
                    {
                        dataset.Dispose();
                        dataset = null;
                    }
                }

                // Free any unmanaged objects here.
                //
                isDisposed = true;
            }


        }

        /// <summary>
        /// Метод для записи XML-файла.
        /// </summary>
        /// <param name="path"></param>
        public static void WriteXML(string target)
        {
            XmlSerializer writer = new XmlSerializer(typeof(SettingsTable));
            //var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            using (System.IO.FileStream file = System.IO.File.Create(target))
                writer.Serialize(file, overview);
            //file.Close();
            //overview.dataset.Dispose();
            overview.Dispose(); //06.05.2016
        }

        /// <summary>
        /// Метод для чтения из XML-файла.
        /// </summary>
        /// <param name="path"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Ликвидировать объекты перед потерей области")]
        public void ReadXML(string target)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(SettingsTable));
            using (System.IO.StreamReader file = new System.IO.StreamReader(target))
            {
                SettingsTable overview = new SettingsTable();
                using (overview = (SettingsTable)reader.Deserialize(file))
                {
                    for (int i = 0; i < overview.dataset.Tables[0].Rows.Count; i++)
                    {
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "lb_lang") Settings.Default.lb_lang = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_tp") Settings.Default.cb_tp = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_map") Settings.Default.cb_map = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key_delay") Settings.Default.cb_key_delay = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_prof_name") Settings.Default.tb_prof_name = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key1_desc") Settings.Default.cb_key1_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key2_desc") Settings.Default.cb_key2_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key3_desc") Settings.Default.cb_key3_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key4_desc") Settings.Default.cb_key4_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key5_desc") Settings.Default.cb_key5_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key6_desc") Settings.Default.cb_key6_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_tp_desc") Settings.Default.cb_tp_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_map_desc") Settings.Default.cb_map_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                        if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "cb_key_delay_desc") Settings.Default.cb_key_delay_desc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                    }
                    for (int j = 0; j < overview.dataset.Tables[1].Rows.Count; j++)
                    {
                        switch (overview.dataset.Tables[1].Rows[j][0].ToString())
                        {
                            case "nud_tmr1": Settings.Default.nud_tmr1 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                            case "nud_tmr2": Settings.Default.nud_tmr2 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                            case "nud_tmr3": Settings.Default.nud_tmr3 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                            case "nud_tmr4": Settings.Default.nud_tmr4 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                            case "nud_tmr5": Settings.Default.nud_tmr5 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                            case "nud_tmr6": Settings.Default.nud_tmr6 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                            case "nud_key_delay_ms": Settings.Default.nud_key_delay_ms = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                            case "nud_rand": Settings.Default.nud_rand = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                            case "nud_coold": Settings.Default.nud_coold = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                        }
                    }
                    for (int k = 0; k < overview.dataset.Tables[2].Rows.Count; k++)
                    {
                        switch (overview.dataset.Tables[2].Rows[k][0].ToString())
                        {
                            case "cb_trig_tmr1": Settings.Default.cb_trig_tmr1 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_trig_tmr2": Settings.Default.cb_trig_tmr2 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_trig_tmr3": Settings.Default.cb_trig_tmr3 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_trig_tmr4": Settings.Default.cb_trig_tmr4 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_trig_tmr5": Settings.Default.cb_trig_tmr5 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_trig_tmr6": Settings.Default.cb_trig_tmr6 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            //case "cb_startstop": Settings.Default.cb_startstop = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;

                            case "cb_tmr1": Settings.Default.cb_tmr1 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_tmr2": Settings.Default.cb_tmr2 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_tmr3": Settings.Default.cb_tmr3 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_tmr4": Settings.Default.cb_tmr4 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_tmr5": Settings.Default.cb_tmr5 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_tmr6": Settings.Default.cb_tmr6 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;

                            case "cb_key1": Settings.Default.cb_key1 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_key2": Settings.Default.cb_key2 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_key3": Settings.Default.cb_key3 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_key4": Settings.Default.cb_key4 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_key5": Settings.Default.cb_key5 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_key6": Settings.Default.cb_key6 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            //case "cb_key_delay": Settings.Default.cb_key_delay = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //09.09.2015

                            case "cb_prog": Settings.Default.cb_prog = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_pause": Settings.Default.cb_pause = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "prof_curr": Settings.Default.prof_curr = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_tpdelay": Settings.Default.cb_tpdelay = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_mapdelay": Settings.Default.cb_mapdelay = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "chb_tray": Settings.Default.chb_tray = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "chb_mult": Settings.Default.chb_mult = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_returndelay": Settings.Default.cb_returndelay = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "cb_press_type": Settings.Default.cb_press_type = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //06.05.2016

                            case "chb_trig1": Settings.Default.chb_trig1 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //02.09.2015
                            case "chb_trig2": Settings.Default.chb_trig2 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //02.09.2015
                            case "chb_trig3": Settings.Default.chb_trig3 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //02.09.2015
                            case "chb_trig4": Settings.Default.chb_trig4 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //02.09.2015
                            case "chb_trig5": Settings.Default.chb_trig5 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //02.09.2015
                            case "chb_trig6": Settings.Default.chb_trig6 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break; //02.09.2015

                            case "chb_hold": Settings.Default.chb_hold = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "chb_mpress": Settings.Default.chb_mpress = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "chb_log": Settings.Default.chb_log = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "chb_ver_check": Settings.Default.chb_ver_check = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "chb_saveload": Settings.Default.chb_saveload = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "chb_users": Settings.Default.chb_users = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            case "chb_proconly": Settings.Default.chb_proconly = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            //case "cb_hot_prof": Settings.Default.cb_hot_prof = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;

                            //case "pos_x": Settings.Default.pos_x = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                            //case "pos_y": Settings.Default.pos_y = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;   

                        }
                    }

                    Settings.Default.Save();
                }
            }
            Load_settings();

        }

    }
}
