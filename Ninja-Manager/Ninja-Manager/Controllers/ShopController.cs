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
                totalGear.OrderBy(t => t.getCost());
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
                    int tradeInValue = 0;
                    if (ninja.GearForNinja.Any(g => g.Type == buyGear.Type))
                    {
                        Gear geartoRemove = ninja.GearForNinja.Where(g => g.Type == buyGear.Type).First();
                        int gearCost = geartoRemove.getCost();
                        ninja.Gold += gearCost;
                        tradeInValue = gearCost;
                        ninja.GearForNinja.Remove(geartoRemove);
                    }
                    if (ninja.Gold >= buyGear.getCost())
                    {
                        int gearCost = buyGear.getCost();
                        ninja.Gold -= gearCost;
                        ninja.GearForNinja.Add(buyGear);
                        NotifySucces(buyGear.Name + " bought for: " + (gearCost - tradeInValue) + " gold.");
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
                        ninja.Gold += sellGear.getCost();
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

        [HttpPost]
        public IActionResult SellAll(int NinjaId)
        {
            List<Gear> gear = Context.Gears.ToList();
            try
            {
                Ninja ninja = Context.Ninjas
                .Include(n => n.GearForNinja)
                .FirstOrDefault(n => n.Id == NinjaId);
                int invValue = 0;
                foreach(Gear g in ninja.GearForNinja)
                {
                    invValue += g.getCost(); 
                }
                ninja.GearForNinja = new List<Gear>();
                ninja.Gold += invValue;
                Context.SaveChanges();
                NotifySucces("Inventory sold for: " + invValue + " gold.");
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("An error occured.", "Index", "Ninja");
            }
            return RedirectToAction("Index", "Shop", new { NinjaId = NinjaId });
        }
    }
}
