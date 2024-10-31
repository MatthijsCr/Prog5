using Microsoft.AspNetCore.Mvc;
using Ninja_Manager.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Ninja_Manager.Controllers
{
    public class MainController : Controller
    {
        protected readonly NinjaManagerDbContext Context;
        [FromServices] protected INotyfService NotifyService { get; set; } = null!;
        protected static IConfiguration Configuration = null!;

        public MainController()
        {
            Context = new NinjaManagerDbContext();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            NotifyService = HttpContext.RequestServices.GetService<INotyfService>()!;
            Configuration = HttpContext.RequestServices.GetService<IConfiguration>()!;
        }

        protected IActionResult NotifyErrorAndRedirect(string message, string redirect, string redirectController)
        {
            NotifyService.Error(message);
            return RedirectToAction(redirect, redirectController);
        }

        protected void NotifySucces(string message)
        {
            NotifyService.Success(message);
        }
    }
}
