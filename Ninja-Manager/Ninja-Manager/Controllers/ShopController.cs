using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            List<Gear> totalGear = Context.Gears.OrderBy(g => g.Type).ThenBy(g => g.Cost).ToList();

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
                ViewBag.MaxStatSize = MaxStatSize;
                ViewBag.MaxGearNameSize = MaxGearNameSize;
                ViewBag.Categories = new List<Category> { Category.All, Category.Ring, Category.Feet, Category.Chest, Category.Head, Category.Hands, };
                ViewBag.CreateCategories = new List<Category> { Category.Ring, Category.Feet, Category.Chest, Category.Head, Category.Hands, };
                return View(shopGear);
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("An Error occured", "Index", "Ninja");
            }
        }

        public IActionResult Edit(int GearId, int NinjaId)
        {
            ViewBag.Categories = new List<Category> { Category.Ring, Category.Feet, Category.Chest, Category.Head, Category.Hands, };
            ViewBag.MaxStatSize = MaxStatSize;
            ViewBag.MaxNameLength = MaxGearNameSize;
            ViewBag.NinjaId = NinjaId;
            ViewBag.AmountOfNinja = 0;
            ViewBag.RightHave = "has";
            Gear gear = Context.Gears.Where(g => g.Id == GearId).FirstOrDefault();
            if (gear == null) { return NotifyErrorAndRedirectWithNinja("An Error occured", "Index", "Shop", NinjaId); }
            foreach (Ninja ninja in Context.Ninjas.Include(n => n.GearForNinja))
            {
                if (ninja.GearForNinja.Contains(gear)) { ViewBag.AmountOfNinja += 1; }
            }
            if(ViewBag.AmountOfNinja != 1)
            { ViewBag.RightHave = "have"; }
            return View(gear);
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
                        int gearCost = geartoRemove.Cost;
                        ninja.Gold += gearCost;
                        tradeInValue = gearCost;
                        ninja.GearForNinja.Remove(geartoRemove);
                    }
                    if (ninja.Gold >= buyGear.Cost)
                    {
                        int gearCost = buyGear.Cost;
                        ninja.Gold -= gearCost;
                        ninja.GearForNinja.Add(buyGear);
                        NotifySuccess(buyGear.Name + " bought for: " + (gearCost - tradeInValue) + " gold.");
                    }
                    else
                    {
                        return NotifyErrorAndRedirectWithNinja("Ninja does not have enough gold to buy " + buyGear.Name + ".", "Index", "Shop", NinjaId);
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
                        NotifySuccess(sellGear.Name + " sold for: " + sellGear.Cost + " gold.");
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
                foreach (Gear g in ninja.GearForNinja)
                {
                    invValue += g.Cost;
                }
                ninja.GearForNinja = new List<Gear>();
                ninja.Gold += invValue;
                Context.SaveChanges();
                NotifySuccess("Inventory sold for: " + invValue + " gold.");
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirectWithNinja("An Error occured", "Index", "Shop", NinjaId);
            }
            return RedirectToAction("Index", "Shop", new { NinjaId = NinjaId });
        }

        [HttpPost]
        public IActionResult CreateGear(int NinjaId, String Name, Category Type, int Strength, int Agility, int Intelligence)
        {
            try
            {
                if (Strength > MaxStatSize || Agility > MaxStatSize || Intelligence > 10)
                {
                    return NotifyErrorAndRedirectWithNinja("Stats are not allowed to be larger than " + MaxStatSize + ".", "Index", "Shop", NinjaId);
                }
                if(Name == null)
                {
                    return NotifyErrorAndRedirectWithNinja("Name value must be filled.", "Index", "Shop", NinjaId);
                }
                if (Name.Length <= 0)
                {
                    return NotifyErrorAndRedirectWithNinja("Name value must be filled.", "Index", "Shop", NinjaId);
                }
                if (Name.Length > MaxGearNameSize)
                {
                    return NotifyErrorAndRedirectWithNinja("Name value is not allowed to be larger than: " + MaxGearNameSize + ".", "Index", "Shop", NinjaId);
                }
                if (!new List<Category> { Category.Hands, Category.Ring, Category.Feet, Category.Chest, Category.Head }.Contains(Type))
                {
                    return NotifyErrorAndRedirectWithNinja("Type does not exist", "Index", "Shop", NinjaId);
                }

                if (Makegear(Name, Type, Strength, Agility, Intelligence))
                {
                    NotifySuccess(Name + " succesfully added to Gear");
                    return RedirectToAction("Index", new { NinjaId = NinjaId});
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirectWithNinja("An Error occured.", "Index", "Shop", NinjaId);
            }
        }
        private bool Makegear(String Name, Category Type, int Strength, int Agility, int Intelligence)
        {
            try
            {
                Gear newGear = new Gear();
                newGear.Name = Name;
                newGear.Strength = Strength;
                newGear.Agility = Agility;
                newGear.Intelligence = Intelligence;
                int Cost = 0;
                Cost += Strength * StrengthCost;
                Cost += Agility * AgilityCost;
                Cost += Intelligence * IntelligenceCost;
                if (Cost <= 0)
                {
                    Cost = 20;
                }
                newGear.Cost = Cost;
                newGear.Type = Type;
                Context.Add(newGear);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public IActionResult EditGear(int GearId, String Name, Category Type, int Strength, int Agility, int Intelligence, int NinjaId)
        {
            try
            {
                Gear gear = Context.Gears.Where(g => g.Id == GearId).FirstOrDefault();
                if ( gear == null)
                {
                    return NotifyErrorAndRedirectWithNinja("An Error occured.", "Index", "Shop", NinjaId);
                }
                if (Strength > MaxStatSize || Agility > MaxStatSize || Intelligence > 10)
                {
                    return NotifyErrorAndRedirectWithNinja("Stats are not allowed to be larger than " + MaxStatSize + ".", "Index", "Shop", NinjaId);
                }
                if (Name == null)
                {
                    return NotifyErrorAndRedirectWithNinja("Name value must be filled.", "Index", "Shop", NinjaId);
                }
                if (Name.Length <= 0)
                {
                    return NotifyErrorAndRedirectWithNinja("Name value must be filled.", "Index", "Shop",NinjaId);
                }
                if (Name.Length > MaxGearNameSize)
                {
                    return NotifyErrorAndRedirectWithNinja("Name value is not allowed to be larger than: " + MaxGearNameSize + ".", "Index", "Shop", NinjaId);
                }
                if (!new List<Category> { Category.Hands, Category.Ring, Category.Feet, Category.Chest, Category.Head }.Contains(Type))
                {
                    return NotifyErrorAndRedirectWithNinja("Type does not exist", "Index", "Shop", NinjaId);
                }

                if (ChangeGear(gear, Name, Type, Strength, Agility, Intelligence))
                {
                    NotifySuccess(Name + " succesfully added to Gear");
                    return RedirectToAction("Index","Shop", new {NinjaId = NinjaId});
                }
                return NotifyErrorAndRedirectWithNinja("An Error occured.", "Index", "Shop", NinjaId);
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("An Error occured.", "Index", "Shop");
            }
        }

        private bool ChangeGear(Gear Gear, String Name, Category Type, int Strength, int Agility, int Intelligence)
        {
            try
            {
                Gear.Name = Name;
                Gear.Strength = Strength;
                Gear.Agility = Agility;
                Gear.Intelligence = Intelligence;
                int Cost = 0;
                Cost += Strength * StrengthCost;
                Cost += Agility * AgilityCost;
                Cost += Intelligence * IntelligenceCost;
                if (Cost <= 0)
                {
                    Cost = 20;
                }
                List<Ninja> ninja = Context.Ninjas.Include(n => n.GearForNinja).ToList();
                foreach (Ninja n in ninja)
                {
                    if (n.GearForNinja.Contains(Gear))
                    {
                        int difference = Cost - Gear.Cost;
                        if(n.Gold - difference < 0 || n.GearForNinja.Where(g => g.Type == Gear.Type).Any()) 
                        { 
                            n.GearForNinja.Remove(Gear);
                            n.Gold += Gear.Cost;
                        }
                        else
                        {
                            n.Gold -= difference;
                        }
                    }
                    
                }
                Gear.Type = Type;
                Gear.Cost = Cost;
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public IActionResult DeleteGear(int GearId,int NinjaId)
        {
            Gear deleteGear = Context.Gears.Where(g => g.Id == GearId).FirstOrDefault();
            if (deleteGear == null)
            {
                return NotifyErrorAndRedirectWithNinja("Gear could not be found", "Index", "Shop",NinjaId);
            }
            if (removeGear(deleteGear))
            {
                NotifySuccess("Deleted " + deleteGear.Name + ".");
                return RedirectToAction("Index", new { NinjaId = NinjaId });
            }
            return NotifyErrorAndRedirectWithNinja("Error occured during deletion.", "Index", "Shop",NinjaId);
            
        }
        private bool removeGear(Gear deleteGear)
        {
            try
            {
                List<Ninja> ninja = Context.Ninjas.Include(n => n.GearForNinja).ToList();
                foreach (Ninja n in ninja)
                {
                    if (n.GearForNinja.Contains(deleteGear))
                    {
                        n.GearForNinja.Remove(deleteGear);
                        n.Gold += deleteGear.Cost;
                    }
                }
                Context.Remove(deleteGear);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
