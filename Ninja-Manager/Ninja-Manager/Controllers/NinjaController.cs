using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ninja_Manager.Models;

namespace Ninja_Manager.Controllers
{
    public class NinjaController : MainController
    {
        NinjaManagerDbContext context = new();

        public IActionResult Index()
        {
            var NinjaList = context.Ninjas
                .Include(n => n.GearForNinja).ToList();

            return View(NinjaList);
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                TempData["ErrorMessage"] = "Name cannot be empty. Please provide a valid name.";
                return RedirectToAction("Index");
            }

            context.Ninjas.Add(new Ninja { Name = name, Gold = 1000 });
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Inventory()
        {
            return View();
        }
    }
}
