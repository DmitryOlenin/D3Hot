using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using D3Hot.Properties;

//using NLog;

namespace D3Hot
{
    public sealed class LogFile : IDisposable
    {
        private readonly StreamWriter _sw;

        // Flag: Has Dispose already been called?
        private bool _disposed; // = false;

        public LogFile(string path)
        {
            _sw = new StreamWriter(path);
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void WriteLine(string str)
        {
            _sw.WriteLine(str);
            _sw.Flush();
        }

        // Protected implementation of Dispose pattern.
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                _sw.Close();
                _sw.Dispose();
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }

        ~LogFile() // the finalizer
        {
            Dispose(false);
        }
    }

    public partial class D3Hotkeys //: Form
    {
        private static IntPtr _windowDc = IntPtr.Zero;

        private readonly int[] _skillYellow = {0, 0, 0, 0, 0, 0}; //07.07.2015

        private int[] _cdrPress = {0, 0, 0, 0, 0, 0}; //07.07.2015 //Массив таймеров готовых для прожатия по кулдауну после проверки картинки

        private int _heightSkills,
            _heightMouse;

        //private static Logger logger = LogManager.GetCurrentClassLogger();
        private List<string> _log;
        private Rect _rc; //= new Rect();

        private Rectangle _rect = Screen.PrimaryScreen.Bounds;

        //public static int test = 0, test1 = 0, test2 = 0;
        //public bool pic_ready = false, cdr_isready = false, cdr_isrun = false, 
        private bool _screenCaptPrepared; // = false;

        private int[] _skillBegin = {0, 0, 0, 0, 0, 0},
            _skillEnd = {0, 0, 0, 0, 0, 0}; //07.07.2015

        //public int[] pic_analyze = new int[] {0, 0, 0, 0, 0, 0}; //14.07.2015
        //public int coold_modify_y = 0;

        private bool _teleportInProgress; //= false, skills_nocheck = true

        private void screen_capt_pre()
        {
            //mem_stream = new MemoryStream();
            //bmp = new Bitmap(rect.Right, rect.Bottom); //04.07.2015

            _screenCaptPrepared = true;

            if (_skillBegin[0] == 0)
                switch (_aspectRatio)
                {
                    case 169:
                        //MessageBox.Show("Начало первого скилла 16:9: " + Convert.ToInt16(rect.Width * 0.33).ToString());
                        _skillBegin = new int[]
                        {
                            Convert.ToInt16(_rect.Width * 0.33),
                            Convert.ToInt16(_rect.Width * 0.365),
                            Convert.ToInt16(_rect.Width * 0.4),
                            Convert.ToInt16(_rect.Width * 0.435),
                            Convert.ToInt16(_rect.Width * 0.4713),
                            Convert.ToInt16(_rect.Width * 0.5057)
                        };

                        _skillEnd = new int[]
                        {
                            Convert.ToInt16(_rect.Width * 0.356),
                            Convert.ToInt16(_rect.Width * 0.391),
                            Convert.ToInt16(_rect.Width * 0.426),
                            Convert.ToInt16(_rect.Width * 0.461),
                            Convert.ToInt16(_rect.Width * 0.4953),
                            Convert.ToInt16(_rect.Width * 0.5297)
                        };
                        break;
                    case 1610:
                        //MessageBox.Show("Начало первого скилла 16:10: " + Convert.ToInt16(rect.Width * 0.497).ToString());
                        _skillBegin = new int[]
                        {
                            Convert.ToInt16(_rect.Width * 0.311),
                            Convert.ToInt16(_rect.Width * 0.349),
                            Convert.ToInt16(_rect.Width * 0.388),
                            Convert.ToInt16(_rect.Width * 0.427),
                            Convert.ToInt16(_rect.Width * 0.467),
                            Convert.ToInt16(_rect.Width * 0.506)
                        };

                        _skillEnd = new int[]
                        {
                            Convert.ToInt16(_rect.Width * 0.340),
                            Convert.ToInt16(_rect.Width * 0.379),
                            Convert.ToInt16(_rect.Width * 0.418),
                            Convert.ToInt16(_rect.Width * 0.456),
                            Convert.ToInt16(_rect.Width * 0.494),
                            Convert.ToInt16(_rect.Width * 0.532)
                        };
                        break;
                    case 54:
                        _skillBegin = new int[]
                        {
                            Convert.ToInt16(_rect.Width * 0.2578125),
                            Convert.ToInt16(_rect.Width * 0.30703125),
                            Convert.ToInt16(_rect.Width * 0.35625),
                            Convert.ToInt16(_rect.Width * 0.40546875),
                            Convert.ToInt16(_rect.Width * 0.45859375),
                            Convert.ToInt16(_rect.Width * 0.50703125)
                        };

                        _skillEnd = new int[]
                        {
                            Convert.ToInt16(_rect.Width * 0.2953125),
                            Convert.ToInt16(_rect.Width * 0.34453125),
                            Convert.ToInt16(_rect.Width * 0.39375),
                            Convert.ToInt16(_rect.Width * 0.44296875),
                            Convert.ToInt16(_rect.Width * 0.49375),
                            Convert.ToInt16(_rect.Width * 0.5421875)
                        };
                        break;
                    case 43:
                        _skillBegin = new int[]
                        {
                            Convert.ToInt16(_rect.Width * 0.2724609375),
                            Convert.ToInt16(_rect.Width * 0.3193359375),
                            Convert.ToInt16(_rect.Width * 0.365234375),
                            Convert.ToInt16(_rect.Width * 0.4111328125),
                            Convert.ToInt16(_rect.Width * 0.4609375),
                            Convert.ToInt16(_rect.Width * 0.5068359375)
                        };

                        _skillEnd = new int[]
                        {
                            Convert.ToInt16(_rect.Width * 0.3076171875),
                            Convert.ToInt16(_rect.Width * 0.3544921875),
                            Convert.ToInt16(_rect.Width * 0.400390625),
                            Convert.ToInt16(_rect.Width * 0.4462890625),
                            Convert.ToInt16(_rect.Width * 0.4931640625),
                            Convert.ToInt16(_rect.Width * 0.5390625)
                        };
                        break;
                }

            //height_skills = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9287)); //04.07.2015 обрезаем 90% картинки
            //height_mouse = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9297)); //04.07.2015
            //height_skills = Convert.ToInt16((rect.Height * 0.1) - (rect.Height * (1 - 0.9288))); //09.11.2015

            //int he = (int)(Height * 0.1);
            //int he1 = (int)(rect.Height * 0.1);
            //int he2 = (int)(rect.Height * (1 - 0.93));
            //int he3 = Convert.ToInt16(he1 - he2);

            _heightMouse = Convert.ToInt16(_rect.Height * 0.1 - _rect.Height * (1 - 0.93)); //09.11.2015
            _heightSkills = _heightMouse - 1; //-2 15.06.2016
        }

        private static void LogWriter(ICollection<string> log, string filename)
        {
            if (log == null || log.Count <= 0) return;
            using (var lf = new LogFile(filename))
            {
                foreach (var t in log)
                    lf.WriteLine(t);
            }
        }

        /// <summary>
        /// Метод для сохранения логов. Номер строки не менее текущего количества строк. Всего не больше 10 000 строк.
        /// </summary>
        /// <param name="log"></param>
        /// <param name="strnum"></param>
        /// <param name="logstr"></param>
        private static void LogMaker(ref List<string> log, int strnum, string logstr)
        {
            if (Settings.Default.chb_log != 1 || log.Count >= strnum || !_logEnabled) return;
            //if (log.Count>10000) log.Clear(); //Очищаем, если более 10 000 строк.
            if (log.Count >= 10000) log.RemoveRange(1, 1000); //Удаляем первую 1000 строк, когда достигаем 10 000.
            var timenow = DateTime.Now.ToLongTimeString();
            const string type = " Info: ";
            log.Add(timenow + type + logstr);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
        private int[] ScreenCapture(int[] cdrRun) //System.Drawing.Bitmap
        {
            int[] result = {2, 2, 2, 2, 2, 2};
            //int[] skill = new int[] { 0, 0, 0, 0, 0, 0 };
            var j = _heightSkills; //27.10.2015
            var l = _heightMouse; //27.10.2015

            if (_rect.Height == 768 || _rect.Height == 1024 || _rect.Height == 1527)
                //1366x768 18.09.2015 //2715х1527 и 3840х2160
            {
                j--;
                l--;
            }

            if (_rect.Width == 1024 && _rect.Height == 768)
            {
                j = 20;
                l = 21;
            }

            else if (_rect.Width == 1366 && _rect.Height == 768)
            {
                j = 21;
                l = 22;
            }

            //j += coold_modify_y;
            //l += coold_modify_y;

            //if (rect.Height == 2160) //09.11.2015
            //{ j++; l++; }

            //IntPtr hwnd = handle;
            //pic_ready = false;

            if (_rc.Right <= 0) NativeMethods.GetWindowRect(_handle, ref _rc);
            var gameWidth = _rc.Right - _rc.Left;
            var gameHeight = _rc.Bottom - _rc.Top;

            //IntPtr windowDC = GetDC(handle); //Источник, если просто экран, то IntPtr.Zero --16.10.2015
            //test_times = "1: "+ sw.ElapsedMilliseconds.ToString();

            if (!_debug &&
                (gameWidth <= 0 || gameHeight <= 0 || gameWidth <= _rect.Width - 2 || gameHeight <= _rect.Height - 2))
                return result;
            //MessageBox.Show("Нашли область для создания снимка экрана!"); //For Test
            //logger.Info("Нашли область для создания снимка экрана!"); //For Test
            //if (log.Count < 1) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Нашли область для создания снимка экрана.");
            //LogMaker(ref log, 1, "Нашли область для создания снимка экрана.");


            if (_windowDc == IntPtr.Zero) //31.05.2016
                _windowDc = NativeMethods.GetDC(_handle); //16.10.2015

            if (_windowDc == IntPtr.Zero)
            {
                MessageBox.Show(@"Не удаётся получить область окна приложения");
            }

            else
            {
                gameHeight = _rect.Height; //21.04.2016
                gameWidth = _rect.Width;

                //PixelFormat.Format64bppArgb
                //Bitmap test = new Bitmap (@"C:\Users\dolenin\Dropbox\Public\Diablo\2560x1440_coold_gold.png");
                //MessageBox.Show(Image.GetPixelFormatSize(test.PixelFormat).ToString());

                using (var pImage = new Bitmap(gameWidth, (int) (gameHeight * 0.1), PixelFormat.Format32bppRgb))
                    //Height //PixelFormat.Format32bppRgb
                    //using (Bitmap pImage = new Bitmap(Width, Height, PixelFormat.Format32bppRgb)) //Height
                {
                    using (var g = Graphics.FromImage(pImage))
                    {
                        var hDc = g.GetHdc();
                        //paint control onto graphics using provided options        
                        try
                        {
                            //if (PrintWindow(hwnd, hDC, 0)) pic_ready = true;
                            NativeMethods.BitBlt(hDc, 0, 0, gameWidth, gameHeight, _windowDc, 0,
                                (int) (gameHeight * 0.9),
                                13369376); //0
                            //BitBlt(hDC, 0, 0, Width, Height, windowDC, 0, 0, 13369376); //0
                            //MessageBox.Show("Сделали снимок: " + windowDC.ToString()); //For Test
                            //if (log.Count < 2) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Сделали снимок: " + windowDC.ToString());
                            //LogMaker(ref log, 2, "Сделали снимок: " + windowDC.ToString()+".");
                        }
                        finally
                        {
                            g.ReleaseHdc(hDc);
                        }
                    }


                    //ReleaseDC(handle, windowDC);//IntPtr.Zero --16.10.2015

                    //test_times = test_times + " 2: " + sw.ElapsedMilliseconds.ToString();

                    //pImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

                    //bmp = pImage.Clone(new Rectangle(0, 0, pImage.Width, (int)(pImage.Height * 0.1)), pImage.PixelFormat);
                    //bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    //bmp_short.Save("img_1_test.jpg");

                    //using (Bitmap bmp = pImage.Clone(new Rectangle(0, 0, pImage.Width, (int)(pImage.Height * 0.1)), pImage.PixelFormat))
                    using (var bmp = pImage)
                    {
                        //bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        //LockBitmap lockBitmap = new LockBitmap(bmp);
                        //lockBitmap.LockBits();

                        //if (skills_nocheck) //Проверять только один раз при каждом старте программы 23.06.2016
                        {
                            var jCheck = j;
                            //skills_nocheck = false; //Поставить обнуление при старте!

                            for (var i = 0; i < 6; i++)
                                //if (cdr_run[i] == 1 && skill_yellow[i] != 2)
                                if (CdrKey[i] == 1 && TmrF[i] == 1 && _skillYellow[i] != 2)
                                    //Триггер с кулдауном и активен (то есть может быть включен) и не помечен как серый
                                {
                                    if (i > 3) jCheck = l;
                                    var counter = 0;

                                    for (var x = _skillBegin[i]; x < _skillEnd[i]; x += 1)
                                    {
                                        var pix = bmp.GetPixel(x, jCheck);

                                        if (pix.R > 190 && pix.G > 190)
                                            //Если без G, то оранжевые считаются золотыми
                                            counter++;
                                    }

                                    if (counter > 3)
                                        _skillYellow[i] = 1; //yellow //05.05.2016
                                    else
                                        _skillYellow[i] = 2; //gray //05.05.2016

                                    //if (i == 2)
                                    //{
                                    //    MessageBox.Show("Сколько насчитали на номере " + i.ToString() + ": " + counter.ToString());
                                    //    MessageBox.Show("Жёлтый скилл? " + skill_yellow[i].ToString());
                                    //}
                                }
                        }

                        //skill_yellow[5] = 2; //gray //test for debug 15.06.2016

                        for (var i = 0; i < 6; i++)
                            if (cdrRun[i] == 1)
                            {
                                var jCheck = j;
                                result[i] = 1; //0 - скилл в кулдауне, 1 - можно прожимать

                                var counter = 0;
                                var line = _skillEnd[i] - _skillBegin[i];
                                var halfline = line / 2;
                                var stoppot = ""; //for test

                                if (i > 3)
                                {
                                    jCheck = l;
                                    halfline -= 1;
                                }

                                //if (log.Count < 3) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Проверяем скилл № " + (i+1).ToString() + " от " + skill_begin[i].ToString() + " до " + skill_end[i].ToString());
                                //LogMaker(ref log, 3, "Проверяем скилл № " + (i + 1).ToString() + " от " + skill_begin[i].ToString() + " до " + skill_end[i].ToString() + ".");

                                for (var x = _skillBegin[i]; x < _skillBegin[i] + halfline + 1; x += 1) //21.04.2016
                                {
                                    var red = bmp.GetPixel(x, jCheck).R;
                                    var green = bmp.GetPixel(x, jCheck).G;
                                    var blue = bmp.GetPixel(x, jCheck).B;

                                    //if (Red > 190 && Green > 190)
                                    //    counter++;

                                    var iscoold = false;
                                    if (_skillYellow[i] == 2 && (red > 130 || blue < 45)) //05.05.2016
                                    {
                                        iscoold = true;
                                    }

                                    else if (_skillYellow[i] == 1 && blue < 90) //Red > 200 && Green > 200 && Blue < 90)
                                    {
                                        counter++;
                                        if (counter > halfline * 0.05)
                                            iscoold = true;
                                    }

                                    stoppot = " Жёлтый? " + _skillYellow[i] + " Координата X: " +
                                              x + ", координата Y: " + jCheck + ", Цвета R: " +
                                              red + ", G: " + green + ", B: " +
                                              blue;
                                    //if (i == 0 && x == (skill_begin[i] + halfline)) bmp.Save("temp" + x.ToString() + "-" + j.ToString() + ".jpg"); //сохраняем при проверке 1 скилла


                                    //if (bmp.GetPixel(x, j).R > 130 || bmp.GetPixel(x, j).B < 45)
                                    ////if (lockBitmap.GetPixel(i, j).B < 45 || lockBitmap.GetPixel(i, j).R > 130)
                                    //{
                                    //    if (!debug) result[i] = 0;
                                    //    //bmp.Save("temp"+x.ToString()+"-"+j.ToString()+".jpg");
                                    //    //break; 21.04.2016
                                    //}
                                    if (_debug && !_debugPic || !iscoold) continue;
                                    result[i] = 0;
                                    //if (log.Count < 4) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Обнаружено, что скилл № " + (i + 1).ToString() + " в кулдауне.");
                                    break;
                                }
                                //if (result[i] == 1) bmp.Save("test.jpg");
                                //if (i == 4 && result[i] == 1) 
                                //{
                                //    bmp.Save("temp" + "-" + j.ToString() +"_"+test_counter.ToString()+".jpg");
                                //    test_counter++;
                                //}
                                //if (log.Count < 4 && result[i] == 1) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Обнаружено, что скилл № " + (i + 1).ToString() + " можно прожимать.");

                                if (_debug && i == 0 && result[i] == 1)
                                {
                                    bmp.Save("work" + "-" + jCheck + "_" + counter + "_" +
                                             halfline + ".jpg");
                                    LogMaker(ref _log, 400,
                                        "Обнаружено, что скилл № " + (i + 1) + " можно прожимать." +
                                        stoppot);
                                }
                                else if (_debug && i == 0 && result[i] == 0)
                                {
                                    bmp.Save("notwork" + "-" + jCheck + "_" + counter + "_" +
                                             halfline + ".jpg");
                                    LogMaker(ref _log, 300,
                                        "Обнаружено, что скилл № " + (i + 1) + " в кулдауне. Насчитали:" +
                                        counter + " из " + halfline + stoppot);
                                }
                            }
                        //lockBitmap.UnlockBits();
                    }
                }

                //Проверяем картинку на телепорт 31.05.2016

                using (
                    var pImage = new Bitmap((int) (gameWidth * 0.02), (int) (gameHeight * 0.01),
                        PixelFormat.Format32bppRgb))
                {
                    using (var g = Graphics.FromImage(pImage))
                    {
                        var hDc = g.GetHdc();
                        try
                        {
                            if (_aspectRatio == 54)
                                NativeMethods.BitBlt(hDc, 0, 0, gameWidth, gameHeight, _windowDc,
                                    (int) (gameWidth * 0.431),
                                    (int) (gameHeight * 0.295), 13369376); //(int)(Width * 0.47)
                            else
                                NativeMethods.BitBlt(hDc, 0, 0, gameWidth, gameHeight, _windowDc,
                                    (int) (gameWidth * 0.45),
                                    (int) (gameHeight * 0.295), 13369376); //(int)(Width * 0.47)
                        }
                        finally
                        {
                            g.ReleaseHdc(hDc);
                        }
                    }

                    //pImage.Save("temp.jpg");
                    //MessageBox.Show("сохранили");

                    var yCenter = pImage.Height / 2;
                    var xStart = pImage.Width / 4; //Начинаем с 1/4 рисунка

                    using (var bmp = pImage)
                    {
                        //j = y_center;
                        var counter = 0;

                        for (var x = xStart; x < xStart * 2; x += 1) //21.04.2016
                        {
                            var red = bmp.GetPixel(x, yCenter).R;
                            var green = bmp.GetPixel(x, yCenter).G;
                            var blue = bmp.GetPixel(x, yCenter).B;

                            //if (Red < 22 && Green > 30 && Blue > 60)

                            if (
                                //(Red < 25 && (Green > 30 && Green < 90) && (Blue > 55 && Blue < 125)) || //тёмная полоска
                                red < 43 &&
                                (
                                    green > 100 && green < 190 && blue > 150 && blue < 230 || //яркая полоска
                                    green > 30 && green < 91 && blue > 55 && blue < 125 //тёмная полоска
                                ) ||
                                red < 60 && green > 70 && green < 120 && blue > 70 && blue < 140
                                //совсем тёмная полоска
                            )
                                counter++;
                        }

                        _teleportInProgress = counter > xStart / 2;

                        //pImage.Save("temp_y-" + y_center.ToString() + "_x-" + x_start.ToString() + "_count-" + counter.ToString() + ".jpg");
                        //MessageBox.Show("сохранили");
                    }
                }

                //Закончили проверять картинку на телепорт 31.05.2016
            }
            //return bmp_short;

            //test_times = test_times + " 3: " + sw.ElapsedMilliseconds.ToString(); 
            return result;
        }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
        //    "CA2202:Не ликвидировать объекты несколько раз")]
        //private int[] ScreenCapTeleport(int[] cdr_run) //System.Drawing.Bitmap
        //{
        //    int[] result = new int[] {2, 2, 2, 2, 2, 2};
        //    int[] skill = new int[] {0, 0, 0, 0, 0, 0};
        //    int j = _heightSkills; //27.10.2015
        //    int l = _heightMouse; //27.10.2015

        //    //pic_ready = false;

        //    if (_rc.Right <= 0) NativeMethods.GetWindowRect(_handle, ref _rc);
        //    int Width = _rc.Right - _rc.Left;
        //    int Height = _rc.Bottom - _rc.Top;

        //    if ((Width > 0 && Height > 0 && Width > _rect.Width - 2 && Height > _rect.Height - 2) || _debug)
        //    {
        //        //LogMaker(ref log, 1, "Нашли область для создания снимка экрана.");


        //        if (_windowDc == IntPtr.Zero) //16.10.2015
        //            _windowDc = NativeMethods.GetDC(_handle); //16.10.2015


        //        if (_windowDc == IntPtr.Zero)
        //            MessageBox.Show("Не удаётся получить область окна приложения");
        //        else
        //        {
        //            Height = _rect.Height; //21.04.2016
        //            Width = _rect.Width;

        //            using (
        //                    Bitmap pImage = new Bitmap((int) (Width * 0.02), (int) (Height * 0.01),
        //                        PixelFormat.Format32bppRgb)) //Height (int)(Width*0.02)
        //                //using (Bitmap pImage = new Bitmap(Width, Height, PixelFormat.Format32bppRgb)) //Height
        //            {
        //                using (Graphics g = Graphics.FromImage(pImage))
        //                {
        //                    IntPtr hDC = g.GetHdc();
        //                    //paint control onto graphics using provided options        
        //                    try
        //                    {
        //                        //NativeMethods.BitBlt(hDC, 0, 0, Width, Height, windowDC, 0, (int)(Height * 0.295), 13369376); //0
        //                        NativeMethods.BitBlt(hDC, 0, 0, Width, Height, _windowDc, (int) (Width * 0.45),
        //                            (int) (Height * 0.295), 13369376); //(int)(Width * 0.47)
        //                    }
        //                    finally
        //                    {
        //                        g.ReleaseHdc(hDC);
        //                    }
        //                }

        //                pImage.Save("temp.jpg");
        //                MessageBox.Show("сохранили");

        //                //int y_center = (int)(((Height * 0.297) - (Height * 0.295)) / 2);
        //                //int x_start = (int)(((Width * 0.47) - (Width * 0.45)) / 3);
        //                int y_center = (int) pImage.Height / 2;
        //                int x_start = (int) pImage.Width / 3;


        //                using (Bitmap bmp = pImage)
        //                {
        //                    j = y_center;
        //                    int counter = 0;

        //                    for (int x = x_start; x < x_start * 2; x += 1) //21.04.2016
        //                    {
        //                        byte Red = bmp.GetPixel(x, j).R;
        //                        byte Green = bmp.GetPixel(x, j).G;
        //                        byte Blue = bmp.GetPixel(x, j).B;

        //                        if (Red < 10 && Green > 30 && Blue > 60)
        //                            counter++;
        //                    }

        //                    if (counter > (int) (x_start * 0.7))
        //                        MessageBox.Show("Есть контакт");
        //                }
        //            }
        //        }
        //    }
        //    //return bmp_short;

        //    //test_times = test_times + " 3: " + sw.ElapsedMilliseconds.ToString(); 
        //    return result;
        //}

        //public void tmr_pic_Elapsed(object sender, EventArgs e)
        //{
        //-------------Super New Version Start-------------

        //if (cdr_run.Any(item => item == 1)) //13.07.2015
        //{
        //screen_capt_pre();
        //_cdrPress = ScreenCapture(CdrRun);
        //}

        //int[] check_res = PrintWindow2(cdr_run);
        //if (cdr_press.Any(item => item == 1)) RestartWatch(ref cdr_watch);

        //for (int i = 0; i < 6; i++)
        //    if (cdr_press[i] != 2) cdr_press[i] = check_res[i];

        //bool press = false;
        //for (int i = 0; i < 6; i++)
        //{
        //    if (cdr_press[i] == 1) press = true;
        //}
        //if (press) cdr_count = tmr_all_count;


        //for (int i = 0; i < 6; i++)
        //{
        //    if (cdr_press[i] == 1) timer_cdr_create(i);
        //}

        //-------------New Version Start-------------
        //screen_capt_pre();

        //get_pic();

        //if (pic_ready) //04.07.2015
        //{
        //    int[] check_res = check_pic(cdr_run);

        //    for (int i = 0; i < 6; i++)
        //    {
        //        if (check_res[i] == 1) timer_cdr_create(i);
        //    }
        //}

        //-------------Old Version Start-------------
        //get_picture(cdr_run);

        //for (int i = 0; i < 6; i++)
        //    if (cdr_run[i] == 1 && cdr_key_check(i))
        //    {
        //        //MessageBox.Show("123");
        //        timer_cdr_create(i);
        //        //bmpScreen[i].Save("img" + i + ".jpg");
        //    }


        //if (debug) bmp.Save("img_test.jpg");
        //mem_stream.Dispose();
        //bmp.Dispose();
        //PrintWindow().Dispose();

        //bmp = null; mem_stream = null; //07.07.2015
        //bmp.Dispose();  //mem_stream.Dispose();

        //tmr_pic.Stop();

        //cdr_isready = false;
        //if (!tmr_pic.Enabled) MessageBox.Show("Выключился таймер. Прошло: " + test_sw.ElapsedMilliseconds.ToString() + " мс.");
        //}

        private void tmr_cdr_Elapsed(object sender, EventArgs e)
        {
            var tmrLocal = (NumericTimer) sender;
            var num = tmrLocal.Number;

            if (TmrPress[num] == 1 || !_progStart || !_progRun) return; //25.01.2017 Не заходим в таймер, пока он работает

             if (_cdrPress[num] != 1) 
             {
                 if (CdrRun[num] > 1) CdrRun[num]--; //Сбрасываем необходимость проверять нажатие скилла до 1 (начиная с 1 идёт проверка)
                 return; //Если не жмём - на выход 23.01.2017
             }

            //if (Monitor.TryEnter(valueLocker, TimeSpan.FromMilliseconds(10))) //55
            //{
            try
            {
                TmrPress[num] = 1; //помечаем, что таймер работает

                //tmrLocal.Stop(); //Останавливаем таймер на время его выполенения
                //MessageBox.Show(@"Включён ли таймер?" + _tmr[num].Enabled);

                //tmr_cdr_curr = i + 1; 23.06.2016

                if (_trigPressed[num] != 1 || CdrRun[num] <= 0 || _teleportInProgress) return;
                var cooldSec = _tmrI[num] > 0 && _cdrWatch != null && _cdrWatch[num] != null &&
                               _cdrWatch[num].IsRunning && _cdrWatch[num].ElapsedMilliseconds < _tmrI[num];

                if (cooldSec) return;
                //if (cdr_press[3] == 1)
                //    LogMaker(ref log, 5, "Прожимаем четвёртый скилл");

                _cdrPress[num] = 2; //Эту кнопку больше нажимать не надо (пока проверка не покажет иного)
                CdrRun[num] = 2;
                //Один раз (3-1=2 сразу, 2-1=1 второй раз) уменьшать счётчик до повторной проверки (60*1 = 60мс)

                if (_tmrI[num] > 0) //Проверка на "Кулдаун + сек."
                {
                    if (_cdrWatch == null)
                        _cdrWatch = new Stopwatch[6];

                    if (_cdrWatch[num] == null)
                        _cdrWatch[num] = new Stopwatch(); //16.11.2015

                    _cdrWatch[num].Reset();
                    _cdrWatch[num].Start(); //15.06.2016
                }

                button_press(KeyH[num], KeyV[num]); //Нажать кнопку
            }
            catch
            {
                // ignored
            }
            finally
            {
                //if (_tmr[tmrLocal.Number] != null) tmrLocal.Start(); //Запускаем таймер снова после его выполнения
                TmrPress[num] = 0; //Помечаем, что таймер работу завершил
            }
            //finally { Monitor.Exit(valueLocker); }
            //}
        }

        //public Stopwatch test_sw = new Stopwatch(); //07.07.2015
        //public Stopwatch sw; // = new Stopwatch();
        //public string test_times = "";
        //public int test_counter = 0;

        public struct Rect
        {
            // ReSharper disable once UnusedMember.Global
            public Rect(int left, int top, int right, int bottom) : this()
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left { get; private set; }
            public int Top { get; private set; }
            public int Right { get; private set; }
            public int Bottom { get; private set; }
        }

        //            {
        //            using (Graphics g = Graphics.FromImage(pImage))
        //            pImage = new Bitmap(Width, (int)(Height * 0.1), PixelFormat.Format32bppRgb);
        //    {

        //    if (Width > 0 && Height > 0)

        //    Bitmap pImage = null;
        //    //test_times = "1: "+ sw.ElapsedMilliseconds.ToString();

        //    IntPtr windowDC = NativeMethods.GetDC(handle); //Источник, если просто экран, то IntPtr.Zero
        //    int Height = rc.Bottom - rc.Top;
        //    int Width = rc.Right - rc.Left;

        //    if (rc.Right <= 0) NativeMethods.GetWindowRect(hwnd, ref rc);
        //    pic_ready = false;
        //    IntPtr hwnd = handle;
        //{
        //public Bitmap ScreenShot() //System.Drawing.Bitmap


        // Метод только создания скриншота 21.07,2015
        //                IntPtr hDC = g.GetHdc();       
        //                try
        //                {
        //                    NativeMethods.BitBlt(hDC, 0, 0, Width, Height, windowDC, 0, (int)(Height * 0.9), 13369376); 
        //                }
        //                finally
        //                {
        //                    g.ReleaseHdc(hDC);
        //                }
        //            }

        //            NativeMethods.ReleaseDC(handle, windowDC);//IntPtr.Zero
        //    }
        //    return pImage;
        //}

        //public int[] ScreenFind(int[] cdr_run, Bitmap bmp)
        //{
        //    int[] result = new int[] { 2, 2, 2, 2, 2, 2 };
        //    int[] skill = new int[] { 0, 0, 0, 0, 0, 0 };

        //    int j = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9287)); //04.07.2015 обрезаем 90% картинки
        //    int l = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9297)); //04.07.2015 обрезаем 90% картинки

        //    for (int i = 0; i < 6; i++)
        //        if (cdr_run[i] == 1)
        //        {
        //            result[i] = 1; //0 - скилл в кулдауне, 1 - можно прожимать
        //            if (i > 3) j = l;
        //            for (int x = skill_begin[i]; x < skill_end[i]; x += 1)
        //            {
        //                if (bmp.GetPixel(x, j).B < 45 || bmp.GetPixel(x, j).R > 130)
        //                //if (lockBitmap.GetPixel(i, j).B < 45 || lockBitmap.GetPixel(i, j).R > 130)
        //                {
        //                    result[i] = 0;
        //                    //bmp.Save("temp"+x.ToString()+"-"+j.ToString()+".jpg");
        //                    break;
        //                }
        //            }
        //            //if (result[i] == 1) bmp.Save("test.jpg");
        //            //if (i == 4 && result[i] == 1) 
        //            //{
        //            //    bmp.Save("temp" + "-" + j.ToString() +"_"+test_counter.ToString()+".jpg");
        //            //    test_counter++;
        //            //}
        //        }

        //    return result;
        //}

        //private void cdr_timers_create()
        //{
        //    if (_tmrCoold == null)
        //        _tmrCoold = new System.Timers.Timer[6];
        //}

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
        //    "CA2202:Не ликвидировать объекты несколько раз")]
        //public int[] WarframeCapture(int[] cdr_run) //System.Drawing.Bitmap
        //{
        //    int[] result = new int[] {2, 2, 2, 2, 2, 2};
        //    int[] skill = new int[] {0, 0, 0, 0, 0, 0};
        //    int j = _heightSkills; //27.10.2015
        //    int l = _heightMouse; //27.10.2015

        //    if (_rect.Height == 768 || _rect.Height == 1024 || _rect.Height == 1527)
        //        //1366x768 18.09.2015 //2715х1527 и 3840х2160
        //    {
        //        j--;
        //        l--;
        //    }

        //    if (_rect.Width == 1024 && _rect.Height == 768)
        //    {
        //        j = 20;
        //        l = 21;
        //    }

        //    //pic_ready = false;

        //    if (_rc.Right <= 0) NativeMethods.GetWindowRect(_handle, ref _rc);
        //    int Width = _rc.Right - _rc.Left;
        //    int Height = _rc.Bottom - _rc.Top;


        //    if (Width > 0 && Height > 0) //&& Width > rect.Width - 2 && Height > rect.Height - 2
        //    {
        //        if (_windowDc == IntPtr.Zero) //16.10.2015
        //        {
        //            _windowDc = NativeMethods.GetDC(_handle); //16.10.2015
        //            if (_windowDc == IntPtr.Zero)
        //                MessageBox.Show("Не удаётся получить область окна приложения");
        //        }

        //        using (Bitmap pImage = new Bitmap((int) (Width / 2), Height, PixelFormat.Format24bppRgb)) //Height
        //            //using (Bitmap pImage = new Bitmap(Width, Height, PixelFormat.Format32bppRgb)) //Height
        //        {
        //            using (Graphics g = Graphics.FromImage(pImage))
        //            {
        //                IntPtr hDC = g.GetHdc();
        //                //paint control onto graphics using provided options        
        //                try
        //                {
        //                    NativeMethods.BitBlt(hDC, 0, 0, Width, Height, _windowDc, (int) (Width / 2), 0, 13369376);
        //                    //0
        //                }
        //                finally
        //                {
        //                    g.ReleaseHdc(hDC);
        //                }
        //            }

        //            //pImage.Save("myfile.jpg", ImageFormat.Jpeg);

        //            using (Bitmap bmp = pImage)
        //            {
        //                for (int k = 0; k < 6; k++)
        //                    if (cdr_run[k] == 1)
        //                    {
        //                        result[k] = 0; //0 - скилл в кулдауне, 1 - можно прожимать

        //                        List<int> find = new List<int>();
        //                        int find_count = 0;

        //                        Rectangle rect_strip = new Rectangle(0, 0, bmp.Width, bmp.Height);

        //                        BitmapData bData = bmp.LockBits(rect_strip, ImageLockMode.ReadOnly, bmp.PixelFormat);
        //                        /* GetBitsPerPixel just does a switch on the PixelFormat and returns the number */
        //                        byte bitsPerPixel = 24;

        //                        /*the size of the image in bytes */
        //                        int size = bData.Stride * bData.Height;

        //                        /*Allocate buffer for image*/
        //                        byte[] data = new byte[size];

        //                        /*This overload copies data of /size/ into /data/ from location specified (/Scan0/)*/
        //                        System.Runtime.InteropServices.Marshal.Copy(bData.Scan0, data, 0, size);

        //                        int x_now = 0, y_now = 0, x_check = -1;

        //                        for (int i = 0; i < size; i += bitsPerPixel / 8)
        //                        {
        //                            //double magnitude = 1 / 3d * (data[i] + data[i + 1] + data[i + 2]);
        //                            //data[i] is the first of 3 bytes of color

        //                            if (data[i + 2] > 120 && data[i + 2] < 180 && data[i + 1] > 80 && data[i + 1] < 110 &&
        //                                data[i] < 30)
        //                            {
        //                                x_now = (int) Math.Ceiling((decimal) (i + 2) / 3 / bmp.Width) - 1;
        //                                y_now = (i - (x_now * bmp.Width * 3)) / 3;

        //                                if (x_now > x_check)
        //                                {
        //                                    x_check = -1;
        //                                    //find_count = 0;
        //                                    //find.Clear();
        //                                }

        //                                find.Add(y_now);

        //                                if (find.Count > 1 && find[find.Count - 1] - find[find.Count - 2] < 5)
        //                                {
        //                                    x_check = x_now;
        //                                    find_count++;
        //                                }
        //                                else
        //                                    find_count = 0;

        //                                if (find_count > 50)
        //                                {
        //                                    result[k] = 1;
        //                                    break;
        //                                }
        //                            }
        //                        }

        //                        bmp.UnlockBits(bData);
        //                    }

        //                //for (int i = 0; i < 6; i++)
        //                //    if (cdr_run[i] == 1)
        //                //    {
        //                //        result[i] = 0; //0 - скилл в кулдауне, 1 - можно прожимать
        //                //        if (i > 3) j = l;
        //                //        for (int y = 0; y < Height; y++) // 380 480
        //                //        {
        //                //            find_count = 0;
        //                //            find.Clear();

        //                //            for (int x = 0; x < Width / 2; x++)
        //                //            {
        //                //                Color pix = bmp.GetPixel(x, y);

        //                //                byte color_r = pix.R;
        //                //                byte color_g = pix.G;
        //                //                byte color_b = pix.B;


        //                //                if (color_r > 120 && color_r < 180 && color_g > 80 && color_g < 110 && color_b < 30)
        //                //                {
        //                //                    find.Add(x);
        //                //                    if (find.Count > 1 && find[find.Count - 1] - find[find.Count - 2] < 5)
        //                //                        find_count++;
        //                //                    else
        //                //                        find_count = 0;
        //                //                }

        //                //                if (find.Count > 50 && find_count > 50)
        //                //                {
        //                //                    result[i] = 1;
        //                //                    break;
        //                //                }

        //                //            }

        //                //            if (find_count > 50)
        //                //                break;
        //                //        }
        //                //    }
        //                //lockBitmap.UnlockBits();
        //            }
        //        }
        //    }
        //    //return bmp_short;

        //    //test_times = test_times + " 3: " + sw.ElapsedMilliseconds.ToString(); 
        //    return result;
        //}
    }
}