﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class ProjectList:EMPLOYEES
    {
        public List<PROJECTS> projects { get; set; }
        
    }
}