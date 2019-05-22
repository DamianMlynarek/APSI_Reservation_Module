using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class ResourceDetails
    {
        public List<RESOURCES_RESERVATIONS> reservations { get; set; }
        public List<EMPLOYEES> employees { get; set; }
        public List<PROJECTS> projects { get; set; }
        public RESOURCES resource { get; set; }
    }
}