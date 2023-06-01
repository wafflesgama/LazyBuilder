// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.PropertyValueUIHandler
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.ComponentModel;

namespace System.Drawing.Design
{
  /// <summary>Represents the method that adds a delegate to an implementation of <see cref="T:System.Drawing.Design.IPropertyValueUIService" />.</summary>
  /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to obtain context information.</param>
  /// <param name="propDesc">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property being queried.</param>
  /// <param name="valueUIItemList">An <see cref="T:System.Collections.ArrayList" /> of <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> objects containing the UI items associated with the property.</param>
  public delegate void PropertyValueUIHandler(
    ITypeDescriptorContext context,
    PropertyDescriptor propDesc,
    ArrayList valueUIItemList);
}
