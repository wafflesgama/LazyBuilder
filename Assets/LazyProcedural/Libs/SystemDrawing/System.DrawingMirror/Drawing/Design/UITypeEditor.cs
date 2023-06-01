// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.UITypeEditor
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;

namespace System.Drawing.Design
{
  /// <summary>Provides a base class that can be used to design value editors that can provide a user interface (UI) for representing and editing the values of objects of the supported data types.</summary>
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class UITypeEditor
  {
    static UITypeEditor() => TypeDescriptor.AddEditorTable(typeof (UITypeEditor), new Hashtable()
    {
      [(object) typeof (DateTime)] = (object) "System.ComponentModel.Design.DateTimeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      [(object) typeof (Array)] = (object) "System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      [(object) typeof (IList)] = (object) "System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      [(object) typeof (ICollection)] = (object) "System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      [(object) typeof (byte[])] = (object) "System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      [(object) typeof (Stream)] = (object) "System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      [(object) typeof (string[])] = (object) "System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      [(object) typeof (Collection<string>)] = (object) "System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    });

    /// <summary>Gets a value indicating whether drop-down editors should be resizable by the user.</summary>
    /// <returns>
    /// <see langword="true" /> if drop-down editors are resizable; otherwise, <see langword="false" />.</returns>
    public virtual bool IsDropDownResizable => false;

    /// <summary>Edits the value of the specified object using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.</summary>
    /// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
    /// <param name="value">The object to edit.</param>
    /// <returns>The new value of the object.</returns>
    public object EditValue(IServiceProvider provider, object value) => this.EditValue((ITypeDescriptorContext) null, provider, value);

    /// <summary>Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    /// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
    /// <param name="value">The object to edit.</param>
    /// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
    public virtual object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      return value;
    }

    /// <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.</summary>
    /// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value that indicates the style of editor used by the current <see cref="T:System.Drawing.Design.UITypeEditor" />. By default, this method will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
    public UITypeEditorEditStyle GetEditStyle() => this.GetEditStyle((ITypeDescriptorContext) null);

    /// <summary>Indicates whether this editor supports painting a representation of an object's value.</summary>
    /// <returns>
    /// <see langword="true" /> if <see cref="M:System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)" /> is implemented; otherwise, <see langword="false" />.</returns>
    public bool GetPaintValueSupported() => this.GetPaintValueSupported((ITypeDescriptorContext) null);

    /// <summary>Indicates whether the specified context supports painting a representation of an object's value within the specified context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    /// <returns>
    /// <see langword="true" /> if <see cref="M:System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)" /> is implemented; otherwise, <see langword="false" />.</returns>
    public virtual bool GetPaintValueSupported(ITypeDescriptorContext context) => false;

    /// <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    /// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method. If the <see cref="T:System.Drawing.Design.UITypeEditor" /> does not support this method, then <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
    public virtual UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.None;

    /// <summary>Paints a representation of the value of the specified object to the specified canvas.</summary>
    /// <param name="value">The object whose value this type editor will display.</param>
    /// <param name="canvas">A drawing canvas on which to paint the representation of the object's value.</param>
    /// <param name="rectangle">A <see cref="T:System.Drawing.Rectangle" /> within whose boundaries to paint the value.</param>
    public void PaintValue(object value, Graphics canvas, Rectangle rectangle) => this.PaintValue(new PaintValueEventArgs((ITypeDescriptorContext) null, value, canvas, rectangle));

    /// <summary>Paints a representation of the value of an object using the specified <see cref="T:System.Drawing.Design.PaintValueEventArgs" />.</summary>
    /// <param name="e">A <see cref="T:System.Drawing.Design.PaintValueEventArgs" /> that indicates what to paint and where to paint it.</param>
    public virtual void PaintValue(PaintValueEventArgs e)
    {
    }
  }
}
