// Decompiled with JetBrains decompiler
// Type: System.Drawing.Region
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Drawing2D;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing
{
  /// <summary>Describes the interior of a graphics shape composed of rectangles and paths. This class cannot be inherited.</summary>
  public sealed class Region : MarshalByRefObject, IDisposable
  {
    internal IntPtr nativeRegion;

    /// <summary>Initializes a new <see cref="T:System.Drawing.Region" />.</summary>
    public Region()
    {
      IntPtr region1 = IntPtr.Zero;
      int region2 = SafeNativeMethods.Gdip.GdipCreateRegion(out region1);
      if (region2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(region2);
      this.SetNativeRegion(region1);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.RectangleF" /> structure that defines the interior of the new <see cref="T:System.Drawing.Region" />.</param>
    public Region(RectangleF rect)
    {
      IntPtr region = IntPtr.Zero;
      GPRECTF gprectf = rect.ToGPRECTF();
      int regionRect = SafeNativeMethods.Gdip.GdipCreateRegionRect(ref gprectf, out region);
      if (regionRect != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionRect);
      this.SetNativeRegion(region);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that defines the interior of the new <see cref="T:System.Drawing.Region" />.</param>
    public Region(Rectangle rect)
    {
      IntPtr region = IntPtr.Zero;
      GPRECT gprect = new GPRECT(rect);
      int regionRectI = SafeNativeMethods.Gdip.GdipCreateRegionRectI(ref gprect, out region);
      if (regionRectI != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionRectI);
      this.SetNativeRegion(region);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="path">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that defines the new <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is <see langword="null" />.</exception>
    public Region(GraphicsPath path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      IntPtr region = IntPtr.Zero;
      int regionPath = SafeNativeMethods.Gdip.GdipCreateRegionPath(new HandleRef((object) path, path.nativePath), out region);
      if (regionPath != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionPath);
      this.SetNativeRegion(region);
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from the specified data.</summary>
    /// <param name="rgnData">A <see cref="T:System.Drawing.Drawing2D.RegionData" /> that defines the interior of the new <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgnData" /> is <see langword="null" />.</exception>
    public Region(RegionData rgnData)
    {
      if (rgnData == null)
        throw new ArgumentNullException(nameof (rgnData));
      IntPtr region = IntPtr.Zero;
      int regionRgnData = SafeNativeMethods.Gdip.GdipCreateRegionRgnData(rgnData.Data, rgnData.Data.Length, out region);
      if (regionRgnData != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionRgnData);
      this.SetNativeRegion(region);
    }

    internal Region(IntPtr nativeRegion) => this.SetNativeRegion(nativeRegion);

    /// <summary>Initializes a new <see cref="T:System.Drawing.Region" /> from a handle to the specified existing GDI region.</summary>
    /// <param name="hrgn">A handle to an existing <see cref="T:System.Drawing.Region" />.</param>
    /// <returns>The new <see cref="T:System.Drawing.Region" />.</returns>
    public static Region FromHrgn(IntPtr hrgn)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr region = IntPtr.Zero;
      int regionHrgn = SafeNativeMethods.Gdip.GdipCreateRegionHrgn(new HandleRef((object) null, hrgn), out region);
      if (regionHrgn != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionHrgn);
      return new Region(region);
    }

    private void SetNativeRegion(IntPtr nativeRegion) => this.nativeRegion = !(nativeRegion == IntPtr.Zero) ? nativeRegion : throw new ArgumentNullException(nameof (nativeRegion));

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Region" />.</summary>
    /// <returns>The <see cref="T:System.Drawing.Region" /> that this method creates.</returns>
    public Region Clone()
    {
      IntPtr cloneregion = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneRegion(new HandleRef((object) this, this.nativeRegion), out cloneregion);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return new Region(cloneregion);
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Region" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.nativeRegion != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDeleteRegion(new HandleRef((object) this, this.nativeRegion));
      }
      catch (Exception ex)
      {
        if (!ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeRegion = IntPtr.Zero;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~Region() => this.Dispose(false);

    /// <summary>Initializes this <see cref="T:System.Drawing.Region" /> object to an infinite interior.</summary>
    public void MakeInfinite()
    {
      int status = SafeNativeMethods.Gdip.GdipSetInfinite(new HandleRef((object) this, this.nativeRegion));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Initializes this <see cref="T:System.Drawing.Region" /> to an empty interior.</summary>
    public void MakeEmpty()
    {
      int status = SafeNativeMethods.Gdip.GdipSetEmpty(new HandleRef((object) this, this.nativeRegion));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to intersect with this <see cref="T:System.Drawing.Region" />.</param>
    public void Intersect(RectangleF rect)
    {
      GPRECTF gprectf = rect.ToGPRECTF();
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef((object) this, this.nativeRegion), ref gprectf, CombineMode.Intersect);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to intersect with this <see cref="T:System.Drawing.Region" />.</param>
    public void Intersect(Rectangle rect)
    {
      GPRECT gprect = new GPRECT(rect);
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef((object) this, this.nativeRegion), ref gprect, CombineMode.Intersect);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to intersect with this <see cref="T:System.Drawing.Region" />.</param>
    public void Intersect(GraphicsPath path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      int status = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) path, path.nativePath), CombineMode.Intersect);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the intersection of itself with the specified <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="region">The <see cref="T:System.Drawing.Region" /> to intersect with this <see cref="T:System.Drawing.Region" />.</param>
    public void Intersect(Region region)
    {
      int status = region != null ? SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) region, region.nativeRegion), CombineMode.Intersect) : throw new ArgumentNullException(nameof (region));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Releases the handle of the <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="regionHandle">The handle to the <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="regionHandle" /> is <see langword="null" />.</exception>
    public void ReleaseHrgn(IntPtr regionHandle)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      if (regionHandle == IntPtr.Zero)
        throw new ArgumentNullException(nameof (regionHandle));
      SafeNativeMethods.IntDeleteObject(new HandleRef((object) this, regionHandle));
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to unite with this <see cref="T:System.Drawing.Region" />.</param>
    public void Union(RectangleF rect)
    {
      GPRECTF gprectf = new GPRECTF(rect);
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef((object) this, this.nativeRegion), ref gprectf, CombineMode.Union);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to unite with this <see cref="T:System.Drawing.Region" />.</param>
    public void Union(Rectangle rect)
    {
      GPRECT gprect = new GPRECT(rect);
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef((object) this, this.nativeRegion), ref gprect, CombineMode.Union);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to unite with this <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is <see langword="null" />.</exception>
    public void Union(GraphicsPath path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      int status = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) path, path.nativePath), CombineMode.Union);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union of itself and the specified <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="region">The <see cref="T:System.Drawing.Region" /> to unite with this <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="region" /> is <see langword="null" />.</exception>
    public void Union(Region region)
    {
      int status = region != null ? SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) region, region.nativeRegion), CombineMode.Union) : throw new ArgumentNullException(nameof (region));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to <see cref="M:System.Drawing.Region.Xor(System.Drawing.Drawing2D.GraphicsPath)" /> with this <see cref="T:System.Drawing.Region" />.</param>
    public void Xor(RectangleF rect)
    {
      GPRECTF gprectf = new GPRECTF(rect);
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef((object) this, this.nativeRegion), ref gprectf, CombineMode.Xor);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />.</param>
    public void Xor(Rectangle rect)
    {
      GPRECT gprect = new GPRECT(rect);
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef((object) this, this.nativeRegion), ref gprect, CombineMode.Xor);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is <see langword="null" />.</exception>
    public void Xor(GraphicsPath path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      int status = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) path, path.nativePath), CombineMode.Xor);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to the union minus the intersection of itself with the specified <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="region">The <see cref="T:System.Drawing.Region" /> to <see cref="Overload:System.Drawing.Region.Xor" /> with this <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="region" /> is <see langword="null" />.</exception>
    public void Xor(Region region)
    {
      int status = region != null ? SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) region, region.nativeRegion), CombineMode.Xor) : throw new ArgumentNullException(nameof (region));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to exclude from this <see cref="T:System.Drawing.Region" />.</param>
    public void Exclude(RectangleF rect)
    {
      GPRECTF gprectf = new GPRECTF(rect);
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef((object) this, this.nativeRegion), ref gprectf, CombineMode.Exclude);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to exclude from this <see cref="T:System.Drawing.Region" />.</param>
    public void Exclude(Rectangle rect)
    {
      GPRECT gprect = new GPRECT(rect);
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef((object) this, this.nativeRegion), ref gprect, CombineMode.Exclude);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to exclude from this <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is <see langword="null" />.</exception>
    public void Exclude(GraphicsPath path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      int status = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) path, path.nativePath), CombineMode.Exclude);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain only the portion of its interior that does not intersect with the specified <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="region">The <see cref="T:System.Drawing.Region" /> to exclude from this <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="region" /> is <see langword="null" />.</exception>
    public void Exclude(Region region)
    {
      int status = region != null ? SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) region, region.nativeRegion), CombineMode.Exclude) : throw new ArgumentNullException(nameof (region));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to complement this <see cref="T:System.Drawing.Region" />.</param>
    public void Complement(RectangleF rect)
    {
      GPRECTF gprectf = rect.ToGPRECTF();
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRect(new HandleRef((object) this, this.nativeRegion), ref gprectf, CombineMode.Complement);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to complement this <see cref="T:System.Drawing.Region" />.</param>
    public void Complement(Rectangle rect)
    {
      GPRECT gprect = new GPRECT(rect);
      int status = SafeNativeMethods.Gdip.GdipCombineRegionRectI(new HandleRef((object) this, this.nativeRegion), ref gprect, CombineMode.Complement);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to complement this <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is <see langword="null" />.</exception>
    public void Complement(GraphicsPath path)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      int status = SafeNativeMethods.Gdip.GdipCombineRegionPath(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) path, path.nativePath), CombineMode.Complement);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates this <see cref="T:System.Drawing.Region" /> to contain the portion of the specified <see cref="T:System.Drawing.Region" /> that does not intersect with this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="region">The <see cref="T:System.Drawing.Region" /> object to complement this <see cref="T:System.Drawing.Region" /> object.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="region" /> is <see langword="null" />.</exception>
    public void Complement(Region region)
    {
      int status = region != null ? SafeNativeMethods.Gdip.GdipCombineRegionRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) region, region.nativeRegion), CombineMode.Complement) : throw new ArgumentNullException(nameof (region));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Offsets the coordinates of this <see cref="T:System.Drawing.Region" /> by the specified amount.</summary>
    /// <param name="dx">The amount to offset this <see cref="T:System.Drawing.Region" /> horizontally.</param>
    /// <param name="dy">The amount to offset this <see cref="T:System.Drawing.Region" /> vertically.</param>
    public void Translate(float dx, float dy)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslateRegion(new HandleRef((object) this, this.nativeRegion), dx, dy);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Offsets the coordinates of this <see cref="T:System.Drawing.Region" /> by the specified amount.</summary>
    /// <param name="dx">The amount to offset this <see cref="T:System.Drawing.Region" /> horizontally.</param>
    /// <param name="dy">The amount to offset this <see cref="T:System.Drawing.Region" /> vertically.</param>
    public void Translate(int dx, int dy)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslateRegionI(new HandleRef((object) this, this.nativeRegion), dx, dy);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Transforms this <see cref="T:System.Drawing.Region" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to transform this <see cref="T:System.Drawing.Region" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="matrix" /> is <see langword="null" />.</exception>
    public void Transform(Matrix matrix)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int status = SafeNativeMethods.Gdip.GdipTransformRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) matrix, matrix.nativeMatrix));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets a <see cref="T:System.Drawing.RectangleF" /> structure that represents a rectangle that bounds this <see cref="T:System.Drawing.Region" /> on the drawing surface of a <see cref="T:System.Drawing.Graphics" /> object.</summary>
    /// <param name="g">The <see cref="T:System.Drawing.Graphics" /> on which this <see cref="T:System.Drawing.Region" /> is drawn.</param>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle for this <see cref="T:System.Drawing.Region" /> on the specified drawing surface.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="g" /> is <see langword="null" />.</exception>
    public RectangleF GetBounds(Graphics g)
    {
      if (g == null)
        throw new ArgumentNullException(nameof (g));
      GPRECTF gprectf = new GPRECTF();
      int regionBounds = SafeNativeMethods.Gdip.GdipGetRegionBounds(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) g, g.NativeGraphics), ref gprectf);
      if (regionBounds != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionBounds);
      return gprectf.ToRectangleF();
    }

    /// <summary>Returns a Windows handle to this <see cref="T:System.Drawing.Region" /> in the specified graphics context.</summary>
    /// <param name="g">The <see cref="T:System.Drawing.Graphics" /> on which this <see cref="T:System.Drawing.Region" /> is drawn.</param>
    /// <returns>A Windows handle to this <see cref="T:System.Drawing.Region" />.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="g" /> is <see langword="null" />.</exception>
    public IntPtr GetHrgn(Graphics g)
    {
      if (g == null)
        throw new ArgumentNullException(nameof (g));
      IntPtr hrgn = IntPtr.Zero;
      int regionHrgn = SafeNativeMethods.Gdip.GdipGetRegionHRgn(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) g, g.NativeGraphics), out hrgn);
      if (regionHrgn != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionHrgn);
      return hrgn;
    }

    /// <summary>Tests whether this <see cref="T:System.Drawing.Region" /> has an empty interior on the specified drawing surface.</summary>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface.</param>
    /// <returns>
    /// <see langword="true" /> if the interior of this <see cref="T:System.Drawing.Region" /> is empty when the transformation associated with <paramref name="g" /> is applied; otherwise, <see langword="false" />.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="g" /> is <see langword="null" />.</exception>
    public bool IsEmpty(Graphics g)
    {
      if (g == null)
        throw new ArgumentNullException(nameof (g));
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsEmptyRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) g, g.NativeGraphics), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Tests whether this <see cref="T:System.Drawing.Region" /> has an infinite interior on the specified drawing surface.</summary>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface.</param>
    /// <returns>
    /// <see langword="true" /> if the interior of this <see cref="T:System.Drawing.Region" /> is infinite when the transformation associated with <paramref name="g" /> is applied; otherwise, <see langword="false" />.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="g" /> is <see langword="null" />.</exception>
    public bool IsInfinite(Graphics g)
    {
      if (g == null)
        throw new ArgumentNullException(nameof (g));
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsInfiniteRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) g, g.NativeGraphics), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Tests whether the specified <see cref="T:System.Drawing.Region" /> is identical to this <see cref="T:System.Drawing.Region" /> on the specified drawing surface.</summary>
    /// <param name="region">The <see cref="T:System.Drawing.Region" /> to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a drawing surface.</param>
    /// <returns>
    /// <see langword="true" /> if the interior of region is identical to the interior of this region when the transformation associated with the <paramref name="g" /> parameter is applied; otherwise, <see langword="false" />.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="g" /> or <paramref name="region" /> is <see langword="null" />.</exception>
    public bool Equals(Region region, Graphics g)
    {
      if (g == null)
        throw new ArgumentNullException(nameof (g));
      if (region == null)
        throw new ArgumentNullException(nameof (region));
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsEqualRegion(new HandleRef((object) this, this.nativeRegion), new HandleRef((object) region, region.nativeRegion), new HandleRef((object) g, g.NativeGraphics), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Returns a <see cref="T:System.Drawing.Drawing2D.RegionData" /> that represents the information that describes this <see cref="T:System.Drawing.Region" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Drawing2D.RegionData" /> that represents the information that describes this <see cref="T:System.Drawing.Region" />.</returns>
    public RegionData GetRegionData()
    {
      int bufferSize = 0;
      int regionDataSize = SafeNativeMethods.Gdip.GdipGetRegionDataSize(new HandleRef((object) this, this.nativeRegion), out bufferSize);
      if (regionDataSize != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionDataSize);
      if (bufferSize == 0)
        return (RegionData) null;
      byte[] numArray = new byte[bufferSize];
      int regionData = SafeNativeMethods.Gdip.GdipGetRegionData(new HandleRef((object) this, this.nativeRegion), numArray, bufferSize, out bufferSize);
      if (regionData != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionData);
      return new RegionData(numArray);
    }

    /// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <returns>
    /// <see langword="true" /> when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(float x, float y) => this.IsVisible(new PointF(x, y), (Graphics) null);

    /// <summary>Tests whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="point">The <see cref="T:System.Drawing.PointF" /> structure to test.</param>
    /// <returns>
    /// <see langword="true" /> when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(PointF point) => this.IsVisible(point, (Graphics) null);

    /// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
    /// <returns>
    /// <see langword="true" /> when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(float x, float y, Graphics g) => this.IsVisible(new PointF(x, y), g);

    /// <summary>Tests whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="point">The <see cref="T:System.Drawing.PointF" /> structure to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
    /// <returns>
    /// <see langword="true" /> when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(PointF point, Graphics g)
    {
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsVisibleRegionPoint(new HandleRef((object) this, this.nativeRegion), point.X, point.Y, new HandleRef((object) g, g == null ? IntPtr.Zero : g.NativeGraphics), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test.</param>
    /// <param name="width">The width of the rectangle to test.</param>
    /// <param name="height">The height of the rectangle to test.</param>
    /// <returns>
    /// <see langword="true" /> when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> object; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(float x, float y, float width, float height) => this.IsVisible(new RectangleF(x, y, width, height), (Graphics) null);

    /// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to test.</param>
    /// <returns>
    /// <see langword="true" /> when any portion of <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(RectangleF rect) => this.IsVisible(rect, (Graphics) null);

    /// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test.</param>
    /// <param name="width">The width of the rectangle to test.</param>
    /// <param name="height">The height of the rectangle to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
    /// <returns>
    /// <see langword="true" /> when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(float x, float y, float width, float height, Graphics g) => this.IsVisible(new RectangleF(x, y, width, height), g);

    /// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.RectangleF" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> structure to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
    /// <returns>
    /// <see langword="true" /> when <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(RectangleF rect, Graphics g)
    {
      int boolean = 0;
      int status = SafeNativeMethods.Gdip.GdipIsVisibleRegionRect(new HandleRef((object) this, this.nativeRegion), rect.X, rect.Y, rect.Width, rect.Height, new HandleRef((object) g, g == null ? IntPtr.Zero : g.NativeGraphics), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Tests whether the specified point is contained within this <see cref="T:System.Drawing.Region" /> object when drawn using the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
    /// <returns>
    /// <see langword="true" /> when the specified point is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(int x, int y, Graphics g) => this.IsVisible(new Point(x, y), g);

    /// <summary>Tests whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="point">The <see cref="T:System.Drawing.Point" /> structure to test.</param>
    /// <returns>
    /// <see langword="true" /> when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(Point point) => this.IsVisible(point, (Graphics) null);

    /// <summary>Tests whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="point">The <see cref="T:System.Drawing.Point" /> structure to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
    /// <returns>
    /// <see langword="true" /> when <paramref name="point" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(Point point, Graphics g)
    {
      int boolean = 0;
      int status = SafeNativeMethods.Gdip.GdipIsVisibleRegionPointI(new HandleRef((object) this, this.nativeRegion), point.X, point.Y, new HandleRef((object) g, g == null ? IntPtr.Zero : g.NativeGraphics), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test.</param>
    /// <param name="width">The width of the rectangle to test.</param>
    /// <param name="height">The height of the rectangle to test.</param>
    /// <returns>
    /// <see langword="true" /> when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(int x, int y, int width, int height) => this.IsVisible(new Rectangle(x, y, width, height), (Graphics) null);

    /// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure is contained within this <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to test.</param>
    /// <returns>This method returns <see langword="true" /> when any portion of <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(Rectangle rect) => this.IsVisible(rect, (Graphics) null);

    /// <summary>Tests whether any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test.</param>
    /// <param name="width">The width of the rectangle to test.</param>
    /// <param name="height">The height of the rectangle to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
    /// <returns>
    /// <see langword="true" /> when any portion of the specified rectangle is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(int x, int y, int width, int height, Graphics g) => this.IsVisible(new Rectangle(x, y, width, height), g);

    /// <summary>Tests whether any portion of the specified <see cref="T:System.Drawing.Rectangle" /> structure is contained within this <see cref="T:System.Drawing.Region" /> when drawn using the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> structure to test.</param>
    /// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that represents a graphics context.</param>
    /// <returns>
    /// <see langword="true" /> when any portion of the <paramref name="rect" /> is contained within this <see cref="T:System.Drawing.Region" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(Rectangle rect, Graphics g)
    {
      int boolean = 0;
      int status = SafeNativeMethods.Gdip.GdipIsVisibleRegionRectI(new HandleRef((object) this, this.nativeRegion), rect.X, rect.Y, rect.Width, rect.Height, new HandleRef((object) g, g == null ? IntPtr.Zero : g.NativeGraphics), out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Returns an array of <see cref="T:System.Drawing.RectangleF" /> structures that approximate this <see cref="T:System.Drawing.Region" /> after the specified matrix transformation is applied.</summary>
    /// <param name="matrix">A <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents a geometric transformation to apply to the region.</param>
    /// <returns>An array of <see cref="T:System.Drawing.RectangleF" /> structures that approximate this <see cref="T:System.Drawing.Region" /> after the specified matrix transformation is applied.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="matrix" /> is <see langword="null" />.</exception>
    public RectangleF[] GetRegionScans(Matrix matrix)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int count = 0;
      int regionScansCount = SafeNativeMethods.Gdip.GdipGetRegionScansCount(new HandleRef((object) this, this.nativeRegion), out count, new HandleRef((object) matrix, matrix.nativeMatrix));
      if (regionScansCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(regionScansCount);
      int num1 = Marshal.SizeOf(typeof (GPRECTF));
      IntPtr num2 = Marshal.AllocHGlobal(checked (num1 * count));
      RectangleF[] regionScans1;
      try
      {
        int regionScans2 = SafeNativeMethods.Gdip.GdipGetRegionScans(new HandleRef((object) this, this.nativeRegion), num2, out count, new HandleRef((object) matrix, matrix.nativeMatrix));
        if (regionScans2 != 0)
          throw SafeNativeMethods.Gdip.StatusException(regionScans2);
        GPRECTF gprectf = new GPRECTF();
        regionScans1 = new RectangleF[count];
        for (int index = 0; index < count; ++index)
        {
          GPRECTF structure = (GPRECTF) UnsafeNativeMethods.PtrToStructure((IntPtr) checked (unchecked ((long) num2) + (long) (num1 * index)), typeof (GPRECTF));
          regionScans1[index] = structure.ToRectangleF();
        }
      }
      finally
      {
        Marshal.FreeHGlobal(num2);
      }
      return regionScans1;
    }
  }
}
