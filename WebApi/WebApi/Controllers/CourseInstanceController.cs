using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Repositories;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/courseinstances")]
	public class CourseInstanceController : ControllerBase
    {
		private readonly ICourseInstanceRepository _courseInstanceRepository;

		public CourseInstanceController(ICourseInstanceRepository courseInstanceRepository)
		{
			_courseInstanceRepository = courseInstanceRepository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CourseInstance>> Get()
		{
			IEnumerable<CourseInstance> results = _courseInstanceRepository.GetAll();

			return Ok(results);
		}
	}
}

