using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Helpers;
using WebApi.Repositories;
using WebApi.Exceptions;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/courses")]
	public class CourseController : ControllerBase
	{
		private readonly ICourseRepository _courseRepository;

		public CourseController(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Course>> Get()
		{
			IEnumerable<Course> courses = _courseRepository.GetAll();

			return Ok(courses);
		}

		[HttpPost]
		public async Task<ActionResult> Post(IFormFile file)
		{
            if (file != null && file.Length > 0 && file.ContentType == "text/plain")
            {
				try
				{
					IEnumerable<Course> result = await FileParser.ParseFileToCoursesAsync(file);

					result = _courseRepository.AddRange(result);

					return Ok(result);
				}
				catch (FileFormatException ffe)
				{
					return StatusCode(500, ffe.Message.ToString()); 
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message.ToString());
				}
            }

            return BadRequest("File is empty or has incorrect MIME-Type!");
        }
	}
}

