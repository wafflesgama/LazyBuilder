// Decompiled with JetBrains decompiler
// Type: System.Drawing.Pens
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Pens for all the standard colors. This class cannot be inherited.</summary>
  public sealed class Pens
  {
    private static readonly object TransparentKey = new object();
    private static readonly object AliceBlueKey = new object();
    private static readonly object AntiqueWhiteKey = new object();
    private static readonly object AquaKey = new object();
    private static readonly object AquamarineKey = new object();
    private static readonly object AzureKey = new object();
    private static readonly object BeigeKey = new object();
    private static readonly object BisqueKey = new object();
    private static readonly object BlackKey = new object();
    private static readonly object BlanchedAlmondKey = new object();
    private static readonly object BlueKey = new object();
    private static readonly object BlueVioletKey = new object();
    private static readonly object BrownKey = new object();
    private static readonly object BurlyWoodKey = new object();
    private static readonly object CadetBlueKey = new object();
    private static readonly object ChartreuseKey = new object();
    private static readonly object ChocolateKey = new object();
    private static readonly object ChoralKey = new object();
    private static readonly object CornflowerBlueKey = new object();
    private static readonly object CornsilkKey = new object();
    private static readonly object CrimsonKey = new object();
    private static readonly object CyanKey = new object();
    private static readonly object DarkBlueKey = new object();
    private static readonly object DarkCyanKey = new object();
    private static readonly object DarkGoldenrodKey = new object();
    private static readonly object DarkGrayKey = new object();
    private static readonly object DarkGreenKey = new object();
    private static readonly object DarkKhakiKey = new object();
    private static readonly object DarkMagentaKey = new object();
    private static readonly object DarkOliveGreenKey = new object();
    private static readonly object DarkOrangeKey = new object();
    private static readonly object DarkOrchidKey = new object();
    private static readonly object DarkRedKey = new object();
    private static readonly object DarkSalmonKey = new object();
    private static readonly object DarkSeaGreenKey = new object();
    private static readonly object DarkSlateBlueKey = new object();
    private static readonly object DarkSlateGrayKey = new object();
    private static readonly object DarkTurquoiseKey = new object();
    private static readonly object DarkVioletKey = new object();
    private static readonly object DeepPinkKey = new object();
    private static readonly object DeepSkyBlueKey = new object();
    private static readonly object DimGrayKey = new object();
    private static readonly object DodgerBlueKey = new object();
    private static readonly object FirebrickKey = new object();
    private static readonly object FloralWhiteKey = new object();
    private static readonly object ForestGreenKey = new object();
    private static readonly object FuchiaKey = new object();
    private static readonly object GainsboroKey = new object();
    private static readonly object GhostWhiteKey = new object();
    private static readonly object GoldKey = new object();
    private static readonly object GoldenrodKey = new object();
    private static readonly object GrayKey = new object();
    private static readonly object GreenKey = new object();
    private static readonly object GreenYellowKey = new object();
    private static readonly object HoneydewKey = new object();
    private static readonly object HotPinkKey = new object();
    private static readonly object IndianRedKey = new object();
    private static readonly object IndigoKey = new object();
    private static readonly object IvoryKey = new object();
    private static readonly object KhakiKey = new object();
    private static readonly object LavenderKey = new object();
    private static readonly object LavenderBlushKey = new object();
    private static readonly object LawnGreenKey = new object();
    private static readonly object LemonChiffonKey = new object();
    private static readonly object LightBlueKey = new object();
    private static readonly object LightCoralKey = new object();
    private static readonly object LightCyanKey = new object();
    private static readonly object LightGoldenrodYellowKey = new object();
    private static readonly object LightGreenKey = new object();
    private static readonly object LightGrayKey = new object();
    private static readonly object LightPinkKey = new object();
    private static readonly object LightSalmonKey = new object();
    private static readonly object LightSeaGreenKey = new object();
    private static readonly object LightSkyBlueKey = new object();
    private static readonly object LightSlateGrayKey = new object();
    private static readonly object LightSteelBlueKey = new object();
    private static readonly object LightYellowKey = new object();
    private static readonly object LimeKey = new object();
    private static readonly object LimeGreenKey = new object();
    private static readonly object LinenKey = new object();
    private static readonly object MagentaKey = new object();
    private static readonly object MaroonKey = new object();
    private static readonly object MediumAquamarineKey = new object();
    private static readonly object MediumBlueKey = new object();
    private static readonly object MediumOrchidKey = new object();
    private static readonly object MediumPurpleKey = new object();
    private static readonly object MediumSeaGreenKey = new object();
    private static readonly object MediumSlateBlueKey = new object();
    private static readonly object MediumSpringGreenKey = new object();
    private static readonly object MediumTurquoiseKey = new object();
    private static readonly object MediumVioletRedKey = new object();
    private static readonly object MidnightBlueKey = new object();
    private static readonly object MintCreamKey = new object();
    private static readonly object MistyRoseKey = new object();
    private static readonly object MoccasinKey = new object();
    private static readonly object NavajoWhiteKey = new object();
    private static readonly object NavyKey = new object();
    private static readonly object OldLaceKey = new object();
    private static readonly object OliveKey = new object();
    private static readonly object OliveDrabKey = new object();
    private static readonly object OrangeKey = new object();
    private static readonly object OrangeRedKey = new object();
    private static readonly object OrchidKey = new object();
    private static readonly object PaleGoldenrodKey = new object();
    private static readonly object PaleGreenKey = new object();
    private static readonly object PaleTurquoiseKey = new object();
    private static readonly object PaleVioletRedKey = new object();
    private static readonly object PapayaWhipKey = new object();
    private static readonly object PeachPuffKey = new object();
    private static readonly object PeruKey = new object();
    private static readonly object PinkKey = new object();
    private static readonly object PlumKey = new object();
    private static readonly object PowderBlueKey = new object();
    private static readonly object PurpleKey = new object();
    private static readonly object RedKey = new object();
    private static readonly object RosyBrownKey = new object();
    private static readonly object RoyalBlueKey = new object();
    private static readonly object SaddleBrownKey = new object();
    private static readonly object SalmonKey = new object();
    private static readonly object SandyBrownKey = new object();
    private static readonly object SeaGreenKey = new object();
    private static readonly object SeaShellKey = new object();
    private static readonly object SiennaKey = new object();
    private static readonly object SilverKey = new object();
    private static readonly object SkyBlueKey = new object();
    private static readonly object SlateBlueKey = new object();
    private static readonly object SlateGrayKey = new object();
    private static readonly object SnowKey = new object();
    private static readonly object SpringGreenKey = new object();
    private static readonly object SteelBlueKey = new object();
    private static readonly object TanKey = new object();
    private static readonly object TealKey = new object();
    private static readonly object ThistleKey = new object();
    private static readonly object TomatoKey = new object();
    private static readonly object TurquoiseKey = new object();
    private static readonly object VioletKey = new object();
    private static readonly object WheatKey = new object();
    private static readonly object WhiteKey = new object();
    private static readonly object WhiteSmokeKey = new object();
    private static readonly object YellowKey = new object();
    private static readonly object YellowGreenKey = new object();

    private Pens()
    {
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Transparent
    {
      get
      {
        Pen transparent = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.TransparentKey];
        if (transparent == null)
        {
          transparent = new Pen(Color.Transparent, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.TransparentKey] = (object) transparent;
        }
        return transparent;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen AliceBlue
    {
      get
      {
        Pen aliceBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.AliceBlueKey];
        if (aliceBlue == null)
        {
          aliceBlue = new Pen(Color.AliceBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.AliceBlueKey] = (object) aliceBlue;
        }
        return aliceBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen AntiqueWhite
    {
      get
      {
        Pen antiqueWhite = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.AntiqueWhiteKey];
        if (antiqueWhite == null)
        {
          antiqueWhite = new Pen(Color.AntiqueWhite, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.AntiqueWhiteKey] = (object) antiqueWhite;
        }
        return antiqueWhite;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Aqua
    {
      get
      {
        Pen aqua = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.AquaKey];
        if (aqua == null)
        {
          aqua = new Pen(Color.Aqua, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.AquaKey] = (object) aqua;
        }
        return aqua;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Aquamarine
    {
      get
      {
        Pen aquamarine = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.AquamarineKey];
        if (aquamarine == null)
        {
          aquamarine = new Pen(Color.Aquamarine, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.AquamarineKey] = (object) aquamarine;
        }
        return aquamarine;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Azure
    {
      get
      {
        Pen azure = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.AzureKey];
        if (azure == null)
        {
          azure = new Pen(Color.Azure, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.AzureKey] = (object) azure;
        }
        return azure;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Beige
    {
      get
      {
        Pen beige = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.BeigeKey];
        if (beige == null)
        {
          beige = new Pen(Color.Beige, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.BeigeKey] = (object) beige;
        }
        return beige;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Bisque
    {
      get
      {
        Pen bisque = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.BisqueKey];
        if (bisque == null)
        {
          bisque = new Pen(Color.Bisque, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.BisqueKey] = (object) bisque;
        }
        return bisque;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Black
    {
      get
      {
        Pen black = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.BlackKey];
        if (black == null)
        {
          black = new Pen(Color.Black, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.BlackKey] = (object) black;
        }
        return black;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen BlanchedAlmond
    {
      get
      {
        Pen blanchedAlmond = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.BlanchedAlmondKey];
        if (blanchedAlmond == null)
        {
          blanchedAlmond = new Pen(Color.BlanchedAlmond, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.BlanchedAlmondKey] = (object) blanchedAlmond;
        }
        return blanchedAlmond;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Blue
    {
      get
      {
        Pen blue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.BlueKey];
        if (blue == null)
        {
          blue = new Pen(Color.Blue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.BlueKey] = (object) blue;
        }
        return blue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen BlueViolet
    {
      get
      {
        Pen blueViolet = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.BlueVioletKey];
        if (blueViolet == null)
        {
          blueViolet = new Pen(Color.BlueViolet, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.BlueVioletKey] = (object) blueViolet;
        }
        return blueViolet;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Brown
    {
      get
      {
        Pen brown = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.BrownKey];
        if (brown == null)
        {
          brown = new Pen(Color.Brown, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.BrownKey] = (object) brown;
        }
        return brown;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen BurlyWood
    {
      get
      {
        Pen burlyWood = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.BurlyWoodKey];
        if (burlyWood == null)
        {
          burlyWood = new Pen(Color.BurlyWood, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.BurlyWoodKey] = (object) burlyWood;
        }
        return burlyWood;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen CadetBlue
    {
      get
      {
        Pen cadetBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.CadetBlueKey];
        if (cadetBlue == null)
        {
          cadetBlue = new Pen(Color.CadetBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.CadetBlueKey] = (object) cadetBlue;
        }
        return cadetBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Chartreuse
    {
      get
      {
        Pen chartreuse = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.ChartreuseKey];
        if (chartreuse == null)
        {
          chartreuse = new Pen(Color.Chartreuse, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.ChartreuseKey] = (object) chartreuse;
        }
        return chartreuse;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Chocolate
    {
      get
      {
        Pen chocolate = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.ChocolateKey];
        if (chocolate == null)
        {
          chocolate = new Pen(Color.Chocolate, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.ChocolateKey] = (object) chocolate;
        }
        return chocolate;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Coral
    {
      get
      {
        Pen coral = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.ChoralKey];
        if (coral == null)
        {
          coral = new Pen(Color.Coral, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.ChoralKey] = (object) coral;
        }
        return coral;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen CornflowerBlue
    {
      get
      {
        Pen cornflowerBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.CornflowerBlueKey];
        if (cornflowerBlue == null)
        {
          cornflowerBlue = new Pen(Color.CornflowerBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.CornflowerBlueKey] = (object) cornflowerBlue;
        }
        return cornflowerBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Cornsilk
    {
      get
      {
        Pen cornsilk = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.CornsilkKey];
        if (cornsilk == null)
        {
          cornsilk = new Pen(Color.Cornsilk, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.CornsilkKey] = (object) cornsilk;
        }
        return cornsilk;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Crimson
    {
      get
      {
        Pen crimson = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.CrimsonKey];
        if (crimson == null)
        {
          crimson = new Pen(Color.Crimson, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.CrimsonKey] = (object) crimson;
        }
        return crimson;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Cyan
    {
      get
      {
        Pen cyan = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.CyanKey];
        if (cyan == null)
        {
          cyan = new Pen(Color.Cyan, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.CyanKey] = (object) cyan;
        }
        return cyan;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkBlue
    {
      get
      {
        Pen darkBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkBlueKey];
        if (darkBlue == null)
        {
          darkBlue = new Pen(Color.DarkBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkBlueKey] = (object) darkBlue;
        }
        return darkBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkCyan
    {
      get
      {
        Pen darkCyan = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkCyanKey];
        if (darkCyan == null)
        {
          darkCyan = new Pen(Color.DarkCyan, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkCyanKey] = (object) darkCyan;
        }
        return darkCyan;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkGoldenrod
    {
      get
      {
        Pen darkGoldenrod = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkGoldenrodKey];
        if (darkGoldenrod == null)
        {
          darkGoldenrod = new Pen(Color.DarkGoldenrod, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkGoldenrodKey] = (object) darkGoldenrod;
        }
        return darkGoldenrod;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkGray
    {
      get
      {
        Pen darkGray = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkGrayKey];
        if (darkGray == null)
        {
          darkGray = new Pen(Color.DarkGray, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkGrayKey] = (object) darkGray;
        }
        return darkGray;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkGreen
    {
      get
      {
        Pen darkGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkGreenKey];
        if (darkGreen == null)
        {
          darkGreen = new Pen(Color.DarkGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkGreenKey] = (object) darkGreen;
        }
        return darkGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkKhaki
    {
      get
      {
        Pen darkKhaki = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkKhakiKey];
        if (darkKhaki == null)
        {
          darkKhaki = new Pen(Color.DarkKhaki, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkKhakiKey] = (object) darkKhaki;
        }
        return darkKhaki;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkMagenta
    {
      get
      {
        Pen darkMagenta = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkMagentaKey];
        if (darkMagenta == null)
        {
          darkMagenta = new Pen(Color.DarkMagenta, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkMagentaKey] = (object) darkMagenta;
        }
        return darkMagenta;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkOliveGreen
    {
      get
      {
        Pen darkOliveGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkOliveGreenKey];
        if (darkOliveGreen == null)
        {
          darkOliveGreen = new Pen(Color.DarkOliveGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkOliveGreenKey] = (object) darkOliveGreen;
        }
        return darkOliveGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkOrange
    {
      get
      {
        Pen darkOrange = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkOrangeKey];
        if (darkOrange == null)
        {
          darkOrange = new Pen(Color.DarkOrange, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkOrangeKey] = (object) darkOrange;
        }
        return darkOrange;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkOrchid
    {
      get
      {
        Pen darkOrchid = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkOrchidKey];
        if (darkOrchid == null)
        {
          darkOrchid = new Pen(Color.DarkOrchid, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkOrchidKey] = (object) darkOrchid;
        }
        return darkOrchid;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkRed
    {
      get
      {
        Pen darkRed = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkRedKey];
        if (darkRed == null)
        {
          darkRed = new Pen(Color.DarkRed, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkRedKey] = (object) darkRed;
        }
        return darkRed;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkSalmon
    {
      get
      {
        Pen darkSalmon = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkSalmonKey];
        if (darkSalmon == null)
        {
          darkSalmon = new Pen(Color.DarkSalmon, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkSalmonKey] = (object) darkSalmon;
        }
        return darkSalmon;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkSeaGreen
    {
      get
      {
        Pen darkSeaGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkSeaGreenKey];
        if (darkSeaGreen == null)
        {
          darkSeaGreen = new Pen(Color.DarkSeaGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkSeaGreenKey] = (object) darkSeaGreen;
        }
        return darkSeaGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkSlateBlue
    {
      get
      {
        Pen darkSlateBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkSlateBlueKey];
        if (darkSlateBlue == null)
        {
          darkSlateBlue = new Pen(Color.DarkSlateBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkSlateBlueKey] = (object) darkSlateBlue;
        }
        return darkSlateBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkSlateGray
    {
      get
      {
        Pen darkSlateGray = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkSlateGrayKey];
        if (darkSlateGray == null)
        {
          darkSlateGray = new Pen(Color.DarkSlateGray, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkSlateGrayKey] = (object) darkSlateGray;
        }
        return darkSlateGray;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkTurquoise
    {
      get
      {
        Pen darkTurquoise = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkTurquoiseKey];
        if (darkTurquoise == null)
        {
          darkTurquoise = new Pen(Color.DarkTurquoise, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkTurquoiseKey] = (object) darkTurquoise;
        }
        return darkTurquoise;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DarkViolet
    {
      get
      {
        Pen darkViolet = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DarkVioletKey];
        if (darkViolet == null)
        {
          darkViolet = new Pen(Color.DarkViolet, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DarkVioletKey] = (object) darkViolet;
        }
        return darkViolet;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DeepPink
    {
      get
      {
        Pen deepPink = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DeepPinkKey];
        if (deepPink == null)
        {
          deepPink = new Pen(Color.DeepPink, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DeepPinkKey] = (object) deepPink;
        }
        return deepPink;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DeepSkyBlue
    {
      get
      {
        Pen deepSkyBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DeepSkyBlueKey];
        if (deepSkyBlue == null)
        {
          deepSkyBlue = new Pen(Color.DeepSkyBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DeepSkyBlueKey] = (object) deepSkyBlue;
        }
        return deepSkyBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DimGray
    {
      get
      {
        Pen dimGray = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DimGrayKey];
        if (dimGray == null)
        {
          dimGray = new Pen(Color.DimGray, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DimGrayKey] = (object) dimGray;
        }
        return dimGray;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen DodgerBlue
    {
      get
      {
        Pen dodgerBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.DodgerBlueKey];
        if (dodgerBlue == null)
        {
          dodgerBlue = new Pen(Color.DodgerBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.DodgerBlueKey] = (object) dodgerBlue;
        }
        return dodgerBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Firebrick
    {
      get
      {
        Pen firebrick = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.FirebrickKey];
        if (firebrick == null)
        {
          firebrick = new Pen(Color.Firebrick, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.FirebrickKey] = (object) firebrick;
        }
        return firebrick;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen FloralWhite
    {
      get
      {
        Pen floralWhite = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.FloralWhiteKey];
        if (floralWhite == null)
        {
          floralWhite = new Pen(Color.FloralWhite, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.FloralWhiteKey] = (object) floralWhite;
        }
        return floralWhite;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen ForestGreen
    {
      get
      {
        Pen forestGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.ForestGreenKey];
        if (forestGreen == null)
        {
          forestGreen = new Pen(Color.ForestGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.ForestGreenKey] = (object) forestGreen;
        }
        return forestGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Fuchsia
    {
      get
      {
        Pen fuchsia = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.FuchiaKey];
        if (fuchsia == null)
        {
          fuchsia = new Pen(Color.Fuchsia, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.FuchiaKey] = (object) fuchsia;
        }
        return fuchsia;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Gainsboro
    {
      get
      {
        Pen gainsboro = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.GainsboroKey];
        if (gainsboro == null)
        {
          gainsboro = new Pen(Color.Gainsboro, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.GainsboroKey] = (object) gainsboro;
        }
        return gainsboro;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen GhostWhite
    {
      get
      {
        Pen ghostWhite = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.GhostWhiteKey];
        if (ghostWhite == null)
        {
          ghostWhite = new Pen(Color.GhostWhite, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.GhostWhiteKey] = (object) ghostWhite;
        }
        return ghostWhite;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Gold
    {
      get
      {
        Pen gold = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.GoldKey];
        if (gold == null)
        {
          gold = new Pen(Color.Gold, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.GoldKey] = (object) gold;
        }
        return gold;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Goldenrod
    {
      get
      {
        Pen goldenrod = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.GoldenrodKey];
        if (goldenrod == null)
        {
          goldenrod = new Pen(Color.Goldenrod, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.GoldenrodKey] = (object) goldenrod;
        }
        return goldenrod;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Gray
    {
      get
      {
        Pen gray = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.GrayKey];
        if (gray == null)
        {
          gray = new Pen(Color.Gray, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.GrayKey] = (object) gray;
        }
        return gray;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Green
    {
      get
      {
        Pen green = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.GreenKey];
        if (green == null)
        {
          green = new Pen(Color.Green, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.GreenKey] = (object) green;
        }
        return green;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen GreenYellow
    {
      get
      {
        Pen greenYellow = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.GreenYellowKey];
        if (greenYellow == null)
        {
          greenYellow = new Pen(Color.GreenYellow, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.GreenYellowKey] = (object) greenYellow;
        }
        return greenYellow;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Honeydew
    {
      get
      {
        Pen honeydew = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.HoneydewKey];
        if (honeydew == null)
        {
          honeydew = new Pen(Color.Honeydew, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.HoneydewKey] = (object) honeydew;
        }
        return honeydew;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen HotPink
    {
      get
      {
        Pen hotPink = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.HotPinkKey];
        if (hotPink == null)
        {
          hotPink = new Pen(Color.HotPink, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.HotPinkKey] = (object) hotPink;
        }
        return hotPink;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen IndianRed
    {
      get
      {
        Pen indianRed = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.IndianRedKey];
        if (indianRed == null)
        {
          indianRed = new Pen(Color.IndianRed, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.IndianRedKey] = (object) indianRed;
        }
        return indianRed;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Indigo
    {
      get
      {
        Pen indigo = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.IndigoKey];
        if (indigo == null)
        {
          indigo = new Pen(Color.Indigo, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.IndigoKey] = (object) indigo;
        }
        return indigo;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Ivory
    {
      get
      {
        Pen ivory = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.IvoryKey];
        if (ivory == null)
        {
          ivory = new Pen(Color.Ivory, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.IvoryKey] = (object) ivory;
        }
        return ivory;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Khaki
    {
      get
      {
        Pen khaki = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.KhakiKey];
        if (khaki == null)
        {
          khaki = new Pen(Color.Khaki, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.KhakiKey] = (object) khaki;
        }
        return khaki;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Lavender
    {
      get
      {
        Pen lavender = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LavenderKey];
        if (lavender == null)
        {
          lavender = new Pen(Color.Lavender, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LavenderKey] = (object) lavender;
        }
        return lavender;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LavenderBlush
    {
      get
      {
        Pen lavenderBlush = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LavenderBlushKey];
        if (lavenderBlush == null)
        {
          lavenderBlush = new Pen(Color.LavenderBlush, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LavenderBlushKey] = (object) lavenderBlush;
        }
        return lavenderBlush;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LawnGreen
    {
      get
      {
        Pen lawnGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LawnGreenKey];
        if (lawnGreen == null)
        {
          lawnGreen = new Pen(Color.LawnGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LawnGreenKey] = (object) lawnGreen;
        }
        return lawnGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LemonChiffon
    {
      get
      {
        Pen lemonChiffon = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LemonChiffonKey];
        if (lemonChiffon == null)
        {
          lemonChiffon = new Pen(Color.LemonChiffon, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LemonChiffonKey] = (object) lemonChiffon;
        }
        return lemonChiffon;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightBlue
    {
      get
      {
        Pen lightBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightBlueKey];
        if (lightBlue == null)
        {
          lightBlue = new Pen(Color.LightBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightBlueKey] = (object) lightBlue;
        }
        return lightBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightCoral
    {
      get
      {
        Pen lightCoral = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightCoralKey];
        if (lightCoral == null)
        {
          lightCoral = new Pen(Color.LightCoral, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightCoralKey] = (object) lightCoral;
        }
        return lightCoral;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightCyan
    {
      get
      {
        Pen lightCyan = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightCyanKey];
        if (lightCyan == null)
        {
          lightCyan = new Pen(Color.LightCyan, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightCyanKey] = (object) lightCyan;
        }
        return lightCyan;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightGoldenrodYellow
    {
      get
      {
        Pen lightGoldenrodYellow = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightGoldenrodYellowKey];
        if (lightGoldenrodYellow == null)
        {
          lightGoldenrodYellow = new Pen(Color.LightGoldenrodYellow, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightGoldenrodYellowKey] = (object) lightGoldenrodYellow;
        }
        return lightGoldenrodYellow;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightGreen
    {
      get
      {
        Pen lightGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightGreenKey];
        if (lightGreen == null)
        {
          lightGreen = new Pen(Color.LightGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightGreenKey] = (object) lightGreen;
        }
        return lightGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightGray
    {
      get
      {
        Pen lightGray = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightGrayKey];
        if (lightGray == null)
        {
          lightGray = new Pen(Color.LightGray, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightGrayKey] = (object) lightGray;
        }
        return lightGray;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightPink
    {
      get
      {
        Pen lightPink = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightPinkKey];
        if (lightPink == null)
        {
          lightPink = new Pen(Color.LightPink, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightPinkKey] = (object) lightPink;
        }
        return lightPink;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightSalmon
    {
      get
      {
        Pen lightSalmon = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightSalmonKey];
        if (lightSalmon == null)
        {
          lightSalmon = new Pen(Color.LightSalmon, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightSalmonKey] = (object) lightSalmon;
        }
        return lightSalmon;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightSeaGreen
    {
      get
      {
        Pen lightSeaGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightSeaGreenKey];
        if (lightSeaGreen == null)
        {
          lightSeaGreen = new Pen(Color.LightSeaGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightSeaGreenKey] = (object) lightSeaGreen;
        }
        return lightSeaGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightSkyBlue
    {
      get
      {
        Pen lightSkyBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightSkyBlueKey];
        if (lightSkyBlue == null)
        {
          lightSkyBlue = new Pen(Color.LightSkyBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightSkyBlueKey] = (object) lightSkyBlue;
        }
        return lightSkyBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightSlateGray
    {
      get
      {
        Pen lightSlateGray = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightSlateGrayKey];
        if (lightSlateGray == null)
        {
          lightSlateGray = new Pen(Color.LightSlateGray, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightSlateGrayKey] = (object) lightSlateGray;
        }
        return lightSlateGray;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightSteelBlue
    {
      get
      {
        Pen lightSteelBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightSteelBlueKey];
        if (lightSteelBlue == null)
        {
          lightSteelBlue = new Pen(Color.LightSteelBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightSteelBlueKey] = (object) lightSteelBlue;
        }
        return lightSteelBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LightYellow
    {
      get
      {
        Pen lightYellow = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LightYellowKey];
        if (lightYellow == null)
        {
          lightYellow = new Pen(Color.LightYellow, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LightYellowKey] = (object) lightYellow;
        }
        return lightYellow;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Lime
    {
      get
      {
        Pen lime = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LimeKey];
        if (lime == null)
        {
          lime = new Pen(Color.Lime, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LimeKey] = (object) lime;
        }
        return lime;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen LimeGreen
    {
      get
      {
        Pen limeGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LimeGreenKey];
        if (limeGreen == null)
        {
          limeGreen = new Pen(Color.LimeGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LimeGreenKey] = (object) limeGreen;
        }
        return limeGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Linen
    {
      get
      {
        Pen linen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.LinenKey];
        if (linen == null)
        {
          linen = new Pen(Color.Linen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.LinenKey] = (object) linen;
        }
        return linen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Magenta
    {
      get
      {
        Pen magenta = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MagentaKey];
        if (magenta == null)
        {
          magenta = new Pen(Color.Magenta, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MagentaKey] = (object) magenta;
        }
        return magenta;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Maroon
    {
      get
      {
        Pen maroon = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MaroonKey];
        if (maroon == null)
        {
          maroon = new Pen(Color.Maroon, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MaroonKey] = (object) maroon;
        }
        return maroon;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumAquamarine
    {
      get
      {
        Pen mediumAquamarine = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumAquamarineKey];
        if (mediumAquamarine == null)
        {
          mediumAquamarine = new Pen(Color.MediumAquamarine, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumAquamarineKey] = (object) mediumAquamarine;
        }
        return mediumAquamarine;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumBlue
    {
      get
      {
        Pen mediumBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumBlueKey];
        if (mediumBlue == null)
        {
          mediumBlue = new Pen(Color.MediumBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumBlueKey] = (object) mediumBlue;
        }
        return mediumBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumOrchid
    {
      get
      {
        Pen mediumOrchid = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumOrchidKey];
        if (mediumOrchid == null)
        {
          mediumOrchid = new Pen(Color.MediumOrchid, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumOrchidKey] = (object) mediumOrchid;
        }
        return mediumOrchid;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumPurple
    {
      get
      {
        Pen mediumPurple = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumPurpleKey];
        if (mediumPurple == null)
        {
          mediumPurple = new Pen(Color.MediumPurple, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumPurpleKey] = (object) mediumPurple;
        }
        return mediumPurple;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumSeaGreen
    {
      get
      {
        Pen mediumSeaGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumSeaGreenKey];
        if (mediumSeaGreen == null)
        {
          mediumSeaGreen = new Pen(Color.MediumSeaGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumSeaGreenKey] = (object) mediumSeaGreen;
        }
        return mediumSeaGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumSlateBlue
    {
      get
      {
        Pen mediumSlateBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumSlateBlueKey];
        if (mediumSlateBlue == null)
        {
          mediumSlateBlue = new Pen(Color.MediumSlateBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumSlateBlueKey] = (object) mediumSlateBlue;
        }
        return mediumSlateBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumSpringGreen
    {
      get
      {
        Pen mediumSpringGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumSpringGreenKey];
        if (mediumSpringGreen == null)
        {
          mediumSpringGreen = new Pen(Color.MediumSpringGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumSpringGreenKey] = (object) mediumSpringGreen;
        }
        return mediumSpringGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumTurquoise
    {
      get
      {
        Pen mediumTurquoise = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumTurquoiseKey];
        if (mediumTurquoise == null)
        {
          mediumTurquoise = new Pen(Color.MediumTurquoise, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumTurquoiseKey] = (object) mediumTurquoise;
        }
        return mediumTurquoise;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MediumVioletRed
    {
      get
      {
        Pen mediumVioletRed = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MediumVioletRedKey];
        if (mediumVioletRed == null)
        {
          mediumVioletRed = new Pen(Color.MediumVioletRed, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MediumVioletRedKey] = (object) mediumVioletRed;
        }
        return mediumVioletRed;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MidnightBlue
    {
      get
      {
        Pen midnightBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MidnightBlueKey];
        if (midnightBlue == null)
        {
          midnightBlue = new Pen(Color.MidnightBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MidnightBlueKey] = (object) midnightBlue;
        }
        return midnightBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MintCream
    {
      get
      {
        Pen mintCream = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MintCreamKey];
        if (mintCream == null)
        {
          mintCream = new Pen(Color.MintCream, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MintCreamKey] = (object) mintCream;
        }
        return mintCream;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen MistyRose
    {
      get
      {
        Pen mistyRose = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MistyRoseKey];
        if (mistyRose == null)
        {
          mistyRose = new Pen(Color.MistyRose, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MistyRoseKey] = (object) mistyRose;
        }
        return mistyRose;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Moccasin
    {
      get
      {
        Pen moccasin = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.MoccasinKey];
        if (moccasin == null)
        {
          moccasin = new Pen(Color.Moccasin, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.MoccasinKey] = (object) moccasin;
        }
        return moccasin;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen NavajoWhite
    {
      get
      {
        Pen navajoWhite = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.NavajoWhiteKey];
        if (navajoWhite == null)
        {
          navajoWhite = new Pen(Color.NavajoWhite, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.NavajoWhiteKey] = (object) navajoWhite;
        }
        return navajoWhite;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Navy
    {
      get
      {
        Pen navy = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.NavyKey];
        if (navy == null)
        {
          navy = new Pen(Color.Navy, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.NavyKey] = (object) navy;
        }
        return navy;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen OldLace
    {
      get
      {
        Pen oldLace = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.OldLaceKey];
        if (oldLace == null)
        {
          oldLace = new Pen(Color.OldLace, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.OldLaceKey] = (object) oldLace;
        }
        return oldLace;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Olive
    {
      get
      {
        Pen olive = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.OliveKey];
        if (olive == null)
        {
          olive = new Pen(Color.Olive, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.OliveKey] = (object) olive;
        }
        return olive;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen OliveDrab
    {
      get
      {
        Pen oliveDrab = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.OliveDrabKey];
        if (oliveDrab == null)
        {
          oliveDrab = new Pen(Color.OliveDrab, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.OliveDrabKey] = (object) oliveDrab;
        }
        return oliveDrab;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Orange
    {
      get
      {
        Pen orange = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.OrangeKey];
        if (orange == null)
        {
          orange = new Pen(Color.Orange, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.OrangeKey] = (object) orange;
        }
        return orange;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen OrangeRed
    {
      get
      {
        Pen orangeRed = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.OrangeRedKey];
        if (orangeRed == null)
        {
          orangeRed = new Pen(Color.OrangeRed, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.OrangeRedKey] = (object) orangeRed;
        }
        return orangeRed;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Orchid
    {
      get
      {
        Pen orchid = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.OrchidKey];
        if (orchid == null)
        {
          orchid = new Pen(Color.Orchid, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.OrchidKey] = (object) orchid;
        }
        return orchid;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen PaleGoldenrod
    {
      get
      {
        Pen paleGoldenrod = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PaleGoldenrodKey];
        if (paleGoldenrod == null)
        {
          paleGoldenrod = new Pen(Color.PaleGoldenrod, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PaleGoldenrodKey] = (object) paleGoldenrod;
        }
        return paleGoldenrod;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen PaleGreen
    {
      get
      {
        Pen paleGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PaleGreenKey];
        if (paleGreen == null)
        {
          paleGreen = new Pen(Color.PaleGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PaleGreenKey] = (object) paleGreen;
        }
        return paleGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen PaleTurquoise
    {
      get
      {
        Pen paleTurquoise = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PaleTurquoiseKey];
        if (paleTurquoise == null)
        {
          paleTurquoise = new Pen(Color.PaleTurquoise, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PaleTurquoiseKey] = (object) paleTurquoise;
        }
        return paleTurquoise;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen PaleVioletRed
    {
      get
      {
        Pen paleVioletRed = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PaleVioletRedKey];
        if (paleVioletRed == null)
        {
          paleVioletRed = new Pen(Color.PaleVioletRed, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PaleVioletRedKey] = (object) paleVioletRed;
        }
        return paleVioletRed;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen PapayaWhip
    {
      get
      {
        Pen papayaWhip = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PapayaWhipKey];
        if (papayaWhip == null)
        {
          papayaWhip = new Pen(Color.PapayaWhip, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PapayaWhipKey] = (object) papayaWhip;
        }
        return papayaWhip;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen PeachPuff
    {
      get
      {
        Pen peachPuff = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PeachPuffKey];
        if (peachPuff == null)
        {
          peachPuff = new Pen(Color.PeachPuff, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PeachPuffKey] = (object) peachPuff;
        }
        return peachPuff;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Peru
    {
      get
      {
        Pen peru = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PeruKey];
        if (peru == null)
        {
          peru = new Pen(Color.Peru, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PeruKey] = (object) peru;
        }
        return peru;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Pink
    {
      get
      {
        Pen pink = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PinkKey];
        if (pink == null)
        {
          pink = new Pen(Color.Pink, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PinkKey] = (object) pink;
        }
        return pink;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Plum
    {
      get
      {
        Pen plum = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PlumKey];
        if (plum == null)
        {
          plum = new Pen(Color.Plum, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PlumKey] = (object) plum;
        }
        return plum;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen PowderBlue
    {
      get
      {
        Pen powderBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PowderBlueKey];
        if (powderBlue == null)
        {
          powderBlue = new Pen(Color.PowderBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PowderBlueKey] = (object) powderBlue;
        }
        return powderBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Purple
    {
      get
      {
        Pen purple = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.PurpleKey];
        if (purple == null)
        {
          purple = new Pen(Color.Purple, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.PurpleKey] = (object) purple;
        }
        return purple;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Red
    {
      get
      {
        Pen red = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.RedKey];
        if (red == null)
        {
          red = new Pen(Color.Red, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.RedKey] = (object) red;
        }
        return red;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen RosyBrown
    {
      get
      {
        Pen rosyBrown = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.RosyBrownKey];
        if (rosyBrown == null)
        {
          rosyBrown = new Pen(Color.RosyBrown, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.RosyBrownKey] = (object) rosyBrown;
        }
        return rosyBrown;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen RoyalBlue
    {
      get
      {
        Pen royalBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.RoyalBlueKey];
        if (royalBlue == null)
        {
          royalBlue = new Pen(Color.RoyalBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.RoyalBlueKey] = (object) royalBlue;
        }
        return royalBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SaddleBrown
    {
      get
      {
        Pen saddleBrown = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SaddleBrownKey];
        if (saddleBrown == null)
        {
          saddleBrown = new Pen(Color.SaddleBrown, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SaddleBrownKey] = (object) saddleBrown;
        }
        return saddleBrown;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Salmon
    {
      get
      {
        Pen salmon = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SalmonKey];
        if (salmon == null)
        {
          salmon = new Pen(Color.Salmon, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SalmonKey] = (object) salmon;
        }
        return salmon;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SandyBrown
    {
      get
      {
        Pen sandyBrown = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SandyBrownKey];
        if (sandyBrown == null)
        {
          sandyBrown = new Pen(Color.SandyBrown, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SandyBrownKey] = (object) sandyBrown;
        }
        return sandyBrown;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SeaGreen
    {
      get
      {
        Pen seaGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SeaGreenKey];
        if (seaGreen == null)
        {
          seaGreen = new Pen(Color.SeaGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SeaGreenKey] = (object) seaGreen;
        }
        return seaGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SeaShell
    {
      get
      {
        Pen seaShell = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SeaShellKey];
        if (seaShell == null)
        {
          seaShell = new Pen(Color.SeaShell, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SeaShellKey] = (object) seaShell;
        }
        return seaShell;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Sienna
    {
      get
      {
        Pen sienna = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SiennaKey];
        if (sienna == null)
        {
          sienna = new Pen(Color.Sienna, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SiennaKey] = (object) sienna;
        }
        return sienna;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Silver
    {
      get
      {
        Pen silver = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SilverKey];
        if (silver == null)
        {
          silver = new Pen(Color.Silver, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SilverKey] = (object) silver;
        }
        return silver;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SkyBlue
    {
      get
      {
        Pen skyBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SkyBlueKey];
        if (skyBlue == null)
        {
          skyBlue = new Pen(Color.SkyBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SkyBlueKey] = (object) skyBlue;
        }
        return skyBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SlateBlue
    {
      get
      {
        Pen slateBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SlateBlueKey];
        if (slateBlue == null)
        {
          slateBlue = new Pen(Color.SlateBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SlateBlueKey] = (object) slateBlue;
        }
        return slateBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SlateGray
    {
      get
      {
        Pen slateGray = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SlateGrayKey];
        if (slateGray == null)
        {
          slateGray = new Pen(Color.SlateGray, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SlateGrayKey] = (object) slateGray;
        }
        return slateGray;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Snow
    {
      get
      {
        Pen snow = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SnowKey];
        if (snow == null)
        {
          snow = new Pen(Color.Snow, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SnowKey] = (object) snow;
        }
        return snow;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SpringGreen
    {
      get
      {
        Pen springGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SpringGreenKey];
        if (springGreen == null)
        {
          springGreen = new Pen(Color.SpringGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SpringGreenKey] = (object) springGreen;
        }
        return springGreen;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen SteelBlue
    {
      get
      {
        Pen steelBlue = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.SteelBlueKey];
        if (steelBlue == null)
        {
          steelBlue = new Pen(Color.SteelBlue, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.SteelBlueKey] = (object) steelBlue;
        }
        return steelBlue;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Tan
    {
      get
      {
        Pen tan = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.TanKey];
        if (tan == null)
        {
          tan = new Pen(Color.Tan, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.TanKey] = (object) tan;
        }
        return tan;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Teal
    {
      get
      {
        Pen teal = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.TealKey];
        if (teal == null)
        {
          teal = new Pen(Color.Teal, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.TealKey] = (object) teal;
        }
        return teal;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Thistle
    {
      get
      {
        Pen thistle = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.ThistleKey];
        if (thistle == null)
        {
          thistle = new Pen(Color.Thistle, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.ThistleKey] = (object) thistle;
        }
        return thistle;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Tomato
    {
      get
      {
        Pen tomato = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.TomatoKey];
        if (tomato == null)
        {
          tomato = new Pen(Color.Tomato, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.TomatoKey] = (object) tomato;
        }
        return tomato;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Turquoise
    {
      get
      {
        Pen turquoise = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.TurquoiseKey];
        if (turquoise == null)
        {
          turquoise = new Pen(Color.Turquoise, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.TurquoiseKey] = (object) turquoise;
        }
        return turquoise;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Violet
    {
      get
      {
        Pen violet = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.VioletKey];
        if (violet == null)
        {
          violet = new Pen(Color.Violet, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.VioletKey] = (object) violet;
        }
        return violet;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Wheat
    {
      get
      {
        Pen wheat = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.WheatKey];
        if (wheat == null)
        {
          wheat = new Pen(Color.Wheat, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.WheatKey] = (object) wheat;
        }
        return wheat;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen White
    {
      get
      {
        Pen white = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.WhiteKey];
        if (white == null)
        {
          white = new Pen(Color.White, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.WhiteKey] = (object) white;
        }
        return white;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen WhiteSmoke
    {
      get
      {
        Pen whiteSmoke = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.WhiteSmokeKey];
        if (whiteSmoke == null)
        {
          whiteSmoke = new Pen(Color.WhiteSmoke, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.WhiteSmokeKey] = (object) whiteSmoke;
        }
        return whiteSmoke;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen Yellow
    {
      get
      {
        Pen yellow = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.YellowKey];
        if (yellow == null)
        {
          yellow = new Pen(Color.Yellow, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.YellowKey] = (object) yellow;
        }
        return yellow;
      }
    }

    /// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
    /// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
    public static Pen YellowGreen
    {
      get
      {
        Pen yellowGreen = (Pen) SafeNativeMethods.Gdip.ThreadData[Pens.YellowGreenKey];
        if (yellowGreen == null)
        {
          yellowGreen = new Pen(Color.YellowGreen, true);
          SafeNativeMethods.Gdip.ThreadData[Pens.YellowGreenKey] = (object) yellowGreen;
        }
        return yellowGreen;
      }
    }
  }
}
