// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.IntNativeMethods
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
  internal class IntNativeMethods
  {
    public const int MaxTextLengthInWin9x = 8192;
    public const int DT_TOP = 0;
    public const int DT_LEFT = 0;
    public const int DT_CENTER = 1;
    public const int DT_RIGHT = 2;
    public const int DT_VCENTER = 4;
    public const int DT_BOTTOM = 8;
    public const int DT_WORDBREAK = 16;
    public const int DT_SINGLELINE = 32;
    public const int DT_EXPANDTABS = 64;
    public const int DT_TABSTOP = 128;
    public const int DT_NOCLIP = 256;
    public const int DT_EXTERNALLEADING = 512;
    public const int DT_CALCRECT = 1024;
    public const int DT_NOPREFIX = 2048;
    public const int DT_INTERNAL = 4096;
    public const int DT_EDITCONTROL = 8192;
    public const int DT_PATH_ELLIPSIS = 16384;
    public const int DT_END_ELLIPSIS = 32768;
    public const int DT_MODIFYSTRING = 65536;
    public const int DT_RTLREADING = 131072;
    public const int DT_WORD_ELLIPSIS = 262144;
    public const int DT_NOFULLWIDTHCHARBREAK = 524288;
    public const int DT_HIDEPREFIX = 1048576;
    public const int DT_PREFIXONLY = 2097152;
    public const int DIB_RGB_COLORS = 0;
    public const int BI_BITFIELDS = 3;
    public const int BI_RGB = 0;
    public const int BITMAPINFO_MAX_COLORSIZE = 256;
    public const int SPI_GETICONTITLELOGFONT = 31;
    public const int SPI_GETNONCLIENTMETRICS = 41;
    public const int DEFAULT_GUI_FONT = 17;
    public const int HOLLOW_BRUSH = 5;
    public const int BITSPIXEL = 12;
    public const int ALTERNATE = 1;
    public const int WINDING = 2;
    public const int SRCCOPY = 13369376;
    public const int SRCPAINT = 15597702;
    public const int SRCAND = 8913094;
    public const int SRCINVERT = 6684742;
    public const int SRCERASE = 4457256;
    public const int NOTSRCCOPY = 3342344;
    public const int NOTSRCERASE = 1114278;
    public const int MERGECOPY = 12583114;
    public const int MERGEPAINT = 12255782;
    public const int PATCOPY = 15728673;
    public const int PATPAINT = 16452105;
    public const int PATINVERT = 5898313;
    public const int DSTINVERT = 5570569;
    public const int BLACKNESS = 66;
    public const int WHITENESS = 16711778;
    public const int CAPTUREBLT = 1073741824;
    public const int FW_DONTCARE = 0;
    public const int FW_NORMAL = 400;
    public const int FW_BOLD = 700;
    public const int ANSI_CHARSET = 0;
    public const int DEFAULT_CHARSET = 1;
    public const int OUT_DEFAULT_PRECIS = 0;
    public const int OUT_TT_PRECIS = 4;
    public const int OUT_TT_ONLY_PRECIS = 7;
    public const int CLIP_DEFAULT_PRECIS = 0;
    public const int DEFAULT_QUALITY = 0;
    public const int DRAFT_QUALITY = 1;
    public const int PROOF_QUALITY = 2;
    public const int NONANTIALIASED_QUALITY = 3;
    public const int ANTIALIASED_QUALITY = 4;
    public const int CLEARTYPE_QUALITY = 5;
    public const int CLEARTYPE_NATURAL_QUALITY = 6;
    public const int OBJ_PEN = 1;
    public const int OBJ_BRUSH = 2;
    public const int OBJ_DC = 3;
    public const int OBJ_METADC = 4;
    public const int OBJ_FONT = 6;
    public const int OBJ_BITMAP = 7;
    public const int OBJ_MEMDC = 10;
    public const int OBJ_EXTPEN = 11;
    public const int OBJ_ENHMETADC = 12;
    public const int BS_SOLID = 0;
    public const int BS_HATCHED = 2;
    public const int CP_ACP = 0;
    public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
    public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
    public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
    public const int FORMAT_MESSAGE_DEFAULT = 4608;

    public enum RegionFlags
    {
      ERROR,
      NULLREGION,
      SIMPLEREGION,
      COMPLEXREGION,
    }

    public struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;

      public RECT(int left, int top, int right, int bottom)
      {
        this.left = left;
        this.top = top;
        this.right = right;
        this.bottom = bottom;
      }

      public RECT(Rectangle r)
      {
        this.left = r.Left;
        this.top = r.Top;
        this.right = r.Right;
        this.bottom = r.Bottom;
      }

      public static IntNativeMethods.RECT FromXYWH(int x, int y, int width, int height) => new IntNativeMethods.RECT(x, y, x + width, y + height);

      public Size Size => new Size(this.right - this.left, this.bottom - this.top);

      public Rectangle ToRectangle() => new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
      public int x;
      public int y;

      public POINT()
      {
      }

      public POINT(int x, int y)
      {
        this.x = x;
        this.y = y;
      }

      public Point ToPoint() => new Point(this.x, this.y);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class DRAWTEXTPARAMS
    {
      private int cbSize = Marshal.SizeOf(typeof (IntNativeMethods.DRAWTEXTPARAMS));
      public int iTabLength;
      public int iLeftMargin;
      public int iRightMargin;
      public int uiLengthDrawn;

      public DRAWTEXTPARAMS()
      {
      }

      public DRAWTEXTPARAMS(IntNativeMethods.DRAWTEXTPARAMS original)
      {
        this.iLeftMargin = original.iLeftMargin;
        this.iRightMargin = original.iRightMargin;
        this.iTabLength = original.iTabLength;
      }

      public DRAWTEXTPARAMS(int leftMargin, int rightMargin)
      {
        this.iLeftMargin = leftMargin;
        this.iRightMargin = rightMargin;
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class LOGBRUSH
    {
      public int lbStyle;
      public int lbColor;
      public int lbHatch;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class LOGFONT
    {
      public int lfHeight;
      public int lfWidth;
      public int lfEscapement;
      public int lfOrientation;
      public int lfWeight;
      public byte lfItalic;
      public byte lfUnderline;
      public byte lfStrikeOut;
      public byte lfCharSet;
      public byte lfOutPrecision;
      public byte lfClipPrecision;
      public byte lfQuality;
      public byte lfPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string lfFaceName;

      public LOGFONT()
      {
      }

      public LOGFONT(IntNativeMethods.LOGFONT lf)
      {
        this.lfHeight = lf.lfHeight;
        this.lfWidth = lf.lfWidth;
        this.lfEscapement = lf.lfEscapement;
        this.lfOrientation = lf.lfOrientation;
        this.lfWeight = lf.lfWeight;
        this.lfItalic = lf.lfItalic;
        this.lfUnderline = lf.lfUnderline;
        this.lfStrikeOut = lf.lfStrikeOut;
        this.lfCharSet = lf.lfCharSet;
        this.lfOutPrecision = lf.lfOutPrecision;
        this.lfClipPrecision = lf.lfClipPrecision;
        this.lfQuality = lf.lfQuality;
        this.lfPitchAndFamily = lf.lfPitchAndFamily;
        this.lfFaceName = lf.lfFaceName;
      }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TEXTMETRIC
    {
      public int tmHeight;
      public int tmAscent;
      public int tmDescent;
      public int tmInternalLeading;
      public int tmExternalLeading;
      public int tmAveCharWidth;
      public int tmMaxCharWidth;
      public int tmWeight;
      public int tmOverhang;
      public int tmDigitizedAspectX;
      public int tmDigitizedAspectY;
      public char tmFirstChar;
      public char tmLastChar;
      public char tmDefaultChar;
      public char tmBreakChar;
      public byte tmItalic;
      public byte tmUnderlined;
      public byte tmStruckOut;
      public byte tmPitchAndFamily;
      public byte tmCharSet;
    }

    public struct TEXTMETRICA
    {
      public int tmHeight;
      public int tmAscent;
      public int tmDescent;
      public int tmInternalLeading;
      public int tmExternalLeading;
      public int tmAveCharWidth;
      public int tmMaxCharWidth;
      public int tmWeight;
      public int tmOverhang;
      public int tmDigitizedAspectX;
      public int tmDigitizedAspectY;
      public byte tmFirstChar;
      public byte tmLastChar;
      public byte tmDefaultChar;
      public byte tmBreakChar;
      public byte tmItalic;
      public byte tmUnderlined;
      public byte tmStruckOut;
      public byte tmPitchAndFamily;
      public byte tmCharSet;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class SIZE
    {
      public int cx;
      public int cy;

      public SIZE()
      {
      }

      public SIZE(int cx, int cy)
      {
        this.cx = cx;
        this.cy = cy;
      }

      public Size ToSize() => new Size(this.cx, this.cy);
    }
  }
}
