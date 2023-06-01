// Decompiled with JetBrains decompiler
// Type: System.Drawing.FontConverter
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using Microsoft.Win32;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
  /// <summary>Converts <see cref="T:System.Drawing.Font" /> objects from one data type to another.</summary>
  public class FontConverter : TypeConverter
  {
    private FontConverter.FontNameConverter fontNameConverter;
    private const string styleHdr = "style=";

    /// <summary>Allows the <see cref="T:System.Drawing.FontConverter" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Drawing.FontConverter" /> is reclaimed by garbage collection.</summary>
    ~FontConverter()
    {
      if (this.fontNameConverter == null)
        return;
      ((IDisposable) this.fontNameConverter).Dispose();
    }

    /// <summary>Determines whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
    /// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="sourceType">The type you want to convert from.</param>
    /// <returns>This method returns <see langword="true" /> if this object can perform the conversion.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    /// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
    /// <param name="context">An <see langword="ITypeDescriptorContext" /> object that provides a format context.</param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> object that represents the type you want to convert to.</param>
    /// <returns>This method returns <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    /// <summary>Converts the specified object to the native type of the converter.</summary>
    /// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="culture">A <see langword="CultureInfo" /> object that specifies the culture used to represent the font.</param>
    /// <param name="value">The object to convert.</param>
    /// <returns>The converted object.</returns>
    /// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string str1))
        return base.ConvertFrom(context, culture, value);
      string text1 = str1.Trim();
      if (text1.Length == 0)
        return (object) null;
      if (culture == null)
        culture = CultureInfo.CurrentCulture;
      char separator = culture.TextInfo.ListSeparator[0];
      string str2 = text1;
      string str3 = (string) null;
      float emSize = 8.25f;
      FontStyle fontStyle1 = FontStyle.Regular;
      GraphicsUnit unit = GraphicsUnit.Point;
      int length = text1.IndexOf(separator);
      if (length > 0)
      {
        str2 = text1.Substring(0, length);
        if (length < text1.Length - 1)
        {
          int startIndex = text1.IndexOf("style=");
          string text2;
          if (startIndex != -1)
          {
            str3 = text1.Substring(startIndex, text1.Length - startIndex);
            if (!str3.StartsWith("style="))
              throw this.GetFormatException(text1, separator);
            text2 = text1.Substring(length + 1, startIndex - length - 1);
          }
          else
            text2 = text1.Substring(length + 1, text1.Length - length - 1);
          string[] sizeTokens = this.ParseSizeTokens(text2, separator);
          if (sizeTokens[0] != null)
          {
            try
            {
              emSize = (float) TypeDescriptor.GetConverter(typeof (float)).ConvertFromString(context, culture, sizeTokens[0]);
            }
            catch
            {
              throw this.GetFormatException(text1, separator);
            }
          }
          if (sizeTokens[1] != null)
            unit = this.ParseGraphicsUnits(sizeTokens[1]);
          if (str3 != null)
          {
            int num = str3.IndexOf("=");
            string str4 = str3.Substring(num + 1, str3.Length - "style=".Length);
            char[] chArray = new char[1]{ separator };
            foreach (string str5 in str4.Split(chArray))
            {
              string str6 = str5.Trim();
              try
              {
                fontStyle1 |= (FontStyle) Enum.Parse(typeof (FontStyle), str6, true);
              }
              catch (Exception ex)
              {
                if (!(ex is InvalidEnumArgumentException))
                  throw this.GetFormatException(text1, separator);
                throw;
              }
              FontStyle fontStyle2 = FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout;
              if ((fontStyle1 | fontStyle2) != fontStyle2)
                throw new InvalidEnumArgumentException("style", (int) fontStyle1, typeof (FontStyle));
            }
          }
        }
      }
      if (this.fontNameConverter == null)
        this.fontNameConverter = new FontConverter.FontNameConverter();
      return (object) new Font((string) this.fontNameConverter.ConvertFrom(context, culture, (object) str2), emSize, fontStyle1, unit);
    }

    /// <summary>Converts the specified object to another type.</summary>
    /// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies the culture used to represent the object.</param>
    /// <param name="value">The object to convert.</param>
    /// <param name="destinationType">The data type to convert the object to.</param>
    /// <returns>The converted object.</returns>
    /// <exception cref="T:System.NotSupportedException">The conversion was not successful.</exception>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException(nameof (destinationType));
      if (destinationType == typeof (string))
      {
        if (!(value is Font font))
          return (object) SR.GetString("toStringNone");
        if (culture == null)
          culture = CultureInfo.CurrentCulture;
        string separator = culture.TextInfo.ListSeparator + " ";
        int length = 2;
        if (font.Style != FontStyle.Regular)
          ++length;
        string[] strArray1 = new string[length];
        int num1 = 0;
        string[] strArray2 = strArray1;
        int index1 = num1;
        int num2 = index1 + 1;
        string name = font.Name;
        strArray2[index1] = name;
        string[] strArray3 = strArray1;
        int index2 = num2;
        int num3 = index2 + 1;
        string str1 = TypeDescriptor.GetConverter((object) font.Size).ConvertToString(context, culture, (object) font.Size) + this.GetGraphicsUnitText(font.Unit);
        strArray3[index2] = str1;
        if (font.Style != FontStyle.Regular)
        {
          string[] strArray4 = strArray1;
          int index3 = num3;
          int num4 = index3 + 1;
          string str2 = "style=" + font.Style.ToString("G");
          strArray4[index3] = str2;
        }
        return (object) string.Join(separator, strArray1);
      }
      if (destinationType == typeof (InstanceDescriptor) && value is Font)
      {
        Font font = (Font) value;
        int length = 2;
        if (font.GdiVerticalFont)
          length = 6;
        else if (font.GdiCharSet != (byte) 1)
          length = 5;
        else if (font.Unit != GraphicsUnit.Point)
          length = 4;
        else if (font.Style != FontStyle.Regular)
          ++length;
        object[] arguments = new object[length];
        Type[] types = new Type[length];
        arguments[0] = (object) font.Name;
        types[0] = typeof (string);
        arguments[1] = (object) font.Size;
        types[1] = typeof (float);
        if (length > 2)
        {
          arguments[2] = (object) font.Style;
          types[2] = typeof (FontStyle);
        }
        if (length > 3)
        {
          arguments[3] = (object) font.Unit;
          types[3] = typeof (GraphicsUnit);
        }
        if (length > 4)
        {
          arguments[4] = (object) font.GdiCharSet;
          types[4] = typeof (byte);
        }
        if (length > 5)
        {
          arguments[5] = (object) font.GdiVerticalFont;
          types[5] = typeof (bool);
        }
        MemberInfo constructor = (MemberInfo) typeof (Font).GetConstructor(types);
        if (constructor != (MemberInfo) null)
          return (object) new InstanceDescriptor(constructor, (ICollection) arguments);
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>Creates an object of this type by using a specified set of property values for the object.</summary>
    /// <param name="context">A type descriptor through which additional context can be provided.</param>
    /// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from the <see cref="Overload:System.Drawing.FontConverter.GetProperties" /> method.</param>
    /// <returns>The newly created object, or <see langword="null" /> if the object could not be created. The default implementation returns <see langword="null" />.
    /// <see cref="M:System.Drawing.FontConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> useful for creating non-changeable objects that have changeable properties.</returns>
    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      object familyName = propertyValues != null ? propertyValues[(object) "Name"] : throw new ArgumentNullException(nameof (propertyValues));
      object emSize = propertyValues[(object) "Size"];
      object unit = propertyValues[(object) "Unit"];
      object obj1 = propertyValues[(object) "Bold"];
      object obj2 = propertyValues[(object) "Italic"];
      object obj3 = propertyValues[(object) "Strikeout"];
      object obj4 = propertyValues[(object) "Underline"];
      object gdiCharSet = propertyValues[(object) "GdiCharSet"];
      object gdiVerticalFont = propertyValues[(object) "GdiVerticalFont"];
      if (familyName == null)
        familyName = (object) "Tahoma";
      if (emSize == null)
        emSize = (object) 8f;
      if (unit == null)
        unit = (object) GraphicsUnit.Point;
      if (obj1 == null)
        obj1 = (object) false;
      if (obj2 == null)
        obj2 = (object) false;
      if (obj3 == null)
        obj3 = (object) false;
      if (obj4 == null)
        obj4 = (object) false;
      if (gdiCharSet == null)
        gdiCharSet = (object) (byte) 0;
      if (gdiVerticalFont == null)
        gdiVerticalFont = (object) false;
      if (!(familyName is string) || !(emSize is float) || !(gdiCharSet is byte) || !(unit is GraphicsUnit) || !(obj1 is bool) || !(obj2 is bool) || !(obj3 is bool) || !(obj4 is bool) || !(gdiVerticalFont is bool))
        throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"));
      FontStyle style = FontStyle.Regular;
      if (obj1 != null && (bool) obj1)
        style |= FontStyle.Bold;
      if (obj2 != null && (bool) obj2)
        style |= FontStyle.Italic;
      if (obj3 != null && (bool) obj3)
        style |= FontStyle.Strikeout;
      if (obj4 != null && (bool) obj4)
        style |= FontStyle.Underline;
      return (object) new Font((string) familyName, (float) emSize, style, (GraphicsUnit) unit, (byte) gdiCharSet, (bool) gdiVerticalFont);
    }

    /// <summary>Determines whether changing a value on this object should require a call to the <see cref="Overload:System.Drawing.FontConverter.CreateInstance" /> method to create a new value.</summary>
    /// <param name="context">A type descriptor through which additional context can be provided.</param>
    /// <returns>This method returns <see langword="true" /> if the <see langword="CreateInstance" /> object should be called when a change is made to one or more properties of this object; otherwise, <see langword="false" />.</returns>
    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

    private ArgumentException GetFormatException(string text, char separator)
    {
      string str = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "name{0} size[units[{0} style=style1[{0} style2{0} ...]]]", new object[1]
      {
        (object) separator
      });
      return new ArgumentException(SR.GetString("TextParseFailedFormat", (object) text, (object) str));
    }

    private string GetGraphicsUnitText(GraphicsUnit units)
    {
      string graphicsUnitText = "";
      for (int index = 0; index < FontConverter.UnitName.names.Length; ++index)
      {
        if (FontConverter.UnitName.names[index].unit == units)
        {
          graphicsUnitText = FontConverter.UnitName.names[index].name;
          break;
        }
      }
      return graphicsUnitText;
    }

    /// <summary>Retrieves the set of properties for this type. By default, a type does not have any properties to return.</summary>
    /// <param name="context">A type descriptor through which additional context can be provided.</param>
    /// <param name="value">The value of the object to get the properties for.</param>
    /// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
    /// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return <see langword="null" />. The default implementation always returns <see langword="null" />.
    /// An easy implementation of this method can call the <see cref="Overload:System.ComponentModel.TypeConverter.GetProperties" /> method for the correct data type.</returns>
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(typeof (Font), attributes).Sort(new string[4]
      {
        "Name",
        "Size",
        "Unit",
        "Weight"
      });
    }

    /// <summary>Determines whether this object supports properties. The default is <see langword="false" />.</summary>
    /// <param name="context">A type descriptor through which additional context can be provided.</param>
    /// <returns>This method returns <see langword="true" /> if the <see cref="M:System.Drawing.FontConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext)" /> method should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

    private string[] ParseSizeTokens(string text, char separator)
    {
      string str1 = (string) null;
      string str2 = (string) null;
      text = text.Trim();
      int length = text.Length;
      if (length > 0)
      {
        int num = 0;
        while (num < length && !char.IsLetter(text[num]))
          ++num;
        char[] chArray = new char[2]{ separator, ' ' };
        if (num > 0)
          str1 = text.Substring(0, num).Trim(chArray);
        if (num < length)
          str2 = text.Substring(num).TrimEnd(chArray);
      }
      return new string[2]{ str1, str2 };
    }

    private GraphicsUnit ParseGraphicsUnits(string units)
    {
      FontConverter.UnitName unitName = (FontConverter.UnitName) null;
      for (int index = 0; index < FontConverter.UnitName.names.Length; ++index)
      {
        if (string.Equals(FontConverter.UnitName.names[index].name, units, StringComparison.OrdinalIgnoreCase))
        {
          unitName = FontConverter.UnitName.names[index];
          break;
        }
      }
      return unitName != null ? unitName.unit : throw new ArgumentException(SR.GetString("InvalidArgument", (object) nameof (units), (object) units));
    }

    internal class UnitName
    {
      internal string name;
      internal GraphicsUnit unit;
      internal static readonly FontConverter.UnitName[] names = new FontConverter.UnitName[7]
      {
        new FontConverter.UnitName("world", GraphicsUnit.World),
        new FontConverter.UnitName("display", GraphicsUnit.Display),
        new FontConverter.UnitName("px", GraphicsUnit.Pixel),
        new FontConverter.UnitName("pt", GraphicsUnit.Point),
        new FontConverter.UnitName("in", GraphicsUnit.Inch),
        new FontConverter.UnitName("doc", GraphicsUnit.Document),
        new FontConverter.UnitName("mm", GraphicsUnit.Millimeter)
      };

      internal UnitName(string name, GraphicsUnit unit)
      {
        this.name = name;
        this.unit = unit;
      }
    }

    /// <summary>
    /// <see cref="T:System.Drawing.FontConverter.FontNameConverter" /> is a type converter that is used to convert a font name to and from various other representations.</summary>
    public sealed class FontNameConverter : TypeConverter, IDisposable
    {
      private TypeConverter.StandardValuesCollection values;

      /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.FontConverter.FontNameConverter" /> class.</summary>
      public FontNameConverter() => SystemEvents.InstalledFontsChanged += new EventHandler(this.OnInstalledFontsChanged);

      /// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
      /// <param name="sourceType">The type you wish to convert from.</param>
      /// <returns>
      /// <see langword="true" /> if the converter can perform the conversion; otherwise, <see langword="false" />.</returns>
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

      /// <summary>Converts the given object to the converter's native type.</summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
      /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to use to perform the conversion</param>
      /// <param name="value">The object to convert.</param>
      /// <returns>The converted object.</returns>
      /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        return value is string ? (object) this.MatchFontName((string) value, context) : base.ConvertFrom(context, culture, value);
      }

      /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
      void IDisposable.Dispose() => SystemEvents.InstalledFontsChanged -= new EventHandler(this.OnInstalledFontsChanged);

      /// <summary>Retrieves a collection containing a set of standard values for the data type this converter is designed for.</summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
      /// <returns>A collection containing a standard set of valid values, or <see langword="null" />. The default is <see langword="null" />.</returns>
      public override TypeConverter.StandardValuesCollection GetStandardValues(
        ITypeDescriptorContext context)
      {
        if (this.values == null)
        {
          FontFamily[] families = FontFamily.Families;
          Hashtable hashtable = new Hashtable();
          for (int index = 0; index < families.Length; ++index)
          {
            string name = families[index].Name;
            hashtable[(object) name.ToLower(CultureInfo.InvariantCulture)] = (object) name;
          }
          object[] values = new object[hashtable.Values.Count];
          hashtable.Values.CopyTo((Array) values, 0);
          Array.Sort((Array) values, (IComparer) Comparer.Default);
          this.values = new TypeConverter.StandardValuesCollection((ICollection) values);
        }
        return this.values;
      }

      /// <summary>Determines if the list of standard values returned from the <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" /> method is an exclusive list.</summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
      /// <returns>
      /// <see langword="true" /> if the collection returned from <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" /> is an exclusive list of possible values; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
      public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => false;

      /// <summary>Determines if this object supports a standard set of values that can be picked from a list.</summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
      /// <returns>
      /// <see langword="true" /> if <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
      public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

      private string MatchFontName(string name, ITypeDescriptorContext context)
      {
        string str = (string) null;
        name = name.ToLower(CultureInfo.InvariantCulture);
        IEnumerator enumerator = this.GetStandardValues(context).GetEnumerator();
        while (enumerator.MoveNext())
        {
          string lower = enumerator.Current.ToString().ToLower(CultureInfo.InvariantCulture);
          if (lower.Equals(name))
            return enumerator.Current.ToString();
          if (lower.StartsWith(name) && (str == null || lower.Length <= str.Length))
            str = enumerator.Current.ToString();
        }
        if (str == null)
          str = name;
        return str;
      }

      private void OnInstalledFontsChanged(object sender, EventArgs e) => this.values = (TypeConverter.StandardValuesCollection) null;
    }

    /// <summary>Converts font units to and from other unit types.</summary>
    public class FontUnitConverter : EnumConverter
    {
      /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.FontConverter.FontUnitConverter" /> class.</summary>
      public FontUnitConverter()
        : base(typeof (GraphicsUnit))
      {
      }

      /// <summary>Returns a collection of standard values valid for the <see cref="T:System.Drawing.Font" /> type.</summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
      public override TypeConverter.StandardValuesCollection GetStandardValues(
        ITypeDescriptorContext context)
      {
        if (this.Values == null)
        {
          base.GetStandardValues(context);
          ArrayList values = new ArrayList((ICollection) this.Values);
          values.Remove((object) GraphicsUnit.Display);
          this.Values = new TypeConverter.StandardValuesCollection((ICollection) values);
        }
        return this.Values;
      }
    }
  }
}
