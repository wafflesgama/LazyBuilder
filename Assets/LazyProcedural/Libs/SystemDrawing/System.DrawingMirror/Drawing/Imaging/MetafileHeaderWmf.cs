﻿// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.MetafileHeaderWmf
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
  [StructLayout(LayoutKind.Sequential, Pack = 8)]
  internal class MetafileHeaderWmf
  {
    public MetafileType type;
    public int size = Marshal.SizeOf(typeof (MetafileHeaderWmf));
    public int version;
    public EmfPlusFlags emfPlusFlags;
    public float dpiX;
    public float dpiY;
    public int X;
    public int Y;
    public int Width;
    public int Height;
    [MarshalAs(UnmanagedType.Struct)]
    public MetaHeader WmfHeader = new MetaHeader();
    public int dummy1;
    public int dummy2;
    public int dummy3;
    public int dummy4;
    public int dummy5;
    public int dummy6;
    public int dummy7;
    public int dummy8;
    public int dummy9;
    public int dummy10;
    public int dummy11;
    public int dummy12;
    public int dummy13;
    public int dummy14;
    public int dummy15;
    public int dummy16;
    public int EmfPlusHeaderSize;
    public int LogicalDpiX;
    public int LogicalDpiY;
  }
}
