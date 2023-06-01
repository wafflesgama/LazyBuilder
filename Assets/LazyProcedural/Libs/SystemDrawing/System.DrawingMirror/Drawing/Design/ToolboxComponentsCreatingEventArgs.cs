// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.ToolboxComponentsCreatingEventArgs
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Drawing.Design
{
  /// <summary>Provides data for the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreating" /> event that occurs when components are added to the toolbox.</summary>
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class ToolboxComponentsCreatingEventArgs : EventArgs
  {
    private readonly IDesignerHost host;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxComponentsCreatingEventArgs" /> class.</summary>
    /// <param name="host">The designer host that is making the request.</param>
    public ToolboxComponentsCreatingEventArgs(IDesignerHost host) => this.host = host;

    /// <summary>Gets or sets an instance of the <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that made the request to create toolbox components.</summary>
    /// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that made the request to create toolbox components, or <see langword="null" /> if no designer host was provided to the toolbox item.</returns>
    public IDesignerHost DesignerHost => this.host;
  }
}
