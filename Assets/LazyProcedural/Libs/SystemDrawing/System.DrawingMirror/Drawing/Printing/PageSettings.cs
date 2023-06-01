// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PageSettings
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Printing
{
  /// <summary>Specifies settings that apply to a single, printed page.</summary>
  [Serializable]
  public class PageSettings : ICloneable
  {
    internal PrinterSettings printerSettings;
    private TriState color = TriState.Default;
    private PaperSize paperSize;
    private PaperSource paperSource;
    private PrinterResolution printerResolution;
    private TriState landscape = TriState.Default;
    private Margins margins = new Margins();

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PageSettings" /> class using the default printer.</summary>
    public PageSettings()
      : this(new PrinterSettings())
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PageSettings" /> class using a specified printer.</summary>
    /// <param name="printerSettings">The <see cref="T:System.Drawing.Printing.PrinterSettings" /> that describes the printer to use.</param>
    public PageSettings(PrinterSettings printerSettings) => this.printerSettings = printerSettings;

    /// <summary>Gets the size of the page, taking into account the page orientation specified by the <see cref="P:System.Drawing.Printing.PageSettings.Landscape" /> property.</summary>
    /// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the length and width, in hundredths of an inch, of the page.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    public Rectangle Bounds
    {
      get
      {
        IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        IntPtr hdevmode = this.printerSettings.GetHdevmode();
        Rectangle bounds = this.GetBounds(hdevmode);
        SafeNativeMethods.GlobalFree(new HandleRef((object) this, hdevmode));
        return bounds;
      }
    }

    /// <summary>Gets or sets a value indicating whether the page should be printed in color.</summary>
    /// <returns>
    /// <see langword="true" /> if the page should be printed in color; otherwise, <see langword="false" />. The default is determined by the printer.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    public bool Color
    {
      get => this.color.IsDefault ? this.printerSettings.GetModeField(ModeField.Color, (short) 1) == (short) 2 : (bool) this.color;
      set => this.color = (TriState) value;
    }

    /// <summary>Gets the x-coordinate, in hundredths of an inch, of the hard margin at the left of the page.</summary>
    /// <returns>The x-coordinate, in hundredths of an inch, of the left-hand hard margin.</returns>
    public float HardMarginX
    {
      get
      {
        IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        using (DeviceContext deviceContext = this.printerSettings.CreateDeviceContext(this))
        {
          int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) deviceContext, deviceContext.Hdc), 88);
          return (float) (UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) deviceContext, deviceContext.Hdc), 112) * 100 / deviceCaps);
        }
      }
    }

    /// <summary>Gets the y-coordinate, in hundredths of an inch, of the hard margin at the top of the page.</summary>
    /// <returns>The y-coordinate, in hundredths of an inch, of the hard margin at the top of the page.</returns>
    public float HardMarginY
    {
      get
      {
        using (DeviceContext deviceContext = this.printerSettings.CreateDeviceContext(this))
        {
          int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) deviceContext, deviceContext.Hdc), 90);
          return (float) (UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) deviceContext, deviceContext.Hdc), 113) * 100 / deviceCaps);
        }
      }
    }

    /// <summary>Gets or sets a value indicating whether the page is printed in landscape or portrait orientation.</summary>
    /// <returns>
    /// <see langword="true" /> if the page should be printed in landscape orientation; otherwise, <see langword="false" />. The default is determined by the printer.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    public bool Landscape
    {
      get => this.landscape.IsDefault ? this.printerSettings.GetModeField(ModeField.Orientation, (short) 1) == (short) 2 : (bool) this.landscape;
      set => this.landscape = (TriState) value;
    }

    /// <summary>Gets or sets the margins for this page.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.Margins" /> that represents the margins, in hundredths of an inch, for the page. The default is 1-inch margins on all sides.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
    public Margins Margins
    {
      get => this.margins;
      set => this.margins = value;
    }

    /// <summary>Gets or sets the paper size for the page.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PaperSize" /> that represents the size of the paper. The default is the printer's default paper size.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
    public PaperSize PaperSize
    {
      get
      {
        IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        return this.GetPaperSize(IntPtr.Zero);
      }
      set => this.paperSize = value;
    }

    /// <summary>Gets or sets the page's paper source; for example, the printer's upper tray.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PaperSource" /> that specifies the source of the paper. The default is the printer's default paper source.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
    public PaperSource PaperSource
    {
      get
      {
        if (this.paperSource != null)
          return this.paperSource;
        IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        IntPtr hdevmode = this.printerSettings.GetHdevmode();
        PaperSource paperSource = this.PaperSourceFromMode((SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(SafeNativeMethods.GlobalLock(new HandleRef((object) this, hdevmode)), typeof (SafeNativeMethods.DEVMODE)));
        SafeNativeMethods.GlobalUnlock(new HandleRef((object) this, hdevmode));
        SafeNativeMethods.GlobalFree(new HandleRef((object) this, hdevmode));
        return paperSource;
      }
      set => this.paperSource = value;
    }

    /// <summary>Gets the bounds of the printable area of the page for the printer.</summary>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> representing the length and width, in hundredths of an inch, of the area the printer is capable of printing in.</returns>
    public RectangleF PrintableArea
    {
      get
      {
        RectangleF printableArea = new RectangleF();
        DeviceContext informationContext = this.printerSettings.CreateInformationContext(this);
        HandleRef hDC = new HandleRef((object) informationContext, informationContext.Hdc);
        try
        {
          int deviceCaps1 = UnsafeNativeMethods.GetDeviceCaps(hDC, 88);
          int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(hDC, 90);
          if (!this.Landscape)
          {
            printableArea.X = (float) UnsafeNativeMethods.GetDeviceCaps(hDC, 112) * 100f / (float) deviceCaps1;
            printableArea.Y = (float) UnsafeNativeMethods.GetDeviceCaps(hDC, 113) * 100f / (float) deviceCaps2;
            printableArea.Width = (float) UnsafeNativeMethods.GetDeviceCaps(hDC, 8) * 100f / (float) deviceCaps1;
            printableArea.Height = (float) UnsafeNativeMethods.GetDeviceCaps(hDC, 10) * 100f / (float) deviceCaps2;
          }
          else
          {
            printableArea.Y = (float) UnsafeNativeMethods.GetDeviceCaps(hDC, 112) * 100f / (float) deviceCaps1;
            printableArea.X = (float) UnsafeNativeMethods.GetDeviceCaps(hDC, 113) * 100f / (float) deviceCaps2;
            printableArea.Height = (float) UnsafeNativeMethods.GetDeviceCaps(hDC, 8) * 100f / (float) deviceCaps1;
            printableArea.Width = (float) UnsafeNativeMethods.GetDeviceCaps(hDC, 10) * 100f / (float) deviceCaps2;
          }
        }
        finally
        {
          informationContext.Dispose();
        }
        return printableArea;
      }
    }

    /// <summary>Gets or sets the printer resolution for the page.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PrinterResolution" /> that specifies the printer resolution for the page. The default is the printer's default resolution.</returns>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
    public PrinterResolution PrinterResolution
    {
      get
      {
        if (this.printerResolution != null)
          return this.printerResolution;
        IntSecurity.AllPrintingAndUnmanagedCode.Assert();
        IntPtr hdevmode = this.printerSettings.GetHdevmode();
        PrinterResolution printerResolution = this.PrinterResolutionFromMode((SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(SafeNativeMethods.GlobalLock(new HandleRef((object) this, hdevmode)), typeof (SafeNativeMethods.DEVMODE)));
        SafeNativeMethods.GlobalUnlock(new HandleRef((object) this, hdevmode));
        SafeNativeMethods.GlobalFree(new HandleRef((object) this, hdevmode));
        return printerResolution;
      }
      set => this.printerResolution = value;
    }

    /// <summary>Gets or sets the printer settings associated with the page.</summary>
    /// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that represents the printer settings associated with the page.</returns>
    public PrinterSettings PrinterSettings
    {
      get => this.printerSettings;
      set
      {
        if (value == null)
          value = new PrinterSettings();
        this.printerSettings = value;
      }
    }

    /// <summary>Creates a copy of this <see cref="T:System.Drawing.Printing.PageSettings" />.</summary>
    /// <returns>A copy of this object.</returns>
    public object Clone()
    {
      PageSettings pageSettings = (PageSettings) this.MemberwiseClone();
      pageSettings.margins = (Margins) this.margins.Clone();
      return (object) pageSettings;
    }

    /// <summary>Copies the relevant information from the <see cref="T:System.Drawing.Printing.PageSettings" /> to the specified <see langword="DEVMODE" /> structure.</summary>
    /// <param name="hdevmode">The handle to a Win32 <see langword="DEVMODE" /> structure.</param>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
    public void CopyToHdevmode(IntPtr hdevmode)
    {
      IntSecurity.AllPrintingAndUnmanagedCode.Demand();
      IntPtr num1 = SafeNativeMethods.GlobalLock(new HandleRef((object) null, hdevmode));
      SafeNativeMethods.DEVMODE structure = (SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(num1, typeof (SafeNativeMethods.DEVMODE));
      if (this.color.IsNotDefault && (structure.dmFields & 2048) == 2048)
        structure.dmColor = (bool) this.color ? (short) 2 : (short) 1;
      if (this.landscape.IsNotDefault && (structure.dmFields & 1) == 1)
        structure.dmOrientation = (bool) this.landscape ? (short) 2 : (short) 1;
      if (this.paperSize != null)
      {
        if ((structure.dmFields & 2) == 2)
          structure.dmPaperSize = (short) this.paperSize.RawKind;
        bool flag1 = false;
        bool flag2 = false;
        if ((structure.dmFields & 4) == 4)
        {
          int num2 = PrinterUnitConvert.Convert(this.paperSize.Height, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
          structure.dmPaperLength = (short) num2;
          flag2 = true;
        }
        if ((structure.dmFields & 8) == 8)
        {
          int num3 = PrinterUnitConvert.Convert(this.paperSize.Width, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
          structure.dmPaperWidth = (short) num3;
          flag1 = true;
        }
        if (this.paperSize.Kind == PaperKind.Custom)
        {
          if (!flag2)
          {
            structure.dmFields |= 4;
            int num4 = PrinterUnitConvert.Convert(this.paperSize.Height, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
            structure.dmPaperLength = (short) num4;
          }
          if (!flag1)
          {
            structure.dmFields |= 8;
            int num5 = PrinterUnitConvert.Convert(this.paperSize.Width, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
            structure.dmPaperWidth = (short) num5;
          }
        }
      }
      if (this.paperSource != null && (structure.dmFields & 512) == 512)
        structure.dmDefaultSource = (short) this.paperSource.RawKind;
      if (this.printerResolution != null)
      {
        if (this.printerResolution.Kind == PrinterResolutionKind.Custom)
        {
          if ((structure.dmFields & 1024) == 1024)
            structure.dmPrintQuality = (short) this.printerResolution.X;
          if ((structure.dmFields & 8192) == 8192)
            structure.dmYResolution = (short) this.printerResolution.Y;
        }
        else if ((structure.dmFields & 1024) == 1024)
          structure.dmPrintQuality = (short) this.printerResolution.Kind;
      }
      Marshal.StructureToPtr((object) structure, num1, false);
      if ((int) structure.dmDriverExtra >= (int) this.ExtraBytes && SafeNativeMethods.DocumentProperties(System.Drawing.NativeMethods.NullHandleRef, System.Drawing.NativeMethods.NullHandleRef, this.printerSettings.PrinterName, num1, num1, 10) < 0 && System.Drawing.LocalAppContextSwitches.FreeCopyToDevModeOnFailure)
        SafeNativeMethods.GlobalFree(new HandleRef((object) null, num1));
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, hdevmode));
    }

    private short ExtraBytes
    {
      get
      {
        IntPtr hdevmodeInternal = this.printerSettings.GetHdevmodeInternal();
        short dmDriverExtra = ((SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(SafeNativeMethods.GlobalLock(new HandleRef((object) this, hdevmodeInternal)), typeof (SafeNativeMethods.DEVMODE))).dmDriverExtra;
        SafeNativeMethods.GlobalUnlock(new HandleRef((object) this, hdevmodeInternal));
        SafeNativeMethods.GlobalFree(new HandleRef((object) this, hdevmodeInternal));
        return dmDriverExtra;
      }
    }

    internal Rectangle GetBounds(IntPtr modeHandle)
    {
      PaperSize paperSize = this.GetPaperSize(modeHandle);
      return !this.GetLandscape(modeHandle) ? new Rectangle(0, 0, paperSize.Width, paperSize.Height) : new Rectangle(0, 0, paperSize.Height, paperSize.Width);
    }

    private bool GetLandscape(IntPtr modeHandle) => this.landscape.IsDefault ? this.printerSettings.GetModeField(ModeField.Orientation, (short) 1, modeHandle) == (short) 2 : (bool) this.landscape;

    private PaperSize GetPaperSize(IntPtr modeHandle)
    {
      if (this.paperSize != null)
        return this.paperSize;
      bool flag = false;
      if (modeHandle == IntPtr.Zero)
      {
        modeHandle = this.printerSettings.GetHdevmode();
        flag = true;
      }
      PaperSize paperSize = this.PaperSizeFromMode((SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(SafeNativeMethods.GlobalLock(new HandleRef((object) null, modeHandle)), typeof (SafeNativeMethods.DEVMODE)));
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, modeHandle));
      if (flag)
        SafeNativeMethods.GlobalFree(new HandleRef((object) null, modeHandle));
      return paperSize;
    }

    private PaperSize PaperSizeFromMode(SafeNativeMethods.DEVMODE mode)
    {
      PaperSize[] paperSizes = this.printerSettings.Get_PaperSizes();
      if ((mode.dmFields & 2) == 2)
      {
        for (int index = 0; index < paperSizes.Length; ++index)
        {
          if (paperSizes[index].RawKind == (int) mode.dmPaperSize)
            return paperSizes[index];
        }
      }
      return new PaperSize(PaperKind.Custom, "custom", PrinterUnitConvert.Convert((int) mode.dmPaperWidth, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert((int) mode.dmPaperLength, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
    }

    private PaperSource PaperSourceFromMode(SafeNativeMethods.DEVMODE mode)
    {
      PaperSource[] paperSources = this.printerSettings.Get_PaperSources();
      if ((mode.dmFields & 512) == 512)
      {
        for (int index = 0; index < paperSources.Length; ++index)
        {
          if ((int) (short) paperSources[index].RawKind == (int) mode.dmDefaultSource)
            return paperSources[index];
        }
      }
      return new PaperSource((PaperSourceKind) mode.dmDefaultSource, "unknown");
    }

    private PrinterResolution PrinterResolutionFromMode(SafeNativeMethods.DEVMODE mode)
    {
      PrinterResolution[] printerResolutions = this.printerSettings.Get_PrinterResolutions();
      for (int index = 0; index < printerResolutions.Length; ++index)
      {
        if (mode.dmPrintQuality >= (short) 0 && (mode.dmFields & 1024) == 1024 && (mode.dmFields & 8192) == 8192)
        {
          if (printerResolutions[index].X == (int) mode.dmPrintQuality && printerResolutions[index].Y == (int) mode.dmYResolution)
            return printerResolutions[index];
        }
        else if ((mode.dmFields & 1024) == 1024 && printerResolutions[index].Kind == (PrinterResolutionKind) mode.dmPrintQuality)
          return printerResolutions[index];
      }
      return new PrinterResolution(PrinterResolutionKind.Custom, (int) mode.dmPrintQuality, (int) mode.dmYResolution);
    }

    /// <summary>Copies relevant information to the <see cref="T:System.Drawing.Printing.PageSettings" /> from the specified <see langword="DEVMODE" /> structure.</summary>
    /// <param name="hdevmode">The handle to a Win32 <see langword="DEVMODE" /> structure.</param>
    /// <exception cref="T:System.ArgumentException">The printer handle is not valid.</exception>
    /// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
    public void SetHdevmode(IntPtr hdevmode)
    {
      IntSecurity.AllPrintingAndUnmanagedCode.Demand();
      SafeNativeMethods.DEVMODE mode = !(hdevmode == IntPtr.Zero) ? (SafeNativeMethods.DEVMODE) UnsafeNativeMethods.PtrToStructure(SafeNativeMethods.GlobalLock(new HandleRef((object) null, hdevmode)), typeof (SafeNativeMethods.DEVMODE)) : throw new ArgumentException(System.Drawing.SR.GetString("InvalidPrinterHandle", (object) hdevmode));
      if ((mode.dmFields & 2048) == 2048)
        this.color = (TriState) (mode.dmColor == (short) 2);
      if ((mode.dmFields & 1) == 1)
        this.landscape = (TriState) (mode.dmOrientation == (short) 2);
      this.paperSize = this.PaperSizeFromMode(mode);
      this.paperSource = this.PaperSourceFromMode(mode);
      this.printerResolution = this.PrinterResolutionFromMode(mode);
      SafeNativeMethods.GlobalUnlock(new HandleRef((object) null, hdevmode));
    }

    /// <summary>Converts the <see cref="T:System.Drawing.Printing.PageSettings" /> to string form.</summary>
    /// <returns>A string showing the various property settings for the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
    public override string ToString() => "[PageSettings: Color=" + this.Color.ToString() + ", Landscape=" + this.Landscape.ToString() + ", Margins=" + this.Margins.ToString() + ", PaperSize=" + this.PaperSize.ToString() + ", PaperSource=" + this.PaperSource.ToString() + ", PrinterResolution=" + this.PrinterResolution.ToString() + "]";
  }
}
