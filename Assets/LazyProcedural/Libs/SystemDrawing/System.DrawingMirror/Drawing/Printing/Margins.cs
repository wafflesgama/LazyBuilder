// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.Margins
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Drawing.Printing
{
  /// <summary>Specifies the dimensions of the margins of a printed page.</summary>
  [TypeConverter(typeof (MarginsConverter))]
  [Serializable]
  public class Margins : ICloneable
  {
    private int left;
    private int right;
    private int top;
    private int bottom;
    [OptionalField]
    private double doubleLeft;
    [OptionalField]
    private double doubleRight;
    [OptionalField]
    private double doubleTop;
    [OptionalField]
    private double doubleBottom;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.Margins" /> class with 1-inch wide margins.</summary>
    public Margins()
      : this(100, 100, 100, 100)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.Margins" /> class with the specified left, right, top, and bottom margins.</summary>
    /// <param name="left">The left margin, in hundredths of an inch.</param>
    /// <param name="right">The right margin, in hundredths of an inch.</param>
    /// <param name="top">The top margin, in hundredths of an inch.</param>
    /// <param name="bottom">The bottom margin, in hundredths of an inch.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="left" /> parameter value is less than 0.
    /// -or-
    /// The <paramref name="right" /> parameter value is less than 0.
    /// -or-
    /// The <paramref name="top" /> parameter value is less than 0.
    /// -or-
    /// The <paramref name="bottom" /> parameter value is less than 0.</exception>
    public Margins(int left, int right, int top, int bottom)
    {
      this.CheckMargin(left, nameof (left));
      this.CheckMargin(right, nameof (right));
      this.CheckMargin(top, nameof (top));
      this.CheckMargin(bottom, nameof (bottom));
      this.left = left;
      this.right = right;
      this.top = top;
      this.bottom = bottom;
      this.doubleLeft = (double) left;
      this.doubleRight = (double) right;
      this.doubleTop = (double) top;
      this.doubleBottom = (double) bottom;
    }

    /// <summary>Gets or sets the left margin width, in hundredths of an inch.</summary>
    /// <returns>The left margin width, in hundredths of an inch.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Left" /> property is set to a value that is less than 0.</exception>
    public int Left
    {
      get => this.left;
      set
      {
        this.CheckMargin(value, nameof (Left));
        this.left = value;
        this.doubleLeft = (double) value;
      }
    }

    /// <summary>Gets or sets the right margin width, in hundredths of an inch.</summary>
    /// <returns>The right margin width, in hundredths of an inch.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Right" /> property is set to a value that is less than 0.</exception>
    public int Right
    {
      get => this.right;
      set
      {
        this.CheckMargin(value, nameof (Right));
        this.right = value;
        this.doubleRight = (double) value;
      }
    }

    /// <summary>Gets or sets the top margin width, in hundredths of an inch.</summary>
    /// <returns>The top margin width, in hundredths of an inch.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Top" /> property is set to a value that is less than 0.</exception>
    public int Top
    {
      get => this.top;
      set
      {
        this.CheckMargin(value, nameof (Top));
        this.top = value;
        this.doubleTop = (double) value;
      }
    }

    /// <summary>Gets or sets the bottom margin, in hundredths of an inch.</summary>
    /// <returns>The bottom margin, in hundredths of an inch.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.Margins.Bottom" /> property is set to a value that is less than 0.</exception>
    public int Bottom
    {
      get => this.bottom;
      set
      {
        this.CheckMargin(value, nameof (Bottom));
        this.bottom = value;
        this.doubleBottom = (double) value;
      }
    }

    internal double DoubleLeft
    {
      get => this.doubleLeft;
      set
      {
        this.Left = (int) Math.Round(value);
        this.doubleLeft = value;
      }
    }

    internal double DoubleRight
    {
      get => this.doubleRight;
      set
      {
        this.Right = (int) Math.Round(value);
        this.doubleRight = value;
      }
    }

    internal double DoubleTop
    {
      get => this.doubleTop;
      set
      {
        this.Top = (int) Math.Round(value);
        this.doubleTop = value;
      }
    }

    internal double DoubleBottom
    {
      get => this.doubleBottom;
      set
      {
        this.Bottom = (int) Math.Round(value);
        this.doubleBottom = value;
      }
    }

    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
      if (this.doubleLeft == 0.0 && this.left != 0)
        this.doubleLeft = (double) this.left;
      if (this.doubleRight == 0.0 && this.right != 0)
        this.doubleRight = (double) this.right;
      if (this.doubleTop == 0.0 && this.top != 0)
        this.doubleTop = (double) this.top;
      if (this.doubleBottom != 0.0 || this.bottom == 0)
        return;
      this.doubleBottom = (double) this.bottom;
    }

    private void CheckMargin(int margin, string name)
    {
      if (margin < 0)
        throw new ArgumentException(System.Drawing.SR.GetString("InvalidLowBoundArgumentEx", (object) name, (object) margin, (object) "0"));
    }

    /// <summary>Retrieves a duplicate of this object, member by member.</summary>
    /// <returns>A duplicate of this object.</returns>
    public object Clone() => this.MemberwiseClone();

    /// <summary>Compares this <see cref="T:System.Drawing.Printing.Margins" /> to the specified <see cref="T:System.Object" /> to determine whether they have the same dimensions.</summary>
    /// <param name="obj">The object to which to compare this <see cref="T:System.Drawing.Printing.Margins" />.</param>
    /// <returns>
    /// <see langword="true" /> if the specified object is a <see cref="T:System.Drawing.Printing.Margins" /> and has the same <see cref="P:System.Drawing.Printing.Margins.Top" />, <see cref="P:System.Drawing.Printing.Margins.Bottom" />, <see cref="P:System.Drawing.Printing.Margins.Right" /> and <see cref="P:System.Drawing.Printing.Margins.Left" /> values as this <see cref="T:System.Drawing.Printing.Margins" />; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object obj)
    {
      Margins margins = obj as Margins;
      if (margins == this)
        return true;
      return !(margins == (Margins) null) && margins.Left == this.Left && margins.Right == this.Right && margins.Top == this.Top && margins.Bottom == this.Bottom;
    }

    /// <summary>Calculates and retrieves a hash code based on the width of the left, right, top, and bottom margins.</summary>
    /// <returns>A hash code based on the left, right, top, and bottom margins.</returns>
    public override int GetHashCode()
    {
      uint left = (uint) this.Left;
      uint right = (uint) this.Right;
      uint top = (uint) this.Top;
      uint bottom = (uint) this.Bottom;
      return (int) left ^ ((int) right << 13 | (int) (right >> 19)) ^ ((int) top << 26 | (int) (top >> 6)) ^ ((int) bottom << 7 | (int) (bottom >> 25));
    }

    /// <summary>Compares two <see cref="T:System.Drawing.Printing.Margins" /> to determine if they have the same dimensions.</summary>
    /// <param name="m1">The first <see cref="T:System.Drawing.Printing.Margins" /> to compare for equality.</param>
    /// <param name="m2">The second <see cref="T:System.Drawing.Printing.Margins" /> to compare for equality.</param>
    /// <returns>
    /// <see langword="true" /> to indicate the <see cref="P:System.Drawing.Printing.Margins.Left" />, <see cref="P:System.Drawing.Printing.Margins.Right" />, <see cref="P:System.Drawing.Printing.Margins.Top" />, and <see cref="P:System.Drawing.Printing.Margins.Bottom" /> properties of both margins have the same value; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(Margins m1, Margins m2)
    {
      if ((object) m1 == null != ((object) m2 == null))
        return false;
      if ((object) m1 == null)
        return true;
      return m1.Left == m2.Left && m1.Top == m2.Top && m1.Right == m2.Right && m1.Bottom == m2.Bottom;
    }

    /// <summary>Compares two <see cref="T:System.Drawing.Printing.Margins" /> to determine whether they are of unequal width.</summary>
    /// <param name="m1">The first <see cref="T:System.Drawing.Printing.Margins" /> to compare for inequality.</param>
    /// <param name="m2">The second <see cref="T:System.Drawing.Printing.Margins" /> to compare for inequality.</param>
    /// <returns>
    /// <see langword="true" /> to indicate if the <see cref="P:System.Drawing.Printing.Margins.Left" />, <see cref="P:System.Drawing.Printing.Margins.Right" />, <see cref="P:System.Drawing.Printing.Margins.Top" />, or <see cref="P:System.Drawing.Printing.Margins.Bottom" /> properties of both margins are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(Margins m1, Margins m2) => !(m1 == m2);

    /// <summary>Converts the <see cref="T:System.Drawing.Printing.Margins" /> to a string.</summary>
    /// <returns>A <see cref="T:System.String" /> representation of the <see cref="T:System.Drawing.Printing.Margins" />.</returns>
    public override string ToString() => "[Margins Left=" + this.Left.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " Right=" + this.Right.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " Top=" + this.Top.ToString((IFormatProvider) CultureInfo.InvariantCulture) + " Bottom=" + this.Bottom.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "]";
  }
}
