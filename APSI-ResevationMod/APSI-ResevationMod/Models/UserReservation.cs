using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class UserReservation
    {
        public string Project { get; set; }
        public string EmployeeID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public  int TimePercentReserved { get; set; }

    }
}