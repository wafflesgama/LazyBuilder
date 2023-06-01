// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.EmfType
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the nature of the records that are placed in an Enhanced Metafile (EMF) file. This enumeration is used by several constructors in the <see cref="T:System.Drawing.Imaging.Metafile" /> class.</summary>
  public enum EmfType
  {
    /// <summary>Specifies that all the records in the metafile are EMF records, which can be displayed by GDI or GDI+.</summary>
    EmfOnly = 3,
    /// <summary>Specifies that all the records in the metafile are EMF+ records, which can be displayed by GDI+ but not by GDI.</summary>
    EmfPlusOnly = 4,
    /// <summary>Specifies that all EMF+ records in the metafile are associated with an alternate EMF record. Metafiles of type <see cref="F:System.Drawing.Imaging.EmfType.EmfPlusDual" /> can be displayed by GDI or by GDI+.</summary>
    EmfPlusDual = 5,
  }
}
