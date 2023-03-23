using System;
using WebApi.Domain;

namespace WebApi.Services
{
	public interface ICourseInstanceService
	{
        CourseInstance? ProcessNewCourseInstance(CourseInstance potentialNewCourseInstance, int courseId);
    }
}

