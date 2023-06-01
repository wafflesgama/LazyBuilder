// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.MetaHeader
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  /// <summary>Contains information about a windows-format (WMF) metafile.</summary>
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public sealed class MetaHeader
  {
    private short type;
    private short headerSize;
    private short version;
    private int size;
    private short noObjects;
    private int maxRecord;
    private short noParameters;

    /// <summary>Gets or sets the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
    /// <returns>The type of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
    public short Type
    {
      get => this.type;
      set => this.type = value;
    }

    /// <summary>Gets or sets the size, in bytes, of the header file.</summary>
    /// <returns>The size, in bytes, of the header file.</returns>
    public short HeaderSize
    {
      get => this.headerSize;
      set => this.headerSize = value;
    }

    /// <summary>Gets or sets the version number of the header format.</summary>
    /// <returns>The version number of the header format.</returns>
    public short Version
    {
      get => this.version;
      set => this.version = value;
    }

    /// <summary>Gets or sets the size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
    /// <returns>The size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
    public int Size
    {
      get => this.size;
      set => this.size = value;
    }

    /// <summary>Gets or sets the maximum number of objects that exist in the <see cref="T:System.Drawing.Imaging.Metafile" /> object at the same time.</summary>
    /// <returns>The maximum number of objects that exist in the <see cref="T:System.Drawing.Imaging.Metafile" /> object at the same time.</returns>
    public short NoObjects
    {
      get => this.noObjects;
      set => this.noObjects = value;
    }

    /// <summary>Gets or sets the size, in bytes, of the largest record in the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
    /// <returns>The size, in bytes, of the largest record in the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
    public int MaxRecord
    {
      get => this.maxRecord;
      set => this.maxRecord = value;
    }

    /// <summary>Not used. Always returns 0.</summary>
    /// <returns>Always 0.</returns>
    public short NoParameters
    {
      get => this.noParameters;
      set => this.noParameters = value;
    }
  }
}
