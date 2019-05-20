using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class AddUserResevation:PROJECT_EMPLOYEES_RESERVATION
    {
        public List<PROJECTS> projects { get; set; }
    }
}