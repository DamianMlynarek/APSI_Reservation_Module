﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APSI_ResevationMod.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class APSIDbEntities : DbContext
    {
        public APSIDbEntities()
            : base("name=APSIDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EMPLOYEE> EMPLOYEES { get; set; }
        public virtual DbSet<PROJECT_EMPLOYEES> PROJECT_EMPLOYEES { get; set; }
        public virtual DbSet<PROJECT_EMPLOYEES_RESERVATION> PROJECT_EMPLOYEES_RESERVATION { get; set; }
        public virtual DbSet<PROJECT> PROJECTS { get; set; }
    }
}