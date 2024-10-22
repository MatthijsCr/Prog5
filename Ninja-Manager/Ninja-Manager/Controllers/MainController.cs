using Microsoft.AspNetCore.Mvc;
using Ninja_Manager.Models;

namespace Ninja_Manager.Controllers
{
    public class MainController : Controller
    {
        protected readonly NinjaManagerDbContext Context;

        public MainController()
        {
            Context = new NinjaManagerDbContext();
        }
    }
}
