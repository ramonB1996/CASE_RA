using System;
namespace WebApi.Tests
{
	public static class InMemoryDb
	{
        public static DbContextOptions<CourseContext> CreateContextOptions()
        {
            return new DbContextOptionsBuilder<CourseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}

