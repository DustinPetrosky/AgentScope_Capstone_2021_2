﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgentScope_Capstone_2021_2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CapstoneProjectEntities : DbContext
    {
        public CapstoneProjectEntities()
            : base("name=CapstoneProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AgentAccount> AgentAccounts { get; set; }
        public virtual DbSet<AgentHomeSold> AgentHomeSolds { get; set; }
        public virtual DbSet<AgentReview> AgentReviews { get; set; }
        public virtual DbSet<ReviewerAccount> ReviewerAccounts { get; set; }
    }
}
