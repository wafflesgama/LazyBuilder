// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.UITypeEditorEditStyle
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Design
{
  /// <summary>Specifies identifiers that indicate the value editing style of a <see cref="T:System.Drawing.Design.UITypeEditor" />.</summary>
  public enum UITypeEditorEditStyle
  {
    /// <summary>Provides no interactive user interface (UI) component.</summary>
    None = 1,
    /// <summary>Displays an ellipsis (...) button to start a modal dialog box, which requires user input before continuing a program, or a modeless dialog box, which stays on the screen and is available for use at any time but permits other user activities.</summary>
    Modal = 2,
    /// <summary>Displays a drop-down arrow button and hosts the user interface (UI) in a drop-down dialog box.</summary>
    DropDown = 3,
  }
}
