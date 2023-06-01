// Decompiled with JetBrains decompiler
// Type: System.Drawing.IconConverter
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;

namespace System.Drawing
{
  /// <summary>Converts an <see cref="T:System.Drawing.Icon" /> object from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
  public class IconConverter : ExpandableObjectConverter
  {
    /// <summary>Determines whether this <see cref="T:System.Drawing.IconConverter" /> can convert an instance of a specified type to an <see cref="T:System.Drawing.Icon" />, using the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="sourceType">A <see cref="T:System.Type" /> that specifies the type you want to convert from.</param>
    /// <returns>This method returns <see langword="true" /> if this <see cref="T:System.Drawing.IconConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof (byte[]))
        return true;
      return !(sourceType == typeof (InstanceDescriptor)) && base.CanConvertFrom(context, sourceType);
    }

    /// <summary>Determines whether this <see cref="T:System.Drawing.IconConverter" /> can convert an <see cref="T:System.Drawing.Icon" /> to an instance of a specified type, using the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> that specifies the type you want to convert to.</param>
    /// <returns>This method returns <see langword="true" /> if this <see cref="T:System.Drawing.IconConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (Image) || destinationType == typeof (Bitmap) || destinationType == typeof (byte[]) || base.CanConvertTo(context, destinationType);

    /// <summary>Converts a specified object to an <see cref="T:System.Drawing.Icon" />.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that holds information about a specific culture.</param>
    /// <param name="value">The <see cref="T:System.Object" /> to be converted.</param>
    /// <returns>If this method succeeds, it returns the <see cref="T:System.Drawing.Icon" /> that it created by converting the specified object. Otherwise, it throws an exception.</returns>
    /// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return value is byte[] ? (object) new Icon((Stream) new MemoryStream((byte[]) value)) : base.ConvertFrom(context, culture, value);
    }

    /// <summary>Converts an <see cref="T:System.Drawing.Icon" /> (or an object that can be cast to an <see cref="T:System.Drawing.Icon" />) to a specified type.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies formatting conventions used by a particular culture.</param>
    /// <param name="value">The object to convert. This object should be of type icon or some type that can be cast to <see cref="T:System.Drawing.Icon" />.</param>
    /// <param name="destinationType">The type to convert the icon to.</param>
    /// <returns>This method returns the converted object.</returns>
    /// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException(nameof (destinationType));
      if ((destinationType == typeof (Image) || destinationType == typeof (Bitmap)) && value is Icon icon1)
        return (object) icon1.ToBitmap();
      if (destinationType == typeof (string))
        return value != null ? (object) value.ToString() : (object) SR.GetString("toStringNone");
      if (!(destinationType == typeof (byte[])))
        return base.ConvertTo(context, culture, value, destinationType);
      if (value == null)
        return (object) new byte[0];
      MemoryStream outputStream = (MemoryStream) null;
      try
      {
        outputStream = new MemoryStream();
        if (value is Icon icon2)
          icon2.Save((Stream) outputStream);
      }
      finally
      {
        outputStream?.Close();
      }
      if (outputStream != null)
        return (object) outputStream.ToArray();
      return (object) null;
    }
  }
}
