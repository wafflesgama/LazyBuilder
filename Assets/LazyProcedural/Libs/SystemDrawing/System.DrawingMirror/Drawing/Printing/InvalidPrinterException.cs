// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.InvalidPrinterException
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
  /// <summary>Represents the exception that is thrown when you try to access a printer using printer settings that are not valid.</summary>
  [Serializable]
  public class InvalidPrinterException : SystemException
  {
    private PrinterSettings settings;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.InvalidPrinterException" /> class.</summary>
    /// <param name="settings">A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that specifies the settings for a printer.</param>
    public InvalidPrinterException(PrinterSettings settings)
      : base(InvalidPrinterException.GenerateMessage(settings))
    {
      this.settings = settings;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.InvalidPrinterException" /> class with serialized data.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="P:System.Exception.HResult" /> is 0.</exception>
    protected InvalidPrinterException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.settings = (PrinterSettings) info.GetValue(nameof (settings), typeof (PrinterSettings));
    }

    /// <summary>Overridden. Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> is <see langword="null" />.</exception>
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      IntSecurity.AllPrinting.Demand();
      info.AddValue("settings", (object) this.settings);
      base.GetObjectData(info, context);
    }

    private static string GenerateMessage(PrinterSettings settings)
    {
      if (settings.IsDefaultPrinter)
        return System.Drawing.SR.GetString("InvalidPrinterException_NoDefaultPrinter");
      try
      {
        return System.Drawing.SR.GetString("InvalidPrinterException_InvalidPrinter", (object) settings.PrinterName);
      }
      catch (SecurityException ex)
      {
        return System.Drawing.SR.GetString("InvalidPrinterException_InvalidPrinter", (object) System.Drawing.SR.GetString("CantTellPrinterName"));
      }
    }
  }
}
