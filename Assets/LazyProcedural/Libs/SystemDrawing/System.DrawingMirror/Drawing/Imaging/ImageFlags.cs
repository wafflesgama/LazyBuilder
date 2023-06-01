// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ImageFlags
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the attributes of the pixel data contained in an <see cref="T:System.Drawing.Image" /> object. The <see cref="P:System.Drawing.Image.Flags" /> property returns a member of this enumeration.</summary>
  [Flags]
  public enum ImageFlags
  {
    /// <summary>There is no format information.</summary>
    None = 0,
    /// <summary>The pixel data is scalable.</summary>
    Scalable = 1,
    /// <summary>The pixel data contains alpha information.</summary>
    HasAlpha = 2,
    /// <summary>Specifies that the pixel data has alpha values other than 0 (transparent) and 255 (opaque).</summary>
    HasTranslucent = 4,
    /// <summary>The pixel data is partially scalable, but there are some limitations.</summary>
    PartiallyScalable = 8,
    /// <summary>The pixel data uses an RGB color space.</summary>
    ColorSpaceRgb = 16, // 0x00000010
    /// <summary>The pixel data uses a CMYK color space.</summary>
    ColorSpaceCmyk = 32, // 0x00000020
    /// <summary>The pixel data is grayscale.</summary>
    ColorSpaceGray = 64, // 0x00000040
    /// <summary>Specifies that the image is stored using a YCBCR color space.</summary>
    ColorSpaceYcbcr = 128, // 0x00000080
    /// <summary>Specifies that the image is stored using a YCCK color space.</summary>
    ColorSpaceYcck = 256, // 0x00000100
    /// <summary>Specifies that dots per inch information is stored in the image.</summary>
    HasRealDpi = 4096, // 0x00001000
    /// <summary>Specifies that the pixel size is stored in the image.</summary>
    HasRealPixelSize = 8192, // 0x00002000
    /// <summary>The pixel data is read-only.</summary>
    ReadOnly = 65536, // 0x00010000
    /// <summary>The pixel data can be cached for faster access.</summary>
    Caching = 131072, // 0x00020000
  }
}
