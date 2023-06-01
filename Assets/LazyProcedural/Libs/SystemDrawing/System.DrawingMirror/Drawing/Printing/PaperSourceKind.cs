// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PaperSourceKind
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  /// <summary>Standard paper sources.</summary>
  [Serializable]
  public enum PaperSourceKind
  {
    /// <summary>The upper bin of a printer (or the default bin, if the printer only has one bin).</summary>
    Upper = 1,
    /// <summary>The lower bin of a printer.</summary>
    Lower = 2,
    /// <summary>The middle bin of a printer.</summary>
    Middle = 3,
    /// <summary>Manually fed paper.</summary>
    Manual = 4,
    /// <summary>An envelope.</summary>
    Envelope = 5,
    /// <summary>Manually fed envelope.</summary>
    ManualFeed = 6,
    /// <summary>Automatically fed paper.</summary>
    AutomaticFeed = 7,
    /// <summary>A tractor feed.</summary>
    TractorFeed = 8,
    /// <summary>Small-format paper.</summary>
    SmallFormat = 9,
    /// <summary>Large-format paper.</summary>
    LargeFormat = 10, // 0x0000000A
    /// <summary>The printer's large-capacity bin.</summary>
    LargeCapacity = 11, // 0x0000000B
    /// <summary>A paper cassette.</summary>
    Cassette = 14, // 0x0000000E
    /// <summary>The printer's default input bin.</summary>
    FormSource = 15, // 0x0000000F
    /// <summary>A printer-specific paper source.</summary>
    Custom = 257, // 0x00000101
  }
}
