using System;
using Microsoft.EntityFrameworkCore;
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
			return _context.CourseInstances.Include(x => x.Course).AsNoTracking().ToList();
		}

		public CourseInstance? GetByStartDateAndCourseId(DateOnly startDate, int courseId)
		{
			return _context.CourseInstances.FirstOrDefault(x => x.StartDate == startDate && x.CourseId == courseId);
		}

		public CourseInstance Add(CourseInstance newCourseInstance)
		{
			_context.Add(newCourseInstance);
			_context.SaveChanges();

			return newCourseInstance;
		}
	}
}

