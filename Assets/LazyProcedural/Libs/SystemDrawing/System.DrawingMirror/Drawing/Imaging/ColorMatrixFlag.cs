// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ColorMatrixFlag
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the types of images and colors that will be affected by the color and grayscale adjustment settings of an <see cref="T:System.Drawing.Imaging.ImageAttributes" />.</summary>
  public enum ColorMatrixFlag
  {
    /// <summary>All color values, including gray shades, are adjusted by the same color-adjustment matrix.</summary>
    Default,
    /// <summary>All colors are adjusted, but gray shades are not adjusted. A gray shade is any color that has the same value for its red, green, and blue components.</summary>
    SkipGrays,
    /// <summary>Only gray shades are adjusted.</summary>
    AltGrays,
  }
}
