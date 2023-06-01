// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ColorPalette
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  /// <summary>Defines an array of colors that make up a color palette. The colors are 32-bit ARGB colors. Not inheritable.</summary>
  public sealed class ColorPalette
  {
    private int flags;
    private Color[] entries;

    /// <summary>Gets a value that specifies how to interpret the color information in the array of colors.</summary>
    /// <returns>The following flag values are valid:
    /// 0x00000001 The color values in the array contain alpha information.
    /// 0x00000002 The colors in the array are grayscale values.
    /// 0x00000004 The colors in the array are halftone values.</returns>
    public int Flags => this.flags;

    /// <summary>Gets an array of <see cref="T:System.Drawing.Color" /> structures.</summary>
    /// <returns>The array of <see cref="T:System.Drawing.Color" /> structure that make up this <see cref="T:System.Drawing.Imaging.ColorPalette" />.</returns>
    public Color[] Entries => this.entries;

    internal ColorPalette(int count) => this.entries = new Color[count];

    internal ColorPalette() => this.entries = new Color[1];

    internal void ConvertFromMemory(IntPtr memory)
    {
      this.flags = Marshal.ReadInt32(memory);
      int length = Marshal.ReadInt32((IntPtr) ((long) memory + 4L));
      this.entries = new Color[length];
      for (int index = 0; index < length; ++index)
      {
        int argb = Marshal.ReadInt32((IntPtr) ((long) memory + 8L + (long) (index * 4)));
        this.entries[index] = Color.FromArgb(argb);
      }
    }

    internal IntPtr ConvertToMemory()
    {
      int length = this.entries.Length;
      IntPtr ptr = Marshal.AllocHGlobal(checked (4 * 2 + length));
      Marshal.WriteInt32(ptr, 0, this.flags);
      Marshal.WriteInt32((IntPtr) checked (unchecked ((long) ptr) + 4L), 0, length);
      for (int index = 0; index < length; ++index)
        Marshal.WriteInt32((IntPtr) ((long) ptr + (long) (4 * (index + 2))), 0, this.entries[index].ToArgb());
      return ptr;
    }
  }
}
