// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.WmfPlaceableFileHeader
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  /// <summary>Defines a placeable metafile. Not inheritable.</summary>
  [StructLayout(LayoutKind.Sequential)]
  public sealed class WmfPlaceableFileHeader
  {
    private int key = -1698247209;
    private short hmf;
    private short bboxLeft;
    private short bboxTop;
    private short bboxRight;
    private short bboxBottom;
    private short inch;
    private int reserved;
    private short checksum;

    /// <summary>Gets or sets a value indicating the presence of a placeable metafile header.</summary>
    /// <returns>A value indicating presence of a placeable metafile header.</returns>
    public int Key
    {
      get => this.key;
      set => this.key = value;
    }

    /// <summary>Gets or sets the handle of the metafile in memory.</summary>
    /// <returns>The handle of the metafile in memory.</returns>
    public short Hmf
    {
      get => this.hmf;
      set => this.hmf = value;
    }

    /// <summary>Gets or sets the x-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</summary>
    /// <returns>The x-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</returns>
    public short BboxLeft
    {
      get => this.bboxLeft;
      set => this.bboxLeft = value;
    }

    /// <summary>Gets or sets the y-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</summary>
    /// <returns>The y-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</returns>
    public short BboxTop
    {
      get => this.bboxTop;
      set => this.bboxTop = value;
    }

    /// <summary>Gets or sets the x-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</summary>
    /// <returns>The x-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</returns>
    public short BboxRight
    {
      get => this.bboxRight;
      set => this.bboxRight = value;
    }

    /// <summary>Gets or sets the y-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</summary>
    /// <returns>The y-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</returns>
    public short BboxBottom
    {
      get => this.bboxBottom;
      set => this.bboxBottom = value;
    }

    /// <summary>Gets or sets the number of twips per inch.</summary>
    /// <returns>The number of twips per inch.</returns>
    public short Inch
    {
      get => this.inch;
      set => this.inch = value;
    }

    /// <summary>Reserved. Do not use.</summary>
    /// <returns>Reserved. Do not use.</returns>
    public int Reserved
    {
      get => this.reserved;
      set => this.reserved = value;
    }

    /// <summary>Gets or sets the checksum value for the previous ten <see langword="WORD" /> s in the header.</summary>
    /// <returns>The checksum value for the previous ten <see langword="WORD" /> s in the header.</returns>
    public short Checksum
    {
      get => this.checksum;
      set => this.checksum = value;
    }
  }
}
