// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.LinearGradientBrush
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
  /// <summary>Encapsulates a <see cref="T:System.Drawing.Brush" /> with a linear gradient. This class cannot be inherited.</summary>
  public sealed class LinearGradientBrush : Brush
  {
    private bool interpolationColorsWasSet;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class with the specified points and colors.</summary>
    /// <param name="point1">A <see cref="T:System.Drawing.PointF" /> structure that represents the starting point of the linear gradient.</param>
    /// <param name="point2">A <see cref="T:System.Drawing.PointF" /> structure that represents the endpoint of the linear gradient.</param>
    /// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color of the linear gradient.</param>
    /// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color of the linear gradient.</param>
    public LinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2)
    {
      IntPtr lineGradient = IntPtr.Zero;
      int lineBrush = SafeNativeMethods.Gdip.GdipCreateLineBrush(new GPPOINTF(point1), new GPPOINTF(point2), color1.ToArgb(), color2.ToArgb(), 0, out lineGradient);
      if (lineBrush != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineBrush);
      this.SetNativeBrushInternal(lineGradient);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class with the specified points and colors.</summary>
    /// <param name="point1">A <see cref="T:System.Drawing.Point" /> structure that represents the starting point of the linear gradient.</param>
    /// <param name="point2">A <see cref="T:System.Drawing.Point" /> structure that represents the endpoint of the linear gradient.</param>
    /// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color of the linear gradient.</param>
    /// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color of the linear gradient.</param>
    public LinearGradientBrush(Point point1, Point point2, Color color1, Color color2)
    {
      IntPtr lineGradient = IntPtr.Zero;
      int lineBrushI = SafeNativeMethods.Gdip.GdipCreateLineBrushI(new GPPOINT(point1), new GPPOINT(point2), color1.ToArgb(), color2.ToArgb(), 0, out lineGradient);
      if (lineBrushI != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineBrushI);
      this.SetNativeBrushInternal(lineGradient);
    }

    /// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> based on a rectangle, starting and ending colors, and an orientation mode.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient.</param>
    /// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
    /// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
    /// <param name="linearGradientMode">A <see cref="T:System.Drawing.Drawing2D.LinearGradientMode" /> enumeration element that specifies the orientation of the gradient. The orientation determines the starting and ending points of the gradient. For example, <see langword="LinearGradientMode.ForwardDiagonal" /> specifies that the starting point is the upper-left corner of the rectangle and the ending point is the lower-right corner of the rectangle.</param>
    public LinearGradientBrush(
      RectangleF rect,
      Color color1,
      Color color2,
      LinearGradientMode linearGradientMode)
    {
      if (!System.Drawing.ClientUtils.IsEnumValid((Enum) linearGradientMode, (int) linearGradientMode, 0, 3))
        throw new InvalidEnumArgumentException(nameof (linearGradientMode), (int) linearGradientMode, typeof (LinearGradientMode));
      if ((double) rect.Width == 0.0 || (double) rect.Height == 0.0)
        throw new ArgumentException(System.Drawing.SR.GetString("GdiplusInvalidRectangle", (object) rect.ToString()));
      IntPtr lineGradient = IntPtr.Zero;
      GPRECTF rect1 = new GPRECTF(rect);
      int lineBrushFromRect = SafeNativeMethods.Gdip.GdipCreateLineBrushFromRect(ref rect1, color1.ToArgb(), color2.ToArgb(), (int) linearGradientMode, 0, out lineGradient);
      if (lineBrushFromRect != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineBrushFromRect);
      this.SetNativeBrushInternal(lineGradient);
    }

    /// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and orientation.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient.</param>
    /// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
    /// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
    /// <param name="linearGradientMode">A <see cref="T:System.Drawing.Drawing2D.LinearGradientMode" /> enumeration element that specifies the orientation of the gradient. The orientation determines the starting and ending points of the gradient. For example, <see langword="LinearGradientMode.ForwardDiagonal" /> specifies that the starting point is the upper-left corner of the rectangle and the ending point is the lower-right corner of the rectangle.</param>
    public LinearGradientBrush(
      System.Drawing.Rectangle rect,
      Color color1,
      Color color2,
      LinearGradientMode linearGradientMode)
    {
      if (!System.Drawing.ClientUtils.IsEnumValid((Enum) linearGradientMode, (int) linearGradientMode, 0, 3))
        throw new InvalidEnumArgumentException(nameof (linearGradientMode), (int) linearGradientMode, typeof (LinearGradientMode));
      if (rect.Width == 0 || rect.Height == 0)
        throw new ArgumentException(System.Drawing.SR.GetString("GdiplusInvalidRectangle", (object) rect.ToString()));
      IntPtr lineGradient = IntPtr.Zero;
      GPRECT rect1 = new GPRECT(rect);
      int lineBrushFromRectI = SafeNativeMethods.Gdip.GdipCreateLineBrushFromRectI(ref rect1, color1.ToArgb(), color2.ToArgb(), (int) linearGradientMode, 0, out lineGradient);
      if (lineBrushFromRectI != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineBrushFromRectI);
      this.SetNativeBrushInternal(lineGradient);
    }

    /// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient.</param>
    /// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
    /// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
    /// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line.</param>
    public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle)
      : this(rect, color1, color2, angle, false)
    {
    }

    /// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the bounds of the linear gradient.</param>
    /// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
    /// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
    /// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line.</param>
    /// <param name="isAngleScaleable">Set to <see langword="true" /> to specify that the angle is affected by the transform associated with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, <see langword="false" />.</param>
    public LinearGradientBrush(
      RectangleF rect,
      Color color1,
      Color color2,
      float angle,
      bool isAngleScaleable)
    {
      IntPtr lineGradient = IntPtr.Zero;
      if ((double) rect.Width == 0.0 || (double) rect.Height == 0.0)
        throw new ArgumentException(System.Drawing.SR.GetString("GdiplusInvalidRectangle", (object) rect.ToString()));
      GPRECTF rect1 = new GPRECTF(rect);
      int fromRectWithAngle = SafeNativeMethods.Gdip.GdipCreateLineBrushFromRectWithAngle(ref rect1, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, 0, out lineGradient);
      if (fromRectWithAngle != 0)
        throw SafeNativeMethods.Gdip.StatusException(fromRectWithAngle);
      this.SetNativeBrushInternal(lineGradient);
    }

    /// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient.</param>
    /// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
    /// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
    /// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line.</param>
    public LinearGradientBrush(System.Drawing.Rectangle rect, Color color1, Color color2, float angle)
      : this(rect, color1, color2, angle, false)
    {
    }

    /// <summary>Creates a new instance of the <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> class based on a rectangle, starting and ending colors, and an orientation angle.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the bounds of the linear gradient.</param>
    /// <param name="color1">A <see cref="T:System.Drawing.Color" /> structure that represents the starting color for the gradient.</param>
    /// <param name="color2">A <see cref="T:System.Drawing.Color" /> structure that represents the ending color for the gradient.</param>
    /// <param name="angle">The angle, measured in degrees clockwise from the x-axis, of the gradient's orientation line.</param>
    /// <param name="isAngleScaleable">Set to <see langword="true" /> to specify that the angle is affected by the transform associated with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, <see langword="false" />.</param>
    public LinearGradientBrush(
      System.Drawing.Rectangle rect,
      Color color1,
      Color color2,
      float angle,
      bool isAngleScaleable)
    {
      IntPtr lineGradient = IntPtr.Zero;
      if (rect.Width == 0 || rect.Height == 0)
        throw new ArgumentException(System.Drawing.SR.GetString("GdiplusInvalidRectangle", (object) rect.ToString()));
      GPRECT rect1 = new GPRECT(rect);
      int fromRectWithAngleI = SafeNativeMethods.Gdip.GdipCreateLineBrushFromRectWithAngleI(ref rect1, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, 0, out lineGradient);
      if (fromRectWithAngleI != 0)
        throw SafeNativeMethods.Gdip.StatusException(fromRectWithAngleI);
      this.SetNativeBrushInternal(lineGradient);
    }

    internal LinearGradientBrush(IntPtr nativeBrush) => this.SetNativeBrushInternal(nativeBrush);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> this method creates, cast as an object.</returns>
    public override object Clone()
    {
      IntPtr clonebrush = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef((object) this, this.NativeBrush), out clonebrush);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new LinearGradientBrush(clonebrush);
    }

    private void _SetLinearColors(Color color1, Color color2)
    {
      int status = SafeNativeMethods.Gdip.GdipSetLineColors(new HandleRef((object) this, this.NativeBrush), color1.ToArgb(), color2.ToArgb());
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private Color[] _GetLinearColors()
    {
      int[] colors = new int[2];
      int lineColors = SafeNativeMethods.Gdip.GdipGetLineColors(new HandleRef((object) this, this.NativeBrush), colors);
      if (lineColors != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineColors);
      return new Color[2]
      {
        Color.FromArgb(colors[0]),
        Color.FromArgb(colors[1])
      };
    }

    /// <summary>Gets or sets the starting and ending colors of the gradient.</summary>
    /// <returns>An array of two <see cref="T:System.Drawing.Color" /> structures that represents the starting and ending colors of the gradient.</returns>
    public Color[] LinearColors
    {
      get => this._GetLinearColors();
      set => this._SetLinearColors(value[0], value[1]);
    }

    private RectangleF _GetRectangle()
    {
      GPRECTF gprectf = new GPRECTF();
      int lineRect = SafeNativeMethods.Gdip.GdipGetLineRect(new HandleRef((object) this, this.NativeBrush), ref gprectf);
      if (lineRect != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineRect);
      return gprectf.ToRectangleF();
    }

    /// <summary>Gets a rectangular region that defines the starting and ending points of the gradient.</summary>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that specifies the starting and ending points of the gradient.</returns>
    public RectangleF Rectangle => this._GetRectangle();

    /// <summary>Gets or sets a value indicating whether gamma correction is enabled for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
    /// <returns>The value is <see langword="true" /> if gamma correction is enabled for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />; otherwise, <see langword="false" />.</returns>
    public bool GammaCorrection
    {
      get
      {
        bool useGammaCorrection;
        int lineGammaCorrection = SafeNativeMethods.Gdip.GdipGetLineGammaCorrection(new HandleRef((object) this, this.NativeBrush), out useGammaCorrection);
        if (lineGammaCorrection != 0)
          throw SafeNativeMethods.Gdip.StatusException(lineGammaCorrection);
        return useGammaCorrection;
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetLineGammaCorrection(new HandleRef((object) this, this.NativeBrush), value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    private Blend _GetBlend()
    {
      if (this.interpolationColorsWasSet)
        return (Blend) null;
      int count = 0;
      int lineBlendCount = SafeNativeMethods.Gdip.GdipGetLineBlendCount(new HandleRef((object) this, this.NativeBrush), out count);
      if (lineBlendCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineBlendCount);
      if (count <= 0)
        return (Blend) null;
      int num1 = count;
      IntPtr num2 = IntPtr.Zero;
      IntPtr num3 = IntPtr.Zero;
      Blend blend;
      try
      {
        int cb = checked (4 * num1);
        num2 = Marshal.AllocHGlobal(cb);
        num3 = Marshal.AllocHGlobal(cb);
        int lineBlend = SafeNativeMethods.Gdip.GdipGetLineBlend(new HandleRef((object) this, this.NativeBrush), num2, num3, num1);
        if (lineBlend != 0)
          throw SafeNativeMethods.Gdip.StatusException(lineBlend);
        blend = new Blend(num1);
        Marshal.Copy(num2, blend.Factors, 0, num1);
        Marshal.Copy(num3, blend.Positions, 0, num1);
      }
      finally
      {
        if (num2 != IntPtr.Zero)
          Marshal.FreeHGlobal(num2);
        if (num3 != IntPtr.Zero)
          Marshal.FreeHGlobal(num3);
      }
      return blend;
    }

    private void _SetBlend(Blend blend)
    {
      int length = blend.Factors.Length;
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      try
      {
        int cb = checked (4 * length);
        num1 = Marshal.AllocHGlobal(cb);
        num2 = Marshal.AllocHGlobal(cb);
        Marshal.Copy(blend.Factors, 0, num1, length);
        Marshal.Copy(blend.Positions, 0, num2, length);
        int status = SafeNativeMethods.Gdip.GdipSetLineBlend(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) null, num1), new HandleRef((object) null, num2), length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        if (num1 != IntPtr.Zero)
          Marshal.FreeHGlobal(num1);
        if (num2 != IntPtr.Zero)
          Marshal.FreeHGlobal(num2);
      }
    }

    /// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.Blend" /> that specifies positions and factors that define a custom falloff for the gradient.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.Blend" /> that represents a custom falloff for the gradient.</returns>
    public Blend Blend
    {
      get => this._GetBlend();
      set => this._SetBlend(value);
    }

    /// <summary>Creates a gradient falloff based on a bell-shaped curve.</summary>
    /// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the starting color and ending color are blended equally).</param>
    public void SetSigmaBellShape(float focus) => this.SetSigmaBellShape(focus, 1f);

    /// <summary>Creates a gradient falloff based on a bell-shaped curve.</summary>
    /// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color).</param>
    /// <param name="scale">A value from 0 through 1 that specifies how fast the colors falloff from the <paramref name="focus" />.</param>
    public void SetSigmaBellShape(float focus, float scale)
    {
      int status = SafeNativeMethods.Gdip.GdipSetLineSigmaBlend(new HandleRef((object) this, this.NativeBrush), focus, scale);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Creates a linear gradient with a center color and a linear falloff to a single color on both ends.</summary>
    /// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color).</param>
    public void SetBlendTriangularShape(float focus) => this.SetBlendTriangularShape(focus, 1f);

    /// <summary>Creates a linear gradient with a center color and a linear falloff to a single color on both ends.</summary>
    /// <param name="focus">A value from 0 through 1 that specifies the center of the gradient (the point where the gradient is composed of only the ending color).</param>
    /// <param name="scale">A value from 0 through1 that specifies how fast the colors falloff from the starting color to <paramref name="focus" /> (ending color)</param>
    public void SetBlendTriangularShape(float focus, float scale)
    {
      int status = SafeNativeMethods.Gdip.GdipSetLineLinearBlend(new HandleRef((object) this, this.NativeBrush), focus, scale);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private ColorBlend _GetInterpolationColors()
    {
      if (!this.interpolationColorsWasSet)
        throw new ArgumentException(System.Drawing.SR.GetString("InterpolationColorsCommon", (object) System.Drawing.SR.GetString("InterpolationColorsColorBlendNotSet"), (object) ""));
      int count = 0;
      int presetBlendCount = SafeNativeMethods.Gdip.GdipGetLinePresetBlendCount(new HandleRef((object) this, this.NativeBrush), out count);
      if (presetBlendCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(presetBlendCount);
      int length = count;
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      ColorBlend interpolationColors;
      try
      {
        int cb = checked (4 * length);
        num1 = Marshal.AllocHGlobal(cb);
        num2 = Marshal.AllocHGlobal(cb);
        int linePresetBlend = SafeNativeMethods.Gdip.GdipGetLinePresetBlend(new HandleRef((object) this, this.NativeBrush), num1, num2, length);
        if (linePresetBlend != 0)
          throw SafeNativeMethods.Gdip.StatusException(linePresetBlend);
        interpolationColors = new ColorBlend(length);
        int[] destination = new int[length];
        Marshal.Copy(num1, destination, 0, length);
        Marshal.Copy(num2, interpolationColors.Positions, 0, length);
        interpolationColors.Colors = new Color[destination.Length];
        for (int index = 0; index < destination.Length; ++index)
          interpolationColors.Colors[index] = Color.FromArgb(destination[index]);
      }
      finally
      {
        if (num1 != IntPtr.Zero)
          Marshal.FreeHGlobal(num1);
        if (num2 != IntPtr.Zero)
          Marshal.FreeHGlobal(num2);
      }
      return interpolationColors;
    }

    private void _SetInterpolationColors(ColorBlend blend)
    {
      this.interpolationColorsWasSet = true;
      if (blend == null)
        throw new ArgumentException(System.Drawing.SR.GetString("InterpolationColorsCommon", (object) System.Drawing.SR.GetString("InterpolationColorsInvalidColorBlendObject"), (object) ""));
      if (blend.Colors.Length < 2)
        throw new ArgumentException(System.Drawing.SR.GetString("InterpolationColorsCommon", (object) System.Drawing.SR.GetString("InterpolationColorsInvalidColorBlendObject"), (object) System.Drawing.SR.GetString("InterpolationColorsLength")));
      if (blend.Colors.Length != blend.Positions.Length)
        throw new ArgumentException(System.Drawing.SR.GetString("InterpolationColorsCommon", (object) System.Drawing.SR.GetString("InterpolationColorsInvalidColorBlendObject"), (object) System.Drawing.SR.GetString("InterpolationColorsLengthsDiffer")));
      if ((double) blend.Positions[0] != 0.0)
        throw new ArgumentException(System.Drawing.SR.GetString("InterpolationColorsCommon", (object) System.Drawing.SR.GetString("InterpolationColorsInvalidColorBlendObject"), (object) System.Drawing.SR.GetString("InterpolationColorsInvalidStartPosition")));
      if ((double) blend.Positions[blend.Positions.Length - 1] != 1.0)
        throw new ArgumentException(System.Drawing.SR.GetString("InterpolationColorsCommon", (object) System.Drawing.SR.GetString("InterpolationColorsInvalidColorBlendObject"), (object) System.Drawing.SR.GetString("InterpolationColorsInvalidEndPosition")));
      int length = blend.Colors.Length;
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      try
      {
        int cb = checked (4 * length);
        num1 = Marshal.AllocHGlobal(cb);
        num2 = Marshal.AllocHGlobal(cb);
        int[] source = new int[length];
        for (int index = 0; index < length; ++index)
          source[index] = blend.Colors[index].ToArgb();
        Marshal.Copy(source, 0, num1, length);
        Marshal.Copy(blend.Positions, 0, num2, length);
        int status = SafeNativeMethods.Gdip.GdipSetLinePresetBlend(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) null, num1), new HandleRef((object) null, num2), length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        if (num1 != IntPtr.Zero)
          Marshal.FreeHGlobal(num1);
        if (num2 != IntPtr.Zero)
          Marshal.FreeHGlobal(num2);
      }
    }

    /// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> that defines a multicolor linear gradient.</returns>
    public ColorBlend InterpolationColors
    {
      get => this._GetInterpolationColors();
      set => this._SetInterpolationColors(value);
    }

    private void _SetWrapMode(WrapMode wrapMode)
    {
      int status = SafeNativeMethods.Gdip.GdipSetLineWrapMode(new HandleRef((object) this, this.NativeBrush), (int) wrapMode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private WrapMode _GetWrapMode()
    {
      int wrapMode = 0;
      int lineWrapMode = SafeNativeMethods.Gdip.GdipGetLineWrapMode(new HandleRef((object) this, this.NativeBrush), out wrapMode);
      if (lineWrapMode != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineWrapMode);
      return (WrapMode) wrapMode;
    }

    /// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that indicates the wrap mode for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> are tiled.</returns>
    public WrapMode WrapMode
    {
      get => this._GetWrapMode();
      set
      {
        if (!System.Drawing.ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 4))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (WrapMode));
        this._SetWrapMode(value);
      }
    }

    private void _SetTransform(Matrix matrix)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int status = SafeNativeMethods.Gdip.GdipSetLineTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) matrix, matrix.nativeMatrix));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private Matrix _GetTransform()
    {
      Matrix wrapper = new Matrix();
      int lineTransform = SafeNativeMethods.Gdip.GdipGetLineTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) wrapper, wrapper.nativeMatrix));
      if (lineTransform != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineTransform);
      return wrapper;
    }

    /// <summary>Gets or sets a copy <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a local geometric transform for this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</summary>
    /// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies only to fills drawn with this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" />.</returns>
    public Matrix Transform
    {
      get => this._GetTransform();
      set => this._SetTransform(value);
    }

    /// <summary>Resets the <see cref="P:System.Drawing.Drawing2D.LinearGradientBrush.Transform" /> property to identity.</summary>
    public void ResetTransform()
    {
      int status = SafeNativeMethods.Gdip.GdipResetLineTransform(new HandleRef((object) this, this.NativeBrush));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Multiplies the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the local geometric transform of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the geometric transform.</param>
    public void MultiplyTransform(Matrix matrix) => this.MultiplyTransform(matrix, MatrixOrder.Prepend);

    /// <summary>Multiplies the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the local geometric transform of this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the geometric transform.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies in which order to multiply the two matrices.</param>
    public void MultiplyTransform(Matrix matrix, MatrixOrder order)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int status = SafeNativeMethods.Gdip.GdipMultiplyLineTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) matrix, matrix.nativeMatrix), order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Translates the local geometric transform by the specified dimensions. This method prepends the translation to the transform.</summary>
    /// <param name="dx">The value of the translation in x.</param>
    /// <param name="dy">The value of the translation in y.</param>
    public void TranslateTransform(float dx, float dy) => this.TranslateTransform(dx, dy, MatrixOrder.Prepend);

    /// <summary>Translates the local geometric transform by the specified dimensions in the specified order.</summary>
    /// <param name="dx">The value of the translation in x.</param>
    /// <param name="dy">The value of the translation in y.</param>
    /// <param name="order">The order (prepend or append) in which to apply the translation.</param>
    public void TranslateTransform(float dx, float dy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslateLineTransform(new HandleRef((object) this, this.NativeBrush), dx, dy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Scales the local geometric transform by the specified amounts. This method prepends the scaling matrix to the transform.</summary>
    /// <param name="sx">The amount by which to scale the transform in the x-axis direction.</param>
    /// <param name="sy">The amount by which to scale the transform in the y-axis direction.</param>
    public void ScaleTransform(float sx, float sy) => this.ScaleTransform(sx, sy, MatrixOrder.Prepend);

    /// <summary>Scales the local geometric transform by the specified amounts in the specified order.</summary>
    /// <param name="sx">The amount by which to scale the transform in the x-axis direction.</param>
    /// <param name="sy">The amount by which to scale the transform in the y-axis direction.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix.</param>
    public void ScaleTransform(float sx, float sy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipScaleLineTransform(new HandleRef((object) this, this.NativeBrush), sx, sy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Rotates the local geometric transform by the specified amount. This method prepends the rotation to the transform.</summary>
    /// <param name="angle">The angle of rotation.</param>
    public void RotateTransform(float angle) => this.RotateTransform(angle, MatrixOrder.Prepend);

    /// <summary>Rotates the local geometric transform by the specified amount in the specified order.</summary>
    /// <param name="angle">The angle of rotation.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix.</param>
    public void RotateTransform(float angle, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipRotateLineTransform(new HandleRef((object) this, this.NativeBrush), angle, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }
  }
}
