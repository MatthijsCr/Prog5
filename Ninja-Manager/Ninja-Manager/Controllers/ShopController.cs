using Microsoft.AspNetCore.Mvc;
using Ninja_Manager.Models;
using System.Collections.Generic;

namespace Ninja_Manager.Controllers
{
    public class ShopController : MainController
    {
        public IActionResult Index(int NinjaId, Category? Type)
        {
            List<Gear> totalGear = Context.Gears.ToList();
            Ninja ninja = (Ninja)Context.Ninjas.Where(e => e.Id == NinjaId);
            if (ninja == null) { RedirectToAction("Index", "Ninja"); }

            ViewBag.Ninja = ninja.Name;
            ViewBag.NinjaId = NinjaId;
            ViewBag.NinjaGold = ninja.Gold;
            ViewBag.Type = Type;
            List<Gear> ninjaGear = ninja.GearForNinja;
            if (ninjaGear == null)
            {
                ninjaGear = new List<Gear>();
            }
            foreach (Gear g in totalGear)
            {
                if (ninjaGear.Contains(g)) { totalGear.Remove(g); }
            }
            ViewBag.NinjaGear = ninjaGear;
            ViewBag.Categories = new List<Category> { Category.Ring, Category.Feet, Category.Chest, Category.Head, Category.Chest };
            totalGear.OrderBy(t => t.Cost);
            return View(totalGear);
        }

        [HttpPost]
        public IActionResult Buy(int gearId, int ninjaId)
        {
            Ninja ninja = (Ninja)Context.Ninjas.Where(e => e.Id == ninjaId);
            Gear gear = (Gear)Context.Gears.Where(e => e.Id == gearId);
            if (!(ninja == null || gear == null))
            {
                if(ninja.GearForNinja.Any(g => g.Type == gear.Type))
                {
                    Gear geartoRemove = ninja.GearForNinja.Where(g => g.Type == gear.Type).First();
                    ninja.Gold += geartoRemove.Cost;
                    ninja.GearForNinja.Remove(geartoRemove);
                }
                if (ninja.Gold >= gear.Cost) 
                {
                    ninja.Gold -= gear.Cost;
                    ninja.GearForNinja.Add(gear);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
