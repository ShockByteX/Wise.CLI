using System.Diagnostics;

namespace Wise.CLI.Generator.UnitTests;

public class CliTests
{
    [Fact]
    public void Parse_MustPass_TypeCheck()
    {
        var args = new[]
        {
            "--file", @"C:\test.txt",
            "--value1", "1",
            "--value2", "2",
            "--value3", "3",
            "--value4", "4",
            "--value5", "5",
            "--value6", "6",
            "--value7", "7",
            "--value8", "8.5",
            "--value9", "9.5",
            "--value10", "10.5",
            "-t", "2024-04-24T12:00:00",
            "-e", "Option2"
        };

        var options = TestOptions.Parse(args);

        Assert.Equal(@"C:\test.txt", options.File);
        Assert.Equal((byte)1, options.Value1);
        Assert.Equal((short)2, options.Value2);
        Assert.Equal((ushort)3, options.Value3);
        Assert.Equal(4, options.Value4);
        Assert.Equal((uint)5, options.Value5);
        Assert.Equal(6L, options.Value6);
        Assert.Equal(7UL, options.Value7);
        Assert.Equal(8.5f, options.Value8);
        Assert.Equal(9.5d, options.Value9);
        Assert.Equal(10.5m, options.Value10);
        Assert.Equal(new DateTime(2024, 4, 24, 12, 0, 0), options.Time);
        Assert.Equal(TestEnum.Option2, options.Enum);
    }

    [Fact]
    public void MustPass_AliasCheck()
    {
        var args = new[]
        {
            "--file", "test.txt",
            "--value1", "1",
            "--value2", "2",
            "-e", "Option3",
            "-t", "2025-01-01T00:00:00"
        };

        var options = TestOptions.Parse(args);

        Assert.Equal(TestEnum.Option3, options.Enum);
        Assert.Equal(new DateTime(2025, 1, 1, 0, 0, 0), options.Time);
    }

    [Fact]
    public void MustThrow_MissingRequiredArgument()
    {
        var args = new[]
        {
            "--file", "test.txt",
            "--value2", "2"
            // --value1 is missing
        };

        Assert.Contains("value1", Assert.Throws<ArgumentException>(() => TestOptions.Parse(args)).Message);
    }

    [Fact]
    public void MustThrow_UnknownArgument()
    {
        var args = new[]
        {
            "--file", "test.txt",
            "--value1", "1",
            "--value2", "2",
            "--unknown", "oops"
        };

        Assert.Contains("unknown", Assert.Throws<ArgumentException>(() => TestOptions.Parse(args)).Message);
    }

    [Theory]
    [InlineData("Option1")]
    [InlineData("option1")]
    [InlineData("OPTION1")]
    public void MustPass_EnumCaseInsensitive(string enumValue)
    {
        var args = new[]
        {
            "--file", "test.txt",
            "--value1", "1",
            "--value2", "2",
            "-e", enumValue
        };

        var options = TestOptions.Parse(args);

        Assert.Equal(TestEnum.Option1, options.Enum);
    }
}