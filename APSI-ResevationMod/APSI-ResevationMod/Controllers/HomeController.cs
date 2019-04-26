using APSI_ResevationMod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APSI_ResevationMod.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult User()
        {
            DetailsUser model = new DetailsUser();

            model.User = new User
            {
                Name = "MeHow",
                Surname = "Kappa"
            };
            model.UserReservation = new List<UserReservation>();
            model.UserReservation.Add(new UserReservation
            {
                EmployeeID = "123",
                Project="APSI nr 4"

            })
            ViewBag.Message = "Your contact page.";

            return View();

        }

        public ActionResult Hardware()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }

        public ActionResult Room()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }
    }
}