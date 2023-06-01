// Decompiled with JetBrains decompiler
// Type: System.Drawing.ColorConverter
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
  /// <summary>Converts colors from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
  public class ColorConverter : TypeConverter
  {
    private static string ColorConstantsLock = nameof (colorConstants);
    private static Hashtable colorConstants;
    private static string SystemColorConstantsLock = nameof (systemColorConstants);
    private static Hashtable systemColorConstants;
    private static string ValuesLock = nameof (values);
    private static TypeConverter.StandardValuesCollection values;

    private static Hashtable Colors
    {
      get
      {
        if (ColorConverter.colorConstants == null)
        {
          lock (ColorConverter.ColorConstantsLock)
          {
            if (ColorConverter.colorConstants == null)
            {
              Hashtable hash = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
              ColorConverter.FillConstants(hash, typeof (Color));
              ColorConverter.colorConstants = hash;
            }
          }
        }
        return ColorConverter.colorConstants;
      }
    }

    private static Hashtable SystemColors
    {
      get
      {
        if (ColorConverter.systemColorConstants == null)
        {
          lock (ColorConverter.SystemColorConstantsLock)
          {
            if (ColorConverter.systemColorConstants == null)
            {
              Hashtable hash = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
              ColorConverter.FillConstants(hash, typeof (System.Drawing.SystemColors));
              ColorConverter.systemColorConstants = hash;
            }
          }
        }
        return ColorConverter.systemColorConstants;
      }
    }

    /// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked.</param>
    /// <param name="sourceType">The type from which you want to convert.</param>
    /// <returns>
    /// <see langword="true" /> if this object can perform the conversion; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    /// <summary>Returns a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type to which you want to convert.</param>
    /// <returns>
    /// <see langword="true" /> if this converter can perform the operation; otherwise, <see langword="false" />.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    internal static object GetNamedColor(string name) => ColorConverter.Colors[(object) name] ?? ColorConverter.SystemColors[(object) name];

    /// <summary>Converts the given object to the converter's native type.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the color.</param>
    /// <param name="value">The object to convert.</param>
    /// <returns>An <see cref="T:System.Object" /> representing the converted value.</returns>
    /// <exception cref="T:System.ArgumentException">The conversion cannot be performed.</exception>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string str1))
        return base.ConvertFrom(context, culture, value);
      string str2 = str1.Trim();
      object obj;
      if (str2.Length == 0)
      {
        obj = (object) Color.Empty;
      }
      else
      {
        obj = ColorConverter.GetNamedColor(str2);
        if (obj == null)
        {
          if (culture == null)
            culture = CultureInfo.CurrentCulture;
          char ch = culture.TextInfo.ListSeparator[0];
          bool flag = true;
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
          if (str2.IndexOf(ch) == -1)
          {
            if (str2.Length >= 2 && (str2[0] == '\'' || str2[0] == '"') && (int) str2[0] == (int) str2[str2.Length - 1])
            {
              obj = (object) Color.FromName(str2.Substring(1, str2.Length - 2));
              flag = false;
            }
            else if (str2.Length == 7 && str2[0] == '#' || str2.Length == 8 && (str2.StartsWith("0x") || str2.StartsWith("0X")) || str2.Length == 8 && (str2.StartsWith("&h") || str2.StartsWith("&H")))
              obj = (object) Color.FromArgb(-16777216 | (int) converter.ConvertFromString(context, culture, str2));
          }
          if (obj == null)
          {
            string[] strArray = str2.Split(ch);
            int[] numArray = new int[strArray.Length];
            for (int index = 0; index < numArray.Length; ++index)
              numArray[index] = (int) converter.ConvertFromString(context, culture, strArray[index]);
            switch (numArray.Length)
            {
              case 1:
                obj = (object) Color.FromArgb(numArray[0]);
                break;
              case 3:
                obj = (object) Color.FromArgb(numArray[0], numArray[1], numArray[2]);
                break;
              case 4:
                obj = (object) Color.FromArgb(numArray[0], numArray[1], numArray[2], numArray[3]);
                break;
            }
            flag = true;
          }
          if (obj != null & flag)
          {
            int argb = ((Color) obj).ToArgb();
            foreach (Color color in (IEnumerable) ColorConverter.Colors.Values)
            {
              if (color.ToArgb() == argb)
              {
                obj = (object) color;
                break;
              }
            }
          }
        }
        if (obj == null)
          throw new ArgumentException(SR.GetString("InvalidColor", (object) str2));
      }
      return obj;
    }

    /// <summary>Converts the specified object to another type.</summary>
    /// <param name="context">A formatter context. Use this object to extract additional information about the environment from which this converter is being invoked. Always check whether this value is <see langword="null" />. Also, properties on the context object may return <see langword="null" />.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the color.</param>
    /// <param name="value">The object to convert.</param>
    /// <param name="destinationType">The type to convert the object to.</param>
    /// <returns>An <see cref="T:System.Object" /> representing the converted value.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationtype" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException(nameof (destinationType));
      if (value is Color)
      {
        if (destinationType == typeof (string))
        {
          Color color = (Color) value;
          if (color == Color.Empty)
            return (object) string.Empty;
          if (color.IsKnownColor)
            return (object) color.Name;
          if (color.IsNamedColor)
            return (object) ("'" + color.Name + "'");
          if (culture == null)
            culture = CultureInfo.CurrentCulture;
          string separator = culture.TextInfo.ListSeparator + " ";
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
          int num1 = 0;
          string[] strArray1;
          if (color.A < byte.MaxValue)
          {
            strArray1 = new string[4];
            strArray1[num1++] = converter.ConvertToString(context, culture, (object) color.A);
          }
          else
            strArray1 = new string[3];
          string[] strArray2 = strArray1;
          int index1 = num1;
          int num2 = index1 + 1;
          string str1 = converter.ConvertToString(context, culture, (object) color.R);
          strArray2[index1] = str1;
          string[] strArray3 = strArray1;
          int index2 = num2;
          int num3 = index2 + 1;
          string str2 = converter.ConvertToString(context, culture, (object) color.G);
          strArray3[index2] = str2;
          string[] strArray4 = strArray1;
          int index3 = num3;
          int num4 = index3 + 1;
          string str3 = converter.ConvertToString(context, culture, (object) color.B);
          strArray4[index3] = str3;
          return (object) string.Join(separator, strArray1);
        }
        if (destinationType == typeof (InstanceDescriptor))
        {
          object[] arguments = (object[]) null;
          Color color = (Color) value;
          MemberInfo member;
          if (color.IsEmpty)
            member = (MemberInfo) typeof (Color).GetField("Empty");
          else if (color.IsSystemColor)
            member = (MemberInfo) typeof (System.Drawing.SystemColors).GetProperty(color.Name);
          else if (color.IsKnownColor)
            member = (MemberInfo) typeof (Color).GetProperty(color.Name);
          else if (color.A != byte.MaxValue)
          {
            member = (MemberInfo) typeof (Color).GetMethod("FromArgb", new Type[4]
            {
              typeof (int),
              typeof (int),
              typeof (int),
              typeof (int)
            });
            arguments = new object[4]
            {
              (object) color.A,
              (object) color.R,
              (object) color.G,
              (object) color.B
            };
          }
          else if (color.IsNamedColor)
          {
            member = (MemberInfo) typeof (Color).GetMethod("FromName", new Type[1]
            {
              typeof (string)
            });
            arguments = new object[1]{ (object) color.Name };
          }
          else
          {
            member = (MemberInfo) typeof (Color).GetMethod("FromArgb", new Type[3]
            {
              typeof (int),
              typeof (int),
              typeof (int)
            });
            arguments = new object[3]
            {
              (object) color.R,
              (object) color.G,
              (object) color.B
            };
          }
          return member != (MemberInfo) null ? (object) new InstanceDescriptor(member, (ICollection) arguments) : (object) null;
        }
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    private static void FillConstants(Hashtable hash, Type enumType)
    {
      MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;
      foreach (PropertyInfo property in enumType.GetProperties())
      {
        if (property.PropertyType == typeof (Color))
        {
          MethodInfo getMethod = property.GetGetMethod();
          if (getMethod != (MethodInfo) null && (getMethod.Attributes & methodAttributes) == methodAttributes)
          {
            object[] index = (object[]) null;
            hash[(object) property.Name] = property.GetValue((object) null, index);
          }
        }
      }
    }

    /// <summary>Retrieves a collection containing a set of standard values for the data type for which this validator is designed. This will return <see langword="null" /> if the data type does not support a standard set of values.</summary>
    /// <param name="context">A formatter context. Use this object to extract additional information about the environment from which this converter is being invoked. Always check whether this value is <see langword="null" />. Also, properties on the context object may return <see langword="null" />.</param>
    /// <returns>A collection containing <see langword="null" /> or a standard set of valid values. The default implementation always returns <see langword="null" />.</returns>
    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      if (ColorConverter.values == null)
      {
        lock (ColorConverter.ValuesLock)
        {
          if (ColorConverter.values == null)
          {
            ArrayList arrayList = new ArrayList();
            arrayList.AddRange(ColorConverter.Colors.Values);
            arrayList.AddRange(ColorConverter.SystemColors.Values);
            int count = arrayList.Count;
            for (int index1 = 0; index1 < count - 1; ++index1)
            {
              for (int index2 = index1 + 1; index2 < count; ++index2)
              {
                if (arrayList[index1].Equals(arrayList[index2]))
                {
                  arrayList.RemoveAt(index2);
                  --count;
                  --index2;
                }
              }
            }
            arrayList.Sort(0, arrayList.Count, (IComparer) new ColorConverter.ColorComparer());
            ColorConverter.values = new TypeConverter.StandardValuesCollection((ICollection) arrayList.ToArray());
          }
        }
      }
      return ColorConverter.values;
    }

    /// <summary>Determines if this object supports a standard set of values that can be chosen from a list.</summary>
    /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
    /// <returns>
    /// <see langword="true" /> if <see cref="Overload:System.Drawing.ColorConverter.GetStandardValues" /> must be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

    private class ColorComparer : IComparer
    {
      public int Compare(object left, object right) => string.Compare(((Color) left).Name, ((Color) right).Name, false, CultureInfo.InvariantCulture);
    }
  }
}
