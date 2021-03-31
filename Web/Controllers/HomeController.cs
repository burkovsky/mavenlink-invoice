using System.IO;

using Abstractions;

using Core;

using Microsoft.AspNetCore.Mvc;

using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReportHandler _reportHandler;

        public HomeController(IReportHandler reportHandler)
        {
            _reportHandler = reportHandler;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public IActionResult Index(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!Path.GetExtension(model.Report.FileName).Contains("csv"))
                {
                    ModelState.AddModelError(nameof(model.Report), "Please upload a file in CSV format.");
                }
                else
                {
                    model.Invoices = _reportHandler
                        .ProcessReport(model.Report, out string message)
                        .ToInvoices();

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        ModelState.AddModelError(nameof(model.Report), message);
                    }
                }
            }

            return View(model);
        }
    }
}
