// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PreviewPrintController
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Printing
{
  /// <summary>Specifies a print controller that displays a document on a screen as a series of images.</summary>
  public class PreviewPrintController : PrintController
  {
    private IList list = (IList) new ArrayList();
    private Graphics graphics;
    private DeviceContext dc;
    private bool antiAlias;

    private void CheckSecurity() => IntSecurity.SafePrinting.Demand();

    /// <summary>Gets a value indicating whether this controller is used for print preview.</summary>
    /// <returns>
    /// <see langword="true" /> in all cases.</returns>
    public override bool IsPreview => true;

    /// <summary>Begins the control sequence that determines when and how to preview a print document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains data about how to print the document.</param>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
    {
      this.CheckSecurity();
      base.OnStartPrint(document, e);
      try
      {
        if (!document.PrinterSettings.IsValid)
          throw new InvalidPrinterException(document.PrinterSettings);
        IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        this.dc = document.PrinterSettings.CreateInformationContext((IntPtr) this.modeHandle);
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
    }

    /// <summary>Begins the control sequence that determines when and how to preview a page in a print document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains data about how to preview a page in the print document. Initially, the <see cref="P:System.Drawing.Printing.PrintPageEventArgs.Graphics" /> property of this parameter will be <see langword="null" />. The value returned from this method will be used to set this property.</param>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
    public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
    {
      this.CheckSecurity();
      base.OnStartPage(document, e);
      try
      {
        IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        if (e.CopySettingsToDevMode)
          e.PageSettings.CopyToHdevmode((IntPtr) this.modeHandle);
        Size size1 = e.PageBounds.Size;
        Size size2 = PrinterUnitConvert.Convert(size1, PrinterUnit.Display, PrinterUnit.HundredthsOfAMillimeter);
        Metafile metafile = new Metafile(this.dc.Hdc, new Rectangle(0, 0, size2.Width, size2.Height), MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusOnly);
        this.list.Add((object) new PreviewPageInfo((Image) metafile, size1));
        PrintPreviewGraphics printPreviewGraphics = new PrintPreviewGraphics(document, e);
        this.graphics = Graphics.FromImage((Image) metafile);
        if (this.graphics != null && document.OriginAtMargins)
        {
          int deviceCaps1 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) this.dc, this.dc.Hdc), 88);
          int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) this.dc, this.dc.Hdc), 90);
          int deviceCaps3 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) this.dc, this.dc.Hdc), 112);
          int deviceCaps4 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) this.dc, this.dc.Hdc), 113);
          this.graphics.TranslateTransform(-(float) (deviceCaps3 * 100 / deviceCaps1), -(float) (deviceCaps4 * 100 / deviceCaps2));
          this.graphics.TranslateTransform((float) document.DefaultPageSettings.Margins.Left, (float) document.DefaultPageSettings.Margins.Top);
        }
        this.graphics.PrintingHelper = (object) printPreviewGraphics;
        if (this.antiAlias)
        {
          this.graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
          this.graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      return this.graphics;
    }

    /// <summary>Completes the control sequence that determines when and how to preview a page in a print document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains data about how to preview a page in the print document.</param>
    public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
    {
      this.CheckSecurity();
      this.graphics.Dispose();
      this.graphics = (Graphics) null;
      base.OnEndPage(document, e);
    }

    /// <summary>Completes the control sequence that determines when and how to preview a print document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains data about how to preview the print document.</param>
    public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
    {
      this.CheckSecurity();
      this.dc.Dispose();
      this.dc = (DeviceContext) null;
      base.OnEndPrint(document, e);
    }

    /// <summary>Captures the pages of a document as a series of images.</summary>
    /// <returns>An array of type <see cref="T:System.Drawing.Printing.PreviewPageInfo" /> that contains the pages of a <see cref="T:System.Drawing.Printing.PrintDocument" /> as a series of images.</returns>
    public PreviewPageInfo[] GetPreviewPageInfo()
    {
      this.CheckSecurity();
      PreviewPageInfo[] previewPageInfo = new PreviewPageInfo[this.list.Count];
      this.list.CopyTo((Array) previewPageInfo, 0);
      return previewPageInfo;
    }

    /// <summary>Gets or sets a value indicating whether to use anti-aliasing when displaying the print preview.</summary>
    /// <returns>
    /// <see langword="true" /> if the print preview uses anti-aliasing; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    public virtual bool UseAntiAlias
    {
      get => this.antiAlias;
      set => this.antiAlias = value;
    }
  }
}
