using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Helpers_1_0
{
    class Graphics_Helpers
    {
        [StructLayout(LayoutKind.Sequential)]
        struct ICONINFO
        {
            public bool fIcon;         // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies
            // an icon; FALSE specifies a cursor.
            public Int32 xHotspot;     // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot
            // spot is always in the center of the icon, and this member is ignored.
            public Int32 yHotspot;     // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot
            // spot is always in the center of the icon, and this member is ignored.
            public IntPtr hbmMask;     // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon,
            // this bitmask is formatted so that the upper half is the icon AND bitmask and the lower half is
            // the icon XOR bitmask. Under this condition, the height should be an even multiple of two. If
            // this structure defines a color icon, this mask only defines the AND bitmask of the icon.
            public IntPtr hbmColor;    // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this
            // structure defines a black and white icon. The AND bitmask of hbmMask is applied with the SRCAND
            // flag to the destination; subsequently, the color bitmap is applied (using XOR) to the
            // destination by using the SRCINVERT flag.
        }

        [DllImport("user32.dll")]
        static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport("user32.dll")]
        static extern IntPtr CreateIconIndirect([In] ref ICONINFO piconinfo);

        public static bool encode_BMP_To_PNG(ref byte[] bmp,ref byte[] png)
        {
            if (bmp != null)
            {
                Bitmap bm = new Bitmap(new MemoryStream(bmp));
                MemoryStream dest = new MemoryStream();
                bm.Save(dest, ImageFormat.Png);
                png = new byte[dest.Length];
                dest.Seek(0, SeekOrigin.Begin);
                int size = dest.Read(png, 0, (int)dest.Length);

                return true;
            }

            return false;
        }

        public static bool encode_BMP_To_JPEG(ref byte[] bmp, ref byte[] jpeg)
        {
            if (bmp != null)
            {
                Bitmap bm = new Bitmap(new MemoryStream(bmp));
                MemoryStream dest = new MemoryStream();
                bm.Save(dest, ImageFormat.Jpeg);
                jpeg = new byte[dest.Length];
                dest.Seek(0, SeekOrigin.Begin);
                int size = dest.Read(jpeg, 0, (int)dest.Length);

                return true;
            }

            return false;
        }

        // Create a cursor from a bitmap.
        public static Cursor BitmapToCursor(Bitmap bmp, int hot_x, int hot_y)
        {
            // Initialize the cursor information.
            ICONINFO icon_info = new ICONINFO();
            IntPtr h_icon = bmp.GetHicon();
            GetIconInfo(h_icon, out icon_info);
            icon_info.xHotspot = hot_x;
            icon_info.yHotspot = hot_y;
            icon_info.fIcon = false;    // Cursor, not icon.

            // Create the cursor.
            IntPtr h_cursor = CreateIconIndirect(ref icon_info);
            return new Cursor(h_cursor);
        }
    }

    class Win32Stuff
    {

        #region Class Variables

        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;

        public const Int32 CURSOR_SHOWING = 0x00000001;

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;         // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies 
            public Int32 xHotspot;     // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot 
            public Int32 yHotspot;     // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot 
            public IntPtr hbmMask;     // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
            public IntPtr hbmColor;    // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this 
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 x;
            public Int32 y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CURSORINFO
        {
            public Int32 cbSize;        // Specifies the size, in bytes, of the structure. 
            public Int32 flags;         // Specifies the cursor state. This parameter can be one of the following values:
            public IntPtr hCursor;          // Handle to the cursor. 
            public POINT ptScreenPos;       // A POINT structure that receives the screen coordinates of the cursor. 
        }

        #endregion


        #region Class Functions

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);


        [DllImport("user32.dll", EntryPoint = "GetCursorInfo")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", EntryPoint = "CopyIcon")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll", EntryPoint = "GetIconInfo")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);


        #endregion
    }

    class Graphics_Utilities
    {

	    string capture_file_name;

	    Bitmap bmpScreenshot;
        Graphics gfxScreenshot;
        Bitmap desktop_with_cursor;


    public Graphics_Utilities()
	    {
		    capture_file_name = "cap_01.bmp";

            bmpScreenshot = null;
            gfxScreenshot = null;

	    }
	    ~Graphics_Utilities(){}

	public void set_Capture_File_Name(string ext_cap_file_name)
	    {
		    if(ext_cap_file_name.Length > 0)
			    capture_file_name = ext_cap_file_name;
		    else
			    Console.Write("\next_cap_file_name empty");
	    }

	public string get_Capture_File_Name()
	    {
		    if(capture_file_name.Length>1)
		    {
			    return capture_file_name;
		    }
		    else
			    return "";
	    }

	public void gdp_Screen_Capture()//IntPtr hwnd)
	    {
            try
            {
                bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size);//, CopyPixelOperation.SourceCopy);
            }
            catch(Exception e)
            {
                Console.WriteLine("gdp_Screen_Capture" + "\n" + e.Message);
            }

	    }

    public Bitmap gdp_CaptureCursor(ref int x, ref int y)
    {
        try
        {
            Bitmap bmp;
            IntPtr hicon;
            Win32Stuff.CURSORINFO ci = new Win32Stuff.CURSORINFO();
            Win32Stuff.ICONINFO icInfo;
            ci.cbSize = Marshal.SizeOf(ci);
            if (Win32Stuff.GetCursorInfo(out ci))
            {
                if (ci.flags == Win32Stuff.CURSOR_SHOWING)
                {
                    hicon = Win32Stuff.CopyIcon(ci.hCursor);
                    if (Win32Stuff.GetIconInfo(hicon, out icInfo))
                    {
                        x = ci.ptScreenPos.x - ((int)icInfo.xHotspot);
                        y = ci.ptScreenPos.y - ((int)icInfo.yHotspot);

                        Icon ic = Icon.FromHandle(hicon);
                        bmp = ic.ToBitmap();
                        return bmp;
                    }
                }
            }

            return null;
        }
        catch(Exception e)
        {
            Console.WriteLine("gdp_CaptureCursor" + "\n" + e.Message);
            return null;
        }

        
    }

    public void gdp_CaptureDesktopWithCursor()
    {
        try
        {
            int cursorX = 0;
            int cursorY = 0;
            Bitmap desktopBMP;
            Bitmap cursorBMP;

            Graphics g;
            Rectangle r;

            desktopBMP = bmpScreenshot;
            cursorBMP = gdp_CaptureCursor(ref cursorX, ref cursorY);
            if (desktopBMP != null)
            {
                if (cursorBMP != null)
                {
                    r = new Rectangle(cursorX, cursorY, cursorBMP.Width, cursorBMP.Height);
                    g = Graphics.FromImage(desktopBMP);
                    g.DrawImage(cursorBMP, r);
                    g.Flush();

                    desktop_with_cursor = desktopBMP;

                    g.Dispose();
                    cursorBMP = null;
                }
                desktopBMP = null;
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("gdp_CaptureDesktopWithCursor"+"\n"+e.Message);
        }


    }

	public void gdp_Clear()
	{
        try
        {
            gfxScreenshot = null;//gfxScreenshot.Dispose();
            bmpScreenshot = null;//bmpScreenshot.Dispose();
            desktop_with_cursor = null;//desktop_with_cursor.Dispose();
        }
        catch(Exception e)
        {
            Console.WriteLine("gdp_Clear"+ "\n" + e.Message);
        }
	}

	public void gdp_Save_To_Bitmap_File()
	    {
            bmpScreenshot.Save(capture_file_name, ImageFormat.Bmp);
	    }

	void gdp_Save_To_PNG_File()
	{
        bmpScreenshot.Save(capture_file_name, ImageFormat.Png);
	}

    public int gdp_Save_PNG_To_Binary(ref byte[] packet)
    {
        MemoryStream s = new MemoryStream();

        bmpScreenshot.Save(s, ImageFormat.Png);

        s.Seek(0, SeekOrigin.Begin);

        packet = s.ToArray();

        return packet.Length;
    }

    public int gdp_Save_with_Cursor_PNG_To_Binary(ref byte[] packet)
    {
        MemoryStream s = new MemoryStream();

        if (desktop_with_cursor != null)
        {
            desktop_with_cursor.Save(s, ImageFormat.Png);
        }

        s.Seek(0, SeekOrigin.Begin);

        packet = s.ToArray();

        return packet.Length;
    }

	void gdp_Save_To_JPEG_File()
	{
        bmpScreenshot.Save(capture_file_name, ImageFormat.Jpeg);
	}

    public int gdp_Save_JPEG_To_Binary(ref byte[] packet)
    {
        try
        {
            MemoryStream s = new MemoryStream();

            bmpScreenshot.Save(s, ImageFormat.Jpeg);

            s.Seek(0, SeekOrigin.Begin);

            packet = s.ToArray();

            return packet.Length;
        }
        catch(Exception e)
        {
            Console.WriteLine("gdp_Save_JPEG_To_Binary"+ " " + e.Message);
            return 0;
        }
    }

    public int gdp_Save_with_Cursor_JPEG_To_Binary(ref byte[] packet)
    {
        try
        {
            MemoryStream s = new MemoryStream();

            if (desktop_with_cursor != null)
            {
                desktop_with_cursor.Save(s, ImageFormat.Jpeg);
            }

            s.Seek(0, SeekOrigin.Begin);

            packet = s.ToArray();

            return packet.Length;
        }
        catch(Exception e)
        {
            Console.WriteLine("gdp_Save_with_Cursor_JPEG_To_Binary" + " " + e.Message);
            return 0;
        }
    }

    
     
    };
}
