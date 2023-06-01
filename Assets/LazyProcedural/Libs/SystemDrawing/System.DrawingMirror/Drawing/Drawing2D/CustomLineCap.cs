// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.CustomLineCap
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
  /// <summary>Encapsulates a custom user-defined line cap.</summary>
  public class CustomLineCap : MarshalByRefObject, ICloneable, IDisposable
  {
    internal SafeCustomLineCapHandle nativeCap;
    private bool disposed;

    internal CustomLineCap()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class with the specified outline and fill.</summary>
    /// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap.</param>
    /// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap.</param>
    public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath)
      : this(fillPath, strokePath, LineCap.Flat)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class from the specified existing <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration with the specified outline and fill.</summary>
    /// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap.</param>
    /// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap.</param>
    /// <param name="baseCap">The line cap from which to create the custom cap.</param>
    public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap)
      : this(fillPath, strokePath, baseCap, 0.0f)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class from the specified existing <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration with the specified outline, fill, and inset.</summary>
    /// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap.</param>
    /// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap.</param>
    /// <param name="baseCap">The line cap from which to create the custom cap.</param>
    /// <param name="baseInset">The distance between the cap and the line.</param>
    public CustomLineCap(
      GraphicsPath fillPath,
      GraphicsPath strokePath,
      LineCap baseCap,
      float baseInset)
    {
      IntPtr customCap = IntPtr.Zero;
      int customLineCap = SafeNativeMethods.Gdip.GdipCreateCustomLineCap(new HandleRef((object) fillPath, fillPath == null ? IntPtr.Zero : fillPath.nativePath), new HandleRef((object) strokePath, strokePath == null ? IntPtr.Zero : strokePath.nativePath), baseCap, baseInset, out customCap);
      if (customLineCap != 0)
        throw SafeNativeMethods.Gdip.StatusException(customLineCap);
      this.SetNativeLineCap(customCap);
    }

    internal CustomLineCap(IntPtr nativeLineCap) => this.SetNativeLineCap(nativeLineCap);

    internal void SetNativeLineCap(IntPtr handle) => this.nativeCap = !(handle == IntPtr.Zero) ? new SafeCustomLineCapHandle(handle) : throw new ArgumentNullException(nameof (handle));

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> and optionally releases the managed resources.</summary>
    /// <param name="disposing">
    /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (disposing && this.nativeCap != null)
        this.nativeCap.Dispose();
      this.disposed = true;
    }

    /// <summary>Allows an <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> is reclaimed by garbage collection.</summary>
    ~CustomLineCap() => this.Dispose(false);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> this method creates, cast as an object.</returns>
    public object Clone()
    {
      IntPtr clonedCap = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneCustomLineCap(new HandleRef((object) this, (IntPtr) this.nativeCap), out clonedCap);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) CustomLineCap.CreateCustomLineCapObject(clonedCap);
    }

    internal static CustomLineCap CreateCustomLineCapObject(IntPtr cap)
    {
      CustomLineCapType capType = CustomLineCapType.Default;
      int customLineCapType = SafeNativeMethods.Gdip.GdipGetCustomLineCapType(new HandleRef((object) null, cap), out capType);
      if (customLineCapType != 0)
      {
        SafeNativeMethods.Gdip.GdipDeleteCustomLineCap(new HandleRef((object) null, cap));
        throw SafeNativeMethods.Gdip.StatusException(customLineCapType);
      }
      if (capType == CustomLineCapType.Default)
        return new CustomLineCap(cap);
      if (capType == CustomLineCapType.AdjustableArrowCap)
        return (CustomLineCap) new AdjustableArrowCap(cap);
      SafeNativeMethods.Gdip.GdipDeleteCustomLineCap(new HandleRef((object) null, cap));
      throw SafeNativeMethods.Gdip.StatusException(6);
    }

    /// <summary>Sets the caps used to start and end lines that make up this custom cap.</summary>
    /// <param name="startCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the beginning of a line within this cap.</param>
    /// <param name="endCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the end of a line within this cap.</param>
    public void SetStrokeCaps(LineCap startCap, LineCap endCap)
    {
      int status = SafeNativeMethods.Gdip.GdipSetCustomLineCapStrokeCaps(new HandleRef((object) this, (IntPtr) this.nativeCap), startCap, endCap);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets the caps used to start and end lines that make up this custom cap.</summary>
    /// <param name="startCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the beginning of a line within this cap.</param>
    /// <param name="endCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the end of a line within this cap.</param>
    public void GetStrokeCaps(out LineCap startCap, out LineCap endCap)
    {
      int lineCapStrokeCaps = SafeNativeMethods.Gdip.GdipGetCustomLineCapStrokeCaps(new HandleRef((object) this, (IntPtr) this.nativeCap), out startCap, out endCap);
      if (lineCapStrokeCaps != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineCapStrokeCaps);
    }

    private void _SetStrokeJoin(LineJoin lineJoin)
    {
      int status = SafeNativeMethods.Gdip.GdipSetCustomLineCapStrokeJoin(new HandleRef((object) this, (IntPtr) this.nativeCap), lineJoin);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private LineJoin _GetStrokeJoin()
    {
      LineJoin lineJoin;
      int lineCapStrokeJoin = SafeNativeMethods.Gdip.GdipGetCustomLineCapStrokeJoin(new HandleRef((object) this, (IntPtr) this.nativeCap), out lineJoin);
      if (lineCapStrokeJoin != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineCapStrokeJoin);
      return lineJoin;
    }

    /// <summary>Gets or sets the <see cref="T:System.Drawing.Drawing2D.LineJoin" /> enumeration that determines how lines that compose this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object are joined.</summary>
    /// <returns>The <see cref="T:System.Drawing.Drawing2D.LineJoin" /> enumeration this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object uses to join lines.</returns>
    public LineJoin StrokeJoin
    {
      get => this._GetStrokeJoin();
      set => this._SetStrokeJoin(value);
    }

    private void _SetBaseCap(LineCap baseCap)
    {
      int status = SafeNativeMethods.Gdip.GdipSetCustomLineCapBaseCap(new HandleRef((object) this, (IntPtr) this.nativeCap), baseCap);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private LineCap _GetBaseCap()
    {
      LineCap baseCap;
      int customLineCapBaseCap = SafeNativeMethods.Gdip.GdipGetCustomLineCapBaseCap(new HandleRef((object) this, (IntPtr) this.nativeCap), out baseCap);
      if (customLineCapBaseCap != 0)
        throw SafeNativeMethods.Gdip.StatusException(customLineCapBaseCap);
      return baseCap;
    }

    /// <summary>Gets or sets the <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration on which this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> is based.</summary>
    /// <returns>The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration on which this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> is based.</returns>
    public LineCap BaseCap
    {
      get => this._GetBaseCap();
      set => this._SetBaseCap(value);
    }

    private void _SetBaseInset(float inset)
    {
      int status = SafeNativeMethods.Gdip.GdipSetCustomLineCapBaseInset(new HandleRef((object) this, (IntPtr) this.nativeCap), inset);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private float _GetBaseInset()
    {
      float inset;
      int lineCapBaseInset = SafeNativeMethods.Gdip.GdipGetCustomLineCapBaseInset(new HandleRef((object) this, (IntPtr) this.nativeCap), out inset);
      if (lineCapBaseInset != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineCapBaseInset);
      return inset;
    }

    /// <summary>Gets or sets the distance between the cap and the line.</summary>
    /// <returns>The distance between the beginning of the cap and the end of the line.</returns>
    public float BaseInset
    {
      get => this._GetBaseInset();
      set => this._SetBaseInset(value);
    }

    private void _SetWidthScale(float widthScale)
    {
      int status = SafeNativeMethods.Gdip.GdipSetCustomLineCapWidthScale(new HandleRef((object) this, (IntPtr) this.nativeCap), widthScale);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private float _GetWidthScale()
    {
      float widthScale;
      int lineCapWidthScale = SafeNativeMethods.Gdip.GdipGetCustomLineCapWidthScale(new HandleRef((object) this, (IntPtr) this.nativeCap), out widthScale);
      if (lineCapWidthScale != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineCapWidthScale);
      return widthScale;
    }

    /// <summary>Gets or sets the amount by which to scale this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> Class object with respect to the width of the <see cref="T:System.Drawing.Pen" /> object.</summary>
    /// <returns>The amount by which to scale the cap.</returns>
    public float WidthScale
    {
      get => this._GetWidthScale();
      set => this._SetWidthScale(value);
    }
  }
}
