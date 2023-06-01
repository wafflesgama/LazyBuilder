// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrintPageEventArgs
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  /// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
  public class PrintPageEventArgs : EventArgs
  {
    private bool hasMorePages;
    private bool cancel;
    private Graphics graphics;
    private readonly Rectangle marginBounds;
    private readonly Rectangle pageBounds;
    private readonly PageSettings pageSettings;
    internal bool CopySettingsToDevMode = true;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> class.</summary>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the item.</param>
    /// <param name="marginBounds">The area between the margins.</param>
    /// <param name="pageBounds">The total area of the paper.</param>
    /// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> for the page.</param>
    public PrintPageEventArgs(
      Graphics graphics,
      Rectangle marginBounds,
      Rectangle pageBounds,
      PageSettings pageSettings)
    {
      this.graphics = graphics;
      this.marginBounds = marginBounds;
      this.pageBounds = pageBounds;
      this.pageSettings = pageSettings;
    }

    /// <summary>Gets or sets a value indicating whether the print job should be canceled.</summary>
    /// <returns>
    /// <see langword="true" /> if the print job should be canceled; otherwise, <see langword="false" />.</returns>
    public bool Cancel
    {
      get => this.cancel;
      set => this.cancel = value;
    }

    /// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the page.</summary>
    /// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the page.</returns>
    public Graphics Graphics => this.graphics;

    /// <summary>Gets or sets a value indicating whether an additional page should be printed.</summary>
    /// <returns>
    /// <see langword="true" /> if an additional page should be printed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    public bool HasMorePages
    {
      get => this.hasMorePages;
      set => this.hasMorePages = value;
    }

    /// <summary>Gets the rectangular area that represents the portion of the page inside the margins.</summary>
    /// <returns>The rectangular area, measured in hundredths of an inch, that represents the portion of the page inside the margins.</returns>
    public Rectangle MarginBounds => this.marginBounds;

    /// <summary>Gets the rectangular area that represents the total area of the page.</summary>
    /// <returns>The rectangular area that represents the total area of the page.</returns>
    public Rectangle PageBounds => this.pageBounds;

    /// <summary>Gets the page settings for the current page.</summary>
    /// <returns>The page settings for the current page.</returns>
    public PageSettings PageSettings => this.pageSettings;

    internal void Dispose() => this.graphics.Dispose();

    internal void SetGraphics(Graphics value) => this.graphics = value;
  }
}
