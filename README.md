# csharp-to-typescript-converter
This is a C# console application that converts a C# object definition into its TypeScript interface equivalent.

Features:
- Converts C# class definitions to TypeScript interfaces
- Supports:
  - `string`, `int`, and `long` (including nullable variants)
  - `List<T>` for supported types
  - Single-level nested classes
- Outputs properly formatted and camel-cased TypeScript code
- Reads input from a `.cs` file and writes the result to a `.ts` file
- Easily extensible for additional types (e.g. `double`, `decimal`, `bool`)

Assumptions:
1. A class property can only be a `string`, an `int`, a `long`, a nullable of these types, a list of these types, or a nested class consisting of these data types.
2. All class members are public.
3. Only one level of class nesting is allowed.
4. The definition of the nested class is always at the end of the definition of the parent class.
5. No empty lines are presented in the class definition.

**NOTE:** The converter reads the input from input.cs file only for convenience.
The file is excluded from compilation, so it can contain any text.
The converter itself operates on strings and can easily be tested with string inputs directly, without reading from files.

## How to run
By default, the converter reads `input.cs` from the current folder and writes `output.ts` next to it.

```bash
cd CSharpToTsConverter
dotnet run
```

You can also specify custom paths:
```bash
dotnet run --project CSharpToTsConverter -- path/to/input.cs path/to/output.ts
```

## Running tests
Unit tests are written using xUnit.
```bash
dotnet test
