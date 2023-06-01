// Decompiled with JetBrains decompiler
// Type: System.Drawing.BufferedGraphicsManager
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.ConstrainedExecution;

namespace System.Drawing
{
  /// <summary>Provides access to the main buffered graphics context object for the application domain.</summary>
  public sealed class BufferedGraphicsManager
  {
    private static BufferedGraphicsContext bufferedGraphicsContext;

    private BufferedGraphicsManager()
    {
    }

    static BufferedGraphicsManager()
    {
      AppDomain.CurrentDomain.ProcessExit += new EventHandler(BufferedGraphicsManager.OnShutdown);
      AppDomain.CurrentDomain.DomainUnload += new EventHandler(BufferedGraphicsManager.OnShutdown);
      BufferedGraphicsManager.bufferedGraphicsContext = new BufferedGraphicsContext();
    }

    /// <summary>Gets the <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</summary>
    /// <returns>The <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</returns>
    public static BufferedGraphicsContext Current => BufferedGraphicsManager.bufferedGraphicsContext;

    [PrePrepareMethod]
    private static void OnShutdown(object sender, EventArgs e) => BufferedGraphicsManager.Current.Invalidate();
  }
}
