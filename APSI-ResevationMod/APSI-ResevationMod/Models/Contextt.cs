using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace APSI_ResevationMod.Models
{
    public class Contextt: DbContext
    {
        public Contextt() : base("APSIDbEntities")
        {

        }
        
        public DbSet<PROJECT> ProjectContext { get; set; }
    }
}