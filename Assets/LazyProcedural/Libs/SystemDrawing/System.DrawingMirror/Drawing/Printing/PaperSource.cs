// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PaperSource
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;

namespace System.Drawing.Printing
{
  /// <summary>Specifies the paper tray from which the printer gets paper.</summary>
  [Serializable]
  public class PaperSource
  {
    private string name;
    private PaperSourceKind kind;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSource" /> class.</summary>
    public PaperSource()
    {
      this.kind = PaperSourceKind.Custom;
      this.name = string.Empty;
    }

    internal PaperSource(PaperSourceKind kind, string name)
    {
      this.kind = kind;
      this.name = name;
    }

    /// <summary>Gets the paper source.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values.</returns>
    public PaperSourceKind Kind => this.kind >= (PaperSourceKind) 256 ? PaperSourceKind.Custom : this.kind;

    /// <summary>Gets or sets the integer representing one of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values or a custom value.</summary>
    /// <returns>The integer value representing one of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values or a custom value.</returns>
    public int RawKind
    {
      get => (int) this.kind;
      set => this.kind = (PaperSourceKind) value;
    }

    /// <summary>Gets or sets the name of the paper source.</summary>
    /// <returns>The name of the paper source.</returns>
    public string SourceName
    {
      get => this.name;
      set => this.name = value;
    }

    /// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PaperSource" /> in string form.</summary>
    /// <returns>A string.</returns>
    public override string ToString() => "[PaperSource " + this.SourceName + " Kind=" + TypeDescriptor.GetConverter(typeof (PaperSourceKind)).ConvertToString((object) this.Kind) + "]";
  }
}
