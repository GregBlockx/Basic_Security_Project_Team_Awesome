using databaseConnectionTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace databaseConnectionTest.Controllers
{
    public class HomeController : Controller
    {
        login myPerson = new login();
        public ActionResult Index()
        {
            return View(myPerson);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}