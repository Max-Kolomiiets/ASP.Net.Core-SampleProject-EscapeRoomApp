using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Services.Interfaces
{
    interface IAddFile
    {
        Task<string> AddFile(IFormFile file, string pathToSave = "");
    }
}
