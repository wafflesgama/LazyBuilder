// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PrintEventArgs
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;

namespace System.Drawing.Printing
{
  /// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> and <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> events.</summary>
  public class PrintEventArgs : CancelEventArgs
  {
    private PrintAction printAction;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintEventArgs" /> class.</summary>
    public PrintEventArgs()
    {
    }

    internal PrintEventArgs(PrintAction action) => this.printAction = action;

    /// <summary>Returns <see cref="F:System.Drawing.Printing.PrintAction.PrintToFile" /> in all cases.</summary>
    /// <returns>
    /// <see cref="F:System.Drawing.Printing.PrintAction.PrintToFile" /> in all cases.</returns>
    public PrintAction PrintAction => this.printAction;
  }
}
