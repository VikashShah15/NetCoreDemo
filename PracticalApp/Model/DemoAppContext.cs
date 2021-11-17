using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalApp.Model
{
    public class DemoAppContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SignInDetails> SignInDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-TDVJ1ST;Database=Demo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserName)                  
                    .IsUnicode(false);

                entity.Property(d => d.Password)            
                    .IsUnicode(false);

                entity.Property(d => d.isDeleted)                   
                   .IsUnicode(false);

                entity.Property(d => d.IsLocked)                   
                   .IsUnicode(false);
             });

            modelBuilder.Entity<SignInDetails>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.UserId)               
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)              
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)              
                    .IsUnicode(false);

                entity.Property(e => e.GuidId)              
                    .IsUnicode(false);

                entity.Property(e => e.isDeleted)                   
                    .IsUnicode(false);

                entity.Property(e => e.Attempt)                    
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductName)                    
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .IsUnicode(false);

                entity.Property(e => e.Price)      
                    .IsUnicode(false);

                entity.Property(e => e.isDeleted)         
                    .IsUnicode(false);              
            });
        }
    }
}
