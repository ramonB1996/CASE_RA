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

    [Fact]
    public async Task ParseFileToCourseAsync_CorrectInput_Returns_ListOfCourses()
    {
        IFormFile file = CreateMockFile("Titel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 8/10/2018\n\nTitel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 15/10/2018\n\nTitel: Java Persistence API\nCursuscode: JPA\nDuur: 2 dagen\nStartdatum: 15/10/2018\n\nTitel: Java Persistence API\nCursuscode: JPA\nDuur: 2 dagen\nStartdatum: 8/10/2018\n\nTitel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 8/10/2018\n\n");

        IEnumerable<Course> result = await FileParser.ParseFileToCoursesAsync(file);

        List<Course> expected = new List<Course>()
        {
            new Course()
            {
                Title = "C# Programmeren",
                Code = "CNETIN",
                Duration = 5,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        StartDate = DateOnly.Parse("8/10/2018")
                    }
                }
            },
            new Course()
            {
                Title = "C# Programmeren",
                Code = "CNETIN",
                Duration = 5,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        StartDate = DateOnly.Parse("15/10/2018")
                    }
                }
            },
            new Course()
            {
                Title = "Java Persistence API",
                Code = "JPA",
                Duration = 2,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        StartDate = DateOnly.Parse("15/10/2018")
                    }
                }
            },
            new Course()
            {
                Title = "Java Persistence API",
                Code = "JPA",
                Duration = 2,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        StartDate = DateOnly.Parse("8/10/2018")
                    }
                }
            },
            new Course()
            {
                Title = "C# Programmeren",
                Code = "CNETIN",
                Duration = 5,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        StartDate = DateOnly.Parse("8/10/2018")
                    }
                }
            },
        };

        Assert.Equal(expected.Count(), result.Count());
        Assert.Collection(result,
            elem1 =>
            {
                Assert.Equal(expected[0].Title, elem1.Title);
                Assert.Equal(expected[0].Code, elem1.Code);
                Assert.Equal(expected[0].Duration, elem1.Duration);
                Assert.Equal(expected[0].CourseInstances[0].StartDate, elem1.CourseInstances[0].StartDate);
            },
            elem2 =>
            {
                Assert.Equal(expected[1].Title, elem2.Title);
                Assert.Equal(expected[1].Code, elem2.Code);
                Assert.Equal(expected[1].Duration, elem2.Duration);
                Assert.Equal(expected[1].CourseInstances[0].StartDate, elem2.CourseInstances[0].StartDate);
            },
            elem3 =>
            {
                Assert.Equal(expected[2].Title, elem3.Title);
                Assert.Equal(expected[2].Code, elem3.Code);
                Assert.Equal(expected[2].Duration, elem3.Duration);
                Assert.Equal(expected[2].CourseInstances[0].StartDate, elem3.CourseInstances[0].StartDate);
            },
            elem4 =>
            {
                Assert.Equal(expected[3].Title, elem4.Title);
                Assert.Equal(expected[3].Code, elem4.Code);
                Assert.Equal(expected[3].Duration, elem4.Duration);
                Assert.Equal(expected[3].CourseInstances[0].StartDate, elem4.CourseInstances[0].StartDate);
            },
            elem5 =>
            {
                Assert.Equal(expected[4].Title, elem5.Title);
                Assert.Equal(expected[4].Code, elem5.Code);
                Assert.Equal(expected[4].Duration, elem5.Duration);
                Assert.Equal(expected[4].CourseInstances[0].StartDate, elem5.CourseInstances[0].StartDate);
            }
        );
    }
}
