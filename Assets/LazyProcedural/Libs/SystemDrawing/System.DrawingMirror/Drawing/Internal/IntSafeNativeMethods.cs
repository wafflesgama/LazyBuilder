// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.IntSafeNativeMethods
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Internal
{
  [SuppressUnmanagedCodeSecurity]
  internal static class IntSafeNativeMethods
  {
    [DllImport("gdi32.dll", EntryPoint = "CreateSolidBrush", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCreateSolidBrush(int crColor);

    public static IntPtr CreateSolidBrush(int crColor) => System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreateSolidBrush(crColor), IntSafeNativeMethods.CommonHandles.GDI);

    [DllImport("gdi32.dll", EntryPoint = "CreatePen", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCreatePen(int fnStyle, int nWidth, int crColor);

    public static IntPtr CreatePen(int fnStyle, int nWidth, int crColor) => System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreatePen(fnStyle, nWidth, crColor), IntSafeNativeMethods.CommonHandles.GDI);

    [DllImport("gdi32.dll", EntryPoint = "ExtCreatePen", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntExtCreatePen(
      int fnStyle,
      int dwWidth,
      IntNativeMethods.LOGBRUSH lplb,
      int dwStyleCount,
      [MarshalAs(UnmanagedType.LPArray)] int[] lpStyle);

    public static IntPtr ExtCreatePen(
      int fnStyle,
      int dwWidth,
      IntNativeMethods.LOGBRUSH lplb,
      int dwStyleCount,
      int[] lpStyle)
    {
      return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntExtCreatePen(fnStyle, dwWidth, lplb, dwStyleCount, lpStyle), IntSafeNativeMethods.CommonHandles.GDI);
    }

    [DllImport("gdi32.dll", EntryPoint = "CreateRectRgn", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

    public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2) => System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreateRectRgn(x1, y1, x2, y2), IntSafeNativeMethods.CommonHandles.GDI);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetUserDefaultLCID();

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool GdiFlush();

    public sealed class CommonHandles
    {
      public static readonly int EMF = System.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
      public static readonly int GDI = System.Internal.HandleCollector.RegisterType(nameof (GDI), 90, 50);
      public static readonly int HDC = System.Internal.HandleCollector.RegisterType(nameof (HDC), 100, 2);
    }
  }
}
