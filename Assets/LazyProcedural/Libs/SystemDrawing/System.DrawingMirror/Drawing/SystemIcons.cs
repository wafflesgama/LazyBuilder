// Decompiled with JetBrains decompiler
// Type: System.Drawing.SystemIcons
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>Each property of the <see cref="T:System.Drawing.SystemIcons" /> class is an <see cref="T:System.Drawing.Icon" /> object for Windows system-wide icons. This class cannot be inherited.</summary>
  public sealed class SystemIcons
  {
    private static Icon _application;
    private static Icon _asterisk;
    private static Icon _error;
    private static Icon _exclamation;
    private static Icon _hand;
    private static Icon _information;
    private static Icon _question;
    private static Icon _warning;
    private static Icon _winlogo;
    private static Icon _shield;

    private SystemIcons()
    {
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the default application icon (WIN32: IDI_APPLICATION).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the default application icon.</returns>
    public static Icon Application
    {
      get
      {
        if (SystemIcons._application == null)
          SystemIcons._application = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32512));
        return SystemIcons._application;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system asterisk icon (WIN32: IDI_ASTERISK).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system asterisk icon.</returns>
    public static Icon Asterisk
    {
      get
      {
        if (SystemIcons._asterisk == null)
          SystemIcons._asterisk = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32516));
        return SystemIcons._asterisk;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system error icon (WIN32: IDI_ERROR).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system error icon.</returns>
    public static Icon Error
    {
      get
      {
        if (SystemIcons._error == null)
          SystemIcons._error = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32513));
        return SystemIcons._error;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system exclamation icon (WIN32: IDI_EXCLAMATION).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system exclamation icon.</returns>
    public static Icon Exclamation
    {
      get
      {
        if (SystemIcons._exclamation == null)
          SystemIcons._exclamation = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32515));
        return SystemIcons._exclamation;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system hand icon (WIN32: IDI_HAND).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system hand icon.</returns>
    public static Icon Hand
    {
      get
      {
        if (SystemIcons._hand == null)
          SystemIcons._hand = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32513));
        return SystemIcons._hand;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system information icon (WIN32: IDI_INFORMATION).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system information icon.</returns>
    public static Icon Information
    {
      get
      {
        if (SystemIcons._information == null)
          SystemIcons._information = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32516));
        return SystemIcons._information;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system question icon (WIN32: IDI_QUESTION).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system question icon.</returns>
    public static Icon Question
    {
      get
      {
        if (SystemIcons._question == null)
          SystemIcons._question = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32514));
        return SystemIcons._question;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system warning icon (WIN32: IDI_WARNING).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system warning icon.</returns>
    public static Icon Warning
    {
      get
      {
        if (SystemIcons._warning == null)
          SystemIcons._warning = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32515));
        return SystemIcons._warning;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the Windows logo icon (WIN32: IDI_WINLOGO).</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the Windows logo icon.</returns>
    public static Icon WinLogo
    {
      get
      {
        if (SystemIcons._winlogo == null)
          SystemIcons._winlogo = new Icon(SafeNativeMethods.LoadIcon(NativeMethods.NullHandleRef, 32517));
        return SystemIcons._winlogo;
      }
    }

    /// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the shield icon.</summary>
    /// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the shield icon.</returns>
    public static Icon Shield
    {
      get
      {
        if (SystemIcons._shield == null)
        {
          try
          {
            if (Environment.OSVersion.Version.Major >= 6)
            {
              IntPtr zero = IntPtr.Zero;
              if (SafeNativeMethods.LoadIconWithScaleDown(NativeMethods.NullHandleRef, 32518, 32, 32, ref zero) == 0)
                SystemIcons._shield = new Icon(zero);
            }
          }
          catch
          {
          }
        }
        if (SystemIcons._shield == null)
          SystemIcons._shield = new Icon(typeof (SystemIcons), "ShieldIcon.ico");
        return SystemIcons._shield;
      }
    }
  }
}
