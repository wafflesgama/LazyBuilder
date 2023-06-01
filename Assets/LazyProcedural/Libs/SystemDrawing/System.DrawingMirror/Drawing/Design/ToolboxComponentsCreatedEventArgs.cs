// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.ToolboxComponentsCreatedEventArgs
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Security.Permissions;

namespace System.Drawing.Design
{
  /// <summary>Provides data for the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreated" /> event that occurs when components are added to the toolbox.</summary>
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class ToolboxComponentsCreatedEventArgs : EventArgs
  {
    private readonly IComponent[] comps;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxComponentsCreatedEventArgs" /> class.</summary>
    /// <param name="components">The components to include in the toolbox.</param>
    public ToolboxComponentsCreatedEventArgs(IComponent[] components) => this.comps = components;

    /// <summary>Gets or sets an array containing the components to add to the toolbox.</summary>
    /// <returns>An array of type <see cref="T:System.ComponentModel.IComponent" /> indicating the components to add to the toolbox.</returns>
    public IComponent[] Components => (IComponent[]) this.comps.Clone();
  }
}
