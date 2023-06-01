// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.FrameDimension
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Provides properties that get the frame dimensions of an image. Not inheritable.</summary>
  public sealed class FrameDimension
  {
    private static FrameDimension time = new FrameDimension(new Guid("{6aedbd6d-3fb5-418a-83a6-7f45229dc872}"));
    private static FrameDimension resolution = new FrameDimension(new Guid("{84236f7b-3bd3-428f-8dab-4ea1439ca315}"));
    private static FrameDimension page = new FrameDimension(new Guid("{7462dc86-6180-4c7e-8e3f-ee7333a7a483}"));
    private Guid guid;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.FrameDimension" /> class using the specified <see langword="Guid" /> structure.</summary>
    /// <param name="guid">A <see langword="Guid" /> structure that contains a GUID for this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</param>
    public FrameDimension(Guid guid) => this.guid = guid;

    /// <summary>Gets a globally unique identifier (GUID) that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
    /// <returns>A <see langword="Guid" /> structure that contains a GUID that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
    public Guid Guid => this.guid;

    /// <summary>Gets the time dimension.</summary>
    /// <returns>The time dimension.</returns>
    public static FrameDimension Time => FrameDimension.time;

    /// <summary>Gets the resolution dimension.</summary>
    /// <returns>The resolution dimension.</returns>
    public static FrameDimension Resolution => FrameDimension.resolution;

    /// <summary>Gets the page dimension.</summary>
    /// <returns>The page dimension.</returns>
    public static FrameDimension Page => FrameDimension.page;

    /// <summary>Returns a value that indicates whether the specified object is a <see cref="T:System.Drawing.Imaging.FrameDimension" /> equivalent to this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
    /// <param name="o">The object to test.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Drawing.Imaging.FrameDimension" /> equivalent to this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object o) => o is FrameDimension frameDimension && this.guid == frameDimension.guid;

    /// <summary>Returns a hash code for this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
    /// <returns>The hash code of this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
    public override int GetHashCode() => this.guid.GetHashCode();

    /// <summary>Converts this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object to a human-readable string.</summary>
    /// <returns>A string that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
    public override string ToString()
    {
      if (this == FrameDimension.time)
        return "Time";
      if (this == FrameDimension.resolution)
        return "Resolution";
      return this == FrameDimension.page ? "Page" : "[FrameDimension: " + this.guid.ToString() + "]";
    }
  }
}
