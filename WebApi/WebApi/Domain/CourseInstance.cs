using System;
namespace WebApi.Domain
{
	public class CourseInstance
	{
		public int Id { get; set; }

		public DateOnly StartDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
	}
}

