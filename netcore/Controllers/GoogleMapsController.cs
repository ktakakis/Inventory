using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcore.Data;

namespace netcore.Controllers
{
    public class GoogleMapsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoogleMapsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() 
        {
            return View(await _context.CustomerLine.ToListAsync());
        }
    }
}