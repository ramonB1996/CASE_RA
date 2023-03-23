using System;
using WebApi.Domain;
using WebApi.Domain.DTO;
using WebApi.Repositories;

namespace WebApi.Services
{
	public class CourseService : ICourseService
	{ 
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseInstanceService _courseInstanceService;

        public CourseService(ICourseRepository courseRepository, ICourseInstanceService courseInstanceService)
        {
            _courseRepository = courseRepository;
            _courseInstanceService = courseInstanceService;
        }

		public CourseAndInstancesDTO ProcessCoursesToDTO(IEnumerable<Course> courses)
		{
            CourseAndInstancesDTO result = new CourseAndInstancesDTO();

            foreach (Course potentialNewCourse in courses)
            {
                // Make sure to not add courseInstances when adding the course.
                List<CourseInstance> potentialNewCourseInstances = new List<CourseInstance>(potentialNewCourse.CourseInstances);
                potentialNewCourse.CourseInstances.Clear();

                Course? course = _courseRepository.GetByCode(potentialNewCourse.Code);

                if (course == null)
                {
                    course = _courseRepository.Add(potentialNewCourse);

                    result.Courses.Add(course);
                }

                foreach (CourseInstance potentialNewCourseInstance in potentialNewCourseInstances)
                {
                    CourseInstance? newCourseInstance = _courseInstanceService.ProcessNewCourseInstance(potentialNewCourseInstance, course.Id);

                    if (newCourseInstance != null)
                    {
                        result.CourseInstances.Add(newCourseInstance);
                    }
                }
            }

            return result;
        }
	}
}

