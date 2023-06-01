// Decompiled with JetBrains decompiler
// Type: System.Drawing.Printing.TriState
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

namespace System.Drawing.Printing
{
  [Serializable]
  internal struct TriState
  {
    private byte value;
    public static readonly TriState Default = new TriState((byte) 0);
    public static readonly TriState False = new TriState((byte) 1);
    public static readonly TriState True = new TriState((byte) 2);

    private TriState(byte value) => this.value = value;

    public bool IsDefault => this == TriState.Default;

    public bool IsFalse => this == TriState.False;

    public bool IsNotDefault => this != TriState.Default;

    public bool IsTrue => this == TriState.True;

    public static bool operator ==(TriState left, TriState right) => (int) left.value == (int) right.value;

    public static bool operator !=(TriState left, TriState right) => !(left == right);

    public override bool Equals(object o) => (int) this.value == (int) ((TriState) o).value;

    public override int GetHashCode() => (int) this.value;

    public static implicit operator TriState(bool value) => !value ? TriState.False : TriState.True;

    public static explicit operator bool(TriState value)
    {
      if (value.IsDefault)
        throw new InvalidCastException(System.Drawing.SR.GetString("TriStateCompareError"));
      return value == TriState.True;
    }

    public override string ToString()
    {
      if (this == TriState.Default)
        return "Default";
      return this == TriState.False ? "False" : "True";
    }
  }
}
