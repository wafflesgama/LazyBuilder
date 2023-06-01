// Decompiled with JetBrains decompiler
// Type: System.Drawing.StringFormatFlags
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Specifies the display and layout information for text strings.</summary>
  [Flags]
  public enum StringFormatFlags
  {
    /// <summary>Text is displayed from right to left.</summary>
    DirectionRightToLeft = 1,
    /// <summary>Text is vertically aligned.</summary>
    DirectionVertical = 2,
    /// <summary>Parts of characters are allowed to overhang the string's layout rectangle. By default, characters are repositioned to avoid any overhang.</summary>
    FitBlackBox = 4,
    /// <summary>Control characters such as the left-to-right mark are shown in the output with a representative glyph.</summary>
    DisplayFormatControl = 32, // 0x00000020
    /// <summary>Fallback to alternate fonts for characters not supported in the requested font is disabled. Any missing characters are displayed with the fonts missing glyph, usually an open square.</summary>
    NoFontFallback = 1024, // 0x00000400
    /// <summary>Includes the trailing space at the end of each line. By default the boundary rectangle returned by the <see cref="Overload:System.Drawing.Graphics.MeasureString" /> method excludes the space at the end of each line. Set this flag to include that space in measurement.</summary>
    MeasureTrailingSpaces = 2048, // 0x00000800
    /// <summary>Text wrapping between lines when formatting within a rectangle is disabled. This flag is implied when a point is passed instead of a rectangle, or when the specified rectangle has a zero line length.</summary>
    NoWrap = 4096, // 0x00001000
    /// <summary>Only entire lines are laid out in the formatting rectangle. By default layout continues until the end of the text, or until no more lines are visible as a result of clipping, whichever comes first. Note that the default settings allow the last line to be partially obscured by a formatting rectangle that is not a whole multiple of the line height. To ensure that only whole lines are seen, specify this value and be careful to provide a formatting rectangle at least as tall as the height of one line.</summary>
    LineLimit = 8192, // 0x00002000
    /// <summary>Overhanging parts of glyphs, and unwrapped text reaching outside the formatting rectangle are allowed to show. By default all text and glyph parts reaching outside the formatting rectangle are clipped.</summary>
    NoClip = 16384, // 0x00004000
  }
}
