// Decompiled with JetBrains decompiler
// Type: System.Drawing.ImageAnimator
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading;

namespace System.Drawing
{
  /// <summary>Animates an image that has time-based frames.</summary>
  public sealed class ImageAnimator
  {
    private static List<ImageAnimator.ImageInfo> imageInfoList;
    private static bool anyFrameDirty;
    private static Thread animationThread;
    private static ReaderWriterLock rwImgListLock = new ReaderWriterLock();
    [ThreadStatic]
    private static int threadWriterLockWaitCount;

    private ImageAnimator()
    {
    }

    /// <summary>Advances the frame in the specified image. The new frame is drawn the next time the image is rendered. This method applies only to images with time-based frames.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object for which to update frames.</param>
    public static void UpdateFrames(Image image)
    {
      if (!ImageAnimator.anyFrameDirty || image == null || ImageAnimator.imageInfoList == null || ImageAnimator.threadWriterLockWaitCount > 0)
        return;
      ImageAnimator.rwImgListLock.AcquireReaderLock(-1);
      try
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (ImageAnimator.ImageInfo imageInfo in ImageAnimator.imageInfoList)
        {
          if (imageInfo.Image == image)
          {
            if (imageInfo.FrameDirty)
            {
              lock (imageInfo.Image)
                imageInfo.UpdateFrame();
            }
            flag2 = true;
          }
          if (imageInfo.FrameDirty)
            flag1 = true;
          if (flag1 & flag2)
            break;
        }
        ImageAnimator.anyFrameDirty = flag1;
      }
      finally
      {
        ImageAnimator.rwImgListLock.ReleaseReaderLock();
      }
    }

    /// <summary>Advances the frame in all images currently being animated. The new frame is drawn the next time the image is rendered.</summary>
    public static void UpdateFrames()
    {
      if (!ImageAnimator.anyFrameDirty || ImageAnimator.imageInfoList == null || ImageAnimator.threadWriterLockWaitCount > 0)
        return;
      ImageAnimator.rwImgListLock.AcquireReaderLock(-1);
      try
      {
        foreach (ImageAnimator.ImageInfo imageInfo in ImageAnimator.imageInfoList)
        {
          lock (imageInfo.Image)
            imageInfo.UpdateFrame();
        }
        ImageAnimator.anyFrameDirty = false;
      }
      finally
      {
        ImageAnimator.rwImgListLock.ReleaseReaderLock();
      }
    }

    /// <summary>Displays a multiple-frame image as an animation.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object to animate.</param>
    /// <param name="onFrameChangedHandler">An <see langword="EventHandler" /> object that specifies the method that is called when the animation frame changes.</param>
    public static void Animate(Image image, EventHandler onFrameChangedHandler)
    {
      if (image == null)
        return;
      ImageAnimator.ImageInfo imageInfo = (ImageAnimator.ImageInfo) null;
      lock (image)
        imageInfo = new ImageAnimator.ImageInfo(image);
      ImageAnimator.StopAnimate(image, onFrameChangedHandler);
      bool isReaderLockHeld = ImageAnimator.rwImgListLock.IsReaderLockHeld;
      LockCookie lockCookie = new LockCookie();
      ++ImageAnimator.threadWriterLockWaitCount;
      try
      {
        if (isReaderLockHeld)
          lockCookie = ImageAnimator.rwImgListLock.UpgradeToWriterLock(-1);
        else
          ImageAnimator.rwImgListLock.AcquireWriterLock(-1);
      }
      finally
      {
        --ImageAnimator.threadWriterLockWaitCount;
      }
      try
      {
        if (!imageInfo.Animated)
          return;
        if (ImageAnimator.imageInfoList == null)
          ImageAnimator.imageInfoList = new List<ImageAnimator.ImageInfo>();
        imageInfo.FrameChangedHandler = onFrameChangedHandler;
        ImageAnimator.imageInfoList.Add(imageInfo);
        if (ImageAnimator.animationThread != null)
          return;
        ImageAnimator.animationThread = new Thread(new ThreadStart(ImageAnimator.AnimateImages50ms));
        ImageAnimator.animationThread.Name = typeof (ImageAnimator).Name;
        ImageAnimator.animationThread.IsBackground = true;
        ImageAnimator.animationThread.Start();
      }
      finally
      {
        if (isReaderLockHeld)
          ImageAnimator.rwImgListLock.DowngradeFromWriterLock(ref lockCookie);
        else
          ImageAnimator.rwImgListLock.ReleaseWriterLock();
      }
    }

    /// <summary>Returns a Boolean value indicating whether the specified image contains time-based frames.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object to test.</param>
    /// <returns>This method returns <see langword="true" /> if the specified image contains time-based frames; otherwise, <see langword="false" />.</returns>
    public static bool CanAnimate(Image image)
    {
      if (image == null)
        return false;
      lock (image)
      {
        foreach (Guid frameDimensions in image.FrameDimensionsList)
        {
          if (new FrameDimension(frameDimensions).Equals((object) FrameDimension.Time))
            return image.GetFrameCount(FrameDimension.Time) > 1;
        }
      }
      return false;
    }

    /// <summary>Terminates a running animation.</summary>
    /// <param name="image">The <see cref="T:System.Drawing.Image" /> object to stop animating.</param>
    /// <param name="onFrameChangedHandler">An <see langword="EventHandler" /> object that specifies the method that is called when the animation frame changes.</param>
    public static void StopAnimate(Image image, EventHandler onFrameChangedHandler)
    {
      if (image == null || ImageAnimator.imageInfoList == null)
        return;
      bool isReaderLockHeld = ImageAnimator.rwImgListLock.IsReaderLockHeld;
      LockCookie lockCookie = new LockCookie();
      ++ImageAnimator.threadWriterLockWaitCount;
      try
      {
        if (isReaderLockHeld)
          lockCookie = ImageAnimator.rwImgListLock.UpgradeToWriterLock(-1);
        else
          ImageAnimator.rwImgListLock.AcquireWriterLock(-1);
      }
      finally
      {
        --ImageAnimator.threadWriterLockWaitCount;
      }
      try
      {
        for (int index = 0; index < ImageAnimator.imageInfoList.Count; ++index)
        {
          ImageAnimator.ImageInfo imageInfo = ImageAnimator.imageInfoList[index];
          if (image == imageInfo.Image)
          {
            if (!(onFrameChangedHandler == imageInfo.FrameChangedHandler) && (onFrameChangedHandler == null || !onFrameChangedHandler.Equals((object) imageInfo.FrameChangedHandler)))
              break;
            ImageAnimator.imageInfoList.Remove(imageInfo);
            break;
          }
        }
      }
      finally
      {
        if (isReaderLockHeld)
          ImageAnimator.rwImgListLock.DowngradeFromWriterLock(ref lockCookie);
        else
          ImageAnimator.rwImgListLock.ReleaseWriterLock();
      }
    }

    private static void AnimateImages50ms()
    {
      while (true)
      {
        ImageAnimator.rwImgListLock.AcquireReaderLock(-1);
        try
        {
          for (int index = 0; index < ImageAnimator.imageInfoList.Count; ++index)
          {
            ImageAnimator.ImageInfo imageInfo = ImageAnimator.imageInfoList[index];
            imageInfo.FrameTimer += 5;
            if (imageInfo.FrameTimer >= imageInfo.FrameDelay(imageInfo.Frame))
            {
              imageInfo.FrameTimer = 0;
              if (imageInfo.Frame + 1 < imageInfo.FrameCount)
                ++imageInfo.Frame;
              else
                imageInfo.Frame = 0;
              if (imageInfo.FrameDirty)
                ImageAnimator.anyFrameDirty = true;
            }
          }
        }
        finally
        {
          ImageAnimator.rwImgListLock.ReleaseReaderLock();
        }
        Thread.Sleep(50);
      }
    }

    private class ImageInfo
    {
      private const int PropertyTagFrameDelay = 20736;
      private Image image;
      private int frame;
      private int frameCount;
      private bool frameDirty;
      private bool animated;
      private EventHandler onFrameChangedHandler;
      private int[] frameDelay;
      private int frameTimer;

      public ImageInfo(Image image)
      {
        this.image = image;
        this.animated = ImageAnimator.CanAnimate(image);
        if (this.animated)
        {
          this.frameCount = image.GetFrameCount(FrameDimension.Time);
          PropertyItem propertyItem = image.GetPropertyItem(20736);
          if (propertyItem != null)
          {
            byte[] numArray = propertyItem.Value;
            this.frameDelay = new int[this.FrameCount];
            for (int index = 0; index < this.FrameCount; ++index)
              this.frameDelay[index] = (int) numArray[index * 4] + 256 * (int) numArray[index * 4 + 1] + 65536 * (int) numArray[index * 4 + 2] + 16777216 * (int) numArray[index * 4 + 3];
          }
        }
        else
          this.frameCount = 1;
        if (this.frameDelay != null)
          return;
        this.frameDelay = new int[this.FrameCount];
      }

      public bool Animated => this.animated;

      public int Frame
      {
        get => this.frame;
        set
        {
          if (this.frame == value)
            return;
          if (value < 0 || value >= this.FrameCount)
            throw new ArgumentException(SR.GetString("InvalidFrame"), nameof (value));
          if (!this.Animated)
            return;
          this.frame = value;
          this.frameDirty = true;
          this.OnFrameChanged(EventArgs.Empty);
        }
      }

      public bool FrameDirty => this.frameDirty;

      public EventHandler FrameChangedHandler
      {
        get => this.onFrameChangedHandler;
        set => this.onFrameChangedHandler = value;
      }

      public int FrameCount => this.frameCount;

      public int FrameDelay(int frame) => this.frameDelay[frame];

      internal int FrameTimer
      {
        get => this.frameTimer;
        set => this.frameTimer = value;
      }

      internal Image Image => this.image;

      internal void UpdateFrame()
      {
        if (!this.frameDirty)
          return;
        this.image.SelectActiveFrame(FrameDimension.Time, this.Frame);
        this.frameDirty = false;
      }

      protected void OnFrameChanged(EventArgs e)
      {
        if (this.onFrameChangedHandler == null)
          return;
        this.onFrameChangedHandler((object) this.image, e);
      }
    }
  }
}
