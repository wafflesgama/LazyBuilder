// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.CompositingMode
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies how the source colors are combined with the background colors.</summary>
  public enum CompositingMode
  {
    /// <summary>Specifies that when a color is rendered, it is blended with the background color. The blend is determined by the alpha component of the color being rendered.</summary>
    SourceOver,
    /// <summary>Specifies that when a color is rendered, it overwrites the background color.</summary>
    SourceCopy,
  }
}
