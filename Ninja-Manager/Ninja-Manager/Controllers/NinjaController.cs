using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ninja_Manager.Controllers
{
    public class NinjaController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
