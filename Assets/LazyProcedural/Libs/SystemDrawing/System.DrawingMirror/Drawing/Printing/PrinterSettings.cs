// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrinterSettings
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Drawing.Printing
{
  /// <summary>Specifies information about how a document is printed, including the printer that prints it, when printing from a Windows Forms application.</summary>
  [Serializable]
  public class PrinterSettings : ICloneable
  {
    private const int PADDING_IA64 = 4;
    private string printerName;
    private string driverName = "";
    private string outputPort = "";
    private bool printToFile;
    private bool printDialogDisplayed;
    private short extrabytes;
    private byte[] extrainfo;
    private short copies = -1;
    private Duplex duplex = Duplex.Default;
    private TriState collate = TriState.Default;
    private PageSettings defaultPageSettings;
    private int fromPage;
    private int toPage;
    private int maxPage = 9999;
    private int minPage;
    private PrintRange printRange;
    private short devmodebytes;
    private byte[] cachedDevmode;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings" /> class.</summary>
    public PrinterSettings() => this.defaultPageSettings = new PageSettings(this);

    /// <summary>Gets a value indicating whether the printer supports double-sided printing.</summary>
    /// <returns>
    /// <see langword="true" /> if the printer supports double-sided printing; otherwise, <see langword="false" />.</returns>
    public bool CanDuplex => this.DeviceCapabilities((short) 7, IntPtr.Zero, 0) == 1;

    /// <summary>Gets or sets the number of copies of the document to print.</summary>
    /// <returns>The number of copies to print. The default is 1.</returns>
    /// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.Copies" /> property is less than zero.</exception>
    public short Copies
    {
      get => this.copies != (short) -1 ? this.copies : this.GetModeField(ModeField.Copies, (short) 1);
      set
      {
        if (value < (short) 0)
          throw new ArgumentException(System.Drawing.SR.GetString("InvalidLowBoundArgumentEx", (object) nameof (value), (object) value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (object) 0.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
        System.Drawing.IntSecurity.SafePrinting.Demand();
        this.copies = value;
      }
    }

    /// <summary>Gets or sets a value indicating whether the printed document is collated.</summary>
    /// <returns>
    /// <see langword="true" /> if the printed document is collated; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    public bool Collate
    {
      get => !this.collate.IsDefault ? (bool) this.collate : this.GetModeField(ModeField.Collate, (short) 0) == (short) 1;
      set => this.collate = (TriState) value;
    }

    /// <summary>Gets the default page settings for this printer.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PageSettings" /> that represents the default page settings for this printer.</returns>
    public PageSettings DefaultPageSettings => this.defaultPageSettings;

    internal string DriverName => this.driverName;

    /// <summary>Gets or sets the printer setting for double-sided printing.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Printing.Duplex" /> values. The default is determined by the printer.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.Duplex" /> property is not one of the <see cref="T:System.Drawing.Printing.Duplex" /> values.</exception>
    public Duplex Duplex
    {
      get => this.duplex != Duplex.Default ? this.duplex : (Duplex) this.GetModeField(ModeField.Duplex, (short) 1);
      set => this.duplex = System.Drawing.ClientUtils.IsEnumValid((Enum) value, (int) value, -1, 3) ? value : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (Duplex));
    }

    /// <summary>Gets or sets the page number of the first page to print.</summary>
    /// <returns>The page number of the first page to print.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> property's value is less than zero.</exception>
    public int FromPage
    {
      get => this.fromPage;
      set => this.fromPage = value >= 0 ? value : throw new ArgumentException(System.Drawing.SR.GetString("InvalidLowBoundArgumentEx", (object) nameof (value), (object) value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (object) 0.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
    }

    /// <summary>Gets the names of all printers installed on the computer.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" /> that represents the names of all printers installed on the computer.</returns>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The available printers could not be enumerated.</exception>
    public static PrinterSettings.StringCollection InstalledPrinters
    {
      get
      {
        System.Drawing.IntSecurity.AllPrinting.Demand();
        int level;
        int num1;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
          level = 4;
          num1 = IntPtr.Size != 8 ? IntPtr.Size * 2 + Marshal.SizeOf(typeof (int)) : IntPtr.Size * 2 + Marshal.SizeOf(typeof (int)) + 4;
        }
        else
        {
          level = 5;
          num1 = IntPtr.Size * 2 + Marshal.SizeOf(typeof (int)) * 3;
        }
        System.Drawing.IntSecurity.UnmanagedCode.Assert();
        string[] array;
        try
        {
          int pcbNeeded;
          int pcReturned;
          SafeNativeMethods.EnumPrinters(6, (string) null, level, IntPtr.Zero, 0, out pcbNeeded, out pcReturned);
          IntPtr num2 = Marshal.AllocCoTaskMem(pcbNeeded);
          int num3 = SafeNativeMethods.EnumPrinters(6, (string) null, level, num2, pcbNeeded, out pcbNeeded, out pcReturned);
          array = new string[pcReturned];
          if (num3 == 0)
          {
            Marshal.FreeCoTaskMem(num2);
            throw new Win32Exception();
          }
          for (int index = 0; index < pcReturned; ++index)
          {
            IntPtr ptr = Marshal.ReadIntPtr((IntPtr) checked (unchecked ((long) num2) + (long) (index * num1)));
            array[index] = Marshal.PtrToStringAuto(ptr);
          }
          Marshal.FreeCoTaskMem(num2);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
        return new PrinterSettings.StringCollection(array);
      }
    }

    /// <summary>Gets a value indicating whether the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates the default printer, except when the user explicitly sets <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" />.</summary>
    /// <returns>
    /// <see langword="true" /> if <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> designates the default printer; otherwise, <see langword="false" />.</returns>
    public bool IsDefaultPrinter => this.printerName == null || this.printerName == PrinterSettings.GetDefaultPrinterName();

    /// <summary>Gets a value indicating whether the printer is a plotter.</summary>
    /// <returns>
    /// <see langword="true" /> if the printer is a plotter; <see langword="false" /> if the printer is a raster.</returns>
    public bool IsPlotter => this.GetDeviceCaps(2, 2) == 0;

    /// <summary>Gets a value indicating whether the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates a valid printer.</summary>
    /// <returns>
    /// <see langword="true" /> if the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates a valid printer; otherwise, <see langword="false" />.</returns>
    public bool IsValid => this.DeviceCapabilities((short) 18, IntPtr.Zero, -1) != -1;

    /// <summary>Gets the angle, in degrees, that the portrait orientation is rotated to produce the landscape orientation.</summary>
    /// <returns>The angle, in degrees, that the portrait orientation is rotated to produce the landscape orientation.</returns>
    public int LandscapeAngle => this.DeviceCapabilities((short) 17, IntPtr.Zero, 0);

    /// <summary>Gets the maximum number of copies that the printer enables the user to print at a time.</summary>
    /// <returns>The maximum number of copies that the printer enables the user to print at a time.</returns>
    public int MaximumCopies => this.DeviceCapabilities((short) 18, IntPtr.Zero, 1);

    /// <summary>Gets or sets the maximum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</summary>
    /// <returns>The maximum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</returns>
    /// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.MaximumPage" /> property is less than zero.</exception>
    public int MaximumPage
    {
      get => this.maxPage;
      set => this.maxPage = value >= 0 ? value : throw new ArgumentException(System.Drawing.SR.GetString("InvalidLowBoundArgumentEx", (object) nameof (value), (object) value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (object) 0.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
    }

    /// <summary>Gets or sets the minimum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</summary>
    /// <returns>The minimum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</returns>
    /// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.MinimumPage" /> property is less than zero.</exception>
    public int MinimumPage
    {
      get => this.minPage;
      set => this.minPage = value >= 0 ? value : throw new ArgumentException(System.Drawing.SR.GetString("InvalidLowBoundArgumentEx", (object) nameof (value), (object) value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (object) 0.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
    }

    internal string OutputPort
    {
      get => this.outputPort;
      set => this.outputPort = value;
    }

    /// <summary>Gets or sets the file name, when printing to a file.</summary>
    /// <returns>The file name, when printing to a file.</returns>
    public string PrintFileName
    {
      get
      {
        string outputPort = this.OutputPort;
        if (!string.IsNullOrEmpty(outputPort))
          System.Drawing.IntSecurity.DemandReadFileIO(outputPort);
        return outputPort;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          throw new ArgumentNullException(value);
        System.Drawing.IntSecurity.DemandWriteFileIO(value);
        this.OutputPort = value;
      }
    }

    /// <summary>Gets the paper sizes that are supported by this printer.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> that represents the paper sizes that are supported by this printer.</returns>
    public PrinterSettings.PaperSizeCollection PaperSizes => new PrinterSettings.PaperSizeCollection(this.Get_PaperSizes());

    /// <summary>Gets the paper source trays that are available on the printer.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> that represents the paper source trays that are available on this printer.</returns>
    public PrinterSettings.PaperSourceCollection PaperSources => new PrinterSettings.PaperSourceCollection(this.Get_PaperSources());

    internal bool PrintDialogDisplayed
    {
      get => this.printDialogDisplayed;
      set
      {
        System.Drawing.IntSecurity.AllPrinting.Demand();
        this.printDialogDisplayed = value;
      }
    }

    /// <summary>Gets or sets the page numbers that the user has specified to be printed.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Printing.PrintRange" /> values.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.PrintRange" /> property is not one of the <see cref="T:System.Drawing.Printing.PrintRange" /> values.</exception>
    public PrintRange PrintRange
    {
      get => this.printRange;
      set => this.printRange = Enum.IsDefined(typeof (PrintRange), (object) value) ? value : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (PrintRange));
    }

    /// <summary>Gets or sets a value indicating whether the printing output is sent to a file instead of a port.</summary>
    /// <returns>
    /// <see langword="true" /> if the printing output is sent to a file; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    public bool PrintToFile
    {
      get => this.printToFile;
      set => this.printToFile = value;
    }

    /// <summary>Gets or sets the name of the printer to use.</summary>
    /// <returns>The name of the printer to use.</returns>
    public string PrinterName
    {
      get
      {
        System.Drawing.IntSecurity.AllPrinting.Demand();
        return this.PrinterNameInternal;
      }
      set
      {
        System.Drawing.IntSecurity.AllPrinting.Demand();
        this.PrinterNameInternal = value;
      }
    }

    private string PrinterNameInternal
    {
      get => this.printerName == null ? PrinterSettings.GetDefaultPrinterName() : this.printerName;
      set
      {
        this.cachedDevmode = (byte[]) null;
        this.extrainfo = (byte[]) null;
        this.printerName = value;
      }
    }

    /// <summary>Gets all the resolutions that are supported by this printer.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> that represents the resolutions that are supported by this printer.</returns>
    public PrinterSettings.PrinterResolutionCollection PrinterResolutions => new PrinterSettings.PrinterResolutionCollection(this.Get_PrinterResolutions());

    /// <summary>Returns a value indicating whether the printer supports printing the specified image format.</summary>
    /// <param name="imageFormat">An <see cref="T:System.Drawing.Imaging.ImageFormat" /> to print.</param>
    /// <returns>
    /// <see langword="true" /> if the printer supports printing the specified image format; otherwise, <see langword="false" />.</returns>
    public bool IsDirectPrintingSupported(ImageFormat imageFormat)
    {
      bool flag = false;
      if (imageFormat.Equals((object) ImageFormat.Jpeg) || imageFormat.Equals((object) ImageFormat.Png))
      {
        int inData = imageFormat.Equals((object) ImageFormat.Jpeg) ? 4119 : 4120;
        int outData = 0;
        DeviceContext informationContext = this.CreateInformationContext(this.DefaultPageSettings);
        HandleRef hDC = new HandleRef((object) informationContext, informationContext.Hdc);
        try
        {
          flag = SafeNativeMethods.ExtEscape(hDC, 8, Marshal.SizeOf(typeof (int)), ref inData, 0, out outData) > 0;
        }
        finally
        {
          informationContext.Dispose();
        }
      }
      return flag;
    }

    /// <summary>Gets a value indicating whether the printer supports printing the specified image file.</summary>
    /// <param name="image">The image to print.</param>
    /// <returns>
    /// <see langword="true" /> if the printer supports printing the specified image; otherwise, <see langword="false" />.</returns>
    public bool IsDirectPrintingSupported(Image image)
    {
      bool flag = false;
      if (image.RawFormat.Equals((object) ImageFormat.Jpeg) || image.RawFormat.Equals((object) ImageFormat.Png))
      {
        MemoryStream memoryStream = new MemoryStream();
        try
        {
          image.Save((Stream) memoryStream, image.RawFormat);
          memoryStream.Position = 0L;
          using (BufferedStream bufferedStream = new BufferedStream((Stream) memoryStream))
          {
            int length = (int) bufferedStream.Length;
            byte[] numArray = new byte[length];
            bufferedStream.Read(numArray, 0, length);
            int inData = image.RawFormat.Equals((object) ImageFormat.Jpeg) ? 4119 : 4120;
            int outData = 0;
            DeviceContext informationContext = this.CreateInformationContext(this.DefaultPageSettings);
            HandleRef hDC = new HandleRef((object) informationContext, informationContext.Hdc);
            try
            {
              if (SafeNativeMethods.ExtEscape(hDC, 8, Marshal.SizeOf(typeof (int)), ref inData, 0, out outData) > 0)
                flag = SafeNativeMethods.ExtEscape(hDC, inData, length, numArray, Marshal.SizeOf(typeof (int)), out outData) > 0 && outData == 1;
            }
            finally
            {
              informationContext.Dispose();
            }
          }
        }
        finally
        {
          memoryStream.Close();
        }
      }
      return flag;
    }

    /// <summary>Gets a value indicating whether this printer supports color printing.</summary>
    /// <returns>
    /// <see langword="true" /> if this printer supports color; otherwise, <see langword="false" />.</returns>
    public bool SupportsColor => this.GetDeviceCaps(12, 1) > 1;

    /// <summary>Gets or sets the number of the last page to print.</summary>
    /// <returns>The number of the last page to print.</returns>
    /// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> property is less than zero.</exception>
    public int ToPage
    {
      get => this.toPage;
      set => this.toPage = value >= 0 ? value : throw new ArgumentException(System.Drawing.SR.GetString("InvalidLowBoundArgumentEx", (object) nameof (value), (object) value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (object) 0.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
    }

    /// <summary>Creates a copy of this <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
    /// <returns>A copy of this object.</returns>
    public object Clone()
    {
      PrinterSettings printerSettings = (PrinterSettings) this.MemberwiseClone();
      printerSettings.printDialogDisplayed = false;
      return (object) printerSettings;
    }

    internal DeviceContext CreateDeviceContext(PageSettings pageSettings)
    {
      IntPtr hdevmodeInternal = this.GetHdevmodeInternal();
      try
      {
        System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        try
        {
          pageSettings.CopyToHdevmode(hdevmodeInternal);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
        return this.CreateDeviceContext(hdevmodeInternal);
      }
      finally
      {
        SafeNativeMethods.GlobalFree(new HandleRef((object) null, hdevmodeInternal));
      }
    }

    internal DeviceContext CreateDeviceContext(IntPtr hdevmode)
    {
      DeviceContext dc = DeviceContext.CreateDC(this.DriverName, this.PrinterNameInternal, (string) null, new HandleRef((object) null, SafeNativeMethods.GlobalLock(new HandleRef((object) null, hdevmode))));
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, hdevmode));
      return dc;
    }

    internal DeviceContext CreateInformationContext(PageSettings pageSettings)
    {
      IntPtr hdevmodeInternal = this.GetHdevmodeInternal();
      try
      {
        System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        try
        {
          pageSettings.CopyToHdevmode(hdevmodeInternal);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
        return this.CreateInformationContext(hdevmodeInternal);
      }
      finally
      {
        SafeNativeMethods.GlobalFree(new HandleRef((object) null, hdevmodeInternal));
      }
    }

    internal DeviceContext CreateInformationContext(IntPtr hdevmode)
    {
      DeviceContext ic = DeviceContext.CreateIC(this.DriverName, this.PrinterNameInternal, (string) null, new HandleRef((object) null, SafeNativeMethods.GlobalLock(new HandleRef((object) null, hdevmode))));
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, hdevmode));
      return ic;
    }

    /// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information that is useful when creating a <see cref="T:System.Drawing.Printing.PrintDocument" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains information from a printer.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    public Graphics CreateMeasurementGraphics() => this.CreateMeasurementGraphics(this.DefaultPageSettings);

    /// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information, optionally specifying the origin at the margins.</summary>
    /// <param name="honorOriginAtMargins">
    /// <see langword="true" /> to indicate the origin at the margins; otherwise, <see langword="false" />.</param>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
    public Graphics CreateMeasurementGraphics(bool honorOriginAtMargins)
    {
      Graphics measurementGraphics = this.CreateMeasurementGraphics();
      if (measurementGraphics != null & honorOriginAtMargins)
      {
        System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        try
        {
          measurementGraphics.TranslateTransform(-this.defaultPageSettings.HardMarginX, -this.defaultPageSettings.HardMarginY);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
        measurementGraphics.TranslateTransform((float) this.defaultPageSettings.Margins.Left, (float) this.defaultPageSettings.Margins.Top);
      }
      return measurementGraphics;
    }

    /// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information associated with the specified <see cref="T:System.Drawing.Printing.PageSettings" />.</summary>
    /// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> to retrieve a graphics object for.</param>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
    public Graphics CreateMeasurementGraphics(PageSettings pageSettings)
    {
      DeviceContext deviceContext = this.CreateDeviceContext(pageSettings);
      Graphics measurementGraphics = Graphics.FromHdcInternal(deviceContext.Hdc);
      measurementGraphics.PrintingHelper = (object) deviceContext;
      return measurementGraphics;
    }

    /// <summary>Creates a <see cref="T:System.Drawing.Graphics" /> associated with the specified page settings and optionally specifying the origin at the margins.</summary>
    /// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> to retrieve a <see cref="T:System.Drawing.Graphics" /> object for.</param>
    /// <param name="honorOriginAtMargins">
    /// <see langword="true" /> to specify the origin at the margins; otherwise, <see langword="false" />.</param>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
    public Graphics CreateMeasurementGraphics(PageSettings pageSettings, bool honorOriginAtMargins)
    {
      Graphics measurementGraphics = this.CreateMeasurementGraphics();
      if (measurementGraphics != null & honorOriginAtMargins)
      {
        System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        try
        {
          measurementGraphics.TranslateTransform(-pageSettings.HardMarginX, -pageSettings.HardMarginY);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
        measurementGraphics.TranslateTransform((float) pageSettings.Margins.Left, (float) pageSettings.Margins.Top);
      }
      return measurementGraphics;
    }

    private static SafeNativeMethods.PRINTDLGX86 CreatePRINTDLGX86()
    {
      SafeNativeMethods.PRINTDLGX86 printdlgX86 = new SafeNativeMethods.PRINTDLGX86()
      {
        lStructSize = Marshal.SizeOf(typeof (SafeNativeMethods.PRINTDLGX86)),
        hwndOwner = IntPtr.Zero,
        hDevMode = IntPtr.Zero,
        hDevNames = IntPtr.Zero,
        Flags = 0
      };
      printdlgX86.hwndOwner = IntPtr.Zero;
      printdlgX86.hDC = IntPtr.Zero;
      printdlgX86.nFromPage = (short) 1;
      printdlgX86.nToPage = (short) 1;
      printdlgX86.nMinPage = (short) 0;
      printdlgX86.nMaxPage = (short) 9999;
      printdlgX86.nCopies = (short) 1;
      printdlgX86.hInstance = IntPtr.Zero;
      printdlgX86.lCustData = IntPtr.Zero;
      printdlgX86.lpfnPrintHook = IntPtr.Zero;
      printdlgX86.lpfnSetupHook = IntPtr.Zero;
      printdlgX86.lpPrintTemplateName = (string) null;
      printdlgX86.lpSetupTemplateName = (string) null;
      printdlgX86.hPrintTemplate = IntPtr.Zero;
      printdlgX86.hSetupTemplate = IntPtr.Zero;
      return printdlgX86;
    }

    private static SafeNativeMethods.PRINTDLG CreatePRINTDLG()
    {
      SafeNativeMethods.PRINTDLG printdlg = new SafeNativeMethods.PRINTDLG()
      {
        lStructSize = Marshal.SizeOf(typeof (SafeNativeMethods.PRINTDLG)),
        hwndOwner = IntPtr.Zero,
        hDevMode = IntPtr.Zero,
        hDevNames = IntPtr.Zero,
        Flags = 0
      };
      printdlg.hwndOwner = IntPtr.Zero;
      printdlg.hDC = IntPtr.Zero;
      printdlg.nFromPage = (short) 1;
      printdlg.nToPage = (short) 1;
      printdlg.nMinPage = (short) 0;
      printdlg.nMaxPage = (short) 9999;
      printdlg.nCopies = (short) 1;
      printdlg.hInstance = IntPtr.Zero;
      printdlg.lCustData = IntPtr.Zero;
      printdlg.lpfnPrintHook = IntPtr.Zero;
      printdlg.lpfnSetupHook = IntPtr.Zero;
      printdlg.lpPrintTemplateName = (string) null;
      printdlg.lpSetupTemplateName = (string) null;
      printdlg.hPrintTemplate = IntPtr.Zero;
      printdlg.hSetupTemplate = IntPtr.Zero;
      return printdlg;
    }

    private int DeviceCapabilities(short capability, IntPtr pointerToBuffer, int defaultValue)
    {
      System.Drawing.IntSecurity.AllPrinting.Assert();
      string printerName = this.PrinterName;
      CodeAccessPermission.RevertAssert();
      System.Drawing.IntSecurity.UnmanagedCode.Assert();
      return PrinterSettings.FastDeviceCapabilities(capability, pointerToBuffer, defaultValue, printerName);
    }

    private static int FastDeviceCapabilities(
      short capability,
      IntPtr pointerToBuffer,
      int defaultValue,
      string printerName)
    {
      int num = SafeNativeMethods.DeviceCapabilities(printerName, PrinterSettings.GetOutputPort(), capability, pointerToBuffer, IntPtr.Zero);
      return num == -1 ? defaultValue : num;
    }

    private static string GetDefaultPrinterName()
    {
      System.Drawing.IntSecurity.UnmanagedCode.Assert();
      if (IntPtr.Size == 8)
      {
        SafeNativeMethods.PRINTDLG printdlg = PrinterSettings.CreatePRINTDLG();
        printdlg.Flags = 1024;
        if (!SafeNativeMethods.PrintDlg(printdlg))
          return System.Drawing.SR.GetString("NoDefaultPrinter");
        IntPtr hDevNames = printdlg.hDevNames;
        IntPtr pDevnames = SafeNativeMethods.GlobalLock(new HandleRef((object) printdlg, hDevNames));
        string defaultPrinterName = !(pDevnames == IntPtr.Zero) ? PrinterSettings.ReadOneDEVNAME(pDevnames, 1) : throw new Win32Exception();
        SafeNativeMethods.GlobalUnlock(new HandleRef((object) printdlg, hDevNames));
        IntPtr zero = IntPtr.Zero;
        SafeNativeMethods.GlobalFree(new HandleRef((object) printdlg, printdlg.hDevNames));
        SafeNativeMethods.GlobalFree(new HandleRef((object) printdlg, printdlg.hDevMode));
        return defaultPrinterName;
      }
      SafeNativeMethods.PRINTDLGX86 printdlgX86 = PrinterSettings.CreatePRINTDLGX86();
      printdlgX86.Flags = 1024;
      if (!SafeNativeMethods.PrintDlg(printdlgX86))
        return System.Drawing.SR.GetString("NoDefaultPrinter");
      IntPtr hDevNames1 = printdlgX86.hDevNames;
      IntPtr pDevnames1 = SafeNativeMethods.GlobalLock(new HandleRef((object) printdlgX86, hDevNames1));
      string defaultPrinterName1 = !(pDevnames1 == IntPtr.Zero) ? PrinterSettings.ReadOneDEVNAME(pDevnames1, 1) : throw new Win32Exception();
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) printdlgX86, hDevNames1));
      IntPtr zero1 = IntPtr.Zero;
      SafeNativeMethods.GlobalFree(new HandleRef((object) printdlgX86, printdlgX86.hDevNames));
      SafeNativeMethods.GlobalFree(new HandleRef((object) printdlgX86, printdlgX86.hDevMode));
      return defaultPrinterName1;
    }

    private static string GetOutputPort()
    {
      System.Drawing.IntSecurity.UnmanagedCode.Assert();
      if (IntPtr.Size == 8)
      {
        SafeNativeMethods.PRINTDLG printdlg = PrinterSettings.CreatePRINTDLG();
        printdlg.Flags = 1024;
        if (!SafeNativeMethods.PrintDlg(printdlg))
          return System.Drawing.SR.GetString("NoDefaultPrinter");
        IntPtr hDevNames = printdlg.hDevNames;
        IntPtr pDevnames = SafeNativeMethods.GlobalLock(new HandleRef((object) printdlg, hDevNames));
        string outputPort = !(pDevnames == IntPtr.Zero) ? PrinterSettings.ReadOneDEVNAME(pDevnames, 2) : throw new Win32Exception();
        SafeNativeMethods.GlobalUnlock(new HandleRef((object) printdlg, hDevNames));
        IntPtr zero = IntPtr.Zero;
        SafeNativeMethods.GlobalFree(new HandleRef((object) printdlg, printdlg.hDevNames));
        SafeNativeMethods.GlobalFree(new HandleRef((object) printdlg, printdlg.hDevMode));
        return outputPort;
      }
      SafeNativeMethods.PRINTDLGX86 printdlgX86 = PrinterSettings.CreatePRINTDLGX86();
      printdlgX86.Flags = 1024;
      if (!SafeNativeMethods.PrintDlg(printdlgX86))
        return System.Drawing.SR.GetString("NoDefaultPrinter");
      IntPtr hDevNames1 = printdlgX86.hDevNames;
      IntPtr pDevnames1 = SafeNativeMethods.GlobalLock(new HandleRef((object) printdlgX86, hDevNames1));
      string outputPort1 = !(pDevnames1 == IntPtr.Zero) ? PrinterSettings.ReadOneDEVNAME(pDevnames1, 2) : throw new Win32Exception();
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) printdlgX86, hDevNames1));
      IntPtr zero1 = IntPtr.Zero;
      SafeNativeMethods.GlobalFree(new HandleRef((object) printdlgX86, printdlgX86.hDevNames));
      SafeNativeMethods.GlobalFree(new HandleRef((object) printdlgX86, printdlgX86.hDevMode));
      return outputPort1;
    }

    private int GetDeviceCaps(int capability, int defaultValue)
    {
      DeviceContext informationContext = this.CreateInformationContext(this.DefaultPageSettings);
      int deviceCaps = defaultValue;
      try
      {
        deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) informationContext, informationContext.Hdc), capability);
      }
      catch (InvalidPrinterException ex)
      {
      }
      finally
      {
        informationContext.Dispose();
      }
      return deviceCaps;
    }

    /// <summary>Creates a handle to a <see langword="DEVMODE" /> structure that corresponds to the printer settings.</summary>
    /// <returns>A handle to a <see langword="DEVMODE" /> structure.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The printer's initialization information could not be retrieved.</exception>
    public IntPtr GetHdevmode()
    {
      System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Demand();
      IntPtr hdevmodeInternal = this.GetHdevmodeInternal();
      this.defaultPageSettings.CopyToHdevmode(hdevmodeInternal);
      return hdevmodeInternal;
    }

    internal IntPtr GetHdevmodeInternal() => this.GetHdevmodeInternal(this.PrinterNameInternal);

    private IntPtr GetHdevmodeInternal(string printer)
    {
      int dwBytes = SafeNativeMethods.DocumentProperties(System.Drawing.NativeMethods.NullHandleRef, System.Drawing.NativeMethods.NullHandleRef, printer, IntPtr.Zero, System.Drawing.NativeMethods.NullHandleRef, 0);
      IntPtr handle = dwBytes >= 1 ? SafeNativeMethods.GlobalAlloc(2, (uint) dwBytes) : throw new InvalidPrinterException(this);
      IntPtr num = SafeNativeMethods.GlobalLock(new HandleRef((object) null, handle));
      if (this.cachedDevmode != null)
        Marshal.Copy(this.cachedDevmode, 0, num, (int) this.devmodebytes);
      else if (SafeNativeMethods.DocumentProperties(System.Drawing.NativeMethods.NullHandleRef, System.Drawing.NativeMethods.NullHandleRef, printer, num, System.Drawing.NativeMethods.NullHandleRef, 2) < 0)
        throw new Win32Exception();
      SafeNativeMethods.DEVMODE structure = (SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(num, typeof (SafeNativeMethods.DEVMODE));
      if (this.extrainfo != null && (int) this.extrabytes <= (int) structure.dmDriverExtra)
        Marshal.Copy(this.extrainfo, 0, (IntPtr) checked (unchecked ((long) num) + (long) structure.dmSize), (int) this.extrabytes);
      if ((structure.dmFields & 256) == 256 && this.copies != (short) -1)
        structure.dmCopies = this.copies;
      if ((structure.dmFields & 4096) == 4096 && this.duplex != Duplex.Default)
        structure.dmDuplex = (short) this.duplex;
      if ((structure.dmFields & 32768) == 32768 && this.collate.IsNotDefault)
        structure.dmCollate = (bool) this.collate ? (short) 1 : (short) 0;
      Marshal.StructureToPtr((object) structure, num, false);
      if (SafeNativeMethods.DocumentProperties(System.Drawing.NativeMethods.NullHandleRef, System.Drawing.NativeMethods.NullHandleRef, printer, num, num, 10) < 0)
      {
        SafeNativeMethods.GlobalFree(new HandleRef((object) null, handle));
        SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, handle));
        return IntPtr.Zero;
      }
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, handle));
      return handle;
    }

    /// <summary>Creates a handle to a <see langword="DEVMODE" /> structure that corresponds to the printer and the page settings specified through the <paramref name="pageSettings" /> parameter.</summary>
    /// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> object that the <see langword="DEVMODE" /> structure's handle corresponds to.</param>
    /// <returns>A handle to a <see langword="DEVMODE" /> structure.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The printer's initialization information could not be retrieved.</exception>
    public IntPtr GetHdevmode(PageSettings pageSettings)
    {
      System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Demand();
      IntPtr hdevmodeInternal = this.GetHdevmodeInternal();
      pageSettings.CopyToHdevmode(hdevmodeInternal);
      return hdevmodeInternal;
    }

    /// <summary>Creates a handle to a <see langword="DEVNAMES" /> structure that corresponds to the printer settings.</summary>
    /// <returns>A handle to a <see langword="DEVNAMES" /> structure.</returns>
    public IntPtr GetHdevnames()
    {
      System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Demand();
      string printerName = this.PrinterName;
      string driverName = this.DriverName;
      string outputPort = this.OutputPort;
      int num1 = checked (4 + printerName.Length + driverName.Length + outputPort.Length);
      short num2 = (short) (8 / Marshal.SystemDefaultCharSize);
      IntPtr handle = SafeNativeMethods.GlobalAlloc(66, (uint) checked (Marshal.SystemDefaultCharSize * (int) num2 + num1));
      IntPtr num3 = SafeNativeMethods.GlobalLock(new HandleRef((object) null, handle));
      Marshal.WriteInt16(num3, num2);
      short num4 = (short) ((int) num2 + (int) this.WriteOneDEVNAME(driverName, num3, (int) num2));
      Marshal.WriteInt16((IntPtr) checked (unchecked ((long) num3) + 2L), num4);
      short num5 = (short) ((int) num4 + (int) this.WriteOneDEVNAME(printerName, num3, (int) num4));
      Marshal.WriteInt16((IntPtr) checked (unchecked ((long) num3) + 4L), num5);
      short val = (short) ((int) num5 + (int) this.WriteOneDEVNAME(outputPort, num3, (int) num5));
      Marshal.WriteInt16((IntPtr) checked (unchecked ((long) num3) + 6L), val);
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, handle));
      return handle;
    }

    internal short GetModeField(ModeField field, short defaultValue) => this.GetModeField(field, defaultValue, IntPtr.Zero);

    internal short GetModeField(ModeField field, short defaultValue, IntPtr modeHandle)
    {
      bool flag = false;
      short modeField;
      try
      {
        if (modeHandle == IntPtr.Zero)
        {
          try
          {
            modeHandle = this.GetHdevmodeInternal();
            flag = true;
          }
          catch (InvalidPrinterException ex)
          {
            return defaultValue;
          }
        }
        SafeNativeMethods.DEVMODE structure = (SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(SafeNativeMethods.GlobalLock(new HandleRef((object) this, modeHandle)), typeof (SafeNativeMethods.DEVMODE));
        switch (field)
        {
          case ModeField.Orientation:
            modeField = structure.dmOrientation;
            break;
          case ModeField.PaperSize:
            modeField = structure.dmPaperSize;
            break;
          case ModeField.PaperLength:
            modeField = structure.dmPaperLength;
            break;
          case ModeField.PaperWidth:
            modeField = structure.dmPaperWidth;
            break;
          case ModeField.Copies:
            modeField = structure.dmCopies;
            break;
          case ModeField.DefaultSource:
            modeField = structure.dmDefaultSource;
            break;
          case ModeField.PrintQuality:
            modeField = structure.dmPrintQuality;
            break;
          case ModeField.Color:
            modeField = structure.dmColor;
            break;
          case ModeField.Duplex:
            modeField = structure.dmDuplex;
            break;
          case ModeField.YResolution:
            modeField = structure.dmYResolution;
            break;
          case ModeField.TTOption:
            modeField = structure.dmTTOption;
            break;
          case ModeField.Collate:
            modeField = structure.dmCollate;
            break;
          default:
            modeField = defaultValue;
            break;
        }
        SafeNativeMethods.GlobalUnlock(new HandleRef((object) this, modeHandle));
      }
      finally
      {
        if (flag)
          SafeNativeMethods.GlobalFree(new HandleRef((object) this, modeHandle));
      }
      return modeField;
    }

    internal PaperSize[] Get_PaperSizes()
    {
      System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Assert();
      string printerName = this.PrinterName;
      int length1 = PrinterSettings.FastDeviceCapabilities((short) 16, IntPtr.Zero, -1, printerName);
      if (length1 == -1)
        return new PaperSize[0];
      int num1 = Marshal.SystemDefaultCharSize * 64;
      IntPtr num2 = Marshal.AllocCoTaskMem(checked (num1 * length1));
      PrinterSettings.FastDeviceCapabilities((short) 16, num2, -1, printerName);
      IntPtr num3 = Marshal.AllocCoTaskMem(2 * length1);
      PrinterSettings.FastDeviceCapabilities((short) 2, num3, -1, printerName);
      IntPtr num4 = Marshal.AllocCoTaskMem(8 * length1);
      PrinterSettings.FastDeviceCapabilities((short) 3, num4, -1, printerName);
      PaperSize[] paperSizes = new PaperSize[length1];
      for (int index = 0; index < length1; ++index)
      {
        string name = Marshal.PtrToStringAuto((IntPtr) checked (unchecked ((long) num2) + (long) (num1 * index)), 64);
        int length2 = name.IndexOf(char.MinValue);
        if (length2 > -1)
          name = name.Substring(0, length2);
        short kind = Marshal.ReadInt16((IntPtr) checked (unchecked ((long) num3) + (long) (index * 2)));
        int num5 = Marshal.ReadInt32((IntPtr) checked (unchecked ((long) num4) + (long) (index * 8)));
        int num6 = Marshal.ReadInt32((IntPtr) checked (unchecked ((long) num4) + (long) (index * 8) + 4L));
        paperSizes[index] = new PaperSize((PaperKind) kind, name, PrinterUnitConvert.Convert(num5, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(num6, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
      }
      Marshal.FreeCoTaskMem(num2);
      Marshal.FreeCoTaskMem(num3);
      Marshal.FreeCoTaskMem(num4);
      return paperSizes;
    }

    internal PaperSource[] Get_PaperSources()
    {
      System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Assert();
      string printerName = this.PrinterName;
      int length1 = PrinterSettings.FastDeviceCapabilities((short) 12, IntPtr.Zero, -1, printerName);
      if (length1 == -1)
        return new PaperSource[0];
      int num1 = Marshal.SystemDefaultCharSize * 24;
      IntPtr num2 = Marshal.AllocCoTaskMem(checked (num1 * length1));
      PrinterSettings.FastDeviceCapabilities((short) 12, num2, -1, printerName);
      IntPtr num3 = Marshal.AllocCoTaskMem(2 * length1);
      PrinterSettings.FastDeviceCapabilities((short) 6, num3, -1, printerName);
      PaperSource[] paperSources = new PaperSource[length1];
      for (int index = 0; index < length1; ++index)
      {
        string name = Marshal.PtrToStringAuto((IntPtr) checked (unchecked ((long) num2) + (long) (num1 * index)), 24);
        int length2 = name.IndexOf(char.MinValue);
        if (length2 > -1)
          name = name.Substring(0, length2);
        short kind = Marshal.ReadInt16((IntPtr) checked (unchecked ((long) num3) + (long) (2 * index)));
        paperSources[index] = new PaperSource((PaperSourceKind) kind, name);
      }
      Marshal.FreeCoTaskMem(num2);
      Marshal.FreeCoTaskMem(num3);
      return paperSources;
    }

    internal PrinterResolution[] Get_PrinterResolutions()
    {
      System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Assert();
      string printerName = this.PrinterName;
      int num1 = PrinterSettings.FastDeviceCapabilities((short) 13, IntPtr.Zero, -1, printerName);
      if (num1 == -1)
        return new PrinterResolution[4]
        {
          new PrinterResolution(PrinterResolutionKind.High, -4, -1),
          new PrinterResolution(PrinterResolutionKind.Medium, -3, -1),
          new PrinterResolution(PrinterResolutionKind.Low, -2, -1),
          new PrinterResolution(PrinterResolutionKind.Draft, -1, -1)
        };
      PrinterResolution[] printerResolutions = new PrinterResolution[num1 + 4];
      printerResolutions[0] = new PrinterResolution(PrinterResolutionKind.High, -4, -1);
      printerResolutions[1] = new PrinterResolution(PrinterResolutionKind.Medium, -3, -1);
      printerResolutions[2] = new PrinterResolution(PrinterResolutionKind.Low, -2, -1);
      printerResolutions[3] = new PrinterResolution(PrinterResolutionKind.Draft, -1, -1);
      IntPtr num2 = Marshal.AllocCoTaskMem(checked (8 * num1));
      PrinterSettings.FastDeviceCapabilities((short) 13, num2, -1, printerName);
      for (int index = 0; index < num1; ++index)
      {
        int x = Marshal.ReadInt32((IntPtr) checked (unchecked ((long) num2) + (long) (index * 8)));
        int y = Marshal.ReadInt32((IntPtr) checked (unchecked ((long) num2) + (long) (index * 8) + 4L));
        printerResolutions[index + 4] = new PrinterResolution(PrinterResolutionKind.Custom, x, y);
      }
      Marshal.FreeCoTaskMem(num2);
      return printerResolutions;
    }

    private static string ReadOneDEVNAME(IntPtr pDevnames, int slot)
    {
      int num = checked (Marshal.SystemDefaultCharSize * (int) Marshal.ReadInt16(unchecked ((IntPtr) checked (unchecked ((long) pDevnames) + (long) (slot * 2)))));
      return Marshal.PtrToStringAuto((IntPtr) checked (unchecked ((long) pDevnames) + (long) num));
    }

    /// <summary>Copies the relevant information out of the given handle and into the <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
    /// <param name="hdevmode">The handle to a Win32 <see langword="DEVMODE" /> structure.</param>
    /// <exception cref="T:System.ArgumentException">The printer handle is not valid.</exception>
    public void SetHdevmode(IntPtr hdevmode)
    {
      System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Demand();
      IntPtr num = !(hdevmode == IntPtr.Zero) ? SafeNativeMethods.GlobalLock(new HandleRef((object) null, hdevmode)) : throw new ArgumentException(System.Drawing.SR.GetString("InvalidPrinterHandle", (object) hdevmode));
      SafeNativeMethods.DEVMODE structure = (SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(num, typeof (SafeNativeMethods.DEVMODE));
      this.devmodebytes = structure.dmSize;
      if (this.devmodebytes > (short) 0)
      {
        this.cachedDevmode = new byte[(int) this.devmodebytes];
        Marshal.Copy(num, this.cachedDevmode, 0, (int) this.devmodebytes);
      }
      this.extrabytes = structure.dmDriverExtra;
      if (this.extrabytes > (short) 0)
      {
        this.extrainfo = new byte[(int) this.extrabytes];
        Marshal.Copy((IntPtr) checked (unchecked ((long) num) + (long) structure.dmSize), this.extrainfo, 0, (int) this.extrabytes);
      }
      if ((structure.dmFields & 256) == 256)
        this.copies = structure.dmCopies;
      if ((structure.dmFields & 4096) == 4096)
        this.duplex = (Duplex) structure.dmDuplex;
      if ((structure.dmFields & 32768) == 32768)
        this.collate = (TriState) (structure.dmCollate == (short) 1);
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, hdevmode));
    }

    /// <summary>Copies the relevant information out of the given handle and into the <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
    /// <param name="hdevnames">The handle to a Win32 <see langword="DEVNAMES" /> structure.</param>
    /// <exception cref="T:System.ArgumentException">The printer handle is invalid.</exception>
    public void SetHdevnames(IntPtr hdevnames)
    {
      System.Drawing.IntSecurity.AllPrintingAndUnmanagedCode.Demand();
      IntPtr pDevnames = !(hdevnames == IntPtr.Zero) ? SafeNativeMethods.GlobalLock(new HandleRef((object) null, hdevnames)) : throw new ArgumentException(System.Drawing.SR.GetString("InvalidPrinterHandle", (object) hdevnames));
      this.driverName = PrinterSettings.ReadOneDEVNAME(pDevnames, 0);
      this.printerName = PrinterSettings.ReadOneDEVNAME(pDevnames, 1);
      this.outputPort = PrinterSettings.ReadOneDEVNAME(pDevnames, 2);
      this.PrintDialogDisplayed = true;
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, hdevnames));
    }

    /// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PrinterSettings" /> in string form.</summary>
    /// <returns>A string.</returns>
    public override string ToString() => "[PrinterSettings " + (System.Drawing.IntSecurity.HasPermission(System.Drawing.IntSecurity.AllPrinting) ? this.PrinterName : "<printer name unavailable>") + " Copies=" + this.Copies.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " Collate=" + this.Collate.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " Duplex=" + TypeDescriptor.GetConverter(typeof (Duplex)).ConvertToString((object) (int) this.Duplex) + " FromPage=" + this.FromPage.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " LandscapeAngle=" + this.LandscapeAngle.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " MaximumCopies=" + this.MaximumCopies.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " OutputPort=" + this.OutputPort.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " ToPage=" + this.ToPage.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "]";

    private short WriteOneDEVNAME(string str, IntPtr bufferStart, int index)
    {
      if (str == null)
        str = "";
      IntPtr destination = (IntPtr) checked (unchecked ((long) bufferStart) + (long) (index * Marshal.SystemDefaultCharSize));
      if (Marshal.SystemDefaultCharSize == 1)
      {
        byte[] bytes = Encoding.Default.GetBytes(str);
        Marshal.Copy(bytes, 0, destination, bytes.Length);
        Marshal.WriteByte((IntPtr) checked (unchecked ((long) destination) + (long) bytes.Length), (byte) 0);
      }
      else
      {
        char[] charArray = str.ToCharArray();
        Marshal.Copy(charArray, 0, destination, charArray.Length);
        Marshal.WriteInt16((IntPtr) checked (unchecked ((long) destination) + (long) (charArray.Length * 2)), (short) 0);
      }
      return checked ((short) (str.Length + 1));
    }

    /// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PaperSize" /> objects.</summary>
    public class PaperSizeCollection : ICollection, IEnumerable
    {
      private PaperSize[] array;

      /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> class.</summary>
      /// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PaperSize" />.</param>
      public PaperSizeCollection(PaperSize[] array) => this.array = array;

      /// <summary>Gets the number of different paper sizes in the collection.</summary>
      /// <returns>The number of different paper sizes in the collection.</returns>
      public int Count => this.array.Length;

      /// <summary>Gets the <see cref="T:System.Drawing.Printing.PaperSize" /> at a specified index.</summary>
      /// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PaperSize" /> to get.</param>
      /// <returns>The <see cref="T:System.Drawing.Printing.PaperSize" /> at the specified index.</returns>
      public virtual PaperSize this[int index] => this.array[index];

      /// <summary>Returns an enumerator that can iterate through the collection.</summary>
      /// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" />.</returns>
      public IEnumerator GetEnumerator() => (IEnumerator) new PrinterSettings.ArrayEnumerator((object[]) this.array, 0, this.Count);

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
      int ICollection.Count => this.Count;

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
      bool ICollection.IsSynchronized => false;

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
      object ICollection.SyncRoot => (object) this;

      /// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
      /// <param name="array">A zero-based array that receives the items copied from the collection.</param>
      /// <param name="index">The index at which to start copying items.</param>
      void ICollection.CopyTo(Array array, int index) => Array.Copy((Array) this.array, index, array, 0, this.array.Length);

      /// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> to the specified array, starting at the specified index.</summary>
      /// <param name="paperSizes">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" />.</param>
      /// <param name="index">The index at which to start copying items.</param>
      public void CopyTo(PaperSize[] paperSizes, int index) => Array.Copy((Array) this.array, index, (Array) paperSizes, 0, this.array.Length);

      /// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
      /// <returns>An enumerator associated with the collection.</returns>
      IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

      /// <summary>Adds a <see cref="T:System.Drawing.Printing.PrinterResolution" /> to the end of the collection.</summary>
      /// <param name="paperSize">The <see cref="T:System.Drawing.Printing.PaperSize" /> to add to the collection.</param>
      /// <returns>The zero-based index of the newly added item.</returns>
      [EditorBrowsable(EditorBrowsableState.Never)]
      public int Add(PaperSize paperSize)
      {
        PaperSize[] paperSizeArray = new PaperSize[this.Count + 1];
        ((ICollection) this).CopyTo((Array) paperSizeArray, 0);
        paperSizeArray[this.Count] = paperSize;
        this.array = paperSizeArray;
        return this.Count;
      }
    }

    /// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PaperSource" /> objects.</summary>
    public class PaperSourceCollection : ICollection, IEnumerable
    {
      private PaperSource[] array;

      /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> class.</summary>
      /// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PaperSource" />.</param>
      public PaperSourceCollection(PaperSource[] array) => this.array = array;

      /// <summary>Gets the number of different paper sources in the collection.</summary>
      /// <returns>The number of different paper sources in the collection.</returns>
      public int Count => this.array.Length;

      /// <summary>Gets the <see cref="T:System.Drawing.Printing.PaperSource" /> at a specified index.</summary>
      /// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PaperSource" /> to get.</param>
      /// <returns>The <see cref="T:System.Drawing.Printing.PaperSource" /> at the specified index.</returns>
      public virtual PaperSource this[int index] => this.array[index];

      /// <summary>Returns an enumerator that can iterate through the collection.</summary>
      /// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</returns>
      public IEnumerator GetEnumerator() => (IEnumerator) new PrinterSettings.ArrayEnumerator((object[]) this.array, 0, this.Count);

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
      int ICollection.Count => this.Count;

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
      bool ICollection.IsSynchronized => false;

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
      object ICollection.SyncRoot => (object) this;

      /// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
      /// <param name="array">The destination array for the contents of the collection.</param>
      /// <param name="index">The index at which to start the copy operation.</param>
      void ICollection.CopyTo(Array array, int index) => Array.Copy((Array) this.array, index, array, 0, this.array.Length);

      /// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> to the specified array, starting at the specified index.</summary>
      /// <param name="paperSources">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</param>
      /// <param name="index">The index at which to start copying items.</param>
      public void CopyTo(PaperSource[] paperSources, int index) => Array.Copy((Array) this.array, index, (Array) paperSources, 0, this.array.Length);

      /// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
      IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

      /// <summary>Adds the specified <see cref="T:System.Drawing.Printing.PaperSource" /> to end of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</summary>
      /// <param name="paperSource">The <see cref="T:System.Drawing.Printing.PaperSource" /> to add to the collection.</param>
      /// <returns>The zero-based index where the <see cref="T:System.Drawing.Printing.PaperSource" /> was added.</returns>
      [EditorBrowsable(EditorBrowsableState.Never)]
      public int Add(PaperSource paperSource)
      {
        PaperSource[] paperSourceArray = new PaperSource[this.Count + 1];
        ((ICollection) this).CopyTo((Array) paperSourceArray, 0);
        paperSourceArray[this.Count] = paperSource;
        this.array = paperSourceArray;
        return this.Count;
      }
    }

    /// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PrinterResolution" /> objects.</summary>
    public class PrinterResolutionCollection : ICollection, IEnumerable
    {
      private PrinterResolution[] array;

      /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> class.</summary>
      /// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PrinterResolution" />.</param>
      public PrinterResolutionCollection(PrinterResolution[] array) => this.array = array;

      /// <summary>Gets the number of available printer resolutions in the collection.</summary>
      /// <returns>The number of available printer resolutions in the collection.</returns>
      public int Count => this.array.Length;

      /// <summary>Gets the <see cref="T:System.Drawing.Printing.PrinterResolution" /> at a specified index.</summary>
      /// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PrinterResolution" /> to get.</param>
      /// <returns>The <see cref="T:System.Drawing.Printing.PrinterResolution" /> at the specified index.</returns>
      public virtual PrinterResolution this[int index] => this.array[index];

      /// <summary>Returns an enumerator that can iterate through the collection.</summary>
      /// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" />.</returns>
      public IEnumerator GetEnumerator() => (IEnumerator) new PrinterSettings.ArrayEnumerator((object[]) this.array, 0, this.Count);

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
      int ICollection.Count => this.Count;

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
      bool ICollection.IsSynchronized => false;

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
      object ICollection.SyncRoot => (object) this;

      /// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
      /// <param name="array">The destination array.</param>
      /// <param name="index">The index at which to start the copy operation.</param>
      void ICollection.CopyTo(Array array, int index) => Array.Copy((Array) this.array, index, array, 0, this.array.Length);

      /// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> to the specified array, starting at the specified index.</summary>
      /// <param name="printerResolutions">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" />.</param>
      /// <param name="index">The index at which to start copying items.</param>
      public void CopyTo(PrinterResolution[] printerResolutions, int index) => Array.Copy((Array) this.array, index, (Array) printerResolutions, 0, this.array.Length);

      /// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
      IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

      /// <summary>Adds a <see cref="T:System.Drawing.Printing.PrinterResolution" /> to the end of the collection.</summary>
      /// <param name="printerResolution">The <see cref="T:System.Drawing.Printing.PrinterResolution" /> to add to the collection.</param>
      /// <returns>The zero-based index of the newly added item.</returns>
      [EditorBrowsable(EditorBrowsableState.Never)]
      public int Add(PrinterResolution printerResolution)
      {
        PrinterResolution[] printerResolutionArray = new PrinterResolution[this.Count + 1];
        ((ICollection) this).CopyTo((Array) printerResolutionArray, 0);
        printerResolutionArray[this.Count] = printerResolution;
        this.array = printerResolutionArray;
        return this.Count;
      }
    }

    /// <summary>Contains a collection of <see cref="T:System.String" /> objects.</summary>
    public class StringCollection : ICollection, IEnumerable
    {
      private string[] array;

      /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" /> class.</summary>
      /// <param name="array">An array of type <see cref="T:System.String" />.</param>
      public StringCollection(string[] array) => this.array = array;

      /// <summary>Gets the number of strings in the collection.</summary>
      /// <returns>The number of strings in the collection.</returns>
      public int Count => this.array.Length;

      /// <summary>Gets the <see cref="T:System.String" /> at a specified index.</summary>
      /// <param name="index">The index of the <see cref="T:System.String" /> to get.</param>
      /// <returns>The <see cref="T:System.String" /> at the specified index.</returns>
      public virtual string this[int index] => this.array[index];

      /// <summary>Returns an enumerator that can iterate through the collection.</summary>
      /// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" />.</returns>
      public IEnumerator GetEnumerator() => (IEnumerator) new PrinterSettings.ArrayEnumerator((object[]) this.array, 0, this.Count);

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
      int ICollection.Count => this.Count;

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
      bool ICollection.IsSynchronized => false;

      /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
      object ICollection.SyncRoot => (object) this;

      /// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
      /// <param name="array">The array for items to be copied to.</param>
      /// <param name="index">The starting index.</param>
      void ICollection.CopyTo(Array array, int index) => Array.Copy((Array) this.array, index, array, 0, this.array.Length);

      /// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> to the specified array, starting at the specified index</summary>
      /// <param name="strings">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" />.</param>
      /// <param name="index">The index at which to start copying items.</param>
      public void CopyTo(string[] strings, int index) => Array.Copy((Array) this.array, index, (Array) strings, 0, this.array.Length);

      /// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
      IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

      /// <summary>Adds a string to the end of the collection.</summary>
      /// <param name="value">The string to add to the collection.</param>
      /// <returns>The zero-based index of the newly added item.</returns>
      [EditorBrowsable(EditorBrowsableState.Never)]
      public int Add(string value)
      {
        string[] strArray = new string[this.Count + 1];
        ((ICollection) this).CopyTo((Array) strArray, 0);
        strArray[this.Count] = value;
        this.array = strArray;
        return this.Count;
      }
    }

    private class ArrayEnumerator : IEnumerator
    {
      private object[] array;
      private object item;
      private int index;
      private int startIndex;
      private int endIndex;

      public ArrayEnumerator(object[] array, int startIndex, int count)
      {
        this.array = array;
        this.startIndex = startIndex;
        this.endIndex = this.index + count;
        this.index = this.startIndex;
      }

      public object Current => this.item;

      public bool MoveNext()
      {
        if (this.index >= this.endIndex)
          return false;
        this.item = this.array[this.index++];
        return true;
      }

      public void Reset()
      {
        this.index = this.startIndex;
        this.item = (object) null;
      }
    }
  }
}
