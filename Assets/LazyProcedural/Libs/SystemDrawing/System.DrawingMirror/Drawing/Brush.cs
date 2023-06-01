// Decompiled with JetBrains decompiler
// Type: System.Drawing.Brush
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
  /// <summary>Defines objects used to fill the interiors of graphical shapes such as rectangles, ellipses, pies, polygons, and paths.</summary>
  public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable
  {
    private IntPtr nativeBrush;

    /// <summary>When overridden in a derived class, creates an exact copy of this <see cref="T:System.Drawing.Brush" />.</summary>
    /// <returns>The new <see cref="T:System.Drawing.Brush" /> that this method creates.</returns>
    public abstract object Clone();

    /// <summary>In a derived class, sets a reference to a GDI+ brush object.</summary>
    /// <param name="brush">A pointer to the GDI+ brush object.</param>
    protected internal void SetNativeBrush(IntPtr brush)
    {
      IntSecurity.UnmanagedCode.Demand();
      this.SetNativeBrushInternal(brush);
    }

    internal void SetNativeBrushInternal(IntPtr brush) => this.nativeBrush = brush;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal IntPtr NativeBrush => this.nativeBrush;

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Brush" /> object.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Brush" /> and optionally releases the managed resources.</summary>
    /// <param name="disposing">
    /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!(this.nativeBrush != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDeleteBrush(new HandleRef((object) this, this.nativeBrush));
      }
      catch (Exception ex)
      {
        if (!ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeBrush = IntPtr.Zero;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~Brush() => this.Dispose(false);
  }
}
