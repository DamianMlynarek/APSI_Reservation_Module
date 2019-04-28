﻿using APSI_ResevationMod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Core_Logic
{
    public class DbOperations
    {
        public List<EMPLOYEE> GetEmployees()
        {
            List<EMPLOYEE> employees = new List<EMPLOYEE>();
            using(var context = new APSIDbEntities())
            {
                employees = context.EMPLOYEES.ToList<EMPLOYEE>();
            }
            return employees;
        }
        public List<PROJECT_EMPLOYEES> GetEmployeeProjects(int EmployeeId)
        {
            List<PROJECT_EMPLOYEES> projects = new List<PROJECT_EMPLOYEES>();
            using(var context = new APSIDbEntities())
            {
                projects = context.PROJECT_EMPLOYEES.Where(r => r.EmployeeId == EmployeeId).ToList<PROJECT_EMPLOYEES>();
            }
            return projects;
        }
        public List<PROJECT_EMPLOYEES_RESERVATION> GetEmployeeReservation(int EmployeeId)
        {
            List<PROJECT_EMPLOYEES_RESERVATION> reservations = new List<PROJECT_EMPLOYEES_RESERVATION>();
            using(var context = new APSIDbEntities())
            {
                reservations = context.PROJECT_EMPLOYEES_RESERVATION.Where(r => r.EmployeeId == EmployeeId).ToList<PROJECT_EMPLOYEES_RESERVATION>();
            }
            return reservations;
        }
    }
}