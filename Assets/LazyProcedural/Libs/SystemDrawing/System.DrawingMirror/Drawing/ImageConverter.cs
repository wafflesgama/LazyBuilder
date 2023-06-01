// Decompiled with JetBrains decompiler
// Type: System.Drawing.ImageConverter
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Drawing
{
  /// <summary>
  /// <see cref="T:System.Drawing.ImageConverter" /> is a class that can be used to convert <see cref="T:System.Drawing.Image" /> objects from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
  public class ImageConverter : TypeConverter
  {
    private Type iconType = typeof (Icon);

    /// <summary>Determines whether this <see cref="T:System.Drawing.ImageConverter" /> can convert an instance of a specified type to an <see cref="T:System.Drawing.Image" />, using the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="sourceType">A <see cref="T:System.Type" /> that specifies the type you want to convert from.</param>
    /// <returns>This method returns <see langword="true" /> if this <see cref="T:System.Drawing.ImageConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == this.iconType || sourceType == typeof (byte[]))
        return true;
      return !(sourceType == typeof (InstanceDescriptor)) && base.CanConvertFrom(context, sourceType);
    }

    /// <summary>Determines whether this <see cref="T:System.Drawing.ImageConverter" /> can convert an <see cref="T:System.Drawing.Image" /> to an instance of a specified type, using the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> that specifies the type you want to convert to.</param>
    /// <returns>This method returns <see langword="true" /> if this <see cref="T:System.Drawing.ImageConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (byte[]) || base.CanConvertTo(context, destinationType);

    /// <summary>Converts a specified object to an <see cref="T:System.Drawing.Image" />.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that holds information about a specific culture.</param>
    /// <param name="value">The <see cref="T:System.Object" /> to be converted.</param>
    /// <returns>If this method succeeds, it returns the <see cref="T:System.Drawing.Image" /> that it created by converting the specified object. Otherwise, it throws an exception.</returns>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      switch (value)
      {
        case Icon _:
          return (object) ((Icon) value).ToBitmap();
        case byte[] numArray:
          return (object) Image.FromStream(this.GetBitmapStream(numArray) ?? (Stream) new MemoryStream(numArray));
        default:
          return base.ConvertFrom(context, culture, value);
      }
    }

    /// <summary>Converts an <see cref="T:System.Drawing.Image" /> (or an object that can be cast to an <see cref="T:System.Drawing.Image" />) to the specified type.</summary>
    /// <param name="context">A formatter context. This object can be used to get more information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies formatting conventions used by a particular culture.</param>
    /// <param name="value">The <see cref="T:System.Drawing.Image" /> to convert.</param>
    /// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <see cref="T:System.Drawing.Image" /> to.</param>
    /// <returns>This method returns the converted object.</returns>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException(nameof (destinationType));
      if (destinationType == typeof (string))
        return value != null ? (object) ((Image) value).ToString() : (object) SR.GetString("toStringNone");
      if (!(destinationType == typeof (byte[])))
        return base.ConvertTo(context, culture, value, destinationType);
      if (value == null)
        return (object) new byte[0];
      bool flag = false;
      MemoryStream stream = (MemoryStream) null;
      Image original = (Image) null;
      try
      {
        stream = new MemoryStream();
        original = (Image) value;
        if (original.RawFormat.Equals((object) ImageFormat.Icon))
        {
          flag = true;
          original = (Image) new Bitmap(original, original.Width, original.Height);
        }
        original.Save(stream);
      }
      finally
      {
        stream?.Close();
        if (flag && original != null)
          original.Dispose();
      }
      if (stream != null)
        return (object) stream.ToArray();
      return (object) null;
    }

    private unsafe Stream GetBitmapStream(byte[] rawData)
    {
      try
      {
        fixed (byte* numPtr = rawData)
        {
          IntPtr ptr = (IntPtr) (void*) numPtr;
          if (ptr == IntPtr.Zero || rawData.Length <= sizeof (SafeNativeMethods.OBJECTHEADER) || Marshal.ReadInt16(ptr) != (short) 7189)
            return (Stream) null;
          SafeNativeMethods.OBJECTHEADER structure = (SafeNativeMethods.OBJECTHEADER) Marshal.PtrToStructure(ptr, typeof (SafeNativeMethods.OBJECTHEADER));
          if (rawData.Length <= (int) structure.headersize + 18 || Encoding.ASCII.GetString(rawData, (int) structure.headersize + 12, 6) != "PBrush")
            return (Stream) null;
          byte[] bytes = Encoding.ASCII.GetBytes("BM");
          for (int index = (int) structure.headersize + 18; index < (int) structure.headersize + 510; ++index)
          {
            if (index + 1 < rawData.Length)
            {
              if ((int) bytes[0] == (int) numPtr[index] && (int) bytes[1] == (int) numPtr[index + 1])
                return (Stream) new MemoryStream(rawData, index, rawData.Length - index);
            }
            else
              break;
          }
        }
      }
      catch (OutOfMemoryException ex)
      {
      }
      catch (ArgumentException ex)
      {
      }
      return (Stream) null;
    }

    /// <summary>Gets the set of properties for this type.</summary>
    /// <param name="context">A type descriptor through which additional context can be provided.</param>
    /// <param name="value">The value of the object to get the properties for.</param>
    /// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
    /// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this can return <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(typeof (Image), attributes);
    }

    /// <summary>Indicates whether this object supports properties. By default, this is <see langword="false" />.</summary>
    /// <param name="context">A type descriptor through which additional context can be provided.</param>
    /// <returns>This method returns <see langword="true" /> if the <see cref="Overload:System.Drawing.ImageConverter.GetProperties" /> method should be called to find the properties of this object.</returns>
    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
  }
}
