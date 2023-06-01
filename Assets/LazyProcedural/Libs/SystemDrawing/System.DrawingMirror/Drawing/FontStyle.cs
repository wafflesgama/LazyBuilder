// Decompiled with JetBrains decompiler
// Type: System.Drawing.FontStyle
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Specifies style information applied to text.</summary>
  [Flags]
  public enum FontStyle
  {
    /// <summary>Normal text.</summary>
    Regular = 0,
    /// <summary>Bold text.</summary>
    Bold = 1,
    /// <summary>Italic text.</summary>
    Italic = 2,
    /// <summary>Underlined text.</summary>
    Underline = 4,
    /// <summary>Text with a line through the middle.</summary>
    Strikeout = 8,
  }
}
