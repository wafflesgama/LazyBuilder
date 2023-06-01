// Decompiled with JetBrains decompiler
// Type: System.Drawing.Text.InstalledFontCollection
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Text
{
  /// <summary>Represents the fonts installed on the system. This class cannot be inherited.</summary>
  public sealed class InstalledFontCollection : FontCollection
  {
    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Text.InstalledFontCollection" /> class.</summary>
    public InstalledFontCollection()
    {
      this.nativeFontCollection = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipNewInstalledFontCollection(out this.nativeFontCollection);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }
  }
}
