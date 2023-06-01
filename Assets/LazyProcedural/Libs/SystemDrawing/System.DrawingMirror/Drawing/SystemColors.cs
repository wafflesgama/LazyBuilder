// Decompiled with JetBrains decompiler
// Type: System.Drawing.SystemColors
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Each property of the <see cref="T:System.Drawing.SystemColors" /> class is a <see cref="T:System.Drawing.Color" /> structure that is the color of a Windows display element.</summary>
  public sealed class SystemColors
  {
    private SystemColors()
    {
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the active window's border.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the active window's border.</returns>
    public static Color ActiveBorder => new Color(KnownColor.ActiveBorder);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of the active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the active window's title bar.</returns>
    public static Color ActiveCaption => new Color(KnownColor.ActiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in the active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in the active window's title bar.</returns>
    public static Color ActiveCaptionText => new Color(KnownColor.ActiveCaptionText);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the application workspace.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the application workspace.</returns>
    public static Color AppWorkspace => new Color(KnownColor.AppWorkspace);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the face color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the face color of a 3-D element.</returns>
    public static Color ButtonFace => new Color(KnownColor.ButtonFace);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the highlight color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the highlight color of a 3-D element.</returns>
    public static Color ButtonHighlight => new Color(KnownColor.ButtonHighlight);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the shadow color of a 3-D element.</returns>
    public static Color ButtonShadow => new Color(KnownColor.ButtonShadow);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the face color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the face color of a 3-D element.</returns>
    public static Color Control => new Color(KnownColor.Control);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the shadow color of a 3-D element.</returns>
    public static Color ControlDark => new Color(KnownColor.ControlDark);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the dark shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the dark shadow color of a 3-D element.</returns>
    public static Color ControlDarkDark => new Color(KnownColor.ControlDarkDark);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the light color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the light color of a 3-D element.</returns>
    public static Color ControlLight => new Color(KnownColor.ControlLight);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the highlight color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the highlight color of a 3-D element.</returns>
    public static Color ControlLightLight => new Color(KnownColor.ControlLightLight);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of text in a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of text in a 3-D element.</returns>
    public static Color ControlText => new Color(KnownColor.ControlText);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the desktop.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the desktop.</returns>
    public static Color Desktop => new Color(KnownColor.Desktop);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the lightest color in the color gradient of an active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
    public static Color GradientActiveCaption => new Color(KnownColor.GradientActiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the lightest color in the color gradient of an inactive window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
    public static Color GradientInactiveCaption => new Color(KnownColor.GradientInactiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of dimmed text.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of dimmed text.</returns>
    public static Color GrayText => new Color(KnownColor.GrayText);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of selected items.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of selected items.</returns>
    public static Color Highlight => new Color(KnownColor.Highlight);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text of selected items.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text of selected items.</returns>
    public static Color HighlightText => new Color(KnownColor.HighlightText);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color used to designate a hot-tracked item.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color used to designate a hot-tracked item.</returns>
    public static Color HotTrack => new Color(KnownColor.HotTrack);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of an inactive window's border.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of an inactive window's border.</returns>
    public static Color InactiveBorder => new Color(KnownColor.InactiveBorder);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of an inactive window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of an inactive window's title bar.</returns>
    public static Color InactiveCaption => new Color(KnownColor.InactiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in an inactive window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in an inactive window's title bar.</returns>
    public static Color InactiveCaptionText => new Color(KnownColor.InactiveCaptionText);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a ToolTip.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a ToolTip.</returns>
    public static Color Info => new Color(KnownColor.Info);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text of a ToolTip.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text of a ToolTip.</returns>
    public static Color InfoText => new Color(KnownColor.InfoText);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a menu's background.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a menu's background.</returns>
    public static Color Menu => new Color(KnownColor.Menu);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a menu bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a menu bar.</returns>
    public static Color MenuBar => new Color(KnownColor.MenuBar);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
    public static Color MenuHighlight => new Color(KnownColor.MenuHighlight);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a menu's text.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a menu's text.</returns>
    public static Color MenuText => new Color(KnownColor.MenuText);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a scroll bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a scroll bar.</returns>
    public static Color ScrollBar => new Color(KnownColor.ScrollBar);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background in the client area of a window.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background in the client area of a window.</returns>
    public static Color Window => new Color(KnownColor.Window);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a window frame.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a window frame.</returns>
    public static Color WindowFrame => new Color(KnownColor.WindowFrame);

    /// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in the client area of a window.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in the client area of a window.</returns>
    public static Color WindowText => new Color(KnownColor.WindowText);
  }
}
