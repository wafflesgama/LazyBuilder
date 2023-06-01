// Decompiled with JetBrains decompiler
// Type: System.Drawing.PrintPreviewGraphics
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Internal;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace System.Drawing
{
  internal class PrintPreviewGraphics
  {
    private PrintPageEventArgs printPageEventArgs;
    private PrintDocument printDocument;

    public PrintPreviewGraphics(PrintDocument document, PrintPageEventArgs e)
    {
      this.printPageEventArgs = e;
      this.printDocument = document;
    }

    public RectangleF VisibleClipBounds
    {
      get
      {
        using (DeviceContext deviceContext = this.printPageEventArgs.PageSettings.PrinterSettings.CreateDeviceContext(this.printPageEventArgs.PageSettings.PrinterSettings.GetHdevmodeInternal()))
        {
          using (Graphics graphics = Graphics.FromHdcInternal(deviceContext.Hdc))
          {
            if (this.printDocument.OriginAtMargins)
            {
              int deviceCaps1 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) deviceContext, deviceContext.Hdc), 88);
              int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) deviceContext, deviceContext.Hdc), 90);
              int deviceCaps3 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) deviceContext, deviceContext.Hdc), 112);
              int deviceCaps4 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) deviceContext, deviceContext.Hdc), 113);
              float num1 = (float) (deviceCaps3 * 100 / deviceCaps1);
              float num2 = (float) (deviceCaps4 * 100 / deviceCaps2);
              graphics.TranslateTransform(-num1, -num2);
              graphics.TranslateTransform((float) this.printDocument.DefaultPageSettings.Margins.Left, (float) this.printDocument.DefaultPageSettings.Margins.Top);
            }
            return graphics.VisibleClipBounds;
          }
        }
      }
    }
  }
}
