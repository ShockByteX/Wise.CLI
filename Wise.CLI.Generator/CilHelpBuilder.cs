using System.Text;

namespace Wise.CLI.Generator;

internal sealed class CilHelpBuilder
{
    private const int IndentSize = 2;

    private const string HelpConditionTemplate = """


                                                         if (args.Length is 1 && args[0] is "--help")
                                                         {
                                                             PrintHelp();
                                                             Environment.Exit(0);
                                                         }

                                                 """;

    private const string PrintHelpAppendLineTemplate = "        builder.AppendLine(\"{0}{1}{2}\");";
    private const string PrintHelpTemplate =
        """

            public static void PrintHelp()
            {{
                var builder = new StringBuilder();
                builder.AppendLine($"Usage: {{AppDomain.CurrentDomain.FriendlyName}} [options]\n");
                builder.AppendLine("Options:");
        {1}
                Console.WriteLine(builder.ToString());
            }}
        """;

    private readonly StringBuilder _builder  = new();

    private readonly bool _generateHelp;
    private readonly int _definitionPadSize;
    private readonly int _typeDefinitionPadSize;

    public CilHelpBuilder(IReadOnlyCollection<CilArgumentInfo> arguments, bool generateHelp)
    {
        _generateHelp = generateHelp;

        if (generateHelp)
        {
            _definitionPadSize = arguments.Max(options => options.Definition.Length) + IndentSize;
            _typeDefinitionPadSize = arguments.Max(options => options.TypeDefinition.Length) + IndentSize;
        }
    }

    public void AppendArgument(CilArgumentInfo argument)
    {
        if (!_generateHelp) return;

        _builder.AppendLine(string.Format(PrintHelpAppendLineTemplate,
            argument.Definition.PadRight(_definitionPadSize),
            argument.TypeDefinition.PadRight(_typeDefinitionPadSize),
            argument.Description));
    }

    public (string helpCondition, string printHelp) Build()
    {
        return _generateHelp
            ? (HelpConditionTemplate, string.Format(PrintHelpTemplate, _builder))
            : (string.Empty, string.Empty);
    }
}