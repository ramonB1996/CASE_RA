namespace WebApi.Tests
{
	public class CourseControllerTests
	{
		private readonly Mock<ICourseService> _mockCourseService;
		private readonly Mock<IFileParser> _mockFileParser;
		private CourseController _controller;

		public CourseControllerTests()
		{
			_mockCourseService = new Mock<ICourseService>();
			_mockFileParser = new Mock<IFileParser>();

			_controller = new CourseController(_mockCourseService.Object, _mockFileParser.Object);
        }

		[Fact]
		public async Task PostAsync_Wrong_MimeType_Returns_BadRequest()
		{
			IFormFile input = FormFileMocker.CreateMockFile("example content", "application/json");

			var result = await _controller.PostAsync(input);

			Assert.IsType<BadRequestObjectResult>(result);
        }

		[Fact]
		public async Task PostAsync_Input_Empty_Returns_BadRequest()
		{
            IFormFile input = FormFileMocker.CreateMockFile(string.Empty);

            var result = await _controller.PostAsync(input);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostAsync_Input_Null_Returns_BadRequest()
        {
            var result = await _controller.PostAsync(null!);

            Assert.IsType<BadRequestObjectResult>(result);
        }

		[Fact]
		public async Task PostAsync_CorrectInput_Returns_CorrectDTO()
		{
			IFormFile mockFile = FormFileMocker.CreateMockFile("Titel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 8/10/2018\n\nTitel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 15/10/2018\n\nTitel: Java Persistence API\nCursuscode: JPA\nDuur: 2 dagen\nStartdatum: 15/10/2018\n\nTitel: Java Persistence API\nCursuscode: JPA\nDuur: 2 dagen\nStartdatum: 8/10/2018\n\nTitel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 8/10/2018\n\n");

			using (var context = new CourseContext(InMemoryDb.CreateContextOptions()))
			{
				ICourseInstanceRepository instanceRepository = new CourseInstanceRepository(context);
				ICourseInstanceService instanceService = new CourseInstanceService(instanceRepository);
				ICourseRepository courseRepository = new CourseRepository(context);
				ICourseService courseService = new CourseService(courseRepository, instanceService);
				IFileParser fileParser = new FileParser();

				CourseController controller = new CourseController(courseService, fileParser);

                var actionResult = await controller.PostAsync(mockFile);
                var okObjectResult = actionResult as OkObjectResult;
                var result = okObjectResult!.Value as CourseAndInstancesDTO;

				Assert.Equal(2, context.Courses.Count());
				Assert.Equal(2, result!.Courses.Count);
				Assert.Equal(4, result!.CourseInstances.Count());
				Assert.Equal(4, result!.CourseInstances.Count);
            }
		}

		[Fact]
		public async Task PostAsync_Calls_CorrectMethods()
		{
            IFormFile mockFile = FormFileMocker.CreateMockFile("Titel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 8/10/2018\n\nTitel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 15/10/2018\n\nTitel: Java Persistence API\nCursuscode: JPA\nDuur: 2 dagen\nStartdatum: 15/10/2018\n\nTitel: Java Persistence API\nCursuscode: JPA\nDuur: 2 dagen\nStartdatum: 8/10/2018\n\nTitel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 8/10/2018\n\n");

			await _controller.PostAsync(mockFile);

			_mockFileParser.Verify(x => x.ParseFileToCoursesAsync(mockFile), Times.Exactly(1));
			_mockCourseService.Verify(x => x.ProcessCoursesToDTO(It.IsAny<IEnumerable<Course>>()), Times.Exactly(1));
		}
    }
}

