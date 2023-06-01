// Decompiled with JetBrains decompiler
// Type: System.Drawing.Bitmap
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Drawing
{
  /// <summary>Encapsulates a GDI+ bitmap, which consists of the pixel data for a graphics image and its attributes. A <see cref="T:System.Drawing.Bitmap" /> is an object used to work with images defined by pixel data.</summary>
  [Editor("System.Drawing.Design.BitmapEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [ComVisible(true)]
  [Serializable]
  public sealed class Bitmap : Image
  {
    private static Color defaultTransparentColor = Color.LightGray;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified file.</summary>
    /// <param name="filename">The bitmap file name and path.</param>
    /// <exception cref="T:System.IO.FileNotFoundException">The specified file is not found.</exception>
    public Bitmap(string filename)
    {
      IntSecurity.DemandReadFileIO(filename);
      filename = Path.GetFullPath(filename);
      IntPtr bitmap = IntPtr.Zero;
      int bitmapFromFile = SafeNativeMethods.Gdip.GdipCreateBitmapFromFile(filename, out bitmap);
      if (bitmapFromFile != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromFile);
      int status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, bitmap));
      if (status != 0)
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, bitmap));
        throw SafeNativeMethods.Gdip.StatusException(status);
      }
      this.SetNativeImage(bitmap);
      Image.EnsureSave((Image) this, filename, (Stream) null);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified file.</summary>
    /// <param name="filename">The name of the bitmap file.</param>
    /// <param name="useIcm">
    /// <see langword="true" /> to use color correction for this <see cref="T:System.Drawing.Bitmap" />; otherwise, <see langword="false" />.</param>
    public Bitmap(string filename, bool useIcm)
    {
      IntSecurity.DemandReadFileIO(filename);
      filename = Path.GetFullPath(filename);
      IntPtr bitmap = IntPtr.Zero;
      int status1 = !useIcm ? SafeNativeMethods.Gdip.GdipCreateBitmapFromFile(filename, out bitmap) : SafeNativeMethods.Gdip.GdipCreateBitmapFromFileICM(filename, out bitmap);
      if (status1 != 0)
        throw SafeNativeMethods.Gdip.StatusException(status1);
      int status2 = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, bitmap));
      if (status2 != 0)
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, bitmap));
        throw SafeNativeMethods.Gdip.StatusException(status2);
      }
      this.SetNativeImage(bitmap);
      Image.EnsureSave((Image) this, filename, (Stream) null);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from a specified resource.</summary>
    /// <param name="type">The class used to extract the resource.</param>
    /// <param name="resource">The name of the resource.</param>
    public Bitmap(Type type, string resource)
    {
      Stream manifestResourceStream = type.Module.Assembly.GetManifestResourceStream(type, resource);
      if (manifestResourceStream == null)
        throw new ArgumentException(SR.GetString("ResourceNotFound", (object) type, (object) resource));
      IntPtr bitmap = IntPtr.Zero;
      int bitmapFromStream = SafeNativeMethods.Gdip.GdipCreateBitmapFromStream((UnsafeNativeMethods.IStream) new GPStream(manifestResourceStream), out bitmap);
      if (bitmapFromStream != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromStream);
      int status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, bitmap));
      if (status != 0)
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, bitmap));
        throw SafeNativeMethods.Gdip.StatusException(status);
      }
      this.SetNativeImage(bitmap);
      Image.EnsureSave((Image) this, (string) null, manifestResourceStream);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified data stream.</summary>
    /// <param name="stream">The data stream used to load the image.</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="stream" /> does not contain image data or is <see langword="null" />.
    /// -or-
    /// <paramref name="stream" /> contains a PNG image file with a single dimension greater than 65,535 pixels.</exception>
    public Bitmap(Stream stream)
    {
      if (stream == null)
        throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (stream), (object) "null"));
      IntPtr bitmap = IntPtr.Zero;
      int bitmapFromStream = SafeNativeMethods.Gdip.GdipCreateBitmapFromStream((UnsafeNativeMethods.IStream) new GPStream(stream), out bitmap);
      if (bitmapFromStream != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromStream);
      int status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, bitmap));
      if (status != 0)
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, bitmap));
        throw SafeNativeMethods.Gdip.StatusException(status);
      }
      this.SetNativeImage(bitmap);
      Image.EnsureSave((Image) this, (string) null, stream);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified data stream.</summary>
    /// <param name="stream">The data stream used to load the image.</param>
    /// <param name="useIcm">
    /// <see langword="true" /> to use color correction for this <see cref="T:System.Drawing.Bitmap" />; otherwise, <see langword="false" />.</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="stream" /> does not contain image data or is <see langword="null" />.
    /// -or-
    /// <paramref name="stream" /> contains a PNG image file with a single dimension greater than 65,535 pixels.</exception>
    public Bitmap(Stream stream, bool useIcm)
    {
      if (stream == null)
        throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (stream), (object) "null"));
      IntPtr bitmap = IntPtr.Zero;
      int status1 = !useIcm ? SafeNativeMethods.Gdip.GdipCreateBitmapFromStream((UnsafeNativeMethods.IStream) new GPStream(stream), out bitmap) : SafeNativeMethods.Gdip.GdipCreateBitmapFromStreamICM((UnsafeNativeMethods.IStream) new GPStream(stream), out bitmap);
      if (status1 != 0)
        throw SafeNativeMethods.Gdip.StatusException(status1);
      int status2 = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, bitmap));
      if (status2 != 0)
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, bitmap));
        throw SafeNativeMethods.Gdip.StatusException(status2);
      }
      this.SetNativeImage(bitmap);
      Image.EnsureSave((Image) this, (string) null, stream);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size, pixel format, and pixel data.</summary>
    /// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="stride">Integer that specifies the byte offset between the beginning of one scan line and the next. This is usually (but not necessarily) the number of bytes in the pixel format (for example, 2 for 16 bits per pixel) multiplied by the width of the bitmap. The value passed to this parameter must be a multiple of four.</param>
    /// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with Format.</param>
    /// <param name="scan0">Pointer to an array of bytes that contains the pixel data.</param>
    /// <exception cref="T:System.ArgumentException">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
    public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr bitmap = IntPtr.Zero;
      int bitmapFromScan0 = SafeNativeMethods.Gdip.GdipCreateBitmapFromScan0(width, height, stride, (int) format, new HandleRef((object) null, scan0), out bitmap);
      if (bitmapFromScan0 != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromScan0);
      this.SetNativeImage(bitmap);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size and format.</summary>
    /// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with Format.</param>
    /// <exception cref="T:System.ArgumentException">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
    public Bitmap(int width, int height, PixelFormat format)
    {
      IntPtr bitmap = IntPtr.Zero;
      int bitmapFromScan0 = SafeNativeMethods.Gdip.GdipCreateBitmapFromScan0(width, height, 0, (int) format, NativeMethods.NullHandleRef, out bitmap);
      if (bitmapFromScan0 != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromScan0);
      this.SetNativeImage(bitmap);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size.</summary>
    /// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    public Bitmap(int width, int height)
      : this(width, height, PixelFormat.Format32bppArgb)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size and with the resolution of the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
    /// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object that specifies the resolution for the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="g" /> is <see langword="null" />.</exception>
    public Bitmap(int width, int height, Graphics g)
    {
      if (g == null)
        throw new ArgumentNullException(SR.GetString("InvalidArgument", (object) nameof (g), (object) "null"));
      IntPtr bitmap = IntPtr.Zero;
      int bitmapFromGraphics = SafeNativeMethods.Gdip.GdipCreateBitmapFromGraphics(width, height, new HandleRef((object) g, g.NativeGraphics), out bitmap);
      if (bitmapFromGraphics != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromGraphics);
      this.SetNativeImage(bitmap);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image.</summary>
    /// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />.</param>
    public Bitmap(Image original)
      : this(original, original.Width, original.Height)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image, scaled to the specified size.</summary>
    /// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    public Bitmap(Image original, int width, int height)
      : this(width, height)
    {
      Graphics graphics = (Graphics) null;
      try
      {
        graphics = Graphics.FromImage((Image) this);
        graphics.Clear(Color.Transparent);
        graphics.DrawImage(original, 0, 0, width, height);
      }
      finally
      {
        graphics?.Dispose();
      }
    }

    private Bitmap(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a Windows handle to an icon.</summary>
    /// <param name="hicon">A handle to an icon.</param>
    /// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
    public static Bitmap FromHicon(IntPtr hicon)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr bitmap = IntPtr.Zero;
      int bitmapFromHicon = SafeNativeMethods.Gdip.GdipCreateBitmapFromHICON(new HandleRef((object) null, hicon), out bitmap);
      if (bitmapFromHicon != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromHicon);
      return Bitmap.FromGDIplus(bitmap);
    }

    /// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from the specified Windows resource.</summary>
    /// <param name="hinstance">A handle to an instance of the executable file that contains the resource.</param>
    /// <param name="bitmapName">A string that contains the name of the resource bitmap.</param>
    /// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
    public static Bitmap FromResource(IntPtr hinstance, string bitmapName)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr hglobalUni = Marshal.StringToHGlobalUni(bitmapName);
      IntPtr bitmap;
      int bitmapFromResource = SafeNativeMethods.Gdip.GdipCreateBitmapFromResource(new HandleRef((object) null, hinstance), new HandleRef((object) null, hglobalUni), out bitmap);
      Marshal.FreeHGlobal(hglobalUni);
      if (bitmapFromResource != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromResource);
      return Bitmap.FromGDIplus(bitmap);
    }

    /// <summary>Creates a GDI bitmap object from this <see cref="T:System.Drawing.Bitmap" />.</summary>
    /// <returns>A handle to the GDI bitmap object that this method creates.</returns>
    /// <exception cref="T:System.ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public IntPtr GetHbitmap() => this.GetHbitmap(Color.LightGray);

    /// <summary>Creates a GDI bitmap object from this <see cref="T:System.Drawing.Bitmap" />.</summary>
    /// <param name="background">A <see cref="T:System.Drawing.Color" /> structure that specifies the background color. This parameter is ignored if the bitmap is totally opaque.</param>
    /// <returns>A handle to the GDI bitmap object that this method creates.</returns>
    /// <exception cref="T:System.ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public IntPtr GetHbitmap(Color background)
    {
      IntPtr hbitmap = IntPtr.Zero;
      int hbitmapFromBitmap = SafeNativeMethods.Gdip.GdipCreateHBITMAPFromBitmap(new HandleRef((object) this, this.nativeImage), out hbitmap, ColorTranslator.ToWin32(background));
      if (hbitmapFromBitmap == 2 && (this.Width >= (int) short.MaxValue || this.Height >= (int) short.MaxValue))
        throw new ArgumentException(SR.GetString("GdiplusInvalidSize"));
      if (hbitmapFromBitmap != 0)
        throw SafeNativeMethods.Gdip.StatusException(hbitmapFromBitmap);
      return hbitmap;
    }

    /// <summary>Returns the handle to an icon.</summary>
    /// <returns>A Windows handle to an icon with the same image as the <see cref="T:System.Drawing.Bitmap" />.</returns>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public IntPtr GetHicon()
    {
      IntPtr hicon = IntPtr.Zero;
      int hiconFromBitmap = SafeNativeMethods.Gdip.GdipCreateHICONFromBitmap(new HandleRef((object) this, this.nativeImage), out hicon);
      if (hiconFromBitmap != 0)
        throw SafeNativeMethods.Gdip.StatusException(hiconFromBitmap);
      return hicon;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image, scaled to the specified size.</summary>
    /// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="newSize">The <see cref="T:System.Drawing.Size" /> structure that represent the size of the new <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    public Bitmap(Image original, Size newSize)
      : this(original, (ValueType) newSize != null ? newSize.Width : 0, (ValueType) newSize != null ? newSize.Height : 0)
    {
    }

    private Bitmap()
    {
    }

    internal static Bitmap FromGDIplus(IntPtr handle)
    {
      Bitmap bitmap = new Bitmap();
      bitmap.SetNativeImage(handle);
      return bitmap;
    }

    /// <summary>Creates a copy of the section of this <see cref="T:System.Drawing.Bitmap" /> defined by <see cref="T:System.Drawing.Rectangle" /> structure and with a specified <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration.</summary>
    /// <param name="rect">Defines the portion of this <see cref="T:System.Drawing.Bitmap" /> to copy. Coordinates are relative to this <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with Format.</param>
    /// <returns>The new <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
    /// <exception cref="T:System.OutOfMemoryException">
    /// <paramref name="rect" /> is outside of the source bitmap bounds.</exception>
    /// <exception cref="T:System.ArgumentException">The height or width of <paramref name="rect" /> is 0.
    /// -or-
    /// A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
    public Bitmap Clone(Rectangle rect, PixelFormat format)
    {
      if (rect.Width == 0 || rect.Height == 0)
        throw new ArgumentException(SR.GetString("GdiplusInvalidRectangle", (object) rect.ToString()));
      IntPtr dstbitmap = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneBitmapAreaI(rect.X, rect.Y, rect.Width, rect.Height, (int) format, new HandleRef((object) this, this.nativeImage), out dstbitmap);
      if (status != 0 || dstbitmap == IntPtr.Zero)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return Bitmap.FromGDIplus(dstbitmap);
    }

    /// <summary>Creates a copy of the section of this <see cref="T:System.Drawing.Bitmap" /> defined with a specified <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration.</summary>
    /// <param name="rect">Defines the portion of this <see cref="T:System.Drawing.Bitmap" /> to copy.</param>
    /// <param name="format">Specifies the <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration for the destination <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
    /// <exception cref="T:System.OutOfMemoryException">
    /// <paramref name="rect" /> is outside of the source bitmap bounds.</exception>
    /// <exception cref="T:System.ArgumentException">The height or width of <paramref name="rect" /> is 0.</exception>
    public Bitmap Clone(RectangleF rect, PixelFormat format)
    {
      if ((double) rect.Width == 0.0 || (double) rect.Height == 0.0)
        throw new ArgumentException(SR.GetString("GdiplusInvalidRectangle", (object) rect.ToString()));
      IntPtr dstbitmap = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneBitmapArea(rect.X, rect.Y, rect.Width, rect.Height, (int) format, new HandleRef((object) this, this.nativeImage), out dstbitmap);
      if (status != 0 || dstbitmap == IntPtr.Zero)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return Bitmap.FromGDIplus(dstbitmap);
    }

    /// <summary>Makes the default transparent color transparent for this <see cref="T:System.Drawing.Bitmap" />.</summary>
    /// <exception cref="T:System.InvalidOperationException">The image format of the <see cref="T:System.Drawing.Bitmap" /> is an icon format.</exception>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    public void MakeTransparent()
    {
      Color transparentColor = Bitmap.defaultTransparentColor;
      if (this.Height > 0 && this.Width > 0)
        transparentColor = this.GetPixel(0, this.Size.Height - 1);
      if (transparentColor.A < byte.MaxValue)
        return;
      this.MakeTransparent(transparentColor);
    }

    /// <summary>Makes the specified color transparent for this <see cref="T:System.Drawing.Bitmap" />.</summary>
    /// <param name="transparentColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color to make transparent.</param>
    /// <exception cref="T:System.InvalidOperationException">The image format of the <see cref="T:System.Drawing.Bitmap" /> is an icon format.</exception>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    public void MakeTransparent(Color transparentColor)
    {
      if (this.RawFormat.Guid == ImageFormat.Icon.Guid)
        throw new InvalidOperationException(SR.GetString("CantMakeIconTransparent"));
      Size size = this.Size;
      Bitmap bitmap = (Bitmap) null;
      Graphics graphics = (Graphics) null;
      try
      {
        bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
        try
        {
          graphics = Graphics.FromImage((Image) bitmap);
          graphics.Clear(Color.Transparent);
          Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);
          ImageAttributes imageAttrs = (ImageAttributes) null;
          try
          {
            imageAttrs = new ImageAttributes();
            imageAttrs.SetColorKey(transparentColor, transparentColor);
            graphics.DrawImage((Image) this, destRect, 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, imageAttrs, (Graphics.DrawImageAbort) null, IntPtr.Zero);
          }
          finally
          {
            imageAttrs?.Dispose();
          }
        }
        finally
        {
          graphics?.Dispose();
        }
        IntPtr nativeImage = this.nativeImage;
        this.nativeImage = bitmap.nativeImage;
        bitmap.nativeImage = nativeImage;
      }
      finally
      {
        bitmap?.Dispose();
      }
    }

    /// <summary>Locks a <see cref="T:System.Drawing.Bitmap" /> into system memory.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <see cref="T:System.Drawing.Bitmap" /> to lock.</param>
    /// <param name="flags">An <see cref="T:System.Drawing.Imaging.ImageLockMode" /> enumeration that specifies the access level (read/write) for the <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="format">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration that specifies the data format of this <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <returns>A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about this lock operation.</returns>
    /// <exception cref="T:System.ArgumentException">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> is not a specific bits-per-pixel value.
    /// -or-
    /// The incorrect <see cref="T:System.Drawing.Imaging.PixelFormat" /> is passed in for a bitmap.</exception>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
    {
      BitmapData bitmapData = new BitmapData();
      return this.LockBits(rect, flags, format, bitmapData);
    }

    /// <summary>Locks a <see cref="T:System.Drawing.Bitmap" /> into system memory</summary>
    /// <param name="rect">A rectangle structure that specifies the portion of the <see cref="T:System.Drawing.Bitmap" /> to lock.</param>
    /// <param name="flags">One of the <see cref="T:System.Drawing.Imaging.ImageLockMode" /> values that specifies the access level (read/write) for the <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="format">One of the <see cref="T:System.Drawing.Imaging.PixelFormat" /> values that specifies the data format of the <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="bitmapData">A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about the lock operation.</param>
    /// <returns>A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about the lock operation.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///         <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is not a specific bits-per-pixel value.
    /// -or-
    /// The incorrect <see cref="T:System.Drawing.Imaging.PixelFormat" /> is passed in for a bitmap.</exception>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public BitmapData LockBits(
      Rectangle rect,
      ImageLockMode flags,
      PixelFormat format,
      BitmapData bitmapData)
    {
      GPRECT rect1 = new GPRECT(rect);
      int status = SafeNativeMethods.Gdip.GdipBitmapLockBits(new HandleRef((object) this, this.nativeImage), ref rect1, flags, format, bitmapData);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return bitmapData;
    }

    /// <summary>Unlocks this <see cref="T:System.Drawing.Bitmap" /> from system memory.</summary>
    /// <param name="bitmapdata">A <see cref="T:System.Drawing.Imaging.BitmapData" /> that specifies information about the lock operation.</param>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public void UnlockBits(BitmapData bitmapdata)
    {
      int status = SafeNativeMethods.Gdip.GdipBitmapUnlockBits(new HandleRef((object) this, this.nativeImage), bitmapdata);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets the color of the specified pixel in this <see cref="T:System.Drawing.Bitmap" />.</summary>
    /// <param name="x">The x-coordinate of the pixel to retrieve.</param>
    /// <param name="y">The y-coordinate of the pixel to retrieve.</param>
    /// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of the specified pixel.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="x" /> is less than 0, or greater than or equal to <see cref="P:System.Drawing.Image.Width" />.
    /// -or-
    /// <paramref name="y" /> is less than 0, or greater than or equal to <see cref="P:System.Drawing.Image.Height" />.</exception>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    public Color GetPixel(int x, int y)
    {
      int argb = 0;
      if (x < 0 || x >= this.Width)
        throw new ArgumentOutOfRangeException(nameof (x), SR.GetString("ValidRangeX"));
      if (y < 0 || y >= this.Height)
        throw new ArgumentOutOfRangeException(nameof (y), SR.GetString("ValidRangeY"));
      int pixel = SafeNativeMethods.Gdip.GdipBitmapGetPixel(new HandleRef((object) this, this.nativeImage), x, y, out argb);
      if (pixel != 0)
        throw SafeNativeMethods.Gdip.StatusException(pixel);
      return Color.FromArgb(argb);
    }

    /// <summary>Sets the color of the specified pixel in this <see cref="T:System.Drawing.Bitmap" />.</summary>
    /// <param name="x">The x-coordinate of the pixel to set.</param>
    /// <param name="y">The y-coordinate of the pixel to set.</param>
    /// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that represents the color to assign to the specified pixel.</param>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    public void SetPixel(int x, int y, Color color)
    {
      if ((this.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
        throw new InvalidOperationException(SR.GetString("GdiplusCannotSetPixelFromIndexedPixelFormat"));
      if (x < 0 || x >= this.Width)
        throw new ArgumentOutOfRangeException(nameof (x), SR.GetString("ValidRangeX"));
      if (y < 0 || y >= this.Height)
        throw new ArgumentOutOfRangeException(nameof (y), SR.GetString("ValidRangeY"));
      int status = SafeNativeMethods.Gdip.GdipBitmapSetPixel(new HandleRef((object) this, this.nativeImage), x, y, color.ToArgb());
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the resolution for this <see cref="T:System.Drawing.Bitmap" />.</summary>
    /// <param name="xDpi">The horizontal resolution, in dots per inch, of the <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="yDpi">The vertical resolution, in dots per inch, of the <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <exception cref="T:System.Exception">The operation failed.</exception>
    public void SetResolution(float xDpi, float yDpi)
    {
      int status = SafeNativeMethods.Gdip.GdipBitmapSetResolution(new HandleRef((object) this, this.nativeImage), xDpi, yDpi);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }
  }
}
