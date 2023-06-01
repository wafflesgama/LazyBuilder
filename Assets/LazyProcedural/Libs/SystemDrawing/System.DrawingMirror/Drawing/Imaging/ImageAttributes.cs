// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ImageAttributes
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  /// <summary>Contains information about how bitmap and metafile colors are manipulated during rendering.</summary>
  [StructLayout(LayoutKind.Sequential)]
  public sealed class ImageAttributes : ICloneable, IDisposable
  {
    internal IntPtr nativeImageAttributes;

    internal void SetNativeImageAttributes(IntPtr handle) => this.nativeImageAttributes = !(handle == IntPtr.Zero) ? handle : throw new ArgumentNullException(nameof (handle));

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class.</summary>
    public ImageAttributes()
    {
      IntPtr imageattr = IntPtr.Zero;
      int imageAttributes = SafeNativeMethods.Gdip.GdipCreateImageAttributes(out imageattr);
      if (imageAttributes != 0)
        throw SafeNativeMethods.Gdip.StatusException(imageAttributes);
      this.SetNativeImageAttributes(imageattr);
    }

    internal ImageAttributes(IntPtr newNativeImageAttributes) => this.SetNativeImageAttributes(newNativeImageAttributes);

    /// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(this.nativeImageAttributes != IntPtr.Zero))
        return;
      try
      {
        SafeNativeMethods.Gdip.GdipDisposeImageAttributes(new HandleRef((object) this, this.nativeImageAttributes));
      }
      catch (Exception ex)
      {
        if (!System.Drawing.ClientUtils.IsSecurityOrCriticalException(ex))
          return;
        throw;
      }
      finally
      {
        this.nativeImageAttributes = IntPtr.Zero;
      }
    }

    /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~ImageAttributes() => this.Dispose(false);

    /// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
    /// <returns>The <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object this class creates, cast as an object.</returns>
    public object Clone()
    {
      IntPtr cloneImageattr = IntPtr.Zero;
      int status = SafeNativeMethods.Gdip.GdipCloneImageAttributes(new HandleRef((object) this, this.nativeImageAttributes), out cloneImageattr);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
      return (object) new ImageAttributes(cloneImageattr);
    }

    /// <summary>Sets the color-adjustment matrix for the default category.</summary>
    /// <param name="newColorMatrix">The color-adjustment matrix.</param>
    public void SetColorMatrix(ColorMatrix newColorMatrix) => this.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);

    /// <summary>Sets the color-adjustment matrix for the default category.</summary>
    /// <param name="newColorMatrix">The color-adjustment matrix.</param>
    /// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix.</param>
    public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag flags) => this.SetColorMatrix(newColorMatrix, flags, ColorAdjustType.Default);

    /// <summary>Sets the color-adjustment matrix for a specified category.</summary>
    /// <param name="newColorMatrix">The color-adjustment matrix.</param>
    /// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix.</param>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is set.</param>
    public void SetColorMatrix(
      ColorMatrix newColorMatrix,
      ColorMatrixFlag mode,
      ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesColorMatrix(new HandleRef((object) this, this.nativeImageAttributes), type, true, newColorMatrix, (ColorMatrix) null, mode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Clears the color-adjustment matrix for the default category.</summary>
    public void ClearColorMatrix() => this.ClearColorMatrix(ColorAdjustType.Default);

    /// <summary>Clears the color-adjustment matrix for a specified category.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is cleared.</param>
    public void ClearColorMatrix(ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesColorMatrix(new HandleRef((object) this, this.nativeImageAttributes), type, false, (ColorMatrix) null, (ColorMatrix) null, ColorMatrixFlag.Default);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
    /// <param name="newColorMatrix">The color-adjustment matrix.</param>
    /// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
    public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix) => this.SetColorMatrices(newColorMatrix, grayMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);

    /// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
    /// <param name="newColorMatrix">The color-adjustment matrix.</param>
    /// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
    /// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment and grayscale-adjustment matrices.</param>
    public void SetColorMatrices(
      ColorMatrix newColorMatrix,
      ColorMatrix grayMatrix,
      ColorMatrixFlag flags)
    {
      this.SetColorMatrices(newColorMatrix, grayMatrix, flags, ColorAdjustType.Default);
    }

    /// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for a specified category.</summary>
    /// <param name="newColorMatrix">The color-adjustment matrix.</param>
    /// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
    /// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment and grayscale-adjustment matrices.</param>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment and grayscale-adjustment matrices are set.</param>
    public void SetColorMatrices(
      ColorMatrix newColorMatrix,
      ColorMatrix grayMatrix,
      ColorMatrixFlag mode,
      ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesColorMatrix(new HandleRef((object) this, this.nativeImageAttributes), type, true, newColorMatrix, grayMatrix, mode);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the threshold (transparency range) for the default category.</summary>
    /// <param name="threshold">A real number that specifies the threshold value.</param>
    public void SetThreshold(float threshold) => this.SetThreshold(threshold, ColorAdjustType.Default);

    /// <summary>Sets the threshold (transparency range) for a specified category.</summary>
    /// <param name="threshold">A threshold value from 0.0 to 1.0 that is used as a breakpoint to sort colors that will be mapped to either a maximum or a minimum value.</param>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color threshold is set.</param>
    public void SetThreshold(float threshold, ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesThreshold(new HandleRef((object) this, this.nativeImageAttributes), type, true, threshold);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Clears the threshold value for the default category.</summary>
    public void ClearThreshold() => this.ClearThreshold(ColorAdjustType.Default);

    /// <summary>Clears the threshold value for a specified category.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the threshold is cleared.</param>
    public void ClearThreshold(ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesThreshold(new HandleRef((object) this, this.nativeImageAttributes), type, false, 0.0f);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the gamma value for the default category.</summary>
    /// <param name="gamma">The gamma correction value.</param>
    public void SetGamma(float gamma) => this.SetGamma(gamma, ColorAdjustType.Default);

    /// <summary>Sets the gamma value for a specified category.</summary>
    /// <param name="gamma">The gamma correction value.</param>
    /// <param name="type">An element of the <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> enumeration that specifies the category for which the gamma value is set.</param>
    public void SetGamma(float gamma, ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesGamma(new HandleRef((object) this, this.nativeImageAttributes), type, true, gamma);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Disables gamma correction for the default category.</summary>
    public void ClearGamma() => this.ClearGamma(ColorAdjustType.Default);

    /// <summary>Disables gamma correction for a specified category.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which gamma correction is disabled.</param>
    public void ClearGamma(ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesGamma(new HandleRef((object) this, this.nativeImageAttributes), type, false, 0.0f);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Turns off color adjustment for the default category. You can call the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment settings that were in place before the call to the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.</summary>
    public void SetNoOp() => this.SetNoOp(ColorAdjustType.Default);

    /// <summary>Turns off color adjustment for a specified category. You can call the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment settings that were in place before the call to the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which color correction is turned off.</param>
    public void SetNoOp(ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesNoOp(new HandleRef((object) this, this.nativeImageAttributes), type, true);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Clears the <see langword="NoOp" /> setting for the default category.</summary>
    public void ClearNoOp() => this.ClearNoOp(ColorAdjustType.Default);

    /// <summary>Clears the <see langword="NoOp" /> setting for a specified category.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the <see langword="NoOp" /> setting is cleared.</param>
    public void ClearNoOp(ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesNoOp(new HandleRef((object) this, this.nativeImageAttributes), type, false);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the color key for the default category.</summary>
    /// <param name="colorLow">The low color-key value.</param>
    /// <param name="colorHigh">The high color-key value.</param>
    public void SetColorKey(Color colorLow, Color colorHigh) => this.SetColorKey(colorLow, colorHigh, ColorAdjustType.Default);

    /// <summary>Sets the color key (transparency range) for a specified category.</summary>
    /// <param name="colorLow">The low color-key value.</param>
    /// <param name="colorHigh">The high color-key value.</param>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is set.</param>
    public void SetColorKey(Color colorLow, Color colorHigh, ColorAdjustType type)
    {
      int argb1 = colorLow.ToArgb();
      int argb2 = colorHigh.ToArgb();
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesColorKeys(new HandleRef((object) this, this.nativeImageAttributes), type, true, argb1, argb2);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Clears the color key (transparency range) for the default category.</summary>
    public void ClearColorKey() => this.ClearColorKey(ColorAdjustType.Default);

    /// <summary>Clears the color key (transparency range) for a specified category.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is cleared.</param>
    public void ClearColorKey(ColorAdjustType type)
    {
      int num = 0;
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesColorKeys(new HandleRef((object) this, this.nativeImageAttributes), type, false, num, num);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for the default category.</summary>
    /// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorChannelFlag" /> that specifies the output channel.</param>
    public void SetOutputChannel(ColorChannelFlag flags) => this.SetOutputChannel(flags, ColorAdjustType.Default);

    /// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for a specified category.</summary>
    /// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorChannelFlag" /> that specifies the output channel.</param>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel is set.</param>
    public void SetOutputChannel(ColorChannelFlag flags, ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesOutputChannel(new HandleRef((object) this, this.nativeImageAttributes), type, true, flags);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Clears the CMYK (cyan-magenta-yellow-black) output channel setting for the default category.</summary>
    public void ClearOutputChannel() => this.ClearOutputChannel(ColorAdjustType.Default);

    /// <summary>Clears the (cyan-magenta-yellow-black) output channel setting for a specified category.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel setting is cleared.</param>
    public void ClearOutputChannel(ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesOutputChannel(new HandleRef((object) this, this.nativeImageAttributes), type, false, ColorChannelFlag.ColorChannelLast);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the output channel color-profile file for the default category.</summary>
    /// <param name="colorProfileFilename">The path name of a color-profile file. If the color-profile file is in the %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter must be the fully qualified path name.</param>
    public void SetOutputChannelColorProfile(string colorProfileFilename) => this.SetOutputChannelColorProfile(colorProfileFilename, ColorAdjustType.Default);

    /// <summary>Sets the output channel color-profile file for a specified category.</summary>
    /// <param name="colorProfileFilename">The path name of a color-profile file. If the color-profile file is in the %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter must be the fully qualified path name.</param>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel color-profile file is set.</param>
    public void SetOutputChannelColorProfile(string colorProfileFilename, ColorAdjustType type)
    {
      IntSecurity.DemandReadFileIO(colorProfileFilename);
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesOutputChannelColorProfile(new HandleRef((object) this, this.nativeImageAttributes), type, true, colorProfileFilename);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Clears the output channel color profile setting for the default category.</summary>
    public void ClearOutputChannelColorProfile() => this.ClearOutputChannel(ColorAdjustType.Default);

    /// <summary>Clears the output channel color profile setting for a specified category.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel profile setting is cleared.</param>
    public void ClearOutputChannelColorProfile(ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesOutputChannel(new HandleRef((object) this, this.nativeImageAttributes), type, false, ColorChannelFlag.ColorChannelLast);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the color-remap table for the default category.</summary>
    /// <param name="map">An array of color pairs of type <see cref="T:System.Drawing.Imaging.ColorMap" />. Each color pair contains an existing color (the first value) and the color that it will be mapped to (the second value).</param>
    public void SetRemapTable(ColorMap[] map) => this.SetRemapTable(map, ColorAdjustType.Default);

    /// <summary>Sets the color-remap table for a specified category.</summary>
    /// <param name="map">An array of color pairs of type <see cref="T:System.Drawing.Imaging.ColorMap" />. Each color pair contains an existing color (the first value) and the color that it will be mapped to (the second value).</param>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-remap table is set.</param>
    public void SetRemapTable(ColorMap[] map, ColorAdjustType type)
    {
      int length = map.Length;
      int num1 = 4;
      IntPtr num2 = Marshal.AllocHGlobal(checked (length * num1 * 2));
      try
      {
        for (int index = 0; index < length; ++index)
        {
          Color color = map[index].OldColor;
          Marshal.StructureToPtr((object) color.ToArgb(), (IntPtr) ((long) num2 + (long) (index * num1 * 2)), false);
          color = map[index].NewColor;
          Marshal.StructureToPtr((object) color.ToArgb(), (IntPtr) ((long) num2 + (long) (index * num1 * 2) + (long) num1), false);
        }
        int status = SafeNativeMethods.Gdip.GdipSetImageAttributesRemapTable(new HandleRef((object) this, this.nativeImageAttributes), type, true, length, new HandleRef((object) null, num2));
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
      }
      finally
      {
        Marshal.FreeHGlobal(num2);
      }
    }

    /// <summary>Clears the color-remap table for the default category.</summary>
    public void ClearRemapTable() => this.ClearRemapTable(ColorAdjustType.Default);

    /// <summary>Clears the color-remap table for a specified category.</summary>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the remap table is cleared.</param>
    public void ClearRemapTable(ColorAdjustType type)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesRemapTable(new HandleRef((object) this, this.nativeImageAttributes), type, false, 0, System.Drawing.NativeMethods.NullHandleRef);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Sets the color-remap table for the brush category.</summary>
    /// <param name="map">An array of <see cref="T:System.Drawing.Imaging.ColorMap" /> objects.</param>
    public void SetBrushRemapTable(ColorMap[] map) => this.SetRemapTable(map, ColorAdjustType.Brush);

    /// <summary>Clears the brush color-remap table of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
    public void ClearBrushRemapTable() => this.ClearRemapTable(ColorAdjustType.Brush);

    /// <summary>Sets the wrap mode that is used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
    /// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
    public void SetWrapMode(WrapMode mode) => this.SetWrapMode(mode, new Color(), false);

    /// <summary>Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
    /// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
    /// <param name="color">An <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object that specifies the color of pixels outside of a rendered image. This color is visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself.</param>
    public void SetWrapMode(WrapMode mode, Color color) => this.SetWrapMode(mode, color, false);

    /// <summary>Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
    /// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
    /// <param name="color">A color object that specifies the color of pixels outside of a rendered image. This color is visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself.</param>
    /// <param name="clamp">This parameter has no effect. Set it to <see langword="false" />.</param>
    public void SetWrapMode(WrapMode mode, Color color, bool clamp)
    {
      int status = SafeNativeMethods.Gdip.GdipSetImageAttributesWrapMode(new HandleRef((object) this, this.nativeImageAttributes), (int) mode, color.ToArgb(), clamp);
      if (status != 0)
        throw SafeNativeMethods.Gdip.StatusException(status);
    }

    /// <summary>Adjusts the colors in a palette according to the adjustment settings of a specified category.</summary>
    /// <param name="palette">A <see cref="T:System.Drawing.Imaging.ColorPalette" /> that on input contains the palette to be adjusted, and on output contains the adjusted palette.</param>
    /// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category whose adjustment settings will be applied to the palette.</param>
    public void GetAdjustedPalette(ColorPalette palette, ColorAdjustType type)
    {
      IntPtr memory = palette.ConvertToMemory();
      try
      {
        int attributesAdjustedPalette = SafeNativeMethods.Gdip.GdipGetImageAttributesAdjustedPalette(new HandleRef((object) this, this.nativeImageAttributes), new HandleRef((object) null, memory), type);
        if (attributesAdjustedPalette != 0)
          throw SafeNativeMethods.Gdip.StatusException(attributesAdjustedPalette);
        palette.ConvertFromMemory(memory);
      }
      finally
      {
        if (memory != IntPtr.Zero)
          Marshal.FreeHGlobal(memory);
      }
    }
  }
}
