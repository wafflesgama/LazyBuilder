// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.LineJoin
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies how to join consecutive line or curve segments in a figure (subpath) contained in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
  public enum LineJoin
  {
    /// <summary>Specifies a mitered join. This produces a sharp corner or a clipped corner, depending on whether the length of the miter exceeds the miter limit.</summary>
    Miter,
    /// <summary>Specifies a beveled join. This produces a diagonal corner.</summary>
    Bevel,
    /// <summary>Specifies a circular join. This produces a smooth, circular arc between the lines.</summary>
    Round,
    /// <summary>Specifies a mitered join. This produces a sharp corner or a beveled corner, depending on whether the length of the miter exceeds the miter limit.</summary>
    MiterClipped,
  }
}
