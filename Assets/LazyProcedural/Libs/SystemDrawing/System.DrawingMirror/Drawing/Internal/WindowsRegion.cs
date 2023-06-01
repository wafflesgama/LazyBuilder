// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.WindowsRegion
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
  internal sealed class WindowsRegion : MarshalByRefObject, ICloneable, IDisposable
  {
    private IntPtr nativeHandle;
    private bool ownHandle;

    private WindowsRegion()
    {
    }

    public WindowsRegion(Rectangle rect) => this.CreateRegion(rect);

    public WindowsRegion(int x, int y, int width, int height) => this.CreateRegion(new Rectangle(x, y, width, height));

    public static WindowsRegion FromHregion(IntPtr hRegion, bool takeOwnership)
    {
      WindowsRegion windowsRegion = new WindowsRegion();
      if (hRegion != IntPtr.Zero)
      {
        windowsRegion.nativeHandle = hRegion;
        if (takeOwnership)
        {
          windowsRegion.ownHandle = true;
          System.Internal.HandleCollector.Add(hRegion, IntSafeNativeMethods.CommonHandles.GDI);
        }
      }
      return windowsRegion;
    }

    public static WindowsRegion FromRegion(Region region, Graphics g) => region.IsInfinite(g) ? new WindowsRegion() : WindowsRegion.FromHregion(region.GetHrgn(g), true);

    public object Clone() => !this.IsInfinite ? (object) new WindowsRegion(this.ToRectangle()) : (object) new WindowsRegion();

    public IntNativeMethods.RegionFlags CombineRegion(
      WindowsRegion region1,
      WindowsRegion region2,
      RegionCombineMode mode)
    {
      return IntUnsafeNativeMethods.CombineRgn(new HandleRef((object) this, this.HRegion), new HandleRef((object) region1, region1.HRegion), new HandleRef((object) region2, region2.HRegion), mode);
    }

    private void CreateRegion(Rectangle rect)
    {
      this.nativeHandle = IntSafeNativeMethods.CreateRectRgn(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
      this.ownHandle = true;
    }

    public void Dispose() => this.Dispose(true);

    public void Dispose(bool disposing)
    {
      if (!(this.nativeHandle != IntPtr.Zero))
        return;
      if (this.ownHandle)
        IntUnsafeNativeMethods.DeleteObject(new HandleRef((object) this, this.nativeHandle));
      this.nativeHandle = IntPtr.Zero;
      if (!disposing)
        return;
      GC.SuppressFinalize((object) this);
    }

    ~WindowsRegion() => this.Dispose(false);

    public IntPtr HRegion => this.nativeHandle;

    public bool IsInfinite => this.nativeHandle == IntPtr.Zero;

    public Rectangle ToRectangle()
    {
      if (this.IsInfinite)
        return new Rectangle(-2147483647, -2147483647, int.MaxValue, int.MaxValue);
      IntNativeMethods.RECT clipRect = new IntNativeMethods.RECT();
      int rgnBox = (int) IntUnsafeNativeMethods.GetRgnBox(new HandleRef((object) this, this.nativeHandle), ref clipRect);
      return new Rectangle(new Point(clipRect.left, clipRect.top), clipRect.Size);
    }
  }
}
