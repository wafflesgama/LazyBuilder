// Decompiled with JetBrains decompiler
// Type: System.Drawing.StringTrimming
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Specifies how to trim characters from a string that does not completely fit into a layout shape.</summary>
  public enum StringTrimming
  {
    /// <summary>Specifies no trimming.</summary>
    None,
    /// <summary>Specifies that the text is trimmed to the nearest character.</summary>
    Character,
    /// <summary>Specifies that text is trimmed to the nearest word.</summary>
    Word,
    /// <summary>Specifies that the text is trimmed to the nearest character, and an ellipsis is inserted at the end of a trimmed line.</summary>
    EllipsisCharacter,
    /// <summary>Specifies that text is trimmed to the nearest word, and an ellipsis is inserted at the end of a trimmed line.</summary>
    EllipsisWord,
    /// <summary>The center is removed from trimmed lines and replaced by an ellipsis. The algorithm keeps as much of the last slash-delimited segment of the line as possible.</summary>
    EllipsisPath,
  }
}
