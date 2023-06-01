// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.WindowsGraphics
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Drawing2D;

namespace System.Drawing.Internal
{
  internal sealed class WindowsGraphics : MarshalByRefObject, IDisposable, IDeviceContext
  {
    private DeviceContext dc;
    private bool disposeDc;
    private Graphics graphics;

    public WindowsGraphics(DeviceContext dc)
    {
      this.dc = dc;
      this.dc.SaveHdc();
    }

    public static WindowsGraphics CreateMeasurementWindowsGraphics() => new WindowsGraphics(DeviceContext.FromCompatibleDC(IntPtr.Zero))
    {
      disposeDc = true
    };

    public static WindowsGraphics CreateMeasurementWindowsGraphics(IntPtr screenDC) => new WindowsGraphics(DeviceContext.FromCompatibleDC(screenDC))
    {
      disposeDc = true
    };

    public static WindowsGraphics FromHwnd(IntPtr hWnd) => new WindowsGraphics(DeviceContext.FromHwnd(hWnd))
    {
      disposeDc = true
    };

    public static WindowsGraphics FromHdc(IntPtr hDc) => new WindowsGraphics(DeviceContext.FromHdc(hDc))
    {
      disposeDc = true
    };

    public static WindowsGraphics FromGraphics(Graphics g)
    {
      ApplyGraphicsProperties properties = ApplyGraphicsProperties.All;
      return WindowsGraphics.FromGraphics(g, properties);
    }

    public static WindowsGraphics FromGraphics(Graphics g, ApplyGraphicsProperties properties)
    {
      WindowsRegion wr = (WindowsRegion) null;
      float[] numArray = (float[]) null;
      Region region = (Region) null;
      Matrix matrix = (Matrix) null;
      if ((properties & ApplyGraphicsProperties.TranslateTransform) != ApplyGraphicsProperties.None || (properties & ApplyGraphicsProperties.Clipping) != ApplyGraphicsProperties.None)
      {
        if (g.GetContextInfo() is object[] contextInfo && contextInfo.Length == 2)
        {
          region = contextInfo[0] as Region;
          matrix = contextInfo[1] as Matrix;
        }
        if (matrix != null)
        {
          if ((properties & ApplyGraphicsProperties.TranslateTransform) != ApplyGraphicsProperties.None)
            numArray = matrix.Elements;
          matrix.Dispose();
        }
        if (region != null)
        {
          if ((properties & ApplyGraphicsProperties.Clipping) != ApplyGraphicsProperties.None && !region.IsInfinite(g))
            wr = WindowsRegion.FromRegion(region, g);
          region.Dispose();
        }
      }
      WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(g.GetHdc());
      windowsGraphics.graphics = g;
      if (wr != null)
      {
        using (wr)
          windowsGraphics.DeviceContext.IntersectClip(wr);
      }
      if (numArray != null)
        windowsGraphics.DeviceContext.TranslateTransform((int) numArray[4], (int) numArray[5]);
      return windowsGraphics;
    }

    ~WindowsGraphics() => this.Dispose(false);

    public DeviceContext DeviceContext => this.dc;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    internal void Dispose(bool disposing)
    {
      if (this.dc == null)
        return;
      try
      {
        this.dc.RestoreHdc();
        if (this.disposeDc)
          this.dc.Dispose(disposing);
        if (this.graphics == null)
          return;
        this.graphics.ReleaseHdcInternal(this.dc.Hdc);
        this.graphics = (Graphics) null;
      }
      catch (Exception ex)
      {
        if (!System.Drawing.ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.dc = (DeviceContext) null;
      }
    }

    public IntPtr GetHdc() => this.dc.Hdc;

    public void ReleaseHdc() => this.dc.Dispose();
  }
}
