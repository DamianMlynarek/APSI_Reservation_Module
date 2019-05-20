using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class ProjectDetails
    {
        public List<PROJECT_EMPLOYEES_RESERVATION> reservations { get; set; }
        public List<EMPLOYEE> employees { get; set; }
        public PROJECT project { get; set; }
    }
}