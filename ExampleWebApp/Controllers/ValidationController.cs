using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApplication.Models;
using TestWebApplication;
using mUtility.Validation;

namespace TestWebApplication.Controllers
{
    public class ValidationController : Controller
    {
        [HttpPost]
        public JsonResult Index(FormModel model)
        {
            return this.GetModelErrors<FormModel>(model);
        }
	}
}