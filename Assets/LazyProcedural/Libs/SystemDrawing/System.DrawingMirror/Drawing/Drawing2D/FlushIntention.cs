// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.FlushIntention
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies whether commands in the graphics stack are terminated (flushed) immediately or executed as soon as possible.</summary>
  public enum FlushIntention
  {
    /// <summary>Specifies that the stack of all graphics operations is flushed immediately.</summary>
    Flush,
    /// <summary>Specifies that all graphics operations on the stack are executed as soon as possible. This synchronizes the graphics state.</summary>
    Sync,
  }
}
