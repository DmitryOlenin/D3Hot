using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using D3Hot.Properties;
//using NLog;

namespace D3Hot
{

    public class LogFile : IDisposable
    {
        StreamWriter sw;

        public LogFile(string path)
        {
            sw = new StreamWriter(path);
        }

        public void WriteLine(string str)
        {
            sw.WriteLine(str);
            sw.Flush();
        }

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
             // Free any other managed objects here.
                sw.Close();
                sw.Dispose(); 
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
    }

    public partial class d3hot : Form
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        public List<string> log;

        private static IntPtr windowDC = IntPtr.Zero;

        public static int test = 0, test1 = 0, test2 = 0;
        public bool pic_ready = false, cdr_isready = false, cdr_isrun = false;

        public Rectangle rect = Screen.PrimaryScreen.Bounds;
        public Rect rc = new Rect();

        public int height_skills, height_mouse;
        public int[] skill_begin = new int[] { 0,0,0,0,0,0 };
        public int[] skill_end = new int[] { 0, 0, 0, 0, 0, 0 };
        public int[] skill_yellow = new int[] { 0, 0, 0, 0, 0, 0 };
        public int[] cdr_press = new int[] { 0, 0, 0, 0, 0, 0 }; //07.07.2015
        public int[] pic_analyze = new int[] { 0, 0, 0, 0, 0, 0 }; //14.07.2015
        public int coold_modify_y = 0;

        public bool skills_nocheck = true;

        //public Stopwatch test_sw = new Stopwatch(); //07.07.2015
        public Stopwatch sw;// = new Stopwatch();
        public string test_times = "";
        public int test_counter = 0;

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public void screen_capt_pre()
        {
            //mem_stream = new MemoryStream();
            //bmp = new Bitmap(rect.Right, rect.Bottom); //04.07.2015

            if (skill_begin[0] == 0)
            {
                if (aspect_ratio == 169) //09.11.2015 (rect.Width / 16 * 10 != rect.Height)
                {
                    //MessageBox.Show("Начало первого скилла 16:9: " + Convert.ToInt16(rect.Width * 0.33).ToString());
                    skill_begin = new int[] 
                        {Convert.ToInt16(rect.Width * 0.33), 
                        Convert.ToInt16(rect.Width * 0.365), 
                        Convert.ToInt16(rect.Width * 0.4),
                        Convert.ToInt16(rect.Width * 0.435), 
                        Convert.ToInt16(rect.Width * 0.4713), 
                        Convert.ToInt16(rect.Width * 0.5057)};

                    skill_end = new int[] 
                        {Convert.ToInt16(rect.Width * 0.356),
                        Convert.ToInt16(rect.Width * 0.391),
                        Convert.ToInt16(rect.Width * 0.426),
                        Convert.ToInt16(rect.Width * 0.461),
                        Convert.ToInt16(rect.Width * 0.4953),
                        Convert.ToInt16(rect.Width * 0.5297)};
                }
                else if (aspect_ratio == 1610) //1680x1050: (522 -> 572) / (587 -> 637) / (652 -> 702) / (717 -> 767) / (785 -> 830) / (850 -> 895)
                {
                    //MessageBox.Show("Начало первого скилла 16:10: " + Convert.ToInt16(rect.Width * 0.497).ToString());
                    skill_begin = new int[] 
                        {Convert.ToInt16(rect.Width * 0.311), 
                        Convert.ToInt16(rect.Width * 0.349), 
                        Convert.ToInt16(rect.Width * 0.388),
                        Convert.ToInt16(rect.Width * 0.427), 
                        Convert.ToInt16(rect.Width * 0.467), 
                        Convert.ToInt16(rect.Width * 0.506)};

                    skill_end = new int[] 
                        {Convert.ToInt16(rect.Width * 0.340),
                        Convert.ToInt16(rect.Width * 0.379),
                        Convert.ToInt16(rect.Width * 0.418),
                        Convert.ToInt16(rect.Width * 0.456),
                        Convert.ToInt16(rect.Width * 0.494),
                        Convert.ToInt16(rect.Width * 0.532)};
                }
                else if (aspect_ratio == 54)
                {
                    skill_begin = new int[] 
                        {Convert.ToInt16(rect.Width * 0.2578125),
                        Convert.ToInt16(rect.Width * 0.30703125),
                        Convert.ToInt16(rect.Width * 0.35625),
                        Convert.ToInt16(rect.Width * 0.40546875),
                        Convert.ToInt16(rect.Width * 0.45859375),
                        Convert.ToInt16(rect.Width * 0.50703125)};

                    skill_end = new int[] 
                        {Convert.ToInt16(rect.Width * 0.2953125),
                        Convert.ToInt16(rect.Width * 0.34453125),
                        Convert.ToInt16(rect.Width * 0.39375),
                        Convert.ToInt16(rect.Width * 0.44296875),
                        Convert.ToInt16(rect.Width * 0.49375),
                        Convert.ToInt16(rect.Width * 0.5421875)};
                }
                else if (aspect_ratio == 43) //1024x768
                {
                    skill_begin = new int[] 
                        {Convert.ToInt16(rect.Width * 0.2724609375),
                        Convert.ToInt16(rect.Width * 0.3193359375),
                        Convert.ToInt16(rect.Width * 0.365234375),
                        Convert.ToInt16(rect.Width * 0.4111328125),
                        Convert.ToInt16(rect.Width * 0.4609375),
                        Convert.ToInt16(rect.Width * 0.5068359375)};

                    skill_end = new int[] 
                        {Convert.ToInt16(rect.Width * 0.3076171875),
                        Convert.ToInt16(rect.Width * 0.3544921875),
                        Convert.ToInt16(rect.Width * 0.400390625),
                        Convert.ToInt16(rect.Width * 0.4462890625),
                        Convert.ToInt16(rect.Width * 0.4931640625),
                        Convert.ToInt16(rect.Width * 0.5390625)};
                }
            }

            //height_skills = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9287)); //04.07.2015 обрезаем 90% картинки
            //height_mouse = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9297)); //04.07.2015
            //height_skills = Convert.ToInt16((rect.Height * 0.1) - (rect.Height * (1 - 0.9288))); //09.11.2015

            //int he = (int)(Height * 0.1);
            //int he1 = (int)(rect.Height * 0.1);
            //int he2 = (int)(rect.Height * (1 - 0.93));
            //int he3 = Convert.ToInt16(he1 - he2);

            height_mouse = Convert.ToInt16((rect.Height * 0.1) - (rect.Height * (1 - 0.93))); //09.11.2015
            height_skills = height_mouse - 2;
        }

        public void LogWriter(List<string> log, string filename)
        {
            if (log != null && log.Count > 0)
            {
                using (LogFile lf = new LogFile(filename))
                {
                    for (int i = 0; i < log.Count; i++)
                    {
                        lf.WriteLine(log[i]);
                    }
                }
            }
        }

        public void LogMaker(ref List<string> log, int strnum, string logstr)
        {
            if (Settings.Default.chb_log == 1 && log.Count < strnum)
            {
                string timenow = System.DateTime.Now.ToLongTimeString();
                string type = " Info: ";
                log.Add(timenow + type + logstr);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
        public int[] ScreenCapture(int[] cdr_run) //System.Drawing.Bitmap
        {
            int[] result = new int[] { 2, 2, 2, 2, 2, 2 };
            int[] skill = new int[] { 0, 0, 0, 0, 0, 0 };
            int j = height_skills; //27.10.2015
            int l = height_mouse; //27.10.2015

            if (rect.Height == 768 || rect.Height == 1024 || rect.Height == 1527) //1366x768 18.09.2015 //2715х1527 и 3840х2160
            { j--; l--; }

            if (rect.Width == 1024 && rect.Height == 768) 
            {
                j = 20;
                l = 21;
            }

            else if (rect.Width == 1366 && rect.Height == 768)
            {
                j = 21;
                l = 22;
            }

            //j += coold_modify_y;
            //l += coold_modify_y;

            //if (rect.Height == 2160) //09.11.2015
            //{ j++; l++; }

            //IntPtr hwnd = handle;
            pic_ready = false;

            if (rc.Right <= 0) NativeMethods.GetWindowRect(handle, ref rc);
            int Width = rc.Right - rc.Left;
            int Height = rc.Bottom - rc.Top;

            //IntPtr windowDC = GetDC(handle); //Источник, если просто экран, то IntPtr.Zero --16.10.2015
            //test_times = "1: "+ sw.ElapsedMilliseconds.ToString();

            if (Width > 0 && Height > 0 && Width > rect.Width-2 && Height > rect.Height-2)
            {

                //MessageBox.Show("Нашли область для создания снимка экрана!"); //For Test
                //logger.Info("Нашли область для создания снимка экрана!"); //For Test
                //if (log.Count < 1) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Нашли область для создания снимка экрана.");
                LogMaker(ref log, 1, "Нашли область для создания снимка экрана.");


                if (windowDC == IntPtr.Zero) //16.10.2015
                {
                    windowDC = NativeMethods.GetDC(handle); //16.10.2015
                    if (windowDC == IntPtr.Zero)
                        MessageBox.Show("Не удаётся получить область окна приложения");
                }
                else
                {
                    Height = rect.Height; //21.04.2016
                    Width = rect.Width;

                    using (Bitmap pImage = new Bitmap(Width, (int)(Height * 0.1), PixelFormat.Format32bppRgb)) //Height
                    //using (Bitmap pImage = new Bitmap(Width, Height, PixelFormat.Format32bppRgb)) //Height
                    {
                        using (Graphics g = Graphics.FromImage(pImage))
                        {
                            IntPtr hDC = g.GetHdc();
                            //paint control onto graphics using provided options        
                            try
                            {
                                //if (PrintWindow(hwnd, hDC, 0)) pic_ready = true;
                                NativeMethods.BitBlt(hDC, 0, 0, Width, Height, windowDC, 0, (int)(Height * 0.9), 13369376); //0
                                //BitBlt(hDC, 0, 0, Width, Height, windowDC, 0, 0, 13369376); //0
                                //MessageBox.Show("Сделали снимок: " + windowDC.ToString()); //For Test
                                //if (log.Count < 2) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Сделали снимок: " + windowDC.ToString());
                                LogMaker(ref log, 2, "Сделали снимок: " + windowDC.ToString()+".");
                            }
                            finally
                            {
                                g.ReleaseHdc(hDC);
                            }
                        }

                        

                        //ReleaseDC(handle, windowDC);//IntPtr.Zero --16.10.2015

                        //test_times = test_times + " 2: " + sw.ElapsedMilliseconds.ToString();

                        //pImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

                        //bmp = pImage.Clone(new Rectangle(0, 0, pImage.Width, (int)(pImage.Height * 0.1)), pImage.PixelFormat);
                        //bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        //bmp_short.Save("img_1_test.jpg");

                        //using (Bitmap bmp = pImage.Clone(new Rectangle(0, 0, pImage.Width, (int)(pImage.Height * 0.1)), pImage.PixelFormat))
                        using (Bitmap bmp = pImage)
                        {
                            //bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            //LockBitmap lockBitmap = new LockBitmap(bmp);
                            //lockBitmap.LockBits();

                            if (skills_nocheck)
                            {
                                skills_nocheck = false; //Поставить обнуление при старте!

                                for (int i = 0; i < 6; i++)
                                    if (cdr_run[i] == 1 && skill_yellow[i] != 2)
                                    {
                                        if (i > 3) j = l;
                                        int counter = 0;

                                        for (int x = skill_begin[i]; x < skill_end[i]; x += 1)
                                        {
                                            Color pix = bmp.GetPixel(x, j);

                                            if (pix.R > 190) //&& pix.G > 190
                                                counter++;
                                        }

                                        if (counter > 3)
                                            skill_yellow[i] = 1; //yellow //05.05.2016
                                        else
                                            skill_yellow[i] = 2; //gray //05.05.2016

                                        //if (i == 2)
                                        //{
                                        //    MessageBox.Show("Сколько насчитали на номере " + i.ToString() + ": " + counter.ToString());
                                        //    MessageBox.Show("Жёлтый скилл? " + skill_yellow[i].ToString());
                                        //}
                                    }
                            }


                            for (int i = 0; i < 6; i++)
                                if (cdr_run[i] == 1)
                                {
                                    result[i] = 1; //0 - скилл в кулдауне, 1 - можно прожимать
                                    if (i > 3) j = l;
                                    
                                    int counter = 0;
                                    int line = skill_end[i] - skill_begin[i];
                                    int halfline = (int)(line / 2);

                                    bool iscoold = false;

                                    //if (log.Count < 3) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Проверяем скилл № " + (i+1).ToString() + " от " + skill_begin[i].ToString() + " до " + skill_end[i].ToString());
                                    LogMaker(ref log, 3, "Проверяем скилл № " + (i + 1).ToString() + " от " + skill_begin[i].ToString() + " до " + skill_end[i].ToString() + ".");
                                   
                                    for (int x = skill_begin[i]; x < skill_begin[i] + halfline + 1; x += 1) //21.04.2016
                                    {

                                        byte Red = bmp.GetPixel(x, j).R;
                                        byte Green = bmp.GetPixel(x, j).G;
                                        byte Blue = bmp.GetPixel(x, j).B;

                                        //if (Red > 190 && Green > 190)
                                        //    counter++;

                                        if (skill_yellow[i] == 2 && (Red > 130 || Blue < 45)) //05.05.2016
                                            iscoold = true;

                                        else if (skill_yellow[i] == 1 && Blue < 90)//Red > 200 && Green > 200 && Blue < 90)
                                        {
                                            counter++;
                                            if (counter > halfline * 0.05)
                                                iscoold = true;
                                        }
                                        
                                        
                                        //if (bmp.GetPixel(x, j).R > 130 || bmp.GetPixel(x, j).B < 45)
                                        ////if (lockBitmap.GetPixel(i, j).B < 45 || lockBitmap.GetPixel(i, j).R > 130)
                                        //{
                                        //    if (!debug) result[i] = 0;
                                        //    //bmp.Save("temp"+x.ToString()+"-"+j.ToString()+".jpg");
                                        //    //break; 21.04.2016
                                        //}
                                        if (!debug && iscoold)
                                        {
                                            result[i] = 0;
                                            //if (log.Count < 4) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Обнаружено, что скилл № " + (i + 1).ToString() + " в кулдауне.");
                                            LogMaker(ref log, 3, "Обнаружено, что скилл № " + (i + 1).ToString() + " в кулдауне.");
                                            break;
                                        }

                                    }
                                    //if (result[i] == 1) bmp.Save("test.jpg");
                                    //if (i == 4 && result[i] == 1) 
                                    //{
                                    //    bmp.Save("temp" + "-" + j.ToString() +"_"+test_counter.ToString()+".jpg");
                                    //    test_counter++;
                                    //}
                                    //if (log.Count < 4 && result[i] == 1) log.Add(System.DateTime.Now.ToLongTimeString() + " Info: Обнаружено, что скилл № " + (i + 1).ToString() + " можно прожимать.");
                                    LogMaker(ref log, 4, "Обнаружено, что скилл № " + (i + 1).ToString() + " можно прожимать.");
                                }
                            //lockBitmap.UnlockBits();
                        }
                    }
                }
            }
            //return bmp_short;
            
            //test_times = test_times + " 3: " + sw.ElapsedMilliseconds.ToString(); 
            return result;
        }

        public void tmr_pic_Elapsed(object sender, EventArgs e)
        {
            //-------------Super New Version Start-------------

            //if (cdr_run.Any(item => item == 1)) //13.07.2015
            //{
                screen_capt_pre();
                cdr_press = ScreenCapture(cdr_run);
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

            cdr_isready = false;
            //if (!tmr_pic.Enabled) MessageBox.Show("Выключился таймер. Прошло: " + test_sw.ElapsedMilliseconds.ToString() + " мс.");
        }

        public void tmr_cdr_Elapsed(object sender, EventArgs e)
        {
            //MessageBox.Show(cdr_watch.ElapsedMilliseconds.ToString()); //15.07.2015
            //StopWatch(cdr_watch);

            //if (cdr_watch.ElapsedMilliseconds < 2000) MessageBox.Show(cdr_watch.ElapsedMilliseconds.ToString());
            //if (cdr_count > 0 && tmr_all_count > cdr_count + 10)
            //{
                for (int i = 0; i < 6; i++) //прожимаем после предыдущего таймера
                {
                    if (cdr_press[i] == 1 && key_press(trig[i])) timer_cdr_create(i);
                    cdr_press[i] = 0;
                }
                //cdr_count = 0;
                //tmr_all_count = 0;
            //}

            //Array.Clear(cdr_press, 0, 6);
            //MessageBox.Show(cdr_press[5].ToString());

            //tmr_cdr.Stop();
            //tmr_all_count = (int)cdr_watch.ElapsedMilliseconds; 

            cdr_isrun = false;
        }



        /// <summary>
        /// Метод только создания скриншота 21.07,2015
        /// </summary>
        /// <param name="cdr_run"></param>
        /// <returns></returns>
        //public Bitmap ScreenShot() //System.Drawing.Bitmap
        //{
        //    IntPtr hwnd = handle;
        //    pic_ready = false;

        //    if (rc.Right <= 0) NativeMethods.GetWindowRect(hwnd, ref rc);
        //    int Width = rc.Right - rc.Left;
        //    int Height = rc.Bottom - rc.Top;

        //    IntPtr windowDC = NativeMethods.GetDC(handle); //Источник, если просто экран, то IntPtr.Zero
        //    //test_times = "1: "+ sw.ElapsedMilliseconds.ToString();

        //    Bitmap pImage = null;

        //    if (Width > 0 && Height > 0)
        //    {
        //            pImage = new Bitmap(Width, (int)(Height * 0.1), PixelFormat.Format32bppRgb);
        //            using (Graphics g = Graphics.FromImage(pImage))
        //            {
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

        private void cdr_timers_create()
        {
            if (tmr_coold == null)
                tmr_coold = new System.Timers.Timer[6];



        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
        public int[] WarframeCapture(int[] cdr_run) //System.Drawing.Bitmap
        {
            int[] result = new int[] { 2, 2, 2, 2, 2, 2 };
            int[] skill = new int[] { 0, 0, 0, 0, 0, 0 };
            int j = height_skills; //27.10.2015
            int l = height_mouse; //27.10.2015

            if (rect.Height == 768 || rect.Height == 1024 || rect.Height == 1527) //1366x768 18.09.2015 //2715х1527 и 3840х2160
            { j--; l--; }

            if (rect.Width == 1024 && rect.Height == 768)
            {
                j = 20;
                l = 21;
            }

            pic_ready = false;

            if (rc.Right <= 0) NativeMethods.GetWindowRect(handle, ref rc);
            int Width = rc.Right - rc.Left;
            int Height = rc.Bottom - rc.Top;


            if (Width > 0 && Height > 0) //&& Width > rect.Width - 2 && Height > rect.Height - 2
            {

                if (windowDC == IntPtr.Zero) //16.10.2015
                {
                    windowDC = NativeMethods.GetDC(handle); //16.10.2015
                    if (windowDC == IntPtr.Zero)
                        MessageBox.Show("Не удаётся получить область окна приложения");
                }

                using (Bitmap pImage = new Bitmap((int)(Width / 2), Height, PixelFormat.Format24bppRgb)) //Height
                //using (Bitmap pImage = new Bitmap(Width, Height, PixelFormat.Format32bppRgb)) //Height
                {
                    using (Graphics g = Graphics.FromImage(pImage))
                    {
                        IntPtr hDC = g.GetHdc();
                        //paint control onto graphics using provided options        
                        try
                        {
                            NativeMethods.BitBlt(hDC, 0, 0, Width, Height, windowDC, (int)(Width / 2), 0, 13369376); //0
                        }
                        finally
                        {
                            g.ReleaseHdc(hDC);
                        }
                    }

                    //pImage.Save("myfile.jpg", ImageFormat.Jpeg);

                    using (Bitmap bmp = pImage)
                    {

                        for (int k = 0; k < 6; k++)
                            if (cdr_run[k] == 1)
                            {
                                result[k] = 0; //0 - скилл в кулдауне, 1 - можно прожимать

                                List<int> find = new List<int>();
                                int find_count = 0;

                                Rectangle rect_strip = new Rectangle(0, 0, bmp.Width, bmp.Height);

                                BitmapData bData = bmp.LockBits(rect_strip, ImageLockMode.ReadOnly, bmp.PixelFormat);
                                /* GetBitsPerPixel just does a switch on the PixelFormat and returns the number */
                                byte bitsPerPixel = 24;

                                /*the size of the image in bytes */
                                int size = bData.Stride * bData.Height;

                                /*Allocate buffer for image*/
                                byte[] data = new byte[size];

                                /*This overload copies data of /size/ into /data/ from location specified (/Scan0/)*/
                                System.Runtime.InteropServices.Marshal.Copy(bData.Scan0, data, 0, size);

                                int x_now = 0, y_now = 0, x_check = -1;

                                for (int i = 0; i < size; i += bitsPerPixel / 8)
                                {
                                    //double magnitude = 1 / 3d * (data[i] + data[i + 1] + data[i + 2]);
                                    //data[i] is the first of 3 bytes of color

                                    if (data[i + 2] > 120 && data[i + 2] < 180 && data[i + 1] > 80 && data[i + 1] < 110 && data[i] < 30)
                                    {


                                        x_now = (int)Math.Ceiling((decimal)(i + 2) / 3 / bmp.Width) - 1;
                                        y_now = (i - (x_now * bmp.Width * 3)) / 3;

                                        if (x_now > x_check)
                                        {
                                            x_check = -1;
                                            //find_count = 0;
                                            //find.Clear();
                                        }

                                        find.Add(y_now);

                                        if (find.Count > 1 && find[find.Count - 1] - find[find.Count - 2] < 5)
                                        {
                                            x_check = x_now;
                                            find_count++;
                                        }
                                        else
                                            find_count = 0;

                                        if (find_count > 50)
                                        {
                                            result[k] = 1;
                                            break;
                                        }


                                    }
                                }

                                bmp.UnlockBits(bData);
                            }
                        
                        //for (int i = 0; i < 6; i++)
                        //    if (cdr_run[i] == 1)
                        //    {
                        //        result[i] = 0; //0 - скилл в кулдауне, 1 - можно прожимать
                        //        if (i > 3) j = l;
                        //        for (int y = 0; y < Height; y++) // 380 480
                        //        {
                        //            find_count = 0;
                        //            find.Clear();

                        //            for (int x = 0; x < Width / 2; x++)
                        //            {
                        //                Color pix = bmp.GetPixel(x, y);

                        //                byte color_r = pix.R;
                        //                byte color_g = pix.G;
                        //                byte color_b = pix.B;


                        //                if (color_r > 120 && color_r < 180 && color_g > 80 && color_g < 110 && color_b < 30)
                        //                {
                        //                    find.Add(x);
                        //                    if (find.Count > 1 && find[find.Count - 1] - find[find.Count - 2] < 5)
                        //                        find_count++;
                        //                    else
                        //                        find_count = 0;
                        //                }

                        //                if (find.Count > 50 && find_count > 50)
                        //                {
                        //                    result[i] = 1;
                        //                    break;
                        //                }

                        //            }

                        //            if (find_count > 50)
                        //                break;
                        //        }
                        //    }
                        //lockBitmap.UnlockBits();
                    }
                }
            }
            //return bmp_short;

            //test_times = test_times + " 3: " + sw.ElapsedMilliseconds.ToString(); 
            return result;
        }

    }
}