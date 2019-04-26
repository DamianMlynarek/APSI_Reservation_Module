using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class HardwareReservation
    {
        public string WhatIsReserved { get; set; }
        public string WhoReserves { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}