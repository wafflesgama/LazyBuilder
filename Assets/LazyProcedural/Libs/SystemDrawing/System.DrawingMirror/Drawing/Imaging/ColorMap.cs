// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ColorMap
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Defines a map for converting colors. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color-remap table, which is an array of <see cref="T:System.Drawing.Imaging.ColorMap" /> structures. Not inheritable.</summary>
  public sealed class ColorMap
  {
    private Color oldColor;
    private Color newColor;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMap" /> class.</summary>
    public ColorMap()
    {
      this.oldColor = new Color();
      this.newColor = new Color();
    }

    /// <summary>Gets or sets the existing <see cref="T:System.Drawing.Color" /> structure to be converted.</summary>
    /// <returns>The existing <see cref="T:System.Drawing.Color" /> structure to be converted.</returns>
    public Color OldColor
    {
      get => this.oldColor;
      set => this.oldColor = value;
    }

    /// <summary>Gets or sets the new <see cref="T:System.Drawing.Color" /> structure to which to convert.</summary>
    /// <returns>The new <see cref="T:System.Drawing.Color" /> structure to which to convert.</returns>
    public Color NewColor
    {
      get => this.newColor;
      set => this.newColor = value;
    }
  }
}
