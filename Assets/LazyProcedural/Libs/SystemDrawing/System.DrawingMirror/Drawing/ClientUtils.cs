// Decompiled with JetBrains decompiler
// Type: System.Drawing.ClientUtils
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Security;
using System.Threading;

namespace System.Drawing
{
  internal static class ClientUtils
  {
    public static bool IsCriticalException(Exception ex)
    {
      switch (ex)
      {
        case NullReferenceException _:
        case StackOverflowException _:
        case OutOfMemoryException _:
        case ThreadAbortException _:
        case ExecutionEngineException _:
        case IndexOutOfRangeException _:
          return true;
        default:
          return ex is AccessViolationException;
      }
    }

    public static bool IsSecurityOrCriticalException(Exception ex) => ex is SecurityException || ClientUtils.IsCriticalException(ex);

    public static int GetBitCount(uint x)
    {
      int bitCount = 0;
      while (x > 0U)
      {
        x &= x - 1U;
        ++bitCount;
      }
      return bitCount;
    }

    public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue) => value >= minValue && value <= maxValue;

    public static bool IsEnumValid(
      Enum enumValue,
      int value,
      int minValue,
      int maxValue,
      int maxNumberOfBitsOn)
    {
      return value >= minValue && value <= maxValue && ClientUtils.GetBitCount((uint) value) <= maxNumberOfBitsOn;
    }

    public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask) => ((long) value & (long) mask) == (long) value;

    public static bool IsEnumValid_NotSequential(
      Enum enumValue,
      int value,
      params int[] enumValues)
    {
      for (int index = 0; index < enumValues.Length; ++index)
      {
        if (enumValues[index] == value)
          return true;
      }
      return false;
    }

    internal class WeakRefCollection : IList, ICollection, IEnumerable
    {
      private int refCheckThreshold = int.MaxValue;
      private ArrayList _innerList;

      internal WeakRefCollection() => this._innerList = new ArrayList(4);

      internal WeakRefCollection(int size) => this._innerList = new ArrayList(size);

      internal ArrayList InnerList => this._innerList;

      public int RefCheckThreshold
      {
        get => this.refCheckThreshold;
        set => this.refCheckThreshold = value;
      }

      public object this[int index]
      {
        get => this.InnerList[index] is ClientUtils.WeakRefCollection.WeakRefObject inner && inner.IsAlive ? inner.Target : (object) null;
        set => this.InnerList[index] = (object) this.CreateWeakRefObject(value);
      }

      public void ScavengeReferences()
      {
        int index1 = 0;
        int count = this.Count;
        for (int index2 = 0; index2 < count; ++index2)
        {
          if (this[index1] == null)
            this.InnerList.RemoveAt(index1);
          else
            ++index1;
        }
      }

      public override bool Equals(object obj)
      {
        ClientUtils.WeakRefCollection weakRefCollection = obj as ClientUtils.WeakRefCollection;
        if (weakRefCollection == this)
          return true;
        if (weakRefCollection == null || this.Count != weakRefCollection.Count)
          return false;
        for (int index = 0; index < this.Count; ++index)
        {
          if (this.InnerList[index] != weakRefCollection.InnerList[index] && (this.InnerList[index] == null || !this.InnerList[index].Equals(weakRefCollection.InnerList[index])))
            return false;
        }
        return true;
      }

      public override int GetHashCode() => base.GetHashCode();

      private ClientUtils.WeakRefCollection.WeakRefObject CreateWeakRefObject(object value) => value == null ? (ClientUtils.WeakRefCollection.WeakRefObject) null : new ClientUtils.WeakRefCollection.WeakRefObject(value);

      private static void Copy(
        ClientUtils.WeakRefCollection sourceList,
        int sourceIndex,
        ClientUtils.WeakRefCollection destinationList,
        int destinationIndex,
        int length)
      {
        if (sourceIndex < destinationIndex)
        {
          sourceIndex += length;
          destinationIndex += length;
          for (; length > 0; --length)
            destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
        }
        else
        {
          for (; length > 0; --length)
            destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
        }
      }

      public void RemoveByHashCode(object value)
      {
        if (value == null)
          return;
        int hashCode = value.GetHashCode();
        for (int index = 0; index < this.InnerList.Count; ++index)
        {
          if (this.InnerList[index] != null && this.InnerList[index].GetHashCode() == hashCode)
          {
            this.RemoveAt(index);
            break;
          }
        }
      }

      public void Clear() => this.InnerList.Clear();

      public bool IsFixedSize => this.InnerList.IsFixedSize;

      public bool Contains(object value) => this.InnerList.Contains((object) this.CreateWeakRefObject(value));

      public void RemoveAt(int index) => this.InnerList.RemoveAt(index);

      public void Remove(object value) => this.InnerList.Remove((object) this.CreateWeakRefObject(value));

      public int IndexOf(object value) => this.InnerList.IndexOf((object) this.CreateWeakRefObject(value));

      public void Insert(int index, object value) => this.InnerList.Insert(index, (object) this.CreateWeakRefObject(value));

      public int Add(object value)
      {
        if (this.Count > this.RefCheckThreshold)
          this.ScavengeReferences();
        return this.InnerList.Add((object) this.CreateWeakRefObject(value));
      }

      public int Count => this.InnerList.Count;

      object ICollection.SyncRoot => this.InnerList.SyncRoot;

      public bool IsReadOnly => this.InnerList.IsReadOnly;

      public void CopyTo(Array array, int index) => this.InnerList.CopyTo(array, index);

      bool ICollection.IsSynchronized => this.InnerList.IsSynchronized;

      public IEnumerator GetEnumerator() => this.InnerList.GetEnumerator();

      internal class WeakRefObject
      {
        private int hash;
        private WeakReference weakHolder;

        internal WeakRefObject(object obj)
        {
          this.weakHolder = new WeakReference(obj);
          this.hash = obj.GetHashCode();
        }

        internal bool IsAlive => this.weakHolder.IsAlive;

        internal object Target => this.weakHolder.Target;

        public override int GetHashCode() => this.hash;

        public override bool Equals(object obj)
        {
          ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = obj as ClientUtils.WeakRefCollection.WeakRefObject;
          return weakRefObject == this || weakRefObject != null && (weakRefObject.Target == this.Target || this.Target != null && this.Target.Equals(weakRefObject.Target));
        }
      }
    }
  }
}
