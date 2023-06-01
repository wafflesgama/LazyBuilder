// Decompiled with JetBrains decompiler
// Type: System.Drawing.StringUnit
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Specifies the units of measure for a text string.</summary>
  public enum StringUnit
  {
    /// <summary>Specifies world units as the unit of measure.</summary>
    World = 0,
    /// <summary>Specifies the device unit as the unit of measure.</summary>
    Display = 1,
    /// <summary>Specifies a pixel as the unit of measure.</summary>
    Pixel = 2,
    /// <summary>Specifies a printer's point (1/72 inch) as the unit of measure.</summary>
    Point = 3,
    /// <summary>Specifies an inch as the unit of measure.</summary>
    Inch = 4,
    /// <summary>Specifies 1/300 of an inch as the unit of measure.</summary>
    Document = 5,
    /// <summary>Specifies a millimeter as the unit of measure</summary>
    Millimeter = 6,
    /// <summary>Specifies a printer's em size of 32 as the unit of measure.</summary>
    Em = 32, // 0x00000020
  }
}
