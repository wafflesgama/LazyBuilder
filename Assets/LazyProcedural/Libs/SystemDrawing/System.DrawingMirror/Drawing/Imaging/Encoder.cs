// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.Encoder
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>An <see cref="T:System.Drawing.Imaging.Encoder" /> object encapsulates a globally unique identifier (GUID) that identifies the category of an image encoder parameter.</summary>
  public sealed class Encoder
  {
    /// <summary>An <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the compression parameter category.</summary>
    public static readonly Encoder Compression = new Encoder(new Guid(-526552163, (short) -13100, (short) 17646, new byte[8]
    {
      (byte) 142,
      (byte) 186,
      (byte) 63,
      (byte) 191,
      (byte) 139,
      (byte) 228,
      (byte) 252,
      (byte) 88
    }));
    /// <summary>An <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the color depth parameter category.</summary>
    public static readonly Encoder ColorDepth = new Encoder(new Guid(1711829077, (short) -21146, (short) 19580, new byte[8]
    {
      (byte) 154,
      (byte) 24,
      (byte) 56,
      (byte) 162,
      (byte) 49,
      (byte) 11,
      (byte) 131,
      (byte) 55
    }));
    /// <summary>Represents an <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the scan method parameter category.</summary>
    public static readonly Encoder ScanMethod = new Encoder(new Guid(978200161, (short) 12553, (short) 20054, new byte[8]
    {
      (byte) 133,
      (byte) 54,
      (byte) 66,
      (byte) 193,
      (byte) 86,
      (byte) 231,
      (byte) 220,
      (byte) 250
    }));
    /// <summary>Represents an <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the version parameter category.</summary>
    public static readonly Encoder Version = new Encoder(new Guid(617712758, (short) -32438, (short) 16804, new byte[8]
    {
      (byte) 191,
      (byte) 83,
      (byte) 28,
      (byte) 33,
      (byte) 156,
      (byte) 204,
      (byte) 247,
      (byte) 151
    }));
    /// <summary>Represents an <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the render method parameter category.</summary>
    public static readonly Encoder RenderMethod = new Encoder(new Guid(1833092410, (short) 8858, (short) 18469, new byte[8]
    {
      (byte) 139,
      (byte) 183,
      (byte) 92,
      (byte) 153,
      (byte) 226,
      (byte) 185,
      (byte) 168,
      (byte) 184
    }));
    /// <summary>Gets an <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the quality parameter category.</summary>
    public static readonly Encoder Quality = new Encoder(new Guid(492561589, (short) -1462, (short) 17709, new byte[8]
    {
      (byte) 156,
      (byte) 221,
      (byte) 93,
      (byte) 179,
      (byte) 81,
      (byte) 5,
      (byte) 231,
      (byte) 235
    }));
    /// <summary>Represents an <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the transformation parameter category.</summary>
    public static readonly Encoder Transformation = new Encoder(new Guid(-1928416559, (short) -23154, (short) 20136, new byte[8]
    {
      (byte) 170,
      (byte) 20,
      (byte) 16,
      (byte) 128,
      (byte) 116,
      (byte) 183,
      (byte) 182,
      (byte) 249
    }));
    /// <summary>Represents an <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the luminance table parameter category.</summary>
    public static readonly Encoder LuminanceTable = new Encoder(new Guid(-307020850, (short) 614, (short) 19063, new byte[8]
    {
      (byte) 185,
      (byte) 4,
      (byte) 39,
      (byte) 33,
      (byte) 96,
      (byte) 153,
      (byte) 231,
      (byte) 23
    }));
    /// <summary>An <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the chrominance table parameter category.</summary>
    public static readonly Encoder ChrominanceTable = new Encoder(new Guid(-219916836, (short) 2483, (short) 17174, new byte[8]
    {
      (byte) 130,
      (byte) 96,
      (byte) 103,
      (byte) 106,
      (byte) 218,
      (byte) 50,
      (byte) 72,
      (byte) 28
    }));
    /// <summary>Represents an <see cref="T:System.Drawing.Imaging.Encoder" /> object that is initialized with the globally unique identifier for the save flag parameter category.</summary>
    public static readonly Encoder SaveFlag = new Encoder(new Guid(690120444, (short) -21440, (short) 18367, new byte[8]
    {
      (byte) 140,
      (byte) 252,
      (byte) 168,
      (byte) 91,
      (byte) 137,
      (byte) 166,
      (byte) 85,
      (byte) 222
    }));
    private Guid guid;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Encoder" /> class from the specified globally unique identifier (GUID). The GUID specifies an image encoder parameter category.</summary>
    /// <param name="guid">A globally unique identifier that identifies an image encoder parameter category.</param>
    public Encoder(Guid guid) => this.guid = guid;

    /// <summary>Gets a globally unique identifier (GUID) that identifies an image encoder parameter category.</summary>
    /// <returns>The GUID that identifies an image encoder parameter category.</returns>
    public Guid Guid => this.guid;
  }
}
