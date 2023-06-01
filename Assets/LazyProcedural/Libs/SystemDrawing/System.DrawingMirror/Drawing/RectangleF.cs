// Decompiled with JetBrains decompiler
// Type: System.Drawing.RectangleF
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Internal;
using System.Globalization;

namespace System.Drawing
{
  /// <summary>Stores a set of four floating-point numbers that represent the location and size of a rectangle. For more advanced region functions, use a <see cref="T:System.Drawing.Region" /> object.</summary>
  [Serializable]
  public struct RectangleF
  {
    /// <summary>Represents an instance of the <see cref="T:System.Drawing.RectangleF" /> class with its members uninitialized.</summary>
    public static readonly RectangleF Empty;
    private float x;
    private float y;
    private float width;
    private float height;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    public RectangleF(float x, float y, float width, float height)
    {
      this.x = x;
      this.y = y;
      this.width = width;
      this.height = height;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
    /// <param name="location">A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of the rectangular region.</param>
    /// <param name="size">A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of the rectangular region.</param>
    public RectangleF(PointF location, SizeF size)
    {
      this.x = location.X;
      this.y = location.Y;
      this.width = size.Width;
      this.height = size.Height;
    }

    /// <summary>Creates a <see cref="T:System.Drawing.RectangleF" /> structure with upper-left corner and lower-right corner at the specified locations.</summary>
    /// <param name="left">The x-coordinate of the upper-left corner of the rectangular region.</param>
    /// <param name="top">The y-coordinate of the upper-left corner of the rectangular region.</param>
    /// <param name="right">The x-coordinate of the lower-right corner of the rectangular region.</param>
    /// <param name="bottom">The y-coordinate of the lower-right corner of the rectangular region.</param>
    /// <returns>The new <see cref="T:System.Drawing.RectangleF" /> that this method creates.</returns>
    public static RectangleF FromLTRB(float left, float top, float right, float bottom) => new RectangleF(left, top, right - left, bottom - top);

    /// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
    [Browsable(false)]
    public PointF Location
    {
      get => new PointF(this.X, this.Y);
      set
      {
        this.X = value.X;
        this.Y = value.Y;
      }
    }

    /// <summary>Gets or sets the size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
    [Browsable(false)]
    public SizeF Size
    {
      get => new SizeF(this.Width, this.Height);
      set
      {
        this.Width = value.Width;
        this.Height = value.Height;
      }
    }

    /// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
    public float X
    {
      get => this.x;
      set => this.x = value;
    }

    /// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
    public float Y
    {
      get => this.y;
      set => this.y = value;
    }

    /// <summary>Gets or sets the width of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>The width of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
    public float Width
    {
      get => this.width;
      set => this.width = value;
    }

    /// <summary>Gets or sets the height of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>The height of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
    public float Height
    {
      get => this.height;
      set => this.height = value;
    }

    /// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
    [Browsable(false)]
    public float Left => this.X;

    /// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
    [Browsable(false)]
    public float Top => this.Y;

    /// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
    [Browsable(false)]
    public float Right => this.X + this.Width;

    /// <summary>Gets the y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
    [Browsable(false)]
    public float Bottom => this.Y + this.Height;

    /// <summary>Gets a value that indicates whether the <see cref="P:System.Drawing.RectangleF.Width" /> or <see cref="P:System.Drawing.RectangleF.Height" /> property of this <see cref="T:System.Drawing.RectangleF" /> has a value of zero.</summary>
    /// <returns>
    /// <see langword="true" /> if the <see cref="P:System.Drawing.RectangleF.Width" /> or <see cref="P:System.Drawing.RectangleF.Height" /> property of this <see cref="T:System.Drawing.RectangleF" /> has a value of zero; otherwise, <see langword="false" />.</returns>
    [Browsable(false)]
    public bool IsEmpty => (double) this.Width <= 0.0 || (double) this.Height <= 0.0;

    /// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> with the same location and size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
    /// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> and its <see langword="X" />, <see langword="Y" />, <see langword="Width" />, and <see langword="Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.RectangleF" />; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object obj) => obj is RectangleF rectangleF && (double) rectangleF.X == (double) this.X && (double) rectangleF.Y == (double) this.Y && (double) rectangleF.Width == (double) this.Width && (double) rectangleF.Height == (double) this.Height;

    /// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures have equal location and size.</summary>
    /// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the equality operator.</param>
    /// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the equality operator.</param>
    /// <returns>
    /// <see langword="true" /> if the two specified <see cref="T:System.Drawing.RectangleF" /> structures have equal <see cref="P:System.Drawing.RectangleF.X" />, <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, and <see cref="P:System.Drawing.RectangleF.Height" /> properties; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(RectangleF left, RectangleF right) => (double) left.X == (double) right.X && (double) left.Y == (double) right.Y && (double) left.Width == (double) right.Width && (double) left.Height == (double) right.Height;

    /// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures differ in location or size.</summary>
    /// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the inequality operator.</param>
    /// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the inequality operator.</param>
    /// <returns>
    /// <see langword="true" /> if any of the <see cref="P:System.Drawing.RectangleF.X" /> , <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, or <see cref="P:System.Drawing.RectangleF.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(RectangleF left, RectangleF right) => !(left == right);

    /// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <returns>
    /// <see langword="true" /> if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.RectangleF" /> structure; otherwise, <see langword="false" />.</returns>
    public bool Contains(float x, float y) => (double) this.X <= (double) x && (double) x < (double) this.X + (double) this.Width && (double) this.Y <= (double) y && (double) y < (double) this.Y + (double) this.Height;

    /// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to test.</param>
    /// <returns>
    /// <see langword="true" /> if the point represented by the <paramref name="pt" /> parameter is contained within this <see cref="T:System.Drawing.RectangleF" /> structure; otherwise, <see langword="false" />.</returns>
    public bool Contains(PointF pt) => this.Contains(pt.X, pt.Y);

    /// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to test.</param>
    /// <returns>
    /// <see langword="true" /> if the rectangular region represented by <paramref name="rect" /> is entirely contained within the rectangular region represented by this <see cref="T:System.Drawing.RectangleF" />; otherwise, <see langword="false" />.</returns>
    public bool Contains(RectangleF rect) => (double) this.X <= (double) rect.X && (double) rect.X + (double) rect.Width <= (double) this.X + (double) this.Width && (double) this.Y <= (double) rect.Y && (double) rect.Y + (double) rect.Height <= (double) this.Y + (double) this.Height;

    /// <summary>Gets the hash code for this <see cref="T:System.Drawing.RectangleF" /> structure. For information about the use of hash codes, see <see langword="Object.GetHashCode" />.</summary>
    /// <returns>The hash code for this <see cref="T:System.Drawing.RectangleF" />.</returns>
    public override int GetHashCode() => (int) (uint) this.X ^ ((int) (uint) this.Y << 13 | (int) ((uint) this.Y >> 19)) ^ ((int) (uint) this.Width << 26 | (int) ((uint) this.Width >> 6)) ^ ((int) (uint) this.Height << 7 | (int) ((uint) this.Height >> 25));

    /// <summary>Enlarges this <see cref="T:System.Drawing.RectangleF" /> structure by the specified amount.</summary>
    /// <param name="x">The amount to inflate this <see cref="T:System.Drawing.RectangleF" /> structure horizontally.</param>
    /// <param name="y">The amount to inflate this <see cref="T:System.Drawing.RectangleF" /> structure vertically.</param>
    public void Inflate(float x, float y)
    {
      this.X -= x;
      this.Y -= y;
      this.Width += 2f * x;
      this.Height += 2f * y;
    }

    /// <summary>Enlarges this <see cref="T:System.Drawing.RectangleF" /> by the specified amount.</summary>
    /// <param name="size">The amount to inflate this rectangle.</param>
    public void Inflate(SizeF size) => this.Inflate(size.Width, size.Height);

    /// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.RectangleF" /> structure. The copy is enlarged by the specified amount and the original rectangle remains unmodified.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to be copied. This rectangle is not modified.</param>
    /// <param name="x">The amount to enlarge the copy of the rectangle horizontally.</param>
    /// <param name="y">The amount to enlarge the copy of the rectangle vertically.</param>
    /// <returns>The enlarged <see cref="T:System.Drawing.RectangleF" />.</returns>
    public static RectangleF Inflate(RectangleF rect, float x, float y)
    {
      RectangleF rectangleF = rect;
      rectangleF.Inflate(x, y);
      return rectangleF;
    }

    /// <summary>Replaces this <see cref="T:System.Drawing.RectangleF" /> structure with the intersection of itself and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">The rectangle to intersect.</param>
    public void Intersect(RectangleF rect)
    {
      RectangleF rectangleF = RectangleF.Intersect(rect, this);
      this.X = rectangleF.X;
      this.Y = rectangleF.Y;
      this.Width = rectangleF.Width;
      this.Height = rectangleF.Height;
    }

    /// <summary>Returns a <see cref="T:System.Drawing.RectangleF" /> structure that represents the intersection of two rectangles. If there is no intersection, and empty <see cref="T:System.Drawing.RectangleF" /> is returned.</summary>
    /// <param name="a">A rectangle to intersect.</param>
    /// <param name="b">A rectangle to intersect.</param>
    /// <returns>A third <see cref="T:System.Drawing.RectangleF" /> structure the size of which represents the overlapped area of the two specified rectangles.</returns>
    public static RectangleF Intersect(RectangleF a, RectangleF b)
    {
      float x = Math.Max(a.X, b.X);
      float num1 = Math.Min(a.X + a.Width, b.X + b.Width);
      float y = Math.Max(a.Y, b.Y);
      float num2 = Math.Min(a.Y + a.Height, b.Y + b.Height);
      return (double) num1 >= (double) x && (double) num2 >= (double) y ? new RectangleF(x, y, num1 - x, num2 - y) : RectangleF.Empty;
    }

    /// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
    /// <param name="rect">The rectangle to test.</param>
    /// <returns>
    /// <see langword="true" /> if there is any intersection; otherwise, <see langword="false" />.</returns>
    public bool IntersectsWith(RectangleF rect) => (double) rect.X < (double) this.X + (double) this.Width && (double) this.X < (double) rect.X + (double) rect.Width && (double) rect.Y < (double) this.Y + (double) this.Height && (double) this.Y < (double) rect.Y + (double) rect.Height;

    /// <summary>Creates the smallest possible third rectangle that can contain both of two rectangles that form a union.</summary>
    /// <param name="a">A rectangle to union.</param>
    /// <param name="b">A rectangle to union.</param>
    /// <returns>A third <see cref="T:System.Drawing.RectangleF" /> structure that contains both of the two rectangles that form the union.</returns>
    public static RectangleF Union(RectangleF a, RectangleF b)
    {
      float x = Math.Min(a.X, b.X);
      float num1 = Math.Max(a.X + a.Width, b.X + b.Width);
      float y = Math.Min(a.Y, b.Y);
      float num2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
      return new RectangleF(x, y, num1 - x, num2 - y);
    }

    /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
    /// <param name="pos">The amount to offset the location.</param>
    public void Offset(PointF pos) => this.Offset(pos.X, pos.Y);

    /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
    /// <param name="x">The amount to offset the location horizontally.</param>
    /// <param name="y">The amount to offset the location vertically.</param>
    public void Offset(float x, float y)
    {
      this.X += x;
      this.Y += y;
    }

    internal GPRECTF ToGPRECTF() => new GPRECTF(this.X, this.Y, this.Width, this.Height);

    /// <summary>Converts the specified <see cref="T:System.Drawing.Rectangle" /> structure to a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> structure to convert.</param>
    /// <returns>The <see cref="T:System.Drawing.RectangleF" /> structure that is converted from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
    public static implicit operator RectangleF(Rectangle r) => new RectangleF((float) r.X, (float) r.Y, (float) r.Width, (float) r.Height);

    /// <summary>Converts the <see langword="Location" /> and <see cref="T:System.Drawing.Size" /> of this <see cref="T:System.Drawing.RectangleF" /> to a human-readable string.</summary>
    /// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.RectangleF" /> structure. For example, "{X=20, Y=20, Width=100, Height=50}".</returns>
    public override string ToString() => "{X=" + this.X.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ",Width=" + this.Width.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ",Height=" + this.Height.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "}";
  }
}
