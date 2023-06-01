// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrintRange
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  /// <summary>Specifies the part of the document to print.</summary>
  [Serializable]
  public enum PrintRange
  {
    /// <summary>All pages are printed.</summary>
    AllPages = 0,
    /// <summary>The selected pages are printed.</summary>
    Selection = 1,
    /// <summary>The pages between <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> and <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> are printed.</summary>
    SomePages = 2,
    /// <summary>The currently displayed page is printed</summary>
    CurrentPage = 4194304, // 0x00400000
  }
}
