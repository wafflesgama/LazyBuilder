// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.PathData
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Contains the graphical data that makes up a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object. This class cannot be inherited.</summary>
  public sealed class PathData
  {
    private PointF[] points;
    private byte[] types;

    /// <summary>Gets or sets an array of <see cref="T:System.Drawing.PointF" /> structures that represents the points through which the path is constructed.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.PointF" /> objects that represents the points through which the path is constructed.</returns>
    public PointF[] Points
    {
      get => this.points;
      set => this.points = value;
    }

    /// <summary>Gets or sets the types of the corresponding points in the path.</summary>
    /// <returns>An array of bytes that specify the types of the corresponding points in the path.</returns>
    public byte[] Types
    {
      get => this.types;
      set => this.types = value;
    }
  }
}
