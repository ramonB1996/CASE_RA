using System;
using WebApi.Domain;

namespace WebApi.Helpers
{
	public interface IFileParser
	{
        Task<IEnumerable<Course>> ParseFileToCoursesAsync(IFormFile file);
    }
}

