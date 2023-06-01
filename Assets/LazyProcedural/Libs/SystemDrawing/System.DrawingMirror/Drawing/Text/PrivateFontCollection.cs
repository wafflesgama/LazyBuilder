// Decompiled with JetBrains decompiler
// Type: System.Drawing.Text.PrivateFontCollection
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
  /// <summary>Provides a collection of font families built from font files that are provided by the client application.</summary>
  public sealed class PrivateFontCollection : FontCollection
  {
    private List<string> gdiFonts;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Text.PrivateFontCollection" /> class.</summary>
    public PrivateFontCollection()
    {
      this.nativeFontCollection = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipNewPrivateFontCollection(out this.nativeFontCollection);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      if (System.Drawing.LocalAppContextSwitches.DoNotRemoveGdiFontsResourcesFromFontCollection)
        return;
      this.gdiFonts = new List<string>();
    }

    protected override void Dispose(bool disposing)
    {
      if (this.nativeFontCollection != IntPtr.Zero)
      {
        try
        {
          SafeNativeMethods.Gdip.GdipDeletePrivateFontCollection(out this.nativeFontCollection);
          if (this.gdiFonts != null)
          {
            foreach (string gdiFont in this.gdiFonts)
              SafeNativeMethods.RemoveFontFile(gdiFont);
            this.gdiFonts.Clear();
            this.gdiFonts = (List<string>) null;
          }
        }
        catch (Exception ex)
        {
          if (System.Drawing.ClientUtils.IsSecurityOrCriticalException(ex))
            throw;
        }
        finally
        {
          this.nativeFontCollection = IntPtr.Zero;
        }
      }
      base.Dispose(disposing);
    }

    /// <summary>Adds a font from the specified file to this <see cref="T:System.Drawing.Text.PrivateFontCollection" />.</summary>
    /// <param name="filename">A <see cref="T:System.String" /> that contains the file name of the font to add.</param>
    /// <exception cref="T:System.IO.FileNotFoundException">The specified font is not supported or the font file cannot be found.</exception>
    public void AddFontFile(string filename)
    {
      IntSecurity.DemandReadFileIO(filename);
      int status = SafeNativeMethods.Gdip.GdipPrivateAddFontFile(new HandleRef((object) this, this.nativeFontCollection), filename);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      if (SafeNativeMethods.AddFontFile(filename) == 0 || this.gdiFonts == null)
        return;
      this.gdiFonts.Add(filename);
    }

    /// <summary>Adds a font contained in system memory to this <see cref="T:System.Drawing.Text.PrivateFontCollection" />.</summary>
    /// <param name="memory">The memory address of the font to add.</param>
    /// <param name="length">The memory length of the font to add.</param>
    public void AddMemoryFont(IntPtr memory, int length)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      int status = SafeNativeMethods.Gdip.GdipPrivateAddMemoryFont(new HandleRef((object) this, this.nativeFontCollection), new HandleRef((object) null, memory), length);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }
  }
}
