// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.IntUnsafeNativeMethods
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Internal
{
  [SuppressUnmanagedCodeSecurity]
  internal static class IntUnsafeNativeMethods
  {
    [DllImport("user32.dll", EntryPoint = "GetDC", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntGetDC(HandleRef hWnd);

    public static IntPtr GetDC(HandleRef hWnd) => System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntGetDC(hWnd), IntSafeNativeMethods.CommonHandles.HDC);

    [DllImport("gdi32.dll", EntryPoint = "DeleteDC", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntDeleteDC(HandleRef hDC);

    public static bool DeleteDC(HandleRef hDC)
    {
      System.Internal.HandleCollector.Remove((IntPtr) hDC, IntSafeNativeMethods.CommonHandles.GDI);
      return IntUnsafeNativeMethods.IntDeleteDC(hDC);
    }

    public static bool DeleteHDC(HandleRef hDC)
    {
      System.Internal.HandleCollector.Remove((IntPtr) hDC, IntSafeNativeMethods.CommonHandles.HDC);
      return IntUnsafeNativeMethods.IntDeleteDC(hDC);
    }

    [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

    public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
    {
      System.Internal.HandleCollector.Remove((IntPtr) hDC, IntSafeNativeMethods.CommonHandles.HDC);
      return IntUnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
    }

    [DllImport("gdi32.dll", EntryPoint = "CreateDC", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntCreateDC(
      string lpszDriverName,
      string lpszDeviceName,
      string lpszOutput,
      HandleRef lpInitData);

    public static IntPtr CreateDC(
      string lpszDriverName,
      string lpszDeviceName,
      string lpszOutput,
      HandleRef lpInitData)
    {
      return System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntCreateDC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), IntSafeNativeMethods.CommonHandles.HDC);
    }

    [DllImport("gdi32.dll", EntryPoint = "CreateIC", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntCreateIC(
      string lpszDriverName,
      string lpszDeviceName,
      string lpszOutput,
      HandleRef lpInitData);

    public static IntPtr CreateIC(
      string lpszDriverName,
      string lpszDeviceName,
      string lpszOutput,
      HandleRef lpInitData)
    {
      return System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntCreateIC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), IntSafeNativeMethods.CommonHandles.HDC);
    }

    [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

    public static IntPtr CreateCompatibleDC(HandleRef hDC) => System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntCreateCompatibleDC(hDC), IntSafeNativeMethods.CommonHandles.GDI);

    [DllImport("gdi32.dll", EntryPoint = "SaveDC", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntSaveDC(HandleRef hDC);

    public static int SaveDC(HandleRef hDC) => IntUnsafeNativeMethods.IntSaveDC(hDC);

    [DllImport("gdi32.dll", EntryPoint = "RestoreDC", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntRestoreDC(HandleRef hDC, int nSavedDC);

    public static bool RestoreDC(HandleRef hDC, int nSavedDC) => IntUnsafeNativeMethods.IntRestoreDC(hDC, nSavedDC);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr WindowFromDC(HandleRef hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

    [DllImport("gdi32.dll", EntryPoint = "OffsetViewportOrgEx", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntOffsetViewportOrgEx(
      HandleRef hDC,
      int nXOffset,
      int nYOffset,
      [In, Out] IntNativeMethods.POINT point);

    public static bool OffsetViewportOrgEx(
      HandleRef hDC,
      int nXOffset,
      int nYOffset,
      [In, Out] IntNativeMethods.POINT point)
    {
      return IntUnsafeNativeMethods.IntOffsetViewportOrgEx(hDC, nXOffset, nYOffset, point);
    }

    [DllImport("gdi32.dll", EntryPoint = "SetGraphicsMode", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntSetGraphicsMode(HandleRef hDC, int iMode);

    public static int SetGraphicsMode(HandleRef hDC, int iMode)
    {
      iMode = IntUnsafeNativeMethods.IntSetGraphicsMode(hDC, iMode);
      return iMode;
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetGraphicsMode(HandleRef hDC);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern int GetROP2(HandleRef hdc);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SetROP2(HandleRef hDC, int nDrawMode);

    [DllImport("gdi32.dll", EntryPoint = "CombineRgn", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntNativeMethods.RegionFlags IntCombineRgn(
      HandleRef hRgnDest,
      HandleRef hRgnSrc1,
      HandleRef hRgnSrc2,
      RegionCombineMode combineMode);

    public static IntNativeMethods.RegionFlags CombineRgn(
      HandleRef hRgnDest,
      HandleRef hRgnSrc1,
      HandleRef hRgnSrc2,
      RegionCombineMode combineMode)
    {
      return hRgnDest.Wrapper == null || hRgnSrc1.Wrapper == null || hRgnSrc2.Wrapper == null ? IntNativeMethods.RegionFlags.ERROR : IntUnsafeNativeMethods.IntCombineRgn(hRgnDest, hRgnSrc1, hRgnSrc2, combineMode);
    }

    [DllImport("gdi32.dll", EntryPoint = "GetClipRgn", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntGetClipRgn(HandleRef hDC, HandleRef hRgn);

    public static int GetClipRgn(HandleRef hDC, HandleRef hRgn) => IntUnsafeNativeMethods.IntGetClipRgn(hDC, hRgn);

    [DllImport("gdi32.dll", EntryPoint = "SelectClipRgn", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntNativeMethods.RegionFlags IntSelectClipRgn(
      HandleRef hDC,
      HandleRef hRgn);

    public static IntNativeMethods.RegionFlags SelectClipRgn(HandleRef hDC, HandleRef hRgn) => IntUnsafeNativeMethods.IntSelectClipRgn(hDC, hRgn);

    [DllImport("gdi32.dll", EntryPoint = "GetRgnBox", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntNativeMethods.RegionFlags IntGetRgnBox(
      HandleRef hRgn,
      [In, Out] ref IntNativeMethods.RECT clipRect);

    public static IntNativeMethods.RegionFlags GetRgnBox(
      HandleRef hRgn,
      [In, Out] ref IntNativeMethods.RECT clipRect)
    {
      return IntUnsafeNativeMethods.IntGetRgnBox(hRgn, ref clipRect);
    }

    [DllImport("gdi32.dll", EntryPoint = "CreateFontIndirect", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntCreateFontIndirect([MarshalAs(UnmanagedType.AsAny), In, Out] object lf);

    public static IntPtr CreateFontIndirect(object lf) => System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntCreateFontIndirect(lf), IntSafeNativeMethods.CommonHandles.GDI);

    [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntDeleteObject(HandleRef hObject);

    public static bool DeleteObject(HandleRef hObject)
    {
      System.Internal.HandleCollector.Remove((IntPtr) hObject, IntSafeNativeMethods.CommonHandles.GDI);
      return IntUnsafeNativeMethods.IntDeleteObject(hObject);
    }

    [DllImport("gdi32.dll", EntryPoint = "GetObject", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntGetObject(
      HandleRef hBrush,
      int nSize,
      [In, Out] IntNativeMethods.LOGBRUSH lb);

    public static int GetObject(HandleRef hBrush, IntNativeMethods.LOGBRUSH lb) => IntUnsafeNativeMethods.IntGetObject(hBrush, Marshal.SizeOf(typeof (IntNativeMethods.LOGBRUSH)), lb);

    [DllImport("gdi32.dll", EntryPoint = "GetObject", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntGetObject(HandleRef hFont, int nSize, [In, Out] IntNativeMethods.LOGFONT lf);

    public static int GetObject(HandleRef hFont, IntNativeMethods.LOGFONT lp) => IntUnsafeNativeMethods.IntGetObject(hFont, Marshal.SizeOf(typeof (IntNativeMethods.LOGFONT)), lp);

    [DllImport("gdi32.dll", EntryPoint = "SelectObject", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntSelectObject(HandleRef hdc, HandleRef obj);

    public static IntPtr SelectObject(HandleRef hdc, HandleRef obj) => IntUnsafeNativeMethods.IntSelectObject(hdc, obj);

    [DllImport("gdi32.dll", EntryPoint = "GetCurrentObject", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntGetCurrentObject(HandleRef hDC, int uObjectType);

    public static IntPtr GetCurrentObject(HandleRef hDC, int uObjectType) => IntUnsafeNativeMethods.IntGetCurrentObject(hDC, uObjectType);

    [DllImport("gdi32.dll", EntryPoint = "GetStockObject", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntGetStockObject(int nIndex);

    public static IntPtr GetStockObject(int nIndex) => IntUnsafeNativeMethods.IntGetStockObject(nIndex);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetNearestColor(HandleRef hDC, int color);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SetTextColor(HandleRef hDC, int crColor);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetTextAlign(HandleRef hdc);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetTextColor(HandleRef hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SetBkColor(HandleRef hDC, int clr);

    [DllImport("gdi32.dll", EntryPoint = "SetBkMode", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntSetBkMode(HandleRef hDC, int nBkMode);

    public static int SetBkMode(HandleRef hDC, int nBkMode) => IntUnsafeNativeMethods.IntSetBkMode(hDC, nBkMode);

    [DllImport("gdi32.dll", EntryPoint = "GetBkMode", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntGetBkMode(HandleRef hDC);

    public static int GetBkMode(HandleRef hDC) => IntUnsafeNativeMethods.IntGetBkMode(hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetBkColor(HandleRef hDC);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int DrawTextW(
      HandleRef hDC,
      string lpszString,
      int nCount,
      ref IntNativeMethods.RECT lpRect,
      int nFormat);

    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int DrawTextA(
      HandleRef hDC,
      byte[] lpszString,
      int byteCount,
      ref IntNativeMethods.RECT lpRect,
      int nFormat);

    public static int DrawText(
      HandleRef hDC,
      string text,
      ref IntNativeMethods.RECT lpRect,
      int nFormat)
    {
      int num;
      if (Marshal.SystemDefaultCharSize == 1)
      {
        lpRect.top = Math.Min((int) short.MaxValue, lpRect.top);
        lpRect.left = Math.Min((int) short.MaxValue, lpRect.left);
        lpRect.right = Math.Min((int) short.MaxValue, lpRect.right);
        lpRect.bottom = Math.Min((int) short.MaxValue, lpRect.bottom);
        int multiByte = IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, (byte[]) null, 0, IntPtr.Zero, IntPtr.Zero);
        byte[] numArray = new byte[multiByte];
        IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, numArray, numArray.Length, IntPtr.Zero, IntPtr.Zero);
        int byteCount = Math.Min(multiByte, 8192);
        num = IntUnsafeNativeMethods.DrawTextA(hDC, numArray, byteCount, ref lpRect, nFormat);
      }
      else
        num = IntUnsafeNativeMethods.DrawTextW(hDC, text, text.Length, ref lpRect, nFormat);
      return num;
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int DrawTextExW(
      HandleRef hDC,
      string lpszString,
      int nCount,
      ref IntNativeMethods.RECT lpRect,
      int nFormat,
      [In, Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams);

    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int DrawTextExA(
      HandleRef hDC,
      byte[] lpszString,
      int byteCount,
      ref IntNativeMethods.RECT lpRect,
      int nFormat,
      [In, Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams);

    public static int DrawTextEx(
      HandleRef hDC,
      string text,
      ref IntNativeMethods.RECT lpRect,
      int nFormat,
      [In, Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams)
    {
      int num;
      if (Marshal.SystemDefaultCharSize == 1)
      {
        lpRect.top = Math.Min((int) short.MaxValue, lpRect.top);
        lpRect.left = Math.Min((int) short.MaxValue, lpRect.left);
        lpRect.right = Math.Min((int) short.MaxValue, lpRect.right);
        lpRect.bottom = Math.Min((int) short.MaxValue, lpRect.bottom);
        int multiByte = IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, (byte[]) null, 0, IntPtr.Zero, IntPtr.Zero);
        byte[] numArray = new byte[multiByte];
        IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, numArray, numArray.Length, IntPtr.Zero, IntPtr.Zero);
        int byteCount = Math.Min(multiByte, 8192);
        num = IntUnsafeNativeMethods.DrawTextExA(hDC, numArray, byteCount, ref lpRect, nFormat, lpDTParams);
      }
      else
        num = IntUnsafeNativeMethods.DrawTextExW(hDC, text, text.Length, ref lpRect, nFormat, lpDTParams);
      return num;
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int GetTextExtentPoint32W(
      HandleRef hDC,
      string text,
      int len,
      [In, Out] IntNativeMethods.SIZE size);

    [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int GetTextExtentPoint32A(
      HandleRef hDC,
      byte[] lpszString,
      int byteCount,
      [In, Out] IntNativeMethods.SIZE size);

    public static int GetTextExtentPoint32(HandleRef hDC, string text, [In, Out] IntNativeMethods.SIZE size)
    {
      int length = text.Length;
      int textExtentPoint32;
      if (Marshal.SystemDefaultCharSize == 1)
      {
        byte[] numArray = new byte[IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, (byte[]) null, 0, IntPtr.Zero, IntPtr.Zero)];
        IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, numArray, numArray.Length, IntPtr.Zero, IntPtr.Zero);
        int byteCount = Math.Min(text.Length, 8192);
        textExtentPoint32 = IntUnsafeNativeMethods.GetTextExtentPoint32A(hDC, numArray, byteCount, size);
      }
      else
        textExtentPoint32 = IntUnsafeNativeMethods.GetTextExtentPoint32W(hDC, text, text.Length, size);
      return textExtentPoint32;
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern bool ExtTextOut(
      HandleRef hdc,
      int x,
      int y,
      int options,
      ref IntNativeMethods.RECT rect,
      string str,
      int length,
      int[] spacing);

    [DllImport("gdi32.dll", EntryPoint = "LineTo", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntLineTo(HandleRef hdc, int x, int y);

    public static bool LineTo(HandleRef hdc, int x, int y) => IntUnsafeNativeMethods.IntLineTo(hdc, x, y);

    [DllImport("gdi32.dll", EntryPoint = "MoveToEx", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntMoveToEx(HandleRef hdc, int x, int y, IntNativeMethods.POINT pt);

    public static bool MoveToEx(HandleRef hdc, int x, int y, IntNativeMethods.POINT pt) => IntUnsafeNativeMethods.IntMoveToEx(hdc, x, y, pt);

    [DllImport("gdi32.dll", EntryPoint = "Rectangle", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntRectangle(
      HandleRef hdc,
      int left,
      int top,
      int right,
      int bottom);

    public static bool Rectangle(HandleRef hdc, int left, int top, int right, int bottom) => IntUnsafeNativeMethods.IntRectangle(hdc, left, top, right, bottom);

    [DllImport("user32.dll", EntryPoint = "FillRect", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntFillRect(
      HandleRef hdc,
      [In] ref IntNativeMethods.RECT rect,
      HandleRef hbrush);

    public static bool FillRect(HandleRef hDC, [In] ref IntNativeMethods.RECT rect, HandleRef hbrush) => IntUnsafeNativeMethods.IntFillRect(hDC, ref rect, hbrush);

    [DllImport("gdi32.dll", EntryPoint = "SetMapMode", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntSetMapMode(HandleRef hDC, int nMapMode);

    public static int SetMapMode(HandleRef hDC, int nMapMode) => IntUnsafeNativeMethods.IntSetMapMode(hDC, nMapMode);

    [DllImport("gdi32.dll", EntryPoint = "GetMapMode", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntGetMapMode(HandleRef hDC);

    public static int GetMapMode(HandleRef hDC) => IntUnsafeNativeMethods.IntGetMapMode(hDC);

    [DllImport("gdi32.dll", EntryPoint = "GetViewportExtEx", SetLastError = true)]
    public static extern bool IntGetViewportExtEx(HandleRef hdc, [In, Out] IntNativeMethods.SIZE lpSize);

    public static bool GetViewportExtEx(HandleRef hdc, [In, Out] IntNativeMethods.SIZE lpSize) => IntUnsafeNativeMethods.IntGetViewportExtEx(hdc, lpSize);

    [DllImport("gdi32.dll", EntryPoint = "GetViewportOrgEx", SetLastError = true)]
    public static extern bool IntGetViewportOrgEx(HandleRef hdc, [In, Out] IntNativeMethods.POINT lpPoint);

    public static bool GetViewportOrgEx(HandleRef hdc, [In, Out] IntNativeMethods.POINT lpPoint) => IntUnsafeNativeMethods.IntGetViewportOrgEx(hdc, lpPoint);

    [DllImport("gdi32.dll", EntryPoint = "SetViewportExtEx", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntSetViewportExtEx(
      HandleRef hDC,
      int x,
      int y,
      [In, Out] IntNativeMethods.SIZE size);

    public static bool SetViewportExtEx(HandleRef hDC, int x, int y, [In, Out] IntNativeMethods.SIZE size) => IntUnsafeNativeMethods.IntSetViewportExtEx(hDC, x, y, size);

    [DllImport("gdi32.dll", EntryPoint = "SetViewportOrgEx", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntSetViewportOrgEx(
      HandleRef hDC,
      int x,
      int y,
      [In, Out] IntNativeMethods.POINT point);

    public static bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In, Out] IntNativeMethods.POINT point) => IntUnsafeNativeMethods.IntSetViewportOrgEx(hDC, x, y, point);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int GetTextMetricsW(HandleRef hDC, [In, Out] ref IntNativeMethods.TEXTMETRIC lptm);

    [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int GetTextMetricsA(HandleRef hDC, [In, Out] ref IntNativeMethods.TEXTMETRICA lptm);

    public static int GetTextMetrics(HandleRef hDC, ref IntNativeMethods.TEXTMETRIC lptm)
    {
      int textMetrics;
      if (Marshal.SystemDefaultCharSize == 1)
      {
        IntNativeMethods.TEXTMETRICA lptm1 = new IntNativeMethods.TEXTMETRICA();
        textMetrics = IntUnsafeNativeMethods.GetTextMetricsA(hDC, ref lptm1);
        lptm.tmHeight = lptm1.tmHeight;
        lptm.tmAscent = lptm1.tmAscent;
        lptm.tmDescent = lptm1.tmDescent;
        lptm.tmInternalLeading = lptm1.tmInternalLeading;
        lptm.tmExternalLeading = lptm1.tmExternalLeading;
        lptm.tmAveCharWidth = lptm1.tmAveCharWidth;
        lptm.tmMaxCharWidth = lptm1.tmMaxCharWidth;
        lptm.tmWeight = lptm1.tmWeight;
        lptm.tmOverhang = lptm1.tmOverhang;
        lptm.tmDigitizedAspectX = lptm1.tmDigitizedAspectX;
        lptm.tmDigitizedAspectY = lptm1.tmDigitizedAspectY;
        lptm.tmFirstChar = (char) lptm1.tmFirstChar;
        lptm.tmLastChar = (char) lptm1.tmLastChar;
        lptm.tmDefaultChar = (char) lptm1.tmDefaultChar;
        lptm.tmBreakChar = (char) lptm1.tmBreakChar;
        lptm.tmItalic = lptm1.tmItalic;
        lptm.tmUnderlined = lptm1.tmUnderlined;
        lptm.tmStruckOut = lptm1.tmStruckOut;
        lptm.tmPitchAndFamily = lptm1.tmPitchAndFamily;
        lptm.tmCharSet = lptm1.tmCharSet;
      }
      else
        textMetrics = IntUnsafeNativeMethods.GetTextMetricsW(hDC, ref lptm);
      return textMetrics;
    }

    [DllImport("gdi32.dll", EntryPoint = "BeginPath", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntBeginPath(HandleRef hDC);

    public static bool BeginPath(HandleRef hDC) => IntUnsafeNativeMethods.IntBeginPath(hDC);

    [DllImport("gdi32.dll", EntryPoint = "EndPath", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntEndPath(HandleRef hDC);

    public static bool EndPath(HandleRef hDC) => IntUnsafeNativeMethods.IntEndPath(hDC);

    [DllImport("gdi32.dll", EntryPoint = "StrokePath", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntStrokePath(HandleRef hDC);

    public static bool StrokePath(HandleRef hDC) => IntUnsafeNativeMethods.IntStrokePath(hDC);

    [DllImport("gdi32.dll", EntryPoint = "AngleArc", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntAngleArc(
      HandleRef hDC,
      int x,
      int y,
      int radius,
      float startAngle,
      float endAngle);

    public static bool AngleArc(
      HandleRef hDC,
      int x,
      int y,
      int radius,
      float startAngle,
      float endAngle)
    {
      return IntUnsafeNativeMethods.IntAngleArc(hDC, x, y, radius, startAngle, endAngle);
    }

    [DllImport("gdi32.dll", EntryPoint = "Arc", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntArc(
      HandleRef hDC,
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nXStartArc,
      int nYStartArc,
      int nXEndArc,
      int nYEndArc);

    public static bool Arc(
      HandleRef hDC,
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nXStartArc,
      int nYStartArc,
      int nXEndArc,
      int nYEndArc)
    {
      return IntUnsafeNativeMethods.IntArc(hDC, nLeftRect, nTopRect, nRightRect, nBottomRect, nXStartArc, nYStartArc, nXEndArc, nYEndArc);
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SetTextAlign(HandleRef hDC, int nMode);

    [DllImport("gdi32.dll", EntryPoint = "Ellipse", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntEllipse(HandleRef hDc, int x1, int y1, int x2, int y2);

    public static bool Ellipse(HandleRef hDc, int x1, int y1, int x2, int y2) => IntUnsafeNativeMethods.IntEllipse(hDc, x1, y1, x2, y2);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern int WideCharToMultiByte(
      int codePage,
      int flags,
      [MarshalAs(UnmanagedType.LPWStr)] string wideStr,
      int chars,
      [In, Out] byte[] pOutBytes,
      int bufferBytes,
      IntPtr defaultChar,
      IntPtr pDefaultUsed);
  }
}
