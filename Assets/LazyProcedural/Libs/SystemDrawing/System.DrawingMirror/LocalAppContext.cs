// Decompiled with JetBrains decompiler
// Type: System.LocalAppContext
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
  internal static class LocalAppContext
  {
    private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;
    private static bool s_canForwardCalls;
    private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();
    private static readonly object s_syncLock = new object();

    private static bool DisableCaching { get; set; }

    static LocalAppContext()
    {
      LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
      AppContextDefaultValues.PopulateDefaultValues();
      LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
    }

    public static bool IsSwitchEnabled(string switchName)
    {
      bool flag;
      return LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out flag) ? flag : LocalAppContext.IsSwitchEnabledLocal(switchName);
    }

    private static bool IsSwitchEnabledLocal(string switchName)
    {
      bool flag1;
      bool flag2;
      lock (LocalAppContext.s_switchMap)
        flag2 = LocalAppContext.s_switchMap.TryGetValue(switchName, out flag1);
      return flag2 && flag1;
    }

    private static bool SetupDelegate()
    {
      Type type = typeof (object).Assembly.GetType("System.AppContext");
      if (type == (Type) null)
        return false;
      MethodInfo method = type.GetMethod("TryGetSwitch", BindingFlags.Static | BindingFlags.Public, (Binder) null, new Type[2]
      {
        typeof (string),
        typeof (bool).MakeByRefType()
      }, (ParameterModifier[]) null);
      if (method == (MethodInfo) null)
        return false;
      LocalAppContext.TryGetSwitchFromCentralAppContext = (LocalAppContext.TryGetSwitchDelegate) Delegate.CreateDelegate(typeof (LocalAppContext.TryGetSwitchDelegate), method);
      return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
    {
      if (switchValue < 0)
        return false;
      return switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue);
    }

    private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
    {
      if (LocalAppContext.DisableCaching)
        return LocalAppContext.IsSwitchEnabled(switchName);
      bool switchValueInternal = LocalAppContext.IsSwitchEnabled(switchName);
      switchValue = switchValueInternal ? 1 : -1;
      return switchValueInternal;
    }

    internal static void DefineSwitchDefault(string switchName, bool initialValue) => LocalAppContext.s_switchMap[switchName] = initialValue;

    private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
  }
}
