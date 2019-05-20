using APSI_ResevationMod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Core_Logic
{
    public class DbOperations
    {
        public List<EMPLOYEES> GetEmployees()
        {
            List<EMPLOYEES> employees = new List<EMPLOYEES>();
            using(var context = new APSITestDbEntities1 ())
            {
                employees = context.EMPLOYEES.ToList<EMPLOYEES>();
            }
            return employees;
        }
        public List<PROJECTS> GetProjects()
        {
            List<PROJECTS> projects = new List<PROJECTS>();
            using (var context = new APSITestDbEntities1())
            {
                projects = context.PROJECTS.ToList<PROJECTS>();
            }
            return projects;
        }
        public List<PROJECT_EMPLOYEES> GetEmployeeProjects(int EmployeeId)
        {
            List<PROJECT_EMPLOYEES> projects = new List<PROJECT_EMPLOYEES>();
            using(var context = new APSITestDbEntities1())
            {
                projects = context.PROJECT_EMPLOYEES.Where(r => r.EmployeeId == EmployeeId).ToList<PROJECT_EMPLOYEES>();
            }
            return projects;
        }
        public List<PROJECT_EMPLOYEES> GetEmployeeProjectsByPC(string ProjectCode)
        {
            List<PROJECT_EMPLOYEES> projects = new List<PROJECT_EMPLOYEES>();
            using (var context = new APSITestDbEntities1())
            {
                projects = context.PROJECT_EMPLOYEES.Where(r => r.ProjectCode == ProjectCode).ToList<PROJECT_EMPLOYEES>();
            }
            return projects;
        }
        public List<PROJECT_EMPLOYEES_RESERVATION> GetEmployeeReservation(int EmployeeId)
        {
            List<PROJECT_EMPLOYEES_RESERVATION> reservations = new List<PROJECT_EMPLOYEES_RESERVATION>();
            using(var context = new APSITestDbEntities1())
            {
                reservations = context.PROJECT_EMPLOYEES_RESERVATION.Where(r => r.EmployeeId == EmployeeId).ToList<PROJECT_EMPLOYEES_RESERVATION>();
            }
            return reservations;
        }
        public List<PROJECT_EMPLOYEES_RESERVATION> GetEmployeeReservationByPC(string ProjectCode)
        {
            List<PROJECT_EMPLOYEES_RESERVATION> reservations = new List<PROJECT_EMPLOYEES_RESERVATION>();
            using (var context = new APSITestDbEntities1())
            {
                reservations = context.PROJECT_EMPLOYEES_RESERVATION.Where(r => r.ProjectCode == ProjectCode).ToList<PROJECT_EMPLOYEES_RESERVATION>();
            }
            return reservations;
        }
        public List<RESOURCES> GetResources()
        {
            List<RESOURCES> resources = new List<RESOURCES>();
            using (var context = new APSITestDbEntities1())
            {
                resources = context.RESOURCES.ToList<RESOURCES>();
            }
            return resources;
        }
        public static void AddProjectToDB(PROJECTS model)
        {
            using (var context = new Contextt()) 
            {
                context.ProjectContext.Add(model);
                context.SaveChanges();
            };
        }
        public static void AddResourceToDB(RESOURCES model)
        {
            using (var context = new Contextt())
            {
                context.ResourceContext.Add(model);
                context.SaveChanges();
            };
        }
        public static void AddResourceReservationToDB(RESOURCES_RESERVATIONS model)
        {
            using (var context = new Contextt())
            {
                context.ResourceReservationContext.Add(model);
                context.SaveChanges();
            };
        }
        public static void AddEmployeeReservationToDB(PROJECT_EMPLOYEES_RESERVATION model)
        {
            using (var context = new Contextt())
            {
                context.ProjectEmployeeReservationContext.Add(model);
                context.SaveChanges();
            };
        }
        public static void AddProjectEmployeeToDB(PROJECT_EMPLOYEES model)
        {
            using (var context = new Contextt())
            {
                context.ProjectEmployeeContext.Add(model);
                context.SaveChanges();
            };
        }
    }
}