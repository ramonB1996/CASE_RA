using System;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Domain;

namespace WebApi.Repositories
{
	public class CourseRepository : ICourseRepository
	{
		private readonly CourseContext _context;

		public CourseRepository(CourseContext context)
		{
			_context = context;
		}

		public IEnumerable<Course> GetAll()
		{
			return _context.Courses.Include(x => x.CourseInstances).ToList();
		}
	}
}

