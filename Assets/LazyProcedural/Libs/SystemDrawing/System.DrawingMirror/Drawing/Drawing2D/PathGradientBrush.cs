// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.PathGradientBrush
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
  /// <summary>Encapsulates a <see cref="T:System.Drawing.Brush" /> object that fills the interior of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object with a gradient. This class cannot be inherited.</summary>
  public sealed class PathGradientBrush : Brush
  {
    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that make up the vertices of the path.</param>
    public PathGradientBrush(PointF[] points)
      : this(points, WrapMode.Clamp)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points and wrap mode.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that make up the vertices of the path.</param>
    /// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</param>
    public PathGradientBrush(PointF[] points, WrapMode wrapMode)
    {
      if (points == null)
        throw new ArgumentNullException(nameof (points));
      if (!System.Drawing.ClientUtils.IsEnumValid((Enum) wrapMode, (int) wrapMode, 0, 4))
        throw new InvalidEnumArgumentException(nameof (wrapMode), (int) wrapMode, typeof (WrapMode));
      IntPtr brush = IntPtr.Zero;
      IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
      try
      {
        int pathGradient = SafeNativeMethods.Gdip.GdipCreatePathGradient(new HandleRef((object) null, memory), points.Length, (int) wrapMode, out brush);
        if (pathGradient != 0)
          throw SafeNativeMethods.Gdip.StatusException(pathGradient);
        this.SetNativeBrushInternal(brush);
      }
      finally
      {
        if (memory != IntPtr.Zero)
          Marshal.FreeHGlobal(memory);
      }
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that make up the vertices of the path.</param>
    public PathGradientBrush(Point[] points)
      : this(points, WrapMode.Clamp)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified points and wrap mode.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that make up the vertices of the path.</param>
    /// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</param>
    public PathGradientBrush(Point[] points, WrapMode wrapMode)
    {
      if (points == null)
        throw new ArgumentNullException(nameof (points));
      if (!System.Drawing.ClientUtils.IsEnumValid((Enum) wrapMode, (int) wrapMode, 0, 4))
        throw new InvalidEnumArgumentException(nameof (wrapMode), (int) wrapMode, typeof (WrapMode));
      IntPtr brush = IntPtr.Zero;
      IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(points);
      try
      {
        int pathGradientI = SafeNativeMethods.Gdip.GdipCreatePathGradientI(new HandleRef((object) null, memory), points.Length, (int) wrapMode, out brush);
        if (pathGradientI != 0)
          throw SafeNativeMethods.Gdip.StatusException(pathGradientI);
        this.SetNativeBrushInternal(brush);
      }
      finally
      {
        if (memory != IntPtr.Zero)
          Marshal.FreeHGlobal(memory);
      }
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> class with the specified path.</summary>
    /// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that defines the area filled by this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</param>
    public PathGradientBrush(GraphicsPath path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      IntPtr brush = IntPtr.Zero;
      int gradientFromPath = SafeNativeMethods.Gdip.GdipCreatePathGradientFromPath(new HandleRef((object) path, path.nativePath), out brush);
      if (gradientFromPath != 0)
        throw SafeNativeMethods.Gdip.StatusException(gradientFromPath);
      this.SetNativeBrushInternal(brush);
    }

    internal PathGradientBrush(IntPtr nativeBrush) => this.SetNativeBrushInternal(nativeBrush);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> this method creates, cast as an object.</returns>
    public override object Clone()
    {
      IntPtr clonebrush = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef((object) this, this.NativeBrush), out clonebrush);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new PathGradientBrush(clonebrush);
    }

    /// <summary>Gets or sets the color at the center of the path gradient.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color at the center of the path gradient.</returns>
    public Color CenterColor
    {
      get
      {
        int color;
        int gradientCenterColor = SafeNativeMethods.Gdip.GdipGetPathGradientCenterColor(new HandleRef((object) this, this.NativeBrush), out color);
        if (gradientCenterColor != 0)
          throw SafeNativeMethods.Gdip.StatusException(gradientCenterColor);
        return Color.FromArgb(color);
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetPathGradientCenterColor(new HandleRef((object) this, this.NativeBrush), value.ToArgb());
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    private void _SetSurroundColors(Color[] colors)
    {
      int count;
      int surroundColorCount = SafeNativeMethods.Gdip.GdipGetPathGradientSurroundColorCount(new HandleRef((object) this, this.NativeBrush), out count);
      if (surroundColorCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(surroundColorCount);
      if (colors.Length > count || count <= 0)
        throw SafeNativeMethods.Gdip.StatusException(2);
      int length = colors.Length;
      int[] argb = new int[length];
      for (int index = 0; index < colors.Length; ++index)
        argb[index] = colors[index].ToArgb();
      int status = SafeNativeMethods.Gdip.GdipSetPathGradientSurroundColorsWithCount(new HandleRef((object) this, this.NativeBrush), argb, ref length);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private Color[] _GetSurroundColors()
    {
      int count;
      int surroundColorCount = SafeNativeMethods.Gdip.GdipGetPathGradientSurroundColorCount(new HandleRef((object) this, this.NativeBrush), out count);
      if (surroundColorCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(surroundColorCount);
      int[] color = new int[count];
      int surroundColorsWithCount = SafeNativeMethods.Gdip.GdipGetPathGradientSurroundColorsWithCount(new HandleRef((object) this, this.NativeBrush), color, ref count);
      if (surroundColorsWithCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(surroundColorsWithCount);
      Color[] surroundColors = new Color[count];
      for (int index = 0; index < count; ++index)
        surroundColors[index] = Color.FromArgb(color[index]);
      return surroundColors;
    }

    /// <summary>Gets or sets an array of colors that correspond to the points in the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.Color" /> structures that represents the colors associated with each point in the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</returns>
    public Color[] SurroundColors
    {
      get => this._GetSurroundColors();
      set => this._SetSurroundColors(value);
    }

    /// <summary>Gets or sets the center point of the path gradient.</summary>
    /// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the center point of the path gradient.</returns>
    public PointF CenterPoint
    {
      get
      {
        GPPOINTF point = new GPPOINTF();
        int gradientCenterPoint = SafeNativeMethods.Gdip.GdipGetPathGradientCenterPoint(new HandleRef((object) this, this.NativeBrush), point);
        if (gradientCenterPoint != 0)
          throw SafeNativeMethods.Gdip.StatusException(gradientCenterPoint);
        return point.ToPoint();
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetPathGradientCenterPoint(new HandleRef((object) this, this.NativeBrush), new GPPOINTF(value));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    private RectangleF _GetRectangle()
    {
      GPRECTF gprectf = new GPRECTF();
      int pathGradientRect = SafeNativeMethods.Gdip.GdipGetPathGradientRect(new HandleRef((object) this, this.NativeBrush), ref gprectf);
      if (pathGradientRect != 0)
        throw SafeNativeMethods.Gdip.StatusException(pathGradientRect);
      return gprectf.ToRectangleF();
    }

    /// <summary>Gets a bounding rectangle for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangular region that bounds the path this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> fills.</returns>
    public RectangleF Rectangle => this._GetRectangle();

    private Blend _GetBlend()
    {
      int count = 0;
      int gradientBlendCount = SafeNativeMethods.Gdip.GdipGetPathGradientBlendCount(new HandleRef((object) this, this.NativeBrush), out count);
      if (gradientBlendCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(gradientBlendCount);
      int num1 = count;
      IntPtr num2 = IntPtr.Zero;
      IntPtr num3 = IntPtr.Zero;
      Blend blend;
      try
      {
        int cb = checked (4 * num1);
        num2 = Marshal.AllocHGlobal(cb);
        num3 = Marshal.AllocHGlobal(cb);
        int pathGradientBlend = SafeNativeMethods.Gdip.GdipGetPathGradientBlend(new HandleRef((object) this, this.NativeBrush), num2, num3, num1);
        if (pathGradientBlend != 0)
          throw SafeNativeMethods.Gdip.StatusException(pathGradientBlend);
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
        int status = SafeNativeMethods.Gdip.GdipSetPathGradientBlend(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) null, num1), new HandleRef((object) null, num2), length);
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

    /// <summary>Creates a gradient brush that changes color starting from the center of the path outward to the path's boundary. The transition from one color to another is based on a bell-shaped curve.</summary>
    /// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
    public void SetSigmaBellShape(float focus) => this.SetSigmaBellShape(focus, 1f);

    /// <summary>Creates a gradient brush that changes color starting from the center of the path outward to the path's boundary. The transition from one color to another is based on a bell-shaped curve.</summary>
    /// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
    /// <param name="scale">A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the default value.</param>
    public void SetSigmaBellShape(float focus, float scale)
    {
      int status = SafeNativeMethods.Gdip.GdipSetPathGradientSigmaBlend(new HandleRef((object) this, this.NativeBrush), focus, scale);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Creates a gradient with a center color and a linear falloff to one surrounding color.</summary>
    /// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
    public void SetBlendTriangularShape(float focus) => this.SetBlendTriangularShape(focus, 1f);

    /// <summary>Creates a gradient with a center color and a linear falloff to each surrounding color.</summary>
    /// <param name="focus">A value from 0 through 1 that specifies where, along any radial from the center of the path to the path's boundary, the center color will be at its highest intensity. A value of 1 (the default) places the highest intensity at the center of the path.</param>
    /// <param name="scale">A value from 0 through 1 that specifies the maximum intensity of the center color that gets blended with the boundary color. A value of 1 causes the highest possible intensity of the center color, and it is the default value.</param>
    public void SetBlendTriangularShape(float focus, float scale)
    {
      int status = SafeNativeMethods.Gdip.GdipSetPathGradientLinearBlend(new HandleRef((object) this, this.NativeBrush), focus, scale);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private ColorBlend _GetInterpolationColors()
    {
      int count = 0;
      int presetBlendCount = SafeNativeMethods.Gdip.GdipGetPathGradientPresetBlendCount(new HandleRef((object) this, this.NativeBrush), out count);
      if (presetBlendCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(presetBlendCount);
      if (count == 0)
        return new ColorBlend();
      int length = count;
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      ColorBlend interpolationColors;
      try
      {
        int cb = checked (4 * length);
        num1 = Marshal.AllocHGlobal(cb);
        num2 = Marshal.AllocHGlobal(cb);
        int gradientPresetBlend = SafeNativeMethods.Gdip.GdipGetPathGradientPresetBlend(new HandleRef((object) this, this.NativeBrush), num1, num2, length);
        if (gradientPresetBlend != 0)
          throw SafeNativeMethods.Gdip.StatusException(gradientPresetBlend);
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
        int status = SafeNativeMethods.Gdip.GdipSetPathGradientPresetBlend(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) null, num1), new HandleRef((object) null, num2), length);
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

    private void _SetTransform(Matrix matrix)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int status = SafeNativeMethods.Gdip.GdipSetPathGradientTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) matrix, matrix.nativeMatrix));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private Matrix _GetTransform()
    {
      Matrix wrapper = new Matrix();
      int gradientTransform = SafeNativeMethods.Gdip.GdipGetPathGradientTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) wrapper, wrapper.nativeMatrix));
      if (gradientTransform != 0)
        throw SafeNativeMethods.Gdip.StatusException(gradientTransform);
      return wrapper;
    }

    /// <summary>Gets or sets a copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a local geometric transform for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
    /// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that defines a geometric transform that applies only to fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</returns>
    public Matrix Transform
    {
      get => this._GetTransform();
      set => this._SetTransform(value);
    }

    /// <summary>Resets the <see cref="P:System.Drawing.Drawing2D.PathGradientBrush.Transform" /> property to identity.</summary>
    public void ResetTransform()
    {
      int status = SafeNativeMethods.Gdip.GdipResetPathGradientTransform(new HandleRef((object) this, this.NativeBrush));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates the brush's transformation matrix with the product of brush's transformation matrix multiplied by another matrix.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that will be multiplied by the brush's current transformation matrix.</param>
    public void MultiplyTransform(Matrix matrix) => this.MultiplyTransform(matrix, MatrixOrder.Prepend);

    /// <summary>Updates the brush's transformation matrix with the product of the brush's transformation matrix multiplied by another matrix.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that will be multiplied by the brush's current transformation matrix.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies in which order to multiply the two matrices.</param>
    public void MultiplyTransform(Matrix matrix, MatrixOrder order)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int status = SafeNativeMethods.Gdip.GdipMultiplyPathGradientTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) matrix, matrix.nativeMatrix), order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies the specified translation to the local geometric transform. This method prepends the translation to the transform.</summary>
    /// <param name="dx">The value of the translation in x.</param>
    /// <param name="dy">The value of the translation in y.</param>
    public void TranslateTransform(float dx, float dy) => this.TranslateTransform(dx, dy, MatrixOrder.Prepend);

    /// <summary>Applies the specified translation to the local geometric transform in the specified order.</summary>
    /// <param name="dx">The value of the translation in x.</param>
    /// <param name="dy">The value of the translation in y.</param>
    /// <param name="order">The order (prepend or append) in which to apply the translation.</param>
    public void TranslateTransform(float dx, float dy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslatePathGradientTransform(new HandleRef((object) this, this.NativeBrush), dx, dy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Scales the local geometric transform by the specified amounts. This method prepends the scaling matrix to the transform.</summary>
    /// <param name="sx">The transform scale factor in the x-axis direction.</param>
    /// <param name="sy">The transform scale factor in the y-axis direction.</param>
    public void ScaleTransform(float sx, float sy) => this.ScaleTransform(sx, sy, MatrixOrder.Prepend);

    /// <summary>Scales the local geometric transform by the specified amounts in the specified order.</summary>
    /// <param name="sx">The transform scale factor in the x-axis direction.</param>
    /// <param name="sy">The transform scale factor in the y-axis direction.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix.</param>
    public void ScaleTransform(float sx, float sy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipScalePathGradientTransform(new HandleRef((object) this, this.NativeBrush), sx, sy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Rotates the local geometric transform by the specified amount. This method prepends the rotation to the transform.</summary>
    /// <param name="angle">The angle (extent) of rotation.</param>
    public void RotateTransform(float angle) => this.RotateTransform(angle, MatrixOrder.Prepend);

    /// <summary>Rotates the local geometric transform by the specified amount in the specified order.</summary>
    /// <param name="angle">The angle (extent) of rotation.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix.</param>
    public void RotateTransform(float angle, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipRotatePathGradientTransform(new HandleRef((object) this, this.NativeBrush), angle, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets or sets the focus point for the gradient falloff.</summary>
    /// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the focus point for the gradient falloff.</returns>
    public PointF FocusScales
    {
      get
      {
        float[] xScale = new float[1];
        float[] yScale = new float[1];
        int gradientFocusScales = SafeNativeMethods.Gdip.GdipGetPathGradientFocusScales(new HandleRef((object) this, this.NativeBrush), xScale, yScale);
        if (gradientFocusScales != 0)
          throw SafeNativeMethods.Gdip.StatusException(gradientFocusScales);
        return new PointF(xScale[0], yScale[0]);
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetPathGradientFocusScales(new HandleRef((object) this, this.NativeBrush), value.X, value.Y);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    private void _SetWrapMode(WrapMode wrapMode)
    {
      int status = SafeNativeMethods.Gdip.GdipSetPathGradientWrapMode(new HandleRef((object) this, this.NativeBrush), (int) wrapMode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private WrapMode _GetWrapMode()
    {
      int wrapmode = 0;
      int gradientWrapMode = SafeNativeMethods.Gdip.GdipGetPathGradientWrapMode(new HandleRef((object) this, this.NativeBrush), out wrapmode);
      if (gradientWrapMode != 0)
        throw SafeNativeMethods.Gdip.StatusException(gradientWrapMode);
      return (WrapMode) wrapmode;
    }

    /// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that indicates the wrap mode for this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how fills drawn with this <see cref="T:System.Drawing.Drawing2D.PathGradientBrush" /> are tiled.</returns>
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
  }
}
