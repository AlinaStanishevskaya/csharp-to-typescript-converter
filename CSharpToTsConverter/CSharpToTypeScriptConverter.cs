using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class CSharpToTypeScriptConverter
{
	// Maps C# primitive types (as defined in the assumptions) to TypeScript equivalents
	private static readonly Dictionary<string, string> TypeMap = new()
	{
		["string"] = "string",
		["int"] = "number",
		["long"] = "number",

		// The converter can be easily extended to support more types:
		// ["double"] = "number",
		// ["decimal"] = "number",
		// ["bool"] = "boolean"
	};

	public string Convert(string input)
	{
		if (string.IsNullOrWhiteSpace(input))
		{
			return string.Empty;
		}

		var syntaxTree = CSharpSyntaxTree.ParseText(input);
		var root = syntaxTree.GetRoot();

		// Collect all class declarations (parent + nested)
		var classNodes = root.DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();

		var sb = new StringBuilder();

		foreach (var classNode in classNodes)
		{
			sb.AppendLine(ConvertClass(classNode).Trim());
		}

		return sb.ToString().TrimEnd();
	}

	private string ConvertClass(ClassDeclarationSyntax classNode)
	{
		var sb = new StringBuilder();
		sb.AppendLine($"export interface {classNode.Identifier.Text} {{");

		foreach (var prop in classNode.Members.OfType<PropertyDeclarationSyntax>())
		{
			string propName = prop.Identifier.Text.ToCamelCase();
			string propType = ConvertType(prop.Type.ToString());
			string optionalMark = prop.Type.ToString().EndsWith("?") ? "?" : "";

			sb.AppendLine($"    {propName}{optionalMark}: {propType};");
		}

		sb.AppendLine("}");
		return sb.ToString();
	}

	private string ConvertType(string csharpType)
	{
		var typeSyntax = SyntaxFactory.ParseTypeName(csharpType);

		if (typeSyntax is GenericNameSyntax g && g.Identifier.Text == "List")
		{
			var inner = g.TypeArgumentList.Arguments.First().ToString();
			return $"{ConvertType(inner)}[]";
		}

		var nonNullableType = csharpType.TrimEnd('?');

		if (TypeMap.TryGetValue(nonNullableType, out var ts))
		{
			return ts;
		}

		return nonNullableType;
	}
}