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
            ViewBag.Ninja = Context.Ninjas.Find(NinjaId).Name;
            ViewBag.NinjaId = NinjaId;
            ViewBag.NinjaGold = Context.Ninjas.Find(NinjaId).Gold;
            ViewBag.Type = Type;
            ViewBag.NinjaGear = Context.Ninjas.Find(NinjaId).GearForNinja;
            List<Gear> ninjaGear = Context.Ninjas.Find(NinjaId).GearForNinja;
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
            Ninja ninja = Context.Ninjas.Find(ninjaId);
            Gear gear = Context.Gears.Find(gearId);
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
