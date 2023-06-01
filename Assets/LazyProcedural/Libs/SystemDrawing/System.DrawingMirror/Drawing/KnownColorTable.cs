// Decompiled with JetBrains decompiler
// Type: System.Drawing.KnownColorTable
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using Microsoft.Win32;

namespace System.Drawing
{
  internal static class KnownColorTable
  {
    private static int[] colorTable;
    private static string[] colorNameTable;
    private const int AlphaShift = 24;
    private const int RedShift = 16;
    private const int GreenShift = 8;
    private const int BlueShift = 0;
    private const int Win32RedShift = 0;
    private const int Win32GreenShift = 8;
    private const int Win32BlueShift = 16;

    public static Color ArgbToKnownColor(int targetARGB)
    {
      KnownColorTable.EnsureColorTable();
      for (int color = 0; color < KnownColorTable.colorTable.Length; ++color)
      {
        if (KnownColorTable.colorTable[color] == targetARGB)
        {
          Color knownColor = Color.FromKnownColor((KnownColor) color);
          if (!knownColor.IsSystemColor)
            return knownColor;
        }
      }
      return Color.FromArgb(targetARGB);
    }

    private static void EnsureColorTable()
    {
      if (KnownColorTable.colorTable != null)
        return;
      KnownColorTable.InitColorTable();
    }

    private static void InitColorTable()
    {
      int[] colorTable = new int[175];
      SystemEvents.UserPreferenceChanging += new UserPreferenceChangingEventHandler(KnownColorTable.OnUserPreferenceChanging);
      KnownColorTable.UpdateSystemColors(colorTable);
      colorTable[27] = 16777215;
      colorTable[28] = -984833;
      colorTable[29] = -332841;
      colorTable[30] = -16711681;
      colorTable[31] = -8388652;
      colorTable[32] = -983041;
      colorTable[33] = -657956;
      colorTable[34] = -6972;
      colorTable[35] = -16777216;
      colorTable[36] = -5171;
      colorTable[37] = -16776961;
      colorTable[38] = -7722014;
      colorTable[39] = -5952982;
      colorTable[40] = -2180985;
      colorTable[41] = -10510688;
      colorTable[42] = -8388864;
      colorTable[43] = -2987746;
      colorTable[44] = -32944;
      colorTable[45] = -10185235;
      colorTable[46] = -1828;
      colorTable[47] = -2354116;
      colorTable[48] = -16711681;
      colorTable[49] = -16777077;
      colorTable[50] = -16741493;
      colorTable[51] = -4684277;
      colorTable[52] = -5658199;
      colorTable[53] = -16751616;
      colorTable[54] = -4343957;
      colorTable[55] = -7667573;
      colorTable[56] = -11179217;
      colorTable[57] = -29696;
      colorTable[58] = -6737204;
      colorTable[59] = -7667712;
      colorTable[60] = -1468806;
      colorTable[61] = -7357301;
      colorTable[62] = -12042869;
      colorTable[63] = -13676721;
      colorTable[64] = -16724271;
      colorTable[65] = -7077677;
      colorTable[66] = -60269;
      colorTable[67] = -16728065;
      colorTable[68] = -9868951;
      colorTable[69] = -14774017;
      colorTable[70] = -5103070;
      colorTable[71] = -1296;
      colorTable[72] = -14513374;
      colorTable[73] = -65281;
      colorTable[74] = -2302756;
      colorTable[75] = -460545;
      colorTable[76] = -10496;
      colorTable[77] = -2448096;
      colorTable[78] = -8355712;
      colorTable[79] = -16744448;
      colorTable[80] = -5374161;
      colorTable[81] = -983056;
      colorTable[82] = -38476;
      colorTable[83] = -3318692;
      colorTable[84] = -11861886;
      colorTable[85] = -16;
      colorTable[86] = -989556;
      colorTable[87] = -1644806;
      colorTable[88] = -3851;
      colorTable[89] = -8586240;
      colorTable[90] = -1331;
      colorTable[91] = -5383962;
      colorTable[92] = -1015680;
      colorTable[93] = -2031617;
      colorTable[94] = -329006;
      colorTable[95] = -2894893;
      colorTable[96] = -7278960;
      colorTable[97] = -18751;
      colorTable[98] = -24454;
      colorTable[99] = -14634326;
      colorTable[100] = -7876870;
      colorTable[101] = -8943463;
      colorTable[102] = -5192482;
      colorTable[103] = -32;
      colorTable[104] = -16711936;
      colorTable[105] = -13447886;
      colorTable[106] = -331546;
      colorTable[107] = -65281;
      colorTable[108] = -8388608;
      colorTable[109] = -10039894;
      colorTable[110] = -16777011;
      colorTable[111] = -4565549;
      colorTable[112] = -7114533;
      colorTable[113] = -12799119;
      colorTable[114] = -8689426;
      colorTable[115] = -16713062;
      colorTable[116] = -12004916;
      colorTable[117] = -3730043;
      colorTable[118] = -15132304;
      colorTable[119] = -655366;
      colorTable[120] = -6943;
      colorTable[121] = -6987;
      colorTable[122] = -8531;
      colorTable[123] = -16777088;
      colorTable[124] = -133658;
      colorTable[125] = -8355840;
      colorTable[126] = -9728477;
      colorTable[(int) sbyte.MaxValue] = -23296;
      colorTable[128] = -47872;
      colorTable[129] = -2461482;
      colorTable[130] = -1120086;
      colorTable[131] = -6751336;
      colorTable[132] = -5247250;
      colorTable[133] = -2396013;
      colorTable[134] = -4139;
      colorTable[135] = -9543;
      colorTable[136] = -3308225;
      colorTable[137] = -16181;
      colorTable[138] = -2252579;
      colorTable[139] = -5185306;
      colorTable[140] = -8388480;
      colorTable[141] = -65536;
      colorTable[142] = -4419697;
      colorTable[143] = -12490271;
      colorTable[144] = -7650029;
      colorTable[145] = -360334;
      colorTable[146] = -744352;
      colorTable[147] = -13726889;
      colorTable[148] = -2578;
      colorTable[149] = -6270419;
      colorTable[150] = -4144960;
      colorTable[151] = -7876885;
      colorTable[152] = -9807155;
      colorTable[153] = -9404272;
      colorTable[154] = -1286;
      colorTable[155] = -16711809;
      colorTable[156] = -12156236;
      colorTable[157] = -2968436;
      colorTable[158] = -16744320;
      colorTable[159] = -2572328;
      colorTable[160] = -40121;
      colorTable[161] = -12525360;
      colorTable[162] = -1146130;
      colorTable[163] = -663885;
      colorTable[164] = -1;
      colorTable[165] = -657931;
      colorTable[166] = -256;
      colorTable[167] = -6632142;
      KnownColorTable.colorTable = colorTable;
    }

    private static void EnsureColorNameTable()
    {
      if (KnownColorTable.colorNameTable != null)
        return;
      KnownColorTable.InitColorNameTable();
    }

    private static void InitColorNameTable()
    {
      string[] strArray = new string[175];
      strArray[1] = "ActiveBorder";
      strArray[2] = "ActiveCaption";
      strArray[3] = "ActiveCaptionText";
      strArray[4] = "AppWorkspace";
      strArray[168] = "ButtonFace";
      strArray[169] = "ButtonHighlight";
      strArray[170] = "ButtonShadow";
      strArray[5] = "Control";
      strArray[6] = "ControlDark";
      strArray[7] = "ControlDarkDark";
      strArray[8] = "ControlLight";
      strArray[9] = "ControlLightLight";
      strArray[10] = "ControlText";
      strArray[11] = "Desktop";
      strArray[171] = "GradientActiveCaption";
      strArray[172] = "GradientInactiveCaption";
      strArray[12] = "GrayText";
      strArray[13] = "Highlight";
      strArray[14] = "HighlightText";
      strArray[15] = "HotTrack";
      strArray[16] = "InactiveBorder";
      strArray[17] = "InactiveCaption";
      strArray[18] = "InactiveCaptionText";
      strArray[19] = "Info";
      strArray[20] = "InfoText";
      strArray[21] = "Menu";
      strArray[173] = "MenuBar";
      strArray[174] = "MenuHighlight";
      strArray[22] = "MenuText";
      strArray[23] = "ScrollBar";
      strArray[24] = "Window";
      strArray[25] = "WindowFrame";
      strArray[26] = "WindowText";
      strArray[27] = "Transparent";
      strArray[28] = "AliceBlue";
      strArray[29] = "AntiqueWhite";
      strArray[30] = "Aqua";
      strArray[31] = "Aquamarine";
      strArray[32] = "Azure";
      strArray[33] = "Beige";
      strArray[34] = "Bisque";
      strArray[35] = "Black";
      strArray[36] = "BlanchedAlmond";
      strArray[37] = "Blue";
      strArray[38] = "BlueViolet";
      strArray[39] = "Brown";
      strArray[40] = "BurlyWood";
      strArray[41] = "CadetBlue";
      strArray[42] = "Chartreuse";
      strArray[43] = "Chocolate";
      strArray[44] = "Coral";
      strArray[45] = "CornflowerBlue";
      strArray[46] = "Cornsilk";
      strArray[47] = "Crimson";
      strArray[48] = "Cyan";
      strArray[49] = "DarkBlue";
      strArray[50] = "DarkCyan";
      strArray[51] = "DarkGoldenrod";
      strArray[52] = "DarkGray";
      strArray[53] = "DarkGreen";
      strArray[54] = "DarkKhaki";
      strArray[55] = "DarkMagenta";
      strArray[56] = "DarkOliveGreen";
      strArray[57] = "DarkOrange";
      strArray[58] = "DarkOrchid";
      strArray[59] = "DarkRed";
      strArray[60] = "DarkSalmon";
      strArray[61] = "DarkSeaGreen";
      strArray[62] = "DarkSlateBlue";
      strArray[63] = "DarkSlateGray";
      strArray[64] = "DarkTurquoise";
      strArray[65] = "DarkViolet";
      strArray[66] = "DeepPink";
      strArray[67] = "DeepSkyBlue";
      strArray[68] = "DimGray";
      strArray[69] = "DodgerBlue";
      strArray[70] = "Firebrick";
      strArray[71] = "FloralWhite";
      strArray[72] = "ForestGreen";
      strArray[73] = "Fuchsia";
      strArray[74] = "Gainsboro";
      strArray[75] = "GhostWhite";
      strArray[76] = "Gold";
      strArray[77] = "Goldenrod";
      strArray[78] = "Gray";
      strArray[79] = "Green";
      strArray[80] = "GreenYellow";
      strArray[81] = "Honeydew";
      strArray[82] = "HotPink";
      strArray[83] = "IndianRed";
      strArray[84] = "Indigo";
      strArray[85] = "Ivory";
      strArray[86] = "Khaki";
      strArray[87] = "Lavender";
      strArray[88] = "LavenderBlush";
      strArray[89] = "LawnGreen";
      strArray[90] = "LemonChiffon";
      strArray[91] = "LightBlue";
      strArray[92] = "LightCoral";
      strArray[93] = "LightCyan";
      strArray[94] = "LightGoldenrodYellow";
      strArray[95] = "LightGray";
      strArray[96] = "LightGreen";
      strArray[97] = "LightPink";
      strArray[98] = "LightSalmon";
      strArray[99] = "LightSeaGreen";
      strArray[100] = "LightSkyBlue";
      strArray[101] = "LightSlateGray";
      strArray[102] = "LightSteelBlue";
      strArray[103] = "LightYellow";
      strArray[104] = "Lime";
      strArray[105] = "LimeGreen";
      strArray[106] = "Linen";
      strArray[107] = "Magenta";
      strArray[108] = "Maroon";
      strArray[109] = "MediumAquamarine";
      strArray[110] = "MediumBlue";
      strArray[111] = "MediumOrchid";
      strArray[112] = "MediumPurple";
      strArray[113] = "MediumSeaGreen";
      strArray[114] = "MediumSlateBlue";
      strArray[115] = "MediumSpringGreen";
      strArray[116] = "MediumTurquoise";
      strArray[117] = "MediumVioletRed";
      strArray[118] = "MidnightBlue";
      strArray[119] = "MintCream";
      strArray[120] = "MistyRose";
      strArray[121] = "Moccasin";
      strArray[122] = "NavajoWhite";
      strArray[123] = "Navy";
      strArray[124] = "OldLace";
      strArray[125] = "Olive";
      strArray[126] = "OliveDrab";
      strArray[(int) sbyte.MaxValue] = "Orange";
      strArray[128] = "OrangeRed";
      strArray[129] = "Orchid";
      strArray[130] = "PaleGoldenrod";
      strArray[131] = "PaleGreen";
      strArray[132] = "PaleTurquoise";
      strArray[133] = "PaleVioletRed";
      strArray[134] = "PapayaWhip";
      strArray[135] = "PeachPuff";
      strArray[136] = "Peru";
      strArray[137] = "Pink";
      strArray[138] = "Plum";
      strArray[139] = "PowderBlue";
      strArray[140] = "Purple";
      strArray[141] = "Red";
      strArray[142] = "RosyBrown";
      strArray[143] = "RoyalBlue";
      strArray[144] = "SaddleBrown";
      strArray[145] = "Salmon";
      strArray[146] = "SandyBrown";
      strArray[147] = "SeaGreen";
      strArray[148] = "SeaShell";
      strArray[149] = "Sienna";
      strArray[150] = "Silver";
      strArray[151] = "SkyBlue";
      strArray[152] = "SlateBlue";
      strArray[153] = "SlateGray";
      strArray[154] = "Snow";
      strArray[155] = "SpringGreen";
      strArray[156] = "SteelBlue";
      strArray[157] = "Tan";
      strArray[158] = "Teal";
      strArray[159] = "Thistle";
      strArray[160] = "Tomato";
      strArray[161] = "Turquoise";
      strArray[162] = "Violet";
      strArray[163] = "Wheat";
      strArray[164] = "White";
      strArray[165] = "WhiteSmoke";
      strArray[166] = "Yellow";
      strArray[167] = "YellowGreen";
      KnownColorTable.colorNameTable = strArray;
    }

    public static int KnownColorToArgb(KnownColor color)
    {
      KnownColorTable.EnsureColorTable();
      return color <= KnownColor.MenuHighlight ? KnownColorTable.colorTable[(int) color] : 0;
    }

    public static string KnownColorToName(KnownColor color)
    {
      KnownColorTable.EnsureColorNameTable();
      return color <= KnownColor.MenuHighlight ? KnownColorTable.colorNameTable[(int) color] : (string) null;
    }

    private static int SystemColorToArgb(int index) => KnownColorTable.FromWin32Value(SafeNativeMethods.GetSysColor(index));

    private static int Encode(int alpha, int red, int green, int blue) => red << 16 | green << 8 | blue | alpha << 24;

    private static int FromWin32Value(int value) => KnownColorTable.Encode((int) byte.MaxValue, value & (int) byte.MaxValue, value >> 8 & (int) byte.MaxValue, value >> 16 & (int) byte.MaxValue);

    private static void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
    {
      if (e.Category != UserPreferenceCategory.Color || KnownColorTable.colorTable == null)
        return;
      KnownColorTable.UpdateSystemColors(KnownColorTable.colorTable);
    }

    private static void UpdateSystemColors(int[] colorTable)
    {
      colorTable[1] = KnownColorTable.SystemColorToArgb(10);
      colorTable[2] = KnownColorTable.SystemColorToArgb(2);
      colorTable[3] = KnownColorTable.SystemColorToArgb(9);
      colorTable[4] = KnownColorTable.SystemColorToArgb(12);
      colorTable[168] = KnownColorTable.SystemColorToArgb(15);
      colorTable[169] = KnownColorTable.SystemColorToArgb(20);
      colorTable[170] = KnownColorTable.SystemColorToArgb(16);
      colorTable[5] = KnownColorTable.SystemColorToArgb(15);
      colorTable[6] = KnownColorTable.SystemColorToArgb(16);
      colorTable[7] = KnownColorTable.SystemColorToArgb(21);
      colorTable[8] = KnownColorTable.SystemColorToArgb(22);
      colorTable[9] = KnownColorTable.SystemColorToArgb(20);
      colorTable[10] = KnownColorTable.SystemColorToArgb(18);
      colorTable[11] = KnownColorTable.SystemColorToArgb(1);
      colorTable[171] = KnownColorTable.SystemColorToArgb(27);
      colorTable[172] = KnownColorTable.SystemColorToArgb(28);
      colorTable[12] = KnownColorTable.SystemColorToArgb(17);
      colorTable[13] = KnownColorTable.SystemColorToArgb(13);
      colorTable[14] = KnownColorTable.SystemColorToArgb(14);
      colorTable[15] = KnownColorTable.SystemColorToArgb(26);
      colorTable[16] = KnownColorTable.SystemColorToArgb(11);
      colorTable[17] = KnownColorTable.SystemColorToArgb(3);
      colorTable[18] = KnownColorTable.SystemColorToArgb(19);
      colorTable[19] = KnownColorTable.SystemColorToArgb(24);
      colorTable[20] = KnownColorTable.SystemColorToArgb(23);
      colorTable[21] = KnownColorTable.SystemColorToArgb(4);
      colorTable[173] = KnownColorTable.SystemColorToArgb(30);
      colorTable[174] = KnownColorTable.SystemColorToArgb(29);
      colorTable[22] = KnownColorTable.SystemColorToArgb(7);
      colorTable[23] = KnownColorTable.SystemColorToArgb(0);
      colorTable[24] = KnownColorTable.SystemColorToArgb(5);
      colorTable[25] = KnownColorTable.SystemColorToArgb(6);
      colorTable[26] = KnownColorTable.SystemColorToArgb(8);
    }
  }
}
