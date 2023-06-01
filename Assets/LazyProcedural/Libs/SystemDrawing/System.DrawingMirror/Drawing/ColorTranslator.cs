// Decompiled with JetBrains decompiler
// Type: System.Drawing.ColorTranslator
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Drawing
{
  /// <summary>Translates colors to and from GDI+ <see cref="T:System.Drawing.Color" /> structures. This class cannot be inherited.</summary>
  public sealed class ColorTranslator
  {
    private const int Win32RedShift = 0;
    private const int Win32GreenShift = 8;
    private const int Win32BlueShift = 16;
    private static Hashtable htmlSysColorTable;

    private ColorTranslator()
    {
    }

    /// <summary>Translates the specified <see cref="T:System.Drawing.Color" /> structure to a Windows color.</summary>
    /// <param name="c">The <see cref="T:System.Drawing.Color" /> structure to translate.</param>
    /// <returns>The Windows color value.</returns>
    public static int ToWin32(Color c) => (int) c.R | (int) c.G << 8 | (int) c.B << 16;

    /// <summary>Translates the specified <see cref="T:System.Drawing.Color" /> structure to an OLE color.</summary>
    /// <param name="c">The <see cref="T:System.Drawing.Color" /> structure to translate.</param>
    /// <returns>The OLE color value.</returns>
    public static int ToOle(Color c)
    {
      if (c.IsKnownColor)
      {
        switch (c.ToKnownColor())
        {
          case KnownColor.ActiveBorder:
            return -2147483638;
          case KnownColor.ActiveCaption:
            return -2147483646;
          case KnownColor.ActiveCaptionText:
            return -2147483639;
          case KnownColor.AppWorkspace:
            return -2147483636;
          case KnownColor.Control:
            return -2147483633;
          case KnownColor.ControlDark:
            return -2147483632;
          case KnownColor.ControlDarkDark:
            return -2147483627;
          case KnownColor.ControlLight:
            return -2147483626;
          case KnownColor.ControlLightLight:
            return -2147483628;
          case KnownColor.ControlText:
            return -2147483630;
          case KnownColor.Desktop:
            return -2147483647;
          case KnownColor.GrayText:
            return -2147483631;
          case KnownColor.Highlight:
            return -2147483635;
          case KnownColor.HighlightText:
            return -2147483634;
          case KnownColor.HotTrack:
            return -2147483622;
          case KnownColor.InactiveBorder:
            return -2147483637;
          case KnownColor.InactiveCaption:
            return -2147483645;
          case KnownColor.InactiveCaptionText:
            return -2147483629;
          case KnownColor.Info:
            return -2147483624;
          case KnownColor.InfoText:
            return -2147483625;
          case KnownColor.Menu:
            return -2147483644;
          case KnownColor.MenuText:
            return -2147483641;
          case KnownColor.ScrollBar:
            return int.MinValue;
          case KnownColor.Window:
            return -2147483643;
          case KnownColor.WindowFrame:
            return -2147483642;
          case KnownColor.WindowText:
            return -2147483640;
          case KnownColor.ButtonFace:
            return -2147483633;
          case KnownColor.ButtonHighlight:
            return -2147483628;
          case KnownColor.ButtonShadow:
            return -2147483632;
          case KnownColor.GradientActiveCaption:
            return -2147483621;
          case KnownColor.GradientInactiveCaption:
            return -2147483620;
          case KnownColor.MenuBar:
            return -2147483618;
          case KnownColor.MenuHighlight:
            return -2147483619;
        }
      }
      return ColorTranslator.ToWin32(c);
    }

    /// <summary>Translates an OLE color value to a GDI+ <see cref="T:System.Drawing.Color" /> structure.</summary>
    /// <param name="oleColor">The OLE color to translate.</param>
    /// <returns>The <see cref="T:System.Drawing.Color" /> structure that represents the translated OLE color.</returns>
    public static Color FromOle(int oleColor)
    {
      if ((int) ((long) oleColor & 4278190080L) == int.MinValue && (oleColor & 16777215) <= 30)
      {
        switch (oleColor)
        {
          case int.MinValue:
            return Color.FromKnownColor(KnownColor.ScrollBar);
          case -2147483647:
            return Color.FromKnownColor(KnownColor.Desktop);
          case -2147483646:
            return Color.FromKnownColor(KnownColor.ActiveCaption);
          case -2147483645:
            return Color.FromKnownColor(KnownColor.InactiveCaption);
          case -2147483644:
            return Color.FromKnownColor(KnownColor.Menu);
          case -2147483643:
            return Color.FromKnownColor(KnownColor.Window);
          case -2147483642:
            return Color.FromKnownColor(KnownColor.WindowFrame);
          case -2147483641:
            return Color.FromKnownColor(KnownColor.MenuText);
          case -2147483640:
            return Color.FromKnownColor(KnownColor.WindowText);
          case -2147483639:
            return Color.FromKnownColor(KnownColor.ActiveCaptionText);
          case -2147483638:
            return Color.FromKnownColor(KnownColor.ActiveBorder);
          case -2147483637:
            return Color.FromKnownColor(KnownColor.InactiveBorder);
          case -2147483636:
            return Color.FromKnownColor(KnownColor.AppWorkspace);
          case -2147483635:
            return Color.FromKnownColor(KnownColor.Highlight);
          case -2147483634:
            return Color.FromKnownColor(KnownColor.HighlightText);
          case -2147483633:
            return Color.FromKnownColor(KnownColor.Control);
          case -2147483632:
            return Color.FromKnownColor(KnownColor.ControlDark);
          case -2147483631:
            return Color.FromKnownColor(KnownColor.GrayText);
          case -2147483630:
            return Color.FromKnownColor(KnownColor.ControlText);
          case -2147483629:
            return Color.FromKnownColor(KnownColor.InactiveCaptionText);
          case -2147483628:
            return Color.FromKnownColor(KnownColor.ControlLightLight);
          case -2147483627:
            return Color.FromKnownColor(KnownColor.ControlDarkDark);
          case -2147483626:
            return Color.FromKnownColor(KnownColor.ControlLight);
          case -2147483625:
            return Color.FromKnownColor(KnownColor.InfoText);
          case -2147483624:
            return Color.FromKnownColor(KnownColor.Info);
          case -2147483622:
            return Color.FromKnownColor(KnownColor.HotTrack);
          case -2147483621:
            return Color.FromKnownColor(KnownColor.GradientActiveCaption);
          case -2147483620:
            return Color.FromKnownColor(KnownColor.GradientInactiveCaption);
          case -2147483619:
            return Color.FromKnownColor(KnownColor.MenuHighlight);
          case -2147483618:
            return Color.FromKnownColor(KnownColor.MenuBar);
        }
      }
      return KnownColorTable.ArgbToKnownColor(Color.FromArgb((int) (byte) (oleColor & (int) byte.MaxValue), (int) (byte) (oleColor >> 8 & (int) byte.MaxValue), (int) (byte) (oleColor >> 16 & (int) byte.MaxValue)).ToArgb());
    }

    /// <summary>Translates a Windows color value to a GDI+ <see cref="T:System.Drawing.Color" /> structure.</summary>
    /// <param name="win32Color">The Windows color to translate.</param>
    /// <returns>The <see cref="T:System.Drawing.Color" /> structure that represents the translated Windows color.</returns>
    public static Color FromWin32(int win32Color) => ColorTranslator.FromOle(win32Color);

    /// <summary>Translates an HTML color representation to a GDI+ <see cref="T:System.Drawing.Color" /> structure.</summary>
    /// <param name="htmlColor">The string representation of the Html color to translate.</param>
    /// <returns>The <see cref="T:System.Drawing.Color" /> structure that represents the translated HTML color or <see cref="F:System.Drawing.Color.Empty" /> if <paramref name="htmlColor" /> is <see langword="null" />.</returns>
    /// <exception cref="T:System.Exception">
    /// <paramref name="htmlColor" /> is not a valid HTML color name.</exception>
    public static Color FromHtml(string htmlColor)
    {
      Color color = Color.Empty;
      if (htmlColor == null || htmlColor.Length == 0)
        return color;
      if (htmlColor[0] == '#' && (htmlColor.Length == 7 || htmlColor.Length == 4))
      {
        if (htmlColor.Length == 7)
        {
          color = Color.FromArgb(Convert.ToInt32(htmlColor.Substring(1, 2), 16), Convert.ToInt32(htmlColor.Substring(3, 2), 16), Convert.ToInt32(htmlColor.Substring(5, 2), 16));
        }
        else
        {
          string str1 = char.ToString(htmlColor[1]);
          string str2 = char.ToString(htmlColor[2]);
          string str3 = char.ToString(htmlColor[3]);
          color = Color.FromArgb(Convert.ToInt32(str1 + str1, 16), Convert.ToInt32(str2 + str2, 16), Convert.ToInt32(str3 + str3, 16));
        }
      }
      if (color.IsEmpty && string.Equals(htmlColor, "LightGrey", StringComparison.OrdinalIgnoreCase))
        color = Color.LightGray;
      if (color.IsEmpty)
      {
        if (ColorTranslator.htmlSysColorTable == null)
          ColorTranslator.InitializeHtmlSysColorTable();
        object obj = ColorTranslator.htmlSysColorTable[(object) htmlColor.ToLower(CultureInfo.InvariantCulture)];
        if (obj != null)
          color = (Color) obj;
      }
      if (color.IsEmpty)
        color = (Color) TypeDescriptor.GetConverter(typeof (Color)).ConvertFromString(htmlColor);
      return color;
    }

    /// <summary>Translates the specified <see cref="T:System.Drawing.Color" /> structure to an HTML string color representation.</summary>
    /// <param name="c">The <see cref="T:System.Drawing.Color" /> structure to translate.</param>
    /// <returns>The string that represents the HTML color.</returns>
    public static string ToHtml(Color c)
    {
      string html = string.Empty;
      if (c.IsEmpty)
        return html;
      if (c.IsSystemColor)
      {
        switch (c.ToKnownColor())
        {
          case KnownColor.ActiveBorder:
            html = "activeborder";
            break;
          case KnownColor.ActiveCaption:
          case KnownColor.GradientActiveCaption:
            html = "activecaption";
            break;
          case KnownColor.ActiveCaptionText:
            html = "captiontext";
            break;
          case KnownColor.AppWorkspace:
            html = "appworkspace";
            break;
          case KnownColor.Control:
            html = "buttonface";
            break;
          case KnownColor.ControlDark:
            html = "buttonshadow";
            break;
          case KnownColor.ControlDarkDark:
            html = "threeddarkshadow";
            break;
          case KnownColor.ControlLight:
            html = "buttonface";
            break;
          case KnownColor.ControlLightLight:
            html = "buttonhighlight";
            break;
          case KnownColor.ControlText:
            html = "buttontext";
            break;
          case KnownColor.Desktop:
            html = "background";
            break;
          case KnownColor.GrayText:
            html = "graytext";
            break;
          case KnownColor.Highlight:
          case KnownColor.HotTrack:
            html = "highlight";
            break;
          case KnownColor.HighlightText:
          case KnownColor.MenuHighlight:
            html = "highlighttext";
            break;
          case KnownColor.InactiveBorder:
            html = "inactiveborder";
            break;
          case KnownColor.InactiveCaption:
          case KnownColor.GradientInactiveCaption:
            html = "inactivecaption";
            break;
          case KnownColor.InactiveCaptionText:
            html = "inactivecaptiontext";
            break;
          case KnownColor.Info:
            html = "infobackground";
            break;
          case KnownColor.InfoText:
            html = "infotext";
            break;
          case KnownColor.Menu:
          case KnownColor.MenuBar:
            html = "menu";
            break;
          case KnownColor.MenuText:
            html = "menutext";
            break;
          case KnownColor.ScrollBar:
            html = "scrollbar";
            break;
          case KnownColor.Window:
            html = "window";
            break;
          case KnownColor.WindowFrame:
            html = "windowframe";
            break;
          case KnownColor.WindowText:
            html = "windowtext";
            break;
        }
      }
      else if (c.IsNamedColor)
      {
        html = !(c == Color.LightGray) ? c.Name : "LightGrey";
      }
      else
      {
        byte num = c.R;
        string str1 = num.ToString("X2", (IFormatProvider) null);
        num = c.G;
        string str2 = num.ToString("X2", (IFormatProvider) null);
        num = c.B;
        string str3 = num.ToString("X2", (IFormatProvider) null);
        html = "#" + str1 + str2 + str3;
      }
      return html;
    }

    private static void InitializeHtmlSysColorTable()
    {
      ColorTranslator.htmlSysColorTable = new Hashtable(26);
      ColorTranslator.htmlSysColorTable[(object) "activeborder"] = (object) Color.FromKnownColor(KnownColor.ActiveBorder);
      ColorTranslator.htmlSysColorTable[(object) "activecaption"] = (object) Color.FromKnownColor(KnownColor.ActiveCaption);
      ColorTranslator.htmlSysColorTable[(object) "appworkspace"] = (object) Color.FromKnownColor(KnownColor.AppWorkspace);
      ColorTranslator.htmlSysColorTable[(object) "background"] = (object) Color.FromKnownColor(KnownColor.Desktop);
      ColorTranslator.htmlSysColorTable[(object) "buttonface"] = (object) Color.FromKnownColor(KnownColor.Control);
      ColorTranslator.htmlSysColorTable[(object) "buttonhighlight"] = (object) Color.FromKnownColor(KnownColor.ControlLightLight);
      ColorTranslator.htmlSysColorTable[(object) "buttonshadow"] = (object) Color.FromKnownColor(KnownColor.ControlDark);
      ColorTranslator.htmlSysColorTable[(object) "buttontext"] = (object) Color.FromKnownColor(KnownColor.ControlText);
      ColorTranslator.htmlSysColorTable[(object) "captiontext"] = (object) Color.FromKnownColor(KnownColor.ActiveCaptionText);
      ColorTranslator.htmlSysColorTable[(object) "graytext"] = (object) Color.FromKnownColor(KnownColor.GrayText);
      ColorTranslator.htmlSysColorTable[(object) "highlight"] = (object) Color.FromKnownColor(KnownColor.Highlight);
      ColorTranslator.htmlSysColorTable[(object) "highlighttext"] = (object) Color.FromKnownColor(KnownColor.HighlightText);
      ColorTranslator.htmlSysColorTable[(object) "inactiveborder"] = (object) Color.FromKnownColor(KnownColor.InactiveBorder);
      ColorTranslator.htmlSysColorTable[(object) "inactivecaption"] = (object) Color.FromKnownColor(KnownColor.InactiveCaption);
      ColorTranslator.htmlSysColorTable[(object) "inactivecaptiontext"] = (object) Color.FromKnownColor(KnownColor.InactiveCaptionText);
      ColorTranslator.htmlSysColorTable[(object) "infobackground"] = (object) Color.FromKnownColor(KnownColor.Info);
      ColorTranslator.htmlSysColorTable[(object) "infotext"] = (object) Color.FromKnownColor(KnownColor.InfoText);
      ColorTranslator.htmlSysColorTable[(object) "menu"] = (object) Color.FromKnownColor(KnownColor.Menu);
      ColorTranslator.htmlSysColorTable[(object) "menutext"] = (object) Color.FromKnownColor(KnownColor.MenuText);
      ColorTranslator.htmlSysColorTable[(object) "scrollbar"] = (object) Color.FromKnownColor(KnownColor.ScrollBar);
      ColorTranslator.htmlSysColorTable[(object) "threeddarkshadow"] = (object) Color.FromKnownColor(KnownColor.ControlDarkDark);
      ColorTranslator.htmlSysColorTable[(object) "threedface"] = (object) Color.FromKnownColor(KnownColor.Control);
      ColorTranslator.htmlSysColorTable[(object) "threedhighlight"] = (object) Color.FromKnownColor(KnownColor.ControlLight);
      ColorTranslator.htmlSysColorTable[(object) "threedlightshadow"] = (object) Color.FromKnownColor(KnownColor.ControlLightLight);
      ColorTranslator.htmlSysColorTable[(object) "window"] = (object) Color.FromKnownColor(KnownColor.Window);
      ColorTranslator.htmlSysColorTable[(object) "windowframe"] = (object) Color.FromKnownColor(KnownColor.WindowFrame);
      ColorTranslator.htmlSysColorTable[(object) "windowtext"] = (object) Color.FromKnownColor(KnownColor.WindowText);
    }
  }
}
