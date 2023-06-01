// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.MetafileFrameUnit
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the unit of measurement for the rectangle used to size and position a metafile. This is specified during the creation of the <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
  public enum MetafileFrameUnit
  {
    /// <summary>The unit of measurement is 1 pixel.</summary>
    Pixel = 2,
    /// <summary>The unit of measurement is 1 printer's point.</summary>
    Point = 3,
    /// <summary>The unit of measurement is 1 inch.</summary>
    Inch = 4,
    /// <summary>The unit of measurement is 1/300 of an inch.</summary>
    Document = 5,
    /// <summary>The unit of measurement is 1 millimeter.</summary>
    Millimeter = 6,
    /// <summary>The unit of measurement is 0.01 millimeter. Provided for compatibility with GDI.</summary>
    GdiCompatible = 7,
  }
}
