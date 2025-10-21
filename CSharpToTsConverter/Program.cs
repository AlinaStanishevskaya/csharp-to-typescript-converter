
var inputFilePath = args.Length > 0 ? args[0] : "input.cs";
var outputFilePath = args.Length > 1 ? args[1] : "output.ts";

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