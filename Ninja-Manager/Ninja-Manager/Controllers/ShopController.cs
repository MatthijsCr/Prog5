﻿using Microsoft.AspNetCore.Mvc;
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
        private int ninjaId = 1;
        public IActionResult Index(int? NinjaId, Category? Type)
        {
            List<Gear> totalGear = Context.Gears.ToList();
            if (NinjaId == null)
            {
                NinjaId = ninjaId;
            }
            ninjaId = (int)NinjaId;
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
                ViewBag.MaxNameLength = MaxGearNameSize;
                ViewBag.Categories = new List<Category> { Category.All, Category.Ring, Category.Feet, Category.Chest, Category.Head, Category.Hands, };
                ViewBag.CreateCategories = new List<Category> { Category.Ring, Category.Feet, Category.Chest, Category.Head, Category.Hands, };
                totalGear.OrderBy(t => t.getCost());
                return View(shopGear);
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("An Error occured", "Index", "Ninja");
            }
        }

        public IActionResult Edit(int GearId)
        {
            ViewBag.Categories = new List<Category> { Category.Ring, Category.Feet, Category.Chest, Category.Head, Category.Hands, };
            ViewBag.MaxStatSize = MaxStatSize;
            ViewBag.MaxNameLength = MaxGearNameSize;
            Gear gear = Context.Gears.Where(g => g.Id == GearId).FirstOrDefault();
            if (gear == null) { return NotifyErrorAndRedirect("An Error occured", "Index", "Shop"); }
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
                foreach (Gear g in ninja.GearForNinja)
                {
                    invValue += g.Cost;
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

        [HttpPost]
        public IActionResult CreateGear(String Name, Category Type, int Strength, int Agility, int Intelligence)
        {
            try
            {
                if (Strength > MaxStatSize || Agility > MaxStatSize || Intelligence > 10)
                {
                    return NotifyErrorAndRedirect("Stats are not allowed to be larger than " + MaxStatSize + ".", "Index", "Shop");
                }
                if(Name == null)
                {
                    return NotifyErrorAndRedirect("Name value must be filled.", "Index", "Shop");
                }
                if (Name.Length <= 0)
                {
                    return NotifyErrorAndRedirect("Name value must be filled.", "Index", "Shop");
                }
                if (Name.Length > MaxGearNameSize)
                {
                    return NotifyErrorAndRedirect("Name value is not allowed to be larger than: " + MaxGearNameSize + ".", "Index", "Shop");
                }
                if (!new List<Category> { Category.Hands, Category.Ring, Category.Feet, Category.Chest, Category.Head }.Contains(Type))
                {
                    return NotifyErrorAndRedirect("Type does not exist", "Index", "Shop");
                }

                if (Makegear(Name, Type, Strength, Agility, Intelligence))
                {
                    NotifySucces(Name + " succesfully added to Gear");
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return NotifyErrorAndRedirect("An Error occured.", "Index", "Shop");
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
        public IActionResult DeleteGear(int GearId)
        {
            Gear deleteGear = Context.Gears.Where(g => g.Id == GearId).FirstOrDefault();
            if (deleteGear == null)
            {
                return NotifyErrorAndRedirect("Gear could not be found", "Index", "Shop");
            }
            if (removeGear(deleteGear))
            {
                NotifySucces("Deleted " + deleteGear.Name + ".");
                return RedirectToAction("Index");
            }
            return NotifyErrorAndRedirect("Error occured during deletion.", "Index", "Shop");
            
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
