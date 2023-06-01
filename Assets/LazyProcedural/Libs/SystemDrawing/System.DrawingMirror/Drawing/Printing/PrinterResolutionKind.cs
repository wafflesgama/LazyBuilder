// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrinterResolutionKind
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  /// <summary>Specifies a printer resolution.</summary>
  [Serializable]
  public enum PrinterResolutionKind
  {
    /// <summary>High resolution.</summary>
    High = -4, // 0xFFFFFFFC
    /// <summary>Medium resolution.</summary>
    Medium = -3, // 0xFFFFFFFD
    /// <summary>Low resolution.</summary>
    Low = -2, // 0xFFFFFFFE
    /// <summary>Draft-quality resolution.</summary>
    Draft = -1, // 0xFFFFFFFF
    /// <summary>Custom resolution.</summary>
    Custom = 0,
  }
}
