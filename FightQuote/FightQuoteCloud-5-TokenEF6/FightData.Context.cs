﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FightQuoteCloud
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ufcdataEntities : DbContext
    {
        public ufcdataEntities()
            : base("name=yourdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<fighter_details> fighter_details { get; set; }
        public virtual DbSet<total_fight_data> total_fight_data { get; set; }
        public virtual DbSet<quick_fight_facts> quick_fight_facts { get; set; }
    
        public virtual ObjectResult<get_random_fight_fact_Result> get_random_fight_fact()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<get_random_fight_fact_Result>("get_random_fight_fact");
        }
    }
}
