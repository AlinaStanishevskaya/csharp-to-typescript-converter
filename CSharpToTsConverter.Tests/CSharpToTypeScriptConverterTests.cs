using Xunit;
namespace CSharpToTsConverter.Tests;

public class CSharpToTypeScriptConverterTests
{
	private readonly CSharpToTypeScriptConverter _converter;

	public CSharpToTypeScriptConverterTests()
	{
		_converter = new CSharpToTypeScriptConverter();
	}

	[Fact]
	public void Convert_SimpleClass_ReturnsExpectedTypeScript()
	{
		string input = @"
public class PersonDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public long? DriverLicenceNumber { get; set; }
}";

		string expectedOutput = @"export interface PersonDto {
    name: string;
    age: number;
    driverLicenceNumber?: number;
}";

		var actual = _converter.Convert(input).Trim();

		Assert.Equal(expectedOutput, actual);
	}

	[Fact]
	public void Convert_ClassWithNested_ReturnsNestedTypeScript()
	{
		string input = @"
public class PersonDto
{
	public string Name { get; set; }
	public List<Address> Addresses { get; set; }
	public class Address
	{
		public string Street { get; set; }
	}
}";

		string expectedOutput = @"export interface PersonDto {
    name: string;
    addresses: Address[];
}
export interface Address {
    street: string;
}";

		var actual = _converter.Convert(input).Trim();

		Assert.Equal(expectedOutput, actual);
	}

	[Fact]
	public void Convert_NullInput_ReturnsEmptyString()
	{
		string? input = null;

		string expectedOutput = "";

		var actual = _converter.Convert(input).Trim();

		Assert.Equal(expectedOutput, actual);
	}

	[Fact]
	public void Convert_NotCSharpInputString_ReturnsEmptyString()
	{
		string? input = "Just some random text that is not C# code.";

		string expectedOutput = "";

		var actual = _converter.Convert(input).Trim();

		Assert.Equal(expectedOutput, actual);
	}
}