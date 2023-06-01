// Decompiled with JetBrains decompiler
// Type: System.Drawing.Image
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
  /// <summary>An abstract base class that provides functionality for the <see cref="T:System.Drawing.Bitmap" /> and <see cref="T:System.Drawing.Imaging.Metafile" /> descended classes.</summary>
  [TypeConverter(typeof (ImageConverter))]
  [Editor("System.Drawing.Design.ImageEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [ImmutableObject(true)]
  [ComVisible(true)]
  [Serializable]
  public abstract class Image : MarshalByRefObject, ISerializable, ICloneable, IDisposable
  {
    internal IntPtr nativeImage;
    private byte[] rawData;
    private object userData;

    internal Image()
    {
    }

    internal Image(SerializationInfo info, StreamingContext context)
    {
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      if (enumerator == null)
        return;
      while (enumerator.MoveNext())
      {
        if (string.Equals(enumerator.Name, "Data", StringComparison.OrdinalIgnoreCase))
        {
          try
          {
            byte[] buffer = (byte[]) enumerator.Value;
            if (buffer != null)
              this.InitializeFromStream((Stream) new MemoryStream(buffer));
          }
          catch (ExternalException ex)
          {
          }
          catch (ArgumentException ex)
          {
          }
          catch (OutOfMemoryException ex)
          {
          }
          catch (InvalidOperationException ex)
          {
          }
          catch (NotImplementedException ex)
          {
          }
          catch (FileNotFoundException ex)
          {
          }
        }
      }
    }

    /// <summary>Gets or sets an object that provides additional data about the image.</summary>
    /// <returns>The <see cref="T:System.Object" /> that provides additional data about the image.</returns>
    [Localizable(false)]
    [Bindable(true)]
    [DefaultValue(null)]
    [TypeConverter(typeof (StringConverter))]
    public object Tag
    {
      get => this.userData;
      set => this.userData = value;
    }

    /// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified file.</summary>
    /// <param name="filename">A string that contains the name of the file from which to create the <see cref="T:System.Drawing.Image" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
    /// <exception cref="T:System.OutOfMemoryException">The file does not have a valid image format.
    /// -or-
    /// GDI+ does not support the pixel format of the file.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="filename" /> is a <see cref="T:System.Uri" />.</exception>
    public static Image FromFile(string filename) => Image.FromFile(filename, false);

    /// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified file using embedded color management information in that file.</summary>
    /// <param name="filename">A string that contains the name of the file from which to create the <see cref="T:System.Drawing.Image" />.</param>
    /// <param name="useEmbeddedColorManagement">Set to <see langword="true" /> to use color management information embedded in the image file; otherwise, <see langword="false" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
    /// <exception cref="T:System.OutOfMemoryException">The file does not have a valid image format.
    /// -or-
    /// GDI+ does not support the pixel format of the file.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="filename" /> is a <see cref="T:System.Uri" />.</exception>
    public static Image FromFile(string filename, bool useEmbeddedColorManagement)
    {
      if (!File.Exists(filename))
      {
        IntSecurity.DemandReadFileIO(filename);
        throw new FileNotFoundException(filename);
      }
      filename = Path.GetFullPath(filename);
      IntPtr image = IntPtr.Zero;
      int status1 = !useEmbeddedColorManagement ? SafeNativeMethods.Gdip.GdipLoadImageFromFile(filename, out image) : SafeNativeMethods.Gdip.GdipLoadImageFromFileICM(filename, out image);
      if (status1 != 0)
        throw SafeNativeMethods.Gdip.StatusException(status1);
      int status2 = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, image));
      if (status2 != 0)
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, image));
        throw SafeNativeMethods.Gdip.StatusException(status2);
      }
      Image imageObject = Image.CreateImageObject(image);
      Image.EnsureSave(imageObject, filename, (Stream) null);
      return imageObject;
    }

    /// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
    /// <exception cref="T:System.ArgumentException">The stream does not have a valid image format
    /// -or-
    /// <paramref name="stream" /> is <see langword="null" />.</exception>
    public static Image FromStream(Stream stream) => Image.FromStream(stream, false);

    /// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream, optionally using embedded color management information in that stream.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />.</param>
    /// <param name="useEmbeddedColorManagement">
    /// <see langword="true" /> to use color management information embedded in the data stream; otherwise, <see langword="false" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
    /// <exception cref="T:System.ArgumentException">The stream does not have a valid image format
    /// -or-
    /// <paramref name="stream" /> is <see langword="null" />.</exception>
    public static Image FromStream(Stream stream, bool useEmbeddedColorManagement) => Image.FromStream(stream, useEmbeddedColorManagement, true);

    /// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream, optionally using embedded color management information and validating the image data.</summary>
    /// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />.</param>
    /// <param name="useEmbeddedColorManagement">
    /// <see langword="true" /> to use color management information embedded in the data stream; otherwise, <see langword="false" />.</param>
    /// <param name="validateImageData">
    /// <see langword="true" /> to validate the image data; otherwise, <see langword="false" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
    /// <exception cref="T:System.ArgumentException">The stream does not have a valid image format.</exception>
    public static Image FromStream(
      Stream stream,
      bool useEmbeddedColorManagement,
      bool validateImageData)
    {
      if (!validateImageData)
        IntSecurity.UnmanagedCode.Demand();
      if (stream == null)
        throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (stream), (object) "null"));
      IntPtr image = IntPtr.Zero;
      int status1 = !useEmbeddedColorManagement ? SafeNativeMethods.Gdip.GdipLoadImageFromStream((UnsafeNativeMethods.IStream) new GPStream(stream), out image) : SafeNativeMethods.Gdip.GdipLoadImageFromStreamICM((UnsafeNativeMethods.IStream) new GPStream(stream), out image);
      if (status1 != 0)
        throw SafeNativeMethods.Gdip.StatusException(status1);
      if (validateImageData)
      {
        int status2 = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, image));
        if (status2 != 0)
        {
          SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, image));
          throw SafeNativeMethods.Gdip.StatusException(status2);
        }
      }
      Image imageObject = Image.CreateImageObject(image);
      Image.EnsureSave(imageObject, (string) null, stream);
      return imageObject;
    }

    private void InitializeFromStream(Stream stream)
    {
      IntPtr image = IntPtr.Zero;
      int status1 = SafeNativeMethods.Gdip.GdipLoadImageFromStream((UnsafeNativeMethods.IStream) new GPStream(stream), out image);
      if (status1 != 0)
        throw SafeNativeMethods.Gdip.StatusException(status1);
      int status2 = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, image));
      if (status2 != 0)
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, image));
        throw SafeNativeMethods.Gdip.StatusException(status2);
      }
      this.nativeImage = image;
      int type = -1;
      int imageType = SafeNativeMethods.Gdip.GdipGetImageType(new HandleRef((object) this, this.nativeImage), out type);
      Image.EnsureSave(this, (string) null, stream);
      if (imageType != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageType);
    }

    internal Image(IntPtr nativeImage) => this.SetNativeImage(nativeImage);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Image" /> this method creates, cast as an object.</returns>
    public object Clone()
    {
      IntPtr cloneimage = IntPtr.Zero;
      int status1 = SafeNativeMethods.Gdip.GdipCloneImage(new HandleRef((object) this, this.nativeImage), out cloneimage);
      if (status1 != 0)
        throw SafeNativeMethods.Gdip.StatusException(status1);
      int status2 = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef((object) null, cloneimage));
      if (status2 != 0)
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) null, cloneimage));
        throw SafeNativeMethods.Gdip.StatusException(status2);
      }
      return (object) Image.CreateImageObject(cloneimage);
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Image" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Image" /> and optionally releases the managed resources.</summary>
    /// <param name="disposing">
    /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!(this.nativeImage != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef((object) this, this.nativeImage));
      }
      catch (Exception ex)
      {
        if (!ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeImage = IntPtr.Zero;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~Image() => this.Dispose(false);

    internal static void EnsureSave(Image image, string filename, Stream dataStream)
    {
      if (!image.RawFormat.Equals((object) ImageFormat.Gif))
        return;
      bool flag = false;
      foreach (Guid frameDimensions in image.FrameDimensionsList)
      {
        if (new FrameDimension(frameDimensions).Equals((object) FrameDimension.Time))
        {
          flag = image.GetFrameCount(FrameDimension.Time) > 1;
          break;
        }
      }
      if (!flag)
        return;
      try
      {
        Stream stream = (Stream) null;
        long num = 0;
        if (dataStream != null)
        {
          num = dataStream.Position;
          dataStream.Position = 0L;
        }
        try
        {
          if (dataStream == null)
            stream = dataStream = (Stream) File.OpenRead(filename);
          image.rawData = new byte[(int) dataStream.Length];
          dataStream.Read(image.rawData, 0, (int) dataStream.Length);
        }
        finally
        {
          if (stream != null)
            stream.Close();
          else
            dataStream.Position = num;
        }
      }
      catch (UnauthorizedAccessException ex)
      {
      }
      catch (DirectoryNotFoundException ex)
      {
      }
      catch (IOException ex)
      {
      }
      catch (NotSupportedException ex)
      {
      }
      catch (ObjectDisposedException ex)
      {
      }
      catch (ArgumentException ex)
      {
      }
    }

    internal static Image CreateImageObject(IntPtr nativeImage)
    {
      int type = -1;
      int imageType = SafeNativeMethods.Gdip.GdipGetImageType(new HandleRef((object) null, nativeImage), out type);
      if (imageType != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageType);
      switch ((Image.ImageTypeEnum) type)
      {
        case Image.ImageTypeEnum.Bitmap:
          return (Image) Bitmap.FromGDIplus(nativeImage);
        case Image.ImageTypeEnum.Metafile:
          return (Image) Metafile.FromGDIplus(nativeImage);
        default:
          throw new ArgumentException(SR.GetString("InvalidImage"));
      }
    }

    /// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
    /// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
    /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
    {
      using (MemoryStream stream = new MemoryStream())
      {
        this.Save(stream);
        si.AddValue("Data", (object) stream.ToArray(), typeof (byte[]));
      }
    }

    /// <summary>Returns information about the parameters supported by the specified image encoder.</summary>
    /// <param name="encoder">A GUID that specifies the image encoder.</param>
    /// <returns>An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that contains an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects. Each <see cref="T:System.Drawing.Imaging.EncoderParameter" /> contains information about one of the parameters supported by the specified image encoder.</returns>
    public EncoderParameters GetEncoderParameterList(Guid encoder)
    {
      int size;
      int parameterListSize = SafeNativeMethods.Gdip.GdipGetEncoderParameterListSize(new HandleRef((object) this, this.nativeImage), ref encoder, out size);
      if (parameterListSize != 0)
        throw SafeNativeMethods.Gdip.StatusException(parameterListSize);
      if (size <= 0)
        return (EncoderParameters) null;
      IntPtr num = Marshal.AllocHGlobal(size);
      int encoderParameterList = SafeNativeMethods.Gdip.GdipGetEncoderParameterList(new HandleRef((object) this, this.nativeImage), ref encoder, size, num);
      try
      {
        if (encoderParameterList != 0)
          throw SafeNativeMethods.Gdip.StatusException(encoderParameterList);
        return EncoderParameters.ConvertFromMemory(num);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file or stream.</summary>
    /// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="filename" /> is <see langword="null." /></exception>
    /// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.
    /// -or-
    /// The image was saved to the same file it was created from.</exception>
    public void Save(string filename) => this.Save(filename, this.RawFormat);

    /// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file in the specified format.</summary>
    /// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />.</param>
    /// <param name="format">The <see cref="T:System.Drawing.Imaging.ImageFormat" /> for this <see cref="T:System.Drawing.Image" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="filename" /> or <paramref name="format" /> is <see langword="null." /></exception>
    /// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.
    /// -or-
    /// The image was saved to the same file it was created from.</exception>
    public void Save(string filename, ImageFormat format)
    {
      if (format == null)
        throw new ArgumentNullException(nameof (format));
      ImageCodecInfo encoder = format.FindEncoder() ?? ImageFormat.Png.FindEncoder();
      this.Save(filename, encoder, (EncoderParameters) null);
    }

    /// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file, with the specified encoder and image-encoder parameters.</summary>
    /// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />.</param>
    /// <param name="encoder">The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> for this <see cref="T:System.Drawing.Image" />.</param>
    /// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> to use for this <see cref="T:System.Drawing.Image" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="filename" /> or <paramref name="encoder" /> is <see langword="null." /></exception>
    /// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.
    /// -or-
    /// The image was saved to the same file it was created from.</exception>
    public void Save(string filename, ImageCodecInfo encoder, EncoderParameters encoderParams)
    {
      if (filename == null)
        throw new ArgumentNullException(nameof (filename));
      if (encoder == null)
        throw new ArgumentNullException(nameof (encoder));
      IntSecurity.DemandWriteFileIO(filename);
      IntPtr num = IntPtr.Zero;
      if (encoderParams != null)
      {
        this.rawData = (byte[]) null;
        num = encoderParams.ConvertToMemory();
      }
      int status = 0;
      try
      {
        Guid clsid = encoder.Clsid;
        bool flag = false;
        if (this.rawData != null)
        {
          ImageCodecInfo encoder1 = this.RawFormat.FindEncoder();
          if (encoder1 != null && encoder1.Clsid == clsid)
          {
            using (FileStream fileStream = File.OpenWrite(filename))
            {
              fileStream.Write(this.rawData, 0, this.rawData.Length);
              flag = true;
            }
          }
        }
        if (!flag)
          status = SafeNativeMethods.Gdip.GdipSaveImageToFile(new HandleRef((object) this, this.nativeImage), filename, ref clsid, new HandleRef((object) encoderParams, num));
      }
      finally
      {
        if (num != IntPtr.Zero)
          Marshal.FreeHGlobal(num);
      }
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    internal void Save(MemoryStream stream)
    {
      ImageFormat imageFormat = this.RawFormat;
      if (imageFormat == ImageFormat.Jpeg)
        imageFormat = ImageFormat.Png;
      ImageCodecInfo encoder = imageFormat.FindEncoder() ?? ImageFormat.Png.FindEncoder();
      this.Save((Stream) stream, encoder, (EncoderParameters) null);
    }

    /// <summary>Saves this image to the specified stream in the specified format.</summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved.</param>
    /// <param name="format">An <see cref="T:System.Drawing.Imaging.ImageFormat" /> that specifies the format of the saved image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> or <paramref name="format" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format</exception>
    public void Save(Stream stream, ImageFormat format)
    {
      ImageCodecInfo encoder = format != null ? format.FindEncoder() : throw new ArgumentNullException(nameof (format));
      this.Save(stream, encoder, (EncoderParameters) null);
    }

    /// <summary>Saves this image to the specified stream, with the specified encoder and image encoder parameters.</summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved.</param>
    /// <param name="encoder">The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> for this <see cref="T:System.Drawing.Image" />.</param>
    /// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that specifies parameters used by the image encoder.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.</exception>
    public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      if (encoder == null)
        throw new ArgumentNullException(nameof (encoder));
      IntPtr num = IntPtr.Zero;
      if (encoderParams != null)
      {
        this.rawData = (byte[]) null;
        num = encoderParams.ConvertToMemory();
      }
      int status = 0;
      try
      {
        Guid clsid = encoder.Clsid;
        bool flag = false;
        if (this.rawData != null)
        {
          ImageCodecInfo encoder1 = this.RawFormat.FindEncoder();
          if (encoder1 != null && encoder1.Clsid == clsid)
          {
            stream.Write(this.rawData, 0, this.rawData.Length);
            flag = true;
          }
        }
        if (!flag)
          status = SafeNativeMethods.Gdip.GdipSaveImageToStream(new HandleRef((object) this, this.nativeImage), (UnsafeNativeMethods.IStream) new UnsafeNativeMethods.ComStreamFromDataStream(stream), ref clsid, new HandleRef((object) encoderParams, num));
      }
      finally
      {
        if (num != IntPtr.Zero)
          Marshal.FreeHGlobal(num);
      }
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a frame to the file or stream specified in a previous call to the <see cref="Overload:System.Drawing.Image.Save" /> method. Use this method to save selected frames from a multiple-frame image to another multiple-frame image.</summary>
    /// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that holds parameters required by the image encoder that is used by the save-add operation.</param>
    public void SaveAdd(EncoderParameters encoderParams)
    {
      IntPtr num = IntPtr.Zero;
      if (encoderParams != null)
        num = encoderParams.ConvertToMemory();
      this.rawData = (byte[]) null;
      int status = SafeNativeMethods.Gdip.GdipSaveAdd(new HandleRef((object) this, this.nativeImage), new HandleRef((object) encoderParams, num));
      if (num != IntPtr.Zero)
        Marshal.FreeHGlobal(num);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a frame to the file or stream specified in a previous call to the <see cref="Overload:System.Drawing.Image.Save" /> method.</summary>
    /// <param name="image">An <see cref="T:System.Drawing.Image" /> that contains the frame to add.</param>
    /// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that holds parameters required by the image encoder that is used by the save-add operation.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void SaveAdd(Image image, EncoderParameters encoderParams)
    {
      IntPtr num = IntPtr.Zero;
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (encoderParams != null)
        num = encoderParams.ConvertToMemory();
      this.rawData = (byte[]) null;
      int status = SafeNativeMethods.Gdip.GdipSaveAddImage(new HandleRef((object) this, this.nativeImage), new HandleRef((object) image, image.nativeImage), new HandleRef((object) encoderParams, num));
      if (num != IntPtr.Zero)
        Marshal.FreeHGlobal(num);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    private SizeF _GetPhysicalDimension()
    {
      float width;
      float height;
      int imageDimension = SafeNativeMethods.Gdip.GdipGetImageDimension(new HandleRef((object) this, this.nativeImage), out width, out height);
      if (imageDimension != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageDimension);
      return new SizeF(width, height);
    }

    /// <summary>Gets the width and height of this image.</summary>
    /// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that represents the width and height of this <see cref="T:System.Drawing.Image" />.</returns>
    public SizeF PhysicalDimension => this._GetPhysicalDimension();

    /// <summary>Gets the width and height, in pixels, of this image.</summary>
    /// <returns>A <see cref="T:System.Drawing.Size" /> structure that represents the width and height, in pixels, of this image.</returns>
    public Size Size => new Size(this.Width, this.Height);

    /// <summary>Gets the width, in pixels, of this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>The width, in pixels, of this <see cref="T:System.Drawing.Image" />.</returns>
    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Width
    {
      get
      {
        int width;
        int imageWidth = SafeNativeMethods.Gdip.GdipGetImageWidth(new HandleRef((object) this, this.nativeImage), out width);
        if (imageWidth != 0)
          throw SafeNativeMethods.Gdip.StatusException(imageWidth);
        return width;
      }
    }

    /// <summary>Gets the height, in pixels, of this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>The height, in pixels, of this <see cref="T:System.Drawing.Image" />.</returns>
    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Height
    {
      get
      {
        int height;
        int imageHeight = SafeNativeMethods.Gdip.GdipGetImageHeight(new HandleRef((object) this, this.nativeImage), out height);
        if (imageHeight != 0)
          throw SafeNativeMethods.Gdip.StatusException(imageHeight);
        return height;
      }
    }

    /// <summary>Gets the horizontal resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>The horizontal resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</returns>
    public float HorizontalResolution
    {
      get
      {
        float horzRes;
        int horizontalResolution = SafeNativeMethods.Gdip.GdipGetImageHorizontalResolution(new HandleRef((object) this, this.nativeImage), out horzRes);
        if (horizontalResolution != 0)
          throw SafeNativeMethods.Gdip.StatusException(horizontalResolution);
        return horzRes;
      }
    }

    /// <summary>Gets the vertical resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>The vertical resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</returns>
    public float VerticalResolution
    {
      get
      {
        float vertRes;
        int verticalResolution = SafeNativeMethods.Gdip.GdipGetImageVerticalResolution(new HandleRef((object) this, this.nativeImage), out vertRes);
        if (verticalResolution != 0)
          throw SafeNativeMethods.Gdip.StatusException(verticalResolution);
        return vertRes;
      }
    }

    /// <summary>Gets attribute flags for the pixel data of this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>The integer representing a bitwise combination of <see cref="T:System.Drawing.Imaging.ImageFlags" /> for this <see cref="T:System.Drawing.Image" />.</returns>
    [Browsable(false)]
    public int Flags
    {
      get
      {
        int flags;
        int imageFlags = SafeNativeMethods.Gdip.GdipGetImageFlags(new HandleRef((object) this, this.nativeImage), out flags);
        if (imageFlags != 0)
          throw SafeNativeMethods.Gdip.StatusException(imageFlags);
        return flags;
      }
    }

    /// <summary>Gets the file format of this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Imaging.ImageFormat" /> that represents the file format of this <see cref="T:System.Drawing.Image" />.</returns>
    public ImageFormat RawFormat
    {
      get
      {
        Guid format = new Guid();
        int imageRawFormat = SafeNativeMethods.Gdip.GdipGetImageRawFormat(new HandleRef((object) this, this.nativeImage), ref format);
        if (imageRawFormat != 0)
          throw SafeNativeMethods.Gdip.StatusException(imageRawFormat);
        return new ImageFormat(format);
      }
    }

    /// <summary>Gets the pixel format for this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Imaging.PixelFormat" /> that represents the pixel format for this <see cref="T:System.Drawing.Image" />.</returns>
    public PixelFormat PixelFormat
    {
      get
      {
        int format;
        return SafeNativeMethods.Gdip.GdipGetImagePixelFormat(new HandleRef((object) this, this.nativeImage), out format) != 0 ? PixelFormat.Undefined : (PixelFormat) format;
      }
    }

    /// <summary>Gets the bounds of the image in the specified unit.</summary>
    /// <param name="pageUnit">One of the <see cref="T:System.Drawing.GraphicsUnit" /> values indicating the unit of measure for the bounding rectangle.</param>
    /// <returns>The <see cref="T:System.Drawing.RectangleF" /> that represents the bounds of the image, in the specified unit.</returns>
    public RectangleF GetBounds(ref GraphicsUnit pageUnit)
    {
      GPRECTF gprectf = new GPRECTF();
      int imageBounds = SafeNativeMethods.Gdip.GdipGetImageBounds(new HandleRef((object) this, this.nativeImage), ref gprectf, out pageUnit);
      if (imageBounds != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageBounds);
      return gprectf.ToRectangleF();
    }

    private ColorPalette _GetColorPalette()
    {
      int size = -1;
      int imagePaletteSize = SafeNativeMethods.Gdip.GdipGetImagePaletteSize(new HandleRef((object) this, this.nativeImage), out size);
      if (imagePaletteSize != 0)
        throw SafeNativeMethods.Gdip.StatusException(imagePaletteSize);
      ColorPalette colorPalette = new ColorPalette(size);
      IntPtr num = Marshal.AllocHGlobal(size);
      int imagePalette = SafeNativeMethods.Gdip.GdipGetImagePalette(new HandleRef((object) this, this.nativeImage), num, size);
      try
      {
        if (imagePalette != 0)
          throw SafeNativeMethods.Gdip.StatusException(imagePalette);
        colorPalette.ConvertFromMemory(num);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
      return colorPalette;
    }

    private void _SetColorPalette(ColorPalette palette)
    {
      IntPtr memory = palette.ConvertToMemory();
      int status = SafeNativeMethods.Gdip.GdipSetImagePalette(new HandleRef((object) this, this.nativeImage), memory);
      if (memory != IntPtr.Zero)
        Marshal.FreeHGlobal(memory);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets or sets the color palette used for this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Imaging.ColorPalette" /> that represents the color palette used for this <see cref="T:System.Drawing.Image" />.</returns>
    [Browsable(false)]
    public ColorPalette Palette
    {
      get => this._GetColorPalette();
      set => this._SetColorPalette(value);
    }

    /// <summary>Returns a thumbnail for this <see cref="T:System.Drawing.Image" />.</summary>
    /// <param name="thumbWidth">The width, in pixels, of the requested thumbnail image.</param>
    /// <param name="thumbHeight">The height, in pixels, of the requested thumbnail image.</param>
    /// <param name="callback">A <see cref="T:System.Drawing.Image.GetThumbnailImageAbort" /> delegate.
    /// Note You must create a delegate and pass a reference to the delegate as the <paramref name="callback" /> parameter, but the delegate is not used.</param>
    /// <param name="callbackData">Must be <see cref="F:System.IntPtr.Zero" />.</param>
    /// <returns>An <see cref="T:System.Drawing.Image" /> that represents the thumbnail.</returns>
    public Image GetThumbnailImage(
      int thumbWidth,
      int thumbHeight,
      Image.GetThumbnailImageAbort callback,
      IntPtr callbackData)
    {
      IntPtr thumbImage = IntPtr.Zero;
      int imageThumbnail = SafeNativeMethods.Gdip.GdipGetImageThumbnail(new HandleRef((object) this, this.nativeImage), thumbWidth, thumbHeight, out thumbImage, callback, callbackData);
      if (imageThumbnail != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageThumbnail);
      return Image.CreateImageObject(thumbImage);
    }

    /// <summary>Gets an array of GUIDs that represent the dimensions of frames within this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>An array of GUIDs that specify the dimensions of frames within this <see cref="T:System.Drawing.Image" /> from most significant to least significant.</returns>
    [Browsable(false)]
    public Guid[] FrameDimensionsList
    {
      get
      {
        int count;
        int frameDimensionsCount = SafeNativeMethods.Gdip.GdipImageGetFrameDimensionsCount(new HandleRef((object) this, this.nativeImage), out count);
        if (frameDimensionsCount != 0)
          throw SafeNativeMethods.Gdip.StatusException(frameDimensionsCount);
        if (count <= 0)
          return new Guid[0];
        int num1 = Marshal.SizeOf(typeof (Guid));
        IntPtr num2 = Marshal.AllocHGlobal(checked (num1 * count));
        if (num2 == IntPtr.Zero)
          throw SafeNativeMethods.Gdip.StatusException(3);
        int frameDimensionsList1 = SafeNativeMethods.Gdip.GdipImageGetFrameDimensionsList(new HandleRef((object) this, this.nativeImage), num2, count);
        if (frameDimensionsList1 != 0)
        {
          Marshal.FreeHGlobal(num2);
          throw SafeNativeMethods.Gdip.StatusException(frameDimensionsList1);
        }
        Guid[] frameDimensionsList2 = new Guid[count];
        try
        {
          for (int index = 0; index < count; ++index)
            frameDimensionsList2[index] = (Guid) UnsafeNativeMethods.PtrToStructure((IntPtr) ((long) num2 + (long) (num1 * index)), typeof (Guid));
        }
        finally
        {
          Marshal.FreeHGlobal(num2);
        }
        return frameDimensionsList2;
      }
    }

    /// <summary>Returns the number of frames of the specified dimension.</summary>
    /// <param name="dimension">A <see cref="T:System.Drawing.Imaging.FrameDimension" /> that specifies the identity of the dimension type.</param>
    /// <returns>The number of frames in the specified dimension.</returns>
    public int GetFrameCount(FrameDimension dimension)
    {
      int[] count = new int[1];
      Guid guid = dimension.Guid;
      int frameCount = SafeNativeMethods.Gdip.GdipImageGetFrameCount(new HandleRef((object) this, this.nativeImage), ref guid, count);
      if (frameCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(frameCount);
      return count[0];
    }

    /// <summary>Selects the frame specified by the dimension and index.</summary>
    /// <param name="dimension">A <see cref="T:System.Drawing.Imaging.FrameDimension" /> that specifies the identity of the dimension type.</param>
    /// <param name="frameIndex">The index of the active frame.</param>
    /// <returns>Always returns 0.</returns>
    public int SelectActiveFrame(FrameDimension dimension, int frameIndex)
    {
      int[] numArray = new int[1];
      Guid guid = dimension.Guid;
      int status = SafeNativeMethods.Gdip.GdipImageSelectActiveFrame(new HandleRef((object) this, this.nativeImage), ref guid, frameIndex);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return numArray[0];
    }

    /// <summary>Rotates, flips, or rotates and flips the <see cref="T:System.Drawing.Image" />.</summary>
    /// <param name="rotateFlipType">A <see cref="T:System.Drawing.RotateFlipType" /> member that specifies the type of rotation and flip to apply to the image.</param>
    public void RotateFlip(RotateFlipType rotateFlipType)
    {
      int status = SafeNativeMethods.Gdip.GdipImageRotateFlip(new HandleRef((object) this, this.nativeImage), (int) rotateFlipType);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets IDs of the property items stored in this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>An array of the property IDs, one for each property item stored in this image.</returns>
    [Browsable(false)]
    public int[] PropertyIdList
    {
      get
      {
        int count;
        int propertyCount = SafeNativeMethods.Gdip.GdipGetPropertyCount(new HandleRef((object) this, this.nativeImage), out count);
        if (propertyCount != 0)
          throw SafeNativeMethods.Gdip.StatusException(propertyCount);
        int[] list = new int[count];
        if (count == 0)
          return list;
        int propertyIdList = SafeNativeMethods.Gdip.GdipGetPropertyIdList(new HandleRef((object) this, this.nativeImage), count, list);
        if (propertyIdList != 0)
          throw SafeNativeMethods.Gdip.StatusException(propertyIdList);
        return list;
      }
    }

    /// <summary>Gets the specified property item from this <see cref="T:System.Drawing.Image" />.</summary>
    /// <param name="propid">The ID of the property item to get.</param>
    /// <returns>The <see cref="T:System.Drawing.Imaging.PropertyItem" /> this method gets.</returns>
    /// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
    public PropertyItem GetPropertyItem(int propid)
    {
      int size;
      int propertyItemSize = SafeNativeMethods.Gdip.GdipGetPropertyItemSize(new HandleRef((object) this, this.nativeImage), propid, out size);
      if (propertyItemSize != 0)
        throw SafeNativeMethods.Gdip.StatusException(propertyItemSize);
      if (size == 0)
        return (PropertyItem) null;
      IntPtr num = Marshal.AllocHGlobal(size);
      if (num == IntPtr.Zero)
        throw SafeNativeMethods.Gdip.StatusException(3);
      int propertyItem = SafeNativeMethods.Gdip.GdipGetPropertyItem(new HandleRef((object) this, this.nativeImage), propid, size, num);
      try
      {
        if (propertyItem != 0)
          throw SafeNativeMethods.Gdip.StatusException(propertyItem);
        return PropertyItemInternal.ConvertFromMemory(num, 1)[0];
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Removes the specified property item from this <see cref="T:System.Drawing.Image" />.</summary>
    /// <param name="propid">The ID of the property item to remove.</param>
    /// <exception cref="T:System.ArgumentException">The image does not contain the requested property item.
    /// -or-
    /// The image format for this image does not support property items.</exception>
    public void RemovePropertyItem(int propid)
    {
      int status = SafeNativeMethods.Gdip.GdipRemovePropertyItem(new HandleRef((object) this, this.nativeImage), propid);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Stores a property item (piece of metadata) in this <see cref="T:System.Drawing.Image" />.</summary>
    /// <param name="propitem">The <see cref="T:System.Drawing.Imaging.PropertyItem" /> to be stored.</param>
    /// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
    public void SetPropertyItem(PropertyItem propitem)
    {
      PropertyItemInternal propitem1 = PropertyItemInternal.ConvertFromPropertyItem(propitem);
      using (propitem1)
      {
        int status = SafeNativeMethods.Gdip.GdipSetPropertyItem(new HandleRef((object) this, this.nativeImage), propitem1);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets all the property items (pieces of metadata) stored in this <see cref="T:System.Drawing.Image" />.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.Imaging.PropertyItem" /> objects, one for each property item stored in the image.</returns>
    [Browsable(false)]
    public PropertyItem[] PropertyItems
    {
      get
      {
        int count;
        int propertyCount = SafeNativeMethods.Gdip.GdipGetPropertyCount(new HandleRef((object) this, this.nativeImage), out count);
        if (propertyCount != 0)
          throw SafeNativeMethods.Gdip.StatusException(propertyCount);
        int totalSize;
        int propertySize = SafeNativeMethods.Gdip.GdipGetPropertySize(new HandleRef((object) this, this.nativeImage), out totalSize, ref count);
        if (propertySize != 0)
          throw SafeNativeMethods.Gdip.StatusException(propertySize);
        if (totalSize == 0 || count == 0)
          return new PropertyItem[0];
        IntPtr num = Marshal.AllocHGlobal(totalSize);
        int allPropertyItems = SafeNativeMethods.Gdip.GdipGetAllPropertyItems(new HandleRef((object) this, this.nativeImage), totalSize, count, num);
        try
        {
          if (allPropertyItems != 0)
            throw SafeNativeMethods.Gdip.StatusException(allPropertyItems);
          return PropertyItemInternal.ConvertFromMemory(num, count);
        }
        finally
        {
          Marshal.FreeHGlobal(num);
        }
      }
    }

    internal void SetNativeImage(IntPtr handle) => this.nativeImage = !(handle == IntPtr.Zero) ? handle : throw new ArgumentException(SR.GetString("NativeHandle0"), nameof (handle));

    /// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a handle to a GDI bitmap.</summary>
    /// <param name="hbitmap">The GDI bitmap handle from which to create the <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Bitmap" /> this method creates.</returns>
    public static Bitmap FromHbitmap(IntPtr hbitmap)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      return Image.FromHbitmap(hbitmap, IntPtr.Zero);
    }

    /// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a handle to a GDI bitmap and a handle to a GDI palette.</summary>
    /// <param name="hbitmap">The GDI bitmap handle from which to create the <see cref="T:System.Drawing.Bitmap" />.</param>
    /// <param name="hpalette">A handle to a GDI palette used to define the bitmap colors if the bitmap specified in the <paramref name="hbitmap" /> parameter is not a device-independent bitmap (DIB).</param>
    /// <returns>The <see cref="T:System.Drawing.Bitmap" /> this method creates.</returns>
    public static Bitmap FromHbitmap(IntPtr hbitmap, IntPtr hpalette)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr bitmap = IntPtr.Zero;
      int bitmapFromHbitmap = SafeNativeMethods.Gdip.GdipCreateBitmapFromHBITMAP(new HandleRef((object) null, hbitmap), new HandleRef((object) null, hpalette), out bitmap);
      if (bitmapFromHbitmap != 0)
        throw SafeNativeMethods.Gdip.StatusException(bitmapFromHbitmap);
      return Bitmap.FromGDIplus(bitmap);
    }

    /// <summary>Returns the color depth, in number of bits per pixel, of the specified pixel format.</summary>
    /// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> member that specifies the format for which to find the size.</param>
    /// <returns>The color depth of the specified pixel format.</returns>
    public static int GetPixelFormatSize(PixelFormat pixfmt) => (int) pixfmt >> 8 & (int) byte.MaxValue;

    /// <summary>Returns a value that indicates whether the pixel format for this <see cref="T:System.Drawing.Image" /> contains alpha information.</summary>
    /// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> to test.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="pixfmt" /> contains alpha information; otherwise, <see langword="false" />.</returns>
    public static bool IsAlphaPixelFormat(PixelFormat pixfmt) => (pixfmt & PixelFormat.Alpha) != 0;

    /// <summary>Returns a value that indicates whether the pixel format is 64 bits per pixel.</summary>
    /// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration to test.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="pixfmt" /> is extended; otherwise, <see langword="false" />.</returns>
    public static bool IsExtendedPixelFormat(PixelFormat pixfmt) => (pixfmt & PixelFormat.Extended) != 0;

    /// <summary>Returns a value that indicates whether the pixel format is 32 bits per pixel.</summary>
    /// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> to test.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="pixfmt" /> is canonical; otherwise, <see langword="false" />.</returns>
    public static bool IsCanonicalPixelFormat(PixelFormat pixfmt) => (pixfmt & PixelFormat.Canonical) != 0;

    /// <summary>Provides a callback method for determining when the <see cref="M:System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" /> method should prematurely cancel execution.</summary>
    /// <returns>This method returns <see langword="true" /> if it decides that the <see cref="M:System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" /> method should prematurely stop execution; otherwise, it returns <see langword="false" />.</returns>
    public delegate bool GetThumbnailImageAbort();

    private enum ImageTypeEnum
    {
      Bitmap = 1,
      Metafile = 2,
    }
  }
}
