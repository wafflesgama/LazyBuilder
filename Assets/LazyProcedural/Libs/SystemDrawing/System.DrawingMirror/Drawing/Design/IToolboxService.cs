// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.IToolboxService
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.Drawing.Design
{
  /// <summary>Provides methods and properties to manage and query the toolbox in the development environment.</summary>
  [Guid("4BACD258-DE64-4048-BC4E-FEDBEF9ACB76")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IToolboxService
  {
    /// <summary>Gets the names of all the tool categories currently on the toolbox.</summary>
    /// <returns>A <see cref="T:System.Drawing.Design.CategoryNameCollection" /> containing the tool categories.</returns>
    CategoryNameCollection CategoryNames { get; }

    /// <summary>Gets or sets the name of the currently selected tool category from the toolbox.</summary>
    /// <returns>The name of the currently selected category.</returns>
    string SelectedCategory { get; set; }

    /// <summary>Adds a new toolbox item creator for a specified data format.</summary>
    /// <param name="creator">A <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" /> that can create a component when the toolbox item is invoked.</param>
    /// <param name="format">The data format that the creator handles.</param>
    void AddCreator(ToolboxItemCreatorCallback creator, string format);

    /// <summary>Adds a new toolbox item creator for a specified data format and designer host.</summary>
    /// <param name="creator">A <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" /> that can create a component when the toolbox item is invoked.</param>
    /// <param name="format">The data format that the creator handles.</param>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the designer host to associate with the creator.</param>
    void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host);

    /// <summary>Adds the specified project-linked toolbox item to the toolbox.</summary>
    /// <param name="toolboxItem">The linked <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
    void AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host);

    /// <summary>Adds the specified project-linked toolbox item to the toolbox in the specified category.</summary>
    /// <param name="toolboxItem">The linked <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
    /// <param name="category">The toolbox item category to add the toolbox item to.</param>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
    void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host);

    /// <summary>Adds the specified toolbox item to the toolbox.</summary>
    /// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
    void AddToolboxItem(ToolboxItem toolboxItem);

    /// <summary>Adds the specified toolbox item to the toolbox in the specified category.</summary>
    /// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
    /// <param name="category">The toolbox item category to add the <see cref="T:System.Drawing.Design.ToolboxItem" /> to.</param>
    void AddToolboxItem(ToolboxItem toolboxItem, string category);

    /// <summary>Gets a toolbox item from the specified object that represents a toolbox item in serialized form.</summary>
    /// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
    /// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> created from the serialized object.</returns>
    ToolboxItem DeserializeToolboxItem(object serializedObject);

    /// <summary>Gets a toolbox item from the specified object that represents a toolbox item in serialized form, using the specified designer host.</summary>
    /// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to associate with this <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> created from deserialization.</returns>
    ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host);

    /// <summary>Gets the currently selected toolbox item.</summary>
    /// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> that is currently selected, or <see langword="null" /> if no toolbox item has been selected.</returns>
    ToolboxItem GetSelectedToolboxItem();

    /// <summary>Gets the currently selected toolbox item if it is available to all designers, or if it supports the specified designer.</summary>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that the selected tool must be associated with for it to be returned.</param>
    /// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> that is currently selected, or <see langword="null" /> if no toolbox item is currently selected.</returns>
    ToolboxItem GetSelectedToolboxItem(IDesignerHost host);

    /// <summary>Gets the entire collection of toolbox items from the toolbox.</summary>
    /// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items.</returns>
    ToolboxItemCollection GetToolboxItems();

    /// <summary>Gets the collection of toolbox items that are associated with the specified designer host from the toolbox.</summary>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the toolbox items to retrieve.</param>
    /// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified designer host.</returns>
    ToolboxItemCollection GetToolboxItems(IDesignerHost host);

    /// <summary>Gets a collection of toolbox items from the toolbox that match the specified category.</summary>
    /// <param name="category">The toolbox item category to retrieve all the toolbox items from.</param>
    /// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified category.</returns>
    ToolboxItemCollection GetToolboxItems(string category);

    /// <summary>Gets the collection of toolbox items that are associated with the specified designer host and category from the toolbox.</summary>
    /// <param name="category">The toolbox item category to retrieve the toolbox items from.</param>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the toolbox items to retrieve.</param>
    /// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified category and designer host.</returns>
    ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host);

    /// <summary>Gets a value indicating whether the specified object which represents a serialized toolbox item can be used by the specified designer host.</summary>
    /// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to test for support for the <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
    /// <returns>
    /// <see langword="true" /> if the specified object is compatible with the specified designer host; otherwise, <see langword="false" />.</returns>
    bool IsSupported(object serializedObject, IDesignerHost host);

    /// <summary>Gets a value indicating whether the specified object which represents a serialized toolbox item matches the specified attributes.</summary>
    /// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
    /// <param name="filterAttributes">An <see cref="T:System.Collections.ICollection" /> that contains the attributes to test the serialized object for.</param>
    /// <returns>
    /// <see langword="true" /> if the object matches the specified attributes; otherwise, <see langword="false" />.</returns>
    bool IsSupported(object serializedObject, ICollection filterAttributes);

    /// <summary>Gets a value indicating whether the specified object is a serialized toolbox item.</summary>
    /// <param name="serializedObject">The object to inspect.</param>
    /// <returns>
    /// <see langword="true" /> if the object contains a toolbox item object; otherwise, <see langword="false" />.</returns>
    bool IsToolboxItem(object serializedObject);

    /// <summary>Gets a value indicating whether the specified object is a serialized toolbox item, using the specified designer host.</summary>
    /// <param name="serializedObject">The object to inspect.</param>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is making this request.</param>
    /// <returns>
    /// <see langword="true" /> if the object contains a toolbox item object; otherwise, <see langword="false" />.</returns>
    bool IsToolboxItem(object serializedObject, IDesignerHost host);

    /// <summary>Refreshes the state of the toolbox items.</summary>
    void Refresh();

    /// <summary>Removes a previously added toolbox item creator of the specified data format.</summary>
    /// <param name="format">The data format of the creator to remove.</param>
    void RemoveCreator(string format);

    /// <summary>Removes a previously added toolbox creator that is associated with the specified data format and the specified designer host.</summary>
    /// <param name="format">The data format of the creator to remove.</param>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the creator to remove.</param>
    void RemoveCreator(string format, IDesignerHost host);

    /// <summary>Removes the specified toolbox item from the toolbox.</summary>
    /// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to remove from the toolbox.</param>
    void RemoveToolboxItem(ToolboxItem toolboxItem);

    /// <summary>Removes the specified toolbox item from the toolbox.</summary>
    /// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to remove from the toolbox.</param>
    /// <param name="category">The toolbox item category to remove the <see cref="T:System.Drawing.Design.ToolboxItem" /> from.</param>
    void RemoveToolboxItem(ToolboxItem toolboxItem, string category);

    /// <summary>Notifies the toolbox service that the selected tool has been used.</summary>
    void SelectedToolboxItemUsed();

    /// <summary>Gets a serializable object that represents the specified toolbox item.</summary>
    /// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to serialize.</param>
    /// <returns>An object that represents the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
    object SerializeToolboxItem(ToolboxItem toolboxItem);

    /// <summary>Sets the current application's cursor to a cursor that represents the currently selected tool.</summary>
    /// <returns>
    /// <see langword="true" /> if the cursor is set by the currently selected tool, <see langword="false" /> if there is no tool selected and the cursor is set to the standard windows cursor.</returns>
    bool SetCursor();

    /// <summary>Selects the specified toolbox item.</summary>
    /// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
    void SetSelectedToolboxItem(ToolboxItem toolboxItem);
  }
}
