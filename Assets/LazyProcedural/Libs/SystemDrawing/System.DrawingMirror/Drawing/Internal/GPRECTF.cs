// Decompiled with JetBrains decompiler
// Type: System.Drawing.Internal.GPRECTF
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Internal
{
  internal struct GPRECTF
  {
    internal float X;
    internal float Y;
    internal float Width;
    internal float Height;

    internal GPRECTF(float x, float y, float width, float height)
    {
      this.X = x;
      this.Y = y;
      this.Width = width;
      this.Height = height;
    }

    internal GPRECTF(RectangleF rect)
    {
      this.X = rect.X;
      this.Y = rect.Y;
      this.Width = rect.Width;
      this.Height = rect.Height;
    }

    internal SizeF SizeF => new SizeF(this.Width, this.Height);

    internal RectangleF ToRectangleF() => new RectangleF(this.X, this.Y, this.Width, this.Height);
  }
}
