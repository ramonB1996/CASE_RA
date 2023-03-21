using System;
using WebApi.Domain;

namespace WebApi.Repositories
{
	public interface ICourseRepository
	{
		IEnumerable<Course> GetAll();

		IEnumerable<Course> AddRange(IEnumerable<Course> courses);
    }
}

