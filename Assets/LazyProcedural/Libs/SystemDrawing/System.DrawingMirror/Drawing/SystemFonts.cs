// Decompiled with JetBrains decompiler
// Type: System.Drawing.SystemFonts
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing
{
  /// <summary>Specifies the fonts used to display text in Windows display elements.</summary>
  public sealed class SystemFonts
  {
    private static readonly object SystemFontsKey = new object();

    private SystemFonts()
    {
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of windows.</summary>
    /// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of windows.</returns>
    public static Font CaptionFont
    {
      get
      {
        Font font = (Font) null;
        NativeMethods.NONCLIENTMETRICS pvParam = new NativeMethods.NONCLIENTMETRICS();
        if (UnsafeNativeMethods.SystemParametersInfo(41, pvParam.cbSize, pvParam, 0) && pvParam.lfCaptionFont != null)
        {
          IntSecurity.ObjectFromWin32Handle.Assert();
          try
          {
            font = Font.FromLogFont((object) pvParam.lfCaptionFont);
          }
          catch (Exception ex)
          {
            if (SystemFonts.IsCriticalFontException(ex))
              throw;
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
          if (font == null)
            font = SystemFonts.DefaultFont;
          else if (font.Unit != GraphicsUnit.Point)
            font = SystemFonts.FontInPoints(font);
        }
        font.SetSystemFontName(nameof (CaptionFont));
        return font;
      }
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of small windows, such as tool windows.</summary>
    /// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of small windows, such as tool windows.</returns>
    public static Font SmallCaptionFont
    {
      get
      {
        Font font = (Font) null;
        NativeMethods.NONCLIENTMETRICS pvParam = new NativeMethods.NONCLIENTMETRICS();
        if (UnsafeNativeMethods.SystemParametersInfo(41, pvParam.cbSize, pvParam, 0) && pvParam.lfSmCaptionFont != null)
        {
          IntSecurity.ObjectFromWin32Handle.Assert();
          try
          {
            font = Font.FromLogFont((object) pvParam.lfSmCaptionFont);
          }
          catch (Exception ex)
          {
            if (SystemFonts.IsCriticalFontException(ex))
              throw;
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
          if (font == null)
            font = SystemFonts.DefaultFont;
          else if (font.Unit != GraphicsUnit.Point)
            font = SystemFonts.FontInPoints(font);
        }
        font.SetSystemFontName(nameof (SmallCaptionFont));
        return font;
      }
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for menus.</summary>
    /// <returns>A <see cref="T:System.Drawing.Font" /> that is used for menus.</returns>
    public static Font MenuFont
    {
      get
      {
        Font font = (Font) null;
        NativeMethods.NONCLIENTMETRICS pvParam = new NativeMethods.NONCLIENTMETRICS();
        if (UnsafeNativeMethods.SystemParametersInfo(41, pvParam.cbSize, pvParam, 0) && pvParam.lfMenuFont != null)
        {
          IntSecurity.ObjectFromWin32Handle.Assert();
          try
          {
            font = Font.FromLogFont((object) pvParam.lfMenuFont);
          }
          catch (Exception ex)
          {
            if (SystemFonts.IsCriticalFontException(ex))
              throw;
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
          if (font == null)
            font = SystemFonts.DefaultFont;
          else if (font.Unit != GraphicsUnit.Point)
            font = SystemFonts.FontInPoints(font);
        }
        font.SetSystemFontName(nameof (MenuFont));
        return font;
      }
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the status bar.</summary>
    /// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the status bar.</returns>
    public static Font StatusFont
    {
      get
      {
        Font font = (Font) null;
        NativeMethods.NONCLIENTMETRICS pvParam = new NativeMethods.NONCLIENTMETRICS();
        if (UnsafeNativeMethods.SystemParametersInfo(41, pvParam.cbSize, pvParam, 0) && pvParam.lfStatusFont != null)
        {
          IntSecurity.ObjectFromWin32Handle.Assert();
          try
          {
            font = Font.FromLogFont((object) pvParam.lfStatusFont);
          }
          catch (Exception ex)
          {
            if (SystemFonts.IsCriticalFontException(ex))
              throw;
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
          if (font == null)
            font = SystemFonts.DefaultFont;
          else if (font.Unit != GraphicsUnit.Point)
            font = SystemFonts.FontInPoints(font);
        }
        font.SetSystemFontName(nameof (StatusFont));
        return font;
      }
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for message boxes.</summary>
    /// <returns>A <see cref="T:System.Drawing.Font" /> that is used for message boxes</returns>
    public static Font MessageBoxFont
    {
      get
      {
        Font font = (Font) null;
        NativeMethods.NONCLIENTMETRICS pvParam = new NativeMethods.NONCLIENTMETRICS();
        if (UnsafeNativeMethods.SystemParametersInfo(41, pvParam.cbSize, pvParam, 0) && pvParam.lfMessageFont != null)
        {
          IntSecurity.ObjectFromWin32Handle.Assert();
          try
          {
            font = Font.FromLogFont((object) pvParam.lfMessageFont);
          }
          catch (Exception ex)
          {
            if (SystemFonts.IsCriticalFontException(ex))
              throw;
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
          if (font == null)
            font = SystemFonts.DefaultFont;
          else if (font.Unit != GraphicsUnit.Point)
            font = SystemFonts.FontInPoints(font);
        }
        font.SetSystemFontName(nameof (MessageBoxFont));
        return font;
      }
    }

    private static bool IsCriticalFontException(Exception ex)
    {
      switch (ex)
      {
        case ExternalException _:
        case ArgumentException _:
        case OutOfMemoryException _:
        case InvalidOperationException _:
        case NotImplementedException _:
          return false;
        default:
          return !(ex is FileNotFoundException);
      }
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for icon titles.</summary>
    /// <returns>A <see cref="T:System.Drawing.Font" /> that is used for icon titles.</returns>
    public static Font IconTitleFont
    {
      get
      {
        Font font = (Font) null;
        SafeNativeMethods.LOGFONT logfont = new SafeNativeMethods.LOGFONT();
        if (UnsafeNativeMethods.SystemParametersInfo(31, Marshal.SizeOf((object) logfont), logfont, 0) && logfont != null)
        {
          IntSecurity.ObjectFromWin32Handle.Assert();
          try
          {
            font = Font.FromLogFont((object) logfont);
          }
          catch (Exception ex)
          {
            if (SystemFonts.IsCriticalFontException(ex))
              throw;
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
          if (font == null)
            font = SystemFonts.DefaultFont;
          else if (font.Unit != GraphicsUnit.Point)
            font = SystemFonts.FontInPoints(font);
        }
        font.SetSystemFontName(nameof (IconTitleFont));
        return font;
      }
    }

    /// <summary>Gets the default font that applications can use for dialog boxes and forms.</summary>
    /// <returns>The default <see cref="T:System.Drawing.Font" /> of the system. The value returned will vary depending on the user's operating system and the local culture setting of their system.</returns>
    public static Font DefaultFont
    {
      get
      {
        Font font1 = (Font) null;
        bool flag = false;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major <= 4)
        {
          if ((UnsafeNativeMethods.GetSystemDefaultLCID() & 1023) == 17)
          {
            try
            {
              font1 = new Font("MS UI Gothic", 9f);
            }
            catch (Exception ex)
            {
              if (SystemFonts.IsCriticalFontException(ex))
                throw;
            }
          }
        }
        if (font1 == null)
          flag = (UnsafeNativeMethods.GetSystemDefaultLCID() & 1023) == 1;
        if (flag)
        {
          try
          {
            font1 = new Font("Tahoma", 8f);
          }
          catch (Exception ex)
          {
            if (SystemFonts.IsCriticalFontException(ex))
              throw;
          }
        }
        if (font1 == null)
        {
          IntPtr stockObject = UnsafeNativeMethods.GetStockObject(17);
          try
          {
            Font font2 = (Font) null;
            IntSecurity.ObjectFromWin32Handle.Assert();
            try
            {
              font2 = Font.FromHfont(stockObject);
            }
            finally
            {
              CodeAccessPermission.RevertAssert();
            }
            try
            {
              font1 = SystemFonts.FontInPoints(font2);
            }
            finally
            {
              font2.Dispose();
            }
          }
          catch (ArgumentException ex)
          {
          }
        }
        if (font1 == null)
        {
          try
          {
            font1 = new Font("Tahoma", 8f);
          }
          catch (ArgumentException ex)
          {
          }
        }
        if (font1 == null)
          font1 = new Font(FontFamily.GenericSansSerif, 8f);
        if (font1.Unit != GraphicsUnit.Point)
          font1 = SystemFonts.FontInPoints(font1);
        font1.SetSystemFontName(nameof (DefaultFont));
        return font1;
      }
    }

    /// <summary>Gets a font that applications can use for dialog boxes and forms.</summary>
    /// <returns>A <see cref="T:System.Drawing.Font" /> that can be used for dialog boxes and forms, depending on the operating system and local culture setting of the system.</returns>
    public static Font DialogFont
    {
      get
      {
        Font font = (Font) null;
        if ((UnsafeNativeMethods.GetSystemDefaultLCID() & 1023) == 17)
          font = SystemFonts.DefaultFont;
        else if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
        {
          font = SystemFonts.DefaultFont;
        }
        else
        {
          try
          {
            font = new Font("MS Shell Dlg 2", 8f);
          }
          catch (ArgumentException ex)
          {
          }
        }
        if (font == null)
          font = SystemFonts.DefaultFont;
        else if (font.Unit != GraphicsUnit.Point)
          font = SystemFonts.FontInPoints(font);
        font.SetSystemFontName(nameof (DialogFont));
        return font;
      }
    }

    private static Font FontInPoints(Font font) => new Font(font.FontFamily, font.SizeInPoints, font.Style, GraphicsUnit.Point, font.GdiCharSet, font.GdiVerticalFont);

    /// <summary>Returns a font object that corresponds to the specified system font name.</summary>
    /// <param name="systemFontName">The name of the system font you need a font object for.</param>
    /// <returns>A <see cref="T:System.Drawing.Font" /> if the specified name matches a value in <see cref="T:System.Drawing.SystemFonts" />; otherwise, <see langword="null" />.</returns>
    public static Font GetFontByName(string systemFontName)
    {
      if ("CaptionFont".Equals(systemFontName))
        return SystemFonts.CaptionFont;
      if ("DefaultFont".Equals(systemFontName))
        return SystemFonts.DefaultFont;
      if ("DialogFont".Equals(systemFontName))
        return SystemFonts.DialogFont;
      if ("IconTitleFont".Equals(systemFontName))
        return SystemFonts.IconTitleFont;
      if ("MenuFont".Equals(systemFontName))
        return SystemFonts.MenuFont;
      if ("MessageBoxFont".Equals(systemFontName))
        return SystemFonts.MessageBoxFont;
      if ("SmallCaptionFont".Equals(systemFontName))
        return SystemFonts.SmallCaptionFont;
      return "StatusFont".Equals(systemFontName) ? SystemFonts.StatusFont : (Font) null;
    }
  }
}
