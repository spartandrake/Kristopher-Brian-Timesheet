﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TimeClock.Models.Entities;

namespace K2BrianTimeClock.DAL.EF
{
    public class ClockContext : DbContext
    {
        public ClockContext()
        {

        }
        public ClockContext(DbContextOptions options) : base(options)
        {
            try
            {
                Database.Migrate();
            }
            catch
            {
                //Should do something intelligent here
            }

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=K2BRIANTIMECLOCK;Trusted_Connection=True;MultipleActiveResultSets=true;", options => options.EnableRetryOnFailure());
            }
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<HRManager> HRManagers { get; set; }
        public DbSet<DateAndTime> DateAndTimes { get; set; }
        public DbSet<ClockIn> ClockIns { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.EmailAddress).HasName("IX_Employees").IsUnique();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.CurrentWage).HasColumnType("money");
            });

    
            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.Property(e => e.TotalPay).HasColumnType("money");
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                //entity.Property(e => e.HoursWorked)
                //.HasComputedColumnSql("[]");
                entity.Property(e => e.TotalPay)
                .HasColumnType("money")
                .HasComputedColumnSql("[HoursWorked]*[CurrentWage]");
                
            });



        }
    }
}
