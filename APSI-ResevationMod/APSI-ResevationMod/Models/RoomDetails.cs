using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class RoomDetails: ROOMS
    {
        public List<ROOM_RESERVATIONS> reservations { get; set; }
        public List<EMPLOYEES> employees { get; set; }
        public List<PROJECTS> projects { get; set; }
    }
}