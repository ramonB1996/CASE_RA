using System;
using WebApi.DAL;
using WebApi.Domain;

namespace WebApi.Repositories
{
	public class CourseInstanceRepository : ICourseInstanceRepository
	{
		private readonly CourseContext _context;

		public CourseInstanceRepository(CourseContext context)
		{
			_context = context;
		}

		public IEnumerable<CourseInstance> GetAll()
		{
			return _context.CourseInstances.ToList();
		}
	}
}

