// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.PixelFormat
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the format of the color data for each pixel in the image.</summary>
  public enum PixelFormat
  {
    /// <summary>No pixel format is specified.</summary>
    DontCare = 0,
    /// <summary>The pixel format is undefined.</summary>
    Undefined = 0,
    /// <summary>The maximum value for this enumeration.</summary>
    Max = 15, // 0x0000000F
    /// <summary>The pixel data contains color-indexed values, which means the values are an index to colors in the system color table, as opposed to individual color values.</summary>
    Indexed = 65536, // 0x00010000
    /// <summary>The pixel data contains GDI colors.</summary>
    Gdi = 131072, // 0x00020000
    /// <summary>Specifies that the format is 16 bits per pixel; 5 bits each are used for the red, green, and blue components. The remaining bit is not used.</summary>
    Format16bppRgb555 = 135173, // 0x00021005
    /// <summary>Specifies that the format is 16 bits per pixel; 5 bits are used for the red component, 6 bits are used for the green component, and 5 bits are used for the blue component.</summary>
    Format16bppRgb565 = 135174, // 0x00021006
    /// <summary>Specifies that the format is 24 bits per pixel; 8 bits each are used for the red, green, and blue components.</summary>
    Format24bppRgb = 137224, // 0x00021808
    /// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the red, green, and blue components. The remaining 8 bits are not used.</summary>
    Format32bppRgb = 139273, // 0x00022009
    /// <summary>Specifies that the pixel format is 1 bit per pixel and that it uses indexed color. The color table therefore has two colors in it.</summary>
    Format1bppIndexed = 196865, // 0x00030101
    /// <summary>Specifies that the format is 4 bits per pixel, indexed.</summary>
    Format4bppIndexed = 197634, // 0x00030402
    /// <summary>Specifies that the format is 8 bits per pixel, indexed. The color table therefore has 256 colors in it.</summary>
    Format8bppIndexed = 198659, // 0x00030803
    /// <summary>The pixel data contains alpha values that are not premultiplied.</summary>
    Alpha = 262144, // 0x00040000
    /// <summary>The pixel format is 16 bits per pixel. The color information specifies 32,768 shades of color, of which 5 bits are red, 5 bits are green, 5 bits are blue, and 1 bit is alpha.</summary>
    Format16bppArgb1555 = 397319, // 0x00061007
    /// <summary>The pixel format contains premultiplied alpha values.</summary>
    PAlpha = 524288, // 0x00080000
    /// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components. The red, green, and blue components are premultiplied, according to the alpha component.</summary>
    Format32bppPArgb = 925707, // 0x000E200B
    /// <summary>Reserved.</summary>
    Extended = 1048576, // 0x00100000
    /// <summary>The pixel format is 16 bits per pixel. The color information specifies 65536 shades of gray.</summary>
    Format16bppGrayScale = 1052676, // 0x00101004
    /// <summary>Specifies that the format is 48 bits per pixel; 16 bits each are used for the red, green, and blue components.</summary>
    Format48bppRgb = 1060876, // 0x0010300C
    /// <summary>Specifies that the format is 64 bits per pixel; 16 bits each are used for the alpha, red, green, and blue components. The red, green, and blue components are premultiplied according to the alpha component.</summary>
    Format64bppPArgb = 1851406, // 0x001C400E
    /// <summary>The default pixel format of 32 bits per pixel. The format specifies 24-bit color depth and an 8-bit alpha channel.</summary>
    Canonical = 2097152, // 0x00200000
    /// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components.</summary>
    Format32bppArgb = 2498570, // 0x0026200A
    /// <summary>Specifies that the format is 64 bits per pixel; 16 bits each are used for the alpha, red, green, and blue components.</summary>
    Format64bppArgb = 3424269, // 0x0034400D
  }
}
