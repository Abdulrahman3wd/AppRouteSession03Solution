using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace AppRouteSession03.PL.Helpers
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            // 1. get located folder path 
            //string folderPath = $"C:\\Users\\Jet\\Desktop\\mvc\\AppRouteSession03Solution\\AppRouteSession03\\wwwroot\\Files\\{folderName}";
            //string folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            // 2. get file name and make it Unique           
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // 3. get file path
            string filePath = Path.Combine(folderPath, fileName);

            // 4. save file as Streams [Data per Time]
            var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
            
        }


        public static void DeleteFile(string fileName , string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if (File.Exists(filePath))            
                File.Delete(filePath);
            
        }
    }
}
