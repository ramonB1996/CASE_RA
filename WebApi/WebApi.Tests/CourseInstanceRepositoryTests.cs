using System;

namespace WebApi.Tests
{
	public class CourseInstanceRepositoryTests
	{
        [Fact]
        public void GetAll_Returns_AllInstances()
        {
            using (CourseContext context = new CourseContext(CreateContextOptions()))
            {
                ICourseInstanceRepository repository = new CourseInstanceRepository(context);
                Course newCourse = new Course() { Id = 1, Code = "CNETIN", Title = "Programming with C#", Duration = 5 };
                CourseInstance newInstance = new CourseInstance() { Id = 1, CourseId = 1, StartDate = DateOnly.Parse("18/10/2020") };

                context.Add(newInstance);
                context.SaveChanges();

                var result = repository.GetAll();

                Assert.Single(result);
                Assert.Equal(newInstance, result.First());
            }
        }

        private DbContextOptions<CourseContext> CreateContextOptions()
        {
            return new DbContextOptionsBuilder<CourseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}

