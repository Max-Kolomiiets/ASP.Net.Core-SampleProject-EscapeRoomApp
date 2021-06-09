using AutoMapper;
using EscapeRoomWebAppCore.Areas.Admin.DTOs;
using EscapeRoomWebAppCore.Data;
using EscapeRoomWebAppCore.Models;
using EscapeRoomWebAppCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EscapeRoomController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public EscapeRoomController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 4;

            IQueryable<EscapeRoom> source = _db.EscapeRooms;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new(count, page, pageSize);
            IndexViewModel viewModel = new()
            {
                PageViewModel = pageViewModel,
                EscapeRooms = items
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var room = await _db.EscapeRooms.FirstOrDefaultAsync(p => p.Id == id);
            return View(room);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DTOs.EscapeRoomDTO roomDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var room = _mapper.Map<EscapeRoom>(roomDTO);

                    if (roomDTO.Logotype != null && room != null)
                    {
                        byte[] imageData = null;

                        using (var binaryReader = new BinaryReader(roomDTO.Logotype.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)roomDTO.Logotype.Length);
                        }
                        room.Logotype = imageData;
                    }
                    _db.EscapeRooms.Add(room);
                    await _db.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            var room = _db.EscapeRooms.FirstOrDefault(p => p.Id == id);
            return View(_mapper.Map<EscapeRoomDTO>(room));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EscapeRoomDTO escapeRoomDTO)
        {
            if (ModelState.IsValid)
            {
                var room = _mapper.Map<EscapeRoom>(escapeRoomDTO);

                if (room != null)
                {
                    if(escapeRoomDTO.Logotype != null)
                    {
                        byte[] imageData = null;

                        using (var binaryReader = new BinaryReader(escapeRoomDTO.Logotype.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)escapeRoomDTO.Logotype.Length);
                        }
                        room.Logotype = imageData;
                    }
                    _db.EscapeRooms.Update(room);
                    _db.SaveChanges();
                } 

                return RedirectToAction(nameof(Index));
            }
            return View(escapeRoomDTO);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id)
        {
            if (id == null) return NotFound();
            var model = _mapper.Map<EscapeRoomDTO>(_db.EscapeRooms.FirstOrDefault(p => p.Id == id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var room = _db.EscapeRooms.FirstOrDefault(p => p.Id == id);
            if (room == null) return View();

            _db.Remove(room);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
