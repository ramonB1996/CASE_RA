using System;
namespace WebApi.Domain.DTO
{
	public class CourseAndInstancesDTO
	{
		public List<Course> Courses { get; set; } = new List<Course>();

		public List<CourseInstance> CourseInstances { get; set; } = new List<CourseInstance>();

		public int DuplicateCourses { get; set; } = 0;

		public int DuplicateCourseInstances { get; set; } = 0;
	}
}

