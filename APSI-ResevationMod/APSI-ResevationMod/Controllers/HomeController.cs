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
        private static EMPLOYEES currentEmployee = new EMPLOYEES();

        private static List<EMPLOYEES> _employees = new List<EMPLOYEES>();
        private static List<PROJECTS> _projects = new List<PROJECTS>();
        private static List<PROJECT_EMPLOYEES> _projectsEmployees = new List<PROJECT_EMPLOYEES>();
        private static DbOperations dbOperations = new DbOperations();
        private static PROJECT_EMPLOYEES_RESERVATION _reservation= new PROJECT_EMPLOYEES_RESERVATION();
        private static int _employeeId;
        private static int _loggedUserId;
        private static List<RESOURCES> _resources = new List<RESOURCES>();
        

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

        public ActionResult Reservations()
        {
            
            return View();
        }
        public ActionResult UserListFromProject(string ProjectCode)
        {
            _employees = dbOperations.GetEmployees();

            return View(_employees);
        }
        public ActionResult UserList()
        {
            /*if (id.HasValue)
            {
                _employeeId = id.Value;
            }
            if(User.Identity.IsAuthenticated == false)
                return RedirectToAction("NotAuthenticated");
            var employee = _employees.FirstOrDefault(e => e.AADName.ToLower() == User.Identity.Name.ToLower());
            if(employee == null)
                return RedirectToAction("UserNotExisitngInDB");
            if(employee != null && employee.EmployeeType == "Owner")
                _employees = dbOperations.GetEmployees();
            else
                return RedirectToAction("UnauthorizedRequest");*/
            _employees = dbOperations.GetEmployees();

            return View(_employees);
        }
        
        //[AuthorizeAD(GroupId = "fe52b7e1-0d05-425c-a6d4-1b9d9d0e6616")]
        public ActionResult UserDetails(int? id)
        {
            
            ViewBag.Message = "User data";
            var employeeReservation = new EmployeeReservation();
            /*if(User.Identity.IsAuthenticated == false)
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
            */
            employeeReservation.employee = _employees.FirstOrDefault(e => e.EmployeeId == id);
            employeeReservation.reservations = dbOperations.GetEmployeeReservation(id.Value);
            employeeReservation.precentOfDaysReserved = DateUtils.CalculateProjectsLoadForEmployee(employeeReservation.reservations);

            return View(employeeReservation);
        }
        
        public ActionResult UserNotExisitngInDB()
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
            _resources = dbOperations.GetResources();
            var resources = new List<Resource>();
            resources.Add(new Resource { ID = 1, name = "Printer" });
            resources.Add(new Resource { ID = 2, name = "Pen" });
            resources.Add(new Resource { ID = 3, name = "Table" });
            resources.Add(new Resource { ID = 4, name = "Desk" });
            resources.Add(new Resource { ID = 5, name = "Mouse" });
            resources.Add(new Resource { ID = 6, name = "Keyboard" });
            resources.Add(new Resource { ID = 7, name = "Mobile touchpad" });

            return View(_resources);
        }

        public ActionResult CreateResource()
        {
            var model = new RESOURCES();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateResource(RESOURCES model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DbOperations.AddResourceToDB(model);
            return RedirectToAction("ProjectList");
        }
       public ActionResult ResourceReservation(int id)
        {
            var model = new RESOURCES_RESERVATIONS();
            model.ResourceId = id;
            model.EmployeeId = GetLoggedUserId();
            
            return View(model);
        }
        [HttpPost]
        public ActionResult ResourceReservation (RESOURCES_RESERVATIONS model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DbOperations.AddResourceReservationToDB(model);
            return RedirectToAction("ResourceList");
        }

        public ActionResult ProjectList()
        {
            _loggedUserId = GetLoggedUserId();
            _projects = dbOperations.GetProjects();
            _projectsEmployees = dbOperations.GetEmployeeProjects(_loggedUserId);
            var POProjects = new List<PROJECTS>();
            foreach (var projectEmp in _projectsEmployees)
            {
                POProjects.Add(_projects.Where(s => s.ProjectCode == projectEmp.ProjectCode).FirstOrDefault());
            }
            //projects for owner only
            return View(POProjects);//okrojona lista projektow
            
        }

        public ActionResult CreateProject()
        {
            var model = new PROJECTS();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateProject(PROJECTS model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            DbOperations.AddProjectToDB(model);

            var model2 = new PROJECT_EMPLOYEES();
            model2.ProjectCode = model.ProjectCode;
            model2.ProjectOwner = true;
            model2.EmployeeId = _loggedUserId;
            DbOperations.AddProjectEmployeeToDB(model2);

            return RedirectToAction("ProjectList");
        }


        public ActionResult EmployeeReservation(int id)
        {
            var reservation = new AddUserResevation();
            reservation.EmployeeId = new int();
            reservation.projects = dbOperations.GetProjects();
            reservation.EmployeeId = id;
            _loggedUserId = id;
            _employeeId = id;
            return View(reservation);
        }

        [HttpPost]
        public ActionResult EmployeeReservation(AddUserResevation reservation)
        {
            if (!ModelState.IsValid)
            {
                return View(reservation);
            }
            var employeeProjects = dbOperations.GetEmployeeProjects(_employeeId);
            var x = employeeProjects.Where(s => s.ProjectCode == reservation.ProjectCode).ToList();
            if (x.Count()>0 )
            {
                return RedirectToAction("EmployeeIsReserved");
            }
           
            var model = new PROJECT_EMPLOYEES_RESERVATION();
            var modelEmployee = new PROJECT_EMPLOYEES();

            modelEmployee.EmployeeId = _loggedUserId;
            modelEmployee.ProjectCode = reservation.ProjectCode;
            modelEmployee.ProjectOwner = true;
            DbOperations.AddProjectEmployeeToDB(modelEmployee);
            
            
            model.EmployeeId = _employeeId;
            model.ProjectOwnerId = _loggedUserId;
            model.BeginDate = reservation.BeginDate;
            model.EndDate = reservation.EndDate;
            model.Extent = reservation.Extent;
            model.ProjectCode = reservation.ProjectCode;
            DbOperations.AddEmployeeReservationToDB(model);
            return RedirectToAction("UserList");
        }
        public ActionResult ProjectDetails(string ProjectCode)
        {
            var PDetails = new ProjectDetails();
            var EmployeesOfProject = new List<EMPLOYEES>();
            _employees = dbOperations.GetEmployees();
             var projectEmployees = dbOperations.GetEmployeeProjectsByPC(ProjectCode);
            
            foreach (var projectEmployee in projectEmployees)
            {
                EmployeesOfProject.Add(_employees.Where(s => s.EmployeeId == projectEmployee.EmployeeId).FirstOrDefault());
            }
            PDetails.employees = EmployeesOfProject;
            PDetails.reservations = dbOperations.GetEmployeeReservationByPC(ProjectCode); ;
            PDetails.project = _projects.Find(s => s.ProjectCode == ProjectCode);
           
            return View(PDetails);
        }
        public int GetLoggedUserId()
        {
            return 3;
           // return (_projectOwnerId);
        }

        public ActionResult EmployeeIsReserved()
        {
            return View();
        }
    }
}