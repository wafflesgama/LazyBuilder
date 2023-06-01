// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrintController
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Printing
{
  /// <summary>Controls how a document is printed, when printing from a Windows Forms application.</summary>
  public abstract class PrintController
  {
    internal PrintController.SafeDeviceModeHandle modeHandle;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintController" /> class.</summary>
    protected PrintController() => IntSecurity.SafePrinting.Demand();

    /// <summary>Gets a value indicating whether the <see cref="T:System.Drawing.Printing.PrintController" /> is used for print preview.</summary>
    /// <returns>
    /// <see langword="false" /> in all cases.</returns>
    public virtual bool IsPreview => false;

    internal void Print(PrintDocument document)
    {
      IntSecurity.SafePrinting.Demand();
      PrintEventArgs e = new PrintEventArgs(!this.IsPreview ? (document.PrinterSettings.PrintToFile ? PrintAction.PrintToFile : PrintAction.PrintToPrinter) : PrintAction.PrintToPreview);
      document._OnBeginPrint(e);
      if (e.Cancel)
      {
        document._OnEndPrint(e);
      }
      else
      {
        this.OnStartPrint(document, e);
        if (e.Cancel)
        {
          document._OnEndPrint(e);
          this.OnEndPrint(document, e);
        }
        else
        {
          bool flag = true;
          try
          {
            flag = System.Drawing.LocalAppContextSwitches.OptimizePrintPreview ? this.PrintLoopOptimized(document) : this.PrintLoop(document);
          }
          finally
          {
            try
            {
              try
              {
                document._OnEndPrint(e);
                e.Cancel = flag | e.Cancel;
              }
              finally
              {
                this.OnEndPrint(document, e);
              }
            }
            finally
            {
              if (!IntSecurity.HasPermission(IntSecurity.AllPrinting))
              {
                IntSecurity.AllPrinting.Assert();
                document.PrinterSettings.PrintDialogDisplayed = false;
              }
            }
          }
        }
      }
    }

    private bool PrintLoop(PrintDocument document)
    {
      QueryPageSettingsEventArgs e = new QueryPageSettingsEventArgs((PageSettings) document.DefaultPageSettings.Clone());
      PrintPageEventArgs printPageEvent;
      do
      {
        document._OnQueryPageSettings(e);
        if (e.Cancel)
          return true;
        printPageEvent = this.CreatePrintPageEvent(e.PageSettings);
        Graphics graphics = this.OnStartPage(document, printPageEvent);
        printPageEvent.SetGraphics(graphics);
        try
        {
          document._OnPrintPage(printPageEvent);
          this.OnEndPage(document, printPageEvent);
        }
        finally
        {
          printPageEvent.Dispose();
        }
        if (printPageEvent.Cancel)
          return true;
      }
      while (printPageEvent.HasMorePages);
      return false;
    }

    private bool PrintLoopOptimized(PrintDocument document)
    {
      PrintPageEventArgs e1 = (PrintPageEventArgs) null;
      QueryPageSettingsEventArgs e2 = new QueryPageSettingsEventArgs((PageSettings) document.DefaultPageSettings.Clone());
      do
      {
        e2.PageSettingsChanged = false;
        document._OnQueryPageSettings(e2);
        if (e2.Cancel)
          return true;
        if (!e2.PageSettingsChanged)
        {
          if (e1 == null)
            e1 = this.CreatePrintPageEvent(e2.PageSettings);
          else
            e1.CopySettingsToDevMode = false;
          Graphics graphics = this.OnStartPage(document, e1);
          e1.SetGraphics(graphics);
        }
        else
        {
          e1 = this.CreatePrintPageEvent(e2.PageSettings);
          Graphics graphics = this.OnStartPage(document, e1);
          e1.SetGraphics(graphics);
        }
        try
        {
          document._OnPrintPage(e1);
          this.OnEndPage(document, e1);
        }
        finally
        {
          e1.Graphics.Dispose();
          e1.SetGraphics((Graphics) null);
        }
        if (e1.Cancel)
          return true;
      }
      while (e1.HasMorePages);
      return false;
    }

    private PrintPageEventArgs CreatePrintPageEvent(PageSettings pageSettings)
    {
      IntSecurity.AllPrintingAndUnmanagedCode.Assert();
      Rectangle bounds = pageSettings.GetBounds((IntPtr) this.modeHandle);
      return new PrintPageEventArgs((Graphics) null, new Rectangle(pageSettings.Margins.Left, pageSettings.Margins.Top, bounds.Width - (pageSettings.Margins.Left + pageSettings.Margins.Right), bounds.Height - (pageSettings.Margins.Top + pageSettings.Margins.Bottom)), bounds, pageSettings);
    }

    /// <summary>When overridden in a derived class, begins the control sequence that determines when and how to print a document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
    public virtual void OnStartPrint(PrintDocument document, PrintEventArgs e)
    {
      IntSecurity.AllPrintingAndUnmanagedCode.Assert();
      this.modeHandle = (PrintController.SafeDeviceModeHandle) document.PrinterSettings.GetHdevmode(document.DefaultPageSettings);
    }

    /// <summary>When overridden in a derived class, begins the control sequence that determines when and how to print a page of a document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
    public virtual Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e) => (Graphics) null;

    /// <summary>When overridden in a derived class, completes the control sequence that determines when and how to print a page of a document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
    public virtual void OnEndPage(PrintDocument document, PrintPageEventArgs e)
    {
    }

    /// <summary>When overridden in a derived class, completes the control sequence that determines when and how to print a document.</summary>
    /// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
    public virtual void OnEndPrint(PrintDocument document, PrintEventArgs e)
    {
      IntSecurity.UnmanagedCode.Assert();
      if (this.modeHandle == null)
        return;
      this.modeHandle.Close();
    }

    [SecurityCritical]
    internal sealed class SafeDeviceModeHandle : SafeHandle
    {
      private SafeDeviceModeHandle()
        : base(IntPtr.Zero, true)
      {
      }

      internal SafeDeviceModeHandle(IntPtr handle)
        : base(IntPtr.Zero, true)
      {
        this.SetHandle(handle);
      }

      public override bool IsInvalid => this.handle == IntPtr.Zero;

      [SecurityCritical]
      protected override bool ReleaseHandle()
      {
        if (!this.IsInvalid)
          SafeNativeMethods.GlobalFree(new HandleRef((object) this, this.handle));
        this.handle = IntPtr.Zero;
        return true;
      }

      public static implicit operator IntPtr(PrintController.SafeDeviceModeHandle handle) => handle != null ? handle.handle : IntPtr.Zero;

      public static explicit operator PrintController.SafeDeviceModeHandle(IntPtr handle) => new PrintController.SafeDeviceModeHandle(handle);
    }
  }
}
