// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.LineCap
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies the available cap styles with which a <see cref="T:System.Drawing.Pen" /> object can end a line.</summary>
  public enum LineCap
  {
    /// <summary>Specifies a flat line cap.</summary>
    Flat = 0,
    /// <summary>Specifies a square line cap.</summary>
    Square = 1,
    /// <summary>Specifies a round line cap.</summary>
    Round = 2,
    /// <summary>Specifies a triangular line cap.</summary>
    Triangle = 3,
    /// <summary>Specifies no anchor.</summary>
    NoAnchor = 16, // 0x00000010
    /// <summary>Specifies a square anchor line cap.</summary>
    SquareAnchor = 17, // 0x00000011
    /// <summary>Specifies a round anchor cap.</summary>
    RoundAnchor = 18, // 0x00000012
    /// <summary>Specifies a diamond anchor cap.</summary>
    DiamondAnchor = 19, // 0x00000013
    /// <summary>Specifies an arrow-shaped anchor cap.</summary>
    ArrowAnchor = 20, // 0x00000014
    /// <summary>Specifies a mask used to check whether a line cap is an anchor cap.</summary>
    AnchorMask = 240, // 0x000000F0
    /// <summary>Specifies a custom line cap.</summary>
    Custom = 255, // 0x000000FF
  }
}
