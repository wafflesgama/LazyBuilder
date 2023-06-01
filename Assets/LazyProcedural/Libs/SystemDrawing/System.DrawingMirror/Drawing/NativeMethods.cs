// Decompiled with JetBrains decompiler
// Type: System.Drawing.NativeMethods
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing
{
  internal class NativeMethods
  {
    internal static HandleRef NullHandleRef = new HandleRef((object) null, IntPtr.Zero);
    public const byte PC_NOCOLLAPSE = 4;
    public const int MAX_PATH = 260;
    internal const int SM_REMOTESESSION = 4096;
    internal const int OBJ_DC = 3;
    internal const int OBJ_METADC = 4;
    internal const int OBJ_MEMDC = 10;
    internal const int OBJ_ENHMETADC = 12;
    internal const int DIB_RGB_COLORS = 0;
    internal const int BI_BITFIELDS = 3;
    internal const int BI_RGB = 0;
    internal const int BITMAPINFO_MAX_COLORSIZE = 256;
    internal const int SPI_GETICONTITLELOGFONT = 31;
    internal const int SPI_GETNONCLIENTMETRICS = 41;
    internal const int DEFAULT_GUI_FONT = 17;

    public enum RegionFlags
    {
      ERROR,
      NULLREGION,
      SIMPLEREGION,
      COMPLEXREGION,
    }

    internal struct BITMAPINFO_FLAT
    {
      public int bmiHeader_biSize;
      public int bmiHeader_biWidth;
      public int bmiHeader_biHeight;
      public short bmiHeader_biPlanes;
      public short bmiHeader_biBitCount;
      public int bmiHeader_biCompression;
      public int bmiHeader_biSizeImage;
      public int bmiHeader_biXPelsPerMeter;
      public int bmiHeader_biYPelsPerMeter;
      public int bmiHeader_biClrUsed;
      public int bmiHeader_biClrImportant;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
      public byte[] bmiColors;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class BITMAPINFOHEADER
    {
      public int biSize = 40;
      public int biWidth;
      public int biHeight;
      public short biPlanes;
      public short biBitCount;
      public int biCompression;
      public int biSizeImage;
      public int biXPelsPerMeter;
      public int biYPelsPerMeter;
      public int biClrUsed;
      public int biClrImportant;
    }

    internal struct PALETTEENTRY
    {
      public byte peRed;
      public byte peGreen;
      public byte peBlue;
      public byte peFlags;
    }

    internal struct RGBQUAD
    {
      public byte rgbBlue;
      public byte rgbGreen;
      public byte rgbRed;
      public byte rgbReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class NONCLIENTMETRICS
    {
      public int cbSize = Marshal.SizeOf(typeof (NativeMethods.NONCLIENTMETRICS));
      public int iBorderWidth;
      public int iScrollWidth;
      public int iScrollHeight;
      public int iCaptionWidth;
      public int iCaptionHeight;
      [MarshalAs(UnmanagedType.Struct)]
      public SafeNativeMethods.LOGFONT lfCaptionFont;
      public int iSmCaptionWidth;
      public int iSmCaptionHeight;
      [MarshalAs(UnmanagedType.Struct)]
      public SafeNativeMethods.LOGFONT lfSmCaptionFont;
      public int iMenuWidth;
      public int iMenuHeight;
      [MarshalAs(UnmanagedType.Struct)]
      public SafeNativeMethods.LOGFONT lfMenuFont;
      [MarshalAs(UnmanagedType.Struct)]
      public SafeNativeMethods.LOGFONT lfStatusFont;
      [MarshalAs(UnmanagedType.Struct)]
      public SafeNativeMethods.LOGFONT lfMessageFont;
    }
  }
}
