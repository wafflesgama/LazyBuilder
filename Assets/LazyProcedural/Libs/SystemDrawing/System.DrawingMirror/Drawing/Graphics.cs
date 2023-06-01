// Decompiled with JetBrains decompiler
// Type: System.Drawing.Graphics
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.Drawing.Text;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Drawing
{
  /// <summary>Encapsulates a GDI+ drawing surface. This class cannot be inherited.</summary>
  public sealed class Graphics : MarshalByRefObject, IDisposable, IDeviceContext
  {
    private GraphicsContext previousContext;
    private static readonly object syncObject = new object();
    private IntPtr nativeGraphics;
    private IntPtr nativeHdc;
    private object printingHelper;
    private static IntPtr halftonePalette;
    private Image backingImage;

    private Graphics(IntPtr gdipNativeGraphics) => this.nativeGraphics = !(gdipNativeGraphics == IntPtr.Zero) ? gdipNativeGraphics : throw new ArgumentNullException(nameof (gdipNativeGraphics));

    /// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a device context.</summary>
    /// <param name="hdc">Handle to a device context.</param>
    /// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified device context.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Graphics FromHdc(IntPtr hdc)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      return !(hdc == IntPtr.Zero) ? Graphics.FromHdcInternal(hdc) : throw new ArgumentNullException(nameof (hdc));
    }

    /// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> for the specified device context.</summary>
    /// <param name="hdc">Handle to a device context.</param>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> for the specified device context.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static Graphics FromHdcInternal(IntPtr hdc)
    {
      IntPtr graphics = IntPtr.Zero;
      int fromHdc = SafeNativeMethods.Gdip.GdipCreateFromHDC(new HandleRef((object) null, hdc), out graphics);
      if (fromHdc != 0)
        throw SafeNativeMethods.Gdip.StatusException(fromHdc);
      return new Graphics(graphics);
    }

    /// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a device context and handle to a device.</summary>
    /// <param name="hdc">Handle to a device context.</param>
    /// <param name="hdevice">Handle to a device.</param>
    /// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified device context and device.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Graphics FromHdc(IntPtr hdc, IntPtr hdevice)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      IntPtr graphics = IntPtr.Zero;
      int fromHdC2 = SafeNativeMethods.Gdip.GdipCreateFromHDC2(new HandleRef((object) null, hdc), new HandleRef((object) null, hdevice), out graphics);
      if (fromHdC2 != 0)
        throw SafeNativeMethods.Gdip.StatusException(fromHdC2);
      return new Graphics(graphics);
    }

    /// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a window.</summary>
    /// <param name="hwnd">Handle to a window.</param>
    /// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified window handle.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Graphics FromHwnd(IntPtr hwnd)
    {
      IntSecurity.ObjectFromWin32Handle.Demand();
      return Graphics.FromHwndInternal(hwnd);
    }

    /// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> for the specified windows handle.</summary>
    /// <param name="hwnd">Handle to a window.</param>
    /// <returns>A <see cref="T:System.Drawing.Graphics" /> for the specified window handle.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static Graphics FromHwndInternal(IntPtr hwnd)
    {
      IntPtr graphics = IntPtr.Zero;
      int fromHwnd = SafeNativeMethods.Gdip.GdipCreateFromHWND(new HandleRef((object) null, hwnd), out graphics);
      if (fromHwnd != 0)
        throw SafeNativeMethods.Gdip.StatusException(fromHwnd);
      return new Graphics(graphics);
    }

    /// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified <see cref="T:System.Drawing.Image" />.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Graphics" />.</param>
    /// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified <see cref="T:System.Drawing.Image" />.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.Exception">
    /// <paramref name="image" /> has an indexed pixel format or its format is undefined.</exception>
    public static Graphics FromImage(Image image)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if ((image.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
        throw new Exception(SR.GetString("GdiplusCannotCreateGraphicsFromIndexedPixelFormat"));
      IntPtr graphics = IntPtr.Zero;
      int imageGraphicsContext = SafeNativeMethods.Gdip.GdipGetImageGraphicsContext(new HandleRef((object) image, image.nativeImage), out graphics);
      if (imageGraphicsContext != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageGraphicsContext);
      return new Graphics(graphics) { backingImage = image };
    }

    internal IntPtr NativeGraphics => this.nativeGraphics;

    /// <summary>Gets the handle to the device context associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>Handle to the device context associated with this <see cref="T:System.Drawing.Graphics" />.</returns>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public IntPtr GetHdc()
    {
      IntPtr hdc = IntPtr.Zero;
      int dc = SafeNativeMethods.Gdip.GdipGetDC(new HandleRef((object) this, this.NativeGraphics), out hdc);
      if (dc != 0)
        throw SafeNativeMethods.Gdip.StatusException(dc);
      this.nativeHdc = hdc;
      return this.nativeHdc;
    }

    /// <summary>Releases a device context handle obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="hdc">Handle to a device context obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void ReleaseHdc(IntPtr hdc)
    {
      IntSecurity.Win32HandleManipulation.Demand();
      this.ReleaseHdcInternal(hdc);
    }

    /// <summary>Releases a device context handle obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</summary>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public void ReleaseHdc() => this.ReleaseHdcInternal(this.nativeHdc);

    /// <summary>Releases a handle to a device context.</summary>
    /// <param name="hdc">Handle to a device context.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public void ReleaseHdcInternal(IntPtr hdc)
    {
      int status = SafeNativeMethods.Gdip.GdipReleaseDC(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) null, hdc));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.nativeHdc = IntPtr.Zero;
    }

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Graphics" />.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      GraphicsContext previous;
      for (; this.previousContext != null; this.previousContext = previous)
      {
        previous = this.previousContext.Previous;
        this.previousContext.Dispose();
      }
      if (!(this.nativeGraphics != IntPtr.Zero))
        return;
      try
      {
        if (this.nativeHdc != IntPtr.Zero)
          this.ReleaseHdc();
        if (this.PrintingHelper != null && this.PrintingHelper is DeviceContext printingHelper)
        {
          printingHelper.Dispose();
          this.printingHelper = (object) null;
        }
        SafeNativeMethods.Gdip.GdipDeleteGraphics(new HandleRef((object) this, this.nativeGraphics));
      }
      catch (Exception ex)
      {
        if (!ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeGraphics = IntPtr.Zero;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~Graphics() => this.Dispose(false);

    /// <summary>Forces execution of all pending graphics operations and returns immediately without waiting for the operations to finish.</summary>
    public void Flush() => this.Flush(FlushIntention.Flush);

    /// <summary>Forces execution of all pending graphics operations with the method waiting or not waiting, as specified, to return before the operations finish.</summary>
    /// <param name="intention">Member of the <see cref="T:System.Drawing.Drawing2D.FlushIntention" /> enumeration that specifies whether the method returns immediately or waits for any existing operations to finish.</param>
    public void Flush(FlushIntention intention)
    {
      int status = SafeNativeMethods.Gdip.GdipFlush(new HandleRef((object) this, this.NativeGraphics), intention);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets a value that specifies how composited images are drawn to this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.CompositingMode" /> enumeration. The default is <see cref="F:System.Drawing.Drawing2D.CompositingMode.SourceOver" />.</returns>
    public CompositingMode CompositingMode
    {
      get
      {
        int compositeMode = 0;
        int compositingMode = SafeNativeMethods.Gdip.GdipGetCompositingMode(new HandleRef((object) this, this.NativeGraphics), out compositeMode);
        if (compositingMode != 0)
          throw SafeNativeMethods.Gdip.StatusException(compositingMode);
        return (CompositingMode) compositeMode;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 1) ? SafeNativeMethods.Gdip.GdipSetCompositingMode(new HandleRef((object) this, this.NativeGraphics), (int) value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (CompositingMode));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the rendering origin of this <see cref="T:System.Drawing.Graphics" /> for dithering and for hatch brushes.</summary>
    /// <returns>A <see cref="T:System.Drawing.Point" /> structure that represents the dither origin for 8-bits-per-pixel and 16-bits-per-pixel dithering and is also used to set the origin for hatch brushes.</returns>
    public Point RenderingOrigin
    {
      get
      {
        int x;
        int y;
        int renderingOrigin = SafeNativeMethods.Gdip.GdipGetRenderingOrigin(new HandleRef((object) this, this.NativeGraphics), out x, out y);
        if (renderingOrigin != 0)
          throw SafeNativeMethods.Gdip.StatusException(renderingOrigin);
        return new Point(x, y);
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetRenderingOrigin(new HandleRef((object) this, this.NativeGraphics), value.X, value.Y);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the rendering quality of composited images drawn to this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.CompositingQuality" /> enumeration. The default is <see cref="F:System.Drawing.Drawing2D.CompositingQuality.Default" />.</returns>
    public CompositingQuality CompositingQuality
    {
      get
      {
        CompositingQuality quality;
        int compositingQuality = SafeNativeMethods.Gdip.GdipGetCompositingQuality(new HandleRef((object) this, this.NativeGraphics), out quality);
        if (compositingQuality != 0)
          throw SafeNativeMethods.Gdip.StatusException(compositingQuality);
        return quality;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, -1, 4) ? SafeNativeMethods.Gdip.GdipSetCompositingQuality(new HandleRef((object) this, this.NativeGraphics), value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (CompositingQuality));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the rendering mode for text associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Text.TextRenderingHint" /> values.</returns>
    public TextRenderingHint TextRenderingHint
    {
      get
      {
        TextRenderingHint textRenderingHint1 = TextRenderingHint.SystemDefault;
        int textRenderingHint2 = SafeNativeMethods.Gdip.GdipGetTextRenderingHint(new HandleRef((object) this, this.NativeGraphics), out textRenderingHint1);
        if (textRenderingHint2 != 0)
          throw SafeNativeMethods.Gdip.StatusException(textRenderingHint2);
        return textRenderingHint1;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 5) ? SafeNativeMethods.Gdip.GdipSetTextRenderingHint(new HandleRef((object) this, this.NativeGraphics), value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (TextRenderingHint));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the gamma correction value for rendering text.</summary>
    /// <returns>The gamma correction value used for rendering antialiased and ClearType text.</returns>
    public int TextContrast
    {
      get
      {
        int textContrast1 = 0;
        int textContrast2 = SafeNativeMethods.Gdip.GdipGetTextContrast(new HandleRef((object) this, this.NativeGraphics), out textContrast1);
        if (textContrast2 != 0)
          throw SafeNativeMethods.Gdip.StatusException(textContrast2);
        return textContrast1;
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetTextContrast(new HandleRef((object) this, this.NativeGraphics), value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the rendering quality for this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Drawing2D.SmoothingMode" /> values.</returns>
    public SmoothingMode SmoothingMode
    {
      get
      {
        SmoothingMode smoothingMode1 = SmoothingMode.Default;
        int smoothingMode2 = SafeNativeMethods.Gdip.GdipGetSmoothingMode(new HandleRef((object) this, this.NativeGraphics), out smoothingMode1);
        if (smoothingMode2 != 0)
          throw SafeNativeMethods.Gdip.StatusException(smoothingMode2);
        return smoothingMode1;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, -1, 4) ? SafeNativeMethods.Gdip.GdipSetSmoothingMode(new HandleRef((object) this, this.NativeGraphics), value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (SmoothingMode));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets a value specifying how pixels are offset during rendering of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.PixelOffsetMode" /> enumeration</returns>
    public PixelOffsetMode PixelOffsetMode
    {
      get
      {
        PixelOffsetMode pixelOffsetMode1 = PixelOffsetMode.Default;
        int pixelOffsetMode2 = SafeNativeMethods.Gdip.GdipGetPixelOffsetMode(new HandleRef((object) this, this.NativeGraphics), out pixelOffsetMode1);
        if (pixelOffsetMode2 != 0)
          throw SafeNativeMethods.Gdip.StatusException(pixelOffsetMode2);
        return pixelOffsetMode1;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, -1, 4) ? SafeNativeMethods.Gdip.GdipSetPixelOffsetMode(new HandleRef((object) this, this.NativeGraphics), value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (PixelOffsetMode));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    internal object PrintingHelper
    {
      get => this.printingHelper;
      set => this.printingHelper = value;
    }

    /// <summary>Gets or sets the interpolation mode associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.Drawing2D.InterpolationMode" /> values.</returns>
    public InterpolationMode InterpolationMode
    {
      get
      {
        int mode = 0;
        int interpolationMode = SafeNativeMethods.Gdip.GdipGetInterpolationMode(new HandleRef((object) this, this.NativeGraphics), out mode);
        if (interpolationMode != 0)
          throw SafeNativeMethods.Gdip.StatusException(interpolationMode);
        return (InterpolationMode) mode;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, -1, 7) ? SafeNativeMethods.Gdip.GdipSetInterpolationMode(new HandleRef((object) this, this.NativeGraphics), (int) value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (InterpolationMode));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets a copy of the geometric world transformation for this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the geometric world transformation for this <see cref="T:System.Drawing.Graphics" />.</returns>
    public Matrix Transform
    {
      get
      {
        Matrix wrapper = new Matrix();
        int worldTransform = SafeNativeMethods.Gdip.GdipGetWorldTransform(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) wrapper, wrapper.nativeMatrix));
        if (worldTransform != 0)
          throw SafeNativeMethods.Gdip.StatusException(worldTransform);
        return wrapper;
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetWorldTransform(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) value, value.nativeMatrix));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the unit of measure used for page coordinates in this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>One of the <see cref="T:System.Drawing.GraphicsUnit" /> values other than <see cref="F:System.Drawing.GraphicsUnit.World" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <see cref="P:System.Drawing.Graphics.PageUnit" /> is set to <see cref="F:System.Drawing.GraphicsUnit.World" />, which is not a physical unit.</exception>
    public GraphicsUnit PageUnit
    {
      get
      {
        int unit = 0;
        int pageUnit = SafeNativeMethods.Gdip.GdipGetPageUnit(new HandleRef((object) this, this.NativeGraphics), out unit);
        if (pageUnit != 0)
          throw SafeNativeMethods.Gdip.StatusException(pageUnit);
        return (GraphicsUnit) unit;
      }
      set
      {
        int status = ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 6) ? SafeNativeMethods.Gdip.GdipSetPageUnit(new HandleRef((object) this, this.NativeGraphics), (int) value) : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (GraphicsUnit));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets or sets the scaling between world units and page units for this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>This property specifies a value for the scaling between world units and page units for this <see cref="T:System.Drawing.Graphics" />.</returns>
    public float PageScale
    {
      get
      {
        float[] scale = new float[1];
        int pageScale = SafeNativeMethods.Gdip.GdipGetPageScale(new HandleRef((object) this, this.NativeGraphics), scale);
        if (pageScale != 0)
          throw SafeNativeMethods.Gdip.StatusException(pageScale);
        return scale[0];
      }
      set
      {
        int status = SafeNativeMethods.Gdip.GdipSetPageScale(new HandleRef((object) this, this.NativeGraphics), value);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    /// <summary>Gets the horizontal resolution of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>The value, in dots per inch, for the horizontal resolution supported by this <see cref="T:System.Drawing.Graphics" />.</returns>
    public float DpiX
    {
      get
      {
        float[] dpi = new float[1];
        int dpiX = SafeNativeMethods.Gdip.GdipGetDpiX(new HandleRef((object) this, this.NativeGraphics), dpi);
        if (dpiX != 0)
          throw SafeNativeMethods.Gdip.StatusException(dpiX);
        return dpi[0];
      }
    }

    /// <summary>Gets the vertical resolution of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>The value, in dots per inch, for the vertical resolution supported by this <see cref="T:System.Drawing.Graphics" />.</returns>
    public float DpiY
    {
      get
      {
        float[] dpi = new float[1];
        int dpiY = SafeNativeMethods.Gdip.GdipGetDpiY(new HandleRef((object) this, this.NativeGraphics), dpi);
        if (dpiY != 0)
          throw SafeNativeMethods.Gdip.StatusException(dpiY);
        return dpi[0];
      }
    }

    /// <summary>Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
    /// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
    /// <param name="blockRegionSize">The size of the area to be transferred.</param>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
    public void CopyFromScreen(
      Point upperLeftSource,
      Point upperLeftDestination,
      Size blockRegionSize)
    {
      this.CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize);
    }

    /// <summary>Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
    /// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle.</param>
    /// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
    /// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
    /// <param name="blockRegionSize">The size of the area to be transferred.</param>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
    public void CopyFromScreen(
      int sourceX,
      int sourceY,
      int destinationX,
      int destinationY,
      Size blockRegionSize)
    {
      this.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize, CopyPixelOperation.SourceCopy);
    }

    /// <summary>Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
    /// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
    /// <param name="blockRegionSize">The size of the area to be transferred.</param>
    /// <param name="copyPixelOperation">One of the <see cref="T:System.Drawing.CopyPixelOperation" /> values.</param>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="copyPixelOperation" /> is not a member of <see cref="T:System.Drawing.CopyPixelOperation" />.</exception>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
    public void CopyFromScreen(
      Point upperLeftSource,
      Point upperLeftDestination,
      Size blockRegionSize,
      CopyPixelOperation copyPixelOperation)
    {
      this.CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize, copyPixelOperation);
    }

    /// <summary>Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
    /// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle</param>
    /// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
    /// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
    /// <param name="blockRegionSize">The size of the area to be transferred.</param>
    /// <param name="copyPixelOperation">One of the <see cref="T:System.Drawing.CopyPixelOperation" /> values.</param>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="copyPixelOperation" /> is not a member of <see cref="T:System.Drawing.CopyPixelOperation" />.</exception>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
    public void CopyFromScreen(
      int sourceX,
      int sourceY,
      int destinationX,
      int destinationY,
      Size blockRegionSize,
      CopyPixelOperation copyPixelOperation)
    {
      if (copyPixelOperation <= CopyPixelOperation.SourceInvert)
      {
        if (copyPixelOperation <= CopyPixelOperation.NotSourceCopy)
        {
          if (copyPixelOperation <= CopyPixelOperation.Blackness)
          {
            if (copyPixelOperation == CopyPixelOperation.NoMirrorBitmap || copyPixelOperation == CopyPixelOperation.Blackness)
              goto label_16;
          }
          else if (copyPixelOperation == CopyPixelOperation.NotSourceErase || copyPixelOperation == CopyPixelOperation.NotSourceCopy)
            goto label_16;
        }
        else if (copyPixelOperation <= CopyPixelOperation.DestinationInvert)
        {
          if (copyPixelOperation == CopyPixelOperation.SourceErase || copyPixelOperation == CopyPixelOperation.DestinationInvert)
            goto label_16;
        }
        else if (copyPixelOperation == CopyPixelOperation.PatInvert || copyPixelOperation == CopyPixelOperation.SourceInvert)
          goto label_16;
      }
      else if (copyPixelOperation <= CopyPixelOperation.SourceCopy)
      {
        if (copyPixelOperation <= CopyPixelOperation.MergePaint)
        {
          if (copyPixelOperation == CopyPixelOperation.SourceAnd || copyPixelOperation == CopyPixelOperation.MergePaint)
            goto label_16;
        }
        else if (copyPixelOperation == CopyPixelOperation.MergeCopy || copyPixelOperation == CopyPixelOperation.SourceCopy)
          goto label_16;
      }
      else if (copyPixelOperation <= CopyPixelOperation.PatCopy)
      {
        if (copyPixelOperation == CopyPixelOperation.SourcePaint || copyPixelOperation == CopyPixelOperation.PatCopy)
          goto label_16;
      }
      else if (copyPixelOperation == CopyPixelOperation.PatPaint || copyPixelOperation == CopyPixelOperation.Whiteness || copyPixelOperation == CopyPixelOperation.CaptureBlt)
        goto label_16;
      throw new InvalidEnumArgumentException("value", (int) copyPixelOperation, typeof (CopyPixelOperation));
label_16:
      new UIPermission(UIPermissionWindow.AllWindows).Demand();
      int width = blockRegionSize.Width;
      int height = blockRegionSize.Height;
      using (DeviceContext deviceContext = DeviceContext.FromHwnd(IntPtr.Zero))
      {
        HandleRef hSrcDC = new HandleRef((object) null, deviceContext.Hdc);
        HandleRef hDC = new HandleRef((object) null, this.GetHdc());
        try
        {
          if (SafeNativeMethods.BitBlt(hDC, destinationX, destinationY, width, height, hSrcDC, sourceX, sourceY, (int) copyPixelOperation) == 0)
            throw new Win32Exception();
        }
        finally
        {
          this.ReleaseHdc();
        }
      }
    }

    /// <summary>Resets the world transformation matrix of this <see cref="T:System.Drawing.Graphics" /> to the identity matrix.</summary>
    public void ResetTransform()
    {
      int status = SafeNativeMethods.Gdip.GdipResetWorldTransform(new HandleRef((object) this, this.NativeGraphics));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Multiplies the world transformation of this <see cref="T:System.Drawing.Graphics" /> and specified the <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
    /// <param name="matrix">4x4 <see cref="T:System.Drawing.Drawing2D.Matrix" /> that multiplies the world transformation.</param>
    public void MultiplyTransform(Matrix matrix) => this.MultiplyTransform(matrix, MatrixOrder.Prepend);

    /// <summary>Multiplies the world transformation of this <see cref="T:System.Drawing.Graphics" /> and specified the <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
    /// <param name="matrix">4x4 <see cref="T:System.Drawing.Drawing2D.Matrix" /> that multiplies the world transformation.</param>
    /// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that determines the order of the multiplication.</param>
    public void MultiplyTransform(Matrix matrix, MatrixOrder order)
    {
      if (matrix == null)
        throw new ArgumentNullException(nameof (matrix));
      int status = SafeNativeMethods.Gdip.GdipMultiplyWorldTransform(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) matrix, matrix.nativeMatrix), order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Changes the origin of the coordinate system by prepending the specified translation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="dx">The x-coordinate of the translation.</param>
    /// <param name="dy">The y-coordinate of the translation.</param>
    public void TranslateTransform(float dx, float dy) => this.TranslateTransform(dx, dy, MatrixOrder.Prepend);

    /// <summary>Changes the origin of the coordinate system by applying the specified translation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
    /// <param name="dx">The x-coordinate of the translation.</param>
    /// <param name="dy">The y-coordinate of the translation.</param>
    /// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the translation is prepended or appended to the transformation matrix.</param>
    public void TranslateTransform(float dx, float dy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslateWorldTransform(new HandleRef((object) this, this.NativeGraphics), dx, dy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies the specified scaling operation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> by prepending it to the object's transformation matrix.</summary>
    /// <param name="sx">Scale factor in the x direction.</param>
    /// <param name="sy">Scale factor in the y direction.</param>
    public void ScaleTransform(float sx, float sy) => this.ScaleTransform(sx, sy, MatrixOrder.Prepend);

    /// <summary>Applies the specified scaling operation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
    /// <param name="sx">Scale factor in the x direction.</param>
    /// <param name="sy">Scale factor in the y direction.</param>
    /// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the scaling operation is prepended or appended to the transformation matrix.</param>
    public void ScaleTransform(float sx, float sy, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipScaleWorldTransform(new HandleRef((object) this, this.NativeGraphics), sx, sy, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="angle">Angle of rotation in degrees.</param>
    public void RotateTransform(float angle) => this.RotateTransform(angle, MatrixOrder.Prepend);

    /// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
    /// <param name="angle">Angle of rotation in degrees.</param>
    /// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the rotation is appended or prepended to the matrix transformation.</param>
    public void RotateTransform(float angle, MatrixOrder order)
    {
      int status = SafeNativeMethods.Gdip.GdipRotateWorldTransform(new HandleRef((object) this, this.NativeGraphics), angle, order);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Transforms an array of points from one coordinate space to another using the current world and page transformations of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="destSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the destination coordinate space.</param>
    /// <param name="srcSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the source coordinate space.</param>
    /// <param name="pts">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points to transform.</param>
    public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
    {
      IntPtr num = pts != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(pts) : throw new ArgumentNullException(nameof (pts));
      int status = SafeNativeMethods.Gdip.GdipTransformPoints(new HandleRef((object) this, this.NativeGraphics), (int) destSpace, (int) srcSpace, num, pts.Length);
      try
      {
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        PointF[] pointFArray = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(num, pts.Length);
        for (int index = 0; index < pts.Length; ++index)
          pts[index] = pointFArray[index];
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Transforms an array of points from one coordinate space to another using the current world and page transformations of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="destSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the destination coordinate space.</param>
    /// <param name="srcSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the source coordinate space.</param>
    /// <param name="pts">Array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transformation.</param>
    public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
    {
      IntPtr num = pts != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(pts) : throw new ArgumentNullException(nameof (pts));
      int status = SafeNativeMethods.Gdip.GdipTransformPointsI(new HandleRef((object) this, this.NativeGraphics), (int) destSpace, (int) srcSpace, num, pts.Length);
      try
      {
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        Point[] pointArray = SafeNativeMethods.Gdip.ConvertGPPOINTArray(num, pts.Length);
        for (int index = 0; index < pts.Length; ++index)
          pts[index] = pointArray[index];
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Gets the nearest color to the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
    /// <param name="color">
    /// <see cref="T:System.Drawing.Color" /> structure for which to find a match.</param>
    /// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the nearest color to the one specified with the <paramref name="color" /> parameter.</returns>
    public Color GetNearestColor(Color color)
    {
      int argb = color.ToArgb();
      int nearestColor = SafeNativeMethods.Gdip.GdipGetNearestColor(new HandleRef((object) this, this.NativeGraphics), ref argb);
      if (nearestColor != 0)
        throw SafeNativeMethods.Gdip.StatusException(nearestColor);
      return Color.FromArgb(argb);
    }

    /// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line.</param>
    /// <param name="x1">The x-coordinate of the first point.</param>
    /// <param name="y1">The y-coordinate of the first point.</param>
    /// <param name="x2">The x-coordinate of the second point.</param>
    /// <param name="y2">The y-coordinate of the second point.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawLine(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x1, y1, x2, y2));
    }

    /// <summary>Draws a line connecting two <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line.</param>
    /// <param name="pt1">
    /// <see cref="T:System.Drawing.PointF" /> structure that represents the first point to connect.</param>
    /// <param name="pt2">
    /// <see cref="T:System.Drawing.PointF" /> structure that represents the second point to connect.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawLine(Pen pen, PointF pt1, PointF pt2) => this.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);

    /// <summary>Draws a series of line segments that connect an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line segments.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points to connect.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawLines(Pen pen, PointF[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawLines(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line.</param>
    /// <param name="x1">The x-coordinate of the first point.</param>
    /// <param name="y1">The y-coordinate of the first point.</param>
    /// <param name="x2">The x-coordinate of the second point.</param>
    /// <param name="y2">The y-coordinate of the second point.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawLineI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x1, y1, x2, y2));
    }

    /// <summary>Draws a line connecting two <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line.</param>
    /// <param name="pt1">
    /// <see cref="T:System.Drawing.Point" /> structure that represents the first point to connect.</param>
    /// <param name="pt2">
    /// <see cref="T:System.Drawing.Point" /> structure that represents the second point to connect.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawLine(Pen pen, Point pt1, Point pt2) => this.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);

    /// <summary>Draws a series of line segments that connect an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line segments.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the points to connect.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawLines(Pen pen, Point[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawLinesI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
    /// <param name="width">Width of the rectangle that defines the ellipse.</param>
    /// <param name="height">Height of the rectangle that defines the ellipse.</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawArc(
      Pen pen,
      float x,
      float y,
      float width,
      float height,
      float startAngle,
      float sweepAngle)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawArc(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x, y, width, height, startAngle, sweepAngle));
    }

    /// <summary>Draws an arc representing a portion of an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" /></exception>
    public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle) => this.DrawArc(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);

    /// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
    /// <param name="width">Width of the rectangle that defines the ellipse.</param>
    /// <param name="height">Height of the rectangle that defines the ellipse.</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawArc(
      Pen pen,
      int x,
      int y,
      int width,
      int height,
      int startAngle,
      int sweepAngle)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawArcI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x, y, width, height, (float) startAngle, (float) sweepAngle));
    }

    /// <summary>Draws an arc representing a portion of an ellipse specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle) => this.DrawArc(pen, (float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height, startAngle, sweepAngle);

    /// <summary>Draws a Bézier spline defined by four ordered pairs of coordinates that represent points.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="x1">The x-coordinate of the starting point of the curve.</param>
    /// <param name="y1">The y-coordinate of the starting point of the curve.</param>
    /// <param name="x2">The x-coordinate of the first control point of the curve.</param>
    /// <param name="y2">The y-coordinate of the first control point of the curve.</param>
    /// <param name="x3">The x-coordinate of the second control point of the curve.</param>
    /// <param name="y3">The y-coordinate of the second control point of the curve.</param>
    /// <param name="x4">The x-coordinate of the ending point of the curve.</param>
    /// <param name="y4">The y-coordinate of the ending point of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawBezier(
      Pen pen,
      float x1,
      float y1,
      float x2,
      float y2,
      float x3,
      float y3,
      float x4,
      float y4)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawBezier(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x1, y1, x2, y2, x3, y3, x4, y4));
    }

    /// <summary>Draws a Bézier spline defined by four <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="pt1">
    /// <see cref="T:System.Drawing.PointF" /> structure that represents the starting point of the curve.</param>
    /// <param name="pt2">
    /// <see cref="T:System.Drawing.PointF" /> structure that represents the first control point for the curve.</param>
    /// <param name="pt3">
    /// <see cref="T:System.Drawing.PointF" /> structure that represents the second control point for the curve.</param>
    /// <param name="pt4">
    /// <see cref="T:System.Drawing.PointF" /> structure that represents the ending point of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4) => this.DrawBezier(pen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);

    /// <summary>Draws a series of Bézier splines from an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points that determine the curve. The number of points in the array should be a multiple of 3 plus 1, such as 4, 7, or 10.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawBeziers(Pen pen, PointF[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawBeziers(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a Bézier spline defined by four <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> structure that determines the color, width, and style of the curve.</param>
    /// <param name="pt1">
    /// <see cref="T:System.Drawing.Point" /> structure that represents the starting point of the curve.</param>
    /// <param name="pt2">
    /// <see cref="T:System.Drawing.Point" /> structure that represents the first control point for the curve.</param>
    /// <param name="pt3">
    /// <see cref="T:System.Drawing.Point" /> structure that represents the second control point for the curve.</param>
    /// <param name="pt4">
    /// <see cref="T:System.Drawing.Point" /> structure that represents the ending point of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4) => this.DrawBezier(pen, (float) pt1.X, (float) pt1.Y, (float) pt2.X, (float) pt2.Y, (float) pt3.X, (float) pt3.Y, (float) pt4.X, (float) pt4.Y);

    /// <summary>Draws a series of Bézier splines from an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the points that determine the curve. The number of points in the array should be a multiple of 3 plus 1, such as 4, 7, or 10.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawBeziers(Pen pen, Point[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawBeziersI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle.</param>
    /// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to draw.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawRectangle(Pen pen, Rectangle rect) => this.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
    /// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
    /// <param name="width">The width of the rectangle to draw.</param>
    /// <param name="height">The height of the rectangle to draw.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawRectangle(Pen pen, float x, float y, float width, float height)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawRectangle(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x, y, width, height));
    }

    /// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
    /// <param name="width">Width of the rectangle to draw.</param>
    /// <param name="height">Height of the rectangle to draw.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawRectangle(Pen pen, int x, int y, int width, int height)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawRectangleI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x, y, width, height));
    }

    /// <summary>Draws a series of rectangles specified by <see cref="T:System.Drawing.RectangleF" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the outlines of the rectangles.</param>
    /// <param name="rects">Array of <see cref="T:System.Drawing.RectangleF" /> structures that represent the rectangles to draw.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="rects" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rects" /> is a zero-length array.</exception>
    public void DrawRectangles(Pen pen, RectangleF[] rects)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = rects != null ? SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects) : throw new ArgumentNullException(nameof (rects));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawRectangles(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), rects.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a series of rectangles specified by <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the outlines of the rectangles.</param>
    /// <param name="rects">Array of <see cref="T:System.Drawing.Rectangle" /> structures that represent the rectangles to draw.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="rects" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rects" /> is a zero-length array.</exception>
    public void DrawRectangles(Pen pen, Rectangle[] rects)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = rects != null ? SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects) : throw new ArgumentNullException(nameof (rects));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawRectanglesI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), rects.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws an ellipse defined by a bounding <see cref="T:System.Drawing.RectangleF" />.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawEllipse(Pen pen, RectangleF rect) => this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Draws an ellipse defined by a bounding rectangle specified by a pair of coordinates, a height, and a width.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
    /// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawEllipse(Pen pen, float x, float y, float width, float height)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawEllipse(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x, y, width, height));
    }

    /// <summary>Draws an ellipse specified by a bounding <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that defines the boundaries of the ellipse.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawEllipse(Pen pen, Rectangle rect) => this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Draws an ellipse defined by a bounding rectangle specified by coordinates for the upper-left corner of the rectangle, a height, and a width.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
    /// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawEllipse(Pen pen, int x, int y, int width, int height)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawEllipseI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x, y, width, height));
    }

    /// <summary>Draws a pie shape defined by an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure and two radial lines.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
    /// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle) => this.DrawPie(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);

    /// <summary>Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial lines.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
    /// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawPie(
      Pen pen,
      float x,
      float y,
      float width,
      float height,
      float startAngle,
      float sweepAngle)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawPie(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x, y, width, height, startAngle, sweepAngle));
    }

    /// <summary>Draws a pie shape defined by an ellipse specified by a <see cref="T:System.Drawing.Rectangle" /> structure and two radial lines.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
    /// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle) => this.DrawPie(pen, (float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height, startAngle, sweepAngle);

    /// <summary>Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial lines.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
    /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
    /// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawPie(
      Pen pen,
      int x,
      int y,
      int width,
      int height,
      int startAngle,
      int sweepAngle)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawPieI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), x, y, width, height, (float) startAngle, (float) sweepAngle));
    }

    /// <summary>Draws a polygon defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the polygon.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawPolygon(Pen pen, PointF[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawPolygon(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a polygon defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the polygon.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pen" /> is <see langword="null" />.</exception>
    public void DrawPolygon(Pen pen, Point[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawPolygonI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the path.</param>
    /// <param name="path">
    /// <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to draw.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="path" /> is <see langword="null" />.</exception>
    public void DrawPath(Pen pen, GraphicsPath path)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawPath(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) path, path.nativePath)));
    }

    /// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawCurve(Pen pen, PointF[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawCurve(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points that define the curve.</param>
    /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawCurve(Pen pen, PointF[] points, float tension)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawCurve2(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length, tension));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures. The drawing begins offset from the beginning of the array.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
    /// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve.</param>
    /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments) => this.DrawCurve(pen, points, offset, numberOfSegments, 0.5f);

    /// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension. The drawing begins offset from the beginning of the array.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
    /// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve.</param>
    /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
    /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawCurve(
      Pen pen,
      PointF[] points,
      int offset,
      int numberOfSegments,
      float tension)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawCurve3(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length, offset, numberOfSegments, tension));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawCurve(Pen pen, Point[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawCurveI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
    /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawCurve(Pen pen, Point[] points, float tension)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawCurve2I(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length, tension));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
    /// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve.</param>
    /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
    /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawCurve(
      Pen pen,
      Point[] points,
      int offset,
      int numberOfSegments,
      float tension)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawCurve3I(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length, offset, numberOfSegments, tension));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawClosedCurve(Pen pen, PointF[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawClosedCurve(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
    /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
    /// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. This parameter is required but is ignored.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawClosedCurve2(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length, tension));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawClosedCurve(Pen pen, Point[] points)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawClosedCurveI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
    /// <param name="pen">
    /// <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
    /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
    /// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. This parameter is required but ignored.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="pen" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
    {
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawClosedCurve2I(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) pen, pen.NativePen), new HandleRef((object) this, num), points.Length, tension));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Clears the entire drawing surface and fills it with the specified background color.</summary>
    /// <param name="color">
    /// <see cref="T:System.Drawing.Color" /> structure that represents the background color of the drawing surface.</param>
    public void Clear(Color color)
    {
      int status = SafeNativeMethods.Gdip.GdipGraphicsClear(new HandleRef((object) this, this.NativeGraphics), color.ToArgb());
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Fills the interior of a rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillRectangle(Brush brush, RectangleF rect) => this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
    /// <param name="width">Width of the rectangle to fill.</param>
    /// <param name="height">Height of the rectangle to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillRectangle(Brush brush, float x, float y, float width, float height)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillRectangle(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), x, y, width, height));
    }

    /// <summary>Fills the interior of a rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillRectangle(Brush brush, Rectangle rect) => this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
    /// <param name="width">Width of the rectangle to fill.</param>
    /// <param name="height">Height of the rectangle to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillRectangle(Brush brush, int x, int y, int width, int height)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillRectangleI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), x, y, width, height));
    }

    /// <summary>Fills the interiors of a series of rectangles specified by <see cref="T:System.Drawing.RectangleF" /> structures.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="rects">Array of <see cref="T:System.Drawing.RectangleF" /> structures that represent the rectangles to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="rects" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Rects" /> is a zero-length array.</exception>
    public void FillRectangles(Brush brush, RectangleF[] rects)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      IntPtr num = rects != null ? SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects) : throw new ArgumentNullException(nameof (rects));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillRectangles(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) this, num), rects.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Fills the interiors of a series of rectangles specified by <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="rects">Array of <see cref="T:System.Drawing.Rectangle" /> structures that represent the rectangles to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="rects" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rects" /> is a zero-length array.</exception>
    public void FillRectangles(Brush brush, Rectangle[] rects)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      IntPtr num = rects != null ? SafeNativeMethods.Gdip.ConvertRectangleToMemory(rects) : throw new ArgumentNullException(nameof (rects));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillRectanglesI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) this, num), rects.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillPolygon(Brush brush, PointF[] points) => this.FillPolygon(brush, points, FillMode.Alternate);

    /// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon to fill.</param>
    /// <param name="fillMode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines the style of the fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillPolygon(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) this, num), points.Length, (int) fillMode));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillPolygon(Brush brush, Point[] points) => this.FillPolygon(brush, points, FillMode.Alternate);

    /// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.Point" /> structures using the specified fill mode.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon to fill.</param>
    /// <param name="fillMode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines the style of the fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillPolygonI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) this, num), points.Length, (int) fillMode));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle that defines the ellipse.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillEllipse(Brush brush, RectangleF rect) => this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
    /// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillEllipse(Brush brush, float x, float y, float width, float height)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillEllipse(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), x, y, width, height));
    }

    /// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillEllipse(Brush brush, Rectangle rect) => this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
    /// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
    /// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillEllipse(Brush brush, int x, int y, int width, int height)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillEllipseI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), x, y, width, height));
    }

    /// <summary>Fills the interior of a pie section defined by an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure and two radial lines.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle) => this.FillPie(brush, (float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height, startAngle, sweepAngle);

    /// <summary>Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a height, and two radial lines.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillPie(
      Brush brush,
      float x,
      float y,
      float width,
      float height,
      float startAngle,
      float sweepAngle)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillPie(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), x, y, width, height, startAngle, sweepAngle));
    }

    /// <summary>Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a height, and two radial lines.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="brush" /> is <see langword="null" />.</exception>
    public void FillPie(
      Brush brush,
      int x,
      int y,
      int width,
      int height,
      int startAngle,
      int sweepAngle)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillPieI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), x, y, width, height, (float) startAngle, (float) sweepAngle));
    }

    /// <summary>Fills the interior of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="path">
    /// <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that represents the path to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="path" /> is <see langword="null" />.</exception>
    public void FillPath(Brush brush, GraphicsPath path)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillPath(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) path, path.nativePath)));
    }

    /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillClosedCurve(Brush brush, PointF[] points)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillClosedCurve(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
    /// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode) => this.FillClosedCurve(brush, points, fillmode, 0.5f);

    /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode and tension.</summary>
    /// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
    /// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled.</param>
    /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillClosedCurve2(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) this, num), points.Length, tension, (int) fillmode));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillClosedCurve(Brush brush, Point[] points)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillClosedCurveI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) this, num), points.Length));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures using the specified fill mode.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
    /// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode) => this.FillClosedCurve(brush, points, fillmode, 0.5f);

    /// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures using the specified fill mode and tension.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
    /// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled.</param>
    /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="points" /> is <see langword="null" />.</exception>
    public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      IntPtr num = points != null ? SafeNativeMethods.Gdip.ConvertPointToMemory(points) : throw new ArgumentNullException(nameof (points));
      try
      {
        this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillClosedCurve2I(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) this, num), points.Length, tension, (int) fillmode));
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    /// <summary>Fills the interior of a <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
    /// <param name="region">
    /// <see cref="T:System.Drawing.Region" /> that represents the area to fill.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="region" /> is <see langword="null" />.</exception>
    public void FillRegion(Brush brush, Region region)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      if (region == null)
        throw new ArgumentNullException(nameof (region));
      this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipFillRegion(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) brush, brush.NativeBrush), new HandleRef((object) region, region.nativeRegion)));
    }

    /// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
    /// <param name="s">String to draw.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn text.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn text.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="s" /> is <see langword="null" />.</exception>
    public void DrawString(string s, Font font, Brush brush, float x, float y) => this.DrawString(s, font, brush, new RectangleF(x, y, 0.0f, 0.0f), (StringFormat) null);

    /// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
    /// <param name="s">String to draw.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
    /// <param name="point">
    /// <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="s" /> is <see langword="null" />.</exception>
    public void DrawString(string s, Font font, Brush brush, PointF point) => this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0.0f, 0.0f), (StringFormat) null);

    /// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
    /// <param name="s">String to draw.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn text.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn text.</param>
    /// <param name="format">
    /// <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="s" /> is <see langword="null" />.</exception>
    public void DrawString(
      string s,
      Font font,
      Brush brush,
      float x,
      float y,
      StringFormat format)
    {
      this.DrawString(s, font, brush, new RectangleF(x, y, 0.0f, 0.0f), format);
    }

    /// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
    /// <param name="s">String to draw.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
    /// <param name="point">
    /// <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text.</param>
    /// <param name="format">
    /// <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="s" /> is <see langword="null" />.</exception>
    public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format) => this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0.0f, 0.0f), format);

    /// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
    /// <param name="s">String to draw.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
    /// <param name="layoutRectangle">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="s" /> is <see langword="null" />.</exception>
    public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle) => this.DrawString(s, font, brush, layoutRectangle, (StringFormat) null);

    /// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
    /// <param name="s">String to draw.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="brush">
    /// <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
    /// <param name="layoutRectangle">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text.</param>
    /// <param name="format">
    /// <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///         <paramref name="brush" /> is <see langword="null" />.
    /// -or-
    /// <paramref name="s" /> is <see langword="null" />.</exception>
    public void DrawString(
      string s,
      Font font,
      Brush brush,
      RectangleF layoutRectangle,
      StringFormat format)
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      switch (s)
      {
        case null:
          break;
        case "":
          break;
        default:
          if (font == null)
            throw new ArgumentNullException(nameof (font));
          GPRECTF layoutRect = new GPRECTF(layoutRectangle);
          IntPtr handle = format == null ? IntPtr.Zero : format.nativeFormat;
          this.CheckErrorStatus(SafeNativeMethods.Gdip.GdipDrawString(new HandleRef((object) this, this.NativeGraphics), s, s.Length, new HandleRef((object) font, font.NativeFont), ref layoutRect, new HandleRef((object) format, handle), new HandleRef((object) brush, brush.NativeBrush)));
          break;
      }
    }

    /// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
    /// <param name="text">String to measure.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="layoutArea">
    /// <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.</param>
    /// <param name="stringFormat">
    /// <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
    /// <param name="charactersFitted">Number of characters in the string.</param>
    /// <param name="linesFilled">Number of text lines in the string.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size of the string, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="font" /> is <see langword="null" />.</exception>
    public SizeF MeasureString(
      string text,
      Font font,
      SizeF layoutArea,
      StringFormat stringFormat,
      out int charactersFitted,
      out int linesFilled)
    {
      if (text == null || text.Length == 0)
      {
        charactersFitted = 0;
        linesFilled = 0;
        return new SizeF(0.0f, 0.0f);
      }
      if (font == null)
        throw new ArgumentNullException(nameof (font));
      GPRECTF layoutRect = new GPRECTF(0.0f, 0.0f, layoutArea.Width, layoutArea.Height);
      GPRECTF boundingBox = new GPRECTF();
      int status = SafeNativeMethods.Gdip.GdipMeasureString(new HandleRef((object) this, this.NativeGraphics), text, text.Length, new HandleRef((object) font, font.NativeFont), ref layoutRect, new HandleRef((object) stringFormat, stringFormat == null ? IntPtr.Zero : stringFormat.nativeFormat), ref boundingBox, out charactersFitted, out linesFilled);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boundingBox.SizeF;
    }

    /// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
    /// <param name="text">String to measure.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> defines the text format of the string.</param>
    /// <param name="origin">
    /// <see cref="T:System.Drawing.PointF" /> structure that represents the upper-left corner of the string.</param>
    /// <param name="stringFormat">
    /// <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="font" /> is <see langword="null" />.</exception>
    public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
    {
      if (text == null || text.Length == 0)
        return new SizeF(0.0f, 0.0f);
      if (font == null)
        throw new ArgumentNullException(nameof (font));
      GPRECTF layoutRect = new GPRECTF();
      GPRECTF boundingBox = new GPRECTF();
      layoutRect.X = origin.X;
      layoutRect.Y = origin.Y;
      layoutRect.Width = 0.0f;
      layoutRect.Height = 0.0f;
      int status = SafeNativeMethods.Gdip.GdipMeasureString(new HandleRef((object) this, this.NativeGraphics), text, text.Length, new HandleRef((object) font, font.NativeFont), ref layoutRect, new HandleRef((object) stringFormat, stringFormat == null ? IntPtr.Zero : stringFormat.nativeFormat), ref boundingBox, out int _, out int _);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boundingBox.SizeF;
    }

    /// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> within the specified layout area.</summary>
    /// <param name="text">String to measure.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> defines the text format of the string.</param>
    /// <param name="layoutArea">
    /// <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="font" /> is <see langword="null" />.</exception>
    public SizeF MeasureString(string text, Font font, SizeF layoutArea) => this.MeasureString(text, font, layoutArea, (StringFormat) null);

    /// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
    /// <param name="text">String to measure.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> defines the text format of the string.</param>
    /// <param name="layoutArea">
    /// <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.</param>
    /// <param name="stringFormat">
    /// <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="font" /> is <see langword="null" />.</exception>
    public SizeF MeasureString(
      string text,
      Font font,
      SizeF layoutArea,
      StringFormat stringFormat)
    {
      if (text == null || text.Length == 0)
        return new SizeF(0.0f, 0.0f);
      if (font == null)
        throw new ArgumentNullException(nameof (font));
      GPRECTF layoutRect = new GPRECTF(0.0f, 0.0f, layoutArea.Width, layoutArea.Height);
      GPRECTF boundingBox = new GPRECTF();
      int status = SafeNativeMethods.Gdip.GdipMeasureString(new HandleRef((object) this, this.NativeGraphics), text, text.Length, new HandleRef((object) font, font.NativeFont), ref layoutRect, new HandleRef((object) stringFormat, stringFormat == null ? IntPtr.Zero : stringFormat.nativeFormat), ref boundingBox, out int _, out int _);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boundingBox.SizeF;
    }

    /// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" />.</summary>
    /// <param name="text">String to measure.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="font" /> is <see langword="null" />.</exception>
    public SizeF MeasureString(string text, Font font) => this.MeasureString(text, font, new SizeF(0.0f, 0.0f));

    /// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" />.</summary>
    /// <param name="text">String to measure.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the format of the string.</param>
    /// <param name="width">Maximum width of the string in pixels.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="font" /> is <see langword="null" />.</exception>
    public SizeF MeasureString(string text, Font font, int width) => this.MeasureString(text, font, new SizeF((float) width, 999999f));

    /// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
    /// <param name="text">String to measure.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="width">Maximum width of the string.</param>
    /// <param name="format">
    /// <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="font" /> is <see langword="null" />.</exception>
    public SizeF MeasureString(string text, Font font, int width, StringFormat format) => this.MeasureString(text, font, new SizeF((float) width, 999999f), format);

    /// <summary>Gets an array of <see cref="T:System.Drawing.Region" /> objects, each of which bounds a range of character positions within the specified string.</summary>
    /// <param name="text">String to measure.</param>
    /// <param name="font">
    /// <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
    /// <param name="layoutRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the layout rectangle for the string.</param>
    /// <param name="stringFormat">
    /// <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
    /// <returns>This method returns an array of <see cref="T:System.Drawing.Region" /> objects, each of which bounds a range of character positions within the specified string.</returns>
    public Region[] MeasureCharacterRanges(
      string text,
      Font font,
      RectangleF layoutRect,
      StringFormat stringFormat)
    {
      if (text == null || text.Length == 0)
        return new Region[0];
      if (font == null)
        throw new ArgumentNullException(nameof (font));
      int count;
      int characterRangeCount = SafeNativeMethods.Gdip.GdipGetStringFormatMeasurableCharacterRangeCount(new HandleRef((object) stringFormat, stringFormat == null ? IntPtr.Zero : stringFormat.nativeFormat), out count);
      if (characterRangeCount != 0)
        throw SafeNativeMethods.Gdip.StatusException(characterRangeCount);
      IntPtr[] region = new IntPtr[count];
      GPRECTF layoutRect1 = new GPRECTF(layoutRect);
      Region[] regionArray = new Region[count];
      for (int index = 0; index < count; ++index)
      {
        regionArray[index] = new Region();
        region[index] = regionArray[index].nativeRegion;
      }
      int status = SafeNativeMethods.Gdip.GdipMeasureCharacterRanges(new HandleRef((object) this, this.NativeGraphics), text, text.Length, new HandleRef((object) font, font.NativeFont), ref layoutRect1, new HandleRef((object) stringFormat, stringFormat == null ? IntPtr.Zero : stringFormat.nativeFormat), count, region);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return regionArray;
    }

    /// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> at the specified coordinates.</summary>
    /// <param name="icon">
    /// <see cref="T:System.Drawing.Icon" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="icon" /> is <see langword="null" />.</exception>
    public void DrawIcon(Icon icon, int x, int y)
    {
      if (icon == null)
        throw new ArgumentNullException(nameof (icon));
      if (this.backingImage != null)
        this.DrawImage((Image) icon.ToBitmap(), x, y);
      else
        icon.Draw(this, x, y);
    }

    /// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> within the area specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="icon">
    /// <see cref="T:System.Drawing.Icon" /> to draw.</param>
    /// <param name="targetRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image on the display surface. The image contained in the <paramref name="icon" /> parameter is scaled to the dimensions of this rectangular area.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="icon" /> is <see langword="null" />.</exception>
    public void DrawIcon(Icon icon, Rectangle targetRect)
    {
      if (icon == null)
        throw new ArgumentNullException(nameof (icon));
      if (this.backingImage != null)
        this.DrawImage((Image) icon.ToBitmap(), targetRect);
      else
        icon.Draw(this, targetRect);
    }

    /// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> without scaling the image.</summary>
    /// <param name="icon">
    /// <see cref="T:System.Drawing.Icon" /> to draw.</param>
    /// <param name="targetRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image. The image is not scaled to fit this rectangle, but retains its original size. If the image is larger than the rectangle, it is clipped to fit inside it.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="icon" /> is <see langword="null" />.</exception>
    public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
    {
      if (icon == null)
        throw new ArgumentNullException(nameof (icon));
      if (this.backingImage != null)
        this.DrawImageUnscaled((Image) icon.ToBitmap(), targetRect);
      else
        icon.DrawUnstretched(this, targetRect);
    }

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="point">
    /// <see cref="T:System.Drawing.PointF" /> structure that represents the upper-left corner of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, PointF point) => this.DrawImage(image, point.X, point.Y);

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, float x, float y)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImage(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), x, y);
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, RectangleF rect) => this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="width">Width of the drawn image.</param>
    /// <param name="height">Height of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, float x, float y, float width, float height)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImageRect(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), x, y, width, height);
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="point">
    /// <see cref="T:System.Drawing.Point" /> structure that represents the location of the upper-left corner of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, Point point) => this.DrawImage(image, point.X, point.Y);

    /// <summary>Draws the specified image, using its original physical size, at the location specified by a coordinate pair.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, int x, int y)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImageI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), x, y);
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, Rectangle rect) => this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="width">Width of the drawn image.</param>
    /// <param name="height">Height of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, int x, int y, int width, int height)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImageRectI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), x, y, width, height);
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="point">
    /// <see cref="T:System.Drawing.Point" /> structure that specifies the upper-left corner of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImageUnscaled(Image image, Point point) => this.DrawImage(image, point.X, point.Y);

    /// <summary>Draws the specified image using its original physical size at the location specified by a coordinate pair.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImageUnscaled(Image image, int x, int y) => this.DrawImage(image, x, y);

    /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> that specifies the upper-left corner of the drawn image. The X and Y properties of the rectangle specify the upper-left corner. The Width and Height properties are ignored.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImageUnscaled(Image image, Rectangle rect) => this.DrawImage(image, rect.X, rect.Y);

    /// <summary>Draws a specified image using its original physical size at a specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="width">Not used.</param>
    /// <param name="height">Not used.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImageUnscaled(Image image, int x, int y, int width, int height) => this.DrawImage(image, x, y);

    /// <summary>Draws the specified image without scaling and clips it, if necessary, to fit in the specified rectangle.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the image.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int srcWidth = Math.Min(rect.Width, image.Width);
      int srcHeight = Math.Min(rect.Height, image.Height);
      this.DrawImage(image, rect, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
    }

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified shape and size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, PointF[] destPoints)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int length = destPoints.Length;
      switch (length)
      {
        case 3:
        case 4:
          IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
          try
          {
            int errorStatus = SafeNativeMethods.Gdip.GdipDrawImagePoints(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), new HandleRef((object) this, memory), length);
            this.IgnoreMetafileErrors(image, ref errorStatus);
            this.CheckErrorStatus(errorStatus);
            break;
          }
          finally
          {
            Marshal.FreeHGlobal(memory);
          }
        default:
          throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidLength"));
      }
    }

    /// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified shape and size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, Point[] destPoints)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int length = destPoints.Length;
      switch (length)
      {
        case 3:
        case 4:
          IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
          try
          {
            int errorStatus = SafeNativeMethods.Gdip.GdipDrawImagePointsI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), new HandleRef((object) this, memory), length);
            this.IgnoreMetafileErrors(image, ref errorStatus);
            this.CheckErrorStatus(errorStatus);
            break;
          }
          finally
          {
            Marshal.FreeHGlobal(memory);
          }
        default:
          throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidLength"));
      }
    }

    /// <summary>Draws a portion of an image at a specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      float x,
      float y,
      RectangleF srcRect,
      GraphicsUnit srcUnit)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImagePointRect(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), x, y, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (int) srcUnit);
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws a portion of an image at a specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImagePointRectI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), x, y, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (int) srcUnit);
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      RectangleF destRect,
      RectangleF srcRect,
      GraphicsUnit srcUnit)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImageRectRect(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (int) srcUnit, NativeMethods.NullHandleRef, (Graphics.DrawImageAbort) null, NativeMethods.NullHandleRef);
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      Rectangle srcRect,
      GraphicsUnit srcUnit)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImageRectRectI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (int) srcUnit, NativeMethods.NullHandleRef, (Graphics.DrawImageAbort) null, NativeMethods.NullHandleRef);
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      PointF[] destPoints,
      RectangleF srcRect,
      GraphicsUnit srcUnit)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      switch (destPoints.Length)
      {
        case 3:
        case 4:
          IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
          try
          {
            int errorStatus = SafeNativeMethods.Gdip.GdipDrawImagePointsRect(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), new HandleRef((object) this, memory), destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (int) srcUnit, NativeMethods.NullHandleRef, (Graphics.DrawImageAbort) null, NativeMethods.NullHandleRef);
            this.IgnoreMetafileErrors(image, ref errorStatus);
            this.CheckErrorStatus(errorStatus);
            break;
          }
          finally
          {
            Marshal.FreeHGlobal(memory);
          }
        default:
          throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidLength"));
      }
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      PointF[] destPoints,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttr)
    {
      this.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, (Graphics.DrawImageAbort) null, 0);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      PointF[] destPoints,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttr,
      Graphics.DrawImageAbort callback)
    {
      this.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, 0);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method according to application-determined criteria.</param>
    /// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      PointF[] destPoints,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttr,
      Graphics.DrawImageAbort callback,
      int callbackData)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      switch (destPoints.Length)
      {
        case 3:
        case 4:
          IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
          try
          {
            int errorStatus = SafeNativeMethods.Gdip.GdipDrawImagePointsRect(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), new HandleRef((object) this, memory), destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (int) srcUnit, new HandleRef((object) imageAttr, imageAttr != null ? imageAttr.nativeImageAttributes : IntPtr.Zero), callback, new HandleRef((object) null, (IntPtr) callbackData));
            this.IgnoreMetafileErrors(image, ref errorStatus);
            this.CheckErrorStatus(errorStatus);
            break;
          }
          finally
          {
            Marshal.FreeHGlobal(memory);
          }
        default:
          throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidLength"));
      }
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Point[] destPoints,
      Rectangle srcRect,
      GraphicsUnit srcUnit)
    {
      this.DrawImage(image, destPoints, srcRect, srcUnit, (ImageAttributes) null, (Graphics.DrawImageAbort) null, 0);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Point[] destPoints,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttr)
    {
      this.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, (Graphics.DrawImageAbort) null, 0);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Point[] destPoints,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttr,
      Graphics.DrawImageAbort callback)
    {
      this.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, 0);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method according to application-determined criteria.</param>
    /// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method.</param>
    public void DrawImage(
      Image image,
      Point[] destPoints,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttr,
      Graphics.DrawImageAbort callback,
      int callbackData)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      switch (destPoints.Length)
      {
        case 3:
        case 4:
          IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
          try
          {
            int errorStatus = SafeNativeMethods.Gdip.GdipDrawImagePointsRectI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), new HandleRef((object) this, memory), destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (int) srcUnit, new HandleRef((object) imageAttr, imageAttr != null ? imageAttr.nativeImageAttributes : IntPtr.Zero), callback, new HandleRef((object) null, (IntPtr) callbackData));
            this.IgnoreMetafileErrors(image, ref errorStatus);
            this.CheckErrorStatus(errorStatus);
            break;
          }
          finally
          {
            Marshal.FreeHGlobal(memory);
          }
        default:
          throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidLength"));
      }
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcWidth">Width of the portion of the source image to draw.</param>
    /// <param name="srcHeight">Height of the portion of the source image to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      float srcX,
      float srcY,
      float srcWidth,
      float srcHeight,
      GraphicsUnit srcUnit)
    {
      this.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, (ImageAttributes) null);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcWidth">Width of the portion of the source image to draw.</param>
    /// <param name="srcHeight">Height of the portion of the source image to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
    /// <param name="imageAttrs">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      float srcX,
      float srcY,
      float srcWidth,
      float srcHeight,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttrs)
    {
      this.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, (Graphics.DrawImageAbort) null);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcWidth">Width of the portion of the source image to draw.</param>
    /// <param name="srcHeight">Height of the portion of the source image to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
    /// <param name="imageAttrs">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      float srcX,
      float srcY,
      float srcWidth,
      float srcHeight,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttrs,
      Graphics.DrawImageAbort callback)
    {
      this.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback, IntPtr.Zero);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcWidth">Width of the portion of the source image to draw.</param>
    /// <param name="srcHeight">Height of the portion of the source image to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
    /// <param name="imageAttrs">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)" /> method according to application-determined criteria.</param>
    /// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see langword="DrawImage" /> method.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      float srcX,
      float srcY,
      float srcWidth,
      float srcHeight,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttrs,
      Graphics.DrawImageAbort callback,
      IntPtr callbackData)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImageRectRect(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), (float) destRect.X, (float) destRect.Y, (float) destRect.Width, (float) destRect.Height, srcX, srcY, srcWidth, srcHeight, (int) srcUnit, new HandleRef((object) imageAttrs, imageAttrs != null ? imageAttrs.nativeImageAttributes : IntPtr.Zero), callback, new HandleRef((object) null, callbackData));
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcWidth">Width of the portion of the source image to draw.</param>
    /// <param name="srcHeight">Height of the portion of the source image to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      int srcX,
      int srcY,
      int srcWidth,
      int srcHeight,
      GraphicsUnit srcUnit)
    {
      this.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, (ImageAttributes) null);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcWidth">Width of the portion of the source image to draw.</param>
    /// <param name="srcHeight">Height of the portion of the source image to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      int srcX,
      int srcY,
      int srcWidth,
      int srcHeight,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttr)
    {
      this.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr, (Graphics.DrawImageAbort) null);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcWidth">Width of the portion of the source image to draw.</param>
    /// <param name="srcHeight">Height of the portion of the source image to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for <paramref name="image" />.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      int srcX,
      int srcY,
      int srcWidth,
      int srcHeight,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttr,
      Graphics.DrawImageAbort callback)
    {
      this.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr, callback, IntPtr.Zero);
    }

    /// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
    /// <param name="image">
    /// <see cref="T:System.Drawing.Image" /> to draw.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
    /// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
    /// <param name="srcWidth">Width of the portion of the source image to draw.</param>
    /// <param name="srcHeight">Height of the portion of the source image to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
    /// <param name="imageAttrs">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)" /> method according to application-determined criteria.</param>
    /// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see langword="DrawImage" /> method.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="image" /> is <see langword="null" />.</exception>
    public void DrawImage(
      Image image,
      Rectangle destRect,
      int srcX,
      int srcY,
      int srcWidth,
      int srcHeight,
      GraphicsUnit srcUnit,
      ImageAttributes imageAttrs,
      Graphics.DrawImageAbort callback,
      IntPtr callbackData)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      int errorStatus = SafeNativeMethods.Gdip.GdipDrawImageRectRectI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) image, image.nativeImage), destRect.X, destRect.Y, destRect.Width, destRect.Height, srcX, srcY, srcWidth, srcHeight, (int) srcUnit, new HandleRef((object) imageAttrs, imageAttrs != null ? imageAttrs.nativeImageAttributes : IntPtr.Zero), callback, new HandleRef((object) null, callbackData));
      this.IgnoreMetafileErrors(image, ref errorStatus);
      this.CheckErrorStatus(errorStatus);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF destPoint,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destPoint, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF destPoint,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destPoint, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF destPoint,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestPoint(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), new GPPOINTF(destPoint), callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point destPoint,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destPoint, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point destPoint,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destPoint, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point destPoint,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestPointI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), new GPPOINT(destPoint), callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      RectangleF destRect,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destRect, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      RectangleF destRect,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destRect, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      RectangleF destRect,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      GPRECTF destRect1 = new GPRECTF(destRect);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestRect(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), ref destRect1, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Rectangle destRect,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destRect, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Rectangle destRect,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destRect, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Rectangle destRect,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      GPRECT destRect1 = new GPRECT(destRect);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestRectI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), ref destRect1, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF[] destPoints,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destPoints, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF[] destPoints,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destPoints, callback, IntPtr.Zero, (ImageAttributes) null);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF[] destPoints,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (destPoints.Length != 3)
        throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidParallelogram"));
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestPoints(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), memory, destPoints.Length, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      Marshal.FreeHGlobal(memory);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point[] destPoints,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destPoints, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point[] destPoints,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destPoints, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point[] destPoints,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (destPoints.Length != 3)
        throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidParallelogram"));
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileDestPointsI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), memory, destPoints.Length, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      Marshal.FreeHGlobal(memory);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF destPoint,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF destPoint,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF destPoint,
      RectangleF srcRect,
      GraphicsUnit unit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      GPRECTF srcRect1 = new GPRECTF(srcRect);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestPoint(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), new GPPOINTF(destPoint), ref srcRect1, (int) unit, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point destPoint,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point destPoint,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoint">
    /// <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point destPoint,
      Rectangle srcRect,
      GraphicsUnit unit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      GPPOINT destPoint1 = new GPPOINT(destPoint);
      GPRECT srcRect1 = new GPRECT(srcRect);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestPointI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), destPoint1, ref srcRect1, (int) unit, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      RectangleF destRect,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      RectangleF destRect,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      RectangleF destRect,
      RectangleF srcRect,
      GraphicsUnit unit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      GPRECTF destRect1 = new GPRECTF(destRect);
      GPRECTF srcRect1 = new GPRECTF(srcRect);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestRect(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), ref destRect1, ref srcRect1, (int) unit, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Rectangle destRect,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Rectangle destRect,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Rectangle destRect,
      Rectangle srcRect,
      GraphicsUnit unit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      GPRECT destRect1 = new GPRECT(destRect);
      GPRECT srcRect1 = new GPRECT(srcRect);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestRectI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), ref destRect1, ref srcRect1, (int) unit, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structures that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF[] destPoints,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF[] destPoints,
      RectangleF srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      PointF[] destPoints,
      RectangleF srcRect,
      GraphicsUnit unit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (destPoints.Length != 3)
        throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidParallelogram"));
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
      GPRECTF srcRect1 = new GPRECTF(srcRect);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestPoints(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), memory, destPoints.Length, ref srcRect1, (int) unit, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      Marshal.FreeHGlobal(memory);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point[] destPoints,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback)
    {
      this.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, IntPtr.Zero);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point[] destPoints,
      Rectangle srcRect,
      GraphicsUnit srcUnit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData)
    {
      this.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, callbackData, (ImageAttributes) null);
    }

    /// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
    /// <param name="metafile">
    /// <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
    /// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
    /// <param name="srcRect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
    /// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
    /// <param name="callback">
    /// <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
    /// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
    /// <param name="imageAttr">
    /// <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
    public void EnumerateMetafile(
      Metafile metafile,
      Point[] destPoints,
      Rectangle srcRect,
      GraphicsUnit unit,
      Graphics.EnumerateMetafileProc callback,
      IntPtr callbackData,
      ImageAttributes imageAttr)
    {
      if (destPoints == null)
        throw new ArgumentNullException(nameof (destPoints));
      if (destPoints.Length != 3)
        throw new ArgumentException(SR.GetString("GdiplusDestPointsInvalidParallelogram"));
      IntPtr handle1 = metafile == null ? IntPtr.Zero : metafile.nativeImage;
      IntPtr handle2 = imageAttr == null ? IntPtr.Zero : imageAttr.nativeImageAttributes;
      IntPtr memory = SafeNativeMethods.Gdip.ConvertPointToMemory(destPoints);
      GPRECT srcRect1 = new GPRECT(srcRect);
      int status = SafeNativeMethods.Gdip.GdipEnumerateMetafileSrcRectDestPointsI(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) metafile, handle1), memory, destPoints.Length, ref srcRect1, (int) unit, callback, new HandleRef((object) null, callbackData), new HandleRef((object) imageAttr, handle2));
      Marshal.FreeHGlobal(memory);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the <see langword="Clip" /> property of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="g">
    /// <see cref="T:System.Drawing.Graphics" /> from which to take the new clip region.</param>
    public void SetClip(Graphics g) => this.SetClip(g, CombineMode.Replace);

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified combining operation of the current clip region and the <see cref="P:System.Drawing.Graphics.Clip" /> property of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="g">
    /// <see cref="T:System.Drawing.Graphics" /> that specifies the clip region to combine.</param>
    /// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
    public void SetClip(Graphics g, CombineMode combineMode)
    {
      if (g == null)
        throw new ArgumentNullException(nameof (g));
      int status = SafeNativeMethods.Gdip.GdipSetClipGraphics(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) g, g.NativeGraphics), combineMode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that represents the new clip region.</param>
    public void SetClip(Rectangle rect) => this.SetClip(rect, CombineMode.Replace);

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure to combine.</param>
    /// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
    public void SetClip(Rectangle rect, CombineMode combineMode)
    {
      int status = SafeNativeMethods.Gdip.GdipSetClipRectI(new HandleRef((object) this, this.NativeGraphics), rect.X, rect.Y, rect.Width, rect.Height, combineMode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that represents the new clip region.</param>
    public void SetClip(RectangleF rect) => this.SetClip(rect, CombineMode.Replace);

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure to combine.</param>
    /// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
    public void SetClip(RectangleF rect, CombineMode combineMode)
    {
      int status = SafeNativeMethods.Gdip.GdipSetClipRect(new HandleRef((object) this, this.NativeGraphics), rect.X, rect.Y, rect.Width, rect.Height, combineMode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="path">
    /// <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that represents the new clip region.</param>
    public void SetClip(GraphicsPath path) => this.SetClip(path, CombineMode.Replace);

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
    /// <param name="path">
    /// <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to combine.</param>
    /// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
    public void SetClip(GraphicsPath path, CombineMode combineMode)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      int status = SafeNativeMethods.Gdip.GdipSetClipPath(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) path, path.nativePath), combineMode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the specified <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="region">
    /// <see cref="T:System.Drawing.Region" /> to combine.</param>
    /// <param name="combineMode">Member from the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
    public void SetClip(Region region, CombineMode combineMode)
    {
      if (region == null)
        throw new ArgumentNullException(nameof (region));
      int status = SafeNativeMethods.Gdip.GdipSetClipRegion(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) region, region.nativeRegion), combineMode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure to intersect with the current clip region.</param>
    public void IntersectClip(Rectangle rect)
    {
      int status = SafeNativeMethods.Gdip.GdipSetClipRectI(new HandleRef((object) this, this.NativeGraphics), rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Intersect);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure to intersect with the current clip region.</param>
    public void IntersectClip(RectangleF rect)
    {
      int status = SafeNativeMethods.Gdip.GdipSetClipRect(new HandleRef((object) this, this.NativeGraphics), rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Intersect);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="region">
    /// <see cref="T:System.Drawing.Region" /> to intersect with the current region.</param>
    public void IntersectClip(Region region)
    {
      if (region == null)
        throw new ArgumentNullException(nameof (region));
      int status = SafeNativeMethods.Gdip.GdipSetClipRegion(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) region, region.nativeRegion), CombineMode.Intersect);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to exclude the area specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that specifies the rectangle to exclude from the clip region.</param>
    public void ExcludeClip(Rectangle rect)
    {
      int status = SafeNativeMethods.Gdip.GdipSetClipRectI(new HandleRef((object) this, this.NativeGraphics), rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Exclude);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to exclude the area specified by a <see cref="T:System.Drawing.Region" />.</summary>
    /// <param name="region">
    /// <see cref="T:System.Drawing.Region" /> that specifies the region to exclude from the clip region.</param>
    public void ExcludeClip(Region region)
    {
      if (region == null)
        throw new ArgumentNullException(nameof (region));
      int status = SafeNativeMethods.Gdip.GdipSetClipRegion(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) region, region.nativeRegion), CombineMode.Exclude);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Resets the clip region of this <see cref="T:System.Drawing.Graphics" /> to an infinite region.</summary>
    public void ResetClip()
    {
      int status = SafeNativeMethods.Gdip.GdipResetClip(new HandleRef((object) this, this.NativeGraphics));
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Translates the clipping region of this <see cref="T:System.Drawing.Graphics" /> by specified amounts in the horizontal and vertical directions.</summary>
    /// <param name="dx">The x-coordinate of the translation.</param>
    /// <param name="dy">The y-coordinate of the translation.</param>
    public void TranslateClip(float dx, float dy)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslateClip(new HandleRef((object) this, this.NativeGraphics), dx, dy);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Translates the clipping region of this <see cref="T:System.Drawing.Graphics" /> by specified amounts in the horizontal and vertical directions.</summary>
    /// <param name="dx">The x-coordinate of the translation.</param>
    /// <param name="dy">The y-coordinate of the translation.</param>
    public void TranslateClip(int dx, int dy)
    {
      int status = SafeNativeMethods.Gdip.GdipTranslateClip(new HandleRef((object) this, this.NativeGraphics), (float) dx, (float) dy);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets the cumulative graphics context.</summary>
    /// <returns>An <see cref="T:System.Object" /> representing the cumulative graphics context.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [StrongNameIdentityPermission(SecurityAction.LinkDemand, Name = "System.Windows.Forms", PublicKey = "0x00000000000000000400000000000000")]
    public object GetContextInfo()
    {
      Region clip = this.Clip;
      Matrix transform = this.Transform;
      PointF pointF = PointF.Empty;
      PointF empty = PointF.Empty;
      if (!transform.IsIdentity)
      {
        float[] elements = transform.Elements;
        pointF.X = elements[4];
        pointF.Y = elements[5];
      }
      GraphicsContext graphicsContext = this.previousContext;
      while (graphicsContext != null)
      {
        PointF transformOffset = graphicsContext.TransformOffset;
        if (!transformOffset.IsEmpty)
        {
          Matrix matrix = transform;
          transformOffset = graphicsContext.TransformOffset;
          double x = (double) transformOffset.X;
          transformOffset = graphicsContext.TransformOffset;
          double y = (double) transformOffset.Y;
          matrix.Translate((float) x, (float) y);
        }
        if (!pointF.IsEmpty)
        {
          clip.Translate(pointF.X, pointF.Y);
          empty.X += pointF.X;
          empty.Y += pointF.Y;
        }
        if (graphicsContext.Clip != null)
          clip.Intersect(graphicsContext.Clip);
        pointF = graphicsContext.TransformOffset;
label_10:
        graphicsContext = graphicsContext.Previous;
        if (graphicsContext != null && graphicsContext.Next.IsCumulative && graphicsContext.IsCumulative)
          goto label_10;
      }
      if (!empty.IsEmpty)
        clip.Translate(-empty.X, -empty.Y);
      return (object) new object[2]
      {
        (object) clip,
        (object) transform
      };
    }

    /// <summary>Gets or sets a <see cref="T:System.Drawing.Region" /> that limits the drawing region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.Region" /> that limits the portion of this <see cref="T:System.Drawing.Graphics" /> that is currently available for drawing.</returns>
    public Region Clip
    {
      get
      {
        Region wrapper = new Region();
        int clip = SafeNativeMethods.Gdip.GdipGetClip(new HandleRef((object) this, this.NativeGraphics), new HandleRef((object) wrapper, wrapper.nativeRegion));
        if (clip != 0)
          throw SafeNativeMethods.Gdip.StatusException(clip);
        return wrapper;
      }
      set => this.SetClip(value, CombineMode.Replace);
    }

    /// <summary>Gets a <see cref="T:System.Drawing.RectangleF" /> structure that bounds the clipping region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents a bounding rectangle for the clipping region of this <see cref="T:System.Drawing.Graphics" />.</returns>
    public RectangleF ClipBounds
    {
      get
      {
        GPRECTF rect = new GPRECTF();
        int clipBounds = SafeNativeMethods.Gdip.GdipGetClipBounds(new HandleRef((object) this, this.NativeGraphics), ref rect);
        if (clipBounds != 0)
          throw SafeNativeMethods.Gdip.StatusException(clipBounds);
        return rect.ToRectangleF();
      }
    }

    /// <summary>Gets a value indicating whether the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty.</summary>
    /// <returns>
    /// <see langword="true" /> if the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty; otherwise, <see langword="false" />.</returns>
    public bool IsClipEmpty
    {
      get
      {
        int boolean;
        int status = SafeNativeMethods.Gdip.GdipIsClipEmpty(new HandleRef((object) this, this.NativeGraphics), out boolean);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        return boolean != 0;
      }
    }

    /// <summary>Gets the bounding rectangle of the visible clipping region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents a bounding rectangle for the visible clipping region of this <see cref="T:System.Drawing.Graphics" />.</returns>
    public RectangleF VisibleClipBounds
    {
      get
      {
        if (this.PrintingHelper != null && this.PrintingHelper is PrintPreviewGraphics printingHelper)
          return printingHelper.VisibleClipBounds;
        GPRECTF rect = new GPRECTF();
        int visibleClipBounds = SafeNativeMethods.Gdip.GdipGetVisibleClipBounds(new HandleRef((object) this, this.NativeGraphics), ref rect);
        if (visibleClipBounds != 0)
          throw SafeNativeMethods.Gdip.StatusException(visibleClipBounds);
        return rect.ToRectangleF();
      }
    }

    /// <summary>Gets a value indicating whether the visible clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty.</summary>
    /// <returns>
    /// <see langword="true" /> if the visible portion of the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty; otherwise, <see langword="false" />.</returns>
    public bool IsVisibleClipEmpty
    {
      get
      {
        int boolean;
        int status = SafeNativeMethods.Gdip.GdipIsVisibleClipEmpty(new HandleRef((object) this, this.NativeGraphics), out boolean);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        return boolean != 0;
      }
    }

    /// <summary>Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the point to test for visibility.</param>
    /// <param name="y">The y-coordinate of the point to test for visibility.</param>
    /// <returns>
    /// <see langword="true" /> if the point defined by the <paramref name="x" /> and <paramref name="y" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(int x, int y) => this.IsVisible(new Point(x, y));

    /// <summary>Indicates whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="point">
    /// <see cref="T:System.Drawing.Point" /> structure to test for visibility.</param>
    /// <returns>
    /// <see langword="true" /> if the point specified by the <paramref name="point" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(Point point)
    {
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsVisiblePointI(new HandleRef((object) this, this.NativeGraphics), point.X, point.Y, out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the point to test for visibility.</param>
    /// <param name="y">The y-coordinate of the point to test for visibility.</param>
    /// <returns>
    /// <see langword="true" /> if the point defined by the <paramref name="x" /> and <paramref name="y" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(float x, float y) => this.IsVisible(new PointF(x, y));

    /// <summary>Indicates whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="point">
    /// <see cref="T:System.Drawing.PointF" /> structure to test for visibility.</param>
    /// <returns>
    /// <see langword="true" /> if the point specified by the <paramref name="point" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(PointF point)
    {
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsVisiblePoint(new HandleRef((object) this, this.NativeGraphics), point.X, point.Y, out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
    /// <param name="width">Width of the rectangle to test for visibility.</param>
    /// <param name="height">Height of the rectangle to test for visibility.</param>
    /// <returns>
    /// <see langword="true" /> if the rectangle defined by the <paramref name="x" />, <paramref name="y" />, <paramref name="width" />, and <paramref name="height" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(int x, int y, int width, int height) => this.IsVisible(new Rectangle(x, y, width, height));

    /// <summary>Indicates whether the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure to test for visibility.</param>
    /// <returns>
    /// <see langword="true" /> if the rectangle specified by the <paramref name="rect" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(Rectangle rect)
    {
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsVisibleRectI(new HandleRef((object) this, this.NativeGraphics), rect.X, rect.Y, rect.Width, rect.Height, out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    /// <summary>Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
    /// <param name="width">Width of the rectangle to test for visibility.</param>
    /// <param name="height">Height of the rectangle to test for visibility.</param>
    /// <returns>
    /// <see langword="true" /> if the rectangle defined by the <paramref name="x" />, <paramref name="y" />, <paramref name="width" />, and <paramref name="height" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(float x, float y, float width, float height) => this.IsVisible(new RectangleF(x, y, width, height));

    /// <summary>Indicates whether the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
    /// <param name="rect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure to test for visibility.</param>
    /// <returns>
    /// <see langword="true" /> if the rectangle specified by the <paramref name="rect" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
    public bool IsVisible(RectangleF rect)
    {
      int boolean;
      int status = SafeNativeMethods.Gdip.GdipIsVisibleRect(new HandleRef((object) this, this.NativeGraphics), rect.X, rect.Y, rect.Width, rect.Height, out boolean);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return boolean != 0;
    }

    private void PushContext(GraphicsContext context)
    {
      if (this.previousContext != null)
      {
        context.Previous = this.previousContext;
        this.previousContext.Next = context;
      }
      this.previousContext = context;
    }

    private void PopContext(int currentContextState)
    {
      for (GraphicsContext graphicsContext = this.previousContext; graphicsContext != null; graphicsContext = graphicsContext.Previous)
      {
        if (graphicsContext.State == currentContextState)
        {
          this.previousContext = graphicsContext.Previous;
          graphicsContext.Dispose();
          break;
        }
      }
    }

    /// <summary>Saves the current state of this <see cref="T:System.Drawing.Graphics" /> and identifies the saved state with a <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.</summary>
    /// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that represents the saved state of this <see cref="T:System.Drawing.Graphics" />.</returns>
    public System.Drawing.Drawing2D.GraphicsState Save()
    {
      GraphicsContext context = new GraphicsContext(this);
      int state = 0;
      int status = SafeNativeMethods.Gdip.GdipSaveGraphics(new HandleRef((object) this, this.NativeGraphics), out state);
      if (status != 0)
      {
        context.Dispose();
        throw SafeNativeMethods.Gdip.StatusException(status);
      }
      context.State = state;
      context.IsCumulative = true;
      this.PushContext(context);
      return new System.Drawing.Drawing2D.GraphicsState(state);
    }

    /// <summary>Restores the state of this <see cref="T:System.Drawing.Graphics" /> to the state represented by a <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.</summary>
    /// <param name="gstate">
    /// <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that represents the state to which to restore this <see cref="T:System.Drawing.Graphics" />.</param>
    public void Restore(System.Drawing.Drawing2D.GraphicsState gstate)
    {
      int status = SafeNativeMethods.Gdip.GdipRestoreGraphics(new HandleRef((object) this, this.NativeGraphics), gstate.nativeState);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.PopContext(gstate.nativeState);
    }

    /// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container with the specified scale transformation.</summary>
    /// <param name="dstrect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that, together with the <paramref name="srcrect" /> parameter, specifies a scale transformation for the new graphics container.</param>
    /// <param name="srcrect">
    /// <see cref="T:System.Drawing.RectangleF" /> structure that, together with the <paramref name="dstrect" /> parameter, specifies a scale transformation for the new graphics container.</param>
    /// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure for the container.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
    public GraphicsContainer BeginContainer(
      RectangleF dstrect,
      RectangleF srcrect,
      GraphicsUnit unit)
    {
      GraphicsContext context = new GraphicsContext(this);
      int state = 0;
      GPRECTF gprectf1 = dstrect.ToGPRECTF();
      GPRECTF gprectf2 = srcrect.ToGPRECTF();
      int status = SafeNativeMethods.Gdip.GdipBeginContainer(new HandleRef((object) this, this.NativeGraphics), ref gprectf1, ref gprectf2, (int) unit, out state);
      if (status != 0)
      {
        context.Dispose();
        throw SafeNativeMethods.Gdip.StatusException(status);
      }
      context.State = state;
      this.PushContext(context);
      return new GraphicsContainer(state);
    }

    /// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container.</summary>
    /// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
    public GraphicsContainer BeginContainer()
    {
      GraphicsContext context = new GraphicsContext(this);
      int state = 0;
      int status = SafeNativeMethods.Gdip.GdipBeginContainer2(new HandleRef((object) this, this.NativeGraphics), out state);
      if (status != 0)
      {
        context.Dispose();
        throw SafeNativeMethods.Gdip.StatusException(status);
      }
      context.State = state;
      this.PushContext(context);
      return new GraphicsContainer(state);
    }

    /// <summary>Closes the current graphics container and restores the state of this <see cref="T:System.Drawing.Graphics" /> to the state saved by a call to the <see cref="M:System.Drawing.Graphics.BeginContainer" /> method.</summary>
    /// <param name="container">
    /// <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the container this method restores.</param>
    public void EndContainer(GraphicsContainer container)
    {
      if (container == null)
        throw new ArgumentNullException(nameof (container));
      int status = SafeNativeMethods.Gdip.GdipEndContainer(new HandleRef((object) this, this.NativeGraphics), container.nativeGraphicsContainer);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      this.PopContext(container.nativeGraphicsContainer);
    }

    /// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container with the specified scale transformation.</summary>
    /// <param name="dstrect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that, together with the <paramref name="srcrect" /> parameter, specifies a scale transformation for the container.</param>
    /// <param name="srcrect">
    /// <see cref="T:System.Drawing.Rectangle" /> structure that, together with the <paramref name="dstrect" /> parameter, specifies a scale transformation for the container.</param>
    /// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure for the container.</param>
    /// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
    public GraphicsContainer BeginContainer(
      Rectangle dstrect,
      Rectangle srcrect,
      GraphicsUnit unit)
    {
      GraphicsContext context = new GraphicsContext(this);
      int state = 0;
      GPRECT dstRect = new GPRECT(dstrect);
      GPRECT srcRect = new GPRECT(srcrect);
      int status = SafeNativeMethods.Gdip.GdipBeginContainerI(new HandleRef((object) this, this.NativeGraphics), ref dstRect, ref srcRect, (int) unit, out state);
      if (status != 0)
      {
        context.Dispose();
        throw SafeNativeMethods.Gdip.StatusException(status);
      }
      context.State = state;
      this.PushContext(context);
      return new GraphicsContainer(state);
    }

    /// <summary>Adds a comment to the current <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
    /// <param name="data">Array of bytes that contains the comment.</param>
    public void AddMetafileComment(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      int status = SafeNativeMethods.Gdip.GdipComment(new HandleRef((object) this, this.NativeGraphics), data.Length, data);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Gets a handle to the current Windows halftone palette.</summary>
    /// <returns>Internal pointer that specifies the handle to the palette.</returns>
    public static IntPtr GetHalftonePalette()
    {
      if (Graphics.halftonePalette == IntPtr.Zero)
      {
        lock (Graphics.syncObject)
        {
          if (Graphics.halftonePalette == IntPtr.Zero)
          {
            if (Environment.OSVersion.Platform != PlatformID.Win32Windows)
              AppDomain.CurrentDomain.DomainUnload += new EventHandler(Graphics.OnDomainUnload);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(Graphics.OnDomainUnload);
            Graphics.halftonePalette = SafeNativeMethods.Gdip.GdipCreateHalftonePalette();
          }
        }
      }
      return Graphics.halftonePalette;
    }

    [PrePrepareMethod]
    private static void OnDomainUnload(object sender, EventArgs e)
    {
      if (!(Graphics.halftonePalette != IntPtr.Zero))
        return;
      SafeNativeMethods.IntDeleteObject(new HandleRef((object) null, Graphics.halftonePalette));
      Graphics.halftonePalette = IntPtr.Zero;
    }

    private void CheckErrorStatus(int status)
    {
      if (status != 0)
      {
        if (status == 1 || status == 7)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          switch (lastWin32Error)
          {
            case 5:
              return;
            case (int) sbyte.MaxValue:
              return;
            default:
              if ((UnsafeNativeMethods.GetSystemMetrics(4096) & 1) != 0 && lastWin32Error == 0)
                return;
              break;
          }
        }
        throw SafeNativeMethods.Gdip.StatusException(status);
      }
    }

    private void IgnoreMetafileErrors(Image image, ref int errorStatus)
    {
      if (errorStatus == 0 || !image.RawFormat.Equals((object) ImageFormat.Emf))
        return;
      errorStatus = 0;
    }

    /// <summary>Provides a callback method for deciding when the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should prematurely cancel execution and stop drawing an image.</summary>
    /// <param name="callbackdata">Internal pointer that specifies data for the callback method. This parameter is not passed by all <see cref="Overload:System.Drawing.Graphics.DrawImage" /> overloads. You can test for its absence by checking for the value <see cref="F:System.IntPtr.Zero" />.</param>
    /// <returns>This method returns <see langword="true" /> if it decides that the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should prematurely stop execution. Otherwise it returns <see langword="false" /> to indicate that the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should continue execution.</returns>
    public delegate bool DrawImageAbort(IntPtr callbackdata);

    /// <summary>Provides a callback method for the <see cref="Overload:System.Drawing.Graphics.EnumerateMetafile" /> method.</summary>
    /// <param name="recordType">Member of the <see cref="T:System.Drawing.Imaging.EmfPlusRecordType" /> enumeration that specifies the type of metafile record.</param>
    /// <param name="flags">Set of flags that specify attributes of the record.</param>
    /// <param name="dataSize">Number of bytes in the record data.</param>
    /// <param name="data">Pointer to a buffer that contains the record data.</param>
    /// <param name="callbackData">Not used.</param>
    /// <returns>Return <see langword="true" /> if you want to continue enumerating records; otherwise, <see langword="false" />.</returns>
    public delegate bool EnumerateMetafileProc(
      EmfPlusRecordType recordType,
      int flags,
      int dataSize,
      IntPtr data,
      PlayRecordCallback callbackData);
  }
}
