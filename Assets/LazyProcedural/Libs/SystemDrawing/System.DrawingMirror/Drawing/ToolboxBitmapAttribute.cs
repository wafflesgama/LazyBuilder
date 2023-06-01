// Decompiled with JetBrains decompiler
// Type: System.Drawing.ToolboxBitmapAttribute
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace System.Drawing
{
  /// <summary>Allows you to specify an icon to represent a control in a container, such as the Microsoft Visual Studio Form Designer.</summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class ToolboxBitmapAttribute : Attribute
  {
    private Image smallImage;
    private Image largeImage;
    private Bitmap originalBitmap;
    private string imageFile;
    private Type imageType;
    private string imageName;
    private static readonly Size largeSize = new Size(32, 32);
    private static readonly Size smallSize = new Size(16, 16);
    private static string lastOriginalFileName;
    private static string lastUpdatedFileName;
    /// <summary>A <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object that has its small image and its large image set to <see langword="null" />.</summary>
    public static readonly ToolboxBitmapAttribute Default = new ToolboxBitmapAttribute((Image) null, (Image) null);
    private static readonly ToolboxBitmapAttribute DefaultComponent;

    /// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object with an image from a specified file.</summary>
    /// <param name="imageFile">The name of a file that contains a 16 by 16 bitmap.</param>
    public ToolboxBitmapAttribute(string imageFile)
      : this(ToolboxBitmapAttribute.GetImageFromFile(imageFile, false), ToolboxBitmapAttribute.GetImageFromFile(imageFile, true))
    {
      this.imageFile = imageFile;
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object based on a 16 x 16 bitmap that is embedded as a resource in a specified assembly.</summary>
    /// <param name="t">A <see cref="T:System.Type" /> whose defining assembly is searched for the bitmap resource.</param>
    public ToolboxBitmapAttribute(Type t)
      : this(ToolboxBitmapAttribute.GetImageFromResource(t, (string) null, false), ToolboxBitmapAttribute.GetImageFromResource(t, (string) null, true))
    {
      this.imageType = t;
    }

    /// <summary>Initializes a new <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object based on a 16 by 16 bitmap that is embedded as a resource in a specified assembly.</summary>
    /// <param name="t">A <see cref="T:System.Type" /> whose defining assembly is searched for the bitmap resource.</param>
    /// <param name="name">The name of the embedded bitmap resource.</param>
    public ToolboxBitmapAttribute(Type t, string name)
      : this(ToolboxBitmapAttribute.GetImageFromResource(t, name, false), ToolboxBitmapAttribute.GetImageFromResource(t, name, true))
    {
      this.imageType = t;
      this.imageName = name;
    }

    private ToolboxBitmapAttribute(Image smallImage, Image largeImage)
    {
      this.smallImage = smallImage;
      this.largeImage = largeImage;
    }

    /// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object and is identical to this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
    /// <param name="value">The <see cref="T:System.Object" /> to test.</param>
    /// <returns>This method returns <see langword="true" /> if <paramref name="value" /> is both a <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object and is identical to this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
    public override bool Equals(object value)
    {
      if (value == this)
        return true;
      return value is ToolboxBitmapAttribute toolboxBitmapAttribute && toolboxBitmapAttribute.smallImage == this.smallImage && toolboxBitmapAttribute.largeImage == this.largeImage;
    }

    /// <summary>Gets a hash code for this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
    /// <returns>The hash code for this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
    public override int GetHashCode() => base.GetHashCode();

    /// <summary>Gets the small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
    /// <param name="component">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type of the object specified by the component parameter. For example, if you pass an object of type ControlA to the component parameter, then this method searches the assembly that defines ControlA.</param>
    /// <returns>The small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
    public Image GetImage(object component) => this.GetImage(component, true);

    /// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
    /// <param name="component">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type of the object specified by the component parameter. For example, if you pass an object of type ControlA to the component parameter, then this method searches the assembly that defines ControlA.</param>
    /// <param name="large">Specifies whether this method returns a large image (<see langword="true" />) or a small image (<see langword="false" />). The small image is 16 by 16, and the large image is 32 by 32.</param>
    /// <returns>An <see cref="T:System.Drawing.Image" /> object associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
    public Image GetImage(object component, bool large) => component != null ? this.GetImage(component.GetType(), large) : (Image) null;

    /// <summary>Gets the small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
    /// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type specified by the type parameter. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA.</param>
    /// <returns>The small <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
    public Image GetImage(Type type) => this.GetImage(type, false);

    /// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
    /// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for a bitmap resource in the assembly that defines the type specified by the component type. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA.</param>
    /// <param name="large">Specifies whether this method returns a large image (<see langword="true" />) or a small image (<see langword="false" />). The small image is 16 by 16, and the large image is 32 by 32.</param>
    /// <returns>An <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
    public Image GetImage(Type type, bool large) => this.GetImage(type, (string) null, large);

    /// <summary>Gets the small or large <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</summary>
    /// <param name="type">If this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object does not already have a small image, this method searches for an embedded bitmap resource in the assembly that defines the type specified by the component type. For example, if you pass typeof(ControlA) to the type parameter, then this method searches the assembly that defines ControlA.</param>
    /// <param name="imgName">The name of the embedded bitmap resource.</param>
    /// <param name="large">Specifies whether this method returns a large image (<see langword="true" />) or a small image (<see langword="false" />). The small image is 16 by 16, and the large image is 32 by 32.</param>
    /// <returns>An <see cref="T:System.Drawing.Image" /> associated with this <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> object.</returns>
    public Image GetImage(Type type, string imgName, bool large)
    {
      if (large && this.largeImage == null || !large && this.smallImage == null)
      {
        Image image = (!large ? this.smallImage : this.largeImage) ?? ToolboxBitmapAttribute.GetImageFromResource(type, imgName, large);
        if (large && this.largeImage == null && this.smallImage != null)
          image = (Image) new Bitmap(this.smallImage, ToolboxBitmapAttribute.largeSize.Width, ToolboxBitmapAttribute.largeSize.Height);
        if (image is Bitmap img)
          ToolboxBitmapAttribute.MakeBackgroundAlphaZero(img);
        if (image == null)
          image = ToolboxBitmapAttribute.DefaultComponent.GetImage(type, large);
        if (large)
          this.largeImage = image;
        else
          this.smallImage = image;
      }
      Image image1 = large ? this.largeImage : this.smallImage;
      if (this.Equals((object) ToolboxBitmapAttribute.Default))
      {
        this.largeImage = (Image) null;
        this.smallImage = (Image) null;
      }
      return image1;
    }

    internal Bitmap GetOriginalBitmap()
    {
      if (this.originalBitmap != null)
        return this.originalBitmap;
      if (this.smallImage == null)
        return (Bitmap) null;
      if (!DpiHelper.IsScalingRequired)
        return (Bitmap) null;
      if (!string.IsNullOrEmpty(this.imageFile))
        this.originalBitmap = ToolboxBitmapAttribute.GetImageFromFile(this.imageFile, false, false) as Bitmap;
      else if (this.imageType != (Type) null)
        this.originalBitmap = ToolboxBitmapAttribute.GetImageFromResource(this.imageType, this.imageName, false, false) as Bitmap;
      return this.originalBitmap;
    }

    private static Image GetIconFromStream(Stream stream, bool large, bool scaled)
    {
      if (stream == null)
        return (Image) null;
      Bitmap bitmap = new Icon(new Icon(stream), large ? ToolboxBitmapAttribute.largeSize : ToolboxBitmapAttribute.smallSize).ToBitmap();
      if (DpiHelper.IsScalingRequired & scaled)
        DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap);
      return (Image) bitmap;
    }

    private static string GetFileNameFromBitmapSelector(string originalName)
    {
      if (originalName != ToolboxBitmapAttribute.lastOriginalFileName)
      {
        ToolboxBitmapAttribute.lastOriginalFileName = originalName;
        ToolboxBitmapAttribute.lastUpdatedFileName = BitmapSelector.GetFileName(originalName);
      }
      return ToolboxBitmapAttribute.lastUpdatedFileName;
    }

    private static Image GetImageFromFile(string imageFile, bool large, bool scaled = true)
    {
      Image imageFromFile = (Image) null;
      try
      {
        if (imageFile != null)
        {
          imageFile = ToolboxBitmapAttribute.GetFileNameFromBitmapSelector(imageFile);
          string extension = Path.GetExtension(imageFile);
          if (extension != null && string.Equals(extension, ".ico", StringComparison.OrdinalIgnoreCase))
          {
            FileStream fileStream = File.Open(imageFile, FileMode.Open);
            if (fileStream != null)
            {
              try
              {
                imageFromFile = ToolboxBitmapAttribute.GetIconFromStream((Stream) fileStream, large, scaled);
              }
              finally
              {
                fileStream.Close();
              }
            }
          }
          else if (!large)
          {
            imageFromFile = Image.FromFile(imageFile);
            Bitmap logicalBitmap = imageFromFile as Bitmap;
            if (DpiHelper.IsScalingRequired & scaled)
              DpiHelper.ScaleBitmapLogicalToDevice(ref logicalBitmap);
          }
        }
      }
      catch (Exception ex)
      {
        if (ClientUtils.IsCriticalException(ex))
          throw;
      }
      return imageFromFile;
    }

    private static Image GetBitmapFromResource(Type t, string bitmapname, bool large, bool scaled)
    {
      if (bitmapname == null)
        return (Image) null;
      Image bitmapFromResource = (Image) null;
      Stream resourceStream = BitmapSelector.GetResourceStream(t, bitmapname);
      if (resourceStream != null)
      {
        Bitmap img = new Bitmap(resourceStream);
        bitmapFromResource = (Image) img;
        ToolboxBitmapAttribute.MakeBackgroundAlphaZero(img);
        if (large)
        {
          Bitmap original = img;
          Size largeSize = ToolboxBitmapAttribute.largeSize;
          int width = largeSize.Width;
          largeSize = ToolboxBitmapAttribute.largeSize;
          int height = largeSize.Height;
          bitmapFromResource = (Image) new Bitmap((Image) original, width, height);
        }
        if (DpiHelper.IsScalingRequired & scaled)
        {
          Bitmap logicalBitmap = (Bitmap) bitmapFromResource;
          DpiHelper.ScaleBitmapLogicalToDevice(ref logicalBitmap);
          bitmapFromResource = (Image) logicalBitmap;
        }
      }
      return bitmapFromResource;
    }

    private static Image GetIconFromResource(Type t, string bitmapname, bool large, bool scaled) => bitmapname == null ? (Image) null : ToolboxBitmapAttribute.GetIconFromStream(BitmapSelector.GetResourceStream(t, bitmapname), large, scaled);

    /// <summary>Returns an <see cref="T:System.Drawing.Image" /> object based on a bitmap resource that is embedded in an assembly.</summary>
    /// <param name="t">This method searches for an embedded bitmap resource in the assembly that defines the type specified by the t parameter. For example, if you pass typeof(ControlA) to the t parameter, then this method searches the assembly that defines ControlA.</param>
    /// <param name="imageName">The name of the embedded bitmap resource.</param>
    /// <param name="large">Specifies whether this method returns a large image (true) or a small image (false). The small image is 16 by 16, and the large image is 32 x 32.</param>
    /// <returns>An <see cref="T:System.Drawing.Image" /> object based on the retrieved bitmap.</returns>
    public static Image GetImageFromResource(Type t, string imageName, bool large) => ToolboxBitmapAttribute.GetImageFromResource(t, imageName, large, true);

    internal static Image GetImageFromResource(Type t, string imageName, bool large, bool scaled)
    {
      Image imageFromResource = (Image) null;
      try
      {
        string str1 = imageName;
        string bitmapname1 = (string) null;
        string bitmapname2 = (string) null;
        string bitmapname3 = (string) null;
        if (str1 == null)
        {
          string str2 = t.FullName;
          int num = str2.LastIndexOf('.');
          if (num != -1)
            str2 = str2.Substring(num + 1);
          bitmapname1 = str2 + ".ico";
          bitmapname2 = str2 + ".bmp";
        }
        else if (string.Compare(Path.GetExtension(imageName), ".ico", true, CultureInfo.CurrentCulture) == 0)
          bitmapname1 = str1;
        else if (string.Compare(Path.GetExtension(imageName), ".bmp", true, CultureInfo.CurrentCulture) == 0)
        {
          bitmapname2 = str1;
        }
        else
        {
          bitmapname3 = str1;
          bitmapname2 = str1 + ".bmp";
          bitmapname1 = str1 + ".ico";
        }
        if (bitmapname3 != null)
          imageFromResource = ToolboxBitmapAttribute.GetBitmapFromResource(t, bitmapname3, large, scaled);
        if (imageFromResource == null && bitmapname2 != null)
          imageFromResource = ToolboxBitmapAttribute.GetBitmapFromResource(t, bitmapname2, large, scaled);
        if (imageFromResource == null)
        {
          if (bitmapname1 != null)
            imageFromResource = ToolboxBitmapAttribute.GetIconFromResource(t, bitmapname1, large, scaled);
        }
      }
      catch (Exception ex)
      {
        int num = t == (Type) null ? 1 : 0;
      }
      return imageFromResource;
    }

    private static void MakeBackgroundAlphaZero(Bitmap img)
    {
      Color pixel = img.GetPixel(0, img.Height - 1);
      img.MakeTransparent();
      Color color = Color.FromArgb(0, pixel);
      img.SetPixel(0, img.Height - 1, color);
    }

    static ToolboxBitmapAttribute()
    {
      SafeNativeMethods.Gdip.DummyFunction();
      Bitmap bitmap = (Bitmap) null;
      Stream resourceStream = BitmapSelector.GetResourceStream(typeof (ToolboxBitmapAttribute), "DefaultComponent.bmp");
      if (resourceStream != null)
      {
        bitmap = new Bitmap(resourceStream);
        ToolboxBitmapAttribute.MakeBackgroundAlphaZero(bitmap);
      }
      ToolboxBitmapAttribute.DefaultComponent = new ToolboxBitmapAttribute((Image) bitmap, (Image) null);
    }
  }
}
