// Decompiled with JetBrains decompiler
// Type: System.Drawing.BitmapSelector
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Configuration;
using System.Drawing.Configuration;
using System.IO;
using System.Reflection;

namespace System.Drawing
{
  internal static class BitmapSelector
  {
    private static string _suffix;

    internal static string Suffix
    {
      get
      {
        if (BitmapSelector._suffix == null)
        {
          BitmapSelector._suffix = string.Empty;
          if (ConfigurationManager.GetSection("system.drawing") is SystemDrawingSection section)
          {
            string bitmapSuffix = section.BitmapSuffix;
            if (bitmapSuffix != null && bitmapSuffix != null)
              BitmapSelector._suffix = bitmapSuffix;
          }
        }
        return BitmapSelector._suffix;
      }
      set => BitmapSelector._suffix = value;
    }

    internal static string AppendSuffix(string filePath)
    {
      try
      {
        return Path.ChangeExtension(filePath, BitmapSelector.Suffix + Path.GetExtension(filePath));
      }
      catch (ArgumentException ex)
      {
        return filePath;
      }
    }

    public static string GetFileName(string originalPath)
    {
      if (BitmapSelector.Suffix == string.Empty)
        return originalPath;
      string path = BitmapSelector.AppendSuffix(originalPath);
      return !File.Exists(path) ? originalPath : path;
    }

    private static Stream GetResourceStreamHelper(Assembly assembly, Type type, string name)
    {
      Stream resourceStreamHelper = (Stream) null;
      try
      {
        resourceStreamHelper = assembly.GetManifestResourceStream(type, name);
      }
      catch (FileNotFoundException ex)
      {
      }
      return resourceStreamHelper;
    }

    private static bool DoesAssemblyHaveCustomAttribute(Assembly assembly, string typeName) => BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, assembly.GetType(typeName));

    private static bool DoesAssemblyHaveCustomAttribute(Assembly assembly, Type attrType) => attrType != (Type) null && assembly.GetCustomAttributes(attrType, false).Length != 0;

    internal static bool SatelliteAssemblyOptIn(Assembly assembly) => BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, typeof (BitmapSuffixInSatelliteAssemblyAttribute)) || BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, "System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute");

    internal static bool SameAssemblyOptIn(Assembly assembly) => BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, typeof (BitmapSuffixInSameAssemblyAttribute)) || BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, "System.Drawing.BitmapSuffixInSameAssemblyAttribute");

    public static Stream GetResourceStream(Assembly assembly, Type type, string originalName)
    {
      if (BitmapSelector.Suffix != string.Empty)
      {
        try
        {
          if (BitmapSelector.SameAssemblyOptIn(assembly))
          {
            string name = BitmapSelector.AppendSuffix(originalName);
            Stream resourceStreamHelper = BitmapSelector.GetResourceStreamHelper(assembly, type, name);
            if (resourceStreamHelper != null)
              return resourceStreamHelper;
          }
        }
        catch
        {
        }
        try
        {
          if (BitmapSelector.SatelliteAssemblyOptIn(assembly))
          {
            AssemblyName name = assembly.GetName();
            name.Name += BitmapSelector.Suffix;
            name.ProcessorArchitecture = ProcessorArchitecture.None;
            Assembly assembly1 = Assembly.Load(name);
            if (assembly1 != (Assembly) null)
            {
              Stream resourceStreamHelper = BitmapSelector.GetResourceStreamHelper(assembly1, type, originalName);
              if (resourceStreamHelper != null)
                return resourceStreamHelper;
            }
          }
        }
        catch
        {
        }
      }
      return assembly.GetManifestResourceStream(type, originalName);
    }

    public static Stream GetResourceStream(Type type, string originalName) => BitmapSelector.GetResourceStream(type.Module.Assembly, type, originalName);

    public static Icon CreateIcon(Type type, string originalName) => new Icon(BitmapSelector.GetResourceStream(type, originalName));

    public static Bitmap CreateBitmap(Type type, string originalName) => new Bitmap(BitmapSelector.GetResourceStream(type, originalName));
  }
}
