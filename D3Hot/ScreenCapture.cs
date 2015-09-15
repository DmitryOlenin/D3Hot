using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public static int test = 0, test1 = 0, test2 = 0;
        public bool pic_ready = false, cdr_isready = false, cdr_isrun = false;

        public Rectangle rect = Screen.PrimaryScreen.Bounds;
        public Rect rc = new Rect();

        public int[] skill_begin = new int[] { 0,0,0,0,0,0 };
        public int[] skill_end = new int[] { 0, 0, 0, 0, 0, 0 };
        public int[] cdr_press = new int[] { 0, 0, 0, 0, 0, 0 }; //07.07.2015
        public int[] pic_analyze = new int[] { 0, 0, 0, 0, 0, 0 }; //14.07.2015

        //public Stopwatch test_sw = new Stopwatch(); //07.07.2015
        public Stopwatch sw;// = new Stopwatch();
        public string test_times = "";
        public int test_counter = 0;

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, Int32 dwRop);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        internal static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

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
                if (rect.Width / 16 * 10 != rect.Height)
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
                else //1680x1050: (522 -> 572) / (587 -> 637) / (652 -> 702) / (717 -> 767) / (785 -> 830) / (850 -> 895)
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
            }
        }

        public int[] ScreenCapture(int[] cdr_run) //System.Drawing.Bitmap
        {
            int[] result = new int[] { 2, 2, 2, 2, 2, 2 };
            int[] skill = new int[] { 0, 0, 0, 0, 0, 0 };
            int j = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9287)); //04.07.2015 обрезаем 90% картинки
            int l = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9297)); //04.07.2015 обрезаем 90% картинки

            IntPtr hwnd = handle;
            pic_ready = false;

            if (rc.Right <= 0) GetWindowRect(hwnd, ref rc);
            int Width = rc.Right - rc.Left;
            int Height = rc.Bottom - rc.Top;

            IntPtr windowDC = GetDC(handle); //Источник, если просто экран, то IntPtr.Zero
            //test_times = "1: "+ sw.ElapsedMilliseconds.ToString();

            if (Width > 0 && Height > 0)
            {
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
                            BitBlt(hDC, 0, 0, Width, Height, windowDC, 0, (int)(Height*0.9), 13369376); //0
                            //BitBlt(hDC, 0, 0, Width, Height, windowDC, 0, 0, 13369376); //0
                        }
                        finally
                        {
                            g.ReleaseHdc(hDC);
                        }
                    }

                    ReleaseDC(handle, windowDC);//IntPtr.Zero

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


                        for (int i = 0; i < 6; i++)
                            if (cdr_run[i] == 1)
                            {
                                result[i] = 1; //0 - скилл в кулдауне, 1 - можно прожимать
                                if (i > 3) j = l;
                                for (int x = skill_begin[i]; x < skill_end[i]; x += 1)
                                {
                                    if (bmp.GetPixel(x, j).B < 45 || bmp.GetPixel(x, j).R > 130)
                                    //if (lockBitmap.GetPixel(i, j).B < 45 || lockBitmap.GetPixel(i, j).R > 130)
                                    {
                                        if (!debug) result[i] = 0;
                                        //bmp.Save("temp"+x.ToString()+"-"+j.ToString()+".jpg");
                                        break;
                                    }
                                }
                                //if (result[i] == 1) bmp.Save("test.jpg");
                                //if (i == 4 && result[i] == 1) 
                                //{
                                //    bmp.Save("temp" + "-" + j.ToString() +"_"+test_counter.ToString()+".jpg");
                                //    test_counter++;
                                //}
                            }


                        //lockBitmap.UnlockBits();
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
        public Bitmap ScreenShot() //System.Drawing.Bitmap
        {
            IntPtr hwnd = handle;
            pic_ready = false;

            if (rc.Right <= 0) GetWindowRect(hwnd, ref rc);
            int Width = rc.Right - rc.Left;
            int Height = rc.Bottom - rc.Top;

            IntPtr windowDC = GetDC(handle); //Источник, если просто экран, то IntPtr.Zero
            //test_times = "1: "+ sw.ElapsedMilliseconds.ToString();

            Bitmap pImage = null;

            if (Width > 0 && Height > 0)
            {
                    pImage = new Bitmap(Width, (int)(Height * 0.1), PixelFormat.Format32bppRgb);
                    using (Graphics g = Graphics.FromImage(pImage))
                    {
                        IntPtr hDC = g.GetHdc();       
                        try
                        {
                            BitBlt(hDC, 0, 0, Width, Height, windowDC, 0, (int)(Height * 0.9), 13369376); 
                        }
                        finally
                        {
                            g.ReleaseHdc(hDC);
                        }
                    }

                    ReleaseDC(handle, windowDC);//IntPtr.Zero
            }
            return pImage;
        }

        public int[] ScreenFind(int[] cdr_run, Bitmap bmp)
        {
            int[] result = new int[] { 2, 2, 2, 2, 2, 2 };
            int[] skill = new int[] { 0, 0, 0, 0, 0, 0 };

            int j = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9287)); //04.07.2015 обрезаем 90% картинки
            int l = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9297)); //04.07.2015 обрезаем 90% картинки

            for (int i = 0; i < 6; i++)
                if (cdr_run[i] == 1)
                {
                    result[i] = 1; //0 - скилл в кулдауне, 1 - можно прожимать
                    if (i > 3) j = l;
                    for (int x = skill_begin[i]; x < skill_end[i]; x += 1)
                    {
                        if (bmp.GetPixel(x, j).B < 45 || bmp.GetPixel(x, j).R > 130)
                        //if (lockBitmap.GetPixel(i, j).B < 45 || lockBitmap.GetPixel(i, j).R > 130)
                        {
                            result[i] = 0;
                            //bmp.Save("temp"+x.ToString()+"-"+j.ToString()+".jpg");
                            break;
                        }
                    }
                    //if (result[i] == 1) bmp.Save("test.jpg");
                    //if (i == 4 && result[i] == 1) 
                    //{
                    //    bmp.Save("temp" + "-" + j.ToString() +"_"+test_counter.ToString()+".jpg");
                    //    test_counter++;
                    //}
                }


            return result;
        }

    }
}