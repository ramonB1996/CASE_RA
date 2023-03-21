﻿using System;
using Azure.Core;
using System.Globalization;
using WebApi.Domain;
using WebApi.Exceptions;

namespace WebApi.Helpers
{
	public static class FileParser
	{
        private static List<string> CorrectKeysInOrder = new List<string>()
        {
            "Titel: ",
            "Cursuscode: ",
            "Duur: ",
            "Startdatum: "
        };

        public static async Task<IEnumerable<Course>> ParseFileToCoursesAsync(IFormFile file)
		{
			List<Course> result = new List<Course>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                await ReadStream(result, reader);
            }

            return result;
        }

        private static async Task ReadStream(List<Course> result, StreamReader reader)
        {
            Course course = new Course()
            {
                CourseInstances = new List<CourseInstance>()
            };

            for (int i = 0; i < 4; i++)
            {
                string? line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line) || !line.StartsWith(CorrectKeysInOrder[i]))
                {
                    throw new FileFormatException("Wrong file format.");
                }

                string data = line.Substring(CorrectKeysInOrder[i].Length);

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
                            DateTime startDate = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            course.CourseInstances.Add(new CourseInstance()
                            {
                                StartDate = DateOnly.FromDateTime(startDate)
                            });
                        }
                        catch (Exception)
                        {
                            throw new FormatException("Startdatum is niet in het correcte format dd/MM/yyyy");
                        }
                        break;
                }
            }

            string? emptyLine = await reader.ReadLineAsync();

            if (!string.IsNullOrWhiteSpace(emptyLine))
            {
                throw new FileFormatException("Empty line needed between new entities.");
            }

            result.Add(course);

            if (!reader.EndOfStream)
            {
                await ReadStream(result, reader);
            }
        }
    }
}

