// Decompiled with JetBrains decompiler
// Type: System.ComponentModel.CompModSwitches
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Diagnostics;

namespace System.ComponentModel
{
  internal static class CompModSwitches
  {
    private static TraceSwitch handleLeak;
    private static BooleanSwitch traceCollect;

    public static TraceSwitch HandleLeak
    {
      get
      {
        if (CompModSwitches.handleLeak == null)
          CompModSwitches.handleLeak = new TraceSwitch("HANDLELEAK", "HandleCollector: Track Win32 Handle Leaks");
        return CompModSwitches.handleLeak;
      }
    }

    public static BooleanSwitch TraceCollect
    {
      get
      {
        if (CompModSwitches.traceCollect == null)
          CompModSwitches.traceCollect = new BooleanSwitch("TRACECOLLECT", "HandleCollector: Trace HandleCollector operations");
        return CompModSwitches.traceCollect;
      }
    }
  }
}
