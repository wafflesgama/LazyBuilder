// Decompiled with JetBrains decompiler
// Type: System.Drawing.UnsafeNativeMethods
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing
{
  [SuppressUnmanagedCodeSecurity]
  internal class UnsafeNativeMethods
  {
    [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern void CopyMemory(HandleRef destData, HandleRef srcData, int size);

    [DllImport("user32.dll", EntryPoint = "GetDC", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntGetDC(HandleRef hWnd);

    public static IntPtr GetDC(HandleRef hWnd) => System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntGetDC(hWnd), SafeNativeMethods.CommonHandles.HDC);

    [DllImport("gdi32.dll", EntryPoint = "DeleteDC", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool IntDeleteDC(HandleRef hDC);

    public static bool DeleteDC(HandleRef hDC)
    {
      System.Internal.HandleCollector.Remove((IntPtr) hDC, SafeNativeMethods.CommonHandles.GDI);
      return UnsafeNativeMethods.IntDeleteDC(hDC);
    }

    [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

    public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
    {
      System.Internal.HandleCollector.Remove((IntPtr) hDC, SafeNativeMethods.CommonHandles.HDC);
      return UnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
    }

    [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

    public static IntPtr CreateCompatibleDC(HandleRef hDC) => System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateCompatibleDC(hDC), SafeNativeMethods.CommonHandles.GDI);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetStockObject(int nIndex);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetSystemDefaultLCID();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
    public static extern bool SystemParametersInfo(
      int uiAction,
      int uiParam,
      [In, Out] NativeMethods.NONCLIENTMETRICS pvParam,
      int fWinIni);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
    public static extern bool SystemParametersInfo(
      int uiAction,
      int uiParam,
      [In, Out] SafeNativeMethods.LOGFONT pvParam,
      int fWinIni);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObjectType(HandleRef hObject);

    [ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
    [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static object PtrToStructure(IntPtr lparam, Type cls) => Marshal.PtrToStructure(lparam, cls);

    [ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
    [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static void PtrToStructure(IntPtr lparam, object data) => Marshal.PtrToStructure(lparam, data);

    [Guid("0000000C-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IStream
    {
      int Read([In] IntPtr buf, [In] int len);

      int Write([In] IntPtr buf, [In] int len);

      [return: MarshalAs(UnmanagedType.I8)]
      long Seek([MarshalAs(UnmanagedType.I8), In] long dlibMove, [In] int dwOrigin);

      void SetSize([MarshalAs(UnmanagedType.I8), In] long libNewSize);

      [return: MarshalAs(UnmanagedType.I8)]
      long CopyTo([MarshalAs(UnmanagedType.Interface), In] UnsafeNativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I8), In] long cb, [MarshalAs(UnmanagedType.LPArray), Out] long[] pcbRead);

      void Commit([In] int grfCommitFlags);

      void Revert();

      void LockRegion([MarshalAs(UnmanagedType.I8), In] long libOffset, [MarshalAs(UnmanagedType.I8), In] long cb, [In] int dwLockType);

      void UnlockRegion([MarshalAs(UnmanagedType.I8), In] long libOffset, [MarshalAs(UnmanagedType.I8), In] long cb, [In] int dwLockType);

      void Stat([In] IntPtr pStatstg, [In] int grfStatFlag);

      [return: MarshalAs(UnmanagedType.Interface)]
      UnsafeNativeMethods.IStream Clone();
    }

    internal class ComStreamFromDataStream : UnsafeNativeMethods.IStream
    {
      protected Stream dataStream;
      private long virtualPosition = -1;

      internal ComStreamFromDataStream(Stream dataStream) => this.dataStream = dataStream != null ? dataStream : throw new ArgumentNullException(nameof (dataStream));

      private void ActualizeVirtualPosition()
      {
        if (this.virtualPosition == -1L)
          return;
        if (this.virtualPosition > this.dataStream.Length)
          this.dataStream.SetLength(this.virtualPosition);
        this.dataStream.Position = this.virtualPosition;
        this.virtualPosition = -1L;
      }

      public virtual UnsafeNativeMethods.IStream Clone()
      {
        UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
        return (UnsafeNativeMethods.IStream) null;
      }

      public virtual void Commit(int grfCommitFlags)
      {
        this.dataStream.Flush();
        this.ActualizeVirtualPosition();
      }

      public virtual long CopyTo(UnsafeNativeMethods.IStream pstm, long cb, long[] pcbRead)
      {
        int cb1 = 4096;
        IntPtr num1 = Marshal.AllocHGlobal(cb1);
        if (num1 == IntPtr.Zero)
          throw new OutOfMemoryException();
        long num2 = 0;
        try
        {
          int len;
          for (; num2 < cb; num2 += (long) len)
          {
            int length = cb1;
            if (num2 + (long) length > cb)
              length = (int) (cb - num2);
            len = this.Read(num1, length);
            if (len != 0)
            {
              if (pstm.Write(num1, len) != len)
                throw UnsafeNativeMethods.ComStreamFromDataStream.EFail("Wrote an incorrect number of bytes");
            }
            else
              break;
          }
        }
        finally
        {
          Marshal.FreeHGlobal(num1);
        }
        if (pcbRead != null && pcbRead.Length != 0)
          pcbRead[0] = num2;
        return num2;
      }

      public virtual Stream GetDataStream() => this.dataStream;

      public virtual void LockRegion(long libOffset, long cb, int dwLockType)
      {
      }

      protected static ExternalException EFail(string msg) => throw new ExternalException(msg, -2147467259);

      protected static void NotImplemented() => throw new ExternalException(SR.GetString(nameof (NotImplemented)), -2147467263);

      public virtual int Read(IntPtr buf, int length)
      {
        byte[] numArray = new byte[length];
        int num = this.Read(numArray, length);
        Marshal.Copy(numArray, 0, buf, length);
        return num;
      }

      public virtual int Read(byte[] buffer, int length)
      {
        this.ActualizeVirtualPosition();
        return this.dataStream.Read(buffer, 0, length);
      }

      public virtual void Revert() => UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();

      public virtual long Seek(long offset, int origin)
      {
        long num = this.virtualPosition;
        if (this.virtualPosition == -1L)
          num = this.dataStream.Position;
        long length = this.dataStream.Length;
        switch (origin)
        {
          case 0:
            if (offset <= length)
            {
              this.dataStream.Position = offset;
              this.virtualPosition = -1L;
              break;
            }
            this.virtualPosition = offset;
            break;
          case 1:
            if (offset + num <= length)
            {
              this.dataStream.Position = num + offset;
              this.virtualPosition = -1L;
              break;
            }
            this.virtualPosition = offset + num;
            break;
          case 2:
            if (offset <= 0L)
            {
              this.dataStream.Position = length + offset;
              this.virtualPosition = -1L;
              break;
            }
            this.virtualPosition = length + offset;
            break;
        }
        return this.virtualPosition != -1L ? this.virtualPosition : this.dataStream.Position;
      }

      public virtual void SetSize(long value) => this.dataStream.SetLength(value);

      public virtual void Stat(IntPtr pstatstg, int grfStatFlag) => UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();

      public virtual void UnlockRegion(long libOffset, long cb, int dwLockType)
      {
      }

      public virtual int Write(IntPtr buf, int length)
      {
        byte[] numArray = new byte[length];
        Marshal.Copy(buf, numArray, 0, length);
        return this.Write(numArray, length);
      }

      public virtual int Write(byte[] buffer, int length)
      {
        this.ActualizeVirtualPosition();
        this.dataStream.Write(buffer, 0, length);
        return length;
      }
    }
  }
}
