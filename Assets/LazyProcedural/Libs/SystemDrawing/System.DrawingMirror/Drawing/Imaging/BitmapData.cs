// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.BitmapData
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the attributes of a bitmap image. The <see cref="T:System.Drawing.Imaging.BitmapData" /> class is used by the <see cref="Overload:System.Drawing.Bitmap.LockBits" /> and <see cref="M:System.Drawing.Bitmap.UnlockBits(System.Drawing.Imaging.BitmapData)" /> methods of the <see cref="T:System.Drawing.Bitmap" /> class. Not inheritable.</summary>
  [StructLayout(LayoutKind.Sequential)]
  public sealed class BitmapData
  {
    private int width;
    private int height;
    private int stride;
    private int pixelFormat;
    private IntPtr scan0;
    private int reserved;

    /// <summary>Gets or sets the pixel width of the <see cref="T:System.Drawing.Bitmap" /> object. This can also be thought of as the number of pixels in one scan line.</summary>
    /// <returns>The pixel width of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
    public int Width
    {
      get => this.width;
      set => this.width = value;
    }

    /// <summary>Gets or sets the pixel height of the <see cref="T:System.Drawing.Bitmap" /> object. Also sometimes referred to as the number of scan lines.</summary>
    /// <returns>The pixel height of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
    public int Height
    {
      get => this.height;
      set => this.height = value;
    }

    /// <summary>Gets or sets the stride width (also called scan width) of the <see cref="T:System.Drawing.Bitmap" /> object.</summary>
    /// <returns>The stride width, in bytes, of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
    public int Stride
    {
      get => this.stride;
      set => this.stride = value;
    }

    /// <summary>Gets or sets the format of the pixel information in the <see cref="T:System.Drawing.Bitmap" /> object that returned this <see cref="T:System.Drawing.Imaging.BitmapData" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Imaging.PixelFormat" /> that specifies the format of the pixel information in the associated <see cref="T:System.Drawing.Bitmap" /> object.</returns>
    public PixelFormat PixelFormat
    {
      get => (PixelFormat) this.pixelFormat;
      set
      {
        switch (value)
        {
          case PixelFormat.Undefined:
          case PixelFormat.Max:
          case PixelFormat.Indexed:
          case PixelFormat.Gdi:
          case PixelFormat.Format16bppRgb555:
          case PixelFormat.Format16bppRgb565:
          case PixelFormat.Format24bppRgb:
          case PixelFormat.Format32bppRgb:
          case PixelFormat.Format1bppIndexed:
          case PixelFormat.Format4bppIndexed:
          case PixelFormat.Format8bppIndexed:
          case PixelFormat.Alpha:
          case PixelFormat.Format16bppArgb1555:
          case PixelFormat.PAlpha:
          case PixelFormat.Format32bppPArgb:
          case PixelFormat.Extended:
          case PixelFormat.Format16bppGrayScale:
          case PixelFormat.Format48bppRgb:
          case PixelFormat.Format64bppPArgb:
          case PixelFormat.Canonical:
          case PixelFormat.Format32bppArgb:
          case PixelFormat.Format64bppArgb:
            this.pixelFormat = (int) value;
            break;
          default:
            throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (PixelFormat));
        }
      }
    }

    /// <summary>Gets or sets the address of the first pixel data in the bitmap. This can also be thought of as the first scan line in the bitmap.</summary>
    /// <returns>The address of the first pixel data in the bitmap.</returns>
    public IntPtr Scan0
    {
      get => this.scan0;
      set => this.scan0 = value;
    }

    /// <summary>Reserved. Do not use.</summary>
    /// <returns>Reserved. Do not use.</returns>
    public int Reserved
    {
      get => this.reserved;
      set => this.reserved = value;
    }
  }
}
