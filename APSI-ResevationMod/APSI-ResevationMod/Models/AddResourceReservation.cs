﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class AddResourceReservation: RESOURCES_RESERVATIONS
    {
        public List<PROJECTS> projects { get; set; }
    }
}