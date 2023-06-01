// Decompiled with JetBrains decompiler
// Type: System.Drawing.BufferedGraphicsContext
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Drawing
{
  /// <summary>Provides methods for creating graphics buffers that can be used for double buffering.</summary>
  public sealed class BufferedGraphicsContext : IDisposable
  {
    private Size maximumBuffer;
    private Size bufferSize;
    private Size virtualSize;
    private Point targetLoc;
    private IntPtr compatDC;
    private IntPtr dib;
    private IntPtr oldBitmap;
    private Graphics compatGraphics;
    private BufferedGraphics buffer;
    private int busy;
    private bool invalidateWhenFree;
    private const int BUFFER_FREE = 0;
    private const int BUFFER_BUSY_PAINTING = 1;
    private const int BUFFER_BUSY_DISPOSING = 2;
    private static TraceSwitch doubleBuffering;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.BufferedGraphicsContext" /> class.</summary>
    public BufferedGraphicsContext()
    {
      this.maximumBuffer.Width = 225;
      this.maximumBuffer.Height = 96;
      this.bufferSize = Size.Empty;
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~BufferedGraphicsContext() => this.Dispose(false);

    internal static TraceSwitch DoubleBuffering
    {
      get
      {
        if (BufferedGraphicsContext.doubleBuffering == null)
          BufferedGraphicsContext.doubleBuffering = new TraceSwitch(nameof (DoubleBuffering), "Output information about double buffering");
        return BufferedGraphicsContext.doubleBuffering;
      }
    }

    /// <summary>Gets or sets the maximum size of the buffer to use.</summary>
    /// <returns>A <see cref="T:System.Drawing.Size" /> indicating the maximum size of the buffer dimensions.</returns>
    /// <exception cref="T:System.ArgumentException">The height or width of the size is less than or equal to zero.</exception>
    public Size MaximumBuffer
    {
      get => this.maximumBuffer;
      [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)] set
      {
        if (value.Width <= 0 || value.Height <= 0)
          throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (MaximumBuffer), (object) value));
        if (value.Width * value.Height < this.maximumBuffer.Width * this.maximumBuffer.Height)
          this.Invalidate();
        this.maximumBuffer = value;
      }
    }

    /// <summary>Creates a graphics buffer of the specified size using the pixel format of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="targetGraphics">The <see cref="T:System.Drawing.Graphics" /> to match the pixel format for the new buffer to.</param>
    /// <param name="targetRectangle">A <see cref="T:System.Drawing.Rectangle" /> indicating the size of the buffer to create.</param>
    /// <returns>A <see cref="T:System.Drawing.BufferedGraphics" /> that can be used to draw to a buffer of the specified dimensions.</returns>
    public BufferedGraphics Allocate(Graphics targetGraphics, Rectangle targetRectangle) => this.ShouldUseTempManager(targetRectangle) ? this.AllocBufferInTempManager(targetGraphics, IntPtr.Zero, targetRectangle) : this.AllocBuffer(targetGraphics, IntPtr.Zero, targetRectangle);

    /// <summary>Creates a graphics buffer of the specified size using the pixel format of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="targetDC">An <see cref="T:System.IntPtr" /> to a device context to match the pixel format of the new buffer to.</param>
    /// <param name="targetRectangle">A <see cref="T:System.Drawing.Rectangle" /> indicating the size of the buffer to create.</param>
    /// <returns>A <see cref="T:System.Drawing.BufferedGraphics" /> that can be used to draw to a buffer of the specified dimensions.</returns>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public BufferedGraphics Allocate(IntPtr targetDC, Rectangle targetRectangle) => this.ShouldUseTempManager(targetRectangle) ? this.AllocBufferInTempManager((Graphics) null, targetDC, targetRectangle) : this.AllocBuffer((Graphics) null, targetDC, targetRectangle);

    private BufferedGraphics AllocBuffer(
      Graphics targetGraphics,
      IntPtr targetDC,
      Rectangle targetRectangle)
    {
      if (Interlocked.CompareExchange(ref this.busy, 1, 0) != 0)
        return this.AllocBufferInTempManager(targetGraphics, targetDC, targetRectangle);
      this.targetLoc = new Point(targetRectangle.X, targetRectangle.Y);
      try
      {
        Graphics buffer;
        if (targetGraphics != null)
        {
          IntPtr hdc = targetGraphics.GetHdc();
          try
          {
            buffer = this.CreateBuffer(hdc, -this.targetLoc.X, -this.targetLoc.Y, targetRectangle.Width, targetRectangle.Height);
          }
          finally
          {
            targetGraphics.ReleaseHdcInternal(hdc);
          }
        }
        else
          buffer = this.CreateBuffer(targetDC, -this.targetLoc.X, -this.targetLoc.Y, targetRectangle.Width, targetRectangle.Height);
        this.buffer = new BufferedGraphics(buffer, this, targetGraphics, targetDC, this.targetLoc, this.virtualSize);
      }
      catch
      {
        this.busy = 0;
        throw;
      }
      return this.buffer;
    }

    private BufferedGraphics AllocBufferInTempManager(
      Graphics targetGraphics,
      IntPtr targetDC,
      Rectangle targetRectangle)
    {
      BufferedGraphicsContext bufferedGraphicsContext = (BufferedGraphicsContext) null;
      BufferedGraphics bufferedGraphics = (BufferedGraphics) null;
      try
      {
        bufferedGraphicsContext = new BufferedGraphicsContext();
        if (bufferedGraphicsContext != null)
        {
          bufferedGraphics = bufferedGraphicsContext.AllocBuffer(targetGraphics, targetDC, targetRectangle);
          bufferedGraphics.DisposeContext = true;
        }
      }
      finally
      {
        if (bufferedGraphicsContext != null && (bufferedGraphics == null || bufferedGraphics != null && !bufferedGraphics.DisposeContext))
          bufferedGraphicsContext.Dispose();
      }
      return bufferedGraphics;
    }

    private bool bFillBitmapInfo(IntPtr hdc, IntPtr hpal, ref NativeMethods.BITMAPINFO_FLAT pbmi)
    {
      IntPtr handle = IntPtr.Zero;
      try
      {
        handle = SafeNativeMethods.CreateCompatibleBitmap(new HandleRef((object) null, hdc), 1, 1);
        if (handle == IntPtr.Zero)
          throw new OutOfMemoryException(SR.GetString("GraphicsBufferQueryFail"));
        pbmi.bmiHeader_biSize = Marshal.SizeOf(typeof (NativeMethods.BITMAPINFOHEADER));
        pbmi.bmiColors = new byte[1024];
        SafeNativeMethods.GetDIBits(new HandleRef((object) null, hdc), new HandleRef((object) null, handle), 0, 0, IntPtr.Zero, ref pbmi, 0);
        if (pbmi.bmiHeader_biBitCount <= (short) 8)
          return this.bFillColorTable(hdc, hpal, ref pbmi);
        if (pbmi.bmiHeader_biCompression == 3)
          SafeNativeMethods.GetDIBits(new HandleRef((object) null, hdc), new HandleRef((object) null, handle), 0, pbmi.bmiHeader_biHeight, IntPtr.Zero, ref pbmi, 0);
        return true;
      }
      finally
      {
        if (handle != IntPtr.Zero)
        {
          SafeNativeMethods.DeleteObject(new HandleRef((object) null, handle));
          IntPtr zero = IntPtr.Zero;
        }
      }
    }

    private unsafe bool bFillColorTable(
      IntPtr hdc,
      IntPtr hpal,
      ref NativeMethods.BITMAPINFO_FLAT pbmi)
    {
      bool flag = false;
      byte[] lppe = new byte[sizeof (NativeMethods.PALETTEENTRY) * 256];
      fixed (byte* numPtr1 = pbmi.bmiColors)
        fixed (byte* numPtr2 = lppe)
        {
          NativeMethods.RGBQUAD* rgbquadPtr = (NativeMethods.RGBQUAD*) numPtr1;
          NativeMethods.PALETTEENTRY* paletteentryPtr = (NativeMethods.PALETTEENTRY*) numPtr2;
          int nEntries = 1 << (int) pbmi.bmiHeader_biBitCount;
          if (nEntries <= 256)
          {
            IntPtr zero = IntPtr.Zero;
            if ((!(hpal == IntPtr.Zero) ? (int) SafeNativeMethods.GetPaletteEntries(new HandleRef((object) null, hpal), 0, nEntries, lppe) : (int) SafeNativeMethods.GetPaletteEntries(new HandleRef((object) null, Graphics.GetHalftonePalette()), 0, nEntries, lppe)) != 0)
            {
              for (int index = 0; index < nEntries; ++index)
              {
                rgbquadPtr[index].rgbRed = paletteentryPtr[index].peRed;
                rgbquadPtr[index].rgbGreen = paletteentryPtr[index].peGreen;
                rgbquadPtr[index].rgbBlue = paletteentryPtr[index].peBlue;
                rgbquadPtr[index].rgbReserved = (byte) 0;
              }
              flag = true;
            }
          }
        }
      return flag;
    }

    private Graphics CreateBuffer(IntPtr src, int offsetX, int offsetY, int width, int height)
    {
      this.busy = 2;
      this.DisposeDC();
      this.busy = 1;
      this.compatDC = UnsafeNativeMethods.CreateCompatibleDC(new HandleRef((object) null, src));
      if (width > this.bufferSize.Width || height > this.bufferSize.Height)
      {
        int num1 = Math.Max(width, this.bufferSize.Width);
        int num2 = Math.Max(height, this.bufferSize.Height);
        this.busy = 2;
        this.DisposeBitmap();
        this.busy = 1;
        IntPtr zero = IntPtr.Zero;
        this.dib = this.CreateCompatibleDIB(src, IntPtr.Zero, num1, num2, ref zero);
        this.bufferSize = new Size(num1, num2);
      }
      this.oldBitmap = SafeNativeMethods.SelectObject(new HandleRef((object) this, this.compatDC), new HandleRef((object) this, this.dib));
      this.compatGraphics = Graphics.FromHdcInternal(this.compatDC);
      this.compatGraphics.TranslateTransform((float) -this.targetLoc.X, (float) -this.targetLoc.Y);
      this.virtualSize = new Size(width, height);
      return this.compatGraphics;
    }

    private IntPtr CreateCompatibleDIB(
      IntPtr hdc,
      IntPtr hpal,
      int ulWidth,
      int ulHeight,
      ref IntPtr ppvBits)
    {
      IntPtr compatibleDib = !(hdc == IntPtr.Zero) ? IntPtr.Zero : throw new ArgumentNullException(nameof (hdc));
      NativeMethods.BITMAPINFO_FLAT bitmapinfoFlat = new NativeMethods.BITMAPINFO_FLAT();
      switch (UnsafeNativeMethods.GetObjectType(new HandleRef((object) null, hdc)))
      {
        case 3:
        case 4:
        case 10:
        case 12:
          if (this.bFillBitmapInfo(hdc, hpal, ref bitmapinfoFlat))
          {
            bitmapinfoFlat.bmiHeader_biWidth = ulWidth;
            bitmapinfoFlat.bmiHeader_biHeight = ulHeight;
            bitmapinfoFlat.bmiHeader_biSizeImage = bitmapinfoFlat.bmiHeader_biCompression != 0 ? (bitmapinfoFlat.bmiHeader_biBitCount != (short) 16 ? (bitmapinfoFlat.bmiHeader_biBitCount != (short) 32 ? 0 : ulWidth * ulHeight * 4) : ulWidth * ulHeight * 2) : 0;
            bitmapinfoFlat.bmiHeader_biClrUsed = 0;
            bitmapinfoFlat.bmiHeader_biClrImportant = 0;
            compatibleDib = SafeNativeMethods.CreateDIBSection(new HandleRef((object) null, hdc), ref bitmapinfoFlat, 0, ref ppvBits, IntPtr.Zero, 0);
            Win32Exception win32Exception = (Win32Exception) null;
            if (compatibleDib == IntPtr.Zero)
              win32Exception = new Win32Exception(Marshal.GetLastWin32Error());
            if (win32Exception != null)
              throw win32Exception;
          }
          return compatibleDib;
        default:
          throw new ArgumentException(SR.GetString("DCTypeInvalid"));
      }
    }

    /// <summary>Releases all resources used by the <see cref="T:System.Drawing.BufferedGraphicsContext" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void DisposeDC()
    {
      if (this.oldBitmap != IntPtr.Zero && this.compatDC != IntPtr.Zero)
      {
        SafeNativeMethods.SelectObject(new HandleRef((object) this, this.compatDC), new HandleRef((object) this, this.oldBitmap));
        this.oldBitmap = IntPtr.Zero;
      }
      if (!(this.compatDC != IntPtr.Zero))
        return;
      UnsafeNativeMethods.DeleteDC(new HandleRef((object) this, this.compatDC));
      this.compatDC = IntPtr.Zero;
    }

    private void DisposeBitmap()
    {
      if (!(this.dib != IntPtr.Zero))
        return;
      SafeNativeMethods.DeleteObject(new HandleRef((object) this, this.dib));
      this.dib = IntPtr.Zero;
    }

    private void Dispose(bool disposing)
    {
      int num = Interlocked.CompareExchange(ref this.busy, 2, 0);
      if (disposing)
      {
        if (num == 1)
          throw new InvalidOperationException(SR.GetString("GraphicsBufferCurrentlyBusy"));
        if (this.compatGraphics != null)
        {
          this.compatGraphics.Dispose();
          this.compatGraphics = (Graphics) null;
        }
      }
      this.DisposeDC();
      this.DisposeBitmap();
      if (this.buffer != null)
      {
        this.buffer.Dispose();
        this.buffer = (BufferedGraphics) null;
      }
      this.bufferSize = Size.Empty;
      this.virtualSize = Size.Empty;
      this.busy = 0;
    }

    /// <summary>Disposes of the current graphics buffer, if a buffer has been allocated and has not yet been disposed.</summary>
    public void Invalidate()
    {
      if (Interlocked.CompareExchange(ref this.busy, 2, 0) == 0)
      {
        this.Dispose();
        this.busy = 0;
      }
      else
        this.invalidateWhenFree = true;
    }

    internal void ReleaseBuffer(BufferedGraphics buffer)
    {
      this.buffer = (BufferedGraphics) null;
      if (this.invalidateWhenFree)
      {
        this.busy = 2;
        this.Dispose();
      }
      else
      {
        this.busy = 2;
        this.DisposeDC();
      }
      this.busy = 0;
    }

    private bool ShouldUseTempManager(Rectangle targetBounds)
    {
      int num1 = targetBounds.Width * targetBounds.Height;
      Size maximumBuffer = this.MaximumBuffer;
      int width = maximumBuffer.Width;
      maximumBuffer = this.MaximumBuffer;
      int height = maximumBuffer.Height;
      int num2 = width * height;
      return num1 > num2;
    }
  }
}
