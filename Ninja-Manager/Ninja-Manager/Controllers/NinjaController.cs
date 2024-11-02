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
            try
            {
                string message = ValidateNinjaName(name);
                if (message != null)
                {
                    TempData["Name"] = name;
                    return NotifyErrorAndRedirect(message, "Index", "Ninja");
                }

                context.Ninjas.Add(new Ninja { Name = name, Gold = 1000 });
                context.SaveChanges();

                NotifySuccess("Ninja created, hello " + name + "!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("Something went wrong.", "Index", "Ninja");
            }
        }

        private string ValidateNinjaName(string name)
        {
            if (name == null)
            {
                return "Name cannot be empty.";
            }

            name = name.Trim();

            if (name.Length > 10)
            {
                return "Name length must be at most 10 characters.";
            }
            else if (name.Contains("  "))
            {
                return "Name contains two or more consecutive spaces.";
            }

            foreach (Ninja n in context.Ninjas)
            {
                if (name.ToLower().Equals(n.Name.ToLower()))
                {
                    return "A ninja with the name " + name + " already exists!";
                }
            }

            return null;
        }

        public IActionResult Edit(int NinjaId)
        {
            try
            {
                if (TempData["EditName"] != null)
                {
                    ViewBag.EditName = TempData["EditName"];
                }
                return View(CreateInventoryViewModel(NinjaId));
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("Something went wrong.", "Index", "Ninja");
            }
        }

        public IActionResult Save(int NinjaId, string name)
        {
            try
            {
                if (!(context.Ninjas.Where(n => n.Id == NinjaId).First().Name == name))
                {
                    string message = ValidateNinjaName(name);
                    if (message != null)
                    {
                        TempData["EditName"] = name;
                        return NotifyErrorAndRedirectWithNinja(message, "Edit", "Ninja", NinjaId);
                    }

                    context.Ninjas
                        .Where(n => n.Id == NinjaId)
                        .First()
                        .Name = name;
                    context.SaveChanges();
                }

                NotifySuccess("Saved changes for ninja.");
                return RedirectToAction("Inventory", new { NinjaId = NinjaId });
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("Something went wrong.", "Inventory", "Ninja");
            }
        }

        public IActionResult Inventory(int NinjaId)
        {
            try
            {
                return View(CreateInventoryViewModel(NinjaId));
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("Something went wrong.", "Index", "Ninja");
            }
        }

        public IActionResult Delete(int NinjaId)
        {
            try
            {
                Ninja ninja = context.Ninjas.Where(n => n.Id == NinjaId).FirstOrDefault();
                string name = ninja.Name;

                context.Ninjas.Remove(ninja);
                context.SaveChanges();

                NotifySuccess("Deleted ninja " + name + " successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirectWithNinja("Something went wrong.", "Inventory", "Ninja", NinjaId);
            }
        }

        private InventoryViewModel CreateInventoryViewModel(int NinjaId)
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
            return inventoryView;
        }
    }
}
