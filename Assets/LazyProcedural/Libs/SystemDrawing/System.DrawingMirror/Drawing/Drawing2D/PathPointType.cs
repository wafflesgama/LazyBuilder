// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.PathPointType
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies the type of point in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
  public enum PathPointType
  {
    /// <summary>The starting point of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
    Start = 0,
    /// <summary>A line segment.</summary>
    Line = 1,
    /// <summary>A default Bézier curve.</summary>
    Bezier = 3,
    /// <summary>A cubic Bézier curve.</summary>
    Bezier3 = 3,
    /// <summary>A mask point.</summary>
    PathTypeMask = 7,
    /// <summary>The corresponding segment is dashed.</summary>
    DashMode = 16, // 0x00000010
    /// <summary>A path marker.</summary>
    PathMarker = 32, // 0x00000020
    /// <summary>The endpoint of a subpath.</summary>
    CloseSubpath = 128, // 0x00000080
  }
}
