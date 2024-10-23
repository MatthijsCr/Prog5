using Microsoft.AspNetCore.Mvc;
using Ninja_Manager.Models;
using System.Collections.Generic;

namespace Ninja_Manager.Controllers
{
    public class ShopController : MainController
    {
        public IActionResult Index(int NinjaId)
        {
            List <Gear> totalGear = Context.Gears.ToList();
            List<Gear> ninjaGear = Context.Ninjas.Find(NinjaId).GearForNinja;
            if (ninjaGear == null)
            {
                RedirectToAction("Index","Ninja");
            }
            foreach (Gear g in totalGear)
            {
                if(ninjaGear.Contains(g)) {totalGear.Remove(g);}
            }
            return View(totalGear);
        }
    }
}
