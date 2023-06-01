﻿// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.DeviceContexts
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Internal
{
  internal static class DeviceContexts
  {
    [ThreadStatic]
    private static System.Drawing.ClientUtils.WeakRefCollection activeDeviceContexts;

    internal static void AddDeviceContext(DeviceContext dc)
    {
      if (DeviceContexts.activeDeviceContexts == null)
      {
        DeviceContexts.activeDeviceContexts = new System.Drawing.ClientUtils.WeakRefCollection();
        DeviceContexts.activeDeviceContexts.RefCheckThreshold = 20;
      }
      if (DeviceContexts.activeDeviceContexts.Contains((object) dc))
        return;
      dc.Disposing += new EventHandler(DeviceContexts.OnDcDisposing);
      DeviceContexts.activeDeviceContexts.Add((object) dc);
    }

    private static void OnDcDisposing(object sender, EventArgs e)
    {
      if (!(sender is DeviceContext dc))
        return;
      dc.Disposing -= new EventHandler(DeviceContexts.OnDcDisposing);
      DeviceContexts.RemoveDeviceContext(dc);
    }

    internal static void RemoveDeviceContext(DeviceContext dc)
    {
      if (DeviceContexts.activeDeviceContexts == null)
        return;
      DeviceContexts.activeDeviceContexts.RemoveByHashCode((object) dc);
    }
  }
}
