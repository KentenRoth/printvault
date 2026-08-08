using PrintVault.Backend.Services;

namespace PrintVault.Tests.Services;

public class MfParserServiceTests
{
    private readonly MfParserService _service;
    private readonly string _testFilePath;

    public MfParserServiceTests()
    {
        _service = new MfParserService();
        _testFilePath = Path.Combine(AppContext.BaseDirectory, "TestData", "Post-it_holder_and_templates_V2.3mf");
    }

    [Fact]
    public void Parse_ValidFile_ReturnsCorrectTitle()
    {
        var result = _service.Parse(_testFilePath);
        Assert.Equal("Calendar and list stencil for Post-it Notes ", result.Title);
    }

    [Fact]
    public void Parse_ValidFile_ReturnsCorrectCreationDate()
    {
        var result = _service.Parse(_testFilePath);
        Assert.Equal("2025-03-15", result.CreationDate);
    }

    [Fact]
    public void Parse_ValidFile_ReturnsCorrectNumberOfPlates()
    {
        var result = _service.Parse(_testFilePath);
        Assert.Equal(2, result.NumberOfPlates);
    }

    [Fact]
    public void Parse_ValidFile_ReturnsTotalFilamentUsed()
    {
        var result = _service.Parse(_testFilePath);
        Assert.Equal(121.14, result.TotalFilamentUsed, precision: 2);
    }

    [Fact]
    public void Parse_ValidFile_ReturnsTotalPrintTimeSeconds()
    {
        var result = _service.Parse(_testFilePath);
        Assert.Equal(15441, result.TotalPrintTimeSeconds);
    }

    [Fact]
    public void Parse_ValidFile_ReturnsCorrectPlateCount()
    {
        var result = _service.Parse(_testFilePath);
        Assert.Equal(2, result.Plates.Count);
    }

    [Fact]
    public void Parse_ValidFile_FirstPlateHasCorrectFilament()
    {
        var result = _service.Parse(_testFilePath);
        Assert.Equal(91.96, result.Plates[0].FilamentUsed, precision: 2);
    }

    [Fact]
    public void Parse_ValidFile_SecondPlateHasCorrectPrintTime()
    {
        var result = _service.Parse(_testFilePath);
        Assert.Equal(4032, result.Plates[1].PrintTimeSeconds);
    }

    [Fact]
    public void Parse_FileDoesNotExist_ThrowsException()
    {
        Assert.Throws<DirectoryNotFoundException>(() => 
            _service.Parse("/nonexistent/path/file.3mf"));
    }
}