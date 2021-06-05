using EscapeRoomWebAppCore.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Services
{
    public class AddFileLocaly : IAddFile
    {
        private readonly IWebHostEnvironment _env;
        public AddFileLocaly(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> AddFile(IFormFile file, string pathToSave)
        {
            if (file != null)
            {
                // путь к папке Files
                string path = "/Files/" + file.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return path;
            }
            throw new FileNotFoundException();
        }
    }
}
