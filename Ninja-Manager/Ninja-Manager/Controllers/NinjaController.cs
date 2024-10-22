using Microsoft.AspNetCore.Mvc;
using Ninja_Manager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ninja_Manager.Controllers
{
    public class NinjaController : MainController
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
