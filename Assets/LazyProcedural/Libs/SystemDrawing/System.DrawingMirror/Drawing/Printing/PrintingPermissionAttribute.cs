// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrintingPermissionAttribute
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
  /// <summary>Allows declarative printing permission checks.</summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
  public sealed class PrintingPermissionAttribute : CodeAccessSecurityAttribute
  {
    private PrintingPermissionLevel level;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermissionAttribute" /> class.</summary>
    /// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
    public PrintingPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>Gets or sets the type of printing allowed.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</returns>
    /// <exception cref="T:System.ArgumentException">The value is not one of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</exception>
    public PrintingPermissionLevel Level
    {
      get => this.level;
      set => this.level = value >= PrintingPermissionLevel.NoPrinting && value <= PrintingPermissionLevel.AllPrinting ? value : throw new ArgumentException(System.Drawing.SR.GetString("PrintingPermissionAttributeInvalidPermissionLevel"), nameof (value));
    }

    /// <summary>Creates the permission based on the requested access levels, which are set through the <see cref="P:System.Drawing.Printing.PrintingPermissionAttribute.Level" /> property on the attribute.</summary>
    /// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
    public override IPermission CreatePermission() => this.Unrestricted ? (IPermission) new PrintingPermission(PermissionState.Unrestricted) : (IPermission) new PrintingPermission(this.level);
  }
}
