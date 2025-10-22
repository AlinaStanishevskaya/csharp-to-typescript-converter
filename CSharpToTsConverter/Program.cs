// NOTE: The converter reads the input from input.cs file only for convenience.
// The file is excluded from compilation, so it can contain any text.
// The converter itself operates on strings and can easily be tested with
// string inputs directly, without reading from files.

var defaultInput = Path.Combine("Samples", "input.cs");
var defaultOutput = Path.Combine("Samples", "output.ts");

var inputFilePath = args.Length > 0 ? args[0] : defaultInput;
var outputFilePath = args.Length > 1 ? args[1] : defaultOutput;

if (!File.Exists(inputFilePath))
{
	Console.WriteLine($"Input file not found: {inputFilePath}");
	return;
}

var input = File.ReadAllText(inputFilePath);

var converter = new CSharpToTypeScriptConverter();
var result = converter.Convert(input);

File.WriteAllText(outputFilePath, result);

Console.WriteLine($"TypeScript definitions written to: {outputFilePath}");
Console.WriteLine(result);