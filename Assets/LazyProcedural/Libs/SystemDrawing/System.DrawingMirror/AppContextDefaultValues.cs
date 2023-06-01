// Decompiled with JetBrains decompiler
// Type: System.AppContextDefaultValues
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System
{
  internal static class AppContextDefaultValues
  {
    public static void PopulateDefaultValues()
    {
      string identifier;
      string profile;
      int version;
      AppContextDefaultValues.ParseTargetFrameworkName(out identifier, out profile, out version);
      AppContextDefaultValues.PopulateDefaultValuesPartial(identifier, profile, version);
    }

    private static void ParseTargetFrameworkName(
      out string identifier,
      out string profile,
      out int version)
    {
      if (AppContextDefaultValues.TryParseFrameworkName(AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName, out identifier, out version, out profile))
        return;
      identifier = ".NETFramework";
      version = 40000;
      profile = string.Empty;
    }

    private static bool TryParseFrameworkName(
      string frameworkName,
      out string identifier,
      out int version,
      out string profile)
    {
      identifier = profile = string.Empty;
      version = 0;
      if (frameworkName == null || frameworkName.Length == 0)
        return false;
      string[] strArray1 = frameworkName.Split(',');
      version = 0;
      if (strArray1.Length < 2 || strArray1.Length > 3)
        return false;
      identifier = strArray1[0].Trim();
      if (identifier.Length == 0)
        return false;
      bool frameworkName1 = false;
      profile = (string) null;
      for (int index = 1; index < strArray1.Length; ++index)
      {
        string[] strArray2 = strArray1[index].Split('=');
        if (strArray2.Length != 2)
          return false;
        string str = strArray2[0].Trim();
        string version1 = strArray2[1].Trim();
        if (str.Equals("Version", StringComparison.OrdinalIgnoreCase))
        {
          frameworkName1 = true;
          if (version1.Length > 0 && (version1[0] == 'v' || version1[0] == 'V'))
            version1 = version1.Substring(1);
          Version version2 = new Version(version1);
          version = version2.Major * 10000;
          if (version2.Minor > 0)
            version += version2.Minor * 100;
          if (version2.Build > 0)
            version += version2.Build;
        }
        else
        {
          if (!str.Equals("Profile", StringComparison.OrdinalIgnoreCase))
            return false;
          if (!string.IsNullOrEmpty(version1))
            profile = version1;
        }
      }
      return frameworkName1;
    }

    private static void PopulateDefaultValuesPartial(
      string platformIdentifier,
      string profile,
      int version)
    {
      if (!(platformIdentifier == ".NETCore") && !(platformIdentifier == ".NETFramework"))
        return;
      if (version <= 40502)
        LocalAppContext.DefineSwitchDefault("Switch.System.Drawing.DontSupportPngFramesInIcons", true);
      if (version > 40701)
        return;
      LocalAppContext.DefineSwitchDefault("Switch.System.Drawing.Text.DoNotRemoveGdiFontsResourcesFromFontCollection", true);
    }
  }
}
