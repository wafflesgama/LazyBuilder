// Decompiled with JetBrains decompiler
// Type: System.Drawing.LocalAppContextSwitches
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.CompilerServices;

namespace System.Drawing
{
  internal static class LocalAppContextSwitches
  {
    private static int dontSupportPngFramesInIcons;
    private static int optimizePrintPreview;
    private static int doNotRemoveGdiFontsResourcesFromFontCollection;
    private static int freeCopyToDevModeOnFailure;

    public static bool DontSupportPngFramesInIcons
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => LocalAppContext.GetCachedSwitchValue("Switch.System.Drawing.DontSupportPngFramesInIcons", ref LocalAppContextSwitches.dontSupportPngFramesInIcons);
    }

    public static bool OptimizePrintPreview
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => LocalAppContext.GetCachedSwitchValue("Switch.System.Drawing.Printing.OptimizePrintPreview", ref LocalAppContextSwitches.optimizePrintPreview);
    }

    public static bool DoNotRemoveGdiFontsResourcesFromFontCollection
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => LocalAppContext.GetCachedSwitchValue("Switch.System.Drawing.Text.DoNotRemoveGdiFontsResourcesFromFontCollection", ref LocalAppContextSwitches.doNotRemoveGdiFontsResourcesFromFontCollection);
    }

    public static bool FreeCopyToDevModeOnFailure
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => LocalAppContext.GetCachedSwitchValue("Switch.System.Drawing.Printing.CopyToDevModeFreeOnFailure", ref LocalAppContextSwitches.freeCopyToDevModeOnFailure);
    }
  }
}
