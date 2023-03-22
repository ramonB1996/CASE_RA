using System;
using WebApi.Domain;

namespace WebApi.Repositories
{
	public interface ICourseRepository
	{
		IEnumerable<Course> GetAll();

        Course? GetByCode(string code);

		Course Add(Course newCourse);
	}
}

