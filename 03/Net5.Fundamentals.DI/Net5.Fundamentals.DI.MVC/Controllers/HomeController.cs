using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Net5.Fundamentals.DI.MVC.ApplicationService.Core;
using Net5.Fundamentals.DI.MVC.ApplicationService.Scoped;
using Net5.Fundamentals.DI.MVC.ApplicationService.Singleton;
using Net5.Fundamentals.DI.MVC.ApplicationService.Transient;
using Net5.Fundamentals.DI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private IGuidTransientApplicationService _guidTransientApplicationService { get; }
        private IGuidScopedApplicationService _guidScopedApplicationService { get; }
        private IGuidSingletonApplicationService _guidSingletonApplicationService { get; }
        private ICoreApplicationService _coreApplicationService { get; }

        public HomeController(
            IGuidTransientApplicationService guidTransientApplicationService,
            IGuidScopedApplicationService guidScopedApplicationService,
            IGuidSingletonApplicationService guidSingletonApplicationService,

            ICoreApplicationService coreApplicationService
            )
        {
            _guidTransientApplicationService = guidTransientApplicationService;
            _guidScopedApplicationService = guidScopedApplicationService;
            _guidSingletonApplicationService = guidSingletonApplicationService;

            _coreApplicationService = coreApplicationService;
        }
        public IActionResult Index()
        {
            DIViewModel diViewModel = new DIViewModel();

            diViewModel.GuidTransient.GuidController = _guidTransientApplicationService.GetGuid();
            diViewModel.GuidTransient.GuidApplicationService = _coreApplicationService.GetGuidTransient();

            diViewModel.GuidScoped.GuidController = _guidScopedApplicationService.GetGuid();
            diViewModel.GuidScoped.GuidApplicationService = _coreApplicationService.GetGuidScoped();

            diViewModel.GuidSingleton.GuidController = _guidSingletonApplicationService.GetGuid();
            diViewModel.GuidSingleton.GuidApplicationService = _coreApplicationService.GetGuidSingleton();

            return View(diViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
