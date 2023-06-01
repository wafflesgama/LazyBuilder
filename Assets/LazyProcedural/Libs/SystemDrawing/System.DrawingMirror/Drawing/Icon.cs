// Decompiled with JetBrains decompiler
// Type: System.Drawing.Icon
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
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Drawing
{
  /// <summary>Represents a Windows icon, which is a small bitmap image that is used to represent an object. Icons can be thought of as transparent bitmaps, although their size is determined by the system.</summary>
  [TypeConverter(typeof (IconConverter))]
  [Editor("System.Drawing.Design.IconEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [Serializable]
  public sealed class Icon : MarshalByRefObject, ISerializable, ICloneable, IDisposable
  {
    private static int bitDepth;
    private const int PNGSignature1 = 1196314761;
    private const int PNGSignature2 = 169478669;
    private byte[] iconData;
    private int bestImageOffset;
    private int bestBitDepth;
    private int bestBytesInRes;
    private bool? isBestImagePng;
    private Size iconSize = Size.Empty;
    private IntPtr handle = IntPtr.Zero;
    private bool ownHandle = true;

    private Icon()
    {
    }

    internal Icon(IntPtr handle)
      : this(handle, false)
    {
    }

    internal Icon(IntPtr handle, bool takeOwnership)
    {
      this.handle = !(handle == IntPtr.Zero) ? handle : throw new ArgumentException(SR.GetString("InvalidGDIHandle", (object) typeof (Icon).Name));
      this.ownHandle = takeOwnership;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified file name.</summary>
    /// <param name="fileName">The file to load the <see cref="T:System.Drawing.Icon" /> from.</param>
    public Icon(string fileName)
      : this(fileName, 0, 0)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class of the specified size from the specified file.</summary>
    /// <param name="fileName">The name and path to the file that contains the icon data.</param>
    /// <param name="size">The desired size of the icon.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="string" /> is <see langword="null" /> or does not contain image data.</exception>
    public Icon(string fileName, Size size)
      : this(fileName, size.Width, size.Height)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class with the specified width and height from the specified file.</summary>
    /// <param name="fileName">The name and path to the file that contains the <see cref="T:System.Drawing.Icon" /> data.</param>
    /// <param name="width">The desired width of the <see cref="T:System.Drawing.Icon" />.</param>
    /// <param name="height">The desired height of the <see cref="T:System.Drawing.Icon" />.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="string" /> is <see langword="null" /> or does not contain image data.</exception>
    public Icon(string fileName, int width, int height)
      : this()
    {
      using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        this.iconData = new byte[(int) fileStream.Length];
        fileStream.Read(this.iconData, 0, this.iconData.Length);
      }
      this.Initialize(width, height);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class and attempts to find a version of the icon that matches the requested size.</summary>
    /// <param name="original">The <see cref="T:System.Drawing.Icon" /> from which to load the newly sized icon.</param>
    /// <param name="size">A <see cref="T:System.Drawing.Size" /> structure that specifies the height and width of the new <see cref="T:System.Drawing.Icon" />.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="original" /> parameter is <see langword="null" />.</exception>
    public Icon(Icon original, Size size)
      : this(original, size.Width, size.Height)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class and attempts to find a version of the icon that matches the requested size.</summary>
    /// <param name="original">The icon to load the different size from.</param>
    /// <param name="width">The width of the new icon.</param>
    /// <param name="height">The height of the new icon.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="original" /> parameter is <see langword="null" />.</exception>
    public Icon(Icon original, int width, int height)
      : this()
    {
      this.iconData = original != null ? original.iconData : throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (original), (object) "null"));
      if (this.iconData == null)
      {
        this.iconSize = original.Size;
        this.handle = SafeNativeMethods.CopyImage(new HandleRef((object) original, original.Handle), 1, this.iconSize.Width, this.iconSize.Height, 0);
      }
      else
        this.Initialize(width, height);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from a resource in the specified assembly.</summary>
    /// <param name="type">A <see cref="T:System.Type" /> that specifies the assembly in which to look for the resource.</param>
    /// <param name="resource">The resource name to load.</param>
    /// <exception cref="T:System.ArgumentException">An icon specified by <paramref name="resource" /> cannot be found in the assembly that contains the specified <paramref name="type" />.</exception>
    public Icon(Type type, string resource)
      : this()
    {
      Stream manifestResourceStream = type.Module.Assembly.GetManifestResourceStream(type, resource);
      this.iconData = manifestResourceStream != null ? new byte[(int) manifestResourceStream.Length] : throw new ArgumentException(SR.GetString("ResourceNotFound", (object) type, (object) resource));
      manifestResourceStream.Read(this.iconData, 0, this.iconData.Length);
      this.Initialize(0, 0);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified data stream.</summary>
    /// <param name="stream">The data stream from which to load the <see cref="T:System.Drawing.Icon" />.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
    public Icon(Stream stream)
      : this(stream, 0, 0)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class of the specified size from the specified stream.</summary>
    /// <param name="stream">The stream that contains the icon data.</param>
    /// <param name="size">The desired size of the icon.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> is <see langword="null" /> or does not contain image data.</exception>
    public Icon(Stream stream, Size size)
      : this(stream, size.Width, size.Height)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified data stream and with the specified width and height.</summary>
    /// <param name="stream">The data stream from which to load the icon.</param>
    /// <param name="width">The width, in pixels, of the icon.</param>
    /// <param name="height">The height, in pixels, of the icon.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
    public Icon(Stream stream, int width, int height)
      : this()
    {
      this.iconData = stream != null ? new byte[(int) stream.Length] : throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (stream), (object) "null"));
      stream.Read(this.iconData, 0, this.iconData.Length);
      this.Initialize(width, height);
    }

    private Icon(SerializationInfo info, StreamingContext context)
    {
      this.iconData = (byte[]) info.GetValue("IconData", typeof (byte[]));
      this.iconSize = (Size) info.GetValue("IconSize", typeof (Size));
      if (this.iconSize.IsEmpty)
        this.Initialize(0, 0);
      else
        this.Initialize(this.iconSize.Width, this.iconSize.Height);
    }

    /// <summary>Returns an icon representation of an image that is contained in the specified file.</summary>
    /// <param name="filePath">The path to the file that contains an image.</param>
    /// <returns>The <see cref="T:System.Drawing.Icon" /> representation of the image that is contained in the specified file.</returns>
    /// <exception cref="T:System.ArgumentException">The <paramref name="filePath" /> does not indicate a valid file.
    /// -or-
    /// The <paramref name="filePath" /> indicates a Universal Naming Convention (UNC) path.</exception>
    public static Icon ExtractAssociatedIcon(string filePath) => Icon.ExtractAssociatedIcon(filePath, 0);

    private static Icon ExtractAssociatedIcon(string filePath, int index)
    {
      if (filePath == null)
        throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (filePath), (object) "null"));
      Uri uri;
      try
      {
        uri = new Uri(filePath);
      }
      catch (UriFormatException ex)
      {
        filePath = Path.GetFullPath(filePath);
        uri = new Uri(filePath);
      }
      if (uri.IsUnc)
        throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (filePath), (object) filePath));
      if (uri.IsFile)
      {
        if (!File.Exists(filePath))
        {
          IntSecurity.DemandReadFileIO(filePath);
          throw new FileNotFoundException(filePath);
        }
        Icon icon = new Icon();
        StringBuilder iconPath = new StringBuilder(260);
        iconPath.Append(filePath);
        IntPtr associatedIcon = SafeNativeMethods.ExtractAssociatedIcon(NativeMethods.NullHandleRef, iconPath, ref index);
        if (associatedIcon != IntPtr.Zero)
        {
          IntSecurity.ObjectFromWin32Handle.Demand();
          return new Icon(associatedIcon, true);
        }
      }
      return (Icon) null;
    }

    /// <summary>Gets the Windows handle for this <see cref="T:System.Drawing.Icon" />. This is not a copy of the handle; do not free it.</summary>
    /// <returns>The Windows handle for the icon.</returns>
    [Browsable(false)]
    public IntPtr Handle => !(this.handle == IntPtr.Zero) ? this.handle : throw new ObjectDisposedException(this.GetType().Name);

    /// <summary>Gets the height of this <see cref="T:System.Drawing.Icon" />.</summary>
    /// <returns>The height of this <see cref="T:System.Drawing.Icon" />.</returns>
    [Browsable(false)]
    public int Height => this.Size.Height;

    /// <summary>Gets the size of this <see cref="T:System.Drawing.Icon" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Size" /> structure that specifies the width and height of this <see cref="T:System.Drawing.Icon" />.</returns>
    public Size Size
    {
      get
      {
        if (this.iconSize.IsEmpty)
        {
          SafeNativeMethods.ICONINFO info = new SafeNativeMethods.ICONINFO();
          SafeNativeMethods.GetIconInfo(new HandleRef((object) this, this.Handle), info);
          SafeNativeMethods.BITMAP bm = new SafeNativeMethods.BITMAP();
          if (info.hbmColor != IntPtr.Zero)
          {
            SafeNativeMethods.GetObject(new HandleRef((object) null, info.hbmColor), Marshal.SizeOf(typeof (SafeNativeMethods.BITMAP)), bm);
            SafeNativeMethods.IntDeleteObject(new HandleRef((object) null, info.hbmColor));
            this.iconSize = new Size(bm.bmWidth, bm.bmHeight);
          }
          else if (info.hbmMask != IntPtr.Zero)
          {
            SafeNativeMethods.GetObject(new HandleRef((object) null, info.hbmMask), Marshal.SizeOf(typeof (SafeNativeMethods.BITMAP)), bm);
            this.iconSize = new Size(bm.bmWidth, bm.bmHeight / 2);
          }
          if (info.hbmMask != IntPtr.Zero)
            SafeNativeMethods.IntDeleteObject(new HandleRef((object) null, info.hbmMask));
        }
        return this.iconSize;
      }
    }

    /// <summary>Gets the width of this <see cref="T:System.Drawing.Icon" />.</summary>
    /// <returns>The width of this <see cref="T:System.Drawing.Icon" />.</returns>
    [Browsable(false)]
    public int Width => this.Size.Width;

    /// <summary>Clones the <see cref="T:System.Drawing.Icon" />, creating a duplicate image.</summary>
    /// <returns>An object that can be cast to an <see cref="T:System.Drawing.Icon" />.</returns>
    public object Clone()
    {
      Size size = this.Size;
      int width = size.Width;
      size = this.Size;
      int height = size.Height;
      return (object) new Icon(this, width, height);
    }

    internal void DestroyHandle()
    {
      if (!this.ownHandle)
        return;
      SafeNativeMethods.DestroyIcon(new HandleRef((object) this, this.handle));
      this.handle = IntPtr.Zero;
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Icon" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.handle != IntPtr.Zero))
        return;
      this.DestroyHandle();
    }

    private void DrawIcon(IntPtr dc, Rectangle imageRect, Rectangle targetRect, bool stretch)
    {
      int num1 = 0;
      int num2 = 0;
      int x1 = 0;
      int y1 = 0;
      Size size = this.Size;
      int width1;
      int height1;
      if (!imageRect.IsEmpty)
      {
        num1 = imageRect.X;
        num2 = imageRect.Y;
        width1 = imageRect.Width;
        height1 = imageRect.Height;
      }
      else
      {
        width1 = size.Width;
        height1 = size.Height;
      }
      int width2;
      int height2;
      if (!targetRect.IsEmpty)
      {
        x1 = targetRect.X;
        y1 = targetRect.Y;
        width2 = targetRect.Width;
        height2 = targetRect.Height;
      }
      else
      {
        width2 = size.Width;
        height2 = size.Height;
      }
      int width3;
      int height3;
      int num3;
      int num4;
      if (stretch)
      {
        width3 = size.Width * width2 / width1;
        height3 = size.Height * height2 / height1;
        num3 = width2;
        num4 = height2;
      }
      else
      {
        width3 = size.Width;
        height3 = size.Height;
        num3 = width2 < width1 ? width2 : width1;
        num4 = height2 < height1 ? height2 : height1;
      }
      IntPtr hRgn = SafeNativeMethods.SaveClipRgn(dc);
      try
      {
        SafeNativeMethods.IntersectClipRect(new HandleRef((object) this, dc), x1, y1, x1 + num3, y1 + num4);
        SafeNativeMethods.DrawIconEx(new HandleRef((object) null, dc), x1 - num1, y1 - num2, new HandleRef((object) this, this.handle), width3, height3, 0, NativeMethods.NullHandleRef, 3);
      }
      finally
      {
        SafeNativeMethods.RestoreClipRgn(dc, hRgn);
      }
    }

    internal void Draw(Graphics graphics, int x, int y)
    {
      Size size = this.Size;
      this.Draw(graphics, new Rectangle(x, y, size.Width, size.Height));
    }

    internal void Draw(Graphics graphics, Rectangle targetRect)
    {
      Rectangle targetRect1 = targetRect;
      targetRect1.X += (int) graphics.Transform.OffsetX;
      targetRect1.Y += (int) graphics.Transform.OffsetY;
      WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics, ApplyGraphicsProperties.Clipping);
      IntPtr hdc = windowsGraphics.GetHdc();
      try
      {
        this.DrawIcon(hdc, Rectangle.Empty, targetRect1, true);
      }
      finally
      {
        windowsGraphics.Dispose();
      }
    }

    internal void DrawUnstretched(Graphics graphics, Rectangle targetRect)
    {
      Rectangle targetRect1 = targetRect;
      targetRect1.X += (int) graphics.Transform.OffsetX;
      targetRect1.Y += (int) graphics.Transform.OffsetY;
      WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics, ApplyGraphicsProperties.Clipping);
      IntPtr hdc = windowsGraphics.GetHdc();
      try
      {
        this.DrawIcon(hdc, Rectangle.Empty, targetRect1, false);
      }
      finally
      {
        windowsGraphics.Dispose();
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~Icon() => this.Dispose(false);

    /// <summary>Creates a GDI+ <see cref="T:System.Drawing.Icon" /> from the specified Windows handle to an icon (<see langword="HICON" />).</summary>
    /// <param name="handle">A Windows handle to an icon.</param>
    /// <returns>The <see cref="T:System.Drawing.Icon" /> this method creates.</returns>
    public static Icon FromHandle(IntPtr handle)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      return new Icon(handle);
    }

    private unsafe short GetShort(byte* pb)
    {
      int num1;
      if (((int) (byte) pb & 1) != 0)
      {
        int num2 = (int) *pb;
        ++pb;
        num1 = num2 | (int) *pb << 8;
      }
      else
        num1 = (int) *(short*) pb;
      return (short) num1;
    }

    private unsafe int GetInt(byte* pb)
    {
      int num1;
      if (((int) (byte) pb & 3) != 0)
      {
        int num2 = (int) *pb;
        ++pb;
        int num3 = num2 | (int) *pb << 8;
        ++pb;
        int num4 = num3 | (int) *pb << 16;
        ++pb;
        num1 = num4 | (int) *pb << 24;
      }
      else
        num1 = *(int*) pb;
      return num1;
    }

    private unsafe void Initialize(int width, int height)
    {
      if (this.iconData == null || this.handle != IntPtr.Zero)
        throw new InvalidOperationException(SR.GetString("IllegalState", (object) this.GetType().Name));
      int num1 = Marshal.SizeOf(typeof (SafeNativeMethods.ICONDIR));
      if (this.iconData.Length < num1)
        throw new ArgumentException(SR.GetString("InvalidPictureType", (object) "picture", (object) nameof (Icon)));
      if (width == 0)
        width = UnsafeNativeMethods.GetSystemMetrics(11);
      if (height == 0)
        height = UnsafeNativeMethods.GetSystemMetrics(12);
      if (Icon.bitDepth == 0)
      {
        IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
        Icon.bitDepth = UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) null, dc), 12);
        Icon.bitDepth *= UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) null, dc), 14);
        UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef((object) null, dc));
        if (Icon.bitDepth == 8)
          Icon.bitDepth = 4;
      }
      fixed (byte* pb = this.iconData)
      {
        short num2 = this.GetShort(pb);
        short num3 = this.GetShort(pb + 2);
        short num4 = this.GetShort(pb + 4);
        if (num2 != (short) 0 || num3 != (short) 1 || num4 == (short) 0)
          throw new ArgumentException(SR.GetString("InvalidPictureType", (object) "picture", (object) nameof (Icon)));
        byte num5 = 0;
        byte num6 = 0;
        byte* numPtr = pb + 6;
        int num7 = Marshal.SizeOf(typeof (SafeNativeMethods.ICONDIRENTRY));
        if (num7 * ((int) num4 - 1) + num1 > this.iconData.Length)
          throw new ArgumentException(SR.GetString("InvalidPictureType", (object) "picture", (object) nameof (Icon)));
        for (int index = 0; index < (int) num4; ++index)
        {
          SafeNativeMethods.ICONDIRENTRY icondirentry;
          icondirentry.bWidth = *numPtr;
          icondirentry.bHeight = numPtr[1];
          icondirentry.bColorCount = numPtr[2];
          icondirentry.bReserved = numPtr[3];
          icondirentry.wPlanes = this.GetShort(numPtr + 4);
          icondirentry.wBitCount = this.GetShort(numPtr + 6);
          icondirentry.dwBytesInRes = this.GetInt(numPtr + 8);
          icondirentry.dwImageOffset = this.GetInt(numPtr + 12);
          bool flag = false;
          int num8;
          if (icondirentry.bColorCount != (byte) 0)
          {
            num8 = 4;
            if (icondirentry.bColorCount < (byte) 16)
              num8 = 1;
          }
          else
            num8 = (int) icondirentry.wBitCount;
          if (num8 == 0)
            num8 = 8;
          if (this.bestBytesInRes == 0)
          {
            flag = true;
          }
          else
          {
            int num9 = Math.Abs((int) num5 - width) + Math.Abs((int) num6 - height);
            int num10 = Math.Abs((int) icondirentry.bWidth - width) + Math.Abs((int) icondirentry.bHeight - height);
            if (num10 < num9 || num10 == num9 && (num8 <= Icon.bitDepth && num8 > this.bestBitDepth || this.bestBitDepth > Icon.bitDepth && num8 < this.bestBitDepth))
              flag = true;
          }
          if (flag)
          {
            num5 = icondirentry.bWidth;
            num6 = icondirentry.bHeight;
            this.bestImageOffset = icondirentry.dwImageOffset;
            this.bestBytesInRes = icondirentry.dwBytesInRes;
            this.bestBitDepth = num8;
          }
          numPtr += num7;
        }
        if (this.bestImageOffset < 0)
          throw new ArgumentException(SR.GetString("InvalidPictureType", (object) "picture", (object) nameof (Icon)));
        if (this.bestBytesInRes < 0)
          throw new Win32Exception(87);
        int num11;
        try
        {
          num11 = checked (this.bestImageOffset + this.bestBytesInRes);
        }
        catch (OverflowException ex)
        {
          throw new Win32Exception(87);
        }
        if (num11 > this.iconData.Length)
          throw new ArgumentException(SR.GetString("InvalidPictureType", (object) "picture", (object) nameof (Icon)));
        if (this.bestImageOffset % IntPtr.Size != 0)
        {
          byte[] destinationArray = new byte[this.bestBytesInRes];
          Array.Copy((Array) this.iconData, this.bestImageOffset, (Array) destinationArray, 0, this.bestBytesInRes);
          fixed (byte* pbIconBits = destinationArray)
            this.handle = SafeNativeMethods.CreateIconFromResourceEx(pbIconBits, this.bestBytesInRes, true, 196608, 0, 0, 0);
        }
        else
        {
          try
          {
            this.handle = SafeNativeMethods.CreateIconFromResourceEx((byte*) checked (unchecked ((UIntPtr) pb) + unchecked ((UIntPtr) this.bestImageOffset)), this.bestBytesInRes, true, 196608, 0, 0, 0);
          }
          catch (OverflowException ex)
          {
            throw new Win32Exception(87);
          }
        }
        if (this.handle == IntPtr.Zero)
          throw new Win32Exception();
        // ISSUE: __unpin statement
        __unpin(pb);
      }
    }

    /// <summary>Saves this <see cref="T:System.Drawing.Icon" /> to the specified output <see cref="T:System.IO.Stream" />.</summary>
    /// <param name="outputStream">The <see cref="T:System.IO.Stream" /> to save to.</param>
    public void Save(Stream outputStream)
    {
      if (this.iconData != null)
      {
        outputStream.Write(this.iconData, 0, this.iconData.Length);
      }
      else
      {
        SafeNativeMethods.PICTDESC iconPictdesc = SafeNativeMethods.PICTDESC.CreateIconPICTDESC(this.Handle);
        Guid guid = typeof (SafeNativeMethods.IPicture).GUID;
        SafeNativeMethods.IPicture pictureIndirect = SafeNativeMethods.OleCreatePictureIndirect(iconPictdesc, ref guid, false);
        if (pictureIndirect == null)
          return;
        try
        {
          pictureIndirect.SaveAsFile((UnsafeNativeMethods.IStream) new UnsafeNativeMethods.ComStreamFromDataStream(outputStream), -1, out int _);
        }
        finally
        {
          Marshal.ReleaseComObject((object) pictureIndirect);
        }
      }
    }

    private void CopyBitmapData(BitmapData sourceData, BitmapData targetData)
    {
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < Math.Min(sourceData.Height, targetData.Height); ++index)
      {
        IntPtr handle1;
        IntPtr handle2;
        if (IntPtr.Size == 4)
        {
          handle1 = new IntPtr(sourceData.Scan0.ToInt32() + num1);
          handle2 = new IntPtr(targetData.Scan0.ToInt32() + num2);
        }
        else
        {
          handle1 = new IntPtr(sourceData.Scan0.ToInt64() + (long) num1);
          handle2 = new IntPtr(targetData.Scan0.ToInt64() + (long) num2);
        }
        UnsafeNativeMethods.CopyMemory(new HandleRef((object) this, handle2), new HandleRef((object) this, handle1), Math.Abs(targetData.Stride));
        num1 += sourceData.Stride;
        num2 += targetData.Stride;
      }
    }

    private static unsafe bool BitmapHasAlpha(BitmapData bmpData)
    {
      bool flag = false;
      for (int index1 = 0; index1 < bmpData.Height; ++index1)
      {
        for (int index2 = 3; index2 < Math.Abs(bmpData.Stride); index2 += 4)
        {
          if (*(byte*) ((IntPtr) bmpData.Scan0.ToPointer() + index1 * bmpData.Stride + index2) != (byte) 0)
          {
            flag = true;
            goto label_8;
          }
        }
      }
label_8:
      return flag;
    }

    /// <summary>Converts this <see cref="T:System.Drawing.Icon" /> to a GDI+ <see cref="T:System.Drawing.Bitmap" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the converted <see cref="T:System.Drawing.Icon" />.</returns>
    public Bitmap ToBitmap() => this.HasPngSignature() && !LocalAppContextSwitches.DontSupportPngFramesInIcons ? this.PngFrame() : this.BmpFrame();

    private unsafe Bitmap BmpFrame()
    {
      Bitmap bitmap1 = (Bitmap) null;
      if (this.iconData != null && this.bestBitDepth == 32)
      {
        int width1 = this.Size.Width;
        Size size = this.Size;
        int height1 = size.Height;
        bitmap1 = new Bitmap(width1, height1, PixelFormat.Format32bppArgb);
        Bitmap bitmap2 = bitmap1;
        size = this.Size;
        int width2 = size.Width;
        size = this.Size;
        int height2 = size.Height;
        Rectangle rect = new Rectangle(0, 0, width2, height2);
        BitmapData bitmapdata = bitmap2.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        try
        {
          uint* pointer = (uint*) bitmapdata.Scan0.ToPointer();
          int num = this.bestImageOffset + Marshal.SizeOf(typeof (SafeNativeMethods.BITMAPINFOHEADER));
          size = this.Size;
          int length = size.Width * 4;
          size = this.Size;
          int width3 = size.Width;
          size = this.Size;
          for (int index = (size.Height - 1) * 4; index >= 0; index -= 4)
          {
            Marshal.Copy(this.iconData, num + index * width3, (IntPtr) (void*) pointer, length);
            pointer += width3;
          }
        }
        finally
        {
          bitmap1.UnlockBits(bitmapdata);
        }
      }
      else if (this.bestBitDepth == 0 || this.bestBitDepth == 32)
      {
        SafeNativeMethods.ICONINFO info = new SafeNativeMethods.ICONINFO();
        SafeNativeMethods.GetIconInfo(new HandleRef((object) this, this.handle), info);
        SafeNativeMethods.BITMAP bm = new SafeNativeMethods.BITMAP();
        try
        {
          if (info.hbmColor != IntPtr.Zero)
          {
            SafeNativeMethods.GetObject(new HandleRef((object) null, info.hbmColor), Marshal.SizeOf(typeof (SafeNativeMethods.BITMAP)), bm);
            if (bm.bmBitsPixel == (short) 32)
            {
              Bitmap bitmap3 = (Bitmap) null;
              BitmapData bitmapData1 = (BitmapData) null;
              BitmapData bitmapData2 = (BitmapData) null;
              IntSecurity.ObjectFromWin32Handle.Assert();
              try
              {
                bitmap3 = Image.FromHbitmap(info.hbmColor);
                bitmapData1 = bitmap3.LockBits(new Rectangle(0, 0, bitmap3.Width, bitmap3.Height), ImageLockMode.ReadOnly, bitmap3.PixelFormat);
                if (Icon.BitmapHasAlpha(bitmapData1))
                {
                  bitmap1 = new Bitmap(bitmapData1.Width, bitmapData1.Height, PixelFormat.Format32bppArgb);
                  bitmapData2 = bitmap1.LockBits(new Rectangle(0, 0, bitmapData1.Width, bitmapData1.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                  this.CopyBitmapData(bitmapData1, bitmapData2);
                }
              }
              finally
              {
                CodeAccessPermission.RevertAssert();
                if (bitmap3 != null && bitmapData1 != null)
                  bitmap3.UnlockBits(bitmapData1);
                if (bitmap1 != null && bitmapData2 != null)
                  bitmap1.UnlockBits(bitmapData2);
              }
              bitmap3.Dispose();
            }
          }
        }
        finally
        {
          if (info.hbmColor != IntPtr.Zero)
            SafeNativeMethods.IntDeleteObject(new HandleRef((object) null, info.hbmColor));
          if (info.hbmMask != IntPtr.Zero)
            SafeNativeMethods.IntDeleteObject(new HandleRef((object) null, info.hbmMask));
        }
      }
      if (bitmap1 == null)
      {
        Size size = this.Size;
        bitmap1 = new Bitmap(size.Width, size.Height);
        Graphics graphics = (Graphics) null;
        try
        {
          graphics = Graphics.FromImage((Image) bitmap1);
          IntSecurity.ObjectFromWin32Handle.Assert();
          try
          {
            using (Bitmap bitmap4 = Bitmap.FromHicon(this.Handle))
              graphics.DrawImage((Image) bitmap4, new Rectangle(0, 0, size.Width, size.Height));
          }
          catch (ArgumentException ex)
          {
            this.Draw(graphics, new Rectangle(0, 0, size.Width, size.Height));
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
        }
        finally
        {
          graphics?.Dispose();
        }
        Color transparentColor = Color.FromArgb(13, 11, 12);
        bitmap1.MakeTransparent(transparentColor);
      }
      return bitmap1;
    }

    private Bitmap PngFrame()
    {
      Bitmap bitmap = (Bitmap) null;
      if (this.iconData != null)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          memoryStream.Write(this.iconData, this.bestImageOffset, this.bestBytesInRes);
          bitmap = new Bitmap((Stream) memoryStream);
        }
      }
      return bitmap;
    }

    private bool HasPngSignature()
    {
      if (!this.isBestImagePng.HasValue)
      {
        if (this.iconData != null && this.iconData.Length >= this.bestImageOffset + 8)
        {
          int int32_1 = BitConverter.ToInt32(this.iconData, this.bestImageOffset);
          int int32_2 = BitConverter.ToInt32(this.iconData, this.bestImageOffset + 4);
          this.isBestImagePng = new bool?(int32_1 == 1196314761 && int32_2 == 169478669);
        }
        else
          this.isBestImagePng = new bool?(false);
      }
      return this.isBestImagePng.Value;
    }

    /// <summary>Gets a human-readable string that describes the <see cref="T:System.Drawing.Icon" />.</summary>
    /// <returns>A string that describes the <see cref="T:System.Drawing.Icon" />.</returns>
    public override string ToString() => SR.GetString("toStringIcon");

    /// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is required to serialize the target object.</summary>
    /// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
    /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
    {
      if (this.iconData != null)
      {
        si.AddValue("IconData", (object) this.iconData, typeof (byte[]));
      }
      else
      {
        MemoryStream outputStream = new MemoryStream();
        this.Save((Stream) outputStream);
        si.AddValue("IconData", (object) outputStream.ToArray(), typeof (byte[]));
      }
      si.AddValue("IconSize", (object) this.iconSize, typeof (Size));
    }
  }
}
