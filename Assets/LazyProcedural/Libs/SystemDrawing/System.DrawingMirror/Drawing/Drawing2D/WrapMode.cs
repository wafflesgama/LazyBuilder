// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.WrapMode
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies how a texture or gradient is tiled when it is smaller than the area being filled.</summary>
  public enum WrapMode
  {
    /// <summary>Tiles the gradient or texture.</summary>
    Tile,
    /// <summary>Reverses the texture or gradient horizontally and then tiles the texture or gradient.</summary>
    TileFlipX,
    /// <summary>Reverses the texture or gradient vertically and then tiles the texture or gradient.</summary>
    TileFlipY,
    /// <summary>Reverses the texture or gradient horizontally and vertically and then tiles the texture or gradient.</summary>
    TileFlipXY,
    /// <summary>The texture or gradient is not tiled.</summary>
    Clamp,
  }
}
