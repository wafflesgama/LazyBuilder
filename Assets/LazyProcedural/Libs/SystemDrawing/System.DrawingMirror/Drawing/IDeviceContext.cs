// Decompiled with JetBrains decompiler
// Type: System.Drawing.IDeviceContext
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Security.Permissions;

namespace System.Drawing
{
  /// <summary>Defines methods for obtaining and releasing an existing handle to a Windows device context.</summary>
  public interface IDeviceContext : IDisposable
  {
    /// <summary>Returns the handle to a Windows device context.</summary>
    /// <returns>An <see cref="T:System.IntPtr" /> representing the handle of a device context.</returns>
    [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    IntPtr GetHdc();

    /// <summary>Releases the handle of a Windows device context.</summary>
    [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    void ReleaseHdc();
  }
}
