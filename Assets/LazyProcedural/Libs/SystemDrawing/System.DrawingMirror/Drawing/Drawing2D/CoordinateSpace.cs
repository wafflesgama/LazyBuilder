// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.CoordinateSpace
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Drawing2D
{
  /// <summary>Specifies the system to use when evaluating coordinates.</summary>
  public enum CoordinateSpace
  {
    /// <summary>Specifies that coordinates are in the world coordinate context. World coordinates are used in a nonphysical environment, such as a modeling environment.</summary>
    World,
    /// <summary>Specifies that coordinates are in the page coordinate context. Their units are defined by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, and must be one of the elements of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration.</summary>
    Page,
    /// <summary>Specifies that coordinates are in the device coordinate context. On a computer screen the device coordinates are usually measured in pixels.</summary>
    Device,
  }
}
