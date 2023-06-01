// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.PropertyValueUIItemInvokeHandler
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;

namespace System.Drawing.Design
{
  /// <summary>Represents the method that will handle the <see cref="P:System.Drawing.Design.PropertyValueUIItem.InvokeHandler" /> event of a <see cref="T:System.Drawing.Design.PropertyValueUIItem" />.</summary>
  /// <param name="context">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> for the property associated with the icon that was double-clicked.</param>
  /// <param name="descriptor">The property associated with the icon that was double-clicked.</param>
  /// <param name="invokedItem">The <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> associated with the icon that was double-clicked.</param>
  public delegate void PropertyValueUIItemInvokeHandler(
    ITypeDescriptorContext context,
    PropertyDescriptor descriptor,
    PropertyValueUIItem invokedItem);
}
