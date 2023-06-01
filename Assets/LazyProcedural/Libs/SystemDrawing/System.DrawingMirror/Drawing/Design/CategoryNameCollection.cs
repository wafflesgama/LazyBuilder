// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.CategoryNameCollection
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Security.Permissions;

namespace System.Drawing.Design
{
  /// <summary>Represents a collection of category name strings.</summary>
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public sealed class CategoryNameCollection : ReadOnlyCollectionBase
  {
    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.CategoryNameCollection" /> class using the specified collection.</summary>
    /// <param name="value">A <see cref="T:System.Drawing.Design.CategoryNameCollection" /> that contains the names to initialize the collection values to.</param>
    public CategoryNameCollection(CategoryNameCollection value) => this.InnerList.AddRange((ICollection) value);

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.CategoryNameCollection" /> class using the specified array of names.</summary>
    /// <param name="value">An array of strings that contains the names of the categories to initialize the collection values to.</param>
    public CategoryNameCollection(string[] value) => this.InnerList.AddRange((ICollection) value);

    /// <summary>Gets the category name at the specified index.</summary>
    /// <param name="index">The index of the collection element to access.</param>
    /// <returns>The category name at the specified index.</returns>
    public string this[int index] => (string) this.InnerList[index];

    /// <summary>Indicates whether the specified category is contained in the collection.</summary>
    /// <param name="value">The string to check for in the collection.</param>
    /// <returns>
    /// <see langword="true" /> if the specified category is contained in the collection; otherwise, <see langword="false" />.</returns>
    public bool Contains(string value) => this.InnerList.Contains((object) value);

    /// <summary>Copies the collection elements to the specified array at the specified index.</summary>
    /// <param name="array">The array to copy to.</param>
    /// <param name="index">The index of the destination array at which to begin copying.</param>
    public void CopyTo(string[] array, int index) => this.InnerList.CopyTo((Array) array, index);

    /// <summary>Gets the index of the specified value.</summary>
    /// <param name="value">The category name to retrieve the index of in the collection.</param>
    /// <returns>The index in the collection, or <see langword="null" /> if the string does not exist in the collection.</returns>
    public int IndexOf(string value) => this.InnerList.IndexOf((object) value);
  }
}
