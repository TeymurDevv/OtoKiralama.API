using Microsoft.AspNetCore.Http;

namespace OtoKiralama.Application.Extensions
{
    public static class FileExtension
    {
        public static async Task<string> SaveFile(this IFormFile file, string FolderName = null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", FolderName ?? string.Empty, fileName);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public static void DeleteFile(this string fileName, string folderName = null)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", folderName ?? string.Empty, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
