// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.EmfPlusRecordType
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the methods available for use with a metafile to read and write graphic commands.</summary>
  public enum EmfPlusRecordType
  {
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfHeader = 1,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfMin = 1,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyBezier = 2,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolygon = 3,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyline = 4,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyBezierTo = 5,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyLineTo = 6,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyPolyline = 7,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyPolygon = 8,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetWindowExtEx = 9,
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetWindowOrgEx = 10, // 0x0000000A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetViewportExtEx = 11, // 0x0000000B
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetViewportOrgEx = 12, // 0x0000000C
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetBrushOrgEx = 13, // 0x0000000D
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfEof = 14, // 0x0000000E
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetPixelV = 15, // 0x0000000F
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetMapperFlags = 16, // 0x00000010
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetMapMode = 17, // 0x00000011
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetBkMode = 18, // 0x00000012
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetPolyFillMode = 19, // 0x00000013
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetROP2 = 20, // 0x00000014
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetStretchBltMode = 21, // 0x00000015
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetTextAlign = 22, // 0x00000016
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetColorAdjustment = 23, // 0x00000017
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetTextColor = 24, // 0x00000018
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetBkColor = 25, // 0x00000019
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfOffsetClipRgn = 26, // 0x0000001A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfMoveToEx = 27, // 0x0000001B
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetMetaRgn = 28, // 0x0000001C
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfExcludeClipRect = 29, // 0x0000001D
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfIntersectClipRect = 30, // 0x0000001E
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfScaleViewportExtEx = 31, // 0x0000001F
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfScaleWindowExtEx = 32, // 0x00000020
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSaveDC = 33, // 0x00000021
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfRestoreDC = 34, // 0x00000022
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetWorldTransform = 35, // 0x00000023
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfModifyWorldTransform = 36, // 0x00000024
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSelectObject = 37, // 0x00000025
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfCreatePen = 38, // 0x00000026
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfCreateBrushIndirect = 39, // 0x00000027
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfDeleteObject = 40, // 0x00000028
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfAngleArc = 41, // 0x00000029
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfEllipse = 42, // 0x0000002A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfRectangle = 43, // 0x0000002B
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfRoundRect = 44, // 0x0000002C
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfRoundArc = 45, // 0x0000002D
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfChord = 46, // 0x0000002E
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPie = 47, // 0x0000002F
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSelectPalette = 48, // 0x00000030
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfCreatePalette = 49, // 0x00000031
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetPaletteEntries = 50, // 0x00000032
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfResizePalette = 51, // 0x00000033
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfRealizePalette = 52, // 0x00000034
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfExtFloodFill = 53, // 0x00000035
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfLineTo = 54, // 0x00000036
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfArcTo = 55, // 0x00000037
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyDraw = 56, // 0x00000038
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetArcDirection = 57, // 0x00000039
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetMiterLimit = 58, // 0x0000003A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfBeginPath = 59, // 0x0000003B
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfEndPath = 60, // 0x0000003C
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfCloseFigure = 61, // 0x0000003D
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfFillPath = 62, // 0x0000003E
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfStrokeAndFillPath = 63, // 0x0000003F
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfStrokePath = 64, // 0x00000040
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfFlattenPath = 65, // 0x00000041
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfWidenPath = 66, // 0x00000042
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSelectClipPath = 67, // 0x00000043
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfAbortPath = 68, // 0x00000044
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfReserved069 = 69, // 0x00000045
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfGdiComment = 70, // 0x00000046
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfFillRgn = 71, // 0x00000047
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfFrameRgn = 72, // 0x00000048
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfInvertRgn = 73, // 0x00000049
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPaintRgn = 74, // 0x0000004A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfExtSelectClipRgn = 75, // 0x0000004B
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfBitBlt = 76, // 0x0000004C
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfStretchBlt = 77, // 0x0000004D
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfMaskBlt = 78, // 0x0000004E
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPlgBlt = 79, // 0x0000004F
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetDIBitsToDevice = 80, // 0x00000050
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfStretchDIBits = 81, // 0x00000051
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfExtCreateFontIndirect = 82, // 0x00000052
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfExtTextOutA = 83, // 0x00000053
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfExtTextOutW = 84, // 0x00000054
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyBezier16 = 85, // 0x00000055
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolygon16 = 86, // 0x00000056
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyline16 = 87, // 0x00000057
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyBezierTo16 = 88, // 0x00000058
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolylineTo16 = 89, // 0x00000059
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyPolyline16 = 90, // 0x0000005A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyPolygon16 = 91, // 0x0000005B
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyDraw16 = 92, // 0x0000005C
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfCreateMonoBrush = 93, // 0x0000005D
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfCreateDibPatternBrushPt = 94, // 0x0000005E
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfExtCreatePen = 95, // 0x0000005F
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyTextOutA = 96, // 0x00000060
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPolyTextOutW = 97, // 0x00000061
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetIcmMode = 98, // 0x00000062
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfCreateColorSpace = 99, // 0x00000063
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetColorSpace = 100, // 0x00000064
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfDeleteColorSpace = 101, // 0x00000065
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfGlsRecord = 102, // 0x00000066
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfGlsBoundedRecord = 103, // 0x00000067
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPixelFormat = 104, // 0x00000068
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfDrawEscape = 105, // 0x00000069
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfExtEscape = 106, // 0x0000006A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfStartDoc = 107, // 0x0000006B
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSmallTextOut = 108, // 0x0000006C
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfForceUfiMapping = 109, // 0x0000006D
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfNamedEscpae = 110, // 0x0000006E
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfColorCorrectPalette = 111, // 0x0000006F
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetIcmProfileA = 112, // 0x00000070
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetIcmProfileW = 113, // 0x00000071
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfAlphaBlend = 114, // 0x00000072
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetLayout = 115, // 0x00000073
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfTransparentBlt = 116, // 0x00000074
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfReserved117 = 117, // 0x00000075
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfGradientFill = 118, // 0x00000076
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetLinkedUfis = 119, // 0x00000077
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfSetTextJustification = 120, // 0x00000078
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfColorMatchToTargetW = 121, // 0x00000079
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfCreateColorSpaceW = 122, // 0x0000007A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfMax = 122, // 0x0000007A
    /// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    EmfPlusRecordBase = 16384, // 0x00004000
    /// <summary>Indicates invalid data.</summary>
    Invalid = 16384, // 0x00004000
    /// <summary>Identifies a record that is the EMF+ header.</summary>
    Header = 16385, // 0x00004001
    /// <summary>The minimum value for this enumeration.</summary>
    Min = 16385, // 0x00004001
    /// <summary>Identifies a record that marks the last EMF+ record of a metafile.</summary>
    EndOfFile = 16386, // 0x00004002
    /// <summary>See <see cref="M:System.Drawing.Graphics.AddMetafileComment(System.Byte[])" />.</summary>
    Comment = 16387, // 0x00004003
    /// <summary>See <see cref="M:System.Drawing.Graphics.GetHdc" />.</summary>
    GetDC = 16388, // 0x00004004
    /// <summary>Marks the start of a multiple-format section.</summary>
    MultiFormatStart = 16389, // 0x00004005
    /// <summary>Marks a multiple-format section.</summary>
    MultiFormatSection = 16390, // 0x00004006
    /// <summary>Marks the end of a multiple-format section.</summary>
    MultiFormatEnd = 16391, // 0x00004007
    /// <summary>Marks an object.</summary>
    Object = 16392, // 0x00004008
    /// <summary>See <see cref="M:System.Drawing.Graphics.Clear(System.Drawing.Color)" />.</summary>
    Clear = 16393, // 0x00004009
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.FillRectangles" /> methods.</summary>
    FillRects = 16394, // 0x0000400A
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawRectangles" /> methods.</summary>
    DrawRects = 16395, // 0x0000400B
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.FillPolygon" /> methods.</summary>
    FillPolygon = 16396, // 0x0000400C
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawLines" /> methods.</summary>
    DrawLines = 16397, // 0x0000400D
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.FillEllipse" /> methods.</summary>
    FillEllipse = 16398, // 0x0000400E
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawEllipse" /> methods.</summary>
    DrawEllipse = 16399, // 0x0000400F
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.FillPie" /> methods.</summary>
    FillPie = 16400, // 0x00004010
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawPie" /> methods.</summary>
    DrawPie = 16401, // 0x00004011
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawArc" /> methods.</summary>
    DrawArc = 16402, // 0x00004012
    /// <summary>See <see cref="M:System.Drawing.Graphics.FillRegion(System.Drawing.Brush,System.Drawing.Region)" />.</summary>
    FillRegion = 16403, // 0x00004013
    /// <summary>See <see cref="M:System.Drawing.Graphics.FillPath(System.Drawing.Brush,System.Drawing.Drawing2D.GraphicsPath)" />.</summary>
    FillPath = 16404, // 0x00004014
    /// <summary>See <see cref="M:System.Drawing.Graphics.DrawPath(System.Drawing.Pen,System.Drawing.Drawing2D.GraphicsPath)" />.</summary>
    DrawPath = 16405, // 0x00004015
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.FillClosedCurve" /> methods.</summary>
    FillClosedCurve = 16406, // 0x00004016
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawClosedCurve" /> methods.</summary>
    DrawClosedCurve = 16407, // 0x00004017
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawCurve" /> methods.</summary>
    DrawCurve = 16408, // 0x00004018
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawBeziers" /> methods.</summary>
    DrawBeziers = 16409, // 0x00004019
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawImage" /> methods.</summary>
    DrawImage = 16410, // 0x0000401A
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawImage" /> methods.</summary>
    DrawImagePoints = 16411, // 0x0000401B
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawString" /> methods.</summary>
    DrawString = 16412, // 0x0000401C
    /// <summary>See <see cref="P:System.Drawing.Graphics.RenderingOrigin" />.</summary>
    SetRenderingOrigin = 16413, // 0x0000401D
    /// <summary>See <see cref="P:System.Drawing.Graphics.SmoothingMode" />.</summary>
    SetAntiAliasMode = 16414, // 0x0000401E
    /// <summary>See <see cref="P:System.Drawing.Graphics.TextRenderingHint" />.</summary>
    SetTextRenderingHint = 16415, // 0x0000401F
    /// <summary>See <see cref="P:System.Drawing.Graphics.TextContrast" />.</summary>
    SetTextContrast = 16416, // 0x00004020
    /// <summary>See <see cref="P:System.Drawing.Graphics.InterpolationMode" />.</summary>
    SetInterpolationMode = 16417, // 0x00004021
    /// <summary>See <see cref="P:System.Drawing.Graphics.PixelOffsetMode" />.</summary>
    SetPixelOffsetMode = 16418, // 0x00004022
    /// <summary>See <see cref="P:System.Drawing.Graphics.CompositingMode" />.</summary>
    SetCompositingMode = 16419, // 0x00004023
    /// <summary>See <see cref="P:System.Drawing.Graphics.CompositingQuality" />.</summary>
    SetCompositingQuality = 16420, // 0x00004024
    /// <summary>See <see cref="M:System.Drawing.Graphics.Save" />.</summary>
    Save = 16421, // 0x00004025
    /// <summary>See <see cref="M:System.Drawing.Graphics.Restore(System.Drawing.Drawing2D.GraphicsState)" />.</summary>
    Restore = 16422, // 0x00004026
    /// <summary>See <see cref="M:System.Drawing.Graphics.BeginContainer" /> methods.</summary>
    BeginContainer = 16423, // 0x00004027
    /// <summary>See <see cref="M:System.Drawing.Graphics.BeginContainer" /> methods.</summary>
    BeginContainerNoParams = 16424, // 0x00004028
    /// <summary>See <see cref="M:System.Drawing.Graphics.EndContainer(System.Drawing.Drawing2D.GraphicsContainer)" />.</summary>
    EndContainer = 16425, // 0x00004029
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
    SetWorldTransform = 16426, // 0x0000402A
    /// <summary>See <see cref="M:System.Drawing.Graphics.ResetTransform" />.</summary>
    ResetWorldTransform = 16427, // 0x0000402B
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.MultiplyTransform" /> methods.</summary>
    MultiplyWorldTransform = 16428, // 0x0000402C
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
    TranslateWorldTransform = 16429, // 0x0000402D
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.ScaleTransform" /> methods.</summary>
    ScaleWorldTransform = 16430, // 0x0000402E
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.RotateTransform" /> methods.</summary>
    RotateWorldTransform = 16431, // 0x0000402F
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
    SetPageTransform = 16432, // 0x00004030
    /// <summary>See <see cref="M:System.Drawing.Graphics.ResetClip" />.</summary>
    ResetClip = 16433, // 0x00004031
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
    SetClipRect = 16434, // 0x00004032
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
    SetClipPath = 16435, // 0x00004033
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
    SetClipRegion = 16436, // 0x00004034
    /// <summary>See <see cref="Overload:System.Drawing.Graphics.TranslateClip" /> methods.</summary>
    OffsetClip = 16437, // 0x00004035
    /// <summary>Specifies a character string, a location, and formatting information.</summary>
    DrawDriverString = 16438, // 0x00004036
    /// <summary>The maximum value for this enumeration.</summary>
    Max = 16438, // 0x00004036
    /// <summary>Used internally.</summary>
    Total = 16439, // 0x00004037
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfRecordBase = 65536, // 0x00010000
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSaveDC = 65566, // 0x0001001E
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfRealizePalette = 65589, // 0x00010035
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetPalEntries = 65591, // 0x00010037
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfCreatePalette = 65783, // 0x000100F7
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetBkMode = 65794, // 0x00010102
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetMapMode = 65795, // 0x00010103
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetROP2 = 65796, // 0x00010104
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetRelAbs = 65797, // 0x00010105
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetPolyFillMode = 65798, // 0x00010106
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetStretchBltMode = 65799, // 0x00010107
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetTextCharExtra = 65800, // 0x00010108
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfRestoreDC = 65831, // 0x00010127
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfInvertRegion = 65834, // 0x0001012A
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfPaintRegion = 65835, // 0x0001012B
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSelectClipRegion = 65836, // 0x0001012C
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSelectObject = 65837, // 0x0001012D
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetTextAlign = 65838, // 0x0001012E
    /// <summary>Increases or decreases the size of a logical palette based on the specified value.</summary>
    WmfResizePalette = 65849, // 0x00010139
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfDibCreatePatternBrush = 65858, // 0x00010142
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetLayout = 65865, // 0x00010149
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfDeleteObject = 66032, // 0x000101F0
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfCreatePatternBrush = 66041, // 0x000101F9
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetBkColor = 66049, // 0x00010201
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetTextColor = 66057, // 0x00010209
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetTextJustification = 66058, // 0x0001020A
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetWindowOrg = 66059, // 0x0001020B
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetWindowExt = 66060, // 0x0001020C
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetViewportOrg = 66061, // 0x0001020D
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetViewportExt = 66062, // 0x0001020E
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfOffsetWindowOrg = 66063, // 0x0001020F
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfOffsetViewportOrg = 66065, // 0x00010211
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfLineTo = 66067, // 0x00010213
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfMoveTo = 66068, // 0x00010214
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfOffsetCilpRgn = 66080, // 0x00010220
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfFillRegion = 66088, // 0x00010228
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetMapperFlags = 66097, // 0x00010231
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSelectPalette = 66100, // 0x00010234
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfCreatePenIndirect = 66298, // 0x000102FA
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfCreateFontIndirect = 66299, // 0x000102FB
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfCreateBrushIndirect = 66300, // 0x000102FC
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfPolygon = 66340, // 0x00010324
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfPolyline = 66341, // 0x00010325
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfScaleWindowExt = 66576, // 0x00010410
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfScaleViewportExt = 66578, // 0x00010412
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfExcludeClipRect = 66581, // 0x00010415
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfIntersectClipRect = 66582, // 0x00010416
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfEllipse = 66584, // 0x00010418
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfFloodFill = 66585, // 0x00010419
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfRectangle = 66587, // 0x0001041B
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetPixel = 66591, // 0x0001041F
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfFrameRegion = 66601, // 0x00010429
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfAnimatePalette = 66614, // 0x00010436
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfTextOut = 66849, // 0x00010521
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfPolyPolygon = 66872, // 0x00010538
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfExtFloodFill = 66888, // 0x00010548
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfRoundRect = 67100, // 0x0001061C
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfPatBlt = 67101, // 0x0001061D
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfEscape = 67110, // 0x00010626
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfCreateRegion = 67327, // 0x000106FF
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfArc = 67607, // 0x00010817
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfPie = 67610, // 0x0001081A
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfChord = 67632, // 0x00010830
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfBitBlt = 67874, // 0x00010922
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfDibBitBlt = 67904, // 0x00010940
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfExtTextOut = 68146, // 0x00010A32
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfStretchBlt = 68387, // 0x00010B23
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfDibStretchBlt = 68417, // 0x00010B41
    /// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
    WmfSetDibToDev = 68915, // 0x00010D33
    /// <summary>Copies the color data for a rectangle of pixels in a DIB to the specified destination rectangle.</summary>
    WmfStretchDib = 69443, // 0x00010F43
  }
}
