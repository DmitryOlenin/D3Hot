using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using WindowsInput.Native;
using D3Hot.Properties;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace D3Hot
{
    public partial class d3hot : Form
    {
        public int test = 0;
        public Bitmap bmp;
        public MemoryStream mem_stream; //03.07.2015
        public bool pic_ready = false, cdr_isready = false, cdr_isrun = false;

        public Rectangle rect = Screen.PrimaryScreen.Bounds;
        public Rect rc = new Rect();
        public Bitmap[] bmpScreen = new Bitmap[6]; 
        public MemoryStream[] ms = new MemoryStream[6];
        public int[] skill_begin = new int[] { 0,0,0,0,0,0 };
        public int[] skill_end = new int[] { 0, 0, 0, 0, 0, 0 };
        public int[] cdr_press = new int[] { 0, 0, 0, 0, 0, 0 }; //07.07.2015
        public int[] pic_analyze = new int[] { 0, 0, 0, 0, 0, 0 }; //14.07.2015

        //public Stopwatch test_sw = new Stopwatch(); //07.07.2015
        public Stopwatch sw;// = new Stopwatch();
        public string test_times = "";
        public int test_counter = 0;


        // Win32 API calls necessary to support screen capture
        //[DllImport("gdi32", EntryPoint = "BitBlt", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //private static extern int BitBlt(int hDestDC, int x, int y, int nWidth, int nHeight, int hSrcDC, int xSrc,
        //                                 int ySrc, int dwRop);

        //[DllImport("user32", EntryPoint = "GetDC", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //private static extern int GetDC(int hwnd);

        //[DllImport("user32", EntryPoint = "ReleaseDC", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //private static extern int ReleaseDC(int hwnd, int hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, Int32 dwRop);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        internal static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        public void screen_capt_prereq(int[] arr) //int[] arr
        {
            rect = Screen.PrimaryScreen.Bounds;
            //bmp = new Bitmap(rect.Right, rect.Bottom);
            //mem_stream = new MemoryStream();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 1)
                {
                    bmpScreen[i] = new Bitmap(rect.Right, rect.Bottom);
                    ms[i] = new MemoryStream();
                }
            }
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

        public void get_picture(int[] arr)
        {
            if (rect.Width / 16 * 9 == rect.Height)
            {
                //MemoryStream ms = new MemoryStream();
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] == 1)
                        {
                            ms[i] = PrintWindow();
                            if (ms != null && ms.Length > 0)
                            {
                                try
                                {
                                    bmpScreen[i] = (Bitmap)Image.FromStream(ms[i]); //CaptureImage2(0, 0);
                                    pic_get = true;
                                }
                                catch { }
                            }
                        }
                    }
                }
                //ms.Dispose();
            }
        }

        public void get_pic()
        {
            if (rect.Height == 1056) rect.Height = 1050;
            //MessageBox.Show("Разрешение в картинке:" + rect.Width.ToString() + "x" + rect.Height.ToString());
            //if (rect.Width / 16 * 9 == rect.Height)
            //{
                try
                {
                    mem_stream = PrintWindow(); //03.07.2015
                    if (pic_ready)
                        bmp = (Bitmap)Image.FromStream(mem_stream);
                }
                catch
                {
                }
            //}
        }

        public Bitmap get_pic_crop() //04.07.2015
        {
            IntPtr hwnd = handle;
            Bitmap result = null;

            if (rect.Height == 1056) rect.Height = 1050;

            if (hwnd != null)
            {
                pic_ready = false;
                Rect rc = new Rect();
                GetWindowRect(hwnd, ref rc);


                if ((rc.Top + rc.Bottom + rc.Left + rc.Right) > 0 && rc.Right - rc.Left > 0 && rc.Bottom - rc.Top > 0)
                {
                    using (bmp = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top, PixelFormat.Format32bppRgb))
                    {
                        using (Graphics gfxBmp = Graphics.FromImage(bmp))
                        {
                            IntPtr hdcBitmap = gfxBmp.GetHdc();
                            try
                            {
                                if (PrintWindow(hwnd, hdcBitmap, 0)) pic_ready = true;
                            }
                            finally
                            {
                                gfxBmp.ReleaseHdc(hdcBitmap);
                            }
                        }

                        //Обрезаем картинку до узкой полоски. Экономия ~14% времени
                        if (pic_ready)
                        {
                            bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            result = bmp.Clone(new Rectangle(0, 0, bmp.Width, (int)(bmp.Height * 0.1)), bmp.PixelFormat);
                            result.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        }
                            //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //bitmap2.Dispose();
                    }
                }
            }
            return result;
        }


        public int[] check_pic(int[] cdr_run)
        {
            int[] result = new int[] { 2,2,2,2,2,2 };
            int[] skill = new int[] { 0, 0, 0, 0, 0, 0 };
            //int start = 0, finish = 0;

            //if (rect.Width / 16 * 9 == rect.Height)
            //{

                int j = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9287)); //04.07.2015 обрезаем 90% картинки
                int l = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9297)); //04.07.2015 обрезаем 90% картинки

                //if (rect.Height < 1200) 
                //{
                  //  MessageBox.Show("Разрешение:" + rect.Width.ToString() + "x" + rect.Height.ToString() + ", высота полоски: " + j.ToString());
                  //  MessageBox.Show("Высота картинки: " + Convert.ToInt16(rect.Height * 0.1).ToString() + "Расположение скилла от конца: " + Convert.ToInt16(rect.Height * (1 - 0.9287)).ToString());
                  //string filename =  Convert.ToInt16(rect.Height * 0.1).ToString() + "_" + Convert.ToInt16(rect.Height * (1 - 0.9287)).ToString() + ".jpg";
                  //bmp.Save("c:\\" + filename);
                //}

                //int j = Convert.ToInt16(rect.Height * 0.9287); //1003 пикселя высота полоски для 4 скиллов
                //int l = Convert.ToInt16(rect.Height * 0.9306); //1005 пикселя (первые 2 скилла)

                for (int i = 0; i < 6; i++)
                {
                    if (cdr_run[i] == 1 && bmp != null)
                    {
                        if (i > 3) j = l;

                        //switch (i)
                        //{
                        //    case 0:
                        //        start = Convert.ToInt16(rect.Width * 0.33);
                        //        finish = Convert.ToInt16(rect.Width * 0.356);
                        //        break;
                        //    case 1:
                        //        start = Convert.ToInt16(rect.Width * 0.365);
                        //        finish = Convert.ToInt16(rect.Width * 0.391);
                        //        break;
                        //    case 2:
                        //        start = Convert.ToInt16(rect.Width * 0.4);
                        //        finish = Convert.ToInt16(rect.Width * 0.426);
                        //        break;
                        //    case 3:
                        //        start = Convert.ToInt16(rect.Width * 0.435);
                        //        finish = Convert.ToInt16(rect.Width * 0.461);
                        //        break;
                        //    case 4:
                        //        start = Convert.ToInt16(rect.Width * 0.4713);
                        //        finish = Convert.ToInt16(rect.Width * 0.4953);
                        //        j = l;
                        //        break;
                        //    case 5:
                        //        start = Convert.ToInt16(rect.Width * 0.5057);
                        //        finish = Convert.ToInt16(rect.Width * 0.5297);
                        //        j = l;
                        //        break;
                        //}


                        for (int x = skill_begin[i]; x < skill_end[i]; x += 1)
                        {
                            if (find_pic(bmp, x, j))
                            {
                                //if (debug) MessageBox.Show("Скилл " + i.ToString() + " не активен");
                                result[i] = 0;
                                break;
                            }
                            else result[i] = 1; //0 - скилл в кулдауне, 1 - можно прожимать
                        }

                    }
                }
            //}
            return result;
        }

        private bool cdr_key_check(int k)
        {
            bool result = false;

            //int WidthScreen = Screen.PrimaryScreen.Bounds.Width;//Screen.PrimaryScreen.WorkingArea.X;
            //int HeightScreen = Screen.PrimaryScreen.Bounds.Height;//Screen.PrimaryScreen.WorkingArea.Y;

            if (rect.Width / 16 * 9 == rect.Height) //Соотношение сторон экрана 16:9
            {
                //MessageBox.Show(rect.Width.ToString() + " x " + rect.Height.ToString()); //1920x1080

                //using (Bitmap bmpScreen = new Bitmap(r.Right, r.Bottom))
                //{
                //const int SRCCOPY = 13369376;

                //using (Graphics g = Graphics.FromImage(bmpScreen))
                //{
                //    // Get a device context to the windows desktop and our destination  bitmaps
                //    int hdcSrc = GetDC(0);
                //    IntPtr hdcDest = g.GetHdc();

                //    // Copy what is on the desktop to the bitmap
                //    BitBlt(hdcDest.ToInt32(), 0, 0, r.Right, r.Bottom, hdcSrc, 0, 0, SRCCOPY);

                //    // Release device contexts
                //    g.ReleaseHdc(hdcDest);
                //    ReleaseDC(0, hdcSrc);
                //}

                //Rectangle r = Screen.PrimaryScreen.Bounds;
                //Bitmap bmpScreen = new Bitmap(r.Right, r.Bottom);
                //MemoryStream ms = new MemoryStream();

                //ms = PrintWindow();
                //if (ms != null && ms.Length > 0)
                //    bmpScreen = (Bitmap)Image.FromStream(ms); //CaptureImage2(0, 0);

                //LockBitmap lockBitmap = new LockBitmap(bmpScreen);
                //lockBitmap.LockBits();

                //Color compareClr = Color.FromArgb(255, 255, 255, 255);
                //for (int y = 0; y < lockBitmap.Height; y++)
                //{
                //    for (int x = 0; x < lockBitmap.Width; x++)
                //    {
                //        if (lockBitmap.GetPixel(x, y) == compareClr)
                //        {
                //            MessageBox.Show("!23");
                //        }
                //    }
                //}

                //lockBitmap.UnlockBits();


                //Bitmap bmpScreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                //Graphics g = Graphics.FromImage(bmpScreen);
                //g.CopyFromScreen(0, 0, 0, 0, bmpScreen.Size);

                //Color border = Color.FromArgb(93, 93, 77); //рамка 105/106/86

                //MessageBox.Show(bmpScreen.GetPixel(700, 917).ToString()); //103/106/88
                //от 600 до 1000 по ширине.
                //по высоте на тест от 900 до 930

                //rgb(42, 56, 108); rgb(45, 61, 112); - синий над скиллами.

                bool found = false, d3 = false;
                //int x_found = 0, y_found = 0;
                int skill1 = 0, skill2 = 0, skill3 = 0, skill4 = 0, skill5 = 0, skill6 = 0, life_mana = 20;

                //for (int j = Convert.ToInt16(rect.Height * 0.83); j < Convert.ToInt16(rect.Height * 0.85); j += 1) //Левая нижняя четверть экрана
                int j = Convert.ToInt16(rect.Height * 0.9287); //1003 пикселя высота полоски для 4 скиллов
                int l = Convert.ToInt16(rect.Height * 0.9306); //1005 пикселя (первые 2 скилла)
                int f = Convert.ToInt16(rect.Height * 0.9722); //1050 пикселя из 1080 (полоска возле банок)

                //for (int i = Convert.ToInt16(rect.Width * 0.265); i < Convert.ToInt16(rect.Width * 0.315); i += 1) //100 пикселей банка здоровья
                //    if (bmpScreen.GetPixel(i, j).R > 100 && bmpScreen.GetPixel(i, j).G < 40 && bmpScreen.GetPixel(i, j).B < 40) life_mana++;

                //for (int i = Convert.ToInt16(rect.Width * 0.68); i < Convert.ToInt16(rect.Width * 0.73); i += 1) //100 пикселей банка маны
                //    if (bmpScreen.GetPixel(i, j).B > 100 && bmpScreen.GetPixel(i, j).R < 40 && bmpScreen.GetPixel(i, j).G < 40) life_mana++;

                //for (int i = Convert.ToInt16(rect.Width * 0.3125); i < Convert.ToInt16(rect.Width * 0.3229); i += 1) //20 пикселей около банки здоровья 600->
                //    if (bmpScreen.GetPixel(i, f).R < 18 &&
                //        bmpScreen.GetPixel(i, f).G < 90 && bmpScreen.GetPixel(i, f).G > 40 &&
                //        bmpScreen.GetPixel(i, f).B < 90 && bmpScreen.GetPixel(i, f).B > 40) life_mana++;
                ////MessageBox.Show("x: " + i.ToString() + ", y: " + f.ToString() + " Цвет" +
                ////    " R: " + bmpScreen.GetPixel(i, f).R.ToString() + " G: " + bmpScreen.GetPixel(i, f).G.ToString() + " B: " + bmpScreen.GetPixel(i, f).B.ToString());

                //for (int i = Convert.ToInt16(rect.Width * 0.6771); i < Convert.ToInt16(rect.Width * 0.6875); i += 1) //20 пикселей около банки маны 1300->
                //    if (bmpScreen.GetPixel(i, f).R < 18 &&
                //        bmpScreen.GetPixel(i, f).G < 90 && bmpScreen.GetPixel(i, f).G > 40 &&
                //        bmpScreen.GetPixel(i, f).B < 90 && bmpScreen.GetPixel(i, f).B > 40) life_mana++;

                if (life_mana > 10) //Есть ли лазурные области возле панелей жизней/маны на экране?
                {

                    switch (k)
                    {
                        case 0:
                            for (int i = Convert.ToInt16(rect.Width * 0.33); i < Convert.ToInt16(rect.Width * 0.356); i += 1) //50 пикселей
                            {
                                if (bmpScreen[0] != null && find_pic(bmpScreen[0], i, j)) skill1++;
                            }
                            if (skill1 > 0)
                            {
                                found = true;
                                if (test == 1) MessageBox.Show("Скилл 1 не активен");
                            }
                            break;
                        case 1:
                            for (int i = Convert.ToInt16(rect.Width * 0.365); i < Convert.ToInt16(rect.Width * 0.391); i += 1) //701 -> 751
                            {
                                if (bmpScreen[1] != null && find_pic(bmpScreen[1], i, j)) skill2++;
                            }
                            if (skill2 > 0)
                            {
                                found = true;
                                if (test == 1) MessageBox.Show("Скилл 2 не активен");
                            }
                            break;
                        case 2:
                            for (int i = Convert.ToInt16(rect.Width * 0.4); i < Convert.ToInt16(rect.Width * 0.426); i += 1)
                            {
                                if (bmpScreen[2] != null && find_pic(bmpScreen[2], i, j)) skill3++;
                            }
                            if (skill3 > 0)
                            {
                                found = true;
                                if (test == 1) MessageBox.Show("Скилл 3 не активен");
                            }
                            break;
                        case 3:
                            for (int i = Convert.ToInt16(rect.Width * 0.435); i < Convert.ToInt16(rect.Width * 0.461); i += 1)
                            {
                                if (bmpScreen[3] != null && find_pic(bmpScreen[3], i, j)) skill4++;
                            }
                            if (skill4 > 0)
                            {
                                found = true;
                                if (test == 1) MessageBox.Show("Скилл 4 не активен");
                            }
                            break;
                        case 4:
                            for (int i = Convert.ToInt16(rect.Width * 0.4713); i < Convert.ToInt16(rect.Width * 0.4953); i += 1)
                            {
                                if (bmpScreen[4] != null && find_pic(bmpScreen[4], i, l)) skill5++;
                            }
                            if (skill5 > 0)
                            {
                                found = true;
                                if (test == 1) MessageBox.Show("Скилл 5 не активен");
                            }
                            break;
                        case 5:
                            for (int i = Convert.ToInt16(rect.Width * 0.5057); i < Convert.ToInt16(rect.Width * 0.5297); i += 1)
                            {
                                if (bmpScreen[5] != null && find_pic(bmpScreen[5], i, l)) skill6++;
                            }
                            if (skill6 > 0)
                            {
                                found = true;
                                if (test == 1) MessageBox.Show("Скилл 6 не активен");
                            }
                            break;
                    }

                    if (!found) result = true;
                    //MessageBox.Show("Не нашли");

                }

                //bmpScreen.Dispose();
                //g.Dispose();
                //}
            }
            return result;
        }


        private bool find_pic(Bitmap bmpScreen, int i, int j)
        {
            bool result = false;
            //if ((bmpScreen.GetPixel(i, j).R > 70 && bmpScreen.GetPixel(i, j).R < 130) &&
            //    (bmpScreen.GetPixel(i, j).G > 70 && bmpScreen.GetPixel(i, j).G < 130) &&
            //    (bmpScreen.GetPixel(i, j).B > 50 && bmpScreen.GetPixel(i, j).B < 100))
            //    result = true;

            try
            {
                if (bmpScreen.GetPixel(i, j).B < 45 || bmpScreen.GetPixel(i, j).R > 130) result = true; //скилл в откате
            }
            catch { }            
            
            //if ( (i == 724 && j == 1003) || (i == 725 && j == 1003) || (i == 726 && j == 1003) )
            //MessageBox.Show(" R: " + bmpScreen.GetPixel(i, j).R.ToString() + " G: " + bmpScreen.GetPixel(i, j).G.ToString() + " B: " + bmpScreen.GetPixel(i, j).B.ToString());
            return result;
        }

        private bool find_pic_lock(Bitmap bmpScreen, int i, int j)
        {
            bool result = false;
            LockBitmap lockBitmap = new LockBitmap(bmpScreen);
            lockBitmap.LockBits();

            if (lockBitmap.GetPixel(i, j).B < 45) result = true;

            lockBitmap.UnlockBits();
            return result;
        }

        public static Bitmap CaptureImage(int x, int y)
        {
            Rectangle r = new Rectangle(0, 0, 100, 100);
            //Rectangle r = Screen.PrimaryScreen.Bounds;

            Bitmap b = new Bitmap(r.Right, r.Bottom);
            //Color c = Color.Black;

            const int SRCCOPY = 13369376;

            using (Graphics g = Graphics.FromImage(b))
            {
                // Get a device context to the windows desktop and our destination  bitmaps
                IntPtr hdcSrc = GetDC((IntPtr)0);
                IntPtr hdcDest = g.GetHdc();

                // Copy what is on the desktop to the bitmap
                BitBlt(hdcDest, 0, 0, r.Right, r.Bottom, hdcSrc, 0, 0, SRCCOPY);

                // Release device contexts
                g.ReleaseHdc(hdcDest);
                ReleaseDC((IntPtr)0, hdcSrc);
            }

            //c = b.GetPixel(0, 0);
            return b;
        }

        private static Bitmap CaptureImage1(int x, int y)
        {
            Bitmap b = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.CopyFromScreen(x, y, 0, 0, new Size(100, 100), CopyPixelOperation.SourceCopy);
                g.DrawLine(Pens.Black, new Point(0, 27), new Point(99, 27));
                g.DrawLine(Pens.Black, new Point(0, 73), new Point(99, 73));
                g.DrawLine(Pens.Black, new Point(52, 0), new Point(52, 99));
                g.DrawLine(Pens.Black, new Point(14, 0), new Point(14, 99));
                g.DrawLine(Pens.Black, new Point(85, 0), new Point(85, 99));
            }
            return b;
        }

        /*Note unsafe keyword*/
        public unsafe Bitmap CaptureImage2(int x, int y)
        {
            //Rectangle r = new Rectangle(0, 0, 1000, 1000);
            Rectangle r = Screen.PrimaryScreen.Bounds;

            Bitmap b = new Bitmap(r.Right, r.Bottom);//note this has several overloads, including a path to an image
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(0, 0, 0, 0, b.Size);
            g.Dispose();

            BitmapData bData = b.LockBits(new Rectangle(0, 0, r.Right, r.Bottom), ImageLockMode.ReadWrite, b.PixelFormat);

            byte bitsPerPixel = (byte)System.Windows.Forms.Screen.PrimaryScreen.BitsPerPixel;


            /*This time we convert the IntPtr to a ptr*/
            byte* scan0 = (byte*)bData.Scan0.ToPointer();

            for (int i = 0; i < bData.Height; ++i)
            {
                for (int j = 0; j < bData.Width; ++j)
                {
                    byte* data = scan0 + i * bData.Stride + j * bitsPerPixel / 8;

                    //data is a pointer to the first byte of the 3-byte color data
                }
            }

            b.UnlockBits(bData);

            return b;
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }
        public MemoryStream PrintWindow() //Bitmap
        {
            MemoryStream ms = new MemoryStream();
            IntPtr hwnd = handle;
            //Bitmap bmp = null;  //29.06.2015
            //using (var proc = Process.GetProcessesByName(prcname)[0])
            //{
            //    hwnd = proc.MainWindowHandle;
            //}
            
            //System.Windows.Rect rc;

            if (hwnd != null)
            {
                pic_ready = false;

                //Rect rc = new Rect();
                //if (rc.Right <= 0)
                    GetWindowRect(hwnd, ref rc);

                //Rectangle rc = Screen.PrimaryScreen.Bounds;

                if ((rc.Top + rc.Bottom + rc.Left + rc.Right) > 0 && rc.Right - rc.Left > 0 && rc.Bottom - rc.Top > 0)
                {
                    //using (Bitmap bmp = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                    using (bmp = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top, PixelFormat.Format32bppRgb))
                    {
                        using (Graphics gfxBmp = Graphics.FromImage(bmp))
                        {
                            IntPtr hdcBitmap = gfxBmp.GetHdc();
                            try
                            {
                                if (PrintWindow(hwnd, hdcBitmap, 0)) pic_ready = true;
                            }
                            finally
                            {
                                gfxBmp.ReleaseHdc(hdcBitmap);
                            }
                        }

                        //Обрезаем картинку до узкой полоски. Экономия ~14% времени
                        //bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        //Bitmap bitmap2 = bmp.Clone(new Rectangle(0, 0, bmp.Width, (int)(bmp.Height * 0.1)), bmp.PixelFormat);
                        //bitmap2.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        //bitmap2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //bitmap2.Dispose();

                        //Bitmap bitmap2 = new Bitmap(bmp.Width, (int)(bmp.Height * 0.2));
                        //using (Graphics g = Graphics.FromImage(bitmap2))
                        //{
                        //    g.DrawImage(bmp, new Rectangle
                        //    (0, 0, bmp.Width, (int)(bmp.Height * 0.2)), 0, 0, bmp.Width, (int)(bmp.Height * 0.2), GraphicsUnit.Pixel);
                        //    g.Save();
                        //}


                        if (pic_ready) //Обрезаем картинку до узкой полоски в 10%. Получаем 4% загрузки CPU вместо 10%.
                        {
                            bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            using (Bitmap bmp_short = bmp.Clone(new Rectangle(0, 0, bmp.Width, (int)(bmp.Height * 0.1)), bmp.PixelFormat))
                            {
                                bmp_short.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                bmp_short.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }


                        //MessageBox.Show(ms.Length.ToString());
                        //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //if (ms.Length > 0) pic_ready = true;
                        
                        //return bmp;
                    }
                }
            }
            //else return null;
            return ms;//bmp;
        }

        public MemoryStream PrintWindow1() //Bitmap
        {
            MemoryStream ms = new MemoryStream();
            IntPtr dc1;
            IntPtr hwnd = handle;

            if (hwnd != null)
            {
                pic_ready = false;
                Rect rc = new Rect();
                GetWindowRect(hwnd, ref rc);
                const int SRCCOPY = 13369376;

                using (bmp = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top, PixelFormat.Format32bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        dc1 = g.GetHdc();
                        try
                        {
                            //BitBlt(dc1, 0, 0, rc.Right - rc.Left, rc.Bottom - rc.Top, hwnd, 0, 0, 13369376);
                            BitBlt(dc1, 0, 0, rc.Right - rc.Left, rc.Bottom - rc.Top, hwnd, 0, 0, SRCCOPY);
                        }
                        finally
                        {
                            g.ReleaseHdc(dc1);
                        }
                    }

                    //bmp.Save(ms, GetEncoderInfo(ImageFormat.Jpeg), JpegParam);
                    //bmp.Save(ms, ImageFormat.Jpeg);

                    bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    using (Bitmap bmp_short = bmp.Clone(new Rectangle(0, 0, bmp.Width, (int)(bmp.Height * 0.1)), bmp.PixelFormat))
                    {
                        bmp_short.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bmp_short.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                }
            }

            return ms;//bmp;
        }

        public int[] PrintWindow2(int[] cdr_run) //System.Drawing.Bitmap
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
                cdr_press = PrintWindow2(cdr_run);
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

        //public void tmr_cdr_press()
        //{
        //    for (int i = 0; i < 6; i++)
        //    {
        //        if (cdr_press[i] == 1)
        //        {
        //            tmr_r[i] = 1;
        //            timer_load(i + 1);
        //        }
        //    }

        //    for (int i = 0; i < 6; i++)
        //    {
        //        if (cdr_press[i] == 1)
        //        {
        //            tmr[i].AutoReset = false;
        //            //tmr_watch[i].Start();
        //            tmr[i].Enabled = true;
        //        }
        //    }

        //    Array.Clear(cdr_press, 0, 6);
        //    cdr_isrun = false;
        //}

    }
}