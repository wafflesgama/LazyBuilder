// Decompiled with JetBrains decompiler
// Type: System.Drawing.FontFamily
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Drawing
{
  /// <summary>Defines a group of type faces having a similar basic design and certain variations in styles. This class cannot be inherited.</summary>
  public sealed class FontFamily : MarshalByRefObject, IDisposable
  {
    private const int LANG_NEUTRAL = 0;
    private IntPtr nativeFamily;
    private bool createDefaultOnFail;

    private void SetNativeFamily(IntPtr family) => this.nativeFamily = family;

    internal FontFamily(IntPtr family) => this.SetNativeFamily(family);

    internal FontFamily(string name, bool createDefaultOnFail)
    {
      this.createDefaultOnFail = createDefaultOnFail;
      this.CreateFontFamily(name, (FontCollection) null);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> with the specified name.</summary>
    /// <param name="name">The name of the new <see cref="T:System.Drawing.FontFamily" />.</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="name" /> is an empty string ("").
    /// -or-
    /// <paramref name="name" /> specifies a font that is not installed on the computer running the application.
    /// -or-
    /// <paramref name="name" /> specifies a font that is not a TrueType font.</exception>
    public FontFamily(string name) => this.CreateFontFamily(name, (FontCollection) null);

    /// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> in the specified <see cref="T:System.Drawing.Text.FontCollection" /> with the specified name.</summary>
    /// <param name="name">A <see cref="T:System.String" /> that represents the name of the new <see cref="T:System.Drawing.FontFamily" />.</param>
    /// <param name="fontCollection">The <see cref="T:System.Drawing.Text.FontCollection" /> that contains this <see cref="T:System.Drawing.FontFamily" />.</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="name" /> is an empty string ("").
    /// -or-
    /// <paramref name="name" /> specifies a font that is not installed on the computer running the application.
    /// -or-
    /// <paramref name="name" /> specifies a font that is not a TrueType font.</exception>
    public FontFamily(string name, FontCollection fontCollection) => this.CreateFontFamily(name, fontCollection);

    private void CreateFontFamily(string name, FontCollection fontCollection)
    {
      IntPtr FontFamily = IntPtr.Zero;
      IntPtr handle = fontCollection == null ? IntPtr.Zero : fontCollection.nativeFontCollection;
      int fontFamilyFromName = SafeNativeMethods.Gdip.GdipCreateFontFamilyFromName(name, new HandleRef((object) fontCollection, handle), out FontFamily);
      if (fontFamilyFromName != 0)
      {
        if (this.createDefaultOnFail)
        {
          FontFamily = FontFamily.GetGdipGenericSansSerif();
        }
        else
        {
          if (fontFamilyFromName == 14)
            throw new ArgumentException(SR.GetString("GdiplusFontFamilyNotFound", (object) name));
          if (fontFamilyFromName == 16)
            throw new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont", (object) name));
          throw SafeNativeMethods.Gdip.StatusException(fontFamilyFromName);
        }
      }
      this.SetNativeFamily(FontFamily);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> from the specified generic font family.</summary>
    /// <param name="genericFamily">The <see cref="T:System.Drawing.Text.GenericFontFamilies" /> from which to create the new <see cref="T:System.Drawing.FontFamily" />.</param>
    public FontFamily(GenericFontFamilies genericFamily)
    {
      IntPtr fontfamily = IntPtr.Zero;
      int status;
      switch (genericFamily)
      {
        case GenericFontFamilies.Serif:
          status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySerif(out fontfamily);
          break;
        case GenericFontFamilies.SansSerif:
          status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySansSerif(out fontfamily);
          break;
        default:
          status = SafeNativeMethods.Gdip.GdipGetGenericFontFamilyMonospace(out fontfamily);
          break;
      }
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.SetNativeFamily(fontfamily);
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~FontFamily() => this.Dispose(false);

    internal IntPtr NativeFamily => this.nativeFamily;

    /// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />.</summary>
    /// <param name="obj">The object to test.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object obj)
    {
      if (obj == this)
        return true;
      return obj is FontFamily fontFamily && fontFamily.NativeFamily == this.NativeFamily;
    }

    /// <summary>Converts this <see cref="T:System.Drawing.FontFamily" /> to a human-readable string representation.</summary>
    /// <returns>The string that represents this <see cref="T:System.Drawing.FontFamily" />.</returns>
    public override string ToString() => string.Format((IFormatProvider) CultureInfo.CurrentCulture, "[{0}: Name={1}]", new object[2]
    {
      (object) this.GetType().Name,
      (object) this.Name
    });

    /// <summary>Gets a hash code for this <see cref="T:System.Drawing.FontFamily" />.</summary>
    /// <returns>The hash code for this <see cref="T:System.Drawing.FontFamily" />.</returns>
    public override int GetHashCode() => this.GetName(0).GetHashCode();

    private static int CurrentLanguage => CultureInfo.CurrentUICulture.LCID;

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.FontFamily" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.nativeFamily != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDeleteFontFamily(new HandleRef((object) this, this.nativeFamily));
      }
      catch (Exception ex)
      {
        if (!ClientUtils.IsCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeFamily = IntPtr.Zero;
      }
    }

    /// <summary>Gets the name of this <see cref="T:System.Drawing.FontFamily" />.</summary>
    /// <returns>A <see cref="T:System.String" /> that represents the name of this <see cref="T:System.Drawing.FontFamily" />.</returns>
    public string Name => this.GetName(FontFamily.CurrentLanguage);

    /// <summary>Returns the name, in the specified language, of this <see cref="T:System.Drawing.FontFamily" />.</summary>
    /// <param name="language">The language in which the name is returned.</param>
    /// <returns>A <see cref="T:System.String" /> that represents the name, in the specified language, of this <see cref="T:System.Drawing.FontFamily" />.</returns>
    public string GetName(int language)
    {
      StringBuilder name = new StringBuilder(32);
      int familyName = SafeNativeMethods.Gdip.GdipGetFamilyName(new HandleRef((object) this, this.NativeFamily), name, language);
      if (familyName != 0)
        throw SafeNativeMethods.Gdip.StatusException(familyName);
      return name.ToString();
    }

    /// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</returns>
    public static FontFamily[] Families => new InstalledFontCollection().Families;

    /// <summary>Gets a generic sans serif <see cref="T:System.Drawing.FontFamily" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.FontFamily" /> object that represents a generic sans serif font.</returns>
    public static FontFamily GenericSansSerif => new FontFamily(FontFamily.GetGdipGenericSansSerif());

    private static IntPtr GetGdipGenericSansSerif()
    {
      IntPtr fontfamily = IntPtr.Zero;
      int fontFamilySansSerif = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySansSerif(out fontfamily);
      if (fontFamilySansSerif != 0)
        throw SafeNativeMethods.Gdip.StatusException(fontFamilySansSerif);
      return fontfamily;
    }

    /// <summary>Gets a generic serif <see cref="T:System.Drawing.FontFamily" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.FontFamily" /> that represents a generic serif font.</returns>
    public static FontFamily GenericSerif => new FontFamily(FontFamily.GetNativeGenericSerif());

    private static IntPtr GetNativeGenericSerif()
    {
      IntPtr fontfamily = IntPtr.Zero;
      int genericFontFamilySerif = SafeNativeMethods.Gdip.GdipGetGenericFontFamilySerif(out fontfamily);
      if (genericFontFamilySerif != 0)
        throw SafeNativeMethods.Gdip.StatusException(genericFontFamilySerif);
      return fontfamily;
    }

    /// <summary>Gets a generic monospace <see cref="T:System.Drawing.FontFamily" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.FontFamily" /> that represents a generic monospace font.</returns>
    public static FontFamily GenericMonospace => new FontFamily(FontFamily.GetNativeGenericMonospace());

    private static IntPtr GetNativeGenericMonospace()
    {
      IntPtr fontfamily = IntPtr.Zero;
      int fontFamilyMonospace = SafeNativeMethods.Gdip.GdipGetGenericFontFamilyMonospace(out fontfamily);
      if (fontFamilyMonospace != 0)
        throw SafeNativeMethods.Gdip.StatusException(fontFamilyMonospace);
      return fontfamily;
    }

    /// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects available for the specified graphics context.</summary>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object from which to return <see cref="T:System.Drawing.FontFamily" /> objects.</param>
    /// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects available for the specified <see cref="T:System.Drawing.Graphics" /> object.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="graphics" /> is <see langword="null" />.</exception>
    [Obsolete("Do not use method GetFamilies, use property Families instead")]
    public static FontFamily[] GetFamilies(Graphics graphics)
    {
      if (graphics == null)
        throw new ArgumentNullException(nameof (graphics));
      return new InstalledFontCollection().Families;
    }

    /// <summary>Indicates whether the specified <see cref="T:System.Drawing.FontStyle" /> enumeration is available.</summary>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> to test.</param>
    /// <returns>
    /// <see langword="true" /> if the specified <see cref="T:System.Drawing.FontStyle" /> is available; otherwise, <see langword="false" />.</returns>
    public bool IsStyleAvailable(FontStyle style)
    {
      int isStyleAvailable;
      int status = SafeNativeMethods.Gdip.GdipIsStyleAvailable(new HandleRef((object) this, this.NativeFamily), style, out isStyleAvailable);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return isStyleAvailable != 0;
    }

    /// <summary>Gets the height, in font design units, of the em square for the specified style.</summary>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> for which to get the em height.</param>
    /// <returns>The height of the em square.</returns>
    public int GetEmHeight(FontStyle style)
    {
      int EmHeight = 0;
      int emHeight = SafeNativeMethods.Gdip.GdipGetEmHeight(new HandleRef((object) this, this.NativeFamily), style, out EmHeight);
      if (emHeight != 0)
        throw SafeNativeMethods.Gdip.StatusException(emHeight);
      return EmHeight;
    }

    /// <summary>Returns the cell ascent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style.</summary>
    /// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font.</param>
    /// <returns>The cell ascent for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
    public int GetCellAscent(FontStyle style)
    {
      int CellAscent = 0;
      int cellAscent = SafeNativeMethods.Gdip.GdipGetCellAscent(new HandleRef((object) this, this.NativeFamily), style, out CellAscent);
      if (cellAscent != 0)
        throw SafeNativeMethods.Gdip.StatusException(cellAscent);
      return CellAscent;
    }

    /// <summary>Returns the cell descent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style.</summary>
    /// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font.</param>
    /// <returns>The cell descent metric for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
    public int GetCellDescent(FontStyle style)
    {
      int CellDescent = 0;
      int cellDescent = SafeNativeMethods.Gdip.GdipGetCellDescent(new HandleRef((object) this, this.NativeFamily), style, out CellDescent);
      if (cellDescent != 0)
        throw SafeNativeMethods.Gdip.StatusException(cellDescent);
      return CellDescent;
    }

    /// <summary>Returns the line spacing, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style. The line spacing is the vertical distance between the base lines of two consecutive lines of text.</summary>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> to apply.</param>
    /// <returns>The distance between two consecutive lines of text.</returns>
    public int GetLineSpacing(FontStyle style)
    {
      int LineSpaceing = 0;
      int lineSpacing = SafeNativeMethods.Gdip.GdipGetLineSpacing(new HandleRef((object) this, this.NativeFamily), style, out LineSpaceing);
      if (lineSpacing != 0)
        throw SafeNativeMethods.Gdip.StatusException(lineSpacing);
      return LineSpaceing;
    }
  }
}
