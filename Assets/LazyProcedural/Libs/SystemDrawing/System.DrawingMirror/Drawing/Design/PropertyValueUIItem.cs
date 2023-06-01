// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.PropertyValueUIItem
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Security.Permissions;

namespace System.Drawing.Design
{
  /// <summary>Provides information about a property displayed in the Properties window, including the associated event handler, pop-up information string, and the icon to display for the property.</summary>
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class PropertyValueUIItem
  {
    private Image itemImage;
    private PropertyValueUIItemInvokeHandler handler;
    private string tooltip;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> class.</summary>
    /// <param name="uiItemImage">The icon to display. The image must be 8 x 8 pixels.</param>
    /// <param name="handler">The handler to invoke when the image is double-clicked.</param>
    /// <param name="tooltip">The <see cref="P:System.Drawing.Design.PropertyValueUIItem.ToolTip" /> to display for the property that this <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> is associated with.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="uiItemImage" /> or <paramref name="handler" /> is <see langword="null" />.</exception>
    public PropertyValueUIItem(
      Image uiItemImage,
      PropertyValueUIItemInvokeHandler handler,
      string tooltip)
    {
      this.itemImage = uiItemImage;
      this.handler = handler;
      if (this.itemImage == null)
        throw new ArgumentNullException(nameof (uiItemImage));
      if (handler == null)
        throw new ArgumentNullException(nameof (handler));
      this.tooltip = tooltip;
    }

    /// <summary>Gets the 8 x 8 pixel image that will be drawn in the Properties window.</summary>
    /// <returns>The image to use for the property icon.</returns>
    public virtual Image Image => this.itemImage;

    /// <summary>Gets the handler that is raised when a user double-clicks this item.</summary>
    /// <returns>A <see cref="T:System.Drawing.Design.PropertyValueUIItemInvokeHandler" /> indicating the event handler for this user interface (UI) item.</returns>
    public virtual PropertyValueUIItemInvokeHandler InvokeHandler => this.handler;

    /// <summary>Gets or sets the information string to display for this item.</summary>
    /// <returns>A string containing the information string to display for this item.</returns>
    public virtual string ToolTip => this.tooltip;

    /// <summary>Resets the user interface (UI) item.</summary>
    public virtual void Reset()
    {
    }
  }
}
