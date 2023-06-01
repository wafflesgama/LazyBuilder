// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PreviewPageInfo
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  /// <summary>Specifies print preview information for a single page. This class cannot be inherited.</summary>
  public sealed class PreviewPageInfo
  {
    private Image image;
    private Size physicalSize = Size.Empty;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PreviewPageInfo" /> class.</summary>
    /// <param name="image">The image of the printed page.</param>
    /// <param name="physicalSize">The size of the printed page, in hundredths of an inch.</param>
    public PreviewPageInfo(Image image, Size physicalSize)
    {
      this.image = image;
      this.physicalSize = physicalSize;
    }

    /// <summary>Gets the image of the printed page.</summary>
    /// <returns>An <see cref="T:System.Drawing.Image" /> representing the printed page.</returns>
    public Image Image => this.image;

    /// <summary>Gets the size of the printed page, in hundredths of an inch.</summary>
    /// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the printed page, in hundredths of an inch.</returns>
    public Size PhysicalSize => this.physicalSize;
  }
}
