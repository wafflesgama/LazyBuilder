// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.InterpolationMode
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>The <see cref="T:System.Drawing.Drawing2D.InterpolationMode" /> enumeration specifies the algorithm that is used when images are scaled or rotated.</summary>
  public enum InterpolationMode
  {
    /// <summary>Equivalent to the <see cref="F:System.Drawing.Drawing2D.QualityMode.Invalid" /> element of the <see cref="T:System.Drawing.Drawing2D.QualityMode" /> enumeration.</summary>
    Invalid = -1, // 0xFFFFFFFF
    /// <summary>Specifies default mode.</summary>
    Default = 0,
    /// <summary>Specifies low quality interpolation.</summary>
    Low = 1,
    /// <summary>Specifies high quality interpolation.</summary>
    High = 2,
    /// <summary>Specifies bilinear interpolation. No prefiltering is done. This mode is not suitable for shrinking an image below 50 percent of its original size.</summary>
    Bilinear = 3,
    /// <summary>Specifies bicubic interpolation. No prefiltering is done. This mode is not suitable for shrinking an image below 25 percent of its original size.</summary>
    Bicubic = 4,
    /// <summary>Specifies nearest-neighbor interpolation.</summary>
    NearestNeighbor = 5,
    /// <summary>Specifies high-quality, bilinear interpolation. Prefiltering is performed to ensure high-quality shrinking.</summary>
    HighQualityBilinear = 6,
    /// <summary>Specifies high-quality, bicubic interpolation. Prefiltering is performed to ensure high-quality shrinking. This mode produces the highest quality transformed images.</summary>
    HighQualityBicubic = 7,
  }
}
