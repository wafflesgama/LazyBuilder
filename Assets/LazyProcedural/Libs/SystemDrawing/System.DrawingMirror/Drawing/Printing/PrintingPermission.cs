// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrintingPermission
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
  /// <summary>Controls access to printers. This class cannot be inherited.</summary>
  [Serializable]
  public sealed class PrintingPermission : CodeAccessPermission, IUnrestrictedPermission
  {
    private PrintingPermissionLevel printingLevel;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermission" /> class with either fully restricted or unrestricted access, as specified.</summary>
    /// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
    public PrintingPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.printingLevel = PrintingPermissionLevel.AllPrinting;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(System.Drawing.SR.GetString("InvalidPermissionState"));
        this.printingLevel = PrintingPermissionLevel.NoPrinting;
      }
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermission" /> class with the level of printing access specified.</summary>
    /// <param name="printingLevel">One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</param>
    public PrintingPermission(PrintingPermissionLevel printingLevel)
    {
      PrintingPermission.VerifyPrintingLevel(printingLevel);
      this.printingLevel = printingLevel;
    }

    /// <summary>Gets or sets the code's level of printing access.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</returns>
    public PrintingPermissionLevel Level
    {
      get => this.printingLevel;
      set
      {
        PrintingPermission.VerifyPrintingLevel(value);
        this.printingLevel = value;
      }
    }

    private static void VerifyPrintingLevel(PrintingPermissionLevel level)
    {
      if (level < PrintingPermissionLevel.NoPrinting || level > PrintingPermissionLevel.AllPrinting)
        throw new ArgumentException(System.Drawing.SR.GetString("InvalidPermissionLevel"));
    }

    /// <summary>Gets a value indicating whether the permission is unrestricted.</summary>
    /// <returns>
    /// <see langword="true" /> if permission is unrestricted; otherwise, <see langword="false" />.</returns>
    public bool IsUnrestricted() => this.printingLevel == PrintingPermissionLevel.AllPrinting;

    /// <summary>Determines whether the current permission object is a subset of the specified permission.</summary>
    /// <param name="target">A permission object that is to be tested for the subset relationship. This object must be of the same type as the current permission object.</param>
    /// <returns>
    /// <see langword="true" /> if the current permission object is a subset of <paramref name="target" />; otherwise, <see langword="false" />.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> is an object that is not of the same type as the current permission object.</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.printingLevel == PrintingPermissionLevel.NoPrinting;
      if (!(target is PrintingPermission printingPermission))
        throw new ArgumentException(System.Drawing.SR.GetString("TargetNotPrintingPermission"));
      return this.printingLevel <= printingPermission.printingLevel;
    }

    /// <summary>Creates and returns a permission that is the intersection of the current permission object and a target permission object.</summary>
    /// <param name="target">A permission object of the same type as the current permission object.</param>
    /// <returns>A new permission object that represents the intersection of the current object and the specified target. This object is <see langword="null" /> if the intersection is empty.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> is an object that is not of the same type as the current permission object.</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!(target is PrintingPermission printingPermission))
        throw new ArgumentException(System.Drawing.SR.GetString("TargetNotPrintingPermission"));
      PrintingPermissionLevel printingLevel = this.printingLevel < printingPermission.printingLevel ? this.printingLevel : printingPermission.printingLevel;
      return printingLevel == PrintingPermissionLevel.NoPrinting ? (IPermission) null : (IPermission) new PrintingPermission(printingLevel);
    }

    /// <summary>Creates a permission that combines the permission object and the target permission object.</summary>
    /// <param name="target">A permission object of the same type as the current permission object.</param>
    /// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> is an object that is not of the same type as the current permission object.</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!(target is PrintingPermission printingPermission))
        throw new ArgumentException(System.Drawing.SR.GetString("TargetNotPrintingPermission"));
      PrintingPermissionLevel printingLevel = this.printingLevel > printingPermission.printingLevel ? this.printingLevel : printingPermission.printingLevel;
      return printingLevel == PrintingPermissionLevel.NoPrinting ? (IPermission) null : (IPermission) new PrintingPermission(printingLevel);
    }

    /// <summary>Creates and returns an identical copy of the current permission object.</summary>
    /// <returns>A copy of the current permission object.</returns>
    public override IPermission Copy() => (IPermission) new PrintingPermission(this.printingLevel);

    /// <summary>Creates an XML encoding of the security object and its current state.</summary>
    /// <returns>An XML encoding of the security object, including any state information.</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement xml = new SecurityElement("IPermission");
      xml.AddAttribute("class", this.GetType().FullName + ", " + this.GetType().Module.Assembly.FullName.Replace('"', '\''));
      xml.AddAttribute("version", "1");
      if (!this.IsUnrestricted())
        xml.AddAttribute("Level", Enum.GetName(typeof (PrintingPermissionLevel), (object) this.printingLevel));
      else
        xml.AddAttribute("Unrestricted", "true");
      return xml;
    }

    /// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
    /// <param name="esd">The XML encoding to use to reconstruct the security object.</param>
    public override void FromXml(SecurityElement esd)
    {
      string str1 = esd != null ? esd.Attribute("class") : throw new ArgumentNullException(nameof (esd));
      if (str1 == null || str1.IndexOf(this.GetType().FullName) == -1)
        throw new ArgumentException(System.Drawing.SR.GetString("InvalidClassName"));
      string a = esd.Attribute("Unrestricted");
      if (a != null && string.Equals(a, "true", StringComparison.OrdinalIgnoreCase))
      {
        this.printingLevel = PrintingPermissionLevel.AllPrinting;
      }
      else
      {
        this.printingLevel = PrintingPermissionLevel.NoPrinting;
        string str2 = esd.Attribute("Level");
        if (str2 == null)
          return;
        this.printingLevel = (PrintingPermissionLevel) Enum.Parse(typeof (PrintingPermissionLevel), str2);
      }
    }
  }
}
