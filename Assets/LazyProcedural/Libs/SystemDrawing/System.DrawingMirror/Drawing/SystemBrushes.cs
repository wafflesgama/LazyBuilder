// Decompiled with JetBrains decompiler
// Type: System.Drawing.SystemBrushes
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Each property of the <see cref="T:System.Drawing.SystemBrushes" /> class is a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a Windows display element.</summary>
  public sealed class SystemBrushes
  {
    private static readonly object SystemBrushesKey = new object();

    private SystemBrushes()
    {
    }

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the active window's border.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the active window's border.</returns>
    public static Brush ActiveBorder => SystemBrushes.FromSystemColor(SystemColors.ActiveBorder);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</returns>
    public static Brush ActiveCaption => SystemBrushes.FromSystemColor(SystemColors.ActiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</returns>
    public static Brush ActiveCaptionText => SystemBrushes.FromSystemColor(SystemColors.ActiveCaptionText);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the application workspace.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the application workspace.</returns>
    public static Brush AppWorkspace => SystemBrushes.FromSystemColor(SystemColors.AppWorkspace);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
    public static Brush ButtonFace => SystemBrushes.FromSystemColor(SystemColors.ButtonFace);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
    public static Brush ButtonHighlight => SystemBrushes.FromSystemColor(SystemColors.ButtonHighlight);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
    public static Brush ButtonShadow => SystemBrushes.FromSystemColor(SystemColors.ButtonShadow);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
    public static Brush Control => SystemBrushes.FromSystemColor(SystemColors.Control);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
    public static Brush ControlLightLight => SystemBrushes.FromSystemColor(SystemColors.ControlLightLight);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element.</returns>
    public static Brush ControlLight => SystemBrushes.FromSystemColor(SystemColors.ControlLight);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
    public static Brush ControlDark => SystemBrushes.FromSystemColor(SystemColors.ControlDark);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element.</returns>
    public static Brush ControlDarkDark => SystemBrushes.FromSystemColor(SystemColors.ControlDarkDark);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</returns>
    public static Brush ControlText => SystemBrushes.FromSystemColor(SystemColors.ControlText);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the desktop.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the desktop.</returns>
    public static Brush Desktop => SystemBrushes.FromSystemColor(SystemColors.Desktop);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an active window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
    public static Brush GradientActiveCaption => SystemBrushes.FromSystemColor(SystemColors.GradientActiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an inactive window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
    public static Brush GradientInactiveCaption => SystemBrushes.FromSystemColor(SystemColors.GradientInactiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of dimmed text.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of dimmed text.</returns>
    public static Brush GrayText => SystemBrushes.FromSystemColor(SystemColors.GrayText);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items.</returns>
    public static Brush Highlight => SystemBrushes.FromSystemColor(SystemColors.Highlight);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items.</returns>
    public static Brush HighlightText => SystemBrushes.FromSystemColor(SystemColors.HighlightText);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color used to designate a hot-tracked item.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color used to designate a hot-tracked item.</returns>
    public static Brush HotTrack => SystemBrushes.FromSystemColor(SystemColors.HotTrack);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of an inactive window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of an inactive window's title bar.</returns>
    public static Brush InactiveCaption => SystemBrushes.FromSystemColor(SystemColors.InactiveCaption);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of an inactive window's border.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of an inactive window's border.</returns>
    public static Brush InactiveBorder => SystemBrushes.FromSystemColor(SystemColors.InactiveBorder);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in an inactive window's title bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in an inactive window's title bar.</returns>
    public static Brush InactiveCaptionText => SystemBrushes.FromSystemColor(SystemColors.InactiveCaptionText);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a ToolTip.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a ToolTip.</returns>
    public static Brush Info => SystemBrushes.FromSystemColor(SystemColors.Info);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of a ToolTip.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> is the color of the text of a ToolTip.</returns>
    public static Brush InfoText => SystemBrushes.FromSystemColor(SystemColors.InfoText);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</returns>
    public static Brush Menu => SystemBrushes.FromSystemColor(SystemColors.Menu);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a menu bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a menu bar.</returns>
    public static Brush MenuBar => SystemBrushes.FromSystemColor(SystemColors.MenuBar);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
    public static Brush MenuHighlight => SystemBrushes.FromSystemColor(SystemColors.MenuHighlight);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's text.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's text.</returns>
    public static Brush MenuText => SystemBrushes.FromSystemColor(SystemColors.MenuText);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a scroll bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a scroll bar.</returns>
    public static Brush ScrollBar => SystemBrushes.FromSystemColor(SystemColors.ScrollBar);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</returns>
    public static Brush Window => SystemBrushes.FromSystemColor(SystemColors.Window);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a window frame.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a window frame.</returns>
    public static Brush WindowFrame => SystemBrushes.FromSystemColor(SystemColors.WindowFrame);

    /// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the client area of a window.</summary>
    /// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the client area of a window.</returns>
    public static Brush WindowText => SystemBrushes.FromSystemColor(SystemColors.WindowText);

    /// <summary>Creates a <see cref="T:System.Drawing.Brush" /> from the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
    /// <param name="c">The <see cref="T:System.Drawing.Color" /> structure from which to create the <see cref="T:System.Drawing.Brush" />.</param>
    /// <returns>The <see cref="T:System.Drawing.Brush" /> this method creates.</returns>
    public static Brush FromSystemColor(Color c)
    {
      if (!c.IsSystemColor)
        throw new ArgumentException(SR.GetString("ColorNotSystemColor", (object) c.ToString()));
      Brush[] brushArray = (Brush[]) SafeNativeMethods.Gdip.ThreadData[SystemBrushes.SystemBrushesKey];
      if (brushArray == null)
      {
        brushArray = new Brush[33];
        SafeNativeMethods.Gdip.ThreadData[SystemBrushes.SystemBrushesKey] = (object) brushArray;
      }
      int knownColor = (int) c.ToKnownColor();
      if (knownColor > 167)
        knownColor -= 141;
      int index = knownColor - 1;
      if (brushArray[index] == null)
        brushArray[index] = (Brush) new SolidBrush(c, true);
      return brushArray[index];
    }
  }
}
