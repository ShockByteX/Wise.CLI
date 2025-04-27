namespace Wise.CLI;

[AttributeUsage(AttributeTargets.Property)]
public class CliOptionAttribute : Attribute
{
    public CliOptionAttribute(string name, string? description = null, bool required = false)
    {
        Name = name;
        Description = description;
        Required = required;
    }

    public CliOptionAttribute(string name, char alias, string? description = null, bool required = false)
    {
        Name = name;
        Alias = alias;
        Description = description;
        Required = required;
    }

    public string Name { get; }
    public char? Alias { get; }
    public string? Description { get; }
    public bool Required { get; }
}