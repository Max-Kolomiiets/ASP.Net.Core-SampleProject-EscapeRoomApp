using EscapeRoomWebAppCore.Data;
using EscapeRoomWebAppCore.Models;
using EscapeRoomWebAppCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(int? diffLevel, int? countPlayers, int? fearLevel, int page = 1)
        {
            int pageSize = 4;

            IQueryable<EscapeRoom> source = _db.EscapeRooms;

            if (diffLevel.HasValue && diffLevel > 0)
                source = source.Where(p => p.DifficultyLevel == diffLevel);

            if (countPlayers.HasValue && (countPlayers > 0 && countPlayers <= 40))
                source = source.Where(p => p.MaximumPlayers >= countPlayers);

            if (fearLevel.HasValue && fearLevel > 0)
                source = source.Where(p => p.FearLevel == fearLevel);


            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).OrderBy(p=>p.Id).ToListAsync();

            PageViewModel pageViewModel = new(count, page, pageSize);
            IndexViewModel viewModel = new()
            {
                PageViewModel = pageViewModel,
                FilterViewModel = new FilterViewModel(countPlayers, diffLevel, fearLevel),
                EscapeRooms = items
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
