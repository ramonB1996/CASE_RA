namespace WebApi.Tests
{
	public class CourseControllerTests
	{
		private CourseController _controller;

		public CourseControllerTests()
		{
            var mockCourseRepository = Mock.Of<ICourseRepository>();
			var mockCourseInstanceRepository = Mock.Of<ICourseInstanceRepository>();

			_controller = new CourseController(mockCourseRepository, mockCourseInstanceRepository);
        }

		[Fact]
		public void CourseController_Get_Returns_AllCourses()
		{
			var result = _controller.Get();

			Assert.IsType<ActionResult<IEnumerable<Course>>>(result); 
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
    }
}

