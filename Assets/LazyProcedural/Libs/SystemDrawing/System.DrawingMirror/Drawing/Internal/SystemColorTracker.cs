// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.SystemColorTracker
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using Microsoft.Win32;

namespace System.Drawing.Internal
{
  internal class SystemColorTracker
  {
    private static int INITIAL_SIZE = 200;
    private static int WARNING_SIZE = 100000;
    private static float EXPAND_THRESHOLD = 0.75f;
    private static int EXPAND_FACTOR = 2;
    private static WeakReference[] list = new WeakReference[SystemColorTracker.INITIAL_SIZE];
    private static int count = 0;
    private static bool addedTracker;

    private SystemColorTracker()
    {
    }

    internal static void Add(ISystemColorTracker obj)
    {
      lock (typeof (SystemColorTracker))
      {
        if (SystemColorTracker.list.Length == SystemColorTracker.count)
          SystemColorTracker.GarbageCollectList();
        if (!SystemColorTracker.addedTracker)
        {
          SystemColorTracker.addedTracker = true;
          SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(SystemColorTracker.OnUserPreferenceChanged);
        }
        int count = SystemColorTracker.count;
        ++SystemColorTracker.count;
        if (SystemColorTracker.list[count] == null)
          SystemColorTracker.list[count] = new WeakReference((object) obj);
        else
          SystemColorTracker.list[count].Target = (object) obj;
      }
    }

    private static void CleanOutBrokenLinks()
    {
      int index1 = SystemColorTracker.list.Length - 1;
      int index2 = 0;
      int length = SystemColorTracker.list.Length;
      while (true)
      {
        while (index2 >= length || SystemColorTracker.list[index2].Target == null)
        {
          while (index1 >= 0 && SystemColorTracker.list[index1].Target == null)
            --index1;
          if (index2 >= index1)
          {
            SystemColorTracker.count = index2;
            return;
          }
          WeakReference weakReference = SystemColorTracker.list[index2];
          SystemColorTracker.list[index2] = SystemColorTracker.list[index1];
          SystemColorTracker.list[index1] = weakReference;
          ++index2;
          --index1;
        }
        ++index2;
      }
    }

    private static void GarbageCollectList()
    {
      SystemColorTracker.CleanOutBrokenLinks();
      if ((double) SystemColorTracker.count / (double) SystemColorTracker.list.Length <= (double) SystemColorTracker.EXPAND_THRESHOLD)
        return;
      WeakReference[] weakReferenceArray = new WeakReference[SystemColorTracker.list.Length * SystemColorTracker.EXPAND_FACTOR];
      SystemColorTracker.list.CopyTo((Array) weakReferenceArray, 0);
      SystemColorTracker.list = weakReferenceArray;
      int length = SystemColorTracker.list.Length;
      int warningSize = SystemColorTracker.WARNING_SIZE;
    }

    private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
      if (e.Category != UserPreferenceCategory.Color)
        return;
      for (int index = 0; index < SystemColorTracker.count; ++index)
        ((ISystemColorTracker) SystemColorTracker.list[index].Target)?.OnSystemColorChanged();
    }
  }
}
