using System;
using WebApi.Domain;
using WebApi.Repositories;

namespace WebApi.Services
{
	public class CourseInstanceService : ICourseInstanceService
	{
		private readonly ICourseInstanceRepository _courseInstanceRepository;

        public CourseInstanceService(ICourseInstanceRepository courseInstanceRepository)
		{
			_courseInstanceRepository = courseInstanceRepository;
		}

		public CourseInstance? ProcessNewCourseInstance(CourseInstance potentialNewCourseInstance, int courseId)
		{
            CourseInstance? courseInstance = _courseInstanceRepository.GetByStartDateAndCourseId(potentialNewCourseInstance.StartDate, courseId);

            if (courseInstance == null)
            {
                potentialNewCourseInstance.CourseId = courseId;
                courseInstance = _courseInstanceRepository.Add(potentialNewCourseInstance);

				return courseInstance;
            }

			return null;
        }
	}
}

