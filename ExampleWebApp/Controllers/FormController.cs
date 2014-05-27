using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class FormController : Controller
    {
        //
        // GET: /Form/
        [HttpGet]
        public ActionResult Index()
        {
            return View(new FormModel());
        }

        [HttpPost]
        public ActionResult Index(FormModel model)
        {
            return View(model);
        }
	}
}