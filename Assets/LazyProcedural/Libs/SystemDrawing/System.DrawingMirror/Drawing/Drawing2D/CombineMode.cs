// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.CombineMode
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies how different clipping regions can be combined.</summary>
  public enum CombineMode
  {
    /// <summary>One clipping region is replaced by another.</summary>
    Replace,
    /// <summary>Two clipping regions are combined by taking their intersection.</summary>
    Intersect,
    /// <summary>Two clipping regions are combined by taking the union of both.</summary>
    Union,
    /// <summary>Two clipping regions are combined by taking only the areas enclosed by one or the other region, but not both.</summary>
    Xor,
    /// <summary>Specifies that the existing region is replaced by the result of the new region being removed from the existing region. Said differently, the new region is excluded from the existing region.</summary>
    Exclude,
    /// <summary>Specifies that the existing region is replaced by the result of the existing region being removed from the new region. Said differently, the existing region is excluded from the new region.</summary>
    Complement,
  }
}
