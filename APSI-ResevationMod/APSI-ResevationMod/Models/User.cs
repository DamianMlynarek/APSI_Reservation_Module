using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class User
    {
        public string Name { get; set; }
        public string EmployeeID { get; set; }
        public string Surname { get; set; }
        public enum EmployeeType { Employee, ProjectOwner, Admin}
        

    }
}