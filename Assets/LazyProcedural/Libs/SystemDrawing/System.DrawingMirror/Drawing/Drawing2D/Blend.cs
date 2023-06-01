// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.Blend
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Defines a blend pattern for a <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> object. This class cannot be inherited.</summary>
  public sealed class Blend
  {
    private float[] factors;
    private float[] positions;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Blend" /> class.</summary>
    public Blend()
    {
      this.factors = new float[1];
      this.positions = new float[1];
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Blend" /> class with the specified number of factors and positions.</summary>
    /// <param name="count">The number of elements in the <see cref="P:System.Drawing.Drawing2D.Blend.Factors" /> and <see cref="P:System.Drawing.Drawing2D.Blend.Positions" /> arrays.</param>
    public Blend(int count)
    {
      this.factors = new float[count];
      this.positions = new float[count];
    }

    /// <summary>Gets or sets an array of blend factors for the gradient.</summary>
    /// <returns>An array of blend factors that specify the percentages of the starting color and the ending color to be used at the corresponding position.</returns>
    public float[] Factors
    {
      get => this.factors;
      set => this.factors = value;
    }

    /// <summary>Gets or sets an array of blend positions for the gradient.</summary>
    /// <returns>An array of blend positions that specify the percentages of distance along the gradient line.</returns>
    public float[] Positions
    {
      get => this.positions;
      set => this.positions = value;
    }
  }
}
