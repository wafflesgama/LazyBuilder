// Decompiled with JetBrains decompiler
// Type: System.Drawing.Text.FontCollection
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
  /// <summary>Provides a base class for installed and private font collections.</summary>
  public abstract class FontCollection : IDisposable
  {
    internal IntPtr nativeFontCollection;

    internal FontCollection() => this.nativeFontCollection = IntPtr.Zero;

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Text.FontCollection" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Text.FontCollection" /> and optionally releases the managed resources.</summary>
    /// <param name="disposing">
    /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>Gets the array of <see cref="T:System.Drawing.FontFamily" /> objects associated with this <see cref="T:System.Drawing.Text.FontCollection" />.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects.</returns>
    public FontFamily[] Families
    {
      get
      {
        int numFound1 = 0;
        int collectionFamilyCount = SafeNativeMethods.Gdip.GdipGetFontCollectionFamilyCount(new HandleRef((object) this, this.nativeFontCollection), out numFound1);
        if (collectionFamilyCount != 0)
          throw SafeNativeMethods.Gdip.StatusException(collectionFamilyCount);
        IntPtr[] gpfamilies = new IntPtr[numFound1];
        int numFound2 = 0;
        int collectionFamilyList = SafeNativeMethods.Gdip.GdipGetFontCollectionFamilyList(new HandleRef((object) this, this.nativeFontCollection), numFound1, gpfamilies, out numFound2);
        if (collectionFamilyList != 0)
          throw SafeNativeMethods.Gdip.StatusException(collectionFamilyList);
        FontFamily[] families = new FontFamily[numFound2];
        for (int index = 0; index < numFound2; ++index)
        {
          IntPtr clonefontfamily;
          SafeNativeMethods.Gdip.GdipCloneFontFamily(new HandleRef((object) null, gpfamilies[index]), out clonefontfamily);
          families[index] = new FontFamily(clonefontfamily);
        }
        return families;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~FontCollection() => this.Dispose(false);
  }
}
