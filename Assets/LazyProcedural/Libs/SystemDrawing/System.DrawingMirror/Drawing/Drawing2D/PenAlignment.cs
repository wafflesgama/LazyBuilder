// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.PenAlignment
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies the alignment of a <see cref="T:System.Drawing.Pen" /> object in relation to the theoretical, zero-width line.</summary>
  public enum PenAlignment
  {
    /// <summary>Specifies that the <see cref="T:System.Drawing.Pen" /> object is centered over the theoretical line.</summary>
    Center,
    /// <summary>Specifies that the <see cref="T:System.Drawing.Pen" /> is positioned on the inside of the theoretical line.</summary>
    Inset,
    /// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned on the outside of the theoretical line.</summary>
    Outset,
    /// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned to the left of the theoretical line.</summary>
    Left,
    /// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned to the right of the theoretical line.</summary>
    Right,
  }
}
