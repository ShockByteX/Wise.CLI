using System;

namespace Wise.CLI.Generator;

[AttributeUsage(AttributeTargets.Class)]
public class CliOptionsAttribute(bool generateHelp = true) : Attribute
{
    public bool GenerateHelp { get; } = generateHelp;
};