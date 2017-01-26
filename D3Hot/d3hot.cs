using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using D3Hot.Properties;
using Timer = System.Timers.Timer;

//using System.Reflection;

//using InputManager; //26.03.2015
//InputManager - http://www.codeproject.com/Articles/117657/InputManager-library-Track-user-input-and-simulate

namespace D3Hot
{
    public partial class D3Hotkeys : Form
    {
        private const double Ver = 2.5;

        //private static readonly string FileNameExe = Assembly.GetExecutingAssembly().Location;
        //public static string FileNameHlp = Path.ChangeExtension(FileNameExe, "chm");

        private static bool _hotkeyPressed,
            _profNameChanged,
            _formShown,
            _startMain = true,
            _startOpt = true,
            _procSelected,
            _lmousehold,
            _rmousehold,
            _holded,
            _crossTrig,
            _debug,
            _debugPic,
            _progRun = true,
            _progStart,
            _tmrCdrDisposing,
            _tmrCdrRunning,
            //_keyPressed,
            _d3Prog,
            _d3Proc,
            _resolution = true,
            _langLoad,
            _wasUsed,
            _logEnabled
            //stopped_once
            //pic_get = false, 
            //was_cdr
            //shift = false, 
            ;

        //private const bool Warframe = false;


        private static int _tPress,
            _mapPress,
            _returnPress,
            _rPress,
            _returnPressCount,
            _delayPress,
            _delayPressInterval,
            _shiftPress,
            _delayWait,
            _trigPressedSum,
            _trigPressedSumDelay,
            _currTrig = -1,
            _tpDelay,
            //_tmrAllCounter,
            _mapDelay,
            _returnDelay,
            _multikeys,
            _optChange,
            _optClick,
            _aspectRatio,
            _pressType
            //_startStop
            //pause = 0,
            //prof_prev,
            //tmr_all_count,
            //cdr_count = 0,
            //WidthScreen,
            //HeightScreen,
            //_tmrCdrCurr,
            ;

        private static string _tpKey = "",
            _mapKey = "",
            _procCurr = "",
            _keyDelay = "",
            _verClick = "",
            _diabloName = "diablo"
            ;

        private static readonly int[]
            CdrKey = {0, 0, 0, 0, 0, 0},
            //Массив триггеров, отмеченных для кулдауна (int) - обновление при выборе куладуна (или старте всей программы)
            CdrRun = {0, 0, 0, 0, 0, 0},
            //Массив  работающих триггеров, отмеченных для кулдауна (int) - обновление при каждом круге главного таймера (tmr_all_Elapsed)
            FKeys = new int[11],
            //Массив клавиш F1-F12
            Hold = {0, 0, 0, 0, 0, 0},
            //Массив триггеров, отмеченных для зажатия (int) - обновление при каждом круге главного таймера (tmr_all_Elapsed)
            HoldKey = {0, 0, 0, 0, 0, 0},
            //Массив триггеров, отмеченных для зажатия (int) - обновление при установке галочки зажатия (или старте всей программы)
            TmrLeft = {0, 0, 0, 0, 0, 0},
            //Массив остатков времени по таймерам
            TrigUsed = {0, 0, 0, 0, 0, 0},
            //Массив триггеров, которые были нажаты недавно, а сейчас неактивны
            Trig = {0, 0, 0, 0, 0, 0},
            //Массив кнопок выбранных триггеров (int)  - обновление при запуске (Start)
            TmrR = {0, 0, 0, 0, 0, 0},
            //Массив таймеров для обычного прожимания - обновление в процессе работы главного таймера (tmr_all_Elapsed)
            TmrF = {0, 0, 0, 0, 0, 0},
            //Массив таймеров, которые вообще могут быть нажаты  - обновление при запуске (Start)
            TmrPress = {0, 0, 0, 0, 0, 0},
            //Массив таймеров, которые прожимаются в данный момент
            TmrOnce = {0, 0, 0, 0, 0, 0} //Пока не используется
            //TmrCdrN = {0, 0, 0, 0, 0, 0} //30.06.2015
            ;

        private static int[] _trigPressed = {0, 0, 0, 0, 0, 0}; //01.07.2015

        private static SettingsTable _overview;
        //public static int[] trig_cdr_pressed = {0, 0, 0, 0, 0, 0}; //17.06.2016

        private static Stopwatch[] _tmrWatch, _cdrWatch; //09.07.2015
        private static Stopwatch _returnWatch, _keyWatch, _mapWatch, _teleWatch, _hoverWatch;
        private static readonly uint[] KeyH = {0, 0, 0, 0, 0, 0}; //09.07.2015 //08.09.2015
        private static readonly VirtualKeyCode[] KeyV = {0, 0, 0, 0, 0, 0}; //09.07.2015 
        private static readonly string[] KeySt = {"", "", "", "", "", ""}; //08.09.2015

        private static readonly bool[] TrigPress = {false, false, false, false, false, false},
            //Массив нажатых триггеров (boolean) - обновление при каждом нажатии клавиши - обновление в процессе работы главного таймера (tmr_all_Elapsed)
            TrigHold = {false, false, false, false, false, false};
        //Массив триггеров для зажатия (boolean) - обновление при установке галочки зажатия (или старте всей программы) - не обнуляем

        private readonly InputSimulator _inp = new InputSimulator();

        private readonly ClassLang _lng = new ClassLang();

        private readonly long _procSize = 400000000;

        private readonly string _sep =
            Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).ToString();

        private readonly double[] _tmrI = {0, 0, 0, 0, 0, 0};
        //_tmrCoold; //18.12.2015

        //public Stopwatch DelayWatch, ProcWatch; 

        private List<int> _procIdList;

        //public static object valueLocker = new object(), valueLocker1 = new object(); //16.07.2015
        //private readonly object valueLocker = new object();

        private Random _rand; //23.03.2015

        private Timer[] _tmr; //09.07.2015

        private Timer _tmrAll, _tmrCdr, _tmrHover, _tmrVer;
        //09.07.2015 //Массив интервалов таймеров (double)  - обновление при запуске (Start)

        //public System.Timers.Timer TmrSave, TmrPic, TmrPicPress;

        private readonly double _tmrCdrInt = 30; //Задержка таймера общего кулдауна и куладунов отдельных
        private double _cooldDelay = 300; //Задержка таймера общего кулдауна и куладунов отдельных
        private string _version = "Diablo 3 Hotkeys ver. " + Ver.ToString(CultureInfo.InvariantCulture);


        public D3Hotkeys()
        {
            InitializeComponent();
            //mouseKeyEventProvider1.Enabled = false;
            MaximizeBox = false;
        }

        private void rand_interval(int i)
        {
            if (_tmr[i] == null) return;
            var rnd = 0;
            if (nud_rand.Value > 0)
            {
                _rand = new Random();
                rnd = _rand.Next(-(int) nud_rand.Value, (int) nud_rand.Value);
                if (rnd + _tmrI[i] < 1) rnd = 31 - (int) _tmrI[i];
            }
            if (_tmrI[i] + rnd > 0) _tmr[i].Interval = _tmrI[i] + rnd; //31.05.2016
        }

        /// <summary>
        ///     Метод запуска таймеров, установка задержки по ним.
        /// </summary>
        /// <param name="i"></param>
        private void timer_load(int i)
        {
            if (i == -1)
            {
                _tmrAll = new Timer();
                _tmrAll.Elapsed += tmr_all_Elapsed;
                _tmrAll.Interval = 30; //07.12.2015
            }
            else if (_tmr[i] == null) //Создаём таймер, только если его не было 23.01.2017
            {
                if (CdrKey[i] < 1)
                {
                    //_tmr[i] = new Timer();
                    _tmr[i] = new NumericTimer(i); //25.01.2017
                    _tmr[i].Elapsed += tmr_Elapsed;
                    rand_interval(i); //&& hold_key[i] < 1
                    _tmrWatch[i] = new Stopwatch();
                }
                else //19.06.2016
                {
                    _tmr[i] = new NumericTimer(i); //Задержка отдельных таймеров, минимум 100мс
                    try
                    {
                        _tmr[i].Interval = _cooldDelay;
                        _tmr[i].Elapsed += tmr_cdr_Elapsed;
                    }
                    catch
                    {
                        _tmr[i].Dispose();
                    }
                }
            }
        }


        //protected override void OnClosed(EventArgs e)
        //{
        //    MessageBox.Show("The form is now closing.",
        //    "Close Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //}

        /// <summary>
        ///     Метод удаления таймеров при остановке. 99 - всё. 88 - все, кроме главного потока.
        /// </summary>
        /// <param name="i"></param>
        private void timer_unload(int i)
        {
            //if (_debug)
            //{
            //    var timenow = DateTime.Now.ToLongTimeString();
            //    MessageBox.Show(timenow + @" Как будто кто-то хочет timer_unload " + i);
            //}

            var result = false;

            try
            {
                if (_tmrAll != null && (i == -1 || i == 99))
                {
                    //07.12.2005
                    if (_tmrAll.Enabled) _tmrAll.Stop(); //11.01.2017 Анализ PVS Studio - двойная проверка
                    _tmrAll.Dispose();
                    _tmrAll = null;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(@"1: " + exp);
            }

            var timerElapsed = 0;

            if (i > -1 && i < 88 && _tmr != null && _tmr[i] != null && TmrR[i] == 1)
                try
                {
                    if (_tmrWatch[i] != null)
                    {
                        result = int.TryParse(_tmrWatch[i].ElapsedMilliseconds.ToString(), out timerElapsed);
                        StopWatch(_tmrWatch[i]);
                    }
                    TmrLeft[i] = (int) _tmr[i].Interval > 0 && result ? (int) _tmr[i].Interval - timerElapsed : 0;
                        //оставшееся время до прожатия таймера (при его выгрузке)

                    _tmr[i].Stop();
                    _tmr[i].Dispose();
                    _tmr[i] = null;
                    TmrR[i] = 0; //19.01.2017
                    TmrPress[i] = 0; //Отмечаем, что таймер не прожимается в данный момент

                    //tmr_watch[i].Stop();
                }
                catch //Exception exp
                {
                    //MessageBox.Show(@"2. Номер таймера:" + i + @" Ошибка:" + exp);
                }

            else if (i > 87)
                try
                {
                    for (var j = 0; j < 6; j++)
                        if (_tmr != null && _tmr[j] != null && TmrR[j] == 1)
                        {
                            if (_tmrWatch != null && _tmrWatch[j] != null)
                            {
                                result = int.TryParse(_tmrWatch[j].ElapsedMilliseconds.ToString(), out timerElapsed);
                                StopWatch(_tmrWatch[j]);
                                _tmrWatch[j] = null;
                            }
                            TmrLeft[j] = (int) _tmr[j].Interval > 0 && result
                                ? (int) _tmr[j].Interval - timerElapsed
                                : 0;

                            _tmr[j].Dispose();
                            _tmr[j] = null;
                        }
                    Array.Clear(TmrR, 0, 6);
                    Array.Clear(TmrPress, 0, 6); //Отмечаем, что таймеры не прожимаются в данный момент
                }
                catch //(Exception exp)
                {
                    //MessageBox.Show(@"3. " + exp);
                }
        }

        /// <summary>
        ///     Метод для отслеживания нажатий T/Enter и подсчёта количества нажатий.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseKeyEventProvider1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_delayPressInterval > 0 && e.KeyCode.ToString() == _keyDelay) _delayPress = 1;
            if (e.KeyCode.ToString() == _tpKey) _tPress = 1;
            if (e.KeyCode.ToString() == _mapKey) _mapPress = 1;
            if (e.KeyCode.ToString() == "Return") _returnPress = 1;

            if (Trig[0] == -1 && KeySt[0].Length > 0 && e.KeyCode.ToString() == KeySt[0])
                TrigPress[0] = TrigHold[0] || !TrigPress[0]; //09.09.2015
            if (Trig[1] == -2 && KeySt[1].Length > 0 && e.KeyCode.ToString() == KeySt[1])
                TrigPress[1] = TrigHold[1] || !TrigPress[1]; //09.09.2015
            if (Trig[2] == -3 && KeySt[2].Length > 0 && e.KeyCode.ToString() == KeySt[2])
                TrigPress[2] = TrigHold[2] || !TrigPress[2]; //09.09.2015
            if (Trig[3] == -4 && KeySt[3].Length > 0 && e.KeyCode.ToString() == KeySt[3])
                TrigPress[3] = TrigHold[3] || !TrigPress[3]; //09.09.2015
            if (Trig[4] == -5 && KeySt[4].Length > 0 && e.KeyCode.ToString() == KeySt[4])
                TrigPress[4] = TrigHold[4] || !TrigPress[4]; //09.09.2015
            if (Trig[5] == -6 && KeySt[5].Length > 0 && e.KeyCode.ToString() == KeySt[5])
                TrigPress[5] = TrigHold[5] || !TrigPress[5]; //09.09.2015

            //for (int i = 6; i < 12; i++)
            //{
            //    if (key_st[i].Length > 0 && e.KeyCode.ToString() == key_st[i].ToString())
            //        trig_press[i] = !trig_press[i];
            //}

            //MessageBox.Show(e.KeyCode.ToString() + " E: " + key_st[5].ToString());
            //lb_press.Text = e.KeyCode.ToString();
        }

        private void mouseKeyEventProvider1_KeyUp(object sender, KeyEventArgs e)
        {
            if (TrigHold[0] && Trig[0] == -1 && KeySt[0].Length > 0 && e.KeyCode.ToString() == KeySt[0])
                TrigPress[0] = false;
            if (TrigHold[1] && Trig[1] == -2 && KeySt[1].Length > 0 && e.KeyCode.ToString() == KeySt[1])
                TrigPress[1] = false;
            if (TrigHold[2] && Trig[2] == -3 && KeySt[2].Length > 0 && e.KeyCode.ToString() == KeySt[2])
                TrigPress[2] = false;
            if (TrigHold[3] && Trig[3] == -4 && KeySt[3].Length > 0 && e.KeyCode.ToString() == KeySt[3])
                TrigPress[3] = false;
            if (TrigHold[4] && Trig[4] == -5 && KeySt[4].Length > 0 && e.KeyCode.ToString() == KeySt[4])
                TrigPress[4] = false;
            if (TrigHold[5] && Trig[5] == -6 && KeySt[5].Length > 0 && e.KeyCode.ToString() == KeySt[5])
                TrigPress[5] = false;
        }

        private void mouseKeyEventProvider1_MouseDown(object sender, MouseEventArgs e)
        {
            if (_delayPressInterval > 0 &&
                _trigPressed.Sum() > 0 && (_keyDelay == "LMouse" && e.Button.ToString() == "Left"
                                           || _keyDelay == "RMouse" && e.Button.ToString() == "Right") &&
                _shiftPress == 0)
                _delayPress = 1;
        }

        private void ShowMe()
        {
            Show();
            WindowState = FormWindowState.Normal;
            notify_d3h.Visible = false;
            Activate();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int wmSyscommand = 0x0112;
            const int scMinimize = 0xF020;

            if (m.Msg == wmSyscommand)
                if (m.WParam.ToInt32() == scMinimize)
                {
                    if (Location.X > 0) Settings.Default.pos_x = Location.X; //14.05.2015
                    if (Location.Y > 0) Settings.Default.pos_y = Location.Y;
                    if (Location.X > 0 || Location.Y > 0) Settings.Default.Save();

                    if (pan_opt.Visible || _optClick == 1) b_opt_Click(null, null);
                    error_not(2); //17.04.2015
                    error_select();
                    if (!_startMain || !_startOpt) // this.WindowState = FormWindowState.Minimized;
                    {
                        m.Result = IntPtr.Zero;
                        error_show(2);
                        return;
                    }
                }

            if (m.Msg == NativeMethods.WmShowme)
                ShowMe();

            base.WndProc(ref m);

            if (m.Msg != 0x0312) return;
            /* Note that the three lines below are not needed if you only want to register one hotkey.
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, or if you want to know which key/modifier was pressed for some particular reason. */

            //var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF); // The key of the hotkey that was pressed.
            //var modifier = (KeyModifier) ((int) m.LParam & 0xFFFF); // The modifier of the hotkey that was pressed.
            var id = m.WParam.ToInt32(); // The id of the hotkey that was pressed.

            var j = 0;

            if (id == 0)
            {
                cb_start.Checked = !cb_start.Checked; //cb_start_CheckedChanged(null, null);
            }
            else if ((id == 1 || id == 2 || id == 3) && !_hotkeyPressed && cb_hot_prof.Enabled)
            {
                _hotkeyPressed = true;
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
                                cb_start.Checked = !cb_start.Checked;
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
                                cb_start.Checked = !cb_start.Checked;
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
                                cb_start.Checked = !cb_start.Checked;
                        }
                        break;
                }
            }
            // do something
            _hotkeyPressed = false;
        }

        private void d3hot_Load(object sender, EventArgs e)
        {
            FKeys[0] = Keys.F1.GetHashCode();
            for (var i = 1; i < 11; i++)
                FKeys[i] = FKeys[i - 1] + 1;

            cb_press_type.SelectedIndex = 0;

            //this.Size = new System.Drawing.Size(465, 344); //Сжатие окна, плохо работает с увеличенным шрифтом

            //MessageBox.Show("Height*0.9 : "+((int)(1440 * 0.9)).ToString());
            //MessageBox.Show(Convert.ToInt16((1440 * 0.1) - (1440 * (1 - 0.93))).ToString());
            //MessageBox.Show(((int)(768 * 0.1)).ToString()); //152

            //MessageBox.Show(Convert.ToInt16((768 * 0.1) - (768 * (1 - 0.9288))).ToString());
            //MessageBox.Show(Convert.ToInt16((1050 * 0.1) - (1050 * (1 - 0.93))).ToString());

            _debug = lb_debug.Visible;
            Icon = Resources.diablo_hot;
            notify_d3h.Icon = Resources.diablo_hot;

            //MessageBox.Show((rect.Width / 16 * 10).ToString() + " / " + (rect.Width / 16 * 9).ToString() + " /  " + rect.Height.ToString());

            //resolution = false;

            //int wid16_t = (int)Math.Round((decimal)1366 / 16 * 9, MidpointRounding.AwayFromZero);
            //MessageBox.Show(wid16_t.ToString());

            //int j = Convert.ToInt16(1527 * 0.1) - Convert.ToInt16(1527 * (1 - 0.9287)); //04.07.2015 обрезаем 90% картинки
            //int l = Convert.ToInt16(1527 * 0.1) - Convert.ToInt16(1527 * (1 - 0.9297)); //04.07.2015 обрезаем 90% картинки
            //l++; j++;
            //MessageBox.Show(((int)(1527 * 0.9)).ToString() + " " + Convert.ToInt16(1527 * 0.1).ToString() + " j: " + j.ToString() + " l: " + l.ToString());

            //rect.Width = 1366; //for test
            //rect.Height = 768; //for test

            var wid = _rect.Width;
            var hei = _rect.Height;

            var wid169 = (int) Math.Round((decimal) wid / 16 * 9, MidpointRounding.AwayFromZero);
            var wid1610 = (int) Math.Round((decimal) wid / 16 * 10, MidpointRounding.AwayFromZero);
            var wid54 = (int) Math.Round((decimal) wid / 5 * 4, MidpointRounding.AwayFromZero);
            //var wid43 = (int) Math.Round((decimal) wid / 4 * 3, MidpointRounding.AwayFromZero); 4:3 не работает нормально

            if (wid169 == hei || wid169 == hei + 1 || wid169 == hei - 1)
                _aspectRatio = 169;
            else if (wid1610 == hei || wid1610 == hei + 1 || wid1610 == hei - 1)
                _aspectRatio = 1610;
            else if (wid54 == hei || wid54 == hei + 1 || wid54 == hei - 1)
                _aspectRatio = 54;
            //else if (wid4_3 == hei || wid4_3 == hei + 1 || wid4_3 == hei - 1) //Кривые параметры 4:3
            //    aspect_ratio = 43;

            //if (wid16_10 != hei && wid16_9 != hei //15.07.2015
            //    && wid16_10 != hei + 1 && wid16_9 != hei + 1
            //    && wid16_10 != hei - 1 && wid16_9 != hei - 1
            //    //&& !(rect.Width == 1366 && rect.Height == 768)
            //    ) //07.09.2015

            if (_aspectRatio == 0)
                _resolution = false;

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
                Settings.Default.lb_lang = CultureInfo.CurrentCulture.EnglishName.Contains("Russia") ? "Eng" : "Rus";
                Settings.Default.Save();
            }
            //Загрузка языка программы из настроек
            lb_lang.Text = Settings.Default.lb_lang;
            if (lb_lang.Text == @"Eng") _lng.Lang_rus();
            else _lng.Lang_eng();
            Lang();

            //Установка подписи в секундах, если при загрузке настроечные поля заполнены.
            if (nud_tmr1.Value > 0 && cb_tmr1.SelectedIndex < 1)
                lb_tmr1_sec.Text = Math.Round(nud_tmr1.Value / 1000, 2) + @" " + _lng.LangSec;
            if (nud_tmr2.Value > 0 && cb_tmr2.SelectedIndex < 1)
                lb_tmr2_sec.Text = Math.Round(nud_tmr2.Value / 1000, 2) + @" " + _lng.LangSec;
            if (nud_tmr3.Value > 0 && cb_tmr3.SelectedIndex < 1)
                lb_tmr3_sec.Text = Math.Round(nud_tmr3.Value / 1000, 2) + @" " + _lng.LangSec;
            if (nud_tmr4.Value > 0 && cb_tmr4.SelectedIndex < 1)
                lb_tmr4_sec.Text = Math.Round(nud_tmr4.Value / 1000, 2) + @" " + _lng.LangSec;
            if (nud_tmr5.Value > 0 && cb_tmr5.SelectedIndex < 1)
                lb_tmr5_sec.Text = Math.Round(nud_tmr5.Value / 1000, 2) + @" " + _lng.LangSec;
            if (nud_tmr6.Value > 0 && cb_tmr6.SelectedIndex < 1)
                lb_tmr6_sec.Text = Math.Round(nud_tmr6.Value / 1000, 2) + @" " + _lng.LangSec;
            if (nud_key_delay_ms.Value > 0)
                lb_key_delay_desc.Text = Math.Round(nud_key_delay_ms.Value / 1000, 2) + @" " + _lng.LangSec;
            if (nud_rand.Value > 0) lb_nud_rand.Text = Math.Round(nud_rand.Value / 1000, 2) + @" " + _lng.LangSec;
            if (nud_coold.Value > 0) lb_nud_coold.Text = Math.Round(nud_coold.Value / 1000, 2) + @" " + _lng.LangSec;

            if (nud_trig_time.Value > 0)
                lb_trig_time_ms.Text = Math.Round(nud_trig_time.Value / 1000, 2) + @" " + _lng.LangSec; //10.12.2015
            if (nud_trig_delay.Value > 0)
                lb_trig_delay_ms.Text = Math.Round(nud_trig_delay.Value / 1000, 2) + @" " + _lng.LangSec; //10.12.2015

            //Установка глобального хоткея запуска/остановки
            Reghotkey();

            Person(chb_users.Checked ? 1 : 0);

            //22.04.2015
            form_create();

            key_menu();
            chb_hold_CheckedChanged(null, null);

            if (chb_ver_check.Checked) lb_ver_check_Click(null, null); //Проверка новой версии 27.04.2015
        }

        private void key_menu()
        {
            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("key") || cb.Name.Contains("trig")) //08.09.2015
                    cb.Items[0] = _lng.CbKeysChoose;
            cb_tp.Items[0] = _lng.CbKeysChoose;
            cb_map.Items[0] = _lng.CbKeysChoose;
            cb_key_delay.Items[0] = _lng.CbKeysChoose;
        }

        /// <summary>
        ///     Метод установки заголовка окна, иконки, фона программы
        /// </summary>
        private void Person(int i)
        {
            if (i == 0)
            {
                Icon = Resources.diablo_hot;
                notify_d3h.Icon = Resources.diablo_hot;
                BackgroundImage = null;

                _version = @"Diablo 3 Hotkeys ver. " + string.Format("{0:F1}", Ver).Replace(_sep, ".");
                Text = _version;
            }
            else
            {
                string progname = "", iconame = "", jpgname = "", txtname = "";
                //progname = Path.GetFullPath(Application.ExecutablePath); //GetFileNameWithoutExtension
                try
                {
                    progname = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 3);
                }
                catch
                {
                    // ignored
                }

                if (progname.Length > 0 && File.Exists(progname + "ico"))
                {
                    iconame = progname + "ico";
                }
                else
                {
                    var icofiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ico",
                        SearchOption.TopDirectoryOnly);
                    if (icofiles.Length > 0 && File.Exists(icofiles[0])) iconame = icofiles[0];
                }

                if (progname.Length > 0 && File.Exists(progname + "jpg"))
                {
                    jpgname = progname + "jpg";
                }
                else
                {
                    var jpgfiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg",
                        SearchOption.TopDirectoryOnly);
                    if (jpgfiles.Length > 0 && File.Exists(jpgfiles[0])) jpgname = jpgfiles[0];
                }

                if (progname.Length > 0 && File.Exists(progname + "txt"))
                {
                    txtname = progname + "txt";
                }
                else
                {
                    var txtfiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt",
                        SearchOption.TopDirectoryOnly);
                    if (txtfiles.Length > 0 && File.Exists(txtfiles[0])) txtname = txtfiles[0];
                }


                if (iconame.Length > 0)
                {
                    Icon = new Icon(iconame);
                    notify_d3h.Icon = new Icon(iconame);
                }

                if (jpgname.Length > 0)
                    BackgroundImage = new Bitmap(jpgname);


                if (txtname.Length <= 0) return;
                var readText = File.ReadAllLines(txtname, Encoding.Default);
                if (readText.Length > 0 && readText[0].Length > 0) Text = readText[0];
                if (Text.Length > 30) Text = readText[0].Substring(0, 30);
            }
        }

        //private void tmr_save_Elapsed(object sender, EventArgs e)
        //{
        //    Settings.Default.Save();
        //    tmr_save.Stop();
        //}

        private static string GetActiveWindowTitle(IntPtr handleInput)
        {
            const int nChars = 256;
            var buff = new StringBuilder(nChars);

            var handleLocal = handleInput;
            if (handleInput == IntPtr.Zero) handleLocal = NativeMethods.GetForegroundWindow();

            return NativeMethods.GetWindowText(handleLocal, buff, nChars) > 0 ? buff.ToString() : null;
        }

        /// <summary>
        ///     Метод для проверки зажатой/переключённой клавиши.
        /// </summary>
        /// <param name="i">1: Shift, 2: Scroll, 3: Caps, 4: Num</param>
        /// <returns></returns>
        private static bool key_press(int i)
        {
            var result = false;
            switch (i)
            {
                case 1:
                    result = ModifierKeys == Keys.Shift;
                    break;
                case 2:
                    result = IsKeyLocked(Keys.Scroll);
                    break;
                case 3:
                    result = IsKeyLocked(Keys.CapsLock);
                    break;
                case 4:
                    result = IsKeyLocked(Keys.NumLock);
                    break;

                case -1:
                    result = TrigPress[0];
                    break;
                case -2:
                    result = TrigPress[1];
                    break;
                case -3:
                    result = TrigPress[2];
                    break;
                case -4:
                    result = TrigPress[3];
                    break;
                case -5:
                    result = TrigPress[4];
                    break;
                case -6:
                    result = TrigPress[5];
                    break;
            }
            return result;
        }

        //private void timer_cdr_create(int i)
        //{
        //    //if (tmr_cdr_n[i] == 0) //- 1
        //    //{
        //    //MessageBox.Show("123"+i.ToString());
        //    //tmr_cdr = new System.Timers.Timer();
        //    _tmrCdrCurr = i + 1;
        //    tmr_Elapsed(_tmrCdr, null);
        //    _tmrCdrCurr = 0; //11.09.2015

        //    //tmr_cdr.Dispose();
        //    //}
        //    //tmr_cdr_n[i]++;// 07.07.2015
        //    //if (tmr_cdr_n[i] > 1) tmr_cdr_n[i] = 0; // 07.07.2015
        //}

        /// <summary>
        ///     Проверка триггеров на необходимость прожатия
        /// </summary>
        /// <param name="i"></param>
        private void trig_check(int i)
        {
            if (!_progStart || !_progRun) return; //Если выключаемся, выходим

            if (CdrKey[i] == 1) //Триггер с кулдауном
            {
                if (_tmrCdr == null)
                {
                    _tmrCdr = new Timer(); //30мс - общий таймер 23.01.2017

                    try
                    {
                        _tmrCdr.Interval = _tmrCdrInt;
                        _tmrCdr.Elapsed += Cooldown_Tick;
                    }
                    catch
                    {
                        _tmrCdr.Dispose();
                        return;
                    }
                }
                if (!_tmrCdr.Enabled)
                    _tmrCdr.Start();

                timer_load(i); //Создаём таймер (Внутри проверка, если его ещё нет).
                if (_tmr[i] != null && !_tmr[i].Enabled) _tmr[i].Enabled = true; //19.01.2017
            }

            else if (HoldKey[i] == 1 && Hold[i] == 0) //18.03.2015 //cdr_key[i] == 0 && 
            {
                Hold[i] = 1;
                hold_load(i);
            }

            else if (TmrR[i] == 0 && HoldKey[i] == 0) //cdr_key[i] == 0 && 
            {
                TmrR[i] = 1;
                timer_load(i);

                if (TmrLeft[i] != 0 && TmrLeft[i] - _delayWait > 0)
                    _tmr[i].Interval = TmrLeft[i] - _delayWait;
                else if (_tmr[i] != null)
                    tmr_Elapsed(_tmr[i], null); //Чтобы отрабатало при запуске один раз


                if (_tmr[i] == null || TmrOnce[i] >= 1) return; //Пока не используется

                if (!_tmr[i].Enabled)
                    _tmr[i].Enabled = true;

                if (_tmrWatch[i] != null && !_tmrWatch[i].IsRunning)
                    _tmrWatch[i].Start();
            }
        }

        /// <summary>
        ///     Основной метод после запуска процесса - срабатывание главного таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmr_all_Elapsed(object sender, EventArgs e)
        {
            //if (_tmrAllCounter == 3) return; //пропускаем круг, если проверяются триггеры 19.01.2017

            if (!_progStart || !_progRun) //|| _tmrAllCounter > 0
                return;

            //_tmrAllCounter = 10;

            if (_debug)
            {
                LogMaker(ref _log, 1, "Первый круг общего цикла!");

                var timersStatus = "";
                if (_tmrAll != null) timersStatus += "_tmrAll: " + _tmrAll.Enabled;
                if (_tmrCdr != null) timersStatus += ", _tmrCdr: " + _tmrCdr.Enabled;
                if (_tmr[0] != null) timersStatus += ", _tmr[0]: " + _tmr[0].Enabled;
                if (_tmr[1] != null) timersStatus += ", _tmr[1]: " + _tmr[1].Enabled;
                if (_tmr[2] != null) timersStatus += ", _tmr[2]: " + _tmr[2].Enabled;
                if (_tmr[3] != null) timersStatus += ", _tmr[3]: " + _tmr[3].Enabled;
                if (_tmr[4] != null) timersStatus += ", _tmr[4]: " + _tmr[4].Enabled;
                if (_tmr[5] != null) timersStatus += ", _tmr[5]: " + _tmr[5].Enabled;
                if (TmrPress.Sum() > 0)
                    timersStatus += " Кнопок прожимается: " + TmrPress.Sum(); //TmrPress.Count(i => i > 0);

                LogMaker(ref _log, 99999, timersStatus);
            }

            //Проверяем, есть ли процесс в памяти
            if (_d3Proc && _procId > 0)
                try
                {
                    Process.GetProcessById(_procId);
                }
                catch
                {
                    //MessageBox.Show(@"Вы вышли из программы!");
                    //if (!_progStart) return;
                    TimerStop(99); //Останавливаем все таймеры, включая главный
                    BeginInvoke((Action) (() => cb_proc.SelectedIndex = -1)); //Сбрасываем выбранный процесс
                    BeginInvoke((Action) (() => cb_start.Checked = false));
                    //_tmrAllCounter = 0;
                    //if (_startStop != 0 && _progStart)
                    //{
                    //    _inp.Keyboard.KeyPress((VirtualKeyCode)_startStop);
                    //}
                    //else if (_progStart)
                    //{
                    //     BeginInvoke((Action)(() => cb_start.Checked = false));
                    //}

                    return;
                }

            if (_returnDelay > 0 && _returnPress == 1) //(pause == 2 || pause == 3)
            {
                _returnPressCount++;
                _returnPress = 0;
                if (_returnPressCount == 1)
                {
                    _returnWatch = new Stopwatch();
                    _returnWatch.Start();
                }
            }

            if (_returnWatch != null &&
                (_returnPressCount > 1 ||
                 _returnDelay > 0 && (int) _returnWatch.ElapsedMilliseconds > _returnDelay * 1000))
                //Обработка количества Enter для работы с Shift
            {
                //return_press_count = 0; //01.06.2016
                _returnPress = 0;
                _delayWait = (int) _returnWatch.ElapsedMilliseconds;
                _returnWatch.Stop();
            }

            //if (seconds_count > 0) seconds_count++;

            _trigPressed = some_trig_press(); //проверяем, какие триггеры активны и нажаты
            _trigPressedSum = _trigPressed.Sum();

            for (var i = 0; i < 6; i++)
                if (_trigPressed[i] == 1 && CdrKey[i] == 1 && CdrRun[i] < 1) //trig_cdr_pressed[i] == 0
                    CdrRun[i] = 1;
            //else if (_trigPressed[i] == 0)
            //    CdrRun[i] = 0;

            //if (CdrRun[3] == 1) //Для записи логов
            //    LogMaker(ref _log, 5,
            //        "Триггеры нажаты: " + _trigPressed[0] + _trigPressed[1] + _trigPressed[2] + _trigPressed[3] +
            //        _trigPressed[4] + _trigPressed[5]);

            //Работаем только если хоть что-то из триггеров зажато/переключено.
            if (_trigPressedSum > 0)
            {
                _wasUsed = true;
                if (_returnPressCount > 1 ||
                    _returnDelay > 0 && _returnWatch != null &&
                    (int) _returnWatch.ElapsedMilliseconds > _returnDelay * 1000)
                    //Обработка количества Enter для работы с Shift
                    _returnPressCount = 0; //01.06.2016

                if (_returnWatch != null && _returnWatch.IsRunning && _rPress == 0)
                {
                    _rPress = 1;

                    MainTimerStop(88); //Удаляем данные по обычным таймерам, а также кулдауна и зажатия
                }

                //if ((r_press == 1 && return_press == 1) || (delay_watch != null && (int)delay_watch.ElapsedMilliseconds > 30000)) 
                else if (_rPress == 1 && (_returnWatch == null || !_returnWatch.IsRunning))
                {
                    _rPress = 0;
                    _tPress = 0;
                    _mapPress = 0;
                    //r_press = 0; return_press = 0; t_press = 0; map_press = 0; return_press_count = 0;
                    //delay_wait = (int)delay_watch.ElapsedMilliseconds;
                    //delay_watch.Stop();
                }

                //Проверка на нажатие T.
                if (_teleWatch != null && _teleWatch.ElapsedMilliseconds >= _tpDelay * 1000)
                    //tp_delay > 0 && t_press > 0 && tmr_all.Interval == tp_delay * 1000
                {
                    _delayWait = (int) _teleWatch.ElapsedMilliseconds;
                    _tPress = 0;
                    //tmr_all.Interval = 1; //01.06.2015
                    StopWatch(_teleWatch); //delay_timers(33);
                    _returnPress = 0;
                }

                if (!(_teleWatch != null && _teleWatch.IsRunning) && _tpDelay > 0 && _tPress > 0 && _rPress == 0)
                    //(pause == 1 || pause == 3)
                {
                    MainTimerStop(88); //Удаляем данные по обычным таймерам, а также кулдауна и зажатия
                    RestartWatch(out _teleWatch);
                }

                if (_tpDelay > 0 && _rPress > 0 && _tPress > 0) _tPress = 0; //(pause == 1 || pause == 3)

                //Проверка на нажатие M.
                if (_mapWatch != null && _mapWatch.ElapsedMilliseconds >= _mapDelay * 1000)
                    //map_delay > 0 && map_press > 0 && tmr_all.Interval == map_delay * 1000
                {
                    _delayWait = (int) _mapWatch.ElapsedMilliseconds;
                    _mapPress = 0;
                    //tmr_all.Interval = 1; //01.06.2015
                    StopWatch(_mapWatch); //delay_timers(32);
                    _returnPress = 0;
                }

                if (!(_mapWatch != null && _mapWatch.IsRunning) && _mapDelay > 0 && _mapPress > 0 && _rPress == 0)
                {
                    MainTimerStop(88); //Удаляем данные по обычным таймерам, а также кулдауна и зажатия
                    RestartWatch(out _mapWatch);
                }

                if (_mapDelay > 0 && _rPress > 0 && _mapPress > 0) _mapPress = 0;

                //13.01.2017 Задержка по клавише. Уменьшаем количество нажатых триггеров, если их отжали во время работы таймера
                if (_keyWatch != null && _keyWatch.IsRunning && _trigPressedSumDelay > _trigPressedSum)
                    _trigPressedSumDelay = _trigPressedSum;
                //Если отпустили триггер, актуализируем зафиксированное количество

                //16.03.2015 Проверка на клавишу задержки - отмена или окончание паузы
                if (_keyWatch != null && _keyWatch.IsRunning &&
                    (_trigPressedSum > _trigPressedSumDelay || _keyWatch.ElapsedMilliseconds >= _delayPressInterval))
                    //delay_press > 0 && tmr_all.Interval == delay_press_interval
                {
                    if (_delayWait == 0) _delayWait = (int) _keyWatch.ElapsedMilliseconds;
                    _delayPress = 0;
                    StopWatch(_keyWatch); //delay_timers(31);
                    //tmr_all.Interval = 1;
                }

                //16.03.2015 Проверка на клавишу задержки - запуск задержки
                if (_delayPress > 0 && _delayPressInterval > 0 && _rPress == 0 && _mapPress == 0 && _tPress == 0)
                    //&& !key_press(1)
                {
                    if (_keyWatch == null || !_keyWatch.IsRunning)
                        _delayWait = 0; //11.01.2017 Анализ PVS Studio - упрощение конструкции
                    else _delayWait += (int) _keyWatch.ElapsedMilliseconds; //(key_watch != null && key_watch.IsRunning)

                    MainTimerStop(88); //Удаляем данные по обычным таймерам, а также кулдауна и зажатия

                    _delayPress = 0;
                    _trigPressedSumDelay = _trigPressedSum; //Сохранили количество нажатых триггеров 12.01.2017
                    RestartWatch(out _keyWatch); //delay_timers(21);
                }

                if (_returnDelay > 0 && _rPress > 0 && _delayPress > 0) _delayPress = 0; //(pause == 2 || pause == 3)

                if (_teleportInProgress) //Проверка на телепорт при включённом кулдауне
                {
                    timer_unload(88);
                    hold_clear(88);
                }

                //Если T|Enter не нажаты, запускаем таймеры триггеров 1-2-3-4-5-6 при активном состоянии и останавливаем при отключенном.
                else if (_mapPress == 0 && _tPress == 0 && _rPress == 0 && _delayPress == 0 &&
                         (_keyWatch == null || !_keyWatch.IsRunning)) //!teleport_in_progress && 
                {
                    //_tmrAllCounter = 3;

                    for (var i = 0; i < 6; i++) //31.01.2016
                        if (_trigPressed[i] == 1)
                            //Триггер нажат и не отрабатывает сейчас 25.01.2017 //&& TrigUsed[i] != 1
                        {
                            TrigUsed[i] = 1;
                            //_tmrAllCounter++;
                            //LogMaker(ref _log, 99999, "Сколько раз сюда зашли: " + _tmrAllCounter);
                            trig_check(i);
                        }
                        else if (_trigPressed[i] == 0 && TrigUsed[i] == 1) //Триггер не нажат, но был нажат когда-то
                        {
                            //if (_debug)
                            //{
                            //    var timenow = DateTime.Now.ToLongTimeString();
                            //    MessageBox.Show(timenow + @" Как будто кто-то отжал триггер " + i);
                            //}

                            MainTimerStop(i);
                            TmrLeft[i] = 0;
                            TrigUsed[i] = 0;
                        }

                    _delayWait = 0;
                    //_tmrAllCounter = 2; //19.01.2017
                }
            }
            else if (_wasUsed) //Ни один триггер не нажат //08.12.2015
            {
                TimerStop(88); //Останавливаем все таймеры, кроме главного
            }
            //_tmrAllCounter = 0;
        }

        private void hold_clear(int i)
        {
            if (i < 6 && HoldKey[i] == 0)
                return;

            _holded = false;

            Array.Clear(_stTimerR, 0, 6); //Очищаем список зажатых кнопок
            Array.Clear(_rTimerR, 0, 6); //Очищаем список зажатых кнопок

            if (i < 6)
            {
                if (Hold[i] <= 0) return;
                Keyup(i); //20.03.2015
                hold_unload(i);
                Hold[i] = 0;
            }
            else
            {
                for (var j = 0; j < 6; j++)
                    if (Hold[j] > 0)
                    {
                        hold_unload(j);
                        Keyup(j);
                        Hold[j] = 0;
                    }
            }
        }

        //public enum WMessages : int //Убрал 19.03.2015
        //{
        //    WM_LBUTTONDOWN = 0x201,
        //    WM_LBUTTONUP = 0x202
        //}

        /// <summary>
        ///     Метод для проверки области использования.
        /// </summary>
        /// <returns></returns>
        private static bool usage_area()
        {
            var procRight = false;
            var result = false;

            //Проверка окна на наличе слова "Diablo", если область действия соответствующая.
            var title = "";
            if (GetActiveWindowTitle(IntPtr.Zero) != null) title = GetActiveWindowTitle(IntPtr.Zero); //06.04.2015

            //11.03.2015
            if (_d3Proc)
            {
                int pid;
                NativeMethods.GetWindowThreadProcessId(NativeMethods.GetForegroundWindow(), out pid);
                //pid = 111;
                //var proc = Process.GetProcessById(pid); //17.01.2017 не нужно
                //MessageBox.Show(proc.ProcessName);
                if (pid != 0 && _procCurr.Contains(pid.ToString())) procRight = true;
            }

            if (
                title != null && !title.ToLower().Contains("hotkeys")
                &&
                (!_d3Prog || title.ToLower().Contains(_diabloName))
                //11.01.2017 Анализ PVS Studio - упрощение конструкции
                &&
                (!_d3Proc || procRight)
                //|| debug
            )
                result = true;


            return result;
        }

        /// <summary>
        ///     Create the lParam for PostMessage
        /// </summary>
        /// <param name="a">HiWord</param>
        /// <param name="b">LoWord</param>
        /// <returns>Returns the long value</returns>
        private static uint MakeLong(int a, uint b)
        {
            //MessageBox.Show("Var a: " + a.ToString() + ", Var b: " + b.ToString() + ", Result: " + ((uint)((ushort)(a)) | ((uint)((ushort)(b) << 16))).ToString());
            return (ushort) a | (uint) ((ushort) b << 16);
        }

        //public static IntPtr MakeLParam(Point ptr) //11.09.2015
        //{
        //    return (IntPtr)((ptr.Y << 16) | (ptr.X & 0xffff));
        //}

        private static UIntPtr MakeLParam(int loWord, int hiWord) //11.09.2015
        {
            return (UIntPtr) ((hiWord << 16) | (loWord & 0xffff));
        }

        private void tmr_Elapsed(object sender, EventArgs e)
        {
            var tmrLocal = (NumericTimer) sender;
            var num = tmrLocal.Number;

            if (TmrPress[num] == 1 || _trigPressed[num] != 1 || !_progStart || !_progRun)
                return; //25.01.2017 Не заходим в таймер, пока он работает

            //if (_keyPressed) //13.11.2015
            //    Thread.Sleep(TimeSpan.FromMilliseconds(10)); //09.12.2015
            //_keyPressed = true;

            try
            {
                TmrPress[num] = 1; //Кнопка прожимается, пока не пытаться жать

                VirtualKeyCode key; //VirtualKeyCode.VK_0; //VirtualKeyCode.ESCAPE;
                var keyForHold = timer_key(_tmr[num], out key); //tmrLocal

                //if (key == VirtualKeyCode.VK_Z) 
                //    MessageBox.Show("!@#");

                //if (nud_rand.Value > 0) _rand = new Random();

                //uint scanCode = MapVirtualKey(key_for_hold, 0);
                //MessageBox.Show("(byte)key_for_hold: " + ((byte)key_for_hold).ToString() + " scanCode: " + scanCode.ToString() + " (byte)scanCode: " + ((byte)scanCode).ToString());

                rand_interval(num);
                RestartWatch(out _tmrWatch[num]);
                for (var i = 0; i < _multikeys; i++)
                    button_press(keyForHold, key);

                //for (var i = 0; i < 6; i++)
                //    if (tmrLocal == _tmr[i] && _trigPressed[i] == 1)
                //        //key_press(trig[i]) - используем массив нажатых триггеров 17.06.2016
                //    {
                //        //key = key_v[i];// virt_code(key1); //08.12.2015
                //        if (CdrKey[i] < 1) rand_interval(i); //&& hold_key[i] < 1
                //        //tmr_watch[i].Reset();
                //        //tmr_watch[i].Start();
                //        RestartWatch(out _tmrWatch[i]); //13.07.2015
                //        //if (i==2) MessageBox.Show("2: " + tmr_watch[2].IsRunning.ToString());
                //        break;
                //    }

                //switch (_pressType)
                //{
                //    case 0:
                //        //Если всё в порядке, нажимаем соответствующую клавишу.
                //        if (usage_area()) //(int)handle == 0 && 
                //            //if (tmrLocal == _tmrCdr) Thread.Sleep((int) _cooldDelay / 25); //Кулдаун тут больше не живёт
                //            sendinput_push(key, keyForHold);

                //        else if ((int) _handle > 0)
                //            post_push(keyForHold);

                //        break;
                //    case 1:
                //        post_push(keyForHold);
                //        break;
                //    default:
                //        if (_pressType > 1 && usage_area())
                //            //if (tmrLocal == _tmrCdr) Thread.Sleep((int)_cooldDelay / 15); //Кулдаун тут больше не живёт
                //            sendinput_push(key, keyForHold);
                //        break;
                //}
                //_keyPressed = false;
            }
            catch
            {
                // ignored
            }
            finally
            {
                TmrPress[num] = 0; //Кнопка прожалась, можно прожимать снова
            }
        }

        /// <summary>
        ///     Метод для нажатия клавиш
        /// </summary>
        /// <param name="keyForHold"></param>
        /// <param name="key"></param>
        private void button_press(uint keyForHold, VirtualKeyCode key) //20.06.2016
        {
            switch (_pressType)
            {
                case 0:
                    if (usage_area())
                        sendinput_push(key, keyForHold); //InputSimulator - Press_type 0
                    else if ((int) _handle > 0)
                        post_push(keyForHold); //PostMessage если окно неактивно
                    break;
                case 1:
                    post_push(keyForHold);
                    break;
                default:
                    if (_pressType > 1 && usage_area())
                        sendinput_push(key, keyForHold); //keybd_event - Press_type 2, InputSimulator - Press_type 3
                    break;
            }
        }

        /// <summary>
        ///     Метод нажатия Shift+Click
        /// </summary>
        /// <param name="i"></param>
        private void shift_click(int i)
        {
            _shiftPress = 1;
            _inp.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
            _inp.Keyboard.Sleep(1);
            if (i == 1) _inp.Mouse.LeftButtonClick();
            else _inp.Mouse.RightButtonClick();
            _inp.Keyboard.Sleep(1);
            _inp.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
            _shiftPress = 0;
        }

        /// <summary>
        ///     Метод нахождения кодов клавиш из списка
        /// </summary>
        /// <param name="i"></param>
        private void key_codes(int i)
        {
            var keyFromString = Keys.F12;
            VirtualKeyCode vkc = 0;
            uint keyHold = 0;
            //char item = new char();

            i--; //11.01.2017 Анализ PVS Studio - i может достигать 6

            ComboBox[] cbKey = {cb_key1, cb_key2, cb_key3, cb_key4, cb_key5, cb_key6};

            if (i > 5)
            {
                ComboBox[] cbTrig =
                {
                    cb_trig_tmr1, cb_trig_tmr2, cb_trig_tmr3, cb_trig_tmr4, cb_trig_tmr5, cb_trig_tmr6
                };
                var stTrig = (string) cbTrig[i - 6].Items[cbTrig[i - 6].SelectedIndex];
                if (stTrig.Length > 0 && stTrig.Substring(0, 1) == "*") stTrig = stTrig.Remove(0, 1);
                KeySt[i - 6] = stTrig;
            }
            else
            {
                var st = (string) cbKey[i].Items[cbKey[i].SelectedIndex];
                bool done = false, error = false;

                switch (st)
                {
                    case "Space":
                        done = true;
                        vkc = VirtualKeyCode.SPACE;
                        keyHold = (int) Keys.Space;
                        break;
                    case "LMouse":
                        vkc = VirtualKeyCode.LBUTTON;
                        keyHold = (int) Keys.LButton;
                        done = true;
                        break;
                    case "RMouse":
                        vkc = VirtualKeyCode.RBUTTON;
                        keyHold = (int) Keys.RButton;
                        done = true;
                        break;
                    case "Shift+LM":
                        vkc = VirtualKeyCode.XBUTTON1;
                        keyHold = (int) Keys.XButton1; //не используется
                        done = true;
                        break;
                    case "Shift+RM":
                        vkc = VirtualKeyCode.XBUTTON2;
                        keyHold = (int) Keys.XButton2; //не используется
                        done = true;
                        break;
                    default:
                        try
                        {
                            if (st.Length > 0 && st.Substring(0, 1) == "*") st = st.Remove(0, 1);
                            var convertFromString = new KeysConverter().ConvertFromString(st);
                            if (convertFromString != null)
                                keyFromString = (Keys) convertFromString;
                        }
                        catch
                        {
                            error = true;
                        }
                        break;
                }

                if (error)
                {
                    cbKey[i].SelectedIndex = 0; //08.09.2015
                    cb_start.Checked = false;
                    if (WindowState == FormWindowState.Minimized) ShowMe();
                }
                else
                {
                    //bool res = false;
                    //if (st.Length > 0) res = Char.TryParse(st, out item);
                    //if (res)
                    //{
                    if (!done)
                    {
                        vkc = (VirtualKeyCode) keyFromString;
                        keyHold = (uint) keyFromString;
                    }

                    KeyV[i] = vkc; //08.09.2015
                    KeyH[i] = keyHold;
                }
            }
        }

        /// <summary>
        ///     Метод при запуске программы или её остановке (Start/Stop/F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_start_CheckedChanged(object sender, EventArgs e)
        {
            _cooldDelay = Convert.ToDouble(nud_coold.Value);

            if (chb_log.Checked) _logEnabled = true;
            _pressType = cb_press_type.SelectedIndex;
            lb_lang_name.Focus();

            TimerStop(99); //Остановили все таймеры (включая главный) и почистили почти все массивы

            if (cb_start.Text != @"Stop")
            {
                if (pan_opt.Visible || _optClick == 1) b_opt_Click(null, null);

                var procExist = false;

                if (_debug && _handle == IntPtr.Zero && _d3Proc) //For Debugging //27.04.2015
                {
                    procExist = true;
                    _diabloName = "opera";
                    _handle = NativeMethods.FindWindow(null, "akelpad");
                    _handle = NativeMethods.FindWindowEx(_handle, IntPtr.Zero, "AkelEditW", null);
                    _handleRef = new HandleRef(this, _handle);
                }

                if (!_debug && chb_hold.Checked && !_procSelected && _handle != IntPtr.Zero)
                {
                    var processlist = Process.GetProcesses();
                    try
                    {
                        foreach (var p in processlist)
                            if (p.PagedMemorySize64 > _procSize)
                                if (_handle == p.MainWindowHandle) procExist = true;
                    }
                    catch
                    {
                        // ignored
                    }
                    if (!procExist)
                        cb_proc.SelectedIndex = -1;
                }

                if (chb_hold.Checked == false &&
                    (cb_key1.SelectedItem.ToString().Contains("Shift") ||
                     cb_key2.SelectedItem.ToString().Contains("Shift") ||
                     cb_key3.SelectedItem.ToString().Contains("Shift") ||
                     cb_key4.SelectedItem.ToString().Contains("Shift") ||
                     cb_key5.SelectedItem.ToString().Contains("Shift") ||
                     cb_key6.SelectedItem.ToString().Contains("Shift")))
                    foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                        if (cb.Name.Contains("trig") && cb.SelectedIndex == 1) cb.SelectedIndex = 0;

                error_not(2); //check_only(); 17.04.2015
                error_select();

                if (!_startMain || !_startOpt || !cb_start.Enabled)
                {
                    cb_start.Checked = false;
                    if (WindowState == FormWindowState.Minimized) ShowMe();
                }
            }

            if (cb_start.Checked) //Стартанули
            {
                _progStart = true; //отметили, что стартанули
                if (_logEnabled) _log = new List<string>(); //23.04.2016

                int[] settCbTrig =
                {
                    Settings.Default.chb_trig_once_0, Settings.Default.chb_trig_once_1, Settings.Default.chb_trig_once_2,
                    Settings.Default.chb_trig_once_3, Settings.Default.chb_trig_once_4, Settings.Default.chb_trig_once_5
                };
                if (settCbTrig.Sum() > 0)
                    for (var i = 0; i < 6; i++)
                        TmrOnce[i] = settCbTrig[i];

                //tmr_all_count = 0;
                _tmr = new Timer[6]; //09.07.2015
                _tmrWatch = new Stopwatch[6]; //09.07.2015
                //test_sw.Start(); //07.07.2015
                Array.Clear(KeyH, 0, 6);
                //key_h[0] = 0; key_h[1] = 0; key_h[2] = 0; key_h[3] = 0; key_h[4] = 0; key_h[5] = 0; //24.04.2015
                Array.Clear(KeySt, 0, 6); //08.09.2015
                Array.Clear(KeyV, 0, 6); //08.09.2015
                //key_v[0] = 0; key_v[1] = 0; key_v[2] = 0; key_v[3] = 0; key_v[4] = 0; key_v[5] = 0; //24.04.2015
                d3hot_Activated(null, null); //27.04.2015

                //if (d3proc) proc_watch = new Stopwatch(); //06.04.2015
                if (NativeMethods.GetForegroundWindow() != _handle) d3hot_Deactivate(null, null);

                mouseKeyEventProvider1.Enabled = true;

                _multikeys = Settings.Default.chb_mpress == 0 ? 1 : 3;
                //_hold = new[] {0, 0, 0, 0, 0, 0}; //17.03.2015
                //Array.Clear(Hold, 0, 6);

                if (nud_key_delay_ms.Value > 0) _delayPressInterval = Convert.ToInt32(nud_key_delay_ms.Value);

                //pause = cb_pause.SelectedIndex; //Tp, Enter, All

                cb_start.Text = @"Stop";
                tt_key.SetToolTip(cb_start, _lng.TtStop);

                if ((nud_tmr1.Value > 0 || CdrKey[0] == 1 || HoldKey[0] == 1) && cb_trig_tmr1.SelectedIndex > 0 &&
                    cb_key1.SelectedIndex > 0) //02.09.2015 chb_key1.Checked
                {
                    if (lb_tmr1_sec.Text != _lng.CbTmrCdr && lb_tmr1_sec.Text != _lng.CbTmrHold)
                        lb_tmr1_sec.Text = Math.Round(nud_tmr1.Value / 1000, 2) + @" " + _lng.LangSec;
                    if (nud_tmr1.Enabled) _tmrI[0] = Convert.ToDouble(nud_tmr1.Value); //16.11.2015
                    //trig[0] = cb_trig_tmr1.SelectedIndex;
                    if (cb_trig_tmr1.SelectedIndex > 4)
                    {
                        key_codes(7);
                        Trig[0] = -1;
                        key_codes(1);
                    }
                    else
                    {
                        key_codes(1);
                        Trig[0] = cb_trig_tmr1.SelectedIndex;
                    }
                    TmrF[0] = 1;
                }
                if ((nud_tmr2.Value > 0 || CdrKey[1] == 1 || HoldKey[1] == 1) && cb_trig_tmr2.SelectedIndex > 0 &&
                    cb_key2.SelectedIndex > 0) //02.09.2015 chb_key2.Checked
                {
                    if (lb_tmr2_sec.Text != _lng.CbTmrCdr && lb_tmr2_sec.Text != _lng.CbTmrHold)
                        lb_tmr2_sec.Text = Math.Round(nud_tmr2.Value / 1000, 2) + @" " + _lng.LangSec;
                    if (nud_tmr2.Enabled) _tmrI[1] = Convert.ToDouble(nud_tmr2.Value); //16.11.2015

                    if (cb_trig_tmr2.SelectedIndex > 4)
                    {
                        key_codes(8);
                        Trig[1] = -2;
                        key_codes(2);
                    }
                    else
                    {
                        key_codes(2);
                        Trig[1] = cb_trig_tmr2.SelectedIndex;
                    }
                    TmrF[1] = 1;
                }

                if ((nud_tmr3.Value > 0 || CdrKey[2] == 1 || HoldKey[2] == 1) && cb_trig_tmr3.SelectedIndex > 0 &&
                    cb_key3.SelectedIndex > 0) //02.09.2015 chb_key3.Checked
                {
                    if (lb_tmr3_sec.Text != _lng.CbTmrCdr && lb_tmr3_sec.Text != _lng.CbTmrHold)
                        lb_tmr3_sec.Text = Math.Round(nud_tmr3.Value / 1000, 2) + @" " + _lng.LangSec;
                    if (nud_tmr3.Enabled) _tmrI[2] = Convert.ToDouble(nud_tmr3.Value); //16.11.2015
                    if (cb_trig_tmr3.SelectedIndex > 4)
                    {
                        key_codes(9);
                        Trig[2] = -3;
                        key_codes(3);
                    }
                    else
                    {
                        key_codes(3);
                        Trig[2] = cb_trig_tmr3.SelectedIndex;
                    }
                    TmrF[2] = 1;
                }

                if ((nud_tmr4.Value > 0 || CdrKey[3] == 1 || HoldKey[3] == 1) && cb_trig_tmr4.SelectedIndex > 0 &&
                    cb_key4.SelectedIndex > 0) //02.09.2015 chb_key4.Checked
                {
                    if (lb_tmr4_sec.Text != _lng.CbTmrCdr && lb_tmr4_sec.Text != _lng.CbTmrHold)
                        lb_tmr4_sec.Text = Math.Round(nud_tmr4.Value / 1000, 2) + @" " + _lng.LangSec;
                    if (nud_tmr4.Enabled) _tmrI[3] = Convert.ToDouble(nud_tmr4.Value); //16.11.2015
                    if (cb_trig_tmr4.SelectedIndex > 4)
                    {
                        key_codes(10);
                        Trig[3] = -4;
                        key_codes(4);
                    }
                    else
                    {
                        key_codes(4);
                        Trig[3] = cb_trig_tmr4.SelectedIndex;
                    }
                    TmrF[3] = 1;
                }
                if ((nud_tmr5.Value > 0 || CdrKey[4] == 1 || HoldKey[4] == 1) && cb_trig_tmr5.SelectedIndex > 0 &&
                    cb_key5.SelectedIndex > 0) //02.09.2015 chb_key5.Checked
                {
                    if (lb_tmr5_sec.Text != _lng.CbTmrCdr && lb_tmr5_sec.Text != _lng.CbTmrHold)
                        lb_tmr5_sec.Text = Math.Round(nud_tmr5.Value / 1000, 2) + @" " + _lng.LangSec;
                    if (nud_tmr5.Enabled) _tmrI[4] = Convert.ToDouble(nud_tmr5.Value); //16.11.2015
                    if (cb_trig_tmr5.SelectedIndex > 4)
                    {
                        key_codes(11);
                        Trig[4] = -5;
                        key_codes(5);
                    }
                    else
                    {
                        key_codes(5);
                        Trig[4] = cb_trig_tmr5.SelectedIndex;
                    }
                    TmrF[4] = 1;
                }
                if ((nud_tmr6.Value > 0 || CdrKey[5] == 1 || HoldKey[5] == 1) && cb_trig_tmr6.SelectedIndex > 0 &&
                    cb_key6.SelectedIndex > 0) //02.09.2015 chb_key6.Checked
                {
                    if (lb_tmr6_sec.Text != _lng.CbTmrCdr && lb_tmr6_sec.Text != _lng.CbTmrHold)
                        lb_tmr6_sec.Text = Math.Round(nud_tmr6.Value / 1000, 2) + @" " + _lng.LangSec;
                    if (nud_tmr6.Enabled) _tmrI[5] = Convert.ToDouble(nud_tmr6.Value); //16.11.2015
                    if (cb_trig_tmr6.SelectedIndex > 4)
                    {
                        key_codes(12);
                        Trig[5] = -6;
                        key_codes(6);
                    }
                    else
                    {
                        key_codes(6);
                        Trig[5] = cb_trig_tmr6.SelectedIndex;
                    }
                    TmrF[5] = 1;
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


                //if (tmr_f[0] + tmr_f[1] + tmr_f[2] + tmr_f[3] + tmr_f[4] + tmr_f[5] > 0)
                if (TmrF.Any(item => item == 1))
                {
                    timer_load(-1);
                    //_tmrAllCounter = 0;
                    if (!_tmrAll.Enabled) _tmrAll.Enabled = true;
                    if (!_screenCaptPrepared && CdrKey.Sum() > 0) screen_capt_pre(); //31.05.2015
                }

                //Блокирование элементов настройки, пока программа работает
                tb_prof_name.Enabled = false;
                foreach (var numud in pan_main.Controls.OfType<NumericUpDown>()) numud.Enabled = false;
                foreach (var cb in pan_main.Controls.OfType<ComboBox>()) cb.Enabled = false;
                foreach (var cbm in Controls.OfType<ComboBox>()) cbm.Enabled = false;
                cb_prog.Enabled = false;
                cb_proc.Enabled = false;
                cb_press_type.Enabled = false;
                if (chb_hold.Checked)
                    foreach (var chb in pan_main.Controls.OfType<CheckBox>()) chb.Enabled = false;
                //b_opt.Enabled = false;
                foreach (var b in Controls.OfType<Button>()) b.Enabled = false; //13.08.2015
            }
            else //Остановили работу программы
            {
                _progStart = false; //отметили, что выключились

                LogWriter(_log, "dh_log.txt");
                if (_log != null) _log.Clear();

                if (_windowDc != IntPtr.Zero) //16.10.2015
                {
                    _windowDc = IntPtr.Zero;
                    NativeMethods.ReleaseDC(_handle, _windowDc); //IntPtr.Zero
                }

                mouseKeyEventProvider1.Enabled = false;
                if (!chb_hold.Checked || cb_tmr1.SelectedIndex == 0 || cb_tmr1.SelectedIndex == 2)
                    nud_tmr1.Enabled = true; //02.09.2015 chb_key1.Checked
                if (!chb_hold.Checked || cb_tmr2.SelectedIndex == 0 || cb_tmr2.SelectedIndex == 2)
                    nud_tmr2.Enabled = true; //02.09.2015 chb_key2.Checked
                if (!chb_hold.Checked || cb_tmr3.SelectedIndex == 0 || cb_tmr3.SelectedIndex == 2)
                    nud_tmr3.Enabled = true; //02.09.2015 chb_key3.Checked
                if (!chb_hold.Checked || cb_tmr4.SelectedIndex == 0 || cb_tmr4.SelectedIndex == 2)
                    nud_tmr4.Enabled = true; //02.09.2015 chb_key4.Checked
                if (!chb_hold.Checked || cb_tmr5.SelectedIndex == 0 || cb_tmr5.SelectedIndex == 2)
                    nud_tmr5.Enabled = true; //02.09.2015 chb_key5.Checked
                if (!chb_hold.Checked || cb_tmr6.SelectedIndex == 0 || cb_tmr6.SelectedIndex == 2)
                    nud_tmr6.Enabled = true; //02.09.2015 chb_key6.Checked

                //foreach (NumericUpDown numud in this.pan_main.Controls.OfType<NumericUpDown>()) numud.Enabled = true;
                tb_prof_name.Enabled = true;
                foreach (var cb in pan_main.Controls.OfType<ComboBox>()) cb.Enabled = true;
                foreach (var cbm in Controls.OfType<ComboBox>()) cbm.Enabled = true;
                cb_prog.Enabled = true;
                cb_proc.Enabled = true;
                cb_press_type.Enabled = true;
                if (chb_hold.Checked)
                    foreach (var chb in pan_main.Controls.OfType<CheckBox>())
                        chb.Enabled = true;
                //if (cb_proc.SelectedIndex > 0) cb_prog.Enabled = false; //17.04.2015
                //b_opt.Enabled = true;
                foreach (var b in Controls.OfType<Button>()) b.Enabled = true; //13.08.2015

                _procSelected = false; //Отменяем выбор процесса после остановки

                cb_start.Text = @"Start";
                tt_key.SetToolTip(cb_start, _lng.TtStart);

                //for (int i = 0; i < 6; i++) //16.11.2015
                //{
                //    if (cdr_watch != null && cdr_watch[i] != null) StopWatch(cdr_watch[i]);
                //    //if (tmr[i] != null) timer_unload(i);
                //}

                //TimerStop(99); //Останавливаем все таймеры, включая главный
                //stopped_once = true;

                //timer_unload(99);
                //hold_clear(88);
                //tmr_cdr_destroy();
                //if (_returnWatch != null && _returnWatch.IsRunning)
                //    _returnWatch.Stop();
            }
        }

        /// <summary>
        ///     Закрытие формы, удаление горячей клавиши, сохранение текущих настроек в профиль.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void D3Hotkeys_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_optChange == 1 ||
                Location.X > -1 && Location.Y > -1 &&
                (Settings.Default.pos_x != Location.X || Settings.Default.pos_y != Location.Y))
                Save_settings(1);

            //System.Environment.Exit(1);
            Exit();
            //MessageBox.Show("Проверка");
        }

        /// <summary>
        ///     Три метода для обработки пункта "Автор".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_auth_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void lb_auth_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void lb_auth_Click(object sender, EventArgs e)
        {
            Process.Start("http://glasscannon.ru/forum/viewtopic.php?f=5&t=1988");
            //if (bt_lang.Text == "ENG") System.Diagnostics.Process.Start("http://horadric.ru/forum/viewtopic.php?f=16&t=26771");
            //else System.Diagnostics.Process.Start("http://www.diablofans.com/forums/diablo-iii-general-forums/diablo-iii-general-discussion/89743-offline-dps-calculator-diablo-3");
        }

        /// <summary>
        ///     Метод выбора языка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_lang_Click(object sender, EventArgs e)
        {
            if (lb_lang.Text == @"Eng")
            {
                lb_lang.Text = @"Rus";
                _lng.Lang_eng();
            }
            else
            {
                lb_lang.Text = @"Eng";
                _lng.Lang_rus();
            }
            Lang();
            key_menu();
            //chb_hold_CheckedChanged(null, null);
        }

        /// <summary>
        ///     Метод обработки языка
        /// </summary>
        private void Lang()
        {
            _langLoad = true;

            lb_trig1.Text = _lng.LbTrig1;
            lb_trig2.Text = _lng.LbTrig2;
            lb_trig3.Text = _lng.LbTrig3;
            lb_trig4.Text = _lng.LbTrig4;
            lb_trig5.Text = _lng.LbTrig5;
            lb_trig6.Text = _lng.LbTrig6;

            lb_key1.Text = _lng.LbKey1;
            lb_key2.Text = _lng.LbKey2;
            lb_key3.Text = _lng.LbKey3;
            lb_key4.Text = _lng.LbKey4;
            lb_key5.Text = _lng.LbKey5;
            lb_key6.Text = _lng.LbKey6;

            if (lb_tmr1_sec.Text.Contains("..")) lb_tmr1_sec.Text = _lng.LbTmrSec;
            if (lb_tmr2_sec.Text.Contains("..")) lb_tmr2_sec.Text = _lng.LbTmrSec;
            if (lb_tmr3_sec.Text.Contains("..")) lb_tmr3_sec.Text = _lng.LbTmrSec;
            if (lb_tmr4_sec.Text.Contains("..")) lb_tmr4_sec.Text = _lng.LbTmrSec;
            if (lb_tmr5_sec.Text.Contains("..")) lb_tmr5_sec.Text = _lng.LbTmrSec;
            if (lb_tmr6_sec.Text.Contains("..")) lb_tmr6_sec.Text = _lng.LbTmrSec;

            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("cb_tmr"))
                {
                    var i = cb.SelectedIndex;
                    cb.Items.Clear();
                    cb.Items.Add(_lng.CbTmrDef);
                    if (_resolution)
                    {
                        cb.Items.Add(_lng.CbTmrCdr);
                        cb.Items.Add(_lng.CbTmrCdrtime);
                    }
                    if (!HoldKey.Any(item => item == 1) || i > 1) cb.Items.Add(_lng.CbTmrHold); //14.09.2015
                    cb.SelectedIndex = i;
                }

            lb_key_delay_desc.Text = _lng.LbTmrSec;
            lb_nud_rand.Text = _lng.LbTmrSec;
            lb_nud_coold.Text = _lng.LbTmrSec;
            lb_rand.Text = _lng.LbRand;
            lb_coold.Text = _lng.LbCoold;
            lb_hot_prof.Text = _lng.LbHotProf;

            //lb_about.Text = lng.lb_about;
            lb_area.Text = _lng.LbArea;
            lb_proc.Text = _lng.LbProc;
            lb_press_type.Text = _lng.LbPressType;
            lb_returndelay.Text = _lng.LbReturndelay;
            lb_auth.Text = _lng.LbAuth;
            lb_help.Text = _lng.LbHelp;
            lb_prof.Text = chb_saveload.Checked ? _lng.LbProfSave : _lng.LbProf;
            lb_tp.Text = _lng.LbTp;
            lb_tpdelay.Text = _lng.LbTpdelay;
            lb_startstop.Text = _lng.LbStartstop;
            lb_map.Text = _lng.LbMap;
            lb_mapdelay.Text = _lng.LbMapdelay;

            chb_tray.Text = _lng.ChbTray;
            chb_mult.Text = _lng.ChbMult;
            chb_users.Text = _lng.ChbUsers;
            //chb_proconly.Text = lng.chb_proconly; 17.04.2015
            b_opt.Text = _lng.BOpt;
            lb_key_delay_ms.Text = _lng.LbKeyDelayMs;
            lb_key_delay.Text = _lng.LbKeyDelay;

            tt_key.SetToolTip(cb_start, _lng.TtStart);
            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("key")) tt_key.SetToolTip(cb, _lng.TtKey);
                else if (cb.Name.Contains("trig")) tt_key.SetToolTip(cb, _lng.TtTrig);
                else if (cb.Name.Contains("tmr")) tt_key.SetToolTip(cb, _lng.TtMode);
            foreach (var chb in pan_main.Controls.OfType<CheckBox>()) tt_key.SetToolTip(chb, _lng.TtHold);
            foreach (var nud in pan_main.Controls.OfType<NumericUpDown>()) tt_key.SetToolTip(nud, _lng.TtDelay);

            tt_key.SetToolTip(cb_proc, _lng.TtProc);
            tt_key.SetToolTip(cb_press_type, _lng.TtCbPressType); //17.05.2016
            tt_key.SetToolTip(tb_prof_name, _lng.TtProfname);
            tt_key.SetToolTip(b_opt, _lng.TtOpt);
            tt_key.SetToolTip(lb_lang, _lng.TtLang);
            tt_key.SetToolTip(lb_num, _lng.TtNum);
            tt_key.SetToolTip(lb_caps, _lng.TtCaps);
            tt_key.SetToolTip(lb_scroll, _lng.TtScroll);
            tt_key.SetToolTip(lb_auth, _lng.TtForum);
            tt_key.SetToolTip(lb_help, _lng.TtHelp);
            tt_key.SetToolTip(cb_prof, _lng.TtProf);
            tt_key.SetToolTip(cb_startstop, _lng.TtOpStart);
            tt_key.SetToolTip(cb_returndelay, _lng.TtOpEnter);
            tt_key.SetToolTip(cb_tp, _lng.TtOpTele);
            tt_key.SetToolTip(cb_tpdelay, _lng.TtOpTelePause);
            tt_key.SetToolTip(cb_map, _lng.TtOpMap);
            tt_key.SetToolTip(cb_mapdelay, _lng.TtOpMapPause);
            tt_key.SetToolTip(cb_key_delay, _lng.TtOpAlt);
            tt_key.SetToolTip(nud_key_delay_ms, _lng.TtOpAltPause);
            tt_key.SetToolTip(nud_rand, _lng.TtOpRand);
            tt_key.SetToolTip(nud_coold, _lng.TtOpCdr);
            tt_key.SetToolTip(cb_hot_prof, _lng.TtOpProf);
            tt_key.SetToolTip(chb_mult, _lng.TtOpMult);
            tt_key.SetToolTip(chb_tray, _lng.TtOpTray);
            tt_key.SetToolTip(chb_hold, _lng.TtOpAdv);
            tt_key.SetToolTip(chb_mpress, _lng.TtOpMultpress);
            tt_key.SetToolTip(chb_log, _lng.TtOpLog);
            tt_key.SetToolTip(chb_saveload, _lng.TtOpSaveload);
            tt_key.SetToolTip(chb_users, _lng.TtOpPers);
            tt_key.SetToolTip(lb_ver_check, _lng.TtOpNewver);
            tt_key.SetToolTip(chb_ver_check, _lng.TtOpNewver);

            lb_hold.Text = _lng.LbHold;
            chb_hold.Text = _lng.ChbHold;
            chb_mpress.Text = _lng.ChbMpress;
            chb_log.Text = _lng.ChbLog;

            error_not(pan_opt.Visible ? 1 : 2);

            if (tb_prof_name.Text == "" || tb_prof_name.Text == @"Наименование профиля" ||
                tb_prof_name.Text == @"Profile name")
                tb_prof_name.Text = _lng.TbProfName;

            if (_lbKeyDesc != null) _lbKeyDesc.Text = _lng.LbKeyDesc;

            //chb_ver_check.Text = lng.chb_ver_check;
            lb_ver_check.Text = _lng.ChbVerCheck;
        }

        /// <summary>
        ///     Метод для выбора профилей, сохранения и чтения настроек из них.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_prof_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Save_settings(0);
            //var k = cb_hot_prof.SelectedIndex;

            var patchlocal = "";
            switch (cb_prof.SelectedIndex)
            {
                case 1:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof1.xml");
                    break;
                case 2:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof2.xml");
                    break;
                case 3:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof3.xml");
                    break;
                case 4:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof4.xml");
                    break;
                case 5:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof5.xml");
                    break;
                case 6:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof6.xml");
                    break;
                case 7:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof7.xml");
                    break;
                case 8:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof8.xml");
                    break;
                case 9:
                    patchlocal = Path.Combine(Directory.GetCurrentDirectory(), "d3h_prof9.xml");
                    break;
            }

            if (patchlocal != "" && File.Exists(patchlocal)) ReadXml(patchlocal);
            else if (patchlocal != "") Save_settings(1); //08.04.2015

            //cb_key_delay_SelectedIndexChanged(null, null);
            //cb_map_SelectedIndexChanged(null, null);
            //cb_tp_SelectedIndexChanged(null, null);

            //cb_hot_prof.SelectedIndex = k;

            //if (pan_proc.Visible == true) cb_proc.SelectedIndex = -1;  //14.05.2015

            //if (!chb_proconly.Checked && cb_proc.SelectedIndex < 1) cb_prog.Enabled = true; //17.04.2015
            //cb_prog_SelectionChangeCommitted(null, null); //17.04.2015
            chb_hold_CheckedChanged(null, null);


            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("cb_tmr"))
                    cb_tmr_SelectedIndexChanged(cb, null);


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
            using (var saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = @"Xml Files|*.xml";
                saveFileDialog1.Title = @"Select a xml File to save";
                saveFileDialog1.FileName = tb_prof_name.Text;
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                //MessageBox.Show(openFileDialog1.FileName.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
                _path = saveFileDialog1.FileName;
                Save_settings(0);
                //WriteXML(saveFileDialog1.FileName.ToString());
            }
        }

        private void tsmi_load_Click(object sender, EventArgs e)
        {
            lb_lang_name.Focus();
            using (var openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = @"Xml Files|*.xml";
                openFileDialog1.Title = @"Select a xml File to load";
                //openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
                ReadXml(openFileDialog1.FileName);
                cb_prog_SelectionChangeCommitted(null, null);
                if (cb_proc != null) cb_proc.SelectedIndex = -1;
                cb_proc_SelectionChangeCommitted(null, null);
            }
        }

        /// <summary>
        ///     Метод установки глобального хоткея запуска/остановки
        /// </summary>
        private void Reghotkey()
        {
            var id = 0; // The id of the hotkey. 
            if (cb_startstop.SelectedIndex > 0)
                NativeMethods.RegisterHotKey(Handle, id, (int) KeyModifier.None, FKeys[cb_startstop.SelectedIndex - 1]);
        }

        /// <summary>
        ///     Метод регистрации хоткея запуска/остановки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_startstop_SelectedIndexChanged(object sender, EventArgs e)
        {
            NativeMethods.UnregisterHotKey(Handle, 0);
            Reghotkey();
        }

        /// <summary>
        ///     Метод выбора кнопки для хоткея приостановки (телепорта)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_tp_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tpKey = "";
            if (cb_tp.SelectedIndex > 0 && (string) cb_tp.Items[cb_tp.SelectedIndex] != "") //24.04.2015
                _tpKey = cb_tp.Items[cb_tp.SelectedIndex].ToString();
            if (_tpKey.Length > 0 && _tpKey.Substring(0, 1) == "*") _tpKey = _tpKey.Remove(0, 1);
        }

        /// <summary>
        ///     Метод установки задержки приостановки (при телепорте)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_tpdelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tpDelay = 0;
            if (cb_tpdelay.SelectedIndex > 0) _tpDelay = Convert.ToInt32(cb_tpdelay.Items[cb_tpdelay.SelectedIndex]);
        }

        /// <summary>
        ///     Метод выбора процессов в памяти >400Мб
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        private void cb_proc_Click(object sender, EventArgs e)
        {
            _procHandle = new Dictionary<string, IntPtr>();
            cb_proc.Items.Clear();
            var processlist = Process.GetProcesses();
            _handle = IntPtr.Zero;
            _procIdList = new List<int>();

            //cb_proc.SelectedIndex = -1;
            //cb_proc_SelectionChangeCommitted(null, null);

            //if (!chb_proconly.Checked && cb_proc.SelectedIndex < 1) cb_prog.Enabled = true; //17.04.2015

            cb_proc.Items.Add("");
            try
            {
                foreach (var p in processlist)
                {
                    var procName = "";

                    if (p.PagedMemorySize64 > _procSize)
                        procName = p.ProcessName;
                    else if (lb_debug.Visible && p.ProcessName.ToLower().Contains("dllhost"))
                        procName = "картинка";
                    else if (lb_debug.Visible &&
                             (p.ProcessName.ToLower().Contains("akelpad") || p.ProcessName.ToLower().Contains("notepad")))
                        procName = "блокнот";
                    //else if (p.ProcessName.ToLower().Contains("opera") && p.MainWindowTitle.Contains("Clicker Heroes"))
                    //    procName = "opera";

                    if (procName == "") continue;

                    cb_proc.Items.Add(procName + " " + p.Id);
                    _procHandle.Add(procName + " " + p.Id, p.MainWindowHandle);
                    _procIdList.Add(p.Id);
                }
            }
            catch
            {
                // ignored
            }

            //bool result = false;
            for (var i = 0; i < cb_proc.Items.Count; i++)
                if (cb_proc.Items[i].ToString().ToLower().Contains(_diabloName))
                {
                    cb_proc.SelectedIndex = i;
                    _procSelected = true;
                }
            if (!_procSelected) cb_proc.SelectedIndex = -1;
            cb_proc_SelectionChangeCommitted(null, null);
        }

        /// <summary>
        ///     Метод установки режима доступа к области действия в зависимости от установки меню процесса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_proc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _procCurr = "";

            var index = cb_proc.SelectedIndex;

            if (index > -1) _procCurr = cb_proc.Items[index].ToString();
            if (_procCurr != "") //|| chb_proconly.Checked //17.04.2015
            {
                _d3Proc = true;
                _d3Prog = false;
                _procSelected = true;
                cb_prog.SelectedIndex = 0;
                //cb_prog.Enabled = false; //17.04.2015
                //if (proc_curr != "") //16.10.2015
                _handle = _procHandle[_procCurr];
                if (_procIdList != null && _procIdList.Count >= index - 1)
                    _procId = _procIdList[index - 1];

                //Process.GetProcesses().Contains(Process.GetProcessById(123213));

                //if (cb_proc.Text.Contains("картинка")) //31.05.2016 for test?
                //warframe = true; //Warframe, 15.01.2016

                if (lb_debug.Visible && cb_proc.Text.Contains("блокнот"))
                {
                    _handle = NativeMethods.FindWindow(null, "akelpad");
                    _handle = NativeMethods.FindWindowEx(_handle, IntPtr.Zero, "AkelEditW", null); //For debugging
                    if (_handle == IntPtr.Zero) _handle = NativeMethods.FindWindow(null, "notepad");
                }

                if (lb_debug.Visible && cb_proc.Text.Contains("картинка"))
                    _debugPic = true;

                //if (lb_debug.Visible && cb_proc.Text.Contains("opera")) //25.09.2015
                //{
                //handle = FindWindowEx(handle, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", null);  //For debugging
                //}

                //for (int i = 1; i < 7; i++)
                //{
                //    cb_key_del(i);
                //}

                //pan_hold.Visible = false;
                pan_press_type.Visible = true;
            }
            else
            {
                _d3Proc = false;
                _handle = IntPtr.Zero;

                pan_press_type.Visible = false;

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
            _handleRef = new HandleRef(this, _handle); //11.09.2015
            error_not(2);
        }

        /// <summary>
        ///     Метод выбора области действия программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_prog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_prog.SelectedIndex == 1)
                _d3Prog = true;
            if (cb_prog.SelectedIndex == 0)
                _d3Prog = false;
        }

        /// <summary>
        ///     Метод отключения/выключения списка процессов из-за активной/неактивной области действия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_prog_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cb_prog != null && cb_proc != null && cb_prog.SelectedIndex == 1)
                _d3Proc = false;
            //else
            //{
            //    cb_proc.Enabled = true;
            //}
        }

        private void d3hot_Resize(object sender, EventArgs e)
        {
            if (_frmKeyInput != null) _frmKeyInput.Hide();
            if (WindowState != FormWindowState.Minimized) return;
            if (_formShown && chb_tray.Checked)
            {
                if (!_startMain || !_startOpt) return;
                Hide();
                notify_d3h.Visible = true;
            }
            else if (!_formShown && Settings.Default.chb_tray == 1)
            {
                Hide();
                notify_d3h.Visible = true;
            }
        }

        /// <summary>
        ///     Метод остановки основных таймеров 99 - все таймеры, от 6 до 98 - все без tmrAll, меньше 6 - индивидуальные без
        ///     _tmrCdr
        /// </summary>
        /// <param name="flag"></param>
        private void MainTimerStop(int flag)
        {
            if (flag > 88 && _tmrAll != null)
            {
                _tmrAll.Dispose();
                _tmrAll = null;
            }
            if (flag > 5 && _tmrCdr != null)
            {
                _tmrCdr.Dispose();
                _tmrCdr = null;

                //if (_debug)
                //{
                //    var timenow = DateTime.Now.ToLongTimeString();
                //    MessageBox.Show(timenow + @" Кто-то хочет закрыть таймер CDR из MainTimerStop");
                //}
            }

            timer_unload(flag); //Удаляем данные по обычным таймерам: _tmrAll, _tmrWatch[j], _tmr[j], TmrR[j], TmrPress
            tmr_cdr_destroy(flag);
                //Удаляем данные по таймерам кулдауна: _tmrCdr, _cdrWatch[i], _tmr[i], CdrRun[i], TmrPress
            hold_clear(flag);
                //Удаляем данные по зажатым клавишам: _startTimer[h], _repeatTimer[h], Hold[h], _stTimerR, _rTimerR

            Array.Clear(TrigUsed, 0, 6);
            Array.Clear(TrigPress, 0, 6);
            Array.Clear(_trigPressed, 0, 6);
        }

        /// <summary>
        ////Метод остановки таймеров 99 - все таймеры, меньше - все, кроме главного
        /// </summary>
        private void TimerStop(int flag)
        {
            MainTimerStop(flag);

            if (_returnWatch != null && _returnWatch.IsRunning) _returnWatch.Stop();
            if (_keyWatch != null && _keyWatch.IsRunning) _keyWatch.Stop();
            if (_mapWatch != null && _mapWatch.IsRunning) _mapWatch.Stop();
            if (_teleWatch != null && _teleWatch.IsRunning) _teleWatch.Stop();
            if (_hoverWatch != null && _hoverWatch.IsRunning) _hoverWatch.Stop();

            _delayWait = 0;
            _returnPress = 0;
            _rPress = 0;
            _tPress = 0;
            _mapPress = 0;
            _delayPress = 0;
            //_tmrCdrInt = 30;
            _returnPressCount = 0;
            _shiftPress = 0;

            _wasUsed = false;

            Array.Clear(TmrLeft, 0, 6);

            if (_tmrVer != null) _tmrVer.Dispose(); //Таймер проверки версии программы

            if (_tmrHover != null) //Пока не используется
            {
                _tmrHover.Dispose();
                _tmrHover = null;
            }

            if (flag < 99) return; //Дальше очищаем то, что обновляется только при старте программы

            Array.Clear(Trig, 0, 6);
            Array.Clear(_tmrI, 0, 6);
            Array.Clear(TmrF, 0, 6);
        }

        /// <summary>
        ///     Метод при выходе из программы
        /// </summary>
        private void Exit() //11.01.2016 //object sender, EventArgs e
        {
            if (!_progRun) return;
            try
            {
                _progRun = false; //Помечаем программу на закрытие
                _progStart = false; //Отмечаем, что программа остановлена

                TimerStop(99); //Останавливаем все таймеры, включая главный

                LogWriter(_log, "dh_log.txt");
                if (_log != null) _log.Clear();

                for (var i = 0; i < 4; i++)
                    NativeMethods.UnregisterHotKey(Handle, i);
                // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.   

                if (_windowDc != IntPtr.Zero) //16.10.2015
                {
                    _windowDc = IntPtr.Zero;
                    NativeMethods.ReleaseDC(_handle, _windowDc); //IntPtr.Zero
                }

                if (_handle != IntPtr.Zero) //06.05.2016
                {
                    _handle = IntPtr.Zero;
                    NativeMethods.CloseHandle(_handle);
                }

                if (_procHandle != null && _procHandle.Count > 0)
                    _procHandle = null;

                for (var i = 0; i < 6; i++)
                {
                    if (_startTimer[i] != null) _startTimer[i].Dispose();
                    if (_repeatTimer[i] != null) _repeatTimer[i].Dispose();
                }

                //if (TmrSave != null) TmrSave.Dispose();
                //if (TmrPic != null) TmrPic.Dispose();
                //if (TmrPicPress != null) TmrPicPress.Dispose();

                if (_frmKeyInput != null) _frmKeyInput.Dispose();
                if (_lbKeyDesc != null) _lbKeyDesc.Dispose();
                if (_lbKeyPrev != null) _lbKeyPrev.Dispose();
                if (_bKeyOk != null) _bKeyOk.Dispose();
            }
            catch (Exception ex) //09.06.2016
            {
                const string sourceName = "D3Hot Closing Error";
                if (!EventLog.SourceExists(sourceName))
                    EventLog.CreateEventSource(sourceName, "Application");

                using (var eventLog = new EventLog())
                {
                    eventLog.Source = sourceName;
                    var message = string.Format("Exception: {0} \n\nStack: {1}", ex.Message, ex.StackTrace);
                    eventLog.WriteEntry(message, EventLogEntryType.Error);
                }
            }

            if (Application.MessageLoop)
                Application.Exit();
            else
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

            if (_optClick == 1) _optChange = 1;

            if (cb != null && (cb.Name == "cb_key_delay" || cb.Name == "cb_tp" || cb.Name == "cb_map"))
                key_choose_SelectionChangeCommitted(cb, null);

            error_not(1);
        }

        /// <summary>
        ///     Метод обработки ошибок пересечения триггеров и кнопок.
        /// </summary>
        private void error_hotkeys(ListControl cb, string key)
        {
            _crossTrig = false;
            //var check = false;
            var keyCheck = key;

            var cbNum = key_find(cb);

            if (cbNum > -1 && cbNum < 5 && cb.SelectedIndex > 1 && cb.SelectedIndex < 5)
                keyCheck = "D" + keyCheck;

            keyCheck = keyCheck.Replace("*", "");

            //if (cb_num < 9) 
            var check = double_key_check(cbNum, keyCheck);

            if (check) _crossTrig = true;
        }

        /// <summary>
        ///     Метод обработки ошибок отсутствия выбора
        /// </summary>
        private void error_select()
        {
            int[] nulls = {0, 0, 0, 0, 0, 0};
            int[] keyNulls = {0, 0, 0, 0, 0, 0};
            var notrig = true;

            foreach (var numud in pan_main.Controls.OfType<NumericUpDown>())
                if (numud.Text == "") numud.Value = 0;

            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("trig") && cb.SelectedIndex > 0) notrig = false;

            if (cb_trig_tmr1.SelectedIndex > 0 &&
                (nud_tmr1.Value != 0 || cb_tmr1.SelectedIndex > 1 || cb_tmr1.SelectedIndex > 0))
                nulls[0] = 1; //02.09.2015 chb_key1.Checked
            if (cb_trig_tmr2.SelectedIndex > 0 &&
                (nud_tmr2.Value != 0 || cb_tmr2.SelectedIndex > 1 || cb_tmr2.SelectedIndex > 0))
                nulls[1] = 1; //02.09.2015 chb_key2.Checked
            if (cb_trig_tmr3.SelectedIndex > 0 &&
                (nud_tmr3.Value != 0 || cb_tmr3.SelectedIndex > 1 || cb_tmr3.SelectedIndex > 0))
                nulls[2] = 1; //02.09.2015 chb_key3.Checked
            if (cb_trig_tmr4.SelectedIndex > 0 &&
                (nud_tmr4.Value != 0 || cb_tmr4.SelectedIndex > 1 || cb_tmr4.SelectedIndex > 0))
                nulls[3] = 1; //02.09.2015 chb_key4.Checked
            if (cb_trig_tmr5.SelectedIndex > 0 &&
                (nud_tmr5.Value != 0 || cb_tmr5.SelectedIndex > 1 || cb_tmr5.SelectedIndex > 0))
                nulls[4] = 1; //02.09.2015 chb_key5.Checked
            if (cb_trig_tmr6.SelectedIndex > 0 &&
                (nud_tmr6.Value != 0 || cb_tmr6.SelectedIndex > 1 || cb_tmr6.SelectedIndex > 0))
                nulls[5] = 1; //02.09.2015 chb_key6.Checked

            if (nulls[0] == 1 && cb_key1.SelectedIndex > 0) keyNulls[0] = 1;
            if (nulls[1] == 1 && cb_key2.SelectedIndex > 0) keyNulls[1] = 1;
            if (nulls[2] == 1 && cb_key3.SelectedIndex > 0) keyNulls[2] = 1;
            if (nulls[3] == 1 && cb_key4.SelectedIndex > 0) keyNulls[3] = 1;
            if (nulls[4] == 1 && cb_key5.SelectedIndex > 0) keyNulls[4] = 1;
            if (nulls[5] == 1 && cb_key6.SelectedIndex > 0) keyNulls[5] = 1;

            if (!_crossTrig && !notrig && nulls.Contains(1) && keyNulls.Contains(1) && _startOpt)
            {
                error_show(1);
                _startMain = true;
            }
            else
            {
                _startMain = false;
                if (notrig)
                {
                    lb_hold.Text = _lng.LbHoldTrig; //"Не выбрано ни одного триггера для активации."
                }
                else if (!nulls.Contains(1))
                {
                    lb_hold.Text = _lng.LbHoldDelay; //"Не выставленаы паузы для триггеров."
                }
                else if (!keyNulls.Contains(1))
                {
                    lb_hold.Text = _lng.LbHoldKey; //"Не выбраны клавиши для триггеров."              
                }
                else if (_crossTrig)
                {
                    lb_hold.Text = _lng.LbHoldCross; //"Пересечение хоткеев кнопок и триггеров."              
                }
                else if (!_startOpt)
                {
                    error_not(2);
                    _startMain = true;
                }
                if (_crossTrig || notrig || !nulls.Contains(1) || !keyNulls.Contains(1) || !_startOpt)
                    error_show(2);
            }
        }

        /// <summary>
        ///     Метод обработки ошибок при пользовательском выборе.
        /// </summary>
        /// <param name="i">1: хоткеи, 2: процессы</param>
        private void error_not(int i)
        {
            var hotProf = 0;
            var noProc = 0;
            var hotTpmap = 0;

            if (cb_startstop.SelectedIndex > 0 && cb_hot_prof.SelectedIndex > 0 &&
                cb_hot_prof.Items[cb_hot_prof.SelectedIndex].ToString()
                    .Contains(cb_startstop.Items[cb_startstop.SelectedIndex].ToString())
                &&
                !(cb_startstop.Items[cb_startstop.SelectedIndex].ToString() == "F1" &&
                  (cb_hot_prof.Items[cb_hot_prof.SelectedIndex].ToString().Contains("F11") ||
                   cb_hot_prof.Items[cb_hot_prof.SelectedIndex].ToString().Contains("F10")))
            )
                hotProf = 1;


            if (i == 1 &&
                (_tpKey.Length > 0 && (_tpKey == _keyDelay || _tpKey == _mapKey)
                 || _mapKey.Length > 0 && _mapKey == _keyDelay)
            )
                hotTpmap = 1;

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
                noProc = 1;

            switch (i)
            {
                case 1: //Ошибка пересечения хоткеев
                    if (noProc == 1) //Не выбран процесс
                    {
                        lb_hold.Text = _lng.LbHold;
                        lb_proc.Font = new Font(lb_proc.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else //Процесс выбран
                    {
                        lb_proc.Font = new Font(lb_proc.Font, FontStyle.Regular);
                    }

                    if (hotProf == 1) //Хоткеи старта программы/профилей пересекаются
                    {
                        lb_hold.Text = _lng.LbHoldHot;
                        lb_startstop.Font = new Font(lb_startstop.Font, FontStyle.Bold);
                        lb_hot_prof.Font = new Font(lb_hot_prof.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else //Хоткеи старта программы/профилей не пересекаются
                    {
                        lb_startstop.Font = new Font(lb_startstop.Font, FontStyle.Regular);
                        lb_hot_prof.Font = new Font(lb_hot_prof.Font, FontStyle.Regular);
                    }

                    if (hotTpmap == 1) //Хоткеи карты/телепорта пересекаются
                    {
                        lb_hold.Text = _lng.LbHoldHot;
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Bold);
                        lb_tp.Font = new Font(lb_tp.Font, FontStyle.Bold);
                        lb_key_delay.Font = new Font(lb_key_delay.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else //Хоткеи карты/телепорта не пересекаются
                    {
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Regular);
                        lb_tp.Font = new Font(lb_tp.Font, FontStyle.Regular);
                        lb_key_delay.Font = new Font(lb_key_delay.Font, FontStyle.Regular);
                    }

                    if (_crossTrig)
                        lb_hold.Text = _lng.LbHoldCross; //"Пересечение хоткеев кнопок и триггеров." 

                    break;

                case 2: //Ошибка без выбора процесса
                    if (hotProf == 1) //Хоткеи старта программы/профилей пересекаются
                    {
                        lb_hold.Text = _lng.LbHoldHot;
                        lb_startstop.Font = new Font(lb_startstop.Font, FontStyle.Bold);
                        lb_hot_prof.Font = new Font(lb_hot_prof.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else //Хоткеи старта программы/профилей не пересекаются
                    {
                        lb_startstop.Font = new Font(lb_startstop.Font, FontStyle.Regular);
                        lb_hot_prof.Font = new Font(lb_hot_prof.Font, FontStyle.Regular);
                    }

                    if (hotTpmap == 1) //Хоткеи карты/телепорта пересекаются
                    {
                        lb_hold.Text = _lng.LbHoldHot;
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Bold);
                        lb_tp.Font = new Font(lb_tp.Font, FontStyle.Bold);
                        lb_key_delay.Font = new Font(lb_key_delay.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else //Хоткеи карты/телепорта не пересекаются
                    {
                        lb_map.Font = new Font(lb_map.Font, FontStyle.Regular);
                        lb_tp.Font = new Font(lb_tp.Font, FontStyle.Regular);
                        lb_key_delay.Font = new Font(lb_key_delay.Font, FontStyle.Regular);
                    }

                    if (noProc == 1) //Не выбран процесс
                    {
                        lb_hold.Text = _lng.LbHold;
                        lb_proc.Font = new Font(lb_proc.Font, FontStyle.Bold);
                        //pan_hold.Visible = true;
                    }
                    else //Процесс выбран
                    {
                        lb_proc.Font = new Font(lb_proc.Font, FontStyle.Regular);
                    }

                    if (_crossTrig)
                        lb_hold.Text = _lng.LbHoldCross; //"Пересечение хоткеев кнопок и триггеров." 

                    break;
            }

            if (!_crossTrig && hotProf != 1 && noProc != 1 && hotTpmap != 1)
            {
                error_show(1);
                _startOpt = true;
                if (!_startMain) error_select();
            }
            else
            {
                error_show(2);
                _startOpt = false;
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

            if (!pan_opt.Visible || _optClick == 0)
            {
                pan_opt.BringToFront();
                pan_opt.Visible = true;
                _optClick = 1;
            }
            else
            {
                error_not(1);
                if (lb_hold.Text == _lng.LbHoldHot && pan_hold.Visible) return;
                _optClick = 0;
                lb_lang_name.Focus();
                pan_opt.SendToBack();
                pan_opt.Visible = false;
                if (_optChange != 1) return;
                Save_settings(0);
                _optChange = 0;
            }
        }

        private void cb_key_delay_SelectedIndexChanged(object sender, EventArgs e)
        {
            _keyDelay = "";
            if (cb_key_delay.SelectedIndex > 0 && (string) cb_key_delay.Items[cb_key_delay.SelectedIndex] != "")
            {
                _keyDelay = cb_key_delay.Items[cb_key_delay.SelectedIndex].ToString();
                if (_keyDelay == "1" || _keyDelay == "2" || _keyDelay == "3" || _keyDelay == "4")
                    _keyDelay = "D" + _keyDelay;
                nud_key_delay_ms.Enabled = true;
                if (_keyDelay.Length > 0 && _keyDelay.Substring(0, 1) == "*") _keyDelay = _keyDelay.Remove(0, 1);
            }
            else
            {
                nud_key_delay_ms.Value = 0;
                nud_key_delay_ms.Enabled = false;
                //key_delay = "";
            }
        }

        //private void check_only()
        //{
        //    int i = 0, j = 0;

        //    foreach (var cheb in pan_main.Controls.OfType<CheckBox>())
        //    {
        //        i++;
        //        if (cheb.Checked) j = i;
        //    }

        //    if (j > 0)
        //    {
        //        i = 0;
        //        foreach (var cheb in pan_main.Controls.OfType<CheckBox>())
        //        {
        //            i++;
        //            if (j != i)
        //            {
        //                cheb.Checked = false;
        //                cheb.Visible = false;
        //            }
        //        }
        //        hold_use = true;
        //        //for (int k = 1; k < 7; k++)
        //        //{
        //        //    cb_key_del(k);
        //        //}
        //    }
        //    else
        //    {
        //        hold_use = false;
        //        if (chb_hold.Checked)
        //            foreach (var cheb in pan_main.Controls.OfType<CheckBox>())
        //                cheb.Visible = true;


        //        foreach (var cb in pan_main.Controls.OfType<ComboBox>())
        //        {
        //            if (cb.Name.Contains("tmr") && !cb.Name.Contains("trig")) cb.Visible = true;
        //            cb_tmr_SelectedIndexChanged(cb, null);
        //        }

        //        //if (cb_proc.SelectedIndex>0)
        //        //    for (int k = 1; k < 7; k++)
        //        //    {
        //        //        cb_key_del(k);
        //        //    }
        //    }
        //    error_not(2);
        //}

        /// <summary>
        ///     Метод для автоматического выбора процесса Diablo 3
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
        private void chb_trig_CheckedChanged(object sender, EventArgs e)
        {
            if (_langLoad) _optChange = 1; //14.09.2015
            var cb = (CheckBox) sender;
            var chb = ((CheckBox) sender).Name;

            //check_only();

            switch (chb)
            {
                case "chb_trig1":
                    if (cb_trig_tmr1.SelectedIndex == 1) cb.Checked = true;
                    if (cb.Checked && (cb_trig_tmr1.SelectedIndex == 1 || cb_trig_tmr1.SelectedIndex == 5))
                    {
                        TrigHold[0] = true;
                    }
                    else
                    {
                        TrigHold[0] = false;
                        cb.Checked = false;
                    }
                    break;

                case "chb_trig2":
                    if (cb_trig_tmr2.SelectedIndex == 1) cb.Checked = true;
                    if (cb.Checked && (cb_trig_tmr2.SelectedIndex == 1 || cb_trig_tmr2.SelectedIndex == 5))
                    {
                        TrigHold[1] = true;
                    }
                    else
                    {
                        TrigHold[1] = false;
                        cb.Checked = false;
                    }
                    break;

                case "chb_trig3":
                    if (cb_trig_tmr3.SelectedIndex == 1) cb.Checked = true;
                    if (cb.Checked && (cb_trig_tmr3.SelectedIndex == 1 || cb_trig_tmr3.SelectedIndex == 5))
                    {
                        TrigHold[2] = true;
                    }
                    else
                    {
                        TrigHold[2] = false;
                        cb.Checked = false;
                    }
                    break;

                case "chb_trig4":
                    if (cb_trig_tmr4.SelectedIndex == 1) cb.Checked = true;
                    if (cb.Checked && (cb_trig_tmr4.SelectedIndex == 1 || cb_trig_tmr4.SelectedIndex == 5))
                    {
                        TrigHold[3] = true;
                    }
                    else
                    {
                        TrigHold[3] = false;
                        cb.Checked = false;
                    }
                    break;

                case "chb_trig5":
                    if (cb_trig_tmr5.SelectedIndex == 1) cb.Checked = true;
                    if (cb.Checked && (cb_trig_tmr5.SelectedIndex == 1 || cb_trig_tmr5.SelectedIndex == 5))
                    {
                        TrigHold[4] = true;
                    }
                    else
                    {
                        TrigHold[4] = false;
                        cb.Checked = false;
                    }
                    break;

                case "chb_trig6":
                    if (cb_trig_tmr6.SelectedIndex == 1) cb.Checked = true;
                    if (cb.Checked && (cb_trig_tmr6.SelectedIndex == 1 || cb_trig_tmr6.SelectedIndex == 5))
                    {
                        TrigHold[5] = true;
                    }
                    else
                    {
                        TrigHold[5] = false;
                        cb.Checked = false;
                    }
                    break;
            }
        }


        /// <summary>
        ///     Метод добавления пунктов меню с Shift
        /// </summary>
        private void cb_key_add()
        {
            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("key"))
                {
                    var cbSelect = cb.SelectedIndex;
                    var shiftPos = cb.FindString("Shift");
                    var starPos = cb.FindString("*");

                    var cbSelectNew = cbSelect;

                    if (shiftPos < 2)
                    {
                        cb.Items.Add("Shift+LM");
                        cb.Items.Add("Shift+RM");
                        if (cbSelect == starPos) cbSelectNew = cbSelectNew + 2;
                    }

                    if (starPos > 0)
                    {
                        cb.Items.RemoveAt(cb.FindString("*"));
                        if (cbSelect == starPos) cbSelectNew--;
                    }

                    if (star_add(cb) && cbSelect == starPos)
                        cbSelectNew++;

                    cb.SelectedIndex = cbSelectNew;
                }
        }

        /// <summary>
        ///     Метод удаления пунктов меню с Shift
        /// </summary>
        private void cb_key_del()
        {
            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("key"))
                {
                    var cbSelect = cb.SelectedIndex;
                    var shiftPos = cb.FindString("Shift");
                    var starPos = cb.FindString("*");
                    var cbSelectNew = cbSelect;
                    var starPosNew = starPos;

                    if (shiftPos > 0)
                    {
                        cb.Items.RemoveAt(shiftPos);
                        cb.Items.RemoveAt(shiftPos);
                        if (cbSelect >= shiftPos) cbSelectNew = cbSelect - 2;
                        starPosNew = starPos - 2;
                    }
                    if (starPos > 0)
                    {
                        cb.Items.RemoveAt(starPosNew);
                        if (cbSelect == starPos) cbSelectNew--;
                    }

                    if (star_add(cb) && starPos > 0 && cbSelect == starPos) cbSelectNew++;

                    cb.SelectedIndex = cbSelectNew;
                }
        }

        private bool star_add(ComboBox cb)
        {
            var result = false;
            string[] keys =
            {
                "cb_key1", "cb_key2", "cb_key3", "cb_key4", "cb_key5", "cb_key6", "cb_tp", "cb_map", "cb_key_delay",
                "cb_trig_tmr1", "cb_trig_tmr2", "cb_trig_tmr3", "cb_trig_tmr4", "cb_trig_tmr5", "cb_trig_tmr6"
            }; //08.09.2015
            string[] vals =
            {
                Settings.Default.cb_key1_desc, Settings.Default.cb_key2_desc, Settings.Default.cb_key3_desc,
                Settings.Default.cb_key4_desc, Settings.Default.cb_key5_desc, Settings.Default.cb_key6_desc,
                Settings.Default.cb_tp_desc, Settings.Default.cb_map_desc, Settings.Default.cb_key_delay_desc,
                Settings.Default.cb_trig_tmr1_desc, Settings.Default.cb_trig_tmr2_desc,
                Settings.Default.cb_trig_tmr3_desc, //08.09.2015
                Settings.Default.cb_trig_tmr4_desc, Settings.Default.cb_trig_tmr5_desc,
                Settings.Default.cb_trig_tmr6_desc
            };

            for (var j = 0; j < 15; j++)
                if (cb.Name.Contains(keys[j]) && vals[j] != null && vals[j].Length > 1)
                {
                    cb.Items.Add(vals[j]);
                    result = true;
                }
            return result;
        }

        /// <summary>
        ///     Метод обработки галки выбора программы или области
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chb_hold_CheckedChanged(object sender, EventArgs e)
        {
            if (_optClick == 1) _optChange = 1;
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
                foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                    if (cb.Name.Contains("tmr") && !cb.Name.Contains("trig"))
                        cb.Visible = true;

                //foreach (ComboBox cb in this.pan_main.Controls.OfType<ComboBox>())
                //{
                //    if (cb.Name.Contains("tmr") && !cb.Name.Contains("trig")) cb.Visible = true;
                //    cb_tmr_SelectedIndexChanged(cb, null);
                //}

                //if (cb_key1.FindString("Shift+LM") > 1) 
                cb_key_del();
            }
            else
            {
                _handle = IntPtr.Zero;
                pan_proc.Visible = false;
                pan_prog.Visible = true;
                _d3Proc = false;

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

                NumericUpDown[] nudTmr = {nud_tmr1, nud_tmr2, nud_tmr3, nud_tmr4, nud_tmr5, nud_tmr6}; //11.09.2015

                for (var i = 0; i < 6; i++)
                {
                    nud_Leave(nudTmr[i], null);
                    nudTmr[i].Enabled = true;
                    HoldKey[i] = 0;
                    CdrKey[i] = 0;
                }

                foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                    if (cb.Name.Contains("tmr") && !cb.Name.Contains("trig"))
                    {
                        cb.SelectedIndex = 0;
                        cb.Visible = false;
                    }
                //if (cb_key1.FindString("Shift+LM") < 1) 
                cb_key_add();

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
                lb_prof.Text = _lng.LbProfSave;
                b_save.Visible = true;
                b_load.Visible = true;
                cb_hot_prof.SelectedIndex = -1;
                cb_hot_prof.Enabled = false;
            }
            else
            {
                cb_prof.Visible = true;
                lb_prof.Text = _lng.LbProf;
                b_save.Visible = false;
                b_load.Visible = false;
                cb_hot_prof.Enabled = true;
            }
            cb_op_SelectionChangeCommitted(null, null);
        }

        /// <summary>
        ///     Метод перевод мс в секунды и отображения этой информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_Leave(object sender, EventArgs e)
        {
            Label lb = null;
            var nud = (NumericUpDown) sender;
            var nudName = nud.Name;

            var set = new List<string> {"nud_tmr1", "nud_tmr2", "nud_tmr3", "nud_tmr4", "nud_tmr5", "nud_tmr6"};
            var curr = set.IndexOf(nudName);
            //NumericUpDown [] num = new NumericUpDown [] { nud_tmr1, nud_tmr2, nud_tmr3, nud_tmr4, nud_tmr5, nud_tmr6 };
            decimal[] setNum =
            {
                Settings.Default.nud_tmr1, Settings.Default.nud_tmr2, Settings.Default.nud_tmr3,
                Settings.Default.nud_tmr4, Settings.Default.nud_tmr5, Settings.Default.nud_tmr6
            };

            if (curr > -1 && nud.Value != setNum[curr]) _optChange = 1; //14.09.2015

            if (nud.Text == "") nud.Value = nud.Minimum; //Минимум, не 0 - 23.01.2017
            switch (nudName)
            {
                case "nud_key_delay_ms":
                    lb = lb_key_delay_desc;
                    cb_op_SelectionChangeCommitted(null, null);
                    break;
                case "nud_rand":
                    lb = lb_nud_rand;
                    cb_op_SelectionChangeCommitted(null, null);
                    break;
                case "nud_coold":
                    lb = lb_nud_coold;
                    _cooldDelay = Convert.ToDouble(nud.Value); //_cooldDelay
                    cb_op_SelectionChangeCommitted(null, null);
                    break;
                case "nud_tmr1":
                    lb = lb_tmr1_sec;
                    break;
                case "nud_tmr2":
                    lb = lb_tmr2_sec;
                    break;
                case "nud_tmr3":
                    lb = lb_tmr3_sec;
                    break;
                case "nud_tmr4":
                    lb = lb_tmr4_sec;
                    break;
                case "nud_tmr5":
                    lb = lb_tmr5_sec;
                    break;
                case "nud_tmr6":
                    lb = lb_tmr6_sec;
                    break;

                case "nud_trig_delay":
                    lb = lb_trig_delay_ms;
                    break;
                case "nud_trig_time":
                    lb = lb_trig_time_ms;
                    break;
            }

            if (lb != null)
                if (nud.Value > 0)
                    lb.Text = Math.Round(nud.Value / 1000, 2) + @" " + _lng.LangSec;
                else
                    lb.Text = _lng.LbTmrSec;
            error_select();
        }

        private void cb_hot_prof_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_hot_prof.SelectedIndex <= 0) return;
            for (var i = 1; i < 4; i++)
            {
                NativeMethods.UnregisterHotKey(Handle, i);
                NativeMethods.RegisterHotKey(Handle, i, (int) KeyModifier.None,
                    FKeys[cb_hot_prof.SelectedIndex - 2 + i]);
            }
        }

        private void cb_hot_prof_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_optClick == 1) _optChange = 1;
            error_not(1);
        }

        private void tb_prof_name_Click()
        {
            //tb_prof_name.ReadOnly = !tb_prof_name.ReadOnly;
            tb_prof_name.Focus();
            tb_prof_name.Font = new Font(tb_prof_name.Font, FontStyle.Regular);
            if (tb_prof_name.Text == @"Наименование профиля" || tb_prof_name.Text == @"Profile name")
                tb_prof_name.Text = "";
        }

        /// <summary>
        ///     Метод сохранения наименования профиля.
        /// </summary>
        private void tb_prof_name_save()
        {
            string[] profiles =
            {
                Settings.Default.profile1, Settings.Default.profile2, Settings.Default.profile3,
                Settings.Default.profile4, Settings.Default.profile5, Settings.Default.profile6
            };

            //lb_lang_name.Focus();
            if (tb_prof_name.Text == "")
            {
                tb_prof_name.Text = Settings.Default.tb_prof_name;
            }
            else
                //if (tb_prof_name.Text != Settings.Default.profile1 && 
                //    tb_prof_name.Text != Settings.Default.profile2 && 
                //    tb_prof_name.Text != Settings.Default.profile3)
            if (!profiles.Contains(tb_prof_name.Text))
            {
                //Save_settings(0);

                var j = 0;
                var profName = tb_prof_name.Text;
                _lng.Lang_eng();
                if (_lng.TbProfName == tb_prof_name.Text) j++;
                _lng.Lang_rus();
                if (_lng.TbProfName == tb_prof_name.Text) j++;
                if (profName.Length > 15) profName = profName.Substring(0, 15);

                if (j == 0)
                {
                    Settings.Default.tb_prof_name = tb_prof_name.Text;
                    switch (cb_prof.SelectedIndex)
                    {
                        case 1:
                            Settings.Default.profile1 = profName;
                            break;
                        case 2:
                            Settings.Default.profile2 = profName;
                            break;
                        case 3:
                            Settings.Default.profile3 = profName;
                            break;
                        case 4:
                            Settings.Default.profile4 = profName;
                            break;
                        case 5:
                            Settings.Default.profile5 = profName;
                            break;
                        case 6:
                            Settings.Default.profile6 = profName;
                            break;
                        case 7:
                            Settings.Default.profile7 = profName;
                            break;
                        case 8:
                            Settings.Default.profile8 = profName;
                            break;
                        case 9:
                            Settings.Default.profile9 = profName;
                            break;
                    }
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
            if (e.KeyCode != Keys.Return || !_profNameChanged) return;
            lb_lang_name.Focus();
            tb_prof_name_save();
            _profNameChanged = false;
        }

        private void tb_prof_name_Leave(object sender, EventArgs e)
        {
            if (!_profNameChanged) return;
            tb_prof_name_save();
            _profNameChanged = false;
        }

        private void tb_prof_name_MouseHover(object sender, EventArgs e)
        {
            tb_prof_name_Click();
        }

        private void tb_prof_name_TextChanged(object sender, EventArgs e)
        {
            if (_formShown) _profNameChanged = true;
        }

        private void d3hot_Shown(object sender, EventArgs e)
        {
            _formShown = true;
            //b_opt.Text = "Загрузка: " + delay_watch.ElapsedMilliseconds.ToString() + " мсек.";
        }

        private void cb_mapdelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mapDelay = 0;
            if (cb_mapdelay.SelectedIndex > 0)
                _mapDelay = Convert.ToInt32(cb_mapdelay.Items[cb_mapdelay.SelectedIndex]);
        }

        private void cb_mapdelay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_optClick == 1) _optChange = 1;
        }

        private void cb_returndelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            _returnDelay = 0;
            if (cb_returndelay.SelectedIndex > 0)
                _returnDelay = Convert.ToInt32(cb_returndelay.Items[cb_returndelay.SelectedIndex]);
        }

        private void cb_map_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mapKey = "";
            if (cb_map.SelectedIndex > 0 && (string) cb_map.Items[cb_map.SelectedIndex] != "") //24.04.2015
                _mapKey = cb_map.Items[cb_map.SelectedIndex].ToString();
            if (_mapKey.Length > 0 && _mapKey.Substring(0, 1) == "*") _mapKey = _mapKey.Remove(0, 1);
        }

        private void cb_users_CheckedChanged(object sender, EventArgs e)
        {
            if (_optClick == 1) _optChange = 1;
            Person(chb_users.Checked ? 1 : 0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Exit();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cb_start.Checked = !cb_start.Checked;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Items[0].Text = cb_start.Checked ? @"Stop" : @"Start";
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

        //private void cb_key_trig_MouseHover(object sender, EventArgs e)
        //{
        //    ((ComboBox)sender).DroppedDown = true;
        //}

        //private void cb_key_trig_MouseLeave(object sender, EventArgs e)
        //{
        //    ((ComboBox)sender).DroppedDown = false;
        //}


        private void cb_trig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _optChange = 1; //14.09.2015
            var cb = (ComboBox) sender;
            var cbName = ((ComboBox) sender).Name;

            string[] trigNames =
            {
                "cb_trig_tmr1", "cb_trig_tmr2", "cb_trig_tmr3", "cb_trig_tmr4", "cb_trig_tmr5",
                "cb_trig_tmr6"
            }; //09.09.2015
            CheckBox[] trigCheck = {chb_trig1, chb_trig2, chb_trig3, chb_trig4, chb_trig5, chb_trig6}; //09.09.2015

            var i = Array.IndexOf(trigNames, cbName); //09.09.2015
            if (i > -1 && cb.SelectedIndex == 1) //09.09.2015
                trigCheck[i].Checked = true; //09.09.2015
            else if (i > -1) //09.09.2015
                trigCheck[i].Checked = false; //09.09.2015

            if (cb.SelectedIndex == cb.FindString(_lng.CbKeysChoose))
                form_open(cb);

            error_hotkeys(cb, (string) cb.SelectedItem); //15.09.2015
            error_select();
        }

        private void key_choose_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _optChange = 1; //14.09.2015
            var cb = (ComboBox) sender;

            if (cb == null) return;
            if (cb.SelectedIndex == cb.FindString(_lng.CbKeysChoose))
                //11.01.2017 Анализ PVS Studio - использование без проверки на null
                form_open(cb);

            error_hotkeys(cb, (string) cb.SelectedItem);
            if (cb.Name != "cb_key_delay" && cb.Name != "cb_tp" && cb.Name != "cb_map")
                error_select();
        }

        private void key_choose_MouseClick(object sender, MouseEventArgs e)
        {
            cb_trig_tmr_MouseLeave(null, null); //09.12.25015
            var cb = (ComboBox) sender;
            if (e.Button != MouseButtons.Right) return;
            form_open(cb);
            error_select();
        }

        private void d3hot_Deactivate(object sender, EventArgs e)
        {
            //if (this.WindowState != FormWindowState.Minimized) mouseKeyEventProvider2.Enabled = true; //deactivated = true; //30.06.2015
            if (cb_start.Checked) return;
            NativeMethods.UnregisterHotKey(Handle, 1);
            NativeMethods.UnregisterHotKey(Handle, 2);
            NativeMethods.UnregisterHotKey(Handle, 3);
        }

        private void d3hot_Activated(object sender, EventArgs e)
        {
            //mouseKeyEventProvider2.Enabled = false; //deactivated = false; //30.06.2015
            d3hot_KeyUp(null);
            if (cb_hot_prof.SelectedIndex > 0)
                cb_hot_prof_SelectedIndexChanged(null, null);
        }

        private void lb_ver_check_Click(object sender, EventArgs e)
        {
            var send = (Label) sender;
            _verClick = "";
            if (send != null) _verClick = send.Name;

            _tmrVer = new Timer();
            try
            {
                _tmrVer.AutoReset = false;
                _tmrVer.Interval = 30;
                _tmrVer.Elapsed += _tmrVer_Tick;
                _tmrVer.Start();
            }
            catch
            {
                _tmrVer.Dispose();
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
        private void _tmrVer_Tick(object sender, EventArgs eventArgs)
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

            var reqVer = WebRequest.Create(@"https://github.com/DmitryOlenin/D3Hot");
            WebResponse resp1 = null;

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

            var verOk = true;

            try
            {
                resp1 = (HttpWebResponse) reqVer.GetResponse(); //req_ver.GetResponse();
            }
            catch
            {
                verOk = false;
            }

            if (resp1 != null && verOk)
                using (var stream1 = resp1.GetResponseStream())
                {
                    if (stream1 != null)
                        using (var sr1 = new StreamReader(stream1))
                        {
                            var version = sr1.ReadToEnd();
                            var parsVer = version.Split('\n'); //парсим строку и получаем стринговый массив

                            foreach (var t in parsVer)
                                if (t.Contains("Diablo3 Hotkeys"))
                                {
                                    var vers =
                                        t.Substring(
                                                t.IndexOf("title=\"Diablo3 Hotkeys", StringComparison.Ordinal) + 23, 3)
                                            .Trim();
                                    double newVer;
                                    try
                                    {
                                        newVer = Convert.ToDouble(vers.Replace(".", _sep));
                                    }
                                    catch
                                    {
                                        MessageBox.Show(_lng.VerErrNover, _lng.VerCap, MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                                        break;
                                    }
                                    if (vers != string.Format("{0:F1}", Ver).Trim().Replace(_sep, ".") && newVer > Ver)
                                        if (_verClick == "lb_ver_check")
                                        {
                                            if (MessageBox.Show(_lng.Download, _lng.NewVer + vers,
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1,
                                                    (MessageBoxOptions) 0x40000 //0x40000 is the "MB_TOPMOST"-Flag.
                                                ) == DialogResult.Yes)
                                                Process.Start("http://bit.ly/d3hotkeys");
                                        }
                                        //else if (MessageBox.Show(new Form(),
                                        //             //Пустая форма, чтобы не было ничего в панели задач
                                        //             _lng.Download, _lng.NewVer + vers, MessageBoxButtons.YesNo,
                                        //             MessageBoxIcon.Asterisk
                                        //         ) == DialogResult.Yes)
                                        else
                                        {
                                            using (var dummy = new Form())
                                                //Пустая форма, чтобы не было ничего в панели задач
                                            {
                                                if (MessageBox.Show(dummy,
                                                        _lng.Download, _lng.NewVer + vers, MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Asterisk
                                                    ) == DialogResult.Yes)
                                                    Process.Start("http://bit.ly/d3hotkeys");
                                            }
                                        }
                                    else if (_verClick == "lb_ver_check")
                                        MessageBox.Show(_lng.NoNew, _lng.VerCap, MessageBoxButtons.OK,
                                            MessageBoxIcon.Asterisk);
                                    //MessageBox.Show(vers);
                                    break;
                                }
                        }
                }
            else
                MessageBox.Show(_lng.VerErrOpen + @"https://github.com/DmitryOlenin/D3Hot", _lng.VerCap,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (resp1 != null) resp1.Close();
            ((Timer) sender).Dispose();
        }

        private void d3hot_KeyUp(KeyEventArgs e)
        {
            //1: Shift, 2: Scroll, 3: Caps, 4: Num
            if (e != null)
            {
                switch (e.KeyData)
                {
                    case Keys.Scroll:
                        if (key_press(2) && !mouseKeyEventProvider2.Enabled ||
                            mouseKeyEventProvider2.Enabled && !key_press(2)) lb_scroll.Image = Resources.ind_lock;
                        else lb_scroll.Image = null;
                        break;
                    case Keys.CapsLock:
                        if (key_press(3) && !mouseKeyEventProvider2.Enabled ||
                            mouseKeyEventProvider2.Enabled && !key_press(3)) lb_caps.Image = Resources.ind_lock;
                        else lb_caps.Image = null;
                        break;
                    case Keys.NumLock:
                        if (key_press(4) && !mouseKeyEventProvider2.Enabled ||
                            mouseKeyEventProvider2.Enabled && !key_press(4)) lb_num.Image = Resources.ind_lock;
                        else lb_num.Image = null;
                        break;
                }
            }
            else
            {
                lb_scroll.Image = key_press(2) ? Resources.ind_lock : null;
                lb_caps.Image = key_press(3) ? Resources.ind_lock : null;
                lb_num.Image = key_press(4) ? Resources.ind_lock : null;
            }
        }

        private void lb_locks_Click(object sender, EventArgs e)
        {
            var lb = (Label) sender;
            switch (lb.Name)
            {
                case "lb_scroll":
                    _inp.Keyboard.KeyPress((VirtualKeyCode) Keys.Scroll);
                    break;
                case "lb_caps":
                    _inp.Keyboard.KeyPress((VirtualKeyCode) Keys.CapsLock);
                    break;
                case "lb_num":
                    _inp.Keyboard.KeyPress((VirtualKeyCode) Keys.NumLock);
                    break;
            }
        }

        private void mouseKeyEventProvider2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Scroll || e.KeyData == Keys.CapsLock || e.KeyData == Keys.NumLock)
                d3hot_KeyUp(e);
        }

        /// <summary>
        ///     Остановка и сброс интервала Stopwatch
        /// </summary>
        /// <param name="watch"></param>
        private static void StopWatch(Stopwatch watch)
        {
            if (watch != null) watch.Reset();
        }

        /// <summary>
        ///     Запуск нового Stopwatch
        /// </summary>
        /// <param name="watch"></param>
        private static void RestartWatch(out Stopwatch watch) //ref
        {
            watch = Stopwatch.StartNew();
        }


        private void d3hot_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("cb_trig")) cb.SelectedIndex = 0;

            foreach (var chb in pan_main.Controls.OfType<CheckBox>())
                if (chb.Name.Contains("chb_trig")) chb.Checked = false;

            error_select();
        }


        private void cb_tmr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _optChange = 1; //14.09.2015
        }

        private void cb_tmr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chb_hold.Checked || !_langLoad) return;
            var cb = (ComboBox) sender;
            //var name = cb.Name;

            ComboBox[] cbTmr = {cb_tmr1, cb_tmr2, cb_tmr3, cb_tmr4, cb_tmr5, cb_tmr6}; //15.07.2015
            //ComboBox[] cbKey = {cb_key1, cb_key2, cb_key3, cb_key4, cb_key5, cb_key6}; //28.08.2015
            NumericUpDown[] nudTmr = {nud_tmr1, nud_tmr2, nud_tmr3, nud_tmr4, nud_tmr5, nud_tmr6}; //15.07.2015
            Label[] lbTmrSec = {lb_tmr1_sec, lb_tmr2_sec, lb_tmr3_sec, lb_tmr4_sec, lb_tmr5_sec, lb_tmr6_sec};
            //15.07.2015

            var num = -1;
            for (var i = 0; i < 6; i++)
                if (cbTmr[i].Name == cb.Name) num = i;

            if (num <= -1) return;
            lbTmrSec[num].ForeColor = Color.Black;

            //check_only();
            if (_resolution) //Разрешение 16:10, или 16:9, или 5:4, или 4:3
            {
                nudTmr[num].Enabled = false;
                CdrKey[num] = 0;
                switch (cb.SelectedIndex)
                {
                    case 1:
                        CdrKey[num] = 1;
                        lbTmrSec[num].Text = _lng.CbTmrCdr;

                        //11.11.2015
                        //if (form_shown && !loading)
                        //{
                        //    if (num == 4) cb_key[num].SelectedItem = "LMouse"; //28.08.2015
                        //    else if (num == 5) cb_key[num].SelectedItem = "RMouse"; //28.08.2015
                        //    else cb_key[num].SelectedIndex = num + 1; //28.08.2015
                        //}

                        break;
                    case 2:
                        CdrKey[num] = 1;
                        lbTmrSec[num].ForeColor = Color.Brown;
                        nud_Leave(nudTmr[num], null);
                        nudTmr[num].Enabled = true;
                        break;
                    case 3:
                        lbTmrSec[num].Text = _lng.CbTmrHold;
                        break;
                    default:
                        nud_Leave(nudTmr[num], null);
                        nudTmr[num].Enabled = true;
                        break;
                }


                if (cb.SelectedIndex == 3)
                {
                    cdr_del(cb);
                    Array.Clear(HoldKey, 0, 6);
                    HoldKey[num] = 1;
                }
                else
                {
                    HoldKey[num] = 0;
                    if (!HoldKey.Any(item => item == 1)) hold_add();
                }
            }
            else //Разрешение не 16:10, или 16:9, или 5:4, или 4:3
            {
                //MessageBox.Show(cb_tmr[1].Name + " / " + cb_tmr[2].Name + " Num:" + num.ToString());

                nudTmr[num].Enabled = false;
                switch (cb.SelectedIndex)
                {
                    case 1:
                        lbTmrSec[num].Text = _lng.CbTmrHold;
                        break;
                    default:
                        nud_Leave(nudTmr[num], null);
                        nudTmr[num].Enabled = true;
                        break;
                }

                if (cb.SelectedIndex == 1)
                {
                    cdr_del(cb);
                    Array.Clear(HoldKey, 0, 6);
                    HoldKey[num] = 1;
                }
                else
                {
                    HoldKey[num] = 0;
                    if (!HoldKey.Any(item => item == 1)) hold_add();
                }
            }

            error_select();
        }

        private void cb_tmr_DrawItem(object sender, DrawItemEventArgs e)
        {
            var cb = (ComboBox) sender;

            var pos = -1;
            if (cb.SelectedIndex > -1) pos = cb.SelectedIndex;


            e.DrawBackground();
            var myBrush = Brushes.Black;
            var ft = cb.Font;
            e.Graphics.DrawString(cb.Items[e.Index].ToString(), ft, myBrush, e.Bounds, StringFormat.GenericDefault);

            // Draw the focus rectangle if the mouse hovers over an item.
            if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
                e.DrawFocusRectangle();

            cb.SelectedIndex = pos;
        }

        //public void refreshList(List<string> list, ComboBox cb)
        //{
        //    cb.DataSource = null;
        //    cb.DataSource = list;
        //}

        //private void cdr_only()
        //{
        //    tmr_holding = false;
        //    foreach (var cb in pan_main.Controls.OfType<ComboBox>())
        //        if (cb.Name.Contains("cb_tmr") &&
        //            (resolution && cb.SelectedIndex == 2 || !resolution && cb.SelectedIndex == 1))
        //        {
        //            tmr_holding = true;
        //            break;
        //        }
        //}

        private void cdr_del(ComboBox input)
        {
            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("cb_tmr") && input.Name.Contains("cb_tmr") && cb.Name != input.Name)
                {
                    var sel = !_resolution && cb.SelectedIndex > 1 ? cb.SelectedIndex - 1 : cb.SelectedIndex;

                    if (_resolution && cb.Items.Count > 3) //16.11.2015
                    {
                        cb.Items.Remove(cb.Items[3]); //16.11.2015
                    }
                    else if (!_resolution && cb.Items.Count > 1)
                    {
                        cb.Items.Remove(cb.Items[1]);
                        //cb.Items.Remove(cb.Items[2]); //16.11.2015
                        sel = 0;
                    }

                    if (cb.SelectedIndex != sel) cb.SelectedIndex = sel;
                }
        }

        private void hold_add()
        {
            var res = true;

            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("cb_tmr") && (cb.SelectedIndex > 2 || !_resolution && cb.SelectedIndex > 0))
                    //16.11.2015
                    res = false;

            if (!res) return;

            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("cb_tmr") &&
                    (_resolution && cb.Items.Count < 4 || !_resolution && cb.Items.Count < 2)) //16.11.2015
                    cb.Items.Add(_lng.CbTmrHold);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //cdr_run[0] = 1;
        //    //screen_capt_pre();
        //    //screen_capt_prereq(cdr_run);
        //    //get_picture(cdr_key);
        //    //cdr_key_check((int)nud_tmr6.Value);
        //    //get_pic();
        //    //int[] check_res = check_pic(cdr_run);

        //    //if (check_res[0] == 0) bmp.Save("test_12.jpg");

        //    cdr_run[0] = 1;
        //    cdr_run[1] = 1;
        //    cdr_run[2] = 1;
        //    test = 0; test1 = 0;

        //    System.Timers.Timer test_timer = new System.Timers.Timer();
        //    test_timer.Interval = 1;
        //    test_timer.Elapsed += test_timer_Tick;
        //    //test_timer.AutoReset = false;

        //    sw = new Stopwatch();
        //    sw.Start();
        //    test_timer.Start();

        //}

        //public void test_timer_Tick(object sender, EventArgs eventArgs)
        //{
        //    var timer = (System.Timers.Timer)sender;

        //    //lock (valueLocker)
        //    //    if (cdr_isrun)
        //    //        return;
        //    //    else
        //    //        cdr_isrun = true;


        //    //if (Monitor.TryEnter(valueLocker, 20))
        //    //{
        //    //    try
        //    //    {
        //    //lock (valueLocker)
        //    //{
        //            //var timer = (System.Timers.Timer)sender;
        //            //test++;
        //            //ScreenCapture(cdr_run);
        //            //if (test == 1000)
        //            //{
        //            //    //timer.Stop();
        //            //    MessageBox.Show(sw.ElapsedMilliseconds.ToString() + " / " + test1.ToString());
        //            //    sw.Reset();
        //            //}
        //    //}
        //    //    }
        //    //    finally { Monitor.Exit(valueLocker); }
        //    //}

        //        //lock (valueLocker)
        //        //    cdr_isrun = false;

        //    if (Monitor.TryEnter(valueLocker, TimeSpan.FromMilliseconds(2000)))
        //    {
        //        try
        //        {
        //            test++;

        //            if (test > 10)
        //            {
        //                timer.Stop();
        //                MessageBox.Show("Прошло: " + sw.ElapsedMilliseconds.ToString() + " Посчитали: " + test1.ToString());
        //                sw.Reset();
        //            }

        //            for (int i = 0; i < 10; i++)
        //            {
        //                test1 += 10;
        //                //Thread.Sleep(2);

        //                //ProcessStartInfo psi = new ProcessStartInfo(); //Имя запускаемого приложения
        //                //psi.FileName = "cmd"; //команда, которую надо выполнить       
        //                //psi.Arguments = @"/c ping -n 1 127.0.0.1"; //c - после выполнения команды консоль закроется, k - нет
        //                //psi.WindowStyle = ProcessWindowStyle.Hidden;
        //                //Process.Start(psi);

        //            }
        //        }
        //        finally { Monitor.Exit(valueLocker); }
        //    }
        //    else
        //    {
        //        test++;
        //    }

        //}

        private void button2_Click(object sender, EventArgs e)
        {
            //ScreenCapTeleport(CdrRun);

            //string path = Path.GetDirectoryName(Application.ExecutablePath);
            //string path = @"https://dl.dropboxusercontent.com/u/14539335/Diablo/Help/d3h.chm";
            ////"file://" + Path.Combine(path, "d3h.chm");
            //Help.ShowHelp(this, path);


            //sw = new Stopwatch();
            ////Bitmap bmp = null;
            //sw.Start();
            ////MemoryStream ms = new MemoryStream();

            ////ms = PrintWindow();
            ////if (ms != null && ms.Length > 0)

            ////cdr_key[0] = 1;
            ////screen_capt_prereq(cdr_run);
            //cdr_run[0] = 0; cdr_run[1] = 0; cdr_run[2] = 1;
            //trig[0] = cb_trig_tmr1.SelectedIndex;
            //trig[1] = cb_trig_tmr1.SelectedIndex;
            //trig[2] = cb_trig_tmr1.SelectedIndex;
            //test = 0; test1 = 0;
            //tmr = new System.Timers.Timer[6];
            //tmr_cdr = new System.Timers.Timer();
            //tmr_cdr.Elapsed += tmr_cdr_Elapsed;
            //tmr_cdr.AutoReset = false;
            //key_codes(1); key_codes(2); key_codes(3);
            //screen_capt_pre();


            //System.Timers.Timer test_timer = new System.Timers.Timer();
            //test_timer.Interval = 15;
            //test_timer.Elapsed += Cooldown_Tick;
            //test_timer.Start();

            ////screen_capt_pre();

            ////for (int i = 0; i < 10000; i++)
            ////{

            ////    using (Bitmap bmp = ScreenShot())//.Save("test444.jpg"); 
            ////    {
            ////        ScreenFind(cdr_run, bmp);
            ////    }

            ////    //ScreenCapture (cdr_run);


            ////}
            ////sw.Stop();
            ////MessageBox.Show(sw.ElapsedMilliseconds.ToString());
            //////MessageBox.Show(test_times);
            ////sw.Reset();
        }

        private void Cooldown_Tick(object sender, EventArgs eventArgs)
        {
            if (_tmrCdrRunning || !_progStart || !_progRun || CdrRun.Sum() == 0)
                return; //Выходим, если нечего прожимать или уже проверяем

            try
            {
                _tmrCdrRunning = true; //Отмечаем, что проверка работает
                _cdrPress = ScreenCapture(CdrRun);
            }
            catch
            {
                // ignored
            }
            finally
            {
                _tmrCdrRunning = false;
            }
        }

        private void tmr_cdr_destroy(int flag) //21.07.2015 //Убиваем таймеры скриншотов и прожатия кулдауна
        {
            if (_tmrCdrDisposing) return;
            _tmrCdrDisposing = true;

            if (flag > 5)
            {
                _teleportInProgress = false; //31.05.2016
                Array.Clear(CdrRun, 0, 6);
                Array.Clear(_cdrPress, 0, 6); //09.12.2015
                Array.Clear(TmrPress, 0, 6); //Отмечаем, что таймеры не прожимаются в данный момент
                _tmrCdrRunning = false; //Отмечаем, что сейчас не идёт проверка на CDR
                if (_tmrCdr != null) // && _tmrCdr.Enabled
                {
                    _tmrCdr.Stop();
                    _tmrCdr.Dispose(); //31.05.2016
                    _tmrCdr = null; //31.05.2016

                    //if (_debug)
                    //{
                    //    var timenow = DateTime.Now.ToLongTimeString();
                    //    MessageBox.Show(timenow + @" Кто-то хочет закрыть таймер CDR из tmr_cdr_destroy");
                    //}
                }

                if (_cdrWatch != null) //31.05.2016
                {
                    for (var i = 0; i < 6; i++)
                    {
                        StopWatch(_cdrWatch[i]);
                        _cdrWatch[i] = null;
                    }
                    _cdrWatch = null;
                }

                for (var i = 0; i < 6; i++) //Удаляем информацию о запущенных таймерах CDR
                    if (TmrF[i] == 1 && CdrKey[i] == 1 && _tmr[i] != null)
                    {
                        _tmr[i].Dispose();
                        _tmr[i] = null;
                    }
            }
            else
            {
                if (CdrKey[flag] == 1 && _tmr[flag] != null)
                {
                    _tmr[flag].Dispose();
                    _tmr[flag] = null;

                    if (_cdrWatch != null && _cdrWatch[flag] != null)
                        _cdrWatch[flag] = null;

                    CdrRun[flag] = 0; //Помечаем, что этот триггер пока не готов к прожатию
                    _cdrPress[flag] = 0;
                    TmrPress[flag] = 0; //Помечаем, что кнопка в данный момент не нажимается
                }
            }


            _tmrCdrDisposing = false;
        }

        private void pan_main_Paint(object sender, PaintEventArgs e)
        {
        }

        private void cb_trig_tmr3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private static int[] some_trig_press()
        {
            int[] result = {0, 0, 0, 0, 0, 0};

            for (var i = 0; i < 6; i++)
                if (TmrF[i] == 1 && key_press(Trig[i])) //Если триггер активен и нажат
                    result[i] = 1;

            return result;
        }

        private void lb_help_Click(object sender, EventArgs e)
        {
            Process.Start("http://d3h.droppages.com");
        }

        private void cb_trig_tmr_MouseHover(object sender, EventArgs e)
        {
            var tmr = (ComboBox) sender;

            ComboBox[] cbTrig = {cb_trig_tmr1, cb_trig_tmr2, cb_trig_tmr3, cb_trig_tmr4, cb_trig_tmr5, cb_trig_tmr6};
            //Label[] lbTrig = {lb_trig1, lb_trig2, lb_trig3, lb_trig4, lb_trig5, lb_trig6};

            for (var i = 0; i < 6; i++)
                if (tmr == cbTrig[i])
                    _currTrig = i;

            _hoverWatch = new Stopwatch();
            _hoverWatch.Start();


            _tmrHover = new Timer();
            try
            {
                _tmrHover.AutoReset = false;
                _tmrHover.Elapsed += tmr_hover_Elapsed;
                _tmrHover.Start();
            }
            catch
            {
                _tmrHover.Dispose();
            }
        }

        private void tmr_hover_Elapsed(object sender, EventArgs e)
        {
            //while (hover_watch.ElapsedMilliseconds < 5000)
            //{
            //}

            if (_hoverWatch == null || _hoverWatch.ElapsedMilliseconds <= 994999) return;
            _hoverWatch.Reset();
            //MessageBox.Show("Мышку зачем так долго держишь над кнопкой?");
            //this.BeginInvoke((Action)(() => gb_set.Location = new Point(6, 57))); 
            trig_settings();
        }

        private void cb_trig_tmr_MouseLeave(object sender, EventArgs e)
        {
            if (_tmrHover != null)
            {
                _tmrHover.Stop();
                _tmrHover.Dispose();
            }
            if (_hoverWatch != null && _hoverWatch.IsRunning)
                _hoverWatch.Reset();

            //gb_set.Location = new Point(500, 47);
        }

        private void trig_settings()
        {
            ComboBox[] cbTrig = {cb_trig_tmr1, cb_trig_tmr2, cb_trig_tmr3, cb_trig_tmr4, cb_trig_tmr5, cb_trig_tmr6};

            BeginInvoke((Action) (() => tt_key.SetToolTip(cbTrig[_currTrig], "")));
            //this.BeginInvoke((Action)(() => gb_set.Location = new Point(6, 57)));
            BeginInvoke((Action) (() => pan_set.Visible = true));

            //this.BeginInvoke((Action)(() => tt_key.Active = false));
            //this.BeginInvoke((Action)(() => gb_set.Text = curr_trig));

            //for (int i = 499; i > 5; i-=2) //Плавно выезжает
            //{
            //    this.BeginInvoke((Action)(() => gb_set.Location = new Point(i, 57)));
            //    Thread.Sleep(2);
            //}
        }

        private void cb_trig_once_CheckedChanged(object sender, EventArgs e)
        {
            if (_currTrig <= -1 || TmrOnce == null) return;
            var res = true;
            TmrOnce[_currTrig] = 0;

            if (cb_trig_once.Checked)
                res = false;

            nud_trig_time.Enabled = res;
            lb_trig_time.Enabled = res;
        }

        private void b_trig_ok_Click(object sender, EventArgs e)
        {
            cb_trig_tmr_MouseLeave(null, null);
            //gb_set.Location = new Point(500, 47);
            pan_set.Visible = false;

            foreach (var cb in pan_main.Controls.OfType<ComboBox>())
                if (cb.Name.Contains("trig"))
                    tt_key.SetToolTip(cb, _lng.TtTrig);

            int[] settCbTrig =
            {
                Settings.Default.chb_trig_once_0, Settings.Default.chb_trig_once_1, Settings.Default.chb_trig_once_2,
                Settings.Default.chb_trig_once_3, Settings.Default.chb_trig_once_4, Settings.Default.chb_trig_once_5
            };

            var currState = cb_trig_once.Checked ? 1 : 0;

            if (settCbTrig[_currTrig] != currState)
            {
                switch (_currTrig)
                {
                    case 0:
                        Settings.Default.chb_trig_once_0 = currState;
                        break;
                    case 1:
                        Settings.Default.chb_trig_once_1 = currState;
                        break;
                    case 2:
                        Settings.Default.chb_trig_once_2 = currState;
                        break;
                    case 3:
                        Settings.Default.chb_trig_once_3 = currState;
                        break;
                    case 4:
                        Settings.Default.chb_trig_once_4 = currState;
                        break;
                    case 5:
                        Settings.Default.chb_trig_once_5 = currState;
                        break;
                }
                Settings.Default.Save();
            }

            //decimal[] sett_nud_time =
            //{
            //    Settings.Default.nud_trig_time_0, Settings.Default.nud_trig_time_1, Settings.Default.nud_trig_time_2,
            //    Settings.Default.nud_trig_time_3, Settings.Default.nud_trig_time_4, Settings.Default.nud_trig_time_5
            //};

            var workTime = nud_trig_time.Value;

            if (settCbTrig[_currTrig] != workTime)
            {
                switch (_currTrig)
                {
                    case 0:
                        Settings.Default.nud_trig_time_0 = workTime;
                        break;
                    case 1:
                        Settings.Default.nud_trig_time_1 = workTime;
                        break;
                    case 2:
                        Settings.Default.nud_trig_time_2 = workTime;
                        break;
                    case 3:
                        Settings.Default.nud_trig_time_3 = workTime;
                        break;
                    case 4:
                        Settings.Default.nud_trig_time_4 = workTime;
                        break;
                    case 5:
                        Settings.Default.nud_trig_time_5 = workTime;
                        break;
                }
                Settings.Default.Save();
            }

            decimal[] settNudDelay =
            {
                Settings.Default.nud_trig_time_0, Settings.Default.nud_trig_time_1, Settings.Default.nud_trig_time_2,
                Settings.Default.nud_trig_time_3, Settings.Default.nud_trig_time_4, Settings.Default.nud_trig_time_5
            };

            var nudDelay = nud_trig_delay.Value;

            if (settNudDelay[_currTrig] == nudDelay) return;
            switch (_currTrig)
            {
                case 0:
                    Settings.Default.nud_trig_delay_0 = nudDelay;
                    break;
                case 1:
                    Settings.Default.nud_trig_delay_1 = nudDelay;
                    break;
                case 2:
                    Settings.Default.nud_trig_delay_2 = nudDelay;
                    break;
                case 3:
                    Settings.Default.nud_trig_delay_3 = nudDelay;
                    break;
                case 4:
                    Settings.Default.nud_trig_delay_4 = nudDelay;
                    break;
                case 5:
                    Settings.Default.nud_trig_delay_5 = nudDelay;
                    break;
            }
            Settings.Default.Save();
        }

        private void gb_set_Move(object sender, EventArgs e)
        {
            cb_trig_once_CheckedChanged(null, null);

            ComboBox[] cbTrig = {cb_trig_tmr1, cb_trig_tmr2, cb_trig_tmr3, cb_trig_tmr4, cb_trig_tmr5, cb_trig_tmr6};
            Label[] lbTrig = {lb_trig1, lb_trig2, lb_trig3, lb_trig4, lb_trig5, lb_trig6};

            if (gb_set.Location == new Point(500, 47))
            {
                for (var i = 0; i < 6; i++)
                {
                    cbTrig[i].Enabled = true;
                    lbTrig[i].Enabled = true;
                    lbTrig[i].Font = new Font(lbTrig[i].Font, FontStyle.Regular);
                }
            }
            else if (gb_set.Location == new Point(6, 57))
            {
                int[] settCbTrig =
                {
                    Settings.Default.chb_trig_once_0, Settings.Default.chb_trig_once_1, Settings.Default.chb_trig_once_2,
                    Settings.Default.chb_trig_once_3, Settings.Default.chb_trig_once_4, Settings.Default.chb_trig_once_5
                };

                cb_trig_once.Checked = settCbTrig[_currTrig] == 1;
                //cb_trig_once.Checked = settCbTrig[_currTrig] == 1 ? true : false;

                gb_set.Text = _lng.TrigSettings + @" № " + (_currTrig + 1);
                cb_trig_enable.Items.Clear();
                cb_trig_enable.Items.Add("");
                for (var i = 0; i < 6; i++)
                {
                    cbTrig[i].Enabled = false;
                    if (_currTrig == i)
                    {
                        lbTrig[i].Font = new Font(lbTrig[i].Font, FontStyle.Bold);
                    }
                    else
                    {
                        lbTrig[i].Enabled = false;
                        cb_trig_enable.Items.Add(lbTrig[i].Text);
                    }
                }
            }
        }

        //private void trb_coold_Scroll(object sender, EventArgs e)
        //{
        //    lb_trb_coold.Text = trb_coold.Value.ToString();
        //}

        private void cb_press_type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Save_settings(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //throw new Exception("2");
            usage_area();
        }

        [Flags]
        ////11.01.2017 Анализ PVS Studio - V3059 Members of the 'KeyModifier' enum are powers of 2. Consider adding '[Flags]' attribute to the enum.
        private enum KeyModifier
        {
            None = 0
            //Alt = 1,
            //Control = 2,
            //Shift = 4,
            //WinKey = 8
        }
    }
}