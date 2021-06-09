using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoomWebAppCore.Data;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomWebAppCore.Controllers
{
    public class EscapeRoomsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EscapeRoomsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("{controller}/{name}")]
        public IActionResult Details(string name)
        {
            return View(_db.EscapeRooms.FirstOrDefault(p=>p.Name==name));
        }

    }
}
