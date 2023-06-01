// Decompiled with JetBrains decompiler
// Type: System.Drawing.BufferedGraphics
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing
{
  /// <summary>Provides a graphics buffer for double buffering.</summary>
  public sealed class BufferedGraphics : IDisposable
  {
    private Graphics bufferedGraphicsSurface;
    private Graphics targetGraphics;
    private BufferedGraphicsContext context;
    private IntPtr targetDC;
    private Point targetLoc;
    private Size virtualSize;
    private bool disposeContext;
    private static int rop = 13369376;

    internal BufferedGraphics(
      Graphics bufferedGraphicsSurface,
      BufferedGraphicsContext context,
      Graphics targetGraphics,
      IntPtr targetDC,
      Point targetLoc,
      Size virtualSize)
    {
      this.context = context;
      this.bufferedGraphicsSurface = bufferedGraphicsSurface;
      this.targetDC = targetDC;
      this.targetGraphics = targetGraphics;
      this.targetLoc = targetLoc;
      this.virtualSize = virtualSize;
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~BufferedGraphics() => this.Dispose(false);

    /// <summary>Releases all resources used by the <see cref="T:System.Drawing.BufferedGraphics" /> object.</summary>
    public void Dispose() => this.Dispose(true);

    private void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.context != null)
      {
        this.context.ReleaseBuffer(this);
        if (this.DisposeContext)
        {
          this.context.Dispose();
          this.context = (BufferedGraphicsContext) null;
        }
      }
      if (this.bufferedGraphicsSurface == null)
        return;
      this.bufferedGraphicsSurface.Dispose();
      this.bufferedGraphicsSurface = (Graphics) null;
    }

    internal bool DisposeContext
    {
      get => this.disposeContext;
      set => this.disposeContext = value;
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Graphics" /> object that outputs to the graphics buffer.</summary>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> object that outputs to the graphics buffer.</returns>
    public Graphics Graphics => this.bufferedGraphicsSurface;

    /// <summary>Writes the contents of the graphics buffer to the default device.</summary>
    public void Render()
    {
      if (this.targetGraphics != null)
        this.Render(this.targetGraphics);
      else
        this.RenderInternal(new HandleRef((object) this.Graphics, this.targetDC), this);
    }

    /// <summary>Writes the contents of the graphics buffer to the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
    /// <param name="target">A <see cref="T:System.Drawing.Graphics" /> object to which to write the contents of the graphics buffer.</param>
    public void Render(Graphics target)
    {
      if (target == null)
        return;
      IntPtr hdc = target.GetHdc();
      try
      {
        this.RenderInternal(new HandleRef((object) target, hdc), this);
      }
      finally
      {
        target.ReleaseHdcInternal(hdc);
      }
    }

    /// <summary>Writes the contents of the graphics buffer to the device context associated with the specified <see cref="T:System.IntPtr" /> handle.</summary>
    /// <param name="targetDC">An <see cref="T:System.IntPtr" /> that points to the device context to which to write the contents of the graphics buffer.</param>
    public void Render(IntPtr targetDC)
    {
      IntSecurity.UnmanagedCode.Demand();
      this.RenderInternal(new HandleRef((object) null, targetDC), this);
    }

    private void RenderInternal(HandleRef refTargetDC, BufferedGraphics buffer)
    {
      IntPtr hdc = buffer.Graphics.GetHdc();
      try
      {
        SafeNativeMethods.BitBlt(refTargetDC, this.targetLoc.X, this.targetLoc.Y, this.virtualSize.Width, this.virtualSize.Height, new HandleRef((object) buffer.Graphics, hdc), 0, 0, BufferedGraphics.rop);
      }
      finally
      {
        buffer.Graphics.ReleaseHdcInternal(hdc);
      }
    }
  }
}
