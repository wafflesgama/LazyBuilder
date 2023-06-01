// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.GPStream
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Drawing.Internal
{
  internal class GPStream : System.Drawing.UnsafeNativeMethods.IStream
  {
    protected Stream dataStream;
    private long virtualPosition = -1;

    internal GPStream(Stream stream)
    {
      if (!stream.CanSeek)
      {
        byte[] numArray = new byte[256];
        int offset = 0;
        int num;
        do
        {
          if (numArray.Length < offset + 256)
          {
            byte[] destinationArray = new byte[numArray.Length * 2];
            Array.Copy((Array) numArray, (Array) destinationArray, numArray.Length);
            numArray = destinationArray;
          }
          num = stream.Read(numArray, offset, 256);
          offset += num;
        }
        while (num != 0);
        this.dataStream = (Stream) new MemoryStream(numArray);
      }
      else
        this.dataStream = stream;
    }

    private void ActualizeVirtualPosition()
    {
      if (this.virtualPosition == -1L)
        return;
      if (this.virtualPosition > this.dataStream.Length)
        this.dataStream.SetLength(this.virtualPosition);
      this.dataStream.Position = this.virtualPosition;
      this.virtualPosition = -1L;
    }

    public virtual System.Drawing.UnsafeNativeMethods.IStream Clone()
    {
      GPStream.NotImplemented();
      return (System.Drawing.UnsafeNativeMethods.IStream) null;
    }

    public virtual void Commit(int grfCommitFlags)
    {
      this.dataStream.Flush();
      this.ActualizeVirtualPosition();
    }

    [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
    [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public virtual long CopyTo(System.Drawing.UnsafeNativeMethods.IStream pstm, long cb, long[] pcbRead)
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
              throw GPStream.EFail("Wrote an incorrect number of bytes");
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

    protected static void NotImplemented() => throw new ExternalException(System.Drawing.SR.GetString(nameof (NotImplemented)), -2147467263);

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

    public virtual void Revert() => GPStream.NotImplemented();

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

    public void Stat(IntPtr pstatstg, int grfStatFlag) => Marshal.StructureToPtr((object) new GPStream.STATSTG()
    {
      cbSize = this.dataStream.Length
    }, pstatstg, true);

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

    [StructLayout(LayoutKind.Sequential)]
    public class STATSTG
    {
      public IntPtr pwcsName = IntPtr.Zero;
      public int type;
      [MarshalAs(UnmanagedType.I8)]
      public long cbSize;
      [MarshalAs(UnmanagedType.I8)]
      public long mtime;
      [MarshalAs(UnmanagedType.I8)]
      public long ctime;
      [MarshalAs(UnmanagedType.I8)]
      public long atime;
      [MarshalAs(UnmanagedType.I4)]
      public int grfMode;
      [MarshalAs(UnmanagedType.I4)]
      public int grfLocksSupported;
      public int clsid_data1;
      [MarshalAs(UnmanagedType.I2)]
      public short clsid_data2;
      [MarshalAs(UnmanagedType.I2)]
      public short clsid_data3;
      [MarshalAs(UnmanagedType.U1)]
      public byte clsid_b0;
      [MarshalAs(UnmanagedType.U1)]
      public byte clsid_b1;
      [MarshalAs(UnmanagedType.U1)]
      public byte clsid_b2;
      [MarshalAs(UnmanagedType.U1)]
      public byte clsid_b3;
      [MarshalAs(UnmanagedType.U1)]
      public byte clsid_b4;
      [MarshalAs(UnmanagedType.U1)]
      public byte clsid_b5;
      [MarshalAs(UnmanagedType.U1)]
      public byte clsid_b6;
      [MarshalAs(UnmanagedType.U1)]
      public byte clsid_b7;
      [MarshalAs(UnmanagedType.I4)]
      public int grfStateBits;
      [MarshalAs(UnmanagedType.I4)]
      public int reserved;
    }
  }
}
