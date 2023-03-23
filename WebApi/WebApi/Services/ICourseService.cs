using System;
using WebApi.Domain;
using WebApi.Domain.DTO;

namespace WebApi.Services
{
	public interface ICourseService
	{
        CourseAndInstancesDTO ProcessCoursesToDTO(IEnumerable<Course> courses);
    }
}

