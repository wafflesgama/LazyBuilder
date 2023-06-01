// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.EncoderParameters
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Imaging
{
  /// <summary>Encapsulates an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
  public sealed class EncoderParameters : IDisposable
  {
    private EncoderParameter[] param;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> class that can contain the specified number of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
    /// <param name="count">An integer that specifies the number of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects that the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object can contain.</param>
    public EncoderParameters(int count) => this.param = new EncoderParameter[count];

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> class that can contain one <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
    public EncoderParameters() => this.param = new EncoderParameter[1];

    /// <summary>Gets or sets an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
    /// <returns>The array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</returns>
    public EncoderParameter[] Param
    {
      get => this.param;
      set => this.param = value;
    }

    internal IntPtr ConvertToMemory()
    {
      int num1 = Marshal.SizeOf(typeof (EncoderParameter));
      int length = this.param.Length;
      IntPtr ptr = Marshal.AllocHGlobal(checked (length * num1 + Marshal.SizeOf(typeof (IntPtr))));
      if (ptr == IntPtr.Zero)
        throw SafeNativeMethods.Gdip.StatusException(3);
      Marshal.WriteIntPtr(ptr, (IntPtr) length);
      long num2 = checked (unchecked ((long) ptr) + (long) Marshal.SizeOf(typeof (IntPtr)));
      for (int index = 0; index < length; ++index)
        Marshal.StructureToPtr((object) this.param[index], (IntPtr) (num2 + (long) (index * num1)), false);
      return ptr;
    }

    internal static EncoderParameters ConvertFromMemory(IntPtr memory)
    {
      int count = !(memory == IntPtr.Zero) ? Marshal.ReadIntPtr(memory).ToInt32() : throw SafeNativeMethods.Gdip.StatusException(2);
      EncoderParameters encoderParameters = new EncoderParameters(count);
      int num1 = Marshal.SizeOf(typeof (EncoderParameter));
      long num2 = (long) memory + (long) Marshal.SizeOf(typeof (IntPtr));
      IntSecurity.UnmanagedCode.Assert();
      try
      {
        for (int index = 0; index < count; ++index)
        {
          Guid structure = (Guid) UnsafeNativeMethods.PtrToStructure((IntPtr) ((long) (index * num1) + num2), typeof (Guid));
          int numberValues = Marshal.ReadInt32((IntPtr) ((long) (index * num1) + num2 + 16L));
          EncoderParameterValueType type = (EncoderParameterValueType) Marshal.ReadInt32((IntPtr) ((long) (index * num1) + num2 + 20L));
          IntPtr num3 = Marshal.ReadIntPtr((IntPtr) ((long) (index * num1) + num2 + 24L));
          encoderParameters.param[index] = new EncoderParameter(new Encoder(structure), numberValues, type, num3);
        }
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      return encoderParameters;
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object.</summary>
    public void Dispose()
    {
      foreach (EncoderParameter encoderParameter in this.param)
        encoderParameter?.Dispose();
      this.param = (EncoderParameter[]) null;
    }
  }
}
