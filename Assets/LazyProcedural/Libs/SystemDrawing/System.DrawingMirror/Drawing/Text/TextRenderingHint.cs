// Decompiled with JetBrains decompiler
// Type: System.Drawing.Text.TextRenderingHint
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Text
{
  /// <summary>Specifies the quality of text rendering.</summary>
  public enum TextRenderingHint
  {
    /// <summary>Each character is drawn using its glyph bitmap, with the system default rendering hint. The text will be drawn using whatever font-smoothing settings the user has selected for the system.</summary>
    SystemDefault,
    /// <summary>Each character is drawn using its glyph bitmap. Hinting is used to improve character appearance on stems and curvature.</summary>
    SingleBitPerPixelGridFit,
    /// <summary>Each character is drawn using its glyph bitmap. Hinting is not used.</summary>
    SingleBitPerPixel,
    /// <summary>Each character is drawn using its antialiased glyph bitmap with hinting. Much better quality due to antialiasing, but at a higher performance cost.</summary>
    AntiAliasGridFit,
    /// <summary>Each character is drawn using its antialiased glyph bitmap without hinting. Better quality due to antialiasing. Stem width differences may be noticeable because hinting is turned off.</summary>
    AntiAlias,
    /// <summary>Each character is drawn using its glyph ClearType bitmap with hinting. The highest quality setting. Used to take advantage of ClearType font features.</summary>
    ClearTypeGridFit,
  }
}
