using APSI_ResevationMod.Core_Logic;
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
        private static List<EMPLOYEE> _employees;
        private static DbOperations dbOperations = new DbOperations();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AddUserReservation()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult UserList()
        {
            _employees = new List<EMPLOYEE>();
            _employees = dbOperations.GetEmployees();
            return View(_employees);
        }
        
        public ActionResult UserDetails(int? id)
        {
            ViewBag.Message = "User data";
            var employeeReservation = new EmployeeReservation();
            if(User.Identity.IsAuthenticated)
            {
                //employeeReservation.employee = _employees.FirstOrDefault(e => e.AADName.ToLower() == User.Identity.Name.ToLower());
                if(employeeReservation.employee == null)
                {
                    RedirectToAction("UserNotExisitngInDB");
                }
                id = employeeReservation.employee.EmployeeId;
            }
            else
            {
                employeeReservation.employee = _employees.FirstOrDefault(e => e.EmployeeId == id);
            }
            if(id.HasValue)
            {
                employeeReservation.reservations = dbOperations.GetEmployeeReservation(id.Value);
                employeeReservation.precentOfDaysReserved = DateUtils.CalculateProjectsLoadForEmployee(employeeReservation.reservations);
            }
            return View(employeeReservation);

        }

        public ActionResult Hardware()
        {
            return View();
        }

        public ActionResult UserNotExisitngInDB()
        {
            return View();
        }

        public ActionResult Room()
        {
            return View();
        }
        public ActionResult UnauthorizedRequest()
        {
            return View();
        }
    }
}