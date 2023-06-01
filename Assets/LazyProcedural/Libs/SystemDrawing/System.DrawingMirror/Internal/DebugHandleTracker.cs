// Decompiled with JetBrains decompiler
// Type: System.Internal.DebugHandleTracker
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Internal
{
  internal class DebugHandleTracker
  {
    private static Hashtable handleTypes = new Hashtable();
    private static DebugHandleTracker tracker;
    private static object internalSyncObject = new object();

    static DebugHandleTracker()
    {
      DebugHandleTracker.tracker = new DebugHandleTracker();
      if (CompModSwitches.HandleLeak.Level <= TraceLevel.Off && !CompModSwitches.TraceCollect.Enabled)
        return;
      HandleCollector.HandleAdded += new HandleChangeEventHandler(DebugHandleTracker.tracker.OnHandleAdd);
      HandleCollector.HandleRemoved += new HandleChangeEventHandler(DebugHandleTracker.tracker.OnHandleRemove);
    }

    private DebugHandleTracker()
    {
    }

    public static void IgnoreCurrentHandlesAsLeaks()
    {
      lock (DebugHandleTracker.internalSyncObject)
      {
        if (CompModSwitches.HandleLeak.Level < TraceLevel.Warning)
          return;
        DebugHandleTracker.HandleType[] handleTypeArray = new DebugHandleTracker.HandleType[DebugHandleTracker.handleTypes.Values.Count];
        DebugHandleTracker.handleTypes.Values.CopyTo((Array) handleTypeArray, 0);
        for (int index = 0; index < handleTypeArray.Length; ++index)
        {
          if (handleTypeArray[index] != null)
            handleTypeArray[index].IgnoreCurrentHandlesAsLeaks();
        }
      }
    }

    public static void CheckLeaks()
    {
      lock (DebugHandleTracker.internalSyncObject)
      {
        if (CompModSwitches.HandleLeak.Level < TraceLevel.Warning)
          return;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        DebugHandleTracker.HandleType[] handleTypeArray = new DebugHandleTracker.HandleType[DebugHandleTracker.handleTypes.Values.Count];
        DebugHandleTracker.handleTypes.Values.CopyTo((Array) handleTypeArray, 0);
        for (int index = 0; index < handleTypeArray.Length; ++index)
        {
          if (handleTypeArray[index] != null)
            handleTypeArray[index].CheckLeaks();
        }
      }
    }

    public static void Initialize()
    {
    }

    private void OnHandleAdd(string handleName, IntPtr handle, int handleCount)
    {
      DebugHandleTracker.HandleType handleType = (DebugHandleTracker.HandleType) DebugHandleTracker.handleTypes[(object) handleName];
      if (handleType == null)
      {
        handleType = new DebugHandleTracker.HandleType(handleName);
        DebugHandleTracker.handleTypes[(object) handleName] = (object) handleType;
      }
      handleType.Add(handle);
    }

    private void OnHandleRemove(string handleName, IntPtr handle, int HandleCount)
    {
      DebugHandleTracker.HandleType handleType = (DebugHandleTracker.HandleType) DebugHandleTracker.handleTypes[(object) handleName];
      bool flag = false;
      if (handleType != null)
        flag = handleType.Remove(handle);
      if (flag)
        return;
      int level = (int) CompModSwitches.HandleLeak.Level;
    }

    private class HandleType
    {
      public readonly string name;
      private int handleCount;
      private DebugHandleTracker.HandleType.HandleEntry[] buckets;
      private const int BUCKETS = 10;

      public HandleType(string name)
      {
        this.name = name;
        this.buckets = new DebugHandleTracker.HandleType.HandleEntry[10];
      }

      public void Add(IntPtr handle)
      {
        lock (this)
        {
          int hash = this.ComputeHash(handle);
          if (CompModSwitches.HandleLeak.Level >= TraceLevel.Info)
          {
            int level = (int) CompModSwitches.HandleLeak.Level;
          }
          DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[hash];
          while (handleEntry != null)
            handleEntry = handleEntry.next;
          this.buckets[hash] = new DebugHandleTracker.HandleType.HandleEntry(this.buckets[hash], handle);
          ++this.handleCount;
        }
      }

      public void CheckLeaks()
      {
        lock (this)
        {
          bool flag = false;
          if (this.handleCount <= 0)
            return;
          for (int index = 0; index < 10; ++index)
          {
            for (DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[index]; handleEntry != null; handleEntry = handleEntry.next)
            {
              if (!handleEntry.ignorableAsLeak && !flag)
                flag = true;
            }
          }
        }
      }

      public void IgnoreCurrentHandlesAsLeaks()
      {
        lock (this)
        {
          if (this.handleCount <= 0)
            return;
          for (int index = 0; index < 10; ++index)
          {
            for (DebugHandleTracker.HandleType.HandleEntry handleEntry = this.buckets[index]; handleEntry != null; handleEntry = handleEntry.next)
              handleEntry.ignorableAsLeak = true;
          }
        }
      }

      private int ComputeHash(IntPtr handle) => ((int) handle & (int) ushort.MaxValue) % 10;

      public bool Remove(IntPtr handle)
      {
        lock (this)
        {
          int hash = this.ComputeHash(handle);
          if (CompModSwitches.HandleLeak.Level >= TraceLevel.Info)
          {
            int level = (int) CompModSwitches.HandleLeak.Level;
          }
          DebugHandleTracker.HandleType.HandleEntry handleEntry1 = this.buckets[hash];
          DebugHandleTracker.HandleType.HandleEntry handleEntry2 = (DebugHandleTracker.HandleType.HandleEntry) null;
          for (; handleEntry1 != null && handleEntry1.handle != handle; handleEntry1 = handleEntry1.next)
            handleEntry2 = handleEntry1;
          if (handleEntry1 == null)
            return false;
          if (handleEntry2 == null)
            this.buckets[hash] = handleEntry1.next;
          else
            handleEntry2.next = handleEntry1.next;
          --this.handleCount;
          return true;
        }
      }

      private class HandleEntry
      {
        public readonly IntPtr handle;
        public DebugHandleTracker.HandleType.HandleEntry next;
        public readonly string callStack;
        public bool ignorableAsLeak;

        public HandleEntry(DebugHandleTracker.HandleType.HandleEntry next, IntPtr handle)
        {
          this.handle = handle;
          this.next = next;
          if (CompModSwitches.HandleLeak.Level > TraceLevel.Off)
            this.callStack = Environment.StackTrace;
          else
            this.callStack = (string) null;
        }

        public string ToString(DebugHandleTracker.HandleType type)
        {
          DebugHandleTracker.HandleType.HandleEntry.StackParser stackParser = new DebugHandleTracker.HandleType.HandleEntry.StackParser(this.callStack);
          stackParser.DiscardTo("HandleCollector.Add");
          stackParser.DiscardNext();
          stackParser.Truncate(40);
          string str = "";
          return Convert.ToString((int) this.handle, 16) + str + ": " + stackParser.ToString();
        }

        private class StackParser
        {
          internal string releventStack;
          internal int startIndex;
          internal int endIndex;
          internal int length;

          public StackParser(string callStack)
          {
            this.releventStack = callStack;
            this.length = this.releventStack.Length;
          }

          private static bool ContainsString(string str, string token)
          {
            int length1 = str.Length;
            int length2 = token.Length;
            for (int index1 = 0; index1 < length1; ++index1)
            {
              int index2 = 0;
              while (index2 < length2 && (int) str[index1 + index2] == (int) token[index2])
                ++index2;
              if (index2 == length2)
                return true;
            }
            return false;
          }

          public void DiscardNext() => this.GetLine();

          public void DiscardTo(string discardText)
          {
            while (this.startIndex < this.length)
            {
              string line = this.GetLine();
              if (line == null || DebugHandleTracker.HandleType.HandleEntry.StackParser.ContainsString(line, discardText))
                break;
            }
          }

          private string GetLine()
          {
            this.endIndex = this.releventStack.IndexOf('\r', this.startIndex);
            if (this.endIndex < 0)
              this.endIndex = this.length - 1;
            string str = this.releventStack.Substring(this.startIndex, this.endIndex - this.startIndex);
            char relevent;
            while (this.endIndex < this.length && ((relevent = this.releventStack[this.endIndex]) == '\r' || relevent == '\n'))
              ++this.endIndex;
            if (this.startIndex == this.endIndex)
              return (string) null;
            this.startIndex = this.endIndex;
            return str.Replace('\t', ' ');
          }

          public override string ToString() => this.releventStack.Substring(this.startIndex);

          public void Truncate(int lines)
          {
            string str = "";
            while (lines-- > 0 && this.startIndex < this.length)
              str = (str != null ? str + ": " + this.GetLine() : this.GetLine()) + Environment.NewLine;
            this.releventStack = str;
            this.startIndex = 0;
            this.endIndex = 0;
            this.length = this.releventStack.Length;
          }
        }
      }
    }
  }
}
