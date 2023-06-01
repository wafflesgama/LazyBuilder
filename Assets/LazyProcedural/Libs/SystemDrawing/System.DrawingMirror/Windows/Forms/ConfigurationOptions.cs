// Decompiled with JetBrains decompiler
// Type: System.Windows.Forms.ConfigurationOptions
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.Versioning;

namespace System.Windows.Forms
{
  internal static class ConfigurationOptions
  {
    private static NameValueCollection applicationConfigOptions = (NameValueCollection) null;
    private static Version netFrameworkVersion = (Version) null;
    private static readonly Version featureSupportedMinimumFrameworkVersion = new Version(4, 7);
    internal static Version OSVersion = Environment.OSVersion.Version;
    internal static readonly Version RS2Version = new Version(10, 0, 14933, 0);

    static ConfigurationOptions() => ConfigurationOptions.PopulateWinformsSection();

    private static void PopulateWinformsSection()
    {
      if (ConfigurationOptions.NetFrameworkVersion.CompareTo(ConfigurationOptions.featureSupportedMinimumFrameworkVersion) < 0)
        return;
      try
      {
        ConfigurationOptions.applicationConfigOptions = ConfigurationManager.GetSection("System.Windows.Forms.ApplicationConfigurationSection") as NameValueCollection;
      }
      catch (Exception ex)
      {
      }
    }

    public static Version NetFrameworkVersion
    {
      get
      {
        if (ConfigurationOptions.netFrameworkVersion == (Version) null)
        {
          ConfigurationOptions.netFrameworkVersion = new Version(0, 0, 0, 0);
          try
          {
            string targetFrameworkName = AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
            if (!string.IsNullOrEmpty(targetFrameworkName))
            {
              FrameworkName frameworkName = new FrameworkName(targetFrameworkName);
              if (string.Equals(frameworkName.Identifier, ".NETFramework"))
                ConfigurationOptions.netFrameworkVersion = frameworkName.Version;
            }
          }
          catch (Exception ex)
          {
          }
        }
        return ConfigurationOptions.netFrameworkVersion;
      }
    }

    public static string GetConfigSettingValue(string settingName) => ConfigurationOptions.applicationConfigOptions != null && !string.IsNullOrEmpty(settingName) ? ConfigurationOptions.applicationConfigOptions.Get(settingName) : (string) null;
  }
}
