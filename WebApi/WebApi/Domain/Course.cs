using System;
namespace WebApi.Domain
{
	public class Course
	{
		public int Id { get; set; }

		public int Duration { get; set; }

		public string Title { get; set; }

		public string Code { get; set; }

		public List<CourseInstance> CourseInstances { get; set; }
	}
}

