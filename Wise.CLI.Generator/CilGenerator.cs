using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace Wise.CLI.Generator;

[Generator]
public sealed class WiseCliGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var cliClasses = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: static (context, _) => (ClassDeclarationSyntax)context.Node)
            .Where(static declaration => declaration.AttributeLists
                .SelectMany(list => list.Attributes)
                .Any(attribute => attribute.Name.ToString().Contains("CliOptions")));

        var combined = context.CompilationProvider.Combine(cliClasses.Collect());

        context.RegisterSourceOutput(combined, Generate);
    }

    private static void Generate(SourceProductionContext context, (Compilation compilation, ImmutableArray<ClassDeclarationSyntax> syntax) source)
    {
        var (compilation, declarations) = source;

        foreach (var declaration in declarations)
        {
            var semanticModel = compilation.GetSemanticModel(declaration.SyntaxTree);

            if (semanticModel.GetDeclaredSymbol(declaration) is not INamedTypeSymbol classSymbol)
                continue;

            var attribute = classSymbol.GetAttributes()
                .FirstOrDefault(a => a.AttributeClass?.Name.Equals(nameof(CliOptionsAttribute)) == true);

            if (attribute == null) continue;

            var generateHelp = attribute.GetArgumentValue<bool>(nameof(CliOptionsAttribute.GenerateHelp));
            var ns = classSymbol.ContainingNamespace.ToDisplayString();
            var className = classSymbol.Name;
            var builder = new CilTemplateBuilder(ns, className);

            foreach (var property in classSymbol.GetMembers().OfType<IPropertySymbol>())
                builder.AddProperty(property);

            context.AddSource($"{className}.g.cs", builder.Build(generateHelp));
        }
    }
}