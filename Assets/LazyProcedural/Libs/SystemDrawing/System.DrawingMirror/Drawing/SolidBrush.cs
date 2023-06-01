// Decompiled with JetBrains decompiler
// Type: System.Drawing.SolidBrush
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing
{
  /// <summary>Defines a brush of a single color. Brushes are used to fill graphics shapes, such as rectangles, ellipses, pies, polygons, and paths. This class cannot be inherited.</summary>
  public sealed class SolidBrush : Brush, ISystemColorTracker
  {
    private Color color = Color.Empty;
    private bool immutable;

    /// <summary>Initializes a new <see cref="T:System.Drawing.SolidBrush" /> object of the specified color.</summary>
    /// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that represents the color of this brush.</param>
    public SolidBrush(Color color)
    {
      this.color = color;
      IntPtr brush = IntPtr.Zero;
      int solidFill = SafeNativeMethods.Gdip.GdipCreateSolidFill(this.color.ToArgb(), out brush);
      if (solidFill != 0)
        throw SafeNativeMethods.Gdip.StatusException(solidFill);
      this.SetNativeBrushInternal(brush);
      if (!color.IsSystemColor)
        return;
      SystemColorTracker.Add((ISystemColorTracker) this);
    }

    internal SolidBrush(Color color, bool immutable)
      : this(color)
    {
      this.immutable = immutable;
    }

    internal SolidBrush(IntPtr nativeBrush) => this.SetNativeBrushInternal(nativeBrush);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.SolidBrush" /> object.</summary>
    /// <returns>The <see cref="T:System.Drawing.SolidBrush" /> object that this method creates.</returns>
    public override object Clone()
    {
      IntPtr clonebrush = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef((object) this, this.NativeBrush), out clonebrush);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new SolidBrush(clonebrush);
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
        this.immutable = false;
      else if (this.immutable)
        throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) "Brush"));
      base.Dispose(disposing);
    }

    /// <summary>Gets or sets the color of this <see cref="T:System.Drawing.SolidBrush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of this brush.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.SolidBrush.Color" /> property is set on an immutable <see cref="T:System.Drawing.SolidBrush" />.</exception>
    public Color Color
    {
      get
      {
        if (this.color == Color.Empty)
        {
          int color = 0;
          int solidFillColor = SafeNativeMethods.Gdip.GdipGetSolidFillColor(new HandleRef((object) this, this.NativeBrush), out color);
          if (solidFillColor != 0)
            throw SafeNativeMethods.Gdip.StatusException(solidFillColor);
          this.color = Color.FromArgb(color);
        }
        return this.color;
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) "Brush"));
        if (!(this.color != value))
          return;
        Color color = this.color;
        this.InternalSetColor(value);
        if (!value.IsSystemColor || color.IsSystemColor)
          return;
        SystemColorTracker.Add((ISystemColorTracker) this);
      }
    }

    private void InternalSetColor(Color value)
    {
      int status = SafeNativeMethods.Gdip.GdipSetSolidFillColor(new HandleRef((object) this, this.NativeBrush), value.ToArgb());
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.color = value;
    }

    void ISystemColorTracker.OnSystemColorChanged()
    {
      if (!(this.NativeBrush != IntPtr.Zero))
        return;
      this.InternalSetColor(this.color);
    }
  }
}
