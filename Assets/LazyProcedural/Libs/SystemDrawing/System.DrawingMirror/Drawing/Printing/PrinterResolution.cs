// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrinterResolution
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Globalization;

namespace System.Drawing.Printing
{
  /// <summary>Represents the resolution supported by a printer.</summary>
  [Serializable]
  public class PrinterResolution
  {
    private int x;
    private int y;
    private PrinterResolutionKind kind;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterResolution" /> class.</summary>
    public PrinterResolution() => this.kind = PrinterResolutionKind.Custom;

    internal PrinterResolution(PrinterResolutionKind kind, int x, int y)
    {
      this.kind = kind;
      this.x = x;
      this.y = y;
    }

    /// <summary>Gets or sets the printer resolution.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Printing.PrinterResolutionKind" /> values.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not a member of the <see cref="T:System.Drawing.Printing.PrinterResolutionKind" /> enumeration.</exception>
    public PrinterResolutionKind Kind
    {
      get => this.kind;
      set => this.kind = System.Drawing.ClientUtils.IsEnumValid((Enum) value, (int) value, -4, 0) ? value : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (PrinterResolutionKind));
    }

    /// <summary>Gets the horizontal printer resolution, in dots per inch.</summary>
    /// <returns>The horizontal printer resolution, in dots per inch, if <see cref="P:System.Drawing.Printing.PrinterResolution.Kind" /> is set to <see cref="F:System.Drawing.Printing.PrinterResolutionKind.Custom" />; otherwise, a <see langword="dmPrintQuality" /> value.</returns>
    public int X
    {
      get => this.x;
      set => this.x = value;
    }

    /// <summary>Gets the vertical printer resolution, in dots per inch.</summary>
    /// <returns>The vertical printer resolution, in dots per inch.</returns>
    public int Y
    {
      get => this.y;
      set => this.y = value;
    }

    /// <summary>This member overrides the <see cref="M:System.Object.ToString" /> method.</summary>
    /// <returns>A <see cref="T:System.String" /> that contains information about the <see cref="T:System.Drawing.Printing.PrinterResolution" />.</returns>
    public override string ToString()
    {
      if (this.kind != PrinterResolutionKind.Custom)
        return "[PrinterResolution " + TypeDescriptor.GetConverter(typeof (PrinterResolutionKind)).ConvertToString((object) (int) this.Kind) + "]";
      return "[PrinterResolution X=" + this.X.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " Y=" + this.Y.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "]";
    }
  }
}
