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

		public IEnumerable<CourseInstance> GetAllForDateRange(DateOnly startDate, DateOnly endDate)
		{
			return _context.CourseInstances
				.Where(x => x.StartDate >= startDate && x.StartDate <= endDate)
				.Include(x => x.Course)
				.OrderBy(x => x.StartDate)
				.AsNoTracking()
				.ToList();
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

