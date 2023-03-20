using System;
using WebApi.Domain;

namespace WebApi.Repositories
{
	public interface ICourseInstanceRepository
	{
		IEnumerable<CourseInstance> GetAll();
	}
}

