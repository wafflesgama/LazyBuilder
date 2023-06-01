// Decompiled with JetBrains decompiler
// Type: System.Drawing.IntSecurity
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Printing;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing
{
  internal static class IntSecurity
  {
    private static readonly UIPermission AllWindows = new UIPermission(UIPermissionWindow.AllWindows);
    private static readonly UIPermission SafeSubWindows = new UIPermission(UIPermissionWindow.SafeSubWindows);
    public static readonly CodeAccessPermission UnmanagedCode = (CodeAccessPermission) new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
    public static readonly CodeAccessPermission ObjectFromWin32Handle = IntSecurity.UnmanagedCode;
    public static readonly CodeAccessPermission Win32HandleManipulation = IntSecurity.UnmanagedCode;
    public static readonly PrintingPermission NoPrinting = new PrintingPermission(PrintingPermissionLevel.NoPrinting);
    public static readonly PrintingPermission SafePrinting = new PrintingPermission(PrintingPermissionLevel.SafePrinting);
    public static readonly PrintingPermission DefaultPrinting = new PrintingPermission(PrintingPermissionLevel.DefaultPrinting);
    public static readonly PrintingPermission AllPrinting = new PrintingPermission(PrintingPermissionLevel.AllPrinting);
    private static PermissionSet allPrintingAndUnmanagedCode;

    internal static void DemandReadFileIO(string fileName) => new FileIOPermission(FileIOPermissionAccess.Read, IntSecurity.UnsafeGetFullPath(fileName)).Demand();

    internal static void DemandWriteFileIO(string fileName) => new FileIOPermission(FileIOPermissionAccess.Write, IntSecurity.UnsafeGetFullPath(fileName)).Demand();

    internal static string UnsafeGetFullPath(string fileName)
    {
      new FileIOPermission(PermissionState.None)
      {
        AllFiles = FileIOPermissionAccess.PathDiscovery
      }.Assert();
      try
      {
        return Path.GetFullPath(fileName);
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
    }

    public static PermissionSet AllPrintingAndUnmanagedCode
    {
      get
      {
        if (IntSecurity.allPrintingAndUnmanagedCode == null)
        {
          PermissionSet permissionSet = new PermissionSet(PermissionState.None);
          permissionSet.SetPermission((IPermission) IntSecurity.UnmanagedCode);
          permissionSet.SetPermission((IPermission) IntSecurity.AllPrinting);
          IntSecurity.allPrintingAndUnmanagedCode = permissionSet;
        }
        return IntSecurity.allPrintingAndUnmanagedCode;
      }
    }

    internal static bool HasPermission(PrintingPermission permission)
    {
      try
      {
        permission.Demand();
        return true;
      }
      catch (SecurityException ex)
      {
        return false;
      }
    }
  }
}
