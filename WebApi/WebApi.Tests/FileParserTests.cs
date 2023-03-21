namespace WebApi.Tests;

public class FileParserTests
{
    private IFormFile CreateMockFile(string fileContent)
    {
        var content = fileContent;
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        return new FormFile(stream, 0, stream.Length, string.Empty, "file.txt");
    }

    [Fact]
    public async Task ParseFileToCoursesAsync_WrongOrder_Throws_FileFormatException()
    {
        IFormFile file = CreateMockFile("Titel: C# Programmeren\nDuur: 5 dagen\nCursuscode: CNETIN\nStartdatum: 8/10/2018\n\n");

        await Assert.ThrowsAsync<FileFormatException>(() => FileParser.ParseFileToCoursesAsync(file));
    }

    [Fact]
    public async Task ParseFileToCoursesAsync_NotEnoughData_Throws_FileFormatException()
    {
        IFormFile file = CreateMockFile("Titel: C# Programmeren\nDuur: 5 dagen\nCursuscode: CNETIN\nStartdatum: 8/10/2018\n\n");

        await Assert.ThrowsAsync<FileFormatException>(() => FileParser.ParseFileToCoursesAsync(file));
    }

    [Fact]
    public async Task ParseFileToCoursesAsync_WrongStartDateFormat_Throws_FormatException()
    {
        IFormFile file = CreateMockFile("Titel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 8-10-2018\n\n");

        await Assert.ThrowsAsync<FormatException>(() => FileParser.ParseFileToCoursesAsync(file));
    }

    [Fact]
    public async Task ParseFileToCoursesAsync_WrongDurationFormat_Throws_FileFormatException()
    {
        IFormFile file = CreateMockFile("Titel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5\nStartdatum: 8/10/2018\n\n");

        await Assert.ThrowsAsync<FileFormatException>(() => FileParser.ParseFileToCoursesAsync(file));
    }

    [Fact]
    public async Task ParseFileToCoursesAsync_NoTrailingEmptyLine_BetweenEntities_Throws_FileFormatException()
    {
        IFormFile file = CreateMockFile("Titel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 8/10/2018\nTitel: Java Persistence API\nCursuscode: JPA\nDuur: 2 dagen\nStartdatum: 10/10/2018\n\n");

        await Assert.ThrowsAsync<FileFormatException>(() => FileParser.ParseFileToCoursesAsync(file));
    }
}
