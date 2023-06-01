// Decompiled with JetBrains decompiler
// Type: System.Drawing.SafeNativeMethods
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Drawing
{
  [SuppressUnmanagedCodeSecurity]
  internal class SafeNativeMethods
  {
    public static IntPtr InvalidIntPtr = (IntPtr) -1;
    public const int ERROR_CANCELLED = 1223;
    public const int RASTERCAPS = 38;
    public const int RC_PALETTE = 256;
    public const int SIZEPALETTE = 104;
    public const int SYSPAL_STATIC = 1;
    public const int BS_SOLID = 0;
    public const int HOLLOW_BRUSH = 5;
    public const int R2_BLACK = 1;
    public const int R2_NOTMERGEPEN = 2;
    public const int R2_MASKNOTPEN = 3;
    public const int R2_NOTCOPYPEN = 4;
    public const int R2_MASKPENNOT = 5;
    public const int R2_NOT = 6;
    public const int R2_XORPEN = 7;
    public const int R2_NOTMASKPEN = 8;
    public const int R2_MASKPEN = 9;
    public const int R2_NOTXORPEN = 10;
    public const int R2_NOP = 11;
    public const int R2_MERGENOTPEN = 12;
    public const int R2_COPYPEN = 13;
    public const int R2_MERGEPENNOT = 14;
    public const int R2_MERGEPEN = 15;
    public const int R2_WHITE = 16;
    public const int UOI_FLAGS = 1;
    public const int WSF_VISIBLE = 1;
    public const int E_UNEXPECTED = -2147418113;
    public const int E_NOTIMPL = -2147467263;
    public const int E_OUTOFMEMORY = -2147024882;
    public const int E_INVALIDARG = -2147024809;
    public const int E_NOINTERFACE = -2147467262;
    public const int E_POINTER = -2147467261;
    public const int E_HANDLE = -2147024890;
    public const int E_ABORT = -2147467260;
    public const int E_FAIL = -2147467259;
    public const int E_ACCESSDENIED = -2147024891;
    public const int PM_NOREMOVE = 0;
    public const int PM_REMOVE = 1;
    public const int PM_NOYIELD = 2;
    public const int GMEM_FIXED = 0;
    public const int GMEM_MOVEABLE = 2;
    public const int GMEM_NOCOMPACT = 16;
    public const int GMEM_NODISCARD = 32;
    public const int GMEM_ZEROINIT = 64;
    public const int GMEM_MODIFY = 128;
    public const int GMEM_DISCARDABLE = 256;
    public const int GMEM_NOT_BANKED = 4096;
    public const int GMEM_SHARE = 8192;
    public const int GMEM_DDESHARE = 8192;
    public const int GMEM_NOTIFY = 16384;
    public const int GMEM_LOWER = 4096;
    public const int GMEM_VALID_FLAGS = 32626;
    public const int GMEM_INVALID_HANDLE = 32768;
    public const int DM_UPDATE = 1;
    public const int DM_COPY = 2;
    public const int DM_PROMPT = 4;
    public const int DM_MODIFY = 8;
    public const int DM_IN_BUFFER = 8;
    public const int DM_IN_PROMPT = 4;
    public const int DM_OUT_BUFFER = 2;
    public const int DM_OUT_DEFAULT = 1;
    public const int DT_PLOTTER = 0;
    public const int DT_RASDISPLAY = 1;
    public const int DT_RASPRINTER = 2;
    public const int DT_RASCAMERA = 3;
    public const int DT_CHARSTREAM = 4;
    public const int DT_METAFILE = 5;
    public const int DT_DISPFILE = 6;
    public const int TECHNOLOGY = 2;
    public const int DC_FIELDS = 1;
    public const int DC_PAPERS = 2;
    public const int DC_PAPERSIZE = 3;
    public const int DC_MINEXTENT = 4;
    public const int DC_MAXEXTENT = 5;
    public const int DC_BINS = 6;
    public const int DC_DUPLEX = 7;
    public const int DC_SIZE = 8;
    public const int DC_EXTRA = 9;
    public const int DC_VERSION = 10;
    public const int DC_DRIVER = 11;
    public const int DC_BINNAMES = 12;
    public const int DC_ENUMRESOLUTIONS = 13;
    public const int DC_FILEDEPENDENCIES = 14;
    public const int DC_TRUETYPE = 15;
    public const int DC_PAPERNAMES = 16;
    public const int DC_ORIENTATION = 17;
    public const int DC_COPIES = 18;
    public const int PD_ALLPAGES = 0;
    public const int PD_SELECTION = 1;
    public const int PD_PAGENUMS = 2;
    public const int PD_CURRENTPAGE = 4194304;
    public const int PD_NOSELECTION = 4;
    public const int PD_NOPAGENUMS = 8;
    public const int PD_NOCURRENTPAGE = 8388608;
    public const int PD_COLLATE = 16;
    public const int PD_PRINTTOFILE = 32;
    public const int PD_PRINTSETUP = 64;
    public const int PD_NOWARNING = 128;
    public const int PD_RETURNDC = 256;
    public const int PD_RETURNIC = 512;
    public const int PD_RETURNDEFAULT = 1024;
    public const int PD_SHOWHELP = 2048;
    public const int PD_ENABLEPRINTHOOK = 4096;
    public const int PD_ENABLESETUPHOOK = 8192;
    public const int PD_ENABLEPRINTTEMPLATE = 16384;
    public const int PD_ENABLESETUPTEMPLATE = 32768;
    public const int PD_ENABLEPRINTTEMPLATEHANDLE = 65536;
    public const int PD_ENABLESETUPTEMPLATEHANDLE = 131072;
    public const int PD_USEDEVMODECOPIES = 262144;
    public const int PD_USEDEVMODECOPIESANDCOLLATE = 262144;
    public const int PD_DISABLEPRINTTOFILE = 524288;
    public const int PD_HIDEPRINTTOFILE = 1048576;
    public const int PD_NONETWORKBUTTON = 2097152;
    public const int DI_MASK = 1;
    public const int DI_IMAGE = 2;
    public const int DI_NORMAL = 3;
    public const int DI_COMPAT = 4;
    public const int DI_DEFAULTSIZE = 8;
    public const int IDC_ARROW = 32512;
    public const int IDC_IBEAM = 32513;
    public const int IDC_WAIT = 32514;
    public const int IDC_CROSS = 32515;
    public const int IDC_UPARROW = 32516;
    public const int IDC_SIZE = 32640;
    public const int IDC_ICON = 32641;
    public const int IDC_SIZENWSE = 32642;
    public const int IDC_SIZENESW = 32643;
    public const int IDC_SIZEWE = 32644;
    public const int IDC_SIZENS = 32645;
    public const int IDC_SIZEALL = 32646;
    public const int IDC_NO = 32648;
    public const int IDC_APPSTARTING = 32650;
    public const int IDC_HELP = 32651;
    public const int IMAGE_BITMAP = 0;
    public const int IMAGE_ICON = 1;
    public const int IMAGE_CURSOR = 2;
    public const int IMAGE_ENHMETAFILE = 3;
    public const int IDI_APPLICATION = 32512;
    public const int IDI_HAND = 32513;
    public const int IDI_QUESTION = 32514;
    public const int IDI_EXCLAMATION = 32515;
    public const int IDI_ASTERISK = 32516;
    public const int IDI_WINLOGO = 32517;
    public const int IDI_WARNING = 32515;
    public const int IDI_ERROR = 32513;
    public const int IDI_INFORMATION = 32516;
    public const int IDI_SHIELD = 32518;
    public const int SRCCOPY = 13369376;
    public const int PLANES = 14;
    public const int PS_SOLID = 0;
    public const int PS_DASH = 1;
    public const int PS_DOT = 2;
    public const int PS_DASHDOT = 3;
    public const int PS_DASHDOTDOT = 4;
    public const int PS_NULL = 5;
    public const int PS_INSIDEFRAME = 6;
    public const int PS_USERSTYLE = 7;
    public const int PS_ALTERNATE = 8;
    public const int PS_STYLE_MASK = 15;
    public const int PS_ENDCAP_ROUND = 0;
    public const int PS_ENDCAP_SQUARE = 256;
    public const int PS_ENDCAP_FLAT = 512;
    public const int PS_ENDCAP_MASK = 3840;
    public const int PS_JOIN_ROUND = 0;
    public const int PS_JOIN_BEVEL = 4096;
    public const int PS_JOIN_MITER = 8192;
    public const int PS_JOIN_MASK = 61440;
    public const int PS_COSMETIC = 0;
    public const int PS_GEOMETRIC = 65536;
    public const int PS_TYPE_MASK = 983040;
    public const int BITSPIXEL = 12;
    public const int ALTERNATE = 1;
    public const int LOGPIXELSX = 88;
    public const int LOGPIXELSY = 90;
    public const int PHYSICALWIDTH = 110;
    public const int PHYSICALHEIGHT = 111;
    public const int PHYSICALOFFSETX = 112;
    public const int PHYSICALOFFSETY = 113;
    public const int WINDING = 2;
    public const int VERTRES = 10;
    public const int HORZRES = 8;
    public const int DM_SPECVERSION = 1025;
    public const int DM_ORIENTATION = 1;
    public const int DM_PAPERSIZE = 2;
    public const int DM_PAPERLENGTH = 4;
    public const int DM_PAPERWIDTH = 8;
    public const int DM_SCALE = 16;
    public const int DM_COPIES = 256;
    public const int DM_DEFAULTSOURCE = 512;
    public const int DM_PRINTQUALITY = 1024;
    public const int DM_COLOR = 2048;
    public const int DM_DUPLEX = 4096;
    public const int DM_YRESOLUTION = 8192;
    public const int DM_TTOPTION = 16384;
    public const int DM_COLLATE = 32768;
    public const int DM_FORMNAME = 65536;
    public const int DM_LOGPIXELS = 131072;
    public const int DM_BITSPERPEL = 262144;
    public const int DM_PELSWIDTH = 524288;
    public const int DM_PELSHEIGHT = 1048576;
    public const int DM_DISPLAYFLAGS = 2097152;
    public const int DM_DISPLAYFREQUENCY = 4194304;
    public const int DM_PANNINGWIDTH = 8388608;
    public const int DM_PANNINGHEIGHT = 16777216;
    public const int DM_ICMMETHOD = 33554432;
    public const int DM_ICMINTENT = 67108864;
    public const int DM_MEDIATYPE = 134217728;
    public const int DM_DITHERTYPE = 268435456;
    public const int DM_ICCMANUFACTURER = 536870912;
    public const int DM_ICCMODEL = 1073741824;
    public const int DMORIENT_PORTRAIT = 1;
    public const int DMORIENT_LANDSCAPE = 2;
    public const int DMPAPER_LETTER = 1;
    public const int DMPAPER_LETTERSMALL = 2;
    public const int DMPAPER_TABLOID = 3;
    public const int DMPAPER_LEDGER = 4;
    public const int DMPAPER_LEGAL = 5;
    public const int DMPAPER_STATEMENT = 6;
    public const int DMPAPER_EXECUTIVE = 7;
    public const int DMPAPER_A3 = 8;
    public const int DMPAPER_A4 = 9;
    public const int DMPAPER_A4SMALL = 10;
    public const int DMPAPER_A5 = 11;
    public const int DMPAPER_B4 = 12;
    public const int DMPAPER_B5 = 13;
    public const int DMPAPER_FOLIO = 14;
    public const int DMPAPER_QUARTO = 15;
    public const int DMPAPER_10X14 = 16;
    public const int DMPAPER_11X17 = 17;
    public const int DMPAPER_NOTE = 18;
    public const int DMPAPER_ENV_9 = 19;
    public const int DMPAPER_ENV_10 = 20;
    public const int DMPAPER_ENV_11 = 21;
    public const int DMPAPER_ENV_12 = 22;
    public const int DMPAPER_ENV_14 = 23;
    public const int DMPAPER_CSHEET = 24;
    public const int DMPAPER_DSHEET = 25;
    public const int DMPAPER_ESHEET = 26;
    public const int DMPAPER_ENV_DL = 27;
    public const int DMPAPER_ENV_C5 = 28;
    public const int DMPAPER_ENV_C3 = 29;
    public const int DMPAPER_ENV_C4 = 30;
    public const int DMPAPER_ENV_C6 = 31;
    public const int DMPAPER_ENV_C65 = 32;
    public const int DMPAPER_ENV_B4 = 33;
    public const int DMPAPER_ENV_B5 = 34;
    public const int DMPAPER_ENV_B6 = 35;
    public const int DMPAPER_ENV_ITALY = 36;
    public const int DMPAPER_ENV_MONARCH = 37;
    public const int DMPAPER_ENV_PERSONAL = 38;
    public const int DMPAPER_FANFOLD_US = 39;
    public const int DMPAPER_FANFOLD_STD_GERMAN = 40;
    public const int DMPAPER_FANFOLD_LGL_GERMAN = 41;
    public const int DMPAPER_ISO_B4 = 42;
    public const int DMPAPER_JAPANESE_POSTCARD = 43;
    public const int DMPAPER_9X11 = 44;
    public const int DMPAPER_10X11 = 45;
    public const int DMPAPER_15X11 = 46;
    public const int DMPAPER_ENV_INVITE = 47;
    public const int DMPAPER_RESERVED_48 = 48;
    public const int DMPAPER_RESERVED_49 = 49;
    public const int DMPAPER_LETTER_EXTRA = 50;
    public const int DMPAPER_LEGAL_EXTRA = 51;
    public const int DMPAPER_TABLOID_EXTRA = 52;
    public const int DMPAPER_A4_EXTRA = 53;
    public const int DMPAPER_LETTER_TRANSVERSE = 54;
    public const int DMPAPER_A4_TRANSVERSE = 55;
    public const int DMPAPER_LETTER_EXTRA_TRANSVERSE = 56;
    public const int DMPAPER_A_PLUS = 57;
    public const int DMPAPER_B_PLUS = 58;
    public const int DMPAPER_LETTER_PLUS = 59;
    public const int DMPAPER_A4_PLUS = 60;
    public const int DMPAPER_A5_TRANSVERSE = 61;
    public const int DMPAPER_B5_TRANSVERSE = 62;
    public const int DMPAPER_A3_EXTRA = 63;
    public const int DMPAPER_A5_EXTRA = 64;
    public const int DMPAPER_B5_EXTRA = 65;
    public const int DMPAPER_A2 = 66;
    public const int DMPAPER_A3_TRANSVERSE = 67;
    public const int DMPAPER_A3_EXTRA_TRANSVERSE = 68;
    public const int DMPAPER_DBL_JAPANESE_POSTCARD = 69;
    public const int DMPAPER_A6 = 70;
    public const int DMPAPER_JENV_KAKU2 = 71;
    public const int DMPAPER_JENV_KAKU3 = 72;
    public const int DMPAPER_JENV_CHOU3 = 73;
    public const int DMPAPER_JENV_CHOU4 = 74;
    public const int DMPAPER_LETTER_ROTATED = 75;
    public const int DMPAPER_A3_ROTATED = 76;
    public const int DMPAPER_A4_ROTATED = 77;
    public const int DMPAPER_A5_ROTATED = 78;
    public const int DMPAPER_B4_JIS_ROTATED = 79;
    public const int DMPAPER_B5_JIS_ROTATED = 80;
    public const int DMPAPER_JAPANESE_POSTCARD_ROTATED = 81;
    public const int DMPAPER_DBL_JAPANESE_POSTCARD_ROTATED = 82;
    public const int DMPAPER_A6_ROTATED = 83;
    public const int DMPAPER_JENV_KAKU2_ROTATED = 84;
    public const int DMPAPER_JENV_KAKU3_ROTATED = 85;
    public const int DMPAPER_JENV_CHOU3_ROTATED = 86;
    public const int DMPAPER_JENV_CHOU4_ROTATED = 87;
    public const int DMPAPER_B6_JIS = 88;
    public const int DMPAPER_B6_JIS_ROTATED = 89;
    public const int DMPAPER_12X11 = 90;
    public const int DMPAPER_JENV_YOU4 = 91;
    public const int DMPAPER_JENV_YOU4_ROTATED = 92;
    public const int DMPAPER_P16K = 93;
    public const int DMPAPER_P32K = 94;
    public const int DMPAPER_P32KBIG = 95;
    public const int DMPAPER_PENV_1 = 96;
    public const int DMPAPER_PENV_2 = 97;
    public const int DMPAPER_PENV_3 = 98;
    public const int DMPAPER_PENV_4 = 99;
    public const int DMPAPER_PENV_5 = 100;
    public const int DMPAPER_PENV_6 = 101;
    public const int DMPAPER_PENV_7 = 102;
    public const int DMPAPER_PENV_8 = 103;
    public const int DMPAPER_PENV_9 = 104;
    public const int DMPAPER_PENV_10 = 105;
    public const int DMPAPER_P16K_ROTATED = 106;
    public const int DMPAPER_P32K_ROTATED = 107;
    public const int DMPAPER_P32KBIG_ROTATED = 108;
    public const int DMPAPER_PENV_1_ROTATED = 109;
    public const int DMPAPER_PENV_2_ROTATED = 110;
    public const int DMPAPER_PENV_3_ROTATED = 111;
    public const int DMPAPER_PENV_4_ROTATED = 112;
    public const int DMPAPER_PENV_5_ROTATED = 113;
    public const int DMPAPER_PENV_6_ROTATED = 114;
    public const int DMPAPER_PENV_7_ROTATED = 115;
    public const int DMPAPER_PENV_8_ROTATED = 116;
    public const int DMPAPER_PENV_9_ROTATED = 117;
    public const int DMPAPER_PENV_10_ROTATED = 118;
    public const int DMPAPER_LAST = 118;
    public const int DMPAPER_USER = 256;
    public const int DMBIN_UPPER = 1;
    public const int DMBIN_ONLYONE = 1;
    public const int DMBIN_LOWER = 2;
    public const int DMBIN_MIDDLE = 3;
    public const int DMBIN_MANUAL = 4;
    public const int DMBIN_ENVELOPE = 5;
    public const int DMBIN_ENVMANUAL = 6;
    public const int DMBIN_AUTO = 7;
    public const int DMBIN_TRACTOR = 8;
    public const int DMBIN_SMALLFMT = 9;
    public const int DMBIN_LARGEFMT = 10;
    public const int DMBIN_LARGECAPACITY = 11;
    public const int DMBIN_CASSETTE = 14;
    public const int DMBIN_FORMSOURCE = 15;
    public const int DMBIN_LAST = 15;
    public const int DMBIN_USER = 256;
    public const int DMRES_DRAFT = -1;
    public const int DMRES_LOW = -2;
    public const int DMRES_MEDIUM = -3;
    public const int DMRES_HIGH = -4;
    public const int DMCOLOR_MONOCHROME = 1;
    public const int DMCOLOR_COLOR = 2;
    public const int DMDUP_SIMPLEX = 1;
    public const int DMDUP_VERTICAL = 2;
    public const int DMDUP_HORIZONTAL = 3;
    public const int DMTT_BITMAP = 1;
    public const int DMTT_DOWNLOAD = 2;
    public const int DMTT_SUBDEV = 3;
    public const int DMTT_DOWNLOAD_OUTLINE = 4;
    public const int DMCOLLATE_FALSE = 0;
    public const int DMCOLLATE_TRUE = 1;
    public const int DMDISPLAYFLAGS_TEXTMODE = 4;
    public const int DMICMMETHOD_NONE = 1;
    public const int DMICMMETHOD_SYSTEM = 2;
    public const int DMICMMETHOD_DRIVER = 3;
    public const int DMICMMETHOD_DEVICE = 4;
    public const int DMICMMETHOD_USER = 256;
    public const int DMICM_SATURATE = 1;
    public const int DMICM_CONTRAST = 2;
    public const int DMICM_COLORMETRIC = 3;
    public const int DMICM_USER = 256;
    public const int DMMEDIA_STANDARD = 1;
    public const int DMMEDIA_TRANSPARENCY = 2;
    public const int DMMEDIA_GLOSSY = 3;
    public const int DMMEDIA_USER = 256;
    public const int DMDITHER_NONE = 1;
    public const int DMDITHER_COARSE = 2;
    public const int DMDITHER_FINE = 3;
    public const int DMDITHER_LINEART = 4;
    public const int DMDITHER_GRAYSCALE = 5;
    public const int DMDITHER_USER = 256;
    public const int PRINTER_ENUM_DEFAULT = 1;
    public const int PRINTER_ENUM_LOCAL = 2;
    public const int PRINTER_ENUM_CONNECTIONS = 4;
    public const int PRINTER_ENUM_FAVORITE = 4;
    public const int PRINTER_ENUM_NAME = 8;
    public const int PRINTER_ENUM_REMOTE = 16;
    public const int PRINTER_ENUM_SHARED = 32;
    public const int PRINTER_ENUM_NETWORK = 64;
    public const int PRINTER_ENUM_EXPAND = 16384;
    public const int PRINTER_ENUM_CONTAINER = 32768;
    public const int PRINTER_ENUM_ICONMASK = 16711680;
    public const int PRINTER_ENUM_ICON1 = 65536;
    public const int PRINTER_ENUM_ICON2 = 131072;
    public const int PRINTER_ENUM_ICON3 = 262144;
    public const int PRINTER_ENUM_ICON4 = 524288;
    public const int PRINTER_ENUM_ICON5 = 1048576;
    public const int PRINTER_ENUM_ICON6 = 2097152;
    public const int PRINTER_ENUM_ICON7 = 4194304;
    public const int PRINTER_ENUM_ICON8 = 8388608;
    public const int DC_BINADJUST = 19;
    public const int DC_EMF_COMPLIANT = 20;
    public const int DC_DATATYPE_PRODUCED = 21;
    public const int DC_COLLATE = 22;
    public const int DCTT_BITMAP = 1;
    public const int DCTT_DOWNLOAD = 2;
    public const int DCTT_SUBDEV = 4;
    public const int DCTT_DOWNLOAD_OUTLINE = 8;
    public const int DCBA_FACEUPNONE = 0;
    public const int DCBA_FACEUPCENTER = 1;
    public const int DCBA_FACEUPLEFT = 2;
    public const int DCBA_FACEUPRIGHT = 3;
    public const int DCBA_FACEDOWNNONE = 256;
    public const int DCBA_FACEDOWNCENTER = 257;
    public const int DCBA_FACEDOWNLEFT = 258;
    public const int DCBA_FACEDOWNRIGHT = 259;
    public const int SRCPAINT = 15597702;
    public const int SRCAND = 8913094;
    public const int SRCINVERT = 6684742;
    public const int SRCERASE = 4457256;
    public const int NOTSRCCOPY = 3342344;
    public const int NOTSRCERASE = 1114278;
    public const int MERGECOPY = 12583114;
    public const int MERGEPAINT = 12255782;
    public const int PATCOPY = 15728673;
    public const int PATPAINT = 16452105;
    public const int PATINVERT = 5898313;
    public const int DSTINVERT = 5570569;
    public const int BLACKNESS = 66;
    public const int WHITENESS = 16711778;
    public const int CAPTUREBLT = 1073741824;
    public const int SM_CXSCREEN = 0;
    public const int SM_CYSCREEN = 1;
    public const int SM_CXVSCROLL = 2;
    public const int SM_CYHSCROLL = 3;
    public const int SM_CYCAPTION = 4;
    public const int SM_CXBORDER = 5;
    public const int SM_CYBORDER = 6;
    public const int SM_CXDLGFRAME = 7;
    public const int SM_CYDLGFRAME = 8;
    public const int SM_CYVTHUMB = 9;
    public const int SM_CXHTHUMB = 10;
    public const int SM_CXICON = 11;
    public const int SM_CYICON = 12;
    public const int SM_CXCURSOR = 13;
    public const int SM_CYCURSOR = 14;
    public const int SM_CYMENU = 15;
    public const int SM_CXFULLSCREEN = 16;
    public const int SM_CYFULLSCREEN = 17;
    public const int SM_CYKANJIWINDOW = 18;
    public const int SM_MOUSEPRESENT = 19;
    public const int SM_CYVSCROLL = 20;
    public const int SM_CXHSCROLL = 21;
    public const int SM_DEBUG = 22;
    public const int SM_SWAPBUTTON = 23;
    public const int SM_RESERVED1 = 24;
    public const int SM_RESERVED2 = 25;
    public const int SM_RESERVED3 = 26;
    public const int SM_RESERVED4 = 27;
    public const int SM_CXMIN = 28;
    public const int SM_CYMIN = 29;
    public const int SM_CXSIZE = 30;
    public const int SM_CYSIZE = 31;
    public const int SM_CXFRAME = 32;
    public const int SM_CYFRAME = 33;
    public const int SM_CXMINTRACK = 34;
    public const int SM_CYMINTRACK = 35;
    public const int SM_CXDOUBLECLK = 36;
    public const int SM_CYDOUBLECLK = 37;
    public const int SM_CXICONSPACING = 38;
    public const int SM_CYICONSPACING = 39;
    public const int SM_MENUDROPALIGNMENT = 40;
    public const int SM_PENWINDOWS = 41;
    public const int SM_DBCSENABLED = 42;
    public const int SM_CMOUSEBUTTONS = 43;
    public const int SM_CXFIXEDFRAME = 7;
    public const int SM_CYFIXEDFRAME = 8;
    public const int SM_CXSIZEFRAME = 32;
    public const int SM_CYSIZEFRAME = 33;
    public const int SM_SECURE = 44;
    public const int SM_CXEDGE = 45;
    public const int SM_CYEDGE = 46;
    public const int SM_CXMINSPACING = 47;
    public const int SM_CYMINSPACING = 48;
    public const int SM_CXSMICON = 49;
    public const int SM_CYSMICON = 50;
    public const int SM_CYSMCAPTION = 51;
    public const int SM_CXSMSIZE = 52;
    public const int SM_CYSMSIZE = 53;
    public const int SM_CXMENUSIZE = 54;
    public const int SM_CYMENUSIZE = 55;
    public const int SM_ARRANGE = 56;
    public const int SM_CXMINIMIZED = 57;
    public const int SM_CYMINIMIZED = 58;
    public const int SM_CXMAXTRACK = 59;
    public const int SM_CYMAXTRACK = 60;
    public const int SM_CXMAXIMIZED = 61;
    public const int SM_CYMAXIMIZED = 62;
    public const int SM_NETWORK = 63;
    public const int SM_CLEANBOOT = 67;
    public const int SM_CXDRAG = 68;
    public const int SM_CYDRAG = 69;
    public const int SM_SHOWSOUNDS = 70;
    public const int SM_CXMENUCHECK = 71;
    public const int SM_CYMENUCHECK = 72;
    public const int SM_SLOWMACHINE = 73;
    public const int SM_MIDEASTENABLED = 74;
    public const int SM_MOUSEWHEELPRESENT = 75;
    public const int SM_XVIRTUALSCREEN = 76;
    public const int SM_YVIRTUALSCREEN = 77;
    public const int SM_CXVIRTUALSCREEN = 78;
    public const int SM_CYVIRTUALSCREEN = 79;
    public const int SM_CMONITORS = 80;
    public const int SM_SAMEDISPLAYFORMAT = 81;
    public const int SM_CMETRICS = 83;
    public const int GM_COMPATIBLE = 1;
    public const int GM_ADVANCED = 2;
    public const int MWT_IDENTITY = 1;
    public const int FW_DONTCARE = 0;
    public const int FW_NORMAL = 400;
    public const int FW_BOLD = 700;
    public const int ANSI_CHARSET = 0;
    public const int DEFAULT_CHARSET = 1;
    public const int OUT_DEFAULT_PRECIS = 0;
    public const int OUT_TT_PRECIS = 4;
    public const int OUT_TT_ONLY_PRECIS = 7;
    public const int CLIP_DEFAULT_PRECIS = 0;
    public const int DEFAULT_QUALITY = 0;
    public const int MM_TEXT = 1;
    public const int OBJ_FONT = 6;
    public const int TA_DEFAULT = 0;
    public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
    public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
    public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
    public const int FORMAT_MESSAGE_DEFAULT = 4608;
    public const int NOMIRRORBITMAP = -2147483648;
    public const int QUERYESCSUPPORT = 8;
    public const int CHECKJPEGFORMAT = 4119;
    public const int CHECKPNGFORMAT = 4120;
    public const int ERROR_ACCESS_DENIED = 5;
    public const int ERROR_INVALID_PARAMETER = 87;
    public const int ERROR_PROC_NOT_FOUND = 127;

    [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntCreateCompatibleBitmap(HandleRef hDC, int width, int height);

    public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height) => System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateCompatibleBitmap(hDC, width, height), SafeNativeMethods.CommonHandles.GDI);

    [DllImport("gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntCreateBitmap(
      int width,
      int height,
      int planes,
      int bpp,
      IntPtr bitmapData);

    public static IntPtr CreateBitmap(
      int width,
      int height,
      int planes,
      int bpp,
      IntPtr bitmapData)
    {
      return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateBitmap(width, height, planes, bpp, bitmapData), SafeNativeMethods.CommonHandles.GDI);
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int BitBlt(
      HandleRef hDC,
      int x,
      int y,
      int nWidth,
      int nHeight,
      HandleRef hSrcDC,
      int xSrc,
      int ySrc,
      int dwRop);

    [DllImport("gdi32.dll")]
    public static extern int GetDIBits(
      HandleRef hdc,
      HandleRef hbm,
      int arg1,
      int arg2,
      IntPtr arg3,
      ref NativeMethods.BITMAPINFO_FLAT bmi,
      int arg5);

    [DllImport("gdi32.dll")]
    public static extern uint GetPaletteEntries(
      HandleRef hpal,
      int iStartIndex,
      int nEntries,
      byte[] lppe);

    [DllImport("gdi32.dll", EntryPoint = "CreateDIBSection", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntCreateDIBSection(
      HandleRef hdc,
      ref NativeMethods.BITMAPINFO_FLAT bmi,
      int iUsage,
      ref IntPtr ppvBits,
      IntPtr hSection,
      int dwOffset);

    public static IntPtr CreateDIBSection(
      HandleRef hdc,
      ref NativeMethods.BITMAPINFO_FLAT bmi,
      int iUsage,
      ref IntPtr ppvBits,
      IntPtr hSection,
      int dwOffset)
    {
      return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateDIBSection(hdc, ref bmi, iUsage, ref ppvBits, hSection, dwOffset), SafeNativeMethods.CommonHandles.GDI);
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GlobalFree(HandleRef handle);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int StartDoc(HandleRef hDC, SafeNativeMethods.DOCINFO lpDocInfo);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int StartPage(HandleRef hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int EndPage(HandleRef hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int AbortDoc(HandleRef hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int EndDoc(HandleRef hDC);

    [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool PrintDlg([In, Out] SafeNativeMethods.PRINTDLG lppd);

    [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool PrintDlg([In, Out] SafeNativeMethods.PRINTDLGX86 lppd);

    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int DeviceCapabilities(
      string pDevice,
      string pPort,
      short fwCapabilities,
      IntPtr pOutput,
      IntPtr pDevMode);

    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
    public static extern int DocumentProperties(
      HandleRef hwnd,
      HandleRef hPrinter,
      string pDeviceName,
      IntPtr pDevModeOutput,
      HandleRef pDevModeInput,
      int fMode);

    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
    public static extern int DocumentProperties(
      HandleRef hwnd,
      HandleRef hPrinter,
      string pDeviceName,
      IntPtr pDevModeOutput,
      IntPtr pDevModeInput,
      int fMode);

    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int EnumPrinters(
      int flags,
      string name,
      int level,
      IntPtr pPrinterEnum,
      int cbBuf,
      out int pcbNeeded,
      out int pcReturned);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GlobalLock(HandleRef handle);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr ResetDC(HandleRef hDC, HandleRef lpDevMode);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool GlobalUnlock(HandleRef handle);

    [DllImport("gdi32.dll", EntryPoint = "CreateRectRgn", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

    public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2) => System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateRectRgn(x1, y1, x2, y2), SafeNativeMethods.CommonHandles.GDI);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int AddFontResourceEx(string lpszFilename, int fl, IntPtr pdv);

    public static int AddFontFile(string fileName) => Marshal.SystemDefaultCharSize == 1 ? 0 : SafeNativeMethods.AddFontResourceEx(fileName, 16, IntPtr.Zero);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int RemoveFontResourceEx(string lpszFilename, int fl, IntPtr pdv);

    public static int RemoveFontFile(string fileName) => SafeNativeMethods.RemoveFontResourceEx(fileName, 16, IntPtr.Zero);

    internal static IntPtr SaveClipRgn(IntPtr hDC)
    {
      IntPtr handle = SafeNativeMethods.CreateRectRgn(0, 0, 0, 0);
      IntPtr num = IntPtr.Zero;
      try
      {
        if (SafeNativeMethods.GetClipRgn(new HandleRef((object) null, hDC), new HandleRef((object) null, handle)) > 0)
        {
          num = handle;
          handle = IntPtr.Zero;
        }
      }
      finally
      {
        if (handle != IntPtr.Zero)
          SafeNativeMethods.DeleteObject(new HandleRef((object) null, handle));
      }
      return num;
    }

    internal static void RestoreClipRgn(IntPtr hDC, IntPtr hRgn)
    {
      try
      {
        SafeNativeMethods.SelectClipRgn(new HandleRef((object) null, hDC), new HandleRef((object) null, hRgn));
      }
      finally
      {
        if (hRgn != IntPtr.Zero)
          SafeNativeMethods.DeleteObject(new HandleRef((object) null, hRgn));
      }
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int ExtEscape(
      HandleRef hDC,
      int nEscape,
      int cbInput,
      ref int inData,
      int cbOutput,
      out int outData);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int ExtEscape(
      HandleRef hDC,
      int nEscape,
      int cbInput,
      byte[] inData,
      int cbOutput,
      out int outData);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int IntersectClipRect(HandleRef hDC, int x1, int y1, int x2, int y2);

    [DllImport("kernel32.dll", EntryPoint = "GlobalAlloc", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr IntGlobalAlloc(int uFlags, UIntPtr dwBytes);

    public static IntPtr GlobalAlloc(int uFlags, uint dwBytes) => SafeNativeMethods.IntGlobalAlloc(uFlags, new UIntPtr(dwBytes));

    [DllImport("kernel32.dll")]
    internal static extern void ZeroMemory(IntPtr destination, UIntPtr length);

    [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern int IntDeleteObject(HandleRef hObject);

    public static int DeleteObject(HandleRef hObject)
    {
      System.Internal.HandleCollector.Remove((IntPtr) hObject, SafeNativeMethods.CommonHandles.GDI);
      return SafeNativeMethods.IntDeleteObject(hObject);
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SelectObject(HandleRef hdc, HandleRef obj);

    [DllImport("user32.dll", EntryPoint = "CreateIconFromResourceEx", SetLastError = true)]
    private static extern unsafe IntPtr IntCreateIconFromResourceEx(
      byte* pbIconBits,
      int cbIconBits,
      bool fIcon,
      int dwVersion,
      int csDesired,
      int cyDesired,
      int flags);

    public static unsafe IntPtr CreateIconFromResourceEx(
      byte* pbIconBits,
      int cbIconBits,
      bool fIcon,
      int dwVersion,
      int csDesired,
      int cyDesired,
      int flags)
    {
      return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateIconFromResourceEx(pbIconBits, cbIconBits, fIcon, dwVersion, csDesired, cyDesired, flags), SafeNativeMethods.CommonHandles.Icon);
    }

    [DllImport("shell32.dll", EntryPoint = "ExtractAssociatedIcon", CharSet = CharSet.Auto, BestFitMapping = false)]
    public static extern IntPtr IntExtractAssociatedIcon(
      HandleRef hInst,
      StringBuilder iconPath,
      ref int index);

    public static IntPtr ExtractAssociatedIcon(
      HandleRef hInst,
      StringBuilder iconPath,
      ref int index)
    {
      return System.Internal.HandleCollector.Add(SafeNativeMethods.IntExtractAssociatedIcon(hInst, iconPath, ref index), SafeNativeMethods.CommonHandles.Icon);
    }

    [DllImport("user32.dll", EntryPoint = "LoadIcon", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntLoadIcon(HandleRef hInst, IntPtr iconId);

    public static IntPtr LoadIcon(HandleRef hInst, int iconId) => SafeNativeMethods.IntLoadIcon(hInst, new IntPtr(iconId));

    [DllImport("comctl32.dll", EntryPoint = "LoadIconWithScaleDown", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int IntLoadIconWithScaleDown(
      HandleRef hInst,
      IntPtr iconId,
      int cx,
      int cy,
      ref IntPtr phico);

    public static int LoadIconWithScaleDown(
      HandleRef hInst,
      int iconId,
      int cx,
      int cy,
      ref IntPtr phico)
    {
      return SafeNativeMethods.IntLoadIconWithScaleDown(hInst, new IntPtr(iconId), cx, cy, ref phico);
    }

    [DllImport("user32.dll", EntryPoint = "DestroyIcon", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool IntDestroyIcon(HandleRef hIcon);

    public static bool DestroyIcon(HandleRef hIcon)
    {
      System.Internal.HandleCollector.Remove((IntPtr) hIcon, SafeNativeMethods.CommonHandles.Icon);
      return SafeNativeMethods.IntDestroyIcon(hIcon);
    }

    [DllImport("user32.dll", EntryPoint = "CopyImage", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCopyImage(
      HandleRef hImage,
      int uType,
      int cxDesired,
      int cyDesired,
      int fuFlags);

    public static IntPtr CopyImage(
      HandleRef hImage,
      int uType,
      int cxDesired,
      int cyDesired,
      int fuFlags)
    {
      int type = uType != 1 ? SafeNativeMethods.CommonHandles.GDI : SafeNativeMethods.CommonHandles.Icon;
      return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), type);
    }

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] SafeNativeMethods.BITMAP bm);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] SafeNativeMethods.LOGFONT lf);

    public static int GetObject(HandleRef hObject, SafeNativeMethods.LOGFONT lp) => SafeNativeMethods.GetObject(hObject, Marshal.SizeOf(typeof (SafeNativeMethods.LOGFONT)), lp);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool GetIconInfo(HandleRef hIcon, [In, Out] SafeNativeMethods.ICONINFO info);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetSysColor(int nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool DrawIconEx(
      HandleRef hDC,
      int x,
      int y,
      HandleRef hIcon,
      int width,
      int height,
      int iStepIfAniCursor,
      HandleRef hBrushFlickerFree,
      int diFlags);

    [DllImport("oleaut32.dll", PreserveSig = false)]
    public static extern SafeNativeMethods.IPicture OleCreatePictureIndirect(
      SafeNativeMethods.PICTDESC pictdesc,
      [In] ref Guid refiid,
      bool fOwn);

    [SuppressUnmanagedCodeSecurity]
    internal class Gdip
    {
      private static readonly TraceSwitch GdiPlusInitialization = new TraceSwitch(nameof (GdiPlusInitialization), "Tracks GDI+ initialization and teardown");
      private static IntPtr initToken;
      private const string ThreadDataSlotName = "system.drawing.threaddata";
      internal const int Ok = 0;
      internal const int GenericError = 1;
      internal const int InvalidParameter = 2;
      internal const int OutOfMemory = 3;
      internal const int ObjectBusy = 4;
      internal const int InsufficientBuffer = 5;
      internal const int NotImplemented = 6;
      internal const int Win32Error = 7;
      internal const int WrongState = 8;
      internal const int Aborted = 9;
      internal const int FileNotFound = 10;
      internal const int ValueOverflow = 11;
      internal const int AccessDenied = 12;
      internal const int UnknownImageFormat = 13;
      internal const int FontFamilyNotFound = 14;
      internal const int FontStyleNotFound = 15;
      internal const int NotTrueTypeFont = 16;
      internal const int UnsupportedGdiplusVersion = 17;
      internal const int GdiplusNotInitialized = 18;
      internal const int PropertyNotFound = 19;
      internal const int PropertyNotSupported = 20;

      static Gdip() => SafeNativeMethods.Gdip.Initialize();

      private static bool Initialized => SafeNativeMethods.Gdip.initToken != IntPtr.Zero;

      internal static IDictionary ThreadData
      {
        get
        {
          LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("system.drawing.threaddata");
          IDictionary data = (IDictionary) Thread.GetData(namedDataSlot);
          if (data == null)
          {
            data = (IDictionary) new Hashtable();
            Thread.SetData(namedDataSlot, (object) data);
          }
          return data;
        }
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static void ClearThreadData() => Thread.SetData(Thread.GetNamedDataSlot("system.drawing.threaddata"), (object) null);

      private static void Initialize()
      {
        SafeNativeMethods.Gdip.StartupInput input = SafeNativeMethods.Gdip.StartupInput.GetDefault();
        int status = SafeNativeMethods.Gdip.GdiplusStartup(out SafeNativeMethods.Gdip.initToken, ref input, out SafeNativeMethods.Gdip.StartupOutput _);
        if (status != 0)
          throw SafeNativeMethods.Gdip.StatusException(status);
        AppDomain currentDomain = AppDomain.CurrentDomain;
        currentDomain.ProcessExit += new EventHandler(SafeNativeMethods.Gdip.OnProcessExit);
        if (currentDomain.IsDefaultAppDomain())
          return;
        currentDomain.DomainUnload += new EventHandler(SafeNativeMethods.Gdip.OnProcessExit);
      }

      private static void Shutdown()
      {
        if (!SafeNativeMethods.Gdip.Initialized)
          return;
        SafeNativeMethods.Gdip.ClearThreadData();
        AppDomain currentDomain = AppDomain.CurrentDomain;
        currentDomain.ProcessExit -= new EventHandler(SafeNativeMethods.Gdip.OnProcessExit);
        if (currentDomain.IsDefaultAppDomain())
          return;
        currentDomain.DomainUnload -= new EventHandler(SafeNativeMethods.Gdip.OnProcessExit);
      }

      [PrePrepareMethod]
      private static void OnProcessExit(object sender, EventArgs e) => SafeNativeMethods.Gdip.Shutdown();

      internal static void DummyFunction()
      {
      }

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int GdiplusStartup(
        out IntPtr token,
        ref SafeNativeMethods.Gdip.StartupInput input,
        out SafeNativeMethods.Gdip.StartupOutput output);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern void GdiplusShutdown(HandleRef token);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePath(int brushMode, out IntPtr path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePath2(
        HandleRef points,
        HandleRef types,
        int count,
        int brushMode,
        out IntPtr path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePath2I(
        HandleRef points,
        HandleRef types,
        int count,
        int brushMode,
        out IntPtr path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipClonePath(HandleRef path, out IntPtr clonepath);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeletePath", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeletePath(HandleRef path);

      internal static int GdipDeletePath(HandleRef path) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeletePath(path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipResetPath(HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPointCount(HandleRef path, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathTypes(HandleRef path, byte[] types, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathPoints(HandleRef path, HandleRef points, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathFillMode(HandleRef path, out int fillmode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathFillMode(HandleRef path, int fillmode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathData(HandleRef path, IntPtr pathData);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipStartPathFigure(HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipClosePathFigure(HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipClosePathFigures(HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathMarker(HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipClearPathMarkers(HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipReversePath(HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathLastPoint(HandleRef path, GPPOINTF lastPoint);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathLine(
        HandleRef path,
        float x1,
        float y1,
        float x2,
        float y2);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathLine2(HandleRef path, HandleRef memorypts, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathArc(
        HandleRef path,
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathBezier(
        HandleRef path,
        float x1,
        float y1,
        float x2,
        float y2,
        float x3,
        float y3,
        float x4,
        float y4);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathBeziers(HandleRef path, HandleRef memorypts, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathCurve(HandleRef path, HandleRef memorypts, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathCurve2(
        HandleRef path,
        HandleRef memorypts,
        int count,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathCurve3(
        HandleRef path,
        HandleRef memorypts,
        int count,
        int offset,
        int numberOfSegments,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathClosedCurve(
        HandleRef path,
        HandleRef memorypts,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathClosedCurve2(
        HandleRef path,
        HandleRef memorypts,
        int count,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathRectangle(
        HandleRef path,
        float x,
        float y,
        float width,
        float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathRectangles(HandleRef path, HandleRef rects, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathEllipse(
        HandleRef path,
        float x,
        float y,
        float width,
        float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathPie(
        HandleRef path,
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathPolygon(HandleRef path, HandleRef memorypts, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathPath(
        HandleRef path,
        HandleRef addingPath,
        bool connect);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathString(
        HandleRef path,
        string s,
        int length,
        HandleRef fontFamily,
        int style,
        float emSize,
        ref GPRECTF layoutRect,
        HandleRef format);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathStringI(
        HandleRef path,
        string s,
        int length,
        HandleRef fontFamily,
        int style,
        float emSize,
        ref GPRECT layoutRect,
        HandleRef format);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathLineI(HandleRef path, int x1, int y1, int x2, int y2);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathLine2I(HandleRef path, HandleRef memorypts, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathArcI(
        HandleRef path,
        int x,
        int y,
        int width,
        int height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathBezierI(
        HandleRef path,
        int x1,
        int y1,
        int x2,
        int y2,
        int x3,
        int y3,
        int x4,
        int y4);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathBeziersI(
        HandleRef path,
        HandleRef memorypts,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathCurveI(HandleRef path, HandleRef memorypts, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathCurve2I(
        HandleRef path,
        HandleRef memorypts,
        int count,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathCurve3I(
        HandleRef path,
        HandleRef memorypts,
        int count,
        int offset,
        int numberOfSegments,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathClosedCurveI(
        HandleRef path,
        HandleRef memorypts,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathClosedCurve2I(
        HandleRef path,
        HandleRef memorypts,
        int count,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathRectangleI(
        HandleRef path,
        int x,
        int y,
        int width,
        int height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathRectanglesI(HandleRef path, HandleRef rects, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathEllipseI(
        HandleRef path,
        int x,
        int y,
        int width,
        int height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathPieI(
        HandleRef path,
        int x,
        int y,
        int width,
        int height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipAddPathPolygonI(
        HandleRef path,
        HandleRef memorypts,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFlattenPath(
        HandleRef path,
        HandleRef matrixfloat,
        float flatness);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipWidenPath(
        HandleRef path,
        HandleRef pen,
        HandleRef matrix,
        float flatness);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipWarpPath(
        HandleRef path,
        HandleRef matrix,
        HandleRef points,
        int count,
        float srcX,
        float srcY,
        float srcWidth,
        float srcHeight,
        WarpMode warpMode,
        float flatness);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTransformPath(HandleRef path, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathWorldBounds(
        HandleRef path,
        ref GPRECTF gprectf,
        HandleRef matrix,
        HandleRef pen);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisiblePathPoint(
        HandleRef path,
        float x,
        float y,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisiblePathPointI(
        HandleRef path,
        int x,
        int y,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsOutlineVisiblePathPoint(
        HandleRef path,
        float x,
        float y,
        HandleRef pen,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsOutlineVisiblePathPointI(
        HandleRef path,
        int x,
        int y,
        HandleRef pen,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePathIter(out IntPtr pathIter, HandleRef path);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeletePathIter", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeletePathIter(HandleRef pathIter);

      internal static int GdipDeletePathIter(HandleRef pathIter) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeletePathIter(pathIter);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterNextSubpath(
        HandleRef pathIter,
        out int resultCount,
        out int startIndex,
        out int endIndex,
        out bool isClosed);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterNextSubpathPath(
        HandleRef pathIter,
        out int resultCount,
        HandleRef path,
        out bool isClosed);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterNextPathType(
        HandleRef pathIter,
        out int resultCount,
        out byte pathType,
        out int startIndex,
        out int endIndex);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterNextMarker(
        HandleRef pathIter,
        out int resultCount,
        out int startIndex,
        out int endIndex);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterNextMarkerPath(
        HandleRef pathIter,
        out int resultCount,
        HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterGetCount(HandleRef pathIter, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterGetSubpathCount(HandleRef pathIter, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterHasCurve(HandleRef pathIter, out bool hasCurve);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterRewind(HandleRef pathIter);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterEnumerate(
        HandleRef pathIter,
        out int resultCount,
        IntPtr memoryPts,
        [In, Out] byte[] types,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPathIterCopyData(
        HandleRef pathIter,
        out int resultCount,
        IntPtr memoryPts,
        [In, Out] byte[] types,
        int startIndex,
        int endIndex);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateMatrix(out IntPtr matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateMatrix2(
        float m11,
        float m12,
        float m21,
        float m22,
        float dx,
        float dy,
        out IntPtr matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateMatrix3(
        ref GPRECTF rect,
        HandleRef dstplg,
        out IntPtr matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateMatrix3I(
        ref GPRECT rect,
        HandleRef dstplg,
        out IntPtr matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneMatrix(HandleRef matrix, out IntPtr cloneMatrix);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeleteMatrix", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeleteMatrix(HandleRef matrix);

      internal static int GdipDeleteMatrix(HandleRef matrix) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeleteMatrix(matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetMatrixElements(
        HandleRef matrix,
        float m11,
        float m12,
        float m21,
        float m22,
        float dx,
        float dy);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipMultiplyMatrix(
        HandleRef matrix,
        HandleRef matrix2,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslateMatrix(
        HandleRef matrix,
        float offsetX,
        float offsetY,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipScaleMatrix(
        HandleRef matrix,
        float scaleX,
        float scaleY,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRotateMatrix(HandleRef matrix, float angle, MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipShearMatrix(
        HandleRef matrix,
        float shearX,
        float shearY,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipInvertMatrix(HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTransformMatrixPoints(
        HandleRef matrix,
        HandleRef pts,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTransformMatrixPointsI(
        HandleRef matrix,
        HandleRef pts,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipVectorTransformMatrixPoints(
        HandleRef matrix,
        HandleRef pts,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipVectorTransformMatrixPointsI(
        HandleRef matrix,
        HandleRef pts,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetMatrixElements(HandleRef matrix, IntPtr m);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsMatrixInvertible(HandleRef matrix, out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsMatrixIdentity(HandleRef matrix, out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsMatrixEqual(
        HandleRef matrix,
        HandleRef matrix2,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateRegion(out IntPtr region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateRegionRect(ref GPRECTF gprectf, out IntPtr region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateRegionRectI(ref GPRECT gprect, out IntPtr region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateRegionPath(HandleRef path, out IntPtr region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateRegionRgnData(
        byte[] rgndata,
        int size,
        out IntPtr region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateRegionHrgn(HandleRef hRgn, out IntPtr region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneRegion(HandleRef region, out IntPtr cloneregion);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeleteRegion", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeleteRegion(HandleRef region);

      internal static int GdipDeleteRegion(HandleRef region) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeleteRegion(region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetInfinite(HandleRef region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetEmpty(HandleRef region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCombineRegionRect(
        HandleRef region,
        ref GPRECTF gprectf,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCombineRegionRectI(
        HandleRef region,
        ref GPRECT gprect,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCombineRegionPath(
        HandleRef region,
        HandleRef path,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCombineRegionRegion(
        HandleRef region,
        HandleRef region2,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslateRegion(HandleRef region, float dx, float dy);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslateRegionI(HandleRef region, int dx, int dy);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTransformRegion(HandleRef region, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetRegionBounds(
        HandleRef region,
        HandleRef graphics,
        ref GPRECTF gprectf);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetRegionHRgn(
        HandleRef region,
        HandleRef graphics,
        out IntPtr hrgn);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsEmptyRegion(
        HandleRef region,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsInfiniteRegion(
        HandleRef region,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsEqualRegion(
        HandleRef region,
        HandleRef region2,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetRegionDataSize(HandleRef region, out int bufferSize);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetRegionData(
        HandleRef region,
        byte[] regionData,
        int bufferSize,
        out int sizeFilled);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisibleRegionPoint(
        HandleRef region,
        float X,
        float Y,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisibleRegionPointI(
        HandleRef region,
        int X,
        int Y,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisibleRegionRect(
        HandleRef region,
        float X,
        float Y,
        float width,
        float height,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisibleRegionRectI(
        HandleRef region,
        int X,
        int Y,
        int width,
        int height,
        HandleRef graphics,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetRegionScansCount(
        HandleRef region,
        out int count,
        HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetRegionScans(
        HandleRef region,
        IntPtr rects,
        out int count,
        HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneBrush(HandleRef brush, out IntPtr clonebrush);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeleteBrush", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeleteBrush(HandleRef brush);

      internal static int GdipDeleteBrush(HandleRef brush) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeleteBrush(brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateHatchBrush(
        int hatchstyle,
        int forecol,
        int backcol,
        out IntPtr brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetHatchStyle(HandleRef brush, out int hatchstyle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetHatchForegroundColor(HandleRef brush, out int forecol);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetHatchBackgroundColor(HandleRef brush, out int backcol);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateTexture(
        HandleRef bitmap,
        int wrapmode,
        out IntPtr texture);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateTexture2(
        HandleRef bitmap,
        int wrapmode,
        float x,
        float y,
        float width,
        float height,
        out IntPtr texture);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateTextureIA(
        HandleRef bitmap,
        HandleRef imageAttrib,
        float x,
        float y,
        float width,
        float height,
        out IntPtr texture);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateTexture2I(
        HandleRef bitmap,
        int wrapmode,
        int x,
        int y,
        int width,
        int height,
        out IntPtr texture);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateTextureIAI(
        HandleRef bitmap,
        HandleRef imageAttrib,
        int x,
        int y,
        int width,
        int height,
        out IntPtr texture);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetTextureTransform(HandleRef brush, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetTextureTransform(HandleRef brush, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipResetTextureTransform(HandleRef brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipMultiplyTextureTransform(
        HandleRef brush,
        HandleRef matrix,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslateTextureTransform(
        HandleRef brush,
        float dx,
        float dy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipScaleTextureTransform(
        HandleRef brush,
        float sx,
        float sy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRotateTextureTransform(
        HandleRef brush,
        float angle,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetTextureWrapMode(HandleRef brush, int wrapMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetTextureWrapMode(HandleRef brush, out int wrapMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetTextureImage(HandleRef brush, out IntPtr image);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateSolidFill(int color, out IntPtr brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetSolidFillColor(HandleRef brush, int color);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetSolidFillColor(HandleRef brush, out int color);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateLineBrush(
        GPPOINTF point1,
        GPPOINTF point2,
        int color1,
        int color2,
        int wrapMode,
        out IntPtr lineGradient);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateLineBrushI(
        GPPOINT point1,
        GPPOINT point2,
        int color1,
        int color2,
        int wrapMode,
        out IntPtr lineGradient);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateLineBrushFromRect(
        ref GPRECTF rect,
        int color1,
        int color2,
        int lineGradientMode,
        int wrapMode,
        out IntPtr lineGradient);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateLineBrushFromRectI(
        ref GPRECT rect,
        int color1,
        int color2,
        int lineGradientMode,
        int wrapMode,
        out IntPtr lineGradient);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateLineBrushFromRectWithAngle(
        ref GPRECTF rect,
        int color1,
        int color2,
        float angle,
        bool isAngleScaleable,
        int wrapMode,
        out IntPtr lineGradient);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateLineBrushFromRectWithAngleI(
        ref GPRECT rect,
        int color1,
        int color2,
        float angle,
        bool isAngleScaleable,
        int wrapMode,
        out IntPtr lineGradient);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetLineColors(HandleRef brush, int color1, int color2);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLineColors(HandleRef brush, int[] colors);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLineRect(HandleRef brush, ref GPRECTF gprectf);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLineGammaCorrection(
        HandleRef brush,
        out bool useGammaCorrection);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetLineGammaCorrection(
        HandleRef brush,
        bool useGammaCorrection);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetLineSigmaBlend(HandleRef brush, float focus, float scale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetLineLinearBlend(HandleRef brush, float focus, float scale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLineBlendCount(HandleRef brush, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLineBlend(
        HandleRef brush,
        IntPtr blend,
        IntPtr positions,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetLineBlend(
        HandleRef brush,
        HandleRef blend,
        HandleRef positions,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLinePresetBlendCount(HandleRef brush, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLinePresetBlend(
        HandleRef brush,
        IntPtr blend,
        IntPtr positions,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetLinePresetBlend(
        HandleRef brush,
        HandleRef blend,
        HandleRef positions,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetLineWrapMode(HandleRef brush, int wrapMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLineWrapMode(HandleRef brush, out int wrapMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipResetLineTransform(HandleRef brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipMultiplyLineTransform(
        HandleRef brush,
        HandleRef matrix,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLineTransform(HandleRef brush, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetLineTransform(HandleRef brush, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslateLineTransform(
        HandleRef brush,
        float dx,
        float dy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipScaleLineTransform(
        HandleRef brush,
        float sx,
        float sy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRotateLineTransform(
        HandleRef brush,
        float angle,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePathGradient(
        HandleRef points,
        int count,
        int wrapMode,
        out IntPtr brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePathGradientI(
        HandleRef points,
        int count,
        int wrapMode,
        out IntPtr brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePathGradientFromPath(HandleRef path, out IntPtr brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientCenterColor(HandleRef brush, out int color);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientCenterColor(HandleRef brush, int color);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientSurroundColorsWithCount(
        HandleRef brush,
        int[] color,
        ref int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientSurroundColorsWithCount(
        HandleRef brush,
        int[] argb,
        ref int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientCenterPoint(HandleRef brush, GPPOINTF point);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientCenterPoint(HandleRef brush, GPPOINTF point);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientRect(HandleRef brush, ref GPRECTF gprectf);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientPointCount(HandleRef brush, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientSurroundColorCount(
        HandleRef brush,
        out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientBlendCount(HandleRef brush, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientBlend(
        HandleRef brush,
        IntPtr blend,
        IntPtr positions,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientBlend(
        HandleRef brush,
        HandleRef blend,
        HandleRef positions,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientPresetBlendCount(HandleRef brush, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientPresetBlend(
        HandleRef brush,
        IntPtr blend,
        IntPtr positions,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientPresetBlend(
        HandleRef brush,
        HandleRef blend,
        HandleRef positions,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientSigmaBlend(
        HandleRef brush,
        float focus,
        float scale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientLinearBlend(
        HandleRef brush,
        float focus,
        float scale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientWrapMode(HandleRef brush, int wrapmode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientWrapMode(HandleRef brush, out int wrapmode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientTransform(HandleRef brush, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientTransform(HandleRef brush, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipResetPathGradientTransform(HandleRef brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipMultiplyPathGradientTransform(
        HandleRef brush,
        HandleRef matrix,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslatePathGradientTransform(
        HandleRef brush,
        float dx,
        float dy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipScalePathGradientTransform(
        HandleRef brush,
        float sx,
        float sy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRotatePathGradientTransform(
        HandleRef brush,
        float angle,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPathGradientFocusScales(
        HandleRef brush,
        float[] xScale,
        float[] yScale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPathGradientFocusScales(
        HandleRef brush,
        float xScale,
        float yScale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePen1(int argb, float width, int unit, out IntPtr pen);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreatePen2(
        HandleRef brush,
        float width,
        int unit,
        out IntPtr pen);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipClonePen(HandleRef pen, out IntPtr clonepen);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeletePen", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeletePen(HandleRef Pen);

      internal static int GdipDeletePen(HandleRef pen) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeletePen(pen);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenMode(HandleRef pen, PenAlignment penAlign);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenMode(HandleRef pen, out PenAlignment penAlign);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenWidth(HandleRef pen, float width);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenWidth(HandleRef pen, float[] width);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenLineCap197819(
        HandleRef pen,
        int startCap,
        int endCap,
        int dashCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenStartCap(HandleRef pen, int startCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenEndCap(HandleRef pen, int endCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenStartCap(HandleRef pen, out int startCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenEndCap(HandleRef pen, out int endCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenDashCap197819(HandleRef pen, out int dashCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenDashCap197819(HandleRef pen, int dashCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenLineJoin(HandleRef pen, int lineJoin);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenLineJoin(HandleRef pen, out int lineJoin);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenCustomStartCap(HandleRef pen, HandleRef customCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenCustomStartCap(HandleRef pen, out IntPtr customCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenCustomEndCap(HandleRef pen, HandleRef customCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenCustomEndCap(HandleRef pen, out IntPtr customCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenMiterLimit(HandleRef pen, float miterLimit);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenMiterLimit(HandleRef pen, float[] miterLimit);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenTransform(HandleRef pen, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenTransform(HandleRef pen, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipResetPenTransform(HandleRef brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipMultiplyPenTransform(
        HandleRef brush,
        HandleRef matrix,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslatePenTransform(
        HandleRef brush,
        float dx,
        float dy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipScalePenTransform(
        HandleRef brush,
        float sx,
        float sy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRotatePenTransform(
        HandleRef brush,
        float angle,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenColor(HandleRef pen, int argb);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenColor(HandleRef pen, out int argb);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenBrushFill(HandleRef pen, HandleRef brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenBrushFill(HandleRef pen, out IntPtr brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenFillType(HandleRef pen, out int pentype);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenDashStyle(HandleRef pen, out int dashstyle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenDashStyle(HandleRef pen, int dashstyle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenDashArray(
        HandleRef pen,
        HandleRef memorydash,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenDashOffset(HandleRef pen, float[] dashoffset);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenDashOffset(HandleRef pen, float dashoffset);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenDashCount(HandleRef pen, out int dashcount);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenDashArray(HandleRef pen, IntPtr memorydash, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenCompoundCount(HandleRef pen, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPenCompoundArray(HandleRef pen, float[] array, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPenCompoundArray(HandleRef pen, float[] array, int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateCustomLineCap(
        HandleRef fillpath,
        HandleRef strokepath,
        LineCap baseCap,
        float baseInset,
        out IntPtr customCap);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeleteCustomLineCap", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeleteCustomLineCap(HandleRef customCap);

      internal static int GdipDeleteCustomLineCap(HandleRef customCap) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeleteCustomLineCap(customCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneCustomLineCap(HandleRef customCap, out IntPtr clonedCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCustomLineCapType(
        HandleRef customCap,
        out CustomLineCapType capType);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetCustomLineCapStrokeCaps(
        HandleRef customCap,
        LineCap startCap,
        LineCap endCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCustomLineCapStrokeCaps(
        HandleRef customCap,
        out LineCap startCap,
        out LineCap endCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetCustomLineCapStrokeJoin(
        HandleRef customCap,
        LineJoin lineJoin);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCustomLineCapStrokeJoin(
        HandleRef customCap,
        out LineJoin lineJoin);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetCustomLineCapBaseCap(HandleRef customCap, LineCap baseCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCustomLineCapBaseCap(
        HandleRef customCap,
        out LineCap baseCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetCustomLineCapBaseInset(HandleRef customCap, float inset);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCustomLineCapBaseInset(HandleRef customCap, out float inset);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetCustomLineCapWidthScale(
        HandleRef customCap,
        float widthScale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCustomLineCapWidthScale(
        HandleRef customCap,
        out float widthScale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateAdjustableArrowCap(
        float height,
        float width,
        bool isFilled,
        out IntPtr adjustableArrowCap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetAdjustableArrowCapHeight(
        HandleRef adjustableArrowCap,
        float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetAdjustableArrowCapHeight(
        HandleRef adjustableArrowCap,
        out float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetAdjustableArrowCapWidth(
        HandleRef adjustableArrowCap,
        float width);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetAdjustableArrowCapWidth(
        HandleRef adjustableArrowCap,
        out float width);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetAdjustableArrowCapMiddleInset(
        HandleRef adjustableArrowCap,
        float middleInset);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetAdjustableArrowCapMiddleInset(
        HandleRef adjustableArrowCap,
        out float middleInset);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetAdjustableArrowCapFillState(
        HandleRef adjustableArrowCap,
        bool fillState);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetAdjustableArrowCapFillState(
        HandleRef adjustableArrowCap,
        out bool fillState);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipLoadImageFromStream(
        UnsafeNativeMethods.IStream stream,
        out IntPtr image);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipLoadImageFromFile(string filename, out IntPtr image);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipLoadImageFromStreamICM(
        UnsafeNativeMethods.IStream stream,
        out IntPtr image);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipLoadImageFromFileICM(string filename, out IntPtr image);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneImage(HandleRef image, out IntPtr cloneimage);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDisposeImage", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDisposeImage(HandleRef image);

      internal static int GdipDisposeImage(HandleRef image) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDisposeImage(image);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSaveImageToFile(
        HandleRef image,
        string filename,
        ref Guid classId,
        HandleRef encoderParams);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSaveImageToStream(
        HandleRef image,
        UnsafeNativeMethods.IStream stream,
        ref Guid classId,
        HandleRef encoderParams);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSaveAdd(HandleRef image, HandleRef encoderParams);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSaveAddImage(
        HandleRef image,
        HandleRef newImage,
        HandleRef encoderParams);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageGraphicsContext(HandleRef image, out IntPtr graphics);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageBounds(
        HandleRef image,
        ref GPRECTF gprectf,
        out GraphicsUnit unit);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageDimension(
        HandleRef image,
        out float width,
        out float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageType(HandleRef image, out int type);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageWidth(HandleRef image, out int width);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageHeight(HandleRef image, out int height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageHorizontalResolution(
        HandleRef image,
        out float horzRes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageVerticalResolution(HandleRef image, out float vertRes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageFlags(HandleRef image, out int flags);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageRawFormat(HandleRef image, ref Guid format);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImagePixelFormat(HandleRef image, out int format);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageThumbnail(
        HandleRef image,
        int thumbWidth,
        int thumbHeight,
        out IntPtr thumbImage,
        Image.GetThumbnailImageAbort callback,
        IntPtr callbackdata);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetEncoderParameterListSize(
        HandleRef image,
        ref Guid clsid,
        out int size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetEncoderParameterList(
        HandleRef image,
        ref Guid clsid,
        int size,
        IntPtr buffer);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipImageGetFrameDimensionsCount(HandleRef image, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipImageGetFrameDimensionsList(
        HandleRef image,
        IntPtr buffer,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipImageGetFrameCount(
        HandleRef image,
        ref Guid dimensionID,
        int[] count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipImageSelectActiveFrame(
        HandleRef image,
        ref Guid dimensionID,
        int frameIndex);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipImageRotateFlip(HandleRef image, int rotateFlipType);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImagePalette(HandleRef image, IntPtr palette, int size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImagePalette(HandleRef image, IntPtr palette);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImagePaletteSize(HandleRef image, out int size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPropertyCount(HandleRef image, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPropertyIdList(HandleRef image, int count, int[] list);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPropertyItemSize(HandleRef image, int propid, out int size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPropertyItem(
        HandleRef image,
        int propid,
        int size,
        IntPtr buffer);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPropertySize(
        HandleRef image,
        out int totalSize,
        ref int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetAllPropertyItems(
        HandleRef image,
        int totalSize,
        int count,
        IntPtr buffer);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRemovePropertyItem(HandleRef image, int propid);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPropertyItem(HandleRef image, PropertyItemInternal propitem);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipImageForceValidation(HandleRef image);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageDecodersSize(out int numDecoders, out int size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageDecoders(int numDecoders, int size, IntPtr decoders);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageEncodersSize(out int numEncoders, out int size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageEncoders(int numEncoders, int size, IntPtr encoders);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromStream(
        UnsafeNativeMethods.IStream stream,
        out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromFile(string filename, out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromStreamICM(
        UnsafeNativeMethods.IStream stream,
        out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromFileICM(string filename, out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromScan0(
        int width,
        int height,
        int stride,
        int format,
        HandleRef scan0,
        out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromGraphics(
        int width,
        int height,
        HandleRef graphics,
        out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromHBITMAP(
        HandleRef hbitmap,
        HandleRef hpalette,
        out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromHICON(HandleRef hicon, out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateBitmapFromResource(
        HandleRef hresource,
        HandleRef name,
        out IntPtr bitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateHBITMAPFromBitmap(
        HandleRef nativeBitmap,
        out IntPtr hbitmap,
        int argbBackground);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateHICONFromBitmap(HandleRef nativeBitmap, out IntPtr hicon);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneBitmapArea(
        float x,
        float y,
        float width,
        float height,
        int format,
        HandleRef srcbitmap,
        out IntPtr dstbitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneBitmapAreaI(
        int x,
        int y,
        int width,
        int height,
        int format,
        HandleRef srcbitmap,
        out IntPtr dstbitmap);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipBitmapLockBits(
        HandleRef bitmap,
        ref GPRECT rect,
        ImageLockMode flags,
        PixelFormat format,
        [In, Out] BitmapData lockedBitmapData);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipBitmapUnlockBits(HandleRef bitmap, BitmapData lockedBitmapData);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipBitmapGetPixel(HandleRef bitmap, int x, int y, out int argb);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipBitmapSetPixel(HandleRef bitmap, int x, int y, int argb);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipBitmapSetResolution(HandleRef bitmap, float dpix, float dpiy);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateImageAttributes(out IntPtr imageattr);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneImageAttributes(
        HandleRef imageattr,
        out IntPtr cloneImageattr);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDisposeImageAttributes", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDisposeImageAttributes(HandleRef imageattr);

      internal static int GdipDisposeImageAttributes(HandleRef imageattr) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDisposeImageAttributes(imageattr);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesColorMatrix(
        HandleRef imageattr,
        ColorAdjustType type,
        bool enableFlag,
        ColorMatrix colorMatrix,
        ColorMatrix grayMatrix,
        ColorMatrixFlag flags);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesThreshold(
        HandleRef imageattr,
        ColorAdjustType type,
        bool enableFlag,
        float threshold);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesGamma(
        HandleRef imageattr,
        ColorAdjustType type,
        bool enableFlag,
        float gamma);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesNoOp(
        HandleRef imageattr,
        ColorAdjustType type,
        bool enableFlag);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesColorKeys(
        HandleRef imageattr,
        ColorAdjustType type,
        bool enableFlag,
        int colorLow,
        int colorHigh);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesOutputChannel(
        HandleRef imageattr,
        ColorAdjustType type,
        bool enableFlag,
        ColorChannelFlag flags);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesOutputChannelColorProfile(
        HandleRef imageattr,
        ColorAdjustType type,
        bool enableFlag,
        string colorProfileFilename);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesRemapTable(
        HandleRef imageattr,
        ColorAdjustType type,
        bool enableFlag,
        int mapSize,
        HandleRef map);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetImageAttributesWrapMode(
        HandleRef imageattr,
        int wrapmode,
        int argb,
        bool clamp);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetImageAttributesAdjustedPalette(
        HandleRef imageattr,
        HandleRef palette,
        ColorAdjustType type);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFlush(HandleRef graphics, FlushIntention intention);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateFromHDC(HandleRef hdc, out IntPtr graphics);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateFromHDC2(
        HandleRef hdc,
        HandleRef hdevice,
        out IntPtr graphics);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateFromHWND(HandleRef hwnd, out IntPtr graphics);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeleteGraphics", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeleteGraphics(HandleRef graphics);

      internal static int GdipDeleteGraphics(HandleRef graphics) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeleteGraphics(graphics);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetDC(HandleRef graphics, out IntPtr hdc);

      [DllImport("gdiplus.dll", EntryPoint = "GdipReleaseDC", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipReleaseDC(HandleRef graphics, HandleRef hdc);

      internal static int GdipReleaseDC(HandleRef graphics, HandleRef hdc) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipReleaseDC(graphics, hdc);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetCompositingMode(HandleRef graphics, int compositeMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetTextRenderingHint(
        HandleRef graphics,
        TextRenderingHint textRenderingHint);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetTextContrast(HandleRef graphics, int textContrast);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetInterpolationMode(HandleRef graphics, int mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCompositingMode(HandleRef graphics, out int compositeMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetRenderingOrigin(HandleRef graphics, int x, int y);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetRenderingOrigin(HandleRef graphics, out int x, out int y);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetCompositingQuality(
        HandleRef graphics,
        CompositingQuality quality);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCompositingQuality(
        HandleRef graphics,
        out CompositingQuality quality);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetSmoothingMode(
        HandleRef graphics,
        SmoothingMode smoothingMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetSmoothingMode(
        HandleRef graphics,
        out SmoothingMode smoothingMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPixelOffsetMode(
        HandleRef graphics,
        PixelOffsetMode pixelOffsetMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPixelOffsetMode(
        HandleRef graphics,
        out PixelOffsetMode pixelOffsetMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetTextRenderingHint(
        HandleRef graphics,
        out TextRenderingHint textRenderingHint);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetTextContrast(HandleRef graphics, out int textContrast);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetInterpolationMode(HandleRef graphics, out int mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetWorldTransform(HandleRef graphics, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipResetWorldTransform(HandleRef graphics);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipMultiplyWorldTransform(
        HandleRef graphics,
        HandleRef matrix,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslateWorldTransform(
        HandleRef graphics,
        float dx,
        float dy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipScaleWorldTransform(
        HandleRef graphics,
        float sx,
        float sy,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRotateWorldTransform(
        HandleRef graphics,
        float angle,
        MatrixOrder order);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetWorldTransform(HandleRef graphics, HandleRef matrix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPageUnit(HandleRef graphics, out int unit);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetPageScale(HandleRef graphics, float[] scale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPageUnit(HandleRef graphics, int unit);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetPageScale(HandleRef graphics, float scale);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetDpiX(HandleRef graphics, float[] dpi);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetDpiY(HandleRef graphics, float[] dpi);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTransformPoints(
        HandleRef graphics,
        int destSpace,
        int srcSpace,
        IntPtr points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTransformPointsI(
        HandleRef graphics,
        int destSpace,
        int srcSpace,
        IntPtr points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetNearestColor(HandleRef graphics, ref int color);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern IntPtr GdipCreateHalftonePalette();

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawLine(
        HandleRef graphics,
        HandleRef pen,
        float x1,
        float y1,
        float x2,
        float y2);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawLineI(
        HandleRef graphics,
        HandleRef pen,
        int x1,
        int y1,
        int x2,
        int y2);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawLines(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawLinesI(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawArc(
        HandleRef graphics,
        HandleRef pen,
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawArcI(
        HandleRef graphics,
        HandleRef pen,
        int x,
        int y,
        int width,
        int height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawBezier(
        HandleRef graphics,
        HandleRef pen,
        float x1,
        float y1,
        float x2,
        float y2,
        float x3,
        float y3,
        float x4,
        float y4);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawBeziers(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawBeziersI(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawRectangle(
        HandleRef graphics,
        HandleRef pen,
        float x,
        float y,
        float width,
        float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawRectangleI(
        HandleRef graphics,
        HandleRef pen,
        int x,
        int y,
        int width,
        int height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawRectangles(
        HandleRef graphics,
        HandleRef pen,
        HandleRef rects,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawRectanglesI(
        HandleRef graphics,
        HandleRef pen,
        HandleRef rects,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawEllipse(
        HandleRef graphics,
        HandleRef pen,
        float x,
        float y,
        float width,
        float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawEllipseI(
        HandleRef graphics,
        HandleRef pen,
        int x,
        int y,
        int width,
        int height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawPie(
        HandleRef graphics,
        HandleRef pen,
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawPieI(
        HandleRef graphics,
        HandleRef pen,
        int x,
        int y,
        int width,
        int height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawPolygon(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawPolygonI(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawPath(HandleRef graphics, HandleRef pen, HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawCurve(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawCurveI(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawCurve2(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawCurve2I(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawCurve3(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count,
        int offset,
        int numberOfSegments,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawCurve3I(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count,
        int offset,
        int numberOfSegments,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawClosedCurve(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawClosedCurveI(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawClosedCurve2(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawClosedCurve2I(
        HandleRef graphics,
        HandleRef pen,
        HandleRef points,
        int count,
        float tension);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGraphicsClear(HandleRef graphics, int argb);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillRectangle(
        HandleRef graphics,
        HandleRef brush,
        float x,
        float y,
        float width,
        float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillRectangleI(
        HandleRef graphics,
        HandleRef brush,
        int x,
        int y,
        int width,
        int height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillRectangles(
        HandleRef graphics,
        HandleRef brush,
        HandleRef rects,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillRectanglesI(
        HandleRef graphics,
        HandleRef brush,
        HandleRef rects,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillPolygon(
        HandleRef graphics,
        HandleRef brush,
        HandleRef points,
        int count,
        int brushMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillPolygonI(
        HandleRef graphics,
        HandleRef brush,
        HandleRef points,
        int count,
        int brushMode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillEllipse(
        HandleRef graphics,
        HandleRef brush,
        float x,
        float y,
        float width,
        float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillEllipseI(
        HandleRef graphics,
        HandleRef brush,
        int x,
        int y,
        int width,
        int height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillPie(
        HandleRef graphics,
        HandleRef brush,
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillPieI(
        HandleRef graphics,
        HandleRef brush,
        int x,
        int y,
        int width,
        int height,
        float startAngle,
        float sweepAngle);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillPath(HandleRef graphics, HandleRef brush, HandleRef path);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillClosedCurve(
        HandleRef graphics,
        HandleRef brush,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillClosedCurveI(
        HandleRef graphics,
        HandleRef brush,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillClosedCurve2(
        HandleRef graphics,
        HandleRef brush,
        HandleRef points,
        int count,
        float tension,
        int mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillClosedCurve2I(
        HandleRef graphics,
        HandleRef brush,
        HandleRef points,
        int count,
        float tension,
        int mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipFillRegion(
        HandleRef graphics,
        HandleRef brush,
        HandleRef region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImage(
        HandleRef graphics,
        HandleRef image,
        float x,
        float y);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImageI(HandleRef graphics, HandleRef image, int x, int y);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImageRect(
        HandleRef graphics,
        HandleRef image,
        float x,
        float y,
        float width,
        float height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImageRectI(
        HandleRef graphics,
        HandleRef image,
        int x,
        int y,
        int width,
        int height);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImagePoints(
        HandleRef graphics,
        HandleRef image,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImagePointsI(
        HandleRef graphics,
        HandleRef image,
        HandleRef points,
        int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImagePointRect(
        HandleRef graphics,
        HandleRef image,
        float x,
        float y,
        float srcx,
        float srcy,
        float srcwidth,
        float srcheight,
        int srcunit);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImagePointRectI(
        HandleRef graphics,
        HandleRef image,
        int x,
        int y,
        int srcx,
        int srcy,
        int srcwidth,
        int srcheight,
        int srcunit);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImageRectRect(
        HandleRef graphics,
        HandleRef image,
        float dstx,
        float dsty,
        float dstwidth,
        float dstheight,
        float srcx,
        float srcy,
        float srcwidth,
        float srcheight,
        int srcunit,
        HandleRef imageAttributes,
        Graphics.DrawImageAbort callback,
        HandleRef callbackdata);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImageRectRectI(
        HandleRef graphics,
        HandleRef image,
        int dstx,
        int dsty,
        int dstwidth,
        int dstheight,
        int srcx,
        int srcy,
        int srcwidth,
        int srcheight,
        int srcunit,
        HandleRef imageAttributes,
        Graphics.DrawImageAbort callback,
        HandleRef callbackdata);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImagePointsRect(
        HandleRef graphics,
        HandleRef image,
        HandleRef points,
        int count,
        float srcx,
        float srcy,
        float srcwidth,
        float srcheight,
        int srcunit,
        HandleRef imageAttributes,
        Graphics.DrawImageAbort callback,
        HandleRef callbackdata);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawImagePointsRectI(
        HandleRef graphics,
        HandleRef image,
        HandleRef points,
        int count,
        int srcx,
        int srcy,
        int srcwidth,
        int srcheight,
        int srcunit,
        HandleRef imageAttributes,
        Graphics.DrawImageAbort callback,
        HandleRef callbackdata);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileDestPoint(
        HandleRef graphics,
        HandleRef metafile,
        GPPOINTF destPoint,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileDestPointI(
        HandleRef graphics,
        HandleRef metafile,
        GPPOINT destPoint,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileDestRect(
        HandleRef graphics,
        HandleRef metafile,
        ref GPRECTF destRect,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileDestRectI(
        HandleRef graphics,
        HandleRef metafile,
        ref GPRECT destRect,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileDestPoints(
        HandleRef graphics,
        HandleRef metafile,
        IntPtr destPoints,
        int count,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileDestPointsI(
        HandleRef graphics,
        HandleRef metafile,
        IntPtr destPoints,
        int count,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileSrcRectDestPoint(
        HandleRef graphics,
        HandleRef metafile,
        GPPOINTF destPoint,
        ref GPRECTF srcRect,
        int pageUnit,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileSrcRectDestPointI(
        HandleRef graphics,
        HandleRef metafile,
        GPPOINT destPoint,
        ref GPRECT srcRect,
        int pageUnit,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileSrcRectDestRect(
        HandleRef graphics,
        HandleRef metafile,
        ref GPRECTF destRect,
        ref GPRECTF srcRect,
        int pageUnit,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileSrcRectDestRectI(
        HandleRef graphics,
        HandleRef metafile,
        ref GPRECT destRect,
        ref GPRECT srcRect,
        int pageUnit,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileSrcRectDestPoints(
        HandleRef graphics,
        HandleRef metafile,
        IntPtr destPoints,
        int count,
        ref GPRECTF srcRect,
        int pageUnit,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEnumerateMetafileSrcRectDestPointsI(
        HandleRef graphics,
        HandleRef metafile,
        IntPtr destPoints,
        int count,
        ref GPRECT srcRect,
        int pageUnit,
        Graphics.EnumerateMetafileProc callback,
        HandleRef callbackdata,
        HandleRef imageattributes);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPlayMetafileRecord(
        HandleRef graphics,
        EmfPlusRecordType recordType,
        int flags,
        int dataSize,
        byte[] data);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetClipGraphics(
        HandleRef graphics,
        HandleRef srcgraphics,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetClipRect(
        HandleRef graphics,
        float x,
        float y,
        float width,
        float height,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetClipRectI(
        HandleRef graphics,
        int x,
        int y,
        int width,
        int height,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetClipPath(
        HandleRef graphics,
        HandleRef path,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetClipRegion(
        HandleRef graphics,
        HandleRef region,
        CombineMode mode);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipResetClip(HandleRef graphics);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipTranslateClip(HandleRef graphics, float dx, float dy);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetClip(HandleRef graphics, HandleRef region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetClipBounds(HandleRef graphics, ref GPRECTF rect);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsClipEmpty(HandleRef graphics, out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetVisibleClipBounds(HandleRef graphics, ref GPRECTF rect);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisibleClipEmpty(HandleRef graphics, out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisiblePoint(
        HandleRef graphics,
        float x,
        float y,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisiblePointI(
        HandleRef graphics,
        int x,
        int y,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisibleRect(
        HandleRef graphics,
        float x,
        float y,
        float width,
        float height,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsVisibleRectI(
        HandleRef graphics,
        int x,
        int y,
        int width,
        int height,
        out int boolean);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSaveGraphics(HandleRef graphics, out int state);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRestoreGraphics(HandleRef graphics, int state);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipBeginContainer(
        HandleRef graphics,
        ref GPRECTF dstRect,
        ref GPRECTF srcRect,
        int unit,
        out int state);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipBeginContainer2(HandleRef graphics, out int state);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipBeginContainerI(
        HandleRef graphics,
        ref GPRECT dstRect,
        ref GPRECT srcRect,
        int unit,
        out int state);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipEndContainer(HandleRef graphics, int state);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetMetafileHeaderFromWmf(
        HandleRef hMetafile,
        WmfPlaceableFileHeader wmfplaceable,
        [In, Out] MetafileHeaderWmf metafileHeaderWmf);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetMetafileHeaderFromEmf(
        HandleRef hEnhMetafile,
        [In, Out] MetafileHeaderEmf metafileHeaderEmf);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetMetafileHeaderFromFile(string filename, IntPtr header);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetMetafileHeaderFromStream(
        UnsafeNativeMethods.IStream stream,
        IntPtr header);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetMetafileHeaderFromMetafile(
        HandleRef metafile,
        IntPtr header);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetHemfFromMetafile(
        HandleRef metafile,
        out IntPtr hEnhMetafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateMetafileFromWmf(
        HandleRef hMetafile,
        [MarshalAs(UnmanagedType.Bool)] bool deleteWmf,
        WmfPlaceableFileHeader wmfplacealbeHeader,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateMetafileFromEmf(
        HandleRef hEnhMetafile,
        bool deleteEmf,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateMetafileFromFile(string file, out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateMetafileFromStream(
        UnsafeNativeMethods.IStream stream,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafile(
        HandleRef referenceHdc,
        int emfType,
        ref GPRECTF frameRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafile(
        HandleRef referenceHdc,
        int emfType,
        HandleRef pframeRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafileI(
        HandleRef referenceHdc,
        int emfType,
        ref GPRECT frameRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafileFileName(
        string fileName,
        HandleRef referenceHdc,
        int emfType,
        ref GPRECTF frameRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafileFileName(
        string fileName,
        HandleRef referenceHdc,
        int emfType,
        HandleRef pframeRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafileFileNameI(
        string fileName,
        HandleRef referenceHdc,
        int emfType,
        ref GPRECT frameRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafileStream(
        UnsafeNativeMethods.IStream stream,
        HandleRef referenceHdc,
        int emfType,
        ref GPRECTF frameRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafileStream(
        UnsafeNativeMethods.IStream stream,
        HandleRef referenceHdc,
        int emfType,
        HandleRef pframeRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipRecordMetafileStreamI(
        UnsafeNativeMethods.IStream stream,
        HandleRef referenceHdc,
        int emfType,
        ref GPRECT frameRect,
        int frameUnit,
        string description,
        out IntPtr metafile);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipComment(HandleRef graphics, int sizeData, byte[] data);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipNewInstalledFontCollection(out IntPtr fontCollection);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipNewPrivateFontCollection(out IntPtr fontCollection);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeletePrivateFontCollection", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeletePrivateFontCollection(out IntPtr fontCollection);

      internal static int GdipDeletePrivateFontCollection(out IntPtr fontCollection)
      {
        if (SafeNativeMethods.Gdip.Initialized)
          return SafeNativeMethods.Gdip.IntGdipDeletePrivateFontCollection(out fontCollection);
        fontCollection = IntPtr.Zero;
        return 0;
      }

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFontCollectionFamilyCount(
        HandleRef fontCollection,
        out int numFound);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFontCollectionFamilyList(
        HandleRef fontCollection,
        int numSought,
        IntPtr[] gpfamilies,
        out int numFound);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPrivateAddFontFile(HandleRef fontCollection, string filename);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipPrivateAddMemoryFont(
        HandleRef fontCollection,
        HandleRef memory,
        int length);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateFontFamilyFromName(
        string name,
        HandleRef fontCollection,
        out IntPtr FontFamily);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetGenericFontFamilySansSerif(out IntPtr fontfamily);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetGenericFontFamilySerif(out IntPtr fontfamily);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetGenericFontFamilyMonospace(out IntPtr fontfamily);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeleteFontFamily", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeleteFontFamily(HandleRef fontFamily);

      internal static int GdipDeleteFontFamily(HandleRef fontFamily) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeleteFontFamily(fontFamily);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneFontFamily(
        HandleRef fontfamily,
        out IntPtr clonefontfamily);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFamilyName(
        HandleRef family,
        StringBuilder name,
        int language);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipIsStyleAvailable(
        HandleRef family,
        FontStyle style,
        out int isStyleAvailable);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetEmHeight(
        HandleRef family,
        FontStyle style,
        out int EmHeight);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCellAscent(
        HandleRef family,
        FontStyle style,
        out int CellAscent);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetCellDescent(
        HandleRef family,
        FontStyle style,
        out int CellDescent);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLineSpacing(
        HandleRef family,
        FontStyle style,
        out int LineSpaceing);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateFontFromDC(HandleRef hdc, ref IntPtr font);

      [DllImport("gdiplus.dll", CharSet = CharSet.Ansi, SetLastError = true)]
      internal static extern int GdipCreateFontFromLogfontA(
        HandleRef hdc,
        [MarshalAs(UnmanagedType.AsAny), In, Out] object lf,
        out IntPtr font);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateFontFromLogfontW(
        HandleRef hdc,
        [MarshalAs(UnmanagedType.AsAny), In, Out] object lf,
        out IntPtr font);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateFont(
        HandleRef fontFamily,
        float emSize,
        FontStyle style,
        GraphicsUnit unit,
        out IntPtr font);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetLogFontW(HandleRef font, HandleRef graphics, [MarshalAs(UnmanagedType.AsAny), In, Out] object lf);

      [DllImport("gdiplus.dll", CharSet = CharSet.Ansi, SetLastError = true)]
      internal static extern int GdipGetLogFontA(HandleRef font, HandleRef graphics, [MarshalAs(UnmanagedType.AsAny), In, Out] object lf);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneFont(HandleRef font, out IntPtr cloneFont);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeleteFont", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeleteFont(HandleRef font);

      internal static int GdipDeleteFont(HandleRef font) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeleteFont(font);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFamily(HandleRef font, out IntPtr family);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFontStyle(HandleRef font, out FontStyle style);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFontSize(HandleRef font, out float size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFontHeight(
        HandleRef font,
        HandleRef graphics,
        out float size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFontHeightGivenDPI(
        HandleRef font,
        float dpi,
        out float size);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetFontUnit(HandleRef font, out GraphicsUnit unit);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipDrawString(
        HandleRef graphics,
        string textString,
        int length,
        HandleRef font,
        ref GPRECTF layoutRect,
        HandleRef stringFormat,
        HandleRef brush);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipMeasureString(
        HandleRef graphics,
        string textString,
        int length,
        HandleRef font,
        ref GPRECTF layoutRect,
        HandleRef stringFormat,
        [In, Out] ref GPRECTF boundingBox,
        out int codepointsFitted,
        out int linesFilled);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipMeasureCharacterRanges(
        HandleRef graphics,
        string textString,
        int length,
        HandleRef font,
        ref GPRECTF layoutRect,
        HandleRef stringFormat,
        int characterCount,
        [In, Out] IntPtr[] region);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetStringFormatMeasurableCharacterRanges(
        HandleRef format,
        int rangeCount,
        [In, Out] CharacterRange[] range);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCreateStringFormat(
        StringFormatFlags options,
        int language,
        out IntPtr format);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipStringFormatGetGenericDefault(out IntPtr format);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipStringFormatGetGenericTypographic(out IntPtr format);

      [DllImport("gdiplus.dll", EntryPoint = "GdipDeleteStringFormat", CharSet = CharSet.Unicode, SetLastError = true)]
      private static extern int IntGdipDeleteStringFormat(HandleRef format);

      internal static int GdipDeleteStringFormat(HandleRef format) => !SafeNativeMethods.Gdip.Initialized ? 0 : SafeNativeMethods.Gdip.IntGdipDeleteStringFormat(format);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipCloneStringFormat(HandleRef format, out IntPtr newFormat);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetStringFormatFlags(
        HandleRef format,
        StringFormatFlags options);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatFlags(
        HandleRef format,
        out StringFormatFlags result);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetStringFormatAlign(HandleRef format, StringAlignment align);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatAlign(
        HandleRef format,
        out StringAlignment align);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetStringFormatLineAlign(
        HandleRef format,
        StringAlignment align);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatLineAlign(
        HandleRef format,
        out StringAlignment align);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetStringFormatHotkeyPrefix(
        HandleRef format,
        HotkeyPrefix hotkeyPrefix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatHotkeyPrefix(
        HandleRef format,
        out HotkeyPrefix hotkeyPrefix);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetStringFormatTabStops(
        HandleRef format,
        float firstTabOffset,
        int count,
        float[] tabStops);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatTabStops(
        HandleRef format,
        int count,
        out float firstTabOffset,
        [In, Out] float[] tabStops);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatTabStopCount(HandleRef format, out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatMeasurableCharacterRangeCount(
        HandleRef format,
        out int count);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetStringFormatTrimming(
        HandleRef format,
        StringTrimming trimming);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatTrimming(
        HandleRef format,
        out StringTrimming trimming);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipSetStringFormatDigitSubstitution(
        HandleRef format,
        int langID,
        StringDigitSubstitute sds);

      [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern int GdipGetStringFormatDigitSubstitution(
        HandleRef format,
        out int langID,
        out StringDigitSubstitute sds);

      internal static Exception StatusException(int status)
      {
        switch (status)
        {
          case 1:
            return (Exception) new ExternalException(SR.GetString("GdiplusGenericError"), -2147467259);
          case 2:
            return (Exception) new ArgumentException(SR.GetString("GdiplusInvalidParameter"));
          case 3:
            return (Exception) new OutOfMemoryException(SR.GetString("GdiplusOutOfMemory"));
          case 4:
            return (Exception) new InvalidOperationException(SR.GetString("GdiplusObjectBusy"));
          case 5:
            return (Exception) new OutOfMemoryException(SR.GetString("GdiplusInsufficientBuffer"));
          case 6:
            return (Exception) new NotImplementedException(SR.GetString("GdiplusNotImplemented"));
          case 7:
            return (Exception) new ExternalException(SR.GetString("GdiplusGenericError"), -2147467259);
          case 8:
            return (Exception) new InvalidOperationException(SR.GetString("GdiplusWrongState"));
          case 9:
            return (Exception) new ExternalException(SR.GetString("GdiplusAborted"), -2147467260);
          case 10:
            return (Exception) new FileNotFoundException(SR.GetString("GdiplusFileNotFound"));
          case 11:
            return (Exception) new OverflowException(SR.GetString("GdiplusOverflow"));
          case 12:
            return (Exception) new ExternalException(SR.GetString("GdiplusAccessDenied"), -2147024891);
          case 13:
            return (Exception) new ArgumentException(SR.GetString("GdiplusUnknownImageFormat"));
          case 14:
            return (Exception) new ArgumentException(SR.GetString("GdiplusFontFamilyNotFound", (object) "?"));
          case 15:
            return (Exception) new ArgumentException(SR.GetString("GdiplusFontStyleNotFound", (object) "?", (object) "?"));
          case 16:
            return (Exception) new ArgumentException(SR.GetString("GdiplusNotTrueTypeFont_NoName"));
          case 17:
            return (Exception) new ExternalException(SR.GetString("GdiplusUnsupportedGdiplusVersion"), -2147467259);
          case 18:
            return (Exception) new ExternalException(SR.GetString("GdiplusNotInitialized"), -2147467259);
          case 19:
            return (Exception) new ArgumentException(SR.GetString("GdiplusPropertyNotFoundError"));
          case 20:
            return (Exception) new ArgumentException(SR.GetString("GdiplusPropertyNotSupportedError"));
          default:
            return (Exception) new ExternalException(SR.GetString("GdiplusUnknown"), -2147418113);
        }
      }

      internal static PointF[] ConvertGPPOINTFArrayF(IntPtr memory, int count)
      {
        if (memory == IntPtr.Zero)
          throw new ArgumentNullException(nameof (memory));
        PointF[] pointFArray = new PointF[count];
        GPPOINTF gppointf = new GPPOINTF();
        int num = Marshal.SizeOf(gppointf.GetType());
        for (int index = 0; index < count; ++index)
        {
          gppointf = (GPPOINTF) UnsafeNativeMethods.PtrToStructure((IntPtr) ((long) memory + (long) (index * num)), gppointf.GetType());
          pointFArray[index] = new PointF(gppointf.X, gppointf.Y);
        }
        return pointFArray;
      }

      internal static Point[] ConvertGPPOINTArray(IntPtr memory, int count)
      {
        if (memory == IntPtr.Zero)
          throw new ArgumentNullException(nameof (memory));
        Point[] pointArray = new Point[count];
        GPPOINT gppoint = new GPPOINT();
        int num = Marshal.SizeOf(gppoint.GetType());
        for (int index = 0; index < count; ++index)
        {
          gppoint = (GPPOINT) UnsafeNativeMethods.PtrToStructure((IntPtr) ((long) memory + (long) (index * num)), gppoint.GetType());
          pointArray[index] = new Point(gppoint.X, gppoint.Y);
        }
        return pointArray;
      }

      internal static IntPtr ConvertPointToMemory(PointF[] points)
      {
        if (points == null)
          throw new ArgumentNullException(nameof (points));
        int num = Marshal.SizeOf(typeof (GPPOINTF));
        int length = points.Length;
        IntPtr memory = Marshal.AllocHGlobal(checked (length * num));
        for (int index = 0; index < length; ++index)
          Marshal.StructureToPtr((object) new GPPOINTF(points[index]), (IntPtr) checked (unchecked ((long) memory) + (long) (index * num)), false);
        return memory;
      }

      internal static IntPtr ConvertPointToMemory(Point[] points)
      {
        if (points == null)
          throw new ArgumentNullException(nameof (points));
        int num = Marshal.SizeOf(typeof (GPPOINT));
        int length = points.Length;
        IntPtr memory = Marshal.AllocHGlobal(checked (length * num));
        for (int index = 0; index < length; ++index)
          Marshal.StructureToPtr((object) new GPPOINT(points[index]), (IntPtr) checked (unchecked ((long) memory) + (long) (index * num)), false);
        return memory;
      }

      internal static IntPtr ConvertRectangleToMemory(RectangleF[] rect)
      {
        if (rect == null)
          throw new ArgumentNullException(nameof (rect));
        int num = Marshal.SizeOf(typeof (GPRECTF));
        int length = rect.Length;
        IntPtr memory = Marshal.AllocHGlobal(checked (length * num));
        for (int index = 0; index < length; ++index)
          Marshal.StructureToPtr((object) new GPRECTF(rect[index]), (IntPtr) checked (unchecked ((long) memory) + (long) (index * num)), false);
        return memory;
      }

      internal static IntPtr ConvertRectangleToMemory(Rectangle[] rect)
      {
        if (rect == null)
          throw new ArgumentNullException(nameof (rect));
        int num = Marshal.SizeOf(typeof (GPRECT));
        int length = rect.Length;
        IntPtr memory = Marshal.AllocHGlobal(checked (length * num));
        for (int index = 0; index < length; ++index)
          Marshal.StructureToPtr((object) new GPRECT(rect[index]), (IntPtr) checked (unchecked ((long) memory) + (long) (index * num)), false);
        return memory;
      }

      private struct StartupInput
      {
        public int GdiplusVersion;
        public IntPtr DebugEventCallback;
        public bool SuppressBackgroundThread;
        public bool SuppressExternalCodecs;

        public static SafeNativeMethods.Gdip.StartupInput GetDefault() => new SafeNativeMethods.Gdip.StartupInput()
        {
          GdiplusVersion = 1,
          SuppressBackgroundThread = false,
          SuppressExternalCodecs = false
        };
      }

      private struct StartupOutput
      {
        public IntPtr hook;
        public IntPtr unhook;
      }

      private enum DebugEventLevel
      {
        Fatal,
        Warning,
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ENHMETAHEADER
    {
      public int iType;
      public int nSize = 40;
      public int rclBounds_left;
      public int rclBounds_top;
      public int rclBounds_right;
      public int rclBounds_bottom;
      public int rclFrame_left;
      public int rclFrame_top;
      public int rclFrame_right;
      public int rclFrame_bottom;
      public int dSignature;
      public int nVersion;
      public int nBytes;
      public int nRecords;
      public short nHandles;
      public short sReserved;
      public int nDescription;
      public int offDescription;
      public int nPalEntries;
      public int szlDevice_cx;
      public int szlDevice_cy;
      public int szlMillimeters_cx;
      public int szlMillimeters_cy;
      public int cbPixelFormat;
      public int offPixelFormat;
      public int bOpenGL;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class DOCINFO
    {
      public int cbSize = 20;
      public string lpszDocName;
      public string lpszOutput;
      public string lpszDatatype;
      public int fwType;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class PRINTDLG
    {
      public int lStructSize;
      public IntPtr hwndOwner;
      public IntPtr hDevMode;
      public IntPtr hDevNames;
      public IntPtr hDC;
      public int Flags;
      public short nFromPage;
      public short nToPage;
      public short nMinPage;
      public short nMaxPage;
      public short nCopies;
      public IntPtr hInstance;
      public IntPtr lCustData;
      public IntPtr lpfnPrintHook;
      public IntPtr lpfnSetupHook;
      public string lpPrintTemplateName;
      public string lpSetupTemplateName;
      public IntPtr hPrintTemplate;
      public IntPtr hSetupTemplate;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
    public class PRINTDLGX86
    {
      public int lStructSize;
      public IntPtr hwndOwner;
      public IntPtr hDevMode;
      public IntPtr hDevNames;
      public IntPtr hDC;
      public int Flags;
      public short nFromPage;
      public short nToPage;
      public short nMinPage;
      public short nMaxPage;
      public short nCopies;
      public IntPtr hInstance;
      public IntPtr lCustData;
      public IntPtr lpfnPrintHook;
      public IntPtr lpfnSetupHook;
      public string lpPrintTemplateName;
      public string lpSetupTemplateName;
      public IntPtr hPrintTemplate;
      public IntPtr hSetupTemplate;
    }

    public enum StructFormat
    {
      Ansi = 1,
      Unicode = 2,
      Auto = 3,
    }

    public struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    public struct MSG
    {
      public IntPtr hwnd;
      public int message;
      public IntPtr wParam;
      public IntPtr lParam;
      public int time;
      public int pt_x;
      public int pt_y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ICONINFO
    {
      public int fIcon;
      public int xHotspot;
      public int yHotspot;
      public IntPtr hbmMask = IntPtr.Zero;
      public IntPtr hbmColor = IntPtr.Zero;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class BITMAP
    {
      public int bmType;
      public int bmWidth;
      public int bmHeight;
      public int bmWidthBytes;
      public short bmPlanes;
      public short bmBitsPixel;
      public IntPtr bmBits = IntPtr.Zero;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class BITMAPINFOHEADER
    {
      public int biSize = 40;
      public int biWidth;
      public int biHeight;
      public short biPlanes;
      public short biBitCount;
      public int biCompression;
      public int biSizeImage;
      public int biXPelsPerMeter;
      public int biYPelsPerMeter;
      public int biClrUsed;
      public int biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class LOGFONT
    {
      public int lfHeight;
      public int lfWidth;
      public int lfEscapement;
      public int lfOrientation;
      public int lfWeight;
      public byte lfItalic;
      public byte lfUnderline;
      public byte lfStrikeOut;
      public byte lfCharSet;
      public byte lfOutPrecision;
      public byte lfClipPrecision;
      public byte lfQuality;
      public byte lfPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string lfFaceName;

      public LOGFONT()
      {
      }

      public LOGFONT(SafeNativeMethods.LOGFONT lf)
      {
        this.lfHeight = lf.lfHeight;
        this.lfWidth = lf.lfWidth;
        this.lfEscapement = lf.lfEscapement;
        this.lfOrientation = lf.lfOrientation;
        this.lfWeight = lf.lfWeight;
        this.lfItalic = lf.lfItalic;
        this.lfUnderline = lf.lfUnderline;
        this.lfStrikeOut = lf.lfStrikeOut;
        this.lfCharSet = lf.lfCharSet;
        this.lfOutPrecision = lf.lfOutPrecision;
        this.lfClipPrecision = lf.lfClipPrecision;
        this.lfQuality = lf.lfQuality;
        this.lfPitchAndFamily = lf.lfPitchAndFamily;
        this.lfFaceName = lf.lfFaceName;
      }

      public override string ToString() => "lfHeight=" + this.lfHeight.ToString() + ", lfWidth=" + this.lfWidth.ToString() + ", lfEscapement=" + this.lfEscapement.ToString() + ", lfOrientation=" + this.lfOrientation.ToString() + ", lfWeight=" + this.lfWeight.ToString() + ", lfItalic=" + this.lfItalic.ToString() + ", lfUnderline=" + this.lfUnderline.ToString() + ", lfStrikeOut=" + this.lfStrikeOut.ToString() + ", lfCharSet=" + this.lfCharSet.ToString() + ", lfOutPrecision=" + this.lfOutPrecision.ToString() + ", lfClipPrecision=" + this.lfClipPrecision.ToString() + ", lfQuality=" + this.lfQuality.ToString() + ", lfPitchAndFamily=" + this.lfPitchAndFamily.ToString() + ", lfFaceName=" + this.lfFaceName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TEXTMETRIC
    {
      public int tmHeight;
      public int tmAscent;
      public int tmDescent;
      public int tmInternalLeading;
      public int tmExternalLeading;
      public int tmAveCharWidth;
      public int tmMaxCharWidth;
      public int tmWeight;
      public int tmOverhang;
      public int tmDigitizedAspectX;
      public int tmDigitizedAspectY;
      public char tmFirstChar;
      public char tmLastChar;
      public char tmDefaultChar;
      public char tmBreakChar;
      public byte tmItalic;
      public byte tmUnderlined;
      public byte tmStruckOut;
      public byte tmPitchAndFamily;
      public byte tmCharSet;
    }

    public struct TEXTMETRICA
    {
      public int tmHeight;
      public int tmAscent;
      public int tmDescent;
      public int tmInternalLeading;
      public int tmExternalLeading;
      public int tmAveCharWidth;
      public int tmMaxCharWidth;
      public int tmWeight;
      public int tmOverhang;
      public int tmDigitizedAspectX;
      public int tmDigitizedAspectY;
      public byte tmFirstChar;
      public byte tmLastChar;
      public byte tmDefaultChar;
      public byte tmBreakChar;
      public byte tmItalic;
      public byte tmUnderlined;
      public byte tmStruckOut;
      public byte tmPitchAndFamily;
      public byte tmCharSet;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct ICONDIR
    {
      public short idReserved;
      public short idType;
      public short idCount;
      public SafeNativeMethods.ICONDIRENTRY idEntries;
    }

    public struct ICONDIRENTRY
    {
      public byte bWidth;
      public byte bHeight;
      public byte bColorCount;
      public byte bReserved;
      public short wPlanes;
      public short wBitCount;
      public int dwBytesInRes;
      public int dwImageOffset;
    }

    public class Ole
    {
      public const int PICTYPE_UNINITIALIZED = -1;
      public const int PICTYPE_NONE = 0;
      public const int PICTYPE_BITMAP = 1;
      public const int PICTYPE_METAFILE = 2;
      public const int PICTYPE_ICON = 3;
      public const int PICTYPE_ENHMETAFILE = 4;
      public const int STATFLAG_DEFAULT = 0;
      public const int STATFLAG_NONAME = 1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class PICTDESC
    {
      internal int cbSizeOfStruct;
      public int picType;
      internal IntPtr union1;
      internal int union2;
      internal int union3;

      public static SafeNativeMethods.PICTDESC CreateIconPICTDESC(IntPtr hicon) => new SafeNativeMethods.PICTDESC()
      {
        cbSizeOfStruct = 12,
        picType = 3,
        union1 = hicon
      };

      public virtual IntPtr GetHandle() => this.union1;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class DEVMODE
    {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string dmDeviceName;
      public short dmSpecVersion;
      public short dmDriverVersion;
      public short dmSize;
      public short dmDriverExtra;
      public int dmFields;
      public short dmOrientation;
      public short dmPaperSize;
      public short dmPaperLength;
      public short dmPaperWidth;
      public short dmScale;
      public short dmCopies;
      public short dmDefaultSource;
      public short dmPrintQuality;
      public short dmColor;
      public short dmDuplex;
      public short dmYResolution;
      public short dmTTOption;
      public short dmCollate;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string dmFormName;
      public short dmLogPixels;
      public int dmBitsPerPel;
      public int dmPelsWidth;
      public int dmPelsHeight;
      public int dmDisplayFlags;
      public int dmDisplayFrequency;
      public int dmICMMethod;
      public int dmICMIntent;
      public int dmMediaType;
      public int dmDitherType;
      public int dmICCManufacturer;
      public int dmICCModel;
      public int dmPanningWidth;
      public int dmPanningHeight;

      public override string ToString() => "[DEVMODE: dmDeviceName=" + this.dmDeviceName + ", dmSpecVersion=" + this.dmSpecVersion.ToString() + ", dmDriverVersion=" + this.dmDriverVersion.ToString() + ", dmSize=" + this.dmSize.ToString() + ", dmDriverExtra=" + this.dmDriverExtra.ToString() + ", dmFields=" + this.dmFields.ToString() + ", dmOrientation=" + this.dmOrientation.ToString() + ", dmPaperSize=" + this.dmPaperSize.ToString() + ", dmPaperLength=" + this.dmPaperLength.ToString() + ", dmPaperWidth=" + this.dmPaperWidth.ToString() + ", dmScale=" + this.dmScale.ToString() + ", dmCopies=" + this.dmCopies.ToString() + ", dmDefaultSource=" + this.dmDefaultSource.ToString() + ", dmPrintQuality=" + this.dmPrintQuality.ToString() + ", dmColor=" + this.dmColor.ToString() + ", dmDuplex=" + this.dmDuplex.ToString() + ", dmYResolution=" + this.dmYResolution.ToString() + ", dmTTOption=" + this.dmTTOption.ToString() + ", dmCollate=" + this.dmCollate.ToString() + ", dmFormName=" + this.dmFormName + ", dmLogPixels=" + this.dmLogPixels.ToString() + ", dmBitsPerPel=" + this.dmBitsPerPel.ToString() + ", dmPelsWidth=" + this.dmPelsWidth.ToString() + ", dmPelsHeight=" + this.dmPelsHeight.ToString() + ", dmDisplayFlags=" + this.dmDisplayFlags.ToString() + ", dmDisplayFrequency=" + this.dmDisplayFrequency.ToString() + ", dmICMMethod=" + this.dmICMMethod.ToString() + ", dmICMIntent=" + this.dmICMIntent.ToString() + ", dmMediaType=" + this.dmMediaType.ToString() + ", dmDitherType=" + this.dmDitherType.ToString() + ", dmICCManufacturer=" + this.dmICCManufacturer.ToString() + ", dmICCModel=" + this.dmICCModel.ToString() + ", dmPanningWidth=" + this.dmPanningWidth.ToString() + ", dmPanningHeight=" + this.dmPanningHeight.ToString() + "]";
    }

    public sealed class CommonHandles
    {
      public static readonly int Accelerator = System.Internal.HandleCollector.RegisterType(nameof (Accelerator), 80, 50);
      public static readonly int Cursor = System.Internal.HandleCollector.RegisterType(nameof (Cursor), 20, 500);
      public static readonly int EMF = System.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
      public static readonly int Find = System.Internal.HandleCollector.RegisterType(nameof (Find), 0, 1000);
      public static readonly int GDI = System.Internal.HandleCollector.RegisterType(nameof (GDI), 50, 500);
      public static readonly int HDC = System.Internal.HandleCollector.RegisterType(nameof (HDC), 100, 2);
      public static readonly int Icon = System.Internal.HandleCollector.RegisterType(nameof (Icon), 20, 500);
      public static readonly int Kernel = System.Internal.HandleCollector.RegisterType(nameof (Kernel), 0, 1000);
      public static readonly int Menu = System.Internal.HandleCollector.RegisterType(nameof (Menu), 30, 1000);
      public static readonly int Window = System.Internal.HandleCollector.RegisterType(nameof (Window), 5, 1000);
    }

    public class StreamConsts
    {
      public const int LOCK_WRITE = 1;
      public const int LOCK_EXCLUSIVE = 2;
      public const int LOCK_ONLYONCE = 4;
      public const int STATFLAG_DEFAULT = 0;
      public const int STATFLAG_NONAME = 1;
      public const int STATFLAG_NOOPEN = 2;
      public const int STGC_DEFAULT = 0;
      public const int STGC_OVERWRITE = 1;
      public const int STGC_ONLYIFCURRENT = 2;
      public const int STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4;
      public const int STREAM_SEEK_SET = 0;
      public const int STREAM_SEEK_CUR = 1;
      public const int STREAM_SEEK_END = 2;
    }

    [Guid("7BF80980-BF32-101A-8BBB-00AA00300CAB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IPicture
    {
      [SuppressUnmanagedCodeSecurity]
      IntPtr GetHandle();

      [SuppressUnmanagedCodeSecurity]
      IntPtr GetHPal();

      [SuppressUnmanagedCodeSecurity]
      [return: MarshalAs(UnmanagedType.I2)]
      short GetPictureType();

      [SuppressUnmanagedCodeSecurity]
      int GetWidth();

      [SuppressUnmanagedCodeSecurity]
      int GetHeight();

      [SuppressUnmanagedCodeSecurity]
      void Render();

      [SuppressUnmanagedCodeSecurity]
      void SetHPal([In] IntPtr phpal);

      [SuppressUnmanagedCodeSecurity]
      IntPtr GetCurDC();

      [SuppressUnmanagedCodeSecurity]
      void SelectPicture([In] IntPtr hdcIn, [MarshalAs(UnmanagedType.LPArray), Out] int[] phdcOut, [MarshalAs(UnmanagedType.LPArray), Out] int[] phbmpOut);

      [SuppressUnmanagedCodeSecurity]
      [return: MarshalAs(UnmanagedType.Bool)]
      bool GetKeepOriginalFormat();

      [SuppressUnmanagedCodeSecurity]
      void SetKeepOriginalFormat([MarshalAs(UnmanagedType.Bool), In] bool pfkeep);

      [SuppressUnmanagedCodeSecurity]
      void PictureChanged();

      [SuppressUnmanagedCodeSecurity]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int SaveAsFile([MarshalAs(UnmanagedType.Interface), In] UnsafeNativeMethods.IStream pstm, [In] int fSaveMemCopy, out int pcbSize);

      [SuppressUnmanagedCodeSecurity]
      int GetAttributes();

      [SuppressUnmanagedCodeSecurity]
      void SetHdc([In] IntPtr hdc);
    }

    public struct OBJECTHEADER
    {
      public short signature;
      public short headersize;
      public short objectType;
      public short nameLen;
      public short classLen;
      public short nameOffset;
      public short classOffset;
      public short width;
      public short height;
      public IntPtr pInfo;
    }

    internal enum Win32SystemColors
    {
      ScrollBar = 0,
      Desktop = 1,
      ActiveCaption = 2,
      InactiveCaption = 3,
      Menu = 4,
      Window = 5,
      WindowFrame = 6,
      MenuText = 7,
      WindowText = 8,
      ActiveCaptionText = 9,
      ActiveBorder = 10, // 0x0000000A
      InactiveBorder = 11, // 0x0000000B
      AppWorkspace = 12, // 0x0000000C
      Highlight = 13, // 0x0000000D
      HighlightText = 14, // 0x0000000E
      ButtonFace = 15, // 0x0000000F
      Control = 15, // 0x0000000F
      ButtonShadow = 16, // 0x00000010
      ControlDark = 16, // 0x00000010
      GrayText = 17, // 0x00000011
      ControlText = 18, // 0x00000012
      InactiveCaptionText = 19, // 0x00000013
      ButtonHighlight = 20, // 0x00000014
      ControlLightLight = 20, // 0x00000014
      ControlDarkDark = 21, // 0x00000015
      ControlLight = 22, // 0x00000016
      InfoText = 23, // 0x00000017
      Info = 24, // 0x00000018
      HotTrack = 26, // 0x0000001A
      GradientActiveCaption = 27, // 0x0000001B
      GradientInactiveCaption = 28, // 0x0000001C
      MenuHighlight = 29, // 0x0000001D
      MenuBar = 30, // 0x0000001E
    }

    public enum BackgroundMode
    {
      TRANSPARENT = 1,
      OPAQUE = 2,
    }
  }
}
