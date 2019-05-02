using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class Resource
    {
        public string name { get; set; }
        public int ID { get; set; }
        public string store { get; set; }
        public DateTime ReservationDataFrom { get; set; }
        public DateTime ReservationDataTo { get; set; }
    }
}