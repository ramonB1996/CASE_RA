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

                context.Add(newCourse);
                context.Add(newInstance);
                context.SaveChanges();

                var result = repository.GetAll();

                Assert.Single(result);
                Assert.Single(context.Courses);
                Assert.Single(context.CourseInstances);
                Assert.Equal(newInstance.Id, result.First().Id);
                Assert.Equal(newInstance.CourseId, result.First().CourseId);
                Assert.Equal(newInstance.StartDate, result.First().StartDate);
                Assert.Equal(newInstance.Course.Code, result.First().Course.Code);
                Assert.Equal(newInstance.Course.Duration, result.First().Course.Duration);
                Assert.Equal(newInstance.Course.Title, result.First().Course.Title);
                Assert.Equal(newInstance.Course.Id, result.First().Course.Id);
            }
        }

        [Fact]
        public void GetByStartDateAndCourseId_Returns_Instance()
        {
            using (CourseContext context = new CourseContext(CreateContextOptions()))
            {
                ICourseInstanceRepository repository = new CourseInstanceRepository(context);
                Course newCourse = new Course() { Id = 1, Code = "CNETIN", Title = "Programming with C#", Duration = 5 };
                CourseInstance newInstance = new CourseInstance() { Id = 1, CourseId = 1, StartDate = DateOnly.Parse("18/10/2020") };

                context.Add(newCourse);
                context.Add(newInstance);
                context.SaveChanges();

                var result = repository.GetByStartDateAndCourseId(newInstance.StartDate, newCourse.Id);

                Assert.NotNull(result);
                Assert.Equal(newInstance, result);
                Assert.Single(context.Courses);
                Assert.Single(context.CourseInstances);
            }
        }

        [Fact]
        public void GetByStartDateAndCourseId_Returns_Null()
        {
            using (CourseContext context = new CourseContext(CreateContextOptions()))
            {
                ICourseInstanceRepository repository = new CourseInstanceRepository(context);
                Course newCourse = new Course() { Id = 1, Code = "CNETIN", Title = "Programming with C#", Duration = 5 };
                CourseInstance newInstance = new CourseInstance() { Id = 1, CourseId = 1, StartDate = DateOnly.Parse("18/10/2020") };

                context.Add(newCourse);
                context.Add(newInstance);
                context.SaveChanges();

                var result = repository.GetByStartDateAndCourseId(DateOnly.Parse("19/10/2020"), newCourse.Id);

                Assert.Null(result);
                Assert.Single(context.Courses);
                Assert.Single(context.CourseInstances);
            }
        }

        [Fact]
        public void Add_Returns_NewInstance()
        {
            using (CourseContext context = new CourseContext(CreateContextOptions()))
            {
                ICourseInstanceRepository repository = new CourseInstanceRepository(context);
                CourseInstance newInstance = new CourseInstance() { Id = 1, CourseId = 1, StartDate = DateOnly.Parse("18/10/2020") };

                var result = repository.Add(newInstance);

                Assert.Single(context.CourseInstances);
                Assert.Equal(1, result.Id);
                Assert.Equal(DateOnly.Parse("18/10/2020"), result.StartDate);
                Assert.Equal(1, result.CourseId);
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

