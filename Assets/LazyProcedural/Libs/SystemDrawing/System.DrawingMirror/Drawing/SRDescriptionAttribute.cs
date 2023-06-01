﻿// Decompiled with JetBrains decompiler
// Type: System.Drawing.SRDescriptionAttribute
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;

namespace System.Drawing
{
  [AttributeUsage(AttributeTargets.All)]
  internal sealed class SRDescriptionAttribute : DescriptionAttribute
  {
    private bool replaced;

    public SRDescriptionAttribute(string description)
      : base(description)
    {
    }

    public override string Description
    {
      get
      {
        if (!this.replaced)
        {
          this.replaced = true;
          this.DescriptionValue = SR.GetString(base.Description);
        }
        return base.Description;
      }
    }
  }
}
