using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ninja_Manager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ninja_Manager.Controllers
{
    public class ShopController : MainController
    {
        public IActionResult Index(int NinjaId, Category? Type)
        {
            List<Gear> totalGear = Context.Gears.ToList();
            try
            {
                Ninja ninja = Context.Ninjas
                .Include(n => n.GearForNinja)
                .FirstOrDefault(n => n.Id == NinjaId);
                ViewBag.NinjaName = ninja.Name;
                ViewBag.NinjaId = NinjaId;
                ViewBag.NinjaGold = ninja.Gold;
                ViewBag.Type = Type;
                List<Gear> ninjaGear = ninja.GearForNinja;
                List<Gear> shopGear = new List<Gear>();
                if (Type == null || Type == Category.All)
                {
                    foreach (Gear g in totalGear)
                    {
                        shopGear.Add(g);
                    }
                }
                else
                {
                    foreach (Gear g in totalGear)
                    {
                        if (g.Type == Type) { shopGear.Add(g); }
                    }
                }
                ViewBag.NinjaGear = ninjaGear;
                ViewBag.Categories = new List<Category> { Category.All, Category.Ring, Category.Feet, Category.Chest, Category.Head, Category.Chest, };
                totalGear.OrderBy(t => t.Cost);
                return View(shopGear);
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("Something went Wrong.", "Index", "Ninja");
            }
        }

        [HttpPost]
        public IActionResult Buy(int GearId, int NinjaId)
        {
            List<Gear> gear = Context.Gears.ToList();
            try
            {
                Ninja ninja = Context.Ninjas
                .Include(n => n.GearForNinja)
                .FirstOrDefault(n => n.Id == NinjaId);
                Gear buyGear = gear.Where(e => e.Id == GearId).First();
                if (!(ninja == null || buyGear == null))
                {
                    List<Gear> gearForNinja = ninja.GearForNinja;

                    if (ninja.GearForNinja.Any(g => g.Type == buyGear.Type))
                    {
                        Gear geartoRemove = ninja.GearForNinja.Where(g => g.Type == buyGear.Type).First();
                        ninja.Gold += geartoRemove.Cost;
                        ninja.GearForNinja.Remove(geartoRemove);
                    }
                    if (ninja.Gold >= buyGear.Cost)
                    {
                        ninja.Gold -= buyGear.Cost;
                        ninja.GearForNinja.Add(buyGear);
                        NotifySucces(buyGear.Name + " Bought for: " + buyGear.Cost + " gold.");
                    }
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index", "Shop", new { NinjaId = NinjaId });
        }
        [HttpPost]
        public IActionResult Sell(int GearId, int NinjaId)
        {
            List<Gear> gear = Context.Gears.ToList();
            try
            {
                Ninja ninja = Context.Ninjas
                .Include(n => n.GearForNinja)
                .FirstOrDefault(n => n.Id == NinjaId);
                Gear sellGear = gear.Where(e => e.Id == GearId).First();
                if (!(ninja == null || sellGear == null))
                {
                    List<Gear> gearForNinja = ninja.GearForNinja;

                    if (ninja.GearForNinja.Contains(sellGear))
                    {
                        ninja.Gold += sellGear.Cost;
                        ninja.GearForNinja.Remove(sellGear);
                        NotifySucces(sellGear.Name + " sold for: " + sellGear.Cost + " gold.");
                    }
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index", "Shop", new { NinjaId = NinjaId });
        }
    }
}
