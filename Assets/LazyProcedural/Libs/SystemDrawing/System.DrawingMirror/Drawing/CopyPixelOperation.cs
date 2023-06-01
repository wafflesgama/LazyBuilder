// Decompiled with JetBrains decompiler
// Type: System.Drawing.CopyPixelOperation
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing
{
  /// <summary>Determines how the source color in a copy pixel operation is combined with the destination color to result in a final color.</summary>
  [ComVisible(true)]
  public enum CopyPixelOperation
  {
    /// <summary>The bitmap is not mirrored.</summary>
    NoMirrorBitmap = -2147483648, // 0x80000000
    /// <summary>The destination area is filled by using the color associated with index 0 in the physical palette. (This color is black for the default physical palette.)</summary>
    Blackness = 66, // 0x00000042
    /// <summary>The source and destination colors are combined using the Boolean <see langword="OR" /> operator, and then resultant color is then inverted.</summary>
    NotSourceErase = 1114278, // 0x001100A6
    /// <summary>The inverted source area is copied to the destination.</summary>
    NotSourceCopy = 3342344, // 0x00330008
    /// <summary>The inverted colors of the destination area are combined with the colors of the source area using the Boolean <see langword="AND" /> operator.</summary>
    SourceErase = 4457256, // 0x00440328
    /// <summary>The destination area is inverted.</summary>
    DestinationInvert = 5570569, // 0x00550009
    /// <summary>The colors of the brush currently selected in the destination device context are combined with the colors of the destination are using the Boolean <see langword="XOR" /> operator.</summary>
    PatInvert = 5898313, // 0x005A0049
    /// <summary>The colors of the source and destination areas are combined using the Boolean <see langword="XOR" /> operator.</summary>
    SourceInvert = 6684742, // 0x00660046
    /// <summary>The colors of the source and destination areas are combined using the Boolean <see langword="AND" /> operator.</summary>
    SourceAnd = 8913094, // 0x008800C6
    /// <summary>The colors of the inverted source area are merged with the colors of the destination area by using the Boolean <see langword="OR" /> operator.</summary>
    MergePaint = 12255782, // 0x00BB0226
    /// <summary>The colors of the source area are merged with the colors of the selected brush of the destination device context using the Boolean <see langword="AND" /> operator.</summary>
    MergeCopy = 12583114, // 0x00C000CA
    /// <summary>The source area is copied directly to the destination area.</summary>
    SourceCopy = 13369376, // 0x00CC0020
    /// <summary>The colors of the source and destination areas are combined using the Boolean <see langword="OR" /> operator.</summary>
    SourcePaint = 15597702, // 0x00EE0086
    /// <summary>The brush currently selected in the destination device context is copied to the destination bitmap.</summary>
    PatCopy = 15728673, // 0x00F00021
    /// <summary>The colors of the brush currently selected in the destination device context are combined with the colors of the inverted source area using the Boolean <see langword="OR" /> operator. The result of this operation is combined with the colors of the destination area using the Boolean <see langword="OR" /> operator.</summary>
    PatPaint = 16452105, // 0x00FB0A09
    /// <summary>The destination area is filled by using the color associated with index 1 in the physical palette. (This color is white for the default physical palette.)</summary>
    Whiteness = 16711778, // 0x00FF0062
    /// <summary>Windows that are layered on top of your window are included in the resulting image. By default, the image contains only your window. Note that this generally cannot be used for printing device contexts.</summary>
    CaptureBlt = 1073741824, // 0x40000000
  }
}
