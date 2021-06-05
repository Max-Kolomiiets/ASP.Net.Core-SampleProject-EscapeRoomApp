using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Utils
{
    public static class UrlFileToBytes
    {
        public static byte[] GetBytes(string pathUrl)
        {
            using (var webClient = new WebClient())
            {
                byte[] fileBytes = webClient.DownloadData(pathUrl);
                if (fileBytes != null)
                    return fileBytes;
                return null;
            }
        }
    }
}
