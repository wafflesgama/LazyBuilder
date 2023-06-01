// Decompiled with JetBrains decompiler
// Type: System.Drawing.SizeConverter
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
  /// <summary>The <see cref="T:System.Drawing.SizeConverter" /> class is used to convert from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
  public class SizeConverter : TypeConverter
  {
    /// <summary>Determines whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="sourceType">The type you want to convert from.</param>
    /// <returns>This method returns <see langword="true" /> if this object can perform the conversion.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    /// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This can be <see langword="null" />, so always check. Also, properties on the context object can return <see langword="null" />.</param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
    /// <returns>This method returns <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    /// <summary>Converts the specified object to the converter's native type.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard.</param>
    /// <param name="value">The object to convert.</param>
    /// <returns>The converted object.</returns>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
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
      return numArray.Length == 2 ? (object) new Size(numArray[0], numArray[1]) : throw new ArgumentException(SR.GetString("TextParseFailedFormat", (object) str2, (object) "Width,Height"));
    }

    /// <summary>Converts the specified object to the specified type.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard.</param>
    /// <param name="value">The object to convert.</param>
    /// <param name="destinationType">The type to convert the object to.</param>
    /// <returns>The converted object.</returns>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException(nameof (destinationType));
      if (value is Size)
      {
        if (destinationType == typeof (string))
        {
          Size size = (Size) value;
          if (culture == null)
            culture = CultureInfo.CurrentCulture;
          string separator = culture.TextInfo.ListSeparator + " ";
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
          string[] strArray1 = new string[2];
          int num1 = 0;
          string[] strArray2 = strArray1;
          int index1 = num1;
          int num2 = index1 + 1;
          string str1 = converter.ConvertToString(context, culture, (object) size.Width);
          strArray2[index1] = str1;
          string[] strArray3 = strArray1;
          int index2 = num2;
          int num3 = index2 + 1;
          string str2 = converter.ConvertToString(context, culture, (object) size.Height);
          strArray3[index2] = str2;
          return (object) string.Join(separator, strArray1);
        }
        if (destinationType == typeof (InstanceDescriptor))
        {
          Size size = (Size) value;
          ConstructorInfo constructor = typeof (Size).GetConstructor(new Type[2]
          {
            typeof (int),
            typeof (int)
          });
          if (constructor != (ConstructorInfo) null)
            return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[2]
            {
              (object) size.Width,
              (object) size.Height
            });
        }
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>Creates an object of this type by using a specified set of property values for the object. This is useful for creating non-changeable objects that have changeable properties.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
    /// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method.</param>
    /// <returns>The newly created object, or <see langword="null" /> if the object could not be created. The default implementation returns <see langword="null" />.</returns>
    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      object width = propertyValues != null ? propertyValues[(object) "Width"] : throw new ArgumentNullException(nameof (propertyValues));
      object propertyValue = propertyValues[(object) "Height"];
      if (width == null || propertyValue == null || !(width is int) || !(propertyValue is int height))
        throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"));
      return (object) new Size((int) width, height);
    }

    /// <summary>Determines whether changing a value on this object should require a call to the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method to create a new value.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
    /// <returns>
    /// <see langword="true" /> if the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> object should be called when a change is made to one or more properties of this object.</returns>
    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

    /// <summary>Retrieves the set of properties for this type. By default, a type does not have any properties to return.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
    /// <param name="value">The value of the object to get the properties for.</param>
    /// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
    /// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(typeof (Size), attributes).Sort(new string[2]
      {
        "Width",
        "Height"
      });
    }

    /// <summary>Determines whether this object supports properties. By default, this is <see langword="false" />.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
    /// <returns>
    /// <see langword="true" /> if the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method should be called to find the properties of this object.</returns>
    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
  }
}
