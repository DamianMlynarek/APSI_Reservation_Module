using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class ProjectCreateView
    {
        public string name { get; set; }
        public int Project_ID { get; set; }
        public string ProjectOwner { get; set; }
        public DateTime ReservationDataFrom { get; set; }
        public DateTime ReservationDataTo { get; set; }
        public EMPLOYEE employee;
    }
}