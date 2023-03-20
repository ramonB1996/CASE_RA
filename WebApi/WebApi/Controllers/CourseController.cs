using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Repositories;

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
	}
}

