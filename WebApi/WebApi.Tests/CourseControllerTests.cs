namespace WebApi.Tests
{
	public class CourseControllerTests
	{
		private readonly Mock<ICourseRepository> _mockCourseRepository;
		private readonly Mock<ICourseInstanceRepository> _mockCourseInstanceRepository;
		private readonly Mock<IFileParser> _mockFileParser;
		private CourseController _controller;

		public CourseControllerTests()
		{
            _mockCourseRepository = new Mock<ICourseRepository>();
			_mockCourseInstanceRepository = new Mock<ICourseInstanceRepository>();
			_mockFileParser = new Mock<IFileParser>();

			_controller = new CourseController(_mockCourseRepository.Object, _mockCourseInstanceRepository.Object, _mockFileParser.Object);
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
			
        }
    }
}

