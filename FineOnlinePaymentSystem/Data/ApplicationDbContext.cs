using System;
using System.Collections.Generic;
using System.Text;
using FineOnlinePaymentSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FineOnlinePaymentSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Models that will be used as tables
        public DbSet<Offender> Offenders { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseOffender> CaseOffenders { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<FinePayment> FinePayments { get; set; }
        public DbSet<Amortization> Amortizations { get; set; }
        public DbSet<AmortizationSettings> AmortizationSettings { get; set; }
        public DbSet<Offence> Offences { get; set; }
        public DbSet<OffenderStatus> OffenderStatuses { get; set; }
        public DbSet<CaseStatus> CaseStatuses { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Setting up fields that will be Unique in the database
            builder.Entity<Offender>().HasIndex(o => o.PIN).IsUnique();
            builder.Entity<Relative>().HasIndex(r => r.PIN).IsUnique();
            builder.Entity<Officer>().HasIndex(ofc => ofc.FroceNumber).IsUnique();
            builder.Entity<Case>().HasIndex(c => c.CaseNumber).IsUnique();


            //Case Offender relationships
            builder.Entity<CaseOffender>().HasKey(co => new { co.CaseID, co.OffenderID });

            //Many To Many relationship configuration for CaseOffender Table
            builder.Entity<CaseOffender>().HasOne(co => co.Case).WithMany(co => co.CaseOffenders).HasForeignKey(co => co.CaseID);
            builder.Entity<CaseOffender>().HasOne(co => co.Offender).WithMany(co => co.CaseOffenders).HasForeignKey(co => co.OffenderID);

        }
    }
}
