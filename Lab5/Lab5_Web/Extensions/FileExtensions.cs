using System.Text;

namespace Lab5_Web.Extensions
{
    public static class FileExtensions
    {
        public static async Task<string> ReadAsStringAsync(this IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }
    }
}
