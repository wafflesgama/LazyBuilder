// Decompiled with JetBrains decompiler
// Type: System.Drawing.Brushes
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Brushes for all the standard colors. This class cannot be inherited.</summary>
  public sealed class Brushes
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

    private Brushes()
    {
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Transparent
    {
      get
      {
        Brush transparent = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.TransparentKey];
        if (transparent == null)
        {
          transparent = (Brush) new SolidBrush(Color.Transparent);
          SafeNativeMethods.Gdip.ThreadData[Brushes.TransparentKey] = (object) transparent;
        }
        return transparent;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush AliceBlue
    {
      get
      {
        Brush aliceBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.AliceBlueKey];
        if (aliceBlue == null)
        {
          aliceBlue = (Brush) new SolidBrush(Color.AliceBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.AliceBlueKey] = (object) aliceBlue;
        }
        return aliceBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush AntiqueWhite
    {
      get
      {
        Brush antiqueWhite = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.AntiqueWhiteKey];
        if (antiqueWhite == null)
        {
          antiqueWhite = (Brush) new SolidBrush(Color.AntiqueWhite);
          SafeNativeMethods.Gdip.ThreadData[Brushes.AntiqueWhiteKey] = (object) antiqueWhite;
        }
        return antiqueWhite;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Aqua
    {
      get
      {
        Brush aqua = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.AquaKey];
        if (aqua == null)
        {
          aqua = (Brush) new SolidBrush(Color.Aqua);
          SafeNativeMethods.Gdip.ThreadData[Brushes.AquaKey] = (object) aqua;
        }
        return aqua;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Aquamarine
    {
      get
      {
        Brush aquamarine = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.AquamarineKey];
        if (aquamarine == null)
        {
          aquamarine = (Brush) new SolidBrush(Color.Aquamarine);
          SafeNativeMethods.Gdip.ThreadData[Brushes.AquamarineKey] = (object) aquamarine;
        }
        return aquamarine;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Azure
    {
      get
      {
        Brush azure = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.AzureKey];
        if (azure == null)
        {
          azure = (Brush) new SolidBrush(Color.Azure);
          SafeNativeMethods.Gdip.ThreadData[Brushes.AzureKey] = (object) azure;
        }
        return azure;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Beige
    {
      get
      {
        Brush beige = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.BeigeKey];
        if (beige == null)
        {
          beige = (Brush) new SolidBrush(Color.Beige);
          SafeNativeMethods.Gdip.ThreadData[Brushes.BeigeKey] = (object) beige;
        }
        return beige;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Bisque
    {
      get
      {
        Brush bisque = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.BisqueKey];
        if (bisque == null)
        {
          bisque = (Brush) new SolidBrush(Color.Bisque);
          SafeNativeMethods.Gdip.ThreadData[Brushes.BisqueKey] = (object) bisque;
        }
        return bisque;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Black
    {
      get
      {
        Brush black = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.BlackKey];
        if (black == null)
        {
          black = (Brush) new SolidBrush(Color.Black);
          SafeNativeMethods.Gdip.ThreadData[Brushes.BlackKey] = (object) black;
        }
        return black;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush BlanchedAlmond
    {
      get
      {
        Brush blanchedAlmond = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.BlanchedAlmondKey];
        if (blanchedAlmond == null)
        {
          blanchedAlmond = (Brush) new SolidBrush(Color.BlanchedAlmond);
          SafeNativeMethods.Gdip.ThreadData[Brushes.BlanchedAlmondKey] = (object) blanchedAlmond;
        }
        return blanchedAlmond;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Blue
    {
      get
      {
        Brush blue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.BlueKey];
        if (blue == null)
        {
          blue = (Brush) new SolidBrush(Color.Blue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.BlueKey] = (object) blue;
        }
        return blue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush BlueViolet
    {
      get
      {
        Brush blueViolet = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.BlueVioletKey];
        if (blueViolet == null)
        {
          blueViolet = (Brush) new SolidBrush(Color.BlueViolet);
          SafeNativeMethods.Gdip.ThreadData[Brushes.BlueVioletKey] = (object) blueViolet;
        }
        return blueViolet;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Brown
    {
      get
      {
        Brush brown = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.BrownKey];
        if (brown == null)
        {
          brown = (Brush) new SolidBrush(Color.Brown);
          SafeNativeMethods.Gdip.ThreadData[Brushes.BrownKey] = (object) brown;
        }
        return brown;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush BurlyWood
    {
      get
      {
        Brush burlyWood = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.BurlyWoodKey];
        if (burlyWood == null)
        {
          burlyWood = (Brush) new SolidBrush(Color.BurlyWood);
          SafeNativeMethods.Gdip.ThreadData[Brushes.BurlyWoodKey] = (object) burlyWood;
        }
        return burlyWood;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush CadetBlue
    {
      get
      {
        Brush cadetBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.CadetBlueKey];
        if (cadetBlue == null)
        {
          cadetBlue = (Brush) new SolidBrush(Color.CadetBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.CadetBlueKey] = (object) cadetBlue;
        }
        return cadetBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Chartreuse
    {
      get
      {
        Brush chartreuse = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.ChartreuseKey];
        if (chartreuse == null)
        {
          chartreuse = (Brush) new SolidBrush(Color.Chartreuse);
          SafeNativeMethods.Gdip.ThreadData[Brushes.ChartreuseKey] = (object) chartreuse;
        }
        return chartreuse;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Chocolate
    {
      get
      {
        Brush chocolate = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.ChocolateKey];
        if (chocolate == null)
        {
          chocolate = (Brush) new SolidBrush(Color.Chocolate);
          SafeNativeMethods.Gdip.ThreadData[Brushes.ChocolateKey] = (object) chocolate;
        }
        return chocolate;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Coral
    {
      get
      {
        Brush coral = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.ChoralKey];
        if (coral == null)
        {
          coral = (Brush) new SolidBrush(Color.Coral);
          SafeNativeMethods.Gdip.ThreadData[Brushes.ChoralKey] = (object) coral;
        }
        return coral;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush CornflowerBlue
    {
      get
      {
        Brush cornflowerBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.CornflowerBlueKey];
        if (cornflowerBlue == null)
        {
          cornflowerBlue = (Brush) new SolidBrush(Color.CornflowerBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.CornflowerBlueKey] = (object) cornflowerBlue;
        }
        return cornflowerBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Cornsilk
    {
      get
      {
        Brush cornsilk = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.CornsilkKey];
        if (cornsilk == null)
        {
          cornsilk = (Brush) new SolidBrush(Color.Cornsilk);
          SafeNativeMethods.Gdip.ThreadData[Brushes.CornsilkKey] = (object) cornsilk;
        }
        return cornsilk;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Crimson
    {
      get
      {
        Brush crimson = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.CrimsonKey];
        if (crimson == null)
        {
          crimson = (Brush) new SolidBrush(Color.Crimson);
          SafeNativeMethods.Gdip.ThreadData[Brushes.CrimsonKey] = (object) crimson;
        }
        return crimson;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Cyan
    {
      get
      {
        Brush cyan = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.CyanKey];
        if (cyan == null)
        {
          cyan = (Brush) new SolidBrush(Color.Cyan);
          SafeNativeMethods.Gdip.ThreadData[Brushes.CyanKey] = (object) cyan;
        }
        return cyan;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkBlue
    {
      get
      {
        Brush darkBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkBlueKey];
        if (darkBlue == null)
        {
          darkBlue = (Brush) new SolidBrush(Color.DarkBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkBlueKey] = (object) darkBlue;
        }
        return darkBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkCyan
    {
      get
      {
        Brush darkCyan = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkCyanKey];
        if (darkCyan == null)
        {
          darkCyan = (Brush) new SolidBrush(Color.DarkCyan);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkCyanKey] = (object) darkCyan;
        }
        return darkCyan;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkGoldenrod
    {
      get
      {
        Brush darkGoldenrod = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkGoldenrodKey];
        if (darkGoldenrod == null)
        {
          darkGoldenrod = (Brush) new SolidBrush(Color.DarkGoldenrod);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkGoldenrodKey] = (object) darkGoldenrod;
        }
        return darkGoldenrod;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkGray
    {
      get
      {
        Brush darkGray = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkGrayKey];
        if (darkGray == null)
        {
          darkGray = (Brush) new SolidBrush(Color.DarkGray);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkGrayKey] = (object) darkGray;
        }
        return darkGray;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkGreen
    {
      get
      {
        Brush darkGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkGreenKey];
        if (darkGreen == null)
        {
          darkGreen = (Brush) new SolidBrush(Color.DarkGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkGreenKey] = (object) darkGreen;
        }
        return darkGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkKhaki
    {
      get
      {
        Brush darkKhaki = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkKhakiKey];
        if (darkKhaki == null)
        {
          darkKhaki = (Brush) new SolidBrush(Color.DarkKhaki);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkKhakiKey] = (object) darkKhaki;
        }
        return darkKhaki;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkMagenta
    {
      get
      {
        Brush darkMagenta = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkMagentaKey];
        if (darkMagenta == null)
        {
          darkMagenta = (Brush) new SolidBrush(Color.DarkMagenta);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkMagentaKey] = (object) darkMagenta;
        }
        return darkMagenta;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkOliveGreen
    {
      get
      {
        Brush darkOliveGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkOliveGreenKey];
        if (darkOliveGreen == null)
        {
          darkOliveGreen = (Brush) new SolidBrush(Color.DarkOliveGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkOliveGreenKey] = (object) darkOliveGreen;
        }
        return darkOliveGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkOrange
    {
      get
      {
        Brush darkOrange = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkOrangeKey];
        if (darkOrange == null)
        {
          darkOrange = (Brush) new SolidBrush(Color.DarkOrange);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkOrangeKey] = (object) darkOrange;
        }
        return darkOrange;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkOrchid
    {
      get
      {
        Brush darkOrchid = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkOrchidKey];
        if (darkOrchid == null)
        {
          darkOrchid = (Brush) new SolidBrush(Color.DarkOrchid);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkOrchidKey] = (object) darkOrchid;
        }
        return darkOrchid;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkRed
    {
      get
      {
        Brush darkRed = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkRedKey];
        if (darkRed == null)
        {
          darkRed = (Brush) new SolidBrush(Color.DarkRed);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkRedKey] = (object) darkRed;
        }
        return darkRed;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkSalmon
    {
      get
      {
        Brush darkSalmon = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkSalmonKey];
        if (darkSalmon == null)
        {
          darkSalmon = (Brush) new SolidBrush(Color.DarkSalmon);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkSalmonKey] = (object) darkSalmon;
        }
        return darkSalmon;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkSeaGreen
    {
      get
      {
        Brush darkSeaGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkSeaGreenKey];
        if (darkSeaGreen == null)
        {
          darkSeaGreen = (Brush) new SolidBrush(Color.DarkSeaGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkSeaGreenKey] = (object) darkSeaGreen;
        }
        return darkSeaGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkSlateBlue
    {
      get
      {
        Brush darkSlateBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkSlateBlueKey];
        if (darkSlateBlue == null)
        {
          darkSlateBlue = (Brush) new SolidBrush(Color.DarkSlateBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkSlateBlueKey] = (object) darkSlateBlue;
        }
        return darkSlateBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkSlateGray
    {
      get
      {
        Brush darkSlateGray = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkSlateGrayKey];
        if (darkSlateGray == null)
        {
          darkSlateGray = (Brush) new SolidBrush(Color.DarkSlateGray);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkSlateGrayKey] = (object) darkSlateGray;
        }
        return darkSlateGray;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkTurquoise
    {
      get
      {
        Brush darkTurquoise = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkTurquoiseKey];
        if (darkTurquoise == null)
        {
          darkTurquoise = (Brush) new SolidBrush(Color.DarkTurquoise);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkTurquoiseKey] = (object) darkTurquoise;
        }
        return darkTurquoise;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DarkViolet
    {
      get
      {
        Brush darkViolet = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DarkVioletKey];
        if (darkViolet == null)
        {
          darkViolet = (Brush) new SolidBrush(Color.DarkViolet);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DarkVioletKey] = (object) darkViolet;
        }
        return darkViolet;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DeepPink
    {
      get
      {
        Brush deepPink = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DeepPinkKey];
        if (deepPink == null)
        {
          deepPink = (Brush) new SolidBrush(Color.DeepPink);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DeepPinkKey] = (object) deepPink;
        }
        return deepPink;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DeepSkyBlue
    {
      get
      {
        Brush deepSkyBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DeepSkyBlueKey];
        if (deepSkyBlue == null)
        {
          deepSkyBlue = (Brush) new SolidBrush(Color.DeepSkyBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DeepSkyBlueKey] = (object) deepSkyBlue;
        }
        return deepSkyBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DimGray
    {
      get
      {
        Brush dimGray = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DimGrayKey];
        if (dimGray == null)
        {
          dimGray = (Brush) new SolidBrush(Color.DimGray);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DimGrayKey] = (object) dimGray;
        }
        return dimGray;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush DodgerBlue
    {
      get
      {
        Brush dodgerBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.DodgerBlueKey];
        if (dodgerBlue == null)
        {
          dodgerBlue = (Brush) new SolidBrush(Color.DodgerBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.DodgerBlueKey] = (object) dodgerBlue;
        }
        return dodgerBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Firebrick
    {
      get
      {
        Brush firebrick = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.FirebrickKey];
        if (firebrick == null)
        {
          firebrick = (Brush) new SolidBrush(Color.Firebrick);
          SafeNativeMethods.Gdip.ThreadData[Brushes.FirebrickKey] = (object) firebrick;
        }
        return firebrick;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush FloralWhite
    {
      get
      {
        Brush floralWhite = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.FloralWhiteKey];
        if (floralWhite == null)
        {
          floralWhite = (Brush) new SolidBrush(Color.FloralWhite);
          SafeNativeMethods.Gdip.ThreadData[Brushes.FloralWhiteKey] = (object) floralWhite;
        }
        return floralWhite;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush ForestGreen
    {
      get
      {
        Brush forestGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.ForestGreenKey];
        if (forestGreen == null)
        {
          forestGreen = (Brush) new SolidBrush(Color.ForestGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.ForestGreenKey] = (object) forestGreen;
        }
        return forestGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Fuchsia
    {
      get
      {
        Brush fuchsia = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.FuchiaKey];
        if (fuchsia == null)
        {
          fuchsia = (Brush) new SolidBrush(Color.Fuchsia);
          SafeNativeMethods.Gdip.ThreadData[Brushes.FuchiaKey] = (object) fuchsia;
        }
        return fuchsia;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Gainsboro
    {
      get
      {
        Brush gainsboro = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.GainsboroKey];
        if (gainsboro == null)
        {
          gainsboro = (Brush) new SolidBrush(Color.Gainsboro);
          SafeNativeMethods.Gdip.ThreadData[Brushes.GainsboroKey] = (object) gainsboro;
        }
        return gainsboro;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush GhostWhite
    {
      get
      {
        Brush ghostWhite = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.GhostWhiteKey];
        if (ghostWhite == null)
        {
          ghostWhite = (Brush) new SolidBrush(Color.GhostWhite);
          SafeNativeMethods.Gdip.ThreadData[Brushes.GhostWhiteKey] = (object) ghostWhite;
        }
        return ghostWhite;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Gold
    {
      get
      {
        Brush gold = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.GoldKey];
        if (gold == null)
        {
          gold = (Brush) new SolidBrush(Color.Gold);
          SafeNativeMethods.Gdip.ThreadData[Brushes.GoldKey] = (object) gold;
        }
        return gold;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Goldenrod
    {
      get
      {
        Brush goldenrod = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.GoldenrodKey];
        if (goldenrod == null)
        {
          goldenrod = (Brush) new SolidBrush(Color.Goldenrod);
          SafeNativeMethods.Gdip.ThreadData[Brushes.GoldenrodKey] = (object) goldenrod;
        }
        return goldenrod;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Gray
    {
      get
      {
        Brush gray = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.GrayKey];
        if (gray == null)
        {
          gray = (Brush) new SolidBrush(Color.Gray);
          SafeNativeMethods.Gdip.ThreadData[Brushes.GrayKey] = (object) gray;
        }
        return gray;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Green
    {
      get
      {
        Brush green = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.GreenKey];
        if (green == null)
        {
          green = (Brush) new SolidBrush(Color.Green);
          SafeNativeMethods.Gdip.ThreadData[Brushes.GreenKey] = (object) green;
        }
        return green;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush GreenYellow
    {
      get
      {
        Brush greenYellow = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.GreenYellowKey];
        if (greenYellow == null)
        {
          greenYellow = (Brush) new SolidBrush(Color.GreenYellow);
          SafeNativeMethods.Gdip.ThreadData[Brushes.GreenYellowKey] = (object) greenYellow;
        }
        return greenYellow;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Honeydew
    {
      get
      {
        Brush honeydew = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.HoneydewKey];
        if (honeydew == null)
        {
          honeydew = (Brush) new SolidBrush(Color.Honeydew);
          SafeNativeMethods.Gdip.ThreadData[Brushes.HoneydewKey] = (object) honeydew;
        }
        return honeydew;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush HotPink
    {
      get
      {
        Brush hotPink = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.HotPinkKey];
        if (hotPink == null)
        {
          hotPink = (Brush) new SolidBrush(Color.HotPink);
          SafeNativeMethods.Gdip.ThreadData[Brushes.HotPinkKey] = (object) hotPink;
        }
        return hotPink;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush IndianRed
    {
      get
      {
        Brush indianRed = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.IndianRedKey];
        if (indianRed == null)
        {
          indianRed = (Brush) new SolidBrush(Color.IndianRed);
          SafeNativeMethods.Gdip.ThreadData[Brushes.IndianRedKey] = (object) indianRed;
        }
        return indianRed;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Indigo
    {
      get
      {
        Brush indigo = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.IndigoKey];
        if (indigo == null)
        {
          indigo = (Brush) new SolidBrush(Color.Indigo);
          SafeNativeMethods.Gdip.ThreadData[Brushes.IndigoKey] = (object) indigo;
        }
        return indigo;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Ivory
    {
      get
      {
        Brush ivory = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.IvoryKey];
        if (ivory == null)
        {
          ivory = (Brush) new SolidBrush(Color.Ivory);
          SafeNativeMethods.Gdip.ThreadData[Brushes.IvoryKey] = (object) ivory;
        }
        return ivory;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Khaki
    {
      get
      {
        Brush khaki = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.KhakiKey];
        if (khaki == null)
        {
          khaki = (Brush) new SolidBrush(Color.Khaki);
          SafeNativeMethods.Gdip.ThreadData[Brushes.KhakiKey] = (object) khaki;
        }
        return khaki;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Lavender
    {
      get
      {
        Brush lavender = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LavenderKey];
        if (lavender == null)
        {
          lavender = (Brush) new SolidBrush(Color.Lavender);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LavenderKey] = (object) lavender;
        }
        return lavender;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LavenderBlush
    {
      get
      {
        Brush lavenderBlush = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LavenderBlushKey];
        if (lavenderBlush == null)
        {
          lavenderBlush = (Brush) new SolidBrush(Color.LavenderBlush);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LavenderBlushKey] = (object) lavenderBlush;
        }
        return lavenderBlush;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LawnGreen
    {
      get
      {
        Brush lawnGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LawnGreenKey];
        if (lawnGreen == null)
        {
          lawnGreen = (Brush) new SolidBrush(Color.LawnGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LawnGreenKey] = (object) lawnGreen;
        }
        return lawnGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LemonChiffon
    {
      get
      {
        Brush lemonChiffon = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LemonChiffonKey];
        if (lemonChiffon == null)
        {
          lemonChiffon = (Brush) new SolidBrush(Color.LemonChiffon);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LemonChiffonKey] = (object) lemonChiffon;
        }
        return lemonChiffon;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightBlue
    {
      get
      {
        Brush lightBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightBlueKey];
        if (lightBlue == null)
        {
          lightBlue = (Brush) new SolidBrush(Color.LightBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightBlueKey] = (object) lightBlue;
        }
        return lightBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightCoral
    {
      get
      {
        Brush lightCoral = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightCoralKey];
        if (lightCoral == null)
        {
          lightCoral = (Brush) new SolidBrush(Color.LightCoral);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightCoralKey] = (object) lightCoral;
        }
        return lightCoral;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightCyan
    {
      get
      {
        Brush lightCyan = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightCyanKey];
        if (lightCyan == null)
        {
          lightCyan = (Brush) new SolidBrush(Color.LightCyan);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightCyanKey] = (object) lightCyan;
        }
        return lightCyan;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightGoldenrodYellow
    {
      get
      {
        Brush lightGoldenrodYellow = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightGoldenrodYellowKey];
        if (lightGoldenrodYellow == null)
        {
          lightGoldenrodYellow = (Brush) new SolidBrush(Color.LightGoldenrodYellow);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightGoldenrodYellowKey] = (object) lightGoldenrodYellow;
        }
        return lightGoldenrodYellow;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightGreen
    {
      get
      {
        Brush lightGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightGreenKey];
        if (lightGreen == null)
        {
          lightGreen = (Brush) new SolidBrush(Color.LightGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightGreenKey] = (object) lightGreen;
        }
        return lightGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightGray
    {
      get
      {
        Brush lightGray = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightGrayKey];
        if (lightGray == null)
        {
          lightGray = (Brush) new SolidBrush(Color.LightGray);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightGrayKey] = (object) lightGray;
        }
        return lightGray;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightPink
    {
      get
      {
        Brush lightPink = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightPinkKey];
        if (lightPink == null)
        {
          lightPink = (Brush) new SolidBrush(Color.LightPink);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightPinkKey] = (object) lightPink;
        }
        return lightPink;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightSalmon
    {
      get
      {
        Brush lightSalmon = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightSalmonKey];
        if (lightSalmon == null)
        {
          lightSalmon = (Brush) new SolidBrush(Color.LightSalmon);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightSalmonKey] = (object) lightSalmon;
        }
        return lightSalmon;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightSeaGreen
    {
      get
      {
        Brush lightSeaGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightSeaGreenKey];
        if (lightSeaGreen == null)
        {
          lightSeaGreen = (Brush) new SolidBrush(Color.LightSeaGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightSeaGreenKey] = (object) lightSeaGreen;
        }
        return lightSeaGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightSkyBlue
    {
      get
      {
        Brush lightSkyBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightSkyBlueKey];
        if (lightSkyBlue == null)
        {
          lightSkyBlue = (Brush) new SolidBrush(Color.LightSkyBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightSkyBlueKey] = (object) lightSkyBlue;
        }
        return lightSkyBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightSlateGray
    {
      get
      {
        Brush lightSlateGray = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightSlateGrayKey];
        if (lightSlateGray == null)
        {
          lightSlateGray = (Brush) new SolidBrush(Color.LightSlateGray);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightSlateGrayKey] = (object) lightSlateGray;
        }
        return lightSlateGray;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightSteelBlue
    {
      get
      {
        Brush lightSteelBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightSteelBlueKey];
        if (lightSteelBlue == null)
        {
          lightSteelBlue = (Brush) new SolidBrush(Color.LightSteelBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightSteelBlueKey] = (object) lightSteelBlue;
        }
        return lightSteelBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LightYellow
    {
      get
      {
        Brush lightYellow = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LightYellowKey];
        if (lightYellow == null)
        {
          lightYellow = (Brush) new SolidBrush(Color.LightYellow);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LightYellowKey] = (object) lightYellow;
        }
        return lightYellow;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Lime
    {
      get
      {
        Brush lime = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LimeKey];
        if (lime == null)
        {
          lime = (Brush) new SolidBrush(Color.Lime);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LimeKey] = (object) lime;
        }
        return lime;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush LimeGreen
    {
      get
      {
        Brush limeGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LimeGreenKey];
        if (limeGreen == null)
        {
          limeGreen = (Brush) new SolidBrush(Color.LimeGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LimeGreenKey] = (object) limeGreen;
        }
        return limeGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Linen
    {
      get
      {
        Brush linen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.LinenKey];
        if (linen == null)
        {
          linen = (Brush) new SolidBrush(Color.Linen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.LinenKey] = (object) linen;
        }
        return linen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Magenta
    {
      get
      {
        Brush magenta = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MagentaKey];
        if (magenta == null)
        {
          magenta = (Brush) new SolidBrush(Color.Magenta);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MagentaKey] = (object) magenta;
        }
        return magenta;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Maroon
    {
      get
      {
        Brush maroon = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MaroonKey];
        if (maroon == null)
        {
          maroon = (Brush) new SolidBrush(Color.Maroon);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MaroonKey] = (object) maroon;
        }
        return maroon;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumAquamarine
    {
      get
      {
        Brush mediumAquamarine = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumAquamarineKey];
        if (mediumAquamarine == null)
        {
          mediumAquamarine = (Brush) new SolidBrush(Color.MediumAquamarine);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumAquamarineKey] = (object) mediumAquamarine;
        }
        return mediumAquamarine;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumBlue
    {
      get
      {
        Brush mediumBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumBlueKey];
        if (mediumBlue == null)
        {
          mediumBlue = (Brush) new SolidBrush(Color.MediumBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumBlueKey] = (object) mediumBlue;
        }
        return mediumBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumOrchid
    {
      get
      {
        Brush mediumOrchid = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumOrchidKey];
        if (mediumOrchid == null)
        {
          mediumOrchid = (Brush) new SolidBrush(Color.MediumOrchid);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumOrchidKey] = (object) mediumOrchid;
        }
        return mediumOrchid;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumPurple
    {
      get
      {
        Brush mediumPurple = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumPurpleKey];
        if (mediumPurple == null)
        {
          mediumPurple = (Brush) new SolidBrush(Color.MediumPurple);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumPurpleKey] = (object) mediumPurple;
        }
        return mediumPurple;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumSeaGreen
    {
      get
      {
        Brush mediumSeaGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumSeaGreenKey];
        if (mediumSeaGreen == null)
        {
          mediumSeaGreen = (Brush) new SolidBrush(Color.MediumSeaGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumSeaGreenKey] = (object) mediumSeaGreen;
        }
        return mediumSeaGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumSlateBlue
    {
      get
      {
        Brush mediumSlateBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumSlateBlueKey];
        if (mediumSlateBlue == null)
        {
          mediumSlateBlue = (Brush) new SolidBrush(Color.MediumSlateBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumSlateBlueKey] = (object) mediumSlateBlue;
        }
        return mediumSlateBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumSpringGreen
    {
      get
      {
        Brush mediumSpringGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumSpringGreenKey];
        if (mediumSpringGreen == null)
        {
          mediumSpringGreen = (Brush) new SolidBrush(Color.MediumSpringGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumSpringGreenKey] = (object) mediumSpringGreen;
        }
        return mediumSpringGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumTurquoise
    {
      get
      {
        Brush mediumTurquoise = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumTurquoiseKey];
        if (mediumTurquoise == null)
        {
          mediumTurquoise = (Brush) new SolidBrush(Color.MediumTurquoise);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumTurquoiseKey] = (object) mediumTurquoise;
        }
        return mediumTurquoise;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MediumVioletRed
    {
      get
      {
        Brush mediumVioletRed = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MediumVioletRedKey];
        if (mediumVioletRed == null)
        {
          mediumVioletRed = (Brush) new SolidBrush(Color.MediumVioletRed);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MediumVioletRedKey] = (object) mediumVioletRed;
        }
        return mediumVioletRed;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MidnightBlue
    {
      get
      {
        Brush midnightBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MidnightBlueKey];
        if (midnightBlue == null)
        {
          midnightBlue = (Brush) new SolidBrush(Color.MidnightBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MidnightBlueKey] = (object) midnightBlue;
        }
        return midnightBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MintCream
    {
      get
      {
        Brush mintCream = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MintCreamKey];
        if (mintCream == null)
        {
          mintCream = (Brush) new SolidBrush(Color.MintCream);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MintCreamKey] = (object) mintCream;
        }
        return mintCream;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush MistyRose
    {
      get
      {
        Brush mistyRose = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MistyRoseKey];
        if (mistyRose == null)
        {
          mistyRose = (Brush) new SolidBrush(Color.MistyRose);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MistyRoseKey] = (object) mistyRose;
        }
        return mistyRose;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Moccasin
    {
      get
      {
        Brush moccasin = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.MoccasinKey];
        if (moccasin == null)
        {
          moccasin = (Brush) new SolidBrush(Color.Moccasin);
          SafeNativeMethods.Gdip.ThreadData[Brushes.MoccasinKey] = (object) moccasin;
        }
        return moccasin;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush NavajoWhite
    {
      get
      {
        Brush navajoWhite = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.NavajoWhiteKey];
        if (navajoWhite == null)
        {
          navajoWhite = (Brush) new SolidBrush(Color.NavajoWhite);
          SafeNativeMethods.Gdip.ThreadData[Brushes.NavajoWhiteKey] = (object) navajoWhite;
        }
        return navajoWhite;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Navy
    {
      get
      {
        Brush navy = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.NavyKey];
        if (navy == null)
        {
          navy = (Brush) new SolidBrush(Color.Navy);
          SafeNativeMethods.Gdip.ThreadData[Brushes.NavyKey] = (object) navy;
        }
        return navy;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush OldLace
    {
      get
      {
        Brush oldLace = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.OldLaceKey];
        if (oldLace == null)
        {
          oldLace = (Brush) new SolidBrush(Color.OldLace);
          SafeNativeMethods.Gdip.ThreadData[Brushes.OldLaceKey] = (object) oldLace;
        }
        return oldLace;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Olive
    {
      get
      {
        Brush olive = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.OliveKey];
        if (olive == null)
        {
          olive = (Brush) new SolidBrush(Color.Olive);
          SafeNativeMethods.Gdip.ThreadData[Brushes.OliveKey] = (object) olive;
        }
        return olive;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush OliveDrab
    {
      get
      {
        Brush oliveDrab = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.OliveDrabKey];
        if (oliveDrab == null)
        {
          oliveDrab = (Brush) new SolidBrush(Color.OliveDrab);
          SafeNativeMethods.Gdip.ThreadData[Brushes.OliveDrabKey] = (object) oliveDrab;
        }
        return oliveDrab;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Orange
    {
      get
      {
        Brush orange = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.OrangeKey];
        if (orange == null)
        {
          orange = (Brush) new SolidBrush(Color.Orange);
          SafeNativeMethods.Gdip.ThreadData[Brushes.OrangeKey] = (object) orange;
        }
        return orange;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush OrangeRed
    {
      get
      {
        Brush orangeRed = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.OrangeRedKey];
        if (orangeRed == null)
        {
          orangeRed = (Brush) new SolidBrush(Color.OrangeRed);
          SafeNativeMethods.Gdip.ThreadData[Brushes.OrangeRedKey] = (object) orangeRed;
        }
        return orangeRed;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Orchid
    {
      get
      {
        Brush orchid = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.OrchidKey];
        if (orchid == null)
        {
          orchid = (Brush) new SolidBrush(Color.Orchid);
          SafeNativeMethods.Gdip.ThreadData[Brushes.OrchidKey] = (object) orchid;
        }
        return orchid;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush PaleGoldenrod
    {
      get
      {
        Brush paleGoldenrod = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PaleGoldenrodKey];
        if (paleGoldenrod == null)
        {
          paleGoldenrod = (Brush) new SolidBrush(Color.PaleGoldenrod);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PaleGoldenrodKey] = (object) paleGoldenrod;
        }
        return paleGoldenrod;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush PaleGreen
    {
      get
      {
        Brush paleGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PaleGreenKey];
        if (paleGreen == null)
        {
          paleGreen = (Brush) new SolidBrush(Color.PaleGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PaleGreenKey] = (object) paleGreen;
        }
        return paleGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush PaleTurquoise
    {
      get
      {
        Brush paleTurquoise = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PaleTurquoiseKey];
        if (paleTurquoise == null)
        {
          paleTurquoise = (Brush) new SolidBrush(Color.PaleTurquoise);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PaleTurquoiseKey] = (object) paleTurquoise;
        }
        return paleTurquoise;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush PaleVioletRed
    {
      get
      {
        Brush paleVioletRed = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PaleVioletRedKey];
        if (paleVioletRed == null)
        {
          paleVioletRed = (Brush) new SolidBrush(Color.PaleVioletRed);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PaleVioletRedKey] = (object) paleVioletRed;
        }
        return paleVioletRed;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush PapayaWhip
    {
      get
      {
        Brush papayaWhip = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PapayaWhipKey];
        if (papayaWhip == null)
        {
          papayaWhip = (Brush) new SolidBrush(Color.PapayaWhip);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PapayaWhipKey] = (object) papayaWhip;
        }
        return papayaWhip;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush PeachPuff
    {
      get
      {
        Brush peachPuff = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PeachPuffKey];
        if (peachPuff == null)
        {
          peachPuff = (Brush) new SolidBrush(Color.PeachPuff);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PeachPuffKey] = (object) peachPuff;
        }
        return peachPuff;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Peru
    {
      get
      {
        Brush peru = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PeruKey];
        if (peru == null)
        {
          peru = (Brush) new SolidBrush(Color.Peru);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PeruKey] = (object) peru;
        }
        return peru;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Pink
    {
      get
      {
        Brush pink = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PinkKey];
        if (pink == null)
        {
          pink = (Brush) new SolidBrush(Color.Pink);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PinkKey] = (object) pink;
        }
        return pink;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Plum
    {
      get
      {
        Brush plum = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PlumKey];
        if (plum == null)
        {
          plum = (Brush) new SolidBrush(Color.Plum);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PlumKey] = (object) plum;
        }
        return plum;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush PowderBlue
    {
      get
      {
        Brush powderBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PowderBlueKey];
        if (powderBlue == null)
        {
          powderBlue = (Brush) new SolidBrush(Color.PowderBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PowderBlueKey] = (object) powderBlue;
        }
        return powderBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Purple
    {
      get
      {
        Brush purple = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.PurpleKey];
        if (purple == null)
        {
          purple = (Brush) new SolidBrush(Color.Purple);
          SafeNativeMethods.Gdip.ThreadData[Brushes.PurpleKey] = (object) purple;
        }
        return purple;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Red
    {
      get
      {
        Brush red = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.RedKey];
        if (red == null)
        {
          red = (Brush) new SolidBrush(Color.Red);
          SafeNativeMethods.Gdip.ThreadData[Brushes.RedKey] = (object) red;
        }
        return red;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush RosyBrown
    {
      get
      {
        Brush rosyBrown = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.RosyBrownKey];
        if (rosyBrown == null)
        {
          rosyBrown = (Brush) new SolidBrush(Color.RosyBrown);
          SafeNativeMethods.Gdip.ThreadData[Brushes.RosyBrownKey] = (object) rosyBrown;
        }
        return rosyBrown;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush RoyalBlue
    {
      get
      {
        Brush royalBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.RoyalBlueKey];
        if (royalBlue == null)
        {
          royalBlue = (Brush) new SolidBrush(Color.RoyalBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.RoyalBlueKey] = (object) royalBlue;
        }
        return royalBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SaddleBrown
    {
      get
      {
        Brush saddleBrown = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SaddleBrownKey];
        if (saddleBrown == null)
        {
          saddleBrown = (Brush) new SolidBrush(Color.SaddleBrown);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SaddleBrownKey] = (object) saddleBrown;
        }
        return saddleBrown;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Salmon
    {
      get
      {
        Brush salmon = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SalmonKey];
        if (salmon == null)
        {
          salmon = (Brush) new SolidBrush(Color.Salmon);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SalmonKey] = (object) salmon;
        }
        return salmon;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SandyBrown
    {
      get
      {
        Brush sandyBrown = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SandyBrownKey];
        if (sandyBrown == null)
        {
          sandyBrown = (Brush) new SolidBrush(Color.SandyBrown);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SandyBrownKey] = (object) sandyBrown;
        }
        return sandyBrown;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SeaGreen
    {
      get
      {
        Brush seaGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SeaGreenKey];
        if (seaGreen == null)
        {
          seaGreen = (Brush) new SolidBrush(Color.SeaGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SeaGreenKey] = (object) seaGreen;
        }
        return seaGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SeaShell
    {
      get
      {
        Brush seaShell = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SeaShellKey];
        if (seaShell == null)
        {
          seaShell = (Brush) new SolidBrush(Color.SeaShell);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SeaShellKey] = (object) seaShell;
        }
        return seaShell;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Sienna
    {
      get
      {
        Brush sienna = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SiennaKey];
        if (sienna == null)
        {
          sienna = (Brush) new SolidBrush(Color.Sienna);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SiennaKey] = (object) sienna;
        }
        return sienna;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Silver
    {
      get
      {
        Brush silver = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SilverKey];
        if (silver == null)
        {
          silver = (Brush) new SolidBrush(Color.Silver);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SilverKey] = (object) silver;
        }
        return silver;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SkyBlue
    {
      get
      {
        Brush skyBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SkyBlueKey];
        if (skyBlue == null)
        {
          skyBlue = (Brush) new SolidBrush(Color.SkyBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SkyBlueKey] = (object) skyBlue;
        }
        return skyBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SlateBlue
    {
      get
      {
        Brush slateBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SlateBlueKey];
        if (slateBlue == null)
        {
          slateBlue = (Brush) new SolidBrush(Color.SlateBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SlateBlueKey] = (object) slateBlue;
        }
        return slateBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SlateGray
    {
      get
      {
        Brush slateGray = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SlateGrayKey];
        if (slateGray == null)
        {
          slateGray = (Brush) new SolidBrush(Color.SlateGray);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SlateGrayKey] = (object) slateGray;
        }
        return slateGray;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Snow
    {
      get
      {
        Brush snow = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SnowKey];
        if (snow == null)
        {
          snow = (Brush) new SolidBrush(Color.Snow);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SnowKey] = (object) snow;
        }
        return snow;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SpringGreen
    {
      get
      {
        Brush springGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SpringGreenKey];
        if (springGreen == null)
        {
          springGreen = (Brush) new SolidBrush(Color.SpringGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SpringGreenKey] = (object) springGreen;
        }
        return springGreen;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush SteelBlue
    {
      get
      {
        Brush steelBlue = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.SteelBlueKey];
        if (steelBlue == null)
        {
          steelBlue = (Brush) new SolidBrush(Color.SteelBlue);
          SafeNativeMethods.Gdip.ThreadData[Brushes.SteelBlueKey] = (object) steelBlue;
        }
        return steelBlue;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Tan
    {
      get
      {
        Brush tan = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.TanKey];
        if (tan == null)
        {
          tan = (Brush) new SolidBrush(Color.Tan);
          SafeNativeMethods.Gdip.ThreadData[Brushes.TanKey] = (object) tan;
        }
        return tan;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Teal
    {
      get
      {
        Brush teal = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.TealKey];
        if (teal == null)
        {
          teal = (Brush) new SolidBrush(Color.Teal);
          SafeNativeMethods.Gdip.ThreadData[Brushes.TealKey] = (object) teal;
        }
        return teal;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Thistle
    {
      get
      {
        Brush thistle = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.ThistleKey];
        if (thistle == null)
        {
          thistle = (Brush) new SolidBrush(Color.Thistle);
          SafeNativeMethods.Gdip.ThreadData[Brushes.ThistleKey] = (object) thistle;
        }
        return thistle;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Tomato
    {
      get
      {
        Brush tomato = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.TomatoKey];
        if (tomato == null)
        {
          tomato = (Brush) new SolidBrush(Color.Tomato);
          SafeNativeMethods.Gdip.ThreadData[Brushes.TomatoKey] = (object) tomato;
        }
        return tomato;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Turquoise
    {
      get
      {
        Brush turquoise = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.TurquoiseKey];
        if (turquoise == null)
        {
          turquoise = (Brush) new SolidBrush(Color.Turquoise);
          SafeNativeMethods.Gdip.ThreadData[Brushes.TurquoiseKey] = (object) turquoise;
        }
        return turquoise;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Violet
    {
      get
      {
        Brush violet = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.VioletKey];
        if (violet == null)
        {
          violet = (Brush) new SolidBrush(Color.Violet);
          SafeNativeMethods.Gdip.ThreadData[Brushes.VioletKey] = (object) violet;
        }
        return violet;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Wheat
    {
      get
      {
        Brush wheat = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.WheatKey];
        if (wheat == null)
        {
          wheat = (Brush) new SolidBrush(Color.Wheat);
          SafeNativeMethods.Gdip.ThreadData[Brushes.WheatKey] = (object) wheat;
        }
        return wheat;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush White
    {
      get
      {
        Brush white = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.WhiteKey];
        if (white == null)
        {
          white = (Brush) new SolidBrush(Color.White);
          SafeNativeMethods.Gdip.ThreadData[Brushes.WhiteKey] = (object) white;
        }
        return white;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush WhiteSmoke
    {
      get
      {
        Brush whiteSmoke = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.WhiteSmokeKey];
        if (whiteSmoke == null)
        {
          whiteSmoke = (Brush) new SolidBrush(Color.WhiteSmoke);
          SafeNativeMethods.Gdip.ThreadData[Brushes.WhiteSmokeKey] = (object) whiteSmoke;
        }
        return whiteSmoke;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush Yellow
    {
      get
      {
        Brush yellow = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.YellowKey];
        if (yellow == null)
        {
          yellow = (Brush) new SolidBrush(Color.Yellow);
          SafeNativeMethods.Gdip.ThreadData[Brushes.YellowKey] = (object) yellow;
        }
        return yellow;
      }
    }

    /// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
    /// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
    public static Brush YellowGreen
    {
      get
      {
        Brush yellowGreen = (Brush) SafeNativeMethods.Gdip.ThreadData[Brushes.YellowGreenKey];
        if (yellowGreen == null)
        {
          yellowGreen = (Brush) new SolidBrush(Color.YellowGreen);
          SafeNativeMethods.Gdip.ThreadData[Brushes.YellowGreenKey] = (object) yellowGreen;
        }
        return yellowGreen;
      }
    }
  }
}
