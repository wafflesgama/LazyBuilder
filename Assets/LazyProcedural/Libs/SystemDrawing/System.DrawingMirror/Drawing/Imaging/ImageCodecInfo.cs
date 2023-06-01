// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ImageCodecInfo
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Drawing.Imaging
{
  /// <summary>The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> class provides the necessary storage members and methods to retrieve all pertinent information about the installed image encoders and decoders (called codecs). Not inheritable.</summary>
  public sealed class ImageCodecInfo
  {
    private Guid clsid;
    private Guid formatID;
    private string codecName;
    private string dllName;
    private string formatDescription;
    private string filenameExtension;
    private string mimeType;
    private ImageCodecFlags flags;
    private int version;
    private byte[][] signaturePatterns;
    private byte[][] signatureMasks;

    internal ImageCodecInfo()
    {
    }

    /// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</summary>
    /// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</returns>
    public Guid Clsid
    {
      get => this.clsid;
      set => this.clsid = value;
    }

    /// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</summary>
    /// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</returns>
    public Guid FormatID
    {
      get => this.formatID;
      set => this.formatID = value;
    }

    /// <summary>Gets or sets a string that contains the name of the codec.</summary>
    /// <returns>A string that contains the name of the codec.</returns>
    public string CodecName
    {
      get => this.codecName;
      set => this.codecName = value;
    }

    /// <summary>Gets or sets string that contains the path name of the DLL that holds the codec. If the codec is not in a DLL, this pointer is <see langword="null" />.</summary>
    /// <returns>A string that contains the path name of the DLL that holds the codec.</returns>
    public string DllName
    {
      get
      {
        if (this.dllName != null)
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.dllName).Demand();
        return this.dllName;
      }
      set
      {
        if (value != null)
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, value).Demand();
        this.dllName = value;
      }
    }

    /// <summary>Gets or sets a string that describes the codec's file format.</summary>
    /// <returns>A string that describes the codec's file format.</returns>
    public string FormatDescription
    {
      get => this.formatDescription;
      set => this.formatDescription = value;
    }

    /// <summary>Gets or sets string that contains the file name extension(s) used in the codec. The extensions are separated by semicolons.</summary>
    /// <returns>A string that contains the file name extension(s) used in the codec.</returns>
    public string FilenameExtension
    {
      get => this.filenameExtension;
      set => this.filenameExtension = value;
    }

    /// <summary>Gets or sets a string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</summary>
    /// <returns>A string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</returns>
    public string MimeType
    {
      get => this.mimeType;
      set => this.mimeType = value;
    }

    /// <summary>Gets or sets 32-bit value used to store additional information about the codec. This property returns a combination of flags from the <see cref="T:System.Drawing.Imaging.ImageCodecFlags" /> enumeration.</summary>
    /// <returns>A 32-bit value used to store additional information about the codec.</returns>
    public ImageCodecFlags Flags
    {
      get => this.flags;
      set => this.flags = value;
    }

    /// <summary>Gets or sets the version number of the codec.</summary>
    /// <returns>The version number of the codec.</returns>
    public int Version
    {
      get => this.version;
      set => this.version = value;
    }

    /// <summary>Gets or sets a two dimensional array of bytes that represents the signature of the codec.</summary>
    /// <returns>A two dimensional array of bytes that represents the signature of the codec.</returns>
    [CLSCompliant(false)]
    public byte[][] SignaturePatterns
    {
      get => this.signaturePatterns;
      set => this.signaturePatterns = value;
    }

    /// <summary>Gets or sets a two dimensional array of bytes that can be used as a filter.</summary>
    /// <returns>A two dimensional array of bytes that can be used as a filter.</returns>
    [CLSCompliant(false)]
    public byte[][] SignatureMasks
    {
      get => this.signatureMasks;
      set => this.signatureMasks = value;
    }

    /// <summary>Returns an array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects that contain information about the image decoders built into GDI+.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects. Each <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the built-in image decoders.</returns>
    public static ImageCodecInfo[] GetImageDecoders()
    {
      int numDecoders;
      int size;
      int imageDecodersSize = SafeNativeMethods.Gdip.GdipGetImageDecodersSize(out numDecoders, out size);
      if (imageDecodersSize != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageDecodersSize);
      IntPtr num = Marshal.AllocHGlobal(size);
      try
      {
        int imageDecoders = SafeNativeMethods.Gdip.GdipGetImageDecoders(numDecoders, size, num);
        if (imageDecoders != 0)
          throw SafeNativeMethods.Gdip.StatusException(imageDecoders);
        return ImageCodecInfo.ConvertFromMemory(num, numDecoders);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Returns an array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects that contain information about the image encoders built into GDI+.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects. Each <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the built-in image encoders.</returns>
    public static ImageCodecInfo[] GetImageEncoders()
    {
      int numEncoders;
      int size;
      int imageEncodersSize = SafeNativeMethods.Gdip.GdipGetImageEncodersSize(out numEncoders, out size);
      if (imageEncodersSize != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageEncodersSize);
      IntPtr num = Marshal.AllocHGlobal(size);
      try
      {
        int imageEncoders = SafeNativeMethods.Gdip.GdipGetImageEncoders(numEncoders, size, num);
        if (imageEncoders != 0)
          throw SafeNativeMethods.Gdip.StatusException(imageEncoders);
        return ImageCodecInfo.ConvertFromMemory(num, numEncoders);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    private static ImageCodecInfo[] ConvertFromMemory(IntPtr memoryStart, int numCodecs)
    {
      ImageCodecInfo[] imageCodecInfoArray = new ImageCodecInfo[numCodecs];
      for (int index1 = 0; index1 < numCodecs; ++index1)
      {
        IntPtr lparam = (IntPtr) ((long) memoryStart + (long) (Marshal.SizeOf(typeof (ImageCodecInfoPrivate)) * index1));
        ImageCodecInfoPrivate data = new ImageCodecInfoPrivate();
        System.Drawing.UnsafeNativeMethods.PtrToStructure(lparam, (object) data);
        imageCodecInfoArray[index1] = new ImageCodecInfo();
        imageCodecInfoArray[index1].Clsid = data.Clsid;
        imageCodecInfoArray[index1].FormatID = data.FormatID;
        imageCodecInfoArray[index1].CodecName = Marshal.PtrToStringUni(data.CodecName);
        imageCodecInfoArray[index1].DllName = Marshal.PtrToStringUni(data.DllName);
        imageCodecInfoArray[index1].FormatDescription = Marshal.PtrToStringUni(data.FormatDescription);
        imageCodecInfoArray[index1].FilenameExtension = Marshal.PtrToStringUni(data.FilenameExtension);
        imageCodecInfoArray[index1].MimeType = Marshal.PtrToStringUni(data.MimeType);
        imageCodecInfoArray[index1].Flags = (ImageCodecFlags) data.Flags;
        imageCodecInfoArray[index1].Version = data.Version;
        imageCodecInfoArray[index1].SignaturePatterns = new byte[data.SigCount][];
        imageCodecInfoArray[index1].SignatureMasks = new byte[data.SigCount][];
        for (int index2 = 0; index2 < data.SigCount; ++index2)
        {
          imageCodecInfoArray[index1].SignaturePatterns[index2] = new byte[data.SigSize];
          imageCodecInfoArray[index1].SignatureMasks[index2] = new byte[data.SigSize];
          Marshal.Copy((IntPtr) ((long) data.SigMask + (long) (index2 * data.SigSize)), imageCodecInfoArray[index1].SignatureMasks[index2], 0, data.SigSize);
          Marshal.Copy((IntPtr) ((long) data.SigPattern + (long) (index2 * data.SigSize)), imageCodecInfoArray[index1].SignaturePatterns[index2], 0, data.SigSize);
        }
      }
      return imageCodecInfoArray;
    }
  }
}
