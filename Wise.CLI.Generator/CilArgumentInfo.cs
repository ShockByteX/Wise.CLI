using Microsoft.CodeAnalysis;
using System.Text;

namespace Wise.CLI.Generator;

internal sealed class CilArgumentInfo(IPropertySymbol property, string name, char alias, bool required, string? description)
{
    public IPropertySymbol Property { get; } = property;
    public string Name { get; } = name;
    public char Alias { get; } = alias;
    public bool Required { get; } = required;
    public string? Description { get; } = description;
    public string Definition { get; } = CreateDefinition(name, alias, required);
    public string TypeDefinition { get; } = $"<{property.Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)}>";

    private static string CreateDefinition(string name, char alias, bool required)
    {
        var builder = new StringBuilder();

        if (!required)
            builder.Append('[');

        if (alias > 0)
            builder.Append($"-{alias}, ");

        builder.Append($"--{name}");

        if (!required)
            builder.Append(']');

        return builder.ToString();
    }
}