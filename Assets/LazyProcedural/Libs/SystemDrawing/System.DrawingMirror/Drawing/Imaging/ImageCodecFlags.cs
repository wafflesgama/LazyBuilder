// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ImageCodecFlags
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Provides attributes of an image encoder/decoder (codec).</summary>
  [Flags]
  public enum ImageCodecFlags
  {
    /// <summary>The codec supports encoding (saving).</summary>
    Encoder = 1,
    /// <summary>The codec supports decoding (reading).</summary>
    Decoder = 2,
    /// <summary>The codec supports raster images (bitmaps).</summary>
    SupportBitmap = 4,
    /// <summary>The codec supports vector images (metafiles).</summary>
    SupportVector = 8,
    /// <summary>The encoder requires a seekable output stream.</summary>
    SeekableEncode = 16, // 0x00000010
    /// <summary>The decoder has blocking behavior during the decoding process.</summary>
    BlockingDecode = 32, // 0x00000020
    /// <summary>The codec is built into GDI+.</summary>
    Builtin = 65536, // 0x00010000
    /// <summary>Not used.</summary>
    System = 131072, // 0x00020000
    /// <summary>Not used.</summary>
    User = 262144, // 0x00040000
  }
}
