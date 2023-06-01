// Decompiled with JetBrains decompiler
// Type: System.Drawing.Configuration.SystemDrawingSection
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Configuration;

namespace System.Drawing.Configuration
{
  /// <summary>Represents the configuration section used by classes in the <see cref="N:System.Drawing" /> namespace.</summary>
  public sealed class SystemDrawingSection : ConfigurationSection
  {
    private const string BitmapSuffixSectionName = "bitmapSuffix";
    private static readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
    private static readonly ConfigurationProperty bitmapSuffix = new ConfigurationProperty(nameof (bitmapSuffix), typeof (string), (object) null, ConfigurationPropertyOptions.None);

    static SystemDrawingSection() => SystemDrawingSection.properties.Add(SystemDrawingSection.bitmapSuffix);

    /// <summary>Gets or sets the suffix to append to a file name indicated by a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> when an assembly is declared with a <see cref="T:System.Drawing.BitmapSuffixInSameAssemblyAttribute" /> or a <see cref="T:System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute" />.</summary>
    /// <returns>The bitmap suffix.</returns>
    [ConfigurationProperty("bitmapSuffix")]
    public string BitmapSuffix
    {
      get => (string) this[SystemDrawingSection.bitmapSuffix];
      set => this[SystemDrawingSection.bitmapSuffix] = (object) value;
    }

    protected override ConfigurationPropertyCollection Properties => SystemDrawingSection.properties;
  }
}
