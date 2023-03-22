using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Helpers;
using WebApi.Repositories;
using WebApi.Exceptions;
using WebApi.Domain.DTO;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/courses")]
	public class CourseController : ControllerBase
	{
		private readonly ICourseRepository _courseRepository;
		private readonly ICourseInstanceRepository _courseInstanceRepository;

		public CourseController(ICourseRepository courseRepository, ICourseInstanceRepository courseInstanceRepository)
		{
			_courseRepository = courseRepository;
			_courseInstanceRepository = courseInstanceRepository;
		}

		[HttpPost]
		public async Task<ActionResult> PostAsync(IFormFile file)
		{
            if (file != null && file.Length > 0 && file.ContentType == "text/plain")
            {
				try
				{
					CourseAndInstancesDTO result = new CourseAndInstancesDTO();

                    IEnumerable<Course> potentialNewCourses = await FileParser.ParseFileToCoursesAsync(file);

					foreach (Course potentialNewCourse in potentialNewCourses)
					{
						// Make sure to not add courseInstances when adding the course.
						List<CourseInstance> potentialNewCourseInstances = new List<CourseInstance>(potentialNewCourse.CourseInstances);
						potentialNewCourse.CourseInstances.Clear();

						Course? course = _courseRepository.GetByCode(potentialNewCourse.Code);

                        if (course == null)
						{
                            course = _courseRepository.Add(potentialNewCourse);

							result.Courses.Add(course);
                        }

                        foreach (CourseInstance potentialNewCourseInstance in potentialNewCourseInstances)
						{
							CourseInstance? courseInstance = _courseInstanceRepository.GetByStartDateAndCourseId(potentialNewCourseInstance.StartDate, course.Id);

							if (courseInstance == null)
							{
                                potentialNewCourseInstance.CourseId = course.Id;
                                courseInstance = _courseInstanceRepository.Add(potentialNewCourseInstance);

                                result.CourseInstances.Add(courseInstance);
                            }
                        }
                    }

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

