using System;
using WebApi.Domain;

namespace WebApi.Repositories
{
	public interface ICourseInstanceRepository
	{
		IEnumerable<CourseInstance> GetAllForDateRange(DateOnly startDate, DateOnly endDate);

		CourseInstance? GetByStartDateAndCourseId(DateOnly startDate, int courseId);

		CourseInstance Add(CourseInstance newCourseInstance);
    }
}

