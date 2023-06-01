// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.PaletteFlags
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the type of color data in the system palette. The data can be color data with alpha, grayscale data only, or halftone data.</summary>
  [Flags]
  public enum PaletteFlags
  {
    /// <summary>Alpha data.</summary>
    HasAlpha = 1,
    /// <summary>Grayscale data.</summary>
    GrayScale = 2,
    /// <summary>Halftone data.</summary>
    Halftone = 4,
  }
}
