using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;

namespace Doctorantura.App.Extensions
{
    public static class IFormFileExtension
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool IsVideo(this IFormFile file)
        {
            return file.ContentType.Contains("video/");
        }

        public static async Task<string> SaveAsync(this IFormFile file, string root, string mainFolder, string childFolder)
        {
            string path = Path.Combine(root, mainFolder, childFolder);
            string image = Guid.NewGuid().ToString() + file.FileName;
            string resultPath = Path.Combine(path, image);


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await using (var stream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
            }

            return image;
        }

        public static async Task<string> SaveAsync(this IFormFile file, string root, string mainFolder)
        {
            string path = Path.Combine(root, mainFolder);
            string video = Guid.NewGuid().ToString() + file.FileName;
            string resultPath = Path.Combine(path, video);

            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return video;
        }

        public static void RemoveFile(string root, string mainFolder, string childFolder, string fileName)
        {
            string path = Path.Combine(root, mainFolder, childFolder, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void RemoveFile(string root, string mainFolder, string fileName)
        {
            string path = Path.Combine(root, mainFolder, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
