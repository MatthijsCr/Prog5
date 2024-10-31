using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ninja_Manager.Models;
using System.Threading.Tasks.Dataflow;

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
            if (string.IsNullOrEmpty(name.Trim()))
            {
                TempData["ErrorMessage"] = "Name cannot be empty.";
                return RedirectToAction("Index");
            }
            else if (name.Length > 10)
            {
                TempData["ErrorMessage"] = "Longer than 10 characters.";
                return RedirectToAction("Index");
            }

            context.Ninjas.Add(new Ninja { Name = name, Gold = 1000 });
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Inventory(int NinjaId)
        {
            try
            {
                Ninja ninja = context.Ninjas
                    .Include(n => n.GearForNinja)
                    .FirstOrDefault(n => n.Id == NinjaId);

                List<Gear> ownedGear = new List<Gear>();
                foreach (Gear gear in ninja.GearForNinja)
                {
                    ownedGear.Add(context.Gears
                        .Where(g => g.Id == gear.Id)
                        .First());
                }

                int inventoryValue = 0;
                foreach (Gear gear in ownedGear)
                {
                    inventoryValue += gear.Cost;
                }
                ViewBag.InventoryValue = inventoryValue;

                InventoryViewModel inventoryView = new InventoryViewModel(ownedGear, ninja);

                return View(inventoryView);
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("Something went Wrong.", "Index", "Ninja");
            }
        }
    }
}
