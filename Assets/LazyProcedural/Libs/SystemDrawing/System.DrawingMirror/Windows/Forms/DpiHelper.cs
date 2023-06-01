// Decompiled with JetBrains decompiler
// Type: System.Windows.Forms.DpiHelper
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
  internal static class DpiHelper
  {
    internal const double LogicalDpi = 96.0;
    private static bool isInitialized = false;
    private static double deviceDpi = 96.0;
    private static double logicalToDeviceUnitsScalingFactor = 0.0;
    private static bool enableHighDpi = false;
    private static string dpiAwarenessValue = (string) null;
    private static InterpolationMode interpolationMode = InterpolationMode.Invalid;

    private static void Initialize()
    {
      if (DpiHelper.isInitialized)
        return;
      if (DpiHelper.IsDpiAwarenessValueSet())
      {
        DpiHelper.enableHighDpi = true;
      }
      else
      {
        try
        {
          string a = ConfigurationManager.AppSettings.Get("EnableWindowsFormsHighDpiAutoResizing");
          if (!string.IsNullOrEmpty(a))
          {
            if (string.Equals(a, "true", StringComparison.InvariantCultureIgnoreCase))
              DpiHelper.enableHighDpi = true;
          }
        }
        catch
        {
        }
      }
      if (DpiHelper.enableHighDpi)
      {
        IntPtr dc = UnsafeNativeMethods.GetDC(System.Drawing.NativeMethods.NullHandleRef);
        if (dc != IntPtr.Zero)
        {
          DpiHelper.deviceDpi = (double) UnsafeNativeMethods.GetDeviceCaps(new HandleRef((object) null, dc), 88);
          UnsafeNativeMethods.ReleaseDC(System.Drawing.NativeMethods.NullHandleRef, new HandleRef((object) null, dc));
        }
      }
      DpiHelper.isInitialized = true;
    }

    internal static bool IsDpiAwarenessValueSet()
    {
      bool flag = false;
      try
      {
        if (string.IsNullOrEmpty(DpiHelper.dpiAwarenessValue))
          DpiHelper.dpiAwarenessValue = ConfigurationOptions.GetConfigSettingValue("DpiAwareness");
      }
      catch
      {
      }
      if (!string.IsNullOrEmpty(DpiHelper.dpiAwarenessValue))
      {
        switch (DpiHelper.dpiAwarenessValue.ToLowerInvariant())
        {
          case "true":
          case "system":
          case "true/pm":
          case "permonitor":
          case "permonitorv2":
            flag = true;
            break;
        }
      }
      return flag;
    }

    internal static int DeviceDpi
    {
      get
      {
        DpiHelper.Initialize();
        return (int) DpiHelper.deviceDpi;
      }
    }

    private static double LogicalToDeviceUnitsScalingFactor
    {
      get
      {
        if (DpiHelper.logicalToDeviceUnitsScalingFactor == 0.0)
        {
          DpiHelper.Initialize();
          DpiHelper.logicalToDeviceUnitsScalingFactor = DpiHelper.deviceDpi / 96.0;
        }
        return DpiHelper.logicalToDeviceUnitsScalingFactor;
      }
    }

    private static InterpolationMode InterpolationMode
    {
      get
      {
        if (DpiHelper.interpolationMode == InterpolationMode.Invalid)
        {
          int num = (int) Math.Round(DpiHelper.LogicalToDeviceUnitsScalingFactor * 100.0);
          DpiHelper.interpolationMode = num % 100 != 0 ? (num >= 100 ? InterpolationMode.HighQualityBicubic : InterpolationMode.HighQualityBilinear) : InterpolationMode.NearestNeighbor;
        }
        return DpiHelper.interpolationMode;
      }
    }

    private static Bitmap ScaleBitmapToSize(Bitmap logicalImage, Size deviceImageSize)
    {
      Bitmap size = new Bitmap(deviceImageSize.Width, deviceImageSize.Height, logicalImage.PixelFormat);
      using (Graphics graphics = Graphics.FromImage((Image) size))
      {
        graphics.InterpolationMode = DpiHelper.InterpolationMode;
        RectangleF srcRect = new RectangleF(0.0f, 0.0f, (float) logicalImage.Size.Width, (float) logicalImage.Size.Height);
        RectangleF destRect = new RectangleF(0.0f, 0.0f, (float) deviceImageSize.Width, (float) deviceImageSize.Height);
        srcRect.Offset(-0.5f, -0.5f);
        graphics.DrawImage((Image) logicalImage, destRect, srcRect, GraphicsUnit.Pixel);
      }
      return size;
    }

    private static Bitmap CreateScaledBitmap(Bitmap logicalImage, int deviceDpi = 0)
    {
      Size deviceUnits = DpiHelper.LogicalToDeviceUnits(logicalImage.Size, deviceDpi);
      return DpiHelper.ScaleBitmapToSize(logicalImage, deviceUnits);
    }

    public static bool IsScalingRequired
    {
      get
      {
        DpiHelper.Initialize();
        return DpiHelper.deviceDpi != 96.0;
      }
    }

    public static int LogicalToDeviceUnits(int value, int devicePixels = 0) => devicePixels == 0 ? (int) Math.Round(DpiHelper.LogicalToDeviceUnitsScalingFactor * (double) value) : (int) Math.Round((double) devicePixels / 96.0 * (double) value);

    public static double LogicalToDeviceUnits(double value, int devicePixels = 0) => devicePixels == 0 ? DpiHelper.LogicalToDeviceUnitsScalingFactor * value : (double) devicePixels / 96.0 * value;

    public static int LogicalToDeviceUnitsX(int value) => DpiHelper.LogicalToDeviceUnits(value, 0);

    public static int LogicalToDeviceUnitsY(int value) => DpiHelper.LogicalToDeviceUnits(value, 0);

    public static Size LogicalToDeviceUnits(Size logicalSize, int deviceDpi = 0) => new Size(DpiHelper.LogicalToDeviceUnits(logicalSize.Width, deviceDpi), DpiHelper.LogicalToDeviceUnits(logicalSize.Height, deviceDpi));

    public static Bitmap CreateResizedBitmap(Bitmap logicalImage, Size targetImageSize) => logicalImage == null ? (Bitmap) null : DpiHelper.ScaleBitmapToSize(logicalImage, targetImageSize);

    public static void ScaleBitmapLogicalToDevice(ref Bitmap logicalBitmap, int deviceDpi = 0)
    {
      if (logicalBitmap == null)
        return;
      Bitmap scaledBitmap = DpiHelper.CreateScaledBitmap(logicalBitmap, deviceDpi);
      if (scaledBitmap == null)
        return;
      logicalBitmap.Dispose();
      logicalBitmap = scaledBitmap;
    }

    public static int ConvertToGivenDpiPixel(int value, double pixelFactor)
    {
      int num = (int) Math.Round((double) value * pixelFactor);
      return num != 0 ? num : 1;
    }
  }
}
