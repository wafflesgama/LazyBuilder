// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.DashStyle
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies the style of dashed lines drawn with a <see cref="T:System.Drawing.Pen" /> object.</summary>
  public enum DashStyle
  {
    /// <summary>Specifies a solid line.</summary>
    Solid,
    /// <summary>Specifies a line consisting of dashes.</summary>
    Dash,
    /// <summary>Specifies a line consisting of dots.</summary>
    Dot,
    /// <summary>Specifies a line consisting of a repeating pattern of dash-dot.</summary>
    DashDot,
    /// <summary>Specifies a line consisting of a repeating pattern of dash-dot-dot.</summary>
    DashDotDot,
    /// <summary>Specifies a user-defined custom dash style.</summary>
    Custom,
  }
}
