// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ImageCodecInfoPrivate
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  [StructLayout(LayoutKind.Sequential, Pack = 8)]
  internal class ImageCodecInfoPrivate
  {
    [MarshalAs(UnmanagedType.Struct)]
    public Guid Clsid;
    [MarshalAs(UnmanagedType.Struct)]
    public Guid FormatID;
    public IntPtr CodecName = IntPtr.Zero;
    public IntPtr DllName = IntPtr.Zero;
    public IntPtr FormatDescription = IntPtr.Zero;
    public IntPtr FilenameExtension = IntPtr.Zero;
    public IntPtr MimeType = IntPtr.Zero;
    public int Flags;
    public int Version;
    public int SigCount;
    public int SigSize;
    public IntPtr SigPattern = IntPtr.Zero;
    public IntPtr SigMask = IntPtr.Zero;
  }
}
