﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Example
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CrudDBEntities : DbContext
    {
        public CrudDBEntities()
            : base("name=CrudDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<users>().Property(x => x.id_profile).HasColumnName("Profile");
            modelBuilder.Entity<profiles>().Property(x => x.id_product_group).HasColumnName("Groupe de produits");

            modelBuilder.Entity<users>()
                    .HasRequired<profiles>(s => s.profiles) 
                    .WithMany(s => s.users); 
            throw new UnintentionalCodeFirstException();
            
        }
    
        public virtual DbSet<product_groups> product_groups { get; set; }
        public virtual DbSet<profiles> profiles { get; set; }
        public virtual DbSet<users> users { get; set; }
    }
}
