using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class DetailsRoom
    {
        public Room Room { get; set; }
        public List<RoomReservation> RoomReservations { get; set; }
    }
}