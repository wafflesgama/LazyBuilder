// Decompiled with JetBrains decompiler
// Type: System.Drawing.ContentAlignment
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Design;

namespace System.Drawing
{
  /// <summary>Specifies alignment of content on the drawing surface.</summary>
  [Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  public enum ContentAlignment
  {
    /// <summary>Content is vertically aligned at the top, and horizontally aligned on the left.</summary>
    TopLeft = 1,
    /// <summary>Content is vertically aligned at the top, and horizontally aligned at the center.</summary>
    TopCenter = 2,
    /// <summary>Content is vertically aligned at the top, and horizontally aligned on the right.</summary>
    TopRight = 4,
    /// <summary>Content is vertically aligned in the middle, and horizontally aligned on the left.</summary>
    MiddleLeft = 16, // 0x00000010
    /// <summary>Content is vertically aligned in the middle, and horizontally aligned at the center.</summary>
    MiddleCenter = 32, // 0x00000020
    /// <summary>Content is vertically aligned in the middle, and horizontally aligned on the right.</summary>
    MiddleRight = 64, // 0x00000040
    /// <summary>Content is vertically aligned at the bottom, and horizontally aligned on the left.</summary>
    BottomLeft = 256, // 0x00000100
    /// <summary>Content is vertically aligned at the bottom, and horizontally aligned at the center.</summary>
    BottomCenter = 512, // 0x00000200
    /// <summary>Content is vertically aligned at the bottom, and horizontally aligned on the right.</summary>
    BottomRight = 1024, // 0x00000400
  }
}
