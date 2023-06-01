// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ImageFormat
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;

namespace System.Drawing.Imaging
{
  /// <summary>Specifies the file format of the image. Not inheritable.</summary>
  [TypeConverter(typeof (ImageFormatConverter))]
  public sealed class ImageFormat
  {
    private static ImageFormat memoryBMP = new ImageFormat(new Guid("{b96b3caa-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat bmp = new ImageFormat(new Guid("{b96b3cab-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat emf = new ImageFormat(new Guid("{b96b3cac-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat wmf = new ImageFormat(new Guid("{b96b3cad-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat jpeg = new ImageFormat(new Guid("{b96b3cae-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat png = new ImageFormat(new Guid("{b96b3caf-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat gif = new ImageFormat(new Guid("{b96b3cb0-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat tiff = new ImageFormat(new Guid("{b96b3cb1-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat exif = new ImageFormat(new Guid("{b96b3cb2-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat photoCD = new ImageFormat(new Guid("{b96b3cb3-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat flashPIX = new ImageFormat(new Guid("{b96b3cb4-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat icon = new ImageFormat(new Guid("{b96b3cb5-0728-11d3-9d7b-0000f81ef32e}"));
    private Guid guid;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ImageFormat" /> class by using the specified <see cref="T:System.Guid" /> structure.</summary>
    /// <param name="guid">The <see cref="T:System.Guid" /> structure that specifies a particular image format.</param>
    public ImageFormat(Guid guid) => this.guid = guid;

    /// <summary>Gets a <see cref="T:System.Guid" /> structure that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</summary>
    /// <returns>A <see cref="T:System.Guid" /> structure that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</returns>
    public Guid Guid => this.guid;

    /// <summary>Gets the format of a bitmap in memory.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the format of a bitmap in memory.</returns>
    public static ImageFormat MemoryBmp => ImageFormat.memoryBMP;

    /// <summary>Gets the bitmap (BMP) image format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the bitmap image format.</returns>
    public static ImageFormat Bmp => ImageFormat.bmp;

    /// <summary>Gets the enhanced metafile (EMF) image format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the enhanced metafile image format.</returns>
    public static ImageFormat Emf => ImageFormat.emf;

    /// <summary>Gets the Windows metafile (WMF) image format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Windows metafile image format.</returns>
    public static ImageFormat Wmf => ImageFormat.wmf;

    /// <summary>Gets the Graphics Interchange Format (GIF) image format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the GIF image format.</returns>
    public static ImageFormat Gif => ImageFormat.gif;

    /// <summary>Gets the Joint Photographic Experts Group (JPEG) image format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the JPEG image format.</returns>
    public static ImageFormat Jpeg => ImageFormat.jpeg;

    /// <summary>Gets the W3C Portable Network Graphics (PNG) image format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the PNG image format.</returns>
    public static ImageFormat Png => ImageFormat.png;

    /// <summary>Gets the Tagged Image File Format (TIFF) image format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the TIFF image format.</returns>
    public static ImageFormat Tiff => ImageFormat.tiff;

    /// <summary>Gets the Exchangeable Image File (Exif) format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Exif format.</returns>
    public static ImageFormat Exif => ImageFormat.exif;

    /// <summary>Gets the Windows icon image format.</summary>
    /// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Windows icon image format.</returns>
    public static ImageFormat Icon => ImageFormat.icon;

    /// <summary>Returns a value that indicates whether the specified object is an <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that is equivalent to this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</summary>
    /// <param name="o">The object to test.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="o" /> is an <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that is equivalent to this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object o) => o is ImageFormat imageFormat && this.guid == imageFormat.guid;

    /// <summary>Returns a hash code value that represents this object.</summary>
    /// <returns>A hash code that represents this object.</returns>
    public override int GetHashCode() => this.guid.GetHashCode();

    internal ImageCodecInfo FindEncoder()
    {
      foreach (ImageCodecInfo imageEncoder in ImageCodecInfo.GetImageEncoders())
      {
        if (imageEncoder.FormatID.Equals(this.guid))
          return imageEncoder;
      }
      return (ImageCodecInfo) null;
    }

    /// <summary>Converts this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object to a human-readable string.</summary>
    /// <returns>A string that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</returns>
    public override string ToString()
    {
      if (this == ImageFormat.memoryBMP)
        return "MemoryBMP";
      if (this == ImageFormat.bmp)
        return "Bmp";
      if (this == ImageFormat.emf)
        return "Emf";
      if (this == ImageFormat.wmf)
        return "Wmf";
      if (this == ImageFormat.gif)
        return "Gif";
      if (this == ImageFormat.jpeg)
        return "Jpeg";
      if (this == ImageFormat.png)
        return "Png";
      if (this == ImageFormat.tiff)
        return "Tiff";
      if (this == ImageFormat.exif)
        return "Exif";
      return this == ImageFormat.icon ? "Icon" : "[ImageFormat: " + this.guid.ToString() + "]";
    }
  }
}
