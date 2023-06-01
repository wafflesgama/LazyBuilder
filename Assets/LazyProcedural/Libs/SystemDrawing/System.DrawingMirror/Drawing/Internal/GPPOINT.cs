// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.GPPOINT
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
  [StructLayout(LayoutKind.Sequential)]
  internal class GPPOINT
  {
    internal int X;
    internal int Y;

    internal GPPOINT()
    {
    }

    internal GPPOINT(PointF pt)
    {
      this.X = (int) pt.X;
      this.Y = (int) pt.Y;
    }

    internal GPPOINT(Point pt)
    {
      this.X = pt.X;
      this.Y = pt.Y;
    }

    internal PointF ToPoint() => new PointF((float) this.X, (float) this.Y);
  }
}
