﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string ProjectCode { get; set; }

        public string ProjectOwner { get; set; }
    }
}