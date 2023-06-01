// Decompiled with JetBrains decompiler
// Type: System.InvariantComparer
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Globalization;

namespace System
{
  [Serializable]
  internal class InvariantComparer : IComparer
  {
    private CompareInfo m_compareInfo;
    internal static readonly InvariantComparer Default = new InvariantComparer();

    internal InvariantComparer() => this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;

    public int Compare(object a, object b)
    {
      string string1 = a as string;
      string string2 = b as string;
      return string1 != null && string2 != null ? this.m_compareInfo.Compare(string1, string2) : Comparer.Default.Compare(a, b);
    }
  }
}
