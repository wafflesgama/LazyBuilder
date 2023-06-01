// Decompiled with JetBrains decompiler
// Type: System.Drawing.RectangleConverter
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
  /// <summary>Converts rectangles from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
  public class RectangleConverter : TypeConverter
  {
    /// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
    /// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="sourceType">The type you want to convert from.</param>
    /// <returns>This method returns <see langword="true" /> if this object can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    /// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context. This can be <see langword="null" />, so you should always check. Also, properties on the context object can also return <see langword="null" />.</param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> object that represents the type you want to convert to.</param>
    /// <returns>This method returns <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    /// <summary>Converts the given object to a <see cref="T:System.Drawing.Rectangle" /> object.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard.</param>
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
      return numArray.Length == 4 ? (object) new Rectangle(numArray[0], numArray[1], numArray[2], numArray[3]) : throw new ArgumentException(SR.GetString("TextParseFailedFormat", (object) "text", (object) str2, (object) "x, y, width, height"));
    }

    /// <summary>Converts the specified object to the specified type.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard.</param>
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
      if (value is Rectangle)
      {
        if (destinationType == typeof (string))
        {
          Rectangle rectangle = (Rectangle) value;
          if (culture == null)
            culture = CultureInfo.CurrentCulture;
          string separator = culture.TextInfo.ListSeparator + " ";
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
          string[] strArray1 = new string[4];
          int num1 = 0;
          string[] strArray2 = strArray1;
          int index1 = num1;
          int num2 = index1 + 1;
          string str1 = converter.ConvertToString(context, culture, (object) rectangle.X);
          strArray2[index1] = str1;
          string[] strArray3 = strArray1;
          int index2 = num2;
          int num3 = index2 + 1;
          string str2 = converter.ConvertToString(context, culture, (object) rectangle.Y);
          strArray3[index2] = str2;
          string[] strArray4 = strArray1;
          int index3 = num3;
          int num4 = index3 + 1;
          string str3 = converter.ConvertToString(context, culture, (object) rectangle.Width);
          strArray4[index3] = str3;
          string[] strArray5 = strArray1;
          int index4 = num4;
          int num5 = index4 + 1;
          string str4 = converter.ConvertToString(context, culture, (object) rectangle.Height);
          strArray5[index4] = str4;
          return (object) string.Join(separator, strArray1);
        }
        if (destinationType == typeof (InstanceDescriptor))
        {
          Rectangle rectangle = (Rectangle) value;
          ConstructorInfo constructor = typeof (Rectangle).GetConstructor(new Type[4]
          {
            typeof (int),
            typeof (int),
            typeof (int),
            typeof (int)
          });
          if (constructor != (ConstructorInfo) null)
            return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[4]
            {
              (object) rectangle.X,
              (object) rectangle.Y,
              (object) rectangle.Width,
              (object) rectangle.Height
            });
        }
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>Creates an instance of this type given a set of property values for the object. This is useful for objects that are immutable but still want to provide changeable properties.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be provided.</param>
    /// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from a call to the <see cref="M:System.Drawing.RectangleConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method.</param>
    /// <returns>The newly created object, or <see langword="null" /> if the object could not be created. The default implementation returns <see langword="null" />.</returns>
    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      object x = propertyValues != null ? propertyValues[(object) "X"] : throw new ArgumentNullException(nameof (propertyValues));
      object propertyValue1 = propertyValues[(object) "Y"];
      object propertyValue2 = propertyValues[(object) "Width"];
      object propertyValue3 = propertyValues[(object) "Height"];
      if (x == null || propertyValue1 == null || propertyValue2 == null || propertyValue3 == null || !(x is int) || !(propertyValue1 is int) || !(propertyValue2 is int) || !(propertyValue3 is int height))
        throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"));
      return (object) new Rectangle((int) x, (int) propertyValue1, (int) propertyValue2, height);
    }

    /// <summary>Determines if changing a value on this object should require a call to <see cref="M:System.Drawing.RectangleConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value.</summary>
    /// <param name="context">A type descriptor through which additional context can be provided.</param>
    /// <returns>This method returns <see langword="true" /> if <see cref="M:System.Drawing.RectangleConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> should be called when a change is made to one or more properties of this object; otherwise, <see langword="false" />.</returns>
    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

    /// <summary>Retrieves the set of properties for this type. By default, a type does not return any properties.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be provided.</param>
    /// <param name="value">The value of the object to get the properties for.</param>
    /// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
    /// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(typeof (Rectangle), attributes).Sort(new string[4]
      {
        "X",
        "Y",
        "Width",
        "Height"
      });
    }

    /// <summary>Determines if this object supports properties. By default, this is <see langword="false" />.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be provided.</param>
    /// <returns>This method returns <see langword="true" /> if <see cref="M:System.Drawing.RectangleConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
  }
}
