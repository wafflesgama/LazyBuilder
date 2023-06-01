// Decompiled with JetBrains decompiler
// Type: System.Drawing.GraphicsUnit
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Specifies the unit of measure for the given data.</summary>
  public enum GraphicsUnit
  {
    /// <summary>Specifies the world coordinate system unit as the unit of measure.</summary>
    World,
    /// <summary>Specifies the unit of measure of the display device. Typically pixels for video displays, and 1/100 inch for printers.</summary>
    Display,
    /// <summary>Specifies a device pixel as the unit of measure.</summary>
    Pixel,
    /// <summary>Specifies a printer's point (1/72 inch) as the unit of measure.</summary>
    Point,
    /// <summary>Specifies the inch as the unit of measure.</summary>
    Inch,
    /// <summary>Specifies the document unit (1/300 inch) as the unit of measure.</summary>
    Document,
    /// <summary>Specifies the millimeter as the unit of measure.</summary>
    Millimeter,
  }
}
