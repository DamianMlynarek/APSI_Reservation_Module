using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class DetailHardware
    {
        public Hardware Hardware { get; set; }
        public List<HardwareReservation> HardwareReservations { get; set; }
    }
}