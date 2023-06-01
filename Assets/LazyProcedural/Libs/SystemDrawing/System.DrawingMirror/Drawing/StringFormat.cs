// Decompiled with JetBrains decompiler
// Type: System.Drawing.StringFormat
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Drawing
{
  /// <summary>Encapsulates text layout information (such as alignment, orientation and tab stops) display manipulations (such as ellipsis insertion and national digit substitution) and OpenType features. This class cannot be inherited.</summary>
  public sealed class StringFormat : MarshalByRefObject, ICloneable, IDisposable
  {
    internal IntPtr nativeFormat;

    private StringFormat(IntPtr format) => this.nativeFormat = format;

    /// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    public StringFormat()
      : this((StringFormatFlags) 0, 0)
    {
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object with the specified <see cref="T:System.Drawing.StringFormatFlags" /> enumeration.</summary>
    /// <param name="options">The <see cref="T:System.Drawing.StringFormatFlags" /> enumeration for the new <see cref="T:System.Drawing.StringFormat" /> object.</param>
    public StringFormat(StringFormatFlags options)
      : this(options, 0)
    {
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object with the specified <see cref="T:System.Drawing.StringFormatFlags" /> enumeration and language.</summary>
    /// <param name="options">The <see cref="T:System.Drawing.StringFormatFlags" /> enumeration for the new <see cref="T:System.Drawing.StringFormat" /> object.</param>
    /// <param name="language">A value that indicates the language of the text.</param>
    public StringFormat(StringFormatFlags options, int language)
    {
      int stringFormat = SafeNativeMethods.Gdip.GdipCreateStringFormat(options, language, out this.nativeFormat);
      if (stringFormat != 0)
        throw SafeNativeMethods.Gdip.StatusException(stringFormat);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object from the specified existing <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    /// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> object from which to initialize the new <see cref="T:System.Drawing.StringFormat" /> object.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> is <see langword="null" />.</exception>
    public StringFormat(StringFormat format)
    {
      int status = format != null ? SafeNativeMethods.Gdip.GdipCloneStringFormat(new HandleRef((object) format, format.nativeFormat), out this.nativeFormat) : throw new ArgumentNullException(nameof (format));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.nativeFormat != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDeleteStringFormat(new HandleRef((object) this, this.nativeFormat));
      }
      catch (Exception ex)
      {
        if (!ClientUtils.IsCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeFormat = IntPtr.Zero;
      }
    }

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    /// <returns>The <see cref="T:System.Drawing.StringFormat" /> object this method creates.</returns>
    public object Clone()
    {
      IntPtr newFormat = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneStringFormat(new HandleRef((object) this, this.nativeFormat), out newFormat);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new StringFormat(newFormat);
    }

    /// <summary>Gets or sets a <see cref="T:System.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</summary>
    /// <returns>A <see cref="T:System.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</returns>
    public StringFormatFlags FormatFlags
    {
      get
      {
        StringFormatFlags result;
        int stringFormatFlags = SafeNativeMethods.Gdip.GdipGetStringFormatFlags(new HandleRef((object) this, this.nativeFormat), out result);
        if (stringFormatFlags != 0)
          throw SafeNativeMethods.Gdip.StatusException(stringFormatFlags);
        return result;
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetStringFormatFlags(new HandleRef((object) this, this.nativeFormat), value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Specifies an array of <see cref="T:System.Drawing.CharacterRange" /> structures that represent the ranges of characters measured by a call to the <see cref="M:System.Drawing.Graphics.MeasureCharacterRanges(System.String,System.Drawing.Font,System.Drawing.RectangleF,System.Drawing.StringFormat)" /> method.</summary>
    /// <param name="ranges">An array of <see cref="T:System.Drawing.CharacterRange" /> structures that specifies the ranges of characters measured by a call to the <see cref="M:System.Drawing.Graphics.MeasureCharacterRanges(System.String,System.Drawing.Font,System.Drawing.RectangleF,System.Drawing.StringFormat)" /> method.</param>
    /// <exception cref="T:System.OverflowException">More than 32 character ranges are set.</exception>
    public void SetMeasurableCharacterRanges(CharacterRange[] ranges)
    {
      int status = SafeNativeMethods.Gdip.GdipSetStringFormatMeasurableCharacterRanges(new HandleRef((object) this, this.nativeFormat), ranges.Length, ranges);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets or sets horizontal alignment of the string.</summary>
    /// <returns>A <see cref="T:System.Drawing.StringAlignment" /> enumeration that specifies the horizontal  alignment of the string.</returns>
    public StringAlignment Alignment
    {
      get
      {
        StringAlignment align = StringAlignment.Near;
        int stringFormatAlign = SafeNativeMethods.Gdip.GdipGetStringFormatAlign(new HandleRef((object) this, this.nativeFormat), out align);
        if (stringFormatAlign != 0)
          throw SafeNativeMethods.Gdip.StatusException(stringFormatAlign);
        return align;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 2) ? SafeNativeMethods.Gdip.GdipSetStringFormatAlign(new HandleRef((object) this, this.nativeFormat), value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (StringAlignment));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the vertical alignment of the string.</summary>
    /// <returns>A <see cref="T:System.Drawing.StringAlignment" /> enumeration that represents the vertical line alignment.</returns>
    public StringAlignment LineAlignment
    {
      get
      {
        StringAlignment align = StringAlignment.Near;
        int stringFormatLineAlign = SafeNativeMethods.Gdip.GdipGetStringFormatLineAlign(new HandleRef((object) this, this.nativeFormat), out align);
        if (stringFormatLineAlign != 0)
          throw SafeNativeMethods.Gdip.StatusException(stringFormatLineAlign);
        return align;
      }
      set
      {
        int status = value >= StringAlignment.Near && value <= StringAlignment.Far ? SafeNativeMethods.Gdip.GdipSetStringFormatLineAlign(new HandleRef((object) this, this.nativeFormat), value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (StringAlignment));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the <see cref="T:System.Drawing.Text.HotkeyPrefix" /> object for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    /// <returns>The <see cref="T:System.Drawing.Text.HotkeyPrefix" /> object for this <see cref="T:System.Drawing.StringFormat" /> object, the default is <see cref="F:System.Drawing.Text.HotkeyPrefix.None" />.</returns>
    public HotkeyPrefix HotkeyPrefix
    {
      get
      {
        HotkeyPrefix hotkeyPrefix;
        int formatHotkeyPrefix = SafeNativeMethods.Gdip.GdipGetStringFormatHotkeyPrefix(new HandleRef((object) this, this.nativeFormat), out hotkeyPrefix);
        if (formatHotkeyPrefix != 0)
          throw SafeNativeMethods.Gdip.StatusException(formatHotkeyPrefix);
        return hotkeyPrefix;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 2) ? SafeNativeMethods.Gdip.GdipSetStringFormatHotkeyPrefix(new HandleRef((object) this, this.nativeFormat), value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (HotkeyPrefix));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Sets tab stops for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    /// <param name="firstTabOffset">The number of spaces between the beginning of a line of text and the first tab stop.</param>
    /// <param name="tabStops">An array of distances between tab stops in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property.</param>
    public void SetTabStops(float firstTabOffset, float[] tabStops)
    {
      if ((double) firstTabOffset < 0.0)
        throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (firstTabOffset), (object) firstTabOffset));
      int status = SafeNativeMethods.Gdip.GdipSetStringFormatTabStops(new HandleRef((object) this, this.nativeFormat), firstTabOffset, tabStops.Length, tabStops);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets the tab stops for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    /// <param name="firstTabOffset">The number of spaces between the beginning of a text line and the first tab stop.</param>
    /// <returns>An array of distances (in number of spaces) between tab stops.</returns>
    public float[] GetTabStops(out float firstTabOffset)
    {
      int count = 0;
      int formatTabStopCount = SafeNativeMethods.Gdip.GdipGetStringFormatTabStopCount(new HandleRef((object) this, this.nativeFormat), out count);
      if (formatTabStopCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(formatTabStopCount);
      float[] tabStops = new float[count];
      int stringFormatTabStops = SafeNativeMethods.Gdip.GdipGetStringFormatTabStops(new HandleRef((object) this, this.nativeFormat), count, out firstTabOffset, tabStops);
      if (stringFormatTabStops != 0)
        throw SafeNativeMethods.Gdip.StatusException(stringFormatTabStops);
      return tabStops;
    }

    /// <summary>Gets or sets the <see cref="T:System.Drawing.StringTrimming" /> enumeration for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.StringTrimming" /> enumeration that indicates how text drawn with this <see cref="T:System.Drawing.StringFormat" /> object is trimmed when it exceeds the edges of the layout rectangle.</returns>
    public StringTrimming Trimming
    {
      get
      {
        StringTrimming trimming;
        int stringFormatTrimming = SafeNativeMethods.Gdip.GdipGetStringFormatTrimming(new HandleRef((object) this, this.nativeFormat), out trimming);
        if (stringFormatTrimming != 0)
          throw SafeNativeMethods.Gdip.StatusException(stringFormatTrimming);
        return trimming;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 5) ? SafeNativeMethods.Gdip.GdipSetStringFormatTrimming(new HandleRef((object) this, this.nativeFormat), value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (StringTrimming));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets a generic default <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    /// <returns>The generic default <see cref="T:System.Drawing.StringFormat" /> object.</returns>
    public static StringFormat GenericDefault
    {
      get
      {
        IntPtr format;
        int genericDefault = SafeNativeMethods.Gdip.GdipStringFormatGetGenericDefault(out format);
        if (genericDefault != 0)
          throw SafeNativeMethods.Gdip.StatusException(genericDefault);
        return new StringFormat(format);
      }
    }

    /// <summary>Gets a generic typographic <see cref="T:System.Drawing.StringFormat" /> object.</summary>
    /// <returns>A generic typographic <see cref="T:System.Drawing.StringFormat" /> object.</returns>
    public static StringFormat GenericTypographic
    {
      get
      {
        IntPtr format;
        int genericTypographic = SafeNativeMethods.Gdip.GdipStringFormatGetGenericTypographic(out format);
        if (genericTypographic != 0)
          throw SafeNativeMethods.Gdip.StatusException(genericTypographic);
        return new StringFormat(format);
      }
    }

    /// <summary>Specifies the language and method to be used when local digits are substituted for western digits.</summary>
    /// <param name="language">A National Language Support (NLS) language identifier that identifies the language that will be used when local digits are substituted for western digits. You can pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of a <see cref="T:System.Globalization.CultureInfo" /> object as the NLS language identifier. For example, suppose you create a <see cref="T:System.Globalization.CultureInfo" /> object by passing the string "ar-EG" to a <see cref="T:System.Globalization.CultureInfo" /> constructor. If you pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of that <see cref="T:System.Globalization.CultureInfo" /> object along with <see cref="F:System.Drawing.StringDigitSubstitute.Traditional" /> to the <see cref="M:System.Drawing.StringFormat.SetDigitSubstitution(System.Int32,System.Drawing.StringDigitSubstitute)" /> method, then Arabic-Indic digits will be substituted for western digits at display time.</param>
    /// <param name="substitute">An element of the <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration that specifies how digits are displayed.</param>
    public void SetDigitSubstitution(int language, StringDigitSubstitute substitute)
    {
      int status = SafeNativeMethods.Gdip.GdipSetStringFormatDigitSubstitution(new HandleRef((object) this, this.nativeFormat), language, substitute);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets the method to be used for digit substitution.</summary>
    /// <returns>A <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration value that specifies how to substitute characters in a string that cannot be displayed because they are not supported by the current font.</returns>
    public StringDigitSubstitute DigitSubstitutionMethod
    {
      get
      {
        int langID = 0;
        StringDigitSubstitute sds;
        int digitSubstitution = SafeNativeMethods.Gdip.GdipGetStringFormatDigitSubstitution(new HandleRef((object) this, this.nativeFormat), out langID, out sds);
        if (digitSubstitution != 0)
          throw SafeNativeMethods.Gdip.StatusException(digitSubstitution);
        return sds;
      }
    }

    /// <summary>Gets the language that is used when local digits are substituted for western digits.</summary>
    /// <returns>A National Language Support (NLS) language identifier that identifies the language that will be used when local digits are substituted for western digits. You can pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of a <see cref="T:System.Globalization.CultureInfo" /> object as the NLS language identifier. For example, suppose you create a <see cref="T:System.Globalization.CultureInfo" /> object by passing the string "ar-EG" to a <see cref="T:System.Globalization.CultureInfo" /> constructor. If you pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of that <see cref="T:System.Globalization.CultureInfo" /> object along with <see cref="F:System.Drawing.StringDigitSubstitute.Traditional" /> to the <see cref="M:System.Drawing.StringFormat.SetDigitSubstitution(System.Int32,System.Drawing.StringDigitSubstitute)" /> method, then Arabic-Indic digits will be substituted for western digits at display time.</returns>
    public int DigitSubstitutionLanguage
    {
      get
      {
        int langID = 0;
        int digitSubstitution = SafeNativeMethods.Gdip.GdipGetStringFormatDigitSubstitution(new HandleRef((object) this, this.nativeFormat), out langID, out StringDigitSubstitute _);
        if (digitSubstitution != 0)
          throw SafeNativeMethods.Gdip.StatusException(digitSubstitution);
        return langID;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~StringFormat() => this.Dispose(false);

    /// <summary>Converts this <see cref="T:System.Drawing.StringFormat" /> object to a human-readable string.</summary>
    /// <returns>A string representation of this <see cref="T:System.Drawing.StringFormat" /> object.</returns>
    public override string ToString() => "[StringFormat, FormatFlags=" + this.FormatFlags.ToString() + "]";
  }
}
