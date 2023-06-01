// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.DbgUtil
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Drawing.Internal
{
  [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
  [EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
  [FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
  [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
  [UIPermission(SecurityAction.Assert, Unrestricted = true)]
  internal sealed class DbgUtil
  {
    public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
    public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
    public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
    public const int FORMAT_MESSAGE_DEFAULT = 4608;
    public static int gdipInitMaxFrameCount = 8;
    public static int gdiUseMaxFrameCount = 8;
    public static int finalizeMaxFrameCount = 5;

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetUserDefaultLCID();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int FormatMessage(
      int dwFlags,
      HandleRef lpSource,
      int dwMessageId,
      int dwLanguageId,
      StringBuilder lpBuffer,
      int nSize,
      HandleRef arguments);

    [Conditional("DEBUG")]
    public static void AssertFinalization(object obj, bool disposing)
    {
    }

    [Conditional("DEBUG")]
    public static void AssertWin32(bool expression, string message)
    {
    }

    [Conditional("DEBUG")]
    public static void AssertWin32(bool expression, string format, object arg1)
    {
    }

    [Conditional("DEBUG")]
    public static void AssertWin32(bool expression, string format, object arg1, object arg2)
    {
    }

    [Conditional("DEBUG")]
    public static void AssertWin32(
      bool expression,
      string format,
      object arg1,
      object arg2,
      object arg3)
    {
    }

    [Conditional("DEBUG")]
    public static void AssertWin32(
      bool expression,
      string format,
      object arg1,
      object arg2,
      object arg3,
      object arg4)
    {
    }

    [Conditional("DEBUG")]
    public static void AssertWin32(
      bool expression,
      string format,
      object arg1,
      object arg2,
      object arg3,
      object arg4,
      object arg5)
    {
    }

    [Conditional("DEBUG")]
    private static void AssertWin32Impl(bool expression, string format, object[] args)
    {
    }

    public static string GetLastErrorStr()
    {
      int maxValue = (int) byte.MaxValue;
      StringBuilder lpBuffer = new StringBuilder(maxValue);
      string empty = string.Empty;
      int dwMessageId = 0;
      string str;
      try
      {
        dwMessageId = Marshal.GetLastWin32Error();
        str = DbgUtil.FormatMessage(4608, new HandleRef((object) null, IntPtr.Zero), dwMessageId, DbgUtil.GetUserDefaultLCID(), lpBuffer, maxValue, new HandleRef((object) null, IntPtr.Zero)) != 0 ? lpBuffer.ToString() : "<error returned>";
      }
      catch (Exception ex)
      {
        if (DbgUtil.IsCriticalException(ex))
          throw;
        else
          str = ex.ToString();
      }
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "0x{0:x8} - {1}", new object[2]
      {
        (object) dwMessageId,
        (object) str
      });
    }

    private static bool IsCriticalException(Exception ex)
    {
      switch (ex)
      {
        case StackOverflowException _:
        case OutOfMemoryException _:
          return true;
        default:
          return ex is ThreadAbortException;
      }
    }

    public static string StackTrace => Environment.StackTrace;

    public static string StackFramesToStr(int maxFrameCount)
    {
      string empty = string.Empty;
      try
      {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        int index1;
        for (index1 = 0; index1 < stackTrace.FrameCount; ++index1)
        {
          StackFrame frame = stackTrace.GetFrame(index1);
          if (frame == null || frame.GetMethod().DeclaringType != typeof (DbgUtil))
            break;
        }
        maxFrameCount += index1;
        if (maxFrameCount > stackTrace.FrameCount)
          maxFrameCount = stackTrace.FrameCount;
        for (int index2 = index1; index2 < maxFrameCount; ++index2)
        {
          StackFrame frame = stackTrace.GetFrame(index2);
          if (frame != null)
          {
            MethodBase method = frame.GetMethod();
            if (!(method == (MethodBase) null))
            {
              string str1 = string.Empty;
              string str2 = frame.GetFileName();
              int num = str2 == null ? -1 : str2.LastIndexOf('\\');
              if (num != -1)
                str2 = str2.Substring(num + 1, str2.Length - num - 1);
              foreach (ParameterInfo parameter in method.GetParameters())
                str1 = str1 + parameter.ParameterType.Name + ", ";
              if (str1.Length > 0)
                str1 = str1.Substring(0, str1.Length - 2);
              empty += string.Format((IFormatProvider) CultureInfo.CurrentCulture, "at {0} {1}.{2}({3})\r\n", (object) str2, (object) method.DeclaringType, (object) method.Name, (object) str1);
            }
          }
        }
      }
      catch (Exception ex)
      {
        if (DbgUtil.IsCriticalException(ex))
          throw;
        else
          empty += ex.ToString();
      }
      return empty.ToString();
    }

    public static string StackFramesToStr() => DbgUtil.StackFramesToStr(DbgUtil.gdipInitMaxFrameCount);

    public static string StackTraceToStr(string message, int frameCount) => string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0}\r\nTop Stack Trace:\r\n{1}", new object[2]
    {
      (object) message,
      (object) DbgUtil.StackFramesToStr(frameCount)
    });

    public static string StackTraceToStr(string message) => DbgUtil.StackTraceToStr(message, DbgUtil.gdipInitMaxFrameCount);
  }
}
