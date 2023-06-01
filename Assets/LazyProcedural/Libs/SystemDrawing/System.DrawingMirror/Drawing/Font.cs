// Decompiled with JetBrains decompiler
// Type: System.Drawing.Font
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Internal;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing
{
  /// <summary>Defines a particular format for text, including font face, size, and style attributes. This class cannot be inherited.</summary>
  [TypeConverter(typeof (FontConverter))]
  [Editor("System.Drawing.Design.FontEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [ComVisible(true)]
  [Serializable]
  public sealed class Font : MarshalByRefObject, ICloneable, ISerializable, IDisposable
  {
    private const int LogFontCharSetOffset = 23;
    private const int LogFontNameOffset = 28;
    private IntPtr nativeFont;
    private float fontSize;
    private FontStyle fontStyle;
    private FontFamily fontFamily;
    private GraphicsUnit fontUnit;
    private byte gdiCharSet = 1;
    private bool gdiVerticalFont;
    private string systemFontName = "";
    private string originalFontName;

    private void CreateNativeFont()
    {
      int font = SafeNativeMethods.Gdip.GdipCreateFont(new HandleRef((object) this, this.fontFamily.NativeFamily), this.fontSize, this.fontStyle, this.fontUnit, out this.nativeFont);
      switch (font)
      {
        case 0:
          break;
        case 15:
          throw new ArgumentException(SR.GetString("GdiplusFontStyleNotFound", (object) this.fontFamily.Name, (object) this.fontStyle.ToString()));
        default:
          throw SafeNativeMethods.Gdip.StatusException(font);
      }
    }

    private Font(SerializationInfo info, StreamingContext context)
    {
      string familyName = (string) null;
      float emSize = -1f;
      FontStyle style = FontStyle.Regular;
      GraphicsUnit unit = GraphicsUnit.Point;
      SingleConverter singleConverter = new SingleConverter();
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (string.Equals(enumerator.Name, nameof (Name), StringComparison.OrdinalIgnoreCase))
          familyName = (string) enumerator.Value;
        else if (string.Equals(enumerator.Name, nameof (Size), StringComparison.OrdinalIgnoreCase))
          emSize = !(enumerator.Value is string) ? (float) enumerator.Value : (float) singleConverter.ConvertFrom(enumerator.Value);
        else if (string.Compare(enumerator.Name, nameof (Style), true, CultureInfo.InvariantCulture) == 0)
          style = (FontStyle) enumerator.Value;
        else if (string.Compare(enumerator.Name, nameof (Unit), true, CultureInfo.InvariantCulture) == 0)
          unit = (GraphicsUnit) enumerator.Value;
      }
      this.Initialize(familyName, emSize, style, unit, (byte) 1, Font.IsVerticalName(familyName));
    }

    /// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
    /// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
    /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
    {
      si.AddValue("Name", string.IsNullOrEmpty(this.OriginalFontName) ? (object) this.Name : (object) this.OriginalFontName);
      si.AddValue("Size", this.Size);
      si.AddValue("Style", (object) this.Style);
      si.AddValue("Unit", (object) this.Unit);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> that uses the specified existing <see cref="T:System.Drawing.Font" /> and <see cref="T:System.Drawing.FontStyle" /> enumeration.</summary>
    /// <param name="prototype">The existing <see cref="T:System.Drawing.Font" /> from which to create the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="newStyle">The <see cref="T:System.Drawing.FontStyle" /> to apply to the new <see cref="T:System.Drawing.Font" />. Multiple values of the <see cref="T:System.Drawing.FontStyle" /> enumeration can be combined with the <see langword="OR" /> operator.</param>
    public Font(Font prototype, FontStyle newStyle)
    {
      this.originalFontName = prototype.OriginalFontName;
      this.Initialize(prototype.FontFamily, prototype.Size, newStyle, prototype.Unit, (byte) 1, false);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, and unit.</summary>
    /// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
    /// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="family" /> is <see langword="null" />.</exception>
    public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit) => this.Initialize(family, emSize, style, unit, (byte) 1, false);

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
    /// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
    /// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
    /// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a
    /// GDI character set to use for the new font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="family" /> is <see langword="null" />.</exception>
    public Font(
      FontFamily family,
      float emSize,
      FontStyle style,
      GraphicsUnit unit,
      byte gdiCharSet)
    {
      this.Initialize(family, emSize, style, unit, gdiCharSet, false);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
    /// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
    /// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
    /// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a
    /// GDI character set to use for this font.</param>
    /// <param name="gdiVerticalFont">A Boolean value indicating whether the new font is derived from a GDI vertical font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="family" /> is <see langword="null" /></exception>
    public Font(
      FontFamily family,
      float emSize,
      FontStyle style,
      GraphicsUnit unit,
      byte gdiCharSet,
      bool gdiVerticalFont)
    {
      this.Initialize(family, emSize, style, unit, gdiCharSet, gdiVerticalFont);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
    /// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
    /// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
    /// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    public Font(
      string familyName,
      float emSize,
      FontStyle style,
      GraphicsUnit unit,
      byte gdiCharSet)
    {
      this.Initialize(familyName, emSize, style, unit, gdiCharSet, Font.IsVerticalName(familyName));
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using the specified size, style, unit, and character set.</summary>
    /// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
    /// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
    /// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font.</param>
    /// <param name="gdiVerticalFont">A Boolean value indicating whether the new <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    public Font(
      string familyName,
      float emSize,
      FontStyle style,
      GraphicsUnit unit,
      byte gdiCharSet,
      bool gdiVerticalFont)
    {
      if (float.IsNaN(emSize) || float.IsInfinity(emSize) || (double) emSize <= 0.0)
        throw new ArgumentException(SR.GetString("InvalidBoundArgument", (object) nameof (emSize), (object) emSize, (object) 0, (object) "System.Single.MaxValue"), nameof (emSize));
      this.Initialize(familyName, emSize, style, unit, gdiCharSet, gdiVerticalFont);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and style.</summary>
    /// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size, in points, of the new font.</param>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="family" /> is <see langword="null" />.</exception>
    public Font(FontFamily family, float emSize, FontStyle style) => this.Initialize(family, emSize, style, GraphicsUnit.Point, (byte) 1, false);

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and unit. Sets the style to <see cref="F:System.Drawing.FontStyle.Regular" />.</summary>
    /// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
    /// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="family" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    public Font(FontFamily family, float emSize, GraphicsUnit unit) => this.Initialize(family, emSize, FontStyle.Regular, unit, (byte) 1, false);

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size.</summary>
    /// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size, in points, of the new font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    public Font(FontFamily family, float emSize) => this.Initialize(family, emSize, FontStyle.Regular, GraphicsUnit.Point, (byte) 1, false);

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, and unit.</summary>
    /// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
    /// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number.</exception>
    public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit) => this.Initialize(familyName, emSize, style, unit, (byte) 1, Font.IsVerticalName(familyName));

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and style.</summary>
    /// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size, in points, of the new font.</param>
    /// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    public Font(string familyName, float emSize, FontStyle style) => this.Initialize(familyName, emSize, style, GraphicsUnit.Point, (byte) 1, Font.IsVerticalName(familyName));

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and unit. The style is set to <see cref="F:System.Drawing.FontStyle.Regular" />.</summary>
    /// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
    /// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
    public Font(string familyName, float emSize, GraphicsUnit unit) => this.Initialize(familyName, emSize, FontStyle.Regular, unit, (byte) 1, Font.IsVerticalName(familyName));

    /// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size.</summary>
    /// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="emSize">The em-size, in points, of the new font.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number.</exception>
    public Font(string familyName, float emSize) => this.Initialize(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, (byte) 1, Font.IsVerticalName(familyName));

    private Font(IntPtr nativeFont, byte gdiCharSet, bool gdiVerticalFont)
    {
      float size = 0.0f;
      GraphicsUnit unit = GraphicsUnit.Point;
      FontStyle style = FontStyle.Regular;
      IntPtr family1 = IntPtr.Zero;
      this.nativeFont = nativeFont;
      int fontUnit = SafeNativeMethods.Gdip.GdipGetFontUnit(new HandleRef((object) this, nativeFont), out unit);
      if (fontUnit != 0)
        throw SafeNativeMethods.Gdip.StatusException(fontUnit);
      int fontSize = SafeNativeMethods.Gdip.GdipGetFontSize(new HandleRef((object) this, nativeFont), out size);
      if (fontSize != 0)
        throw SafeNativeMethods.Gdip.StatusException(fontSize);
      int fontStyle = SafeNativeMethods.Gdip.GdipGetFontStyle(new HandleRef((object) this, nativeFont), out style);
      if (fontStyle != 0)
        throw SafeNativeMethods.Gdip.StatusException(fontStyle);
      int family2 = SafeNativeMethods.Gdip.GdipGetFamily(new HandleRef((object) this, nativeFont), out family1);
      if (family2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(family2);
      this.SetFontFamily(new FontFamily(family1));
      this.Initialize(this.fontFamily, size, style, unit, gdiCharSet, gdiVerticalFont);
    }

    private void Initialize(
      string familyName,
      float emSize,
      FontStyle style,
      GraphicsUnit unit,
      byte gdiCharSet,
      bool gdiVerticalFont)
    {
      this.originalFontName = familyName;
      this.SetFontFamily(new FontFamily(Font.StripVerticalName(familyName), true));
      this.Initialize(this.fontFamily, emSize, style, unit, gdiCharSet, gdiVerticalFont);
    }

    private void Initialize(
      FontFamily family,
      float emSize,
      FontStyle style,
      GraphicsUnit unit,
      byte gdiCharSet,
      bool gdiVerticalFont)
    {
      if (family == null)
        throw new ArgumentNullException(nameof (family));
      this.fontSize = !float.IsNaN(emSize) && !float.IsInfinity(emSize) && (double) emSize > 0.0 ? emSize : throw new ArgumentException(SR.GetString("InvalidBoundArgument", (object) nameof (emSize), (object) emSize, (object) 0, (object) "System.Single.MaxValue"), nameof (emSize));
      this.fontStyle = style;
      this.fontUnit = unit;
      this.gdiCharSet = gdiCharSet;
      this.gdiVerticalFont = gdiVerticalFont;
      if (this.fontFamily == null)
        this.SetFontFamily(new FontFamily(family.NativeFamily));
      if (this.nativeFont == IntPtr.Zero)
        this.CreateNativeFont();
      int fontSize = SafeNativeMethods.Gdip.GdipGetFontSize(new HandleRef((object) this, this.nativeFont), out this.fontSize);
      if (fontSize != 0)
        throw SafeNativeMethods.Gdip.StatusException(fontSize);
    }

    /// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified Windows handle.</summary>
    /// <param name="hfont">A Windows handle to a GDI font.</param>
    /// <returns>The <see cref="T:System.Drawing.Font" /> this method creates.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="hfont" /> points to an object that is not a TrueType font.</exception>
    public static Font FromHfont(IntPtr hfont)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      SafeNativeMethods.LOGFONT logfont = new SafeNativeMethods.LOGFONT();
      SafeNativeMethods.GetObject(new HandleRef((object) null, hfont), logfont);
      IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
      try
      {
        return Font.FromLogFont((object) logfont, dc);
      }
      finally
      {
        UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef((object) null, dc));
      }
    }

    /// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified GDI logical font (LOGFONT) structure.</summary>
    /// <param name="lf">An <see cref="T:System.Object" /> that represents the GDI <see langword="LOGFONT" /> structure from which to create the <see cref="T:System.Drawing.Font" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Font" /> that this method creates.</returns>
    public static Font FromLogFont(object lf)
    {
      IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
      try
      {
        return Font.FromLogFont(lf, dc);
      }
      finally
      {
        UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef((object) null, dc));
      }
    }

    /// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified GDI logical font (LOGFONT) structure.</summary>
    /// <param name="lf">An <see cref="T:System.Object" /> that represents the GDI <see langword="LOGFONT" /> structure from which to create the <see cref="T:System.Drawing.Font" />.</param>
    /// <param name="hdc">A handle to a device context that contains additional information about the <paramref name="lf" /> structure.</param>
    /// <returns>The <see cref="T:System.Drawing.Font" /> that this method creates.</returns>
    /// <exception cref="T:System.ArgumentException">The font is not a TrueType font.</exception>
    public static Font FromLogFont(object lf, IntPtr hdc)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr font = IntPtr.Zero;
      int status = Marshal.SystemDefaultCharSize != 1 ? SafeNativeMethods.Gdip.GdipCreateFontFromLogfontW(new HandleRef((object) null, hdc), lf, out font) : SafeNativeMethods.Gdip.GdipCreateFontFromLogfontA(new HandleRef((object) null, hdc), lf, out font);
      switch (status)
      {
        case 0:
          if (font == IntPtr.Zero)
            throw new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont", (object) lf.ToString()));
          bool gdiVerticalFont = Marshal.SystemDefaultCharSize != 1 ? Marshal.ReadInt16(lf, 28) == (short) 64 : Marshal.ReadByte(lf, 28) == (byte) 64;
          return new Font(font, Marshal.ReadByte(lf, 23), gdiVerticalFont);
        case 16:
          throw new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont_NoName"));
        default:
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified Windows handle to a device context.</summary>
    /// <param name="hdc">A handle to a device context.</param>
    /// <returns>The <see cref="T:System.Drawing.Font" /> this method creates.</returns>
    /// <exception cref="T:System.ArgumentException">The font for the specified device context is not a TrueType font.</exception>
    public static Font FromHdc(IntPtr hdc)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr zero = IntPtr.Zero;
      int fontFromDc = SafeNativeMethods.Gdip.GdipCreateFontFromDC(new HandleRef((object) null, hdc), ref zero);
      switch (fontFromDc)
      {
        case 0:
          return new Font(zero, (byte) 0, false);
        case 16:
          throw new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont_NoName"));
        default:
          throw SafeNativeMethods.Gdip.StatusException(fontFromDc);
      }
    }

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Font" /> this method creates, cast as an <see cref="T:System.Object" />.</returns>
    public object Clone()
    {
      IntPtr cloneFont = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneFont(new HandleRef((object) this, this.nativeFont), out cloneFont);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new Font(cloneFont, this.gdiCharSet, this.gdiVerticalFont);
    }

    internal IntPtr NativeFont => this.nativeFont;

    /// <summary>Gets the <see cref="T:System.Drawing.FontFamily" /> associated with this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.FontFamily" /> associated with this <see cref="T:System.Drawing.Font" />.</returns>
    [Browsable(false)]
    public FontFamily FontFamily => this.fontFamily;

    private void SetFontFamily(FontFamily family)
    {
      this.fontFamily = family;
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      GC.SuppressFinalize((object) this.fontFamily);
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~Font() => this.Dispose(false);

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Font" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.nativeFont != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDeleteFont(new HandleRef((object) this, this.nativeFont));
      }
      catch (Exception ex)
      {
        if (!ClientUtils.IsCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeFont = IntPtr.Zero;
      }
    }

    private static bool IsVerticalName(string familyName) => familyName != null && familyName.Length > 0 && familyName[0] == '@';

    /// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> is bold.</summary>
    /// <returns>
    /// <see langword="true" /> if this <see cref="T:System.Drawing.Font" /> is bold; otherwise, <see langword="false" />.</returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Bold => (this.Style & FontStyle.Bold) != 0;

    /// <summary>Gets a byte value that specifies the GDI character set that this <see cref="T:System.Drawing.Font" /> uses.</summary>
    /// <returns>A byte value that specifies the GDI character set that this <see cref="T:System.Drawing.Font" /> uses. The default is 1.</returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public byte GdiCharSet => this.gdiCharSet;

    /// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font.</summary>
    /// <returns>
    /// <see langword="true" /> if this <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font; otherwise, <see langword="false" />.</returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool GdiVerticalFont => this.gdiVerticalFont;

    /// <summary>Gets a value that indicates whether this font has the italic style applied.</summary>
    /// <returns>
    /// <see langword="true" /> to indicate this font has the italic style applied; otherwise, <see langword="false" />.</returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Italic => (this.Style & FontStyle.Italic) != 0;

    /// <summary>Gets the face name of this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>A string representation of the face name of this <see cref="T:System.Drawing.Font" />.</returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [TypeConverter(typeof (FontConverter.FontNameConverter))]
    public string Name => this.FontFamily.Name;

    /// <summary>Gets the name of the font originally specified.</summary>
    /// <returns>The string representing the name of the font originally specified.</returns>
    [Browsable(false)]
    public string OriginalFontName => this.originalFontName;

    /// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> specifies a horizontal line through the font.</summary>
    /// <returns>
    /// <see langword="true" /> if this <see cref="T:System.Drawing.Font" /> has a horizontal line through it; otherwise, <see langword="false" />.</returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Strikeout => (this.Style & FontStyle.Strikeout) != 0;

    /// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> is underlined.</summary>
    /// <returns>
    /// <see langword="true" /> if this <see cref="T:System.Drawing.Font" /> is underlined; otherwise, <see langword="false" />.</returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Underline => (this.Style & FontStyle.Underline) != 0;

    /// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.Font" /> and has the same <see cref="P:System.Drawing.Font.FontFamily" />, <see cref="P:System.Drawing.Font.GdiVerticalFont" />, <see cref="P:System.Drawing.Font.GdiCharSet" />, <see cref="P:System.Drawing.Font.Style" />, <see cref="P:System.Drawing.Font.Size" />, and <see cref="P:System.Drawing.Font.Unit" /> property values as this <see cref="T:System.Drawing.Font" />.</summary>
    /// <param name="obj">The object to test.</param>
    /// <returns>
    /// <see langword="true" /> if the <paramref name="obj" /> parameter is a <see cref="T:System.Drawing.Font" /> and has the same <see cref="P:System.Drawing.Font.FontFamily" />, <see cref="P:System.Drawing.Font.GdiVerticalFont" />, <see cref="P:System.Drawing.Font.GdiCharSet" />, <see cref="P:System.Drawing.Font.Style" />, <see cref="P:System.Drawing.Font.Size" />, and <see cref="P:System.Drawing.Font.Unit" /> property values as this <see cref="T:System.Drawing.Font" />; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object obj)
    {
      if (obj == this)
        return true;
      return obj is Font font && font.FontFamily.Equals((object) this.FontFamily) && font.GdiVerticalFont == this.GdiVerticalFont && (int) font.GdiCharSet == (int) this.GdiCharSet && font.Style == this.Style && (double) font.Size == (double) this.Size && font.Unit == this.Unit;
    }

    /// <summary>Gets the hash code for this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>The hash code for this <see cref="T:System.Drawing.Font" />.</returns>
    public override int GetHashCode() => ((int) this.fontStyle << 13 | (int) ((uint) this.fontStyle >> 19)) ^ ((int) this.fontUnit << 26 | (int) ((uint) this.fontUnit >> 6)) ^ ((int) (uint) this.fontSize << 7 | (int) ((uint) this.fontSize >> 25));

    private static string StripVerticalName(string familyName) => familyName != null && familyName.Length > 1 && familyName[0] == '@' ? familyName.Substring(1) : familyName;

    /// <summary>Returns a human-readable string representation of this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>A string that represents this <see cref="T:System.Drawing.Font" />.</returns>
    public override string ToString() => string.Format((IFormatProvider) CultureInfo.CurrentCulture, "[{0}: Name={1}, Size={2}, Units={3}, GdiCharSet={4}, GdiVerticalFont={5}]", (object) this.GetType().Name, (object) this.FontFamily.Name, (object) this.fontSize, (object) (int) this.fontUnit, (object) this.gdiCharSet, (object) this.gdiVerticalFont);

    /// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:System.Drawing.Font" />.</summary>
    /// <param name="logFont">An <see cref="T:System.Object" /> to represent the <see langword="LOGFONT" /> structure that this method creates.</param>
    public void ToLogFont(object logFont)
    {
      IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
      try
      {
        using (Graphics graphics = Graphics.FromHdcInternal(dc))
          this.ToLogFont(logFont, graphics);
      }
      finally
      {
        UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef((object) null, dc));
      }
    }

    /// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:System.Drawing.Font" />.</summary>
    /// <param name="logFont">An <see cref="T:System.Object" /> to represent the <see langword="LOGFONT" /> structure that this method creates.</param>
    /// <param name="graphics">A <see cref="T:System.Drawing.Graphics" /> that provides additional information for the <see langword="LOGFONT" /> structure.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="graphics" /> is <see langword="null" />.</exception>
    public void ToLogFont(object logFont, Graphics graphics)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      if (graphics == null)
        throw new ArgumentNullException(nameof (graphics));
      int status = Marshal.SystemDefaultCharSize != 1 ? SafeNativeMethods.Gdip.GdipGetLogFontW(new HandleRef((object) this, this.NativeFont), new HandleRef((object) graphics, graphics.NativeGraphics), logFont) : SafeNativeMethods.Gdip.GdipGetLogFontA(new HandleRef((object) this, this.NativeFont), new HandleRef((object) graphics, graphics.NativeGraphics), logFont);
      if (this.gdiVerticalFont)
      {
        if (Marshal.SystemDefaultCharSize == 1)
        {
          for (int index = 30; index >= 0; --index)
            Marshal.WriteByte(logFont, 28 + index + 1, Marshal.ReadByte(logFont, 28 + index));
          Marshal.WriteByte(logFont, 28, (byte) 64);
        }
        else
        {
          for (int index = 60; index >= 0; index -= 2)
            Marshal.WriteInt16(logFont, 28 + index + 2, Marshal.ReadInt16(logFont, 28 + index));
          Marshal.WriteInt16(logFont, 28, (short) 64);
        }
      }
      if (Marshal.ReadByte(logFont, 23) == (byte) 0)
        Marshal.WriteByte(logFont, 23, this.gdiCharSet);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Returns a handle to this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>A Windows handle to this <see cref="T:System.Drawing.Font" />.</returns>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The operation was unsuccessful.</exception>
    public IntPtr ToHfont()
    {
      SafeNativeMethods.LOGFONT logfont = new SafeNativeMethods.LOGFONT();
      IntSecurity.ObjectFromWin32Handle.Assert();
      try
      {
        this.ToLogFont((object) logfont);
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      IntPtr fontIndirect = IntUnsafeNativeMethods.IntCreateFontIndirect((object) logfont);
      return !(fontIndirect == IntPtr.Zero) ? fontIndirect : throw new Win32Exception();
    }

    /// <summary>Returns the line spacing, in the current unit of a specified <see cref="T:System.Drawing.Graphics" />, of this font.</summary>
    /// <param name="graphics">A <see cref="T:System.Drawing.Graphics" /> that holds the vertical resolution, in dots per inch, of the display device as well as settings for page unit and page scale.</param>
    /// <returns>The line spacing, in pixels, of this font.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="graphics" /> is <see langword="null" />.</exception>
    public float GetHeight(Graphics graphics)
    {
      if (graphics == null)
        throw new ArgumentNullException(nameof (graphics));
      float size;
      int fontHeight = SafeNativeMethods.Gdip.GdipGetFontHeight(new HandleRef((object) this, this.NativeFont), new HandleRef((object) graphics, graphics.NativeGraphics), out size);
      if (fontHeight != 0)
        throw SafeNativeMethods.Gdip.StatusException(fontHeight);
      return size;
    }

    /// <summary>Returns the line spacing, in pixels, of this font.</summary>
    /// <returns>The line spacing, in pixels, of this font.</returns>
    public float GetHeight()
    {
      IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
      try
      {
        using (Graphics graphics = Graphics.FromHdcInternal(dc))
          return this.GetHeight(graphics);
      }
      finally
      {
        UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef((object) null, dc));
      }
    }

    /// <summary>Returns the height, in pixels, of this <see cref="T:System.Drawing.Font" /> when drawn to a device with the specified vertical resolution.</summary>
    /// <param name="dpi">The vertical resolution, in dots per inch, used to calculate the height of the font.</param>
    /// <returns>The height, in pixels, of this <see cref="T:System.Drawing.Font" />.</returns>
    public float GetHeight(float dpi)
    {
      float size;
      int fontHeightGivenDpi = SafeNativeMethods.Gdip.GdipGetFontHeightGivenDPI(new HandleRef((object) this, this.NativeFont), dpi, out size);
      if (fontHeightGivenDpi != 0)
        throw SafeNativeMethods.Gdip.StatusException(fontHeightGivenDpi);
      return size;
    }

    /// <summary>Gets style information for this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.FontStyle" /> enumeration that contains style information for this <see cref="T:System.Drawing.Font" />.</returns>
    [Browsable(false)]
    public FontStyle Style => this.fontStyle;

    /// <summary>Gets the em-size of this <see cref="T:System.Drawing.Font" /> measured in the units specified by the <see cref="P:System.Drawing.Font.Unit" /> property.</summary>
    /// <returns>The em-size of this <see cref="T:System.Drawing.Font" />.</returns>
    public float Size => this.fontSize;

    /// <summary>Gets the em-size, in points, of this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>The em-size, in points, of this <see cref="T:System.Drawing.Font" />.</returns>
    [Browsable(false)]
    public float SizeInPoints
    {
      get
      {
        if (this.Unit == GraphicsUnit.Point)
          return this.Size;
        IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
        try
        {
          using (Graphics graphics = Graphics.FromHdcInternal(dc))
          {
            float num = graphics.DpiY / 72f;
            return this.GetHeight(graphics) * (float) this.FontFamily.GetEmHeight(this.Style) / (float) this.FontFamily.GetLineSpacing(this.Style) / num;
          }
        }
        finally
        {
          UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef((object) null, dc));
        }
      }
    }

    /// <summary>Gets the unit of measure for this <see cref="T:System.Drawing.Font" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.GraphicsUnit" /> that represents the unit of measure for this <see cref="T:System.Drawing.Font" />.</returns>
    [TypeConverter(typeof (FontConverter.FontUnitConverter))]
    public GraphicsUnit Unit => this.fontUnit;

    /// <summary>Gets the line spacing of this font.</summary>
    /// <returns>The line spacing, in pixels, of this font.</returns>
    [Browsable(false)]
    public int Height => (int) Math.Ceiling((double) this.GetHeight());

    /// <summary>Gets a value indicating whether the font is a member of <see cref="T:System.Drawing.SystemFonts" />.</summary>
    /// <returns>
    /// <see langword="true" /> if the font is a member of <see cref="T:System.Drawing.SystemFonts" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    [Browsable(false)]
    public bool IsSystemFont => !string.IsNullOrEmpty(this.systemFontName);

    /// <summary>Gets the name of the system font if the <see cref="P:System.Drawing.Font.IsSystemFont" /> property returns <see langword="true" />.</summary>
    /// <returns>The name of the system font, if <see cref="P:System.Drawing.Font.IsSystemFont" /> returns <see langword="true" />; otherwise, an empty string ("").</returns>
    [Browsable(false)]
    public string SystemFontName => this.systemFontName;

    internal void SetSystemFontName(string systemFontName) => this.systemFontName = systemFontName;
  }
}
