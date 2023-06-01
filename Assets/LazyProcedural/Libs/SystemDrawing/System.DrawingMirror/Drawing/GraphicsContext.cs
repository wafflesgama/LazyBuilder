// Decompiled with JetBrains decompiler
// Type: System.Drawing.GraphicsContext
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Drawing2D;

namespace System.Drawing
{
  internal class GraphicsContext : IDisposable
  {
    private int contextState;
    private PointF transformOffset;
    private Region clipRegion;
    private GraphicsContext nextContext;
    private GraphicsContext prevContext;
    private bool isCumulative;

    private GraphicsContext()
    {
    }

    public GraphicsContext(Graphics g)
    {
      Matrix transform = g.Transform;
      if (!transform.IsIdentity)
      {
        float[] elements = transform.Elements;
        this.transformOffset.X = elements[4];
        this.transformOffset.Y = elements[5];
      }
      transform.Dispose();
      Region clip = g.Clip;
      if (clip.IsInfinite(g))
        clip.Dispose();
      else
        this.clipRegion = clip;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public void Dispose(bool disposing)
    {
      if (this.nextContext != null)
      {
        this.nextContext.Dispose();
        this.nextContext = (GraphicsContext) null;
      }
      if (this.clipRegion == null)
        return;
      this.clipRegion.Dispose();
      this.clipRegion = (Region) null;
    }

    public int State
    {
      get => this.contextState;
      set => this.contextState = value;
    }

    public PointF TransformOffset => this.transformOffset;

    public Region Clip => this.clipRegion;

    public GraphicsContext Next
    {
      get => this.nextContext;
      set => this.nextContext = value;
    }

    public GraphicsContext Previous
    {
      get => this.prevContext;
      set => this.prevContext = value;
    }

    public bool IsCumulative
    {
      get => this.isCumulative;
      set => this.isCumulative = value;
    }
  }
}
