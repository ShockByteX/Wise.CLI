# Wise.CLI - Command Line Parser Library
## Advantages

- ⚡ **Lightweight and Minimalistic**  
  No unnecessary dependencies or bloat. Focused purely on parsing command-line arguments with maximum efficiency.

- 🛠️ **Code-Generation instead of Reflection**  
  Generates parsing code at compile-time via Source Generators.  Parser is as fast as hand-written code.

- 🚀 **AOT-Safe**  
  Fully compatible with Native AOT scenarios. No hidden traps, no runtime issues.

- 📖 **Auto-Generated Help**  
  Generates clean `--help` output.

- 📝 **Aliases and Required Arguments**  
  Supports **short aliases** (`-f`) and **required argument validation** out of the box.

---

## Example

```csharp
[CliOptions(GenerateHelp = true)]
public partial class AppOptions
{
    [CliOption("file", 'f', Required = true, Description = "Path to the input file")]
    public string FilePath { get; set; } = default!;

    [CliOption("mode", 'm', Description = "Operating mode")]
    public ModeType Mode { get; set; }
}
