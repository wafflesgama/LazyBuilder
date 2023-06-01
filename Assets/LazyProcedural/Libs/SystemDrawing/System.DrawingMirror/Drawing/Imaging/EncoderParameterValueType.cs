// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.EncoderParameterValueType
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Imaging
{
  /// <summary>Used to specify the data type of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> used with the <see cref="Overload:System.Drawing.Image.Save" /> or <see cref="Overload:System.Drawing.Image.SaveAdd" /> method of an image.</summary>
  public enum EncoderParameterValueType
  {
    /// <summary>Specifies that each value in the array is an 8-bit unsigned integer.</summary>
    ValueTypeByte = 1,
    /// <summary>Specifies that the array of values is a null-terminated ASCII character string. Note that the <see langword="NumberOfValues" /> data member of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object indicates the length of the character string including the NULL terminator.</summary>
    ValueTypeAscii = 2,
    /// <summary>Specifies that each value in the array is a 16-bit, unsigned integer.</summary>
    ValueTypeShort = 3,
    /// <summary>Specifies that each value in the array is a 32-bit unsigned integer.</summary>
    ValueTypeLong = 4,
    /// <summary>Specifies that each value in the array is a pair of 32-bit unsigned integers. Each pair represents a fraction, the first integer being the numerator and the second integer being the denominator.</summary>
    ValueTypeRational = 5,
    /// <summary>Specifies that each value in the array is a pair of 32-bit unsigned integers. Each pair represents a range of numbers.</summary>
    ValueTypeLongRange = 6,
    /// <summary>Specifies that the array of values is an array of bytes that has no data type defined.</summary>
    ValueTypeUndefined = 7,
    /// <summary>Specifies that each value in the array is a set of four, 32-bit unsigned integers. The first two integers represent one fraction, and the second two integers represent a second fraction. The two fractions represent a range of rational numbers. The first fraction is the smallest rational number in the range, and the second fraction is the largest rational number in the range.</summary>
    ValueTypeRationalRange = 8,
  }
}
