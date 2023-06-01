// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.StandardPrintController
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Internal;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Printing
{
  /// <summary>Specifies a print controller that sends information to a printer.</summary>
  public class StandardPrintController : PrintController
  {
    private DeviceContext dc;
    private Graphics graphics;

    private void CheckSecurity(PrintDocument document)
    {
      if (document.PrinterSettings.PrintDialogDisplayed)
        System.Drawing.IntSecurity.SafePrinting.Demand();
      else if (document.PrinterSettings.IsDefaultPrinter)
        System.Drawing.IntSecurity.DefaultPrinting.Demand();
      else
        System.Drawing.IntSecurity.AllPrinting.Demand();
    }

    /// <summary>Begins the control sequence that determines when and how to print a document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being printed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains data about how to print the document.</param>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer settings are not valid.</exception>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The native Win32 Application Programming Interface (API) could not start a print job.</exception>
    public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
    {
      this.CheckSecurity(document);
      base.OnStartPrint(document, e);
      if (!document.PrinterSettings.IsValid)
        throw new InvalidPrinterException(document.PrinterSettings);
      this.dc = document.PrinterSettings.CreateDeviceContext((IntPtr) this.modeHandle);
      if (SafeNativeMethods.StartDoc(new HandleRef((object) this.dc, this.dc.Hdc), new SafeNativeMethods.DOCINFO()
      {
        lpszDocName = document.DocumentName,
        lpszOutput = !document.PrinterSettings.PrintToFile ? (string) null : document.PrinterSettings.OutputPort,
        lpszDatatype = (string) null,
        fwType = 0
      }) > 0)
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error != 1223)
        throw new Win32Exception(lastWin32Error);
      e.Cancel = true;
    }

    /// <summary>Begins the control sequence that determines when and how to print a page in a document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being printed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains data about how to print a page in the document. Initially, the <see cref="P:System.Drawing.Printing.PrintPageEventArgs.Graphics" /> property of this parameter will be <see langword="null" />. The value returned from the <see cref="M:System.Drawing.Printing.StandardPrintController.OnStartPage(System.Drawing.Printing.PrintDocument,System.Drawing.Printing.PrintPageEventArgs)" /> method will be used to set this property.</param>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> object that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The native Win32 Application Programming Interface (API) could not prepare the printer driver to accept data.
    /// -or-
    /// The native Win32 API could not update the specified printer or plotter device context (DC) using the specified information.</exception>
    public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
    {
      this.CheckSecurity(document);
      base.OnStartPage(document, e);
      try
      {
        System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        e.PageSettings.CopyToHdevmode((IntPtr) this.modeHandle);
        IntPtr handle = SafeNativeMethods.GlobalLock(new HandleRef((object) this, (IntPtr) this.modeHandle));
        try
        {
          SafeNativeMethods.ResetDC(new HandleRef((object) this.dc, this.dc.Hdc), new HandleRef((object) null, handle));
        }
        finally
        {
          SafeNativeMethods.GlobalUnlock(new HandleRef((object) this, (IntPtr) this.modeHandle));
        }
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      this.graphics = Graphics.FromHdcInternal(this.dc.Hdc);
      if (this.graphics != null && document.OriginAtMargins)
      {
        int deviceCaps1 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) this.dc, this.dc.Hdc), 88);
        int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) this.dc, this.dc.Hdc), 90);
        int deviceCaps3 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) this.dc, this.dc.Hdc), 112);
        int deviceCaps4 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) this.dc, this.dc.Hdc), 113);
        this.graphics.TranslateTransform(-(float) (deviceCaps3 * 100 / deviceCaps1), -(float) (deviceCaps4 * 100 / deviceCaps2));
        this.graphics.TranslateTransform((float) document.DefaultPageSettings.Margins.Left, (float) document.DefaultPageSettings.Margins.Top);
      }
      if (SafeNativeMethods.StartPage(new HandleRef((object) this.dc, this.dc.Hdc)) <= 0)
        throw new Win32Exception();
      return this.graphics;
    }

    /// <summary>Completes the control sequence that determines when and how to print a page of a document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being printed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains data about how to print a page in the document.</param>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The native Win32 Application Programming Interface (API) could not finish writing to a page.</exception>
    public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
    {
      this.CheckSecurity(document);
      System.Drawing.IntSecurity.UnmanagedCode.Assert();
      try
      {
        if (SafeNativeMethods.EndPage(new HandleRef((object) this.dc, this.dc.Hdc)) <= 0)
          throw new Win32Exception();
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
        this.graphics.Dispose();
        this.graphics = (Graphics) null;
      }
      base.OnEndPage(document, e);
    }

    /// <summary>Completes the control sequence that determines when and how to print a document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being printed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains data about how to print the document.</param>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The native Win32 Application Programming Interface (API) could not complete the print job.
    /// -or-
    /// The native Win32 API could not delete the specified device context (DC).</exception>
    public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
    {
      this.CheckSecurity(document);
      System.Drawing.IntSecurity.UnmanagedCode.Assert();
      try
      {
        if (this.dc != null)
        {
          try
          {
            if ((e.Cancel ? SafeNativeMethods.AbortDoc(new HandleRef((object) this.dc, this.dc.Hdc)) : SafeNativeMethods.EndDoc(new HandleRef((object) this.dc, this.dc.Hdc))) <= 0)
              throw new Win32Exception();
          }
          finally
          {
            this.dc.Dispose();
            this.dc = (DeviceContext) null;
          }
        }
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      base.OnEndPrint(document, e);
    }
  }
}
