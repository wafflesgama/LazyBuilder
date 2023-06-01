// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.Matrix
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
  /// <summary>Encapsulates a 3-by-3 affine matrix that represents a geometric transform. This class cannot be inherited.</summary>
  public sealed class Matrix : MarshalByRefObject, IDisposable
  {
    internal IntPtr nativeMatrix;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class as the identity matrix.</summary>
    public Matrix()
    {
      IntPtr matrix1 = IntPtr.Zero;
      int matrix2 = SafeNativeMethods.Gdip.GdipCreateMatrix(out matrix1);
      if (matrix2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(matrix2);
      this.nativeMatrix = matrix1;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class with the specified elements.</summary>
    /// <param name="m11">The value in the first row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    /// <param name="m12">The value in the first row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    /// <param name="m21">The value in the second row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    /// <param name="m22">The value in the second row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    /// <param name="dx">The value in the third row and first column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    /// <param name="dy">The value in the third row and second column of the new <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    public Matrix(float m11, float m12, float m21, float m22, float dx, float dy)
    {
      IntPtr matrix = IntPtr.Zero;
      int matrix2 = SafeNativeMethods.Gdip.GdipCreateMatrix2(m11, m12, m21, m22, dx, dy, out matrix);
      if (matrix2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(matrix2);
      this.nativeMatrix = matrix;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class to the geometric transform defined by the specified rectangle and array of points.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle to be transformed.</param>
    /// <param name="plgpts">An array of three <see cref="T:System.Drawing.PointF" /> structures that represents the points of a parallelogram to which the upper-left, upper-right, and lower-left corners of the rectangle is to be transformed. The lower-right corner of the parallelogram is implied by the first three corners.</param>
    public Matrix(RectangleF rect, PointF[] plgpts)
    {
      if (plgpts == null)
        throw new ArgumentNullException(nameof (plgpts));
      IntPtr num = plgpts.Length == 3 ? SafeNativeMethods.Gdip.ConvertPointToMemory(plgpts) : throw SafeNativeMethods.Gdip.StatusException(2);
      try
      {
        IntPtr matrix = IntPtr.Zero;
        GPRECTF rect1 = new GPRECTF(rect);
        int matrix3 = SafeNativeMethods.Gdip.GdipCreateMatrix3(ref rect1, new HandleRef((object) null, num), out matrix);
        if (matrix3 != 0)
          throw SafeNativeMethods.Gdip.StatusException(matrix3);
        this.nativeMatrix = matrix;
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class to the geometric transform defined by the specified rectangle and array of points.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to be transformed.</param>
    /// <param name="plgpts">An array of three <see cref="T:System.Drawing.Point" /> structures that represents the points of a parallelogram to which the upper-left, upper-right, and lower-left corners of the rectangle is to be transformed. The lower-right corner of the parallelogram is implied by the first three corners.</param>
    public Matrix(Rectangle rect, Point[] plgpts)
    {
      if (plgpts == null)
        throw new ArgumentNullException(nameof (plgpts));
      IntPtr num = plgpts.Length == 3 ? SafeNativeMethods.Gdip.ConvertPointToMemory(plgpts) : throw SafeNativeMethods.Gdip.StatusException(2);
      try
      {
        IntPtr matrix = IntPtr.Zero;
        GPRECT rect1 = new GPRECT(rect);
        int matrix3I = SafeNativeMethods.Gdip.GdipCreateMatrix3I(ref rect1, new HandleRef((object) null, num), out matrix);
        if (matrix3I != 0)
          throw SafeNativeMethods.Gdip.StatusException(matrix3I);
        this.nativeMatrix = matrix;
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.nativeMatrix != IntPtr.Zero))
        return;
      SafeNativeMethods.Gdip.GdipDeleteMatrix(new HandleRef((object) this, this.nativeMatrix));
      this.nativeMatrix = IntPtr.Zero;
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~Matrix() => this.Dispose(false);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that this method creates.</returns>
    public Matrix Clone()
    {
      IntPtr cloneMatrix = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneMatrix(new HandleRef((object) this, this.nativeMatrix), out cloneMatrix);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return new Matrix(cloneMatrix);
    }

    /// <summary>Gets an array of floating-point values that represents the elements of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <returns>An array of floating-point values that represents the elements of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
    public float[] Elements
    {
      get
      {
        IntPtr num = Marshal.AllocHGlobal(48);
        float[] destination;
        try
        {
          int matrixElements = SafeNativeMethods.Gdip.GdipGetMatrixElements(new HandleRef((object) this, this.nativeMatrix), num);
          if (matrixElements != 0)
            throw SafeNativeMethods.Gdip.StatusException(matrixElements);
          destination = new float[6];
          Marshal.Copy(num, destination, 0, 6);
        }
        finally
        {
          Marshal.FreeHGlobal(num);
        }
        return destination;
      }
    }

    /// <summary>Gets the x translation value (the dx value, or the element in the third row and first column) of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <returns>The x translation value of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
    public float OffsetX => this.Elements[4];

    /// <summary>Gets the y translation value (the dy value, or the element in the third row and second column) of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <returns>The y translation value of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
    public float OffsetY => this.Elements[5];

    /// <summary>Resets this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to have the elements of the identity matrix.</summary>
    public void Reset()
    {
      int status = SafeNativeMethods.Gdip.GdipSetMatrixElements(new HandleRef((object) this, this.nativeMatrix), 1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Multiplies this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by the matrix specified in the <paramref name="matrix" /> parameter, by prepending the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is to be multiplied.</param>
    public void Multiply(Matrix matrix) => this.Multiply(matrix, MatrixOrder.Prepend);

    /// <summary>Multiplies this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by the matrix specified in the <paramref name="matrix" /> parameter, and in the order specified in the <paramref name="order" /> parameter.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is to be multiplied.</param>
    /// <param name="order">The <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that represents the order of the multiplication.</param>
    public void Multiply(Matrix matrix, MatrixOrder order)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int status = SafeNativeMethods.Gdip.GdipMultiplyMatrix(new HandleRef((object) this, this.nativeMatrix), new HandleRef((object) matrix, matrix.nativeMatrix), order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies the specified translation vector (<paramref name="offsetX" /> and <paramref name="offsetY" />) to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the translation vector.</summary>
    /// <param name="offsetX">The x value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    /// <param name="offsetY">The y value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    public void Translate(float offsetX, float offsetY) => this.Translate(offsetX, offsetY, MatrixOrder.Prepend);

    /// <summary>Applies the specified translation vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
    /// <param name="offsetX">The x value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    /// <param name="offsetY">The y value by which to translate this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the translation is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    public void Translate(float offsetX, float offsetY, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef((object) this, this.nativeMatrix), offsetX, offsetY, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies the specified scale vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the scale vector.</summary>
    /// <param name="scaleX">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the x-axis direction.</param>
    /// <param name="scaleY">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the y-axis direction.</param>
    public void Scale(float scaleX, float scaleY) => this.Scale(scaleX, scaleY, MatrixOrder.Prepend);

    /// <summary>Applies the specified scale vector (<paramref name="scaleX" /> and <paramref name="scaleY" />) to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> using the specified order.</summary>
    /// <param name="scaleX">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the x-axis direction.</param>
    /// <param name="scaleY">The value by which to scale this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the y-axis direction.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the scale vector is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    public void Scale(float scaleX, float scaleY, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipScaleMatrix(new HandleRef((object) this, this.nativeMatrix), scaleX, scaleY, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Prepend to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> a clockwise rotation, around the origin and by the specified angle.</summary>
    /// <param name="angle">The angle of the rotation, in degrees.</param>
    public void Rotate(float angle) => this.Rotate(angle, MatrixOrder.Prepend);

    /// <summary>Applies a clockwise rotation of an amount specified in the <paramref name="angle" /> parameter, around the origin (zero x and y coordinates) for this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <param name="angle">The angle (extent) of the rotation, in degrees.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the rotation is applied to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</param>
    public void Rotate(float angle, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipRotateMatrix(new HandleRef((object) this, this.nativeMatrix), angle, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies a clockwise rotation to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> around the point specified in the <paramref name="point" /> parameter, and by prepending the rotation.</summary>
    /// <param name="angle">The angle (extent) of the rotation, in degrees.</param>
    /// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the center of the rotation.</param>
    public void RotateAt(float angle, PointF point) => this.RotateAt(angle, point, MatrixOrder.Prepend);

    /// <summary>Applies a clockwise rotation about the specified point to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
    /// <param name="angle">The angle of the rotation, in degrees.</param>
    /// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the center of the rotation.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the rotation is applied.</param>
    public void RotateAt(float angle, PointF point, MatrixOrder order)
    {
      int status = order != MatrixOrder.Prepend ? SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef((object) this, this.nativeMatrix), -point.X, -point.Y, order) | SafeNativeMethods.Gdip.GdipRotateMatrix(new HandleRef((object) this, this.nativeMatrix), angle, order) | SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef((object) this, this.nativeMatrix), point.X, point.Y, order) : SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef((object) this, this.nativeMatrix), point.X, point.Y, order) | SafeNativeMethods.Gdip.GdipRotateMatrix(new HandleRef((object) this, this.nativeMatrix), angle, order) | SafeNativeMethods.Gdip.GdipTranslateMatrix(new HandleRef((object) this, this.nativeMatrix), -point.X, -point.Y, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies the specified shear vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> by prepending the shear transformation.</summary>
    /// <param name="shearX">The horizontal shear factor.</param>
    /// <param name="shearY">The vertical shear factor.</param>
    public void Shear(float shearX, float shearY)
    {
      int status = SafeNativeMethods.Gdip.GdipShearMatrix(new HandleRef((object) this, this.nativeMatrix), shearX, shearY, MatrixOrder.Prepend);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies the specified shear vector to this <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
    /// <param name="shearX">The horizontal shear factor.</param>
    /// <param name="shearY">The vertical shear factor.</param>
    /// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies the order (append or prepend) in which the shear is applied.</param>
    public void Shear(float shearX, float shearY, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipShearMatrix(new HandleRef((object) this, this.nativeMatrix), shearX, shearY, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Inverts this <see cref="T:System.Drawing.Drawing2D.Matrix" />, if it is invertible.</summary>
    public void Invert()
    {
      int status = SafeNativeMethods.Gdip.GdipInvertMatrix(new HandleRef((object) this, this.nativeMatrix));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies the geometric transform represented by this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to a specified array of points.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points to transform.</param>
    public void TransformPoints(PointF[] pts)
    {
      IntPtr num = pts != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(pts) : throw new ArgumentNullException(nameof (pts));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipTransformMatrixPoints(new HandleRef((object) this, this.nativeMatrix), new HandleRef((object) null, num), pts.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        PointF[] pointFArray = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(num, pts.Length);
        for (int index = 0; index < pts.Length; ++index)
          pts[index] = pointFArray[index];
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Applies the geometric transform represented by this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to a specified array of points.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
    public void TransformPoints(Point[] pts)
    {
      IntPtr num = pts != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(pts) : throw new ArgumentNullException(nameof (pts));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipTransformMatrixPointsI(new HandleRef((object) this, this.nativeMatrix), new HandleRef((object) null, num), pts.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        Point[] pointArray = SafeNativeMethods.Gdip.ConvertGPPOINTArray(num, pts.Length);
        for (int index = 0; index < pts.Length; ++index)
          pts[index] = pointArray[index];
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Multiplies each vector in an array by the matrix. The translation elements of this matrix (third row) are ignored.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
    public void TransformVectors(PointF[] pts)
    {
      IntPtr num = pts != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(pts) : throw new ArgumentNullException(nameof (pts));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipVectorTransformMatrixPoints(new HandleRef((object) this, this.nativeMatrix), new HandleRef((object) null, num), pts.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        PointF[] pointFArray = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(num, pts.Length);
        for (int index = 0; index < pts.Length; ++index)
          pts[index] = pointFArray[index];
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Multiplies each vector in an array by the matrix. The translation elements of this matrix (third row) are ignored.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
    public void VectorTransformPoints(Point[] pts) => this.TransformVectors(pts);

    /// <summary>Applies only the scale and rotate components of this <see cref="T:System.Drawing.Drawing2D.Matrix" /> to the specified array of points.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transform.</param>
    public void TransformVectors(Point[] pts)
    {
      IntPtr num = pts != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(pts) : throw new ArgumentNullException(nameof (pts));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipVectorTransformMatrixPointsI(new HandleRef((object) this, this.nativeMatrix), new HandleRef((object) null, num), pts.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        Point[] pointArray = SafeNativeMethods.Gdip.ConvertGPPOINTArray(num, pts.Length);
        for (int index = 0; index < pts.Length; ++index)
          pts[index] = pointArray[index];
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is invertible.</summary>
    /// <returns>This property is <see langword="true" /> if this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is invertible; otherwise, <see langword="false" />.</returns>
    public bool IsInvertible
    {
      get
      {
        int boolean;
        int status = SafeNativeMethods.Gdip.GdipIsMatrixInvertible(new HandleRef((object) this, this.nativeMatrix), out boolean);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        return boolean != 0;
      }
    }

    /// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is the identity matrix.</summary>
    /// <returns>This property is <see langword="true" /> if this <see cref="T:System.Drawing.Drawing2D.Matrix" /> is identity; otherwise, <see langword="false" />.</returns>
    public bool IsIdentity
    {
      get
      {
        int boolean;
        int status = SafeNativeMethods.Gdip.GdipIsMatrixIdentity(new HandleRef((object) this, this.nativeMatrix), out boolean);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        return boolean != 0;
      }
    }

    /// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Drawing2D.Matrix" /> and is identical to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <param name="obj">The object to test.</param>
    /// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> identical to this <see cref="T:System.Drawing.Drawing2D.Matrix" />; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object obj)
    {
      if (!(obj is Matrix wrapper))
        return false;
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsMatrixEqual(new HandleRef((object) this, this.nativeMatrix), new HandleRef((object) wrapper, wrapper.nativeMatrix), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Returns a hash code.</summary>
    /// <returns>The hash code for this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
    public override int GetHashCode() => base.GetHashCode();

    internal Matrix(IntPtr nativeMatrix) => this.SetNativeMatrix(nativeMatrix);

    internal void SetNativeMatrix(IntPtr nativeMatrix) => this.nativeMatrix = nativeMatrix;
  }
}
