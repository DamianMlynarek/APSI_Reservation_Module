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
        private static EMPLOYEES _currentEmployee = new EMPLOYEES();

        private static List<ROOMS> _rooms = new List<ROOMS>();
        private static List<EMPLOYEES> _employees = new List<EMPLOYEES>();
        private static List<PROJECTS> _projects = new List<PROJECTS>();
        private static List<PROJECT_EMPLOYEES> _projectsEmployees = new List<PROJECT_EMPLOYEES>();
        private static DbOperations dbOperations = new DbOperations();
        private static PROJECT_EMPLOYEES_RESERVATION _reservation = new PROJECT_EMPLOYEES_RESERVATION();
        private static int _employeeId;
        private static string _roomId;
        private static int _loggedUserId = 3;
        private static double _reservedHoursForProject;
        private static List<RESOURCES> _resources = new List<RESOURCES>();
        private static RESOURCES_RESERVATIONS _resourceReservation = new RESOURCES_RESERVATIONS();


        public ActionResult Index()
        {
            if (_employees.Count == 0)
                _employees = dbOperations.GetEmployees();

            _currentEmployee = _employees.Find(e => e.AADName.ToLower() == User.Identity.Name.ToLower());

            return View(_currentEmployee);
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

        public ActionResult UserList()
        {

            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("NotAuthenticated");
            var employee = _employees.FirstOrDefault(e => e.AADName.ToLower() == User.Identity.Name.ToLower());
            if (employee == null)
                return RedirectToAction("UserNotExisitngInDB");
            if (employee != null && employee.EmployeeType == "Owner")
                _employees = dbOperations.GetEmployees();
            else
                return RedirectToAction("UnauthorizedRequest");

            //_employees = dbOperations.GetEmployees();
            return View(_employees);

        }

        [AuthorizeAD(GroupId = "fe52b7e1-0d05-425c-a6d4-1b9d9d0e6616")]
        public ActionResult UserDetails(int? id)
        {

            ViewBag.Message = "User data";
            var employeeReservation = new EmployeeReservation();
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("NotAuthenticated");
            var employee = _employees.FirstOrDefault(e => e.AADName.ToLower() == User.Identity.Name.ToLower());
            if (employee == null)
                return RedirectToAction("UserNotExisitngInDB");
            if (employee != null && employee.EmployeeType == "Programmer")
            {
                employeeReservation.employee = employee;
                id = employee.EmployeeId;
                employeeReservation.reservations = dbOperations.GetEmployeeReservation(id.Value);
                employeeReservation.precentOfDaysReserved = DateUtils.CalculateProjectsLoadForEmployee(employeeReservation.reservations);
            }
            else
            {
                if (id.HasValue)
                {

                    employeeReservation.employee = _employees.FirstOrDefault(e => e.EmployeeId == id);
                    employeeReservation.reservations = dbOperations.GetEmployeeReservation(id.Value);
                    employeeReservation.precentOfDaysReserved = DateUtils.CalculateProjectsLoadForEmployee(employeeReservation.reservations);
                }
                else
                    return RedirectToAction("OnlyForProgrammers");
            }

            //employeeReservation.employee = _employees.FirstOrDefault(e => e.EmployeeId == id);
            //employeeReservation.reservations = dbOperations.GetEmployeeReservation(id.Value);
            //employeeReservation.precentOfDaysReserved = DateUtils.CalculateProjectsLoadForEmployee(employeeReservation.reservations);

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
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("NotAuthenticated");

            _resources = dbOperations.GetResources();
            return View(_resources);
        }
        public ActionResult ResourceDetails(int id)
        {
            var RDetails = new ResourceDetails();
            _resources = dbOperations.GetResources();
            _employees = dbOperations.GetEmployees();
            _projects = dbOperations.GetProjects();
            RDetails.reservations = dbOperations.GetResourcesReservation(id);
            RDetails.resource = _resources.Find(s => s.ResourceId == id);
            RDetails.employees = new List<EMPLOYEES>();
            RDetails.projects = new List<PROJECTS>();

            foreach (var reservation in RDetails.reservations)
            {
                RDetails.employees.Add(_employees.Where(s => s.EmployeeId == reservation.EmployeeId).FirstOrDefault());
                RDetails.projects.Add(_projects.Where(s => s.ProjectCode == reservation.ProjectCode).FirstOrDefault());
            }

            return View(RDetails);
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
            _resourceReservation.ResourceId = id;
            var model = new AddResourceReservation();
            model.projects = dbOperations.GetProjects();
            return View(model);
        }
        [HttpPost]
        public ActionResult ResourceReservation(RESOURCES_RESERVATIONS model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _resourceReservation.EmployeeId = GetLoggedUserId();
            _resourceReservation.ProjectCode = model.ProjectCode;
            _resourceReservation.BeginDate = model.BeginDate;
            _resourceReservation.EndDate = model.EndDate;

            var ReservationList = dbOperations.GetResourcesReservation(_resourceReservation.ResourceId);
            TimeSpan beginDifference = new TimeSpan();
            TimeSpan endDifference = new TimeSpan();
            TimeSpan beginEndDifference = new TimeSpan();
            TimeSpan EndbeginDifference = new TimeSpan();
            TimeSpan zero = new TimeSpan(0, 0, 0, 0);
            foreach (var reservation in ReservationList)
            {
                beginDifference = reservation.BeginDate - _resourceReservation.BeginDate;
                endDifference = reservation.EndDate - _resourceReservation.EndDate;
                beginEndDifference = reservation.BeginDate - _resourceReservation.EndDate;
                EndbeginDifference = reservation.EndDate - _resourceReservation.BeginDate;

                if ((beginEndDifference <= zero && endDifference >= zero) || (endDifference <= zero && EndbeginDifference >= zero) || (beginEndDifference <= zero && beginDifference >= zero))
                    return RedirectToAction("ResourceIsReserved");

            }
            DbOperations.AddResourceReservationToDB(_resourceReservation);
            return RedirectToAction("ResourceList");
        }

        public ActionResult DeleteResourceReservation(int id)
        {
            _resourceReservation = dbOperations.GetResourcesReservationByReservID(id);

            return View(_resourceReservation);
        }
        [HttpPost]
        public ActionResult DeleteResourceReservation()
        {
            using (var context = new Contextt())
            {
                context.ResourceReservationContext.Remove(_resourceReservation);
                context.SaveChanges();
            };
            return RedirectToAction("Index");
        }
        public ActionResult ResourceIsReserved()
        {
            return View();
        }
        public ActionResult ProjectList()
        {
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("NotAuthenticated");

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
            model2.EmployeeId = GetLoggedUserId();
            DbOperations.AddProjectEmployeeToDB(model2);

            return RedirectToAction("ProjectList");
        }
        public ActionResult ProjectDetails(string ProjectCode)
        {
            var PDetails = new ProjectDetails();
            var EmployeesOfProject = new List<EMPLOYEES>();
            PDetails.resourceReservation = new List<RESOURCES_RESERVATIONS>();
            PDetails.resources = new List<RESOURCES>();
            _employees = dbOperations.GetEmployees();
            _resources = dbOperations.GetResources();
            var projectEmployees = dbOperations.GetEmployeeProjectsByPC(ProjectCode);

            foreach (var projectEmployee in projectEmployees)
            {
                EmployeesOfProject.Add(_employees.Where(s => s.EmployeeId == projectEmployee.EmployeeId).FirstOrDefault());
            }
            PDetails.employees = EmployeesOfProject;
            PDetails.reservations = dbOperations.GetEmployeeReservationByPC(ProjectCode); ;
            PDetails.project = _projects.Find(s => s.ProjectCode == ProjectCode);
            PDetails.resourceReservation = dbOperations.GetResourcesReservationByPC(ProjectCode);

            foreach (var resResevation in PDetails.resourceReservation)
            {
                PDetails.resources.Add(_resources.Where(s => s.ResourceId == resResevation.ResourceId).FirstOrDefault());
            }

            _reservedHoursForProject = 0;
            foreach(var reservation in PDetails.reservations)
            {
                var AmountOfHour = (reservation.EndDate - reservation.BeginDate).Days*8*0.01 * reservation.Extent;
                _reservedHoursForProject = _reservedHoursForProject+ AmountOfHour;
            }
            PDetails.reservedHours = _reservedHoursForProject;

            return View(PDetails);
        }

        public ActionResult EmployeeReservation(int id)
        {
            var model = new AddUserResevation();
            model.EmployeeId = new int();
            model.projects = dbOperations.GetProjects();
            model.EmployeeId = id;
            _employeeId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult EmployeeReservation(AddUserResevation reservation)
        {
            if (!ModelState.IsValid)
            {
                return View(reservation);
            }
            var model = new PROJECT_EMPLOYEES_RESERVATION();
            var modelEmployee = new PROJECT_EMPLOYEES();

            model.EmployeeId = _employeeId;
            model.ProjectOwnerId = GetLoggedUserId();
            model.BeginDate = reservation.BeginDate;
            model.EndDate = reservation.EndDate;
            model.Extent = reservation.Extent;
            model.ProjectCode = reservation.ProjectCode;

            var ReservationList = dbOperations.GetEmployeeReservation(model.EmployeeId);
            TimeSpan beginDifference = new TimeSpan();
            TimeSpan endDifference = new TimeSpan();
            TimeSpan beginEndDifference = new TimeSpan();
            TimeSpan EndbeginDifference = new TimeSpan();
            TimeSpan zero = new TimeSpan(0, 0, 0, 0);
            foreach (var reserv in ReservationList)
            {
                beginDifference = reserv.BeginDate - model.BeginDate;
                endDifference = reserv.EndDate - model.EndDate;
                beginEndDifference = reserv.BeginDate - model.EndDate;
                EndbeginDifference = reserv.EndDate - model.BeginDate;

                if ((beginEndDifference <= zero && endDifference >= zero)
                    || (endDifference <= zero && EndbeginDifference >= zero)
                    || (beginEndDifference <= zero && beginDifference >= zero))
                    return RedirectToAction("EmployeeIsReservedTime");

            }
            var employeeProjects = dbOperations.GetEmployeeProjects(_employeeId);
            var noToDoubleProjectEmployees = employeeProjects.Where(s => s.ProjectCode.Replace(" ", string.Empty) == reservation.ProjectCode.Replace(" ", string.Empty)).ToList();

            if (noToDoubleProjectEmployees.Count() == 0)
            {
                modelEmployee.EmployeeId = _employeeId;
                modelEmployee.ProjectCode = reservation.ProjectCode;
                modelEmployee.ProjectOwner = false;
                DbOperations.AddProjectEmployeeToDB(modelEmployee);
            }



            DbOperations.AddEmployeeReservationToDB(model);
            return RedirectToAction("UserList");
        }
        public ActionResult DeleteEmployeeReservation(int id)
        {
            _reservation = dbOperations.GetEmployeeReservationByReservID(id);

            return View(_reservation);
        }
        [HttpPost]
        public ActionResult DeleteEmployeeReservation()
        {
            using (var context = new Contextt())
            {
                context.ProjectEmployeeReservationContext.Remove(_reservation);
                context.SaveChanges();
            };
            return RedirectToAction("Index");
        }


        public ActionResult EmployeeIsReservedTime()
        {
            return View();
        }

        public int GetLoggedUserId()
        {
           // return 3;
            var loggedUser = _employees.Find(e => e.AADName.ToLower() == User.Identity.Name.ToLower());
            var userID = loggedUser.EmployeeId;
            return userID;
        }

        public ActionResult EmployeeIsReserved()
        {
            return View();
        }
        public ActionResult RoomReservation(string code)
        {
            _roomId = code;
            AddRoomReservation model = new AddRoomReservation();
            model.projects = dbOperations.GetProjects();
            return View(model);
        }
        [HttpPost]
        public ActionResult RoomReservation(ROOM_RESERVATIONS model)
        {
            var ReservationList = dbOperations.GetRoomReservation(model.RoomCode); 
            TimeSpan beginDifference = new TimeSpan();
            TimeSpan endDifference = new TimeSpan();
            TimeSpan beginEndDifference = new TimeSpan();
            TimeSpan EndbeginDifference = new TimeSpan();
            TimeSpan zero = new TimeSpan(0, 0, 0, 0);
            foreach (var reservation in ReservationList)
            {
                beginDifference = reservation.BeginDate - model.BeginDate;
                endDifference = reservation.EndDate - model.EndDate;
                beginEndDifference = reservation.BeginDate - model.EndDate;
                EndbeginDifference = reservation.EndDate - model.BeginDate;

                if ((beginEndDifference <= zero && endDifference >= zero) || (endDifference <= zero && EndbeginDifference >= zero) || (beginEndDifference <= zero && beginDifference >= zero))
                    return RedirectToAction("RoomIsReserved");

            }

            model.RoomCode = _roomId;
            model.EmployeeId = GetLoggedUserId();
            DbOperations.AddRoomReservationToDb(model);
            return RedirectToAction("RoomList");
        }
        public ActionResult RoomList()
        {
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("NotAuthenticated");

            _rooms = dbOperations.GetRooms();
        return View(_rooms);
        }
        public ActionResult RoomDetails(string code)
        {
            var model = new RoomDetails();
            model.reservations=dbOperations.GetRoomReservation(code);
            model.RoomCode = code;
            model.employees = dbOperations.GetEmployees();
            model.projects = dbOperations.GetProjects();
            return View(model);
        }
    }
}