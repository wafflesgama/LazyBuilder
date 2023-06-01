// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.GraphicsPath
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
  /// <summary>Represents a series of connected lines and curves. This class cannot be inherited.</summary>
  public sealed class GraphicsPath : MarshalByRefObject, ICloneable, IDisposable
  {
    internal IntPtr nativePath;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with a <see cref="P:System.Drawing.Drawing2D.GraphicsPath.FillMode" /> value of <see cref="F:System.Drawing.Drawing2D.FillMode.Alternate" />.</summary>
    public GraphicsPath()
      : this(FillMode.Alternate)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration.</summary>
    /// <param name="fillMode">The <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the interior of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> is filled.</param>
    public GraphicsPath(FillMode fillMode)
    {
      IntPtr path1 = IntPtr.Zero;
      int path2 = SafeNativeMethods.Gdip.GdipCreatePath((int) fillMode, out path1);
      if (path2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(path2);
      this.nativePath = path1;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> array with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.PointF" /> arrays.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
    /// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
    public GraphicsPath(PointF[] pts, byte[] types)
      : this(pts, types, FillMode.Alternate)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> array with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.PointF" /> arrays and with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration element.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
    /// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
    /// <param name="fillMode">A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</param>
    public GraphicsPath(PointF[] pts, byte[] types, FillMode fillMode)
    {
      if (pts == null)
        throw new ArgumentNullException(nameof (pts));
      IntPtr path = IntPtr.Zero;
      if (pts.Length != types.Length)
        throw SafeNativeMethods.Gdip.StatusException(2);
      int length = types.Length;
      IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
      IntPtr num = Marshal.AllocHGlobal(length);
      try
      {
        Marshal.Copy(types, 0, num, length);
        int path2 = SafeNativeMethods.Gdip.GdipCreatePath2(new HandleRef((object) null, memory), new HandleRef((object) null, num), length, (int) fillMode, out path);
        if (path2 != 0)
          throw SafeNativeMethods.Gdip.StatusException(path2);
      }
      finally
      {
        Marshal.FreeHGlobal(memory);
        Marshal.FreeHGlobal(num);
      }
      this.nativePath = path;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.Point" /> arrays.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
    /// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
    public GraphicsPath(Point[] pts, byte[] types)
      : this(pts, types, FillMode.Alternate)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> class with the specified <see cref="T:System.Drawing.Drawing2D.PathPointType" /> and <see cref="T:System.Drawing.Point" /> arrays and with the specified <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration element.</summary>
    /// <param name="pts">An array of <see cref="T:System.Drawing.Point" /> structures that defines the coordinates of the points that make up this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
    /// <param name="types">An array of <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration elements that specifies the type of each corresponding point in the <paramref name="pts" /> array.</param>
    /// <param name="fillMode">A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</param>
    public GraphicsPath(Point[] pts, byte[] types, FillMode fillMode)
    {
      if (pts == null)
        throw new ArgumentNullException(nameof (pts));
      IntPtr path = IntPtr.Zero;
      if (pts.Length != types.Length)
        throw SafeNativeMethods.Gdip.StatusException(2);
      int length = types.Length;
      IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(pts);
      IntPtr num = Marshal.AllocHGlobal(length);
      try
      {
        Marshal.Copy(types, 0, num, length);
        int path2I = SafeNativeMethods.Gdip.GdipCreatePath2I(new HandleRef((object) null, memory), new HandleRef((object) null, num), length, (int) fillMode, out path);
        if (path2I != 0)
          throw SafeNativeMethods.Gdip.StatusException(path2I);
      }
      finally
      {
        Marshal.FreeHGlobal(memory);
        Marshal.FreeHGlobal(num);
      }
      this.nativePath = path;
    }

    /// <summary>Creates an exact copy of this path.</summary>
    /// <returns>The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> this method creates, cast as an object.</returns>
    public object Clone()
    {
      IntPtr clonepath = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipClonePath(new HandleRef((object) this, this.nativePath), out clonepath);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new GraphicsPath(clonepath, 0);
    }

    private GraphicsPath(IntPtr nativePath, int extra) => this.nativePath = !(nativePath == IntPtr.Zero) ? nativePath : throw new ArgumentNullException(nameof (nativePath));

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.nativePath != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDeletePath(new HandleRef((object) this, this.nativePath));
      }
      catch (Exception ex)
      {
        if (!System.Drawing.ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativePath = IntPtr.Zero;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~GraphicsPath() => this.Dispose(false);

    /// <summary>Empties the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> and <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> arrays and sets the <see cref="T:System.Drawing.Drawing2D.FillMode" /> to <see cref="F:System.Drawing.Drawing2D.FillMode.Alternate" />.</summary>
    public void Reset()
    {
      int status = SafeNativeMethods.Gdip.GdipResetPath(new HandleRef((object) this, this.nativePath));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets or sets a <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that specifies how the interiors of shapes in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> are filled.</returns>
    public FillMode FillMode
    {
      get
      {
        int fillmode = 0;
        int pathFillMode = SafeNativeMethods.Gdip.GdipGetPathFillMode(new HandleRef((object) this, this.nativePath), out fillmode);
        if (pathFillMode != 0)
          throw SafeNativeMethods.Gdip.StatusException(pathFillMode);
        return (FillMode) fillmode;
      }
      set
      {
        int status = System.Drawing.ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 1) ? SafeNativeMethods.Gdip.GdipSetPathFillMode(new HandleRef((object) this, this.nativePath), (int) value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (FillMode));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    private PathData _GetPathData()
    {
      int num1 = Marshal.SizeOf(typeof (GPPOINTF));
      int pointCount = this.PointCount;
      PathData pathData1 = new PathData();
      pathData1.Types = new byte[pointCount];
      IntPtr num2 = Marshal.AllocHGlobal(3 * IntPtr.Size);
      IntPtr num3 = Marshal.AllocHGlobal(checked (num1 * pointCount));
      try
      {
        GCHandle gcHandle = GCHandle.Alloc((object) pathData1.Types, GCHandleType.Pinned);
        try
        {
          IntPtr structure = gcHandle.AddrOfPinnedObject();
          Marshal.StructureToPtr((object) pointCount, num2, false);
          Marshal.StructureToPtr((object) num3, (IntPtr) ((long) num2 + (long) IntPtr.Size), false);
          Marshal.StructureToPtr((object) structure, (IntPtr) ((long) num2 + (long) (2 * IntPtr.Size)), false);
          int pathData2 = SafeNativeMethods.Gdip.GdipGetPathData(new HandleRef((object) this, this.nativePath), num2);
          if (pathData2 != 0)
            throw SafeNativeMethods.Gdip.StatusException(pathData2);
          pathData1.Points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(num3, pointCount);
        }
        finally
        {
          gcHandle.Free();
        }
      }
      finally
      {
        Marshal.FreeHGlobal(num2);
        Marshal.FreeHGlobal(num3);
      }
      return pathData1;
    }

    /// <summary>Gets a <see cref="T:System.Drawing.Drawing2D.PathData" /> that encapsulates arrays of points (<paramref name="points" />) and types (<paramref name="types" />) for this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.PathData" /> that encapsulates arrays for both the points and types for this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
    public PathData PathData => this._GetPathData();

    /// <summary>Starts a new figure without closing the current figure. All subsequent points added to the path are added to this new figure.</summary>
    public void StartFigure()
    {
      int status = SafeNativeMethods.Gdip.GdipStartPathFigure(new HandleRef((object) this, this.nativePath));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Closes the current figure and starts a new figure. If the current figure contains a sequence of connected lines and curves, the method closes the loop by connecting a line from the endpoint to the starting point.</summary>
    public void CloseFigure()
    {
      int status = SafeNativeMethods.Gdip.GdipClosePathFigure(new HandleRef((object) this, this.nativePath));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Closes all open figures in this path and starts a new figure. It closes each open figure by connecting a line from its endpoint to its starting point.</summary>
    public void CloseAllFigures()
    {
      int status = SafeNativeMethods.Gdip.GdipClosePathFigures(new HandleRef((object) this, this.nativePath));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets a marker on this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    public void SetMarkers()
    {
      int status = SafeNativeMethods.Gdip.GdipSetPathMarker(new HandleRef((object) this, this.nativePath));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Clears all markers from this path.</summary>
    public void ClearMarkers()
    {
      int status = SafeNativeMethods.Gdip.GdipClearPathMarkers(new HandleRef((object) this, this.nativePath));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Reverses the order of points in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    public void Reverse()
    {
      int status = SafeNativeMethods.Gdip.GdipReversePath(new HandleRef((object) this, this.nativePath));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets the last point in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the last point in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
    public PointF GetLastPoint()
    {
      GPPOINTF lastPoint = new GPPOINTF();
      int pathLastPoint = SafeNativeMethods.Gdip.GdipGetPathLastPoint(new HandleRef((object) this, this.nativePath), lastPoint);
      if (pathLastPoint != 0)
        throw SafeNativeMethods.Gdip.StatusException(pathLastPoint);
      return lastPoint.ToPoint();
    }

    /// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(float x, float y) => this.IsVisible(new PointF(x, y), (Graphics) null);

    /// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="point">A <see cref="T:System.Drawing.PointF" /> that represents the point to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(PointF point) => this.IsVisible(point, (Graphics) null);

    /// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> in the visible clip region of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(float x, float y, Graphics graphics) => this.IsVisible(new PointF(x, y), graphics);

    /// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="pt">A <see cref="T:System.Drawing.PointF" /> that represents the point to test.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within this; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(PointF pt, Graphics graphics)
    {
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsVisiblePathPoint(new HandleRef((object) this, this.nativePath), pt.X, pt.Y, new HandleRef((object) graphics, graphics != null ? graphics.NativeGraphics : IntPtr.Zero), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(int x, int y) => this.IsVisible(new Point(x, y), (Graphics) null);

    /// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="point">A <see cref="T:System.Drawing.Point" /> that represents the point to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(Point point) => this.IsVisible(point, (Graphics) null);

    /// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />, using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(int x, int y, Graphics graphics) => this.IsVisible(new Point(x, y), graphics);

    /// <summary>Indicates whether the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="pt">A <see cref="T:System.Drawing.Point" /> that represents the point to test.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(Point pt, Graphics graphics)
    {
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsVisiblePathPointI(new HandleRef((object) this, this.nativePath), pt.X, pt.Y, new HandleRef((object) graphics, graphics != null ? graphics.NativeGraphics : IntPtr.Zero), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
    public bool IsOutlineVisible(float x, float y, Pen pen) => this.IsOutlineVisible(new PointF(x, y), pen, (Graphics) null);

    /// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
    /// <param name="point">A <see cref="T:System.Drawing.PointF" /> that specifies the location to test.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
    public bool IsOutlineVisible(PointF point, Pen pen) => this.IsOutlineVisible(point, pen, (Graphics) null);

    /// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
    public bool IsOutlineVisible(float x, float y, Pen pen, Graphics graphics) => this.IsOutlineVisible(new PointF(x, y), pen, graphics);

    /// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="pt">A <see cref="T:System.Drawing.PointF" /> that specifies the location to test.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
    public bool IsOutlineVisible(PointF pt, Pen pen, Graphics graphics)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsOutlineVisiblePathPoint(new HandleRef((object) this, this.nativePath), pt.X, pt.Y, new HandleRef((object) pen, pen.NativePen), new HandleRef((object) graphics, graphics != null ? graphics.NativeGraphics : IntPtr.Zero), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
    public bool IsOutlineVisible(int x, int y, Pen pen) => this.IsOutlineVisible(new Point(x, y), pen, (Graphics) null);

    /// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
    /// <param name="point">A <see cref="T:System.Drawing.Point" /> that specifies the location to test.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
    public bool IsOutlineVisible(Point point, Pen pen) => this.IsOutlineVisible(point, pen, (Graphics) null);

    /// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
    public bool IsOutlineVisible(int x, int y, Pen pen, Graphics graphics) => this.IsOutlineVisible(new Point(x, y), pen, graphics);

    /// <summary>Indicates whether the specified point is contained within (under) the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when drawn with the specified <see cref="T:System.Drawing.Pen" /> and using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the location to test.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> to test.</param>
    /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> for which to test visibility.</param>
    /// <returns>This method returns <see langword="true" /> if the specified point is contained within the outline of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> as drawn with the specified <see cref="T:System.Drawing.Pen" />; otherwise, <see langword="false" />.</returns>
    public bool IsOutlineVisible(Point pt, Pen pen, Graphics graphics)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsOutlineVisiblePathPointI(new HandleRef((object) this, this.nativePath), pt.X, pt.Y, new HandleRef((object) pen, pen.NativePen), new HandleRef((object) graphics, graphics != null ? graphics.NativeGraphics : IntPtr.Zero), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="pt1">A <see cref="T:System.Drawing.PointF" /> that represents the starting point of the line.</param>
    /// <param name="pt2">A <see cref="T:System.Drawing.PointF" /> that represents the endpoint of the line.</param>
    public void AddLine(PointF pt1, PointF pt2) => this.AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);

    /// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="x1">The x-coordinate of the starting point of the line.</param>
    /// <param name="y1">The y-coordinate of the starting point of the line.</param>
    /// <param name="x2">The x-coordinate of the endpoint of the line.</param>
    /// <param name="y2">The y-coordinate of the endpoint of the line.</param>
    public void AddLine(float x1, float y1, float x2, float y2)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathLine(new HandleRef((object) this, this.nativePath), x1, y1, x2, y2);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Appends a series of connected line segments to the end of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the line segments to add.</param>
    public void AddLines(PointF[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathLine2(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Appends a line segment to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="pt1">A <see cref="T:System.Drawing.Point" /> that represents the starting point of the line.</param>
    /// <param name="pt2">A <see cref="T:System.Drawing.Point" /> that represents the endpoint of the line.</param>
    public void AddLine(Point pt1, Point pt2) => this.AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);

    /// <summary>Appends a line segment to the current figure.</summary>
    /// <param name="x1">The x-coordinate of the starting point of the line.</param>
    /// <param name="y1">The y-coordinate of the starting point of the line.</param>
    /// <param name="x2">The x-coordinate of the endpoint of the line.</param>
    /// <param name="y2">The y-coordinate of the endpoint of the line.</param>
    public void AddLine(int x1, int y1, int x2, int y2)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathLineI(new HandleRef((object) this, this.nativePath), x1, y1, x2, y2);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Appends a series of connected line segments to the end of this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the line segments to add.</param>
    public void AddLines(Point[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathLine2I(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Appends an elliptical arc to the current figure.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangular bounds of the ellipse from which the arc is taken.</param>
    /// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
    /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
    public void AddArc(RectangleF rect, float startAngle, float sweepAngle) => this.AddArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);

    /// <summary>Appends an elliptical arc to the current figure.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
    /// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn.</param>
    /// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn.</param>
    /// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
    /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
    public void AddArc(
      float x,
      float y,
      float width,
      float height,
      float startAngle,
      float sweepAngle)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathArc(new HandleRef((object) this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Appends an elliptical arc to the current figure.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangular bounds of the ellipse from which the arc is taken.</param>
    /// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
    /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
    public void AddArc(Rectangle rect, float startAngle, float sweepAngle) => this.AddArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);

    /// <summary>Appends an elliptical arc to the current figure.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangular region that defines the ellipse from which the arc is drawn.</param>
    /// <param name="width">The width of the rectangular region that defines the ellipse from which the arc is drawn.</param>
    /// <param name="height">The height of the rectangular region that defines the ellipse from which the arc is drawn.</param>
    /// <param name="startAngle">The starting angle of the arc, measured in degrees clockwise from the x-axis.</param>
    /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the arc.</param>
    public void AddArc(int x, int y, int width, int height, float startAngle, float sweepAngle)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathArcI(new HandleRef((object) this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a cubic Bézier curve to the current figure.</summary>
    /// <param name="pt1">A <see cref="T:System.Drawing.PointF" /> that represents the starting point of the curve.</param>
    /// <param name="pt2">A <see cref="T:System.Drawing.PointF" /> that represents the first control point for the curve.</param>
    /// <param name="pt3">A <see cref="T:System.Drawing.PointF" /> that represents the second control point for the curve.</param>
    /// <param name="pt4">A <see cref="T:System.Drawing.PointF" /> that represents the endpoint of the curve.</param>
    public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4) => this.AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);

    /// <summary>Adds a cubic Bézier curve to the current figure.</summary>
    /// <param name="x1">The x-coordinate of the starting point of the curve.</param>
    /// <param name="y1">The y-coordinate of the starting point of the curve.</param>
    /// <param name="x2">The x-coordinate of the first control point for the curve.</param>
    /// <param name="y2">The y-coordinate of the first control point for the curve.</param>
    /// <param name="x3">The x-coordinate of the second control point for the curve.</param>
    /// <param name="y3">The y-coordinate of the second control point for the curve.</param>
    /// <param name="x4">The x-coordinate of the endpoint of the curve.</param>
    /// <param name="y4">The y-coordinate of the endpoint of the curve.</param>
    public void AddBezier(
      float x1,
      float y1,
      float x2,
      float y2,
      float x3,
      float y3,
      float x4,
      float y4)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathBezier(new HandleRef((object) this, this.nativePath), x1, y1, x2, y2, x3, y3, x4, y4);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curves.</param>
    public void AddBeziers(PointF[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathBeziers(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a cubic Bézier curve to the current figure.</summary>
    /// <param name="pt1">A <see cref="T:System.Drawing.Point" /> that represents the starting point of the curve.</param>
    /// <param name="pt2">A <see cref="T:System.Drawing.Point" /> that represents the first control point for the curve.</param>
    /// <param name="pt3">A <see cref="T:System.Drawing.Point" /> that represents the second control point for the curve.</param>
    /// <param name="pt4">A <see cref="T:System.Drawing.Point" /> that represents the endpoint of the curve.</param>
    public void AddBezier(Point pt1, Point pt2, Point pt3, Point pt4) => this.AddBezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);

    /// <summary>Adds a cubic Bézier curve to the current figure.</summary>
    /// <param name="x1">The x-coordinate of the starting point of the curve.</param>
    /// <param name="y1">The y-coordinate of the starting point of the curve.</param>
    /// <param name="x2">The x-coordinate of the first control point for the curve.</param>
    /// <param name="y2">The y-coordinate of the first control point for the curve.</param>
    /// <param name="x3">The x-coordinate of the second control point for the curve.</param>
    /// <param name="y3">The y-coordinate of the second control point for the curve.</param>
    /// <param name="x4">The x-coordinate of the endpoint of the curve.</param>
    /// <param name="y4">The y-coordinate of the endpoint of the curve.</param>
    public void AddBezier(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathBezierI(new HandleRef((object) this, this.nativePath), x1, y1, x2, y2, x3, y3, x4, y4);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a sequence of connected cubic Bézier curves to the current figure.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curves.</param>
    public void AddBeziers(params Point[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathBeziersI(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
    public void AddCurve(PointF[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathCurve(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a spline curve to the current figure.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
    /// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
    public void AddCurve(PointF[] points, float tension)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathCurve2(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length, tension);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a spline curve to the current figure.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
    /// <param name="offset">The index of the element in the <paramref name="points" /> array that is used as the first point in the curve.</param>
    /// <param name="numberOfSegments">The number of segments used to draw the curve. A segment can be thought of as a line connecting two points.</param>
    /// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
    public void AddCurve(PointF[] points, int offset, int numberOfSegments, float tension)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathCurve3(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length, offset, numberOfSegments, tension);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a spline curve to the current figure. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
    public void AddCurve(Point[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathCurveI(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a spline curve to the current figure.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
    /// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
    public void AddCurve(Point[] points, float tension)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathCurve2I(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length, tension);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a spline curve to the current figure.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
    /// <param name="offset">The index of the element in the <paramref name="points" /> array that is used as the first point in the curve.</param>
    /// <param name="numberOfSegments">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
    /// <param name="tension">A value that specifies the amount that the curve bends between control points. Values greater than 1 produce unpredictable results.</param>
    public void AddCurve(Point[] points, int offset, int numberOfSegments, float tension)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathCurve3I(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length, offset, numberOfSegments, tension);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
    public void AddClosedCurve(PointF[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathClosedCurve(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that represents the points that define the curve.</param>
    /// <param name="tension">A value between from 0 through 1 that specifies the amount that the curve bends between points, with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve.</param>
    public void AddClosedCurve(PointF[] points, float tension)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathClosedCurve2(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length, tension);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
    public void AddClosedCurve(Point[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathClosedCurveI(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a closed curve to this path. A cardinal spline curve is used because the curve travels through each of the points in the array.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that represents the points that define the curve.</param>
    /// <param name="tension">A value between from 0 through 1 that specifies the amount that the curve bends between points, with 0 being the smallest curve (sharpest corner) and 1 being the smoothest curve.</param>
    public void AddClosedCurve(Point[] points, float tension)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathClosedCurve2I(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length, tension);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a rectangle to this path.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle to add.</param>
    public void AddRectangle(RectangleF rect)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathRectangle(new HandleRef((object) this, this.nativePath), rect.X, rect.Y, rect.Width, rect.Height);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a series of rectangles to this path.</summary>
    /// <param name="rects">An array of <see cref="T:System.Drawing.RectangleF" /> structures that represents the rectangles to add.</param>
    public void AddRectangles(RectangleF[] rects)
    {
      IntPtr num = rects != null ? SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects) : throw new ArgumentNullException(nameof (rects));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathRectangles(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), rects.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a rectangle to this path.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle to add.</param>
    public void AddRectangle(Rectangle rect)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathRectangleI(new HandleRef((object) this, this.nativePath), rect.X, rect.Y, rect.Width, rect.Height);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a series of rectangles to this path.</summary>
    /// <param name="rects">An array of <see cref="T:System.Drawing.Rectangle" /> structures that represents the rectangles to add.</param>
    public void AddRectangles(Rectangle[] rects)
    {
      IntPtr num = rects != null ? SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects) : throw new ArgumentNullException(nameof (rects));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathRectanglesI(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), rects.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds an ellipse to the current path.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> that represents the bounding rectangle that defines the ellipse.</param>
    public void AddEllipse(RectangleF rect) => this.AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Adds an ellipse to the current path.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="y">The y-coordinate of the upper left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="width">The width of the bounding rectangle that defines the ellipse.</param>
    /// <param name="height">The height of the bounding rectangle that defines the ellipse.</param>
    public void AddEllipse(float x, float y, float width, float height)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathEllipse(new HandleRef((object) this, this.nativePath), x, y, width, height);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds an ellipse to the current path.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle that defines the ellipse.</param>
    public void AddEllipse(Rectangle rect) => this.AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Adds an ellipse to the current path.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="width">The width of the bounding rectangle that defines the ellipse.</param>
    /// <param name="height">The height of the bounding rectangle that defines the ellipse.</param>
    public void AddEllipse(int x, int y, int width, int height)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathEllipseI(new HandleRef((object) this, this.nativePath), x, y, width, height);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds the outline of a pie shape to this path.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
    /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
    public void AddPie(Rectangle rect, float startAngle, float sweepAngle) => this.AddPie(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);

    /// <summary>Adds the outline of a pie shape to this path.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
    /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
    public void AddPie(
      float x,
      float y,
      float width,
      float height,
      float startAngle,
      float sweepAngle)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathPie(new HandleRef((object) this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds the outline of a pie shape to this path.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="width">The width of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="height">The height of the bounding rectangle that defines the ellipse from which the pie is drawn.</param>
    /// <param name="startAngle">The starting angle for the pie section, measured in degrees clockwise from the x-axis.</param>
    /// <param name="sweepAngle">The angle between <paramref name="startAngle" /> and the end of the pie section, measured in degrees clockwise from <paramref name="startAngle" />.</param>
    public void AddPie(int x, int y, int width, int height, float startAngle, float sweepAngle)
    {
      int status = SafeNativeMethods.Gdip.GdipAddPathPieI(new HandleRef((object) this, this.nativePath), x, y, width, height, startAngle, sweepAngle);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a polygon to this path.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.PointF" /> structures that defines the polygon to add.</param>
    public void AddPolygon(PointF[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathPolygon(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Adds a polygon to this path.</summary>
    /// <param name="points">An array of <see cref="T:System.Drawing.Point" /> structures that defines the polygon to add.</param>
    public void AddPolygon(Point[] points)
    {
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipAddPathPolygonI(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num), points.Length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Appends the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to this path.</summary>
    /// <param name="addingPath">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to add.</param>
    /// <param name="connect">A Boolean value that specifies whether the first figure in the added path is part of the last figure in this path. A value of <see langword="true" /> specifies that (if possible) the first figure in the added path is part of the last figure in this path. A value of <see langword="false" /> specifies that the first figure in the added path is separate from the last figure in this path.</param>
    public void AddPath(GraphicsPath addingPath, bool connect)
    {
      if (addingPath == null)
        throw new ArgumentNullException(nameof (addingPath));
      int status = SafeNativeMethods.Gdip.GdipAddPathPath(new HandleRef((object) this, this.nativePath), new HandleRef((object) addingPath, addingPath.nativePath), connect);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a text string to this path.</summary>
    /// <param name="s">The <see cref="T:System.String" /> to add.</param>
    /// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
    /// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
    /// <param name="emSize">The height of the em square box that bounds the character.</param>
    /// <param name="origin">A <see cref="T:System.Drawing.PointF" /> that represents the point where the text starts.</param>
    /// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
    public void AddString(
      string s,
      FontFamily family,
      int style,
      float emSize,
      PointF origin,
      StringFormat format)
    {
      GPRECTF layoutRect = new GPRECTF(origin.X, origin.Y, 0.0f, 0.0f);
      int status = SafeNativeMethods.Gdip.GdipAddPathString(new HandleRef((object) this, this.nativePath), s, s.Length, new HandleRef((object) family, family != null ? family.NativeFamily : IntPtr.Zero), style, emSize, ref layoutRect, new HandleRef((object) format, format != null ? format.nativeFormat : IntPtr.Zero));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a text string to this path.</summary>
    /// <param name="s">The <see cref="T:System.String" /> to add.</param>
    /// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
    /// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
    /// <param name="emSize">The height of the em square box that bounds the character.</param>
    /// <param name="origin">A <see cref="T:System.Drawing.Point" /> that represents the point where the text starts.</param>
    /// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
    public void AddString(
      string s,
      FontFamily family,
      int style,
      float emSize,
      Point origin,
      StringFormat format)
    {
      GPRECT layoutRect = new GPRECT(origin.X, origin.Y, 0, 0);
      int status = SafeNativeMethods.Gdip.GdipAddPathStringI(new HandleRef((object) this, this.nativePath), s, s.Length, new HandleRef((object) family, family != null ? family.NativeFamily : IntPtr.Zero), style, emSize, ref layoutRect, new HandleRef((object) format, format != null ? format.nativeFormat : IntPtr.Zero));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a text string to this path.</summary>
    /// <param name="s">The <see cref="T:System.String" /> to add.</param>
    /// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
    /// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
    /// <param name="emSize">The height of the em square box that bounds the character.</param>
    /// <param name="layoutRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the text.</param>
    /// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
    public void AddString(
      string s,
      FontFamily family,
      int style,
      float emSize,
      RectangleF layoutRect,
      StringFormat format)
    {
      GPRECTF layoutRect1 = new GPRECTF(layoutRect);
      int status = SafeNativeMethods.Gdip.GdipAddPathString(new HandleRef((object) this, this.nativePath), s, s.Length, new HandleRef((object) family, family != null ? family.NativeFamily : IntPtr.Zero), style, emSize, ref layoutRect1, new HandleRef((object) format, format != null ? format.nativeFormat : IntPtr.Zero));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds a text string to this path.</summary>
    /// <param name="s">The <see cref="T:System.String" /> to add.</param>
    /// <param name="family">A <see cref="T:System.Drawing.FontFamily" /> that represents the name of the font with which the test is drawn.</param>
    /// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> enumeration that represents style information about the text (bold, italic, and so on). This must be cast as an integer (see the example code later in this section).</param>
    /// <param name="emSize">The height of the em square box that bounds the character.</param>
    /// <param name="layoutRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the text.</param>
    /// <param name="format">A <see cref="T:System.Drawing.StringFormat" /> that specifies text formatting information, such as line spacing and alignment.</param>
    public void AddString(
      string s,
      FontFamily family,
      int style,
      float emSize,
      Rectangle layoutRect,
      StringFormat format)
    {
      GPRECT layoutRect1 = new GPRECT(layoutRect);
      int status = SafeNativeMethods.Gdip.GdipAddPathStringI(new HandleRef((object) this, this.nativePath), s, s.Length, new HandleRef((object) family, family != null ? family.NativeFamily : IntPtr.Zero), style, emSize, ref layoutRect1, new HandleRef((object) format, format != null ? format.nativeFormat : IntPtr.Zero));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies a transform matrix to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the transformation to apply.</param>
    public void Transform(Matrix matrix)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      if (matrix.nativeMatrix == IntPtr.Zero)
        return;
      int status = SafeNativeMethods.Gdip.GdipTransformPath(new HandleRef((object) this, this.nativePath), new HandleRef((object) matrix, matrix.nativeMatrix));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
    public RectangleF GetBounds() => this.GetBounds((Matrix) null);

    /// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when this path is transformed by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied to this path before the bounding rectangle is calculated. This path is not permanently transformed; the transformation is used only during the process of calculating the bounding rectangle.</param>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
    public RectangleF GetBounds(Matrix matrix) => this.GetBounds(matrix, (Pen) null);

    /// <summary>Returns a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> when the current path is transformed by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> and drawn with the specified <see cref="T:System.Drawing.Pen" />.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transformation to be applied to this path before the bounding rectangle is calculated. This path is not permanently transformed; the transformation is used only during the process of calculating the bounding rectangle.</param>
    /// <param name="pen">The <see cref="T:System.Drawing.Pen" /> with which to draw the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</param>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> that represents a rectangle that bounds this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</returns>
    public RectangleF GetBounds(Matrix matrix, Pen pen)
    {
      GPRECTF gprectf = new GPRECTF();
      IntPtr handle1 = IntPtr.Zero;
      IntPtr handle2 = IntPtr.Zero;
      if (matrix != null)
        handle1 = matrix.nativeMatrix;
      if (pen != null)
        handle2 = pen.NativePen;
      int pathWorldBounds = SafeNativeMethods.Gdip.GdipGetPathWorldBounds(new HandleRef((object) this, this.nativePath), ref gprectf, new HandleRef((object) matrix, handle1), new HandleRef((object) pen, handle2));
      if (pathWorldBounds != 0)
        throw SafeNativeMethods.Gdip.StatusException(pathWorldBounds);
      return gprectf.ToRectangleF();
    }

    /// <summary>Converts each curve in this path into a sequence of connected line segments.</summary>
    public void Flatten() => this.Flatten((Matrix) null);

    /// <summary>Applies the specified transform and then converts each curve in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.</summary>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> before flattening.</param>
    public void Flatten(Matrix matrix) => this.Flatten(matrix, 0.25f);

    /// <summary>Converts each curve in this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into a sequence of connected line segments.</summary>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> before flattening.</param>
    /// <param name="flatness">Specifies the maximum permitted error between the curve and its flattened approximation. A value of 0.25 is the default. Reducing the flatness value will increase the number of line segments in the approximation.</param>
    public void Flatten(Matrix matrix, float flatness)
    {
      int status = SafeNativeMethods.Gdip.GdipFlattenPath(new HandleRef((object) this, this.nativePath), new HandleRef((object) matrix, matrix == null ? IntPtr.Zero : matrix.nativeMatrix), flatness);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adds an additional outline to the path.</summary>
    /// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
    public void Widen(Pen pen)
    {
      float flatness = 0.6666667f;
      this.Widen(pen, (Matrix) null, flatness);
    }

    /// <summary>Adds an additional outline to the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path before widening.</param>
    public void Widen(Pen pen, Matrix matrix)
    {
      float flatness = 0.6666667f;
      this.Widen(pen, matrix, flatness);
    }

    /// <summary>Replaces this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> with curves that enclose the area that is filled when this path is drawn by the specified pen.</summary>
    /// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that specifies the width between the original outline of the path and the new outline this method creates.</param>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a transform to apply to the path before widening.</param>
    /// <param name="flatness">A value that specifies the flatness for curves.</param>
    public void Widen(Pen pen, Matrix matrix, float flatness)
    {
      IntPtr handle = matrix != null ? matrix.nativeMatrix : IntPtr.Zero;
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      int count;
      SafeNativeMethods.Gdip.GdipGetPointCount(new HandleRef((object) this, this.nativePath), out count);
      if (count == 0)
        return;
      int status = SafeNativeMethods.Gdip.GdipWidenPath(new HandleRef((object) this, this.nativePath), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) matrix, handle), flatness);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
    /// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
    public void Warp(PointF[] destPoints, RectangleF srcRect) => this.Warp(destPoints, srcRect, (Matrix) null);

    /// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
    /// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
    public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix) => this.Warp(destPoints, srcRect, matrix, WarpMode.Perspective);

    /// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that defines a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
    /// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
    /// <param name="warpMode">A <see cref="T:System.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this warp operation uses perspective or bilinear mode.</param>
    public void Warp(PointF[] destPoints, RectangleF srcRect, Matrix matrix, WarpMode warpMode) => this.Warp(destPoints, srcRect, matrix, warpMode, 0.25f);

    /// <summary>Applies a warp transform, defined by a rectangle and a parallelogram, to this <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="destPoints">An array of <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram to which the rectangle defined by <paramref name="srcRect" /> is transformed. The array can contain either three or four elements. If the array contains three elements, the lower-right corner of the parallelogram is implied by the first three points.</param>
    /// <param name="srcRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that is transformed to the parallelogram defined by <paramref name="destPoints" />.</param>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that specifies a geometric transform to apply to the path.</param>
    /// <param name="warpMode">A <see cref="T:System.Drawing.Drawing2D.WarpMode" /> enumeration that specifies whether this warp operation uses perspective or bilinear mode.</param>
    /// <param name="flatness">A value from 0 through 1 that specifies how flat the resulting path is. For more information, see the <see cref="M:System.Drawing.Drawing2D.GraphicsPath.Flatten" /> methods.</param>
    public void Warp(
      PointF[] destPoints,
      RectangleF srcRect,
      Matrix matrix,
      WarpMode warpMode,
      float flatness)
    {
      IntPtr num = destPoints != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints) : throw new ArgumentNullException(nameof (destPoints));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipWarpPath(new HandleRef((object) this, this.nativePath), new HandleRef((object) matrix, matrix == null ? IntPtr.Zero : matrix.nativeMatrix), new HandleRef((object) null, num), destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, warpMode, flatness);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Gets the number of elements in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.</summary>
    /// <returns>An integer that specifies the number of elements in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> or the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> array.</returns>
    public int PointCount
    {
      get
      {
        int count = 0;
        int pointCount = SafeNativeMethods.Gdip.GdipGetPointCount(new HandleRef((object) this, this.nativePath), out count);
        if (pointCount != 0)
          throw SafeNativeMethods.Gdip.StatusException(pointCount);
        return count;
      }
    }

    /// <summary>Gets the types of the corresponding points in the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> array.</summary>
    /// <returns>An array of bytes that specifies the types of the corresponding points in the path.</returns>
    public byte[] PathTypes
    {
      get
      {
        int pointCount = this.PointCount;
        byte[] types = new byte[pointCount];
        int pathTypes = SafeNativeMethods.Gdip.GdipGetPathTypes(new HandleRef((object) this, this.nativePath), types, pointCount);
        if (pathTypes != 0)
          throw SafeNativeMethods.Gdip.StatusException(pathTypes);
        return types;
      }
    }

    /// <summary>Gets the points in the path.</summary>
    /// <returns>An array of <see cref="T:System.Drawing.PointF" /> objects that represent the path.</returns>
    public PointF[] PathPoints
    {
      get
      {
        int pointCount = this.PointCount;
        int num1 = Marshal.SizeOf(typeof (GPPOINTF));
        IntPtr num2 = Marshal.AllocHGlobal(checked (pointCount * num1));
        try
        {
          int pathPoints = SafeNativeMethods.Gdip.GdipGetPathPoints(new HandleRef((object) this, this.nativePath), new HandleRef((object) null, num2), pointCount);
          if (pathPoints != 0)
            throw SafeNativeMethods.Gdip.StatusException(pathPoints);
          return SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(num2, pointCount);
        }
        finally
        {
          Marshal.FreeHGlobal(num2);
        }
      }
    }
  }
}
