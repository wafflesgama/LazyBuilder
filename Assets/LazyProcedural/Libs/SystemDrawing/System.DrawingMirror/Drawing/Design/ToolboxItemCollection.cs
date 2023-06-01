// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.ToolboxItemCollection
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Security.Permissions;

namespace System.Drawing.Design
{
  /// <summary>Represents a collection of toolbox items.</summary>
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public sealed class ToolboxItemCollection : ReadOnlyCollectionBase
  {
    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> class using the specified collection.</summary>
    /// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> to fill the new collection with.</param>
    public ToolboxItemCollection(ToolboxItemCollection value) => this.InnerList.AddRange((ICollection) value);

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> class using the specified array of toolbox items.</summary>
    /// <param name="value">An array of type <see cref="T:System.Drawing.Design.ToolboxItem" /> containing the toolbox items to fill the collection with.</param>
    public ToolboxItemCollection(ToolboxItem[] value) => this.InnerList.AddRange((ICollection) value);

    /// <summary>Gets the <see cref="T:System.Drawing.Design.ToolboxItem" /> at the specified index.</summary>
    /// <param name="index">The index of the object to get or set.</param>
    /// <returns>A <see cref="T:System.Drawing.Design.ToolboxItem" /> at each valid index in the collection.</returns>
    public ToolboxItem this[int index] => (ToolboxItem) this.InnerList[index];

    /// <summary>Indicates whether the collection contains the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
    /// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItem" /> to search the collection for.</param>
    /// <returns>
    /// <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
    public bool Contains(ToolboxItem value) => this.InnerList.Contains((object) value);

    /// <summary>Copies the collection to the specified array beginning with the specified destination index.</summary>
    /// <param name="array">The array to copy to.</param>
    /// <param name="index">The index to begin copying to.</param>
    public void CopyTo(ToolboxItem[] array, int index) => this.InnerList.CopyTo((Array) array, index);

    /// <summary>Gets the index of the specified <see cref="T:System.Drawing.Design.ToolboxItem" />, if it exists in the collection.</summary>
    /// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItem" /> to get the index of in the collection.</param>
    /// <returns>The index of the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
    public int IndexOf(ToolboxItem value) => this.InnerList.IndexOf((object) value);
  }
}
