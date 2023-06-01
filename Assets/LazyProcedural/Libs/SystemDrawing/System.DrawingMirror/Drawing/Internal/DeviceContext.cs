// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.DeviceContext
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
  internal sealed class DeviceContext : MarshalByRefObject, IDeviceContext, IDisposable
  {
    private IntPtr hDC;
    private DeviceContextType dcType;
    private bool disposed;
    private IntPtr hWnd = (IntPtr) -1;
    private IntPtr hInitialPen;
    private IntPtr hInitialBrush;
    private IntPtr hInitialBmp;
    private IntPtr hInitialFont;
    private IntPtr hCurrentPen;
    private IntPtr hCurrentBrush;
    private IntPtr hCurrentBmp;
    private IntPtr hCurrentFont;
    private Stack contextStack;

    public event EventHandler Disposing;

    public DeviceContextType DeviceContextType => this.dcType;

    public IntPtr Hdc
    {
      get
      {
        if (this.hDC == IntPtr.Zero && this.dcType == DeviceContextType.Display)
        {
          this.hDC = ((IDeviceContext) this).GetHdc();
          this.CacheInitialState();
        }
        return this.hDC;
      }
    }

    private void CacheInitialState()
    {
      this.hCurrentPen = this.hInitialPen = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef((object) this, this.hDC), 1);
      this.hCurrentBrush = this.hInitialBrush = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef((object) this, this.hDC), 2);
      this.hCurrentBmp = this.hInitialBmp = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef((object) this, this.hDC), 7);
      this.hCurrentFont = this.hInitialFont = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef((object) this, this.hDC), 6);
    }

    public void DeleteObject(IntPtr handle, GdiObjectType type)
    {
      IntPtr handle1 = IntPtr.Zero;
      switch (type)
      {
        case GdiObjectType.Pen:
          if (handle == this.hCurrentPen)
          {
            IntUnsafeNativeMethods.SelectObject(new HandleRef((object) this, this.Hdc), new HandleRef((object) this, this.hInitialPen));
            this.hCurrentPen = IntPtr.Zero;
          }
          handle1 = handle;
          break;
        case GdiObjectType.Brush:
          if (handle == this.hCurrentBrush)
          {
            IntUnsafeNativeMethods.SelectObject(new HandleRef((object) this, this.Hdc), new HandleRef((object) this, this.hInitialBrush));
            this.hCurrentBrush = IntPtr.Zero;
          }
          handle1 = handle;
          break;
        case GdiObjectType.Bitmap:
          if (handle == this.hCurrentBmp)
          {
            IntUnsafeNativeMethods.SelectObject(new HandleRef((object) this, this.Hdc), new HandleRef((object) this, this.hInitialBmp));
            this.hCurrentBmp = IntPtr.Zero;
          }
          handle1 = handle;
          break;
      }
      IntUnsafeNativeMethods.DeleteObject(new HandleRef((object) this, handle1));
    }

    private DeviceContext(IntPtr hWnd)
    {
      this.hWnd = hWnd;
      this.dcType = DeviceContextType.Display;
      DeviceContexts.AddDeviceContext(this);
    }

    private DeviceContext(IntPtr hDC, DeviceContextType dcType)
    {
      this.hDC = hDC;
      this.dcType = dcType;
      this.CacheInitialState();
      DeviceContexts.AddDeviceContext(this);
      if (dcType != DeviceContextType.Display)
        return;
      this.hWnd = IntUnsafeNativeMethods.WindowFromDC(new HandleRef((object) this, this.hDC));
    }

    public static DeviceContext CreateDC(
      string driverName,
      string deviceName,
      string fileName,
      HandleRef devMode)
    {
      return new DeviceContext(IntUnsafeNativeMethods.CreateDC(driverName, deviceName, fileName, devMode), DeviceContextType.NamedDevice);
    }

    public static DeviceContext CreateIC(
      string driverName,
      string deviceName,
      string fileName,
      HandleRef devMode)
    {
      return new DeviceContext(IntUnsafeNativeMethods.CreateIC(driverName, deviceName, fileName, devMode), DeviceContextType.Information);
    }

    public static DeviceContext FromCompatibleDC(IntPtr hdc) => new DeviceContext(IntUnsafeNativeMethods.CreateCompatibleDC(new HandleRef((object) null, hdc)), DeviceContextType.Memory);

    public static DeviceContext FromHdc(IntPtr hdc) => new DeviceContext(hdc, DeviceContextType.Unknown);

    public static DeviceContext FromHwnd(IntPtr hwnd) => new DeviceContext(hwnd);

    ~DeviceContext() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    internal void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (this.Disposing != null)
        this.Disposing((object) this, EventArgs.Empty);
      this.disposed = true;
      switch (this.dcType)
      {
        case DeviceContextType.Display:
          ((IDeviceContext) this).ReleaseHdc();
          break;
        case DeviceContextType.NamedDevice:
        case DeviceContextType.Information:
          IntUnsafeNativeMethods.DeleteHDC(new HandleRef((object) this, this.hDC));
          this.hDC = IntPtr.Zero;
          break;
        case DeviceContextType.Memory:
          IntUnsafeNativeMethods.DeleteDC(new HandleRef((object) this, this.hDC));
          this.hDC = IntPtr.Zero;
          break;
      }
    }

    IntPtr IDeviceContext.GetHdc()
    {
      if (this.hDC == IntPtr.Zero)
        this.hDC = IntUnsafeNativeMethods.GetDC(new HandleRef((object) this, this.hWnd));
      return this.hDC;
    }

    void IDeviceContext.ReleaseHdc()
    {
      if (!(this.hDC != IntPtr.Zero) || this.dcType != DeviceContextType.Display)
        return;
      IntUnsafeNativeMethods.ReleaseDC(new HandleRef((object) this, this.hWnd), new HandleRef((object) this, this.hDC));
      this.hDC = IntPtr.Zero;
    }

    public DeviceContextGraphicsMode GraphicsMode => (DeviceContextGraphicsMode) IntUnsafeNativeMethods.GetGraphicsMode(new HandleRef((object) this, this.Hdc));

    public DeviceContextGraphicsMode SetGraphicsMode(DeviceContextGraphicsMode newMode) => (DeviceContextGraphicsMode) IntUnsafeNativeMethods.SetGraphicsMode(new HandleRef((object) this, this.Hdc), (int) newMode);

    public void RestoreHdc()
    {
      IntUnsafeNativeMethods.RestoreDC(new HandleRef((object) this, this.hDC), -1);
      if (this.contextStack == null)
        return;
      DeviceContext.GraphicsState graphicsState = (DeviceContext.GraphicsState) this.contextStack.Pop();
      this.hCurrentBmp = graphicsState.hBitmap;
      this.hCurrentBrush = graphicsState.hBrush;
      this.hCurrentPen = graphicsState.hPen;
      this.hCurrentFont = graphicsState.hFont;
    }

    public int SaveHdc()
    {
      int num = IntUnsafeNativeMethods.SaveDC(new HandleRef((object) this, this.Hdc));
      if (this.contextStack == null)
        this.contextStack = new Stack();
      this.contextStack.Push((object) new DeviceContext.GraphicsState()
      {
        hBitmap = this.hCurrentBmp,
        hBrush = this.hCurrentBrush,
        hPen = this.hCurrentPen,
        hFont = this.hCurrentFont
      });
      return num;
    }

    public void SetClip(WindowsRegion region)
    {
      int num = (int) IntUnsafeNativeMethods.SelectClipRgn(new HandleRef((object) this, this.Hdc), new HandleRef((object) region, region.HRegion));
    }

    public void IntersectClip(WindowsRegion wr)
    {
      if (wr.HRegion == IntPtr.Zero)
        return;
      using (WindowsRegion windowsRegion = new WindowsRegion(0, 0, 0, 0))
      {
        if (IntUnsafeNativeMethods.GetClipRgn(new HandleRef((object) this, this.Hdc), new HandleRef((object) windowsRegion, windowsRegion.HRegion)) == 1)
        {
          int num = (int) wr.CombineRegion(windowsRegion, wr, RegionCombineMode.AND);
        }
        this.SetClip(wr);
      }
    }

    public void TranslateTransform(int dx, int dy)
    {
      IntNativeMethods.POINT point = new IntNativeMethods.POINT();
      IntUnsafeNativeMethods.OffsetViewportOrgEx(new HandleRef((object) this, this.Hdc), dx, dy, point);
    }

    public override bool Equals(object obj)
    {
      DeviceContext deviceContext = obj as DeviceContext;
      if (deviceContext == this)
        return true;
      return deviceContext != null && deviceContext.Hdc == this.Hdc;
    }

    public override int GetHashCode() => this.Hdc.GetHashCode();

    internal class GraphicsState
    {
      internal IntPtr hBrush;
      internal IntPtr hFont;
      internal IntPtr hPen;
      internal IntPtr hBitmap;
    }
  }
}
