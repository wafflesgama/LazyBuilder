// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ColorAdjustType
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies which GDI+ objects use color adjustment information.</summary>
  public enum ColorAdjustType
  {
    /// <summary>Color adjustment information that is used by all GDI+ objects that do not have their own color adjustment information.</summary>
    Default,
    /// <summary>Color adjustment information for <see cref="T:System.Drawing.Bitmap" /> objects.</summary>
    Bitmap,
    /// <summary>Color adjustment information for <see cref="T:System.Drawing.Brush" /> objects.</summary>
    Brush,
    /// <summary>Color adjustment information for <see cref="T:System.Drawing.Pen" /> objects.</summary>
    Pen,
    /// <summary>Color adjustment information for text.</summary>
    Text,
    /// <summary>The number of types specified.</summary>
    Count,
    /// <summary>The number of types specified.</summary>
    Any,
  }
}
