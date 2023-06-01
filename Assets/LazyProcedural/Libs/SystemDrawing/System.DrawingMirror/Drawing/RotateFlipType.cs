// Decompiled with JetBrains decompiler
// Type: System.Drawing.RotateFlipType
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Specifies how much an image is rotated and the axis used to flip the image.</summary>
  public enum RotateFlipType
  {
    /// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
    Rotate180FlipXY = 0,
    /// <summary>Specifies no clockwise rotation and no flipping.</summary>
    RotateNoneFlipNone = 0,
    /// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
    Rotate270FlipXY = 1,
    /// <summary>Specifies a 90-degree clockwise rotation without flipping.</summary>
    Rotate90FlipNone = 1,
    /// <summary>Specifies a 180-degree clockwise rotation without flipping.</summary>
    Rotate180FlipNone = 2,
    /// <summary>Specifies no clockwise rotation followed by a horizontal and vertical flip.</summary>
    RotateNoneFlipXY = 2,
    /// <summary>Specifies a 270-degree clockwise rotation without flipping.</summary>
    Rotate270FlipNone = 3,
    /// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
    Rotate90FlipXY = 3,
    /// <summary>Specifies a 180-degree clockwise rotation followed by a vertical flip.</summary>
    Rotate180FlipY = 4,
    /// <summary>Specifies no clockwise rotation followed by a horizontal flip.</summary>
    RotateNoneFlipX = 4,
    /// <summary>Specifies a 270-degree clockwise rotation followed by a vertical flip.</summary>
    Rotate270FlipY = 5,
    /// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal flip.</summary>
    Rotate90FlipX = 5,
    /// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal flip.</summary>
    Rotate180FlipX = 6,
    /// <summary>Specifies no clockwise rotation followed by a vertical flip.</summary>
    RotateNoneFlipY = 6,
    /// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal flip.</summary>
    Rotate270FlipX = 7,
    /// <summary>Specifies a 90-degree clockwise rotation followed by a vertical flip.</summary>
    Rotate90FlipY = 7,
  }
}
