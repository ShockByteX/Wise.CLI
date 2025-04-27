using Microsoft.CodeAnalysis;

namespace Wise.CLI.Generator;

internal static class AttributeDataExtension
{
    public static T? GetArgumentValue<T>(this AttributeData data, string name)
    {
        if (data.NamedArguments.FirstOrDefault(pair => pair.Key.Equals(name, StringComparison.Ordinal)) is { Value.Value: T namedValue })
            return namedValue;

        if (data.AttributeConstructor is null)
            return default;

        var parameters = data.AttributeConstructor.Parameters;

        for (int i = 0; i < parameters.Length && i < data.ConstructorArguments.Length; i++)
        {
            if (parameters[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase) && data.ConstructorArguments[i].Value is T positionalValue)
                return positionalValue;
        }

        return default;
    }
}