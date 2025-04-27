namespace Wise.CLI.Generator.UnitTests;

public enum TestEnum
{
    Option1,
    Option2,
    Option3
}

[CliOptions(generateHelp: false)]
public partial class TestOptions
{
    [CliOption("file", required: true)]
    public string File { get; set; } = string.Empty;

    [CliOption("value1", required: true)]
    public byte Value1 { get; set; }

    [CliOption("value2", required: true)]
    public short Value2 { get; set; }

    [CliOption("value3", required: false)]
    public ushort Value3 { get; set; }

    [CliOption("value4", required: false)]
    public int Value4 { get; set; }

    [CliOption("value5", required: false)]
    public uint Value5 { get; set; }

    [CliOption("value6", required: false)]
    public long Value6 { get; set; }

    [CliOption("value7", required: false)]
    public ulong Value7 { get; set; }

    [CliOption("value8", required: false)]
    public float Value8 { get; set; }

    [CliOption("value9", required: false)]
    public double Value9 { get; set; }

    [CliOption("value10", required: false)]
    public decimal Value10 { get; set; }

    [CliOption("time", 't', required: false)]
    public DateTime Time { get; set; }

    [CliOption("enum", 'e', required: false)]
    public TestEnum Enum { get; set; }
}