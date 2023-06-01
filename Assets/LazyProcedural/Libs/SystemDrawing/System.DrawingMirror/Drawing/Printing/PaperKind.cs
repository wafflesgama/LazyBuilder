// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.PaperKind
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  /// <summary>Specifies the standard paper sizes.</summary>
  [Serializable]
  public enum PaperKind
  {
    /// <summary>The paper size is defined by the user.</summary>
    Custom = 0,
    /// <summary>Letter paper (8.5 in. by 11 in.).</summary>
    Letter = 1,
    /// <summary>Letter small paper (8.5 in. by 11 in.).</summary>
    LetterSmall = 2,
    /// <summary>Tabloid paper (11 in. by 17 in.).</summary>
    Tabloid = 3,
    /// <summary>Ledger paper (17 in. by 11 in.).</summary>
    Ledger = 4,
    /// <summary>Legal paper (8.5 in. by 14 in.).</summary>
    Legal = 5,
    /// <summary>Statement paper (5.5 in. by 8.5 in.).</summary>
    Statement = 6,
    /// <summary>Executive paper (7.25 in. by 10.5 in.).</summary>
    Executive = 7,
    /// <summary>A3 paper (297 mm by 420 mm).</summary>
    A3 = 8,
    /// <summary>A4 paper (210 mm by 297 mm).</summary>
    A4 = 9,
    /// <summary>A4 small paper (210 mm by 297 mm).</summary>
    A4Small = 10, // 0x0000000A
    /// <summary>A5 paper (148 mm by 210 mm).</summary>
    A5 = 11, // 0x0000000B
    /// <summary>B4 paper (250 mm by 353 mm).</summary>
    B4 = 12, // 0x0000000C
    /// <summary>B5 paper (176 mm by 250 mm).</summary>
    B5 = 13, // 0x0000000D
    /// <summary>Folio paper (8.5 in. by 13 in.).</summary>
    Folio = 14, // 0x0000000E
    /// <summary>Quarto paper (215 mm by 275 mm).</summary>
    Quarto = 15, // 0x0000000F
    /// <summary>Standard paper (10 in. by 14 in.).</summary>
    Standard10x14 = 16, // 0x00000010
    /// <summary>Standard paper (11 in. by 17 in.).</summary>
    Standard11x17 = 17, // 0x00000011
    /// <summary>Note paper (8.5 in. by 11 in.).</summary>
    Note = 18, // 0x00000012
    /// <summary>#9 envelope (3.875 in. by 8.875 in.).</summary>
    Number9Envelope = 19, // 0x00000013
    /// <summary>#10 envelope (4.125 in. by 9.5 in.).</summary>
    Number10Envelope = 20, // 0x00000014
    /// <summary>#11 envelope (4.5 in. by 10.375 in.).</summary>
    Number11Envelope = 21, // 0x00000015
    /// <summary>#12 envelope (4.75 in. by 11 in.).</summary>
    Number12Envelope = 22, // 0x00000016
    /// <summary>#14 envelope (5 in. by 11.5 in.).</summary>
    Number14Envelope = 23, // 0x00000017
    /// <summary>C paper (17 in. by 22 in.).</summary>
    CSheet = 24, // 0x00000018
    /// <summary>D paper (22 in. by 34 in.).</summary>
    DSheet = 25, // 0x00000019
    /// <summary>E paper (34 in. by 44 in.).</summary>
    ESheet = 26, // 0x0000001A
    /// <summary>DL envelope (110 mm by 220 mm).</summary>
    DLEnvelope = 27, // 0x0000001B
    /// <summary>C5 envelope (162 mm by 229 mm).</summary>
    C5Envelope = 28, // 0x0000001C
    /// <summary>C3 envelope (324 mm by 458 mm).</summary>
    C3Envelope = 29, // 0x0000001D
    /// <summary>C4 envelope (229 mm by 324 mm).</summary>
    C4Envelope = 30, // 0x0000001E
    /// <summary>C6 envelope (114 mm by 162 mm).</summary>
    C6Envelope = 31, // 0x0000001F
    /// <summary>C65 envelope (114 mm by 229 mm).</summary>
    C65Envelope = 32, // 0x00000020
    /// <summary>B4 envelope (250 mm by 353 mm).</summary>
    B4Envelope = 33, // 0x00000021
    /// <summary>B5 envelope (176 mm by 250 mm).</summary>
    B5Envelope = 34, // 0x00000022
    /// <summary>B6 envelope (176 mm by 125 mm).</summary>
    B6Envelope = 35, // 0x00000023
    /// <summary>Italy envelope (110 mm by 230 mm).</summary>
    ItalyEnvelope = 36, // 0x00000024
    /// <summary>Monarch envelope (3.875 in. by 7.5 in.).</summary>
    MonarchEnvelope = 37, // 0x00000025
    /// <summary>6 3/4 envelope (3.625 in. by 6.5 in.).</summary>
    PersonalEnvelope = 38, // 0x00000026
    /// <summary>US standard fanfold (14.875 in. by 11 in.).</summary>
    USStandardFanfold = 39, // 0x00000027
    /// <summary>German standard fanfold (8.5 in. by 12 in.).</summary>
    GermanStandardFanfold = 40, // 0x00000028
    /// <summary>German legal fanfold (8.5 in. by 13 in.).</summary>
    GermanLegalFanfold = 41, // 0x00000029
    /// <summary>ISO B4 (250 mm by 353 mm).</summary>
    IsoB4 = 42, // 0x0000002A
    /// <summary>Japanese postcard (100 mm by 148 mm).</summary>
    JapanesePostcard = 43, // 0x0000002B
    /// <summary>Standard paper (9 in. by 11 in.).</summary>
    Standard9x11 = 44, // 0x0000002C
    /// <summary>Standard paper (10 in. by 11 in.).</summary>
    Standard10x11 = 45, // 0x0000002D
    /// <summary>Standard paper (15 in. by 11 in.).</summary>
    Standard15x11 = 46, // 0x0000002E
    /// <summary>Invitation envelope (220 mm by 220 mm).</summary>
    InviteEnvelope = 47, // 0x0000002F
    /// <summary>Letter extra paper (9.275 in. by 12 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.</summary>
    LetterExtra = 50, // 0x00000032
    /// <summary>Legal extra paper (9.275 in. by 15 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.</summary>
    LegalExtra = 51, // 0x00000033
    /// <summary>Tabloid extra paper (11.69 in. by 18 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.</summary>
    TabloidExtra = 52, // 0x00000034
    /// <summary>A4 extra paper (236 mm by 322 mm). This value is specific to the PostScript driver and is used only by Linotronic printers to help save paper.</summary>
    A4Extra = 53, // 0x00000035
    /// <summary>Letter transverse paper (8.275 in. by 11 in.).</summary>
    LetterTransverse = 54, // 0x00000036
    /// <summary>A4 transverse paper (210 mm by 297 mm).</summary>
    A4Transverse = 55, // 0x00000037
    /// <summary>Letter extra transverse paper (9.275 in. by 12 in.).</summary>
    LetterExtraTransverse = 56, // 0x00000038
    /// <summary>SuperA/SuperA/A4 paper (227 mm by 356 mm).</summary>
    APlus = 57, // 0x00000039
    /// <summary>SuperB/SuperB/A3 paper (305 mm by 487 mm).</summary>
    BPlus = 58, // 0x0000003A
    /// <summary>Letter plus paper (8.5 in. by 12.69 in.).</summary>
    LetterPlus = 59, // 0x0000003B
    /// <summary>A4 plus paper (210 mm by 330 mm).</summary>
    A4Plus = 60, // 0x0000003C
    /// <summary>A5 transverse paper (148 mm by 210 mm).</summary>
    A5Transverse = 61, // 0x0000003D
    /// <summary>JIS B5 transverse paper (182 mm by 257 mm).</summary>
    B5Transverse = 62, // 0x0000003E
    /// <summary>A3 extra paper (322 mm by 445 mm).</summary>
    A3Extra = 63, // 0x0000003F
    /// <summary>A5 extra paper (174 mm by 235 mm).</summary>
    A5Extra = 64, // 0x00000040
    /// <summary>ISO B5 extra paper (201 mm by 276 mm).</summary>
    B5Extra = 65, // 0x00000041
    /// <summary>A2 paper (420 mm by 594 mm).</summary>
    A2 = 66, // 0x00000042
    /// <summary>A3 transverse paper (297 mm by 420 mm).</summary>
    A3Transverse = 67, // 0x00000043
    /// <summary>A3 extra transverse paper (322 mm by 445 mm).</summary>
    A3ExtraTransverse = 68, // 0x00000044
    /// <summary>Japanese double postcard (200 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseDoublePostcard = 69, // 0x00000045
    /// <summary>A6 paper (105 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    A6 = 70, // 0x00000046
    /// <summary>Japanese Kaku #2 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeKakuNumber2 = 71, // 0x00000047
    /// <summary>Japanese Kaku #3 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeKakuNumber3 = 72, // 0x00000048
    /// <summary>Japanese Chou #3 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeChouNumber3 = 73, // 0x00000049
    /// <summary>Japanese Chou #4 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeChouNumber4 = 74, // 0x0000004A
    /// <summary>Letter rotated paper (11 in. by 8.5 in.).</summary>
    LetterRotated = 75, // 0x0000004B
    /// <summary>A3 rotated paper (420 mm by 297 mm).</summary>
    A3Rotated = 76, // 0x0000004C
    /// <summary>A4 rotated paper (297 mm by 210 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    A4Rotated = 77, // 0x0000004D
    /// <summary>A5 rotated paper (210 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    A5Rotated = 78, // 0x0000004E
    /// <summary>JIS B4 rotated paper (364 mm by 257 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    B4JisRotated = 79, // 0x0000004F
    /// <summary>JIS B5 rotated paper (257 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    B5JisRotated = 80, // 0x00000050
    /// <summary>Japanese rotated postcard (148 mm by 100 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapanesePostcardRotated = 81, // 0x00000051
    /// <summary>Japanese rotated double postcard (148 mm by 200 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseDoublePostcardRotated = 82, // 0x00000052
    /// <summary>A6 rotated paper (148 mm by 105 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    A6Rotated = 83, // 0x00000053
    /// <summary>Japanese rotated Kaku #2 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeKakuNumber2Rotated = 84, // 0x00000054
    /// <summary>Japanese rotated Kaku #3 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeKakuNumber3Rotated = 85, // 0x00000055
    /// <summary>Japanese rotated Chou #3 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeChouNumber3Rotated = 86, // 0x00000056
    /// <summary>Japanese rotated Chou #4 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeChouNumber4Rotated = 87, // 0x00000057
    /// <summary>JIS B6 paper (128 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    B6Jis = 88, // 0x00000058
    /// <summary>JIS B6 rotated paper (182 mm by 128 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    B6JisRotated = 89, // 0x00000059
    /// <summary>Standard paper (12 in. by 11 in.). Requires Windows 98, Windows NT 4.0, or later.</summary>
    Standard12x11 = 90, // 0x0000005A
    /// <summary>Japanese You #4 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeYouNumber4 = 91, // 0x0000005B
    /// <summary>Japanese You #4 rotated envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
    JapaneseEnvelopeYouNumber4Rotated = 92, // 0x0000005C
    /// <summary>16K paper (146 mm by 215 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    Prc16K = 93, // 0x0000005D
    /// <summary>32K paper (97 mm by 151 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    Prc32K = 94, // 0x0000005E
    /// <summary>32K big paper (97 mm by 151 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    Prc32KBig = 95, // 0x0000005F
    /// <summary>#1 envelope (102 mm by 165 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber1 = 96, // 0x00000060
    /// <summary>#2 envelope (102 mm by 176 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber2 = 97, // 0x00000061
    /// <summary>#3 envelope (125 mm by 176 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber3 = 98, // 0x00000062
    /// <summary>#4 envelope (110 mm by 208 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber4 = 99, // 0x00000063
    /// <summary>#5 envelope (110 mm by 220 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber5 = 100, // 0x00000064
    /// <summary>#6 envelope (120 mm by 230 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber6 = 101, // 0x00000065
    /// <summary>#7 envelope (160 mm by 230 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber7 = 102, // 0x00000066
    /// <summary>#8 envelope (120 mm by 309 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber8 = 103, // 0x00000067
    /// <summary>#9 envelope (229 mm by 324 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber9 = 104, // 0x00000068
    /// <summary>#10 envelope (324 mm by 458 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber10 = 105, // 0x00000069
    /// <summary>16K rotated paper (146 mm by 215 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    Prc16KRotated = 106, // 0x0000006A
    /// <summary>32K rotated paper (97 mm by 151 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    Prc32KRotated = 107, // 0x0000006B
    /// <summary>32K big rotated paper (97 mm by 151 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    Prc32KBigRotated = 108, // 0x0000006C
    /// <summary>#1 rotated envelope (165 mm by 102 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber1Rotated = 109, // 0x0000006D
    /// <summary>#2 rotated envelope (176 mm by 102 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber2Rotated = 110, // 0x0000006E
    /// <summary>#3 rotated envelope (176 mm by 125 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber3Rotated = 111, // 0x0000006F
    /// <summary>#4 rotated envelope (208 mm by 110 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber4Rotated = 112, // 0x00000070
    /// <summary>Envelope #5 rotated envelope (220 mm by 110 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber5Rotated = 113, // 0x00000071
    /// <summary>#6 rotated envelope (230 mm by 120 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber6Rotated = 114, // 0x00000072
    /// <summary>#7 rotated envelope (230 mm by 160 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber7Rotated = 115, // 0x00000073
    /// <summary>#8 rotated envelope (309 mm by 120 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber8Rotated = 116, // 0x00000074
    /// <summary>#9 rotated envelope (324 mm by 229 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber9Rotated = 117, // 0x00000075
    /// <summary>#10 rotated envelope (458 mm by 324 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
    PrcEnvelopeNumber10Rotated = 118, // 0x00000076
  }
}
