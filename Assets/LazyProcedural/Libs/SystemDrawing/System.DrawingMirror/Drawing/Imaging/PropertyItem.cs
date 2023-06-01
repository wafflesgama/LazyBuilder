// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.PropertyItem
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Encapsulates a metadata property to be included in an image file. Not inheritable.</summary>
  public sealed class PropertyItem
  {
    private int id;
    private int len;
    private short type;
    private byte[] value;

    internal PropertyItem()
    {
    }

    /// <summary>Gets or sets the ID of the property.</summary>
    /// <returns>The integer that represents the ID of the property.</returns>
    public int Id
    {
      get => this.id;
      set => this.id = value;
    }

    /// <summary>Gets or sets the length (in bytes) of the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
    /// <returns>An integer that represents the length (in bytes) of the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> byte array.</returns>
    public int Len
    {
      get => this.len;
      set => this.len = value;
    }

    /// <summary>Gets or sets an integer that defines the type of data contained in the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
    /// <returns>An integer that defines the type of data contained in <see cref="P:System.Drawing.Imaging.PropertyItem.Value" />.</returns>
    public short Type
    {
      get => this.type;
      set => this.type = value;
    }

    /// <summary>Gets or sets the value of the property item.</summary>
    /// <returns>A byte array that represents the value of the property item.</returns>
    public byte[] Value
    {
      get => this.value;
      set => this.value = value;
    }
  }
}
