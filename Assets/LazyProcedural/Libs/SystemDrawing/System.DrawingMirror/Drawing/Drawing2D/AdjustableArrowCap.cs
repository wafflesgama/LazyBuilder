// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.AdjustableArrowCap
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
  /// <summary>Represents an adjustable arrow-shaped line cap. This class cannot be inherited.</summary>
  public sealed class AdjustableArrowCap : CustomLineCap
  {
    internal AdjustableArrowCap(IntPtr nativeCap)
      : base(nativeCap)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.AdjustableArrowCap" /> class with the specified width and height. The arrow end caps created with this constructor are always filled.</summary>
    /// <param name="width">The width of the arrow.</param>
    /// <param name="height">The height of the arrow.</param>
    public AdjustableArrowCap(float width, float height)
      : this(width, height, true)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.AdjustableArrowCap" /> class with the specified width, height, and fill property. Whether an arrow end cap is filled depends on the argument passed to the <paramref name="isFilled" /> parameter.</summary>
    /// <param name="width">The width of the arrow.</param>
    /// <param name="height">The height of the arrow.</param>
    /// <param name="isFilled">
    /// <see langword="true" /> to fill the arrow cap; otherwise, <see langword="false" />.</param>
    public AdjustableArrowCap(float width, float height, bool isFilled)
    {
      IntPtr adjustableArrowCap1 = IntPtr.Zero;
      int adjustableArrowCap2 = SafeNativeMethods.Gdip.GdipCreateAdjustableArrowCap(height, width, isFilled, out adjustableArrowCap1);
      if (adjustableArrowCap2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(adjustableArrowCap2);
      this.SetNativeLineCap(adjustableArrowCap1);
    }

    private void _SetHeight(float height)
    {
      int status = SafeNativeMethods.Gdip.GdipSetAdjustableArrowCapHeight(new HandleRef((object) this, (IntPtr) this.nativeCap), height);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private float _GetHeight()
    {
      float height;
      int adjustableArrowCapHeight = SafeNativeMethods.Gdip.GdipGetAdjustableArrowCapHeight(new HandleRef((object) this, (IntPtr) this.nativeCap), out height);
      if (adjustableArrowCapHeight != 0)
        throw SafeNativeMethods.Gdip.StatusException(adjustableArrowCapHeight);
      return height;
    }

    /// <summary>Gets or sets the height of the arrow cap.</summary>
    /// <returns>The height of the arrow cap.</returns>
    public float Height
    {
      get => this._GetHeight();
      set => this._SetHeight(value);
    }

    private void _SetWidth(float width)
    {
      int status = SafeNativeMethods.Gdip.GdipSetAdjustableArrowCapWidth(new HandleRef((object) this, (IntPtr) this.nativeCap), width);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private float _GetWidth()
    {
      float width;
      int adjustableArrowCapWidth = SafeNativeMethods.Gdip.GdipGetAdjustableArrowCapWidth(new HandleRef((object) this, (IntPtr) this.nativeCap), out width);
      if (adjustableArrowCapWidth != 0)
        throw SafeNativeMethods.Gdip.StatusException(adjustableArrowCapWidth);
      return width;
    }

    /// <summary>Gets or sets the width of the arrow cap.</summary>
    /// <returns>The width, in units, of the arrow cap.</returns>
    public float Width
    {
      get => this._GetWidth();
      set => this._SetWidth(value);
    }

    private void _SetMiddleInset(float middleInset)
    {
      int status = SafeNativeMethods.Gdip.GdipSetAdjustableArrowCapMiddleInset(new HandleRef((object) this, (IntPtr) this.nativeCap), middleInset);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private float _GetMiddleInset()
    {
      float middleInset;
      int arrowCapMiddleInset = SafeNativeMethods.Gdip.GdipGetAdjustableArrowCapMiddleInset(new HandleRef((object) this, (IntPtr) this.nativeCap), out middleInset);
      if (arrowCapMiddleInset != 0)
        throw SafeNativeMethods.Gdip.StatusException(arrowCapMiddleInset);
      return middleInset;
    }

    /// <summary>Gets or sets the number of units between the outline of the arrow cap and the fill.</summary>
    /// <returns>The number of units between the outline of the arrow cap and the fill of the arrow cap.</returns>
    public float MiddleInset
    {
      get => this._GetMiddleInset();
      set => this._SetMiddleInset(value);
    }

    private void _SetFillState(bool isFilled)
    {
      int status = SafeNativeMethods.Gdip.GdipSetAdjustableArrowCapFillState(new HandleRef((object) this, (IntPtr) this.nativeCap), isFilled);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private bool _IsFilled()
    {
      bool fillState = false;
      int arrowCapFillState = SafeNativeMethods.Gdip.GdipGetAdjustableArrowCapFillState(new HandleRef((object) this, (IntPtr) this.nativeCap), out fillState);
      if (arrowCapFillState != 0)
        throw SafeNativeMethods.Gdip.StatusException(arrowCapFillState);
      return fillState;
    }

    /// <summary>Gets or sets whether the arrow cap is filled.</summary>
    /// <returns>This property is <see langword="true" /> if the arrow cap is filled; otherwise, <see langword="false" />.</returns>
    public bool Filled
    {
      get => this._IsFilled();
      set => this._SetFillState(value);
    }
  }
}
