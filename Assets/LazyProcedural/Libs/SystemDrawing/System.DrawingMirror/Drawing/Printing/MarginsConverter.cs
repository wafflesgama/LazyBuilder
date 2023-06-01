// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.MarginsConverter
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing.Printing
{
  /// <summary>Provides a <see cref="T:System.Drawing.Printing.MarginsConverter" /> for <see cref="T:System.Drawing.Printing.Margins" />.</summary>
  public class MarginsConverter : ExpandableObjectConverter
  {
    /// <summary>Returns whether this converter can convert an object of the specified source type to the native type of the converter using the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type from which you want to convert.</param>
    /// <returns>
    /// <see langword="true" /> if an object can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    /// <summary>Returns whether this converter can convert an object to the given destination type using the context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type to which you want to convert.</param>
    /// <returns>
    /// <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    /// <summary>Converts the specified object to the converter's native type.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides the language to convert to.</param>
    /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
    /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> does not contain values for all four margins. For example, "100,100,100,100" specifies 1 inch for the left, right, top, and bottom margins.</exception>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string str1))
        return base.ConvertFrom(context, culture, value);
      string str2 = str1.Trim();
      if (str2.Length == 0)
        return (object) null;
      if (culture == null)
        culture = CultureInfo.CurrentCulture;
      char ch = culture.TextInfo.ListSeparator[0];
      string[] strArray = str2.Split(ch);
      int[] numArray = new int[strArray.Length];
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (int) converter.ConvertFromString(context, culture, strArray[index]);
      return numArray.Length == 4 ? (object) new Margins(numArray[0], numArray[1], numArray[2], numArray[3]) : throw new ArgumentException(System.Drawing.SR.GetString("TextParseFailedFormat", (object) str2, (object) "left, right, top, bottom"));
    }

    /// <summary>Converts the given value object to the specified destination type using the specified context and arguments.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides the language to convert to.</param>
    /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
    /// <param name="destinationType">The <see cref="T:System.Type" /> to which to convert the value.</param>
    /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationType" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException(nameof (destinationType));
      if ((object) (value as Margins) != null)
      {
        if (destinationType == typeof (string))
        {
          Margins margins = (Margins) value;
          if (culture == null)
            culture = CultureInfo.CurrentCulture;
          string separator = culture.TextInfo.ListSeparator + " ";
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
          string[] strArray1 = new string[4];
          int num1 = 0;
          string[] strArray2 = strArray1;
          int index1 = num1;
          int num2 = index1 + 1;
          string str1 = converter.ConvertToString(context, culture, (object) margins.Left);
          strArray2[index1] = str1;
          string[] strArray3 = strArray1;
          int index2 = num2;
          int num3 = index2 + 1;
          string str2 = converter.ConvertToString(context, culture, (object) margins.Right);
          strArray3[index2] = str2;
          string[] strArray4 = strArray1;
          int index3 = num3;
          int num4 = index3 + 1;
          string str3 = converter.ConvertToString(context, culture, (object) margins.Top);
          strArray4[index3] = str3;
          string[] strArray5 = strArray1;
          int index4 = num4;
          int num5 = index4 + 1;
          string str4 = converter.ConvertToString(context, culture, (object) margins.Bottom);
          strArray5[index4] = str4;
          return (object) string.Join(separator, strArray1);
        }
        if (destinationType == typeof (InstanceDescriptor))
        {
          Margins margins = (Margins) value;
          ConstructorInfo constructor = typeof (Margins).GetConstructor(new Type[4]
          {
            typeof (int),
            typeof (int),
            typeof (int),
            typeof (int)
          });
          if (constructor != (ConstructorInfo) null)
            return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[4]
            {
              (object) margins.Left,
              (object) margins.Right,
              (object) margins.Top,
              (object) margins.Bottom
            });
        }
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>Creates an <see cref="T:System.Object" /> given a set of property values for the object.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values.</param>
    /// <returns>An <see cref="T:System.Object" /> representing the specified <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="propertyValues" /> is <see langword="null" />.</exception>
    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      object left = propertyValues != null ? propertyValues[(object) "Left"] : throw new ArgumentNullException(nameof (propertyValues));
      object propertyValue1 = propertyValues[(object) "Right"];
      object propertyValue2 = propertyValues[(object) "Top"];
      object propertyValue3 = propertyValues[(object) "Bottom"];
      if (left == null || propertyValue1 == null || propertyValue3 == null || propertyValue2 == null || !(left is int) || !(propertyValue1 is int) || !(propertyValue3 is int) || !(propertyValue2 is int top))
        throw new ArgumentException(System.Drawing.SR.GetString("PropertyValueInvalidEntry"));
      return (object) new Margins((int) left, (int) propertyValue1, top, (int) propertyValue3);
    }

    /// <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.Drawing.Printing.MarginsConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method to create a new value, using the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <returns>
    /// <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.Drawing.Printing.MarginsConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />. This method always returns <see langword="true" />.</returns>
    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;
  }
}
