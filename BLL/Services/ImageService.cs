using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly string _folder;

        public ImageService(string folder)
        {
            _folder = folder;
        }

        public void Delete(string fileName)
        {
            throw new NotImplementedException();
        }

        public string Save(IFormFile file)
        {
            // בדיקת סוג הקובץ
            var ext = Path.GetExtension(file.FileName).ToLower();
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowed.Contains(ext))
                throw new ArgumentException("מותר להעלות רק jpg, png או webp");

            // בדיקת גודל: עד 5MB
            if (file.Length > 5 * 1024 * 1024)
                throw new ArgumentException("הקובץ גדול מדי");

            // שם חדש וייחודי, לא השם שהמשתמש נתן
            var fileName = Guid.NewGuid() + ext;

            using (var stream = File.Create(Path.Combine(_folder, fileName)))
            {
                file.CopyTo(stream);
            }

            return fileName;   // מחזירים רק את השם
        }

    }
}
