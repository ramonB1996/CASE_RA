using System;
namespace WebApi.Tests
{
	public static class FormFileMocker
	{
        public static IFormFile CreateMockFile(string fileContent, string contentType = "text/plain")
        {
            var content = fileContent;
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            return new FormFile(stream, 0, stream.Length, string.Empty, "example.txt")
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }
    }
}

