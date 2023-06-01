// Decompiled with JetBrains decompiler
// Type: System.Drawing.StringDigitSubstitute
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing
{
  /// <summary>The <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration specifies how to substitute digits in a string according to a user's locale or language.</summary>
  public enum StringDigitSubstitute
  {
    /// <summary>Specifies a user-defined substitution scheme.</summary>
    User,
    /// <summary>Specifies to disable substitutions.</summary>
    None,
    /// <summary>Specifies substitution digits that correspond with the official national language of the user's locale.</summary>
    National,
    /// <summary>Specifies substitution digits that correspond with the user's native script or language, which may be different from the official national language of the user's locale.</summary>
    Traditional,
  }
}
