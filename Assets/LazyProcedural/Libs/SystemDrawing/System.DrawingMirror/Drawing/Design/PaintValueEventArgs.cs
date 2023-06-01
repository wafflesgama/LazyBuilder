// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.PaintValueEventArgs
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Security.Permissions;

namespace System.Drawing.Design
{
  /// <summary>Provides data for the <see cref="M:System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)" /> method.</summary>
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class PaintValueEventArgs : EventArgs
  {
    private readonly ITypeDescriptorContext context;
    private readonly object valueToPaint;
    private readonly Graphics graphics;
    private readonly Rectangle bounds;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.PaintValueEventArgs" /> class using the specified values.</summary>
    /// <param name="context">The context in which the value appears.</param>
    /// <param name="value">The value to paint.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object with which drawing is to be done.</param>
    /// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which drawing is to be done.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="graphics" /> is <see langword="null" />.</exception>
    public PaintValueEventArgs(
      ITypeDescriptorContext context,
      object value,
      Graphics graphics,
      Rectangle bounds)
    {
      this.context = context;
      this.valueToPaint = value;
      this.graphics = graphics;
      if (graphics == null)
        throw new ArgumentNullException(nameof (graphics));
      this.bounds = bounds;
    }

    /// <summary>Gets the rectangle that indicates the area in which the painting should be done.</summary>
    /// <returns>The rectangle that indicates the area in which the painting should be done.</returns>
    public Rectangle Bounds => this.bounds;

    /// <summary>Gets the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface to be used to gain additional information about the context this value appears in.</summary>
    /// <returns>An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the event.</returns>
    public ITypeDescriptorContext Context => this.context;

    /// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object with which painting should be done.</summary>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> object to use for painting.</returns>
    public Graphics Graphics => this.graphics;

    /// <summary>Gets the value to paint.</summary>
    /// <returns>An object indicating what to paint.</returns>
    public object Value => this.valueToPaint;
  }
}
