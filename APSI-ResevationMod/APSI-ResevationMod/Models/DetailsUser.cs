using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class DetailsUser
    {
        public User User { get; set; }
        public List<UserReservation> UserReservation { get; set; }

    }
}