// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.MetafileHeader
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  /// <summary>Contains attributes of an associated <see cref="T:System.Drawing.Imaging.Metafile" />. Not inheritable.</summary>
  [StructLayout(LayoutKind.Sequential)]
  public sealed class MetafileHeader
  {
    internal MetafileHeaderWmf wmf;
    internal MetafileHeaderEmf emf;

    internal MetafileHeader()
    {
    }

    /// <summary>Gets the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Imaging.MetafileType" /> enumeration that represents the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public MetafileType Type => !this.IsWmf() ? this.emf.type : this.wmf.type;

    /// <summary>Gets the size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>The size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public int MetafileSize => !this.IsWmf() ? this.emf.size : this.wmf.size;

    /// <summary>Gets the version number of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>The version number of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public int Version => !this.IsWmf() ? this.emf.version : this.wmf.version;

    /// <summary>Gets the horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>The horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public float DpiX => !this.IsWmf() ? this.emf.dpiX : this.wmf.dpiX;

    /// <summary>Gets the vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>The vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public float DpiY => !this.IsWmf() ? this.emf.dpiY : this.wmf.dpiY;

    /// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> that bounds the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Rectangle" /> that bounds the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public Rectangle Bounds => !this.IsWmf() ? new Rectangle(this.emf.X, this.emf.Y, this.emf.Width, this.emf.Height) : new Rectangle(this.wmf.X, this.wmf.Y, this.wmf.Width, this.wmf.Height);

    /// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows metafile format.</summary>
    /// <returns>
    /// <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows metafile format; otherwise, <see langword="false" />.</returns>
    public bool IsWmf()
    {
      if (this.wmf == null && this.emf == null)
        throw SafeNativeMethods.Gdip.StatusException(2);
      return this.wmf != null && (this.wmf.type == MetafileType.Wmf || this.wmf.type == MetafileType.WmfPlaceable);
    }

    /// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows placeable metafile format.</summary>
    /// <returns>
    /// <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows placeable metafile format; otherwise, <see langword="false" />.</returns>
    public bool IsWmfPlaceable()
    {
      if (this.wmf == null && this.emf == null)
        throw SafeNativeMethods.Gdip.StatusException(2);
      return this.wmf != null && this.wmf.type == MetafileType.WmfPlaceable;
    }

    /// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format.</summary>
    /// <returns>
    /// <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format; otherwise, <see langword="false" />.</returns>
    public bool IsEmf()
    {
      if (this.wmf == null && this.emf == null)
        throw SafeNativeMethods.Gdip.StatusException(2);
      return this.emf != null && this.emf.type == MetafileType.Emf;
    }

    /// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format or the Windows enhanced metafile plus format.</summary>
    /// <returns>
    /// <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format or the Windows enhanced metafile plus format; otherwise, <see langword="false" />.</returns>
    public bool IsEmfOrEmfPlus()
    {
      if (this.wmf == null && this.emf == null)
        throw SafeNativeMethods.Gdip.StatusException(2);
      return this.emf != null && this.emf.type >= MetafileType.Emf;
    }

    /// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile plus format.</summary>
    /// <returns>
    /// <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile plus format; otherwise, <see langword="false" />.</returns>
    public bool IsEmfPlus()
    {
      if (this.wmf == null && this.emf == null)
        throw SafeNativeMethods.Gdip.StatusException(2);
      return this.emf != null && this.emf.type >= MetafileType.EmfPlusOnly;
    }

    /// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Dual enhanced metafile format. This format supports both the enhanced and the enhanced plus format.</summary>
    /// <returns>
    /// <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Dual enhanced metafile format; otherwise, <see langword="false" />.</returns>
    public bool IsEmfPlusDual()
    {
      if (this.wmf == null && this.emf == null)
        throw SafeNativeMethods.Gdip.StatusException(2);
      return this.emf != null && this.emf.type == MetafileType.EmfPlusDual;
    }

    /// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> supports only the Windows enhanced metafile plus format.</summary>
    /// <returns>
    /// <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> supports only the Windows enhanced metafile plus format; otherwise, <see langword="false" />.</returns>
    public bool IsEmfPlusOnly()
    {
      if (this.wmf == null && this.emf == null)
        throw SafeNativeMethods.Gdip.StatusException(2);
      return this.emf != null && this.emf.type == MetafileType.EmfPlusOnly;
    }

    /// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is device dependent.</summary>
    /// <returns>
    /// <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is device dependent; otherwise, <see langword="false" />.</returns>
    public bool IsDisplay() => this.IsEmfPlus() && (this.emf.emfPlusFlags & EmfPlusFlags.Display) != 0;

    /// <summary>Gets the Windows metafile (WMF) header file for the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Imaging.MetaHeader" /> that contains the WMF header file for the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public MetaHeader WmfHeader => this.wmf != null ? this.wmf.WmfHeader : throw SafeNativeMethods.Gdip.StatusException(2);

    /// <summary>Gets the size, in bytes, of the enhanced metafile plus header file.</summary>
    /// <returns>The size, in bytes, of the enhanced metafile plus header file.</returns>
    public int EmfPlusHeaderSize
    {
      get
      {
        if (this.wmf == null && this.emf == null)
          throw SafeNativeMethods.Gdip.StatusException(2);
        return !this.IsWmf() ? this.emf.EmfPlusHeaderSize : this.wmf.EmfPlusHeaderSize;
      }
    }

    /// <summary>Gets the logical horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>The logical horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public int LogicalDpiX
    {
      get
      {
        if (this.wmf == null && this.emf == null)
          throw SafeNativeMethods.Gdip.StatusException(2);
        return !this.IsWmf() ? this.emf.LogicalDpiX : this.wmf.LogicalDpiX;
      }
    }

    /// <summary>Gets the logical vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <returns>The logical vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
    public int LogicalDpiY
    {
      get
      {
        if (this.wmf == null && this.emf == null)
          throw SafeNativeMethods.Gdip.StatusException(2);
        return !this.IsWmf() ? this.emf.LogicalDpiX : this.wmf.LogicalDpiY;
      }
    }
  }
}
