// Decompiled with JetBrains decompiler
// Type: System.Drawing.SystemPens
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Each property of the <see cref="T:System.Drawing.SystemPens" /> class is a <see cref="T:System.Drawing.Pen" /> that is the color of a Windows display element and that has a width of 1 pixel.</summary>
  public sealed class SystemPens
  {
    private static readonly object SystemPensKey = new object();

    private SystemPens()
    {
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the active window's border.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the active window's border.</returns>
    public static Pen ActiveBorder => SystemPens.FromSystemColor(SystemColors.ActiveBorder);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of the active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of the active window's title bar.</returns>
    public static Pen ActiveCaption => SystemPens.FromSystemColor(SystemColors.ActiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in the active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in the active window's title bar.</returns>
    public static Pen ActiveCaptionText => SystemPens.FromSystemColor(SystemColors.ActiveCaptionText);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the application workspace.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the application workspace.</returns>
    public static Pen AppWorkspace => SystemPens.FromSystemColor(SystemColors.AppWorkspace);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</returns>
    public static Pen ButtonFace => SystemPens.FromSystemColor(SystemColors.ButtonFace);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</returns>
    public static Pen ButtonHighlight => SystemPens.FromSystemColor(SystemColors.ButtonHighlight);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</returns>
    public static Pen ButtonShadow => SystemPens.FromSystemColor(SystemColors.ButtonShadow);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</returns>
    public static Pen Control => SystemPens.FromSystemColor(SystemColors.Control);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of text in a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of text in a 3-D element.</returns>
    public static Pen ControlText => SystemPens.FromSystemColor(SystemColors.ControlText);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</returns>
    public static Pen ControlDark => SystemPens.FromSystemColor(SystemColors.ControlDark);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the dark shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the dark shadow color of a 3-D element.</returns>
    public static Pen ControlDarkDark => SystemPens.FromSystemColor(SystemColors.ControlDarkDark);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the light color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the light color of a 3-D element.</returns>
    public static Pen ControlLight => SystemPens.FromSystemColor(SystemColors.ControlLight);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</returns>
    public static Pen ControlLightLight => SystemPens.FromSystemColor(SystemColors.ControlLightLight);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the Windows desktop.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the Windows desktop.</returns>
    public static Pen Desktop => SystemPens.FromSystemColor(SystemColors.Desktop);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
    public static Pen GradientActiveCaption => SystemPens.FromSystemColor(SystemColors.GradientActiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an inactive window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
    public static Pen GradientInactiveCaption => SystemPens.FromSystemColor(SystemColors.GradientInactiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of dimmed text.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of dimmed text.</returns>
    public static Pen GrayText => SystemPens.FromSystemColor(SystemColors.GrayText);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of selected items.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of selected items.</returns>
    public static Pen Highlight => SystemPens.FromSystemColor(SystemColors.Highlight);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text of selected items.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text of selected items.</returns>
    public static Pen HighlightText => SystemPens.FromSystemColor(SystemColors.HighlightText);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color used to designate a hot-tracked item.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color used to designate a hot-tracked item.</returns>
    public static Pen HotTrack => SystemPens.FromSystemColor(SystemColors.HotTrack);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> is the color of the border of an inactive window.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the border of an inactive window.</returns>
    public static Pen InactiveBorder => SystemPens.FromSystemColor(SystemColors.InactiveBorder);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the title bar caption of an inactive window.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the title bar caption of an inactive window.</returns>
    public static Pen InactiveCaption => SystemPens.FromSystemColor(SystemColors.InactiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in an inactive window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in an inactive window's title bar.</returns>
    public static Pen InactiveCaptionText => SystemPens.FromSystemColor(SystemColors.InactiveCaptionText);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a ToolTip.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a ToolTip.</returns>
    public static Pen Info => SystemPens.FromSystemColor(SystemColors.Info);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text of a ToolTip.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text of a ToolTip.</returns>
    public static Pen InfoText => SystemPens.FromSystemColor(SystemColors.InfoText);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a menu's background.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a menu's background.</returns>
    public static Pen Menu => SystemPens.FromSystemColor(SystemColors.Menu);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a menu bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a menu bar.</returns>
    public static Pen MenuBar => SystemPens.FromSystemColor(SystemColors.MenuBar);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
    public static Pen MenuHighlight => SystemPens.FromSystemColor(SystemColors.MenuHighlight);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a menu's text.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a menu's text.</returns>
    public static Pen MenuText => SystemPens.FromSystemColor(SystemColors.MenuText);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a scroll bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a scroll bar.</returns>
    public static Pen ScrollBar => SystemPens.FromSystemColor(SystemColors.ScrollBar);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background in the client area of a window.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background in the client area of a window.</returns>
    public static Pen Window => SystemPens.FromSystemColor(SystemColors.Window);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a window frame.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a window frame.</returns>
    public static Pen WindowFrame => SystemPens.FromSystemColor(SystemColors.WindowFrame);

    /// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in the client area of a window.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in the client area of a window.</returns>
    public static Pen WindowText => SystemPens.FromSystemColor(SystemColors.WindowText);

    /// <summary>Creates a <see cref="T:System.Drawing.Pen" /> from the specified <see cref="T:System.Drawing.Color" />.</summary>
    /// <param name="c">The <see cref="T:System.Drawing.Color" /> for the new <see cref="T:System.Drawing.Pen" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Pen" /> this method creates.</returns>
    public static Pen FromSystemColor(Color c)
    {
      if (!c.IsSystemColor)
        throw new ArgumentException(SR.GetString("ColorNotSystemColor", (object) c.ToString()));
      Pen[] penArray = (Pen[]) SafeNativeMethods.Gdip.ThreadData[SystemPens.SystemPensKey];
      if (penArray == null)
      {
        penArray = new Pen[33];
        SafeNativeMethods.Gdip.ThreadData[SystemPens.SystemPensKey] = (object) penArray;
      }
      int knownColor = (int) c.ToKnownColor();
      if (knownColor > 167)
        knownColor -= 141;
      int index = knownColor - 1;
      if (penArray[index] == null)
        penArray[index] = new Pen(c, true);
      return penArray[index];
    }
  }
}
