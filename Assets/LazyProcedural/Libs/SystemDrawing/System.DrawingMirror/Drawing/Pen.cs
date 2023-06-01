// Decompiled with JetBrains decompiler
// Type: System.Drawing.Pen
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing
{
  /// <summary>Defines an object used to draw lines and curves. This class cannot be inherited.</summary>
  public sealed class Pen : MarshalByRefObject, ISystemColorTracker, ICloneable, IDisposable
  {
    private IntPtr nativePen;
    private Color color;
    private bool immutable;

    private Pen(IntPtr nativePen) => this.SetNativePen(nativePen);

    internal Pen(Color color, bool immutable)
      : this(color)
    {
      this.immutable = immutable;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified color.</summary>
    /// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that indicates the color of this <see cref="T:System.Drawing.Pen" />.</param>
    public Pen(Color color)
      : this(color, 1f)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Color" /> and <see cref="P:System.Drawing.Pen.Width" /> properties.</summary>
    /// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that indicates the color of this <see cref="T:System.Drawing.Pen" />.</param>
    /// <param name="width">A value indicating the width of this <see cref="T:System.Drawing.Pen" />.</param>
    public Pen(Color color, float width)
    {
      this.color = color;
      IntPtr pen = IntPtr.Zero;
      int pen1 = SafeNativeMethods.Gdip.GdipCreatePen1(color.ToArgb(), width, 0, out pen);
      if (pen1 != 0)
        throw SafeNativeMethods.Gdip.StatusException(pen1);
      this.SetNativePen(pen);
      if (!this.color.IsSystemColor)
        return;
      SystemColorTracker.Add((ISystemColorTracker) this);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Brush" />.</summary>
    /// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the fill properties of this <see cref="T:System.Drawing.Pen" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public Pen(Brush brush)
      : this(brush, 1f)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="P:System.Drawing.Pen.Width" />.</summary>
    /// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the characteristics of this <see cref="T:System.Drawing.Pen" />.</param>
    /// <param name="width">The width of the new <see cref="T:System.Drawing.Pen" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public Pen(Brush brush, float width)
    {
      IntPtr pen = IntPtr.Zero;
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      int pen2 = SafeNativeMethods.Gdip.GdipCreatePen2(new HandleRef((object) brush, brush.NativeBrush), width, 0, out pen);
      if (pen2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(pen2);
      this.SetNativePen(pen);
    }

    internal void SetNativePen(IntPtr nativePen) => this.nativePen = !(nativePen == IntPtr.Zero) ? nativePen : throw new ArgumentNullException(nameof (nativePen));

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal IntPtr NativePen => this.nativePen;

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>An <see cref="T:System.Object" /> that can be cast to a <see cref="T:System.Drawing.Pen" />.</returns>
    public object Clone()
    {
      IntPtr clonepen = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipClonePen(new HandleRef((object) this, this.NativePen), out clonepen);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new Pen(clonepen);
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Pen" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!disposing)
        this.immutable = false;
      else if (this.immutable)
        throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) "Brush"));
      if (!(this.nativePen != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDeletePen(new HandleRef((object) this, this.NativePen));
      }
      catch (Exception ex)
      {
        if (!ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativePen = IntPtr.Zero;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~Pen() => this.Dispose(false);

    /// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Pen" />, in units of the <see cref="T:System.Drawing.Graphics" /> object used for drawing.</summary>
    /// <returns>The width of this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Width" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public float Width
    {
      get
      {
        float[] width = new float[1];
        int penWidth = SafeNativeMethods.Gdip.GdipGetPenWidth(new HandleRef((object) this, this.NativePen), width);
        if (penWidth != 0)
          throw SafeNativeMethods.Gdip.StatusException(penWidth);
        return width[0];
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenWidth(new HandleRef((object) this, this.NativePen), value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Sets the values that determine the style of cap used to end lines drawn by this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <param name="startCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</param>
    /// <param name="endCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</param>
    /// <param name="dashCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the beginning or end of dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</param>
    public void SetLineCap(LineCap startCap, LineCap endCap, DashCap dashCap)
    {
      if (this.immutable)
        throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
      int status = SafeNativeMethods.Gdip.GdipSetPenLineCap197819(new HandleRef((object) this, this.NativePen), (int) startCap, (int) endCap, (int) dashCap);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets or sets the cap style used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Drawing2D.LineCap" /> values that represents the cap style used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.LineCap" />.</exception>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.StartCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public LineCap StartCap
    {
      get
      {
        int startCap = 0;
        int penStartCap = SafeNativeMethods.Gdip.GdipGetPenStartCap(new HandleRef((object) this, this.NativePen), out startCap);
        if (penStartCap != 0)
          throw SafeNativeMethods.Gdip.StatusException(penStartCap);
        return (LineCap) startCap;
      }
      set
      {
        switch (value)
        {
          case LineCap.Flat:
          case LineCap.Square:
          case LineCap.Round:
          case LineCap.Triangle:
          case LineCap.NoAnchor:
          case LineCap.SquareAnchor:
          case LineCap.RoundAnchor:
          case LineCap.DiamondAnchor:
          case LineCap.ArrowAnchor:
          case LineCap.AnchorMask:
          case LineCap.Custom:
            if (this.immutable)
              throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
            int status = SafeNativeMethods.Gdip.GdipSetPenStartCap(new HandleRef((object) this, this.NativePen), (int) value);
            if (status == 0)
              break;
            throw SafeNativeMethods.Gdip.StatusException(status);
          default:
            throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (LineCap));
        }
      }
    }

    /// <summary>Gets or sets the cap style used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Drawing2D.LineCap" /> values that represents the cap style used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.LineCap" />.</exception>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.EndCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public LineCap EndCap
    {
      get
      {
        int endCap = 0;
        int penEndCap = SafeNativeMethods.Gdip.GdipGetPenEndCap(new HandleRef((object) this, this.NativePen), out endCap);
        if (penEndCap != 0)
          throw SafeNativeMethods.Gdip.StatusException(penEndCap);
        return (LineCap) endCap;
      }
      set
      {
        switch (value)
        {
          case LineCap.Flat:
          case LineCap.Square:
          case LineCap.Round:
          case LineCap.Triangle:
          case LineCap.NoAnchor:
          case LineCap.SquareAnchor:
          case LineCap.RoundAnchor:
          case LineCap.DiamondAnchor:
          case LineCap.ArrowAnchor:
          case LineCap.AnchorMask:
          case LineCap.Custom:
            if (this.immutable)
              throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
            int status = SafeNativeMethods.Gdip.GdipSetPenEndCap(new HandleRef((object) this, this.NativePen), (int) value);
            if (status == 0)
              break;
            throw SafeNativeMethods.Gdip.StatusException(status);
          default:
            throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (LineCap));
        }
      }
    }

    /// <summary>Gets or sets the cap style used at the end of the dashes that make up dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Drawing2D.DashCap" /> values that represents the cap style used at the beginning and end of the dashes that make up dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.DashCap" />.</exception>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public DashCap DashCap
    {
      get
      {
        int dashCap = 0;
        int penDashCap197819 = SafeNativeMethods.Gdip.GdipGetPenDashCap197819(new HandleRef((object) this, this.NativePen), out dashCap);
        if (penDashCap197819 != 0)
          throw SafeNativeMethods.Gdip.StatusException(penDashCap197819);
        return (DashCap) dashCap;
      }
      set
      {
        if (!ClientUtils.IsEnumValid_NotSequential((Enum) value, (int) value, 0, 2, 3))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (DashCap));
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenDashCap197819(new HandleRef((object) this, this.NativePen), (int) value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the join style for the ends of two consecutive lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.LineJoin" /> that represents the join style for the ends of two consecutive lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.LineJoin" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public LineJoin LineJoin
    {
      get
      {
        int lineJoin = 0;
        int penLineJoin = SafeNativeMethods.Gdip.GdipGetPenLineJoin(new HandleRef((object) this, this.NativePen), out lineJoin);
        if (penLineJoin != 0)
          throw SafeNativeMethods.Gdip.StatusException(penLineJoin);
        return (LineJoin) lineJoin;
      }
      set
      {
        if (!ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 3))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (LineJoin));
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenLineJoin(new HandleRef((object) this, this.NativePen), (int) value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets a custom cap to use at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> that represents the cap used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CustomStartCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public CustomLineCap CustomStartCap
    {
      get
      {
        IntPtr customCap = IntPtr.Zero;
        int penCustomStartCap = SafeNativeMethods.Gdip.GdipGetPenCustomStartCap(new HandleRef((object) this, this.NativePen), out customCap);
        if (penCustomStartCap != 0)
          throw SafeNativeMethods.Gdip.StatusException(penCustomStartCap);
        return CustomLineCap.CreateCustomLineCapObject(customCap);
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenCustomStartCap(new HandleRef((object) this, this.NativePen), new HandleRef((object) value, value == null ? IntPtr.Zero : (IntPtr) value.nativeCap));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets a custom cap to use at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> that represents the cap used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CustomEndCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public CustomLineCap CustomEndCap
    {
      get
      {
        IntPtr customCap = IntPtr.Zero;
        int penCustomEndCap = SafeNativeMethods.Gdip.GdipGetPenCustomEndCap(new HandleRef((object) this, this.NativePen), out customCap);
        if (penCustomEndCap != 0)
          throw SafeNativeMethods.Gdip.StatusException(penCustomEndCap);
        return CustomLineCap.CreateCustomLineCapObject(customCap);
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenCustomEndCap(new HandleRef((object) this, this.NativePen), new HandleRef((object) value, value == null ? IntPtr.Zero : (IntPtr) value.nativeCap));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the limit of the thickness of the join on a mitered corner.</summary>
    /// <returns>The limit of the thickness of the join on a mitered corner.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.MiterLimit" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public float MiterLimit
    {
      get
      {
        float[] miterLimit = new float[1];
        int penMiterLimit = SafeNativeMethods.Gdip.GdipGetPenMiterLimit(new HandleRef((object) this, this.NativePen), miterLimit);
        if (penMiterLimit != 0)
          throw SafeNativeMethods.Gdip.StatusException(penMiterLimit);
        return miterLimit[0];
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenMiterLimit(new HandleRef((object) this, this.NativePen), value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the alignment for this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.PenAlignment" /> that represents the alignment for this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.PenAlignment" />.</exception>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Alignment" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public PenAlignment Alignment
    {
      get
      {
        PenAlignment penAlign = PenAlignment.Center;
        int penMode = SafeNativeMethods.Gdip.GdipGetPenMode(new HandleRef((object) this, this.NativePen), out penAlign);
        if (penMode != 0)
          throw SafeNativeMethods.Gdip.StatusException(penMode);
        return penAlign;
      }
      set
      {
        if (!ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 4))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (PenAlignment));
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenMode(new HandleRef((object) this, this.NativePen), value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets a copy of the geometric transformation for this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the geometric transformation for this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Transform" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public Matrix Transform
    {
      get
      {
        Matrix wrapper = new Matrix();
        int penTransform = SafeNativeMethods.Gdip.GdipGetPenTransform(new HandleRef((object) this, this.NativePen), new HandleRef((object) wrapper, wrapper.nativeMatrix));
        if (penTransform != 0)
          throw SafeNativeMethods.Gdip.StatusException(penTransform);
        return wrapper;
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        int status = SafeNativeMethods.Gdip.GdipSetPenTransform(new HandleRef((object) this, this.NativePen), new HandleRef((object) value, value.nativeMatrix));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Resets the geometric transformation matrix for this <see cref="T:System.Drawing.Pen" /> to identity.</summary>
    public void ResetTransform()
    {
      int status = SafeNativeMethods.Gdip.GdipResetPenTransform(new HandleRef((object) this, this.NativePen));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Multiplies the transformation matrix for this <see cref="T:System.Drawing.Pen" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> object by which to multiply the transformation matrix.</param>
    public void MultiplyTransform(Matrix matrix) => this.MultiplyTransform(matrix, MatrixOrder.Prepend);

    /// <summary>Multiplies the transformation matrix for this <see cref="T:System.Drawing.Pen" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the transformation matrix.</param>
    /// <param name="order">The order in which to perform the multiplication operation.</param>
    public void MultiplyTransform(Matrix matrix, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipMultiplyPenTransform(new HandleRef((object) this, this.NativePen), new HandleRef((object) matrix, matrix.nativeMatrix), order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Translates the local geometric transformation by the specified dimensions. This method prepends the translation to the transformation.</summary>
    /// <param name="dx">The value of the translation in x.</param>
    /// <param name="dy">The value of the translation in y.</param>
    public void TranslateTransform(float dx, float dy) => this.TranslateTransform(dx, dy, MatrixOrder.Prepend);

    /// <summary>Translates the local geometric transformation by the specified dimensions in the specified order.</summary>
    /// <param name="dx">The value of the translation in x.</param>
    /// <param name="dy">The value of the translation in y.</param>
    /// <param name="order">The order (prepend or append) in which to apply the translation.</param>
    public void TranslateTransform(float dx, float dy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslatePenTransform(new HandleRef((object) this, this.NativePen), dx, dy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Scales the local geometric transformation by the specified factors. This method prepends the scaling matrix to the transformation.</summary>
    /// <param name="sx">The factor by which to scale the transformation in the x-axis direction.</param>
    /// <param name="sy">The factor by which to scale the transformation in the y-axis direction.</param>
    public void ScaleTransform(float sx, float sy) => this.ScaleTransform(sx, sy, MatrixOrder.Prepend);

    /// <summary>Scales the local geometric transformation by the specified factors in the specified order.</summary>
    /// <param name="sx">The factor by which to scale the transformation in the x-axis direction.</param>
    /// <param name="sy">The factor by which to scale the transformation in the y-axis direction.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix.</param>
    public void ScaleTransform(float sx, float sy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipScalePenTransform(new HandleRef((object) this, this.NativePen), sx, sy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Rotates the local geometric transformation by the specified angle. This method prepends the rotation to the transformation.</summary>
    /// <param name="angle">The angle of rotation.</param>
    public void RotateTransform(float angle) => this.RotateTransform(angle, MatrixOrder.Prepend);

    /// <summary>Rotates the local geometric transformation by the specified angle in the specified order.</summary>
    /// <param name="angle">The angle of rotation.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix.</param>
    public void RotateTransform(float angle, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipRotatePenTransform(new HandleRef((object) this, this.NativePen), angle, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private void InternalSetColor(Color value)
    {
      int status = SafeNativeMethods.Gdip.GdipSetPenColor(new HandleRef((object) this, this.NativePen), this.color.ToArgb());
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.color = value;
    }

    /// <summary>Gets the style of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.PenType" /> enumeration that specifies the style of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
    public PenType PenType
    {
      get
      {
        int pentype = -1;
        int penFillType = SafeNativeMethods.Gdip.GdipGetPenFillType(new HandleRef((object) this, this.NativePen), out pentype);
        if (penFillType != 0)
          throw SafeNativeMethods.Gdip.StatusException(penFillType);
        return (PenType) pentype;
      }
    }

    /// <summary>Gets or sets the color of this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Color" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public Color Color
    {
      get
      {
        if (this.color == Color.Empty)
        {
          int argb = 0;
          int penColor = SafeNativeMethods.Gdip.GdipGetPenColor(new HandleRef((object) this, this.NativePen), out argb);
          if (penColor != 0)
            throw SafeNativeMethods.Gdip.StatusException(penColor);
          this.color = Color.FromArgb(argb);
        }
        return this.color;
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        if (!(value != this.color))
          return;
        Color color = this.color;
        this.color = value;
        this.InternalSetColor(value);
        if (!value.IsSystemColor || color.IsSystemColor)
          return;
        SystemColorTracker.Add((ISystemColorTracker) this);
      }
    }

    /// <summary>Gets or sets the <see cref="T:System.Drawing.Brush" /> that determines attributes of this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> that determines attributes of this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Brush" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public Brush Brush
    {
      get
      {
        Brush brush = (Brush) null;
        switch (this.PenType)
        {
          case PenType.SolidColor:
            brush = (Brush) new SolidBrush(this.GetNativeBrush());
            break;
          case PenType.HatchFill:
            brush = (Brush) new HatchBrush(this.GetNativeBrush());
            break;
          case PenType.TextureFill:
            brush = (Brush) new TextureBrush(this.GetNativeBrush());
            break;
          case PenType.PathGradient:
            brush = (Brush) new PathGradientBrush(this.GetNativeBrush());
            break;
          case PenType.LinearGradient:
            brush = (Brush) new LinearGradientBrush(this.GetNativeBrush());
            break;
        }
        return brush;
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        int status = SafeNativeMethods.Gdip.GdipSetPenBrushFill(new HandleRef((object) this, this.NativePen), new HandleRef((object) value, value.NativeBrush));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    private IntPtr GetNativeBrush()
    {
      IntPtr brush = IntPtr.Zero;
      int penBrushFill = SafeNativeMethods.Gdip.GdipGetPenBrushFill(new HandleRef((object) this, this.NativePen), out brush);
      if (penBrushFill != 0)
        throw SafeNativeMethods.Gdip.StatusException(penBrushFill);
      return brush;
    }

    /// <summary>Gets or sets the style used for dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.DashStyle" /> that represents the style used for dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashStyle" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public DashStyle DashStyle
    {
      get
      {
        int dashstyle = 0;
        int penDashStyle = SafeNativeMethods.Gdip.GdipGetPenDashStyle(new HandleRef((object) this, this.NativePen), out dashstyle);
        if (penDashStyle != 0)
          throw SafeNativeMethods.Gdip.StatusException(penDashStyle);
        return (DashStyle) dashstyle;
      }
      set
      {
        if (!ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 5))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (DashStyle));
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenDashStyle(new HandleRef((object) this, this.NativePen), (int) value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        if (value != DashStyle.Custom)
          return;
        this.EnsureValidDashPattern();
      }
    }

    private void EnsureValidDashPattern()
    {
      int dashcount = 0;
      int penDashCount = SafeNativeMethods.Gdip.GdipGetPenDashCount(new HandleRef((object) this, this.NativePen), out dashcount);
      if (penDashCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(penDashCount);
      if (dashcount != 0)
        return;
      this.DashPattern = new float[1]{ 1f };
    }

    /// <summary>Gets or sets the distance from the start of a line to the beginning of a dash pattern.</summary>
    /// <returns>The distance from the start of a line to the beginning of a dash pattern.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashOffset" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public float DashOffset
    {
      get
      {
        float[] dashoffset = new float[1];
        int penDashOffset = SafeNativeMethods.Gdip.GdipGetPenDashOffset(new HandleRef((object) this, this.NativePen), dashoffset);
        if (penDashOffset != 0)
          throw SafeNativeMethods.Gdip.StatusException(penDashOffset);
        return dashoffset[0];
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenDashOffset(new HandleRef((object) this, this.NativePen), value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets an array of custom dashes and spaces.</summary>
    /// <returns>An array of real numbers that specifies the lengths of alternating dashes and spaces in dashed lines.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashPattern" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public float[] DashPattern
    {
      get
      {
        int dashcount = 0;
        int penDashCount = SafeNativeMethods.Gdip.GdipGetPenDashCount(new HandleRef((object) this, this.NativePen), out dashcount);
        if (penDashCount != 0)
          throw SafeNativeMethods.Gdip.StatusException(penDashCount);
        int length = dashcount;
        IntPtr num = Marshal.AllocHGlobal(checked (4 * length));
        int penDashArray = SafeNativeMethods.Gdip.GdipGetPenDashArray(new HandleRef((object) this, this.NativePen), num, length);
        float[] destination;
        try
        {
          if (penDashArray != 0)
            throw SafeNativeMethods.Gdip.StatusException(penDashArray);
          destination = new float[length];
          Marshal.Copy(num, destination, 0, length);
        }
        finally
        {
          Marshal.FreeHGlobal(num);
        }
        return destination;
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int num1 = value != null && value.Length != 0 ? value.Length : throw new ArgumentException(SR.GetString("InvalidDashPattern"));
        IntPtr num2 = Marshal.AllocHGlobal(checked (4 * num1));
        try
        {
          Marshal.Copy(value, 0, num2, num1);
          int status = SafeNativeMethods.Gdip.GdipSetPenDashArray(new HandleRef((object) this, this.NativePen), new HandleRef((object) num2, num2), num1);
          if (status != 0)
            throw SafeNativeMethods.Gdip.StatusException(status);
        }
        finally
        {
          Marshal.FreeHGlobal(num2);
        }
      }
    }

    /// <summary>Gets or sets an array of values that specifies a compound pen. A compound pen draws a compound line made up of parallel lines and spaces.</summary>
    /// <returns>An array of real numbers that specifies the compound array. The elements in the array must be in increasing order, not less than 0, and not greater than 1.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CompoundArray" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
    public float[] CompoundArray
    {
      get
      {
        int count = 0;
        int penCompoundCount = SafeNativeMethods.Gdip.GdipGetPenCompoundCount(new HandleRef((object) this, this.NativePen), out count);
        if (penCompoundCount != 0)
          throw SafeNativeMethods.Gdip.StatusException(penCompoundCount);
        float[] array = new float[count];
        int penCompoundArray = SafeNativeMethods.Gdip.GdipGetPenCompoundArray(new HandleRef((object) this, this.NativePen), array, count);
        if (penCompoundArray != 0)
          throw SafeNativeMethods.Gdip.StatusException(penCompoundArray);
        return array;
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", (object) nameof (Pen)));
        int status = SafeNativeMethods.Gdip.GdipSetPenCompoundArray(new HandleRef((object) this, this.NativePen), value, value.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    void ISystemColorTracker.OnSystemColorChanged()
    {
      if (!(this.NativePen != IntPtr.Zero))
        return;
      this.InternalSetColor(this.color);
    }
  }
}
