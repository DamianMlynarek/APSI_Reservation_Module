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
        private static EMPLOYEE currentEmployee = new EMPLOYEE();

        private static List<EMPLOYEE> _employees = new List<EMPLOYEE>();
        private static DbOperations dbOperations = new DbOperations();
        public ActionResult Index()
        {
            if(_employees.Count == 0)
                _employees = dbOperations.GetEmployees();
            return View(currentEmployee);
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
            if(User.Identity.IsAuthenticated == false)
                return RedirectToAction("NotAuthenticated");
            var employee = _employees.FirstOrDefault(e => e.AADName.ToLower() == User.Identity.Name.ToLower());
            if(employee == null)
                return RedirectToAction("UserNotExisitngInDB");
            if(employee != null && employee.EmployeeType == "Owner")
                _employees = dbOperations.GetEmployees();
            else
                return RedirectToAction("UnauthorizedRequest");
            return View(_employees);
        }
        //[AuthorizeAD(GroupId = "fe52b7e1-0d05-425c-a6d4-1b9d9d0e6616")]
        public ActionResult UserDetails(int? id)
        {
            ViewBag.Message = "User data";
            var employeeReservation = new EmployeeReservation();
            if(User.Identity.IsAuthenticated == false)
                return RedirectToAction("NotAuthenticated");
            var employee = _employees.FirstOrDefault(e => e.AADName.ToLower() == User.Identity.Name.ToLower());
            if(employee == null)
                return RedirectToAction("UserNotExisitngInDB");
            if(employee !=null && employee.EmployeeType=="Programmer")
            {
                employeeReservation.employee = employee;
                id = employee.EmployeeId;
                employeeReservation.reservations = dbOperations.GetEmployeeReservation(id.Value);
                employeeReservation.precentOfDaysReserved = DateUtils.CalculateProjectsLoadForEmployee(employeeReservation.reservations);
            }
            else
            {
                if(id.HasValue)
                {
                    employeeReservation.employee = _employees.FirstOrDefault(e => e.EmployeeId == id);
                    employeeReservation.reservations = dbOperations.GetEmployeeReservation(id.Value);
                    employeeReservation.precentOfDaysReserved = DateUtils.CalculateProjectsLoadForEmployee(employeeReservation.reservations);
                }
                else
                    return RedirectToAction("OnlyForProgrammers");
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
        public ActionResult NotAuthenticated()
        {
            return View();
        }
        public ActionResult OnlyForProgrammers()
        {
            return View();
        }

        public ActionResult ResourceData()
        {
            return View();

        }
        public ActionResult ResourceList()
        {
            var resources = new List<Resource>();
            resources.Add(new Resource { ID = 1, name = "Printer" });
            resources.Add(new Resource { ID = 2, name = "Pen" });
            resources.Add(new Resource { ID = 3, name = "Table" });
            resources.Add(new Resource { ID = 4, name = "Desk" });
            resources.Add(new Resource { ID = 5, name = "Mouse" });
            resources.Add(new Resource { ID = 6, name = "Keyboard" });
            resources.Add(new Resource { ID = 7, name = "Mobile touchpad" });

            return View(resources);
        }

        IList<Resource> resources = new List<Resource>() {
            new Resource { ID = 1, name = "Printer"  },
            new Resource { ID = 2, name = "Pen" },
            new Resource { ID = 3, name = "Table" },
            new Resource { ID = 4, name = "Desk" },
            new Resource { ID = 5, name = "Mouse" },
            new Resource { ID = 6, name = "Keyboard" },
            new Resource { ID = 7, name = "Mobile touchpad" }
            };

        public ActionResult Reserve(int Id)
        {
            //Get the student from studentList sample collection for demo purpose.
            //Get the student from the database in the real application
            var std = resources.Where(s => s.ID == Id).FirstOrDefault();

            return View(std);
        }
    }
}