// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ColorMatrix
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  /// <summary>Defines a 5 x 5 matrix that contains the coordinates for the RGBAW space. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color matrix. This class cannot be inherited.</summary>
  [StructLayout(LayoutKind.Sequential)]
  public sealed class ColorMatrix
  {
    private float matrix00;
    private float matrix01;
    private float matrix02;
    private float matrix03;
    private float matrix04;
    private float matrix10;
    private float matrix11;
    private float matrix12;
    private float matrix13;
    private float matrix14;
    private float matrix20;
    private float matrix21;
    private float matrix22;
    private float matrix23;
    private float matrix24;
    private float matrix30;
    private float matrix31;
    private float matrix32;
    private float matrix33;
    private float matrix34;
    private float matrix40;
    private float matrix41;
    private float matrix42;
    private float matrix43;
    private float matrix44;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class.</summary>
    public ColorMatrix()
    {
      this.matrix00 = 1f;
      this.matrix11 = 1f;
      this.matrix22 = 1f;
      this.matrix33 = 1f;
      this.matrix44 = 1f;
    }

    /// <summary>Gets or sets the element at the 0 (zero) row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the 0 row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix00
    {
      get => this.matrix00;
      set => this.matrix00 = value;
    }

    /// <summary>Gets or sets the element at the 0 (zero) row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the 0 row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" /> .</returns>
    public float Matrix01
    {
      get => this.matrix01;
      set => this.matrix01 = value;
    }

    /// <summary>Gets or sets the element at the 0 (zero) row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the 0 row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix02
    {
      get => this.matrix02;
      set => this.matrix02 = value;
    }

    /// <summary>Gets or sets the element at the 0 (zero) row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
    /// <returns>The element at the 0 row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix03
    {
      get => this.matrix03;
      set => this.matrix03 = value;
    }

    /// <summary>Gets or sets the element at the 0 (zero) row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the 0 row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix04
    {
      get => this.matrix04;
      set => this.matrix04 = value;
    }

    /// <summary>Gets or sets the element at the first row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the first row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix10
    {
      get => this.matrix10;
      set => this.matrix10 = value;
    }

    /// <summary>Gets or sets the element at the first row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the first row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix11
    {
      get => this.matrix11;
      set => this.matrix11 = value;
    }

    /// <summary>Gets or sets the element at the first row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the first row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix12
    {
      get => this.matrix12;
      set => this.matrix12 = value;
    }

    /// <summary>Gets or sets the element at the first row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
    /// <returns>The element at the first row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix13
    {
      get => this.matrix13;
      set => this.matrix13 = value;
    }

    /// <summary>Gets or sets the element at the first row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the first row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix14
    {
      get => this.matrix14;
      set => this.matrix14 = value;
    }

    /// <summary>Gets or sets the element at the second row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the second row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix20
    {
      get => this.matrix20;
      set => this.matrix20 = value;
    }

    /// <summary>Gets or sets the element at the second row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the second row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix21
    {
      get => this.matrix21;
      set => this.matrix21 = value;
    }

    /// <summary>Gets or sets the element at the second row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the second row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix22
    {
      get => this.matrix22;
      set => this.matrix22 = value;
    }

    /// <summary>Gets or sets the element at the second row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the second row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix23
    {
      get => this.matrix23;
      set => this.matrix23 = value;
    }

    /// <summary>Gets or sets the element at the second row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the second row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix24
    {
      get => this.matrix24;
      set => this.matrix24 = value;
    }

    /// <summary>Gets or sets the element at the third row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the third row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix30
    {
      get => this.matrix30;
      set => this.matrix30 = value;
    }

    /// <summary>Gets or sets the element at the third row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the third row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix31
    {
      get => this.matrix31;
      set => this.matrix31 = value;
    }

    /// <summary>Gets or sets the element at the third row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the third row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix32
    {
      get => this.matrix32;
      set => this.matrix32 = value;
    }

    /// <summary>Gets or sets the element at the third row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
    /// <returns>The element at the third row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix33
    {
      get => this.matrix33;
      set => this.matrix33 = value;
    }

    /// <summary>Gets or sets the element at the third row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the third row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix34
    {
      get => this.matrix34;
      set => this.matrix34 = value;
    }

    /// <summary>Gets or sets the element at the fourth row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the fourth row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix40
    {
      get => this.matrix40;
      set => this.matrix40 = value;
    }

    /// <summary>Gets or sets the element at the fourth row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the fourth row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix41
    {
      get => this.matrix41;
      set => this.matrix41 = value;
    }

    /// <summary>Gets or sets the element at the fourth row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the fourth row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix42
    {
      get => this.matrix42;
      set => this.matrix42 = value;
    }

    /// <summary>Gets or sets the element at the fourth row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
    /// <returns>The element at the fourth row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix43
    {
      get => this.matrix43;
      set => this.matrix43 = value;
    }

    /// <summary>Gets or sets the element at the fourth row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <returns>The element at the fourth row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
    public float Matrix44
    {
      get => this.matrix44;
      set => this.matrix44 = value;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class using the elements in the specified matrix <paramref name="newColorMatrix" />.</summary>
    /// <param name="newColorMatrix">The values of the elements for the new <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</param>
    [CLSCompliant(false)]
    public ColorMatrix(float[][] newColorMatrix) => this.SetMatrix(newColorMatrix);

    internal void SetMatrix(float[][] newColorMatrix)
    {
      this.matrix00 = newColorMatrix[0][0];
      this.matrix01 = newColorMatrix[0][1];
      this.matrix02 = newColorMatrix[0][2];
      this.matrix03 = newColorMatrix[0][3];
      this.matrix04 = newColorMatrix[0][4];
      this.matrix10 = newColorMatrix[1][0];
      this.matrix11 = newColorMatrix[1][1];
      this.matrix12 = newColorMatrix[1][2];
      this.matrix13 = newColorMatrix[1][3];
      this.matrix14 = newColorMatrix[1][4];
      this.matrix20 = newColorMatrix[2][0];
      this.matrix21 = newColorMatrix[2][1];
      this.matrix22 = newColorMatrix[2][2];
      this.matrix23 = newColorMatrix[2][3];
      this.matrix24 = newColorMatrix[2][4];
      this.matrix30 = newColorMatrix[3][0];
      this.matrix31 = newColorMatrix[3][1];
      this.matrix32 = newColorMatrix[3][2];
      this.matrix33 = newColorMatrix[3][3];
      this.matrix34 = newColorMatrix[3][4];
      this.matrix40 = newColorMatrix[4][0];
      this.matrix41 = newColorMatrix[4][1];
      this.matrix42 = newColorMatrix[4][2];
      this.matrix43 = newColorMatrix[4][3];
      this.matrix44 = newColorMatrix[4][4];
    }

    internal float[][] GetMatrix()
    {
      float[][] matrix = new float[5][];
      for (int index = 0; index < 5; ++index)
        matrix[index] = new float[5];
      matrix[0][0] = this.matrix00;
      matrix[0][1] = this.matrix01;
      matrix[0][2] = this.matrix02;
      matrix[0][3] = this.matrix03;
      matrix[0][4] = this.matrix04;
      matrix[1][0] = this.matrix10;
      matrix[1][1] = this.matrix11;
      matrix[1][2] = this.matrix12;
      matrix[1][3] = this.matrix13;
      matrix[1][4] = this.matrix14;
      matrix[2][0] = this.matrix20;
      matrix[2][1] = this.matrix21;
      matrix[2][2] = this.matrix22;
      matrix[2][3] = this.matrix23;
      matrix[2][4] = this.matrix24;
      matrix[3][0] = this.matrix30;
      matrix[3][1] = this.matrix31;
      matrix[3][2] = this.matrix32;
      matrix[3][3] = this.matrix33;
      matrix[3][4] = this.matrix34;
      matrix[4][0] = this.matrix40;
      matrix[4][1] = this.matrix41;
      matrix[4][2] = this.matrix42;
      matrix[4][3] = this.matrix43;
      matrix[4][4] = this.matrix44;
      return matrix;
    }

    /// <summary>Gets or sets the element at the specified row and column in the <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
    /// <param name="row">The row of the element.</param>
    /// <param name="column">The column of the element.</param>
    /// <returns>The element at the specified row and column.</returns>
    public float this[int row, int column]
    {
      get => this.GetMatrix()[row][column];
      set
      {
        float[][] matrix = this.GetMatrix();
        matrix[row][column] = value;
        this.SetMatrix(matrix);
      }
    }
  }
}
