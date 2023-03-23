using System;
using Azure.Core;
using System.Globalization;
using WebApi.Domain;
using WebApi.Exceptions;

namespace WebApi.Helpers
{
	public class FileParser : IFileParser
	{
        private static readonly List<string> _correctKeysInOrder = new List<string>()
        {
            "Titel: ",
            "Cursuscode: ",
            "Duur: ",
            "Startdatum: "
        };

        public async Task<IEnumerable<Course>> ParseFileToCoursesAsync(IFormFile file)
		{
			List<Course> result = new List<Course>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                await ReadStreamAsync(result, reader);
            }

            return result;
        }

        private async Task ReadStreamAsync(List<Course> result, StreamReader reader)
        {
            Course course = new Course()
            {
                CourseInstances = new List<CourseInstance>()
            };

            for (int i = 0; i < 4; i++)
            {
                string? line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line) || !line.StartsWith(_correctKeysInOrder[i]))
                {
                    throw new FileFormatException("Volgorde van kolommen klopt niet.");
                }

                string data = line.Substring(_correctKeysInOrder[i].Length);

                switch (i)
                {
                    case 0:
                        course.Title = data;
                        break;

                    case 1:
                        course.Code = data;
                        break;

                    case 2:
                        if (!data.Contains("dagen"))
                        {
                            throw new FileFormatException("Duur heeft verkeerde format. Het correcte format is: <aantal dagen> dagen.");
                        }

                        string duration = string.Concat(data.Where(char.IsNumber));

                        if (duration.Length < 1)
                        {
                            throw new FileFormatException("Duur heeft verkeerde format. Het correcte format is: <aantal dagen> dagen.");
                        }

                        course.Duration = int.Parse(duration);
                        break;

                    case 3:
                        try
                        {
                            DateOnly startDate = DateOnly.ParseExact(data, new string[] {"dd/MM/yyyy","d/MM/yyyy", "d/M/yyyy", "dd/M/yyyy"}, CultureInfo.InvariantCulture);
                            course.CourseInstances.Add(new CourseInstance()
                            {
                                StartDate = startDate
                            });
                        }
                        catch (Exception)
                        {
                            throw new FileFormatException("Startdatum is niet in het correcte format dd/MM/yyyy");
                        }
                        break;
                }
            }

            string? emptyLine = await reader.ReadLineAsync();

            if (!string.IsNullOrWhiteSpace(emptyLine))
            {
                throw new FileFormatException("Witregel is benodigd tussen cursusentiteiten in het bestand.");
            }

            result.Add(course);

            if (!reader.EndOfStream)
            {
                await ReadStreamAsync(result, reader);
            }
        }
    }
}

