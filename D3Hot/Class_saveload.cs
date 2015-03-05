﻿using System;
using System.Data;
using System.Windows.Forms;
using D3Hot.Properties;
using System.Xml.Serialization;
using System.Configuration;
using System.IO; 

namespace D3Hot
{
    public partial class d3hot : Form
    {
        /// <summary>
        /// Метод для сохранения настроек. Параметр i=1 для сохранения настроек при выходе.
        /// </summary>
        /// <param name="i"></param>
        public void Save_settings(int i)
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
            if (i == 1) Settings.Default.prof_curr = cb_prof.SelectedIndex;
            Settings.Default.Save();
            curr_save();
        }

        /// <summary>
        /// Метод для сохранения настроек в XML-файл.
        /// </summary>
        public void curr_save()
        {
            if (Settings.Default.prof_curr > 0)
            {
                overview = new SettingsTable();

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
                string path = "";
                switch (Settings.Default.prof_curr)
                {
                    case 1: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof1.xml"); break;
                    case 2: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof2.xml"); break;
                    case 3: path = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof3.xml"); break;
                }
                if (path != "") WriteXML(path);
            }
        }

        /// <summary>
        /// Метод для загрузки настроек
        /// </summary>
        public void Load_settings()
        {
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
            cb_prof.SelectedIndex = Settings.Default.prof_curr;
            lb_lang.Text = Settings.Default.lb_lang;
        }

        public class SettingsTable
        {
            public DataSet dataset = new DataSet();
        }

        /// <summary>
        /// Метод для записи XML-файла.
        /// </summary>
        /// <param name="path"></param>
        public static void WriteXML(string path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(SettingsTable));
            //var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, overview);
            file.Close();
            overview.dataset.Dispose();
        }

        /// <summary>
        /// Метод для чтения из XML-файла.
        /// </summary>
        /// <param name="path"></param>
        public void ReadXML(string path)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(SettingsTable));
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            SettingsTable overview = new SettingsTable();
            overview = (SettingsTable)reader.Deserialize(file);

            for (int i = 0; i < overview.dataset.Tables[0].Rows.Count; i++)
            {
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "lb_lang") Settings.Default.lb_lang = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
            }
            for (int j = 0; j < overview.dataset.Tables[1].Rows.Count; j++)
            {
                switch (overview.dataset.Tables[1].Rows[j][0].ToString())
                {
                    case "nud_tmr1": Settings.Default.nud_tmr1 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                    case "nud_tmr2": Settings.Default.nud_tmr2 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                    case "nud_tmr3": Settings.Default.nud_tmr3 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
                    case "nud_tmr4": Settings.Default.nud_tmr4 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]); break;
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

                    case "cb_key1": Settings.Default.cb_key1 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                    case "cb_key2": Settings.Default.cb_key2 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                    case "cb_key3": Settings.Default.cb_key3 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                    case "cb_key4": Settings.Default.cb_key4 = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;

                    case "cb_prog": Settings.Default.cb_prog = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                    case "cb_pause": Settings.Default.cb_pause = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                    case "prof_curr": Settings.Default.prof_curr = Convert.ToInt32(overview.dataset.Tables[2].Rows[k][1]); break;
                }
            }

            Settings.Default.Save();
            file.Close();
            Load_settings();
        }

    }
}