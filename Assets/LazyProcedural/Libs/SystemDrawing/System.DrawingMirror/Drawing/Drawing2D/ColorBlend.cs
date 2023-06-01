// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.ColorBlend
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Defines arrays of colors and positions used for interpolating color blending in a multicolor gradient. This class cannot be inherited.</summary>
  public sealed class ColorBlend
  {
    private Color[] colors;
    private float[] positions;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> class.</summary>
    public ColorBlend()
    {
      this.colors = new Color[1];
      this.positions = new float[1];
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> class with the specified number of colors and positions.</summary>
    /// <param name="count">The number of colors and positions in this <see cref="T:System.Drawing.Drawing2D.ColorBlend" />.</param>
    public ColorBlend(int count)
    {
      this.colors = new Color[count];
      this.positions = new float[count];
    }

    /// <summary>Gets or sets an array of colors that represents the colors to use at corresponding positions along a gradient.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.Color" /> structures that represents the colors to use at corresponding positions along a gradient.</returns>
    public Color[] Colors
    {
      get => this.colors;
      set => this.colors = value;
    }

    /// <summary>Gets or sets the positions along a gradient line.</summary>
    /// <returns>An array of values that specify percentages of distance along the gradient line.</returns>
    public float[] Positions
    {
      get => this.positions;
      set => this.positions = value;
    }
  }
}
