using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Helpers;
using WebApi.Repositories;
using WebApi.Exceptions;
using WebApi.Domain.DTO;
using WebApi.Services;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/courses")]
	public class CourseController : ControllerBase
	{
		private readonly ICourseService _courseService;
		private readonly IFileParser _fileParser;

		public CourseController(ICourseService courseService, IFileParser fileParser)
		{
			_courseService = courseService;
			_fileParser = fileParser;
		}

		[HttpPost]
		public async Task<ActionResult> PostAsync(IFormFile file)
		{
            if (file != null && file.Length > 0 && file.ContentType == "text/plain")
            {
				try
				{
                    IEnumerable<Course> potentialNewCourses = await _fileParser.ParseFileToCoursesAsync(file);

					var result = _courseService.ProcessCoursesToDTO(potentialNewCourses);

                    return Ok(result);
				}
				catch (FileFormatException ffe)
				{
					return StatusCode(500, ffe.Message.ToString()); 
				}
				catch (Exception)
				{
                    return StatusCode(500, "Algemene fout opgetreden op de server");
                }
            }

            return BadRequest("Bestand is geen .txt-bestand of is leeg!");
        }
	}
}

