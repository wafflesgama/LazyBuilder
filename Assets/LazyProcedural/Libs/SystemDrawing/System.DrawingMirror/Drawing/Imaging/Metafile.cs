// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.Metafile
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Internal;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing.Imaging
{
  /// <summary>Defines a graphic metafile. A metafile contains records that describe a sequence of graphics operations that can be recorded (constructed) and played back (displayed). This class is not inheritable.</summary>
  [Editor("System.Drawing.Design.MetafileEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [Serializable]
  public sealed class Metafile : Image
  {
    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle and a <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</summary>
    /// <param name="hmetafile">A windows handle to a <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</param>
    public Metafile(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader)
      : this(hmetafile, wmfHeader, false)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle and a <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />. Also, the <paramref name="deleteWmf" /> parameter can be used to delete the handle when the metafile is deleted.</summary>
    /// <param name="hmetafile">A windows handle to a <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</param>
    /// <param name="deleteWmf">
    /// <see langword="true" /> to delete the handle to the new <see cref="T:System.Drawing.Imaging.Metafile" /> when the <see cref="T:System.Drawing.Imaging.Metafile" /> is deleted; otherwise, <see langword="false" />.</param>
    public Metafile(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader, bool deleteWmf)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      int metafileFromWmf = SafeNativeMethods.Gdip.GdipCreateMetafileFromWmf(new HandleRef((object) null, hmetafile), deleteWmf, wmfHeader, out metafile);
      if (metafileFromWmf != 0)
        throw SafeNativeMethods.Gdip.StatusException(metafileFromWmf);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle.</summary>
    /// <param name="henhmetafile">A handle to an enhanced metafile.</param>
    /// <param name="deleteEmf">
    /// <see langword="true" /> to delete the enhanced metafile handle when the <see cref="T:System.Drawing.Imaging.Metafile" /> is deleted; otherwise, <see langword="false" />.</param>
    public Metafile(IntPtr henhmetafile, bool deleteEmf)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      int metafileFromEmf = SafeNativeMethods.Gdip.GdipCreateMetafileFromEmf(new HandleRef((object) null, henhmetafile), deleteEmf, out metafile);
      if (metafileFromEmf != 0)
        throw SafeNativeMethods.Gdip.StatusException(metafileFromEmf);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified file name.</summary>
    /// <param name="filename">A <see cref="T:System.String" /> that represents the file name from which to create the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(string filename)
    {
      System.Drawing.IntSecurity.DemandReadFileIO(filename);
      IntPtr metafile = IntPtr.Zero;
      int metafileFromFile = SafeNativeMethods.Gdip.GdipCreateMetafileFromFile(filename, out metafile);
      if (metafileFromFile != 0)
        throw SafeNativeMethods.Gdip.StatusException(metafileFromFile);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream.</summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to create the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> is <see langword="null" />.</exception>
    public Metafile(Stream stream)
    {
      if (stream == null)
        throw new ArgumentException(System.Drawing.SR.GetString("InvalidArgument", (object) nameof (stream), (object) "null"));
      IntPtr metafile = IntPtr.Zero;
      int metafileFromStream = SafeNativeMethods.Gdip.GdipCreateMetafileFromStream((UnsafeNativeMethods.IStream) new GPStream(stream), out metafile);
      if (metafileFromStream != 0)
        throw SafeNativeMethods.Gdip.StatusException(metafileFromStream);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle to a device context and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="emfType">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(IntPtr referenceHdc, EmfType emfType)
      : this(referenceHdc, emfType, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle to a device context and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be supplied to name the file.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="emfType">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="description">A descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(IntPtr referenceHdc, EmfType emfType, string description)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipRecordMetafile(new HandleRef((object) null, referenceHdc), (int) emfType, System.Drawing.NativeMethods.NullHandleRef, 7, description, out metafile);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(IntPtr referenceHdc, RectangleF frameRect)
      : this(referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit)
      : this(referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type)
      : this(referenceHdc, frameRect, frameUnit, type, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be provided to name the file.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type,
      string description)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      GPRECTF frameRect1 = new GPRECTF(frameRect);
      int status = SafeNativeMethods.Gdip.GdipRecordMetafile(new HandleRef((object) null, referenceHdc), (int) type, ref frameRect1, (int) frameUnit, description, out metafile);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(IntPtr referenceHdc, Rectangle frameRect)
      : this(referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit)
      : this(referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type)
      : this(referenceHdc, frameRect, frameUnit, type, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be provided to name the file.</summary>
    /// <param name="referenceHdc">The handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="desc">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type,
      string desc)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      int status;
      if (frameRect.IsEmpty)
      {
        status = SafeNativeMethods.Gdip.GdipRecordMetafile(new HandleRef((object) null, referenceHdc), (int) type, System.Drawing.NativeMethods.NullHandleRef, 7, desc, out metafile);
      }
      else
      {
        GPRECT frameRect1 = new GPRECT(frameRect);
        status = SafeNativeMethods.Gdip.GdipRecordMetafileI(new HandleRef((object) null, referenceHdc), (int) type, ref frameRect1, (int) frameUnit, desc, out metafile);
      }
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    public Metafile(string fileName, IntPtr referenceHdc)
      : this(fileName, referenceHdc, EmfType.EmfPlusDual, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(string fileName, IntPtr referenceHdc, EmfType type)
      : this(fileName, referenceHdc, type, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can be added, as well.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(string fileName, IntPtr referenceHdc, EmfType type, string description)
    {
      System.Drawing.IntSecurity.DemandReadFileIO(fileName);
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipRecordMetafileFileName(fileName, new HandleRef((object) null, referenceHdc), (int) type, System.Drawing.NativeMethods.NullHandleRef, 7, description, out metafile);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect)
      : this(fileName, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    public Metafile(
      string fileName,
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit)
      : this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      string fileName,
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type)
      : this(fileName, referenceHdc, frameRect, frameUnit, type, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure. A descriptive string can also be added.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="desc">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      string fileName,
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit,
      string desc)
      : this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, desc)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can also be added.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      string fileName,
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type,
      string description)
    {
      System.Drawing.IntSecurity.DemandReadFileIO(fileName);
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      GPRECTF frameRect1 = new GPRECTF(frameRect);
      int status = SafeNativeMethods.Gdip.GdipRecordMetafileFileName(fileName, new HandleRef((object) null, referenceHdc), (int) type, ref frameRect1, (int) frameUnit, description, out metafile);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect)
      : this(fileName, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    public Metafile(
      string fileName,
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit)
      : this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      string fileName,
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type)
      : this(fileName, referenceHdc, frameRect, frameUnit, type, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure. A descriptive string can also be added.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      string fileName,
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit,
      string description)
      : this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, description)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can also be added.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      string fileName,
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type,
      string description)
    {
      System.Drawing.IntSecurity.DemandReadFileIO(fileName);
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      int status;
      if (frameRect.IsEmpty)
      {
        status = SafeNativeMethods.Gdip.GdipRecordMetafileFileName(fileName, new HandleRef((object) null, referenceHdc), (int) type, System.Drawing.NativeMethods.NullHandleRef, (int) frameUnit, description, out metafile);
      }
      else
      {
        GPRECT frameRect1 = new GPRECT(frameRect);
        status = SafeNativeMethods.Gdip.GdipRecordMetafileFileNameI(fileName, new HandleRef((object) null, referenceHdc), (int) type, ref frameRect1, (int) frameUnit, description, out metafile);
      }
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    public Metafile(Stream stream, IntPtr referenceHdc)
      : this(stream, referenceHdc, EmfType.EmfPlusDual, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(Stream stream, IntPtr referenceHdc, EmfType type)
      : this(stream, referenceHdc, type, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. Also, a string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(Stream stream, IntPtr referenceHdc, EmfType type, string description)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipRecordMetafileStream((UnsafeNativeMethods.IStream) new GPStream(stream), new HandleRef((object) null, referenceHdc), (int) type, System.Drawing.NativeMethods.NullHandleRef, 7, description, out metafile);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect)
      : this(stream, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    public Metafile(
      Stream stream,
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit)
      : this(stream, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      Stream stream,
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type)
      : this(stream, referenceHdc, frameRect, frameUnit, type, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      Stream stream,
      IntPtr referenceHdc,
      RectangleF frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type,
      string description)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      GPRECTF frameRect1 = new GPRECTF(frameRect);
      int status = SafeNativeMethods.Gdip.GdipRecordMetafileStream((UnsafeNativeMethods.IStream) new GPStream(stream), new HandleRef((object) null, referenceHdc), (int) type, ref frameRect1, (int) frameUnit, description, out metafile);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect)
      : this(stream, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    public Metafile(
      Stream stream,
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit)
      : this(stream, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      Stream stream,
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type)
      : this(stream, referenceHdc, frameRect, frameUnit, type, (string) null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="referenceHdc">A Windows handle to a device context.</param>
    /// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
    /// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    /// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
    public Metafile(
      Stream stream,
      IntPtr referenceHdc,
      Rectangle frameRect,
      MetafileFrameUnit frameUnit,
      EmfType type,
      string description)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr metafile = IntPtr.Zero;
      int status;
      if (frameRect.IsEmpty)
      {
        status = SafeNativeMethods.Gdip.GdipRecordMetafileStream((UnsafeNativeMethods.IStream) new GPStream(stream), new HandleRef((object) null, referenceHdc), (int) type, System.Drawing.NativeMethods.NullHandleRef, (int) frameUnit, description, out metafile);
      }
      else
      {
        GPRECT frameRect1 = new GPRECT(frameRect);
        status = SafeNativeMethods.Gdip.GdipRecordMetafileStreamI((UnsafeNativeMethods.IStream) new GPStream(stream), new HandleRef((object) null, referenceHdc), (int) type, ref frameRect1, (int) frameUnit, description, out metafile);
      }
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeImage(metafile);
    }

    private Metafile(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="hmetafile">The handle to the <see cref="T:System.Drawing.Imaging.Metafile" /> for which to return a header.</param>
    /// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public static MetafileHeader GetMetafileHeader(
      IntPtr hmetafile,
      WmfPlaceableFileHeader wmfHeader)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      MetafileHeader metafileHeader = new MetafileHeader();
      metafileHeader.wmf = new MetafileHeaderWmf();
      int metafileHeaderFromWmf = SafeNativeMethods.Gdip.GdipGetMetafileHeaderFromWmf(new HandleRef((object) null, hmetafile), wmfHeader, metafileHeader.wmf);
      if (metafileHeaderFromWmf != 0)
        throw SafeNativeMethods.Gdip.StatusException(metafileHeaderFromWmf);
      return metafileHeader;
    }

    /// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="henhmetafile">The handle to the enhanced <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is returned.</param>
    /// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public static MetafileHeader GetMetafileHeader(IntPtr henhmetafile)
    {
      System.Drawing.IntSecurity.ObjectFromWin32Handle.Demand();
      MetafileHeader metafileHeader = new MetafileHeader();
      metafileHeader.emf = new MetafileHeaderEmf();
      int metafileHeaderFromEmf = SafeNativeMethods.Gdip.GdipGetMetafileHeaderFromEmf(new HandleRef((object) null, henhmetafile), metafileHeader.emf);
      if (metafileHeaderFromEmf != 0)
        throw SafeNativeMethods.Gdip.StatusException(metafileHeaderFromEmf);
      return metafileHeader;
    }

    /// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="fileName">A <see cref="T:System.String" /> containing the name of the <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is retrieved.</param>
    /// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public static MetafileHeader GetMetafileHeader(string fileName)
    {
      System.Drawing.IntSecurity.DemandReadFileIO(fileName);
      MetafileHeader metafileHeader = new MetafileHeader();
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (MetafileHeaderEmf)));
      try
      {
        int metafileHeaderFromFile = SafeNativeMethods.Gdip.GdipGetMetafileHeaderFromFile(fileName, num);
        if (metafileHeaderFromFile != 0)
          throw SafeNativeMethods.Gdip.StatusException(metafileHeaderFromFile);
        int[] destination = new int[1];
        Marshal.Copy(num, destination, 0, 1);
        switch ((MetafileType) destination[0])
        {
          case MetafileType.Wmf:
          case MetafileType.WmfPlaceable:
            metafileHeader.wmf = (MetafileHeaderWmf) UnsafeNativeMethods.PtrToStructure(num, typeof (MetafileHeaderWmf));
            metafileHeader.emf = (MetafileHeaderEmf) null;
            break;
          default:
            metafileHeader.wmf = (MetafileHeaderWmf) null;
            metafileHeader.emf = (MetafileHeaderEmf) UnsafeNativeMethods.PtrToStructure(num, typeof (MetafileHeaderEmf));
            break;
        }
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
      return metafileHeader;
    }

    /// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> containing the <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is retrieved.</param>
    /// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public static MetafileHeader GetMetafileHeader(Stream stream)
    {
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (MetafileHeaderEmf)));
      MetafileHeader metafileHeader;
      try
      {
        int headerFromStream = SafeNativeMethods.Gdip.GdipGetMetafileHeaderFromStream((UnsafeNativeMethods.IStream) new GPStream(stream), num);
        if (headerFromStream != 0)
          throw SafeNativeMethods.Gdip.StatusException(headerFromStream);
        int[] destination = new int[1];
        Marshal.Copy(num, destination, 0, 1);
        MetafileType metafileType = (MetafileType) destination[0];
        metafileHeader = new MetafileHeader();
        if (metafileType == MetafileType.Wmf || metafileType == MetafileType.WmfPlaceable)
        {
          metafileHeader.wmf = (MetafileHeaderWmf) UnsafeNativeMethods.PtrToStructure(num, typeof (MetafileHeaderWmf));
          metafileHeader.emf = (MetafileHeaderEmf) null;
        }
        else
        {
          metafileHeader.wmf = (MetafileHeaderWmf) null;
          metafileHeader.emf = (MetafileHeaderEmf) UnsafeNativeMethods.PtrToStructure(num, typeof (MetafileHeaderEmf));
        }
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
      return metafileHeader;
    }

    /// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with this <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with this <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public MetafileHeader GetMetafileHeader()
    {
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (MetafileHeaderEmf)));
      MetafileHeader metafileHeader;
      try
      {
        int headerFromMetafile = SafeNativeMethods.Gdip.GdipGetMetafileHeaderFromMetafile(new HandleRef((object) this, this.nativeImage), num);
        if (headerFromMetafile != 0)
          throw SafeNativeMethods.Gdip.StatusException(headerFromMetafile);
        int[] destination = new int[1];
        Marshal.Copy(num, destination, 0, 1);
        MetafileType metafileType = (MetafileType) destination[0];
        metafileHeader = new MetafileHeader();
        if (metafileType == MetafileType.Wmf || metafileType == MetafileType.WmfPlaceable)
        {
          metafileHeader.wmf = (MetafileHeaderWmf) UnsafeNativeMethods.PtrToStructure(num, typeof (MetafileHeaderWmf));
          metafileHeader.emf = (MetafileHeaderEmf) null;
        }
        else
        {
          metafileHeader.wmf = (MetafileHeaderWmf) null;
          metafileHeader.emf = (MetafileHeaderEmf) UnsafeNativeMethods.PtrToStructure(num, typeof (MetafileHeaderEmf));
        }
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
      return metafileHeader;
    }

    /// <summary>Returns a Windows handle to an enhanced <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>A Windows handle to this enhanced <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public IntPtr GetHenhmetafile()
    {
      IntPtr hEnhMetafile = IntPtr.Zero;
      int hemfFromMetafile = SafeNativeMethods.Gdip.GdipGetHemfFromMetafile(new HandleRef((object) this, this.nativeImage), out hEnhMetafile);
      if (hemfFromMetafile != 0)
        throw SafeNativeMethods.Gdip.StatusException(hemfFromMetafile);
      return hEnhMetafile;
    }

    /// <summary>Plays an individual metafile record.</summary>
    /// <param name="recordType">Element of the <see cref="T:System.Drawing.Imaging.EmfPlusRecordType" /> that specifies the type of metafile record being played.</param>
    /// <param name="flags">A set of flags that specify attributes of the record.</param>
    /// <param name="dataSize">The number of bytes in the record data.</param>
    /// <param name="data">An array of bytes that contains the record data.</param>
    public void PlayRecord(EmfPlusRecordType recordType, int flags, int dataSize, byte[] data)
    {
      int status = SafeNativeMethods.Gdip.GdipPlayMetafileRecord(new HandleRef((object) this, this.nativeImage), recordType, flags, dataSize, data);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    internal static Metafile FromGDIplus(IntPtr nativeImage)
    {
      Metafile metafile = new Metafile();
      metafile.SetNativeImage(nativeImage);
      return metafile;
    }

    private Metafile()
    {
    }
  }
}
