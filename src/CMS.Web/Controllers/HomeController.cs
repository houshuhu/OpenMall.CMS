using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CMS.IApplication;

namespace CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserApplication _userApplication;

        public HomeController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            Thread.Sleep(50000);
            return View();
        }

        public async Task<JsonResult> Insert()
        {
             var result=await _userApplication.Insert();
            return new JsonResult(){Data = result};
        }
    }
}