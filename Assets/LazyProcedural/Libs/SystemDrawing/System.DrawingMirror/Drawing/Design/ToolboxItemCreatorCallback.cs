// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.ToolboxItemCreatorCallback
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Design
{
  /// <summary>Provides a callback mechanism that can create a <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
  /// <param name="serializedObject">The object which contains the data to create a <see cref="T:System.Drawing.Design.ToolboxItem" /> for.</param>
  /// <param name="format">The name of the clipboard data format to create a <see cref="T:System.Drawing.Design.ToolboxItem" /> for.</param>
  /// <returns>The deserialized <see cref="T:System.Drawing.Design.ToolboxItem" /> object specified by <paramref name="serializedObject" />.</returns>
  public delegate ToolboxItem ToolboxItemCreatorCallback(object serializedObject, string format);
}
