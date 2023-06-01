// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.QualityMode
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies the overall quality when rendering GDI+ objects.</summary>
  public enum QualityMode
  {
    /// <summary>Specifies an invalid mode.</summary>
    Invalid = -1, // 0xFFFFFFFF
    /// <summary>Specifies the default mode.</summary>
    Default = 0,
    /// <summary>Specifies low quality, high speed rendering.</summary>
    Low = 1,
    /// <summary>Specifies high quality, low speed rendering.</summary>
    High = 2,
  }
}
