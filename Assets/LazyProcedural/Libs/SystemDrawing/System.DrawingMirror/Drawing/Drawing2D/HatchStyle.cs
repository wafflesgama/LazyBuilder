// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.HatchStyle
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies the different patterns available for <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> objects.</summary>
  public enum HatchStyle
  {
    /// <summary>A pattern of horizontal lines.</summary>
    Horizontal = 0,
    /// <summary>Specifies hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
    Min = 0,
    /// <summary>A pattern of vertical lines.</summary>
    Vertical = 1,
    /// <summary>A pattern of lines on a diagonal from upper left to lower right.</summary>
    ForwardDiagonal = 2,
    /// <summary>A pattern of lines on a diagonal from upper right to lower left.</summary>
    BackwardDiagonal = 3,
    /// <summary>Specifies horizontal and vertical lines that cross.</summary>
    Cross = 4,
    /// <summary>Specifies the hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
    LargeGrid = 4,
    /// <summary>Specifies hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.SolidDiamond" />.</summary>
    Max = 4,
    /// <summary>A pattern of crisscross diagonal lines.</summary>
    DiagonalCross = 5,
    /// <summary>Specifies a 5-percent hatch. The ratio of foreground color to background color is 5:95.</summary>
    Percent05 = 6,
    /// <summary>Specifies a 10-percent hatch. The ratio of foreground color to background color is 10:90.</summary>
    Percent10 = 7,
    /// <summary>Specifies a 20-percent hatch. The ratio of foreground color to background color is 20:80.</summary>
    Percent20 = 8,
    /// <summary>Specifies a 25-percent hatch. The ratio of foreground color to background color is 25:75.</summary>
    Percent25 = 9,
    /// <summary>Specifies a 30-percent hatch. The ratio of foreground color to background color is 30:70.</summary>
    Percent30 = 10, // 0x0000000A
    /// <summary>Specifies a 40-percent hatch. The ratio of foreground color to background color is 40:60.</summary>
    Percent40 = 11, // 0x0000000B
    /// <summary>Specifies a 50-percent hatch. The ratio of foreground color to background color is 50:50.</summary>
    Percent50 = 12, // 0x0000000C
    /// <summary>Specifies a 60-percent hatch. The ratio of foreground color to background color is 60:40.</summary>
    Percent60 = 13, // 0x0000000D
    /// <summary>Specifies a 70-percent hatch. The ratio of foreground color to background color is 70:30.</summary>
    Percent70 = 14, // 0x0000000E
    /// <summary>Specifies a 75-percent hatch. The ratio of foreground color to background color is 75:25.</summary>
    Percent75 = 15, // 0x0000000F
    /// <summary>Specifies a 80-percent hatch. The ratio of foreground color to background color is 80:100.</summary>
    Percent80 = 16, // 0x00000010
    /// <summary>Specifies a 90-percent hatch. The ratio of foreground color to background color is 90:10.</summary>
    Percent90 = 17, // 0x00000011
    /// <summary>Specifies diagonal lines that slant to the right from top points to bottom points and are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, but are not antialiased.</summary>
    LightDownwardDiagonal = 18, // 0x00000012
    /// <summary>Specifies diagonal lines that slant to the left from top points to bottom points and are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, but they are not antialiased.</summary>
    LightUpwardDiagonal = 19, // 0x00000013
    /// <summary>Specifies diagonal lines that slant to the right from top points to bottom points, are spaced 50 percent closer together than, and are twice the width of <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />. This hatch pattern is not antialiased.</summary>
    DarkDownwardDiagonal = 20, // 0x00000014
    /// <summary>Specifies diagonal lines that slant to the left from top points to bottom points, are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are twice its width, but the lines are not antialiased.</summary>
    DarkUpwardDiagonal = 21, // 0x00000015
    /// <summary>Specifies diagonal lines that slant to the right from top points to bottom points, have the same spacing as hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, and are triple its width, but are not antialiased.</summary>
    WideDownwardDiagonal = 22, // 0x00000016
    /// <summary>Specifies diagonal lines that slant to the left from top points to bottom points, have the same spacing as hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are triple its width, but are not antialiased.</summary>
    WideUpwardDiagonal = 23, // 0x00000017
    /// <summary>Specifies vertical lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" />.</summary>
    LightVertical = 24, // 0x00000018
    /// <summary>Specifies horizontal lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
    LightHorizontal = 25, // 0x00000019
    /// <summary>Specifies vertical lines that are spaced 75 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" /> (or 25 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.LightVertical" />).</summary>
    NarrowVertical = 26, // 0x0000001A
    /// <summary>Specifies horizontal lines that are spaced 75 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" /> (or 25 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.LightHorizontal" />).</summary>
    NarrowHorizontal = 27, // 0x0000001B
    /// <summary>Specifies vertical lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" /> and are twice its width.</summary>
    DarkVertical = 28, // 0x0000001C
    /// <summary>Specifies horizontal lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" /> and are twice the width of <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
    DarkHorizontal = 29, // 0x0000001D
    /// <summary>Specifies dashed diagonal lines, that slant to the right from top points to bottom points.</summary>
    DashedDownwardDiagonal = 30, // 0x0000001E
    /// <summary>Specifies dashed diagonal lines, that slant to the left from top points to bottom points.</summary>
    DashedUpwardDiagonal = 31, // 0x0000001F
    /// <summary>Specifies dashed horizontal lines.</summary>
    DashedHorizontal = 32, // 0x00000020
    /// <summary>Specifies dashed vertical lines.</summary>
    DashedVertical = 33, // 0x00000021
    /// <summary>Specifies a hatch that has the appearance of confetti.</summary>
    SmallConfetti = 34, // 0x00000022
    /// <summary>Specifies a hatch that has the appearance of confetti, and is composed of larger pieces than <see cref="F:System.Drawing.Drawing2D.HatchStyle.SmallConfetti" />.</summary>
    LargeConfetti = 35, // 0x00000023
    /// <summary>Specifies horizontal lines that are composed of zigzags.</summary>
    ZigZag = 36, // 0x00000024
    /// <summary>Specifies horizontal lines that are composed of tildes.</summary>
    Wave = 37, // 0x00000025
    /// <summary>Specifies a hatch that has the appearance of layered bricks that slant to the left from top points to bottom points.</summary>
    DiagonalBrick = 38, // 0x00000026
    /// <summary>Specifies a hatch that has the appearance of horizontally layered bricks.</summary>
    HorizontalBrick = 39, // 0x00000027
    /// <summary>Specifies a hatch that has the appearance of a woven material.</summary>
    Weave = 40, // 0x00000028
    /// <summary>Specifies a hatch that has the appearance of a plaid material.</summary>
    Plaid = 41, // 0x00000029
    /// <summary>Specifies a hatch that has the appearance of divots.</summary>
    Divot = 42, // 0x0000002A
    /// <summary>Specifies horizontal and vertical lines, each of which is composed of dots, that cross.</summary>
    DottedGrid = 43, // 0x0000002B
    /// <summary>Specifies forward diagonal and backward diagonal lines, each of which is composed of dots, that cross.</summary>
    DottedDiamond = 44, // 0x0000002C
    /// <summary>Specifies a hatch that has the appearance of diagonally layered shingles that slant to the right from top points to bottom points.</summary>
    Shingle = 45, // 0x0000002D
    /// <summary>Specifies a hatch that has the appearance of a trellis.</summary>
    Trellis = 46, // 0x0000002E
    /// <summary>Specifies a hatch that has the appearance of spheres laid adjacent to one another.</summary>
    Sphere = 47, // 0x0000002F
    /// <summary>Specifies horizontal and vertical lines that cross and are spaced 50 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
    SmallGrid = 48, // 0x00000030
    /// <summary>Specifies a hatch that has the appearance of a checkerboard.</summary>
    SmallCheckerBoard = 49, // 0x00000031
    /// <summary>Specifies a hatch that has the appearance of a checkerboard with squares that are twice the size of <see cref="F:System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard" />.</summary>
    LargeCheckerBoard = 50, // 0x00000032
    /// <summary>Specifies forward diagonal and backward diagonal lines that cross but are not antialiased.</summary>
    OutlinedDiamond = 51, // 0x00000033
    /// <summary>Specifies a hatch that has the appearance of a checkerboard placed diagonally.</summary>
    SolidDiamond = 52, // 0x00000034
  }
}
