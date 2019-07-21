using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(string statusCode)
        {
            string view;
            switch (statusCode)
            {
                case "404":
                    view = "NotFound";
                    ViewBag.ErrorMessage = "Resource you've requested could not be found";
                    break;
                default:
                    view = "NotFound";
                    break;
            }
            return View(view);
        }
    }
}