namespace Wise.CLI;

[AttributeUsage(AttributeTargets.Class)]
public class CliOptionsAttribute(bool generateHelp = true) : Attribute
{
    public bool GenerateHelp { get; } = generateHelp;
};