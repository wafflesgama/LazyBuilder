// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.IToolboxUser
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Design
{
  /// <summary>Defines an interface for setting the currently selected toolbox item and indicating whether a designer supports a particular toolbox item.</summary>
  public interface IToolboxUser
  {
    /// <summary>Gets a value indicating whether the specified tool is supported by the current designer.</summary>
    /// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to be tested for toolbox support.</param>
    /// <returns>
    /// <see langword="true" /> if the tool is supported by the toolbox and can be enabled; <see langword="false" /> if the document designer does not know how to use the tool.</returns>
    bool GetToolSupported(ToolboxItem tool);

    /// <summary>Selects the specified tool.</summary>
    /// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
    void ToolPicked(ToolboxItem tool);
  }
}
