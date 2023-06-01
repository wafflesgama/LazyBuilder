// Decompiled with JetBrains decompiler
// Type: System.Drawing.TextureBrush
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Drawing
{
  /// <summary>Each property of the <see cref="T:System.Drawing.TextureBrush" /> class is a <see cref="T:System.Drawing.Brush" /> object that uses an image to fill the interior of a shape. This class cannot be inherited.</summary>
  public sealed class TextureBrush : Brush
  {
    /// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image.</summary>
    /// <param name="bitmap">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors.</param>
    public TextureBrush(Image bitmap)
      : this(bitmap, WrapMode.Tile)
    {
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image and wrap mode.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors.</param>
    /// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how this <see cref="T:System.Drawing.TextureBrush" /> object is tiled.</param>
    public TextureBrush(Image image, WrapMode wrapMode)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (!ClientUtils.IsEnumValid((Enum) wrapMode, (int) wrapMode, 0, 4))
        throw new InvalidEnumArgumentException(nameof (wrapMode), (int) wrapMode, typeof (WrapMode));
      IntPtr texture1 = IntPtr.Zero;
      int texture2 = SafeNativeMethods.Gdip.GdipCreateTexture(new HandleRef((object) image, image.nativeImage), (int) wrapMode, out texture1);
      if (texture2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(texture2);
      this.SetNativeBrushInternal(texture1);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image, wrap mode, and bounding rectangle.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors.</param>
    /// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how this <see cref="T:System.Drawing.TextureBrush" /> object is tiled.</param>
    /// <param name="dstRect">A <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.TextureBrush" /> object.</param>
    public TextureBrush(Image image, WrapMode wrapMode, RectangleF dstRect)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (!ClientUtils.IsEnumValid((Enum) wrapMode, (int) wrapMode, 0, 4))
        throw new InvalidEnumArgumentException(nameof (wrapMode), (int) wrapMode, typeof (WrapMode));
      IntPtr texture = IntPtr.Zero;
      int texture2 = SafeNativeMethods.Gdip.GdipCreateTexture2(new HandleRef((object) image, image.nativeImage), (int) wrapMode, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, out texture);
      if (texture2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(texture2);
      this.SetNativeBrushInternal(texture);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image, wrap mode, and bounding rectangle.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors.</param>
    /// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how this <see cref="T:System.Drawing.TextureBrush" /> object is tiled.</param>
    /// <param name="dstRect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.TextureBrush" /> object.</param>
    public TextureBrush(Image image, WrapMode wrapMode, Rectangle dstRect)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (!ClientUtils.IsEnumValid((Enum) wrapMode, (int) wrapMode, 0, 4))
        throw new InvalidEnumArgumentException(nameof (wrapMode), (int) wrapMode, typeof (WrapMode));
      IntPtr texture = IntPtr.Zero;
      int texture2I = SafeNativeMethods.Gdip.GdipCreateTexture2I(new HandleRef((object) image, image.nativeImage), (int) wrapMode, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, out texture);
      if (texture2I != 0)
        throw SafeNativeMethods.Gdip.StatusException(texture2I);
      this.SetNativeBrushInternal(texture);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image and bounding rectangle.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors.</param>
    /// <param name="dstRect">A <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.TextureBrush" /> object.</param>
    public TextureBrush(Image image, RectangleF dstRect)
      : this(image, dstRect, (ImageAttributes) null)
    {
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image, bounding rectangle, and image attributes.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors.</param>
    /// <param name="dstRect">A <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.TextureBrush" /> object.</param>
    /// <param name="imageAttr">An <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object that contains additional information about the image used by this <see cref="T:System.Drawing.TextureBrush" /> object.</param>
    public TextureBrush(Image image, RectangleF dstRect, ImageAttributes imageAttr)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      IntPtr texture = IntPtr.Zero;
      int textureIa = SafeNativeMethods.Gdip.GdipCreateTextureIA(new HandleRef((object) image, image.nativeImage), new HandleRef((object) imageAttr, imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes), dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, out texture);
      if (textureIa != 0)
        throw SafeNativeMethods.Gdip.StatusException(textureIa);
      this.SetNativeBrushInternal(texture);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image and bounding rectangle.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors.</param>
    /// <param name="dstRect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.TextureBrush" /> object.</param>
    public TextureBrush(Image image, Rectangle dstRect)
      : this(image, dstRect, (ImageAttributes) null)
    {
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image, bounding rectangle, and image attributes.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors.</param>
    /// <param name="dstRect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.TextureBrush" /> object.</param>
    /// <param name="imageAttr">An <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object that contains additional information about the image used by this <see cref="T:System.Drawing.TextureBrush" /> object.</param>
    public TextureBrush(Image image, Rectangle dstRect, ImageAttributes imageAttr)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      IntPtr texture = IntPtr.Zero;
      int textureIai = SafeNativeMethods.Gdip.GdipCreateTextureIAI(new HandleRef((object) image, image.nativeImage), new HandleRef((object) imageAttr, imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes), dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, out texture);
      if (textureIai != 0)
        throw SafeNativeMethods.Gdip.StatusException(textureIai);
      this.SetNativeBrushInternal(texture);
    }

    internal TextureBrush(IntPtr nativeBrush) => this.SetNativeBrushInternal(nativeBrush);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.TextureBrush" /> object.</summary>
    /// <returns>The <see cref="T:System.Drawing.TextureBrush" /> object this method creates, cast as an <see cref="T:System.Object" /> object.</returns>
    public override object Clone()
    {
      IntPtr clonebrush = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef((object) this, this.NativeBrush), out clonebrush);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new TextureBrush(clonebrush);
    }

    private void _SetTransform(Matrix matrix)
    {
      int status = SafeNativeMethods.Gdip.GdipSetTextureTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) matrix, matrix.nativeMatrix));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private Matrix _GetTransform()
    {
      Matrix wrapper = new Matrix();
      int textureTransform = SafeNativeMethods.Gdip.GdipGetTextureTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) wrapper, wrapper.nativeMatrix));
      if (textureTransform != 0)
        throw SafeNativeMethods.Gdip.StatusException(textureTransform);
      return wrapper;
    }

    /// <summary>Gets or sets a copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> object that defines a local geometric transformation for the image associated with this <see cref="T:System.Drawing.TextureBrush" /> object.</summary>
    /// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> object that defines a geometric transformation that applies only to fills drawn by using this <see cref="T:System.Drawing.TextureBrush" /> object.</returns>
    public Matrix Transform
    {
      get => this._GetTransform();
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this._SetTransform(value);
      }
    }

    private void _SetWrapMode(WrapMode wrapMode)
    {
      int status = SafeNativeMethods.Gdip.GdipSetTextureWrapMode(new HandleRef((object) this, this.NativeBrush), (int) wrapMode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private WrapMode _GetWrapMode()
    {
      int wrapMode = 0;
      int textureWrapMode = SafeNativeMethods.Gdip.GdipGetTextureWrapMode(new HandleRef((object) this, this.NativeBrush), out wrapMode);
      if (textureWrapMode != 0)
        throw SafeNativeMethods.Gdip.StatusException(textureWrapMode);
      return (WrapMode) wrapMode;
    }

    /// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that indicates the wrap mode for this <see cref="T:System.Drawing.TextureBrush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how fills drawn by using this <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> object are tiled.</returns>
    public WrapMode WrapMode
    {
      get => this._GetWrapMode();
      set
      {
        if (!ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 4))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (WrapMode));
        this._SetWrapMode(value);
      }
    }

    /// <summary>Gets the <see cref="T:System.Drawing.Image" /> object associated with this <see cref="T:System.Drawing.TextureBrush" /> object.</summary>
    /// <returns>An <see cref="T:System.Drawing.Image" /> object that represents the image with which this <see cref="T:System.Drawing.TextureBrush" /> object fills shapes.</returns>
    public Image Image
    {
      get
      {
        IntPtr image;
        int textureImage = SafeNativeMethods.Gdip.GdipGetTextureImage(new HandleRef((object) this, this.NativeBrush), out image);
        if (textureImage != 0)
          throw SafeNativeMethods.Gdip.StatusException(textureImage);
        return Image.CreateImageObject(image);
      }
    }

    /// <summary>Resets the <see langword="Transform" /> property of this <see cref="T:System.Drawing.TextureBrush" /> object to identity.</summary>
    public void ResetTransform()
    {
      int status = SafeNativeMethods.Gdip.GdipResetTextureTransform(new HandleRef((object) this, this.NativeBrush));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Multiplies the <see cref="T:System.Drawing.Drawing2D.Matrix" /> object that represents the local geometric transformation of this <see cref="T:System.Drawing.TextureBrush" /> object by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> object by prepending the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> object.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> object by which to multiply the geometric transformation.</param>
    public void MultiplyTransform(Matrix matrix) => this.MultiplyTransform(matrix, MatrixOrder.Prepend);

    /// <summary>Multiplies the <see cref="T:System.Drawing.Drawing2D.Matrix" /> object that represents the local geometric transformation of this <see cref="T:System.Drawing.TextureBrush" /> object by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> object in the specified order.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> object by which to multiply the geometric transformation.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies the order in which to multiply the two matrices.</param>
    public void MultiplyTransform(Matrix matrix, MatrixOrder order)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int status = SafeNativeMethods.Gdip.GdipMultiplyTextureTransform(new HandleRef((object) this, this.NativeBrush), new HandleRef((object) matrix, matrix.nativeMatrix), order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Translates the local geometric transformation of this <see cref="T:System.Drawing.TextureBrush" /> object by the specified dimensions. This method prepends the translation to the transformation.</summary>
    /// <param name="dx">The dimension by which to translate the transformation in the x direction.</param>
    /// <param name="dy">The dimension by which to translate the transformation in the y direction.</param>
    public void TranslateTransform(float dx, float dy) => this.TranslateTransform(dx, dy, MatrixOrder.Prepend);

    /// <summary>Translates the local geometric transformation of this <see cref="T:System.Drawing.TextureBrush" /> object by the specified dimensions in the specified order.</summary>
    /// <param name="dx">The dimension by which to translate the transformation in the x direction.</param>
    /// <param name="dy">The dimension by which to translate the transformation in the y direction.</param>
    /// <param name="order">The order (prepend or append) in which to apply the translation.</param>
    public void TranslateTransform(float dx, float dy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslateTextureTransform(new HandleRef((object) this, this.NativeBrush), dx, dy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Scales the local geometric transformation of this <see cref="T:System.Drawing.TextureBrush" /> object by the specified amounts. This method prepends the scaling matrix to the transformation.</summary>
    /// <param name="sx">The amount by which to scale the transformation in the x direction.</param>
    /// <param name="sy">The amount by which to scale the transformation in the y direction.</param>
    public void ScaleTransform(float sx, float sy) => this.ScaleTransform(sx, sy, MatrixOrder.Prepend);

    /// <summary>Scales the local geometric transformation of this <see cref="T:System.Drawing.TextureBrush" /> object by the specified amounts in the specified order.</summary>
    /// <param name="sx">The amount by which to scale the transformation in the x direction.</param>
    /// <param name="sy">The amount by which to scale the transformation in the y direction.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether to append or prepend the scaling matrix.</param>
    public void ScaleTransform(float sx, float sy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipScaleTextureTransform(new HandleRef((object) this, this.NativeBrush), sx, sy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Rotates the local geometric transformation of this <see cref="T:System.Drawing.TextureBrush" /> object by the specified amount. This method prepends the rotation to the transformation.</summary>
    /// <param name="angle">The angle of rotation.</param>
    public void RotateTransform(float angle) => this.RotateTransform(angle, MatrixOrder.Prepend);

    /// <summary>Rotates the local geometric transformation of this <see cref="T:System.Drawing.TextureBrush" /> object by the specified amount in the specified order.</summary>
    /// <param name="angle">The angle of rotation.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether to append or prepend the rotation matrix.</param>
    public void RotateTransform(float angle, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipRotateTextureTransform(new HandleRef((object) this, this.NativeBrush), angle, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }
  }
}
