// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PaperSize
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Globalization;

namespace System.Drawing.Printing
{
  /// <summary>Specifies the size of a piece of paper.</summary>
  [Serializable]
  public class PaperSize
  {
    private PaperKind kind;
    private string name;
    private int width;
    private int height;
    private bool createdByDefaultConstructor;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSize" /> class.</summary>
    public PaperSize()
    {
      this.kind = PaperKind.Custom;
      this.name = string.Empty;
      this.createdByDefaultConstructor = true;
    }

    internal PaperSize(PaperKind kind, string name, int width, int height)
    {
      this.kind = kind;
      this.name = name;
      this.width = width;
      this.height = height;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSize" /> class.</summary>
    /// <param name="name">The name of the paper.</param>
    /// <param name="width">The width of the paper, in hundredths of an inch.</param>
    /// <param name="height">The height of the paper, in hundredths of an inch.</param>
    public PaperSize(string name, int width, int height)
    {
      this.kind = PaperKind.Custom;
      this.name = name;
      this.width = width;
      this.height = height;
    }

    /// <summary>Gets or sets the height of the paper, in hundredths of an inch.</summary>
    /// <returns>The height of the paper, in hundredths of an inch.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />.</exception>
    public int Height
    {
      get => this.height;
      set
      {
        if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
          throw new ArgumentException(System.Drawing.SR.GetString("PSizeNotCustom"));
        this.height = value;
      }
    }

    /// <summary>Gets the type of paper.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Printing.PaperKind" /> values.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />.</exception>
    public PaperKind Kind => this.kind <= PaperKind.PrcEnvelopeNumber10Rotated && this.kind != (PaperKind.Standard10x14 | PaperKind.C65Envelope) && this.kind != (PaperKind.Standard11x17 | PaperKind.C65Envelope) ? this.kind : PaperKind.Custom;

    /// <summary>Gets or sets the name of the type of paper.</summary>
    /// <returns>The name of the type of paper.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />.</exception>
    public string PaperName
    {
      get => this.name;
      set
      {
        if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
          throw new ArgumentException(System.Drawing.SR.GetString("PSizeNotCustom"));
        this.name = value;
      }
    }

    /// <summary>Gets or sets an integer representing one of the <see cref="T:System.Drawing.Printing.PaperSize" /> values or a custom value.</summary>
    /// <returns>An integer representing one of the <see cref="T:System.Drawing.Printing.PaperSize" /> values, or a custom value.</returns>
    public int RawKind
    {
      get => (int) this.kind;
      set => this.kind = (PaperKind) value;
    }

    /// <summary>Gets or sets the width of the paper, in hundredths of an inch.</summary>
    /// <returns>The width of the paper, in hundredths of an inch.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />.</exception>
    public int Width
    {
      get => this.width;
      set
      {
        if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
          throw new ArgumentException(System.Drawing.SR.GetString("PSizeNotCustom"));
        this.width = value;
      }
    }

    /// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PaperSize" /> in string form.</summary>
    /// <returns>A string.</returns>
    public override string ToString() => "[PaperSize " + this.PaperName + " Kind=" + TypeDescriptor.GetConverter(typeof (PaperKind)).ConvertToString((object) (int) this.Kind) + " Height=" + this.Height.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " Width=" + this.Width.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "]";
  }
}
