// Decompiled with JetBrains decompiler
// Type: System.Drawing.SizeFConverter
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
  /// <summary>Converts <see cref="T:System.Drawing.SizeF" /> objects from one type to another.</summary>
  public class SizeFConverter : TypeConverter
  {
    /// <summary>Returns a value indicating whether the converter can convert from the type specified to the <see cref="T:System.Drawing.SizeF" /> type, using the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
    /// <param name="sourceType">A <see cref="T:System.Type" /> the represents the type you wish to convert from.</param>
    /// <returns>
    /// <see langword="true" /> to indicate the conversion can be performed; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    /// <summary>Returns a value indicating whether the <see cref="T:System.Drawing.SizeFConverter" /> can convert a <see cref="T:System.Drawing.SizeF" /> to the specified type.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
    /// <returns>
    /// <see langword="true" /> if this converter can perform the conversion otherwise, <see langword="false" />.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    /// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
    /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
    /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
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
      float[] numArray = new float[strArray.Length];
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (float));
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (float) converter.ConvertFromString(context, culture, strArray[index]);
      return numArray.Length == 2 ? (object) new SizeF(numArray[0], numArray[1]) : throw new ArgumentException(SR.GetString("TextParseFailedFormat", (object) str2, (object) "Width,Height"));
    }

    /// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
    /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
    /// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
    /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException(nameof (destinationType));
      if (destinationType == typeof (string) && value is SizeF sizeF1)
      {
        if (culture == null)
          culture = CultureInfo.CurrentCulture;
        string separator = culture.TextInfo.ListSeparator + " ";
        TypeConverter converter = TypeDescriptor.GetConverter(typeof (float));
        string[] strArray1 = new string[2];
        int num1 = 0;
        string[] strArray2 = strArray1;
        int index1 = num1;
        int num2 = index1 + 1;
        string str1 = converter.ConvertToString(context, culture, (object) sizeF1.Width);
        strArray2[index1] = str1;
        string[] strArray3 = strArray1;
        int index2 = num2;
        int num3 = index2 + 1;
        string str2 = converter.ConvertToString(context, culture, (object) sizeF1.Height);
        strArray3[index2] = str2;
        return (object) string.Join(separator, strArray1);
      }
      if (destinationType == typeof (InstanceDescriptor) && value is SizeF sizeF2)
      {
        ConstructorInfo constructor = typeof (SizeF).GetConstructor(new Type[2]
        {
          typeof (float),
          typeof (float)
        });
        if (constructor != (ConstructorInfo) null)
          return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[2]
          {
            (object) sizeF2.Width,
            (object) sizeF2.Height
          });
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>Creates an instance of a <see cref="T:System.Drawing.SizeF" /> with the specified property values using the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
    /// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> containing property names and values.</param>
    /// <returns>An <see cref="T:System.Object" /> representing the new <see cref="T:System.Drawing.SizeF" />, or <see langword="null" /> if the object cannot be created.</returns>
    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      return (object) new SizeF((float) propertyValues[(object) "Width"], (float) propertyValues[(object) "Height"]);
    }

    /// <summary>Returns a value indicating whether changing a value on this object requires a call to the <see cref="Overload:System.Drawing.SizeFConverter.CreateInstance" /> method to create a new value.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. This may be <see langword="null" />.</param>
    /// <returns>Always returns <see langword="true" />.</returns>
    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

    /// <summary>Retrieves a set of properties for the <see cref="T:System.Drawing.SizeF" /> type using the specified context and attributes.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
    /// <param name="value">The <see cref="T:System.Object" /> to return properties for.</param>
    /// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
    /// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties.</returns>
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(typeof (SizeF), attributes).Sort(new string[2]
      {
        "Width",
        "Height"
      });
    }

    /// <summary>Returns whether the <see cref="T:System.Drawing.SizeF" /> type supports properties.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
    /// <returns>Always returns <see langword="true" />.</returns>
    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
  }
}
