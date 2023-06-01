// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrinterUnitConvert
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  /// <summary>Specifies a series of conversion methods that are useful when interoperating with the Win32 printing API. This class cannot be inherited.</summary>
  public sealed class PrinterUnitConvert
  {
    private PrinterUnitConvert()
    {
    }

    /// <summary>Converts a double-precision floating-point number from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
    /// <param name="value">The <see cref="T:System.Drawing.Point" /> being converted.</param>
    /// <param name="fromUnit">The unit to convert from.</param>
    /// <param name="toUnit">The unit to convert to.</param>
    /// <returns>A double-precision floating-point number that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
    public static double Convert(double value, PrinterUnit fromUnit, PrinterUnit toUnit)
    {
      double num1 = PrinterUnitConvert.UnitsPerDisplay(fromUnit);
      double num2 = PrinterUnitConvert.UnitsPerDisplay(toUnit);
      return value * num2 / num1;
    }

    /// <summary>Converts a 32-bit signed integer from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
    /// <param name="value">The value being converted.</param>
    /// <param name="fromUnit">The unit to convert from.</param>
    /// <param name="toUnit">The unit to convert to.</param>
    /// <returns>A 32-bit signed integer that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
    public static int Convert(int value, PrinterUnit fromUnit, PrinterUnit toUnit) => (int) Math.Round(PrinterUnitConvert.Convert((double) value, fromUnit, toUnit));

    /// <summary>Converts a <see cref="T:System.Drawing.Point" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
    /// <param name="value">The <see cref="T:System.Drawing.Point" /> being converted.</param>
    /// <param name="fromUnit">The unit to convert from.</param>
    /// <param name="toUnit">The unit to convert to.</param>
    /// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
    public static Point Convert(Point value, PrinterUnit fromUnit, PrinterUnit toUnit) => new Point(PrinterUnitConvert.Convert(value.X, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Y, fromUnit, toUnit));

    /// <summary>Converts a <see cref="T:System.Drawing.Size" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
    /// <param name="value">The <see cref="T:System.Drawing.Size" /> being converted.</param>
    /// <param name="fromUnit">The unit to convert from.</param>
    /// <param name="toUnit">The unit to convert to.</param>
    /// <returns>A <see cref="T:System.Drawing.Size" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
    public static Size Convert(Size value, PrinterUnit fromUnit, PrinterUnit toUnit) => new Size(PrinterUnitConvert.Convert(value.Width, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Height, fromUnit, toUnit));

    /// <summary>Converts a <see cref="T:System.Drawing.Rectangle" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
    /// <param name="value">The <see cref="T:System.Drawing.Rectangle" /> being converted.</param>
    /// <param name="fromUnit">The unit to convert from.</param>
    /// <param name="toUnit">The unit to convert to.</param>
    /// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
    public static Rectangle Convert(Rectangle value, PrinterUnit fromUnit, PrinterUnit toUnit) => new Rectangle(PrinterUnitConvert.Convert(value.X, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Y, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Width, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Height, fromUnit, toUnit));

    /// <summary>Converts a <see cref="T:System.Drawing.Printing.Margins" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
    /// <param name="value">The <see cref="T:System.Drawing.Printing.Margins" /> being converted.</param>
    /// <param name="fromUnit">The unit to convert from.</param>
    /// <param name="toUnit">The unit to convert to.</param>
    /// <returns>A <see cref="T:System.Drawing.Printing.Margins" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
    public static Margins Convert(Margins value, PrinterUnit fromUnit, PrinterUnit toUnit) => new Margins()
    {
      DoubleLeft = PrinterUnitConvert.Convert(value.DoubleLeft, fromUnit, toUnit),
      DoubleRight = PrinterUnitConvert.Convert(value.DoubleRight, fromUnit, toUnit),
      DoubleTop = PrinterUnitConvert.Convert(value.DoubleTop, fromUnit, toUnit),
      DoubleBottom = PrinterUnitConvert.Convert(value.DoubleBottom, fromUnit, toUnit)
    };

    private static double UnitsPerDisplay(PrinterUnit unit)
    {
      double num;
      switch (unit)
      {
        case PrinterUnit.Display:
          num = 1.0;
          break;
        case PrinterUnit.ThousandthsOfAnInch:
          num = 10.0;
          break;
        case PrinterUnit.HundredthsOfAMillimeter:
          num = 25.4;
          break;
        case PrinterUnit.TenthsOfAMillimeter:
          num = 2.54;
          break;
        default:
          num = 1.0;
          break;
      }
      return num;
    }
  }
}
