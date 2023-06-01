// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.QueryPageSettingsEventArgs
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  /// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.QueryPageSettings" /> event.</summary>
  public class QueryPageSettingsEventArgs : PrintEventArgs
  {
    private PageSettings pageSettings;
    internal bool PageSettingsChanged;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.QueryPageSettingsEventArgs" /> class.</summary>
    /// <param name="pageSettings">The page settings for the page to be printed.</param>
    public QueryPageSettingsEventArgs(PageSettings pageSettings) => this.pageSettings = pageSettings;

    /// <summary>Gets or sets the page settings for the page to be printed.</summary>
    /// <returns>The page settings for the page to be printed.</returns>
    public PageSettings PageSettings
    {
      get
      {
        this.PageSettingsChanged = true;
        return this.pageSettings;
      }
      set
      {
        if (value == null)
          value = new PageSettings();
        this.pageSettings = value;
        this.PageSettingsChanged = true;
      }
    }
  }
}
