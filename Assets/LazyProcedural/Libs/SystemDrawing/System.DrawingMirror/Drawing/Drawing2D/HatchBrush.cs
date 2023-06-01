// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.HatchBrush
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
  /// <summary>Defines a rectangular brush with a hatch style, a foreground color, and a background color. This class cannot be inherited.</summary>
  public sealed class HatchBrush : Brush
  {
    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> class with the specified <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> enumeration and foreground color.</summary>
    /// <param name="hatchstyle">One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
    /// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
    public HatchBrush(HatchStyle hatchstyle, Color foreColor)
      : this(hatchstyle, foreColor, Color.FromArgb(-16777216))
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> class with the specified <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> enumeration, foreground color, and background color.</summary>
    /// <param name="hatchstyle">One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
    /// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
    /// <param name="backColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color of spaces between the lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</param>
    public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor)
    {
      IntPtr brush = IntPtr.Zero;
      int hatchBrush = SafeNativeMethods.Gdip.GdipCreateHatchBrush((int) hatchstyle, foreColor.ToArgb(), backColor.ToArgb(), out brush);
      if (hatchBrush != 0)
        throw SafeNativeMethods.Gdip.StatusException(hatchBrush);
      this.SetNativeBrushInternal(brush);
    }

    internal HatchBrush(IntPtr nativeBrush) => this.SetNativeBrushInternal(nativeBrush);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
    /// <returns>The <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> this method creates, cast as an object.</returns>
    public override object Clone()
    {
      IntPtr clonebrush = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef((object) this, this.NativeBrush), out clonebrush);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new HatchBrush(clonebrush);
    }

    /// <summary>Gets the hatch style of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Drawing2D.HatchStyle" /> values that represents the pattern of this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
    public HatchStyle HatchStyle
    {
      get
      {
        int hatchstyle = 0;
        int hatchStyle = SafeNativeMethods.Gdip.GdipGetHatchStyle(new HandleRef((object) this, this.NativeBrush), out hatchstyle);
        if (hatchStyle != 0)
          throw SafeNativeMethods.Gdip.StatusException(hatchStyle);
        return (HatchStyle) hatchstyle;
      }
    }

    /// <summary>Gets the color of hatch lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the foreground color for this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
    public Color ForegroundColor
    {
      get
      {
        int forecol;
        int hatchForegroundColor = SafeNativeMethods.Gdip.GdipGetHatchForegroundColor(new HandleRef((object) this, this.NativeBrush), out forecol);
        if (hatchForegroundColor != 0)
          throw SafeNativeMethods.Gdip.StatusException(hatchForegroundColor);
        return Color.FromArgb(forecol);
      }
    }

    /// <summary>Gets the color of spaces between the hatch lines drawn by this <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the background color for this <see cref="T:System.Drawing.Drawing2D.HatchBrush" />.</returns>
    public Color BackgroundColor
    {
      get
      {
        int backcol;
        int hatchBackgroundColor = SafeNativeMethods.Gdip.GdipGetHatchBackgroundColor(new HandleRef((object) this, this.NativeBrush), out backcol);
        if (hatchBackgroundColor != 0)
          throw SafeNativeMethods.Gdip.StatusException(hatchBackgroundColor);
        return Color.FromArgb(backcol);
      }
    }
  }
}
