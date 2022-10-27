using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Extensions
{
    public static class FileExtensions
    {
        public static bool UploadFile(this IFormFile file, string fileName, string path, List<string>? ValidFormat = null)
        {
            if (ValidFormat != null && ValidFormat.Any())
            {
                var filefromat = Path.GetExtension(fileName);
                if (ValidFormat.All(v => v != filefromat))
                {
                    return false;
                }
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var finalpath = path + fileName;

            using (var stream = new FileStream(finalpath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return true;
        }       
    }
}
