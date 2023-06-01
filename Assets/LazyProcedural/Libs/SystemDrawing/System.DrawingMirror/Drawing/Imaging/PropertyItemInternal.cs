// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.PropertyItemInternal
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  [StructLayout(LayoutKind.Sequential)]
  internal sealed class PropertyItemInternal : IDisposable
  {
    public int id;
    public int len;
    public short type;
    public IntPtr value = IntPtr.Zero;

    internal PropertyItemInternal()
    {
    }

    ~PropertyItemInternal() => this.Dispose(false);

    public void Dispose() => this.Dispose(true);

    private void Dispose(bool disposing)
    {
      if (this.value != IntPtr.Zero)
      {
        Marshal.FreeHGlobal(this.value);
        this.value = IntPtr.Zero;
      }
      if (!disposing)
        return;
      GC.SuppressFinalize((object) this);
    }

    internal static PropertyItemInternal ConvertFromPropertyItem(PropertyItem propItem)
    {
      PropertyItemInternal propertyItemInternal = new PropertyItemInternal();
      propertyItemInternal.id = propItem.Id;
      propertyItemInternal.len = 0;
      propertyItemInternal.type = propItem.Type;
      byte[] source = propItem.Value;
      if (source != null)
      {
        int length = source.Length;
        propertyItemInternal.len = length;
        propertyItemInternal.value = Marshal.AllocHGlobal(length);
        Marshal.Copy(source, 0, propertyItemInternal.value, length);
      }
      return propertyItemInternal;
    }

    internal static PropertyItem[] ConvertFromMemory(IntPtr propdata, int count)
    {
      PropertyItem[] propertyItemArray = new PropertyItem[count];
      for (int index = 0; index < count; ++index)
      {
        PropertyItemInternal propertyItemInternal = (PropertyItemInternal) null;
        try
        {
          propertyItemInternal = (PropertyItemInternal) UnsafeNativeMethods.PtrToStructure(propdata, typeof (PropertyItemInternal));
          propertyItemArray[index] = new PropertyItem();
          propertyItemArray[index].Id = propertyItemInternal.id;
          propertyItemArray[index].Len = propertyItemInternal.len;
          propertyItemArray[index].Type = propertyItemInternal.type;
          propertyItemArray[index].Value = propertyItemInternal.Value;
          propertyItemInternal.value = IntPtr.Zero;
        }
        finally
        {
          propertyItemInternal?.Dispose();
        }
        propdata = (IntPtr) ((long) propdata + (long) Marshal.SizeOf(typeof (PropertyItemInternal)));
      }
      return propertyItemArray;
    }

    public byte[] Value
    {
      get
      {
        if (this.len == 0)
          return (byte[]) null;
        byte[] destination = new byte[this.len];
        Marshal.Copy(this.value, destination, 0, this.len);
        return destination;
      }
    }
  }
}
