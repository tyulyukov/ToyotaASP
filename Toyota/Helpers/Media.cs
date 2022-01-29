using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Toyota.Helpers
{
    public static class Media
    {
        public static String WebRootPath;
        public static String StoragePath => WebRootPath + "\\storage\\";

        public static string GetFileExtension(string contentType)
        {
            string result;
            RegistryKey key;
            object value;

            key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + contentType, false);
            value = key?.GetValue("Extension", null);
            result = value != null ? value.ToString() : string.Empty;

            return result;
        }

        public async static Task<String> UploadImage(IFormFile file, String directoryName)
        {
            if (file == null)
                return String.Empty;

            String fileExtension = GetFileExtension(file.ContentType);

            String fileName = Guid.NewGuid().ToString() + fileExtension;
            String basePath = CreateDirectory(DateTime.Today, directoryName) + "/" + fileName;

            using (var fileStream = new FileStream(StoragePath + basePath, FileMode.Create))
                await file.CopyToAsync(fileStream);

            return "/storage/" + basePath;
        }

        public static String CreateDirectory(DateTime date, String directoryName)
        {
            if (!Directory.Exists(StoragePath + directoryName + "\\" + date.Year))
                Directory.CreateDirectory(StoragePath + directoryName + "\\" + date.Year);

            if (!Directory.Exists(StoragePath + directoryName + "\\" + date.Year + "\\" + date.Month))
                Directory.CreateDirectory(StoragePath + directoryName + "\\" + date.Year + "\\" + date.Month);

            return directoryName + "/" + date.Year + "/" + date.Month;
        }
    }
}
