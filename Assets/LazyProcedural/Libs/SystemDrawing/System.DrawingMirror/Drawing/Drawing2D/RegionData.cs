// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.RegionData
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Encapsulates the data that makes up a <see cref="T:System.Drawing.Region" /> object. This class cannot be inherited.</summary>
  public sealed class RegionData
  {
    private byte[] data;

    internal RegionData(byte[] data) => this.data = data;

    /// <summary>Gets or sets an array of bytes that specify the <see cref="T:System.Drawing.Region" /> object.</summary>
    /// <returns>An array of bytes that specify the <see cref="T:System.Drawing.Region" /> object.</returns>
    public byte[] Data
    {
      get => this.data;
      set => this.data = value;
    }
  }
}
