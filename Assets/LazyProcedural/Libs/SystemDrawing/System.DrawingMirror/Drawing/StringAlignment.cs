// Decompiled with JetBrains decompiler
// Type: System.Drawing.StringAlignment
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Specifies the alignment of a text string relative to its layout rectangle.</summary>
  public enum StringAlignment
  {
    /// <summary>Specifies the text be aligned near the layout. In a left-to-right layout, the near position is left. In a right-to-left layout, the near position is right.</summary>
    Near,
    /// <summary>Specifies that text is aligned in the center of the layout rectangle.</summary>
    Center,
    /// <summary>Specifies that text is aligned far from the origin position of the layout rectangle. In a left-to-right layout, the far position is right. In a right-to-left layout, the far position is left.</summary>
    Far,
  }
}
