using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class ProjectDetails
    {
        public List<PROJECT_EMPLOYEES_RESERVATION> reservations { get; set; }
        public List<EMPLOYEES> employees { get; set; }
        public PROJECTS project { get; set; }
        public List<RESOURCES> resources { get; set; }
        public List<RESOURCES_RESERVATIONS> resourceReservation { get; set; }
        public List<ROOM_RESERVATIONS> roomReservation { get; set; }
        
        public double reservedHours;
    }
}