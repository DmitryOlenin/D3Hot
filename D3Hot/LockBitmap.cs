//using System.Drawing;
//using System.Drawing.Imaging;
//using System;
//using System.Runtime.InteropServices;
public class LockBitmap
{
    //Bitmap source = null;
    //IntPtr Iptr = IntPtr.Zero;
    //BitmapData bitmapData = null;

    //public byte[] Pixels { get; set; }
    //public int Depth { get; private set; }
    //public int Width { get; private set; }
    //public int Height { get; private set; }

    //public LockBitmap(Bitmap source)
    //{
    //    this.source = source;
    //}

    ///// <summary>
    ///// Lock bitmap data
    ///// </summary>
    //public void LockBits()
    //{
    //    try
    //    {
    //        // Get width and height of bitmap
    //        Width = source.Width;
    //        Height = source.Height;

    //        // get total locked pixels count
    //        int PixelCount = Width * Height;

    //        // Create rectangle to lock
    //        Rectangle rect = new Rectangle(0, 0, Width, Height);

    //        // get source bitmap pixel format size
    //        Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

    //        // Check if bpp (Bits Per Pixel) is 8, 24, or 32
    //        if (Depth != 8 && Depth != 24 && Depth != 32)
    //        {
    //            throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
    //        }

    //        // Lock bitmap and return bitmap data
    //        bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
    //                                     source.PixelFormat);

    //        // create byte array to copy pixel values
    //        int step = Depth / 8;
    //        Pixels = new byte[PixelCount * step];
    //        Iptr = bitmapData.Scan0;

    //        // Copy data from pointer to array
    //        Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ///// <summary>
    ///// Unlock bitmap data
    ///// </summary>
    //public void UnlockBits()
    //{
    //    try
    //    {
    //        // Copy data from byte array to pointer
    //        Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

    //        // Unlock bitmap data
    //        source.UnlockBits(bitmapData);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ///// <summary>
    ///// Get the color of the specified pixel
    ///// </summary>
    ///// <param name="x"></param>
    ///// <param name="y"></param>
    ///// <returns></returns>
    //public Color GetPixel(int x, int y)
    //{
    //    Color clr = Color.Empty;

    //    // Get color components count
    //    int cCount = Depth / 8;

    //    // Get start index of the specified pixel
    //    int i = ((y * Width) + x) * cCount;

    //    if (i > Pixels.Length - cCount)
    //        throw new IndexOutOfRangeException();

    //    if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
    //    {
    //        byte b = Pixels[i];
    //        byte g = Pixels[i + 1];
    //        byte r = Pixels[i + 2];
    //        byte a = Pixels[i + 3]; // a
    //        clr = Color.FromArgb(a, r, g, b);
    //    }
    //    if (Depth == 24) // For 24 bpp get Red, Green and Blue
    //    {
    //        byte b = Pixels[i];
    //        byte g = Pixels[i + 1];
    //        byte r = Pixels[i + 2];
    //        clr = Color.FromArgb(r, g, b);
    //    }
    //    if (Depth == 8)
    //    // For 8 bpp get color value (Red, Green and Blue values are the same)
    //    {
    //        byte c = Pixels[i];
    //        clr = Color.FromArgb(c, c, c);
    //    }
    //    return clr;
    //}

    ///// <summary>
    ///// Set the color of the specified pixel
    ///// </summary>
    ///// <param name="x"></param>
    ///// <param name="y"></param>
    ///// <param name="color"></param>
    //public void SetPixel(int x, int y, Color color)
    //{
    //    // Get color components count
    //    int cCount = Depth / 8;

    //    // Get start index of the specified pixel
    //    int i = ((y * Width) + x) * cCount;

    //    if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
    //    {
    //        Pixels[i] = color.B;
    //        Pixels[i + 1] = color.G;
    //        Pixels[i + 2] = color.R;
    //        Pixels[i + 3] = color.A;
    //    }
    //    if (Depth == 24) // For 24 bpp set Red, Green and Blue
    //    {
    //        Pixels[i] = color.B;
    //        Pixels[i + 1] = color.G;
    //        Pixels[i + 2] = color.R;
    //    }
    //    if (Depth == 8)
    //    // For 8 bpp set color value (Red, Green and Blue values are the same)
    //    {
    //        Pixels[i] = color.B;
    //    }
    //}
}
public class ScreenCap_old
{
    //public Bitmap bmp;
    //public MemoryStream mem_stream; //03.07.2015

    //[DllImport("user32.dll")]
    //[return: MarshalAs(UnmanagedType.Bool)]
    //internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
    // Win32 API calls necessary to support screen capture
    //[DllImport("gdi32", EntryPoint = "BitBlt", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    //private static extern int BitBlt(int hDestDC, int x, int y, int nWidth, int nHeight, int hSrcDC, int xSrc,
    //                                 int ySrc, int dwRop);

    //[DllImport("user32", EntryPoint = "GetDC", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    //private static extern int GetDC(int hwnd);

    //[DllImport("user32", EntryPoint = "ReleaseDC", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    //private static extern int ReleaseDC(int hwnd, int hdc);
    
    //public Bitmap[] bmpScreen = new Bitmap[6];
    //public MemoryStream[] ms = new MemoryStream[6];

    //public MemoryStream PrintWindow() //Bitmap
    //{
    //    MemoryStream ms = new MemoryStream();
    //    IntPtr hwnd = handle;
    //    //Bitmap bmp = null;  //29.06.2015
    //    //using (var proc = Process.GetProcessesByName(prcname)[0])
    //    //{
    //    //    hwnd = proc.MainWindowHandle;
    //    //}

    //    //System.Windows.Rect rc;

    //    if (hwnd != null)
    //    {
    //        pic_ready = false;

    //        //Rect rc = new Rect();
    //        //if (rc.Right <= 0)
    //        GetWindowRect(hwnd, ref rc);

    //        //Rectangle rc = Screen.PrimaryScreen.Bounds;

    //        if ((rc.Top + rc.Bottom + rc.Left + rc.Right) > 0 && rc.Right - rc.Left > 0 && rc.Bottom - rc.Top > 0)
    //        {
    //            //using (Bitmap bmp = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
    //            using (bmp = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top, PixelFormat.Format32bppRgb))
    //            {
    //                using (Graphics gfxBmp = Graphics.FromImage(bmp))
    //                {
    //                    IntPtr hdcBitmap = gfxBmp.GetHdc();
    //                    try
    //                    {
    //                        if (PrintWindow(hwnd, hdcBitmap, 0)) pic_ready = true;
    //                    }
    //                    finally
    //                    {
    //                        gfxBmp.ReleaseHdc(hdcBitmap);
    //                    }
    //                }

    //                //Обрезаем картинку до узкой полоски. Экономия ~14% времени
    //                //bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
    //                //Bitmap bitmap2 = bmp.Clone(new Rectangle(0, 0, bmp.Width, (int)(bmp.Height * 0.1)), bmp.PixelFormat);
    //                //bitmap2.RotateFlip(RotateFlipType.Rotate180FlipNone);
    //                //bitmap2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
    //                //bitmap2.Dispose();

    //                //Bitmap bitmap2 = new Bitmap(bmp.Width, (int)(bmp.Height * 0.2));
    //                //using (Graphics g = Graphics.FromImage(bitmap2))
    //                //{
    //                //    g.DrawImage(bmp, new Rectangle
    //                //    (0, 0, bmp.Width, (int)(bmp.Height * 0.2)), 0, 0, bmp.Width, (int)(bmp.Height * 0.2), GraphicsUnit.Pixel);
    //                //    g.Save();
    //                //}


    //                if (pic_ready) //Обрезаем картинку до узкой полоски в 10%. Получаем 4% загрузки CPU вместо 10%.
    //                {
    //                    bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
    //                    using (Bitmap bmp_short = bmp.Clone(new Rectangle(0, 0, bmp.Width, (int)(bmp.Height * 0.1)), bmp.PixelFormat))
    //                    {
    //                        bmp_short.RotateFlip(RotateFlipType.Rotate180FlipNone);
    //                        bmp_short.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
    //                    }
    //                }


    //                //MessageBox.Show(ms.Length.ToString());
    //                //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
    //                //if (ms.Length > 0) pic_ready = true;

    //                //return bmp;
    //            }
    //        }
    //    }
    //    //else return null;
    //    return ms;//bmp;
    //}

    //public void screen_capt_prereq(int[] arr) //int[] arr
    //{
    //    rect = Screen.PrimaryScreen.Bounds;
    //    //bmp = new Bitmap(rect.Right, rect.Bottom);
    //    //mem_stream = new MemoryStream();

    //    for (int i = 0; i < arr.Length; i++)
    //    {
    //        if (arr[i] == 1)
    //        {
    //            bmpScreen[i] = new Bitmap(rect.Right, rect.Bottom);
    //            ms[i] = new MemoryStream();
    //        }
    //    }
    //}

    //public void get_picture(int[] arr)
    //{
    //    if (rect.Width / 16 * 9 == rect.Height)
    //    {
    //        //MemoryStream ms = new MemoryStream();
    //        {
    //            for (int i = 0; i < arr.Length; i++)
    //            {
    //                if (arr[i] == 1)
    //                {
    //                    ms[i] = PrintWindow();
    //                    if (ms != null && ms.Length > 0)
    //                    {
    //                        try
    //                        {
    //                            bmpScreen[i] = (Bitmap)Image.FromStream(ms[i]); //CaptureImage2(0, 0);
    //                            pic_get = true;
    //                        }
    //                        catch { }
    //                    }
    //                }
    //            }
    //        }
    //        //ms.Dispose();
    //    }
    //}

    //public void get_pic()
    //{
    //    if (rect.Height == 1056) rect.Height = 1050;
    //    //MessageBox.Show("Разрешение в картинке:" + rect.Width.ToString() + "x" + rect.Height.ToString());
    //    //if (rect.Width / 16 * 9 == rect.Height)
    //    //{
    //        try
    //        {
    //            mem_stream = PrintWindow(); //03.07.2015
    //            if (pic_ready)
    //                bmp = (Bitmap)Image.FromStream(mem_stream);
    //        }
    //        catch
    //        {
    //        }
    //    //}
    //}

    //public Bitmap get_pic_crop() //04.07.2015
    //{
    //    IntPtr hwnd = handle;
    //    Bitmap result = null;

    //    if (rect.Height == 1056) rect.Height = 1050;

    //    if (hwnd != null)
    //    {
    //        pic_ready = false;
    //        Rect rc = new Rect();
    //        GetWindowRect(hwnd, ref rc);


    //        if ((rc.Top + rc.Bottom + rc.Left + rc.Right) > 0 && rc.Right - rc.Left > 0 && rc.Bottom - rc.Top > 0)
    //        {
    //            using (bmp = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top, PixelFormat.Format32bppRgb))
    //            {
    //                using (Graphics gfxBmp = Graphics.FromImage(bmp))
    //                {
    //                    IntPtr hdcBitmap = gfxBmp.GetHdc();
    //                    try
    //                    {
    //                        if (PrintWindow(hwnd, hdcBitmap, 0)) pic_ready = true;
    //                    }
    //                    finally
    //                    {
    //                        gfxBmp.ReleaseHdc(hdcBitmap);
    //                    }
    //                }

    //                //Обрезаем картинку до узкой полоски. Экономия ~14% времени
    //                if (pic_ready)
    //                {
    //                    bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
    //                    result = bmp.Clone(new Rectangle(0, 0, bmp.Width, (int)(bmp.Height * 0.1)), bmp.PixelFormat);
    //                    result.RotateFlip(RotateFlipType.Rotate180FlipNone);
    //                }
    //                    //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
    //                //bitmap2.Dispose();
    //            }
    //        }
    //    }
    //    return result;
    //}


    //public int[] check_pic(int[] cdr_run)
    //{
    //    int[] result = new int[] { 2,2,2,2,2,2 };
    //    int[] skill = new int[] { 0, 0, 0, 0, 0, 0 };
    //    //int start = 0, finish = 0;

    //    //if (rect.Width / 16 * 9 == rect.Height)
    //    //{

    //        int j = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9287)); //04.07.2015 обрезаем 90% картинки
    //        int l = Convert.ToInt16(rect.Height * 0.1) - Convert.ToInt16(rect.Height * (1 - 0.9297)); //04.07.2015 обрезаем 90% картинки

    //        //if (rect.Height < 1200) 
    //        //{
    //          //  MessageBox.Show("Разрешение:" + rect.Width.ToString() + "x" + rect.Height.ToString() + ", высота полоски: " + j.ToString());
    //          //  MessageBox.Show("Высота картинки: " + Convert.ToInt16(rect.Height * 0.1).ToString() + "Расположение скилла от конца: " + Convert.ToInt16(rect.Height * (1 - 0.9287)).ToString());
    //          //string filename =  Convert.ToInt16(rect.Height * 0.1).ToString() + "_" + Convert.ToInt16(rect.Height * (1 - 0.9287)).ToString() + ".jpg";
    //          //bmp.Save("c:\\" + filename);
    //        //}

    //        //int j = Convert.ToInt16(rect.Height * 0.9287); //1003 пикселя высота полоски для 4 скиллов
    //        //int l = Convert.ToInt16(rect.Height * 0.9306); //1005 пикселя (первые 2 скилла)

    //        for (int i = 0; i < 6; i++)
    //        {
    //            if (cdr_run[i] == 1 && bmp != null)
    //            {
    //                if (i > 3) j = l;

    //                for (int x = skill_begin[i]; x < skill_end[i]; x += 1)
    //                {
    //                    if (find_pic(bmp, x, j))
    //                    {
    //                        //if (debug) MessageBox.Show("Скилл " + i.ToString() + " не активен");
    //                        result[i] = 0;
    //                        break;
    //                    }
    //                    else result[i] = 1; //0 - скилл в кулдауне, 1 - можно прожимать
    //                }

    //            }
    //        }
    //    //}
    //    return result;
    //}

    //private bool cdr_key_check(int k)
    //{
    //    bool result = false;

    //    //int WidthScreen = Screen.PrimaryScreen.Bounds.Width;//Screen.PrimaryScreen.WorkingArea.X;
    //    //int HeightScreen = Screen.PrimaryScreen.Bounds.Height;//Screen.PrimaryScreen.WorkingArea.Y;

    //    if (rect.Width / 16 * 9 == rect.Height) //Соотношение сторон экрана 16:9
    //    {
    //        //MessageBox.Show(rect.Width.ToString() + " x " + rect.Height.ToString()); //1920x1080

    //        //using (Bitmap bmpScreen = new Bitmap(r.Right, r.Bottom))
    //        //{
    //        //const int SRCCOPY = 13369376;

    //        //using (Graphics g = Graphics.FromImage(bmpScreen))
    //        //{
    //        //    // Get a device context to the windows desktop and our destination  bitmaps
    //        //    int hdcSrc = GetDC(0);
    //        //    IntPtr hdcDest = g.GetHdc();

    //        //    // Copy what is on the desktop to the bitmap
    //        //    BitBlt(hdcDest.ToInt32(), 0, 0, r.Right, r.Bottom, hdcSrc, 0, 0, SRCCOPY);

    //        //    // Release device contexts
    //        //    g.ReleaseHdc(hdcDest);
    //        //    ReleaseDC(0, hdcSrc);
    //        //}

    //        //Rectangle r = Screen.PrimaryScreen.Bounds;
    //        //Bitmap bmpScreen = new Bitmap(r.Right, r.Bottom);
    //        //MemoryStream ms = new MemoryStream();

    //        //ms = PrintWindow();
    //        //if (ms != null && ms.Length > 0)
    //        //    bmpScreen = (Bitmap)Image.FromStream(ms); //CaptureImage2(0, 0);

    //        //LockBitmap lockBitmap = new LockBitmap(bmpScreen);
    //        //lockBitmap.LockBits();

    //        //Color compareClr = Color.FromArgb(255, 255, 255, 255);
    //        //for (int y = 0; y < lockBitmap.Height; y++)
    //        //{
    //        //    for (int x = 0; x < lockBitmap.Width; x++)
    //        //    {
    //        //        if (lockBitmap.GetPixel(x, y) == compareClr)
    //        //        {
    //        //            MessageBox.Show("!23");
    //        //        }
    //        //    }
    //        //}

    //        //lockBitmap.UnlockBits();


    //        //Bitmap bmpScreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
    //        //Graphics g = Graphics.FromImage(bmpScreen);
    //        //g.CopyFromScreen(0, 0, 0, 0, bmpScreen.Size);

    //        //Color border = Color.FromArgb(93, 93, 77); //рамка 105/106/86

    //        //MessageBox.Show(bmpScreen.GetPixel(700, 917).ToString()); //103/106/88
    //        //от 600 до 1000 по ширине.
    //        //по высоте на тест от 900 до 930

    //        //rgb(42, 56, 108); rgb(45, 61, 112); - синий над скиллами.

    //        bool found = false, d3 = false;
    //        //int x_found = 0, y_found = 0;
    //        int skill1 = 0, skill2 = 0, skill3 = 0, skill4 = 0, skill5 = 0, skill6 = 0, life_mana = 20;

    //        //for (int j = Convert.ToInt16(rect.Height * 0.83); j < Convert.ToInt16(rect.Height * 0.85); j += 1) //Левая нижняя четверть экрана
    //        int j = Convert.ToInt16(rect.Height * 0.9287); //1003 пикселя высота полоски для 4 скиллов
    //        int l = Convert.ToInt16(rect.Height * 0.9306); //1005 пикселя (первые 2 скилла)
    //        int f = Convert.ToInt16(rect.Height * 0.9722); //1050 пикселя из 1080 (полоска возле банок)

    //        //for (int i = Convert.ToInt16(rect.Width * 0.265); i < Convert.ToInt16(rect.Width * 0.315); i += 1) //100 пикселей банка здоровья
    //        //    if (bmpScreen.GetPixel(i, j).R > 100 && bmpScreen.GetPixel(i, j).G < 40 && bmpScreen.GetPixel(i, j).B < 40) life_mana++;

    //        //for (int i = Convert.ToInt16(rect.Width * 0.68); i < Convert.ToInt16(rect.Width * 0.73); i += 1) //100 пикселей банка маны
    //        //    if (bmpScreen.GetPixel(i, j).B > 100 && bmpScreen.GetPixel(i, j).R < 40 && bmpScreen.GetPixel(i, j).G < 40) life_mana++;

    //        //for (int i = Convert.ToInt16(rect.Width * 0.3125); i < Convert.ToInt16(rect.Width * 0.3229); i += 1) //20 пикселей около банки здоровья 600->
    //        //    if (bmpScreen.GetPixel(i, f).R < 18 &&
    //        //        bmpScreen.GetPixel(i, f).G < 90 && bmpScreen.GetPixel(i, f).G > 40 &&
    //        //        bmpScreen.GetPixel(i, f).B < 90 && bmpScreen.GetPixel(i, f).B > 40) life_mana++;
    //        ////MessageBox.Show("x: " + i.ToString() + ", y: " + f.ToString() + " Цвет" +
    //        ////    " R: " + bmpScreen.GetPixel(i, f).R.ToString() + " G: " + bmpScreen.GetPixel(i, f).G.ToString() + " B: " + bmpScreen.GetPixel(i, f).B.ToString());

    //        //for (int i = Convert.ToInt16(rect.Width * 0.6771); i < Convert.ToInt16(rect.Width * 0.6875); i += 1) //20 пикселей около банки маны 1300->
    //        //    if (bmpScreen.GetPixel(i, f).R < 18 &&
    //        //        bmpScreen.GetPixel(i, f).G < 90 && bmpScreen.GetPixel(i, f).G > 40 &&
    //        //        bmpScreen.GetPixel(i, f).B < 90 && bmpScreen.GetPixel(i, f).B > 40) life_mana++;

    //        if (life_mana > 10) //Есть ли лазурные области возле панелей жизней/маны на экране?
    //        {

    //            switch (k)
    //            {
    //                case 0:
    //                    for (int i = Convert.ToInt16(rect.Width * 0.33); i < Convert.ToInt16(rect.Width * 0.356); i += 1) //50 пикселей
    //                    {
    //                        if (bmpScreen[0] != null && find_pic(bmpScreen[0], i, j)) skill1++;
    //                    }
    //                    if (skill1 > 0)
    //                    {
    //                        found = true;
    //                        if (test == 1) MessageBox.Show("Скилл 1 не активен");
    //                    }
    //                    break;
    //                case 1:
    //                    for (int i = Convert.ToInt16(rect.Width * 0.365); i < Convert.ToInt16(rect.Width * 0.391); i += 1) //701 -> 751
    //                    {
    //                        if (bmpScreen[1] != null && find_pic(bmpScreen[1], i, j)) skill2++;
    //                    }
    //                    if (skill2 > 0)
    //                    {
    //                        found = true;
    //                        if (test == 1) MessageBox.Show("Скилл 2 не активен");
    //                    }
    //                    break;
    //                case 2:
    //                    for (int i = Convert.ToInt16(rect.Width * 0.4); i < Convert.ToInt16(rect.Width * 0.426); i += 1)
    //                    {
    //                        if (bmpScreen[2] != null && find_pic(bmpScreen[2], i, j)) skill3++;
    //                    }
    //                    if (skill3 > 0)
    //                    {
    //                        found = true;
    //                        if (test == 1) MessageBox.Show("Скилл 3 не активен");
    //                    }
    //                    break;
    //                case 3:
    //                    for (int i = Convert.ToInt16(rect.Width * 0.435); i < Convert.ToInt16(rect.Width * 0.461); i += 1)
    //                    {
    //                        if (bmpScreen[3] != null && find_pic(bmpScreen[3], i, j)) skill4++;
    //                    }
    //                    if (skill4 > 0)
    //                    {
    //                        found = true;
    //                        if (test == 1) MessageBox.Show("Скилл 4 не активен");
    //                    }
    //                    break;
    //                case 4:
    //                    for (int i = Convert.ToInt16(rect.Width * 0.4713); i < Convert.ToInt16(rect.Width * 0.4953); i += 1)
    //                    {
    //                        if (bmpScreen[4] != null && find_pic(bmpScreen[4], i, l)) skill5++;
    //                    }
    //                    if (skill5 > 0)
    //                    {
    //                        found = true;
    //                        if (test == 1) MessageBox.Show("Скилл 5 не активен");
    //                    }
    //                    break;
    //                case 5:
    //                    for (int i = Convert.ToInt16(rect.Width * 0.5057); i < Convert.ToInt16(rect.Width * 0.5297); i += 1)
    //                    {
    //                        if (bmpScreen[5] != null && find_pic(bmpScreen[5], i, l)) skill6++;
    //                    }
    //                    if (skill6 > 0)
    //                    {
    //                        found = true;
    //                        if (test == 1) MessageBox.Show("Скилл 6 не активен");
    //                    }
    //                    break;
    //            }

    //            if (!found) result = true;
    //            //MessageBox.Show("Не нашли");

    //        }

    //        //bmpScreen.Dispose();
    //        //g.Dispose();
    //        //}
    //    }
    //    return result;
    //}

    //public MemoryStream PrintWindow1() //Bitmap
    //{
    //    MemoryStream ms = new MemoryStream();
    //    IntPtr dc1;
    //    IntPtr hwnd = handle;

    //    if (hwnd != null)
    //    {
    //        pic_ready = false;
    //        Rect rc = new Rect();
    //        GetWindowRect(hwnd, ref rc);
    //        const int SRCCOPY = 13369376;

    //        using (bmp = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top, PixelFormat.Format32bppRgb))
    //        {
    //            using (Graphics g = Graphics.FromImage(bmp))
    //            {
    //                dc1 = g.GetHdc();
    //                try
    //                {
    //                    //BitBlt(dc1, 0, 0, rc.Right - rc.Left, rc.Bottom - rc.Top, hwnd, 0, 0, 13369376);
    //                    BitBlt(dc1, 0, 0, rc.Right - rc.Left, rc.Bottom - rc.Top, hwnd, 0, 0, SRCCOPY);
    //                }
    //                finally
    //                {
    //                    g.ReleaseHdc(dc1);
    //                }
    //            }

    //            //bmp.Save(ms, GetEncoderInfo(ImageFormat.Jpeg), JpegParam);
    //            //bmp.Save(ms, ImageFormat.Jpeg);

    //            bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
    //            using (Bitmap bmp_short = bmp.Clone(new Rectangle(0, 0, bmp.Width, (int)(bmp.Height * 0.1)), bmp.PixelFormat))
    //            {
    //                bmp_short.RotateFlip(RotateFlipType.Rotate180FlipNone);
    //                bmp_short.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
    //            }

    //        }
    //    }

    //    return ms;//bmp;
    //}

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

    //private bool find_pic(Bitmap bmpScreen, int i, int j)
    //{
    //    bool result = false;
    //    //if ((bmpScreen.GetPixel(i, j).R > 70 && bmpScreen.GetPixel(i, j).R < 130) &&
    //    //    (bmpScreen.GetPixel(i, j).G > 70 && bmpScreen.GetPixel(i, j).G < 130) &&
    //    //    (bmpScreen.GetPixel(i, j).B > 50 && bmpScreen.GetPixel(i, j).B < 100))
    //    //    result = true;

    //    try
    //    {
    //        if (bmpScreen.GetPixel(i, j).B < 45 || bmpScreen.GetPixel(i, j).R > 130) result = true; //скилл в откате
    //    }
    //    catch { }            

    //    //if ( (i == 724 && j == 1003) || (i == 725 && j == 1003) || (i == 726 && j == 1003) )
    //    //MessageBox.Show(" R: " + bmpScreen.GetPixel(i, j).R.ToString() + " G: " + bmpScreen.GetPixel(i, j).G.ToString() + " B: " + bmpScreen.GetPixel(i, j).B.ToString());
    //    return result;
    //}

    //private bool find_pic_lock(Bitmap bmpScreen, int i, int j)
    //{
    //    bool result = false;
    //    LockBitmap lockBitmap = new LockBitmap(bmpScreen);
    //    lockBitmap.LockBits();

    //    if (lockBitmap.GetPixel(i, j).B < 45) result = true;

    //    lockBitmap.UnlockBits();
    //    return result;
    //}

    //public static Bitmap CaptureImage(int x, int y)
    //{
    //    Rectangle r = new Rectangle(0, 0, 100, 100);
    //    //Rectangle r = Screen.PrimaryScreen.Bounds;

    //    Bitmap b = new Bitmap(r.Right, r.Bottom);
    //    //Color c = Color.Black;

    //    const int SRCCOPY = 13369376;

    //    using (Graphics g = Graphics.FromImage(b))
    //    {
    //        // Get a device context to the windows desktop and our destination  bitmaps
    //        IntPtr hdcSrc = GetDC((IntPtr)0);
    //        IntPtr hdcDest = g.GetHdc();

    //        // Copy what is on the desktop to the bitmap
    //        BitBlt(hdcDest, 0, 0, r.Right, r.Bottom, hdcSrc, 0, 0, SRCCOPY);

    //        // Release device contexts
    //        g.ReleaseHdc(hdcDest);
    //        ReleaseDC((IntPtr)0, hdcSrc);
    //    }

    //    //c = b.GetPixel(0, 0);
    //    return b;
    //}

    //private static Bitmap CaptureImage1(int x, int y)
    //{
    //    Bitmap b = new Bitmap(100, 100);
    //    using (Graphics g = Graphics.FromImage(b))
    //    {
    //        g.CopyFromScreen(x, y, 0, 0, new Size(100, 100), CopyPixelOperation.SourceCopy);
    //        g.DrawLine(Pens.Black, new Point(0, 27), new Point(99, 27));
    //        g.DrawLine(Pens.Black, new Point(0, 73), new Point(99, 73));
    //        g.DrawLine(Pens.Black, new Point(52, 0), new Point(52, 99));
    //        g.DrawLine(Pens.Black, new Point(14, 0), new Point(14, 99));
    //        g.DrawLine(Pens.Black, new Point(85, 0), new Point(85, 99));
    //    }
    //    return b;
    //}

    ///*Note unsafe keyword*/
    //public unsafe Bitmap CaptureImage2(int x, int y)
    //{
    //    //Rectangle r = new Rectangle(0, 0, 1000, 1000);
    //    Rectangle r = Screen.PrimaryScreen.Bounds;

    //    Bitmap b = new Bitmap(r.Right, r.Bottom);//note this has several overloads, including a path to an image
    //    Graphics g = Graphics.FromImage(b);
    //    g.CopyFromScreen(0, 0, 0, 0, b.Size);
    //    g.Dispose();

    //    BitmapData bData = b.LockBits(new Rectangle(0, 0, r.Right, r.Bottom), ImageLockMode.ReadWrite, b.PixelFormat);

    //    byte bitsPerPixel = (byte)System.Windows.Forms.Screen.PrimaryScreen.BitsPerPixel;


    //    /*This time we convert the IntPtr to a ptr*/
    //    byte* scan0 = (byte*)bData.Scan0.ToPointer();

    //    for (int i = 0; i < bData.Height; ++i)
    //    {
    //        for (int j = 0; j < bData.Width; ++j)
    //        {
    //            byte* data = scan0 + i * bData.Stride + j * bitsPerPixel / 8;

    //            //data is a pointer to the first byte of the 3-byte color data
    //        }
    //    }

    //    b.UnlockBits(bData);

    //    return b;
    //}
}
public class d3hot_old
{
    //public System.Timers.Timer StartTimer1, RepeatTimer1, StartTimer2, RepeatTimer2, StartTimer3, RepeatTimer3,
    //                            StartTimer4, RepeatTimer4, StartTimer5, RepeatTimer5, StartTimer6, RepeatTimer6
    //tmr1, tmr2, tmr3, tmr4, tmr5, tmr6 ;

    //public int
    //trig1 = 0, trig2 = 0, trig3 = 0, trig4 = 0, trig5 = 0, trig6 = 0,
    //key1 = 0, key2 = 0, key3 = 0, key4 = 0, key5 = 0, key6 = 0,
    //tmr1_f = 0, tmr2_f = 0, tmr3_f = 0, tmr4_f = 0, tmr5_f = 0, tmr6_f = 0,
    //tmr1_r = 0, tmr2_r = 0, tmr3_r = 0, tmr4_r = 0, tmr5_r = 0, tmr6_r = 0,
    //,hold_key0 = 0, hold_key1 = 0, hold_key2 = 0, hold_key3 = 0, hold_key4 = 0, hold_key5 = 0
    //,cdr_key0 = 0, cdr_key1 = 0, cdr_key2 = 0, cdr_key3 = 0, cdr_key4 = 0, cdr_key5 = 0;

    //public Stopwatch 
    //tmr1_watch, tmr2_watch, tmr3_watch, tmr4_watch, tmr5_watch, tmr6_watch, 

    //public static int key1_h = 0, key2_h = 0, key3_h = 0, key4_h = 0, key5_h = 0, key6_h = 0;
    //public static VirtualKeyCode key_v[0] = 0, key_v[1] = 0, key_v[2] = 0, key_v[3] = 0, key_v[4] = 0, key_v[5] = 0;
    //public double tmr_i[0] = 0, tmr_i[1] = 0, tmr_i[2] = 0, tmr_i[3] = 0, tmr_i[4] = 0, tmr_i[5] = 0;

    //public static int //,tmr1_left = 0, tmr2_left = 0, tmr3_left = 0, tmr4_left = 0, tmr5_left = 0, tmr6_left = 0

    //public void rand_interval(int i)
    //{
        //switch (i)
        //{
        //    case 1:
        //        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
        //        if (rnd + tmr_i[0] < 1) rnd = 31 - (int)tmr_i[0];
        //        break;
        //    case 2:
        //        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
        //        if (rnd + tmr_i[1] < 1) rnd = 31 - (int)tmr_i[1];
        //        break;
        //    case 3:
        //        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
        //        if (rnd + tmr_i[2] < 1) rnd = 31 - (int)tmr_i[2];
        //        break;
        //    case 4:
        //        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
        //        if (rnd + tmr_i[3] < 1) rnd = 31 - (int)tmr_i[3];
        //        break;
        //    case 5:
        //        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
        //        if (rnd + tmr_i[4] < 1) rnd = 31 - (int)tmr_i[4];
        //        break;
        //    case 6:
        //        rnd = rand.Next(-(int)nud_rand.Value, (int)nud_rand.Value);
        //        if (rnd + tmr_i[5] < 1) rnd = 31 - (int)tmr_i[5];
        //        break;
        //}

        //switch (i)
        //{
        //    case 1:
        //        tmr[0].Interval = tmr_i[0] + rnd;
        //        break;
        //    case 2:
        //        tmr[1].Interval = tmr_i[1] + rnd;
        //        break;
        //    case 3:
        //        tmr[2].Interval = tmr_i[2] + rnd;
        //        break;
        //    case 4:
        //        tmr[3].Interval = tmr_i[3] + rnd;
        //        break;
        //    case 5:
        //        tmr[4].Interval = tmr_i[4] + rnd;
        //        break;
        //    case 6:
        //        tmr[5].Interval = tmr_i[5] + rnd;
        //        break;
        //}
    //}

    //public void timer_load(int i)
    //{
        //switch (i)
        //{
        //    case 0: 
        //        tmr_all = new System.Timers.Timer();
        //        tmr_all.Elapsed += tmr_all_Elapsed;
        //        tmr_all.Interval = 1; //01.07.2015
        //        break;
        //    case 1:
        //        tmr[0] = new System.Timers.Timer();
        //        tmr[0].Elapsed += tmr_Elapsed;
        //        if (cdr_key[0] < 1) rand_interval(1);
        //        //MessageBox.Show("Задержка с рандомом 1: " + tmr_i[0].ToString());
        //        //tmr1.Interval = tmr_i[0];
        //        tmr_watch[0] = new Stopwatch();
        //        break;
        //    case 2:
        //        tmr[1] = new System.Timers.Timer();
        //        tmr[1].Elapsed += tmr_Elapsed;
        //        if (cdr_key[1] < 1) rand_interval(2);
        //        //MessageBox.Show("Задержка с рандомом 2: " + tmr_i[1].ToString());
        //        //tmr2.Interval = tmr_i[1];
        //        tmr_watch[1] = new Stopwatch();
        //        break;
        //    case 3:
        //        tmr[2] = new System.Timers.Timer();
        //        tmr[2].Elapsed += tmr_Elapsed;
        //        if (cdr_key[2] < 1) rand_interval(3);
        //        //MessageBox.Show("Задержка с рандомом 3: " + tmr_i[2].ToString());
        //        //tmr3.Interval = tmr_i[2];
        //        tmr_watch[2] = new Stopwatch();
        //        break;
        //    case 4:
        //        tmr[3] = new System.Timers.Timer();
        //        tmr[3].Elapsed += tmr_Elapsed;
        //        if (cdr_key[3] < 1) rand_interval(4);
        //        //MessageBox.Show("Задержка с рандомом 4: " + tmr_i[3].ToString());
        //        //tmr4.Interval = tmr_i[3];
        //        tmr_watch[3] = new Stopwatch();
        //        break;
        //    case 5:
        //        tmr[4] = new System.Timers.Timer();
        //        tmr[4].Elapsed += tmr_Elapsed;
        //        if (cdr_key[4] < 1) rand_interval(5);
        //        //MessageBox.Show("Задержка с рандомом 5: " + tmr_i[4].ToString());
        //        //tmr5.Interval = tmr_i[4];
        //        tmr_watch[4] = new Stopwatch();
        //        break;
        //    case 6:
        //        tmr[5] = new System.Timers.Timer();
        //        tmr[5].Elapsed += tmr_Elapsed;
        //        if (cdr_key[5] < 1) rand_interval(6);
        //        //MessageBox.Show("Задержка с рандомом 6: " + tmr_i[5].ToString());
        //        //tmr6.Interval = tmr_i[5];
        //        tmr_watch[5] = new Stopwatch();
        //        break;
        //}
    //}

    //public void timer_unload(int i)
    //{
        //if (tmr[0] != null && (i == 1 || i == 99 || i == 88))
        //{
        //    tmr_left[0] = (int)tmr[0].Interval > 0 ? (int)tmr[0].Interval - (int)tmr_watch[0].ElapsedMilliseconds : 0;
        //    //tmr1.Enabled = false;
        //    tmr[0].Dispose();
        //    tmr_watch[0].Stop();
        //}
        //if (tmr[1] != null && (i == 2 || i == 99 || i == 88))
        //{
        //    tmr_left[1] = (int)tmr[1].Interval > 0 ? (int)tmr[1].Interval - (int)tmr_watch[1].ElapsedMilliseconds : 0;
        //    tmr[1].Dispose();
        //    tmr_watch[1].Stop();
        //}
        //if (tmr[2] != null && (i == 3 || i == 99 || i == 88)) 
        //{
        //    tmr_left[2] = (int)tmr[2].Interval > 0 ? (int)tmr[2].Interval - (int)tmr_watch[2].ElapsedMilliseconds : 0;
        //    tmr[2].Dispose();
        //    tmr_watch[2].Stop();
        //}
        //if (tmr[3] != null && (i == 4 || i == 99 || i == 88)) 
        //{
        //    tmr_left[3] = (int)tmr[3].Interval > 0 ? (int)tmr[3].Interval - (int)tmr_watch[3].ElapsedMilliseconds : 0;
        //    tmr[3].Dispose();
        //    tmr_watch[3].Stop();
        //}
        //if (tmr[4] != null && (i == 5 || i == 99 || i == 88)) 
        //{
        //    tmr_left[4] = (int)tmr[4].Interval > 0 ? (int)tmr[4].Interval - (int)tmr_watch[4].ElapsedMilliseconds : 0;
        //    tmr[4].Dispose();
        //    tmr_watch[4].Stop();
        //}
        //if (tmr[5] != null && (i == 6 || i == 99 || i == 88)) 
        //{
        //    tmr_left[5] = (int)tmr[5].Interval > 0 ? (int)tmr[5].Interval - (int)tmr_watch[5].ElapsedMilliseconds : 0;
        //    tmr[5].Dispose();
        //    tmr_watch[5].Stop();
        //}
    //}

    /// <summary>
    /// Метод для установки клавиш для нажимания.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    //private VirtualKeyCode virt_code(int i)
    //{
    //    VirtualKeyCode vkc = VirtualKeyCode.VK_0;
    //    switch (i)
    //    {
    //        case 1: vkc = VirtualKeyCode.VK_1; break;
    //        case 2: vkc = VirtualKeyCode.VK_2; break;
    //        case 3: vkc = VirtualKeyCode.VK_3; break;
    //        case 4: vkc = VirtualKeyCode.VK_4; break;
    //        case 5: vkc = VirtualKeyCode.VK_Q; break;
    //        case 6: vkc = VirtualKeyCode.VK_W; break;
    //        case 7: vkc = VirtualKeyCode.VK_E; break;
    //        case 8: vkc = VirtualKeyCode.VK_R; break;
    //        case 9: vkc = VirtualKeyCode.VK_A; break;
    //        case 10: vkc = VirtualKeyCode.VK_S; break;
    //        case 11: vkc = VirtualKeyCode.VK_D; break;
    //        case 12: vkc = VirtualKeyCode.VK_F; break;
    //        case 13: vkc = VirtualKeyCode.VK_Z; break;
    //        case 14: vkc = VirtualKeyCode.VK_X; break;
    //        case 15: vkc = VirtualKeyCode.VK_C; break;
    //        case 16: vkc = VirtualKeyCode.VK_V; break;
    //        case 17: vkc = VirtualKeyCode.SPACE; break;
    //        case 18: vkc = VirtualKeyCode.LBUTTON; break;
    //        case 19: vkc = VirtualKeyCode.RBUTTON; break;
    //        case 20: vkc = VirtualKeyCode.XBUTTON1; break;
    //        case 21: vkc = VirtualKeyCode.XBUTTON2; break;
    //    }
    //    return vkc;
    //}

    /// <summary>
    /// Метод для установки клавиш для залипания.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    //private int key_hold_code(int i)
    //{
    //    int key_code = 0;
    //    switch (i)
    //    {
    //        case 1: key_code = (int)Keys.D1; break;
    //        case 2: key_code = (int)Keys.D2; break;
    //        case 3: key_code = (int)Keys.D3; break;
    //        case 4: key_code = (int)Keys.D4; break;
    //        case 5: key_code = (int)Keys.Q; break;
    //        case 6: key_code = (int)Keys.W; break;
    //        case 7: key_code = (int)Keys.E; break;
    //        case 8: key_code = (int)Keys.R; break;
    //        case 9: key_code = (int)Keys.A; break;
    //        case 10: key_code = (int)Keys.S; break;
    //        case 11: key_code = (int)Keys.D; break;
    //        case 12: key_code = (int)Keys.F; break;
    //        case 13: key_code = (int)Keys.Z; break;
    //        case 14: key_code = (int)Keys.X; break;
    //        case 15: key_code = (int)Keys.C; break;
    //        case 16: key_code = (int)Keys.V; break;
    //        case 17: key_code = (int)Keys.Space; break;
    //        case 18: key_code = (int)Keys.LButton; break;
    //        case 19: key_code = (int)Keys.RButton; break;
    //        //case 19: key_code = (int)Keys.XButton1; break; //27.03.2015
    //        //case 20: key_code = (int)Keys.XButton2; break; //27.03.2015
    //        //case 21: key_code = (int)Keys.Shift; break; //27.03.2015
    //    }
    //    return key_code;
    //}

    //* public void tmr_all_Elapsed(object sender, EventArgs e)
    //* {
        //Проверка существования окна //06.04.2015
        //if ((int)handle > 0 && proc_watch != null && !proc_watch.IsRunning)
        //{
        //    proc_watch.Start();
        //}

        //if (proc_watch != null && (int)proc_watch.ElapsedMilliseconds > 10000 && (int)handle > 0)
        //{
        //    proc_watch.Reset();
        //    proc_watch.Stop();
        //    if (GetActiveWindowTitle(handle) == null)
        //        cb_start.Invoke(new Action(() =>
        //            {
        //                cb_start.Checked = false;
        //            }));
        //}

            //* if (key_press(trig[0]) || key_press(trig[1]) || key_press(trig[2]) || key_press(trig[3]) || key_press(trig[4]) || key_press(trig[5]))
            //* {
                //Проверка на одинарное/двойное нажатие Enter.
                //if (
                //        (pause == 2 || pause == 3) && 
                //        (
                //            (return_press == 1 && r_press == 0 && t_press == 0 && map_press == 0) ||
                //                (
                //                    return_press_count==1 && 
                //                    ((trig1==1 && key_press(trig1)) ||
                //                    (trig2==1 && key_press(trig2)) ||
                //                    (trig3==1 && key_press(trig3)) ||
                //                    (trig4==1 && key_press(trig4)) ||
                //                    (trig5==1 && key_press(trig5)) ||
                //                    (trig6==1 && key_press(trig6)))
                //                )
                //        )
                //    )
    //* }

    //* public void tmr_Elapsed(object sender, EventArgs e)
    //* {
        //if (tmr_local == tmr[0] && key_press(trig[0]))// && cdr_press[0] != 1
        //{
        //    key = key_v[0];// virt_code(key1);
        //    if (cdr_key[0] < 1) rand_interval(1);
        //    tmr_watch[0].Reset();
        //    tmr_watch[0].Start();
        //}
        //else if (tmr_local == tmr[1] && key_press(trig[1]))
        //{
        //    key = key_v[1]; //virt_code(key2);
        //    if (cdr_key[1] < 1) rand_interval(2);
        //    tmr_watch[1].Reset();
        //    tmr_watch[1].Start();
        //}
        //else if (tmr_local == tmr[2] && key_press(trig[2]))
        //{
        //    key = key_v[2]; //virt_code(key3);
        //    if (cdr_key[2] < 1) rand_interval(3);
        //    tmr_watch[2].Reset();
        //    tmr_watch[2].Start();
        //}
        //else if (tmr_local == tmr[3] && key_press(trig[3]))
        //{
        //    key = key_v[3]; //virt_code(key4);
        //    if (cdr_key[3] < 1) rand_interval(4);
        //    tmr_watch[3].Reset();
        //    tmr_watch[3].Start();
        //}
        //else if (tmr_local == tmr[4] && key_press(trig[4]))
        //{
        //    key = key_v[4]; //virt_code(key5);
        //    if (cdr_key[4] < 1) rand_interval(5);
        //    tmr_watch[4].Reset();
        //    tmr_watch[4].Start();
        //}
        //else if (tmr_local == tmr[5] && key_press(trig[5]))
        //{
        //    key = key_v[5]; //virt_code(key6);
        //    if (cdr_key[5] < 1) rand_interval(6);
        //    tmr_watch[5].Reset();
        //    tmr_watch[5].Start();
        //}

        //if (usage_area())
        //{
        //    if (key_for_hold == (int)Keys.LButton)
        //        inp.Mouse.LeftButtonDown(); //Mouse.PressButton(Mouse.MouseKeys.Left);

        //    if (key_for_hold == (int)Keys.RButton)
        //        Mouse.PressButton(Mouse.MouseKeys.Right);
        //}

        //else if (key_for_hold == (int)Keys.XButton1)
        //{
        //    ret = _MapVirtualKey((int)Keys.Shift, 0);
        //    Point defPnt = new Point();
        //    GetCursorPos(ref defPnt);

        //    //_PostMessage(handle, 0x104, (int)Keys.ShiftKey, 0x002A0001);
        //    //_PostMessage(handle, 0x104, (int)Keys.ShiftKey, 0x402A0001);
        //    PostMessage(handle, updown_keys(10), (int)Keys.Shift, ret | 0x00000001);
        //    System.Threading.Thread.Sleep(1);
        //    PostMessage(handle,
        //               updown_keys(1),
        //               1, (int)MakeLong(defPnt.X, defPnt.Y)); //(int)
        //    PostMessage(handle,
        //               updown_keys(1) + 1,
        //               0, (int)MakeLong(defPnt.X, defPnt.Y)); //(int)
        //    PostMessage(handle, updown_keys(10) + 1, (int)Keys.Shift, ret | (int)(MakeLong(1, ret) + 0xC0000000));
        //    //_PostMessage(handle, 0x105, (int)Keys.ShiftKey, 0xC02A0001);
        //    //PostMessage(handle, updown_keys(key_for_hold) + 1, (int)Keys.Shift, (int)(MakeLong(1, ret) + 0xC0000000));
        //}
        //else if (key_for_hold == (int)Keys.XButton2)
        //{
        //    ret = _MapVirtualKey((int)Keys.Shift, 0);
        //    Point defPnt = new Point();
        //    GetCursorPos(ref defPnt);

        //    PostMessage(handle, 0x100, 0x10, 0x002A0001);
        //    PostMessage(handle, 0x100, 0x10, 0x402A0001);
        //    System.Threading.Thread.Sleep(1);
        //    PostMessage(handle,
        //               updown_keys(2),
        //               2, (int)MakeLong(defPnt.X, defPnt.Y));
        //    System.Threading.Thread.Sleep(1);
        //    PostMessage(handle, 0x101, 0x10, (int)(MakeLong(1, ret) + 0xC0000000));
        //    PostMessage(handle,
        //               updown_keys(2) + 1,
        //               0, (int)MakeLong(defPnt.X, defPnt.Y));

        //}
    //* }
    //* private void key_codes(int i)
    //* {
        //}

        //            char item = new char();
        //string st = "1";
        //bool res = false ;
        //if (st.Length > 0) res = Char.TryParse(st, out item);
        //if (res)
        //{
        //    Key wikey = KeyInterop.KeyFromVirtualKey((int)item);
        //    VirtualKeyCode vkc = (VirtualKeyCode)KeyInterop.VirtualKeyFromKey(wikey);
        //    VirtualKeyCode vkc1 = (VirtualKeyCode)item

        //    MessageBox.Show(item.ToString() + " " + ((int)item).ToString());
        //    MessageBox.Show("Keys D1: " + key_hold_code(1).ToString() + " Virt: " + (VirtualKeyCode.VK_1).ToString() + " Vks: " + vkc1.ToString());

        //    //Key.
        //    //KeyInterop.VirtualKeyFromKey
        //    //KeysConverter kc = new KeysConverter();
        //    //kc.ConvertTo(Keys.D1, Type.);

        //}
    //* }

    //private void key_assign(int i, ComboBox curr)
    //{
    //    char item = Char.Parse(((string)curr.Items[curr.SelectedIndex]).Substring(0,1));
    //    VirtualKeyCode vk = VirtualKeyCode.F12;
    //    string test="";
    //    int key_hold = 0;



    //    switch (test)
    //    {
    //        case "1":
    //            vk = VirtualKeyCode.VK_1;
    //            key_hold = (int)Keys.D1;
    //            break;
    //        case "2":
    //            vk = VirtualKeyCode.VK_2;
    //            key_hold = (int)Keys.D2;
    //            break;
    //        case "3":
    //            vk = VirtualKeyCode.VK_3;
    //            key_hold = (int)Keys.D3;
    //            break;
    //        case "4":
    //            vk = VirtualKeyCode.VK_4;
    //            key_hold = (int)Keys.D4;
    //            break;

    //        case "Q":
    //            vk = VirtualKeyCode.VK_1;
    //            key_hold = (int)Keys.D1;
    //            break;
    //        case "W":
    //            vk = VirtualKeyCode.VK_2;
    //            key_hold = (int)Keys.D2;
    //            break;
    //        case "E":
    //            vk = VirtualKeyCode.VK_3;
    //            key_hold = (int)Keys.D3;
    //            break;
    //        case "R":
    //            vk = VirtualKeyCode.VK_4;
    //            key_hold = (int)Keys.D4;
    //            break;
    //    }

    //    switch (i)
    //    {
    //        case 1:
    //            break;
    //        case 2:
    //            break;
    //        case 3:
    //            break;
    //        case 4:
    //            break;
    //        case 5:
    //            break;
    //        case 6:
    //            break;
    //    }
    //}

    /// <summary>
    /// Метод для работы с таймерами задержек. 1: Остновка всего, 2: запуск, 3: остановка. key/map/tele
    /// </summary>
    /// <param name="watch"></param>
    /// <param name="i"></param>
    //public void delay_timers(int i)
    //{
    //    switch (i)
    //    {
    //        case 1:
    //            //if (key_watch != null) key_watch.Stop();
    //            //if (map_watch != null) map_watch.Stop();
    //            //if (tele_watch != null) tele_watch.Stop();
    //            StopAll(key_watch);
    //            StopAll(map_watch);
    //            StopAll(tele_watch);
    //            break;
    //        case 21: RestartWatch(ref key_watch); break;//key_watch = Stopwatch.StartNew(); 
    //        case 22: RestartWatch(ref map_watch); break;//map_watch = Stopwatch.StartNew(); break;
    //        case 23: RestartWatch(ref tele_watch); break;//tele_watch = Stopwatch.StartNew(); break;

    //        case 31:
    //            StopWatch (key_watch);//if (key_watch != null) key_watch.Reset(); 
    //            break;
    //        case 32:
    //            StopWatch (map_watch);//if (map_watch != null) map_watch.Reset();
    //            break;
    //        case 33:
    //            StopWatch (tele_watch);//if (tele_watch != null) tele_watch.Reset();
    //            break;
    //    }
    //}

    // P/Invoke declarations
    //[DllImport("gdi32.dll")]
    //static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int
    //wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);
    //[DllImport("gdi32.dll")]
    //static extern IntPtr DeleteDC(IntPtr hDc);
    //[DllImport("gdi32.dll")]
    //static extern IntPtr DeleteObject(IntPtr hDc);
    //[DllImport("gdi32.dll")]
    //static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
    //[DllImport("gdi32.dll")]
    //static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    //[DllImport("gdi32.dll")]
    //static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
    //[DllImport("user32.dll")]
    //public static extern IntPtr GetDesktopWindow();
    //[DllImport("user32.dll")]
    //public static extern IntPtr GetWindowDC(IntPtr ptr);

    //[DllImport("user32.dll")]
    //static extern IntPtr GetDC(IntPtr hwnd);
    //[DllImport("user32.dll")]
    //static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);
    //[DllImport("gdi32.dll")]
    //static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

    //static public System.Drawing.Color GetPixelColor(int x, int y)
    //{
    //    IntPtr hdc = GetDC(IntPtr.Zero);
    //    uint pixel = GetPixel(hdc, x, y);
    //    ReleaseDC(IntPtr.Zero, hdc);
    //    Color color = Color.FromArgb((int)(pixel & 0x000000FF),
    //                 (int)(pixel & 0x0000FF00) >> 8,
    //                 (int)(pixel & 0x00FF0000) >> 16);
    //    return color;
    //}

    //* private void cb_tmr_SelectedIndexChanged(object sender, EventArgs e)
    //* {
        //* if (resolution)  //Разрешение 16:10 или 16:9
        //* {
            //switch (name)
            //{
            //    case "cb_tmr1":
            //        nud_tmr1.Enabled = false;
            //        //chb_key1.Visible = false;
            //        cdr_key[0] = 0;
            //        switch (cb.SelectedIndex)
            //        {
            //            case 1:
            //                cdr_key[0] = 1;
            //                lb_tmr1_sec.Text = lng.cb_tmr2;
            //                //lb_tmr1_sec.Font = new Font(lb_tmr1_sec.Font, FontStyle.Italic);
            //                //lb_tmr1_sec.TextAlign = ContentAlignment.MiddleCenter;
            //                break;
            //            case 2:
            //                lb_tmr1_sec.Text = lng.cb_tmr3;
            //                //lb_tmr1_sec.Font = new Font(lb_tmr1_sec.Font, FontStyle.Italic);
            //                break;
            //            default:
            //                //lb_tmr1_sec.Font = new Font(lb_tmr1_sec.Font, FontStyle.Regular);
            //                nud_Leave(nud_tmr1, null);
            //                nud_tmr1.Enabled = true;
            //                //if (!hold_use && chb_hold.Checked) chb_key1.Visible = true;
            //                break;
            //        }
            //        break;
            //    case "cb_tmr2":
            //        nud_tmr2.Enabled = false;
            //        //chb_key2.Visible = false;
            //        cdr_key[1] = 0;
            //        switch (cb.SelectedIndex)
            //        {
            //            case 1:
            //                cdr_key[1] = 1;
            //                lb_tmr2_sec.Text = lng.cb_tmr2;
            //                break;
            //            case 2:
            //                lb_tmr2_sec.Text = lng.cb_tmr3;
            //                break;
            //            default:
            //                nud_Leave(nud_tmr2, null);
            //                nud_tmr2.Enabled = true;
            //                //if (!hold_use && chb_hold.Checked) chb_key2.Visible = true;
            //                break;
            //        }
            //        break;
            //    case "cb_tmr3":
            //        nud_tmr3.Enabled = false;
            //        //chb_key3.Visible = false;
            //        cdr_key[2] = 0;
            //        switch (cb.SelectedIndex)
            //        {
            //            case 1:
            //                cdr_key[2] = 1;
            //                lb_tmr3_sec.Text = lng.cb_tmr2;
            //                break;
            //            case 2:
            //                lb_tmr3_sec.Text = lng.cb_tmr3;
            //                break;
            //            default:
            //                nud_Leave(nud_tmr3, null);
            //                nud_tmr3.Enabled = true;
            //                //if (!hold_use && chb_hold.Checked) chb_key3.Visible = true;
            //                break;
            //        }
            //        break;
            //    case "cb_tmr4":
            //        nud_tmr4.Enabled = false;
            //        //chb_key4.Visible = false;
            //        cdr_key[3] = 0;
            //        switch (cb.SelectedIndex)
            //        {
            //            case 1:
            //                cdr_key[3] = 1;
            //                lb_tmr4_sec.Text = lng.cb_tmr2;
            //                break;
            //            case 2:
            //                lb_tmr4_sec.Text = lng.cb_tmr3;
            //                break;
            //            default:
            //                nud_Leave(nud_tmr4, null);
            //                nud_tmr4.Enabled = true;
            //                //if (!hold_use && chb_hold.Checked) chb_key4.Visible = true;
            //                break;
            //        }
            //        break;
            //    case "cb_tmr5":
            //        nud_tmr5.Enabled = false;
            //        //chb_key5.Visible = false;
            //        cdr_key[4] = 0;
            //        switch (cb.SelectedIndex)
            //        {
            //            case 1:
            //                cdr_key[4] = 1;
            //                lb_tmr5_sec.Text = lng.cb_tmr2;
            //                break;
            //            case 2:
            //                lb_tmr5_sec.Text = lng.cb_tmr3;
            //                break;
            //            default:
            //                nud_Leave(nud_tmr5, null);
            //                nud_tmr5.Enabled = true;
            //                //if (!hold_use && chb_hold.Checked) chb_key5.Visible = true;
            //                break;
            //        }
            //        break;
            //    case "cb_tmr6":
            //        nud_tmr6.Enabled = false;
            //        //chb_key6.Visible = false;
            //        cdr_key[5] = 0;
            //        switch (cb.SelectedIndex)
            //        {
            //            case 1:
            //                cdr_key[5] = 1;
            //                lb_tmr6_sec.Text = lng.cb_tmr2;
            //                break;
            //            case 2:
            //                lb_tmr6_sec.Text = lng.cb_tmr3;
            //                break;
            //            default:
            //                nud_Leave(nud_tmr6, null);
            //                nud_tmr6.Enabled = true;
            //                //if (!hold_use && chb_hold.Checked) chb_key6.Visible = true;
            //                break;
            //        }
            //        break;
            //}
        //* }
    //* }

    //private void cb_tmr_Click(object sender, EventArgs e)
    //{
    //    var cb = (ComboBox)sender;
    //    int pos = -1;
    //    if (cb.SelectedIndex > -1) pos = cb.SelectedIndex;

    //    cdr_only();

    //    //List<string> cont = new List<string>();
    //    //cont.Add(lng.cb_tmr1);
    //    //cont.Add(lng.cb_tmr2);
    //    //if (pos == 2 || !tmr_holding) cont.Add(lng.cb_tmr3);

    //    //refreshList(cont, cb);

    //    cb.Items.Clear();
    //    cb.Items.Add(lng.cb_tmr1);
    //    cb.Items.Add(lng.cb_tmr2);
    //    if (pos == 2 || !tmr_holding) cb.Items.Add(lng.cb_tmr3);

    //    //cb.Invalidate();
    //    cb.SelectedIndex = pos;
    //}

    //* private void cb_tmr_DrawItem(object sender, DrawItemEventArgs e)
    //* {
        //Pen borderPen = new Pen(Color.Red, 1);
        //Point start;
        //Point end;

        //if (e.Index == 0)
        //{
        //    //Draw top border
        //    start = new Point(e.Bounds.Left, e.Bounds.Top);
        //    end = new Point(e.Bounds.Left + e.Bounds.Width - 1, e.Bounds.Top);
        //    e.Graphics.DrawLine(borderPen, start, end);
        //}

        //if (e.Index == cb.Items.Count - 1)
        //{
        //    //Draw bottom border
        //    start = new Point(e.Bounds.Left, e.Bounds.Top + e.Bounds.Height - 1);
        //    end = new Point(e.Bounds.Left + e.Bounds.Width - 1, e.Bounds.Top + e.Bounds.Height - 1);
        //    e.Graphics.DrawLine(borderPen, start, end);
        //}
    //* }

    //public void tmr_all_Elapsed(object sender, EventArgs e)
    //{
    //    tmr_all_counter = 3;

    //    //Stopwatch st = null;
    //    //if (tmr_all_count == 1)
    //    //{st = new Stopwatch(); st.Reset(); st.Start(); }

    //    //if (tmr_pic != null && !tmr_pic.Enabled) tmr_all_count += 20;
    //    //if ((tmr_pic == null || !tmr_pic.Enabled)) 

    //    //if (tmr_all_count > 15) tmr_all_count = 0; //|| (tmr_pic != null && !tmr_pic.Enabled) //07.07.2015
    //    //tmr_all_count++;

    //    //if (tmr_watch[2] != null) MessageBox.Show(tmr_watch[2].IsRunning.ToString() + " " + tmr_watch[2].ElapsedMilliseconds.ToString() + " "
    //    //    + tmr_f[2].ToString() + " " + key_press(trig[2]).ToString() + " " + cdr_key[2].ToString());

    //    //MessageBox.Show("Долгое " + cdr_isrun.ToString() + cdr_isready.ToString() + cdr_press.Any(item => item == 1).ToString());
    //    //MessageBox.Show("cdr_isrun: " + cdr_isrun.ToString() +
    //    //" Прожимаем?" + cdr_press.Any(item => item == 1).ToString() +
    //    //" Захват картинки? " + (tmr_pic == null || !tmr_pic.Enabled).ToString());

    //    if (!cdr_isready && //!cdr_isrun && 
    //        //tmr_all_count == 1 &&
    //        (tmr_pic == null || !tmr_pic.Enabled)
    //        //(tmr_cdr == null || !tmr_cdr.Enabled)&&
    //        && !cdr_press.Any(item => item == 1)
    //        //&& (cdr_watch == null || !cdr_watch.IsRunning || cdr_watch.ElapsedMilliseconds > 100)
    //        )// 
    //    {

    //        //RestartWatch(ref cdr_watch); //15.07.2015
    //        //StopWatch(cdr_watch);
    //        cdr_isready = true;
    //        bool coold = false;//13.07.2015 

    //        //if (cdr_run == null)
    //        cdr_run = new int[] { 0, 0, 0, 0, 0, 0 };


    //        //if (Monitor.TryEnter(valueLocker, 10))
    //        //{
    //        //    try { coold = true; }
    //        //    finally { Monitor.Exit(valueLocker); }
    //        //}

    //        //MessageBox.Show(tmr_f[2].ToString() + cdr_key[2].ToString() + key_press(trig[2]).ToString());

    //        for (int i = 0; i < 6; i++)
    //        {
    //            if (tmr_f[i] == 1 && key_press(trig[i]) && cdr_key[i] == 1 && pic_analyze[i] == 0) //&& cdr_press[i] == 0
    //            {
    //                //cdr_run[i] = 1;
    //                //if (tmr_watch[i] != null) MessageBox.Show(tmr_watch[i].IsRunning.ToString() + " " + tmr_watch[i].ElapsedMilliseconds.ToString());

    //                //if (tmr_watch[i] != null) MessageBox.Show(tmr_watch[i].ElapsedMilliseconds.ToString());
    //                //else MessageBox.Show("Наш триггер: "+i.ToString());


    //                //if (tmr_watch[i] != null && tmr_watch[i].IsRunning && tmr_watch[i].ElapsedMilliseconds < 500)
    //                //    cdr_run[i] = 0;
    //                //else
    //                //{
    //                //    //coold = true;//13.07.2015 
    //                //    //timer_unload(i);
    //                //    if (tmr_watch[i] != null) tmr_watch[i].Reset();
    //                //    cdr_run[i] = 1;
    //                //}

    //                cdr_run[i] = 1;
    //                pic_analyze[i]++;
    //                coold = true;

    //            }

    //            //if (tmr_all_count > 1 && cdr_press[i] > 0)
    //            //{
    //            //    cdr_run[i] = 0;
    //            //    cdr_press[i] = 2;
    //            //}
    //            //else
    //            //{
    //            //    //MessageBox.Show("Что обнулили? "+i.ToString());
    //            //    cdr_press[i] = 0;
    //            //}

    //            //if (cdr_run[i] == 1)
    //            //{
    //            //    coold = true;
    //            //    //if (tmr_all_count > 100) MessageBox.Show("!@#");
    //            //}
    //            if (pic_analyze[i] > 0) pic_analyze[i]++;
    //            if (pic_analyze[i] > 20) pic_analyze[i] = 0; //40
    //        }

    //        if (coold) //13.07.2015 
    //        {
    //            //MessageBox.Show("123");
    //            //screen_capt_prereq(cdr_run);
    //            //screen_capt_pre();

    //            //if (tmr_pic == null)
    //            //{
    //            //    tmr_pic = new System.Timers.Timer();
    //            //    tmr_pic.Elapsed += tmr_pic_Elapsed;
    //            //    tmr_pic.AutoReset = false;
    //            //}
    //            //tmr_pic.Start();

    //            screen_capt_pre();
    //            cdr_press = ScreenCapture(cdr_run);
    //            cdr_isready = false;
    //        }
    //        else cdr_isready = false;

    //    }

    //    //if (Monitor.TryEnter(valueLocker, 0))
    //    //{
    //    //try
    //    //{

    //    if (!cdr_isrun && //!cdr_isready &&
    //        //(tmr_cdr == null || !tmr_cdr.Enabled)&&
    //        (cdr_press.Any(item => item == 1))
    //        //&&(cdr_watch != null && cdr_watch.ElapsedMilliseconds > 5)
    //        )
    //    {
    //        //Interlocked.Increment(ref tmr_all_count);
    //        //tmr_all_count++;
    //        cdr_isrun = true;

    //        if (tmr_cdr == null)
    //        {
    //            tmr_cdr = new System.Timers.Timer();
    //            tmr_cdr.Elapsed += tmr_cdr_Elapsed;
    //            tmr_cdr.AutoReset = false;
    //        }
    //        tmr_cdr.Start();

    //        //MessageBox.Show(cdr_watch.ElapsedMilliseconds.ToString()); //15.07.2015
    //        //StopWatch(cdr_watch); //15.07.2015

    //        //for (int i = 0; i < 6; i++) //прожимаем после предыдущего таймера
    //        //{
    //        //    if (cdr_press[i] == 1 && key_press(trig[i])) timer_cdr_create(i);
    //        //    cdr_press[i] = 0;
    //        //}
    //        //cdr_isrun = false;

    //        //for (int i = 0; i < 6; i++)
    //        //{
    //        //    if (cdr_press[i] == 1)
    //        //    {
    //        //        tmr_r[i] = 1;
    //        //        timer_load(i);
    //        //        tmr[i].AutoReset = false;
    //        //        tmr[i].Enabled = true;
    //        //    }
    //        //}

    //        //for (int i = 0; i < 6; i++)
    //        //{
    //        //    if (cdr_press[i] == 1)
    //        //    {
    //        //        tmr[i].AutoReset = false;
    //        //        //tmr_watch[i].Start();
    //        //        tmr[i].Enabled = true;
    //        //        //MessageBox.Show("1: " + tmr_watch[2].IsRunning.ToString());


    //        //        //if (tmr[i] != null)
    //        //        //    try
    //        //        //    {
    //        //        //        tmr[i].Enabled = true;
    //        //        //        tmr_watch[i].Start();
    //        //        //    }
    //        //        //    catch { }

    //        //    }
    //        //}

    //        //for (int i = 0; i < 6; i++)
    //        //{
    //        //    if (cdr_press[i] == 1)
    //        //    {
    //        //        MessageBox.Show(i.ToString() + " Включён:" + cdr_press[i].ToString());
    //        //    }
    //        //}

    //        //Array.Clear(cdr_press, 0, 6); //14.07.2015
    //        //cdr_isrun = false; //14.07.2015

    //        //RestartWatch(ref cdr_watch);
    //        //do { }
    //        //while (cdr_watch.ElapsedMilliseconds < 1000); //Сразу пытается нажать второй раз, как секунда проходит
    //        //StopWatch(cdr_watch);
    //    }
    //    //}
    //    //finally { Monitor.Exit(valueLocker); }
    //    //}
    //}
}