// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ImageLockMode
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Specifies flags that are passed to the flags parameter of the <see cref="Overload:System.Drawing.Bitmap.LockBits" /> method. The <see cref="Overload:System.Drawing.Bitmap.LockBits" /> method locks a portion of an image so that you can read or write the pixel data.</summary>
  public enum ImageLockMode
  {
    /// <summary>Specifies that a portion of the image is locked for reading.</summary>
    ReadOnly = 1,
    /// <summary>Specifies that a portion of the image is locked for writing.</summary>
    WriteOnly = 2,
    /// <summary>Specifies that a portion of the image is locked for reading or writing.</summary>
    ReadWrite = 3,
    /// <summary>Specifies that the buffer used for reading or writing pixel data is allocated by the user. If this flag is set, the <paramref name="flags" /> parameter of the <see cref="Overload:System.Drawing.Bitmap.LockBits" /> method serves as an input parameter (and possibly as an output parameter). If this flag is cleared, then the <paramref name="flags" /> parameter serves only as an output parameter.</summary>
    UserInputBuffer = 4,
  }
}
