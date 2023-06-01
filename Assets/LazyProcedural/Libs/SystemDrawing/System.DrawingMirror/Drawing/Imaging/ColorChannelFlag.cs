// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ColorChannelFlag
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies individual channels in the CMYK (cyan, magenta, yellow, black) color space. This enumeration is used by the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetOutputChannel" /> methods.</summary>
  public enum ColorChannelFlag
  {
    /// <summary>The cyan color channel.</summary>
    ColorChannelC,
    /// <summary>The magenta color channel.</summary>
    ColorChannelM,
    /// <summary>The yellow color channel.</summary>
    ColorChannelY,
    /// <summary>The black color channel.</summary>
    ColorChannelK,
    /// <summary>The last selected channel should be used.</summary>
    ColorChannelLast,
  }
}
