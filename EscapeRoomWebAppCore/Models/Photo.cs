using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public int? EscapeRoomId { get; set; }
        public EscapeRoom EscapeRoom { get; set; }
    }
}
