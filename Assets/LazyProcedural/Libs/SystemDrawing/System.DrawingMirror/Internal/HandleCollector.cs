// Decompiled with JetBrains decompiler
// Type: System.Internal.HandleCollector
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Threading;

namespace System.Internal
{
  internal sealed class HandleCollector
  {
    private static HandleCollector.HandleType[] handleTypes;
    private static int handleTypeCount;
    private static int suspendCount;
    private static object internalSyncObject = new object();

    internal static event HandleChangeEventHandler HandleAdded;

    internal static event HandleChangeEventHandler HandleRemoved;

    internal static IntPtr Add(IntPtr handle, int type)
    {
      HandleCollector.handleTypes[type - 1].Add(handle);
      return handle;
    }

    internal static void SuspendCollect()
    {
      lock (HandleCollector.internalSyncObject)
        ++HandleCollector.suspendCount;
    }

    internal static void ResumeCollect()
    {
      bool flag = false;
      lock (HandleCollector.internalSyncObject)
      {
        if (HandleCollector.suspendCount > 0)
          --HandleCollector.suspendCount;
        if (HandleCollector.suspendCount == 0)
        {
          for (int index = 0; index < HandleCollector.handleTypeCount; ++index)
          {
            lock (HandleCollector.handleTypes[index])
            {
              if (HandleCollector.handleTypes[index].NeedCollection())
                flag = true;
            }
          }
        }
      }
      if (!flag)
        return;
      GC.Collect();
    }

    internal static int RegisterType(string typeName, int expense, int initialThreshold)
    {
      lock (HandleCollector.internalSyncObject)
      {
        if (HandleCollector.handleTypeCount == 0 || HandleCollector.handleTypeCount == HandleCollector.handleTypes.Length)
        {
          HandleCollector.HandleType[] destinationArray = new HandleCollector.HandleType[HandleCollector.handleTypeCount + 10];
          if (HandleCollector.handleTypes != null)
            Array.Copy((Array) HandleCollector.handleTypes, 0, (Array) destinationArray, 0, HandleCollector.handleTypeCount);
          HandleCollector.handleTypes = destinationArray;
        }
        HandleCollector.handleTypes[HandleCollector.handleTypeCount++] = new HandleCollector.HandleType(typeName, expense, initialThreshold);
        return HandleCollector.handleTypeCount;
      }
    }

    internal static IntPtr Remove(IntPtr handle, int type) => HandleCollector.handleTypes[type - 1].Remove(handle);

    private class HandleType
    {
      internal readonly string name;
      private int initialThreshHold;
      private int threshHold;
      private int handleCount;
      private readonly int deltaPercent;

      internal HandleType(string name, int expense, int initialThreshHold)
      {
        this.name = name;
        this.initialThreshHold = initialThreshHold;
        this.threshHold = initialThreshHold;
        this.deltaPercent = 100 - expense;
      }

      internal void Add(IntPtr handle)
      {
        if (handle == IntPtr.Zero)
          return;
        bool flag = false;
        int currentHandleCount = 0;
        lock (this)
        {
          ++this.handleCount;
          flag = this.NeedCollection();
          currentHandleCount = this.handleCount;
        }
        lock (HandleCollector.internalSyncObject)
        {
          if (HandleCollector.HandleAdded != null)
            HandleCollector.HandleAdded(this.name, handle, currentHandleCount);
        }
        if (!flag || !flag)
          return;
        GC.Collect();
        Thread.Sleep((100 - this.deltaPercent) / 4);
      }

      internal int GetHandleCount()
      {
        lock (this)
          return this.handleCount;
      }

      internal bool NeedCollection()
      {
        if (HandleCollector.suspendCount > 0)
          return false;
        if (this.handleCount > this.threshHold)
        {
          this.threshHold = this.handleCount + this.handleCount * this.deltaPercent / 100;
          return true;
        }
        int num = 100 * this.threshHold / (100 + this.deltaPercent);
        if (num >= this.initialThreshHold && this.handleCount < (int) ((double) num * 0.89999997615814209))
          this.threshHold = num;
        return false;
      }

      internal IntPtr Remove(IntPtr handle)
      {
        if (handle == IntPtr.Zero)
          return handle;
        int currentHandleCount = 0;
        lock (this)
        {
          --this.handleCount;
          if (this.handleCount < 0)
            this.handleCount = 0;
          currentHandleCount = this.handleCount;
        }
        lock (HandleCollector.internalSyncObject)
        {
          if (HandleCollector.HandleRemoved != null)
            HandleCollector.HandleRemoved(this.name, handle, currentHandleCount);
        }
        return handle;
      }
    }
  }
}
