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
		public ActionResult<IEnumerable<CourseInstance>> GetForDateRange(string startDate, string endDate)
		{
            try
            {
                DateOnly start = DateOnly.Parse(startDate);
                DateOnly end = DateOnly.Parse(endDate);

                if (end < start)
                {
                    return BadRequest("Einddatum moet na begindatum liggen!");
                }

                IEnumerable<CourseInstance> results = _courseInstanceRepository.GetAllForDateRange(start, end);

                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algemene fout opgetreden op de server");
            }
        }
	}
}

