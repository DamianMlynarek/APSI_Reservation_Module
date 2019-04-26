using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class UserReservation
    {
        public string Project { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public  int TimePercentReserved { get; set; }

    }
}