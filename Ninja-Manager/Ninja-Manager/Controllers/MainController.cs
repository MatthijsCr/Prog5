﻿using Microsoft.AspNetCore.Mvc;
using Ninja_Manager.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient.DataClassification;
using System.ComponentModel.DataAnnotations;

namespace Ninja_Manager.Controllers
{
    public class MainController : Controller
    {
        protected readonly NinjaManagerDbContext Context;
        protected readonly int MaxGearNameSize;
        protected readonly int MaxStatSize;
        protected readonly int StrengthCost;
        protected readonly int AgilityCost;
        protected readonly int IntelligenceCost;

        [FromServices] protected INotyfService NotifyService { get; set; } = null!;
        protected static IConfiguration Configuration = null!;


        public MainController()
        {
            Context = new NinjaManagerDbContext();
            MaxGearNameSize = 20;
            MaxStatSize = 10;
            StrengthCost = 30;
            AgilityCost = 20;
            IntelligenceCost = 20;
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

        protected IActionResult NotifyErrorAndRedirectWithNinja(string message, string redirect, string redirectController, int ninjaId)
        {
            NotifyService.Error(message);
            return RedirectToAction(redirect, redirectController, new { ninjaId = ninjaId} );
        }

        protected void NotifySuccess(string message)
        {
            NotifyService.Success(message);
        }
    }
}
