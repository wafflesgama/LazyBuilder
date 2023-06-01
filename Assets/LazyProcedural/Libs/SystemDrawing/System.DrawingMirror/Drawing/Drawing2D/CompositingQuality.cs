// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.CompositingQuality
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies the quality level to use during compositing.</summary>
  public enum CompositingQuality
  {
    /// <summary>Invalid quality.</summary>
    Invalid = -1, // 0xFFFFFFFF
    /// <summary>Default quality.</summary>
    Default = 0,
    /// <summary>High speed, low quality.</summary>
    HighSpeed = 1,
    /// <summary>High quality, low speed compositing.</summary>
    HighQuality = 2,
    /// <summary>Gamma correction is used.</summary>
    GammaCorrected = 3,
    /// <summary>Assume linear values.</summary>
    AssumeLinear = 4,
  }
}
