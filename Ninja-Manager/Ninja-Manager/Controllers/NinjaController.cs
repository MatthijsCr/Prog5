using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ninja_Manager.Models;
using System.Text.RegularExpressions;
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

            if (TempData["Name"] != null)
            {
                ViewBag.Name = TempData["Name"];
            }

            return View(NinjaList);
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (name == null)
            {
                return NotifyErrorAndRedirect("Name cannot be empty.", "Index", "Ninja");
            }

            name = name.Trim();
            TempData["Name"] = name;

            if (name.Length > 10)
            {
                return NotifyErrorAndRedirect("Name length must be at most 10 characters.", "Index", "Ninja");
            }
            else if (name.Contains("  "))
            {
                return NotifyErrorAndRedirect("Name contains two or more consecutive spaces.", "Index", "Ninja");
            }

            foreach (Ninja n in context.Ninjas)
            {
                if (name.ToLower().Equals(n.Name.ToLower()))
                {
                    return NotifyErrorAndRedirect("A ninja with the name " + name + " already exists!", "Index", "Ninja");
                }
            }

            TempData["Name"] = null;

            context.Ninjas.Add(new Ninja { Name = name, Gold = 1000 });
            context.SaveChanges();

            NotifySucces("Ninja created, hello " + name + "!");
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
