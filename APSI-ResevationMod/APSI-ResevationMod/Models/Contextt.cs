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
        
        public DbSet<PROJECTS> ProjectContext { get; set; }
        public DbSet<PROJECT_EMPLOYEES_RESERVATION> ProjectEmployeeReservationContext { get; set; }
        public DbSet<PROJECT_EMPLOYEES> ProjectEmployeeContext { get; set; }
        public DbSet<RESOURCES> ResourceContext { get; set; }
        public DbSet<RESOURCES_RESERVATIONS> ResourceReservationContext { get; set; }

    }
}