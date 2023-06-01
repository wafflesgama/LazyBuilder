// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrintDocument
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
  /// <summary>Defines a reusable object that sends output to a printer, when printing from a Windows Forms application.</summary>
  [ToolboxItemFilter("System.Drawing.Printing")]
  [DefaultProperty("DocumentName")]
  [System.Drawing.SRDescription("PrintDocumentDesc")]
  [DefaultEvent("PrintPage")]
  public class PrintDocument : Component
  {
    private string documentName = "document";
    private PrintEventHandler beginPrintHandler;
    private PrintEventHandler endPrintHandler;
    private PrintPageEventHandler printPageHandler;
    private QueryPageSettingsEventHandler queryHandler;
    private PrinterSettings printerSettings = new PrinterSettings();
    private PageSettings defaultPageSettings;
    private PrintController printController;
    private bool originAtMargins;
    private bool userSetPageSettings;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintDocument" /> class.</summary>
    public PrintDocument() => this.defaultPageSettings = new PageSettings(this.printerSettings);

    /// <summary>Gets or sets page settings that are used as defaults for all pages to be printed.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PageSettings" /> that specifies the default page settings for the document.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [System.Drawing.SRDescription("PDOCdocumentPageSettingsDescr")]
    public PageSettings DefaultPageSettings
    {
      get => this.defaultPageSettings;
      set
      {
        if (value == null)
          value = new PageSettings();
        this.defaultPageSettings = value;
        this.userSetPageSettings = true;
      }
    }

    /// <summary>Gets or sets the document name to display (for example, in a print status dialog box or printer queue) while printing the document.</summary>
    /// <returns>The document name to display while printing the document. The default is "document".</returns>
    [DefaultValue("document")]
    [System.Drawing.SRDescription("PDOCdocumentNameDescr")]
    public string DocumentName
    {
      get => this.documentName;
      set
      {
        if (value == null)
          value = "";
        this.documentName = value;
      }
    }

    /// <summary>Gets or sets a value indicating whether the position of a graphics object associated with a page is located just inside the user-specified margins or at the top-left corner of the printable area of the page.</summary>
    /// <returns>
    /// <see langword="true" /> if the graphics origin starts at the page margins; <see langword="false" /> if the graphics origin is at the top-left corner of the printable page. The default is <see langword="false" />.</returns>
    [DefaultValue(false)]
    [System.Drawing.SRDescription("PDOCoriginAtMarginsDescr")]
    public bool OriginAtMargins
    {
      get => this.originAtMargins;
      set => this.originAtMargins = value;
    }

    /// <summary>Gets or sets the print controller that guides the printing process.</summary>
    /// <returns>The <see cref="T:System.Drawing.Printing.PrintController" /> that guides the printing process. The default is a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [System.Drawing.SRDescription("PDOCprintControllerDescr")]
    public PrintController PrintController
    {
      get
      {
        System.Drawing.IntSecurity.SafePrinting.Demand();
        if (this.printController == null)
        {
          this.printController = (PrintController) new StandardPrintController();
          new ReflectionPermission(PermissionState.Unrestricted).Assert();
          try
          {
            this.printController = (PrintController) Activator.CreateInstance(Type.GetType("System.Windows.Forms.PrintControllerWithStatusDialog, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new object[1]
            {
              (object) this.printController
            }, (CultureInfo) null);
          }
          catch (TypeLoadException ex)
          {
          }
          catch (TargetInvocationException ex)
          {
          }
          catch (MissingMethodException ex)
          {
          }
          catch (MethodAccessException ex)
          {
          }
          catch (MemberAccessException ex)
          {
          }
          catch (FileNotFoundException ex)
          {
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
        }
        return this.printController;
      }
      set
      {
        System.Drawing.IntSecurity.SafePrinting.Demand();
        this.printController = value;
      }
    }

    /// <summary>Gets or sets the printer that prints the document.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that specifies where and how the document is printed. The default is a <see cref="T:System.Drawing.Printing.PrinterSettings" /> with its properties set to their default values.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [System.Drawing.SRDescription("PDOCprinterSettingsDescr")]
    public PrinterSettings PrinterSettings
    {
      get => this.printerSettings;
      set
      {
        if (value == null)
          value = new PrinterSettings();
        this.printerSettings = value;
        if (this.userSetPageSettings)
          return;
        this.defaultPageSettings = this.printerSettings.DefaultPageSettings;
      }
    }

    /// <summary>Occurs when the <see cref="M:System.Drawing.Printing.PrintDocument.Print" /> method is called and before the first page of the document prints.</summary>
    [System.Drawing.SRDescription("PDOCbeginPrintDescr")]
    public event PrintEventHandler BeginPrint
    {
      add => this.beginPrintHandler += value;
      remove => this.beginPrintHandler -= value;
    }

    /// <summary>Occurs when the last page of the document has printed.</summary>
    [System.Drawing.SRDescription("PDOCendPrintDescr")]
    public event PrintEventHandler EndPrint
    {
      add => this.endPrintHandler += value;
      remove => this.endPrintHandler -= value;
    }

    /// <summary>Occurs when the output to print for the current page is needed.</summary>
    [System.Drawing.SRDescription("PDOCprintPageDescr")]
    public event PrintPageEventHandler PrintPage
    {
      add => this.printPageHandler += value;
      remove => this.printPageHandler -= value;
    }

    /// <summary>Occurs immediately before each <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
    [System.Drawing.SRDescription("PDOCqueryPageSettingsDescr")]
    public event QueryPageSettingsEventHandler QueryPageSettings
    {
      add => this.queryHandler += value;
      remove => this.queryHandler -= value;
    }

    internal void _OnBeginPrint(PrintEventArgs e) => this.OnBeginPrint(e);

    /// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> event. It is called after the <see cref="M:System.Drawing.Printing.PrintDocument.Print" /> method is called and before the first page of the document prints.</summary>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
    protected virtual void OnBeginPrint(PrintEventArgs e)
    {
      if (this.beginPrintHandler == null)
        return;
      this.beginPrintHandler((object) this, e);
    }

    internal void _OnEndPrint(PrintEventArgs e) => this.OnEndPrint(e);

    /// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> event. It is called when the last page of the document has printed.</summary>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
    protected virtual void OnEndPrint(PrintEventArgs e)
    {
      if (this.endPrintHandler == null)
        return;
      this.endPrintHandler((object) this, e);
    }

    internal void _OnPrintPage(PrintPageEventArgs e) => this.OnPrintPage(e);

    /// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event. It is called before a page prints.</summary>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
    protected virtual void OnPrintPage(PrintPageEventArgs e)
    {
      if (this.printPageHandler == null)
        return;
      this.printPageHandler((object) this, e);
    }

    internal void _OnQueryPageSettings(QueryPageSettingsEventArgs e) => this.OnQueryPageSettings(e);

    /// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.QueryPageSettings" /> event. It is called immediately before each <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
    /// <param name="e">A <see cref="T:System.Drawing.Printing.QueryPageSettingsEventArgs" /> that contains the event data.</param>
    protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e)
    {
      if (this.queryHandler == null)
        return;
      this.queryHandler((object) this, e);
    }

    /// <summary>Starts the document's printing process.</summary>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    public void Print()
    {
      if (!this.PrinterSettings.IsDefaultPrinter && !this.PrinterSettings.PrintDialogDisplayed)
        System.Drawing.IntSecurity.AllPrinting.Demand();
      this.PrintController.Print(this);
    }

    /// <summary>Provides information about the print document, in string form.</summary>
    /// <returns>A string.</returns>
    public override string ToString() => "[PrintDocument " + this.DocumentName + "]";
  }
}
