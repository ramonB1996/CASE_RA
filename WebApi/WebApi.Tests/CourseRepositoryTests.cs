using System;
namespace WebApi.Tests
{
	public class CourseRepositoryTests
	{
        [Fact]
        public void GetByCode_Returns_Instance()
        {
            using (CourseContext context = new CourseContext(InMemoryDb.CreateContextOptions()))
            {
                ICourseRepository repository = new CourseRepository(context);
                Course newCourse = new Course() { Id = 1, Code = "CNETIN", Title = "Programming with C#", Duration = 5 };

                context.Add(newCourse);
                context.SaveChanges();

                var result = repository.GetByCode("CNETIN");

                Assert.Single(context.Courses);
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
                Assert.Equal("CNETIN", result.Code);
                Assert.Equal("Programming with C#", result.Title);
                Assert.Equal(5, result.Duration);
            }
        }

        [Fact]
        public void GetByCode_Returns_Null()
        {
            using (CourseContext context = new CourseContext(InMemoryDb.CreateContextOptions()))
            {
                ICourseRepository repository = new CourseRepository(context);
                Course newCourse = new Course() { Id = 1, Code = "CNETIN", Title = "Programming with C#", Duration = 5 };

                context.Add(newCourse);
                context.SaveChanges();

                var result = repository.GetByCode("CNETUIT");

                Assert.Single(context.Courses);
                Assert.Null(result);
            }
        }

        [Fact]
        public void Add_Returns_NewInstance()
        {
            using (CourseContext context = new CourseContext(InMemoryDb.CreateContextOptions()))
            {
                ICourseRepository repository = new CourseRepository(context);
                Course newCourse = new Course() { Id = 1, Code = "CNETIN", Title = "Programming with C#", Duration = 5 };

                var result = repository.Add(newCourse);

                Assert.Single(context.Courses);
                Assert.Equal(1, result.Id);
                Assert.Equal("CNETIN", result.Code);
                Assert.Equal("Programming with C#", result.Title);
                Assert.Equal(5, result.Duration);
            }
        }
    }
}

