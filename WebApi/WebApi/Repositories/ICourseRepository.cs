using System;
using WebApi.Domain;

namespace WebApi.Repositories
{
	public interface ICourseRepository
	{
        Course? GetByCode(string code);

		Course Add(Course newCourse);
	}
}

