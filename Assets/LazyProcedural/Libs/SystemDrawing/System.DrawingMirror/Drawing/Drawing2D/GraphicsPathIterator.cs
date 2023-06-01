// Decompiled with JetBrains decompiler
// Type: System.Drawing.Drawing2D.GraphicsPathIterator
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
  /// <summary>Provides the ability to iterate through subpaths in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> and test the types of shapes contained in each subpath. This class cannot be inherited.</summary>
  public sealed class GraphicsPathIterator : MarshalByRefObject, IDisposable
  {
    internal IntPtr nativeIter;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> class with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
    /// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object for which this helper class is to be initialized.</param>
    public GraphicsPathIterator(GraphicsPath path)
    {
      IntPtr pathIter1 = IntPtr.Zero;
      int pathIter2 = SafeNativeMethods.Gdip.GdipCreatePathIter(out pathIter1, new HandleRef((object) path, path == null ? IntPtr.Zero : path.nativePath));
      if (pathIter2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(pathIter2);
      this.nativeIter = pathIter1;
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> object.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.nativeIter != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDeletePathIter(new HandleRef((object) this, this.nativeIter));
      }
      catch (Exception ex)
      {
        if (!System.Drawing.ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeIter = IntPtr.Zero;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~GraphicsPathIterator() => this.Dispose(false);

    /// <summary>Moves the <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> to the next subpath in the path. The start index and end index of the next subpath are contained in the [out] parameters.</summary>
    /// <param name="startIndex">[out] Receives the starting index of the next subpath.</param>
    /// <param name="endIndex">[out] Receives the ending index of the next subpath.</param>
    /// <param name="isClosed">[out] Indicates whether the subpath is closed.</param>
    /// <returns>The number of subpaths in the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</returns>
    public int NextSubpath(out int startIndex, out int endIndex, out bool isClosed)
    {
      int resultCount = 0;
      int startIndex1 = 0;
      int endIndex1 = 0;
      int status = SafeNativeMethods.Gdip.GdipPathIterNextSubpath(new HandleRef((object) this, this.nativeIter), out resultCount, out startIndex1, out endIndex1, out isClosed);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      startIndex = startIndex1;
      endIndex = endIndex1;
      return resultCount;
    }

    /// <summary>Gets the next figure (subpath) from the associated path of this <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" />.</summary>
    /// <param name="path">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that is to have its data points set to match the data points of the retrieved figure (subpath) for this iterator.</param>
    /// <param name="isClosed">[out] Indicates whether the current subpath is closed. It is <see langword="true" /> if the if the figure is closed, otherwise it is <see langword="false" />.</param>
    /// <returns>The number of data points in the retrieved figure (subpath). If there are no more figures to retrieve, zero is returned.</returns>
    public int NextSubpath(GraphicsPath path, out bool isClosed)
    {
      int resultCount = 0;
      int status = SafeNativeMethods.Gdip.GdipPathIterNextSubpathPath(new HandleRef((object) this, this.nativeIter), out resultCount, new HandleRef((object) path, path == null ? IntPtr.Zero : path.nativePath), out isClosed);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return resultCount;
    }

    /// <summary>Gets the starting index and the ending index of the next group of data points that all have the same type.</summary>
    /// <param name="pathType">[out] Receives the point type shared by all points in the group. Possible types can be retrieved from the <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration.</param>
    /// <param name="startIndex">[out] Receives the starting index of the group of points.</param>
    /// <param name="endIndex">[out] Receives the ending index of the group of points.</param>
    /// <returns>This method returns the number of data points in the group. If there are no more groups in the path, this method returns 0.</returns>
    public int NextPathType(out byte pathType, out int startIndex, out int endIndex)
    {
      int resultCount = 0;
      int status = SafeNativeMethods.Gdip.GdipPathIterNextPathType(new HandleRef((object) this, this.nativeIter), out resultCount, out pathType, out startIndex, out endIndex);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return resultCount;
    }

    /// <summary>Increments the <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> to the next marker in the path and returns the start and stop indexes by way of the [out] parameters.</summary>
    /// <param name="startIndex">[out] The integer reference supplied to this parameter receives the index of the point that starts a subpath.</param>
    /// <param name="endIndex">[out] The integer reference supplied to this parameter receives the index of the point that ends the subpath to which <paramref name="startIndex" /> points.</param>
    /// <returns>The number of points between this marker and the next.</returns>
    public int NextMarker(out int startIndex, out int endIndex)
    {
      int resultCount = 0;
      int status = SafeNativeMethods.Gdip.GdipPathIterNextMarker(new HandleRef((object) this, this.nativeIter), out resultCount, out startIndex, out endIndex);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return resultCount;
    }

    /// <summary>This <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> object has a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object associated with it. The <see cref="M:System.Drawing.Drawing2D.GraphicsPathIterator.NextMarker(System.Drawing.Drawing2D.GraphicsPath)" /> method increments the associated <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to the next marker in its path and copies all the points contained between the current marker and the next marker (or end of path) to a second <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object passed in to the parameter.</summary>
    /// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object to which the points will be copied.</param>
    /// <returns>The number of points between this marker and the next.</returns>
    public int NextMarker(GraphicsPath path)
    {
      int resultCount = 0;
      int status = SafeNativeMethods.Gdip.GdipPathIterNextMarkerPath(new HandleRef((object) this, this.nativeIter), out resultCount, new HandleRef((object) path, path == null ? IntPtr.Zero : path.nativePath));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return resultCount;
    }

    /// <summary>Gets the number of points in the path.</summary>
    /// <returns>The number of points in the path.</returns>
    public int Count
    {
      get
      {
        int count1 = 0;
        int count2 = SafeNativeMethods.Gdip.GdipPathIterGetCount(new HandleRef((object) this, this.nativeIter), out count1);
        if (count2 != 0)
          throw SafeNativeMethods.Gdip.StatusException(count2);
        return count1;
      }
    }

    /// <summary>Gets the number of subpaths in the path.</summary>
    /// <returns>The number of subpaths in the path.</returns>
    public int SubpathCount
    {
      get
      {
        int count = 0;
        int subpathCount = SafeNativeMethods.Gdip.GdipPathIterGetSubpathCount(new HandleRef((object) this, this.nativeIter), out count);
        if (subpathCount != 0)
          throw SafeNativeMethods.Gdip.StatusException(subpathCount);
        return count;
      }
    }

    /// <summary>Indicates whether the path associated with this <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> contains a curve.</summary>
    /// <returns>This method returns <see langword="true" /> if the current subpath contains a curve; otherwise, <see langword="false" />.</returns>
    public bool HasCurve()
    {
      bool hasCurve = false;
      int status = SafeNativeMethods.Gdip.GdipPathIterHasCurve(new HandleRef((object) this, this.nativeIter), out hasCurve);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return hasCurve;
    }

    /// <summary>Rewinds this <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> to the beginning of its associated path.</summary>
    public void Rewind()
    {
      int status = SafeNativeMethods.Gdip.GdipPathIterRewind(new HandleRef((object) this, this.nativeIter));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Copies the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> property and <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> property arrays of the associated <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into the two specified arrays.</summary>
    /// <param name="points">Upon return, contains an array of <see cref="T:System.Drawing.PointF" /> structures that represents the points in the path.</param>
    /// <param name="types">Upon return, contains an array of bytes that represents the types of points in the path.</param>
    /// <returns>The number of points copied.</returns>
    public int Enumerate(ref PointF[] points, ref byte[] types)
    {
      if (points.Length != types.Length)
        throw SafeNativeMethods.Gdip.StatusException(2);
      int resultCount = 0;
      int num1 = Marshal.SizeOf(typeof (GPPOINTF));
      int length = points.Length;
      byte[] types1 = new byte[length];
      IntPtr num2 = Marshal.AllocHGlobal(checked (length * num1));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipPathIterEnumerate(new HandleRef((object) this, this.nativeIter), out resultCount, num2, types1, length);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        if (resultCount < length)
          SafeNativeMethods.ZeroMemory((IntPtr) checked (unchecked ((long) num2) + (long) (resultCount * num1)), (UIntPtr) (ulong) ((length - resultCount) * num1));
        points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(num2, length);
        types1.CopyTo((Array) types, 0);
      }
      finally
      {
        Marshal.FreeHGlobal(num2);
      }
      return resultCount;
    }

    /// <summary>Copies the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> property and <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> property arrays of the associated <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into the two specified arrays.</summary>
    /// <param name="points">Upon return, contains an array of <see cref="T:System.Drawing.PointF" /> structures that represents the points in the path.</param>
    /// <param name="types">Upon return, contains an array of bytes that represents the types of points in the path.</param>
    /// <param name="startIndex">Specifies the starting index of the arrays.</param>
    /// <param name="endIndex">Specifies the ending index of the arrays.</param>
    /// <returns>The number of points copied.</returns>
    public int CopyData(ref PointF[] points, ref byte[] types, int startIndex, int endIndex)
    {
      if (points.Length != types.Length || endIndex - startIndex + 1 > points.Length)
        throw SafeNativeMethods.Gdip.StatusException(2);
      int resultCount = 0;
      int num1 = Marshal.SizeOf(typeof (GPPOINTF));
      int length = points.Length;
      byte[] types1 = new byte[length];
      IntPtr num2 = Marshal.AllocHGlobal(checked (length * num1));
      try
      {
        int status = SafeNativeMethods.Gdip.GdipPathIterCopyData(new HandleRef((object) this, this.nativeIter), out resultCount, num2, types1, startIndex, endIndex);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        if (resultCount < length)
          SafeNativeMethods.ZeroMemory((IntPtr) checked (unchecked ((long) num2) + (long) (resultCount * num1)), (UIntPtr) (ulong) ((length - resultCount) * num1));
        points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(num2, length);
        types1.CopyTo((Array) types, 0);
      }
      finally
      {
        Marshal.FreeHGlobal(num2);
      }
      return resultCount;
    }
  }
}
