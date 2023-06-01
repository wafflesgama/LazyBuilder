// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.MetafileType
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies types of metafiles. The <see cref="P:System.Drawing.Imaging.MetafileHeader.Type" /> property returns a member of this enumeration.</summary>
  public enum MetafileType
  {
    /// <summary>Specifies a metafile format that is not recognized in GDI+.</summary>
    Invalid,
    /// <summary>Specifies a WMF (Windows Metafile) file. Such a file contains only GDI records.</summary>
    Wmf,
    /// <summary>Specifies a WMF (Windows Metafile) file that has a placeable metafile header in front of it.</summary>
    WmfPlaceable,
    /// <summary>Specifies an Enhanced Metafile (EMF) file. Such a file contains only GDI records.</summary>
    Emf,
    /// <summary>Specifies an EMF+ file. Such a file contains only GDI+ records and must be displayed by using GDI+. Displaying the records using GDI may cause unpredictable results.</summary>
    EmfPlusOnly,
    /// <summary>Specifies an EMF+ Dual file. Such a file contains GDI+ records along with alternative GDI records and can be displayed by using either GDI or GDI+. Displaying the records using GDI may cause some quality degradation.</summary>
    EmfPlusDual,
  }
}
