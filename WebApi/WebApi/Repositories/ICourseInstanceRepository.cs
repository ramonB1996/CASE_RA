using System;
using WebApi.Domain;

namespace WebApi.Repositories
{
	public interface ICourseInstanceRepository
	{
		IEnumerable<CourseInstance> GetAll();

		CourseInstance? GetByStartDateAndCourseId(DateOnly startDate, int courseId);

		CourseInstance Add(CourseInstance newCourseInstance);
    }
}

